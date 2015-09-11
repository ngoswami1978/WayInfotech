
Partial Class BOHelpDesk_HDSR_CallLog
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilderFunctional As New StringBuilder
    Public strBuilderTechnical As New StringBuilder
    Dim str As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        Try

            ' txtSubCategoryName.Attributes.Add("onkeypress", "return allTextWithSpace();")
            'Dim m As ClientScriptManager = Me.ClientScript
            'str = m.GetCallbackEventReference(Me, "args", "ReceiveServerData", "'this is context from server'")
            'Dim strCallback As String = "function CallServer(args,context){" + str + ";}"
            'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)
            ''Code for Updating Sub Group
            'ddlQueryGroup.Attributes.Add("OnChange", "return fillSubGroup('ddlQueryGroup');")
            ''Code for Updating Category Name
            ''Dim strCallback1 As String = "function CallServer(args,context){" + str + ";}"
            ''Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback1, True)
            'ddlQuerySubGroup.Attributes.Add("OnChange", "return fillCategoryName('ddlQuerysubGroup');")
            ''Code for Updating Sub Category Name
            'ddlQueryCategory.Attributes.Add("OnChange", "return fillSubCategory('ddlQueryCategory');")
            'hdQueryCategory.Value = Request.Form(ddlQueryCategory.UniqueID)

            'If Request.QueryString("LCodeMuk") IsNot Nothing Then
            '    hdLcodeMuk.Value = Request.QueryString("LCodeMuk")
            'End If

            'If Request.QueryString("HD_RE_IDMuk") IsNot Nothing Then
            '    hdReIDMuk.Value = Request.QueryString("HD_RE_IDMuk")
            'End If


            hdToTime.Value = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm")
            hdFromTime.Value = System.DateTime.Now.Subtract(New TimeSpan(6, 0, 0)).ToString("dd/MM/yyyy HH:mm")
            hdPendingTime.Value = System.DateTime.Now.Subtract(New TimeSpan(30, 0, 0, 0)).ToString("dd/MM/yyyy HH:mm")

            'If chkDisplayLastCall.Checked = False Then
            '    If Request.Form("txtQueryOpenedDateFrom") = "" Then
            '        txtQueryOpenedDateFrom.Text = hdFromTime.Value
            '        txtQueryOpenedDateTo.Text = hdToTime.Value
            '    Else
            '        txtQueryOpenedDateFrom.Text = Request.Form("txtQueryOpenedDateFrom")
            '        txtQueryOpenedDateTo.Text = Request.Form("txtQueryOpenedDateTo")
            '    End If
            'Else
            '    txtQueryOpenedDateFrom.Text = ""
            '    txtQueryOpenedDateTo.Text = ""
            'End If

            txtCloseDateFrom1.Text = Request.Form("txtCloseDateFrom1")
            txtCloseDateTo1.Text = Request.Form("txtCloseDateTo1")
            txtDateAssigned.Text = Request.Form("txtDateAssigned")

            If hdDeleteId.Value <> "" Then
                DeleteRecords()
            End If
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If

            If Not Page.IsPostBack Then
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
                Session("DataSource") = Nothing
                btnSearch.Attributes.Add("onClick", "return ValidateFormCallLog();")
                objeAAMS.BindDropDown(ddlCustomerCategory, "BOCUSTOMERCATEGORY", True, 3)
                objeAAMS.BindDropDown(ddlContactType, "BOCONTACTTYPE", True, 3)
                objeAAMS.BindDropDown(ddlAOffice, "AOFFICE", True, 3)
                objeAAMS.BindDropDown(ddlAgencyAOffice, "AOFFICE", True, 3)
                objeAAMS.BindDropDown(ddlDisposition, "BODISPOSITION", True, 3)
                objeAAMS.BindDropDown(ddlCoordinator1, "BOCOORDINATOR1", True, 3)
                objeAAMS.BindDropDown(ddlCoordinator2, "BOCOORDINATOR2", True, 3)
                objeAAMS.BindDropDown(ddlState, "STATE", True, 3)
                objeAAMS.BindDropDown(ddlCity, "CITY", True, 3)
                objeAAMS.BindDropDown(ddlRegion, "REGION", True, 3)
                objeAAMS.BindDropDown(drpAirLineName, "AIRLINE", True, 3)
                objeAAMS.BindDropDown(DlstCompVertical, "CompanyVertical", True, 3)
                loadGroup()
                loadSubGroup()
                loadCategory()
                loadSubCategory()
                loadPriority()
                loadQueryStatus()
                ' This code is used for checking session handler according to user login.


                Dim objSecurityXml As New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Log HD Call']").Count <> 0 Then
                        strBuilderFunctional = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Log HD Call']").Attributes("Value").Value)
                    End If
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Log Tech Call']").Count <> 0 Then
                        strBuilderTechnical = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Log Tech Call']").Attributes("Value").Value)
                    End If

                    If strBuilderFunctional(0) = "0" And strBuilderTechnical(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnSearch.Enabled = False
                    End If
                    If strBuilderFunctional(1) = "0" And strBuilderTechnical(1) = "0" Then
                        btnNew.Enabled = False
                    End If
                    If strBuilderFunctional(4) = "0" And strBuilderTechnical(4) = "0" Then
                        btnExport.Enabled = False
                    End If
                Else
                    strBuilderFunctional = objeAAMS.SecurityCheck(31)
                End If
            End If

            '   Checking Permission For Own Office start.
            If objeAAMS.Limited_To_Aoffice(Session("Security")) <> "" Then
                ddlAgencyAOffice.SelectedValue = objeAAMS.Limited_To_Aoffice(Session("Security"))
                ddlAgencyAOffice.Enabled = False
            End If

            '   Checking Permission For Own Office end.
        Catch ex As Exception
            lblError.Text = ex.Message
            'LogWrite(gstrModule_name, lstrMETHOD_NAME, ex)
        End Try
    End Sub
    Sub loadGroup()
        Try
            If Session("Security") IsNot Nothing Then
                Dim objSecurityXml As New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Log HD Call']").Count <> 0 Then
                    strBuilderFunctional = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Log HD Call']").Attributes("Value").Value)
                End If
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Log Tech Call']").Count <> 0 Then
                    strBuilderTechnical = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Log Tech Call']").Attributes("Value").Value)
                End If
                ddlQueryGroup.Items.Clear()
                If strBuilderFunctional(0) <> "0" And strBuilderTechnical(0) <> "0" Then
                    ddlQueryGroup.Items.Insert(0, New ListItem("--All--", ""))
                    ddlQueryGroup.Items.Insert(1, New ListItem("Functional", "1"))
                    ddlQueryGroup.Items.Insert(2, New ListItem("Technical", "2"))
                End If

                If strBuilderFunctional(0) <> "0" And strBuilderTechnical(0) = "0" Then
                    ddlQueryGroup.Items.Insert(0, New ListItem("Functional", "1"))
                End If

                If strBuilderFunctional(0) = "0" And strBuilderTechnical(0) <> "0" Then
                    ddlQueryGroup.Items.Insert(0, New ListItem("Technical", "2"))
                End If

            End If
        Catch ex As Exception
            Throw ex
            'LogWrite(gstrModule_name, lstrMETHOD_NAME, ex)
        End Try
    End Sub
    Sub loadSubGroup()
        Try
            'Code to fill Query Sub Group
            Dim objCallSubGroup As New AAMS.bizBOHelpDesk.bzCallSubGroup
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
        Catch ex As Exception
            Throw ex
            'LogWrite(gstrModule_name, lstrMETHOD_NAME, ex)
        End Try
    End Sub

    Sub loadQueryStatus()
        Try


            'Code to fill Query Status
            Dim objCallStatus As New AAMS.bizBOHelpDesk.bzCallStatus
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
        Catch ex As Exception
            Throw ex
            'LogWrite(gstrModule_name, lstrMETHOD_NAME, ex)
        End Try
    End Sub


    Sub loadPriority()
        Try
            'Code to fill Query Priority
            Dim objSeverities As New AAMS.bizBOHelpDesk.bzSeverities
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
        Catch ex As Exception
            Throw ex
            'LogWrite(gstrModule_name, lstrMETHOD_NAME, ex)
        End Try
    End Sub
    Sub loadCategory()
        Try


            Dim objCallCategory As New AAMS.bizBOHelpDesk.bzCallCategory
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
        Catch ex As Exception
            Throw ex
            'LogWrite(gstrModule_name, lstrMETHOD_NAME, ex)
        End Try
    End Sub
    Sub loadSubCategory()
        Try
            Dim objCallSubCategory As New AAMS.bizBOHelpDesk.bzCallSubCategory
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
        Catch ex As Exception
            Throw ex
            'LogWrite(gstrModule_name, lstrMETHOD_NAME, ex)
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            SearchRecords(PageOperation.Search)
            'If chkDisplayLastCall.Checked = False Then
            txtQueryOpenedDateFrom.Text = Request.Form("txtQueryOpenedDateFrom")
            txtQueryOpenedDateTo.Text = Request.Form("txtQueryOpenedDateto")

            'If Not Request.QueryString("Popup") Is Nothing Then
            '    If gvCallLog.Rows.Count > 0 Then
            '        btnSelect.CssClass = "button bottomMargin"
            '        btnSelect.Visible = True
            '    End If
            'Else
            '    If gvCallLog.Rows.Count > 0 Then
            '        btnSelect.CssClass = "button bottomMargin displayNone"
            '        btnSelect.Visible = False
            '    End If
            'End If

            ' Else
            'txtQueryOpenedDateFrom.Text = ""

            'txtQueryOpenedDateTo.Text = ""
            'End If

        Catch ex As Exception
            lblError.Text = ex.Message
            'LogWrite(gstrModule_name, lstrMETHOD_NAME, ex)

        End Try

    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        ' Session("Action") = "I" & hdRowID.Value
        'If ddlQueryGroup.SelectedValue = "2" Then
        '    Response.Redirect("HDUP_CallLog.aspx?Action=I&strStatus=" + objED.Encrypt("Technical"))
        'Else
        Response.Redirect("BOHDUP_CallLog.aspx?Action=I")
        ' End If
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("BOHDSR_CallLog.aspx")
    End Sub

    Sub SearchRecords(ByVal Operation As Integer)
        Try


            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet


            Dim objbzCall As New AAMS.bizBOHelpDesk.bzCall
            objInputXml.LoadXml("<HD_SEARCH_CALLREQUEST_INPUT><LCODE /><AgencyName /><OfficeID /><CUSTOMER_CATEGORY_ID /><LoggedBy />" & _
            "<CALLER_NAME /><HD_QUERY_GROUP_ID /><CALL_SUB_GROUP_ID /><CALL_CATEGORY_ID /><CALL_SUB_CATEGORY_ID /><HD_STATUS_ID /> " & _
            "<HD_SEVERITY_ID /><COORDINATOR1 /><COORDINATOR2 /><AssignedDatetime /><DISPOSITION_ID /><HD_RE_OPEN_DATE_FROM /> " & _
            "<HD_RE_OPEN_DATE_TO /><HD_RE_CLOSED_DATE_FROM /><HD_RE_CLOSED_DATE_TO /><Aoffice /><AgencyAoffice /><HD_RE_ID /><HD_PTR_REF /> " & _
            "<LastCall /><WO_ID /><LIMITED_TO_OWNAAGENCY /><LIMITED_TO_AOFFICE /><LIMITED_TO_REGION /><EmployeeID /><CITY /><State /><IATA_TID /><ISIATA/>" & _
            "<Region /><IsPending /><LTRs /><ASSIGNEDTO /><CONTACT_TYPE_ID/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><TITLE/><AIRLINE_CODE/><HD_IR_REF></HD_IR_REF><COMP_VERTICAL></COMP_VERTICAL></HD_SEARCH_CALLREQUEST_INPUT>")

            'objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyName.Value


            If DlstCompVertical.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
            End If

            objInputXml.DocumentElement.SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text
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
            objInputXml.DocumentElement.SelectSingleNode("AssignedDatetime").InnerText = objeAAMS.GetDateFormat(Request.Form("txtDateAssigned"), "dd/MM/yyyy", "yyyyMMdd", "/")
            objInputXml.DocumentElement.SelectSingleNode("DISPOSITION_ID").InnerText = ddlDisposition.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("HD_RE_OPEN_DATE_FROM").InnerText = objeAAMS.GetDateFormat(Request.Form("txtQueryOpenedDateFrom"), "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm", "/")
            objInputXml.DocumentElement.SelectSingleNode("HD_RE_OPEN_DATE_TO").InnerText = objeAAMS.GetDateFormat(Request.Form("txtQueryOpenedDateTo"), "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm", "/")
            objInputXml.DocumentElement.SelectSingleNode("HD_RE_CLOSED_DATE_FROM").InnerText = objeAAMS.GetDateFormat(Request.Form("txtCloseDateFrom1"), "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm", "/")
            objInputXml.DocumentElement.SelectSingleNode("HD_RE_CLOSED_DATE_TO").InnerText = objeAAMS.GetDateFormat(Request.Form("txtCloseDateTo1"), "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm", "/")
            objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = ddlAOffice.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("AgencyAoffice").InnerText = ddlAgencyAOffice.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = txtLTRNo.Text
            objInputXml.DocumentElement.SelectSingleNode("HD_PTR_REF").InnerText = txtPTRNo.Text
            objInputXml.DocumentElement.SelectSingleNode("LastCall").InnerText = IIf(chkDisplayLastCall.Checked, "True", "false")
            objInputXml.DocumentElement.SelectSingleNode("WO_ID").InnerText = txtWorkOrderNo.Text
            If Not Session("Security") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = objeAAMS.Limited_To_OwnAgency(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = objeAAMS.Limited_To_Aoffice(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = objeAAMS.Limited_To_Region(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = objeAAMS.EmployeeID(Session("Security"))
            End If
            objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = IIf(ddlCity.SelectedItem.Text.ToUpper = "--ALL--", "", ddlCity.SelectedItem.Text)
            objInputXml.DocumentElement.SelectSingleNode("State").InnerText = ddlState.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("ISIATA").InnerText = chkISIATA.Checked
            objInputXml.DocumentElement.SelectSingleNode("IATA_TID").InnerText = txtIATA.Text
            objInputXml.DocumentElement.SelectSingleNode("IsPending").InnerText = chkISPending.Checked
            objInputXml.DocumentElement.SelectSingleNode("Region").InnerText = ddlRegion.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("ASSIGNEDTO").InnerText = txtAssignedTo.Text
            objInputXml.DocumentElement.SelectSingleNode("CONTACT_TYPE_ID").InnerText = ddlContactType.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("TITLE").InnerText = txtTitle.Text
            objInputXml.DocumentElement.SelectSingleNode("AIRLINE_CODE").InnerText = drpAirLineName.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("HD_IR_REF").InnerText = txtHD_IR_REF.Text


            '<CITY /><State /><IATA_TID /><ISIATA/> IsPending
            'Start CODE for sorting and paging
            If Operation = PageOperation.Search Then

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
                    ViewState("SortName") = "HD_RE_ID"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "HD_RE_ID"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
                End If

                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
                End If
            End If
            'End Code for paging and sorting
            ' Here Back end Method Call
            objOutputXml = objbzCall.Search(objInputXml)
            ' objOutputXml.Load("c:\staff.xml")
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Dim objSecurityXml As New XmlDocument
                    objSecurityXml.LoadXml(Session("Security"))
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                        If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Log HD Call']").Count <> 0 Then
                            strBuilderFunctional = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Log HD Call']").Attributes("Value").Value)
                        End If
                        If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Log Tech Call']").Count <> 0 Then
                            strBuilderTechnical = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Log Tech Call']").Attributes("Value").Value)
                        End If

                        If strBuilderFunctional(4) = "0" Then
                            Dim objnode As XmlNode
                            For Each objnode In objOutputXml.DocumentElement.SelectNodes("//CALLREQUEST[@HD_QUERY_GROUP_NAME='Functional' or @HD_QUERY_GROUP_NAME='FUNCTIONAL' or @HD_QUERY_GROUP_NAME='functional']")
                                objOutputXml.DocumentElement.RemoveChild(objnode)
                            Next

                            'HD_QUERY_GROUP_NAME
                        End If
                        If strBuilderTechnical(4) = "0" Then
                            Dim objnode As XmlNode
                            For Each objnode In objOutputXml.DocumentElement.SelectNodes("//CALLREQUEST[@HD_QUERY_GROUP_NAME='Technical' or @HD_QUERY_GROUP_NAME='TECHNICAL' or @HD_QUERY_GROUP_NAME='technical']")
                                objOutputXml.DocumentElement.RemoveChild(objnode)
                            Next
                        End If
                    End If
                    Export(objOutputXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvCallLog.DataSource = ds.Tables("CALLREQUEST")
                gvCallLog.DataBind()
                lblError.Text = ""
                'Code Added For Paging And Sorting In case Of Delete The Record
                pnlPaging.Visible = True
                Dim count As Integer = CInt(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
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
                hdRecordOnCurrentPage.Value = ds.Tables("CALLREQUEST").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' @ Added Code To Show Image'
                Dim imgUp As New Image
                imgUp.ImageUrl = "~/Images/Sortup.gif"
                Dim imgDown As New Image
                imgDown.ImageUrl = "~/Images/Sortdown.gif"

                Select Case ViewState("SortName")
                    Case "HD_RE_ID"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvCallLog.HeaderRow.Cells(1).Controls.Add(imgUp)
                            Case "TRUE"
                                gvCallLog.HeaderRow.Cells(1).Controls.Add(imgDown)
                        End Select
                    Case "OfficeID"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvCallLog.HeaderRow.Cells(2).Controls.Add(imgUp)
                            Case "TRUE"
                                gvCallLog.HeaderRow.Cells(2).Controls.Add(imgDown)
                        End Select
                    Case "AgencyName"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvCallLog.HeaderRow.Cells(3).Controls.Add(imgUp)
                            Case "TRUE"
                                gvCallLog.HeaderRow.Cells(3).Controls.Add(imgDown)

                        End Select

                    Case "COMP_VERTICAL"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvCallLog.HeaderRow.Cells(4).Controls.Add(imgUp)
                            Case "TRUE"
                                gvCallLog.HeaderRow.Cells(4).Controls.Add(imgDown)

                        End Select

                    Case "HD_RE_OPEN_DATE"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvCallLog.HeaderRow.Cells(5).Controls.Add(imgUp)
                            Case "TRUE"
                                gvCallLog.HeaderRow.Cells(5).Controls.Add(imgDown)
                        End Select

                    Case "HD_RE_CLOSED_DATE"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvCallLog.HeaderRow.Cells(6).Controls.Add(imgUp)
                            Case "TRUE"
                                gvCallLog.HeaderRow.Cells(6).Controls.Add(imgDown)
                        End Select
                    Case "LoggedBy"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvCallLog.HeaderRow.Cells(7).Controls.Add(imgUp)
                            Case "TRUE"
                                gvCallLog.HeaderRow.Cells(7).Controls.Add(imgDown)
                        End Select


                    Case "AssignedTo"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvCallLog.HeaderRow.Cells(8).Controls.Add(imgUp)
                            Case "TRUE"
                                gvCallLog.HeaderRow.Cells(8).Controls.Add(imgDown)
                        End Select
                    Case "HD_QUERY_GROUP_NAME"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvCallLog.HeaderRow.Cells(9).Controls.Add(imgUp)
                            Case "TRUE"
                                gvCallLog.HeaderRow.Cells(9).Controls.Add(imgDown)
                        End Select

                    Case "CALL_SUB_GROUP_NAME"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvCallLog.HeaderRow.Cells(10).Controls.Add(imgUp)
                            Case "TRUE"
                                gvCallLog.HeaderRow.Cells(10).Controls.Add(imgDown)
                        End Select

                    Case "CALL_CATEGORY_NAME"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvCallLog.HeaderRow.Cells(11).Controls.Add(imgUp)
                            Case "TRUE"
                                gvCallLog.HeaderRow.Cells(11).Controls.Add(imgDown)
                        End Select

                    Case "AssignedTo"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvCallLog.HeaderRow.Cells(12).Controls.Add(imgUp)
                            Case "TRUE"
                                gvCallLog.HeaderRow.Cells(12).Controls.Add(imgDown)
                        End Select

                    Case "LEFTDATETIME"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvCallLog.HeaderRow.Cells(13).Controls.Add(imgUp)
                            Case "TRUE"
                                gvCallLog.HeaderRow.Cells(13).Controls.Add(imgDown)
                        End Select

                    Case "HD_STATUS_NAME"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvCallLog.HeaderRow.Cells(14).Controls.Add(imgUp)
                            Case "TRUE"
                                gvCallLog.HeaderRow.Cells(14).Controls.Add(imgDown)
                        End Select
                    Case "RepeatCalls"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvCallLog.HeaderRow.Cells(15).Controls.Add(imgUp)
                            Case "TRUE"
                                gvCallLog.HeaderRow.Cells(15).Controls.Add(imgDown)
                        End Select

                    Case "HD_RE_ID_CHILD"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvCallLog.HeaderRow.Cells(16).Controls.Add(imgUp)
                            Case "TRUE"
                                gvCallLog.HeaderRow.Cells(16).Controls.Add(imgDown)
                        End Select
                    Case "AOffice"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvCallLog.HeaderRow.Cells(17).Controls.Add(imgUp)
                            Case "TRUE"
                                gvCallLog.HeaderRow.Cells(17).Controls.Add(imgDown)
                        End Select

                    Case "TITLE"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvCallLog.HeaderRow.Cells(18).Controls.Add(imgUp)
                            Case "TRUE"
                                gvCallLog.HeaderRow.Cells(18).Controls.Add(imgDown)
                        End Select

                    Case "COORDINATOR1"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvCallLog.HeaderRow.Cells(19).Controls.Add(imgUp)
                            Case "TRUE"
                                gvCallLog.HeaderRow.Cells(19).Controls.Add(imgDown)
                        End Select

                    Case "COORDINATOR2"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvCallLog.HeaderRow.Cells(20).Controls.Add(imgUp)
                            Case "TRUE"
                                gvCallLog.HeaderRow.Cells(20).Controls.Add(imgDown)
                        End Select

                    Case "AIRLINE_NAME"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvCallLog.HeaderRow.Cells(21).Controls.Add(imgUp)
                            Case "TRUE"
                                gvCallLog.HeaderRow.Cells(21).Controls.Add(imgDown)
                        End Select
                    Case "LAST_ASSIGNEDTO"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvCallLog.HeaderRow.Cells(22).Controls.Add(imgUp)
                            Case "TRUE"
                                gvCallLog.HeaderRow.Cells(22).Controls.Add(imgDown)
                        End Select

                End Select

                '  Added Code To Show Image'

                ' End of Code Added For Paging And Sorting In case Of Delete The Record
            Else
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                gvCallLog.DataSource = Nothing
                gvCallLog.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

            If Not Request.QueryString("Popup") Is Nothing Then
                If gvCallLog.Rows.Count > 0 Then
                    btnSelect.CssClass = "button bottomMargin"
                    btnSelect.Visible = True
                    btnEdit.Visible = False
                    btnChangeStatus.Visible = False
                End If
            Else
                If gvCallLog.Rows.Count > 0 Then
                    btnSelect.CssClass = "button bottomMargin displayNone"
                    btnSelect.Visible = False
                    btnEdit.Visible = True
                    btnChangeStatus.Visible = True
                Else
                    btnEdit.Visible = False
                    btnChangeStatus.Visible = False
                End If
            End If
        Catch ex As Exception
            Throw ex
            'LogWrite(gstrModule_name, lstrMETHOD_NAME, ex)
        End Try
    End Sub

    Sub DeleteRecords()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbzCall As New AAMS.bizBOHelpDesk.bzCall
            objInputXml.LoadXml("<HD_DELETE_CALLREQUEST_INPUT><HD_RE_ID></HD_RE_ID></HD_DELETE_CALLREQUEST_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = objED.Decrypt(hdDeleteId.Value)
            hdDeleteId.Value = ""
            objOutputXml = objbzCall.Delete(objInputXml)
            SearchRecords(PageOperation.Search)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messDelete
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            Throw ex
            'LogWrite(gstrModule_name, lstrMETHOD_NAME, ex)
        End Try
    End Sub

    Protected Sub gvCallLog_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCallLog.RowDataBound
        If e.Row.RowIndex < 0 Then
            If Request.QueryString("Popup") Is Nothing Then
                'e.Row.Cells(0).CssClass = "displayNone"
            End If
            Exit Sub
        End If
        e.Row.Attributes.Add("style", "background-color: " + DataBinder.Eval(e.Row.DataItem, "COLOR_CODE"))
        ' Dim hdHD_RE_ID As HiddenField
        'Dim hdLCode As HiddenField
        Dim btnEdit As LinkButton
        Dim btnRelog As LinkButton
        Dim btnDelete As LinkButton
        '   Dim lnkSelect As LinkButton
        btnEdit = CType(e.Row.FindControl("lnkEdit"), LinkButton)
        btnRelog = CType(e.Row.FindControl("lnkRelog"), LinkButton)
        btnDelete = CType(e.Row.FindControl("lnkDelete"), LinkButton)
        '  lnkSelect = CType(e.Row.FindControl("lnkSelect"), LinkButton)
        '   hdHD_RE_ID = CType(e.Row.FindControl("hdHD_RE_ID"), HiddenField)
        '  hdLCode = CType(e.Row.FindControl("hdLCode"), HiddenField)
        Dim strStatus As String = DataBinder.Eval(e.Row.DataItem, "HD_QUERY_GROUP_NAME") '  e.Row.Cells(5).Text.Trim
        Dim strHD_RE_ID As String = DataBinder.Eval(e.Row.DataItem, "HD_RE_ID")
        Dim strLCode As String = DataBinder.Eval(e.Row.DataItem, "LCode")
        Dim EnstrStatus As String = objED.Encrypt(strStatus)
        Dim EnstrhdHD_RE_ID As String = objED.Encrypt(strHD_RE_ID)
        Dim EnstrhdLCode As String = objED.Encrypt(strLCode)

        'As told by Tapan on 27 jan 09
        Dim strHD_RE_CLOSED_DATE As String = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "HD_RE_CLOSED_DATE"))
        If strHD_RE_CLOSED_DATE.Trim = "" Then
            e.Row.Cells(8).Text = ""
        End If

        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Log HD Call']").Count <> 0 Then
                strBuilderFunctional = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Log HD Call']").Attributes("Value").Value)
            End If
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Log Tech Call']").Count <> 0 Then
                strBuilderTechnical = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Log Tech Call']").Attributes("Value").Value)
            End If
            If ddlQueryGroup.SelectedValue = "1" Or ddlQueryGroup.SelectedValue = "" Then
                If strStatus.ToUpper.Trim = "FUNCTIONAL" Then
                    If strBuilderFunctional(3) = "0" Then
                        btnDelete.Enabled = False
                    Else
                        btnDelete.Attributes.Add("OnClick", "javascript:return DeleteFunctionCallLog('" + EnstrhdHD_RE_ID + "','" + EnstrStatus + "');")
                    End If
                    'If strBuilderFunctional(2) = "0" Then
                    '    btnEdit.Enabled = False
                    'Else

                    'End If
                    If strBuilderFunctional(1) = "0" Then
                        btnRelog.Enabled = False
                    Else
                        btnRelog.Attributes.Add("OnClick", "javascript:return ReLogFunctionCallLog('" + EnstrhdLCode + "','" + EnstrStatus + "');")
                    End If
                End If
                btnEdit.Attributes.Add("OnClick", "javascript:return EditFunctionCallLog('" + EnstrhdLCode + "','" + EnstrhdHD_RE_ID + "','" + EnstrStatus + "');")

            End If
            If ddlQueryGroup.SelectedValue = "2" Or ddlQueryGroup.SelectedValue = "" Then
                If strStatus.ToUpper.Trim = "TECHNICAL" Then
                    If strBuilderTechnical(3) = "0" Then
                        btnDelete.Enabled = False
                    Else
                        btnDelete.Attributes.Add("OnClick", "javascript:return DeleteFunctionCallLog('" + EnstrhdHD_RE_ID + "','" + EnstrStatus + "');")
                    End If

                    'If strBuilderFunctional(2) = "0" Then
                    '    btnEdit.Enabled = False
                    'Else

                    'End If
                    If strBuilderTechnical(1) = "0" Then
                        btnRelog.Enabled = False
                    Else
                        btnRelog.Attributes.Add("OnClick", "javascript:return ReLogFunctionCallLog('" + EnstrhdLCode + "','" + EnstrStatus + "');")
                    End If
                End If
                btnEdit.Attributes.Add("OnClick", "javascript:return EditFunctionCallLog('" + EnstrhdLCode + "','" + EnstrhdHD_RE_ID + "','" + EnstrStatus + "');")
            End If

        Else
            btnDelete.Attributes.Add("OnClick", "javascript:return DeleteFunctionCallLog('" + EnstrhdHD_RE_ID + "','" + EnstrStatus + "');")
            btnEdit.Attributes.Add("OnClick", "javascript:return EditFunctionCallLog('" + EnstrhdLCode + "','" + EnstrhdHD_RE_ID + "','" + EnstrStatus + "');")
            btnRelog.Attributes.Add("OnClick", "javascript:return ReLogFunctionCallLog('" + EnstrhdLCode + "','" + EnstrStatus + "');")
        End If


        '  <asp:BoundField HeaderText="OfficeID" DataField="OfficeID" />
        '<asp:BoundField HeaderText="Agency Name" DataField="OfficeID"/>
        '      <asp:BoundField HeaderText="Opened Date" DataField="HD_RE_OPEN_DATE"/>
        '                <asp:BoundField HeaderText="Call Logged By" DataField="LoggedBy"/>
        '                               <asp:BoundField HeaderText="Query Group" DataField="HD_QUERY_GROUP_NAME"/>
        '                           <asp:BoundField HeaderText="Status" DataField="HD_STHD_QUERY_GROUP_NAMEATUS_NAME"/>
        If Not Request.QueryString("Popup") Is Nothing Then
            '   btnSelect.Attributes.Add("OnClick", "javascript:return SelectFunction('" + hdHD_RE_ID.Value + "','" + strStatus + "','" + DataBinder.Eval(e.Row.DataItem, "OfficeID") + "','" + DataBinder.Eval(e.Row.DataItem, "AgencyName") + "','" + DataBinder.Eval(e.Row.DataItem, "HD_RE_OPEN_DATE") + "','" + DataBinder.Eval(e.Row.DataItem, "LoggedBy") + "','" + DataBinder.Eval(e.Row.DataItem, "HD_QUERY_GROUP_NAME") + "','" + DataBinder.Eval(e.Row.DataItem, "HD_STATUS_NAME") + "');")
            'lnkSelect.Visible = False
        Else
            'e.Row.Cells(0).CssClass = "displayNone"
            'e.Row.Cells(0).Controls.Clear()
            'lnkSelect.Visible = False
        End If

    End Sub

    Protected Sub ddlQueryGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlQueryGroup.SelectedIndexChanged
        Try
            txtTotalRecordCount.Text = "0"
            hdRecordOnCurrentPage.Value = "0"
            pnlPaging.Visible = False
            lblError.Text = ""
            loadSubGroup()
            loadCategory()
            loadSubCategory()
            loadPriority()
            loadQueryStatus()
            btnEdit.Visible = False
            btnChangeStatus.Visible = False
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
        '    'Code to fill Query Sub Group
        '    If ddlQueryGroup.SelectedValue <> "" Then
        '        Dim objCallSubGroup As New AAMS.bizBOHelpDesk.bzCallSubGroup
        '        Dim objOutputXml As XmlDocument
        '        Dim objXmlReader As XmlNodeReader
        '        Dim ds As New DataSet
        '        objOutputXml = New XmlDocument
        '        Dim objInXml As New XmlDocument
        '        objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID /></HD_LISTCALLSUBGROUP_INPUT> ")
        '        objInXml.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = ddlQueryGroup.SelectedValue
        '        objOutputXml = objCallSubGroup.List1(objInXml)
        '        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
        '            objXmlReader = New XmlNodeReader(objOutputXml)
        '            ds.ReadXml(objXmlReader)
        '            ddlQuerySubGroup.DataSource = ds.Tables("CALL_SUB_GROUP")
        '            ddlQuerySubGroup.DataTextField = "CALL_SUB_GROUP_NAME"
        '            ddlQuerySubGroup.DataValueField = "CALL_SUB_GROUP_ID"
        '            ddlQuerySubGroup.DataBind()
        '            ddlQuerySubGroup.Items.Insert(0,  New ListItem("All", ""))
        '        Else
        '            ddlQuerySubGroup.Items.Clear()
        '            ddlQuerySubGroup.Items.Insert(0,  New ListItem("All", ""))
        '        End If
        '        'Code End

        '        'Code to fill Query Status
        '        Dim objCallStatus As New AAMS.bizBOHelpDesk.bzCallStatus
        '        Dim objOutputXml1 As XmlDocument
        '        Dim objXmlReader1 As XmlNodeReader
        '        Dim ds1 As New DataSet
        '        objOutputXml1 = New XmlDocument
        '        Dim objInXml1 As New XmlDocument
        '        objOutputXml1 = New XmlDocument
        '        objInXml1.LoadXml("<HD_LISTCALLSTATUS_INPUT><HD_QUERY_GROUP_ID /></HD_LISTCALLSTATUS_INPUT> ")
        '        objInXml1.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = ddlQueryGroup.SelectedValue
        '        objOutputXml1 = objCallStatus.List1(objInXml1)
        '        If objOutputXml1.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
        '            objXmlReader1 = New XmlNodeReader(objOutputXml1)
        '            ds1.ReadXml(objXmlReader1)
        '            ddlQueryStatus.DataSource = ds1.Tables("CALL_STATUS")
        '            ddlQueryStatus.DataTextField = "HD_STATUS_NAME"
        '            ddlQueryStatus.DataValueField = "HD_STATUS_ID"
        '            ddlQueryStatus.DataBind()
        '            ddlQueryStatus.Items.Insert(0,  New ListItem("All", ""))
        '        Else
        '            ddlQueryStatus.Items.Clear()
        '            ddlQueryStatus.Items.Insert(0,  New ListItem("All", ""))
        '        End If
        '        'Code End

        '        'Code to fill Query Priority
        '        Dim objSeverities As New AAMS.bizBOHelpDesk.bzSeverities
        '        Dim objOutputXml2 As XmlDocument
        '        Dim objXmlReader2 As XmlNodeReader
        '        Dim ds2 As New DataSet
        '        objOutputXml2 = New XmlDocument
        '        Dim objInXml2 As New XmlDocument
        '        objOutputXml2 = New XmlDocument
        '        objOutputXml2 = New XmlDocument
        '        objInXml2.LoadXml("<HD_LISTSEVERITIES_INPUT><HD_QUERY_GROUP_ID /></HD_LISTSEVERITIES_INPUT>")
        '        objInXml2.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = ddlQueryGroup.SelectedValue
        '        objOutputXml2 = objSeverities.List1(objInXml2)
        '        If objOutputXml2.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
        '            objXmlReader2 = New XmlNodeReader(objOutputXml2)
        '            ds2.ReadXml(objXmlReader2)
        '            ddlQueryPriority.DataSource = ds2.Tables("SEVERITIES")
        '            ddlQueryPriority.DataTextField = "HD_SEVERITY_NAME"
        '            ddlQueryPriority.DataValueField = "HD_SEVERITY_ID"
        '            ddlQueryPriority.DataBind()
        '            ddlQueryPriority.Items.Insert(0,  New ListItem("All", ""))
        '        Else
        '            ddlQueryPriority.Items.Clear()
        '            ddlQueryPriority.Items.Insert(0,  New ListItem("All", ""))
        '        End If
        '    Else
        '        loadSubGroup()
        '        loadCategory()
        '        loadSubCategory()
        '        loadPriority()
        '        loadQueryStatus()
        '    End If
        '    'Code End

    End Sub

    Protected Sub ddlQuerySubGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlQuerySubGroup.SelectedIndexChanged
        Try
            txtTotalRecordCount.Text = "0"
            hdRecordOnCurrentPage.Value = "0"
            pnlPaging.Visible = False
            lblError.Text = ""
            loadCategory()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
        'Try
        '    'Code to fill Query Category
        '    If ddlQuerySubGroup.SelectedValue <> "" Then
        '        Dim objCallCategory As New AAMS.bizBOHelpDesk.bzCallCategory
        '        Dim objOutputXml As XmlDocument
        '        Dim objXmlReader As XmlNodeReader
        '        Dim ds As New DataSet
        '        objOutputXml = New XmlDocument
        '        Dim objInXml As New XmlDocument
        '        'objInXml.LoadXml("<HD_SEARCHCALLCATEGORY_INPUT><CALL_CATEGORY_NAME /><CALL_SUB_GROUP_ID /><HD_QUERY_GROUP_ID /></HD_SEARCHCALLCATEGORY_INPUT>")
        '        'objInXml.DocumentElement.SelectSingleNode("CALL_SUB_GROUP_ID").InnerText = ddlQuerySubGroup.SelectedValue
        '        'objInXml.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = ddlQueryGroup.SelectedValue
        '        'objOutputXml = objCallCategory.Search(objInXml)
        '        objInXml.LoadXml("<HD_LISTCALLCATEGORY_INPUT><CALL_SUB_GROUP_ID /></HD_LISTCALLCATEGORY_INPUT>")
        '        objInXml.DocumentElement.SelectSingleNode("CALL_SUB_GROUP_ID").InnerText = ddlQuerySubGroup.SelectedValue
        '        objOutputXml = objCallCategory.List1(objInXml)
        '        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
        '            objXmlReader = New XmlNodeReader(objOutputXml)
        '            ds.ReadXml(objXmlReader)
        '            ddlQueryCategory.DataSource = ds.Tables("CALL_CATEGORY")
        '            ddlQueryCategory.DataTextField = "CALL_CATEGORY_NAME"
        '            ddlQueryCategory.DataValueField = "CALL_CATEGORY_ID"
        '            ddlQueryCategory.DataBind()
        '            ddlQueryCategory.Items.Insert(0,  New ListItem("All", ""))
        '        Else
        '            ddlQueryCategory.Items.Clear()
        '            ddlQueryCategory.Items.Insert(0,  New ListItem("All", ""))
        '        End If
        '    Else
        '        loadCategory()
        '    End If
        'Catch ex As Exception
        '    lblError.Text = ex.Message
        'End Try
    End Sub

    Protected Sub ddlQueryCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlQueryCategory.SelectedIndexChanged
        Try
            txtTotalRecordCount.Text = "0"
            hdRecordOnCurrentPage.Value = "0"
            pnlPaging.Visible = False
            lblError.Text = ""
            loadSubCategory()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
        'Try
        '    'Code to fill Query Sub Category
        '    If ddlQueryCategory.SelectedValue <> "" Then
        '        Dim objCallSubCategory As New AAMS.bizBOHelpDesk.bzCallSubCategory
        '        Dim objOutputXml As XmlDocument
        '        Dim objXmlReader As XmlNodeReader
        '        Dim ds As New DataSet
        '        objOutputXml = New XmlDocument
        '        Dim objInXml As New XmlDocument
        '        objInXml.LoadXml("<HD_SEARCHCALLSUBCATEGORY_INPUT><CALL_SUB_CATEGORY_NAME /><CALL_CATEGORY_ID /><CALL_SUB_GROUP_ID /><HD_QUERY_GROUP_ID /></HD_SEARCHCALLSUBCATEGORY_INPUT>")
        '        objInXml.DocumentElement.SelectSingleNode("CALL_SUB_GROUP_ID").InnerText = ddlQuerySubGroup.SelectedValue
        '        objInXml.DocumentElement.SelectSingleNode("CALL_CATEGORY_ID").InnerText = ddlQueryCategory.SelectedValue
        '        objInXml.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = ddlQueryGroup.SelectedValue
        '        objOutputXml = objCallSubCategory.Search(objInXml)
        '        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
        '            objXmlReader = New XmlNodeReader(objOutputXml)
        '            ds.ReadXml(objXmlReader)
        '            ddlQuerySubCategory.DataSource = ds.Tables("CALL_SUB_CATEGORY")
        '            ddlQuerySubCategory.DataTextField = "CALL_SUB_CATEGORY_NAME"
        '            ddlQuerySubCategory.DataValueField = "CALL_SUB_CATEGORY_ID"
        '            ddlQuerySubCategory.DataBind()
        '            ddlQuerySubCategory.Items.Insert(0,  New ListItem("All", ""))
        '        Else
        '            ddlQuerySubCategory.Items.Clear()
        '            ddlQuerySubCategory.Items.Insert(0,  New ListItem("All", ""))
        '        End If
        '    Else
        '        loadSubCategory()
        '    End If
        'Catch ex As Exception
        '    lblError.Text = ex.Message
        'End Try
    End Sub

    Protected Sub gvCallLog_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCallLog.PageIndexChanged
        Try

            'Dim currentPage As Integer = gvCallLog.PageIndex + 1
            'lblError.Text = ""
            'gvCallLog.PageIndex = currentPage
            'If Session("DataSource") IsNot Nothing Then
            '    gvCallLog.DataSource = CType(Session("DataSource"), DataTable)
            '    gvCallLog.DataBind()
            'End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gvCallLog_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvCallLog.PageIndexChanging

        Try
            lblError.Text = ""
            gvCallLog.PageIndex = e.NewPageIndex
            If Session("DataSource") IsNot Nothing Then
                gvCallLog.DataSource = CType(Session("DataSource"), DataTable)
                gvCallLog.DataBind()
            End If

        Catch ex As Exception

        End Try
    End Sub

#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            SearchRecords(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            SearchRecords(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            SearchRecords(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub



    Protected Sub gvCallLog_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCallLog.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvCallLog_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvCallLog.Sorting
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
            SearchRecords(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            SearchRecords(PageOperation.Export)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    Enum PageOperation
        Search = 1
        Export = 2
    End Enum
    Sub Export(ByVal objOutputXml As XmlDocument)
        Dim objExport As New ExportExcel
        '    <CALLREQUEST HD_RE_ID="" LCODE="" OfficeID="" AgencyName="  " HD_RE_OPEN_DATE="
        'HD_RE_CLOSED_DATE="" LoggedBy="" HD_QUERY_GROUP_NAME="" HD_STATUS_NAME=" " COLOR_CODE="" />
        'Old
        'Dim strArray() As String = {"LTR No", "OfficeID", "Agency Name", "Opened Date", "Closed Date", "Call Logged By", "Call Closed By", "Query Group", "Query Sub Group", "Query Category", "Assigned To", "Left Date Time", "Status", "ReptCalls", "Linked LTR", "AOffice", "Title", "Co-Ordinator1", "Co-Ordinator2", "Airline Name"}
        'Dim intArray() As Integer = {0, 2, 3, 4, 10, 5, 9, 6, 7, 11, 12, 13, 14, 15, 16, 17}

        'Dim strArray() As String = {"LTR No", "OfficeID", "Agency Name", "Opened Date", "Closed Date", "Call Logged By", "Call Closed By", "Query Group", "Query Sub Group", "Query Category", "Assigned To", "Left Date Time", "Status", "Rept Calls", "Linked LTR", "AOffice", "Title", "Coordinator1", "Coordinator2", "AirlineName"}
        'Dim intArray() As Integer = {0, 2, 3, 4, 10, 5, 9, 6, 18, 19, 9, 20, 7, 11, 12, 13, 14, 15, 16, 17}
        Dim strArray() As String = {"LTR No", "OfficeID", "Agency Name", "Company Vertical", "Opened Date", "Closed Date", "Call Logged By", "Call Closed By", "Query Group", "Query Sub Group", "Query Category", "Assigned To", "Left Date Time", "Status", "Rept Calls", "Linked LTR", "AOffice", "Title", "Coordinator1", "Coordinator2", "AirlineName"}
        Dim intArray() As Integer = {0, 2, 3, 22, 4, 10, 5, 9, 6, 18, 19, 9, 20, 7, 11, 12, 13, 14, 15, 16, 17}
        objExport.ExportDetails(objOutputXml, "CALLREQUEST", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportCALLLOG.xls")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                '  Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
End Class
