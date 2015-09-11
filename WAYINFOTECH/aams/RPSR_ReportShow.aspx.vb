Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Math
Partial Class RPSR_PTypeChallanShow
    Inherits System.Web.UI.Page
    Dim objRepDocument As New CrystalDecisions.CrystalReports.Engine.ReportDocument

    Protected Sub FrmReportShow_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles FrmReportShow.Init
        Dim objxmlout As New XmlDocument
        Dim eCashXSession As New XmlDocument
        Dim objds As New DataSet
        Dim xreader As XmlReader

        If Request.QueryString("Case") IsNot Nothing Then
            Select Case Request.QueryString("Case")

                Case Is = "AgencyWiseIncentivePaid"
                    If Session("AgencyWiseIncentivePaid") IsNot Nothing Then
                        objxmlout.LoadXml(Session("AgencyWiseIncentivePaid"))
                        ' objxmlout.Load("c:\AgencyWiseIncentivePaid.xml")
                        If objxmlout.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            xreader = New XmlNodeReader(objxmlout)
                            objds.ReadXml(xreader)
                            objRepDocument.Load(Server.MapPath("Incentive/Reports/rptIncentivePaid.rpt"))
                            objRepDocument.SetDataSource(objds)
                            CRViewer1.EnableParameterPrompt = False
                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = True
                        Else
                            lblError.Text = objxmlout.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        BindReport(objRepDocument)
                    End If


                    'Added by Pankaj on 13/08/2009 For Incentive Module Report Start
                Case Is = "IssueSideLetter"
                    If Session("IssueSideLetter") IsNot Nothing Then
                        Dim objTraining As New AAMS.bizIncetive.bzBusinessCase
                        Dim InputXML As New XmlDocument
                        InputXML.LoadXml(Session("IssueSideLetter"))
                        '   objxmlout = objTraining.rpt_ObjectiveMonitoring(InputXML)
                        objxmlout.Load("c:\IssueSideLetter.xml")
                        If objxmlout.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            xreader = New XmlNodeReader(objxmlout)
                            objds.ReadXml(xreader)
                            objRepDocument.Load(Server.MapPath("Incentive/Reports/rptSideLetter.rpt"))
                            ' objRepDocument.SetDataSource(objds)
                            'CRViewer1.EnableParameterPrompt = False
                            'CRViewer1.DisplayGroupTree = False
                            'CRViewer1.EnableViewState = True

                            Dim DiskOpts As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions
                            objRepDocument.ReportOptions.EnableSaveDataWithReport = True
                            ' objRepDocument.SetDataSource(objds)
                            objRepDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "IssueSideLetter")

                        Else

                            lblError.Text = objxmlout.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        '  BindReport(objRepDocument)
                    End If


                    ' Code For Incentive Module Report Start.
                    'Added by Pankaj on 04/06/2009 
                    'Case Is = "VolumeBasedReport"
                    '    If Session("VolumeBasedReport") IsNot Nothing Then
                    '        Dim objbizIncetive As New AAMS.bizIncetive.bzAgencyClassification
                    '        Dim InputXML As New XmlDocument
                    '        InputXML.LoadXml(Session("VolumeBasedReport"))
                    '        '  objxmlout.Load("C:\Inetpub\wwwroot\STYLUS\AAMS\RP_VOLUMECLASSIFICATION_OUTPUT.xml")
                    '        objxmlout = objbizIncetive.rpt_AgencyVolumeClassification(InputXML)
                    '        ' objxmlout.Save("C:\Inetpub\wwwroot\STYLUS\AAMS\RP_VOLUMECLASSIFICATION_OUTPUT1.xml")
                    '        If objxmlout.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    '            xreader = New XmlNodeReader(objxmlout)
                    '            objds.ReadXml(xreader)
                    '            objRepDocument.Load(Server.MapPath("Incentive/Reports/rptClassificationVolumeBased.rpt"))

                    '            ' Code For Adding Parameter In Report For Heading
                    '            Dim paramFldsInc As New ParameterFields
                    '            Dim Date_pField As New ParameterField()
                    '            Dim discreteValDate As New ParameterDiscreteValue()
                    '            Date_pField = New ParameterField()
                    '            discreteValDate = New ParameterDiscreteValue()
                    '            Date_pField.ParameterFieldName = "p_VolumeRpt"
                    '            ' Set the first discrete value and pass it to the parameter
                    '            discreteValDate.Value = (MonthName(InputXML.DocumentElement.SelectSingleNode("Month").InnerText.Trim(), True) + Right(InputXML.DocumentElement.SelectSingleNode("Year").InnerText.Trim(), 2)).ToUpper
                    '            Date_pField.CurrentValues.Add(discreteValDate)
                    '            paramFldsInc.Add(Date_pField)
                    '            CRViewer1.ParameterFieldInfo = paramFldsInc
                    '            'end


                    '            objRepDocument.SetDataSource(objds)
                    '            CRViewer1.EnableParameterPrompt = False
                    '            CRViewer1.DisplayGroupTree = False
                    '            CRViewer1.EnableViewState = True
                    '        Else
                    '            lblError.Text = objxmlout.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    '            Exit Select
                    '        End If

                    '        BindReport(objRepDocument)
                    '    End If
                    ' Code For Incentive Module Report End.

                Case Is = "DistinctionLetter"
                    If Session("LetterIDs") IsNot Nothing AndAlso hdStatus.Value = "" Then
                        Dim objInputXml As New XmlDocument
                        Dim objOutputXml As New XmlDocument
                        objInputXml.LoadXml("<TR_VIEWLETTERS_INPUT><DETAILS TR_CLETTER_IDS ='' LETTERTYPE='' /></TR_VIEWLETTERS_INPUT>")

                        objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("TR_CLETTER_IDS").Value = Session("LetterIDs")
                        objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("LETTERTYPE").Value = "Distinction Letter"
                        Dim objbzParticipant As New AAMS.bizTraining.bzParticipant
                        objOutputXml = objbzParticipant.ViewLetters_Print_Email(objInputXml)
                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            hdStatus.Value = "1"
                            For Each objnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("DETAILS")
                                If hdData1.Value = "" Then
                                    hdData1.Value = objnode.Attributes("TR_CLETTER").Value
                                Else
                                    hdData1.Value = hdData1.Value & "|" & objnode.Attributes("TR_CLETTER").Value
                                End If
                            Next

                            'xreader = New XmlNodeReader(objOutputXml)
                            'objds.ReadXml(xreader)
                            'objRepDocument.Load(Server.MapPath("Training/Reports/rptLetterAll.rpt"))
                            'objRepDocument.SetDataSource(objds)
                            'CRViewer1.EnableParameterPrompt = False
                            'CRViewer1.DisplayGroupTree = False
                            'CRViewer1.EnableViewState = True
                            'CRViewer1.ReportSource = objRepDocument
                            'CRViewer1.DataBind()

                            'Code for Exporting Data in PDF without PDF

                            '  Dim DiskOpts As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions
                            'objRepDocument.ReportOptions.EnableSaveDataWithReport = False
                            'objRepDocument.SetDataSource(objds)
                            'objRepDocument.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat '.WordForWindows
                            'objRepDocument.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile '.NoDestination
                            '  objRepDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, False, "")

                        End If
                    End If
                Case Is = "InvitationLetter"
                    If Session("LetterIDs") IsNot Nothing AndAlso hdStatus.Value = "" Then
                        Dim objInputXml As New XmlDocument
                        Dim objOutputXml As New XmlDocument
                        objInputXml.LoadXml("<TR_VIEWLETTERS_INPUT><DETAILS TR_CLETTER_IDS ='' LETTERTYPE='' /></TR_VIEWLETTERS_INPUT>")

                        objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("TR_CLETTER_IDS").Value = Session("LetterIDs")
                        objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("LETTERTYPE").Value = "Invitation Letter"
                        Dim objbzParticipant As New AAMS.bizTraining.bzParticipant
                        objOutputXml = objbzParticipant.ViewLetters_Print_Email(objInputXml)
                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            hdStatus.Value = "1"
                            For Each objnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("DETAILS")
                                If hdData1.Value = "" Then
                                    hdData1.Value = objnode.Attributes("TR_CLETTER").Value
                                Else
                                    hdData1.Value = hdData1.Value & "|" & objnode.Attributes("TR_CLETTER").Value
                                End If
                            Next

                            ' objds.ReadXml(xreader)
                            'objds.Tables.Add(dt)
                            'objRepDocument.Load(Server.MapPath("Training/Reports/rptLetterAll.rpt"))
                            'objRepDocument.SetDataSource(objds)

                            '<DETAILS TR_CLETTER_ID="180"
                            'TR_CLETTER()
                            'CRViewer1.EnableParameterPrompt = False
                            'CRViewer1.DisplayGroupTree = False
                            'CRViewer1.EnableViewState = True
                            'CRViewer1.ReportSource = objRepDocument
                            'CRViewer1.DataBind()
                            'CRViewer1.Visible = False

                            'Code for Exporting Data in PDF without PDF

                            'Dim DiskOpts As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions
                            'objRepDocument.ReportOptions.EnableSaveDataWithReport = False
                            'objRepDocument.SetDataSource(objds)
                            'objRepDocument.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat
                            'objRepDocument.ExportOptions.ExportDestinationType = ExportDestinationType.NoDestination '.DiskFile
                            'objRepDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, False, "")
                        End If
                    End If


                    'Added by Pankaj on 30/05/2008 For Training Module Report Start
                Case Is = "TrainingObjectiveMonitor"
                    If Session("TrainingObjectiveMonitor") IsNot Nothing Then
                        '  ' If Not Page.IsPostBack Then
                        Dim objTraining As New AAMS.bizTraining.bzTraining
                        Dim InputXML As New XmlDocument
                        InputXML.LoadXml(Session("TrainingObjectiveMonitor"))
                        objxmlout = objTraining.rpt_ObjectiveMonitoring(InputXML)
                        If objxmlout.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            xreader = New XmlNodeReader(objxmlout)
                            objds.ReadXml(xreader)
                            objRepDocument.Load(Server.MapPath("Training/Reports/rptObjectiveMonitoring.rpt"))
                            objRepDocument.SetDataSource(objds)
                            CRViewer1.EnableParameterPrompt = False
                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = True
                            ' 'Session("MyPage") = objRepDocument
                        Else
                            ' Session("MyPage") = Nothing
                            lblError.Text = objxmlout.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "TrainingRoomPerformance"

                    If Session("TrainingRoomPerformance") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        Dim objbzTrainingRoom As New AAMS.bizTraining.bzTrainingRoom
                        Dim InputXML As New XmlDocument
                        InputXML.LoadXml(Session("TrainingRoomPerformance"))
                        objxmlout = objbzTrainingRoom.rptTrainingRoomPerformance(InputXML)
                        If objxmlout.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            xreader = New XmlNodeReader(objxmlout)
                            objds.ReadXml(xreader)
                            objRepDocument.Load(Server.MapPath("Training/Reports/rptTrainingRoomPerformance.rpt"))
                            objRepDocument.SetDataSource(objds)
                            CRViewer1.EnableParameterPrompt = False
                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = True
                            'Session("MyPage") = objRepDocument
                        Else
                            'Session("MyPage") = Nothing
                            lblError.Text = objxmlout.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If


                Case Is = "AgencyTrainingPeople"
                    If Session("AgencyTrainingGroup") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objRepDocument.Load(Server.MapPath("Training/Reports/rptAgencyPeopleTraining.rpt"))
                        objxmlout.LoadXml(Session("AgencyTrainingGroup"))
                        'objxmlout.Load("c:\TR_SEARCH_STAFF_TRAINING_OUTPUT.xml")
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = True
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "AgencyTrainingSession"

                    If Session("AgencyTrainingGroup") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objRepDocument.Load(Server.MapPath("Training/Reports/rptAgencySessionTraining.rpt"))
                        objxmlout.LoadXml(Session("AgencyTrainingGroup"))
                        'objxmlout.Load("c:\TR_SEARCH_STAFF_TRAINING_OUTPUT.xml")
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = True
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If
                    ' Code Start for Letter
                Case Is = "Letter"
                    If Session("Letter") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objRepDocument.Load(Server.MapPath("Training/Reports/rptLetter.rpt"))

                        objRepDocument.SetParameterValue("Name", Session("Letter"))
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = True
                        CRViewer1.PrintMode = CrystalDecisions.Web.PrintMode.Pdf
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                    'End For Training Module Report 

                Case Is = "FlashReport"
                    If Session("eFlashReport") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objRepDocument.Load(Server.MapPath("Market/Reports/RPT_FLASH.rpt"))
                        objxmlout.LoadXml(Session("eFlashReport"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "FlashSummaryReport"
                    If Session("eFlashSummaryReport") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objRepDocument.Load(Server.MapPath("Market/Reports/RPT_FLASHSUMMARY.rpt"))
                        objxmlout.LoadXml(Session("eFlashSummaryReport"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If
                Case Is = "AgencyLsit"

                    If Session("eAgencyListRpt") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objRepDocument.Load(Server.MapPath("TravelAgency/Reports/rptAgencyList.rpt"))
                        objxmlout.LoadXml(Session("eAgencyListRpt"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If


                Case Is = "PTypeChallan"
                    '******************** New Code implement for Ptype Challan Report woking by ashish******************
                    Dim objInputXml, objOutputXml As New XmlDocument
                    Dim objInputXmlOrderView, objOutputXmlOrderView As New XmlDocument  ''xml for calling view
                    Dim objInputXmlOrderATID, objOutputXmlOrderATID As New XmlDocument  ''xml for calling orderid and ATID

                    Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
                    Dim objbzOrder As New AAMS.bizTravelAgency.bzOrder

                    Dim objFinalOutxml As New XmlDocument
                    Dim intLcode As Integer
                    Dim strOfficeID As String = ""

                    objInputXmlOrderATID.LoadXml("<TA_GET_ATID_INPUT><LCODE></LCODE><ORDER_NO></ORDER_NO></TA_GET_ATID_INPUT>") ''load ATID
                    objInputXmlOrderView.LoadXml("<MS_VIEWORDER_INPUT><ORDERID></ORDERID></MS_VIEWORDER_INPUT>")  ''load View

                    ''Fill input XML to get OrderId
                    objInputXmlOrderATID.DocumentElement.SelectSingleNode("LCODE").InnerText = Session("Action").ToString().Split("|").GetValue(1) ''GET LCODE
                    objInputXmlOrderATID.DocumentElement.SelectSingleNode("ORDER_NO").InnerText = Request.QueryString("OrderNo")
                    ''end here

                    objOutputXmlOrderATID = objbzOrder.GET_ATID(objInputXmlOrderATID)
                    If objOutputXmlOrderATID.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        objInputXmlOrderView.DocumentElement.SelectSingleNode("ORDERID").InnerXml = objOutputXmlOrderATID.DocumentElement.SelectSingleNode("ATID").Attributes("ORDERID").Value
                        objOutputXmlOrderView = objbzOrder.View(objInputXmlOrderView)
                    End If


                    If Session("Action").ToString().Split("|").GetValue(1) <> "" Then
                        intLcode = Val(Session("Action").ToString().Split("|").GetValue(1) & "")
                        objRepDocument.Load(Server.MapPath("TravelAgency/Reports/rptDeinstallationPtype.rpt"))
                        If intLcode <> 0 Then
                            objInputXml.LoadXml("<UP_GETCRSDETAILS_INPUT><LCODE>" & intLcode & "</LCODE></UP_GETCRSDETAILS_INPUT>")
                            objOutputXml = objbzAgency.GetAgencyCrsUseDetails(objInputXml)
                            If objOutputXml.DocumentElement.SelectNodes("CRS[@CURRENTID='Y']").Count <> 0 Then
                                strOfficeID = (objOutputXml.DocumentElement.SelectSingleNode("CRS[@CURRENTID='Y']").Attributes("OFFICEID").Value)
                            End If
                        End If
                        objInputXml.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")
                        objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerXml = Session("Action").ToString().Split("|").GetValue(1)
                        objOutputXml = objbzAgency.View(objInputXml)

                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                            objFinalOutxml.LoadXml("<RP_PTYPECHALLAN_OUTPUT><PTYPECHALLAN AgencyName='' Address='' TelNo='' FaxNo='' City='' Country='' ApprovedBy='' ReceivedBY='' OfficeID='' ChallanNumber='' Branch='' OrderNo='' OrderType='' OrderQty='' OrderRemark='' OnlineStatus='' ATID='' /><PTYPEFOTTEROUTPUT Company='' Employee_Name='' Address='' Phone='' Fax='' City='' Country_Name='' REG_OFFICE_ADDRESS='' Reg_Tel='' Reg_Fax='' /> </RP_PTYPECHALLAN_OUTPUT>")

                            With objOutputXml.DocumentElement.SelectSingleNode("Agency")
                                objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("AgencyName").Value = .Attributes("NAME").Value()
                                objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("Address").Value() = .Attributes("ADDRESS").Value() + " " + .Attributes("ADDRESS1").Value()
                                objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("TelNo").Value() = .Attributes("PHONE").Value()
                                objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("FaxNo").Value() = .Attributes("FAX").Value()
                                objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("City").Value() = .Attributes("CITY").Value()
                                objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("Country").Value() = .Attributes("COUNTRY").Value()
                                'new aaded
                                objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("OnlineStatus").Value() = .Attributes("ONLINE_STATUS").Value()
                                'end here    

                                objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("ApprovedBy").Value() = ""
                                objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("ReceivedBY").Value() = ""
                                objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("OfficeID").Value() = strOfficeID
                                objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("ChallanNumber").Value() = ""
                                objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("Branch").Value() = .Attributes("Aoffice").Value()

                                objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("OrderNo").Value() = Request.QueryString("OrderNo")
                                objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("OrderType").Value() = Request.QueryString("OrderType")

                                If Request.QueryString("IsOrderType") = 1 Then
                                    objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("OrderQty").Value() = Request.QueryString("OrderQtyAPC")
                                Else
                                    objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("OrderQty").Value() = Request.QueryString("OrderQty")
                                End If

                                ''remarks now ATID
                                objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("OrderRemark").Value() = Request.QueryString("OrderRemark")


                                objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("ATID").Value() = objOutputXmlOrderView.DocumentElement.SelectSingleNode("ORDERS").Attributes("ATID").Value

                                Dim strarrATID() As String
                                Dim intcount As Integer
                                Dim strFinal As String = String.Empty
                                Dim strATID As String
                                Dim strLine(200) As String
                                Dim line As Integer
                                Dim PrmBenefitDOM As New ParameterField()
                                Dim discreteVal_PrmBenefitDOM As New ParameterDiscreteValue()
                                Dim paramfields As New ParameterFields
                                Dim x As Integer
                                Dim pcCounter As Integer
                                pcCounter = CInt(Request.QueryString("OrderQty"))

                                PrmBenefitDOM.ParameterFieldName = "P_ATID"

                                strATID = objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("ATID").Value()

                                discreteVal_PrmBenefitDOM.Value = strATID
                                strarrATID = strATID.Split(",")


                                ''*********************Logic for print****************************************************


                                x = 0
                                line = 0
                                strarrATID = strATID.Split(",")

                                If strarrATID(0) <> "" Then
                                    'lblError.Text = "Invalid.."
                                    'Exit Sub

                                    intcount = 0
                                    While intcount < pcCounter 'strarrATID.Length - 1
                                        If line = 3 Then
                                            strFinal = strFinal & vbNewLine
                                            strLine(x) = strFinal
                                            'MsgBox(strLine(x))
                                            strFinal = ""
                                            line = 0
                                            x = x + 1
                                            If intcount = strarrATID.Length Then Exit While

                                        Else
                                            If UCase(strarrATID(intcount)) <> "" Then
                                                strFinal = UCase(strarrATID(intcount)) & vbTab & strFinal
                                                ' MsgBox(strFinal)
                                                intcount = intcount + 1
                                                line = line + 1
                                            Else
                                                Exit While
                                            End If
                                        End If
                                    End While
                                    If intcount = pcCounter Then
                                        strFinal = strFinal & vbNewLine
                                        strLine(x) = strFinal
                                    End If
                                    ''end 
                                    strFinal = ""

                                    For intcount = 0 To strLine.Length - 1
                                        If strLine(intcount) <> "" Then
                                            strFinal = strLine(intcount) & strFinal
                                        Else
                                            strFinal = strFinal
                                        End If
                                    Next
                                End If





                                discreteVal_PrmBenefitDOM.Value = strFinal
                                PrmBenefitDOM.CurrentValues.Add(discreteVal_PrmBenefitDOM)
                                paramfields.Add(PrmBenefitDOM)
                                CRViewer1.ParameterFieldInfo = paramfields

                            End With





                            '##################################################################################################
                            '               Getting Employee Details For PType Footer details (working by ashish)
                            '###################################################################################################
                            Try

                                Dim objInputEmpXml As New XmlDocument
                                Dim objOutputLcodeXml As New XmlDocument
                                Dim objbzPCInstallation As New AAMS.bizTravelAgency.bzPCInstallation

                                objInputEmpXml.LoadXml("<GET_PTYPEFOTTER_INPUT><LOCATION_CODE></LOCATION_CODE></GET_PTYPEFOTTER_INPUT>")

                                If Not Session("LoginSession") Is Nothing Then
                                    objInputEmpXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = Session("Action").ToString().Split("|").GetValue(1)  'Get Lcode from 
                                    objOutputLcodeXml = objbzPCInstallation.GetPTypeFooterDetails(objInputEmpXml)

                                    If objOutputLcodeXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                                        objFinalOutxml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("Company").Value = objOutputLcodeXml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("COMPANY").Value
                                        objFinalOutxml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("Employee_Name").Value = objOutputLcodeXml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("EMPLOYEE_NAME").Value
                                        objFinalOutxml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("Address").Value = objOutputLcodeXml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("ADDRESS").Value
                                        objFinalOutxml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("Phone").Value = objOutputLcodeXml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("PHONE").Value
                                        objFinalOutxml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("Fax").Value = objOutputLcodeXml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("FAX").Value
                                        objFinalOutxml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("City").Value = objOutputLcodeXml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("CITY").Value
                                        objFinalOutxml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("Country_Name").Value = objOutputLcodeXml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("COUNTRY_NAME").Value
                                        objFinalOutxml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("REG_OFFICE_ADDRESS").Value = objOutputLcodeXml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("REG_OFFICE_ADDRESS").Value
                                        objFinalOutxml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("Reg_Tel").Value = objOutputLcodeXml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("Reg_Tel").Value
                                        objFinalOutxml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("Reg_Fax").Value = objOutputLcodeXml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("Reg_Fax").Value
                                    End If
                                End If
                            Catch ex As Exception
                            End Try
                            '#########################################  END ################################################

                            xreader = New XmlNodeReader(objFinalOutxml)
                            objds.ReadXml(xreader)
                            objRepDocument.SetDataSource(objds)
                            CRViewer1.EnableParameterPrompt = False
                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = False

                            BindReport(objRepDocument)
                        End If
                    End If



                    'Old Code ##################################################################################
                    ''If Session("ePTypeChallan") IsNot Nothing Then
                    ''    ' If Not Page.IsPostBack Then
                    ''    objRepDocument.Load(Server.MapPath("TravelAgency/Reports/rptDeinstallationPtype.rpt"))
                    ''    objxmlout.LoadXml(Session("ePTypeChallan"))

                    ''    objFinalOutxml.LoadXml("<RP_PTYPECHALLAN_OUTPUT><PTYPECHALLAN AgencyName='' Address='' TelNo='' FaxNo='' City='' Country='' ApprovedBy='' ReceivedBY='' OfficeID='' ChallanNumber='' Branch='' OrderNo='' OrderType='' /><PTYPEFOTTEROUTPUT Company='' Employee_Name='' Address='' Phone='' Fax='' City='' Country_Name='' REG_OFFICE_ADDRESS=''  /> </RP_PTYPECHALLAN_OUTPUT>")
                    ''    objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("AgencyName").Value = objxmlout.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("AgencyName").Value()
                    ''    objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("Address").Value() = objxmlout.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("Address").Value()
                    ''    objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("TelNo").Value() = objxmlout.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("TelNo").Value()
                    ''    objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("FaxNo").Value() = objxmlout.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("FaxNo").Value()
                    ''    objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("City").Value() = objxmlout.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("City").Value()
                    ''    objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("Country").Value() = objxmlout.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("Country").Value()
                    ''    objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("ApprovedBy").Value() = objxmlout.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("ApprovedBy").Value()
                    ''    objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("ReceivedBY").Value() = objxmlout.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("ReceivedBY").Value()
                    ''    objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("OfficeID").Value() = objxmlout.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("OfficeID").Value()
                    ''    objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("ChallanNumber").Value() = objxmlout.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("ChallanNumber").Value()
                    ''    objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("Branch").Value() = objxmlout.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("Branch").Value()

                    ''    objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("OrderNo").Value() = Request.QueryString("OrderNo")
                    ''    objFinalOutxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("OrderType").Value() = Request.QueryString("OrderType")

                    ''    '################################################
                    ''    'Getting Employee Details For PType Footer details
                    ''    '################################################
                    ''    Try

                    ''        Dim objInputEmpXml As New XmlDocument
                    ''        Dim objOutputLcodeXml As New XmlDocument
                    ''        Dim objbzPCInstallation As New AAMS.bizTravelAgency.bzPCInstallation

                    ''        'objInputEmpXml.LoadXml("<GET_PTYPEFOTTER_INPUT><EMPLOYEEID></EMPLOYEEID></GET_PTYPEFOTTER_INPUT>")
                    ''        objInputEmpXml.LoadXml("<GET_PTYPEFOTTER_INPUT><LOCATION_CODE></LOCATION_CODE></GET_PTYPEFOTTER_INPUT>")

                    ''        If Not Session("LoginSession") Is Nothing Then
                    ''            ' objInputEmpXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = Session("LoginSession").ToString().Split("|")(0)

                    ''            objInputEmpXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = Session("Action").ToString().Split("|").GetValue(1)  'Get Lcode from 
                    ''            objOutputLcodeXml = objbzPCInstallation.GetPTypeFooterDetails(objInputEmpXml)

                    ''            If objOutputLcodeXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    ''                objFinalOutxml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("Company").Value = objOutputLcodeXml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("COMPANY").Value
                    ''                objFinalOutxml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("Employee_Name").Value = objOutputLcodeXml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("EMPLOYEE_NAME").Value
                    ''                objFinalOutxml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("Address").Value = objOutputLcodeXml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("ADDRESS").Value
                    ''                objFinalOutxml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("Phone").Value = objOutputLcodeXml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("PHONE").Value
                    ''                objFinalOutxml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("Fax").Value = objOutputLcodeXml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("FAX").Value
                    ''                objFinalOutxml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("City").Value = objOutputLcodeXml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("CITY").Value
                    ''                objFinalOutxml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("Country_Name").Value = objOutputLcodeXml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("COUNTRY_NAME").Value
                    ''                ' new added on 21-may-2010
                    ''                objFinalOutxml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("REG_OFFICE_ADDRESS").Value = objOutputLcodeXml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("REG_OFFICE_ADDRESS").Value
                    ''                'objFinalOutxml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("REG_TEL").Value = objOutputLcodeXml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("REG_TEL").Value
                    ''                'objFinalOutxml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("REG_FAX").Value = objOutputLcodeXml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT").Attributes("REG_FAX").Value
                    ''                'end here

                    ''            End If
                    ''        End If
                    ''    Catch ex As Exception
                    ''    End Try
                    ''    '################################################
                    ''    'Getting Employee Details For PType Footer details
                    ''    '################################################

                    ''    xreader = New XmlNodeReader(objFinalOutxml)

                    ''    '  xreader = New XmlNodeReader(objxmlout)
                    ''    objds.ReadXml(xreader)
                    ''    objRepDocument.SetDataSource(objds)
                    ''    CRViewer1.EnableParameterPrompt = False
                    ''    CRViewer1.DisplayGroupTree = False
                    ''    CRViewer1.EnableViewState = False
                    ''    'Session("MyPage") = objRepDocument
                    ''    'Else
                    ''    '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                    ''    'End If
                    ''    'Modified on 22 August
                    ''    BindReport(objRepDocument)
                    ''End If
                    'end Old Code ##################################################################################

                Case Is = "ISPPlanList"

                    If Session("eISPPlanList") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objRepDocument.Load(Server.MapPath("ISP/Reports/RptISPPlan.rpt"))
                        objxmlout.LoadXml(Session("eISPPlanList"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If


                Case Is = "ISPFeasibilityRequest"
                    If Session("eISPFeasibilityRequest") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objRepDocument.Load(Server.MapPath("ISP/Reports/RptISP_Feasibility.rpt"))
                        objxmlout.LoadXml(Session("eISPFeasibilityRequest"))
                        If objxmlout.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            xreader = New XmlNodeReader(objxmlout)
                            objds.ReadXml(xreader)
                            objRepDocument.SetDataSource(objds)
                            CRViewer1.EnableParameterPrompt = False
                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = False
                            'Session("MyPage") = objRepDocument
                        Else
                            Session("MyPage") = Nothing
                            lblError.Text = objxmlout.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "ISPOrderReport"

                    If Session("ISPOrderReport") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objRepDocument.Load(Server.MapPath("ISP/Reports/RptIspOrder.rpt"))
                        objxmlout.LoadXml(Session("ISPOrderReport"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "ISPPaymentProceed"
                    If Session("ISPPaymentProceed") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objRepDocument.Load(Server.MapPath("ISP/Reports/RPTPaymentProceed.rpt"))
                        objxmlout.LoadXml(Session("ISPPaymentProceed"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If
                Case Is = "ISPPaymentReceived"
                    If Session("ISPPaymentReceived") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objRepDocument.Load(Server.MapPath("ISP/Reports/RptPaymentReceived.rpt"))
                        objxmlout.LoadXml(Session("ISPPaymentReceived"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "EmployeeList"
                    If Session("eEmployeeListRpt") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then

                        objRepDocument.Load(Server.MapPath("Setup/Reports/RptEmployeeList.rpt"))
                        objxmlout.LoadXml(Session("eEmployeeListRpt"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If
                Case Is = "OfficeIdSumCityWise"
                    If Session("eOfficeID") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        Dim objOutputXml, objInputXml As New XmlDocument
                        Dim objbzOfficeId As New AAMS.bizTravelAgency.bzOfficeID
                        objInputXml.LoadXml(Session("eOfficeID"))
                        objOutputXml = objbzOfficeId.rptOfficeIDSummary(objInputXml)
                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            objRepDocument.Load(Server.MapPath("TravelAgency/Reports/rptOfficeIdSumCityWise.rpt"))
                            xreader = New XmlNodeReader(objOutputXml)
                            objds.ReadXml(xreader)
                            objRepDocument.SetDataSource(objds)
                            CRViewer1.EnableParameterPrompt = False

                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = False
                            'Session("MyPage") = objRepDocument
                        Else
                            'Session("MyPage") = Nothing
                            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If
                Case Is = "OfficeIdUnallocCItyWise"
                    If Session("eOfficeID") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        Dim objOutputXml, objInputXml As New XmlDocument
                        Dim objbzOfficeId As New AAMS.bizTravelAgency.bzOfficeID
                        objInputXml.LoadXml(Session("eOfficeID"))
                        objOutputXml = objbzOfficeId.rptOfficeIDSummary(objInputXml)
                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            objRepDocument.Load(Server.MapPath("TravelAgency/Reports/rptOfficeIdUnallocCItyWise.rpt"))
                            xreader = New XmlNodeReader(objOutputXml)
                            objds.ReadXml(xreader)
                            objRepDocument.SetDataSource(objds)
                            CRViewer1.EnableParameterPrompt = False

                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = False
                            'Session("MyPage") = objRepDocument
                        Else
                            ' Session("MyPage") = Nothing
                            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If
                Case Is = "OfficeIdallocCityWise"
                    If Session("eOfficeID") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then

                        Dim objOutputXml, objInputXml As New XmlDocument
                        Dim objbzOfficeId As New AAMS.bizTravelAgency.bzOfficeID
                        objInputXml.LoadXml(Session("eOfficeID"))
                        objOutputXml = objbzOfficeId.rptOfficeIDSummary(objInputXml)
                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            objRepDocument.Load(Server.MapPath("TravelAgency/Reports/rptOfficeIdallocCItyWise.rpt"))
                            xreader = New XmlNodeReader(objOutputXml)
                            objds.ReadXml(xreader)
                            objRepDocument.SetDataSource(objds)
                            CRViewer1.EnableParameterPrompt = False

                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = False
                            'Session("MyPage") = objRepDocument
                        Else
                            '  Session("MyPage") = Nothing
                            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If
                Case Is = "BRDLetter"
                    If Session("eBRDLetterRpt") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then

                        objRepDocument.Load(Server.MapPath("HelpDesk/Reports/RptHDBDRReport.rpt"))
                        objxmlout.LoadXml(Session("eBRDLetterRpt"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If
                Case Is = "BOBRDLetter"
                    If Session("eBOBRDLetterRpt") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then

                        objRepDocument.Load(Server.MapPath("BackOfficeHelpDesk/Reports/RptBOHDBDRReport.rpt"))
                        objxmlout.LoadXml(Session("eBOBRDLetterRpt"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If



                Case Is = "BR BRDLetter"
                    If Session("eBRDLetterRpt") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then

                        objRepDocument.Load(Server.MapPath("BirdresHelpDesk/Reports/RptHDBDRReport.rpt"))
                        objxmlout.LoadXml(Session("eBRDLetterRpt"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "Feedback"
                    If Session("eFeedback") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then

                        Dim objfb As New AAMS.bizHelpDesk.bzFeedback
                        objRepDocument.Load(Server.MapPath("HelpDesk/Reports/RPTFeedback.rpt"))
                        Dim InputXML As New XmlDocument, OutputXML As New XmlDocument
                        'InputXML.LoadXml("<HD_RPTFEEDBACK_INPUT><MONTH>03</MONTH><YEAR>2008</YEAR></HD_RPTFEEDBACK_INPUT>")
                        InputXML.LoadXml(Session("eFeedback"))

                        OutputXML = objfb.FeedbackReport(InputXML)

                        If OutputXML.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                            '@ Remove Node On the basis of Report Type 
                            If Request.QueryString("Action").ToString().Trim = "F" Then
                                Dim i As Integer = 0
                                For Each objnode As XmlNode In OutputXML.DocumentElement.SelectNodes("SECONDFOURTH")
                                    If i <> 0 Then
                                        OutputXML.DocumentElement.RemoveChild(objnode)
                                    End If
                                    i = i + 1
                                Next
                                i = 0
                                For Each objnode As XmlNode In OutputXML.DocumentElement.SelectNodes("MONTHLY")
                                    If i <> 0 Then
                                        OutputXML.DocumentElement.RemoveChild(objnode)
                                    End If
                                    i = i + 1
                                Next

                            ElseIf Request.QueryString("Action").ToString().Trim = "S" Then
                                Dim i As Integer = 0
                                For Each objnode As XmlNode In OutputXML.DocumentElement.SelectNodes("FIRSTFOURTH")
                                    If i <> 0 Then
                                        OutputXML.DocumentElement.RemoveChild(objnode)
                                    End If
                                    i = i + 1
                                Next
                                i = 0
                                For Each objnode As XmlNode In OutputXML.DocumentElement.SelectNodes("MONTHLY")
                                    If i <> 0 Then
                                        OutputXML.DocumentElement.RemoveChild(objnode)
                                    End If
                                    i = i + 1
                                Next

                            ElseIf Request.QueryString("Action").ToString().Trim = "M" Then
                                Dim i As Integer = 0
                                For Each objnode As XmlNode In OutputXML.DocumentElement.SelectNodes("SECONDFOURTH")
                                    If i <> 0 Then
                                        OutputXML.DocumentElement.RemoveChild(objnode)
                                    End If
                                    i = i + 1
                                Next
                                i = 0
                                For Each objnode As XmlNode In OutputXML.DocumentElement.SelectNodes("FIRSTFOURTH")
                                    If i <> 0 Then
                                        OutputXML.DocumentElement.RemoveChild(objnode)
                                    End If
                                    i = i + 1
                                Next

                            End If
                            objxmlout.LoadXml(OutputXML.OuterXml) 'Session("eFeedback"))
                            xreader = New XmlNodeReader(objxmlout)
                            objds.ReadXml(xreader)
                            objRepDocument.SetDataSource(objds)
                            CRViewer1.EnableParameterPrompt = False
                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = False
                            ' CRViewer1.ReportSource = objRepDocument

                            Dim paramfields As New ParameterFields
                            Dim InstallationFrom_pField As New ParameterField()
                            Dim InstallationTo_pField As New ParameterField()
                            Dim discreteValFrom As New ParameterDiscreteValue()
                            Dim discreteValTo As New ParameterDiscreteValue()

                            InstallationFrom_pField = New ParameterField()
                            discreteValFrom = New ParameterDiscreteValue()
                            InstallationFrom_pField.ParameterFieldName = "RptTypeNew"
                            ' Set the first discrete value and pass it to the parameter
                            discreteValFrom.Value = Request.QueryString("Action").Trim
                            InstallationFrom_pField.CurrentValues.Add(discreteValFrom)
                            paramfields.Add(InstallationFrom_pField)
                            CRViewer1.ParameterFieldInfo = paramfields


                            'Dim paramfields As New ParameterFields
                            'Dim Pra_RptType As New ParameterField()
                            'Dim Pra_Year As New ParameterField()
                            'Dim discreteValFrom As New ParameterDiscreteValue()
                            'Dim discreteValTo As New ParameterDiscreteValue()

                            'Pra_RptType = New ParameterField()
                            'discreteValFrom = New ParameterDiscreteValue()
                            'Pra_RptType.ParameterFieldName = "RptTypeNew"
                            'discreteValFrom.Value = Request.QueryString("Action").Trim

                            'Pra_RptType.CurrentValues.Add(Pra_RptType)
                            'paramfields.Add(Pra_RptType)
                            'CRViewer1.ParameterFieldInfo = paramfields
                            'Session("MyPage") = objRepDocument
                        Else
                            ' Session("MyPage") = Nothing
                            lblError.Text = OutputXML.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If
                Case Is = "HelpDeskDynamicReport"

                    If Session("HelpDeskDynamicReport") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then

                        objxmlout.LoadXml(Session("HelpDeskDynamicReport"))

                        '    objRepDocument.Load(Server.MapPath("HelpDesk/Reports/RptHelpDeskDynamicReport.rpt"))


                        If objxmlout.DocumentElement.SelectNodes("SEARCHDESC[@Ltr!='']").Count > 0 And objxmlout.DocumentElement.SelectNodes("SEARCHSOLN[@Ltr!='']").Count > 0 Then
                            objRepDocument.Load(Server.MapPath("HelpDesk/Reports/RptHelpDeskDynamicReport.rpt"))
                        End If
                        If objxmlout.DocumentElement.SelectNodes("SEARCHDESC[@Ltr!='']").Count <= 0 And objxmlout.DocumentElement.SelectNodes("SEARCHSOLN[@Ltr!='']").Count > 0 Then
                            objRepDocument.Load(Server.MapPath("HelpDesk/Reports/RptHelpDeskDynamicReportWithSoln.rpt"))
                        End If
                        If objxmlout.DocumentElement.SelectNodes("SEARCHDESC[@Ltr!='']").Count > 0 And objxmlout.DocumentElement.SelectNodes("SEARCHSOLN[@Ltr!='']").Count <= 0 Then
                            objRepDocument.Load(Server.MapPath("HelpDesk/Reports/RptHelpDeskDynamicReportWithDesc.rpt"))
                        End If

                        If objxmlout.DocumentElement.SelectNodes("SEARCHDESC[@Ltr!='']").Count <= 0 And objxmlout.DocumentElement.SelectNodes("SEARCHSOLN[@Ltr!='']").Count <= 0 Then
                            objRepDocument.Load(Server.MapPath("HelpDesk/Reports/RptHelpDeskDynamicReportWithoutSolnAndDesc.rpt"))
                        End If



                        'objxmlout.LoadXml("<HD_DMSEARCH_CALLREQUEST_OUTPUT><HEADER FIELD1='LTR_NO' FIELD2='AGENCY' FIELD3='ADDRESS' FIELD4='PTR_NO' FIELD5='LAST_CALL' FIELD6='QUERY_STATUS' FIELD7='FOLLOWUP' FIELD8='SEVERITY' FIELD9='LOGGED_BY' FIELD10='CLOSE_DATE_FROM' FIELD11='CLOSE_DATE_TO' FIELD12='OPEN_DATE_FROM' FIELD13='OPEN_DATE_TO' FIELD14='ASSIGNED_TO' FIELD15='ASSIGNED_DATE_TIME' FIELD16='OFFICE_ID' FIELD17='CUSTOMER_CATEGORY' FIELD18='CALLER_NAME' FIELD19='QUERY_GROUP' FIELD20='QUERY_SUB_GROUP' FIELD21='QUERY_CATEGORY' FIELD22='QUERY_SUB_CATEGORY' FIELD23='COORDINATOR1'  FIELD24='COORDINATOR2' FIELD25='DISPOSITION' FIELD26='1AOFFICE' FIELD27='AGENCY_1A_OFFICE' FIELD28='WORK_ORDER_NO'><DETAIL VALUE1='554' VALUE2='fgfdd879576546gdfg' VALUE3='fgfg' VALUE4='fgfdg' VALUE5='' VALUE6='' VALUE7='' VALUE8='' VALUE9='' VALUE10='' VALUE11='' VALUE12='' VALUE13='' VALUE14='' VALUE15='' VALUE16='' VALUE17='' VALUE18='' VALUE19='' VALUE20='' VALUE21='' VALUE22='' 					VALUE23='' VALUE24='' VALUE25='' VALUE26='' VALUE27='' VALUE28=''/><DETAIL VALUE1='55674' VALUE2='54654654' VALUE3='fgfg' VALUE4='fgfdg' VALUE5='' VALUE6='' VALUE7='' VALUE8='' VALUE9='' VALUE10='' VALUE11='' VALUE12='' VALUE13='' VALUE14='' VALUE15='7' VALUE16='' VALUE17='' VALUE18='' VALUE19='' VALUE20='' VALUE21='' VALUE22='' VALUE23='' VALUE24='' VALUE25='' VALUE26='' VALUE27='' VALUE28=''/><DETAIL VALUE1='567654' VALUE2='fgfhfghfghfggdfg' VALUE3='fgfg657567656657 546  566765756765765765765757' VALUE4='fgfdg' VALUE5='' VALUE6='' VALUE7='' VALUE8='' VALUE9='' VALUE10='' VALUE11='' VALUE12='' VALUE13='' VALUE14='' VALUE15='' VALUE16='' VALUE17='' VALUE18='' VALUE19='' VALUE20='' VALUE21='' VALUE22=''	VALUE23='' VALUE24='' VALUE25='' VALUE26='' VALUE27='h' VALUE28='66'/>	</HEADER><Errors Status=''>	<Error Code='' Description=''/>	</Errors></HD_DMSEARCH_CALLREQUEST_OUTPUT>")
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "InvStockTransferReport"

                    ' Purchase Order Challan
                    If Session("StockTransferRpt") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objRepDocument.Load(Server.MapPath("Inventory/Reports/rptStockTransferChallan.rpt"))
                        objxmlout.LoadXml(Session("StockTransferRpt"))
                        ' objxmlout.Load("c:\StockTransfer.xml")
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "InventoryChallanReport"

                    ' Purchase Order Challan
                    If Session("PurchaseOrderRpt") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objRepDocument.Load(Server.MapPath("Inventory/Reports/rptPurchaseOrderChallan.rpt"))
                        objxmlout.LoadXml(Session("PurchaseOrderRpt"))
                        'objxmlout.Load("c:\PurchaseOrder.xml")
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If
                    ' ************************** Customer Report Start **********************************

                Case Is = "CustomerIssue"
                    ' Customer Issue Challan Report .
                    If Session("CustomerReport") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objRepDocument.Load(Server.MapPath("Inventory/Reports/RptINV_CustomerIssue.rpt"))
                        '  objxmlout.Load("c:\CustomerChallan.xml")

                        objxmlout.LoadXml(Session("CustomerReport"))
                        '  objxmlout.Save("c:\CustomerChallan.xml")
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If
                    '
                Case Is = "CustomerReceive"
                    ' Customer Challan Delivery Report 

                    If Session("CustomerReport") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objRepDocument.Load(Server.MapPath("Inventory/Reports/RptINV_CustomerReceive.rpt"))
                        ' objxmlout.Load("c:\CustomerChallan.xml")

                        objxmlout.LoadXml(Session("CustomerReport"))

                        '  objxmlout.Save("c:\CustomerChallan.xml")
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                    '***************** Code End For Customer Report ******************************


                    '####Code for the Inventory Reports-PC Installation,Misc Hardware and PC Summary
                Case Is = "INV_Pc"
                    If Session("eInventory_Pc") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        Dim objOutputXml, objInputXml As New XmlDocument
                        Dim objPcInstall As New AAMS.bizInventory.bzChallan
                        objInputXml.LoadXml(Session("eInventory_Pc"))

                        Dim xDoc As New XmlDocument
                        xDoc.LoadXml(objInputXml.OuterXml)
                        If xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText.Trim().Length <> 0 Then
                            Dim str As String() = xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText.Trim().Split("/")
                            Dim dt As String = ""
                            dt = str(1) & "/" & str(0) & "/" & str(2)
                            xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText = dt 'Format(CDate(dt), "MM/dd/yyyy") '.ToShortDateString("MM/dd/yyyy")
                        Else
                            xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText = "" 'Format(CDate(dt), "MM/dd/yyyy") '.ToShortDateString("MM/dd/yyyy")
                        End If
                        If xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText.Trim().Length <> 0 Then
                            Dim str1 As String() = xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText.Trim().Split("/")
                            Dim dt1 As String = ""
                            dt1 = str1(1) & "/" & str1(0) & "/" & str1(2)
                            xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText = dt1 'Format(CDate(xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText), "MM/dd/yyyy")
                        Else
                            xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText = ""
                        End If

                        objOutputXml = objPcInstall.PCInstallationReport(xDoc)
                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then


                            objRepDocument.Load(Server.MapPath("Inventory/Reports/RptINV_Pc.rpt"))
                            xreader = New XmlNodeReader(objOutputXml)
                            objds.ReadXml(xreader)
                            objRepDocument.SetDataSource(objds)
                            CRViewer1.EnableParameterPrompt = False
                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = False
                            'Session("MyPage") = objRepDocument
                        Else
                            ' Session("MyPage") = Nothing
                            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "INV_MiscHardware"
                    If Session("eInventory_MiscHardware") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then


                        Dim objOutputXml, objInputXml As New XmlDocument
                        Dim objMiscHardware As New AAMS.bizInventory.bzChallan
                        objInputXml.LoadXml(Session("eInventory_MiscHardware"))

                        Dim xDoc As New XmlDocument
                        xDoc.LoadXml(objInputXml.OuterXml)
                        If xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText.Trim().Length <> 0 Then
                            Dim str As String() = xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText.Trim().Split("/")
                            Dim dt As String = ""
                            dt = str(1) & "/" & str(0) & "/" & str(2)
                            'Dim dt As Date
                            'dt = CDate(MonthName(Mid(xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText, 4, 5)) & "/" & Left(xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText, 2) & "/" & Right(xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText, 4))
                            xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText = dt 'Format(CDate(dt), "MM/dd/yyyy") '.ToShortDateString("MM/dd/yyyy")
                        Else
                            xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText = "" 'Format(CDate(dt), "MM/dd/yyyy") '.ToShortDateString("MM/dd/yyyy")
                        End If
                        If xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText.Trim().Length <> 0 Then
                            Dim str1 As String() = xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText.Trim().Split("/")
                            Dim dt1 As String = ""
                            dt1 = str1(1) & "/" & str1(0) & "/" & str1(2)
                            xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText = dt1 'Format(CDate(xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText), "MM/dd/yyyy")
                        Else
                            xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText = ""
                        End If
                        objOutputXml = objMiscHardware.MISCHardwareReport(xDoc)
                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                            objRepDocument.Load(Server.MapPath("Inventory/Reports/RptINV_MiscHardware.rpt"))
                            xreader = New XmlNodeReader(objOutputXml)
                            objds.ReadXml(xreader)
                            objRepDocument.SetDataSource(objds)
                            CRViewer1.EnableParameterPrompt = False
                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = False
                            'Session("MyPage") = objRepDocument
                        Else
                            '  Session("MyPage") = Nothing
                            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "INV_PcSummary"
                    If Session("eInventory_PcSummary") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then

                        Dim objbzGodown As New AAMS.bizInventory.bzChallan
                        Dim InputXML As New XmlDocument, OutputXML As New XmlDocument
                        objRepDocument.Load(Server.MapPath("Inventory/Reports/RptINV_PcSummary1.rpt"))
                        InputXML.LoadXml(Session("eInventory_PcSummary"))
                        Dim xDoc As New XmlDocument
                        xDoc.LoadXml(InputXML.OuterXml)
                        If xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText.Trim().Length <> 0 Then
                            Dim str As String() = xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText.Trim().Split("/")
                            Dim dt As String = ""
                            dt = str(1) & "/" & str(0) & "/" & str(2)
                            xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText = dt 'Format(CDate(dt), "MM/dd/yyyy") '.ToShortDateString("MM/dd/yyyy")
                        Else
                            xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText = "" 'Format(CDate(dt), "MM/dd/yyyy") '.ToShortDateString("MM/dd/yyyy")
                        End If
                        If xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText.Trim().Length <> 0 Then
                            Dim str1 As String() = xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText.Trim().Split("/")
                            Dim dt1 As String = ""
                            dt1 = str1(1) & "/" & str1(0) & "/" & str1(2)
                            xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText = dt1 'Format(CDate(xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText), "MM/dd/yyyy")
                        Else
                            xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText = ""
                        End If
                        OutputXML = objbzGodown.PCSummaryReport(xDoc)
                        objxmlout.LoadXml(OutputXML.OuterXml)
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        ' CRViewer1.ReportSource = objRepDocument

                        Dim paramfields As New ParameterFields
                        Dim InstallationFrom_pField As New ParameterField()
                        Dim InstallationTo_pField As New ParameterField()
                        Dim discreteValFrom As New ParameterDiscreteValue()
                        Dim discreteValTo As New ParameterDiscreteValue()

                        InstallationFrom_pField = New ParameterField()
                        discreteValFrom = New ParameterDiscreteValue()
                        InstallationFrom_pField.ParameterFieldName = "p_InstallationUpto"
                        ' Set the first discrete value and pass it to the parameter
                        discreteValFrom.Value = InputXML.DocumentElement.SelectSingleNode("DATEFROM").InnerText.Trim()
                        InstallationFrom_pField.CurrentValues.Add(discreteValFrom)
                        InstallationTo_pField = New ParameterField()
                        discreteValTo = New ParameterDiscreteValue()
                        InstallationTo_pField.ParameterFieldName = "p_InstallationFrom"
                        discreteValTo.Value = InputXML.DocumentElement.SelectSingleNode("DATETO").InnerText.Trim()
                        InstallationTo_pField.CurrentValues.Add(discreteValTo)
                        paramfields.Add(InstallationFrom_pField)
                        paramfields.Add(InstallationTo_pField)
                        CRViewer1.ParameterFieldInfo = paramfields
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        '    ' CRViewer1.DataBind()
                        'End If
                        ' Modified on 22 August
                        BindReport(objRepDocument)
                    End If
                    'This Reports show is modified by Mukund on 25th March 2008

                    'Dim objbzGodown As New AAMS.bizInventory.bzChallan
                    'Dim InputXML As New XmlDocument, OutputXML As New XmlDocument

                    'If Session("eInventory_PcSummary") IsNot Nothing Then
                    '    ' If Not Page.IsPostBack Then
                    '        InputXML.LoadXml(Session("eInventory_PcSummary"))
                    '        Dim xDoc As New XmlDocument
                    '        xDoc.LoadXml(InputXML.OuterXml)

                    '        OutputXML = objbzGodown.PCSummaryReport(xDoc)

                    '        If OutputXML.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then


                    '            objRepDocument.Load(Server.MapPath("Inventory/Reports/RptINV_PcSummary1.rpt"))

                    '            If xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText.Trim().Length <> 0 Then
                    '                Dim str As String() = xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText.Trim().Split("/")
                    '                Dim dt As String = ""
                    '                dt = str(1) & "/" & str(0) & "/" & str(2)
                    '                xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText = dt 'Format(CDate(dt), "MM/dd/yyyy") '.ToShortDateString("MM/dd/yyyy")
                    '            Else
                    '                xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText = "" 'Format(CDate(dt), "MM/dd/yyyy") '.ToShortDateString("MM/dd/yyyy")
                    '            End If
                    '            If xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText.Trim().Length <> 0 Then
                    '                Dim str1 As String() = xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText.Trim().Split("/")
                    '                Dim dt1 As String = ""
                    '                dt1 = str1(1) & "/" & str1(0) & "/" & str1(2)
                    '                xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText = dt1 'Format(CDate(xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText), "MM/dd/yyyy")
                    '            Else
                    '                xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText = ""
                    '            End If
                    '            objxmlout.LoadXml(OutputXML.OuterXml)
                    '            xreader = New XmlNodeReader(objxmlout)
                    '            objds.ReadXml(xreader)
                    '            objRepDocument.SetDataSource(objds)
                    '            CRViewer1.EnableParameterPrompt = False
                    '            CRViewer1.DisplayGroupTree = False
                    '            CRViewer1.EnableViewState = False
                    '            'CRViewer1.ReportSource = objRepDocument

                    '            Dim paramfields As New ParameterFields
                    '            Dim InstallationFrom_pField As New ParameterField()
                    '            Dim InstallationTo_pField As New ParameterField()
                    '            Dim discreteValFrom As New ParameterDiscreteValue()
                    '            Dim discreteValTo As New ParameterDiscreteValue()

                    '            InstallationFrom_pField = New ParameterField()
                    '            discreteValFrom = New ParameterDiscreteValue()
                    '            InstallationFrom_pField.ParameterFieldName = "p_InstallationUpto"
                    '            ' Set the first discrete value and pass it to the parameter
                    '            discreteValFrom.Value = InputXML.DocumentElement.SelectSingleNode("DATEFROM").InnerText.Trim()
                    '            InstallationFrom_pField.CurrentValues.Add(discreteValFrom)
                    '            InstallationTo_pField = New ParameterField()
                    '            discreteValTo = New ParameterDiscreteValue()

                    '            InstallationTo_pField.ParameterFieldName = "p_InstallationFrom"
                    '            discreteValTo.Value = InputXML.DocumentElement.SelectSingleNode("DATETO").InnerText.Trim()
                    '            InstallationTo_pField.CurrentValues.Add(discreteValTo)
                    '            paramfields.Add(InstallationFrom_pField)
                    '            paramfields.Add(InstallationTo_pField)
                    '            'Session("MyPage") = objRepDocument
                    '        Else
                    '            Session("MyPage") = Nothing
                    '            lblError.Text = OutputXML.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    '        End If
                    '    Else
                    '        objRepDocument = CType(Session("MyPage"), ReportDocument)
                    '    End If
                    '    'Modified on 22 August
                    '    BindReport(objRepDocument)
                    'End If
                    'This Reports show is modified by Mukund on 25th March 2008
                Case Is = "InventoryPcDeInstallation"

                    If Session("MiscPcDeinstall") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then


                        Dim objPcDeInstall As New AAMS.bizInventory.bzChallan
                        objRepDocument.Load(Server.MapPath("Inventory/Reports/RptINV_PcDeinstall.rpt"))
                        Dim InputXML As New XmlDocument, OutputXML As New XmlDocument
                        'OutPut xml
                        '<INV_RPTPCDEINSTALL_OUTPUT><PCDEINSTALL LCODE='' AGENCYNAME='' OFFICEID='' CITY='' DATEINSTALLED='' CPUTYPE='' CPUNO='' MONTYPE ='' MONNO='' ONLINE_STATUS='' DATEDEINSTALLED='' /><Errors Status=''><Error Code='' Description=''/></Errors></INV_RPTPCDEINSTALL_OUTPUT>
                        InputXML.LoadXml(Session("MiscPcDeinstall"))


                        'For changing Format of Date 
                        Dim xDoc As New XmlDocument
                        xDoc.LoadXml(InputXML.OuterXml)
                        If xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText.Trim().Length <> 0 Then
                            Dim str As String() = xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText.Trim().Split("/")
                            Dim dt As String = ""
                            dt = str(1) & "/" & str(0) & "/" & str(2)
                            'Dim dt As Date
                            'dt = CDate(MonthName(Mid(xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText, 4, 5)) & "/" & Left(xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText, 2) & "/" & Right(xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText, 4))
                            xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText = dt 'Format(CDate(dt), "MM/dd/yyyy") '.ToShortDateString("MM/dd/yyyy")
                        Else
                            xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText = "" 'Format(CDate(dt), "MM/dd/yyyy") '.ToShortDateString("MM/dd/yyyy")

                        End If


                        If xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText.Trim().Length <> 0 Then
                            Dim str1 As String() = xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText.Trim().Split("/")
                            Dim dt1 As String = ""
                            dt1 = str1(1) & "/" & str1(0) & "/" & str1(2)
                            xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText = dt1 'Format(CDate(xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText), "MM/dd/yyyy")
                        Else
                            xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText = ""
                        End If

                        'For changing Format of Date 

                        OutputXML = objPcDeInstall.PCDeInstallationReport(xDoc)
                        'OutputXML.LoadXml("<INV_RPTPCDEINSTALL_OUTPUT><PCDEINSTALL LCODE='1' AGENCYNAME='Delhi Agency' OFFICEID='100' CITY='New Delhi' DATEINSTALLED='12/01/2005' CPUTYPE='Dell' CPUNO='CP34523' MONTYPE='Dell' MONNO='Mon435435' ONLINE_STATUS='Delivered' DATEDEINSTALLED='12/23/2006'/><PCDEINSTALL LCODE='2' AGENCYNAME='Delhi Agency' OFFICEID='1001' CITY='New Delhi' DATEINSTALLED='12/01/2005' CPUTYPE='Dell' CPUNO='CP34523' MONTYPE='Dell' MONNO='Mon435435' ONLINE_STATUS='Delivered' DATEDEINSTALLED='12/23/2006'/><PCDEINSTALL LCODE='3' AGENCYNAME='Agra Agency' OFFICEID='2001' CITY='Agra' DATEINSTALLED='12/01/2005' CPUTYPE='Dell' CPUNO='CP34523' MONTYPE='Dell' MONNO='Mon435435' ONLINE_STATUS='Delivered' DATEDEINSTALLED='12/23/2006'/><PCDEINSTALL LCODE='4' AGENCYNAME='Agra Agency' OFFICEID='2001' CITY='Agra' DATEINSTALLED='12/01/2005' CPUTYPE='HCL' CPUNO='CP34523' MONTYPE='Dell' MONNO='Mon435435' ONLINE_STATUS='Delivered' DATEDEINSTALLED='12/23/2006'/><Errors Status=''><Error Code='' Description=''/></Errors></INV_RPTPCDEINSTALL_OUTPUT>")
                        If OutputXML.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                            objxmlout.LoadXml(OutputXML.OuterXml) 'Session("eFeedback"))
                            xreader = New XmlNodeReader(objxmlout)
                            objds.ReadXml(xreader)
                            objRepDocument.SetDataSource(objds)
                            CRViewer1.EnableParameterPrompt = False
                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = False
                            'Session("MyPage") = objRepDocument
                        Else
                            ' Session("MyPage") = Nothing
                            lblError.Text = OutputXML.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If
                    'This Reports show is modified by Mukund on 25th March 2008
                Case Is = "InventoryMiscDeInstallation"

                    If Session("MiscDeInstall") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then

                        Dim objMiscDeInstall As New AAMS.bizInventory.bzChallan
                        objRepDocument.Load(Server.MapPath("Inventory/Reports/RptINV_MiscDeinstall.rpt"))
                        Dim InputXML As New XmlDocument, OutputXML As New XmlDocument
                        'OutPut xml
                        '<INV_RPTPCDEINSTALL_OUTPUT><PCDEINSTALL LCODE='' AGENCYNAME='' OFFICEID='' CITY='' DATEINSTALLED='' CPUTYPE='' CPUNO='' MONTYPE ='' MONNO='' ONLINE_STATUS='' DATEDEINSTALLED='' /><Errors Status=''><Error Code='' Description=''/></Errors></INV_RPTPCDEINSTALL_OUTPUT>
                        InputXML.LoadXml(Session("MiscDeInstall"))

                        Dim xDoc As New XmlDocument
                        xDoc.LoadXml(InputXML.OuterXml)
                        If xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText.Trim().Length <> 0 Then
                            Dim str As String() = xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText.Trim().Split("/")
                            Dim dt As String = ""
                            dt = str(1) & "/" & str(0) & "/" & str(2)
                            'Dim dt As Date
                            'dt = CDate(MonthName(Mid(xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText, 4, 5)) & "/" & Left(xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText, 2) & "/" & Right(xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText, 4))
                            xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText = dt 'Format(CDate(dt), "MM/dd/yyyy") '.ToShortDateString("MM/dd/yyyy")
                        Else
                            xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText = "" 'Format(CDate(dt), "MM/dd/yyyy") '.ToShortDateString("MM/dd/yyyy")

                        End If


                        If xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText.Trim().Length <> 0 Then
                            Dim str1 As String() = xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText.Trim().Split("/")
                            Dim dt1 As String = ""
                            dt1 = str1(1) & "/" & str1(0) & "/" & str1(2)
                            xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText = dt1 'Format(CDate(xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText), "MM/dd/yyyy")
                        Else
                            xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText = ""
                        End If









                        OutputXML = objMiscDeInstall.MiscDeInstallationReport(xDoc)
                        ' OutputXML.LoadXml("<INV_RPTPCDEINSTALL_OUTPUT><PCDEINSTALL LCODE='1' AGENCYNAME='Delhi Agency' OFFICEID='100' CITY='New Delhi' DATEINSTALLED='12/01/2005' CPUTYPE='Dell' CPUNO='CP34523' MONTYPE='Dell' MONNO='Mon435435' ONLINE_STATUS='Delivered' DATEDEINSTALLED='12/23/2006'/><PCDEINSTALL LCODE='2' AGENCYNAME='Delhi Agency' OFFICEID='1001' CITY='New Delhi' DATEINSTALLED='12/01/2005' CPUTYPE='Dell' CPUNO='CP34523' MONTYPE='Dell' MONNO='Mon435435' ONLINE_STATUS='Delivered' DATEDEINSTALLED='12/23/2006'/><PCDEINSTALL LCODE='3' AGENCYNAME='Agra Agency' OFFICEID='2001' CITY='Agra' DATEINSTALLED='12/01/2005' CPUTYPE='Dell' CPUNO='CP34523' MONTYPE='Dell' MONNO='Mon435435' ONLINE_STATUS='Delivered' DATEDEINSTALLED='12/23/2006'/><PCDEINSTALL LCODE='4' AGENCYNAME='Agra Agency' OFFICEID='2001' CITY='Agra' DATEINSTALLED='12/01/2005' CPUTYPE='HCL' CPUNO='CP34523' MONTYPE='Dell' MONNO='Mon435435' ONLINE_STATUS='Delivered' DATEDEINSTALLED='12/23/2006'/><Errors Status=''><Error Code='' Description=''/></Errors></INV_RPTPCDEINSTALL_OUTPUT>")
                        If OutputXML.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                            objxmlout.LoadXml(OutputXML.OuterXml) 'Session("eFeedback"))
                            xreader = New XmlNodeReader(objxmlout)
                            objds.ReadXml(xreader)
                            objRepDocument.SetDataSource(objds)
                            CRViewer1.EnableParameterPrompt = False
                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = False
                            'Session("MyPage") = objRepDocument
                        Else
                            ' Session("MyPage") = Nothing
                            lblError.Text = OutputXML.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If
                    'This Reports show is modified by Mukund on 25th March 2008
                Case Is = "InventoryMiscSummary"
                    '<INV_RPTPCINSTALL_INPUT><LCODE/><AGENCYNAME/><CITY/><COUNTRY/><AOFFICE/><REGION/><WHOLEGROUP/><ONLINESTATUS/><EQUIPMENTGROUP /><EQUIPMENTTYPE/><DATEFROM/><DATETO/><RESPONSIBLESTAFFID/></INV_RPTPCINSTALL_INPUT>

                    If Session("MiscSummary") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        Dim objMiscSummary As New AAMS.bizInventory.bzChallan
                        objRepDocument.Load(Server.MapPath("Inventory/Reports/RptINV_MiscSummary.rpt"))
                        Dim InputXML As New XmlDocument, OutputXML As New XmlDocument
                        'OutPut xml
                        '<INV_RPTPCDEINSTALL_OUTPUT><PCDEINSTALL LCODE='' AGENCYNAME='' OFFICEID='' CITY='' DATEINSTALLED='' CPUTYPE='' CPUNO='' MONTYPE ='' MONNO='' ONLINE_STATUS='' DATEDEINSTALLED='' /><Errors Status=''><Error Code='' Description=''/></Errors></INV_RPTPCDEINSTALL_OUTPUT>
                        InputXML.LoadXml(Session("MiscSummary"))



                        Dim xDoc As New XmlDocument
                        xDoc.LoadXml(InputXML.OuterXml)
                        If xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText.Trim().Length <> 0 Then
                            Dim str As String() = xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText.Trim().Split("/")
                            Dim dt As String = ""
                            dt = str(1) & "/" & str(0) & "/" & str(2)
                            'Dim dt As Date
                            'dt = CDate(MonthName(Mid(xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText, 4, 5)) & "/" & Left(xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText, 2) & "/" & Right(xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText, 4))
                            xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText = dt 'Format(CDate(dt), "MM/dd/yyyy") '.ToShortDateString("MM/dd/yyyy")
                        Else
                            xDoc.DocumentElement.SelectSingleNode("DATEFROM").InnerText = "" 'Format(CDate(dt), "MM/dd/yyyy") '.ToShortDateString("MM/dd/yyyy")

                        End If


                        If xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText.Trim().Length <> 0 Then
                            Dim str1 As String() = xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText.Trim().Split("/")
                            Dim dt1 As String = ""
                            dt1 = str1(1) & "/" & str1(0) & "/" & str1(2)
                            xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText = dt1 'Format(CDate(xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText), "MM/dd/yyyy")
                        Else
                            xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText = ""
                        End If




                        OutputXML = objMiscSummary.MISCSummaryReport(xDoc)
                        'OutputXML.LoadXml("<INV_RPTPCDEINSTALL_OUTPUT><PCDEINSTALL LCODE='1' AGENCYNAME='Delhi Agency' OFFICEID='100' CITY='New Delhi' DATEINSTALLED='12/01/2005' CPUTYPE='Dell' CPUNO='CP34523' MONTYPE='Dell' MONNO='Mon435435' ONLINE_STATUS='Delivered' DATEDEINSTALLED='12/23/2006'/><PCDEINSTALL LCODE='2' AGENCYNAME='Delhi Agency' OFFICEID='1001' CITY='New Delhi' DATEINSTALLED='12/01/2005' CPUTYPE='Dell' CPUNO='CP34523' MONTYPE='Dell' MONNO='Mon435435' ONLINE_STATUS='Delivered' DATEDEINSTALLED='12/23/2006'/><PCDEINSTALL LCODE='3' AGENCYNAME='Agra Agency' OFFICEID='2001' CITY='Agra' DATEINSTALLED='12/01/2005' CPUTYPE='Dell' CPUNO='CP34523' MONTYPE='Dell' MONNO='Mon435435' ONLINE_STATUS='Delivered' DATEDEINSTALLED='12/23/2006'/><PCDEINSTALL LCODE='4' AGENCYNAME='Agra Agency' OFFICEID='2001' CITY='Agra' DATEINSTALLED='12/01/2005' CPUTYPE='HCL' CPUNO='CP34523' MONTYPE='Dell' MONNO='Mon435435' ONLINE_STATUS='Delivered' DATEDEINSTALLED='12/23/2006'/><Errors Status=''><Error Code='' Description=''/></Errors></INV_RPTPCDEINSTALL_OUTPUT>")
                        objxmlout.LoadXml(OutputXML.OuterXml) 'Session("eFeedback"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        ' CRViewer1.ReportSource = objRepDocument

                        'Input fields 
                        Dim paramfields As New ParameterFields
                        Dim InstallationFrom_pField As New ParameterField()
                        Dim InstallationTo_pField As New ParameterField()
                        Dim discreteValFrom As New ParameterDiscreteValue()
                        Dim discreteValTo As New ParameterDiscreteValue()

                        InstallationFrom_pField = New ParameterField()
                        discreteValFrom = New ParameterDiscreteValue()
                        InstallationFrom_pField.ParameterFieldName = "p_InstallationUpto"
                        ' Set the first discrete value and pass it to the parameter
                        discreteValFrom.Value = InputXML.DocumentElement.SelectSingleNode("DATEFROM").InnerText.Trim()
                        InstallationFrom_pField.CurrentValues.Add(discreteValFrom)



                        InstallationTo_pField = New ParameterField()
                        discreteValTo = New ParameterDiscreteValue()
                        InstallationTo_pField.ParameterFieldName = "p_InstallationFrom"
                        discreteValTo.Value = InputXML.DocumentElement.SelectSingleNode("DATETO").InnerText.Trim()
                        InstallationTo_pField.CurrentValues.Add(discreteValTo)
                        paramfields.Add(InstallationFrom_pField)
                        paramfields.Add(InstallationTo_pField)
                        CRViewer1.ParameterFieldInfo = paramfields
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If
                    'This Reports show is modified by Mukund on 25th March 2008
                Case Is = "InventoryWarrantyStatus"
                    If Session("WarrantyStatus") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then


                        Dim objWarrantyStatus As New AAMS.bizInventory.bzChallan
                        objRepDocument.Load(Server.MapPath("Inventory/Reports/rptInv_WarrantyStatus.rpt"))
                        Dim InputXML As New XmlDocument, OutputXML As New XmlDocument
                        'OutPut xml
                        '<INV_RPTPCDEINSTALL_OUTPUT><PCDEINSTALL LCODE='' AGENCYNAME='' OFFICEID='' CITY='' DATEINSTALLED='' CPUTYPE='' CPUNO='' MONTYPE ='' MONNO='' ONLINE_STATUS='' DATEDEINSTALLED='' /><Errors Status=''><Error Code='' Description=''/></Errors></INV_RPTPCDEINSTALL_OUTPUT>
                        InputXML.LoadXml(Session("WarrantyStatus"))
                        OutputXML = objWarrantyStatus.Rpt_WarrantyStatus(InputXML)
                        If OutputXML.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                            ' OutputXML.LoadXml("<INV_RPTPCDEINSTALL_OUTPUT><PCDEINSTALL LCODE='1' AGENCYNAME='Delhi Agency' OFFICEID='100' CITY='New Delhi' DATEINSTALLED='12/01/2005' CPUTYPE='Dell' CPUNO='CP34523' MONTYPE='Dell' MONNO='Mon435435' ONLINE_STATUS='Delivered' DATEDEINSTALLED='12/23/2006'/><PCDEINSTALL LCODE='2' AGENCYNAME='Delhi Agency' OFFICEID='1001' CITY='New Delhi' DATEINSTALLED='12/01/2005' CPUTYPE='Dell' CPUNO='CP34523' MONTYPE='Dell' MONNO='Mon435435' ONLINE_STATUS='Delivered' DATEDEINSTALLED='12/23/2006'/><PCDEINSTALL LCODE='3' AGENCYNAME='Agra Agency' OFFICEID='2001' CITY='Agra' DATEINSTALLED='12/01/2005' CPUTYPE='Dell' CPUNO='CP34523' MONTYPE='Dell' MONNO='Mon435435' ONLINE_STATUS='Delivered' DATEDEINSTALLED='12/23/2006'/><PCDEINSTALL LCODE='4' AGENCYNAME='Agra Agency' OFFICEID='2001' CITY='Agra' DATEINSTALLED='12/01/2005' CPUTYPE='HCL' CPUNO='CP34523' MONTYPE='Dell' MONNO='Mon435435' ONLINE_STATUS='Delivered' DATEDEINSTALLED='12/23/2006'/><Errors Status=''><Error Code='' Description=''/></Errors></INV_RPTPCDEINSTALL_OUTPUT>")
                            objxmlout.LoadXml(OutputXML.OuterXml) 'Session("eFeedback"))
                            xreader = New XmlNodeReader(objxmlout)
                            objds.ReadXml(xreader)
                            objRepDocument.SetDataSource(objds)
                            CRViewer1.EnableParameterPrompt = False

                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = True
                            'Session("MyPage") = objRepDocument
                        Else
                            'Session("MyPage") = Nothing
                            lblError.Text = OutputXML.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                    'RptINV_MiscSummary.rpt
                    '####Code for the Inventory Reports-Stock Transfer Summary and Stock Transfer Detail
                Case Is = "StockTransferSummary"
                    If Session("eStockTransferDetail") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then


                        Dim objOutputXml, objInputXml As New XmlDocument
                        Dim objStockTransfer As New AAMS.bizInventory.bzChallan

                        objInputXml.LoadXml(Session("eStockTransferDetail"))

                        'Dim xDoc As New XmlDocument
                        'xDoc.LoadXml(objInputXml.OuterXml)
                        'If xDoc.DocumentElement.SelectSingleNode("ChallanDateFrom").InnerText.Trim().Length <> 0 Then
                        '    Dim str As String() = xDoc.DocumentElement.SelectSingleNode("ChallanDateFrom").InnerText.Trim().Split("/")
                        '    Dim dt As String = ""
                        '    dt = str(1) & "/" & str(0) & "/" & str(2)
                        '    xDoc.DocumentElement.SelectSingleNode("ChallanDateFrom").InnerText = dt
                        'Else
                        '    xDoc.DocumentElement.SelectSingleNode("ChallanDateFrom").InnerText = ""
                        'End If
                        'If xDoc.DocumentElement.SelectSingleNode("ChallanDateTo").InnerText.Trim().Length <> 0 Then
                        '    Dim str1 As String() = xDoc.DocumentElement.SelectSingleNode("ChallanDateTo").InnerText.Trim().Split("/")
                        '    Dim dt1 As String = ""
                        '    dt1 = str1(1) & "/" & str1(0) & "/" & str1(2)
                        '    xDoc.DocumentElement.SelectSingleNode("ChallanDateTo").InnerText = dt1 'Format(CDate(xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText), "MM/dd/yyyy")
                        'Else
                        '    xDoc.DocumentElement.SelectSingleNode("ChallanDateTo").InnerText = ""
                        'End If
                        'objOutputXml = objStockTransfer.rpt_TrackStockTransfer(xDoc)
                        objOutputXml = objStockTransfer.rpt_TrackStockTransfer(objInputXml)
                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                            objRepDocument.Load(Server.MapPath("Inventory/Reports/RptINV_StockTransferSummary.rpt"))
                            xreader = New XmlNodeReader(objOutputXml)
                            objds.ReadXml(xreader)
                            objRepDocument.SetDataSource(objds)
                            CRViewer1.EnableParameterPrompt = False
                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = False
                            'Session("MyPage") = objRepDocument
                        Else
                            ' Session("MyPage") = Nothing
                            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "StockTransferDetail"

                    If Session("eStockTransferSummary") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then


                        Dim objOutputXml, objInputXml As New XmlDocument
                        Dim objStockTransfer As New AAMS.bizInventory.bzChallan
                        objInputXml.LoadXml(Session("eStockTransferSummary"))

                        'Dim xDoc As New XmlDocument
                        'xDoc.LoadXml(objInputXml.OuterXml)
                        'If xDoc.DocumentElement.SelectSingleNode("ChallanDateFrom").InnerText.Trim().Length <> 0 Then
                        '    Dim str As String() = xDoc.DocumentElement.SelectSingleNode("ChallanDateFrom").InnerText.Trim().Split("/")
                        '    Dim dt As String = ""
                        '    dt = str(1) & "/" & str(0) & "/" & str(2)
                        '    xDoc.DocumentElement.SelectSingleNode("ChallanDateFrom").InnerText = dt
                        'Else
                        '    xDoc.DocumentElement.SelectSingleNode("ChallanDateFrom").InnerText = ""
                        'End If
                        'If xDoc.DocumentElement.SelectSingleNode("ChallanDateTo").InnerText.Trim().Length <> 0 Then
                        '    Dim str1 As String() = xDoc.DocumentElement.SelectSingleNode("ChallanDateTo").InnerText.Trim().Split("/")
                        '    Dim dt1 As String = ""
                        '    dt1 = str1(1) & "/" & str1(0) & "/" & str1(2)
                        '    xDoc.DocumentElement.SelectSingleNode("ChallanDateTo").InnerText = dt1 'Format(CDate(xDoc.DocumentElement.SelectSingleNode("DATETO").InnerText), "MM/dd/yyyy")
                        'Else
                        '    xDoc.DocumentElement.SelectSingleNode("ChallanDateTo").InnerText = ""
                        'End If

                        'objOutputXml = objStockTransfer.rpt_TrackStockTransfer(xDoc)
                        objOutputXml = objStockTransfer.rpt_TrackStockTransfer(objInputXml)
                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            objRepDocument.Load(Server.MapPath("Inventory/Reports/RptINV_StockTransferDetail.rpt"))
                            xreader = New XmlNodeReader(objOutputXml)
                            objds.ReadXml(xreader)
                            objRepDocument.SetDataSource(objds)
                            CRViewer1.EnableParameterPrompt = False
                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = False
                            'Session("MyPage") = objRepDocument
                        Else
                            '  Session("MyPage") = Nothing
                            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "HardwareInstallation"

                    If Session("Installation") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        Dim objbzInstallation As New AAMS.bizInventory.bzChallan
                        Dim objbzMiscInstallation As New AAMS.bizInventory.bzChallan
                        Dim objXmlReader, objXmlReader1 As XmlNodeReader
                        Dim ds, ds1 As New DataSet
                        objRepDocument.Load(Server.MapPath("Inventory/Reports/RptINV_HardwareInstallation.rpt"))

                        Dim InputXML As New XmlDocument, OutputXML As New XmlDocument, OutputXML1 As New XmlDocument

                        InputXML.LoadXml(Session("Installation"))

                        OutputXML = objbzInstallation.PCInstallationReport(InputXML)
                        OutputXML1 = objbzMiscInstallation.MISCHardwareReport(InputXML)
                        'Session("PCInstallation") = OutputXML
                        'Session("HardwareInstallation") = OutputXML1

                        'objxmlout.LoadXml("<RPT_MAIN>" & Session("PCInstallation") & Session("HardwareInstallation") & "</RPT_MAIN>")
                        'objxmlout.LoadXml("<RPT_MAIN>" & OutputXML & OutputXML1 & "</RPT_MAIN>") 'Session("eFeedback"))
                        'xreader = New XmlNodeReader(objxmlout)
                        'objds.ReadXml(xreader)
                        objXmlReader = New XmlNodeReader(OutputXML)
                        objXmlReader1 = New XmlNodeReader(OutputXML1)
                        ds.ReadXml(objXmlReader)
                        ds1.ReadXml(objXmlReader1)

                        objRepDocument.Subreports("RptINV_PCDetails.rpt").SetDataSource(ds)
                        objRepDocument.Subreports("RptINV_MiscHardwareDetails.rpt").SetDataSource(ds1)

                        'objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If


                Case Is = "MarketShareCity"

                    If Session("MarketShareCity") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objRepDocument.Load(Server.MapPath("Market/Reports/RptMarketShare.rpt"))
                        objxmlout.LoadXml(Session("MarketShareCity"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = True
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "MarketShareCountry"

                    If Session("MarketShareCountry") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objRepDocument.Load(Server.MapPath("Market/Reports/RptCountryMarketShare.rpt"))
                        objxmlout.LoadXml(Session("MarketShareCountry"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = True
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "MarketShareRegion"

                    If Session("MarketShareRegion") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objRepDocument.Load(Server.MapPath("Market/Reports/RptRegionMarketShare.rpt"))
                        objxmlout.LoadXml(Session("MarketShareRegion"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = True
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "CountryWiseDailyBookingReports"

                    If Session("CountryWiseBudget") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then

                        Dim objWarrantyStatus As New AAMS.bizProductivity.bzDailyBookings
                        objRepDocument.Load(Server.MapPath("Productivity/Reports/PRDRpt_DailyBookingCountryWise.rpt"))
                        Dim InputXML As New XmlDocument, OutputXML As New XmlDocument
                        InputXML.LoadXml(Session("CountryWiseBudget"))
                        OutputXML = objWarrantyStatus.DailyBookingsCountryWise(InputXML)
                        'OutputXML.LoadXml("<PR_RPT_DAILYBOOKINGS_COUNTRYWISE_OUTPUT><COUNTRYBREAKUP COUNTRY='India' BUDGET='12' ACTUAL='34' DIFFERENCE='45' CxlationRate='67' AVERAGEPERDAY='67' per_Changeover_LastYear='24' DAYSLEFT='56' SEGMENTSREQUIREDPERDAY='67' PROJECTION='67' TillDate='12/02/2006'/><COUNTRYBREAKUP COUNTRY='India' BUDGET='12' ACTUAL='34' DIFFERENCE='45' CxlationRate='67' AVERAGEPERDAY='67' per_Changeover_LastYear='24' DAYSLEFT='56' SEGMENTSREQUIREDPERDAY='67' PROJECTION='67' TillDate='12/02/2006'/><COUNTRYBREAKUP COUNTRY='India' BUDGET='12' ACTUAL='34' DIFFERENCE='45' CxlationRate='67' AVERAGEPERDAY='67' per_Changeover_LastYear='24' DAYSLEFT='56' SEGMENTSREQUIREDPERDAY='67' PROJECTION='67' TillDate='12/02/2006'/><INDIACONSOLIDATED TTLBUDGET='' TTLACTUAL='' DIFFERENCE='' AVERAGEPERDAY='' CxlationRate='' DaystoTarget='' DAYSLEFT='' SEGMENTSREQUIREDPERDAY='' PROJECTION='' /><DAILYBOOKINGS_COUNTRYWISE DATE1='12/02/2004' AIRBKGS='34' AIRXX='4' AIRNET='45' CARBKGS='45' HOTELBKG='45' TTL1='67' CxlationFactor='78' DATE2='10/2/2004' TTL2='457' Country='India'/><DAILYBOOKINGS_COUNTRYWISE DATE1='12/02/2004' AIRBKGS='34' AIRXX='4' AIRNET='45' CARBKGS='45' HOTELBKG='45' TTL1='67' CxlationFactor='78' DATE2='10/2/2004' TTL2='457' Country='India'/><DAILYBOOKINGS_COUNTRYWISE DATE1='12/02/2004' AIRBKGS='34' AIRXX='4' AIRNET='45' CARBKGS='45' HOTELBKG='45' TTL1='67' CxlationFactor='78' DATE2='10/2/2004' TTL2='457' Country='India'/><DAILYBOOKINGS_COUNTRYWISE DATE1='12/02/2004' AIRBKGS='34' AIRXX='4' AIRNET='45' CARBKGS='45' HOTELBKG='45' TTL1='67' CxlationFactor='78' DATE2='10/2/2004' TTL2='457' Country='India'/><DAILYBOOKINGS_COUNTRYWISE DATE1='12/02/2004' AIRBKGS='34' AIRXX='4' AIRNET='45' CARBKGS='45' HOTELBKG='45' TTL1='67' CxlationFactor='78' DATE2='10/2/2004' TTL2='457' Country='India'/><DAILYBOOKINGS_COUNTRYWISE DATE1='12/02/2004' AIRBKGS='34' AIRXX='4' AIRNET='45' CARBKGS='45' HOTELBKG='45' TTL1='67' CxlationFactor='78' DATE2='10/2/2004' TTL2='457' Country='Ind'/><DAILYBOOKINGS_COUNTRYWISE DATE1='12/02/2004' AIRBKGS='34' AIRXX='4' AIRNET='45' CARBKGS='45' HOTELBKG='45' TTL1='67' CxlationFactor='78' DATE2='10/2/2004' TTL2='457' Country='Ind'/><DAILYBOOKINGS_COUNTRYWISE DATE1='12/02/2004' AIRBKGS='34' AIRXX='4' AIRNET='45' CARBKGS='45' HOTELBKG='45' TTL1='67' CxlationFactor='78' DATE2='10/2/2004' TTL2='457' Country='Ind'/><DAILYBOOKINGS_COUNTRYWISE DATE1='12/02/2004' AIRBKGS='34' AIRXX='4' AIRNET='45' CARBKGS='45' HOTELBKG='45' TTL1='67' CxlationFactor='78' DATE2='10/2/2004' TTL2='457' Country='Ind'/><Errors Status='False'><Error Code='' Description='' /></Errors></PR_RPT_DAILYBOOKINGS_COUNTRYWISE_OUTPUT>")

                        If OutputXML.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            objxmlout.LoadXml(OutputXML.OuterXml)
                            xreader = New XmlNodeReader(objxmlout)
                            objds.ReadXml(xreader)
                            objRepDocument.SetDataSource(objds)

                            CRViewer1.EnableParameterPrompt = False
                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = True
                            'Session("MyPage") = objRepDocument
                        Else
                            ' Session("MyPage") = Nothing
                            lblError.Text = OutputXML.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If


                    ' objRepDocument.Subreports
                    'CRViewer1.EnableParameterPrompt = False
                    'CRViewer1.DisplayGroupTree = False
                    'CRViewer1.EnableViewState = False
                    'CRViewer1.ReportSource = objRepDocument
                    'CRViewer1.DataBind()

                Case Is = "DailyBookingCityWise"
                    If Session("CityWiseBudget") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        Dim objWarrantyStatus As New AAMS.bizProductivity.bzDailyBookings

                        Dim InputXML As New XmlDocument, OutputXML As New XmlDocument
                        InputXML.LoadXml(Session("CityWiseBudget"))

                        If InputXML.DocumentElement.SelectSingleNode("SummaryOption").InnerText.Trim() = "1" Then
                            objRepDocument.Load(Server.MapPath("Productivity/Reports/PRDRpt_DailBookingCityWise.rpt"))
                            SetParameters()
                        ElseIf InputXML.DocumentElement.SelectSingleNode("SummaryOption").InnerText.Trim() = "2" Then
                            objRepDocument.Load(Server.MapPath("Productivity/Reports/PRDRPT_DailyBookingCityGraph.rpt"))
                        ElseIf InputXML.DocumentElement.SelectSingleNode("SummaryOption").InnerText.Trim() = "3" Then
                            objRepDocument.Load(Server.MapPath("Productivity/Reports/PRDRPT_DailyBookingCityOfficeID.rpt"))
                            SetParameters()
                        ElseIf InputXML.DocumentElement.SelectSingleNode("SummaryOption").InnerText.Trim() = "5" Then
                            objRepDocument.Load(Server.MapPath("Productivity/Reports/PRDRPT_DailyBookingCityState.rpt"))
                            SetParameters()
                        ElseIf InputXML.DocumentElement.SelectSingleNode("SummaryOption").InnerText.Trim() = "4" Then
                            objRepDocument.Load(Server.MapPath("Productivity/Reports/PRDRPT_DailyBookingCityRegion.rpt"))
                            SetParameters()

                        End If
                        OutputXML = objWarrantyStatus.DailyBookingsCityWise(InputXML)
                        objxmlout.LoadXml(OutputXML.OuterXml)
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "CRSDETAILS"
                    If Session("CRSDETAILS") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then

                        objRepDocument.Load(Server.MapPath("Productivity/Reports/RptCRSDetails.rpt"))
                        objxmlout.LoadXml(Session("CRSDETAILS"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)

                        Dim dtCRSDetailsGraph As New DataTable("CRSDetailsGraph")

                        Dim drow As DataRow
                        '@Previous Code
                        'dtCRSDetailsGraph.Columns.Add("1A", GetType(Long))
                        'dtCRSDetailsGraph.Columns.Add("1B", GetType(Long))
                        'dtCRSDetailsGraph.Columns.Add("1G", GetType(Long))
                        'dtCRSDetailsGraph.Columns.Add("1P", GetType(Long))
                        'dtCRSDetailsGraph.Columns.Add("1W", GetType(Long))
                        'dtCRSDetailsGraph.Columns.Add("Total", GetType(Long))

                        'drow = dtCRSDetailsGraph.NewRow
                        'drow("1A") = objds.Tables("PAGE_TOTAL").Rows(0)("A1")
                        'drow("1B") = objds.Tables("PAGE_TOTAL").Rows(0)("B1")
                        'drow("1G") = objds.Tables("PAGE_TOTAL").Rows(0)("G1")
                        'drow("1P") = objds.Tables("PAGE_TOTAL").Rows(0)("P1")
                        'drow("1W") = objds.Tables("PAGE_TOTAL").Rows(0)("W1")
                        'drow("Total") = objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL")
                        'dtCRSDetailsGraph.Rows.Add(drow)
                        '@Previous Code


                        '@New Code

                        dtCRSDetailsGraph.Columns.Add("1AVal", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("1BVal", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("1GVal", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("1PVal", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("1WVal", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("TotalVal", GetType(Double))

                        dtCRSDetailsGraph.Columns.Add("1A", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("1B", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("1G", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("1P", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("1W", GetType(Double))


                        drow = dtCRSDetailsGraph.NewRow
                        drow("1AVal") = objds.Tables("PAGE_TOTAL").Rows(0)("A1")
                        drow("1BVal") = objds.Tables("PAGE_TOTAL").Rows(0)("B1")
                        drow("1GVal") = objds.Tables("PAGE_TOTAL").Rows(0)("G1")
                        drow("1PVal") = objds.Tables("PAGE_TOTAL").Rows(0)("P1")
                        drow("1WVal") = objds.Tables("PAGE_TOTAL").Rows(0)("W1")
                        drow("TotalVal") = objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL")



                        drow("1A") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("A1")) * 100) / Val(objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL"))
                        drow("1B") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("B1")) * 100) / Val(objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL"))
                        drow("1G") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("G1")) * 100) / Val(objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL"))
                        drow("1P") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("P1")) * 100) / Val(objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL"))
                        drow("1W") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("W1")) * 100) / Val(objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL"))


                        dtCRSDetailsGraph.Rows.Add(drow)
                        '@New Code


                        Dim dset As New DataSet
                        dset.Tables.Add(dtCRSDetailsGraph)

                        ' dset.WriteXmlSchema("c:\rptCRSDetailsGraph.xsd")
                        objRepDocument.SetDataSource(dset)
                        'objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = True
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If


                    'Added by Mukund on 30-05
                Case Is = "1aStaffTrainging"
                    If Session("1aStaffTraining") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        Dim obj1aTraining As New AAMS.bizTraining.bzTraining
                        Dim InputXML As New XmlDocument, OutputXML As New XmlDocument
                        objRepDocument.Load(Server.MapPath("Training/Reports/rpt1aStaffTraining.rpt"))
                        InputXML.LoadXml(Session("1aStaffTraining"))
                        OutputXML = obj1aTraining.rpt_1AStaffTraining(InputXML)
                        If OutputXML.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            xreader = New XmlNodeReader(OutputXML)
                            objds.ReadXml(xreader)
                            objRepDocument.SetDataSource(objds)
                            CRViewer1.EnableParameterPrompt = False
                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = True
                            CRViewer1.HasPageNavigationButtons = True
                            'Session("MyPage") = objRepDocument
                        Else
                            Session("MyPage") = Nothing
                            lblError.Text = OutputXML.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If


                Case Is = "MonthlyStaffTrainging"
                    If Session("MonthlyStaffTraining") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        Dim obj1aTraining As New AAMS.bizTraining.bzTraining
                        Dim InputXML As New XmlDocument, OutputXML As New XmlDocument
                        objRepDocument.Load(Server.MapPath("Training/Reports/rptMonthlyTraining.rpt"))
                        InputXML.LoadXml(Session("MonthlyStaffTraining"))
                        OutputXML = obj1aTraining.rpt_MonthlyTraining(InputXML)
                        If OutputXML.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            xreader = New XmlNodeReader(OutputXML)
                            objds.ReadXml(xreader)
                            objRepDocument.SetDataSource(objds)
                            CRViewer1.EnableParameterPrompt = False
                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = True
                            CRViewer1.HasPageNavigationButtons = True
                            'Session("MyPage") = objRepDocument
                        Else
                            Session("MyPage") = Nothing
                            lblError.Text = OutputXML.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If


                Case Is = "PendingTraining"
                    If Session("ePendingTraining") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        Dim objOutputXml, objInputXml As New XmlDocument
                        Dim objPendingTraining As New AAMS.bizTraining.bzTraining
                        objInputXml.LoadXml(Session("ePendingTraining"))
                        objOutputXml = objPendingTraining.rpt_PendingTraining(objInputXml)
                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            objRepDocument.Load(Server.MapPath("Training/Reports/rptPendingTraining.rpt"))
                            xreader = New XmlNodeReader(objOutputXml)
                            objds.ReadXml(xreader)
                            objRepDocument.SetDataSource(objds)
                            CRViewer1.EnableParameterPrompt = False
                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = False
                            CRViewer1.HasPageNavigationButtons = True
                            'Session("MyPage") = objRepDocument
                        Else
                            'Session("MyPage") = Nothing
                            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If

                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "RequestTraining"
                    If Session("eRequestTraining") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        Dim objOutputXml, objInputXml As New XmlDocument
                        Dim objPendingTraining As New AAMS.bizTraining.bzTraining
                        objInputXml.LoadXml(Session("eRequestTraining"))
                        objOutputXml = objPendingTraining.rpt_TrainingRequest(objInputXml)
                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            objRepDocument.Load(Server.MapPath("Training/Reports/rptRequestForTraining.rpt"))
                            xreader = New XmlNodeReader(objOutputXml)
                            objds.ReadXml(xreader)
                            objRepDocument.SetDataSource(objds)
                            CRViewer1.EnableParameterPrompt = False
                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = False
                            'Session("MyPage") = objRepDocument
                        Else
                            ' Session("MyPage") = Nothing
                            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If



                Case Is = "OrderFeasibilityReport"
                    If Session("eOrderFeasibilityReport") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        Dim objOutputXml, objInputXml As New XmlDocument
                        Dim objbzbzFeasibilityReport As New AAMS.bizTravelAgency.bzAgency
                        objInputXml.LoadXml(Session("eOrderFeasibilityReport"))

                        objOutputXml = objbzbzFeasibilityReport.OrderFeasibility(objInputXml)
                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            objRepDocument.Load(Server.MapPath("TravelAgency/Reports/rptOrderFeasibility.rpt"))
                            xreader = New XmlNodeReader(objOutputXml)
                            objds.ReadXml(xreader)
                            objRepDocument.SetDataSource(objds)
                            CRViewer1.EnableParameterPrompt = False
                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = False
                            'Session("MyPage") = objRepDocument
                        Else
                            '  Session("MyPage") = Nothing
                            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If


                Case Is = "AgencyTarget"
                    If Session("ATarget") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        Dim objOutputXml, inputXml As New XmlDocument
                        objOutputXml.LoadXml(Session("ATarget"))
                        inputXml.LoadXml(Session("IP_ATarget"))

                        objRepDocument.Load(Server.MapPath("TravelAgency/Reports/rptAgencyTarget.rpt"))
                        xreader = New XmlNodeReader(objOutputXml)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        Dim paramfields As New ParameterFields
                        Dim City_pField As New ParameterField()
                        Dim Month_pField As New ParameterField()
                        Dim Year_pField As New ParameterField()

                        Dim discreteValCity As New ParameterDiscreteValue()
                        Dim discreteValMonth As New ParameterDiscreteValue()
                        Dim discreteValYear As New ParameterDiscreteValue()

                        Dim E_ack As New Ack_Functions

                        City_pField.ParameterFieldName = "p_City"
                        Month_pField.ParameterFieldName = "p_Month"
                        Year_pField.ParameterFieldName = "p_Year"

                        ' Set the first discrete value and pass it to the parameter
                        discreteValCity.Value = inputXml.DocumentElement.SelectSingleNode("CityName").InnerText.Trim()

                        'discreteValMonth.Value = inputXml.DocumentElement.SelectSingleNode("Month").InnerText.Trim()
                        discreteValMonth.Value = E_ack.GetMonthName(inputXml.DocumentElement.SelectSingleNode("Month").InnerText.Trim())
                        discreteValYear.Value = inputXml.DocumentElement.SelectSingleNode("Year").InnerText.Trim()

                        City_pField.CurrentValues.Add(discreteValCity)
                        Month_pField.CurrentValues.Add(discreteValMonth)
                        Year_pField.CurrentValues.Add(discreteValYear)





                        paramfields.Add(City_pField)
                        paramfields.Add(Month_pField)
                        paramfields.Add(Year_pField)


                        CRViewer1.ParameterFieldInfo = paramfields
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If




                    ''Ashish                    
                Case Is = "TARPT_ReTaining"
                    If Session("TARPT_ReTaining") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objRepDocument.Load(Server.MapPath("Training/Reports/rptReTraining.rpt"))
                        objxmlout.LoadXml(Session("TARPT_ReTaining"))
                        'objxmlout.Load("c:\TR_SEARCH_STAFF_TRAINING_OUTPUT.xml")
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "TARPT_TainingWeekly"
                    If Session("TARPT_TainingWeekly") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objRepDocument.Load(Server.MapPath("Training/Reports/rptMainWeeklyTraning.rpt"))
                        objxmlout.LoadXml(Session("TARPT_TainingWeekly"))
                        'objxmlout.Load("c:\TR_SEARCH_STAFF_TRAINING_OUTPUT.xml")
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = True
                        CRViewer1.HasPageNavigationButtons = True
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "AgencyAirlineBreakupReport"
                    If Session("AgencyAirlineBreakupReport") IsNot Nothing Then

                        ' If Not Page.IsPostBack Then
                        objRepDocument.Load(Server.MapPath("Market/Reports/RptAgencyAirlineBreakup.rpt"))
                        objxmlout.LoadXml(Session("AgencyAirlineBreakupReport"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        Dim paramfields As New ParameterFields
                        Dim FromMonth As New ParameterField()
                        Dim ToMonth As New ParameterField()
                        Dim discreteValFrom As New ParameterDiscreteValue()
                        Dim discreteValTo As New ParameterDiscreteValue()
                        FromMonth = New ParameterField()
                        discreteValFrom = New ParameterDiscreteValue()
                        FromMonth.ParameterFieldName = "FromDate"
                        If Request.QueryString("FDate") IsNot Nothing Then
                            discreteValFrom.Value = Request.QueryString("FDate").ToString
                        End If

                        FromMonth.CurrentValues.Add(discreteValFrom)
                        ToMonth = New ParameterField()
                        discreteValTo = New ParameterDiscreteValue()
                        ToMonth.ParameterFieldName = "Todate"
                        If Request.QueryString("TDate") IsNot Nothing Then
                            discreteValTo.Value = Request.QueryString("TDate").ToString
                        End If

                        ToMonth.CurrentValues.Add(discreteValTo)
                        paramfields.Add(FromMonth)
                        paramfields.Add(ToMonth)
                        CRViewer1.ParameterFieldInfo = paramfields

                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = True
                        CRViewer1.HasPageNavigationButtons = True
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "QuestionPaper"

                    If Session("QuestionPaper") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objxmlout.LoadXml(Session("QuestionPaper"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.Load(Server.MapPath("Training/Reports/rptQuestionPaper.rpt"))
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = True
                        CRViewer1.HasPageNavigationButtons = True
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "MarketShareGraph"

                    If Session("MarketShareGraph") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objxmlout.LoadXml(Session("MarketShareGraph"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        Dim dtMarketShareGraph As New DataTable("MarketShareGraph")
                        Dim drow As DataRow
                        '@ Previous Code

                        'dtMarketShareGraph.Columns.Add("1A", GetType(Long))
                        'dtMarketShareGraph.Columns.Add("1B", GetType(Long))
                        'dtMarketShareGraph.Columns.Add("1G", GetType(Long))
                        'dtMarketShareGraph.Columns.Add("1P", GetType(Long))
                        'dtMarketShareGraph.Columns.Add("1W", GetType(Long))
                        'dtMarketShareGraph.Columns.Add("Total", GetType(Long))

                        'drow = dtMarketShareGraph.NewRow
                        'drow("1A") = objds.Tables("PAGE_TOTAL").Rows(0)("A")
                        'drow("1B") = objds.Tables("PAGE_TOTAL").Rows(0)("B")
                        'drow("1G") = objds.Tables("PAGE_TOTAL").Rows(0)("G")
                        'drow("1P") = objds.Tables("PAGE_TOTAL").Rows(0)("P")
                        'drow("1W") = objds.Tables("PAGE_TOTAL").Rows(0)("W")
                        'drow("Total") = objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL")
                        'dtMarketShareGraph.Rows.Add(drow)
                        '@ Previous Code

                        '@ new Code

                        dtMarketShareGraph.Columns.Add("1AVal", GetType(Double))
                        dtMarketShareGraph.Columns.Add("1BVal", GetType(Double))
                        dtMarketShareGraph.Columns.Add("1GVal", GetType(Double))
                        dtMarketShareGraph.Columns.Add("1PVal", GetType(Double))
                        dtMarketShareGraph.Columns.Add("1WVal", GetType(Double))
                        dtMarketShareGraph.Columns.Add("TotalVal", GetType(Double))

                        dtMarketShareGraph.Columns.Add("1A", GetType(Double))
                        dtMarketShareGraph.Columns.Add("1B", GetType(Double))
                        dtMarketShareGraph.Columns.Add("1G", GetType(Double))
                        dtMarketShareGraph.Columns.Add("1P", GetType(Double))
                        dtMarketShareGraph.Columns.Add("1W", GetType(Double))


                        drow = dtMarketShareGraph.NewRow

                        drow("1AVal") = objds.Tables("PAGE_TOTAL").Rows(0)("A")
                        drow("1BVal") = objds.Tables("PAGE_TOTAL").Rows(0)("B")
                        drow("1GVal") = objds.Tables("PAGE_TOTAL").Rows(0)("G")
                        drow("1PVal") = objds.Tables("PAGE_TOTAL").Rows(0)("P")
                        drow("1WVal") = objds.Tables("PAGE_TOTAL").Rows(0)("W")
                        drow("TotalVal") = objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL")

                        drow("1A") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("A")) * 100) / Val(objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL"))
                        drow("1B") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("B")) * 100) / Val(objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL"))
                        drow("1G") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("G")) * 100) / Val(objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL"))
                        drow("1P") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("P")) * 100) / Val(objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL"))
                        drow("1W") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("W")) * 100) / Val(objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL"))

                        dtMarketShareGraph.Rows.Add(drow)

                        '@ new Code

                        Dim dset As New DataSet
                        dset.Tables.Add(dtMarketShareGraph)
                        ' dset.WriteXmlSchema("c:\rptMktShareGraph.xsd")
                        objRepDocument.Load(Server.MapPath("Market/Reports/RptMarketShareGraph.rpt"))
                        '   objRepDocument.SetDataSource(objds)
                        objRepDocument.SetDataSource(dtMarketShareGraph)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = True
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "ALLCRSMIDTGRAPH"
                    If Session("ALLCRSMIDTGRAPH") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then

                        objRepDocument.Load(Server.MapPath("Productivity/Reports/PRDrptAllCRSMIDT.rpt"))
                        objxmlout.LoadXml(Session("ALLCRSMIDTGRAPH"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)


                        Dim totalCRSproductivit As Double = Val(objds.Tables("PAGE_TOTAL").Rows(0)("A1"))
                        totalCRSproductivit += Val(objds.Tables("PAGE_TOTAL").Rows(0)("B1"))
                        totalCRSproductivit += Val(objds.Tables("PAGE_TOTAL").Rows(0)("G1"))
                        totalCRSproductivit += Val(objds.Tables("PAGE_TOTAL").Rows(0)("P1"))
                        totalCRSproductivit += Val(objds.Tables("PAGE_TOTAL").Rows(0)("W1"))




                        Dim dtCRSDetailsGraph As New DataTable("PAGE_TOTAL")
                        Dim drow As DataRow
                        dtCRSDetailsGraph.Columns.Add("1A", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("1B", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("1G", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("1P", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("1W", GetType(Double))



                        dtCRSDetailsGraph.Columns.Add("1AVal", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("1BVal", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("1GVal", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("1PVal", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("1WVal", GetType(Double))

                        dtCRSDetailsGraph.Columns.Add("TotalVal", GetType(Double))



                        drow = dtCRSDetailsGraph.NewRow
                        drow("1A") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("A1")) * 100) / totalCRSproductivit
                        drow("1B") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("B1")) * 100) / totalCRSproductivit
                        drow("1G") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("G1")) * 100) / totalCRSproductivit
                        drow("1P") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("P1")) * 100) / totalCRSproductivit
                        drow("1W") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("W1")) * 100) / totalCRSproductivit


                        drow("1AVal") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("A1")))
                        drow("1BVal") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("B1")))
                        drow("1GVal") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("G1")))
                        drow("1PVal") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("P1")))
                        drow("1WVal") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("W1")))

                        drow("TotalVal") = totalCRSproductivit '(Val(objds.Tables("PAGE_TOTAL").Rows(0)("W1")))

                        dtCRSDetailsGraph.Rows.Add(drow)

                        ' dtCRSDetailsGraph.WriteXmlSchema("C:\Admin\midt.xsd")

                        totalCRSproductivit = 0

                        Dim dset As New DataSet
                        dset.Tables.Add(dtCRSDetailsGraph)
                        objRepDocument.SetDataSource(dset)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = True
                        'Session("MyPage") = objRepDocument
                        ' Session("ALLCRSMIDTGRAPH") = Nothing
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        BindReport(objRepDocument)
                    End If

                    ''aDDED
                Case Is = "ALLCRSMIDTGRAPHDUBAI"
                    If Session("ALLCRSMIDTGRAPHDUBAI") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then

                        objRepDocument.Load(Server.MapPath("Productivity/Reports/PRDrptAllCRSMIDTDUBAI.rpt"))
                        objxmlout.LoadXml(Session("ALLCRSMIDTGRAPHDUBAI"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)


                        Dim totalCRSproductivit As Double = Val(objds.Tables("PAGE_TOTAL").Rows(0)("A1"))
                        totalCRSproductivit += Val(objds.Tables("PAGE_TOTAL").Rows(0)("B1"))
                        totalCRSproductivit += Val(objds.Tables("PAGE_TOTAL").Rows(0)("G1"))
                        totalCRSproductivit += Val(objds.Tables("PAGE_TOTAL").Rows(0)("P1"))
                        totalCRSproductivit += Val(objds.Tables("PAGE_TOTAL").Rows(0)("W1"))




                        Dim dtCRSDetailsGraph As New DataTable("PAGE_TOTAL")
                        Dim drow As DataRow
                        dtCRSDetailsGraph.Columns.Add("1A", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("1B", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("1G", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("1P", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("1W", GetType(Double))



                        dtCRSDetailsGraph.Columns.Add("1AVal", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("1BVal", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("1GVal", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("1PVal", GetType(Double))
                        dtCRSDetailsGraph.Columns.Add("1WVal", GetType(Double))

                        dtCRSDetailsGraph.Columns.Add("TotalVal", GetType(Double))



                        drow = dtCRSDetailsGraph.NewRow
                        drow("1A") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("A1")) * 100) / totalCRSproductivit
                        drow("1B") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("B1")) * 100) / totalCRSproductivit
                        drow("1G") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("G1")) * 100) / totalCRSproductivit
                        drow("1P") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("P1")) * 100) / totalCRSproductivit
                        drow("1W") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("W1")) * 100) / totalCRSproductivit


                        drow("1AVal") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("A1")))
                        drow("1BVal") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("B1")))
                        drow("1GVal") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("G1")))
                        drow("1PVal") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("P1")))
                        drow("1WVal") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("W1")))

                        drow("TotalVal") = totalCRSproductivit '(Val(objds.Tables("PAGE_TOTAL").Rows(0)("W1")))

                        dtCRSDetailsGraph.Rows.Add(drow)

                        ' dtCRSDetailsGraph.WriteXmlSchema("C:\Admin\midt.xsd")

                        totalCRSproductivit = 0

                        Dim dset As New DataSet
                        dset.Tables.Add(dtCRSDetailsGraph)
                        objRepDocument.SetDataSource(dset)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = True
                        'Session("MyPage") = objRepDocument
                        ' Session("ALLCRSMIDTGRAPH") = Nothing
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        BindReport(objRepDocument)
                    End If
                    ''aDDED
                Case Is = "FeedbackTopics"
                    If Session("FeedbackTopics") IsNot Nothing Then
                        objxmlout.LoadXml(Session("FeedbackTopics"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.Load(Server.MapPath("Training/Reports/rptPrintFeedBackTopics.rpt"))
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = True
                        CRViewer1.HasPageNavigationButtons = True

                        Dim paramfields As New ParameterFields

                        Dim Option1 As New ParameterField()
                        Dim Option2 As New ParameterField()
                        Dim Option3 As New ParameterField()
                        Dim Option4 As New ParameterField()
                        Dim Option5 As New ParameterField()

                        Dim discreteValOption1 As New ParameterDiscreteValue()
                        Dim discreteValOption2 As New ParameterDiscreteValue()
                        Dim discreteValOption3 As New ParameterDiscreteValue()
                        Dim discreteValOption4 As New ParameterDiscreteValue()
                        Dim discreteValOption5 As New ParameterDiscreteValue()



                        Dim E_ack As New Ack_Functions


                        Option1.ParameterFieldName = "opt1"
                        Option2.ParameterFieldName = "opt2"
                        Option3.ParameterFieldName = "opt3"
                        Option4.ParameterFieldName = "opt4"
                        Option5.ParameterFieldName = "opt5"

                        If objxmlout.DocumentElement.SelectNodes("PTMOOD[@TR_PART_MOOD_ORDER='1']").Count > 0 Then
                            discreteValOption1.Value = objxmlout.DocumentElement.SelectSingleNode("PTMOOD[@TR_PART_MOOD_ORDER='1']").Attributes("TR_PART_MOOD_NAME").Value.Trim()
                        Else
                            discreteValOption1.Value = ""
                        End If
                        If objxmlout.DocumentElement.SelectNodes("PTMOOD[@TR_PART_MOOD_ORDER='2']").Count > 0 Then
                            discreteValOption2.Value = objxmlout.DocumentElement.SelectSingleNode("PTMOOD[@TR_PART_MOOD_ORDER='2']").Attributes("TR_PART_MOOD_NAME").Value.Trim()
                        Else
                            discreteValOption2.Value = ""
                        End If
                        If objxmlout.DocumentElement.SelectNodes("PTMOOD[@TR_PART_MOOD_ORDER='3']").Count > 0 Then
                            discreteValOption3.Value = objxmlout.DocumentElement.SelectSingleNode("PTMOOD[@TR_PART_MOOD_ORDER='3']").Attributes("TR_PART_MOOD_NAME").Value.Trim()
                        Else
                            discreteValOption3.Value = ""
                        End If
                        If objxmlout.DocumentElement.SelectNodes("PTMOOD[@TR_PART_MOOD_ORDER='4']").Count > 0 Then
                            discreteValOption4.Value = objxmlout.DocumentElement.SelectSingleNode("PTMOOD[@TR_PART_MOOD_ORDER='4']").Attributes("TR_PART_MOOD_NAME").Value.Trim()
                        Else
                            discreteValOption4.Value = ""
                        End If
                        If objxmlout.DocumentElement.SelectNodes("PTMOOD[@TR_PART_MOOD_ORDER='5']").Count > 0 Then
                            discreteValOption5.Value = objxmlout.DocumentElement.SelectSingleNode("PTMOOD[@TR_PART_MOOD_ORDER='5']").Attributes("TR_PART_MOOD_NAME").Value.Trim()
                        Else
                            discreteValOption5.Value = ""
                        End If


                        Option1.CurrentValues.Add(discreteValOption1)
                        Option2.CurrentValues.Add(discreteValOption2)
                        Option3.CurrentValues.Add(discreteValOption3)
                        Option4.CurrentValues.Add(discreteValOption4)
                        Option5.CurrentValues.Add(discreteValOption5)

                        paramfields.Add(Option1)
                        paramfields.Add(Option2)
                        paramfields.Add(Option3)
                        paramfields.Add(Option4)
                        paramfields.Add(Option5)
                        CRViewer1.ParameterFieldInfo = paramfields

                        BindReport(objRepDocument)
                    End If

                    ' Code For Birdres Report

                Case Is = "BirdresHelpDeskDynamicReport"

                    If Session("BirdresHelpDeskDynamicReport") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then

                        objxmlout.LoadXml(Session("BirdresHelpDeskDynamicReport"))

                        '    objRepDocument.Load(Server.MapPath("HelpDesk/Reports/RptHelpDeskDynamicReport.rpt"))


                        If objxmlout.DocumentElement.SelectNodes("SEARCHDESC[@Ltr!='']").Count > 0 And objxmlout.DocumentElement.SelectNodes("SEARCHSOLN[@Ltr!='']").Count > 0 Then
                            objRepDocument.Load(Server.MapPath("BirdresHelpDesk/Reports/RptHelpDeskDynamicReport.rpt"))
                        End If
                        If objxmlout.DocumentElement.SelectNodes("SEARCHDESC[@Ltr!='']").Count <= 0 And objxmlout.DocumentElement.SelectNodes("SEARCHSOLN[@Ltr!='']").Count > 0 Then
                            objRepDocument.Load(Server.MapPath("BirdresHelpDesk/Reports/RptHelpDeskDynamicReportWithSoln.rpt"))
                        End If
                        If objxmlout.DocumentElement.SelectNodes("SEARCHDESC[@Ltr!='']").Count > 0 And objxmlout.DocumentElement.SelectNodes("SEARCHSOLN[@Ltr!='']").Count <= 0 Then
                            objRepDocument.Load(Server.MapPath("BirdresHelpDesk/Reports/RptHelpDeskDynamicReportWithDesc.rpt"))
                        End If

                        If objxmlout.DocumentElement.SelectNodes("SEARCHDESC[@Ltr!='']").Count <= 0 And objxmlout.DocumentElement.SelectNodes("SEARCHSOLN[@Ltr!='']").Count <= 0 Then
                            objRepDocument.Load(Server.MapPath("BirdresHelpDesk/Reports/RptHelpDeskDynamicReportWithoutSolnAndDesc.rpt"))
                        End If



                        'objxmlout.LoadXml("<HD_DMSEARCH_CALLREQUEST_OUTPUT><HEADER FIELD1='LTR_NO' FIELD2='AGENCY' FIELD3='ADDRESS' FIELD4='PTR_NO' FIELD5='LAST_CALL' FIELD6='QUERY_STATUS' FIELD7='FOLLOWUP' FIELD8='SEVERITY' FIELD9='LOGGED_BY' FIELD10='CLOSE_DATE_FROM' FIELD11='CLOSE_DATE_TO' FIELD12='OPEN_DATE_FROM' FIELD13='OPEN_DATE_TO' FIELD14='ASSIGNED_TO' FIELD15='ASSIGNED_DATE_TIME' FIELD16='OFFICE_ID' FIELD17='CUSTOMER_CATEGORY' FIELD18='CALLER_NAME' FIELD19='QUERY_GROUP' FIELD20='QUERY_SUB_GROUP' FIELD21='QUERY_CATEGORY' FIELD22='QUERY_SUB_CATEGORY' FIELD23='COORDINATOR1'  FIELD24='COORDINATOR2' FIELD25='DISPOSITION' FIELD26='1AOFFICE' FIELD27='AGENCY_1A_OFFICE' FIELD28='WORK_ORDER_NO'><DETAIL VALUE1='554' VALUE2='fgfdd879576546gdfg' VALUE3='fgfg' VALUE4='fgfdg' VALUE5='' VALUE6='' VALUE7='' VALUE8='' VALUE9='' VALUE10='' VALUE11='' VALUE12='' VALUE13='' VALUE14='' VALUE15='' VALUE16='' VALUE17='' VALUE18='' VALUE19='' VALUE20='' VALUE21='' VALUE22='' 					VALUE23='' VALUE24='' VALUE25='' VALUE26='' VALUE27='' VALUE28=''/><DETAIL VALUE1='55674' VALUE2='54654654' VALUE3='fgfg' VALUE4='fgfdg' VALUE5='' VALUE6='' VALUE7='' VALUE8='' VALUE9='' VALUE10='' VALUE11='' VALUE12='' VALUE13='' VALUE14='' VALUE15='7' VALUE16='' VALUE17='' VALUE18='' VALUE19='' VALUE20='' VALUE21='' VALUE22='' VALUE23='' VALUE24='' VALUE25='' VALUE26='' VALUE27='' VALUE28=''/><DETAIL VALUE1='567654' VALUE2='fgfhfghfghfggdfg' VALUE3='fgfg657567656657 546  566765756765765765765757' VALUE4='fgfdg' VALUE5='' VALUE6='' VALUE7='' VALUE8='' VALUE9='' VALUE10='' VALUE11='' VALUE12='' VALUE13='' VALUE14='' VALUE15='' VALUE16='' VALUE17='' VALUE18='' VALUE19='' VALUE20='' VALUE21='' VALUE22=''	VALUE23='' VALUE24='' VALUE25='' VALUE26='' VALUE27='h' VALUE28='66'/>	</HEADER><Errors Status=''>	<Error Code='' Description=''/>	</Errors></HD_DMSEARCH_CALLREQUEST_OUTPUT>")
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "BRFeedback"
                    If Session("eBRFeedback") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        Dim objfb As New AAMS.bizBRHelpDesk.bzFeedback
                        objRepDocument.Load(Server.MapPath("BirdresHelpDesk/Reports/RPTFeedback.rpt"))
                        Dim InputXML As New XmlDocument, OutputXML As New XmlDocument
                        'InputXML.LoadXml("<HD_RPTFEEDBACK_INPUT><MONTH>03</MONTH><YEAR>2008</YEAR></HD_RPTFEEDBACK_INPUT>")
                        InputXML.LoadXml(Session("eBRFeedback"))

                        OutputXML = objfb.FeedbackReport(InputXML)

                        If OutputXML.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                            '@ Remove Node On the basis of Report Type 
                            If Request.QueryString("Action").ToString().Trim = "F" Then
                                Dim i As Integer = 0
                                For Each objnode As XmlNode In OutputXML.DocumentElement.SelectNodes("SECONDFOURTH")
                                    If i <> 0 Then
                                        OutputXML.DocumentElement.RemoveChild(objnode)
                                    End If
                                    i = i + 1
                                Next
                                i = 0
                                For Each objnode As XmlNode In OutputXML.DocumentElement.SelectNodes("MONTHLY")
                                    If i <> 0 Then
                                        OutputXML.DocumentElement.RemoveChild(objnode)
                                    End If
                                    i = i + 1
                                Next

                            ElseIf Request.QueryString("Action").ToString().Trim = "S" Then
                                Dim i As Integer = 0
                                For Each objnode As XmlNode In OutputXML.DocumentElement.SelectNodes("FIRSTFOURTH")
                                    If i <> 0 Then
                                        OutputXML.DocumentElement.RemoveChild(objnode)
                                    End If
                                    i = i + 1
                                Next
                                i = 0
                                For Each objnode As XmlNode In OutputXML.DocumentElement.SelectNodes("MONTHLY")
                                    If i <> 0 Then
                                        OutputXML.DocumentElement.RemoveChild(objnode)
                                    End If
                                    i = i + 1
                                Next

                            ElseIf Request.QueryString("Action").ToString().Trim = "M" Then
                                Dim i As Integer = 0
                                For Each objnode As XmlNode In OutputXML.DocumentElement.SelectNodes("SECONDFOURTH")
                                    If i <> 0 Then
                                        OutputXML.DocumentElement.RemoveChild(objnode)
                                    End If
                                    i = i + 1
                                Next
                                i = 0
                                For Each objnode As XmlNode In OutputXML.DocumentElement.SelectNodes("FIRSTFOURTH")
                                    If i <> 0 Then
                                        OutputXML.DocumentElement.RemoveChild(objnode)
                                    End If
                                    i = i + 1
                                Next

                            End If
                            objxmlout.LoadXml(OutputXML.OuterXml) 'Session("eFeedback"))
                            xreader = New XmlNodeReader(objxmlout)
                            objds.ReadXml(xreader)
                            objRepDocument.SetDataSource(objds)
                            CRViewer1.EnableParameterPrompt = False
                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = False
                            ' CRViewer1.ReportSource = objRepDocument

                            Dim paramfields As New ParameterFields
                            Dim InstallationFrom_pField As New ParameterField()
                            Dim InstallationTo_pField As New ParameterField()
                            Dim discreteValFrom As New ParameterDiscreteValue()
                            Dim discreteValTo As New ParameterDiscreteValue()

                            InstallationFrom_pField = New ParameterField()
                            discreteValFrom = New ParameterDiscreteValue()
                            InstallationFrom_pField.ParameterFieldName = "RptTypeNew"
                            ' Set the first discrete value and pass it to the parameter
                            discreteValFrom.Value = Request.QueryString("Action").Trim
                            InstallationFrom_pField.CurrentValues.Add(discreteValFrom)
                            paramfields.Add(InstallationFrom_pField)
                            CRViewer1.ParameterFieldInfo = paramfields


                            'Dim paramfields As New ParameterFields
                            'Dim Pra_RptType As New ParameterField()
                            'Dim Pra_Year As New ParameterField()
                            'Dim discreteValFrom As New ParameterDiscreteValue()
                            'Dim discreteValTo As New ParameterDiscreteValue()

                            'Pra_RptType = New ParameterField()
                            'discreteValFrom = New ParameterDiscreteValue()
                            'Pra_RptType.ParameterFieldName = "RptTypeNew"
                            'discreteValFrom.Value = Request.QueryString("Action").Trim

                            'Pra_RptType.CurrentValues.Add(Pra_RptType)
                            'paramfields.Add(Pra_RptType)
                            'CRViewer1.ParameterFieldInfo = paramfields
                            'Session("MyPage") = objRepDocument
                        Else
                            ' Session("MyPage") = Nothing
                            lblError.Text = OutputXML.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "TechReport"
                    If Session("TechReport") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objxmlout.LoadXml(Session("TechReport"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)


                        Dim dtTectReport As New DataTable("DETAILS")
                        Dim drow As DataRow

                        dtTectReport.Columns.Add("Region", GetType(String))
                        dtTectReport.Columns.Add("State", GetType(String))
                        dtTectReport.Columns.Add("City", GetType(String))
                        dtTectReport.Columns.Add("A1Office", GetType(String))
                        dtTectReport.Columns.Add("Agency1AOffice", GetType(String))
                        dtTectReport.Columns.Add("AgencyName", GetType(String))
                        dtTectReport.Columns.Add("OfficeId", GetType(String))
                        dtTectReport.Columns.Add("LoggedBy", GetType(String))
                        dtTectReport.Columns.Add("Callername", GetType(String))
                        dtTectReport.Columns.Add("Customercategory", GetType(String))
                        dtTectReport.Columns.Add("QueryGroup", GetType(String))
                        dtTectReport.Columns.Add("QuerySubGroup", GetType(String))
                        dtTectReport.Columns.Add("QueryCategory", GetType(String))
                        dtTectReport.Columns.Add("QuerySubCategory", GetType(String))
                        dtTectReport.Columns.Add("QueryStatus", GetType(String))
                        dtTectReport.Columns.Add("QueryPriority", GetType(String))
                        dtTectReport.Columns.Add("DateassignedTime", GetType(DateTime)).AllowDBNull = True
                        dtTectReport.Columns.Add("DateopenFrom", GetType(DateTime)).AllowDBNull = True
                        dtTectReport.Columns.Add("DateopenTo", GetType(DateTime)).AllowDBNull = True
                        dtTectReport.Columns.Add("ClosedateFrom", GetType(DateTime)).AllowDBNull = True
                        dtTectReport.Columns.Add("ClosedateTo", GetType(DateTime)).AllowDBNull = True
                        dtTectReport.Columns.Add("PRTNo", GetType(String))
                        dtTectReport.Columns.Add("LTRNo", GetType(String))
                        dtTectReport.Columns.Add("CallAssignedTo", GetType(String))
                        dtTectReport.Columns.Add("Co_ordinator1", GetType(String))
                        dtTectReport.Columns.Add("Workorderno", GetType(String))
                        dtTectReport.Columns.Add("contacttype", GetType(String))
                        dtTectReport.Columns.Add("Discription", GetType(String))
                        dtTectReport.Columns.Add("Solution", GetType(String))

                        For i As Integer = 0 To objds.Tables("DETAILS").Rows.Count - 1
                            drow = dtTectReport.NewRow

                            drow("Region") = objds.Tables("DETAILS").Rows(i)("Region")
                            drow("State") = objds.Tables("DETAILS").Rows(i)("State")
                            drow("City") = objds.Tables("DETAILS").Rows(i)("City")
                            drow("A1Office") = objds.Tables("DETAILS").Rows(i)("A1Office")
                            drow("Agency1AOffice") = objds.Tables("DETAILS").Rows(i)("Agency1AOffice")
                            drow("AgencyName") = objds.Tables("DETAILS").Rows(i)("AgencyName")
                            drow("OfficeId") = objds.Tables("DETAILS").Rows(i)("OfficeId")
                            drow("LoggedBy") = objds.Tables("DETAILS").Rows(i)("LoggedBy")
                            drow("Callername") = objds.Tables("DETAILS").Rows(i)("Callername")
                            drow("Customercategory") = objds.Tables("DETAILS").Rows(i)("Customercategory")
                            drow("QueryGroup") = objds.Tables("DETAILS").Rows(i)("QueryGroup")
                            drow("QuerySubGroup") = objds.Tables("DETAILS").Rows(i)("QuerySubGroup")
                            drow("QueryCategory") = objds.Tables("DETAILS").Rows(i)("QueryCategory")
                            drow("QuerySubCategory") = objds.Tables("DETAILS").Rows(i)("QuerySubCategory")
                            drow("QueryStatus") = objds.Tables("DETAILS").Rows(i)("QueryStatus")
                            drow("QueryPriority") = objds.Tables("DETAILS").Rows(i)("QueryPriority")
                            If objds.Tables("DETAILS").Rows(i)("DateassignedTime").ToString.Trim.Length > 0 Then
                                drow("DateassignedTime") = objds.Tables("DETAILS").Rows(i)("DateassignedTime") ' objeAAMS.GetDateFormat(objds.Tables("DETAILS").Rows(i)("DateassignedTime"), "dd-MMM-yy HH:MM", "dd-MMM-yy hh:mm tt", "-")
                            End If
                            If objds.Tables("DETAILS").Rows(i)("DateopenFrom").ToString.Trim.Length > 0 Then
                                drow("DateopenFrom") = objds.Tables("DETAILS").Rows(i)("DateopenFrom") ' objeAAMS.GetDateFormat(objds.Tables("DETAILS").Rows(i)("DateopenFrom"), "dd-MMM-yy HH:MM", "dd-MMM-yy hh:mm tt", "-")
                            End If
                            If objds.Tables("DETAILS").Rows(i)("DateopenTo").ToString.Trim.Length > 0 Then
                                drow("DateopenTo") = objds.Tables("DETAILS").Rows(i)("DateopenTo") ' objeAAMS.GetDateFormat(objds.Tables("DETAILS").Rows(i)("DateopenTo"), "dd-MMM-yy HH:MM", "dd-MMM-yy hh:mm tt", "-")
                            End If
                            If objds.Tables("DETAILS").Rows(i)("ClosedateFrom").ToString.Trim.Length > 0 Then
                                drow("ClosedateFrom") = objds.Tables("DETAILS").Rows(i)("ClosedateFrom") ' objeAAMS.GetDateFormat(objds.Tables("DETAILS").Rows(i)("ClosedateFrom"), "dd-MMM-yy HH:MM", "dd-MMM-yy hh:mm tt", "-")
                            End If
                            If objds.Tables("DETAILS").Rows(i)("ClosedateTo").ToString.Trim.Length > 0 Then
                                drow("ClosedateTo") = objds.Tables("DETAILS").Rows(i)("ClosedateTo") ' objeAAMS.GetDateFormat(objds.Tables("DETAILS").Rows(i)("ClosedateTo"), "dd-MMM-yy HH:MM", "dd-MMM-yy hh:mm tt", "-")
                            End If

                            drow("PRTNo") = objds.Tables("DETAILS").Rows(i)("PRTNo")
                            drow("LTRNo") = objds.Tables("DETAILS").Rows(i)("LTRNo")
                            drow("CallAssignedTo") = objds.Tables("DETAILS").Rows(i)("CallAssignedTo")
                            drow("Co_ordinator1") = objds.Tables("DETAILS").Rows(i)("Co_ordinator1")
                            drow("Workorderno") = objds.Tables("DETAILS").Rows(i)("Workorderno")
                            drow("contacttype") = objds.Tables("DETAILS").Rows(i)("contacttype")
                            drow("Discription") = objds.Tables("DETAILS").Rows(i)("Discription")
                            drow("Solution") = objds.Tables("DETAILS").Rows(i)("Solution")
                            dtTectReport.Rows.Add(drow)
                            dtTectReport.AcceptChanges()
                        Next
                        Dim dset As New DataSet
                        dset.Tables.Add(dtTectReport)

                        ' dset.WriteXmlSchema("C:\TechReport.xsd")
                        ' objRepDocument.Load(Server.MapPath("HelpDesk/Reports/Copy (2) of RptTechnicalReport.rpt"))

                        objRepDocument.Load(Server.MapPath("HelpDesk/Reports/RptTechnicalReport.rpt"))
                        objRepDocument.SetDataSource(dset)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = True
                        CRViewer1.HasPageNavigationButtons = True

                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 03 Feb  2009
                        BindReport(objRepDocument)
                    End If
                    'Modified on 03 Feb  2009

                Case Is = "BRTechReport"
                    If Session("BRTechReport") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objxmlout.LoadXml(Session("BRTechReport"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)


                        Dim dtTectReport As New DataTable("DETAILS")
                        Dim drow As DataRow

                        '' Region ='' 
                        ''     State ='' 
                        ''     City ='' 
                        ''     A1Office ='' 
                        ''     Agency1AOffice =''
                        ''     AgencyName =''
                        ''      OfficeId =''
                        ''      LoggedBy =''
                        ''      Callername =''
                        ''      Customercategory =''
                        ''      QueryGroup =''
                        ''      QuerySubGroup =''
                        ''      QueryCategory =''
                        ''      QuerySubCategory =''
                        ''      QueryStatus =''
                        ''      QueryPriority =''
                        ''      DateassignedTime =''
                        ''      DateopenFrom =''
                        ''      DateopenTo =''
                        ''      ClosedateFrom =''
                        ''      ClosedateTo =''
                        ''      PRTNo =''
                        ''      LTRNo =''
                        ''      CallAssignedTo =''
                        ''      Co_ordinator1 =''
                        ''      Workorderno =''
                        ''      contacttype =''
                        ''<Discription Value=''/>
                        ''<Discription Value=''/>
                        ''<Solution Value=''/>
                        ''<Solution Value=''

                        dtTectReport.Columns.Add("Region", GetType(String))
                        dtTectReport.Columns.Add("State", GetType(String))
                        dtTectReport.Columns.Add("City", GetType(String))
                        dtTectReport.Columns.Add("A1Office", GetType(String))
                        dtTectReport.Columns.Add("Agency1AOffice", GetType(String))
                        dtTectReport.Columns.Add("AgencyName", GetType(String))
                        dtTectReport.Columns.Add("OfficeId", GetType(String))
                        dtTectReport.Columns.Add("LoggedBy", GetType(String))
                        dtTectReport.Columns.Add("Callername", GetType(String))
                        dtTectReport.Columns.Add("Customercategory", GetType(String))
                        dtTectReport.Columns.Add("QueryGroup", GetType(String))
                        dtTectReport.Columns.Add("QuerySubGroup", GetType(String))
                        dtTectReport.Columns.Add("QueryCategory", GetType(String))
                        dtTectReport.Columns.Add("QuerySubCategory", GetType(String))
                        dtTectReport.Columns.Add("QueryStatus", GetType(String))
                        dtTectReport.Columns.Add("QueryPriority", GetType(String))
                        dtTectReport.Columns.Add("DateassignedTime", GetType(DateTime)).AllowDBNull = True
                        dtTectReport.Columns.Add("DateopenFrom", GetType(DateTime)).AllowDBNull = True
                        dtTectReport.Columns.Add("DateopenTo", GetType(DateTime)).AllowDBNull = True
                        dtTectReport.Columns.Add("ClosedateFrom", GetType(DateTime)).AllowDBNull = True
                        dtTectReport.Columns.Add("ClosedateTo", GetType(DateTime)).AllowDBNull = True
                        dtTectReport.Columns.Add("PRTNo", GetType(String))
                        dtTectReport.Columns.Add("LTRNo", GetType(String))
                        dtTectReport.Columns.Add("CallAssignedTo", GetType(String))
                        dtTectReport.Columns.Add("Co_ordinator1", GetType(String))
                        dtTectReport.Columns.Add("Workorderno", GetType(String))
                        dtTectReport.Columns.Add("contacttype", GetType(String))
                        dtTectReport.Columns.Add("Discription", GetType(String))
                        dtTectReport.Columns.Add("Solution", GetType(String))

                        For i As Integer = 0 To objds.Tables("DETAILS").Rows.Count - 1
                            drow = dtTectReport.NewRow

                            drow("Region") = objds.Tables("DETAILS").Rows(i)("Region")
                            drow("State") = objds.Tables("DETAILS").Rows(i)("State")
                            drow("City") = objds.Tables("DETAILS").Rows(i)("City")
                            drow("A1Office") = objds.Tables("DETAILS").Rows(i)("A1Office")
                            drow("Agency1AOffice") = objds.Tables("DETAILS").Rows(i)("Agency1AOffice")
                            drow("AgencyName") = objds.Tables("DETAILS").Rows(i)("AgencyName")
                            drow("OfficeId") = objds.Tables("DETAILS").Rows(i)("OfficeId")
                            drow("LoggedBy") = objds.Tables("DETAILS").Rows(i)("LoggedBy")
                            drow("Callername") = objds.Tables("DETAILS").Rows(i)("Callername")
                            drow("Customercategory") = objds.Tables("DETAILS").Rows(i)("Customercategory")
                            drow("QueryGroup") = objds.Tables("DETAILS").Rows(i)("QueryGroup")
                            drow("QuerySubGroup") = objds.Tables("DETAILS").Rows(i)("QuerySubGroup")
                            drow("QueryCategory") = objds.Tables("DETAILS").Rows(i)("QueryCategory")
                            drow("QuerySubCategory") = objds.Tables("DETAILS").Rows(i)("QuerySubCategory")
                            drow("QueryStatus") = objds.Tables("DETAILS").Rows(i)("QueryStatus")
                            drow("QueryPriority") = objds.Tables("DETAILS").Rows(i)("QueryPriority")
                            If objds.Tables("DETAILS").Rows(i)("DateassignedTime").ToString.Trim.Length > 0 Then
                                drow("DateassignedTime") = objds.Tables("DETAILS").Rows(i)("DateassignedTime") ' objeAAMS.GetDateFormat(objds.Tables("DETAILS").Rows(i)("DateassignedTime"), "dd-MMM-yy HH:MM", "dd-MMM-yy hh:mm tt", "-")
                            End If
                            If objds.Tables("DETAILS").Rows(i)("DateopenFrom").ToString.Trim.Length > 0 Then
                                drow("DateopenFrom") = objds.Tables("DETAILS").Rows(i)("DateopenFrom") ' objeAAMS.GetDateFormat(objds.Tables("DETAILS").Rows(i)("DateopenFrom"), "dd-MMM-yy HH:MM", "dd-MMM-yy hh:mm tt", "-")
                            End If
                            If objds.Tables("DETAILS").Rows(i)("DateopenTo").ToString.Trim.Length > 0 Then
                                drow("DateopenTo") = objds.Tables("DETAILS").Rows(i)("DateopenTo") ' objeAAMS.GetDateFormat(objds.Tables("DETAILS").Rows(i)("DateopenTo"), "dd-MMM-yy HH:MM", "dd-MMM-yy hh:mm tt", "-")
                            End If
                            If objds.Tables("DETAILS").Rows(i)("ClosedateFrom").ToString.Trim.Length > 0 Then
                                drow("ClosedateFrom") = objds.Tables("DETAILS").Rows(i)("ClosedateFrom") ' objeAAMS.GetDateFormat(objds.Tables("DETAILS").Rows(i)("ClosedateFrom"), "dd-MMM-yy HH:MM", "dd-MMM-yy hh:mm tt", "-")
                            End If
                            If objds.Tables("DETAILS").Rows(i)("ClosedateTo").ToString.Trim.Length > 0 Then
                                drow("ClosedateTo") = objds.Tables("DETAILS").Rows(i)("ClosedateTo") ' objeAAMS.GetDateFormat(objds.Tables("DETAILS").Rows(i)("ClosedateTo"), "dd-MMM-yy HH:MM", "dd-MMM-yy hh:mm tt", "-")
                            End If

                            drow("PRTNo") = objds.Tables("DETAILS").Rows(i)("PRTNo")
                            drow("LTRNo") = objds.Tables("DETAILS").Rows(i)("LTRNo")
                            drow("CallAssignedTo") = objds.Tables("DETAILS").Rows(i)("CallAssignedTo")
                            drow("Co_ordinator1") = objds.Tables("DETAILS").Rows(i)("Co_ordinator1")
                            drow("Workorderno") = objds.Tables("DETAILS").Rows(i)("Workorderno")
                            drow("contacttype") = objds.Tables("DETAILS").Rows(i)("contacttype")
                            drow("Discription") = objds.Tables("DETAILS").Rows(i)("Discription")
                            drow("Solution") = objds.Tables("DETAILS").Rows(i)("Solution")
                            dtTectReport.Rows.Add(drow)
                            dtTectReport.AcceptChanges()
                        Next
                        Dim dset As New DataSet
                        dset.Tables.Add(dtTectReport)

                        ' dset.WriteXmlSchema("C:\TechReport.xsd")

                        ' objRepDocument.Load(Server.MapPath("HelpDesk/Reports/Copy (2) of RptTechnicalReport.rpt"))
                        objRepDocument.Load(Server.MapPath("BirdresHelpDesk/Reports/RptTechnicalReport.rpt"))
                        objRepDocument.SetDataSource(dset)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = True
                        CRViewer1.HasPageNavigationButtons = True
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 03 Feb  2009
                        BindReport(objRepDocument)
                    End If


                Case Is = "AirlineWiseGraph"

                    If Session("AirlineWiseGraph") Is Nothing Then
                        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
                        Dim objInputXml As New XmlDocument
                        Dim objOutputXml As New XmlDocument


                        'objInputXml.LoadXml("<PR_SEARCH_AIRLINEWISEMARKETSHAREDETAILS_INPUT> <AIRLINE_CODE></AIRLINE_CODE>  <SMonth></SMonth> <SYear></SYear>  <EMonth></EMonth>  <EYear></EYear> <Limited_To_Aoffice></Limited_To_Aoffice>  <Limited_To_OwnAagency></Limited_To_OwnAagency>  <Limited_To_Region></Limited_To_Region>  <PAGE_NO>1</PAGE_NO>  <PAGE_SIZE>25</PAGE_SIZE>  <SORT_BY>AgencyName</SORT_BY>  <DESC>FALSE</DESC></PR_SEARCH_AIRLINEWISEMARKETSHAREDETAILS_INPUT>")

                        objInputXml.LoadXml("<PR_SEARCH_AIRLINEWISEMARKETSHAREDETAILS_INPUT><GroupTypeID></GroupTypeID> <AIRLINE_CODE></AIRLINE_CODE> <CITY></CITY><COUNTRY></COUNTRY><Aoffice></Aoffice><Region></Region><ONLINE_CARRIER></ONLINE_CARRIER><BreakupType></BreakupType> <SMonth></SMonth> <SYear></SYear>  <EMonth></EMonth>  <EYear></EYear> <Limited_To_Aoffice></Limited_To_Aoffice>  <Limited_To_OwnAagency></Limited_To_OwnAagency>  <Limited_To_Region></Limited_To_Region>  <PAGE_NO>1</PAGE_NO>  <PAGE_SIZE>25</PAGE_SIZE>  <SORT_BY>AgencyName</SORT_BY>  <DESC>FALSE</DESC></PR_SEARCH_AIRLINEWISEMARKETSHAREDETAILS_INPUT>")

                        '<PR_SEARCH_AIRLINEWISEMARKETSHAREDETAILS_INPUT>
                        '  <AIRLINE_CODE></AIRLINE_CODE>
                        '  <SMonth></SMonth>
                        '  <SYear></SYear>
                        '  <EMonth></EMonth>
                        '  <EYear></EYear>
                        '  <Limited_To_Aoffice></Limited_To_Aoffice>
                        '  <Limited_To_OwnAagency></Limited_To_OwnAagency>
                        '  <Limited_To_Region></Limited_To_Region>
                        '  <PAGE_NO>1</PAGE_NO>
                        '  <PAGE_SIZE>25</PAGE_SIZE>
                        '  <SORT_BY>AgencyName</SORT_BY>
                        '  <DESC>FALSE</DESC>
                        '</PR_SEARCH_AIRLINEWISEMARKETSHAREDETAILS_INPUT>
                        '")

                        'parameter="Fmonth=" + FMonth  + "&TMonth=" + TMonth + "&FYear=" + FYear +  "&TYear=" + TYear +  "&Aoff=" + Aoff + "&LimAoff=" +  LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff + "&AirCode=" + AirCode  ;

                        If (Request.QueryString("AirCode") IsNot Nothing) Then

                            objInputXml.DocumentElement.SelectSingleNode("AIRLINE_CODE").InnerText = Request.QueryString("AirCode").ToString
                        End If
                        If (Request.QueryString("Fmonth") IsNot Nothing) Then
                            objInputXml.DocumentElement.SelectSingleNode("SMonth").InnerText = Request.QueryString("Fmonth").ToString

                        End If
                        If (Request.QueryString("TMonth") IsNot Nothing) Then
                            objInputXml.DocumentElement.SelectSingleNode("EMonth").InnerText = Request.QueryString("TMonth").ToString
                        End If
                        If (Request.QueryString("FYear") IsNot Nothing) Then
                            objInputXml.DocumentElement.SelectSingleNode("SYear").InnerText = Request.QueryString("FYear").ToString
                        End If
                        If (Request.QueryString("TYear") IsNot Nothing) Then
                            objInputXml.DocumentElement.SelectSingleNode("EYear").InnerText = Request.QueryString("TYear").ToString
                        End If

                        If (Request.QueryString("Aoff") IsNot Nothing) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = Request.QueryString("Aoff").ToString
                        End If

                        If (Request.QueryString("LimOwnOff") IsNot Nothing) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = Request.QueryString("LimOwnOff").ToString
                        End If

                        If (Request.QueryString("LimReg") IsNot Nothing) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = Request.QueryString("LimReg").ToString
                        End If


                        '@ Added on 11/02/10
                        If (Request.QueryString("City") IsNot Nothing) Then

                            objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = Request.QueryString("City").ToString
                        End If

                        If (Request.QueryString("Country") IsNot Nothing) Then

                            objInputXml.DocumentElement.SelectSingleNode("COUNTRY").InnerText = Request.QueryString("Country").ToString
                        End If

                        If (Request.QueryString("Region") IsNot Nothing) Then

                            objInputXml.DocumentElement.SelectSingleNode("Region").InnerText = Request.QueryString("Region").ToString
                        End If

                        If (Request.QueryString("OnCarr") IsNot Nothing) Then

                            objInputXml.DocumentElement.SelectSingleNode("ONLINE_CARRIER").InnerText = Request.QueryString("OnCarr").ToString
                        End If

                        If (Request.QueryString("Aoff") IsNot Nothing) Then

                            objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = Request.QueryString("Aoff").ToString
                        End If

                        If (Request.QueryString("GType") IsNot Nothing) Then
                            objInputXml.DocumentElement.SelectSingleNode("GroupTypeID").InnerText = Request.QueryString("GType").ToString
                        End If

                        If (Request.QueryString("BreakupType") IsNot Nothing) Then
                            objInputXml.DocumentElement.SelectSingleNode("BreakupType").InnerText = Request.QueryString("BreakupType").ToString
                        End If



                        '@ Added on 11/02/10



                        'End Code for paging and sorting

                        'Here Back end Method Call

                        objOutputXml = objbzMIDT.AirLineWiseMarketShareGraph(objInputXml)
                        '  
                        'objOutputXml.LoadXml("<PR_SEARCH_AIRLINEWISEMARKETSHAREGRAPH_OUTPUT> <AIRLINEWISEMARKETSHAREGRAPH  AIRLINE_CODE='' AIRLINE_NAME='4A' A='5' B='6' G='4' P='5' W='5' TOTAL='25'/>  <Errors Status='FALSE'>    <Error Code='' Description='' />  </Errors></PR_SEARCH_AIRLINEWISEMARKETSHAREGRAPH_OUTPUT>")
                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                                Session("AirlineWiseGraph") = objOutputXml.OuterXml
                            End If
                        Else
                            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

                        End If
                    End If

                    If Session("AirlineWiseGraph") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then

                        objRepDocument.Load(Server.MapPath("Market/Reports/RptAirlineMarketShareGraph.rpt"))

                        objxmlout.LoadXml(Session("AirlineWiseGraph"))

                        Session("AirlineWiseGraph") = Nothing

                        xreader = New XmlNodeReader(objxmlout)

                        objds.ReadXml(xreader)

                        Dim dtAilineWiseGraphGraph As New DataTable("AilineWiseGraph")

                        Dim drow As DataRow


                        dtAilineWiseGraphGraph.Columns.Add("AIRLINE_CODE", GetType(String))

                        dtAilineWiseGraphGraph.Columns.Add("AIRLINE_NAME", GetType(String))

                        dtAilineWiseGraphGraph.Columns.Add("PARAM", GetType(String))

                        dtAilineWiseGraphGraph.Columns.Add("1AVal", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("1BVal", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("1GVal", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("1PVal", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("1WVal", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("TotalVal", GetType(Double))

                        dtAilineWiseGraphGraph.Columns.Add("1A", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("1B", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("1G", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("1P", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("1W", GetType(Double))


                        drow = dtAilineWiseGraphGraph.NewRow

                        drow("AIRLINE_CODE") = objds.Tables("AIRLINEWISEMARKETSHAREGRAPH").Rows(0)("AIRLINE_CODE")
                        drow("AIRLINE_NAME") = objds.Tables("AIRLINEWISEMARKETSHAREGRAPH").Rows(0)("AIRLINE_NAME")
                        If (Request.QueryString("Param") IsNot Nothing) Then
                            drow("PARAM") = "Airline wise Market Share (" + objds.Tables("AIRLINEWISEMARKETSHAREGRAPH").Rows(0)("AIRLINE_NAME").ToString.Trim + ")"
                        End If



                        drow("1AVal") = objds.Tables("AIRLINEWISEMARKETSHAREGRAPH").Rows(0)("A")
                        drow("1BVal") = objds.Tables("AIRLINEWISEMARKETSHAREGRAPH").Rows(0)("B")
                        drow("1GVal") = objds.Tables("AIRLINEWISEMARKETSHAREGRAPH").Rows(0)("G")
                        drow("1PVal") = objds.Tables("AIRLINEWISEMARKETSHAREGRAPH").Rows(0)("P")
                        drow("1WVal") = objds.Tables("AIRLINEWISEMARKETSHAREGRAPH").Rows(0)("W")
                        drow("TotalVal") = objds.Tables("AIRLINEWISEMARKETSHAREGRAPH").Rows(0)("TOTAL")



                        drow("1A") = (Val(objds.Tables("AIRLINEWISEMARKETSHAREGRAPH").Rows(0)("A")) * 100) / Val(objds.Tables("AIRLINEWISEMARKETSHAREGRAPH").Rows(0)("TOTAL"))
                        drow("1B") = (Val(objds.Tables("AIRLINEWISEMARKETSHAREGRAPH").Rows(0)("B")) * 100) / Val(objds.Tables("AIRLINEWISEMARKETSHAREGRAPH").Rows(0)("TOTAL"))
                        drow("1G") = (Val(objds.Tables("AIRLINEWISEMARKETSHAREGRAPH").Rows(0)("G")) * 100) / Val(objds.Tables("AIRLINEWISEMARKETSHAREGRAPH").Rows(0)("TOTAL"))
                        drow("1P") = (Val(objds.Tables("AIRLINEWISEMARKETSHAREGRAPH").Rows(0)("P")) * 100) / Val(objds.Tables("AIRLINEWISEMARKETSHAREGRAPH").Rows(0)("TOTAL"))
                        drow("1W") = (Val(objds.Tables("AIRLINEWISEMARKETSHAREGRAPH").Rows(0)("W")) * 100) / Val(objds.Tables("AIRLINEWISEMARKETSHAREGRAPH").Rows(0)("TOTAL"))


                        dtAilineWiseGraphGraph.Rows.Add(drow)
                        '@New Code

                        Dim dset As New DataSet
                        dset.Tables.Add(dtAilineWiseGraphGraph)

                        'dset.WriteXmlSchema("c:\rptAilineWiseGraph.xsd")
                        'Exit Sub
                        objRepDocument.SetDataSource(dset)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = True
                        BindReport(objRepDocument)
                        Session("AirlineWiseGraph") = Nothing
                    End If

                Case Is = "TotalMarketShareGraph"

                    If Session("TotalMarketShareGraph") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then

                        objRepDocument.Load(Server.MapPath("Market/Reports/RptAirlineMarketShareGraph.rpt"))

                        objxmlout.LoadXml(Session("TotalMarketShareGraph"))

                        xreader = New XmlNodeReader(objxmlout)

                        objds.ReadXml(xreader)

                        Dim dtAilineWiseGraphGraph As New DataTable("AilineWiseGraph")

                        Dim drow As DataRow


                        dtAilineWiseGraphGraph.Columns.Add("AIRLINE_CODE", GetType(String))

                        dtAilineWiseGraphGraph.Columns.Add("AIRLINE_NAME", GetType(String))

                        dtAilineWiseGraphGraph.Columns.Add("PARAM", GetType(String))

                        dtAilineWiseGraphGraph.Columns.Add("1AVal", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("1BVal", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("1GVal", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("1PVal", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("1WVal", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("TotalVal", GetType(Double))

                        dtAilineWiseGraphGraph.Columns.Add("1A", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("1B", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("1G", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("1P", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("1W", GetType(Double))


                        drow = dtAilineWiseGraphGraph.NewRow

                        drow("AIRLINE_CODE") = "" ' objds.Tables("AIRLINEWISEMARKETSHAREGRAPH").Rows(0)("AIRLINE_CODE")
                        drow("AIRLINE_NAME") = "" 'objds.Tables("AIRLINEWISEMARKETSHAREGRAPH").Rows(0)("AIRLINE_NAME")

                        Dim strtype As String = ""
                        If (Request.QueryString("SelectedBy") IsNot Nothing) Then
                            If Request.QueryString("SelectedBy").Trim.ToString = "1" Then
                                strtype = "City"
                            ElseIf Request.QueryString("SelectedBy").Trim.ToString = "2" Then
                                strtype = "Country"
                            ElseIf Request.QueryString("SelectedBy").Trim.ToString = "4" Then
                                strtype = "Region"
                            ElseIf Request.QueryString("SelectedBy").Trim.ToString = "3" Then
                                strtype = "Aoffice"
                            ElseIf Request.QueryString("SelectedBy").Trim.ToString = "6" Then
                                strtype = "Agency"
                            End If
                        End If


                        If (Request.QueryString("Param") IsNot Nothing) Then
                            If (Request.QueryString("Acode") IsNot Nothing) Then
                                drow("PARAM") = "Market Share (" + Request.QueryString("Acode").Trim.ToString + ")" '+ strtype + " Wise"
                            End If
                        End If



                        drow("1AVal") = objds.Tables("PAGE_TOTAL").Rows(0)("A")
                        drow("1BVal") = objds.Tables("PAGE_TOTAL").Rows(0)("B")
                        drow("1GVal") = objds.Tables("PAGE_TOTAL").Rows(0)("G")
                        drow("1PVal") = objds.Tables("PAGE_TOTAL").Rows(0)("P")
                        drow("1WVal") = objds.Tables("PAGE_TOTAL").Rows(0)("W")
                        drow("TotalVal") = objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL")



                        drow("1A") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("A")) * 100) / Val(objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL"))
                        drow("1B") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("B")) * 100) / Val(objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL"))
                        drow("1G") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("G")) * 100) / Val(objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL"))
                        drow("1P") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("P")) * 100) / Val(objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL"))
                        drow("1W") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("W")) * 100) / Val(objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL"))


                        dtAilineWiseGraphGraph.Rows.Add(drow)
                        '@New Code

                        Dim dset As New DataSet
                        dset.Tables.Add(dtAilineWiseGraphGraph)

                        'dset.WriteXmlSchema("c:\rptAilineWiseGraph.xsd")
                        'Exit Sub
                        objRepDocument.SetDataSource(dset)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = True
                        BindReport(objRepDocument)

                    End If

                Case Is = "MarketShareGraphRowWise"


                    If Session("MarketShareGraphRowWise") Is Nothing Then
                        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
                        Dim objInputXml As New XmlDocument
                        Dim objOutputXml As New XmlDocument

                        Dim objXmlReader As XmlNodeReader
                        Dim ds As New DataSet
                        Dim objbzDailyBooking As New AAMS.bizProductivity.bzTravelAssistance
                        '<PR_SEARCHAIRMARKETSHARE_INPUT><MONTHFROM>01</MONTHFROM><YEARFROM>
                        '</YEARFROM><MONTHTO>02</MONTHTO><YEARTO></YEARTO>
                        '<AIRLINECODE>IC</AIRLINECODE><CITY></CITY><COUNTRY>INDIA</COUNTRY><AOFFICE></AOFFICE><REGION></REGION>
                        '<SELECTBY>1</SELECTBY><SHOWBREAKUP>1</SHOWBREAKUP><RESPONSIBLESTAFFID />
                        '<LIMITED_TO_REGION></LIMITED_TO_REGION></PR_SEARCHAIRMARKETSHARE_INPUT>
                        objInputXml.LoadXml("<PR_SEARCHAIRMARKETSHARE_INPUT><GroupTypeID></GroupTypeID><MONTHFROM></MONTHFROM><YEARFROM></YEARFROM><MONTHTO></MONTHTO><YEARTO></YEARTO><AIRLINECODE></AIRLINECODE><CITY></CITY><COUNTRY></COUNTRY><AOFFICE></AOFFICE><REGION></REGION><SELECTBY></SELECTBY><LCODE></LCODE><SHOWBREAKUP>0</SHOWBREAKUP><RESPONSIBLESTAFFID /><LIMITED_TO_REGION></LIMITED_TO_REGION>  <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/><COMP_VERTICAL></COMP_VERTICAL></PR_SEARCHAIRMARKETSHARE_INPUT>")

                        If (Request.QueryString("Com_Ver") IsNot Nothing) Then

                            objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = Request.QueryString("Com_Ver").ToString
                        End If

                        If (Request.QueryString("AirCode") IsNot Nothing) Then

                            objInputXml.DocumentElement.SelectSingleNode("AIRLINECODE").InnerText = Request.QueryString("AirCode").ToString
                        End If
                        If (Request.QueryString("Fmonth") IsNot Nothing) Then
                            objInputXml.DocumentElement.SelectSingleNode("MONTHFROM").InnerText = Request.QueryString("Fmonth").ToString

                        End If
                        If (Request.QueryString("TMonth") IsNot Nothing) Then
                            objInputXml.DocumentElement.SelectSingleNode("MONTHTO").InnerText = Request.QueryString("TMonth").ToString
                        End If
                        If (Request.QueryString("FYear") IsNot Nothing) Then
                            objInputXml.DocumentElement.SelectSingleNode("YEARFROM").InnerText = Request.QueryString("FYear").ToString
                        End If
                        If (Request.QueryString("TYear") IsNot Nothing) Then
                            objInputXml.DocumentElement.SelectSingleNode("YEARTO").InnerText = Request.QueryString("TYear").ToString
                        End If

                        'If (Request.QueryString("Aoff") IsNot Nothing) Then
                        '    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = Request.QueryString("Aoff").ToString
                        'End If
                        'If (Request.QueryString("LimOwnOff") IsNot Nothing) Then
                        '    objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = Request.QueryString("LimOwnOff").ToString
                        'End If

                        If (Request.QueryString("LimReg") IsNot Nothing) Then
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = Request.QueryString("LimReg").ToString
                        End If


                        '@ Added on 11/02/10
                        If (Request.QueryString("City") IsNot Nothing) Then

                            objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = Request.QueryString("City").ToString
                        End If

                        If (Request.QueryString("Country") IsNot Nothing) Then

                            objInputXml.DocumentElement.SelectSingleNode("COUNTRY").InnerText = Request.QueryString("Country").ToString
                        End If

                        If (Request.QueryString("Region") IsNot Nothing) Then

                            objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = Request.QueryString("Region").ToString
                        End If

                        If (Request.QueryString("OnCarr") IsNot Nothing) Then

                            'objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = Request.QueryString("OnCarr").ToString
                        End If

                        If (Request.QueryString("Aoff") IsNot Nothing) Then

                            objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = Request.QueryString("Aoff").ToString
                        End If

                        If (Request.QueryString("GType") IsNot Nothing) Then

                            objInputXml.DocumentElement.SelectSingleNode("GroupTypeID").InnerText = Request.QueryString("GType").ToString
                        End If
                        '@ Added on 11/02/10







                        'End Code for paging and sorting


                        Dim strtype As String = ""
                        If (Request.QueryString("SelectedBy") IsNot Nothing) Then
                            If (Request.QueryString("SelectedByValue") IsNot Nothing) Then
                                If Request.QueryString("SelectedBy").Trim.ToString = "1" Then
                                    strtype = "City"
                                    objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = Request.QueryString("SelectedByValue").Trim.ToString

                                ElseIf Request.QueryString("SelectedBy").Trim.ToString = "2" Then
                                    strtype = "Country"
                                    objInputXml.DocumentElement.SelectSingleNode("COUNTRY").InnerText = Request.QueryString("SelectedByValue").Trim.ToString
                                ElseIf Request.QueryString("SelectedBy").Trim.ToString = "4" Then
                                    strtype = "Region"
                                    objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = Request.QueryString("SelectedByValue").Trim.ToString
                                ElseIf Request.QueryString("SelectedBy").Trim.ToString = "3" Then
                                    strtype = "Office"
                                    objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = Request.QueryString("SelectedByValue").Trim.ToString
                                ElseIf Request.QueryString("SelectedBy").Trim.ToString = "6" Then
                                    objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = Request.QueryString("SelectedByValue").Trim.ToString
                                    strtype = "Agency"
                                End If
                            End If
                        End If

                        objInputXml.DocumentElement.SelectSingleNode("SELECTBY").InnerText = Request.QueryString("SelectedBy").Trim.ToString
                        If Session("Security") IsNot Nothing Then
                            Dim xDoc As New XmlDocument
                            xDoc.LoadXml(Session("Security"))

                            Dim str As String()
                            str = Session("LoginSession").ToString().Split("|")
                            objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = str(0)
                        End If

                        objOutputXml = objbzDailyBooking.SearchMarketShareAirline(objInputXml)

                        'parameter="Fmonth=" + FMonth  + "&TMonth=" + TMonth + "&FYear=" + FYear +  "&TYear=" + TYear +  "&Aoff=" + Aoff + "&LimAoff=" +  LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff + "&AirCode=" + AirCode  ;

                        'Here Back end Method Call


                        '  
                        'objOutputXml.LoadXml("<PR_SEARCH_AIRLINEWISEMARKETSHAREGRAPH_OUTPUT> <AIRLINEWISEMARKETSHAREGRAPH  AIRLINE_CODE='' AIRLINE_NAME='4A' A='5' B='6' G='4' P='5' W='5' TOTAL='25'/>  <Errors Status='FALSE'>    <Error Code='' Description='' />  </Errors></PR_SEARCH_AIRLINEWISEMARKETSHAREGRAPH_OUTPUT>")
                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                                Session("MarketShareGraphRowWise") = objOutputXml.OuterXml
                            End If
                        Else
                            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

                        End If
                    End If

                    If Session("MarketShareGraphRowWise") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then

                        objRepDocument.Load(Server.MapPath("Market/Reports/RptAirlineMarketShareGraph.rpt"))

                        objxmlout.LoadXml(Session("MarketShareGraphRowWise"))

                        Session("MarketShareGraphRowWise") = Nothing

                        xreader = New XmlNodeReader(objxmlout)

                        objds.ReadXml(xreader)

                        Dim dtAilineWiseGraphGraph As New DataTable("AilineWiseGraph")

                        Dim drow As DataRow


                        dtAilineWiseGraphGraph.Columns.Add("AIRLINE_CODE", GetType(String))

                        dtAilineWiseGraphGraph.Columns.Add("AIRLINE_NAME", GetType(String))

                        dtAilineWiseGraphGraph.Columns.Add("PARAM", GetType(String))

                        dtAilineWiseGraphGraph.Columns.Add("1AVal", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("1BVal", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("1GVal", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("1PVal", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("1WVal", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("TotalVal", GetType(Double))

                        dtAilineWiseGraphGraph.Columns.Add("1A", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("1B", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("1G", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("1P", GetType(Double))
                        dtAilineWiseGraphGraph.Columns.Add("1W", GetType(Double))


                        drow = dtAilineWiseGraphGraph.NewRow

                        drow("AIRLINE_CODE") = "" ' objds.Tables("AIRLINEWISEMARKETSHAREGRAPH").Rows(0)("AIRLINE_CODE")
                        drow("AIRLINE_NAME") = "" 'objds.Tables("AIRLINEWISEMARKETSHAREGRAPH").Rows(0)("AIRLINE_NAME")

                        Dim strtype As String = ""
                        Dim strtypeValue As String = ""
                        If (Request.QueryString("SelectedBy") IsNot Nothing) Then
                            If (Request.QueryString("SelectedByValue") IsNot Nothing) Then
                                If Request.QueryString("SelectedBy").Trim.ToString = "1" Then
                                    strtype = "City"
                                    strtypeValue = Request.QueryString("SelectedByValue").Trim.ToString

                                ElseIf Request.QueryString("SelectedBy").Trim.ToString = "2" Then
                                    strtype = "Country"
                                    strtypeValue = Request.QueryString("SelectedByValue").Trim.ToString
                                ElseIf Request.QueryString("SelectedBy").Trim.ToString = "4" Then
                                    strtype = "Region"
                                    strtypeValue = Request.QueryString("SelectedByValue").Trim.ToString
                                ElseIf Request.QueryString("SelectedBy").Trim.ToString = "3" Then
                                    strtype = "Aoffice"
                                    strtypeValue = Request.QueryString("SelectedByValue").Trim.ToString
                                ElseIf Request.QueryString("SelectedBy").Trim.ToString = "6" Then
                                    strtypeValue = objds.Tables("DETAIL").Rows(0)("NAME") 'Request.QueryString("SelectedByValue").Trim.ToString
                                    strtype = "Agency"
                                End If
                            End If
                        End If


                        If (Request.QueryString("Param") IsNot Nothing) Then
                            If (Request.QueryString("AirLineName") IsNot Nothing) Then
                                drow("PARAM") = "Market Share for the " + strtype + " " + strtypeValue + vbCrLf + "(" + Request.QueryString("AirLineName").Trim.ToString + ")"
                            End If
                        End If



                        drow("1AVal") = objds.Tables("PAGE_TOTAL").Rows(0)("A")
                        drow("1BVal") = objds.Tables("PAGE_TOTAL").Rows(0)("B")
                        drow("1GVal") = objds.Tables("PAGE_TOTAL").Rows(0)("G")
                        drow("1PVal") = objds.Tables("PAGE_TOTAL").Rows(0)("P")
                        drow("1WVal") = objds.Tables("PAGE_TOTAL").Rows(0)("W")
                        drow("TotalVal") = objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL")



                        drow("1A") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("A")) * 100) / Val(objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL"))
                        drow("1B") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("B")) * 100) / Val(objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL"))
                        drow("1G") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("G")) * 100) / Val(objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL"))
                        drow("1P") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("P")) * 100) / Val(objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL"))
                        drow("1W") = (Val(objds.Tables("PAGE_TOTAL").Rows(0)("W")) * 100) / Val(objds.Tables("PAGE_TOTAL").Rows(0)("TOTAL"))


                        dtAilineWiseGraphGraph.Rows.Add(drow)
                        '@New Code

                        Dim dset As New DataSet
                        dset.Tables.Add(dtAilineWiseGraphGraph)

                        'dset.WriteXmlSchema("c:\rptAilineWiseGraph.xsd")
                        'Exit Sub
                        objRepDocument.SetDataSource(dset)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = True
                        BindReport(objRepDocument)

                    End If
                Case Is = "TrainingOrderReport"

                    If Session("TrainingOrderReport") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        Dim strFromToDate As String = ""
                        Dim paramfields As New ParameterFields

                        Dim prmFromToDate As New ParameterField()
                        Dim discreteVal_FromToDate As New ParameterDiscreteValue()

                        If Request.QueryString("FDate").ToString <> Nothing Then
                            strFromToDate = "Period - " & Request.QueryString("FDate").ToString
                        End If

                        If strFromToDate.Trim() = "" Then
                            If Request.QueryString("TDate").ToString <> Nothing Then
                                strFromToDate = strFromToDate + " Period - " & Request.QueryString("TDate").ToString
                            End If
                        Else
                            If Request.QueryString("TDate").ToString <> Nothing Then
                                strFromToDate = strFromToDate + " - " & Request.QueryString("TDate").ToString
                            End If
                        End If


                        ''work for amt in Words
                        prmFromToDate.ParameterFieldName = "PFromToDate"
                        discreteVal_FromToDate.Value = strFromToDate

                        prmFromToDate.CurrentValues.Add(discreteVal_FromToDate)
                        paramfields.Add(prmFromToDate)
                        ''end here

                        CRViewer1.ParameterFieldInfo = paramfields


                        objRepDocument.Load(Server.MapPath("Training/Reports/rptTrainingOrder.rpt"))
                        objxmlout.LoadXml(Session("TrainingOrderReport"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False

                        BindReport(objRepDocument)
                    End If


                    ''ASHISH 
                Case Is = "PaymentSheet"
                    Dim objbzBCPaymentProcess As New AAMS.bizIncetive.bzIncentive
                    Dim strChain_code As String = ""
                    Dim objED As New EncyrptDeCyrpt
                    Dim objInputXml As New XmlDocument
                    Dim objOutPutxml As New XmlDocument
                    Dim strWord As String = ""
                    'objOutPutxml.Load("C:\Admin\PaymentSheet.xml")

                    objInputXml.LoadXml("<UP_SER_INC_PAYMENTPROCESSDETAILS_INPUT><BC_ID></BC_ID><MONTH></MONTH><YEAR></YEAR><CHAINCODE></CHAINCODE><NO_OF_PAYMENT></NO_OF_PAYMENT><PAYMENT_ID></PAYMENT_ID><IS_PLB></IS_PLB><PLBPAYMENTPERIOD_FROM></PLBPAYMENTPERIOD_FROM><PLBPAYMENTPERIOD_TO></PLBPAYMENTPERIOD_TO></UP_SER_INC_PAYMENTPROCESSDETAILS_INPUT>")

                    objInputXml.DocumentElement.SelectSingleNode("BC_ID").InnerText = Request.QueryString("BCaseID")
                    objInputXml.DocumentElement.SelectSingleNode("MONTH").InnerText = Request.QueryString("Month")
                    objInputXml.DocumentElement.SelectSingleNode("YEAR").InnerText = Request.QueryString("Year")
                    strChain_code = objED.Decrypt(Request.QueryString("Chain_Code"))
                    objInputXml.DocumentElement.SelectSingleNode("CHAINCODE").InnerText = strChain_code

                    If Request.QueryString("CurPayNo") IsNot Nothing Then
                        objInputXml.DocumentElement.SelectSingleNode("NO_OF_PAYMENT").InnerText = Request.QueryString("CurPayNo")
                    End If
                    If Request.QueryString("PayId") IsNot Nothing Then
                        objInputXml.DocumentElement.SelectSingleNode("PAYMENT_ID").InnerText = Request.QueryString("PayId")
                    End If
                    If Request.QueryString("PLB") IsNot Nothing Then
                        objInputXml.DocumentElement.SelectSingleNode("IS_PLB").InnerText = Request.QueryString("PLB").ToUpper
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("IS_PLB").InnerText = "FALSE"
                    End If
                    If Request.QueryString("PeriodFrom") IsNot Nothing Then
                        objInputXml.DocumentElement.SelectSingleNode("PLBPAYMENTPERIOD_FROM").InnerText = Request.QueryString("PeriodFrom")
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("PLBPAYMENTPERIOD_FROM").InnerText = ""
                    End If
                    If Request.QueryString("PeriodTo") IsNot Nothing Then
                        objInputXml.DocumentElement.SelectSingleNode("PLBPAYMENTPERIOD_TO").InnerText = Request.QueryString("PeriodTo").ToUpper
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("PLBPAYMENTPERIOD_TO").InnerText = ""
                    End If

                    ''callback Method
                    objOutPutxml = objbzBCPaymentProcess.PaymentSummaryReport(objInputXml)

                    If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                        strWord = CurrencyToWord(objOutPutxml.DocumentElement.SelectSingleNode("FINALPAYMENT/FINALPAYMENTDETAILS").Attributes("FINALPAYMENT").InnerText)

                        ''************************************Define Parameter****************************************
                        Dim paramfields As New ParameterFields

                        Dim prmHeraderString As New ParameterField()
                        Dim discreteVal_HeraderString As New ParameterDiscreteValue()

                        Dim prmInWords As New ParameterField()
                        Dim discreteVal_prmInWords As New ParameterDiscreteValue()

                        Dim PrmBenefitDOM As New ParameterField()
                        Dim discreteVal_PrmBenefitDOM As New ParameterDiscreteValue()

                        ''work for motive parameter for month
                        Dim prmMotiveMonth1 As New ParameterField()
                        Dim discreteVal_prmMotiveMonth1 As New ParameterDiscreteValue()

                        ''work for motive parameter for year
                        Dim prmMotiveYear1 As New ParameterField()
                        Dim discreteVal_prmMotiveYear1 As New ParameterDiscreteValue()
                        '' end here

                        ' ''work for chain_code 
                        Dim prmChain_Code As New ParameterField()
                        Dim discreteVal_prmChain_Code As New ParameterDiscreteValue()
                        '' end here


                        ''work for BC_ID 
                        Dim prmBC_ID As New ParameterField()
                        Dim discreteVal_BC_ID As New ParameterDiscreteValue()
                        '' end here


                        ''work for PLAN 
                        Dim prmPlan As New ParameterField()
                        Dim discreteVal_prmPlan As New ParameterDiscreteValue()
                        ''end here

                        ''****************************************end parameter******************************************

                        Dim strArrDOM() As String
                        Dim intcount As Integer
                        Dim strFinal As String = String.Empty


                        ''work for chain code
                        prmChain_Code.ParameterFieldName = "PChainCode"
                        discreteVal_prmChain_Code.Value = "Group Code : " & strChain_code
                        prmChain_Code.CurrentValues.Add(discreteVal_prmChain_Code)
                        paramfields.Add(prmChain_Code)
                        ''end here

                        ''work for BC_ID
                        prmBC_ID.ParameterFieldName = "PBCID"
                        discreteVal_BC_ID.Value = "Business ID : " & objOutPutxml.DocumentElement.SelectSingleNode("GROUPDETAILS").Attributes("BC_ID").InnerText
                        prmBC_ID.CurrentValues.Add(discreteVal_BC_ID)
                        paramfields.Add(prmBC_ID)
                        ''end here


                        ''work for Plan
                        prmPlan.ParameterFieldName = "P_Plan"
                        discreteVal_prmPlan.Value = "Plan : " & objOutPutxml.DocumentElement.SelectSingleNode("GROUPDETAILS").Attributes("INC_TYPE_NAME").InnerText.ToString() & " " & objOutPutxml.DocumentElement.SelectSingleNode("GROUPDETAILS").Attributes("BILLINGVYCLE").InnerText.ToString & " Effective :  " & objOutPutxml.DocumentElement.SelectSingleNode("GROUPDETAILS").Attributes("BC_EFFEVTIVE_FROM").InnerText.ToUpper.ToString() & " to " & objOutPutxml.DocumentElement.SelectSingleNode("GROUPDETAILS").Attributes("BC_VALID_TILL").InnerText.ToUpper.ToString()
                        prmPlan.CurrentValues.Add(discreteVal_prmPlan)
                        paramfields.Add(prmPlan)
                        ''end here

                        ''work for amt in Words
                        prmInWords.ParameterFieldName = "PInWords"
                        If strWord = "" Then
                            discreteVal_prmInWords.Value = ""
                        Else
                            discreteVal_prmInWords.Value = strWord
                        End If
                        prmInWords.CurrentValues.Add(discreteVal_prmInWords)
                        paramfields.Add(prmInWords)
                        ''end here

                        ''work for motive parameter
                        prmMotiveMonth1.ParameterFieldName = "PMotiveMonth1"
                        discreteVal_prmMotiveMonth1.Value = Request.QueryString("Month")
                        prmMotiveMonth1.CurrentValues.Add(discreteVal_prmMotiveMonth1)
                        paramfields.Add(prmMotiveMonth1)
                        ''end here

                        ''work for motive parameter
                        prmMotiveYear1.ParameterFieldName = "PMotiveYear1"
                        discreteVal_prmMotiveYear1.Value = Request.QueryString("Year")
                        prmMotiveYear1.CurrentValues.Add(discreteVal_prmMotiveYear1)
                        paramfields.Add(prmMotiveYear1)
                        ''End here

                        ''work for header
                        prmHeraderString.ParameterFieldName = "PHeaderString"
                        discreteVal_HeraderString.Value = "PAYMENT DETAIL SHEET (MULTI RATES) "
                        discreteVal_HeraderString.Value = discreteVal_HeraderString.Value & objOutPutxml.DocumentElement.SelectSingleNode("GROUPDETAILS").Attributes("PAYMENTPERIOD1").InnerText.ToUpper.ToString()

                        'discreteVal_HeraderString.Value = discreteVal_HeraderString.Value & "Q" & ReturnQuarter(Request.QueryString("Month"), Request.QueryString("Year")) & " "
                        'discreteVal_HeraderString.Value = discreteVal_HeraderString.Value & Request.QueryString("Year") & " "
                        'discreteVal_HeraderString.Value = discreteVal_HeraderString.Value & UCase(objOutPutxml.DocumentElement.SelectSingleNode("GROUPDETAILS").Attributes("PAYMENTCYCLE").InnerText)

                        prmHeraderString.CurrentValues.Add(discreteVal_HeraderString)
                        paramfields.Add(prmHeraderString)
                        ''end here

                        ''work for DOM
                        PrmBenefitDOM.ParameterFieldName = "PBenefitDOM"
                        If objOutPutxml.DocumentElement.SelectSingleNode("MIDT/CPS").Attributes("BENEFIT_HEAD").InnerText.Trim() <> "" Then
                            discreteVal_PrmBenefitDOM.Value = objOutPutxml.DocumentElement.SelectSingleNode("MIDT/CPS").Attributes("BENEFIT_HEAD").InnerText

                            strArrDOM = discreteVal_PrmBenefitDOM.Value.Split(",")
                            For intcount = 0 To strArrDOM.Length - 1
                                If strArrDOM(intcount) <> "" Then
                                    strFinal = strArrDOM(intcount) & vbNewLine & strFinal
                                End If
                            Next
                            discreteVal_PrmBenefitDOM.Value = strFinal
                        End If
                        If discreteVal_PrmBenefitDOM.Value = "" Then
                            discreteVal_PrmBenefitDOM.Value = ""
                        Else
                            discreteVal_PrmBenefitDOM.Value = strFinal
                        End If
                        PrmBenefitDOM.CurrentValues.Add(discreteVal_PrmBenefitDOM)
                        paramfields.Add(PrmBenefitDOM)
                        ''end here



                        CRViewer1.ParameterFieldInfo = paramfields
                        objRepDocument.Load(Server.MapPath("Incentive/Reports/rptPaymentDetailSheet.rpt"))
                        xreader = New XmlNodeReader(objOutPutxml)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        BindReport(objRepDocument)
                    Else
                        lblError.Text = objOutPutxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                        Exit Select
                    End If

                    ''**********************working for ET Help Desk module**********************************

               

                Case Is = "BOTechReport"
                    If Session("BOTechReport") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objxmlout.LoadXml(Session("BOTechReport"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)


                        Dim dtTectReport As New DataTable("DETAILS")
                        Dim drow As DataRow

                        dtTectReport.Columns.Add("Region", GetType(String))
                        dtTectReport.Columns.Add("State", GetType(String))
                        dtTectReport.Columns.Add("City", GetType(String))
                        dtTectReport.Columns.Add("A1Office", GetType(String))
                        dtTectReport.Columns.Add("Agency1AOffice", GetType(String))
                        dtTectReport.Columns.Add("AgencyName", GetType(String))
                        dtTectReport.Columns.Add("OfficeId", GetType(String))
                        dtTectReport.Columns.Add("LoggedBy", GetType(String))
                        dtTectReport.Columns.Add("Callername", GetType(String))
                        dtTectReport.Columns.Add("Customercategory", GetType(String))
                        dtTectReport.Columns.Add("QueryGroup", GetType(String))
                        dtTectReport.Columns.Add("QuerySubGroup", GetType(String))
                        dtTectReport.Columns.Add("QueryCategory", GetType(String))
                        dtTectReport.Columns.Add("QuerySubCategory", GetType(String))
                        dtTectReport.Columns.Add("QueryStatus", GetType(String))
                        dtTectReport.Columns.Add("QueryPriority", GetType(String))
                        dtTectReport.Columns.Add("DateassignedTime", GetType(DateTime)).AllowDBNull = True
                        dtTectReport.Columns.Add("DateopenFrom", GetType(DateTime)).AllowDBNull = True
                        dtTectReport.Columns.Add("DateopenTo", GetType(DateTime)).AllowDBNull = True
                        dtTectReport.Columns.Add("ClosedateFrom", GetType(DateTime)).AllowDBNull = True
                        dtTectReport.Columns.Add("ClosedateTo", GetType(DateTime)).AllowDBNull = True
                        dtTectReport.Columns.Add("PRTNo", GetType(String))
                        dtTectReport.Columns.Add("LTRNo", GetType(String))
                        dtTectReport.Columns.Add("CallAssignedTo", GetType(String))
                        dtTectReport.Columns.Add("Co_ordinator1", GetType(String))
                        dtTectReport.Columns.Add("Workorderno", GetType(String))
                        dtTectReport.Columns.Add("contacttype", GetType(String))
                        dtTectReport.Columns.Add("Discription", GetType(String))
                        dtTectReport.Columns.Add("Solution", GetType(String))

                        For i As Integer = 0 To objds.Tables("DETAILS").Rows.Count - 1
                            drow = dtTectReport.NewRow

                            drow("Region") = objds.Tables("DETAILS").Rows(i)("Region")
                            drow("State") = objds.Tables("DETAILS").Rows(i)("State")
                            drow("City") = objds.Tables("DETAILS").Rows(i)("City")
                            drow("A1Office") = objds.Tables("DETAILS").Rows(i)("A1Office")
                            drow("Agency1AOffice") = objds.Tables("DETAILS").Rows(i)("Agency1AOffice")
                            drow("AgencyName") = objds.Tables("DETAILS").Rows(i)("AgencyName")
                            drow("OfficeId") = objds.Tables("DETAILS").Rows(i)("OfficeId")
                            drow("LoggedBy") = objds.Tables("DETAILS").Rows(i)("LoggedBy")
                            drow("Callername") = objds.Tables("DETAILS").Rows(i)("Callername")
                            drow("Customercategory") = objds.Tables("DETAILS").Rows(i)("Customercategory")
                            drow("QueryGroup") = objds.Tables("DETAILS").Rows(i)("QueryGroup")
                            drow("QuerySubGroup") = objds.Tables("DETAILS").Rows(i)("QuerySubGroup")
                            drow("QueryCategory") = objds.Tables("DETAILS").Rows(i)("QueryCategory")
                            drow("QuerySubCategory") = objds.Tables("DETAILS").Rows(i)("QuerySubCategory")
                            drow("QueryStatus") = objds.Tables("DETAILS").Rows(i)("QueryStatus")
                            drow("QueryPriority") = objds.Tables("DETAILS").Rows(i)("QueryPriority")
                            If objds.Tables("DETAILS").Rows(i)("DateassignedTime").ToString.Trim.Length > 0 Then
                                drow("DateassignedTime") = objds.Tables("DETAILS").Rows(i)("DateassignedTime") ' objeAAMS.GetDateFormat(objds.Tables("DETAILS").Rows(i)("DateassignedTime"), "dd-MMM-yy HH:MM", "dd-MMM-yy hh:mm tt", "-")
                            End If
                            If objds.Tables("DETAILS").Rows(i)("DateopenFrom").ToString.Trim.Length > 0 Then
                                drow("DateopenFrom") = objds.Tables("DETAILS").Rows(i)("DateopenFrom") ' objeAAMS.GetDateFormat(objds.Tables("DETAILS").Rows(i)("DateopenFrom"), "dd-MMM-yy HH:MM", "dd-MMM-yy hh:mm tt", "-")
                            End If
                            If objds.Tables("DETAILS").Rows(i)("DateopenTo").ToString.Trim.Length > 0 Then
                                drow("DateopenTo") = objds.Tables("DETAILS").Rows(i)("DateopenTo") ' objeAAMS.GetDateFormat(objds.Tables("DETAILS").Rows(i)("DateopenTo"), "dd-MMM-yy HH:MM", "dd-MMM-yy hh:mm tt", "-")
                            End If
                            If objds.Tables("DETAILS").Rows(i)("ClosedateFrom").ToString.Trim.Length > 0 Then
                                drow("ClosedateFrom") = objds.Tables("DETAILS").Rows(i)("ClosedateFrom") ' objeAAMS.GetDateFormat(objds.Tables("DETAILS").Rows(i)("ClosedateFrom"), "dd-MMM-yy HH:MM", "dd-MMM-yy hh:mm tt", "-")
                            End If
                            If objds.Tables("DETAILS").Rows(i)("ClosedateTo").ToString.Trim.Length > 0 Then
                                drow("ClosedateTo") = objds.Tables("DETAILS").Rows(i)("ClosedateTo") ' objeAAMS.GetDateFormat(objds.Tables("DETAILS").Rows(i)("ClosedateTo"), "dd-MMM-yy HH:MM", "dd-MMM-yy hh:mm tt", "-")
                            End If

                            drow("PRTNo") = objds.Tables("DETAILS").Rows(i)("PRTNo")
                            drow("LTRNo") = objds.Tables("DETAILS").Rows(i)("LTRNo")
                            drow("CallAssignedTo") = objds.Tables("DETAILS").Rows(i)("CallAssignedTo")
                            drow("Co_ordinator1") = objds.Tables("DETAILS").Rows(i)("Co_ordinator1")
                            drow("Workorderno") = objds.Tables("DETAILS").Rows(i)("Workorderno")
                            drow("contacttype") = objds.Tables("DETAILS").Rows(i)("contacttype")
                            drow("Discription") = objds.Tables("DETAILS").Rows(i)("Discription")
                            drow("Solution") = objds.Tables("DETAILS").Rows(i)("Solution")
                            dtTectReport.Rows.Add(drow)
                            dtTectReport.AcceptChanges()
                        Next
                        Dim dset As New DataSet
                        dset.Tables.Add(dtTectReport)

                        ' dset.WriteXmlSchema("C:\TechReport.xsd")
                        ' objRepDocument.Load(Server.MapPath("HelpDesk/Reports/Copy (2) of RptTechnicalReport.rpt"))

                        objRepDocument.Load(Server.MapPath("BackOfficeHelpDesk/Reports/RptBOTechnicalReport.rpt"))
                        objRepDocument.SetDataSource(dset)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = True
                        CRViewer1.HasPageNavigationButtons = True

                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 03 Feb  2009
                        BindReport(objRepDocument)
                    End If


                Case Is = "BOHelpDeskDynamicReport"

                    If Session("BOHelpDeskDynamicReport") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then

                        objxmlout.LoadXml(Session("BOHelpDeskDynamicReport"))

                        '    objRepDocument.Load(Server.MapPath("HelpDesk/Reports/RptHelpDeskDynamicReport.rpt"))


                        If objxmlout.DocumentElement.SelectNodes("SEARCHDESC[@Ltr!='']").Count > 0 And objxmlout.DocumentElement.SelectNodes("SEARCHSOLN[@Ltr!='']").Count > 0 Then
                            objRepDocument.Load(Server.MapPath("BackOfficeHelpDesk/Reports/RptBOHelpDeskDynamicReport.rpt"))
                        End If
                        If objxmlout.DocumentElement.SelectNodes("SEARCHDESC[@Ltr!='']").Count <= 0 And objxmlout.DocumentElement.SelectNodes("SEARCHSOLN[@Ltr!='']").Count > 0 Then
                            objRepDocument.Load(Server.MapPath("BackOfficeHelpDesk/Reports/RptBOHelpDeskDynamicReportWithSoln.rpt"))
                        End If
                        If objxmlout.DocumentElement.SelectNodes("SEARCHDESC[@Ltr!='']").Count > 0 And objxmlout.DocumentElement.SelectNodes("SEARCHSOLN[@Ltr!='']").Count <= 0 Then
                            objRepDocument.Load(Server.MapPath("BackOfficeHelpDesk/Reports/RptBOHelpDeskDynamicReportWithDesc.rpt"))
                        End If

                        If objxmlout.DocumentElement.SelectNodes("SEARCHDESC[@Ltr!='']").Count <= 0 And objxmlout.DocumentElement.SelectNodes("SEARCHSOLN[@Ltr!='']").Count <= 0 Then
                            objRepDocument.Load(Server.MapPath("BackOfficeHelpDesk/Reports/RptBOHelpDeskDynamicReportWithoutSolnAndDesc.rpt"))
                        End If



                        'objxmlout.LoadXml("<HD_DMSEARCH_CALLREQUEST_OUTPUT><HEADER FIELD1='LTR_NO' FIELD2='AGENCY' FIELD3='ADDRESS' FIELD4='PTR_NO' FIELD5='LAST_CALL' FIELD6='QUERY_STATUS' FIELD7='FOLLOWUP' FIELD8='SEVERITY' FIELD9='LOGGED_BY' FIELD10='CLOSE_DATE_FROM' FIELD11='CLOSE_DATE_TO' FIELD12='OPEN_DATE_FROM' FIELD13='OPEN_DATE_TO' FIELD14='ASSIGNED_TO' FIELD15='ASSIGNED_DATE_TIME' FIELD16='OFFICE_ID' FIELD17='CUSTOMER_CATEGORY' FIELD18='CALLER_NAME' FIELD19='QUERY_GROUP' FIELD20='QUERY_SUB_GROUP' FIELD21='QUERY_CATEGORY' FIELD22='QUERY_SUB_CATEGORY' FIELD23='COORDINATOR1'  FIELD24='COORDINATOR2' FIELD25='DISPOSITION' FIELD26='1AOFFICE' FIELD27='AGENCY_1A_OFFICE' FIELD28='WORK_ORDER_NO'><DETAIL VALUE1='554' VALUE2='fgfdd879576546gdfg' VALUE3='fgfg' VALUE4='fgfdg' VALUE5='' VALUE6='' VALUE7='' VALUE8='' VALUE9='' VALUE10='' VALUE11='' VALUE12='' VALUE13='' VALUE14='' VALUE15='' VALUE16='' VALUE17='' VALUE18='' VALUE19='' VALUE20='' VALUE21='' VALUE22='' 					VALUE23='' VALUE24='' VALUE25='' VALUE26='' VALUE27='' VALUE28=''/><DETAIL VALUE1='55674' VALUE2='54654654' VALUE3='fgfg' VALUE4='fgfdg' VALUE5='' VALUE6='' VALUE7='' VALUE8='' VALUE9='' VALUE10='' VALUE11='' VALUE12='' VALUE13='' VALUE14='' VALUE15='7' VALUE16='' VALUE17='' VALUE18='' VALUE19='' VALUE20='' VALUE21='' VALUE22='' VALUE23='' VALUE24='' VALUE25='' VALUE26='' VALUE27='' VALUE28=''/><DETAIL VALUE1='567654' VALUE2='fgfhfghfghfggdfg' VALUE3='fgfg657567656657 546  566765756765765765765757' VALUE4='fgfdg' VALUE5='' VALUE6='' VALUE7='' VALUE8='' VALUE9='' VALUE10='' VALUE11='' VALUE12='' VALUE13='' VALUE14='' VALUE15='' VALUE16='' VALUE17='' VALUE18='' VALUE19='' VALUE20='' VALUE21='' VALUE22=''	VALUE23='' VALUE24='' VALUE25='' VALUE26='' VALUE27='h' VALUE28='66'/>	</HEADER><Errors Status=''>	<Error Code='' Description=''/>	</Errors></HD_DMSEARCH_CALLREQUEST_OUTPUT>")
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "BOFeedback"
                    If Session("BOFeedback") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then

                        Dim objfb As New AAMS.bizBOHelpdesk.bzFeedback
                        objRepDocument.Load(Server.MapPath("BackOfficeHelpDesk/Reports/RPTBOFeedback.rpt"))
                        Dim InputXML As New XmlDocument, OutputXML As New XmlDocument
                        'InputXML.LoadXml("<HD_RPTFEEDBACK_INPUT><MONTH>03</MONTH><YEAR>2008</YEAR></HD_RPTFEEDBACK_INPUT>")
                        InputXML.LoadXml(Session("BOFeedback"))

                        OutputXML = objfb.FeedbackReport(InputXML)

                        If OutputXML.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                            '@ Remove Node On the basis of Report Type 
                            If Request.QueryString("Action").ToString().Trim = "F" Then
                                Dim i As Integer = 0
                                For Each objnode As XmlNode In OutputXML.DocumentElement.SelectNodes("SECONDFOURTH")
                                    If i <> 0 Then
                                        OutputXML.DocumentElement.RemoveChild(objnode)
                                    End If
                                    i = i + 1
                                Next
                                i = 0
                                For Each objnode As XmlNode In OutputXML.DocumentElement.SelectNodes("MONTHLY")
                                    If i <> 0 Then
                                        OutputXML.DocumentElement.RemoveChild(objnode)
                                    End If
                                    i = i + 1
                                Next

                            ElseIf Request.QueryString("Action").ToString().Trim = "S" Then
                                Dim i As Integer = 0
                                For Each objnode As XmlNode In OutputXML.DocumentElement.SelectNodes("FIRSTFOURTH")
                                    If i <> 0 Then
                                        OutputXML.DocumentElement.RemoveChild(objnode)
                                    End If
                                    i = i + 1
                                Next
                                i = 0
                                For Each objnode As XmlNode In OutputXML.DocumentElement.SelectNodes("MONTHLY")
                                    If i <> 0 Then
                                        OutputXML.DocumentElement.RemoveChild(objnode)
                                    End If
                                    i = i + 1
                                Next

                            ElseIf Request.QueryString("Action").ToString().Trim = "M" Then
                                Dim i As Integer = 0
                                For Each objnode As XmlNode In OutputXML.DocumentElement.SelectNodes("SECONDFOURTH")
                                    If i <> 0 Then
                                        OutputXML.DocumentElement.RemoveChild(objnode)
                                    End If
                                    i = i + 1
                                Next
                                i = 0
                                For Each objnode As XmlNode In OutputXML.DocumentElement.SelectNodes("FIRSTFOURTH")
                                    If i <> 0 Then
                                        OutputXML.DocumentElement.RemoveChild(objnode)
                                    End If
                                    i = i + 1
                                Next

                            End If
                            objxmlout.LoadXml(OutputXML.OuterXml) 'Session("eFeedback"))
                            xreader = New XmlNodeReader(objxmlout)
                            objds.ReadXml(xreader)
                            objRepDocument.SetDataSource(objds)
                            CRViewer1.EnableParameterPrompt = False
                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = False
                            ' CRViewer1.ReportSource = objRepDocument

                            Dim paramfields As New ParameterFields
                            Dim InstallationFrom_pField As New ParameterField()
                            Dim InstallationTo_pField As New ParameterField()
                            Dim discreteValFrom As New ParameterDiscreteValue()
                            Dim discreteValTo As New ParameterDiscreteValue()

                            InstallationFrom_pField = New ParameterField()
                            discreteValFrom = New ParameterDiscreteValue()
                            InstallationFrom_pField.ParameterFieldName = "RptTypeNew"
                            ' Set the first discrete value and pass it to the parameter
                            discreteValFrom.Value = Request.QueryString("Action").Trim
                            InstallationFrom_pField.CurrentValues.Add(discreteValFrom)
                            paramfields.Add(InstallationFrom_pField)
                            CRViewer1.ParameterFieldInfo = paramfields


                            'Dim paramfields As New ParameterFields
                            'Dim Pra_RptType As New ParameterField()
                            'Dim Pra_Year As New ParameterField()
                            'Dim discreteValFrom As New ParameterDiscreteValue()
                            'Dim discreteValTo As New ParameterDiscreteValue()

                            'Pra_RptType = New ParameterField()
                            'discreteValFrom = New ParameterDiscreteValue()
                            'Pra_RptType.ParameterFieldName = "RptTypeNew"
                            'discreteValFrom.Value = Request.QueryString("Action").Trim

                            'Pra_RptType.CurrentValues.Add(Pra_RptType)
                            'paramfields.Add(Pra_RptType)
                            'CRViewer1.ParameterFieldInfo = paramfields
                            'Session("MyPage") = objRepDocument
                        Else
                            ' Session("MyPage") = Nothing
                            lblError.Text = OutputXML.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If
                Case Is = "ETBRDLetter"
                    If Session("ETeBRDLetterRpt") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then

                        objRepDocument.Load(Server.MapPath("ECOMMTrackerHelpDesk/Reports/RptHDBDRReport.rpt"))
                        objxmlout.LoadXml(Session("ETeBRDLetterRpt"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If
                Case Is = "ETHelpDeskDynamicReport"

                    If Session("ETHelpDeskDynamicReport") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then

                        objxmlout.LoadXml(Session("ETHelpDeskDynamicReport"))

                        '    objRepDocument.Load(Server.MapPath("HelpDesk/Reports/RptHelpDeskDynamicReport.rpt"))


                        If objxmlout.DocumentElement.SelectNodes("SEARCHDESC[@Ltr!='']").Count > 0 And objxmlout.DocumentElement.SelectNodes("SEARCHSOLN[@Ltr!='']").Count > 0 Then
                            objRepDocument.Load(Server.MapPath("ECOMMTrackerHelpDesk/Reports/RptHelpDeskDynamicReport.rpt"))
                        End If
                        If objxmlout.DocumentElement.SelectNodes("SEARCHDESC[@Ltr!='']").Count <= 0 And objxmlout.DocumentElement.SelectNodes("SEARCHSOLN[@Ltr!='']").Count > 0 Then
                            objRepDocument.Load(Server.MapPath("ECOMMTrackerHelpDesk/Reports/RptHelpDeskDynamicReportWithSoln.rpt"))
                        End If
                        If objxmlout.DocumentElement.SelectNodes("SEARCHDESC[@Ltr!='']").Count > 0 And objxmlout.DocumentElement.SelectNodes("SEARCHSOLN[@Ltr!='']").Count <= 0 Then
                            objRepDocument.Load(Server.MapPath("ECOMMTrackerHelpDesk/Reports/RptHelpDeskDynamicReportWithDesc.rpt"))
                        End If

                        If objxmlout.DocumentElement.SelectNodes("SEARCHDESC[@Ltr!='']").Count <= 0 And objxmlout.DocumentElement.SelectNodes("SEARCHSOLN[@Ltr!='']").Count <= 0 Then
                            objRepDocument.Load(Server.MapPath("ECOMMTrackerHelpDesk/Reports/RptHelpDeskDynamicReportWithoutSolnAndDesc.rpt"))
                        End If



                        'objxmlout.LoadXml("<HD_DMSEARCH_CALLREQUEST_OUTPUT><HEADER FIELD1='LTR_NO' FIELD2='AGENCY' FIELD3='ADDRESS' FIELD4='PTR_NO' FIELD5='LAST_CALL' FIELD6='QUERY_STATUS' FIELD7='FOLLOWUP' FIELD8='SEVERITY' FIELD9='LOGGED_BY' FIELD10='CLOSE_DATE_FROM' FIELD11='CLOSE_DATE_TO' FIELD12='OPEN_DATE_FROM' FIELD13='OPEN_DATE_TO' FIELD14='ASSIGNED_TO' FIELD15='ASSIGNED_DATE_TIME' FIELD16='OFFICE_ID' FIELD17='CUSTOMER_CATEGORY' FIELD18='CALLER_NAME' FIELD19='QUERY_GROUP' FIELD20='QUERY_SUB_GROUP' FIELD21='QUERY_CATEGORY' FIELD22='QUERY_SUB_CATEGORY' FIELD23='COORDINATOR1'  FIELD24='COORDINATOR2' FIELD25='DISPOSITION' FIELD26='1AOFFICE' FIELD27='AGENCY_1A_OFFICE' FIELD28='WORK_ORDER_NO'><DETAIL VALUE1='554' VALUE2='fgfdd879576546gdfg' VALUE3='fgfg' VALUE4='fgfdg' VALUE5='' VALUE6='' VALUE7='' VALUE8='' VALUE9='' VALUE10='' VALUE11='' VALUE12='' VALUE13='' VALUE14='' VALUE15='' VALUE16='' VALUE17='' VALUE18='' VALUE19='' VALUE20='' VALUE21='' VALUE22='' 					VALUE23='' VALUE24='' VALUE25='' VALUE26='' VALUE27='' VALUE28=''/><DETAIL VALUE1='55674' VALUE2='54654654' VALUE3='fgfg' VALUE4='fgfdg' VALUE5='' VALUE6='' VALUE7='' VALUE8='' VALUE9='' VALUE10='' VALUE11='' VALUE12='' VALUE13='' VALUE14='' VALUE15='7' VALUE16='' VALUE17='' VALUE18='' VALUE19='' VALUE20='' VALUE21='' VALUE22='' VALUE23='' VALUE24='' VALUE25='' VALUE26='' VALUE27='' VALUE28=''/><DETAIL VALUE1='567654' VALUE2='fgfhfghfghfggdfg' VALUE3='fgfg657567656657 546  566765756765765765765757' VALUE4='fgfdg' VALUE5='' VALUE6='' VALUE7='' VALUE8='' VALUE9='' VALUE10='' VALUE11='' VALUE12='' VALUE13='' VALUE14='' VALUE15='' VALUE16='' VALUE17='' VALUE18='' VALUE19='' VALUE20='' VALUE21='' VALUE22=''	VALUE23='' VALUE24='' VALUE25='' VALUE26='' VALUE27='h' VALUE28='66'/>	</HEADER><Errors Status=''>	<Error Code='' Description=''/>	</Errors></HD_DMSEARCH_CALLREQUEST_OUTPUT>")
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)
                        objRepDocument.SetDataSource(objds)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = False
                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If

                Case Is = "ETFeedback"
                    If Session("ETFeedback") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then

                        Dim objfb As New AAMS.bizETrackerHelpDesk.bzFeedback
                        objRepDocument.Load(Server.MapPath("ECOMMTrackerHelpDesk/Reports/RPTFeedback.rpt"))
                        Dim InputXML As New XmlDocument, OutputXML As New XmlDocument
                        'InputXML.LoadXml("<HD_RPTFEEDBACK_INPUT><MONTH>03</MONTH><YEAR>2008</YEAR></HD_RPTFEEDBACK_INPUT>")
                        InputXML.LoadXml(Session("ETFeedback"))

                        OutputXML = objfb.FeedbackReport(InputXML)

                        If OutputXML.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                            '@ Remove Node On the basis of Report Type 
                            If Request.QueryString("Action").ToString().Trim = "F" Then
                                Dim i As Integer = 0
                                For Each objnode As XmlNode In OutputXML.DocumentElement.SelectNodes("SECONDFOURTH")
                                    If i <> 0 Then
                                        OutputXML.DocumentElement.RemoveChild(objnode)
                                    End If
                                    i = i + 1
                                Next
                                i = 0
                                For Each objnode As XmlNode In OutputXML.DocumentElement.SelectNodes("MONTHLY")
                                    If i <> 0 Then
                                        OutputXML.DocumentElement.RemoveChild(objnode)
                                    End If
                                    i = i + 1
                                Next

                            ElseIf Request.QueryString("Action").ToString().Trim = "S" Then
                                Dim i As Integer = 0
                                For Each objnode As XmlNode In OutputXML.DocumentElement.SelectNodes("FIRSTFOURTH")
                                    If i <> 0 Then
                                        OutputXML.DocumentElement.RemoveChild(objnode)
                                    End If
                                    i = i + 1
                                Next
                                i = 0
                                For Each objnode As XmlNode In OutputXML.DocumentElement.SelectNodes("MONTHLY")
                                    If i <> 0 Then
                                        OutputXML.DocumentElement.RemoveChild(objnode)
                                    End If
                                    i = i + 1
                                Next

                            ElseIf Request.QueryString("Action").ToString().Trim = "M" Then
                                Dim i As Integer = 0
                                For Each objnode As XmlNode In OutputXML.DocumentElement.SelectNodes("SECONDFOURTH")
                                    If i <> 0 Then
                                        OutputXML.DocumentElement.RemoveChild(objnode)
                                    End If
                                    i = i + 1
                                Next
                                i = 0
                                For Each objnode As XmlNode In OutputXML.DocumentElement.SelectNodes("FIRSTFOURTH")
                                    If i <> 0 Then
                                        OutputXML.DocumentElement.RemoveChild(objnode)
                                    End If
                                    i = i + 1
                                Next

                            End If
                            objxmlout.LoadXml(OutputXML.OuterXml) 'Session("eFeedback"))
                            xreader = New XmlNodeReader(objxmlout)
                            objds.ReadXml(xreader)
                            objRepDocument.SetDataSource(objds)
                            CRViewer1.EnableParameterPrompt = False
                            CRViewer1.DisplayGroupTree = False
                            CRViewer1.EnableViewState = False
                            ' CRViewer1.ReportSource = objRepDocument

                            Dim paramfields As New ParameterFields
                            Dim InstallationFrom_pField As New ParameterField()
                            Dim InstallationTo_pField As New ParameterField()
                            Dim discreteValFrom As New ParameterDiscreteValue()
                            Dim discreteValTo As New ParameterDiscreteValue()

                            InstallationFrom_pField = New ParameterField()
                            discreteValFrom = New ParameterDiscreteValue()
                            InstallationFrom_pField.ParameterFieldName = "RptTypeNew"
                            ' Set the first discrete value and pass it to the parameter
                            discreteValFrom.Value = Request.QueryString("Action").Trim
                            InstallationFrom_pField.CurrentValues.Add(discreteValFrom)
                            paramfields.Add(InstallationFrom_pField)
                            CRViewer1.ParameterFieldInfo = paramfields


                            'Dim paramfields As New ParameterFields
                            'Dim Pra_RptType As New ParameterField()
                            'Dim Pra_Year As New ParameterField()
                            'Dim discreteValFrom As New ParameterDiscreteValue()
                            'Dim discreteValTo As New ParameterDiscreteValue()

                            'Pra_RptType = New ParameterField()
                            'discreteValFrom = New ParameterDiscreteValue()
                            'Pra_RptType.ParameterFieldName = "RptTypeNew"
                            'discreteValFrom.Value = Request.QueryString("Action").Trim

                            'Pra_RptType.CurrentValues.Add(Pra_RptType)
                            'paramfields.Add(Pra_RptType)
                            'CRViewer1.ParameterFieldInfo = paramfields
                            'Session("MyPage") = objRepDocument
                        Else
                            ' Session("MyPage") = Nothing
                            lblError.Text = OutputXML.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            Exit Select
                        End If
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 22 August
                        BindReport(objRepDocument)
                    End If


                Case Is = "ETTechReport"
                    If Session("ETTechReport") IsNot Nothing Then
                        ' If Not Page.IsPostBack Then
                        objxmlout.LoadXml(Session("ETTechReport"))
                        xreader = New XmlNodeReader(objxmlout)
                        objds.ReadXml(xreader)


                        Dim dtTectReport As New DataTable("DETAILS")
                        Dim drow As DataRow

                        dtTectReport.Columns.Add("Region", GetType(String))
                        dtTectReport.Columns.Add("State", GetType(String))
                        dtTectReport.Columns.Add("City", GetType(String))
                        dtTectReport.Columns.Add("A1Office", GetType(String))
                        dtTectReport.Columns.Add("Agency1AOffice", GetType(String))
                        dtTectReport.Columns.Add("AgencyName", GetType(String))
                        dtTectReport.Columns.Add("OfficeId", GetType(String))
                        dtTectReport.Columns.Add("LoggedBy", GetType(String))
                        dtTectReport.Columns.Add("Callername", GetType(String))
                        dtTectReport.Columns.Add("Customercategory", GetType(String))
                        dtTectReport.Columns.Add("QueryGroup", GetType(String))
                        dtTectReport.Columns.Add("QuerySubGroup", GetType(String))
                        dtTectReport.Columns.Add("QueryCategory", GetType(String))
                        dtTectReport.Columns.Add("QuerySubCategory", GetType(String))
                        dtTectReport.Columns.Add("QueryStatus", GetType(String))
                        dtTectReport.Columns.Add("QueryPriority", GetType(String))
                        dtTectReport.Columns.Add("DateassignedTime", GetType(DateTime)).AllowDBNull = True
                        dtTectReport.Columns.Add("DateopenFrom", GetType(DateTime)).AllowDBNull = True
                        dtTectReport.Columns.Add("DateopenTo", GetType(DateTime)).AllowDBNull = True
                        dtTectReport.Columns.Add("ClosedateFrom", GetType(DateTime)).AllowDBNull = True
                        dtTectReport.Columns.Add("ClosedateTo", GetType(DateTime)).AllowDBNull = True
                        dtTectReport.Columns.Add("PRTNo", GetType(String))
                        dtTectReport.Columns.Add("LTRNo", GetType(String))
                        dtTectReport.Columns.Add("CallAssignedTo", GetType(String))
                        dtTectReport.Columns.Add("Co_ordinator1", GetType(String))
                        dtTectReport.Columns.Add("Workorderno", GetType(String))
                        dtTectReport.Columns.Add("contacttype", GetType(String))
                        dtTectReport.Columns.Add("Discription", GetType(String))
                        dtTectReport.Columns.Add("Solution", GetType(String))

                        For i As Integer = 0 To objds.Tables("DETAILS").Rows.Count - 1
                            drow = dtTectReport.NewRow

                            drow("Region") = objds.Tables("DETAILS").Rows(i)("Region")
                            drow("State") = objds.Tables("DETAILS").Rows(i)("State")
                            drow("City") = objds.Tables("DETAILS").Rows(i)("City")
                            drow("A1Office") = objds.Tables("DETAILS").Rows(i)("A1Office")
                            drow("Agency1AOffice") = objds.Tables("DETAILS").Rows(i)("Agency1AOffice")
                            drow("AgencyName") = objds.Tables("DETAILS").Rows(i)("AgencyName")
                            drow("OfficeId") = objds.Tables("DETAILS").Rows(i)("OfficeId")
                            drow("LoggedBy") = objds.Tables("DETAILS").Rows(i)("LoggedBy")
                            drow("Callername") = objds.Tables("DETAILS").Rows(i)("Callername")
                            drow("Customercategory") = objds.Tables("DETAILS").Rows(i)("Customercategory")
                            drow("QueryGroup") = objds.Tables("DETAILS").Rows(i)("QueryGroup")
                            drow("QuerySubGroup") = objds.Tables("DETAILS").Rows(i)("QuerySubGroup")
                            drow("QueryCategory") = objds.Tables("DETAILS").Rows(i)("QueryCategory")
                            drow("QuerySubCategory") = objds.Tables("DETAILS").Rows(i)("QuerySubCategory")
                            drow("QueryStatus") = objds.Tables("DETAILS").Rows(i)("QueryStatus")
                            drow("QueryPriority") = objds.Tables("DETAILS").Rows(i)("QueryPriority")
                            If objds.Tables("DETAILS").Rows(i)("DateassignedTime").ToString.Trim.Length > 0 Then
                                drow("DateassignedTime") = objds.Tables("DETAILS").Rows(i)("DateassignedTime") ' objeAAMS.GetDateFormat(objds.Tables("DETAILS").Rows(i)("DateassignedTime"), "dd-MMM-yy HH:MM", "dd-MMM-yy hh:mm tt", "-")
                            End If
                            If objds.Tables("DETAILS").Rows(i)("DateopenFrom").ToString.Trim.Length > 0 Then
                                drow("DateopenFrom") = objds.Tables("DETAILS").Rows(i)("DateopenFrom") ' objeAAMS.GetDateFormat(objds.Tables("DETAILS").Rows(i)("DateopenFrom"), "dd-MMM-yy HH:MM", "dd-MMM-yy hh:mm tt", "-")
                            End If
                            If objds.Tables("DETAILS").Rows(i)("DateopenTo").ToString.Trim.Length > 0 Then
                                drow("DateopenTo") = objds.Tables("DETAILS").Rows(i)("DateopenTo") ' objeAAMS.GetDateFormat(objds.Tables("DETAILS").Rows(i)("DateopenTo"), "dd-MMM-yy HH:MM", "dd-MMM-yy hh:mm tt", "-")
                            End If
                            If objds.Tables("DETAILS").Rows(i)("ClosedateFrom").ToString.Trim.Length > 0 Then
                                drow("ClosedateFrom") = objds.Tables("DETAILS").Rows(i)("ClosedateFrom") ' objeAAMS.GetDateFormat(objds.Tables("DETAILS").Rows(i)("ClosedateFrom"), "dd-MMM-yy HH:MM", "dd-MMM-yy hh:mm tt", "-")
                            End If
                            If objds.Tables("DETAILS").Rows(i)("ClosedateTo").ToString.Trim.Length > 0 Then
                                drow("ClosedateTo") = objds.Tables("DETAILS").Rows(i)("ClosedateTo") ' objeAAMS.GetDateFormat(objds.Tables("DETAILS").Rows(i)("ClosedateTo"), "dd-MMM-yy HH:MM", "dd-MMM-yy hh:mm tt", "-")
                            End If

                            drow("PRTNo") = objds.Tables("DETAILS").Rows(i)("PRTNo")
                            drow("LTRNo") = objds.Tables("DETAILS").Rows(i)("LTRNo")
                            drow("CallAssignedTo") = objds.Tables("DETAILS").Rows(i)("CallAssignedTo")
                            drow("Co_ordinator1") = objds.Tables("DETAILS").Rows(i)("Co_ordinator1")
                            drow("Workorderno") = objds.Tables("DETAILS").Rows(i)("Workorderno")
                            drow("contacttype") = objds.Tables("DETAILS").Rows(i)("contacttype")
                            drow("Discription") = objds.Tables("DETAILS").Rows(i)("Discription")
                            drow("Solution") = objds.Tables("DETAILS").Rows(i)("Solution")
                            dtTectReport.Rows.Add(drow)
                            dtTectReport.AcceptChanges()
                        Next
                        Dim dset As New DataSet
                        dset.Tables.Add(dtTectReport)

                        ' dset.WriteXmlSchema("C:\TechReport.xsd")
                        ' objRepDocument.Load(Server.MapPath("HelpDesk/Reports/Copy (2) of RptTechnicalReport.rpt"))

                        objRepDocument.Load(Server.MapPath("ECOMMTrackerHelpDesk/Reports/RptTechnicalReport.rpt"))
                        objRepDocument.SetDataSource(dset)
                        CRViewer1.EnableParameterPrompt = False
                        CRViewer1.DisplayGroupTree = False
                        CRViewer1.EnableViewState = True
                        CRViewer1.HasPageNavigationButtons = True

                        'Session("MyPage") = objRepDocument
                        'Else
                        '    objRepDocument = CType(Session("MyPage"), ReportDocument)
                        'End If
                        'Modified on 03 Feb  2009
                        BindReport(objRepDocument)
                    End If
                    ''***************************************end here******************************************




            End Select
        End If
    End Sub
    Function ReturnQuarter(ByVal intMonth As Integer, ByVal intYear As Integer) As String
        'Dim intMonth = Date.Today.Month()

        Dim strQuarter As String = ""
        If (intMonth >= 1 And intMonth <= 3) Then
            strQuarter = "1 "
        ElseIf (intMonth >= 4 And intMonth <= 6) Then
            strQuarter = "2 "
        ElseIf (intMonth >= 7 And intMonth <= 9) Then
            strQuarter = "3 "
        ElseIf (intMonth >= 10 And intMonth <= 12) Then
            strQuarter = "4 "
        End If
        Return strQuarter
    End Function

#Region "ConvertNumberToWord"
    Shared Function CurrencyToWord(ByVal MyNumber)
        Dim Amt As Double = CDbl(Val(MyNumber))
        Dim Temp
        Dim Rupees, Paisa As String
        Dim DecimalPlace, iCount
        Dim Hundred, Words As String

        Dim ch As String
        Dim i As Integer
        Dim count As Integer
        Dim Alldigits As Boolean

        Dim Place(9) As String
        Place(0) = " Thousand "
        Place(2) = " Lakh "
        Place(4) = " Crore "
        Place(6) = " Arab "
        Place(8) = " Kharab "
        On Error Resume Next
        ' Convert MyNumber to a string, trimming extra spaces.

        MyNumber = Trim(Microsoft.VisualBasic.Str(MyNumber))
        '==========================Modified================================
        ' count "." if it is more than 1
        For i = 1 To Len(MyNumber)
            If Mid$(MyNumber, i, 1) = "." Then
                count = count + 1
                If count > 1 Then
                    CurrencyToWord = "Invalid Currency....!"
                    Exit Function
                End If
            End If
        Next i
        ' check all the digits are numbers
        Alldigits = True
        For i = 1 To Len(MyNumber)
            ' See if the next character is a non-digit.
            ch = Mid$(MyNumber, i, 1)
            If ch < "0" Or ch > "9" Or ch = "." Then
                If ch <> "." Then
                    ' This is not a digit.
                    Alldigits = False
                    Exit For
                End If
            End If
        Next i

        If Alldigits = False Then
            CurrencyToWord = "Invalid Currency....!"
            Exit Function
        End If
        '====================================================================

        ' Find decimal place.
        DecimalPlace = InStr(MyNumber, ".")
        '==========================Modified==================================
        If DecimalPlace = 0 Then
            If Len(MyNumber) > 13 Then
                CurrencyToWord = "Value is too large, Function accepts 13 digits before decimal point"
                Exit Function
            End If
        End If
        '====================================================================
        ' If we find decimal place...
        If DecimalPlace > 0 Then
            '==========================Modified==================================
            If DecimalPlace > 14 Then
                CurrencyToWord = "Value is too large, Function accepts 13 digits before decimal point"
                Exit Function
            End If
            '====================================================================
            ' Convert Paisa
            Temp = Left(Mid(MyNumber, DecimalPlace + 1) & "00", 2)


            ' ModiFied By Abhishek
            '   Paisa = " and " & ConvertTens(Temp) & " Paisa Only"
            If Val(DecimalPlace) > 0 Then
                Paisa = " and " & ConvertTens(Temp) & " Paisa Only"
            End If


            ' Strip off paisa from remainder to convert.
            MyNumber = Trim(Left(MyNumber, DecimalPlace - 1))
        End If

        ' Convert last 3 digits of MyNumber to Rupees in word.
        Hundred = ConvertHundred(Right(MyNumber, 3))
        '==========================Modified============
        If Len(MyNumber) <= 2 Then
            ' Append leading zeros to number.
            MyNumber = Right("000" & MyNumber, 3)
            MyNumber = Left(MyNumber, Len(MyNumber) - 3)
        Else
            ' Strip off last three digits
            MyNumber = Left(MyNumber, Len(MyNumber) - 3)
        End If
        '====================================================================
        iCount = 0
        Do While MyNumber <> ""
            'Strip last two digits
            Temp = Right(MyNumber, 2)
            If Len(MyNumber) = 1 Then
                Words = ConvertDigit(Temp) & Place(iCount) & Words
                MyNumber = Left(MyNumber, Len(MyNumber) - 1)

            Else
                '==========================Modified==================================
                If Temp <> "00" Then
                    Words = ConvertTens(Temp) & Place(iCount) & Words
                    MyNumber = Left(MyNumber, Len(MyNumber) - 2)
                Else
                    MyNumber = Left(MyNumber, Len(MyNumber) - 2)
                End If
            End If
            iCount = iCount + 2
        Loop

        '@ ModiFied By Abhishek
        'If Paisa = "" Then
        '    CurrencyToWord = "Rupees " & Words & Hundred & " Only."
        'Else
        '    CurrencyToWord = "Rupees " & Words & Hundred & Paisa
        'End If

        If Amt >= 1 Then
            If Paisa = "" Or Paisa Is Nothing Then
                'CurrencyToWord = "Rupees " & Words & Hundred & " Only."
                CurrencyToWord = Words & Hundred & " Only."
            Else
                ' CurrencyToWord = "Rupees " & Words & Hundred & Paisa
                CurrencyToWord = Words & Hundred & Paisa
            End If
        Else
            CurrencyToWord = "" & Words & Hundred & Paisa
            CurrencyToWord = CurrencyToWord.ToString.Replace("and", "")
        End If
        '=====================================================================
    End Function

    Private Shared Function ConvertHundred(ByVal MyNumber)
        Dim Result As String

        ' Exit if there is nothing to convert.
        If Val(MyNumber) = 0 Then Exit Function

        ' Append leading zeros to number.
        MyNumber = Right("000" & MyNumber, 3)

        ' Do we have a Hundred place digit to convert?
        If Left(MyNumber, 1) <> "0" Then
            Result = ConvertDigit(Left(MyNumber, 1)) & " Hundred "
        End If

        ' Do we have a tens place digit to convert?
        If Mid(MyNumber, 2, 1) <> "0" Then
            Result = Result & ConvertTens(Mid(MyNumber, 2))
        Else
            ' If not, then convert the ones place digit.
            Result = Result & ConvertDigit(Mid(MyNumber, 3))
        End If

        ConvertHundred = Trim(Result)
    End Function

    Private Shared Function ConvertTens(ByVal MyTens)
        Dim Result As String

        ' Is value between 10 and 19?
        If Val(Left(MyTens, 1)) = 1 Then
            Select Case Val(MyTens)
                Case 10 : Result = "Ten"
                Case 11 : Result = "Eleven"
                Case 12 : Result = "Twelve"
                Case 13 : Result = "Thirteen"
                Case 14 : Result = "Fourteen"
                Case 15 : Result = "Fifteen"
                Case 16 : Result = "Sixteen"
                Case 17 : Result = "Seventeen"
                Case 18 : Result = "Eighteen"
                Case 19 : Result = "Nineteen"
                Case Else
            End Select
        Else
            ' .. otherwise it's between 20 and 99.
            Select Case Val(Left(MyTens, 1))
                Case 2 : Result = "Twenty "
                Case 3 : Result = "Thirty "
                Case 4 : Result = "Forty "
                Case 5 : Result = "Fifty "
                Case 6 : Result = "Sixty "
                Case 7 : Result = "Seventy "
                Case 8 : Result = "Eighty "
                Case 9 : Result = "Ninety "
                Case Else
            End Select

            ' Convert ones place digit.
            Result = Result & ConvertDigit(Right(MyTens, 1))
        End If

        ConvertTens = Result
    End Function

    Private Shared Function ConvertDigit(ByVal MyDigit)
        Select Case Val(MyDigit)
            Case 1 : ConvertDigit = "One"
            Case 2 : ConvertDigit = "Two"
            Case 3 : ConvertDigit = "Three"
            Case 4 : ConvertDigit = "Four"
            Case 5 : ConvertDigit = "Five"
            Case 6 : ConvertDigit = "Six"
            Case 7 : ConvertDigit = "Seven"
            Case 8 : ConvertDigit = "Eight"
            Case 9 : ConvertDigit = "Nine"
            Case Else : ConvertDigit = ""
        End Select
    End Function
#End Region

    Private Sub SetParameters()
        Dim InputXML As New XmlDocument, OutputXML As New XmlDocument
        InputXML.LoadXml(Session("CityWiseBudget"))

        Dim paramfields As New ParameterFields
        Dim Pra_Month As New ParameterField()
        Dim Pra_Year As New ParameterField()
        Dim discreteValFrom As New ParameterDiscreteValue()
        Dim discreteValTo As New ParameterDiscreteValue()

        Pra_Month = New ParameterField()
        discreteValFrom = New ParameterDiscreteValue()
        Pra_Month.ParameterFieldName = "PMonth"
        discreteValFrom.Value = InputXML.DocumentElement.SelectSingleNode("Month").InnerText.Trim()
        Pra_Month.CurrentValues.Add(discreteValFrom)



        Pra_Year = New ParameterField()
        discreteValTo = New ParameterDiscreteValue()
        Pra_Year.ParameterFieldName = "PYear"
        discreteValTo.Value = InputXML.DocumentElement.SelectSingleNode("Year").InnerText.Trim()
        Pra_Year.CurrentValues.Add(discreteValTo)
        paramfields.Add(Pra_Month)
        paramfields.Add(Pra_Year)
        CRViewer1.ParameterFieldInfo = paramfields
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        hdStatus.Value = "0"
        If hdData.Value <> "" Then
            Dim objxmlout As New XmlDocument
            Dim eCashXSession As New XmlDocument
            Dim objds As New DataSet
            Dim xreader As XmlReader
            objxmlout.LoadXml("<TR_VIEWLETTERS_OUTPUT><DETAILS TR_CLETTER_ID='' TR_CLETTER='' /></TR_VIEWLETTERS_OUTPUT>")
            'Dim dt As New DataTable
            'dt.Columns.Add(New DataColumn("TR_CLETTER_ID"))
            'dt.Columns.Add(New DataColumn("TR_CLETTER"))
            'Dim dr As DataRow
            Dim arstrData() As String = hdData.Value.Split("|")
            Dim objNode, objCloneNode As XmlNode
            objNode = objxmlout.DocumentElement.SelectSingleNode("DETAILS")
            objCloneNode = objNode.CloneNode(True)
            objxmlout.DocumentElement.RemoveChild(objNode)
            For i As Integer = 0 To arstrData.Length - 1
                'dr = dt.NewRow
                'dr("TR_CLETTER_ID") = ""
                'dr("TR_CLETTER") = arstrData(i)
                'dt.Rows.Add(dr)
                objCloneNode.Attributes("TR_CLETTER_ID").Value = ""
                objCloneNode.Attributes("TR_CLETTER").Value = arstrData(i)
                objxmlout.DocumentElement.AppendChild(objCloneNode)
                objCloneNode = objNode.CloneNode(True)
            Next
            xreader = New XmlNodeReader(objxmlout)
            objds.ReadXml(xreader)
            'objds.Tables.Add(dt)
            Dim objRepDocument As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            objRepDocument.Load(Server.MapPath("Training/Reports/rptLetterAll.rpt"))
            objRepDocument.SetDataSource(objds)
            objRepDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, False, "")
        End If
    End Sub

    Private Sub BindReport(ByVal objRepDocument As ReportDocument)
        If objRepDocument IsNot Nothing Then
            CRViewer1.HasPageNavigationButtons = True
            CRViewer1.EnableViewState = True
            CRViewer1.ReportSource = objRepDocument
            CRViewer1.DataBind()
        Else
            CRViewer1.ReportSource = Nothing
            CRViewer1.DataBind()
        End If
    End Sub

    Protected Sub FrmReportShow_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles FrmReportShow.Unload
        Try
            objRepDocument.Close()
        Catch ex As Exception

        End Try
    End Sub
End Class
