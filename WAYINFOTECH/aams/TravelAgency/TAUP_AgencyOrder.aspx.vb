Partial Class TravelAgency_MSUP_AgencyOrder
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Public flag As Boolean

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
            txtOfficeID1.Attributes.Add("readonly", "readonly")
            'hdCity.Value = Session("a")
            hdCity.Value = Session("CityName")
            Session("PageName") = Request.Url.ToString()
            'Session("AgencyOrderMail") = Nothing
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()




            btnMail.CssClass = "headingtabactive"
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If
            btnMail.Enabled = False
            Session("EmailList") = Nothing
            ' Panel1.Visible = False
            '  btnOrder.Attributes.Add("onclick", "return  NewFunctionAorder();")
            If (Session("Action") IsNot Nothing) Then
                hdLcode.Value = Session("Action").ToString().Split("|").GetValue(1)
            End If
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
            ' txtOfficeID1.Attributes.Add("onkeypress", "return ChkApprovalDateAorder();")
            'btnHistory.Attributes.Add("onclick", "return PopupHistoryPageAorder();")
            btnSave.Attributes.Add("onclick", "return ValidationAgencyOrderAorder();")
            btnNew.Attributes.Add("onclick", "return NewFunctionAorder();")
            ' drpIspName.Attributes.Add("onchange", "return ResetNPIdAorder();")

            ' rdlNewCancel.Attributes.Add("onclick", "return FillOrderTypeAorder();")
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Order']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Order']").Attributes("Value").Value)

                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                    End If

                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                        btnSave.Enabled = False
                    End If
                    If Not ViewState("ORDERID") Is Nothing Then
                        If strBuilder(2) = "1" Then
                            btnSave.Enabled = True
                        End If
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If


            ''ashish added on 09-02-2011
            If Not Page.IsPostBack Then
                If rdlNewCancel.SelectedValue = "T" Then
                    pnlATID.Visible = True
                    txtATID.Visible = True

                Else
                    pnlATID.Visible = False
                    txtATID.Visible = False
                End If
            End If

            If Not Page.IsPostBack Then
                ' Load Data For Editing Mode
                Session("AgencyOrderMail") = Nothing
                BindOrderType("New")
                objeAAMS.BindDropDown(ddlOrderStatus, "ORDERSTATUS", True, 1)
                BindDropDownIspName(drpIspName, "ISPLISTByAgencyCity", True)

                'Code Added by Mukund
                AssignOrderTypeDays()

            End If
            If Not Page.IsPostBack Then
                ' Load Data For Editing Mode
                If Session("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Then
                    GetOrder()
                End If
            End If
            
            If (Not Page.IsPostBack) Then
                If Request.QueryString("EditOrderId") IsNot Nothing Then
                    AgencyOrderEdit(Request.QueryString("EditOrderId").ToString())
                End If
            End If




            ' This code is usedc for checking session handler according to user login.

            '################ Added Code To get the value of Agency Name And There Address according to LCode

            If (Session("Action") IsNot Nothing) Then
                objInputXml.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = Session("Action").ToString().Split("|").GetValue(1)

                'Here Back end Method Call
                objOutputXml = objbzAgency.View(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    With objOutputXml.DocumentElement.SelectSingleNode("Agency")
                        'txtAgencyGroup.Text = .Attributes("Chain_Name").Value()
                        'hdChainId.Value = .Attributes("Chain_Code").Value()
                        txtAgencyName.Text = .Attributes("NAME").Value()
                        txtAgencyAddress.Text = .Attributes("ADDRESS").Value() & "  " & .Attributes("ADDRESS1").Value() & ", " & .Attributes("CITY").Value() & ", " & .Attributes("COUNTRY").Value() & ", PIN-" & IIf(.Attributes("PINCODE").Value() = "", "NA", .Attributes("PINCODE").Value())
                        If (Not Page.IsPostBack) Then
                            txtOfficeID1.Text = .Attributes("OFFICEID").Value()
                        End If
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
                    End With
                End If
            End If
            '###################################  End ###########################################################

            '@ New code Added For Finding OrderStatus for Appproved To Check For OfficeId is mandatory or not 
            If Not Page.IsPostBack Then
                hDCompanyVerticalSelectByUser.Value = hDCompanyVertical.Value
                GetOrderStatus()
            End If

            ''Added Tapan Nath for Last OrderView
            'If Session("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Then
            '    If grdOrder.Rows.Count > 0 Then
            '        Dim hdLastOrderID As HiddenField
            '        hdLastOrderID = CType(grdOrder.Rows(0).Cells(11).FindControl("hdOrderId"), HiddenField)
            '        If (Not hdLastOrderID Is Nothing) Then
            '            AgencyOrderEdit(hdLastOrderID.Value.ToString)
            '        End If
            '    End If
            'End If
            ''Added Tapan Nath for Last OrderView --Ends Here

            If Not Page.IsPostBack Then
                If Request.QueryString("EditOrderId") Is Nothing Then
                    If Request.QueryString("Mode") IsNot Nothing Then
                        If Request.QueryString("Mode").Trim.ToString.ToUpper = "F" Then
                            If grdOrder.Rows.Count > 0 Then
                                Dim hdLastOrderID As HiddenField
                                hdLastOrderID = CType(grdOrder.Rows(0).Cells(11).FindControl("hdOrderId"), HiddenField)
                                If (Not hdLastOrderID Is Nothing) Then
                                    AgencyOrderEdit(hdLastOrderID.Value.ToString)
                                End If
                            End If
                        End If
                    End If
                End If
            End If

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
            lblError.Text = "ATID should more than eight character"
            checklen = False
            Exit Function
            'ElseIf (strInputText.Length >= 225) Then
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
            If (Not Session("Action") = Nothing) Then

                '@ ##################################################################################
                '@ Code to check for  Isp checked/not on the basis of ordertypeid
                ' @ Code for GroupISP on the basis of Order Type
                Dim objInputOrderTypeXml As New XmlDataDocument, objOutputOrderTypeXml As New XmlDocument
                Dim objbzOrderType As New AAMS.bizTravelAgency.bzOrderType

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
                If (blnGroupISP = True) Then
                    'txtPlainId.Text = hdNPID.Value

                    If drpIspName.SelectedIndex <= 0 Then
                        lblError.Text = "For the selected Order Type ISP Name is mandatory."
                        If rdlNewCancel.SelectedIndex = 0 Then
                            BindOrderType("New")
                        Else
                            BindOrderType("Cancel")
                        End If
                        ddlOrderType.SelectedValue = objInputOrderTypeXml.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText
                        Exit Sub
                    End If
                    If Request("drpPlainId") IsNot Nothing Then
                        If Request("drpPlainId").ToString().Length <= 0 Then
                            'If Request("txtPlainId").ToString().Length <= 0 Then
                            ' hdNPID.Value = ""
                            ' txtPlainId.Text = ""
                            lblError.Text = "For the selected Order Type Plan Id is mandatory."
                            If rdlNewCancel.SelectedIndex = 0 Then
                                BindOrderType("New")
                            Else
                                BindOrderType("Cancel")
                            End If
                            ddlOrderType.SelectedValue = objInputOrderTypeXml.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText
                            Exit Sub
                        End If
                    Else
                        If drpPlainId.SelectedValue.Trim().Length() <= 0 Then
                            'If drpPlainId.ToString().Length <= 0 Then
                            'If Request("txtPlainId").ToString().Length <= 0 Then
                            ' hdNPID.Value = ""
                            ' txtPlainId.Text = ""
                            lblError.Text = "For the selected Order Type Plan Id is mandatory."
                            If rdlNewCancel.SelectedIndex = 0 Then
                                BindOrderType("New")
                            Else
                                BindOrderType("Cancel")
                            End If
                            ddlOrderType.SelectedValue = objInputOrderTypeXml.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText
                            Exit Sub
                        End If
                    End If


                End If
                ' code check for pc type
                If (objOutputOrderTypeXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("ForPCType").Value = "Agency") Then
                    If (txtAgencyPcReq.Text.Trim().Length <= 0) Then
                        lblError.Text = "Agency Pc is required for the selected Order Type"
                        txtAgencyPcReq.Focus()
                        Exit Sub
                    End If
                    'ElseIf (objOutputOrderTypeXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("ForPCType").Value <> "None") And (objOutputOrderTypeXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("ForPCType").Value <> "") Then
                ElseIf (objOutputOrderTypeXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("ForPCType").Value = "Amadeus") Then
                    If (txtAmadeusPcReq.Text.Trim().Length <= 0) Then
                        lblError.Text = "Amadeus Pc is required for the selected Order Type"
                        txtAmadeusPcReq.Focus()
                        Exit Sub
                    End If
                End If



                

                ''**************************************ashish on 10-02-2011*********************************************
                Dim strInputText As String
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


                '@ ##############################################################
                Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                Dim objbzOrder As New AAMS.bizTravelAgency.bzOrder

                objInputXml.LoadXml("<MS_UPDATEORDERS_INPUT><ORDERS ORDERID='' ORDERTYPEID='' LCODE='' ORDER_NUMBER='' ORDERSTATUSID='' PROCESSEDBYID='' PLANID='' ISPID='' RESEND_DATE_MKT='' RECEIVED_DATE_MKT='' INSTALLATION_DUE_DATE='' EXPECTED_INSTALLATION_DATE='' APR='' OPC='' APC='' NewOrder='' OFFICEID1='' OFFICEID='' RECEIVING_OFFICEID='' MSG_SEND_DATE='' APPLIED_DATE='' SENDBACK_DATE='' APPROVAL_DATE=''  RECEIVED_DATE='' PROCESSED_DATE='' REMARKS='' PENDINGWITHID='' EMPLOYEEID='' ATID='' COMP_VERTICAL='' /></MS_UPDATEORDERS_INPUT>")


                With objInputXml.DocumentElement
                    .SelectSingleNode("ORDERS").Attributes("ORDERTYPEID").Value = Request.Form("ddlOrderType")
                    .SelectSingleNode("ORDERS").Attributes("LCODE").Value = Session("Action").ToString().Split("|").GetValue(1) 'hdAgencyNameId.Value
                    .SelectSingleNode("ORDERS").Attributes("ORDER_NUMBER").Value = txtOrderNumber.Text
                    .SelectSingleNode("ORDERS").Attributes("ORDERSTATUSID").Value = ddlOrderStatus.SelectedValue
                    ' .SelectSingleNode("ORDERS").Attributes("PROCESSEDBYID").Value = hdProcessedById.Value
                    ' .SelectSingleNode("ORDERS").Attributes("PLANID").Value = drpPlainId.SelectedItem.Text ' hdNPID.Value 'txtPlainId.Text
                    .SelectSingleNode("ORDERS").Attributes("PLANID").Value = drpPlainId.SelectedValue ' hdNPID.Value 'txtPlainId.Text

                    .SelectSingleNode("ORDERS").Attributes("ISPID").Value = drpIspName.SelectedValue ' hdISPId.Value 'txtIspName.Text


                    .SelectSingleNode("ORDERS").Attributes("RESEND_DATE_MKT").Value = objeAAMS.ConvertTextDate(txtDateMdResending.Text)
                    .SelectSingleNode("ORDERS").Attributes("RECEIVED_DATE_MKT").Value = objeAAMS.ConvertTextDate(txtDateMdReceiving.Text)
                    .SelectSingleNode("ORDERS").Attributes("INSTALLATION_DUE_DATE").Value = ""

                    'If hDCompanyVerticalSelectByUser.Value <> "3" Then
                    .SelectSingleNode("ORDERS").Attributes("COMP_VERTICAL").Value = hDCompanyVerticalSelectByUser.Value
                    'End If
                    'Code Added by Mukund

                    If ddlOrderStatus.SelectedValue = hdOrderStatus.Value Then
                        If (hdDaysforExpected.Value.Trim().Length <> 0) Then
                            If txtDateMessage.Text.Trim().Length <> 0 Then
                                Dim dtMessagedate As New DateTime
                                dtMessagedate = Date.ParseExact(txtDateMessage.Text, "dd/MM/yyyy", Nothing)
                                'dtMessagedate = Convert.ToDateTime("#" & txtDateMessage.Text & "#")
                                'Dim dtExptDate As New DateTime
                                'dtExptDate = Convert.ToDateTime(OrderStatusDays.Value.Trim()).ToShortDateString()

                                ' Dim lngDays As Integer = DateDiff(DateInterval.Day, dtExptDate, dtMessagedate)
                                dtMessagedate = DateAdd(DateInterval.Day, Convert.ToInt32(hdDaysforExpected.Value.Trim()), dtMessagedate)
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

                    'Code Added by Mukund


                    '.SelectSingleNode("ORDERS").Attributes("EXPECTED_INSTALLATION_DATE").Value = objeAAMS.ConvertTextDate(txtDateExp.Text)

                    .SelectSingleNode("ORDERS").Attributes("APR").Value = txtAmadeusPrinterReq.Text
                    .SelectSingleNode("ORDERS").Attributes("OPC").Value = txtAgencyPcReq.Text
                    .SelectSingleNode("ORDERS").Attributes("APC").Value = txtAmadeusPcReq.Text
                    ' .SelectSingleNode("ORDERS").Attributes("NewOrder").Value = rdlNewCancel.SelectedValue
                    If (Request("rdlNewCancel") IsNot Nothing) Then
                        Dim strNewcancel As String = Request("rdlNewCancel")
                        .SelectSingleNode("ORDERS").Attributes("NewOrder").Value = strNewcancel 'rdlNewCancel.SelectedValue
                    Else
                        .SelectSingleNode("ORDERS").Attributes("NewOrder").Value = rdlNewCancel.SelectedValue
                    End If
                    If (Request("txtOfficeID1") IsNot Nothing) Then
                        .SelectSingleNode("ORDERS").Attributes("OFFICEID").Value = Request("txtOfficeID1")
                    Else
                        .SelectSingleNode("ORDERS").Attributes("OFFICEID").Value = txtOfficeID1.Text
                    End If
                    '   .SelectSingleNode("ORDERS").Attributes("OFFICEID").Value = txtOfficeID1.Text
                    .SelectSingleNode("ORDERS").Attributes("OFFICEID1").Value = txtOfficeID2.Text
                    .SelectSingleNode("ORDERS").Attributes("RECEIVING_OFFICEID").Value = txtReceivingOfficeID.Text
                    .SelectSingleNode("ORDERS").Attributes("MSG_SEND_DATE").Value = objeAAMS.ConvertTextDate(txtDateMessage.Text)
                    .SelectSingleNode("ORDERS").Attributes("APPLIED_DATE").Value = objeAAMS.ConvertTextDate(txtDateApplied.Text)
                    .SelectSingleNode("ORDERS").Attributes("SENDBACK_DATE").Value = objeAAMS.ConvertTextDate(txtDateSentBack.Text)
                    .SelectSingleNode("ORDERS").Attributes("APPROVAL_DATE").Value = objeAAMS.ConvertTextDate(txtDateApproval.Text)
                    .SelectSingleNode("ORDERS").Attributes("RECEIVED_DATE").Value = objeAAMS.ConvertTextDate(txtDateReceived.Text)
                    '.SelectSingleNode("ORDERS").Attributes("PROCESSED_DATE").Value = objeAAMS.ConvertTextDate(txtDateProcessed.Text)
                    .SelectSingleNode("ORDERS").Attributes("REMARKS").Value = txtremarks.Text
                    .SelectSingleNode("ORDERS").Attributes("PENDINGWITHID").Value = hdPendingWithId.Value
                    .SelectSingleNode("ORDERS").Attributes("ATID").Value = strFinal   ''added by ashish on date 10-02-2011


                    If Not Session("LoginSession") Is Nothing Then
                        .SelectSingleNode("ORDERS").Attributes("EMPLOYEEID").Value = Session("LoginSession").ToString().Split("|")(0)
                        If ViewState("ORDERID") Is Nothing Then
                            .SelectSingleNode("ORDERS").Attributes("PROCESSEDBYID").Value = Session("LoginSession").ToString().Split("|")(0)
                        Else
                            .SelectSingleNode("ORDERS").Attributes("PROCESSEDBYID").Value = hdProcessedById.Value
                        End If
                    End If


                    If Not ViewState("ORDERID") Is Nothing Then
                        '  .SelectSingleNode("ORDERS").Attributes("ACTION").Value = "U"
                        .SelectSingleNode("ORDERS").Attributes("ORDERID").Value = ViewState("ORDERID")
                    Else
                        ' .SelectSingleNode("ORDERS").Attributes("ACTION").Value = "I"
                        .SelectSingleNode("ORDERS").Attributes("ORDERID").Value = ""
                    End If
                End With


                objOutputXml = objbzOrder.Update(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If Not ViewState("ORDERID") Is Nothing Then
                        lblError.Text = objeAAMSMessage.messUpdate
                        ' ViewState("ORDERID") = Nothing
                        '   btnSendMail.Enabled = False
                        AgencyOrderEdit(ViewState("ORDERID").ToString())

                    Else
                        AgencyOrderEdit(objOutputXml.DocumentElement.SelectSingleNode("ORDERS").Attributes("ORDERID").Value)
                        'Response.Redirect("TAUP_AgencyOrder.asp?Action=US|" & objOutputXml.DocumentElement.SelectSingleNode("ORDERS").Attributes("ORDERID").Value)
                        lblError.Text = objeAAMSMessage.messInsert
                    End If
                    GetOrder()
                    'txtIspName.Text = Request.Form("txtIspName")
                    ' txtProcessedBy.Text = Request.Form("txtProcessedBy")
                    'txtProcessedBy.Text = Request.Form("txtProcessedBy")

                    hdISPId.Value = Request.Form("hdISPId")
                    hdIspNameId.Value = Request.Form("hdIspNameId")

                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            ' #######################################
            ' ########## This code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Order']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Order']").Attributes("Value").Value)
                    If strBuilder(2) = "1" Then
                        btnSave.Enabled = True
                    End If
                End If
            End If
            ' #######################################
            ' ########## End of code used for enable/disable 
            ' ########## the button according to rights
            GetCompanyVertical(1)

        End Try
    End Sub


    Sub BindOrderType(ByVal filloption As String)
        Try

            ddlOrderType.Items.Clear()
            Dim objbzOrderType As New AAMS.bizTravelAgency.bzOrderType
            Dim objOutputXml As New XmlDocument
            Dim objInputXml As New XmlDocument
            objInputXml.LoadXml("<UP_LISTORDERTYPE_INPUT><ORDER_TYPE></ORDER_TYPE></UP_LISTORDERTYPE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ORDER_TYPE").InnerText = 0
            Dim objNodeList As XmlNodeList
            objOutputXml = objbzOrderType.List(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                hdOrderTypeNew.Value = objOutputXml.OuterXml
                If filloption = "New" Then
                    objNodeList = objOutputXml.SelectNodes("UP_LISTORDERTYPE_OUTPUT/ORDER_TYPE")
                    For Each objXmlNode As XmlNode In objNodeList
                        ddlOrderType.Items.Add(New ListItem(objXmlNode.Attributes("ORDER_TYPE_NAME").InnerText, objXmlNode.Attributes("ORDERTYPEID").InnerText))
                    Next
                End If
            End If

            objInputXml.DocumentElement.SelectSingleNode("ORDER_TYPE").InnerText = 1
            objOutputXml = objbzOrderType.List(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                hdOrderTypeCancel.Value = objOutputXml.OuterXml
                If filloption = "Cancel" Then
                    objNodeList = objOutputXml.SelectNodes("UP_LISTORDERTYPE_OUTPUT/ORDER_TYPE")
                    For Each objXmlNode As XmlNode In objNodeList
                        ddlOrderType.Items.Add(New ListItem(objXmlNode.Attributes("ORDER_TYPE_NAME").InnerText, objXmlNode.Attributes("ORDERTYPEID").InnerText))
                    Next
                End If
            End If
            ddlOrderType.Items.Insert(0, New ListItem("Select One", ""))
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    'Sub ViewOrder()
    '    Try

    '        Dim objInputXml, objOutputXml As New XmlDocument
    '        'Dim objXmlReader As XmlNodeReader
    '        Dim objbzOrder As New AAMS.bizTravelAgency.bzOrder
    '        objInputXml.LoadXml("<MS_VIEWORDERS_INPUT><ORDERID></ORDERID></MS_VIEWORDERS_INPUT>")
    '        objInputXml.DocumentElement.SelectSingleNode("ORDERID").InnerText = Session("Action").ToString().Split("|").GetValue(1).ToString()
    '        'Here Back end Method Call
    '        objOutputXml = objbzOrder.View(objInputXml)

    '        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '            With objOutputXml.DocumentElement.SelectSingleNode("ORDERS")
    '                If .SelectSingleNode("ORDERS").Attributes("NewOrder").InnerText = "f" Then
    '                    BindOrderType("Cancel")
    '                End If
    '                ddlOrderType.SelectedValue = .Attributes("ORDERTYPEID").InnerText
    '                hdAgencyNameId.Value = .Attributes("LCODE").InnerText
    '                txtAgencyName.Text = .Attributes("NAME").InnerText
    '                txtAgencyAddress.Text = .Attributes("ADDRESS").InnerText
    '                txtOrderNumber.Text = .Attributes("ORDER_NUMBER").InnerText
    '                ddlOrderStatus.SelectedValue = .Attributes("ORDERSTATUSID").InnerText
    '                hdProcessedById.Value = .Attributes("PROCESSEDBYID").InnerText
    '                txtProcessedBy.Text = .Attributes("PROCESSEDBYNAME").InnerText
    '                txtPlainId.Text = .Attributes("PLANID").InnerText
    '                txtIspName.Text = .Attributes("ISPNAME").InnerText
    '                hdIspNameId.Value = .Attributes("ISPID").InnerText
    '                If .Attributes("RESEND_DATE_MKT").InnerText <> "" Then
    '                    txtDateMdResending.Text = objeAAMS.ConvertDate(.Attributes("RESEND_DATE_MKT").InnerText).ToString("dd/MM/yyyy")
    '                End If
    '                If .Attributes("RECEIVED_DATE_MKT").InnerText <> "" Then
    '                    txtDateMdReceiving.Text = objeAAMS.ConvertDate(.Attributes("RECEIVED_DATE_MKT").InnerText).ToString("dd/MM/yyyy")
    '                End If

    '                txtDateExp.Text = objeAAMS.ConvertDate(.Attributes("EXPECTED_INSTALLATION_DATE").InnerText).ToString("dd/MM/yyyy")
    '                txtAmadeusPrinterReq.Text = .Attributes("APR").InnerText
    '                txtAgencyPcReq.Text = .Attributes("OPC").InnerText
    '                txtAmadeusPcReq.Text = .Attributes("APC").InnerText
    '                rdlNewCancel.SelectedValue = .Attributes("NewOrder").InnerText
    '                txtOfficeID1.Text = .Attributes("OFFICEID").InnerText
    '                txtOfficeID2.Text = .Attributes("OFFICEID1").InnerText
    '                txtReceivingOfficeID.Text = .Attributes("RECEIVING_OFFICEID").InnerText
    '                txtDateMessage.Text = .Attributes("MSG_SEND_DATE").InnerText
    '                txtDateApplied.Text = .Attributes("APPLIED_DATE").InnerText
    '                If .Attributes("SENDBACK_DATE").InnerText <> "" Then
    '                    txtDateSentBack.Text = objeAAMS.ConvertDate(.Attributes("SENDBACK_DATE").InnerText).ToString("dd/MM/yyyy")
    '                End If
    '                If .Attributes("APPROVAL_DATE").InnerText <> "" Then
    '                    txtDateApproval.Text = objeAAMS.ConvertDate(.Attributes("APPROVAL_DATE").InnerText).ToString("dd/MM/yyyy")
    '                End If
    '                If .Attributes("RECEIVED_DATE").InnerText <> "" Then
    '                    txtDateReceived.Text = objeAAMS.ConvertDate(.Attributes("RECEIVED_DATE").InnerText).ToString("dd/MM/yyyy")
    '                End If
    '                If .Attributes("PROCESSED_DATE").InnerText <> "" Then
    '                    txtDateProcessed.Text = objeAAMS.ConvertDate(.Attributes("PROCESSED_DATE").InnerText).ToString("dd/MM/yyyy")
    '                End If

    '                txtremarks.Text = .Attributes("REMARKS").InnerText
    '                hdPendingWithId.Value = .Attributes("PENDINGWITHID").InnerText
    '                txtPendingWith.Text = .Attributes("PENDINGWITHNAME").InnerText
    '                'hdAgencyOrderId.Value = Session("Action").ToString().Split("|").GetValue(1).ToString()
    '                btnSendMail.Enabled = True

    '                ' #######################################
    '                ' ########## This code is used for enable/disable 
    '                ' ########## the button according to rights
    '                Dim objSecurityXml As New XmlDocument
    '                objSecurityXml.LoadXml(Session("Security"))
    '                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
    '                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Order']").Count <> 0 Then
    '                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Order']").Attributes("Value").Value)
    '                        If strBuilder(2) = "0" Then
    '                            btnSave.Enabled = False
    '                        End If

    '                    End If
    '                End If
    '            End With

    '        Else
    '            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '    End Try
    'End Sub
    Sub GetOrder()
        Try
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Order']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Order']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Exit Sub
                    End If
                End If
            End If
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            '            Dim objNode2 As XmlNode
            Dim objbzOrder As New AAMS.bizTravelAgency.bzOrder
            Dim ds As New DataSet
            ' objInputXml.LoadXml("<MS_GETORDERS_INPUT><LCODE /></MS_GETORDERS_INPUT>")
            objInputXml.LoadXml("<MS_GETORDERS_INPUT><LCODE /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_GETORDERS_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = Session("Action").ToString().Split("|").GetValue(1).ToString()

            '@ Coding For Paging Ansd sorting
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
                ViewState("SortName") = "ORDER_NUMBER"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "ORDER_NUMBER" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "TRUE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            'End Code for paging and sorting


            'Here Back end Method Call
            objOutputXml = objbzOrder.GetDetails(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml

                'For Each objNode2 In objOutputXml.DocumentElement.SelectNodes("ORDERS")
                '    objNode2.Attributes("APPROVAL_DATE").Value = objeAAMS.ConvertDateBlank(objNode2.Attributes("APPROVAL_DATE").Value)
                'Next
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdOrder.DataSource = ds.Tables("ORDERS")
                grdOrder.DataBind()

                ' ##################################################################
                '@ Code Added For Paging And Sorting In case Of 
                ' ###################################################################
                'BindControlsForNavigation(2)
                BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                SetImageForSorting(grdOrder)
                ' ###################################################################
                '@ End of Code Added For Paging And Sorting In case 
                ' ###################################################################

                pnlPaging.Visible = True

            Else
                grdOrder.DataSource = String.Empty
                grdOrder.DataBind()
                pnlPaging.Visible = False
                txtTotalRecordCount.Text = "0"
                'lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            pnlPaging.Visible = False
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            '  If (Session("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Session("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
            If (ViewState("ORDERID") IsNot Nothing) Then
                cleardata()
                ' ViewOrder()
                AgencyOrderEdit(ViewState("ORDERID").ToString())
                lblError.Text = ""
            Else
                cleardata()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    Private Sub cleardata()
        Try

            txtDateMdReceiving.Enabled = True
            txtDateMdResending.Enabled = True
            txtDateMdReceiving.CssClass = "textbox"
            txtDateMdResending.CssClass = "textbox"
            imgDateMdReceiving.Visible = True
            imgDateMdReceiving2.Visible = False
            imgDateMdResending.Visible = True
            imgDateMdResending2.Visible = False

            ddlOrderType.SelectedIndex = 0
            'hdAgencyNameId.Value = ""
            'txtAgencyName.Text = ""
            'txtAgencyAddress.Text = ""
            txtOrderNumber.Text = ""

            ddlOrderStatus.SelectedIndex = 0
            hdProcessedById.Value = ""
            'ddlProcessedBy.SelectedIndex = -1
            txtProcessedBy.Text = ""
            'txtPlainId.Text = ""
            drpPlainId.SelectedValue = ""
            'txtIspName.Text = ""
            drpIspName.SelectedIndex = 0
            hdIspNameId.Value = ""
            txtDateMdResending.Text = ""
            txtDateMdReceiving.Text = ""
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
            rdlNewCancel.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub



    Private Sub AgencyOrderEdit(ByVal strOrderId As String)
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            'Dim objXmlReader As XmlNodeReader
            Dim objbzOrder As New AAMS.bizTravelAgency.bzOrder
            objInputXml.LoadXml("<MS_VIEWORDERS_INPUT>	<ORDERID></ORDERID></MS_VIEWORDERS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ORDERID").InnerText = strOrderId ' Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()
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
                    ViewState("ORDERID") = strOrderId
                    txtOrderNumber.Text = .SelectSingleNode("ORDERS").Attributes("ORDER_NUMBER").InnerText
                    If .SelectSingleNode("ORDERS").Attributes("NewOrder").InnerText = "T" Then
                        BindOrderType("New")
                    Else
                        BindOrderType("Cancel")
                    End If
                    'If .SelectSingleNode("ORDERS").Attributes("NewOrder").InnerText = "0" Then
                    '    BindOrderType("Cancel")
                    'Else
                    '    BindOrderType("New")
                    'End If
                    Dim li6 As ListItem = ddlOrderType.Items.FindByValue(.SelectSingleNode("ORDERS").Attributes("ORDERTYPEID").InnerText)
                    If li6 IsNot Nothing Then
                        ddlOrderType.SelectedValue = li6.Value ' .SelectSingleNode("ORDERS").Attributes("PLANID").InnerText
                    End If

                    'ddlOrderType.SelectedValue = .SelectSingleNode("ORDERS").Attributes("ORDERTYPEID").InnerText
                    If (.SelectSingleNode("ORDERS").Attributes("NewOrder").InnerText = "T") Then
                        rdlNewCancel.SelectedValue = "T"
                        pnlATID.Visible = True
                        txtATID.Visible = True
                        txtATID.Text = .SelectSingleNode("ORDERS").Attributes("ATID").InnerText

                    Else
                        rdlNewCancel.SelectedValue = "f"
                        txtATID.Text = ""
                        pnlATID.Visible = False
                        txtATID.Visible = False

                    End If
                    rdlNewCancel.Enabled = False
                    IspNameEnableorDisable()
                    ' rdlNewCancel.SelectedValue = .SelectSingleNode("ORDERS").Attributes("NewOrder").InnerText
                    hdAgencyNameId.Value = .SelectSingleNode("ORDERS").Attributes("LCODE").InnerText
                    txtAgencyName.Text = .SelectSingleNode("ORDERS").Attributes("NAME").InnerText
                    txtAgencyAddress.Text = .SelectSingleNode("ORDERS").Attributes("ADDRESS").InnerText & " "

                    ddlOrderStatus.SelectedValue = .SelectSingleNode("ORDERS").Attributes("ORDERSTATUSID").InnerText
                    hdProcessedById.Value = .SelectSingleNode("ORDERS").Attributes("PROCESSEDBYID").InnerText
                    txtProcessedBy.Text = .SelectSingleNode("ORDERS").Attributes("PROCESSEDBYNAME").InnerText



                    ' txtIspName.Text = .SelectSingleNode("ORDERS").Attributes("ISPNAME").InnerText
                    'txtIspName.Text = "" '.SelectSingleNode("ORDERS").Attributes("ISPName").InnerText ' .SelectSingleNode("ORDERS").Attributes("ISPID").InnerText
                    hdIspNameId.Value = .SelectSingleNode("ORDERS").Attributes("ISPNAME").InnerText ' .SelectSingleNode("ORDERS").Attributes("ISPID").InnerText

                    hdISPId.Value = .SelectSingleNode("ORDERS").Attributes("ISPID").InnerText

                    Dim li5 As ListItem = drpIspName.Items.FindByValue(.SelectSingleNode("ORDERS").Attributes("ISPID").InnerText)
                    If li5 IsNot Nothing Then
                        drpIspName.SelectedValue = li5.Value ' .SelectSingleNode("ORDERS").Attributes("PLANID").InnerText
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


                    hdNPID.Value = .SelectSingleNode("ORDERS").Attributes("PLANID").InnerText





                    txtDateMdResending.Text = objeAAMS.ConvertDateBlank(.SelectSingleNode("ORDERS").Attributes("RESEND_DATE_MKT").InnerText)
                    txtDateMdReceiving.Text = objeAAMS.ConvertDateBlank(.SelectSingleNode("ORDERS").Attributes("RECEIVED_DATE_MKT").InnerText)
                    ' "" = .SelectSingleNode("ORDERS").Attributes("INSTALLATION_DUE_DATE").InnerText
                    txtDateExp.Text = objeAAMS.ConvertDateBlank(.SelectSingleNode("ORDERS").Attributes("EXPECTED_INSTALLATION_DATE").InnerText)
                    txtAmadeusPrinterReq.Text = .SelectSingleNode("ORDERS").Attributes("APR").InnerText
                    txtAgencyPcReq.Text = .SelectSingleNode("ORDERS").Attributes("OPC").InnerText
                    txtAmadeusPcReq.Text = .SelectSingleNode("ORDERS").Attributes("APC").InnerText

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
                    '  ViewState("ORDERID") = strOrderId
                    Session("AgencyOrderMail") = strOrderId.ToString()

                    btnSendMail.Enabled = True
                    btnMail.Enabled = True
                    'Panel1.Visible = True
                    '  btnOrder.Attributes.Add("onclick", "return  NewFunctionAorder();")


                End With


                ' #######################################
                ' ########## This code is used for enable/disable 
                ' ########## the button according to rights
                Dim objSecurityXml As New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Order']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Order']").Attributes("Value").Value)
                        If strBuilder(2) = "1" Then
                            btnSave.Enabled = True
                        End If
                        If strBuilder(2) = "0" Then
                            btnSave.Enabled = False
                            Session("AgencyOrderMail") = Nothing
                        End If

                    End If
                End If
                ' #######################################
                ' ########## End of code used for enable/disable 
                ' ########## the button according to rights

                ' #######################################
                ' ########## This code is used for enable/disable 
                ' ########## the button according to rights

                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Date Mkt']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Date Mkt']").Attributes("Value").Value)
                        If strBuilder(2) = "0" Then
                            txtDateMdReceiving.Enabled = False
                            txtDateMdResending.Enabled = False
                            txtDateMdReceiving.CssClass = "textboxgrey"
                            txtDateMdResending.CssClass = "textboxgrey"
                            imgDateMdReceiving.Visible = False
                            imgDateMdReceiving2.Visible = True
                            imgDateMdResending.Visible = False
                            imgDateMdResending2.Visible = True
                        End If
                    Else
                        txtDateMdReceiving.Enabled = True
                        txtDateMdResending.Enabled = True
                        txtDateMdReceiving.CssClass = "textbox"
                        txtDateMdResending.CssClass = "textbox"
                        imgDateMdReceiving.Visible = True
                        imgDateMdReceiving2.Visible = False
                        imgDateMdResending.Visible = True
                        imgDateMdResending2.Visible = False

                    End If
                Else
                    txtDateMdReceiving.Enabled = True
                    txtDateMdResending.Enabled = True
                    txtDateMdReceiving.CssClass = "textbox"
                    txtDateMdResending.CssClass = "textbox"
                    imgDateMdReceiving.Visible = True
                    imgDateMdReceiving2.Visible = False
                    imgDateMdResending.Visible = True
                    imgDateMdResending2.Visible = False
                End If
                ' #######################################
                ' ########## End of code used for enable/disable 
                ' ########## the button according to rights

                '@ Code For Readonly Or Write For IspName and NPID on the basis of Users Write
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
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Sub AgencyOrderDelete(ByVal strOrderId As String)
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbzOrder As New AAMS.bizTravelAgency.bzOrder
            objInputXml.LoadXml("<MS_DELETEORDER_INPUT>	<ORDERID/></MS_DELETEORDER_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ORDERID").InnerXml = strOrderId
            'Here Back end Method Call
            objOutputXml = objbzOrder.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                cleardata()
                GetOrder()
                '  btnSendMail.Enabled = False
                lblError.Text = objeAAMSMessage.messDelete
                'Response.Redirect("MSSR_ORDER.aspx")
                If (ViewState("ORDERID") IsNot Nothing) Then
                    ViewState("ORDERID") = Nothing
                End If
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub




    Protected Sub btnSendMail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSendMail.Click
        Try

            'Dim objInputXml, objOutputXml As New XmlDocument
            'Dim objInputXmlForMail, objOutputXmlForMail As New XmlDocument
            'Dim objInputXml2, objOutputXml2 As New XmlDocument

            'Dim objInputTempXml, objOutputTempXml As New XmlDocument

            ''Dim objXmlReader As XmlNodeReader
            'Dim blnGroupISP As Boolean
            'Dim blnMNC As Boolean
            'Dim strAoffice As String = ""

            'Dim objbzOrderType As New AAMS.bizTravelAgency.bzOrderType
            'Dim objOrderSendmail As New AAMS.bizMaster.bzEmailGroup
            'Dim objAgencyMNC As New AAMS.bizTravelAgency.bzAgency
            'Dim objMailTemplate As New AAMS.bizMaster.bzEmailTemplates


            '' @ Code for GroupISP on the basis of Order Type
            'objInputXml.LoadXml("<MS_VIEWORDERTYPE_INPUT><ORDERTYPEID></ORDERTYPEID></MS_VIEWORDERTYPE_INPUT>")
            'objInputXml.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText = ddlOrderType.SelectedValue
            ''Here Back end Method Call
            'objOutputXml = objbzOrderType.View(objInputXml)
            'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '    blnGroupISP = objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsISPOrder").Value
            'End If

            '' @End of  Code for GroupISP on the basis of Order Type

            '' @ Code for MNC/NON-MNC BY LCODE on the basis of Order Type
            'objInputXml2.LoadXml("<UP_GETAGENCYGROUP_INPUT><LOCATION_CODE></LOCATION_CODE></UP_GETAGENCYGROUP_INPUT>")
            'objInputXml2.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = Session("Action").ToString().Split("|").GetValue(1).ToString()
            ''Here Back end Method Call
            'objOutputXml2 = objAgencyMNC.AgencyGroup_Type(objInputXml2)
            'If objOutputXml2.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '    ' blnGroupISP = objOutputXml2.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsISPOrder").Value
            '    If (objOutputXml2.DocumentElement.SelectSingleNode("AGENCYGROUPTYPE").Attributes("GroupTypeID").Value() = "1") Then
            '        blnMNC = True
            '    Else
            '        blnMNC = False
            '    End If
            '    If (objOutputXml2.DocumentElement.SelectSingleNode("AGENCYGROUPTYPE").Attributes("GroupTypeID").Value() = "1") Then
            '        strAoffice = objOutputXml2.DocumentElement.SelectSingleNode("AGENCYGROUPTYPE").Attributes("AOffice").Value()
            '    Else
            '        strAoffice = objOutputXml2.DocumentElement.SelectSingleNode("AGENCYGROUPTYPE").Attributes("AOffice").Value() ' ""
            '    End If
            'End If


            '' @ End of Code for MNC/NON-MNC BY LCODE on the basis of Order Type

            'objInputXmlForMail.LoadXml("<MS_GETEMAIL_INPUT><GROUPDETAIL GroupMNC='' GroupISP='' GroupTraining='' GroupAoffice='' Aoffice='' TrainingAoffice='' /></MS_GETEMAIL_INPUT>")
            'With objInputXmlForMail.DocumentElement.SelectSingleNode("GROUPDETAIL")

            '    .Attributes("GroupAoffice").Value() = 0
            '    .Attributes("GroupTraining").Value() = 0
            '    .Attributes("TrainingAoffice").Value() = ""
            '    If blnMNC = True Then
            '        .Attributes("GroupMNC").Value() = 1
            '        .Attributes("Aoffice").Value() = ""
            '    Else
            '        .Attributes("GroupMNC").Value() = 0
            '        .Attributes("GroupAoffice").Value() = 1
            '        .Attributes("Aoffice").Value() = strAoffice

            '    End If
            '    If (blnGroupISP = True) Then
            '        .Attributes("GroupISP").Value() = 1
            '    Else
            '        .Attributes("GroupISP").Value() = 0
            '    End If

            'End With
            'objOutputXmlForMail = objOrderSendmail.GetEmailID2s(objInputXmlForMail)
            ''Here Back end Method Call
            '' objOutputXml = objbzOrder.Delete(objInputXml)
            'If objOutputXmlForMail.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '    ' @ Code for MNC/NON-MNC BY LCODE on the basis of Order Type
            '    objInputTempXml.LoadXml("<MS_VIEWEMAILTEMPLATES_INPUT><TEMPLATES MailTemplateName='' /></MS_VIEWEMAILTEMPLATES_INPUT>")
            '    objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("MailTemplateName").Value() = "ORDER_MAIL" ' Session("Action").ToString().Split("|").GetValue(1).ToString()
            '    'Here Back end Method Call
            '    objOutputTempXml = objMailTemplate.View(objInputTempXml)
            '    If objOutputTempXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

            '    End If
            'End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Protected Sub btnOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOrder.Click
        btnMail.CssClass = "headingtabactive"
        If (ViewState("ORDERID") IsNot Nothing) Then
            cleardata()
            ' ViewOrder()
            AgencyOrderEdit(ViewState("ORDERID").ToString())
            lblError.Text = ""
        Else
            cleardata()
            btnMail.Enabled = False
        End If

    End Sub

    Protected Sub btnMail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMail.Click
        Try
            If Not ViewState("ORDERID") Is Nothing Then
                Response.Redirect("TAUP_AgencyMailingList.aspx?Id=7&Action=U|" + ViewState("ORDERID").ToString(), False)
            End If

        Catch ex As Exception

        End Try
    End Sub
    Sub BindDropDownIspName(ByVal drpDownList As DropDownList, ByVal strType As String, ByVal bolSelect As Boolean)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim objInputXmlAgency, objOutputXmlAgency As New XmlDocument
        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
        Dim objbzISP As New AAMS.bizISP.bzISP
        Dim ds As New DataSet
        Dim Lcode As String
        drpDownList.Items.Clear()
        Select Case strType
            Case "ISPLISTByAgencyCity"
                If (Session("Action") IsNot Nothing) Then
                    If Session("Action").ToString().Split("|").GetValue(1) IsNot Nothing Then
                        Lcode = Session("Action").ToString().Split("|").GetValue(1)
                        objInputXmlAgency.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")
                        objInputXmlAgency.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerXml = Session("Action").ToString().Split("|").GetValue(1)

                        'Here Back end Method Call
                        objOutputXmlAgency = objbzAgency.View(objInputXmlAgency)

                        If objOutputXmlAgency.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            With objOutputXmlAgency.DocumentElement.SelectSingleNode("Agency")

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
                            End With
                            If bolSelect = True Then
                                drpDownList.Items.Insert(0, New ListItem("", ""))
                            End If
                        End If
                    End If
                End If
        End Select
        IspNameEnableorDisable()
    End Sub

    Protected Sub rdlNewCancel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdlNewCancel.SelectedIndexChanged
        'txtPlainId.Text = ""
        hdNPID.Value = ""
        SelectOrderType()
        IspNameEnableorDisable()

        If rdlNewCancel.SelectedValue = "f" Then
            ''added by ashish on date 09-02-2011
            pnlATID.Visible = False
            txtATID.Visible = False
        End If

        If rdlNewCancel.SelectedValue = "T" Then
            ''added by ashish on date 09-02-2011
            pnlATID.Visible = True
            txtATID.Visible = True
        End If
        
    End Sub
    Public Sub SelectOrderType()
        Try
            If rdlNewCancel.SelectedIndex = 0 Then
                BindOrderType("New")
            Else
                BindOrderType("Cancel")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlOrderType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOrderType.SelectedIndexChanged
        Try
            drpPlainId.Items.Clear()
            'txtPlainId.Text = ""
            hdNPID.Value = ""
            IspNameEnableorDisable()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub IspNameEnableorDisable()
        '@ ##################################################################################
        '@ Code to check for  Isp checked/not on the basis of ordertypeid
        ' @ Code for GroupISP on the basis of Order Type
        'txtPlainId.Text = ""
        hdNPID.Value = ""
        Dim objInputOrderTypeXml As New XmlDataDocument, objOutputOrderTypeXml As New XmlDocument
        Dim objbzOrderType As New AAMS.bizTravelAgency.bzOrderType

        Dim blnGroupISP As Boolean = False
        objInputOrderTypeXml.LoadXml("<MS_VIEWORDERTYPE_INPUT><ORDERTYPEID></ORDERTYPEID></MS_VIEWORDERTYPE_INPUT>")

        objInputOrderTypeXml.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText = ddlOrderType.SelectedValue


        'Here Back end Method Call
        objOutputOrderTypeXml = objbzOrderType.View(objInputOrderTypeXml)
        If objOutputOrderTypeXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

            If objOutputOrderTypeXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("EXPECTED_INSTALLATION_DATE").Value <> "" Then
                'txtDateExp.Text = objOutputOrderTypeXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("EXPECTED_INSTALLATION_DATE").Value
            End If

            hdDaysforExpected.Value = objOutputOrderTypeXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("TimeRequired").InnerText.Trim

            blnGroupISP = objOutputOrderTypeXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsISPOrder").Value
        End If
        If (blnGroupISP = True) Then
            hdIspOrder.Value = "1"
            drpIspName.Enabled = True
            drpPlainId.Enabled = True
        Else
            hdIspOrder.Value = "0"
            drpIspName.Enabled = False
            drpPlainId.Enabled = False
            drpPlainId.SelectedValue = ""
            drpIspName.SelectedValue = ""
        End If

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

    End Sub

    Protected Sub drpIspName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpIspName.SelectedIndexChanged
        GetNPID()
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
                drpPlainId.Items.Insert(0, New ListItem("Select one", ""))
            End If
            '@ Code For Readonly Or Write For IspName and NPID on the basis of Users Write
            Dim objSecurityXml As New XmlDocument

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


#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            GetOrder()
            If Request.QueryString("EditOrderId") IsNot Nothing Then
                AgencyOrderEdit(Request.QueryString("EditOrderId").ToString())
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            GetOrder()
            If Request.QueryString("EditOrderId") IsNot Nothing Then
                AgencyOrderEdit(Request.QueryString("EditOrderId").ToString())
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            GetOrder()
            If Request.QueryString("EditOrderId") IsNot Nothing Then
                AgencyOrderEdit(Request.QueryString("EditOrderId").ToString())
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdOrder_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdOrder.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdOrder_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdOrder.Sorting
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
            GetOrder()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub BindControlsForNavigation(ByVal CrrentPageNo As String)
        ' ##################################################################
        '@ Code Added For Paging And Sorting
        ' ###################################################################
        pnlPaging.Visible = True
        '  Dim count As Integer = 0
        Dim count As Integer = CInt(CrrentPageNo)
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

        ' ###################################################################
        '@ End of Code Added For Paging And Sorting 
        ' ###################################################################
    End Sub
    Private Sub SetImageForSorting(ByVal grd As GridView)
        Dim imgUp As New Image
        imgUp.ImageUrl = "~/Images/Sortup.gif"
        Dim imgDown As New Image
        imgDown.ImageUrl = "~/Images/Sortdown.gif"
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Dim intcol As Integer = grd.Columns.IndexOf(field)
                If ViewState("Desc") = "FALSE" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "TRUE" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If
            End If
        Next
    End Sub

#End Region

    Protected Sub grdOrder_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdOrder.RowDataBound
        Try
            If (e.Row.RowIndex < 0) Then
                Exit Sub
            End If
            Dim Lcode As String = Session("Action").ToString().Split("|").GetValue(1).ToString()
            Dim linkHistory, linkViewDoc As System.Web.UI.HtmlControls.HtmlAnchor
            Dim linkPtype As System.Web.UI.HtmlControls.HtmlAnchor


            Dim hdOrderId As HiddenField = CType(e.Row.FindControl("hdOrderId"), HiddenField)
            Dim hdOrderNo As HiddenField = CType(e.Row.FindControl("hdOrderNo"), HiddenField)
            Dim hdOrderType As HiddenField = CType(e.Row.FindControl("hdOrderType"), HiddenField)
            Dim hdOrderQty As HiddenField = CType(e.Row.FindControl("hdOrderQty"), HiddenField)
            Dim hdOrderQtyAPC As HiddenField = CType(e.Row.FindControl("hdOrderQtyAPC"), HiddenField) 'Variable added in 12-10-2010
            Dim hdOrderRemarks As HiddenField = CType(e.Row.FindControl("hdOrderRemarks"), HiddenField)
            Dim hdORDERTYPEID As HiddenField = CType(e.Row.FindControl("hdORDERTYPEID"), HiddenField) 'Variable added in 12-10-2010

            
            Dim fileno As String
            Dim Agencyname As String
            Agencyname = ""
            fileno = ""
            If (Session("Action") IsNot Nothing) Then
                If (Session("Action").ToString().Split("|").Length > 2) Then
                    fileno = Session("Action").ToString().Split("|").GetValue(2).ToString()
                End If
                If (Session("Action").ToString().Split("|").Length > 3) Then
                    Agencyname = Session("Action").ToString().Split("|").GetValue(3).ToString()
                End If

            End If


            Dim linkDelete, linkEdit As LinkButton
            linkDelete = e.Row.FindControl("lnkDelete")
            linkEdit = e.Row.FindControl("lnkEdit")
            'linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdstrPriorityId.Value & "'" & ");")
            'linkDelete.Attributes.Add("onclick", "return DeleteFunctionAorder(" & "'" & hdstrPriorityId.Value & "'" & ");")
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Order']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Order']").Attributes("Value").Value)
                    If strBuilder(3) = "0" Then
                        linkDelete.Enabled = False
                    End If
                    If strBuilder(3) = "1" Then
                        linkDelete.Attributes.Add("onclick", "return DeleteFunctionAorder();")
                    End If
                    If strBuilder(2) = "0" Then
                        linkEdit.Enabled = False
                    End If
                Else
                    linkDelete.Enabled = True
                    linkEdit.Enabled = True
                    linkDelete.Attributes.Add("onclick", "return DeleteFunctionAorder();")

                    'linkDelete.Enabled = False
                    'linkEdit.Enabled = False
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                linkDelete.Enabled = True
                linkEdit.Enabled = True
                linkDelete.Attributes.Add("onclick", "return DeleteFunctionAorder();")

            End If

            linkHistory = e.Row.FindControl("linkHistory")
            linkHistory.Attributes.Add("onclick", "return PopupHistoryPageAorder(" & "'" & hdOrderId.Value & "'" & ");")


            linkViewDoc = e.Row.FindControl("linkViewDoc")
            Agencyname = Agencyname.ToString().Replace("'", "")
            Agencyname = Agencyname.ToString().Replace(vbCrLf, "\n")

            linkPtype = e.Row.FindControl("linkPtype")

            

            Dim args As String = hdOrderNo.Value & "|" & fileno & "|" & Server.UrlEncode(Agencyname)

            'Increase the two  parameter in args1   OrderQtyAPC and IsOrderType on 12-10-2010
            Dim args1 As String = hdOrderType.Value & "|" & hdOrderQty.Value & "|" & hdOrderQtyAPC.Value & "|" & Server.UrlEncode(hdOrderRemarks.Value) & "|" & hdORDERTYPEID.Value

            'var type="OrderNo="+ Var_array[0] + "&Fileno=" + Var_array[1] + "&AgencyName=" +  Var_array[2] + 
            '"&OrderType=" + Var_array1[0] + "&OrderQty=" +Var_array1[1] + "&OrderQtyAPC=" +Var_array1[2] + "&OrderRemark="  + Var_array1[3] +"&IsOrderType" +Var_array1[4];

            Dim strOrderType As String = hdOrderType.Value

            'View Order Document security rights
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='View Order Documents']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='View Order Documents']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        linkViewDoc.Disabled = True
                    Else
                        linkViewDoc.Attributes.Add("onclick", "return ViewDocAorder(" & "'" & args & "'" & ",'" & Lcode & "','" & hdOrderId.Value & "'" & ");")
                    End If
                End If
            Else
                linkViewDoc.Attributes.Add("onclick", "return ViewDocAorder(" & "'" & args & "'" & ",'" & Lcode & "','" & hdOrderId.Value & "'" & ");")
            End If

            ''Start coded by ashish on 12-10-2010
            If CStr(hdOrderQty.Value & "") <> "0" Or CStr(hdORDERTYPEID.Value & "") = "1" Then
                linkPtype.Style.Add("display", "block")
            Else
                linkPtype.Style.Add("display", "none")
                Exit Sub
            End If

            If CStr(hdOrderQty.Value & "") = "0" And CStr(hdOrderQtyAPC.Value & "") = "0" Then
                linkPtype.Style.Add("display", "none")
                Exit Sub
            End If
            'End 


            ' Ptype report security rights
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Ptype report']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Ptype report']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        linkPtype.Disabled = True
                    Else
                        linkPtype.Attributes.Add("onclick", "return ViewPtypeReport(" & "'" & args & "'" & ",'" & args1 & "'" & ");")
                    End If
                End If
            Else
                linkPtype.Attributes.Add("onclick", "return ViewPtypeReport(" & "'" & args & "'" & ",'" & args1 & "'" & ");")
            End If



        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdOrder_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdOrder.RowCommand
        Try
            lblError.Text = ""
            If e.CommandName = "EditX" Then

                AgencyOrderEdit(e.CommandArgument)

            End If
            If e.CommandName = "DeleteX" Then
                AgencyOrderDelete(e.CommandArgument)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Private Sub GetOrderStatus()

        Dim objInputXml, objOutputXml As New XmlDocument
        Try
            Dim objbzOrder As New AAMS.bizTravelAgency.bzOrder
            objInputXml.LoadXml("<IS_SEARCHISPPLAN_INPUT><ISPID></ISPID><Name></Name><CityID></CityID><NPID></NPID><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><ProviderID/></IS_SEARCHISPPLAN_INPUT>")

            objOutputXml = objbzOrder.ListOrderingConfigValue()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                If objOutputXml.DocumentElement.SelectNodes("CONFIGVALUE[@FIELD_NAME='APPROVEDORDERSATUS']").Count <> 0 Then

                    hdOrderStatus.Value = objOutputXml.DocumentElement.SelectSingleNode("CONFIGVALUE[@FIELD_NAME='APPROVEDORDERSATUS']").Attributes("FIELD_VALUE").Value
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub


    'Code Added by Mukund
    Private Sub AssignOrderTypeDays()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzOrderType As New AAMS.bizTravelAgency.bzOrderType
        If ddlOrderType.Items.Count > 0 Then
            objInputXml.LoadXml("<MS_VIEWORDERTYPE_INPUT><ORDERTYPEID></ORDERTYPEID></MS_VIEWORDERTYPE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText = ddlOrderType.SelectedValue.Trim()
            objOutputXml = objbzOrderType.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("EXPECTED_INSTALLATION_DATE").InnerText.Trim <> "" Then
                    hdDaysforExpected.Value = objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("TimeRequired").InnerText.Trim
                End If
            End If
        End If
    End Sub
    Private Sub GetCompanyVertical(ByVal UpdateUserCV As Integer)
        If (Session("Action") IsNot Nothing) Then
            Dim objInputXmlAgency As New XmlDocument
            Dim objOutputXmlAgency As New XmlDocument
            Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
            objInputXmlAgency.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")
            objInputXmlAgency.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = Session("Action").ToString().Split("|").GetValue(1)

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
