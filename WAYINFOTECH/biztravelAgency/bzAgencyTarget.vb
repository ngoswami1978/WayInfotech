'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Tapan $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizTravelAgency/bzAgencyTarget.vb $
'$Workfile: bzAgencyTarget.vb $
'$Revision: 51 $
'$Archive: /AAMS/Components/bizTravelAgency/bzAgencyTarget.vb $
'$Modtime: 6/07/11 3:36p $
Imports System.Xml
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports AAMS.bizShared
Imports System.Math
Namespace bizTravelAgency
    Public Class bzAgencyTarget
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzAgencyTarget"
        Const strAGENCYEMPLIST_INPUT = "<PR_AGENCYEMPLIST_INPUT><City_Id></City_Id><PR_AGENCYEMPLIST_INPUT>"
        Const strAGENCYEMPLIST_OUTPUT = "<PR_AGENCYEMPLIST_OUTPUT><TARGET SalesManId='' SalesManName=''/><Errors Status=''><Error Code='' Description=''/></Errors></PR_AGENCYEMPLIST_OUTPUT>"
        'Paging
        'Const strSEARCH_INPUT = "<TA_AGENCYTARGET_INPUT><City_Id></City_Id><CityName></CityName><Year></Year><Month></Month><SalesManNameId></SalesManNameId><RESP_1A></RESP_1A><PAGE_NO></PAGE_NO><PAGE_SIZE></PAGE_SIZE><SORT_BY></SORT_BY><DESC>FALSE</DESC></TA_AGENCYTARGET_INPUT>"
        'Const strSEARCH_OUTPUT = "<TA_AGENCYTARGET_OUTPUT><TARGET LCode=''  Year='' Month=''  AgencyName='' OfficeId='' Address='' SalesPersonId='' SalesManName='' Target='' /><PAGE PAGE_COUNT='' TOTAL_ROWS='' /><PAGE_TOTAL TARGET='' /><Errors Status=''><Error Code='' Description='' /></Errors></TA_AGENCYTARGET_OUTPUT>"
        'end paging
        Const strSEARCH_INPUT = "<TA_AGENCYTARGET_INPUT><City_Id></City_Id><CityName></CityName><Year></Year><Month></Month><SalesManNameId></SalesManNameId><RESP_1A></RESP_1A></TA_AGENCYTARGET_INPUT>"
        Const strSEARCH_OUTPUT = "<TA_AGENCYTARGET_OUTPUT><TARGET LCode='' Year='' Month='' AgencyName='' OfficeId='' Address='' SalesPersonId='' SalesManName='' LoginId='' Target=''/><Total/><Errors Status=''><Error Code='' Description='' /></Errors></TA_AGENCYTARGET_OUTPUT>"
        Const strUPDATE_INPUT = "<TA_UPDATEAGENCYTARGET_INPUT><TARGET   Action='' LCode='' Year='' Month='' Target='' SalesPersonId='' LoginId='' /></TA_UPDATEAGENCYTARGET_INPUT>"
        Const strUPDATE_OUTPUT = "<TA_UPDATEAGENCYTARGET_OUTPUT><TARGET Action='' LCode='' Year='' Month='' Target='' SalesPersonId='' LoginId='' /><Errors Status=''><Error Code='' Description='' /></Errors></TA_UPDATEAGENCYTARGET_OUTPUT>"
        Const strHISTORY_INPUT = "<TA_HISTORY_AGENCYTARGET_INPUT><TARGET Action='' LCode='' Year='' Month='' SalesPersonId='' Resp_1a='' /></TA_HISTORY_AGENCYTARGET_INPUT>"
        Const strHISTORY_OUTPUT = "<TA_HISTORY_AGENCYTARGET_OUTPUT><TARGET  Resp_1a='' LCode=''  Year='' Month='' EmpName='' SalesPersonId='' ChangedData='' Date='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description=''/></Errors></TA_HISTORY_AGENCYTARGET_OUTPUT>"
        Const str_SearchXml1_INPUT = "<TA_AGENCYTARGET_INPUT><LCode></LCode><Year></Year><Month></Month><SalesPersonId></SalesPersonId><Resp_1a></Resp_1a></TA_AGENCYTARGET_INPUT>"
        Const str_SearchXml1_OUTPUT = "<TA_AGENCYTARGET_OUTPUT><TARGET LCode=''  Year='' Month='' SalesPersonId='' LoginId='' Target=''/><Errors Status=''><Error Code='' Description='' /></Errors></TA_AGENCYTARGET_OUTPUT>"
        'Const STR_Search_Prev_Target_INPUT = "<TA_AGENCYTARGET_PREV_MONTH_INPUT><City_Id></City_Id><CityName></CityName><Year></Year><Month></Month><SalesManNameId></SalesManNameId><RESP_1A></RESP_1A><PAGE_NO></PAGE_NO><PAGE_SIZE></PAGE_SIZE><SORT_BY></SORT_BY><DESC>FALSE</DESC><TA_AGENCYTARGET_PREV_MONTH_INPUT>"
        Const STR_Search_Prev_Target_INPUT = "<TA_AGENCYTARGET_PREV_MONTH_INPUT><City_Id></City_Id><CityName></CityName><Year></Year><Month></Month><SalesManNameId></SalesManNameId><RESP_1A></RESP_1A><TA_AGENCYTARGET_PREV_MONTH_INPUT>"
        Const STR_Search_Prev_Target_OUTPUT = "<TA_AGENCYTARGET_PREV_MONTH_OUTPUT><TARGET LCode='' Year='' Month='' AgencyName='' OfficeId='' Address='' SalesPersonId='' SalesManName='' LoginId='' Target=''/><Total/><Errors Status='FALSE'><Error Code='' Description=''/></Errors></TA_AGENCYTARGET_PREV_MONTH_OUTPUT>"
        Const str_Search_TargetValue_Input = "<TA_AGENCYTARGET_VALUE_INPUT><LCode><Lcode><CityName></CityName><Year></Year><Month></Month><SalesManNameId></SalesManNameId><Target></Target><Increment></Increment><Decrement></Decrement><TargetValuePer></TargetValuePer></TA_AGENCYTARGET_VALUE_INPUT>"
        Const str_Search_TargetValue_Output = "<TA_AGENCYTARGET_VALUE_OUTPUT><TARGET LCode='' Year='' Month='' AgencyName='' OfficeId='' Address='' SalesPersonId='' SalesManName='' Target='' LoginId='' /><Total/><Errors Status=''><Error Code='' Description=''/></Errors></TA_AGENCYTARGET_VALUE_OUTPUT>"
        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add

        End Function
        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Delete



        End Function
        Public Function Search(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Search
            'Purpose:This function gives search results based on choosen search criterion.
            'Input  :- 
            '<TA_AGENCYTARGET_INPUT>
            '	<City_Id></City_Id>
            '   <CityName></CityName>
            '	<Year></Year>
            '	<Month></Month>
            '	<SalesManNameId><SalesManNameId>
            '   <Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency>            
            '<TA_AGENCYTARGET_INPUT>


            'Output:
            '<TA_AGENCYTARGET_OUTPUT>
            '	<TARGET LCode=''  Year='' Month=''  AgencyName='' OfficeId='' Address='' SalesPersonId='' SalesManName='' Target='' />
            '	<CityName/>
            '   <PAGE_TOTAL TARGET='' />
            '	<PAGE PAGE_COUNT='' TOTAL_ROWS='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</TA_AGENCYTARGET_OUTPUT>
            '-----------------------------------------------------------------------------
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            'Call connection()
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim intCityId As Integer
            Dim strCityName As String
            Dim intYear As Integer
            Dim intMonth As Integer
            Dim intSalesManNameId As Integer
            Dim intRESP_1A As Integer
            Dim intTargetValuePer As Integer
            Dim intCalc As Integer
            Dim intIncrement As Integer
            Dim intDecrement As Integer
            Dim TargetValuePer As Integer
            Dim intTotal As Long
            Dim tot As Long


            ''paging
            'Dim intPageNo, intPageSize As Integer
            'Dim strSortBy As String
            'Dim blnDesc As Boolean
            ''end paging

            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                If SearchDoc.DocumentElement.SelectSingleNode("CityName").InnerText.Trim() <> "" Then
                    strCityName = SearchDoc.DocumentElement.SelectSingleNode("CityName").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("Year").InnerText.Trim() <> "" Then
                    intYear = SearchDoc.DocumentElement.SelectSingleNode("Year").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("Month").InnerText.Trim() <> "" Then
                    intMonth = SearchDoc.DocumentElement.SelectSingleNode("Month").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("SalesManNameId").InnerText.Trim() <> "" Then
                    intSalesManNameId = SearchDoc.DocumentElement.SelectSingleNode("SalesManNameId").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("RESP_1A").InnerText.Trim() <> "" Then
                    intRESP_1A = SearchDoc.DocumentElement.SelectSingleNode("RESP_1A").InnerText.Trim() ''Doubt
                End If

                Dim strLimitedToAoffice As String = ""
                Dim intLimitedToRegion, intLimitedToOwnAgency As Integer


                If SearchDoc.DocumentElement.SelectSingleNode("Limited_To_Region") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.Trim() = "" Then
                        intLimitedToRegion = 0
                    Else
                        intLimitedToRegion = SearchDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.Trim()
                    End If
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("Limited_To_Aoffice") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText.Trim() <> "" Then
                        strLimitedToAoffice = SearchDoc.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText.Trim()
                    End If
                End If


                If SearchDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAagency") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText.Trim() = "" Then
                        intLimitedToOwnAgency = 0
                    Else
                        intLimitedToOwnAgency = SearchDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText.Trim()
                    End If
                End If


                '--------Retrieving & Checking Details from Input XMLDocument ------End
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_SER_TA_SALESMAN_TARGET]"
                    .Connection = objSqlConnection
                    '.Connection = con


                    .Parameters.Add("@CityName", SqlDbType.Char, 100)
                    If Len(strCityName) <= 0 Then
                        .Parameters("@CityName").Value = DBNull.Value
                    Else
                        .Parameters("@CityName").Value = strCityName
                    End If


                    .Parameters.Add("@YEAR", SqlDbType.Int)
                    If intYear = 0 Then
                        .Parameters("@YEAR").Value = DBNull.Value
                    Else
                        .Parameters("@YEAR").Value = intYear
                    End If


                    .Parameters.Add("@MONTH", SqlDbType.Int)
                    If intMonth = 0 Then
                        .Parameters("@MONTH").Value = DBNull.Value
                    Else
                        .Parameters("@MONTH").Value = intMonth
                    End If


                    .Parameters.Add("@SALESPERSON", SqlDbType.Int)
                    If intSalesManNameId = 0 Then
                        .Parameters("@SALESPERSON").Value = DBNull.Value
                    Else
                        .Parameters("@SALESPERSON").Value = intSalesManNameId
                    End If


                    .Parameters.Add("@RESP_1A", SqlDbType.BigInt)
                    If intSalesManNameId = 0 Then
                        .Parameters("@RESP_1A").Value = DBNull.Value
                    Else
                        .Parameters("@RESP_1A").Value = intSalesManNameId
                    End If


                  .Parameters.Add("@LIMITED_TO_AOFFICE", SqlDbType.Char, 3)

                   If Len(strLimitedToAoffice) <= 0 Then
                    .Parameters("@LIMITED_TO_AOFFICE").Value = DBNull.Value
                   Else
                    .Parameters("@LIMITED_TO_AOFFICE").Value = strLimitedToAoffice

                   End If

                    .Parameters.Add("@LIMITED_TO_REGION", SqlDbType.Int)
                    If intLimitedToRegion = 0 Then
                        .Parameters("@LIMITED_TO_REGION").Value = DBNull.Value
                    Else
                        .Parameters("@LIMITED_TO_REGION").Value = intLimitedToRegion
                    End If

                    .Parameters.Add("@LIMITED_TO_OWNAAGENCY", SqlDbType.Int) 'Location_code
                    If intLimitedToOwnAgency = 0 Then
                        .Parameters("@LIMITED_TO_OWNAAGENCY").Value = DBNull.Value
                    Else
                        .Parameters("@LIMITED_TO_OWNAAGENCY").Value = intLimitedToOwnAgency
                    End If

                    Dim strEmployeeID As String = ""
                    If SearchDoc.DocumentElement.SelectSingleNode("EmployeeID") IsNot Nothing Then
                        If SearchDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim() <> "" Then
                            strEmployeeID = SearchDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                        End If
                    End If


                    .Parameters.Add("@EmployeeID", SqlDbType.Int)
                    If strEmployeeID = "" Then
                        .Parameters("@EmployeeID").Value = DBNull.Value
                    Else
                        .Parameters("@EmployeeID").Value = strEmployeeID
                    End If

                End With

                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("TARGET")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    '  '	<TARGET LCode=''  Year='' Month=''  AgencyName='' OfficeId='' Address='' SalesPersonId='' SalesManName='' Target='' />
                    objAptNodeClone.Attributes("LCode").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Lcode")))
                    objAptNodeClone.Attributes("Year").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("YEAR")) & "")
                    objAptNodeClone.Attributes("Month").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MONTH")) & "")
                    objAptNodeClone.Attributes("AgencyName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AgencyName")))
                    objAptNodeClone.Attributes("OfficeId").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OfficeId")) & "")
                    objAptNodeClone.Attributes("Address").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Address")) & "")
                    objAptNodeClone.Attributes("SalesPersonId").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SalesPersonId")) & "")
                    objAptNodeClone.Attributes("SalesManName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SalesPerson")) & "")
                    objAptNodeClone.Attributes("LoginId").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LoginId")) & "")
                    objAptNodeClone.Attributes("Target").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Target")) & "")


                    tot = tot + Val(objSqlReader.GetValue(objSqlReader.GetOrdinal("Target")) & "")

                    objOutputXml.DocumentElement.SelectSingleNode("Total").InnerText = tot.ToString
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop

                'objOutputXml.DocumentElement.SelectSingleNode("Total").InnerText = intAllTotal
                objOutputXml.DocumentElement.SelectSingleNode("Total").InnerText = tot.ToString
                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    'objSqlReader.NextResult()
                    'While objSqlReader.Read
                    '    objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("TARGET").InnerText = objSqlReader.GetValue(objSqlReader.GetOrdinal("Target"))
                    'End While
                    'objSqlReader.Close()

                    'objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").InnerText = objSqlCommand.Parameters("@TOTALROWS").Value
                    'If intPageSize = 0 Then
                    '    objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = 0
                    'Else
                    '    objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = Ceiling(objSqlCommand.Parameters("@TOTALROWS").Value / intPageSize)
                    'End If
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                End If

            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                'msgbox(Exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                'If con.State = ConnectionState.Open Then con.Close()

                objSqlCommand.Dispose()
            End Try
            Return objOutputXml

        End Function
        Public Function Update(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Update
            '*************************************************************************************
            ''  ObjAgencyTarget.TargetPerDay = Round(Val(GridArray.value(i, 8)) / intNoOfDays, 0)
            'Round(436 / 30, 0)
            '***********************************************************************
            '<TA_UPDATEAGENCYTARGET_INPUT>
            '	<TARGET Action='' Lcode='' Year='' Month='' Target='' Resp_1a='' />
            '</TA_UPDATEAGENCYTARGET_INPUT>

            '<TA_UPDATEAGENCYTARGET_OUTPUT>
            '	<TARGET Action='' Lcode=''Year='' Month='' Target='' Resp_1a='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</TA_UPDATEAGENCYTARGET_OUTPUT>

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim intRetId As String = ""
            Dim strAction As String = ""
            Dim objUpdateDocOutput As New XmlDocument
            Dim objNode As XmlNode
            Dim intLcode As Integer
            Dim intYear As Integer
            Dim intMonth As Integer
            Dim intTarget As Integer
            Dim intSalesPersonId As Integer

            Dim objTran As New SqlCommand
            Dim objSqlCommand As New SqlCommand
            Dim intRecordsAffected As Int32
            Dim objNode1 As XmlNode
            Dim objXmlAttribute As XmlAttribute
            Dim strAttributeName As String
            Dim blnAssignUpdate As Boolean
            Dim blnMaintainHistory As Boolean
            Dim strChangeData As String = ""
            Dim objHisSqlCommand As New SqlCommand
            Dim i As Integer

            Dim intHisLcode As Integer
            Dim intHisYear As Integer
            Dim intHisMonth As Integer
            Dim intHisRESP_1A As Integer
            Dim strSearchXml As String
            Dim objSearchXml As New XmlDocument
            Dim objHistoryOutputXml As New XmlDocument
            Dim strOnlineStatusAttributes As String
            Dim intLoginId As Integer
            Dim objsqlTran As SqlTransaction

            strOnlineStatusAttributes = "Lcode,"
            Const strMETHOD_NAME As String = "Update"

            '###################################### History ###################################################
            For Each objNode In UpdateDoc.DocumentElement.SelectNodes("TARGET")


                intHisLcode = objNode.Attributes("LCode").InnerText
                intHisYear = Val(objNode.Attributes("Year").InnerText)
                intHisMonth = Val(objNode.Attributes("Month").InnerText)
                intHisRESP_1A = Val(objNode.Attributes("SalesPersonId").InnerText)

                intLoginId = Val(objNode.Attributes("LoginId").InnerText)
                strSearchXml = "<TA_AGENCYTARGET_INPUT><Lcode> " & intHisLcode & " </Lcode><Year>" & intHisYear & "</Year><Month>" & intHisMonth & "</Month><SalesManNameId>" & intHisRESP_1A & "</SalesManNameId></TA_AGENCYTARGET_INPUT>"
                objSearchXml.LoadXml(strSearchXml)
                objHistoryOutputXml = SearchHisByLcode(objSearchXml)

                If objHistoryOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    ' objNode1 = objHistoryOutputXml.DocumentElement.SelectSingleNode("TARGET[@LCode='" + objNode.Attributes("Lcode").Value + "' and @Year='" + objNode.Attributes("Year").Value + "' and @Month='" + objNode.Attributes("Month").Value + "' and @Target='" + objNode.Attributes("Target").Value + "' and @SalesPersonId='" + objNode.Attributes("Resp_1a").Value + "']")
                    objNode1 = objHistoryOutputXml.DocumentElement.SelectSingleNode("TARGET[@LCode='" + objNode.Attributes("LCode").Value + "']")

                    If objNode1 Is Nothing Then
                        Exit For
                    End If

                    i = 0
                    For Each objXmlAttribute In objNode1.Attributes
                        strAttributeName = objXmlAttribute.Name
                        'MsgBox(strAttributeName)

                        If i = objNode1.Attributes.Count Then Exit For
                        If InStr(strAttributeName, "LCode") <> 0 Then
                            If objXmlAttribute.InnerText.Trim <> objNode.Attributes(strAttributeName).InnerText.Trim Then
                                blnAssignUpdate = True
                            End If
                        Else
                            If objXmlAttribute.InnerText.Trim <> objNode.Attributes(strAttributeName).InnerText.Trim Then
                                Select Case LTrim(RTrim(UCase(strAttributeName)))
                                    Case "TARGET"
                                        strChangeData = strChangeData & " Target Value : " & objXmlAttribute.InnerText & " To " & objNode.Attributes(strAttributeName).InnerText
                                        blnMaintainHistory = True
                                        strAction = "U"
                                        Exit For
                                End Select
                            End If
                        End If
                        i = i + 1
                    Next

                    If Len(strChangeData) > 1 Then strChangeData = Mid(strChangeData, 2, strChangeData.Trim.Length)
                    If blnMaintainHistory = True Then
                        objHisSqlCommand = New SqlCommand
                        With objHisSqlCommand
                            .Connection = objSqlConnection
                            '.Connection = con
                            .CommandType = CommandType.StoredProcedure
                            .CommandText = "UP_SRO_TA_AGENCYTARGET_LOG"

                            .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.BigInt))
                            .Parameters("@LCODE").Value = objNode.Attributes("LCode").InnerText

                            .Parameters.Add(New SqlParameter("@YEAR", SqlDbType.Int))
                            .Parameters("@YEAR").Value = objNode.Attributes("Year").InnerText

                            .Parameters.Add(New SqlParameter("@MONTH", SqlDbType.Int))
                            .Parameters("@MONTH").Value = objNode.Attributes("Month").InnerText

                            '' If salesmanId is  blank , then i Need Login Name (ID)

                            .Parameters.Add(New SqlParameter("@EMPLOYEEID", SqlDbType.BigInt))
                            .Parameters("@EMPLOYEEID").Value = intLoginId 'objNode.Attributes("SalesPersonId").InnerText


                            .Parameters.Add(New SqlParameter("@SALESPERSONID", SqlDbType.BigInt))
                            If objNode.Attributes("SalesPersonId").InnerText = "" Then
                                .Parameters("@SALESPERSONID").Value = vbNullString ' objNode.Attributes("SalesPersonId").InnerText
                            Else
                                .Parameters("@SALESPERSONID").Value = objNode.Attributes("SalesPersonId").InnerText
                            End If


                            .Parameters.Add(New SqlParameter("@CHANGEDATA", SqlDbType.VarChar, 2000))
                            .Parameters("@CHANGEDATA").Value = strChangeData
                            If .Connection.State = ConnectionState.Closed Then
                                .Connection.Open()
                            End If
                            intRecordsAffected = .ExecuteNonQuery()

                            If .Connection.State = ConnectionState.Open Then
                                objHisSqlCommand.Connection.Close()
                                objHisSqlCommand.Dispose()
                                blnMaintainHistory = False
                                strChangeData = ""
                                ' objNode = Nothing
                                ' objNode1 = Nothing
                            End If
                        End With
                    End If


                End If

            Next
            ' End If
            '###################################### END Of History ###################################################

            '#######################################  UPDATE #####################################
            Try

                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)

                'For Each objNode In UpdateDoc.DocumentElement.SelectNodes("TARGET")
                Dim objInnerSqlCommand As New SqlCommand
                With UpdateDoc.DocumentElement.SelectSingleNode("TARGET")
                    strAction = "U"

                    If ((.Attributes("LCode").Value).Trim = "") Then
                        intLcode = 0
                    Else
                        intLcode = ((.Attributes("LCode").Value).Trim)
                    End If


                    If ((.Attributes("Year").Value).Trim = "") Then
                        intYear = 0
                    Else
                        intYear = ((.Attributes("Year").Value).Trim)
                    End If


                    If ((.Attributes("Month").Value).Trim = "") Then
                        intMonth = 0
                    Else
                        intMonth = ((.Attributes("Month").Value).Trim)
                    End If


                    If ((.Attributes("Target").Value).Trim = "") Then
                        intTarget = 0
                    Else
                        intTarget = ((.Attributes("Target").Value).Trim)
                    End If

                    '' If salesmanId is  blank , then i Need Login Name (ID) as salesperson
                    If ((.Attributes("SalesPersonId").InnerText).Trim = "") Then
                        intSalesPersonId = vbNullString
                    Else
                        intSalesPersonId = ((.Attributes("SalesPersonId").Value).Trim)
                    End If

                    If strAction = "I" Or strAction = "U" Then
                        If Len(intLcode) <= 0 Then
                            Throw (New AAMSException("Location code can't be blank."))
                        End If

                        If Len(intYear) <= 0 Then
                            Throw (New AAMSException("YEAR can't be blank."))
                        End If

                        If Len(intYear) <= 0 Then
                            Throw (New AAMSException("Month can't be blank."))
                        End If
                    Else
                        Throw (New AAMSException("Invalid Action Code."))
                    End If
                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objInnerSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_SRO_TA_AGENCYTARGET]"

                    .Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1))
                    .Parameters("@Action").Value = "U"

                    .Parameters.Add(New SqlParameter("@Lcode", SqlDbType.BigInt))
                    .Parameters("@Lcode").Value = intLcode

                    .Parameters.Add(New SqlParameter("@YEAR", SqlDbType.Int))
                    .Parameters("@YEAR").Value = intYear

                    .Parameters.Add(New SqlParameter("@MONTH", SqlDbType.Int))
                    .Parameters("@MONTH").Value = intMonth

                    .Parameters.Add(New SqlParameter("@TARGETS", SqlDbType.BigInt))
                    .Parameters("@TARGETS").Value = intTarget

                    .Parameters.Add(New SqlParameter("@INPUTXML", SqlDbType.Xml))
                    .Parameters("@INPUTXML").Value = UpdateDoc.OuterXml

                    .Parameters.Add(New SqlParameter("@RESP_1A", SqlDbType.BigInt))
                    .Parameters("@Resp_1a").Value = DBNull.Value

                    
                End With

                objInnerSqlCommand.Connection.Open()
                objsqlTran = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)
                objInnerSqlCommand.Transaction = objsqlTran
                intRecordsAffected = objInnerSqlCommand.ExecuteNonQuery()

                objsqlTran.Commit()

                If objInnerSqlCommand.Connection.State = ConnectionState.Open Then
                    objInnerSqlCommand.Connection.Close()
                    objInnerSqlCommand.Dispose()
                End If



                With objUpdateDocOutput.DocumentElement.SelectSingleNode("TARGET")
                    .Attributes("LCode").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("TARGET").Attributes("LCode").InnerText
                    .Attributes("Year").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("TARGET").Attributes("Year").InnerText
                    .Attributes("Month").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("TARGET").Attributes("Month").InnerText
                    .Attributes("Target").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("TARGET").Attributes("Target").InnerText
                    .Attributes("SalesPersonId").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("TARGET").Attributes("SalesPersonId").InnerText
                    objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                End With
                'Next


            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                'Fill Error Status
                If Not objTran Is Nothing Then
                    objsqlTran.Rollback()
                End If
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                Return objUpdateDocOutput
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                ' If con.State = ConnectionState.Open Then con.Close()

                objSqlCommand.Dispose()
            End Try
            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Return objUpdateDocOutput

            ''######################################End Here##########################################



        End Function
        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.View

        End Function
        Public Function GetAgencyEmpList(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Input  :
            '<PR_AGENCYEMPLIST_INPUT>
            '<City_Id></City_Id>
            '<PR_AGENCYEMPLIST_INPUT>
            'Output :
            '<PR_AGENCYEMPLIST_OUTPUT>
            '	<TARGET SalesManId="" SalesManName="" />
            '	<Errors Status="">
            '		<Error Code="" Description="" />
            '	</Errors>
            '<PR_AGENCYEMPLIST_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim intCityId As Integer, intEmployeeID As Integer
            objOutputXml.LoadXml(strAGENCYEMPLIST_OUTPUT)

            Const strMETHOD_NAME As String = "GetAgencyEmpList"
            Try

                If UpdateDoc.DocumentElement.SelectSingleNode("City_Id").InnerText <> "" Then
                    intCityId = UpdateDoc.DocumentElement.SelectSingleNode("City_Id").InnerText
                End If


                'If Len(intCityId) = 0 Then
                '    Throw (New AAMSException("City Id can't be blank."))
                'End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LIST_AGENCYTARGET_EMPNAME"
                    .Connection = objSqlConnection
                    '.Connection = con
                    .Parameters.Add(New SqlParameter("@CITYID", SqlDbType.Int))
                    If intCityId = 0 Then
                        .Parameters("@CITYID").Value = DBNull.Value
                    Else
                        .Parameters("@CITYID").Value = intCityId
                    End If


                    ' .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    '.Parameters("@RETURNID").Direction = ParameterDirection.Output
                    '.Parameters("@RETURNID").Value = 0

                End With

                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("TARGET")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("SalesManId").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("employeeid")))
                    objAptNodeClone.Attributes("SalesManName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("employee_name")))
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop

                If (blnRecordFound = False) Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                End If
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch exec As Exception
                MsgBox(exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()

                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function
        Public Function SearchHisByLcode(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            'Purpose:This function gives search results based on choosen search criterion.
            'Input  :- 
            '<TA_AGENCYTARGET_INPUT>
            '	<Lcode></Lcode>
            '	<Year></Year>
            '	<Month></Month>
            '	<Resp_1a><Resp_1a>
            '<TA_AGENCYTARGET_INPUT>

            'Output:
            '<TA_AGENCYTARGET_OUTPUT>
            '	<TARGET LCode=''  Year='' Month=''  AgencyName='' OfficeId='' Address='' SalesPersonId='' SalesManName='' Target='' />
            '	<PAGE PAGE_COUNT='' TOTAL_ROWS='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</TA_AGENCYTARGET_OUTPUT>
            '-----------------------------------------------------------------------------
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            ' Call connection()
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim intLcode As Integer
            Dim intYear As Integer
            Dim intMonth As Integer
            Dim intSalesManNameId As Integer
            Dim intRESP_1A As Integer


            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(str_SearchXml1_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                If SearchDoc.DocumentElement.SelectSingleNode("Lcode").InnerText.Trim() <> "" Then
                    intLcode = SearchDoc.DocumentElement.SelectSingleNode("Lcode").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("Year").InnerText.Trim() <> "" Then
                    intYear = SearchDoc.DocumentElement.SelectSingleNode("Year").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("Month").InnerText.Trim() <> "" Then
                    intMonth = SearchDoc.DocumentElement.SelectSingleNode("Month").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("SalesManNameId").InnerText.Trim() <> "" Then
                    intSalesManNameId = SearchDoc.DocumentElement.SelectSingleNode("SalesManNameId").InnerText.Trim()
                End If

                'If SearchDoc.DocumentElement.SelectSingleNode("Resp_1a").InnerText.Trim() <> "" Then
                '    intRESP_1A = SearchDoc.DocumentElement.SelectSingleNode("Resp_1a").InnerText.Trim() ''Doubt
                'End If
                '--------Retrieving & Checking Details from Input XMLDocument ------End
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_SER_TA_SALESMAN_TARGET_HIS]"
                    .Connection = objSqlConnection
                    '.Connection = con
                    .Parameters.Add("@Lcode", SqlDbType.Int)
                    If intLcode = 0 Then
                        .Parameters("@Lcode").Value = DBNull.Value
                    Else
                        .Parameters("@Lcode").Value = intLcode
                    End If


                    .Parameters.Add("@YEAR", SqlDbType.Int)
                    If intYear = 0 Then
                        .Parameters("@YEAR").Value = DBNull.Value
                    Else
                        .Parameters("@YEAR").Value = intYear
                    End If


                    .Parameters.Add("@MONTH", SqlDbType.Int)
                    If intMonth = 0 Then
                        .Parameters("@MONTH").Value = DBNull.Value
                    Else
                        .Parameters("@MONTH").Value = intMonth
                    End If


                    .Parameters.Add("@SALESPERSON", SqlDbType.Int)
                    If intSalesManNameId = 0 Then
                        .Parameters("@SALESPERSON").Value = DBNull.Value
                    Else
                        .Parameters("@SALESPERSON").Value = intSalesManNameId
                    End If


                    '.Parameters.Add("@Resp_1a", SqlDbType.BigInt)
                    'If intSalesManNameId = 0 Then
                    '    .Parameters("@Resp_1a").Value = DBNull.Value
                    'Else
                    '    .Parameters("@Resp_1a").Value = intSalesManNameId
                    'End If

                End With

                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("TARGET")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    '  '	<TARGET LCode=''  Year='' Month=''  AgencyName='' OfficeId='' Address='' SalesPersonId='' SalesManName='' Target='' />
                    objAptNodeClone.Attributes("LCode").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Lcode")))
                    objAptNodeClone.Attributes("Year").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("YEAR")) & "")
                    objAptNodeClone.Attributes("Month").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MONTH")) & "")
                    'objAptNodeClone.Attributes("AgencyName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AgencyName")))
                    'objAptNodeClone.Attributes("OfficeId").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OfficeId")) & "")
                    'objAptNodeClone.Attributes("Address").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Address")) & "")
                    objAptNodeClone.Attributes("SalesPersonId").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SalesPersonId")) & "")
                    'objAptNodeClone.Attributes("SalesManName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SalesPerson")) & "")
                    objAptNodeClone.Attributes("Target").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Target")) & "")
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop
                objSqlReader.Close()
                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                End If
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                'msgbox(Exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()

                'If con.State = ConnectionState.Open Then con.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function
        Public Function History(ByVal HisDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument    '<TA_HISTORY_AGENCYTARGET_INPUT>
            '   <TARGET Action='' LCode='' YEAR='' Year='' Month='' SalesPersonId='' Resp_1a='' />
            '</TA_HISTORY_AGENCYTARGET_INPUT>

            'Output   
            '  <TA_UPDATEAGENCYTARGET_OUTPUT>
            '     <TARGET Action='' LCode='' Year='' Month='' EmpName='' Target='' SalesPersonId='' Date='' />
            '         <Errors Status=''><Error Code='' Description='' /></Errors>
            ' </TA_UPDATEAGENCYTARGET_OUTPUT>
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim intCOUNTRYId As Integer
            Dim intYear As Integer
            Dim intMonth As Integer
            Dim intUSERID As Integer
            Dim strAction As Char
            Dim intLcode As Integer
            Dim intSalesPersonId As Integer


           ''paging
                Dim intPageNo, intPageSize As Integer
                Dim strSortBy As String
                Dim blnDesc As Boolean
             ''end


            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strHISTORY_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                ' objXmlInput.LoadXml("<TA_HISTORY_AGENCYTARGET_INPUT><TARGET Action='' LCode='14'  Year='2007' Month='2' SalesPersonId='145' Resp_1a='' /></TA_HISTORY_AGENCYTARGET_INPUT>")

                intLcode = HisDoc.DocumentElement.SelectSingleNode("TARGET").Attributes("LCode").InnerText
                intYear = HisDoc.DocumentElement.SelectSingleNode("TARGET").Attributes("Year").InnerText
                intMonth = HisDoc.DocumentElement.SelectSingleNode("TARGET").Attributes("Month").InnerText
                'intSalesPersonId = HisDoc.DocumentElement.SelectSingleNode("TARGET").Attributes("SalesPersonId").InnerText

          ''paging
               If HisDoc.DocumentElement.SelectSingleNode("TARGET").Attributes("PAGE_NO").InnerText <> "" Then
                    intPageNo = HisDoc.DocumentElement.SelectSingleNode("TARGET").Attributes("PAGE_NO").InnerText
                End If

               If HisDoc.DocumentElement.SelectSingleNode("TARGET").Attributes("PAGE_SIZE").InnerText <> "" Then
                   intPageSize = HisDoc.DocumentElement.SelectSingleNode("TARGET").Attributes("PAGE_SIZE").InnerText
               End If

               If HisDoc.DocumentElement.SelectSingleNode("TARGET").Attributes("SORT_BY").InnerText <> "" Then
                   strSortBy = HisDoc.DocumentElement.SelectSingleNode("TARGET").Attributes("SORT_BY").InnerText
               End If

               'If HisDoc.DocumentElement.SelectSingleNode("TARGET").Attributes("DESC").InnerText <> "" Then

               If HisDoc.DocumentElement.SelectSingleNode("TARGET").Attributes("DESC").InnerText.ToUpper = "TRUE" Then
                      blnDesc = True
                Else
                     blnDesc = False
               End If
          ''end paging




                '--------Retrieving & Checking Details from Input XMLDocument ------End
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_TA_TARGET_HIS]"
                    .Connection = objSqlConnection
                    '.Connection = con

                    '.Parameters.Add("@Action", SqlDbType.Char, 1)
                    'If Len(strAction) > 0 Then
                    '    .Parameters("@Action").Value = Trim(strAction)
                    'Else
                    '    .Parameters("@Action").Value = "S"
                    'End If


                    .Parameters.Add("@LCODE", SqlDbType.BigInt)
                    If Len(intLcode) > 0 Then
                        .Parameters("@LCODE").Value = Trim(intLcode)
                    Else
                        .Parameters("@LCODE").Value = DBNull.Value
                    End If

                    .Parameters.Add("@YEAR", SqlDbType.Int)
                    If Len(intYear) > 0 Then
                        .Parameters("@YEAR").Value = Trim(intYear)
                    Else
                        .Parameters("@YEAR").Value = DBNull.Value
                    End If

                    .Parameters.Add("@MONTH", SqlDbType.Int)
                    If Len(intMonth) > 0 Then
                        .Parameters("@MONTH").Value = Trim(intMonth)
                    Else
                        .Parameters("@MONTH").Value = DBNull.Value
                    End If


                    '.Parameters.Add("@SALESPERSONID", SqlDbType.BigInt)
                    'If Len(intSalesPersonId) > 0 Then
                    '    .Parameters("@SALESPERSONID").Value = Trim(intSalesPersonId)
                    'Else
                    '    .Parameters("@SALESPERSONID").Value = DBNull.Value
                    'End If


                    'If Len(intUSERID) > 0 Then
                    '    .Parameters("@EMPLOYEEID").Value = Trim(intUSERID)
                    'Else
                    '    .Parameters("@EMPLOYEEID").Value = DBNull.Value
                    'End If


                 'paging 
                   .Parameters.Add("@PAGE_NO", SqlDbType.Int)
                    If intPageNo = 0 Then

                        .Parameters("@PAGE_NO").Value = DBNull.Value
                    Else
                        .Parameters("@PAGE_NO").Value = intPageNo
                    End If

                    .Parameters.Add("@PAGE_SIZE", SqlDbType.Int)
                    If intPageSize = 0 Then
                        .Parameters("@PAGE_SIZE").Value = DBNull.Value
                    Else
                        .Parameters("@PAGE_SIZE").Value = intPageSize
                    End If

                    .Parameters.Add("@SORT_BY", SqlDbType.VarChar, 100)
                    If strSortBy = "" Then
                        .Parameters("@SORT_BY").Value = DBNull.Value
                    Else
                        .Parameters("@SORT_BY").Value = strSortBy
                    End If


                    .Parameters.Add("@DESC", SqlDbType.Bit)
                    If blnDesc = True Then
                        .Parameters("@DESC").Value = 1
                    Else
                        .Parameters("@DESC").Value = 0
                    End If

                    .Parameters.Add(New SqlParameter("@TOTALROWS", SqlDbType.BigInt))
                    .Parameters("@TOTALROWS").Direction = ParameterDirection.Output
                    .Parameters("@TOTALROWS").Value = 0

               'end paging



                End With

                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("TARGET")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    'objAptNodeClone.Attributes("LCode").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")) & "")
                    objAptNodeClone.Attributes("Year").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("YEAR")) & "")
                    objAptNodeClone.Attributes("Month").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MONTH")) & "")
                    objAptNodeClone.Attributes("EmpName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmpName")) & "")
                    objAptNodeClone.Attributes("ChangedData").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ChangedData")) & "")
                    objAptNodeClone.Attributes("Date").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Date")) & "")
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop
                     objSqlReader.Close()

                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").InnerText = objSqlCommand.Parameters("@TOTALROWS").Value
                    If intPageSize = 0 Then
                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = 0
                    Else
                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = Ceiling(objSqlCommand.Parameters("@TOTALROWS").Value / intPageSize)
                    End If
                End If
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                'msgbox(Exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try

            Return objOutputXml
        End Function
        Public Function Search_Prev_Target(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            'Purpose:This function gives search results based on choosen search criterion.
            'Input  :- 
            '<TA_AGENCYTARGET_INPUT>
            '	  <City_Id></City_Id>
            '   <CityName></CityName>
            '	  <PYear></PYear>
            '	  <PMonth></PMonth>
            '	  <Year></Year>
            '	  <Month></Month>
            '  	<SalesManNameId><SalesManNameId>
            '   <Increment></Increment>
            '   <Decrement></Decrement>
            '   <TargetValuePer><TargetValuePer>
            '   <PAGE_NO></PAGE_NO>
            '   <PAGE_SIZE></PAGE_SIZE>
            '   <SORT_BY></SORT_BY>
            '   <DESC>FALSE</DESC>
            '<TA_AGENCYTARGET_INPUT>

            'Output:
            '<TA_AGENCYTARGET_OUTPUT>
            '	<TARGET LCode=''  Year='' Month=''  AgencyName='' OfficeId='' Address='' SalesPersonId='' SalesManName='' Target='' />
            '	<PAGE_TOTAL TARGET='' />
            '	<PAGE PAGE_COUNT='' TOTAL_ROWS='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</TA_AGENCYTARGET_OUTPUT>
            '-----------------------------------------------------------------------------
            'Call getConnection()
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim intCityId As Integer
            Dim strCityName As String
            Dim intYear As Integer
            Dim intMonth As Integer
            Dim intPYear As Integer
            Dim intPMonth As Integer

            Dim intSalesManNameId As Integer
            Dim intRESP_1A As Integer
            Dim intTargetValuePer As Integer
            Dim intCalc As Integer
            Dim intIncrement As Integer
            Dim intDecrement As Integer
            Dim TargetValuePer As Integer

            '   <Increment></Increment>
            '   <Decrement></Decrement>
            '   <TargetValuePer><TargetValuePer>

            'paging
            'Dim intPageNo, intPageSize As Integer
            'Dim strSortBy As String
            'Dim blnDesc As Boolean
            'end paging

            Dim intAllTotal As Long

            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(STR_Search_Prev_Target_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                If SearchDoc.DocumentElement.SelectSingleNode("CityName").InnerText.Trim() <> "" Then
                    strCityName = SearchDoc.DocumentElement.SelectSingleNode("CityName").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("Year").InnerText.Trim() <> "" Then
                    intYear = SearchDoc.DocumentElement.SelectSingleNode("Year").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("Month").InnerText.Trim() <> "" Then
                    intMonth = SearchDoc.DocumentElement.SelectSingleNode("Month").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("Year").InnerText.Trim() <> "" Then
                    intPYear = SearchDoc.DocumentElement.SelectSingleNode("PYear").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("Month").InnerText.Trim() <> "" Then
                    intPMonth = SearchDoc.DocumentElement.SelectSingleNode("PMonth").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("SalesManNameId").InnerText.Trim() <> "" Then
                    intSalesManNameId = SearchDoc.DocumentElement.SelectSingleNode("SalesManNameId").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("RESP_1A").InnerText.Trim() <> "" Then
                    intRESP_1A = SearchDoc.DocumentElement.SelectSingleNode("RESP_1A").InnerText.Trim() ''Doubt
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("Increment").InnerText.Trim() <> "" Then
                    intIncrement = SearchDoc.DocumentElement.SelectSingleNode("Increment").InnerText.Trim() ''Doubt
                Else
                    intIncrement = 0
                End If


                If SearchDoc.DocumentElement.SelectSingleNode("Decrement").InnerText.Trim() <> "" Then
                    intDecrement = SearchDoc.DocumentElement.SelectSingleNode("Decrement").InnerText.Trim() ''Doubt
                Else
                    intDecrement = 0
                End If


                If SearchDoc.DocumentElement.SelectSingleNode("TargetValuePer").InnerText.Trim() <> "" Then
                    intTargetValuePer = SearchDoc.DocumentElement.SelectSingleNode("TargetValuePer").InnerText.Trim() ''Doubt
                Else
                    intTargetValuePer = 0
                End If



                ''Paging

                'If SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim() <> "" Then
                '    intPageNo = SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim()
                'End If
                'If SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim() <> "" Then
                '    intPageSize = SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim()
                'End If

                'If SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim() <> "" Then
                '    strSortBy = SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim()
                'End If

                'If SearchDoc.DocumentElement.SelectSingleNode("DESC").InnerText.Trim().ToUpper = "TRUE" Then
                '    blnDesc = True
                'Else
                '    blnDesc = False
                'End If
                ' ''end paging

                '--------Retrieving & Checking Details from Input XMLDocument ------End
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_SER_TA_COMBINE_AGENCYTARGET]"
                    .Connection = objSqlConnection

                    '.Connection = con
                    .Parameters.Add("@CityName", SqlDbType.Char, 100)
                    If Len(strCityName) <= 0 Then
                        .Parameters("@CityName").Value = DBNull.Value
                    Else
                        .Parameters("@CityName").Value = strCityName
                    End If


                    .Parameters.Add("@PYEAR", SqlDbType.Int)
                    If intPYear = 0 Then
                        .Parameters("@PYEAR").Value = DBNull.Value
                    Else
                        .Parameters("@PYEAR").Value = intPYear
                    End If


                    .Parameters.Add("@PMONTH", SqlDbType.Int)
                    If intPMonth = 0 Then
                        .Parameters("@PMONTH").Value = DBNull.Value
                    Else
                        .Parameters("@PMONTH").Value = intPMonth
                    End If


                    .Parameters.Add("@YEAR", SqlDbType.Int)
                    If intPYear = 0 Then
                        .Parameters("@YEAR").Value = DBNull.Value
                    Else
                        .Parameters("@YEAR").Value = intYear
                    End If

                    .Parameters.Add("@MONTH", SqlDbType.Int)
                    If intMonth = 0 Then
                        .Parameters("@MONTH").Value = DBNull.Value
                    Else
                        .Parameters("@MONTH").Value = intMonth
                    End If


                    .Parameters.Add("@SALESPERSON", SqlDbType.Int)
                    If intSalesManNameId = 0 Then
                        .Parameters("@SALESPERSON").Value = DBNull.Value
                    Else
                        .Parameters("@SALESPERSON").Value = intSalesManNameId
                    End If


                    .Parameters.Add("@RESP_1A", SqlDbType.BigInt)
                    If intSalesManNameId = 0 Then
                        .Parameters("@RESP_1A").Value = DBNull.Value
                    Else
                        .Parameters("@RESP_1A").Value = intSalesManNameId
                    End If

                    ' ''paging
                    ''.Parameters.Add("@PAGE_NO", SqlDbType.Int)
                    ''If intPageNo = 0 Then
                    ''    .Parameters("@PAGE_NO").Value = DBNull.Value
                    ''Else
                    ''    .Parameters("@PAGE_NO").Value = intPageNo
                    ''End If

                    ''.Parameters.Add("@PAGE_SIZE", SqlDbType.Int)
                    ''If intPageSize = 0 Then
                    ''    .Parameters("@PAGE_SIZE").Value = DBNull.Value
                    ''Else
                    ''    .Parameters("@PAGE_SIZE").Value = intPageSize
                    ''End If

                    ''.Parameters.Add("@SORT_BY", SqlDbType.VarChar, 100)
                    ''If strSortBy = "" Then
                    ''    .Parameters("@SORT_BY").Value = DBNull.Value
                    ''Else
                    ''    .Parameters("@SORT_BY").Value = strSortBy
                    ''End If

                    ''.Parameters.Add("@DESC", SqlDbType.Bit)
                    ''If blnDesc = True Then
                    ''    .Parameters("@DESC").Value = 1
                    ''Else
                    ''    .Parameters("@DESC").Value = 0
                    ''End If

                    ''.Parameters.Add(New SqlParameter("@TOTALROWS", SqlDbType.BigInt))
                    ''.Parameters("@TOTALROWS").Direction = ParameterDirection.Output
                    ''.Parameters("@TOTALROWS").Value = 0

                    ' ''end paging

                End With

                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()
                'con.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("TARGET")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)


                Do While objSqlReader.Read()
                    blnRecordFound = True
                    '  '	<TARGET LCode=''  Year='' Month=''  AgencyName='' OfficeId='' Address='' SalesPersonId='' SalesManName='' Target='' />
                    objAptNodeClone.Attributes("LCode").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Lcode")))
                    objAptNodeClone.Attributes("Year").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("YEAR")) & "")
                    objAptNodeClone.Attributes("Month").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MONTH")) & "")
                    objAptNodeClone.Attributes("AgencyName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AgencyName")))
                    objAptNodeClone.Attributes("OfficeId").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OfficeId")) & "")
                    objAptNodeClone.Attributes("Address").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Address")) & "")
                    objAptNodeClone.Attributes("SalesPersonId").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SalesPersonId")) & "")
                    objAptNodeClone.Attributes("LoginId").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LoginId")) & "")
                    objAptNodeClone.Attributes("SalesManName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SalesPerson")) & "")

                    '' case when no TargetValuePer value
                    If intTargetValuePer <= 0 Then

                        objAptNodeClone.Attributes("Target").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Target")) & "")
                        If objAptNodeClone.Attributes("Target").InnerText = "" Then
                            objAptNodeClone.Attributes("Target").InnerText = "0"
                            intAllTotal = intAllTotal + objAptNodeClone.Attributes("Target").InnerText
                        Else

                            objAptNodeClone.Attributes("Target").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Target")) & "")
                            intAllTotal = intAllTotal + objAptNodeClone.Attributes("Target").InnerText

                        End If

                    End If

                    ''case when taregetvalueper increase up
                    If intTargetValuePer > 0 And intIncrement > 0 Then
                        Dim current_target_value As Integer
                        objAptNodeClone.Attributes("Target").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Target")) & "")
                        If objAptNodeClone.Attributes("Target").InnerText = "" Then
                            current_target_value = 0
                        Else
                            current_target_value = objAptNodeClone.Attributes("Target").InnerText
                        End If
                        'MsgBox(current_target_value)
                        intCalc = Round(current_target_value + (current_target_value * Val(intTargetValuePer)) / 100)
                        'MsgBox(intCalc)
                        intAllTotal = intAllTotal + intCalc
                        objAptNodeClone.Attributes("Target").InnerText = intCalc.ToString

                    End If

                    ''case when taregetvalueper decrease 
                    If intTargetValuePer > 0 And intDecrement > 0 Then
                        Dim current_target_value As Integer
                        objAptNodeClone.Attributes("Target").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Target")) & "")

                        If objAptNodeClone.Attributes("Target").InnerText = "" Then
                            current_target_value = 0
                        Else
                            current_target_value = objAptNodeClone.Attributes("Target").InnerText
                        End If

                        current_target_value = objAptNodeClone.Attributes("Target").InnerText
                        intCalc = Round(current_target_value - (current_target_value * Val(intTargetValuePer)) / 100)
                        intAllTotal = intAllTotal + intCalc

                    End If
                    objOutputXml.DocumentElement.SelectSingleNode("Total").InnerText = intAllTotal
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop


                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    ''objSqlReader.NextResult()
                    ''While objSqlReader.Read
                    ''    objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("TARGET").InnerText = objSqlReader.GetValue(objSqlReader.GetOrdinal("Target"))
                    ''End While
                    ''objSqlReader.Close()

                    ''objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").InnerText = objSqlCommand.Parameters("@TOTALROWS").Value
                    ''If intPageSize = 0 Then
                    ''    objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = 0
                    ''Else
                    ''    objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = Ceiling(objSqlCommand.Parameters("@TOTALROWS").Value / intPageSize)
                    ''End If
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                End If
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                'msgbox(Exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                'If con.State = ConnectionState.Open Then con.Close()

                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function
        'Public Function Search_Agency_Target(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument

        '    'Purpose:This function gives search results based on choosen search criterion.
        '    'Input  :- 
        '    '<TA_AGENCYTARGET_INPUT>
        '    '	<City_Id></City_Id>
        '    '   <CityName></CityName>
        '    '	<Year></Year>
        '    '	<Month></Month>
        '    '	<SalesManNameId><SalesManNameId>
        '    '   <Increment></Increment>
        '    '   <Decrement></Decrement>
        '    '   <TargetValuePer><TargetValuePer>
        '    '   <PAGE_NO></PAGE_NO>
        '    '   <PAGE_SIZE></PAGE_SIZE>
        '    '   <SORT_BY></SORT_BY>
        '    '   <DESC>FALSE</DESC>
        '    '<TA_AGENCYTARGET_INPUT>

        '    'Output:
        '    '<TA_AGENCYTARGET_OUTPUT>
        '    '	<TARGET LCode=''  Year='' Month=''  AgencyName='' OfficeId='' Address='' SalesPersonId='' SalesManName='' Target='' />
        '    '	<PAGE_TOTAL TARGET='' />
        '    '	<PAGE PAGE_COUNT='' TOTAL_ROWS='' />
        '    '	<Errors Status=''>
        '    '		<Error Code='' Description='' />
        '    '	</Errors>
        '    '</TA_AGENCYTARGET_OUTPUT>
        '    '-----------------------------------------------------------------------------
        '    'Call getConnection()
        '    Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
        '    Dim objSqlCommand As New SqlCommand
        '    Dim objOutputXml As New XmlDocument
        '    Dim objAptNode, objAptNodeClone As XmlNode
        '    Dim objTempNode As XmlNode
        '    Dim objSqlReader As SqlDataReader
        '    Dim blnRecordFound As Boolean
        '    Dim intLcode As Integer
        '    Dim intCityId As Integer
        '    Dim strCityName As String
        '    Dim intYear As Integer
        '    Dim intMonth As Integer
        '    Dim intSalesManNameId As Integer
        '    Dim intRESP_1A As Integer
        '    Dim intTargetValuePer As Integer
        '    Dim intCalc As Integer
        '    Dim intIncrement As Integer
        '    Dim intDecrement As Integer
        '    Dim objNode As XmlNode
        '    Dim intTarget As Integer
        '    Dim intTotal As Long
        '    '   <Increment></Increment>
        '    '   <Decrement></Decrement>
        '    '   <TargetValuePer><TargetValuePer>

        '    ''paging
        '    'Dim intPageNo, intPageSize As Integer
        '    'Dim strSortBy As String
        '    'Dim blnDesc As Boolean
        '    'end paging

        '    '    <LCode><Lcode>
        '    '	 <CityName></CityName>
        '    '	 <Year></Year>
        '    '	 <Month></Month>
        '    '	 <SalesManNameId></SalesManNameId>
        '    '	 <Target></Target>
        '    '	 <Increment></Increment>
        '    '	 <Decrement></Decrement>
        '    '	 <TargetValuePer></TargetValuePer>
        '    Const strMETHOD_NAME As String = "Search"
        '    objOutputXml.LoadXml(str_Search_TargetValue_Output)
        '    objTempNode = objOutputXml.DocumentElement.SelectSingleNode("TARGET")
        '    Try

        '        intIncrement = Val(SearchDoc.DocumentElement.SelectSingleNode("CHANGE").Attributes("Increment").Value & "")
        '        intDecrement = Val(SearchDoc.DocumentElement.SelectSingleNode("CHANGE").Attributes("Decrement").Value & "")
        '        intTargetValuePer = Val(SearchDoc.DocumentElement.SelectSingleNode("CHANGE").Attributes("TargetValuePer").Value & "")

        '        For Each objNode In SearchDoc.DocumentElement.SelectNodes("TARGET")
        '            '--------Retrieving & Checking Details from Input XMLDocument ------Start

        '            If objNode.Attributes("LCode").Value <> "" Then
        '                intLcode = objNode.Attributes("LCode").Value
        '            Else
        '                intLcode = 0
        '            End If


        '            strCityName = SearchDoc.DocumentElement.SelectSingleNode("CHANGE").Attributes("CityName").InnerText


        '            If objNode.Attributes("Year").Value <> "" Then
        '                intYear = objNode.Attributes("Year").Value
        '            Else
        '                intYear = 0
        '            End If

        '            If objNode.Attributes("Month").Value <> "" Then
        '                intMonth = objNode.Attributes("Month").Value
        '            Else
        '                intMonth = 0
        '            End If

        '            If objNode.Attributes("SalesPersonId").Value <> "" Then
        '                intSalesManNameId = Val(objNode.Attributes("SalesPersonId").Value)
        '            Else
        '                intSalesManNameId = 0
        '            End If




        '            'If objNode.Attributes("Target").Value <> "" Then
        '            '    intTarget = objNode.Attributes("Target").Value
        '            'Else
        '            '    intTarget = 0
        '            'End If

        '            '--------Retrieving & Checking Details from Input XMLDocument ------End
        '            With objSqlCommand
        '                .CommandType = CommandType.StoredProcedure
        '                .CommandText = "[UP_GET_TA_TARGETVALUE]"
        '                .Connection = objSqlConnection
        '                '.Connection = con

        '                .Parameters.Add("@CityName", SqlDbType.Char, 100)
        '                If Len(strCityName) <= 0 Then
        '                    .Parameters("@CityName").Value = DBNull.Value
        '                Else
        '                    .Parameters("@CityName").Value = LTrim(RTrim(strCityName))
        '                End If

        '                .Parameters.Add("@YEAR", SqlDbType.Int)
        '                If intYear = 0 Then
        '                    .Parameters("@YEAR").Value = DBNull.Value
        '                Else
        '                    .Parameters("@YEAR").Value = intYear
        '                End If


        '                .Parameters.Add("@MONTH", SqlDbType.Int)
        '                If intMonth = 0 Then
        '                    .Parameters("@MONTH").Value = DBNull.Value
        '                Else
        '                    .Parameters("@MONTH").Value = intMonth
        '                End If


        '                .Parameters.Add("@RESP_1A", SqlDbType.BigInt)
        '                If intSalesManNameId = 0 Then
        '                    .Parameters("@RESP_1A").Value = DBNull.Value
        '                Else
        '                    .Parameters("@RESP_1A").Value = intSalesManNameId
        '                End If


        '                .Parameters.Add("@Lcode", SqlDbType.BigInt)
        '                If intLcode <= 0 Then
        '                    .Parameters("@Lcode").Value = DBNull.Value
        '                Else
        '                    .Parameters("@Lcode").Value = intLcode
        '                End If

        '            End With

        '            'RETRIEVING THE RECORDS ACCORDING TO THE SEARCH CRITERIA
        '            objSqlConnection.Open()
        '            objSqlReader = objSqlCommand.ExecuteReader()


        '            'Reading and Appending records into the Output XMLDocument
        '            'objAptNode = objOutputXml.DocumentElement.SelectSingleNode("TARGET")
        '            objAptNodeClone = objTempNode.CloneNode(True)
        '            'objOutputXml.DocumentElement.RemoveChild(objAptNode)
        '            Do While objSqlReader.Read()
        '                blnRecordFound = True
        '                '<TARGET LCode=''  Year='' Month=''  AgencyName='' OfficeId='' Address='' SalesPersonId='' SalesManName='' Target='' />
        '                objAptNodeClone.Attributes("LCode").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Lcode")))
        '                objAptNodeClone.Attributes("Year").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("YEAR")) & "")
        '                objAptNodeClone.Attributes("Month").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MONTH")) & "")
        '                objAptNodeClone.Attributes("AgencyName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AgencyName")) & "")
        '                objAptNodeClone.Attributes("OfficeId").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OfficeId")) & "")
        '                objAptNodeClone.Attributes("Address").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Address")) & "")
        '                objAptNodeClone.Attributes("SalesPersonId").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SalesPersonId")) & "")
        '                objAptNodeClone.Attributes("SalesManName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SalesPerson")) & "")
        '                'objAptNodeClone.Attributes("Target").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Target")) & "")


        '                '' case when no TargetValuePer value
        '                If intTargetValuePer <= 0 Then
        '                    Dim current_target_value As Integer
        '                    objAptNodeClone.Attributes("Target").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Target")) & "")
        '                    current_target_value = objAptNodeClone.Attributes("Target").InnerText
        '                    objAptNodeClone.Attributes("Target").InnerText = current_target_value.ToString
        '                    intTotal = intTotal + current_target_value

        '                    'If objAptNodeClone.Attributes("Target").InnerText = "" Then
        '                    '    objAptNodeClone.Attributes("Target").InnerText = "0"
        '                    'Else
        '                    '    objAptNodeClone.Attributes("Target").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Target")) & "")
        '                    'End If



        '                End If

        '                ''case when taregetvalueper increase up
        '                If intTargetValuePer > 0 And intIncrement > 0 Then
        '                    Dim current_target_value As Integer
        '                    If Val(objNode.Attributes("Target").Value & "") = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Target")) & "") Then
        '                        current_target_value = Val(objNode.Attributes("Target").Value & "")
        '                        intCalc = Round(current_target_value + (current_target_value * Val(intTargetValuePer)) / 100)
        '                        objAptNodeClone.Attributes("Target").InnerText = intCalc.ToString
        '                        intTotal = intTotal + intCalc
        '                    Else
        '                                 If Val(objNode.Attributes("Target").Value & "") > 0 Then
        '                                     current_target_value = Val(objNode.Attributes("Target").Value & "")
        '                                     intCalc = Round(current_target_value + (current_target_value * Val(intTargetValuePer)) / 100)
        '                                     objAptNodeClone.Attributes("Target").InnerText = intCalc.ToString
        '                                     intTotal = intTotal + intCalc

        '                                     'objSqlCommand.Dispose()
        '                                 Else
        '                                     current_target_value = Val(objNode.Attributes("Target").Value & "")
        '                                     intCalc = Round(current_target_value + (current_target_value * Val(intTargetValuePer)) / 100)
        '                                     objAptNodeClone.Attributes("Target").InnerText = intCalc.ToString
        '                                     intTotal = intTotal + intCalc

        '                                     current_target_value = Val(objNode.Attributes("Target").Value)  ''chaged alter
        '                                     objAptNodeClone.Attributes("Target").InnerText = intCalc.ToString ''chaged alter


        '                                 End If
        '                    End If

        '                End If

        '                ''case when taregetvalueper decrease 
        '                If intTargetValuePer > 0 And intDecrement > 0 Then
        '                    Dim current_target_value As Integer
        '                    If Val(objNode.Attributes("Target").Value & "") = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Target")) & "") Then
        '                        current_target_value = objNode.Attributes("Target").Value
        '                        intCalc = Round(current_target_value - (current_target_value * Val(intTargetValuePer)) / 100)
        '                        objAptNodeClone.Attributes("Target").InnerText = intCalc.ToString
        '                        intTotal = intTotal + intCalc
        '                    Else
        '                        If Val(objNode.Attributes("Target").Value & "") > 0 Then
        '                            current_target_value = Val(objNode.Attributes("Target").Value)
        '                            intCalc = Round(current_target_value - (current_target_value * Val(intTargetValuePer)) / 100)
        '                            objAptNodeClone.Attributes("Target").InnerText = intCalc.ToString
        '                            intTotal = intTotal + intCalc
        '                            Else
        '                            current_target_value = Val(objNode.Attributes("Target").Value)
        '                            objAptNodeClone.Attributes("Target").InnerText = intCalc.ToString
        '                        End If
        '                    End If

        '                End If

        '                objOutputXml.DocumentElement.AppendChild(objAptNodeClone)

        '            Loop

        '            If blnRecordFound = False Then
        '                bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
        '                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
        '                objSqlReader.Close()
        '            Else
        '                objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
        '            End If
        '            objSqlReader.Close()
        '            objSqlCommand.Parameters.Clear()
        '            objSqlCommand.Connection.Close()
        '            objSqlReader = Nothing
        '            objSqlConnection.Close()

        '        Next
        '        objOutputXml.DocumentElement.RemoveChild(objTempNode)
        '        objOutputXml.DocumentElement.SelectSingleNode("Total").InnerText = intTotal.ToString
        '    Catch Exec As AAMSException
        '        'CATCHING AAMS EXCEPTIONS
        '        bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
        '        Return objOutputXml
        '    Catch Exec As Exception
        '        'CATCHING OTHER EXCEPTIONS
        '        'msgbox(Exec.ToString)
        '        bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
        '        bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
        '        Return objOutputXml
        '    Finally
        '        If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
        '        'If con.State = ConnectionState.Open Then con.Close()

        '        objSqlCommand.Dispose()
        '    End Try
        '    Return objOutputXml

        'End Function

Public Function Search_Agency_Target(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            'Purpose:This function gives search results based on choosen search criterion.
            'Input  :- 
            '<TA_AGENCYTARGET_INPUT>
            '	<City_Id></City_Id>
            '   <CityName></CityName>
            '	<Year></Year>
            '	<Month></Month>
            '	<SalesManNameId><SalesManNameId>
            '   <Increment></Increment>
            '   <Decrement></Decrement>
            '   <TargetValuePer><TargetValuePer>
            '   <PAGE_NO></PAGE_NO>
            '   <PAGE_SIZE></PAGE_SIZE>
            '   <SORT_BY></SORT_BY>
            '   <DESC>FALSE</DESC>
            '<TA_AGENCYTARGET_INPUT>

            'Output:
            '<TA_AGENCYTARGET_OUTPUT>
            '	<TARGET LCode=''  Year='' Month=''  AgencyName='' OfficeId='' Address='' SalesPersonId='' SalesManName='' Target='' />
            '	<PAGE_TOTAL TARGET='' />
            '	<PAGE PAGE_COUNT='' TOTAL_ROWS='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</TA_AGENCYTARGET_OUTPUT>
            '-----------------------------------------------------------------------------
            'Call getConnection()
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml, objOutputXml1 As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objTempNode As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim intLcode As Integer
            Dim intCityId As Integer
            Dim strCityName As String
            Dim intYear As Integer
            Dim intMonth As Integer
            Dim intSalesManNameId As Integer
            Dim intRESP_1A As Integer
            Dim intTargetValuePer As Integer
            Dim intCalc As Integer
            Dim intIncrement As Integer
            Dim intDecrement As Integer
            Dim objNode As XmlNode
            Dim intTarget As Integer
            Dim intTotal As Integer

            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(str_Search_TargetValue_Output)
            objTempNode = objOutputXml.DocumentElement.SelectSingleNode("TARGET")
            Try

                intIncrement = Val(SearchDoc.DocumentElement.SelectSingleNode("CHANGE").Attributes("Increment").Value & "")
                intDecrement = Val(SearchDoc.DocumentElement.SelectSingleNode("CHANGE").Attributes("Decrement").Value & "")
                intTargetValuePer = Val(SearchDoc.DocumentElement.SelectSingleNode("CHANGE").Attributes("TargetValuePer").Value & "")


                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("TARGET")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)




                For Each objNode In SearchDoc.DocumentElement.SelectNodes("TARGET")
                    '--------Retrieving & Checking Details from Input XMLDocument ------Start

                    '<TARGET LCode=''  Year='' Month=''  AgencyName='' OfficeId='' Address='' SalesPersonId='' SalesManName='' Target='' />
                    blnRecordFound = True

                    objAptNodeClone.Attributes("LCode").InnerText = Trim(objNode.Attributes("LCode").Value)
                    objAptNodeClone.Attributes("Year").InnerText = Trim(objNode.Attributes("Year").Value & "")
                    objAptNodeClone.Attributes("Month").InnerText = Trim(objNode.Attributes("Month").Value & "")
                    objAptNodeClone.Attributes("AgencyName").InnerText = Trim(objNode.Attributes("AgencyName").Value & "")
                    objAptNodeClone.Attributes("OfficeId").InnerText = Trim(objNode.Attributes("OfficeId").Value & "")
                    objAptNodeClone.Attributes("Address").InnerText = Trim(objNode.Attributes("Address").Value & "")
                    objAptNodeClone.Attributes("SalesPersonId").InnerText = Trim(objNode.Attributes("SalesPersonId").Value & "")
                    objAptNodeClone.Attributes("SalesManName").InnerText = Trim(objNode.Attributes("SalesManName").Value & "")
                    'objAptNodeClone.Attributes("Target").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Target")) & "")


                    '' $$$$$$$$$$$$$$$$$$$$$$$$$$$case when no TargetValuePer value$$$$$$$$$$$$$$$$$$$$
                    If intTargetValuePer <= 0 Then
                        Dim current_target_value As Integer
                        objAptNodeClone.Attributes("Target").InnerText = Trim(objNode.Attributes("Target").Value & "")
                        current_target_value = objNode.Attributes("Target").InnerText
                        objAptNodeClone.Attributes("Target").InnerText = current_target_value.ToString
                        intTotal = intTotal + current_target_value
                    End If



                    ''$$$$$$$$$$$$$$$$$$$$$$$$$$$$case when taregetvalueper increase up$$$$$$$$$$$$$$$$$$$$
                    If intTargetValuePer > 0 And intIncrement > 0 Then
                        Dim current_target_value As Integer
                        current_target_value = Val(objNode.Attributes("Target").Value & "")
                        If current_target_value <= 0 Then  'case when target vlaue is negative
                            intCalc = Round(current_target_value - (current_target_value * Val(intTargetValuePer)) / 100)
                            objAptNodeClone.Attributes("Target").InnerText = intCalc.ToString
                        Else
                            intCalc = Round(current_target_value + (current_target_value * Val(intTargetValuePer)) / 100)
                            objAptNodeClone.Attributes("Target").InnerText = intCalc.ToString
                        End If
                        intTotal = intTotal + intCalc
                    End If



                    ''$$$$$$$$$$$$$$$$$$$$$$$$$$$$case when taregetvalueper decrease down$$$$$$$$$$$$$$$$$$$$
                    If intTargetValuePer > 0 And intDecrement > 0 Then
                        Dim current_target_value As Integer
                        current_target_value = Val(objNode.Attributes("Target").Value & "")
                            intCalc = Round(current_target_value - (current_target_value * Val(intTargetValuePer)) / 100)
                            objAptNodeClone.Attributes("Target").InnerText = intCalc.ToString
                        intTotal = intTotal + intCalc

                    End If





                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)


                    If blnRecordFound = False Then
                        bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                    Else
                        objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    End If
                Next

                objOutputXml.DocumentElement.SelectSingleNode("Total").InnerText = intTotal.ToString
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                'MsgBox(Exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml

        End Function

        Public Function Search_Agency_target1(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument


            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim intCityId As Integer
            Dim strCityName As String
            Dim intYear As Integer
            Dim intMonth As Integer
            Dim intPYear As Integer
            Dim intPMonth As Integer

            Dim intSalesManNameId As Integer
            Dim intRESP_1A As Integer
            Dim intTargetValuePer As Integer
            Dim intCalc As Integer
            Dim intIncrement As Integer
            Dim intDecrement As Integer
            Dim TargetValuePer As Integer

            Dim intAllTotal As Long
            Dim intLCode As Integer
            Dim objNode As XmlNode
            Dim intTotal As Double

            Dim strLocation_Code As String
            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(STR_Search_Prev_Target_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("TARGET")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)

                '--------Retrieving & Checking Details from Input XMLDocument ------End

                'For Each objNode In SearchDoc.DocumentElement.SelectNodes("TARGET")

                'intLCode = objNode.Attributes("LCode").Value
                intYear = SearchDoc.DocumentElement.SelectSingleNode("TARGET").Attributes("Year").Value
                intMonth = SearchDoc.DocumentElement.SelectSingleNode("TARGET").Attributes("Month").Value


                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_GET_TA_TARGETVALUE]"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@YEAR", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@MONTH", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@INPUTXML", SqlDbType.Xml))


                    .Parameters("@LCODE").Value = DBNull.Value
                    .Parameters("@YEAR").Value = intYear
                    .Parameters("@MONTH").Value = intMonth
                    .Parameters("@INPUTXML").Value = SearchDoc.OuterXml



                    '.Parameters.Add("@YEAR", SqlDbType.Int)
                    'If intYear = 0 Then
                    '    .Parameters("@YEAR").Value = DBNull.Value
                    'Else
                    '    .Parameters("@YEAR").Value = 2009
                    'End If

                    '.Parameters.Add("@MONTH", SqlDbType.Int)
                    'If intMonth = 0 Then
                    '    .Parameters("@MONTH").Value = DBNull.Value
                    'Else
                    '    .Parameters("@MONTH").Value = 9
                    'End If

                    '.Parameters.Add("@INPUTXML", SqlDbType.Xml)
                    'If strLocation_Code = "" Then
                    '    .Parameters("@INPUTXML").Value = DBNull.Value
                    'Else
                    '    .Parameters("@INPUTXML").Value = SearchDoc.OuterXml
                    'End If



                End With



                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()

                    blnRecordFound = True
                    objAptNodeClone.Attributes("LCode").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")) & "")
                    objAptNodeClone.Attributes("Year").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("YEAR")) & "")
                    objAptNodeClone.Attributes("Month").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MONTH")) & "")
                    objAptNodeClone.Attributes("AgencyName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AgencyName")))
                    objAptNodeClone.Attributes("OfficeId").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OfficeId")) & "")
                    objAptNodeClone.Attributes("Address").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Address")) & "")
                    objAptNodeClone.Attributes("SalesPersonId").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SalesPersonID")) & "")
                    objAptNodeClone.Attributes("SalesManName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SalesPerson")) & "")
                    'objAptNodeClone.Attributes("LoginId").InnerText = objNode.Attributes("LoginId").Value
                    objAptNodeClone.Attributes("Target").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Target")) & "")
                    intCalc = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Target")) & "")
                    intTotal = intTotal + intCalc
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop

                objSqlReader.Close()

                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    objOutputXml.DocumentElement.SelectSingleNode("Total").InnerText = intTotal.ToString
                End If



                'If blnRecordFound = False Then
                '    'bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")

                '    objAptNodeClone.Attributes("LCode").InnerText = objNode.Attributes("LCode").Value
                '    objAptNodeClone.Attributes("Year").InnerText = objNode.Attributes("Year").Value
                '    objAptNodeClone.Attributes("Month").InnerText = objNode.Attributes("Month").Value
                '    objAptNodeClone.Attributes("AgencyName").InnerText = objNode.Attributes("AgencyName").Value
                '    objAptNodeClone.Attributes("OfficeId").InnerText = objNode.Attributes("OfficeId").Value
                '    objAptNodeClone.Attributes("Address").InnerText = objNode.Attributes("Address").Value
                '    objAptNodeClone.Attributes("SalesPersonId").InnerText = objNode.Attributes("SalesPersonId").Value
                '    objAptNodeClone.Attributes("SalesManName").InnerText = objNode.Attributes("SalesManName").Value
                '    objAptNodeClone.Attributes("LoginId").InnerText = objNode.Attributes("LoginId").Value
                '    objAptNodeClone.Attributes("Target").InnerText = 0
                '    intCalc = 0 ' objNode.Attributes("Target").Value
                '    intTotal = intTotal + intCalc
                '    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                '    objAptNodeClone = objAptNode.CloneNode(True)

                '    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                '    'con.Dispose()
                '    objSqlCommand.Parameters.Clear()
                '    objSqlCommand.Dispose()


                'Else
                '    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                '    'con.Dispose()
                '    objSqlCommand.Parameters.Clear()
                '    objSqlCommand.Dispose()
                '    blnRecordFound = False
                'End If


                ' Next

             








            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                'msgbox(Exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Parameters.Clear()
                'objSqlCommand.Dispose()
            End Try
            Return objOutputXml


        End Function


    End Class
End Namespace

