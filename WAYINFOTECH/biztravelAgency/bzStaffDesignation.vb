'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Tapan $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizTravelAgency/bzStaffDesignation.vb $
'$Workfile: bzStaffDesignation.vb $
'$Revision: 2 $
'$Archive: /AAMS/Components/bizTravelAgency/bzStaffDesignation.vb $
'$Modtime: 7/09/11 11:47a $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizTravelAgency
    Public Class bzStaffDesignation
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzDesignation"
        Const gstrList_OUTPUT = "<MS_LISTDESIGNATION_OUTPUT><Designation DesignationID='' Designation='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTDESIGNATION_OUTPUT>"

        Const strVIEW_INPUT = "<MS_VIEWDESIGNATION_INPUT><DesignationID></DesignationID></MS_VIEWDESIGNATION_INPUT>"
        Const srtVIEW_OUTPUT = "<MS_VIEWDESIGNATION_OUTPUT><DESIGNATION DesignationID='' Designation=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWDESIGNATION_OUTPUT>"

        Const strSEARCH_INPUT = "<MS_SEARCHDESIGNATION_INPUT><Designation></Designation></MS_SEARCHDESIGNATION_INPUT>"
        Const strSEARCH_OUTPUT = "<MS_SEARCHDESIGNATION_OUTPUT><DESIGNATION DesignationID='' Designation=''/><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHDESIGNATION_OUTPUT>"


        Const StrADDDESIGNATION_OUTPUT = "<MS_UPDATEDESIGNATION_INPUT><DESIGNATION DesignationID='' Designation=''><SECURITY SecurityOptionID='' Value='' /></DESIGNATION></MS_UPDATEDESIGNATION_INPUT>"

        Const strDELETE_INPUT = "<MS_DELETEDESIGNATION_INPUT><DesignationID></DesignationID></MS_DELETEDESIGNATION_INPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETEDESIGNATION_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETEDESIGNATION_OUTPUT>"

        Const strUPDATE_INPUT = "<MS_UPDATEDESIGNATION_INPUT><DESIGNATION DesignationID='' Designation=''></DESIGNATION></MS_UPDATEDESIGNATION_INPUT>"
        Const strUPDATE_OUTPUT = "<MS_UPDATEDESIGNATION_OUTPUT><DESIGNATION DesignationID='' Designation=''></DESIGNATION><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEDESIGNATION_OUTPUT>"

        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add
            'Declare Output XML String
            '***********************************************************************
            'Purpose: 
            'Input:None
            'Output:Xml Document

            '<MS_UPDATEDESIGNATION_INPUT>
            '   <DESIGNATION DesignationID='' Designation=''>
            '   <SECURITY SecurityOptionID='' Value='' />
            '   </DESIGNATION>
            '</MS_UPDATEDESIGNATION_INPUT>
            '**************************************************************************

            Dim strMETHOD_NAME As String = "Add"
            Dim objXMLDoc As New XmlDocument
            Try
                objXMLDoc.LoadXml(StrADDDESIGNATION_OUTPUT)
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

            '<MS_DELETEDESIGNATION_INPUT>
            '   <DesignationID>
            '   </DesignationID>
            '</MS_DELETEDESIGNATION_INPUT>

            'Output :
            '<MS_DELETEDESIGNATION_OUTPUT>
            '   <Errors Status=''>
            '       <Error Code='' Description='' />
            '   </Errors>
            '</MS_DELETEDESIGNATION_OUTPUT>

            ''************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objTransaction As SqlTransaction
            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Delete"
            Dim strDESIGNATION_Code As String

            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0
                strDESIGNATION_Code = DeleteDoc.DocumentElement.SelectSingleNode("DesignationID").InnerText.Trim()
                If strDESIGNATION_Code = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_STAFF_DESIGNATION"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"

                    .Parameters.Add(New SqlParameter("@DESIGNATION_ID", SqlDbType.Int))
                    .Parameters("@DESIGNATION_ID").Value = strDESIGNATION_Code

                    objSqlCommand.Connection.Open()
                    objTransaction = objSqlConnection.BeginTransaction
                    .Transaction = objTransaction
                    Try
                        intRecordsAffected = .ExecuteNonQuery()
                        objTransaction.Commit()
                    Catch ex As Exception
                        intRecordsAffected = 0
                        objTransaction.Rollback()
                    End Try
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
                Return objDeleteDocOutput
            Catch Exec As Exception

                'CATCHING OTHER EXCEPTIONS '**********Fill Error Status
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objDeleteDocOutput, "101", Exec.Message)
                Return objDeleteDocOutput
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
            '<MS_SEARCHDESIGNATION_INPUT>
            '   <Designation></Designation>
            '</MS_SEARCHDESIGNATION_INPUT>


            'Output :
            '<MS_SEARCHDESIGNATION_OUTPUT>
            '   <Designation DesignationID='' Designation=''/>
            '<Errors Status=''>
            '   <Error Code='' Description='' />
            '</Errors>
            '<MS_SEARCHDESIGNATION_INPUT>
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strDESIGNATION_NAME As String = String.Empty

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)

            Try

                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strDESIGNATION_NAME = SearchDoc.DocumentElement.SelectSingleNode("Designation").InnerText.Trim()
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
                    .CommandText = "UP_SER_MS_STAFF_DESIGNATION"
                    .Connection = objSqlConnection

                    .Parameters.Add("@DESIGNATION_NAME", SqlDbType.VarChar, 100)
                    .Parameters("@DESIGNATION_NAME").Value = strDESIGNATION_NAME

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

                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("DESIGNATION")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()

                    blnRecordFound = True
                    objAptNodeClone.Attributes("DesignationID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DesignationID")))
                    objAptNodeClone.Attributes("Designation").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Designation")))
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                Loop
                If objSqlReader.HasRows Then
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "TRUE"
                    Call bzShared.FillErrorStatus(objOutputXml, "101", "Data not found")
                End If
                objSqlReader.Close()
                If blnRecordFound = True Then
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
            '<MS_UPDATEDESIGNATION_INPUT>
            '   <DESIGNATION DesignationID='' Designation=''>
            '   </DESIGNATION>
            '</MS_UPDATEDESIGNATION_INPUT>

            'Output :
            '<MS_UPDATEDESIGNATION_OUTPUT>
            '   <DESIGNATION DesignationID='' Designation=''>
            '   </DESIGNATION>
            '<Errors Status=''>
            '   <Error Code='' Description='' />
            '</Errors>
            '</MS_UPDATEDESIGNATION_OUTPUT>
            '************************************************************************

            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objUpdateDocOutput As New XmlDocument
            Dim intRecordsAffected As Int32
            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)
                With objUpdateDocOutput.DocumentElement.SelectSingleNode("DESIGNATION")
                    .Attributes("DesignationID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DESIGNATION").Attributes("DesignationID").InnerText
                    .Attributes("Designation").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DESIGNATION").Attributes("Designation").InnerText

                End With
                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("DESIGNATION")
                    If ((.Attributes("DesignationID").InnerText).Trim).ToString = "" Then
                        strAction = "I"
                    Else
                        strAction = "U"
                    End If
                    If strAction = "I" Then
                        If ((.Attributes("Designation").InnerText).Trim).ToString = "" Then
                            Throw (New AAMSException("Designation can't be blank."))
                        End If
                    ElseIf strAction = "U" Then
                        If ((.Attributes("Designation").InnerText).Trim).ToString = "" Then
                            Throw (New AAMSException("Designation can't be blank."))
                        End If
                        If ((.Attributes("DesignationID").InnerText).Trim).ToString = "" Then
                            Throw (New AAMSException("Designation can't be blank."))
                        End If
                    Else
                        Throw (New AAMSException("Invalid Action Code."))
                    End If
                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_STAFF_DESIGNATION"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction

                    .Parameters.Add(New SqlParameter("@DESIGNATION_ID", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DESIGNATION").Attributes("DesignationID").InnerText = "" Then
                        .Parameters("@DESIGNATION_ID").Value = DBNull.Value
                    Else
                        .Parameters("@DESIGNATION_ID").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("DESIGNATION").Attributes("DesignationID").InnerText)
                    End If

                    .Parameters.Add(New SqlParameter("@DESIGNATION_NAME", SqlDbType.VarChar, 1000))
                    .Parameters("@DESIGNATION_NAME").Value = UpdateDoc.DocumentElement.SelectSingleNode("DESIGNATION").Attributes("Designation").InnerText.Trim


                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0

                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED
                    .Connection.Open()
                    intRecordsAffected = .ExecuteNonQuery()
                    If UCase(strAction) = "I" Then
                        intRetId = .Parameters("@RETURNID").Value
                        If intRetId = 0 Then
                            Throw (New AAMSException("Designation Already Exists!"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("DESIGNATION").Attributes("DesignationID").InnerText = intRetId
                        End If
                    ElseIf UCase(strAction) = "U" Then
                        intRetId = .Parameters("@RETURNID").Value
                        If intRetId <= 0 Then
                            Throw (New AAMSException("Unable to update!"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                        End If
                    End If

                End With
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
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
            '<MS_VIEWDESIGNATION_INPUT>
            '   <DesignationID></DesignationID>
            '</MS_VIEWDESIGNATION_INPUT>


            'Output :
            '<MS_VIEWDESIGNATION_OUTPUT>
            '   <DESIGNATION DesignationID='' Designation=''>
            '   </DESIGNATION>
            '<Errors Status=''>
            '   <Error Code='' Description='' />
            '</Errors>
            '</MS_VIEWDESIGNATION_OUTPUT>
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Const strMETHOD_NAME As String = "View"
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(srtVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                If IndexDoc.DocumentElement.SelectSingleNode("DesignationID").InnerText.Trim = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_STAFF_DESIGNATION"
                    .Connection = objSqlConnection

                    .Parameters.Add("@DESIGNATION_ID", SqlDbType.Int)
                    .Parameters("@DESIGNATION_ID").Value = IndexDoc.DocumentElement.SelectSingleNode("DesignationID").InnerText.Trim

                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("DESIGNATION")
                        .Attributes("DesignationID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DesignationID")) & "")
                        .Attributes("Designation").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Designation")) & "")
                    End With
                    blnRecordFound = True
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
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objOutputXml, "103", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml


        End Function
        Public Function List() As XmlDocument
            '***********************************************************************
            'Purpose: List out Designation
            'Input  : 
            'Output :  
            '<MS_LISTDESIGNATION_OUTPUT>
            '   <Designation DesignationID='' Designation='' />
            '<Errors Status=''>
            '   <Error Code='' Description='' />
            '</Errors>
            '</MS_LISTDESIGNATION_OUTPUT>

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "List"

            objOutputXml.LoadXml(gstrList_OUTPUT)

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LST_MS_STAFF_DESIGNATION"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("Designation")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)

                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("DesignationID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DesignationID")))
                    objAptNodeClone.Attributes("Designation").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Designation")))
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
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try

            Return objOutputXml
        End Function
    End Class
End Namespace


