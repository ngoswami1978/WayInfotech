'Copyright notice: � 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Neeraj $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizTravelAgency/bzOrderStatus.vb $
'$Workfile: bzOrderStatus.vb $
'$Revision: 11 $
'$Archive: /AAMS/Components/bizTravelAgency/bzOrderStatus.vb $
'$Modtime: 9/03/10 2:56p $
Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizTravelAgency
    Public Class bzOrderStatus
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzOrderStatus"
        Const gstrList_OUTPUT = "<UP_LISTORDERSTATUS_OUTPUT><ORDER_STATUS ORDERSTATUSID='' ORDER_STATUS_NAME='' /><Errors Status=''><Error Code='' Description='' /></Errors></UP_LISTORDERSTATUS_OUTPUT>"
        Const srtVIEW_OUTPUT = "<MS_SEARCHORDERSTATUS_OUTPUT><ORDERSTATUS ORDER_STATUS_NAME='' ORDERSTATUSID='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHORDERSTATUS_OUTPUT>"
        Const strSEARCH_OUTPUT = "<MS_SEARCHORDERSTATUS_OUTPUT><ORDERSTATUS ORDER_STATUS_NAME='' ORDERSTATUSID=''/><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHORDERSTATUS_OUTPUT>"

        Const StrADD_OUTPUT = "<MS_ADDORDERSTATUS_OUTPUT><ORDERSTATUS ACTION=''  ORDER_STATUS_NAME='' ORDERSTATUSID=''/></MS_ADDORDERSTATUS_OUTPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETEORDERSTATUS_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETEORDERSTATUS_OUTPUT>"
        Const strUPDATE_OUTPUT = "<MS_UPDATEORDERSTATUS_INPUT><ORDERSTATUS ACTION=''  ORDER_STATUS_NAME='' ORDERSTATUSID=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEORDERSTATUS_INPUT>"

        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add
            'Declare Output XML String
            '***********************************************************************
            'Purpose: 
            'Input:None
            'Output:Xml Document
            '<MS_ADDORDERSTATUS_OUTPUT><ORDERSTATUS ACTION=''  ORDER_STATUS_NAME='' ORDERSTATUSID=''/></MS_ADDORDERSTATUS_OUTPUT>
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
            '***********************************************************************
            'Purpose:This function deletes a city.
            'Input:XmlDocument
            '<MS_DELETEORDERSTATUS_OUTPUT>
            '	<ORDERSTATUSID></ORDERSTATUSID>
            '</MS_DELETEORDERSTATUS_OUTPUT>           
            'Output :
            '<MS_DELETEORDERSTATUS_OUTPUT>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_DELETEORDERSTATUS_OUTPUT>            
            '************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Delete"
            Dim strORDER_id As String

            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0
                strORDER_id = DeleteDoc.DocumentElement.SelectSingleNode("ORDERSTATUSID").InnerText.Trim
                If strORDER_id = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_ORDER_STATUS"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"
                    .Parameters.Add(New SqlParameter("@ORDERSTATUSID", SqlDbType.Int))
                    .Parameters("@ORDERSTATUSID").Value = strORDER_id
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
            '<MS_SEARCHORDERSTATUS_INPUT>
            '	<ORDER_STATUS_NAME/>
            '</MS_SEARCHORDERSTATUS_INPUT>

            'Output :
            '<MS_SEARCHORDERSTATUS_OUTPUT>
            '	<ORDERSTATUS ORDER_STATUS_NAME="" ORDERSTATUSID="">
            '		<Errors Status="">
            '			<Error Code="" Description="" />
            '		</Errors>	
            '</MS_SEARCHORDERSTATUS_OUTPUT>

            '        '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strST_NAME As String
            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strST_NAME = (SearchDoc.DocumentElement.SelectSingleNode("ORDER_STATUS_NAME").InnerText.Trim())
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
                    .CommandText = "UP_SER_TA_ORDER_STATUS"
                    .Connection = objSqlConnection
                    .Parameters.Add("@ORDER_STATUS_NAME", SqlDbType.VarChar, 100)
                    .Parameters("@ORDER_STATUS_NAME").Value = strST_NAME
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
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("ORDERSTATUS")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("ORDER_STATUS_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDER_STATUS_NAME")))
                    objAptNodeClone.Attributes("ORDERSTATUSID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDERSTATUSID")))

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
            '<MS_SEARCHORDERSTATUS_OUTPUT>
            '	<ORDERSTATUS ACTION='' ORDER_STATUS_NAME="" ORDERSTATUSID="">
            '</MS_SEARCHORDERSTATUS_OUTPUT>
            'Output :
            '<MS_UPDATECITY_OUTPUT>
            '	<CITY_DETAIL Action="" CityID="" CityCode="" City_Name="" Aoffice="" CountryID="" StateID="" />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_UPDATECITY_OUTPUT>
            '************************************************************************
            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objUpdateDocOutput As New XmlDocument
            Dim INTST_CODE As Integer
            Dim strST_NAME As String
            Dim intRecordsAffected As Int32
            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)

                With objUpdateDocOutput.DocumentElement.SelectSingleNode("ORDERSTATUS")
                    .Attributes("ACTION").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERSTATUS").Attributes("ACTION").InnerText
                    .Attributes("ORDER_STATUS_NAME").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERSTATUS").Attributes("ORDER_STATUS_NAME").InnerText
                    .Attributes("ORDERSTATUSID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERSTATUS").Attributes("ORDERSTATUSID").InnerText
                End With

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("ORDERSTATUS")
                    strAction = ((.Attributes("ACTION").InnerText).Trim).ToString
                    strST_NAME = ((.Attributes("ORDER_STATUS_NAME").InnerText).Trim).ToString

                    If strAction = "I" Or strAction = "U" Then
                        If strST_NAME = "" Then
                            Throw (New AAMSException("Order Status Name can't be blank."))
                        End If
                    Else
                        Throw (New AAMSException("Invalid Action Code."))
                    End If
                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_ORDER_STATUS"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction

                    .Parameters.Add(New SqlParameter("@ORDERSTATUSID", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("ORDERSTATUS").Attributes("ORDERSTATUSID").InnerText = "" Then
                        .Parameters("@ORDERSTATUSID").Value = DBNull.Value
                    Else
                        .Parameters("@ORDERSTATUSID").Value = UpdateDoc.DocumentElement.SelectSingleNode("ORDERSTATUS").Attributes("ORDERSTATUSID").InnerText
                    End If


                    .Parameters.Add(New SqlParameter("@ORDER_STATUS_NAME", SqlDbType.VarChar, 100))
                    .Parameters("@ORDER_STATUS_NAME").Value = strST_NAME

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0

                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED
                    .Connection.Open()
                    intRecordsAffected = .ExecuteNonQuery()
                    If UCase(strAction) = "I" Then
                        intRetId = .Parameters("@RETURNID").Value
                        If intRetId = -1 Then
                            Throw (New AAMSException("Order Status Already Exists!"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                            With objUpdateDocOutput.DocumentElement.SelectSingleNode("ORDERSTATUS")
                                .Attributes("ORDERSTATUSID").InnerText = intRetId
                            End With
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
            'Purpose:This function gives details of City.
            'Input  :
            '<MS_SEARCHORDERSTATUS_INPUT>
            '	<ORDERSTATUSID/>
            '</MS_SEARCHORDERSTATUS_INPUT>

            'Output :
            '<MS_SEARCHORDERSTATUS_OUTPUT>
            '	<ORDERSTATUS ORDER_STATUS_NAME="" ORDERSTATUSID="">
            '		<Errors Status="">
            '			<Error Code="" Description="" />
            '		</Errors>	
            '</MS_SEARCHORDERSTATUS_OUTPUT> 
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
                strST_ID = IndexDoc.DocumentElement.SelectSingleNode("ORDERSTATUSID").InnerText.Trim
                If strST_ID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_ORDER_STATUS"
                    .Connection = objSqlConnection
                    .Parameters.Add("@ORDERSTATUSID", SqlDbType.Int)
                    .Parameters("@ORDERSTATUSID").Value = strST_ID
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("ORDERSTATUS")
                        .Attributes("ORDER_STATUS_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDER_STATUS_NAME")))
                        .Attributes("ORDERSTATUSID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDERSTATUSID")))
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
        Public Function List() As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the CRS record, based on the given field value
            'Input  : 
            'Output :  
            '<UP_LISTORDERSTATUS_OUTPUT>
            '    <ORDER_STATUS ORDERSTATUSID='' ORDER_STATUS_NAME='' />
            '    <Errors Status=''>
            '        <Error Code='' Description='' />
            '    </Errors>
            '</UP_LISTORDERSTATUS_OUTPUT>
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
                    .CommandText = "UP_LST_TA_ORDER_STATUS"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("ORDER_STATUS")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("ORDERSTATUSID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDERSTATUSID")))
                    objAptNodeClone.Attributes("ORDER_STATUS_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDER_STATUS_NAME")))
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
        Public Function List1() As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the CRS record, based on the given field value
            'Input  : 
            'Output :  
            '<UP_LISTORDERSTATUS_OUTPUT>
            '    <ORDER_STATUS ORDERSTATUSID='' ORDER_STATUS_NAME='' />
            '    <Errors Status=''>
            '        <Error Code='' Description='' />
            '    </Errors>
            '</UP_LISTORDERSTATUS_OUTPUT>
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
                    .CommandText = "UP_LST_TA_ORDER_STATUS_TRAINING"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("ORDER_STATUS")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("ORDERSTATUSID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDERSTATUSID")))
                    objAptNodeClone.Attributes("ORDER_STATUS_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDER_STATUS_NAME")))
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
    End Class
End Namespace
