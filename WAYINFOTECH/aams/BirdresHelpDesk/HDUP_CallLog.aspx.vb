
Partial Class BirdresHelpDesk_HDUP_CallLog
    Inherits System.Web.UI.Page
    'Inherits BasePage
    Implements ICallbackEventHandler

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
            'Code Added
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
            txtNxtFollowupDate.Text = Request.Form("txtNxtFollowupDate")
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
            'hdAOffice.Value = strData.Split("|").GetValue(10)

            'Check Security
            checkSecurity()
            'code for officeID

            Dim m As ClientScriptManager = Me.ClientScript
            str = m.GetCallbackEventReference(Me, "args", "BRReceiveServerDataFunctional", "'this is context from server'")
            Dim strCallback As String = "function BRCallServerFunctional(args,context){" + str + ";}"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)
            'txtOfficeId.Attributes.Add("onfocusout", "return FillBRAgencyDetailsFunctional()")
            txtOfficeId.Attributes.Add("onblur", "return FillBRAgencyDetailsFunctional()")


            ' code end

            'Dim m As ClientScriptManager = Me.ClientScript
            'str = m.GetCallbackEventReference(Me, "args", "ReceiveServerData", "'this is context from server'")
            'Dim strCallback As String = "function CallServer(args,context){" + str + ";}"
            'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)
            ddlQuerySubGroup.Attributes.Add("OnChange", "return fillBRCategoryFunctional();")
            ddlQueryCategory.Attributes.Add("OnChange", "return fillBRSubCategoryFunctional();")
            ddlQuerySubCategory.Attributes.Add("OnChange", "return fillBRSubCategoryDefaultValues();")
            'hdQueryCategory.Value = Request.Form(

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


                hdEnFunctional.Value = objED.Encrypt("Functional") 'Storing encrypted value in hiddenfield 
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
                    '@End of  Previous Code
                End If


                If Not Request.QueryString("strStatus") Is Nothing Then
                    Dim EnstrStatus As String
                    EnstrStatus = objED.Decrypt(Request.QueryString("strStatus"))
                    If EnstrStatus.ToUpper = "TECHNICAL" Then
                        Dim strQueryString As String = Request.QueryString.ToString
                        Response.Redirect("TCUP_CallLog.aspx?" + strQueryString)
                    End If
                End If
                If Not Request.QueryString("Action") Is Nothing Then
                    ' Dim EnstrAction As String
                    ' EnstrAction = Request.QueryString("Action")
                    hdPageStatus.Value = Request.QueryString("Action")
                    'hdPageStatus.Value = Request.QueryString("Action").ToString
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
                    rdTechanical.Enabled = False
                Else
                    btnAssigneeHistory.Enabled = False
                    btnHistory.Enabled = False
                    btnFeedBack.Enabled = False
                End If

                LoadAllControl()
                Bindata()
                'If hdPageHD_RE_ID.Value <> "" Then
                '    ViewRecords()
                'Else

                'End If

                If hdPageHD_RE_ID.Value <> "" Then
                    ViewRecords()
                    txtCallerName.Focus()
                Else

                    'Code Added on 23rd 
                    If Session("Security") IsNot Nothing Then
                        Dim xDoc As New XmlDocument
                        xDoc.LoadXml(Session("Security"))
                        txtLoggedBy.Text = xDoc.DocumentElement.SelectSingleNode("Login").InnerText    '"admin"


                        'Time Calculation
                        ' hdTimeSpan.Value = DateTime.Now.TimeOfDay.ToString()




                        ' txtOfficeId.Focus()
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
                            'AddCoordinator()
                        End If
                    End If
                End If
            End If


            'code to hide and show popup image
            If hdPageHD_RE_ID.Value <> "" Then
                Img2.Visible = False
                Img3.Visible = True
                Img4.Visible = True
                Img5.Visible = True
            Else
                Img2.Visible = True
                Img3.Visible = False
                Img4.Visible = False
                Img5.Visible = False
            End If

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
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Log HD Call']").Count <> 0 Then
                strBuilderFunctional = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Log HD Call']").Attributes("Value").Value)
            End If
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Log Tech Call']").Count <> 0 Then
                strBuilderTechnical = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Log Tech Call']").Attributes("Value").Value)
            End If
            'When View rights disabled
            If strBuilderFunctional(0) = "0" And strBuilderTechnical(0) = "0" Then
                Response.Redirect("../NoRights.aspx")
                btnSave.Enabled = False
            End If
            If strBuilderFunctional(0) = "0" And strBuilderTechnical(0) <> "0" Then
                Dim strQueryString As String = Request.QueryString.ToString
                Response.Redirect("TCUP_CallLog.aspx?" + strQueryString)
            End If
            If strBuilderTechnical(0) = "0" Then
                rdTechanical.Enabled = False
            End If
            'When Add rights disabled
            If strBuilderFunctional(1) = "0" Then
                btnNew.Enabled = False
                btnSave.Enabled = False
            End If
            'When modify rights disabled and Add rights enabled
            If strBuilderFunctional(2) = "0" And (hdPageHD_RE_ID.Value <> "" Or Request.QueryString("HD_RE_ID") IsNot Nothing) Then
                btnSave.Enabled = False
            End If
            'When modify rights Enabled and Add rights disabled
            If strBuilderFunctional(1) = "0" And strBuilderFunctional(2) <> "0" And (hdPageHD_RE_ID.Value <> "" Or Request.QueryString("HD_RE_ID") IsNot Nothing) Then
                btnSave.Enabled = True
            End If
            If objeAAMS.ReturnViewPermission(Session("Security"), "BR Feedback Action") = "0" Then
                btnFeedBack.Enabled = False
            End If
        Else
            strBuilderFunctional = objeAAMS.SecurityCheck(31)
        End If
    End Sub

    Sub LoadAllControl()
        Try
            'objeAAMS.BindDropDown(ddlContactType, "CONTACTTYPE", True)
            objeAAMS.BindDropDown(ddlQuerySubGroup, "BRQuerySubGroup", True, 1)
            objeAAMS.BindDropDown(ddlQueryStatus, "BRQueryStatusWithClosed", True, 1)
            objeAAMS.BindDropDown(ddlPriority, "BRQUERYPRIORITY", True, 1)
            objeAAMS.BindDropDown(ddlTeamAssignedTo, "BRTEAM", True, 1)
            objeAAMS.BindDropDown(ddlContactType, "BRCONTACTTYPE", True, 1)
            objeAAMS.BindDropDown(ddlMode, "BRCONTACTTYPE", True, 2)
            objeAAMS.BindDropDown(ddlDisposition, "BRDISPOSITION", True, 2)
            'objeAAMS.BindDropDown(ddlCoordinator1, "COORDINATOR1", True)
            'objeAAMS.BindDropDown(ddlCoordinator2, "COORDINATOR2", True)


            'Code to fill Query Category
            Dim objCallCategory As New AAMS.bizBRHelpDesk.bzCallCategory
            Dim objCallCategory1 As New AAMS.bizBRHelpDesk.bzDisposition

            Dim objOutputXml As XmlDocument
            objOutputXml = New XmlDocument
            Dim objInXml As New XmlDocument
            objInXml.LoadXml("<HD_SEARCHCALLCATEGORY_INPUT><CALL_CATEGORY_NAME /><CALL_SUB_GROUP_ID /><HD_QUERY_GROUP_ID /><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHCALLCATEGORY_INPUT>")
            objInXml.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = "1"
            objOutputXml = objCallCategory.SearchNew(objInXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Dim objErrorNode As XmlNode
                objErrorNode = objOutputXml.DocumentElement.SelectSingleNode("Errors")
                If objErrorNode IsNot Nothing Then
                    objOutputXml.DocumentElement.RemoveChild(objErrorNode)
                End If
                hdCategory.Value = objOutputXml.OuterXml
            End If

            'Code to fill Query Sub Category
            Dim objCallSubCategory As New AAMS.bizBRHelpDesk.bzCallSubCategory
            Dim objOutputXml1 As XmlDocument
            objOutputXml1 = New XmlDocument
            Dim objInXml1 As New XmlDocument
            objInXml1.LoadXml("<HD_SEARCHCALLSUBCATEGORY_INPUT><CALL_SUB_CATEGORY_NAME /><CALL_CATEGORY_ID /><CALL_SUB_GROUP_ID /><HD_QUERY_GROUP_ID /><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHCALLSUBCATEGORY_INPUT>")
            objInXml1.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = "1"
            objOutputXml1 = objCallSubCategory.SearchNew(objInXml1)
            If objOutputXml1.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Dim objErrorNode As XmlNode
                objErrorNode = objOutputXml1.DocumentElement.SelectSingleNode("Errors")
                If objErrorNode IsNot Nothing Then
                    objOutputXml1.DocumentElement.RemoveChild(objErrorNode)
                End If
                hdSubCategory.Value = objOutputXml1.OuterXml
            End If

            Dim objInputXml As New XmlDocument
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
                ddlCoordinator1.DataSource = ds.Tables("COORDINATOR")
                ddlCoordinator1.DataTextField = "EMPLOYEE_NAME"
                ddlCoordinator1.DataValueField = "EmployeeID"
                ddlCoordinator1.DataBind()
                ddlCoordinator1.Items.Insert(0, New ListItem("--Select One--", ""))
            End If


            objInputXml.DocumentElement.SelectSingleNode("COORDINATOR_TYPE").InnerText = "2"
            objOutputXml = objbzCoordinator.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds1.ReadXml(objXmlReader)
                ddlCoordinator2.DataSource = ds1.Tables("COORDINATOR")
                ddlCoordinator2.DataTextField = "EMPLOYEE_NAME"
                ddlCoordinator2.DataValueField = "EmployeeID"
                ddlCoordinator2.DataBind()
                ddlCoordinator2.Items.Insert(0, New ListItem("", ""))
            End If
            '  ViewCallLogInsertDetails()

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Sub Bindata()
        Dim TabText As New ArrayList()
        Try
            TabText.Add("Call")
            TabText.Add("Description")
            TabText.Add("Solution")
            TabText.Add("Follow Up")
            If Request.QueryString("Popup") Is Nothing Then
                TabText.Add("Linked LTR")
                If hdFeedBackPresence.Value = "1" Then
                    TabText.Add("FeedBack")
                End If
            End If

            theTabStrip.DataSource = TabText
            theTabStrip.DataBind()

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
        'If LTR number(hdPageHD_RE_ID) doesn't exist then "Linked LTR" and "FeedBack" link will not come
        If hdPageHD_RE_ID.Value = "" Then
            If e.Item.ItemIndex = 4 Or e.Item.ItemIndex = 5 Then
                Button1.Visible = False
            End If
        End If


        If Session("Security") IsNot Nothing Then
            Dim strButtonText As String
            Dim strLogHDCallPermission As String = ""
            strButtonText = Button1.Text
            strLogHDCallPermission = objeAAMS.ReturnViewPermission(Session("Security"), "BR Log HD Call")
            Select Case strButtonText
                Case "Call"
                    If strLogHDCallPermission = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Description"
                    If strLogHDCallPermission = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Solution"
                    If strLogHDCallPermission = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Follow Up"
                    If strLogHDCallPermission = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Linked LTR"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "BR Linked HD Call") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If

                Case "FeedBack"
                    'If objeAAMS.ReturnViewPermission(Session("Security"), "Feedback Action") = "0" Then
                    Button1.CssClass = "displayNone"
                    ' End If
            End Select
        End If

        If Request.QueryString("Popup") Is Nothing Then
            If hdPageHD_RE_ID.Value = "" Then
                Button1.Attributes.Add("onclick", "return ColorMethodFunctional('" & Button1.ClientID.ToString() & "',4);")
            Else
                Button1.Attributes.Add("onclick", "return ColorMethodFunctional('" & Button1.ClientID.ToString() & "',5);")
            End If

        Else
            Button1.Attributes.Add("onclick", "return ColorMethodFunctional('" & Button1.ClientID.ToString() & "',4);")
        End If

    End Sub


    Sub SaveInformation(ByVal Operation As Integer)
        'If 1 = 1 Then
        '    hdMsg.Value = "aasddfasd"
        '    ClientScript.RegisterClientScriptBlock(Me.GetType, "strPopup", "<script language='javascript'>window.setTimeout('YesNo();', 200);</script>")
        '    Exit Sub
        'End If
        If Operation = PageOperation.ReSave Then

        End If

        Dim objInputXml, objOutputXml As New XmlDocument
        'Start this will check ddlQueryStatus selected value is separated by pipe or not
        'This code mainly helpful when we ReSave LTR ,Because onclientClick of button doesn't fire
        Dim strQueryStatus As String
        Dim strQuerySubCategory As String
        strQueryStatus = ddlQueryStatus.SelectedValue
        If strQueryStatus.Split("|").Length <> 2 Then
            lblError.Text = "Query Status is mandatory"
            Exit Sub
        End If
        'End
        Dim objbzCall As New AAMS.bizBRHelpDesk.bzCall
        Try
            objInputXml.LoadXml("<HD_UPDATE_CALLREQUEST_INPUT><CALLREQUEST HD_QUERY_GROUP_ID='' LCODE ='' HD_RE_ID='' CALLER_NAME='' CALL_SUB_GROUP_ID='' CALL_CATEGORY_ID='' CALL_SUB_CATEGORY_ID='' CALL_DURATION=''  HD_SEVERITY_ID='' HD_STATUS_ID='' COORDINATOR1='' COORDINATOR2='' HD_PTR_REF='' TT_NUMBAR='' HD_RE_BDR_LETTER='' WO_ID='' CONTACT_TYPE_ID='' DISPOSITION_ID='' AssignedTo='' LoggedBy='' LeftDateTime='' DESCRIPTION='' SOLUTION='' FU_UP_CONTACT_TYPE_ID='' FOLLOWUP_DESC='' FOLLOWUP_DATETIME='' HD_STATUS_CLOSE='' RESAVE='' HD_RE_CLOSED_DATE='' TITLE=''/></HD_UPDATE_CALLREQUEST_INPUT>")
            With objInputXml.DocumentElement.SelectSingleNode("CALLREQUEST")
                .Attributes("HD_QUERY_GROUP_ID").Value = "1"
                .Attributes("LCODE").Value = hdCallAgencyName.Value.Split("|").GetValue(0)
                .Attributes("HD_RE_ID").Value = hdPageHD_RE_ID.Value
                .Attributes("CALLER_NAME").Value = txtCallerName.Text
                .Attributes("CALL_SUB_GROUP_ID").Value = ddlQuerySubGroup.SelectedValue

                'start ,here we store query category and query sub category in a hidden field
                If Request.Form(ddlQueryCategory.UniqueID) = "" Then
                    .Attributes("CALL_CATEGORY_ID").Value = hdQueryCategory.Value
                Else
                    .Attributes("CALL_CATEGORY_ID").Value = Request.Form(ddlQueryCategory.UniqueID)
                    hdQueryCategory.Value = Request.Form(ddlQueryCategory.UniqueID)
                End If

                If Request.Form(ddlQuerySubCategory.UniqueID) = "" AndAlso hdSubCategoryMandatory.Value = "1" Then
                    strQuerySubCategory = hdQuerySubCategory.Value.Split(",").GetValue(0)
                    .Attributes("CALL_SUB_CATEGORY_ID").Value = strQuerySubCategory
                Else
                    strQuerySubCategory = Request.Form(ddlQuerySubCategory.UniqueID).Split(",").GetValue(0)
                    .Attributes("CALL_SUB_CATEGORY_ID").Value = strQuerySubCategory
                    hdQuerySubCategory.Value = Request.Form(ddlQuerySubCategory.UniqueID)
                End If
                'end


                'Code Added by Mukund

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
                .Attributes("COORDINATOR2").Value = ddlCoordinator2.SelectedValue
                If txtPTRNo.Text = "" Then
                    .Attributes("HD_PTR_REF").Value = Request.Form("txtPTRNo")
                Else
                    .Attributes("HD_PTR_REF").Value = txtPTRNo.Text
                End If

                .Attributes("TT_NUMBAR").Value = ""
                If txtBDRLetterID.Text = "" Then
                    .Attributes("HD_RE_BDR_LETTER").Value = Request.Form("txtBDRLetterID")
                Else
                    .Attributes("HD_RE_BDR_LETTER").Value = txtBDRLetterID.Text
                End If
                If txtWorkOrderNo.Text = "" Then
                    .Attributes("WO_ID").Value = Request.Form("txtWorkOrderNo")
                Else
                    .Attributes("WO_ID").Value = txtWorkOrderNo.Text
                End If

                .Attributes("CONTACT_TYPE_ID").Value = ddlContactType.SelectedValue
                .Attributes("DISPOSITION_ID").Value = ddlDisposition.SelectedValue
                .Attributes("AssignedTo").Value = ddlTeamAssignedTo.SelectedValue
                If Not Session("LoginSession") Is Nothing Then
                    .Attributes("LoggedBy").Value = Session("LoginSession").ToString.Split("|").GetValue(0)
                End If

                .Attributes("DESCRIPTION").Value = txtDescription.Text
                .Attributes("SOLUTION").Value = txtSolution.Text
                .Attributes("FU_UP_CONTACT_TYPE_ID").Value = ddlMode.SelectedValue
                .Attributes("FOLLOWUP_DESC").Value = txtFollowDesc.Text
                .Attributes("FOLLOWUP_DATETIME").Value = objeAAMS.GetDateFormat(txtNxtFollowupDate.Text, "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm", "/")
                .Attributes("RESAVE").Value = hdReSave.Value
                .Attributes("TITLE").Value = txtTitle.Text
            End With

            '   var itemTeam=document.getElementById("ddlTeamAssignedTo").selectedIndex;
            '   var textTeam=document.getElementById("ddlTeamAssignedTo").options[itemTeam].text ;
            'textTeam=textTeam.toUpperCase();
            ' var itemStatus=document.getElementById("ddlQueryStatus").selectedIndex;
            'var textStatus=document.getElementById("ddlQueryStatus").options[itemStatus].text ;
            ' textStatus=textStatus.toUpperCase();
            'This condition is applied on 23 July
            If ddlTeamAssignedTo.SelectedItem.Text.ToUpper = "ONLINE" And (ddlQueryStatus.SelectedItem.Text.ToUpper = "SOLVED - OFFLINE" Or ddlQueryStatus.SelectedItem.Text.ToUpper = "PENDING - OFFLINE") Then
                lblError.Text = "Call Could not be saved because AssignedTo is Online and Query Status is " & ddlQueryStatus.SelectedItem.Text
                Exit Sub
            End If

            'Commented on 24 feb 09
            'If txtPTRNo.Text <> "" And (txtWorkOrderNo.Text <> "" Or txtBDRLetterID.Text <> "") Then
            '    lblError.Text = "Only one allowed from these PTR No , Work Order No and BDR Letter Id "
            '    Exit Sub
            'End If

            'If txtWorkOrderNo.Text <> "" And (txtPTRNo.Text <> "" Or txtBDRLetterID.Text <> "") Then
            '    lblError.Text = "Only one allowed from these PTR No , Work Order No and BDR Letter Id "
            '    Exit Sub
            'End If

            'If txtBDRLetterID.Text <> "" And (txtWorkOrderNo.Text <> "" Or txtPTRNo.Text <> "") Then
            '    lblError.Text = "Only one allowed from these PTR No , Work Order No and BDR Letter Id "
            '    Exit Sub
            'End If

            'Dim strPTRNo = objInputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("HD_PTR_REF").Value
            'Dim strBDRLetterID = objInputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("HD_RE_BDR_LETTER").Value
            'Dim strWorkOrderNo = objInputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("WO_ID").Value

            'If strPTRNo <> "" And (strWorkOrderNo <> "" Or strBDRLetterID <> "") Then
            '    lblError.Text = "Only one allowed from these PTR No , Work Order No and BDR Letter Id "
            '    Exit Sub
            'End If

            'If strWorkOrderNo <> "" And (strPTRNo <> "" Or strBDRLetterID <> "") Then
            '    lblError.Text = "Only one allowed from these PTR No , Work Order No and BDR Letter Id "
            '    Exit Sub
            'End If

            'If strBDRLetterID <> "" And (strWorkOrderNo <> "" Or strPTRNo <> "") Then
            '    lblError.Text = "Only one allowed from these PTR No , Work Order No and BDR Letter Id "
            '    Exit Sub
            'End If
            'End commented code
            'Here Back end Method Call
            objOutputXml = objbzCall.Update(objInputXml)

            If hdQueryCategory.Value = "" Then
                hdQueryCategory.Value = Request.Form(ddlQueryCategory.UniqueID)
            End If
            If hdQuerySubCategory.Value = "" Then
                hdQuerySubCategory.Value = Request.Form(ddlQuerySubCategory.UniqueID)
            End If
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


               
                ViewState("vsMode") = ddlMode.SelectedValue
                ViewState("vsNxtFollowupDate") = txtNxtFollowupDate.Text

                txtDescription.Text = ""
                txtSolution.Text = ""
                txtFollowDesc.Text = ""

                ViewState("vsDescription") = txtDescription.Text
                ViewState("vsSolution") = txtSolution.Text
                ViewState("vsFollowDesc") = txtFollowDesc.Text

                rdTechanical.Enabled = False
                btnAssigneeHistory.Enabled = True
                btnHistory.Enabled = True
                btnFeedBack.Enabled = True
                hdPageHD_RE_ID.Value = objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("HD_RE_ID").Value
                txtLTRNo.Text = hdPageHD_RE_ID.Value
                hdEnPageHD_RE_ID.Value = objED.Encrypt(hdPageHD_RE_ID.Value)
                'code to hid and show popup image
                If hdPageHD_RE_ID.Value <> "" Then
                    Img2.Visible = False
                    Img3.Visible = True
                    Img4.Visible = True
                    Img5.Visible = True
                Else
                    Img2.Visible = True
                    Img3.Visible = False
                    Img4.Visible = False
                    Img5.Visible = False
                End If
                'End
                Bindata()
                ViewRecords()
                'fillCategoryName(objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("CALL_SUB_GROUP_ID").Value)
                'ddlQueryCategory.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("CALL_CATEGORY_ID").Value.Trim
                'fillSubCategoryName(objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("CALL_CATEGORY_ID").Value)
                'ddlQuerySubCategory.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("CALL_SUB_CATEGORY_ID").Value.Trim
                'txtPTRNo.Text = objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("HD_PTR_REF").Value
                'txtBDRLetterID.Text = objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("HD_RE_BDR_LETTER").Value
                'txtWorkOrderNo.Text = objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("WO_ID").Value
                hdCoordinator1.Value = objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("COORDINATOR1").Value
                hdCoordinator2.Value = objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("COORDINATOR2").Value
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
                    'If Request.Browser.Browser.ToString.ToUpper = "IE" Then
                    'If ConfigurationManager.AppSettings("WAIT_TIME") IsNot Nothing Then
                    'Dim intTime As Integer = Val(ConfigurationManager.AppSettings("WAIT_TIME"))
                    ' ClientScript.RegisterClientScriptBlock(Me.GetType, "strPopup", "<script language='javascript'>window.setTimeout('YesNoFunctional();'," + intTime.ToString + ");</script>")
                    'Else
                    '   ClientScript.RegisterClientScriptBlock(Me.GetType, "strPopup", "<script language='javascript'>window.setTimeout('YesNoFunctional();',1000);</script>")
                    'End If

                    'Else
                    'ClientScript.RegisterClientScriptBlock(Me.GetType, "strPopup", "<script language='javascript'>initPopUp();removeEvent(window, 'load', initPopUp);YesNoFunctional();</script>")
                    'strString = "<script language='javascript'>initPopUp();removeEvent(window, 'load', initPopUp);openCreditLimit('" & InvAmount & "');</script>"
                    'End If
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
        Finally

            'This part is handled in page load event through request.form
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
        End Try
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveInformation(PageOperation.Save)

    End Sub
    'This function is used to enable or disable save button according to rights 
    'This function called once call is saved
    'Here parameter passed is currently (Closed status ie "1")
    Sub EnabledDisabledSavebutton(ByVal strValue As String)
        If strValue = "1" Then
            btnSave.Enabled = False
            If Session("LoginSession") IsNot Nothing And Session("Security") IsNot Nothing Then
                If objeAAMS.REOPENCLOSEDBRLTR(Session("Security")) = "1" Then
                    hdSaveRights.Value = "1"
                    btnSave.Enabled = True
                    'If strStatus.ToUpper = "SOLVED ONLINE" Or strStatus.ToUpper = "SOLVED OFFLINE" Then
                    'End If                
                End If
            End If
        End If
    End Sub
    'This function is used to check whether user can update record or not acc to rights and conditions
    'This function called once call is saved
    'This is under hold
    Function SaveRights(ByVal strStatus As String) As Boolean
        Dim blnStatus As Boolean = False
        If hdSaveRights.Value = "1" Then
            If strStatus.ToUpper = "SOLVED ONLINE" Or strStatus.ToUpper = "SOLVED OFFLINE" Then
                If ddlQueryStatus.SelectedValue.Split("|").GetValue(1) <> "0" Then
                    blnStatus = True
                End If
            End If
        Else
            blnStatus = True
        End If
        Return blnStatus
    End Function

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("HDUP_CallLog.aspx?Action=I")
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try

            If hdPageHD_RE_ID.Value <> "" Then
                lblError.Text = ""
                txtDescription.Text = ViewState("vsDescription")
                txtSolution.Text = ViewState("vsSolution")
                txtFollowDesc.Text = ViewState("vsFollowDesc")
                ddlMode.SelectedValue = ViewState("vsMode")
                txtNxtFollowupDate.Text = ViewState("vsNxtFollowupDate")

                ViewRecords()
            Else
                Dim strQueryString As String = Request.QueryString.ToString
                Response.Redirect("HDUP_CallLog.aspx?" + strQueryString)
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
        Return str
    End Function

    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
        Dim objInputXml, objOutputXml As New XmlDocument
        'If eventArgument <> "" Then
        '    Dim objCallCategory As New AAMS.bizBRHelpDesk.bzCallCategory


        '    Dim id As String = eventArgument
        '    objInputXml.LoadXml("<HD_SEARCHCALLCATEGORY_INPUT><CALL_CATEGORY_NAME /><CALL_SUB_GROUP_ID /><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHCALLCATEGORY_INPUT>")
        '    objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_GROUP_ID").InnerText = id
        '    objOutputXml = objCallCategory.Search(objInputXml)
        '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
        '        str = objOutputXml.OuterXml
        '    Else
        '        str = ""
        '    End If
        'End If
        If eventArgument <> "" Then
            ' objOutputXml.Load("c:\agencyd.xml")
            ' str = objOutputXml.OuterXml
            AgencyViewByOfficeID(eventArgument)
        End If

    End Sub

    Protected Sub rdTechanical_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdTechanical.CheckedChanged
        If hdPageStatus.Value = "I" Or hdPageStatus.Value = "" Then
            'This code added on 22 july 08
            'This code is for passing lcode when user swap query group after selecting agencyName
            If hdCallAgencyName.Value <> "" Then
                'Response.Redirect("TCUP_Calllog.aspx?LCodeTemp=" + objED.Decrypt(hdCallAgencyName.Value.Split("|").GetValue(0)))

                If hdEnCallAgencyName_LCODE.Value = "" Then
                    hdEnCallAgencyName_LCODE.Value = objED.Encrypt(hdCallAgencyName.Value.Split("|").GetValue(0))
                End If
                Response.Redirect("TCUP_Calllog.aspx?LCodeTemp=" + hdEnCallAgencyName_LCODE.Value)
            Else
                Response.Redirect("TCUP_Calllog.aspx?" + hdQueryString.Value)
            End If
        Else
            Response.Redirect("TCUP_Calllog.aspx?" + hdQueryString.Value)
        End If
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
                    ' txtAddress.Text = .Attributes("ADDRESS").Value()
                    txtAddress.Text = .Attributes("ADDRESS").Value() + vbCrLf + .Attributes("ADDRESS1").Value()

                    txtCity.Text = .Attributes("CITY").Value()
                    txtCountry.Text = .Attributes("COUNTRY").Value()
                    txtPhone.Text = .Attributes("PHONE").Value()
                    txtFax.Text = .Attributes("FAX").Value()
                    txtOnlineStatus.Text = .Attributes("ONLINE_STATUS").Value()
                    txtOfficeId.Text = .Attributes("OFFICEID").Value()
                    hdEnAOffice.Value = objED.Encrypt(.Attributes("Aoffice").Value())
                    hdAOffice.Value = .Attributes("Aoffice").Value()
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
                    ' txtAgencyName.Text = .Attributes("NAME").Value()
                    '  txtAddress.Text = .Attributes("ADDRESS").Value()
                    ' txtCity.Text = .Attributes("CITY").Value()
                    ' txtCountry.Text = .Attributes("COUNTRY").Value()
                    ' txtPhone.Text = .Attributes("PHONE").Value()
                    ' txtFax.Text = .Attributes("FAX").Value()
                    ' txtOnlineStatus.Text = .Attributes("ONLINE_STATUS").Value()
                    'txtOfficeId.Text = .Attributes("OFFICEID").Value()

                    ' txtPincode.Text = .Attributes("PINCODE").Value()
                    ' txtEmail.Text = .Attributes("EMAIL").Value()

                    ' hdEnAOffice.Value = objED.Encrypt(.Attributes("Aoffice").Value())
                    ' hdAOffice.Value = .Attributes("Aoffice").Value()
                    'hdCallAgencyName.Value = .Attributes("LOCATION_CODE").Value + "|" + .Attributes("NAME").Value + "|" + .Attributes("ADDRESS").Value + "|" + "" + "|" + .Attributes("CITY").Value + "|" + .Attributes("COUNTRY").Value + "|" + .Attributes("PHONE").Value + "|" + .Attributes("OfficeID").Value + "|" + .Attributes("FAX").Value + "|" + .Attributes("ONLINE_STATUS").Value + "|" + .Attributes("Aoffice").Value + "|" + .Attributes("PINCODE").Value() + "|" + .Attributes("Email").Value()
                    'hdEnCallAgencyName_LCODE.Value = objED.Encrypt(.Attributes("LOCATION_CODE").Value)
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

    Sub ViewRecords()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzCall As New AAMS.bizBRHelpDesk.bzCall
        objInputXml.LoadXml("<HD_VIEW_CALLREQUEST_INPUT><HD_RE_ID></HD_RE_ID></HD_VIEW_CALLREQUEST_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = hdPageHD_RE_ID.Value
        'Here Back end Method Call 
      
        '    <CALLREQUEST HD_QUERY_GROUP_ID="1"
        '    FEEDBACK_ID="" LCODE="30343" AGENCYNAME="Annu Tours & Travels"
        'ADDRESS = "97-c, Gulmohar Shopping Complex, Sector - 15, Noida"
        '    COUNTRY="India" CITY="Noida" PHONE="+(91)-(11)-66242896" FAX="0120-4572257" 
        '    ONLINE_STATUS="VISTA" AOFFICE="DEL" OFFICEID="DELVS34BI" HD_RE_ID="8055" 
        '    CALLER_NAME="Abhishek" CALL_SUB_GROUP_ID="20" CALL_CATEGORY_ID="127"
        '    CALL_SUB_CATEGORY_ID="1" CALL_DURATION="0001" HD_SEVERITY_ID="1" 
        '    HD_STATUS_ID="3" COORDINATOR1="24" COORDINATOR1_NAME="Admin" COORDINATOR2=""
        '    TT_NUMBAR="" CONTACT_TYPE_ID="1" DISPOSITION_ID="" 
        'HD_RE_OPEN_DATE = "18/08/2010 12:03"
        '    HD_RE_CLOSED_DATE="18/08/2010 12:03" AssignedTo="1"
        'AssignedDateTime = "18/08/2010 12:03"
        '    LoggedBy="24" LoggedByName="Admin" LeftDateTime="" 
        '    WO_ID="" WO_NUMBER="" HD_PTR_ID="" HD_PTR_REF="" HD_RE_BDR_ID="" 
        '    HD_RE_BDR_TICKETS="" HD_STATUS_CLOSE="3|1" PINCODE="" 
        '    EMAIL="info@annutravels.com" TITLE="" /> 

        objOutputXml = objbzCall.View(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            With objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST")
                hdPageLCode.Value = .Attributes("LCODE").Value
                hdEnPageLCode.Value = objED.Encrypt(hdPageLCode.Value)
                hdEnFeedBackId.Value = objED.Encrypt(.Attributes("FEEDBACK_ID").Value)
                hdFeedBackId.Value = .Attributes("FEEDBACK_ID").Value
                hdAOffice.Value = .Attributes("AOFFICE").Value
                hdEnAOffice.Value = objED.Encrypt(.Attributes("AOFFICE").Value)
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

                hdEnWorkOrderNo.Value = objED.Encrypt(.Attributes("WO_ID").Value)
                hdEnPTRNo.Value = objED.Encrypt(.Attributes("HD_PTR_ID").Value)
                hdEnBDRLetterID.Value = objED.Encrypt(.Attributes("HD_RE_BDR_ID").Value)

                hdWorkOrderNo.Value = .Attributes("WO_ID").Value
                hdPTRNo.Value = .Attributes("HD_PTR_ID").Value
                hdBDRLetterID.Value = .Attributes("HD_RE_BDR_ID").Value


                ddlQuerySubGroup.SelectedValue = .Attributes("CALL_SUB_GROUP_ID").Value

                fillCategoryName(ddlQuerySubGroup.SelectedValue)
                Dim liQueryCategory As New ListItem
                liQueryCategory = ddlQueryCategory.Items.FindByValue(.Attributes("CALL_CATEGORY_ID").Value)
                If liQueryCategory IsNot Nothing Then
                    ddlQueryCategory.SelectedValue = liQueryCategory.Value
                End If

                'If ddlQueryCategory.Items.Count > 1 Then
                '    ddlQueryCategory.SelectedValue = .Attributes("CALL_CATEGORY_ID").Value
                'End If


                fillSubCategoryName(ddlQueryCategory.SelectedValue)

                Dim liQuerySubCategory As New ListItem
                liQuerySubCategory = ddlQuerySubCategory.Items.FindByValue(getDefaultValue(.Attributes("CALL_SUB_CATEGORY_ID").Value))
                If liQuerySubCategory IsNot Nothing Then
                    ddlQuerySubCategory.SelectedValue = liQuerySubCategory.Value
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
                hdAOffice.Value = .Attributes("AOFFICE").Value
                hdEnAOffice.Value = objED.Encrypt(.Attributes("AOFFICE").Value)
                ' fillCoordinator("1", .Attributes("AOFFICE").Value)
                ' hdCoordinator1.Value = .Attributes("COORDINATOR1").Value
                'ddlCoordinator1.SelectedValue = .Attributes("COORDINATOR1").Value
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

                '  fillCoordinator("2", .Attributes("AOFFICE").Value)
                '  hdCoordinator2.Value = .Attributes("COORDINATOR2").Value
                Dim liCoordinator2 As New ListItem
                liCoordinator2 = ddlCoordinator2.Items.FindByValue(.Attributes("COORDINATOR2").Value)
                If liCoordinator2 IsNot Nothing Then
                    ddlCoordinator2.SelectedValue = liCoordinator2.Value
                End If
                ' ddlCoordinator2.SelectedValue = .Attributes("COORDINATOR2").Value

                txtPTRNo.Text = .Attributes("HD_PTR_REF").Value
                txtBDRLetterID.Text = .Attributes("HD_RE_BDR_ID").Value
                txtWorkOrderNo.Text = .Attributes("WO_NUMBER").Value
                ddlContactType.SelectedValue = .Attributes("CONTACT_TYPE_ID").Value
                ddlDisposition.SelectedValue = .Attributes("DISPOSITION_ID").Value
                txtLoggedDate.Text = .Attributes("HD_RE_OPEN_DATE").Value
                txtCloseDateTime.Text = .Attributes("HD_RE_CLOSED_DATE").Value
                ddlTeamAssignedTo.SelectedValue = .Attributes("AssignedTo").Value
                txtDateAssigned.Text = .Attributes("AssignedDateTime").Value
                txtLoggedBy.Text = .Attributes("LoggedByName").Value
                txtTitle.Text = .Attributes("TITLE").Value

                '1052|test67|ewrw||Agra|India|||||AGR
                hdCallAgencyName.Value = .Attributes("LCODE").Value + "|" + .Attributes("AGENCYNAME").Value + "|" + .Attributes("ADDRESS").Value + "|" + "" + "|" + .Attributes("CITY").Value + "|" + .Attributes("COUNTRY").Value + "|" + .Attributes("PHONE").Value + "|" + .Attributes("OFFICEID").Value + "|" + .Attributes("FAX").Value + "|" + .Attributes("ONLINE_STATUS").Value + "|" + .Attributes("AOFFICE").Value
                hdEnCallAgencyName_LCODE.Value = objED.Encrypt(.Attributes("LCODE").Value)
                If .Attributes("HD_STATUS_CLOSE").Value.Split("|").GetValue(1) = "1" Then
                    'Commented on 22 July 08
                    'In place of this new function is called
                    '   btnSave.Enabled = False
                    EnabledDisabledSavebutton("1")
                End If
                BindDescription()

                BindSolution()

                BindFollowUp()
            End With
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Function getDefaultValue(ByVal strCALL_SUB_CATEGORY_ID As String) As String
        Dim strValue As String = ""
        Dim objXmlDoc As New XmlDocument
        Dim objNode As XmlNode
        If hdSubCategory.Value <> "" Then
            objXmlDoc.LoadXml(hdSubCategory.Value)
            objNode = objXmlDoc.DocumentElement.SelectSingleNode("CSC[@CSCI='" + strCALL_SUB_CATEGORY_ID + "']")
            If objNode IsNot Nothing Then
                strValue = strCALL_SUB_CATEGORY_ID + "," + objNode.Attributes("HEI").Value + "," + objNode.Attributes("HSI").Value + "," + objNode.Attributes("TI").Value + "," + objNode.Attributes("CTI").Value + "," + objNode.Attributes("TE").Value
            End If
        End If


        Return strValue
    End Function
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

    Sub BindFollowUp()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objCall As New AAMS.bizBRHelpDesk.bzCall
        objInputXml.LoadXml("<HD_GETREQUESTFOLLOWUP_INPUT><HD_RE_ID></HD_RE_ID></HD_GETREQUESTFOLLOWUP_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = hdPageHD_RE_ID.Value
        objOutputXml = objCall.ViewRequestFollowup(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)

            If ds.Tables("FOLLOWUPDETAILS").Rows.Count <> 0 Then
                gvFollowUp.DataSource = ds.Tables("FOLLOWUPDETAILS")
                gvFollowUp.DataBind()
                If Not ds.Tables("REQUESTFOLLOWUP").Rows(0)("NO_OF_FOLLOWUP") Is Nothing Then
                    txtNoOfFollowup.Text = ds.Tables("REQUESTFOLLOWUP").Rows(0)("NO_OF_FOLLOWUP").ToString
                End If
                'If Not ds.Tables("REQUESTFOLLOWUP").Rows(0)("DATETIME") Is Nothing Then
                '    txtNxtFollowupDate.Text = ds.Tables("REQUESTFOLLOWUP").Rows(0)("DATETIME").ToString
                'End If

            Else
                gvFollowUp.DataSource = Nothing
                gvFollowUp.DataBind()
            End If

        Else
            If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value <> "No Record Found!") Then
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            Else
                ' code for binding header when no record found.
                'Dim dt As New DataTable
                'Dim dc As New DataColumn("EmployeeName", GetType(String))
                'Dim dc1 As New DataColumn("DATETIME", GetType(String))
                'Dim dc2 As New DataColumn("CONTACT_TYPE_Name", GetType(String))
                'Dim dc3 As New DataColumn("FOLLOWUP_DESC", GetType(String))
                'Dim dr As DataRow
                'dt.Columns.Add(dc)
                'dt.Columns.Add(dc1)
                'dt.Columns.Add(dc2)
                'dt.Columns.Add(dc3)
                'dr = dt.NewRow()
                'dt.Rows.Add(dr)
                'gvFollowUp.DataSource = dt
                'gvFollowUp.DataBind()

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
            objInputXml.LoadXml("<HD_SEARCHCALLCATEGORY_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID><CALL_CATEGORY_NAME /><CALL_SUB_GROUP_ID /><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHCALLCATEGORY_INPUT>")
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
            ddlQueryCategory.Items.Insert(0, New ListItem("--Select One--", ""))
        Else
            ddlQueryCategory.Items.Clear()
            ddlQueryCategory.Items.Insert(0, New ListItem("--Select One--", ""))
        End If
    End Sub

    Public Sub fillSubCategoryName(ByVal strId As String)
        If strId <> "" Then
            ddlQuerySubCategory.Items.Clear()
            Dim objbzCallSubCategory As New AAMS.bizBRHelpDesk.bzCallSubCategory
            Dim ds As New DataSet
            Dim objInputXml, objOutputXml As New XmlDocument
            objInputXml.LoadXml("<HD_SEARCHCALLSUBCATEGORY_INPUT><CALL_SUB_CATEGORY_NAME /><CALL_CATEGORY_ID /><CALL_SUB_GROUP_ID /><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHCALLSUBCATEGORY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_GROUP_ID").InnerText = ddlQuerySubGroup.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("CALL_CATEGORY_ID").InnerText = strId

            objOutputXml = objbzCallSubCategory.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                '    objXmlReader = New XmlNodeReader(objOutputXml)
                '   ds.ReadXml(objXmlReader)
                '  <CALL_SUB_CATEGORY CALL_SUB_CATEGORY_ID="902" CALL_SUB_CATEGORY_NAME="Checked" CALL_CATEGORY_NAME="Availability" CALL_SUB_GROUP_NAME="Air" HD_STATUS_ID="3|1" CONTACT_TYPE_ID="1" HD_SEVERITY_ID="1" TEAM_ID="1" TITLE="" /> 

                ddlQuerySubCategory.Items.Clear()
                For Each xNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("CALL_SUB_CATEGORY")
                    ddlQuerySubCategory.Items.Add(New ListItem(xNode.Attributes("CALL_SUB_CATEGORY_NAME").Value.Trim, xNode.Attributes("CALL_SUB_CATEGORY_ID").Value.Trim & "," & xNode.Attributes("HD_SEVERITY_ID").Value.Trim & "," & xNode.Attributes("HD_STATUS_ID").Value.Trim & "," & xNode.Attributes("TEAM_ID").Value.Trim & "," & xNode.Attributes("CONTACT_TYPE_ID").Value.Trim & "," & xNode.Attributes("TITLE").Value.Trim))
                Next


                ddlQuerySubCategory.Items.Insert(0, New ListItem("--Select One--", ""))
            Else
                ddlQuerySubCategory.Items.Clear()
                ddlQuerySubCategory.Items.Insert(0, New ListItem("--Select One--", ""))
            End If

        Else
            ddlQuerySubCategory.Items.Clear()
            ddlQuerySubCategory.Items.Insert(0, New ListItem("--Select One--", ""))
        End If
    End Sub
    'Public Sub fillSubCategoryName(ByVal strId As String)
    '    If strId <> "" Then
    '        ddlQuerySubCategory.Items.Clear()
    '        Dim objbzCallSubCategory As New AAMS.bizBRHelpDesk.bzCallSubCategory
    '        Dim ds As New DataSet
    '        Dim objInputXml, objOutputXml As New XmlDocument
    '        objInputXml.LoadXml("<HD_SEARCHCALLSUBCATEGORY_INPUT><CALL_SUB_CATEGORY_NAME /><CALL_CATEGORY_ID /><CALL_SUB_GROUP_ID /><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHCALLSUBCATEGORY_INPUT>")
    '        objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_GROUP_ID").InnerText = ddlQuerySubGroup.SelectedValue
    '        objInputXml.DocumentElement.SelectSingleNode("CALL_CATEGORY_ID").InnerText = strId

    '        objOutputXml = objbzCallSubCategory.Search(objInputXml)
    '        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '            '    objXmlReader = New XmlNodeReader(objOutputXml)
    '            '   ds.ReadXml(objXmlReader)
    '            ddlQuerySubCategory.Items.Clear()
    '            For Each xNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("CALL_SUB_CATEGORY")
    '                ddlQuerySubCategory.Items.Add(New ListItem(xNode.Attributes("CALL_SUB_CATEGORY_NAME").Value.Trim, xNode.Attributes("CALL_SUB_CATEGORY_ID").Value.Trim & "," & xNode.Attributes("HD_SEVERITY_ID").Value.Trim & "," & xNode.Attributes("HD_STATUS_ID").Value.Trim & "," & xNode.Attributes("TEAM_ID").Value.Trim & "," & xNode.Attributes("CONTACT_TYPE_ID").Value.Trim & "," & xNode.Attributes("TITLE").Value.Trim))
    '            Next


    '            ddlQuerySubCategory.Items.Insert(0, New ListItem("--Select One--", ""))
    '        Else
    '            ddlQuerySubCategory.Items.Clear()
    '            ddlQuerySubCategory.Items.Insert(0, New ListItem("--Select One--", ""))
    '        End If

    '    Else
    '        ddlQuerySubCategory.Items.Clear()
    '        ddlQuerySubCategory.Items.Insert(0, New ListItem("--Select One--", ""))
    '    End If
    'End Sub

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
                    ddlCoordinator1.Items.Insert(0, New ListItem("--Select One--", ""))
                Else
                    ddlCoordinator1.Items.Clear()
                    ddlCoordinator2.DataSource = ds.Tables("COORDINATOR")
                    ddlCoordinator2.DataTextField = "EMPLOYEE_NAME"
                    ddlCoordinator2.DataValueField = "EmployeeID"
                    ddlCoordinator2.DataBind()
                    ddlCoordinator2.Items.Insert(0, New ListItem("--Select One--", ""))
                End If

            End If
        End If
    End Sub

    Protected Sub btnHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistory.Click

    End Sub
    Private Sub ViewCallLogInsertDetails()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objhdPtrView As New AAMS.bizBRHelpDesk.bzPTR
        objOutputXml = objhdPtrView.ListPTRConfigValue()

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            Dim xNode As XmlNode
            For Each xNode In objOutputXml.DocumentElement.SelectNodes("CONFIGVALUE")
                If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_HD_REQUEST_SEVERITY" Then
                    ddlPriority.SelectedValue = xNode.Attributes("FIELD_VALUE").Value.Trim().Split("|").GetValue(0)
                End If
                If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_HD_REQUEST_STATUS" Then
                    ddlQueryStatus.SelectedValue = xNode.Attributes("FIELD_VALUE").Value.Trim().Split("|").GetValue(0) & "|" & "0"
                    If ddlQueryStatus.SelectedValue = "" Then
                        ddlQueryStatus.SelectedValue = xNode.Attributes("FIELD_VALUE").Value.Trim().Split("|").GetValue(0) & "|" & "1"
                    End If
                End If
                If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_HD_REQUEST_CONTACT_TYPE" Then
                    ddlContactType.SelectedValue = xNode.Attributes("FIELD_VALUE").Value.Trim()
                End If
                If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_HD_FUNCTIONAL_ASSIGNEDTO" Then
                    ddlTeamAssignedTo.SelectedValue = xNode.Attributes("FIELD_VALUE").Value.Trim()
                End If
            Next
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Protected Sub rdFunctional_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdFunctional.CheckedChanged

    End Sub

    Enum PageOperation
        Save = 1
        ReSave = 2
    End Enum

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
            ddlCoordinator1.Items.Insert(0, New ListItem("--Select One--", ""))
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
                fillCoordinator()
                ddlCoordinator1.SelectedValue = objeAAMS.EmployeeID(Session("Security"))
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
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

