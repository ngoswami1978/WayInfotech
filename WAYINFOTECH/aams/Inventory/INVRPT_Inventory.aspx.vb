Imports System.Data
Imports System.Xml
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports System.Collections
Partial Class Inventory_INVRPT_Pc
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = Request.Url.ToString()
            objeAAMS.ExpirePageCache()
            btnReset.Attributes.Add("onclick", "return InventoryReset();")
            btnDisplay.Attributes.Add("onclick", "return CheckValidation();")
            lblError.Text = String.Empty
            'Code of Security Check
            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Inventory Report']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Inventory Report']").Attributes("Value").Value)
                    If strBuilder(4) = "0" Then
                        btnDisplay.Enabled = False
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                    End If

                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If Not Page.IsPostBack Then
                BindAllControl()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub BindAllControl()
        Try
            objeAAMS.BindDropDown(drpCity, "CITY", True, 3)
            objeAAMS.BindDropDown(drpCountry, "COUNTRY", True, 3)
            objeAAMS.BindDropDown(drpRegion, "REGION", True, 3)
            objeAAMS.BindDropDown(drpAoffice, "AOFFICE", True, 3)
            objeAAMS.BindDropDown(drpOnlineStatus, "ONLINESTATUSCODE", True, 3)
            objeAAMS.BindDropDown(drpEquipmentGroup, "EQUIPMENTGROUP", True, 3)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnDisplay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisplay.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objParentNode As XmlNode
        Dim objbzINV As New AAMS.bizMaster.bzEquipment
        Dim objbzGodown As New AAMS.bizInventory.bzGodown
        Try
            objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE /><AGENCYNAME /><CITY /><COUNTRY /><AOFFICE /><REGION /><WHOLEGROUP /><ONLINESTATUS /><EQUIPMENTGROUP /><EQUIPMENTTYPE /><DATEFROM /><DATETO /><RESPONSIBLESTAFFID /><RESP_1A/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_RPTPCINSTALL_INPUT>")
            objParentNode = objInputXml.DocumentElement.SelectSingleNode("PCINSTALL")
            objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hidLcode.Value
            objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text
            If (drpCity.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text
            End If
            If (drpCountry.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text
            End If
            If (drpAoffice.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = drpAoffice.SelectedItem.Text
            End If
            If (drpRegion.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = drpRegion.SelectedItem.Text
            End If
            If (drpOnlineStatus.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("ONLINESTATUS").InnerText = drpOnlineStatus.SelectedItem.Text
            End If
            If (drpEquipmentGroup.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("EQUIPMENTGROUP").InnerText = drpEquipmentGroup.SelectedItem.Text
            End If

            objInputXml.DocumentElement.SelectSingleNode("EQUIPMENTTYPE").InnerText = Trim(txtEquipmentType.Text) & ""
            objInputXml.DocumentElement.SelectSingleNode("DATEFROM").InnerText = txtInstallationFrom.Text
            objInputXml.DocumentElement.SelectSingleNode("DATETO").InnerText = txtInstallationTo.Text
            Dim objEmpXml As New XmlDocument
            objEmpXml.LoadXml(Session("Security"))
            objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
            If (chbWholeGroup.Checked) Then
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
            If Session("Security") IsNot Nothing Then
                'done on 18jan 09  as per discussion with neeraj
                objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = objeAAMS.EmployeeID(Session("Security"))
                'end
            End If
            If INV_Report.Items(0).Selected = True Then
                Session("eInventory_Pc") = objInputXml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=INV_Pc", False)
            ElseIf INV_Report.Items(1).Selected = True Then
                Session("eInventory_MiscHardware") = objInputXml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=INV_MiscHardware", False)
            ElseIf INV_Report.Items(2).Selected = True Then
                Session("eInventory_PcSummary") = objInputXml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=INV_PcSummary", False)
            ElseIf INV_Report.Items(3).Selected = True Then
                Session("MiscPcDeinstall") = objInputXml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=InventoryPcDeInstallation", False)
            ElseIf INV_Report.Items(4).Selected = True Then
                Session("MiscDeInstall") = objInputXml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=InventoryMiscDeInstallation", False)
            ElseIf INV_Report.Items(5).Selected = True Then
                Session("MiscSummary") = objInputXml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=InventoryMiscSummary", False)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
End Class
