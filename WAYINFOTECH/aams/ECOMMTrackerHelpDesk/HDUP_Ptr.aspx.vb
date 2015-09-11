Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml

Partial Class ETHelpDesk_HDUP_Ptr
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl

            btnSave.Attributes.Add("onclick", "return PtrValidate();")
            ' drpStatus.Attributes.Add("onchange", "return AssignCurrentDate();")

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If

            'Dim strDt As String = DateTime.Now.ToShortDateString()
            'strDt = Format(Convert.ToDateTime(strDt), "dd/MM/yyyy")
            'hdCurrentVal.Value = strDt

            'Rakesh start code 
            'If Request.QueryString("Popup") Is Nothing Then
            '    lnkClose.Visible = False
            'Else
            '    lnkClose.Visible = True
            'End If

            If Request.QueryString("Popup") Is Nothing Then
                lnkClose.Visible = False
                ' btnLtr.Visible = False
            Else
                lnkClose.Visible = True
                btnLtr.Visible = False
            End If

            'End code

            If Not Page.IsPostBack Then
                BindDropDowns()



                'Code Modified on 27th December

                If Request.QueryString("Popup") IsNot Nothing Then
                    If Request.QueryString("Popup").Trim() = "T" Then
                        If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "I" Then
                            Dim strValu As String = ReturnPTR_statusValu()
                            If strValu.Trim().Length = 0 Then
                                drpStatus.SelectedIndex = 0
                            Else
                                'drpStatus.Items.FindByValue(strValu).Selected = True
                                Dim lst As ListItem = CallStatus()
                                'strValu &= "|"
                                'strValu &= "0"
                                drpStatus.ClearSelection()
                                Dim lstNew As New ListItem
                                lstNew = drpStatus.Items.FindByValue(lst.Value)
                                If lstNew IsNot Nothing Then
                                    drpStatus.SelectedValue = lstNew.Value
                                End If
                                ' drpStatus.Items.FindByText(lst.Text 
                                ' drpStatus.SelectedIndex = 1
                            End If
                        End If
                    End If
                End If


                'End of Code Modification on 27th December


                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Then
                    hdPtrID.Value = Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()
                    hdEnPtrID.Value = Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()
                    ViewPtrDetails()
                    ' imgAgency.Visible = False
                    BindListForPTR(hdPtrID.Value + "|" + txtPtrNo.Text)
                Else
                    'ViewPtrInsertDetails()
                End If
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                    hdPtrID.Value = Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()
                    hdEnPtrID.Value = Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()
                    ViewPtrDetails()
                    BindListForPTR(hdPtrID.Value + "|" + txtPtrNo.Text)
                    '  imgAgency.Visible = False
                    lblError.Text = objeAAMSMessage.messInsert
                    If Not Request.QueryString("Msg") Is Nothing Then
                        lblError.Text = objeAAMSMessage.messUpdate
                    End If
                Else
                    'ViewPtrInsertDetails()
                End If

                'Rakesh Start code
                If Not Request.QueryString("ReqID") Is Nothing And Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() <> "U" Then
                    hdRequestID.Value = objED.Decrypt(Request.QueryString("ReqID").ToString)
                    hdEnRequestID.Value = Request.QueryString("ReqID").ToString
                    txtPtrNo.ReadOnly = False
                    txtPtrNo.CssClass = "textbox"

                End If
                If Not Request.QueryString("LCode") Is Nothing And Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() <> "U" Then
                    hdEnLcode.Value = Request.QueryString("LCode")
                    hdLcode.Value = objED.Decrypt(Request.QueryString("LCode"))
                    AgencyView()
                End If
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "I" Then
                    ViewPtrInsertDetails()
                End If
                'End code

                'If txtCloseDt.Text.Trim().Length <> 0 Then
                '    btnSave.Enabled = False
                '    drpAssignedTo.Enabled = False
                '    drpFollowup.Enabled = False


                '    drpStatus.Enabled = False


                '    drpSeverity.Enabled = False
                '    drpType.Enabled = False
                '    'img2.Disabled = True
                '    'img1.Disabled = True
                '    'imgReceivedFrom.Disabled = True
                '    'txtOpenDt.ReadOnly = True
                '    'txtOpenDt.CssClass = "textboxgrey"
                '    'txtAssignedDt.ReadOnly = True
                '    'txtAssignedDt.CssClass = "textboxgrey"
                '    txtPtrTitle.ReadOnly = True
                '    txtPtrTitle.CssClass = "textboxgrey"

                '    'Code Modified on 29th Dece
                '    'txtPtrNo.ReadOnly = True
                '    'txtPtrNo.CssClass = "textboxgrey"

                '    'Code Modified on 29th Dece



                '    'txtCloseDt.ReadOnly = True
                '    'txtCloseDt.CssClass = "textboxgrey"
                'End If



            End If

            CheckSecurity()
            If hdPtrID.Value = "" Then
                btnHistory.Disabled = True
                btnAssignHistory.Disabled = True
            Else
                btnHistory.Disabled = False
                btnAssignHistory.Disabled = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub ViewPtrDetails()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument

            '            Dim objhdPtrView As New AAMS.bizETrackerHelpDesk.bzPTR
            Dim objhdPtrView As New AAMS.bizETrackerHelpDesk.bzPTR

            objInputXml.LoadXml("<HD_VIEW_PTR_INPUT><HD_PTR_ID></HD_PTR_ID></HD_VIEW_PTR_INPUT>")
            'If Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString() IsNot Nothing Then
            '    objInputXml.DocumentElement.SelectSingleNode("HD_PTR_ID").InnerText = hdPtrID.Value 'Rakesh 1 Aug 08 Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()
            '    '  hdPtrID.Value = Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()
            'End If
            objInputXml.DocumentElement.SelectSingleNode("HD_PTR_ID").InnerText = hdPtrID.Value
            objOutputXml = objhdPtrView.View(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXml.DocumentElement.SelectSingleNode("PTR")
                    txtAgencyName.Text = .Attributes("NAME").Value
                    hdLcode.Value = .Attributes("LCODE").Value
                    txtAgencyAddress.Text = .Attributes("ADDRESS").Value
                    txtCity.Text = .Attributes("CITY").Value
                    txtPhone.Text = .Attributes("PHONE").Value
                    'txtPriority.Text = .Attributes("PRIORITY").Value
                    txtFax.Text = .Attributes("FAX").Value
                    txtOnlineStatus.Text = .Attributes("ONLINE_STATUS").Value
                    txtOfficeID.Text = .Attributes("OFFICEID").Value
                    txtPtrNo.Text = .Attributes("HD_PTR_REF").Value
                    drpType.SelectedValue = .Attributes("HD_PTR_TYPE_ID").Value
                    drpSeverity.SelectedValue = .Attributes("HD_PTR_SEV_ID").Value
                    drpFollowup.SelectedValue = .Attributes("HD_PTR_FOLLOWUP_ID").Value

                    drpStatus.ClearSelection()

                    Dim lst As New ListItem

                    lst = drpStatus.Items.FindByText(.Attributes("HD_STATUS_NAME").Value)
                    If lst IsNot Nothing Then
                        drpStatus.SelectedValue = lst.Value
                    End If
                    '.Selected = True

                    If .Attributes("HD_PTR_CLOSEDATE").Value.Trim().Length <> 0 Then
                        txtCloseDt.Text = objeAAMS.ConvertDateBlank(.Attributes("HD_PTR_CLOSEDATE").Value.Trim())
                    Else
                        txtCloseDt.Text = ""
                    End If

                    If .Attributes("HD_PTR_OPENDATE").Value.Trim().Length <> 0 Then
                        txtOpenDt.Text = objeAAMS.ConvertDateBlank(.Attributes("HD_PTR_OPENDATE").Value.Trim())
                    Else
                        txtOpenDt.Text = ""
                    End If

                    If .Attributes("ASSIGNED_DATE").Value.Trim().Length <> 0 Then
                        txtAssignedDt.Text = objeAAMS.ConvertDateBlank(.Attributes("ASSIGNED_DATE").Value.Trim())
                    Else
                        txtAssignedDt.Text = ""
                    End If
                    drpAssignedTo.SelectedValue = .Attributes("ASSIGNEEID").Value
                    hdAssigneeID.Value = .Attributes("ASSIGNEEID").Value
                    txtPtrTitle.Text = .Attributes("HD_PTR_TITLE").Value
                    txtCountry.Text = .Attributes("COUNTRY").Value
                    hdEnRequestID.Value = objED.Encrypt(.Attributes("HD_RE_ID").Value)
                    hdRequestID.Value = .Attributes("HD_RE_ID").Value
                    hdRPTRNO.Value = .Attributes("HD_PTR_REF").Value
                    hdRPTRId.Value = hdPtrID.Value
                    '   hdEnRPTRId.Value = objED.Encrypt(hdPtrID.Value)
                    hdEnRPTRId.Value = hdPtrID.Value
                End With
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub BindDropDowns()
        Try
            'objeAAMS.BindDropDown(drpCity, "CITY", True)
            'objeAAMS.BindDropDown(drpCountry, "COUNTRY", True)
            objeAAMS.BindDropDown(drpFollowup, "ET_HDFOLLOWUP", True)
            objeAAMS.BindDropDown(drpType, "ET_HDPTRTYPE", True)
            objeAAMS.BindDropDown(drpSeverity, "ET_HDPTRSEVERITY", True)
            'objeAAMS.BindDropDown(drpStatus, "HDPTRCALLSTATUS_NOTUSE", False)
            objeAAMS.BindDropDown(drpStatus, "ET_HDPTRSTATUSFORMANAGE", False)
            objeAAMS.BindDropDown(drpAssignedTo, "ET_HDASSIGNEDTO", True)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If (Not Request.QueryString("Action") = Nothing) Then
                Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                '                Dim objhdPtrSave As New AAMS.bizETrackerHelpDesk.bzPTR
                Dim objhdPtrSave As New AAMS.bizETrackerHelpDesk.bzPTR

                'objInputXml.LoadXml("<HD_UPDATE_PTR_INPUT><PTR HD_PTR_ID='' HD_RE_ID='' HD_PTR_REF='' HD_STATUS_ID='' HD_PTR_SEV_ID='' HD_PTR_FOLLOWUP_ID='' HD_PTR_TYPE_ID='' HD_PTR_OPENDATE='' HD_PTR_CLOSEDATE='' HD_PTR_TITLE='' ASSIGNEEID='' ASSIGNED_DATE='' HD_PTR_TYPE_NAME ='' HD_PTR_SEV_NAME='' HD_PTR_FOLLOWUP_NAME='' HD_STATUS_NAME='' ASSIGNEE_NAME='' EMPLOYEEID='' /></HD_UPDATE_PTR_INPUT>")
                objInputXml.LoadXml("<HD_UPDATE_PTR_INPUT><PTR HD_PTR_ID='' HD_RE_ID='' HD_PTR_REF='' HD_STATUS_ID='' HD_PTR_SEV_ID='' HD_PTR_FOLLOWUP_ID='' HD_PTR_TYPE_ID='' HD_STATUS_CLOSE='' HD_PTR_TITLE='' ASSIGNEEID='' HD_PTR_TYPE_NAME='' HD_PTR_SEV_NAME='' HD_PTR_FOLLOWUP_NAME='' HD_STATUS_NAME='' ASSIGNEE_NAME=''  EMPLOYEEID='' /></HD_UPDATE_PTR_INPUT>")
                'Rakesh start code
                If hdRPTRId.Value <> "" Then
                    objInputXml.DocumentElement.SelectSingleNode("PTR").Attributes("HD_PTR_ID").Value = hdRPTRId.Value
                End If
                'End code
                If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
                    objInputXml.DocumentElement.SelectSingleNode("PTR").Attributes("HD_PTR_ID").Value = hdRPTRId.Value ' Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()
                End If
                With objInputXml.DocumentElement.SelectSingleNode("PTR")
                    .Attributes("HD_RE_ID").Value = hdRequestID.Value
                    .Attributes("HD_PTR_REF").Value = txtPtrNo.Text.Trim()

                    Dim StrPtrStatus As String = drpStatus.SelectedValue.Trim()
                    Dim strStatus() As String = StrPtrStatus.Split("|")

                    .Attributes("HD_STATUS_ID").Value = strStatus(0).Trim() '
                    .Attributes("HD_PTR_SEV_ID").Value = drpSeverity.SelectedValue.Trim()
                    .Attributes("HD_PTR_FOLLOWUP_ID").Value = drpFollowup.SelectedValue.Trim()
                    .Attributes("HD_PTR_TYPE_ID").Value = drpType.SelectedValue.Trim()
                    'HD_STATUS_CLOSE
                    .Attributes("HD_STATUS_CLOSE").Value = strStatus(1).Trim()  'Need for Change drpType.SelectedValue.Trim()

                    '.Attributes("HD_PTR_OPENDATE").Value = objeAAMS.ConvertTextDateBlank(txtOpenDt.Text.Trim())
                    '.Attributes("HD_PTR_CLOSEDATE").Value = objeAAMS.ConvertTextDateBlank(txtCloseDt.Text.Trim())
                    .Attributes("HD_PTR_TITLE").Value = txtPtrTitle.Text.Trim()
                    .Attributes("ASSIGNEEID").Value = drpAssignedTo.SelectedValue
                    '.Attributes("ASSIGNED_DATE").Value = objeAAMS.ConvertTextDateBlank(txtAssignedDt.Text.Trim())

                    'HD_PTR_TYPE_NAME ='' HD_PTR_SEV_NAME='' HD_PTR_FOLLOWUP_NAME='' HD_STATUS_NAME='' ASSIGNEE_NAME='' EMPLOYEEID=''
                    .Attributes("HD_PTR_TYPE_NAME").Value = drpType.Items(drpType.SelectedIndex).Text 'drpType.Text
                    .Attributes("HD_PTR_SEV_NAME").Value = drpSeverity.Items(drpSeverity.SelectedIndex).Text   'drpSeverity.Text
                    .Attributes("HD_PTR_FOLLOWUP_NAME").Value = drpFollowup.Items(drpFollowup.SelectedIndex).Text  'drpFollowup.Text
                    .Attributes("HD_STATUS_NAME").Value = drpStatus.Items(drpStatus.SelectedIndex).Text  'drpStatus.Text
                    .Attributes("ASSIGNEE_NAME").Value = drpAssignedTo.Items(drpAssignedTo.SelectedIndex).Text  'drpAssignedTo.Text
                    'Following Statement is Written for 1A Employee ID
                    Dim str As String()
                    str = Session("LoginSession").ToString().Split("|")
                    .Attributes("EMPLOYEEID").Value = str(0)
                End With

                objOutputXml = objhdPtrSave.Update(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
                        If Not Request.QueryString("ReqID") Is Nothing Then
                            Dim strPTRNo As String
                            strPTRNo = objOutputXml.DocumentElement.SelectSingleNode("PTR").Attributes("HD_PTR_REF").Value
                            hdRPTRNO.Value = strPTRNo
                            ViewPtrDetails()
                            lblError.Text = objeAAMSMessage.messUpdate
                        Else
                            If Request.QueryString("Popup") Is Nothing Then

                                Response.Redirect("HDUP_Ptr.aspx?Action=US|" & objOutputXml.DocumentElement.SelectSingleNode("PTR").Attributes("HD_PTR_ID").Value + "&Msg=U", False)
                            Else

                                Response.Redirect("HDUP_Ptr.aspx?Action=US|" & objOutputXml.DocumentElement.SelectSingleNode("PTR").Attributes("HD_PTR_ID").Value + "&Popup=T", False)
                            End If

                            lblError.Text = objeAAMSMessage.messUpdate
                        End If
                    Else

                        hdPtrID.Value = objOutputXml.DocumentElement.SelectSingleNode("PTR").Attributes("HD_PTR_ID").Value
                        hdEnPtrID.Value = hdPtrID.Value
                        lblError.Text = objeAAMSMessage.messInsert
                        'Rakesh Start code
                        If hdRPTRId.Value <> "" Then
                            lblError.Text = objeAAMSMessage.messUpdate
                        End If
                        'End code

                        If Not Request.QueryString("ReqID") Is Nothing Then
                            Dim strPTRNo As String
                            strPTRNo = objOutputXml.DocumentElement.SelectSingleNode("PTR").Attributes("HD_PTR_REF").Value
                            hdRPTRNO.Value = strPTRNo
                            hdRPTRId.Value = objOutputXml.DocumentElement.SelectSingleNode("PTR").Attributes("HD_PTR_ID").Value
                            hdEnRPTRId.Value = objOutputXml.DocumentElement.SelectSingleNode("PTR").Attributes("HD_PTR_ID").Value
                        End If
                        If Request.QueryString("Popup") Is Nothing Then
                            Response.Redirect("HDUP_Ptr.aspx?Action=US|" & hdEnPtrID.Value + "&LCode=" + hdEnLcode.Value, False)
                        Else
                            Response.Redirect("HDUP_Ptr.aspx?Action=US|" & hdEnPtrID.Value + "&Popup=T&ReqID=" + hdEnRequestID.Value + "&LCode=" + hdEnLcode.Value, False)
                        End If
                    End If
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub ViewPtrInsertDetails()

        Dim objInputXml, objOutputXml As New XmlDocument
        'Dim objhdPtrView As New AAMS.bizETrackerHelpDesk.bzPTR
        Dim objhdPtrView As New AAMS.bizETrackerHelpDesk.bzPTR


        objOutputXml = objhdPtrView.ListPTRConfigValue()

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            Dim xNode As XmlNode
            For Each xNode In objOutputXml.DocumentElement.SelectNodes("CONFIGVALUE")
                If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_HD_PTR_TYPE" Then
                    drpType.SelectedValue = xNode.Attributes("FIELD_VALUE").Value.Trim()
                End If
                If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_HD_PTR_FOLLOWUP" Then
                    drpFollowup.SelectedValue = xNode.Attributes("FIELD_VALUE").Value.Trim()
                End If
                If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_HD_PTR_SEVERITY" Then
                    drpSeverity.SelectedValue = xNode.Attributes("FIELD_VALUE").Value.Trim()
                End If
                If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_HD_PTR_STATUS" Then
                    ' drpStatus.ClearSelection()
                    ' drpStatus.SelectedValue = xNode.Attributes("FIELD_VALUE").Value.Trim()
                End If
            Next
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If Request.QueryString("Action") IsNot Nothing Then
                If Request.QueryString("Action") = "I" Then
                    Response.Redirect("HDUP_Ptr.aspx?" + Request.QueryString.ToString)
                Else
                    Response.Redirect("HDUP_Ptr.aspx?Action=U|" + hdEnPtrID.Value.Trim())
                End If
            Else
                Response.Redirect("HDUP_Ptr.aspx?Action=U|" + hdEnPtrID.Value.Trim())
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    'Rakesh Start code
    Private Sub AgencyView()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency

        Try
            objInputXml.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerXml = hdLcode.Value ' Request.QueryString("LCode")
            objOutputXml = objbzAgency.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXml.DocumentElement.SelectSingleNode("Agency")
                    txtAgencyName.Text = .Attributes("NAME").Value()
                    txtAgencyAddress.Text = .Attributes("ADDRESS").Value()
                    txtCity.Text = .Attributes("CITY").Value()
                    txtCountry.Text = .Attributes("COUNTRY").Value()
                    txtPhone.Text = .Attributes("PHONE").Value()
                    txtFax.Text = .Attributes("FAX").Value()
                    txtOnlineStatus.Text = .Attributes("ONLINE_STATUS").Value()
                    txtOfficeId.Text = .Attributes("OFFICEID").Value()
                End With

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            objbzAgency = Nothing
        End Try
    End Sub
    'End code

    Protected Sub btnLtr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLtr.Click
        If Request.QueryString("Popup") Is Nothing Then
            Response.Redirect("HDUP_CallLog.aspx?Action=U&LCode=" + hdEnLcode.Value + "&HD_RE_ID=" + hdEnRequestID.Value)
        End If
    End Sub

    Private Sub CheckSecurity()
        Try

            '*********************Security Segment**************************************************
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ET PTR']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ET PTR']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                End If
                If strBuilder(2) = "0" Then
                    btnSave.Enabled = False
                End If

            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            '*********************End of Security Segment*****************************************************
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub

    Public Function ReturnPTR_statusValu() As String
        Dim call_status_valu As String = ""

        Dim objOutputXml As New XmlDocument
        '        Dim objbzCallStatus As New AAMS.bizETrackerHelpDesk.bzPTR
        Dim objbzCallStatus As New AAMS.bizETrackerHelpDesk.bzPTR



        objOutputXml = objbzCallStatus.ListPTRConfigValue()

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then


            Dim xNode As XmlNode

            For Each xNode In objOutputXml.DocumentElement.SelectNodes("CONFIGVALUE")
                If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_ETHD_PTR_STATUS" Then
                    call_status_valu = xNode.Attributes("FIELD_VALUE").Value.Trim()
                End If
            Next

        End If
        'CallStatus()
        Return call_status_valu
    End Function

    Private Function CallStatus() As ListItem
        '        Dim objHdStatis As New AAMS.bizETrackerHelpDesk.bzCallStatus
        Dim objHdStatis As New AAMS.bizETrackerHelpDesk.bzCallStatus

        Dim objOutputXml As XmlDocument
        Dim ds As New DataSet
        Dim lstSeleted As ListItem

        Dim str_HD_STATUS_ID As String = ReturnPTR_statusValu()

        objOutputXml = New XmlDocument
        Dim objInXml As New XmlDocument
        objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT>")
        objOutputXml = objHdStatis.List1(objInXml)
        Dim Node As XmlNode
        Dim nodeLists As XmlNodeList
        Dim outerXmlSet As New XmlDocument
        ' Dim strOuteXml As String = objOutputXml.ReplaceChild(.InnerXml
        nodeLists = objOutputXml.DocumentElement.SelectNodes("CALL_STATUS")
        For Each Node In nodeLists
            If Node.Attributes("HD_STATUS_ID").Value.Trim() = str_HD_STATUS_ID Then
                lstSeleted = New ListItem(Node.Attributes("HD_STATUS_NAME").Value.Trim(), Node.Attributes("HD_STATUS_ID").Value.Trim() + "|" + Node.Attributes("HD_STATUS_CLOSE").Value.Trim())
                Exit For
            End If
        Next

        Return lstSeleted
    End Function

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        If Request.QueryString("Popup") IsNot Nothing Then
            Response.Redirect("HDUP_PTR.aspx?Action=I&Popup=T&ReqID=" + objED.Encrypt(hdRequestID.Value) + "&LCode=" + objED.Encrypt(hdLcode.Value))
        Else
            Response.Redirect("HDUP_PTR.aspx?Action=I&ReqID=" + objED.Encrypt(hdRequestID.Value) + "&LCode=" + objED.Encrypt(hdLcode.Value))
        End If

    End Sub

    '@ Start of Code added By Pankaj on 13/04/2010 for Multi WorkOrder Navigation.
#Region "Code for Paging "

    Private Sub ShownextPrevious()
        Dim count As Integer = ddlPageNumber.Items.Count
        'Code for hiding prev and next button based on count
        If count = 1 Then
            lnkNext.Visible = False
            lnkPrev.Visible = False
        Else

            lnkPrev.Visible = True
            lnkNext.Visible = True
        End If

        'Code for hiding next button when pagenumber is equal to page count
        If ddlPageNumber.SelectedItem.Text = count.ToString Then
            lnkNext.Visible = False
        Else
            lnkNext.Visible = True
        End If

        'Code for hiding prev button when pagenumber is 1
        If ddlPageNumber.SelectedItem.Text = "1" Then
            lnkPrev.Visible = False
        Else
            lnkPrev.Visible = True
        End If
    End Sub

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedItem.Text <> "1" Then
                Dim PageId As String = (CInt(ddlPageNumber.SelectedItem.Text) - 1).ToString
                Dim li As ListItem
                li = ddlPageNumber.Items.FindByText(PageId)
                If li IsNot Nothing Then
                    ddlPageNumber.SelectedValue = li.Value
                End If
                hdPtrID.Value = ddlPageNumber.SelectedValue.Split("|").GetValue(0)
                ' hdHistoryOrderID.Value = ddlPageNumber.SelectedValue.Split("|").GetValue(0)
                ViewPtrDetails()
                ShownextPrevious()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedItem.Text <> (ddlPageNumber.Items.Count).ToString Then
                Dim PageId As String = (CInt(ddlPageNumber.SelectedItem.Text) + 1).ToString
                Dim li As ListItem
                li = ddlPageNumber.Items.FindByText(PageId)
                If li IsNot Nothing Then
                    ddlPageNumber.SelectedValue = li.Value
                End If
                hdPtrID.Value = ddlPageNumber.SelectedValue.Split("|").GetValue(0)
                ' hdHistoryOrderID.Value = ddlPageNumber.SelectedValue.Split("|").GetValue(0)
                ViewPtrDetails()
                ShownextPrevious()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            Dim PageId As String = (CInt(ddlPageNumber.SelectedItem.Text)).ToString
            Dim li As ListItem
            li = ddlPageNumber.Items.FindByText(PageId)
            If li IsNot Nothing Then
                ddlPageNumber.SelectedValue = li.Value
            End If
            hdPtrID.Value = ddlPageNumber.SelectedValue.Split("|").GetValue(0)
            ' hdHistoryOrderID.Value = ddlPageNumber.SelectedValue.Split("|").GetValue(0)
            ViewPtrDetails()
            ShownextPrevious()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub BindControlsForNavigation(ByVal TotalNo As String, ByVal strBdrId As String, ByVal dset As DataSet)
        pnlPaging.Visible = True
        Dim count As Integer = CInt(TotalNo)
        txtRecordCount.Text = TotalNo
        If count <> ddlPageNumber.Items.Count Then
            ddlPageNumber.Items.Clear()
            For i As Integer = 1 To dset.Tables("PTR").Rows.Count
                ddlPageNumber.Items.Insert(i - 1, New ListItem(i.ToString, dset.Tables("PTR").Rows(i - 1)("HD_PTR_ID").ToString + "|" + dset.Tables("PTR").Rows(i - 1)("HD_PTR_REF").ToString))
            Next
        End If
        Dim selectedValue As String = strBdrId
        Dim li As ListItem
        li = ddlPageNumber.Items.FindByValue(selectedValue)
        If li IsNot Nothing Then
            ddlPageNumber.SelectedValue = selectedValue
        End If
        ddlPageNumber.SelectedValue = selectedValue
        ShownextPrevious()
    End Sub

    Private Sub BindListForPTR(ByVal strPTR As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzRequestPTR As New AAMS.bizETrackerHelpDesk.bzPTR
        Dim objXmlReader As XmlNodeReader
        Dim ds As DataSet
        objInputXml.LoadXml("<HD_LISTPTR_INPUT><HD_RE_ID></HD_RE_ID></HD_LISTPTR_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = hdRequestID.Value
        objOutputXml = objbzRequestPTR.ListPTR(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds = New DataSet
            ds.ReadXml(objXmlReader)
            hdTotalNoOfRecords.Value = ds.Tables("PTR").Rows.Count
            If hdTotalNoOfRecords.Value > 0 Then
                BindControlsForNavigation(hdTotalNoOfRecords.Value, strPTR, ds)
            Else
                pnlPaging.Visible = True
            End If
        End If
    End Sub
#End Region
End Class
