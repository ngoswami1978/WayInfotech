'########################################################################
'############   Page Name -- Popup_PUSR_HelpDeskResultConfigure   #######
'############   Date 13-March 2007  ##################################
'############   Developed By Abhishek  ##################################
'########################################################################
Partial Class Popup_PUSR_HelpDeskResultConfigure
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim strBuilder As StringBuilder
    Dim objeAAMSMessage As New eAAMSMessage

#Region "Code for Filter "
    'Code Added by M. K
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    'Code Added by M. K
#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblError.Text = ""
        objeAAMS.ExpirePageCache()
        ' This code is usedc for checking session handler according to user login.
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
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
                LoadResultConfigure(Session("LoginSession").ToString().Split("|")(0).ToString)
            End If

        End If

    End Sub
    Sub LoadResultConfigure(ByVal strEmpId As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        ' Dim RowNo As Long
        Try
            If drpSelectSet.SelectedValue <> "" Then
                Dim objbzDMReportConfiguration As New AAMS.bizHelpDesk.bzDMReportConfiguration
                objInputXml.LoadXml("<HD_VIEWDMSFIELDS_INPUT><EmployeeID /><CNFGFLAG/><SetID/></HD_VIEWDMSFIELDS_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = strEmpId
                objInputXml.DocumentElement.SelectSingleNode("CNFGFLAG").InnerText = "R"
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
                'objOutputXml.LoadXml("<HD_GETDMRESULTCONFIGURE_OUTPUT><FIELDDETAILS DM_FIELD_ID='AGENCY_NAME'  DM_FIELD_NAME='Agency Name' SELECTED='True' /><FIELDDETAILS DM_FIELD_ID='OFFICE_ID'   DM_FIELD_NAME='Office ID' SELECTED='False' /><FIELDDETAILS DM_FIELD_ID='CUST_CATG'    DM_FIELD_NAME='Customer Category' SELECTED='True' /><FIELDDETAILS DM_FIELD_ID='CALLER_NAME'    DM_FIELD_NAME='Caller Name'  SELECTED='True' /><Errors Status='FALSE'><Error Code='' Description='' /> </Errors></HD_GETDMRESULTCONFIGURE_OUTPUT>")
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    gvHelpDeskResCofig.DataSource = ds.Tables("FIELDDETAILS")
                    gvHelpDeskResCofig.DataBind()
                Else
                    gvHelpDeskResCofig.DataSource = Nothing
                    gvHelpDeskResCofig.DataBind()
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

                If Not Session("LoginSession") Is Nothing Then
                    objInputXml.LoadXml("<HD_UPDATEDMRESULTCONFIGURE_INPUT><FIELDDETAILS DM_FIELD_ID='' EmployeeID='' SetID ='' /></HD_UPDATEDMRESULTCONFIGURE_INPUT>")

                    'Reading and Appending records into the Input XMLDocument
                    objParentNode = objInputXml.DocumentElement
                    objChildNode = objInputXml.DocumentElement.SelectSingleNode("FIELDDETAILS")
                    objChildNodeClone = objChildNode.CloneNode(True)
                    objParentNode.RemoveChild(objChildNode)
                    For Rowno = 0 To gvHelpDeskResCofig.Rows.Count - 1
                        'objAptchildNodeClone.Attributes("Aoffice").InnerText = dbgrdManageSecurityRegion.Rows(Rowno).Cells(0).Text
                        Dim chk As CheckBox = CType(gvHelpDeskResCofig.Rows(Rowno).FindControl("chkSelected"), CheckBox)
                        Dim hdDM_FIELD_ID As HiddenField = CType(gvHelpDeskResCofig.Rows(Rowno).FindControl("DM_FIELD_ID"), HiddenField)
                        If (chk.Checked = True) Then
                            'if (chk.Checked = True And chk.Enabled = True) Then
                            objChildNodeClone.Attributes("DM_FIELD_ID").Value = hdDM_FIELD_ID.Value
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
                            If MaxSetCountFunction() = 0 Then
                                objChildNodeClone.Attributes("SetID").Value = 1
                            Else
                                objChildNodeClone.Attributes("SetID").Value = MaxSetCountFunction()
                            End If
                            'objChildNodeClone.Attributes("SetID").Value = MaxSetCountFunction()
                        Else
                            objChildNodeClone.Attributes("SetID").Value = drpSelectSet.SelectedValue
                        End If
                        objParentNode.AppendChild(objChildNodeClone)
                    End If
                    'Here Back end Method Call
                    If gvHelpDeskResCofig.Rows.Count <= 0 Then
                        lblError.Text = "There is no Row for Insert/Update"
                        Exit Sub
                    End If

                    objOutputXml = objbzDMReportConfiguration.UpdateResultConfiguration(objInputXml)

                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                        '   lblError.Text = objeAAMSMessage.messUpdate
                        If hdAction.Value = "I" Then
                            hdAction.Value = ""
                            lblError.Text = objeAAMSMessage.messInsert
                            GetSetId()
                            Dim Counts As Integer = MaxSetCountFunction()
                            drpSelectSet.SelectedValue = (Counts - 1).ToString

                            LoadResultConfigure(Session("LoginSession").ToString().Split("|")(0).ToString)
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
            For Rowno = 0 To gvHelpDeskResCofig.Rows.Count - 1
                'objAptchildNodeClone.Attributes("Aoffice").InnerText = dbgrdManageSecurityRegion.Rows(Rowno).Cells(0).Text
                Dim chk As CheckBox = CType(gvHelpDeskResCofig.Rows(Rowno).FindControl("chkSelected"), CheckBox)
                chk.Checked = True
            Next Rowno
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnDeSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeSelectAll.Click
        Try
            Dim Rowno As Integer
            For Rowno = 0 To gvHelpDeskResCofig.Rows.Count - 1
                'objAptchildNodeClone.Attributes("Aoffice").InnerText = dbgrdManageSecurityRegion.Rows(Rowno).Cells(0).Text
                Dim chk As CheckBox = CType(gvHelpDeskResCofig.Rows(Rowno).FindControl("chkSelected"), CheckBox)

                If chk.Enabled = True Then
                    chk.Checked = False
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
        Try
            hdAction.Value = ""
          
            Try
                Dim objInputXml, objOutputXml As New XmlDocument
                Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                '  Dim RowNo As Long
                Try
                    If drpSelectSet.SelectedValue <> "" Then
                        If Session("LoginSession") IsNot Nothing Then
                            Dim objbzDMReportConfiguration As New AAMS.bizHelpDesk.bzDMReportConfiguration
                            objInputXml.LoadXml("<HD_VIEWDMSFIELDS_INPUT><EmployeeID /><CNFGFLAG/><SetID/></HD_VIEWDMSFIELDS_INPUT>")
                            objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0).ToString
                            objInputXml.DocumentElement.SelectSingleNode("CNFGFLAG").InnerText = "R"
                            objInputXml.DocumentElement.SelectSingleNode("SetID").InnerText = drpSelectSet.SelectedValue
                            'Here Back end Method Call
                            objOutputXml = objbzDMReportConfiguration.View(objInputXml)
                            'objOutputXml.LoadXml("<HD_GETDMRESULTCONFIGURE_OUTPUT><FIELDDETAILS DM_FIELD_ID='AGENCY_NAME'  DM_FIELD_NAME='Agency Name' SELECTED='True' /><FIELDDETAILS DM_FIELD_ID='OFFICE_ID'   DM_FIELD_NAME='Office ID' SELECTED='False' /><FIELDDETAILS DM_FIELD_ID='CUST_CATG'    DM_FIELD_NAME='Customer Category' SELECTED='True' /><FIELDDETAILS DM_FIELD_ID='CALLER_NAME'    DM_FIELD_NAME='Caller Name'  SELECTED='True' /><Errors Status='FALSE'><Error Code='' Description='' /> </Errors></HD_GETDMRESULTCONFIGURE_OUTPUT>")
                            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                                objXmlReader = New XmlNodeReader(objOutputXml)
                                ds.ReadXml(objXmlReader)
                                gvHelpDeskResCofig.DataSource = ds.Tables("FIELDDETAILS")
                                gvHelpDeskResCofig.DataBind()
                            Else
                                gvHelpDeskResCofig.DataSource = Nothing
                                gvHelpDeskResCofig.DataBind()
                                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            End If
                        End If
                    Else
                        gvHelpDeskResCofig.DataSource = Nothing
                        gvHelpDeskResCofig.DataBind()
                    End If
                Catch ex As Exception
                    lblError.Text = ex.Message
                End Try
            Catch ex As Exception

            End Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnNewSet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewSet.Click
        hdAction.Value = "I"
        drpSelectSet.SelectedValue = ""
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            ' Dim RowNo As Long
            Try
                If MaxSetCountFunction() > 10 Then
                    gvHelpDeskResCofig.DataSource = Nothing
                    gvHelpDeskResCofig.DataBind()
                    lblError.Text = "You can't make more than 10 sets"
                    Exit Sub
                End If
                If Session("LoginSession") IsNot Nothing Then
                    Dim objbzDMReportConfiguration As New AAMS.bizHelpDesk.bzDMReportConfiguration
                    objInputXml.LoadXml("<HD_VIEWDMSFIELDS_INPUT><EmployeeID /><CNFGFLAG/><SetID/></HD_VIEWDMSFIELDS_INPUT>")
                    objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0).ToString
                    objInputXml.DocumentElement.SelectSingleNode("CNFGFLAG").InnerText = "R"

                    If MaxSetCountFunction() = 0 Then
                        objInputXml.DocumentElement.SelectSingleNode("SetID").InnerText = 1
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("SetID").InnerText = MaxSetCountFunction()
                    End If

                    '  objInputXml.DocumentElement.SelectSingleNode("SetID").InnerText = ""
                    'Here Back end Method Call
                    objOutputXml = objbzDMReportConfiguration.View(objInputXml)


                    ' objOutputXml.Load("e:\HelpSearch.xml")


                    'objOutputXml.LoadXml("<HD_GETDMRESULTCONFIGURE_OUTPUT><FIELDDETAILS DM_FIELD_ID='AGENCY_NAME'  DM_FIELD_NAME='Agency Name' SELECTED='True' /><FIELDDETAILS DM_FIELD_ID='OFFICE_ID'   DM_FIELD_NAME='Office ID' SELECTED='False' /><FIELDDETAILS DM_FIELD_ID='CUST_CATG'    DM_FIELD_NAME='Customer Category' SELECTED='True' /><FIELDDETAILS DM_FIELD_ID='CALLER_NAME'    DM_FIELD_NAME='Caller Name'  SELECTED='True' /><Errors Status='FALSE'><Error Code='' Description='' /> </Errors></HD_GETDMRESULTCONFIGURE_OUTPUT>")
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        objXmlReader = New XmlNodeReader(objOutputXml)
                        ds.ReadXml(objXmlReader)
                        gvHelpDeskResCofig.DataSource = ds.Tables("FIELDDETAILS")
                        gvHelpDeskResCofig.DataBind()
                    Else
                        gvHelpDeskResCofig.DataSource = Nothing
                        gvHelpDeskResCofig.DataBind()
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
End Class