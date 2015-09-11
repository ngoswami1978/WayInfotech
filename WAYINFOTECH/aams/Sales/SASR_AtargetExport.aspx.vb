
Partial Class Sales_SASR_AtargetExport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objOutputXml As New XmlDocument
        Dim objExport As New ExportExcel
        Dim objXmlNode As XmlNode

        Try

            If Session("DSRActionExport") IsNot Nothing Then
                Dim objNewoutputxml As New XmlDocument
                objNewoutputxml.LoadXml(Session("DSRActionExport").ToString)
                If Request.QueryString("EXPORTDSRACTIONDATA") IsNot Nothing Then
                    If Request.QueryString("EXPORTDSRACTIONDATA").ToString.ToUpper = "DSRACTION" Then
                        exportDSRAction(objNewoutputxml)
                        Exit Sub
                    End If
                End If

            End If

            If Request.QueryString("EXPORTDSRDATA") IsNot Nothing Then
                If Request.QueryString("EXPORTDSRDATA").ToString.ToUpper = "DSR" Then
                    ExportDSRdata()
                    Exit Sub
                End If
            End If
            ExportAgencyTargetData()
        Catch ex As Exception
            Try
                objOutputXml.LoadXml("<pp><TARGET ERROR='' /></pp>")
                objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("TARGET")
                objXmlNode.Attributes("ERROR").Value = ex.Message
                Dim strArray(objXmlNode.Attributes.Count - 1) As String
                Dim intArray(objXmlNode.Attributes.Count - 1) As Integer
                For i As Integer = 0 To objXmlNode.Attributes.Count - 1
                    strArray(i) = objXmlNode.Attributes(i).Name
                    intArray(i) = i
                Next
                objExport.ExportDetails(objOutputXml, "TARGET", intArray, strArray, ExportExcel.ExportFormat.Excel, "Error.xls")
            Catch ex2 As Exception
            End Try
        End Try

    End Sub
    Private Sub ExportAgencyTargetData()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlNode As XmlNode
        Dim objExport As New ExportExcel



        If Not Session("TargetOutputExportXml") Is Nothing Then
            objOutputXml.LoadXml(Session("TargetOutputExportXml").ToString)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("TARGET")



                If DateTime.DaysInMonth(objXmlNode.Attributes("YEAR").Value, objXmlNode.Attributes("MONTH").Value) = 30 Then
                    If objOutputXml.DocumentElement.SelectSingleNode("TARGET").Attributes("D31") IsNot Nothing Then
                        For Each objNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("TARGET")
                            objNode.Attributes.RemoveNamedItem("D31")
                        Next
                    End If
                End If
                If DateTime.DaysInMonth(objXmlNode.Attributes("YEAR").Value, objXmlNode.Attributes("MONTH").Value) = 29 Then
                    If objOutputXml.DocumentElement.SelectSingleNode("TARGET").Attributes("D31") IsNot Nothing Then
                        For Each objNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("TARGET")
                            objNode.Attributes.RemoveNamedItem("D31")
                            objNode.Attributes.RemoveNamedItem("D30")
                        Next
                    End If


                End If
                If DateTime.DaysInMonth(objXmlNode.Attributes("YEAR").Value, objXmlNode.Attributes("MONTH").Value) = 28 Then
                    If objOutputXml.DocumentElement.SelectSingleNode("TARGET").Attributes("D31") IsNot Nothing Then
                        For Each objNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("TARGET")
                            objNode.Attributes.RemoveNamedItem("D31")
                            objNode.Attributes.RemoveNamedItem("D30")
                            objNode.Attributes.RemoveNamedItem("D29")
                        Next
                    End If
                End If

                Dim m As Integer = 0
                For Each objNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("TARGET")
                    For m = 1 To 31
                        Dim objAttributesName As String = "D" + m.ToString
                        If objNode.Attributes(objAttributesName) IsNot Nothing Then
                            If objNode.Attributes(objAttributesName).Value = "1" Then
                                objNode.Attributes(objAttributesName).Value = "Plan"
                            End If
                            If objNode.Attributes(objAttributesName).Value = "2" Then
                                objNode.Attributes(objAttributesName).Value = "Visited"
                            End If
                            If objNode.Attributes(objAttributesName).Value = "3" Then
                                objNode.Attributes(objAttributesName).Value = "Unplan Visited"
                            End If
                            If objNode.Attributes(objAttributesName).Value = "4" Then
                                objNode.Attributes(objAttributesName).Value = "Not Visited"
                            End If
                            If objNode.Attributes(objAttributesName).Value = "5" Then
                                objNode.Attributes(objAttributesName).Value = "BACKDATED DSR LOGGED"
                            End If
                            If objNode.Attributes(objAttributesName).Value = "6" Then
                                objNode.Attributes(objAttributesName).Value = "PLANNED VISIT NOT LOGGED AFTER 5 DAYS"
                            End If
                            If objNode.Attributes(objAttributesName).Value = "7" Then
                                objNode.Attributes(objAttributesName).Value = "PLANNED CALL BY MANAGER"
                            End If
                        End If
                    Next
                Next


                'TOTAL NODE FOOTER
                Dim objXmlNodeClone As XmlNode
                objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("TARGET")
                objXmlNodeClone = objXmlNode.CloneNode(True)

                For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                    XmlAttr.Value = ""
                Next
                With objXmlNodeClone
                    If objXmlNodeClone.Attributes("RESP1A_NAME") IsNot Nothing Then
                        objXmlNodeClone.Attributes("RESP1A_NAME").Value = "Total"
                    End If
                    If objXmlNodeClone.Attributes("AVGBIDT") IsNot Nothing Then
                        objXmlNodeClone.Attributes("AVGBIDT").Value = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("AVGBIDT").Value
                    End If
                    If objXmlNodeClone.Attributes("AVGMIDT") IsNot Nothing Then
                        objXmlNodeClone.Attributes("AVGMIDT").Value = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("AVGMIDT").Value
                    End If
                    If objXmlNodeClone.Attributes("PASTMOTIVE") IsNot Nothing Then
                        objXmlNodeClone.Attributes("PASTMOTIVE").Value = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("PASTMOTIVE").Value
                    End If
                    If objXmlNodeClone.Attributes("SEGSTARGET") IsNot Nothing Then
                        objXmlNodeClone.Attributes("SEGSTARGET").Value = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("SEGSTARGET").Value
                    End If
                    If objXmlNodeClone.Attributes("VISITCOUNT") IsNot Nothing Then
                        objXmlNodeClone.Attributes("VISITCOUNT").Value = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("VISITCOUNT").Value
                    End If
                    If objXmlNode.Attributes("VISITTARGET") IsNot Nothing Then
                        objXmlNodeClone.Attributes("VISITTARGET").Value = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("VISITTARGET").Value
                    End If
                    If objXmlNode.Attributes("PVISITDONE") IsNot Nothing Then
                        objXmlNodeClone.Attributes("PVISITDONE").Value = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("PVISITDONE").Value
                    End If
                    If objXmlNodeClone.Attributes("UVISITDONE") IsNot Nothing Then

                        objXmlNodeClone.Attributes("UVISITDONE").Value = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("UVISITDONE").Value
                    End If
                    If objXmlNodeClone.Attributes("MINIUMSEGS") IsNot Nothing Then

                        objXmlNodeClone.Attributes("MINIUMSEGS").Value = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("MINIUMSEGS").Value
                    End If

                    For RowNum As Integer = 1 To 31
                        Dim strAttr As String = "D" + RowNum.ToString
                        If objXmlNodeClone.Attributes(strAttr) IsNot Nothing AndAlso objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes(strAttr) IsNot Nothing Then
                            objXmlNodeClone.Attributes(strAttr).Value = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes(strAttr).Value
                        End If
                    Next
                End With

                objOutputXml.DocumentElement.AppendChild(objXmlNodeClone)


                ' <PAGE_TOTAL AVGBIDT ='' AVGMIDT='' PASTMOTIVE='' SEGSTARGET='' VISITCOUNT='' VISITTARGET ='' PVISITDONE='' UVISITDONE='' MINIUMSEGS=''/>
                'AVGBIDT="" AVGMIDT="" PASTMOTIVE="" BUSINESSCCOMMIT="" MINIUMSEGS="" VISITCOUNT="0" SEGSTARGET="0" VISITTARGET="0" PVISITDONE="0" UVISITDONE="0"              

                'TOTAL NODE FOOTER

                Dim strArray(objXmlNode.Attributes.Count - 6) As String
                Dim intArray(objXmlNode.Attributes.Count - 6) As Integer

                If objOutputXml.DocumentElement("TARGET").Attributes("TA_AGENCYTARGET_OUTPUT_Id") Is Nothing Then
                    ReDim strArray(objXmlNode.Attributes.Count - 1 - 6)
                    ReDim intArray(objXmlNode.Attributes.Count - 1 - 6)
                Else
                    ReDim strArray(objXmlNode.Attributes.Count - 1 - 7)
                    ReDim intArray(objXmlNode.Attributes.Count - 1 - 7)
                End If
                'Dim j As Integer = 0
                'For i As Integer = 0 To objXmlNode.Attributes.Count - 1
                '    If Not (objXmlNode.Attributes(i).Name = "RESP1A_ID" Or objXmlNode.Attributes(i).Name = "EMPLOYEEID" Or objXmlNode.Attributes(i).Name = "LOGDATE" Or objXmlNode.Attributes(i).Name = "TA_AGENCYTARGET_OUTPUT_Id" Or objXmlNode.Attributes(i).Name = "PLANNED_DAYS" Or objXmlNode.Attributes(i).Name = "MONTH" Or objXmlNode.Attributes(i).Name = "YEAR") Then
                '        If objXmlNode.Attributes(i).Name = "RESP1A_NAME" Then
                '            strArray(j) = "SALES EXECUTIVE"
                '        ElseIf objXmlNode.Attributes(i).Name = "GROUP_CATG" Then
                '            strArray(j) = "TYPE"
                '        ElseIf objXmlNode.Attributes(i).Name = "AGENCYNAME" Then
                '            strArray(j) = "AGENCY NAME"
                '        ElseIf objXmlNode.Attributes(i).Name = "VISITCOUNT" Then
                '            strArray(j) = "SALES OBJECTIVE VISIT"
                '        ElseIf objXmlNode.Attributes(i).Name = "PASTMOTIVE" Then
                '            strArray(j) = "LAST MOTIVE"
                '        ElseIf objXmlNode.Attributes(i).Name = "PVISITDONE" Then
                '            strArray(j) = "PLAN VISIT DONE"
                '        ElseIf objXmlNode.Attributes(i).Name = "UVISITDONE" Then
                '            strArray(j) = "UNPLAN VISIT DONE"
                '        ElseIf objXmlNode.Attributes(i).Name = "AVGBIDT" Then
                '            strArray(j) = "BIDT"
                '        ElseIf objXmlNode.Attributes(i).Name = "AVGMIDT" Then
                '            strArray(j) = "POTENTIAL"
                '        ElseIf objXmlNode.Attributes(i).Name = "MINIUMSEGS" Then
                '            strArray(j) = "MIN SEGMENTS"
                '        ElseIf objXmlNode.Attributes(i).Name = "VISITTARGET" Then
                '            strArray(j) = "VISIT TARGET"
                '        ElseIf objXmlNode.Attributes(i).Name = "SEGSTARGET" Then
                '            strArray(j) = "SEGS TARGET"
                '        ElseIf objXmlNode.Attributes(i).Name = "CHAINCODE" Then
                '            strArray(j) = "CHAIN CODE"
                '        Else
                '            strArray(j) = objXmlNode.Attributes(i).Name
                '        End If
                '        intArray(j) = i
                '        j = j + 1
                '    End If
                'Next

                '@ Start of  Making Sequence ItemList According To Grid showing

                ' MONTH="10" YEAR="2011" LCODE="31189" CHAINCODE="16" AGENCYNAME="Acme" ADDRESS="Plot No-63,Udyog Vihar" OFFICEID="DELVS34BB" CITY="Gurgaon" GROUP_CATG="E" RESP1A_ID="24" RESP1A_NAME="Admin" EMPLOYEEID="24" AVGBIDT="" AVGMIDT="" PASTMOTIVE="" BUSINESSCCOMMIT="" MINIUMSEGS="" VISITCOUNT="" SEGSTARGET="" VISITTARGET="" PVISITDONE="" UVISITDONE="" LOGDATE="" D1="" D2="" D3="" D4="" D5="" D6="" D7="" D8="" D9="" D10="" D11="" D12="" D13="" D14="" D15="" D16="" D17="" D18="" D19="" D20="" D21="" D22="" D23="" D24="" D25="" D26="" D27="" D28="" D29="" D30="" D31="" PLANNED_DAYS="" COLORCODE="" M_CHK_VT="True"

                objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("TARGET")
                Dim SequenceItem As New ArrayList()
                SequenceItem.Add("LCODE")
                SequenceItem.Add("CHAINCODE")
                SequenceItem.Add("AGENCYNAME")
                SequenceItem.Add("OFFICEID")
                SequenceItem.Add("ADDRESS")
                SequenceItem.Add("CITY")
                SequenceItem.Add("GROUP_CATG")
                SequenceItem.Add("RESP1A_NAME")
                SequenceItem.Add("SEGSTARGET")
                SequenceItem.Add("VISITCOUNT")
                SequenceItem.Add("VISITTARGET")
                SequenceItem.Add("PVISITDONE")
                SequenceItem.Add("UVISITDONE")
                SequenceItem.Add("AVGMIDT")
                SequenceItem.Add("AVGBIDT")
                SequenceItem.Add("PASTMOTIVE")
                SequenceItem.Add("MINIUMSEGS")
                SequenceItem.Add("BUSINESSCCOMMIT")
                SequenceItem.Add("BCMINSEGMENT")
                For RowNum As Integer = 1 To 31
                    Dim strAttr As String = "D" + RowNum.ToString
                    SequenceItem.Add(strAttr)
                Next

                Dim counter As Integer = SequenceItem.Count - 1

                For i As Integer = 0 To counter
                    For k As Integer = 0 To SequenceItem.Count - 1
                        If objXmlNode.Attributes(SequenceItem(k).ToString) Is Nothing Then
                            SequenceItem.Remove(SequenceItem(k).ToString)
                            Exit For
                        End If
                    Next k
                Next i
                '@ End of  Making Sequence ItemList According To Grid showing

                For index As Integer = 0 To SequenceItem.Count - 1
                    For i As Integer = 0 To objXmlNode.Attributes.Count - 1
                        If SequenceItem(index).ToString = objXmlNode.Attributes(i).Name.ToString Then
                            intArray(index) = i

                            If objXmlNode.Attributes(i).Name = "RESP1A_NAME" Then
                                strArray(index) = "SALES EXECUTIVE"
                            ElseIf objXmlNode.Attributes(i).Name = "GROUP_CATG" Then
                                strArray(index) = "TYPE"
                            ElseIf objXmlNode.Attributes(i).Name = "AGENCYNAME" Then
                                strArray(index) = "AGENCY NAME"
                            ElseIf objXmlNode.Attributes(i).Name = "VISITCOUNT" Then
                                strArray(index) = "SALES OBJECTIVE VISIT"
                            ElseIf objXmlNode.Attributes(i).Name = "PASTMOTIVE" Then
                                strArray(index) = "LAST MOTIVE"
                            ElseIf objXmlNode.Attributes(i).Name = "PVISITDONE" Then
                                strArray(index) = "PLAN VISIT DONE"
                            ElseIf objXmlNode.Attributes(i).Name = "UVISITDONE" Then
                                strArray(index) = "UNPLAN VISIT DONE"
                            ElseIf objXmlNode.Attributes(i).Name = "AVGBIDT" Then
                                strArray(index) = "BIDT"
                            ElseIf objXmlNode.Attributes(i).Name = "AVGMIDT" Then
                                strArray(index) = "POTENTIAL"
                            ElseIf objXmlNode.Attributes(i).Name = "MINIUMSEGS" Then
                                strArray(index) = "MIN SEGMENTS"
                            ElseIf objXmlNode.Attributes(i).Name = "VISITTARGET" Then
                                strArray(index) = "VISIT TARGET"
                            ElseIf objXmlNode.Attributes(i).Name = "SEGSTARGET" Then
                                strArray(index) = "SEGS TARGET"
                            ElseIf objXmlNode.Attributes(i).Name = "CHAINCODE" Then
                                strArray(index) = "CHAIN CODE"
                            ElseIf objXmlNode.Attributes(i).Name = "BUSINESSCCOMMIT" Then
                                strArray(index) = "COMMITMENT"
                            Else
                                strArray(index) = objXmlNode.Attributes(i).Name
                            End If

                        End If
                    Next i
                Next index


                objExport.ExportDetails(objOutputXml, "TARGET", intArray, strArray, ExportExcel.ExportFormat.Excel, "AGencyTarget.xls")
            Else
                objOutputXml.LoadXml("<pp><TARGET MONTH='4' YEAR='2011' LCODE='236' CHAINCODE='36' AGENCYNAME='Shree Raj Travel &amp; Tours Ltd' ADDRESS='73/1,Cunningham Road' OFFICEID='BLRWI2122' CITY='Bangalore' GROUP_CATG='A'  RESPONSIBLE NAME='Elizabeth Miriam'  AVGBIDT='0' AVGMIDT='0' PASTMOTIVE='0' BUSINESSCCOMMIT='0' MINIUMSEGS='0' VISITCOUNT='20' SEGSTARGET='0' VISITTARGET='' PVISITDONE='' UVISITDONE='' LOGDATE='5/11/2011 3:49:34 PM' D1='' D2='' D3='' D4='' D5='' D6='' D7='' D8='' D9='' D10='' D11='' D12='' D13='' D14='' D15='' D16='' D17='' D18='' D19='' D20='' D21='' D22='' D23='' D24='' D25='' D26='' D27='' D28='' D29='' D30='' D31='' PLANNED_DAYS='' /></pp>")

                objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("TARGET")

                For Each XmlAttr As XmlAttribute In objXmlNode.Attributes
                    XmlAttr.Value = ""
                Next

                objOutputXml.DocumentElement.AppendChild(objOutputXml.ImportNode(objXmlNode, True))

                Dim strArray(objXmlNode.Attributes.Count - 1) As String
                Dim intArray(objXmlNode.Attributes.Count - 1) As Integer
                For i As Integer = 0 To objXmlNode.Attributes.Count - 1
                    strArray(i) = objXmlNode.Attributes(i).Name
                    intArray(i) = i
                Next

                objExport.ExportDetails(objOutputXml, "TARGET", intArray, strArray, ExportExcel.ExportFormat.Excel, "AGencyTarget.xls")
            End If
        Else
            objOutputXml.LoadXml("<pp><TARGET MONTH='4' YEAR='2011' LCODE='236' CHAINCODE='36' AGENCYNAME='Shree Raj Travel &amp; Tours Ltd' ADDRESS='73/1,Cunningham Road' OFFICEID='BLRWI2122' CITY='Bangalore' GROUP_CATG='A'  RESPONSIBLE NAME='Elizabeth Miriam'  AVGBIDT='0' AVGMIDT='0' PASTMOTIVE='0' BUSINESSCCOMMIT='0' MINIUMSEGS='0' VISITCOUNT='20' SEGSTARGET='0' VISITTARGET='' PVISITDONE='' UVISITDONE='' LOGDATE='5/11/2011 3:49:34 PM' D1='' D2='' D3='' D4='' D5='' D6='' D7='' D8='' D9='' D10='' D11='' D12='' D13='' D14='' D15='' D16='' D17='' D18='' D19='' D20='' D21='' D22='' D23='' D24='' D25='' D26='' D27='' D28='' D29='' D30='' D31='' PLANNED_DAYS='' /></pp>")

            objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("TARGET")

            For Each XmlAttr As XmlAttribute In objXmlNode.Attributes
                XmlAttr.Value = ""
            Next

            objOutputXml.DocumentElement.AppendChild(objOutputXml.ImportNode(objXmlNode, True))

            Dim strArray(objXmlNode.Attributes.Count - 1) As String
            Dim intArray(objXmlNode.Attributes.Count - 1) As Integer
            For i As Integer = 0 To objXmlNode.Attributes.Count - 1
                strArray(i) = objXmlNode.Attributes(i).Name
                intArray(i) = i
            Next

            objExport.ExportDetails(objOutputXml, "TARGET", intArray, strArray, ExportExcel.ExportFormat.Excel, "AGencyTarget.xls")
        End If

    End Sub
    Private Sub ExportDSRdata()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objTarget As New AAMS.bizSales.bzAgencyTarget
        Dim objExport As New ExportExcel
        Dim objXmlNode As XmlNode
        Dim NotShowColumnArray As New ArrayList
        If Not Session("DSREXPORTINPUTXML") Is Nothing Then
            objInputXml.LoadXml(Session("DSREXPORTINPUTXML").ToString)

            If Session("NotShowColumnArray") IsNot Nothing Then
                NotShowColumnArray = CType(Session("NotShowColumnArray"), ArrayList)
            End If


            NotShowColumnArray.Add("ROWID")
            NotShowColumnArray.Add("CITYID")
            'NotShowColumnArray.Add("DSR_VISIT_ID")
            NotShowColumnArray.Add("HODID")
            NotShowColumnArray.Add("MANAGERID")
            NotShowColumnArray.Add("EMPLOYEEID")
            NotShowColumnArray.Add("RESP_1A")
            NotShowColumnArray.Add("UNPLANVISIT")
            NotShowColumnArray.Add("PREV_REMARKS1DATE")
            NotShowColumnArray.Add("PREV_REMARKS2DATE")
            NotShowColumnArray.Add("VISITCOUNT")


            Session("DSREXPORTINPUTXML") = Nothing
            Session("NotShowColumnArray") = Nothing
            ' objOutputXml.Load("C:\SL_SEARCH_LOG_DSR_OUTPUT.xml")
            'objOutputXml.Load("c:\SearchDSROuputXml.xml")
            objOutputXml = objTarget.SearchDSR(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                '@ Start of Code For Removing Attribute which is not to show on Export
                For i As Integer = 0 To NotShowColumnArray.Count - 1
                    For Each objNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("DSRLOG")
                        objNode.Attributes.RemoveNamedItem(NotShowColumnArray(i).ToString)
                    Next
                Next
                '@ End of Code For Removing Attribute which is not to show on Export

                objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("DSRLOG")

                Dim strArray(objXmlNode.Attributes.Count - 1) As String
                Dim intArray(objXmlNode.Attributes.Count - 1) As Integer
                Dim j As Integer = 0
                ' DSRLOG ROWID="1" DSR_VISIT_ID="12" DATE="2/1/2010" AGENCY_NAME="Cat Air Pvt Ltd" ADDRESS="Dhondusa Complex," LCODE="264" OFFICEID="BLRWI2124" DESIGNATION="DESIGNATION" PERSONMET="CONTACT_NAME" PENDINGISSUES="" BCCOMMIT="" BCMINSEGMENT="" MANAGERID="0" MANAGERNAME="" HODID="" HODNAME="" CITYID="124" CITY_NAME="Bangalore" CHAIN_CODE="606" A1DAILYMOTIVES="0" MIDT="0" BIDT="0" VISITED="" REASON_REMARKS="" OBJ_VISITCOUNT="" PLAN_VISIT_DONE="" UNPLAN_VISIT_DONE="" VISIT_TYPE_NAME="" DISCUSSION_ISSUE_REMARKS="" COMPETITION_MKT_INFO_REMARKS="" STATUS_NAME="" FOLLOWUP_REMARKS="" PREV_REMARKS1="" PREV_REMARKS2="" INTIME="" OUTTIME="" GROUP_CATG_NAME="" VISITCOUNT="" LOGDATE="5/21/2011 11:55:32 AM" RESP_NAME="Admin" RESP_1A="24" UNPLANVISIT=""


                '@ Start of  Making Sequence ItemList According To Grid showing
                Dim SequenceItem As New ArrayList()
                SequenceItem.Add("DSR_VISIT_ID")
                SequenceItem.Add("LCODE")
                SequenceItem.Add("CHAIN_CODE")
                SequenceItem.Add("AGENCY_NAME")
                SequenceItem.Add("ADDRESS")
                SequenceItem.Add("CITY_NAME")
                SequenceItem.Add("GROUP_CATG_NAME")
                SequenceItem.Add("OFFICEID")
                SequenceItem.Add("RESP_NAME")
                SequenceItem.Add("VISITED")
                SequenceItem.Add("REASON_REMARKS")
                SequenceItem.Add("SEGSTARGET")
                SequenceItem.Add("OBJ_VISITCOUNT")
                SequenceItem.Add("VISITTARGET")
                SequenceItem.Add("PLAN_VISIT_DONE")
                SequenceItem.Add("UNPLAN_VISIT_DONE")
                SequenceItem.Add("MIDT")
                SequenceItem.Add("MIDT_LATEST")
                SequenceItem.Add("BIDT")
                SequenceItem.Add("BIDT_LATEST")

                SequenceItem.Add("A1DAILYMOTIVES")
                SequenceItem.Add("BCMINSEGMENT")
                SequenceItem.Add("BCCOMMIT")
                SequenceItem.Add("VISIT_TYPE_NAME")
                SequenceItem.Add("DISCUSSION_ISSUE_REMARKS")
                SequenceItem.Add("COMPETITION_MKT_INFO_REMARKS")
                SequenceItem.Add("STATUS_NAME")
                SequenceItem.Add("FOLLOWUP_REMARKS")
                SequenceItem.Add("PREV_REMARKS1")
                SequenceItem.Add("PREV_REMARKS2")
                SequenceItem.Add("DESIGNATION")
                SequenceItem.Add("PERSONMET")
                SequenceItem.Add("LOGDATE")
                SequenceItem.Add("INTIME")
                SequenceItem.Add("OUTTIME")
                SequenceItem.Add("HODNAME")
                SequenceItem.Add("MANAGERNAME")
                SequenceItem.Add("STAFFNAME")
                SequenceItem.Add("JOINTCALLREMARKS")
                SequenceItem.Add("SR_ASSIGNTO")
                SequenceItem.Add("ST_ASSIGNTO")
                SequenceItem.Add("LOGMANAGERNAME")
                SequenceItem.Add("DATE")

                SequenceItem.Add("ST_DEPARTMENT")
                SequenceItem.Add("SC_DEPARTMENT_SPECIFIC")
                SequenceItem.Add("ST_TARGET_CLOSERDATE")
                SequenceItem.Add("ST_CLOSERDATE")
                SequenceItem.Add("ST_RETENTION_REASON")
                SequenceItem.Add("ST_CPS")
                SequenceItem.Add("ST_NEWCPS")
                SequenceItem.Add("ST_A1APPROVED_NEW_DEAL")
                SequenceItem.Add("ST_STR_SIGNON_DATE")
                SequenceItem.Add("ST_STR_INSTALLATION_DATE")
                SequenceItem.Add("ST_STR_PRODUCT")
                SequenceItem.Add("ST_STR_REVENUE")
                SequenceItem.Add("ST_STR_TARGET_SEG")






                Dim counter As Integer = SequenceItem.Count - 1

                For i As Integer = 0 To counter
                    For k As Integer = 0 To SequenceItem.Count - 1
                        If objXmlNode.Attributes(SequenceItem(k).ToString) Is Nothing Then
                            SequenceItem.Remove(SequenceItem(k).ToString)
                            Exit For
                        End If
                    Next k
                Next i


                '@ End of  Making Sequence ItemList According To Grid showing
                For index As Integer = 0 To SequenceItem.Count - 1
                    For i As Integer = 0 To objXmlNode.Attributes.Count - 1
                        If SequenceItem(index).ToString = objXmlNode.Attributes(i).Name.ToString Then
                            intArray(index) = i
                            If objXmlNode.Attributes(i).Name = "RESP_NAME" Then
                                strArray(index) = "SALES EXECUTIVE"
                            ElseIf objXmlNode.Attributes(i).Name = "CHAIN_CODE" Then
                                strArray(index) = "CHAIN CODE"
                            ElseIf objXmlNode.Attributes(i).Name = "CITY_NAME" Then
                                strArray(index) = "CITY NAME"
                            ElseIf objXmlNode.Attributes(i).Name = "PLAN_VISIT_DONE" Then
                                strArray(index) = "PLAN VISIT DONE"
                            ElseIf objXmlNode.Attributes(i).Name = "UNPLAN_VISIT_DONE" Then
                                strArray(index) = "UNPLAN VISIT DONE"
                            ElseIf objXmlNode.Attributes(i).Name = "VISIT_TYPE_NAME" Then
                                strArray(index) = "VISIT SUB TYPE"
                            ElseIf objXmlNode.Attributes(i).Name = "HODNAME" Then
                                strArray(index) = "ACCOMPANIED BY MANAGER"
                            ElseIf objXmlNode.Attributes(i).Name = "MANAGERNAME" Then
                                strArray(index) = "ACCOMPANIED BY REPORTING MANAGER"

                            ElseIf objXmlNode.Attributes(i).Name = "GROUP_CATG_NAME" Then
                                strArray(index) = "TYPE"
                            ElseIf objXmlNode.Attributes(i).Name = "AGENCY_NAME" Then
                                strArray(index) = "AGENCY NAME"
                            ElseIf objXmlNode.Attributes(i).Name = "REASON_REMARKS" Then
                                strArray(index) = "REASON FOR NO VISIT AS PER PLANNED VISIT"

                            ElseIf objXmlNode.Attributes(i).Name = "OBJ_VISITCOUNT" Then
                                strArray(index) = "SALES OBJECTIVE VISIT"
                            ElseIf objXmlNode.Attributes(i).Name = "VISITTARGET" Then
                                strArray(index) = "VISIT TARGET"
                            ElseIf objXmlNode.Attributes(i).Name = "DISCUSSION_ISSUE_REMARKS" Then
                                strArray(index) = "DETAILED DISCUSSION/ ISSUE REPORTED"
                            ElseIf objXmlNode.Attributes(i).Name = "COMPETITION_MKT_INFO_REMARKS" Then
                                strArray(index) = "COMPETITION INFO/MKT INFO REMARKS"
                            ElseIf objXmlNode.Attributes(i).Name = "STATUS_NAME" Then
                                strArray(index) = "STATUS"
                            ElseIf objXmlNode.Attributes(i).Name = "FOLLOWUP_REMARKS" Then
                                strArray(index) = "FOLLOWUP REMARKS"
                            ElseIf objXmlNode.Attributes(i).Name = "PREV_REMARKS1" Then
                                strArray(index) = "PREV REMARKS-I"
                            ElseIf objXmlNode.Attributes(i).Name = "PREV_REMARKS2" Then
                                strArray(index) = "PREV REMARKS-II"
                            ElseIf objXmlNode.Attributes(i).Name = "JOINTCALLREMARKS" Then
                                strArray(index) = "JOINT CALL REMARKS"
                            ElseIf objXmlNode.Attributes(i).Name = "STAFFNAME" Then
                                strArray(index) = "ACCOMPANIED BY COLLEAGUE"
                            ElseIf objXmlNode.Attributes(i).Name = "MIDT" Then
                                strArray(index) = "POTENTIAL"
                            ElseIf objXmlNode.Attributes(i).Name = "A1DAILYMOTIVES" Then
                                strArray(index) = "1 A DAILYMOTIVES"
                            ElseIf objXmlNode.Attributes(i).Name = "BCMINSEGMENT" Then
                                strArray(index) = "MINIMUM SEGMENT"
                            ElseIf objXmlNode.Attributes(i).Name = "BCCOMMIT" Then
                                strArray(index) = "COMMITMENT"
                                'SequenceItem.Add("SR_ASSIGNTO")
                                'SequenceItem.Add("ST_ASSIGNTO")
                            ElseIf objXmlNode.Attributes(i).Name = "SR_ASSIGNTO" Then
                                strArray(index) = "SERVICE CALL ASSIGN TO"
                            ElseIf objXmlNode.Attributes(i).Name = "ST_ASSIGNTO" Then
                                strArray(index) = "STRATEGIC CALL ASSIGN TO"
                            ElseIf objXmlNode.Attributes(i).Name = "LOGMANAGERNAME" Then
                                strArray(index) = "LOGGED BY MANAGER"
                            ElseIf objXmlNode.Attributes(i).Name = "DATE" Then
                                strArray(index) = "VISIT DATE"
                            ElseIf objXmlNode.Attributes(i).Name = "DSR_VISIT_ID" Then
                                strArray(index) = "DSR CODE"

                            ElseIf objXmlNode.Attributes(i).Name = "ST_DEPARTMENT" Then
                                strArray(index) = "DEPARTMENT"
                            ElseIf objXmlNode.Attributes(i).Name = "SC_DEPARTMENT_SPECIFIC" Then
                                strArray(index) = "DEPARTMENT SPECIFIC"
                            ElseIf objXmlNode.Attributes(i).Name = "ST_TARGET_CLOSERDATE" Then
                                strArray(index) = "TARGET CLOSER DATE"
                            ElseIf objXmlNode.Attributes(i).Name = "ST_CLOSERDATE" Then
                                strArray(index) = "COLOSER DATE"
                            ElseIf objXmlNode.Attributes(i).Name = "ST_RETENTION_REASON" Then
                                strArray(index) = "RETENTION REASON"
                            ElseIf objXmlNode.Attributes(i).Name = "ST_CPS" Then
                                strArray(index) = "CPS"
                            ElseIf objXmlNode.Attributes(i).Name = "ST_NEWCPS" Then
                                strArray(index) = "NEW CPS"
                            ElseIf objXmlNode.Attributes(i).Name = "ST_A1APPROVED_NEW_DEAL" Then
                                strArray(index) = "1A APPROVAL NEW DEAL"
                            ElseIf objXmlNode.Attributes(i).Name = "ST_STR_SIGNON_DATE" Then
                                strArray(index) = "SIGNON/CONVERSION DATE"
                            ElseIf objXmlNode.Attributes(i).Name = "ST_STR_INSTALLATION_DATE" Then
                                strArray(index) = "INSTALLATION DATE"
                            ElseIf objXmlNode.Attributes(i).Name = "ST_STR_PRODUCT" Then
                                strArray(index) = "PRODUCT"
                            ElseIf objXmlNode.Attributes(i).Name = "ST_STR_REVENUE" Then
                                strArray(index) = "REVENUE"
                            ElseIf objXmlNode.Attributes(i).Name = "ST_STR_TARGET_SEG" Then
                                strArray(index) = "TARGET SEGS % OF BUSINESS"
                            ElseIf objXmlNode.Attributes(i).Name = "MIDT_LATEST" Then
                                strArray(index) = "POTENTIAL LATEST"
                            ElseIf objXmlNode.Attributes(i).Name = "BIDT_LATEST" Then
                                strArray(index) = "BIDT LATEST"
                            Else
                                strArray(index) = objXmlNode.Attributes(i).Name
                            End If
                        End If
                    Next i
                Next index


                'For i As Integer = 0 To objXmlNode.Attributes.Count - 1
                '    intArray(i) = i
                '    If objXmlNode.Attributes(i).Name = "RESP_NAME" Then
                '        strArray(i) = "SALES EXECUTIVE"
                '    ElseIf objXmlNode.Attributes(i).Name = "CHAIN_CODE" Then
                '        strArray(i) = "CHAIN CODE"
                '    ElseIf objXmlNode.Attributes(i).Name = "CITY_NAME" Then
                '        strArray(i) = "CITY NAME"
                '    ElseIf objXmlNode.Attributes(i).Name = "PLAN_VISIT_DONE" Then
                '        strArray(i) = "PLAN VISIT DONE"
                '    ElseIf objXmlNode.Attributes(i).Name = "UNPLAN_VISIT_DONE" Then
                '        strArray(i) = "UNPLAN VISIT DONE"
                '    ElseIf objXmlNode.Attributes(i).Name = "VISIT_TYPE_NAME" Then
                '        strArray(i) = "VISIT SUB TYPE"
                '    ElseIf objXmlNode.Attributes(i).Name = "MANAGERNAME" Then
                '        strArray(i) = "ACCOMPANIED BY MANAGER"
                '    ElseIf objXmlNode.Attributes(i).Name = "HODNAME" Then
                '        strArray(i) = "ACCOMPANIED BY REPORTING MANAGER"

                '    ElseIf objXmlNode.Attributes(i).Name = "GROUP_CATG_NAME" Then
                '        strArray(i) = "TYPE"
                '    ElseIf objXmlNode.Attributes(i).Name = "AGENCY_NAME" Then
                '        strArray(i) = "AGENCY NAME"
                '    ElseIf objXmlNode.Attributes(i).Name = "REASON_REMARKS" Then
                '        strArray(i) = "REASON REMARKS"
                '    ElseIf objXmlNode.Attributes(i).Name = "OBJ_VISITCOUNT" Then
                '        strArray(i) = "SALES OBJECTIVE"
                '    ElseIf objXmlNode.Attributes(i).Name = "DISCUSSION_ISSUE_REMARKS" Then
                '        strArray(i) = "DETAILED DISCUSSION/ ISSUE REPORTED"
                '    ElseIf objXmlNode.Attributes(i).Name = "COMPETITION_MKT_INFO_REMARKS" Then
                '        strArray(i) = "COMPETITION INFO/MKT INFO REMARKS"
                '    ElseIf objXmlNode.Attributes(i).Name = "STATUS_NAME" Then
                '        strArray(i) = "STATUS"
                '    ElseIf objXmlNode.Attributes(i).Name = "FOLLOWUP_REMARKS" Then
                '        strArray(i) = "FOLLOWUP REMARKS"
                '    ElseIf objXmlNode.Attributes(i).Name = "PREV_REMARKS1" Then
                '        strArray(i) = "PREV REMARKS2"
                '    ElseIf objXmlNode.Attributes(i).Name = "PREV_REMARKS2" Then
                '        strArray(i) = "PREV REMARKS2"
                '    ElseIf objXmlNode.Attributes(i).Name = "DATE" Then
                '        strArray(i) = "VISIT DATE"
                '    Else
                '        strArray(i) = objXmlNode.Attributes(i).Name
                '    End If

                'Next


                'TOTAL NODE FOOTER
                Dim objXmlNodeClone As XmlNode
                objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("DSRLOG")
                objXmlNodeClone = objXmlNode.CloneNode(True)

                For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                    XmlAttr.Value = ""
                Next
                ' <PAGE_TOTAL BIDT="1144" MIDT="548" A1DAILYMOTIVES="0" OBJ_VISITCOUNT="68" SEGSTARGET="0" VISITTARGET="0" PLAN_VISIT_DONE="0" UNPLAN_VISIT_DONE="0" BCMINSEGMENT="176" />
                With objXmlNodeClone
                    If objXmlNodeClone.Attributes("AGENCY_NAME") IsNot Nothing Then
                        objXmlNodeClone.Attributes("AGENCY_NAME").Value = "Total"
                    End If
                    If objXmlNodeClone.Attributes("BIDT") IsNot Nothing Then
                        objXmlNodeClone.Attributes("BIDT").Value = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("BIDT").Value
                    End If
                    If objXmlNodeClone.Attributes("MIDT") IsNot Nothing Then
                        objXmlNodeClone.Attributes("MIDT").Value = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("MIDT").Value
                    End If
                    If objXmlNodeClone.Attributes("A1DAILYMOTIVES") IsNot Nothing Then
                        objXmlNodeClone.Attributes("A1DAILYMOTIVES").Value = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("A1DAILYMOTIVES").Value
                    End If
                    If objXmlNodeClone.Attributes("SEGSTARGET") IsNot Nothing Then
                        objXmlNodeClone.Attributes("SEGSTARGET").Value = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("SEGSTARGET").Value
                    End If
                    If objXmlNodeClone.Attributes("OBJ_VISITCOUNT") IsNot Nothing Then
                        objXmlNodeClone.Attributes("OBJ_VISITCOUNT").Value = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("OBJ_VISITCOUNT").Value
                    End If
                    If objXmlNode.Attributes("VISITTARGET") IsNot Nothing Then
                        objXmlNodeClone.Attributes("VISITTARGET").Value = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("VISITTARGET").Value
                    End If
                    If objXmlNode.Attributes("PLAN_VISIT_DONE") IsNot Nothing Then
                        objXmlNodeClone.Attributes("PLAN_VISIT_DONE").Value = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("PVISITDONE").Value
                    End If
                    If objXmlNodeClone.Attributes("UNPLAN_VISIT_DONE") IsNot Nothing Then

                        objXmlNodeClone.Attributes("UNPLAN_VISIT_DONE").Value = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("UVISITDONE").Value
                    End If
                    If objXmlNodeClone.Attributes("BCMINSEGMENT") IsNot Nothing Then

                        objXmlNodeClone.Attributes("BCMINSEGMENT").Value = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("BCMINSEGMENT").Value
                    End If

                End With

                objOutputXml.DocumentElement.AppendChild(objXmlNodeClone)








                objExport.ExportDetails(objOutputXml, "DSRLOG", intArray, strArray, ExportExcel.ExportFormat.Excel, "DSR.xls")
            Else
                objOutputXml.LoadXml("<pp> <DSRLOG  DATE='' AGENCY_NAME='' ADDRESS='' LCODE='' 	OFFICEID='' DESIGNATION='' PERSONMET=''  BCCOMMIT='' BCMINSEGMENT='' 	MANAGERNAME=''  HODNAME=''  CITY_NAME='' CHAIN_CODE='' 	A1DAILYMOTIVES='' MIDT='' BIDT='' VISITED='' REASON_REMARKS='' OBJ_VISITCOUNT='' 	PLAN_VISIT_DONE='' UNPLAN_VISIT_DONE='' VISIT_TYPE_NAME='' DISCUSSION_ISSUE_REMARKS='' 		COMPETITION_MKT_INFO_REMARKS='' STATUS_NAME='' FOLLOWUP_REMARKS='' PREV_REMARKS1='' 		PREV_REMARKS2='' INTIME='' OUTTIME='' GROUP_CATG_NAME='' LOGDATE='' RESP_NAME=''  /></pp>")

                objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("DSRLOG")
                Dim strArray(objXmlNode.Attributes.Count - 1) As String
                Dim intArray(objXmlNode.Attributes.Count - 1) As Integer
                Dim j As Integer = 0
                ' DSRLOG ROWID="1" DSR_VISIT_ID="12" DATE="2/1/2010" AGENCY_NAME="Cat Air Pvt Ltd" ADDRESS="Dhondusa Complex," LCODE="264" OFFICEID="BLRWI2124" DESIGNATION="DESIGNATION" PERSONMET="CONTACT_NAME" PENDINGISSUES="" BCCOMMIT="" BCMINSEGMENT="" MANAGERID="0" MANAGERNAME="" HODID="" HODNAME="" CITYID="124" CITY_NAME="Bangalore" CHAIN_CODE="606" A1DAILYMOTIVES="0" MIDT="0" BIDT="0" VISITED="" REASON_REMARKS="" OBJ_VISITCOUNT="" PLAN_VISIT_DONE="" UNPLAN_VISIT_DONE="" VISIT_TYPE_NAME="" DISCUSSION_ISSUE_REMARKS="" COMPETITION_MKT_INFO_REMARKS="" STATUS_NAME="" FOLLOWUP_REMARKS="" PREV_REMARKS1="" PREV_REMARKS2="" INTIME="" OUTTIME="" GROUP_CATG_NAME="" VISITCOUNT="" LOGDATE="5/21/2011 11:55:32 AM" RESP_NAME="Admin" RESP_1A="24" UNPLANVISIT=""
                For i As Integer = 0 To objXmlNode.Attributes.Count - 1
                    intArray(i) = i
                    If objXmlNode.Attributes(i).Name = "RESP_NAME" Then
                        strArray(i) = "SALES EXECUTIVE"
                    ElseIf objXmlNode.Attributes(i).Name = "CHAIN_CODE" Then
                        strArray(i) = "CHAIN CODE"
                    ElseIf objXmlNode.Attributes(i).Name = "CITY_NAME" Then
                        strArray(i) = "CITY NAME"
                    ElseIf objXmlNode.Attributes(i).Name = "PLAN_VISIT_DONE" Then
                        strArray(i) = "PLAN VISIT DONE"
                    ElseIf objXmlNode.Attributes(i).Name = "UNPLAN_VISIT_DONE" Then
                        strArray(i) = "UNPLAN VISIT DONE"
                    ElseIf objXmlNode.Attributes(i).Name = "VISIT_TYPE_NAME" Then
                        strArray(i) = "VISIT SUB TYPE"
                    ElseIf objXmlNode.Attributes(i).Name = "HODNAME" Then
                        strArray(i) = "ACCOMPANIED BY MANAGER"
                    ElseIf objXmlNode.Attributes(i).Name = "MANAGERNAME" Then
                        strArray(i) = "ACCOMPANIED BY REPORTING MANAGER"

                    ElseIf objXmlNode.Attributes(i).Name = "GROUP_CATG_NAME" Then
                        strArray(i) = "TYPE"
                    ElseIf objXmlNode.Attributes(i).Name = "AGENCY_NAME" Then
                        strArray(i) = "AGENCY NAME"
                    ElseIf objXmlNode.Attributes(i).Name = "REASON_REMARKS" Then
                        strArray(i) = "REASON FOR NO VISIT AS PER PLANNED VISIT"
                    ElseIf objXmlNode.Attributes(i).Name = "OBJ_VISITCOUNT" Then
                        strArray(i) = "SALES OBJECTIVE VISIT"
                    ElseIf objXmlNode.Attributes(i).Name = "DISCUSSION_ISSUE_REMARKS" Then
                        strArray(i) = "DETAILED DISCUSSION/ ISSUE REPORTED"
                    ElseIf objXmlNode.Attributes(i).Name = "COMPETITION_MKT_INFO_REMARKS" Then
                        strArray(i) = "COMPETITION INFO/MKT INFO REMARKS"
                    ElseIf objXmlNode.Attributes(i).Name = "STATUS_NAME" Then
                        strArray(i) = "STATUS"
                    ElseIf objXmlNode.Attributes(i).Name = "FOLLOWUP_REMARKS" Then
                        strArray(i) = "FOLLOWUP REMARKS"
                    ElseIf objXmlNode.Attributes(i).Name = "PREV_REMARKS1" Then
                        strArray(i) = "PREV REMARKS2"
                    ElseIf objXmlNode.Attributes(i).Name = "PREV_REMARKS2" Then
                        strArray(i) = "PREV REMARKS2"
                    Else
                        strArray(i) = objXmlNode.Attributes(i).Name
                    End If

                Next
                objExport.ExportDetails(objOutputXml, "DSRLOG", intArray, strArray, ExportExcel.ExportFormat.Excel, "DSR.xls")
            End If
        End If
    End Sub
    Private Sub exportDSRAction(ByVal objOutputXml As XmlDocument)
        Dim objExport As New ExportExcel
        Dim objXmlNode As XmlNode
        Dim NotShowColumnArray As New ArrayList

        '<DSRACTIONDETAILS DSRCODE='1'  DSR_DETAIL_ID='11'  LCODE='' CHAIN_CODE='' NAME='' ADDRESS=''  OFFICEID='' AOFFICE='' TARGET_CLOSER_DATETIME='' DSR_DATETIME='' STATUS='' ASSIGNEDBYID=''  ASSIGNEDBYNAME=''  ASSIGNEDTOID=''  ASSIGNEDTONAME='' VISITSUBTYPE ='' STRATEGICTYPE='' DEPARTMENT='' REGION=''  /> 
        '<DSRACTIONDETAILS DSRCODE='2'  DSR_DETAIL_ID='12'  LCODE='' CHAIN_CODE='' NAME='' ADDRESS=''  OFFICEID='' AOFFICE='' TARGET_CLOSER_DATETIME='' DSR_DATETIME='' STATUS='' ASSIGNEDBYID=''  ASSIGNEDBYNAME=''  ASSIGNEDTOID=''  ASSIGNEDTONAME='' VISITSUBTYPE ='' STRATEGICTYPE='' DEPARTMENT='' REGION=''  /> 
        If Session("NotShowColumnArrayForDSRAction") IsNot Nothing Then
            NotShowColumnArray = CType(Session("NotShowColumnArrayForDSRAction"), ArrayList)
        End If
        'NotShowColumnArray.Add("DSR_DETAIL_ID")
        'NotShowColumnArray.Add("LCODE")
        'NotShowColumnArray.Add("CHAIN_CODE")
        'NotShowColumnArray.Add("ADDRESS")
        'NotShowColumnArray.Add("ASSIGNEDBYID")
        'NotShowColumnArray.Add("ASSIGNEDBYNAME")
        'NotShowColumnArray.Add("ASSIGNEDTOID")
        'NotShowColumnArray.Add("ASSIGNEDTONAME")
        'NotShowColumnArray.Add("VISITSUBTYPE")
        'NotShowColumnArray.Add("STRATEGICTYPE")
        'NotShowColumnArray.Add("DEPARTMENT")
        'NotShowColumnArray.Add("REGION")



        '@ Start of Code For Removing Attribute which is not to show on Export
        For i As Integer = 0 To NotShowColumnArray.Count - 1
            For Each objNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("DSRACTIONDETAILS")
                objNode.Attributes.RemoveNamedItem(NotShowColumnArray(i).ToString)
            Next
        Next
        '@ End of Code For Removing Attribute which is not to show on Export

        objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("DSRACTIONDETAILS")

        Dim strArray(objXmlNode.Attributes.Count - 1) As String
        Dim intArray(objXmlNode.Attributes.Count - 1) As Integer
        Dim j As Integer = 0

        '@ Start of  Making Sequence ItemList According To Grid showing
        Dim SequenceItem As New ArrayList()
        SequenceItem.Add("DSRCODE")
        SequenceItem.Add("LCODE")
        SequenceItem.Add("NAME")
        SequenceItem.Add("OFFICEID")
        SequenceItem.Add("AOFFICE")
        SequenceItem.Add("TARGET_CLOSER_DATETIME")
        SequenceItem.Add("DSR_DATETIME")
        SequenceItem.Add("STATUS")
        SequenceItem.Add("ASSIGNEDBYNAME")
        SequenceItem.Add("DEPARTMENT")
        SequenceItem.Add("ASSIGNEDTONAME")
        SequenceItem.Add("VISITSUBTYPE")
        SequenceItem.Add("REGION")

        Dim counter As Integer = SequenceItem.Count - 1

        For i As Integer = 0 To counter
            For k As Integer = 0 To SequenceItem.Count - 1
                If objXmlNode.Attributes(SequenceItem(k).ToString) Is Nothing Then
                    SequenceItem.Remove(SequenceItem(k).ToString)
                    Exit For
                End If
            Next k
        Next i
        '@ End of  Making Sequence ItemList According To Grid showing

        For index As Integer = 0 To SequenceItem.Count - 1
            For i As Integer = 0 To objXmlNode.Attributes.Count - 1
                If SequenceItem(index).ToString = objXmlNode.Attributes(i).Name.ToString Then
                    intArray(index) = i
                    If objXmlNode.Attributes(i).Name = "DSRCODE" Then
                        strArray(index) = "DSR CODE"
                    ElseIf objXmlNode.Attributes(i).Name = "LCODE" Then
                        strArray(index) = "LCODE"
                    ElseIf objXmlNode.Attributes(i).Name = "NAME" Then
                        strArray(index) = "AGENCY NAME"
                    ElseIf objXmlNode.Attributes(i).Name = "OFFICEID" Then
                        strArray(index) = "OFFICEID"
                    ElseIf objXmlNode.Attributes(i).Name = "AOFFICE" Then
                        strArray(index) = "AOFFICE"
                    ElseIf objXmlNode.Attributes(i).Name = "TARGET_CLOSER_DATETIME" Then
                        strArray(index) = "TARGET COSER DATE"
                    ElseIf objXmlNode.Attributes(i).Name = "DSR_DATETIME" Then
                        strArray(index) = "DSR DATE"
                    ElseIf objXmlNode.Attributes(i).Name = "STATUS" Then
                        strArray(index) = "STATUS"
                    ElseIf objXmlNode.Attributes(i).Name = "ASSIGNEDBYNAME" Then
                        strArray(index) = "ASSIGNED BY NAME"
                    ElseIf objXmlNode.Attributes(i).Name = "ASSIGNEDTONAME" Then
                        strArray(index) = "ASSIGNED TO NAME"
                    ElseIf objXmlNode.Attributes(i).Name = "VISITSUBTYPE" Then
                        strArray(index) = "VISIT SUB TYPE"
                    ElseIf objXmlNode.Attributes(i).Name = "REGION" Then
                        strArray(index) = "REGION"
                    Else
                        strArray(index) = objXmlNode.Attributes(i).Name
                    End If
                End If
            Next i
        Next index

        For Each objTempxmlnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("DSRACTIONDETAILS")
            objTempxmlnode.Attributes("DSR_DATETIME").Value = ConvertToCalenderDate(objTempxmlnode.Attributes("DSR_DATETIME").Value)
        Next

        objExport.ExportDetails(objOutputXml, "DSRACTIONDETAILS", intArray, strArray, ExportExcel.ExportFormat.Excel, "DSRACTION.xls")
    End Sub
#Region "ConvertToCalenderDate(ByVal strDateInInterger As String) As String"
    Private Function ConvertToCalenderDate(ByVal strDateInInterger As String) As String
        Try
            If strDateInInterger.Trim.Length = 8 Then
                strDateInInterger = strDateInInterger.Substring(6, 2) + "/" + strDateInInterger.Substring(4, 2) + "/" + strDateInInterger.Substring(0, 4)
            End If
        Catch ex As Exception
        End Try
        Return strDateInInterger
    End Function
#End Region
End Class
