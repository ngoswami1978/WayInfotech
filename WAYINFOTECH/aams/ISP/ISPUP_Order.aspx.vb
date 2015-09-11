
Partial Class ISP_ISPUP_Order
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objmsg As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
    Dim imgDown As New Image
    Dim imgUp As New Image
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnPrevIspDetails.CssClass = "headingtabactive"
        btnIspOrder.CssClass = "headingtab"
        BtnSendingMail.CssClass = "headingtabactive"
        Dim strErrormsg As String = ""
        Try
            Session("EmailList") = Nothing
            lblError.Text = ""
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

            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgDown.ImageUrl = "~/Images/Sortdown.gif"

            btnSave.Attributes.Add("onclick", "return ValidationISPOrder();")
            btnNew.Attributes.Add("onclick", "return NewFunction();")
            ' imgISP.Visible = True
            ' imgNpid.Visible = True
            ' #######################################
            ' ########## This Code code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspOrder']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspOrder']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If

                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                        'btnSave.Enabled = False
                    End If
                    If strBuilder(4) = "0" Then
                        btnExport.Enabled = False

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
                BindDropDownIspName(drpIspName, "ISPLISTByAgencyCity", True)
                ' objeAAMS.BindDropDown(drpLoggedBy, "EMPLOYEE", True)
                ' objeAAMS.BindDropDown(drpApprovedBy, "EMPLOYEE", True)
                If (Not Request.QueryString("msg") Is Nothing) Then
                    lblError.Text = objmsg.messInsert
                End If
                If Not Request.QueryString("Action") Is Nothing Then
                    If Request.QueryString("Action").ToString() = "New" Then
                        imgAgency.Visible = True
                        'imgISP.Visible = True
                        'imgNpid.Visible = True
                        btnPrevIspDetails.Enabled = False
                        BtnSendingMail.Enabled = False
                    End If
                End If

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

            If Session("PrevIspDetails") IsNot Nothing Then
                Session("PrevIspDetails") = Nothing
                BindPrevieouoOder()
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    Protected Sub btnPrevIspDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrevIspDetails.Click
        Try
            BindPrevieouoOder()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnIspOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIspOrder.Click
        pnlPrevIspOrder.Visible = False
        pnlIspOrder.Visible = True
        btnPrevIspDetails.CssClass = "headingtabactive"
        BtnSendingMail.CssClass = "headingtabactive"
        btnIspOrder.CssClass = "headingtab"
        Try
            btnReset_Click(sender, e)

            btnPrevIspDetails.CssClass = "headingtabactive"
            BtnSendingMail.CssClass = "headingtabactive"
            btnIspOrder.CssClass = "headingtab"
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Private Sub ViewIspOrder(ByVal strIspOrderId As String, ByRef strErrormsg As String)
        ' Response.Write(strIspOrderId)      
        Try

            Dim objInputIspOrderXml, objOutputIspOrderXml As New XmlDocument
            Dim objbzIspOrder As New AAMS.bizISP.bzISPOrder
            objInputIspOrderXml.LoadXml("<ISP_VIEWORDER_INPUT><ISPOrderID/></ISP_VIEWORDER_INPUT>")
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

                ' txtIsp.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ISPName").Value.Trim()



                ' @ New Code Added

                'Dim li2 As ListItem = drpIspName.Items.FindByText(objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ISPName").Value.Trim())
                'If li2 IsNot Nothing Then
                '    drpIspName.SelectedValue = li2.Value ' .SelectSingleNode("ORDERS").Attributes("PLANID").InnerText
                'End If

                Dim li2 As ListItem = drpIspName.Items.FindByValue(objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ISPID").Value.Trim())
                If li2 IsNot Nothing Then
                    drpIspName.SelectedValue = li2.Value ' .SelectSingleNode("ORDERS").Attributes("PLANID").InnerText
                End If
                ' drpIspName.SelectedValue = .SelectSingleNode("ORDERS").Attributes("ISPID").InnerText

                GetNPID()

                ' txtPlainId.Text = .SelectSingleNode("ORDERS").Attributes("PLANID").InnerText

                'Dim li As ListItem = drpPlainId.Items.FindByText(objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("NPID").Value.Trim())
                'If li IsNot Nothing Then
                '    drpPlainId.SelectedValue = li.Value ' .SelectSingleNode("ORDERS").Attributes("PLANID").InnerText
                'End If
                Dim li As ListItem = drpPlainId.Items.FindByValue(objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ISPPlanID").Value.Trim())
                If li IsNot Nothing Then
                    drpPlainId.SelectedValue = li.Value ' .SelectSingleNode("ORDERS").Attributes("PLANID").InnerText
                End If

                ' @ End of New Code Added


                '  txtNEWNPID.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("NPID").Value.Trim()

                txtISPCityName.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ISPCityName").Value.Trim()





                txtBandWidth.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("BandWidth").Value.Trim()
                txtInsCharges.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("InstallationCharges").Value.Trim()
                txtMonCharges.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("MonthlyCharges").Value.Trim()
                If objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("EquipmentInclude").Value.Trim() = True Then
                    txtEquipIncluded.Text = "Yes" ' objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("EquipmentInclude").Value.Trim()
                Else
                    txtEquipIncluded.Text = "No"
                End If

                txtEqipOneTimeCharges.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("EquipmentOneTimeCharges").Value.Trim()
                txtEqipMonRented.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("EquipmentMonthlyCharges").Value.Trim()
                txtDelTimeLine.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("DeliveryTimeLine").Value.Trim()
                txtOrderNumber.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("OrderNumber").Value.Trim()

                Dim li3 As ListItem = drpIspOrderStatus.Items.FindByValue(objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ISPOrderStatusId").Value.Trim())
                If li3 IsNot Nothing Then
                    drpIspOrderStatus.SelectedValue = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ISPOrderStatusId").Value.Trim()
                End If



                txtOrderDate.Text = objeAAMS.ConvertDateBlank(objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("OrderDate").Value.Trim())
                txtOnlineDate.Text = objeAAMS.ConvertDateBlank(objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("OnlineDate").Value.Trim())
                txtExpOnlineDate.Text = objeAAMS.ConvertDateBlank(objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ExpectedDate").Value.Trim())
                hdApprovedBy.Value = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ApprovedBY").Value.Trim()

                txtApprovedBy.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ApprovedByName").Value.Trim()
                'drpApprovedBy.SelectedValue = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ApprovedBY").Value.Trim()
                txtLoggedby.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("LoggedByName").Value.Trim()
                '  txtCreationDate.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("CreationDate").Value.Trim()

                hdLoggedByID.Value = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("LoggedBy").Value.Trim()

                txtCafAcId.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("CAFNumber").Value.Trim()
                txtApprovalDate.Text = objeAAMS.ConvertDateBlank(objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ApprovalDate").Value.Trim())
                txtMDNNo.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("MDNNumber").Value.Trim()
                txtCanDate.Text = objeAAMS.ConvertDateBlank(objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("CancellationDate").Value.Trim())
                txtLoginName.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("LoginName").Value.Trim()
                txtCanReason.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("CancellationReason").Value.Trim()
                txtRem.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("Remarks").Value.Trim()

                hdIspPlanId.Value = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ISPPlanID").Value.Trim()
                'Dim SumTotal As Double = Val(txtInsCharges.Text) + Val(txtMonCharges.Text) + Val(txtEqipOneTimeCharges.Text) + Val(txtEqipMonRented.Text)
                'txtTotalSum.Text = SumTotal.ToString(".00")
                txtStaticIP.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("StaticIP").Value.Trim()

                '@ Code For Readonly Or Write For IspName and NPID on the basis of Users Write
                Dim objSecurityXml As New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify ISPNAME']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify ISPNAME']").Attributes("Value").Value)
                        If strBuilder(2) = "0" Then
                            drpIspName.Enabled = False
                        End If
                    End If
                Else
                    strBuilder = objeAAMS.SecurityCheck(31)
                End If
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify NPID']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify NPID']").Attributes("Value").Value)
                        If strBuilder(2) = "0" Then
                            drpPlainId.Enabled = False
                        End If
                    End If
                Else
                    strBuilder = objeAAMS.SecurityCheck(31)
                End If
                '@End of Code For Readonly Or Write For IspName and NPID on the basis of Users Write

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

            ' For New Isp Order Creation
            If Not Request.QueryString("Action") Is Nothing Then
                If Request.QueryString("Action").ToString() = "New" Then


                    objInputIspOrderXml.LoadXml("<ISP_UPDATEISPORDER_INPUT><ISPORDERDETAILS ISPOrderID='' LCODE='' ISPPlanID='' OrderNumber='' OrderDate='' CommissionedOn='' ECommissionedOn='' OrderStatusID='' LoggedBy='' ApprovedBY='' CancellationDate='' CancellationReason=''  ApprovalDate='' CircuitDeliveryDate='' CAFNumber='' UserName='' MDNNumber='' Remarks='' ISPID='' OriginalOrderNo='' StaticIP='' ></ISPORDERDETAILS></ISP_UPDATEISPORDER_INPUT>")
                    objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("ISPOrderID").InnerText = ""
                    objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("LCODE").InnerText = hdLcode.Value
                    objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("OrderNumber").InnerText = txtOrderNumber.Text
                    objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("OrderDate").InnerText = objeAAMS.ConvertTextDate(txtOrderDate.Text)
                    objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("CommissionedOn").InnerText = objeAAMS.ConvertTextDate(txtOnlineDate.Text)
                    objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("ECommissionedOn").InnerText = objeAAMS.ConvertTextDate(txtExpOnlineDate.Text)
                    objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("OrderStatusID").InnerText = drpIspOrderStatus.SelectedValue

                    objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("ApprovedBY").InnerText = hdApprovedBy.Value ' hdRespId.Value ' drpApprovedBy.SelectedValue
                    objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("CancellationDate").InnerText = objeAAMS.ConvertTextDate(txtCanDate.Text)
                    objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("CancellationReason").InnerText = txtCanReason.Text
                    objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("ApprovalDate").InnerText = objeAAMS.ConvertTextDate(txtApprovalDate.Text)
                    objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("CAFNumber").InnerText = txtCafAcId.Text
                    objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("ISPPlanID").InnerText = drpPlainId.SelectedValue ' hdIspPlanId.Value
                    objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("ISPID").InnerText = drpIspName.SelectedValue
                    'If Session("Security") IsNot Nothing Then
                    '    Dim objSecurityXml As New XmlDocument
                    '    objSecurityXml.LoadXml(Session("Security"))
                    '    objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("UserName").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Login").InnerText

                    'End If
                    objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("UserName").InnerText = txtLoginName.Text
                    If Not Session("LoginSession") Is Nothing Then
                        objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("LoggedBy").InnerText = Session("LoginSession").ToString().Split("|")(0)
                    End If

                    objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("MDNNumber").InnerText = txtMDNNo.Text
                    objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("Remarks").InnerText = txtRem.Text

                    objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("StaticIP").InnerText = txtStaticIP.Text


                    objOutputIspOrderXml = obbzISPOrder.Update(objInputIspOrderXml)

                    If objOutputIspOrderXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                        '@ Getting  Encrepted Data 
                        Dim EncreptedISPOrderID, EncryptedLcode As String
                        EncreptedISPOrderID = objED.Encrypt(objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("ISPOrderID").Value)
                        EncryptedLcode = objED.Encrypt(objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("LCODE").Value)
                        '@ End of Getting Encrepted Data


                        ' lblError.Text = objmsg.messUpdate
                        Response.Redirect("ISPUP_Order.aspx?msg=A&IspOrderId=" + EncreptedISPOrderID + "&Lcode=" + EncryptedLcode, False)
                        'Response.Redirect("ISPUP_Order.aspx?msg=A&IspOrderId=" + objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("ISPOrderID").Value + "&Lcode=" + objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("LCODE").Value, False)
                    Else
                        lblError.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                        Exit Sub
                    End If
                End If
            End If


            ' For Ediding the existing Isp Order 
            If Request.QueryString("IspOrderId") IsNot Nothing Then

                If (rdlNewCancel.SelectedValue = "t") Then

                End If




                objInputIspOrderXml.LoadXml("<ISP_UPDATEISPORDER_INPUT><ISPORDERDETAILS ISPOrderID='' LCODE='' ISPPlanID='' OrderNumber='' OrderDate='' CommissionedOn='' ECommissionedOn='' OrderStatusID='' LoggedBy='' ApprovedBY='' CancellationDate='' CancellationReason=''  ApprovalDate='' CircuitDeliveryDate='' CAFNumber='' UserName='' MDNNumber='' Remarks='' ISPID='' OriginalOrderNo='' StaticIP='' ></ISPORDERDETAILS></ISP_UPDATEISPORDER_INPUT>")

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
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("CommissionedOn").InnerText = objeAAMS.ConvertTextDate(txtOnlineDate.Text)
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("ECommissionedOn").InnerText = objeAAMS.ConvertTextDate(txtExpOnlineDate.Text)
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("OrderStatusID").InnerText = drpIspOrderStatus.SelectedValue
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("LoggedBy").InnerText = hdLoggedByID.Value.Trim()
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("ApprovedBY").InnerText = hdApprovedBy.Value 'drpApprovedBy.SelectedValue
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("CancellationDate").InnerText = objeAAMS.ConvertTextDate(txtCanDate.Text)
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("CancellationReason").InnerText = txtCanReason.Text
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("ApprovalDate").InnerText = objeAAMS.ConvertTextDate(txtApprovalDate.Text)
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("CAFNumber").InnerText = txtCafAcId.Text
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("ISPID").InnerText = drpIspName.SelectedValue

                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("StaticIP").InnerText = txtStaticIP.Text

                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("ISPPlanID").InnerText = drpPlainId.SelectedValue ' hdIspPlanId.Value
                'If Session("Security") IsNot Nothing Then
                '    Dim objSecurityXml As New XmlDocument
                '    objSecurityXml.LoadXml(Session("Security"))
                '    objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("UserName").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Login").InnerText

                'End If
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("UserName").InnerText = txtLoginName.Text
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("MDNNumber").InnerText = txtMDNNo.Text
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDERDETAILS").Attributes("Remarks").InnerText = txtRem.Text

                objOutputIspOrderXml = obbzISPOrder.Update(objInputIspOrderXml)

                If objOutputIspOrderXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    lblError.Text = objmsg.messUpdate

                    ViewIspOrder(DecreptedISPOrderID, strErrormsg)

                    'ViewIspOrder(Request.QueryString("IspOrderId"), strErrormsg)
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
            hdAgencyNameId.Value = ""
            lblError.Text = ""
            txtCity.Text = ""
            txtCountry.Text = ""
            txtPhone.Text = ""
            txtFax.Text = ""
            txtOfficeId.Text = ""

            ' txtIsp.Text = ""
            drpIspName.SelectedValue = ""

            txtISPCityName.Text = ""

            '  txtNEWNPID.Text = ""
            drpPlainId.SelectedValue = ""

            txtBandWidth.Text = ""
            txtInsCharges.Text = ""
            txtMonCharges.Text = ""
            txtEquipIncluded.Text = ""
            txtEqipOneTimeCharges.Text = ""
            txtEqipMonRented.Text = ""
            txtDelTimeLine.Text = ""
            txtOrderNumber.Text = ""
            drpIspOrderStatus.SelectedValue = ""
            txtOrderDate.Text = ""
            txtOnlineDate.Text = ""
            txtExpOnlineDate.Text = ""
            txtLoggedby.Text = ""
            hdApprovedBy.Value = "" ' objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("ApprovedBY").Value.Trim()
            txtApprovedBy.Text = ""
            'drpApprovedBy.SelectedIndex = 0
            txtCafAcId.Text = ""
            txtApprovalDate.Text = ""
            txtMDNNo.Text = ""
            txtCanDate.Text = ""
            txtLoginName.Text = ""
            txtCanReason.Text = ""
            txtRem.Text = ""
            hdIspPlanId.Value = ""
            hdLcode.Value = ""
            Dim strErrormsg As String = ""
            If Not Request.QueryString("IspOrderId") Is Nothing Then

                '@ Getting  Decrepted Data 
                Dim DecreptedISPOrderID, DecreptedLcode As String
                DecreptedISPOrderID = objED.Decrypt(Request.QueryString("IspOrderId").ToString())
                DecreptedLcode = objED.Decrypt(Request.QueryString("Lcode").ToString())

                '@ End of Getting Decrepted Data
                ViewIspOrder(DecreptedISPOrderID, strErrormsg)

                'ViewIspOrder(Request.QueryString("IspOrderId").ToString(), strErrormsg)
                If strErrormsg.Trim.Length > 0 Then
                    lblError.Text = strErrormsg
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub drpIspName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpIspName.SelectedIndexChanged
        GetNPID()
        getAgencyDetails()
    End Sub
    Private Sub GetNPID()

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        drpPlainId.Items.Clear()
        Try
            Dim objbzISP As New AAMS.bizISP.bzISPPlan
            objInputXml.LoadXml("<IS_SEARCHISPPLAN_INPUT><ISPID></ISPID><Name></Name><CityID></CityID><NPID></NPID><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><ProviderID/></IS_SEARCHISPPLAN_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("ISPID").InnerText = drpIspName.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "NPID"
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            objOutputXml = objbzISP.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                'drpPlainId.DataSource=objOutputXml
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                drpPlainId.DataSource = ds.Tables("ISPPLAN")
                drpPlainId.DataTextField = "NPID"
                drpPlainId.DataValueField = "ISPPlanID"
                drpPlainId.DataBind()
            End If
        Catch ex As Exception
        Finally
            Dim li As ListItem
            li = drpPlainId.Items.FindByValue("")
            If li Is Nothing Then
                drpPlainId.Items.Insert(0, New ListItem("--Select one--", ""))
            End If
        End Try
    End Sub

    Sub BindDropDownIspName(ByVal drpDownList As DropDownList, ByVal strType As String, ByVal bolSelect As Boolean)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim objInputXmlAgency, objOutputXmlAgency As New XmlDocument
        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
        Dim objbzISP As New AAMS.bizISP.bzISP
        Dim ds As New DataSet
        drpDownList.Items.Clear()
        Select Case strType
            Case "ISPLISTByAgencyCity"

                objInputXml.LoadXml("<IS_LISTISP_INPUT><CITY></CITY></IS_LISTISP_INPUT>")
                'objInputXml.DocumentElement.SelectSingleNode("CITY").InnerXml = .Attributes("CITY").Value()
                objOutputXml = New XmlDocument
                objOutputXml = objbzISP.List1(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("ISP")
                    drpDownList.DataTextField = "Name"
                    drpDownList.DataValueField = "ISPID"
                    drpDownList.DataBind()
                End If
                If bolSelect = True Then
                    drpDownList.Items.Insert(0, New ListItem("", ""))
                End If
        End Select
        '  IspNameEnableorDisable()
    End Sub

    Private Sub getAgencyDetails()

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
        Try
            If hdAgencyNameId.Value.Trim.Length > 0 Then
                objInputXml.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerXml = hdAgencyNameId.Value

                hdLcode.Value = hdAgencyNameId.Value
                objOutputXml = objbzAgency.View(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    With objOutputXml.DocumentElement.SelectSingleNode("Agency")
                        txtAgencyName.Text = .Attributes("NAME").Value()
                        txtAgencyAddress.Text = .Attributes("ADDRESS").Value()
                        txtCity.Text = .Attributes("CITY").Value()
                        txtCountry.Text = .Attributes("COUNTRY").Value()
                        txtPhone.Text = .Attributes("PHONE").Value()
                        txtFax.Text = .Attributes("FAX").Value()
                        txtOfficeId.Text = .Attributes("OFFICEID").Value()
                    End With
                End If
            End If

            ' hdAgencyNameId.Value
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdPrevISpOrder_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdPrevISpOrder.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            BindPrevieouoOder()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            BindPrevieouoOder()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            BindPrevieouoOder()
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
        BindPrevieouoOder()
    End Sub
#End Region

    Private Function GetSortColumnIndex(ByVal grd As GridView) As Int16
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Return grd.Columns.IndexOf(field)
            End If
        Next
        Return 0
    End Function
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

    Private Sub BindPrevieouoOder()
        pnlPrevIspOrder.Visible = True
        pnlIspOrder.Visible = False
        btnIspOrder.CssClass = "headingtabactive"
        btnPrevIspDetails.CssClass = "headingtab"
        BtnSendingMail.CssClass = "headingtabactive"

        If Request.QueryString("Lcode") IsNot Nothing Then
            Dim objXmlReader As XmlNodeReader
            Dim dset As New DataSet
            Dim objNode As XmlNode
            Dim objInputIspOrderXml, objOutputIspOrderXml As New XmlDocument
            Dim objbzIspOrder As New AAMS.bizISP.bzISPOrder

            Try
                objInputIspOrderXml.LoadXml("<ISP_PREVIOUSORDER_INPUT><LCODE ></LCODE> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></ISP_PREVIOUSORDER_INPUT>")
                '@ Getting  Decrrepted Data 
                Dim DecreptedLcode As String
                DecreptedLcode = objED.Decrypt(Request.QueryString("Lcode").ToString())
                '@ End of Getting Decrepted Data
                objInputIspOrderXml.DocumentElement.SelectSingleNode("LCODE").InnerText = DecreptedLcode

                'objInputIspOrderXml.DocumentElement.SelectSingleNode("LCODE").InnerText = Request.QueryString("Lcode")

                ' objOutputIspOrderXml.LoadXml("<ISP_PREVIOUSORDER_OUTPUT><ORDERDETAILS OrderNumber='' ISPName='' AgencyName='' Address='' OfficeId='' City='' ApprovedDate='' OrderDate='' OnlineDate='' StatusName='' /></ISP_PREVIOUSORDER_OUTPUT>")


                'Start CODE for sorting and paging

                If ViewState("PrevSearching") Is Nothing Then
                    objInputIspOrderXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                Else
                    Dim objTempInputXml As New XmlDocument
                    Dim objNodeList As XmlNodeList

                    objTempInputXml.LoadXml(ViewState("PrevSearching"))
                    objNodeList = objTempInputXml.DocumentElement.ChildNodes
                    objInputIspOrderXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                    For Each objNode1 As XmlNode In objNodeList
                        If objNode1.Name <> "PAGE_NO" And objNode1.Name <> "SORT_BY" And objNode1.Name <> "DESC" And objNode1.Name <> "PAGE_SIZE" Then
                            If objNode1.InnerText <> objInputIspOrderXml.DocumentElement.SelectSingleNode(objNode1.Name.ToString).InnerText Then
                                objInputIspOrderXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                                ddlPageNumber.SelectedValue = "1"
                            End If
                        End If
                    Next
                End If


                objInputIspOrderXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString


                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "OrderNumber"
                    objInputIspOrderXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "OrderNumber" '"LOCATION_CODE"
                Else
                    objInputIspOrderXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
                End If

                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                    objInputIspOrderXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                Else
                    objInputIspOrderXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
                End If

                'End Code for paging and sorting

                objOutputIspOrderXml = objbzIspOrder.GetPreviousISPOrder(objInputIspOrderXml)
                If objOutputIspOrderXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    For Each objNode In objOutputIspOrderXml.DocumentElement.SelectNodes("ORDERDETAILS")
                        objNode.Attributes("ApprovedDate").Value = objeAAMS.ConvertDateBlank(objNode.Attributes("ApprovedDate").Value)
                        objNode.Attributes("OrderDate").Value = objeAAMS.ConvertDateBlank(objNode.Attributes("OrderDate").Value)
                        objNode.Attributes("OnlineDate").Value = objeAAMS.ConvertDateBlank(objNode.Attributes("OnlineDate").Value)
                    Next

                    objXmlReader = New XmlNodeReader(objOutputIspOrderXml)
                    dset.ReadXml(objXmlReader)


                    ViewState("PrevSearching") = objInputIspOrderXml.OuterXml
                    lblError.Text = ""
                    grdPrevISpOrder.DataSource = dset.Tables("ORDERDETAILS").DefaultView
                    grdPrevISpOrder.DataBind()

                    'txtRecordOnCurrReco.Text = grdvIpPoolHistory.Rows.Count.ToString()
                    PagingCommon(objOutputIspOrderXml)

                    Dim intcol As Integer = GetSortColumnIndex(grdPrevISpOrder)
                    If ViewState("Desc") = "FALSE" Then
                        grdPrevISpOrder.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                    End If
                    If ViewState("Desc") = "TRUE" Then
                        grdPrevISpOrder.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                    End If


                    lblError.Text = ""
                Else
                    grdPrevISpOrder.DataSource = String.Empty
                    grdPrevISpOrder.DataBind()
                    pnlPaging.Visible = False
                    lblError.Text = objOutputIspOrderXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If

            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
        End If
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            If Not Request.QueryString("IspOrderId") Is Nothing Then
                '@ Getting  Decrrepted Data 
                Dim DecreptedIspOrderId As String
                DecreptedIspOrderId = objED.Decrypt(Request.QueryString("IspOrderId").ToString())
                '@ End of Getting Decrepted Data             

                Dim objInputIspOrderXml, objOutputIspOrderXml As New XmlDocument
                Dim objbzIspOrder As New AAMS.bizISP.bzISPOrder
                objInputIspOrderXml.LoadXml("<ISP_VIEWORDER_INPUT><ISPOrderID/></ISP_VIEWORDER_INPUT>")
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPOrderID").InnerText = DecreptedIspOrderId
                ' objOutputIspOrderXml.LoadXml("<ISP_VIEWISP_OUTPUT><ISPORDER ISPOrderID='' CreationDate='' LCODE='' Agencyname='' Address='' City='' Country='' Phone='' Fax='' OfficeId='' ISPName='' ISPCityName='' ISPPlanID='' BandWidth='' InstallationCharges='' MonthlyCharges='' EquipmentInclude='' EquipmentOneTimeCharges='' EquipmentMonthlyCharges='' DeliveryTimeLine='' OrderNumber='' OrderDate='' OnlineDate='' ExpectedDate='' ISPOrderStatusId='' LoggedBy='' ApprovedBY='' ApprovalDate='' CAFNumber='' CancellationDate='' MDNNumber='' LoginName='' CancellationReason='' Remarks=''/><Errors Status='FALSE'><Error Code='' Description=''/></Errors></ISP_VIEWISP_OUTPUT>")
                objOutputIspOrderXml = objbzIspOrder.View(objInputIspOrderXml)

                If objOutputIspOrderXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("CancellationDate").InnerText = (objeAAMS.ConvertDateBlank(objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("CancellationDate").Value.Trim()))

                    objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("OnlineDate").InnerText = (objeAAMS.ConvertDateBlank(objOutputIspOrderXml.DocumentElement.SelectSingleNode("ISPORDER").Attributes("OnlineDate").Value.Trim()))

                    Dim objExport As New ExportExcel
                    Dim strArray() As String = {"Agency Name", "Agency Address", "City", "Office ID", "NPID", "BandWidth", "Online Date", "Cancellation Date", "A/c Id", "WLL Number", "Static IP", "Cancellation Reason", "Remarks"}
                    Dim intArray() As Integer = {2, 3, 4, 8, 12, 13, 22, 30, 29, 32, 38, 34, 35}
                    '<ISPORDER 
                    'ISPOrderID = "107"
                    ' LCODE="22168" 
                    'Agencyname="(Gsa) Carsons Airline Services (Pvt) Ltd" 
                    'Address="65c, Dharmapala Mawatha" 
                    'City="Colombo" 
                    'Country = "Srilanka"
                    ' Phone="2337662"
                    'Fax = "2439766"
                    ' OfficeId="CMBVS3394"
                    ' ISPName="DISHNET DSL"
                    ' ISPCityName="" 
                    'ISPPlanID="37"
                    ' NPID="TEL7890435"
                    ' BandWidth="256KB" 
                    'InstallationCharges="1200.00" 
                    'MonthlyCharges="0.00"
                    ' EquipmentInclude="False" 
                    'EquipmentOneTimeCharges="0.00"
                    ' EquipmentMonthlyCharges="0.00"
                    ' DeliveryTimeLine="4" 
                    'OrderNumber="2008/9/2" 
                    'OrderDate="20080918" 
                    'OnlineDate="" 
                    'ExpectedDate = ""
                    ' ISPOrderStatusId="5"
                    ' LoggedBy="24"
                    'ApprovedBY = ""
                    ' ApprovedByName=""
                    ' ApprovalDate=""
                    ' CAFNumber=""
                    ' CancellationDate=""
                    ' CreationDate=""
                    ' MDNNumber="" 
                    'LoginName = "admin"
                    'CancellationReason=""
                    ' Remarks="" 
                    'ISPID = "3"
                    'OriginalOrderNo="" 
                    'StaticIP="192.168.2.37" /> 

                    objExport.ExportDetails(objOutputIspOrderXml, "ISPORDER", intArray, strArray, ExportExcel.ExportFormat.Excel, "ISPORDER.xls")

                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
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

    Protected Sub BtnSendingMail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSendingMail.Click
        Try
            'pnlPrevIspOrder.Visible = True
            'pnlIspOrder.Visible = False
            ' btnIspOrder.CssClass = "headingtabactive"
            '   btnPrevIspDetails.CssClass = "headingtab"
            '  BtnSendingMail.CssClass = "headingtabactive"


            If Request.QueryString("IspOrderId").ToString() IsNot Nothing And Request.QueryString("Lcode").ToString() IsNot Nothing Then
                ' Dim DecreptedISPOrderID As String
                ' DecreptedISPOrderID = objED.Decrypt(Request.QueryString("IspOrderId").ToString())

                '@ Getting  Decrrepted Data 
                Dim DecreptedLcode As String
                DecreptedLcode = objED.Decrypt(Request.QueryString("Lcode").ToString())
                '@ End of Getting Decrepted Data

                Response.Redirect("ISP_OrderSendMail.aspx?Action=U|&Id=7&IspOrderId=" + Request.QueryString("IspOrderId") + "&Lcode=" + Request.QueryString("Lcode").ToString(), False)

            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
