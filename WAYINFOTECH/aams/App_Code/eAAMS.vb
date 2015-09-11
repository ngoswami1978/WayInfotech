Imports System.Xml
Imports System.Data
Imports Microsoft.VisualBasic
Imports AAMS.bizMaster
Imports AAMS
Imports AAMS.bizInterface
Imports AAMS.bizShared
Imports System.Globalization

Public Class eAAMS
    Public Sub BindDropDown(ByVal drpDownList As DropDownList, ByVal strType As String, ByVal bolSelect As Boolean, Optional ByVal SelectOption As Integer = 1)
        Dim objOutputXml As XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Select Case strType
            'Code For Sales module 
            'Added By Pankaj 
            'Added On 11th May 11
            Case "ServiceCallStatus"
                Dim objServiceCallStatus As New AAMS.bizSales.bzServiceCallStatus
                objOutputXml = New XmlDocument
                drpDownList.Items.Clear()
                objOutputXml = objServiceCallStatus.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    For Each objNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("SERVICE_CALL_STATUS")
                        Dim li As New ListItem(objNode.Attributes("SC_STATUS_NAME").Value, objNode.Attributes("SC_STATUSID").Value & "|" & objNode.Attributes("SC_STATUS_CLOSE").Value)
                        drpDownList.Items.Add(li)
                    Next
                End If
                '<STRATEGIC_VISIT_RETENTION SVR_REASONID='' SVR_REASON_NAME=''/>
            Case "RetentionReason"
                Dim objRetentionReason As New AAMS.bizSales.bzStrategicVisitRetentionReason
                objOutputXml = New XmlDocument
                objOutputXml = objRetentionReason.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("STRATEGIC_VISIT_RETENTION")
                    drpDownList.DataTextField = "SVR_REASON_NAME"
                    drpDownList.DataValueField = "SVR_REASONID"
                    drpDownList.DataBind()
                End If
                '<STRATEGIC_VISIT_RETENTION SVR_STATUSID='' SVR_STATUS_NAME=''/>
            Case "RetentionStatus"
                Dim objRetentionStatus As New AAMS.bizSales.bzStrategicVisitRetentionStatus
                objOutputXml = New XmlDocument
                objOutputXml = objRetentionStatus.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("STRATEGIC_VISIT_RETENTION")
                    drpDownList.DataTextField = "SVR_STATUS_NAME"
                    drpDownList.DataValueField = "SVR_STATUSID"
                    drpDownList.DataBind()
                End If
                '<STRATEGIC_VISIT_TARGET SVT_STATUSID='' SVT_STATUS_NAME=''/>
            Case "TargetStatus"
                Dim objTargetStatus As New AAMS.bizSales.bzStrategicVisitTargetStatus
                objOutputXml = New XmlDocument
                objOutputXml = objTargetStatus.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("STRATEGIC_VISIT_TARGET")
                    drpDownList.DataTextField = "SVT_STATUS_NAME"
                    drpDownList.DataValueField = "SVT_STATUSID"
                    drpDownList.DataBind()
                End If
                '<STRATEGIC_VISIT_STATUS SV_STATUSID='' SV_STATUS_NAME=''/>
            Case "AirNonAirOTHStatus"
                Dim objStrategicVisitSatus As New AAMS.bizSales.bzStrategicVisitSatus
                objOutputXml = New XmlDocument
                objOutputXml = objStrategicVisitSatus.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("STRATEGIC_VISIT_STATUS")
                    drpDownList.DataTextField = "SV_STATUS_NAME"
                    drpDownList.DataValueField = "SV_STATUSID"
                    drpDownList.DataBind()
                End If

                'End

                'Code Added  on 12 Aug 09
            Case "INCENTIVE_STATUS"
                Dim objBusinessCase As New AAMS.bizIncetive.bzBusinessCase
                objOutputXml = New XmlDocument
                objOutputXml = objBusinessCase.List_Inc_Status()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("STATUS")
                    drpDownList.DataTextField = "APPROVAL_STATUS_NAME"
                    drpDownList.DataValueField = "APPROVAL_STATUS_ID"
                    drpDownList.DataBind()
                End If

                ' ######################################## Start of Code ########################################
                'Code Added  on 28 DEc 2010 By Abhishek
                '@ This is used in productivity/market module for agency group classification type like AA/AB/AC etc.
            Case "AGENCYGROUPCLASSTYPE"
                Dim ObjbzAgencyType As New AAMS.bizTravelAgency.bzAgencyType
                objOutputXml = New XmlDocument
                objOutputXml = ObjbzAgencyType.ListGroupAgencyType
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("AGENCYGROUPTYPE")
                    drpDownList.DataTextField = "TYPE"
                    drpDownList.DataValueField = "TYPEID"
                    drpDownList.DataBind()
                End If
                'Code Added  on 28 DEc 2010 By Abhishek
                '@ This is used in productivity/market module for agency group classification type like AA/AB/AC etc.
                ' ######################################## End of Code ########################################

            Case "GROUP_CLASSIFICATION"
                Dim objAgencyGroup As New AAMS.bizMaster.bzAgencyGroup
                objOutputXml = New XmlDocument
                objOutputXml = objAgencyGroup.ListGroupClassificationType()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("GROUPCLASSIFICATION_TYPE")
                    drpDownList.DataTextField = "Group_Classification_Name"
                    drpDownList.DataValueField = "GroupClassificationID"
                    drpDownList.DataBind()
                End If

                'Sepecifc not for general
            Case "IPPROVIDER_NOT"
                Dim objProvider As New AAMS.bizISP.bzIP_ISPProvider
                Dim objNodeList As XmlNodeList
                objOutputXml = New XmlDocument
                objOutputXml = objProvider.List() '.ListFeedbackSuggestionStatus()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        objNodeList = objOutputXml.DocumentElement.SelectNodes("ISP")
                        For Each xNode As XmlNode In objNodeList
                            drpDownList.Items.Add(New ListItem(xNode.Attributes("ProviderName").Value.Trim, xNode.Attributes("ProviderID").Value.Trim & "|" & xNode.Attributes("Reserved").Value.Trim))
                        Next
                    End If
                End If


            Case "IPPROVIDER"
                Dim objProvider As New AAMS.bizISP.bzIP_ISPProvider
                'Dim objNodeList As XmlNodeList
                objOutputXml = New XmlDocument
                objOutputXml = objProvider.List() '.ListFeedbackSuggestionStatus()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("ISP")
                    drpDownList.DataTextField = "ProviderName"
                    drpDownList.DataValueField = "ProviderID"
                    drpDownList.DataBind()
                End If

            Case "IPPOP"
                Dim objPop As New AAMS.bizISP.bzPOP
                objOutputXml = New XmlDocument
                objOutputXml = objPop.List() '.ListFeedbackSuggestionStatus()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("ISP")
                    drpDownList.DataTextField = "PopName"
                    drpDownList.DataValueField = "PopID"
                    drpDownList.DataBind()
                End If


            Case "FIRSTFORM"
                Dim objEmployee As New AAMS.bizMaster.bzEmployee
                objOutputXml = New XmlDocument
                objOutputXml = objEmployee.ListFirstForm() '.ListFeedbackSuggestionStatus()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("EMPLOYEE_FIRSTFORM")
                    drpDownList.DataTextField = "DISPLAYFORMNAME"
                    drpDownList.DataValueField = "DISPLAYFORMNAME"
                    drpDownList.DataBind()
                End If

            Case "SETCONFIGURATION"
                Dim objbzConfig As New AAMS.bizMaster.bzConfiguration
                objOutputXml = New XmlDocument
                objOutputXml = objbzConfig.List() '.ListFeedbackSuggestionStatus()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CONFIGRURABLE")
                    drpDownList.DataTextField = "CCA_NAME"
                    drpDownList.DataValueField = "CCA_ID"
                    drpDownList.DataBind()
                End If

            Case "FEEDBACKSTATUS"
                Dim objbzFeedBack As New AAMS.bizHelpDesk.bzFeedback
                objOutputXml = New XmlDocument
                objOutputXml = objbzFeedBack.ListFeedbackSuggestionStatus()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("STATUS")
                    drpDownList.DataTextField = "FEEDBACK_STATUS_NAME"
                    drpDownList.DataValueField = "FEEDBACK_STATUS_ID"
                    drpDownList.DataBind()
                End If
            Case "DOMAIN"
                Dim objbzDomain As New AAMS.bizTraining.bzDomain
                objOutputXml = New XmlDocument
                objOutputXml = objbzDomain.ListDomain()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("DOMAIN")
                    drpDownList.DataTextField = "TR_VALTOPICDOM_NAME"
                    drpDownList.DataValueField = "TR_VALTOPICDOM_ID"
                    drpDownList.DataBind()
                End If


                ' Used in Training Module
            Case "FEEDBACK"
                Dim objParticipantsBasket As New AAMS.bizTraining.bzParticipantFeedBack
                objOutputXml = New XmlDocument
                objOutputXml = objParticipantsBasket.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("MOOD")
                    drpDownList.DataTextField = "TR_PART_MOOD_NAME"
                    drpDownList.DataValueField = "TR_PART_MOOD_ID"
                    drpDownList.DataBind()
                End If

            Case "PARTCIPANTSTATUS"
                Dim objParticipantsBasket As New AAMS.bizTraining.bzParticipantsBasket
                objOutputXml = New XmlDocument
                objOutputXml = objParticipantsBasket.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("STATUS")
                    drpDownList.DataTextField = "TR_PARTSTATUS_NAME"
                    drpDownList.DataValueField = "TR_PARTSTATUS_ID"
                    drpDownList.DataBind()
                End If

            Case "COURSE"
                Dim objCourse As New AAMS.bizTraining.bzCourse
                objOutputXml = New XmlDocument
                objOutputXml = objCourse.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("COURSE")
                    drpDownList.DataTextField = "TR_COURSE_NAME"
                    drpDownList.DataValueField = "TR_COURSE_ID"
                    drpDownList.DataBind()
                End If

            Case "COURSELEVEL"
                Dim objCourse As New AAMS.bizTraining.bzCourse
                objOutputXml = New XmlDocument
                objOutputXml = objCourse.ListCourseLevel()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("COURSELEVEL")
                    drpDownList.DataTextField = "TR_COURSELEVEL_NAME"
                    drpDownList.DataValueField = "TR_COURSELEVEL_ID"
                    drpDownList.DataBind()
                End If
            Case "DOCUMENT"
                Dim objCourse As New AAMS.bizTraining.bzCourse
                objOutputXml = New XmlDocument
                objOutputXml = objCourse.ListDocument()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("DOCUMENT")
                    drpDownList.DataTextField = "DOCUMENTNAME"
                    drpDownList.DataValueField = "DOCUMENTID"
                    drpDownList.DataBind()
                End If
                ' end training
            Case "COORDINATOR1"
                Dim objbzCoordinator As New AAMS.bizHelpDesk.bzCoordinator
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCOORDINATOR_INPUT><COORDINATOR_TYPE/><Aoffice/></HD_LISTCOORDINATOR_INPUT>")
                objInXml.DocumentElement.SelectSingleNode("COORDINATOR_TYPE").InnerText = "1"
                objOutputXml = objbzCoordinator.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("COORDINATOR")
                    drpDownList.DataTextField = "EMPLOYEE_NAME"
                    drpDownList.DataValueField = "EMPLOYEE_ID"
                    drpDownList.DataBind()
                End If
            Case "COORDINATOR2"
                Dim objbzCoordinator As New AAMS.bizHelpDesk.bzCoordinator
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCOORDINATOR_INPUT><COORDINATOR_TYPE/><Aoffice/></HD_LISTCOORDINATOR_INPUT>")
                objInXml.DocumentElement.SelectSingleNode("COORDINATOR_TYPE").InnerText = "2"
                objOutputXml = objbzCoordinator.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("COORDINATOR")
                    drpDownList.DataTextField = "EMPLOYEE_NAME"
                    drpDownList.DataValueField = "EMPLOYEE_ID"
                    drpDownList.DataBind()
                End If

            Case "DISPOSITION"
                Dim objbzDisposition As New AAMS.bizHelpDesk.bzDisposition
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
                objOutputXml = objbzDisposition.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("DISPOSITION")
                    drpDownList.DataTextField = "DISPOSITION_NAME"
                    drpDownList.DataValueField = "DISPOSITION_ID"
                    drpDownList.DataBind()
                End If
            Case "TEAM"
                Dim objbzTeam As New AAMS.bizHelpDesk.bzTeam
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
                objOutputXml = objbzTeam.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("TEAM")
                    drpDownList.DataTextField = "TEAM_NAME"
                    drpDownList.DataValueField = "TEAM_ID"
                    drpDownList.DataBind()
                End If
            Case "QueryStatus"
                Dim objCallStatus As New AAMS.bizHelpDesk.bzCallStatus
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSTATUS_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSTATUS_INPUT>")
                objOutputXml = objCallStatus.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_STATUS")
                    drpDownList.DataTextField = "HD_STATUS_NAME"
                    drpDownList.DataValueField = "HD_STATUS_ID"
                    drpDownList.DataBind()
                End If
            Case "QueryStatusWithClosed"
                Dim objCallStatus As New AAMS.bizHelpDesk.bzCallStatus
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                Dim objNodeList As XmlNodeList
                objInXml.LoadXml("<HD_LISTCALLSTATUS_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSTATUS_INPUT>")
                objOutputXml = objCallStatus.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objNodeList = objOutputXml.DocumentElement.SelectNodes("CALL_STATUS")
                    For Each objNode As XmlNode In objNodeList
                        Dim li As New ListItem(objNode.Attributes("HD_STATUS_NAME").Value, objNode.Attributes("HD_STATUS_ID").Value & "|" & objNode.Attributes("HD_STATUS_CLOSE").Value)
                        drpDownList.Items.Add(li)
                    Next
                    'objXmlReader = New XmlNodeReader(objOutputXml)
                    'ds.ReadXml(objXmlReader)
                    'drpDownList.DataSource = ds.Tables("CALL_STATUS")
                    'drpDownList.DataTextField = "HD_STATUS_NAME"
                    'drpDownList.DataValueField = "HD_STATUS_ID" & "|" & "HD_STATUS_CLOSE"
                    'drpDownList.DataBind()
                End If
            Case "QUERYPRIORITY"
                Dim objSeverities As New AAMS.bizHelpDesk.bzSeverities
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
                objOutputXml = objSeverities.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("SEVERITIES")
                    drpDownList.DataTextField = "HD_SEVERITY_NAME"
                    drpDownList.DataValueField = "HD_SEVERITY_ID"
                    drpDownList.DataBind()
                End If
            Case "CUSTOMERCATEGORY"
                Dim objCustomerCategory As New AAMS.bizHelpDesk.bzCustomerCategory
                objOutputXml = New XmlDocument
                objOutputXml = objCustomerCategory.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CUSTOMER_CATEGORY")
                    drpDownList.DataTextField = "CUSTOMER_CATEGORY_NAME"
                    drpDownList.DataValueField = "CUSTOMER_CATEGORY_ID"
                    drpDownList.DataBind()
                End If
            Case "CONTACTTYPE"
                Dim objContactType As New AAMS.bizHelpDesk.bzContactType
                objOutputXml = New XmlDocument
                objOutputXml = objContactType.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CONTACT_TYPE")
                    drpDownList.DataTextField = "CONTACT_TYPE_NAME"
                    drpDownList.DataValueField = "CONTACT_TYPE_ID"
                    drpDownList.DataBind()
                End If
            Case "WorkOrderAssignee"
                Dim objWOAssignees As New AAMS.bizHelpDesk.bzWOAssignees
                objOutputXml = New XmlDocument
                objOutputXml = objWOAssignees.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("WO_ASSIGNEES")
                    drpDownList.DataTextField = "WO_ASSIGNEE_NAME"
                    drpDownList.DataValueField = "WO_ASSIGNEE_ID"
                    drpDownList.DataBind()
                End If
            Case "WorkOrderFollowUp"
                Dim objWOFollowUp As New AAMS.bizHelpDesk.bzWOFollowUp
                objOutputXml = New XmlDocument

                objOutputXml = objWOFollowUp.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("WO_FOLLOWUP")
                    drpDownList.DataTextField = "WO_FOLLOWUP_NAME"
                    drpDownList.DataValueField = "WO_FOLLOWUP_ID"
                    drpDownList.DataBind()
                End If
            Case "WorkOrderSeverity"
                Dim objWOSeverities As New AAMS.bizHelpDesk.bzWOSeverities
                objOutputXml = New XmlDocument

                objOutputXml = objWOSeverities.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("WO_SEVERITIES")
                    drpDownList.DataTextField = "WO_SEVERITY_NAME"
                    drpDownList.DataValueField = "WO_SEVERITY_ID"
                    drpDownList.DataBind()
                End If
            Case "WorkOrderType"
                Dim objWOType As New AAMS.bizHelpDesk.bzWOType
                objOutputXml = New XmlDocument

                objOutputXml = objWOType.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("WO_TYPE")
                    drpDownList.DataTextField = "WO_TYPE_NAME"
                    drpDownList.DataValueField = "WO_TYPE_ID"
                    drpDownList.DataBind()
                End If
            Case "QuerySubGroup"
                Dim objCallSubGroup As New AAMS.bizHelpDesk.bzCallSubGroup
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT>")
                objOutputXml = objCallSubGroup.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_SUB_GROUP")
                    drpDownList.DataTextField = "CALL_SUB_GROUP_NAME"
                    drpDownList.DataValueField = "CALL_SUB_GROUP_ID"
                    drpDownList.DataBind()
                End If
                ' Added by Rakesh
            Case "TSTATUS"
                Dim objInXml As New XmlDocument

                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>2</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
                Dim objbzCallStatus As New AAMS.bizHelpDesk.bzCallStatus
                objOutputXml = New XmlDocument
                objOutputXml = objbzCallStatus.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_STATUS")
                    drpDownList.DataTextField = "HD_STATUS_NAME"
                    drpDownList.DataValueField = "HD_STATUS_ID"
                    drpDownList.DataBind()
                End If
            Case "TSTATUSWITHCLOSED"
                Dim objCallStatus As New AAMS.bizHelpDesk.bzCallStatus
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                Dim objNodeList As XmlNodeList
                objInXml.LoadXml("<HD_LISTCALLSTATUS_INPUT><HD_QUERY_GROUP_ID>2</HD_QUERY_GROUP_ID></HD_LISTCALLSTATUS_INPUT>")
                objOutputXml = objCallStatus.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objNodeList = objOutputXml.DocumentElement.SelectNodes("CALL_STATUS")
                    For Each objNode As XmlNode In objNodeList
                        Dim li As New ListItem(objNode.Attributes("HD_STATUS_NAME").Value, objNode.Attributes("HD_STATUS_ID").Value & "|" & objNode.Attributes("HD_STATUS_CLOSE").Value)
                        drpDownList.Items.Add(li)
                    Next
                    'objXmlReader = New XmlNodeReader(objOutputXml)
                    'ds.ReadXml(objXmlReader)
                    'drpDownList.DataSource = ds.Tables("CALL_STATUS")
                    'drpDownList.DataTextField = "HD_STATUS_NAME"
                    'drpDownList.DataValueField = "HD_STATUS_ID" & "|" & "HD_STATUS_CLOSE"
                    'drpDownList.DataBind()
                End If
            Case "TPRIORITY"
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>2</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
                Dim objbzCallSeverity As New AAMS.bizHelpDesk.bzSeverities
                objOutputXml = New XmlDocument
                objOutputXml = objbzCallSeverity.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("SEVERITIES")
                    drpDownList.DataTextField = "HD_SEVERITY_NAME"
                    drpDownList.DataValueField = "HD_SEVERITY_ID"
                    drpDownList.DataBind()
                End If

                'Case "TCATEGORYNAME"
                '    Dim objInXml As New XmlDocument
                '    objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>2</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
                '    Dim objbzCategory As New AAMS.bizHelpDesk.bzCallCategory
                '    objOutputXml = New XmlDocument
                '    objOutputXml = objbzCategory.List1(objInXml)
                '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                '        objXmlReader = New XmlNodeReader(objOutputXml)
                '        ds.ReadXml(objXmlReader)
                '        drpDownList.DataSource = ds.Tables("DETAILS")
                '        drpDownList.DataTextField = "CALL_CATEGORY_NAME"
                '        drpDownList.DataValueField = "CALL_CATEGORY_ID"
                '        drpDownList.DataBind()
                '    End If
                ' For populating Sub group combo for technical.
                ' Edit by Pankaj
            Case "TQUERYSUBGROUP"
                Dim objCallSubGroup As New AAMS.bizHelpDesk.bzCallSubGroup
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>2</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT>")
                objOutputXml = objCallSubGroup.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_SUB_GROUP")
                    drpDownList.DataTextField = "CALL_SUB_GROUP_NAME"
                    drpDownList.DataValueField = "CALL_SUB_GROUP_ID"
                    drpDownList.DataBind()
                End If
            Case "AOFFICE"
                Dim objbzAOffice As New AAMS.bizMaster.bzAOffice
                objOutputXml = New XmlDocument
                objOutputXml = objbzAOffice.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("AOFFICE")
                    drpDownList.DataTextField = "AOFFICE"
                    drpDownList.DataValueField = "AOFFICE"
                    drpDownList.DataBind()
                End If
            Case "BRAOFFICE"
                Dim objbzAOffice As New AAMS.bizBRHelpDesk.bzMasterList
                objOutputXml = New XmlDocument
                objOutputXml = objbzAOffice.ListAoffice
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("AOFFICE")
                    drpDownList.DataTextField = "AOFFICE"
                    drpDownList.DataValueField = "AOFFICE"
                    drpDownList.DataBind()
                End If
            Case "AIRLINE"
                Dim objbzAOffice As New AAMS.bizMaster.bzAirline
                objOutputXml = New XmlDocument
                objOutputXml = objbzAOffice.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("AIRLINE")
                    drpDownList.DataTextField = "NAME"
                    drpDownList.DataValueField = "Airline_Code"
                    drpDownList.DataBind()
                End If
            Case "AGROUP"
                Dim objbzAgencyGroup As New AAMS.bizMaster.bzAgencyGroup
                objOutputXml = New XmlDocument
                objOutputXml = objbzAgencyGroup.ListGroupType()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("GROUP_TYPE")
                    drpDownList.DataTextField = "GroupTypeName"
                    drpDownList.DataValueField = "GroupTypeID"
                    drpDownList.DataBind()
                End If
            Case "AGENCYTYPE"
                Dim objbzAgencyType As New AAMS.bizTravelAgency.bzAgencyType
                objOutputXml = New XmlDocument
                objOutputXml = objbzAgencyType.List
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("AGENCYTYPE")
                    drpDownList.DataTextField = "Agency_Type_Name"
                    drpDownList.DataValueField = "AgencyTypeId"
                    drpDownList.DataBind()
                End If
            Case "AGENCYSTATUS"
                Dim objbzAgencyStatus As New AAMS.bizTravelAgency.bzAgencyStatus
                objOutputXml = New XmlDocument
                objOutputXml = objbzAgencyStatus.List
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("AGENCYSTATUS")
                    drpDownList.DataTextField = "Agency_Status_Name"
                    drpDownList.DataValueField = "AgencyStatusId"
                    drpDownList.DataBind()
                End If
            Case "CITY"
                Dim objbzCity As New AAMS.bizMaster.bzCity
                objOutputXml = New XmlDocument
                objOutputXml = objbzCity.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CITY")
                    drpDownList.DataTextField = "City_Name"
                    drpDownList.DataValueField = "CityID"
                    drpDownList.DataBind()
                End If
            Case "BRCITY"
                Dim objbzCity As New AAMS.bizBRHelpDesk.bzMasterList
                objOutputXml = New XmlDocument
                objOutputXml = objbzCity.ListCity
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CITY")
                    drpDownList.DataTextField = "City_Name"
                    drpDownList.DataValueField = "CityID"
                    drpDownList.DataBind()
                End If

            Case "COUNTRY"
                Dim objbzCountry As New AAMS.bizMaster.bzCountry
                objOutputXml = New XmlDocument
                objOutputXml = objbzCountry.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("COUNTRY")
                    drpDownList.DataTextField = "Country_Name"
                    drpDownList.DataValueField = "CountryID"
                    drpDownList.DataBind()
                End If
            Case "BRCOUNTRY"
                Dim objbzCountry As New AAMS.bizBRHelpDesk.bzMasterList
                objOutputXml = New XmlDocument
                objOutputXml = objbzCountry.ListCountry
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("COUNTRY")
                    drpDownList.DataTextField = "Country_Name"
                    drpDownList.DataValueField = "CountryID"
                    drpDownList.DataBind()
                End If
            Case "CRS"
                Dim objbzCRS As New AAMS.bizMaster.bzCRS
                objOutputXml = New XmlDocument
                objOutputXml = objbzCRS.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CRS")
                    drpDownList.DataTextField = "Name"
                    drpDownList.DataValueField = "CRSCodeText"
                    drpDownList.DataBind()
                End If
            Case "CRSCODEWITHTEXT"
                Dim objbzCRS As New AAMS.bizMaster.bzCRS
                objOutputXml = New XmlDocument
                objOutputXml = objbzCRS.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CRS")
                    drpDownList.DataTextField = "CRSCodeText"
                    drpDownList.DataValueField = "CRSCode"
                    drpDownList.DataBind()
                End If
            Case "CRSTEXT"
                Dim objbzCRS As New AAMS.bizMaster.bzCRS
                objOutputXml = New XmlDocument
                objOutputXml = objbzCRS.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CRS")
                    drpDownList.DataTextField = "CRSCodeText"
                    drpDownList.DataValueField = "CRSCodeText"
                    drpDownList.DataBind()
                End If
            Case "DESIGNATION"
                Dim objbzDesignation As New AAMS.bizMaster.bzDesignation
                objOutputXml = New XmlDocument
                objOutputXml = objbzDesignation.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("DESIGNATION")
                    drpDownList.DataTextField = "Designation"
                    drpDownList.DataValueField = "DesignationID"
                    drpDownList.DataBind()
                End If

            Case "TRAININGSTATUS"
                Dim objbzTraiingStatus As New AAMS.bizTraining.bzMailStatus
                objOutputXml = New XmlDocument
                objOutputXml = objbzTraiingStatus.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)

                    drpDownList.DataSource = ds.Tables("MAILSTATUS")
                    drpDownList.DataTextField = "TR_STATUS_NAME"
                    drpDownList.DataValueField = "TR_STATUS_ID"
                    drpDownList.DataBind()
                End If


            Case "PRODUCTS"
                Dim objTaProducts As New AAMS.bizTravelAgency.bzProduct
                objOutputXml = New XmlDocument
                objOutputXml = objTaProducts.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PRODUCT")
                    drpDownList.DataTextField = "PRODUCTNAME"
                    drpDownList.DataValueField = "PRODUCTID"
                    drpDownList.DataBind()
                End If
            Case "ONLINESTATUS"
                Dim objTaProducts As New AAMS.bizTravelAgency.bzOnlineStatus
                objOutputXml = New XmlDocument
                objOutputXml = objTaProducts.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("Status")
                    drpDownList.DataTextField = "OnlineStatus"
                    drpDownList.DataValueField = "StatusCode"
                    drpDownList.DataBind()
                End If

            Case "ONLINESTATUSCODE"
                Dim objTaProducts As New AAMS.bizTravelAgency.bzOnlineStatus
                objOutputXml = New XmlDocument
                objOutputXml = objTaProducts.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("Status")
                    drpDownList.DataTextField = "StatusCode"
                    drpDownList.DataValueField = "StatusCode"
                    drpDownList.DataBind()
                End If


            Case "EMPLOYEE"
                Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
                Dim objInputXml As New XmlDocument
                objOutputXml = objbzEmployee.List
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("EMPLOYEE")
                    drpDownList.DataTextField = "Employee_Name"
                    drpDownList.DataValueField = "EmployeeID"
                    drpDownList.DataBind()
                End If
            Case "ASSIGNEDTOEMPLOYEE"
                Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
                Dim objInputXml As New XmlDocument
                objOutputXml = objbzEmployee.ListAssignedToEmployee
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("EMPLOYEE")
                    drpDownList.DataTextField = "Employee_Name"
                    drpDownList.DataValueField = "EmployeeID"
                    drpDownList.DataBind()
                End If
                'Case "REGION1"
                '    Dim objbzRegion As New AAMS.bizMaster.bzSecurityRegion
                '    objOutputXml = New XmlDocument

                '    objOutputXml = objbzRegion.List()
                '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                '        objXmlReader = New XmlNodeReader(objOutputXml)
                '        ds.ReadXml(objXmlReader)
                '        drpDownList.DataSource = ds.Tables("SECURITYREGION")
                '        drpDownList.DataTextField = "Name"
                '        drpDownList.DataValueField = "RegionID"
                '        drpDownList.DataBind()
                '    End If

            Case "REGION1"
                Dim objbzRegion As New AAMS.bizMaster.bzRegion
                objOutputXml = New XmlDocument
                objOutputXml = objbzRegion.RegionList()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("REGION")

                    drpDownList.DataTextField = "NAME"

                    drpDownList.DataValueField = "REGIONID"

                    drpDownList.DataBind()

                End If

            Case "BRREGION"
                Dim objbzRegion As New AAMS.bizMaster.bzRegion
                objOutputXml = New XmlDocument

                objOutputXml = objbzRegion.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("REGION_DET")
                    drpDownList.DataTextField = "Region_Name"
                    drpDownList.DataValueField = "Region_Name"
                    drpDownList.DataBind()
                End If

            Case "IPPOOL"
                Dim objbzIPPool As New AAMS.bizMaster.bzIPPool
                Dim objInputXml As New XmlDocument
                objOutputXml = New XmlDocument
                objInputXml.LoadXml("<MS_SEARCHIPPOOL_INPUT><PoolName></PoolName><Aoffice></Aoffice><Department_Name></Department_Name><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHIPPOOL_INPUT>")
                objOutputXml = objbzIPPool.Search(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("IPPOOL")
                    drpDownList.DataTextField = "PoolName"
                    drpDownList.DataValueField = "PoolID"
                    drpDownList.DataBind()
                End If
            Case "ORDERSTATUS"
                Dim objbzOrderStatus As New AAMS.bizTravelAgency.bzOrderStatus
                objOutputXml = New XmlDocument
                objOutputXml = objbzOrderStatus.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("ORDER_STATUS")
                    drpDownList.DataTextField = "ORDER_STATUS_NAME"
                    drpDownList.DataValueField = "ORDERSTATUSID"
                    drpDownList.DataBind()
                End If
            Case "ORDERSTATUSTRAINING"
                Dim objbzOrderStatus As New AAMS.bizTravelAgency.bzOrderStatus
                objOutputXml = New XmlDocument
                objOutputXml = objbzOrderStatus.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("ORDER_STATUS")
                    drpDownList.DataTextField = "ORDER_STATUS_NAME"
                    drpDownList.DataValueField = "ORDERSTATUSID"
                    drpDownList.DataBind()
                End If

            Case "ORDERSTATUSTRAINING_APPROVED"
                Dim objbzOrderStatus As New AAMS.bizTravelAgency.bzOrderStatus
                ' Dim foundRows() As DataRow
                Dim dv As DataView
                objOutputXml = New XmlDocument
                objOutputXml = objbzOrderStatus.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    dv = ds.Tables("ORDER_STATUS").DefaultView
                    dv.RowFilter = "ORDER_STATUS_NAME='Approved'"
                    drpDownList.DataSource = dv.Table()
                    drpDownList.DataTextField = "ORDER_STATUS_NAME"
                    drpDownList.DataValueField = "ORDERSTATUSID"
                    drpDownList.DataBind()
                End If

            Case "PRIORITY"
                Dim objbzAgencyGroup As New AAMS.bizMaster.bzAgencyGroup
                objOutputXml = New XmlDocument
                objOutputXml = objbzAgencyGroup.ListPriority()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PRIORITY")
                    drpDownList.DataTextField = "PriorityName"
                    drpDownList.DataValueField = "PriorityID"
                    drpDownList.DataBind()
                End If
            Case "REGION"
                Dim objbzRegion As New AAMS.bizMaster.bzRegion
                objOutputXml = New XmlDocument

                objOutputXml = objbzRegion.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("REGION_DET")
                    drpDownList.DataTextField = "Region_Name"
                    drpDownList.DataValueField = "Region_Name"
                    drpDownList.DataBind()
                End If
            Case "REGIONHQ"
                Dim objbzAOffice As New AAMS.bizMaster.bzAOffice
                objOutputXml = New XmlDocument
                objOutputXml = objbzAOffice.ListHQ()

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("Detail")
                    drpDownList.DataTextField = "Name"
                    drpDownList.DataValueField = "ID"
                    drpDownList.DataBind()
                End If
            Case "STATE"
                Dim objbzCountryState As New AAMS.bizMaster.bzCountryState
                objOutputXml = New XmlDocument
                objOutputXml = objbzCountryState.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("STATE")
                    drpDownList.DataTextField = "StateName"
                    drpDownList.DataValueField = "StateID"
                    drpDownList.DataBind()
                End If
            Case "SECURITYREGION"
                Dim objbzSecurityRegion As New AAMS.bizMaster.bzSecurityRegion
                objOutputXml = New XmlDocument
                objOutputXml = objbzSecurityRegion.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("SECURITYREGION")
                    drpDownList.DataTextField = "Name"
                    drpDownList.DataValueField = "RegionID"
                    drpDownList.DataBind()
                End If
            Case "SECURITYREGIONWITHAOFFICE"
                Dim objbzSecurityRegion As New AAMS.bizMaster.bzSecurityRegion
                objOutputXml = New XmlDocument
                objOutputXml = objbzSecurityRegion.List1()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("SECURITYREGION")
                    drpDownList.DataTextField = "Name"
                    drpDownList.DataValueField = "RegionID"
                    drpDownList.DataBind()
                End If


            Case "ManagerName"
                Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
                objOutputXml = New XmlDocument
                objOutputXml = objbzEmployee.ListManager()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("EMPLOYEE")
                    drpDownList.DataTextField = "Employee_Name"
                    drpDownList.DataValueField = "EmployeeID"
                    drpDownList.DataBind()
                End If
            Case "DepartmentName"
                Dim objbzDepartment As New AAMS.bizMaster.bzDepartment
                objOutputXml = New XmlDocument
                objOutputXml = objbzDepartment.List

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("DEPARTMENT")
                    drpDownList.DataTextField = "Department_Name"
                    drpDownList.DataValueField = "DepartmentID"
                    drpDownList.DataBind()
                End If
            Case "DESIGNATION"
                Dim objbzDesignation As New AAMS.bizMaster.bzDesignation
                objOutputXml = New XmlDocument
                objOutputXml = objbzDesignation.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("DESIGNATION")
                    drpDownList.DataTextField = "Designation"
                    drpDownList.DataValueField = "DesignationID"
                    drpDownList.DataBind()
                End If
            Case "PRODUCTGROUP"
                Dim objPRODUCTGROUP As New AAMS.bizTravelAgency.bzProductGroup
                objOutputXml = New XmlDocument
                objOutputXml = objPRODUCTGROUP.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PRODUCTGROUP")
                    drpDownList.DataTextField = "ProductGroupName"
                    drpDownList.DataValueField = "ProductGroupId"
                    drpDownList.DataBind()
                End If
            Case "PROVIDERCRS"
                Dim objbzCRS As New AAMS.bizMaster.bzCRS
                objOutputXml = New XmlDocument
                objOutputXml = objbzCRS.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CRS")
                    drpDownList.DataTextField = "Name"
                    drpDownList.DataValueField = "CRSCode"
                    drpDownList.DataBind()
                End If
            Case "PROVIDERCRS1"
                Dim objbzCRS As New AAMS.bizMaster.bzCRS
                objOutputXml = New XmlDocument
                objOutputXml = objbzCRS.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CRS")
                    drpDownList.DataTextField = "CRSCodeText"
                    drpDownList.DataValueField = "CRSCodeText"
                    drpDownList.DataBind()
                End If
            Case "OS"
                Dim objbzOS As New AAMS.bizTravelAgency.bzOS
                objOutputXml = New XmlDocument
                objOutputXml = objbzOS.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("OS")
                    drpDownList.DataTextField = "OSName"
                    drpDownList.DataValueField = "OSID"
                    drpDownList.DataBind()
                End If
            Case "OrderTypeCategory"
                Dim objbzCategory As New AAMS.bizTravelAgency.bzOrderCategory
                objOutputXml = New XmlDocument
                objOutputXml = objbzCategory.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("Order")
                    drpDownList.DataTextField = "OrderTypeCategoryName"
                    drpDownList.DataValueField = "OrderTypeCategoryID"
                    drpDownList.DataBind()
                End If
            Case "NewConnectivity"
                Dim objbzOnlineStatus As New AAMS.bizTravelAgency.bzOnlineStatus
                objOutputXml = New XmlDocument
                Dim objNodeList As XmlNodeList
                objOutputXml = objbzOnlineStatus.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objNodeList = objOutputXml.SelectNodes("MS_LISTONLINESTATUS_OUTPUT/Status")
                    For Each objXmlNode As XmlNode In objNodeList
                        drpDownList.Items.Add(New ListItem(objXmlNode.Attributes("OnlineStatus").InnerText, objXmlNode.Attributes("StatusCode").InnerText))
                    Next
                End If

            Case "OldConnectivity"
                Dim objbzOnlineStatus As New AAMS.bizTravelAgency.bzOnlineStatus
                objOutputXml = New XmlDocument
                Dim objNodeList As XmlNodeList
                objOutputXml = objbzOnlineStatus.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objNodeList = objOutputXml.SelectNodes("MS_LISTONLINESTATUS_OUTPUT/Status")
                    For Each objXmlNode As XmlNode In objNodeList
                        drpDownList.Items.Add(New ListItem(objXmlNode.Attributes("OnlineStatus").InnerText, objXmlNode.Attributes("StatusCode").InnerText))
                    Next
                End If
            Case "CorporateCode"
                Dim objbzCorporateCodes As New AAMS.bizTravelAgency.bzCorporateCodes
                objOutputXml = New XmlDocument
                Dim objNodeList As XmlNodeList
                objOutputXml = objbzCorporateCodes.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objNodeList = objOutputXml.SelectNodes("MS_LISTOFFICEIDQUALIFIERS_OUTPUT/QUALIFIERS")
                    For Each objXmlNode As XmlNode In objNodeList
                        drpDownList.Items.Add(New ListItem(objXmlNode.Attributes("Code").InnerText, objXmlNode.Attributes("RowID").InnerText & "|" & objXmlNode.Attributes("Qualifier").InnerText & "|" & objXmlNode.Attributes("Description").InnerText))
                    Next
                End If
            Case "FileNo"
                Dim objbzTravelAgency As New AAMS.bizTravelAgency.bzAgency
                objOutputXml = New XmlDocument
                objOutputXml = objbzTravelAgency.GetFileNumber()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("FILENUMBER")
                    drpDownList.DataTextField = "FileNo"
                    drpDownList.DataValueField = "FileNo"
                    drpDownList.DataBind()
                End If
            Case "PERMISSIONS"
                Dim objbzSecurityRegion As New AAMS.bizMaster.bzPermission
                objOutputXml = New XmlDocument
                objOutputXml = objbzSecurityRegion.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    ds.Tables("SECURITY_OPTION").DefaultView.Sort = "SecurityOptionSubName"
                    drpDownList.DataSource = ds.Tables("SECURITY_OPTION").DefaultView
                    drpDownList.DataTextField = "SecurityOptionSubName"
                    drpDownList.DataValueField = "securityOptionID"
                    drpDownList.DataBind()
                End If
            Case "AgencyGroup"
                Dim objbzAgencyGroup As New AAMS.bizMaster.bzAgencyGroup
                objOutputXml = New XmlDocument
                objOutputXml = objbzAgencyGroup.ListGroupType()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("AGENCYGROUP")
                    drpDownList.DataTextField = "AGENCYGROUPNAME"
                    drpDownList.DataValueField = "CHAIN_CODE"
                    drpDownList.DataBind()
                End If
            Case "CORPORATECODE"
                Dim objCorporateCode As New AAMS.bizTravelAgency.bzCorporateCodes
                objOutputXml = New XmlDocument
                objOutputXml = objCorporateCode.ListCorporateCode()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CORPORATECODE").DefaultView
                    drpDownList.DataTextField = "CCode"
                    drpDownList.DataValueField = "CCode"
                    drpDownList.DataBind()
                End If
            Case "OFFICETYPE"
                Dim objOfficeType As New AAMS.bizTravelAgency.bzOfficeID
                objOutputXml = New XmlDocument
                objOutputXml = objOfficeType.ListOfficeType()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("OFFICETYPE")
                    drpDownList.DataTextField = "OFFICEID_TYPE_NAME"
                    drpDownList.DataValueField = "OFFICEID_TYPE_ID"
                    drpDownList.DataBind()
                End If


            Case "ISPLIST"
                Dim objIspDrp As New AAMS.bizISP.bzISP
                objOutputXml = New XmlDocument
                objOutputXml = objIspDrp.List() '.ListOfficeType()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("ISP")
                    drpDownList.DataTextField = "Name"
                    drpDownList.DataValueField = "ISPID"
                    drpDownList.DataBind()
                End If
            Case "CITYOFFICEIDGENERATION"
                Dim objbzCity As New AAMS.bizMaster.bzCity
                objOutputXml = New XmlDocument
                Dim objNodeList As XmlNodeList
                objOutputXml = objbzCity.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objNodeList = objOutputXml.DocumentElement.SelectNodes("CITY")
                    For Each objNode As XmlNode In objNodeList
                        Dim li As New ListItem(objNode.Attributes("City_Name").Value, objNode.Attributes("CityID").Value & "|" & objNode.Attributes("CityCode").Value)
                        drpDownList.Items.Add(li)
                    Next
                    '    objXmlReader = New XmlNodeReader(objOutputXml)
                    '    ds.ReadXml(objXmlReader)
                    '    drpDownList.DataSource = ds.Tables("CITY")
                    '    drpDownList.DataTextField = "City_Name"
                    '    drpDownList.DataValueField = "CityCode"
                    '    drpDownList.DataBind()
                End If

            Case "ISPOrderStatusList"
                Dim objISPOrderStatus As New AAMS.bizISP.bzISPOrderStatus
                objOutputXml = New XmlDocument

                objOutputXml = objISPOrderStatus.List() '.ListOfficeType()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("OrderStatus")
                    drpDownList.DataTextField = "StatusName"
                    drpDownList.DataValueField = "OrderStatusID"
                    drpDownList.DataBind()
                End If
            Case "ISPPLANLIST"
                Dim objbzISPPlan As New AAMS.bizISP.bzISPPlan
                objOutputXml = New XmlDocument
                objOutputXml = objbzISPPlan.List() '.ListOfficeType()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("ISPPLAN")
                    drpDownList.DataTextField = "NPID"
                    drpDownList.DataValueField = "ISPPlanID"
                    drpDownList.DataBind()
                End If
            Case "FeasibilityStatus"
                Dim objISPFeasibilityStatus As New AAMS.bizISP.bzISPFeasibleStatus
                objOutputXml = New XmlDocument

                objOutputXml = objISPFeasibilityStatus.List() '.ListOfficeType()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("FEASIBILE_STATUS")
                    drpDownList.DataTextField = "Name"
                    drpDownList.DataValueField = "FeasibleStatusID"
                    drpDownList.DataBind()
                End If
                'Code Added by Mukund On 28th Feb 2008 
            Case "HDFOLLOWUP"
                Dim objHdFollowUp As New AAMS.bizHelpDesk.bzPTRFollowUp
                objOutputXml = New XmlDocument
                objOutputXml = objHdFollowUp.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_FOLLOWUP")
                    drpDownList.DataTextField = "HD_PTR_FOLLOWUP_NAME"
                    drpDownList.DataValueField = "HD_PTR_FOLLOWUP_ID"
                    drpDownList.DataBind()
                End If
                'Code Added by Mukund On 28th Feb 2008 
            Case "HDPTRTYPECAT"
                Dim objHdPtrTypeCat As New AAMS.bizHelpDesk.bzPTRTypeCategory
                objOutputXml = New XmlDocument
                objOutputXml = objHdPtrTypeCat.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_CATEGORIES")
                    drpDownList.DataTextField = "HD_PTR_TYCAT_NAME"
                    drpDownList.DataValueField = "HD_PTR_TYCAT_ID"
                    drpDownList.DataBind()
                End If
                'Code Added by Mukund On 28th Feb 2008 
                'Start of Code Added by Abhishek On 30th Sep 2010 
            Case "HDIRFOLLOWUP"
                Dim objHdFollowUp As New AAMS.bizHelpDesk.bzIRFollowUp
                objOutputXml = New XmlDocument
                objOutputXml = objHdFollowUp.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("IR_FOLLOWUP")
                    drpDownList.DataTextField = "HD_IR_FOLLOWUP_NAME"
                    drpDownList.DataValueField = "HD_IR_FOLLOWUP_ID"
                    drpDownList.DataBind()
                End If
            Case "HDIRSTATUS"
                Dim objPTRStatus As New AAMS.bizHelpDesk.bzIRStatus
                objOutputXml = New XmlDocument
                objOutputXml = objPTRStatus.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("IR_STATUS")
                    drpDownList.DataTextField = "HD_STATUS_NAME"
                    drpDownList.DataValueField = "HD_STATUS_ID"
                    drpDownList.DataBind()
                End If
            Case "HDIRSTATUSFORMANAGE"
                Dim objIRStatus As New AAMS.bizHelpDesk.bzIRStatus
                objOutputXml = New XmlDocument
                objOutputXml = objIRStatus.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    Dim Node As XmlNode
                    Dim nodeLists As XmlNodeList
                    Dim outerXmlSet As New XmlDocument
                    ' Dim strOuteXml As String = objOutputXml.ReplaceChild(.InnerXml
                    nodeLists = objOutputXml.DocumentElement.SelectNodes("IR_STATUS")
                    For Each Node In nodeLists
                        Dim NodeOld As XmlNode
                        NodeOld = Node
                        Node.Attributes("HD_STATUS_ID").Value = Node.Attributes("HD_STATUS_ID").Value.Trim() + "|" + Node.Attributes("HD_STATUS_CLOSE").Value.Trim()
                        objOutputXml.DocumentElement.ReplaceChild(Node, NodeOld)
                    Next

                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("IR_STATUS")
                    drpDownList.DataTextField = "HD_STATUS_NAME"
                    drpDownList.DataValueField = "HD_STATUS_ID"
                    drpDownList.DataBind()
                End If
            Case "HDIRTYPECAT"
                Dim objHdPtrTypeCat As New AAMS.bizHelpDesk.bzIRTypeCategory
                objOutputXml = New XmlDocument
                objOutputXml = objHdPtrTypeCat.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("IR_CATEGORIES")
                    drpDownList.DataTextField = "HD_IR_TYCAT_NAME"
                    drpDownList.DataValueField = "HD_IR_TYCAT_ID"
                    drpDownList.DataBind()
                End If

            Case "HDIRASSIGNEDTO"
                Dim objHdPtrAssignree As New AAMS.bizHelpDesk.bzIRAssignee
                objOutputXml = New XmlDocument
                objOutputXml = objHdPtrAssignree.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("IR_ASSIGNEE")
                    drpDownList.DataTextField = "ASSIGNEE_NAME"
                    drpDownList.DataValueField = "ASSIGNEEID"
                    drpDownList.DataBind()
                End If
            Case "HDIRTYPE"
                Dim objHdPtrType As New AAMS.bizHelpDesk.bzIRType
                objOutputXml = New XmlDocument
                objOutputXml = objHdPtrType.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("IR_TYPE")
                    drpDownList.DataTextField = "HD_IR_TYPE_NAME"
                    drpDownList.DataValueField = "HD_IR_TYPE_ID"
                    drpDownList.DataBind()
                End If
            Case "HDIRSEVERITY"
                Dim objHdSeverity As New AAMS.bizHelpDesk.bzIRSeverity
                objOutputXml = New XmlDocument
                objOutputXml = objHdSeverity.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("IR_SEVERITY")
                    drpDownList.DataTextField = "HD_IR_SEV_NAME"
                    drpDownList.DataValueField = "HD_IR_SEV_ID"
                    drpDownList.DataBind()
                End If

                'End of Code Added by Abhishek On 30th Sep 2010 

            Case "HDPTRTYPE"
                Dim objHdPtrType As New AAMS.bizHelpDesk.bzPTRType
                objOutputXml = New XmlDocument
                objOutputXml = objHdPtrType.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_TYPE")
                    drpDownList.DataTextField = "HD_PTR_TYPE_NAME"
                    drpDownList.DataValueField = "HD_PTR_TYPE_ID"
                    drpDownList.DataBind()
                End If
                'Code Added by Mukund On 28th Feb 2008 
            Case "HDPTRSEVERITY"
                Dim objHdSeverity As New AAMS.bizHelpDesk.bzPTRSeverity
                objOutputXml = New XmlDocument
                objOutputXml = objHdSeverity.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_SEVERITY")
                    drpDownList.DataTextField = "HD_PTR_SEV_NAME"
                    drpDownList.DataValueField = "HD_PTR_SEV_ID"
                    drpDownList.DataBind()
                End If
                ' Code Added by Mukund On 28th Feb 2008 
            Case "HDPTRCALLSTATUS"
                Dim objHdStatis As New AAMS.bizHelpDesk.bzCallStatus
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT>")
                objOutputXml = objHdStatis.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_STATUS")
                    drpDownList.DataTextField = "HD_STATUS_NAME"
                    drpDownList.DataValueField = "HD_STATUS_ID"
                    drpDownList.DataBind()
                End If

            Case "HDPTRSTATUS"
                Dim objPTRStatus As New AAMS.bizHelpDesk.bzPTRStatus
                objOutputXml = New XmlDocument
                objOutputXml = objPTRStatus.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_STATUS")
                    drpDownList.DataTextField = "HD_STATUS_NAME"
                    drpDownList.DataValueField = "HD_STATUS_ID"
                    drpDownList.DataBind()
                End If


                'Code Added by Tapan On 7th July 2010 Not for General Use 
            Case "HDPTRSTATUSFORMANAGE"
                Dim objPTRStatus As New AAMS.bizHelpDesk.bzPTRStatus
                objOutputXml = New XmlDocument
                objOutputXml = objPTRStatus.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    Dim Node As XmlNode
                    Dim nodeLists As XmlNodeList
                    Dim outerXmlSet As New XmlDocument
                    ' Dim strOuteXml As String = objOutputXml.ReplaceChild(.InnerXml
                    nodeLists = objOutputXml.DocumentElement.SelectNodes("PTR_STATUS")
                    For Each Node In nodeLists
                        Dim NodeOld As XmlNode
                        NodeOld = Node
                        Node.Attributes("HD_STATUS_ID").Value = Node.Attributes("HD_STATUS_ID").Value.Trim() + "|" + Node.Attributes("HD_STATUS_CLOSE").Value.Trim()
                        objOutputXml.DocumentElement.ReplaceChild(Node, NodeOld)
                    Next

                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_STATUS")
                    drpDownList.DataTextField = "HD_STATUS_NAME"
                    drpDownList.DataValueField = "HD_STATUS_ID"
                    drpDownList.DataBind()
                End If
                'Code Added by Mukund On 7th March 2008 Not for General Use 
            Case "HDPTRCALLSTATUS_NOTUSE"
                ' Case "HDPTRCALLSTATUS"
                Dim objHdStatis As New AAMS.bizHelpDesk.bzCallStatus
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT>")
                objOutputXml = objHdStatis.List1(objInXml)
                Dim Node As XmlNode
                Dim nodeLists As XmlNodeList
                Dim outerXmlSet As New XmlDocument
                ' Dim strOuteXml As String = objOutputXml.ReplaceChild(.InnerXml
                nodeLists = objOutputXml.DocumentElement.SelectNodes("CALL_STATUS")
                For Each Node In nodeLists
                    Dim NodeOld As XmlNode
                    NodeOld = Node
                    Node.Attributes("HD_STATUS_ID").Value = Node.Attributes("HD_STATUS_ID").Value.Trim() + "|" + Node.Attributes("HD_STATUS_CLOSE").Value.Trim()
                    objOutputXml.DocumentElement.ReplaceChild(Node, NodeOld)
                Next
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_STATUS")
                    drpDownList.DataTextField = "HD_STATUS_NAME"
                    drpDownList.DataValueField = "HD_STATUS_ID"
                    drpDownList.DataBind()
                End If



                'Code Added by Mukund On 29th Feb 2008 
            Case "HDASSIGNEDTO"
                Dim objHdPtrAssignree As New AAMS.bizHelpDesk.bzPTRAssignee
                objOutputXml = New XmlDocument
                objOutputXml = objHdPtrAssignree.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_ASSIGNEE")
                    drpDownList.DataTextField = "ASSIGNEE_NAME"
                    drpDownList.DataValueField = "ASSIGNEEID"
                    drpDownList.DataBind()
                End If

                'Code Added by Mukund On 13th March 2008 

            Case "HDQUESTIONSTATUS"
                Dim objFeedback As New AAMS.bizHelpDesk.bzFeedback
                objOutputXml = New XmlDocument
                objOutputXml = objFeedback.ListFeedbackStatus()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("STATUS")
                    drpDownList.DataTextField = "STATUS_NAME"
                    drpDownList.DataValueField = "STATUS_ID"
                    drpDownList.DataBind()
                End If



            Case "QueryCategory"
                Dim objCallCategory As New AAMS.bizHelpDesk.bzCallCategory
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
                objOutputXml = objCallCategory.List1(objInXml)
                ' objOutputXml = objCallCategory.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_CATEGORY") '
                    drpDownList.DataTextField = "CALL_CATEGORY_NAME"
                    drpDownList.DataValueField = "CALL_CATEGORY_ID"
                    drpDownList.DataBind()
                End If
            Case "ListFuntionalCategory"
                Dim objbzCallCategory As New AAMS.bizHelpDesk.bzCallCategory
                objOutputXml = New XmlDocument
                objOutputXml = objbzCallCategory.ListFuntionalCategory
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_CATEGORY")
                    drpDownList.DataTextField = "CALL_CATEGORY_NAME"
                    drpDownList.DataValueField = "CALL_CATEGORY_ID"
                    drpDownList.DataBind()
                End If
            Case "SUPPLIER"
                Dim objbzSupplier As New AAMS.bizInventory.bzSupplier
                objOutputXml = New XmlDocument
                objOutputXml = objbzSupplier.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("SUPPLIER")
                    drpDownList.DataTextField = "SUPPLIERNAME"
                    drpDownList.DataValueField = "SUPPLIERID"
                    drpDownList.DataBind()
                End If
            Case "EQUIPMENTGROUP"
                Dim objbzEquipment As New AAMS.bizMaster.bzEquipmentGroup
                objOutputXml = New XmlDocument
                objOutputXml = objbzEquipment.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("EQUIPMENTGROUP")
                    drpDownList.DataTextField = "GROUP_CODE"
                    drpDownList.DataValueField = "GROUP_CODE"
                    drpDownList.DataBind()
                End If

            Case "GODOWN"
                Dim objbzGodown As New AAMS.bizInventory.bzGodown
                objOutputXml = New XmlDocument
                objOutputXml = objbzGodown.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("GODOWN")
                    drpDownList.DataTextField = "GODOWNNAME"
                    drpDownList.DataValueField = "GODOWNID"
                    drpDownList.DataBind()
                End If
            Case "EQUIPMENT"
                Dim objbzEquipment As New AAMS.bizMaster.bzEquipment
                objOutputXml = New XmlDocument
                objOutputXml = objbzEquipment.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("EQUIPMENT")
                    drpDownList.DataTextField = "DESCRIPTION"
                    drpDownList.DataValueField = "PRODUCTID"
                    drpDownList.DataBind()
                End If
            Case "EQUIPMENTCODE"
                Dim objbzEquipment As New AAMS.bizMaster.bzEquipment
                objOutputXml = New XmlDocument
                objOutputXml = objbzEquipment.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("EQUIPMENT")
                    drpDownList.DataTextField = "EQUIPMENT_CODE"
                    drpDownList.DataValueField = "PRODUCTID"
                    drpDownList.DataBind()
                End If
            Case "EQUIPMENTCODEWITH_MAINTAIN_BALANCE_BY"
                Dim objbzEquipment As New AAMS.bizMaster.bzEquipment
                Dim objNodeList As XmlNodeList
                objOutputXml = New XmlDocument
                objOutputXml = objbzEquipment.List2()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objNodeList = objOutputXml.DocumentElement.SelectNodes("EQUIPMENT")
                    For Each objNode As XmlNode In objNodeList
                        Dim li As New ListItem(objNode.Attributes("DESCRIPTION").Value, objNode.Attributes("PRODUCTID").Value & "|" & objNode.Attributes("MAINTAIN_BALANCE_BY").Value & "|" & objNode.Attributes("MAINTAIN_BALANCE").Value & "|" & objNode.Attributes("EGROUP_CODE").Value)
                        drpDownList.Items.Add(li)
                    Next
                End If
                'Not for Common Use 
            Case "EQUIPMENTNOT"
                Dim objbzEquipment As New AAMS.bizMaster.bzEquipment
                Dim objNodeList As XmlNodeList
                objOutputXml = New XmlDocument
                objOutputXml = objbzEquipment.List2()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objNodeList = objOutputXml.DocumentElement.SelectNodes("EQUIPMENT")
                    For Each objNode As XmlNode In objNodeList
                        Dim li As New ListItem(objNode.Attributes("DESCRIPTION").Value, objNode.Attributes("PRODUCTID").Value)
                        drpDownList.Items.Add(li)
                    Next
                End If

                'EQUIPMENTNOT
            Case "CHALLANSTATUS"
                Dim objbzChallan As New AAMS.bizInventory.bzChallan
                objOutputXml = New XmlDocument
                objOutputXml = objbzChallan.ListChallanStatus()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CHALLANSTATUS")
                    drpDownList.DataTextField = "STATUS_NAME"
                    drpDownList.DataValueField = "STATUSID"
                    drpDownList.DataBind()
                End If

            Case "EQUIPMENTCPU"
                Dim objbzEquipment As New AAMS.bizMaster.bzEquipment
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<MS_LISTEQUIPMENT_INPUT><EGROUP_CODE>CPU</EGROUP_CODE></MS_LISTEQUIPMENT_INPUT>")
                objOutputXml = objbzEquipment.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("EQUIPMENT")
                    drpDownList.DataTextField = "EQUIPMENT_CODE"
                    drpDownList.DataValueField = "EQUIPMENT_CODE"
                    drpDownList.DataBind()
                End If
            Case "EQUIPMENTKBD"
                Dim objbzEquipment As New AAMS.bizMaster.bzEquipment
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<MS_LISTEQUIPMENT_INPUT><EGROUP_CODE>KBD</EGROUP_CODE></MS_LISTEQUIPMENT_INPUT>")
                objOutputXml = objbzEquipment.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("EQUIPMENT")
                    drpDownList.DataTextField = "EQUIPMENT_CODE"
                    drpDownList.DataValueField = "EQUIPMENT_CODE"
                    drpDownList.DataBind()
                End If
            Case "EQUIPMENTMSE"
                Dim objbzEquipment As New AAMS.bizMaster.bzEquipment
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<MS_LISTEQUIPMENT_INPUT><EGROUP_CODE>MSE</EGROUP_CODE></MS_LISTEQUIPMENT_INPUT>")
                objOutputXml = objbzEquipment.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("EQUIPMENT")
                    drpDownList.DataTextField = "EQUIPMENT_CODE"
                    drpDownList.DataValueField = "EQUIPMENT_CODE"
                    drpDownList.DataBind()
                End If
            Case "EQUIPMENTMON"
                Dim objbzEquipment As New AAMS.bizMaster.bzEquipment
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<MS_LISTEQUIPMENT_INPUT><EGROUP_CODE>MON</EGROUP_CODE></MS_LISTEQUIPMENT_INPUT>")
                objOutputXml = objbzEquipment.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("EQUIPMENT")
                    drpDownList.DataTextField = "EQUIPMENT_CODE"
                    drpDownList.DataValueField = "EQUIPMENT_CODE"
                    drpDownList.DataBind()
                End If

                ' @  It is used for Misc. Equipment only with null input  --  By Abhishek
            Case "EQUIPMENTMISC"
                Dim objbzEquipment As New AAMS.bizMaster.bzEquipment
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<MS_LISTEQUIPMENT_INPUT><EGROUP_CODE></EGROUP_CODE></MS_LISTEQUIPMENT_INPUT>")
                objOutputXml = objbzEquipment.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("EQUIPMENT")
                    drpDownList.DataTextField = "EQUIPMENT_CODE"
                    drpDownList.DataValueField = "EQUIPMENT_CODE"
                    drpDownList.DataBind()
                End If
            Case "ResponsbileStaff"  ' Only By Department Market and Sales
                Dim objbzEmployee As New AAMS.bizMaster.bzEmployee


                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<MS_SEARCHDEPARTMENTEMPLOYEE_INPUT><Employee_Type>Productivity</Employee_Type><Aoffice/></MS_SEARCHDEPARTMENTEMPLOYEE_INPUT>")
                objOutputXml = objbzEmployee.ListDepartmentEmployees(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("EMPLOYEE")
                    drpDownList.DataTextField = "Employee_Name"
                    drpDownList.DataValueField = "EmployeeID"
                    drpDownList.DataBind()
                End If

            Case "PROVIDERS"
                Dim bzProviders As New AAMS.bizProductivity.bzTravelAssistance
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objOutputXml = bzProviders.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("TAS_PROVIDER")
                    drpDownList.DataTextField = "TAS_PROVIDER_NAME"
                    drpDownList.DataValueField = "TAS_PROVIDER_CODE"
                    drpDownList.DataBind()
                End If
            Case "ISPPROVIDER"
                Dim obzProvider As New AAMS.bizISP.bzProvider
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objOutputXml = obzProvider.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("ISP")
                    drpDownList.DataTextField = "ProviderName"
                    drpDownList.DataValueField = "ProviderID"
                    drpDownList.DataBind()
                End If
            Case "SECURITYGROUP"
                Dim obzbzPermission As New AAMS.bizMaster.bzPermission
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objOutputXml = obzbzPermission.ListSecurityGroup
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("SECURITYGROUP")
                    drpDownList.DataTextField = "Sec_Group"
                    drpDownList.DataValueField = "Sec_Group_ID"
                    drpDownList.DataBind()
                End If
            Case "EmplolyeeOfAccount" ' Only By Department Accounts In ISP Module
                Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<MS_SEARCHDEPARTMENTEMPLOYEE_INPUT><Employee_Type>ACCOUNTS</Employee_Type><Aoffice/></MS_SEARCHDEPARTMENTEMPLOYEE_INPUT>")
                objOutputXml = objbzEmployee.ListDepartmentEmployees(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("EMPLOYEE")
                    drpDownList.DataTextField = "Employee_Name"
                    drpDownList.DataValueField = "EmployeeID"
                    drpDownList.DataBind()
                End If

                '************************** Code For Birdres Helpdesk ************************************************

            Case "BRFEEDBACKSTATUS"
                Dim objbzFeedBack As New AAMS.bizBRHelpDesk.bzFeedback
                objOutputXml = New XmlDocument
                objOutputXml = objbzFeedBack.ListFeedbackSuggestionStatus()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("STATUS")
                    drpDownList.DataTextField = "FEEDBACK_STATUS_NAME"
                    drpDownList.DataValueField = "FEEDBACK_STATUS_ID"
                    drpDownList.DataBind()
                End If


            Case "BRCOORDINATOR1"
                Dim objbzCoordinator As New AAMS.bizBRHelpDesk.bzCoordinator
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCOORDINATOR_INPUT><COORDINATOR_TYPE/><Aoffice/></HD_LISTCOORDINATOR_INPUT>")
                objInXml.DocumentElement.SelectSingleNode("COORDINATOR_TYPE").InnerText = "1"
                objOutputXml = objbzCoordinator.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("COORDINATOR")
                    drpDownList.DataTextField = "EMPLOYEE_NAME"
                    drpDownList.DataValueField = "EMPLOYEE_ID"
                    drpDownList.DataBind()
                End If


            Case "BRCOORDINATOR2"
                Dim objbzCoordinator As New AAMS.bizBRHelpDesk.bzCoordinator
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCOORDINATOR_INPUT><COORDINATOR_TYPE/><Aoffice/></HD_LISTCOORDINATOR_INPUT>")
                objInXml.DocumentElement.SelectSingleNode("COORDINATOR_TYPE").InnerText = "2"
                objOutputXml = objbzCoordinator.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("COORDINATOR")
                    drpDownList.DataTextField = "EMPLOYEE_NAME"
                    drpDownList.DataValueField = "EMPLOYEE_ID"
                    drpDownList.DataBind()
                End If


                '*******************************************************************************************

            Case "BRDISPOSITION"
                Dim objbzDisposition As New AAMS.bizBRHelpDesk.bzDisposition
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
                objOutputXml = objbzDisposition.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("DISPOSITION")
                    drpDownList.DataTextField = "DISPOSITION_NAME"
                    drpDownList.DataValueField = "DISPOSITION_ID"
                    drpDownList.DataBind()
                End If
            Case "BRTEAM"
                Dim objbzTeam As New AAMS.bizBRHelpDesk.bzTeam
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
                objOutputXml = objbzTeam.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("TEAM")
                    drpDownList.DataTextField = "TEAM_NAME"
                    drpDownList.DataValueField = "TEAM_ID"
                    drpDownList.DataBind()
                End If
            Case "BRQueryStatus"
                Dim objCallStatus As New AAMS.bizBRHelpDesk.bzCallStatus
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSTATUS_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSTATUS_INPUT>")
                objOutputXml = objCallStatus.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_STATUS")
                    drpDownList.DataTextField = "HD_STATUS_NAME"
                    drpDownList.DataValueField = "HD_STATUS_ID"
                    drpDownList.DataBind()
                End If
            Case "BRQueryStatusWithClosed"
                Dim objCallStatus As New AAMS.bizBRHelpDesk.bzCallStatus
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                Dim objNodeList As XmlNodeList
                objInXml.LoadXml("<HD_LISTCALLSTATUS_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSTATUS_INPUT>")
                objOutputXml = objCallStatus.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objNodeList = objOutputXml.DocumentElement.SelectNodes("CALL_STATUS")
                    For Each objNode As XmlNode In objNodeList
                        Dim li As New ListItem(objNode.Attributes("HD_STATUS_NAME").Value, objNode.Attributes("HD_STATUS_ID").Value & "|" & objNode.Attributes("HD_STATUS_CLOSE").Value)
                        drpDownList.Items.Add(li)
                    Next
                    'objXmlReader = New XmlNodeReader(objOutputXml)
                    'ds.ReadXml(objXmlReader)
                    'drpDownList.DataSource = ds.Tables("CALL_STATUS")
                    'drpDownList.DataTextField = "HD_STATUS_NAME"
                    'drpDownList.DataValueField = "HD_STATUS_ID" & "|" & "HD_STATUS_CLOSE"
                    'drpDownList.DataBind()
                End If
            Case "BRQUERYPRIORITY"
                Dim objSeverities As New AAMS.bizBRHelpDesk.bzSeverities
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
                objOutputXml = objSeverities.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("SEVERITIES")
                    drpDownList.DataTextField = "HD_SEVERITY_NAME"
                    drpDownList.DataValueField = "HD_SEVERITY_ID"
                    drpDownList.DataBind()
                End If
            Case "BRCUSTOMERCATEGORY"
                Dim objCustomerCategory As New AAMS.bizBRHelpDesk.bzCustomerCategory
                objOutputXml = New XmlDocument
                objOutputXml = objCustomerCategory.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CUSTOMER_CATEGORY")
                    drpDownList.DataTextField = "CUSTOMER_CATEGORY_NAME"
                    drpDownList.DataValueField = "CUSTOMER_CATEGORY_ID"
                    drpDownList.DataBind()
                End If
            Case "BRCONTACTTYPE"
                Dim objContactType As New AAMS.bizBRHelpDesk.bzContactType
                objOutputXml = New XmlDocument
                objOutputXml = objContactType.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CONTACT_TYPE")
                    drpDownList.DataTextField = "CONTACT_TYPE_NAME"
                    drpDownList.DataValueField = "CONTACT_TYPE_ID"
                    drpDownList.DataBind()
                End If
            Case "BRWorkOrderAssignee"
                Dim objWOAssignees As New AAMS.bizBRHelpDesk.bzWOAssignees
                objOutputXml = New XmlDocument
                objOutputXml = objWOAssignees.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("WO_ASSIGNEES")
                    drpDownList.DataTextField = "WO_ASSIGNEE_NAME"
                    drpDownList.DataValueField = "WO_ASSIGNEE_ID"
                    drpDownList.DataBind()
                End If
            Case "BRWorkOrderFollowUp"
                Dim objWOFollowUp As New AAMS.bizBRHelpDesk.bzWOFollowUp
                objOutputXml = New XmlDocument

                objOutputXml = objWOFollowUp.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("WO_FOLLOWUP")
                    drpDownList.DataTextField = "WO_FOLLOWUP_NAME"
                    drpDownList.DataValueField = "WO_FOLLOWUP_ID"
                    drpDownList.DataBind()
                End If
            Case "BRWorkOrderSeverity"
                Dim objWOSeverities As New AAMS.bizBRHelpDesk.bzWOSeverities
                objOutputXml = New XmlDocument

                objOutputXml = objWOSeverities.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("WO_SEVERITIES")
                    drpDownList.DataTextField = "WO_SEVERITY_NAME"
                    drpDownList.DataValueField = "WO_SEVERITY_ID"
                    drpDownList.DataBind()
                End If
            Case "BRWorkOrderType"
                Dim objWOType As New AAMS.bizBRHelpDesk.bzWOType
                objOutputXml = New XmlDocument

                objOutputXml = objWOType.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("WO_TYPE")
                    drpDownList.DataTextField = "WO_TYPE_NAME"
                    drpDownList.DataValueField = "WO_TYPE_ID"
                    drpDownList.DataBind()
                End If
            Case "BRQuerySubGroup"
                Dim objCallSubGroup As New AAMS.bizBRHelpDesk.bzCallSubGroup
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT>")
                objOutputXml = objCallSubGroup.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_SUB_GROUP")
                    drpDownList.DataTextField = "CALL_SUB_GROUP_NAME"
                    drpDownList.DataValueField = "CALL_SUB_GROUP_ID"
                    drpDownList.DataBind()
                End If
                ' Added by Rakesh
            Case "BRTSTATUS"
                Dim objInXml As New XmlDocument

                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>2</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
                Dim objbzCallStatus As New AAMS.bizBRHelpDesk.bzCallStatus
                objOutputXml = New XmlDocument
                objOutputXml = objbzCallStatus.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_STATUS")
                    drpDownList.DataTextField = "HD_STATUS_NAME"
                    drpDownList.DataValueField = "HD_STATUS_ID"
                    drpDownList.DataBind()
                End If
            Case "BRTSTATUSWITHCLOSED"
                Dim objCallStatus As New AAMS.bizBRHelpDesk.bzCallStatus
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                Dim objNodeList As XmlNodeList
                objInXml.LoadXml("<HD_LISTCALLSTATUS_INPUT><HD_QUERY_GROUP_ID>2</HD_QUERY_GROUP_ID></HD_LISTCALLSTATUS_INPUT>")
                objOutputXml = objCallStatus.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objNodeList = objOutputXml.DocumentElement.SelectNodes("CALL_STATUS")
                    For Each objNode As XmlNode In objNodeList
                        Dim li As New ListItem(objNode.Attributes("HD_STATUS_NAME").Value, objNode.Attributes("HD_STATUS_ID").Value & "|" & objNode.Attributes("HD_STATUS_CLOSE").Value)
                        drpDownList.Items.Add(li)
                    Next
                    'objXmlReader = New XmlNodeReader(objOutputXml)
                    'ds.ReadXml(objXmlReader)
                    'drpDownList.DataSource = ds.Tables("CALL_STATUS")
                    'drpDownList.DataTextField = "HD_STATUS_NAME"
                    'drpDownList.DataValueField = "HD_STATUS_ID" & "|" & "HD_STATUS_CLOSE"
                    'drpDownList.DataBind()
                End If
            Case "BRTPRIORITY"
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>2</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
                Dim objbzCallSeverity As New AAMS.bizBRHelpDesk.bzSeverities
                objOutputXml = New XmlDocument
                objOutputXml = objbzCallSeverity.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("SEVERITIES")
                    drpDownList.DataTextField = "HD_SEVERITY_NAME"
                    drpDownList.DataValueField = "HD_SEVERITY_ID"
                    drpDownList.DataBind()
                End If

            Case "BRTQUERYSUBGROUP"
                Dim objCallSubGroup As New AAMS.bizBRHelpDesk.bzCallSubGroup
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>2</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT>")
                objOutputXml = objCallSubGroup.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_SUB_GROUP")
                    drpDownList.DataTextField = "CALL_SUB_GROUP_NAME"
                    drpDownList.DataValueField = "CALL_SUB_GROUP_ID"
                    drpDownList.DataBind()
                End If



                '***********************************************

            Case "BRHDFOLLOWUP"
                Dim objHdFollowUp As New AAMS.bizBRHelpDesk.bzPTRFollowUp
                objOutputXml = New XmlDocument
                objOutputXml = objHdFollowUp.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_FOLLOWUP")
                    drpDownList.DataTextField = "HD_PTR_FOLLOWUP_NAME"
                    drpDownList.DataValueField = "HD_PTR_FOLLOWUP_ID"
                    drpDownList.DataBind()
                End If
                'Code Added by Mukund On 28th Feb 2008 
            Case "BRHDPTRTYPECAT"
                Dim objHdPtrTypeCat As New AAMS.bizBRHelpDesk.bzPTRTypeCategory
                objOutputXml = New XmlDocument
                objOutputXml = objHdPtrTypeCat.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_CATEGORIES")
                    drpDownList.DataTextField = "HD_PTR_TYCAT_NAME"
                    drpDownList.DataValueField = "HD_PTR_TYCAT_ID"
                    drpDownList.DataBind()
                End If
                'Code Added by Mukund On 28th Feb 2008 
            Case "BRHDPTRTYPE"
                Dim objHdPtrType As New AAMS.bizBRHelpDesk.bzPTRType
                objOutputXml = New XmlDocument
                objOutputXml = objHdPtrType.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_TYPE")
                    drpDownList.DataTextField = "HD_PTR_TYPE_NAME"
                    drpDownList.DataValueField = "HD_PTR_TYPE_ID"
                    drpDownList.DataBind()
                End If
                'Code Added by Mukund On 28th Feb 2008 
            Case "BRHDPTRSEVERITY"
                Dim objHdSeverity As New AAMS.bizBRHelpDesk.bzPTRSeverity
                objOutputXml = New XmlDocument
                objOutputXml = objHdSeverity.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_SEVERITY")
                    drpDownList.DataTextField = "HD_PTR_SEV_NAME"
                    drpDownList.DataValueField = "HD_PTR_SEV_ID"
                    drpDownList.DataBind()
                End If
                ' Code Added by Mukund On 28th Feb 2008 
            Case "BRHDPTRCALLSTATUS"
                Dim objHdStatis As New AAMS.bizBRHelpDesk.bzCallStatus
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT>")
                objOutputXml = objHdStatis.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_STATUS")
                    drpDownList.DataTextField = "HD_STATUS_NAME"
                    drpDownList.DataValueField = "HD_STATUS_ID"
                    drpDownList.DataBind()
                End If


                'Code Added by Mukund On 7th March 2008 Not for General Use 
            Case "BRHDPTRCALLSTATUS_NOTUSE"
                ' Case "HDPTRCALLSTATUS"
                Dim objHdStatis As New AAMS.bizBRHelpDesk.bzCallStatus
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT>")
                objOutputXml = objHdStatis.List1(objInXml)
                Dim Node As XmlNode
                Dim nodeLists As XmlNodeList
                Dim outerXmlSet As New XmlDocument
                ' Dim strOuteXml As String = objOutputXml.ReplaceChild(.InnerXml
                nodeLists = objOutputXml.DocumentElement.SelectNodes("CALL_STATUS")
                For Each Node In nodeLists
                    Dim NodeOld As XmlNode
                    NodeOld = Node
                    Node.Attributes("HD_STATUS_ID").Value = Node.Attributes("HD_STATUS_ID").Value.Trim() + "|" + Node.Attributes("HD_STATUS_CLOSE").Value.Trim()
                    objOutputXml.DocumentElement.ReplaceChild(Node, NodeOld)
                Next
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_STATUS")
                    drpDownList.DataTextField = "HD_STATUS_NAME"
                    drpDownList.DataValueField = "HD_STATUS_ID"
                    drpDownList.DataBind()
                End If



                'Code Added by Mukund On 29th Feb 2008 
            Case "BRHDASSIGNEDTO"
                Dim objHdPtrAssignree As New AAMS.bizBRHelpDesk.bzPTRAssignee
                objOutputXml = New XmlDocument
                objOutputXml = objHdPtrAssignree.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_ASSIGNEE")
                    drpDownList.DataTextField = "ASSIGNEE_NAME"
                    drpDownList.DataValueField = "ASSIGNEEID"
                    drpDownList.DataBind()
                End If

                'Code Added by Mukund On 13th March 2008 
            Case "BRHDQUESTIONSTATUS"
                Dim objFeedback As New AAMS.bizBRHelpDesk.bzFeedback
                objOutputXml = New XmlDocument
                objOutputXml = objFeedback.ListFeedbackStatus()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("STATUS")
                    drpDownList.DataTextField = "STATUS_NAME"
                    drpDownList.DataValueField = "STATUS_ID"
                    drpDownList.DataBind()
                End If



            Case "BRQueryCategory"
                Dim objCallCategory As New AAMS.bizBRHelpDesk.bzCallCategory
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
                objOutputXml = objCallCategory.List1(objInXml)
                ' objOutputXml = objCallCategory.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_CATEGORY") '
                    drpDownList.DataTextField = "CALL_CATEGORY_NAME"
                    drpDownList.DataValueField = "CALL_CATEGORY_ID"
                    drpDownList.DataBind()
                End If
            Case "BRListFuntionalCategory"
                Dim objbzCallCategory As New AAMS.bizBRHelpDesk.bzCallCategory
                objOutputXml = New XmlDocument
                objOutputXml = objbzCallCategory.ListFuntionalCategory
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_CATEGORY")
                    drpDownList.DataTextField = "CALL_CATEGORY_NAME"
                    drpDownList.DataValueField = "CALL_CATEGORY_ID"
                    drpDownList.DataBind()
                End If
                '************************** End  Code For Birdres Helpdesk ********************************************

            Case "FeedbackDepartment"
                Dim objbzDepartment As New AAMS.bizHelpDesk.bzFeedback
                objOutputXml = New XmlDocument
                objOutputXml = objbzDepartment.ListFeedbackDept

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("DEPARTMENT")
                    drpDownList.DataTextField = "SUG_DEPT_NAME"
                    drpDownList.DataValueField = "SUG_DEPT_ID"
                    drpDownList.DataBind()
                End If

                '@ Start of Code Added On 5Th April 2011 By Abhishek
            Case "CompanyVertical"
                drpDownList.Items.Insert(0, New ListItem("Amadeus", "1"))
                drpDownList.Items.Insert(1, New ListItem("ResBird", "2"))
                drpDownList.Items.Insert(2, New ListItem("Non 1A", "3"))
                '@ End of Code Added on %th April 2011 By Abhishek



                '################################################################
                'Start of Code Added by Abhishek On 25th April 2011  For Back Office HelpDesk
                '################################################################
            Case "BOQuerySubGroup"
                Dim objCallSubGroup As New AAMS.bizBOHelpdesk.bzCallSubGroup
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT>")
                objOutputXml = objCallSubGroup.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_SUB_GROUP")
                    drpDownList.DataTextField = "CALL_SUB_GROUP_NAME"
                    drpDownList.DataValueField = "CALL_SUB_GROUP_ID"
                    drpDownList.DataBind()
                End If

            Case "BOHDASSIGNEDTO"
                Dim objHdPtrAssignree As New AAMS.bizBOHelpdesk.bzPTRAssignee
                objOutputXml = New XmlDocument
                objOutputXml = objHdPtrAssignree.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_ASSIGNEE")
                    drpDownList.DataTextField = "ASSIGNEE_NAME"
                    drpDownList.DataValueField = "ASSIGNEEID"
                    drpDownList.DataBind()
                End If

            Case "BOHDFOLLOWUP"
                Dim objHdFollowUp As New AAMS.bizBOHelpdesk.bzPTRFollowUp
                objOutputXml = New XmlDocument
                objOutputXml = objHdFollowUp.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_FOLLOWUP")
                    drpDownList.DataTextField = "HD_PTR_FOLLOWUP_NAME"
                    drpDownList.DataValueField = "HD_PTR_FOLLOWUP_ID"
                    drpDownList.DataBind()
                End If

            Case "BOHDPTRFOLLOWUP"
                Dim objHdFollowUp As New AAMS.bizBOHelpdesk.bzPTRFollowUp
                objOutputXml = New XmlDocument
                objOutputXml = objHdFollowUp.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_FOLLOWUP")
                    drpDownList.DataTextField = "HD_PTR_FOLLOWUP_NAME"
                    drpDownList.DataValueField = "HD_PTR_FOLLOWUP_ID"
                    drpDownList.DataBind()
                End If
            Case "BOHDPTRSTATUS"
                Dim objPTRStatus As New AAMS.bizBOHelpdesk.bzPTRStatus
                objOutputXml = New XmlDocument
                objOutputXml = objPTRStatus.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_STATUS")
                    drpDownList.DataTextField = "HD_STATUS_NAME"
                    drpDownList.DataValueField = "HD_STATUS_ID"
                    drpDownList.DataBind()
                End If
            Case "BOHDPTRSTATUSFORMANAGE"
                Dim objIRStatus As New AAMS.bizBOHelpdesk.bzPTRStatus
                objOutputXml = New XmlDocument
                objOutputXml = objIRStatus.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    Dim Node As XmlNode
                    Dim nodeLists As XmlNodeList
                    Dim outerXmlSet As New XmlDocument
                    ' Dim strOuteXml As String = objOutputXml.ReplaceChild(.InnerXml
                    nodeLists = objOutputXml.DocumentElement.SelectNodes("PTR_STATUS")
                    For Each Node In nodeLists
                        Dim NodeOld As XmlNode
                        NodeOld = Node
                        Node.Attributes("HD_STATUS_ID").Value = Node.Attributes("HD_STATUS_ID").Value.Trim() + "|" + Node.Attributes("HD_STATUS_CLOSE").Value.Trim()
                        objOutputXml.DocumentElement.ReplaceChild(Node, NodeOld)
                    Next

                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_STATUS")
                    drpDownList.DataTextField = "HD_STATUS_NAME"
                    drpDownList.DataValueField = "HD_STATUS_ID"
                    drpDownList.DataBind()
                End If
            Case "BOHDPTRTYPECAT"
                Dim objHdPtrTypeCat As New AAMS.bizBOHelpdesk.bzPTRTypeCategory
                objOutputXml = New XmlDocument
                objOutputXml = objHdPtrTypeCat.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_CATEGORIES")
                    drpDownList.DataTextField = "HD_PTR_TYCAT_NAME"
                    drpDownList.DataValueField = "HD_PTR_TYCAT_ID"
                    drpDownList.DataBind()
                End If

            Case "BOHDPTRASSIGNEDTO"
                Dim objHdPtrAssignree As New AAMS.bizBOHelpdesk.bzPTRAssignee
                objOutputXml = New XmlDocument
                objOutputXml = objHdPtrAssignree.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_ASSIGNEE")
                    drpDownList.DataTextField = "ASSIGNEE_NAME"
                    drpDownList.DataValueField = "ASSIGNEEID"
                    drpDownList.DataBind()
                End If
            Case "BOHDPTRTYPE"
                Dim objHdPtrType As New AAMS.bizBOHelpdesk.bzPTRType
                objOutputXml = New XmlDocument
                objOutputXml = objHdPtrType.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_TYPE")
                    drpDownList.DataTextField = "HD_PTR_TYPE_NAME"
                    drpDownList.DataValueField = "HD_PTR_TYPE_ID"
                    drpDownList.DataBind()
                End If
            Case "BOHDPTRSEVERITY"
                Dim objHdSeverity As New AAMS.bizBOHelpdesk.bzPTRSeverity
                objOutputXml = New XmlDocument
                objOutputXml = objHdSeverity.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_SEVERITY")
                    drpDownList.DataTextField = "HD_PTR_SEV_NAME"
                    drpDownList.DataValueField = "HD_PTR_SEV_ID"
                    drpDownList.DataBind()
                End If



            Case "BOHDIRFOLLOWUP"
                Dim objHdFollowUp As New AAMS.bizBOHelpdesk.bzIRFollowUp
                objOutputXml = New XmlDocument
                objOutputXml = objHdFollowUp.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("IR_FOLLOWUP")
                    drpDownList.DataTextField = "HD_IR_FOLLOWUP_NAME"
                    drpDownList.DataValueField = "HD_IR_FOLLOWUP_ID"
                    drpDownList.DataBind()
                End If
            Case "BOHDIRSTATUS"
                Dim objPTRStatus As New AAMS.bizBOHelpdesk.bzIRStatus
                objOutputXml = New XmlDocument
                objOutputXml = objPTRStatus.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("IR_STATUS")
                    drpDownList.DataTextField = "HD_STATUS_NAME"
                    drpDownList.DataValueField = "HD_STATUS_ID"
                    drpDownList.DataBind()
                End If
            Case "BOHDIRSTATUSFORMANAGE"
                Dim objIRStatus As New AAMS.bizBOHelpdesk.bzIRStatus
                objOutputXml = New XmlDocument
                objOutputXml = objIRStatus.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    Dim Node As XmlNode
                    Dim nodeLists As XmlNodeList
                    Dim outerXmlSet As New XmlDocument
                    ' Dim strOuteXml As String = objOutputXml.ReplaceChild(.InnerXml
                    nodeLists = objOutputXml.DocumentElement.SelectNodes("IR_STATUS")
                    For Each Node In nodeLists
                        Dim NodeOld As XmlNode
                        NodeOld = Node
                        Node.Attributes("HD_STATUS_ID").Value = Node.Attributes("HD_STATUS_ID").Value.Trim() + "|" + Node.Attributes("HD_STATUS_CLOSE").Value.Trim()
                        objOutputXml.DocumentElement.ReplaceChild(Node, NodeOld)
                    Next

                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("IR_STATUS")
                    drpDownList.DataTextField = "HD_STATUS_NAME"
                    drpDownList.DataValueField = "HD_STATUS_ID"
                    drpDownList.DataBind()
                End If
            Case "BOHDIRTYPECAT"
                Dim objHdPtrTypeCat As New AAMS.bizBOHelpdesk.bzIRTypeCategory
                objOutputXml = New XmlDocument
                objOutputXml = objHdPtrTypeCat.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("IR_CATEGORIES")
                    drpDownList.DataTextField = "HD_IR_TYCAT_NAME"
                    drpDownList.DataValueField = "HD_IR_TYCAT_ID"
                    drpDownList.DataBind()
                End If

            Case "BOHDIRASSIGNEDTO"
                Dim objHdPtrAssignree As New AAMS.bizBOHelpdesk.bzIRAssignee
                objOutputXml = New XmlDocument
                objOutputXml = objHdPtrAssignree.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("IR_ASSIGNEE")
                    drpDownList.DataTextField = "ASSIGNEE_NAME"
                    drpDownList.DataValueField = "ASSIGNEEID"
                    drpDownList.DataBind()
                End If
            Case "BOHDIRTYPE"
                Dim objHdPtrType As New AAMS.bizBOHelpdesk.bzIRType
                objOutputXml = New XmlDocument
                objOutputXml = objHdPtrType.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("IR_TYPE")
                    drpDownList.DataTextField = "HD_IR_TYPE_NAME"
                    drpDownList.DataValueField = "HD_IR_TYPE_ID"
                    drpDownList.DataBind()
                End If
            Case "BOHDIRSEVERITY"
                Dim objHdSeverity As New AAMS.bizBOHelpdesk.bzIRSeverity
                objOutputXml = New XmlDocument
                objOutputXml = objHdSeverity.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("IR_SEVERITY")
                    drpDownList.DataTextField = "HD_IR_SEV_NAME"
                    drpDownList.DataValueField = "HD_IR_SEV_ID"
                    drpDownList.DataBind()
                End If

            Case "BOListFuntionalCategory"
                Dim objbzCallCategory As New AAMS.bizBOHelpdesk.bzCallCategory
                objOutputXml = New XmlDocument
                objOutputXml = objbzCallCategory.ListFuntionalCategory
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_CATEGORY")
                    drpDownList.DataTextField = "CALL_CATEGORY_NAME"
                    drpDownList.DataValueField = "CALL_CATEGORY_ID"
                    drpDownList.DataBind()
                End If

            Case "BOWorkOrderAssignee"
                Dim objWOAssignees As New AAMS.bizBOHelpdesk.bzWOAssignees
                objOutputXml = New XmlDocument
                objOutputXml = objWOAssignees.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("WO_ASSIGNEES")
                    drpDownList.DataTextField = "WO_ASSIGNEE_NAME"
                    drpDownList.DataValueField = "WO_ASSIGNEE_ID"
                    drpDownList.DataBind()
                End If
            Case "BOWorkOrderFollowUp"
                Dim objWOFollowUp As New AAMS.bizBOHelpdesk.bzWOFollowUp
                objOutputXml = New XmlDocument

                objOutputXml = objWOFollowUp.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("WO_FOLLOWUP")
                    drpDownList.DataTextField = "WO_FOLLOWUP_NAME"
                    drpDownList.DataValueField = "WO_FOLLOWUP_ID"
                    drpDownList.DataBind()
                End If
            Case "BOWorkOrderSeverity"
                Dim objWOSeverities As New AAMS.bizBOHelpdesk.bzWOSeverities
                objOutputXml = New XmlDocument

                objOutputXml = objWOSeverities.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("WO_SEVERITIES")
                    drpDownList.DataTextField = "WO_SEVERITY_NAME"
                    drpDownList.DataValueField = "WO_SEVERITY_ID"
                    drpDownList.DataBind()
                End If
            Case "BOWorkOrderType"
                Dim objWOType As New AAMS.bizBOHelpdesk.bzWOType
                objOutputXml = New XmlDocument

                objOutputXml = objWOType.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("WO_TYPE")
                    drpDownList.DataTextField = "WO_TYPE_NAME"
                    drpDownList.DataValueField = "WO_TYPE_ID"
                    drpDownList.DataBind()
                End If
            Case "BOCONTACTTYPE"
                Dim objContactType As New AAMS.bizBOHelpdesk.bzContactType
                objOutputXml = New XmlDocument
                objOutputXml = objContactType.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CONTACT_TYPE")
                    drpDownList.DataTextField = "CONTACT_TYPE_NAME"
                    drpDownList.DataValueField = "CONTACT_TYPE_ID"
                    drpDownList.DataBind()
                End If
            Case "BOQueryStatus"
                Dim objCallStatus As New AAMS.bizBOHelpdesk.bzCallStatus
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSTATUS_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSTATUS_INPUT>")
                objOutputXml = objCallStatus.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_STATUS")
                    drpDownList.DataTextField = "HD_STATUS_NAME"
                    drpDownList.DataValueField = "HD_STATUS_ID"
                    drpDownList.DataBind()
                End If
            Case "BOQueryStatusWithClosed"
                Dim objCallStatus As New AAMS.bizBOHelpdesk.bzCallStatus
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                Dim objNodeList As XmlNodeList
                objInXml.LoadXml("<HD_LISTCALLSTATUS_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSTATUS_INPUT>")
                objOutputXml = objCallStatus.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objNodeList = objOutputXml.DocumentElement.SelectNodes("CALL_STATUS")
                    For Each objNode As XmlNode In objNodeList
                        Dim li As New ListItem(objNode.Attributes("HD_STATUS_NAME").Value, objNode.Attributes("HD_STATUS_ID").Value & "|" & objNode.Attributes("HD_STATUS_CLOSE").Value)
                        drpDownList.Items.Add(li)
                    Next
                    'objXmlReader = New XmlNodeReader(objOutputXml)
                    'ds.ReadXml(objXmlReader)
                    'drpDownList.DataSource = ds.Tables("CALL_STATUS")
                    'drpDownList.DataTextField = "HD_STATUS_NAME"
                    'drpDownList.DataValueField = "HD_STATUS_ID" & "|" & "HD_STATUS_CLOSE"
                    'drpDownList.DataBind()
                End If
            Case "BOQUERYPRIORITY"
                Dim objSeverities As New AAMS.bizBOHelpdesk.bzSeverities
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
                objOutputXml = objSeverities.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("SEVERITIES")
                    drpDownList.DataTextField = "HD_SEVERITY_NAME"
                    drpDownList.DataValueField = "HD_SEVERITY_ID"
                    drpDownList.DataBind()
                End If
            Case "BOTEAM"
                Dim objbzTeam As New AAMS.bizBOHelpdesk.bzTeam
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
                objOutputXml = objbzTeam.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("TEAM")
                    drpDownList.DataTextField = "TEAM_NAME"
                    drpDownList.DataValueField = "TEAM_ID"
                    drpDownList.DataBind()
                End If

            Case "BOCONTACTTYPE"
                Dim objContactType As New AAMS.bizBOHelpdesk.bzContactType
                objOutputXml = New XmlDocument
                objOutputXml = objContactType.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CONTACT_TYPE")
                    drpDownList.DataTextField = "CONTACT_TYPE_NAME"
                    drpDownList.DataValueField = "CONTACT_TYPE_ID"
                    drpDownList.DataBind()
                End If
            Case "BOTQUERYSUBGROUP"
                Dim objCallSubGroup As New AAMS.bizBOHelpdesk.bzCallSubGroup
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>2</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT>")
                objOutputXml = objCallSubGroup.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_SUB_GROUP")
                    drpDownList.DataTextField = "CALL_SUB_GROUP_NAME"
                    drpDownList.DataValueField = "CALL_SUB_GROUP_ID"
                    drpDownList.DataBind()
                End If

            Case "BOCOORDINATOR1"
                Dim objbzCoordinator As New AAMS.bizBOHelpdesk.bzCoordinator
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCOORDINATOR_INPUT><COORDINATOR_TYPE/><Aoffice/></HD_LISTCOORDINATOR_INPUT>")
                objInXml.DocumentElement.SelectSingleNode("COORDINATOR_TYPE").InnerText = "1"
                objOutputXml = objbzCoordinator.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("COORDINATOR")
                    drpDownList.DataTextField = "EMPLOYEE_NAME"
                    drpDownList.DataValueField = "EMPLOYEE_ID"
                    drpDownList.DataBind()
                End If
            Case "BOCOORDINATOR2"
                Dim objbzCoordinator As New AAMS.bizBOHelpdesk.bzCoordinator
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCOORDINATOR_INPUT><COORDINATOR_TYPE/><Aoffice/></HD_LISTCOORDINATOR_INPUT>")
                objInXml.DocumentElement.SelectSingleNode("COORDINATOR_TYPE").InnerText = "2"
                objOutputXml = objbzCoordinator.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("COORDINATOR")
                    drpDownList.DataTextField = "EMPLOYEE_NAME"
                    drpDownList.DataValueField = "EMPLOYEE_ID"
                    drpDownList.DataBind()
                End If

            Case "BODISPOSITION"
                Dim objbzDisposition As New AAMS.bizBOHelpdesk.bzDisposition
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
                objOutputXml = objbzDisposition.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("DISPOSITION")
                    drpDownList.DataTextField = "DISPOSITION_NAME"
                    drpDownList.DataValueField = "DISPOSITION_ID"
                    drpDownList.DataBind()
                End If

            Case "BOFEEDBACKSTATUS"
                Dim objbzFeedBack As New AAMS.bizBOHelpdesk.bzFeedback
                objOutputXml = New XmlDocument
                objOutputXml = objbzFeedBack.ListFeedbackSuggestionStatus()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("STATUS")
                    drpDownList.DataTextField = "FEEDBACK_STATUS_NAME"
                    drpDownList.DataValueField = "FEEDBACK_STATUS_ID"
                    drpDownList.DataBind()
                End If


            Case "BOTQUERYSUBGROUP"
                Dim objCallSubGroup As New AAMS.bizBOHelpdesk.bzCallSubGroup
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>2</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT>")
                objOutputXml = objCallSubGroup.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_SUB_GROUP")
                    drpDownList.DataTextField = "CALL_SUB_GROUP_NAME"
                    drpDownList.DataValueField = "CALL_SUB_GROUP_ID"
                    drpDownList.DataBind()
                End If

            Case "BOTSTATUSWITHCLOSED"
                Dim objCallStatus As New AAMS.bizBOHelpdesk.bzCallStatus
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                Dim objNodeList As XmlNodeList
                objInXml.LoadXml("<HD_LISTCALLSTATUS_INPUT><HD_QUERY_GROUP_ID>2</HD_QUERY_GROUP_ID></HD_LISTCALLSTATUS_INPUT>")
                objOutputXml = objCallStatus.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objNodeList = objOutputXml.DocumentElement.SelectNodes("CALL_STATUS")
                    For Each objNode As XmlNode In objNodeList
                        Dim li As New ListItem(objNode.Attributes("HD_STATUS_NAME").Value, objNode.Attributes("HD_STATUS_ID").Value & "|" & objNode.Attributes("HD_STATUS_CLOSE").Value)
                        drpDownList.Items.Add(li)
                    Next
                    'objXmlReader = New XmlNodeReader(objOutputXml)
                    'ds.ReadXml(objXmlReader)
                    'drpDownList.DataSource = ds.Tables("CALL_STATUS")
                    'drpDownList.DataTextField = "HD_STATUS_NAME"
                    'drpDownList.DataValueField = "HD_STATUS_ID" & "|" & "HD_STATUS_CLOSE"
                    'drpDownList.DataBind()
                End If
            Case "BOTPRIORITY"
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>2</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
                Dim objbzCallSeverity As New AAMS.bizBOHelpdesk.bzSeverities
                objOutputXml = New XmlDocument
                objOutputXml = objbzCallSeverity.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("SEVERITIES")
                    drpDownList.DataTextField = "HD_SEVERITY_NAME"
                    drpDownList.DataValueField = "HD_SEVERITY_ID"
                    drpDownList.DataBind()
                End If

            Case "BOASSIGNEDTOEMPLOYEE"
                Dim objbzEmployee As New AAMS.bizBOHelpdesk.bzCall
                Dim objInputXml As New XmlDocument
                objOutputXml = objbzEmployee.ListAssignedToEmployee
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("EMPLOYEE")
                    drpDownList.DataTextField = "Employee_Name"
                    drpDownList.DataValueField = "EmployeeID"
                    drpDownList.DataBind()
                End If
            Case "BOCUSTOMERCATEGORY"
                Dim objCustomerCategory As New AAMS.bizBOHelpdesk.bzCustomerCategory
                objOutputXml = New XmlDocument
                objOutputXml = objCustomerCategory.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CUSTOMER_CATEGORY")
                    drpDownList.DataTextField = "CUSTOMER_CATEGORY_NAME"
                    drpDownList.DataValueField = "CUSTOMER_CATEGORY_ID"
                    drpDownList.DataBind()
                End If

            Case "BOHDQUESTIONSTATUS"
                Dim objFeedback As New AAMS.bizBOHelpdesk.bzFeedback
                objOutputXml = New XmlDocument
                objOutputXml = objFeedback.ListFeedbackStatus()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("STATUS")
                    drpDownList.DataTextField = "STATUS_NAME"
                    drpDownList.DataValueField = "STATUS_ID"
                    drpDownList.DataBind()
                End If
            Case "BOFeedbackDepartment"
                Dim objbzDepartment As New AAMS.bizBOHelpdesk.bzFeedback
                objOutputXml = New XmlDocument
                objOutputXml = objbzDepartment.ListFeedbackDept

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("DEPARTMENT")
                    drpDownList.DataTextField = "SUG_DEPT_NAME"
                    drpDownList.DataValueField = "SUG_DEPT_ID"
                    drpDownList.DataBind()
                End If


                '################################################################
                'End of Code Added by Abhishek On 25th April 2011  For Back Office HelpDesk
                '################################################################






                '################################################################
                'Start of Code Added by Ashish for ECOMM Helpdesk Module
                '################################################################

            Case "ET_ListFuntionalCategory"
                Dim objbzCallCategory As New AAMS.bizETrackerHelpDesk.bzCallCategory
                objOutputXml = New XmlDocument
                objOutputXml = objbzCallCategory.ListFuntionalCategory
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_CATEGORY")
                    drpDownList.DataTextField = "CALL_CATEGORY_NAME"
                    drpDownList.DataValueField = "CALL_CATEGORY_ID"
                    drpDownList.DataBind()
                End If

            Case "ET_CUSTOMERCATEGORY"
                Dim objCustomerCategory As New AAMS.bizETrackerHelpDesk.bzCustomerCategory
                objOutputXml = New XmlDocument
                objOutputXml = objCustomerCategory.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CUSTOMER_CATEGORY")
                    drpDownList.DataTextField = "CUSTOMER_CATEGORY_NAME"
                    drpDownList.DataValueField = "CUSTOMER_CATEGORY_ID"
                    drpDownList.DataBind()
                End If


            Case "ET_CONTACTTYPE"
                Dim objContactType As New AAMS.bizETrackerHelpDesk.bzContactType
                objOutputXml = New XmlDocument
                objOutputXml = objContactType.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CONTACT_TYPE")
                    drpDownList.DataTextField = "CONTACT_TYPE_NAME"
                    drpDownList.DataValueField = "CONTACT_TYPE_ID"
                    drpDownList.DataBind()
                End If

            Case "ET_DISPOSITION"
                Dim objbzDisposition As New AAMS.bizETrackerHelpDesk.bzDisposition
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
                objOutputXml = objbzDisposition.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("DISPOSITION")
                    drpDownList.DataTextField = "DISPOSITION_NAME"
                    drpDownList.DataValueField = "DISPOSITION_ID"
                    drpDownList.DataBind()
                End If

            Case "ET_COORDINATOR1"
                Dim objbzCoordinator As New AAMS.bizETrackerHelpDesk.bzCoordinator
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCOORDINATOR_INPUT><COORDINATOR_TYPE/><Aoffice/></HD_LISTCOORDINATOR_INPUT>")
                objInXml.DocumentElement.SelectSingleNode("COORDINATOR_TYPE").InnerText = "1"
                objOutputXml = objbzCoordinator.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("COORDINATOR")
                    drpDownList.DataTextField = "EMPLOYEE_NAME"
                    drpDownList.DataValueField = "EMPLOYEE_ID"
                    drpDownList.DataBind()
                End If
            Case "ET_COORDINATOR2"
                Dim objbzCoordinator As New AAMS.bizETrackerHelpDesk.bzCoordinator
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCOORDINATOR_INPUT><COORDINATOR_TYPE/><Aoffice/></HD_LISTCOORDINATOR_INPUT>")
                objInXml.DocumentElement.SelectSingleNode("COORDINATOR_TYPE").InnerText = "2"
                objOutputXml = objbzCoordinator.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("COORDINATOR")
                    drpDownList.DataTextField = "EMPLOYEE_NAME"
                    drpDownList.DataValueField = "EMPLOYEE_ID"
                    drpDownList.DataBind()
                End If

                ''''''''''
            Case "ET_WorkOrderFollowUp"
                Dim objWOFollowUp As New AAMS.bizETrackerHelpDesk.bzWOFollowUp
                objOutputXml = New XmlDocument

                objOutputXml = objWOFollowUp.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("WO_FOLLOWUP")
                    drpDownList.DataTextField = "WO_FOLLOWUP_NAME"
                    drpDownList.DataValueField = "WO_FOLLOWUP_ID"
                    drpDownList.DataBind()
                End If

            Case "ET_FEEDBACKSTATUS"
                Dim objbzFeedBack As New AAMS.bizETrackerHelpDesk.bzFeedback
                objOutputXml = New XmlDocument
                objOutputXml = objbzFeedBack.ListFeedbackSuggestionStatus()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("STATUS")
                    drpDownList.DataTextField = "FEEDBACK_STATUS_NAME"
                    drpDownList.DataValueField = "FEEDBACK_STATUS_ID"
                    drpDownList.DataBind()
                End If


            Case "ET_FeedbackDepartment"
                Dim objbzDepartment As New AAMS.bizETrackerHelpDesk.bzFeedback
                objOutputXml = New XmlDocument
                objOutputXml = objbzDepartment.ListFeedbackDept

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("DEPARTMENT")
                    drpDownList.DataTextField = "SUG_DEPT_NAME"
                    drpDownList.DataValueField = "SUG_DEPT_ID"
                    drpDownList.DataBind()
                End If



            Case "ET_HDIRFOLLOWUP"
                Dim objHdFollowUp As New AAMS.bizETrackerHelpDesk.bzIRFollowUp
                objOutputXml = New XmlDocument
                objOutputXml = objHdFollowUp.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("IR_FOLLOWUP")
                    drpDownList.DataTextField = "HD_IR_FOLLOWUP_NAME"
                    drpDownList.DataValueField = "HD_IR_FOLLOWUP_ID"
                    drpDownList.DataBind()
                End If

            Case "ET_HDIRSTATUS"
                Dim objPTRStatus As New AAMS.bizETrackerHelpDesk.bzIRStatus
                objOutputXml = New XmlDocument
                objOutputXml = objPTRStatus.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("IR_STATUS")
                    drpDownList.DataTextField = "HD_STATUS_NAME"
                    drpDownList.DataValueField = "HD_STATUS_ID"
                    drpDownList.DataBind()
                End If


            Case "ET_HDIRTYPECAT"
                Dim objHdPtrTypeCat As New AAMS.bizETrackerHelpDesk.bzIRTypeCategory
                objOutputXml = New XmlDocument
                objOutputXml = objHdPtrTypeCat.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("IR_CATEGORIES")
                    drpDownList.DataTextField = "HD_IR_TYCAT_NAME"
                    drpDownList.DataValueField = "HD_IR_TYCAT_ID"
                    drpDownList.DataBind()
                End If

            Case "ET_HDIRTYPE"
                Dim objHdPtrType As New AAMS.bizETrackerHelpDesk.bzIRType
                objOutputXml = New XmlDocument
                objOutputXml = objHdPtrType.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("IR_TYPE")
                    drpDownList.DataTextField = "HD_IR_TYPE_NAME"
                    drpDownList.DataValueField = "HD_IR_TYPE_ID"
                    drpDownList.DataBind()
                End If

            Case "ET_HDIRSEVERITY"
                Dim objHdSeverity As New AAMS.bizETrackerHelpDesk.bzIRSeverity
                objOutputXml = New XmlDocument
                objOutputXml = objHdSeverity.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("IR_SEVERITY")
                    drpDownList.DataTextField = "HD_IR_SEV_NAME"
                    drpDownList.DataValueField = "HD_IR_SEV_ID"
                    drpDownList.DataBind()
                End If

            Case "ET_HDIRASSIGNEDTO"
                Dim objHdPtrAssignree As New AAMS.bizETrackerHelpDesk.bzIRAssignee
                objOutputXml = New XmlDocument
                objOutputXml = objHdPtrAssignree.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("IR_ASSIGNEE")
                    drpDownList.DataTextField = "ASSIGNEE_NAME"
                    drpDownList.DataValueField = "ASSIGNEEID"
                    drpDownList.DataBind()
                End If
                '''''''''''
            Case "ET_HDFOLLOWUP"
                Dim objHdFollowUp As New AAMS.bizETrackerHelpDesk.bzPTRFollowUp
                objOutputXml = New XmlDocument
                objOutputXml = objHdFollowUp.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_FOLLOWUP")
                    drpDownList.DataTextField = "HD_PTR_FOLLOWUP_NAME"
                    drpDownList.DataValueField = "HD_PTR_FOLLOWUP_ID"
                    drpDownList.DataBind()
                End If

            Case "ET_HDPTRSTATUS"
                Dim objPTRStatus As New AAMS.bizETrackerHelpDesk.bzPTRStatus
                objOutputXml = New XmlDocument
                objOutputXml = objPTRStatus.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_STATUS")
                    drpDownList.DataTextField = "HD_STATUS_NAME"
                    drpDownList.DataValueField = "HD_STATUS_ID"
                    drpDownList.DataBind()
                End If

            Case "ET_HDPTRTYPECAT"
                Dim objHdPtrTypeCat As New AAMS.bizETrackerHelpDesk.bzPTRTypeCategory
                objOutputXml = New XmlDocument
                objOutputXml = objHdPtrTypeCat.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_CATEGORIES")
                    drpDownList.DataTextField = "HD_PTR_TYCAT_NAME"
                    drpDownList.DataValueField = "HD_PTR_TYCAT_ID"
                    drpDownList.DataBind()
                End If


            Case "ET_HDPTRTYPE"
                Dim objHdPtrType As New AAMS.bizETrackerHelpDesk.bzPTRType
                objOutputXml = New XmlDocument
                objOutputXml = objHdPtrType.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_TYPE")
                    drpDownList.DataTextField = "HD_PTR_TYPE_NAME"
                    drpDownList.DataValueField = "HD_PTR_TYPE_ID"
                    drpDownList.DataBind()
                End If


            Case "ET_HDPTRSEVERITY"
                Dim objHdSeverity As New AAMS.bizETrackerHelpDesk.bzPTRSeverity
                objOutputXml = New XmlDocument
                objOutputXml = objHdSeverity.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_SEVERITY")
                    drpDownList.DataTextField = "HD_PTR_SEV_NAME"
                    drpDownList.DataValueField = "HD_PTR_SEV_ID"
                    drpDownList.DataBind()
                End If

            Case "ET_HDASSIGNEDTO"
                Dim objHdPtrAssignree As New AAMS.bizETrackerHelpDesk.bzPTRAssignee
                objOutputXml = New XmlDocument
                objOutputXml = objHdPtrAssignree.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_ASSIGNEE")
                    drpDownList.DataTextField = "ASSIGNEE_NAME"
                    drpDownList.DataValueField = "ASSIGNEEID"
                    drpDownList.DataBind()
                End If


            Case "ET_HDPTRTYPECAT"
                Dim objHdPtrTypeCat As New AAMS.bizETrackerHelpDesk.bzPTRTypeCategory
                objOutputXml = New XmlDocument
                objOutputXml = objHdPtrTypeCat.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_CATEGORIES")
                    drpDownList.DataTextField = "HD_PTR_TYCAT_NAME"
                    drpDownList.DataValueField = "HD_PTR_TYCAT_ID"
                    drpDownList.DataBind()
                End If


            Case "ET_QuerySubGroup"
                Dim objCallSubGroup As New AAMS.bizETrackerHelpDesk.bzCallSubGroup
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT>")
                objOutputXml = objCallSubGroup.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_SUB_GROUP")
                    drpDownList.DataTextField = "CALL_SUB_GROUP_NAME"
                    drpDownList.DataValueField = "CALL_SUB_GROUP_ID"
                    drpDownList.DataBind()
                End If


            Case "ET_TQUERYSUBGROUP"
                Dim objCallSubGroup As New AAMS.bizETrackerHelpDesk.bzCallSubGroup
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>2</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT>")
                objOutputXml = objCallSubGroup.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_SUB_GROUP")
                    drpDownList.DataTextField = "CALL_SUB_GROUP_NAME"
                    drpDownList.DataValueField = "CALL_SUB_GROUP_ID"
                    drpDownList.DataBind()
                End If


            Case "ET_QuerySubGroup"
                Dim objCallSubGroup As New AAMS.bizETrackerHelpDesk.bzCallSubGroup
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT>")
                objOutputXml = objCallSubGroup.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_SUB_GROUP")
                    drpDownList.DataTextField = "CALL_SUB_GROUP_NAME"
                    drpDownList.DataValueField = "CALL_SUB_GROUP_ID"
                    drpDownList.DataBind()
                End If


            Case "ET_WorkOrderType"
                Dim objWOType As New AAMS.bizETrackerHelpDesk.bzWOType
                objOutputXml = New XmlDocument

                objOutputXml = objWOType.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("WO_TYPE")
                    drpDownList.DataTextField = "WO_TYPE_NAME"
                    drpDownList.DataValueField = "WO_TYPE_ID"
                    drpDownList.DataBind()
                End If


            Case "ET_WorkOrderSeverity"
                Dim objWOSeverities As New AAMS.bizETrackerHelpDesk.bzWOSeverities
                objOutputXml = New XmlDocument

                objOutputXml = objWOSeverities.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("WO_SEVERITIES")
                    drpDownList.DataTextField = "WO_SEVERITY_NAME"
                    drpDownList.DataValueField = "WO_SEVERITY_ID"
                    drpDownList.DataBind()
                End If

            Case "ET_WorkOrderAssignee"
                Dim objWOAssignees As New AAMS.bizETrackerHelpDesk.bzWOAssignees
                objOutputXml = New XmlDocument
                objOutputXml = objWOAssignees.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("WO_ASSIGNEES")
                    drpDownList.DataTextField = "WO_ASSIGNEE_NAME"
                    drpDownList.DataValueField = "WO_ASSIGNEE_ID"
                    drpDownList.DataBind()
                End If


            Case "ET_QueryStatus"
                Dim objCallStatus As New AAMS.bizETrackerHelpDesk.bzCallStatus
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSTATUS_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSTATUS_INPUT>")
                objOutputXml = objCallStatus.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("CALL_STATUS")
                    drpDownList.DataTextField = "HD_STATUS_NAME"
                    drpDownList.DataValueField = "HD_STATUS_ID"
                    drpDownList.DataBind()
                End If


            Case "ET_QUERYPRIORITY"
                Dim objSeverities As New AAMS.bizETrackerHelpDesk.bzSeverities
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
                objOutputXml = objSeverities.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("SEVERITIES")
                    drpDownList.DataTextField = "HD_SEVERITY_NAME"
                    drpDownList.DataValueField = "HD_SEVERITY_ID"
                    drpDownList.DataBind()
                End If


            Case "ET_TEAM"
                Dim objbzTeam As New AAMS.bizETrackerHelpDesk.bzTeam
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
                objOutputXml = objbzTeam.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("TEAM")
                    drpDownList.DataTextField = "TEAM_NAME"
                    drpDownList.DataValueField = "TEAM_ID"
                    drpDownList.DataBind()
                End If


            Case "ET_QueryStatusWithClosed"
                Dim objCallStatus As New AAMS.bizETrackerHelpDesk.bzCallStatus
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                Dim objNodeList As XmlNodeList
                objInXml.LoadXml("<HD_LISTCALLSTATUS_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID></HD_LISTCALLSTATUS_INPUT>")
                objOutputXml = objCallStatus.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objNodeList = objOutputXml.DocumentElement.SelectNodes("CALL_STATUS")
                    For Each objNode As XmlNode In objNodeList
                        Dim li As New ListItem(objNode.Attributes("HD_STATUS_NAME").Value, objNode.Attributes("HD_STATUS_ID").Value & "|" & objNode.Attributes("HD_STATUS_CLOSE").Value)
                        drpDownList.Items.Add(li)
                    Next
                    'objXmlReader = New XmlNodeReader(objOutputXml)
                    'ds.ReadXml(objXmlReader)
                    'drpDownList.DataSource = ds.Tables("CALL_STATUS")
                    'drpDownList.DataTextField = "HD_STATUS_NAME"
                    'drpDownList.DataValueField = "HD_STATUS_ID" & "|" & "HD_STATUS_CLOSE"
                    'drpDownList.DataBind()
                End If




            Case "ET_TSTATUSWITHCLOSED"
                Dim objCallStatus As New AAMS.bizETrackerHelpDesk.bzCallStatus
                objOutputXml = New XmlDocument
                Dim objInXml As New XmlDocument
                Dim objNodeList As XmlNodeList
                objInXml.LoadXml("<HD_LISTCALLSTATUS_INPUT><HD_QUERY_GROUP_ID>2</HD_QUERY_GROUP_ID></HD_LISTCALLSTATUS_INPUT>")
                objOutputXml = objCallStatus.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objNodeList = objOutputXml.DocumentElement.SelectNodes("CALL_STATUS")
                    For Each objNode As XmlNode In objNodeList
                        Dim li As New ListItem(objNode.Attributes("HD_STATUS_NAME").Value, objNode.Attributes("HD_STATUS_ID").Value & "|" & objNode.Attributes("HD_STATUS_CLOSE").Value)
                        drpDownList.Items.Add(li)
                    Next
                    'objXmlReader = New XmlNodeReader(objOutputXml)
                    'ds.ReadXml(objXmlReader)
                    'drpDownList.DataSource = ds.Tables("CALL_STATUS")
                    'drpDownList.DataTextField = "HD_STATUS_NAME"
                    'drpDownList.DataValueField = "HD_STATUS_ID" & "|" & "HD_STATUS_CLOSE"
                    'drpDownList.DataBind()
                End If



            Case "ET_TPRIORITY"
                Dim objInXml As New XmlDocument
                objInXml.LoadXml("<HD_LISTCALLSUBGROUP_INPUT><HD_QUERY_GROUP_ID>2</HD_QUERY_GROUP_ID></HD_LISTCALLSUBGROUP_INPUT> ")
                Dim objbzCallSeverity As New AAMS.bizETrackerHelpDesk.bzSeverities
                objOutputXml = New XmlDocument
                objOutputXml = objbzCallSeverity.List1(objInXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("SEVERITIES")
                    drpDownList.DataTextField = "HD_SEVERITY_NAME"
                    drpDownList.DataValueField = "HD_SEVERITY_ID"
                    drpDownList.DataBind()
                End If




            Case "ET_HDIRSTATUSFORMANAGE"
                Dim objIRStatus As New AAMS.bizETrackerHelpDesk.bzIRStatus
                objOutputXml = New XmlDocument
                objOutputXml = objIRStatus.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    Dim Node As XmlNode
                    Dim nodeLists As XmlNodeList
                    Dim outerXmlSet As New XmlDocument
                    ' Dim strOuteXml As String = objOutputXml.ReplaceChild(.InnerXml
                    nodeLists = objOutputXml.DocumentElement.SelectNodes("IR_STATUS")
                    For Each Node In nodeLists
                        Dim NodeOld As XmlNode
                        NodeOld = Node
                        Node.Attributes("HD_STATUS_ID").Value = Node.Attributes("HD_STATUS_ID").Value.Trim() + "|" + Node.Attributes("HD_STATUS_CLOSE").Value.Trim()
                        objOutputXml.DocumentElement.ReplaceChild(Node, NodeOld)
                    Next

                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("IR_STATUS")
                    drpDownList.DataTextField = "HD_STATUS_NAME"
                    drpDownList.DataValueField = "HD_STATUS_ID"
                    drpDownList.DataBind()
                End If

            Case "ET_HDPTRSTATUSFORMANAGE"
                Dim objPTRStatus As New AAMS.bizETrackerHelpDesk.bzPTRStatus
                objOutputXml = New XmlDocument
                objOutputXml = objPTRStatus.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    Dim Node As XmlNode
                    Dim nodeLists As XmlNodeList
                    Dim outerXmlSet As New XmlDocument
                    ' Dim strOuteXml As String = objOutputXml.ReplaceChild(.InnerXml
                    nodeLists = objOutputXml.DocumentElement.SelectNodes("PTR_STATUS")
                    For Each Node In nodeLists
                        Dim NodeOld As XmlNode
                        NodeOld = Node
                        Node.Attributes("HD_STATUS_ID").Value = Node.Attributes("HD_STATUS_ID").Value.Trim() + "|" + Node.Attributes("HD_STATUS_CLOSE").Value.Trim()
                        objOutputXml.DocumentElement.ReplaceChild(Node, NodeOld)
                    Next

                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PTR_STATUS")
                    drpDownList.DataTextField = "HD_STATUS_NAME"
                    drpDownList.DataValueField = "HD_STATUS_ID"
                    drpDownList.DataBind()
                End If
            Case "ET_HDQUESTIONSTATUS"
                Dim objFeedback As New AAMS.bizETrackerHelpDesk.bzFeedback
                objOutputXml = New XmlDocument
                objOutputXml = objFeedback.ListFeedbackStatus()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("STATUS")
                    drpDownList.DataTextField = "STATUS_NAME"
                    drpDownList.DataValueField = "STATUS_ID"
                    drpDownList.DataBind()
                End If
            Case "ET_ASSIGNEDTOEMPLOYEE"
                Dim objbzCall As New AAMS.bizETrackerHelpDesk.bzCall
                Dim objInputXml As New XmlDocument
                objOutputXml = objbzCall.ListAssignedToEmployee
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("EMPLOYEE")
                    drpDownList.DataTextField = "Employee_Name"
                    drpDownList.DataValueField = "EmployeeID"
                    drpDownList.DataBind()
                End If


                '################################################################
                'End of Code Added by Ashish 
                '################################################################

            Case "STAFFDESIGNATION"
                Dim objbzStaffDesignation As New AAMS.bizTravelAgency.bzStaffDesignation
                objOutputXml = New XmlDocument
                objOutputXml = objbzStaffDesignation.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("Designation")
                    drpDownList.DataTextField = "Designation"
                    drpDownList.DataValueField = "DesignationID"
                    drpDownList.DataBind()
                End If
            Case "DEPARTMENTSPECIFIC"
                Dim objbzDeptSpec As New AAMS.bizSales.bzBusinessDevelopmentDepartment
                objOutputXml = New XmlDocument
                objOutputXml = objbzDeptSpec.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("DEPT")
                    drpDownList.DataTextField = "DEPARTMENT_NAME"
                    drpDownList.DataValueField = "ID"
                    drpDownList.DataBind()
                End If

            Case "IATASTATUS"

                Dim objbzAgencyGroup As New AAMS.bizMaster.bzAgencyGroup
                objOutputXml = New XmlDocument
                objOutputXml = objbzAgencyGroup.ListPriority()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    Dim Node As XmlNode
                    Dim nodeLists As XmlNodeList
                    Dim outerXmlSet As New XmlDocument

                    nodeLists = objOutputXml.DocumentElement.SelectNodes("PRIORITY")
                    For Each Node In nodeLists
                        Dim NodeOld As XmlNode
                        NodeOld = Node
                        Node.Attributes("PriorityID").Value = Node.Attributes("PriorityID").Value.Trim() + "|" + Node.Attributes("StatusType").Value.Trim()
                        objOutputXml.DocumentElement.ReplaceChild(Node, NodeOld)
                    Next

                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("PRIORITY")
                    drpDownList.DataTextField = "PriorityName"
                    drpDownList.DataValueField = "PriorityID"
                    drpDownList.DataBind()
                End If

        End Select
        'If bolSelect = True Then
        '    drpDownList.Items.Insert(0, New ListItem("Select One", ""))
        'End If
        If SelectOption = 1 Then
            drpDownList.Items.Insert(0, New ListItem("--Select One--", "")) ' In Search/New/Edit Form For Mandatory Field
        ElseIf SelectOption = 2 Then
            drpDownList.Items.Insert(0, New ListItem("", "")) 'In Edit/New Form For Non Mandatory Field
        ElseIf SelectOption = 3 Then
            drpDownList.Items.Insert(0, New ListItem("--All--", "")) ' In Search Form For Non Mandatory Field
        ElseIf SelectOption = 4 Then

        End If

    End Sub
    Public Function CheckSession() As String
        Dim loginScript As String = ""
        loginScript = " <noscript><meta http-equiv='REFRESH' content='1; URL=../Information.aspx' /> </noscript><script language='javascript'>" & _
       "parent.location.href='../SupportPages/TimeOutSession.aspx?Logout=True'; " & _
       "</script>"
        Return loginScript
    End Function
    Public Function ConvertDate(ByVal intDate As String) As Date
        Try
            Dim dtDateFrom As New Date(Left(CType(intDate, String), 4), Mid(CType(intDate, String), 5, 2), Right(CType(intDate, String), 2))
            Return dtDateFrom
        Catch ex As Exception
            Return CDate("1/1/1900")
        End Try
    End Function

    Public Function ConvertTextDate(ByVal dt As String) As String '''''''to convert dd/mm/yyyy to yyyymmdd
        Dim lstrarrDate As Array
        Dim lstrdtyear As String
        Dim lstrdtmonth As String
        Dim lstrdtday As String
        Dim lstrDate As String
        If dt <> "" Then
            lstrarrDate = Split(dt, "/", -1, 1)
            lstrdtyear = lstrarrDate(2)
            lstrdtmonth = lstrarrDate(1)
            lstrdtday = lstrarrDate(0)
            If CType(lstrarrDate(1), String).Length = 1 Then
                lstrdtmonth = "0" + lstrarrDate(1)
            Else
                lstrdtmonth = lstrarrDate(1)
            End If
            If CType(lstrarrDate(0), String).Length = 1 Then
                lstrdtday = "0" + lstrarrDate(0)
            Else
                lstrdtday = lstrarrDate(0)
            End If

            lstrDate = lstrdtyear & lstrdtmonth & lstrdtday
            Return lstrDate
        Else
            Return dt
        End If
    End Function
    Public Sub ExpirePageCache()
        'HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'HttpContext.Current.Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        'HttpContext.Current.Response.Cache.SetLastModified(DateTime.Now)
        'HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(False)
    End Sub
    Public Function SecurityCheck(ByVal intValue As Integer) As StringBuilder
        Dim builSecurity As New StringBuilder
        Dim ViewRight, AddRight, ModifyRight, DeleteRight, PrintRight As String
        ViewRight = 0 : AddRight = 0 : ModifyRight = 0 : DeleteRight = 0 : PrintRight = 0

        'Code for restricting user to log multiple times at a same time 

        If System.Configuration.ConfigurationManager.AppSettings("MULTIPLE_LOGIN") IsNot Nothing Then
            If System.Configuration.ConfigurationManager.AppSettings("MULTIPLE_LOGIN").ToString = "1" Then
                GoTo StartFromHere
            End If
            Dim str As String = RestrictMultiUserByMultiIp()
            If str <> "True" Then
                HttpContext.Current.Session("RestrictedUser") = "Yes"
                builSecurity.Append(ViewRight)
                'Index 1 Add
                builSecurity.Append(AddRight)
                'Index 2 Modify
                builSecurity.Append(ModifyRight)
                'Index 3 Delete
                builSecurity.Append(DeleteRight)
                'Index 4 Print
                builSecurity.Append(PrintRight)
                Return builSecurity
                Exit Function
            Else
                HttpContext.Current.Session("RestrictedUser") = "No"
            End If
        Else
            Dim str As String = RestrictMultiUserByMultiIp()
            If str <> "True" Then
                HttpContext.Current.Session("RestrictedUser") = "Yes"
                builSecurity.Append(ViewRight)
                'Index 1 Add
                builSecurity.Append(AddRight)
                'Index 2 Modify
                builSecurity.Append(ModifyRight)
                'Index 3 Delete
                builSecurity.Append(DeleteRight)
                'Index 4 Print
                builSecurity.Append(PrintRight)
                Return builSecurity
                Exit Function
            Else
                HttpContext.Current.Session("RestrictedUser") = "No"
            End If

            'End code

        End If


StartFromHere:

        Select Case intValue
            Case 1
                ViewRight = "1"
            Case 2, 3
                ViewRight = "1"
                AddRight = "1"
            Case 4, 5
                ViewRight = "1"
                ModifyRight = "1"
            Case 6, 7
                ViewRight = "1"
                ModifyRight = "1"
                AddRight = "1"
            Case 8, 9
                ViewRight = "1"
                DeleteRight = "1"
            Case 10, 11
                ViewRight = "1"
                DeleteRight = "1"
                AddRight = "1"
            Case 12, 13
                ViewRight = "1"
                DeleteRight = "1"
                ModifyRight = "1"
            Case 14, 15
                ViewRight = "1"
                DeleteRight = "1"
                ModifyRight = "1"
                AddRight = "1"
            Case 16, 17
                ViewRight = "1"
                PrintRight = "1"
            Case 18, 19
                ViewRight = "1"
                PrintRight = "1"
                AddRight = "1"
            Case 20, 21
                ViewRight = "1"
                PrintRight = "1"
                ModifyRight = "1"
            Case 22, 23
                ViewRight = "1"
                PrintRight = "1"
                AddRight = "1"
                ModifyRight = "1"
            Case 24, 25
                ViewRight = "1"
                PrintRight = "1"
                DeleteRight = "1"
            Case 26, 27
                ViewRight = "1"
                PrintRight = "1"
                DeleteRight = "1"
                AddRight = "1"
            Case 28, 29
                ViewRight = "1"
                PrintRight = "1"
                DeleteRight = "1"
                ModifyRight = "1"
            Case 30, 31
                ViewRight = "1"
                PrintRight = "1"
                DeleteRight = "1"
                ModifyRight = "1"
                AddRight = "1"
        End Select
       
        If HttpContext.Current.Session("LoginSession").ToString().Split("|").GetValue(1).ToString() = "1" Then
            builSecurity.Append(1)
            builSecurity.Append(1)
            builSecurity.Append(1)
            builSecurity.Append(1)
            builSecurity.Append(1)
            Return builSecurity
        Else
            'Index 0 View
            builSecurity.Append(ViewRight)
            'Index 1 Add
            builSecurity.Append(AddRight)
            'Index 2 Modify
            builSecurity.Append(ModifyRight)
            'Index 3 Delete
            builSecurity.Append(DeleteRight)
            'Index 4 Print
            builSecurity.Append(PrintRight)
            Return builSecurity
        End If
    End Function
    Public Function ConvertTextDateBlank(ByVal dt As String) As String '''''''to convert dd/mm/yyyy to yyyymmdd
        Dim lstrarrDate As Array
        If dt.Trim = "" Then
            Return dt
        End If
        lstrarrDate = Split(dt, "/", -1, 1)
        Dim lstrdtyear As String
        Dim lstrdtmonth As String
        Dim lstrdtday As String
        Dim lstrDate As String

        lstrdtyear = lstrarrDate(2)
        lstrdtmonth = lstrarrDate(1)
        lstrdtday = lstrarrDate(0)
        If CType(lstrarrDate(1), Integer) < 2 Then
            lstrdtmonth = "0" + lstrarrDate(1)
        Else
            lstrdtmonth = lstrarrDate(1)
        End If
        If CType(lstrarrDate(0), Integer) < 2 Then
            lstrdtday = "0" + lstrarrDate(0)
        Else
            lstrdtday = lstrarrDate(0)
        End If

        lstrDate = lstrdtyear & lstrdtmonth & lstrdtday
        Return lstrDate
    End Function
    Public Function ConvertDateBlank(ByVal intDate As String) As String
        If intDate.Trim = "" Then
            Return intDate
        End If
        Try
            Dim dtDateFrom As New Date(Left(CType(intDate, String), 4), Mid(CType(intDate, String), 5, 2), Right(CType(intDate, String), 2))
            Return dtDateFrom.ToString("dd/MM/yyyy")
        Catch ex As Exception
            Return CDate("1/1/1900")
        End Try
    End Function
    Public Function Limited_To_OwnAgency(ByVal strInput As String) As String
        Dim str As String = ""
        Dim objInXml As New XmlDocument
        objInXml.LoadXml(strInput)
        If objInXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText.ToUpper = "TRUE" Then
            str = objInXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText
        End If
        Return str
    End Function
    Public Function Limited_To_Aoffice(ByVal strInput As String) As String
        Dim str As String = ""
        Dim objInXml As New XmlDocument
        objInXml.LoadXml(strInput)
        If objInXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText.ToUpper = "TRUE" Then
            str = objInXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
        End If
        Return str
    End Function
    Public Function Limited_To_Region(ByVal strInput As String) As String
        Dim str As String = ""
        Dim objInXml As New XmlDocument
        objInXml.LoadXml(strInput)
        If objInXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper = "TRUE" Then
            str = objInXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
        End If
        Return str
    End Function
    Public Function AOffice(ByVal strInput As String) As String
        Dim str As String = ""
        Dim objInXml As New XmlDocument
        objInXml.LoadXml(strInput)
        str = objInXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
        Return str
    End Function
   
    Public Function EmployeePageName(ByVal strInput As String) As String
        Dim str As String = ""
        Dim strResult As String = ""
        Dim objInXml As New XmlDocument
        objInXml.LoadXml(strInput)
        str = objInXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='EmployeeEX']").Attributes("Value").Value
        Dim strBuilder As New StringBuilder
        If (objInXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            strBuilder = SecurityCheck(str)
            If strBuilder(0) = "0" Then
                str = objInXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Request User']").Attributes("Value").Value()
                strBuilder = New StringBuilder
                strBuilder = SecurityCheck(str)
                If strBuilder(0) = "1" Then
                    strResult = "MSSR_Employee.aspx" 'New form
                Else
                    str = objInXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee']").Attributes("Value").Value()
                    strBuilder = New StringBuilder
                    strBuilder = SecurityCheck(str)
                    If strBuilder(0) = "1" Then
                        strResult = "MSSR_EmployeeR.aspx" 'New form
                    End If

                End If
            Else
                strResult = "MSSR_Employee.aspx" 'Old form
            End If
        Else
            strResult = "MSSR_Employee.aspx" 'Old form
        End If
        Return strResult
    End Function

    'This function returns 1 or 0 according to permission (1 for Permission granted and 0 access denied)
    'This function shows user has a permission to open close LTR no or not
    'If value is One than user can open Closed LTR else Not
    Public Function REOPENCLOSEDLTR(ByVal strInput As String) As String
        Dim intStatus As Integer = 0
        Dim str As String = ""
        Dim objInXml As New XmlDocument
        objInXml.LoadXml(strInput)
        str = objInXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='REOPEN CLOSED LTR']").Attributes("Value").Value
        Dim strBuilder As New StringBuilder
        If (objInXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            strBuilder = SecurityCheck(str)
            If strBuilder(2) = "1" Then
                intStatus = 1
            End If
        Else
            intStatus = 1
        End If
        
        Return intStatus.ToString
    End Function
    Public Function REOPENCLOSEDBRLTR(ByVal strInput As String) As String
        Dim intStatus As Integer = 0
        Dim str As String = ""
        Dim objInXml As New XmlDocument
        objInXml.LoadXml(strInput)
        str = objInXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR REOPEN CLOSED LTR']").Attributes("Value").Value
        Dim strBuilder As New StringBuilder
        If (objInXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            strBuilder = SecurityCheck(str)
            If strBuilder(2) = "1" Then
                intStatus = 1
            End If
        Else
            intStatus = 1
        End If

        Return intStatus.ToString
    End Function

    'This function returns 1 or 0 according to permission of view 
    Public Function ReturnViewPermission(ByVal strInput As String, ByVal SecurityOptionSubName As String) As String
        Dim intStatus As Integer = 0
        Dim str As String = ""
        Dim objInXml As New XmlDocument
        objInXml.LoadXml(strInput)
        str = objInXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='" + SecurityOptionSubName + "']").Attributes("Value").Value
        Dim strBuilder As New StringBuilder
        If (objInXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            strBuilder = SecurityCheck(str)
            If strBuilder(0) <> "0" Then
                intStatus = 1
            End If
        Else
            intStatus = 1
        End If

        Return intStatus.ToString
    End Function

 'This function returns SecurityRegionID when SecurityOptionSubName='CHALLANREGIONWISEGODOWN' has value "1" or "2"  else return "-100"
    Public Function ChallanRegionWiseGodown(ByVal strInput As String)

        Dim str As String = ""
        Dim objInXml As New XmlDocument
        objInXml.LoadXml(strInput)
        'str = objInXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CHALLANREGIONWISEGODOWN']").Attributes("Value").Value
        If str = "1" Or str = "2" Then
            str = objInXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
        Else
            str = "-100"
        End If
        Return str
    End Function

    Public Function SecurityRegionID(ByVal strInput As String)

        Dim str As String = ""
        Dim objInXml As New XmlDocument
        objInXml.LoadXml(strInput)
        If objInXml.DocumentElement.SelectSingleNode("SecurityRegionID") IsNot Nothing Then
            str = objInXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
        End If
        Return str
    End Function

    Public Function OVERRIDE_ORDER_NO(ByVal strInput As String) As String
        Dim str As String = "False"
        Dim objInXml As New XmlDocument
        objInXml.LoadXml(strInput)
        str = objInXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OVERRIDE_ORDER_NO']").Attributes("Value").Value
        If str = "0" Then
            str = "False"
        Else
            str = "True"
        End If
        Return str
    End Function

Public Function EmployeeID(ByVal strInput As String) As String
        Dim str As String = ""
        Dim objInXml As New XmlDocument
        objInXml.LoadXml(strInput)
        str = objInXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText
        Return str
    End Function

    Public Function EmployeeName(ByVal strInput As String) As String
        Dim str As String = ""
        Dim objInXml As New XmlDocument
        objInXml.LoadXml(strInput)
        str = objInXml.DocumentElement.SelectSingleNode("Employee_Name").InnerText
        Return str
    End Function

    'For Example GetDateFormat(txtdate.Text, "dd/MM/yyyy", "yyyyMMdd", "/")
    Public Function GetDateFormat(ByVal objDate As Object, ByVal dateInFormat As String, ByVal dateOutFormat As String, ByVal dateSepChar As String) As String
        Dim str As String = ""
        If objDate.Trim = "" Then
        Else
            Try
                If dateInFormat.Equals("yyyyMMdd") Then
                    str = DateTime.ParseExact(objDate, dateInFormat, System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat).ToString(dateOutFormat)
                Else
                    Dim ln As Integer = objDate.ToString().Length
                    If ln = 8 And dateInFormat.Equals("dd/MM/yyyy") Then
                        dateInFormat = "dd/MM/yy"
                    End If
                    Dim dt As New DateTime()
                    Dim dtfi As New DateTimeFormatInfo
                    dtfi.ShortDatePattern = dateInFormat
                    dtfi.DateSeparator = dateSepChar
                    dt = Convert.ToDateTime(objDate, dtfi)
                    str = dt.ToString(dateOutFormat)
                End If

            Catch ex As Exception
                str = "0"
            End Try
        End If
        Return str
    End Function
    Public Sub BindTrainer(ByVal drpDownList As DropDownList, ByVal strAOffice As String, Optional ByVal SelectOption As Integer = 1)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
        objInputXml.LoadXml("<MS_SEARCHEMPLOYEE_INPUT><Employee_Name></Employee_Name><DepartmentID></DepartmentID><Aoffice></Aoffice> <Designation></Designation><SecurityOptionID></SecurityOptionID><AgreementSigned></AgreementSigned><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><SecurityRegionID></SecurityRegionID><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHEMPLOYEE_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("DepartmentID").InnerText = "31"
        objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = strAOffice
        'Here Back end Method Call
        objOutputXml = objbzEmployee.Search(objInputXml)
        drpDownList.Items.Clear()
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            drpDownList.DataSource = ds.Tables("Employee")
            drpDownList.DataTextField = "Employee_Name"
            drpDownList.DataValueField = "EmployeeID"
            drpDownList.DataBind()
        End If
        If SelectOption = 1 Then
            drpDownList.Items.Insert(0, New ListItem("--Select One--", "")) ' In Search/New/Edit Form For Mandatory Field
        ElseIf SelectOption = 2 Then
            drpDownList.Items.Insert(0, New ListItem("", "")) 'In Edit/New Form For Non Mandatory Field
        ElseIf SelectOption = 3 Then
            drpDownList.Items.Insert(0, New ListItem("--All--", "")) ' In Search Form For Non Mandatory Field
        End If
    End Sub

    Public Function RestrictMultiUserByMultiIp() As String
        Dim strReturnExistedUser As String = ""
        Dim strIp As String
        strIp = HttpContext.Current.Request.Params("HTTP_X_FORWARDED_FOR")
        If strIp = "" Or strIp Is Nothing Then
            strIp = HttpContext.Current.Request.UserHostAddress
        End If


        Dim dtUserApplicationDetails As New DataTable
        dtUserApplicationDetails = CType(HttpContext.Current.Application("dtUserApplicationDetails"), DataTable)
        If dtUserApplicationDetails IsNot Nothing Then
            Dim dr As DataRow = dtUserApplicationDetails.Rows.Find(HttpContext.Current.Session("UserId"))
            If dr IsNot Nothing Then
                If dr("IPAddress") <> strIp Then
                    strReturnExistedUser = "False"
                Else
                    strReturnExistedUser = "True"
                End If

            Else
                strReturnExistedUser = "True"
            End If
        Else
            strReturnExistedUser = "True"
        End If



        Return strReturnExistedUser
    End Function
End Class

