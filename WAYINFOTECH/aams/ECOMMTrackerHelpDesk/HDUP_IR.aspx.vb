'################################################
'######## Developed By Abhishek #################
'################################################
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Partial Class ETHelpDesk_HDUP_IR
    Inherits System.Web.UI.Page

    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            lblError.Text = String.Empty
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            btnSave.Attributes.Add("onclick", "return IRValidate();")

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            If Request.QueryString("Popup") Is Nothing Then
                lnkClose.Visible = False
            Else
                lnkClose.Visible = True
                btnLtr.Visible = False
            End If


            If Not Page.IsPostBack Then
                BindDropDowns()
                'Code Modified on 27th December

                If Request.QueryString("Popup") IsNot Nothing Then
                    If Request.QueryString("Popup").Trim() = "T" Then
                        If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "I" Then
                            Dim strValu As String = ReturnIR_statusValu()
                            If strValu.Trim().Length = 0 Then
                                drpStatus.SelectedIndex = 0
                            Else
                                Dim lst As ListItem = CallStatus()
                                drpStatus.ClearSelection()
                                Dim lstNew As New ListItem
                                lstNew = drpStatus.Items.FindByValue(lst.Value)
                                If lstNew IsNot Nothing Then
                                    drpStatus.SelectedValue = lstNew.Value
                                End If
                            End If
                        End If
                    End If
                End If


                'End of Code Modification on 27th December


                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Then
                    hdIRID.Value = Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()
                    hdEnIRID.Value = Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()
                    ViewIRDetails()
                    ' imgAgency.Visible = False
                    BindListForIR(hdIRID.Value + "|" + txtIRNo.Text)
                Else
                    'ViewIRInsertDetails()
                End If
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                    hdIRID.Value = Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()
                    hdEnIRID.Value = Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()
                    ViewIRDetails()
                    BindListForIR(hdIRID.Value + "|" + txtIRNo.Text)
                    '  imgAgency.Visible = False
                    lblError.Text = objeAAMSMessage.messInsert
                    If Not Request.QueryString("Msg") Is Nothing Then
                        lblError.Text = objeAAMSMessage.messUpdate
                    End If
                Else
                    'ViewIRInsertDetails()
                End If

                'Rakesh Start code
                If Not Request.QueryString("ReqID") Is Nothing And Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() <> "U" Then
                    hdRequestID.Value = objED.Decrypt(Request.QueryString("ReqID").ToString)
                    hdEnRequestID.Value = Request.QueryString("ReqID").ToString
                    txtIRNo.ReadOnly = False
                    txtIRNo.CssClass = "textbox"

                End If
                If Not Request.QueryString("LCode") Is Nothing And Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() <> "U" Then
                    hdEnLcode.Value = Request.QueryString("LCode")
                    hdLcode.Value = objED.Decrypt(Request.QueryString("LCode"))
                    AgencyView()
                End If
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "I" Then
                    ViewIRInsertDetails()
                End If
                'End code
            End If

            CheckSecurity()
            If hdIRID.Value = "" Then
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

    Private Sub ViewIRDetails()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument

            '            Dim objhdIRView As New AAMS.bizETrackerHelpDesk.bzIR
            Dim objhdIRView As New AAMS.bizETrackerHelpDesk.bzIR

            objInputXml.LoadXml("<HD_VIEW_IR_INPUT><HD_IR_ID></HD_IR_ID></HD_VIEW_IR_INPUT>")
            'If Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString() IsNot Nothing Then
            '    objInputXml.DocumentElement.SelectSingleNode("HD_IR_ID").InnerText = hdIRID.Value 'Rakesh 1 Aug 08 Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()
            '    '  hdIRID.Value = Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()
            'End If
            objInputXml.DocumentElement.SelectSingleNode("HD_IR_ID").InnerText = hdIRID.Value
            objOutputXml = objhdIRView.View(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXml.DocumentElement.SelectSingleNode("IR")
                    txtAgencyName.Text = .Attributes("NAME").Value
                    hdLcode.Value = .Attributes("LCODE").Value
                    txtAgencyAddress.Text = .Attributes("ADDRESS").Value
                    txtCity.Text = .Attributes("CITY").Value
                    txtPhone.Text = .Attributes("PHONE").Value
                    'txtPriority.Text = .Attributes("PRIORITY").Value
                    txtFax.Text = .Attributes("FAX").Value
                    txtOnlineStatus.Text = .Attributes("ONLINE_STATUS").Value
                    txtOfficeID.Text = .Attributes("OFFICEID").Value
                    txtIRNo.Text = .Attributes("HD_IR_REF").Value

                    'drpType.ClearSelection()
                    'drpSeverity.ClearSelection()
                    'drpFollowup.ClearSelection()
                    drpStatus.ClearSelection()
                    drpAssignedTo.ClearSelection()


                    Dim lstType As New ListItem
                    lstType = drpType.Items.FindByValue(.Attributes("HD_IR_TYPE_ID").Value)
                    If lstType IsNot Nothing Then
                        drpType.SelectedValue = .Attributes("HD_IR_TYPE_ID").Value
                    End If
                    Dim lstSev As New ListItem
                    lstSev = drpSeverity.Items.FindByValue(.Attributes("HD_IR_SEV_ID").Value)
                    If lstSev IsNot Nothing Then
                        drpSeverity.SelectedValue = .Attributes("HD_IR_SEV_ID").Value
                    End If
                    Dim lstFollowup As New ListItem
                    lstFollowup = drpFollowup.Items.FindByValue(.Attributes("HD_IR_FOLLOWUP_ID").Value)
                    If lstFollowup IsNot Nothing Then
                        drpFollowup.SelectedValue = .Attributes("HD_IR_FOLLOWUP_ID").Value
                    End If


                    Dim lst As New ListItem

                    lst = drpStatus.Items.FindByText(.Attributes("HD_STATUS_NAME").Value)
                    If lst IsNot Nothing Then
                        drpStatus.SelectedValue = lst.Value
                    End If
                    '.Selected = True

                    If .Attributes("HD_IR_CLOSEDATE").Value.Trim().Length <> 0 Then
                        txtCloseDt.Text = objeAAMS.ConvertDateBlank(.Attributes("HD_IR_CLOSEDATE").Value.Trim())
                    Else
                        txtCloseDt.Text = ""
                    End If

                    If .Attributes("HD_IR_OPENDATE").Value.Trim().Length <> 0 Then
                        txtOpenDt.Text = objeAAMS.ConvertDateBlank(.Attributes("HD_IR_OPENDATE").Value.Trim())
                    Else
                        txtOpenDt.Text = ""
                    End If

                    If .Attributes("ASSIGNED_DATE").Value.Trim().Length <> 0 Then
                        txtAssignedDt.Text = objeAAMS.ConvertDateBlank(.Attributes("ASSIGNED_DATE").Value.Trim())
                    Else
                        txtAssignedDt.Text = ""
                    End If

                    Dim lstAssign As New ListItem
                    lstAssign = drpAssignedTo.Items.FindByValue(.Attributes("ASSIGNEEID").Value)
                    If lstAssign IsNot Nothing Then
                        drpAssignedTo.SelectedValue = .Attributes("ASSIGNEEID").Value
                    End If

                    hdAssigneeID.Value = .Attributes("ASSIGNEEID").Value

                    txtIRTitle.Text = .Attributes("HD_IR_TITLE").Value
                    txtCountry.Text = .Attributes("COUNTRY").Value
                    hdEnRequestID.Value = objED.Encrypt(.Attributes("HD_RE_ID").Value)
                    hdRequestID.Value = .Attributes("HD_RE_ID").Value
                    hdRIRNO.Value = .Attributes("HD_IR_REF").Value
                    hdRIRId.Value = hdIRID.Value
                    '   hdEnRIRId.Value = objED.Encrypt(hdIRID.Value)
                    hdEnRIRId.Value = hdIRID.Value
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
            objeAAMS.BindDropDown(drpFollowup, "ET_HDIRFOLLOWUP", True)
            objeAAMS.BindDropDown(drpType, "ET_HDIRTYPE", True)
            objeAAMS.BindDropDown(drpSeverity, "ET_HDIRSEVERITY", True)
            objeAAMS.BindDropDown(drpStatus, "ET_HDIRSTATUSFORMANAGE", False)
            objeAAMS.BindDropDown(drpAssignedTo, "ET_HDASSIGNEDTO", True)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If (Not Request.QueryString("Action") = Nothing) Then
                Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                '                Dim objhdIRSave As New AAMS.bizETrackerHelpDesk.bzIR
                Dim objhdIRSave As New AAMS.bizETrackerHelpDesk.bzIR

                'objInputXml.LoadXml("<HD_UPDATE_IR_INPUT><IR HD_IR_ID='' HD_RE_ID='' HD_IR_REF='' HD_STATUS_ID='' HD_IR_SEV_ID='' HD_IR_FOLLOWUP_ID='' HD_IR_TYPE_ID='' HD_IR_OPENDATE='' HD_IR_CLOSEDATE='' HD_IR_TITLE='' ASSIGNEEID='' ASSIGNED_DATE='' HD_IR_TYPE_NAME ='' HD_IR_SEV_NAME='' HD_IR_FOLLOWUP_NAME='' HD_STATUS_NAME='' ASSIGNEE_NAME='' EMPLOYEEID='' /></HD_UPDATE_IR_INPUT>")
                objInputXml.LoadXml("<HD_UPDATE_IR_INPUT><IR HD_IR_ID='' HD_RE_ID='' HD_IR_REF='' HD_STATUS_ID='' HD_IR_SEV_ID='' HD_IR_FOLLOWUP_ID='' HD_IR_TYPE_ID='' HD_STATUS_CLOSE='' HD_IR_TITLE='' ASSIGNEEID='' HD_IR_TYPE_NAME='' HD_IR_SEV_NAME='' HD_IR_FOLLOWUP_NAME='' HD_STATUS_NAME='' ASSIGNEE_NAME=''  EMPLOYEEID='' /></HD_UPDATE_IR_INPUT>")
                'Rakesh start code
                If hdRIRId.Value <> "" Then
                    objInputXml.DocumentElement.SelectSingleNode("IR").Attributes("HD_IR_ID").Value = hdRIRId.Value
                End If
                'End code
                If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
                    objInputXml.DocumentElement.SelectSingleNode("IR").Attributes("HD_IR_ID").Value = hdRIRId.Value ' Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()
                End If
                With objInputXml.DocumentElement.SelectSingleNode("IR")
                    .Attributes("HD_RE_ID").Value = hdRequestID.Value
                    .Attributes("HD_IR_REF").Value = txtIRNo.Text.Trim()

                    Dim StrIRStatus As String = drpStatus.SelectedValue.Trim()
                    Dim strStatus() As String = StrIRStatus.Split("|")

                    .Attributes("HD_STATUS_ID").Value = strStatus(0).Trim() '
                    .Attributes("HD_IR_SEV_ID").Value = drpSeverity.SelectedValue.Trim()
                    .Attributes("HD_IR_FOLLOWUP_ID").Value = drpFollowup.SelectedValue.Trim()
                    .Attributes("HD_IR_TYPE_ID").Value = drpType.SelectedValue.Trim()
                    'HD_STATUS_CLOSE
                    .Attributes("HD_STATUS_CLOSE").Value = strStatus(1).Trim()  'Need for Change drpType.SelectedValue.Trim()

                    '.Attributes("HD_IR_OPENDATE").Value = objeAAMS.ConvertTextDateBlank(txtOpenDt.Text.Trim())
                    '.Attributes("HD_IR_CLOSEDATE").Value = objeAAMS.ConvertTextDateBlank(txtCloseDt.Text.Trim())
                    .Attributes("HD_IR_TITLE").Value = txtIRTitle.Text.Trim()
                    .Attributes("ASSIGNEEID").Value = drpAssignedTo.SelectedValue
                    '.Attributes("ASSIGNED_DATE").Value = objeAAMS.ConvertTextDateBlank(txtAssignedDt.Text.Trim())

                    'HD_IR_TYPE_NAME ='' HD_IR_SEV_NAME='' HD_IR_FOLLOWUP_NAME='' HD_STATUS_NAME='' ASSIGNEE_NAME='' EMPLOYEEID=''
                    .Attributes("HD_IR_TYPE_NAME").Value = drpType.Items(drpType.SelectedIndex).Text 'drpType.Text
                    .Attributes("HD_IR_SEV_NAME").Value = drpSeverity.Items(drpSeverity.SelectedIndex).Text   'drpSeverity.Text
                    .Attributes("HD_IR_FOLLOWUP_NAME").Value = drpFollowup.Items(drpFollowup.SelectedIndex).Text  'drpFollowup.Text
                    .Attributes("HD_STATUS_NAME").Value = drpStatus.Items(drpStatus.SelectedIndex).Text  'drpStatus.Text
                    If drpAssignedTo.SelectedValue <> "" Then
                        .Attributes("ASSIGNEE_NAME").Value = drpAssignedTo.SelectedItem.Text
                    End If
                    'drpAssignedTo.Text
                    'Following Statement is Written for 1A Employee ID
                    Dim str As String()
                    str = Session("LoginSession").ToString().Split("|")
                    .Attributes("EMPLOYEEID").Value = str(0)
                End With

                objOutputXml = objhdIRSave.Update(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
                        If Not Request.QueryString("ReqID") Is Nothing Then
                            Dim strIRNo As String
                            strIRNo = objOutputXml.DocumentElement.SelectSingleNode("IR").Attributes("HD_IR_REF").Value
                            hdRIRNO.Value = strIRNo
                            ViewIRDetails()
                            lblError.Text = objeAAMSMessage.messUpdate
                        Else
                            If Request.QueryString("Popup") Is Nothing Then

                                Response.Redirect("HDUP_IR.aspx?Action=US|" & objOutputXml.DocumentElement.SelectSingleNode("IR").Attributes("HD_IR_ID").Value + "&Msg=U", False)
                            Else

                                Response.Redirect("HDUP_IR.aspx?Action=US|" & objOutputXml.DocumentElement.SelectSingleNode("IR").Attributes("HD_IR_ID").Value + "&Popup=T", False)
                            End If

                            lblError.Text = objeAAMSMessage.messUpdate
                        End If
                    Else

                        hdIRID.Value = objOutputXml.DocumentElement.SelectSingleNode("IR").Attributes("HD_IR_ID").Value
                        hdEnIRID.Value = hdIRID.Value
                        lblError.Text = objeAAMSMessage.messInsert
                        'Rakesh Start code
                        If hdRIRId.Value <> "" Then
                            lblError.Text = objeAAMSMessage.messUpdate
                        End If
                        'End code

                        If Not Request.QueryString("ReqID") Is Nothing Then
                            Dim strIRNo As String
                            strIRNo = objOutputXml.DocumentElement.SelectSingleNode("IR").Attributes("HD_IR_REF").Value
                            hdRIRNO.Value = strIRNo
                            hdRIRId.Value = objOutputXml.DocumentElement.SelectSingleNode("IR").Attributes("HD_IR_ID").Value
                            hdEnRIRId.Value = objOutputXml.DocumentElement.SelectSingleNode("IR").Attributes("HD_IR_ID").Value
                        End If
                        If Request.QueryString("Popup") Is Nothing Then
                            Response.Redirect("HDUP_IR.aspx?Action=US|" & hdEnIRID.Value + "&LCode=" + hdEnLcode.Value, False)
                        Else
                            Response.Redirect("HDUP_IR.aspx?Action=US|" & hdEnIRID.Value + "&Popup=T&ReqID=" + hdEnRequestID.Value + "&LCode=" + hdEnLcode.Value, False)
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
    Private Sub ViewIRInsertDetails()

        Dim objInputXml, objOutputXml As New XmlDocument
        '        Dim objhdIRView As New AAMS.bizETrackerHelpDesk.bzIR
        Dim objhdIRView As New AAMS.bizETrackerHelpDesk.bzIR

        objOutputXml = objhdIRView.ListIRConfigValue()

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            Dim xNode As XmlNode
            For Each xNode In objOutputXml.DocumentElement.SelectNodes("CONFIGVALUE")
                If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_HD_IR_TYPE" Then
                    drpType.SelectedValue = xNode.Attributes("FIELD_VALUE").Value.Trim()
                End If
                If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_HD_IR_FOLLOWUP" Then
                    drpFollowup.SelectedValue = xNode.Attributes("FIELD_VALUE").Value.Trim()
                End If
                If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_HD_IR_SEVERITY" Then
                    drpSeverity.SelectedValue = xNode.Attributes("FIELD_VALUE").Value.Trim()
                End If
                If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_HD_IR_STATUS" Then
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
                    Response.Redirect("HDUP_IR.aspx?" + Request.QueryString.ToString)
                    'If Request.QueryString("Popup") IsNot Nothing Then
                    '    Response.Redirect("HDUP_IR.aspx?Action=I&Popup=T&ReqID=" + objED.Encrypt(hdRequestID.Value) + "&LCode=" + objED.Encrypt(hdLcode.Value))
                    'Else
                    '    Response.Redirect("HDUP_IR.aspx?Action=I&ReqID=" + objED.Encrypt(hdRequestID.Value) + "&LCode=" + objED.Encrypt(hdLcode.Value))
                    'End If
                Else
                    Response.Redirect("HDUP_IR.aspx?Action=U|" + hdEnIRID.Value.Trim())
                End If
            Else

                Response.Redirect("HDUP_IR.aspx?Action=U|" + hdEnIRID.Value.Trim())
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ET IR']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ET IR']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                End If

                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                End If
                If strBuilder(2) = "0" Then
                    If hdRIRId.Value <> "" Then
                        btnSave.Enabled = False
                    Else
                        If strBuilder(1) <> "0" Then
                            btnSave.Enabled = True
                        End If
                    End If
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

    Public Function ReturnIR_statusValu() As String
        Dim call_status_valu As String = ""

        Dim objOutputXml As New XmlDocument
        '        Dim objbzCallStatus As New AAMS.bizETrackerHelpDesk.bzIR
        Dim objbzCallStatus As New AAMS.bizETrackerHelpDesk.bzIR



        objOutputXml = objbzCallStatus.ListIRConfigValue()

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then


            Dim xNode As XmlNode

            For Each xNode In objOutputXml.DocumentElement.SelectNodes("CONFIGVALUE")
                If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_HD_IR_STATUS" Then
                    call_status_valu = xNode.Attributes("FIELD_VALUE").Value.Trim()
                End If
            Next

        End If
        'CallStatus()
        Return call_status_valu
    End Function

    Private Function CallStatus() As ListItem
        'Dim objHdStatis As New AAMS.bizETrackerHelpDesk.bzCallStatus
        Dim objHdStatis As New AAMS.bizETrackerHelpDesk.bzCallStatus

        Dim objOutputXml As XmlDocument
        Dim ds As New DataSet
        Dim lstSeleted As ListItem

        Dim str_HD_STATUS_ID As String = ReturnIR_statusValu()

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
            Response.Redirect("HDUP_IR.aspx?Action=I&Popup=T&ReqID=" + objED.Encrypt(hdRequestID.Value) + "&LCode=" + objED.Encrypt(hdLcode.Value))
        Else
            Response.Redirect("HDUP_IR.aspx?Action=I&ReqID=" + objED.Encrypt(hdRequestID.Value) + "&LCode=" + objED.Encrypt(hdLcode.Value))
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
                hdIRID.Value = ddlPageNumber.SelectedValue.Split("|").GetValue(0)
                ' hdHistoryOrderID.Value = ddlPageNumber.SelectedValue.Split("|").GetValue(0)
                ViewIRDetails()
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
                hdIRID.Value = ddlPageNumber.SelectedValue.Split("|").GetValue(0)
                ' hdHistoryOrderID.Value = ddlPageNumber.SelectedValue.Split("|").GetValue(0)
                ViewIRDetails()
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
            hdIRID.Value = ddlPageNumber.SelectedValue.Split("|").GetValue(0)
            ' hdHistoryOrderID.Value = ddlPageNumber.SelectedValue.Split("|").GetValue(0)
            ViewIRDetails()
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
            For i As Integer = 1 To dset.Tables("IR").Rows.Count
                ddlPageNumber.Items.Insert(i - 1, New ListItem(i.ToString, dset.Tables("IR").Rows(i - 1)("HD_IR_ID").ToString + "|" + dset.Tables("IR").Rows(i - 1)("HD_IR_REF").ToString))
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

    Private Sub BindListForIR(ByVal strIR As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzRequestIR As New AAMS.bizETrackerHelpDesk.bzIR
        Dim objXmlReader As XmlNodeReader
        Dim ds As DataSet
        objInputXml.LoadXml("<HD_LISTIR_INPUT><HD_RE_ID></HD_RE_ID></HD_LISTIR_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = hdRequestID.Value
        objOutputXml = objbzRequestIR.ListIR(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds = New DataSet
            ds.ReadXml(objXmlReader)
            hdTotalNoOfRecords.Value = ds.Tables("IR").Rows.Count
            If hdTotalNoOfRecords.Value > 0 Then
                BindControlsForNavigation(hdTotalNoOfRecords.Value, strIR, ds)
            Else
                pnlPaging.Visible = True
            End If
        End If
    End Sub
#End Region
End Class

