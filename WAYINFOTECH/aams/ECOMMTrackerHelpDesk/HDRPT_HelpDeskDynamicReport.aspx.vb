'########################################################################
'############   Page Name -- HDRPT_HelpDeskDynamicReport.aspx     #######
'############   Date 13-March 2007  #####################################
'############   Developed By Abhishek  ##################################
'########################################################################
Partial Class ETHelpDesk_HDRPT_HelpDeskDynamicReport
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        Dim objNode As XmlNode
        Try
            btnResultConfig.Attributes.Add("onclick", "return ResultConfigure();")
            btnSearchConfigure.Attributes.Add("onclick", "return SearchConfigure();")
            txtAgencyName.Attributes.Add("onkeydown", "return ActDeAct();")
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If
            If (Request.QueryString("Reload") IsNot Nothing) Then
                Response.Redirect("HDRPT_HelpDeskDynamicReport.aspx", False)
                Exit Sub
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ET Dynamic Report']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ET Dynamic Report']").Attributes("Value").Value)

                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnSearch.Enabled = False
                        btnResultConfig.Enabled = False
                        btnSearchConfigure.Enabled = False
                        BtnRequestForReport.Enabled = False

                    End If
                    'If strBuilder(1) = "0" Then
                    '    btnSave.Enabled = False
                    'End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            hdToTime.Value = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm")
            hdFromTime.Value = System.DateTime.Now.Subtract(New TimeSpan(6, 0, 0)).ToString("dd/MM/yyyy HH:mm")
            If chkDisplayLastCall.Checked = False Then
                If Request.Form("txtQueryOpenedDateFrom") = "" Then
                    txtQueryOpenedDateFrom.Text = hdFromTime.Value
                    txtQueryOpenedDateTo.Text = hdToTime.Value
                Else
                    txtQueryOpenedDateFrom.Text = Request.Form("txtQueryOpenedDateFrom")
                    txtQueryOpenedDateTo.Text = Request.Form("txtQueryOpenedDateTo")
                End If
            Else
                txtQueryOpenedDateFrom.Text = ""
                txtQueryOpenedDateTo.Text = ""
            End If
            txtCloseDateFrom.Text = Request.Form("txtCloseDateFrom")
            txtCloseDateTo.Text = Request.Form("txtCloseDateTo")
            txtDateAssigned.Text = Request.Form("txtDateAssigned")
            If (Not IsPostBack) Then
                DivLoggedBy.Visible = False
                DivCustCateg.Visible = False
                Div1AOffice.Visible = False
                DivAgency1AOffice.Visible = False
                DivCord1.Visible = False
                DivCord2.Visible = False
                DivDateAssign.Visible = False
                DivDispLastCall.Visible = False
                DivDispos.Visible = False
                DivLoggedBy.Visible = False
                DivQueryCateg.Visible = False
                DivOfficeId.Visible = False
                DivQueryGroup.Visible = False
                'DivQueryPriority.Visible = False
                DivQuerySubCategory.Visible = False
                DivQuerySubGroup.Visible = False
                DivWorkOrderNo.Visible = False
                DivCallName.Visible = False
                DivAddress.Visible = False
                DivassigneTo.Visible = False
                DivSevirity.Visible = False
                ' DivTitle.Visible = False
                ' DivType.Visible = False
                DivFollowUp.Visible = False
                DivQueryStatus.Visible = False
                DivConType.Visible = False
                DivQueryCategTitle.Visible = False
                Div2.Visible = False
                DivAgencyStatus.Visible = False
                DivAgencyType.Visible = False
                DivHD_IR_REF.Visible = False
                'btnSearch.Attributes.Add("onClick", "return ValidateForm();")
                btnSearch.Attributes.Add("onClick", "return CkeckValidateForm();")
                BtnRequestForReport.Attributes.Add("onClick", "return CkeckValidateForm();")

                objeAAMS.BindDropDown(ddlCustomerCategory, "ET_CUSTOMERCATEGORY", True, 3)
                objeAAMS.BindDropDown(ddlAOffice, "AOFFICE", True, 3)
                objeAAMS.BindDropDown(ddlAgencyAOffice, "AOFFICE", True, 3)
                objeAAMS.BindDropDown(ddlDisposition, "ET_DISPOSITION", True, 3)
                objeAAMS.BindDropDown(ddlCoordinator1, "ET_COORDINATOR1", True, 3)
                objeAAMS.BindDropDown(ddlCoordinator2, "ET_COORDINATOR2", True, 3)
                objeAAMS.BindDropDown(drpFollowup, "ET_WorkOrderFollowUp", True, 3)
                objeAAMS.BindDropDown(ddlContactType, "ET_CONTACTTYPE", True, 3)
                objeAAMS.BindDropDown(DlstAgencyType, "AGENCYTYPE", True, 3)
                objeAAMS.BindDropDown(dlstAgencyStatus, "AGENCYSTATUS", True, 3)
                objeAAMS.BindDropDown(DlstCompVertical, "CompanyVertical", True, 3)

                loadAssignedTo()
                loadSubGroup()
                loadCategory()
                loadSubCategory()
                loadPriority()
                loadQueryStatus()
                GetSetId()
                If Session("LoginSession") IsNot Nothing Then
                    Dim objInputXml, objOutputXml As New XmlDocument
                    'Dim objXmlReader As XmlNodeReader
                    Dim ds As New DataSet
                    '                    Dim objbzDMReportConfiguration As New AAMS.bizETrackerHelpDesk.bzDMReportConfiguration
                    Dim objbzDMReportConfiguration As New AAMS.bizETrackerHelpDesk.bzDMReportConfiguration


                    objInputXml.LoadXml("<HD_GETDMSEARCHCONFIGURE_INPUT><EmployeeID /><SetID/></HD_GETDMSEARCHCONFIGURE_INPUT>")
                    objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0).ToString

                    If drpSelectSet.Items.Count = 0 Then
                        objInputXml.DocumentElement.SelectSingleNode("SetID").InnerText = "" ' drpSelectSet.SelectedValue
                    Else
                        If drpSelectSet.SelectedValue = "" Then
                            objInputXml.DocumentElement.SelectSingleNode("SetID").InnerText = "" ' drpSelectSet.SelectedValue
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("SetID").InnerText = "1" ' drpSelectSet.SelectedValue
                        End If
                    End If
                    ''Here Back end Method Call
                    objOutputXml = objbzDMReportConfiguration.getConfiguredSearchField(objInputXml)
                    'objOutputXml.LoadXml("<HD_VIEWDMSFIELDS_OUTPUT> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='LTR_NO' DM_FILED_NAME='LTR No' DM_SEARCH_FIELD='True' DM_RESULT_FILED='False' />" & _
                    '"<FIELDDETAILS DM_FIELD_ID='AGENCY' DM_FILED_NAME='Agency' DM_SEARCH_FIELD='True' DM_RESULT_FILED='False' />" & _
                    '"<FIELDDETAILS DM_FIELD_ID='PTR_NO' DM_FILED_NAME='PTR Number' DM_SEARCH_FIELD='True' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='CLOSE_DATE_FROM' DM_FILED_NAME='Close Date From' DM_SEARCH_FIELD='True' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='CLOSE_DATE_TO' DM_FILED_NAME='Close Date To' DM_SEARCH_FIELD='True' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='OPEN_DATE_FROM' DM_FILED_NAME='Open Date From' DM_SEARCH_FIELD='True' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='OPEN_DATE_TO' DM_FILED_NAME='Open Date To' DM_SEARCH_FIELD='True' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='ADDRESS' DM_FILED_NAME='Address' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='TITLE' DM_FILED_NAME='Title' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='QUERY_STATUS' DM_FILED_NAME='Query Status' DM_SEARCH_FIELD='True' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='FOLLOWUP' DM_FILED_NAME='Follow up' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='SEVERITY' DM_FILED_NAME='Severity' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='TYPE' DM_FILED_NAME='Type' DM_SEARCH_FIELD='True' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='LOGGED_BY' DM_FILED_NAME='Logged By' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='ASSIGNED_TO' DM_FILED_NAME='Assigned To' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='ASSIGNED_DATE_TIME' DM_FILED_NAME='Assigned Date Time' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='OFFICE_ID' DM_FILED_NAME='Office ID' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='CUSTOMER_CATEGORY' DM_FILED_NAME='Customer Category' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' />  " & _
                    '"<FIELDDETAILS DM_FIELD_ID='CALLER_NAME' DM_FILED_NAME='Caller Name' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='QUERY_GROUP' DM_FILED_NAME='Query Group' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='QUERY_SUB_GROUP' DM_FILED_NAME='Query Sub Group' DM_SEARCH_FIELD='True' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='QUERY_CATEGORY' DM_FILED_NAME='Query Category' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='QUERY_SUB_CATEGORY' DM_FILED_NAME='Query Sub Category' DM_SEARCH_FIELD='True' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='QUERY_PRIORITY' DM_FILED_NAME='Query Priority' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='COORDINATOR1' DM_FILED_NAME='Coordinator1' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='COORDINATOR2' DM_FILED_NAME='Coordinator2' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='DISPOSITION' DM_FILED_NAME='Disposition' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='1AOFFICE' DM_FILED_NAME='1A Office' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='AGENCY_1A_OFFICE' DM_FILED_NAME='Agency 1A Office' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                    '"<FIELDDETAILS DM_FIELD_ID='WORK_ORDER_NO' DM_FILED_NAME='Work Order No' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' />" & _
                    '"<FIELDDETAILS DM_FIELD_ID='LAST_CALL' DM_FILED_NAME='Last Call' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                    ' "<Errors Status='FALSE'> " & _
                    '  "<Error Code='' Description='' /> " & _
                    '  "</Errors>" & _
                    '  "</HD_VIEWDMSFIELDS_OUTPUT>")
                    ' @ Show Panel 
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='ADDRESS']")
                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivAddress.Visible = True
                            End If
                        End If
                        'objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='TITLE']")
                        'If objNode.Attributes("SELECTED").Value = "True" Then
                        '    DivTitle.Visible = True
                        'End If 
                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='QUERY_STATUS']")
                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivQueryStatus.Visible = True
                            End If
                        End If

                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='FOLLOWUP']")
                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivFollowUp.Visible = True
                            End If
                        End If

                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='SEVERITY']")
                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivSevirity.Visible = True
                            End If
                        End If

                        'objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='TYPE']")
                        'If objNode.Attributes("DM_SEARCH_FIELD").Value = "True" Then
                        '    DivType.Visible = True
                        'End If

                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='LOGGED_BY']")
                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivLoggedBy.Visible = True
                            End If
                        End If

                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='ASSIGNED_TO']")
                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivassigneTo.Visible = True
                            End If
                        End If

                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='ASSIGNED_DATE_TIME']")
                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivDateAssign.Visible = True
                            End If
                        End If

                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='OFFICE_ID']")
                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivOfficeId.Visible = True
                            End If
                        End If

                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='CUSTOMER_CATEGORY']")
                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivCustCateg.Visible = True
                            End If
                        End If

                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='CALLER_NAME']")

                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivCallName.Visible = True
                            End If
                        End If

                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='QUERY_GROUP']")
                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivQueryGroup.Visible = True
                            End If
                        End If

                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='QUERY_SUB_GROUP']")

                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivQuerySubGroup.Visible = True
                            End If
                        End If

                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='QUERY_CATEGORY']")
                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivQueryCateg.Visible = True
                            End If
                        End If

                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='QUERY_SUB_CATEGORY']")
                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivQuerySubCategory.Visible = True
                            End If
                        End If

                        'objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='QUERY_PRIORITY']")
                        'If objNode.Attributes("SELECTED").Value = "True" Then
                        '    DivQueryPriority.Visible = True
                        'End If

                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='COORDINATOR1']")

                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivCord1.Visible = True
                            End If
                        End If

                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='COORDINATOR2']")

                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivCord2.Visible = True
                            End If
                        End If

                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='DISPOSITION']")

                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivDispos.Visible = True
                            End If
                        End If

                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='1AOFFICE']")

                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                Div1AOffice.Visible = True
                            End If
                        End If

                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='AGENCY_1A_OFFICE']")

                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivAgency1AOffice.Visible = True
                            End If
                        End If

                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='WORK_ORDER_NO']")

                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivWorkOrderNo.Visible = True
                            End If
                        End If

                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='LAST_CALL']")
                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivDispLastCall.Visible = True
                            End If

                        End If

                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='CONTACTTYPE']")
                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivConType.Visible = True
                            End If
                        End If

                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='TITLE']")
                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivQueryCategTitle.Visible = True
                                Div2.Visible = True
                            End If
                        End If

                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='ASTATUS']")
                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivAgencyStatus.Visible = True
                            End If
                        End If

                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='ATYPE']")
                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivAgencyType.Visible = True
                            End If
                        End If

                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='HD_IR_REF']")
                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                DivHD_IR_REF.Visible = True
                            End If
                        End If

                        setDefaultValueBySetId(objOutputXml)

                    End If
                End If
                'LoadResultConfigure(Session("LoginSession").ToString().Split("|")(0).ToString)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            If drpSelectSet.SelectedValue = "" Then
                lblError.Text = "First Select any defined Set"
                Exit Sub
            End If

            SearchRecords()
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            If Request("txtQueryOpenedDateFrom") IsNot Nothing Then
                txtQueryOpenedDateFrom.Text = Request("txtQueryOpenedDateFrom")
            End If
            If Request("txtQueryOpenedDateTo") IsNot Nothing Then
                txtQueryOpenedDateTo.Text = Request("txtQueryOpenedDateTo")
            End If

            If Request("txtCloseDateFrom") IsNot Nothing Then
                txtCloseDateFrom.Text = Request("txtCloseDateFrom")
            End If
            If Request("txtCloseDateTo") IsNot Nothing Then
                txtCloseDateTo.Text = Request("txtCloseDateTo")
            End If

        End Try
    End Sub
    Sub loadSubGroup()
        'Code to fill Query Sub Group
        '        Dim objCallSubGroup As New AAMS.bizETrackerHelpDesk.bzCallSubGroup
        Dim objCallSubGroup As New AAMS.bizETrackerHelpDesk.bzCallSubGroup

        Dim objOutputXml As XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        objOutputXml = New XmlDocument
        Dim objInXml As New XmlDocument

        objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID /></HD_LISTCALLSUBGROUP_INPUT> ")
        objInXml.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = ddlQueryGroup.SelectedValue
        objOutputXml = objCallSubGroup.List1(objInXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)

            ddlQuerySubGroup.DataSource = Nothing
            ddlQuerySubGroup.DataBind()

            ddlQuerySubGroup.DataSource = ds.Tables("CALL_SUB_GROUP")
            ddlQuerySubGroup.DataTextField = "CALL_SUB_GROUP_NAME"
            ddlQuerySubGroup.DataValueField = "CALL_SUB_GROUP_ID"
            ddlQuerySubGroup.DataBind()
            ddlQuerySubGroup.Items.Insert(0, New ListItem("--All--", ""))
        Else
            ddlQuerySubGroup.Items.Clear()
            ddlQuerySubGroup.Items.Insert(0, New ListItem("--All--", ""))
        End If

    End Sub

    Sub loadQueryStatus()
        'Code to fill Query Status
        '        Dim objCallStatus As New AAMS.bizETrackerHelpDesk.bzCallStatus
        Dim objCallStatus As New AAMS.bizETrackerHelpDesk.bzCallStatus

        Dim objOutputXml1 As XmlDocument
        Dim objXmlReader1 As XmlNodeReader
        Dim ds1 As New DataSet
        objOutputXml1 = New XmlDocument
        Dim objInXml1 As New XmlDocument
        objOutputXml1 = New XmlDocument
        objInXml1.LoadXml("<HD_LISTCALLSTATUS_INPUT><HD_QUERY_GROUP_ID /></HD_LISTCALLSTATUS_INPUT> ")
        objInXml1.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = ddlQueryGroup.SelectedValue
        objOutputXml1 = objCallStatus.List1(objInXml1)
        If objOutputXml1.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader1 = New XmlNodeReader(objOutputXml1)
            ds1.ReadXml(objXmlReader1)
            ddlQueryStatus.DataSource = Nothing
            ddlQueryStatus.DataBind()
            ddlQueryStatus.DataSource = ds1.Tables("CALL_STATUS")
            ddlQueryStatus.DataTextField = "HD_STATUS_NAME"
            ddlQueryStatus.DataValueField = "HD_STATUS_ID"
            ddlQueryStatus.DataBind()
            ddlQueryStatus.Items.Insert(0, New ListItem("--All--", ""))
        Else
            ddlQueryStatus.Items.Clear()
            ddlQueryStatus.Items.Insert(0, New ListItem("--All--", ""))
        End If

    End Sub

    Sub loadPriority()
        'Code to fill Query Priority
        '        Dim objSeverities As New AAMS.bizETrackerHelpDesk.bzSeverities
        Dim objSeverities As New AAMS.bizETrackerHelpDesk.bzSeverities

        Dim objOutputXml2 As XmlDocument
        Dim objXmlReader2 As XmlNodeReader
        Dim ds2 As New DataSet
        objOutputXml2 = New XmlDocument
        Dim objInXml2 As New XmlDocument
        objOutputXml2 = New XmlDocument
        objOutputXml2 = New XmlDocument
        objInXml2.LoadXml("<HD_LISTSEVERITIES_INPUT><HD_QUERY_GROUP_ID /></HD_LISTSEVERITIES_INPUT>")
        objInXml2.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = ddlQueryGroup.SelectedValue
        objOutputXml2 = objSeverities.List1(objInXml2)
        If objOutputXml2.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader2 = New XmlNodeReader(objOutputXml2)
            ds2.ReadXml(objXmlReader2)
            ddlQueryPriority.DataSource = Nothing
            ddlQueryPriority.DataBind()
            ddlQueryPriority.DataSource = ds2.Tables("SEVERITIES")
            ddlQueryPriority.DataTextField = "HD_SEVERITY_NAME"
            ddlQueryPriority.DataValueField = "HD_SEVERITY_ID"
            ddlQueryPriority.DataBind()
            ddlQueryPriority.Items.Insert(0, New ListItem("--All--", ""))
        Else
            ddlQueryPriority.Items.Clear()
            ddlQueryPriority.Items.Insert(0, New ListItem("--All--", ""))
        End If
    End Sub
    Sub loadCategory()
        '        Dim objCallCategory As New AAMS.bizETrackerHelpDesk.bzCallCategory
        Dim objCallCategory As New AAMS.bizETrackerHelpDesk.bzCallCategory

        Dim objOutputXml As XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        objOutputXml = New XmlDocument
        Dim objInXml As New XmlDocument
        objInXml.LoadXml("<HD_LISTCALLCATEGORY_INPUT><CALL_SUB_GROUP_ID /></HD_LISTCALLCATEGORY_INPUT>")
        objInXml.DocumentElement.SelectSingleNode("CALL_SUB_GROUP_ID").InnerText = ddlQuerySubGroup.SelectedValue
        objOutputXml = objCallCategory.List1(objInXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)

            ddlQueryCategory.DataSource = Nothing
            ddlQueryCategory.DataBind()

            ddlQueryCategory.DataSource = ds.Tables("CALL_CATEGORY")
            ddlQueryCategory.DataTextField = "CALL_CATEGORY_NAME"
            ddlQueryCategory.DataValueField = "CALL_CATEGORY_ID"
            ddlQueryCategory.DataBind()
            ddlQueryCategory.Items.Insert(0, New ListItem("--All--", ""))
            ddlQuerySubCategory.Items.Clear()
            ddlQuerySubCategory.Items.Insert(0, New ListItem("--All--", ""))
        Else
            ddlQueryCategory.Items.Clear()
            ddlQueryCategory.Items.Insert(0, New ListItem("--All--", ""))
            ddlQuerySubCategory.Items.Clear()
            ddlQuerySubCategory.Items.Insert(0, New ListItem("--All--", ""))
        End If
    End Sub
    Sub loadSubCategory()
        '        Dim objCallSubCategory As New AAMS.bizETrackerHelpDesk.bzCallSubCategory
        Dim objCallSubCategory As New AAMS.bizETrackerHelpDesk.bzCallSubCategory

        Dim objOutputXml As XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        objOutputXml = New XmlDocument
        Dim objInXml As New XmlDocument
        objInXml.LoadXml("<HD_LISTCALLSUBCATEGORY_INPUT> <CALL_CATEGORY_ID /></HD_LISTCALLSUBCATEGORY_INPUT>")
        objInXml.DocumentElement.SelectSingleNode("CALL_CATEGORY_ID").InnerText = ddlQueryCategory.SelectedValue
        objOutputXml = objCallSubCategory.List1(objInXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            ddlQuerySubCategory.DataSource = Nothing
            ddlQuerySubCategory.DataBind()
            ddlQuerySubCategory.DataSource = ds.Tables("CALL_SUB_CATEGORY")
            ddlQuerySubCategory.DataTextField = "CALL_SUB_CATEGORY_NAME"
            ddlQuerySubCategory.DataValueField = "CALL_SUB_CATEGORY_ID"
            ddlQuerySubCategory.DataBind()
            ddlQuerySubCategory.Items.Insert(0, New ListItem("--All--", ""))
        Else
            ddlQuerySubCategory.Items.Clear()
            ddlQuerySubCategory.Items.Insert(0, New ListItem("--All--", ""))
        End If
    End Sub
    Sub SearchRecords()
        lblError.Text = ""
        Dim objInputXml, objOutputXml As New XmlDocument
        '        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        '        Dim objbzDMReportConfiguration As New AAMS.bizETrackerHelpDesk.bzDMReportConfiguration
        Dim objbzDMReportConfiguration As New AAMS.bizETrackerHelpDesk.bzDMReportConfiguration

        If Session("LoginSession") IsNot Nothing Then
            Dim strSEARCH_INPUT As String = "<HD_DMSEARCH_CALLREQUEST_INPUT><LCODE></LCODE><HD_RE_ID></HD_RE_ID><AgencyName></AgencyName><Address></Address><HD_PTR_REF></HD_PTR_REF><HD_STATUS_ID></HD_STATUS_ID><FollowUp></FollowUp><HD_SEVERITY_ID></HD_SEVERITY_ID><LoggedBy></LoggedBy><HD_RE_OPEN_DATE_FROM></HD_RE_OPEN_DATE_FROM><HD_RE_OPEN_DATE_TO></HD_RE_OPEN_DATE_TO><HD_RE_CLOSED_DATE_FROM></HD_RE_CLOSED_DATE_FROM><HD_RE_CLOSED_DATE_TO></HD_RE_CLOSED_DATE_TO><Assigned_To></Assigned_To><AssignedDatetime></AssignedDatetime><OfficeID></OfficeID><CUSTOMER_CATEGORY_ID></CUSTOMER_CATEGORY_ID><CALLER_NAME></CALLER_NAME><HD_QUERY_GROUP_ID></HD_QUERY_GROUP_ID><CALL_SUB_GROUP_ID></CALL_SUB_GROUP_ID><CALL_CATEGORY_ID></CALL_CATEGORY_ID><CALL_SUB_CATEGORY_ID></CALL_SUB_CATEGORY_ID><COORDINATOR1></COORDINATOR1><COORDINATOR2></COORDINATOR2><DISPOSITION_ID></DISPOSITION_ID><Aoffice></Aoffice><AgencyAoffice></AgencyAoffice><WO_ID></WO_ID><LastCall></LastCall><EmployeeID></EmployeeID><LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY><LIMITED_TO_AOFFICE></LIMITED_TO_AOFFICE><LIMITED_TO_REGION></LIMITED_TO_REGION><SetID></SetID><HD_DESCRIPTION>0</HD_DESCRIPTION><HD_SOLUTION>0</HD_SOLUTION><CONTACTTYPE></CONTACTTYPE><TITLE></TITLE><ASTATUS></ASTATUS><ATYPE></ATYPE><HD_IR_REF></HD_IR_REF><COMP_VERTICAL></COMP_VERTICAL></HD_DMSEARCH_CALLREQUEST_INPUT>"
            objInputXml.LoadXml(strSEARCH_INPUT)
            'objInputXml.LoadXml("<HD_SEARCH_CALLREQUEST_INPUT><LCODE /><AgencyName /><OfficeID /><CUSTOMER_CATEGORY_ID /><LoggedBy />" & _
            '"<CALLER_NAME /><HD_QUERY_GROUP_ID /><CALL_SUB_GROUP_ID /><CALL_CATEGORY_ID /><CALL_SUB_CATEGORY_ID /><HD_STATUS_ID /> " & _
            '"<HD_SEVERITY_ID /><COORDINATOR1 /><COORDINATOR2 /><AssignedDatetime /><DISPOSITION_ID /><HD_RE_OPEN_DATE_FROM /> " & _
            '"<HD_RE_OPEN_DATE_TO /><HD_RE_CLOSED_DATE_FROM /><HD_RE_CLOSED_DATE_TO /><Aoffice /><AgencyAoffice /><HD_RE_ID /><HD_PTR_REF /> " & _
            '"<LastCall /><WO_ID /><LIMITED_TO_OWNAAGENCY /><LIMITED_TO_AOFFICE /><LIMITED_TO_REGION /></HD_SEARCH_CALLREQUEST_INPUT>")
            'objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyName.Value
            'objInputXml.DocumentElement.SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text
            If DlstCompVertical.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
            End If

            objInputXml.DocumentElement.SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text
            If (Request.Form("txtAgencyName") <> "" And hdAgencyName.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyName.Value = "") Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = ""
            Else
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyName.Value.Trim()
            End If

            'DivLoggedBy.Visible = False
            'DivCustCateg.Visible = False
            'Div1AOffice.Visible = False
            'DivAgency1AOffice.Visible = False
            'DivCord1.Visible = False
            'DivCord2.Visible = False
            'DivDateAssign.Visible = False
            'DivDispLastCall.Visible = False
            'DivDispos.Visible = False
            'DivLoggedBy.Visible = False
            'DivQueryCateg.Visible = False
            'DivOfficeId.Visible = False
            'DivQueryGroup.Visible = False
            ''DivQueryPriority.Visible = False
            'DivQuerySubCategory.Visible = False
            'DivQuerySubGroup.Visible = False
            'DivWorkOrderNo.Visible = False
            'DivCallName.Visible = False
            'DivAddress.Visible = False
            'DivassigneTo.Visible = False
            'DivSevirity.Visible = False
            '' DivTitle.Visible = False
            '' DivType.Visible = False
            'DivFollowUp.Visible = False
            'DivQueryStatus.Visible = False
            'DivConType.Visible = False
            'DivQueryCategTitle.Visible = False
            'Div2.Visible = False
            If DivOfficeId.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("OfficeID").InnerText = txtOfficeID.Text
            End If
            If DivCustCateg.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("CUSTOMER_CATEGORY_ID").InnerText = ddlCustomerCategory.SelectedValue
            End If
            If DivLoggedBy.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("LoggedBy").InnerText = txtLoggedBy.Text
            End If

            If DivCallName.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("CALLER_NAME").InnerText = txtCallerName.Text
            End If
            If DivQueryGroup.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = ddlQueryGroup.SelectedValue
            End If
            If DivQuerySubGroup.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_GROUP_ID").InnerText = ddlQuerySubGroup.SelectedValue
            End If
            If DivQueryCateg.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("CALL_CATEGORY_ID").InnerText = ddlQueryCategory.SelectedValue
            End If

            If DivQuerySubCategory.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY_ID").InnerText = ddlQuerySubCategory.SelectedValue
            End If

            If DivQueryStatus.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("HD_STATUS_ID").InnerText = ddlQueryStatus.SelectedValue
            End If
            If DivSevirity.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("HD_SEVERITY_ID").InnerText = ddlQueryPriority.SelectedValue
            End If

            If DivCord1.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("COORDINATOR1").InnerText = ddlCoordinator1.SelectedValue
            End If
            If DivCord2.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("COORDINATOR2").InnerText = ddlCoordinator2.SelectedValue
            End If

            If DivDateAssign.Visible = True Then
                If (Request.Form("txtDateAssigned") IsNot Nothing) Then
                    objInputXml.DocumentElement.SelectSingleNode("AssignedDatetime").InnerText = objeAAMS.GetDateFormat(Request.Form("txtDateAssigned"), "dd/MM/yyyy", "yyyyMMdd", "/")
                End If
            End If

            If DivDispos.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("DISPOSITION_ID").InnerText = ddlDisposition.SelectedValue
            End If

            If (Request.Form("txtQueryOpenedDateFrom") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("HD_RE_OPEN_DATE_FROM").InnerText = objeAAMS.GetDateFormat(Request.Form("txtQueryOpenedDateFrom"), "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm", "/") ' objeAAMS.GetDateFormat(Request.Form("txtQueryOpenedDateFrom"), "dd/MM/yyyy HH:MM", "MM/dd/yyyy HH:MM", "/")
            End If
            If (Request.Form("txtQueryOpenedDateTo") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("HD_RE_OPEN_DATE_TO").InnerText = objeAAMS.GetDateFormat(Request.Form("txtQueryOpenedDateTo"), "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm", "/") ' objeAAMS.GetDateFormat(Request.Form("txtQueryOpenedDateTo"), "dd/MM/yyyy HH:MM", "MM/dd/yyyy HH:MM", "/")
            End If
            If (Request.Form("txtCloseDateFrom") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("HD_RE_CLOSED_DATE_FROM").InnerText = objeAAMS.GetDateFormat(Request.Form("txtCloseDateFrom"), "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm", "/") 'objeAAMS.GetDateFormat(Request.Form("txtCloseDateFrom"), "dd/MM/yyyy HH:MM", "MM/dd/yyyy HH:MM", "/")
            End If
            If (Request.Form("txtCloseDateTo") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("HD_RE_CLOSED_DATE_TO").InnerText = objeAAMS.GetDateFormat(Request.Form("txtCloseDateTo"), "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm", "/") ' objeAAMS.GetDateFormat(Request.Form("txtCloseDateTo"), "dd/MM/yyyy HH:MM", "MM/dd/yyyy HH:MM", "/")
            End If

            If Div1AOffice.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = ddlAOffice.SelectedValue
            End If
            If DivAgency1AOffice.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("AgencyAoffice").InnerText = ddlAgencyAOffice.SelectedValue
            End If

            objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = txtLTRNo.Text

            objInputXml.DocumentElement.SelectSingleNode("HD_PTR_REF").InnerText = txtPTRNo.Text

            If DivDispLastCall.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("LastCall").InnerText = IIf(chkDisplayLastCall.Checked, "True", "false")
            Else
                objInputXml.DocumentElement.SelectSingleNode("LastCall").InnerText = "false"
            End If

            If DivWorkOrderNo.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("WO_ID").InnerText = txtWorkOrderNo.Text
            End If

            If DivAddress.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("Address").InnerText = txtAddresses.Text
            End If
            If DivFollowUp.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("FollowUp").InnerText = drpFollowup.SelectedValue
            End If

            If DivassigneTo.Visible = True Then
                If drpAssignedTo.SelectedValue.Trim.Length > 0 Then
                    If drpAssignedTo.SelectedValue.Trim.Split("|")(0) = "1" Then
                        objInputXml.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = "1"
                        objInputXml.DocumentElement.SelectSingleNode("Assigned_To").InnerText = drpAssignedTo.SelectedValue.Trim.Split("|")(1)
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = "2"
                        objInputXml.DocumentElement.SelectSingleNode("Assigned_To").InnerText = drpAssignedTo.SelectedValue.Trim.Split("|")(1)
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Assigned_To").InnerText = "" 'drpAssignedTo.SelectedValue
                End If

            End If

            If Not Session("Security") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = objeAAMS.Limited_To_OwnAgency(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = objeAAMS.Limited_To_Aoffice(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = objeAAMS.Limited_To_Region(Session("Security"))

            End If
            objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0).ToString()

            If DivConType.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("CONTACTTYPE").InnerText = ddlContactType.SelectedValue
            End If

            If DivQueryCategTitle.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("TITLE").InnerText = txtTit.Text
            End If

            If DivAgencyStatus.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("ASTATUS").InnerText = dlstAgencyStatus.SelectedValue
            End If

            If DivAgencyType.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("ATYPE").InnerText = DlstAgencyType.SelectedValue
            End If

            If DivHD_IR_REF.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("HD_IR_REF").InnerText = txtHD_IR_REF.Text
            End If

            objInputXml.DocumentElement.SelectSingleNode("SetID").InnerText = drpSelectSet.SelectedValue

            ' Here Back end Method Call
            objOutputXml = objbzDMReportConfiguration.Search(objInputXml)
            ' objOutputXml.Load("C:\Documents and Settings\Administrator\Desktop\HD_DMSEARCH_CALLREQUEST_OUTPUT.xml")

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                Session("ETHelpDeskDynamicReport") = objOutputXml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=ETHelpDeskDynamicReport", False)
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        End If
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect("HDRPT_HelpDeskDynamicReport.aspx", False)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub ddlQueryGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlQueryGroup.SelectedIndexChanged
        Try
            loadSubGroup()
            loadCategory()
            loadSubCategory()
            loadPriority()
            loadQueryStatus()
            loadAssignedTo()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlQuerySubGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlQuerySubGroup.SelectedIndexChanged
        Try
            loadCategory()
            loadSubCategory()

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlQueryCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlQueryCategory.SelectedIndexChanged
        Try
            loadSubCategory()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub drpSelectSet_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpSelectSet.SelectedIndexChanged
        Try
            lblError.Text = ""
            Dim objNode As XmlNode
            DivLoggedBy.Visible = False
            DivCustCateg.Visible = False
            Div1AOffice.Visible = False
            DivAgency1AOffice.Visible = False
            DivCord1.Visible = False
            DivCord2.Visible = False
            DivDateAssign.Visible = False
            DivDispLastCall.Visible = False
            DivDispos.Visible = False
            DivLoggedBy.Visible = False
            DivQueryCateg.Visible = False
            DivOfficeId.Visible = False
            DivQueryGroup.Visible = False
            'DivQueryPriority.Visible = False
            DivQuerySubCategory.Visible = False
            DivQuerySubGroup.Visible = False
            DivWorkOrderNo.Visible = False
            DivCallName.Visible = False
            DivAddress.Visible = False
            DivassigneTo.Visible = False
            DivSevirity.Visible = False
            ' DivTitle.Visible = False
            ' DivType.Visible = False
            DivFollowUp.Visible = False
            DivQueryStatus.Visible = False
            DivConType.Visible = False
            DivQueryCategTitle.Visible = False
            Div2.Visible = False
            DivAgencyType.Visible = False
            DivAgencyStatus.Visible = False
            DivHD_IR_REF.Visible = False

            If Session("LoginSession") IsNot Nothing Then
                Dim objInputXml, objOutputXml As New XmlDocument
                'Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                '                Dim objbzDMReportConfiguration As New AAMS.bizETrackerHelpDesk.bzDMReportConfiguration
                Dim objbzDMReportConfiguration As New AAMS.bizETrackerHelpDesk.bzDMReportConfiguration

                objInputXml.LoadXml("<HD_GETDMSEARCHCONFIGURE_INPUT><EmployeeID /><SetID/></HD_GETDMSEARCHCONFIGURE_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0).ToString
                objInputXml.DocumentElement.SelectSingleNode("SetID").InnerText = drpSelectSet.SelectedValue
                ''Here Back end Method Call
                objOutputXml = objbzDMReportConfiguration.getConfiguredSearchField(objInputXml)
                'objOutputXml.LoadXml("<HD_VIEWDMSFIELDS_OUTPUT> " & _
                '"<FIELDDETAILS DM_FIELD_ID='LTR_NO' DM_FILED_NAME='LTR No' DM_SEARCH_FIELD='True' DM_RESULT_FILED='False' />" & _
                '"<FIELDDETAILS DM_FIELD_ID='AGENCY' DM_FILED_NAME='Agency' DM_SEARCH_FIELD='True' DM_RESULT_FILED='False' />" & _
                '"<FIELDDETAILS DM_FIELD_ID='PTR_NO' DM_FILED_NAME='PTR Number' DM_SEARCH_FIELD='True' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='CLOSE_DATE_FROM' DM_FILED_NAME='Close Date From' DM_SEARCH_FIELD='True' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='CLOSE_DATE_TO' DM_FILED_NAME='Close Date To' DM_SEARCH_FIELD='True' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='OPEN_DATE_FROM' DM_FILED_NAME='Open Date From' DM_SEARCH_FIELD='True' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='OPEN_DATE_TO' DM_FILED_NAME='Open Date To' DM_SEARCH_FIELD='True' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='ADDRESS' DM_FILED_NAME='Address' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='TITLE' DM_FILED_NAME='Title' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='QUERY_STATUS' DM_FILED_NAME='Query Status' DM_SEARCH_FIELD='True' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='FOLLOWUP' DM_FILED_NAME='Follow up' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='SEVERITY' DM_FILED_NAME='Severity' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='TYPE' DM_FILED_NAME='Type' DM_SEARCH_FIELD='True' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='LOGGED_BY' DM_FILED_NAME='Logged By' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='ASSIGNED_TO' DM_FILED_NAME='Assigned To' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='ASSIGNED_DATE_TIME' DM_FILED_NAME='Assigned Date Time' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='OFFICE_ID' DM_FILED_NAME='Office ID' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='CUSTOMER_CATEGORY' DM_FILED_NAME='Customer Category' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' />  " & _
                '"<FIELDDETAILS DM_FIELD_ID='CALLER_NAME' DM_FILED_NAME='Caller Name' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='QUERY_GROUP' DM_FILED_NAME='Query Group' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='QUERY_SUB_GROUP' DM_FILED_NAME='Query Sub Group' DM_SEARCH_FIELD='True' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='QUERY_CATEGORY' DM_FILED_NAME='Query Category' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='QUERY_SUB_CATEGORY' DM_FILED_NAME='Query Sub Category' DM_SEARCH_FIELD='True' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='QUERY_PRIORITY' DM_FILED_NAME='Query Priority' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='COORDINATOR1' DM_FILED_NAME='Coordinator1' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='COORDINATOR2' DM_FILED_NAME='Coordinator2' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='DISPOSITION' DM_FILED_NAME='Disposition' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='1AOFFICE' DM_FILED_NAME='1A Office' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='AGENCY_1A_OFFICE' DM_FILED_NAME='Agency 1A Office' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                '"<FIELDDETAILS DM_FIELD_ID='WORK_ORDER_NO' DM_FILED_NAME='Work Order No' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' />" & _
                '"<FIELDDETAILS DM_FIELD_ID='LAST_CALL' DM_FILED_NAME='Last Call' DM_SEARCH_FIELD='False' DM_RESULT_FILED='False' /> " & _
                ' "<Errors Status='FALSE'> " & _
                '  "<Error Code='' Description='' /> " & _
                '  "</Errors>" & _
                '  "</HD_VIEWDMSFIELDS_OUTPUT>")
                ' @ Show Panel 
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='ADDRESS']")

                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            DivAddress.Visible = True
                        End If
                    End If

                    'objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='TITLE']")
                    'If objNode.Attributes("SELECTED").Value = "True" Then
                    '    DivTitle.Visible = True
                    'End If

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='QUERY_STATUS']")
                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            DivQueryStatus.Visible = True
                        End If
                    End If

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='FOLLOWUP']")

                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            DivFollowUp.Visible = True
                        End If
                    End If

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='SEVERITY']")

                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            DivSevirity.Visible = True
                        End If
                    End If

                    'objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='TYPE']")
                    'If objNode.Attributes("DM_SEARCH_FIELD").Value = "True" Then
                    '    DivType.Visible = True
                    'End If

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='LOGGED_BY']")
                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            DivLoggedBy.Visible = True
                        End If
                    End If

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='ASSIGNED_TO']")
                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            DivassigneTo.Visible = True
                        End If
                    End If

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='ASSIGNED_DATE_TIME']")
                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            DivDateAssign.Visible = True
                        End If
                    End If

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='OFFICE_ID']")

                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            DivOfficeId.Visible = True
                        End If
                    End If

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='CUSTOMER_CATEGORY']")
                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            DivCustCateg.Visible = True
                        End If
                    End If

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='CALLER_NAME']")

                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            DivCallName.Visible = True
                        End If
                    End If

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='QUERY_GROUP']")

                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            DivQueryGroup.Visible = True
                        End If
                    End If

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='QUERY_SUB_GROUP']")
                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            DivQuerySubGroup.Visible = True
                        End If
                    End If

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='QUERY_CATEGORY']")
                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            DivQueryCateg.Visible = True
                        End If
                    End If

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='QUERY_SUB_CATEGORY']")
                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            DivQuerySubCategory.Visible = True
                        End If
                    End If

                    'objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='QUERY_PRIORITY']")
                    'If objNode.Attributes("SELECTED").Value = "True" Then
                    '    DivQueryPriority.Visible = True
                    'End If

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='COORDINATOR1']")
                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            DivCord1.Visible = True
                        End If
                    End If

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='COORDINATOR2']")
                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            DivCord2.Visible = True
                        End If
                    End If

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='DISPOSITION']")
                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            DivDispos.Visible = True
                        End If
                    End If

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='1AOFFICE']")
                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            Div1AOffice.Visible = True
                        End If
                    End If

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='AGENCY_1A_OFFICE']")

                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            DivAgency1AOffice.Visible = True
                        End If
                    End If

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='WORK_ORDER_NO']")
                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            DivWorkOrderNo.Visible = True
                        End If
                    End If

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='LAST_CALL']")
                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            DivDispLastCall.Visible = True
                        End If
                    End If

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='CONTACTTYPE']")
                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            DivConType.Visible = True
                        End If
                    End If

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='TITLE']")
                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            DivQueryCategTitle.Visible = True
                            Div2.Visible = True
                        End If
                    End If

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='ASTATUS']")
                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            DivAgencyStatus.Visible = True
                        End If
                    End If

                    objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='ATYPE']")
                    If objNode IsNot Nothing Then
                        If objNode.Attributes("SELECTED").Value = "True" Then
                            DivAgencyType.Visible = True
                        End If
                    End If

                    setDefaultValueBySetId(objOutputXml)
                Else
                    txtLTRNo.Text = ""
                    txtAgencyName.Text = ""
                    txtAddresses.Text = ""
                    txtPTRNo.Text = ""
                    ' ChkLastCall.Checked = False
                    txtLoggedBy.Text = ""
                    txtCloseDateFrom.Text = ""
                    txtCloseDateTo.Text = ""
                    txtQueryOpenedDateFrom.Text = ""
                    txtQueryOpenedDateTo.Text = ""
                    drpAssignedTo.SelectedValue = ""
                    txtDateAssigned.Text = ""
                    txtOfficeID.Text = ""
                    ddlCustomerCategory.SelectedValue = ""
                    txtCallerName.Text = ""
                    ddlQueryGroup.SelectedValue = ""

                    loadSubGroup()
                    loadPriority()
                    loadQueryStatus()
                    loadAssignedTo()

                    ddlQuerySubGroup.SelectedValue = ""
                    '@
                    loadCategory()
                    '@

                    ddlQueryCategory.SelectedValue = ""
                    '@
                    loadSubCategory()
                    '@

                    ddlQuerySubCategory.SelectedValue = ""

                    ddlQueryStatus.SelectedValue = ""
                    drpFollowup.SelectedValue = ""
                    ddlQueryPriority.SelectedValue = ""

                    ddlCoordinator1.SelectedValue = ""
                    ddlCoordinator2.SelectedValue = ""
                    ddlDisposition.SelectedValue = ""
                    ddlAOffice.SelectedValue = ""
                    ddlAgencyAOffice.SelectedValue = ""
                    txtWorkOrderNo.Text = ""
                    ddlContactType.SelectedValue = ""
                    txtTit.Text = ""
                    dlstAgencyStatus.SelectedValue = ""
                    DlstAgencyType.SelectedValue = ""
                    txtHD_IR_REF.Text = ""

                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub GetSetId()
        '        Dim objbzDMReportConfiguration As New AAMS.bizETrackerHelpDesk.bzDMReportConfiguration
        Dim objbzDMReportConfiguration As New AAMS.bizETrackerHelpDesk.bzDMReportConfiguration

        Dim objOutputXml As XmlDocument
        Dim ds As New DataSet
        objOutputXml = New XmlDocument
        Dim objInXml As New XmlDocument
        Dim count As Integer = 0
        Dim i As Integer
        drpSelectSet.Items.Clear()
        Try
            objInXml.LoadXml("<HD_GETDMCONFIGURESET_INPUT> <EmployeeID></EmployeeID></HD_GETDMCONFIGURESET_INPUT>")
            If Session("LoginSession") IsNot Nothing Then
                objInXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0).ToString
                objOutputXml = objbzDMReportConfiguration.GetConfiguredSetCount(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    count = objOutputXml.DocumentElement.SelectSingleNode("CONFIGUREDSET").Attributes("SetCount").Value
                    If count > 0 Then
                        For i = 1 To count
                            drpSelectSet.Items.Add(i.ToString)
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
        Finally
            Dim li, li2 As ListItem
            li = drpSelectSet.Items.FindByValue("")
            If li Is Nothing Then
                drpSelectSet.Items.Insert(0, New ListItem(""))
            End If
            If count > 0 Then
                li2 = drpSelectSet.Items.FindByValue("1")
                If li2 IsNot Nothing Then
                    drpSelectSet.SelectedValue = "1"
                End If
            End If
        End Try
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    Sub loadAssignedTo()
        'Code to fill Assigned To
        Dim objOutputXml As New XmlDocument
        Dim objInputXml As New XmlDocument
        Dim ds As New DataSet
        objOutputXml = New XmlDocument
        Dim objInXml As New XmlDocument
        Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
        drpAssignedTo.Items.Clear()
        If ddlQueryGroup.SelectedValue = "" Then
            objOutputXml = objbzEmployee.List
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                For Each objnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("EMPLOYEE")
                    drpAssignedTo.Items.Add(New ListItem(objnode.Attributes("Employee_Name").Value, "2" + "|" + objnode.Attributes("EmployeeID").Value))
                Next
            End If

            '            Dim objbzTeam As New AAMS.bizETrackerHelpDesk.bzTeam
            Dim objbzTeam As New AAMS.bizETrackerHelpDesk.bzTeam

            objOutputXml = New XmlDocument
            objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
            objOutputXml = objbzTeam.List()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                For Each objnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("TEAM")
                    drpAssignedTo.Items.Add(New ListItem(objnode.Attributes("TEAM_NAME").Value, "1" + "|" + objnode.Attributes("TEAM_ID").Value))
                Next
            End If

        ElseIf ddlQueryGroup.SelectedValue = "1" Then
            '            Dim objbzTeam As New AAMS.bizETrackerHelpDesk.bzTeam
            Dim objbzTeam As New AAMS.bizETrackerHelpDesk.bzTeam

            objOutputXml = New XmlDocument
            objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
            objOutputXml = objbzTeam.List()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                For Each objnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("TEAM")
                    drpAssignedTo.Items.Add(New ListItem(objnode.Attributes("TEAM_NAME").Value, "1" + "|" + objnode.Attributes("TEAM_ID").Value))
                Next
            End If
        ElseIf ddlQueryGroup.SelectedValue = "2" Then

            objOutputXml = objbzEmployee.List
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                For Each objnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("EMPLOYEE")
                    drpAssignedTo.Items.Add(New ListItem(objnode.Attributes("Employee_Name").Value, "2" + "|" + objnode.Attributes("EmployeeID").Value))
                Next
            End If

        End If
        drpAssignedTo.Items.Insert(0, New ListItem("--All--", ""))
    End Sub


    Private Sub setDefaultValueBySetId(ByVal ObjOutxml As XmlDocument)
        Dim objNode As XmlNode
        Try
            Try
                'txtLTRNo.Text = ""
                'txtAgencyName.Text = ""
                'txtAddresses.Text = ""
                'txtPTRNo.Text = ""
                '' ChkLastCall.Checked = False
                'ddlQueryStatus.SelectedValue = ""
                'drpFollowup.SelectedValue = ""
                'ddlQueryPriority.SelectedValue = ""
                'txtLoggedBy.Text = ""
                'txtCloseDateFrom.Text = ""
                'txtCloseDateTo.Text = ""
                'txtQueryOpenedDateFrom.Text = ""
                'txtQueryOpenedDateTo.Text = ""
                'drpAssignedTo.SelectedValue = ""
                'txtDateAssigned.Text = ""
                'txtOfficeID.Text = ""
                'ddlCustomerCategory.SelectedValue = ""
                'txtCallerName.Text = ""
                'ddlQueryGroup.SelectedValue = ""

                'loadSubGroup()
                'loadPriority()
                'loadQueryStatus()
                'loadAssignedTo()

                'ddlQuerySubGroup.SelectedValue = ""
                ''@
                'loadCategory()
                ''@


                'ddlQueryCategory.SelectedValue = ""
                ''@
                'loadSubCategory()
                ''@

                'ddlQuerySubCategory.SelectedValue = ""
                'ddlCoordinator1.SelectedValue = ""
                'ddlCoordinator2.SelectedValue = ""
                'ddlDisposition.SelectedValue = ""
                'ddlAOffice.SelectedValue = ""
                'ddlAgencyAOffice.SelectedValue = ""
                'txtWorkOrderNo.Text = ""
                'ddlContactType.SelectedValue = ""
                'txtTit.Text = ""
            Catch ex As Exception
            End Try

            If ObjOutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                '<FIELDDETAILS DM_FIELD_ID="LTR_NO" DM_FIELD_NAME="LTR No" SELECTED="True" Value="LTR_NO" ValueText="LTR_NO" DM_FIELD_RS_POSITION="2" POSITION="1" /> 
                ' <FIELDDETAILS DM_FIELD_ID="AGENCY" DM_FIELD_NAME="Agency" SELECTED="True" Value="AGENCY" ValueText="AGENCY" DM_FIELD_RS_POSITION="3" POSITION="2" /> 
                ' <FIELDDETAILS DM_FIELD_ID="ADDRESS" DM_FIELD_NAME="Address" SELECTED="True" Value="ADDRESS" ValueText="ADDRESS" DM_FIELD_RS_POSITION="4" POSITION="3" /> 
                ' <FIELDDETAILS DM_FIELD_ID="PTR_NO" DM_FIELD_NAME="PTR Number" SELECTED="True" Value="PTR_NO" ValueText="PTR_NO" DM_FIELD_RS_POSITION="5" POSITION="4" /> 
                ' <FIELDDETAILS DM_FIELD_ID="QUERY_STATUS" DM_FIELD_NAME="Query Status" SELECTED="True" Value="" ValueText="" DM_FIELD_RS_POSITION="6" POSITION="5" /> 
                ' <FIELDDETAILS DM_FIELD_ID="FOLLOWUP" DM_FIELD_NAME="Follow up" SELECTED="True" Value="" ValueText="" DM_FIELD_RS_POSITION="7" POSITION="6" /> 
                ' <FIELDDETAILS DM_FIELD_ID="OPEN_DATE_FROM" DM_FIELD_NAME="Open Date From" SELECTED="True" Value="OPEN_DATE_FROM" ValueText="OPEN_DATE_FROM" DM_FIELD_RS_POSITION="10" POSITION="7" /> 
                ' <FIELDDETAILS DM_FIELD_ID="OPEN_DATE_TO" DM_FIELD_NAME="Open Date To" SELECTED="True" Value="OPEN_DATE_TO" ValueText="OPEN_DATE_TO" DM_FIELD_RS_POSITION="11" POSITION="8" /> 
                ' <FIELDDETAILS DM_FIELD_ID="CLOSE_DATE_FROM" DM_FIELD_NAME="Close Date From" SELECTED="True" Value="CLOSE_DATE_FROM" ValueText="CLOSE_DATE_FROM" DM_FIELD_RS_POSITION="12" POSITION="9" /> 
                ' <FIELDDETAILS DM_FIELD_ID="CLOSE_DATE_TO" DM_FIELD_NAME="Close Date To" SELECTED="True" Value="CLOSE_DATE_TO" ValueText="CLOSE_DATE_TO" DM_FIELD_RS_POSITION="13" POSITION="10" /> 
                ' <FIELDDETAILS DM_FIELD_ID="LAST_CALL" DM_FIELD_NAME="Last Call" SELECTED="True" Value="" ValueText="" DM_FIELD_RS_POSITION="30" POSITION="11" /> 


                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='COMP_VERTICAL']")
                If objNode IsNot Nothing Then

                    If objNode.Attributes("SELECTED").Value = "True" Then
                        Dim li As New ListItem
                        li = DlstCompVertical.Items.FindByValue(objNode.Attributes("Value").Value)
                        If li IsNot Nothing Then
                            DlstCompVertical.SelectedValue = objNode.Attributes("Value").Value
                        End If
                    End If

                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='ASTATUS']")

                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        Dim li As New ListItem
                        li = dlstAgencyStatus.Items.FindByValue(objNode.Attributes("Value").Value)
                        If li IsNot Nothing Then
                            dlstAgencyStatus.SelectedValue = objNode.Attributes("Value").Value
                        End If
                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='ATYPE']")

                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        Dim li As New ListItem
                        li = DlstAgencyType.Items.FindByValue(objNode.Attributes("Value").Value)
                        If li IsNot Nothing Then
                            DlstAgencyType.SelectedValue = objNode.Attributes("Value").Value
                        End If

                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='AGENCY']")

                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        txtAgencyName.Text = objNode.Attributes("Value").Value
                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='LTR_NO']")

                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        txtLTRNo.Text = objNode.Attributes("Value").Value
                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='ADDRESS']")

                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        txtAddresses.Text = objNode.Attributes("Value").Value
                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='PTR_NO']")

                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        txtPTRNo.Text = objNode.Attributes("Value").Value
                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='ADDRESS']")

                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        txtAddresses.Text = objNode.Attributes("Value").Value
                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='OPEN_DATE_FROM']")

                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        txtQueryOpenedDateFrom.Text = objNode.Attributes("Value").Value
                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='OPEN_DATE_TO']")

                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        txtQueryOpenedDateTo.Text = objNode.Attributes("Value").Value
                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='CLOSE_DATE_FROM']")

                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        txtCloseDateFrom.Text = objNode.Attributes("Value").Value
                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='CLOSE_DATE_TO']")

                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        txtCloseDateTo.Text = objNode.Attributes("Value").Value
                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='QUERY_GROUP']")

                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        Dim li As New ListItem
                        li = ddlQueryGroup.Items.FindByValue(objNode.Attributes("Value").Value)
                        If li IsNot Nothing Then
                            ddlQueryGroup.SelectedValue = objNode.Attributes("Value").Value
                        End If

                    End If
                End If

                '@ 
                loadSubGroup()
                loadPriority()
                loadQueryStatus()
                loadAssignedTo()
                '@

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='QUERY_SUB_GROUP']")
                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        Dim li As New ListItem
                        li = ddlQuerySubGroup.Items.FindByValue(objNode.Attributes("Value").Value)
                        If li IsNot Nothing Then
                            ddlQuerySubGroup.SelectedValue = objNode.Attributes("Value").Value
                        End If

                    End If
                End If

                '@
                loadCategory()
                '@

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='QUERY_CATEGORY']")
                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        Dim li As New ListItem
                        li = ddlQueryCategory.Items.FindByValue(objNode.Attributes("Value").Value)
                        If li IsNot Nothing Then
                            ddlQueryCategory.SelectedValue = objNode.Attributes("Value").Value
                        End If

                    End If
                End If
                '@
                loadSubCategory()
                '@

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='QUERY_SUB_CATEGORY']")
                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        Dim li As New ListItem
                        li = ddlQuerySubCategory.Items.FindByValue(objNode.Attributes("Value").Value)
                        If li IsNot Nothing Then
                            ddlQuerySubCategory.SelectedValue = objNode.Attributes("Value").Value
                        End If

                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='QUERY_STATUS']")
                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then

                        ' ddlQueryStatus.SelectedValue = "1"

                        Dim li As New ListItem
                        li = ddlQueryStatus.Items.FindByValue(objNode.Attributes("Value").Value)
                        If li IsNot Nothing Then
                            ddlQueryStatus.SelectedValue = objNode.Attributes("Value").Value
                        End If

                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='FOLLOWUP']")

                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then

                        Dim li As New ListItem
                        li = drpFollowup.Items.FindByValue(objNode.Attributes("Value").Value)
                        If li IsNot Nothing Then
                            drpFollowup.SelectedValue = objNode.Attributes("Value").Value
                        End If

                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='SEVERITY']")

                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        Dim li As New ListItem
                        li = ddlQueryPriority.Items.FindByValue(objNode.Attributes("Value").Value)
                        If li IsNot Nothing Then
                            ddlQueryPriority.SelectedValue = objNode.Attributes("Value").Value
                        End If

                    End If
                End If

                'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='TYPE']")
                'If objNode.Attributes("DM_SEARCH_FIELD").Value = "True" Then
                '    DivType.Visible = True
                'End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='LOGGED_BY']")
                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then

                        txtLoggedBy.Text = objNode.Attributes("Value").Value
                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='ASSIGNED_TO']")
                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then

                        Dim li As New ListItem
                        Dim strvalue As String = "1|" + objNode.Attributes("Value").Value

                        li = drpAssignedTo.Items.FindByValue(strvalue)
                        If li IsNot Nothing Then
                            drpAssignedTo.SelectedValue = strvalue
                        Else
                            Dim li2 As New ListItem
                            strvalue = ""
                            strvalue = "2|" + objNode.Attributes("Value").Value
                            li2 = drpAssignedTo.Items.FindByValue(strvalue)
                            If li2 IsNot Nothing Then
                                drpAssignedTo.SelectedValue = strvalue
                            End If
                        End If
                        'Dim li As New ListItem
                        'li = drpAssignedTo.Items.FindByValue(objNode.Attributes("Value").Value)
                        'If li IsNot Nothing Then
                        '    drpAssignedTo.SelectedValue = objNode.Attributes("Value").Value
                        'End If
                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='ASSIGNED_DATE_TIME']")
                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        txtDateAssigned.Text = objNode.Attributes("Value").Value
                    End If
                End If


                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='OFFICE_ID']")

                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        txtOfficeID.Text = objNode.Attributes("Value").Value
                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='CUSTOMER_CATEGORY']")
                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        Dim li As New ListItem
                        li = ddlCustomerCategory.Items.FindByValue(objNode.Attributes("Value").Value)
                        If li IsNot Nothing Then
                            ddlCustomerCategory.SelectedValue = objNode.Attributes("Value").Value
                        End If
                    End If
                End If


                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='CALLER_NAME']")

                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        txtCallerName.Text = objNode.Attributes("Value").Value
                    End If
                End If

                'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='QUERY_PRIORITY']")
                'If objNode.Attributes("SELECTED").Value = "True" Then
                '    DivQueryPriority.Visible = True
                'End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='COORDINATOR1']")
                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        Dim li As New ListItem
                        li = ddlCoordinator1.Items.FindByValue(objNode.Attributes("Value").Value)
                        If li IsNot Nothing Then
                            ddlCoordinator1.SelectedValue = objNode.Attributes("Value").Value
                        End If

                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='COORDINATOR2']")
                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        Dim li As New ListItem
                        li = ddlCoordinator2.Items.FindByValue(objNode.Attributes("Value").Value)
                        If li IsNot Nothing Then
                            ddlCoordinator2.SelectedValue = objNode.Attributes("Value").Value
                        End If

                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='DISPOSITION']")
                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        Dim li As New ListItem
                        li = ddlDisposition.Items.FindByValue(objNode.Attributes("Value").Value)
                        If li IsNot Nothing Then
                            ddlDisposition.SelectedValue = objNode.Attributes("Value").Value
                        End If

                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='1AOFFICE']")
                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        Dim li As New ListItem
                        li = ddlAOffice.Items.FindByValue(objNode.Attributes("Value").Value)
                        If li IsNot Nothing Then
                            ddlAOffice.SelectedValue = objNode.Attributes("Value").Value
                        End If

                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='AGENCY_1A_OFFICE']")

                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        Dim li As New ListItem
                        li = ddlAgencyAOffice.Items.FindByValue(objNode.Attributes("Value").Value)
                        If li IsNot Nothing Then
                            ddlAgencyAOffice.SelectedValue = objNode.Attributes("Value").Value
                        End If

                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='WORK_ORDER_NO']")
                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        txtWorkOrderNo.Text = objNode.Attributes("Value").Value
                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='LAST_CALL']")
                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        If objNode.Attributes("Value").Value = "1" Then
                            chkDisplayLastCall.Checked = True
                            txtQueryOpenedDateFrom.Text = ""
                            txtQueryOpenedDateTo.Text = ""
                        Else
                            chkDisplayLastCall.Checked = False
                            If txtQueryOpenedDateFrom.Text = "" Then
                                txtQueryOpenedDateFrom.Text = hdFromTime.Value

                            End If
                            If txtQueryOpenedDateTo.Text = "" Then
                                txtQueryOpenedDateTo.Text = hdToTime.Value
                            End If
                            'hdToTime.Value = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm")
                            'hdFromTime.Value = System.DateTime.Now.Subtract(New TimeSpan(6, 0, 0)).ToString("dd/MM/yyyy HH:mm")
                        End If
                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='CONTACTTYPE']")
                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        Dim li As New ListItem
                        li = ddlContactType.Items.FindByValue(objNode.Attributes("Value").Value)
                        If li IsNot Nothing Then
                            ddlContactType.SelectedValue = objNode.Attributes("Value").Value
                        End If
                    End If
                End If

                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='TITLE']")
                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        txtTit.Text = objNode.Attributes("Value").Value
                    End If
                End If
                objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='HD_IR_REF']")
                If objNode IsNot Nothing Then
                    If objNode.Attributes("SELECTED").Value = "True" Then
                        txtHD_IR_REF.Text = objNode.Attributes("Value").Value
                    End If
                End If
            Else
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub BtnRequestForReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnRequestForReport.Click
        Try
            lblError.Text = ""
            If drpSelectSet.SelectedValue = "" Then
                lblError.Text = "First Select any defined Set"
                Exit Sub
            End If

            Dim objInputXml, objOutputXml As New XmlDocument

            Dim ds As New DataSet
            '            Dim objbzDMReportConfiguration As New AAMS.bizETrackerHelpDesk.bzDMReportConfiguration
            Dim objbzDMReportConfiguration As New AAMS.bizETrackerHelpDesk.bzDMReportConfiguration

            If Session("LoginSession") IsNot Nothing Then

                Dim strSEARCH_INPUT As String = "<HD_DMSEARCH_CALLREQUEST_INPUT><LCODE></LCODE><HD_RE_ID></HD_RE_ID><AgencyName></AgencyName><Address></Address><HD_PTR_REF></HD_PTR_REF><HD_STATUS_ID></HD_STATUS_ID><FollowUp></FollowUp><HD_SEVERITY_ID></HD_SEVERITY_ID><LoggedBy></LoggedBy><HD_RE_OPEN_DATE_FROM></HD_RE_OPEN_DATE_FROM><HD_RE_OPEN_DATE_TO></HD_RE_OPEN_DATE_TO><HD_RE_CLOSED_DATE_FROM></HD_RE_CLOSED_DATE_FROM><HD_RE_CLOSED_DATE_TO></HD_RE_CLOSED_DATE_TO><Assigned_To></Assigned_To><AssignedDatetime></AssignedDatetime><OfficeID></OfficeID><CUSTOMER_CATEGORY_ID></CUSTOMER_CATEGORY_ID><CALLER_NAME></CALLER_NAME><HD_QUERY_GROUP_ID></HD_QUERY_GROUP_ID><CALL_SUB_GROUP_ID></CALL_SUB_GROUP_ID><CALL_CATEGORY_ID></CALL_CATEGORY_ID><CALL_SUB_CATEGORY_ID></CALL_SUB_CATEGORY_ID><COORDINATOR1></COORDINATOR1><COORDINATOR2></COORDINATOR2><DISPOSITION_ID></DISPOSITION_ID><Aoffice></Aoffice><AgencyAoffice></AgencyAoffice><WO_ID></WO_ID><LastCall></LastCall><EmployeeID></EmployeeID><LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY><LIMITED_TO_AOFFICE></LIMITED_TO_AOFFICE><LIMITED_TO_REGION></LIMITED_TO_REGION><SetID></SetID><HD_DESCRIPTION>0</HD_DESCRIPTION><HD_SOLUTION>0</HD_SOLUTION><CONTACTTYPE></CONTACTTYPE><TITLE></TITLE><ASTATUS></ASTATUS><ATYPE></ATYPE><HD_IR_REF></HD_IR_REF><COMP_VERTICAL></COMP_VERTICAL></HD_DMSEARCH_CALLREQUEST_INPUT>"

                Dim StrRequestDEtails As String = ""
                objInputXml.LoadXml(strSEARCH_INPUT)
                'objInputXml.LoadXml("<HD_SEARCH_CALLREQUEST_INPUT><LCODE /><AgencyName /><OfficeID /><CUSTOMER_CATEGORY_ID /><LoggedBy />" & _
                '"<CALLER_NAME /><HD_QUERY_GROUP_ID /><CALL_SUB_GROUP_ID /><CALL_CATEGORY_ID /><CALL_SUB_CATEGORY_ID /><HD_STATUS_ID /> " & _
                '"<HD_SEVERITY_ID /><COORDINATOR1 /><COORDINATOR2 /><AssignedDatetime /><DISPOSITION_ID /><HD_RE_OPEN_DATE_FROM /> " & _
                '"<HD_RE_OPEN_DATE_TO /><HD_RE_CLOSED_DATE_FROM /><HD_RE_CLOSED_DATE_TO /><Aoffice /><AgencyAoffice /><HD_RE_ID /><HD_PTR_REF /> " & _
                '"<LastCall /><WO_ID /><LIMITED_TO_OWNAAGENCY /><LIMITED_TO_AOFFICE /><LIMITED_TO_REGION /></HD_SEARCH_CALLREQUEST_INPUT>")
                'objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyName.Value
                'objInputXml.DocumentElement.SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text

                If DlstCompVertical.SelectedValue <> "" Then
                    objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
                End If

                objInputXml.DocumentElement.SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text
                If (Request.Form("txtAgencyName") <> "" And hdAgencyName.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyName.Value = "") Then
                    objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = ""
                Else
                    objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyName.Value.Trim()
                End If

                'DivLoggedBy.Visible = False
                'DivCustCateg.Visible = False
                'Div1AOffice.Visible = False
                'DivAgency1AOffice.Visible = False
                'DivCord1.Visible = False
                'DivCord2.Visible = False
                'DivDateAssign.Visible = False
                'DivDispLastCall.Visible = False
                'DivDispos.Visible = False
                'DivLoggedBy.Visible = False
                'DivQueryCateg.Visible = False
                'DivOfficeId.Visible = False
                'DivQueryGroup.Visible = False
                ''DivQueryPriority.Visible = False
                'DivQuerySubCategory.Visible = False
                'DivQuerySubGroup.Visible = False
                'DivWorkOrderNo.Visible = False
                'DivCallName.Visible = False
                'DivAddress.Visible = False
                'DivassigneTo.Visible = False
                'DivSevirity.Visible = False
                '' DivTitle.Visible = False
                '' DivType.Visible = False
                'DivFollowUp.Visible = False
                'DivQueryStatus.Visible = False
                'DivConType.Visible = False
                'DivQueryCategTitle.Visible = False
                'Div2.Visible = False

                If DivOfficeId.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("OfficeID").InnerText = txtOfficeID.Text
                End If
                If DivCustCateg.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("CUSTOMER_CATEGORY_ID").InnerText = ddlCustomerCategory.SelectedValue
                End If
                If DivLoggedBy.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("LoggedBy").InnerText = txtLoggedBy.Text
                End If

                If DivCallName.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("CALLER_NAME").InnerText = txtCallerName.Text
                End If
                If DivQueryGroup.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = ddlQueryGroup.SelectedValue
                End If
                If DivQuerySubGroup.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_GROUP_ID").InnerText = ddlQuerySubGroup.SelectedValue
                End If
                If DivQueryCateg.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("CALL_CATEGORY_ID").InnerText = ddlQueryCategory.SelectedValue
                End If
                If DivQuerySubCategory.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY_ID").InnerText = ddlQuerySubCategory.SelectedValue
                End If

                If DivQueryStatus.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("HD_STATUS_ID").InnerText = ddlQueryStatus.SelectedValue
                End If
                If DivSevirity.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("HD_SEVERITY_ID").InnerText = ddlQueryPriority.SelectedValue
                End If
                If DivCord1.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("COORDINATOR1").InnerText = ddlCoordinator1.SelectedValue
                End If
                If DivCord2.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("COORDINATOR2").InnerText = ddlCoordinator2.SelectedValue
                End If
                If DivDateAssign.Visible = True Then
                    If (Request.Form("txtDateAssigned") IsNot Nothing) Then
                        objInputXml.DocumentElement.SelectSingleNode("AssignedDatetime").InnerText = objeAAMS.GetDateFormat(Request.Form("txtDateAssigned"), "dd/MM/yyyy", "yyyyMMdd", "/")
                    End If
                End If
                If DivDispos.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("DISPOSITION_ID").InnerText = ddlDisposition.SelectedValue
                End If
                If (Request.Form("txtQueryOpenedDateFrom") IsNot Nothing) Then
                    objInputXml.DocumentElement.SelectSingleNode("HD_RE_OPEN_DATE_FROM").InnerText = objeAAMS.GetDateFormat(Request.Form("txtQueryOpenedDateFrom"), "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm", "/") ' objeAAMS.GetDateFormat(Request.Form("txtQueryOpenedDateFrom"), "dd/MM/yyyy HH:MM", "MM/dd/yyyy HH:MM", "/")
                End If
                If (Request.Form("txtQueryOpenedDateTo") IsNot Nothing) Then
                    objInputXml.DocumentElement.SelectSingleNode("HD_RE_OPEN_DATE_TO").InnerText = objeAAMS.GetDateFormat(Request.Form("txtQueryOpenedDateTo"), "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm", "/") ' objeAAMS.GetDateFormat(Request.Form("txtQueryOpenedDateTo"), "dd/MM/yyyy HH:MM", "MM/dd/yyyy HH:MM", "/")
                End If
                If (Request.Form("txtCloseDateFrom") IsNot Nothing) Then
                    objInputXml.DocumentElement.SelectSingleNode("HD_RE_CLOSED_DATE_FROM").InnerText = objeAAMS.GetDateFormat(Request.Form("txtCloseDateFrom"), "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm", "/") 'objeAAMS.GetDateFormat(Request.Form("txtCloseDateFrom"), "dd/MM/yyyy HH:MM", "MM/dd/yyyy HH:MM", "/")
                End If
                If (Request.Form("txtCloseDateTo") IsNot Nothing) Then
                    objInputXml.DocumentElement.SelectSingleNode("HD_RE_CLOSED_DATE_TO").InnerText = objeAAMS.GetDateFormat(Request.Form("txtCloseDateTo"), "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm", "/") ' objeAAMS.GetDateFormat(Request.Form("txtCloseDateTo"), "dd/MM/yyyy HH:MM", "MM/dd/yyyy HH:MM", "/")
                End If

                If Div1AOffice.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = ddlAOffice.SelectedValue
                End If
                If DivAgency1AOffice.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("AgencyAoffice").InnerText = ddlAgencyAOffice.SelectedValue
                End If
                objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = txtLTRNo.Text
                objInputXml.DocumentElement.SelectSingleNode("HD_PTR_REF").InnerText = txtPTRNo.Text
                If DivDispLastCall.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("LastCall").InnerText = IIf(chkDisplayLastCall.Checked, "True", "false")
                Else
                    objInputXml.DocumentElement.SelectSingleNode("LastCall").InnerText = "false"
                End If
                If DivWorkOrderNo.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("WO_ID").InnerText = txtWorkOrderNo.Text
                End If

                If DivAddress.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("Address").InnerText = txtAddresses.Text
                End If
                If DivFollowUp.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("FollowUp").InnerText = drpFollowup.SelectedValue
                End If
                If DivassigneTo.Visible = True Then
                    If drpAssignedTo.SelectedValue.Trim.Length > 0 Then
                        If drpAssignedTo.SelectedValue.Trim.Split("|")(0) = "1" Then
                            objInputXml.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = "1"
                            objInputXml.DocumentElement.SelectSingleNode("Assigned_To").InnerText = drpAssignedTo.SelectedValue.Trim.Split("|")(1)
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = "2"
                            objInputXml.DocumentElement.SelectSingleNode("Assigned_To").InnerText = drpAssignedTo.SelectedValue.Trim.Split("|")(1)
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Assigned_To").InnerText = "" 'drpAssignedTo.SelectedValue
                    End If
                End If
                If Not Session("Security") Is Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = objeAAMS.Limited_To_OwnAgency(Session("Security"))
                    objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = objeAAMS.Limited_To_Aoffice(Session("Security"))
                    objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = objeAAMS.Limited_To_Region(Session("Security"))
                End If
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0).ToString()

                If DivConType.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("CONTACTTYPE").InnerText = ddlContactType.SelectedValue
                End If

                If DivQueryCategTitle.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("TITLE").InnerText = txtTit.Text
                End If
                If DivAgencyStatus.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("ASTATUS").InnerText = dlstAgencyStatus.SelectedValue
                End If

                If DivAgencyType.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("ATYPE").InnerText = DlstAgencyType.SelectedValue
                End If
                If DivHD_IR_REF.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("HD_IR_REF").InnerText = txtHD_IR_REF.Text
                End If
                objInputXml.DocumentElement.SelectSingleNode("SetID").InnerText = drpSelectSet.SelectedValue

                ' Here Back end Method Call
                objOutputXml = objbzDMReportConfiguration.UPDATE_DYNAMIC_REPORT_REQUEST("I", Nothing, objInputXml)


                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    lblError.Text = "Data will be send soon."

                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "A1", "<script>alert('Data will be send soon.');</script>")
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            If Request("txtQueryOpenedDateFrom") IsNot Nothing Then
                txtQueryOpenedDateFrom.Text = Request("txtQueryOpenedDateFrom")
            End If
            If Request("txtQueryOpenedDateTo") IsNot Nothing Then
                txtQueryOpenedDateTo.Text = Request("txtQueryOpenedDateTo")
            End If

            If Request("txtCloseDateFrom") IsNot Nothing Then
                txtCloseDateFrom.Text = Request("txtCloseDateFrom")
            End If
            If Request("txtCloseDateTo") IsNot Nothing Then
                txtCloseDateTo.Text = Request("txtCloseDateTo")
            End If

        End Try
    End Sub


End Class

