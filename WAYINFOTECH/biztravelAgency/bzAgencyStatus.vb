'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Tapan $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizTravelAgency/bzAgencyStatus.vb $
'$Workfile: bzAgencyStatus.vb $
'$Revision: 10 $
'$Archive: /AAMS/Components/bizTravelAgency/bzAgencyStatus.vb $
'$Modtime: 18/07/08 10:12a $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizTravelAgency
    Public Class bzAgencyStatus
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzAgencyStatus"
        Const gstrLIST_OUTPUT = "<MS_LISTAGENCYSTATUS_OUTPUT><AGENCYSTATUS AgencyStatusId='' Agency_Status_Name='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTAGENCYSTATUS_OUTPUT>"
        Const strVIEW_INPUT = "<MS_VIEWSTATUS_INPUT><AgencyStatusId /></MS_VIEWSTATUS_INPUT>"
        Const srtVIEW_OUTPUT = "<MS_VIEWSTATUS_OUTPUT><AgencyStatus Agency_Status_Name='' AgencyStatusID='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWSTATUS_OUTPUT>"
        Const strSEARCH_INPUT = "<MS_SEARCHSTATUS_INPUT><Agency_Status_Name></Agency_Status_Name></MS_SEARCHSTATUS_INPUT>"
        Const strSEARCH_OUTPUT = "<MS_SEARCHSTATUS_OUTPUT><AgencyStatus Agency_Status_Name='' AgencyStatusID=''/><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHSTATUS_OUTPUT>"

        Const strADD_OUTPUT = "<MS_ADDSTATUS_OUTPUT><AgencyStatus ACTION='' Agency_Status_Name='' AgencyStatusID='' /></MS_ADDSTATUS_OUTPUT>"
        Const strUPDATE_INPUT = "<MS_UPDATESTATUS_INPUT><AgencyStatus ACTION='' Agency_Status_Name='' AgencyStatusID='' /></MS_UPDATESTATUS_INPUT>"
        Const strUPDATE_OUTPUT = "<MS_UPDATESTATUS_OUTPUT><AgencyStatus ACTION='' Agency_Status_Name='' AgencyStatusID='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATESTATUS_OUTPUT>"
        Const strDELETE_INPUT = "<MS_DELETESTATUS_INPUT><AgencyStatusID/></MS_DELETESTATUS_INPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETESTATUS_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETESTATUS_OUTPUT>"

        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add
            'Declare Output XML String
            '***********************************************************************
            'Purpose: 
            'Input:None
            'Output:Xml Document
            '<MS_ADDSTATUS_OUTPUT><AgencyStatus ACTION='' Agency_Status_Name='' AgencyStatusID='' /></MS_ADDSTATUS_OUTPUT>
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
            '<MS_DELETESTATUS_INPUT><AgencyStatusID/></MS_DELETESTATUS_INPUT>
            'Output :
            '<MS_DELETESTATUS_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETESTATUS_OUTPUT>
            '************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Delete"
            Dim strAgencyStatusID As String

            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0
                strAgencyStatusID = DeleteDoc.DocumentElement.SelectSingleNode("AgencyStatusID").InnerText.Trim
                If strAgencyStatusID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_STATUS"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"

                    .Parameters.Add(New SqlParameter("@AGENCYSTATUSID", SqlDbType.Int))
                    .Parameters("@AGENCYSTATUSID").Value = strAgencyStatusID

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
                bzShared.FillErrorStatus(objDeleteDocOutput, "101", "Agency Status in Use!")
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
            '<MS_SEARCHSTATUS_INPUT><Agency_Status_Name></Agency_Status_Name></MS_SEARCHSTATUS_INPUT>

            'Output :
            '<MS_SEARCHSTATUS_OUTPUT><AgencyStatus Agency_Status_Name='' AgencyStatusID=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHSTATUS_OUTPUT>
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strAgency_Status_Name As String = ""

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strAgency_Status_Name = (SearchDoc.DocumentElement.SelectSingleNode("Agency_Status_Name").InnerText.Trim())
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
                    .CommandText = "UP_SER_TA_STATUS"
                    .Connection = objSqlConnection

                    .Parameters.Add("@AGENCYSTATUSNAME", SqlDbType.VarChar, 20)
                    .Parameters("@AGENCYSTATUSNAME").Value = strAgency_Status_Name

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
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AgencyStatus")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("Agency_Status_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Agency_Status_Name")) & "")
                    objAptNodeClone.Attributes("AgencyStatusID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AgencyStatusID")))
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
            '<MS_UPDATESTATUS_INPUT><AgencyStatus ACTION='' Agency_Status_Name='' AgencyStatusID='' /></MS_UPDATESTATUS_INPUT>

            'Output :
            '<MS_UPDATESTATUS_OUTPUT><AgencyStatus ACTION='' Agency_Status_Name='' AgencyStatusID='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATESTATUS_OUTPUT>"

            '************************************************************************
            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objUpdateDocOutput As New XmlDocument
            Dim intStatusID As Integer
            Dim strStatusName As String
            Dim intProtected As Integer

            Dim intRecordsAffected As Int32
            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)

                With objUpdateDocOutput.DocumentElement.SelectSingleNode("AgencyStatus")
                    .Attributes("ACTION").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AgencyStatus").Attributes("ACTION").InnerText
                    .Attributes("AgencyStatusID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AgencyStatus").Attributes("AgencyStatusID").InnerText
                    .Attributes("Agency_Status_Name").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AgencyStatus").Attributes("Agency_Status_Name").InnerText
                End With

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("AgencyStatus")
                    strAction = ((.Attributes("ACTION").InnerText).Trim).ToString
                    If strAction = "I" Then
                        intStatusID = vbNullString
                    Else
                        intStatusID = ((.Attributes("AgencyStatusID").InnerText).Trim).ToString
                    End If
                    strStatusName = ((.Attributes("Agency_Status_Name").InnerText).Trim).ToString

                    If strAction = "I" Or strAction = "U" Then
                        If strStatusName = "" Then
                            Throw (New AAMSException("Agency Status can't be blank."))
                        End If
                    Else
                        Throw (New AAMSException("Invalid Action Code."))
                    End If

                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_STATUS"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction

                    .Parameters.Add(New SqlParameter("@AGENCYSTATUSID", SqlDbType.Int))
                    .Parameters("@AGENCYSTATUSID").Value = intStatusID

                    .Parameters.Add(New SqlParameter("@AGENCYSTATUSNAME", SqlDbType.Char, 40))
                    .Parameters("@AGENCYSTATUSNAME").Value = strStatusName

                    
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
                            Throw (New AAMSException("Agency Status already exists. Please enter another Agency Status"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("AgencyStatus").Attributes("AgencyStatusID").InnerText = intRetId
                        End If
                    ElseIf UCase(strAction) = "U" Then
                        intRetId = .Parameters("@RETUNID").Value
                        If intRetId = 0 Then
                            Throw (New AAMSException("Unable to update!"))
                        ElseIf intRetId = -1 Then
                            Throw (New AAMSException("Agency Status already exists. Please enter another Agency Status"))
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
        Public Function List() As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the AgencyType record, based on the given field value
            'Input  : 
            'Output :  
            '<MS_LISTAGENCYSTATUS_OUTPUT>
            '	<AGENCYSTATUS AgencyStatusId='' Agency_Status_Name='' />
            '	<Errors Status="">
            '		<Error Code="" Description="" />
            '	</Errors>
            '</MS_LISTAGENCYSTATUS_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "List"

            objOutputXml.LoadXml(gstrLIST_OUTPUT)

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LST_TA_STATUS"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTATUS")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("AgencyStatusId").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AgencyStatusId")))
                    objAptNodeClone.Attributes("Agency_Status_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Agency_Status_Name")))
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop

                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found")
                Else
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
        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.View
            '***********************************************************************
            'Purpose:This function gives details of City.
            'Input  :
            '<MS_SEARCHORDERSTATUS_INPUT>
            '	<ORDERSTATUSID/>
            '</MS_SEARCHORDERSTATUS_INPUT>

            'Output :
            '<MS_VIEWSTATUS_OUTPUT><AgencyStatus Agency_Status_Name='' AgencyStatusID='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWSTATUS_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strST_ID As String
            Const strMETHOD_NAME As String = "View"
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(srtVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strST_ID = IndexDoc.DocumentElement.SelectSingleNode("AgencyStatusId").InnerText.Trim
                If strST_ID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_STATUS"
                    .Connection = objSqlConnection
                    .Parameters.Add("@AGENCYSTATUSID", SqlDbType.Int)
                    .Parameters("@AGENCYSTATUSID").Value = strST_ID
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("AgencyStatus")
                        .Attributes("Agency_Status_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AGENCY_STATUS_NAME")))
                        .Attributes("AgencyStatusID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AGENCYSTATUSID")))
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