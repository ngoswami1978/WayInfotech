'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Tapan $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizTravelAgency/bzAgencyPriority.vb $
'$Workfile: bzAgencyPriority.vb $
'$Revision: 8 $
'$Archive: /AAMS/Components/bizTravelAgency/bzAgencyPriority.vb $
'$Modtime: 18/07/08 10:10a $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizTravelAgency
    Public Class bzAgencyPriority
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzAgencyPriority"
        Const strVIEW_INPUT = "<MS_VIEWAGENCYPRIORITY_INPUT><PRIORITYID></PRIORITYID></MS_VIEWAGENCYPRIORITY_INPUT>"
        Const srtVIEW_OUTPUT = "<MS_VIEWAGENCYPRIORITY_OUTPUT><PRIORITY PRIORITYID='' PRIORITYNAME='' PROTECTED='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWAGENCYPRIORITY_OUTPUT>"
        Const strSEARCH_INPUT = "<MS_SEARCHAGENCYPRIORITY_INPUT><PRIORITYNAME></PRIORITYNAME></MS_SEARCHAGENCYPRIORITY_INPUT>"
        Const strSEARCH_OUTPUT = "<MS_SEARCHAGENCYPRIORITY_OUTPUT><PRIORITY PRIORITYID='' PRIORITYNAME='' PROTECTED='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHAGENCYPRIORITY_OUTPUT>"

        Const strADD_OUTPUT = "<MS_UPDATEAGENCYPRIORITY_INPUT><PRIORITY ACTION='' PRIORITYID='' PRIORITYNAME='' PROTECTED='' /></MS_UPDATEAGENCYPRIORITY_INPUT>"
        Const strUPDATE_INPUT = "<MS_UPDATEAGENCYPRIORITY_INPUT><PRIORITY ACTION=''  PRIORITYID='' PRIORITYNAME='' PROTECTED='' /></MS_UPDATEAGENCYPRIORITY_INPUT>"
        Const strUPDATE_OUTPUT = "<MS_UPDATEAGENCYPRIORITY_OUTPUT><PRIORITY ACTION=''  PRIORITYID='' PRIORITYNAME='' PROTECTED='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEAGENCYPRIORITY_OUTPUT>"
        Const strDELETE_INPUT = "<MS_DELETEAGENCYPRIORITY_INPUT><PRIORITYID></PRIORITYID></MS_DELETEAGENCYPRIORITY_INPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETEAGENCYPRIORITY_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETEAGENCYPRIORITY_OUTPUT>"



        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add
            'Declare Output XML String
            '***********************************************************************
            'Purpose: 
            'Input:None
            'Output:Xml Document
            '<MS_UPDATEAGENCYPRIORITY_INPUT>
            '   <PRIORITY ACTION='' PRIORITYID='' PRIORITYNAME='' PROTECTED='' />
            '</MS_UPDATEAGENCYPRIORITY_INPUT>
            '**************************************************************************
            Dim strMETHOD_NAME As String = "Add"
            Dim objXMLDoc As New XmlDocument
            Try
                objXMLDoc.LoadXml(strADD_OUTPUT)
            Catch Exec As Exception
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objXMLDoc, "101", Exec.Message)
                Return objXMLDoc
            End Try
            Return objXMLDoc
        End Function

        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Delete
            '***********************************************************************
            'Purpose:This function deletes a Online Status.
            'Input:XmlDocument
            '<MS_DELETEAGENCYPRIORITY_INPUT>
            '   <PRIORITYID></PRIORITYID>
            '</MS_DELETEAGENCYPRIORITY_INPUT>
            'Output :
            '<MS_DELETEAGENCYPRIORITY_OUTPUT>
            '	<Errors Status="">
            '		<Error Code="" Description="" />
            '	</Errors>
            '</MS_DELETEAGENCYPRIORITY_OUTPUT>
            '************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Delete"
            Dim strPriorityID As String

            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0
                strPriorityID = DeleteDoc.DocumentElement.SelectSingleNode("PRIORITYID").InnerText.Trim
                If strPriorityID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_PRIORITY"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"

                    .Parameters.Add(New SqlParameter("@PRIORITYID", SqlDbType.Int))
                    .Parameters("@PRIORITYID").Value = strPriorityID

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
                bzShared.FillErrorStatus(objDeleteDocOutput, "101", "Agency Priority in Use!")
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
            '<MS_SEARCHAGENCYPRIORITY_INPUT>
            '   <PRIORITYNAME></PRIORITYNAME>
            '</MS_SEARCHAGENCYPRIORITY_INPUT>

            'Output :
            '<MS_SEARCHAGENCYPRIORITY_OUTPUT>
            '   <PRIORITY PRIORITYID='' PRIORITYNAME='' PROTECTED='' />
            '   <PAGE PAGE_COUNT='' TOTAL_ROWS=''/>
            '       <Errors Status=''><Error Code='' Description='' />
            '       </Errors>
            '</MS_SEARCHAGENCYPRIORITY_OUTPUT>
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strPriorityName As String = ""

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strPriorityName = (SearchDoc.DocumentElement.SelectSingleNode("PRIORITYNAME").InnerText.Trim())
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
                    .CommandText = "UP_SER_TA_PRIORITY"
                    .Connection = objSqlConnection

                    .Parameters.Add("@PRIORITYNAME", SqlDbType.VarChar, 40)
                    .Parameters("@PRIORITYNAME").Value = strPriorityName

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
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("PRIORITY")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("PRIORITYID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRIORITYID")))
                    objAptNodeClone.Attributes("PRIORITYNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRIORITYNAME")))
                    objAptNodeClone.Attributes("PROTECTED").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PROTECTED")))
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
            '***********************************************************************
            'Purpose:This function Inserts/Updates City.
            'Input  :
            '<MS_UPDATEAGENCYPRIORITY_INPUT>
            '   <PRIORITY ACTION=''  PRIORITYID='' PRIORITYNAME='' PROTECTED='' />
            '</MS_UPDATEAGENCYPRIORITY_INPUT>

            'Output :
            '<MS_UPDATEAGENCYPRIORITY_OUTPUT>
            '   <PRIORITY ACTION=''  PRIORITYID='' PRIORITYNAME='' PROTECTED='' />
            '   <Errors Status=''><Error Code='' Description='' /></Errors>
            '</MS_UPDATEAGENCYPRIORITY_OUTPUT>

            '************************************************************************
            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objUpdateDocOutput As New XmlDocument
            Dim intPriorityID As Integer
            Dim strPriorityName As String
            Dim intProtected As Integer

            Dim intRecordsAffected As Int32
            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)

                With objUpdateDocOutput.DocumentElement.SelectSingleNode("PRIORITY")
                    .Attributes("ACTION").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("PRIORITY").Attributes("ACTION").InnerText
                    .Attributes("PRIORITYID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("PRIORITY").Attributes("PRIORITYID").InnerText
                    .Attributes("PRIORITYNAME").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("PRIORITY").Attributes("PRIORITYNAME").InnerText
                    .Attributes("PROTECTED").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("PRIORITY").Attributes("PROTECTED").InnerText
                End With

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("PRIORITY")
                    strAction = ((.Attributes("ACTION").InnerText).Trim).ToString
                    If strAction = "I" Then
                        intPriorityID = vbNullString
                    Else
                        intPriorityID = ((.Attributes("PRIORITYID").InnerText).Trim).ToString
                    End If
                    strPriorityName = ((.Attributes("PRIORITYNAME").InnerText).Trim).ToString
                    If ((.Attributes("PROTECTED").InnerText).Trim).ToString <> "" Then
                        intProtected = ((.Attributes("PROTECTED").InnerText).Trim).ToString
                    Else
                        intProtected = 0
                    End If

                    If strAction = "I" Or strAction = "U" Then
                        If strPriorityName = "" Then
                            Throw (New AAMSException("Priority Name can't be blank."))
                        End If
                    Else
                        Throw (New AAMSException("Invalid Action Code."))
                    End If

                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_PRIORITY"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction

                    .Parameters.Add(New SqlParameter("@PRIORITYID", SqlDbType.Int))
                    .Parameters("@PRIORITYID").Value = intPriorityID

                    .Parameters.Add(New SqlParameter("@PRIORITYNAME", SqlDbType.Char, 40))
                    .Parameters("@PRIORITYNAME").Value = strPriorityName

                    .Parameters.Add(New SqlParameter("@PROTECTED", SqlDbType.Bit))
                    .Parameters("@PROTECTED").Value = intProtected

                    .Parameters.Add(New SqlParameter("@RETUNID", SqlDbType.Int))
                    .Parameters("@RETUNID").Direction = ParameterDirection.Output

                    .Parameters("@RETUNID").Value = ""

                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED
                    .Connection.Open()

                    intRecordsAffected = .ExecuteNonQuery()

                    If UCase(strAction) = "I" Then
                        intRetId = .Parameters("@RETUNID").Value
                        If intRetId = 0 Then
                            Throw (New AAMSException("Unable to insert!"))
                        ElseIf intRetId = -1 Then
                            Throw (New AAMSException("Priority already exists. Please enter another Priority"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("PRIORITY").Attributes("PRIORITYID").InnerText = intRetId
                        End If
                    ElseIf UCase(strAction) = "U" Then
                        intRetId = .Parameters("@RETUNID").Value
                        If intRetId = 0 Then
                            Throw (New AAMSException("Unable to update!"))
                        ElseIf intRetId = -1 Then
                            Throw (New AAMSException("Priority already exists. Please enter another Priority"))
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
                If intRetId = 0 Then
                    bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                    If strAction = "I" Then
                        bzShared.FillErrorStatus(objUpdateDocOutput, "101", "Priority already exists. Please enter another Priority")
                    Else
                        bzShared.FillErrorStatus(objUpdateDocOutput, "101", "Unable to update!")
                    End If
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
            '***********************************************************************
            'Purpose:This function gives details of City.
            'Input  :
            '<MS_VIEWAGENCYPRIORITY_INPUT>
            '   <PRIORITYID></PRIORITYID>
            '</MS_VIEWAGENCYPRIORITY_INPUT>

            'Output :
            '<MS_VIEWAGENCYPRIORITY_OUTPUT>
            '   <PRIORITY PRIORITYID='' PRIORITYNAME='' PROTECTED='' /><Errors Status=''><Error Code='' Description='' /></Errors>
            '</MS_VIEWAGENCYPRIORITY_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strPriorityID As String
            Const strMETHOD_NAME As String = "View"
            Dim blnRecordFound As Boolean
            Try
                objOutputXml.LoadXml(srtVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strPriorityID = IndexDoc.DocumentElement.SelectSingleNode("PRIORITYID").InnerText.Trim
                If strPriorityID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_PRIORITY"
                    .Connection = objSqlConnection
                    .Parameters.Add("@PRIORITYID", SqlDbType.Int)
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@PRIORITYID").Value = strPriorityID
                    .Parameters("@ACTION").Value = "S"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("PRIORITY")
                        .Attributes("PRIORITYID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRIORITYID")) & "")
                        .Attributes("PRIORITYNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRIORITYNAME")) & "")
                        .Attributes("PROTECTED").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PROTECTED")) & "")
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
    End Class
End Namespace

