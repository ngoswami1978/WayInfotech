'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Tapan $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizTravelAgency/bzOfficeID.vb $
'$Workfile: bzOfficeID.vb $
'$Revision: 17 $
'$Archive: /AAMS/Components/bizTravelAgency/bzOfficeID.vb $
'$Modtime: 8/08/08 8:55p $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizTravelAgency
    Public Class bzOfficeID
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzOfficeID"
        Const strListOfficeType_OUTPUT = "<TA_LISTOFFICETYPE_OUTPUT><OFFICETYPE OFFICEID_TYPE_ID='' OFFICEID_TYPE_NAME='' TYPE_TRAVEL_AGENCY='' /><Errors Status=''><Error Code='' Description='' /></Errors></TA_LISTOFFICETYPE_OUTPUT>"
        Const strSEARCH_OUTPUT = "<TA_SEARCHOFFICEID_OUTPUT><OFFICEID OFFICEID='' NAME='' CID='' PROCESSING_DATE='' TERMINALID='' REMARKS=''/><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></TA_SEARCHOFFICEID_OUTPUT>"
        Const rpt_Summaryofficeid = "<TA_RPT_SUMMARYOFFICEID_OUTPUT><SUMMARY CityCode='' COUNT='' /><Errors Status=''><Error Code='' Description='' /></Errors></TA_RPT_SUMMARYOFFICEID_OUTPUT>"

        Const srtVIEW_OUTPUT = "<MS_VIEWOFFICEID_OUTPUT><OFFICEID OFFICEID='' NAME='' CID='' PROCESSING_DATE='' TERMINALID='' REMARKS='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWOFFICEID_OUTPUT>"
        Const strUPDATE_OUTPUT = "<TA_UPDATEOFFICEID_OUTPUT><OFFICEID OFFICEID='' NAME='' CID='' PROCESSING_DATE='' TERMINALID='' REMARKS='' /><Errors Status=''><Error Code='' Description='' /></Errors></TA_UPDATEOFFICEID_OUTPUT>"
        Const strGenOfficeID_OUTPUT = "<TA_GENERATEOFFICEID_OUTPUT><GENERATEOFFICEID OFFICEID='' CityCode='' CorporateCode='' CorporateQualifier='' OFFICEID_TYPE_ID='' /><Errors Status=''><Error Code='' Description='' /></Errors></TA_GENERATEOFFICEID_OUTPUT>"
        Const strGetAgencyCCode_OUTPUT = "<TA_AGENCYCORPORATECODE_OUTPUT><AGENCYCORPORATECODE CCode=''/><Errors Status=''><Error Code='' Description='' /></Errors></TA_AGENCYCORPORATECODE_OUTPUT>"
        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add

        End Function
        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Delete

        End Function
        Public Function Search(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Search
            'Purpose:This function gives search results based on choosen search criterion.
            'Input  :- 
            '<TA_SEARCHOFFICEID_INPUT>
            '	<OFFICEID></OFFICEID>
            '	<CityCode></CityCode>
            '	<ALLOCATED></ALLOCATED>
            '   <CCode></CCode>
            '</TA_SEARCHOFFICEID_INPUT>

            'Output :
            '<TA_SEARCHOFFICEID_OUTPUT>
            '	<OFFICEID OFFICEID='' NAME='' CID='' PROCESSING_DATE='' TERMINALID='' REMARKS=''/>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</TA_SEARCHOFFICEID_OUTPUT>

            ''************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean

            Dim strOfficeID As String = vbNullString, strCityCode As String = vbNullString, blnunAllocated As Boolean
            Dim strCCode As String
            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean


            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                If SearchDoc.DocumentElement.SelectSingleNode("OFFICEID").InnerText.Trim() <> "" Then
                    strOfficeID = SearchDoc.DocumentElement.SelectSingleNode("OFFICEID").InnerText.Trim()
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("CityCode").InnerText.Trim() <> "" Then
                    strCityCode = SearchDoc.DocumentElement.SelectSingleNode("CityCode").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("ALLOCATED").InnerText.ToUpper.Trim() = "TRUE" Then
                    blnunAllocated = True
                Else
                    blnunAllocated = False
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("CCode").InnerText.Trim() <>"" Then
                    strCCode = SearchDoc.DocumentElement.SelectSingleNode("CCode").InnerText.Trim()
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



                '--------Retrieving & Checking Details from Input XMLDocument ------End
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SER_TA_OFFICEID"
                    .Connection = objSqlConnection

                    .Parameters.Add("@OFFICEID", SqlDbType.VarChar, 9)
                    .Parameters("@OFFICEID").Value = strOfficeID

                    .Parameters.Add("@CITYCODE", SqlDbType.VarChar, 3)
                    .Parameters("@CITYCODE").Value = strCityCode

                    .Parameters.Add("@CCode", SqlDbType.VarChar, 2)
                    .Parameters("@CCode").Value = strCCode

                    .Parameters.Add("@ALLOCATED", SqlDbType.Bit, 1)
                    If blnunAllocated = True Then
                        .Parameters("@ALLOCATED").Value = 0
                    Else
                        .Parameters("@ALLOCATED").Value = 1
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
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("OFFICEID")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("OFFICEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OFFICEID")))
                    objAptNodeClone.Attributes("NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NAME")) & "")
                    objAptNodeClone.Attributes("CID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CID")) & "")
                    objAptNodeClone.Attributes("PROCESSING_DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PROCESSING_DATE")) & "")
                    objAptNodeClone.Attributes("TERMINALID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("TERMINALID")) & "")
                    objAptNodeClone.Attributes("REMARKS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("REMARKS")) & "")
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
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
            '***********************************************************************
            'Purpose:This function Updates OfficeID.
            'Input  :
            '<TA_UPDATEOFFICEID_INPUT>
            '	<OFFICEID OFFICEID='' NAME='' CID='' PROCESSING_DATE='' TERMINALID='' REMARKS='' />
            '</TA_UPDATEOFFICEID_INPUT>

            'Output :
            '<TA_UPDATEOFFICEID_OUTPUT>
            '	<OFFICEID OFFICEID='' NAME='' CID='' PROCESSING_DATE='' TERMINALID='' REMARKS='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</TA_UPDATEOFFICEID_OUTPUT>

            '************************************************************************
            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objUpdateDocOutput As New XmlDocument

            Dim strOfficeID As String, strAgencyName As String, strCID As String, strTerminalID As String
            Dim intProcessing_Date As Integer, strRemarks As String
            Dim strCityCode As String

            Dim intRecordsAffected As Int32
            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)

                With objUpdateDocOutput.DocumentElement.SelectSingleNode("OFFICEID")
                    .Attributes("OFFICEID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("OFFICEID").Attributes("OFFICEID").InnerText
                    .Attributes("NAME").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("OFFICEID").Attributes("NAME").InnerText
                    .Attributes("CID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("OFFICEID").Attributes("CID").InnerText
                    .Attributes("PROCESSING_DATE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("OFFICEID").Attributes("PROCESSING_DATE").InnerText
                    .Attributes("TERMINALID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("OFFICEID").Attributes("TERMINALID").InnerText
                    .Attributes("REMARKS").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("OFFICEID").Attributes("REMARKS").InnerText
                End With

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("OFFICEID")
                    If ((.Attributes("OFFICEID").InnerText).Trim).ToString <> "" Then
                        strAction = "U"
                        strOfficeID = .Attributes("OFFICEID").InnerText.Trim()
                    End If
                    If ((.Attributes("NAME").InnerText).Trim).ToString <> "" Then
                        strCityCode = .Attributes("NAME").InnerText.Trim()
                    End If

                    If .Attributes("CID").InnerText.Trim() <> "" Then
                        strCID = .Attributes("CID").InnerText.Trim()
                    End If

                    If .Attributes("PROCESSING_DATE").InnerText.Trim() <> "" Then
                        intProcessing_Date = .Attributes("PROCESSING_DATE").InnerText.Trim()
                    End If

                    If .Attributes("TERMINALID").InnerText.Trim() <> "" Then
                        strTerminalID = .Attributes("TERMINALID").InnerText.Trim()
                    End If

                    If .Attributes("REMARKS").InnerText.Trim() <> "" Then
                        strRemarks = .Attributes("REMARKS").InnerText.Trim()
                    End If

                    If strAction = "U" Then
                        If strOfficeID = "" Then
                            Throw (New AAMSException("OfficeID can't be blank."))
                        End If
                    Else
                        Throw (New AAMSException("Invalid Action Code."))
                    End If

                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_OFFICEID"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction

                    .Parameters.Add(New SqlParameter("@OFFICEID", SqlDbType.VarChar, 9))
                    .Parameters("@OFFICEID").Value = strOfficeID

                    .Parameters.Add(New SqlParameter("@CID", SqlDbType.VarChar, 25))
                    .Parameters("@CID").Value = strCID

                    .Parameters.Add(New SqlParameter("@TERMINALID", SqlDbType.VarChar, 25))
                    .Parameters("@TERMINALID").Value = strTerminalID

                    .Parameters.Add(New SqlParameter("@PROCESSING_DATE", SqlDbType.Int))
                    .Parameters("@PROCESSING_DATE").Value = intProcessing_Date

                    .Parameters.Add(New SqlParameter("@REMARKS", SqlDbType.VarChar, 300))
                    .Parameters("@REMARKS").Value = strRemarks


                    .Parameters.Add(New SqlParameter("@RETUNID", SqlDbType.Int))
                    .Parameters("@RETUNID").Direction = ParameterDirection.Output

                    .Parameters("@RETUNID").Value = ""

                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED
                    .Connection.Open()

                    intRecordsAffected = .ExecuteNonQuery()
                    intRetId = .Parameters("@RETUNID").Value
                    If UCase(strAction) = "U" Then
                        If intRetId = 0 Then Throw (New AAMSException("Unable to update!"))
                    End If
                    objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                End With
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                Return objUpdateDocOutput
            Catch Exec As Exception
                If intRetId = 0 Then
                    bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                    bzShared.FillErrorStatus(objUpdateDocOutput, "101", "Unable to update!")
                    Return objUpdateDocOutput
                Else
                    bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                    bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                    Return objUpdateDocOutput
                End If

            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try

            Return objUpdateDocOutput
        End Function
        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.View
            'Purpose:This function gives details of City.
            'Input  :
            '<TA_VIEWOFFICEID_INPUT>
            '	<OFFICEID></OFFICEID>
            '</TA_VIEWOFFICEID_INPUT>

            'Output :
            '<MS_VIEWOFFICEID_OUTPUT>
            '	<OFFICEID OFFICEID='' NAME='' CID='' PROCESSING_DATE='' TERMINALID='' REMARKS='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_VIEWOFFICEID_OUTPUT>


            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strOfficeID As String
            Const strMETHOD_NAME As String = "View"

            Try
                objOutputXml.LoadXml(srtVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strOfficeID = IndexDoc.DocumentElement.SelectSingleNode("OFFICEID").InnerText.Trim
                If strOfficeID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_OFFICEID"
                    .Connection = objSqlConnection

                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"

                    .Parameters.Add("@OFFICEID", SqlDbType.VarChar, 9)
                    .Parameters("@OFFICEID").Value = strOfficeID


                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    Dim blnRecordFound As Boolean
                    If blnRecordFound = False Then
                        With objOutputXml.DocumentElement.SelectSingleNode("OFFICEID")
                            .Attributes("OFFICEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OFFICEID")))
                            .Attributes("NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NAME")) & "")
                            .Attributes("CID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CID")) & "")
                            .Attributes("PROCESSING_DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PROCESSING_DATE")) & "")
                            .Attributes("TERMINALID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("TERMINALID")) & "")
                            .Attributes("REMARKS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("REMARKS")) & "")
                        End With
                    End If
                    blnRecordFound = True
                Loop
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objOutputXml, "103", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Return objOutputXml
        End Function
        Public Function ListOfficeType() As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: List out OfficeType
            'Input  : 
            'Output :  
            '<TA_LISTOFFICETYPE_OUTPUT>
            '	<OFFICETYPE OFFICEID_TYPE_ID='' OFFICEID_TYPE_NAME='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</TA_LISTOFFICETYPE_OUTPUT>

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "ListOfficeType"

            objOutputXml.LoadXml(strListOfficeType_OUTPUT)

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LST_TA_OFFICETYPE"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("OFFICETYPE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)

                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("OFFICEID_TYPE_ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OFFICEID_TYPE_ID")))
                    objAptNodeClone.Attributes("OFFICEID_TYPE_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OFFICEID_TYPE_NAME")))
                    objAptNodeClone.Attributes("TYPE_TRAVEL_AGENCY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("TYPE_TRAVEL_AGENCY")))
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                Loop

                If (blnRecordFound = False) Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                End If

            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch exec As Exception
                'msgbox(exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function
        Public Function GenerateOfficeID(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            'Purpose : This function generates OfficeID
            'Input:
            '<TA_GENERATEOFFICEID_INPUT>
            '	<GENERATEOFFICEID OFFICEID='' CityCode='' CorporateCode='' CorporateQualifier='' OFFICEID_TYPE_ID='' />
            '</TA_GENERATEOFFICEID_INPUT>

            'Output:
            '<TA_GENERATEOFFICEID_OUTPUT>
            '	<GENERATEOFFICEID OFFICEID='' CityCode='' CorporateCode='' CorporateQualifier='' OFFICEID_TYPE_ID='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</TA_GENERATEOFFICEID_OUTPUT>


            Dim strRetId As String
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objUpdateDocOutput As New XmlDocument

            Dim strOfficeID As String, strCorporateCode As String, strCityCode As String
            Dim strCorporateQualifier As String, intOFFICEID_TYPE_ID As Integer, blnTravelAgency As Boolean

            Dim objTran As SqlTransaction
            Dim intRecordsAffected As Int32
            Const strMETHOD_NAME As String = "GenerateOfficeID"
            Try
                objUpdateDocOutput.LoadXml(strGenOfficeID_OUTPUT)

                With objUpdateDocOutput.DocumentElement.SelectSingleNode("GENERATEOFFICEID")
                    .Attributes("OFFICEID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GENERATEOFFICEID").Attributes("OFFICEID").InnerText
                    .Attributes("CityCode").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GENERATEOFFICEID").Attributes("CityCode").InnerText
                    .Attributes("CorporateCode").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GENERATEOFFICEID").Attributes("CorporateCode").InnerText
                    .Attributes("CorporateQualifier").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GENERATEOFFICEID").Attributes("CorporateQualifier").InnerText
                    .Attributes("OFFICEID_TYPE_ID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GENERATEOFFICEID").Attributes("OFFICEID_TYPE_ID").InnerText
                    '.Attributes("TYPE_TRAVEL_AGENCY").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GENERATEOFFICEID").Attributes("TYPE_TRAVEL_AGENCY").InnerText
                End With

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("GENERATEOFFICEID")
                    If ((.Attributes("OFFICEID").InnerText).Trim).ToString <> "" Then
                        strOfficeID = .Attributes("OFFICEID").InnerText.Trim()
                    End If

                    If ((.Attributes("CityCode").InnerText).Trim).ToString <> "" Then
                        strCityCode = .Attributes("CityCode").InnerText.Trim()
                    End If

                    If .Attributes("CorporateCode").InnerText.Trim() <> "" Then
                        strCorporateCode = .Attributes("CorporateCode").InnerText.Trim()
                    End If

                    If .Attributes("CorporateQualifier").InnerText.Trim() <> "" Then
                        strCorporateQualifier = .Attributes("CorporateQualifier").InnerText.Trim()
                    End If

                    If .Attributes("OFFICEID_TYPE_ID").InnerText.Trim() <> "" Then
                        intOFFICEID_TYPE_ID = .Attributes("OFFICEID_TYPE_ID").InnerText.Trim()
                    End If
                    
                    If strCityCode = "" Then Throw (New AAMSException("City Code can't be blank."))
                    If strCorporateCode = "" Then Throw (New AAMSException("Corporate Code can't be blank."))
                    If strCorporateQualifier = "" Then Throw (New AAMSException("Corporate Qualifier Code can't be blank."))
                    If intOFFICEID_TYPE_ID = 0 Then Throw (New AAMSException("Office Type can't be blank."))
                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_GET_TA_OFFICEID"

                    .Parameters.Add(New SqlParameter("@CITYCODE", SqlDbType.Char, 3))
                    .Parameters("@CITYCODE").Value = strCityCode

                    .Parameters.Add(New SqlParameter("@CORPORATECODE", SqlDbType.Char, 2))
                    .Parameters("@CORPORATECODE").Value = strCorporateCode

                    .Parameters.Add(New SqlParameter("@CORPORATEQUALIFIER", SqlDbType.VarChar, 1))
                    .Parameters("@CORPORATEQUALIFIER").Value = strCorporateQualifier

                    .Parameters.Add(New SqlParameter("@OFFICEID_TYPE_ID", SqlDbType.Int))
                    .Parameters("@OFFICEID_TYPE_ID").Value = intOFFICEID_TYPE_ID

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.VarChar, 9))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = ""

                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED
                    .Connection.Open()
                    objTran = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)
                    .Transaction = objTran
                    intRecordsAffected = .ExecuteNonQuery()
                    strRetId = .Parameters("@RETURNID").Value
                    If strRetId = "-1" Then Throw (New AAMSException("Unable to Generate OfficeID!"))
                    With objUpdateDocOutput.DocumentElement.SelectSingleNode("GENERATEOFFICEID")
                        .Attributes("OFFICEID").InnerText = strRetId
                    End With
                    objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    objTran.Commit()
                End With
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                If Not objTran Is Nothing Then
                    objTran.Rollback()
                End If
                bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                Return objUpdateDocOutput
            Catch Exec As Exception
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                Return objUpdateDocOutput
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objUpdateDocOutput
        End Function
        Public Function GetAgencyCorporateCode(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            'Purpose:This function gives search results based on choosen search criterion.
            'Input  :- 
            '<TA_AGENCYCORPORATECODE_INPUT>
            '	<LCode></LCode>
            '</TA_AGENCYCORPORATECODE_INPUT>

            'Output :
            '<TA_AGENCYCORPORATECODE_OUTPUT>
            '	<AGENCYCORPORATECODE CCode=''/>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</TA_AGENCYCORPORATECODE_OUTPUT>

            ''************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean


            Dim strLCode As String
            Const strMETHOD_NAME As String = "GetAgencyCorporateCode"
            objOutputXml.LoadXml(strGetAgencyCCode_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                If SearchDoc.DocumentElement.SelectSingleNode("LCode").InnerText.Trim() <> "" Then
                    strLCode = SearchDoc.DocumentElement.SelectSingleNode("LCode").InnerText.Trim()
                End If
                '--------Retrieving & Checking Details from Input XMLDocument ------End
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_GET_TA_AGNEYOFFICEID]"
                    .Connection = objSqlConnection

                    .Parameters.Add("@LCode", SqlDbType.VarChar, 10)
                    .Parameters("@LCode").Value = strLCode

                End With

                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AGENCYCORPORATECODE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("CCode").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CCode")) & "")
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                Loop

                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
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
        Public Function rptOfficeIDSummary(ByVal SearchDoc As System.Xml.XmlDocument)
            'Purpose:This function gives search results based on choosen search criterion.
            'Input  :- 
            '<TA_RPT_SUMMARYOFFICEID_INPUT>
            '	<CityCode></CityCode>
            '	<ALLOCATED></ALLOCATED>
            '   <SUMMARYTYPE></SUMMARYTYPE>
            '</TA_RPT_SUMMARYOFFICEID_INPUT>

            '<TA_RPT_SUMMARYOFFICEID_OUTPUT>
            '	<SUMMARY CityCode='' COUNT='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</TA_RPT_SUMMARYOFFICEID_OUTPUT>
            ''************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strOfficeID As String, strCityCode As String
            Dim blnSummaryType As Boolean
            Const strMETHOD_NAME As String = "rptOfficeIDSummary"
            objOutputXml.LoadXml(rpt_Summaryofficeid)
            Try
                If SearchDoc.DocumentElement.SelectSingleNode("CityCode").InnerText.Trim() <> "" Then
                    strCityCode = SearchDoc.DocumentElement.SelectSingleNode("CityCode").InnerText.Trim()
                End If


                If SearchDoc.DocumentElement.SelectSingleNode("SUMMARYTYPE").InnerText.Trim.ToUpper = "1" Then
                    blnSummaryType = True
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("SUMMARYTYPE").InnerText.Trim.ToUpper = "0" Then
                    blnSummaryType = False
                End If
                
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SER_TA_OFFICEID"
                    .Connection = objSqlConnection

                    .Parameters.Add("@CITYCODE", SqlDbType.VarChar, 3)
                    .Parameters("@CITYCODE").Value = strCityCode

                    .Parameters.Add("@ALLOCATED", SqlDbType.Bit, 1)
                    If SearchDoc.DocumentElement.SelectSingleNode("SUMMARYTYPE").InnerText.Trim.ToUpper = "2" Then
                        .Parameters("@ALLOCATED").Value = DBNull.Value
                    Else
                        If blnSummaryType = True Then
                            .Parameters("@ALLOCATED").Value = 1
                        Else
                            .Parameters("@ALLOCATED").Value = 0
                        End If
                    End If



                    .Parameters.Add("@CNT", SqlDbType.Bit, 1)
                    .Parameters("@CNT").Value = 1

                End With
                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("SUMMARY")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("CityCode").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CityCode")))
                    objAptNodeClone.Attributes("COUNT").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("COUNT")))
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)

                Loop
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
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function
    End Class
End Namespace

