
Partial Class Sales_SAUP_PREV_VisitedItems
    Inherits System.Web.UI.Page
#Region "Global Variable Declaration"
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strDSRDate As String = ""
        Try
            Session("PageName") = Request.Url.ToString()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            ddlServiceStatus.Attributes.Add("OnChange", "return ValidateServiceStatus('ddlServiceStatus');")

            If Not IsPostBack Then
                Session("OpenItemsVisitDetails") = Nothing

                'If Request.QueryString("DSR_VISIT_ID") IsNot Nothing AndAlso Request.QueryString("VisitDate") IsNot Nothing AndAlso Request.QueryString("LCODE") IsNot Nothing Then
                If Request.QueryString("VisitDate") IsNot Nothing Then
                    strDSRDate = Request.QueryString("VisitDate").ToString
                    hdVisitDATE.Value = strDSRDate
                End If

                If Request.QueryString("LCODE") IsNot Nothing Then
                    hdLCode.Value = Request.QueryString("LCODE").ToString
                End If

                If Request.QueryString("DSR_VISIT_ID") IsNot Nothing Then
                    hdID.Value = Request.QueryString("DSR_VISIT_ID").ToString
                End If


                rbPrevVisitItems.Checked = True
                objeAAMS.BindDropDown(ddlServiceStatus, "ServiceCallStatus", True, "1")
                ViewDetails()
                ' PreviousRemarks()
                pnlServiceCallDetails.Visible = True
                pnlRemarks.Visible = True

                'btnRemarks.Enabled = False
            End If
            ' Checking security.
            ' CheckSecurity()

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


   

#Region "ViewDetails()"
    Private Sub ViewDetails()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objSales As New AAMS.bizSales.bzDSR
        Dim strDSR_SC_DETAIL_ID As String
        Try
            hdDSR_DETAIL_ID.Value = ""
            objInputXml.LoadXml("<SL_DSR_SERVICECALLS_DETAILS_INPUT><PREDATE/><LCODE/><LOGINID/><PREVITEMTYPE/></SL_DSR_SERVICECALLS_DETAILS_INPUT>")

            If rbPrevVisitItems.Checked Then
                objInputXml.DocumentElement.SelectSingleNode("PREVITEMTYPE").InnerText = "1"
            Else
                objInputXml.DocumentElement.SelectSingleNode("PREVITEMTYPE").InnerText = "2"
            End If

            If Request.QueryString("DSR_VISIT_ID") IsNot Nothing AndAlso Request.QueryString("VisitDate") IsNot Nothing AndAlso Request.QueryString("LCODE") IsNot Nothing Then
                hdDSR_DETAIL_ID.Value = Request.QueryString("DSR_VISIT_ID")
                objInputXml.DocumentElement.SelectSingleNode("PREDATE").InnerText = Request.QueryString("VisitDate")
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = Request.QueryString("LCODE")
                objInputXml.DocumentElement.SelectSingleNode("LOGINID").InnerText = objeAAMS.EmployeeID(Session("Security"))
                '  objInputXml.Save("c:\admin\aams\Service_Call_Previous_DetailsInput.xml")
                objOutputXml = objSales.Service_Call_Previous_Details(objInputXml)

                ' objOutputXml.Save("c:\admin\aams\Service_Call_Previous_DetailsOutPut.xml")
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    Session("OpenItemsVisitDetails") = objOutputXml.OuterXml
                    'Code Start For Visit Details
                    BindGrid(objOutputXml, "SERVICECALL")

                    If hdDSR_DETAIL_ID.Value <> "" Then
                        If objOutputXml.DocumentElement.SelectSingleNode("SERVICECALL[@DSR_VISIT_ID='" + hdDSR_DETAIL_ID.Value + "']") IsNot Nothing Then
                            strDSR_SC_DETAIL_ID = objOutputXml.DocumentElement.SelectSingleNode("SERVICECALL[@DSR_VISIT_ID='" + hdDSR_DETAIL_ID.Value + "']").Attributes("DSR_SC_DETAIL_ID").InnerText
                            ClearControls()
                            AutoPopulateVisitDetail(strDSR_SC_DETAIL_ID)
                        Else
                            ClearControls()
                        End If
                    Else
                        ClearControls()

                    End If

                Else
                    ClearControls()
                    gvVisitItemsServiceCall.DataSource = Nothing
                    gvVisitItemsServiceCall.DataBind()
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

                    'btnRemarks.Enabled = False
                End If
                '  PreviousRemarks()
            End If

            '  objOutputXml.Save("c:\Service_Call_Previous_Details.xml")

            '<SERVICECALLS_DETAILS_OUTPUT>
            '  <DSR PREDATE="20110510" DSR_VISIT_ID="" EMPLOYEEID='' LCODE=''/>
            '  <PREVIOUS_SERVICE_REMARKS>
            '    <SERVICE_REMARKS DATE='' CHANGEDATA='' />
            '  </PREVIOUS_SERVICE_REMARKS>
            '  <SERVICECALL SEQUENCENO='' LOGDATE='' DEPARTMENTID='' DEPARTMENT_NAME='' DEPARTMENT_SPECIFIC=''
            '  SC_DISCUSSIONISSUE_REMARKS='' SC_STATUSID='' SC_STATUSID_NAME='' ASSIGNTO_EMPLOYEEID='' ASSIGNTO_EMPLOYEE_NAME=''
            '  SC_COMPETITION_MKT_INFO_REMARKS='' CLOSER_DATETIME='' TARGET_CLOSER_DATETIME='' SC_FOLLOWUP_REMARKS=''
            '  DSR_SC_DETAIL_ID='' DSR_VISIT_ID=''  />
            '  <Errors Status=''>
            '    <Error Code='' Description='' />
            '  </Errors>
            '</SERVICECALLS_DETAILS_OUTPUT>


            'objOutputXml.Load("C:\Sales.xml")


        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
#End Region


#Region "BindGrid(ByVal objViewXml As XmlDocument, ByVal strGridType As String)"
    Private Sub BindGrid(ByVal objViewXml As XmlDocument, ByVal strGridType As String)
        Dim objXmlReader As XmlNodeReader
        Dim objDS As New DataSet
        Dim objDV As DataView
        Try
            objXmlReader = New XmlNodeReader(objViewXml)
            objDS.ReadXml(objXmlReader)
            Select Case strGridType
                Case "SERVICECALL"
                    objDV = objDS.Tables("SERVICECALL").DefaultView
                    If objDV.Count > 0 Then
                        gvVisitItemsServiceCall.DataSource = objDV
                    Else
                        gvVisitItemsServiceCall.DataSource = Nothing
                    End If
                    gvVisitItemsServiceCall.DataBind()
            End Select
        Catch ex As Exception
            lblError.Text = ex.Message
            Throw ex
        End Try
    End Sub
#End Region

#Region "function to AutoPopolate Visit Details"
    Sub AutoPopulateVisitDetail(ByVal strVisitDetailId As String)
        Dim strCommandArgs As String = ""
        Dim objXmlDoc As New XmlDocument
        Dim objDetailsNode As XmlNode
        Try
            strCommandArgs = strVisitDetailId
            If strCommandArgs <> "" Then
                objXmlDoc.LoadXml(Session("OpenItemsVisitDetails"))
                objDetailsNode = objXmlDoc.DocumentElement.SelectSingleNode("SERVICECALL[@DSR_SC_DETAIL_ID='" + strCommandArgs + "']")
                If objDetailsNode IsNot Nothing Then
                    With objDetailsNode
                        hdDSR_SC_DETAIL_ID.Value = .Attributes("DSR_SC_DETAIL_ID").Value
                        hdDSR_DETAIL_ID.Value = .Attributes("DSR_VISIT_ID").Value
                        txtDepartment.Text = .Attributes("DEPARTMENT_NAME").Value
                        ddlServiceStatus.SelectedValue = .Attributes("SC_STATUSID").Value

                        txtDetailedDiscussion.Text = .Attributes("SC_DISCUSSIONISSUE_REMARKS").Value
                        txtAssignedTo.Text = .Attributes("ASSIGNTO_EMPLOYEE_NAME").Value
                        txtCloserDate.Text = objeAAMS.GetDateFormat(.Attributes("CLOSER_DATETIME").Value, "yyyyMMdd", "dd/MM/yyyy", "/")
                        hdCloserDate.Value = objeAAMS.GetDateFormat(.Attributes("CLOSER_DATETIME").Value, "yyyyMMdd", "dd/MM/yyyy", "/")

                        If .Attributes("SC_STATUSID").Value.Split("|")(1) = "1" Then
                            If (.Attributes("LOGDATE").Value) = objeAAMS.GetDateFormat(Format(Today, "dd/MM/yyyy"), "dd/MM/yyyy", "yyyyMMdd", "/") Then
                                ddlServiceStatus.Enabled = True
                            Else
                                ddlServiceStatus.Enabled = False
                            End If
                        ElseIf .Attributes("SC_STATUSID").Value.Split("|")(1) = "0" Then
                            hdCloserDate.Value = ""
                            txtCloserDate.Text = ""
                            ddlServiceStatus.Enabled = True
                        End If
                        '  txtCompetitionInfo.Text = .Attributes("SC_COMPETITION_MKT_INFO_REMARKS").Value
                        txtTargetCloserDate.Text = objeAAMS.GetDateFormat(.Attributes("TARGET_CLOSER_DATETIME").Value, "yyyyMMdd", "dd/MM/yyyy", "/")
                        txtDepttSpecific.Text = .Attributes("DEPARTMENT_SPECIFIC").Value
                        'txtFollowUpRemarks.Text = .Attributes("SC_FOLLOWUP_REMARKS").Value
                        'btnRemarks.Enabled = True
                    End With
                Else
                    hdDSR_DETAIL_ID.Value = ""
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region


    Protected Sub gvVisitItemsServiceCall_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvVisitItemsServiceCall.RowCommand
        Dim strCommandArgs As String = ""
        Dim objXmlDoc As New XmlDocument
        Dim objDetailsNode As XmlNode
        Try
            '<SERVICECALL SEQUENCENO='' LOGDATE='' DEPARTMENTID='' DEPARTMENT_NAME='' DEPARTMENT_SPECIFIC=''
            'SC_DISCUSSIONISSUE_REMARKS='' SC_STATUSID='' SC_STATUSID_NAME='' ASSIGNTO_EMPLOYEEID='' ASSIGNTO_EMPLOYEE_NAME=''
            'SC_COMPETITION_MKT_INFO_REMARKS='' CLOSER_DATETIME='' TARGET_CLOSER_DATETIME='' SC_FOLLOWUP_REMARKS=''
            'DSR_STR_DETAIL_ID='' />
            If e.CommandName = "EditX" Then
                strCommandArgs = e.CommandArgument
                objXmlDoc.LoadXml(Session("OpenItemsVisitDetails"))
                objDetailsNode = objXmlDoc.DocumentElement.SelectSingleNode("SERVICECALL[@DSR_SC_DETAIL_ID='" + strCommandArgs + "']")
                If objDetailsNode IsNot Nothing Then
                    With objDetailsNode
                        hdDSR_SC_DETAIL_ID.Value = .Attributes("DSR_SC_DETAIL_ID").Value
                        hdDSR_DETAIL_ID.Value = .Attributes("DSR_VISIT_ID").Value
                        txtDepartment.Text = .Attributes("DEPARTMENT_NAME").Value
                        ddlServiceStatus.SelectedValue = .Attributes("SC_STATUSID").Value

                        txtDetailedDiscussion.Text = .Attributes("SC_DISCUSSIONISSUE_REMARKS").Value
                        txtAssignedTo.Text = .Attributes("ASSIGNTO_EMPLOYEE_NAME").Value
                        txtCloserDate.Text = objeAAMS.GetDateFormat(.Attributes("CLOSER_DATETIME").Value, "yyyyMMdd", "dd/MM/yyyy", "/")
                        hdCloserDate.Value = objeAAMS.GetDateFormat(.Attributes("CLOSER_DATETIME").Value, "yyyyMMdd", "dd/MM/yyyy", "/")

                        If .Attributes("SC_STATUSID").Value.Split("|")(1) = "1" Then
                            If (.Attributes("LOGDATE").Value) = objeAAMS.GetDateFormat(Format(Today, "dd/MM/yyyy"), "dd/MM/yyyy", "yyyyMMdd", "/") Then
                                ddlServiceStatus.Enabled = True
                            Else
                                ddlServiceStatus.Enabled = False
                            End If
                        ElseIf .Attributes("SC_STATUSID").Value.Split("|")(1) = "0" Then
                            hdCloserDate.Value = ""
                            txtCloserDate.Text = ""
                            ddlServiceStatus.Enabled = True
                        End If
                        ' txtCompetitionInfo.Text = .Attributes("SC_COMPETITION_MKT_INFO_REMARKS").Value
                        txtTargetCloserDate.Text = objeAAMS.GetDateFormat(.Attributes("TARGET_CLOSER_DATETIME").Value, "yyyyMMdd", "dd/MM/yyyy", "/")
                        txtDepttSpecific.Text = .Attributes("DEPARTMENT_SPECIFIC").Value
                        '   txtFollowUpRemarks.Text = .Attributes("SC_FOLLOWUP_REMARKS").Value
                        '  btnRemarks.Enabled = True
                    End With
                Else
                    'btnRemarks.Enabled = False
                    hdDSR_DETAIL_ID.Value = ""
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvVisitItemsServiceCall_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvVisitItemsServiceCall.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim LnkSCFRem As New LinkButton
                LnkSCFRem = CType(e.Row.FindControl("LnkSCFRem"), LinkButton)


                Dim txtSCIssueReported As New TextBox
                txtSCIssueReported = CType(e.Row.FindControl("txtSCIssueReported"), TextBox)

                If txtSCIssueReported.Text.Trim.Length > 50 Then
                    Dim strIssueReport As String = Left(txtSCIssueReported.Text, 50)
                    txtSCIssueReported.Text = strIssueReport + "..."
                End If

                Dim HdDSR_VISIT_ID As HiddenField
                HdDSR_VISIT_ID = e.Row.FindControl("HdDSR_VISIT_ID")
                
              
                Dim DSR_SC_DETAIL_ID As String = ""
                DSR_SC_DETAIL_ID = LnkSCFRem.CommandArgument.ToString
                Dim HdSC_STATUSID As HiddenField
                HdSC_STATUSID = e.Row.FindControl("HdSC_STATUSID")

                If DSR_SC_DETAIL_ID.Trim.Length > 0 Then
                    LnkSCFRem.Enabled = True
                    LnkSCFRem.Attributes.Add("onclick", "return EditServiceCallFolowupRem('" + DSR_SC_DETAIL_ID + "','" + HdDSR_VISIT_ID.Value + "','" + HdSC_STATUSID.Value + "')")
                Else
                    LnkSCFRem.Attributes.Clear()
                    LnkSCFRem.Enabled = False
                End If


                e.Row.Cells(0).Text = e.Row.RowIndex + 1
                If e.Row.Cells(1).Text.Trim.Length = 8 Then
                    e.Row.Cells(1).Text = objeAAMS.GetDateFormat(e.Row.Cells(1).Text, "yyyyMMdd", "dd/MM/yyyy", "/")
                End If
                If e.Row.Cells(7).Text.Trim.Length = 8 Then
                    e.Row.Cells(7).Text = objeAAMS.GetDateFormat(e.Row.Cells(7).Text, "yyyyMMdd", "dd/MM/yyyy", "/")
                End If
                If e.Row.Cells(8).Text.Trim.Length = 8 Then
                    e.Row.Cells(8).Text = objeAAMS.GetDateFormat(e.Row.Cells(8).Text, "yyyyMMdd", "dd/MM/yyyy", "/")
                End If

                Dim lnkEdit As New LinkButton
                lnkEdit = CType(e.Row.FindControl("lnkEdit"), LinkButton)

             


                If HdSC_STATUSID.Value.Split("|")(1) = "1" Then
                    'LnkSCFRem.Enabled = False
                    lnkEdit.Enabled = False
                    'LnkSCFRem.Attributes.Clear()
                    lnkEdit.Attributes.Clear()
                End If


            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Sub ClearControls()
        Try
            ddlServiceStatus.SelectedIndex = 0
            txtDepartment.Text = ""
            txtDepttSpecific.Text = ""
            txtDetailedDiscussion.Text = ""
            txtAssignedTo.Text = ""
            txtCloserDate.Text = ""
            txtTargetCloserDate.Text = ""
            ' txtCompetitionInfo.Text = ""
            'txtFollowUpRemarks.Text = ""
            hdCloserDate.Value = ""

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub rbPrevVisitItems_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbPrevVisitItems.CheckedChanged
        Try
            ViewDetails()
            pnlServiceCallDetails.Visible = True
            pnlRemarks.Visible = True
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub rbPrevVisitOpenItems_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbPrevVisitOpenItems.CheckedChanged
        Try
            ViewDetails()
            pnlServiceCallDetails.Visible = True
            pnlRemarks.Visible = True
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objInputXml, objFinalInputXml, objOutputXml As New XmlDocument
        Dim objDSR As New AAMS.bizSales.bzDSR
        Dim objServiceNode As XmlNode
        Dim strStatusID As String = ""
        Try
            objFinalInputXml.LoadXml("<SERVICECALLS_DETAILS_OUTPUT></SERVICECALLS_DETAILS_OUTPUT>")
            ' Load Input Xml.
            If Session("OpenItemsVisitDetails") IsNot Nothing AndAlso hdDSR_SC_DETAIL_ID.Value.Trim.Length > 0 Then
                objInputXml.LoadXml(Session("OpenItemsVisitDetails").ToString())

                objServiceNode = objInputXml.DocumentElement.SelectSingleNode("SERVICECALL[@DSR_SC_DETAIL_ID=" + hdDSR_SC_DETAIL_ID.Value.Trim + "]")
                If Not objServiceNode Is Nothing Then
                    objServiceNode.Attributes("SC_STATUSID").Value = ddlServiceStatus.SelectedValue.Split("|").GetValue(0)
                    strStatusID = ddlServiceStatus.SelectedValue.Split("|").GetValue(0) & "|" & ddlServiceStatus.SelectedValue.Split("|").GetValue(1)

                    objServiceNode.Attributes("SC_STATUSID_NAME").Value = ddlServiceStatus.SelectedItem.Text.Trim

                    If ddlServiceStatus.SelectedValue.Split("|").GetValue(1) = "0" Then
                        hdCloserDate.Value = ""
                    End If

                    If hdCloserDate.Value <> "" Then
                        objServiceNode.Attributes("CLOSER_DATETIME").Value = objeAAMS.GetDateFormat(hdCloserDate.Value, "dd/MM/yyyy", "yyyyMMdd", "/")
                    Else
                        objServiceNode.Attributes("CLOSER_DATETIME").Value = ""
                    End If
                    'objServiceNode.Attributes("SC_COMPETITION_MKT_INFO_REMARKS").Value = txtCompetitionInfo.Text.Trim
                    ' objServiceNode.Attributes("SC_FOLLOWUP_REMARKS").Value = txtFollowUpRemarks.Text.Trim

                End If

                objInputXml.DocumentElement.SelectSingleNode("DSR").Attributes("EMPLOYEEID").Value = Session("LoginSession").ToString().Split("|")(0)

                objFinalInputXml.DocumentElement.AppendChild(objFinalInputXml.ImportNode(objInputXml.DocumentElement.SelectSingleNode("DSR"), False))
                objFinalInputXml.DocumentElement.AppendChild(objFinalInputXml.ImportNode(objServiceNode, False))

                objOutputXml = objDSR.UpdateService_Call_Previous_Details(objFinalInputXml)
                'objInputXml.DocumentElement.SelectSingleNode("DSR")

                objInputXml.DocumentElement.SelectSingleNode("SERVICECALL[@DSR_SC_DETAIL_ID=" + hdDSR_SC_DETAIL_ID.Value.Trim + "]").Attributes("SC_STATUSID").InnerText = strStatusID.ToString


                '   Checking error status. 
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    hdDSR_SC_DETAIL_ID.Value = ""
                    'Binding Visit Details Data Grid
                    BindGrid(objInputXml, "SERVICECALL")
                    Session("OpenItemsVisitDetails") = objInputXml.OuterXml
                    'Clear Controls 
                    ClearControls()
                    lblError.Text = objeAAMSMessage.messUpdate
                Else
                    hdDSR_SC_DETAIL_ID.Value = ""
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

                    gvVisitItemsServiceCall.DataSource = Nothing
                    gvVisitItemsServiceCall.DataBind()
                    ClearControls()
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Clear Controls 
        Try
            ClearControls()
            ViewDetails()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    'Protected Sub btnRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemarks.Click
    '    Try
    '        'pnlServiceCallDetails.Visible = False
    '        'pnlRemarks.Visible = True
    '        PreviousRemarks()
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '    End Try
    'End Sub

    'Protected Sub btnDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDetails.Click
    '    Try
    '        pnlServiceCallDetails.Visible = True
    '        pnlRemarks.Visible = True
    '        ViewDetails()
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '    End Try
    'End Sub

#Region "PreviousRemarks()"
    Private Sub PreviousRemarks()
        Dim ds As New DataSet
        Dim objXmlReader As XmlNodeReader
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objDSR As New AAMS.bizSales.bzDSR
        Try
            objInputXml.LoadXml("<UP_SL_REMARKS_INPUT><DSR_VISIT_ID/><REMARKS_TYPE/><PREDATE/><LCODE/></UP_SL_REMARKS_INPUT>")
            If Val(hdDSR_DETAIL_ID.Value) > 0 Then
                objInputXml.DocumentElement.SelectSingleNode("DSR_VISIT_ID").InnerText = hdDSR_DETAIL_ID.Value
                objInputXml.DocumentElement.SelectSingleNode("REMARKS_TYPE").InnerText = "1" '1 for service call
                objInputXml.DocumentElement.SelectSingleNode("PREDATE").InnerText = Request.QueryString("VisitDate")
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = Request.QueryString("LCODE")

                'Here Back end Method Call
                objOutputXml = objDSR.GetDSRRemark(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    gvRemarks.DataSource = ds.Tables("REMARKS")
                    gvRemarks.DataBind()
                Else
                    gvRemarks.DataSource = Nothing
                    gvRemarks.DataBind()
                End If
            Else
                gvRemarks.DataSource = Nothing
                gvRemarks.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
End Class


