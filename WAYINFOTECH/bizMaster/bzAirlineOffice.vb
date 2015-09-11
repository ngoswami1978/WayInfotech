'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Tapan $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizMaster/bzAirlineOffice.vb $
'$Workfile: bzAirlineOffice.vb $
'$Revision: 11 $
'$Archive: /AAMS/Components/bizMaster/bzAirlineOffice.vb $
'$Modtime: 17/07/08 6:28p $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizMaster
    Public Class bzAirlineOffice
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzAirlineOffice"

        Const strVIEW_INPUT = "<MS_VIEWAIRLINEOFFICE_INPUT><AR_OF_ID></AR_OF_ID></MS_VIEWAIRLINEOFFICE_INPUT>"
        Const srtVIEW_OUTPUT = "<MS_VIEWAIRLINEOFFICE_OUTPUT><AIRLINE_OFFICE AR_OF_ID='' Airline_Code='' AR_OF_Address='' Aoffice='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWAIRLINEOFFICE_OUTPUT>"

        Const strSEARCH_INPUT = "<MS_SEARCHAIRLINEOFFICE_INPUT><Airline_Code></Airline_Code><Airline_Name></Airline_Name><Aoffice></Aoffice></MS_SEARCHAIRLINEOFFICE_INPUT>"
        Const strSEARCH_OUTPUT = "<MS_SEARCHAIRLINEOFFICE_OUTPUT><AIRLINE_OFFICE AR_OF_ID='' Airline_Code='' Airline_Name=''  AR_OF_Address='' Aoffice='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''>	<Error Code='' Description='' /></Errors></MS_SEARCHAIRLINEOFFICE_OUTPUT>"

        Const StrADDAIRLINE_OFF_OUTPUT = "<MS_UPDATEAIRLINEOFFICE_INPUT><AIRLINE_OFFICE Action='' AR_OF_ID='' Airline_Code='' AR_OF_Address='' Aoffice='' /></MS_UPDATEAIRLINEOFFICE_INPUT>"

        Const strDELETE_INPUT = "<MS_DELETEAIRLINEOFFICE_INPUT><AR_OF_ID></AR_OF_ID></MS_DELETEAIRLINEOFFICE_INPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETEAIRLINEOFFICE_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETEAIRLINEOFFICE_OUTPUT>"

        Const strUPDATE_INPUT = "<MS_UPDATEAIRLINEOFFICE_INPUT><AIRLINE_OFFICE Action='' AR_OF_ID='' Airline_Code='' AR_OF_Address='' Aoffice='' /></MS_UPDATEAIRLINEOFFICE_INPUT>"
        Const strUPDATE_OUTPUT = "<MS_UPDATEAIRLINEOFFICE_OUTPUT><AIRLINE_OFFICE Action='' AR_OF_ID='' Airline_Code='' AR_OF_Address='' Aoffice='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEAIRLINEOFFICE_OUTPUT>"

        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add
            'Declare Output XML String
            '***********************************************************************
            'Purpose: 
            'Input:None
            'Output:Xml Document
            '<MS_UPDATEAIRLINEOFFICE_INPUT>
            '      <AIRLINE_OFFICE Action='' AR_OF_ID='' Airline_Code='' AR_OF_Address='' Aoffice='' />
            '</MS_UPDATEAIRLINEOFFICE_INPUT>
            '**************************************************************************
            Dim strMETHOD_NAME As String = "Add"
            Dim objXMLDoc As New XmlDocument
            Try
                objXMLDoc.LoadXml(StrADDAIRLINE_OFF_OUTPUT)
            Catch Exec As Exception
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objXMLDoc, "101", Exec.Message)
                Return objXMLDoc
            End Try
            Return objXMLDoc

        End Function

        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Delete


            '***********************************************************************
            'Purpose:This function deletes a AirlineOffice.
            'Input:XmlDocument
            '<MS_DELETEAIRLINEOFFICE_INPUT>
            '   <AR_OF_ID></AR_OF_ID>
            '</MS_DELETEAIRLINEOFFICE_INPUT>

            'Output :
            '<MS_DELETEAIRLINEOFFICE_OUTPUT>
            '       <Errors Status=''>
            '           <Error Code='' Description='' />
            '       </Errors>
            '</MS_DELETEAIRLINEOFFICE_OUTPUT>"            
            '************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Delete"
            Dim strAIRLINEOFF_ID As String

            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0
                strAIRLINEOFF_ID = DeleteDoc.DocumentElement.SelectSingleNode("AR_OF_ID").InnerText.Trim
                If strAIRLINEOFF_ID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_AIRLINE_OFFICES"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"

                    .Parameters.Add(New SqlParameter("@AR_OF_ID", SqlDbType.Int))
                    .Parameters("@AR_OF_ID").Value = strAIRLINEOFF_ID

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

            '<MS_SEARCHAIRLINEOFFICE_INPUT>
            '       <Airline_Code></Airline_Code>
            '       <Airline_Name></Airline_Name>
            '       <Aoffice></Aoffice>
            '</MS_SEARCHAIRLINEOFFICE_INPUT>

            'Output :
            '<MS_SEARCHAIRLINEOFFICE_OUTPUT>
            '       <AIRLINE_OFFICE AR_OF_ID='' Airline_Code='' Airline_Name=''  AR_OF_Address='' Aoffice='' />
            '       <Errors Status=''>	
            '           <Error Code='' Description='' />
            '       </Errors>
            '</MS_SEARCHAIRLINEOFFICE_OUTPUT>
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strAIRLINE_CODE As String = String.Empty
            Dim strAIRLINE_NAME As String = String.Empty
            Dim strAIRLINE_AOFFICE As String = String.Empty

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strAIRLINE_CODE = SearchDoc.DocumentElement.SelectSingleNode("Airline_Code").InnerText.Trim()
                strAIRLINE_NAME = SearchDoc.DocumentElement.SelectSingleNode("Airline_Name").InnerText.Trim()
                strAIRLINE_AOFFICE = SearchDoc.DocumentElement.SelectSingleNode("Aoffice").InnerText.Trim()

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
                    .CommandText = "UP_SER_MS_AIRLINE_OFFICES"
                    .Connection = objSqlConnection

                    .Parameters.Add("@AIRLINE_CODE", SqlDbType.VarChar, 2)
                    .Parameters("@AIRLINE_CODE").Value = strAIRLINE_CODE

                    .Parameters.Add("@NAME", SqlDbType.VarChar, 40)
                    .Parameters("@NAME").Value = strAIRLINE_NAME

                    .Parameters.Add("@AOFFICE", SqlDbType.VarChar, 3)
                    .Parameters("@AOFFICE").Value = strAIRLINE_AOFFICE


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

                Do While objSqlReader.Read()
                    If blnRecordFound = False Then
                        With objOutputXml.DocumentElement.SelectSingleNode("AIRLINE_OFFICE")
                            .Attributes("AR_OF_ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AR_OF_ID")))
                            .Attributes("Airline_Code").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Airline_Code")))
                            .Attributes("Airline_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NAME")))
                            .Attributes("AR_OF_Address").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AR_OF_Address")))
                            .Attributes("Aoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Aoffice")))
                        End With
                        blnRecordFound = True
                    Else
                        objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AIRLINE_OFFICE")
                        objAptNodeClone = objAptNode.CloneNode(True)
                        objAptNodeClone.Attributes("AR_OF_ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AR_OF_ID")))
                        objAptNodeClone.Attributes("Airline_Code").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Airline_Code")))
                        objAptNodeClone.Attributes("AR_OF_Address").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AR_OF_Address")))
                        objAptNodeClone.Attributes("Airline_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NAME")))
                        objAptNodeClone.Attributes("Aoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Aoffice")))

                        objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    End If
                Loop
                objSqlReader.Close()
                If blnRecordFound = False Then
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "TRUE"
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

            '***********************************************************************
            'Purpose:This function Inserts/Updates Airline Office.
            'Input  :

            '<MS_UPDATEAIRLINEOFFICE_INPUT>
            '       <AIRLINE_OFFICE Action='' AR_OF_ID='' Airline_Code='' AR_OF_Address='' Aoffice='' />
            '</MS_UPDATEAIRLINEOFFICE_INPUT>


            'Output :
            '<MS_UPDATEAIRLINEOFFICE_OUTPUT>
            '       <AIRLINE_OFFICE Action='' AR_OF_ID='' Airline_Code='' AR_OF_Address='' Aoffice='' />
            '       <Errors Status=''>
            '           <Error Code='' Description='' />
            '       </Errors>
            '</MS_UPDATEAIRLINEOFFICE_OUTPUT>

            '************************************************************************
            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objUpdateDocOutput As New XmlDocument

            Dim intAIRLINEOFFICE_ID As Int32
            Dim strAIRLINE_CODE As String = String.Empty
            Dim strAIRLINEOFFICE_ADDRESS As String = String.Empty
            Dim strAIRLINE_AOFFICE As String = String.Empty



            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)

                With objUpdateDocOutput.DocumentElement.SelectSingleNode("AIRLINE_OFFICE")
                    .Attributes("Action").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AIRLINE_OFFICE").Attributes("Action").InnerText
                    .Attributes("AR_OF_ID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AIRLINE_OFFICE").Attributes("AR_OF_ID").InnerText
                    .Attributes("Airline_Code").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AIRLINE_OFFICE").Attributes("Airline_Code").InnerText
                    .Attributes("AR_OF_Address").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AIRLINE_OFFICE").Attributes("AR_OF_Address").InnerText
                    .Attributes("Aoffice").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AIRLINE_OFFICE").Attributes("Aoffice").InnerText

                End With

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("AIRLINE_OFFICE")
                    strAction = ((.Attributes("Action").InnerText).Trim).ToString
                    intAIRLINEOFFICE_ID = Val(((.Attributes("AR_OF_ID").InnerText).Trim).ToString & "")
                    strAIRLINE_CODE = ((.Attributes("Airline_Code").InnerText).Trim).ToString
                    strAIRLINEOFFICE_ADDRESS = ((.Attributes("AR_OF_Address").InnerText).Trim).ToString
                    strAIRLINE_AOFFICE = ((.Attributes("Aoffice").InnerText).Trim).ToString

                    If strAction = "I" Or strAction = "U" Then
                        If strAIRLINE_CODE = "" Then
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
                    .CommandText = "UP_SRO_MS_AIRLINE_OFFICES"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction

                    .Parameters.Add(New SqlParameter("@AR_OF_ID", SqlDbType.Int))
                    .Parameters("@AR_OF_ID").Value = intAIRLINEOFFICE_ID

                    .Parameters.Add(New SqlParameter("@Airline_Code", SqlDbType.VarChar, 2))
                    .Parameters("@Airline_Code").Value = strAIRLINE_CODE

                    .Parameters.Add(New SqlParameter("@AR_OF_Address", SqlDbType.VarChar, 255))
                    .Parameters("@AR_OF_Address").Value = strAIRLINEOFFICE_ADDRESS

                    .Parameters.Add(New SqlParameter("@Aoffice", SqlDbType.VarChar, 3))
                    .Parameters("@Aoffice").Value = strAIRLINE_AOFFICE

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output

                    .Parameters("@RETURNID").Value = 0

                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED
                    .Connection.Open()

                    intRecordsAffected = .ExecuteNonQuery()

                    If UCase(strAction) = "I" Then
                        intRetId = .Parameters("@RETURNID").Value
                        If intRetId = 0 Then
                            Throw (New AAMSException("Airline Aoffice Address Already Exists!"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("AIRLINE_OFFICE").Attributes("AR_OF_ID").InnerText = intRetId
                        End If
                    ElseIf UCase(strAction) = "U" Then
                        intRetId = .Parameters("@RETURNID").Value
                        If intRetId = 0 Then
                            Throw (New AAMSException("Airline Aoffice Address Not Updated!"))
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
            '<MS_VIEWAIRLINEOFFICE_INPUT>
            '       <AR_OF_ID></AR_OF_ID>
            '</MS_VIEWAIRLINEOFFICE_INPUT>

            'Output :
            '<MS_VIEWAIRLINEOFFICE_OUTPUT>
            '       <AIRLINE_OFFICE AR_OF_ID='' Airline_Code='' AR_OF_Address='' Aoffice='' />
            '       <Errors Status=''>
            '           <Error Code='' Description='' />
            '       </Errors>
            '</MS_VIEWAIRLINEOFFICE_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strAIRLINEOFFICE_ID As String
            Const strMETHOD_NAME As String = "View"

            Try
                objOutputXml.LoadXml(srtVIEW_OUTPUT)

                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strAIRLINEOFFICE_ID = IndexDoc.DocumentElement.SelectSingleNode("AR_OF_ID").InnerText.Trim
                If strAIRLINEOFFICE_ID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_AIRLINE_OFFICES"
                    .Connection = objSqlConnection
                    .Parameters.Add("AR_OF_ID", SqlDbType.Int)
                    .Parameters("AR_OF_ID").Value = strAIRLINEOFFICE_ID

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
                        With objOutputXml.DocumentElement.SelectSingleNode("AIRLINE_OFFICE")
                            .Attributes("AR_OF_ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AR_OF_ID")))
                            .Attributes("Airline_Code").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Airline_Code")))
                            .Attributes("AR_OF_Address").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AR_OF_Address")))
                            .Attributes("Aoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Aoffice")))

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
    End Class
End Namespace