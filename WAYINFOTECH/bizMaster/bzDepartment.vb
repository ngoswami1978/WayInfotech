'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Tapan $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizMaster/bzDepartment.vb $
'$Workfile: bzDepartment.vb $
'$Revision: 20 $
'$Archive: /AAMS/Components/bizMaster/bzDepartment.vb $
'$Modtime: 18/07/08 11:33a $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizMaster
    Public Class bzDepartment
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzDepartment"
        Const gstrList_OUTPUT = "<MS_LISTDEPARTMENT_OUTPUT><DEPARTMENT DepartmentID='' Department_Name='' ManagerId='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTDEPARTMENT_OUTPUT>"
        Const srtVIEW_OUTPUT = "<MS_VIEWDEPARTMENT_OUTPUT><DEPARTMENT DepartmentID='' Department_Name='' ManagerID='' ManagerName='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWDEPARTMENT_OUTPUT>"
        Const strSEARCH_INPUT = "<MS_SEARCHEPARTMENT_INPUT><Department_Name></Department_Name><ManagerName></ManagerName></MS_SEARCHEPARTMENT_INPUT>"
        Const strSEARCH_OUTPUT = "<MS_SEARCHDEPARTMENT_OUTPUT><DEPARTMENT DepartmentID='' Department_Name='' ManagerName='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHDEPARTMENT_OUTPUT>"

        Const StrADDDEPARTMENT_OUTPUT = "<MS_UPDATEDEPARTMENT_INPUT><DEPARTMENT Action='' DepartmentID='' Department_Name='' ManagerID='' /></MS_UPDATEDEPARTMENT_INPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETEDEPARTMENT_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETEDEPARTMENT_OUTPUT>"
        Const strUPDATE_OUTPUT = "<MS_UPDATEDEPARTMENT_OUTPUT><DEPARTMENT Action='' DepartmentID='' Department_Name='' ManagerID='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEDEPARTMENT_OUTPUT>"
        Const ListManagerName_OUTPUT = "<MS_EMPLOYEE_OUTPUT><EMPLOYEE EmployeeID='' Employee_Name='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_EMPLOYEE_OUTPUT>"
        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add
            'Declare Output XML String
            '***********************************************************************
            'Purpose: 
            'Input:None
            'Output:Xml Document
            '<MS_UPDATEDEPARTMENT_INPUT>
            '<DEPARTMENT Action='' DepartmentID='' Department_Name='' ManagerID='' />
            '</MS_UPDATEDEPARTMENT_INPUT>
            '**************************************************************************
            Dim strMETHOD_NAME As String = "Add"
            Dim objXMLDoc As New XmlDocument
            Try
                objXMLDoc.LoadXml(StrADDDEPARTMENT_OUTPUT)
            Catch Exec As Exception
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objXMLDoc, "101", Exec.Message)
                Return objXMLDoc
            End Try
            Return objXMLDoc
        End Function
        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Delete
            '***********************************************************************
            'Purpose:This function deletes a department.
            'Input:XmlDocument
            '<MS_DELETEDEPARTMENT_INPUT>
            '<DepartmentID></DepartmentID>
            '</MS_DELETEDEPARTMENT_INPUT>

            'Output :
            '<MS_DELETEDEPARTMENT_OUTPUT>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '</Errors>
            '</MS_DELETEDEPARTMENT_OUTPUT>
            '************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Delete"
            Dim intDepartment_id As Integer
            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0
                intDepartment_id = DeleteDoc.DocumentElement.SelectSingleNode("DepartmentID").InnerText.Trim
                If Len(intDepartment_id) <= 0 Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record

                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_DEPARTMENT"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"
                    .Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.Int))
                    .Parameters("@DepartmentID").Value = intDepartment_id
                    objSqlCommand.Connection.Open()
                    intRecordsAffected = .ExecuteNonQuery()
                    objSqlCommand.Connection.Close()
                End With

                'Checking whether record is deleted successfully or not
                If intRecordsAffected > 0 Then
                    objDeleteDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "False"
                    Return (objDeleteDocOutput)
                Else
                    objDeleteDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "True"
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
            '<MS_SEARCHEPARTMENT_INPUT>
            '<Department_Name></Department_Name>
            '<ManagerName></ManagerName>
            '</MS_SEARCHEPARTMENT_INPUT>

            'Output :
            '<MS_SEARCHDEPARTMENT_OUTPUT>
            '	<DEPARTMENT DepartmentID='' Department_Name='' ManagerName='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_SEARCHDEPARTMENT_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strDEPT_NAME As String
            Dim strMGR_NAME As String

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)
            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
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

                strDEPT_NAME = (SearchDoc.DocumentElement.SelectSingleNode("Department_Name").InnerText.Trim())
                strMGR_NAME = (SearchDoc.DocumentElement.SelectSingleNode("ManagerName").InnerText.Trim())
                '--------Retrieving & Checking Details from Input XMLDocument ------End

                '[UP_SER_MS_DEPARTMENT]


                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SER_MS_DEPARTMENT"
                    .Connection = objSqlConnection

                    .Parameters.Add("@DEPARTMENT_NAME", SqlDbType.VarChar, 40)
                    .Parameters("@DEPARTMENT_NAME").Value = strDEPT_NAME
                    .Parameters.Add("@MANAGER_NAME", SqlDbType.VarChar, 40)
                    .Parameters("@MANAGER_NAME").Value = strMGR_NAME
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
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("DEPARTMENT")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)

                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("DepartmentID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DEPARTMENTID")))
                    objAptNodeClone.Attributes("Department_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DEPARTMENT_NAME")))
                    objAptNodeClone.Attributes("ManagerName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MANAGER_NAME")))
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)

                Loop
                objSqlReader.Close()
                If (blnRecordFound = False) Then
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
            '<MS_UPDATEDEPARTMENT_INPUT>
            '<DEPARTMENT Action='' DepartmentID='' Department_Name='' ManagerID='' />
            '</MS_UPDATEDEPARTMENT_INPUT>

            'Output :
            '<MS_UPDATEDEPARTMENT_OUTPUT>
            '	<DEPARTMENT Action='' DepartmentID='' Department_Name='' ManagerID='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_UPDATEDEPARTMENT_OUTPUT>
            '************************************************************************
            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objUpdateDocOutput As New XmlDocument
            Dim int_DepartmentId As Integer
            Dim str_DEPARTMENTNAME As String
            Dim int_ManagerId As String = ""
            Dim intRecordsAffected As Integer


            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)
                'NANAK
                With objUpdateDocOutput.DocumentElement.SelectSingleNode("DEPARTMENT")
                    .Attributes("Action").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DEPARTMENT").Attributes("Action").InnerText
                    .Attributes("DepartmentID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DEPARTMENT").Attributes("DepartmentID").InnerText
                    .Attributes("Department_Name").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DEPARTMENT").Attributes("Department_Name").InnerText
                    .Attributes("ManagerID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DEPARTMENT").Attributes("ManagerID").InnerText
                End With

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("DEPARTMENT")
                    strAction = ((.Attributes("Action").InnerText).Trim).ToString
                    If ((.Attributes("DepartmentID").InnerText).Trim = "") Then
                        int_DepartmentId = 0
                    Else
                        int_DepartmentId = ((.Attributes("DepartmentID").InnerText).Trim)
                    End If

                    str_DEPARTMENTNAME = ((.Attributes("Department_Name").InnerText).Trim).ToString
                    If ((.Attributes("ManagerID").InnerText).Trim) = "" Then
                        int_ManagerId = vbNullString
                    Else
                        int_ManagerId = ((.Attributes("ManagerID").InnerText).Trim)
                    End If

                    If strAction = "I" Or strAction = "U" Then
                        If str_DEPARTMENTNAME = "" Then
                            Throw (New AAMSException("Department Name can't be blank."))
                        End If
                    Else
                        Throw (New AAMSException("Invalid Action Code."))
                    End If

                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_DEPARTMENT"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction

                    .Parameters.Add(New SqlParameter("@DepartmentID", Data.SqlDbType.Int))
                    If int_DepartmentId = 0 Then
                        .Parameters("@DepartmentID").Value = DBNull.Value
                    Else
                        .Parameters("@DepartmentID").Value = int_DepartmentId
                    End If


                    .Parameters.Add(New SqlParameter("@Department_Name", SqlDbType.VarChar, 40))
                    .Parameters("@Department_Name").Value = str_DEPARTMENTNAME

                    .Parameters.Add(New SqlParameter("@ManagerID", SqlDbType.VarChar, 40))
                    .Parameters("@ManagerID").Value = int_ManagerId



                    .Parameters.Add(New SqlParameter("@RETUNID", SqlDbType.Int))
                    .Parameters("@RETUNID").Direction = ParameterDirection.Output

                    .Parameters("@RETUNID").Value = 0

                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED

                    .Connection.Open()

                    intRecordsAffected = .ExecuteNonQuery()

                    If UCase(strAction) = "I" Then
                        intRetId = .Parameters("@RETUNID").Value
                        If intRetId = 0 Then
                            Throw (New AAMSException("Unable to Insert!"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                        End If
                        With objUpdateDocOutput.DocumentElement.SelectSingleNode("DEPARTMENT")
                            .Attributes("DepartmentID").InnerText = intRetId
                        End With
                    ElseIf UCase(strAction) = "U" Then
                        intRetId = .Parameters("@RETUNID").Value
                        If intRetId = 0 Then
                            Throw (New AAMSException("Unable to update!"))
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
                        bzShared.FillErrorStatus(objUpdateDocOutput, "101", "Unable to Insert!")
                    Else
                        bzShared.FillErrorStatus(objUpdateDocOutput, "101", "Unable to Update!")
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
            '<MS_VIEWDEPARTMENT_INPUT>
            '<DepartmentID></DepartmentID>
            '</MS_VIEWDEPARTMENT_INPUT>

            'Output :  
            ' <MS_VIEWDEPARTMENT_OUTPUT>
            '<DEPARTMENT DepartmentID='' Department_Name='' ManagerID='' ManagerName='' />
            '<Errors Status=''>
            '<Error Code='' Description='' />
            '</Errors>
            '</MS_VIEWDEPARTMENT_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim intDepartment_ID As Integer
            Dim blnRecordFound As Boolean
            Const strMETHOD_NAME As String = "View"
            Try
                objOutputXml.LoadXml(srtVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                intDepartment_ID = IndexDoc.DocumentElement.SelectSingleNode("DepartmentID").InnerText.Trim
                If Len(intDepartment_ID) <= 0 Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_DEPARTMENT"
                    .Connection = objSqlConnection

                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"

                    .Parameters.Add("@DepartmentID", SqlDbType.Int)
                    .Parameters("@DepartmentID").Value = intDepartment_ID

                    'ManagerID
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()

                    'Output :
                    '<MS_VIEWDEPARTMENT_OUTPUT>
                    '	<DEPARTMENT DepartmentID='' Department_Name=''   ManagerID='' ManagerName='' />
                    '<Errors Status=''><Error Code='' Description='' /></Errors>
                    '</MS_VIEWDEPARTMENT_OUTPUT>

                    With objOutputXml.DocumentElement.SelectSingleNode("DEPARTMENT")
                        .Attributes("DepartmentID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DepartmentID")))
                        .Attributes("Department_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Department_Name")))
                        .Attributes("ManagerID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmployeeID")))
                        .Attributes("ManagerName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Employee_Name")))
                    End With
                    blnRecordFound = True
                Loop

                If (blnRecordFound = False) Then
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

        Public Function List() As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the airport record, based on the given field value
            'Input  : 
            'Output :  
            '<MS_LISTDEPARTMENT_OUTPUT>
            '<DEPARTMENT DepartmentID='' Department_Name='' , ManagerId='' />
            '<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_LISTDEPARTMENT_OUTPUT>


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
                    .CommandText = "UP_LST_MS_DEPARTMENT"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("DEPARTMENT")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("DepartmentID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DEPARTMENTID")))
                    objAptNodeClone.Attributes("Department_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DEPARTMENT_NAME")))
                    objAptNodeClone.Attributes("ManagerId").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MANAGERID")))

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)

                Loop

                If (blnRecordFound = False) Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
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
        ''''################################################
        ''''    code shifted to    :     bzEmployee
        ''''################################################
        '' ''Public Function ListManagerName() As System.Xml.XmlDocument
        '' ''    '***********************************************************************
        '' ''    'Purpose: To list out the airport record, based on the given field value
        '' ''    'Input  : 
        '' ''    'Output :  
        '' ''    '<MS_EMPLOYEE_OUTPUT>
        '' ''    '   <EMPLOYEE  EmployeeID="" Employee_Name=""/>
        '' ''    '  <Errors Status="False">
        '' ''    '     <Error Code="" Description=""/>
        '' ''    ' </Errors>
        '' ''    '</MS_EMPLOYEE_OUTPUT> 

        '' ''    '************************************************************************

        '' ''    Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
        '' ''    Dim objSqlCommand As New SqlCommand
        '' ''    Dim objOutputXml As New XmlDocument
        '' ''    Dim objAptNode, objAptNodeClone As XmlNode
        '' ''    Dim objSqlReader As SqlDataReader
        '' ''    Dim blnRecordFound As Boolean
        '' ''    Dim strMETHOD_NAME As String = "ListManagerName"

        '' ''    objOutputXml.LoadXml(ListManagerName_OUTPUT)

        '' ''    Try
        '' ''        With objSqlCommand
        '' ''            .CommandType = CommandType.StoredProcedure
        '' ''            .CommandText = "UP_LST_MS_MANAGER"
        '' ''            .Connection = objSqlConnection
        '' ''        End With

        '' ''        'retrieving the records according to the List Criteria
        '' ''        objSqlCommand.Connection.Open()
        '' ''        objSqlReader = objSqlCommand.ExecuteReader()

        '' ''        'Reading and Appending records into the Output XMLDocument
        '' ''        objAptNode = objOutputXml.DocumentElement.SelectSingleNode("EMPLOYEE")
        '' ''        objAptNodeClone = objAptNode.CloneNode(True)
        '' ''        objOutputXml.DocumentElement.RemoveChild(objAptNode)
        '' ''        Do While objSqlReader.Read
        '' ''            blnRecordFound = True
        '' ''            objAptNodeClone.Attributes("EmployeeID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMPLOYEEID")))
        '' ''            objAptNodeClone.Attributes("Employee_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NAME")))
        '' ''            objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
        '' ''            objAptNodeClone = objAptNode.CloneNode(True)

        '' ''        Loop

        '' ''        If (blnRecordFound = False) Then
        '' ''            bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
        '' ''        Else
        '' ''            objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
        '' ''        End If
        '' ''    Catch Exec As AAMSException
        '' ''        'CATCHING AAMS EXCEPTIONS
        '' ''        bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
        '' ''        Return objOutputXml
        '' ''    Catch exec As Exception
        '' ''        'msgbox(exec.ToString)
        '' ''        bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
        '' ''        bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
        '' ''        Return objOutputXml
        '' ''    Finally
        '' ''        If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
        '' ''        objSqlCommand.Dispose()
        '' ''    End Try
        '' ''    Return objOutputXml

        '' ''End Function
    End Class
End Namespace
