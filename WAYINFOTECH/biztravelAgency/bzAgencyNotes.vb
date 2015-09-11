'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Tapan $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizTravelAgency/bzAgencyNotes.vb $
'$Workfile: bzAgencyNotes.vb $
'$Revision: 8 $
'$Archive: /AAMS/Components/bizTravelAgency/bzAgencyNotes.vb $
'$Modtime: 6/08/08 4:45p $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizTravelAgency

    Public Class bzAgencyNotes
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bizTravelAgency"
        Const StrADD_OUTPUT = "<MS_UPDATEAGENCYNOTES_OUTPUT><AGENCYNOTES LCode='' EmployeeID='' Notes='' /></MS_UPDATEAGENCYNOTES_OUTPUT>"
        Const strUPDATE_INPUT = "<MS_UPDATEAGENCYNOTES_INPUT><AGENCYNOTES LCode='' EmployeeID='' Notes='' /></MS_UPDATEAGENCYNOTES_INPUT>"
        Const strUPDATE_OUTPUT = "<MS_UPDATEAGENCYNOTES_OUTPUT><AGENCYNOTES LCode='' EmployeeID='' Notes='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEAGENCYNOTES_OUTPUT>
        Const strGETDETAILS_INPUT = "<MS_GETAGENCYNOTES_INPUT><LCode></LCode></MS_GETAGENCYNOTES_INPUT>"

        Const strGETDETAILS_OUTPUT = "<MS_GETAGENCYNOTES_OUTPUT><AGENCYNOTES LCode='' EmployeeID='' EmployeeName='' DateTime='' Notes='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_GETAGENCYNOTES_OUTPUT>"


        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add
            'Declare Output XML String
            '***********************************************************************
            'Purpose: 
            'Input:None
            'Output:Xml Document
            '<MS_UPDATEAGENCYNOTES_OUTPUT><AGENCYNOTES LCode='' EmployeeID='' Notes='' /></MS_UPDATEAGENCYNOTES_OUTPUT>
            '**************************************************************************
            Dim strMETHOD_NAME As String = "Add"
            Dim objXMLDoc As New XmlDocument
            Try
                objXMLDoc.LoadXml(StrADD_OUTPUT)
            Catch Exec As Exception
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objXMLDoc, "101", Exec.Message)
                Return objXMLDoc
            End Try
            Return objXMLDoc
        End Function

        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Delete
            '---------Not to be implemented----------
        End Function

        Public Function Search(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Search
            '---------Not to be implemented----------
        End Function

        Public Function Update(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Update
            '***********************************************************************
            'Purpose:This function SAVE details of Agency Notes.
            'Input  :
            '<MS_UPDATEAGENCYNOTES_INPUT>
            '	<AGENCYNOTES LCode='' EmployeeID='' Notes='' />
            '</MS_UPDATEAGENCYNOTES_INPUT>
            'Output :
            '<MS_UPDATEAGENCYNOTES_OUTPUT>	
            '	<AGENCYNOTES LCode='' EmployeeID='' Notes='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_UPDATEAGENCYNOTES_OUTPUT>
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objSqlCommand1 As New SqlCommand
            Dim objUpdateDocOutput As New XmlDocument
            Dim strLCODE As String
            Dim strEmployeeID As String
            Dim strNotes As String
            Dim intRetId As Integer
            Dim intRecordsAffected As Int32
            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)
                With objUpdateDocOutput.DocumentElement.SelectSingleNode("AGENCYNOTES")
                    .Attributes("LCode").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AGENCYNOTES").Attributes("LCode").InnerText
                    .Attributes("EmployeeID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AGENCYNOTES").Attributes("EmployeeID").InnerText
                    .Attributes("Notes").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AGENCYNOTES").Attributes("Notes").InnerText
                End With
                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("AGENCYNOTES")
                    strLCODE = ((.Attributes("LCode").InnerText).Trim).ToString
                    strEmployeeID = ((.Attributes("EmployeeID").InnerText).Trim).ToString
                    strNotes = ((.Attributes("Notes").InnerText).Trim).ToString
                    If strLCODE = "" Then
                        Throw (New AAMSException("Agency Code can't be blank."))
                    End If
                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand1
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_AGENCY_NOTES"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "I"

                    .Parameters.Add(New SqlParameter("@EMPLOYEEID", SqlDbType.Int))
                    .Parameters("@EMPLOYEEID").Value = strEmployeeID

                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    .Parameters("@LCODE").Value = strLCODE

                    .Parameters.Add(New SqlParameter("@NOTES", SqlDbType.VarChar))
                    .Parameters("@NOTES").Value = strNotes

                    .Parameters.Add(New SqlParameter("@RETUNID", SqlDbType.Int))
                    .Parameters("@RETUNID").Direction = ParameterDirection.Output
                    .Parameters("@RETUNID").Value = ""

                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED
                    .Connection.Open()
                    intRecordsAffected = .ExecuteNonQuery()

                    intRetId = .Parameters("@RETUNID").Value
                    If intRetId = 0 Then
                        Throw (New AAMSException("Unable to insert!"))
                    Else
                        objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    End If
                End With
                'Checking whether record is deleted successfully or not
                objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
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
            '---------Not to be implemented----------
        End Function

        Public Function GetDetails(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the Agency product, based on the given field value
            'Input  : 
            '<MS_GETAGENCYNOTES_INPUT>
            '	<LCode></LCode>
            '</MS_GETAGENCYNOTES_INPUT>
            'Output :  
            '<MS_GETAGENCYNOTES_OUTPUT>
            '	<AGENCYNOTES LCode='' EmployeeID='' EmployeeName='' DateTime='' Notes='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_GETAGENCYNOTES_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strLCODE As String
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "GetDetails"
            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean


            objOutputXml.LoadXml(strGETDETAILS_OUTPUT)

            Try
                strLCODE = SearchDoc.DocumentElement.SelectSingleNode("LCode").InnerText.Trim
                If strLCODE = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Paging Section    
                If SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim() <> "" Then
                        intPageNo = SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim()
                    End If
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim() <> "" Then
                        intPageSize = SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim()
                    End If
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("SORT_BY") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim() <> "" Then
                        strSortBy = SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim()
                    End If
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("DESC") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("DESC").InnerText.Trim().ToUpper = "TRUE" Then
                        blnDesc = True
                    Else
                        blnDesc = False
                    End If
                End If
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_AGENCY_NOTES"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters.Add(New SqlParameter("@EMPLOYEEID", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@NOTES", SqlDbType.Int))
                    .Parameters("@ACTION").Value = "S"
                    .Parameters("@EMPLOYEEID").Value = vbNullString
                    .Parameters("@LCODE").Value = strLCODE
                    .Parameters("@NOTES").Value = vbNullString


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

                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AGENCYNOTES")
                objAptNodeClone = objAptNode.CloneNode(True)
                If objSqlReader.HasRows = True Then objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("LCode").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCode")) & "")
                    objAptNodeClone.Attributes("EmployeeID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmployeeID")) & "")
                    objAptNodeClone.Attributes("EmployeeName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Employee_Name")) & "")
                    objAptNodeClone.Attributes("DateTime").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DateTime")) & "")
                    objAptNodeClone.Attributes("Notes").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Notes")) & "")
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop
                objSqlReader.Close()
                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found")
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
    End Class
End Namespace
