
Partial Class Sales_SASR_IssueReported
    Inherits System.Web.UI.Page

    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Protected strDSRDate As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            lblError.Text = "Session is expired."
            Exit Sub
        End If
        BtnSave.Attributes.Add("onclick", "return MandatoryData();")
        lblError.Text = ""
        If Not IsPostBack Then
            lblHeading.Text = ""
            If Request.QueryString("VisitDate") IsNot Nothing Then
                hdVisitDATE.Value = Request.QueryString("VisitDate").ToString
                strDSRDate = hdVisitDATE.Value
                strDSRDate = strDSRDate.Substring(6, 2) + "-" + MonthName(strDSRDate.Substring(4, 2)) + "-" + strDSRDate.Substring(0, 4)
            End If
            If Request.QueryString("Lcode") IsNot Nothing Then
                hdLCode.Value = Request.QueryString("Lcode").ToString
            End If
            If Request.QueryString("DSR_VISIT_ID") IsNot Nothing Then
                hdDSR_VISIT_ID.Value = Request.QueryString("DSR_VISIT_ID").ToString
            End If
            If hdDSR_VISIT_ID.Value.Trim.Length = 0 Then
                '  BtnSave.Enabled = False
            End If

            If Request.QueryString("REMARKS_TYPE") IsNot Nothing Then
                If Request.QueryString("REMARKS_TYPE").ToString.Trim.ToUpper = "SC" Then
                    lblHeading.Text = "Service Call Detailed Disc / Issue Reported"
                    HdVisitType.Value = "SC"
                End If
                If Request.QueryString("REMARKS_TYPE").ToString.Trim.ToUpper = "RETENTION" Then
                    lblHeading.Text = "Retention Detailed Disc / Issue Reported"
                    HdVisitType.Value = "RETENTION"
                End If
                If Request.QueryString("REMARKS_TYPE").ToString.Trim.ToUpper = "TARGET" Then
                    lblHeading.Text = "TARGET Detailed Disc / Issue Reported"
                    HdVisitType.Value = "TARGET"
                End If
                If Request.QueryString("REMARKS_TYPE").ToString.Trim.ToUpper = "AIRNONAIR" Then
                    lblHeading.Text = "Air Non Air Detailed Disc / Issue Reported"
                    HdVisitType.Value = "AIRNONAIR"
                End If
                Me.Title = lblHeading.Text
                BindData()
            End If
        End If
    End Sub

    Private Sub BindData()
        Dim ds As New DataSet
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objDSR As New AAMS.bizSales.bzDSR
        Dim strVisitType As String = ""
        Dim objXmlReader As XmlNodeReader
        Dim Predaate As String = ""
        Try
            If HdVisitType.Value.Trim = "SC" Then
                '  objInputXml.LoadXml("<UP_VIEW_SL_STRATEGIC_CALL_AIR_ISSUEREPORT_INPUT>	<DSR_SC_DETAIL_ID />	<DSR_VISIT_ID />	<PREDATE />	<LCODE /><RESP_1A/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></UP_VIEW_SL_STRATEGIC_CALL_AIR_ISSUEREPORT_INPUT>")
            ElseIf HdVisitType.Value.Trim = "RETENTION" Then
                objInputXml.LoadXml("<UP_VIEW_SL_STRATEGIC_CALL_RETENTION_ISSUEREPORT_INPUT>	<DSR_SC_DETAIL_ID />	<DSR_VISIT_ID />	<PREDATE />	<LCODE /><RESP_1A/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></UP_VIEW_SL_STRATEGIC_CALL_RETENTION_ISSUEREPORT_INPUT>")
            ElseIf HdVisitType.Value.Trim = "TARGET" Then
                objInputXml.LoadXml("<UP_VIEW_SL_STRATEGIC_CALL_TARGET_ISSUEREPORT_INPUT>	<DSR_SC_DETAIL_ID />	<DSR_VISIT_ID />	<PREDATE />	<LCODE /><RESP_1A/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></UP_VIEW_SL_STRATEGIC_CALL_TARGET_ISSUEREPORT_INPUT>")
            ElseIf HdVisitType.Value.Trim = "AIRNONAIR" Then
                objInputXml.LoadXml("<UP_VIEW_SL_STRATEGIC_CALL_AIR_ISSUEREPORT_INPUT>	<DSR_SC_DETAIL_ID />	<DSR_VISIT_ID />	<PREDATE />	<LCODE /><RESP_1A/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></UP_VIEW_SL_STRATEGIC_CALL_AIR_ISSUEREPORT_INPUT>")
            End If

            objInputXml.DocumentElement.SelectSingleNode("DSR_SC_DETAIL_ID").InnerText = ""

            objInputXml.DocumentElement.SelectSingleNode("DSR_VISIT_ID").InnerText = hdDSR_VISIT_ID.Value

            objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdLCode.Value
            objInputXml.DocumentElement.SelectSingleNode("PREDATE").InnerText = hdVisitDATE.Value
            Dim objEmpXml As New XmlDocument
            Dim UserId As String
            objEmpXml.LoadXml(Session("Security"))
            UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
            objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = UserId

            '@ Coding For Paging Ansd sorting
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
                ViewState("SortName") = "DATETIME"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "DATETIME" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If


            If HdVisitType.Value.Trim = "SC" Then
                'Dim bzServiceCallStatus As New AAMS.bizSales.bzServiceCallStatus
                'objOutputXml = bzServiceCallStatus.View_ServiceCall_CompInfo(objInputXml)
            ElseIf HdVisitType.Value.Trim = "RETENTION" Then
                Dim ObjbzStrategicVisitRetentionStatus As New AAMS.bizSales.bzStrategicVisitRetentionStatus
                objOutputXml = ObjbzStrategicVisitRetentionStatus.VIEW_STRATEGIC_CALL_RETENTION_FOLLOWUP_ISSUEREPORT(objInputXml)
            ElseIf HdVisitType.Value.Trim = "TARGET" Then
                Dim ObjbzStrategicVisitTargetStatus As New AAMS.bizSales.bzStrategicVisitTargetStatus
                objOutputXml = ObjbzStrategicVisitTargetStatus.VIEWSTRATEGIC_CALL_TARGET_VISIT_FOLLOWUP_ISSUEREPORT(objInputXml)

            ElseIf HdVisitType.Value.Trim = "AIRNONAIR" Then
                Dim ObjbzStrategicVisitAirNonAir As New AAMS.bizSales.bzStrategicVisitAirNonAir
                objOutputXml = ObjbzStrategicVisitAirNonAir.VIEW_STRATEGIC_CALL_AIR_FOLLOWUP_ISSUEREPORT(objInputXml)

            End If

            'Here Back end Method Call          
            'objOutputXml.Save("c:\admin\aams\DSRMarketInfoOut.xml")
            Try
                objInputXml.Save("c:\admin\DSRIssueReportViewInput.xml")
                objOutputXml.Save("c:\admin\DSRIssueReportViewOut.xml")
            Catch ex As Exception
            End Try


            '@ Start of Adding Temporary Item 
            If hdDSR_VISIT_ID.Value.Trim.Length = 0 Then
                Try
                    Dim objAddXmlDoc As New XmlDocument
                    If Session("VisitDetails") IsNot Nothing Then
                        objAddXmlDoc.LoadXml(Session("VisitDetails"))
                        Try
                            Predaate = hdVisitDATE.Value
                            Predaate = Predaate.Substring(6, 2) + "-" + MonthName(Predaate.Substring(4, 2)) + "-" + Predaate.Substring(0, 4)

                        Catch ex As Exception

                        End Try

                        If HdVisitType.Value.Trim = "AIRNONAIR" Then
                            If objAddXmlDoc.DocumentElement.SelectNodes("STRATEGIC_CALL_AIR_OTHER/ISSURE_REPORT_AIR_OTHER[@DSR_VISIT_ID='']").Count > 0 Then
                                For Each objnodeAirNonAirIssueReport As XmlNode In objAddXmlDoc.DocumentElement.SelectNodes("STRATEGIC_CALL_AIR_OTHER/ISSURE_REPORT_AIR_OTHER[@DSR_VISIT_ID='']")
                                    Dim objTempdocument As New XmlDocument
                                    objTempdocument.LoadXml("<Temp><DETAILS RESP_1A='' RESP_1A_NAME='' ISSUEREPORT_REMARKS='' DATETIME=''/></Temp>")

                                    Dim objnode As XmlNode = objTempdocument.DocumentElement.SelectSingleNode("DETAILS")

                                    For Each objattributes As XmlAttribute In objnode.Attributes
                                        objattributes.Value = ""
                                    Next

                                    UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                                    objnode.Attributes("RESP_1A").Value = UserId
                                    objnode.Attributes("RESP_1A_NAME").Value = objEmpXml.DocumentElement.SelectSingleNode("Employee_Name").InnerText.Trim()
                                    objnode.Attributes("ISSUEREPORT_REMARKS").Value = objnodeAirNonAirIssueReport.Attributes("STA_DISCUSSIONISSUE_REMARKS").Value
                                    objnode.Attributes("DATETIME").Value = Predaate
                                    objOutputXml.DocumentElement.AppendChild(objOutputXml.ImportNode(objnode, True))

                                    If objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value.Trim.Length = 0 Then
                                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value = "1"

                                    End If
                                    objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value = CInt(Val(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value)) + 1

                                Next
                            End If
                        End If
                        If HdVisitType.Value.Trim = "RETENTION" Then
                            If objAddXmlDoc.DocumentElement.SelectNodes("STRATEGIC_CALL_RETENTION/ISSURE_REPORT_RETENTION[@DSR_VISIT_ID='']").Count > 0 Then
                                For Each objnodeAirNonAirIssueReport As XmlNode In objAddXmlDoc.DocumentElement.SelectNodes("STRATEGIC_CALL_RETENTION/ISSURE_REPORT_RETENTION[@DSR_VISIT_ID='']")
                                    Dim objTempdocument As New XmlDocument
                                    objTempdocument.LoadXml("<Temp><DETAILS RESP_1A='' RESP_1A_NAME='' ISSUEREPORT_REMARKS='' DATETIME=''/></Temp>")

                                    Dim objnode As XmlNode = objTempdocument.DocumentElement.SelectSingleNode("DETAILS")

                                    For Each objattributes As XmlAttribute In objnode.Attributes
                                        objattributes.Value = ""
                                    Next
                                    UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                                    objnode.Attributes("RESP_1A").Value = UserId
                                    objnode.Attributes("RESP_1A_NAME").Value = objEmpXml.DocumentElement.SelectSingleNode("Employee_Name").InnerText.Trim()
                                    objnode.Attributes("ISSUEREPORT_REMARKS").Value = objnodeAirNonAirIssueReport.Attributes("STR_DISCUSSIONISSUE_REMARKS").Value
                                    objnode.Attributes("DATETIME").Value = Predaate
                                    objOutputXml.DocumentElement.AppendChild(objOutputXml.ImportNode(objnode, True))

                                    If objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value.Trim.Length = 0 Then
                                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value = "1"

                                    End If
                                    objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value = CInt(Val(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value)) + 1


                                Next
                            End If
                        End If
                        If HdVisitType.Value.Trim = "TARGET" Then
                            If objAddXmlDoc.DocumentElement.SelectNodes("STRATEGIC_CALL_TARGET/ISSURE_REPORT_TARGET[@DSR_VISIT_ID='']").Count > 0 Then
                                For Each objnodeAirNonAirIssueReport As XmlNode In objAddXmlDoc.DocumentElement.SelectNodes("STRATEGIC_CALL_TARGET/ISSURE_REPORT_TARGET[@DSR_VISIT_ID='']")
                                    Dim objTempdocument As New XmlDocument
                                    objTempdocument.LoadXml("<Temp><DETAILS RESP_1A='' RESP_1A_NAME='' ISSUEREPORT_REMARKS='' DATETIME=''/></Temp>")

                                    Dim objnode As XmlNode = objTempdocument.DocumentElement.SelectSingleNode("DETAILS")

                                    For Each objattributes As XmlAttribute In objnode.Attributes
                                        objattributes.Value = ""
                                    Next
                                    UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                                    objnode.Attributes("RESP_1A").Value = UserId
                                    objnode.Attributes("RESP_1A_NAME").Value = objEmpXml.DocumentElement.SelectSingleNode("Employee_Name").InnerText.Trim()
                                    objnode.Attributes("ISSUEREPORT_REMARKS").Value = objnodeAirNonAirIssueReport.Attributes("STT_DISCUSSIONISSUE_REMARKS").Value
                                    objnode.Attributes("DATETIME").Value = Predaate
                                    objOutputXml.DocumentElement.AppendChild(objOutputXml.ImportNode(objnode, True))

                                    If objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value.Trim.Length = 0 Then
                                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value = "1"

                                    End If
                                    objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value = CInt(Val(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value)) + 1

                                Next
                            End If
                        End If



                    End If
                Catch ex As Exception

                End Try
                ' @ End of adding temporary item
            End If
         

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvRemarks.DataSource = ds.Tables("DETAILS")
                gvRemarks.DataBind()

                ' ##################################################################
                '  @ Code Added For Paging And Sorting In case Of 
                ' ###################################################################


                BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                SetImageForSorting(gvRemarks)
                '  ###################################################################
                ' @ End of Code Added For Paging And Sorting In case 
                ' ###################################################################

                pnlPaging.Visible = True

            Else
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)

                If ds.Tables("DETAILS") IsNot Nothing Then
                    If ds.Tables("DETAILS").Rows.Count > 0 Then
                        gvRemarks.DataSource = ds.Tables("DETAILS")
                        gvRemarks.DataBind()

                        ' ##################################################################
                        '  @ Code Added For Paging And Sorting In case Of 
                        ' ###################################################################
                        BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                        txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                        SetImageForSorting(gvRemarks)
                        '  ###################################################################
                        ' @ End of Code Added For Paging And Sorting In case 
                        ' ###################################################################

                        pnlPaging.Visible = True
                    Else
                        gvRemarks.DataSource = Nothing
                        gvRemarks.DataBind()
                        If objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value.Trim.ToUpper = "NO RECORD FOUND!" Then

                        Else
                            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                        End If
                        pnlPaging.Visible = False
                        txtTotalRecordCount.Text = ""
                    End If
                Else
                    gvRemarks.DataSource = Nothing
                    gvRemarks.DataBind()
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value.Trim.ToUpper = "NO RECORD FOUND!" Then

                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                    pnlPaging.Visible = False
                    txtTotalRecordCount.Text = ""

                End If
               

            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            txtTotalRecordCount.Text = ""
            pnlPaging.Visible = False
        End Try
    End Sub

    Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Dim ds As New DataSet
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objAddXmlDoc As New XmlDocument
        Dim objDSR As New AAMS.bizSales.bzDSR
        Dim strVisitType As String = ""
        Try
            If hdDSR_VISIT_ID.Value.Trim.Length > 0 Then
                'Here Back end Method Call
                If HdVisitType.Value.Trim = "SC" Then
                    'Dim bzServiceCallStatus As New AAMS.bizSales.
                    'objOutputXml = bzServiceCallStatus.View_ServiceCall_CompInfo(objInputXml)
                ElseIf HdVisitType.Value.Trim = "RETENTION" Then
                    Dim ObjbzStrategicVisitRetentionStatus As New AAMS.bizSales.bzStrategicVisitRetentionStatus

                    objInputXml.LoadXml("<UP_UPDATE_SL_STRATEGIC_CALL_RETENTION_ISSUEREPORT_INPUT>	<DETAILS STR_DISCUSSIONISSUE_REMARKS='' RESP_1A='' DSR_VISIT_ID ='' LCODE ='' PREDATE=''   /></UP_UPDATE_SL_STRATEGIC_CALL_RETENTION_ISSUEREPORT_INPUT>")
                    With objInputXml.DocumentElement.SelectSingleNode("DETAILS")
                        .Attributes("STR_DISCUSSIONISSUE_REMARKS").Value = txtDetailedDiscussion.Text
                        Dim objEmpXml As New XmlDocument
                        Dim UserId As String
                        objEmpXml.LoadXml(Session("Security"))
                        UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                        .Attributes("RESP_1A").Value = UserId
                        .Attributes("DSR_VISIT_ID").Value = hdDSR_VISIT_ID.Value
                        .Attributes("LCODE").Value = hdLCode.Value
                        .Attributes("PREDATE").Value = hdVisitDATE.Value
                    End With


                    objOutputXml = ObjbzStrategicVisitRetentionStatus.UPDATE_STRATEGIC_CALL_RETENTION_FOLLOWUP_ISSUEREPORT(objInputXml)
                ElseIf HdVisitType.Value.Trim = "TARGET" Then
                    Dim ObjbzStrategicVisitTargetStatus As New AAMS.bizSales.bzStrategicVisitTargetStatus
                    objInputXml.LoadXml("<UP_UPDATE_SL_STRATEGIC_CALL_TARGET_ISSUEREPORT_INPUT>	<DETAILS STT_DISCUSSIONISSUE_REMARKS='' RESP_1A='' DSR_VISIT_ID ='' LCODE ='' PREDATE=''   /></UP_UPDATE_SL_STRATEGIC_CALL_TARGET_ISSUEREPORT_INPUT>")
                    With objInputXml.DocumentElement.SelectSingleNode("DETAILS")
                        .Attributes("STT_DISCUSSIONISSUE_REMARKS").Value = txtDetailedDiscussion.Text

                        Dim objEmpXml As New XmlDocument
                        Dim UserId As String
                        objEmpXml.LoadXml(Session("Security"))
                        UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                        .Attributes("RESP_1A").Value = UserId
                        .Attributes("DSR_VISIT_ID").Value = hdDSR_VISIT_ID.Value
                        .Attributes("LCODE").Value = hdLCode.Value
                        .Attributes("PREDATE").Value = hdVisitDATE.Value
                    End With

                    objOutputXml = ObjbzStrategicVisitTargetStatus.UPDATE_SRATEGIC_CALL_TARGET_VISIT_FOLLOWUP_ISSUEREPORT(objInputXml)

                ElseIf HdVisitType.Value.Trim = "AIRNONAIR" Then
                    Dim ObjbzStrategicVisitAirNonAir As New AAMS.bizSales.bzStrategicVisitAirNonAir

                    objInputXml.LoadXml("<UP_UPDATE_SL_STRATEGIC_CALL_AIR_ISSUEREPORT_INPUT>	<DETAILS STA_DISCUSSIONISSUE_REMARKS='' RESP_1A='' DSR_VISIT_ID ='' LCODE ='' PREDATE=''   /></UP_UPDATE_SL_STRATEGIC_CALL_AIR_ISSUEREPORT_INPUT>")
                    With objInputXml.DocumentElement.SelectSingleNode("DETAILS")
                        .Attributes("STA_DISCUSSIONISSUE_REMARKS").Value = txtDetailedDiscussion.Text
                        Dim objEmpXml As New XmlDocument
                        Dim UserId As String
                        objEmpXml.LoadXml(Session("Security"))
                        UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                        .Attributes("RESP_1A").Value = UserId
                        .Attributes("DSR_VISIT_ID").Value = hdDSR_VISIT_ID.Value
                        .Attributes("LCODE").Value = hdLCode.Value
                        .Attributes("PREDATE").Value = hdVisitDATE.Value
                    End With
                    objOutputXml = ObjbzStrategicVisitAirNonAir.UPDATE_STRATEGIC_CALL_AIR_FOLLOWUP_ISSUEREPORT(objInputXml)

                End If
                Try
                    objInputXml.Save("c:\admin\DSRIssueReportSaveInput.xml")
                    objOutputXml.Save("c:\admin\DSRIssueReportSaveOut.xml")
                Catch ex As Exception
                End Try
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    BindData()
                    txtDetailedDiscussion.Text = ""
                    lblError.Text = objeAAMSMessage.messUpdate
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            Else
                If Session("VisitDetails") IsNot Nothing Then
                    objAddXmlDoc.LoadXml(Session("VisitDetails"))
                    If HdVisitType.Value.Trim = "SC" Then
                        '  objInputXml.LoadXml("<UP_UPDATE_SL_SERVICECALL_COMPINFO_INPUT><DETAILS SC_COMPETITION_MKT_INFO_REMARKS='' RESP_1A='' DSR_VISIT_ID ='' LCODE ='' PREDATE=''/></UP_UPDATE_SL_SERVICECALL_COMPINFO_INPUT>")
                    ElseIf HdVisitType.Value.Trim = "RETENTION" Then
                       
                        Dim objTempdocument As New XmlDocument
                        objTempdocument.LoadXml("<Temp><STRATEGIC_CALL_RETENTION><ISSURE_REPORT_RETENTION  DSR_VISIT_ID ='' STR_DISCUSSIONISSUE_REMARKS='' ></ISSURE_REPORT_RETENTION></STRATEGIC_CALL_RETENTION></Temp>")

                        Dim objnode As XmlNode = objTempdocument.DocumentElement.SelectSingleNode("STRATEGIC_CALL_RETENTION/ISSURE_REPORT_RETENTION")

                        For Each objattributes As XmlAttribute In objnode.Attributes
                            objattributes.Value = ""
                        Next
                        objnode.Attributes("STR_DISCUSSIONISSUE_REMARKS").Value = txtDetailedDiscussion.Text
                        objnode.Attributes("DSR_VISIT_ID").Value = ""
                        objAddXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_RETENTION").AppendChild(objAddXmlDoc.ImportNode(objnode, True))
                        lblError.Text = objeAAMSMessage.messInsert
                        txtDetailedDiscussion.Text = ""
                        Session("VisitDetails") = objAddXmlDoc.OuterXml
                    ElseIf HdVisitType.Value.Trim = "TARGET" Then
                       
                        Dim objTempdocument As New XmlDocument
                        objTempdocument.LoadXml("<Temp><STRATEGIC_CALL_TARGET><ISSURE_REPORT_TARGET  DSR_VISIT_ID ='' STT_DISCUSSIONISSUE_REMARKS='' ></ISSURE_REPORT_TARGET></STRATEGIC_CALL_TARGET></Temp>")
                        Dim objnode As XmlNode = objTempdocument.DocumentElement.SelectSingleNode("STRATEGIC_CALL_TARGET/ISSURE_REPORT_TARGET")
                        For Each objattributes As XmlAttribute In objnode.Attributes
                            objattributes.Value = ""
                        Next
                        objnode.Attributes("STT_DISCUSSIONISSUE_REMARKS").Value = txtDetailedDiscussion.Text
                        objnode.Attributes("DSR_VISIT_ID").Value = ""
                        objAddXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_TARGET").AppendChild(objAddXmlDoc.ImportNode(objnode, True))
                        lblError.Text = objeAAMSMessage.messInsert
                        txtDetailedDiscussion.Text = ""
                        Session("VisitDetails") = objAddXmlDoc.OuterXml
                    ElseIf HdVisitType.Value.Trim = "AIRNONAIR" Then
                     
                        Dim objTempdocument As New XmlDocument
                        objTempdocument.LoadXml("<Temp><STRATEGIC_CALL_AIR_OTHER><ISSURE_REPORT_AIR_OTHER  DSR_VISIT_ID ='' STA_DISCUSSIONISSUE_REMARKS='' ></ISSURE_REPORT_AIR_OTHER></STRATEGIC_CALL_AIR_OTHER></Temp>")
                        Dim objnode As XmlNode = objTempdocument.DocumentElement.SelectSingleNode("STRATEGIC_CALL_AIR_OTHER/ISSURE_REPORT_AIR_OTHER")
                        For Each objattributes As XmlAttribute In objnode.Attributes
                            objattributes.Value = ""
                        Next
                        objnode.Attributes("STA_DISCUSSIONISSUE_REMARKS").Value = txtDetailedDiscussion.Text
                        objnode.Attributes("DSR_VISIT_ID").Value = ""
                        objAddXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_AIR_OTHER").AppendChild(objAddXmlDoc.ImportNode(objnode, True))
                        lblError.Text = objeAAMSMessage.messInsert
                        txtDetailedDiscussion.Text = ""
                        Session("VisitDetails") = objAddXmlDoc.OuterXml
                    End If
                End If
                BindData()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            BindData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            BindData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try

            BindData()

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvRemarks_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvRemarks.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvRemarks_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvRemarks.Sorting
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

            BindData()


            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub BindControlsForNavigation(ByVal CrrentPageNo As String)
        ' ##################################################################
        '@ Code Added For Paging And Sorting
        ' ###################################################################
        pnlPaging.Visible = True
        '  Dim count As Integer = 0
        Dim count As Integer = CInt(CrrentPageNo)
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

        ' ###################################################################
        '@ End of Code Added For Paging And Sorting 
        ' ###################################################################
    End Sub
    Private Sub SetImageForSorting(ByVal grd As GridView)
        Dim imgUp As New Image
        imgUp.ImageUrl = "~/Images/Sortup.gif"
        Dim imgDown As New Image
        imgDown.ImageUrl = "~/Images/Sortdown.gif"
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Dim intcol As Integer = grd.Columns.IndexOf(field)
                If ViewState("Desc") = "FALSE" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "TRUE" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If
            End If
        Next
    End Sub

#End Region
End Class
