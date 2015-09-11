'########################################################################
'############   Page Name -- Popup_PUSR_HelpDeskSearchConfigure   #######
'############   Date 06-April 2010  ##################################
'############   Developed By Abhishek  ##################################
'########################################################################
Partial Class Popup_PUSR_HelpDeskSearchConfigure
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim strBuilder As StringBuilder
    Dim objeAAMSMessage As New eAAMSMessage
  

#Region "Code for Filter "
    'Code Added by M. K
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        'If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
        '    If Response.ContentType = "text/html" Then
        '        Response.Filter = New TrimStream(Response.Filter)
        '    End If
        'End If
    End Sub
    'Code Added by M. K
#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblError.Text = ""
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        objeAAMS.ExpirePageCache()


        btnSave.Attributes.Add("onClick", "return FormValidation()")

        ' This code is usedc for checking session handler according to user login.
        If Session("Security") Is Nothing Then
            '  ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            lblError.Text = "Session is expired"
            gvHelpDeskSearchCofig.DataSource = Nothing
            gvHelpDeskSearchCofig.DataBind()
            Exit Sub
        End If

        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Group']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Group']").Attributes("Value").Value)

                'If strBuilder(0) = "0" Then
                '    btnSearch.Enabled = False
                'End If
                'If strBuilder(1) = "0" Then
                '    btnSave.Enabled = False
                'End If
            End If
        Else
            strBuilder = objeAAMS.SecurityCheck(31)
        End If
        If (Not IsPostBack) Then
            hdAction.Value = ""
            If Session("LoginSession") IsNot Nothing Then
                GetSetId()
                If Request.QueryString("CurrentSet") IsNot Nothing Then
                    ' drpSelectSet.SelectedValue
                    Dim li As New ListItem
                    li = drpSelectSet.Items.FindByValue(Request.QueryString("CurrentSet"))
                    If li IsNot Nothing Then
                        drpSelectSet.SelectedValue = li.Value
                    End If
                End If
                LoadSearchConfigure(Session("LoginSession").ToString().Split("|")(0).ToString)
            End If

        End If

    End Sub
    Sub LoadSearchConfigure(ByVal strEmpId As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        'Dim RowNo As Long
        Try
            If drpSelectSet.SelectedValue <> "" Then


                Dim objbzDMReportConfiguration As New AAMS.bizHelpDesk.bzDMReportConfiguration
                objInputXml.LoadXml("<HD_VIEWDMSFIELDS_INPUT><EmployeeID /><CNFGFLAG/> <SetID/></HD_VIEWDMSFIELDS_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = strEmpId
                objInputXml.DocumentElement.SelectSingleNode("CNFGFLAG").InnerText = "S"

                If drpSelectSet.Items.Count = 0 Then
                    objInputXml.DocumentElement.SelectSingleNode("SetID").InnerText = "" ' drpSelectSet.SelectedValue
                Else
                    If drpSelectSet.SelectedValue = "" Then
                        objInputXml.DocumentElement.SelectSingleNode("SetID").InnerText = "" ' drpSelectSet.SelectedValue
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("SetID").InnerText = drpSelectSet.SelectedValue
                    End If

                End If

                'Here Back end Method Call          
                objOutputXml = objbzDMReportConfiguration.View(objInputXml)
                'objOutputXml.LoadXml("<HD_GETDMSEARCHCONFIGURE_OUTPUT><FIELDDETAILS DM_FIELD_ID='AGENCY_NAME'  DM_FIELD_NAME='Agency Name' SELECTED='True' /><FIELDDETAILS DM_FIELD_ID='OFFICE_ID'   DM_FIELD_NAME='Office ID' SELECTED='False' /><FIELDDETAILS DM_FIELD_ID='CUST_CATG'    DM_FIELD_NAME='Customer Category' SELECTED='True' /><FIELDDETAILS DM_FIELD_ID='CALLER_NAME'    DM_FIELD_NAME='Caller Name'  SELECTED='True' /><Errors Status='FALSE'><Error Code='' Description='' /> </Errors></HD_GETDMSEARCHCONFIGURE_OUTPUT>")
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)

                    gvHelpDeskSearchCofig.DataSource = ds.Tables("FIELDDETAILS")
                    gvHelpDeskSearchCofig.DataBind()
                    CallForDefaultValue()

                    '@ Calling For EnableorDisableControls
                    Dim Rowno As Integer
                    For Rowno = 0 To gvHelpDeskSearchCofig.Rows.Count - 1
                        'objAptchildNodeClone.Attributes("Aoffice").InnerText = dbgrdManageSecurityRegion.Rows(Rowno).Cells(0).Text
                        Dim chk As CheckBox = CType(gvHelpDeskSearchCofig.Rows(Rowno).FindControl("chkSelected"), CheckBox)

                        Dim strFieldName As String = ""
                        strFieldName = gvHelpDeskSearchCofig.Rows(Rowno).Cells(0).Text.Trim
                        If chk.Checked = True And chk.Enabled = True Then
                            EnableControls(strFieldName, Rowno)
                        ElseIf chk.Checked = False Then
                            DisableControls(strFieldName, Rowno)
                        End If
                    Next Rowno
                    '@ Calling For EnableorDisableControls

                Else
                    gvHelpDeskSearchCofig.DataSource = Nothing
                    gvHelpDeskSearchCofig.DataBind()
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Function GetValue(ByVal value As Object) As Boolean
        Dim blnvalue As Boolean = False
        If value IsNot Nothing Then
            If value.ToString = "True" Then
                blnvalue = True
            Else
                blnvalue = False
            End If
        Else
            blnvalue = False
        End If
        Return blnvalue
    End Function

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If (IsValid) Then
            Try
                Dim objInputXml, objOutputXml As New XmlDocument
                Dim objbzDMReportConfiguration As New AAMS.bizHelpDesk.bzDMReportConfiguration

                Dim ds As New DataSet
                Dim Rowno As Integer
                Dim objParentNode, objChildNode, objChildNodeClone As XmlNode
                Dim strAoffice As String = ""
                If Not Session("LoginSession") Is Nothing Then
                    objInputXml.LoadXml("<HD_UPDATEDMSEARCHCONFIGURE_INPUT><FIELDDETAILS DM_FIELD_ID='' EmployeeID='' SetID='' Value='' /></HD_UPDATEDMSEARCHCONFIGURE_INPUT>")


                    'Reading and Appending records into the Input XMLDocument
                    objParentNode = objInputXml.DocumentElement
                    objChildNode = objInputXml.DocumentElement.SelectSingleNode("FIELDDETAILS")
                    objChildNodeClone = objChildNode.CloneNode(True)
                    objParentNode.RemoveChild(objChildNode)
                    For Rowno = 0 To gvHelpDeskSearchCofig.Rows.Count - 1


                       

                        'objAptchildNodeClone.Attributes("Aoffice").InnerText = dbgrdManageSecurityRegion.Rows(Rowno).Cells(0).Text
                        Dim chk As CheckBox = CType(gvHelpDeskSearchCofig.Rows(Rowno).FindControl("chkSelected"), CheckBox)
                        Dim hdDM_FIELD_ID As HiddenField = CType(gvHelpDeskSearchCofig.Rows(Rowno).FindControl("DM_FIELD_ID"), HiddenField)
                        If (chk.Checked = True) Then
                            ' If (chk.Checked = True And chk.Enabled = True) Then

                            Dim strFieldName As String = ""
                            strFieldName = gvHelpDeskSearchCofig.Rows(Rowno).Cells(0).Text.Trim

                            objChildNodeClone.Attributes("Value").Value = SetValue(strFieldName, Rowno)

                            objChildNodeClone.Attributes("DM_FIELD_ID").Value = hdDM_FIELD_ID.Value
                            objChildNodeClone.Attributes("EmployeeID").Value = Session("LoginSession").ToString().Split("|")(0).ToString
                            If hdAction.Value = "I" Then
                                If MaxSetCountFunction() = 0 Then
                                    objChildNodeClone.Attributes("SetID").Value = 1
                                Else
                                    objChildNodeClone.Attributes("SetID").Value = MaxSetCountFunction()
                                End If

                            Else
                                objChildNodeClone.Attributes("SetID").Value = drpSelectSet.SelectedValue
                            End If
                            objParentNode.AppendChild(objChildNodeClone)
                            objChildNodeClone = objChildNode.CloneNode(True)
                        End If
                    Next Rowno

                    If objInputXml.DocumentElement.SelectNodes("FIELDDETAILS").Count = 0 Then
                        'If hdAction.Value = "I" Then
                        'lblError.Text = "Atleast one field must be selected for set configuration."
                        ' Exit Sub
                        'End If


                        objChildNodeClone.Attributes("DM_FIELD_ID").Value = ""
                        objChildNodeClone.Attributes("EmployeeID").Value = Session("LoginSession").ToString().Split("|")(0).ToString
                        If hdAction.Value = "I" Then
                            'objChildNodeClone.Attributes("SetID").Value = MaxSetCountFunction()
                            If MaxSetCountFunction() = 0 Then
                                objChildNodeClone.Attributes("SetID").Value = 1
                            Else
                                objChildNodeClone.Attributes("SetID").Value = MaxSetCountFunction()
                            End If
                        Else
                            objChildNodeClone.Attributes("SetID").Value = drpSelectSet.SelectedValue
                        End If
                        objParentNode.AppendChild(objChildNodeClone)
                    End If
                    'Here Back end Method Call
                    If gvHelpDeskSearchCofig.Rows.Count <= 0 Then
                        lblError.Text = "There is no Row for Insert/Update"
                        Exit Sub
                    End If
                    objOutputXml = objbzDMReportConfiguration.UpdateSearchConfiguration(objInputXml)

                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        'ClientScript.RegisterClientScriptBlock(Me.GetType, "st", "<script>window.opener.location.reload(true);window.close();</script>")
                        'ClientScript.RegisterClientScriptBlock(Me.GetType, "st", "<script>window.close();</script>")
                        'Response.Redirect("../HelpDesk/HDRPT_HelpDeskDynamicReport.aspx", False)
                        If hdAction.Value = "I" Then
                            lblError.Text = objeAAMSMessage.messInsert
                            hdAction.Value = ""
                            GetSetId()
                            Dim Counts As Integer = MaxSetCountFunction()
                            drpSelectSet.SelectedValue = (Counts - 1).ToString
                            LoadSearchConfigure(Session("LoginSession").ToString().Split("|")(0).ToString)
                        Else
                            lblError.Text = objeAAMSMessage.messUpdate
                        End If

                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If

                End If
            Catch ex As Exception
            lblError.Text = ex.Message
        End Try
        End If
    End Sub

    Protected Sub btnSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        Try
            Dim Rowno As Integer
            For Rowno = 0 To gvHelpDeskSearchCofig.Rows.Count - 1
                'objAptchildNodeClone.Attributes("Aoffice").InnerText = dbgrdManageSecurityRegion.Rows(Rowno).Cells(0).Text
                Dim chk As CheckBox = CType(gvHelpDeskSearchCofig.Rows(Rowno).FindControl("chkSelected"), CheckBox)
                chk.Checked = True



                Dim strFieldName As String = ""
                strFieldName = gvHelpDeskSearchCofig.Rows(Rowno).Cells(0).Text.Trim
                If chk.Checked = True And chk.Checked = True Then
                    EnableControls(strFieldName, Rowno)
                End If


            Next Rowno


            ' CallForDefaultValue()

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnDeSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeSelectAll.Click
        Try
            Dim Rowno As Integer
            For Rowno = 0 To gvHelpDeskSearchCofig.Rows.Count - 1
                'objAptchildNodeClone.Attributes("Aoffice").InnerText = dbgrdManageSecurityRegion.Rows(Rowno).Cells(0).Text
                Dim chk As CheckBox = CType(gvHelpDeskSearchCofig.Rows(Rowno).FindControl("chkSelected"), CheckBox)
                If chk.Enabled = True Then
                    chk.Checked = False
                End If
                If chk.Checked = False Then
                    Dim strFieldName As String = ""
                    strFieldName = gvHelpDeskSearchCofig.Rows(Rowno).Cells(0).Text.Trim
                    SetEmptyValueForUnselectedItem(strFieldName, Rowno)
                End If
            Next Rowno

            CallForDefaultValue()

            For Rowno = 0 To gvHelpDeskSearchCofig.Rows.Count - 1
                Dim chk As CheckBox = CType(gvHelpDeskSearchCofig.Rows(Rowno).FindControl("chkSelected"), CheckBox)
                If chk.Enabled = True Then
                    chk.Checked = False
                End If
                If chk.Checked = False Then
                    Dim strFieldName As String = ""
                    strFieldName = gvHelpDeskSearchCofig.Rows(Rowno).Cells(0).Text.Trim
                    SetEmptyValueForUnselectedItem(strFieldName, Rowno)

                    DisableControls(strFieldName, Rowno)

                End If
            Next Rowno

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
            Dim li As ListItem
            li = drpSelectSet.Items.FindByValue("")
            If li Is Nothing Then
                drpSelectSet.Items.Insert(0, New ListItem(""))
            End If
        End Try
    End Sub

    Protected Sub drpSelectSet_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpSelectSet.SelectedIndexChanged

        hdAction.Value = ""

        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            'Dim RowNo As Long
            Try
                If drpSelectSet.SelectedValue <> "" Then
                    If Session("LoginSession") IsNot Nothing Then
                        Dim objbzDMReportConfiguration As New AAMS.bizHelpDesk.bzDMReportConfiguration
                        objInputXml.LoadXml("<HD_VIEWDMSFIELDS_INPUT><EmployeeID /><CNFGFLAG/> <SetID/></HD_VIEWDMSFIELDS_INPUT>")
                        objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0).ToString
                        objInputXml.DocumentElement.SelectSingleNode("CNFGFLAG").InnerText = "S"

                        '@ Find Default Setting 
                        objInputXml.DocumentElement.SelectSingleNode("SetID").InnerText = drpSelectSet.SelectedValue



                        'Here Back end Method Call          

                        objOutputXml = objbzDMReportConfiguration.View(objInputXml)
                        ' objOutputXml.Load("e:\HelpSearch.xml")


                        'objOutputXml.LoadXml("<HD_GETDMSEARCHCONFIGURE_OUTPUT><FIELDDETAILS DM_FIELD_ID='AGENCY_NAME'  DM_FIELD_NAME='Agency Name' SELECTED='True' /><FIELDDETAILS DM_FIELD_ID='OFFICE_ID'   DM_FIELD_NAME='Office ID' SELECTED='False' /><FIELDDETAILS DM_FIELD_ID='CUST_CATG'    DM_FIELD_NAME='Customer Category' SELECTED='True' /><FIELDDETAILS DM_FIELD_ID='CALLER_NAME'    DM_FIELD_NAME='Caller Name'  SELECTED='True' /><Errors Status='FALSE'><Error Code='' Description='' /> </Errors></HD_GETDMSEARCHCONFIGURE_OUTPUT>")
                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            objXmlReader = New XmlNodeReader(objOutputXml)
                            ds.ReadXml(objXmlReader)

                            gvHelpDeskSearchCofig.DataSource = ds.Tables("FIELDDETAILS")
                            gvHelpDeskSearchCofig.DataBind()

                            CallForDefaultValue()



                            '@ Calling For EnableorDisableControls
                            Dim Rowno As Integer
                            For Rowno = 0 To gvHelpDeskSearchCofig.Rows.Count - 1
                                'objAptchildNodeClone.Attributes("Aoffice").InnerText = dbgrdManageSecurityRegion.Rows(Rowno).Cells(0).Text
                                Dim chk As CheckBox = CType(gvHelpDeskSearchCofig.Rows(Rowno).FindControl("chkSelected"), CheckBox)

                                Dim strFieldName As String = ""
                                strFieldName = gvHelpDeskSearchCofig.Rows(Rowno).Cells(0).Text.Trim
                                If chk.Checked = True And chk.Enabled = True Then
                                    EnableControls(strFieldName, Rowno)
                                ElseIf chk.Checked = False Then
                                    DisableControls(strFieldName, Rowno)
                                End If
                            Next Rowno
                            '@ Calling For EnableorDisableControls


                        Else
                            gvHelpDeskSearchCofig.DataSource = Nothing
                            gvHelpDeskSearchCofig.DataBind()
                            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                        End If
                    End If
                Else
                    gvHelpDeskSearchCofig.DataSource = Nothing
                    gvHelpDeskSearchCofig.DataBind()
                End If

            Catch ex As Exception
                lblError.Text = ex.Message
            End Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnNewSet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewSet.Click
        hdAction.Value = "I"
        drpSelectSet.SelectedValue = ""
        ' Response.Redirect("", False)
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            ' Dim RowNo As Long
            Try
                If MaxSetCountFunction() > 10 Then
                    gvHelpDeskSearchCofig.DataSource = Nothing
                    gvHelpDeskSearchCofig.DataBind()
                    lblError.Text = "You can't make more than 10 sets"
                    Exit Sub
                End If

                If Session("LoginSession") IsNot Nothing Then
                    Dim objbzDMReportConfiguration As New AAMS.bizHelpDesk.bzDMReportConfiguration
                    objInputXml.LoadXml("<HD_VIEWDMSFIELDS_INPUT><EmployeeID /><CNFGFLAG/> <SetID/></HD_VIEWDMSFIELDS_INPUT>")
                    objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0).ToString
                    objInputXml.DocumentElement.SelectSingleNode("CNFGFLAG").InnerText = "S"

                    '@ Find Default Setting 
                    '  objInputXml.DocumentElement.SelectSingleNode("SetID").InnerText = "" ' drpSelectSet.SelectedValue
                    If MaxSetCountFunction() = 0 Then
                        objInputXml.DocumentElement.SelectSingleNode("SetID").InnerText = 1
                    Else

                        objInputXml.DocumentElement.SelectSingleNode("SetID").InnerText = MaxSetCountFunction()
                    End If

                    ' objInputXml.DocumentElement.SelectSingleNode("SetID").InnerText = MaxSetCountFunction()


                    'Here Back end Method Call          
                    objOutputXml = objbzDMReportConfiguration.View(objInputXml)
                    'objOutputXml.LoadXml("<HD_GETDMSEARCHCONFIGURE_OUTPUT><FIELDDETAILS DM_FIELD_ID='AGENCY_NAME'  DM_FIELD_NAME='Agency Name' SELECTED='True' /><FIELDDETAILS DM_FIELD_ID='OFFICE_ID'   DM_FIELD_NAME='Office ID' SELECTED='False' /><FIELDDETAILS DM_FIELD_ID='CUST_CATG'    DM_FIELD_NAME='Customer Category' SELECTED='True' /><FIELDDETAILS DM_FIELD_ID='CALLER_NAME'    DM_FIELD_NAME='Caller Name'  SELECTED='True' /><Errors Status='FALSE'><Error Code='' Description='' /> </Errors></HD_GETDMSEARCHCONFIGURE_OUTPUT>")
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        objXmlReader = New XmlNodeReader(objOutputXml)
                        ds.ReadXml(objXmlReader)

                        gvHelpDeskSearchCofig.DataSource = ds.Tables("FIELDDETAILS")
                        gvHelpDeskSearchCofig.DataBind()

                        CallForDefaultValue()


                        '@ Calling For EnableorDisableControls
                        Dim Rowno As Integer
                        For Rowno = 0 To gvHelpDeskSearchCofig.Rows.Count - 1
                            'objAptchildNodeClone.Attributes("Aoffice").InnerText = dbgrdManageSecurityRegion.Rows(Rowno).Cells(0).Text
                            Dim chk As CheckBox = CType(gvHelpDeskSearchCofig.Rows(Rowno).FindControl("chkSelected"), CheckBox)

                            Dim strFieldName As String = ""
                            strFieldName = gvHelpDeskSearchCofig.Rows(Rowno).Cells(0).Text.Trim
                            If chk.Checked = True And chk.Enabled = True Then
                                EnableControls(strFieldName, Rowno)
                            ElseIf chk.Checked = False Then
                                DisableControls(strFieldName, Rowno)
                            End If
                        Next Rowno
                        '@ Calling For EnableorDisableControls

                    Else
                        gvHelpDeskSearchCofig.DataSource = Nothing
                        gvHelpDeskSearchCofig.DataBind()
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If

            Catch ex As Exception
                lblError.Text = ex.Message
            End Try

        Catch ex As Exception

        End Try
    End Sub

    Private Function MaxSetCountFunction() As Integer
        Dim objbzDMReportConfiguration As New AAMS.bizHelpDesk.bzDMReportConfiguration
        Dim objOutputXml As XmlDocument
        Dim ds As New DataSet
        objOutputXml = New XmlDocument
        Dim objInXml As New XmlDocument
        Dim count As Integer = 0
        Try
            objInXml.LoadXml("<HD_GETDMCONFIGURESET_INPUT> <EmployeeID></EmployeeID></HD_GETDMCONFIGURESET_INPUT>")
            If Session("LoginSession") IsNot Nothing Then
                objInXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0).ToString
                objOutputXml = objbzDMReportConfiguration.GetConfiguredSetCount(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    count = Val(objOutputXml.DocumentElement.SelectSingleNode("CONFIGUREDSET").Attributes("SetCount").Value)
                    count = count + 1
                End If
            End If
            Return count
        Catch ex As Exception
            Return count
        End Try
    End Function

    Protected Function GetEnableorDisable(ByVal value As Object) As Boolean
        Dim blnvalue As Boolean = False
        If value IsNot Nothing Then
            If value.ToString = "True" Then
                blnvalue = False
            Else
                blnvalue = True
            End If
        Else
            blnvalue = True
        End If
        Return blnvalue
    End Function

    Protected Function GetEnableorDisableOrChecked(ByVal value As Object, ByVal value2 As Object) As Boolean
        Dim blnvalue As Boolean = False

        If value2.ToString = "True" Then
            blnvalue = True
        Else
            If value IsNot Nothing Then
                If value.ToString = "True" Then

                    blnvalue = True
                Else
                    blnvalue = False
                End If
            Else
                blnvalue = False
            End If
        End If
        Return blnvalue
    End Function

    Protected Sub gvHelpDeskSearchCofig_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then


                Dim HdValue As HiddenField = CType(e.Row.FindControl("HdValue"), HiddenField)

                Dim txtAgencyName As TextBox
                txtAgencyName = CType(e.Row.FindControl("txtAgencyName"), TextBox)
                Dim txtLTRNo As TextBox
                txtLTRNo = CType(e.Row.FindControl("txtLTRNo"), TextBox)
                Dim txtPTRNo As TextBox
                txtPTRNo = CType(e.Row.FindControl("txtPTRNo"), TextBox)
                Dim txtOfficeID As TextBox
                txtOfficeID = CType(e.Row.FindControl("txtOfficeID"), TextBox)
                Dim txtLoggedBy As TextBox
                txtLoggedBy = CType(e.Row.FindControl("txtLoggedBy"), TextBox)
                Dim txtCallerName As TextBox
                txtCallerName = CType(e.Row.FindControl("txtCallerName"), TextBox)
                Dim txtWorkOrderNo As TextBox
                txtWorkOrderNo = CType(e.Row.FindControl("txtWorkOrderNo"), TextBox)
                Dim txtAddresses As TextBox
                txtAddresses = CType(e.Row.FindControl("txtAddresses"), TextBox)
                Dim txtTit As TextBox
                txtTit = CType(e.Row.FindControl("txtTit"), TextBox)

                Dim PnlOpenDateFrom As Panel
                PnlOpenDateFrom = CType(e.Row.FindControl("PnlOpenDateFrom"), Panel)
                Dim txtQueryOpenedDateFrom As TextBox
                txtQueryOpenedDateFrom = CType(e.Row.FindControl("txtQueryOpenedDateFrom"), TextBox)
                Dim imgOpenedDateFrom As HtmlImage
                imgOpenedDateFrom = CType(e.Row.FindControl("imgOpenedDateFrom"), HtmlImage)


                Dim PnlOpenDateTo As Panel
                PnlOpenDateTo = CType(e.Row.FindControl("PnlOpenDateTo"), Panel)
                Dim txtQueryOpenedDateTo As TextBox
                txtQueryOpenedDateTo = CType(e.Row.FindControl("txtQueryOpenedDateTo"), TextBox)
                Dim imgOpenedDateTo As HtmlImage
                imgOpenedDateTo = CType(e.Row.FindControl("imgOpenedDateTo"), HtmlImage)


               
                Dim PnlCloseDateFrom As Panel
                PnlCloseDateFrom = CType(e.Row.FindControl("PnlCloseDateFrom"), Panel)
                Dim txtCloseDateFrom As TextBox
                txtCloseDateFrom = CType(e.Row.FindControl("txtCloseDateFrom"), TextBox)
                Dim imgCloseDateFrom As HtmlImage
                imgCloseDateFrom = CType(e.Row.FindControl("imgCloseDateFrom"), HtmlImage)

                Dim PnlCloseDateTo As Panel
                PnlCloseDateTo = CType(e.Row.FindControl("PnlCloseDateTo"), Panel)
                Dim txtCloseDateTo As TextBox
                txtCloseDateTo = CType(e.Row.FindControl("txtCloseDateTo"), TextBox)
                Dim imgCloseDateTo As HtmlImage
                imgCloseDateTo = CType(e.Row.FindControl("imgCloseDateTo"), HtmlImage)

                Dim PnlDateAssigned As Panel
                PnlDateAssigned = CType(e.Row.FindControl("PnlDateAssigned"), Panel)
                Dim txtDateAssigned As TextBox
                txtDateAssigned = CType(e.Row.FindControl("txtDateAssigned"), TextBox)
                Dim imgDateAssigned As HtmlImage
                imgDateAssigned = CType(e.Row.FindControl("imgDateAssigned"), HtmlImage)




               

                Dim ddlQueryGroup As DropDownList
                ddlQueryGroup = CType(e.Row.FindControl("ddlQueryGroup"), DropDownList)
                Dim ddlQuerySubGroup As DropDownList
                ddlQuerySubGroup = CType(e.Row.FindControl("ddlQuerySubGroup"), DropDownList)
                Dim ddlQueryCategory As DropDownList
                ddlQueryCategory = CType(e.Row.FindControl("ddlQueryCategory"), DropDownList)
                Dim ddlQuerySubCategory As DropDownList
                ddlQuerySubCategory = CType(e.Row.FindControl("ddlQuerySubCategory"), DropDownList)
                Dim ddlQueryStatus As DropDownList
                ddlQueryStatus = CType(e.Row.FindControl("ddlQueryStatus"), DropDownList)
                Dim ddlQueryPriority As DropDownList
                ddlQueryPriority = CType(e.Row.FindControl("ddlQueryPriority"), DropDownList)
                Dim ddlCoordinator1 As DropDownList
                ddlCoordinator1 = CType(e.Row.FindControl("ddlCoordinator1"), DropDownList)
                Dim ddlCoordinator2 As DropDownList
                ddlCoordinator2 = CType(e.Row.FindControl("ddlCoordinator2"), DropDownList)
                Dim ddlDisposition As DropDownList
                ddlDisposition = CType(e.Row.FindControl("ddlDisposition"), DropDownList)
                Dim ddlAOffice As DropDownList
                ddlAOffice = CType(e.Row.FindControl("ddlAOffice"), DropDownList)
                Dim ddlAgencyAOffice As DropDownList
                ddlAgencyAOffice = CType(e.Row.FindControl("ddlAgencyAOffice"), DropDownList)
                Dim ddlFollowup As DropDownList
                ddlFollowup = CType(e.Row.FindControl("ddlFollowup"), DropDownList)
                Dim ddlAssignedTo As DropDownList
                ddlAssignedTo = CType(e.Row.FindControl("ddlAssignedTo"), DropDownList)
                Dim ddlContactType As DropDownList
                ddlContactType = CType(e.Row.FindControl("ddlContactType"), DropDownList)

                Dim ddlCustomerCategory As DropDownList
                ddlCustomerCategory = CType(e.Row.FindControl("ddlCustomerCategory"), DropDownList)


                Dim DlstAgencyStatus As DropDownList
                DlstAgencyStatus = CType(e.Row.FindControl("DlstAgencyStatus"), DropDownList)

                Dim DlstAgencyType As DropDownList
                DlstAgencyType = CType(e.Row.FindControl("DlstAgencyType"), DropDownList)




                Dim ChkLastCall As CheckBox
                ChkLastCall = CType(e.Row.FindControl("ChkLastCall"), CheckBox)

                Dim txtHD_IR_REF As TextBox
                txtHD_IR_REF = CType(e.Row.FindControl("txtHD_IR_REF"), TextBox)

                Dim DlstCompVertical As DropDownList
                DlstCompVertical = CType(e.Row.FindControl("DlstCompVertical"), DropDownList)

                Dim DlstAirLine As DropDownList
                DlstAirLine = CType(e.Row.FindControl("DlstAirLine"), DropDownList)


                'objeAAMS.BindDropDown(DlstAirLine, "AIRLINE", True, 3)


                '          
                '           
                '            
                '            
                '           



                '           
                '           




                ' NumericValue

                Dim strFieldName As String = ""
                strFieldName = e.Row.Cells(0).Text.Trim
                Select Case strFieldName
                    Case "LTR No"
                        txtLTRNo.Visible = True

                        txtLTRNo.Text = HdValue.Value

                        txtLTRNo.Attributes.Add("onblur", "return NumericValue('" + txtLTRNo.ClientID + "','" + "LTR No." + "')")

                    Case "Agency"
                        txtAgencyName.Visible = True
                        txtAgencyName.Text = HdValue.Value
                    Case "Address"
                        txtAddresses.Visible = True
                        txtAddresses.Text = HdValue.Value
                    Case "PTR Number"
                        txtPTRNo.Visible = True
                        txtPTRNo.Text = HdValue.Value

                        txtPTRNo.Attributes.Add("onblur", "return NumericValue('" + txtPTRNo.ClientID + "','" + "PTR No." + "')")

                    Case "Last Call"

                        ChkLastCall.Visible = True
                        If HdValue.Value = "1" Then
                            ChkLastCall.Checked = True
                        Else
                            ChkLastCall.Checked = False
                        End If

                    Case "Query Status"
                        ddlQueryStatus.Visible = True
                        Dim li As New ListItem
                        li = ddlQueryStatus.Items.FindByValue(HdValue.Value)
                        If li IsNot Nothing Then
                            ddlQueryStatus.SelectedValue = HdValue.Value
                        End If
                        'xtPTRNo.Text = HdValue.Value

                    Case "Follow up"
                        ddlFollowup.Visible = True
                        objeAAMS.BindDropDown(ddlFollowup, "WorkOrderFollowUp", True, 3)

                        Dim li As New ListItem
                        li = ddlFollowup.Items.FindByValue(HdValue.Value)
                        If li IsNot Nothing Then
                            ddlFollowup.SelectedValue = HdValue.Value
                        End If


                    Case "Severity"
                        ddlQueryPriority.Visible = True

                        Dim li As New ListItem
                        li = ddlQueryPriority.Items.FindByValue(HdValue.Value)
                        If li IsNot Nothing Then
                            ddlQueryPriority.SelectedValue = HdValue.Value
                        End If


                    Case "LCode"

                    Case "Logged By"
                        txtLoggedBy.Visible = True
                        txtLoggedBy.Text = HdValue.Value

                    Case "Close Date From"
                        PnlCloseDateFrom.Visible = True
                        txtCloseDateFrom.Attributes.Add("onblur", "return DateValidation('" + txtCloseDateFrom.ClientID + "')")
                        imgCloseDateFrom.Attributes.Add("onmousedown", "SelectDate('" & txtCloseDateFrom.ClientID.ToString() & "','" & imgCloseDateFrom.ClientID.ToString() & "');")

                        txtCloseDateFrom.Text = HdValue.Value

                    Case "Close Date To"
                        PnlCloseDateTo.Visible = True
                        txtCloseDateTo.Attributes.Add("onblur", "return DateValidation('" + txtCloseDateTo.ClientID + "')")
                        imgCloseDateTo.Attributes.Add("onmousedown", "SelectDate('" & txtCloseDateTo.ClientID.ToString() & "','" & imgCloseDateTo.ClientID.ToString() & "');")

                        txtCloseDateTo.Text = HdValue.Value

                    Case "Open Date From"
                        PnlOpenDateFrom.Visible = True
                        txtQueryOpenedDateFrom.Attributes.Add("onblur", "return DateValidation('" + txtQueryOpenedDateFrom.ClientID + "')")
                        imgOpenedDateFrom.Attributes.Add("onmousedown", "SelectDate('" & txtQueryOpenedDateFrom.ClientID.ToString() & "','" & imgOpenedDateFrom.ClientID.ToString() & "');")

                        txtQueryOpenedDateFrom.Text = HdValue.Value

                    Case "Open Date To"
                        PnlOpenDateTo.Visible = True
                        txtQueryOpenedDateTo.Attributes.Add("onblur", "return DateValidation('" + txtQueryOpenedDateTo.ClientID + "')")
                        imgOpenedDateTo.Attributes.Add("onmousedown", "SelectDate('" & txtQueryOpenedDateTo.ClientID.ToString() & "','" & imgOpenedDateTo.ClientID.ToString() & "');")
                        txtQueryOpenedDateTo.Text = HdValue.Value

                    Case "Assigned To"
                        ddlAssignedTo.Visible = True
                        Dim li As New ListItem
                        Dim strvalue As String = "1|" + HdValue.Value

                        li = ddlAssignedTo.Items.FindByValue(strvalue)
                        If li IsNot Nothing Then
                            ddlAssignedTo.SelectedValue = strvalue
                        Else
                            Dim li2 As New ListItem
                            strvalue = ""
                            strvalue = "2|" + HdValue.Value
                            li2 = ddlAssignedTo.Items.FindByValue(strvalue)
                            If li2 IsNot Nothing Then
                                ddlAssignedTo.SelectedValue = strvalue
                            End If
                        End If


                    Case "Assigned Date Time"
                        PnlDateAssigned.Visible = True
                        txtDateAssigned.Attributes.Add("onblur", "return DateValidationForAssignDate('" + txtDateAssigned.ClientID + "')")
                        imgDateAssigned.Attributes.Add("onmousedown", "SelectDateForAssignDate('" & txtDateAssigned.ClientID.ToString() & "','" & imgDateAssigned.ClientID.ToString() & "');")
                        txtDateAssigned.Text = HdValue.Value

                    Case "Office ID"
                        txtOfficeID.Visible = True
                        txtOfficeID.Text = HdValue.Value

                    Case "Customer Category"
                        ddlCustomerCategory.Visible = True
                        objeAAMS.BindDropDown(ddlCustomerCategory, "CUSTOMERCATEGORY", True, 3)

                        Dim li As New ListItem
                        li = ddlCustomerCategory.Items.FindByValue(HdValue.Value)
                        If li IsNot Nothing Then
                            ddlCustomerCategory.SelectedValue = HdValue.Value
                        End If


                    Case "Caller Name"
                        txtCallerName.Visible = True
                        txtCallerName.Text = HdValue.Value
                    Case "Query Group"
                        ddlQueryGroup.Visible = True

                        Dim li As New ListItem
                        li = ddlQueryGroup.Items.FindByValue(HdValue.Value)
                        If li IsNot Nothing Then
                            ddlQueryGroup.SelectedValue = HdValue.Value
                        End If

                    Case "Query Sub Group"
                        ddlQuerySubGroup.Visible = True

                        Dim li As New ListItem
                        li = ddlQuerySubGroup.Items.FindByValue(HdValue.Value)
                        If li IsNot Nothing Then
                            ddlQuerySubGroup.SelectedValue = HdValue.Value
                        End If

                    Case "Query Category"
                        ddlQueryCategory.Visible = True

                        Dim li As New ListItem
                        li = ddlQueryCategory.Items.FindByValue(HdValue.Value)
                        If li IsNot Nothing Then
                            ddlQueryCategory.SelectedValue = HdValue.Value
                        End If
                    Case "Query Sub Category"
                        ddlQuerySubCategory.Visible = True

                        Dim li As New ListItem
                        li = ddlQuerySubCategory.Items.FindByValue(HdValue.Value)
                        If li IsNot Nothing Then
                            ddlQuerySubCategory.SelectedValue = HdValue.Value
                        End If

                    Case "Coordinator1"
                        ddlCoordinator1.Visible = True
                        objeAAMS.BindDropDown(ddlCoordinator1, "COORDINATOR1", True, 3)

                        Dim li As New ListItem
                        li = ddlCoordinator1.Items.FindByValue(HdValue.Value)
                        If li IsNot Nothing Then
                            ddlCoordinator1.SelectedValue = HdValue.Value
                        End If

                    Case "Coordinator2"
                        ddlCoordinator2.Visible = True
                        objeAAMS.BindDropDown(ddlCoordinator2, "COORDINATOR2", True, 3)

                        Dim li As New ListItem
                        li = ddlCoordinator2.Items.FindByValue(HdValue.Value)
                        If li IsNot Nothing Then
                            ddlCoordinator2.SelectedValue = HdValue.Value
                        End If

                    Case "Disposition"
                        ddlDisposition.Visible = True
                        objeAAMS.BindDropDown(ddlDisposition, "DISPOSITION", True, 3)

                        Dim li As New ListItem
                        li = ddlDisposition.Items.FindByValue(HdValue.Value)
                        If li IsNot Nothing Then
                            ddlDisposition.SelectedValue = HdValue.Value
                        End If


                    Case "1A Office"
                        ddlAOffice.Visible = True
                        objeAAMS.BindDropDown(ddlAOffice, "AOFFICE", True, 3)

                        Dim li As New ListItem
                        li = ddlAOffice.Items.FindByValue(HdValue.Value)
                        If li IsNot Nothing Then
                            ddlAOffice.SelectedValue = HdValue.Value
                        End If

                    Case "Agency 1A Office"
                        ddlAgencyAOffice.Visible = True
                        objeAAMS.BindDropDown(ddlAgencyAOffice, "AOFFICE", True, 3)

                        Dim li As New ListItem
                        li = ddlAgencyAOffice.Items.FindByValue(HdValue.Value)
                        If li IsNot Nothing Then
                            ddlAgencyAOffice.SelectedValue = HdValue.Value
                        End If


                    Case "Work Order No"
                        txtWorkOrderNo.Visible = True
                        txtWorkOrderNo.Text = HdValue.Value

                        txtWorkOrderNo.Attributes.Add("onblur", "return NumericValue('" + txtWorkOrderNo.ClientID + "','" + "Work Order No." + "')")

                    Case "Contact Type"
                        ddlContactType.Visible = True
                        objeAAMS.BindDropDown(ddlContactType, "CONTACTTYPE", True, 3)


                        Dim li As New ListItem
                        li = ddlContactType.Items.FindByValue(HdValue.Value)
                        If li IsNot Nothing Then
                            ddlContactType.SelectedValue = HdValue.Value
                        End If

                    Case "Link Ltr"

                    Case "Title"
                        txtTit.Visible = True
                        txtTit.Text = HdValue.Value


                    Case "Agency Status"
                        DlstAgencyStatus.Visible = True
                        objeAAMS.BindDropDown(DlstAgencyStatus, "AGENCYSTATUS", True, 3)

                        Dim li As New ListItem
                        li = DlstAgencyStatus.Items.FindByValue(HdValue.Value)
                        If li IsNot Nothing Then
                            DlstAgencyStatus.SelectedValue = HdValue.Value
                        End If

                    Case "Agency Type"
                        DlstAgencyType.Visible = True
                        objeAAMS.BindDropDown(DlstAgencyType, "AGENCYTYPE", True, 3)

                        Dim li As New ListItem
                        li = DlstAgencyType.Items.FindByValue(HdValue.Value)
                        If li IsNot Nothing Then
                            DlstAgencyType.SelectedValue = HdValue.Value
                        End If


                    Case "IR Number"
                        txtHD_IR_REF.Visible = True
                        txtHD_IR_REF.Text = HdValue.Value
                        txtHD_IR_REF.Attributes.Add("onblur", "return NumericValue('" + txtHD_IR_REF.ClientID + "','" + "IR Number" + "')")


                    Case "Company Vertical"
                        DlstCompVertical.Visible = True
                        objeAAMS.BindDropDown(DlstCompVertical, "CompanyVertical", True, 3)
                        Dim li As New ListItem
                        li = DlstCompVertical.Items.FindByValue(HdValue.Value)
                        If li IsNot Nothing Then
                            DlstCompVertical.SelectedValue = HdValue.Value
                        End If

                    Case "AIRLINE"
                        DlstAirLine.Visible = True
                        objeAAMS.BindDropDown(DlstAirLine, "AIRLINE", True, 3)
                        Dim li As New ListItem
                        li = DlstAirLine.Items.FindByValue(HdValue.Value)
                        If li IsNot Nothing Then
                            DlstAirLine.SelectedValue = HdValue.Value
                        End If

                End Select

                Dim chkSelected As CheckBox
                chkSelected = CType(e.Row.FindControl("chkSelected"), CheckBox)
                chkSelected.Attributes.Add("GettingRowNo", e.Row.RowIndex.ToString)

            End If
        Catch ex As Exception

        End Try
    End Sub


    Sub loadAssignedTo()
        'Code to fill Assigned To
        Dim objOutputXml As New XmlDocument
        Dim objInputXml As New XmlDocument
        Dim ds As New DataSet
        objOutputXml = New XmlDocument
        Dim objInXml As New XmlDocument
        Dim objbzEmployee As New AAMS.bizMaster.bzEmployee

        Dim ddlAssignedTo As DropDownList
        Dim ddlQueryGroup As DropDownList
        Dim HdValue As HiddenField

        For i As Integer = 0 To gvHelpDeskSearchCofig.Rows.Count - 1


            Dim ddlTempQueryGroup As DropDownList
            ddlTempQueryGroup = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlQueryGroup"), DropDownList)
            If ddlTempQueryGroup IsNot Nothing Then
                If ddlTempQueryGroup.Visible = True Then
                    ddlQueryGroup = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlQueryGroup"), DropDownList)
                End If
            Else
                ddlQueryGroup = Nothing
            End If

            Dim ddlTempAssignedTo As DropDownList
            ddlTempAssignedTo = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlAssignedTo"), DropDownList)
            If ddlTempAssignedTo IsNot Nothing Then
                If ddlTempAssignedTo.Visible = True Then
                    ddlAssignedTo = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlAssignedTo"), DropDownList)

                    '@ Setting Selected Value
                    HdValue = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("HdValue"), HiddenField)

                End If
            Else
                ddlAssignedTo = Nothing
            End If
        Next


        If ddlAssignedTo IsNot Nothing And ddlQueryGroup IsNot Nothing Then
            ddlAssignedTo.Items.Clear()
            If ddlQueryGroup.SelectedValue = "" Then
                objOutputXml = objbzEmployee.List
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    For Each objnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("EMPLOYEE")
                        ddlAssignedTo.Items.Add(New ListItem(objnode.Attributes("Employee_Name").Value, "2" + "|" + objnode.Attributes("EmployeeID").Value))
                    Next
                End If

                Dim objbzTeam As New AAMS.bizHelpDesk.bzTeam
                objOutputXml = New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
                objOutputXml = objbzTeam.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    For Each objnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("TEAM")
                        ddlAssignedTo.Items.Add(New ListItem(objnode.Attributes("TEAM_NAME").Value, "1" + "|" + objnode.Attributes("TEAM_ID").Value))
                    Next
                End If

            ElseIf ddlQueryGroup.SelectedValue = "1" Then
                Dim objbzTeam As New AAMS.bizHelpDesk.bzTeam
                objOutputXml = New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
                objOutputXml = objbzTeam.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    For Each objnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("TEAM")
                        ddlAssignedTo.Items.Add(New ListItem(objnode.Attributes("TEAM_NAME").Value, "1" + "|" + objnode.Attributes("TEAM_ID").Value))
                    Next
                End If
            ElseIf ddlQueryGroup.SelectedValue = "2" Then

                objOutputXml = objbzEmployee.List
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    For Each objnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("EMPLOYEE")
                        ddlAssignedTo.Items.Add(New ListItem(objnode.Attributes("Employee_Name").Value, "2" + "|" + objnode.Attributes("EmployeeID").Value))
                    Next
                End If

            End If
            ddlAssignedTo.Items.Insert(0, New ListItem("--All--", ""))


            If HdValue IsNot Nothing Then
                Dim li As New ListItem
                Dim strvalue As String = "1|" + HdValue.Value

                li = ddlAssignedTo.Items.FindByValue(strvalue)
                If li IsNot Nothing Then
                    ddlAssignedTo.SelectedValue = strvalue
                Else
                    Dim li2 As New ListItem
                    strvalue = ""
                    strvalue = "2|" + HdValue.Value
                    li2 = ddlAssignedTo.Items.FindByValue(strvalue)
                    If li2 IsNot Nothing Then
                        ddlAssignedTo.SelectedValue = strvalue
                    End If
                End If
            End If



        End If

        
    End Sub
   

    
    Sub loadSubGroup()
        'Code to fill Query Sub Group
        Dim objCallSubGroup As New AAMS.bizHelpDesk.bzCallSubGroup
        Dim objOutputXml As XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        objOutputXml = New XmlDocument
        Dim objInXml As New XmlDocument

        Dim ddlQueryGroup As DropDownList
        Dim ddlQuerySubGroup As DropDownList

        Dim HdValue As HiddenField

        For i As Integer = 0 To gvHelpDeskSearchCofig.Rows.Count - 1



            Dim ddlTempQueryGroup As DropDownList
            ddlTempQueryGroup = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlQueryGroup"), DropDownList)
            If ddlTempQueryGroup IsNot Nothing Then
                If ddlTempQueryGroup.Visible = True Then
                    ddlQueryGroup = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlQueryGroup"), DropDownList)
                End If
            Else
                ddlQueryGroup = Nothing
            End If
            Dim ddlTempQuerySubGroup As DropDownList
            ddlTempQuerySubGroup = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlQuerySubGroup"), DropDownList)
            If ddlTempQuerySubGroup IsNot Nothing Then
                If ddlTempQuerySubGroup.Visible = True Then
                    ddlQuerySubGroup = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlQuerySubGroup"), DropDownList)

                    '@ Setting Selected Value
                    HdValue = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("HdValue"), HiddenField)

                End If
            Else
                ddlQuerySubGroup = Nothing
            End If


        Next


        If ddlQueryGroup IsNot Nothing And ddlQuerySubGroup IsNot Nothing Then
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


                If HdValue IsNot Nothing Then
                    Dim li As New ListItem
                    li = ddlQuerySubGroup.Items.FindByValue(HdValue.Value)
                    If li IsNot Nothing Then
                        ddlQuerySubGroup.SelectedValue = HdValue.Value
                    End If
                End If




            Else
                ddlQuerySubGroup.Items.Clear()
                ddlQuerySubGroup.Items.Insert(0, New ListItem("--All--", ""))
            End If

        End If
       
    End Sub

    Sub loadQueryStatus()
        'Code to fill Query Status
        Dim objCallStatus As New AAMS.bizHelpDesk.bzCallStatus
        Dim objOutputXml1 As XmlDocument
        Dim objXmlReader1 As XmlNodeReader
        Dim ds1 As New DataSet

        Dim ddlQueryGroup As DropDownList
        Dim ddlQueryStatus As DropDownList
        Dim HdValue As HiddenField
        For i As Integer = 0 To gvHelpDeskSearchCofig.Rows.Count - 1


            Dim ddlTempQueryGroup As DropDownList
            ddlTempQueryGroup = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlQueryGroup"), DropDownList)
            If ddlTempQueryGroup IsNot Nothing Then
                If ddlTempQueryGroup.Visible = True Then
                    ddlQueryGroup = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlQueryGroup"), DropDownList)
                End If
            Else
                ddlQueryGroup = Nothing
            End If

            Dim ddlTempQueryStatus As DropDownList
            ddlTempQueryStatus = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlQueryStatus"), DropDownList)
            If ddlTempQueryStatus IsNot Nothing Then
                If ddlTempQueryStatus.Visible = True Then
                    ddlQueryStatus = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlQueryStatus"), DropDownList)

                    '@ Setting Selected Value
                    HdValue = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("HdValue"), HiddenField)

                End If
            Else
                ddlQueryStatus = Nothing
            End If



        Next


        If ddlQueryGroup IsNot Nothing And ddlQueryStatus IsNot Nothing Then
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


                If HdValue IsNot Nothing Then
                    Dim li As New ListItem
                    li = ddlQueryStatus.Items.FindByValue(HdValue.Value)
                    If li IsNot Nothing Then
                        ddlQueryStatus.SelectedValue = HdValue.Value
                    End If
                End If

            Else
                ddlQueryStatus.Items.Clear()
                ddlQueryStatus.Items.Insert(0, New ListItem("--All--", ""))
            End If
        End If
       

    End Sub

    Sub loadPriority()
        'Code to fill Query Priority
        Dim objSeverities As New AAMS.bizHelpDesk.bzSeverities
        Dim objOutputXml2 As XmlDocument
        Dim objXmlReader2 As XmlNodeReader
        Dim ds2 As New DataSet

        Dim ddlQueryGroup As DropDownList
        Dim ddlQueryPriority As DropDownList
        Dim HdValue As HiddenField

        For i As Integer = 0 To gvHelpDeskSearchCofig.Rows.Count - 1


            Dim ddlTempQueryGroup As DropDownList
            ddlTempQueryGroup = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlQueryGroup"), DropDownList)
            If ddlTempQueryGroup IsNot Nothing Then
                If ddlTempQueryGroup.Visible = True Then
                    ddlQueryGroup = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlQueryGroup"), DropDownList)
                End If
            Else
                ddlQueryGroup = Nothing
            End If

            Dim ddlTempQueryPriority As DropDownList
            ddlTempQueryPriority = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlQueryPriority"), DropDownList)
            If ddlTempQueryPriority IsNot Nothing Then
                If ddlTempQueryPriority.Visible = True Then
                    ddlQueryPriority = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlQueryPriority"), DropDownList)
                    '@ Setting Selected Value
                    HdValue = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("HdValue"), HiddenField)

                End If
            Else
                ddlQueryPriority = Nothing
            End If
          


        Next


        If ddlQueryGroup IsNot Nothing And ddlQueryPriority IsNot Nothing Then
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

                If HdValue IsNot Nothing Then
                    Dim li As New ListItem
                    li = ddlQueryPriority.Items.FindByValue(HdValue.Value)
                    If li IsNot Nothing Then
                        ddlQueryPriority.SelectedValue = HdValue.Value
                    End If
                End If

            Else
                ddlQueryPriority.Items.Clear()
                ddlQueryPriority.Items.Insert(0, New ListItem("--All--", ""))
            End If
        End If

       
    End Sub
    Sub loadCategory()
        Dim objCallCategory As New AAMS.bizHelpDesk.bzCallCategory
        Dim objOutputXml As XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        objOutputXml = New XmlDocument
        Dim objInXml As New XmlDocument


        Dim ddlQueryCategory As DropDownList
        Dim ddlQuerySubCategory As DropDownList
        Dim ddlQuerySubGroup As DropDownList

        Dim HdValue As HiddenField

        For i As Integer = 0 To gvHelpDeskSearchCofig.Rows.Count - 1


            Dim ddlTempQuerySubGroup As DropDownList
            ddlTempQuerySubGroup = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlQuerySubGroup"), DropDownList)
            If ddlTempQuerySubGroup IsNot Nothing Then
                If ddlTempQuerySubGroup.Visible = True Then
                    ddlQuerySubGroup = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlQuerySubGroup"), DropDownList)
                End If
            Else
                ddlQuerySubGroup = Nothing
            End If

            Dim ddlTempQueryCategory As DropDownList
            ddlTempQueryCategory = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlQueryCategory"), DropDownList)
            If ddlTempQueryCategory IsNot Nothing Then
                If ddlTempQueryCategory.Visible = True Then
                    ddlQueryCategory = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlQueryCategory"), DropDownList)

                    '@ Setting Selected Value
                    HdValue = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("HdValue"), HiddenField)

                End If
            Else
                ddlQueryCategory = Nothing
            End If
            Dim ddlTempQuerySubCategory As DropDownList
            ddlTempQuerySubCategory = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlQuerySubCategory"), DropDownList)
            If ddlTempQuerySubCategory IsNot Nothing Then
                If ddlTempQuerySubCategory.Visible = True Then
                    ddlQuerySubCategory = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlQuerySubCategory"), DropDownList)
                End If
            Else
                ddlQuerySubCategory = Nothing
            End If


        Next


        If ddlQueryCategory IsNot Nothing And ddlQuerySubCategory IsNot Nothing And ddlQuerySubGroup IsNot Nothing Then
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


                If HdValue IsNot Nothing Then
                    Dim li As New ListItem
                    li = ddlQueryCategory.Items.FindByValue(HdValue.Value)
                    If li IsNot Nothing Then
                        ddlQueryCategory.SelectedValue = HdValue.Value
                    End If
                End If


            Else
                ddlQueryCategory.Items.Clear()
                ddlQueryCategory.Items.Insert(0, New ListItem("--All--", ""))
                ddlQuerySubCategory.Items.Clear()
                ddlQuerySubCategory.Items.Insert(0, New ListItem("--All--", ""))
            End If
        End If


       
    End Sub
    Sub loadSubCategory()

        Dim ddlQueryCategory As DropDownList

        Dim objCallSubCategory As New AAMS.bizHelpDesk.bzCallSubCategory
        Dim objOutputXml As XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        objOutputXml = New XmlDocument
        Dim objInXml As New XmlDocument
        objInXml.LoadXml("<HD_LISTCALLSUBCATEGORY_INPUT> <CALL_CATEGORY_ID /></HD_LISTCALLSUBCATEGORY_INPUT>")


        Dim ddlQuerySubCategory As DropDownList
        Dim HdValue As HiddenField

        For i As Integer = 0 To gvHelpDeskSearchCofig.Rows.Count - 1

            Dim ddlTempQuerySubCategory As DropDownList
            ddlTempQuerySubCategory = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlQuerySubCategory"), DropDownList)
            If ddlTempQuerySubCategory IsNot Nothing Then
                If ddlTempQuerySubCategory.Visible = True Then
                    ddlQuerySubCategory = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlQuerySubCategory"), DropDownList)

                    '@ Setting Selected Value
                    HdValue = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("HdValue"), HiddenField)

                End If
            Else
                ddlQuerySubCategory = Nothing
            End If
            Dim ddlTempQueryCategory As DropDownList
            ddlTempQueryCategory = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlQueryCategory"), DropDownList)
            If ddlTempQueryCategory IsNot Nothing Then
                If ddlTempQueryCategory.Visible = True Then
                    ddlQueryCategory = CType(gvHelpDeskSearchCofig.Rows(i).FindControl("ddlQueryCategory"), DropDownList)
                End If
            Else
                ddlQueryCategory = Nothing
            End If


        Next


        If ddlQuerySubCategory IsNot Nothing And ddlQueryCategory IsNot Nothing Then

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

                If HdValue IsNot Nothing Then
                    Dim li As New ListItem
                    li = ddlQuerySubCategory.Items.FindByValue(HdValue.Value)
                    If li IsNot Nothing Then
                        ddlQuerySubCategory.SelectedValue = HdValue.Value
                    End If
                End If

            Else
                ddlQuerySubCategory.Items.Clear()
                ddlQuerySubCategory.Items.Insert(0, New ListItem("--All--", ""))
            End If
        End If

    End Sub
    Protected Sub ddlQueryGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
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
    Protected Sub ddlQuerySubGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            loadCategory()
            '  loadSubCategory()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub ddlQueryCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            loadSubCategory()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Private Sub CallForDefaultValue()
        loadSubGroup()
        loadCategory()
        loadSubCategory()
        loadPriority()
        loadQueryStatus()
        loadAssignedTo()
    End Sub
    Private Function SetValue(ByVal strFieldName As String, ByVal RowNo As Integer) As String
        Dim strvalue As String = ""

        Try
            Dim txtAgencyName As TextBox
            txtAgencyName = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtAgencyName"), TextBox)
            Dim txtLTRNo As TextBox
            txtLTRNo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtLTRNo"), TextBox)
            Dim txtPTRNo As TextBox
            txtPTRNo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtPTRNo"), TextBox)
            Dim txtOfficeID As TextBox
            txtOfficeID = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtOfficeID"), TextBox)
            Dim txtLoggedBy As TextBox
            txtLoggedBy = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtLoggedBy"), TextBox)
            Dim txtCallerName As TextBox
            txtCallerName = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtCallerName"), TextBox)
            Dim txtWorkOrderNo As TextBox
            txtWorkOrderNo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtWorkOrderNo"), TextBox)
            Dim txtAddresses As TextBox
            txtAddresses = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtAddresses"), TextBox)
            Dim txtTit As TextBox
            txtTit = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtTit"), TextBox)

            Dim PnlOpenDateFrom As Panel
            PnlOpenDateFrom = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("PnlOpenDateFrom"), Panel)
            Dim txtQueryOpenedDateFrom As TextBox
            txtQueryOpenedDateFrom = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtQueryOpenedDateFrom"), TextBox)
            Dim imgOpenedDateFrom As HtmlImage
            imgOpenedDateFrom = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("imgOpenedDateFrom"), HtmlImage)


            Dim PnlOpenDateTo As Panel
            PnlOpenDateTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("PnlOpenDateTo"), Panel)
            Dim txtQueryOpenedDateTo As TextBox
            txtQueryOpenedDateTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtQueryOpenedDateTo"), TextBox)
            Dim imgOpenedDateTo As HtmlImage
            imgOpenedDateTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("imgOpenedDateTo"), HtmlImage)



            Dim PnlCloseDateFrom As Panel
            PnlCloseDateFrom = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("PnlCloseDateFrom"), Panel)
            Dim txtCloseDateFrom As TextBox
            txtCloseDateFrom = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtCloseDateFrom"), TextBox)
            Dim imgCloseDateFrom As HtmlImage
            imgCloseDateFrom = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("imgCloseDateFrom"), HtmlImage)

            Dim PnlCloseDateTo As Panel
            PnlCloseDateTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("PnlCloseDateTo"), Panel)
            Dim txtCloseDateTo As TextBox
            txtCloseDateTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtCloseDateTo"), TextBox)
            Dim imgCloseDateTo As HtmlImage
            imgCloseDateTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("imgCloseDateTo"), HtmlImage)

            Dim PnlDateAssigned As Panel
            PnlDateAssigned = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("PnlDateAssigned"), Panel)
            Dim txtDateAssigned As TextBox
            txtDateAssigned = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtDateAssigned"), TextBox)
            Dim imgDateAssigned As HtmlImage
            imgDateAssigned = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("imgDateAssigned"), HtmlImage)






            Dim ddlQueryGroup As DropDownList
            ddlQueryGroup = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlQueryGroup"), DropDownList)
            Dim ddlQuerySubGroup As DropDownList
            ddlQuerySubGroup = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlQuerySubGroup"), DropDownList)
            Dim ddlQueryCategory As DropDownList
            ddlQueryCategory = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlQueryCategory"), DropDownList)
            Dim ddlQuerySubCategory As DropDownList
            ddlQuerySubCategory = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlQuerySubCategory"), DropDownList)
            Dim ddlQueryStatus As DropDownList
            ddlQueryStatus = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlQueryStatus"), DropDownList)
            Dim ddlQueryPriority As DropDownList
            ddlQueryPriority = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlQueryPriority"), DropDownList)
            Dim ddlCoordinator1 As DropDownList
            ddlCoordinator1 = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlCoordinator1"), DropDownList)
            Dim ddlCoordinator2 As DropDownList
            ddlCoordinator2 = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlCoordinator2"), DropDownList)
            Dim ddlDisposition As DropDownList
            ddlDisposition = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlDisposition"), DropDownList)
            Dim ddlAOffice As DropDownList
            ddlAOffice = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlAOffice"), DropDownList)
            Dim ddlAgencyAOffice As DropDownList
            ddlAgencyAOffice = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlAgencyAOffice"), DropDownList)
            Dim ddlFollowup As DropDownList
            ddlFollowup = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlFollowup"), DropDownList)
            Dim ddlAssignedTo As DropDownList
            ddlAssignedTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlAssignedTo"), DropDownList)
            Dim ddlContactType As DropDownList
            ddlContactType = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlContactType"), DropDownList)

            Dim ddlCustomerCategory As DropDownList
            ddlCustomerCategory = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlCustomerCategory"), DropDownList)

            Dim ChkLastCall As CheckBox
            ChkLastCall = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ChkLastCall"), CheckBox)


            Dim DlstAgencyStatus As DropDownList
            DlstAgencyStatus = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("DlstAgencyStatus"), DropDownList)

            Dim DlstAgencyType As DropDownList
            DlstAgencyType = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("DlstAgencyType"), DropDownList)

            Dim txtHD_IR_REF As TextBox
            txtHD_IR_REF = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtHD_IR_REF"), TextBox)

            Dim DlstCompVertical As DropDownList
            DlstCompVertical = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("DlstCompVertical"), DropDownList)

            Dim DlstAirline As DropDownList
            DlstAirline = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("DlstAirline"), DropDownList)





            Select Case strFieldName
                Case "LTR No"
                    strvalue = txtLTRNo.Text

                Case "Agency"
                    strvalue = txtAgencyName.Text

                Case "Address"
                    strvalue = txtAddresses.Text
                    ' txtAddresses.Visible = True
                Case "PTR Number"
                    strvalue = txtPTRNo.Text

                Case "Last Call"
                    If ChkLastCall.Checked = True Then
                        strvalue = "1"
                    Else
                        strvalue = "0"
                    End If

                Case "Query Status"
                    strvalue = ddlQueryStatus.SelectedValue

                Case "Follow up"
                    strvalue = ddlFollowup.SelectedValue

                Case "Severity"
                    strvalue = ddlQueryPriority.SelectedValue

                Case "LCode"
                    strvalue = ""
                Case "Logged By"
                    strvalue = txtLoggedBy.Text


                Case "Close Date From"
                    strvalue = txtCloseDateFrom.Text

                Case "Close Date To"
                    strvalue = txtCloseDateTo.Text


                Case "Open Date From"
                    strvalue = txtQueryOpenedDateFrom.Text

                Case "Open Date To"
                    strvalue = txtQueryOpenedDateTo.Text

                Case "Assigned To"
                    If ddlAssignedTo.SelectedValue.Trim.Split("|")(1) IsNot Nothing Then
                        strvalue = ddlAssignedTo.SelectedValue.Trim.Split("|")(1)
                    End If



                Case "Assigned Date Time"
                    strvalue = txtDateAssigned.Text


                Case "Office ID"
                    strvalue = txtOfficeID.Text

                Case "Customer Category"
                    strvalue = ddlCustomerCategory.SelectedValue


                Case "Caller Name"

                    strvalue = txtCallerName.Text
                Case "Query Group"

                    strvalue = ddlQueryGroup.SelectedValue
                Case "Query Sub Group"
                    strvalue = ddlQuerySubGroup.SelectedValue

                Case "Query Category"
                    strvalue = ddlQueryCategory.SelectedValue
                Case "Query Sub Category"

                    strvalue = ddlQuerySubCategory.SelectedValue
                Case "Coordinator1"

                    strvalue = ddlCoordinator1.SelectedValue

                Case "Coordinator2"
                    strvalue = ddlCoordinator2.SelectedValue

                Case "Disposition"
                    strvalue = ddlDisposition.SelectedValue

                Case "1A Office"
                    strvalue = ddlAOffice.SelectedValue

                Case "Agency 1A Office"
                    strvalue = ddlAgencyAOffice.SelectedValue

                Case "Work Order No"
                    strvalue = txtWorkOrderNo.Text

                Case "Contact Type"
                    strvalue = ddlContactType.SelectedValue

                Case "Link Ltr"
                    strvalue = ""
                Case "Title"
                    strvalue = txtTit.Text

                Case "Agency Status"
                    strvalue = DlstAgencyStatus.SelectedValue
                Case "Agency Type"
                    strvalue = DlstAgencyType.SelectedValue
                Case "IR Number"
                    strvalue = txtHD_IR_REF.Text
                Case "Company Vertical"
                    strvalue = DlstCompVertical.SelectedValue

                Case "AIRLINE"
                    strvalue = DlstAirline.SelectedValue

            End Select

            Return strvalue
        Catch ex As Exception
            Return strvalue
        End Try
    End Function

    Private Function SetEmptyValueForUnselectedItem(ByVal strFieldName As String, ByVal RowNo As Integer) As String
        Dim strvalue As String = ""

        Try
            Dim txtAgencyName As TextBox
            txtAgencyName = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtAgencyName"), TextBox)
            Dim txtLTRNo As TextBox
            txtLTRNo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtLTRNo"), TextBox)
            Dim txtPTRNo As TextBox
            txtPTRNo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtPTRNo"), TextBox)
            Dim txtOfficeID As TextBox
            txtOfficeID = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtOfficeID"), TextBox)
            Dim txtLoggedBy As TextBox
            txtLoggedBy = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtLoggedBy"), TextBox)
            Dim txtCallerName As TextBox
            txtCallerName = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtCallerName"), TextBox)
            Dim txtWorkOrderNo As TextBox
            txtWorkOrderNo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtWorkOrderNo"), TextBox)
            Dim txtAddresses As TextBox
            txtAddresses = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtAddresses"), TextBox)
            Dim txtTit As TextBox
            txtTit = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtTit"), TextBox)

            Dim PnlOpenDateFrom As Panel
            PnlOpenDateFrom = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("PnlOpenDateFrom"), Panel)
            Dim txtQueryOpenedDateFrom As TextBox
            txtQueryOpenedDateFrom = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtQueryOpenedDateFrom"), TextBox)
            Dim imgOpenedDateFrom As HtmlImage
            imgOpenedDateFrom = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("imgOpenedDateFrom"), HtmlImage)


            Dim PnlOpenDateTo As Panel
            PnlOpenDateTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("PnlOpenDateTo"), Panel)
            Dim txtQueryOpenedDateTo As TextBox
            txtQueryOpenedDateTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtQueryOpenedDateTo"), TextBox)
            Dim imgOpenedDateTo As HtmlImage
            imgOpenedDateTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("imgOpenedDateTo"), HtmlImage)



            Dim PnlCloseDateFrom As Panel
            PnlCloseDateFrom = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("PnlCloseDateFrom"), Panel)
            Dim txtCloseDateFrom As TextBox
            txtCloseDateFrom = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtCloseDateFrom"), TextBox)
            Dim imgCloseDateFrom As HtmlImage
            imgCloseDateFrom = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("imgCloseDateFrom"), HtmlImage)

            Dim PnlCloseDateTo As Panel
            PnlCloseDateTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("PnlCloseDateTo"), Panel)
            Dim txtCloseDateTo As TextBox
            txtCloseDateTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtCloseDateTo"), TextBox)
            Dim imgCloseDateTo As HtmlImage
            imgCloseDateTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("imgCloseDateTo"), HtmlImage)

            Dim PnlDateAssigned As Panel
            PnlDateAssigned = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("PnlDateAssigned"), Panel)
            Dim txtDateAssigned As TextBox
            txtDateAssigned = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtDateAssigned"), TextBox)
            Dim imgDateAssigned As HtmlImage
            imgDateAssigned = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("imgDateAssigned"), HtmlImage)






            Dim ddlQueryGroup As DropDownList
            ddlQueryGroup = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlQueryGroup"), DropDownList)
            Dim ddlQuerySubGroup As DropDownList
            ddlQuerySubGroup = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlQuerySubGroup"), DropDownList)
            Dim ddlQueryCategory As DropDownList
            ddlQueryCategory = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlQueryCategory"), DropDownList)
            Dim ddlQuerySubCategory As DropDownList
            ddlQuerySubCategory = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlQuerySubCategory"), DropDownList)
            Dim ddlQueryStatus As DropDownList
            ddlQueryStatus = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlQueryStatus"), DropDownList)
            Dim ddlQueryPriority As DropDownList
            ddlQueryPriority = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlQueryPriority"), DropDownList)
            Dim ddlCoordinator1 As DropDownList
            ddlCoordinator1 = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlCoordinator1"), DropDownList)
            Dim ddlCoordinator2 As DropDownList
            ddlCoordinator2 = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlCoordinator2"), DropDownList)
            Dim ddlDisposition As DropDownList
            ddlDisposition = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlDisposition"), DropDownList)
            Dim ddlAOffice As DropDownList
            ddlAOffice = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlAOffice"), DropDownList)
            Dim ddlAgencyAOffice As DropDownList
            ddlAgencyAOffice = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlAgencyAOffice"), DropDownList)
            Dim ddlFollowup As DropDownList
            ddlFollowup = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlFollowup"), DropDownList)
            Dim ddlAssignedTo As DropDownList
            ddlAssignedTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlAssignedTo"), DropDownList)
            Dim ddlContactType As DropDownList
            ddlContactType = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlContactType"), DropDownList)

            Dim ddlCustomerCategory As DropDownList
            ddlCustomerCategory = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlCustomerCategory"), DropDownList)

            Dim ChkLastCall As CheckBox
            ChkLastCall = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ChkLastCall"), CheckBox)



            Dim HdValue As HiddenField = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("HdValue"), HiddenField)
            HdValue.Value = ""


            Dim DlstAgencyStatus As DropDownList
            DlstAgencyStatus = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("DlstAgencyStatus"), DropDownList)

            Dim DlstAgencyType As DropDownList
            DlstAgencyType = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("DlstAgencyType"), DropDownList)

            Dim txtHD_IR_REF As TextBox
            txtHD_IR_REF = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtHD_IR_REF"), TextBox)

            Dim DlstCompVertical As DropDownList
            DlstCompVertical = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("DlstCompVertical"), DropDownList)

            Dim DlstAirline As DropDownList
            DlstAirline = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("DlstAirline"), DropDownList)



            Select Case strFieldName
                Case "LTR No"
                    txtLTRNo.Text = ""

                Case "Agency"
                    txtAgencyName.Text = ""

                Case "Address"
                    txtAddresses.Text = ""
                    ' txtAddresses.Visible = True
                Case "PTR Number"
                    txtPTRNo.Text = ""

                Case "Last Call"
                    ChkLastCall.Checked = False

                Case "Query Status"
                    ddlQueryStatus.SelectedValue = ""

                Case "Follow up"
                    ddlFollowup.SelectedValue = ""

                Case "Severity"
                    ddlQueryPriority.SelectedValue = ""

                Case "LCode"
                    strvalue = ""
                Case "Logged By"
                    txtLoggedBy.Text = ""


                Case "Close Date From"
                    txtCloseDateFrom.Text = ""

                Case "Close Date To"
                    txtCloseDateTo.Text = ""


                Case "Open Date From"
                    txtQueryOpenedDateFrom.Text = ""

                Case "Open Date To"
                    txtQueryOpenedDateTo.Text = ""

                Case "Assigned To"
                    ddlAssignedTo.SelectedValue = ""

                Case "Assigned Date Time"
                    txtDateAssigned.Text = ""

                Case "Office ID"
                    txtOfficeID.Text = ""

                Case "Customer Category"
                    ddlCustomerCategory.SelectedValue = ""

                Case "Caller Name"

                    txtCallerName.Text = ""
                Case "Query Group"

                    ddlQueryGroup.SelectedValue = ""
                Case "Query Sub Group"
                    ddlQuerySubGroup.SelectedValue = ""

                Case "Query Category"
                    ddlQueryCategory.SelectedValue = ""
                Case "Query Sub Category"

                    ddlQuerySubCategory.SelectedValue = ""
                Case "Coordinator1"

                    ddlCoordinator1.SelectedValue = ""

                Case "Coordinator2"
                    ddlCoordinator2.SelectedValue = ""

                Case "Disposition"
                    ddlDisposition.SelectedValue = ""

                Case "1A Office"
                    ddlAOffice.SelectedValue = ""

                Case "Agency 1A Office"
                    ddlAgencyAOffice.SelectedValue = ""

                Case "Work Order No"
                    txtWorkOrderNo.Text = ""

                Case "Contact Type"
                    ddlContactType.SelectedValue = ""

                Case "Link Ltr"
                    strvalue = ""
                Case "Title"
                    txtTit.Text = ""

                Case "Agency Status"
                    DlstAgencyStatus.SelectedValue = ""
                Case "Agency Type"
                    DlstAgencyType.SelectedValue = ""
                Case "IR Number"
                    txtHD_IR_REF.Text = ""
                Case "Company Vertical"
                    DlstCompVertical.SelectedValue = ""

                Case "AIRLINE"
                    Try
                        DlstAirline.SelectedValue = ""
                    Catch ex As Exception
                    End Try


            End Select

            Return strvalue
        Catch ex As Exception
            Return strvalue
        End Try
    End Function


    Private Function EnableControls(ByVal strFieldName As String, ByVal RowNo As Integer) As String
        Dim strvalue As String = ""

        Try
            Dim txtAgencyName As TextBox
            txtAgencyName = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtAgencyName"), TextBox)
            Dim txtLTRNo As TextBox
            txtLTRNo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtLTRNo"), TextBox)
            Dim txtPTRNo As TextBox
            txtPTRNo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtPTRNo"), TextBox)
            Dim txtOfficeID As TextBox
            txtOfficeID = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtOfficeID"), TextBox)
            Dim txtLoggedBy As TextBox
            txtLoggedBy = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtLoggedBy"), TextBox)
            Dim txtCallerName As TextBox
            txtCallerName = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtCallerName"), TextBox)
            Dim txtWorkOrderNo As TextBox
            txtWorkOrderNo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtWorkOrderNo"), TextBox)
            Dim txtAddresses As TextBox
            txtAddresses = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtAddresses"), TextBox)
            Dim txtTit As TextBox
            txtTit = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtTit"), TextBox)

            Dim PnlOpenDateFrom As Panel
            PnlOpenDateFrom = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("PnlOpenDateFrom"), Panel)
            Dim txtQueryOpenedDateFrom As TextBox
            txtQueryOpenedDateFrom = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtQueryOpenedDateFrom"), TextBox)
            Dim imgOpenedDateFrom As HtmlImage
            imgOpenedDateFrom = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("imgOpenedDateFrom"), HtmlImage)


            Dim PnlOpenDateTo As Panel
            PnlOpenDateTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("PnlOpenDateTo"), Panel)
            Dim txtQueryOpenedDateTo As TextBox
            txtQueryOpenedDateTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtQueryOpenedDateTo"), TextBox)
            Dim imgOpenedDateTo As HtmlImage
            imgOpenedDateTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("imgOpenedDateTo"), HtmlImage)



            Dim PnlCloseDateFrom As Panel
            PnlCloseDateFrom = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("PnlCloseDateFrom"), Panel)
            Dim txtCloseDateFrom As TextBox
            txtCloseDateFrom = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtCloseDateFrom"), TextBox)
            Dim imgCloseDateFrom As HtmlImage
            imgCloseDateFrom = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("imgCloseDateFrom"), HtmlImage)

            Dim PnlCloseDateTo As Panel
            PnlCloseDateTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("PnlCloseDateTo"), Panel)
            Dim txtCloseDateTo As TextBox
            txtCloseDateTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtCloseDateTo"), TextBox)
            Dim imgCloseDateTo As HtmlImage
            imgCloseDateTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("imgCloseDateTo"), HtmlImage)

            Dim PnlDateAssigned As Panel
            PnlDateAssigned = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("PnlDateAssigned"), Panel)
            Dim txtDateAssigned As TextBox
            txtDateAssigned = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtDateAssigned"), TextBox)
            Dim imgDateAssigned As HtmlImage
            imgDateAssigned = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("imgDateAssigned"), HtmlImage)






            Dim ddlQueryGroup As DropDownList
            ddlQueryGroup = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlQueryGroup"), DropDownList)
            Dim ddlQuerySubGroup As DropDownList
            ddlQuerySubGroup = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlQuerySubGroup"), DropDownList)
            Dim ddlQueryCategory As DropDownList
            ddlQueryCategory = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlQueryCategory"), DropDownList)
            Dim ddlQuerySubCategory As DropDownList
            ddlQuerySubCategory = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlQuerySubCategory"), DropDownList)
            Dim ddlQueryStatus As DropDownList
            ddlQueryStatus = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlQueryStatus"), DropDownList)
            Dim ddlQueryPriority As DropDownList
            ddlQueryPriority = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlQueryPriority"), DropDownList)
            Dim ddlCoordinator1 As DropDownList
            ddlCoordinator1 = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlCoordinator1"), DropDownList)
            Dim ddlCoordinator2 As DropDownList
            ddlCoordinator2 = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlCoordinator2"), DropDownList)
            Dim ddlDisposition As DropDownList
            ddlDisposition = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlDisposition"), DropDownList)
            Dim ddlAOffice As DropDownList
            ddlAOffice = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlAOffice"), DropDownList)
            Dim ddlAgencyAOffice As DropDownList
            ddlAgencyAOffice = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlAgencyAOffice"), DropDownList)
            Dim ddlFollowup As DropDownList
            ddlFollowup = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlFollowup"), DropDownList)
            Dim ddlAssignedTo As DropDownList
            ddlAssignedTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlAssignedTo"), DropDownList)
            Dim ddlContactType As DropDownList
            ddlContactType = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlContactType"), DropDownList)

            Dim ddlCustomerCategory As DropDownList
            ddlCustomerCategory = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlCustomerCategory"), DropDownList)

            Dim ChkLastCall As CheckBox
            ChkLastCall = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ChkLastCall"), CheckBox)


            Dim DlstAgencyStatus As DropDownList
            DlstAgencyStatus = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("DlstAgencyStatus"), DropDownList)

            Dim DlstAgencyType As DropDownList
            DlstAgencyType = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("DlstAgencyType"), DropDownList)

            Dim txtHD_IR_REF As TextBox
            txtHD_IR_REF = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtHD_IR_REF"), TextBox)

            Dim DlstCompVertical As DropDownList
            DlstCompVertical = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("DlstCompVertical"), DropDownList)

            Dim DlstAirline As DropDownList
            DlstAirline = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("DlstAirline"), DropDownList)



            Select Case strFieldName
                Case "LTR No"
                    txtLTRNo.Enabled = True

                Case "Agency"
                    txtAgencyName.Enabled = True

                Case "Address"
                    txtAddresses.Enabled = True
                    ' txtAddresses.Visible = True
                Case "PTR Number"
                    txtPTRNo.Enabled = True

                Case "Last Call"
                    ChkLastCall.Enabled = True

                Case "Query Status"
                    ddlQueryStatus.Enabled = True

                Case "Follow up"
                    ddlFollowup.Enabled = True

                Case "Severity"
                    ddlQueryPriority.Enabled = True

                Case "LCode"
                    strvalue = ""
                Case "Logged By"
                    txtLoggedBy.Enabled = True


                Case "Close Date From"
                    txtCloseDateFrom.Enabled = True

                Case "Close Date To"
                    txtCloseDateTo.Enabled = True


                Case "Open Date From"
                    txtQueryOpenedDateFrom.Enabled = True

                Case "Open Date To"
                    txtQueryOpenedDateTo.Enabled = True

                Case "Assigned To"
                    ddlAssignedTo.Enabled = True

                Case "Assigned Date Time"
                    txtDateAssigned.Enabled = True

                Case "Office ID"
                    txtOfficeID.Enabled = True

                Case "Customer Category"
                    ddlCustomerCategory.Enabled = True

                Case "Caller Name"

                    txtCallerName.Enabled = True
                Case "Query Group"

                    ddlQueryGroup.Enabled = True
                Case "Query Sub Group"
                    ddlQuerySubGroup.Enabled = True

                Case "Query Category"
                    ddlQueryCategory.Enabled = True
                Case "Query Sub Category"

                    ddlQuerySubCategory.Enabled = True
                Case "Coordinator1"

                    ddlCoordinator1.Enabled = True

                Case "Coordinator2"
                    ddlCoordinator2.Enabled = True

                Case "Disposition"
                    ddlDisposition.Enabled = True

                Case "1A Office"
                    ddlAOffice.Enabled = True

                Case "Agency 1A Office"
                    ddlAgencyAOffice.Enabled = True

                Case "Work Order No"
                    txtWorkOrderNo.Enabled = True

                Case "Contact Type"
                    ddlContactType.Enabled = True

                Case "Link Ltr"
                    strvalue = ""
                Case "Title"
                    txtTit.Enabled = True

                Case "Agency Status"
                    DlstAgencyStatus.Enabled = True
                Case "Agency Type"
                    DlstAgencyType.Enabled = True

                Case "IR Number"
                    txtHD_IR_REF.Enabled = True
                Case "Company Vertical"
                    DlstCompVertical.Enabled = True
                Case "AIRLINE"
                    DlstAirline.Enabled = True

            End Select

            Return strvalue
        Catch ex As Exception
            Return strvalue
        End Try
    End Function

    Private Function DisableControls(ByVal strFieldName As String, ByVal RowNo As Integer) As String
        Dim strvalue As String = ""

        Try
            Dim txtAgencyName As TextBox
            txtAgencyName = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtAgencyName"), TextBox)
            Dim txtLTRNo As TextBox
            txtLTRNo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtLTRNo"), TextBox)
            Dim txtPTRNo As TextBox
            txtPTRNo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtPTRNo"), TextBox)
            Dim txtOfficeID As TextBox
            txtOfficeID = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtOfficeID"), TextBox)
            Dim txtLoggedBy As TextBox
            txtLoggedBy = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtLoggedBy"), TextBox)
            Dim txtCallerName As TextBox
            txtCallerName = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtCallerName"), TextBox)
            Dim txtWorkOrderNo As TextBox
            txtWorkOrderNo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtWorkOrderNo"), TextBox)
            Dim txtAddresses As TextBox
            txtAddresses = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtAddresses"), TextBox)
            Dim txtTit As TextBox
            txtTit = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtTit"), TextBox)

            Dim PnlOpenDateFrom As Panel
            PnlOpenDateFrom = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("PnlOpenDateFrom"), Panel)
            Dim txtQueryOpenedDateFrom As TextBox
            txtQueryOpenedDateFrom = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtQueryOpenedDateFrom"), TextBox)
            Dim imgOpenedDateFrom As HtmlImage
            imgOpenedDateFrom = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("imgOpenedDateFrom"), HtmlImage)


            Dim PnlOpenDateTo As Panel
            PnlOpenDateTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("PnlOpenDateTo"), Panel)
            Dim txtQueryOpenedDateTo As TextBox
            txtQueryOpenedDateTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtQueryOpenedDateTo"), TextBox)
            Dim imgOpenedDateTo As HtmlImage
            imgOpenedDateTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("imgOpenedDateTo"), HtmlImage)



            Dim PnlCloseDateFrom As Panel
            PnlCloseDateFrom = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("PnlCloseDateFrom"), Panel)
            Dim txtCloseDateFrom As TextBox
            txtCloseDateFrom = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtCloseDateFrom"), TextBox)
            Dim imgCloseDateFrom As HtmlImage
            imgCloseDateFrom = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("imgCloseDateFrom"), HtmlImage)

            Dim PnlCloseDateTo As Panel
            PnlCloseDateTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("PnlCloseDateTo"), Panel)
            Dim txtCloseDateTo As TextBox
            txtCloseDateTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtCloseDateTo"), TextBox)
            Dim imgCloseDateTo As HtmlImage
            imgCloseDateTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("imgCloseDateTo"), HtmlImage)

            Dim PnlDateAssigned As Panel
            PnlDateAssigned = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("PnlDateAssigned"), Panel)
            Dim txtDateAssigned As TextBox
            txtDateAssigned = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtDateAssigned"), TextBox)
            Dim imgDateAssigned As HtmlImage
            imgDateAssigned = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("imgDateAssigned"), HtmlImage)






            Dim ddlQueryGroup As DropDownList
            ddlQueryGroup = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlQueryGroup"), DropDownList)
            Dim ddlQuerySubGroup As DropDownList
            ddlQuerySubGroup = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlQuerySubGroup"), DropDownList)
            Dim ddlQueryCategory As DropDownList
            ddlQueryCategory = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlQueryCategory"), DropDownList)
            Dim ddlQuerySubCategory As DropDownList
            ddlQuerySubCategory = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlQuerySubCategory"), DropDownList)
            Dim ddlQueryStatus As DropDownList
            ddlQueryStatus = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlQueryStatus"), DropDownList)
            Dim ddlQueryPriority As DropDownList
            ddlQueryPriority = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlQueryPriority"), DropDownList)
            Dim ddlCoordinator1 As DropDownList
            ddlCoordinator1 = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlCoordinator1"), DropDownList)
            Dim ddlCoordinator2 As DropDownList
            ddlCoordinator2 = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlCoordinator2"), DropDownList)
            Dim ddlDisposition As DropDownList
            ddlDisposition = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlDisposition"), DropDownList)
            Dim ddlAOffice As DropDownList
            ddlAOffice = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlAOffice"), DropDownList)
            Dim ddlAgencyAOffice As DropDownList
            ddlAgencyAOffice = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlAgencyAOffice"), DropDownList)
            Dim ddlFollowup As DropDownList
            ddlFollowup = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlFollowup"), DropDownList)
            Dim ddlAssignedTo As DropDownList
            ddlAssignedTo = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlAssignedTo"), DropDownList)
            Dim ddlContactType As DropDownList
            ddlContactType = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlContactType"), DropDownList)

            Dim ddlCustomerCategory As DropDownList
            ddlCustomerCategory = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ddlCustomerCategory"), DropDownList)

            Dim ChkLastCall As CheckBox
            ChkLastCall = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("ChkLastCall"), CheckBox)

            Dim DlstAgencyStatus As DropDownList
            DlstAgencyStatus = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("DlstAgencyStatus"), DropDownList)

            Dim DlstAgencyType As DropDownList
            DlstAgencyType = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("DlstAgencyType"), DropDownList)

            Dim txtHD_IR_REF As TextBox
            txtHD_IR_REF = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("txtHD_IR_REF"), TextBox)


            Dim DlstCompVertical As DropDownList
            DlstCompVertical = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("DlstCompVertical"), DropDownList)

            Dim Dlstairline As DropDownList
            Dlstairline = CType(gvHelpDeskSearchCofig.Rows(RowNo).FindControl("Dlstairline"), DropDownList)




            Select Case strFieldName
                Case "LTR No"
                    txtLTRNo.Enabled = False

                Case "Agency"
                    txtAgencyName.Enabled = False

                Case "Address"
                    txtAddresses.Enabled = False
                    ' txtAddresses.Visible = True
                Case "PTR Number"
                    txtPTRNo.Enabled = False

                Case "Last Call"
                    ChkLastCall.Enabled = False

                Case "Query Status"
                    ddlQueryStatus.Enabled = False

                Case "Follow up"
                    ddlFollowup.Enabled = False

                Case "Severity"
                    ddlQueryPriority.Enabled = False

                Case "LCode"
                    strvalue = ""
                Case "Logged By"
                    txtLoggedBy.Enabled = False


                Case "Close Date From"
                    txtCloseDateFrom.Enabled = False

                Case "Close Date To"
                    txtCloseDateTo.Enabled = False


                Case "Open Date From"
                    txtQueryOpenedDateFrom.Enabled = False

                Case "Open Date To"
                    txtQueryOpenedDateTo.Enabled = False

                Case "Assigned To"
                    ddlAssignedTo.Enabled = False

                Case "Assigned Date Time"
                    txtDateAssigned.Enabled = False

                Case "Office ID"
                    txtOfficeID.Enabled = False

                Case "Customer Category"
                    ddlCustomerCategory.Enabled = False

                Case "Caller Name"

                    txtCallerName.Enabled = False
                Case "Query Group"

                    ddlQueryGroup.Enabled = False
                Case "Query Sub Group"
                    ddlQuerySubGroup.Enabled = False

                Case "Query Category"
                    ddlQueryCategory.Enabled = False
                Case "Query Sub Category"

                    ddlQuerySubCategory.Enabled = False
                Case "Coordinator1"

                    ddlCoordinator1.Enabled = False

                Case "Coordinator2"
                    ddlCoordinator2.Enabled = False

                Case "Disposition"
                    ddlDisposition.Enabled = False

                Case "1A Office"
                    ddlAOffice.Enabled = False

                Case "Agency 1A Office"
                    ddlAgencyAOffice.Enabled = False

                Case "Work Order No"
                    txtWorkOrderNo.Enabled = False

                Case "Contact Type"
                    ddlContactType.Enabled = False

                Case "Link Ltr"
                    strvalue = ""
                Case "Title"
                    txtTit.Enabled = False

                Case "Agency Status"
                    DlstAgencyStatus.Enabled = False
                Case "Agency Type"
                    DlstAgencyType.Enabled = False
                Case "IR Number"
                    txtHD_IR_REF.Enabled = False
                Case "Company Vertical"
                    DlstCompVertical.Enabled = False
                Case "AIRLINE"
                    Dlstairline.Enabled = False

            End Select

            Return strvalue
        Catch ex As Exception
            Return strvalue
        End Try
    End Function

    Protected Sub chkSelected_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim chkSelected As CheckBox = CType(sender, CheckBox)
            Dim RowNo As Integer = Integer.Parse(chkSelected.Attributes("GettingRowNo"))
            Dim gvrow As GridViewRow = gvHelpDeskSearchCofig.Rows(RowNo)



            'If chkSelected.Checked = False And chkSelected.Enabled = True Then
            '    chkSelected.Checked = True
            'ElseIf chkSelected.Checked = True And chkSelected.Enabled = True Then
            '    chkSelected.Checked = False
            'End If

            Dim strFieldName As String = ""
            strFieldName = gvrow.Cells(0).Text.Trim
            If chkSelected.Checked = True And chkSelected.Enabled = True Then
                EnableControls(strFieldName, RowNo)
            ElseIf chkSelected.Checked = False Then
                DisableControls(strFieldName, RowNo)
                SetEmptyValueForUnselectedItem(strFieldName, RowNo)

            End If
          


        Catch ex As Exception

        End Try
    End Sub

   
End Class