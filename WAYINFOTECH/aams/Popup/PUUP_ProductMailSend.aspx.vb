'#######################################################
'############   Page Name --PUUP_ProductMailSend  #########  
'############   Date 15 Jan 2008  #################
'############   Developed By Abhishek  #################
'#######################################################
Imports System.Net.Mail
Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml
Partial Class Popup_PUUP_ProductMailSend
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS

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
        Session("PageName") = Request.Url.ToString()
        'objeAAMS.ExpirePageCache()
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        HttpContext.Current.Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        HttpContext.Current.Response.Cache.SetLastModified(DateTime.Now)
        HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(False)
        Dim strbody As String = ""
        Dim boolBTMailTemplate As Boolean = False
        hdRequestId.Value = ""
        Try

            If (Request.QueryString("RequestId") IsNot Nothing) Then
                If Request.QueryString("MailType") IsNot Nothing Then
                    If Request.QueryString("MailType").Trim = "BT" Then
                        boolBTMailTemplate = True
                    End If
                End If
                hdRequestId.Value = Request.QueryString("RequestId").ToString
                pnlAttach.Visible = True
            Else
                pnlAttach.Visible = False
                hdRequestId.Value = ""
                boolBTMailTemplate = False
            End If


            btnSendMail.Attributes.Add("onclick", "return MailMandatory();")
            btnRemove.Attributes.Add("onclick", "return ResetValueofBody();")
            btnUpLoad.Attributes.Add("onclick", "return ResetValueofBody();")

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            If (Not IsPostBack) Then
                Dim objInputTempXml, objOutputTempXml As New XmlDocument
                Dim objInputXml, objOutputXml As New XmlDocument
                Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
                'Code of Security Check
                Dim objMailTemplate As New AAMS.bizMaster.bzEmailTemplates


                '#####################################################################
                ' @ New Code is added to send mail for ISP Feasibility Request Mail

                objInputTempXml.LoadXml("<MS_VIEWEMAILTEMPLATES_INPUT><TEMPLATES MailTemplateName='' /></MS_VIEWEMAILTEMPLATES_INPUT>")
                If boolBTMailTemplate = False Then
                    objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("MailTemplateName").Value() = "ISP MAILING" ' Session("Action").ToString().Split("|").GetValue(1).ToString()
                Else
                    objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("MailTemplateName").Value() = "BT_EMAILTEMPLATE"
                End If

                'Here Back end Method Call
                objOutputTempXml = objMailTemplate.View(objInputTempXml)
                If objOutputTempXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    txtbody.Text = objOutputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("MailTemplate").Value
                    '###############################################################################
                    '@ Now find the value of Body parts on the basis of Lcode

                    strbody = txtbody.Text
                    If (Request.QueryString("RequestId") IsNot Nothing) Then
                        'hdRequestId.Value = Request.QueryString("RequestId").ToString

                        Dim objtFeasibilityStatusId As New AAMS.bizISP.bzISPFeasibleRequest
                        Dim objInputFeasibilityXml, objOutputFeasibilityXml As New XmlDocument
                        objInputFeasibilityXml.LoadXml("<ISP_VIEWFEASIBILEREQUEST_INPUT><RequestID></RequestID><Name></Name></ISP_VIEWFEASIBILEREQUEST_INPUT>")
                        objInputFeasibilityXml.DocumentElement.SelectSingleNode("Name").InnerXml = String.Empty
                        objInputFeasibilityXml.DocumentElement.SelectSingleNode("RequestID").InnerXml = Request.QueryString("RequestId")

                        objOutputFeasibilityXml = objtFeasibilityStatusId.View(objInputFeasibilityXml)
                        'Here Back end Method Call

                        If objOutputFeasibilityXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then


                            '@ Previous Code For Change Template For Replacing Tag

                            'strbody = strbody.Replace("<<AgencyName>>", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("Name").Value)
                            'strbody = strbody.Replace("<<Address>>", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("ADDRESS").Value)
                            'strbody = strbody.Replace("<<TelNo>>", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("PHONE").Value)
                            'strbody = strbody.Replace("<<ContactPerson>>", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("CONCERNED_PERSON").Value)
                            'strbody = strbody.Replace("<<FAX>>", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("FAX").Value)
                            ''strbody = strbody.Replace("<<OFFICEID>>", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("OFFICEID").Value)
                            'strbody = strbody.Replace("<<Remarks>>", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("Remarks").Value)
                            'If Session("Security") IsNot Nothing Then
                            '    Dim objSecurityXml As New XmlDocument
                            '    objSecurityXml.LoadXml(Session("Security"))
                            '    strbody = strbody.Replace("<<AuthorizedSign>>", " " & objSecurityXml.DocumentElement.SelectSingleNode("Login").InnerText)
                            'End If


                            '@ End of Previous Code For Change Template For Replacing Tag




                            '@ New Code For Change Template For Replacing Tag

                            strbody = strbody.Replace("[[AgencyName]]", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("Name").Value)
                            strbody = strbody.Replace("[[Address]]", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("ADDRESS").Value)
                            strbody = strbody.Replace("[[TelNo]]", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("PHONE").Value)
                            strbody = strbody.Replace("[[ContactPerson]]", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("CONCERNED_PERSON").Value)
                            strbody = strbody.Replace("[[FAX]]", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("FAX").Value)
                            'strbody = strbody.Replace("[[OFFICEID]]", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("OFFICEID").Value)
                            strbody = strbody.Replace("[[Address1]]", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("ADDRESS1").Value)
                            strbody = strbody.Replace("[[City]]", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("CityName").Value)

                            strbody = strbody.Replace("[[PIN]]", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("PINCODE").Value)

                            strbody = strbody.Replace("[[Remarks]]", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("Remarks").Value)
                            If Session("Security") IsNot Nothing Then
                                Dim objSecurityXml As New XmlDocument
                                objSecurityXml.LoadXml(Session("Security"))
                                strbody = strbody.Replace("[[AuthorizedSign]]", " " & objSecurityXml.DocumentElement.SelectSingleNode("Login").InnerText)
                            End If


                            '@ New of Previous Code For Change Template For Replacing Tag



                            txtSub.Text = "Agency : " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("Name").Value & " ; Request Id : " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("RequestID").Value
                        End If
                        txtbody.Text = strbody

                        DivBody.InnerHtml = strbody
                        hdnmsg.Value = strbody

                        '  ClientScript.RegisterClientScriptBlock(Me.GetType, "strPopup", "<script language='javascript'>window.setTimeout('TextContent();', 5000);</script>")
                    End If
                End If

                ' #######################################################################
                If Not Session("LoginSession") Is Nothing Then
                    objInputXml.LoadXml("<MS_VIEWEMPLOYEE_INPUT><EmployeeID></EmployeeID></MS_VIEWEMPLOYEE_INPUT>")
                    objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerXml = Session("LoginSession").ToString().Split("|")(0)
                    'Here Back end Method Call
                    objOutputXml = objbzEmployee.View(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        hdFrom.Value = objOutputXml.DocumentElement.SelectSingleNode("Employee").Attributes("Email").Value()
                    End If
                End If
                If Session("EmailList") IsNot Nothing Then
                    txtEmailTo.Text = Session("EmailList").ToString

                    objInputTempXml.LoadXml("<MS_VIEWEMAILTEMPLATES_INPUT><TEMPLATES MailTemplateName='' /></MS_VIEWEMAILTEMPLATES_INPUT>")
                    objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("MailTemplateName").Value() = "ORDER_MAIL" ' Session("Action").ToString().Split("|").GetValue(1).ToString()
                    'Here Back end Method Call
                    objOutputTempXml = objMailTemplate.View(objInputTempXml)
                    If objOutputTempXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        txtbody.Text = objOutputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("MailTemplate").Value
                        '###############################################################################
                        '@ Now find the value of Body parts on the basis of Lcode

                        strbody = txtbody.Text
                        If (Request.QueryString("Lcode") IsNot Nothing) Then
                            Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
                            Dim objInputAgencyXml, objOutputAgencyXml As New XmlDocument
                            objInputAgencyXml.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")
                            objInputAgencyXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerXml = Request.QueryString("Lcode").ToString() ' Session("Action").ToString().Split("|").GetValue(1)
                            'Here Back end Method Call
                            objOutputAgencyXml = objbzAgency.View(objInputAgencyXml)

                            If objOutputAgencyXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then


                                ' ##############################################################################
                                ' @ Previous Code For Chanhing Tamplate and Replace Tag in body parts

                                'strbody = strbody.Replace("<<TXTADDRESS>>", " " & objOutputAgencyXml.DocumentElement.SelectSingleNode("Agency").Attributes("ADDRESS").Value)
                                'strbody = strbody.Replace("<<PHONE>>", " " & objOutputAgencyXml.DocumentElement.SelectSingleNode("Agency").Attributes("PHONE").Value)
                                'strbody = strbody.Replace("<<CONTACTPERSON>>", " " & objOutputAgencyXml.DocumentElement.SelectSingleNode("Agency").Attributes("CONTACT_PERSON_NAME").Value)
                                'strbody = strbody.Replace("<<FAX>>", " " & objOutputAgencyXml.DocumentElement.SelectSingleNode("Agency").Attributes("FAX").Value)

                                'strbody = strbody.Replace("<<OFFICEID>>", " " & objOutputAgencyXml.DocumentElement.SelectSingleNode("Agency").Attributes("Aoffice").Value)
                                'strbody = strbody.Replace("<<P_ONLINESTATUS>>", " " & objOutputAgencyXml.DocumentElement.SelectSingleNode("Agency").Attributes("ONLINE_STATUS").Value)
                                'strbody = strbody.Replace("<<B_ONLINESTATUS>>", " " & objOutputAgencyXml.DocumentElement.SelectSingleNode("Agency").Attributes("ONLINE_STATUS_BACKUP").Value)

                                'If (Request.QueryString("OrderId") IsNot Nothing) Then
                                '    Dim objbzOrder As New AAMS.bizTravelAgency.bzOrder
                                '    Dim objInputOrderXml, objOutputOrderXml As New XmlDocument
                                '    objInputOrderXml.LoadXml("<MS_VIEWORDERS_INPUT><ORDERID></ORDERID></MS_VIEWORDERS_INPUT>")
                                '    objInputOrderXml.DocumentElement.SelectSingleNode("ORDERID").InnerText = Request.QueryString("OrderId").ToString()
                                '    'Here Back end Method Call
                                '    objOutputOrderXml = objbzOrder.View(objInputOrderXml)
                                '    '            <MS_VIEWORDERS_OUTPUT>
                                '    '	<ORDERS ORDERID='' ORDERTYPEID='' LCODE='' NAME='' ADDRESS='' ADDRESS1='' ORDER_NUMBER=''
                                '    '		ORDERSTATUSID='' PROCESSEDBYID='' PROCESSEDBYNAME='' PLANID='' ISPNAME='' ISPID='' RESEND_DATE_MKT=''
                                '    '		RECEIVED_DATE_MKT='' INSTALLATION_DUE_DATE='' EXPECTED_INSTALLATION_DATE='' APR='' OPC=''
                                '    '		APC='' NewOrder='' OFFICEID1='' OFFICEID='' RECEIVING_OFFICEID='' MSG_SEND_DATE='' APPLIED_DATE=''
                                '    '		SENDBACK_DATE='' APPROVAL_DATE='' RECEIVED_DATE='' PROCESSED_DATE='' REMARKS='' PENDINGWITHID=""
                                '    '		PENDINGWITHNAME="" />
                                '    '         <Errors Status=''>
                                '    '		<Error Code='' Description='' />
                                '    '	</Errors>
                                '    '</MS_VIEWORDERS_OUTPUT>

                                '    If objOutputOrderXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                                '        With objOutputOrderXml.DocumentElement

                                '            strbody = strbody.Replace("<<ORDERTYPE>>", " " & .SelectSingleNode("ORDERS").Attributes("ORDERTYPE").InnerText)


                                '            strbody = strbody.Replace("<<PLANID>>", " " & .SelectSingleNode("ORDERS").Attributes("PLANID").Value)
                                '            strbody = strbody.Replace("<<ISPName>>", " " & .SelectSingleNode("ORDERS").Attributes("ISPNAME").Value)
                                '            strbody = strbody.Replace("<<TXTREMARKS>>", " " & .SelectSingleNode("ORDERS").Attributes("REMARKS").Value)
                                '            txtSub.Text = "Agency : " & objOutputAgencyXml.DocumentElement.SelectSingleNode("Agency").Attributes("NAME").Value & " ; Order Number : " & .SelectSingleNode("ORDERS").Attributes("ORDER_NUMBER").Value
                                '        End With
                                '    End If
                                '    '  ()
                                'End If
                                'If Session("Security") IsNot Nothing Then
                                '    Dim objSecurityXml As New XmlDocument
                                '    objSecurityXml.LoadXml(Session("Security"))
                                '    strbody = strbody.Replace("<<USERNAME>>", " " & objSecurityXml.DocumentElement.SelectSingleNode("Login").InnerText)
                                'End If
                                '' strbody = strbody.Replace("<<USERNAME>>", " " & "Admin")
                                'txtbody.Text = strbody

                                '@ End of Code For Chanhing Tamplate and Replace Tag in body parts
                                '#########################################################################################

                                '@ New Code For Changing  Template and Replace Tag in body parts

                                strbody = strbody.Replace("[[TXTADDRESS]]", " " & objOutputAgencyXml.DocumentElement.SelectSingleNode("Agency").Attributes("ADDRESS").Value)


                                strbody = strbody.Replace("[[TXTADDRESS1]]", " " & objOutputAgencyXml.DocumentElement.SelectSingleNode("Agency").Attributes("ADDRESS1").Value)

                                strbody = strbody.Replace("[[PIN]]", " " & objOutputAgencyXml.DocumentElement.SelectSingleNode("Agency").Attributes("PINCODE").Value)



                                strbody = strbody.Replace("[[PHONE]]", " " & objOutputAgencyXml.DocumentElement.SelectSingleNode("Agency").Attributes("PHONE").Value)
                                strbody = strbody.Replace("[[ContactPerson]]", " " & objOutputAgencyXml.DocumentElement.SelectSingleNode("Agency").Attributes("CONTACT_PERSON_NAME").Value)
                                strbody = strbody.Replace("[[FAX]]", " " & objOutputAgencyXml.DocumentElement.SelectSingleNode("Agency").Attributes("FAX").Value)
                                strbody = strbody.Replace("[[CITY]]", " " & objOutputAgencyXml.DocumentElement.SelectSingleNode("Agency").Attributes("CITY").Value & "")

                                ' strbody = strbody.Replace("[[OFFICEID]]", " " & objOutputAgencyXml.DocumentElement.SelectSingleNode("Agency").Attributes("OFFICEID").Value)
                                strbody = strbody.Replace("[[P_ONLINESTATUS]]", " " & objOutputAgencyXml.DocumentElement.SelectSingleNode("Agency").Attributes("ONLINE_STATUS").Value)
                                strbody = strbody.Replace("[[B_ONLINESTATUS]]", " " & objOutputAgencyXml.DocumentElement.SelectSingleNode("Agency").Attributes("ONLINE_STATUS_BACKUP").Value)


                                '##############################################################################
                                '@ In Case of Mail Send From Order Module
                                '##############################################################################

                                If (Request.QueryString("OrderId") IsNot Nothing) Then
                                    Dim objbzOrder As New AAMS.bizTravelAgency.bzOrder
                                    Dim objInputOrderXml, objOutputOrderXml As New XmlDocument
                                    objInputOrderXml.LoadXml("<MS_VIEWORDERS_INPUT><ORDERID></ORDERID></MS_VIEWORDERS_INPUT>")
                                    objInputOrderXml.DocumentElement.SelectSingleNode("ORDERID").InnerText = Request.QueryString("OrderId").ToString()
                                    'Here Back end Method Call
                                    objOutputOrderXml = objbzOrder.View(objInputOrderXml)
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

                                    If objOutputOrderXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                                        '' @ Code for GroupISP on the basis of Order Type
                                        'Dim blnGroupISP As Boolean
                                        'Dim objbzOrderType As New AAMS.bizTravelAgency.bzOrderType
                                        'Dim objOrderSendmail As New AAMS.bizMaster.bzEmailGroup
                                        'Dim objAgencyMNC As New AAMS.bizTravelAgency.bzAgency
                                        'Dim OrderType As String
                                        'OrderType = objOutputOrderXml.DocumentElement.SelectSingleNode("ORDERS").Attributes("ORDERTYPEID").Value

                                        'objInputXml.LoadXml("<MS_VIEWORDERTYPE_INPUT><ORDERTYPEID></ORDERTYPEID></MS_VIEWORDERTYPE_INPUT>")
                                        'objInputXml.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText = OrderType ' ddlOrderType.SelectedValue
                                        ''Here Back end Method Call
                                        'objOutputXml = objbzOrderType.View(objInputXml)
                                        'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                                        '    blnGroupISP = objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsISPOrder").Value
                                        'End If

                                        ''For Isp Order Type Get Template For Isp Order
                                        'If blnGroupISP = "True" Then

                                        'End If
                                        '' @End of  Code for GroupISP on the basis of Order Type

                                        With objOutputOrderXml.DocumentElement

                                            strbody = strbody.Replace("[[OFFICEID]]", " " & .SelectSingleNode("ORDERS").Attributes("OFFICEID").InnerText) '
                                            strbody = strbody.Replace("[[ORDERTYPE]]", " " & .SelectSingleNode("ORDERS").Attributes("ORDERTYPE").InnerText)


                                            'strbody = strbody.Replace("[[PLANID]]", " " & .SelectSingleNode("ORDERS").Attributes("PLANID").Value)
                                            'strbody = strbody.Replace("[[IPLANID]]", " " & .SelectSingleNode("ORDERS").Attributes("PLANID").Value)
                                            strbody = strbody.Replace("[[IPLANID]]", " " & .SelectSingleNode("ORDERS").Attributes("NPID").Value)
                                            strbody = strbody.Replace("[[ISPName]]", " " & .SelectSingleNode("ORDERS").Attributes("ISPNAME").Value)
                                            strbody = strbody.Replace("[[TXTREMARKS]]", " " & .SelectSingleNode("ORDERS").Attributes("REMARKS").Value)
                                            txtSub.Text = "Agency : " & objOutputAgencyXml.DocumentElement.SelectSingleNode("Agency").Attributes("NAME").Value & " ; Order Number : " & .SelectSingleNode("ORDERS").Attributes("ORDER_NUMBER").Value

                                            '@ Added By Abhishek 04 July 2010
                                            '@ Code For replacing the value for chain code and chain name
                                            If objOutputOrderXml.DocumentElement.SelectSingleNode("ORDERS").Attributes("NEW_ORD_MSG").Value.Trim.ToUpper = "TRUE" Then
                                                If objOutputOrderXml.DocumentElement.SelectSingleNode("ORDERS").Attributes("LCOUNT").Value.Trim = "1" Then
                                                    strbody = strbody.Replace("[[NEWORDMSG]]", "This location has been assigned a new chain code : " & objOutputOrderXml.DocumentElement.SelectSingleNode("ORDERS").Attributes("CHAIN_CODE").Value & " and chain name : " & objOutputOrderXml.DocumentElement.SelectSingleNode("ORDERS").Attributes("CHAIN_NAME").Value & ". Please notify if wrongly associated.")
                                                Else
                                                    strbody = strbody.Replace("[[NEWORDMSG]]", "This location has been added to existing chain code : " & objOutputOrderXml.DocumentElement.SelectSingleNode("ORDERS").Attributes("CHAIN_CODE").Value & " and chain name : " & objOutputOrderXml.DocumentElement.SelectSingleNode("ORDERS").Attributes("CHAIN_NAME").Value & ". Please notify if wrongly associated.")
                                                End If

                                            Else
                                                strbody = strbody.Replace("[[NEWORDMSG]]", " " & "")
                                            End If
                                            '@ Added By Abhishek 04 July 2010



                                        End With
                                    End If
                                    '  ()
                                End If
                                '##############################################################################
                                '@ end of  In Case of Mail Send From Order Module /A
                                '##############################################################################
                                '##############################################################################
                                '@ In Case of Mail Send From ISP Order Module
                                '##############################################################################
                                If (Request.QueryString("ISPOrderId") IsNot Nothing) Then


                                    Dim objbzOrder As New AAMS.bizTravelAgency.bzOrder
                                    Dim objbzIspOrder As New AAMS.bizISP.bzISPOrder
                                    Dim objInputIspOrderXml, objOutputIspOrderXml As New XmlDocument
                                    objInputIspOrderXml.LoadXml("<ISP_VIEWORDER_INPUT><ISPOrderID/></ISP_VIEWORDER_INPUT>")
                                    objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPOrderID").InnerText = Request.QueryString("ISPOrderId") 'strIspOrderId
                                    ' objOutputIspOrderXml.LoadXml("<ISP_VIEWISP_OUTPUT><ISPORDER ISPOrderID='' CreationDate='' LCODE='' Agencyname='' Address='' City='' Country='' Phone='' Fax='' OfficeId='' ISPName='' ISPCityName='' ISPPlanID='' BandWidth='' InstallationCharges='' MonthlyCharges='' EquipmentInclude='' EquipmentOneTimeCharges='' EquipmentMonthlyCharges='' DeliveryTimeLine='' OrderNumber='' OrderDate='' OnlineDate='' ExpectedDate='' ISPOrderStatusId='' LoggedBy='' ApprovedBY='' ApprovalDate='' CAFNumber='' CancellationDate='' MDNNumber='' LoginName='' CancellationReason='' Remarks=''/><Errors Status='FALSE'><Error Code='' Description=''/></Errors></ISP_VIEWISP_OUTPUT>")
                                    objOutputIspOrderXml = objbzIspOrder.View(objInputIspOrderXml)
                                    'Here Back end Method Call
                                    '  <ISP_VIEWORDER_OUTPUT>
                                    '  <ISPORDER ISPOrderID="99"
                                    ' LCODE="21555" Agencyname="(Aurion Pro Solutions)" 
                                    'Address="Garden 404,Winchester, High Street, Hiranandani" City="Mumbai" 
                                    'Country="India" 
                                    'Phone = "67707700"
                                    'Fax = "67707722"
                                    'OfficeId = "BOMMY3100"
                                    'ISPName="airtel Ltd" 
                                    'ISPCityName = "Mumbai"
                                    ' ISPPlanID="39"
                                    'NPID = "INIS-0211"
                                    'BandWidth="512K"
                                    ' InstallationCharges="0.00" MonthlyCharges="3995.00" 
                                    'EquipmentInclude="True"
                                    ' EquipmentOneTimeCharges="0.00" EquipmentMonthlyCharges="0.00" 
                                    'DeliveryTimeLine="7"
                                    'OrderNumber = "2008/8/59"
                                    'OrderDate="20080828" OnlineDate="20080829"
                                    ' ExpectedDate="20080829" ISPOrderStatusId="2" 
                                    'LoggedBy="202787" ApprovedBY="" ApprovedByName=""
                                    'ApprovalDate="" CAFNumber="" 
                                    'CancellationDate="20080919" CreationDate="" 
                                    'MDNNumber="" LoginName="admin" 
                                    'CancellationReason="krishna" Remarks="" ISPID="82" 
                                    'OriginalOrderNo="" StaticIP="" /> 
                                    '- <Errors Status="FALSE">
                                    '  <Error Code="" Description="" /> 
                                    '  </Errors>
                                    '  </ISP_VIEWORDER_OUTPUT>

                                    If objOutputIspOrderXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                                        With objOutputIspOrderXml.DocumentElement

                                            strbody = strbody.Replace("[[OFFICEID]]", " " & .SelectSingleNode("ISPORDER").Attributes("OfficeId").InnerText) '
                                            'strbody = strbody.Replace("[[ORDERTYPE]]", " " & .SelectSingleNode("ISPORDER").Attributes("ORDERTYPE").InnerText)
                                            strbody = strbody.Replace("[[ORDERTYPE]]", " " & "")


                                            'strbody = strbody.Replace("[[PLANID]]", " " & .SelectSingleNode("ISPORDER").Attributes("PLANID").Value)
                                            'strbody = strbody.Replace("[[IPLANID]]", " " & .SelectSingleNode("ISPORDER").Attributes("PLANID").Value)
                                            strbody = strbody.Replace("[[IPLANID]]", " " & .SelectSingleNode("ISPORDER").Attributes("NPID").Value)
                                            strbody = strbody.Replace("[[ISPName]]", " " & .SelectSingleNode("ISPORDER").Attributes("ISPName").Value)
                                            strbody = strbody.Replace("[[TXTREMARKS]]", " " & .SelectSingleNode("ISPORDER").Attributes("Remarks").Value)
                                            txtSub.Text = "Agency : " & objOutputAgencyXml.DocumentElement.SelectSingleNode("Agency").Attributes("NAME").Value & " ; Order Number : " & .SelectSingleNode("ISPORDER").Attributes("OrderNumber").Value

                                            '@ Added By Abhishek 04 July 2010
                                            '@ Code For replacing the value for chain code and chain name
                                            strbody = strbody.Replace("[[NEWORDMSG]]", " " & "")
                                            '@ Added By Abhishek 04 July 2010
                                        End With
                                    End If

                                End If
                                '##############################################################################
                                '@ In Case of Mail Send From ISP Order Module
                                '##############################################################################


                                If Session("Security") IsNot Nothing Then
                                    Dim objSecurityXml As New XmlDocument
                                    objSecurityXml.LoadXml(Session("Security"))
                                    strbody = strbody.Replace("[[USERNAME]]", " " & objSecurityXml.DocumentElement.SelectSingleNode("Login").InnerText)
                                End If
                                ' strbody = strbody.Replace("[[USERNAME]]", " " & "Admin")
                                txtbody.Text = strbody
                                DivBody.InnerHtml = strbody
                                hdnmsg.Value = strbody

                                '@ End of New Code For Changing  Template and Replace Tag in body parts
                                '#########################################################################################################
                            End If
                        End If
                        '###############################################################################

                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            ' ClientScript.RegisterClientScriptBlock(Me.GetType, "strPopup", "<script language='javascript'>window.setTimeout('TextContent();', 5000);</script>")
        End Try

    End Sub

    Protected Sub btnSendMail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSendMail.Click
        Try
            Dim i As Integer
            Dim StrAttachmentFileName As String = ""
            If (Request.QueryString("RequestId") IsNot Nothing) Then
                'If FileAttach.HasFile = False Then
                '    lblError.Text = "Please upload the file."
                '    Exit Sub
                'End If
                'If FileAttach.PostedFile.FileName <> "" Then
                '    If FileAttach.PostedFile.ContentLength > 5242880 Then  ' 1024 bytes= 1KB
                '        lblError.Text = "The image selected exceeds the file size limit of 5MB."
                '        Exit Sub
                '    End If
                'End If
            End If

            '#####################################################################
            ' @ New Code is added to send mail for ISP Feasibility Request Mail
            Dim objbzISPFeasibleRequest As New AAMS.bizISP.bzISPFeasibleRequest
            Dim objISPMailInput, objISPMailOutput As New XmlDocument
            If (Request.QueryString("RequestId") IsNot Nothing) Then
                ' objISPMailInput.LoadXml("<ISP_UPDATESENDEMAILDATE_INPUT><FEASIBILE_REQUEST RequestID='' ISPID='' ContentType='' EmailFrom='' EmailTo='' EmailSubject ='' EmailBody='' /></ISP_UPDATESENDEMAILDATE_INPUT>")

                objISPMailInput.LoadXml("<ISP_UPDATESENDEMAILDATE_INPUT><FEASIBILE_REQUEST RequestID='' ISPID='' ContentType='' EmailFrom='' EmailTo='' EmailSubject ='' EmailBody='' EmailCC='' EmailBCC='' ATTACHMENT_FILE='' /></ISP_UPDATESENDEMAILDATE_INPUT>")
                objISPMailInput.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("ISPID").InnerText = Request.QueryString("ISPID").ToString()
                objISPMailInput.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("RequestID").InnerText = Request.QueryString("RequestId").ToString()
                objISPMailInput.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("ContentType").Value = "2"
                objISPMailInput.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("EmailFrom").Value = hdFrom.Value '(txtEmailFrom.Text)
                objISPMailInput.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("EmailTo").Value = Trim(txtEmailTo.Text) + "," + hdFrom.Value
                objISPMailInput.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("EmailSubject").Value = Trim(txtSub.Text)
                'objISPMailInput.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("EmailBody").Value = Trim(txtbody.Text)
                objISPMailInput.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("EmailBody").Value = hdnmsg.Value ' DivBody.InnerHtml
                objISPMailInput.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("EmailCC").Value = Trim(txtCC.Text)
                objISPMailInput.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("EmailBCC").Value = Trim(txtBCC.Text)


                For i = 0 To lstAttachment.Items.Count - 1

                    'If i = 0 Then
                    '    StrAttachmentFileName = lstAttachment.Items(i).Text
                    'Else
                    '    StrAttachmentFileName = StrAttachmentFileName + "," + lstAttachment.Items(i).Text
                    'End If

                    If i = 0 Then
                        StrAttachmentFileName = Server.MapPath("~") + "\MailAttachment\" + lstAttachment.Items(i).Text 'Request.MapPath("~") + "\MailAttachment\" + lstAttachment.Items(i).Text
                    Else
                        StrAttachmentFileName = StrAttachmentFileName + "," + Server.MapPath("~") + "\MailAttachment\" + lstAttachment.Items(i).Text 'Request.MapPath("~") + "\MailAttachment\" + lstAttachment.Items(i).Text
                    End If
                Next
                objISPMailInput.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("ATTACHMENT_FILE").Value = StrAttachmentFileName


                'Here Back end Method Call
                objISPMailOutput = objbzISPFeasibleRequest.SendMail(objISPMailInput)
                If objISPMailOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    lblError.Text = "Mail sent successfully."
                    Session("ISPMailDate") = Request.QueryString("RequestId") + "|" + Request.QueryString("ISPID") + "|" + Now.Date.ToString("yyyyMMdd")
                    Exit Sub
                Else
                    lblError.Text = objISPMailOutput.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    Exit Sub
                End If
            Else
                'lblError.Text = "Not any RequestId Is Exist."
            End If
            '#####################################################################
            If (Request.QueryString("OrderId") IsNot Nothing) Then

                Dim objbzOrder As New AAMS.bizTravelAgency.bzOrder
                Dim objbzOrderMail As New AAMS.bizTravelAgency.bzAgency
                Dim objInputOrderMailXml, objOutputOrderMailXml As New XmlDocument
                Dim objInputOrderXml, objOutputOrderXml As New XmlDocument
                objInputOrderXml.LoadXml("<MS_VIEWORDERS_INPUT><ORDERID></ORDERID></MS_VIEWORDERS_INPUT>")
                objInputOrderXml.DocumentElement.SelectSingleNode("ORDERID").InnerText = Request.QueryString("OrderId").ToString()
                'Here Back end Method Call
                objOutputOrderXml = objbzOrder.View(objInputOrderXml)
                If objOutputOrderXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    With objOutputOrderXml.DocumentElement

                        'Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency


                        objInputOrderMailXml.LoadXml("<TA_SENDMAILAGENCY_INPUT><AGENCYFILE ID ='' FILENO=''  ORDERNO ='' DOCTYPE ='' SEQUENCE='' ORDERTYPE='' DOCUMENT='' ContentType='' EmailFrom='' EmailTo=''  EmailSubject=''  EmailBody='' PDFDocFileName='' LCode ='' ORDERID=''  ISISPORDER='False' /></TA_SENDMAILAGENCY_INPUT>")
                        objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").Value = .SelectSingleNode("ORDERS").Attributes("FILENO").InnerText
                        objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERNO").Value = .SelectSingleNode("ORDERS").Attributes("ORDER_NUMBER").InnerText
                        objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("DOCTYPE").Value = "2"
                        objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("SEQUENCE").Value = ""
                        objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERTYPE").Value = .SelectSingleNode("ORDERS").Attributes("ORDERTYPE").InnerText
                        objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("DOCUMENT").Value = ""
                        objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ContentType").Value = "2"
                        objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailFrom").Value = hdFrom.Value 'txtEmailFrom.Text
                        objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailTo").Value = txtEmailTo.Text + "," + hdFrom.Value
                        'objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailBody").Value = txtbody.Text '  DivBody.InnerHtml "<Html><Table><tr><td>" + txtbody.Text + "</td></tr></Table></Html>"
                        objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailBody").Value = hdnmsg.Value 'DivBody.InnerHtml '  "<Html><Table><tr><td>" + txtbody.Text + "</td></tr></Table></Html>"
                        objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("PDFDocFileName").Value = "" 'txtbody.Text 
                        objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailSubject").Value = txtSub.Text

                        If (Request.QueryString("Lcode") IsNot Nothing) Then
                            objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("LCode").Value = Request.QueryString("Lcode").ToString()
                        End If
                        objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERID").Value = Request.QueryString("OrderId").ToString()


                    End With
                End If

                'Here Back end Method Call
                'objOutputOrderMailXml = objbzOrderMail.SendMail(objInputOrderMailXml)
                objOutputOrderMailXml = objbzOrderMail.SendMailWeb(objInputOrderMailXml)
                If objOutputOrderMailXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    lblError.Text = "Mail sent successfully."
                    If hdnmsg.Value <> "" Then
                        DivBody.InnerHtml = hdnmsg.Value
                    End If

                Else
                    lblError.Text = objOutputOrderMailXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            Else
                ' lblError.Text = "Not any Order Is Exist."
            End If


            '#####################################################################

            '@ In Case of Mail Send From ISP Order Module
            If (Request.QueryString("ISPOrderId") IsNot Nothing) Then
                Dim objbzOrderMail As New AAMS.bizTravelAgency.bzAgency
                Dim objInputOrderMailXml, objOutputOrderMailXml As New XmlDocument
                Dim objInputIspOrderXml, objOutputIspOrderXml As New XmlDocument
                Dim objbzIspOrder As New AAMS.bizISP.bzISPOrder
                objInputIspOrderXml.LoadXml("<ISP_VIEWORDER_INPUT><ISPOrderID/></ISP_VIEWORDER_INPUT>")
                objInputIspOrderXml.DocumentElement.SelectSingleNode("ISPOrderID").InnerText = Request.QueryString("ISPOrderId") 'strIspOrderId
                ' objOutputIspOrderXml.LoadXml("<ISP_VIEWISP_OUTPUT><ISPORDER ISPOrderID='' CreationDate='' LCODE='' Agencyname='' Address='' City='' Country='' Phone='' Fax='' OfficeId='' ISPName='' ISPCityName='' ISPPlanID='' BandWidth='' InstallationCharges='' MonthlyCharges='' EquipmentInclude='' EquipmentOneTimeCharges='' EquipmentMonthlyCharges='' DeliveryTimeLine='' OrderNumber='' OrderDate='' OnlineDate='' ExpectedDate='' ISPOrderStatusId='' LoggedBy='' ApprovedBY='' ApprovalDate='' CAFNumber='' CancellationDate='' MDNNumber='' LoginName='' CancellationReason='' Remarks=''/><Errors Status='FALSE'><Error Code='' Description=''/></Errors></ISP_VIEWISP_OUTPUT>")
                objOutputIspOrderXml = objbzIspOrder.View(objInputIspOrderXml)


                'Here Back end Method Call

                'Here Back end Method Call
                '  <ISP_VIEWORDER_OUTPUT>
                '  <ISPORDER ISPOrderID="99"
                ' LCODE="21555" Agencyname="(Aurion Pro Solutions)" 
                'Address="Garden 404,Winchester, High Street, Hiranandani" City="Mumbai" 
                'Country="India" 
                'Phone = "67707700"
                'Fax = "67707722"
                'OfficeId = "BOMMY3100"
                'ISPName="airtel Ltd" 
                'ISPCityName = "Mumbai"
                ' ISPPlanID="39"
                'NPID = "INIS-0211"
                'BandWidth="512K"
                ' InstallationCharges="0.00" MonthlyCharges="3995.00" 
                'EquipmentInclude="True"
                ' EquipmentOneTimeCharges="0.00" EquipmentMonthlyCharges="0.00" 
                'DeliveryTimeLine="7"
                'OrderNumber = "2008/8/59"
                'OrderDate="20080828" OnlineDate="20080829"
                ' ExpectedDate="20080829" ISPOrderStatusId="2" 
                'LoggedBy="202787" ApprovedBY="" ApprovedByName=""
                'ApprovalDate="" CAFNumber="" 
                'CancellationDate="20080919" CreationDate="" 
                'MDNNumber="" LoginName="admin" 
                'CancellationReason="krishna" Remarks="" ISPID="82" 
                'OriginalOrderNo="" StaticIP="" /> 
                '- <Errors Status="FALSE">
                '  <Error Code="" Description="" /> 
                '  </Errors>
                '  </ISP_VIEWORDER_OUTPUT>


                If objOutputIspOrderXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    With objOutputIspOrderXml.DocumentElement

                        'Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency


                        objInputOrderMailXml.LoadXml("<TA_SENDMAILAGENCY_INPUT><AGENCYFILE ID ='' FILENO=''  ORDERNO ='' DOCTYPE ='' SEQUENCE='' ORDERTYPE='' DOCUMENT='' ContentType='' EmailFrom='' EmailTo=''  EmailSubject=''  EmailBody='' PDFDocFileName='' LCode ='' ORDERID=''  ISISPORDER='True' /></TA_SENDMAILAGENCY_INPUT>")
                        'objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").Value = .SelectSingleNode("ISPORDER").Attributes("FILENO").InnerText
                        objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERNO").Value = .SelectSingleNode("ISPORDER").Attributes("OrderNumber").InnerText
                        objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("DOCTYPE").Value = "2"
                        objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("SEQUENCE").Value = ""
                        ' objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERTYPE").Value = .SelectSingleNode("ISPORDER").Attributes("ORDERTYPE").InnerText
                        objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("DOCUMENT").Value = ""
                        objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ContentType").Value = "2"
                        objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailFrom").Value = hdFrom.Value 'txtEmailFrom.Text
                        objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailTo").Value = txtEmailTo.Text + "," + hdFrom.Value
                        'objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailBody").Value = txtbody.Text '  DivBody.InnerHtml "<Html><Table><tr><td>" + txtbody.Text + "</td></tr></Table></Html>"
                        objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailBody").Value = hdnmsg.Value 'DivBody.InnerHtml '  "<Html><Table><tr><td>" + txtbody.Text + "</td></tr></Table></Html>"
                        objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("PDFDocFileName").Value = "" 'txtbody.Text 
                        objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailSubject").Value = txtSub.Text

                        If (Request.QueryString("Lcode") IsNot Nothing) Then
                            objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("LCode").Value = Request.QueryString("Lcode").ToString()
                        End If

                        objInputOrderMailXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERID").Value = Request.QueryString("ISPOrderId") 'strIspOrderId

                    End With
                End If

                'Here Back end Method Call
                ' objOutputOrderMailXml = objbzOrderMail.SendMail(objInputOrderMailXml)
                objOutputOrderMailXml = objbzOrderMail.SendMailWeb(objInputOrderMailXml)
                If objOutputOrderMailXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    lblError.Text = "Mail sent successfully."
                    If hdnmsg.Value <> "" Then
                        DivBody.InnerHtml = hdnmsg.Value
                    End If

                Else
                    lblError.Text = objOutputOrderMailXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            Else
                ' lblError.Text = "Not any Order Is Exist."
            End If

            'Dim i As Integer
            'Dim objSendmail As New MailMessage
            'Dim smtp As New SmtpClient("220.227.154.35")
            'objSendmail.From = New MailAddress(txtEmailFrom.Text)
            'objSendmail.Body = txtbody.Text '"<html><table><tr><td>" + txtbody.Text + "</td></tr></table></html>"
            'objSendmail.Subject = txtSub.Text
            'For i = 0 To txtEmailTo.Text.Split(";").Length - 1
            '    objSendmail.To.Add(New MailAddress(txtEmailTo.Text.Split(";")(i)))
            'Next
            'smtp.Send(objSendmail)
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            If hdnmsg.Value <> "" Then
                DivBody.InnerHtml = hdnmsg.Value
            End If
        End Try
    End Sub
    Protected Sub btnUpLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpLoad.Click
        Try
            'If FileAttach.HasFile = False Then
            '    lblError.Text = "There is no file For Upload."
            '    Exit Sub
            'End If
            If FileAttach.PostedFile.FileName.Trim = "" Then
                lblError.Text = "There is no file for uploading."
                Exit Sub
            End If
            If FileAttach.PostedFile.FileName <> "" Then
                If FileAttach.PostedFile.ContentLength > 5242880 Then  ' 1024 bytes= 1KB
                    lblError.Text = "The selected  file size is greater than 5MB."
                    Exit Sub
                End If
            End If
            Dim NewFileName As String = ""
            Dim OriginalFileName As String
            OriginalFileName = FileAttach.PostedFile.FileName.ToString()
            Dim LastSlashPosition As Integer
            Dim NewFilePath As String
            LastSlashPosition = OriginalFileName.LastIndexOf("\") + 1
            Dim FileNamePart As String = OriginalFileName.Substring(LastSlashPosition, OriginalFileName.Length - LastSlashPosition)

            If lstAttachment.Items.FindByText(FileNamePart) IsNot Nothing Then
                lblError.Text = "File is already uploaded."
                Exit Sub
            End If

            NewFilePath = Server.MapPath("~") + "\MailAttachment\" + FileNamePart 'Request.MapPath("~") + "\MailAttachment\" + FileNamePart

            If System.IO.File.Exists(NewFilePath) Then
                System.IO.File.Delete(NewFilePath)
            End If
            FileAttach.PostedFile.SaveAs(NewFilePath)
            lstAttachment.Items.Add(FileNamePart)
            If hdnmsg.Value <> "" Then
                DivBody.InnerHtml = hdnmsg.Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            If hdnmsg.Value <> "" Then
                DivBody.InnerHtml = hdnmsg.Value
            End If
        End Try



    End Sub
    Protected Sub btnRemove_Click_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Dim strEmailList As String = ""
        Dim NewFilePath As String = "'"
        Try
            Dim listitem As ListItem
            Dim i As Integer
            Dim count As Integer = lstAttachment.Items.Count
            For i = 0 To count - 1
                For Each listitem In lstAttachment.Items
                    If listitem.Selected Then
                        NewFilePath = Server.MapPath("~") + "\MailAttachment\" + listitem.Text 'Request.MapPath("~") + "\MailAttachment\" + listitem.Text
                        If System.IO.File.Exists(NewFilePath) Then
                            System.IO.File.Delete(NewFilePath)
                        End If
                        lstAttachment.Items.Remove(listitem.Text)
                        Exit For
                    End If
                Next
            Next i
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            If hdnmsg.Value <> "" Then
                DivBody.InnerHtml = hdnmsg.Value
            End If
        End Try
    End Sub


End Class
