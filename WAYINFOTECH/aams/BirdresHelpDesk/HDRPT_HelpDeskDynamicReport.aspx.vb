'########################################################################
'############   Page Name -- HDRPT_HelpDeskDynamicReport.aspx     #######
'############   Date 13-March 2007  #####################################
'############   Developed By Abhishek  ##################################
'########################################################################
Partial Class BirdresHelpDesk_HDRPT_HelpDeskDynamicReport
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Dynamic Report']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Dynamic Report']").Attributes("Value").Value)

                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnSearch.Enabled = False
                        btnResultConfig.Enabled = False
                        btnSearchConfigure.Enabled = False
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

                btnSearch.Attributes.Add("onClick", "return ValidateForm();")
                btnSearch.Attributes.Add("onClick", "return CkeckValidateForm();")

                objeAAMS.BindDropDown(ddlCustomerCategory, "BRCUSTOMERCATEGORY", True, 3)
                objeAAMS.BindDropDown(ddlAOffice, "BRAOFFICE", True, 3)
                objeAAMS.BindDropDown(ddlAgencyAOffice, "BRAOFFICE", True, 3)
                objeAAMS.BindDropDown(ddlDisposition, "BRDISPOSITION", True, 3)
                objeAAMS.BindDropDown(ddlCoordinator1, "BRCOORDINATOR1", True, 3)
                objeAAMS.BindDropDown(ddlCoordinator2, "BRCOORDINATOR2", True, 3)

                '  objeAAMS.BindDropDown(drpAssignedTo, "EMPLOYEE", True, 3)
                objeAAMS.BindDropDown(drpFollowup, "BRWorkOrderFollowUp", True, 3)
                objeAAMS.BindDropDown(ddlContactType, "BRCONTACTTYPE", True, 3)

                ' objeAAMS.BindDropDown(drpAssignedTo, "TEAM", True)
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
                    Dim objbzDMReportConfiguration As New AAMS.bizBRHelpDesk.bzDMReportConfiguration
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
        Dim objCallSubGroup As New AAMS.bizBRHelpDesk.bzCallSubGroup
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
        Dim objCallStatus As New AAMS.bizBRHelpDesk.bzCallStatus
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
        Dim objSeverities As New AAMS.bizBRHelpDesk.bzSeverities
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
        Dim objCallCategory As New AAMS.bizBRHelpDesk.bzCallCategory
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
        Dim objCallSubCategory As New AAMS.bizBRHelpDesk.bzCallSubCategory
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
        Dim objbzDMReportConfiguration As New AAMS.bizBRHelpDesk.bzDMReportConfiguration
        If Session("LoginSession") IsNot Nothing Then


            ' Dim strSEARCH_INPUT As String = "<HD_DMSEARCH_CALLREQUEST_INPUT><LCODE></LCODE><HD_RE_ID></HD_RE_ID><AgencyName></AgencyName><Address></Address><HD_PTR_REF></HD_PTR_REF><HD_STATUS_ID></HD_STATUS_ID><FollowUp></FollowUp><HD_SEVERITY_ID></HD_SEVERITY_ID><LoggedBy></LoggedBy><HD_RE_OPEN_DATE_FROM></HD_RE_OPEN_DATE_FROM><HD_RE_OPEN_DATE_TO></HD_RE_OPEN_DATE_TO><HD_RE_CLOSED_DATE_FROM></HD_RE_CLOSED_DATE_FROM><HD_RE_CLOSED_DATE_TO></HD_RE_CLOSED_DATE_TO><Assigned_To></Assigned_To><AssignedDatetime></AssignedDatetime><OfficeID></OfficeID><CUSTOMER_CATEGORY_ID></CUSTOMER_CATEGORY_ID><CALLER_NAME></CALLER_NAME><HD_QUERY_GROUP_ID></HD_QUERY_GROUP_ID><CALL_SUB_GROUP_ID></CALL_SUB_GROUP_ID><CALL_CATEGORY_ID></CALL_CATEGORY_ID><CALL_SUB_CATEGORY_ID></CALL_SUB_CATEGORY_ID><COORDINATOR1></COORDINATOR1><COORDINATOR2></COORDINATOR2><DISPOSITION_ID></DISPOSITION_ID><Aoffice></Aoffice><AgencyAoffice></AgencyAoffice><WO_ID></WO_ID><LastCall></LastCall><EmployeeID></EmployeeID><LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY><LIMITED_TO_AOFFICE></LIMITED_TO_AOFFICE><LIMITED_TO_REGION></LIMITED_TO_REGION><SetID></SetID><HD_DESCRIPTION>0</HD_DESCRIPTION><HD_SOLUTION>0</HD_SOLUTION></HD_DMSEARCH_CALLREQUEST_INPUT>"

            Dim strSEARCH_INPUT As String = "<HD_DMSEARCH_CALLREQUEST_INPUT><LCODE></LCODE><HD_RE_ID></HD_RE_ID><AgencyName></AgencyName><Address></Address><HD_PTR_REF></HD_PTR_REF><HD_STATUS_ID></HD_STATUS_ID><FollowUp></FollowUp><HD_SEVERITY_ID></HD_SEVERITY_ID><LoggedBy></LoggedBy><HD_RE_OPEN_DATE_FROM></HD_RE_OPEN_DATE_FROM><HD_RE_OPEN_DATE_TO></HD_RE_OPEN_DATE_TO><HD_RE_CLOSED_DATE_FROM></HD_RE_CLOSED_DATE_FROM><HD_RE_CLOSED_DATE_TO></HD_RE_CLOSED_DATE_TO><Assigned_To></Assigned_To><AssignedDatetime></AssignedDatetime><OfficeID></OfficeID><CUSTOMER_CATEGORY_ID></CUSTOMER_CATEGORY_ID><CALLER_NAME></CALLER_NAME><HD_QUERY_GROUP_ID></HD_QUERY_GROUP_ID><CALL_SUB_GROUP_ID></CALL_SUB_GROUP_ID><CALL_CATEGORY_ID></CALL_CATEGORY_ID><CALL_SUB_CATEGORY_ID></CALL_SUB_CATEGORY_ID><COORDINATOR1></COORDINATOR1><COORDINATOR2></COORDINATOR2><DISPOSITION_ID></DISPOSITION_ID><Aoffice></Aoffice><AgencyAoffice></AgencyAoffice><WO_ID></WO_ID><LastCall></LastCall><EmployeeID></EmployeeID><LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY><LIMITED_TO_AOFFICE></LIMITED_TO_AOFFICE><LIMITED_TO_REGION></LIMITED_TO_REGION><SetID></SetID><HD_DESCRIPTION>0</HD_DESCRIPTION><HD_SOLUTION>0</HD_SOLUTION><CONTACTTYPE></CONTACTTYPE><TITLE></TITLE></HD_DMSEARCH_CALLREQUEST_INPUT>"

            objInputXml.LoadXml(strSEARCH_INPUT)
            'objInputXml.LoadXml("<HD_SEARCH_CALLREQUEST_INPUT><LCODE /><AgencyName /><OfficeID /><CUSTOMER_CATEGORY_ID /><LoggedBy />" & _
            '"<CALLER_NAME /><HD_QUERY_GROUP_ID /><CALL_SUB_GROUP_ID /><CALL_CATEGORY_ID /><CALL_SUB_CATEGORY_ID /><HD_STATUS_ID /> " & _
            '"<HD_SEVERITY_ID /><COORDINATOR1 /><COORDINATOR2 /><AssignedDatetime /><DISPOSITION_ID /><HD_RE_OPEN_DATE_FROM /> " & _
            '"<HD_RE_OPEN_DATE_TO /><HD_RE_CLOSED_DATE_FROM /><HD_RE_CLOSED_DATE_TO /><Aoffice /><AgencyAoffice /><HD_RE_ID /><HD_PTR_REF /> " & _
            '"<LastCall /><WO_ID /><LIMITED_TO_OWNAAGENCY /><LIMITED_TO_AOFFICE /><LIMITED_TO_REGION /></HD_SEARCH_CALLREQUEST_INPUT>")
            'objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyName.Value
            'objInputXml.DocumentElement.SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text


            objInputXml.DocumentElement.SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text
            If (Request.Form("txtAgencyName") <> "" And hdAgencyName.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyName.Value = "") Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = ""
            Else
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyName.Value.Trim()
            End If
            objInputXml.DocumentElement.SelectSingleNode("OfficeID").InnerText = txtOfficeID.Text
            objInputXml.DocumentElement.SelectSingleNode("CUSTOMER_CATEGORY_ID").InnerText = ddlCustomerCategory.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("LoggedBy").InnerText = txtLoggedBy.Text
            objInputXml.DocumentElement.SelectSingleNode("CALLER_NAME").InnerText = txtCallerName.Text
            objInputXml.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = ddlQueryGroup.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_GROUP_ID").InnerText = ddlQuerySubGroup.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("CALL_CATEGORY_ID").InnerText = ddlQueryCategory.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY_ID").InnerText = ddlQuerySubCategory.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("HD_STATUS_ID").InnerText = ddlQueryStatus.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("HD_SEVERITY_ID").InnerText = ddlQueryPriority.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("COORDINATOR1").InnerText = ddlCoordinator1.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("COORDINATOR2").InnerText = ddlCoordinator2.SelectedValue
            If (Request.Form("txtDateAssigned") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("AssignedDatetime").InnerText = objeAAMS.GetDateFormat(Request.Form("txtDateAssigned"), "dd/MM/yyyy", "yyyyMMdd", "/")
            End If


            objInputXml.DocumentElement.SelectSingleNode("DISPOSITION_ID").InnerText = ddlDisposition.SelectedValue
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

            objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = ddlAOffice.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("AgencyAoffice").InnerText = ddlAgencyAOffice.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = txtLTRNo.Text
            objInputXml.DocumentElement.SelectSingleNode("HD_PTR_REF").InnerText = txtPTRNo.Text
            If DivDispLastCall.Visible = True Then
                objInputXml.DocumentElement.SelectSingleNode("LastCall").InnerText = IIf(chkDisplayLastCall.Checked, "True", "false")
            Else
                objInputXml.DocumentElement.SelectSingleNode("LastCall").InnerText = "false"
            End If

            objInputXml.DocumentElement.SelectSingleNode("WO_ID").InnerText = txtWorkOrderNo.Text

            objInputXml.DocumentElement.SelectSingleNode("Address").InnerText = txtAddresses.Text
            objInputXml.DocumentElement.SelectSingleNode("FollowUp").InnerText = drpFollowup.SelectedValue

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


            objInputXml.DocumentElement.SelectSingleNode("SetID").InnerText = drpSelectSet.SelectedValue


            ' Here Back end Method Call
            objOutputXml = objbzDMReportConfiguration.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                Session("BirdresHelpDeskDynamicReport") = objOutputXml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=BirdresHelpDeskDynamicReport", False)
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

            If Session("LoginSession") IsNot Nothing Then
                Dim objInputXml, objOutputXml As New XmlDocument
                'Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                Dim objbzDMReportConfiguration As New AAMS.bizBRHelpDesk.bzDMReportConfiguration
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

                End If
            End If


        Catch ex As Exception

        End Try
    End Sub
    Private Sub GetSetId()
        Dim objbzDMReportConfiguration As New AAMS.bizBRHelpDesk.bzDMReportConfiguration
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

            Dim objbzTeam As New AAMS.bizBRHelpDesk.bzTeam
            objOutputXml = New XmlDocument
            objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
            objOutputXml = objbzTeam.List()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                For Each objnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("TEAM")
                    drpAssignedTo.Items.Add(New ListItem(objnode.Attributes("TEAM_NAME").Value, "1" + "|" + objnode.Attributes("TEAM_ID").Value))
                Next
            End If

        ElseIf ddlQueryGroup.SelectedValue = "1" Then
            Dim objbzTeam As New AAMS.bizBRHelpDesk.bzTeam
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

End Class

