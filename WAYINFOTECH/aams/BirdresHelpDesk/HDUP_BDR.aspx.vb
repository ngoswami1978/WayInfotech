'#####################################################################
'######   Page Name -- HelpDesk_HDUP_BDR                     #########  
'######   Date 27-February 2007  #####################################    
'############   Developed By Abhishek  ###############################
'#####################################################################
#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data
#End Region
Partial Class BirdresHelpDesk_HDUP_BDR
    Inherits System.Web.UI.Page

#Region "Page Level Variable/Objects Declaration"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
#End Region
#Region "Page_Load Event"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            btnSave.Attributes.Add("onclick", "return BDRMandatory();")
            ' btnNew.Attributes.Add("onclick", "return NewHDUPBDR();")
            '
            'Rakesh start code 
            If Request.QueryString("Popup") Is Nothing Then
                lnkClose.Visible = False
            Else
                lnkClose.Visible = True
                btnLTR.Visible = False

            End If
            'End code



            ' #######################################
            ' ########## This Code code is used for enable/disable 


            ' ########## the button according to rights For BDR letter Print
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR BDR Letter Report']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR BDR Letter Report']").Attributes("Value").Value)
                    If strBuilder(4) = "0" Then
                        btnPrint.Enabled = False
                        ' btnSave.Enabled = False
                    End If

                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If


            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR BDRLetter']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR BDRLetter']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Log HD Call']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Log HD Call']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Or strBuilder(2) = "0" Then
                        btnLTR.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR BDR History']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR BDR History']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnHistory.Disabled = True
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            '#######################
            If strBuilder(0) = "1" Then
                If (Request.QueryString("Action") = "U") Then
                    If (Request.QueryString("HD_RE_BDR_ID") IsNot Nothing) Then
                        hdEnbdrLetterId.Value = Request.QueryString("HD_RE_BDR_ID").ToString 'Rakesh 2 Aug 08 Added
                        hdbdrLetterId.Value = objED.Decrypt(hdEnbdrLetterId.Value) 'Rakesh 2 Aug 08 Added
                        btnHistory.Disabled = False
                        btnHistory.Attributes.Add("onclick", "return PopupHistoryPage(" & "'" & hdEnbdrLetterId.Value & "'" & ");")
                    Else
                        btnHistory.Disabled = True
                    End If
                Else
                    btnHistory.Disabled = True
                End If
            Else
                btnHistory.Disabled = True
            End If
            '

            '*******************************************************************
            ''Code For 
            If Not Page.IsPostBack Then
                BindDropDown(drpAuthSig, "AurizedSignatory", False)
            End If

            ' ###################################################################
            ' @ New Code Added For Setting the value of Agency Details In New Mode on 12-03-08
            If Not Page.IsPostBack Then
                If (Request.QueryString("Action") IsNot Nothing) Then
                    If (Request.QueryString("Action") = "I") Then
                        If (Request.QueryString("ReqId") IsNot Nothing) Then
                            'txtLtrNo.Text = Request.QueryString("ReqId") 'Rakesh 2 Aug 08 commented
                            txtLtrNo.Text = objED.Decrypt(Request.QueryString("ReqId")) 'Rakesh 2 Aug 08 Added
                            hdRequestID.Value = txtLtrNo.Text
                            hdEnRequestID.Value = objED.Encrypt(txtLtrNo.Text) 'Rakesh 2 Aug 08 Added
                        End If
                        If (Request.QueryString("requestType") IsNot Nothing) Then
                            hdCallCategoryName.Value = Request.QueryString("requestType")
                        End If

                        If (Request.QueryString("LCode") IsNot Nothing) Then
                            'hdLcode.Value = Request.QueryString("LCode") 'Rakesh 2 Aug 08 commented
                            hdLcode.Value = objED.Decrypt(Request.QueryString("LCode")) 'Rakesh 2 Aug 08 Added
                            hdEnLcode.Value = Request.QueryString("LCode") 'Rakesh 2 Aug 08 Added
                            Dim objInputXml, objOutputXml As New XmlDocument
                            Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
                            objInputXml.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")
                            ' objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerXml = (Request.QueryString("LCode")) 'Rakesh 2 Aug 08 commented
                            objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerXml = hdLcode.Value 'Rakesh 2 Aug 08 Added
                            objOutputXml = objbzAgency.View(objInputXml)
                            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                                With objOutputXml.DocumentElement.SelectSingleNode("Agency")
                                    txtAgencyName.Text = .Attributes("NAME").Value()
                                    txtAddress.Text = .Attributes("ADDRESS").Value()
                                    txtCity.Text = .Attributes("CITY").Value()
                                    txtCountry.Text = .Attributes("COUNTRY").Value()
                                    txtPhone.Text = .Attributes("PHONE").Value()
                                    txtFax.Text = .Attributes("FAX").Value()
                                    txtOfficeId.Text = .Attributes("OFFICEID").Value()
                                    txtOnlineStatus.Text = .Attributes("ONLINE_STATUS").Value()
                                End With
                            End If
                        End If
                    End If
                End If
            End If
            ' @ New Code Added For Setting the value of Agency Details In New Mode on 12-03-0
            ' #########################################################################


            If Not Page.IsPostBack Then                '

                '*******************************************************************
                ''Code For 
                'BindDropDown(drpAuthSig, "AurizedSignatory", False)
                ' Code for Getting Template in new mode
                If (Request.QueryString("Action") = "I") Then
                    GetTemplate()
                End If
                '*******************************************************************
                ' Load Data For Editing Mode
                If (Request.QueryString("Action") = "U") Then
                    hdEnbdrLetterId.Value = Request.QueryString("HD_RE_BDR_ID").ToString 'Rakesh 2 Aug 08 Added
                    hdbdrLetterId.Value = objED.Decrypt(hdEnbdrLetterId.Value) 'Rakesh 2 Aug 08 Added
                    ViewBdrLetter()
                    ' txtCategory.Focus()
                    If (Request.QueryString("Msg") = "A") Then
                        lblError.Text = objeAAMSMessage.messInsert
                    End If
                    If (Request.QueryString("Msg") = "U") Then
                        lblError.Text = objeAAMSMessage.messUpdate
                    End If
                End If

            End If

            ' #######################################
            ' ########## This Code code is used for enable/disable 
            ' ########## the button according to rights
            'Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR BDRLetter']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR BDRLetter']").Attributes("Value").Value)
                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                        ' btnSave.Enabled = False
                    End If
                    If (Request.QueryString("Action") = "U") Then
                        If strBuilder(2) = "0" Then
                            btnSave.Enabled = False
                        End If
                        'If strBuilder(2) = "0" Then

                        'End If
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            ' #######################################
            ' ########## End of code used for enable/disable 
            ' ########## the button according to rights
            ' btnReset.Attributes.Add("onclick", "return CityReset();")


            '@ New Code
            If (Request.QueryString("Action") = "I") Then
                btnPrint.Enabled = False
            End If
            '@End of New Code



        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region


#Region " Load ViewBdrLetter Data For Editing Mode"
    Sub ViewBdrLetter()
        Try
            If (Not Request.QueryString("Action") = Nothing) Then
                Dim objInputXml, objOutputXml As New XmlDocument
                'Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                Dim objbzbzRequestBDR As New AAMS.bizBRHelpDesk.bzRequestBDR
                objInputXml.LoadXml("<HD_VIEWBDRLETTER_INPUT><HD_RE_BDR_ID /></HD_VIEWBDRLETTER_INPUT>")
                '  objInputXml.DocumentElement.SelectSingleNode("HD_RE_BDR_ID").InnerText = Request.QueryString("HD_RE_BDR_ID").ToString() 'Rakesh Commented on 2 aug 08
                objInputXml.DocumentElement.SelectSingleNode("HD_RE_BDR_ID").InnerText = hdbdrLetterId.Value 'Rakesh Added on 2 aug 08
                'Here Back end Method Call
                objOutputXml = objbzbzRequestBDR.View(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    BindBdrLetterData(objOutputXml)
                    ' img1A.Attributes.Add("onClick", "<script>Javascript:'#';</script>")
                    img1A.Disabled = True
                    ImgBDRSendDate.Disabled = True
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "Call BindBdrLetterData For Binding Data in Controls"
    Sub BindBdrLetterData(ByVal objOutputXml As XmlDocument)

        Try
            '<HD_VIEWBDRLETTER_OUTPUT><BDRLetter ADRESS='' AGENCY='' AUTHORISED=''
            'CITY='' COUNTRY='' FAX='' HD_RE_BDR_AIRLINEOFFICE='' AIRLINECODE = ''
            'AIRLINENAME='' AIRLINEOFFICEADDRESS = '' HD_RE_BDR_DATESEND=''
            'HD_RE_BDR_ID='' HD_RE_BDR_LETTER='' HD_RE_BDR_SENDBY='' HD_RE_BDR_TICKETS=''
            'LTRNO='' CALL_CATEGORY_NAME  ='' ONLINESTATUS='' PHONE=''
            'PRIORITY=''></BDRLetter><Errors Status=''><Error Code=''
            'Description=''></Error></Errors></HD_VIEWBDRLETTER_OUTPUT>


            ' Agency Detais

            txtAgencyName.Text = objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("AGENCY").InnerText
            txtCity.Text = objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("CITY").InnerText
            txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("ADRESS").InnerText
            txtCountry.Text = objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("COUNTRY").InnerText
            txtFax.Text = objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("FAX").InnerXml
            txtOnlineStatus.Text = objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("ONLINESTATUS").InnerText
            txtPhone.Text = objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("PHONE").InnerText
            txtOfficeId.Text = objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("OFFICEID").InnerText
            ' txtPriority.Text = objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("PRIORITY").InnerXml
            hdAirLineCode.Value = objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("AIRLINECODE").InnerText
            ' Bdr Details
            HdAROFID.Value = objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_BDR_AIRLINEOFFICE").InnerText
            txtBDrId.Text = objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_BDR_ID").InnerText
            txtBDRTicket.Text = objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_BDR_TICKETS").InnerText
            txtLtrNo.Text = objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("LTRNO").InnerText
            txtBdrSentBy.Text = objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_BDR_SENDBY").InnerText
            hdEmployeeId.Value = objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_BDR_ID").InnerText
            txtAirLineoffice.Text = objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("AIRLINENAME").InnerText
            ' txtAirLineoffice.Text = objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_BDR_AIRLINEOFFICE").InnerXml
            txtBDRBDRSendDate.Text = objeAAMS.ConvertDateBlank(objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_BDR_DATESEND").InnerText)
            txtBdrLetter.Text = objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_BDR_LETTER").InnerText
            txtAirLineOfficeAdd.Text = objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("AIRLINEOFFICEADDRESS").InnerText
            hdCallCategoryName.Value = objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("CALL_CATEGORY_NAME").InnerText

            hdbdrLetterId.Value = txtBDrId.Text
            hdEnbdrLetterId.Value = objED.Encrypt(txtBDrId.Text) 'Rakesh 2 Aug 08 Added
            Dim strbody As String = txtBdrLetter.Text
            strbody = strbody.Replace("<<BDRID>>", " " & txtBDrId.Text)
            txtBdrLetter.Text = strbody
            hdRequestID.Value = txtLtrNo.Text
            hdEnRequestID.Value = objED.Encrypt(txtLtrNo.Text) 'Rakesh 2 Aug 08 Added
            hdLcode.Value = objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("LCODE").InnerText
            hdEnLcode.Value = objED.Encrypt(hdLcode.Value) 'Rakesh 2 Aug 08 Added


            'GetTemplate()
            'Dim li As ListItem
            'li = drpAuthSig.Items.FindByValue(objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("AUTHORISED").InnerXml)
            'If li IsNot Nothing Then
            '    If li.Selected = False Then
            '        li.Selected = True
            '    End If
            'End If
            'If objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_ID").InnerXml IsNot Nothing Then
            'hdReqId.Value = objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_ID").InnerXml
            ' End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnSave_Click Event"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If (IsValid) Then
            Dim strbdrLetterId As String = ""
            Try

                If (Not Request.QueryString("Action") = Nothing) Then
                    '<HD_UPDATEBDRLETTER_INPUT>
                    ' <BDRLetter   
                    '    HD_RE_BDR_ID =''
                    '    HD_RE_ID=''      
                    '    HD_RE_BDR_TICKETS =''
                    '             HD_RE_BDR_DATESEND =''     
                    '    HD_RE_BDR_AIRLINEOFFICE =''     
                    '             HD_RE_BDR_LETTER ='' 
                    '    HD_RE_BDR_SENDBY =''     
                    '    AUTHORISED = ''
                    '    EMPLOYEEID='' />
                    '</HD_UPDATEBDRLETTER_INPUT>
                    Dim objInputXml As New XmlDataDocument, objUpdateInputXml, objOutputXml As New XmlDocument
                    GetTemplate()
                    'objUpdateInputXml.LoadXml("<HD_VIEWBDRLETTER_OUTPUT><BDRLetter ADRESS='' AUTHORISED='' CITY='' COUNTRY='' FAX='' HD_RE_BDR_AIRLINEOFFICE='' AIRLINENAME='' AIRLINEOFFICEADDRESS=''   HD_RE_BDR_DATESEND='' HD_RE_BDR_ID='' HD_RE_BDR_LETTER='' HD_RE_BDR_SENDBY='' HD_RE_BDR_TICKETS=''  LTRNO='' ONLINESTATUS='' PHONE='' PRIORITY=''  AOFFICEADDRESS='' AOFFICECITYNAME='' AOFFICECOUNTRY='' AOFFICEPIN='' AOFFICEFAX='' AOFFICEPHONE='' VERSION=''  /> <Errors Status='FALSE'>  <Error Code='' Description='' />   </Errors> </HD_VIEWBDRLETTER_OUTPUT>")
                    'UpdateTemplate(objUpdateInputXml)
                    Dim objbzbzRequestBDR As New AAMS.bizBRHelpDesk.bzRequestBDR
                    objInputXml.LoadXml("<HD_UPDATEBDRLETTER_INPUT><BDRLetter  HD_RE_BDR_ID ='' HD_RE_ID='' HD_RE_BDR_TICKETS ='' HD_RE_BDR_DATESEND ='' HD_RE_BDR_AIRLINEOFFICE ='' HD_RE_BDR_LETTER ='' HD_RE_BDR_SENDBY ='' AUTHORISED = '' EMPLOYEEID='' /></HD_UPDATEBDRLETTER_INPUT>")
                    If (Request.QueryString("HD_RE_BDR_ID") IsNot Nothing) Then
                        'objInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_BDR_ID").Value = Request.QueryString("HD_RE_BDR_ID") 'Rakesh Commented 2 Aug 08
                        objInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_BDR_ID").Value = hdbdrLetterId.Value 'Rakesh Addded 2 Aug 08
                    End If
                    'Rakesh Code start
                    If hdbdrLetterId.Value <> "" Then
                        objInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_BDR_ID").Value = hdbdrLetterId.Value
                    End If
                    'code End
                    If (Request.QueryString("Action") IsNot Nothing) Then
                        If (Request.QueryString("Action") = "I") Then
                            If (Request.QueryString("ReqID") IsNot Nothing) Then
                                'objInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_ID").Value = Request.QueryString("ReqID") ' "26538341" ' Hardcoded Session("HD_RE_ID") 'Rakesh Commented 2 Aug 08
                                objInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_ID").Value = hdRequestID.Value 'Rakesh Added 2 Aug 08
                            End If
                            If Not Session("LoginSession") Is Nothing Then
                                objInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_BDR_SENDBY").Value = Session("LoginSession").ToString().Split("|")(0)
                            End If
                        End If
                    End If
                    If (Request.QueryString("Action") = "U") Then
                        objInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_ID").Value = txtLtrNo.Text
                    End If

                    'Rakesh Code start
                    If (Request.QueryString("Action") = "U" And Request.QueryString("ReqID") IsNot Nothing) Then
                        'objInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_ID").Value = Request.QueryString("ReqID") '"26538341" ' Hardcoded Session("HD_RE_ID")
                        objInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_ID").Value = hdRequestID.Value ' Rakesh Commented 2 Aug 08
                    End If
                    'code End


                    objInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_BDR_TICKETS").Value = txtBDRTicket.Text
                    If (Request("txtBDRBDRSendDate") IsNot Nothing) Then '
                        objInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_BDR_DATESEND").Value = objeAAMS.ConvertTextDate(Request("txtBDRBDRSendDate").Trim()) '.Text
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_BDR_DATESEND").Value = objeAAMS.ConvertTextDate(txtBDRBDRSendDate.Text) '.Text
                    End If

                    objInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_BDR_AIRLINEOFFICE").Value = HdAROFID.Value ' txtAirLineoffice.Text
                    objInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_BDR_LETTER").Value = txtBdrLetter.Text
                    If (Request.QueryString("Action") = "U") Then
                        objInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_BDR_SENDBY").Value = hdEmployeeId.Value ' drpBdrSentBy.SelectedValue
                    End If

                    objInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("AUTHORISED").Value = drpAuthSig.SelectedValue

                    If Not Session("LoginSession") Is Nothing Then
                        objInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("EMPLOYEEID").Value = Session("LoginSession").ToString().Split("|")(0)
                    End If


                    ''Here Back end Method Call
                    objOutputXml = objbzbzRequestBDR.Update(objInputXml)

                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        strbdrLetterId = objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_BDR_ID").Value


                        '################################################
                        ' @ New Code Added  By abhishek on 16-03-08
                        '################################################

                        If (Request.QueryString("Action") = "U") Then
                            hdbdrLetterId.Value = strbdrLetterId
                            hdEnbdrLetterId.Value = objED.Encrypt(strbdrLetterId) 'Rakesh 2 Aug 08 Added
                            If Request.QueryString("Popup") Is Nothing Then
                                'Response.Redirect("HDUP_BDR.aspx?Msg=U&Action=U&HD_RE_BDR_ID=" & objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_BDR_ID").Value, False)
                                Response.Redirect("HDUP_BDR.aspx?Msg=U&Action=U&HD_RE_BDR_ID=" & hdEnbdrLetterId.Value, False) 'Rakesh 2 Aug 08 inplace of above line 
                            Else
                                'Response.Redirect("HDUP_BDR.aspx?Msg=U&Action=U&HD_RE_BDR_ID=" & objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_BDR_ID").Value, False)
                                Response.Redirect("HDUP_BDR.aspx?Popup=T&Msg=U&Action=U&HD_RE_BDR_ID=" & hdEnbdrLetterId.Value, False) 'Rakesh 2 Aug 08 inplace of above line
                            End If

                        End If
                        If (Request.QueryString("Action") = "I") Then
                            hdbdrLetterId.Value = strbdrLetterId
                            hdEnbdrLetterId.Value = objED.Encrypt(strbdrLetterId) 'Rakesh 2 Aug 08 Added
                            Response.Redirect("HDUP_BDR.aspx?Popup=T&Msg=A&Action=U&HD_RE_BDR_ID=" & hdEnbdrLetterId.Value, False)
                        End If




                        '#################################################
                        ' @ End of New Code Added  By abhishek on 16-03-08
                        '################################################




                        'If (Request.QueryString("Action") = "U") Then
                        '    lblError.Text = objeAAMSMessage.messUpdate ' "Record updated successfully."
                        '    'Rakesh start code
                        '    hdbdrLetterId.Value = strbdrLetterId
                        '    'End code
                        '    If Not Request.QueryString("ReqID") Is Nothing Then
                        '        '  ClientScript.RegisterClientScriptBlock(Me.GetType, "st", "<script>window.opener.document.forms['form1']['txtBDRLetterID'].value='" + strbdrLetterId + "';window.close();</script>")
                        '    Else
                        '        Response.Redirect("HDUP_BDR.aspx?&Msg=U&Action=U&HD_RE_BDR_ID=" & objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_BDR_ID").Value, False)
                        '    End If

                        'Else
                        '    lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."
                        '    'Rakesh Code start
                        '    If hdbdrLetterId.Value <> "" Then
                        '        lblError.Text = objeAAMSMessage.messUpdate
                        '    End If
                        '    hdbdrLetterId.Value = strbdrLetterId
                        '    'code End
                        '    ' Response.Redirect("HDUP_BDR.aspx?&Msg=A&Action=U&HD_RE_BDR_ID=" & objOutputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_BDR_ID").Value, False)
                        '    ' ClientScript.RegisterClientScriptBlock(Me.GetType, "st", "<script>window.opener.document.forms['form1']['txtBDRLetterID'].value='" + strbdrLetterId + "';window.opener.document.forms['form1']['hdBDRLetterID'].value='" + strbdrLetterId + "';window.close();</script>")
                        'End If
                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                    ' strbdrLetterId = "34324"
                    ' ClientScript.RegisterClientScriptBlock(Me.GetType, "st", "<script>window.opener.document.forms['form1']['txtBDRLetterID'].value='" + strbdrLetterId + "';window.close();</script>")

                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
        End If
    End Sub
#End Region
#Region "btnReset_Click Event"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If (Request.QueryString("Action") = "U") Then
                ViewBdrLetter()
                txtAgencyName.Focus()
            Else
                ' Setting Value In New Mode '   Like From Session , HiddenField
                txtBDrId.Text = ""
                txtBDRTicket.Text = ""
                txtLtrNo.Text = ""
                hdEmployeeId.Value = ""
                txtBdrSentBy.Text = ""
                ' HdAROFID.Value =
                'hdAirLineCode.Value=
                ' hdAilLineName.Value=
                'txtAirLineoffice.Text
                'txtAirLineOfficeAdd.Text
                'hdAoffice.Value
                txtBdrLetter.Text = ""
                'hdTemplateVersion.Value =
                'txtAirLineoffice.Text = ""
                'txtBDRBDRSendDate.Text = ""
                txtBdrLetter.Text = ""
                GetTemplate()
            End If
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "btnPrint_ClickEvent"
    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            GetTemplate()

            Dim objUpdateInputXml, objInputXml, objSecurityXml, objOutputXml As New XmlDocument

            objUpdateInputXml.LoadXml("<HD_VIEWBDRLETTER_OUTPUT><BDRLetter ADRESS='' AUTHORISED='' CITY='' COUNTRY='' FAX='' HD_RE_BDR_AIRLINEOFFICE='' AIRLINENAME='' AIRLINEOFFICEADDRESS=''   HD_RE_BDR_DATESEND='' HD_RE_BDR_ID='' HD_RE_BDR_LETTER='' HD_RE_BDR_SENDBY='' HD_RE_BDR_TICKETS=''  LTRNO='' ONLINESTATUS='' PHONE='' PRIORITY=''  AOFFICEADDRESS='' AOFFICECITYNAME='' AOFFICECOUNTRY='' AOFFICEPIN='' AOFFICEFAX='' AOFFICEPHONE='' VERSION=''  /> <Errors Status='FALSE'>  <Error Code='' Description='' />   </Errors> </HD_VIEWBDRLETTER_OUTPUT>")
            UpdateTemplate(objUpdateInputXml)

            objOutputXml = objUpdateInputXml 'objInputXml
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Session("eBRDLetterRpt") = objOutputXml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=BR BRDLetter", False)
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    Private Sub GetTemplate()

        Try
            Dim strbody As String = ""
            Dim objInputTempXml, objOutputTempXml, objSecurityXml As New XmlDocument
            Dim objbzbzTemplate As New AAMS.bizMaster.bzTemplate
            Dim ds As New DataSet
            Dim strAoffice As String = ""

            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                    strAoffice = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                Else
                    strAoffice = ""
                End If
            End If
            If Request.QueryString("HD_RE_BDR_ID") IsNot Nothing Then

            End If


            '#####################################################################
            ' Getting Template for BDR Template

            objInputTempXml.LoadXml("<HD_DOCUMENTTEMPLATE_INPUT><TEMPLATES TemplateName='' AOFFICE ='' AIRLINECODE ='' /></HD_DOCUMENTTEMPLATE_INPUT>")
            objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("TemplateName").Value() = "BR BDR Letter" ' Session("Action").ToString().Split("|").GetValue(1).ToString()
            objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("AOFFICE").Value() = strAoffice ' hdAoffice.Value  
            objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("AIRLINECODE").Value() = hdAirLineCode.Value

            'objOutputTempXml.LoadXml("<HD_DOCUMENTTEMPLATE_OUTPUT><TEMPLATES TemplateName='' AOFFICE ='' AIRLINECODE ='' BDRTemplate = ''/><Errors Status='FALSE'><Error Code='' Description=''/></Errors></HD_DOCUMENTTEMPLATE_OUTPUT>")
            objOutputTempXml = objbzbzTemplate.GetDocumentTemplate(objInputTempXml)
            If objOutputTempXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                hdTemplateVersion.Value = objOutputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("T_G_DOC_VERSION").Value
                strbody = objOutputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("BDRTemplate").Value
                '###############################################################################
                '@ Now find the value of Body parts on the basis of Lcode
                If txtBDrId.Text.Trim.Length > 0 Then
                    strbody = strbody.Replace("<<BDRID>>", " " & txtBDrId.Text)
                End If
                'If (Request("txtBDRBDRSendDate") IsNot Nothing) Then '
                '    strbody = strbody.Replace("<<TXTDATE>>", " " & (Request("txtBDRBDRSendDate").Trim())) '.Text
                'Else
                '    strbody = strbody.Replace("<<TXTDATE>>", " " & (txtBDRBDRSendDate.Text)) '.Text
                'End If
                ' Dim TestString As String = FormatDateTime(Request("txtBDRBDRSendDate").Trim(), DateFormat.LongDate)
                If (Request("txtBDRBDRSendDate") IsNot Nothing) Then '
                    If (Request("txtBDRBDRSendDate").Trim().Length > 0) Then
                        strbody = strbody.Replace("<<TXTDATE>>", " " & (Request("txtBDRBDRSendDate").Trim())) '.Text
                    End If
                Else
                    If (txtBDRBDRSendDate.Text.Trim().Length > 0) Then
                        strbody = strbody.Replace("<<TXTDATE>>", " " & (txtBDRBDRSendDate.Text)) '.Text
                    End If
                End If
                If (Request("txtAirLineOfficeAdd") IsNot Nothing) Then '
                    If (Request("txtAirLineOfficeAdd").Trim().Length > 0) Then
                        strbody = strbody.Replace("<<TXTAIROFFICE>>", " " & Request("txtAirLineOfficeAdd").Trim()) '.Text
                    End If
                Else
                    If (txtAirLineOfficeAdd.Text.Trim().Length > 0) Then
                        strbody = strbody.Replace("<<TXTAIROFFICE>>", " " & txtAirLineOfficeAdd.Text)
                    End If
                End If
                If txtBDRTicket.Text.Trim.Length > 0 Then
                    strbody = strbody.Replace("<<TKTNO>>", " " & txtBDRTicket.Text)
                End If

                If (Request("txtAgencyName") IsNot Nothing) Then '
                    If Request("txtAgencyName").Trim().Length > 0 Then
                        strbody = strbody.Replace("<<TXTAGENCYNAME>>", " " & Request("txtAgencyName").Trim()) '.Text
                    End If
                Else
                    If (txtAgencyName.Text.Trim.Length > 0) Then
                        strbody = strbody.Replace("<<TXTAGENCYNAME>>", " " & txtAgencyName.Text) '.Text
                    End If
                End If

                If (Request("txtAirLineoffice") IsNot Nothing) Then '
                    If (Request("txtAirLineoffice").Trim().Length > 0) Then
                        strbody = strbody.Replace("<<TXTAIRNAME>>", " " & Request("txtAirLineoffice").Trim()) '.Text
                    End If

                Else
                    If (txtAirLineoffice.Text.Trim().Length > 0) Then
                        strbody = strbody.Replace("<<TXTAIRNAME>>", " " & txtAirLineoffice.Text) '.Text
                    End If
                End If

                'Now Calling employee View()
                'Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
                'Dim objInputobjbzEmployee, objOutputobjbzEmployee As New XmlDocument
                'objInputobjbzEmployee.LoadXml("<MS_VIEWEMPLOYEE_INPUT><EmployeeID></EmployeeID></MS_VIEWEMPLOYEE_INPUT>")
                'objInputobjbzEmployee.DocumentElement.SelectSingleNode("EmployeeID").InnerXml = drpAuthSig.SelectedValue
                ''Here Back end Method Call
                'objOutputobjbzEmployee = objbzEmployee.View(objInputobjbzEmployee)
                'If objOutputobjbzEmployee.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                '    strbody = strbody.Replace("<<TXTBRANCHHEAD>>", " ")
                '    strbody = strbody.Replace("<<TXTDESIGNATION>>", " " & objOutputobjbzEmployee.DocumentElement.SelectSingleNode("Employee").Attributes("Designation").Value())
                'End If

                strbody = strbody.Replace("<<TXTBRANCHHEAD>>", " " & drpAuthSig.SelectedItem.Text)
                strbody = strbody.Replace("<<TXTDESIGNATION>>", " " & drpAuthSig.SelectedValue)
                'Here Back end Method Call

                strbody = strbody.Replace("<<AOFFICE>>", " " & "")

                If (Request.QueryString("requestType") IsNot Nothing) Then
                    hdCallCategoryName.Value = Request.QueryString("requestType")
                End If
                If (hdCallCategoryName.Value.Length > 0) Then
                    strbody = strbody.Replace("<<TXTREQUESTTYPE>>", " " & hdCallCategoryName.Value)

                End If



                txtBdrLetter.Text = strbody
            End If

        Catch ex As Exception

        End Try

    End Sub

    Public Sub BindDropDown(ByVal drpDownList As DropDownList, ByVal strType As String, ByVal bolSelect As Boolean)
        Try
            Dim objInputAurizedSignatoryXml, objSecurityXml, objOutputAurizedSignatoryXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim objbzDocEmployee As New AAMS.bizMaster.bzDocEmployee
            Dim ds As New DataSet
            Dim strAoffice As String = ""

            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                    strAoffice = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                Else
                    strAoffice = ""
                End If
            End If
            drpDownList.Items.Clear()
            Select Case strType
                Case "AurizedSignatory"
                    objInputAurizedSignatoryXml.LoadXml("<DOCUMENTEMPLOYEE_INPUT><DOCUMENT Aoffice='' /></DOCUMENTEMPLOYEE_INPUT>")
                    objInputAurizedSignatoryXml.DocumentElement.SelectSingleNode("DOCUMENT").Attributes("Aoffice").InnerText = strAoffice

                    'Here Back end Method Call
                    objOutputAurizedSignatoryXml = objbzDocEmployee.List(objInputAurizedSignatoryXml)

                    If objOutputAurizedSignatoryXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        objXmlReader = New XmlNodeReader(objOutputAurizedSignatoryXml)
                        ds.ReadXml(objXmlReader)
                        drpDownList.DataSource = ds.Tables("DOCUMENT")
                        drpDownList.DataTextField = "EmployeeName"
                        drpDownList.DataValueField = "DESIGNATION"
                        drpDownList.DataBind()
                    End If
            End Select
            If bolSelect = True Then
                drpDownList.Items.Insert(0, New ListItem("---Select One---", ""))
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub UpdateTemplate(ByRef objUpdateInputXml As XmlDocument)
        Dim objSecurityXml, objOutputXml As New XmlDocument
        'objUpdateInputXml.LoadXml("<HD_VIEWBDRLETTER_OUTPUT><BDRLetter ADRESS='' AUTHORISED='' CITY='' COUNTRY='' FAX='' HD_RE_BDR_AIRLINEOFFICE='' AIRLINENAME='' AIRLINEOFFICEADDRESS=''   HD_RE_BDR_DATESEND='' HD_RE_BDR_ID='' HD_RE_BDR_LETTER='' HD_RE_BDR_SENDBY='' HD_RE_BDR_TICKETS=''  LTRNO='' ONLINESTATUS='' PHONE='' PRIORITY=''  AOFFICEADDRESS='' AOFFICECITYNAME='' AOFFICECOUNTRY='' AOFFICEPIN='' AOFFICEFAX='' AOFFICEPHONE='' VERSION=''  /> <Errors Status='FALSE'>  <Error Code='' Description='' />   </Errors> </HD_VIEWBDRLETTER_OUTPUT>")
        objUpdateInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("HD_RE_BDR_LETTER").Value = txtBdrLetter.Text
        If (Request("txtAirLineOfficeAdd") IsNot Nothing) Then '
            objUpdateInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("AIRLINEOFFICEADDRESS").Value = Request("txtAirLineOfficeAdd").Trim()
        Else
            objUpdateInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("AIRLINEOFFICEADDRESS").Value = txtAirLineOfficeAdd.Text
        End If

        ' #############################################################
        ' Getting Value of AOFFICE Details On The Basis of AOffice 
        Dim strAoffice As String = ""

        If Session("Security") IsNot Nothing Then
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                strAoffice = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
            Else
                strAoffice = ""
            End If
        End If
        'Now Calling Aoffice View()
        Dim objbbzAoffice As New AAMS.bizMaster.bzAOffice
        Dim objInputAoffice, objOutputAoffice As New XmlDocument
        objInputAoffice.LoadXml("<MS_VIEWAOFFICE_INPUT><Aoffice></Aoffice></MS_VIEWAOFFICE_INPUT>")
        objInputAoffice.DocumentElement.SelectSingleNode("Aoffice").InnerXml = strAoffice
        'Here Back end Method Call
        objOutputAoffice = objbbzAoffice.View(objInputAoffice)
        If objOutputAoffice.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objUpdateInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("AOFFICEADDRESS").Value = objOutputAoffice.DocumentElement.SelectSingleNode("AOFFICE").Attributes("Address").Value()
            objUpdateInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("AOFFICECITYNAME").Value = "" ' objOutputAoffice.DocumentElement.SelectSingleNode("AOFFICE").Attributes("Aoffice").Value()
            objUpdateInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("AOFFICECOUNTRY").Value = "" ' objOutputAoffice.DocumentElement.SelectSingleNode("AOFFICE ").Attributes("Designation").Value()
            objUpdateInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("AOFFICEPIN").Value = objOutputAoffice.DocumentElement.SelectSingleNode("AOFFICE").Attributes("Pincode").Value()
            objUpdateInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("AOFFICEFAX").Value = objOutputAoffice.DocumentElement.SelectSingleNode("AOFFICE").Attributes("Fax").Value()
            objUpdateInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("VERSION").Value = hdTemplateVersion.Value

            objUpdateInputXml.DocumentElement.SelectSingleNode("BDRLetter").Attributes("AOFFICEPHONE").Value = objOutputAoffice.DocumentElement.SelectSingleNode("AOFFICE").Attributes("Phone").Value()


        End If
        ' #############################################################

    End Sub
    Protected Sub btnLtr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLTR.Click
        If Request.QueryString("Popup") Is Nothing Then
            Response.Redirect("HDUP_CallLog.aspx?Action=U&LCode=" + hdEnLcode.Value + "&HD_RE_ID=" + hdEnRequestID.Value)
        End If
    End Sub

    Protected Sub btnHistory_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistory.ServerClick

    End Sub

    Protected Sub txtOnlineStatus_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOnlineStatus.TextChanged

    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
End Class
