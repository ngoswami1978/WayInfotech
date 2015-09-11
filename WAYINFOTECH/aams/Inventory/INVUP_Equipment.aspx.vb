Imports System.Data
Imports System.Xml
Partial Class Inventory_INVUP_Equipment
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim strBuilderCatg As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = Request.Url.ToString() '"Inventory/INVUP_Equipment.aspx"
            btn_New.Attributes.Add("onclick", "return NewFunction();")
            btnSave.Attributes.Add("onclick", "return ValidateEquipment();")
            If Request.QueryString("Action").ToUpper() = "U" Or Request.QueryString("Action").ToUpper() = "US" Then
                txtEquipmentType.Attributes.Add("readonly", "readonly")
                txtEquipmentType.CssClass = "textboxgrey"
                txtEquipmentType.Focus()
            End If
            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(drpEquipmentGroup, "EQUIPMENTGROUP", True)
                bindEqipMentGatory()

                If Not Request.QueryString("Action") Is Nothing Then
                    If Request.QueryString("Action").ToUpper() = "U" Or Request.QueryString("Action").ToUpper() = "US" Then
                        hdID.Value = objED.Decrypt(Request.QueryString("Productid").ToString().Trim())
                        ViewEquipment()
                        drpEquipmentGroup.Focus()
                    End If
                    If Request.QueryString("Action").ToUpper() = "US" Then
                        lblError.Text = objeAAMSMessage.messInsert
                    End If
                End If
            End If
            objeAAMS.ExpirePageCache()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
           
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='EquipmentMaster']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='EquipmentMaster']").Attributes("Value").Value)
                End If
                'If strBuilder(1) = "0" Then
                '    btn_New.Enabled = False
                'End If
                'If strBuilder(2) = "0" Then
                '    btnSave.Enabled = False
                'End If
                'When View rights disabled
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                End If
                'When Add rights disabled
                If strBuilder(1) = "0" Then
                    btn_New.Enabled = False
                    btnSave.Enabled = False
                End If
                'When modify rights disabled and Add rights enabled
                If strBuilder(2) = "0" And (Request.QueryString("Action") = "US" Or Request.QueryString("Action") = "U") Then
                    btnSave.Enabled = False
                End If
                'When modify rights Enabled and Add rights disabled
                If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                    btnSave.Enabled = True
                End If


                'Rights for Equipment Category
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='EquipmentCategoryEdit']").Count <> 0 Then
                    strBuilderCatg = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='EquipmentCategoryEdit']").Attributes("Value").Value)
                End If
                
                'When modify rights disabled and Add rights enabled
                If strBuilderCatg(2) = "0" And (Request.QueryString("Action") = "US" Or Request.QueryString("Action") = "U") Then
                    DlstEquipMentCategory.Enabled = False
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub ViewEquipment()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objtEquipment As New AAMS.bizMaster.bzEquipment
            Dim objArray As New ArrayList()
            '            Dim strli As ListItem

            objInputXml.LoadXml("<MS_VIEWEQUIPMENT_INPUT><Productid></Productid></MS_VIEWEQUIPMENT_INPUT>")
            If Request.QueryString("Action").ToUpper() = "U" Or Request.QueryString("Action").ToUpper() = "US" Then
                objInputXml.DocumentElement.SelectSingleNode("Productid").InnerXml = hdID.Value 'Request.QueryString("Productid").ToString()
            End If

            'Here Back end Method Call
            objOutputXml = objtEquipment.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                txtEquipmentType.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Equipment_Code").Value()
                drpEquipmentGroup.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Egroup_Code").Value()
                txtDescription.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Description").Value()
                txtConfiguration.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Config").Value()
                txtCpuSpeed.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Cpu_Speed").Value()
                txtRAM.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Ram").Value()
                txtVRAM.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Vram").Value()
                txtHDD.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Hdd").Value()
                txtPrinterSpeed.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Printer_Speed").Value()
                txtPSpeedMeasure.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("P_Speed_Measure").Value()
                txtMonitorSize.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Monitor_Size").Value()
                txtModemSPeed.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Modem_Speed").Value()
                txtLanCardType.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Lan_Card_Type").Value()
                txtLanCardSpeed.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Lan_Card_Speed").Value()
                txtWanCardType.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Wan_Card_Type").Value()
                txtCDRSpeed.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Cdr_Speed").Value()
                txtISASlots.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Isa_Slots").Value()
                chkMaintainBy.Checked = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Maintain_Balance_By").Value()
                txtPCISlots.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Pci_Slots").Value()
                chkMaintainBal.Checked = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Maintain_Balance").Value()
                txtRemarks.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Remarks").Value()

                txtSegExpected.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("SegExpected").Value()

                txtUnitCost.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("UnitCost").Value()


                Try
                    txtNPUnitCost.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("NPUnitCost").InnerText
                    txtLKUnitCost.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("LKUnitCost").InnerText
                    txtBDUnitCost.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("BDUnitCost").InnerText
                    txtBTUnitCost.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("BTUnitCost").InnerText
                    txtMLUnitCost.Text = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("MLUnitCost").InnerText
                Catch ex As Exception

                End Try

                'If objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Equipment_Cat_ID") IsNot Nothing Then
                '    Dim li As ListItem = DlstEquipMentCategory.Items.FindByValue(objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Equipment_Cat_ID").Value())
                '    If li IsNot Nothing Then
                '        DlstEquipMentCategory.SelectedValue = li.Value
                '    End If
                'End If

                ' @ Added by Neeraj for Multiple Selection Category
                If objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Equipment_Cat_IDS") IsNot Nothing Then
                    Dim strCatgIDS As String = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Equipment_Cat_IDS").Value().ToString()
                    objArray.AddRange(strCatgIDS.Split("|"))
                    For Each Str As String In objArray
                        Dim li As ListItem = DlstEquipMentCategory.Items.FindByValue(Str)
                        If li IsNot Nothing Then
                            'DlstEquipMentCategory.SelectedValue = li.Value
                            DlstEquipMentCategory.Items(DlstEquipMentCategory.Items.IndexOf(li)).Selected = True

                        End If
                    Next
                End If
                'end @ Added by Neeraj for Multiple Selection Category
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            ' DlstEquipMentCategory.Enabled = False
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        ' Exit Sub
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objEquipment As New AAMS.bizMaster.bzEquipment
        Dim selected() As String
        '@ ArayList for holding the sections for the user  
        Dim sections As New ArrayList()

        Try
            ' objInputXml.LoadXml("<MS_UPDATEEQUIPMENT_INPUT><EQUIPMENT_DETAIL Action='' Productid='' Equipment_Code=''  Egroup_Code='' Config='' Description='' Maintain_Balance_By='' Cpu_Speed='' Cdr_Speed='' Ram='' Vram='' Hdd='' Printer_Speed=''   P_Speed_Measure='' Monitor_Size='' Modem_Speed='' Lan_Card_Type='' Lan_Card_Speed='' Wan_Card_Type='' Pci_Slots='' Isa_Slots='' Remarks='' Maintain_Balance='' SegExpected='' UnitCost='' /></MS_UPDATEEQUIPMENT_INPUT>")

            objInputXml.LoadXml("<MS_UPDATEEQUIPMENT_INPUT><EQUIPMENT_DETAIL Action='' Productid='' Equipment_Code=''  Egroup_Code='' Equipment_Cat_ID ='' Config='' Description='' Maintain_Balance_By='' Cpu_Speed='' Cdr_Speed='' Ram='' Vram='' Hdd='' Printer_Speed=''   P_Speed_Measure='' Monitor_Size='' Modem_Speed='' Lan_Card_Type='' Lan_Card_Speed='' Wan_Card_Type='' Pci_Slots='' Isa_Slots='' Remarks='' Maintain_Balance='' SegExpected='' UnitCost='' /></MS_UPDATEEQUIPMENT_INPUT>")

            With objInputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL")
                If Request.QueryString("ACTION").ToString().Trim().ToUpper = "U" Or Request.QueryString("Action").ToUpper() = "US" Then
                    .Attributes("Productid").Value() = hdID.Value 'Request.QueryString("Productid")
                    .Attributes("Equipment_Code").Value() = txtEquipmentType.Text
                    .Attributes("Action").Value() = "U"
                    .Attributes("Egroup_Code").Value() = drpEquipmentGroup.SelectedValue
                    .Attributes("Description").Value() = txtDescription.Text
                    .Attributes("Config").Value() = txtConfiguration.Text
                    .Attributes("Cpu_Speed").Value() = txtCpuSpeed.Text
                    .Attributes("Ram").Value() = txtRAM.Text
                    .Attributes("Vram").Value() = txtVRAM.Text
                    .Attributes("Hdd").Value() = txtHDD.Text
                    .Attributes("Printer_Speed").Value() = txtPrinterSpeed.Text
                    .Attributes("P_Speed_Measure").Value() = txtPSpeedMeasure.Text
                    .Attributes("Monitor_Size").Value() = txtMonitorSize.Text
                    .Attributes("Modem_Speed").Value() = txtModemSPeed.Text
                    .Attributes("Lan_Card_Type").Value() = txtLanCardType.Text
                    .Attributes("Lan_Card_Speed").Value() = txtLanCardSpeed.Text
                    .Attributes("Wan_Card_Type").Value() = txtWanCardType.Text
                    .Attributes("Cdr_Speed").Value() = txtCDRSpeed.Text
                    .Attributes("Isa_Slots").Value() = txtISASlots.Text
                    .Attributes("Pci_Slots").Value() = txtPCISlots.Text

                    .Attributes("UnitCost").Value() = txtUnitCost.Text.Trim()

                    If chkMaintainBy.Checked Then
                        .Attributes("Maintain_Balance_By").Value() = 1
                    Else
                        .Attributes("Maintain_Balance_By").Value() = 0
                    End If
                    .Attributes("Pci_Slots").Value() = txtPCISlots.Text
                    If chkMaintainBal.Checked Then
                        .Attributes("Maintain_Balance").Value() = 1
                    Else
                        .Attributes("Maintain_Balance").Value() = 0
                    End If
                    .Attributes("Remarks").Value() = txtRemarks.Text
                    .Attributes("SegExpected").Value() = txtSegExpected.Text

                    If DlstEquipMentCategory.SelectedValue <> "" Then
                        For Each item As ListItem In DlstEquipMentCategory.Items
                            If item.Selected Then
                                sections.Add(item.Value)
                            End If
                        Next
                        selected = CType(sections.ToArray(Type.GetType("System.String")), String())
                        '.Attributes("Equipment_Cat_ID").Value() = DlstEquipMentCategory.SelectedValue
                        .Attributes("Equipment_Cat_ID").Value() = String.Join("|", selected)
                    End If

                Else
                    .Attributes("Action").Value() = "I"
                    .Attributes("Equipment_Code").Value() = txtEquipmentType.Text
                    .Attributes("Egroup_Code").Value() = drpEquipmentGroup.SelectedValue
                    .Attributes("Description").Value() = txtDescription.Text
                    .Attributes("Config").Value() = txtConfiguration.Text
                    .Attributes("Cpu_Speed").Value() = txtCpuSpeed.Text
                    .Attributes("Ram").Value() = txtRAM.Text
                    .Attributes("Vram").Value() = txtVRAM.Text
                    .Attributes("Hdd").Value() = txtHDD.Text
                    .Attributes("Printer_Speed").Value() = txtPrinterSpeed.Text
                    .Attributes("P_Speed_Measure").Value() = txtPSpeedMeasure.Text
                    .Attributes("Monitor_Size").Value() = txtMonitorSize.Text
                    .Attributes("Modem_Speed").Value() = txtModemSPeed.Text
                    .Attributes("Lan_Card_Type").Value() = txtLanCardType.Text
                    .Attributes("Lan_Card_Speed").Value() = txtLanCardSpeed.Text
                    .Attributes("Wan_Card_Type").Value() = txtWanCardType.Text
                    .Attributes("Cdr_Speed").Value() = txtCDRSpeed.Text
                    .Attributes("Isa_Slots").Value() = txtISASlots.Text
                    If chkMaintainBy.Checked Then
                        .Attributes("Maintain_Balance_By").Value() = 1
                    Else
                        .Attributes("Maintain_Balance_By").Value() = 0
                    End If
                    .Attributes("Pci_Slots").Value() = txtPCISlots.Text

                    .Attributes("UnitCost").Value() = txtUnitCost.Text.Trim()

                    If chkMaintainBal.Checked Then
                        .Attributes("Maintain_Balance").Value() = 1
                    Else
                        .Attributes("Maintain_Balance").Value() = 0
                    End If
                    .Attributes("Remarks").Value() = txtRemarks.Text
                    .Attributes("SegExpected").Value() = txtSegExpected.Text

                    If DlstEquipMentCategory.SelectedValue <> "" Then
                        For Each item As ListItem In DlstEquipMentCategory.Items
                            If item.Selected Then
                                sections.Add(item.Value)
                            End If
                        Next
                        selected = CType(sections.ToArray(Type.GetType("System.String")), String())
                        '.Attributes("Equipment_Cat_ID").Value() = DlstEquipMentCategory.SelectedValue
                        .Attributes("Equipment_Cat_ID").Value() = String.Join("|", selected)
                    End If
                End If
            End With
            objOutputXml = objEquipment.Update(objInputXml)
            Dim CheckBoxOb As String = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Productid").Value().Trim()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If (Request.QueryString("Action").ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().ToUpper() = "US") Then
                    ViewEquipment()
                    lblError.Text = objeAAMSMessage.messUpdate
                Else
                    lblError.Text = objeAAMSMessage.messInsert
                    Response.Redirect("INVUP_Equipment.aspx?Action=US&Productid=" + objED.Encrypt(CheckBoxOb))

                End If
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click

        Response.Redirect(Request.Url.ToString, False)
        'If Not Request.QueryString("Action") Is Nothing Then
        '    If (Request.QueryString("Action").ToUpper() = "U") Then
        '        ViewEquipment()
        '        drpEquipmentGroup.Focus()
        '    Else
        '        txtEquipmentType.Text = String.Empty
        '        drpEquipmentGroup.SelectedIndex = -1
        '        txtDescription.Text = String.Empty
        '        txtConfiguration.Text = String.Empty
        '        txtCpuSpeed.Text = String.Empty
        '        txtRAM.Text = String.Empty
        '        txtHDD.Text = String.Empty
        '        txtPrinterSpeed.Text = String.Empty
        '        txtPSpeedMeasure.Text = String.Empty
        '        txtMonitorSize.Text = String.Empty
        '        txtModemSPeed.Text = String.Empty
        '        txtLanCardType.Text = String.Empty
        '        txtLanCardSpeed.Text = String.Empty
        '        txtWanCardType.Text = String.Empty
        '        txtCDRSpeed.Text = String.Empty
        '        txtISASlots.Text = String.Empty
        '        chkMaintainBy.Checked = False
        '        txtPCISlots.Text = String.Empty
        '        chkMaintainBal.Checked = False
        '        txtSegExpected.Text = String.Empty
        '        txtRemarks.Text = ""
        '        txtEquipmentType.Focus()
        '        txtVRAM.Text = String.Empty
        '        txtUnitCost.Text = String.Empty


        '    End If
        'End If
    End Sub

    Protected Sub btn_New_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_New.Click

    End Sub
    Private Sub bindEqipMentGatory()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim dsHardware As New DataSet
        Try
            Dim objHardward As New AAMS.bizIncetive.bzEquipment
            objOutputXml = New XmlDocument
            objOutputXml = objHardward.INC_HARDWARE_LIST()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                dsHardware.ReadXml(objXmlReader)
                DlstEquipMentCategory.DataSource = dsHardware.Tables("HARDWARE").DefaultView
                DlstEquipMentCategory.DataTextField = "BC_EQP_CATG_TYPE"
                DlstEquipMentCategory.DataValueField = "BC_EQP_CATG_ID"
                DlstEquipMentCategory.DataBind()
            End If
            'DlstEquipMentCategory.Items.Insert(0, "")
        Catch ex As Exception
        End Try
    End Sub
End Class
