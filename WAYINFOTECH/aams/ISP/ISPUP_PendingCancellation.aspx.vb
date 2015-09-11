
Partial Class ISP_ISPUP_PendingCancellation
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objmsg As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strErrormsg As String = ""
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If
            btnSave.Attributes.Add("onclick", "return ValidationISPPendingCancellation();")
            ' btnNew.Attributes.Add("onclick", "return NewFunction();")

            ' #######################################
            ' ########## This Code code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspPendingCancellation']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspPendingCancellation']").Attributes("Value").Value)

                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                    If strBuilder(1) = "0" Then
                        '  btnNew.Enabled = False
                        'btnSave.Enabled = False
                    End If
                    If Not Request.QueryString("IspOrderId") Is Nothing Then
                        If strBuilder(2) = "0" Then
                            btnSave.Enabled = False
                        End If
                    End If

                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            If (Not IsPostBack) Then
                objeAAMS.BindDropDown(drpIspOrderStatus, "ISPOrderStatusList", True)
             
                BindDropDownOrderNoByLcode(drpNewOrderNoForCan, "OrderNoByLcode", True)
                If (Not Request.QueryString("msg") Is Nothing) Then
                    lblError.Text = objmsg.messInsert
                End If
                'If Not Request.QueryString("Action") Is Nothing Then
                '    If Request.QueryString("Action").ToString() = "New" Then
                '        imgAgency.Visible = True
                '        imgISP.Visible = True
                '        imgNpid.Visible = True
                '        btnPrevIspDetails.Enabled = False
                '    End If
                'End If

                If Not Request.QueryString("IspOrderId") Is Nothing Then
                    '@ Getting  Decrrepted Data 
                    Dim DecreptedIspOrderId As String
                    DecreptedIspOrderId = objED.Decrypt(Request.QueryString("IspOrderId").ToString())
                    '@ End of Getting Decrepted Data
                    ViewIspOrder(DecreptedIspOrderId, strErrormsg)
                    'ViewIspOrder(Request.QueryString("IspOrderId").ToString(), strErrormsg)
                    If strErrormsg.Trim.Length > 0 Then
                        lblError.Text = strErrormsg
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub


    Private Sub ViewIspOrder(ByVal strIspOrderId As String, ByRef strErrormsg As String)
        ' Response.Write(strIspOrderId)      
        Try

            Dim objInputIspOrderXml, objOutputIspOrderXml As New XmlDocument
            Dim objbzIspOrder As New AAMS.bizISP.bzISPOrder
            objInputIspOrderXml.LoadXml("<ISP_VIEWORDER_INPUT><ISPOrderID/><ACTION>C</ACTION></ISP_VIEWORDER_INPUT>")
            objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPOrderID").InnerText = strIspOrderId
            ' objOutputIspOrderXml.LoadXml("<ISP_VIEWISP_OUTPUT><ISPORDER ISPOrderID='' CreationDate='' LCODE='' Agencyname='' Address='' City='' Country='' Phone='' Fax='' OfficeId='' ISPName='' ISPCityName='' ISPPlanID='' BandWidth='' InstallationCharges='' MonthlyCharges='' EquipmentInclude='' EquipmentOneTimeCharges='' EquipmentMonthlyCharges='' DeliveryTimeLine='' OrderNumber='' OrderDate='' OnlineDate='' ExpectedDate='' ISPOrderStatusId='' LoggedBy='' ApprovedBY='' ApprovalDate='' CAFNumber='' CancellationDate='' MDNNumber='' LoginName='' CancellationReason='' Remarks=''/><Errors Status='FALSE'><Error Code='' Description=''/></Errors></ISP_VIEWISP_OUTPUT>")
            objOutputIspOrderXml = objbzIspOrder.View(objInputIspOrderXml)

            If objOutputIspOrderXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                txtAgencyName.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("Agencyname").Value.Trim()
                txtAgencyAddress.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("Address").Value.Trim()
                txtCity.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("City").Value.Trim()
                txtCountry.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("Country").Value.Trim()
                txtPhone.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("Phone").Value.Trim()
                txtFax.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("Fax").Value.Trim()
                txtOfficeId.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("OfficeId").Value.Trim()
                txtIsp.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ISPName").Value.Trim()
                txtISPCityName.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ISPCityName").Value.Trim()
                txtNPID.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("NPID").Value.Trim()
                txtBandWidth.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("BandWidth").Value.Trim()
                ' txtInsCharges.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("InstallationCharges").Value.Trim()
                ' txtMonCharges.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("MonthlyCharges").Value.Trim()
                'If objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("EquipmentInclude").Value.Trim() = True Then
                '    txtEquipIncluded.Text = "Yes" ' objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("EquipmentInclude").Value.Trim()
                'Else
                '    txtEquipIncluded.Text = "No"
                'End If

                '  txtEqipOneTimeCharges.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("EquipmentOneTimeCharges").Value.Trim()
                ' txtEqipMonRented.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("EquipmentMonthlyCharges").Value.Trim()
                '  txtDelTimeLine.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("DeliveryTimeLine").Value.Trim()
                txtOrderNumber.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("OrderNumber").Value.Trim()

                Dim li3 As ListItem = drpIspOrderStatus.Items.FindByValue(objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ISPOrderStatusId").Value.Trim())
                If li3 IsNot Nothing Then
                    drpIspOrderStatus.SelectedValue = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ISPOrderStatusId").Value.Trim()

                End If


                txtOrderDate.Text = objeAAMS.ConvertDateBlank(objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("OrderDate").Value.Trim())
                ' txtOnlineDate.Text = objeAAMS.ConvertDateBlank(objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("OnlineDate").Value.Trim())
                ' txtExpOnlineDate.Text = objeAAMS.ConvertDateBlank(objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ExpectedDate").Value.Trim())
                hdApprovedBy.Value = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ApprovedBY").Value.Trim()

                txtApprovedBy.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ApprovedByName").Value.Trim()
                'drpApprovedBy.SelectedValue = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ApprovedBY").Value.Trim()
                txtLoggedby.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("LoginName").Value.Trim()
                '  txtCreationDate.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("CreationDate").Value.Trim()

                hdLoggedByID.Value = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("LoggedBy").Value.Trim()

                txtCafAcId.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("CAFNumber").Value.Trim()
                ' txtApprovalDate.Text = objeAAMS.ConvertDateBlank(objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ApprovalDate").Value.Trim())
                txtMDNNo.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("MDNNumber").Value.Trim()
                txtCanDate.Text = objeAAMS.ConvertDateBlank(objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("CancellationDate").Value.Trim())
                ' txtLoginName.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("LoginName").Value.Trim()
                txtCanReason.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("CancellationReason").Value.Trim()
                'txtRem.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("Remarks").Value.Trim()
                hdISPId.Value = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ISPID").Value.Trim()

                Dim li2 As ListItem = drpNewOrderNoForCan.Items.FindByValue(objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("OriginalOrderNo").Value.Trim())
                If li2 IsNot Nothing Then
                    drpNewOrderNoForCan.SelectedValue = li2.Value ' .SelectSingleNode("ORDERS").Attributes("PLANID").InnerText
                Else
                    drpNewOrderNoForCan.Items.Insert(drpNewOrderNoForCan.Items.Count, New ListItem(objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("OriginalOrderNo").Value.Trim(), objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("OriginalOrderNo").Value.Trim()))
                    drpNewOrderNoForCan.SelectedValue = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("OriginalOrderNo").Value.Trim()
                End If
                hdIspPlanId.Value = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ISPPlanID").Value.Trim()

                '####################################
                ' @ Added Code For ISP on 19/06/09
                If drpIspOrderStatus.SelectedValue <> "" Then
                    If drpIspOrderStatus.SelectedItem.Text.Trim.ToUpper() = "ORDER CANCELLED" Then
                        Dim li5 As ListItem = drpNewOrderNoForCan.Items.FindByValue("Order Rejected")
                        If li5 IsNot Nothing Then
                            drpNewOrderNoForCan.SelectedValue = li5.Value ' .SelectSingleNode("ORDERS").Attributes("PLANID").InnerText
                        Else
                            drpNewOrderNoForCan.Items.Insert(drpNewOrderNoForCan.Items.Count, New ListItem("Order Rejected", "Order Rejected"))
                            drpNewOrderNoForCan.SelectedValue = "Order Rejected"
                        End If
                    Else
                        Dim li5 As ListItem = drpNewOrderNoForCan.Items.FindByValue("Order Rejected")
                        If li5 IsNot Nothing Then
                            drpNewOrderNoForCan.SelectedValue = li5.Value ' .SelectSingleNode("ORDERS").Attributes("PLANID").InnerText
                            drpNewOrderNoForCan.Items.Remove(li5)
                        End If
                    End If
                End If

                ' @ Added Code For ISP on 19/06/09
                '@###############################

            Else
                lblError.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            strErrormsg = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim strErrormsg As String = ""
            Dim objInputIspOrderXml, objOutputIspOrderXml As New XmlDocument
            Dim obbzISPOrder As New AAMS.bizISP.bzISPOrder

            ' For Ediding the existing Isp Order 
            If Request.QueryString("IspOrderId") IsNot Nothing Then

                If (rdlNewCancel.SelectedValue = "t") Then

                End If

                objInputIspOrderXml.LoadXml("<ISP_UPDATEISPORDER_INPUT><ISPORDERDETAILS ISPOrderID='' LCODE='' ISPPlanID='' OrderNumber='' OrderDate='' CommissionedOn='' ECommissionedOn='' OrderStatusID='' LoggedBy='' ApprovedBY='' CancellationDate='' CancellationReason=''  ApprovalDate='' CircuitDeliveryDate='' CAFNumber='' UserName='' MDNNumber='' Remarks='' ISPID=''  OriginalOrderNo='' StaticIP=''></ISPORDERDETAILS></ISP_UPDATEISPORDER_INPUT>")

                '@ Getting  Decrepted Data 
                Dim DecreptedISPOrderID, DecreptedLcode As String
                DecreptedISPOrderID = objED.Decrypt(Request.QueryString("IspOrderId").ToString())
                DecreptedLcode = objED.Decrypt(Request.QueryString("Lcode").ToString())
                '@ End of Getting Decrepted Data
              
                'objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("ISPOrderID").InnerText = Request.QueryString("IspOrderId").ToString
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("ISPOrderID").InnerText = DecreptedISPOrderID
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("LCODE").InnerText = DecreptedLcode
                'objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("LCODE").InnerText = Request.QueryString("Lcode").ToString


                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("OrderNumber").InnerText = txtOrderNumber.Text
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("OrderDate").InnerText = objeAAMS.ConvertTextDate(txtOrderDate.Text)
                'objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("CommissionedOn").InnerText = objeAAMS.ConvertTextDate(txtOnlineDate.Text)
                ' objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("ECommissionedOn").InnerText = objeAAMS.ConvertTextDate(txtExpOnlineDate.Text)
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("OrderStatusID").InnerText = drpIspOrderStatus.SelectedValue
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("LoggedBy").InnerText = hdLoggedByID.Value.Trim()
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("ApprovedBY").InnerText = hdApprovedBy.Value 'drpApprovedBy.SelectedValue
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("CancellationDate").InnerText = objeAAMS.ConvertTextDate(txtCanDate.Text)
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("CancellationReason").InnerText = txtCanReason.Text
                '  objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("ApprovalDate").InnerText = objeAAMS.ConvertTextDate(txtApprovalDate.Text)
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("CAFNumber").InnerText = txtCafAcId.Text

                If Session("Security") IsNot Nothing Then
                    Dim objSecurityXml As New XmlDocument
                    objSecurityXml.LoadXml(Session("Security"))
                    objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("UserName").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Login").InnerText

                End If

                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("MDNNumber").InnerText = txtMDNNo.Text
                '  objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("Remarks").InnerText = txtRem.Text
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("OriginalOrderNo").InnerText = drpNewOrderNoForCan.SelectedValue
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("ISPPlanID").InnerText = hdIspPlanId.Value

                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("ISPID").InnerText = hdISPId.Value
                objOutputIspOrderXml = obbzISPOrder.UpdateCanCellation(objInputIspOrderXml)

                If objOutputIspOrderXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    lblError.Text = objmsg.messUpdate
                    ViewIspOrder(DecreptedISPOrderID, strErrormsg)
                    '   ViewIspOrder(Request.QueryString("IspOrderId"), strErrormsg)
                Else
                    lblError.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            txtAgencyName.Text = ""
            txtAgencyAddress.Text = ""
            lblError.Text = ""
            txtCity.Text = ""
            txtCountry.Text = ""
            txtPhone.Text = ""
            txtFax.Text = ""
            txtOfficeId.Text = ""
            txtIsp.Text = ""
            txtISPCityName.Text = ""
            txtNPID.Text = ""
            txtBandWidth.Text = ""
            'txtInsCharges.Text = ""
            'txtMonCharges.Text = ""
            'txtEquipIncluded.Text = ""
            'txtEqipOneTimeCharges.Text = ""
            'txtEqipMonRented.Text = ""
            'txtDelTimeLine.Text = ""
            txtOrderNumber.Text = ""
            drpIspOrderStatus.SelectedIndex = 0
            txtOrderDate.Text = ""
            'txtOnlineDate.Text = ""
            'txtExpOnlineDate.Text = ""
            txtLoggedby.Text = ""
            hdApprovedBy.Value = "" ' objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ApprovedBY").Value.Trim()
            txtApprovedBy.Text = ""
            'drpApprovedBy.SelectedIndex = 0
            txtCafAcId.Text = ""
            ' txtApprovalDate.Text = ""
            txtMDNNo.Text = ""
            txtCanDate.Text = ""
            ' txtLoginName.Text = ""
            txtCanReason.Text = ""
            ' txtRem.Text = ""
            hdIspPlanId.Value = ""
            hdLcode.Value = ""
            hdISPId.Value = ""
            Dim strErrormsg As String = ""
            If Not Request.QueryString("IspOrderId") Is Nothing Then
                '@ Getting  Decrepted Data 
                Dim DecreptedISPOrderID, DecreptedLcode As String
                DecreptedISPOrderID = objED.Decrypt(Request.QueryString("IspOrderId").ToString())
                DecreptedLcode = objED.Decrypt(Request.QueryString("Lcode").ToString())
                '@ End of Getting Decrepted Data
                ViewIspOrder(DecreptedISPOrderID, strErrormsg)

                '  ViewIspOrder(Request.QueryString("IspOrderId").ToString(), strErrormsg)

                If strErrormsg.Trim.Length > 0 Then
                    lblError.Text = strErrormsg
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Sub BindDropDownOrderNoByLcode(ByVal drpDownList As DropDownList, ByVal strType As String, ByVal bolSelect As Boolean)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        drpDownList.Items.Clear()
        Select Case strType
            Case "OrderNoByLcode"
                Dim objbzOrder As New AAMS.bizTravelAgency.bzOrder
                'objInputXml.LoadXml("<UP_SEARCHORDER_INPUT>	<LCODE />	<ORDER_NUMBER />	<ORDERTYPEID />	<ORDERSTATUSID />	<REGION />	<AGENCYNAME />	<GROUPDATA />	<City />	<Country />	<MSG_SEND_DATE_FROM />	<SENDBACK_DATE_FROM />	<APPROVAL_DATE_FROM />	<RECEIVED_DATE_FROM />	<PROCESSED_DATE_FROM />	<MSG_SEND_DATE_TO />	<SENDBACK_DATE_TO />	<APPROVAL_DATE_TO />	<RECEIVED_DATE_TO />	<PROCESSED_DATE_TO />	<Limited_To_OwnAagency></Limited_To_OwnAagency><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></UP_SEARCHORDER_INPUT>")
                objInputXml.LoadXml("<TA_ORIGINAL_INPUT><LCODE></LCODE></TA_ORIGINAL_INPUT>")
                With objInputXml.DocumentElement
                    If Request.QueryString("Lcode") IsNot Nothing Then
                        '@ Getting  Decrepted Data 
                        Dim DecreptedISPOrderID, DecreptedLcode As String
                        DecreptedISPOrderID = objED.Decrypt(Request.QueryString("IspOrderId").ToString())
                        DecreptedLcode = objED.Decrypt(Request.QueryString("Lcode").ToString())
                        '@ End of Getting Decrepted Data
                        '.SelectSingleNode("LCODE").InnerText = Request.QueryString("Lcode")
                        .SelectSingleNode("LCODE").InnerText = DecreptedLcode
                     
                    End If
                    ' .SelectSingleNode("ORDERTYPEID").InnerText = ""
                End With
                objOutputXml = New XmlDocument
                objOutputXml = objbzOrder.GetOrderNumber(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("ORDER")
                    drpDownList.DataTextField = "ORDER_NUMBER"
                    drpDownList.DataValueField = "ORDER_NUMBER"
                    drpDownList.DataBind()
                End If
                Dim li As ListItem
                li = drpDownList.Items.FindByValue("")
                If li Is Nothing Then
                    drpDownList.Items.Insert(0, New ListItem("--Select one--", ""))
                End If
        End Select
        '  IspNameEnableorDisable()
    End Sub

#Region "Code for Filter "
    'Code Added by Abhishek
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    'Code Added by Abhishek
#End Region

    Protected Sub drpIspOrderStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpIspOrderStatus.SelectedIndexChanged
        Try
            '####################################
            ' @ Added Code For ISP on 19/06/09
            'If drpIspOrderStatus.SelectedValue <> "" Then
            If drpIspOrderStatus.SelectedItem.Text.Trim.ToUpper() = "ORDER CANCELLED" Then
                Dim li5 As ListItem = drpNewOrderNoForCan.Items.FindByValue("Order Rejected")
                If li5 IsNot Nothing Then
                    drpNewOrderNoForCan.SelectedValue = li5.Value ' .SelectSingleNode("ORDERS").Attributes("PLANID").InnerText
                Else
                    drpNewOrderNoForCan.Items.Insert(drpNewOrderNoForCan.Items.Count, New ListItem("Order Rejected", "Order Rejected"))
                    drpNewOrderNoForCan.SelectedValue = "Order Rejected"
                End If
            Else
                Dim li5 As ListItem = drpNewOrderNoForCan.Items.FindByValue("Order Rejected")
                If li5 IsNot Nothing Then
                    drpNewOrderNoForCan.SelectedValue = li5.Value ' .SelectSingleNode("ORDERS").Attributes("PLANID").InnerText
                    drpNewOrderNoForCan.Items.Remove(li5)
                End If
            End If
            ' End If

            ' @ Added Code For ISP on 19/06/09
            '@###############################
        Catch ex As Exception

        End Try
    End Sub
End Class
