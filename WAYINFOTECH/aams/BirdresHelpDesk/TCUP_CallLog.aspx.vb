Imports System.IO
Imports System.Text
Partial Class BirdresHelpDesk_TCUP_CallLog
    Inherits System.Web.UI.Page
    Implements ICallbackEventHandler

    'Inherits BasePage
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Protected strIndex As String
    Protected flagPermission As String
    Dim str As String
    Dim objED As New EncyrptDeCyrpt
    Shared startTime As TimeSpan


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl




        Try
            If hdTabType.Value = "" Then
                hdTabType.Value = 0
            Else
                strIndex = hdTabType.Value
            End If
            'If Request.QueryString("TabType") IsNot Nothing Then
            '    hdTabType.Value = Request.QueryString("TabType")
            'End If



            btnSave.Enabled = True

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            hdQueryString.Value = Request.QueryString.ToString
            txtAgencyName.Text = Request.Form("txtAgencyName")
            txtAddress.Text = Request.Form("txtAddress")
            txtCity.Text = Request.Form("txtCity")
            txtCountry.Text = Request.Form("txtCountry")
            txtPhone.Text = Request.Form("txtPhone")
            txtOfficeId.Text = Request.Form("txtOfficeId")
            txtFax.Text = Request.Form("txtFax")
            txtOnlineStatus.Text = Request.Form("txtOnlineStatus")
            txtPincode.Text = Request.Form("txtPincode")
            txtEmail.Text = Request.Form("txtEmail")
            checkSecurity()

            'code for officeId

            Dim m As ClientScriptManager = Me.ClientScript
            str = m.GetCallbackEventReference(Me, "args", "BRReceiveServerDataTech", "'this is context from server'")
            Dim strCallback As String = "function BRCallServerTech(args,context){" + str + ";}"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)
            'txtOfficeId.Attributes.Add("onfocusout", "return FillBRAgencyDetailsTech()")
            txtOfficeId.Attributes.Add("onblur", "return FillBRAgencyDetailsTech()")



            ' code end

            'Dim strBuilder As New StringBuilder
            'Dim objSecurityXml As New XmlDocument
            'objSecurityXml.LoadXml(Session("Security"))
            'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Log HD Call']").Count <> 0 Then
            '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Log HD Call']").Attributes("Value").Value)
            '    End If
            '    If strBuilder(0) = "0" Then
            '        btnSave.Enabled = False
            '    End If
            '    If strBuilder(1) = "0" Then
            '        btnNew.Enabled = False
            '    End If
            'Else
            '    strBuilder = objeAAMS.SecurityCheck(31)
            'End If
            'Dim m As ClientScriptManager = Me.ClientScript
            'str = m.GetCallbackEventReference(Me, "args", "ReceiveServerData", "'this is context from server'")
            'Dim strCallback As String = "function CallServer(args,context){" + str + ";}"
            'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)
            ddlQuerySubGroup.Attributes.Add("OnChange", "return fillBRCategoryTechnical();")
            ddlQueryCategory.Attributes.Add("OnChange", "return fillBRSubCategoryTechnical();")
            'hdQueryCategory.Value = Request.Form(
            txtLeftDateTime.Text = Request.Form("txtLeftDateTime")
            txtCloseDateTime.Text = Request.Form("txtCloseDateTime")
            If Request.QueryString("Popup") Is Nothing Then
                lnkClose.Visible = False
            Else
                lnkClose.Visible = True
            End If
            If hdReSave.Value = "1" Then
                SaveInformation(PageOperation.ReSave)
            End If
            If Not Page.IsPostBack Then

                startTime = DateTime.Now.TimeOfDay


                hdEnTechnical.Value = objED.Encrypt("Technical") 'Storing encrypted value in hiddenfield 
                If Request.QueryString("TabType") IsNot Nothing Then
                    hdTabType.Value = Request.QueryString("TabType")
                End If

                If Request.QueryString("QueryGroup") IsNot Nothing Then
                    Dim strQueryString As String = Request.QueryString.ToString

                    '@ Code Added By Abhishek
                    If Request.QueryString("Popup") Is Nothing Then
                        Response.Redirect("HDUP_helpDeskFeedBack.aspx?" + strQueryString)
                    Else

                        Response.Redirect("HDUP_helpDeskFeedBack.aspx?Popup=p" + strQueryString)
                    End If
                    '@ End of Code Added By Abhishek

                    '@ Previous Code

                    ' Response.Redirect("HDUP_helpDeskFeedBack.aspx?" + strQueryString)
                    '@ End of Previous Code

                End If


                If Not Request.QueryString("strStatus") Is Nothing Then
                    Dim EnstrStatus As String
                    EnstrStatus = objED.Decrypt(Request.QueryString("strStatus"))
                    If Request.QueryString("strStatus").ToString.ToUpper = "FUNCTIONAL" Then
                        Dim strQueryString As String = Request.QueryString.ToString
                        Response.Redirect("HDUP_CallLog.aspx?" + strQueryString)
                    End If
                End If
                If Not Request.QueryString("Action") Is Nothing Then
                    hdPageStatus.Value = Request.QueryString("Action").ToString
                End If
                If Not Request.QueryString("LCode") Is Nothing Then
                    hdEnPageLCode.Value = Request.QueryString("LCode").ToString
                    hdPageLCode.Value = objED.Decrypt(Request.QueryString("LCode").ToString)
                    If Request.QueryString("HD_RE_ID") Is Nothing Then
                        AgencyView()
                    End If
                End If
                If Not Request.QueryString("LCodeTemp") Is Nothing Then
                    hdEnPageLCode.Value = Request.QueryString("LCodeTemp").ToString
                    hdPageLCode.Value = objED.Decrypt(Request.QueryString("LCodeTemp").ToString)
                    AgencyView()
                End If


                If Not Request.QueryString("HD_RE_ID") Is Nothing Then
                    hdEnPageHD_RE_ID.Value = Request.QueryString("HD_RE_ID").ToString
                    hdPageHD_RE_ID.Value = objED.Decrypt(Request.QueryString("HD_RE_ID").ToString)
                    rdFunctional.Enabled = False
                Else
                    btnAssigneeHistory.Enabled = False
                    btnHistory.Enabled = False
                    btnFeedBack.Enabled = False
                End If

                LoadAllControl()
                Bindata()
                If hdPageHD_RE_ID.Value <> "" Then
                    ViewRecords()
                    txtCallerName.Focus()
                Else

                    'Code Added on 23rd 
                    If Session("Security") IsNot Nothing Then
                        Dim xDoc As New XmlDocument
                        xDoc.LoadXml(Session("Security"))
                        txtLoggedBy.Text = xDoc.DocumentElement.SelectSingleNode("Login").InnerText    '"admin"


                        '  txtOfficeId.Focus()
                        Dim li As New ListItem
                        li = ddlCoordinator1.Items.FindByValue(objeAAMS.EmployeeID(Session("Security")))
                        If li IsNot Nothing Then
                            ddlCoordinator1.SelectedValue = li.Value
                        Else
                            li = New ListItem
                            li.Value = objeAAMS.EmployeeID(Session("Security"))
                            li.Text = xDoc.DocumentElement.SelectSingleNode("Employee_Name").InnerText
                            ddlCoordinator1.Items.Add(li)
                            ddlCoordinator1.SelectedValue = li.Value
                            ' AddCoordinator()
                        End If
                    End If
                End If
            End If

            'code to hid and show popup image
            If hdPageHD_RE_ID.Value <> "" Then
                Img2.Visible = False
            Else
                Img2.Visible = True
            End If
            'End

            '***************************************************************************************
            'Code of Security Check
            'objeAAMS.ExpirePageCache()


        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub

    Sub checkSecurity()
        Dim strBuilderFunctional As New StringBuilder
        Dim strBuilderTechnical As New StringBuilder
        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Log Tech Call']").Count <> 0 Then
                strBuilderTechnical = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Log Tech Call']").Attributes("Value").Value)
            End If
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Log HD Call']").Count <> 0 Then
                strBuilderFunctional = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Log HD Call']").Attributes("Value").Value)
            End If
            'When View rights disabled
            If strBuilderTechnical(0) = "0" Then
                Response.Redirect("../NoRights.aspx")
                btnSave.Enabled = False
            End If
            If strBuilderFunctional(0) = "0" Then
                rdFunctional.Enabled = False
            End If
            'When Add rights disabled
            If strBuilderTechnical(1) = "0" Then
                btnNew.Enabled = False
                btnSave.Enabled = False
            End If
            'When modify rights disabled and Add rights enabled
            If strBuilderTechnical(2) = "0" And (hdPageHD_RE_ID.Value <> "" Or Request.QueryString("HD_RE_ID") IsNot Nothing) Then
                btnSave.Enabled = False
            End If
            'When modify rights Enabled and Add rights disabled
            If strBuilderTechnical(1) = "0" And strBuilderTechnical(2) <> "0" And (hdPageHD_RE_ID.Value <> "" Or Request.QueryString("HD_RE_ID") IsNot Nothing) Then
                btnSave.Enabled = True
            End If
            If objeAAMS.ReturnViewPermission(Session("Security"), "BR Feedback Action") = "0" Then
                btnFeedBack.Enabled = False
            End If
        Else
            strBuilderTechnical = objeAAMS.SecurityCheck(31)
        End If
    End Sub

    Sub LoadAllControl()
        Try
            objeAAMS.BindDropDown(ddlQuerySubGroup, "BRTQUERYSUBGROUP", True, 1)
            objeAAMS.BindDropDown(ddlQueryStatus, "BRTSTATUSWITHCLOSED", True, 1)
            objeAAMS.BindDropDown(ddlPriority, "BRTPRIORITY", True, 1)
            objeAAMS.BindDropDown(ddlTeamAssignedTo, "ASSIGNEDTOEMPLOYEE", True, 1)
            objeAAMS.BindDropDown(ddlContactType, "BRCONTACTTYPE", True, 1)

            'Code to fill Query Category
            Dim objCallCategory As New AAMS.bizBRHelpDesk.bzCallCategory
            Dim objCallCategory1 As New AAMS.bizBRHelpDesk.bzDisposition

            Dim objOutputXml As XmlDocument
            objOutputXml = New XmlDocument
            Dim objInXml As New XmlDocument
            objInXml.LoadXml("<HD_SEARCHCALLCATEGORY_INPUT><CALL_CATEGORY_NAME /><CALL_SUB_GROUP_ID /><HD_QUERY_GROUP_ID /><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHCALLCATEGORY_INPUT>")
            ' objInXml.LoadXml("<HD_SEARCHCALLCATEGORY_INPUT><CALL_CATEGORY_NAME /><CALL_SUB_GROUP_ID /><HD_QUERY_GROUP_ID /></HD_SEARCHCALLCATEGORY_INPUT>")
            objInXml.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = "2"
            objOutputXml = objCallCategory.SearchNew(objInXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Dim objErrorNode As XmlNode
                objErrorNode = objOutputXml.DocumentElement.SelectSingleNode("Errors")
                If objErrorNode IsNot Nothing Then
                    objOutputXml.DocumentElement.RemoveChild(objErrorNode)
                End If
                hdCategory.Value = objOutputXml.OuterXml
            End If

            '<HD_CC><CC CCI='' CCN='' CSGN='' /><Errors Status=''><Error Code='' Description='' /></Errors></HD_CC>
            '<HD_CSC> <CSC CSCI='' CSCN='' CCN='' CSGN='' /> <Errors Status=''><Error Code='' Description='' /></Errors></HD_CSC>

            'Code to fill Query Sub Category
            Dim objCallSubCategory As New AAMS.bizBRHelpDesk.bzCallSubCategory
            Dim objOutputXml1 As XmlDocument
            objOutputXml1 = New XmlDocument
            Dim objInXml1 As New XmlDocument
            objInXml1.LoadXml("<HD_SEARCHCALLSUBCATEGORY_INPUT><CALL_SUB_CATEGORY_NAME /><CALL_CATEGORY_ID /><CALL_SUB_GROUP_ID /><HD_QUERY_GROUP_ID /></HD_SEARCHCALLSUBCATEGORY_INPUT>")
            objInXml1.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = "2"
            objOutputXml1 = objCallSubCategory.SearchNew(objInXml1)
            If objOutputXml1.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Dim objErrorNode As XmlNode
                objErrorNode = objOutputXml1.DocumentElement.SelectSingleNode("Errors")
                If objErrorNode IsNot Nothing Then
                    objOutputXml1.DocumentElement.RemoveChild(objErrorNode)
                End If
                hdSubCategory.Value = objOutputXml1.OuterXml
            End If

            'Dim objInputXml As New XmlDocument
            'Dim objXmlReader As XmlNodeReader
            'Dim objbzCoordinator As New AAMS.bizBRHelpDesk.bzCoordinator
            'Dim ds As New DataSet
            'Dim ds1 As New DataSet
            'objInputXml.LoadXml("<HD_SEARCHCOORDINATOR_INPUT><COORDINATOR_TYPE></COORDINATOR_TYPE><EMPLOYEE_NAME /><Aoffice /><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHCOORDINATOR_INPUT>")
            'objInputXml.DocumentElement.SelectSingleNode("COORDINATOR_TYPE").InnerText = "1"
            'objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = objeAAMS.AOffice(Session("Security"))
            'objOutputXml = objbzCoordinator.Search(objInputXml)
            'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '    objXmlReader = New XmlNodeReader(objOutputXml)
            '    ds.ReadXml(objXmlReader)
            '    ddlCoordinator1.DataSource = ds.Tables("COORDINATOR")
            '    ddlCoordinator1.DataTextField = "EMPLOYEE_NAME"
            '    ddlCoordinator1.DataValueField = "EmployeeID"
            '    ddlCoordinator1.DataBind()
            '    ddlCoordinator1.Items.Insert(0, New ListItem("", ""))
            'End If
            FillCoordinator()
            ViewCallLogInsertDetails()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Private Sub FillCoordinator()
        Dim objInputXml As New XmlDocument
        Dim objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim objbzCoordinator As New AAMS.bizBRHelpDesk.bzCoordinator
        Dim ds As New DataSet
        Dim ds1 As New DataSet
        objInputXml.LoadXml("<HD_SEARCHCOORDINATOR_INPUT><COORDINATOR_TYPE></COORDINATOR_TYPE><EMPLOYEE_NAME /><Aoffice /><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHCOORDINATOR_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("COORDINATOR_TYPE").InnerText = "1"
        objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = objeAAMS.AOffice(Session("Security"))
        objOutputXml = objbzCoordinator.Search(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            ddlCoordinator1.Items.Clear()
            ddlCoordinator1.DataSource = ds.Tables("COORDINATOR")
            ddlCoordinator1.DataTextField = "EMPLOYEE_NAME"
            ddlCoordinator1.DataValueField = "EmployeeID"
            ddlCoordinator1.DataBind()
            ddlCoordinator1.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub

    Sub Bindata()
        Dim TabText As New ArrayList()
        Try
            TabText.Add("Call")
            TabText.Add("Description")
            TabText.Add("Solution")
            If Request.QueryString("Popup") Is Nothing Then
                TabText.Add("Linked LTR")
                If hdFeedBackPresence.Value = "1" Then
                    TabText.Add("FeedBack")
                End If
            End If
            'If lblError.Text.Trim() = objeAAMSMessage.messInsert Then
            '    Dim objSecurityXml As New XmlDocument
            '    Dim strBuilder As New StringBuilder
            '    objSecurityXml.LoadXml(Session("Security"))
            '    If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            '        If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee']").Count <> 0 Then
            '            strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee']").Attributes("Value").Value)
            '            If strBuilder(2) = "0" Then
            '                btnSave.Enabled = False
            '                Session("Action") = "I"
            '                ' hideTabStrip("0")
            '                strIndex = "0"
            '            End If
            '        End If
            '    Else
            '        strBuilder = objeAAMS.SecurityCheck(31)
            '    End If
            'End If


            theTabStrip.DataSource = TabText
            theTabStrip.DataBind()
            '   hideTabStrip(strIndex)

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Protected Sub theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound
        Dim Button1 As Button
        Button1 = e.Item.FindControl("Button1")
        If e.Item.ItemIndex = 0 Then
            Button1.CssClass = "headingtab"
        End If
        Button1 = e.Item.FindControl("Button1")

        If Session("Security") IsNot Nothing Then
            Dim strButtonText As String
            strButtonText = Button1.Text
            Select Case strButtonText
                Case "Call"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "BR Log Tech Call") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Description"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "BR Log Tech Call") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Solution"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "BR Log Tech Call") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Linked LTR"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "BR Linked HD Call") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If

                Case "FeedBack"
                    'If objeAAMS.ReturnViewPermission(Session("Security"), "Feedback Action") = "0" Then
                    Button1.CssClass = "displayNone"
                    'End If
            End Select
        End If


        If hdPageHD_RE_ID.Value = "" Then
            If e.Item.ItemIndex = 3 Or e.Item.ItemIndex = 4 Then
                Button1.Visible = False
            End If
        End If

        If Request.QueryString("Popup") Is Nothing Then
            If hdPageHD_RE_ID.Value = "" Then
                Button1.Attributes.Add("onclick", "return ColorMethod('" & Button1.ClientID.ToString() & "',3);")
            Else
                Button1.Attributes.Add("onclick", "return ColorMethod('" & Button1.ClientID.ToString() & "',4);")
            End If
        Else
            Button1.Attributes.Add("onclick", "return ColorMethod('" & Button1.ClientID.ToString() & "',3);")
        End If
    End Sub


    '**************************************************************************************************************
    'Save Employee Information
    Sub SaveInformation(ByVal Operation As Integer)
        Dim strQueryStatus As String
        strQueryStatus = ddlQueryStatus.SelectedValue
        If strQueryStatus.Split("|").Length <> 2 Then
            lblError.Text = "Query Category is mandatory"
            Exit Sub
        End If
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzCall As New AAMS.bizBRHelpDesk.bzCall
        Try
            objInputXml.LoadXml("<HD_UPDATE_CALLREQUEST_INPUT><CALLREQUEST HD_QUERY_GROUP_ID='' LCODE ='' HD_RE_ID='' CALLER_NAME='' CALL_SUB_GROUP_ID='' CALL_CATEGORY_ID='' CALL_SUB_CATEGORY_ID='' CALL_DURATION=''  HD_SEVERITY_ID='' HD_STATUS_ID='' COORDINATOR1='' COORDINATOR2='' HD_PTR_REF='' TT_NUMBAR='' HD_RE_BDR_LETTER='' WO_ID='' CONTACT_TYPE_ID='' DISPOSITION_ID='' AssignedTo='' LoggedBy='' LeftDateTime='' DESCRIPTION='' SOLUTION='' FU_UP_CONTACT_TYPE_ID='' FOLLOWUP_DESC='' FOLLOWUP_DATETIME='' HD_STATUS_CLOSE='' RESAVE='' HD_RE_CLOSED_DATE='' TITLE='' /></HD_UPDATE_CALLREQUEST_INPUT>")
            With objInputXml.DocumentElement.SelectSingleNode("CALLREQUEST")
                .Attributes("HD_QUERY_GROUP_ID").Value = "2"
                .Attributes("LCODE").Value = hdCallAgencyName.Value.Split("|").GetValue(0)
                .Attributes("HD_RE_ID").Value = hdPageHD_RE_ID.Value
                .Attributes("CALLER_NAME").Value = txtCallerName.Text
                .Attributes("CALL_SUB_GROUP_ID").Value = ddlQuerySubGroup.SelectedValue

                If Request.Form(ddlQueryCategory.UniqueID) = "" Or Request.Form(ddlQueryCategory.UniqueID) = "--Select One--" Then
                    .Attributes("CALL_CATEGORY_ID").Value = hdQueryCategory.Value
                Else
                    .Attributes("CALL_CATEGORY_ID").Value = Request.Form(ddlQueryCategory.UniqueID)
                    hdQueryCategory.Value = Request.Form(ddlQueryCategory.UniqueID)
                End If

                If Request.Form(ddlQuerySubCategory.UniqueID) = "" AndAlso hdSubCategoryMandatory.Value = "1" Or Request.Form(ddlQuerySubCategory.UniqueID) = "--Select One--" Then
                    .Attributes("CALL_SUB_CATEGORY_ID").Value = hdQuerySubCategory.Value
                Else
                    .Attributes("CALL_SUB_CATEGORY_ID").Value = Request.Form(ddlQuerySubCategory.UniqueID)
                    hdQuerySubCategory.Value = Request.Form(ddlQuerySubCategory.UniqueID)
                End If



                'Dim strCallDuration As String = txtCallDuration.Text.Trim
                'Dim strCallDuration1 As String = txtCallDuration1.Text.Trim

                'If strCallDuration.Length = 0 And strCallDuration1.Length <> 0 Then
                '    If strCallDuration1.Length >= 1 Then
                '        strCallDuration = "00"
                '    End If
                'End If

                'If strCallDuration.Length = 1 Then
                '    strCallDuration = "0" & strCallDuration
                'End If
                'If strCallDuration1.Length = 1 Then
                '    strCallDuration1 = "0" & strCallDuration1
                'End If

                Dim tSpan, resultTime As TimeSpan

                Dim strCallDuration As String = ""
                Dim strCallDuration1 As String = ""


                If txtCallDuration.Text.Trim.Length = 0 And txtCallDuration1.Text.Trim.Length = 0 Then
                    tSpan = DateTime.Now.TimeOfDay
                    resultTime = tSpan.Subtract(startTime)
                    'Formatting for Hours
                    If resultTime.Hours > 0 Then
                        If resultTime.Hours < 10 Then
                            strCallDuration = "0" & resultTime.Hours.ToString()
                        Else
                            strCallDuration = resultTime.Hours.ToString()
                        End If
                    Else
                        strCallDuration = "00"
                    End If
                    'Formatting for Hours


                    'Formatting for Minutes

                    If resultTime.Minutes > 0 Then

                        If resultTime.Minutes < 10 Then
                            strCallDuration1 = "0" & resultTime.Minutes.ToString()
                        Else
                            strCallDuration1 = resultTime.Minutes.ToString()
                        End If

                        If resultTime.Seconds > 30 Then
                            Dim minCalc As Integer = Convert.ToInt32(strCallDuration1) + 1
                            strCallDuration1 = minCalc.ToString()
                        End If

                        If strCallDuration1.Trim().Length = 1 Then
                            strCallDuration1 = "0" + strCallDuration1.ToString()
                        End If

                    Else
                        strCallDuration1 = "01"
                    End If

                    'Formatting for Minutes

                Else
                    strCallDuration = txtCallDuration.Text.Trim
                    ' Dim strCallDuration1 As String
                    strCallDuration1 = txtCallDuration1.Text.Trim

                    If txtCallDuration1.Text.Trim.Length = 0 Then
                        strCallDuration1 = "00"
                    End If

                    If txtCallDuration.Text.Trim.Length = 0 Then
                        strCallDuration = "00"
                    End If
                    ' If strCallDuration.Length = 0 And strCallDuration1.Length <> 0 Then
                    'If strCallDuration1.Length >= 1 Then
                    '    strCallDuration = "00"
                    'End If
                    ' End If

                    If strCallDuration.Length = 1 Then
                        strCallDuration = "0" & strCallDuration
                    End If
                    If strCallDuration1.Length = 1 Then
                        strCallDuration1 = "0" & strCallDuration1
                    End If

                End If

                'Code Added by Mukund


                .Attributes("CALL_DURATION").Value = strCallDuration & strCallDuration1
                .Attributes("HD_SEVERITY_ID").Value = ddlPriority.SelectedValue
                .Attributes("HD_STATUS_ID").Value = strQueryStatus.Split("|").GetValue(0)
                .Attributes("HD_STATUS_CLOSE").Value = strQueryStatus.Split("|").GetValue(1)
                .Attributes("COORDINATOR1").Value = ddlCoordinator1.SelectedValue
                .Attributes("COORDINATOR2").Value = ""
                ' .Attributes("HD_PTR_REF").Value = txtTTNo.Text
                .Attributes("TT_NUMBAR").Value = txtTTNo.Text
                '  .Attributes("HD_RE_BDR_LETTER").Value = txtBDRLetterID.Text
                ' .Attributes("WO_ID").Value = txtWorkOrderNo.Text
                .Attributes("CONTACT_TYPE_ID").Value = ddlContactType.SelectedValue
                .Attributes("AssignedTo").Value = ddlTeamAssignedTo.Text
                If Not Session("LoginSession") Is Nothing Then
                    .Attributes("LoggedBy").Value = Session("LoginSession").ToString.Split("|").GetValue(0)
                End If
                .Attributes("LeftDateTime").Value = objeAAMS.GetDateFormat(txtLeftDateTime.Text, "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm", "/")
                'Added on 23 Sep 08
                .Attributes("HD_RE_CLOSED_DATE").Value = objeAAMS.GetDateFormat(txtCloseDateTime.Text, "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm", "/")

                .Attributes("DESCRIPTION").Value = txtDescription.Text
                .Attributes("SOLUTION").Value = txtSolution.Text
                .Attributes("RESAVE").Value = hdReSave.Value
            End With

            'Here Back end Method Call
            objOutputXml = objbzCall.Update(objInputXml)
            hdMsg.Value = ""
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                hdReSave.Value = "0"
                If hdPageHD_RE_ID.Value = "" Then
                    lblError.Text = objeAAMSMessage.messInsert
                    hdPageStatus.Value = "U"
                Else
                    lblError.Text = objeAAMSMessage.messUpdate
                End If
                If ddlQueryStatus.SelectedValue.Split("|").GetValue(1) = "1" Then
                    'btnSave.Enabled = False
                    EnabledDisabledSavebutton("1")
                End If

              
                txtDescription.Text = ""
                txtSolution.Text = ""
                ViewState("vsDescription") = txtDescription.Text
                ViewState("vsSolution") = txtSolution.Text

                rdFunctional.Enabled = False
                hdPageHD_RE_ID.Value = objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("HD_RE_ID").Value
                btnFeedBack.Enabled = True
                btnAssigneeHistory.Enabled = True
                btnHistory.Enabled = True
                txtLTRNo.Text = hdPageHD_RE_ID.Value
                hdEnPageHD_RE_ID.Value = objED.Encrypt(hdPageHD_RE_ID.Value)
                'code to hid and show popup image
                If hdPageHD_RE_ID.Value <> "" Then
                    Img2.Visible = False
                Else
                    Img2.Visible = True
                End If
                'End
                Bindata()
                ViewRecords()
                'fillCategoryName(objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("CALL_SUB_GROUP_ID").Value)
                'ddlQueryCategory.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("CALL_CATEGORY_ID").Value.Trim
                'fillSubCategoryName(objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("CALL_CATEGORY_ID").Value)
                'ddlQuerySubCategory.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("CALL_SUB_CATEGORY_ID").Value.Trim
                'txtTTNo.Text = objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("TT_NUMBAR").Value
                ''txtBDRLetterID.Text = objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("HD_RE_BDR_LETTER").Value
                ''txtWorkOrderNo.Text = objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("WO_ID").Value
                hdCoordinator1.Value = objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("COORDINATOR1").Value
                hdTabType.Value = "0"
                checkSecurity()
            Else
                If objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Code").Value = "-100" Then
                    checkSecurity()
                    hdMsg.Value = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

                    fillCategoryName(ddlQuerySubGroup.SelectedValue)
                    If ddlQueryCategory.Items.Count > 1 Then
                        ddlQueryCategory.SelectedValue = hdQueryCategory.Value
                    End If

                    fillSubCategoryName(ddlQueryCategory.SelectedValue)

                    Dim liSubCategoryName As New ListItem
                    liSubCategoryName = ddlQuerySubCategory.Items.FindByValue(hdQuerySubCategory.Value)
                    If liSubCategoryName IsNot Nothing Then
                        ddlQuerySubCategory.SelectedValue = liSubCategoryName.Value
                    End If


                    'If ddlQuerySubCategory.Items.Count > 1 Then
                    'ddlQuerySubCategory.SelectedValue = hdQuerySubCategory.Value
                    'End If
                    hdTabType.Value = "0"
                    'If ConfigurationManager.AppSettings("WAIT_TIME") IsNot Nothing Then
                    ' Dim intTime As Integer = Val(ConfigurationManager.AppSettings("WAIT_TIME"))
                    ' ClientScript.RegisterClientScriptBlock(Me.GetType, "strPopup", "<script language='javascript'>window.setTimeout('YesNoTechnical();'," + intTime.ToString + ");</script>")
                    'Else
                    'ClientScript.RegisterClientScriptBlock(Me.GetType, "strPopup", "<script language='javascript'>window.setTimeout('YesNoTechnical();',1000);</script>")
                    'End If
                    'ClientScript.RegisterClientScriptBlock(Me.GetType, "strPopup", "<script language='javascript'>window.setTimeout('YesNoTechnical();', 1000);</script>")
                    Exit Sub
                End If
                fillCategoryName(ddlQuerySubGroup.SelectedValue)
                If ddlQueryCategory.Items.Count > 1 Then
                    ddlQueryCategory.SelectedValue = hdQueryCategory.Value
                End If

                fillSubCategoryName(ddlQueryCategory.SelectedValue)

                Dim liSubCategoryName2 As New ListItem
                liSubCategoryName2 = ddlQuerySubCategory.Items.FindByValue(hdQuerySubCategory.Value)
                If liSubCategoryName2 IsNot Nothing Then
                    ddlQuerySubCategory.SelectedValue = liSubCategoryName2.Value
                End If


                'If ddlQuerySubCategory.Items.Count > 1 Then
                'ddlQuerySubCategory.SelectedValue = hdQuerySubCategory.Value
                'End If
                hdTabType.Value = "0"
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            'Dim strData As String = hdCallAgencyName.Value
            'txtAgencyName.Text = strData.Split("|").GetValue(1)
            'txtAddress.Text = strData.Split("|").GetValue(2)
            'txtCity.Text = strData.Split("|").GetValue(4)
            'txtCountry.Text = strData.Split("|").GetValue(5)
            'txtPhone.Text = strData.Split("|").GetValue(6)
            'txtOfficeId.Text = strData.Split("|").GetValue(7)
            'txtFax.Text = strData.Split("|").GetValue(8)
            'txtOnlineStatus.Text = strData.Split("|").GetValue(9)
            'hdAOffice.Value = strData.Split("|").GetValue(10)
            'txtPincode.Text = strData.Split("|").GetValue(10)
            'txtEmail.Text = strData.Split("|").GetValue(10)
        End Try
    End Sub

    '**************************************************************************************************************
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveInformation(PageOperation.Save)
    End Sub
    'This function is used to enable or disable save button according to rights 
    'This function called once when call is saved
    'Here parameter passed is currently (Closed status ie "1")
    Sub EnabledDisabledSavebutton(ByVal strValue As String)
        If strValue = "1" Then
            btnSave.Enabled = False
            If Session("LoginSession") IsNot Nothing And Session("Security") IsNot Nothing Then
                If objeAAMS.REOPENCLOSEDBRLTR(Session("Security")) = "1" Then
                    hdSaveRights.Value = "1"
                    btnSave.Enabled = True
                    imgCloseDateTime.Visible = True
                    'If strStatus.ToUpper = "SOLVED ONLINE" Or strStatus.ToUpper = "SOLVED OFFLINE" Then
                    'End If                
                End If
            End If
        End If
    End Sub
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click

        Response.Redirect("TCUP_CallLog.aspx?Action=I")

    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If hdPageHD_RE_ID.Value <> "" Then
                lblError.Text = ""
                txtDescription.Text = ViewState("vsDescription")
                txtSolution.Text = ViewState("vsSolution")
                ViewRecords()
            Else
                Dim strQueryString As String = Request.QueryString.ToString
                Response.Redirect("TCUP_CallLog.aspx?" + strQueryString)
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub


    Protected Sub rdFunctional_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdFunctional.CheckedChanged
        'This code added on 22 july 08
        'New start Code
        If hdPageStatus.Value = "I" Or hdPageStatus.Value = "" Then
            'This code is for passing lcode when user swap query group after selecting agencyName
            If hdCallAgencyName.Value <> "" Then
                If hdEnCallAgencyName_LCODE.Value = "" Then
                    hdEnCallAgencyName_LCODE.Value = objED.Encrypt(hdCallAgencyName.Value.Split("|").GetValue(0))
                End If
                Response.Redirect("HDUP_Calllog.aspx?LCodeTemp=" + hdEnCallAgencyName_LCODE.Value)
            Else
                Response.Redirect("HDUP_Calllog.aspx?" + hdQueryString.Value)
            End If
        Else
            Response.Redirect("HDUP_Calllog.aspx?" + hdQueryString.Value)
        End If
        'New End Code

        'Earlier Code
        '   Response.Redirect("HDUP_Calllog.aspx?" + hdQueryString.Value)
    End Sub
    Private Sub AgencyView()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency

        Try
            objInputXml.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerXml = hdPageLCode.Value
            objOutputXml = objbzAgency.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXml.DocumentElement.SelectSingleNode("Agency")
                    txtAgencyName.Text = .Attributes("NAME").Value()
                    txtAddress.Text = .Attributes("ADDRESS").Value() + vbCrLf + .Attributes("ADDRESS1").Value()
                    txtCity.Text = .Attributes("CITY").Value()
                    txtCountry.Text = .Attributes("COUNTRY").Value()
                    txtPhone.Text = .Attributes("PHONE").Value()
                    txtFax.Text = .Attributes("FAX").Value()
                    txtOnlineStatus.Text = .Attributes("ONLINE_STATUS").Value()
                    txtOfficeId.Text = .Attributes("OFFICEID").Value()
                    hdAOffice.Value = .Attributes("Aoffice").Value()
                    hdEnAOffice.Value = objED.Encrypt(hdAOffice.Value)
                    txtPincode.Text = .Attributes("PINCODE").Value()
                    txtEmail.Text = .Attributes("EMAIL").Value()
                    hdCallAgencyName.Value = .Attributes("LOCATION_CODE").Value + "|" + .Attributes("NAME").Value + "|" + .Attributes("ADDRESS").Value + "|" + "" + "|" + .Attributes("CITY").Value + "|" + .Attributes("COUNTRY").Value + "|" + .Attributes("PHONE").Value + "|" + .Attributes("OFFICEID").Value + "|" + .Attributes("FAX").Value + "|" + .Attributes("ONLINE_STATUS").Value + "|" + .Attributes("Aoffice").Value + "|" + .Attributes("PINCODE").Value() + "|" + .Attributes("EMAIL").Value()
                    hdEnCallAgencyName_LCODE.Value = objED.Encrypt(.Attributes("LOCATION_CODE").Value)

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
    Sub ViewRecords()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzCall As New AAMS.bizBRHelpDesk.bzCall
        objInputXml.LoadXml("<HD_VIEW_CALLREQUEST_INPUT><HD_RE_ID></HD_RE_ID></HD_VIEW_CALLREQUEST_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = hdPageHD_RE_ID.Value
        'Here Back end Method Call 
        '<CALLREQUEST HD_QUERY_GROUP_ID="1" LCODE="664"  COUNTRY="India" CITY="Mumbai" PHONE="24464123" FAX="24442036" 
        'ONLINE_STATUS="DE" OFFICEID="" HD_RE_ID="107" CALLER_NAME="Tester"
        '      CALL_SUB_GROUP_ID="5" CALL_CATEGORY_ID="5" CALL_SUB_CATEGORY_ID="7" 
        'CALL_DURATION="10" HD_SEVERITY_ID="3" HD_STATUS_ID="1" COORDINATOR1="37" COORDINATOR2="37" HD_PTR_REF=""
        ' TT_NUMBAR="" HD_RE_BDR_LETTER="" WO_ID="" CONTACT_TYPE_ID="1"
        '      DISPOSITION_ID="1" HD_RE_OPEN_DATE="3/7/2008 1:39:25 PM" HD_RE_CLOSED_DATE="" AssignedTo="" AssignedDateTime="" LoggedBy="35"/>
        objOutputXml = objbzCall.View(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            With objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST")
                hdPageLCode.Value = .Attributes("LCODE").Value
                hdEnPageLCode.Value = objED.Encrypt(hdPageLCode.Value)
                hdFeedBackId.Value = .Attributes("FEEDBACK_ID").Value
                hdEnFeedBackId.Value = objED.Encrypt(.Attributes("FEEDBACK_ID").Value)
                hdAOffice.Value = .Attributes("AOFFICE").Value
                hdEnAOffice.Value = objED.Encrypt(hdAOffice.Value)
                txtLTRNo.Text = .Attributes("HD_RE_ID").Value
                txtAgencyName.Text = .Attributes("AGENCYNAME").Value
                txtAddress.Text = .Attributes("ADDRESS").Value
                txtCountry.Text = .Attributes("COUNTRY").Value
                txtCity.Text = .Attributes("CITY").Value
                txtPhone.Text = .Attributes("PHONE").Value
                txtFax.Text = .Attributes("FAX").Value
                txtOnlineStatus.Text = .Attributes("ONLINE_STATUS").Value
                txtOfficeId.Text = .Attributes("OFFICEID").Value
                txtOfficeId.ReadOnly = True
                txtOfficeId.CssClass = "textboxgrey"

                txtPincode.Text = .Attributes("PINCODE").Value
                txtEmail.Text = .Attributes("EMAIL").Value

                txtCallerName.Text = .Attributes("CALLER_NAME").Value
                ddlQuerySubGroup.SelectedValue = .Attributes("CALL_SUB_GROUP_ID").Value


                fillCategoryName(ddlQuerySubGroup.SelectedValue)
                If ddlQueryCategory.Items.Count > 1 Then
                    ddlQueryCategory.SelectedValue = .Attributes("CALL_CATEGORY_ID").Value
                End If
                fillSubCategoryName(ddlQueryCategory.SelectedValue)
                Dim liSubCategoryName As New ListItem
                liSubCategoryName = ddlQuerySubCategory.Items.FindByValue(.Attributes("CALL_SUB_CATEGORY_ID").Value)
                If liSubCategoryName IsNot Nothing Then
                    ddlQuerySubCategory.SelectedValue = liSubCategoryName.Value
                End If


                'If ddlQuerySubCategory.Items.Count > 1 Then
                '    ddlQuerySubCategory.SelectedValue = .Attributes("CALL_SUB_CATEGORY_ID").Value
                'End If


                Dim strCallDuration As String = .Attributes("CALL_DURATION").Value
                Select Case strCallDuration.Length
                    Case "1"
                        txtCallDuration.Text = strCallDuration
                    Case "2"
                        txtCallDuration.Text = strCallDuration
                    Case "3"
                        txtCallDuration.Text = strCallDuration.Substring(0, 2)
                        txtCallDuration1.Text = strCallDuration.Substring(2, 1)
                    Case "4"
                        txtCallDuration.Text = strCallDuration.Substring(0, 2)
                        txtCallDuration1.Text = strCallDuration.Substring(2, 2)
                End Select


                ddlPriority.SelectedValue = .Attributes("HD_SEVERITY_ID").Value
                ddlQueryStatus.SelectedValue = .Attributes("HD_STATUS_CLOSE").Value
                hdQueryStatus.Value = ddlQueryStatus.SelectedItem.Text

                ' fillCoordinator("1", .Attributes("AOFFICE").Value)
                hdCoordinator1.Value = .Attributes("COORDINATOR1").Value
                Dim liCoordinator1 As New ListItem
                liCoordinator1 = ddlCoordinator1.Items.FindByValue(.Attributes("COORDINATOR1").Value)
                If liCoordinator1 IsNot Nothing Then
                    ddlCoordinator1.SelectedValue = liCoordinator1.Value
                Else
                    liCoordinator1 = New ListItem
                    liCoordinator1.Value = .Attributes("COORDINATOR1").Value
                    liCoordinator1.Text = .Attributes("COORDINATOR1_NAME").Value
                    ddlCoordinator1.Items.Add(liCoordinator1)
                    ddlCoordinator1.SelectedValue = liCoordinator1.Value
                End If
                'ddlCoordinator1.SelectedValue = .Attributes("COORDINATOR1").Value


                '  fillCoordinator("2", .Attributes("AOFFICE").Value)
                '  hdCoordinator2.Value = .Attributes("COORDINATOR2").Value

                txtTTNo.Text = .Attributes("TT_NUMBAR").Value
                ' txtBDRLetterID.Text = .Attributes("HD_RE_BDR_LETTER").Value
                ' txtWorkOrderNo.Text = .Attributes("WO_ID").Value

                Dim liContactType As New ListItem
                liContactType = ddlContactType.Items.FindByValue(.Attributes("CONTACT_TYPE_ID").Value)
                If liContactType IsNot Nothing Then
                    ddlContactType.SelectedValue = liContactType.Value
                End If
                ' ddlContactType.SelectedValue = .Attributes("CONTACT_TYPE_ID").Value

                txtLoggedDate.Text = .Attributes("HD_RE_OPEN_DATE").Value
                txtCloseDateTime.Text = .Attributes("HD_RE_CLOSED_DATE").Value
                Dim liTeamAssignedTo As New ListItem
                liTeamAssignedTo = ddlTeamAssignedTo.Items.FindByValue(.Attributes("AssignedTo").Value)
                If liTeamAssignedTo IsNot Nothing Then
                    ddlTeamAssignedTo.SelectedValue = liTeamAssignedTo.Value
                End If

                'ddlTeamAssignedTo.SelectedValue = .Attributes("AssignedTo").Value
                txtDateAssigned.Text = .Attributes("AssignedDateTime").Value
                txtLoggedBy.Text = .Attributes("LoggedByName").Value
                '1052|test67|ewrw||Agra|India|||||AGR
                hdCallAgencyName.Value = .Attributes("LCODE").Value + "|" + .Attributes("AGENCYNAME").Value + "|" + .Attributes("ADDRESS").Value + "|" + "" + "|" + .Attributes("CITY").Value + "|" + .Attributes("COUNTRY").Value + "|" + .Attributes("PHONE").Value + "|" + .Attributes("OFFICEID").Value + "|" + .Attributes("FAX").Value + "|" + .Attributes("ONLINE_STATUS").Value + "|" + .Attributes("AOFFICE").Value
                hdEnCallAgencyName_LCODE.Value = objED.Encrypt(.Attributes("LCODE").Value)
                txtLeftDateTime.Text = .Attributes("LeftDateTime").Value

                If .Attributes("HD_STATUS_CLOSE").Value.Split("|").GetValue(1) = "1" Then
                    'Commented on 22 July 08
                    'In place of this new function is called
                    '   btnSave.Enabled = False
                    EnabledDisabledSavebutton("1")
                End If

                BindDescription()

                BindSolution()
            End With
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
    Sub AddCoordinator()
        Dim objOutputXml As New XmlDocument
        Dim objXmlDoc As New XmlDocument
        Dim objCordinator As New AAMS.bizBRHelpDesk.bzCoordinator
        Try
            objXmlDoc.LoadXml("<HD_UPDATECOORDINATOR_INPUT><COORDINATOR Action='' Aoffice='' COORDINATOR_TYPE=''><EMPLOYEE EMPLOYEE_ID='' /></COORDINATOR></HD_UPDATECOORDINATOR_INPUT>")

            objXmlDoc.DocumentElement.SelectSingleNode("COORDINATOR").Attributes("Action").Value = "I"
            objXmlDoc.DocumentElement.SelectSingleNode("COORDINATOR").Attributes("COORDINATOR_TYPE").Value = "1"
            objXmlDoc.DocumentElement.SelectSingleNode("COORDINATOR").Attributes("Aoffice").Value = objeAAMS.AOffice(Session("Security"))
            objXmlDoc.DocumentElement.SelectSingleNode("COORDINATOR/EMPLOYEE").Attributes("EMPLOYEE_ID").Value = objeAAMS.EmployeeID(Session("Security"))
            objOutputXml = objCordinator.Update(objXmlDoc)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ' Filling Cordinator
                FillCoordinator()
                ddlCoordinator1.SelectedValue = objeAAMS.EmployeeID(Session("Security"))
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Sub BindDescription()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objCall As New AAMS.bizBRHelpDesk.bzCall
        objInputXml.LoadXml("<HD_GETREQUESTDESCRIPTION_INPUT><HD_RE_ID></HD_RE_ID></HD_GETREQUESTDESCRIPTION_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = hdPageHD_RE_ID.Value
        objOutputXml = objCall.ViewRequestDesc(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)

            If ds.Tables("REQUESTDESCRIPTION").Rows.Count <> 0 Then
                gvDescription.DataSource = ds.Tables("REQUESTDESCRIPTION")
                gvDescription.DataBind()

            Else
                gvDescription.DataSource = Nothing
                gvDescription.DataBind()
            End If

        Else
            If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value <> "No Record Found!") Then
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            Else
                ' code for binding header when no record found.
                'Dim dt As New DataTable
                'Dim dc As New DataColumn("EmployeeName", GetType(String))
                'Dim dc1 As New DataColumn("DATETIME", GetType(String))
                'Dim dc2 As New DataColumn("ACTION_TAKEN", GetType(String))
                'Dim dr As DataRow
                'dt.Columns.Add(dc)
                'dt.Columns.Add(dc1)
                'dt.Columns.Add(dc2)
                'dr = dt.NewRow()
                'dt.Rows.Add(dr)
                'gvDescription.DataSource = dt
                'gvDescription.DataBind()

            End If
        End If
    End Sub

    Sub BindSolution()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objCall As New AAMS.bizBRHelpDesk.bzCall
        objInputXml.LoadXml("<HD_GETREQUESTSOLUTION_INPUT><HD_RE_ID></HD_RE_ID></HD_GETREQUESTSOLUTION_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = hdPageHD_RE_ID.Value
        objOutputXml = objCall.ViewRequestSolution(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)

            If ds.Tables("REQUESTSOLUTION").Rows.Count <> 0 Then
                gvSolution.DataSource = ds.Tables("REQUESTSOLUTION")
                gvSolution.DataBind()

            Else
                gvSolution.DataSource = Nothing
                gvSolution.DataBind()
            End If

        Else
            If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value <> "No Record Found!") Then
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            Else
                ' code for binding header when no record found.
                'Dim dt As New DataTable
                'Dim dc As New DataColumn("EmployeeName", GetType(String))
                'Dim dc1 As New DataColumn("DATETIME", GetType(String))
                'Dim dc2 As New DataColumn("ACTION_TAKEN", GetType(String))
                'Dim dr As DataRow
                'dt.Columns.Add(dc)
                'dt.Columns.Add(dc1)
                'dt.Columns.Add(dc2)
                'dr = dt.NewRow()
                'dt.Rows.Add(dr)
                'gvSolution.DataSource = dt
                'gvSolution.DataBind()

            End If
        End If
    End Sub

    Public Sub fillCategoryName(ByVal strId As String)
        If strId <> "" Then
            ddlQueryCategory.Items.Clear()
            Dim objbzCallCategory As New AAMS.bizBRHelpDesk.bzCallCategory
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            Dim objInputXml, objOutputXml As New XmlDocument
            objInputXml.LoadXml("<HD_SEARCHCALLCATEGORY_INPUT><HD_QUERY_GROUP_ID>2</HD_QUERY_GROUP_ID><CALL_CATEGORY_NAME /><CALL_SUB_GROUP_ID /><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHCALLCATEGORY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_GROUP_ID").InnerText = strId
            objOutputXml = objbzCallCategory.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                ddlQueryCategory.DataSource = ds.Tables("CALL_CATEGORY")
                ddlQueryCategory.DataTextField = "CALL_CATEGORY_NAME"
                ddlQueryCategory.DataValueField = "CALL_CATEGORY_ID"
                ddlQueryCategory.DataBind()
            End If
            ddlQueryCategory.Items.Insert(0, New ListItem("Select One", ""))
        Else
            ddlQueryCategory.Items.Clear()
            ddlQueryCategory.Items.Insert(0, New ListItem("Select One", ""))
        End If
    End Sub

    Public Sub fillSubCategoryName(ByVal strId As String)
        If strId <> "" Then
            ddlQuerySubCategory.Items.Clear()
            Dim objbzCallSubCategory As New AAMS.bizBRHelpDesk.bzCallSubCategory
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            Dim objInputXml, objOutputXml As New XmlDocument
            objInputXml.LoadXml("<HD_SEARCHCALLSUBCATEGORY_INPUT><CALL_SUB_CATEGORY_NAME /><CALL_CATEGORY_ID /><CALL_SUB_GROUP_ID /><HD_QUERY_GROUP_ID>2</HD_QUERY_GROUP_ID><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHCALLSUBCATEGORY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_GROUP_ID").InnerText = ddlQuerySubGroup.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("CALL_CATEGORY_ID").InnerText = strId

            objOutputXml = objbzCallSubCategory.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                ddlQuerySubCategory.Items.Clear()
                ddlQuerySubCategory.DataSource = ds.Tables("CALL_SUB_CATEGORY")
                ddlQuerySubCategory.DataTextField = "CALL_SUB_CATEGORY_NAME"
                ddlQuerySubCategory.DataValueField = "CALL_SUB_CATEGORY_ID"
                ddlQuerySubCategory.DataBind()
                ddlQuerySubCategory.Items.Insert(0, New ListItem("Select One", ""))
            Else
                ddlQuerySubCategory.Items.Clear()
                ddlQuerySubCategory.Items.Insert(0, New ListItem("Select One", ""))
            End If
        Else
            ddlQuerySubCategory.Items.Clear()
            ddlQuerySubCategory.Items.Insert(0, New ListItem("Select One", ""))
        End If
    End Sub

    Public Sub fillCoordinator(ByVal strCoordinatorType As String, ByVal strAoffice As String)
        'Code to fill Coordinator
        If strAoffice <> "" Then


            Dim objbzCoordinator As New AAMS.bizBRHelpDesk.bzCoordinator
            Dim objInputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim objOutputXml As XmlDocument
            objOutputXml = New XmlDocument
            Dim ds As New DataSet
            objInputXml.LoadXml("<HD_SEARCHCOORDINATOR_INPUT><COORDINATOR_TYPE></COORDINATOR_TYPE><EMPLOYEE_NAME /><Aoffice /><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHCOORDINATOR_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("COORDINATOR_TYPE").InnerText = strCoordinatorType
            objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = strAoffice
            objOutputXml = objbzCoordinator.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                If strCoordinatorType = "1" Then
                    ddlCoordinator1.Items.Clear()
                    ddlCoordinator1.DataSource = ds.Tables("COORDINATOR")
                    ddlCoordinator1.DataTextField = "EMPLOYEE_NAME"
                    ddlCoordinator1.DataValueField = "EmployeeID"
                    ddlCoordinator1.DataBind()
                    ddlCoordinator1.Items.Insert(0, New ListItem("Select One", ""))

                End If

            End If
        End If
    End Sub
    Private Sub ViewCallLogInsertDetails()

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objhdPtrView As New AAMS.bizBRHelpDesk.bzPTR
        objOutputXml = objhdPtrView.ListPTRConfigValue()

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            Dim xNode As XmlNode
            For Each xNode In objOutputXml.DocumentElement.SelectNodes("CONFIGVALUE")
                If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_HD_REQUEST_SEVERITY" Then
                    ddlPriority.SelectedValue = xNode.Attributes("FIELD_VALUE").Value.Trim().Split("|").GetValue(1)
                End If
                If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_HD_REQUEST_STATUS" Then
                    ddlQueryStatus.SelectedValue = xNode.Attributes("FIELD_VALUE").Value.Trim().Split("|").GetValue(1) & "|" & "0"
                    If ddlQueryStatus.SelectedValue = "" Then
                        ddlQueryStatus.SelectedValue = xNode.Attributes("FIELD_VALUE").Value.Trim().Split("|").GetValue(1) & "|" & "1"
                    End If
                End If
                If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_HD_REQUEST_CONTACT_TYPE" Then
                    ddlContactType.SelectedValue = xNode.Attributes("FIELD_VALUE").Value.Trim()
                End If
            Next
        ElseIf (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value.ToUpper <> "NO RECORD FOUND") Then
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
    Enum PageOperation
        Save = 1
        ReSave = 2
    End Enum


    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
        Return str
    End Function

    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
        Dim objInputXml, objOutputXml As New XmlDocument
        If eventArgument <> "" Then
            ' objOutputXml.Load("c:\agencyd.xml")
            ' str = objOutputXml.OuterXml
            AgencyViewByOfficeID(eventArgument)
        End If
    End Sub

    Private Sub AgencyViewByOfficeID(ByVal strOfficeID As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
        Dim strhdEnAOffice As String = ""
        Dim strhdEnCallAgencyName_LCODE As String = ""

        Try
            ' <AGNECY LOCATION_CODE="21555" CHAIN_CODE="951" OfficeID="BOMMY3100" NAME="(Aurion Pro Solutions)" ADDRESS="404,Winchester, High Street, Hiranandani" ADDRESS1="Garden" CITY="Mumbai" COUNTRY="India" PHONE="67707700" FAX="67707722" ONLINE_STATUS="DE"
            'Aoffice="BOM" PINCODE="" Email="" CONTACT_PERSON="" CONTACT_PERSON_ID="" CITYID="180" COUNTRYID="1"/>
            objInputXml.LoadXml("<TA_SEARCHAGENCY_INPUT><OFFICEID></OFFICEID><BR_HD>TRUE</BR_HD></TA_SEARCHAGENCY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerXml = strOfficeID
            objOutputXml = objbzAgency.OfficeID_AgencySearch(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXml.DocumentElement.SelectSingleNode("AGNECY")
                    strhdEnAOffice = objED.Encrypt(.Attributes("Aoffice").Value())
                    strhdEnCallAgencyName_LCODE = objED.Encrypt(.Attributes("LOCATION_CODE").Value)
                End With
                str = objOutputXml.OuterXml + "$" + strhdEnAOffice + "$" + strhdEnCallAgencyName_LCODE
            Else
                str = ""
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

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                '   Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
End Class
