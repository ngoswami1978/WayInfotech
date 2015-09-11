Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.IO
Imports System
Imports Excel
Partial Class ETHelpDesk_HDRPT_TechnicalReport
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

            hdToTime.Value = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm")
            hdFromTime.Value = System.DateTime.Now.Subtract(New TimeSpan(6, 0, 0)).ToString("dd/MM/yyyy HH:mm")
            hdPendingTime.Value = System.DateTime.Now.Subtract(New TimeSpan(30, 0, 0, 0)).ToString("dd/MM/yyyy HH:mm")
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

            txtCloseDateFrom1.Text = Request.Form("txtCloseDateFrom1")
            txtCloseDateTo1.Text = Request.Form("txtCloseDateTo1")
            'txtDateAssigned.Text = Request.Form("txtDateAssigned")           
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If
            txtAgencyName.Attributes.Add("onkeydown", "return ActDeAct();")

            If Not Page.IsPostBack Then
                Session("DataSource") = Nothing
                BtnPrint.Attributes.Add("onClick", "return ValidateForm();")
                '   btnExtract.Attributes.Add("onClick", "return ValidateForm();")
                'objeAAMS.BindDropDown(ddlCustomerCategory, "CUSTOMERCATEGORY", True, 3)
                objeAAMS.BindDropDown(ddlAOffice, "AOFFICE", True, 3)
                objeAAMS.BindDropDown(ddlAgencyAOffice, "AOFFICE", True, 3)
                objeAAMS.BindDropDown(drpAssignedTo, "EMPLOYEE", True, 3)
                'objeAAMS.BindDropDown(ddlDisposition, "DISPOSITION", True, 3)
                'objeAAMS.BindDropDown(ddlCoordinator1, "COORDINATOR1", True, 3)
                'objeAAMS.BindDropDown(ddlCoordinator2, "COORDINATOR2", True, 3)
                'objeAAMS.BindDropDown(ddlState, "STATE", True, 3)
                'objeAAMS.BindDropDown(ddlCity, "CITY", True, 3)
                'objeAAMS.BindDropDown(ddlRegion, "REGION", True, 3)
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
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ET Technical Report']").Count <> 0 Then
                        strBuilderTechnical = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ET Technical Report']").Attributes("Value").Value)
                    End If
                    If strBuilderTechnical(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        ' btnExtract.Enabled = False
                    End If
                    If strBuilderTechnical(0) = "4" Then
                        BtnPrint.Enabled = False
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
        End Try
    End Sub

    Protected Sub btnExtract_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExtract.Click
        Try
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

            If Request("txtCloseDateFrom1") IsNot Nothing Then
                txtCloseDateFrom1.Text = Request("txtCloseDateFrom1")
            End If
            If Request("txtCloseDateTo1") IsNot Nothing Then
                txtCloseDateTo1.Text = Request("txtCloseDateTo1")
            End If
        End Try
    End Sub
    Sub loadGroup()
        If Session("Security") IsNot Nothing Then
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            'If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Log HD Call']").Count <> 0 Then
            '    strBuilderFunctional = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Log HD Call']").Attributes("Value").Value)
            'End If
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Log Tech Call']").Count <> 0 Then
                strBuilderTechnical = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Log Tech Call']").Attributes("Value").Value)
            End If
            ddlQueryGroup.Items.Clear()
            'If strBuilderFunctional(0) <> "0" And strBuilderTechnical(0) <> "0" Then
            '    ddlQueryGroup.Items.Insert(0, New ListItem("--All--", ""))
            '    ' ddlQueryGroup.Items.Insert(1, New ListItem("Functional", "1"))
            '    ddlQueryGroup.Items.Insert(2, New ListItem("Technical", "2"))
            'End If

            'If strBuilderFunctional(0) <> "0" And strBuilderTechnical(0) = "0" Then
            '    ddlQueryGroup.Items.Insert(0, New ListItem("Functional", "1"))
            'End If

            'If strBuilderFunctional(0) = "0" And strBuilderTechnical(0) <> "0" Then
            '    ddlQueryGroup.Items.Insert(0, New ListItem("Technical", "2"))
            'End If
            ddlQueryGroup.Items.Insert(0, New ListItem("Technical", "2"))

        End If
    End Sub
    Sub loadSubGroup()
        'Code to fill Query Sub Group
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

    Private Sub SearchRecords()
        lblError.Text = ""
        Dim objInputXml, objOutputXml As New XmlDocument
        '        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzDMReportConfiguration As New AAMS.bizETrackerHelpDesk.bzDMReportConfiguration
        If Session("LoginSession") IsNot Nothing Then
            Dim strSEARCH_INPUT As String = "<HD_DMSEARCH_CALLREQUEST_INPUT><LCODE></LCODE><HD_RE_ID></HD_RE_ID><AgencyName></AgencyName><Address></Address><HD_PTR_REF></HD_PTR_REF><HD_STATUS_ID></HD_STATUS_ID><FollowUp></FollowUp><HD_SEVERITY_ID></HD_SEVERITY_ID><LoggedBy></LoggedBy><HD_RE_OPEN_DATE_FROM></HD_RE_OPEN_DATE_FROM><HD_RE_OPEN_DATE_TO></HD_RE_OPEN_DATE_TO><HD_RE_CLOSED_DATE_FROM></HD_RE_CLOSED_DATE_FROM><HD_RE_CLOSED_DATE_TO></HD_RE_CLOSED_DATE_TO><Assigned_To></Assigned_To><AssignedDatetime></AssignedDatetime><OfficeID></OfficeID><CUSTOMER_CATEGORY_ID></CUSTOMER_CATEGORY_ID><CALLER_NAME></CALLER_NAME><HD_QUERY_GROUP_ID></HD_QUERY_GROUP_ID><CALL_SUB_GROUP_ID></CALL_SUB_GROUP_ID><CALL_CATEGORY_ID></CALL_CATEGORY_ID><CALL_SUB_CATEGORY_ID></CALL_SUB_CATEGORY_ID><COORDINATOR1></COORDINATOR1><COORDINATOR2></COORDINATOR2><DISPOSITION_ID></DISPOSITION_ID><Aoffice></Aoffice><AgencyAoffice></AgencyAoffice><WO_ID></WO_ID><LastCall></LastCall><EmployeeID></EmployeeID><LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY><LIMITED_TO_AOFFICE></LIMITED_TO_AOFFICE><LIMITED_TO_REGION></LIMITED_TO_REGION><SetID></SetID><HD_DESCRIPTION>0</HD_DESCRIPTION><HD_SOLUTION>0</HD_SOLUTION></HD_DMSEARCH_CALLREQUEST_INPUT>"

            objInputXml.LoadXml(strSEARCH_INPUT)

            'objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyName.Value
            'objInputXml.DocumentElement.SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text
            objInputXml.DocumentElement.SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text
            If (Request.Form("txtAgencyName") <> "" And hdAgencyName.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyName.Value = "") Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = ""
            Else
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyName.Value.Trim()
            End If



            'objInputXml.DocumentElement.SelectSingleNode("OfficeID").InnerText = txtOfficeID.Text
            'objInputXml.DocumentElement.SelectSingleNode("CUSTOMER_CATEGORY_ID").InnerText = ddlCustomerCategory.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("LoggedBy").InnerText = txtLoggedBy.Text
            'objInputXml.DocumentElement.SelectSingleNode("CALLER_NAME").InnerText = txtCallerName.Text
            objInputXml.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = ddlQueryGroup.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_GROUP_ID").InnerText = ddlQuerySubGroup.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("CALL_CATEGORY_ID").InnerText = ddlQueryCategory.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY_ID").InnerText = ddlQuerySubCategory.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("HD_STATUS_ID").InnerText = ddlQueryStatus.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("HD_SEVERITY_ID").InnerText = ddlQueryPriority.SelectedValue
            'objInputXml.DocumentElement.SelectSingleNode("COORDINATOR1").InnerText = ddlCoordinator1.SelectedValue
            ' objInputXml.DocumentElement.SelectSingleNode("COORDINATOR2").InnerText = ddlCoordinator2.SelectedValue
            If (Request.Form("txtDateAssigned") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("AssignedDatetime").InnerText = objeAAMS.GetDateFormat(Request.Form("txtDateAssigned"), "dd/MM/yyyy", "yyyyMMdd", "/")
            End If


            ' objInputXml.DocumentElement.SelectSingleNode("DISPOSITION_ID").InnerText = ddlDisposition.SelectedValue
            If (Request.Form("txtQueryOpenedDateFrom") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("HD_RE_OPEN_DATE_FROM").InnerText = objeAAMS.GetDateFormat(Request.Form("txtQueryOpenedDateFrom"), "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm", "/") ' objeAAMS.GetDateFormat(Request.Form("txtQueryOpenedDateFrom"), "dd/MM/yyyy HH:MM", "MM/dd/yyyy HH:MM", "/")
            End If
            If (Request.Form("txtQueryOpenedDateTo") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("HD_RE_OPEN_DATE_TO").InnerText = objeAAMS.GetDateFormat(Request.Form("txtQueryOpenedDateTo"), "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm", "/") 'objeAAMS.GetDateFormat(Request.Form("txtQueryOpenedDateTo"), "dd/MM/yyyy HH:MM", "MM/dd/yyyy HH:MM", "/")
            End If
            If (Request.Form("txtCloseDateFrom1") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("HD_RE_CLOSED_DATE_FROM").InnerText = objeAAMS.GetDateFormat(Request.Form("txtCloseDateFrom1"), "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm", "/") 'objeAAMS.GetDateFormat(Request.Form("txtCloseDateFrom1"), "dd/MM/yyyy HH:MM", "MM/dd/yyyy HH:MM", "/")
            End If
            If (Request.Form("txtCloseDateTo1") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("HD_RE_CLOSED_DATE_TO").InnerText = objeAAMS.GetDateFormat(Request.Form("txtCloseDateTo1"), "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm", "/") 'objeAAMS.GetDateFormat(Request.Form("txtCloseDateTo1"), "dd/MM/yyyy HH:MM", "MM/dd/yyyy HH:MM", "/")
            End If

            objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = ddlAOffice.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("AgencyAoffice").InnerText = ddlAgencyAOffice.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = txtLTRNo.Text
            objInputXml.DocumentElement.SelectSingleNode("HD_PTR_REF").InnerText = txtPTRNo.Text
            '  If DivDispLastCall.Visible = True Then
            'objInputXml.DocumentElement.SelectSingleNode("LastCall").InnerText = IIf(chkDisplayLastCall.Checked, "True", "false")
            ' Else
            objInputXml.DocumentElement.SelectSingleNode("LastCall").InnerText = "false"
            'End If

            'objInputXml.DocumentElement.SelectSingleNode("WO_ID").InnerText = txtWorkOrderNo.Text

            ' objInputXml.DocumentElement.SelectSingleNode("Address").InnerText = txtAddress.Text
            'objInputXml.DocumentElement.SelectSingleNode("FollowUp").InnerText = drpFollowup.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("Assigned_To").InnerText = drpAssignedTo.SelectedValue


            If Not Session("Security") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = objeAAMS.Limited_To_OwnAgency(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = objeAAMS.Limited_To_Aoffice(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = objeAAMS.Limited_To_Region(Session("Security"))

            End If
            objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0).ToString()

            objInputXml.DocumentElement.SelectSingleNode("SetID").InnerText = 1
            ' Here Back end Method Call

            '  objOutputXml.Load("C:\techrep.xml")
            objOutputXml = objbzDMReportConfiguration.TechnicalReport(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                '   Session("HelpDeskDynamicReport") = objOutputXml.OuterXml
                ' Response.Redirect("../RPSR_ReportShow.aspxobjNodeListDetailsCase=HelpDeskDynamicReport", False)
                ' ExportinExcel(objOutputXml)
                ExportinExcelByXmlNode(objOutputXml)

                '###########################################################
                '@ Now Writing The data in Excel Sheet
                '###########################################################




            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        End If
    End Sub

    Private Sub ExportinExcel(ByVal objResultOutputXml As XmlDocument)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        Dim objbzBIDT As New AAMS.bizProductivity.bzBIDT
        Dim dS As New DataSet
        Dim objXmlReader As XmlNodeReader
        Dim objExWorkSheet As Excel.Worksheet
        Dim objExApplication As New Excel.Application
        Dim objExWorkBooks As Excel.Workbooks
        Dim objExWorkBook As Excel.Workbook
        '  Dim xlWorkBook As Excel.Workbook

        Dim dtLTRDeistinct As System.Data.DataTable

        Dim UserId As Integer = 0
        Try
            objOutputXml.LoadXml(objResultOutputXml.OuterXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                objXmlReader = New XmlNodeReader(objOutputXml)
                dS.ReadXml(objXmlReader)
                Dim objTemplatePath As String
                Dim rowCounter As Integer = 0
                Dim rowCounterPosForImage As Integer = 0


                '************ End of  Declaration Section *******************************
                If Not Session("LoginSession") Is Nothing Then
                    UserId = Session("LoginSession").ToString().Split("|")(0)
                Else
                    lblError.Text = "Session is Exipred."
                    Exit Sub
                End If
                If File.Exists(Server.MapPath("~") + "\Template\" + "HDRepTechnicalReport" + UserId.ToString + ".xls") Then
                    File.Delete(Server.MapPath("~") + "\Template\" + "HDRepTechnicalReport" + UserId.ToString + ".xls")
                End If
                objTemplatePath = Server.MapPath("~") + "\Template\HDRepTechnicalReport.xls"



                objExWorkBooks = objExApplication.Workbooks
                objExWorkBook = objExWorkBooks.Open(objTemplatePath)
                objExWorkSheet = objExApplication.ActiveSheet

                rowCounter = 2
                '<HD_TECHREPORT_OUTPUT>
                '     <DETAILS 
                '     Region ='' 
                '     State ='' 
                '     City ='' 
                '     A1Office ='' 
                '     Agency1AOffice =''
                '     AgencyName =''
                '      OfficeId =''
                '      LoggedBy =''
                '      Callername =''
                '      Customercategory =''
                '      QueryGroup =''
                '      QuerySubGroup =''
                '      QueryCategory =''
                '      QuerySubCategory =''
                '      QueryStatus =''
                '      QueryPriority =''
                '      DateassignedTime =''
                '      DateopenFrom =''
                '      DateopenTo =''
                '      ClosedateFrom =''
                '      ClosedateTo =''
                '      PRTNo =''
                '      LTRNo =''
                '      CallAssignedTo =''
                '      Co_ordinator1 =''
                '      Workorderno =''
                '      contacttype =''
                '      Discription =''
                '      Solution =''/>
                '</HD_TECHREPORT_OUTPUT>

                Dim intcolor As Integer = 1

                dtLTRDeistinct = New System.Data.DataTable

                dtLTRDeistinct = dS.Tables("DETAILS").DefaultView.ToTable(True, "LTRNo")
                For Each drDistinctLTRRow As DataRow In dtLTRDeistinct.Rows

                    'objExWorkSheet.Cells.Range().Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver);

                    Dim dLTRrows() As DataRow = dS.Tables("DETAILS").Select("LTRNo='" + drDistinctLTRRow("LTRNo").ToString + "'")

                    objExWorkSheet.Cells(rowCounter, 1) = dLTRrows(0)("Region").ToString()
                    objExWorkSheet.Cells(rowCounter, 2) = dLTRrows(0)("State").ToString()
                    objExWorkSheet.Cells(rowCounter, 3) = dLTRrows(0)("City").ToString()
                    objExWorkSheet.Cells(rowCounter, 4) = dLTRrows(0)("A1Office").ToString()
                    objExWorkSheet.Cells(rowCounter, 5) = dLTRrows(0)("Agency1AOffice").ToString()
                    objExWorkSheet.Cells(rowCounter, 6) = dLTRrows(0)("AgencyName").ToString()
                    objExWorkSheet.Cells(rowCounter, 7) = dLTRrows(0)("OfficeId").ToString()
                    objExWorkSheet.Cells(rowCounter, 8) = dLTRrows(0)("LoggedBy").ToString()
                    objExWorkSheet.Cells(rowCounter, 9) = dLTRrows(0)("Callername").ToString()
                    objExWorkSheet.Cells(rowCounter, 10) = dLTRrows(0)("Customercategory").ToString()
                    objExWorkSheet.Cells(rowCounter, 11) = dLTRrows(0)("QueryGroup").ToString()
                    objExWorkSheet.Cells(rowCounter, 12) = dLTRrows(0)("QuerySubGroup").ToString()
                    objExWorkSheet.Cells(rowCounter, 13) = dLTRrows(0)("QueryCategory").ToString()
                    objExWorkSheet.Cells(rowCounter, 14) = dLTRrows(0)("QuerySubCategory").ToString()
                    objExWorkSheet.Cells(rowCounter, 15) = dLTRrows(0)("QueryStatus").ToString()
                    objExWorkSheet.Cells(rowCounter, 16) = dLTRrows(0)("QueryPriority").ToString()
                    objExWorkSheet.Cells(rowCounter, 17) = dLTRrows(0)("DateassignedTime").ToString()
                    objExWorkSheet.Cells(rowCounter, 18) = dLTRrows(0)("DateopenFrom").ToString()
                    objExWorkSheet.Cells(rowCounter, 19) = dLTRrows(0)("DateopenTo").ToString()
                    objExWorkSheet.Cells(rowCounter, 20) = dLTRrows(0)("ClosedateFrom").ToString()
                    objExWorkSheet.Cells(rowCounter, 21) = dLTRrows(0)("ClosedateTo").ToString()
                    objExWorkSheet.Cells(rowCounter, 22) = dLTRrows(0)("PRTNo").ToString()
                    objExWorkSheet.Cells(rowCounter, 23) = dLTRrows(0)("LTRNo").ToString()
                    objExWorkSheet.Cells(rowCounter, 24) = dLTRrows(0)("CallAssignedTo").ToString()
                    objExWorkSheet.Cells(rowCounter, 25) = dLTRrows(0)("Co_ordinator1").ToString()
                    objExWorkSheet.Cells(rowCounter, 26) = dLTRrows(0)("Workorderno").ToString()
                    objExWorkSheet.Cells(rowCounter, 27) = dLTRrows(0)("contacttype").ToString()



                    ' For DEsc
                    Dim RowcounterIncrement As Integer
                    RowcounterIncrement = rowCounter
                    Dim dDescrows() As DataRow = dS.Tables("DETAILS").Select("LTRNo='" + drDistinctLTRRow("LTRNo").ToString + "' and Discription<>''")


                    Dim RowcounterForDesc As Integer
                    RowcounterForDesc = 1
                    For Each dtrow2 As DataRow In dDescrows
                        objExWorkSheet.Cells(RowcounterIncrement, 28) = dtrow2("Discription").ToString() '.Replace(vbCrLf, "  ") ' dtrow2("Discription").ToString().Replace(vbCrLf, "")
                        RowcounterForDesc += 1
                        RowcounterIncrement = RowcounterIncrement + 1
                    Next

                    'End of For DEsc

                    RowcounterIncrement = rowCounter
                    Dim RowcounterForSoln As Integer
                    RowcounterForSoln = 1
                    ' For soln
                    Dim dSolnrows() As DataRow = dS.Tables("DETAILS").Select("LTRNo='" + drDistinctLTRRow("LTRNo").ToString + "' and Solution<>''")

                    For Each dtrow3 As DataRow In dSolnrows
                        objExWorkSheet.Cells(RowcounterIncrement, 29) = dtrow3("Solution").ToString() '.Replace(vbCrLf, "  ")
                        RowcounterForSoln += 1
                        RowcounterIncrement = RowcounterIncrement + 1
                    Next
                    ' end of For soln

                    '@Merging cells for dulicacy record except desc and solution
                    If RowcounterForSoln >= RowcounterForDesc Then

                        ' If rowCounter + 1 < RowcounterIncrement Then
                        If RowcounterForSoln > 2 Then
                            objExWorkSheet.Cells.Range("A" + rowCounter.ToString(), "A" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("B" + rowCounter.ToString(), "B" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("C" + rowCounter.ToString(), "C" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("D" + rowCounter.ToString(), "D" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("E" + rowCounter.ToString(), "E" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()

                            objExWorkSheet.Cells.Range("F" + rowCounter.ToString(), "F" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("G" + rowCounter.ToString(), "G" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("H" + rowCounter.ToString(), "H" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("I" + rowCounter.ToString(), "I" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("J" + rowCounter.ToString(), "J" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("K" + rowCounter.ToString(), "K" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("L" + rowCounter.ToString(), "L" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("M" + rowCounter.ToString(), "M" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("N" + rowCounter.ToString(), "N" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("O" + rowCounter.ToString(), "O" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("P" + rowCounter.ToString(), "P" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("Q" + rowCounter.ToString(), "Q" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("R" + rowCounter.ToString(), "R" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("S" + rowCounter.ToString(), "S" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("T" + rowCounter.ToString(), "T" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("U" + rowCounter.ToString(), "U" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("V" + rowCounter.ToString(), "V" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("W" + rowCounter.ToString(), "W" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("X" + rowCounter.ToString(), "X" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("Y" + rowCounter.ToString(), "Y" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("Z" + rowCounter.ToString(), "Z" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("AA" + rowCounter.ToString(), "AA" + (rowCounter + RowcounterForSoln - 2).ToString()).Merge()

                            '    objExWorkSheet.Cells.Range("A" + rowCounter.ToString(), "A" + (rowCounter + RowcounterForSoln - 2).ToString()).Font.Shadow

                            If intcolor = 1 Then
                                objExWorkSheet.Cells.Range("A" + rowCounter.ToString(), "AC" + (rowCounter + RowcounterForSoln - 2).ToString()).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
                                intcolor = 2
                            Else
                                intcolor = 1
                                objExWorkSheet.Cells.Range("A" + rowCounter.ToString(), "AC" + (rowCounter + RowcounterForSoln - 2).ToString()).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
                            End If

                        Else

                            If intcolor = 1 Then
                                intcolor = 2
                                objExWorkSheet.Cells.Range("A" + rowCounter.ToString(), "AC" + (rowCounter.ToString)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
                            Else
                                intcolor = 1
                                objExWorkSheet.Cells.Range("A" + rowCounter.ToString(), "AC" + (rowCounter.ToString)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
                            End If


                        End If


                        If RowcounterForSoln = 1 Then
                            rowCounter = rowCounter + RowcounterForSoln
                        Else
                            rowCounter = rowCounter + RowcounterForSoln - 1
                        End If

                    Else
                        'If rowCounter + 1 < RowcounterIncrement Then
                        If RowcounterForDesc > 2 Then
                            objExWorkSheet.Cells.Range("A" + rowCounter.ToString(), "A" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("B" + rowCounter.ToString(), "B" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("C" + rowCounter.ToString(), "C" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("D" + rowCounter.ToString(), "D" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("E" + rowCounter.ToString(), "E" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()

                            objExWorkSheet.Cells.Range("F" + rowCounter.ToString(), "F" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("G" + rowCounter.ToString(), "G" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("H" + rowCounter.ToString(), "H" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("I" + rowCounter.ToString(), "I" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("J" + rowCounter.ToString(), "J" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("K" + rowCounter.ToString(), "K" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("L" + rowCounter.ToString(), "L" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("M" + rowCounter.ToString(), "M" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("N" + rowCounter.ToString(), "N" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("O" + rowCounter.ToString(), "O" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("P" + rowCounter.ToString(), "P" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("Q" + rowCounter.ToString(), "Q" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("R" + rowCounter.ToString(), "R" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("S" + rowCounter.ToString(), "S" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("T" + rowCounter.ToString(), "T" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("U" + rowCounter.ToString(), "U" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("V" + rowCounter.ToString(), "V" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("W" + rowCounter.ToString(), "W" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("X" + rowCounter.ToString(), "X" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("Y" + rowCounter.ToString(), "Y" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("Z" + rowCounter.ToString(), "Z" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()
                            objExWorkSheet.Cells.Range("AA" + rowCounter.ToString(), "AA" + (rowCounter + RowcounterForDesc - 2).ToString()).Merge()

                            If intcolor = 1 Then
                                objExWorkSheet.Cells.Range("A" + rowCounter.ToString(), "AC" + (rowCounter + RowcounterForDesc - 2).ToString()).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
                                intcolor = 2
                            Else
                                intcolor = 1
                                objExWorkSheet.Cells.Range("A" + rowCounter.ToString(), "AC" + (rowCounter + RowcounterForDesc - 2).ToString()).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
                            End If

                        Else
                            If intcolor = 1 Then
                                objExWorkSheet.Cells.Range("A" + rowCounter.ToString(), "AC" + (rowCounter.ToString)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
                                intcolor = 2
                            Else
                                intcolor = 1
                                objExWorkSheet.Cells.Range("A" + rowCounter.ToString(), "AC" + (rowCounter.ToString)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
                            End If


                        End If
                        If RowcounterForDesc = 1 Then
                            rowCounter = rowCounter + RowcounterForDesc
                        Else
                            rowCounter = rowCounter + RowcounterForDesc - 1
                        End If
                        'rowCounter = rowCounter + RowcounterForDesc - 1 'RowcounterIncrement
                    End If


                    '@ End of  Merging cells for dulicacy record except desc and solution
                    ' rowCounter = rowCounter + 1
                Next

                ' objExWorkSheet.Range("A3" + rowCounter.ToString(), "AC" + (rowCounter.ToString)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightPink)
                objExWorkSheet.Range("A1", "AA" + (rowCounter.ToString)).Columns.AutoFit()
                ' objExWorkSheet.Columns.AutoFit()
                '    objExWorkSheet.Columns.WrapText = True

                '   objExWorkSheet.Cells()


                objTemplatePath = Server.MapPath("~") + "\Template\" + "HDRepTechnicalReport" + UserId.ToString + ".xls"
                objExWorkBook.SaveAs(objTemplatePath)
                objExWorkBook.Close()
                objExWorkBooks.Close()
                objExApplication.Quit()

                Dim filePath As String = objTemplatePath
                Dim targetFile As System.IO.FileInfo = New System.IO.FileInfo(filePath)
                If targetFile.Exists Then
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name)
                    Response.AddHeader("Content-Length", targetFile.Length.ToString)
                    Response.ContentType = "excel"
                    Response.WriteFile(targetFile.FullName)
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
            'objExApplication.Workbooks.Close()
        Finally
            'objInputXml = Nothing
            'objOutputXml = Nothing
            'System.Runtime.InteropServices.Marshal.ReleaseComObject(objExApplication)
            'objExWorkSheet = Nothing
            'objExApplication = Nothing
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
            Try
                objInputXml = Nothing
                objOutputXml = Nothing
                If objExWorkSheet IsNot Nothing Then
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objExWorkSheet)
                End If

                If objExWorkBook IsNot Nothing Then
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objExWorkBook)
                End If
                If objExWorkBooks IsNot Nothing Then
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objExWorkBooks)
                End If
                If objExApplication IsNot Nothing Then
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objExApplication)
                End If
                'System.Runtime.InteropServices.Marshal.ReleaseComObject(objExApplication)
                objExWorkSheet = Nothing
                objExWorkBook = Nothing
                objExWorkBooks = Nothing
                objExApplication = Nothing
                GC.Collect()
                'GC.WaitForPendingFinalizers()
            Catch ex As Exception
            End Try
        End Try

    End Sub

    Protected Sub ddlQueryGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlQueryGroup.SelectedIndexChanged
        Try
            loadSubGroup()
            loadCategory()
            loadSubCategory()
            loadPriority()
            loadQueryStatus()
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

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect("HDRPT_TechnicalReport.aspx", False)
        Catch ex As Exception
        End Try
    End Sub


    Private Sub printTechReport()
        lblError.Text = ""
        Dim objInputXml, objOutputXml As New XmlDocument
        '        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzDMReportConfiguration As New AAMS.bizETrackerHelpDesk.bzDMReportConfiguration
        If Session("LoginSession") IsNot Nothing Then
            Dim strSEARCH_INPUT As String = "<HD_DMSEARCH_CALLREQUEST_INPUT><LCODE></LCODE><HD_RE_ID></HD_RE_ID><AgencyName></AgencyName><Address></Address><HD_PTR_REF></HD_PTR_REF><HD_STATUS_ID></HD_STATUS_ID><FollowUp></FollowUp><HD_SEVERITY_ID></HD_SEVERITY_ID><LoggedBy></LoggedBy><HD_RE_OPEN_DATE_FROM></HD_RE_OPEN_DATE_FROM><HD_RE_OPEN_DATE_TO></HD_RE_OPEN_DATE_TO><HD_RE_CLOSED_DATE_FROM></HD_RE_CLOSED_DATE_FROM><HD_RE_CLOSED_DATE_TO></HD_RE_CLOSED_DATE_TO><Assigned_To></Assigned_To><AssignedDatetime></AssignedDatetime><OfficeID></OfficeID><CUSTOMER_CATEGORY_ID></CUSTOMER_CATEGORY_ID><CALLER_NAME></CALLER_NAME><HD_QUERY_GROUP_ID></HD_QUERY_GROUP_ID><CALL_SUB_GROUP_ID></CALL_SUB_GROUP_ID><CALL_CATEGORY_ID></CALL_CATEGORY_ID><CALL_SUB_CATEGORY_ID></CALL_SUB_CATEGORY_ID><COORDINATOR1></COORDINATOR1><COORDINATOR2></COORDINATOR2><DISPOSITION_ID></DISPOSITION_ID><Aoffice></Aoffice><AgencyAoffice></AgencyAoffice><WO_ID></WO_ID><LastCall></LastCall><EmployeeID></EmployeeID><LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY><LIMITED_TO_AOFFICE></LIMITED_TO_AOFFICE><LIMITED_TO_REGION></LIMITED_TO_REGION><SetID></SetID><HD_DESCRIPTION>0</HD_DESCRIPTION><HD_SOLUTION>0</HD_SOLUTION></HD_DMSEARCH_CALLREQUEST_INPUT>"

            objInputXml.LoadXml(strSEARCH_INPUT)

            'objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyName.Value
            'objInputXml.DocumentElement.SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text
            objInputXml.DocumentElement.SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text
            If (Request.Form("txtAgencyName") <> "" And hdAgencyName.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyName.Value = "") Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = ""
            Else
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyName.Value.Trim()
            End If



            'objInputXml.DocumentElement.SelectSingleNode("OfficeID").InnerText = txtOfficeID.Text
            'objInputXml.DocumentElement.SelectSingleNode("CUSTOMER_CATEGORY_ID").InnerText = ddlCustomerCategory.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("LoggedBy").InnerText = txtLoggedBy.Text
            'objInputXml.DocumentElement.SelectSingleNode("CALLER_NAME").InnerText = txtCallerName.Text
            objInputXml.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = ddlQueryGroup.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_GROUP_ID").InnerText = ddlQuerySubGroup.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("CALL_CATEGORY_ID").InnerText = ddlQueryCategory.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY_ID").InnerText = ddlQuerySubCategory.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("HD_STATUS_ID").InnerText = ddlQueryStatus.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("HD_SEVERITY_ID").InnerText = ddlQueryPriority.SelectedValue
            'objInputXml.DocumentElement.SelectSingleNode("COORDINATOR1").InnerText = ddlCoordinator1.SelectedValue
            ' objInputXml.DocumentElement.SelectSingleNode("COORDINATOR2").InnerText = ddlCoordinator2.SelectedValue
            If (Request.Form("txtDateAssigned") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("AssignedDatetime").InnerText = objeAAMS.GetDateFormat(Request.Form("txtDateAssigned"), "dd/MM/yyyy", "yyyyMMdd", "/")
            End If


            ' objInputXml.DocumentElement.SelectSingleNode("DISPOSITION_ID").InnerText = ddlDisposition.SelectedValue
            If (Request.Form("txtQueryOpenedDateFrom") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("HD_RE_OPEN_DATE_FROM").InnerText = objeAAMS.GetDateFormat(Request.Form("txtQueryOpenedDateFrom"), "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm", "/") ' objeAAMS.GetDateFormat(Request.Form("txtQueryOpenedDateFrom"), "dd/MM/yyyy HH:MM", "MM/dd/yyyy HH:MM", "/")
            End If
            If (Request.Form("txtQueryOpenedDateTo") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("HD_RE_OPEN_DATE_TO").InnerText = objeAAMS.GetDateFormat(Request.Form("txtQueryOpenedDateTo"), "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm", "/") 'objeAAMS.GetDateFormat(Request.Form("txtQueryOpenedDateTo"), "dd/MM/yyyy HH:MM", "MM/dd/yyyy HH:MM", "/")
            End If
            If (Request.Form("txtCloseDateFrom1") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("HD_RE_CLOSED_DATE_FROM").InnerText = objeAAMS.GetDateFormat(Request.Form("txtCloseDateFrom1"), "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm", "/") 'objeAAMS.GetDateFormat(Request.Form("txtCloseDateFrom1"), "dd/MM/yyyy HH:MM", "MM/dd/yyyy HH:MM", "/")
            End If
            If (Request.Form("txtCloseDateTo1") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("HD_RE_CLOSED_DATE_TO").InnerText = objeAAMS.GetDateFormat(Request.Form("txtCloseDateTo1"), "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm", "/") 'objeAAMS.GetDateFormat(Request.Form("txtCloseDateTo1"), "dd/MM/yyyy HH:MM", "MM/dd/yyyy HH:MM", "/")
            End If

            objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = ddlAOffice.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("AgencyAoffice").InnerText = ddlAgencyAOffice.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = txtLTRNo.Text
            objInputXml.DocumentElement.SelectSingleNode("HD_PTR_REF").InnerText = txtPTRNo.Text
            '  If DivDispLastCall.Visible = True Then
            'objInputXml.DocumentElement.SelectSingleNode("LastCall").InnerText = IIf(chkDisplayLastCall.Checked, "True", "false")
            ' Else
            objInputXml.DocumentElement.SelectSingleNode("LastCall").InnerText = "false"
            'End If

            'objInputXml.DocumentElement.SelectSingleNode("WO_ID").InnerText = txtWorkOrderNo.Text

            ' objInputXml.DocumentElement.SelectSingleNode("Address").InnerText = txtAddress.Text
            'objInputXml.DocumentElement.SelectSingleNode("FollowUp").InnerText = drpFollowup.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("Assigned_To").InnerText = drpAssignedTo.SelectedValue


            If Not Session("Security") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = objeAAMS.Limited_To_OwnAgency(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = objeAAMS.Limited_To_Aoffice(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = objeAAMS.Limited_To_Region(Session("Security"))

            End If
            objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0).ToString()

            objInputXml.DocumentElement.SelectSingleNode("SetID").InnerText = 1
            ' Here Back end Method Call
            objOutputXml = objbzDMReportConfiguration.TechnicalReport(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Session("ETTechReport") = objOutputXml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=ETTechReport", False)
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        End If
    End Sub

    Protected Sub BtnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Try
            printTechReport()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub ExportinExcelByXmlNode(ByVal objResultOutputXml As XmlDocument)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        Dim objbzBIDT As New AAMS.bizProductivity.bzBIDT
        Dim objExWorkSheet As Excel.Worksheet
        Dim objExApplication As New Excel.Application
        Dim objExWorkBooks As Excel.Workbooks
        Dim objExWorkBook As Excel.Workbook
        Dim objNodeListDetails, objNodeListDesc, objNodeListSol As XmlNodeList
        Dim objNodeDetails As XmlNode

        Dim UserId As Integer = 0
        Try
            objOutputXml.LoadXml(objResultOutputXml.OuterXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                Dim objTemplatePath As String
                Dim rowCounter As Integer = 0
                Dim rowCounterPosForImage As Integer = 0
                Dim intCountRow As Int32 = 0
                Dim intCount As Int32 = 0
                Dim RowStartCounterForMerging As Integer
                Dim intcolor As Integer = 1

                '************ End of  Declaration Section *******************************
                If Not Session("LoginSession") Is Nothing Then
                    UserId = Session("LoginSession").ToString().Split("|")(0)
                Else
                    lblError.Text = "Session is Exipred."
                    Exit Sub
                End If
                If File.Exists(Server.MapPath("~") + "\Template\" + "HDRepTechnicalReport" + UserId.ToString + ".xls") Then
                    File.Delete(Server.MapPath("~") + "\Template\" + "HDRepTechnicalReport" + UserId.ToString + ".xls")
                End If
                objTemplatePath = Server.MapPath("~") + "\Template\HDRepTechnicalReport.xls"
                objExWorkBooks = objExApplication.Workbooks
                objExWorkBook = objExWorkBooks.Open(objTemplatePath)
                objExWorkSheet = objExApplication.ActiveSheet
                rowCounter = 2
                '<HD_TECHREPORT_OUTPUT>
                '     <DETAILS 
                '     Region ='' 
                '     State ='' 
                '     City ='' 
                '     A1Office ='' 
                '     Agency1AOffice =''
                '     AgencyName =''
                '      OfficeId =''
                '      LoggedBy =''
                '      Callername =''
                '      Customercategory =''
                '      QueryGroup =''
                '      QuerySubGroup =''
                '      QueryCategory =''
                '      QuerySubCategory =''
                '      QueryStatus =''
                '      QueryPriority =''
                '      DateassignedTime =''
                '      DateopenFrom =''
                '      DateopenTo =''
                '      ClosedateFrom =''
                '      ClosedateTo =''
                '      PRTNo =''
                '      LTRNo =''
                '      CallAssignedTo =''
                '      Co_ordinator1 =''
                '      Workorderno =''
                '      contacttype =''
                '<Discription Value=''/>
                '<Discription Value=''/>
                '<Solution Value=''/>
                '<Solution Value=''/>

                '</DETAILS>
                '     <DETAILS 
                '     Region ='' 
                '     State ='' 
                '     City ='' 
                '     A1Office ='' 
                '     Agency1AOffice =''
                '     AgencyName =''
                '      OfficeId =''
                '      LoggedBy =''
                '      Callername =''
                '      Customercategory =''
                '      QueryGroup =''
                '      QuerySubGroup =''
                '      QueryCategory =''
                '      QuerySubCategory =''
                '      QueryStatus =''
                '      QueryPriority =''
                '      DateassignedTime =''
                '      DateopenFrom =''
                '      DateopenTo =''
                '      ClosedateFrom =''
                '      ClosedateTo =''
                '      PRTNo =''
                '      LTRNo =''
                '      CallAssignedTo =''
                '      Co_ordinator1 =''
                '      Workorderno =''
                '      contacttype =''
                '<Discription Value=''/>
                '<Discription Value=''/>
                '<Solution Value=''/>
                '<Solution Value=''/>

                '</DETAILS>
                '</HD_TECHREPORT_OUTPUT>            
                'objNodeListDetails = objOutputXml.SelectNodes("//DETAILS")
                objNodeListDetails = objOutputXml.DocumentElement.SelectNodes("DETAILS")

                If objNodeListDetails.Count = 0 Then
                    lblError.Text = "No Record found!"
                    Exit Sub
                End If

                For Each objNodeDetails In objNodeListDetails

                    objExWorkSheet.Cells(rowCounter, 1) = objNodeDetails.Attributes("Region").InnerText 'dLTRrows(0)("Region").ToString()
                    objExWorkSheet.Cells(rowCounter, 2) = objNodeDetails.Attributes("State").InnerText 'dLTRrows(0)("State").ToString()
                    objExWorkSheet.Cells(rowCounter, 3) = objNodeDetails.Attributes("City").InnerText ' dLTRrows(0)("City").ToString()
                    objExWorkSheet.Cells(rowCounter, 4) = objNodeDetails.Attributes("A1Office").InnerText 'dLTRrows(0)("A1Office").ToString()
                    objExWorkSheet.Cells(rowCounter, 5) = objNodeDetails.Attributes("Agency1AOffice").InnerText ' dLTRrows(0)("Agency1AOffice").ToString()
                    objExWorkSheet.Cells(rowCounter, 6) = objNodeDetails.Attributes("AgencyName").InnerText 'dLTRrows(0)("AgencyName").ToString()
                    objExWorkSheet.Cells(rowCounter, 7) = objNodeDetails.Attributes("OfficeId").InnerText ' dLTRrows(0)("OfficeId").ToString()
                    objExWorkSheet.Cells(rowCounter, 8) = objNodeDetails.Attributes("LoggedBy").InnerText 'dLTRrows(0)("LoggedBy").ToString()
                    objExWorkSheet.Cells(rowCounter, 9) = objNodeDetails.Attributes("Callername").InnerText 'dLTRrows(0)("Callername").ToString()
                    objExWorkSheet.Cells(rowCounter, 10) = objNodeDetails.Attributes("Customercategory").InnerText 'dLTRrows(0)("Customercategory").ToString()
                    objExWorkSheet.Cells(rowCounter, 11) = objNodeDetails.Attributes("QueryGroup").InnerText 'dLTRrows(0)("QueryGroup").ToString()
                    objExWorkSheet.Cells(rowCounter, 12) = objNodeDetails.Attributes("QuerySubGroup").InnerText 'dLTRrows(0)("QuerySubGroup").ToString()
                    objExWorkSheet.Cells(rowCounter, 13) = objNodeDetails.Attributes("QueryCategory").InnerText 'dLTRrows(0)("QueryCategory").ToString()
                    objExWorkSheet.Cells(rowCounter, 14) = objNodeDetails.Attributes("QuerySubCategory").InnerText 'dLTRrows(0)("QuerySubCategory").ToString()
                    objExWorkSheet.Cells(rowCounter, 15) = objNodeDetails.Attributes("QueryStatus").InnerText 'dLTRrows(0)("QueryStatus").ToString()
                    objExWorkSheet.Cells(rowCounter, 16) = objNodeDetails.Attributes("QueryPriority").InnerText 'dLTRrows(0)("QueryPriority").ToString()
                    objExWorkSheet.Cells(rowCounter, 17) = objNodeDetails.Attributes("DateassignedTime").InnerText 'dLTRrows(0)("DateassignedTime").ToString()
                    objExWorkSheet.Cells(rowCounter, 18) = objNodeDetails.Attributes("DateopenFrom").InnerText 'dLTRrows(0)("DateopenFrom").ToString()
                    objExWorkSheet.Cells(rowCounter, 19) = objNodeDetails.Attributes("DateopenTo").InnerText 'dLTRrows(0)("DateopenTo").ToString()
                    objExWorkSheet.Cells(rowCounter, 20) = objNodeDetails.Attributes("ClosedateFrom").InnerText 'dLTRrows(0)("ClosedateFrom").ToString()
                    objExWorkSheet.Cells(rowCounter, 21) = objNodeDetails.Attributes("ClosedateTo").InnerText 'dLTRrows(0)("ClosedateTo").ToString()
                    objExWorkSheet.Cells(rowCounter, 22) = objNodeDetails.Attributes("PRTNo").InnerText 'dLTRrows(0)("PRTNo").ToString()
                    objExWorkSheet.Cells(rowCounter, 23) = objNodeDetails.Attributes("LTRNo").InnerText 'dLTRrows(0)("LTRNo").ToString()
                    objExWorkSheet.Cells(rowCounter, 24) = objNodeDetails.Attributes("CallAssignedTo").InnerText 'dLTRrows(0)("CallAssignedTo").ToString()
                    objExWorkSheet.Cells(rowCounter, 25) = objNodeDetails.Attributes("Co_ordinator1").InnerText 'dLTRrows(0)("Co_ordinator1").ToString()
                    objExWorkSheet.Cells(rowCounter, 26) = objNodeDetails.Attributes("Workorderno").InnerText 'dLTRrows(0)("Workorderno").ToString()
                    objExWorkSheet.Cells(rowCounter, 27) = objNodeDetails.Attributes("contacttype").InnerText 'dLTRrows(0)("contacttype").ToString()

                    objExWorkSheet.Cells(rowCounter, 28) = objNodeDetails.Attributes("Discription").InnerText 'dLTRrows(0)("contacttype").ToString()
                    objExWorkSheet.Cells(rowCounter, 29) = objNodeDetails.Attributes("Solution").InnerText 'dLTRrows(0)("contacttype").ToString()

                    objNodeListDesc = objNodeDetails.SelectNodes("Discription[@Value!='']")
                    objNodeListSol = objNodeDetails.SelectNodes("Solution[@Value!='']")

                    RowStartCounterForMerging = rowCounter

                    If (objNodeListSol.Count >= objNodeListDesc.Count) Then
                        intCountRow = objNodeListSol.Count
                    Else
                        intCountRow = objNodeListDesc.Count
                    End If
                    For intCount = 0 To intCountRow - 1   '@ here changing
                        If (intCount < objNodeListDesc.Count) Then
                            objExWorkSheet.Cells(rowCounter, 28) = objNodeListDesc(intCount).Attributes("Value").InnerText
                        End If
                        If (intCount < objNodeListSol.Count) Then
                            objExWorkSheet.Cells(rowCounter, 29) = objNodeListSol(intCount).Attributes("Value").InnerText
                        End If
                        rowCounter += 1
                    Next

                    If intCountRow = 0 Then   ' If Not any solution/description is exist
                        rowCounter += 1
                    End If

                    'Code for Merging
                    If intCountRow > 1 Then
                        objExWorkSheet.Cells.Range("A" + RowStartCounterForMerging.ToString(), "A" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("B" + RowStartCounterForMerging.ToString(), "B" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("C" + RowStartCounterForMerging.ToString(), "C" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("D" + RowStartCounterForMerging.ToString(), "D" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("E" + RowStartCounterForMerging.ToString(), "E" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()

                        objExWorkSheet.Cells.Range("F" + RowStartCounterForMerging.ToString(), "F" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("G" + RowStartCounterForMerging.ToString(), "G" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("H" + RowStartCounterForMerging.ToString(), "H" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("I" + RowStartCounterForMerging.ToString(), "I" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("J" + RowStartCounterForMerging.ToString(), "J" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("K" + RowStartCounterForMerging.ToString(), "K" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("L" + RowStartCounterForMerging.ToString(), "L" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("M" + RowStartCounterForMerging.ToString(), "M" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("N" + RowStartCounterForMerging.ToString(), "N" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("O" + RowStartCounterForMerging.ToString(), "O" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("P" + RowStartCounterForMerging.ToString(), "P" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("Q" + RowStartCounterForMerging.ToString(), "Q" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("R" + RowStartCounterForMerging.ToString(), "R" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("S" + RowStartCounterForMerging.ToString(), "S" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("T" + RowStartCounterForMerging.ToString(), "T" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("U" + RowStartCounterForMerging.ToString(), "U" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("V" + RowStartCounterForMerging.ToString(), "V" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("W" + RowStartCounterForMerging.ToString(), "W" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("X" + RowStartCounterForMerging.ToString(), "X" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("Y" + RowStartCounterForMerging.ToString(), "Y" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("Z" + RowStartCounterForMerging.ToString(), "Z" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()
                        objExWorkSheet.Cells.Range("AA" + RowStartCounterForMerging.ToString(), "AA" + (RowStartCounterForMerging + intCountRow - 1).ToString()).Merge()

                        '@ Changing Alternate color
                        If intcolor = 1 Then
                            intcolor = 2
                            objExWorkSheet.Cells.Range("A" + RowStartCounterForMerging.ToString(), "AC" + ((RowStartCounterForMerging + intCountRow - 1).ToString)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
                        Else
                            intcolor = 1
                            objExWorkSheet.Cells.Range("A" + RowStartCounterForMerging.ToString(), "AC" + ((RowStartCounterForMerging + intCountRow - 1).ToString)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
                        End If
                    Else
                        '@ Changing Alternate color
                        If intcolor = 1 Then
                            intcolor = 2
                            objExWorkSheet.Cells.Range("A" + RowStartCounterForMerging.ToString(), "AC" + (RowStartCounterForMerging.ToString)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
                        Else
                            intcolor = 1
                            objExWorkSheet.Cells.Range("A" + RowStartCounterForMerging.ToString(), "AC" + (RowStartCounterForMerging.ToString)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
                        End If
                    End If

                Next

                objExWorkSheet.Range("A1", "AA" + (rowCounter.ToString)).Columns.AutoFit()

                objTemplatePath = Server.MapPath("~") + "\Template\" + "HDRepTechnicalReport" + UserId.ToString + ".xls"
                objExWorkBook.SaveAs(objTemplatePath)
                objExWorkBook.Close()
                objExWorkBooks.Close()
                objExApplication.Quit()

                Dim filePath As String = objTemplatePath
                Dim targetFile As System.IO.FileInfo = New System.IO.FileInfo(filePath)
                If targetFile.Exists Then
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name)
                    Response.AddHeader("Content-Length", targetFile.Length.ToString)
                    Response.ContentType = "excel"
                    Response.WriteFile(targetFile.FullName)
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
            'objExApplication.Workbooks.Close()
        Finally
            'objInputXml = Nothing
            'objOutputXml = Nothing
            'System.Runtime.InteropServices.Marshal.ReleaseComObject(objExApplication)
            'objExWorkSheet = Nothing
            'objExApplication = Nothing
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
            Try
                objInputXml = Nothing
                objOutputXml = Nothing
                If objExWorkSheet IsNot Nothing Then
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objExWorkSheet)
                End If

                If objExWorkBook IsNot Nothing Then
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objExWorkBook)
                End If
                If objExWorkBooks IsNot Nothing Then
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objExWorkBooks)
                End If
                If objExApplication IsNot Nothing Then
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objExApplication)
                End If
                'System.Runtime.InteropServices.Marshal.ReleaseComObject(objExApplication)
                objExWorkSheet = Nothing
                objExWorkBook = Nothing
                objExWorkBooks = Nothing
                objExApplication = Nothing
                GC.Collect()
                'GC.WaitForPendingFinalizers()
            Catch ex As Exception
            End Try
        End Try

    End Sub






End Class
