
Partial Class BOHelpDesk_HDUP_WorkOrder
    Inherits System.Web.UI.Page
#Region "Global Variable Declarations."
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
#End Region

#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            ' Checking security.
            CheckSecurity()
            If Not IsPostBack Then

                drpAssigned.Attributes.Add("onChange", "return ClearAssigneeDate();")
                btnSave.Attributes.Add("onClick", "return ValidateForm();")
                objeAAMS.BindDropDown(drpOrderType, "BOWorkOrderType", True, 1)
                objeAAMS.BindDropDown(drpFollowUp, "BOWorkOrderFollowUp", True, 1)
                objeAAMS.BindDropDown(drpSeverity, "BOWorkOrderSeverity", True, 1)
                objeAAMS.BindDropDown(drpAssigned, "BOWorkOrderAssignee", True, 1)
                objeAAMS.BindDropDown(drpStatus, "BOHDPTRSTATUS", True, 1)
                ViewCallLogInsertDetails()

                ' Showing previous values.
                If Not Request.QueryString("OrderID") Is Nothing Then
                    ' hdOrderID.Value = objED.Decrypt(Request.QueryString("OrderID").ToString)
                    hdOrderID.Value = Request.QueryString("OrderID")
                    hdHistoryOrderID.Value = Request.QueryString("OrderID").ToString
                    ViewDetails()
                    'code added on 13 th APR
                    BindListForWO(hdOrderID.Value + "|" + txtOrderNo.Text)
                End If
                If Not Request.QueryString("Action") Is Nothing Then
                    If Request.QueryString("Action") = "U" Then
                        '    imgAgency.Visible = False
                        txtOrderNo.ReadOnly = True
                        txtOrderNo.CssClass = "textboxgrey"
                        btnHistory.Enabled = True
                        btnAssigneeHistory.Enabled = True
                        drpOrderType.Focus()
                    Else
                        txtOrderNo.Focus()
                        ' Code for new work order.
                        If Not Request.QueryString("ReqID") Is Nothing Then
                            hdEnReqID.Value = Request.QueryString("ReqID")
                            hdReqID.Value = objED.Decrypt(Request.QueryString("ReqID"))
                        End If
                        If Not Request.QueryString("LCode") Is Nothing Then
                            hdLCode.Value = objED.Decrypt(Request.QueryString("LCode"))
                            AgencyView()
                        End If
                        btnHistory.Enabled = False
                        btnAssigneeHistory.Enabled = False
                        '    imgAgency.Visible = True
                    End If
                End If
                If Not Request.QueryString("Msg") Is Nothing Then
                    lblError.Text = objeAAMSMessage.messInsert
                End If
            End If
            'Rakesh start code 
            If Request.QueryString("Popup") Is Nothing Then
                lnkClose.Visible = False
                btnLTR.Visible = True
            Else
                lnkClose.Visible = True
                btnLTR.Visible = False
            End If
            'End code

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click


        Dim objInputXml As New XmlDocument
        Dim objOutputXml As New XmlDocument
        Dim objWorkOrder As New AAMS.bizBOHelpDesk.bzWorkOrder
        Try
            ' Load Input Xml.
            objInputXml.LoadXml("<HD_UPDATEWORKORDER_INPUT><WORKORDER WO_ID='' WO_SEVERITY_NAME='' WO_FOLLOWUP_NAME='' WO_TYPE_NAME='' HD_STATUS_NAME='' HD_RE_ID='' WO_NUMBER='' WO_TITLE='' WO_SEVERITY_ID='' WO_TYPE_ID='' WO_FOLLOWUP_ID=''  STATUS='' WO_ASSIGNEE_ID=''  EMPLOYEEID='' /></HD_UPDATEWORKORDER_INPUT>")
            If Not Request.QueryString("OrderID") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_ID").Value = ddlPageNumber.SelectedValue.Split("|").GetValue(0) 'hdOrderID.Value 'Rakesh Request.QueryString("OrderID").ToString().Trim()
            Else
                objInputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_ID").Value = ""
            End If
            objInputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_SEVERITY_NAME").Value = drpSeverity.SelectedItem.Text
            objInputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_FOLLOWUP_NAME").Value = drpFollowUp.SelectedItem.Text
            objInputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_TYPE_NAME").Value = drpOrderType.SelectedItem.Text
            objInputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("HD_STATUS_NAME").Value = drpStatus.SelectedItem.Text

            objInputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_NUMBER").Value = txtOrderNo.Text
            objInputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_TITLE").Value = txtOrderTitle.Text
            objInputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_SEVERITY_ID").Value = drpSeverity.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_TYPE_ID").Value = drpOrderType.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_FOLLOWUP_ID").Value = drpFollowUp.SelectedValue

            objInputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("STATUS").Value = drpStatus.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_ASSIGNEE_ID").Value = drpAssigned.SelectedValue

            objInputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("EMPLOYEEID").Value = Session("LoginSession").ToString().Split("|")(0)
            objInputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("HD_RE_ID").Value = hdReqID.Value

            ' Calling update method for update.EMPLOYEEID
            objOutputXml = objWorkOrder.Update(objInputXml)
            '   Checking error status. 
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If Not Request.QueryString("Action") Is Nothing Then
                    txtOrderNo.ReadOnly = True
                    txtOrderNo.CssClass = "textboxgrey"

                    If Request.QueryString("Action") = "U" Then
                        lblError.Text = objeAAMSMessage.messUpdate ' Record updated successfully.
                        If Not Request.QueryString("ReqID") Is Nothing Then
                            Dim strWorkOrderNo As String
                            strWorkOrderNo = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_NUMBER").Value
                            hdRWONO.Value = strWorkOrderNo
                            '  ClientScript.RegisterClientScriptBlock(Me.GetType, "stWorkOrder", "<script>window.opener.document.forms['form1']['txtWorkOrderNo'].value='" + strWorkOrderNo + "';window.close();</script>")
                        End If
                    Else
                        lblError.Text = objeAAMSMessage.messInsert
                        If hdRWONO.Value <> "" Then
                            lblError.Text = objeAAMSMessage.messUpdate
                        End If
                        Dim strWorkOrderNo As String
                        strWorkOrderNo = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_NUMBER").Value
                        Dim strWorkOrderID As String
                        strWorkOrderID = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_ID").Value
                        hdRWONO.Value = strWorkOrderNo
                        hdRWOId.Value = strWorkOrderID
                        hdEnRWOId.Value = objED.Encrypt(strWorkOrderID)
                        hdOrderID.Value = strWorkOrderID
                        ' Redirect to same page
                        ' "HDUP_WorkOrder.aspx?Action=U&OrderID=" +OrderID
                        ' ClientScript.RegisterClientScriptBlock(Me.GetType, "stWorkOrder", "<script>window.opener.document.forms['form1']['txtWorkOrderNo'].value='" + strWorkOrderNo + "';window.opener.document.forms['form1']['hdWorkOrderNo'].value='" + strWorkOrderID + "';window.close();</script>")
                        ' Response.Redirect("HDUP_WorkOrder.aspx?Action=U&Popup=T&Msg=In&OrderID=" + objED.Encrypt(hdOrderID.Value))
                        Response.Redirect("BOHDUP_WorkOrder.aspx?Action=U&Popup=T&Msg=In&OrderID=" + hdOrderID.Value)
                    End If

                End If

                ' Calling View Method after update
                ViewDetails()

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            ' Calling View Method.
            If Not Request.QueryString("OrderID") Is Nothing Then
                ViewDetails()
            Else
                txtOrderNo.Text = ""
                txtOrderTitle.Text = ""
                drpAssigned.SelectedIndex = 0
                drpOrderType.SelectedIndex = 0
                drpFollowUp.SelectedIndex = 0
                drpSeverity.SelectedIndex = 0
                drpStatus.SelectedIndex = 0

            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "ViewDetails()"
    Private Sub ViewDetails()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objWorkOrder As New AAMS.bizBOHelpDesk.bzWorkOrder
        Dim strOpenDate, strAssignedDate, strCloseDate As String

        Try
            objInputXml.LoadXml("<HD_VIEWWORKORDER_INPUT><WO_ID /></HD_VIEWWORKORDER_INPUT>")

            If Request.QueryString("OrderID").ToString().Trim() = "" Then
                objInputXml.DocumentElement.SelectSingleNode("WO_ID").InnerText = hdOrderID.Value
            Else
                objInputXml.DocumentElement.SelectSingleNode("WO_ID").InnerText = hdOrderID.Value 'Request.QueryString("OrderID").ToString().Trim()
            End If


            objOutputXml = objWorkOrder.View(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                hdLCode.Value = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("LCODE").Value
                txtAgencyName.Text = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("AGENCYNAME").Value
                txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("ADDRESS").Value
                txtCity.Text = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("CITY").Value
                txtCountry.Text = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("COUNTRY").Value
                txtPhone.Text = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("PHONE").Value
                txtFax.Text = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("FAX").Value
                txtOfficeID.Text = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("OFFICEID").Value
                txtOnlineStatus.Text = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("ONLINESTATUS").Value
                txtOrderNo.Text = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_NUMBER").Value
                txtOrderTitle.Text = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_TITLE").Value
                drpOrderType.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_TYPE_ID").Value
                drpAssigned.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_ASSIGNEE_ID").Value
                hdAssignedTo.Value = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_ASSIGNEE_ID").Value
                drpFollowUp.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_FOLLOWUP_ID").Value
                drpSeverity.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_SEVERITY_ID").Value
                drpStatus.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("STATUS").Value
                hdAssignedTo.Value = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_ASSIGNEE_ID").Value
                ' txtCloseDate.Text = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_CLOSEDATE").Value
                'txtAssignedDate.Text = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("ASSIGNED_DATE").Value
                hdReqID.Value = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("HD_RE_ID").Value

                strOpenDate = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_OPENDATE").Value
                strAssignedDate = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("ASSIGNED_DATE").Value
                strCloseDate = objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("WO_CLOSEDATE").Value

                If strOpenDate <> "" Then
                    txtOpenDate.Text = strOpenDate.Substring(6, 2) + "/" + strOpenDate.Substring(4, 2) + "/" + strOpenDate.Substring(0, 4)
                End If
                If strCloseDate <> "" Then
                    txtCloseDate.Text = strCloseDate.Substring(6, 2) + "/" + strCloseDate.Substring(4, 2) + "/" + strCloseDate.Substring(0, 4)
                End If
                If strAssignedDate <> "" Then
                    txtAssignedDate.Text = strAssignedDate.Substring(6, 2) + "/" + strAssignedDate.Substring(4, 2) + "/" + strAssignedDate.Substring(0, 4)
                    hdAssignedDate.Value = strAssignedDate.Substring(6, 2) + "/" + strAssignedDate.Substring(4, 2) + "/" + strAssignedDate.Substring(0, 4)
                End If

                '   When status closed then disable all controls .
                If objOutputXml.DocumentElement.SelectSingleNode("WORKORDER").Attributes("HD_STATUS_CLOSE").Value = "1" Then
                    drpOrderType.Enabled = False
                    drpAssigned.Enabled = False
                    drpFollowUp.Enabled = False
                    drpSeverity.Enabled = False
                    drpStatus.Enabled = False
                    txtOrderTitle.ReadOnly = True
                    txtOrderTitle.CssClass = "textboxgrey"
                    btnSave.Enabled = False
                End If
                '   End

                hdRWONO.Value = txtOrderNo.Text
                'hdRWOId.Value = objED.Decrypt(Request.QueryString("OrderID").ToString().Trim())
                hdRWOId.Value = Request.QueryString("OrderID").ToString().Trim()
                hdEnRWOId.Value = Request.QueryString("OrderID").ToString().Trim()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
#End Region

#Region "AgencyView()"
    Private Sub AgencyView()
        'Rakesh Start code
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency

        Try
            objInputXml.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerXml = hdLCode.Value
            objOutputXml = objbzAgency.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXml.DocumentElement.SelectSingleNode("Agency")
                    txtAgencyName.Text = .Attributes("NAME").Value()
                    txtAddress.Text = .Attributes("ADDRESS").Value()
                    txtCity.Text = .Attributes("CITY").Value()
                    txtCountry.Text = .Attributes("COUNTRY").Value()
                    txtPhone.Text = .Attributes("PHONE").Value()
                    txtFax.Text = .Attributes("FAX").Value()
                    txtOnlineStatus.Text = .Attributes("ONLINE_STATUS").Value()
                    txtOfficeID.Text = .Attributes("OFFICEID").Value()
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
#End Region

#Region "CheckSecurity()::This method is used for security check."
    Private Sub CheckSecurity()
        Try
            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Work Order']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Work Order']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                    btnHistory.Enabled = False
                    btnAssigneeHistory.Enabled = False
                    btnLTR.Enabled = False
                    btnNew.Enabled = False
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnLTR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLTR.Click"
    Protected Sub btnLTR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLTR.Click
        If Request.QueryString("Popup") Is Nothing Then
            Response.Redirect("BOHDUP_CallLog.aspx?Action=U&LCode=" + objED.Encrypt(hdLCode.Value) + "&HD_RE_ID=" + objED.Encrypt(hdReqID.Value))
        End If
    End Sub
#End Region

#Region "ViewCallLogInsertDetails()"
    Private Sub ViewCallLogInsertDetails()

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objhdPtrView As New AAMS.bizBOHelpDesk.bzPTR
        objOutputXml = objhdPtrView.ListPTRConfigValue()

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            Dim xNode As XmlNode
            For Each xNode In objOutputXml.DocumentElement.SelectNodes("CONFIGVALUE")
                If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_HD_WO_FOLLOWUP" Then
                    drpFollowUp.SelectedValue = xNode.Attributes("FIELD_VALUE").Value.Trim()
                End If
                If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_HD_WO_SEVERITY" Then
                    drpSeverity.SelectedValue = xNode.Attributes("FIELD_VALUE").Value.Trim()
                End If
                If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_HD_WO_STATUS" Then
                    drpStatus.SelectedValue = xNode.Attributes("FIELD_VALUE").Value.Trim()
                End If
            Next
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
#End Region

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub


    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("BOHDUP_WorkOrder.aspx?Action=I&ReqID=" + objED.Encrypt(hdReqID.Value) + "&LCode=" + objED.Encrypt(hdLCode.Value))
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
                hdOrderID.Value = ddlPageNumber.SelectedValue.Split("|").GetValue(0)
                hdHistoryOrderID.Value = ddlPageNumber.SelectedValue.Split("|").GetValue(0)
                ViewDetails()
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
                hdOrderID.Value = ddlPageNumber.SelectedValue.Split("|").GetValue(0)
                hdHistoryOrderID.Value = ddlPageNumber.SelectedValue.Split("|").GetValue(0)
                ViewDetails()
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
            hdOrderID.Value = ddlPageNumber.SelectedValue.Split("|").GetValue(0)
            hdHistoryOrderID.Value = ddlPageNumber.SelectedValue.Split("|").GetValue(0)
            ViewDetails()
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
            For i As Integer = 1 To dset.Tables("WORKORDER").Rows.Count
                ddlPageNumber.Items.Insert(i - 1, New ListItem(i.ToString, dset.Tables("WORKORDER").Rows(i - 1)("WO_ID").ToString + "|" + dset.Tables("WORKORDER").Rows(i - 1)("WO_NUMBER").ToString))
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

    Private Sub BindListForWO(ByVal strWO As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzRequestWO As New AAMS.bizBOHelpDesk.bzWorkOrder
        Dim objXmlReader As XmlNodeReader
        Dim ds As DataSet
        objInputXml.LoadXml("<HD_LISTWORKORDER_INPUT><HD_RE_ID></HD_RE_ID></HD_LISTWORKORDER_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = hdReqID.Value
        objOutputXml = objbzRequestWO.ListWorkOrder(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds = New DataSet
            ds.ReadXml(objXmlReader)
            hdTotalNoOfRecords.Value = ds.Tables("WORKORDER").Rows.Count
            If hdTotalNoOfRecords.Value > 0 Then
                BindControlsForNavigation(hdTotalNoOfRecords.Value, strWO, ds)
            Else
                pnlPaging.Visible = True
            End If
        End If
    End Sub
#End Region

End Class
