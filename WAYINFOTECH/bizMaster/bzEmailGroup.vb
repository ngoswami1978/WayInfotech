'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Tapan $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizMaster/bzEmailGroup.vb $
'$Workfile: bzEmailGroup.vb $
'$Revision: 18 $
'$Archive: /AAMS/Components/bizMaster/bzEmailGroup.vb $
'$Modtime: 8/12/08 6:42p $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizMaster

    Public Class bzEmailGroup
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzEmailGroup"
        Const gstrList_EMAILID_OUTPUT = "<MS_LISTEMPLOYEE_EMAIL_OUTPUT><AOFFICE_DETAILS ID='' AOFFICE=''><EMPLOYEE_DETAIL EMPLOYEEID='' EMPLOYEE_NAME ='' EMAIL=''/></AOFFICE_DETAILS><Errors Status=''><Error Code='' Description=''/></Errors></MS_LISTEMPLOYEE_EMAIL_OUTPUT>"
        Const gstrSearch_output = "<MS_SEARCHEMAILGROUP_OUTPUT><GROUP_DETAIL GroupType='' GroupName='' GROUPID='' EmailID=''/><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description=''/></Errors></MS_SEARCHEMAILGROUP_OUTPUT>"
        Const gstrSearchEMIAL_output = "<MS_GETEMAIL_OUTPUT><EMAILDETAIL Email=''/><Errors Status=''><Error Code='' Description=''/></Errors></MS_GETEMAIL_OUTPUT>"
        Const strMS_UPDATEEMAILGROUP_OUTPUT = "<MS_UPDATEEMAILGROUP_OUTPUT>	<GROUP_DETAIL ACTION='' GroupID='' GroupName='' EmployeeID='' GroupMNC='' GroupISP='' GroupTraining='' GroupAoffice='' Aoffice='' TrainingAoffice=''>		<EMAIL_DETAIL Email=''/>	</GROUP_DETAIL>	<Errors Status='FALSE'>		<Error Code='' Description=''/>	</Errors></MS_UPDATEEMAILGROUP_OUTPUT>"
        Const srtVIEW_OUTPUT = "<MS_VIEWEMAILGROUP_OUTPUT>	<GROUP_DETAIL ACTION='' GroupID='' GroupName='' EmployeeID='' GroupMNC='' GroupISP='' GroupTraining='' GroupAoffice='' Aoffice='' TrainingAoffice=''>	<EMAIL_DETAILS Email=''/>	</GROUP_DETAIL>	<Errors Status=''>		<Error Code='' Description=''/>	</Errors></MS_VIEWEMAILGROUP_OUTPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETEEMAILGROUP_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETEEMAILGROUP_OUTPUT>"

        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add

        End Function

        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Delete
            '***********************************************************************
            'Purpose:This function deletes a department.
            'Input:XmlDocument
            '<MS_DELETEEMAILGROUP_INPUT>
            '<GroupID></GroupID>
            '</MS_DELETEEMAILGROUP_INPUT>

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
            Dim intGroupID As Integer
            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0
                intGroupID = DeleteDoc.DocumentElement.SelectSingleNode("GroupID").InnerText.Trim
                If Len(intGroupID) <= 0 Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record

                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_EMAIL_GROUP"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"
                    .Parameters.Add(New SqlParameter("@GroupID", SqlDbType.Int))
                    .Parameters("@GroupID").Value = intGroupID
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
            '<MS_SEARCHEMAILGROUP_INPUT>
            '<GROUPDETAIL GroupName="" EmployeeID="" GroupMNC='' GroupISP='' GroupTraining='' GroupAoffice='' GroupID='' DepartmentID=''/>
            '</MS_SEARCHEMAILGROUP_INPUT>

            'Output :
            '<MS_SEARCHEMAILGROUP_OUTPUT>
            '<GROUP_DETAIL GroupType='' GroupName='' GROUPID='' EmailID=''/>
            '<Errors Status="">
            '	<Error Code="" Description=""/>
            '</Errors>
            '</MS_SEARCHEMAILGROUP_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strGroupName As String
            Dim GroupMNC, GroupISP, GroupTraining, GroupAoffice As Integer

            Dim intDepartmentID, intGroupID As Integer
            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(gstrSearch_output)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start

                strGroupName = SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupName").InnerText.Trim()
                GroupMNC = Val(SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupMNC").InnerText.Trim())
                GroupISP = Val(SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupISP").InnerText.Trim())
                GroupTraining = Val(SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupTraining").InnerText.Trim())
                GroupAoffice = Val(SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupAoffice").InnerText.Trim())

                If SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("DepartmentID") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("DepartmentID").InnerText.Trim() <> "" Then
                        intDepartmentID = SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("DepartmentID").InnerText.Trim()
                    End If

                End If

                If SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupID") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupID").InnerText.Trim() <> "" Then
                        intGroupID = SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupID").InnerText.Trim()
                    End If
                End If

                'Paging Section    
                If SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("PAGE_NO") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("PAGE_NO").InnerText.Trim() <> "" Then
                        intPageNo = SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("PAGE_NO").InnerText.Trim()
                    End If
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("PAGE_SIZE") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("PAGE_SIZE").InnerText.Trim() <> "" Then
                        intPageSize = SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("PAGE_SIZE").InnerText.Trim()
                    End If
                End If


                If SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("SORT_BY") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("SORT_BY").InnerText.Trim() <> "" Then
                        strSortBy = SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("SORT_BY").InnerText.Trim()
                    End If
                End If


                If SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("DESC") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("DESC").InnerText.Trim().ToUpper = "TRUE" Then
                        blnDesc = True
                    Else
                        blnDesc = False
                    End If
                End If
                '--------Retrieving & Checking Details from Input XMLDocument ------End
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SER_MS_EMAIL_GROUP"
                    .Connection = objSqlConnection

                    .Parameters.Add("@GROUPNAME", SqlDbType.VarChar, 25)
                    .Parameters("@GROUPNAME").Value = strGroupName

                    .Parameters.Add("@GroupMNC", SqlDbType.Bit)
                    .Parameters.Add("@GroupISP", SqlDbType.Bit)
                    .Parameters.Add("@GroupTraining", SqlDbType.Bit)
                    .Parameters.Add("@GroupAoffice", SqlDbType.Bit)

                    .Parameters.Add("@GROUPID", SqlDbType.Int)
                    If intGroupID = 0 Then
                        .Parameters("@GROUPID").Value = DBNull.Value
                    Else
                        .Parameters("@GROUPID").Value = intGroupID
                    End If

                    .Parameters.Add("@DEPTID", SqlDbType.Int)
                    If intDepartmentID = 0 Then
                        .Parameters("@DEPTID").Value = DBNull.Value
                    Else
                        .Parameters("@DEPTID").Value = intDepartmentID
                    End If
                    'If GroupMNC = 0 Then
                    .Parameters("@GroupMNC").Value = CBool(GroupMNC)
                    .Parameters("@GroupISP").Value = CBool(GroupISP)
                    .Parameters("@GroupTraining").Value = CBool(GroupTraining)
                    .Parameters("@GroupAoffice").Value = CBool(GroupAoffice)
                    'Else
                    '    .Parameters("@GroupMNC").Value = True
                    'End If

                    '.Parameters.Add("@GroupISP", SqlDbType.Bit)
                    'If GroupISP = 0 Then
                    '    .Parameters("@GroupISP").Value = False
                    'Else
                    '    .Parameters("@GroupISP").Value = True
                    'End If

                    '.Parameters.Add("@GroupTraining", SqlDbType.Bit)
                    'If GroupTraining = 0 Then
                    '    .Parameters("@GroupTraining").Value = False
                    'Else
                    '    .Parameters("@GroupTraining").Value = True
                    'End If

                    '.Parameters.Add("@GroupAoffice", SqlDbType.Bit)
                    'If GroupAoffice = 0 Then
                    '    .Parameters("@GroupAoffice").Value = False
                    'Else
                    '    .Parameters("@GroupAoffice").Value = True
                    'End If

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
                If blnRecordFound = False Then
                    objAptNode = objOutputXml.DocumentElement.SelectSingleNode("GROUP_DETAIL")
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.RemoveChild(objAptNode)
                    Do While objSqlReader.Read()
                        objAptNodeClone.Attributes("GroupType").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("GroupType")) & "")
                        objAptNodeClone.Attributes("GroupName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("GroupName")) & "")
                        objAptNodeClone.Attributes("GROUPID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("GROUPID")))
                        objAptNodeClone.Attributes("EmailID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailID")))
                        objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                        objAptNodeClone = objAptNode.CloneNode(True)
                    Loop
                End If
                objSqlReader.Close()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE" Then
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
            'This function has been created by Avinash kaushik on 18.12.2007
            'Purpose: To insert/update Email group
            'Input  : xmlDocument
            ' <MS_UPDATEEMAILGROUP_INPUT>
            '<GROUP_DETAIL ACTION="" GroupID="" GroupName="" EmployeeID="" GroupMNC='' GroupISP='' GroupTraining='' GroupAoffice='' Aoffice='' TrainingAoffice=''>
            '<EMAIL_DETAIL Email=''/>
            '</GROUP_DETAIL>
            '</MS_UPDATEEMAILGROUP_INPUT>

            'Output : xmlDocument
            '
            '<MS_UPDATEEMAILGROUP_OUTPUT>
            '	<GROUP_DETAIL ACTION="" GroupID="" GroupName="" EmployeeID="" GroupMNC="" GroupISP="" GroupTraining="" GROUPAOFFICE="" Aoffice="" TrainingAoffice="">
            '		<EMAIL_DETAIL Email=""/>
            '	</GROUP_DETAIL>
            '	<Errors Status="FALSE">
            '		<Error Code="" Description=""/>
            '	</Errors>
            '</MS_UPDATEEMAILGROUP_OUTPUT>
            '************************************************************************
            Dim intRetId, GroupID, EmpID, countID As Integer
            Dim gmnc, gisp, gtrg, goff As Boolean
            Dim strAction, Aoff, Troff, gname As String
            Const strMETHOD_NAME As String = "Update"
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objXMLDocOutput As New XmlDocument
            Dim UpdateTransaction As SqlTransaction
            'Dim objAptNode, objAptNodeClone As XmlNode
            Try
                objXMLDocOutput.LoadXml(strMS_UPDATEEMAILGROUP_OUTPUT)
                With objXMLDocOutput.DocumentElement.SelectSingleNode("GROUP_DETAIL")
                    strAction = UpdateDoc.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("ACTION").InnerText.Trim.ToUpper
                    If strAction <> "I" And strAction <> "U" Then
                        Throw New Exception("Invalid Action Code !")
                    End If
                    .Attributes("GroupName").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupName").InnerText
                    gname = UpdateDoc.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupName").InnerText
                    If strAction = "U" AndAlso Val(UpdateDoc.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupID").InnerText) <= 0 Then
                        Throw New Exception("Invalid parameter group Id !")
                    End If
                    .Attributes("GroupID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupID").InnerText
                    GroupID = Val(UpdateDoc.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupID").InnerText)
                    EmpID = Val(UpdateDoc.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("EmployeeID").InnerText)
                    If EmpID = 0 Then
                        Throw New Exception("Enter Employee ID!")
                    End If
                    .Attributes("EmployeeID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("EmployeeID").InnerText
                    .Attributes("GroupMNC").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupMNC").InnerText
                    gmnc = Val(UpdateDoc.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupMNC").InnerText)
                    .Attributes("GroupISP").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupISP").InnerText
                    gisp = Val(UpdateDoc.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupISP").InnerText)
                    .Attributes("GroupTraining").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupTraining").InnerText
                    gtrg = Val(UpdateDoc.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupTraining").InnerText)
                    .Attributes("GroupAoffice").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupAoffice").InnerText
                    goff = Val(UpdateDoc.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupAoffice").InnerText)

                    .Attributes("Aoffice").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("Aoffice").InnerText
                    Aoff = UpdateDoc.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("Aoffice").InnerText
                    .Attributes("TrainingAoffice").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("TrainingAoffice").InnerText
                    Troff = UpdateDoc.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("TrainingAoffice").InnerText

                    If gtrg = True AndAlso Troff = "" Then
                        Throw New Exception("Select atleast one Office !")
                    End If
                    If goff = True AndAlso Aoff = "" Then
                        Throw New Exception("Select atleast one Office !")
                    End If
                    countID = UpdateDoc.DocumentElement.SelectNodes("GROUP_DETAIL/EMAIL_DETAILS").Count
                    If countID <= 0 Then
                        Throw New Exception("Enter atleast one Email ID!")
                    End If

                    intRetId = 0
                    'Inserting the output data into output xml ---- End

                    With objSqlCommand
                        .Connection = objSqlConnection
                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "UP_SRO_EMAIL_GROUP"

                        .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                        .Parameters.Add(New SqlParameter("@GroupID", SqlDbType.Int))
                        .Parameters.Add(New SqlParameter("@GroupName", SqlDbType.VarChar, 25))
                        .Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.Int))
                        .Parameters.Add(New SqlParameter("@GroupMNC", SqlDbType.Bit))
                        .Parameters.Add(New SqlParameter("@GroupISP", SqlDbType.Bit))
                        .Parameters.Add(New SqlParameter("@GroupTraining", SqlDbType.Bit))
                        .Parameters.Add(New SqlParameter("@GroupAoffice", SqlDbType.Bit))
                        .Parameters.Add(New SqlParameter("@Aoffice", SqlDbType.Char, 3))
                        .Parameters.Add(New SqlParameter("@TrainingAoffice", SqlDbType.Char, 3))
                        .Parameters.Add(New SqlParameter("@RETUNID", SqlDbType.Int))
                        .Parameters.Add(New SqlParameter("@ERRMSG", SqlDbType.VarChar, 100))
                        .Parameters("@ERRMSG").Direction = ParameterDirection.Output
                        .Parameters("@RETUNID").Direction = ParameterDirection.Output
                        .Parameters("@ACTION").Value = strAction
                        .Parameters("@GroupID").Value = GroupID
                        .Parameters("@GroupName").Value = gname
                        .Parameters("@EmployeeID").Value = EmpID
                        .Parameters("@GroupMNC").Value = CBool(gmnc)
                        .Parameters("@GroupISP").Value = CBool(gisp)
                        .Parameters("@GroupTraining").Value = CBool(gtrg)
                        .Parameters("@GroupAoffice").Value = CBool(goff)
                        .Parameters("@Aoffice").Value = Aoff
                        .Parameters("@TrainingAoffice").Value = Troff
                        .Parameters("@RETUNID").Value = 0
                        .Connection.Open()
                        UpdateTransaction = .Connection.BeginTransaction()
                        .Transaction = UpdateTransaction
                        .ExecuteNonQuery()
                        intRetId = .Parameters("@RETUNID").Value
                        If strAction = "I" Then
                            GroupID = .Parameters("@RETUNID").Value
                        Else
                            GroupID = GroupID
                        End If

                        If UCase(strAction) = "U" Then
                            If intRetId <= 0 Then
                                If intRetId = -10 Then
                                    Throw (New AAMSException(.Parameters("@ERRMSG").Value & ""))
                                Else
                                    Throw (New AAMSException("Unable to update!"))
                                End If
                            End If
                        Else
                            If intRetId <= 0 Then
                                If intRetId = -10 Then
                                    Throw (New AAMSException(.Parameters("@ERRMSG").Value & ""))
                                Else
                                    Throw (New AAMSException("Unable to insert!"))
                                End If
                            End If
                        End If

                        If countID > 0 Then
                            For Each N2 As XmlNode In UpdateDoc.DocumentElement.SelectNodes("GROUP_DETAIL/EMAIL_DETAILS")
                                .Parameters.Clear()
                                .Parameters.Add("@ACTION", SqlDbType.Char, 1)
                                .Parameters.Add("@EMAIL", SqlDbType.VarChar, 100)
                                .Parameters.Add("@GroupID", SqlDbType.Int)
                                .Parameters.Add("@RETUNID", SqlDbType.Int)
                                .Parameters.Add(New SqlParameter("@ERRMSG", SqlDbType.VarChar, 100))

                                .Parameters("@ACTION").Value = "E"
                                .Parameters("@EMAIL").Value = N2.Attributes("Email").InnerText
                                .Parameters("@GroupID").Value = GroupID
                                .Parameters("@ERRMSG").Direction = ParameterDirection.InputOutput
                                .Parameters("@RETUNID").Direction = ParameterDirection.InputOutput
                                .Parameters("@RETUNID").Value = 0
                                .Parameters("@ERRMSG").Value = ""
                                .ExecuteNonQuery()
                                intRetId = .Parameters("@RETUNID").Value
                                If UCase(strAction) = "U" Then
                                    If intRetId <= 0 Then
                                        Throw (New AAMSException("Unable to update!"))
                                    End If
                                Else
                                    If intRetId = -10 Then
                                        Throw (New AAMSException(.Parameters("@ERRMSG").Value & ""))
                                    ElseIf intRetId <= 0 Then
                                        Throw (New AAMSException("Unable to insert!"))
                                    End If
                                End If
                            Next
                        End If
                        objXMLDocOutput.SelectSingleNode("MS_UPDATEEMAILGROUP_OUTPUT/Errors").Attributes("Status").InnerText = "FALSE"
                        objXMLDocOutput.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupID").InnerText = GroupID
                        .Transaction.Commit()
                    End With
                End With
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objXMLDocOutput, "101", Exec.Message)
                If Not (UpdateTransaction Is Nothing) AndAlso objSqlConnection.State <> ConnectionState.Closed Then
                    UpdateTransaction.Rollback()
                End If
            Catch Exec As Exception
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objXMLDocOutput, "101", Exec.Message)
                If Not (UpdateTransaction Is Nothing) AndAlso objSqlConnection.State <> ConnectionState.Closed Then
                    UpdateTransaction.Rollback()
                End If
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objXMLDocOutput
        End Function

        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.View

            '***********************************************************************
            'Purpose:This function gives details of Email Group.
            'Input  :
            '<MS_VIEWEMAILGROUP_INPUT>
            '<GroupID></GroupID>
            '</MS_VIEWEMAILGROUP_INPUT>

            'Output :
            '<MS_VIEWEMAILGROUP_OUTPUT>
            '<GROUP_DETAIL ACTION='' GroupID='' GroupName='' EmployeeID='' GroupMNC='' GroupISP='' GroupTraining='' GroupAoffice='' Aoffice='' TrainingAoffice=''>
            '<EMAIL_DETAIL Email=''/>
            '</GROUP_DETAIL>
            '<Errors Status=''>
            '<Error Code='' Description=''/>
            '</Errors>
            '</MS_VIEWEMAILGROUP_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim GroupID As Integer
            Dim node1, node2 As XmlNode

            Const strMETHOD_NAME As String = "View"
            Dim blnRecordFound As Boolean
            Try
                objOutputXml.LoadXml(srtVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                GroupID = Val(IndexDoc.DocumentElement.SelectSingleNode("GroupID").InnerText)
                If GroupID = 0 Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_EMAIL_GROUP"
                    .Connection = objSqlConnection
                    .Parameters.Add("@GroupID", SqlDbType.Int)
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@GroupID").Value = GroupID
                    .Parameters("@ACTION").Value = "V"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("GROUP_DETAIL")
                        .Attributes("GroupID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("GroupID")) & "")
                        .Attributes("GroupName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("GroupName")) & "")
                        .Attributes("EmployeeID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmployeeID")) & "")
                        .Attributes("GroupMNC").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("GroupMNC")) & "")
                        .Attributes("GroupISP").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("GroupISP")) & "")
                        .Attributes("GroupTraining").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("GroupTraining")) & "")
                        .Attributes("GroupAoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("GroupAoffice")) & "")
                        .Attributes("Aoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Aoffice")) & "")
                        .Attributes("TrainingAoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("TrainingAoffice")) & "")
                    End With
                    blnRecordFound = True
                    If objSqlReader.NextResult = True Then
                        node1 = objOutputXml.DocumentElement.SelectSingleNode("GROUP_DETAIL/EMAIL_DETAILS")
                        node2 = node1.CloneNode(True)
                        'objOutputXml.DocumentElement.SelectSingleNode("GROUP_DETAIL").RemoveChild(node1)
                        Do While objSqlReader.Read()
                            node2.Attributes("Email").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Email")) & "")
                            objOutputXml.DocumentElement.SelectSingleNode("GROUP_DETAIL").AppendChild(node2)
                            node2 = node1.CloneNode(True)
                        Loop
                    End If
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

        Public Function GetEmailIDs() As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: List out City 
            'Input  : 
            'Output :  
            '<MS_LISTEMPLOYEE_EMAIL_OUTPUT>
            '<AOFFICE_DETAILS ID='' AOFFICE=''>
            '<EMPLOYEE_DETAIL EMPLOYEEID='' EMPLOYEE_NAME ='' EMAIL=''/>
            '</AOFFICE_DETAILS>
            '<Errors Status=""><Error Code="" Description=""/></Errors>
            '</MS_LISTEMPLOYEE_EMAIL_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone, objAptNode2, objAptNodeClone2 As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strOffice1, strOffice2 As String
            Dim intId As Integer = 0
            Dim intId2 As Integer = 1
            Dim strMETHOD_NAME As String = "ListEmailID"

            objOutputXml.LoadXml(gstrList_EMAILID_OUTPUT)

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LST_MS_EMAILID"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                strOffice1 = ""
                strOffice2 = ""
                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AOFFICE_DETAILS")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    If intId = 0 Then
                        strOffice1 = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AOFFICE")))
                        objAptNodeClone.Attributes("AOFFICE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AOFFICE")))
                        objAptNodeClone.Attributes("ID").InnerText = intId2
                        strOffice2 = ""
                        objAptNode2 = objAptNodeClone.SelectSingleNode("EMPLOYEE_DETAIL")
                        objAptNodeClone2 = objAptNode2.CloneNode(True)
                        objAptNodeClone.RemoveChild(objAptNode2)

                        objAptNodeClone2.Attributes("EMPLOYEEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMPLOYEEID")))
                        objAptNodeClone2.Attributes("EMPLOYEE_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMPLOYEE_NAME")))
                        objAptNodeClone2.Attributes("EMAIL").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMAIL")))
                        objAptNodeClone.AppendChild(objAptNodeClone2)
                        objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    End If
                    If intId >= 1 Then
                        strOffice2 = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AOFFICE")))
                        If strOffice1 = strOffice2 Then
                            objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AOFFICE_DETAILS[@AOFFICE='" & strOffice2 & "']")
                            objAptNodeClone = objAptNode.CloneNode(True)
                            objOutputXml.DocumentElement.RemoveChild(objAptNode)
                            objAptNode2 = objAptNodeClone.SelectSingleNode("EMPLOYEE_DETAIL")
                            objAptNodeClone2 = objAptNode2.CloneNode(True)
                            'objAptNodeClone.RemoveChild(objAptNode2)
                            objAptNodeClone2.Attributes("EMPLOYEEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMPLOYEEID")))
                            objAptNodeClone2.Attributes("EMPLOYEE_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMPLOYEE_NAME")))
                            objAptNodeClone2.Attributes("EMAIL").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMAIL")))
                            objAptNodeClone.AppendChild(objAptNodeClone2)
                            objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                        Else
                            objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AOFFICE_DETAILS")
                            objAptNodeClone = objAptNode.CloneNode(True)
                            objAptNodeClone.Attributes("AOFFICE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AOFFICE")))
                            intId2 = intId2 + 1
                            objAptNodeClone.Attributes("ID").InnerText = intId2
                            objAptNode2 = objAptNodeClone.SelectSingleNode("EMPLOYEE_DETAIL")
                            objAptNodeClone2 = objAptNode2.CloneNode(True)
                            Dim nodelist3 As XmlNodeList
                            Dim node2 As XmlNode
                            nodelist3 = objAptNodeClone.SelectNodes("EMPLOYEE_DETAIL")
                            For Each node2 In nodelist3
                                objAptNodeClone.RemoveChild(node2)
                            Next
                            objAptNodeClone2.Attributes("EMPLOYEEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMPLOYEEID")))
                            objAptNodeClone2.Attributes("EMPLOYEE_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMPLOYEE_NAME")))
                            objAptNodeClone2.Attributes("EMAIL").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMAIL")))
                            objAptNodeClone.AppendChild(objAptNodeClone2)
                            objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                        End If
                        strOffice1 = strOffice2
                    End If
                    intId = intId + 1
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
        Public Function GetEmailID2s(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function gives search results based on choosen search criterion.
            'Input  :- 
            '<MS_GETEMAIL_INPUT>
            '<GROUPDETAIL GroupMNC='' GroupISP='' GroupTraining='' GroupAoffice='' Aoffice='' TrainingAoffice=''/>
            '</MS_GETEMAIL_INPUT>
            'Output :
            '<MS_GETEMAIL_OUTPUT>
            '<EMAILDETAIL Email=''/>
            '<Errors Status="">
            '<Error Code="" Description=""/>
            '</Errors>
            '</MS_GETEMAIL_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim STRAoffice, STRTrainingAoffice As String
            Dim GroupMNC, GroupISP, GroupTraining, GroupAoffice As Integer

            Const strMETHOD_NAME As String = "Search email"
            objOutputXml.LoadXml(gstrSearchEMIAL_output)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start


                GroupMNC = Val(SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupMNC").InnerText.Trim())
                GroupISP = Val(SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupISP").InnerText.Trim())
                GroupTraining = Val(SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupTraining").InnerText.Trim())
                GroupAoffice = Val(SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupAoffice").InnerText.Trim())
                STRAoffice = SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("Aoffice").InnerText.Trim()
                STRTrainingAoffice = SearchDoc.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("TrainingAoffice").InnerText.Trim()

                '--------Retrieving & Checking Details from Input XMLDocument ------End
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SER_MS_EMAILID"
                    .Connection = objSqlConnection

                    

                    .Parameters.Add("@GroupMNC", SqlDbType.Bit)
                    .Parameters.Add("@GroupISP", SqlDbType.Bit)
                    .Parameters.Add("@GroupTraining", SqlDbType.Bit)
                    .Parameters.Add("@GroupAoffice", SqlDbType.Bit)
                    .Parameters.Add("@Aoffice", SqlDbType.Char, 3)
                    .Parameters.Add("@TrainingAoffice", SqlDbType.Char, 3)

                    'If GroupMNC = 0 Then
                    .Parameters("@GroupMNC").Value = CBool(GroupMNC)
                    .Parameters("@GroupISP").Value = CBool(GroupISP)
                    .Parameters("@GroupTraining").Value = CBool(GroupTraining)
                    .Parameters("@GroupAoffice").Value = CBool(GroupAoffice)
                    .Parameters("@Aoffice").Value = STRAoffice
                    .Parameters("@TrainingAoffice").Value = STRTrainingAoffice

                End With

                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                If objSqlReader.HasRows = True Then
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    blnRecordFound = False
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "TRUE"
                    Call bzShared.FillErrorStatus(objOutputXml, "101", "Record not found!")
                    blnRecordFound = True
                End If
                If blnRecordFound = False Then
                    objAptNode = objOutputXml.DocumentElement.SelectSingleNode("EMAILDETAIL")
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.RemoveChild(objAptNode)
                    Do While objSqlReader.Read()
                        objAptNodeClone.Attributes("Email").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Email")))
                        objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                        objAptNodeClone = objAptNode.CloneNode(True)
                    Loop
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