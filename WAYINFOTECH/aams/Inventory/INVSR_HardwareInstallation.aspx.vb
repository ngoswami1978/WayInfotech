Imports System.Xml
Imports System.Data
Partial Class Inventory_INVSR_Installations
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim str As String
    Protected strIndex As String
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        'If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
        '    If Response.ContentType = "text/html" Then
        '        Response.Filter = New TrimStream(Response.Filter)
        '    End If
        'End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            btnSearch.Attributes.Add("onclick", "return CheckValidation();")
            btnDisplay.Attributes.Add("onclick", "return CheckValidation();")
            btnExport.Attributes.Add("onclick", "return SetValueForExport();")
            btnExport.Attributes.Add("onclick", "return CheckValidationFroExport();")
            txtAgencyName.Attributes.Add("onkeydown", "return ActDeAct();")
            drpOrderType.Attributes.Add("onChange", "return setOrderTypeValue();")

            ''Added by Tapan Nath for LCODE, & Chain Code 18/03/2011
            txtLcode.Attributes.Add("onfocusout", "return EnableDisableGroupProductivity();")
            txtChainCode.Attributes.Add("onfocusout", "return EnableDisableGroupProductivity();")
            txtAgencyName.Attributes.Add("onfocusout", "return ActDecLcodeChainCode();")
            ''Added by Tapan Nath for LCODE, & Chain Code

            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If
            ' btnReset.Attributes.Add("onclick", "return InstallationReset();")
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='HardwareInstallationReport']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='HardwareInstallationReport']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                    If strBuilder(4) = "0" Then
                        btnDisplay.Enabled = False
                        btnExport.Enabled = False
                    End If
                End If
            Else
                objeAAMS.SecurityCheck(31)
            End If

            If Not Page.IsPostBack Then
                ' Load Data For Editing Mode
                Bindata()
                BindAllControl()
                If hdTabType.Value = "" Then
                    hdTabType.Value = 0
                End If
            End If
           
            'If hdTabType.Value = 0 Then
            '    ' hdTabType.Value = hdTabType.Value + 1
            'Else
            '    strIndex = hdTabType.Value
            'End If
            If Not IsPostBack Then
                txtInstallationFrom.Text = "01/01/1994"
                Dim strTodate As String = System.DateTime.Now.ToString("dd/MM/yyyy")
                txtInstallationTo.Text = strTodate
            End If

            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If

            strIndex = hdTabType.Value
            If Not IsPostBack Then

            End If

            If hdExport.Value = "" Then
                If hdTabType.Value = 1 Then
                    HardwareInstallationSearch()
                ElseIf hdTabType.Value = 2 Then
                    HardwareInstallationSearch()
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub bindOrderStatusNew()
        Dim objbzOrderType As New AAMS.bizTravelAgency.bzOrderType
        Dim objOutputXml As New XmlDocument
        Dim objInputXml As New XmlDocument
        '        Dim objNodeList As XmlNodeList
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet

        drpOrderType.Items.Clear()
        objInputXml.LoadXml("<UP_LISTORDERTYPE_INPUT><ORDER_TYPE></ORDER_TYPE></UP_LISTORDERTYPE_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("ORDER_TYPE").InnerText = "4"
        objOutputXml = objbzOrderType.List(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            drpOrderType.DataSource = ds.Tables("ORDER_TYPE")
            drpOrderType.DataTextField = "ORDER_TYPE_NAME"
            drpOrderType.DataValueField = "ORDERTYPEID"
            drpOrderType.DataBind()
        End If

        'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
        '    objNodeList = objOutputXml.SelectNodes("UP_LISTORDERTYPE_OUTPUT/ORDER_TYPE")
        '    For Each objXmlNode As XmlNode In objNodeList
        '        drpOrderType.Items.Add(New ListItem(objXmlNode.Attributes("ORDER_TYPE_NAME").InnerText, objXmlNode.Attributes("ORDERTYPEID").InnerText))
        '    Next
        'Else
        '    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        'End If
        drpOrderType.Items.Insert(0, New ListItem("--All--", ""))

    End Sub
    Private Sub BindAllControl()
        Try
            objeAAMS.BindDropDown(drpCountry1, "COUNTRY", True, 3)
            objeAAMS.BindDropDown(drpCity1, "CITY", True, 3)
            objeAAMS.BindDropDown(ddlRegion, "REGION", True, 3)
            objeAAMS.BindDropDown(drpAoffice1, "AOFFICE", True, 3)
            objeAAMS.BindDropDown(drpEquipmentGroup, "EQUIPMENTGROUP", True, 3)
            objeAAMS.BindDropDown(DlstCompVertical, "CompanyVertical", True, 3)

            bindOrderStatusNew()

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Sub Bindata()
        Dim TabText As New ArrayList()
        Try
            TabText.Add("PC Installations")
            TabText.Add("MISC Installations")
            theTabStrip.DataSource = TabText
            theTabStrip.DataBind()
            hideTabStrip(strIndex)
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub hideTabStrip(ByVal strIndex As String)
        Dim strScript As String = ""
        Select Case strIndex
            Case "1"
                strScript = "<script language='javascript'>" & _
                "document.getElementById('pnlPCInstallation').style.display ='block'; " & _
                "document.getElementById('pnlmiscInstallation').style.display  ='none';" & _
                "</script>"

            Case "2"
                strScript = "<script language='javascript'>" & _
               "document.getElementById('pnlPCInstallation').style.display ='none'; " & _
               "document.getElementById('pnlmiscInstallation').style.display  ='block';" & _
               "</script>"
        End Select
        ' ClientScript.RegisterStartupScript(Me.GetType(),"strScript", strScript)
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            HardwareInstallationSearch()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Public Sub HardwareInstallationSearch()
        hdExport.Value = ""

        Dim objInputXml, objOutputXml, objOutputXml1, objInputXml1 As New XmlDocument
        '        Dim objParentNode As XmlNode
        Dim objXmlReader, objXmlReader1 As XmlNodeReader
        Dim ds, ds1 As New DataSet
        Dim objbzINV As New AAMS.bizMaster.bzEquipment
        Dim objbzInstallation As New AAMS.bizInventory.bzChallan
        Dim objbzMiscInstallation As New AAMS.bizInventory.bzChallan
        Try
            lblError.Text = ""
            objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE /><AGENCYNAME /><CITY /><COUNTRY /><AOFFICE /><REGION /><WHOLEGROUP /><ONLINESTATUS /><EQUIPMENTGROUP /><EQUIPMENTTYPE /><DATEFROM /><DATETO /><RESPONSIBLESTAFFID /><VENDERSERIALNO /><SECREGIONID /><FPURDATE /><TPURDATE /><VENDORSRNOFLTRTYPE /><RESP1A/><ORDERTYPEID/><LCODE_1/><CHAINCODE/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><COMP_VERTICAL></COMP_VERTICAL></INV_RPTPCINSTALL_INPUT>")
            objInputXml1.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE /><AGENCYNAME /><CITY /><COUNTRY /><AOFFICE /><REGION /><WHOLEGROUP /><ONLINESTATUS /><EQUIPMENTGROUP /><EQUIPMENTTYPE /><DATEFROM /><DATETO /><RESPONSIBLESTAFFID /><VENDERSERIALNO /><SECREGIONID /><FPURDATE /><TPURDATE /><VENDORSRNOFLTRTYPE /><RESP1A/><ORDERTYPEID/><LCODE_1/><CHAINCODE/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><COMP_VERTICAL></COMP_VERTICAL></INV_RPTPCINSTALL_INPUT>")

            'objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hidLcode.Value
            'objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text
            naviGate.Style.Add("display", "block")


            If hdTabType.Value = 0 Or hdTabType.Value = 1 Then
                drpOrderType.Enabled = True
                hdDefaultPC.Value = 1
               
                If Val(hdOrderTypeSelectedIndex.Value & "") <> 0 Then
                    If drpOrderType.SelectedIndex = 0 Then
                        If Val(hdOrderTypeSelectedIndex.Value & "") <> 0 Then
                            drpOrderType.SelectedIndex = Val(hdOrderTypeSelectedIndex.Value & "")
                        Else
                            drpOrderType.SelectedIndex = 0
                        End If

                    Else
                        drpOrderType.SelectedIndex = Val(hdOrderTypeSelectedIndex.Value & "")
                    End If

                ElseIf drpOrderType.SelectedIndex = 0 Then
                    drpOrderType.SelectedIndex = 0
                End If

                If DlstCompVertical.SelectedValue <> "" Then
                    objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
                End If


                ViewState("PrevSearchingMisc") = Nothing
                'If (Request.Form("txtAgencyName") <> "" And hidLcode.Value = "") Or (Request.Form("txtAgencyName") = "" Or hidLcode.Value = "") Then
                '    objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = ""
                '    objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text.Trim()
                'Else
                '    objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hidLcode.Value
                '    objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = ""
                'End If

                If (Request.Form("txtAgencyName") <> "" And hdAgencyName.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyName.Value = "") Then
                    objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = ""
                Else
                    objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyName.Value.Trim()
                End If

                objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text

                If (drpCity1.SelectedIndex <> 0) Then
                    objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = drpCity1.SelectedItem.Text

                End If
                If (drpCountry1.SelectedIndex <> 0) Then
                    objInputXml.DocumentElement.SelectSingleNode("COUNTRY").InnerText = drpCountry1.SelectedItem.Text
                End If
                If (drpAoffice1.SelectedIndex <> 0) Then
                    objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = drpAoffice1.SelectedItem.Text
                End If
                If (ddlRegion.SelectedIndex <> 0) Then
                    objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = ddlRegion.SelectedItem.Text
                End If

                objInputXml.DocumentElement.SelectSingleNode("RESP1A").InnerText = txtAResponsibility.Text
                If drpOrderType.SelectedIndex <> 0 Then
                    objInputXml.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText = drpOrderType.SelectedValue 'drpOrderType.SelectedItem.Text
                End If

                If (drpEquipmentGroup.SelectedIndex <> 0) Then
                    objInputXml.DocumentElement.SelectSingleNode("EQUIPMENTGROUP").InnerText = drpEquipmentGroup.SelectedItem.Text
                End If
                objInputXml.DocumentElement.SelectSingleNode("EQUIPMENTTYPE").InnerText = Trim(txtEquipmentType.Text) & ""
                objInputXml.DocumentElement.SelectSingleNode("DATEFROM").InnerText = objeAAMS.ConvertTextDate(txtInstallationFrom.Text)
                objInputXml.DocumentElement.SelectSingleNode("DATETO").InnerText = objeAAMS.ConvertTextDate(txtInstallationTo.Text)
                objInputXml.DocumentElement.SelectSingleNode("FPURDATE").InnerText = objeAAMS.ConvertTextDate(txtPurchaseFrom.Text)
                objInputXml.DocumentElement.SelectSingleNode("TPURDATE").InnerText = objeAAMS.ConvertTextDate(txtPurchaseTo.Text)
                objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = txtVendorSerialNo.Text
                objInputXml.DocumentElement.SelectSingleNode("LCODE_1").InnerText = txtLcode.Text
                objInputXml.DocumentElement.SelectSingleNode("CHAINCODE").InnerText = txtChainCode.Text

                Dim objEmpXml As New XmlDocument
                objEmpXml.LoadXml(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                If (ChkGrpProductivity.Checked) Then
                    objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "1"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "0"
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
                    ViewState("SortName") = "AGENCYNAME"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AGENCYNAME" '"LOCATION_CODE"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
                End If
                'If ViewState("SortNameMISC") Is Nothing Then
                '    ViewState("SortNameMISC") = "AGENCYNAME"
                '    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AGENCYNAME" '"LOCATION_CODE"
                '    'ViewState("SortName") = ViewState("SortNameMISC")
                'Else
                '    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortNameMISC")
                '    'ViewState("SortName") = ViewState("SortNameMISC")
                'End If

                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
                End If
                If hdDefaultMisc.Value = "2" Then
                    hdDefaultMisc.Value = ""
                    ViewState("SortName") = "AGENCYNAME"
                    ViewState("Desc") = "FALSE"
                End If
                objOutputXml = objbzInstallation.PCInstallationReport(objInputXml)
                objOutputXml1 = Nothing
            End If
            If hdTabType.Value = 2 Then

                If drpOrderType.SelectedIndex > 0 Then
                    naviGate.Style.Add("display", "none")
                    lblError.Text = "OrderType wise searching is not applicable on MISC Installations. please click search again"

                    hdOrderTypeSelectedIndex.Value = Val(drpOrderType.SelectedIndex & "")

                    drpOrderType.SelectedIndex = 0
                    drpOrderType.Enabled = False
                    Exit Sub
                End If

                hdDefaultMisc.Value = 2
                ViewState("PrevSearching") = Nothing

                'If (Request.Form("txtAgencyName") <> "" And hidLcode.Value = "") Or (Request.Form("txtAgencyName") = "" Or hidLcode.Value = "") Then
                '    objInputXml1.DocumentElement.SelectSingleNode("LCODE").InnerText = ""
                '    objInputXml1.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text.Trim()
                'Else
                '    objInputXml1.DocumentElement.SelectSingleNode("LCODE").InnerText = hidLcode.Value
                '    objInputXml1.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = ""
                'End If

                If DlstCompVertical.SelectedValue <> "" Then
                    objInputXml1.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
                End If


                If (Request.Form("txtAgencyName") <> "" And hdAgencyName.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyName.Value = "") Then
                    objInputXml1.DocumentElement.SelectSingleNode("LCODE").InnerText = ""
                Else
                    objInputXml1.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyName.Value.Trim()
                End If
                objInputXml1.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text

                If (drpCity1.SelectedIndex <> 0) Then
                    objInputXml1.DocumentElement.SelectSingleNode("CITY").InnerText = drpCity1.SelectedItem.Text
                End If
                If (drpCountry1.SelectedIndex <> 0) Then
                    objInputXml1.DocumentElement.SelectSingleNode("COUNTRY").InnerText = drpCountry1.SelectedItem.Text
                End If
                If (drpAoffice1.SelectedIndex <> 0) Then
                    objInputXml1.DocumentElement.SelectSingleNode("AOFFICE").InnerText = drpAoffice1.SelectedItem.Text
                End If
                If (ddlRegion.SelectedIndex <> 0) Then
                    objInputXml1.DocumentElement.SelectSingleNode("REGION").InnerText = ddlRegion.SelectedItem.Text
                End If
                If (drpEquipmentGroup.SelectedIndex <> 0) Then
                    objInputXml1.DocumentElement.SelectSingleNode("EQUIPMENTGROUP").InnerText = drpEquipmentGroup.SelectedItem.Text
                End If

                objInputXml1.DocumentElement.SelectSingleNode("RESP1A").InnerText = txtAResponsibility.Text
                If drpOrderType.SelectedIndex <> 0 Then
                    objInputXml1.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText = drpOrderType.SelectedValue ' drpOrderType.SelectedItem.Text
                End If

                objInputXml1.DocumentElement.SelectSingleNode("EQUIPMENTTYPE").InnerText = Trim(txtEquipmentType.Text) & ""
                objInputXml1.DocumentElement.SelectSingleNode("DATEFROM").InnerText = objeAAMS.ConvertTextDate(txtInstallationFrom.Text)
                objInputXml1.DocumentElement.SelectSingleNode("DATETO").InnerText = objeAAMS.ConvertTextDate(txtInstallationTo.Text)
                objInputXml1.DocumentElement.SelectSingleNode("FPURDATE").InnerText = objeAAMS.ConvertTextDate(txtPurchaseFrom.Text)
                objInputXml1.DocumentElement.SelectSingleNode("TPURDATE").InnerText = objeAAMS.ConvertTextDate(txtPurchaseTo.Text)
                objInputXml1.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = txtVendorSerialNo.Text
                objInputXml1.DocumentElement.SelectSingleNode("LCODE_1").InnerText = txtLcode.Text
                objInputXml1.DocumentElement.SelectSingleNode("CHAINCODE").InnerText = txtChainCode.Text

                Dim objEmpXml1 As New XmlDocument
                objEmpXml1.LoadXml(Session("Security"))
                objInputXml1.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = objEmpXml1.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                If (ChkGrpProductivity.Checked) Then
                    objInputXml1.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "1"
                Else
                    objInputXml1.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "0"
                End If
                If ViewState("PrevSearchingMisc") Is Nothing Then
                    ddlPageNumber.Items.Clear()
                    objInputXml1.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                Else
                    Dim objTempInputXml1 As New XmlDocument
                    Dim objNodeList1 As XmlNodeList

                    objTempInputXml1.LoadXml(ViewState("PrevSearchingMisc"))
                    objNodeList1 = objTempInputXml1.DocumentElement.ChildNodes
                    objInputXml1.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                    For Each objNode As XmlNode In objNodeList1
                        If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                            If objNode.InnerText <> objInputXml1.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                                objInputXml1.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                                ddlPageNumber.SelectedValue = "1"
                            End If
                        End If
                    Next
                End If
                objInputXml1.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString

                If ViewState("SortNameMISC") Is Nothing Then
                    ViewState("SortNameMISC") = "AGENCYNAME"
                    objInputXml1.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AGENCYNAME" '"LOCATION_CODE"
                    ViewState("SortName") = ViewState("SortNameMISC")
                Else
                    objInputXml1.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortNameMISC")
                    ViewState("SortName") = ViewState("SortNameMISC")
                End If

                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                    objInputXml1.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                Else
                    objInputXml1.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
                End If
                If hdDefaultPC.Value = "1" Then
                    hdDefaultPC.Value = ""
                    ViewState("SortName") = "AGENCYNAME"
                    ViewState("Desc") = "FALSE"
                End If
                objOutputXml1 = objbzMiscInstallation.MISCHardwareReport(objInputXml1)
                objOutputXml = Nothing

            End If
            'If hdTabType.Value = 0 Or hdTabType.Value = 1 Then
            '    objOutputXml = objbzInstallation.PCInstallationReport(objInputXml)
            '    objOutputXml1 = Nothing
            'End If

            'If hdTabType.Value = 2 Then
            '    objOutputXml1 = objbzMiscInstallation.MISCHardwareReport(objInputXml1)
            '    objOutputXml = Nothing
            'End If

            ' If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            'If Not objOutputXml Is Nothing Then
            '    objXmlReader = New XmlNodeReader(objOutputXml)
            '    ds.ReadXml(objXmlReader)
            'End If
            'If Not objOutputXml1 Is Nothing Then
            '    objXmlReader1 = New XmlNodeReader(objOutputXml1)
            '    ds1.ReadXml(objXmlReader1)
            'End If
            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                If Not objOutputXml Is Nothing Then
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        ViewState("PrevSearching") = objInputXml.OuterXml
                        objXmlReader = New XmlNodeReader(objOutputXml)
                        ds.ReadXml(objXmlReader)
                        gvPCInstallation.DataSource = ds.Tables("PCINSTALL")
                        gvPCInstallation.DataBind()
                        pnlPaging.Visible = True
                    Else
                        pnlPaging.Visible = False
                        txtTotalRecordCount.Text = "0"
                        gvPCInstallation.DataSource = String.Empty
                        gvPCInstallation.DataBind()
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                        Exit Sub
                    End If

                End If
            End If

            If hdTabType.Value = 2 Then
                If Not objOutputXml1 Is Nothing Then
                    If objOutputXml1.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        ViewState("PrevSearchingMisc") = objInputXml1.OuterXml
                        objXmlReader1 = New XmlNodeReader(objOutputXml1)
                        ds1.ReadXml(objXmlReader1)
                        miscInstallation.DataSource = ds1.Tables("MISCHARDWARE")
                        miscInstallation.DataBind()
                        pnlPaging.Visible = True
                    Else
                        pnlPaging.Visible = False
                        txtTotalRecordCount.Text = "0"
                        miscInstallation.DataSource = String.Empty
                        miscInstallation.DataBind()
                        lblError.Text = objOutputXml1.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                        Exit Sub
                    End If
                End If
            End If

            Dim count As Integer
            If Not objOutputXml Is Nothing Then
                count = CInt(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
            End If
            If Not objOutputXml1 Is Nothing Then
                count = CInt(objOutputXml1.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
            End If
            Dim selectedValue As String

            selectedValue = IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue)


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

            '  txtRecordOnCurrentPage.Text = ds.Tables("MIDT").Rows.Count.ToString
            If Not objOutputXml Is Nothing Then
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
            End If
            If Not objOutputXml1 Is Nothing Then
                txtTotalRecordCount.Text = objOutputXml1.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
            End If
            ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            ' @ Added Code To Show Image'
            Dim imgUp, imgUp1 As New Image
            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgUp1.ImageUrl = "~/Images/Sortup.gif"
            Dim imgDown, imgDown1 As New Image
            imgDown.ImageUrl = "~/Images/Sortdown.gif"
            imgDown1.ImageUrl = "~/Images/Sortdown.gif"

            'If gvPCInstallation.Rows.Count <> 0 Then
            Select Case ViewState("SortName").ToString()

                Case "LCODE"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                                gvPCInstallation.HeaderRow.Cells(0).Controls.Add(imgUp)
                            Else
                                miscInstallation.HeaderRow.Cells(0).Controls.Add(imgUp1)
                            End If
                        Case "TRUE"
                            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                                gvPCInstallation.HeaderRow.Cells(0).Controls.Add(imgDown1)
                            Else
                                miscInstallation.HeaderRow.Cells(0).Controls.Add(imgDown)
                            End If
                    End Select
                Case "CHAIN_CODE"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                                gvPCInstallation.HeaderRow.Cells(1).Controls.Add(imgUp)
                            Else
                                miscInstallation.HeaderRow.Cells(1).Controls.Add(imgUp1)
                            End If
                        Case "TRUE"
                            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                                gvPCInstallation.HeaderRow.Cells(1).Controls.Add(imgDown1)
                            Else
                                miscInstallation.HeaderRow.Cells(1).Controls.Add(imgDown)
                            End If
                    End Select
                Case "AGENCYNAME"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                                gvPCInstallation.HeaderRow.Cells(2).Controls.Add(imgUp)
                            Else
                                miscInstallation.HeaderRow.Cells(2).Controls.Add(imgUp1)
                            End If
                        Case "TRUE"
                            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                                gvPCInstallation.HeaderRow.Cells(2).Controls.Add(imgDown1)
                            Else
                                miscInstallation.HeaderRow.Cells(2).Controls.Add(imgDown)
                            End If
                    End Select
                Case "OFFICEID"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                                gvPCInstallation.HeaderRow.Cells(3).Controls.Add(imgUp)
                            Else
                                miscInstallation.HeaderRow.Cells(3).Controls.Add(imgUp1)
                            End If
                        Case "TRUE"
                            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                                gvPCInstallation.HeaderRow.Cells(3).Controls.Add(imgDown)
                            Else
                                miscInstallation.HeaderRow.Cells(3).Controls.Add(imgDown1)
                            End If
                    End Select

                Case "COMP_VERTICAL"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                                gvPCInstallation.HeaderRow.Cells(4).Controls.Add(imgUp)
                            Else
                                miscInstallation.HeaderRow.Cells(4).Controls.Add(imgUp1)
                            End If
                        Case "TRUE"
                            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                                gvPCInstallation.HeaderRow.Cells(4).Controls.Add(imgDown)
                            Else
                                miscInstallation.HeaderRow.Cells(4).Controls.Add(imgDown1)
                            End If
                    End Select
                Case "CITY"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                                gvPCInstallation.HeaderRow.Cells(5).Controls.Add(imgUp)
                            Else
                                miscInstallation.HeaderRow.Cells(5).Controls.Add(imgUp1)
                            End If
                        Case "TRUE"
                            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                                gvPCInstallation.HeaderRow.Cells(5).Controls.Add(imgDown)
                            Else
                                miscInstallation.HeaderRow.Cells(5).Controls.Add(imgDown1)
                            End If
                    End Select
                Case "COUNTRY"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                                gvPCInstallation.HeaderRow.Cells(6).Controls.Add(imgUp)
                            Else
                                miscInstallation.HeaderRow.Cells(6).Controls.Add(imgUp1)
                            End If
                        Case "TRUE"
                            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                                gvPCInstallation.HeaderRow.Cells(6).Controls.Add(imgDown)
                            Else
                                miscInstallation.HeaderRow.Cells(6).Controls.Add(imgDown1)
                            End If

                    End Select
                Case "ONLINE_STATUS"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                                gvPCInstallation.HeaderRow.Cells(7).Controls.Add(imgUp)
                            Else
                                miscInstallation.HeaderRow.Cells(11).Controls.Add(imgUp1)
                            End If
                        Case "TRUE"
                            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                                gvPCInstallation.HeaderRow.Cells(7).Controls.Add(imgDown)
                            Else
                                miscInstallation.HeaderRow.Cells(11).Controls.Add(imgDown1)
                            End If


                    End Select
                Case "CPUTYPE"
                    Select Case ViewState("Desc").ToString()

                        Case "FALSE"
                            gvPCInstallation.HeaderRow.Cells(9).Controls.Add(imgUp)
                        Case "TRUE"
                            gvPCInstallation.HeaderRow.Cells(9).Controls.Add(imgDown)

                    End Select
                Case "DATEINSTALLED"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                                gvPCInstallation.HeaderRow.Cells(8).Controls.Add(imgUp)
                            Else
                                miscInstallation.HeaderRow.Cells(7).Controls.Add(imgUp1)
                            End If
                        Case "TRUE"
                            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                                gvPCInstallation.HeaderRow.Cells(8).Controls.Add(imgDown)
                            Else
                                miscInstallation.HeaderRow.Cells(7).Controls.Add(imgDown1)
                            End If
                    End Select
                Case "CPUNO"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            gvPCInstallation.HeaderRow.Cells(10).Controls.Add(imgUp)
                        Case "TRUE"
                            gvPCInstallation.HeaderRow.Cells(10).Controls.Add(imgDown)
                    End Select
                Case "MONTYPE"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            gvPCInstallation.HeaderRow.Cells(11).Controls.Add(imgUp)
                        Case "TRUE"
                            gvPCInstallation.HeaderRow.Cells(11).Controls.Add(imgDown)
                    End Select

                Case "MONNO"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            gvPCInstallation.HeaderRow.Cells(12).Controls.Add(imgUp)
                        Case "TRUE"
                            gvPCInstallation.HeaderRow.Cells(12).Controls.Add(imgDown)
                    End Select

                Case "KBDTYPE"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            gvPCInstallation.HeaderRow.Cells(13).Controls.Add(imgUp)
                        Case "TRUE"
                            gvPCInstallation.HeaderRow.Cells(13).Controls.Add(imgDown)
                    End Select
                Case "KBDNO"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            gvPCInstallation.HeaderRow.Cells(14).Controls.Add(imgUp)
                        Case "TRUE"
                            gvPCInstallation.HeaderRow.Cells(14).Controls.Add(imgDown)
                    End Select
                Case "MSETYPE"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            gvPCInstallation.HeaderRow.Cells(15).Controls.Add(imgUp)
                        Case "TRUE"
                            gvPCInstallation.HeaderRow.Cells(15).Controls.Add(imgDown)
                    End Select
                Case "MSENO"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            gvPCInstallation.HeaderRow.Cells(16).Controls.Add(imgUp)
                        Case "TRUE"
                            gvPCInstallation.HeaderRow.Cells(16).Controls.Add(imgDown)
                    End Select
                Case "CDRNO"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            gvPCInstallation.HeaderRow.Cells(17).Controls.Add(imgUp)
                        Case "TRUE"
                            gvPCInstallation.HeaderRow.Cells(17).Controls.Add(imgDown)
                    End Select

                Case "INVOICENO"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            gvPCInstallation.HeaderRow.Cells(18).Controls.Add(imgUp)
                        Case "TRUE"
                            gvPCInstallation.HeaderRow.Cells(18).Controls.Add(imgDown)
                    End Select
                Case "WARRANTY"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                                gvPCInstallation.HeaderRow.Cells(19).Controls.Add(imgUp)
                            Else
                                miscInstallation.HeaderRow.Cells(13).Controls.Add(imgUp1)
                            End If
                        Case "TRUE"
                            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                                gvPCInstallation.HeaderRow.Cells(19).Controls.Add(imgDown)
                            Else
                                miscInstallation.HeaderRow.Cells(13).Controls.Add(imgDown1)
                            End If
                    End Select
                Case "PURCHASEDATE"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                                gvPCInstallation.HeaderRow.Cells(20).Controls.Add(imgUp)
                            Else
                                miscInstallation.HeaderRow.Cells(14).Controls.Add(imgUp1)
                            End If
                        Case "TRUE"
                            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                                gvPCInstallation.HeaderRow.Cells(20).Controls.Add(imgDown)
                            Else
                                miscInstallation.HeaderRow.Cells(14).Controls.Add(imgDown1)
                            End If
                    End Select

                    'Case "PURCHASEDATE"
                    '    Select Case ViewState("Desc")
                    '        Case "FALSE"
                    '            gvPCInstallation.HeaderRow.Cells(17).Controls.Add(imgUp)
                    '        Case "TRUE"
                    '            gvPCInstallation.HeaderRow.Cells(17).Controls.Add(imgDown)
                    '    End Select
                    'Case "PURCHASEDATE"
                    '    Select Case ViewState("Desc")
                    '        Case "FALSE"
                    '            miscInstallation.HeaderRow.Cells(11).Controls.Add(imgUp)
                    '        Case "TRUE"
                    '            miscInstallation.HeaderRow.Cells(11).Controls.Add(imgDown)
                    '    End Select

                Case "SUPPLIERNAME"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            gvPCInstallation.HeaderRow.Cells(21).Controls.Add(imgUp)
                        Case "TRUE"
                            gvPCInstallation.HeaderRow.Cells(21).Controls.Add(imgDown)
                    End Select




                Case "OrderType"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            gvPCInstallation.HeaderRow.Cells(22).Controls.Add(imgUp)
                        Case "TRUE"
                            gvPCInstallation.HeaderRow.Cells(22).Controls.Add(imgDown)
                    End Select


                Case "RESPNAME"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            gvPCInstallation.HeaderRow.Cells(23).Controls.Add(imgUp)
                        Case "TRUE"
                            gvPCInstallation.HeaderRow.Cells(23).Controls.Add(imgDown)
                    End Select



                Case "EQUIPMENTTYPE"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            miscInstallation.HeaderRow.Cells(8).Controls.Add(imgUp)
                        Case "TRUE"
                            miscInstallation.HeaderRow.Cells(8).Controls.Add(imgDown)

                    End Select
                Case "EQUIPMENTNO"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            miscInstallation.HeaderRow.Cells(9).Controls.Add(imgUp)
                        Case "TRUE"
                            miscInstallation.HeaderRow.Cells(9).Controls.Add(imgDown)
                    End Select
                Case "QTY"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            miscInstallation.HeaderRow.Cells(10).Controls.Add(imgUp)
                        Case "TRUE"
                            miscInstallation.HeaderRow.Cells(10).Controls.Add(imgDown)
                    End Select



                Case "CHALLANNUMBER"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                                gvPCInstallation.HeaderRow.Cells(18).Controls.Add(imgUp)
                            Else
                                miscInstallation.HeaderRow.Cells(12).Controls.Add(imgUp1)
                            End If
                        Case "TRUE"
                            If hdTabType.Value = 1 Or hdTabType.Value = 0 Then
                                gvPCInstallation.HeaderRow.Cells(18).Controls.Add(imgDown)
                            Else
                                miscInstallation.HeaderRow.Cells(12).Controls.Add(imgDown1)
                            End If
                    End Select


                    'Case "INVOICENO"
                    '    Select Case ViewState("Desc")
                    '        Case "FALSE"
                    '            gvPCInstallation.HeaderRow.Cells(15).Controls.Add(imgUp)
                    '        Case "TRUE"
                    '            gvPCInstallation.HeaderRow.Cells(15).Controls.Add(imgDown)
                    '    End Select
                Case "VENDORNAME"
                    Select Case ViewState("Desc").ToString()
                        Case "FALSE"
                            miscInstallation.HeaderRow.Cells(15).Controls.Add(imgUp)
                        Case "TRUE"
                            miscInstallation.HeaderRow.Cells(15).Controls.Add(imgDown)
                    End Select


            End Select
            ' End If

            '' @ Added Code To Show Image'
            ''@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@





            '*************************************
            'Else
            'gvPCInstallation.DataSource = String.Empty
            'gvPCInstallation.DataBind()
            'pnlPaging.Visible = False
            'txtTotalRecordCount.Text = "0"
            'lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            'End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub


    Protected Sub theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound
        Try
            Dim Button1 As Button
            Button1 = e.Item.FindControl("Button1")
            If e.Item.ItemIndex = 0 Then
                Button1.CssClass = "headingtab"
            End If
            Button1.Attributes.Add("onclick", "return ColorMethod('" & Button1.ClientID.ToString() & "',2);")

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Protected Sub btnDisplay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisplay.Click
        hdExport.Value = ""
        Dim objInputXml, objOutputXml, objOutputXml1 As New XmlDocument
        ' Dim objParentNode As XmlNode
        ' Dim objXmlReader, objXmlReader1 As XmlNodeReader
        Dim ds, ds1 As New DataSet
        Dim objbzINV As New AAMS.bizMaster.bzEquipment
        Dim objbzInstallation As New AAMS.bizInventory.bzChallan
        Dim objbzMiscInstallation As New AAMS.bizInventory.bzChallan
        Try
            lblError.Text = ""
            objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE /><AGENCYNAME /><CITY /><COUNTRY /><AOFFICE /><REGION /><WHOLEGROUP /><ONLINESTATUS /><EQUIPMENTGROUP /><EQUIPMENTTYPE /><DATEFROM /><DATETO /><RESPONSIBLESTAFFID /><VENDERSERIALNO /><SECREGIONID /><FPURDATE /><TPURDATE /><VENDORSRNOFLTRTYPE /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><RESP1A/><ORDERTYPEID/><COMP_VERTICAL></COMP_VERTICAL></INV_RPTPCINSTALL_INPUT>")
            'objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hidLcode.Value
            'objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text

            If DlstCompVertical.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
            End If

            If (Request.Form("txtAgencyName") <> "" And hdAgencyName.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyName.Value = "") Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = ""
            Else
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyName.Value.Trim()
            End If

            objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text

            If (drpCity1.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = drpCity1.SelectedItem.Text
            End If
            If (drpCountry1.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("COUNTRY").InnerText = drpCountry1.SelectedItem.Text
            End If
            If (drpAoffice1.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = drpAoffice1.SelectedItem.Text
            End If
            If (ddlRegion.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = ddlRegion.SelectedItem.Text
            End If

            objInputXml.DocumentElement.SelectSingleNode("RESP1A").InnerText = txtAResponsibility.Text
            If drpOrderType.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText = drpOrderType.SelectedValue 'drpOrderType.SelectedItem.Text
            End If


            If (drpEquipmentGroup.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("EQUIPMENTGROUP").InnerText = drpEquipmentGroup.SelectedItem.Text
            End If

            objInputXml.DocumentElement.SelectSingleNode("EQUIPMENTTYPE").InnerText = Trim(txtEquipmentType.Text) & ""
            objInputXml.DocumentElement.SelectSingleNode("DATEFROM").InnerText = objeAAMS.ConvertTextDate(txtInstallationFrom.Text)
            objInputXml.DocumentElement.SelectSingleNode("DATETO").InnerText = objeAAMS.ConvertTextDate(txtInstallationTo.Text)
            objInputXml.DocumentElement.SelectSingleNode("FPURDATE").InnerText = objeAAMS.ConvertTextDate(txtPurchaseFrom.Text)
            objInputXml.DocumentElement.SelectSingleNode("TPURDATE").InnerText = objeAAMS.ConvertTextDate(txtPurchaseTo.Text)
            objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = txtVendorSerialNo.Text
            Dim objEmpXml As New XmlDocument
            objEmpXml.LoadXml(Session("Security"))
            objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
            If (ChkGrpProductivity.Checked) Then
                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "1"
            Else
                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "0"
            End If
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
            Session("Installation") = objInputXml.OuterXml
            ' Session("HardwareInstallation") = objInputXml.OuterXml
            Response.Redirect("../RPSR_ReportShow.aspx?Case=HardwareInstallation", False)
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            HardwareInstallationSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            HardwareInstallationSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            HardwareInstallationSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvPCInstallation_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvPCInstallation.Sorting
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
            ViewState("SortNameMISC") = Nothing
            HardwareInstallationSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        hdExport.Value = ""
        gvPCInstallation.AllowSorting = False
        gvPCInstallation.HeaderStyle.ForeColor = Drawing.Color.Black
        miscInstallation.AllowSorting = False
        miscInstallation.HeaderStyle.ForeColor = Drawing.Color.Black
        HardwareInstallationExport()
    End Sub
    Private Sub HardwareInstallationExport()

        Dim objInputXml, objOutputXml, objOutputXml1 As New XmlDocument
        ' Dim objParentNode As XmlNode
        '        Dim objXmlReader, objXmlReader1 As XmlNodeReader
        Dim ds, ds1 As New DataSet
        Dim objbzINV As New AAMS.bizMaster.bzEquipment
        Dim objbzInstallation As New AAMS.bizInventory.bzChallan
        Dim objbzMiscInstallation As New AAMS.bizInventory.bzChallan
        Dim objExport As New ExportExcel
        Try
            lblError.Text = ""
            If hdTabType.Value = "0" Then
            Else
            End If

            'objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE /><AGENCYNAME /><CITY /><COUNTRY /><AOFFICE /><REGION /><WHOLEGROUP /><ONLINESTATUS /><EQUIPMENTGROUP /><EQUIPMENTTYPE /><DATEFROM /><DATETO /><RESPONSIBLESTAFFID /><VENDERSERIALNO /><SECREGIONID /><FPURDATE /><TPURDATE /><VENDORSRNOFLTRTYPE /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><RESP1A/><ORDERTYPEID/></INV_RPTPCINSTALL_INPUT>")
            objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE /><AGENCYNAME /><CITY /><COUNTRY /><AOFFICE /><REGION /><WHOLEGROUP /><ONLINESTATUS /><EQUIPMENTGROUP /><EQUIPMENTTYPE /><DATEFROM /><DATETO /><RESPONSIBLESTAFFID /><VENDERSERIALNO /><SECREGIONID /><FPURDATE /><TPURDATE /><VENDORSRNOFLTRTYPE /><RESP1A/><ORDERTYPEID/><LCODE_1/><CHAINCODE/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><COMP_VERTICAL></COMP_VERTICAL></INV_RPTPCINSTALL_INPUT>")

            'objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hidLcode.Value
            'objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text
            'If (Request.Form("txtAgencyName") <> "" And hidLcode.Value = "") Or (Request.Form("txtAgencyName") = "" Or hidLcode.Value = "") Then
            '    objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = ""
            '    objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text.Trim()

            'Else
            '    objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hidLcode.Value
            '    objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = ""
            'End If
            If DlstCompVertical.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
            End If


            If (Request.Form("txtAgencyName") <> "" And hdAgencyName.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyName.Value = "") Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = ""
            Else
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyName.Value.Trim()
            End If

            objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text

            If (drpCity1.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = drpCity1.SelectedItem.Text
            End If
            If (drpCountry1.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("COUNTRY").InnerText = drpCountry1.SelectedItem.Text
            End If
            If (drpAoffice1.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = drpAoffice1.SelectedItem.Text
            End If
            If (ddlRegion.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = ddlRegion.SelectedItem.Text
            End If

            objInputXml.DocumentElement.SelectSingleNode("RESP1A").InnerText = txtAResponsibility.Text
            If drpOrderType.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText = drpOrderType.SelectedValue ' drpOrderType.SelectedItem.Text
            End If

            If (drpEquipmentGroup.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("EQUIPMENTGROUP").InnerText = drpEquipmentGroup.SelectedItem.Text
            End If

            objInputXml.DocumentElement.SelectSingleNode("EQUIPMENTTYPE").InnerText = Trim(txtEquipmentType.Text) & ""
            objInputXml.DocumentElement.SelectSingleNode("DATEFROM").InnerText = objeAAMS.ConvertTextDate(txtInstallationFrom.Text)
            objInputXml.DocumentElement.SelectSingleNode("DATETO").InnerText = objeAAMS.ConvertTextDate(txtInstallationTo.Text)
            objInputXml.DocumentElement.SelectSingleNode("FPURDATE").InnerText = objeAAMS.ConvertTextDate(txtPurchaseFrom.Text)
            objInputXml.DocumentElement.SelectSingleNode("TPURDATE").InnerText = objeAAMS.ConvertTextDate(txtPurchaseTo.Text)
            objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = txtVendorSerialNo.Text
            objInputXml.DocumentElement.SelectSingleNode("LCODE_1").InnerText = txtLcode.Text
            objInputXml.DocumentElement.SelectSingleNode("CHAINCODE").InnerText = txtChainCode.Text

            Dim objEmpXml As New XmlDocument
            objEmpXml.LoadXml(Session("Security"))
            objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
            If (ChkGrpProductivity.Checked) Then
                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "1"
            Else
                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "0"
            End If
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
            If strIndex = 0 Or strIndex = 1 Then
                objOutputXml = objbzInstallation.PCInstallationReport(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    '<PCINSTALL 
                    'LCODE = "18827"                                    0
                    'AGENCYNAME = "Deepika Travels (Bhirmingham House)" 1
                    'OFFICEID = "DELVS388M"                             2
                    'COMP_VERTICAL = "ResBird"                          3
                    'CITY = "New Delhi"                                 4
                    'COUNTRY = "India"                                  5
                    'DATEINSTALLED = "15-Nov-2005"                      6
                    'CPUTYPE = "CPP"                                    7
                    'CPUNO = "NA"                                       8
                    'MONNO = "NA"                                       9
                    'MONTYPE = "MMP"                                    10
                    'KBDTYPE = "KBP"                                    11
                    'KBDNO = "NA"                                       12
                    'MSETYPE = "MSP"                                    13
                    'MSENO = "NA"                                       14
                    'CDRNO = "NA"   `                                   15
                    'CHALLANNUMBER = ""                                 16
                    'WARRANTY = ""                                      17
                    'PURCHASEDATE = ""                                  18
                    'VENDORNAME = ""                                    19
                    'ONLINE_STATUS = "VISTA"                            20
                    'SUPPLIERNAME = ""                                  21
                    'INVOICENO = ""                                     22
                    'INVOICEDATE = ""                                   23
                    'CHAIN_CODE = "3"                                   24
                    'OrderType = "VISTA OWN H/W"                        25
                    'RESPNAME = "Abhishek Dhillon"                      26
                    '/> 


                    'Dim strArray() As String = {"Location Code", "Agency Name", "Office ID", "City", "Country", "Date", "CPU Type", "CPU No", "Monitor Numer", "Monitor type", "KeyBoard Type", "KeyBoard Number", "Mouse Type", "Mouse Number", "CDR Number", "Challan Number", "Warranty", "Date", "Vendor Name", "Online Status", "Supllier Name", "Invoice Number", "Invoice date"}
                    'Dim intArray() As Integer = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22}
                    'Dim strArray() As String = {"Location Code", "Chain Code", "Agency Name", "Office ID", "City", "Country", "Date", "CPU Type", "CPU No", "Monitor Numer", "Monitor type", "KeyBoard Type", "KeyBoard Number", "Mouse Type", "Mouse Number", "CDR Number", "Challan Number", "Warranty", "Date", "Vendor Name", "Online Status", "Supllier Name", "Invoice Number", "Invoice date", "Order Type", "1A Responsible"}
                    'Dim intArray() As Integer = {0, 23, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 24, 25}

                    Dim strArray() As String = {"Location Code", "Chain Code", "Agency Name", "Office ID", "Company Vertical", "City", "Country", "Date", "CPU Type", "CPU No", "Monitor Number", "Monitor type", "KeyBoard Type", "KeyBoard Number", "Mouse Type", "Mouse Number", "CDR Number", "Challan Number", "Warranty", "Date", "Vendor Name", "Online Status", "Supllier Name", "Invoice Number", "Invoice date", "Order Type", "1A Responsible"}
                    Dim intArray() As Integer = {0, 24, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 25, 26}


                    objExport.ExportDetails(objOutputXml, "PCINSTALL", intArray, strArray, ExportExcel.ExportFormat.Excel, "PCInstallation.xls")

                Else
                    gvPCInstallation.DataSource = String.Empty
                    gvPCInstallation.DataBind()
                    pnlPaging.Visible = False
                    txtTotalRecordCount.Text = "0"
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If

            Else
                objOutputXml1 = objbzMiscInstallation.MISCHardwareReport(objInputXml)

                If objOutputXml1.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    '<MISCHARDWARE 
                    'LCODE = "537"                          0
                    'AGENCYNAME = "Mazda Travels Pvt Ltd"   1
                    'OFFICEID = "BOMWI2123"                 2
                    'COMP_VERTICAL = "ResBird"              3
                    'CITY = "Mumbai"                        4
                    'DATEINSTALLED = "06-Dec-1998"          5
                    'EQUIPMENTTYPE = "LX8"                  6
                    'EQUIPMENTNO = "LX1C195356"             7
                    'ONLINE_STATUS = "BTWMAX"               8
                    'QTY = "1"                              9
                    'SUPPLIERNAME = ""                      10
                    'INVOICENO = ""                         11
                    'INVOICEDATE = ""                       12
                    'CHALLANNUMBER = "BOM/5/2010/00752"     13
                    'WARRANTY = ""                          14
                    'PURCHASEDATE = "21-Apr-2010"           15
                    'VENDORNAME = "amadeus old vendor"      16
                    'COUNTRY = "India"                      17
                    'CHAIN_CODE = "951"                     18
                    'OrderType = ""                         19
                    'RESPNAME="Nikita Patil"                20
                    ' /> 


                    'Dim strArray1() As String = {"Location Code", "Agency Name", "Office Id", "Country", "City", "Date", "Equipment Type", "Equipment No", "Online Status", "Quantity", "SupplierName", "Invoice numeber", "Invoice Date", "Challan number", "Warranty", "Purchase Date", "Vendor Name"}
                    'Dim intArray1() As Integer = {0, 1, 2, 16, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}
                    'Dim strArray1() As String = {"Location Code", "Chain Code", "Agency Name", "Office Id", "Country", "City", "Date", "Equipment Type", "Equipment No", "Online Status", "Quantity", "SupplierName", "Invoice numeber", "Invoice Date", "Challan number", "Warranty", "Purchase Date", "Vendor Name"}
                    'Dim intArray1() As Integer = {0, 17, 1, 2, 16, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}
                    Dim strArray1() As String = {"Location Code", "Chain Code", "Agency Name", "Office Id", "Company Vertical", "Country", "City", "Date", "Equipment Type", "Equipment No", "Online Status", "Quantity", "SupplierName", "Invoice numeber", "Invoice Date", "Challan number", "Warranty", "Purchase Date", "Vendor Name"}
                    Dim intArray1() As Integer = {0, 18, 1, 2, 3, 17, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16}

                    objExport.ExportDetails(objOutputXml1, "MISCHARDWARE", intArray1, strArray1, ExportExcel.ExportFormat.Excel, "MISCInstallation.xls")

                Else
                    miscInstallation.DataSource = String.Empty
                    miscInstallation.DataBind()
                    pnlPaging.Visible = False
                    txtTotalRecordCount.Text = "0"
                    lblError.Text = objOutputXml1.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If

            End If


            'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '    objXmlReader = New XmlNodeReader(objOutputXml)
            '    objXmlReader1 = New XmlNodeReader(objOutputXml1)
            '    ds.ReadXml(objXmlReader)
            '    gvPCInstallation.DataSource = ds.Tables("PCINSTALL")
            '    gvPCInstallation.DataBind()



            '    If strIndex = 0 Or strIndex = 1 Then
            '        Dim strArray() As String = {"Location Code", "Agency Name", "Office ID", "City", "Country", "Date", "CPU Type", "CPU No", "Monitor Numer", "Monitor type", "KeyBoard Type", "KeyBoard Number", "Mouse Type", "Mouse Number", "CDR Number", "Challan Number", "Warranty", "Date", "Vendor Name", "Online Status", "Supllier Name", "Invoice Number", "Invoice date"}
            '        Dim intArray() As Integer = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22}
            '        objExport.ExportDetails(objOutputXml, "PCINSTALL", intArray, strArray, ExportExcel.ExportFormat.Excel, "PCInstallation.xls")
            '    Else
            '        Dim strArray1() As String = {"Location Code", "Agency Name", "Office Id", "Country", "City", "Date", "Equipment Type", "Equipment No", "Online Status", "Quantity", "SupplierName", "Invoice numeber", "Invoice Date", "Challan number", "Warranty", "Purchase Date", "Vendor Name"}
            '        Dim intArray1() As Integer = {0, 1, 2, 16, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}
            '        objExport.ExportDetails(objOutputXml1, "MISCHARDWARE", intArray1, strArray1, ExportExcel.ExportFormat.Excel, "MISCInstallation.xls")
            '    End If
            'Else
            '    gvPCInstallation.DataSource = String.Empty
            '    gvPCInstallation.DataBind()
            '    miscInstallation.DataSource = String.Empty
            '    miscInstallation.DataBind()
            '    pnlPaging.Visible = False
            '    txtTotalRecordCount.Text = "0"
            '    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            'End If
            'Else

            'End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub



    Protected Sub miscInstallation_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles miscInstallation.Sorting
        Try
            Dim SortName As String = e.SortExpression
            If ViewState("SortNameMISC") Is Nothing Then
                ViewState("SortNameMISC") = SortName
                ViewState("Desc") = "FALSE"
            Else
                If ViewState("SortNameMISC") = SortName Then
                    If ViewState("Desc") = "TRUE" Then
                        ViewState("Desc") = "FALSE"
                    Else
                        ViewState("Desc") = "TRUE"
                    End If
                Else
                    ViewState("SortNameMISC") = SortName
                    ViewState("Desc") = "FALSE"
                End If
            End If
            HardwareInstallationSearch()
            ViewState("SortName") = Nothing
            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("INVSR_HardwareInstallation.aspx", False)
    End Sub
End Class
