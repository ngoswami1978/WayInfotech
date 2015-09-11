Imports System.Xml
Imports System.Data
Partial Class TravelAgency_MSUP_Order
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
    Public flag As Boolean  ''ashish 

#Region "Code for Filter "
    'Code Added by M. K
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    'Code Added by M. K
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblError.Text = ""
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            ' This code is used for Expiration of Page From Cache
            '  objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            ' txtOfficeID1.Attributes.Add("onkeypress", "return ChkApprovalDateMainOrder();")
            txtOfficeID1.Attributes.Add("readonly", "readonly")
            btnHistory.Attributes.Add("onclick", "return PopupHistoryPageMainOrder();")
            btnViewDocument.Attributes.Add("onclick", "return ViewOrderDocMainOrder();")
            'drpIspList.Attributes.Add("onchange", "return ResetNPIdMainOrder();")

            Session("EmailList") = Nothing


           


            If hdOfficeID.Value <> "" Then
                txtOfficeID1.Text = hdOfficeID.Value

            End If
            If hdAddress.Value <> "" Then
                txtAgencyAddress.Text = hdAddress.Value
                txtAgencyName.Text = Request("txtAgencyName")
                ' txtOfficeID1.Text = Request("txtOfficeID1")

                'hdOfficeID.Value = Request("txtOfficeID1")
            End If
          

            If drpIspList.Enabled = False Then
                drpIspList.Enabled = True
                drpIspList.DataSource = String.Empty
                drpIspList.DataTextField = String.Empty
            End If

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If

            GetCompanyVertical(0)

            If Not Page.IsPostBack Then

                'Function is written for getting Number of days for Expected Installation Date


                objeAAMS.BindDropDown(ddlOrderStatus, "ORDERSTATUS", True)

                'Changed by Mukund on 26th August
                AssignHiddApprovedValu()
                'End of Changes by Mukund on 26th August

                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "I" Then
                    btnHistory.Disabled = True
                    btnViewDocument.Enabled = False
                    drpIspList.Enabled = False
                    drpPlainId.Enabled = False
                End If


                If rdlNewCancel.SelectedValue = "T" Then
                    bindOrderStatusNew()
                    ''ashish added on 09-02-2011
                    pnlATID.Visible = True
                    txtATID.Visible = True

                Else
                    ''ashish added on 09-02-2011
                    bindOrderStatusCancellation()
                    pnlATID.Visible = False
                    txtATID.Visible = False

                End If

                '@ Added By Abhishek

                '@ End  Added By Abhishek

                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Then
                    ViewOrder()
                    rdlNewCancel.Enabled = False
                    GetCompanyVertical(1)
                End If
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                    lblError.Text = objeAAMSMessage.messInsert
                    rdlNewCancel.Enabled = False
                    ViewOrder()
                    GetCompanyVertical(1)
                End If

                AssignOrderTypeDays()

       
            End If

            If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "I" Then
                btnHistory.Disabled = True
                btnViewDocument.Enabled = False
                'drpIspList.Enabled = False
                ' drpPlainId.Enabled = False
            Else
                btnHistory.Disabled = False
                btnViewDocument.Enabled = True
                Session("orderIDVal") = objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString().ToUpper())
            End If





            '*********************Security Segment**************************************************
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order']").Attributes("Value").Value)
                End If

                If strBuilder(0) = "0" Then
                    Response.Redirect("~/NoRights.aspx")
                    Exit Sub
                End If

                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    'btnSave.Enabled = False
                End If

                If strBuilder(2) = "0" Then
                   btnSave.Enabled = False
                End If

            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If


            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='View Order Documents']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='View Order Documents']").Attributes("Value").Value)
                End If
                If Not strBuilder Is Nothing Then
                    If strBuilder(0) = "0" Then
                        btnViewDocument.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            CheckSecurity()

          

            '*********************End of Security Segment*****************************************************
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    ''******************************************ASHISH WRITTEN ON 10-02-2011********************************
    Public Function checkAlphaNumeric(ByVal strInputText As String) As Boolean
        Dim intCounter As Integer
        Dim strCompare As String
        Dim strInput As String

        strInputText = strInputText.Replace(ControlChars.NewLine, "")  ''ignore the new line

        checkAlphaNumeric = False

        For intCounter = 1 To Len(strInputText)
            strCompare = Mid$(strInputText, intCounter, 1)
            strInput = Mid$(strInputText, intCounter + 1, Len(strInputText))
            If strCompare Like ("[A-Z]") Or strCompare Like ("[a-z]") Or strCompare Like ("[0-9]") Or strCompare Like (",") Then
                checkAlphaNumeric = True
                flag = True
            Else
                checkAlphaNumeric = False
                flag = False
                lblError.Text = "Special Characters are not allow"
                Exit Function
            End If
        Next intCounter
    End Function
    Public Function checklen(ByVal strInputText As String) As Boolean
        strInputText = strInputText.Replace(ControlChars.NewLine, "")  ''ignore the new line

        checklen = False
        If strInputText.Length = 0 Then 'Or (strInputText.Length >= 7 And strInputText.Length <= 20) Then
            lblError.Text = "ATID is Mandatory"
            checklen = False
            Exit Function
        ElseIf (strInputText.Length <= 7) Then
            lblError.Text = "Invalid ATID"
            checklen = False
            Exit Function
            '        ElseIf (strInputText.Length >= 225) Then
        ElseIf (strInputText.Length >= 1890) Then

            lblError.Text = "ATID should not exceeds 200"
            checklen = False
            Exit Function
        Else
            checklen = True
            flag = True
        End If

    End Function


    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If (Not Request.QueryString("Action") = Nothing) Then
                'If (txtOfficeID1.Text.Trim().Length <= 0) Then
                '    lblError.Text = "Office ID is mandatory. "
                '    Exit Sub
                'End If

                If (hdOrderApproved.Value.Trim = ddlOrderStatus.SelectedValue.Trim) Then
                    If (txtOfficeID1.Text.Trim().Length <= 0) Then
                        lblError.Text = "Office ID is mandatory. "
                        Exit Sub
                    End If
                End If






                '@ ##################################################################################
                '@ Code to check for  Isp checked/not on the basis of ordertypeid
                ' @ Code for GroupISP on the basis of Order Type
                Dim objInputXml2, objOutputXml2 As New XmlDocument
                Dim objbzOrderType As New AAMS.bizTravelAgency.bzOrderType

                Dim objInputOrderTypeXml As New XmlDataDocument, objOutputOrderTypeXml As New XmlDocument


                Dim blnGroupISP As Boolean = False
                objInputOrderTypeXml.LoadXml("<MS_VIEWORDERTYPE_INPUT><ORDERTYPEID></ORDERTYPEID></MS_VIEWORDERTYPE_INPUT>")
                If (Request("ddlOrderType") IsNot Nothing) Then
                    objInputOrderTypeXml.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText = Request("ddlOrderType")
                Else
                    objInputOrderTypeXml.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText = ddlOrderType.SelectedValue
                End If

                'Here Back end Method Call
                objOutputOrderTypeXml = objbzOrderType.View(objInputOrderTypeXml)
                If objOutputOrderTypeXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    blnGroupISP = objOutputOrderTypeXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsISPOrder").Value
                End If

                'bindOrderStatusNew() bindOrderStatusCancellation()
                If (blnGroupISP = True) Then
                    'txtPlainId.Text = hdNPID.Value

                    If drpIspList.SelectedIndex <= 0 Then
                        lblError.Text = "For the selected Order Type ISP Name is mandatory."
                        If rdlNewCancel.SelectedIndex = 0 Then
                            bindOrderStatusNew()
                        Else
                            bindOrderStatusCancellation()
                        End If
                        ddlOrderType.SelectedValue = objInputOrderTypeXml.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText
                        drpIspList.Focus()
                        Exit Sub
                    End If

                    If Request("drpPlainId") IsNot Nothing Then
                        If drpPlainId.SelectedIndex = 0 Then
                            ' If Request("drpPlainId").ToString().Length <= 0 Then
                            hdNPID.Value = ""
                            ' txtPlainId.Text = ""
                            lblError.Text = "For the selected Order Type Plan Id is mandatory."
                            If rdlNewCancel.SelectedIndex = 0 Then
                                bindOrderStatusNew()
                            Else
                                bindOrderStatusCancellation()
                            End If
                            ddlOrderType.SelectedValue = objInputOrderTypeXml.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText
                            drpPlainId.Focus()
                            Exit Sub
                        End If
                    Else
                        If drpPlainId.SelectedIndex = 0 Then
                            'If drpPlainId.ToString().Length <= 0 Then
                            'If Request("txtPlainId").ToString().Length <= 0 Then
                            hdNPID.Value = ""
                            ' txtPlainId.Text = ""
                            lblError.Text = "For the selected Order Type Plan Id is mandatory."
                            If rdlNewCancel.SelectedIndex = 0 Then
                                bindOrderStatusNew()
                            Else
                                bindOrderStatusCancellation()
                            End If
                            ddlOrderType.SelectedValue = objInputOrderTypeXml.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText
                            Exit Sub
                        End If
                    End If
                End If

                If ddlOrderType.Items.Count > 0 Then
                    objInputXml2.LoadXml("<MS_VIEWORDERTYPE_INPUT><ORDERTYPEID></ORDERTYPEID></MS_VIEWORDERTYPE_INPUT>")
                    objInputXml2.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText = ddlOrderType.SelectedValue.Trim()
                    objOutputXml2 = objbzOrderType.View(objInputXml2)

                    If (objOutputXml2.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("ForPCType").Value.Trim() = "Agency") Then
                        If (txtAgencyPcReq.Text.Trim().Length <= 0) Then
                            lblError.Text = "Agency Pc is required for the selected Order Type"
                            txtAgencyPcReq.Focus()
                            Exit Sub
                        End If
                    ElseIf (objOutputXml2.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("ForPCType").Value.Trim() = "Amadeus") Then
                        If (txtAmadeusPcReq.Text.Trim().Length <= 0) Then
                            lblError.Text = "Amadeus Pc is required for the selected Order Type"
                            txtAmadeusPcReq.Focus()
                            Exit Sub
                        End If
                    End If
                End If



                ''**************************************ashish on 16-03-2011*********************************************
                Dim strInputText As String = ""
                Dim strArr() As String
                Dim strFinal As String = ""
                Dim count As Integer
                Dim flagEightChar As Boolean
                Dim totpc As Integer
                Dim arrcount As Integer
                If txtATID.Text.Trim.Length > 0 Then  ' Added By abhishek  for removing madatory field for atid

                    totpc = Val(txtAgencyPcReq.Text) + Val(txtAmadeusPcReq.Text)


                    If rdlNewCancel.SelectedValue = "T" Then

                        Call checklen(txtATID.Text)
                        If flag = False Then
                            txtATID.Focus()
                            Exit Sub
                        End If

                        Call checkAlphaNumeric(txtATID.Text)
                        If flag = False Then
                            txtATID.Focus()
                            Exit Sub
                        End If

                        txtATID.Text = txtATID.Text.Replace(ControlChars.NewLine, "")  ''ignore the new line

                        If flag = True Then
                            If (txtATID.Text.Length = 8) And (totpc = 1) Then
                                txtATID.Text = txtATID.Text
                                strFinal = txtATID.Text
                                flagEightChar = True
                                flag = False
                                'ElseIf (txtATID.Text.Length >= 8 And txtATID.Text.Length <= 225) Then
                            ElseIf (txtATID.Text.Length >= 8 And txtATID.Text.Length <= 1890) Then

                                ''find for comma
                                Dim intCommaPosition As String = InStr(txtATID.Text, ",", CompareMethod.Text)
                                ' TextBox2.Text = intCommaPosition
                                If intCommaPosition > 0 Then
                                    ''build array : main work here    
                                    strArr = txtATID.Text.Split(",")
                                    arrcount = strArr.GetUpperBound(0) + 1
                                    If arrcount = totpc Then
                                        For count = 0 To strArr.Length - 1
                                            If Len(strArr(count)) = 8 Then
                                                strFinal = strFinal & "," & strArr(count)
                                                flag = True
                                            Else
                                                strFinal = ""
                                                lblError.Text = "Invalid ATID"
                                                flag = False
                                                txtATID.Focus()
                                                Exit Sub
                                            End If
                                        Next
                                    Else
                                        strFinal = ""
                                        lblError.Text = "Invalid ATID"
                                        flag = False
                                        txtATID.Focus()
                                        Exit Sub
                                    End If

                                Else
                                    lblError.Text = "Invalid ATID"
                                    flag = False
                                    txtATID.Focus()
                                    Exit Sub
                                End If ''comma check

                            End If  ''len > 8

                        End If   ''flag

                        If flag = True Then  ''remove extra comma here
                            strFinal = Mid(strFinal, 2)
                        ElseIf flagEightChar = True Then
                            strFinal = strFinal
                            'Exit Sub
                        End If

                    End If ''CHECK BY ASHISH
                    ''***********************************************end here*******************************************
                End If



                ' ''**************************************ashish on 10-02-2011*********************************************
                'Dim strInputText As String
                'Dim strArr() As String
                'Dim strFinal As String = ""
                'Dim count As Integer
                'Dim flagEightChar As Boolean

                'If rdlNewCancel.SelectedValue = "T" Then

                '    Call checklen(txtATID.Text)
                '    If flag = False Then
                '        txtATID.Focus()
                '        Exit Sub
                '    End If

                '    Call checkAlphaNumeric(txtATID.Text)
                '    If flag = False Then
                '        txtATID.Focus()
                '        Exit Sub
                '    End If

                '    txtATID.Text = txtATID.Text.Replace(ControlChars.NewLine, "")  ''ignore the new line

                '    If flag = True Then
                '        If (txtATID.Text.Length = 8) Then
                '            txtATID.Text = txtATID.Text
                '            strFinal = txtATID.Text
                '            flagEightChar = True
                '            flag = False
                '        ElseIf (txtATID.Text.Length >= 8 And txtATID.Text.Length <= 225) Then
                '            ''find for comma
                '            Dim intCommaPosition As String = InStr(txtATID.Text, ",", CompareMethod.Text)
                '            ' TextBox2.Text = intCommaPosition
                '            If intCommaPosition > 0 Then
                '                ''build array : main work here    
                '                strArr = txtATID.Text.Split(",")
                '                For count = 0 To strArr.Length - 1
                '                    If Len(strArr(count)) = 8 Then
                '                        strFinal = strFinal & "," & strArr(count)
                '                        flag = True
                '                    Else
                '                        strFinal = ""
                '                        lblError.Text = "Invalid ATID"
                '                        flag = False
                '                        txtATID.Focus()
                '                        Exit Sub
                '                    End If
                '                Next
                '            Else
                '                lblError.Text = "Invalid ATID"
                '                flag = False
                '                txtATID.Focus()
                '                Exit Sub
                '            End If ''comma check

                '        End If  ''len > 8

                '    End If   ''flag

                '    If flag = True Then  ''remove extra comma here
                '        strFinal = Mid(strFinal, 2)
                '    ElseIf flagEightChar = True Then
                '        strFinal = strFinal
                '        'Exit Sub
                '    End If

                'End If ''CHECK BY ASHISH
                ' ''***********************************************end here*******************************************




                Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                Dim objbzOrder As New AAMS.bizTravelAgency.bzOrder


                objInputXml.LoadXml("<MS_UPDATEORDERS_INPUT><ORDERS ORDERID='' ORDERTYPEID='' LCODE='' ORDER_NUMBER='' ORDERSTATUSID='' PROCESSEDBYID='' PLANID='' ISPID='' RESEND_DATE_MKT='' RECEIVED_DATE_MKT='' INSTALLATION_DUE_DATE='' EXPECTED_INSTALLATION_DATE='' APR='' OPC='' APC='' NewOrder='' OFFICEID1='' OFFICEID='' RECEIVING_OFFICEID='' MSG_SEND_DATE='' APPLIED_DATE='' SENDBACK_DATE='' APPROVAL_DATE=''  RECEIVED_DATE='' PROCESSED_DATE='' REMARKS='' PENDINGWITHID='' EMPLOYEEID='' ATID='' COMP_VERTICAL='' /></MS_UPDATEORDERS_INPUT>")


                With objInputXml.DocumentElement

                    If hDCompanyVerticalSelectByUser.Value = "" Then
                        GetCompanyVertical(1)
                    End If

                    If hDCompanyVerticalSelectByUser.Value = "3" Or hDCompanyVerticalSelectByUser.Value = "" Then
                        'lblError.Text = "Please select either Amadeus or ResBird to process the order."
                        'Exit Sub
                    End If

                    .SelectSingleNode("ORDERS").Attributes("COMP_VERTICAL").Value = hDCompanyVerticalSelectByUser.Value


                    .SelectSingleNode("ORDERS").Attributes("ORDERTYPEID").Value = Request.Form("ddlOrderType")
                    .SelectSingleNode("ORDERS").Attributes("LCODE").Value = hdAgencyNameId.Value
                    .SelectSingleNode("ORDERS").Attributes("ORDER_NUMBER").Value = txtOrderNumber.Text

                    .SelectSingleNode("ORDERS").Attributes("ORDERSTATUSID").Value = ddlOrderStatus.SelectedValue


                    If hdEmpID.Value = "" Then
                        Dim str As String()
                        str = Session("LoginSession").ToString().Split("|")
                        .SelectSingleNode("ORDERS").Attributes("PROCESSEDBYID").Value = str(0)
                    Else
                        .SelectSingleNode("ORDERS").Attributes("PROCESSEDBYID").Value = hdEmpID.Value
                    End If

                    '.SelectSingleNode("ORDERS").Attributes("PLANID").Value = hdNPID.Value ' txtPlainId.Text

                    .SelectSingleNode("ORDERS").Attributes("PLANID").Value = drpPlainId.SelectedValue
                    If drpIspList.SelectedIndex <> 0 Then
                        .SelectSingleNode("ORDERS").Attributes("ISPID").Value = drpIspList.SelectedValue
                    Else
                        .SelectSingleNode("ORDERS").Attributes("ISPID").Value = ""
                    End If
                    .SelectSingleNode("ORDERS").Attributes("RESEND_DATE_MKT").Value = objeAAMS.ConvertTextDate(txtDateMdResending.Text)
                    .SelectSingleNode("ORDERS").Attributes("RECEIVED_DATE_MKT").Value = objeAAMS.ConvertTextDate(txtDateMdReceivingMukund.Text)
                    .SelectSingleNode("ORDERS").Attributes("INSTALLATION_DUE_DATE").Value = ""

                    'Modified on 23rd December

                    If ddlOrderStatus.SelectedValue = hdOrderApproved.Value Then
                        If (OrderStatusDays.Value.Trim().Length <> 0) Then
                            If txtDateMessage.Text.Trim().Length <> 0 Then
                                Dim dtMessagedate As New DateTime
                                dtMessagedate = Date.ParseExact(txtDateMessage.Text, "dd/MM/yyyy", Nothing)
                                'dtMessagedate = Convert.ToDateTime("#" & txtDateMessage.Text & "#")
                                'Dim dtExptDate As New DateTime
                                'dtExptDate = Convert.ToDateTime(OrderStatusDays.Value.Trim()).ToShortDateString()

                                ' Dim lngDays As Integer = DateDiff(DateInterval.Day, dtExptDate, dtMessagedate)
                                dtMessagedate = DateAdd(DateInterval.Day, Convert.ToInt32(OrderStatusDays.Value.Trim()), dtMessagedate)
                                Dim strDate As String = dtMessagedate.Day.ToString() & "/" & dtMessagedate.Month.ToString() & "/" & dtMessagedate.Year.ToString()

                                ' dtMessagedate = Date.ParseExact(dtMessagedate.ToShortDateString(), "dd/MM/yyyy", Nothing)
                                .SelectSingleNode("ORDERS").Attributes("EXPECTED_INSTALLATION_DATE").Value = objeAAMS.ConvertTextDate(strDate)
                            End If

                        Else

                            If txtDateMessage.Text.Trim().Length <> 0 Then
                                .SelectSingleNode("ORDERS").Attributes("EXPECTED_INSTALLATION_DATE").Value = objeAAMS.ConvertTextDate(txtDateMessage.Text.Trim())
                            End If

                        End If
                    End If

                    'Modified on 23rd December


                    ' .SelectSingleNode("ORDERS").Attributes("EXPECTED_INSTALLATION_DATE").Value = objeAAMS.ConvertTextDate(txtDateExp.Text)


                    .SelectSingleNode("ORDERS").Attributes("APR").Value = txtAmadeusPrinterReq.Text
                    .SelectSingleNode("ORDERS").Attributes("OPC").Value = txtAgencyPcReq.Text
                    .SelectSingleNode("ORDERS").Attributes("APC").Value = txtAmadeusPcReq.Text
                    .SelectSingleNode("ORDERS").Attributes("NewOrder").Value = rdlNewCancel.SelectedValue
                    .SelectSingleNode("ORDERS").Attributes("OFFICEID").Value = txtOfficeID1.Text
                    .SelectSingleNode("ORDERS").Attributes("OFFICEID1").Value = txtOfficeID2.Text
                    .SelectSingleNode("ORDERS").Attributes("RECEIVING_OFFICEID").Value = txtReceivingOfficeID.Text
                    .SelectSingleNode("ORDERS").Attributes("MSG_SEND_DATE").Value = objeAAMS.ConvertTextDate(txtDateMessage.Text)
                    .SelectSingleNode("ORDERS").Attributes("APPLIED_DATE").Value = objeAAMS.ConvertTextDate(txtDateApplied.Text)
                    .SelectSingleNode("ORDERS").Attributes("SENDBACK_DATE").Value = objeAAMS.ConvertTextDate(txtDateSentBack.Text)
                    .SelectSingleNode("ORDERS").Attributes("APPROVAL_DATE").Value = objeAAMS.ConvertTextDate(txtDateApproval.Text)
                    .SelectSingleNode("ORDERS").Attributes("RECEIVED_DATE").Value = objeAAMS.ConvertTextDate(txtDateReceived.Text)
                    .SelectSingleNode("ORDERS").Attributes("PROCESSED_DATE").Value = "" ' objeAAMS.ConvertTextDate(txtDateProcessed.Text)
                    .SelectSingleNode("ORDERS").Attributes("REMARKS").Value = txtremarks.Text
                    .SelectSingleNode("ORDERS").Attributes("PENDINGWITHID").Value = hdPendingWithId.Value
                    .SelectSingleNode("ORDERS").Attributes("ATID").Value = strFinal



                    If Not Session("LoginSession") Is Nothing Then
                        Dim str As String()
                        str = Session("LoginSession").ToString().Split("|")
                        .SelectSingleNode("ORDERS").Attributes("EMPLOYEEID").Value = str(0)
                    Else
                        lblError.Text = "Login Failed"
                        Exit Sub
                    End If



                    If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
                        '  .SelectSingleNode("ORDERS").Attributes("ACTION").Value = "U"
                        .SelectSingleNode("ORDERS").Attributes("ORDERID").Value = objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString().ToUpper())
                        hdOrderID.Value = objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString().ToUpper())
                    Else
                        ' .SelectSingleNode("ORDERS").Attributes("ACTION").Value = "I"
                        .SelectSingleNode("ORDERS").Attributes("ORDERID").Value = ""
                    End If
                End With


                objOutputXml = objbzOrder.Update(objInputXml)


                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
                        lblError.Text = objeAAMSMessage.messUpdate ' "Record updated successfully."
                        ViewOrder()
                        GetCompanyVertical(1)
                    Else
                        Response.Redirect("MSUP_Order.aspx?Action=US|" & objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("ORDERS").Attributes("ORDERID").Value) & "|" & hdCity.Value.Trim())
                        lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."

                    End If
                Else

                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("MSUP_Order.aspx?Action=I|")
    End Sub

    Sub ViewOrder()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            'Dim objXmlReader As XmlNodeReader
            Dim objbzOrder As New AAMS.bizTravelAgency.bzOrder
            objInputXml.LoadXml("<MS_VIEWORDERS_INPUT>	<ORDERID></ORDERID></MS_VIEWORDERS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ORDERID").InnerText = objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString())
            hdOrderID.Value = objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString().ToUpper())
            hdCity.Value = Request.QueryString("Action").ToString().Split("|").GetValue(2).ToString()
            'Here Back end Method Call
            objOutputXml = objbzOrder.View(objInputXml)
            '            <MS_VIEWORDERS_OUTPUT>
            '	<ORDERS ORDERID='' ORDERTYPEID='' LCODE='' NAME='' ADDRESS='' ADDRESS1='' ORDER_NUMBER=''
            '		ORDERSTATUSID='' PROCESSEDBYID='' PROCESSEDBYNAME='' PLANID='' ISPNAME='' ISPID='' RESEND_DATE_MKT=''
            '		RECEIVED_DATE_MKT='' INSTALLATION_DUE_DATE='' EXPECTED_INSTALLATION_DATE='' APR='' OPC=''
            '		APC='' NewOrder='' OFFICEID1='' OFFICEID='' RECEIVING_OFFICEID='' MSG_SEND_DATE='' APPLIED_DATE=''
            '		SENDBACK_DATE='' APPROVAL_DATE='' RECEIVED_DATE='' PROCESSED_DATE='' REMARKS='' PENDINGWITHID=""
            '		PENDINGWITHNAME="" />
            '         <Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_VIEWORDERS_OUTPUT>

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXml.DocumentElement
                    If .SelectSingleNode("ORDERS").Attributes("NewOrder").InnerText = "f" Then
                        rdlNewCancel.SelectedValue = "f"
                        bindOrderStatusCancellation()
                    End If

                    If .SelectSingleNode("ORDERS").Attributes("NewOrder").InnerText = "T" Then
                        rdlNewCancel.SelectedValue = "T"
                        bindOrderStatusNew()
                    End If

                    Dim lstItem As ListItem
                    lstItem = ddlOrderType.Items.FindByValue(.SelectSingleNode("ORDERS").Attributes("ORDERTYPEID").InnerText) 'New ListItem(.SelectSingleNode("ORDERS").Attributes("ORDERTYPE").InnerText, .SelectSingleNode("ORDERS").Attributes("ORDERTYPEID").InnerText)


                    If lstItem IsNot Nothing Then
                        ddlOrderType.SelectedValue = lstItem.Value
                    End If



                    'ddlOrderType.SelectedValue = .SelectSingleNode("ORDERS").Attributes("ORDERTYPEID").InnerText
                    hdAgencyNameId.Value = .SelectSingleNode("ORDERS").Attributes("LCODE").InnerText
                    hdAgencyName.Value = .SelectSingleNode("ORDERS").Attributes("NAME").InnerText
                    hdEmpID.Value = .SelectSingleNode("ORDERS").Attributes("PROCESSEDBYID").InnerText

                    txtAgencyName.Text = .SelectSingleNode("ORDERS").Attributes("NAME").InnerText
                    txtAgencyAddress.Text = .SelectSingleNode("ORDERS").Attributes("ADDRESS").InnerText
                    hdAddress.Value = txtAgencyAddress.Text
                    txtOrderNumber.Text = .SelectSingleNode("ORDERS").Attributes("ORDER_NUMBER").InnerText
                    hdFileNo.Value = .SelectSingleNode("ORDERS").Attributes("FILENO").InnerText
                    ddlOrderStatus.SelectedValue = .SelectSingleNode("ORDERS").Attributes("ORDERSTATUSID").InnerText

                    txtProcessedBy.Text = .SelectSingleNode("ORDERS").Attributes("PROCESSEDBYNAME").InnerText

                    ' txtPlainId.Text = .SelectSingleNode("ORDERS").Attributes("PLANID").InnerText

                    hdNPID.Value = .SelectSingleNode("ORDERS").Attributes("PLANID").InnerText

                    ' txtIspName.Text = .SelectSingleNode("ORDERS").Attributes("ISPNAME").InnerText

                    hdIspNameId.Value = .SelectSingleNode("ORDERS").Attributes("ISPID").InnerText

                    ' txtPlainId.Text = .SelectSingleNode("ORDERS").Attributes("PLANID").InnerText




                    hdNPID.Value = .SelectSingleNode("ORDERS").Attributes("PLANID").InnerText





                    txtDateMdResending.Text = objeAAMS.ConvertDateBlank(.SelectSingleNode("ORDERS").Attributes("RESEND_DATE_MKT").InnerText)
                    txtDateMdReceivingMukund.Text = objeAAMS.ConvertDateBlank(.SelectSingleNode("ORDERS").Attributes("RECEIVED_DATE_MKT").InnerText)
                    ' "" = .SelectSingleNode("ORDERS").Attributes("INSTALLATION_DUE_DATE").InnerText
                    txtDateExp.Text = objeAAMS.ConvertDateBlank(.SelectSingleNode("ORDERS").Attributes("EXPECTED_INSTALLATION_DATE").InnerText)
                    txtAmadeusPrinterReq.Text = .SelectSingleNode("ORDERS").Attributes("APR").InnerText
                    txtAgencyPcReq.Text = .SelectSingleNode("ORDERS").Attributes("OPC").InnerText
                    txtAmadeusPcReq.Text = .SelectSingleNode("ORDERS").Attributes("APC").InnerText
                    rdlNewCancel.SelectedValue = .SelectSingleNode("ORDERS").Attributes("NewOrder").InnerText
                    txtOfficeID1.Text = .SelectSingleNode("ORDERS").Attributes("OFFICEID").InnerText
                    txtOfficeID2.Text = .SelectSingleNode("ORDERS").Attributes("OFFICEID1").InnerText
                    txtReceivingOfficeID.Text = .SelectSingleNode("ORDERS").Attributes("RECEIVING_OFFICEID").InnerText
                    txtDateMessage.Text = objeAAMS.ConvertDateBlank(.SelectSingleNode("ORDERS").Attributes("MSG_SEND_DATE").InnerText)
                    txtDateApplied.Text = objeAAMS.ConvertDateBlank(.SelectSingleNode("ORDERS").Attributes("APPLIED_DATE").InnerText)

                    txtDateSentBack.Text = objeAAMS.ConvertDateBlank(.SelectSingleNode("ORDERS").Attributes("SENDBACK_DATE").InnerText)
                    txtDateApproval.Text = objeAAMS.ConvertDateBlank(.SelectSingleNode("ORDERS").Attributes("APPROVAL_DATE").InnerText)

                    txtDateReceived.Text = objeAAMS.ConvertDateBlank(.SelectSingleNode("ORDERS").Attributes("RECEIVED_DATE").InnerText)
                    txtDateProcessed.Text = objeAAMS.ConvertDateBlank(.SelectSingleNode("ORDERS").Attributes("PROCESSED_DATE").InnerText)

                    txtremarks.Text = .SelectSingleNode("ORDERS").Attributes("REMARKS").InnerText
                    hdPendingWithId.Value = .SelectSingleNode("ORDERS").Attributes("PENDINGWITHID").InnerText
                    txtPendingWith.Text = .SelectSingleNode("ORDERS").Attributes("PENDINGWITHNAME").InnerText
                    hdOfficeID.Value = .SelectSingleNode("ORDERS").Attributes("OFFICEID").InnerText


                    txtATID.Text = .SelectSingleNode("ORDERS").Attributes("ATID").InnerText ''added by ashish

                    BindDrpIspName(drpIspList, True)


                    Dim li5 As ListItem = drpIspList.Items.FindByValue(.SelectSingleNode("ORDERS").Attributes("ISPID").InnerText)
                    If li5 IsNot Nothing Then
                        drpIspList.SelectedValue = li5.Value ' .SelectSingleNode("ORDERS").Attributes("PLANID").InnerText
                    End If

                    '   drpIspName.SelectedValue = .SelectSingleNode("ORDERS").Attributes("ISPID").InnerText

                    GetNPID()

                    ' txtPlainId.Text = .SelectSingleNode("ORDERS").Attributes("PLANID").InnerText

                    'Dim li As ListItem = drpPlainId.Items.FindByText(.SelectSingleNode("ORDERS").Attributes("PLANID").InnerText)
                    'If li IsNot Nothing Then
                    '    drpPlainId.SelectedValue = li.Value ' .SelectSingleNode("ORDERS").Attributes("PLANID").InnerText
                    'End If
                    Dim li As ListItem = drpPlainId.Items.FindByValue(.SelectSingleNode("ORDERS").Attributes("PLANID").InnerText)
                    If li IsNot Nothing Then
                        drpPlainId.SelectedValue = li.Value ' .SelectSingleNode("ORDERS").Attributes("PLANID").InnerText
                    End If


                    'If hdIspNameId.Value = "" Then
                    '    drpIspList.SelectedValue = ""
                    '    'drpIspList.Enabled = False
                    '    'drpPlainId.Enabled = False
                    'Else
                    '    'drpIspList.Enabled = True
                    '    'drpPlainId.Enabled = True
                    '    drpIspList.SelectedValue = hdIspNameId.Value
                    'End If

                    'Enable Disable ISP & NPID
                    EnableDisableIsp()

                    '**********************************************************

                    Dim objSecurityXml As New XmlDocument
                    objSecurityXml.LoadXml(Session("Security"))
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                        If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Date Mkt']").Count <> 0 Then
                            strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Date Mkt']").Attributes("Value").Value)
                        End If

                        If strBuilder(2) = "0" Then
                            imgDateMdReceivingMukund.Disabled = True
                            txtDateMdReceivingMukund.CssClass = "textboxgrey"
                            txtDateMdReceivingMukund.ReadOnly = True
                            txtDateMdResending.ReadOnly = True
                            txtDateMdResending.CssClass = "textboxgrey"
                            imgDateMdResending.Disabled = True
                        End If
                    Else
                        strBuilder = objeAAMS.SecurityCheck(31)
                    End If

                    '**********************************************************

                    '@ Code For Readonly Or Write For IspName and NPID on the basis of Users Write
                    objSecurityXml.LoadXml(Session("Security"))
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                        If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify ISPNAME']").Count <> 0 Then
                            strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify ISPNAME']").Attributes("Value").Value)
                            If strBuilder(2) = "0" Then
                                drpIspList.Enabled = False
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


                End With

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
            lblError.Text = ""
            ViewOrder()
        Else
            ddlOrderType.SelectedIndex = 0
            hdAgencyNameId.Value = ""
            txtAgencyName.Text = ""
            txtAgencyAddress.Text = ""
            txtOrderNumber.Text = ""
            ddlOrderStatus.SelectedIndex = 0
            hdProcessedById.Value = ""
            txtProcessedBy.Text = ""
            ' txtPlainId.Text = ""
            drpPlainId.SelectedValue = ""
            'txtIspName.Text = ""
            hdIspNameId.Value = ""
            txtDateMdResending.Text = ""
            txtDateMdReceivingMukund.Text = ""
            ' ddlOrderType.SelectedValue = .SelectSingleNode("ORDERS").Attributes("INSTALLATION_DUE_DATE").InnerText
            txtDateExp.Text = ""
            txtAmadeusPrinterReq.Text = ""
            txtAgencyPcReq.Text = ""
            txtAmadeusPcReq.Text = ""
            txtOfficeID1.Text = ""
            txtOfficeID2.Text = ""
            txtReceivingOfficeID.Text = ""
            txtDateMessage.Text = ""
            txtDateApplied.Text = ""
            txtDateSentBack.Text = ""
            txtDateApproval.Text = ""
            txtDateReceived.Text = ""
            txtDateProcessed.Text = ""
            txtremarks.Text = ""
            txtPendingWith.Text = ""
            hdPendingWithId.Value = ""
            lblError.Text = ""
        End If
    End Sub

    Protected Sub btnViewDocument_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnViewDocument.Click
        'If hdFileNo.Value = "" Then
        '    lblError.Text = "File Number Not Found"
        '    Exit Sub
        'End If
        'If txtOrderNumber.Text.Trim().Length <= 0 Then
        '    lblError.Text = "Order Number Not Found"
        '    Exit Sub
        'End If
        'Response.Redirect("TASR_ViewOrderDoc.aspx?OrderNo=" & txtOrderNumber.Text.Trim() & "&FileNo=" & hdFileNo.Value & "&AgencyName=" & txtAgencyName.Text)
    End Sub



    'Modified by Mukund on 30-01-08


    Sub BindDrpIspName(ByVal drpDownList As DropDownList, ByVal bolSelect As Boolean)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim objbzISP As New AAMS.bizISP.bzISP
        Dim ds As New DataSet

        drpIspList.Items.Clear()

        'If (hdCity.Value.Trim() IsNot Nothing) Then
        objInputXml.LoadXml("<IS_LISTISP_INPUT><CITY></CITY></IS_LISTISP_INPUT>")
        ' objInputXml.DocumentElement.SelectSingleNode("CITY").InnerXml = hdCity.Value.Trim()
        objOutputXml = New XmlDocument
        objOutputXml = objbzISP.List1(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            drpDownList.DataSource = ds.Tables("ISP")
            drpDownList.DataTextField = "Name"
            drpDownList.DataValueField = "ISPID"
            drpDownList.DataBind()
        Else
            drpDownList.DataTextField = String.Empty
            drpDownList.DataValueField = String.Empty
            drpDownList.DataBind()
        End If
        Dim li As ListItem
        If bolSelect = True Then
            li = drpDownList.Items.FindByValue("")
            If li Is Nothing Then
                drpDownList.Items.Insert(0, New ListItem("---Select One---", ""))
            End If
        End If
        'Else
        'lblError.Text = "City Name Not Found"
        'End If

    End Sub


    'this Method is written by Mukund on 6th Feb 2008
    Protected Sub rdlNewCancel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdlNewCancel.SelectedIndexChanged
        If rdlNewCancel.SelectedValue = "f" Then
            bindOrderStatusCancellation()
            ''added by ashish on date 09-02-2011
            pnlATID.Visible = False
            txtATID.Visible = False
        End If
        If rdlNewCancel.SelectedValue = "T" Then
            bindOrderStatusNew()
            ''added by ashish on date 09-02-2011
            pnlATID.Visible = True
            txtATID.Visible = True
        End If


    End Sub



    Protected Sub ddlOrderType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOrderType.SelectedIndexChanged

        Dim objSecurityXml As New XmlDocument
        Try
            lblError.Text = ""
            ' drpIspList.SelectedIndex = -1
            ' txtPlainId.Text = ""
            drpIspList.Items.Clear()
            drpPlainId.Items.Clear()
            hdNPID.Value = ""
            'txtAgencyName.Text = hdAgencyNameId.Value
            txtOfficeID1.Text = hdOfficeID.Value
            txtAgencyAddress.Text = hdAddress.Value

            If hdAgencyName.Value.Trim().Length = 0 Then

                lblError.Text = "Select Agency Name"
                Exit Sub
            End If
            'If txtAgencyName.Text.Length = 0 Then
            '    lblError.Text = "Select Agency Name"
            '    Exit Sub
            'End If


            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbzOrderType As New AAMS.bizTravelAgency.bzOrderType
            If ddlOrderType.Items.Count > 0 Then
                objInputXml.LoadXml("<MS_VIEWORDERTYPE_INPUT><ORDERTYPEID></ORDERTYPEID></MS_VIEWORDERTYPE_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText = ddlOrderType.SelectedValue.Trim()
                objOutputXml = objbzOrderType.View(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    'Modified on 26th August
                    If objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("EXPECTED_INSTALLATION_DATE").InnerText.Trim <> "" Then

                        ' txtDateExp.Text = objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("EXPECTED_INSTALLATION_DATE").InnerText.Trim

                        'Added on 22nd December
                        OrderStatusDays.Value = objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("TimeRequired").InnerText.Trim
                        'Added on 22nd December


                    Else
                        txtDateExp.Text = ""
                    End If
                    'End of Modification

                    If objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsISPOrder").Value.ToUpper() = "TRUE" Then
                        BindDrpIspName(drpIspList, True)
                        drpIspList.Enabled = True
                        drpPlainId.Enabled = True



                    Else
                        ' drpIspList.SelectedIndex = 0
                        ' txtPlainId.Text = ""
                        hdNPID.Value = ""
                        drpIspList.Enabled = False
                        drpPlainId.Enabled = False
                    End If

                Else
                    ' lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            '@ Code For Readonly Or Write For IspName and NPID on the basis of Users Write
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify ISPNAME']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify ISPNAME']").Attributes("Value").Value)
                    If strBuilder(2) = "0" Then
                        drpIspList.Enabled = False
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

        End Try

        'Enable or Disable ISP
        EnableDisableIsp()


    End Sub
    Private Sub bindOrderStatusNew()
        Dim objbzOrderType As New AAMS.bizTravelAgency.bzOrderType
        Dim objOutputXml As New XmlDocument
        Dim objInputXml As New XmlDocument
        Dim objNodeList As XmlNodeList
        ddlOrderType.Items.Clear()
        objInputXml.LoadXml("<UP_LISTORDERTYPE_INPUT><ORDER_TYPE></ORDER_TYPE></UP_LISTORDERTYPE_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("ORDER_TYPE").InnerText = 0
        objOutputXml = objbzOrderType.List(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            ' hdOrderTypeNew.Value = objOutputXml.InnerXml
            objNodeList = objOutputXml.SelectNodes("UP_LISTORDERTYPE_OUTPUT/ORDER_TYPE")
            For Each objXmlNode As XmlNode In objNodeList
                ddlOrderType.Items.Add(New ListItem(objXmlNode.Attributes("ORDER_TYPE_NAME").InnerText, objXmlNode.Attributes("ORDERTYPEID").InnerText))
            Next
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If

        ddlOrderType.Items.Insert(0, New ListItem("---Select One---", ""))
    End Sub
    Private Sub bindOrderStatusCancellation()
        Dim objbzOrderType As New AAMS.bizTravelAgency.bzOrderType
        Dim objOutputXml As New XmlDocument
        Dim objInputXml As New XmlDocument
        Dim objNodeList As XmlNodeList
        ddlOrderType.Items.Clear()
        objInputXml.LoadXml("<UP_LISTORDERTYPE_INPUT><ORDER_TYPE></ORDER_TYPE></UP_LISTORDERTYPE_INPUT>")

        objInputXml.DocumentElement.SelectSingleNode("ORDER_TYPE").InnerText = 1
        objOutputXml = objbzOrderType.List(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            ' hdOrderTypeCancel.Value = objOutputXml.InnerXml
            objNodeList = objOutputXml.SelectNodes("UP_LISTORDERTYPE_OUTPUT/ORDER_TYPE")
            For Each objXmlNode As XmlNode In objNodeList
                ddlOrderType.Items.Add(New ListItem(objXmlNode.Attributes("ORDER_TYPE_NAME").InnerText, objXmlNode.Attributes("ORDERTYPEID").InnerText))
            Next
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
        ddlOrderType.Items.Insert(0, New ListItem("---Select One---", ""))
    End Sub

    Private Sub GetNPID()

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        drpPlainId.Items.Clear()
        Try
            Dim objbzISP As New AAMS.bizISP.bzISPPlan
            objInputXml.LoadXml("<IS_SEARCHISPPLAN_INPUT><ISPID></ISPID><Name></Name><CityID></CityID><NPID></NPID><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><ProviderID/></IS_SEARCHISPPLAN_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("ISPID").InnerText = drpIspList.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "NPID"
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"

            If drpIspList.SelectedValue <> "" Then
                objOutputXml = objbzISP.Search(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    'drpPlainId.DataSource=objOutputXml
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpPlainId.DataSource = ds.Tables("ISPPLAN")
                    drpPlainId.DataTextField = "NPID"
                    drpPlainId.DataValueField = "ISPPlanID"
                    drpPlainId.DataBind()
                Else

                End If

            End If


            Dim li As ListItem
            li = drpPlainId.Items.FindByValue("")
            If li Is Nothing Then
                drpPlainId.Items.Insert(0, New ListItem("---Select one---", ""))
            End If


        Catch ex As Exception

            ' Finally
            'Dim li As ListItem
            'li = drpPlainId.Items.FindByValue("")
            'If li Is Nothing Then
            '    drpPlainId.Items.Insert(0, New ListItem("Select one", ""))
            'End If
            ''@ Code For Readonly Or Write For IspName and NPID on the basis of Users Write
            'Dim objSecurityXml As New XmlDocument

            'objSecurityXml.LoadXml(Session("Security"))
            'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify NPID']").Count <> 0 Then
            '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify NPID']").Attributes("Value").Value)
            '        If strBuilder(2) = "0" Then
            '            drpPlainId.Enabled = False
            '        End If
            '    End If
            'Else
            '    strBuilder = objeAAMS.SecurityCheck(31)
            'End If
            '@End of Code For Readonly Or Write For IspName and NPID on the basis of Users Write
        End Try
    End Sub

    Protected Sub drpIspList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpIspList.SelectedIndexChanged
        GetNPID()
    End Sub
#Region "CheckSecurity()::This method is used for security check."
    Private Sub CheckSecurity()
        Try
            ' This code is used for checking session handler according to user login.
            'Dim strBuilder1 As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order']").Attributes("Value").Value)
            End If
            If strBuilder(1) = "0" Then
                btnNew.Enabled = False
                btnSave.Enabled = False
            End If
            If strBuilder(2) = "0" And strBuilder(1) = "0" Then 'Add =false /Modify= false
                btnSave.Enabled = False
            End If
            If strBuilder(2) = "0" And strBuilder(1) = "1" Then 'Modify =false /Add= True
                If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0) = "US" Then
                    btnSave.Enabled = False
                Else
                    btnSave.Enabled = True
                End If
            End If
            If strBuilder(2) = "1" And strBuilder(1) = "0" Then 'Modify= true/Add =false
                If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0) = "US" Then
                    btnSave.Enabled = True
                Else
                    btnSave.Enabled = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Private Sub AssignHiddApprovedValu()
        Dim objOutputXml As New XmlDocument
        Dim objNode As XmlNode
        Dim objGetConfigvalu As New AAMS.bizTravelAgency.bzOrder
        objOutputXml = objGetConfigvalu.ListOrderingConfigValue()

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objNode = objOutputXml.DocumentElement.SelectSingleNode("CONFIGVALUE[@FIELD_NAME='APPROVEDORDERSATUS']")
            If objNode IsNot Nothing Then
                hdOrderApproved.Value = objNode.Attributes("FIELD_VALUE").Value.Trim
            Else
                hdOrderApproved.Value = ""
            End If
        Else
            hdOrderApproved.Value = ""
        End If
    End Sub
    'Modified on 26th August

    Private Sub AssignOrderTypeDays()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzOrderType As New AAMS.bizTravelAgency.bzOrderType
        If ddlOrderType.Items.Count > 0 Then
            objInputXml.LoadXml("<MS_VIEWORDERTYPE_INPUT><ORDERTYPEID></ORDERTYPEID></MS_VIEWORDERTYPE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText = ddlOrderType.SelectedValue.Trim()
            objOutputXml = objbzOrderType.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("EXPECTED_INSTALLATION_DATE").InnerText.Trim <> "" Then
                    OrderStatusDays.Value = objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("TimeRequired").InnerText.Trim
                End If
            End If
        End If
    End Sub

    Private Sub EnableDisableIsp()
        Dim objSecurityXml As New XmlDocument
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbzOrderType As New AAMS.bizTravelAgency.bzOrderType
            If ddlOrderType.Items.Count > 0 Then
                objInputXml.LoadXml("<MS_VIEWORDERTYPE_INPUT><ORDERTYPEID></ORDERTYPEID></MS_VIEWORDERTYPE_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText = ddlOrderType.SelectedValue.Trim()
                objOutputXml = objbzOrderType.View(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsISPOrder").Value.ToUpper() = "TRUE" Then
                        drpIspList.Enabled = True
                        drpPlainId.Enabled = True
                    Else
                        drpIspList.Enabled = False
                        drpPlainId.Enabled = False
                    End If
                Else
                    drpIspList.Enabled = False
                    drpPlainId.Enabled = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            '@ Code For Readonly Or Write For IspName and NPID on the basis of Users Write
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify ISPNAME']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify ISPNAME']").Attributes("Value").Value)
                    If strBuilder(2) = "0" Then
                        drpIspList.Enabled = False
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

        End Try
    End Sub
    Private Sub GetCompanyVertical(ByVal UpdateUserCV As Integer)
        If (hdAgencyNameId.Value.Trim.Length > 0) Then
            Dim objInputXmlAgency As New XmlDocument
            Dim objOutputXmlAgency As New XmlDocument
            Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
            objInputXmlAgency.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")
            objInputXmlAgency.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = hdAgencyNameId.Value.Trim

            'Here Back end Method Call
            objOutputXmlAgency = objbzAgency.View(objInputXmlAgency)

            If objOutputXmlAgency.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXmlAgency.DocumentElement.SelectSingleNode("Agency")

                    hDCompanyVertical.Value = .Attributes("COMP_VERTICAL").Value()
                    If hDCompanyVertical.Value.Trim = "Amadeus" Then
                        hDCompanyVertical.Value = "1"
                    ElseIf hDCompanyVertical.Value.Trim = "ResBird" Then
                        hDCompanyVertical.Value = "2"
                    ElseIf hDCompanyVertical.Value.Trim = "Non1A" Then
                        hDCompanyVertical.Value = "3"
                    Else
                        hDCompanyVertical.Value = ""
                    End If

                    If UpdateUserCV = 1 Then
                        hDCompanyVerticalSelectByUser.Value = hDCompanyVertical.Value
                    End If

                End With
            End If
        End If
    End Sub

End Class
