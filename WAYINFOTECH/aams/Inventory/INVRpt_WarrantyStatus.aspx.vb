Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Partial Class Inventory_INVRpt_WarrantyStatus
    Inherits System.Web.UI.Page
    Dim objEaams As New eAAMS
    Dim strBuilder As New StringBuilder
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
            btnDisplay.Attributes.Add("onclick", "return ValidateOrderSearch();")
            btnReset.Attributes.Add("onclick", "return ResetWarrantyStatus();")
            Session("PageName") = "Inventory/INVRpt_WarrantyStatus.aspx"
            objEaams.ExpirePageCache()
           If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objEaams.CheckSession())
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='WARRANTYSTATUS REPORT']").Count <> 0 Then
                    strBuilder = objEaams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='WARRANTYSTATUS REPORT']").Attributes("Value").Value)
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
                strBuilder = objEaams.SecurityCheck(31)
            End If


            If Not Page.IsPostBack Then
                BindDropDowns()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnDisplay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisplay.Click
        Try
            WarrantyStatusReport()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub WarrantyStatusReport()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        ' Dim objbzFeedback As New AAMS.bizHelpDesk.bzFeedback
        Try
            objInputXml.LoadXml("<INV_RPT_WARRRNTYSTATUS_CHALLAN_INPUT><PurchaseOrderID></PurchaseOrderID><EquipmentCode></EquipmentCode><PurchaseDateFrom></PurchaseDateFrom><PurchaseDateTo></PurchaseDateTo><PRODUCTID></PRODUCTID><SerailNumber></SerailNumber><VenderSerialNo></VenderSerialNo></INV_RPT_WARRRNTYSTATUS_CHALLAN_INPUT>")
            With objInputXml.DocumentElement
                .SelectSingleNode("PurchaseOrderID").InnerText = txtPurchaseOrderNo.Text.Trim()
                .SelectSingleNode("SerailNumber").InnerText = txtAmadeusSerialNo.Text.Trim()
                .SelectSingleNode("VenderSerialNo").InnerText = txtVendorSerialNo.Text.Trim()
                If txtOrderDateTo.Text.Trim().Length <> 0 Then
                    .SelectSingleNode("PurchaseDateTo").InnerText = objEaams.ConvertTextDate(txtOrderDateTo.Text.Trim())
                Else
                    .SelectSingleNode("PurchaseDateTo").InnerText = ""
                End If
                If txtOrderFromDt.Text.Trim().Length <> 0 Then
                    .SelectSingleNode("PurchaseDateFrom").InnerText = objEaams.ConvertTextDate(txtOrderFromDt.Text.Trim())
                Else
                    .SelectSingleNode("PurchaseDateFrom").InnerText = txtOrderFromDt.Text.Trim()
                End If
                If (drpProduct.SelectedIndex <> 0) Then
                    .SelectSingleNode("PRODUCTID").InnerText = drpProduct.SelectedIndex
                End If
                .SelectSingleNode("EquipmentCode").InnerText = txtEquipCode.Text.Trim()
            End With
            'Here Back end Method Call
            Session("WarrantyStatus") = objInputXml.OuterXml
            Response.Redirect("../RPSR_ReportShow.aspx?Case=InventoryWarrantyStatus", False)
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub

    Private Sub BindDropDowns()
        Try
            objEaams.BindDropDown(drpProduct, "EQUIPMENT", True, 3)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

End Class
