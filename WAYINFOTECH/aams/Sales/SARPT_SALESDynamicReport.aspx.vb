
Partial Class Sales_SARPT_SALESDynamicReport
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

            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If Session("SALESDynamicReport") IsNot Nothing Then
                hdEmployeePageName.Value = Session("SALESDynamicReport")
            End If
            If (Request.QueryString("Reload") IsNot Nothing) Then
                Response.Redirect("SARPT_SALESDynamicReport.aspx", False)
                Exit Sub
            End If

            'Dim objSecurityXml As New XmlDocument
            'objSecurityXml.LoadXml(Session("Security"))
            'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Dynamic Report']").Count <> 0 Then
            '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Dynamic Report']").Attributes("Value").Value)

            '        If strBuilder(0) = "0" Then
            '            Response.Redirect("../NoRights.aspx")
            '            btnSearch.Enabled = False
            '            btnResultConfig.Enabled = False
            '            btnSearchConfigure.Enabled = False
            '            BtnRequestForReport.Enabled = False

            '        End If
            '        'If strBuilder(1) = "0" Then
            '        '    btnSave.Enabled = False
            '        'End If
            '    End If
            'Else
            '    strBuilder = objeAAMS.SecurityCheck(31)
            'End If
            hdToTime.Value = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm")
            hdFromTime.Value = System.DateTime.Now.Subtract(New TimeSpan(6, 0, 0)).ToString("dd/MM/yyyy HH:mm")


            GetSetId()
            If (Not IsPostBack) Then

                'btnSearch.Attributes.Add("onClick", "return ValidateForm();")
                ' btnSearch.Attributes.Add("onClick", "return CkeckValidateForm();")

                If Session("LoginSession") IsNot Nothing Then
                    Dim objInputXml, objOutputXml As New XmlDocument
                    'Dim objXmlReader As XmlNodeReader
                    Dim ds As New DataSet
                    Dim objbzDMReportConfiguration As New AAMS.bizHelpDesk.bzDMReportConfiguration

                    objInputXml.LoadXml("<SL_GETDMSEARCHCONFIGURE_INPUT><EmployeeID /><SetID/></SL_GETDMSEARCHCONFIGURE_INPUT>")
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
                    ' objOutputXml = objbzDMReportConfiguration.getConfiguredSearchField(objInputXml)

                    objOutputXml.Load("C:\SALES_DM\GetConfiguredSetCount.XML")

                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        objNode = objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='ADDRESS']")
                        If objNode IsNot Nothing Then
                            If objNode.Attributes("SELECTED").Value = "True" Then
                                'DivAddress.Visible = True
                            End If
                        End If

                        ' setDefaultValueBySetId(objOutputXml)

                    End If
                End If
                'LoadResultConfigure(Session("LoginSession").ToString().Split("|")(0).ToString)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub GetSetId()
        Dim objbzDMReportConfiguration As New AAMS.bizHelpDesk.bzDMReportConfiguration
        Dim objOutputXml As XmlDocument
        Dim ds As New DataSet
        objOutputXml = New XmlDocument
        Dim objInXml As New XmlDocument
        Dim count As Integer = 0
        Dim i As Integer
        drpSelectSet.Items.Clear()
        Try
            objInXml.LoadXml("<SL_GETDMCONFIGURESET_INPUT> <EmployeeID></EmployeeID></SL_GETDMCONFIGURESET_INPUT>")
            If Session("LoginSession") IsNot Nothing Then
                objInXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0).ToString

                'objOutputXml = objbzDMReportConfiguration.GetConfiguredSetCount(objInXml)

                objOutputXml.Load("C:\SALES_DM\GetConfiguredSetCount.XML")

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
    'Private Sub setDefaultValueBySetId(ByVal ObjOutxml As XmlDocument)
    '    Dim objNode As XmlNode
    '    Try

    '        If ObjOutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then


    '            'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='COMP_VERTICAL']")
    '            'If objNode IsNot Nothing Then

    '            '    If objNode.Attributes("SELECTED").Value = "True" Then
    '            '        Dim li As New ListItem
    '            '        li = DlstCompVertical.Items.FindByValue(objNode.Attributes("Value").Value)
    '            '        If li IsNot Nothing Then
    '            '            DlstCompVertical.SelectedValue = objNode.Attributes("Value").Value
    '            '        End If
    '            '    End If

    '            'End If


    '            '@ 
    '            'loadSubGroup()
    '            ' loadPriority()
    '            'loadQueryStatus()
    '            'loadAssignedTo()
    '            '@


    '            'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='QUERY_SUB_GROUP']")
    '            'If objNode IsNot Nothing Then
    '            '    If objNode.Attributes("SELECTED").Value = "True" Then
    '            '        Dim li As New ListItem
    '            '        li = ddlQuerySubGroup.Items.FindByValue(objNode.Attributes("Value").Value)
    '            '        If li IsNot Nothing Then
    '            '            ddlQuerySubGroup.SelectedValue = objNode.Attributes("Value").Value
    '            '        End If

    '            '    End If
    '            'End If

    '            ''@
    '            'loadCategory()
    '            ''@

    '            'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='QUERY_CATEGORY']")
    '            'If objNode IsNot Nothing Then
    '            '    If objNode.Attributes("SELECTED").Value = "True" Then
    '            '        Dim li As New ListItem
    '            '        li = ddlQueryCategory.Items.FindByValue(objNode.Attributes("Value").Value)
    '            '        If li IsNot Nothing Then
    '            '            ddlQueryCategory.SelectedValue = objNode.Attributes("Value").Value
    '            '        End If

    '            '    End If
    '            'End If
    '            ''@
    '            'loadSubCategory()
    '            ''@

    '            'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='QUERY_SUB_CATEGORY']")
    '            'If objNode IsNot Nothing Then
    '            '    If objNode.Attributes("SELECTED").Value = "True" Then
    '            '        Dim li As New ListItem
    '            '        li = ddlQuerySubCategory.Items.FindByValue(objNode.Attributes("Value").Value)
    '            '        If li IsNot Nothing Then
    '            '            ddlQuerySubCategory.SelectedValue = objNode.Attributes("Value").Value
    '            '        End If

    '            '    End If
    '            'End If

    '            'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='QUERY_STATUS']")
    '            'If objNode IsNot Nothing Then
    '            '    If objNode.Attributes("SELECTED").Value = "True" Then

    '            '        ' ddlQueryStatus.SelectedValue = "1"

    '            '        Dim li As New ListItem
    '            '        li = ddlQueryStatus.Items.FindByValue(objNode.Attributes("Value").Value)
    '            '        If li IsNot Nothing Then
    '            '            ddlQueryStatus.SelectedValue = objNode.Attributes("Value").Value
    '            '        End If

    '            '    End If
    '            'End If

    '            'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='FOLLOWUP']")

    '            'If objNode IsNot Nothing Then
    '            '    If objNode.Attributes("SELECTED").Value = "True" Then

    '            '        Dim li As New ListItem
    '            '        li = drpFollowup.Items.FindByValue(objNode.Attributes("Value").Value)
    '            '        If li IsNot Nothing Then
    '            '            drpFollowup.SelectedValue = objNode.Attributes("Value").Value
    '            '        End If

    '            '    End If
    '            'End If

    '            'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='SEVERITY']")

    '            'If objNode IsNot Nothing Then
    '            '    If objNode.Attributes("SELECTED").Value = "True" Then
    '            '        Dim li As New ListItem
    '            '        li = ddlQueryPriority.Items.FindByValue(objNode.Attributes("Value").Value)
    '            '        If li IsNot Nothing Then
    '            '            ddlQueryPriority.SelectedValue = objNode.Attributes("Value").Value
    '            '        End If

    '            '    End If
    '            'End If

    '            ''objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='TYPE']")
    '            ''If objNode.Attributes("DM_SEARCH_FIELD").Value = "True" Then
    '            ''    DivType.Visible = True
    '            ''End If

    '            'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='LOGGED_BY']")
    '            'If objNode IsNot Nothing Then
    '            '    If objNode.Attributes("SELECTED").Value = "True" Then

    '            '        txtLoggedBy.Text = objNode.Attributes("Value").Value
    '            '    End If
    '            'End If

    '            'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='ASSIGNED_TO']")
    '            'If objNode IsNot Nothing Then
    '            '    If objNode.Attributes("SELECTED").Value = "True" Then

    '            '        Dim li As New ListItem
    '            '        Dim strvalue As String = "1|" + objNode.Attributes("Value").Value

    '            '        li = drpAssignedTo.Items.FindByValue(strvalue)
    '            '        If li IsNot Nothing Then
    '            '            drpAssignedTo.SelectedValue = strvalue
    '            '        Else
    '            '            Dim li2 As New ListItem
    '            '            strvalue = ""
    '            '            strvalue = "2|" + objNode.Attributes("Value").Value
    '            '            li2 = drpAssignedTo.Items.FindByValue(strvalue)
    '            '            If li2 IsNot Nothing Then
    '            '                drpAssignedTo.SelectedValue = strvalue
    '            '            End If
    '            '        End If
    '            '        'Dim li As New ListItem
    '            '        'li = drpAssignedTo.Items.FindByValue(objNode.Attributes("Value").Value)
    '            '        'If li IsNot Nothing Then
    '            '        '    drpAssignedTo.SelectedValue = objNode.Attributes("Value").Value
    '            '        'End If
    '            '    End If
    '            'End If

    '            'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='ASSIGNED_DATE_TIME']")
    '            'If objNode IsNot Nothing Then
    '            '    If objNode.Attributes("SELECTED").Value = "True" Then
    '            '        txtDateAssigned.Text = objNode.Attributes("Value").Value
    '            '    End If
    '            'End If


    '            'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='OFFICE_ID']")

    '            'If objNode IsNot Nothing Then
    '            '    If objNode.Attributes("SELECTED").Value = "True" Then
    '            '        txtOfficeID.Text = objNode.Attributes("Value").Value
    '            '    End If
    '            'End If

    '            'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='CUSTOMER_CATEGORY']")
    '            'If objNode IsNot Nothing Then
    '            '    If objNode.Attributes("SELECTED").Value = "True" Then
    '            '        Dim li As New ListItem
    '            '        li = ddlCustomerCategory.Items.FindByValue(objNode.Attributes("Value").Value)
    '            '        If li IsNot Nothing Then
    '            '            ddlCustomerCategory.SelectedValue = objNode.Attributes("Value").Value
    '            '        End If
    '            '    End If
    '            'End If


    '            'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='CALLER_NAME']")

    '            'If objNode IsNot Nothing Then
    '            '    If objNode.Attributes("SELECTED").Value = "True" Then
    '            '        txtCallerName.Text = objNode.Attributes("Value").Value
    '            '    End If
    '            'End If

    '            ''objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='QUERY_PRIORITY']")
    '            ''If objNode.Attributes("SELECTED").Value = "True" Then
    '            ''    DivQueryPriority.Visible = True
    '            ''End If

    '            'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='COORDINATOR1']")
    '            'If objNode IsNot Nothing Then
    '            '    If objNode.Attributes("SELECTED").Value = "True" Then
    '            '        Dim li As New ListItem
    '            '        li = ddlCoordinator1.Items.FindByValue(objNode.Attributes("Value").Value)
    '            '        If li IsNot Nothing Then
    '            '            ddlCoordinator1.SelectedValue = objNode.Attributes("Value").Value
    '            '        End If

    '            '    End If
    '            'End If

    '            'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='COORDINATOR2']")
    '            'If objNode IsNot Nothing Then
    '            '    If objNode.Attributes("SELECTED").Value = "True" Then
    '            '        Dim li As New ListItem
    '            '        li = ddlCoordinator2.Items.FindByValue(objNode.Attributes("Value").Value)
    '            '        If li IsNot Nothing Then
    '            '            ddlCoordinator2.SelectedValue = objNode.Attributes("Value").Value
    '            '        End If

    '            '    End If
    '            'End If

    '            'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='DISPOSITION']")
    '            'If objNode IsNot Nothing Then
    '            '    If objNode.Attributes("SELECTED").Value = "True" Then
    '            '        Dim li As New ListItem
    '            '        li = ddlDisposition.Items.FindByValue(objNode.Attributes("Value").Value)
    '            '        If li IsNot Nothing Then
    '            '            ddlDisposition.SelectedValue = objNode.Attributes("Value").Value
    '            '        End If

    '            '    End If
    '            'End If

    '            'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='1AOFFICE']")
    '            'If objNode IsNot Nothing Then
    '            '    If objNode.Attributes("SELECTED").Value = "True" Then
    '            '        Dim li As New ListItem
    '            '        li = ddlAOffice.Items.FindByValue(objNode.Attributes("Value").Value)
    '            '        If li IsNot Nothing Then
    '            '            ddlAOffice.SelectedValue = objNode.Attributes("Value").Value
    '            '        End If

    '            '    End If
    '            'End If

    '            'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='AGENCY_1A_OFFICE']")

    '            'If objNode IsNot Nothing Then
    '            '    If objNode.Attributes("SELECTED").Value = "True" Then
    '            '        Dim li As New ListItem
    '            '        li = ddlAgencyAOffice.Items.FindByValue(objNode.Attributes("Value").Value)
    '            '        If li IsNot Nothing Then
    '            '            ddlAgencyAOffice.SelectedValue = objNode.Attributes("Value").Value
    '            '        End If

    '            '    End If
    '            'End If

    '            'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='WORK_ORDER_NO']")
    '            'If objNode IsNot Nothing Then
    '            '    If objNode.Attributes("SELECTED").Value = "True" Then
    '            '        txtWorkOrderNo.Text = objNode.Attributes("Value").Value
    '            '    End If
    '            'End If

    '            'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='LAST_CALL']")
    '            'If objNode IsNot Nothing Then
    '            '    If objNode.Attributes("SELECTED").Value = "True" Then
    '            '        If objNode.Attributes("Value").Value = "1" Then
    '            '            chkDisplayLastCall.Checked = True
    '            '            txtQueryOpenedDateFrom.Text = ""
    '            '            txtQueryOpenedDateTo.Text = ""
    '            '        Else
    '            '            chkDisplayLastCall.Checked = False
    '            '            If txtQueryOpenedDateFrom.Text = "" Then
    '            '                txtQueryOpenedDateFrom.Text = hdFromTime.Value

    '            '            End If
    '            '            If txtQueryOpenedDateTo.Text = "" Then
    '            '                txtQueryOpenedDateTo.Text = hdToTime.Value
    '            '            End If
    '            '            'hdToTime.Value = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm")
    '            '            'hdFromTime.Value = System.DateTime.Now.Subtract(New TimeSpan(6, 0, 0)).ToString("dd/MM/yyyy HH:mm")
    '            '        End If
    '            '    End If
    '            'End If

    '            'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='CONTACTTYPE']")
    '            'If objNode IsNot Nothing Then
    '            '    If objNode.Attributes("SELECTED").Value = "True" Then
    '            '        Dim li As New ListItem
    '            '        li = ddlContactType.Items.FindByValue(objNode.Attributes("Value").Value)
    '            '        If li IsNot Nothing Then
    '            '            ddlContactType.SelectedValue = objNode.Attributes("Value").Value
    '            '        End If
    '            '    End If
    '            'End If

    '            'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='TITLE']")
    '            'If objNode IsNot Nothing Then
    '            '    If objNode.Attributes("SELECTED").Value = "True" Then
    '            '        txtTit.Text = objNode.Attributes("Value").Value
    '            '    End If
    '            'End If
    '            'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='HD_IR_REF']")
    '            'If objNode IsNot Nothing Then
    '            '    If objNode.Attributes("SELECTED").Value = "True" Then
    '            '        txtHD_IR_REF.Text = objNode.Attributes("Value").Value
    '            '    End If
    '            'End If

    '            'objNode = ObjOutxml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='AIRLINE_NAME']") ' objOutputXml.DocumentElement.SelectSingleNode("FIELDDETAILS[@DM_FIELD_ID='AIRLINE']")
    '            'If objNode IsNot Nothing Then
    '            '    If objNode.Attributes("SELECTED").Value = "True" Then
    '            '        Dim li As New ListItem
    '            '        li = DlstAirLine.Items.FindByValue(objNode.Attributes("Value").Value)
    '            '        If li IsNot Nothing Then
    '            '            DlstAirLine.SelectedValue = objNode.Attributes("Value").Value
    '            '        End If
    '            '    End If
    '            'End If





    '        Else
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Sub loadSubGroup()
    '    'Code to fill Query Sub Group
    '    Dim objCallSubGroup As New AAMS.bizHelpDesk.bzCallSubGroup
    '    Dim objOutputXml As XmlDocument
    '    Dim objXmlReader As XmlNodeReader
    '    Dim ds As New DataSet
    '    objOutputXml = New XmlDocument
    '    Dim objInXml As New XmlDocument

    '    objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID /></HD_LISTCALLSUBGROUP_INPUT> ")
    '    objInXml.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = ddlQueryGroup.SelectedValue
    '    objOutputXml = objCallSubGroup.List1(objInXml)
    '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '        objXmlReader = New XmlNodeReader(objOutputXml)
    '        ds.ReadXml(objXmlReader)

    '        ddlQuerySubGroup.DataSource = Nothing
    '        ddlQuerySubGroup.DataBind()

    '        ddlQuerySubGroup.DataSource = ds.Tables("CALL_SUB_GROUP")
    '        ddlQuerySubGroup.DataTextField = "CALL_SUB_GROUP_NAME"
    '        ddlQuerySubGroup.DataValueField = "CALL_SUB_GROUP_ID"
    '        ddlQuerySubGroup.DataBind()
    '        ddlQuerySubGroup.Items.Insert(0, New ListItem("--All--", ""))
    '    Else
    '        ddlQuerySubGroup.Items.Clear()
    '        ddlQuerySubGroup.Items.Insert(0, New ListItem("--All--", ""))
    '    End If

    'End Sub
    'Sub loadQueryStatus()
    '    'Code to fill Query Status
    '    Dim objCallStatus As New AAMS.bizHelpDesk.bzCallStatus
    '    Dim objOutputXml1 As XmlDocument
    '    Dim objXmlReader1 As XmlNodeReader
    '    Dim ds1 As New DataSet
    '    objOutputXml1 = New XmlDocument
    '    Dim objInXml1 As New XmlDocument
    '    objOutputXml1 = New XmlDocument
    '    objInXml1.LoadXml("<HD_LISTCALLSTATUS_INPUT><HD_QUERY_GROUP_ID /></HD_LISTCALLSTATUS_INPUT> ")
    '    objInXml1.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = ddlQueryGroup.SelectedValue
    '    objOutputXml1 = objCallStatus.List1(objInXml1)
    '    If objOutputXml1.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '        objXmlReader1 = New XmlNodeReader(objOutputXml1)
    '        ds1.ReadXml(objXmlReader1)
    '        ddlQueryStatus.DataSource = Nothing
    '        ddlQueryStatus.DataBind()
    '        ddlQueryStatus.DataSource = ds1.Tables("CALL_STATUS")
    '        ddlQueryStatus.DataTextField = "HD_STATUS_NAME"
    '        ddlQueryStatus.DataValueField = "HD_STATUS_ID"
    '        ddlQueryStatus.DataBind()
    '        ddlQueryStatus.Items.Insert(0, New ListItem("--All--", ""))
    '    Else
    '        ddlQueryStatus.Items.Clear()
    '        ddlQueryStatus.Items.Insert(0, New ListItem("--All--", ""))
    '    End If

    'End Sub
    'Sub loadPriority()
    '    'Code to fill Query Priority
    '    Dim objSeverities As New AAMS.bizHelpDesk.bzSeverities
    '    Dim objOutputXml2 As XmlDocument
    '    Dim objXmlReader2 As XmlNodeReader
    '    Dim ds2 As New DataSet
    '    objOutputXml2 = New XmlDocument
    '    Dim objInXml2 As New XmlDocument
    '    objOutputXml2 = New XmlDocument
    '    objOutputXml2 = New XmlDocument
    '    objInXml2.LoadXml("<HD_LISTSEVERITIES_INPUT><HD_QUERY_GROUP_ID /></HD_LISTSEVERITIES_INPUT>")
    '    objInXml2.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = ddlQueryGroup.SelectedValue
    '    objOutputXml2 = objSeverities.List1(objInXml2)
    '    If objOutputXml2.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '        objXmlReader2 = New XmlNodeReader(objOutputXml2)
    '        ds2.ReadXml(objXmlReader2)
    '        ddlQueryPriority.DataSource = Nothing
    '        ddlQueryPriority.DataBind()
    '        ddlQueryPriority.DataSource = ds2.Tables("SEVERITIES")
    '        ddlQueryPriority.DataTextField = "HD_SEVERITY_NAME"
    '        ddlQueryPriority.DataValueField = "HD_SEVERITY_ID"
    '        ddlQueryPriority.DataBind()
    '        ddlQueryPriority.Items.Insert(0, New ListItem("--All--", ""))
    '    Else
    '        ddlQueryPriority.Items.Clear()
    '        ddlQueryPriority.Items.Insert(0, New ListItem("--All--", ""))
    '    End If
    'End Sub
    'Sub loadCategory()
    '    Dim objCallCategory As New AAMS.bizHelpDesk.bzCallCategory
    '    Dim objOutputXml As XmlDocument
    '    Dim objXmlReader As XmlNodeReader
    '    Dim ds As New DataSet
    '    objOutputXml = New XmlDocument
    '    Dim objInXml As New XmlDocument
    '    objInXml.LoadXml("<HD_LISTCALLCATEGORY_INPUT><CALL_SUB_GROUP_ID /></HD_LISTCALLCATEGORY_INPUT>")
    '    objInXml.DocumentElement.SelectSingleNode("CALL_SUB_GROUP_ID").InnerText = ddlQuerySubGroup.SelectedValue
    '    objOutputXml = objCallCategory.List1(objInXml)
    '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '        objXmlReader = New XmlNodeReader(objOutputXml)
    '        ds.ReadXml(objXmlReader)

    '        ddlQueryCategory.DataSource = Nothing
    '        ddlQueryCategory.DataBind()

    '        ddlQueryCategory.DataSource = ds.Tables("CALL_CATEGORY")
    '        ddlQueryCategory.DataTextField = "CALL_CATEGORY_NAME"
    '        ddlQueryCategory.DataValueField = "CALL_CATEGORY_ID"
    '        ddlQueryCategory.DataBind()
    '        ddlQueryCategory.Items.Insert(0, New ListItem("--All--", ""))
    '        ddlQuerySubCategory.Items.Clear()
    '        ddlQuerySubCategory.Items.Insert(0, New ListItem("--All--", ""))
    '    Else
    '        ddlQueryCategory.Items.Clear()
    '        ddlQueryCategory.Items.Insert(0, New ListItem("--All--", ""))
    '        ddlQuerySubCategory.Items.Clear()
    '        ddlQuerySubCategory.Items.Insert(0, New ListItem("--All--", ""))
    '    End If
    'End Sub
    'Sub loadSubCategory()
    '    Dim objCallSubCategory As New AAMS.bizHelpDesk.bzCallSubCategory
    '    Dim objOutputXml As XmlDocument
    '    Dim objXmlReader As XmlNodeReader
    '    Dim ds As New DataSet
    '    objOutputXml = New XmlDocument
    '    Dim objInXml As New XmlDocument
    '    objInXml.LoadXml("<HD_LISTCALLSUBCATEGORY_INPUT> <CALL_CATEGORY_ID /></HD_LISTCALLSUBCATEGORY_INPUT>")
    '    objInXml.DocumentElement.SelectSingleNode("CALL_CATEGORY_ID").InnerText = ddlQueryCategory.SelectedValue
    '    objOutputXml = objCallSubCategory.List1(objInXml)
    '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '        objXmlReader = New XmlNodeReader(objOutputXml)
    '        ds.ReadXml(objXmlReader)
    '        ddlQuerySubCategory.DataSource = Nothing
    '        ddlQuerySubCategory.DataBind()
    '        ddlQuerySubCategory.DataSource = ds.Tables("CALL_SUB_CATEGORY")
    '        ddlQuerySubCategory.DataTextField = "CALL_SUB_CATEGORY_NAME"
    '        ddlQuerySubCategory.DataValueField = "CALL_SUB_CATEGORY_ID"
    '        ddlQuerySubCategory.DataBind()
    '        ddlQuerySubCategory.Items.Insert(0, New ListItem("--All--", ""))
    '    Else
    '        ddlQuerySubCategory.Items.Clear()
    '        ddlQuerySubCategory.Items.Insert(0, New ListItem("--All--", ""))
    '    End If
    'End Sub


  
End Class
