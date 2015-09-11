'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Tapan $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizMaster/bzAirline.vb $
'$Workfile: bzAirline.vb $
'$Revision: 17 $
'$Archive: /AAMS/Components/bizMaster/bzAirline.vb $
'$Modtime: 17/07/08 6:15p $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizMaster

    Public Class bzAirline
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzAirline"
        Const gstrList_OUTPUT = "<MS_LISTAIRLINE_OUTPUT><AIRLINE Airline_Code='' Name='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTAIRLINE_OUTPUT>"
        Const strVIEW_INPUT = "<MS_VIEWAIRLINE_INPUT><Airline_Code></Airline_Code></MS_VIEWAIRLINE_INPUT>"
        Const srtVIEW_OUTPUT = "<MS_VIEWAIRLINE_OUTPUT><AIRLINE Airline_Code='' Name='' Online_Carrier='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWAIRLINE_OUTPUT>"

        Const strSEARCH_INPUT = "<MS_SEARCHAIRLINE_INPUT><Airline_Code></Airline_Code><Name></Name><Online_Carrier></Online_Carrier></MS_SEARCHAIRLINE_INPUT>"
        Const strSEARCH_OUTPUT = "<MS_SEARCHAIRLINE_OUTPUT><AIRLINE Airline_Code='' Name='' Online_Carrier='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHAIRLINE_OUTPUT>"

        Const StrADDAIRLINE_OUTPUT = "<MS_UPDATEAIRLINE_INPUT><AIRLINE Action='' Airline_Code='' Name='' Online_Carrier='' /></MS_UPDATEAIRLINE_INPUT>"

        Const strDELETE_INPUT = "<MS_DELETEAIRLINE_INPUT><Airline_Code></Airline_Code></MS_DELETEAIRLINE_INPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETEAIRLINE_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETEAIRLINE_OUTPUT>"

        Const strUPDATE_INPUT = "<MS_UPDATEAIRLINE_INPUT><AIRLINE Action='' Airline_Code='' Name='' Online_Carrier='' /></MS_UPDATEAIRLINE_INPUT>"
        Const strUPDATE_OUTPUT = "<MS_UPDATEAIRLINE_OUTPUT><AIRLINE Action='' Airline_Code='' Name='' Online_Carrier='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEAIRLINE_OUTPUT>"

        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add

            'Declare Output XML String
            '***********************************************************************
            'Purpose: 
            'Input:None
            'Output:Xml Document
            '<MS_UPDATEAIRLINE_INPUT>
            '       <AIRLINE Action='' Airline_Code='' Name='' Online_Carrier='' />
            '</MS_UPDATEAIRLINE_INPUT>
            '**************************************************************************
            Dim strMETHOD_NAME As String = "Add"
            Dim objXMLDoc As New XmlDocument
            Try
                objXMLDoc.LoadXml(StrADDAIRLINE_OUTPUT)
            Catch Exec As Exception
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objXMLDoc, "101", Exec.Message)
                Return objXMLDoc
            End Try
            Return objXMLDoc
        End Function

        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Delete

            '***********************************************************************
            'Purpose:This function deletes a Airline.
            'Input:XmlDocument
            '<MS_DELETEAIRLINE_INPUT>
            '   <Airline_Code></Airline_Code>
            '</MS_DELETEAIRLINE_INPUT>

            'Output :
            '<MS_DELETEAIRLINE_OUTPUT>
            '   <Errors Status=''>
            '   <Error Code='' Description='' />
            '   </Errors>
            '</MS_DELETEAIRLINE_OUTPUT>
            '************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Delete"
            Dim strAIRLINE_Code As String

            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0
                strAIRLINE_Code = DeleteDoc.DocumentElement.SelectSingleNode("Airline_Code").InnerText.Trim()
                If strAIRLINE_Code = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_AIRLINE"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"

                    .Parameters.Add(New SqlParameter("@Airline_Code", SqlDbType.VarChar, 2))
                    .Parameters("@Airline_Code").Value = strAIRLINE_Code

                    objSqlCommand.Connection.Open()
                    intRecordsAffected = .ExecuteNonQuery()
                    objSqlCommand.Connection.Close()
                End With

                'Checking whether record is deleted successfully or not
                If intRecordsAffected > 0 Then
                    objDeleteDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    Return (objDeleteDocOutput)
                Else
                    objDeleteDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "TRUE"
                    Call bzShared.FillErrorStatus(objDeleteDocOutput, "101", "Record has not been deleted!")
                    Return (objDeleteDocOutput)
                End If
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objDeleteDocOutput, "101", Exec.Message)
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                'Fill Error Status
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objDeleteDocOutput, "101", Exec.Message)
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objDeleteDocOutput
        End Function

        Public Function Search(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Search
            '***********************************************************************
            'Purpose:This function gives search results based on choosen search criterion.
            'Input  :- 
            '<MS_SEARCHAIRLINE_INPUT>
            '   <Airline_Code></Airline_Code>
            '   <Name></Name>
            '   <Online_Carrier></Online_Carrier>
            '</MS_SEARCHAIRLINE_INPUT>

            'Output :
            '<MS_SEARCHAIRLINE_OUTPUT>
            '   <AIRLINE Airline_Code='' Name='' Online_Carrier='' />
            '   <Errors Status=''>
            '       <Error Code='' Description='' />
            '   </Errors>
            '</MS_SEARCHAIRLINE_OUTPUT>
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strAR_CODE As String = String.Empty
            Dim strAR_NAME As String = String.Empty
            Dim intAR_ONLINE_STATUS As Integer

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean


            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start

                strAR_CODE = SearchDoc.DocumentElement.SelectSingleNode("Airline_Code").InnerText.Trim()
                strAR_NAME = SearchDoc.DocumentElement.SelectSingleNode("Name").InnerText.Trim()
                intAR_ONLINE_STATUS = SearchDoc.DocumentElement.SelectSingleNode("Online_Carrier").InnerText.Trim()

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
                    .CommandText = "UP_SER_MS_AIRLINE"
                    .Connection = objSqlConnection

                    .Parameters.Add("@AIRLINE_CODE", SqlDbType.VarChar, 2)
                    .Parameters("@AIRLINE_CODE").Value = strAR_CODE

                    .Parameters.Add("@AIRLINENAME", SqlDbType.VarChar, 40)
                    .Parameters("@AIRLINENAME").Value = strAR_NAME

                    .Parameters.Add("@ONLINE_CARRIER", SqlDbType.Bit, 1)
                    .Parameters("@ONLINE_CARRIER").Value = intAR_ONLINE_STATUS


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
                If objSqlReader.HasRows = True Then
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "TRUE"
                    Call bzShared.FillErrorStatus(objOutputXml, "101", "Record not found!")
                End If

                Do While objSqlReader.Read()
                    If blnRecordFound = False Then
                        With objOutputXml.DocumentElement.SelectSingleNode("AIRLINE")
                            .Attributes("Airline_Code").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Airline_Code")))
                            .Attributes("Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NAME")) & "")
                            '.Attributes("Online_Carrier").InnerText = IIf(UCase(Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Online_Carrier")))) = "FALSE", 0, 1)
                            .Attributes("Online_Carrier").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Online_Carrier")))
                        End With
                        blnRecordFound = True
                    Else
                        objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AIRLINE")
                        objAptNodeClone = objAptNode.CloneNode(True)
                        objAptNodeClone.Attributes("Airline_Code").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Airline_Code")))
                        objAptNodeClone.Attributes("Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Name")) & "")
                        objAptNodeClone.Attributes("Online_Carrier").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Online_Carrier")))
                        objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    End If
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
            'Purpose:This function Inserts/Updates City.
            'Input  :
            '<MS_UPDATEAIRLINE_INPUT>
            '       <AIRLINE Action='' Airline_Code='' Name='' Online_Carrier='' />
            '</MS_UPDATEAIRLINE_INPUT> 

            'Output :
            '<MS_UPDATEAIRLINE_OUTPUT>
            '       <AIRLINE Action='' Airline_Code='' Name='' Online_Carrier='' />
            '       <Errors Status=''><Error Code='' Description='' /></Errors>
            '</MS_UPDATEAIRLINE_OUTPUT>
            '************************************************************************
            Dim intRetId As String = ""
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objUpdateDocOutput As New XmlDocument
            Dim strAR_CODE As String
            Dim strAR_NAME As String
            Dim intAR_ONLINE_STATUS As Integer
            Dim intRecordsAffected As Int32
            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)

                With objUpdateDocOutput.DocumentElement.SelectSingleNode("AIRLINE")
                    .Attributes("Action").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AIRLINE").Attributes("Action").InnerText
                    .Attributes("Airline_Code").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AIRLINE").Attributes("Airline_Code").InnerText
                    .Attributes("Name").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AIRLINE").Attributes("Name").InnerText
                    .Attributes("Online_Carrier").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AIRLINE").Attributes("Online_Carrier").InnerText
                End With

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("AIRLINE")
                    strAction = ((.Attributes("Action").InnerText).Trim).ToString
                    strAR_CODE = ((.Attributes("Airline_Code").InnerText).Trim).ToString
                    strAR_NAME = ((.Attributes("Name").InnerText).Trim).ToString
                    intAR_ONLINE_STATUS = ((.Attributes("Online_Carrier").InnerText).Trim).ToString


                    If strAction = "I" Or strAction = "U" Then
                        If strAR_CODE = "" Then
                            Throw (New AAMSException("Airline code can't be blank."))
                        End If

                        If strAR_NAME = "" Then
                            Throw (New AAMSException("Airline Name can't be blank."))
                        End If
                    Else
                        Throw (New AAMSException("Invalid Action Code."))
                    End If

                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_AIRLINE"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction

                    .Parameters.Add(New SqlParameter("@Airline_Code", SqlDbType.VarChar, 2))
                    .Parameters("@Airline_Code").Value = strAR_CODE

                    .Parameters.Add(New SqlParameter("@AIRLINENAME", SqlDbType.VarChar, 40))
                    .Parameters("@AIRLINENAME").Value = strAR_NAME

                    .Parameters.Add(New SqlParameter("@Online_Carrier", SqlDbType.Int))
                    .Parameters("@Online_Carrier").Value = intAR_ONLINE_STATUS
                    
                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output

                    .Parameters("@RETURNID").Value = 0

                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED
                    .Connection.Open()

                    intRecordsAffected = .ExecuteNonQuery()

                    If UCase(strAction) = "I" Then
                        intRetId = .Parameters("@RETURNID").Value

                        If intRetId = -1 Then
                            Throw (New AAMSException("Airline Code Already Exists!"))

                        ElseIf intRetId = 0 Then
                            Throw (New AAMSException("Airline Name Already Exists!"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("AIRLINE").Attributes("Airline_Code").InnerText = strAR_CODE
                        End If

                    ElseIf UCase(strAction) = "U" Then
                        intRetId = .Parameters("@RETURNID").Value
                        If intRetId = 0 Then
                            Throw (New AAMSException("Airline Code / Name Already Exists!"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                        End If
                    End If

                End With

            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
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

        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.View

            '***********************************************************************
            'Purpose: This function gives details of Airline.
            'Input  :
            '<MS_VIEWAIRLINE_INPUT>
            '   <Airline_Code></Airline_Code>
            '</MS_VIEWAIRLINE_INPUT>

            'Output :
            '<MS_VIEWAIRLINE_OUTPUT>
            '   <AIRLINE Airline_Code ='' Name='' Online_Carrier='' />
            '   <Errors Status=''><Error Code='' Description='' /></Errors>
            '</MS_VIEWAIRLINE_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strAIRLINE_CODE As String
            Const strMETHOD_NAME As String = "View"

            Try
                objOutputXml.LoadXml(srtVIEW_OUTPUT)

                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strAIRLINE_CODE = IndexDoc.DocumentElement.SelectSingleNode("Airline_Code").InnerText.Trim
                If strAIRLINE_CODE = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_AIRLINE"
                    .Connection = objSqlConnection
                    .Parameters.Add("@Airline_Code", SqlDbType.VarChar, 2)
                    .Parameters("@Airline_Code").Value = strAIRLINE_CODE
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"

                    .Parameters.Add("@RETURNID", SqlDbType.Int)
                    .Parameters("@RETURNID").Value = 0

                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    Dim blnRecordFound As Boolean
                    If blnRecordFound = False Then
                        With objOutputXml.DocumentElement.SelectSingleNode("AIRLINE")
                            .Attributes("Airline_Code").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Airline_Code")))
                            .Attributes("Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Name")))
                            .Attributes("Online_Carrier").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Online_Carrier")))
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

        Public Function List() As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the Airline record, based on the given field value
            'Input  : 
            'Output :  
            '<MS_LISTAIRLINE_OUTPUT>
            '   <AIRLINE Airline_Code='' Name='' />
            '   <Errors Status=''><Error Code='' Description='' /></Errors>
            '</MS_LISTAIRLINE_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strMETHOD_NAME As String = "List"

            objOutputXml.LoadXml(gstrList_OUTPUT)

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LST_MS_AIRLINE"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument

                Do While objSqlReader.Read
                    Dim blnRecordFound As Boolean
                    If blnRecordFound = False Then
                        With objOutputXml.DocumentElement.SelectSingleNode("AIRLINE")
                            .Attributes("Airline_Code").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AIRLINE_CODE")))
                            .Attributes("Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NAME")))
                            blnRecordFound = True
                        End With
                    Else
                        objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AIRLINE")
                        objAptNodeClone = objAptNode.CloneNode(True)
                        objAptNodeClone.Attributes("Airline_Code").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AIRLINE_CODE")))
                        objAptNodeClone.Attributes("Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Name")))
                        objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    End If
                Loop
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
            objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Return objOutputXml

        End Function

    End Class
End Namespace
