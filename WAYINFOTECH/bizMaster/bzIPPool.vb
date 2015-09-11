'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Tapan $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizMaster/bzIPPool.vb $
'$Workfile: bzIPPool.vb $
'$Revision: 17 $
'$Archive: /AAMS/Components/bizMaster/bzIPPool.vb $
'$Modtime: 17/07/08 6:00p $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizMaster
    Public Class bzIPPool
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzIPPool"
        Const strVIEW_OUTPUT = "<MS_VIEWIPPOOL_OUTPUT><IPPOOL PoolID='' PoolName='' Aoffice='' DepartmentID=''><IPAddress></IPAddress></IPPOOL><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWIPPOOL_OUTPUT>"
        Const strSEARCH_OUTPUT = "<MS_SEARCHIPPOOL_OUTPUT><IPPOOL PoolID='' PoolName='' Department_Name='' Aoffice='' IPAddress='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHIPPOOL_OUTPUT>"
        Const strADDIPPOOL_OUTPUT = "<MS_UPDATEIPPOOL_INPUT><IPPOOL PoolID='' PoolName='' Aoffice='' DepartmentID=''><IPAddress></IPAddress></IPPOOL></MS_UPDATEIPPOOL_INPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETEIPPOOL_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETEIPPOOL_OUTPUT>"
        Const strUPDATE_OUTPUT = "<MS_UPDATEIPPOOL_OUTPUT><IPPOOL PoolID='' PoolName='' Aoffice='' DepartmentID=''><IPAddress></IPAddress></IPPOOL><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEIPPOOL_OUTPUT>"

        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add
            'Purpose:This function Add a IPPool.
            'Declare Output XML String
            '***********************************************************************
            'Purpose: 
            'Input:None
            'XML
            '<MS_UPDATEIPPOOL_INPUT>
            '	<IPPOOL PoolID='' PoolName='' Aoffice='' DepartmentID=''>
            '		<IPAddress></IPAddress>
            '	</IPPOOL>
            '</MS_UPDATEIPPOOL_INPUT>
            '**************************************************************************
            Dim strMETHOD_NAME As String = "Add"
            Dim objXMLDoc As New XmlDocument
            Try
                objXMLDoc.LoadXml(strADDIPPOOL_OUTPUT)
            Catch Exec As Exception
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objXMLDoc, "101", Exec.Message)
                Return objXMLDoc
            End Try
            Return objXMLDoc
        End Function
        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Delete
            '***********************************************************************
            'Purpose:This function deletes a IPPool.
            'Input:XmlDocument
            '<MS_DELETEIPPOOL_INPUT>
            '	<PoolID></PoolID>
            '</MS_DELETEIPPOOL_INPUT>

            'Output :
            '<MS_DELETEIPPOOL_OUTPUT>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_DELETEIPPOOL_OUTPUT>

            '************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objTransaction As SqlTransaction
            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Delete"
            Dim intPoolID As Integer

            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0

                intPoolID = DeleteDoc.DocumentElement.SelectSingleNode("PoolID").InnerText.Trim

                If intPoolID = 0 Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_IP_POOL"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"
                    .Parameters.Add(New SqlParameter("@POOLID", SqlDbType.Int))
                    .Parameters("@POOLID").Value = intPoolID

                    objSqlCommand.Connection.Open()
                    objTransaction = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)
                    .Transaction = objTransaction
                    intRecordsAffected = .ExecuteNonQuery()
                    objTransaction.Commit()
                End With
                objSqlConnection.Close()

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
            'Purpose:This function gives search results based on choosen search criteria.
            'Input  :- 
            '<MS_SEARCHIPPOOL_INPUT>
            '	<PoolName></PoolName>
            '	<Aoffice></Aoffice>
            '	<Dept_Name></Dept_Name>
            '   <PAGE_NO></PAGE_NO>
            '   <PAGE_SIZE></PAGE_SIZE>
            '   <SORT_BY></SORT_BY>
            '   <DESC></DESC>
            '</MS_SEARCHIPPOOL_INPUT>

            'Output :
            '<MS_SEARCHIPPOOL_OUTPUT>
            '	<IPPOOL PoolID='' PoolName='' DeptName='' Aoffice='' IP='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_SEARCHIPPOOL_OUTPUT>

            ' ************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strIPPoolName As String
            Dim strAoffice As String
            Dim strDeptName As String
            Dim intPoolID As Integer

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean


            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)
            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                If (SearchDoc.DocumentElement.SelectSingleNode("PoolName").InnerText.Trim() = "") Then
                    strIPPoolName = vbNullString
                Else
                    strIPPoolName = (SearchDoc.DocumentElement.SelectSingleNode("PoolName").InnerText.Trim())
                End If


                If (SearchDoc.DocumentElement.SelectSingleNode("Aoffice").InnerText.Trim() = "") Then
                    strAoffice = vbNullString
                Else
                    strAoffice = (SearchDoc.DocumentElement.SelectSingleNode("Aoffice").InnerText.Trim())
                End If


                If (SearchDoc.DocumentElement.SelectSingleNode("Department_Name").InnerText.Trim() = "") Then
                    strDeptName = vbNullString
                Else
                    strDeptName = (SearchDoc.DocumentElement.SelectSingleNode("Department_Name").InnerText.Trim())
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
                    .CommandText = "UP_SER_MS_IP_POOL"
                    .Connection = objSqlConnection

                    .Parameters.Add("@POOLID", SqlDbType.Int)
                    If (intPoolID = 0) Then
                        .Parameters("@POOLID").Value = DBNull.Value
                    Else
                        .Parameters("@POOLID").Value = intPoolID
                    End If
                    .Parameters.Add("@POOLNAME", SqlDbType.VarChar, 100)
                    .Parameters("@POOLNAME").Value = strIPPoolName

                    .Parameters.Add("@AOFFICE", SqlDbType.VarChar, 3)
                    .Parameters("@AOFFICE").Value = strAoffice

                    .Parameters.Add("@DEPTNAME", SqlDbType.VarChar, 50)
                    .Parameters("@DEPTNAME").Value = strDeptName

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
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("IPPOOL")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("PoolID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("POOLID")))
                    objAptNodeClone.Attributes("PoolName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("POOLNAME")))
                    objAptNodeClone.Attributes("Department_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DEPARTMENT_NAME")))
                    objAptNodeClone.Attributes("Aoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AOFFICE")))
                    objAptNodeClone.Attributes("IPAddress").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("IPAddress")))
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
            'Purpose:This function Inserts/Updates a IPPool.
            'Input  :
            '<MS_UPDATEIPPOOL_INPUT>
            '	<IPPOOL PoolID='' PoolName='' Aoffice='' DepartmentID=''>
            '		<IPAddress></IPAddress>
            '	</IPPOOL>
            '</MS_UPDATEIPPOOL_INPUT>

            'Output  :
            '<MS_UPDATEIPPOOL_OUTPUT>
            '	<IPPOOL PoolID='' PoolName='' Aoffice='' DepartmentID=''>
            '		<IPAddress></IPAddress>
            '	</IPPOOL>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_UPDATEIPPOOL_OUTPUT>


            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objTransaction As SqlTransaction
            Dim objSqlCommand As New SqlCommand
            Dim objUpdateDocOutput As New XmlDocument

            Dim strPoolName As String
            Dim strAoffice As String
            Dim intIPPoolId As Integer
            Dim intDeptID As Integer
            Dim strIPAddress As String = ""

            Dim intRecordsAffected As Int32
            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)
                Dim objXmlNode As XmlNode
                Dim objXmlNodeList As XmlNodeList
                Dim objAptNode As XmlNode, objAptNodeClone As XmlNode

                With objUpdateDocOutput.DocumentElement.SelectSingleNode("IPPOOL")
                    .Attributes("PoolID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("IPPOOL").Attributes("PoolID").InnerText
                    .Attributes("PoolName").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("IPPOOL").Attributes("PoolName").InnerText
                    .Attributes("Aoffice").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("IPPOOL").Attributes("Aoffice").InnerText
                    .Attributes("DepartmentID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("IPPOOL").Attributes("DepartmentID").InnerText
                End With
                objAptNode = objUpdateDocOutput.DocumentElement.SelectSingleNode("IPPOOL/IPAddress")
                objAptNodeClone = objAptNode.CloneNode(True)
                objUpdateDocOutput.DocumentElement.SelectSingleNode("IPPOOL").RemoveChild(objAptNode)

                objXmlNodeList = UpdateDoc.DocumentElement.SelectNodes("IPPOOL/IPAddress")
                'objXmlNodeList = UpdateDoc.DocumentElement.ChildNodes
                For Each objXmlNode In objXmlNodeList
                    strIPAddress = strIPAddress & "," & objXmlNode.InnerText
                    objAptNodeClone.InnerText = objXmlNode.InnerText
                    objUpdateDocOutput.DocumentElement.SelectSingleNode("IPPOOL").AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Next

                If strIPAddress.Length() > 0 Then
                    strIPAddress = Mid(strIPAddress, 2, strIPAddress.Length())
                End If


                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("IPPOOL")
                    If ((.Attributes("PoolID").InnerText).Trim = "") Then
                        intIPPoolId = 0
                        strAction = "I"
                    Else
                        intIPPoolId = ((.Attributes("PoolID").InnerText).Trim)
                        strAction = "U"
                    End If

                    If ((.Attributes("PoolName").InnerText).Trim = "") Then
                        strPoolName = vbNullString
                    Else
                        strPoolName = ((.Attributes("PoolName").InnerText).Trim)
                    End If

                    If ((.Attributes("DepartmentID").InnerText).Trim = "") Then
                        intDeptID = 0
                    Else
                        intDeptID = ((.Attributes("DepartmentID").InnerText).Trim)
                    End If

                    If ((.Attributes("Aoffice").InnerText).Trim = "") Then
                        strAoffice = vbNullString
                    Else
                        strAoffice = ((.Attributes("Aoffice").InnerText).Trim)
                    End If

                    If strAction = "I" Or strAction = "U" Then
                        If strAction = "U" Then
                            If intIPPoolId = 0 Then
                                Throw (New AAMSException("IPPoolID can't be blank."))
                            ElseIf strPoolName = "" Then
                                Throw (New AAMSException("IPPoolName can't be blank."))
                            End If
                        End If
                        If strAction = "I" Then
                            If strPoolName = "" Then
                                Throw (New AAMSException("IPPoolName can't be blank."))
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
                    .CommandText = "UP_SRO_MS_IP_POOL"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction
                    .Parameters.Add(New SqlParameter("@POOLID", SqlDbType.Int))

                    If strAction = "I" Then
                        If intIPPoolId = 0 Then
                            .Parameters("@POOLID").Value = DBNull.Value
                        End If
                    Else
                        .Parameters("@POOLID").Value = intIPPoolId
                    End If

                    .Parameters.Add(New SqlParameter("@POOLNAME", SqlDbType.VarChar, 200))
                    .Parameters("@POOLNAME").Value = strPoolName

                    .Parameters.Add(New SqlParameter("@AOFFICE", SqlDbType.VarChar, 3))
                    .Parameters("@AOFFICE").Value = strAoffice


                    .Parameters.Add(New SqlParameter("@IPADDRESS", SqlDbType.VarChar, 8000))
                    .Parameters("@IPADDRESS").Value = strIPAddress

                    .Parameters.Add(New SqlParameter("@DEPARTMENTID", SqlDbType.Int))
                    .Parameters("@DEPARTMENTID").Value = intDeptID

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0

                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED

                    .Connection.Open()
                    objTransaction = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)
                    .Transaction = objTransaction
                    intRecordsAffected = .ExecuteNonQuery()
                    objTransaction.Commit()
                    .Connection.Close()
                    If UCase(strAction) = "I" Then
                        intRetId = .Parameters("@RETURNID").Value
                        If intRetId = 0 Then
                            Throw (New AAMSException("Unable to Insert!"))
                        End If
                        With objUpdateDocOutput.DocumentElement.SelectSingleNode("IPPOOL")
                            .Attributes("PoolID").InnerText = intRetId
                        End With
                    ElseIf UCase(strAction) = "U" Then
                        intRetId = .Parameters("@RETURNID").Value
                        If intRetId = 0 Then
                            Throw (New AAMSException("Unable to update!"))
                        End If
                    End If

                End With
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                If intRetId = 0 Then
                    bzShared.FillErrorStatus(objUpdateDocOutput, "101", "IP Pool already exists. Please enter another IP Pool.")
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
            'Purpose:This function gives details of a IP Pool.
            'Input  :
            '<MS_VIEWIPPOOL_INPUT>
            '	<PoolName></PoolName>
            '	<Aoffice></Aoffice>
            '	<Dept_Name></Dept_Name>
            '</MS_VIEWIPPOOL_INPUT>

            'Output :
            '<MS_VIEWIPPOOL_OUTPUT>
            '	<IPPOOL PoolName='' Dept_Name='' Aoffice='' IP='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_VIEWIPPOOL_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim intIPPoolID As Integer
            Dim blnRecordFound As Boolean
            Dim objAptNode As XmlNode, objAptNodeClone As XmlNode
            Const strMETHOD_NAME As String = "View"

            Try
                objOutputXml.LoadXml(strVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                intIPPoolID = IndexDoc.DocumentElement.SelectSingleNode("PoolID").InnerText.Trim
                If intIPPoolID = 0 Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_IP_POOL"
                    .Connection = objSqlConnection
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"

                    .Parameters.Add("@POOLID", SqlDbType.Int)
                    .Parameters("@POOLID").Value = intIPPoolID
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                'Reading and Appending records into the Output XMLDocument

                Do While objSqlReader.Read()
                    If blnRecordFound = False Then
                        blnRecordFound = True
                        objOutputXml.DocumentElement.SelectSingleNode("IPPOOL").Attributes("PoolID").Value = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("POOLID")))
                        objOutputXml.DocumentElement.SelectSingleNode("IPPOOL").Attributes("PoolName").Value = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("POOLNAME")))
                        objOutputXml.DocumentElement.SelectSingleNode("IPPOOL").Attributes("DepartmentID").Value = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DepartmentID")))
                        objOutputXml.DocumentElement.SelectSingleNode("IPPOOL").Attributes("Aoffice").Value = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Aoffice")))

                        objAptNode = objOutputXml.DocumentElement.SelectSingleNode("IPPOOL/IPAddress")
                        objAptNodeClone = objAptNode.CloneNode(True)
                        objOutputXml.DocumentElement.SelectSingleNode("IPPOOL").RemoveChild(objAptNode)
                    End If
                    objAptNodeClone.InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("IPAddress")))
                    objOutputXml.DocumentElement.SelectSingleNode("IPPOOL").AppendChild(objAptNodeClone)
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
    End Class
End Namespace

