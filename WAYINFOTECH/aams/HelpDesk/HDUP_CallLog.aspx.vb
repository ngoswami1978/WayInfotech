Imports System.IO
Imports System.Text
Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Web.Services.Protocols
Imports AjaxControlToolkit
Partial Class HelpDesk_HDUP_CallLog
    Inherits System.Web.UI.Page
    'Inherits BasePage
    Implements ICallbackEventHandler
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Protected strIndex As String
    Protected flagPermission As String
    Dim str, strAjaxAgencyname, strAjaxOfficeId, strAjaxLmOffice, strAjaxLmRegion, strAjaxLmAgency As String
    Dim objED As New EncyrptDeCyrpt
    Dim startTime As TimeSpan
    Dim starrDateAndTime As Date
    Dim dtMultiLTR As New DataTable

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
            txtLoggedDate.Text = Request.Form("txtLoggedDate")
            'hdAOffice.Value = strData.Split("|").GetValue(10)



          
            If HdRequireCallerName.Value <> "" Then
                HdRequireCallerName.Value = ""
                FillCallerName()

            End If

            txtCallerName.Text = Request.Form("txtCallerName")

            'Check Security
            checkSecurity()
            'code for officeID

            Dim m As ClientScriptManager = Me.ClientScript
            str = m.GetCallbackEventReference(Me, "args", "ReceiveServerDataFunctional", "'this is context from server'")
            Dim strCallback As String = "function CallServerFunctional(args,context){" + str + ";}"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)

            str = m.GetCallbackEventReference(Me, "args", "ReceiveServerDataSubCategory", "'this is context from server'")
            strCallback = "function CallServerFunctional1(args,context){" + str + ";}"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer1", strCallback, True)

            str = m.GetCallbackEventReference(Me, "args", "ReceiveServerDataCallerName", "'this is context from server'")
            strCallback = "function CallServerFunctional2(args,context){" + str + ";}"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer2", strCallback, True)

            txtOfficeId.Attributes.Add("onfocusout", "return FillAgencyDetailsFunctional()")
            '
            'Dim m As ClientScriptManager = Me.ClientScript
            'str = m.GetCallbackEventReference(Me, "args", "ReceiveServerData", "'this is context from server'")
            'Dim strCallback As String = "function CallServer(args,context){" + str + ";}"
            'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)
            ddlQuerySubGroup.Attributes.Add("OnChange", "return fillCategoryFunctional();")
            ddlQueryCategory.Attributes.Add("OnChange", "return fillSubCategoryFunctional();")
            ddlQuerySubCategory.Attributes.Add("OnChange", "return fillSubCategoryDefaultValues();")
            'hdQueryCategory.Value = Request.Form(
            ddlPageNumber.Attributes.Add("Onclick", "return ManageCallLogPagePageno();")

            ddlQueryStatus.Attributes.Add("OnChange", "return ResetAssingnedToValueForFunc();")

            If Request.QueryString("Popup") Is Nothing Then
                lnkClose.Visible = False
            Else
                lnkClose.Visible = True
            End If
            If hdReSave.Value = "1" Then
                SaveInformation(PageOperation.ReSave)
            End If
            If Not Page.IsPostBack Then

                Session("TempMultiLTRXml") = Nothing
                Session("DESC") = Nothing
                Session("SOLUTION") = Nothing

                hdDurationDate.Value = DateAndTime.Now.ToString()

                hdLoggedDatetime.Value = DateAndTime.Now.ToString("dd/MM/yyyy HH:mm") '"yyyy-MM-dd HH:mm:ss")

                hdstartTimeSpan.Value = DateTime.Now.TimeOfDay.ToString()

                hdEnFunctional.Value = objED.Encrypt("Functional") 'Storing encrypted value in hiddenfield 
                If Request.QueryString("TabType") IsNot Nothing Then
                    hdTabType.Value = Request.QueryString("TabType")
                End If

                If Request.QueryString("QueryGroup") IsNot Nothing Then
                    Dim strQueryString = Request.QueryString.ToString

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
                If Request.QueryString("MULTIHD_RE_ID") Is Nothing Then
                    If Not Request.QueryString("strStatus") Is Nothing Then
                        Dim EnstrStatus As String
                        EnstrStatus = objED.Decrypt(Request.QueryString("strStatus"))
                        If EnstrStatus.ToUpper = "TECHNICAL" Then
                            Dim strQueryString As String = Request.QueryString.ToString
                            Response.Redirect("TCUP_CallLog.aspx?" + strQueryString)
                        End If
                    End If
                    btnSaveAll.Enabled = False
                Else
                    hdMULTIHD_RE_ID.Value = Request.QueryString("MULTIHD_RE_ID").ToString
                    If Not Request.QueryString("strStatus") Is Nothing Then
                        Dim EnstrStatus As String
                        EnstrStatus = Request.QueryString("strStatus")
                        If EnstrStatus.ToUpper = "TECHNICAL" Then
                            Dim strQueryString As String = Request.QueryString.ToString
                            Response.Redirect("TCUP_CallLog.aspx?" + strQueryString)
                        End If
                    End If

                End If
                If Not Request.QueryString("Action") Is Nothing Then
                    ' Dim EnstrAction As String
                    ' EnstrAction = Request.QueryString("Action")
                    hdPageStatus.Value = Request.QueryString("Action")
                    'hdPageStatus.Value = Request.QueryString("Action").ToString
                End If

                If Not Request.QueryString("LCode") Is Nothing And Request.QueryString("MULTIHD_RE_ID") Is Nothing Then
                    hdEnPageLCode.Value = Request.QueryString("LCode").ToString
                    hdPageLCode.Value = objED.Decrypt(Request.QueryString("LCode").ToString)
                    If Request.QueryString("HD_RE_ID") Is Nothing Then
                        AgencyView()
                    End If
                End If

                If Not Request.QueryString("LCodeTemp") Is Nothing And Request.QueryString("MULTIHD_RE_ID") Is Nothing Then
                    hdEnPageLCode.Value = Request.QueryString("LCodeTemp").ToString
                    hdPageLCode.Value = objED.Decrypt(Request.QueryString("LCodeTemp").ToString)

                    AgencyView()
                    hdLoggedDatetime.Value = DateAndTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
                    If hdPageStatus.Value.Trim() = "I" Then
                        txtLoggedDate.Text = DateAndTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
                    End If
                    FillCallerName()
                    AutoCompleteExtenderForCaller.ContextKey = objED.Decrypt(Request.QueryString("LCodeTemp").ToString) + "|" + txtOfficeId.Text


                End If
                If Not Request.QueryString("HD_RE_ID") Is Nothing And Request.QueryString("MULTIHD_RE_ID") Is Nothing Then
                    hdEnPageHD_RE_ID.Value = Request.QueryString("HD_RE_ID").ToString
                    hdPageHD_RE_ID.Value = objED.Decrypt(Request.QueryString("HD_RE_ID").ToString)
                    rdTechanical.Enabled = False
                Else
                    btnAssigneeHistory.Enabled = False
                    btnHistory.Enabled = False
                    btnFeedBack.Enabled = False
                End If

                BindMultiLTR()
                LoadAllControl()
                Bindata()
                'If hdPageHD_RE_ID.Value <> "" Then
                '    ViewRecords()
                'Else
                'End If
                If hdPageHD_RE_ID.Value <> "" Then
                    ViewRecords("single")
                    txtIRNumber.Focus()
                    'txtCallerName.Focus()
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
                ImgIRNo.Visible = True
            Else
                Img2.Visible = True
                Img3.Visible = False
                Img4.Visible = False
                Img5.Visible = False
                ImgIRNo.Visible = False
            End If

            '***************************************************************************************
            'Code of Security Check
            'objeAAMS.ExpirePageCache()

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            If Session("AllRecordUpdates") IsNot Nothing Then
                lblError.Text = Session("AllRecordUpdates").ToString
                Session("AllRecordUpdates") = Nothing
                ' HdFuncManForDecAndSoln.Value = "T"
            End If
        End Try

    End Sub

    Sub BindMultiLTR()
        If Not Request.QueryString("MULTIHD_RE_ID") Is Nothing Then
            Dim drNewRow As DataRow
            Dim objCol As DataColumn
            Dim intRowIndex As Integer
            Dim arstrMultiLTR() As String
            Session("LTRMultiEdit") = Request.QueryString("MULTIHD_RE_ID").ToString

            objCol = New DataColumn("RowID", System.Type.GetType("System.Int32"))
            dtMultiLTR.Columns.Add(objCol)

            objCol = New DataColumn("LTR", System.Type.GetType("System.String"))
            dtMultiLTR.Columns.Add(objCol)

            objCol = New DataColumn("LCODE", System.Type.GetType("System.String"))
            dtMultiLTR.Columns.Add(objCol)

            objCol = New DataColumn("EnLTR", System.Type.GetType("System.String"))
            dtMultiLTR.Columns.Add(objCol)

            objCol = New DataColumn("EnLCODE", System.Type.GetType("System.String"))
            dtMultiLTR.Columns.Add(objCol)

            If Session("LTRMultiEdit") IsNot Nothing And Request.QueryString("MULTIHD_RE_ID").ToString <> "" Then
                arstrMultiLTR = Session("LTRMultiEdit").ToString.Split(",")

                For icount As Integer = 0 To arstrMultiLTR.Length - 1
                    drNewRow = dtMultiLTR.NewRow
                    drNewRow("RowID") = icount + 1
                    drNewRow("LTR") = arstrMultiLTR(icount).Split("|").GetValue(0)
                    drNewRow("LCODE") = arstrMultiLTR(icount).Split("|").GetValue(1)
                    drNewRow("EnLTR") = objED.Encrypt(arstrMultiLTR(icount).Split("|").GetValue(0))
                    drNewRow("EnLCODE") = objED.Encrypt(arstrMultiLTR(icount).Split("|").GetValue(1))
                    dtMultiLTR.Rows.Add(drNewRow)
                Next
            End If


            If Session("LTRMultiEdit") IsNot Nothing And Request.QueryString("MULTIHD_RE_ID").ToString <> "" Then
                If hdMultiLTR.Value = "" Then
                    hdEnPageLCode.Value = dtMultiLTR.Rows(0)("EnLCODE").ToString
                    hdPageLCode.Value = dtMultiLTR.Rows(0)("LCODE").ToString
                    AgencyView()
                    hdEnPageHD_RE_ID.Value = dtMultiLTR.Rows(0)("EnLTR").ToString
                    hdPageHD_RE_ID.Value = dtMultiLTR.Rows(0)("LTR").ToString
                    rdTechanical.Enabled = False
                    hdMultiLTR.Value = 0
                    btnAssigneeHistory.Enabled = True
                    btnHistory.Enabled = True
                    btnFeedBack.Enabled = True
                Else
                    intRowIndex = Val(hdMultiLTR.Value)
                    hdEnPageLCode.Value = dtMultiLTR.Rows(intRowIndex)("EnLCODE").ToString
                    hdPageLCode.Value = dtMultiLTR.Rows(intRowIndex)("LCODE").ToString
                    AgencyView()
                    hdEnPageHD_RE_ID.Value = dtMultiLTR.Rows(intRowIndex)("EnLTR").ToString
                    hdPageHD_RE_ID.Value = dtMultiLTR.Rows(intRowIndex)("LTR").ToString
                    rdTechanical.Enabled = False
                    ViewRecords("multi")
                    txtLTRNo.Focus()
                    ' txtCallerName.Focus()
                    btnAssigneeHistory.Enabled = True
                    btnHistory.Enabled = True
                    btnFeedBack.Enabled = True

                    Img2.Visible = False
                    Img3.Visible = True
                    Img4.Visible = True
                    Img5.Visible = True
                    ImgIRNo.Visible = True

                End If

                pnlPaging.Visible = True
                Dim count As Integer = dtMultiLTR.Rows.Count
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
                txtTotalRecordCount.Text = count.ToString

            End If
        End If
    End Sub

    Sub checkSecurity()
        Dim strBuilderFunctional As New StringBuilder
        Dim strBuilderTechnical As New StringBuilder
        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Log HD Call']").Count <> 0 Then
                strBuilderFunctional = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Log HD Call']").Attributes("Value").Value)
            End If
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Log Tech Call']").Count <> 0 Then
                strBuilderTechnical = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Log Tech Call']").Attributes("Value").Value)
            End If
            'When View rights disabled
            If strBuilderFunctional(0) = "0" And strBuilderTechnical(0) = "0" Then
                Response.Redirect("../NoRights.aspx")
                btnSave.Enabled = False
            End If
            If strBuilderFunctional(0) = "0" And strBuilderTechnical(0) <> "0" Then
                Dim strQueryString = Request.QueryString.ToString
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
            If objeAAMS.ReturnViewPermission(Session("Security"), "Feedback Action") = "0" Then
                btnFeedBack.Enabled = False
            End If
        Else
            strBuilderFunctional = objeAAMS.SecurityCheck(31)
        End If
    End Sub

    Sub LoadAllControl()
        Try
            'objeAAMS.BindDropDown(ddlContactType, "CONTACTTYPE", True)
            objeAAMS.BindDropDown(ddlQuerySubGroup, "QuerySubGroup", True, 1)
            objeAAMS.BindDropDown(ddlQueryStatus, "QueryStatusWithClosed", True, 1)
            objeAAMS.BindDropDown(ddlPriority, "QUERYPRIORITY", True, 1)
            objeAAMS.BindDropDown(ddlTeamAssignedTo, "TEAM", True, 1)
            objeAAMS.BindDropDown(ddlContactType, "CONTACTTYPE", True, 1)
            objeAAMS.BindDropDown(ddlMode, "CONTACTTYPE", True, 2)
            objeAAMS.BindDropDown(ddlDisposition, "DISPOSITION", True, 2)
            'objeAAMS.BindDropDown(ddlCoordinator1, "COORDINATOR1", True)
            'objeAAMS.BindDropDown(ddlCoordinator2, "COORDINATOR2", True)
            objeAAMS.BindDropDown(drpAirLineName, "AIRLINE", True, 1)


            'Code to fill Query Category
            Dim objCallCategory As New AAMS.bizHelpDesk.bzCallCategory
            Dim objCallCategory1 As New AAMS.bizHelpDesk.bzDisposition
            Dim objSecurityXml As New XmlDocument

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
            Dim objCallSubCategory As New AAMS.bizHelpDesk.bzCallSubCategory
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
            Dim objbzCoordinator As New AAMS.bizHelpDesk.bzCoordinator
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


            'for PTR combo



            ViewCallLogInsertDetails()
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            hdLimited_To_Aoffice.Value = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        End If
                    End If
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            hdLimited_To_Region.Value = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        End If
                    End If
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        If Not Session("LoginSession") Is Nothing Then
                            hdLimited_To_OwnAgency.Value = Session("LoginSession").ToString().Split("|")(0)
                        End If
                    End If
                End If

            End If
            ' AutoCompleteExtender1.ContextKey = txtAgencyName.Text + "|" + txtOfficeId.Text + "|" + hdLimited_To_Aoffice.Value + "|" + hdLimited_To_OwnAgency.Value + "|" + hdLimited_To_Region.Value
            ' objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = "" 'txtAgencyName.text
            'objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerText = txtOfficeId.text

            'objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = hdLimited_To_Aoffice.value
            'objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = hdLimited_To_OwnAgency.value
            'objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = hdLimited_To_Region.value
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
                If Request.QueryString("MULTIHD_RE_ID") Is Nothing Then
                    TabText.Add("Linked LTR")
                End If

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
            strLogHDCallPermission = objeAAMS.ReturnViewPermission(Session("Security"), "Log HD Call")
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
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Linked HD Call") = "0" Then
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

    Sub SaveMultiLTR(ByVal Operation As Integer)

        Dim objInputXml, objOutputXml, objMultiLTRInputXml, objTempXml As New XmlDocument
        Dim strQueryStatus As String
        Dim strQuerySubCategory As String
        Dim objbzCall As New AAMS.bizHelpDesk.bzCall
        Dim file As HttpPostedFile
        Dim strFullFileName As String = ""
        Dim strFilePath As String = ""
        Dim strFileName As String = ""
        Dim blnAttributePresent As Boolean = False
        strQueryStatus = ddlQueryStatus.SelectedValue
        If strQueryStatus.Split("|").Length <> 2 Then
            lblError.Text = "Query Status is mandatory"
            Exit Sub
        End If
        'End

        Try
            objInputXml.LoadXml("<HD_UPDATE_CALLREQUEST_INPUT><CALLREQUEST HD_QUERY_GROUP_ID='' HD_RE_OPEN_DATE=''  LCODE ='' HD_RE_ID='' CALLER_NAME='' CALL_SUB_GROUP_ID='' CALL_CATEGORY_ID='' CALL_SUB_CATEGORY_ID='' CALL_DURATION=''  HD_SEVERITY_ID='' HD_STATUS_ID='' COORDINATOR1='' COORDINATOR2='' HD_PTR_REF='' TT_NUMBAR='' HD_RE_BDR_LETTER='' WO_ID='' CONTACT_TYPE_ID='' DISPOSITION_ID='' AssignedTo='' LoggedBy='' LeftDateTime='' DESCRIPTION='' SOLUTION='' FU_UP_CONTACT_TYPE_ID='' FOLLOWUP_DESC='' FOLLOWUP_DATETIME='' HD_STATUS_CLOSE='' RESAVE='' HD_RE_CLOSED_DATE='' TITLE='' AIRLINE_CODE='' DESCRIPTION_ATTACH='' SOLUTION_ATTACH='' HD_RE_BDR_ID='' IRNUMBER='' HD_IR_ID ='' HD_IR_REF ='' DS_MANDATORY=''  /></HD_UPDATE_CALLREQUEST_INPUT>")

            If Session("TempMultiLTRXml") IsNot Nothing Then
                objMultiLTRInputXml.LoadXml(Session("TempMultiLTRXml"))
            End If
            'objMultiLTRInputXml.LoadXml("<HD_UPDATE_CALLREQUESTSAVEALL_INPUT><CALLREQUESTDETAILS></CALLREQUESTDETAILS><CALLVIEWDETAILS></CALLVIEWDETAILS></HD_UPDATE_CALLREQUESTSAVEALL_INPUT>")

            With objInputXml.DocumentElement.SelectSingleNode("CALLREQUEST")

                'file = FileUploadDesc.PostedFile
                'If Not FileUploadDesc Is Nothing Then
                '    strFilePath = FileUploadDesc.PostedFile.FileName
                '    strFileName = "D" + Now.ToString("ddMMyyyy") + "_" + txtOfficeId.Text + "_" + hdPageHD_RE_ID.Value + "_" + Path.GetFileName(strFilePath)
                '    strFullFileName = Server.MapPath("../HelpDeskFiles/") + strFileName
                '    If strFilePath <> "" Then
                '        file.SaveAs(strFullFileName)
                '        .Attributes("DESCRIPTION_ATTACH").Value = strFileName

                '    End If
                'End If



                'file = FileUploadSol.PostedFile
                'If Not FileUploadSol Is Nothing Then
                '    strFilePath = FileUploadSol.PostedFile.FileName
                '    strFileName = "S" + Now.ToString("ddMMyyyy") + "_" + txtOfficeId.Text + "_" + hdPageHD_RE_ID.Value + "_" + Path.GetFileName(strFilePath)
                '    strFullFileName = Server.MapPath("../HelpDeskFiles/") + strFileName
                '    If strFilePath <> "" Then
                '        file.SaveAs(strFullFileName)
                '        .Attributes("SOLUTION_ATTACH").Value = strFileName

                '    End If
                'End If


                .Attributes("HD_QUERY_GROUP_ID").Value = "1"
                .Attributes("LCODE").Value = hdCallAgencyName.Value.Split("|").GetValue(0)
                .Attributes("HD_RE_ID").Value = hdPageHD_RE_ID.Value

                ' .Attributes("CALLER_NAME").Value = txtCallerName.Text

                If Request.Form(DlstCallerName.UniqueID) <> "" Then
                    .Attributes("CALLER_NAME").Value = Request.Form(DlstCallerName.UniqueID)
                End If

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

                .Attributes("CALL_DURATION").Value = "" 'strCallDuration & ":" & strCallDuration1

                'Date Time Needed on the case of New call Log
                If hdPageStatus.Value.Trim() = "I" Then
                    '@ Start of Commented On 10/11/10
                    'Try
                    '    If hdDurationDate.Value.Trim().Length > 0 Then
                    '        starrDateAndTime = Convert.ToDateTime(hdDurationDate.Value.Trim())
                    '    Else
                    '        starrDateAndTime = DateAndTime.Now
                    '    End If
                    'Catch ex As Exception
                    '    starrDateAndTime = DateAndTime.Now
                    'End Try
                    ' .Attributes("HD_RE_OPEN_DATE").Value = starrDateAndTime.ToString("yyyy-MM-dd HH:mm:ss")
                    '@End of  Commented On 10/11/10

                    '@ Start of code On 10/11/10
                    If txtLoggedDate.Text.Trim.Length > 0 Then
                        .Attributes("HD_RE_OPEN_DATE").Value = objeAAMS.GetDateFormat(txtLoggedDate.Text, "dd/MM/yyyy HH:mm:ss", "yyyy-MM-dd HH:mm:ss", "/") ' starrDateAndTime.ToString("yyyy-MM-dd HH:mm:ss")
                    Else
                        .Attributes("HD_RE_OPEN_DATE").Value = objeAAMS.GetDateFormat(DateAndTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), "dd/MM/yyyy HH:mm:ss", "yyyy-MM-dd HH:mm:ss", "/")
                    End If

                    '@ End of code On 10/11/10

                Else

                    If hdCallSatrtDateTimeInitialy.Value.Trim().Length > 0 Then
                        Try
                            '.Attributes("HD_RE_OPEN_DATE").Value = dtime.ToString("yyyy-MM-dd HH:mm:ss")
                            .Attributes("HD_RE_OPEN_DATE").Value = objeAAMS.GetDateFormat(hdCallSatrtDateTimeInitialy.Value.Trim(), "dd/MM/yyyy HH:mm:ss", "yyyy-MM-dd HH:mm:ss", "/")

                        Catch ex As Exception
                            .Attributes("HD_RE_OPEN_DATE").Value = DateAndTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                        End Try
                    End If

                End If
                .Attributes("CALL_DURATION").Value = ""
                'HD_RE_OPEN_DATE

                .Attributes("HD_SEVERITY_ID").Value = ddlPriority.SelectedValue
                .Attributes("HD_STATUS_ID").Value = strQueryStatus.Split("|").GetValue(0)
                .Attributes("HD_STATUS_CLOSE").Value = strQueryStatus ' strQueryStatus.Split("|").GetValue(1) changed on 16 apr 10 to mactch with view output
                .Attributes("COORDINATOR1").Value = ddlCoordinator1.SelectedValue
                .Attributes("COORDINATOR2").Value = ddlCoordinator2.SelectedValue
                'If txtPTRNo.Text = "" Then
                '    .Attributes("HD_PTR_REF").Value = Request.Form("txtPTRNo")
                'Else
                '    .Attributes("HD_PTR_REF").Value = txtPTRNo.Text
                'End If

                If ddlPTRNo.SelectedValue <> "" Then
                    .Attributes("HD_PTR_REF").Value = ddlPTRNo.SelectedItem.Text
                End If
                .Attributes("HD_RE_BDR_LETTER").Value = ddlBDRLetterID.SelectedValue
                .Attributes("HD_RE_BDR_ID").Value = ddlBDRLetterID.SelectedValue
                .Attributes("WO_ID").Value = ddlWorkOrderNo.SelectedValue
                .Attributes("TT_NUMBAR").Value = ""
                'If txtBDRLetterID.Text = "" Then
                '    .Attributes("HD_RE_BDR_LETTER").Value = Request.Form("txtBDRLetterID")
                'Else
                '    .Attributes("HD_RE_BDR_LETTER").Value = txtBDRLetterID.Text
                'End If
                'If txtWorkOrderNo.Text = "" Then
                '    .Attributes("WO_ID").Value = Request.Form("txtWorkOrderNo")
                'Else
                '    .Attributes("WO_ID").Value = txtWorkOrderNo.Text
                'End If

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
                .Attributes("AIRLINE_CODE").Value = drpAirLineName.SelectedValue 'Added

                If ddlIRNumber.SelectedValue <> "" Then
                    .Attributes("IRNUMBER").Value = ddlIRNumber.SelectedItem.Text ' txtIRNumber.Text
                    .Attributes("HD_IR_REF").Value = ddlIRNumber.SelectedItem.Text
                    .Attributes("HD_IR_ID").Value = ddlIRNumber.SelectedValue
                End If
               
                If .Attributes("DS_MANDATORY") IsNot Nothing Then
                    .Attributes("DS_MANDATORY").Value = HdFuncManForDecAndSoln.Value
                End If

                '##################################################### Added on 01/0ct 2010 By Abhishek
                '######  Start of This Code Specilly added in 
                '######  that case when New Item is made in Ptr/bdr/workorder/ir number and
                '######  without selecting an item from dropdownlist click on save button
                '######################
                Try
                    Dim Item As New ListItem
                    Item = ddlPTRNo.Items.FindByValue(Request.Form("ddlPTRNO"))
                    If Item Is Nothing And Request.Form("ddlPTRNO") IsNot Nothing Then
                        If .Attributes("HD_PTR_REF").Value = "" Then
                            .Attributes("HD_PTR_REF").Value = Request.Form("ddlPTRNO")
                        End If
                    End If
                    Item = ddlBDRLetterID.Items.FindByValue(Request.Form("ddlBDRLetterID"))
                    If Item Is Nothing And Request.Form("ddlBDRLetterID") IsNot Nothing Then
                        If .Attributes("HD_RE_BDR_ID").Value = "" Then
                            .Attributes("HD_RE_BDR_ID").Value = Request.Form("ddlBDRLetterID")
                        End If
                        If .Attributes("HD_RE_BDR_LETTER").Value = "" Then
                            .Attributes("HD_RE_BDR_LETTER").Value = Request.Form("ddlBDRLetterID")
                        End If
                    End If
                    Item = ddlIRNumber.Items.FindByValue(Request.Form("ddlIRNumber"))
                    If Item Is Nothing And Request.Form("ddlIRNumber") IsNot Nothing Then
                        If .Attributes("IRNUMBER").Value = "" Then
                            .Attributes("IRNUMBER").Value = GetIRNOByID(Request.Form("ddlIRNumber").ToString)
                        End If
                        If .Attributes("HD_IR_ID").Value = "" Then
                            .Attributes("HD_IR_ID").Value = Request.Form("ddlIRNumber")
                        End If
                        If .Attributes("HD_IR_REF").Value = "" Then
                            .Attributes("HD_IR_REF").Value = GetIRNOByID(Request.Form("ddlIRNumber").ToString)
                        End If

                    End If
                    Item = ddlWorkOrderNo.Items.FindByValue(Request.Form("ddlWorkOrderNo"))
                    If Item Is Nothing And Request.Form("ddlWorkOrderNo") IsNot Nothing Then
                        If .Attributes("WO_ID").Value = "" Then
                            .Attributes("WO_ID").Value = Request.Form("ddlWorkOrderNo")
                        End If
                    End If
                Catch ex As Exception
                End Try
                '##################################################### Added on 01/0ct 2010 By Abhishek
                '######  End of This Code Specilly added in 
                '######  that case when New Item is made in Ptr/bdr/workorder/ir number and
                '######  without selecting an item from dropdownlist click on save button
                '######################
            End With


            If ddlTeamAssignedTo.SelectedItem.Text.ToUpper = "ONLINE" And (ddlQueryStatus.SelectedItem.Text.ToUpper = "SOLVED - OFFLINE" Or ddlQueryStatus.SelectedItem.Text.ToUpper = "PENDING - OFFLINE") Then
                lblError.Text = "Call Could not be saved because AssignedTo is Online and Query Status is " & ddlQueryStatus.SelectedItem.Text
                Exit Sub
            End If

            If Session("TempMultiLTRXml") IsNot Nothing Then
                objMultiLTRInputXml.LoadXml(Session("TempMultiLTRXml"))
                If objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLREQUESTDETAILS/CALLREQUEST[@HD_RE_ID='" + txtLTRNo.Text + "']") IsNot Nothing Then
                    objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLREQUESTDETAILS").RemoveChild(objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLREQUESTDETAILS/CALLREQUEST[@HD_RE_ID='" + txtLTRNo.Text + "']"))
                End If
                objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLREQUESTDETAILS").AppendChild(objMultiLTRInputXml.ImportNode(objInputXml.DocumentElement.SelectSingleNode("CALLREQUEST"), True))
                Session("TempMultiLTRXml") = objMultiLTRInputXml.OuterXml
            Else
                objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLREQUESTDETAILS").AppendChild(objMultiLTRInputXml.ImportNode(objInputXml.DocumentElement.SelectSingleNode("CALLREQUEST"), True))
                Session("TempMultiLTRXml") = objMultiLTRInputXml.OuterXml
            End If

            'Dim arobjAtt(20) As String
            Dim arobjAtt(21) As String
            Dim intArrIndex As Integer = 0
            Dim objXmlAttribute As XmlAttribute
            Dim objXmlNodeMulti As XmlNode
            For Each objAtt As XmlAttribute In objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLREQUESTDETAILS/CALLREQUEST[@HD_RE_ID='" + txtLTRNo.Text + "']").Attributes
                blnAttributePresent = False
                For Each objInnerAtt As XmlAttribute In objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLVIEWDETAILS/CALLREQUEST[@HD_RE_ID='" + txtLTRNo.Text + "']").Attributes
                    If objInnerAtt.Name = objAtt.Name Then
                        objInnerAtt.Value = objAtt.Value
                        blnAttributePresent = True
                        Exit For
                    End If
                Next
                If blnAttributePresent = False Then
                    arobjAtt(intArrIndex) = objAtt.Name
                    intArrIndex = intArrIndex + 1
                    'objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLVIEWDETAILS/CALLREQUEST[@HD_RE_ID='" + txtLTRNo.Text + "']").Attributes.Append(objAtt)
                End If
            Next

            objXmlNodeMulti = objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLVIEWDETAILS/CALLREQUEST[@HD_RE_ID='" + txtLTRNo.Text + "']")
            For Each strAr As String In arobjAtt
                If strAr IsNot Nothing Then
                    objXmlAttribute = objMultiLTRInputXml.CreateAttribute(strAr)
                    objXmlNodeMulti.Attributes.Append(objXmlAttribute)
                End If

            Next

            For Each objAtt As XmlAttribute In objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLREQUESTDETAILS/CALLREQUEST[@HD_RE_ID='" + txtLTRNo.Text + "']").Attributes
                blnAttributePresent = False
                For Each objInnerAtt As XmlAttribute In objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLVIEWDETAILS/CALLREQUEST[@HD_RE_ID='" + txtLTRNo.Text + "']").Attributes
                    If objInnerAtt.Name = objAtt.Name Then
                        objInnerAtt.Value = objAtt.Value
                        blnAttributePresent = True
                    End If
                Next
            Next

            '#################################
            '@ Start of Code Added By Abhishek on 22/06/10
            AddSolnAndDescForMultiltr(objMultiLTRInputXml)
            '@ End  Code added by abhishek
            '#################################
            Session("TempMultiLTRXml") = objMultiLTRInputXml.OuterXml
            'Here Back end Method Call

            If PageMultiLTROperation.TempSave = Operation Then

            Else

                '@ Code Added By Abhishek
                ' Before sending the input xml just change the xml
                Dim objFinalMultiinputLtr As New XmlDocument
                objFinalMultiinputLtr.LoadXml("<HD_UPDATE_CALLREQUESTSAVEALL_INPUT><CALLREQUESTDETAILS></CALLREQUESTDETAILS></HD_UPDATE_CALLREQUESTSAVEALL_INPUT>")
                For Each objxmlnode As XmlNode In objMultiLTRInputXml.DocumentElement.SelectNodes("CALLREQUESTDETAILS/CALLREQUEST")
                    objFinalMultiinputLtr.DocumentElement.SelectSingleNode("CALLREQUESTDETAILS").AppendChild(objFinalMultiinputLtr.ImportNode(objxmlnode, True))
                Next
                For Each objxmlnode As XmlNode In objFinalMultiinputLtr.DocumentElement.SelectNodes("CALLREQUESTDETAILS/CALLREQUEST")
                    '@ Find All Desc node for current ltr no
                    For Each xmlnodedesc As XmlNode In objMultiLTRInputXml.DocumentElement.SelectNodes("DESC_FILE[@HD_RE_ID='" + objxmlnode.Attributes("HD_RE_ID").Value + "']")
                        objxmlnode.AppendChild(objFinalMultiinputLtr.ImportNode(xmlnodedesc, True))
                    Next
                    '@ Find All Soln node for current ltr no
                    For Each xmlnodeSon As XmlNode In objMultiLTRInputXml.DocumentElement.SelectNodes("SOL_FILE[@HD_RE_ID='" + objxmlnode.Attributes("HD_RE_ID").Value + "']")
                        objxmlnode.AppendChild(objFinalMultiinputLtr.ImportNode(xmlnodeSon, True))
                    Next
                Next


                '#######################################
                '@ Added on 21/07/10
                '@ Now Cheking Description ans solution mandatory field for each Ltr No
                Dim strMandadatoryMsgForDesc As String = ""
                Dim strMandadatoryMsgForSoln As String = ""
                Dim strMandadatoryMsg As String = ""
                For Each objxmlnode As XmlNode In objFinalMultiinputLtr.DocumentElement.SelectNodes("CALLREQUESTDETAILS/CALLREQUEST")
                    ''@ Find All Desc node for current ltr no
                    Dim xmlnodeDescListByLtRno As XmlNodeList
                    xmlnodeDescListByLtRno = objMultiLTRInputXml.DocumentElement.SelectNodes("DESC_FILE[@HD_RE_ID='" + objxmlnode.Attributes("HD_RE_ID").Value + "']")
                    If hdSaveRights.Value = "1" And objxmlnode.Attributes("DS_MANDATORY").Value.Trim() <> "T" Then
                        If xmlnodeDescListByLtRno.Count = 0 Then
                            If strMandadatoryMsgForDesc.Trim.Length = 0 Then
                                strMandadatoryMsgForDesc = "Description is Mandatory for the LTR No:-" + objxmlnode.Attributes("HD_RE_ID").Value
                            Else
                                strMandadatoryMsgForDesc = strMandadatoryMsgForDesc & "<BR/>" & "Description is Mandatory for the LTR No:-" + objxmlnode.Attributes("HD_RE_ID").Value
                            End If
                        End If
                    End If
                    Dim strclosedstatus As String = objxmlnode.Attributes("HD_STATUS_CLOSE").Value.Trim.Split("|")(1).ToString
                    ''@ Find All Soln node for current ltr no
                    Dim xmlnodeSolnListByLtRno As XmlNodeList
                    xmlnodeSolnListByLtRno = objMultiLTRInputXml.DocumentElement.SelectNodes("SOL_FILE[@HD_RE_ID='" + objxmlnode.Attributes("HD_RE_ID").Value + "']")
                    If strclosedstatus = "1" And objxmlnode.Attributes("DS_MANDATORY").Value.Trim() <> "T" Then
                        If xmlnodeSolnListByLtRno.Count = 0 Then
                            If strMandadatoryMsgForSoln.Trim.Length = 0 Then
                                strMandadatoryMsgForSoln = "Solution is Mandatory for the LTR No:-" + objxmlnode.Attributes("HD_RE_ID").Value
                            Else
                                strMandadatoryMsgForSoln = strMandadatoryMsgForSoln & "<BR/>" & "Solution is Mandatory for the LTR No:-" + objxmlnode.Attributes("HD_RE_ID").Value
                            End If
                        End If
                    End If
                Next
                strMandadatoryMsg = strMandadatoryMsgForDesc
                If strMandadatoryMsg.Trim.Length = 0 Then
                    strMandadatoryMsg = strMandadatoryMsg + strMandadatoryMsgForSoln
                Else
                    strMandadatoryMsg = strMandadatoryMsg + "<BR/>" + strMandadatoryMsgForSoln
                End If
                If strMandadatoryMsg.Trim.Length > 0 Then
                    lblError.Text = strMandadatoryMsg
                    Exit Sub
                End If

                '#######################################
                '@ Added on 21/07/10
                '@ Now Cheking Description ans solution mandatory field for each Ltr No



                Try
                    objFinalMultiinputLtr.Save("c:\InputMultiLtr.xml")
                Catch ex As Exception
                End Try

                objOutputXml = objbzCall.UpdateAll(objFinalMultiinputLtr)
                '@ Code Added By Abhishek

                '  objOutputXml = objbzCall.UpdateAll(objMultiLTRInputXml)

                If hdQueryCategory.Value = "" Then
                    hdQueryCategory.Value = Request.Form(ddlQueryCategory.UniqueID)
                End If
                If hdQuerySubCategory.Value = "" Then
                    hdQuerySubCategory.Value = Request.Form(ddlQuerySubCategory.UniqueID)
                End If
                hdMsg.Value = ""
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    'After Save All Recall page
                    Response.Redirect(Request.Url.ToString, False)
                    Session("AllRecordUpdates") = objeAAMSMessage.messUpdate
                    Exit Sub
                    '@


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


                    ViewState("vsDescription") = txtDescription.Text
                    ViewState("vsSolution") = txtSolution.Text
                    ViewState("vsFollowDesc") = txtFollowDesc.Text
                    ViewState("vsMode") = ddlMode.SelectedValue
                    ViewState("vsNxtFollowupDate") = txtNxtFollowupDate.Text

                    txtDescription.Text = ""
                    txtSolution.Text = ""
                    txtFollowDesc.Text = ""

                    rdTechanical.Enabled = False
                    btnAssigneeHistory.Enabled = True
                    btnHistory.Enabled = True
                    btnFeedBack.Enabled = True
                    'hdPageHD_RE_ID.Value = objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("HD_RE_ID").Value
                    'txtLTRNo.Text = hdPageHD_RE_ID.Value
                    'hdEnPageHD_RE_ID.Value = objED.Encrypt(hdPageHD_RE_ID.Value)
                    'code to hid and show popup image
                    If hdPageHD_RE_ID.Value <> "" Then
                        Img2.Visible = False
                        Img3.Visible = True
                        Img4.Visible = True
                        Img5.Visible = True
                        ImgIRNo.Visible = True
                    Else
                        Img2.Visible = True
                        Img3.Visible = False
                        Img4.Visible = False
                        Img5.Visible = False
                        ImgIRNo.Visible = False
                    End If
                    'End
                    Bindata()
                    ViewRecords("single")

                    ' hdCoordinator1.Value = objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("COORDINATOR1").Value
                    'hdCoordinator2.Value = objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("COORDINATOR2").Value
                    hdTabType.Value = "0"
                    checkSecurity()

                    '@ Start of Code Added By Abhishek on 22/06/10
                    UpdatesessionForMultLTRcase()
                    '@ End of  code Added by Abhishek

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


                        hdTabType.Value = "0"
                        Exit Sub
                    End If

                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
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

        End Try
    End Sub

    Sub SaveInformation(ByVal Operation As Integer)
        'If 1 = 1 Then
        '    hdMsg.Value = "aasddfasd"
        '    ClientScript.RegisterClientScriptBlock(Me.GetType, "strPopup", "<script language='javascript'>window.setTimeout('YesNo();', 200);</script>")
        '    Exit Sub
        'End If

        If Operation = PageOperation.ReSave Then

        End If
        Dim objbzCall As New AAMS.bizHelpDesk.bzCall
        Dim objInputXml, objOutputXml As New XmlDocument
        'Start this will check ddlQueryStatus selected value is separated by pipe or not
        'This code mainly helpful when we ReSave LTR ,Because onclientClick of button doesn't fire
        Dim strQueryStatus As String
        Dim strQuerySubCategory As String
        Dim file As HttpPostedFile
        Dim strFullFileName As String = ""
        Dim strFilePath As String = ""
        Dim strFileName As String = ""

        Try
            strQueryStatus = ddlQueryStatus.SelectedValue
            If strQueryStatus.Split("|").Length <> 2 Then
                lblError.Text = "Query Status is mandatory"
                Exit Sub
            End If
            'End


            objInputXml.LoadXml("<HD_UPDATE_CALLREQUEST_INPUT><CALLREQUEST HD_QUERY_GROUP_ID='' HD_RE_OPEN_DATE=''  LCODE ='' HD_RE_ID='' CALLER_NAME='' CALL_SUB_GROUP_ID='' CALL_CATEGORY_ID='' CALL_SUB_CATEGORY_ID='' CALL_DURATION=''  HD_SEVERITY_ID='' HD_STATUS_ID='' COORDINATOR1='' COORDINATOR2='' HD_PTR_REF='' TT_NUMBAR='' HD_RE_BDR_LETTER='' WO_ID='' CONTACT_TYPE_ID='' DISPOSITION_ID='' AssignedTo='' LoggedBy='' LeftDateTime='' DESCRIPTION='' SOLUTION='' FU_UP_CONTACT_TYPE_ID='' FOLLOWUP_DESC='' FOLLOWUP_DATETIME='' HD_STATUS_CLOSE='' RESAVE='' HD_RE_CLOSED_DATE='' TITLE='' AIRLINE_CODE='' DESCRIPTION_ATTACH='' SOLUTION_ATTACH='' HD_RE_BDR_ID='' IRNUMBER ='' HD_IR_ID =''  HD_IR_REF =''  DS_MANDATORY=''  /></HD_UPDATE_CALLREQUEST_INPUT>")


            With objInputXml.DocumentElement.SelectSingleNode("CALLREQUEST")

                'file = FileUploadDesc.PostedFile
                'If Not FileUploadDesc Is Nothing Then
                '    strFilePath = FileUploadDesc.PostedFile.FileName
                '    strFileName = "D" + Now.ToString("ddMMyyyy") + "_" + txtOfficeId.Text + "_" + hdPageHD_RE_ID.Value + "_" + Path.GetFileName(strFilePath)
                '    strFullFileName = Server.MapPath("../HelpDeskFiles/") + strFileName
                '    If strFilePath <> "" Then
                '        file.SaveAs(strFullFileName)
                '        .Attributes("DESCRIPTION_ATTACH").Value = strFileName
                '    End If
                'End If

                'file = FileUploadSol.PostedFile
                'If Not FileUploadSol Is Nothing Then
                '    strFilePath = FileUploadSol.PostedFile.FileName
                '    strFileName = "S" + Now.ToString("ddMMyyyy") + "_" + txtOfficeId.Text + "_" + hdPageHD_RE_ID.Value + "_" + Path.GetFileName(strFilePath)
                '    strFullFileName = Server.MapPath("../HelpDeskFiles/") + strFileName
                '    If strFilePath <> "" Then
                '        file.SaveAs(strFullFileName)
                '        .Attributes("SOLUTION_ATTACH").Value = strFileName
                '    End If
                'End If



                .Attributes("HD_QUERY_GROUP_ID").Value = "1"
                .Attributes("LCODE").Value = hdCallAgencyName.Value.Split("|").GetValue(0)
                .Attributes("HD_RE_ID").Value = hdPageHD_RE_ID.Value

                .Attributes("CALLER_NAME").Value = txtCallerName.Text

                If Request.Form(DlstCallerName.UniqueID) <> "" Then
                    .Attributes("CALLER_NAME").Value = Request.Form(DlstCallerName.UniqueID)
                End If

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




                .Attributes("CALL_DURATION").Value = "" 'strCallDuration & ":" & strCallDuration1

                'Date Time Needed on the case of New call Log
                If hdPageStatus.Value.Trim() = "I" Then

                    '@ Start of Commented On 10/11/10
                    'Try
                    '    If hdDurationDate.Value.Trim().Length > 0 Then
                    '        starrDateAndTime = Convert.ToDateTime(hdDurationDate.Value.Trim())
                    '    Else
                    '        starrDateAndTime = DateAndTime.Now
                    '    End If
                    'Catch ex As Exception
                    '    starrDateAndTime = DateAndTime.Now
                    'End Try
                    ' .Attributes("HD_RE_OPEN_DATE").Value = starrDateAndTime.ToString("yyyy-MM-dd HH:mm:ss")
                    '@End of  Commented On 10/11/10

                    '@ Start of code On 10/11/10
                    If txtLoggedDate.Text.Trim.Length > 0 Then
                        .Attributes("HD_RE_OPEN_DATE").Value = objeAAMS.GetDateFormat(txtLoggedDate.Text, "dd/MM/yyyy HH:mm:ss", "yyyy-MM-dd HH:mm:ss", "/") ' starrDateAndTime.ToString("yyyy-MM-dd HH:mm:ss")
                    Else
                        .Attributes("HD_RE_OPEN_DATE").Value = objeAAMS.GetDateFormat(DateAndTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), "dd/MM/yyyy HH:mm:ss", "yyyy-MM-dd HH:mm:ss", "/")
                    End If
                    '@ End of code On 10/11/10

                Else

                    If hdCallSatrtDateTimeInitialy.Value.Trim().Length > 0 Then
                        Try
                            '.Attributes("HD_RE_OPEN_DATE").Value = dtime.ToString("yyyy-MM-dd HH:mm:ss")
                            .Attributes("HD_RE_OPEN_DATE").Value = objeAAMS.GetDateFormat(hdCallSatrtDateTimeInitialy.Value.Trim(), "dd/MM/yyyy HH:mm:ss", "yyyy-MM-dd HH:mm:ss", "/")

                        Catch ex As Exception
                            .Attributes("HD_RE_OPEN_DATE").Value = DateAndTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                        End Try
                    End If

                End If
                .Attributes("CALL_DURATION").Value = ""


                'HD_RE_OPEN_DATE


                .Attributes("HD_SEVERITY_ID").Value = ddlPriority.SelectedValue
                .Attributes("HD_STATUS_ID").Value = strQueryStatus.Split("|").GetValue(0)
                .Attributes("HD_STATUS_CLOSE").Value = strQueryStatus ' strQueryStatus.Split("|").GetValue(1) changed on 16 apr 10 to mactch with view output
                .Attributes("COORDINATOR1").Value = ddlCoordinator1.SelectedValue
                .Attributes("COORDINATOR2").Value = ddlCoordinator2.SelectedValue
                'If txtPTRNo.Text = "" Then
                '    .Attributes("HD_PTR_REF").Value = Request.Form("txtPTRNo")
                'Else
                '    .Attributes("HD_PTR_REF").Value = txtPTRNo.Text
                'End If

                If ddlPTRNo.SelectedValue <> "" Then
                    .Attributes("HD_PTR_REF").Value = ddlPTRNo.SelectedItem.Text
                End If

                .Attributes("HD_RE_BDR_ID").Value = ddlBDRLetterID.SelectedValue 'newly added attribute on 16 apr 10
                .Attributes("HD_RE_BDR_LETTER").Value = ddlBDRLetterID.SelectedValue
                .Attributes("WO_ID").Value = ddlWorkOrderNo.SelectedValue

                .Attributes("TT_NUMBAR").Value = ""
                'If txtBDRLetterID.Text = "" Then
                '    .Attributes("HD_RE_BDR_LETTER").Value = Request.Form("txtBDRLetterID")
                'Else
                '    .Attributes("HD_RE_BDR_LETTER").Value = txtBDRLetterID.Text
                'End If
                'If txtWorkOrderNo.Text = "" Then
                '    .Attributes("WO_ID").Value = Request.Form("txtWorkOrderNo")
                'Else
                '    .Attributes("WO_ID").Value = txtWorkOrderNo.Text
                'End If

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
                .Attributes("AIRLINE_CODE").Value = drpAirLineName.SelectedValue 'Added


                If ddlIRNumber.SelectedValue <> "" Then
                    .Attributes("IRNUMBER").Value = ddlIRNumber.SelectedItem.Text ' txtIRNumber.Text
                    .Attributes("HD_IR_REF").Value = ddlIRNumber.SelectedItem.Text
                    .Attributes("HD_IR_ID").Value = ddlIRNumber.SelectedValue

                End If

                If .Attributes("DS_MANDATORY") IsNot Nothing Then
                    .Attributes("DS_MANDATORY").Value = HdFuncManForDecAndSoln.Value
                End If

                '##################################################### Added on 01/0ct 2010 By Abhishek
                '######  Start of This Code Specilly added in 
                '######  that case when New Item is made in Ptr/bdr/workorder/ir number and
                '######  without selecting an item from dropdownlist click on save button
                '######################
                Try
                    Dim Item As New ListItem
                    Item = ddlPTRNo.Items.FindByValue(Request.Form("ddlPTRNO"))
                    If Item Is Nothing And Request.Form("ddlPTRNO") IsNot Nothing Then
                        If .Attributes("HD_PTR_REF").Value = "" Then
                            .Attributes("HD_PTR_REF").Value = Request.Form("ddlPTRNO")
                        End If
                    End If
                    Item = ddlBDRLetterID.Items.FindByValue(Request.Form("ddlBDRLetterID"))
                    If Item Is Nothing And Request.Form("ddlBDRLetterID") IsNot Nothing Then
                        If .Attributes("HD_RE_BDR_ID").Value = "" Then
                            .Attributes("HD_RE_BDR_ID").Value = Request.Form("ddlBDRLetterID")
                        End If
                        If .Attributes("HD_RE_BDR_LETTER").Value = "" Then
                            .Attributes("HD_RE_BDR_LETTER").Value = Request.Form("ddlBDRLetterID")
                        End If
                    End If
                    Item = ddlIRNumber.Items.FindByValue(Request.Form("ddlIRNumber"))
                    If Item Is Nothing And Request.Form("ddlIRNumber") IsNot Nothing Then
                        If .Attributes("IRNUMBER").Value = "" Then
                            .Attributes("IRNUMBER").Value = GetIRNOByID(Request.Form("ddlIRNumber").ToString)
                        End If
                        If .Attributes("HD_IR_ID").Value = "" Then
                            .Attributes("HD_IR_ID").Value = Request.Form("ddlIRNumber")
                        End If
                        If .Attributes("HD_IR_REF").Value = "" Then
                            .Attributes("HD_IR_REF").Value = GetIRNOByID(Request.Form("ddlIRNumber").ToString)
                        End If

                    End If
                    Item = ddlWorkOrderNo.Items.FindByValue(Request.Form("ddlWorkOrderNo"))
                    If Item Is Nothing And Request.Form("ddlWorkOrderNo") IsNot Nothing Then
                        If .Attributes("WO_ID").Value = "" Then
                            .Attributes("WO_ID").Value = Request.Form("ddlWorkOrderNo")
                        End If
                    End If
                Catch ex As Exception
                End Try
                '##################################################### Added on 01/0ct 2010 By Abhishek
                '######  End  of This Code Specilly added in 
                '######  that case when New Item is made in Ptr/bdr/workorder/ir number and
                '######  without selecting an item from dropdownlist click on save button
                '######################

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


            '#################################
            '@ Start of Code Added By Abhishek on 22/06/10   
          
            Dim objMultiLTRInputXml As New XmlDocument
            objMultiLTRInputXml.LoadXml("<HD_UPDATE_CALLREQUESTSAVEALL_INPUT><CALLREQUESTDETAILS></CALLREQUESTDETAILS><CALLVIEWDETAILS></CALLVIEWDETAILS></HD_UPDATE_CALLREQUESTSAVEALL_INPUT>")

            If Session("TempMultiLTRXml") IsNot Nothing Then
                objMultiLTRInputXml.LoadXml(Session("TempMultiLTRXml"))
                If objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLREQUESTDETAILS/CALLREQUEST[@HD_RE_ID='" + txtLTRNo.Text + "']") IsNot Nothing Then
                    objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLREQUESTDETAILS").RemoveChild(objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLREQUESTDETAILS/CALLREQUEST[@HD_RE_ID='" + txtLTRNo.Text + "']"))
                End If
                objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLREQUESTDETAILS").AppendChild(objMultiLTRInputXml.ImportNode(objInputXml.DocumentElement.SelectSingleNode("CALLREQUEST"), True))
                Session("TempMultiLTRXml") = objMultiLTRInputXml.OuterXml
            Else
                objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLREQUESTDETAILS").AppendChild(objMultiLTRInputXml.ImportNode(objInputXml.DocumentElement.SelectSingleNode("CALLREQUEST"), True))
                Session("TempMultiLTRXml") = objMultiLTRInputXml.OuterXml
            End If

            AddDescAndSoln(objInputXml)

            '@ Changed Xml Before Sending for Saving
            Dim objFinalInputXml As New XmlDocument
            objFinalInputXml.LoadXml("<HD_UPDATE_CALLREQUEST_INPUT></HD_UPDATE_CALLREQUEST_INPUT>")

            For Each objxmlnode As XmlNode In objInputXml.DocumentElement.SelectNodes("CALLREQUEST")
                objFinalInputXml.DocumentElement.AppendChild(objFinalInputXml.ImportNode(objxmlnode, True))
            Next
            For Each objxmlnode As XmlNode In objFinalInputXml.DocumentElement.SelectNodes("CALLREQUEST")
                '@ Find All Desc node for current ltr no
                For Each xmlnodedesc As XmlNode In objInputXml.DocumentElement.SelectNodes("DESC_FILE[@HD_RE_ID='" + objxmlnode.Attributes("HD_RE_ID").Value + "']")
                    objxmlnode.AppendChild(objFinalInputXml.ImportNode(xmlnodedesc, True))
                Next
                '@ Find All Soln node for current ltr no
                For Each xmlnodeSon As XmlNode In objInputXml.DocumentElement.SelectNodes("SOL_FILE[@HD_RE_ID='" + objxmlnode.Attributes("HD_RE_ID").Value + "']")
                    objxmlnode.AppendChild(objFinalInputXml.ImportNode(xmlnodeSon, True))
                Next
            Next

            Try
                objFinalInputXml.Save("c:\objFinalInputXmlLtr.xml")
            Catch ex As Exception
            End Try
            '@ End  Code added by abhishek
            '#################################
            objOutputXml = objbzCall.Update(objFinalInputXml)
            'objOutputXml = objbzCall.Update(objInputXml)

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

                ViewState("vsDescription") = txtDescription.Text
                ViewState("vsSolution") = txtSolution.Text
                ViewState("vsFollowDesc") = txtFollowDesc.Text
                ViewState("vsMode") = ddlMode.SelectedValue
                ViewState("vsNxtFollowupDate") = txtNxtFollowupDate.Text

                txtDescription.Text = ""
                txtSolution.Text = ""
                txtFollowDesc.Text = ""

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
                    ImgIRNo.Visible = True
                Else
                    Img2.Visible = True
                    Img3.Visible = False
                    Img4.Visible = False
                    Img5.Visible = False
                    ImgIRNo.Visible = False
                End If
                'End
                Bindata()

                '##################################################
                '@ Code Added By Abhishek 0n 22/06/10
                '@ After Save get the value from database and update CALLVIEWDETAILS node  in TempMultiLTRXml
                objMultiLTRInputXml.LoadXml("<HD_UPDATE_CALLREQUESTSAVEALL_INPUT><CALLREQUESTDETAILS></CALLREQUESTDETAILS><CALLVIEWDETAILS></CALLVIEWDETAILS></HD_UPDATE_CALLREQUESTSAVEALL_INPUT>")
                objInputXml.LoadXml("<HD_VIEW_CALLREQUEST_INPUT><HD_RE_ID></HD_RE_ID></HD_VIEW_CALLREQUEST_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = hdPageHD_RE_ID.Value
                objOutputXml = objbzCall.View(objInputXml)

                '@ Code For Updation DS_MANDATORY attributes in view xml
                objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST").Attributes("DS_MANDATORY").Value = "T"

                If Session("TempMultiLTRXml") IsNot Nothing Then
                    objMultiLTRInputXml.LoadXml(Session("TempMultiLTRXml"))
                    If objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLVIEWDETAILS/CALLREQUEST[@HD_RE_ID='" + txtLTRNo.Text + "']") IsNot Nothing Then
                        objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLVIEWDETAILS").RemoveChild(objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLVIEWDETAILS/CALLREQUEST[@HD_RE_ID='" + txtLTRNo.Text + "']"))
                    End If
                    objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLVIEWDETAILS").AppendChild(objMultiLTRInputXml.ImportNode(objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST"), True))
                    Session("TempMultiLTRXml") = objMultiLTRInputXml.OuterXml
                Else
                    objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLVIEWDETAILS").AppendChild(objMultiLTRInputXml.ImportNode(objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST"), True))
                    Session("TempMultiLTRXml") = objMultiLTRInputXml.OuterXml
                End If
                '##################################################
                '@ Code Added By Abhishek 0n 22/06/10
                '@ After Save get the value from database and update CALLVIEWDETAILS node  in TempMultiLTRXml



                ViewRecords("single")
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

                '@ Start of Code Added By Abhishek on 22/06/10
                UpdatesessionvalueForSingleRecords()
                '@ End of  code Added by Abhishek
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

                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

                FillCallerName()
                Dim liCaller As New ListItem
                liCaller = DlstCallerName.Items.FindByValue(Request.Form("DlstCallerName"))
                If liCaller IsNot Nothing Then
                    DlstCallerName.SelectedValue = liCaller.Value
                End If

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

            FillCallerName()
            Dim liCaller As New ListItem
            liCaller = DlstCallerName.Items.FindByValue(Request.Form("DlstCallerName"))
            If liCaller IsNot Nothing Then
                DlstCallerName.SelectedValue = liCaller.Value
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

        '@ Cide Added By Abhishek 22/06/10
        If hdPageHD_RE_ID.Value.Trim.Length > 0 Then
            BindDescription()
            BindSolution()
        End If
        '@ Cide Added By Abhishek
    End Sub
    'This function is used to enable or disable save button according to rights 
    'This function called once call is saved
    'Here parameter passed is currently (Closed status ie "1")
    Sub EnabledDisabledSavebutton(ByVal strValue As String)
        If strValue = "1" Then
            btnSave.Enabled = False
            If Session("LoginSession") IsNot Nothing And Session("Security") IsNot Nothing Then
                If objeAAMS.REOPENCLOSEDLTR(Session("Security")) = "1" Then
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

                '@Added By Abhishek
                UpdatesessionvalueForSingleRecords

                '@Added By Abhishek

                ViewRecords("single")
                hdTabType.Value = "0"
                ActiveTab()
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
        Dim strInput As String
        strInput = eventArgument.ToString

        If strInput.Length >= 4 Then
            If (Right(strInput, 3) = "|SC") Then 'denotes callback for querysubcategory to fill desc textbox
                FillPreDesc(strInput.Split("|").GetValue(0))
            ElseIf (Right(strInput, 3) = "|CN") Then 'denotes callback for CallerName textbox to autopopulate list
                GetCallerName(eventArgument)
            ElseIf strInput <> "" Then
                hdDurationDate.Value = DateAndTime.Now.ToString()
                '  22/06/2010 12:29
                hdLoggedDatetime.Value = DateAndTime.Now.ToString("dd/MM/yyyy HH:mm:tt") '"yyyy-MM-dd HH:mm:ss")
                AgencyViewByOfficeID(eventArgument)

            End If
        ElseIf strInput <> "" Then
            AgencyViewByOfficeID(eventArgument)
        End If

    End Sub

    Sub FillPreDesc(ByVal strSubcategoryId As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzCallSubCategory As New AAMS.bizHelpDesk.bzCallSubCategory
        Try
            objInputXml.LoadXml("<HD_VIEWCALLSUBCATEGORY_INPUT><CALL_SUB_CATEGORY_ID /></HD_VIEWCALLSUBCATEGORY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY_ID").InnerText = strSubcategoryId
            'Here Back end Method Call 
            objOutputXml = objbzCallSubCategory.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                str = objOutputXml.OuterXml
            Else
                str = ""
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            objbzCallSubCategory = Nothing
        End Try
    End Sub

    Protected Sub rdTechanical_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdTechanical.CheckedChanged
        If hdPageStatus.Value = "I" Or hdPageStatus.Value = "" Then
            'This code added on 22 july 08
            'This code is for passing lcode when user swap query group after selecting agencyName
            If hdCallAgencyName.Value <> "" Then
                'Response.Redirect("TCUP_Calllog.aspx?LCodeTemp=" + objED.Decrypt(hdCallAgencyName.Value.Split("|").GetValue(0)))

                ' If hdEnCallAgencyName_LCODE.Value = "" Then
                hdEnCallAgencyName_LCODE.Value = objED.Encrypt(hdCallAgencyName.Value.Split("|").GetValue(0))
                'End If
                Response.Redirect("TCUP_Calllog.aspx?LCodeTemp=" + hdEnCallAgencyName_LCODE.Value)
            Else
                Response.Redirect("TCUP_Calllog.aspx?Action=I") ' + hdQueryString.Value)
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
                    If .Attributes("COMP_VERTICAL") IsNot Nothing Then
                        TxtCompVertical.Text = .Attributes("COMP_VERTICAL").Value()
                    End If

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
        Dim strhdEnAOffice = ""
        Dim strhdEnCallAgencyName_LCODE = ""
        Dim strCallerName As String = ""
        Try
            ' <AGNECY LOCATION_CODE="21555" CHAIN_CODE="951" OfficeID="BOMMY3100" NAME="(Aurion Pro Solutions)" ADDRESS="404,Winchester, High Street, Hiranandani" ADDRESS1="Garden" CITY="Mumbai" COUNTRY="India" PHONE="67707700" FAX="67707722" ONLINE_STATUS="DE"
            'Aoffice="BOM" PINCODE="" Email="" CONTACT_PERSON="" CONTACT_PERSON_ID="" CITYID="180" COUNTRYID="1"/>
            objInputXml.LoadXml("<TA_SEARCHAGENCY_INPUT><OFFICEID></OFFICEID><ResponsibleStaffID></ResponsibleStaffID></TA_SEARCHAGENCY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerText = strOfficeID
            objInputXml.DocumentElement.SelectSingleNode("ResponsibleStaffID").InnerText = objeAAMS.EmployeeID(Session("Security"))
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

               
                Dim objNEWLOGDATETIMEAttribute As XmlAttribute = objOutputXml.DocumentElement.SelectSingleNode("AGNECY").OwnerDocument.CreateAttribute("NEWLOGDATETIME")
                If objOutputXml.DocumentElement.SelectSingleNode("AGNECY").Attributes("NEWLOGDATETIME") Is Nothing Then
                    objNEWLOGDATETIMEAttribute.Value = DateAndTime.Now.ToString("dd/MM/yyyy HH:mm:ss") '"yyyy-MM-dd HH:mm:ss")
                    objOutputXml.DocumentElement.SelectSingleNode("AGNECY").Attributes.Append(objNEWLOGDATETIMEAttribute)

                End If



                str = objOutputXml.OuterXml + "$" + strhdEnAOffice + "$" + strhdEnCallAgencyName_LCODE
                strCallerName = GetCallerName(strOfficeID)
                str = str + "$" + strCallerName

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

    Sub fillDropDownList()
        Dim objInputXml As New XmlDocument
        Dim objOutputXml As New XmlDocument
        Dim objPTR As New AAMS.bizHelpDesk.bzPTR
        Dim objWorkOrder As New AAMS.bizHelpDesk.bzWorkOrder
        Dim objRequestBDR As New AAMS.bizHelpDesk.bzRequestBDR
        Dim objIR As New AAMS.bizHelpDesk.bzIR
        Dim ds As New DataSet
        Dim objXmlReader As XmlNodeReader
        objInputXml.LoadXml("<HD_LISTPTR_INPUT><HD_RE_ID></HD_RE_ID></HD_LISTPTR_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = hdPageHD_RE_ID.Value
        objOutputXml = objPTR.ListPTR(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds = New DataSet
            ds.ReadXml(objXmlReader)
            ddlPTRNo.DataSource = ds.Tables("PTR")
            ddlPTRNo.DataTextField = "HD_PTR_REF"
            ddlPTRNo.DataValueField = "HD_PTR_ID"
            ddlPTRNo.DataBind()
            ddlPTRNo.Items.Insert(0, New ListItem("", ""))
        End If


        objInputXml.LoadXml("<HD_LISTWORKORDER_INPUT><HD_RE_ID></HD_RE_ID></HD_LISTWORKORDER_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = hdPageHD_RE_ID.Value
        objOutputXml = objWorkOrder.ListWorkOrder(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds = New DataSet
            ds.ReadXml(objXmlReader)
            ddlWorkOrderNo.DataSource = ds.Tables("WORKORDER")
            ddlWorkOrderNo.DataTextField = "WO_NUMBER"
            ddlWorkOrderNo.DataValueField = "WO_ID"
            ddlWorkOrderNo.DataBind()
            ddlWorkOrderNo.Items.Insert(0, New ListItem("", ""))
        End If



        objInputXml.LoadXml("<HD_LISTBDR_INPUT><HD_RE_ID></HD_RE_ID></HD_LISTBDR_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = hdPageHD_RE_ID.Value
        objOutputXml = objRequestBDR.ListBDR(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds = New DataSet
            ds.ReadXml(objXmlReader)
            ddlBDRLetterID.DataSource = ds.Tables("BDR")
            ddlBDRLetterID.DataTextField = "HD_RE_BDR_ID"
            ddlBDRLetterID.DataValueField = "HD_RE_BDR_ID"
            ddlBDRLetterID.DataBind()
            ddlBDRLetterID.Items.Insert(0, New ListItem("", ""))
        End If


        
        objInputXml.LoadXml("<HD_LISTIR_INPUT><HD_RE_ID></HD_RE_ID></HD_LISTIR_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = hdPageHD_RE_ID.Value
        objOutputXml = objIR.ListIR(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds = New DataSet
            ds.ReadXml(objXmlReader)

            ddlIRNumber.DataSource = Nothing
            ddlIRNumber.DataBind()


            ddlIRNumber.DataSource = ds.Tables("IR")
            ddlIRNumber.DataTextField = "HD_IR_REF"
            ddlIRNumber.DataValueField = "HD_IR_ID"
            ddlIRNumber.DataBind()
            ddlIRNumber.Items.Insert(0, New ListItem("", ""))
        End If


    End Sub

    Sub ViewRecords(ByVal strViewType As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objMultiLTRInputXml As New XmlDocument
        Dim objbzCall As New AAMS.bizHelpDesk.bzCall
        Dim li As New ListItem
        objInputXml.LoadXml("<HD_VIEW_CALLREQUEST_INPUT><HD_RE_ID></HD_RE_ID></HD_VIEW_CALLREQUEST_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = hdPageHD_RE_ID.Value
        'Here Back end Method Call 
        '<CALLREQUEST HD_QUERY_GROUP_ID="1" LCODE="664"  COUNTRY="India" CITY="Mumbai" PHONE="24464123" FAX="24442036" 
        'ONLINE_STATUS="DE" OFFICEID="" HD_RE_ID="107" CALLER_NAME="Tester"
        '      CALL_SUB_GROUP_ID="5" CALL_CATEGORY_ID="5" CALL_SUB_CATEGORY_ID="7" 
        'CALL_DURATION="10" HD_SEVERITY_ID="3" HD_STATUS_ID="1" COORDINATOR1="37" COORDINATOR2="37" HD_PTR_REF=""
        ' TT_NUMBAR="" HD_RE_BDR_LETTER="" WO_ID="" CONTACT_TYPE_ID="1"
        '      DISPOSITION_ID="1" HD_RE_OPEN_DATE="3/7/2008 1:39:25 PM" HD_RE_CLOSED_DATE="" AssignedTo="" AssignedDateTime="" LoggedBy="35"/>
        fillDropDownList()
        '<DESC_FILE SNO='' USER='' DATETIME='' FILENAME=''/><SOL_FILE Sno='' FileName=''/>
        objMultiLTRInputXml.LoadXml("<HD_UPDATE_CALLREQUESTSAVEALL_INPUT><CALLREQUESTDETAILS></CALLREQUESTDETAILS><CALLVIEWDETAILS></CALLVIEWDETAILS></HD_UPDATE_CALLREQUESTSAVEALL_INPUT>")


        If Session("TempMultiLTRXml") IsNot Nothing Then
            objMultiLTRInputXml.LoadXml(Session("TempMultiLTRXml"))
            If objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLVIEWDETAILS/CALLREQUEST[@HD_RE_ID='" + hdPageHD_RE_ID.Value + "']") IsNot Nothing Then
                objOutputXml.LoadXml("<HD_VIEW_CALLREQUEST_0UTPUT></HD_VIEW_CALLREQUEST_0UTPUT>")
                objOutputXml.DocumentElement.AppendChild(objOutputXml.ImportNode(objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLVIEWDETAILS/CALLREQUEST[@HD_RE_ID='" + hdPageHD_RE_ID.Value + "']"), True))
            Else
                objOutputXml = objbzCall.View(objInputXml)
                objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLVIEWDETAILS").AppendChild(objMultiLTRInputXml.ImportNode(objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST"), True))
                Session("TempMultiLTRXml") = objMultiLTRInputXml.OuterXml
            End If
        Else
            objOutputXml = objbzCall.View(objInputXml)
            objMultiLTRInputXml.DocumentElement.SelectSingleNode("CALLVIEWDETAILS").AppendChild(objMultiLTRInputXml.ImportNode(objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST"), True))
            Session("TempMultiLTRXml") = objMultiLTRInputXml.OuterXml
        End If


        If objOutputXml.DocumentElement.SelectSingleNode("Errors") Is Nothing Then
            GoTo byPassCondition
        End If

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

byPassCondition:
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


                '@ Start of New Code for Getting CallerName
                FillCallerName()
                Dim liCallerName As New ListItem
                liCallerName = DlstCallerName.Items.FindByText(.Attributes("CALLER_NAME").Value)
                If liCallerName IsNot Nothing Then
                    DlstCallerName.SelectedValue = liCallerName.Value
                Else
                    If .Attributes("CALLER_NAME").Value.Trim.Split("-").Length <= 1 Then
                        liCallerName = DlstCallerName.Items.FindByText(.Attributes("CALLER_NAME").Value + "-")
                        If liCallerName IsNot Nothing Then
                            DlstCallerName.SelectedValue = liCallerName.Value
                        End If
                    End If

                End If
                '@ End of New Code for Getting CallerName

                txtCallerName.Text = .Attributes("CALLER_NAME").Value
                hdCallerName.Value = .Attributes("CALLER_NAME").Value


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

                If .Attributes("COMP_VERTICAL") IsNot Nothing Then
                    TxtCompVertical.Text = .Attributes("COMP_VERTICAL").Value()
                End If


                Dim strCallDuration As String = .Attributes("CALL_DURATION").Value
                Select Case strCallDuration.Length
                    Case "0"
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
                    Case Else
                        txtCallDuration.Text = strCallDuration.Substring(0, (strCallDuration.Length) - 2)
                        txtCallDuration1.Text = strCallDuration.Substring((strCallDuration.Length) - 2, 2)
                End Select


                ddlPriority.SelectedValue = .Attributes("HD_SEVERITY_ID").Value
                ddlQueryStatus.SelectedValue = .Attributes("HD_STATUS_CLOSE").Value

                'Code Added for Call Duration
                hdCallPreviousState.Value = .Attributes("HD_STATUS_CLOSE").Value.Trim().Split("|").GetValue(1)
                hdCallSatrtDateTimeInitialy.Value = .Attributes("HD_RE_OPEN_DATE").Value
                hdCallCloseDateTime.Value = .Attributes("HD_RE_CLOSED_DATE").Value

                'If (hdCallPreviousState.Value.Trim() = "1") And (strCallDuration.Trim().Length <> 0) Then
                '    txtCallDuration.Text = strCallDuration.Split(":").GetValue(0).ToString()
                '    txtCallDuration1.Text = strCallDuration.Split(":").GetValue(1).ToString()
                'End If

                'Code Added for Call Duration

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

                li = ddlPTRNo.Items.FindByValue(.Attributes("HD_PTR_ID").Value)
                If li IsNot Nothing Then
                    ddlPTRNo.SelectedValue = li.Value
                End If

                li = ddlBDRLetterID.Items.FindByValue(.Attributes("HD_RE_BDR_ID").Value)
                If li IsNot Nothing Then
                    ddlBDRLetterID.SelectedValue = li.Value
                End If

                li = ddlWorkOrderNo.Items.FindByValue(.Attributes("WO_ID").Value)
                If li IsNot Nothing Then
                    ddlWorkOrderNo.SelectedValue = li.Value
                End If


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
                drpAirLineName.SelectedValue = .Attributes("AIRLINE_CODE").Value
                '1052|test67|ewrw||Agra|India|||||AGR
                hdCallAgencyName.Value = .Attributes("LCODE").Value + "|" + .Attributes("AGENCYNAME").Value + "|" + .Attributes("ADDRESS").Value + "|" + "" + "|" + .Attributes("CITY").Value + "|" + .Attributes("COUNTRY").Value + "|" + .Attributes("PHONE").Value + "|" + .Attributes("OFFICEID").Value + "|" + .Attributes("FAX").Value + "|" + .Attributes("ONLINE_STATUS").Value + "|" + .Attributes("AOFFICE").Value
                hdEnCallAgencyName_LCODE.Value = objED.Encrypt(.Attributes("LCODE").Value)
                hdSaveRights.Value = "0"
                If .Attributes("HD_STATUS_CLOSE").Value.Split("|").GetValue(1) = "1" Then
                    'Commented on 22 July 08
                    'In place of this new function is called
                    '   btnSave.Enabled = False
                    EnabledDisabledSavebutton("1")
                End If


                If .Attributes("DESCRIPTION") IsNot Nothing Then
                    '   txtDescription.Text = .Attributes("DESCRIPTION").Value
                Else
                    txtDescription.Text = ""
                End If

                If .Attributes("SOLUTION") IsNot Nothing Then
                    ' txtSolution.Text = .Attributes("SOLUTION").Value
                Else
                    txtSolution.Text = ""
                End If

                If .Attributes("FOLLOWUP_DESC") IsNot Nothing Then
                    txtFollowDesc.Text = .Attributes("FOLLOWUP_DESC").Value
                Else
                    txtFollowDesc.Text = ""
                End If


                If .Attributes("HD_IR_ID") IsNot Nothing Then
                    Dim liIRId As New ListItem
                    liIRId = ddlIRNumber.Items.FindByValue(.Attributes("HD_IR_ID").Value)
                    If liIRId IsNot Nothing Then
                        ddlIRNumber.SelectedValue = liIRId.Value
                    End If
                    If .Attributes("HD_IR_REF") IsNot Nothing Then
                        txtIRNumber.Text = .Attributes("HD_IR_REF").Value
                    Else
                        txtIRNumber.Text = ""
                    End If
                Else
                    txtIRNumber.Text = ""
                    '  ddlIRNumber.SelectedValue = ""
                End If

                If .Attributes("HD_IR_ID") IsNot Nothing Then
                    hdIRNo.Value = .Attributes("HD_IR_ID").Value
                    hdEnIRNo.Value = objED.Encrypt(.Attributes("HD_IR_ID").Value)
                End If



                If .Attributes("DS_MANDATORY") IsNot Nothing Then
                    HdFuncManForDecAndSoln.Value = .Attributes("DS_MANDATORY").Value
                End If


                AutoCompleteExtenderForCaller.ContextKey = .Attributes("LCODE").Value + "|" + txtOfficeId.Text

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

        Dim objInputXml, objOutputXml, objDescxml As New XmlDocument
        Dim objXmlReader As XmlNodeReader

        Dim ds As New DataSet
        Dim objCall As New AAMS.bizHelpDesk.bzCall

        objInputXml.LoadXml("<HD_GETREQUESTDESCRIPTION_INPUT><HD_RE_ID></HD_RE_ID></HD_GETREQUESTDESCRIPTION_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = hdPageHD_RE_ID.Value
        objOutputXml = objCall.ViewRequestDesc(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then


            '@ Start of  Code Added By Abhishek on 31-05-10
            If Session("DESC") Is Nothing Then
                Session("DESC") = objOutputXml.OuterXml
                objDescxml = objOutputXml
            Else
                objDescxml.LoadXml(Session("DESC").ToString)
                For Each objxmlnode As XmlNode In objDescxml.DocumentElement.SelectNodes("REQUESTDESCRIPTION[@HD_RE_ID='" + txtLTRNo.Text + "' and @RTYPE!='N']")
                    objDescxml.DocumentElement.RemoveChild(objxmlnode)
                Next
                For Each objxmlnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("REQUESTDESCRIPTION")
                    objDescxml.DocumentElement.AppendChild(objDescxml.ImportNode(objxmlnode, True))
                Next
                Session("DESC") = objDescxml.OuterXml
            End If

            If objDescxml.DocumentElement.SelectNodes("REQUESTDESCRIPTION[@HD_RE_ID='" + txtLTRNo.Text + "']").Count > 0 Then
                objXmlReader = New XmlNodeReader(objDescxml)
                ds = New DataSet
                ds.ReadXml(objXmlReader)
                If ds.Tables("REQUESTDESCRIPTION").Rows.Count > 0 Then
                    gvDescription.DataSource = Nothing
                    gvDescription.DataBind()
                    Dim dv As DataView = ds.Tables("REQUESTDESCRIPTION").DefaultView
                    dv.RowFilter = "HD_RE_ID='" + txtLTRNo.Text + "'"
                    If dv.Count > 0 Then
                        '  dv.Sort = "ROWNUMBER DESC"
                        gvDescription.DataSource = dv 'ds.Tables("REQUESTDESCRIPTION")
                        gvDescription.DataBind()
                    Else
                        gvDescription.DataSource = Nothing
                        gvDescription.DataBind()
                    End If
                Else
                    gvDescription.DataSource = Nothing
                    gvDescription.DataBind()
                End If
            Else
                gvDescription.DataSource = Nothing
                gvDescription.DataBind()
            End If
            '@ End of of  Code Added By Abhishek on 31-05-10

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

            If Session("DESC") IsNot Nothing Then
                objDescxml.LoadXml(Session("DESC").ToString)
                If objDescxml.DocumentElement.SelectNodes("REQUESTDESCRIPTION[@HD_RE_ID='" + txtLTRNo.Text + "']").Count > 0 Then
                    objXmlReader = New XmlNodeReader(objDescxml)
                    ds = New DataSet
                    ds.ReadXml(objXmlReader)
                    If ds.Tables("REQUESTDESCRIPTION").Rows.Count > 0 Then
                        gvDescription.DataSource = Nothing
                        gvDescription.DataBind()
                        Dim dv As DataView = ds.Tables("REQUESTDESCRIPTION").DefaultView
                        dv.RowFilter = "HD_RE_ID='" + txtLTRNo.Text + "'"
                        If dv.Count > 0 Then
                            '  dv.Sort = "ROWNUMBER DESC"
                            gvDescription.DataSource = dv 'ds.Tables("REQUESTDESCRIPTION")
                            gvDescription.DataBind()
                        Else
                            gvDescription.DataSource = Nothing
                            gvDescription.DataBind()
                        End If
                    Else
                        gvDescription.DataSource = Nothing
                        gvDescription.DataBind()
                    End If
                Else
                    gvDescription.DataSource = Nothing
                    gvDescription.DataBind()
                End If

            End If
        End If
    End Sub

    Sub BindSolution()
        '   Session("SOLUTION") = Nothing
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim objSolxml As New XmlDocument
        Dim ds As New DataSet
        Dim objCall As New AAMS.bizHelpDesk.bzCall
        objInputXml.LoadXml("<HD_GETREQUESTSOLUTION_INPUT><HD_RE_ID></HD_RE_ID></HD_GETREQUESTSOLUTION_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = hdPageHD_RE_ID.Value
        objOutputXml = objCall.ViewRequestSolution(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then


            'objXmlReader = New XmlNodeReader(objOutputXml)
            'ds.ReadXml(objXmlReader)

            'If ds.Tables("REQUESTSOLUTION").Rows.Count <> 0 Then
            '    gvSolution.DataSource = ds.Tables("REQUESTSOLUTION")
            '    gvSolution.DataBind()

            'Else
            '    gvSolution.DataSource = Nothing
            '    gvSolution.DataBind()
            'End If

            '@ Start of  Code Added By Abhishek on 31-05-10
            If Session("SOLUTION") Is Nothing Then
                Session("SOLUTION") = objOutputXml.OuterXml
                objSolxml = objOutputXml
            Else
                objSolxml.LoadXml(Session("SOLUTION").ToString)
                For Each objxmlnode As XmlNode In objSolxml.DocumentElement.SelectNodes("REQUESTSOLUTION[@HD_RE_ID='" + txtLTRNo.Text + "' and @RTYPE!='N']")
                    objSolxml.DocumentElement.RemoveChild(objxmlnode)
                Next
                For Each objxmlnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("REQUESTSOLUTION")
                    objSolxml.DocumentElement.AppendChild(objSolxml.ImportNode(objxmlnode, True))
                Next
                Session("SOLUTION") = objSolxml.OuterXml
            End If

            If objSolxml.DocumentElement.SelectNodes("REQUESTSOLUTION[@HD_RE_ID='" + txtLTRNo.Text + "']").Count > 0 Then

                objXmlReader = New XmlNodeReader(objSolxml)
                ds = New DataSet
                ds.ReadXml(objXmlReader)

                If ds.Tables("REQUESTSOLUTION").Rows.Count > 0 Then
                    gvSolution.DataSource = Nothing
                    gvSolution.DataBind()
                    Dim dv As DataView = ds.Tables("REQUESTSOLUTION").DefaultView

                    dv.RowFilter = "HD_RE_ID='" + txtLTRNo.Text + "'"
                    If dv.Count > 0 Then
                        '  dv.Sort = "ROWNUMBER DESC"
                        gvSolution.DataSource = dv 'ds.Tables("REQUESTSOLUTION")
                        gvSolution.DataBind()
                    Else
                        gvSolution.DataSource = Nothing
                        gvSolution.DataBind()
                    End If
                Else
                    gvSolution.DataSource = Nothing
                    gvSolution.DataBind()
                End If
            Else
                gvSolution.DataSource = Nothing
                gvSolution.DataBind()
            End If

            '@ End of of  Code Added By Abhishek on 31-05-10
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

            If Session("SOLUTION") IsNot Nothing Then
                objSolxml.LoadXml(Session("SOLUTION").ToString)

                If objSolxml.DocumentElement.SelectNodes("REQUESTSOLUTION[@HD_RE_ID='" + txtLTRNo.Text + "']").Count > 0 Then

                    objXmlReader = New XmlNodeReader(objSolxml)
                    ds = New DataSet
                    ds.ReadXml(objXmlReader)

                    If ds.Tables("REQUESTSOLUTION").Rows.Count > 0 Then
                        gvSolution.DataSource = Nothing
                        gvSolution.DataBind()
                        Dim dv As DataView = ds.Tables("REQUESTSOLUTION").DefaultView

                        dv.RowFilter = "HD_RE_ID='" + txtLTRNo.Text + "'"
                        If dv.Count > 0 Then
                            '  dv.Sort = "ROWNUMBER DESC"
                            gvSolution.DataSource = dv 'ds.Tables("REQUESTSOLUTION")
                            gvSolution.DataBind()
                        Else
                            gvSolution.DataSource = Nothing
                            gvSolution.DataBind()
                        End If
                    Else
                        gvSolution.DataSource = Nothing
                        gvSolution.DataBind()
                    End If
                Else
                    gvSolution.DataSource = Nothing
                    gvSolution.DataBind()
                End If

            End If


        End If
    End Sub

    Sub BindFollowUp()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objCall As New AAMS.bizHelpDesk.bzCall
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
            Dim objbzCallCategory As New AAMS.bizHelpDesk.bzCallCategory
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
            Dim objbzCallSubCategory As New AAMS.bizHelpDesk.bzCallSubCategory
            Dim ds As New DataSet
            Dim objInputXml, objOutputXml As New XmlDocument
            objInputXml.LoadXml("<HD_SEARCHCALLSUBCATEGORY_INPUT><CALL_SUB_CATEGORY_NAME /><CALL_CATEGORY_ID /><CALL_SUB_GROUP_ID /><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHCALLSUBCATEGORY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_GROUP_ID").InnerText = ddlQuerySubGroup.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("CALL_CATEGORY_ID").InnerText = strId

            objOutputXml = objbzCallSubCategory.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                '    objXmlReader = New XmlNodeReader(objOutputXml)
                '   ds.ReadXml(objXmlReader)
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

    Public Sub fillCoordinator(ByVal strCoordinatorType As String, ByVal strAoffice As String)
        'Code to fill Coordinator
        If strAoffice <> "" Then


            Dim objbzCoordinator As New AAMS.bizHelpDesk.bzCoordinator
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
        ' Dim objhdPtrView As New AAMS.bizHelpDesk.bzPTR
        Dim li As New ListItem
        'objOutputXml = objhdPtrView.ListPTRConfigValue()

        'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
        '    Dim xNode As XmlNode
        '    For Each xNode In objOutputXml.DocumentElement.SelectNodes("CONFIGVALUE")
        '        If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_HD_REQUEST_SEVERITY" Then
        '            ddlPriority.SelectedValue = xNode.Attributes("FIELD_VALUE").Value.Trim().Split("|").GetValue(0)
        '        End If
        '        If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_HD_REQUEST_STATUS" Then
        '            ddlQueryStatus.SelectedValue = xNode.Attributes("FIELD_VALUE").Value.Trim().Split("|").GetValue(0) & "|" & "0"
        '            If ddlQueryStatus.SelectedValue = "" Then
        '                ddlQueryStatus.SelectedValue = xNode.Attributes("FIELD_VALUE").Value.Trim().Split("|").GetValue(0) & "|" & "1"
        '            End If
        '        End If
        '        If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_HD_REQUEST_CONTACT_TYPE" Then
        '            ddlContactType.SelectedValue = xNode.Attributes("FIELD_VALUE").Value.Trim()
        '        End If
        '        If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_HD_FUNCTIONAL_ASSIGNEDTO" Then
        '            ddlTeamAssignedTo.SelectedValue = xNode.Attributes("FIELD_VALUE").Value.Trim()
        '        End If
        '    Next
        'Else
        '    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        'End If


        'code added for employee wise default ASSIGNEDTO and CONTACT_TYPE 14 apr 10
        If Session("Security") IsNot Nothing Then
            objOutputXml.LoadXml(Session("Security"))
            '          <HelpDesk_Default>
            '<HD_Default FUN_ASSIGNEDTO="" TEC_ASSIGNEDTO="" CONTACT_TYPE_ID="" /> 
            '</HelpDesk_Default>


            If objOutputXml.DocumentElement.SelectSingleNode("HelpDesk_Default") IsNot Nothing Then
                If objOutputXml.DocumentElement.SelectSingleNode("HelpDesk_Default/HD_Default").Attributes("FUN_ASSIGNEDTO").Value <> "" Then
                    HdDefaultTeamAssignedTo.Value = objOutputXml.DocumentElement.SelectSingleNode("HelpDesk_Default/HD_Default").Attributes("FUN_ASSIGNEDTO").Value
                    li = ddlTeamAssignedTo.Items.FindByValue(objOutputXml.DocumentElement.SelectSingleNode("HelpDesk_Default/HD_Default").Attributes("FUN_ASSIGNEDTO").Value)
                    If li IsNot Nothing Then
                        ddlTeamAssignedTo.SelectedValue = li.Value
                    End If
                End If
                If objOutputXml.DocumentElement.SelectSingleNode("HelpDesk_Default/HD_Default").Attributes("CONTACT_TYPE_ID").Value <> "" Then
                    HdDefaultContactType.Value = objOutputXml.DocumentElement.SelectSingleNode("HelpDesk_Default/HD_Default").Attributes("CONTACT_TYPE_ID").Value
                    li = ddlContactType.Items.FindByValue(objOutputXml.DocumentElement.SelectSingleNode("HelpDesk_Default/HD_Default").Attributes("CONTACT_TYPE_ID").Value)
                    If li IsNot Nothing Then
                        ddlContactType.SelectedValue = li.Value
                    End If
                End If
            End If

        End If

    End Sub

    Protected Sub rdFunctional_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdFunctional.CheckedChanged

    End Sub

    Enum PageOperation
        Save = 1
        ReSave = 2
    End Enum

    Enum PageMultiLTROperation
        Save = 1
        TempSave = 2
    End Enum

    Private Sub FillCoordinator()
        Dim objInputXml As New XmlDocument
        Dim objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim objbzCoordinator As New AAMS.bizHelpDesk.bzCoordinator
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
        Dim objCordinator As New AAMS.bizHelpDesk.bzCoordinator
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

    Protected Sub gvDescription_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvDescription.RowCommand
        If e.CommandName = "ViewX" Then

            DownloadFile(e.CommandArgument.ToString)

        End If
        If e.CommandName = "DelX" Then
            DeleteDescFile(e.CommandArgument.ToString)
        End If


    End Sub

    Protected Sub gvDescription_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDescription.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim txtDesc As TextBox = CType(e.Row.FindControl("txtDesc"), TextBox)
                Dim HdRowId As HiddenField = CType(e.Row.FindControl("HdRowId"), HiddenField)

                Dim lnkView As LinkButton = CType(e.Row.FindControl("lnkView"), LinkButton)
                Dim LnkDelete As LinkButton = CType(e.Row.FindControl("LnkDelete"), LinkButton)

                Dim hdRowType As HiddenField = CType(e.Row.FindControl("hdRowType"), HiddenField)
                Dim hdRowNo As HiddenField = CType(e.Row.FindControl("hdRowNo"), HiddenField)


                If hdRowType.Value.Trim.ToUpper <> "N" Then
                    txtDesc.Attributes.Add("onclick", "return CallLogDescription('ds','" + objED.Encrypt(hdPageHD_RE_ID.Value) + "','" + objED.Encrypt(HdRowId.Value) + "')")
                End If
                If hdRowType.Value.Trim.ToUpper = "N" Then
                    'If hdRowType.Value.Trim.ToUpper = "N" And lnkView.Text.Trim.Length > 0 Then
                    LnkDelete.Enabled = True
                Else
                    LnkDelete.Enabled = False
                End If

                If lnkView.Text.Trim.Length > 0 Then
                    lnkView.Enabled = True
                Else
                    lnkView.Enabled = False
                End If
                'If DataBinder.Eval(e.Row.DataItem, "Attach").ToString = "" Then
                '    CType(e.Row.FindControl("lnkView"), LinkButton).Enabled = False
                'End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gvSolution_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvSolution.RowCommand
        If e.CommandName = "ViewX" Then
            DownloadFile(e.CommandArgument.ToString)
        End If

        If e.CommandName = "DelX" Then
            DeleteSolFile(e.CommandArgument.ToString)
        End If

    End Sub

    Sub DownloadFile(ByVal strFileName As String)

        Try

            If strFileName.Trim = "" Then
                Exit Sub
            End If
            Dim fullpath = Path.GetFullPath(Server.MapPath("../HelpDeskFiles/") + strFileName)
            Dim name = Path.GetFileName(fullpath)
            Dim ext = Path.GetExtension(fullpath)
            Dim type As String = ""
            If Not File.Exists(fullpath) Then
                lblError.Text = "File does not exist."
                Exit Sub
            End If


            If Not IsDBNull(ext) Then
                ext = LCase(ext)
            End If

            Select Case ext
                Case ".htm", ".html"
                    type = "text/HTML"
                Case ".txt"
                    type = "text/plain"
                Case ".doc", ".rtf"
                    type = "Application/msword"
                Case ".csv", ".xls"
                    type = "Application/x-msexcel"
                Case Else
                    type = "text/plain"
            End Select


            Response.AppendHeader("content-disposition", "attachment; filename=" + name)

            If type <> "" Then
                Response.ContentType = type
            End If

            Response.WriteFile(fullpath)
            Response.End()
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            ' hdTabType.Value = 1
            ActiveTab()
        End Try
    End Sub
   

    Public Function GetCallerName(ByVal strCallerName As String) As String
        Dim objbzAgencyStaff As New AAMS.bizTravelAgency.bzAgencyStaff
        Dim objInputXml As New XmlDocument
        Dim objOXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        Dim strOut As String = ""
        Try
            ' objInputXml.LoadXml("<TA_SEARCHSTAFF_INPUT><STAFFNAME></STAFFNAME><AGENCYNAME></AGENCYNAME><OFFICEID/><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><EmployeeID/><Source/></TA_SEARCHSTAFF_INPUT>")
            objInputXml.LoadXml("<TA_SEARCHSTAFF_INPUT><STAFFNAME></STAFFNAME><AGENCYNAME></AGENCYNAME><OFFICEID/><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><EmployeeID/><SIGNINID></SIGNINID><Source/></TA_SEARCHSTAFF_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("STAFFNAME").InnerText = "" ' strCallerName
            objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = "" ' txtAgencyName.Text
            objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerText = strCallerName 'txtOfficeId.Text

            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = hdLimited_To_Aoffice.Value
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = hdLimited_To_Region.Value
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = hdLimited_To_OwnAgency.Value
            'objInputXml.DocumentElement.SelectSingleNode("Source").InnerText = "Helpdesk"

            If Not HttpContext.Current.Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = HttpContext.Current.Session("LoginSession").ToString().Split("|")(0)
            End If
            'If Session("Security") IsNot Nothing Then
            '    objSecurityXml.LoadXml(Session("Security"))

            '    If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
            '        If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
            '            If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
            '                objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
            '            End If
            '        End If
            '    End If
            '    If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
            '        If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
            '            If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
            '                objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
            '            End If
            '        End If
            '    End If
            '    If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
            '        If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
            '            If Not Session("LoginSession") Is Nothing Then
            '                objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = Session("LoginSession").ToString().Split("|")(0)
            '            End If
            '        End If
            '    End If

            'End If

            objOXml = objbzAgencyStaff.Search(objInputXml)

            If objOXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                strOut = objOXml.OuterXml
            Else
                strOut = ""
            End If
            Return strOut
        Catch ex As Exception
            Return ""
        End Try

    End Function

    '  <System.Web.Script.Services.ScriptMethod()> _
    '<WebMethod()> _
    'Public Function GetCallerName(ByVal prefixText As String, ByVal count As Integer) As String()
    '      Dim objbzAgencyStaff As New AAMS.bizTravelAgency.bzAgencyStaff
    '      Dim objInputXml As New XmlDocument
    '      Dim objOXml As New XmlDocument
    '      Dim objSecurityXml As New XmlDocument
    '      Dim objXmlList As XmlNodeList
    '      Dim stn(0) As String
    '      'Dim strContextKey As String = contextKey


    '      Try
    '          objInputXml.LoadXml("<TA_SEARCHSTAFF_INPUT><STAFFNAME></STAFFNAME><AGENCYNAME></AGENCYNAME><OFFICEID/><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><EmployeeID/><Source/></TA_SEARCHSTAFF_INPUT>")

    '          objInputXml.DocumentElement.SelectSingleNode("STAFFNAME").InnerText = prefixText
    '          ' objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = strContextKey.Split("|").GetValue(0)
    '          ' objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerText = strContextKey.Split("|").GetValue(1)

    '          'objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = strContextKey.Split("|").Get Value(2)
    '          'objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = strContextKey.Split("|").GetValue(3)
    '          'objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = strContextKey.Split("|").GetValue(4)
    '          objInputXml.DocumentElement.SelectSingleNode("Source").InnerText = "Helpdesk1"

    '          'If Session("Security") IsNot Nothing Then
    '          '    objSecurityXml.LoadXml(Session("Security"))

    '          '    If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
    '          '        If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
    '          '            If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
    '          '                objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
    '          '            End If
    '          '        End If
    '          '    End If
    '          '    If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
    '          '        If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
    '          '            If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
    '          '                objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
    '          '            End If
    '          '        End If
    '          '    End If
    '          '    If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
    '          '        If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
    '          '            If Not Session("LoginSession") Is Nothing Then
    '          '                objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = Session("LoginSession").ToString().Split("|")(0)
    '          '            End If
    '          '        End If
    '          '    End If

    '          'End If

    '          objOXml = objbzAgencyStaff.Search(objInputXml)

    '          If objOXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value = "TRUE" Then

    '              stn(0) = objOXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '              Return stn
    '          Else
    '              If count = 0 Then
    '                  count = 10
    '              End If

    '              objXmlList = objOXml.DocumentElement.SelectNodes("STAFF")
    '              Dim st(objXmlList.Count - 1) As String
    '              For i As Integer = 0 To objXmlList.Count - 1
    '                  st(i) = objXmlList.Item(i).Attributes("STAFFNAME").InnerText
    '              Next
    '              Return st
    '          End If

    '      Catch ex As Exception
    '      End Try

    '  End Function
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try

            If ddlPageNumber.SelectedValue <> "1" Then
                hdTabType.Value = 0
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
                hdPageNo.Value = ddlPageNumber.SelectedValue
                hdMultiLTR.Value = (Val(hdMultiLTR.Value) - 1).ToString
                SaveMultiLTR(PageMultiLTROperation.TempSave)
                BindMultiLTR()


            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            hdTabType.Value = 0
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
                hdPageNo.Value = ddlPageNumber.SelectedValue
                hdMultiLTR.Value = (Val(hdMultiLTR.Value) + 1).ToString
                SaveMultiLTR(PageMultiLTROperation.TempSave)
                BindMultiLTR()


            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            hdPageNo.Value = ddlPageNumber.SelectedValue
            hdMultiLTR.Value = Val((ddlPageNumber.SelectedValue) - 1).ToString
            SaveMultiLTR(PageMultiLTROperation.TempSave)
            BindMultiLTR()
            hdTabType.Value = 0

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSaveAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveAll.Click
        SaveMultiLTR(PageMultiLTROperation.Save)
        '@ Cide Added By Abhishek 22/06/10
        If hdPageHD_RE_ID.Value.Trim.Length > 0 Then
            BindDescription()
            BindSolution()
        End If
        '@ Cide Added By Abhishek
    End Sub

    Private Sub FillCallerName()
        Dim strout As String = ""
        DlstCallerName.Items.Clear()
        Dim objoutputxml As New XmlDocument
        Try
            strout = GetCallerName(txtOfficeId.Text)
            If strout <> "" Then
                objoutputxml.LoadXml(strout)
                If objoutputxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    For Each objxmlnode As XmlNode In objoutputxml.DocumentElement.SelectNodes("STAFF")
                        DlstCallerName.Items.Insert(DlstCallerName.Items.Count, New ListItem(objxmlnode.Attributes("FIRSTNAME").Value + objxmlnode.Attributes("SURNAME").Value + "-" + objxmlnode.Attributes("SIGNINID").Value, objxmlnode.Attributes("FIRSTNAME").Value + objxmlnode.Attributes("SURNAME").Value + "-" + objxmlnode.Attributes("SIGNINID").Value))
                    Next
                Else

                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            DlstCallerName.Items.Insert(0, New ListItem("--Select One--", ""))
        End Try
    End Sub



#Region "Code Added By Abhishek on 22/06/10"

    <System.Web.Script.Services.ScriptMethod()> _
<WebMethod()> _
Public Shared Function GetCallerNameForFunctional(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
        Dim objbzAgencyStaff As New AAMS.bizTravelAgency.bzAgencyStaff
        Dim objInputXml As New XmlDocument
        Dim objOXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        Dim objXmlList As XmlNodeList
        Dim stn(0) As String
        'Dim strContextKey As String = contextKey


        Try
            objInputXml.LoadXml("<TA_SEARCHSTAFF_INPUT><Source></Source><STYPE></STYPE><LCODE></LCODE><STAFFNAME></STAFFNAME><AGENCYNAME></AGENCYNAME><OFFICEID/><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><EmployeeID/></TA_SEARCHSTAFF_INPUT>")

            If Not HttpContext.Current.Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = HttpContext.Current.Session("LoginSession").ToString().Split("|")(0)
            End If

            objInputXml.DocumentElement.SelectSingleNode("STAFFNAME").InnerText = prefixText
            objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerText = contextKey.Split("|").GetValue(1).ToString
            objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = contextKey.Split("|").GetValue(0).ToString


            objInputXml.DocumentElement.SelectSingleNode("Source").InnerText = "Helpdesk"

            objInputXml.DocumentElement.SelectSingleNode("STYPE").InnerText = "WEBSERVICE"

            If HttpContext.Current.Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(HttpContext.Current.Session("Security"))

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        End If
                    End If
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        End If
                    End If
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        If Not HttpContext.Current.Session("LoginSession") Is Nothing Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = HttpContext.Current.Session("LoginSession").ToString().Split("|")(0)
                        End If
                    End If
                End If

            End If

            objOXml = objbzAgencyStaff.Search(objInputXml)

            If objOXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value = "TRUE" Then

                stn(0) = "" ' objOXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                Return stn
            Else
                If count = 0 Then
                    count = 10
                End If

                objXmlList = objOXml.DocumentElement.SelectNodes("STAFF")
                Dim st(objXmlList.Count - 1) As String
                For i As Integer = 0 To objXmlList.Count - 1
                    st(i) = objXmlList.Item(i).Attributes("STAFFNAME").InnerText
                Next
                Return st
            End If

        Catch ex As Exception
        End Try

    End Function
    Protected Sub gvSolution_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSolution.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim txtSoln As TextBox = CType(e.Row.FindControl("txtSoln"), TextBox)
                Dim HdRowId As HiddenField = CType(e.Row.FindControl("HdSLRowId"), HiddenField)

                Dim lnkView As LinkButton = CType(e.Row.FindControl("lnkView"), LinkButton)
                Dim LnkDelete As LinkButton = CType(e.Row.FindControl("LnkDelete"), LinkButton)

                Dim hdRowType As HiddenField = CType(e.Row.FindControl("hdRowType"), HiddenField)
                Dim hdRowNo As HiddenField = CType(e.Row.FindControl("hdRowNo"), HiddenField)


                If hdRowType.Value.Trim.ToUpper <> "N" Then
                    txtSoln.Attributes.Add("onclick", "return CallLogDescription('sl','" + objED.Encrypt(hdPageHD_RE_ID.Value) + "','" + objED.Encrypt(HdRowId.Value) + "')")
                End If
                If hdRowType.Value.Trim.ToUpper = "N" Then
                    'If hdRowType.Value.Trim.ToUpper = "N" And lnkView.Text.Trim.Length > 0 Then
                    LnkDelete.Enabled = True
                Else
                    LnkDelete.Enabled = False
                End If

                If lnkView.Text.Trim.Length > 0 Then
                    lnkView.Enabled = True
                Else
                    lnkView.Enabled = False
                End If


                'If DataBinder.Eval(e.Row.DataItem, "Attach").ToString = "" Then
                '    CType(e.Row.FindControl("lnkView"), LinkButton).Enabled = False
                'End If
                ' txtSoln.Attributes.Add("onclick", "return CallLogDescription('sl'," + hdPageHD_RE_ID.Value + "','" + HdRowId.Value + "')")
                'txtSoln.Attributes.Add("onclick", "return CallLogDescription('" + HdRowId.Value + "','" + hdPageHD_RE_ID.Value + "')")
            End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnDUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDUpload.Click
        hdTabType.Value = 1
        '<DESC_FILE SNO='' USER='' DATETIME='' FILENAME=''/>
        Dim objMultiLTRInputXml As New XmlDocument
        Dim objDescInputXml As New XmlDocument
        Dim objInputXml As New XmlDocument
        Dim objXmlReader As XmlReader
        Dim ds As New DataSet
        Dim file As HttpPostedFile
        Dim strFullFileName As String = ""
        Dim strFilePath As String = ""
        Dim strFileName As String = ""
        Try
            ' objInputXml.LoadXml("<Root><DESC_FILE  HD_RE_ID=''  SNO='' USER='' DATETIME='' FILENAME=''/></Root>")
            objInputXml.LoadXml("<Root><DESC_FILE HD_RE_ID ='' SNO='' USER='' DATETIME='' FILENAME='' DESC='' /></Root>")
            '     <REQUESTSOLUTION HD_RE_ID="211647" ID="223930" EmployeeName="Admin" ACTION_TAKEN="k" DATETIME="14-Apr-10 12:32" Attach="" TempAdded="" /> 

            If Session("TempMultiLTRXml") IsNot Nothing Then
                objMultiLTRInputXml.LoadXml(Session("TempMultiLTRXml"))
            End If

            '@ Codde Added By Abhishek
            If Session("DESC") IsNot Nothing Then
                objDescInputXml.LoadXml(Session("DESC"))
            Else
                objDescInputXml.LoadXml("<HD_GETREQUESTDESCRIPTION_OUTPUT><PAGE PAGE_COUNT='0' TOTAL_ROWS='0' /> <Errors Status='FALSE'><Error Code='' Description='' /></Errors></HD_GETREQUESTDESCRIPTION_OUTPUT>")
            End If

            '@ Code For Saving at disk
            file = FileUploadDesc.PostedFile
            If file.FileName.Trim = "" Then
                lblError.Text = "There is no file for uploading."
                Exit Sub
            End If
            'If file.FileName <> "" Then
            '    If file.ContentLength > 5242880 Then  ' 1024 bytes= 1KB
            '        lblError.Text = "The selected  file size is greater than 5MB."
            '        Exit Sub
            '    End If
            'End If

            If FileUploadDesc.HasFile Then

                If Not FileUploadSol Is Nothing Then
                    strFilePath = file.FileName
                    strFileName = "D" + Now.ToString("ddMMyyyy") + "_" + txtOfficeId.Text + "_" + hdPageHD_RE_ID.Value + "_" + Path.GetFileName(strFilePath)
                    strFullFileName = Server.MapPath("../HelpDeskFiles/") + strFileName
                    If strFilePath <> "" Then
                        file.SaveAs(strFullFileName)
                    End If
                End If
                '@


                objInputXml.DocumentElement.SelectSingleNode("DESC_FILE").Attributes("FILENAME").Value = strFileName ' FileUploadDesc.PostedFile.FileName
                objInputXml.DocumentElement.SelectSingleNode("DESC_FILE").Attributes("DATETIME").Value = Now.ToString("dd/MM/yyy HH:mm")
                objInputXml.DocumentElement.SelectSingleNode("DESC_FILE").Attributes("USER").Value = objeAAMS.EmployeeName(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("DESC_FILE").Attributes("DESC").Value = txtDescription.Text
                objInputXml.DocumentElement.SelectSingleNode("DESC_FILE").Attributes("SNO").Value = objDescInputXml.DocumentElement.SelectNodes("REQUESTDESCRIPTION[@HD_RE_ID='" + txtLTRNo.Text + "']").Count + 1
                objInputXml.DocumentElement.SelectSingleNode("DESC_FILE").Attributes("HD_RE_ID").Value = txtLTRNo.Text


                If Session("TempMultiLTRXml") IsNot Nothing Then
                    objMultiLTRInputXml.DocumentElement.AppendChild(objMultiLTRInputXml.ImportNode(objInputXml.DocumentElement.SelectSingleNode("DESC_FILE"), True))
                    Session("TempMultiLTRXml") = objMultiLTRInputXml.OuterXml

                    If objMultiLTRInputXml.DocumentElement.SelectNodes("DESC_FILE[@HD_RE_ID='" + txtLTRNo.Text + "']").Count > 0 Then
                        'objXmlReader = New XmlNodeReader(objMultiLTRInputXml)
                        'ds.ReadXml(objXmlReader)
                        'If ds.Tables("DESC_FILE").Rows.Count > 0 Then
                        '    gvDUpload.DataSource = Nothing
                        '    gvDUpload.DataBind()
                        '    Dim dv As DataView = ds.Tables("DESC_FILE").DefaultView
                        '    dv.RowFilter = "HD_RE_ID='" + txtLTRNo.Text + "'"
                        '    If dv.Count > 0 Then
                        '        '    dv.Sort = "SNO DESC"
                        '        gvDUpload.DataSource = dv 'ds.Tables("DESC_FILE")
                        '        gvDUpload.DataBind()
                        '    Else
                        '        gvDUpload.DataSource = Nothing
                        '        gvDUpload.DataBind()
                        '    End If
                        'Else
                        '    gvDUpload.DataSource = Nothing
                        '    gvDUpload.DataBind()
                        'End If
                    End If
                End If


                Dim objTempxmldocument As New XmlDocument
                objTempxmldocument.LoadXml("<Root><REQUESTDESCRIPTION HD_RE_ID='' ID='' EmployeeName='' ACTION_TAKEN='' DATETIME='' Attach='' ROWNUMBER='' RTYPE='T' /></Root> ")

                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTDESCRIPTION").Attributes("HD_RE_ID").Value = txtLTRNo.Text
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTDESCRIPTION").Attributes("ID").Value = ""
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTDESCRIPTION").Attributes("EmployeeName").Value = objeAAMS.EmployeeName(Session("Security"))
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTDESCRIPTION").Attributes("ACTION_TAKEN").Value = txtDescription.Text
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTDESCRIPTION").Attributes("DATETIME").Value = Now.ToString("dd/MM/yyy HH:mm")
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTDESCRIPTION").Attributes("Attach").Value = strFileName 'FileUploadDesc.PostedFile.FileName
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTDESCRIPTION").Attributes("ROWNUMBER").Value = objDescInputXml.DocumentElement.SelectNodes("REQUESTDESCRIPTION[@HD_RE_ID='" + txtLTRNo.Text + "']").Count + 1
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTDESCRIPTION").Attributes("RTYPE").Value = "N"

                objDescInputXml.DocumentElement.AppendChild(objDescInputXml.ImportNode(objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTDESCRIPTION"), True))

                Session("DESC") = objDescInputXml.OuterXml

                If objDescInputXml.DocumentElement.SelectNodes("REQUESTDESCRIPTION[@HD_RE_ID='" + txtLTRNo.Text + "']").Count > 0 Then
                    objXmlReader = New XmlNodeReader(objDescInputXml)
                    ds = New DataSet
                    ds.ReadXml(objXmlReader)
                    If ds.Tables("REQUESTDESCRIPTION").Rows.Count > 0 Then
                        gvDescription.DataSource = Nothing
                        gvDescription.DataBind()
                        Dim dv As DataView = ds.Tables("REQUESTDESCRIPTION").DefaultView

                        dv.RowFilter = "HD_RE_ID='" + txtLTRNo.Text + "'"
                        If dv.Count > 0 Then
                            '   dv.Sort = "ROWNUMBER DESC"
                            gvDescription.DataSource = dv ' ds.Tables("REQUESTDESCRIPTION")
                            gvDescription.DataBind()
                        Else
                            gvDescription.DataSource = Nothing
                            gvDescription.DataBind()
                        End If
                    Else
                        gvDescription.DataSource = Nothing
                        gvDescription.DataBind()
                    End If
                End If
            End If
            txtDescription.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally

            'start ,here we store query category and query sub category in a hidden field
            If Request.Form(ddlQueryCategory.UniqueID) <> "" Then
                fillCategoryName(ddlQuerySubGroup.SelectedValue)
                If ddlQueryCategory.Items.Count > 1 Then
                    ddlQueryCategory.SelectedValue = Request.Form(ddlQueryCategory.UniqueID)
                End If
            End If
            If Request.Form(ddlQuerySubCategory.UniqueID) <> "" Then
                fillSubCategoryName(ddlQueryCategory.SelectedValue)
                Dim liSubCategoryName As New ListItem
                liSubCategoryName = ddlQuerySubCategory.Items.FindByValue(Request.Form(ddlQuerySubCategory.UniqueID))
                If liSubCategoryName IsNot Nothing Then
                    ddlQuerySubCategory.SelectedValue = liSubCategoryName.Value
                End If
            End If
            hdTabType.Value = 1
            ActiveTab()
        End Try
    End Sub


    Protected Sub gvDUpload_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvDUpload.RowCommand
        Dim objMultiLTRInputXml As New XmlDocument
        Dim objXmlReader As XmlReader
        Dim ds As New DataSet
        If Session("TempMultiLTRXml") IsNot Nothing Then
            objMultiLTRInputXml.LoadXml(Session("TempMultiLTRXml"))
        End If
        If e.CommandName = "DeleteX" Then
            If objMultiLTRInputXml.DocumentElement.SelectSingleNode("DESC_FILE[@FILENAME='" + e.CommandArgument.ToString + "']") IsNot Nothing Then
                objMultiLTRInputXml.DocumentElement.RemoveChild(objMultiLTRInputXml.DocumentElement.SelectSingleNode("DESC_FILE[@FILENAME='" + e.CommandArgument.ToString + "']"))
            End If
            objXmlReader = New XmlNodeReader(objMultiLTRInputXml)
            ds.ReadXml(objXmlReader)
            Session("TempMultiLTRXml") = objMultiLTRInputXml.OuterXml
            If ds.Tables("DESC_FILE").Rows.Count > 0 Then
                gvDUpload.DataSource = ds.Tables("DESC_FILE")
                gvDUpload.DataBind()
            End If

        End If
    End Sub


    Protected Sub btnSUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSUpload.Click
        hdTabType.Value = 2
        '<DESC_FILE SNO='' USER='' DATETIME='' FILENAME=''/>
        Dim objMultiLTRInputXml As New XmlDocument
        Dim objSolInputXml As New XmlDocument
        Dim objInputXml As New XmlDocument
        Dim objXmlReader As XmlReader
        Dim ds As New DataSet
        Dim file As HttpPostedFile
        Dim strFullFileName As String = ""
        Dim strFilePath As String = ""
        Dim strFileName As String = ""
        Try
            objInputXml.LoadXml("<Root><SOL_FILE HD_RE_ID ='' SNO='' USER='' DATETIME='' FILENAME='' SOLN='' /></Root>")
            '     <REQUESTSOLUTION HD_RE_ID="211647" ID="223930" EmployeeName="Admin" ACTION_TAKEN="k" DATETIME="14-Apr-10 12:32" Attach="" TempAdded="" /> 

            If Session("TempMultiLTRXml") IsNot Nothing Then
                objMultiLTRInputXml.LoadXml(Session("TempMultiLTRXml"))
            End If

            '@ Codde Added By Abhishek
            If Session("SOLUTION") IsNot Nothing Then
                objSolInputXml.LoadXml(Session("SOLUTION"))
            Else
                objSolInputXml.LoadXml("<HD_GETREQUESTSOLUTION_OUTPUT><PAGE PAGE_COUNT='0' TOTAL_ROWS='0' /> <Errors Status='FALSE'><Error Code='' Description='' /></Errors></HD_GETREQUESTSOLUTION_OUTPUT>")
            End If



            '@ Code For Saving at disk
            file = FileUploadSol.PostedFile

            If file.FileName.Trim = "" Then
                lblError.Text = "There is no file for uploading."
                hdTabType.Value = 2
                ActiveTab()
                Exit Sub
            End If
            'If file.FileName <> "" Then
            '    If file.ContentLength > 5242880 Then  ' 1024 bytes= 1KB
            '        lblError.Text = "The selected  file size is greater than 5MB."
            '        Exit Sub
            '    End If
            'End If

            If FileUploadSol.HasFile Then

                If Not FileUploadSol Is Nothing Then
                    strFilePath = file.FileName
                    strFileName = "S" + Now.ToString("ddMMyyyy") + "_" + txtOfficeId.Text + "_" + hdPageHD_RE_ID.Value + "_" + Path.GetFileName(strFilePath)
                    strFullFileName = Server.MapPath("../HelpDeskFiles/") + strFileName
                    If strFilePath <> "" Then
                        file.SaveAs(strFullFileName)
                    End If
                End If
                '@

                objInputXml.DocumentElement.SelectSingleNode("SOL_FILE").Attributes("FILENAME").Value = strFileName ' FileUploadSol.PostedFile.FileName
                objInputXml.DocumentElement.SelectSingleNode("SOL_FILE").Attributes("DATETIME").Value = Now.ToString("dd/MM/yyy HH:mm")
                objInputXml.DocumentElement.SelectSingleNode("SOL_FILE").Attributes("USER").Value = objeAAMS.EmployeeName(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("SOL_FILE").Attributes("SOLN").Value = txtSolution.Text
                objInputXml.DocumentElement.SelectSingleNode("SOL_FILE").Attributes("SNO").Value = objSolInputXml.DocumentElement.SelectNodes("REQUESTSOLUTION[@HD_RE_ID='" + txtLTRNo.Text + "']").Count + 1
                objInputXml.DocumentElement.SelectSingleNode("SOL_FILE").Attributes("HD_RE_ID").Value = txtLTRNo.Text
                If Session("TempMultiLTRXml") IsNot Nothing Then
                    objMultiLTRInputXml.DocumentElement.AppendChild(objMultiLTRInputXml.ImportNode(objInputXml.DocumentElement.SelectSingleNode("SOL_FILE"), True))
                    Session("TempMultiLTRXml") = objMultiLTRInputXml.OuterXml

                    If objMultiLTRInputXml.DocumentElement.SelectNodes("SOL_FILE[@HD_RE_ID='" + txtLTRNo.Text + "']").Count > 0 Then
                        'objXmlReader = New XmlNodeReader(objMultiLTRInputXml)
                        'ds.ReadXml(objXmlReader)
                        'If ds.Tables("SOL_FILE").Rows.Count > 0 Then
                        '    Dim dv As DataView = ds.Tables("SOL_FILE").DefaultView
                        '    dv.RowFilter = "HD_RE_ID='" + txtLTRNo.Text + "'"
                        '    If dv.Count > 0 Then
                        '        '   dv.Sort = "SNO DESC"
                        '        gvSUpload.DataSource = dv 'ds.Tables("SOL_FILE")
                        '        gvSUpload.DataBind()
                        '    Else
                        '        gvSUpload.DataSource = Nothing
                        '        gvSUpload.DataBind()
                        '    End If
                        'Else
                        '    gvSUpload.DataSource = Nothing
                        '    gvSUpload.DataBind()
                        'End If
                    End If

                End If

                Dim objTempxmldocument As New XmlDocument
                objTempxmldocument.LoadXml("<Root><REQUESTSOLUTION HD_RE_ID='' ID='' EmployeeName='' ACTION_TAKEN='' DATETIME='' Attach='' ROWNUMBER='' RTYPE='T' /></Root> ")

                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTSOLUTION").Attributes("HD_RE_ID").Value = txtLTRNo.Text
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTSOLUTION").Attributes("ID").Value = ""
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTSOLUTION").Attributes("EmployeeName").Value = objeAAMS.EmployeeName(Session("Security"))
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTSOLUTION").Attributes("ACTION_TAKEN").Value = txtSolution.Text
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTSOLUTION").Attributes("DATETIME").Value = Now.ToString("dd-mmm-yy HH:mm") ' Now.ToString("dd/MM/yyy HH:mm")
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTSOLUTION").Attributes("Attach").Value = strFileName 'FileUploadSol.PostedFile.FileName
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTSOLUTION").Attributes("ROWNUMBER").Value = objSolInputXml.DocumentElement.SelectNodes("REQUESTSOLUTION[@HD_RE_ID='" + txtLTRNo.Text + "']").Count + 1
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTSOLUTION").Attributes("RTYPE").Value = "N"

                objSolInputXml.DocumentElement.AppendChild(objSolInputXml.ImportNode(objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTSOLUTION"), True))

                Session("SOLUTION") = objSolInputXml.OuterXml
                If objSolInputXml.DocumentElement.SelectNodes("REQUESTSOLUTION[@HD_RE_ID='" + txtLTRNo.Text + "']").Count > 0 Then
                    objXmlReader = New XmlNodeReader(objSolInputXml)
                    ds = New DataSet
                    ds.ReadXml(objXmlReader)
                    If ds.Tables("REQUESTSOLUTION").Rows.Count > 0 Then
                        gvSolution.DataSource = Nothing
                        gvSolution.DataBind()
                        Dim dv As DataView = ds.Tables("REQUESTSOLUTION").DefaultView
                        dv.RowFilter = "HD_RE_ID='" + txtLTRNo.Text + "'"
                        If dv.Count > 0 Then
                            '  dv.Sort = "ROWNUMBER DESC"
                            gvSolution.DataSource = dv 'ds.Tables("REQUESTSOLUTION")
                            gvSolution.DataBind()
                        Else
                            gvSolution.DataSource = Nothing
                            gvSolution.DataBind()
                        End If
                    Else
                        gvSolution.DataSource = Nothing
                        gvSolution.DataBind()
                    End If
                End If
            End If
            txtSolution.Text = ""
        Catch ex As Exception
        Finally
            If Request.Form(ddlQueryCategory.UniqueID) <> "" Then
                fillCategoryName(ddlQuerySubGroup.SelectedValue)
                If ddlQueryCategory.Items.Count > 1 Then
                    ddlQueryCategory.SelectedValue = Request.Form(ddlQueryCategory.UniqueID)
                End If
            End If
            If Request.Form(ddlQuerySubCategory.UniqueID) <> "" Then
                fillSubCategoryName(ddlQueryCategory.SelectedValue)
                Dim liSubCategoryName As New ListItem
                liSubCategoryName = ddlQuerySubCategory.Items.FindByValue(Request.Form(ddlQuerySubCategory.UniqueID))
                If liSubCategoryName IsNot Nothing Then
                    ddlQuerySubCategory.SelectedValue = liSubCategoryName.Value
                End If
            End If
            hdTabType.Value = 2
            ActiveTab()
        End Try
    End Sub

    Sub DeleteSolFile(ByVal strInput As String)
        hdTabType.Value = 2
        Dim objSolInputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim objMultiLTRInputXml As New XmlDocument
        Dim objdeltxmlnode As XmlNode
        Dim ds As DataSet
        Try

            If strInput.Trim = "" Then
                Exit Sub
            End If
            Dim strFileName As String = strInput.Split("|")(1).ToString
            Dim Rowono As String = strInput.Split("|")(0).ToString
            If strFileName.Trim.Trim.Length > 0 Then
                Dim fullpath = Path.GetFullPath(Server.MapPath("~/HelpDeskFiles/") + strFileName)
                '@ Remove from disk
                If File.Exists(fullpath) Then
                    File.Delete(fullpath)
                End If
            End If


            '@ Remove node from TempMultiLTRXml session
            If Session("TempMultiLTRXml") IsNot Nothing Then
                objMultiLTRInputXml.LoadXml(Session("TempMultiLTRXml"))
                objdeltxmlnode = objMultiLTRInputXml.DocumentElement.SelectSingleNode("SOL_FILE[@SNO='" + Rowono + "' and @HD_RE_ID='" + txtLTRNo.Text + "']")
                If objdeltxmlnode IsNot Nothing Then
                    objMultiLTRInputXml.DocumentElement.RemoveChild(objdeltxmlnode)
                End If
                Session("TempMultiLTRXml") = objMultiLTRInputXml.OuterXml

                If objMultiLTRInputXml.DocumentElement.SelectNodes("SOL_FILE[@HD_RE_ID='" + txtLTRNo.Text + "']").Count > 0 Then
                    'objXmlReader = New XmlNodeReader(objMultiLTRInputXml)
                    'ds = New DataSet
                    'ds.ReadXml(objXmlReader)
                    'If ds.Tables("SOL_FILE").Rows.Count > 0 Then
                    '    Dim dv As DataView = ds.Tables("SOL_FILE").DefaultView
                    '    dv.RowFilter = "HD_RE_ID='" + txtLTRNo.Text + "'"
                    '    If dv.Count > 0 Then
                    '        '  dv.Sort = "SNO DESC"
                    '        gvSUpload.DataSource = dv 'ds.Tables("SOL_FILE")
                    '        gvSUpload.DataBind()
                    '    Else
                    '        gvSUpload.DataSource = Nothing
                    '        gvSUpload.DataBind()
                    '    End If
                    'Else
                    '    gvSUpload.DataSource = Nothing
                    '    gvSUpload.DataBind()
                    'End If
                Else
                    gvSUpload.DataSource = Nothing
                    gvSUpload.DataBind()
                End If


            End If

            '@ Remove node from SOLUTION session
            If Session("SOLUTION") IsNot Nothing Then
                objSolInputXml.LoadXml(Session("SOLUTION"))
                objdeltxmlnode = objSolInputXml.DocumentElement.SelectSingleNode("REQUESTSOLUTION[@ROWNUMBER='" + Rowono + "' and @HD_RE_ID='" + txtLTRNo.Text + "']")

                If objdeltxmlnode IsNot Nothing Then
                    objSolInputXml.DocumentElement.RemoveChild(objdeltxmlnode)
                End If
                Session("SOLUTION") = objSolInputXml.OuterXml

                If objSolInputXml.DocumentElement.SelectNodes("REQUESTSOLUTION[@HD_RE_ID='" + txtLTRNo.Text + "']").Count > 0 Then

                    objXmlReader = New XmlNodeReader(objSolInputXml)
                    ds = New DataSet
                    ds.ReadXml(objXmlReader)

                    If ds.Tables("REQUESTSOLUTION").Rows.Count > 0 Then
                        gvSolution.DataSource = Nothing
                        gvSolution.DataBind()
                        Dim dv As DataView = ds.Tables("REQUESTSOLUTION").DefaultView
                        dv.RowFilter = "HD_RE_ID='" + txtLTRNo.Text + "'"
                        If dv.Count > 0 Then
                            '   dv.Sort = "ROWNUMBER DESC"
                            gvSolution.DataSource = dv ' ds.Tables("REQUESTSOLUTION")
                            gvSolution.DataBind()
                        Else
                            gvSolution.DataSource = Nothing
                            gvSolution.DataBind()
                        End If
                    Else
                        gvSolution.DataSource = Nothing
                        gvSolution.DataBind()
                    End If
                Else
                    gvSolution.DataSource = Nothing
                    gvSolution.DataBind()
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            If Request.Form(ddlQueryCategory.UniqueID) <> "" Then
                fillCategoryName(ddlQuerySubGroup.SelectedValue)
                If ddlQueryCategory.Items.Count > 1 Then
                    ddlQueryCategory.SelectedValue = Request.Form(ddlQueryCategory.UniqueID)
                End If
            End If
            If Request.Form(ddlQuerySubCategory.UniqueID) <> "" Then
                fillSubCategoryName(ddlQueryCategory.SelectedValue)
                Dim liSubCategoryName As New ListItem
                liSubCategoryName = ddlQuerySubCategory.Items.FindByValue(Request.Form(ddlQuerySubCategory.UniqueID))
                If liSubCategoryName IsNot Nothing Then
                    ddlQuerySubCategory.SelectedValue = liSubCategoryName.Value
                End If
            End If
            hdTabType.Value = 2
            ActiveTab()
        End Try
    End Sub

    Sub DeleteDescFile(ByVal strInput As String)
        hdTabType.Value = 1
        Dim objDescInputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim objMultiLTRInputXml As New XmlDocument
        Dim objdeltxmlnode As XmlNode
        Dim ds As DataSet
        Try


            If strInput.Trim = "" Then
                Exit Sub
            End If



            Dim strFileName As String = strInput.Split("|")(1).ToString
            Dim Rowono As String = strInput.Split("|")(0).ToString
            If strFileName.Trim.Trim.Length > 0 Then
                Dim fullpath = Path.GetFullPath(Server.MapPath("~/HelpDeskFiles/") + strFileName)
                '@ Remove from disk
                If File.Exists(fullpath) Then
                    File.Delete(fullpath)
                End If
            End If


            '@ Remove node from TempMultiLTRXml session
            If Session("TempMultiLTRXml") IsNot Nothing Then
                objMultiLTRInputXml.LoadXml(Session("TempMultiLTRXml"))
                objdeltxmlnode = objMultiLTRInputXml.DocumentElement.SelectSingleNode("DESC_FILE[@SNO='" + Rowono + "' and @HD_RE_ID='" + txtLTRNo.Text + "']")
                If objdeltxmlnode IsNot Nothing Then
                    objMultiLTRInputXml.DocumentElement.RemoveChild(objdeltxmlnode)
                End If
                Session("TempMultiLTRXml") = objMultiLTRInputXml.OuterXml

                If objMultiLTRInputXml.DocumentElement.SelectNodes("DESC_FILE[@HD_RE_ID='" + txtLTRNo.Text + "']").Count > 0 Then
                    'objXmlReader = New XmlNodeReader(objMultiLTRInputXml)
                    'ds = New DataSet
                    'ds.ReadXml(objXmlReader)
                    'If ds.Tables("DESC_FILE").Rows.Count > 0 Then

                    '    Dim dv As DataView = ds.Tables("DESC_FILE").DefaultView
                    '    dv.RowFilter = "HD_RE_ID='" + txtLTRNo.Text + "'"
                    '    If dv.Count > 0 Then
                    '        '   dv.Sort = "SNO DESC"
                    '        gvDUpload.DataSource = dv 'ds.Tables("DESC_FILE")
                    '        gvDUpload.DataBind()
                    '    Else
                    '        gvDUpload.DataSource = Nothing
                    '        gvDUpload.DataBind()
                    '    End If
                    'Else
                    '    gvDUpload.DataSource = Nothing
                    '    gvDUpload.DataBind()
                    'End If
                Else
                    gvDUpload.DataSource = Nothing
                    gvDUpload.DataBind()
                End If
            End If

            '@ Remove node from DESC session
            If Session("DESC") IsNot Nothing Then
                objDescInputXml.LoadXml(Session("DESC"))
                objdeltxmlnode = objDescInputXml.DocumentElement.SelectSingleNode("REQUESTDESCRIPTION[@ROWNUMBER='" + Rowono + "' and @HD_RE_ID='" + txtLTRNo.Text + "']")
                If objdeltxmlnode IsNot Nothing Then
                    objDescInputXml.DocumentElement.RemoveChild(objdeltxmlnode)
                End If
                Session("DESC") = objDescInputXml.OuterXml

                If objDescInputXml.DocumentElement.SelectNodes("REQUESTDESCRIPTION[@HD_RE_ID='" + txtLTRNo.Text + "']").Count > 0 Then

                    objXmlReader = New XmlNodeReader(objDescInputXml)
                    ds = New DataSet
                    ds.ReadXml(objXmlReader)
                    If ds.Tables("REQUESTDESCRIPTION").Rows.Count > 0 Then
                        gvDescription.DataSource = Nothing
                        gvDescription.DataBind()
                        Dim dv As DataView = ds.Tables("REQUESTDESCRIPTION").DefaultView
                        dv.RowFilter = "HD_RE_ID='" + txtLTRNo.Text + "'"
                        If dv.Count > 0 Then
                            ' dv.Sort = "ROWNUMBER DESC"
                            gvDescription.DataSource = dv ' ds.Tables("REQUESTDESCRIPTION")
                            gvDescription.DataBind()
                        Else
                            gvDescription.DataSource = Nothing
                            gvDescription.DataBind()
                        End If
                    Else
                        gvDescription.DataSource = Nothing
                        gvDescription.DataBind()
                    End If
                Else
                    gvDescription.DataSource = Nothing
                    gvDescription.DataBind()
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally

            If Request.Form(ddlQueryCategory.UniqueID) <> "" Then
                fillCategoryName(ddlQuerySubGroup.SelectedValue)
                If ddlQueryCategory.Items.Count > 1 Then
                    ddlQueryCategory.SelectedValue = Request.Form(ddlQueryCategory.UniqueID)
                End If
            End If
            If Request.Form(ddlQuerySubCategory.UniqueID) <> "" Then
                fillSubCategoryName(ddlQueryCategory.SelectedValue)
                Dim liSubCategoryName As New ListItem
                liSubCategoryName = ddlQuerySubCategory.Items.FindByValue(Request.Form(ddlQuerySubCategory.UniqueID))
                If liSubCategoryName IsNot Nothing Then
                    ddlQuerySubCategory.SelectedValue = liSubCategoryName.Value
                End If
            End If
            hdTabType.Value = 1
            ActiveTab()
        End Try
    End Sub

    Private Sub UpdatesessionvalueForSingleRecords()
        '@ Start of Code By abhishek on 01/06/10 
        ' @ Remove all temporary added attachment file
        If Session("TempMultiLTRXml") IsNot Nothing Then
            Dim objMultiLTRInputXml As New XmlDataDocument
            objMultiLTRInputXml.LoadXml(Session("TempMultiLTRXml"))
            For Each objxmlnode As XmlNode In objMultiLTRInputXml.DocumentElement.SelectNodes("DESC_FILE[@HD_RE_ID='" + txtLTRNo.Text + "']")
                objMultiLTRInputXml.DocumentElement.RemoveChild(objxmlnode)
            Next

            For Each objxmlnode As XmlNode In objMultiLTRInputXml.DocumentElement.SelectNodes("SOL_FILE[@HD_RE_ID='" + txtLTRNo.Text + "']")
                objMultiLTRInputXml.DocumentElement.RemoveChild(objxmlnode)
            Next

            Session("TempMultiLTRXml") = objMultiLTRInputXml.OuterXml
        End If

        If Session("SOLUTION") IsNot Nothing Then
            Dim objSolnInputXml As New XmlDataDocument
            objSolnInputXml.LoadXml(Session("SOLUTION"))
            For Each objxmlnode As XmlNode In objSolnInputXml.DocumentElement.SelectNodes("REQUESTSOLUTION[@RTYPE='N' and @HD_RE_ID='" + txtLTRNo.Text + "']")
                objSolnInputXml.DocumentElement.RemoveChild(objxmlnode)
            Next
            Session("SOLUTION") = objSolnInputXml.OuterXml
        End If
        If Session("DESC") IsNot Nothing Then
            Dim objDescInputXml As New XmlDataDocument
            objDescInputXml.LoadXml(Session("DESC"))
            For Each objxmlnode As XmlNode In objDescInputXml.DocumentElement.SelectNodes("REQUESTDESCRIPTION[@RTYPE='N' and @HD_RE_ID='" + txtLTRNo.Text + "']")
                objDescInputXml.DocumentElement.RemoveChild(objxmlnode)
            Next
            Session("DESC") = objDescInputXml.OuterXml
        End If
        '@ Start of Code By abhishek on 01/06/10
    End Sub
    Private Sub UpdatesessionForMultLTRcase()
        '@ Start of Code By abhishek on 01/06/10 
        ' @ Remove all teporay added attachment file
        If Session("TempMultiLTRXml") IsNot Nothing Then
            Dim objMultiLTRInputXml As New XmlDataDocument
            objMultiLTRInputXml.LoadXml(Session("TempMultiLTRXml"))
            For Each objxmlnode As XmlNode In objMultiLTRInputXml.DocumentElement.SelectNodes("DESC_FILE")
                objMultiLTRInputXml.DocumentElement.RemoveChild(objxmlnode)
            Next
            For Each objxmlnode As XmlNode In objMultiLTRInputXml.DocumentElement.SelectNodes("SOL_FILE")
                objMultiLTRInputXml.DocumentElement.RemoveChild(objxmlnode)
            Next
            Session("TempMultiLTRXml") = objMultiLTRInputXml.OuterXml
        End If

        If Session("SOLUTION") IsNot Nothing Then
            Dim objSolnInputXml As New XmlDataDocument
            objSolnInputXml.LoadXml(Session("SOLUTION"))
            For Each objxmlnode As XmlNode In objSolnInputXml.DocumentElement.SelectNodes("REQUESTSOLUTION[@RTYPE='N']")
                objSolnInputXml.DocumentElement.RemoveChild(objxmlnode)
            Next
            Session("SOLUTION") = objSolnInputXml.OuterXml
        End If

        If Session("DESC") IsNot Nothing Then
            Dim objDescInputXml As New XmlDataDocument
            objDescInputXml.LoadXml(Session("DESC"))
            For Each objxmlnode As XmlNode In objDescInputXml.DocumentElement.SelectNodes("REQUESTDESCRIPTION[@RTYPE='N']")
                objDescInputXml.DocumentElement.RemoveChild(objxmlnode)
            Next
            Session("DESC") = objDescInputXml.OuterXml
        End If

    End Sub

    Private Sub AddDescAndSoln(ByRef objInputXml As XmlDocument)
        Dim objTempInputXml As New XmlDocument
        Dim objNode, objNodeClone As XmlNode
        If Session("DESC") IsNot Nothing Then
            '@ Find all temporary added desc/attachment
            Dim objDescxmldocument As New XmlDocument
            objTempInputXml = New XmlDocument

            objDescxmldocument.LoadXml(Session("DESC").ToString())
            Dim objnodelist As XmlNodeList = objDescxmldocument.DocumentElement.SelectNodes("REQUESTDESCRIPTION[@RTYPE='N' and @HD_RE_ID='" + txtLTRNo.Text + "']")

            objTempInputXml.LoadXml("<Root><DESC_FILE SNO='' USER='' DATETIME='' FILENAME='' DESC='' HD_RE_ID='' /></Root>")
            objNode = objTempInputXml.DocumentElement.SelectSingleNode("DESC_FILE")
            objNodeClone = objNode.CloneNode(True)

            For Each objxmlnode As XmlNode In objnodelist
                With objNodeClone
                    '  <REQUESTDESCRIPTION HD_RE_ID='' ID='' EmployeeName='' ACTION_TAKEN='' DATETIME='' Attach='' ROWNUMBER='' RTYPE='T' />
                    .Attributes("SNO").Value = objxmlnode.Attributes("ROWNUMBER").Value
                    .Attributes("USER").Value = objxmlnode.Attributes("EmployeeName").Value
                    .Attributes("DATETIME").Value = objxmlnode.Attributes("DATETIME").Value
                    .Attributes("FILENAME").Value = objxmlnode.Attributes("Attach").Value
                    .Attributes("DESC").Value = objxmlnode.Attributes("ACTION_TAKEN").Value
                    .Attributes("HD_RE_ID").Value = txtLTRNo.Text
                End With
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objNodeClone, True))
                objNodeClone = objNode.CloneNode(True)
            Next
        End If

        If Session("SOLUTION") IsNot Nothing Then
            '@ Find all temporary added Solution/attachment
            Dim objSolnxmldocument As New XmlDocument
            objTempInputXml = New XmlDocument
            objSolnxmldocument.LoadXml(Session("SOLUTION").ToString())
            Dim objnodelist As XmlNodeList = objSolnxmldocument.DocumentElement.SelectNodes("REQUESTSOLUTION[@RTYPE='N' and @HD_RE_ID='" + txtLTRNo.Text + "']")

            objTempInputXml.LoadXml("<Root><SOL_FILE SNO='' USER='' DATETIME='' FILENAME='' SOLN='' HD_RE_ID=''  /></Root>")
            objNode = objTempInputXml.DocumentElement.SelectSingleNode("SOL_FILE")
            objNodeClone = objNode.CloneNode(True)

            For Each objxmlnode As XmlNode In objnodelist
                With objNodeClone
                    '  <REQUESTSOLUTION HD_RE_ID='' ID='' EmployeeName='' ACTION_TAKEN='' DATETIME='' Attach='' ROWNUMBER='' RTYPE='T' />
                    .Attributes("SNO").Value = objxmlnode.Attributes("ROWNUMBER").Value
                    .Attributes("USER").Value = objxmlnode.Attributes("EmployeeName").Value
                    .Attributes("DATETIME").Value = objxmlnode.Attributes("DATETIME").Value
                    .Attributes("FILENAME").Value = objxmlnode.Attributes("Attach").Value
                    .Attributes("SOLN").Value = objxmlnode.Attributes("ACTION_TAKEN").Value
                    .Attributes("HD_RE_ID").Value = txtLTRNo.Text
                End With
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objNodeClone, True))
                objNodeClone = objNode.CloneNode(True)
            Next
        End If


        '@if Description textbox is not empty then add extra node for desc
        If txtDescription.Text.Trim.Length > 0 Then
            objTempInputXml = New XmlDocument
            objTempInputXml.LoadXml("<Root><DESC_FILE SNO='' USER='' DATETIME='' FILENAME='' DESC='' HD_RE_ID=''  /></Root>")
            objNode = objTempInputXml.DocumentElement.SelectSingleNode("DESC_FILE")
            objNodeClone = objNode.CloneNode(True)
            With objNodeClone
                .Attributes("SNO").Value = objInputXml.DocumentElement.SelectNodes("DESC_FILE").Count + 1
                .Attributes("USER").Value = objeAAMS.EmployeeName(Session("Security"))
                .Attributes("DATETIME").Value = Now.ToString("dd/MM/yyy HH:mm")
                .Attributes("FILENAME").Value = ""
                .Attributes("DESC").Value = txtDescription.Text
                .Attributes("HD_RE_ID").Value = txtLTRNo.Text
            End With
            objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objNodeClone, True))
            objNodeClone = objNode.CloneNode(True)
        End If

        '@if Solution textbox is not empty then add extra node for Soln
        If txtSolution.Text.Trim.Length > 0 Then
            objTempInputXml = New XmlDocument
            objTempInputXml.LoadXml("<Root><SOL_FILE SNO='' USER='' DATETIME='' FILENAME='' SOLN='' HD_RE_ID=''  /></Root>")
            objNode = objTempInputXml.DocumentElement.SelectSingleNode("SOL_FILE")
            objNodeClone = objNode.CloneNode(True)
            With objNodeClone
                .Attributes("SNO").Value = objInputXml.DocumentElement.SelectNodes("SOL_FILE").Count + 1
                .Attributes("USER").Value = objeAAMS.EmployeeName(Session("Security"))
                .Attributes("DATETIME").Value = Now.ToString("dd/MM/yyy HH:mm")
                .Attributes("FILENAME").Value = ""
                .Attributes("SOLN").Value = txtSolution.Text
                .Attributes("HD_RE_ID").Value = txtLTRNo.Text
            End With
            objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objNodeClone, True))
            objNodeClone = objNode.CloneNode(True)
        End If

    End Sub
    Private Sub AddSolnAndDescForMultiltr(ByRef objMultiLTRInputXml)



        '<DESC_FILE SNO='' USER='' DATETIME='' FILENAME=''/>

        Dim objDescInputXml As New XmlDocument
        Dim objSolInputXml As New XmlDocument
        Dim objInputXml As New XmlDocument
        Dim objXmlReader As XmlReader
        Dim objTempxmldocument As New XmlDocument
        Dim objTempInputXml As New XmlDocument
        Dim objNode, objNodeClone As XmlNode
        Dim ds As New DataSet
        Try

            If txtDescription.Text.Trim.Length > 0 Then
                objInputXml.LoadXml("<Root><DESC_FILE HD_RE_ID ='' SNO='' USER='' DATETIME='' FILENAME='' DESC='' /></Root>")

                '@ Codde Added By Abhishek
                If Session("DESC") IsNot Nothing Then
                    objDescInputXml.LoadXml(Session("DESC"))
                Else
                    objDescInputXml.LoadXml("<HD_GETREQUESTDESCRIPTION_OUTPUT><PAGE PAGE_COUNT='0' TOTAL_ROWS='0' /> <Errors Status='FALSE'><Error Code='' Description='' /></Errors></HD_GETREQUESTDESCRIPTION_OUTPUT>")
                End If


                '@if Description textbox is not empty then add extra node for desc
                '  If objMultiLTRInputXml.DocumentElement.SelectNodes("DESC_FILE[@HD_RE_ID='" + txtLTRNo.Text + "' and @DESC='' ]").Count > 0 Then
                objTempInputXml = New XmlDocument
                objTempInputXml.LoadXml("<Root><DESC_FILE SNO='' USER='' DATETIME='' FILENAME='' DESC='' HD_RE_ID=''  /></Root>")
                objNode = objTempInputXml.DocumentElement.SelectSingleNode("DESC_FILE")
                objNodeClone = objNode.CloneNode(True)
                With objNodeClone
                    .Attributes("SNO").Value = objDescInputXml.DocumentElement.SelectNodes("REQUESTDESCRIPTION[@HD_RE_ID='" + txtLTRNo.Text + "']").Count + 1
                    .Attributes("USER").Value = objeAAMS.EmployeeName(Session("Security"))
                    .Attributes("DATETIME").Value = Now.ToString("dd/MM/yyy HH:mm")
                    .Attributes("FILENAME").Value = ""
                    .Attributes("DESC").Value = txtDescription.Text
                    .Attributes("HD_RE_ID").Value = txtLTRNo.Text
                End With
                objMultiLTRInputXml.DocumentElement.AppendChild(objMultiLTRInputXml.ImportNode(objNodeClone, True))



                objTempxmldocument.LoadXml("<Root><REQUESTDESCRIPTION HD_RE_ID='' ID='' EmployeeName='' ACTION_TAKEN='' DATETIME='' Attach='' ROWNUMBER='' RTYPE='T' /></Root> ")

                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTDESCRIPTION").Attributes("HD_RE_ID").Value = txtLTRNo.Text
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTDESCRIPTION").Attributes("ID").Value = ""
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTDESCRIPTION").Attributes("EmployeeName").Value = objeAAMS.EmployeeName(Session("Security"))
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTDESCRIPTION").Attributes("ACTION_TAKEN").Value = txtDescription.Text
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTDESCRIPTION").Attributes("DATETIME").Value = Now.ToString("dd/MM/yyy HH:mm")
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTDESCRIPTION").Attributes("Attach").Value = ""
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTDESCRIPTION").Attributes("ROWNUMBER").Value = objDescInputXml.DocumentElement.SelectNodes("REQUESTDESCRIPTION[@HD_RE_ID='" + txtLTRNo.Text + "']").Count + 1
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTDESCRIPTION").Attributes("RTYPE").Value = "N"

                objDescInputXml.DocumentElement.AppendChild(objDescInputXml.ImportNode(objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTDESCRIPTION"), True))

                Session("DESC") = objDescInputXml.OuterXml

                If objDescInputXml.DocumentElement.SelectNodes("REQUESTDESCRIPTION[@HD_RE_ID='" + txtLTRNo.Text + "']").Count > 0 Then
                    objXmlReader = New XmlNodeReader(objDescInputXml)
                    ds = New DataSet
                    ds.ReadXml(objXmlReader)
                    If ds.Tables("REQUESTDESCRIPTION").Rows.Count > 0 Then
                        gvDescription.DataSource = Nothing
                        gvDescription.DataBind()
                        Dim dv As DataView = ds.Tables("REQUESTDESCRIPTION").DefaultView

                        dv.RowFilter = "HD_RE_ID='" + txtLTRNo.Text + "'"
                        If dv.Count > 0 Then
                            '   dv.Sort = "ROWNUMBER DESC"
                            gvDescription.DataSource = dv ' ds.Tables("REQUESTDESCRIPTION")
                            gvDescription.DataBind()
                        Else
                            gvDescription.DataSource = Nothing
                            gvDescription.DataBind()
                        End If
                    Else
                        gvDescription.DataSource = Nothing
                        gvDescription.DataBind()
                    End If
                End If



                'End If
                txtDescription.Text = ""
            End If


            '@ Now Add Soln
            If txtSolution.Text.Trim.Length > 0 Then
                If Session("SOLUTION") IsNot Nothing Then
                    objSolInputXml.LoadXml(Session("SOLUTION"))
                Else
                    objSolInputXml.LoadXml("<HD_GETREQUESTSOLUTION_OUTPUT><PAGE PAGE_COUNT='0' TOTAL_ROWS='0' /> <Errors Status='FALSE'><Error Code='' Description='' /></Errors></HD_GETREQUESTSOLUTION_OUTPUT>")
                End If


                '@if Solution textbox is not empty then add extra node for Soln

                ' If objMultiLTRInputXml.DocumentElement.SelectNodes("SOL_FILE[@HD_RE_ID='" + txtLTRNo.Text + "' and @SOLN='' ]").Count > 0 Then

                objTempInputXml = New XmlDocument
                objTempInputXml.LoadXml("<Root><SOL_FILE SNO='' USER='' DATETIME='' FILENAME='' SOLN='' HD_RE_ID=''  /></Root>")
                objNode = objTempInputXml.DocumentElement.SelectSingleNode("SOL_FILE")
                objNodeClone = objNode.CloneNode(True)
                With objNodeClone
                    .Attributes("SNO").Value = objSolInputXml.DocumentElement.SelectNodes("REQUESTSOLUTION[@HD_RE_ID='" + txtLTRNo.Text + "']").Count + 1
                    .Attributes("USER").Value = objeAAMS.EmployeeName(Session("Security"))
                    .Attributes("DATETIME").Value = Now.ToString("dd/MM/yyy HH:mm")
                    .Attributes("FILENAME").Value = ""
                    .Attributes("SOLN").Value = txtSolution.Text
                    .Attributes("HD_RE_ID").Value = txtLTRNo.Text
                End With
                objMultiLTRInputXml.DocumentElement.AppendChild(objMultiLTRInputXml.ImportNode(objNodeClone, True))

                'End If



                objTempxmldocument.LoadXml("<Root><REQUESTSOLUTION HD_RE_ID='' ID='' EmployeeName='' ACTION_TAKEN='' DATETIME='' Attach='' ROWNUMBER='' RTYPE='T' /></Root> ")

                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTSOLUTION").Attributes("HD_RE_ID").Value = txtLTRNo.Text
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTSOLUTION").Attributes("ID").Value = ""
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTSOLUTION").Attributes("EmployeeName").Value = objeAAMS.EmployeeName(Session("Security"))
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTSOLUTION").Attributes("ACTION_TAKEN").Value = txtSolution.Text
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTSOLUTION").Attributes("DATETIME").Value = Now.ToString("dd-mmm-yy HH:mm") ' Now.ToString("dd/MM/yyy HH:mm")
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTSOLUTION").Attributes("Attach").Value = "" 'FileUploadSol.PostedFile.FileName
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTSOLUTION").Attributes("ROWNUMBER").Value = objSolInputXml.DocumentElement.SelectNodes("REQUESTSOLUTION[@HD_RE_ID='" + txtLTRNo.Text + "']").Count + 1
                objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTSOLUTION").Attributes("RTYPE").Value = "N"

                objSolInputXml.DocumentElement.AppendChild(objSolInputXml.ImportNode(objTempxmldocument.DocumentElement.SelectSingleNode("REQUESTSOLUTION"), True))

                Session("SOLUTION") = objSolInputXml.OuterXml
                If objSolInputXml.DocumentElement.SelectNodes("REQUESTSOLUTION[@HD_RE_ID='" + txtLTRNo.Text + "']").Count > 0 Then
                    objXmlReader = New XmlNodeReader(objSolInputXml)
                    ds = New DataSet
                    ds.ReadXml(objXmlReader)
                    If ds.Tables("REQUESTSOLUTION").Rows.Count > 0 Then
                        gvSolution.DataSource = Nothing
                        gvSolution.DataBind()
                        Dim dv As DataView = ds.Tables("REQUESTSOLUTION").DefaultView
                        dv.RowFilter = "HD_RE_ID='" + txtLTRNo.Text + "'"
                        If dv.Count > 0 Then
                            '  dv.Sort = "ROWNUMBER DESC"
                            gvSolution.DataSource = dv 'ds.Tables("REQUESTSOLUTION")
                            gvSolution.DataBind()
                        Else
                            gvSolution.DataSource = Nothing
                            gvSolution.DataBind()
                        End If
                    Else
                        gvSolution.DataSource = Nothing
                        gvSolution.DataBind()
                    End If
                End If




                txtSolution.Text = ""
            End If


        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub

    Private Sub ActiveTab()

        'For i As Integer = 0 To theTabStrip.Items.Count - 1
        '    Dim Button1 As Button
        '    Button1 = theTabStrip.Items(i).FindControl("Button1")
        '    If Button1.CssClass <> "displayNone" Then
        '        Button1.CssClass = "headingtabactive"
        '    End If
        '    If Button1.Text = "Description" Then
        '        If hdTabType.Value = 1 Then
        '            Button1.CssClass = "headingtab"
        '        End If
        '    End If

        '    If Button1.Text = "Solution" Then
        '        If hdTabType.Value = 2 Then
        '            Button1.CssClass = "headingtab"
        '        End If
        '    End If

        'Next


    End Sub


    Private Function GetIRNOByID(ByVal Id As String)
        Dim objInputXml As New XmlDocument
        Dim objOutputXml As New XmlDocument
        Dim objIR As New AAMS.bizHelpDesk.bzIR
        Dim strIRNUMBER As String = ""
        Try
            objInputXml.LoadXml("<HD_LISTIR_INPUT><HD_RE_ID></HD_RE_ID></HD_LISTIR_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = hdPageHD_RE_ID.Value
            objOutputXml = objIR.ListIR(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Dim objnode As XmlNode = objOutputXml.DocumentElement.SelectSingleNode("IR[@HD_IR_ID='" + Id + "']")
                If objnode IsNot Nothing Then
                    strIRNUMBER = objnode.Attributes("HD_IR_REF").Value
                End If
            End If
        Catch ex As Exception
            Return strIRNUMBER
        End Try
        Return strIRNUMBER
    End Function

#End Region
End Class

