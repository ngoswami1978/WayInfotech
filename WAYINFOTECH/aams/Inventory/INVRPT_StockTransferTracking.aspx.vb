Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Partial Class Inventory_INVRPT_StockTransferTracking
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
            Session("PageName") = "Inventory/INVRPT_StockTransferTracking.aspx"
            objEaams.ExpirePageCache()
            Dim objSecurityXml As New XmlDocument
            btnDisplay.Attributes.Add("onclick", "return ValidateStockTransferTrackingSearch();")
            btnReset.Attributes.Add("onclick", "return ResetAll();")
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='StockTransferTackingReport']").Count <> 0 Then
                    strBuilder = objEaams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='StockTransferTackingReport']").Attributes("Value").Value)
                    'If strBuilder(0) = "0" Then
                    '    btnDisplay.Enabled = False
                    'End If
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
    Private Sub BindDropDowns()
        Try
            ' objEaams.BindDropDown(drpGodownFrom, "GODOWN", True)
            ' objEaams.BindDropDown(drpGodownTo, "GODOWN", True)
            FillGodown(drpGodownFrom)
            FillGodown(drpGodownTo)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Sub FillGodown(ByRef drp As DropDownList)

        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))


        ' This code binds godown on the basis of logged user
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            Try
                drp.Items.Clear()
                Dim objbzGodown As New AAMS.bizInventory.bzGodown
                Dim objInputXml As New XmlDocument
                Dim objOutputXml As New XmlDocument
                Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                objInputXml.LoadXml("<INV_LISTGODOWN_INPUT><AOFFICE/><CITYNAME/><REGION/></INV_LISTGODOWN_INPUT>")
                'If  ChallanRegionWiseGodown value is "1 or 2" then fill Region id else cityname
                If Session("Security") IsNot Nothing Then
                    Dim strRegionId As String = objEaams.SecurityRegionID(Session("Security"))
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
                    drp.DataSource = ds.Tables("GODOWN").DefaultView
                    drp.DataTextField = "GODOWNNAME"
                    drp.DataValueField = "GODOWNID"
                    drp.DataBind()
                Else
                    If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value <> "No Record Found") Then
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            Finally
                drp.Items.Insert(0, New ListItem("--All--", ""))
            End Try
        Else
            objEaams.BindDropDown(drp, "GODOWN", True, 3)
        End If


    End Sub

    Protected Sub btnDisplay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisplay.Click
        Try
            StockTransferTrackingReport()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub StockTransferTrackingReport()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        Dim objbzFeedback As New AAMS.bizInventory.bzChallan
        Try
            objInputXml.LoadXml("<INV_RPT_STOCKTRANSFERHISTORY_CHALLAN_INPUT><TypeOfData></TypeOfData><SerialNumber></SerialNumber><VenderSerialNo></VenderSerialNo><EQUIPMENT_CODE></EQUIPMENT_CODE><RGodownID></RGodownID><GodownID></GodownID><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo></INV_RPT_STOCKTRANSFERHISTORY_CHALLAN_INPUT>")
            With objInputXml.DocumentElement
                .SelectSingleNode("SerialNumber").InnerText = txtAmadeusSerialNo.Text.Trim()
                .SelectSingleNode("VenderSerialNo").InnerText = txtVendorSerialNo.Text.Trim()
                .SelectSingleNode("EQUIPMENT_CODE").InnerText = txtEquipCode.Text.Trim()
                If (drpGodownFrom.SelectedIndex <> 0) Then
                    .SelectSingleNode("RGodownID").InnerText = drpGodownFrom.SelectedValue
                End If
                If (drpGodownTo.SelectedIndex <> 0) Then
                    .SelectSingleNode("GodownID").InnerText = drpGodownTo.SelectedValue
                End If
                If txtTransferDateFrom.Text.Trim().Length <> 0 Then
                    .SelectSingleNode("ChallanDateFrom").InnerText = objEaams.ConvertTextDate(txtTransferDateFrom.Text.Trim())
                Else
                    .SelectSingleNode("ChallanDateFrom").InnerText = ""
                End If
                If txtTransferDateTo.Text.Trim().Length <> 0 Then
                    .SelectSingleNode("ChallanDateTo").InnerText = objEaams.ConvertTextDate(txtTransferDateTo.Text.Trim())
                Else
                    .SelectSingleNode("ChallanDateTo").InnerText = txtTransferDateTo.Text.Trim()
                End If
                
                If INV_Report.Items(0).Selected = True Then
                    .SelectSingleNode("TypeOfData").InnerText = 2
                Else
                    .SelectSingleNode("TypeOfData").InnerText = 1
                End If

            End With
            'Here Back end Method Call
            If INV_Report.Items(0).Selected = True Then
                Session("eStockTransferDetail") = objInputXml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=StockTransferSummary", False)
            Else
                Session("eStockTransferSummary") = objInputXml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=StockTransferDetail", False)

                
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
End Class
