'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Neeraj $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizMaster/bzSecurityRegion.vb $
'$Workfile: bzSecurityRegion.vb $
'$Revision: 13 $
'$Archive: /AAMS/Components/bizMaster/bzSecurityRegion.vb $
'$Modtime: 3/18/11 12:20p $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizMaster

    Public Class bzSecurityRegion
        Implements bizInterface.BizLayerI

        Const strClass_NAME = "bzSecurityRegion"
        Const strADDSECURITYREGION_OUTPUT = "<MS_UPDATESECURITYREGION_INPUT> <SECURITYREGION RegionID='' Name=''><Aoffice></Aoffice></SECURITYREGION></MS_UPDATESECURITYREGION_INPUT>"
        Const strVIEW_OUTPUT = "<MS_VIEWSECURITYREGION_OUTPUT><SECURITYREGION RegionID='' Name=''><Aoffice></Aoffice></SECURITYREGION><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWSECURITYREGION_OUTPUT>"
        Const strSecRegionList_OUTPUT = "<MS_LISTSECURITYREGION_OUTPUT><SECURITYREGION RegionID='' Name=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTSECURITYREGION_OUTPUT>"
        Const strSEARCH_OUTPUT = "<MS_SEARCHSECURITYREGION_OUTPUT><SECURITYREGION RegionID='' Name=''/><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHSECURITYREGION_OUTPUT>"

        Const strDELETE_OUTPUT = "<MS_DELETESECURITYREGION_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETESECURITYREGION_OUTPUT>"
        Const strUPDATE_OUTPUT = "<MS_UPDATESECURITYREGION_OUTPUT><SECURITYREGION RegionID='' Name=''><Aoffice></Aoffice></SECURITYREGION><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATESECURITYREGION_OUTPUT>"
        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add
            'Declare Output XML String
            '***********************************************************************
            'Purpose: 
            'Input:None
            'Output:Xml Document
            '<MS_UPDATESECURITYREGION_INPUT> 
            '	<SECURITYREGION RegionID='' Name=''>
            '		<Aoffice></Aoffice>
            '	</SECURITYREGION>
            '</MS_UPDATESECURITYREGION_INPUT>
            '**************************************************************************
            Dim strMETHOD_NAME As String = "Add"
            Dim objXMLDoc As New XmlDocument
            Try
                objXMLDoc.LoadXml(StrADDSECURITYREGION_OUTPUT)
            Catch Exec As Exception
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objXMLDoc, "101", Exec.Message)
                Return objXMLDoc
            End Try
            Return objXMLDoc
        End Function
        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Delete
            '***********************************************************************
            'Purpose:This function deletes a Security Region.
            'Input:XmlDocument
            '<MS_DELETESECURITYREGION_INPUT>
            '	<RegionID></RegionID>
            '</MS_DELETESECURITYREGION_INPUT>
            'Output :
            '<MS_DELETESECURITYREGION_OUTPUT>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_DELETESECURITYREGION_OUTPUT>      

            '************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objTransaction As SqlTransaction
            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Delete"
            Dim strSecurityRegionID As String

            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0
                strSecurityRegionID = DeleteDoc.DocumentElement.SelectSingleNode("RegionID").InnerText.Trim
                If strSecurityRegionID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_SECURITY_REGION"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"
                    .Parameters.Add(New SqlParameter("@REGIONID", SqlDbType.Int))
                    .Parameters("@REGIONID").Value = strSecurityRegionID
                    .Connection.Open()
                    objTransaction = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)
                    .Transaction = objTransaction
                    intRecordsAffected = .ExecuteNonQuery()
                    objTransaction.Commit()
                    .Connection.Close()
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
                If objSqlConnection.State <> ConnectionState.Closed Then
                    If Not objTransaction Is Nothing Then
                        objTransaction.Rollback()
                    End If
                    objSqlConnection.Close()
                End If
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
            'Purpose:This function gives search results based on choosen security region.
            'Input  :- 
            '<MS_SEARCHSECURITYREGION_INPUT>
            '	<Name></Name>
            '</MS_SEARCHSECURITYREGION_INPUT>

            'Output :
            '<MS_SEARCHSECURITYREGION_OUTPUT> 
            '	<SECURITYREGION RegionID='' Name=''/>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_SEARCHSECURITYREGION_OUTPUT>


            ''************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strSecurityRegionName As String

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strSecurityRegionName = (SearchDoc.DocumentElement.SelectSingleNode("Name").InnerText.Trim())
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
                    .CommandText = "UP_SER_MS_SECURITY_REGION"
                    .Connection = objSqlConnection
                    .Parameters.Add("@SEC_REGIONNAME", SqlDbType.VarChar, 100)
                    .Parameters("@SEC_REGIONNAME").Value = strSecurityRegionName

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
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("SECURITYREGION")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("RegionID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("RegionID")))
                    objAptNodeClone.Attributes("Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Name")))
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
            'Purpose:This function Inserts/Updates Security Region.
            'Input  :
            '<MS_UPDATESECURITYREGION_INPUT> 
            '	<SECURITYREGION RegionID='' Name=''>
            '		<Aoffice></Aoffice>
            '	</SECURITYREGION>
            '</MS_UPDATESECURITYREGION_INPUT>


            'Output :
            '<MS_UPDATESECURITYREGION_OUTPUT> 
            '	<SECURITYREGION RegionID='' Name=''>
            '		<Aoffice></Aoffice>
            '	</SECURITYREGION>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_UPDATESECURITYREGION_OUTPUT>
            '************************************************************************
            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objTransaction As SqlTransaction
            Dim objUpdateDocOutput As New XmlDocument

            Dim intRegionID As Integer
            Dim strRegionName As String
            Dim strAoffice As String = ""
            Dim intRecordsAffected As Int32
            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)
                Dim objXmlNode As XmlNode
                Dim objXmlNodeList As XmlNodeList
                Dim objAptNode As XmlNode, objAptNodeClone As XmlNode

                With objUpdateDocOutput.DocumentElement.SelectSingleNode("SECURITYREGION")
                    .Attributes("RegionID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("SECURITYREGION").Attributes("RegionID").InnerText
                    .Attributes("Name").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("SECURITYREGION").Attributes("Name").InnerText
                End With
                objAptNode = objUpdateDocOutput.DocumentElement.SelectSingleNode("SECURITYREGION/Aoffice")
                objAptNodeClone = objAptNode.CloneNode(True)
                objUpdateDocOutput.DocumentElement.SelectSingleNode("SECURITYREGION").RemoveChild(objAptNode)

                objXmlNodeList = UpdateDoc.DocumentElement.SelectNodes("SECURITYREGION/Aoffice")
                'objXmlNodeList = UpdateDoc.DocumentElement.ChildNodes
                For Each objXmlNode In objXmlNodeList
                    strAoffice = strAoffice & "," & objXmlNode.InnerText
                    objAptNodeClone.InnerText = objXmlNode.InnerText
                    objUpdateDocOutput.DocumentElement.SelectSingleNode("SECURITYREGION").AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Next

                If strAoffice.Length() > 0 Then
                    strAoffice = Mid(strAoffice, 2, strAoffice.Length())
                End If

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("SECURITYREGION")
                    If ((.Attributes("RegionID").InnerText).Trim = "") Then
                        intRegionID = 0
                        strAction = "I"
                    Else
                        intRegionID = ((.Attributes("RegionID").InnerText).Trim)
                        strAction = "U"
                    End If

                    If ((.Attributes("Name").InnerText).Trim = "") Then
                        strRegionName = 0
                    Else
                        strRegionName = ((.Attributes("Name").InnerText).Trim)
                    End If

                    If strAction = "I" Or strAction = "U" Then
                        If strAction = "U" Then
                            If intRegionID = 0 Then
                                Throw (New AAMSException("Security RegionID can't be blank."))
                            ElseIf strRegionName = "" Then
                                Throw (New AAMSException("Security Region Name can't be blank."))
                            End If
                        End If
                        If strAction = "I" Then
                            If strRegionName = "" Then
                                Throw (New AAMSException("Security Region Name can't be blank."))
                            End If
                        End If
                    Else
                        Throw (New AAMSException("Invalid Action Code."))
                    End If
                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_SECURITY_REGION"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction

                    .Parameters.Add(New SqlParameter("@REGIONID", SqlDbType.Int))
                    If intRegionID = 0 Then
                        .Parameters("@REGIONID").Value = DBNull.Value
                    Else
                        .Parameters("@REGIONID").Value = intRegionID
                    End If

                    .Parameters.Add(New SqlParameter("@REGIONNAME", SqlDbType.VarChar, 40))
                    .Parameters("@REGIONNAME").Value = strRegionName

                    .Parameters.Add(New SqlParameter("@AOFFICE", SqlDbType.VarChar, 8000))
                    .Parameters("@AOFFICE").Value = strAoffice

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0
                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED
                    .Connection.Open()
                    objTransaction = objSqlConnection.BeginTransaction
                    .Transaction = objTransaction
                    intRecordsAffected = .ExecuteNonQuery()
                    objTransaction.Commit()
                    .Connection.Close()
                    intRetId = .Parameters("@RETURNID").Value
                End With
                If UCase(strAction) = "I" Then
                    If intRetId = 0 Then
                        Throw (New AAMSException("Unable to Insert!"))
                    End If
                    With objUpdateDocOutput.DocumentElement.SelectSingleNode("SECURITYREGION")
                        .Attributes("RegionID").InnerText = intRetId
                    End With
                ElseIf UCase(strAction) = "U" Then
                    If intRetId = 0 Then
                        Throw (New AAMSException("Unable to update!"))
                    End If
                End If

            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                If intRetId = 0 Then
                    bzShared.FillErrorStatus(objUpdateDocOutput, "101", "Security Region already exists. Please enter another Security Region.")
                Else
                    bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                End If
                Return objUpdateDocOutput
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                'Fill Error Status
                If objSqlConnection.State <> ConnectionState.Closed Then
                    If Not objTransaction Is Nothing Then
                        objTransaction.Rollback()
                    End If
                    objSqlConnection.Close()
                End If
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                Return objUpdateDocOutput
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try

            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Return objUpdateDocOutput
        End Function
        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.View
            '***********************************************************************
            'Purpose:This function gives details of Security Region.
            'Input  :
            '<MS_VIEWSECURITYREGION_INPUT>
            '	<RegionID></RegionID>
            '</MS_VIEWSECURITYREGION_INPUT>


            'Output :
            '<MS_VIEWSECURITYREGION_OUTPUT> 
            '	<SECURITYREGION RegionID='' Name=''>
            '		<Aoffice></Aoffice>
            '	</SECURITYREGION>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_VIEWSECURITYREGION_OUTPUT>

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strRegionID As String
            Dim blnRecordFound As Boolean
            Dim objAptNode As XmlNode, objAptNodeClone As XmlNode
            Const strMETHOD_NAME As String = "View"

            Try
                objOutputXml.LoadXml(strVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strRegionID = IndexDoc.DocumentElement.SelectSingleNode("RegionID").InnerText.Trim
                If strRegionID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_SECURITY_REGION"
                    .Connection = objSqlConnection
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"

                    .Parameters.Add("@REGIONID", SqlDbType.Char, 3)
                    .Parameters("@REGIONID").Value = strRegionID
                End With

                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    If blnRecordFound = False Then
                        blnRecordFound = True
                        objOutputXml.DocumentElement.SelectSingleNode("SECURITYREGION").Attributes("RegionID").Value = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("RegionID")))
                        objOutputXml.DocumentElement.SelectSingleNode("SECURITYREGION").Attributes("Name").Value = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Name")))
                        objAptNode = objOutputXml.DocumentElement.SelectSingleNode("SECURITYREGION/Aoffice")
                        objAptNodeClone = objAptNode.CloneNode(True)
                        objOutputXml.DocumentElement.SelectSingleNode("SECURITYREGION").RemoveChild(objAptNode)
                    End If
                    objAptNodeClone.InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Aoffice")))
                    objOutputXml.DocumentElement.SelectSingleNode("SECURITYREGION").AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
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
        Public Function List() As XmlDocument
            '***********************************************************************
            'Purpose: List out SecurityRegion 
            'Input  : 
            'Output :  
            '<MS_LISTSECURITYREGION_OUTPUT>
            '       <SECURITYREGION RegionID='' Name=''/>
            '       <Errors Status=''>
            '           <Error Code='' Description='' />
            '       </Errors>
            '</MS_LISTSECURITYREGION_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "List"

            objOutputXml.LoadXml(strSecRegionList_OUTPUT)

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LST_MS_SECURITY_REGION"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("SECURITYREGION")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)

                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("RegionID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("RegionID")))
                    objAptNodeClone.Attributes("Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Name")))
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
        Public Function List1() As XmlDocument
            '***********************************************************************
            'Purpose: List out SecurityRegion 
            'Input  : 
            'Output :  
            '<MS_LISTSECURITYREGION_OUTPUT>
            '       <SECURITYREGION RegionID='' Name=''/>
            '       <Errors Status=''>
            '           <Error Code='' Description='' />
            '       </Errors>
            '</MS_LISTSECURITYREGION_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "List"

            objOutputXml.LoadXml(strSecRegionList_OUTPUT)

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LST_MS_SECURITY_REGION_WITHAOFFICE"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("SECURITYREGION")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)

                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("RegionID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("RegionID")))
                    objAptNodeClone.Attributes("Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Name")))
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
    End Class
End Namespace
