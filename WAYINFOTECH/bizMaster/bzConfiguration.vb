'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Tapan $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizMaster/bzConfiguration.vb $
'$Workfile: bzConfiguration.vb $
'$Revision: 5 $
'$Archive: /AAMS/Components/bizMaster/bzConfiguration.vb $
'$Modtime: 28/07/08 8:06p $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizMaster
    Public Class bzConfiguration
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzConfiguration"
        Const strSEARCH_OUTPUT = "<MS_SEARCHCONFIGRURABLE_OUTPUT><CONFIGRURABLE CCA_ID='' CCA_NAME='' FIELD_NAME='' FIELD_VALUE='' Remarks='' Active=''/><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description=''/></Errors></MS_SEARCHCONFIGRURABLE_OUTPUT>"
        Const strUPDATE_OUTPUT = "<MS_UPDATECONFIGRURABLE_OUTPUT><CONFIGRURABLE CCA_ID='' CCA_NAME='' FIELD_NAME='' FIELD_VALUE='' Remarks='' Active=''/><Errors Status=''><Error Code='' Description=''/></Errors></MS_UPDATECONFIGRURABLE_OUTPUT>"
        Const gstrList_OUTPUT = "<MS_LISTCONFIGRURABLE_OUTPUT><CONFIGRURABLE CCA_ID='' CCA_NAME=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTCONFIGRURABLE_OUTPUT>"
        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add
            '----DO NOT IMPLEMENT------'
        End Function
        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Delete
            '----DO NOT IMPLEMENT------'
        End Function
        Public Function Search(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Search
            'Input
            '<MS_SEARCHCONFIGRURABLE_INPUT>
            '	<CCA_ID></CCA_ID>
            '</MS_SEARCHCONFIGRURABLE_INPUT>

            'Output
            '<MS_SEARCHCONFIGRURABLE_OUTPUT>
            '	<CONFIGRURABLE CCA_ID='' CCA_NAME='' FIELD_NAME='' FIELD_VALUE='' Remarks='' Active=''/>
            '	<PAGE PAGE_COUNT='' TOTAL_ROWS=''/>
            '	<Errors Status=''>
            '		<Error Code='' Description=''/>
            '	</Errors>
            '</MS_SEARCHCONFIGRURABLE_OUTPUT>

            '**************************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strCCAID As String
            Dim blnRecordFound As Boolean

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean
            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)
            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                If (SearchDoc.DocumentElement.SelectSingleNode("CCA_ID").InnerText.Trim() = "") Then
                    strCCAID = vbNullString
                Else
                    strCCAID = (SearchDoc.DocumentElement.SelectSingleNode("CCA_ID").InnerText.Trim())
                End If
                'Paging Section    
                If SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim() <> "" Then
                        intPageNo = SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim()
                    End If
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim() <> "" Then
                        intPageSize = SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim()
                    End If
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim() <> "" Then
                        strSortBy = SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim()
                    End If
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("DESC").InnerText IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("DESC").InnerText.Trim().ToUpper = "TRUE" Then
                        blnDesc = True
                    Else
                        blnDesc = False
                    End If
                End If

                '--------Retrieving & Checking Details from Input XMLDocument ------End
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SER_MS_CONFIG"
                    .Connection = objSqlConnection

                    .Parameters.Add("@CCAID", SqlDbType.Int)
                    If (strCCAID = "") Then
                        .Parameters("@CCAID").Value = DBNull.Value
                    Else
                        .Parameters("@CCAID").Value = strCCAID
                    End If


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
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("CONFIGRURABLE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("CCA_ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CCA_ID")) & "")
                    objAptNodeClone.Attributes("CCA_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CCA_NAME")) & "")
                    objAptNodeClone.Attributes("FIELD_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FIELD_NAME")) & "")
                    objAptNodeClone.Attributes("FIELD_VALUE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FIELD_VALUE")) & "")
                    objAptNodeClone.Attributes("Remarks").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Remarks")) & "")
                    objAptNodeClone.Attributes("Active").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Active")) & "")
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
            'Input
            '<MS_UPDATECONFIGRURABLE_INPUT>
            '	<CONFIGRURABLE CCA_ID='' CCA_NAME='' FIELD_NAME='' FIELD_VALUE='' Remarks='' Active=''/>
            '	<Errors Status=''>
            '		<Error Code='' Description=''/>
            '	</Errors>
            '</MS_UPDATECONFIGRURABLE_INPUT>

            'Output
            '<MS_UPDATECONFIGRURABLE_OUTPUT>
            '	<CONFIGRURABLE CCA_ID='' CCA_NAME='' FIELD_NAME='' FIELD_VALUE='' Remarks='' Active=''/>
            '	<Errors Status=''>
            '		<Error Code='' Description=''/>
            '	</Errors>
            '</MS_UPDATECONFIGRURABLE_OUTPUT>

            '*****************************************************************************************
            Dim intRetId As Integer
            Dim intRecordsAffected As Int32
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objTran As SqlTransaction
            Dim objNode As XmlNode
            Dim objUpdateDoc As New XmlDocument

            Dim intCca_id As Integer
            Dim strCca_name As String
            Dim strField_name As String
            Dim strField_value As String
            Dim strRemarks As String
            Dim blnActive As Boolean


            Const strMETHOD_NAME As String = "Update"
            objUpdateDoc.LoadXml(strUPDATE_OUTPUT)
            Try
                objSqlConnection.Open()
                objTran = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_SRO_MS_CONFIG]"
                    .Connection = objSqlConnection
                    .Transaction = objTran
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters.Add("@CCA_ID", SqlDbType.Int)
                    .Parameters.Add("@CCA_NAME", SqlDbType.VarChar, 40)
                    .Parameters.Add("@FIELD_NAME", SqlDbType.VarChar, 40)
                    .Parameters.Add("@FIELD_VALUE", SqlDbType.VarChar, 1000)
                    .Parameters.Add("@REMARKS", SqlDbType.VarChar, 300)
                    .Parameters.Add("@ACTIVE", SqlDbType.VarChar, 50)
                    .Parameters.Add("@RETURNID", SqlDbType.Int)
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@ACTION").Value = "U"
                End With
                For Each objNode In UpdateDoc.DocumentElement.SelectNodes("CONFIGRURABLE")
                    If ((objNode.Attributes("CCA_ID").InnerText).Trim).ToString <> "" Then
                        intCca_id = ((objNode.Attributes("CCA_ID").InnerText).Trim).ToString
                    Else
                        intCca_id = 0
                    End If

                    If ((objNode.Attributes("CCA_NAME").InnerText).Trim).ToString <> "" Then
                        strCca_name = ((objNode.Attributes("CCA_NAME").InnerText).Trim).ToString
                    Else
                        strCca_name = vbNullString
                    End If

                    If ((objNode.Attributes("FIELD_NAME").InnerText).Trim).ToString <> "" Then
                        strField_name = ((objNode.Attributes("FIELD_NAME").InnerText).Trim).ToString
                    Else
                        strField_name = vbNullString
                    End If

                    If ((objNode.Attributes("FIELD_VALUE").InnerText).Trim).ToString <> "" Then
                        strField_value = ((objNode.Attributes("FIELD_VALUE").InnerText).Trim).ToString
                    Else
                        strField_value = vbNullString
                    End If

                    If ((objNode.Attributes("Remarks").InnerText).Trim).ToString <> "" Then
                        strRemarks = ((objNode.Attributes("Remarks").InnerText).Trim).ToString
                    Else
                        strRemarks = vbNullString
                    End If

                    If ((objNode.Attributes("Active").InnerText).Trim).ToString.ToUpper = "TRUE" Then
                        blnActive = True
                    Else
                        blnActive = False
                    End If

                    If intCca_id = 0 Then Throw (New AAMSException("Category can not be blank."))
                    If strField_name = "" Then Throw (New AAMSException("FieldName can not be blank."))
                    If strField_value = "" Then Throw (New AAMSException("FieldValue can not be blank."))
                    If strRemarks = "" Then Throw (New AAMSException("Remarks can not be blank."))

                    If (strField_name = "DEFAULT_HD_REQUEST_SEVERITY") Or (strField_name = "DEFAULT_HD_REQUEST_STATUS") Then
                        If InStr(strField_value, "|", CompareMethod.Text) = 0 Then
                            Throw (New AAMSException("Default HelpDesk Severity and Status should be '|' separated."))
                        End If
                    End If
                    If intCca_id = 0 Then
                        objSqlCommand.Parameters("@CCA_ID").Value = DBNull.Value
                    Else
                        objSqlCommand.Parameters("@CCA_ID").Value = intCca_id
                    End If
                    objSqlCommand.Parameters("@CCA_NAME").Value = strCca_name
                    objSqlCommand.Parameters("@FIELD_NAME").Value = strField_name
                    objSqlCommand.Parameters("@FIELD_VALUE").Value = strField_value
                    objSqlCommand.Parameters("@REMARKS").Value = strRemarks
                    If blnActive = True Then
                        objSqlCommand.Parameters("@ACTIVE").Value = 1
                    Else
                        objSqlCommand.Parameters("@ACTIVE").Value = 0
                    End If

                    objSqlCommand.Parameters("@RETURNID").Value = 0
                    intRecordsAffected = objSqlCommand.ExecuteNonQuery()
                    If objSqlCommand.Parameters("@RETURNID").Value <= 0 Then
                        Throw (New AAMSException("Unable to update!"))
                    End If
                Next
                objTran.Commit()
                objSqlConnection.Close()
                objUpdateDoc.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objUpdateDoc, "101", Exec.Message)
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                'Fill Error Status
                If objSqlConnection.State <> ConnectionState.Closed Then
                    If Not objTran Is Nothing Then
                        objTran.Rollback()
                    End If
                    objSqlConnection.Close()
                End If
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objUpdateDoc, "101", Exec.Message)
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objUpdateDoc
        End Function

        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.View
            '----DO NOT IMPLEMENT------'
        End Function
        Public Function List() As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: List out City 
            'Input  : 
            'Output :  

            '<MS_LISTCONFIGRURABLE_OUTPUT>
            '    <CONFIGRURABLE CCA_ID="" CCA_NAME=""/>
            '    <Errors Status=''>
            '        <Error Code='' Description='' />
            '    </Errors>
            '</MS_LISTCONFIGRURABLE_OUTPUT>
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
                    .CommandText = "[UP_LST_MS_CONFIG_CATEGORY]"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("CONFIGRURABLE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)

                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("CCA_ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CCA_ID")) & "")
                    objAptNodeClone.Attributes("CCA_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CCA_NAME")) & "")
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


