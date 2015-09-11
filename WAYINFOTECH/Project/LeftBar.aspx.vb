Imports System.Xml
Partial Class LeftBar
    Inherits System.Web.UI.Page
    Public SetPage As String
    Dim objeAAMS As New eAAMS
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            objeAAMS.ExpirePageCache()
            If Session("Security") IsNot Nothing Then
                If Not Page.IsPostBack Then
                    SetPage = CreatePage().ToString()
                    ltLeftbar.Text = SetPage
                End If
                SetModule()
            Else
                Response.Redirect("SupportPages/TimeOutSession.aspx?LogOut=Logout")
            End If


        Catch ex As Exception

        End Try
    End Sub
    Sub SetModule()
        Dim objSessionXml As New XmlDocument
        objSessionXml.LoadXml(Session("Security").ToString())
        Dim strModuleID As String
        strModuleID = objSessionXml.DocumentElement.SelectSingleNode("DisplayFirstForm/FirstFormDetails").Attributes("ID").Value
        If strModuleID <> "" Then
            ClientScript.RegisterStartupScript(Me.GetType, "strMainKey", "<script>GetData('" + strModuleID + "');</script>")
        End If
    End Sub
    Protected Function CreatePage() As StringBuilder

        Dim strPage As New StringBuilder
        Dim objIxml As New XmlDocument
        Dim objOxml As New XmlDocument
        Dim objSessionXml As New XmlDocument
        'Dim objSecNodeList, objItemNodeList As XmlNodeList
        'Dim objxmldocfrag As XmlDocumentFragment
        'Load xml for security
        Try
            objSessionXml.LoadXml(Session("Security").ToString())
            'Load xml for Menu
            objIxml.Load(Server.MapPath("~/App_Data/LeftBar.xml"))


            'objSecNodeList = objIxml.DocumentElement.SelectNodes("HEADING")
            'For Each objXmlNode As XmlNode In objSecNodeList
            '    objItemNodeList = objXmlNode.SelectNodes("Item")
            '    For Each objNodeItem As XmlNode In objItemNodeList
            '        If objNodeItem.Attributes("NAME").Value <> "Logout" Then
            '            objXmlNode.RemoveChild(objNodeItem)
            '        End If
            '    Next
            'Next
            'objOxml.Load(Server.MapPath("~/App_Data/LeftBar.xml"))

            'objSecNodeList = objOxml.DocumentElement.SelectNodes("HEADING")
            'For Each objXmlNode As XmlNode In objSecNodeList
            '    objItemNodeList = objXmlNode.SelectNodes("Item")
            '    For Each objNodeItem As XmlNode In objItemNodeList

            '        If objSessionXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='" & objNodeItem.Attributes("NAME").Value & "']").Count <> 0 Then
            '            If objSessionXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='" & objNodeItem.Attributes("NAME").Value & "']").Attributes("Value").Value <> "0" Then
            '                objxmldocfrag = objIxml.CreateDocumentFragment()
            '                objxmldocfrag.InnerXml = objNodeItem.OuterXml
            '                objIxml.DocumentElement.SelectSingleNode("HEADING[@ID='" & objXmlNode.Attributes("ID").Value & "']").AppendChild(objxmldocfrag)
            '            End If
            '        End If
            '    Next
            'Next
            lblRowCount.Text = objIxml.DocumentElement.SelectNodes("HEADING").Count

            '  objIxml.DocumentElement.RemoveChild(objIxml.DocumentElement.SelectSingleNode("HEADING[@ID='2']"))
            Session("LeftBar") = ApplySecurity(objIxml)
            Dim xnode As XmlNode
            Dim xnodelist As XmlNodeList
            xnodelist = objIxml.DocumentElement.SelectNodes("HEADING")
            'strRowCount.Value = xnodelist.Count

            strPage.Append("<table   cellpadding='0' cellspacing='0'  style=' cursor: pointer'>")

            strPage.Append("<tr valign='top'>")
            strPage.Append("<td align='left' class='topMargin' >")
            'For Each xnode In xnodelist
            '    strPage.Append("<table onclick='GetData(" & xnode.Attributes("ID").Value & ")' border='0' cellpadding='0'  cellspacing='0'  style='width: 159px;cursor: pointer'>")
            '    strPage.Append("<tr valign='top'>")
            '    strPage.Append("<td align='left'  valign='middle' bgcolor='#dee7f7' width='10%'>")
            '    strPage.Append("&nbsp;<img src='Images/down.jpg' border='none' id='NavImg" & xnode.Attributes("ID").Value & "' /></td>")
            '    strPage.Append("<td align='left'  valign='top' class='menu' width='90%'>" & xnode.Attributes("NAME").Value & "</td>")
            '    strPage.Append("</tr>")
            '    strPage.Append("</table>")
            '    strPage.Append("<div style='display: none;width: 159px; valign='top' align='left' id='NavDiv" & xnode.Attributes("ID").Value & "'></div>")
            'Next
            For Each xnode In xnodelist
                'strPage.Append("<table onclick='GetData(" & xnode.Attributes("ID").Value & ")' border='0' cellpadding='0'  cellspacing='0'  style='width: 159px;cursor: pointer'>")
                strPage.Append("<div  onclick='GetData(" & xnode.Attributes("ID").Value & ");' class='moduleDivdown' id='NavImg" & xnode.Attributes("ID").Value & "' >")
                'strPage.Append("<tr valign='top'>")
                'strPage.Append("<td align='left'  valign='middle' bgcolor='#dee7f7' width='10%'>")
                ' strPage.Append("&nbsp;<img src='Images/down.jpg' border='none' id='NavImg" & xnode.Attributes("ID").Value & "' /></td>")
                strPage.Append(xnode.Attributes("NAME").Value)
                ' strPage.Append("</tr>")
                'strPage.Append("</table>")
                strPage.Append("</div>")
                strPage.Append("<div style='display: none;' class='leftbarDiv' id='NavDiv" & xnode.Attributes("ID").Value & "'></div>")
            Next
            strPage.Append("</td>")
            strPage.Append("</tr>")
            strPage.Append("</table>")
            Return strPage
        Catch ex As Exception
            Return strPage
        End Try
    End Function
    Function ApplySecurity(ByVal objInXmlDocument As XmlDocument) As String
        Try
            Dim objxmlDocument As New XmlDocument
            ' Dim strSetup() As String = {"Airline", "AirLine Office", "Aoffice", "City", "Country", "Crs", "Department", "Designation", "Employee", "IP Pool", "Region", "Security Region", "State", "Employee", "Agency Group", "Agency Details", "Agency Priority", "Agency Status", "Agency Type", "Corporate Codes", "Email Group", "Online Status", "Order Status", "Product Group", "Agency product", "Order Type Category", "Order Type", "Order", "Staff", "Agency Details", "OfficeId", "OfficeId Report", "AgencyTarget", "Isp", "IspPlan", "ISP Plan Report", "IspOrder", "IspFeasibilityStatus", "IspFeasibilityRequest", "IspFeasibilityRequest", "ISP Order Report", "ISP Payment Process", "ISP Payment Received", "Search BDR", "Log HD Call", "Contact Type", "Coordinator", "Customer Category", "Disposition", "Dynamic Report", "Feedback Action", "Feedback Report", "Search PTR", "PTR Assignee", "PTR Followup", "PTR Severity", "PTR Type", "PTR Type Category", "Query Category", "Query Priority", "Query Status", "Query Sub Category", "Query Sub group", "Team", "CallCategory", "CallSeverity", "CallStatus", "CallSubCategory", "CallSubGroup", "Work Order", "Work Order Assignee", "Work Order Followup", "Work Order Severity", "Work Order Type", "MANAGE GODOWN", "MANAGE SUPPLIER", "CHALLAN PURCHASEORDER", "MANAGE CHALLAN", "Stock Status", "CHALLAN STATUS", "Inventory Report", "WARRANTYSTATUS REPORT", "MANAGE SCRAP", "StockTransferTackingReport", "HardwareInstallationReport", "EquipmentMaster", "RPTFlash", "DailyBookings", "1A Productivity", "All CRS Productivity", "Group Productivity", "Travel Assistance Bookings", "Country Wise Report", "City Wise Report", "Region Wise Budget", "Country Wise Budget", "Airline Passive Report", "Airline Wise Market Share", "TA Productivity", "Market Share-Airline", "City Average Terminal", "City Productivity Comparision", "Airline Productivity Comparision", "Market Share", "AgencyAirlineBreakup"}
            Dim strSetup() As String = {"Airline", "AirLine Office", "Aoffice", "City", "Country", "Department", "Designation", "EmployeeEX", "IP Pool", "Region", "Security Region", "State", "Employee List"}
            Dim strTravelAgency() As String = {"Agency Group", "Agency Details", "IATA Status", "Agency Status", "Agency Type", "Corporate Codes", "Email Group", "Online Status", "Order Status", "Product Group", "Product", "Order Type Category", "Order Type", "Order", "Staff", "Agency Details", "OfficeId", "OfficeId Report", "Manage IPprovider", "Manage IPpop", "Manage IPPool", "Crs", "Send Email", "Connectivity Category", "Update 1A Resposibility", "Agency Staff designation"}
            Dim strISP() As String = {"IspProvider", "Isp", "IspPlan", "IspOrder", "IspOrderStatus", "IspFeasibilityStatus", "IspFeasibilityRequest", "ISP Order Report", "ISP Payment Process", "ISP Payment Received", "IspPendingCancellation"}
          
            'Dim strHelpDesk() As String = {"BDRLetter", "Log HD Call", "Log HD Call", "Log Tech Call", "Contact Type", "Coordinator", "Customer Category", "Disposition", "Dynamic Report", "Feedback Action", "FeedBackGraph", "Feedback Report", "PTR", "PTR Assignee", "PTR Followup", "PTR Severity", "PTR Type", "PTR Type Category", "Call Query Category", "Call Query Priority", "Call Query Status", "Call Query SubCategory", "Call Query Subgroup", "Team", "Tech Query Category", "Tech Query Priority", "Tech Query Status", "Tech Query SubCategory", "Tech Query SubGroup", "Work Order", "Work Order Assignee", "Work Order Followup", "Work Order Severity", "Work Order Type", "Feedback Questionset", "Technical Report", "PTR Status"}

            Dim strHelpDesk() As String = {"BDRLetter", "Log HD Call", "Log HD Call", "Log Tech Call", "Contact Type", "Coordinator", "Customer Category", "Disposition", "Dynamic Report", "Feedback Action", "FeedBackGraph", "Feedback Report", "PTR", "PTR Assignee", "PTR Followup", "PTR Severity", "PTR Type", "PTR Type Category", "PTR Status", "Call Query Category", "Call Query Priority", "Call Query Status", "Call Query SubCategory", "Call Query Subgroup", "Team", "Tech Query Category", "Tech Query Priority", "Tech Query Status", "Tech Query SubCategory", "Tech Query SubGroup", "Work Order", "Work Order Assignee", "Work Order Followup", "Work Order Severity", "Work Order Type", "Feedback Questionset", "Technical Report", "IR", "IR Assignee", "IR Followup", "IR Severity", "IR Type", "IR Type Category", "IR Status"}

            Dim strInventory() As String = {"MANAGE GODOWN", "MANAGE SUPPLIER", "CHALLAN PURCHASEORDER", "MANAGE CHALLAN", "Stock Status", "CHALLAN STATUS", "Inventory Report", "WARRANTYSTATUS REPORT", "MANAGE SCRAP", "StockTransferTackingReport", "HardwareInstallationReport", "EquipmentMaster", "Flash Report", "Equipment Category"}
            Dim strProductivity() As String = {"DailyBookings", "Airlinewise Dailybookings", "1A Productivity", "All CRS Productivity", "Group Productivity", "Travel Assistance Bookings", "Country Wise Report", "City Wise Report", "Region Wise Budget", "Country Wise Budget", "1A Ticket"}
            Dim strMarket() As String = {"Airline Passive Report", "Airline Wise Market Share", "TA Productivity", "Market Share-Airline", "City Average Terminal", "City Productivity Comparision", "Airline Productivity Comparision", "Market Share Report", "Market Share", "AgencyAirlineBreakup", "Country Manager Report"}
            Dim strTraining() As String = {"Course", "Course Session", "Participant Basket", "Training Room", "Feedback Domain", "Feedback Topic", "Test Question", "Training FeedBack", "Course Level", "FeedBack Status", "Participant Status", "Staff Training", "1A Staff Training", "Monthly Training", "Pending Training", "Request for Training", "Objective Monitoring", "Room Performance", "Weekly Training", "ReTraining"}

            Dim strSales() As String = {"Sales Objective", "Agency Target", "DSR Logging", "Account Management", "Servicecall Status", "Retention Reason", "Retention Status", "Target Status", "Air and Non Air Status", "DSR ACTION", "Back Date Entry on DSR Visit"}
            'Dim strSales() As String = {"AgencyOrder"}
            Dim strBRHelpDesk() As String = {"BR BDRLetter", "BR Log HD Call", "BR Log HD Call", "BR Log Tech Call", "BR Contact Type", "BR Coordinator", "BR Customer Category", "BR Disposition", "BR Dynamic Report", "BR Feedback Action", "BR FeedBackGraph", "BR Feedback Report", "BR PTR", "BR PTR Assignee", "BR PTR Followup", "BR PTR Severity", "BR PTR Type", "BR PTR Type Category", "BR Call Query Category", "BR Call Query Priority", "BR Call Query Status", "BR Call Query SubCategory", "BR Call Query Subgroup", "BR Team", "BR Tech Query Category", "BR Tech Query Priority", "BR Tech Query Status", "BR Tech Query SubCategory", "BR Tech Query SubGroup", "BR Work Order", "BR Work Order Assignee", "BR Work Order Followup", "BR Work Order Severity", "BR Work Order Type", "BR Feedback Questionset", "BR Technical Report"}

            Dim strIncentive() As String = {"Approval Level", "Business Case", "Approval Queue", "Payment Approval Level", "Incentive Payment Process", "Payment Approval Queue", "Incentive Payment Received", "IncentivePaidReport", "VolumeClassification"}

            Dim strBackOfficeHelpdesk() As String = {"BO BDRLetter", "BO Log HD Call", "BO Log HD Call", "BO Log Tech Call", "BO Contact Type", "BO Coordinator", "BO Customer Category", "BO Disposition", "BO Dynamic Report", "BO Feedback Action", "BO FeedBackGraph", "BO Feedback Report", "BO PTR", "BO PTR Assignee", "BO PTR Followup", "BO PTR Severity", "BO PTR Type", "BO PTR Type Category", "BO PTR Status", "BO Call Query Category", "BO Call Query Priority", "BO Call Query Status", "BO Call Query SubCategory", "BO Call Query Subgroup", "BO Team", "BO Tech Query Category", "BO Tech Query Priority", "BO Tech Query Status", "BO Tech Query SubCategory", "BO Tech Query SubGroup", "BO Work Order", "BO Work Order Assignee", "BO Work Order Followup", "BO Work Order Severity", "BO Work Order Type", "BO Feedback Questionset", "BO Technical Report", "BO IR", "BO IR Assignee", "BO IR Followup", "BO IR Severity", "BO IR Type", "BO IR Type Category", "BO IR Status"}
            Dim strETrackerHelpdesk() As String = {"ET BDRLetter", "ET Log HD Call", "ET Log HD Call", "ET Log Tech Call", "ET Contact Type", "ET Coordinator", "ET Customer Category", "ET Disposition", "ET Dynamic Report", "ET Feedback Action", "ET FeedBackGraph", "ET Feedback Report", "ET PTR", "ET PTR Assignee", "ET PTR Followup", "ET PTR Severity", "ET PTR Type", "ET PTR Type Category", "ET PTR Status", "ET Call Query Category", "ET Call Query Priority", "ET Call Query Status", "ET Call Query SubCategory", "ET Call Query Subgroup", "ET Team", "ET Tech Query Category", "ET Tech Query Priority", "ET Tech Query Status", "ET Tech Query SubCategory", "ET Tech Query SubGroup", "ET Work Order", "ET Work Order Assignee", "ET Work Order Followup", "ET Work Order Severity", "ET Work Order Type", "ET Feedback Questionset", "ET Technical Report", "ET IR", "ET IR Assignee", "ET IR Followup", "ET IR Severity", "ET IR Type", "ET IR Type Category", "ET IR Status"}

         
            Dim intstatus As Integer = 0

            objxmlDocument.LoadXml(Session("Security"))
            If objxmlDocument.DocumentElement.SelectSingleNode("Administrator").InnerText = "0" Then


                Dim objnode As XmlNode
                Dim objTempnode As XmlNode
                objnode = objxmlDocument.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Change Password']")
                If objnode.Attributes("Value").Value = "0" Then
                    objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='1']").RemoveChild(objInXmlDocument.DocumentElement.SelectSingleNode("HEADING/Item [@ID='1']"))
                End If

                'Start For SetUp Module
                For i As Integer = 1 To strSetup.Length
                    'Employee
                    Dim strSecurityOptionSubNameEmployee As String = "EmployeeEX"
                    objnode = objxmlDocument.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='" + strSetup(i - 1) + "']")
                    If objnode Is Nothing Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='2']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + i.ToString + "']")
                        If objDeleteNode IsNot Nothing Then
                            objTempnode.RemoveChild(objDeleteNode)
                        End If
                        Continue For
                    End If
                    If objnode.Attributes("Value").Value = "0" Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='2']")
                        Dim objDeleteNodeEmployee As XmlNode
                        objDeleteNodeEmployee = objTempnode.SelectSingleNode("Item[@ID='" + i.ToString + "']")
                        If objDeleteNodeEmployee IsNot Nothing Then
                            If strSecurityOptionSubNameEmployee = strSetup(i - 1) Then
                                objnode = objxmlDocument.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Request User']")
                            End If
                            If objnode Is Nothing Then
                                objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='2']")
                                Dim objDeleteNodeEmployee1 As XmlNode
                                objDeleteNodeEmployee1 = objTempnode.SelectSingleNode("Item[@ID='" + i.ToString + "']")
                                If objDeleteNodeEmployee1 IsNot Nothing Then
                                    objTempnode.RemoveChild(objDeleteNodeEmployee1)
                                End If
                                Continue For
                            End If
                            If objnode.Attributes("Value").Value = "0" Then
                                objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='2']")
                                Dim objDeleteNode2 As XmlNode
                                'for checking employee rights because previously Employee is converted into EmployeeEX
                                '30 Mar 09
                                If strSecurityOptionSubNameEmployee = strSetup(i - 1) Then
                                    objnode = objxmlDocument.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee']")
                                End If
                                'objnode = objxmlDocument.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee']")
                                If objnode Is Nothing Then
                                    Dim objDeleteNodeEmployee1 As XmlNode
                                    objDeleteNodeEmployee1 = objTempnode.SelectSingleNode("Item[@ID='" + i.ToString + "']")
                                    If objDeleteNodeEmployee1 IsNot Nothing Then
                                        objTempnode.RemoveChild(objDeleteNodeEmployee1)
                                    End If
                                    Continue For
                               
                                End If
                                If objnode.Attributes("Value").Value = "0" Then
                                    objDeleteNode2 = objTempnode.SelectSingleNode("Item[@ID='" + i.ToString + "']")
                                    objTempnode.RemoveChild(objDeleteNodeEmployee)
                                Else
                                    intstatus = 1
                                End If
                            Else
                                intstatus = 1
                            End If
                            'objTempnode.RemoveChild(objDeleteNode)
                        End If
                    Else
                        intstatus = 1
                    End If
                Next
                'For Setup Module
                If intstatus = 0 Then
                    objInXmlDocument.DocumentElement.RemoveChild(objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='2']"))
                End If

                'End Setup Module

                'Start For TravelAgency Module
                intstatus = 0
                For j As Integer = 1 To strTravelAgency.Length

                    objnode = objxmlDocument.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='" + strTravelAgency(j - 1) + "']")
                    If objnode Is Nothing Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='3']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + j.ToString + "']")
                        If objDeleteNode IsNot Nothing Then
                            objTempnode.RemoveChild(objDeleteNode)
                        End If
                        Continue For
                    End If
                    If objnode.Attributes("Value").Value = "0" Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='3']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + j.ToString + "']")
                        If objDeleteNode IsNot Nothing Then
                            objTempnode.RemoveChild(objDeleteNode)
                        End If
                    Else
                        intstatus = 1
                    End If
                Next
                'For Setup Module
                If intstatus = 0 Then
                    objInXmlDocument.DocumentElement.RemoveChild(objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='3']"))
                End If

                'End TravelAgency Module


                'Start For ISP Module
                intstatus = 0

                For k As Integer = 1 To strISP.Length
                    objnode = objxmlDocument.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='" + strISP(k - 1) + "']")
                    'this node is nothing when security that we have applied is not contained in Security Session ie not coming from backend
                    If objnode Is Nothing Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='4']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + k.ToString + "']")
                        If objDeleteNode IsNot Nothing Then
                            objTempnode.RemoveChild(objDeleteNode)
                        End If
                        Continue For
                    End If
                    If objnode.Attributes("Value").Value = "0" Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='4']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + k.ToString + "']")
                        If objDeleteNode IsNot Nothing Then
                            objTempnode.RemoveChild(objDeleteNode)
                        End If
                    Else
                        intstatus = 1
                    End If
                Next

                If intstatus = 0 Then
                    objInXmlDocument.DocumentElement.RemoveChild(objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='4']"))
                End If

                'End ISP Module




                'Start For HelpDesk Module
                intstatus = 0

                For m As Integer = 1 To strHelpDesk.Length
                    Dim strSecurityOptionSubName As String = "Log HD Call"
                    objnode = objxmlDocument.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='" + strHelpDesk(m - 1) + "']")
                    'this node is nothing when security that we have applied is not contained in Security Session ie not coming from backend
                    If objnode Is Nothing Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='5']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + m.ToString + "']")
                        If objDeleteNode IsNot Nothing Then
                            objTempnode.RemoveChild(objDeleteNode)
                        End If
                        Continue For
                    End If

                    If objnode.Attributes("Value").Value = "0" Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='5']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + m.ToString + "']")
                        If objDeleteNode IsNot Nothing Then

                            If strSecurityOptionSubName = strHelpDesk(m - 1) Then
                                objnode = objxmlDocument.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Log Tech Call']")
                            End If
                            'this node is nothing when security that we have applied is not contained in Security Session ie not coming from backend
                            If objnode Is Nothing Then
                                objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='5']")
                                Dim objDeleteNode1 As XmlNode
                                objDeleteNode1 = objTempnode.SelectSingleNode("Item[@ID='" + m.ToString + "']")
                                If objDeleteNode1 IsNot Nothing Then
                                    objTempnode.RemoveChild(objDeleteNode1)
                                End If
                                Continue For
                            End If
                            If objnode.Attributes("Value").Value = "0" Then
                                objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='5']")
                                Dim objDeleteNode2 As XmlNode
                                objDeleteNode2 = objTempnode.SelectSingleNode("Item[@ID='" + m.ToString + "']")
                                objTempnode.RemoveChild(objDeleteNode)
                            Else
                                intstatus = 1
                            End If


                        End If
                    Else
                        intstatus = 1
                    End If
                Next

                If intstatus = 0 Then
                    objInXmlDocument.DocumentElement.RemoveChild(objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='5']"))
                End If

                'End HelpDesk Module


                'Start For Inventory Module
                intstatus = 0

                For n As Integer = 1 To strInventory.Length
                    objnode = objxmlDocument.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='" + strInventory(n - 1) + "']")
                    'this node is nothing when security that we have applied is not contained in Security Session ie not coming from backend
                    If objnode Is Nothing Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='6']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + n.ToString + "']")
                        If objDeleteNode IsNot Nothing Then
                            objTempnode.RemoveChild(objDeleteNode)
                        End If
                        Continue For
                    End If
                    If objnode.Attributes("Value").Value = "0" Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='6']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + n.ToString + "']")
                        If objDeleteNode IsNot Nothing Then
                            objTempnode.RemoveChild(objDeleteNode)
                        End If
                    Else
                        intstatus = 1
                    End If
                Next

                If intstatus = 0 Then
                    objInXmlDocument.DocumentElement.RemoveChild(objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='6']"))
                End If

                'End Inventory Module


                'Start For Productivity Module
                intstatus = 0

                For o As Integer = 1 To strProductivity.Length
                    objnode = objxmlDocument.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='" + strProductivity(o - 1) + "']")
                    'this node is nothing when security that we have applied is not contained in Security Session ie not coming from backend
                    If objnode Is Nothing Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='7']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + o.ToString + "']")
                        If objDeleteNode IsNot Nothing Then
                            objTempnode.RemoveChild(objDeleteNode)
                        End If
                        Continue For
                    End If
                    If objnode.Attributes("Value").Value = "0" Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='7']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + o.ToString + "']")
                        If objDeleteNode IsNot Nothing Then
                            objTempnode.RemoveChild(objDeleteNode)
                        End If
                    Else
                        intstatus = 1
                    End If
                Next

                If intstatus = 0 Then
                    objInXmlDocument.DocumentElement.RemoveChild(objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='7']"))
                End If

                'End Productivity Module



                'Start For Market Module
                intstatus = 0

                For p As Integer = 1 To strMarket.Length
                    objnode = objxmlDocument.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='" + strMarket(p - 1) + "']")
                    'this node is nothing when security that we have applied is not contained in Security Session ie not coming from backend
                    If objnode Is Nothing Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='8']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + p.ToString + "']")
                        If objDeleteNode IsNot Nothing Then
                            objTempnode.RemoveChild(objDeleteNode)
                        End If
                        Continue For
                    End If
                    If objnode.Attributes("Value").Value = "0" Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='8']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + p.ToString + "']")
                        If objDeleteNode IsNot Nothing Then
                            objTempnode.RemoveChild(objDeleteNode)
                        End If
                    Else
                        intstatus = 1
                    End If
                Next

                If intstatus = 0 Then
                    objInXmlDocument.DocumentElement.RemoveChild(objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='8']"))
                End If

                'End Market Module

                'Start For Training Module
                intstatus = 0

                For p As Integer = 1 To strTraining.Length
                    ' If strTraining(p - 1) = "Send Email" Then
                    'Continue For
                    '  End If
                    objnode = objxmlDocument.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='" + strTraining(p - 1) + "']")
                    'this node is nothing when security that we have applied is not contained in Security Session ie not coming from backend
                    If objnode Is Nothing Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='9']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + p.ToString + "']")
                        If objDeleteNode IsNot Nothing Then
                            objTempnode.RemoveChild(objDeleteNode)
                        End If
                        Continue For
                    End If
                    If objnode.Attributes("Value").Value = "0" Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='9']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + p.ToString + "']")
                        If objDeleteNode IsNot Nothing Then
                            objTempnode.RemoveChild(objDeleteNode)
                        End If
                    Else
                        intstatus = 1
                    End If
                Next

                If intstatus = 0 Then
                    objInXmlDocument.DocumentElement.RemoveChild(objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='9']"))
                End If

                'End  Training Module


                'Start For Sales Module
                intstatus = 0

                For k As Integer = 1 To strSales.Length
                    objnode = objxmlDocument.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='" + strSales(k - 1) + "']")
                    'this node is nothing when security that we have applied is not contained in Security Session ie not coming from backend
                    If objnode Is Nothing Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='10']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + k.ToString + "']")
                        If objDeleteNode IsNot Nothing Then
                            objTempnode.RemoveChild(objDeleteNode)
                        End If
                        Continue For
                    End If
                    If objnode.Attributes("Value").Value = "0" Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='10']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + k.ToString + "']")
                        If objDeleteNode IsNot Nothing Then
                            objTempnode.RemoveChild(objDeleteNode)
                        End If
                    Else
                        intstatus = 1
                    End If
                Next

                If intstatus = 0 Then
                    objInXmlDocument.DocumentElement.RemoveChild(objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='10']"))
                End If

                'End Sales Module



                'Start For BirdresHelpDesk Module
                intstatus = 0

                For m As Integer = 1 To strBRHelpDesk.Length
                    Dim strSecurityOptionSubName As String = "BR Log HD Call"
                    objnode = objxmlDocument.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='" + strBRHelpDesk(m - 1) + "']")
                    'this node is nothing when security that we have applied is not contained in Security Session ie not coming from backend
                    If objnode Is Nothing Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='11']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + m.ToString + "']")
                        If objDeleteNode IsNot Nothing Then
                            objTempnode.RemoveChild(objDeleteNode)
                        End If
                        Continue For
                    End If

                    If objnode.Attributes("Value").Value = "0" Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='11']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + m.ToString + "']")
                        If objDeleteNode IsNot Nothing Then

                            If strSecurityOptionSubName = strBRHelpDesk(m - 1) Then
                                objnode = objxmlDocument.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Log Tech Call']")
                            End If
                            'this node is nothing when security that we have applied is not contained in Security Session ie not coming from backend
                            If objnode Is Nothing Then
                                objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='11']")
                                Dim objDeleteNode1 As XmlNode
                                objDeleteNode1 = objTempnode.SelectSingleNode("Item[@ID='" + m.ToString + "']")
                                If objDeleteNode1 IsNot Nothing Then
                                    objTempnode.RemoveChild(objDeleteNode1)
                                End If
                                Continue For
                            End If
                            If objnode.Attributes("Value").Value = "0" Then
                                objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='11']")
                                Dim objDeleteNode2 As XmlNode
                                objDeleteNode2 = objTempnode.SelectSingleNode("Item[@ID='" + m.ToString + "']")
                                objTempnode.RemoveChild(objDeleteNode)
                            Else
                                intstatus = 1
                            End If


                        End If
                    Else
                        intstatus = 1
                    End If
                Next

                If intstatus = 0 Then
                    objInXmlDocument.DocumentElement.RemoveChild(objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='11']"))
                End If

                'End bIRDRES HelpDesk Module


               


              

                'Start For BackOfficeHelpdesk Module
                intstatus = 0

                For m As Integer = 1 To strBackOfficeHelpdesk.Length
                    Dim strSecurityOptionSubName As String = "BO Log HD Call"
                    objnode = objxmlDocument.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='" + strBackOfficeHelpdesk(m - 1) + "']")
                    'this node is nothing when security that we have applied is not contained in Security Session ie not coming from backend
                    If objnode Is Nothing Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='13']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + m.ToString + "']")
                        If objDeleteNode IsNot Nothing Then
                            objTempnode.RemoveChild(objDeleteNode)
                        End If
                        Continue For
                    End If

                    If objnode.Attributes("Value").Value = "0" Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='13']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + m.ToString + "']")
                        If objDeleteNode IsNot Nothing Then

                            If strSecurityOptionSubName = strHelpDesk(m - 1) Then
                                objnode = objxmlDocument.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Log Tech Call']")
                            End If
                            'this node is nothing when security that we have applied is not contained in Security Session ie not coming from backend
                            If objnode Is Nothing Then
                                objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='13']")
                                Dim objDeleteNode1 As XmlNode
                                objDeleteNode1 = objTempnode.SelectSingleNode("Item[@ID='" + m.ToString + "']")
                                If objDeleteNode1 IsNot Nothing Then
                                    objTempnode.RemoveChild(objDeleteNode1)
                                End If
                                Continue For
                            End If
                            If objnode.Attributes("Value").Value = "0" Then
                                objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='13']")
                                Dim objDeleteNode2 As XmlNode
                                objDeleteNode2 = objTempnode.SelectSingleNode("Item[@ID='" + m.ToString + "']")
                                objTempnode.RemoveChild(objDeleteNode)
                            Else
                                intstatus = 1
                            End If


                        End If
                    Else
                        intstatus = 1
                    End If
                Next

                If intstatus = 0 Then
                    objInXmlDocument.DocumentElement.RemoveChild(objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='13']"))
                End If

                'End BackOfficeHelpdesk Module

               
                'Start For Incentive Module
                '{"Approval Level", "Business Case", "Approval Queue", "Payment Approval Level", "Incentive Payment Process", "Payment Approval Queue", "Incentive Payment Received", "IncentivePaidReport", "VolumeClassification"}
                intstatus = 0

                For l As Integer = 1 To strIncentive.Length
                    objnode = objxmlDocument.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='" + strIncentive(l - 1) + "']")
                    'this node is nothing when security that we have applied is not contained in Security Session ie not coming from backend
                    If objnode Is Nothing Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='14']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + l.ToString + "']")
                        If objDeleteNode IsNot Nothing Then
                            objTempnode.RemoveChild(objDeleteNode)
                        End If
                        Continue For
                    End If
                    If objnode.Attributes("Value").Value = "0" Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='14']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + l.ToString + "']")
                        If objDeleteNode IsNot Nothing Then
                            objTempnode.RemoveChild(objDeleteNode)
                        End If
                    Else
                        intstatus = 1
                    End If
                Next

                If intstatus = 0 Then
                    objInXmlDocument.DocumentElement.RemoveChild(objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='14']"))
                End If

                'End Incentive Module


                'Start For ETracker Help-Desk Module
                intstatus = 0

                For m As Integer = 1 To strETrackerHelpdesk.Length
                    Dim strSecurityOptionSubName As String = "ET Log HD Call"
                    objnode = objxmlDocument.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='" + strETrackerHelpdesk(m - 1) + "']")
                    'this node is nothing when security that we have applied is not contained in Security Session ie not coming from backend
                    If objnode Is Nothing Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='12']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + m.ToString + "']")
                        If objDeleteNode IsNot Nothing Then
                            objTempnode.RemoveChild(objDeleteNode)
                        End If
                        Continue For
                    End If

                    If objnode.Attributes("Value").Value = "0" Then
                        objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='12']")
                        Dim objDeleteNode As XmlNode
                        objDeleteNode = objTempnode.SelectSingleNode("Item[@ID='" + m.ToString + "']")
                        If objDeleteNode IsNot Nothing Then

                            If strSecurityOptionSubName = strHelpDesk(m - 1) Then
                                objnode = objxmlDocument.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ET Log Tech Call']")
                            End If
                            'this node is nothing when security that we have applied is not contained in Security Session ie not coming from backend
                            If objnode Is Nothing Then
                                objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='12']")
                                Dim objDeleteNode1 As XmlNode
                                objDeleteNode1 = objTempnode.SelectSingleNode("Item[@ID='" + m.ToString + "']")
                                If objDeleteNode1 IsNot Nothing Then
                                    objTempnode.RemoveChild(objDeleteNode1)
                                End If
                                Continue For
                            End If
                            If objnode.Attributes("Value").Value = "0" Then
                                objTempnode = objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='12']")
                                Dim objDeleteNode2 As XmlNode
                                objDeleteNode2 = objTempnode.SelectSingleNode("Item[@ID='" + m.ToString + "']")
                                objTempnode.RemoveChild(objDeleteNode)
                            Else
                                intstatus = 1
                            End If


                        End If
                    Else
                        intstatus = 1
                    End If
                Next

                If intstatus = 0 Then
                    objInXmlDocument.DocumentElement.RemoveChild(objInXmlDocument.DocumentElement.SelectSingleNode("HEADING[@ID='12']"))
                End If

                'End OF ETracker Help-Desk Module



            End If
            Return objInXmlDocument.OuterXml
        Catch ex As Exception
            Return objInXmlDocument.OuterXml
        Finally

        End Try
    End Function

End Class
