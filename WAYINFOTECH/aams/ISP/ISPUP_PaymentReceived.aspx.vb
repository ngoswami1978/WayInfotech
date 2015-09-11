'#############################################################
'############   Page Name -- ISPUP_PaymentReceived   ########
'############   Date 20-December 2007  #######################
'############   Developed By Abhishek  #######################
'#############################################################
Partial Class ISP_ISPUP_PaymentReceived
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim StrError As String = ""
            Session("PageName") = Request.Url.ToString()
            objeAAMS.ExpirePageCache()

            btnSave.Attributes.Add("onclick", "return CheckValidation();")
            '  btnReset.Attributes.Add("onclick", "return IspOrderReset();")
            lblError.Text = String.Empty
            '***************************************************************************************
            'Code of Security Check
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If
            '  tbl.Width = "610px"
            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ISP Payment Received']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ISP Payment Received']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                    End If
                    If strBuilder(2) = "0" Then
                        btnSave.Enabled = False
                    End If

                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            If (Not IsPostBack) Then
                objeAAMS.BindDropDown(drpSentToAc, "EmplolyeeOfAccount", True)
                objeAAMS.BindDropDown(drpRecToAc, "EmplolyeeOfAccount", True)
                '  objeAAMS.BindDropDown(drpChequeSentTo, "EMPLOYEE", True)

                If (Request("NewFirtTime") IsNot Nothing) Then
                    CallDoPayment()
                End If

                If (Request("TotalAmt") IsNot Nothing) Then
                    txtAmt.Text = Request("TotalAmt").ToString()
                End If
                If (Request.QueryString("Action") IsNot Nothing) Then
                    If (Request.QueryString("Action").ToString() = "U") Then
                        If (Request.QueryString("PANumber") IsNot Nothing) Then
                            ' tbl.Width = "866px"
                            ViewPaymentReceived(Request.QueryString("PANumber").ToString(), StrError)
                            If StrError.Length > 0 Then
                                lblError.Text = StrError
                                Exit Sub
                            End If
                            'txtPANO.Text = Request("PANumber").ToString()
                        End If
                    End If
                End If
            End If
            If (Request("Msg") IsNot Nothing) Then
                lblError.Text = objeAAMSMessage.messInsert
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub ViewPaymentReceived(ByVal strPANumber As String, ByRef StrError As String)
        Dim objInputXml, objOutputXml As New XmlDocument

        Try
            Dim objbzISPPayment As New AAMS.bizISP.bzISPPayment
            'Lcode = Session("LCODE")
            objInputXml.LoadXml("<IS_VIEWPAYMENTRECEIVED_INPUT><PANumber /></IS_VIEWPAYMENTRECEIVED_INPUT>")
            ' @ Code for Viwing The records")
            objInputXml.DocumentElement.SelectSingleNode("PANumber").InnerText = strPANumber
            'Here Back end Method Call
            objOutputXml = objbzISPPayment.View(objInputXml)
            ' objOutputXml.LoadXml("<IS_VIEWPAYMENTRECEIVED_OUTPUT> <PAYMENTRECEIVED PANumber='' PAMonth='' PAYear='' DTPASentToAccount=''	DTPAReceivedInAccount='' ChequeNumber='' ChequeDate='' ChequeAmount='' 	ChequeSentTo='' Remarks='' PADTTICreated='' PACreatedBy='' />		<Errors Status=''>		<Error Code='' Description='' />	</Errors></IS_VIEWPAYMENTRECEIVED_OUTPUT>")

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                txtPANO.Text = objOutputXml.DocumentElement.SelectSingleNode("PAYMENTRECEIVED").Attributes("PANumber").InnerText

                drpSentToAc.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("PAYMENTRECEIVED").Attributes("DTPASentToAccount").InnerText
                drpRecToAc.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("PAYMENTRECEIVED").Attributes("DTPAReceivedInAccount").InnerText
                hdMonth.Value = objOutputXml.DocumentElement.SelectSingleNode("PAYMENTRECEIVED").Attributes("PAMonth").InnerText
                hdYear.Value = objOutputXml.DocumentElement.SelectSingleNode("PAYMENTRECEIVED").Attributes("PAYear").InnerText
                txtChqNo.Text = objOutputXml.DocumentElement.SelectSingleNode("PAYMENTRECEIVED").Attributes("ChequeNumber").InnerText
                txtChqDate.Text = objeAAMS.ConvertDateBlank(objOutputXml.DocumentElement.SelectSingleNode("PAYMENTRECEIVED").Attributes("ChequeDate").InnerText)
                txtAmt.Text = objOutputXml.DocumentElement.SelectSingleNode("PAYMENTRECEIVED").Attributes("ChequeAmount").InnerText
                txtRemark.Text = objOutputXml.DocumentElement.SelectSingleNode("PAYMENTRECEIVED").Attributes("Remarks").InnerText
                '   drpChequeSentTo.Text = objOutputXml.DocumentElement.SelectSingleNode("PAYMENTRECEIVED").Attributes("ChequeSentTo").InnerText
                txtEmployeeName.Text = objOutputXml.DocumentElement.SelectSingleNode("PAYMENTRECEIVED").Attributes("CHEQUESENTTOEMP").InnerText
                hdEmployeeID.Value = objOutputXml.DocumentElement.SelectSingleNode("PAYMENTRECEIVED").Attributes("ChequeSentTo").InnerText

                ' txtCreatedBy.Text = objOutputXml.DocumentElement.SelectSingleNode("PAYMENTRECEIVED").Attributes("PACreatedBy").InnerText
              
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                StrError = lblError.Text
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            StrError = ex.Message
        End Try
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim objInputXml As New XmlDataDocument
            Dim objOutputXml As New XmlDocument
            Dim strISPOrderID As String = ""
            Dim objPaymentReceived As XmlNode
            Dim objbzISPPayment As New AAMS.bizISP.bzISPPayment
            If (IsValid) Then

                objInputXml.LoadXml("<IS_UPDATEPAYMENTRECEIVED_INPUT><PAYMENTRECEIVED PANumber='' PAMonth='' PAYear='' DTPASentToAccount='' DTPAReceivedInAccount='' ChequeNumber='' ChequeDate='' ChequeAmount='' ChequeSentTo='' Remarks='' PADTTICreated='' PACreatedBy='' ISPOrderID='' /></IS_UPDATEPAYMENTRECEIVED_INPUT>")
                objPaymentReceived = objInputXml.DocumentElement.SelectSingleNode("PAYMENTRECEIVED")
                With objPaymentReceived
                   
                    If (drpSentToAc.SelectedIndex <> 0) Then
                        .Attributes("DTPASentToAccount").Value() = drpSentToAc.SelectedValue
                    End If
                    If (drpRecToAc.SelectedIndex <> 0) Then
                        .Attributes("DTPAReceivedInAccount").Value() = drpRecToAc.SelectedValue
                    End If
                    If (txtChqDate.Text.Trim().Length > 0) Then
                        .Attributes("ChequeDate").Value() = objeAAMS.ConvertTextDate(txtChqDate.Text)
                    End If
                    'If (drpChequeSentTo.SelectedIndex <> 0) Then
                    '    .Attributes("ChequeSentTo").Value() = drpChequeSentTo.SelectedValue
                    'End If

                    ' If (hdEmployeeID.Value <> "") Then
                    .Attributes("ChequeSentTo").Value() = hdEmployeeID.Value 'drpChequeSentTo.SelectedValue
                    ' End If



                    .Attributes("ChequeNumber").Value() = txtChqNo.Text
                    .Attributes("ChequeAmount").Value() = txtAmt.Text
                    .Attributes("Remarks").Value() = txtRemark.Text
                    If (Request.QueryString("Month") IsNot Nothing) Then
                        .Attributes("PAMonth").Value() = Request.QueryString("Month").ToString()
                    End If
                    If (Request.QueryString("Year") IsNot Nothing) Then
                        .Attributes("PAYear").Value() = Request.QueryString("Year").ToString() '"2008"
                    End If


                    .Attributes("PADTTICreated").Value() = ""

                    If Not Session("LoginSession") Is Nothing Then
                        .Attributes("PACreatedBy").Value() = Session("LoginSession").ToString().Split("|")(0)
                    End If
                End With

                ' Exit Sub
                '#####################################
                '@ Case For Editing the Do Payment Records
                If (Request.QueryString("Action") IsNot Nothing) Then
                    If (Request.QueryString("Action").ToString() = "U") Then
                        If (Request.QueryString("PANumber") IsNot Nothing) Then
                            If Request.QueryString("Action") = "U" Then
                                With objPaymentReceived
                                    .Attributes("PANumber").Value() = Request.QueryString("PANumber")
                                    .Attributes("PAMonth").Value() = hdMonth.Value '"2"
                                    .Attributes("PAYear").Value() = hdYear.Value  '"2008"
                                End With
                                'Here Back end Method Call
                                objOutputXml = objbzISPPayment.Update(objInputXml)
                                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                                    ' objOutputXml = objbzISPPayment.Update(objInputXml)
                                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                                        lblError.Text = objeAAMSMessage.messUpdate
                                        'ViewPaymentReceived(Request.QueryString("PANumber").ToString(), StrError)
                                        txtEmployeeName.Text = Request("txtEmployeeName")
                                    Else
                                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                                    End If
                                Else
                                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                                End If

                            End If
                        End If
                    End If
                End If

                ' ##########################################
                ' Case For Do Payment New Mode
                '###########################################
                If (Request.QueryString("Action") Is Nothing) Then
                    If (Request("TotalAmt") IsNot Nothing) Then
                        If (Request("ISPOrderID") IsNot Nothing) Then
                            strISPOrderID = Request("ISPOrderID").ToString()
                            With objPaymentReceived
                                .Attributes("ISPOrderID").Value() = strISPOrderID
                            End With

                            'Here Back end Method Call
                            objOutputXml = objbzISPPayment.Update(objInputXml)
                            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                                lblError.Text = objeAAMSMessage.messInsert
                                ' ViewPaymentReceived(Request.QueryString("PANumber").ToString(), StrError)
                                Response.Redirect("ISPUP_PaymentReceived.aspx?Msg=A&Action=U&PANumber=" + objOutputXml.DocumentElement.SelectSingleNode("PAYMENTRECEIVED").Attributes("PANumber").Value, False)
                            Else
                                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            End If
                        Else
                            lblError.Text = "Isp OrderId Does not exist."
                        End If
                    Else
                        lblError.Text = "Amount Can't be Zero."
                    End If
                End If

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            lblError.Text = ""
            Dim StrError As String = ""
            drpRecToAc.SelectedIndex = 0
            drpSentToAc.SelectedIndex = 0
            ' drpChequeSentTo.SelectedIndex = 0
            hdEmployeeID.Value = ""
            txtEmployeeName.Text = ""
            If (Request("TotalAmt") IsNot Nothing) Then
                txtAmt.Text = Request("TotalAmt").ToString()
            End If
            If (Request.QueryString("Month") IsNot Nothing) Then
                hdMonth.Value = Request.QueryString("Month").ToString()
            End If
            If (Request.QueryString("Year") IsNot Nothing) Then
                hdYear.Value = Request.QueryString("Year").ToString()
            End If
            If (Request.QueryString("Action") IsNot Nothing) Then
                If (Request.QueryString("Action").ToString() = "U") Then
                    If (Request.QueryString("PANumber") IsNot Nothing) Then
                        ViewPaymentReceived(Request.QueryString("PANumber").ToString(), StrError)
                        If StrError.Length > 0 Then
                            lblError.Text = StrError
                            Exit Sub
                        End If

                    End If
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


    Private Sub CallDoPayment()
        Dim strAmt As String
        Dim strMonths As String
        Dim strYears As String

        Dim strISPOrderID As String = ""
        Dim i As Integer
        Dim TotalAmt As Decimal = 0
        Dim objOutputIspOrderSearchXml As New XmlDocument
        If Session("PaymentRecDataSource") IsNot Nothing Then
            Dim dset As DataSet = CType(Session("PaymentRecDataSource"), DataSet)
            objOutputIspOrderSearchXml.LoadXml(dset.GetXml())
            '  gvIspPaymentRec.PageIndex = CInt(ddlPageNumber.SelectedValue) - 1
            For Each objxmlnode As XmlNode In objOutputIspOrderSearchXml.DocumentElement.SelectNodes("PAYMENTPROCEED[@CheckUncheckStatus ='True' and @PANumber='']")
                '("TARGET[@LCode='" + strlcode + "' and @Year='" + strYear + "' and @Month='" + strMonth + "' ]")
                '   For Each objxmlnode As XmlNode In objOutputIspOrderSearchXml.DocumentElement.SelectNodes("PAYMENTPROCEED[@CheckUncheckStatus ='True']")
                strAmt = objxmlnode.Attributes("Amount").Value

                strMonths = objxmlnode.Attributes("CostActivityMonth").Value

                strYears = objxmlnode.Attributes("Year").Value
                If (strAmt.ToString.Length > 0) Then
                    TotalAmt += Convert.ToDecimal(strAmt)
                    If (strISPOrderID.Trim().Length = 0) Then
                        strISPOrderID = objxmlnode.Attributes("ISPOrderID").Value
                    Else
                        strISPOrderID += "|" + objxmlnode.Attributes("ISPOrderID").Value
                    End If
                End If
            Next
            'If (strISPOrderID.Trim().Length = 0) Then
            '    lblError.Text = "There is no item selected to do Payment."
            '    Exit Sub
            'End If

            Response.Redirect("ISPUP_PaymentReceived.aspx?TotalAmt=" & TotalAmt & "&ISPOrderID=" & strISPOrderID & "&Month=" & strMonths & "&Year=" & strYears, False)

            'ClientScript.RegisterStartupScript(Me.GetType(), "Open1", "<script language='javascript'>" & _
            '                     "PopupWindow=window.open('ISPUP_PaymentReceived.aspx?TotalAmt=" & TotalAmt & "&ISPOrderID=" & strISPOrderID & "&Month=" & hdMonth.Value & "&Year=" & hdYear.Value & "','IspPay','height=400px,width=830px,top=150,left=150,scrollbars=0,status=1')" & _
            '                     ";PopupWindow.focus();</script>")

        End If
    End Sub
End Class
