'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Tapan $
'Purpose   : This Interface must be implemented by all the Agency Classes.
'$Logfile: /AAMS/Components/bizTravelAgency/bzAgency1ARespChange.vb $
'$Workfile: bzAgency1ARespChange.vb $
'$Revision: 20 $
'$Archive: /AAMS/Components/bizTravelAgency/bzAgency1ARespChange.vb $
'$Modtime: 6/04/11 10:53a $
Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizTravelAgency
    Public Class bzAgency1ARespChange
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzAgnecy1ARespChange"
        Const strSEARCH_OUTPUT = "<TA_SEARCHAGENCY_1A_OUTPUT><RESP ACTION='' LOCATION_CODE='' NAME='' CHAIN_CODE='' GROUP_NAME='' OFFICEID='' ADDRESS='' 	ADDRESS1='' CITY_NAME='' COUNTRY_NAME='' ONLINE_STATUS='' AOFFICE='' CRS='' AGENCY_TYPE='' PRIORITY='' RESP_ASSGN_FROM='' RESP_ASSGN_TO='' GET_CHECKED='' /> <PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></TA_SEARCHAGENCY_1A_OUTPUT>"
        Const strupdate_OUTPUT = "<TA_UPDATEAGENCY_1A_OUTPUT><RESP ACTION='' LOCATION_CODE='' NAME='' CHAIN_CODE='' GROUP_NAME='' OFFICEID='' CITY_NAME='' COUNTRY_NAME='' ONLINE_STATUS=''   AOFFICE='' CRS='' AGENCY_TYPE='' PRIORITY='' RESP_ASSGN_FROM='' RESP_ASSGN_TO='' GET_CHECKED='' /><Errors Status=''><Error Code='' Description='' /></Errors></TA_UPDATEAGENCY_1A_OUTPUT>"
        Const strDeallocate_OUTPUT = "<TA_UPDATEAGENCY_1A_OUTPUT><RESP ACTION='' LOCATION_CODE='' NAME='' CHAIN_CODE='' GROUP_NAME='' OFFICEID='' CITY_NAME='' COUNTRY_NAME='' ONLINE_STATUS=''   AOFFICE='' CRS='' AGENCY_TYPE='' PRIORITY='' RESP_ASSGN_FROM='' RESP_ASSGN_TO='' GET_CHECKED='' /><Errors Status=''><Error Code='' Description='' /></Errors></TA_UPDATEAGENCY_1A_OUTPUT>"
        Const strHistory_OUTPUT = "<TA_HISTORY_RESP1A_OUTPUT><RESP  EMPNAME='' DATE=''  RESP_NAME='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description=''/></Errors></TA_HISTORY_RESP1A_OUTPUT>"
        Public Function Search(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Search
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Dim strAction As String = ""
            Dim intLocation_Code As Integer
            Dim strName As String = vbNullString
            Dim intChain_Code As Integer
            Dim strGroupName As String = vbNullString
            Dim strOfficeID As String = vbNullString
            Dim strCity_Name As String = vbNullString
            Dim strCountry_Name As String = vbNullString
            Dim strOnLineStatus As String = vbNullString
            Dim strAoffice As String = vbNullString
            Dim strCRS As String = vbNullString
            Dim strAgencyType As String = vbNullString
            Dim strPRIORITY As String = vbNullString
            Dim strResp_Assgn_From As String = vbNullString
            Dim strResp_Assgn_To As String = vbNullString

            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)
            Try

                If SearchDoc.DocumentElement.SelectSingleNode("ACTION").InnerText.Trim() <> "" Then
                    strAction = SearchDoc.DocumentElement.SelectSingleNode("ACTION").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText.Trim() <> "" Then
                    intLocation_Code = SearchDoc.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("NAME").InnerText.Trim() <> "" Then
                    strName = SearchDoc.DocumentElement.SelectSingleNode("NAME").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText.Trim() <> "" Then
                    intChain_Code = SearchDoc.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("GROUP_NAME").InnerText.Trim() <> "" Then
                    strGroupName = SearchDoc.DocumentElement.SelectSingleNode("GROUP_NAME").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("OFFICEID").InnerText.Trim() <> "" Then
                    strOfficeID = SearchDoc.DocumentElement.SelectSingleNode("OFFICEID").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("CITY_NAME").InnerText.Trim() <> "" Then
                    strCity_Name = SearchDoc.DocumentElement.SelectSingleNode("CITY_NAME").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("COUNTRY_NAME").InnerText.Trim() <> "" Then
                    strCountry_Name = SearchDoc.DocumentElement.SelectSingleNode("COUNTRY_NAME").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("ONLINE_STATUS").InnerText.Trim() <> "" Then
                    strOnLineStatus = SearchDoc.DocumentElement.SelectSingleNode("ONLINE_STATUS").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("AOFFICE").InnerText.Trim() <> "" Then
                    strAoffice = SearchDoc.DocumentElement.SelectSingleNode("AOFFICE").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("CRS").InnerText.Trim() <> "" Then
                    strCRS = SearchDoc.DocumentElement.SelectSingleNode("CRS").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("AGENCY_TYPE").InnerText.Trim() <> "" Then
                    strAgencyType = SearchDoc.DocumentElement.SelectSingleNode("AGENCY_TYPE").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("PRIORITY").InnerText.Trim() <> "" Then
                    strPRIORITY = SearchDoc.DocumentElement.SelectSingleNode("PRIORITY").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("RESP_ASSGN_FROM").InnerText.Trim() <> "" Then
                    strResp_Assgn_From = SearchDoc.DocumentElement.SelectSingleNode("RESP_ASSGN_FROM").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("RESP_ASSGN_TO").InnerText.Trim() <> "" Then
                    strResp_Assgn_To = SearchDoc.DocumentElement.SelectSingleNode("RESP_ASSGN_TO").InnerText.Trim()
                End If

                'Paging Section    
                If SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim() <> "" Then
                    intPageNo = SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim()
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim() <> "" Then
                    intPageSize = SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim() <> "" Then
                    strSortBy = SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("DESC").InnerText.Trim().ToUpper = "TRUE" Then
                    blnDesc = True
                Else
                    blnDesc = False
                End If

                Dim strCOMP_VERTICAL As String = ""
                If SearchDoc.DocumentElement.SelectSingleNode("COMP_VERTICAL") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText.Trim() <> "" Then
                        strCOMP_VERTICAL = SearchDoc.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText.Trim()
                    End If
                End If

                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_RESP_CHANGE"
                    .Connection = objSqlConnection

                    .Parameters.Add("@ACTION", SqlDbType.Char, 1)
                    If strAction = "" Then
                        .Parameters("@ACTION").Value = DBNull.Value
                    Else
                        .Parameters("@ACTION").Value = strAction
                    End If

                    .Parameters.Add("@LOCATION_CODE", SqlDbType.BigInt)
                    If intLocation_Code = 0 Then
                        .Parameters("@LOCATION_CODE").Value = DBNull.Value
                    Else
                        .Parameters("@LOCATION_CODE").Value = intLocation_Code
                    End If

                    .Parameters.Add("@NAME", SqlDbType.VarChar, 100)
                    If strName = "" Then
                        .Parameters("@NAME").Value = DBNull.Value
                    Else
                        .Parameters("@NAME").Value = strName
                    End If

                    .Parameters.Add("@CHAIN_CODE", SqlDbType.Int)
                    If intChain_Code = 0 Then
                        .Parameters("@CHAIN_CODE").Value = DBNull.Value
                    Else
                        .Parameters("@CHAIN_CODE").Value = intChain_Code
                    End If

                    .Parameters.Add("@GROUP_NAME", SqlDbType.VarChar, 100)
                    If strGroupName = "" Then
                        .Parameters("@GROUP_NAME").Value = DBNull.Value
                    Else
                        .Parameters("@GROUP_NAME").Value = strGroupName
                    End If

                    .Parameters.Add("@OFFICEID", SqlDbType.VarChar, 10)
                    If strOfficeID = "" Then
                        .Parameters("@OFFICEID").Value = DBNull.Value
                    Else
                        .Parameters("@OFFICEID").Value = strOfficeID
                    End If

                    .Parameters.Add("@CITY_NAME", SqlDbType.VarChar, 100)
                    If strCity_Name = "" Then
                        .Parameters("@CITY_NAME").Value = DBNull.Value
                    Else
                        .Parameters("@CITY_NAME").Value = strCity_Name
                    End If

                    .Parameters.Add("@COUNTRY_NAME", SqlDbType.VarChar, 100)
                    If strCountry_Name = "" Then
                        .Parameters("@COUNTRY_NAME").Value = DBNull.Value
                    Else
                        .Parameters("@COUNTRY_NAME").Value = strCountry_Name
                    End If

                    .Parameters.Add("@ONLINE_STATUS", SqlDbType.VarChar, 25)
                    If strOnLineStatus = "" Then
                        .Parameters("@ONLINE_STATUS").Value = DBNull.Value
                    Else
                        .Parameters("@ONLINE_STATUS").Value = strOnLineStatus
                    End If

                    .Parameters.Add("@AOFFICE", SqlDbType.Char, 3)
                    If strAoffice = "" Then
                        .Parameters("@AOFFICE").Value = DBNull.Value
                    Else
                        .Parameters("@AOFFICE").Value = strAoffice
                    End If

                    .Parameters.Add("@CRS", SqlDbType.VarChar, 20)
                    If strCRS = "" Then
                        .Parameters("@CRS").Value = DBNull.Value
                    Else
                        .Parameters("@CRS").Value = strCRS
                    End If

                    .Parameters.Add("@AGENCY_TYPE", SqlDbType.VarChar, 30)
                    If strAgencyType = "" Then
                        .Parameters("@AGENCY_TYPE").Value = DBNull.Value
                    Else
                        .Parameters("@AGENCY_TYPE").Value = strAgencyType
                    End If

                    .Parameters.Add("@PRIORITY", SqlDbType.VarChar, 20)
                    If strPRIORITY = "" Then
                        .Parameters("@PRIORITY").Value = DBNull.Value
                    Else
                        .Parameters("@PRIORITY").Value = strPRIORITY
                    End If


                    .Parameters.Add("@RESP_ASSGN_FROM", SqlDbType.VarChar, 100)
                    If strResp_Assgn_From = "" Then
                        .Parameters("@RESP_ASSGN_FROM").Value = DBNull.Value
                    Else
                        .Parameters("@RESP_ASSGN_FROM").Value = strResp_Assgn_From
                    End If


                    .Parameters.Add("@RESP_ASSGN_TO", SqlDbType.VarChar, 100)
                    If strResp_Assgn_To = "" Then
                        .Parameters("@RESP_ASSGN_TO").Value = DBNull.Value
                    Else
                        .Parameters("@RESP_ASSGN_TO").Value = strResp_Assgn_To
                    End If


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

                    .Parameters.Add("@COMP_VERTICAL", SqlDbType.SmallInt)
                    If strCOMP_VERTICAL = "" Then
                        .Parameters("@COMP_VERTICAL").Value = DBNull.Value
                    Else
                        .Parameters("@COMP_VERTICAL").Value = Val(strCOMP_VERTICAL)
                    End If


                End With



                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("RESP")
                objAptNodeClone = objAptNode.CloneNode(True)
                If objSqlReader.HasRows = True Then objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("LOCATION_CODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOCATION_CODE")) & "")
                    objAptNodeClone.Attributes("NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NAME")) & "")
                    objAptNodeClone.Attributes("CHAIN_CODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHAIN_CODE")) & "")
                    objAptNodeClone.Attributes("GROUP_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("GROUP_NAME")) & "")
                    objAptNodeClone.Attributes("OFFICEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OFFICEID")) & "")
                    objAptNodeClone.Attributes("ADDRESS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ADDRESS")) & "")
                    objAptNodeClone.Attributes("ADDRESS1").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ADDRESS1")) & "")
                    objAptNodeClone.Attributes("CITY_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITY_NAME")) & "")
                    objAptNodeClone.Attributes("COUNTRY_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("COUNTRY_NAME")) & "")
                    objAptNodeClone.Attributes("ONLINE_STATUS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ONLINE_STATUS")) & "")
                    objAptNodeClone.Attributes("AOFFICE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AOFFICE")) & "")
                    objAptNodeClone.Attributes("CRS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CRS")) & "")
                    objAptNodeClone.Attributes("AGENCY_TYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AGENCY_TYPE")) & "")
                    objAptNodeClone.Attributes("PRIORITY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRIORITY")) & "")
                    objAptNodeClone.Attributes("RESP_ASSGN_FROM").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("RESP_ASSGN_FROM")) & "")
                    objAptNodeClone.Attributes("RESP_ASSGN_TO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("RESP_ASSGN_TO")) & "")
                    '--GET_CHECKED=''
                    objAptNodeClone.Attributes("GET_CHECKED").InnerText = "FALSE"
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop
                objSqlReader.Close()
                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").InnerText = Val(objSqlCommand.Parameters("@TOTALROWS").Value)
                    If intPageSize = 0 Then
                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = 0
                    Else
                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = Math.Ceiling(objSqlCommand.Parameters("@TOTALROWS").Value / intPageSize)
                    End If
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
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml


        End Function
        Public Function Update(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Update
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Dim strAction As String = ""

            Dim strLocation_Code As String
            Dim intLocationCount As Int64
            Dim arrLCode As Array
            Dim objAllLocationCode As New XmlDocument
            Dim objAptNode1, objAptNodeClone1 As XmlNode


            Dim strName As String = ""
            Dim intChain_Code As Integer
            Dim strGroupName As String = ""
            Dim strOfficeID As String = ""
            Dim strCity_Name As String = ""
            Dim strCountry_Name As String = ""
            Dim strOnLineStatus As String = ""
            Dim strAoffice As String = ""
            Dim strCRS As String = ""
            Dim strAgencyType As String = ""
            Dim strPRIORITY As String = ""
            Dim strResp_Assgn_From As String = ""
            Dim strResp_Assgn_To As String = ""
            Dim intRESP_ASSGN_TO_ID As Integer
            Dim intRecordsAffected As Integer
            Dim intRetId As Integer
            Dim intEmployeeID As Integer
            Dim objTran As SqlTransaction



            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strupdate_OUTPUT)
            objAllLocationCode.LoadXml("<X><DETAILS LCODE ='' EMPLOYEEID='' RESP_ASSGN_TO_ID='' /></X>")
            Try

                If UpdateDoc.DocumentElement.SelectSingleNode("ACTION").InnerText.Trim() <> "" Then
                    strAction = UpdateDoc.DocumentElement.SelectSingleNode("ACTION").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText.Trim() <> "" Then
                    strLocation_Code = UpdateDoc.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText.Trim()
                    intLocationCount = Split(strLocation_Code, ",").Length

                    arrLCode = Split(strLocation_Code, ",")
                    ''Reading and Appending records into the Output XMLDocument
                    objAptNode1 = objAllLocationCode.DocumentElement.SelectSingleNode("DETAILS")
                    objAptNodeClone1 = objAptNode1.CloneNode(True)
                    objAllLocationCode.DocumentElement.RemoveChild(objAptNode1)

                    For i As Integer = 0 To intLocationCount - 1
                        objAptNodeClone1.Attributes("LCODE").InnerText = Split(strLocation_Code, ",")(i).Trim()
                        objAptNodeClone1.Attributes("EMPLOYEEID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText.Trim
                        objAptNodeClone1.Attributes("RESP_ASSGN_TO_ID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("RESP_ASSGN_TO_ID").InnerText.Trim

                        objAllLocationCode.DocumentElement.AppendChild(objAptNodeClone1)
                        objAptNodeClone1 = objAptNode1.CloneNode(True)
                    Next
                    'objAllLocationCode.DocumentElement.SelectSingleNode("DETAILS").InnerText = strLocation_Code
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("NAME").InnerText.Trim() <> "" Then
                    strName = UpdateDoc.DocumentElement.SelectSingleNode("NAME").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText.Trim() <> "" Then
                    intChain_Code = UpdateDoc.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("GROUP_NAME").InnerText.Trim() <> "" Then
                    strGroupName = UpdateDoc.DocumentElement.SelectSingleNode("GROUP_NAME").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("OFFICEID").InnerText.Trim() <> "" Then
                    strOfficeID = UpdateDoc.DocumentElement.SelectSingleNode("OFFICEID").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("CITY_NAME").InnerText.Trim() <> "" Then
                    strCity_Name = UpdateDoc.DocumentElement.SelectSingleNode("CITY_NAME").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("COUNTRY_NAME").InnerText.Trim() <> "" Then
                    strCountry_Name = UpdateDoc.DocumentElement.SelectSingleNode("COUNTRY_NAME").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("ONLINE_STATUS").InnerText.Trim() <> "" Then
                    strOnLineStatus = UpdateDoc.DocumentElement.SelectSingleNode("ONLINE_STATUS").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("AOFFICE").InnerText.Trim() <> "" Then
                    strAoffice = UpdateDoc.DocumentElement.SelectSingleNode("AOFFICE").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("CRS").InnerText.Trim() <> "" Then
                    strCRS = UpdateDoc.DocumentElement.SelectSingleNode("CRS").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("AGENCY_TYPE").InnerText.Trim() <> "" Then
                    strAgencyType = UpdateDoc.DocumentElement.SelectSingleNode("AGENCY_TYPE").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("PRIORITY").InnerText.Trim() <> "" Then
                    strPRIORITY = UpdateDoc.DocumentElement.SelectSingleNode("PRIORITY").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("RESP_ASSGN_FROM").InnerText.Trim() <> "" Then
                    strResp_Assgn_From = UpdateDoc.DocumentElement.SelectSingleNode("RESP_ASSGN_FROM").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("RESP_ASSGN_TO").InnerText.Trim() <> "" Then
                    strResp_Assgn_To = UpdateDoc.DocumentElement.SelectSingleNode("RESP_ASSGN_TO").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("RESP_ASSGN_TO_ID").InnerText.Trim() <> "" Then
                    intRESP_ASSGN_TO_ID = UpdateDoc.DocumentElement.SelectSingleNode("RESP_ASSGN_TO_ID").InnerText.Trim()
                End If

                'intEmployeeID

                If UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText.Trim() <> "" Then
                    intEmployeeID = UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText.Trim()
                End If



                If strAction = "U" Then
                    If intRESP_ASSGN_TO_ID = 0 And strResp_Assgn_To = "" Then
                        Throw (New AAMSException("Resp_Assgn_To can't be blank."))
                    End If

                    If intEmployeeID = 0 And Len(intEmployeeID) = 0 Then
                        Throw (New AAMSException("Employee ID can't be blank."))
                    End If


                Else
                    Throw (New AAMSException("Invalid Action Code."))
                End If

                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_RESP_CHANGE"
                    .Connection = objSqlConnection

                    .Parameters.Add("@ACTION", SqlDbType.Char, 1)
                    If strAction = "" Then
                        .Parameters("@ACTION").Value = DBNull.Value
                    Else
                        .Parameters("@ACTION").Value = strAction
                    End If


                    .Parameters.Add("@LOCATION_CODE", SqlDbType.VarChar, 8000)
                    If strLocation_Code = "" Then
                        .Parameters("@LOCATION_CODE").Value = DBNull.Value
                    Else
                        .Parameters("@LOCATION_CODE").Value = DBNull.Value
                    End If

                    .Parameters.Add("@INPUTXML", SqlDbType.Xml)
                    If strLocation_Code = "" Then
                        .Parameters("@INPUTXML").Value = DBNull.Value
                    Else
                        .Parameters("@INPUTXML").Value = objAllLocationCode.OuterXml
                    End If

                    .Parameters.Add("@NAME", SqlDbType.VarChar, 100)
                    If strName = "" Then
                        .Parameters("@NAME").Value = DBNull.Value
                    Else
                        .Parameters("@NAME").Value = strName
                    End If

                    .Parameters.Add("@CHAIN_CODE", SqlDbType.Int)
                    If intChain_Code = 0 Then
                        .Parameters("@CHAIN_CODE").Value = DBNull.Value
                    Else
                        .Parameters("@CHAIN_CODE").Value = intChain_Code
                    End If

                    .Parameters.Add("@GROUP_NAME", SqlDbType.VarChar, 100)
                    If strGroupName = "" Then
                        .Parameters("@GROUP_NAME").Value = DBNull.Value
                    Else
                        .Parameters("@GROUP_NAME").Value = strGroupName
                    End If

                    .Parameters.Add("@OFFICEID", SqlDbType.VarChar, 15)
                    If strOfficeID = "" Then
                        .Parameters("@OFFICEID").Value = DBNull.Value
                    Else
                        .Parameters("@OFFICEID").Value = strOfficeID
                    End If

                    .Parameters.Add("@CITY_NAME", SqlDbType.VarChar, 100)
                    If strCity_Name = "" Then
                        .Parameters("@CITY_NAME").Value = DBNull.Value
                    Else
                        .Parameters("@CITY_NAME").Value = strCity_Name
                    End If

                    .Parameters.Add("@COUNTRY_NAME", SqlDbType.VarChar, 100)
                    If strCountry_Name = "" Then
                        .Parameters("@COUNTRY_NAME").Value = DBNull.Value
                    Else
                        .Parameters("@COUNTRY_NAME").Value = strCountry_Name
                    End If

                    .Parameters.Add("@ONLINE_STATUS", SqlDbType.VarChar, 25)
                    If strOnLineStatus = "" Then
                        .Parameters("@ONLINE_STATUS").Value = DBNull.Value
                    Else
                        .Parameters("@ONLINE_STATUS").Value = strOnLineStatus
                    End If

                    .Parameters.Add("@AOFFICE", SqlDbType.Char, 3)
                    If strAoffice = "" Then
                        .Parameters("@AOFFICE").Value = DBNull.Value
                    Else
                        .Parameters("@AOFFICE").Value = strAoffice
                    End If

                    .Parameters.Add("@CRS", SqlDbType.VarChar, 20)
                    If strCRS = "" Then
                        .Parameters("@CRS").Value = DBNull.Value
                    Else
                        .Parameters("@CRS").Value = strCRS
                    End If

                    .Parameters.Add("@AGENCY_TYPE", SqlDbType.VarChar, 30)
                    If strAgencyType = "" Then
                        .Parameters("@AGENCY_TYPE").Value = DBNull.Value
                    Else
                        .Parameters("@AGENCY_TYPE").Value = strAgencyType
                    End If

                    .Parameters.Add("@PRIORITY", SqlDbType.VarChar, 20)
                    If strPRIORITY = "" Then
                        .Parameters("@PRIORITY").Value = DBNull.Value
                    Else
                        .Parameters("@PRIORITY").Value = strPRIORITY
                    End If


                    .Parameters.Add("@RESP_ASSGN_FROM", SqlDbType.VarChar, 100)
                    If strResp_Assgn_From = "" Then
                        .Parameters("@RESP_ASSGN_FROM").Value = DBNull.Value
                    Else
                        .Parameters("@RESP_ASSGN_FROM").Value = strResp_Assgn_From
                    End If


                    .Parameters.Add("@RESP_ASSGN_TO", SqlDbType.VarChar, 100)
                    If strResp_Assgn_To = "" Then
                        .Parameters("@RESP_ASSGN_TO").Value = DBNull.Value
                    Else
                        .Parameters("@RESP_ASSGN_TO").Value = strResp_Assgn_To
                    End If


                    .Parameters.Add("@RESP_ASSGN_TO_ID", SqlDbType.BigInt)
                    If intRESP_ASSGN_TO_ID = 0 Then
                        .Parameters("@RESP_ASSGN_TO_ID").Value = DBNull.Value
                    Else
                        .Parameters("@RESP_ASSGN_TO_ID").Value = intRESP_ASSGN_TO_ID
                    End If




                    .Parameters.Add("@EMPLOYEEID", SqlDbType.BigInt)
                    If intEmployeeID = 0 Then
                        .Parameters("@EMPLOYEEID").Value = DBNull.Value
                    Else
                        .Parameters("@EMPLOYEEID").Value = intEmployeeID
                    End If




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

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.BigInt))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0


                    .Parameters.Add(New SqlParameter("@TOTALROWS", SqlDbType.BigInt))
                    .Parameters("@TOTALROWS").Direction = ParameterDirection.Output
                    .Parameters("@TOTALROWS").Value = 0



                End With

                'With UpdateDoc.DocumentElement

                '    If (.SelectSingleNode("ACTION").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@ACTION").Value = (.SelectSingleNode("ACTION").InnerXml)
                '    End If

                '    If (.SelectSingleNode("LOCATION_CODE").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@LOCATION_CODE").Value = .SelectSingleNode("LOCATION_CODE").InnerXml
                '    End If

                '    If (.SelectSingleNode("NAME").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@NAME").Value = .SelectSingleNode("NAME").InnerXml
                '    End If


                '    If (.SelectSingleNode("CHAIN_CODE").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@CHAIN_CODE").Value = .SelectSingleNode("CHAIN_CODE").InnerXml
                '    End If

                '    If (.SelectSingleNode("GROUP_NAME").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@GROUP_NAME").Value = .SelectSingleNode("GROUP_NAME").InnerXml
                '    End If

                '    If (.SelectSingleNode("OFFICEID").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@OFFICEID").Value = .SelectSingleNode("OFFICEID").InnerXml
                '    End If



                '    If (.SelectSingleNode("CITY_NAME").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@CITY_NAME").Value = .SelectSingleNode("CITY_NAME").InnerXml
                '    End If

                '    If (.SelectSingleNode("COUNTRY_NAME").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@COUNTRY_NAME").Value = .SelectSingleNode("COUNTRY_NAME").InnerXml
                '    End If
                '    If (.SelectSingleNode("ONLINE_STATUS").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@ONLINE_STATUS").Value = .SelectSingleNode("ONLINE_STATUS").InnerXml
                '    End If
                '    If (.SelectSingleNode("AOFFICE").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@AOFFICE").Value = .SelectSingleNode("AOFFICE").InnerXml
                '    End If
                '    If (.SelectSingleNode("CRS").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@CRS").Value = .SelectSingleNode("CRS").InnerXml
                '    End If
                '    If (.SelectSingleNode("AGENCY_TYPE").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@AGENCY_TYPE").Value = .SelectSingleNode("AGENCY_TYPE").InnerXml
                '    End If


                '    If (.SelectSingleNode("PRIORITY").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@PRIORITY").Value = .SelectSingleNode("PRIORITY").InnerXml
                '    End If

                '    If (.SelectSingleNode("RESP_ASSGN_FROM").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@RESP_ASSGN_FROM").Value = .SelectSingleNode("RESP_ASSGN_FROM").InnerXml
                '    End If

                '    If (.SelectSingleNode("RESP_ASSGN_TO").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@RESP_ASSGN_TO").Value = .SelectSingleNode("RESP_ASSGN_TO").InnerXml
                '    End If

                '    If (.SelectSingleNode("RESP_ASSGN_TO_ID").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@RESP_ASSGN_TO_ID").Value = .SelectSingleNode("RESP_ASSGN_TO_ID").InnerXml
                '    End If

                'End With


                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()

                objTran = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)
                objSqlCommand.Transaction = objTran


                intRecordsAffected = objSqlCommand.ExecuteNonQuery()
                'objSqlReader = objSqlCommand.ExecuteReader()

                If UCase(strAction) = "U" Then
                    intRetId = objSqlCommand.Parameters("@RETURNID").Value

                    If intRetId = 0 Then
                        Throw (New AAMSException("Unable to update!"))
                    ElseIf intRetId = -1 Then
                        Throw (New AAMSException("Unable to update!"))
                    Else
                        objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    End If

                    objTran.Commit()
                End If

            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)

                If Not objTran Is Nothing Then
                    objTran.Rollback()
                End If

                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml

            Catch Exec As Exception
                If intRetId = 0 Then
                    If Not objTran Is Nothing Then
                        objTran.Rollback()
                    End If

                    bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                    If strAction = "U" Then
                        bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                        Return objOutputXml
                    End If

                End If

            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try

            Return objOutputXml

        End Function
        Public Function Deallocate(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean
            Dim strAction As String = ""

            Dim strLocation_Code As String
            Dim intLocationCount As Int64
            Dim arrLCode As Array
            Dim objAllLocationCode As New XmlDocument
            Dim objAptNode1, objAptNodeClone1 As XmlNode


            Dim strName As String = ""
            Dim intChain_Code As Integer
            Dim strGroupName As String = ""
            Dim strOfficeID As String = ""
            Dim strCity_Name As String = ""
            Dim strCountry_Name As String = ""
            Dim strOnLineStatus As String = ""
            Dim strAoffice As String = ""
            Dim strCRS As String = ""
            Dim strAgencyType As String = ""
            Dim strPRIORITY As String = ""
            Dim strResp_Assgn_From As String = ""
            Dim strResp_Assgn_To As String = ""
            Dim intRESP_ASSGN_TO_ID As Integer
            Dim intRecordsAffected As Integer
            Dim intRetId As Integer
            Dim objTran As SqlTransaction
            Dim intEmployeeID As Integer

            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strupdate_OUTPUT)
            ' objAllLocationCode.LoadXml("<X><DETAILS LCODE =''/></X>")
            objAllLocationCode.LoadXml("<X><DETAILS LCODE ='' EMPLOYEEID='' RESP_ASSGN_TO_ID='' /></X>")
            Try

                If UpdateDoc.DocumentElement.SelectSingleNode("ACTION").InnerText.Trim() <> "" Then
                    strAction = UpdateDoc.DocumentElement.SelectSingleNode("ACTION").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText.Trim() <> "" Then
                    strLocation_Code = UpdateDoc.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText.Trim()
                    intLocationCount = Split(strLocation_Code, ",").Length

                    arrLCode = Split(strLocation_Code, ",")
                    ''Reading and Appending records into the Output XMLDocument
                    objAptNode1 = objAllLocationCode.DocumentElement.SelectSingleNode("DETAILS")
                    objAptNodeClone1 = objAptNode1.CloneNode(True)
                    objAllLocationCode.DocumentElement.RemoveChild(objAptNode1)

                    For i As Integer = 0 To intLocationCount - 1
                        'objAptNodeClone1.Attributes("LCODE").InnerText = Split(strLocation_Code, ",")(i).Trim()
                        objAptNodeClone1.Attributes("LCODE").InnerText = Split(strLocation_Code, ",")(i).Trim()
                        objAptNodeClone1.Attributes("EMPLOYEEID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText.Trim
                        objAptNodeClone1.Attributes("RESP_ASSGN_TO_ID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("RESP_ASSGN_TO_ID").InnerText.Trim

                        objAllLocationCode.DocumentElement.AppendChild(objAptNodeClone1)
                        objAptNodeClone1 = objAptNode1.CloneNode(True)
                    Next
                    'objAllLocationCode.DocumentElement.SelectSingleNode("DETAILS").InnerText = strLocation_Code
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("NAME").InnerText.Trim() <> "" Then
                    strName = UpdateDoc.DocumentElement.SelectSingleNode("NAME").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText.Trim() <> "" Then
                    intChain_Code = UpdateDoc.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("GROUP_NAME").InnerText.Trim() <> "" Then
                    strGroupName = UpdateDoc.DocumentElement.SelectSingleNode("GROUP_NAME").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("OFFICEID").InnerText.Trim() <> "" Then
                    strOfficeID = UpdateDoc.DocumentElement.SelectSingleNode("OFFICEID").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("CITY_NAME").InnerText.Trim() <> "" Then
                    strCity_Name = UpdateDoc.DocumentElement.SelectSingleNode("CITY_NAME").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("COUNTRY_NAME").InnerText.Trim() <> "" Then
                    strCountry_Name = UpdateDoc.DocumentElement.SelectSingleNode("COUNTRY_NAME").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("ONLINE_STATUS").InnerText.Trim() <> "" Then
                    strOnLineStatus = UpdateDoc.DocumentElement.SelectSingleNode("ONLINE_STATUS").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("AOFFICE").InnerText.Trim() <> "" Then
                    strAoffice = UpdateDoc.DocumentElement.SelectSingleNode("AOFFICE").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("CRS").InnerText.Trim() <> "" Then
                    strCRS = UpdateDoc.DocumentElement.SelectSingleNode("CRS").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("AGENCY_TYPE").InnerText.Trim() <> "" Then
                    strAgencyType = UpdateDoc.DocumentElement.SelectSingleNode("AGENCY_TYPE").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("PRIORITY").InnerText.Trim() <> "" Then
                    strPRIORITY = UpdateDoc.DocumentElement.SelectSingleNode("PRIORITY").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("RESP_ASSGN_FROM").InnerText.Trim() <> "" Then
                    strResp_Assgn_From = UpdateDoc.DocumentElement.SelectSingleNode("RESP_ASSGN_FROM").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("RESP_ASSGN_TO").InnerText.Trim() <> "" Then
                    strResp_Assgn_To = UpdateDoc.DocumentElement.SelectSingleNode("RESP_ASSGN_TO").InnerText.Trim()
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("RESP_ASSGN_TO_ID").InnerText.Trim() <> "" Then
                    intRESP_ASSGN_TO_ID = UpdateDoc.DocumentElement.SelectSingleNode("RESP_ASSGN_TO_ID").InnerText.Trim()
                End If

                'intEmployeeID

                If UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText.Trim() <> "" Then
                    intEmployeeID = UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText.Trim()
                End If



                'If strAction = "D" Then
                '    If strResp_Assgn_To = "" And intRESP_ASSGN_TO_ID = 0 Then
                '        Throw (New AAMSException("Resp_Assgn_To can't be blank."))
                '    End If
                'Else
                '    Throw (New AAMSException("Invalid Action Code."))
                'End If

                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_RESP_CHANGE"
                    .Connection = objSqlConnection

                    .Parameters.Add("@ACTION", SqlDbType.Char, 1)
                    If strAction = "" Then
                        .Parameters("@ACTION").Value = DBNull.Value
                    Else
                        .Parameters("@ACTION").Value = strAction
                    End If


                    .Parameters.Add("@LOCATION_CODE", SqlDbType.VarChar, 8000)
                    If strLocation_Code = "" Then
                        .Parameters("@LOCATION_CODE").Value = DBNull.Value
                    Else
                        .Parameters("@LOCATION_CODE").Value = DBNull.Value
                    End If

                    .Parameters.Add("@INPUTXML", SqlDbType.Xml)
                    If strLocation_Code = "" Then
                        .Parameters("@INPUTXML").Value = DBNull.Value
                    Else
                        .Parameters("@INPUTXML").Value = objAllLocationCode.OuterXml
                    End If

                    .Parameters.Add("@NAME", SqlDbType.VarChar, 100)
                    If strName = "" Then
                        .Parameters("@NAME").Value = DBNull.Value
                    Else
                        .Parameters("@NAME").Value = strName
                    End If

                    .Parameters.Add("@CHAIN_CODE", SqlDbType.Int)
                    If intChain_Code = 0 Then
                        .Parameters("@CHAIN_CODE").Value = DBNull.Value
                    Else
                        .Parameters("@CHAIN_CODE").Value = intChain_Code
                    End If

                    .Parameters.Add("@GROUP_NAME", SqlDbType.VarChar, 100)
                    If strGroupName = "" Then
                        .Parameters("@GROUP_NAME").Value = DBNull.Value
                    Else
                        .Parameters("@GROUP_NAME").Value = strGroupName
                    End If

                    .Parameters.Add("@OFFICEID", SqlDbType.VarChar, 15)
                    If strOfficeID = "" Then
                        .Parameters("@OFFICEID").Value = DBNull.Value
                    Else
                        .Parameters("@OFFICEID").Value = strOfficeID
                    End If

                    .Parameters.Add("@CITY_NAME", SqlDbType.VarChar, 100)
                    If strCity_Name = "" Then
                        .Parameters("@CITY_NAME").Value = DBNull.Value
                    Else
                        .Parameters("@CITY_NAME").Value = strCity_Name
                    End If

                    .Parameters.Add("@COUNTRY_NAME", SqlDbType.VarChar, 100)
                    If strCountry_Name = "" Then
                        .Parameters("@COUNTRY_NAME").Value = DBNull.Value
                    Else
                        .Parameters("@COUNTRY_NAME").Value = strCountry_Name
                    End If

                    .Parameters.Add("@ONLINE_STATUS", SqlDbType.VarChar, 25)
                    If strOnLineStatus = "" Then
                        .Parameters("@ONLINE_STATUS").Value = DBNull.Value
                    Else
                        .Parameters("@ONLINE_STATUS").Value = strOnLineStatus
                    End If

                    .Parameters.Add("@AOFFICE", SqlDbType.Char, 3)
                    If strAoffice = "" Then
                        .Parameters("@AOFFICE").Value = DBNull.Value
                    Else
                        .Parameters("@AOFFICE").Value = strAoffice
                    End If

                    .Parameters.Add("@CRS", SqlDbType.VarChar, 20)
                    If strCRS = "" Then
                        .Parameters("@CRS").Value = DBNull.Value
                    Else
                        .Parameters("@CRS").Value = strCRS
                    End If

                    .Parameters.Add("@AGENCY_TYPE", SqlDbType.VarChar, 30)
                    If strAgencyType = "" Then
                        .Parameters("@AGENCY_TYPE").Value = DBNull.Value
                    Else
                        .Parameters("@AGENCY_TYPE").Value = strAgencyType
                    End If

                    .Parameters.Add("@PRIORITY", SqlDbType.VarChar, 20)
                    If strPRIORITY = "" Then
                        .Parameters("@PRIORITY").Value = DBNull.Value
                    Else
                        .Parameters("@PRIORITY").Value = strPRIORITY
                    End If


                    .Parameters.Add("@RESP_ASSGN_FROM", SqlDbType.VarChar, 100)
                    If strResp_Assgn_From = "" Then
                        .Parameters("@RESP_ASSGN_FROM").Value = DBNull.Value
                    Else
                        .Parameters("@RESP_ASSGN_FROM").Value = strResp_Assgn_From
                    End If


                    .Parameters.Add("@RESP_ASSGN_TO", SqlDbType.VarChar, 100)
                    If strResp_Assgn_To = "" Then
                        .Parameters("@RESP_ASSGN_TO").Value = DBNull.Value
                    Else
                        .Parameters("@RESP_ASSGN_TO").Value = strResp_Assgn_To
                    End If


                    .Parameters.Add("@RESP_ASSGN_TO_ID", SqlDbType.BigInt)
                    If intRESP_ASSGN_TO_ID = 0 Then
                        .Parameters("@RESP_ASSGN_TO_ID").Value = DBNull.Value
                    Else
                        .Parameters("@RESP_ASSGN_TO_ID").Value = intRESP_ASSGN_TO_ID
                    End If


                    .Parameters.Add("@EMPLOYEEID", SqlDbType.BigInt)
                    If intEmployeeID = 0 Then
                        .Parameters("@EMPLOYEEID").Value = DBNull.Value
                    Else
                        .Parameters("@EMPLOYEEID").Value = intEmployeeID
                    End If



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

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.BigInt))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0


                    .Parameters.Add(New SqlParameter("@TOTALROWS", SqlDbType.BigInt))
                    .Parameters("@TOTALROWS").Direction = ParameterDirection.Output
                    .Parameters("@TOTALROWS").Value = 0



                End With

                'With UpdateDoc.DocumentElement

                '    If (.SelectSingleNode("ACTION").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@ACTION").Value = (.SelectSingleNode("ACTION").InnerXml)
                '    End If

                '    If (.SelectSingleNode("LOCATION_CODE").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@LOCATION_CODE").Value = .SelectSingleNode("LOCATION_CODE").InnerXml
                '    End If

                '    If (.SelectSingleNode("NAME").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@NAME").Value = .SelectSingleNode("NAME").InnerXml
                '    End If


                '    If (.SelectSingleNode("CHAIN_CODE").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@CHAIN_CODE").Value = .SelectSingleNode("CHAIN_CODE").InnerXml
                '    End If

                '    If (.SelectSingleNode("GROUP_NAME").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@GROUP_NAME").Value = .SelectSingleNode("GROUP_NAME").InnerXml
                '    End If

                '    If (.SelectSingleNode("OFFICEID").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@OFFICEID").Value = .SelectSingleNode("OFFICEID").InnerXml
                '    End If



                '    If (.SelectSingleNode("CITY_NAME").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@CITY_NAME").Value = .SelectSingleNode("CITY_NAME").InnerXml
                '    End If

                '    If (.SelectSingleNode("COUNTRY_NAME").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@COUNTRY_NAME").Value = .SelectSingleNode("COUNTRY_NAME").InnerXml
                '    End If
                '    If (.SelectSingleNode("ONLINE_STATUS").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@ONLINE_STATUS").Value = .SelectSingleNode("ONLINE_STATUS").InnerXml
                '    End If
                '    If (.SelectSingleNode("AOFFICE").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@AOFFICE").Value = .SelectSingleNode("AOFFICE").InnerXml
                '    End If
                '    If (.SelectSingleNode("CRS").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@CRS").Value = .SelectSingleNode("CRS").InnerXml
                '    End If
                '    If (.SelectSingleNode("AGENCY_TYPE").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@AGENCY_TYPE").Value = .SelectSingleNode("AGENCY_TYPE").InnerXml
                '    End If


                '    If (.SelectSingleNode("PRIORITY").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@PRIORITY").Value = .SelectSingleNode("PRIORITY").InnerXml
                '    End If

                '    If (.SelectSingleNode("RESP_ASSGN_FROM").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@RESP_ASSGN_FROM").Value = .SelectSingleNode("RESP_ASSGN_FROM").InnerXml
                '    End If

                '    If (.SelectSingleNode("RESP_ASSGN_TO").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@RESP_ASSGN_TO").Value = .SelectSingleNode("RESP_ASSGN_TO").InnerXml
                '    End If

                '    If (.SelectSingleNode("RESP_ASSGN_TO_ID").InnerXml.Trim <> "") Then
                '        objSqlCommand.Parameters("@RESP_ASSGN_TO_ID").Value = .SelectSingleNode("RESP_ASSGN_TO_ID").InnerXml
                '    End If

                'End With


                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()

                objTran = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)
                objSqlCommand.Transaction = objTran
                intRecordsAffected = objSqlCommand.ExecuteNonQuery()

                If UCase(strAction) = "D" Then
                    intRetId = objSqlCommand.Parameters("@RETURNID").Value
                    If intRetId = 0 Then
                        Throw (New AAMSException("Unable to update!"))
                    ElseIf intRetId = -1 Then
                        Throw (New AAMSException("Unable to update!"))
                    Else
                        objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    End If
                    objTran.Commit()
                End If

            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                If Not objTran Is Nothing Then
                    objTran.Rollback()
                End If
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml

            Catch Exec As Exception
                If intRetId = 0 Then
                    bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)

                    If Not objTran Is Nothing Then
                        objTran.Rollback()
                    End If

                    If strAction = "D" Then
                        bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                        Return objOutputXml
                    End If


                End If

            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try

            Return objOutputXml
        End Function
        Public Function History(ByVal HisDoc As XmlDocument) As XmlDocument
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim intLcode As Integer
            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Const strMETHOD_NAME As String = "History"
            objOutputXml.LoadXml(strHistory_OUTPUT)

            Try


                If HisDoc.DocumentElement.SelectSingleNode("Lcode").InnerText.Trim() <> "" Then
                    intLcode = HisDoc.DocumentElement.SelectSingleNode("Lcode").InnerText.Trim()
                End If


                'Paging Section    
                If HisDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim() <> "" Then
                    intPageNo = HisDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim()
                End If
                If HisDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim() <> "" Then
                    intPageSize = HisDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim()
                End If

                If HisDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim() <> "" Then
                    strSortBy = HisDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim()
                End If

                If HisDoc.DocumentElement.SelectSingleNode("DESC").InnerText.Trim().ToUpper = "TRUE" Then
                    blnDesc = True
                Else
                    blnDesc = False
                End If

                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_TA_RESP_1A_HIS"
                    .Connection = objSqlConnection


                    .Parameters.Add("@Lcode", SqlDbType.BigInt)
                    If intLcode = 0 Then
                        .Parameters("@Lcode").Value = DBNull.Value
                    Else
                        .Parameters("@Lcode").Value = intLcode
                    End If


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

                End With



                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("RESP")
                objAptNodeClone = objAptNode.CloneNode(True)
                If objSqlReader.HasRows = True Then objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("EMPNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMPNAME")) & "")
                    'Format(departDate, "DD/MM/YYY")
                    objAptNodeClone.Attributes("DATE").InnerText = Format(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE")), "dd/MMM/yyyy:hh:mm:ss")
                    objAptNodeClone.Attributes("RESP_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("RESP_NAME")) & "")
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop
                objSqlReader.Close()
                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").InnerText = Val(objSqlCommand.Parameters("@TOTALROWS").Value)
                    If intPageSize = 0 Then
                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = 0
                    Else
                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = Math.Ceiling(objSqlCommand.Parameters("@TOTALROWS").Value / intPageSize)
                    End If
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
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml

        End Function

        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add

        End Function
        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Delete

        End Function
        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.View

        End Function




    End Class




End Namespace


