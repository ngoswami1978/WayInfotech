'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Ashishsrivastava $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizTravelAgency/bzOnlineStatus.vb $
'$Workfile: bzOnlineStatus.vb $
'$Revision: 19 $
'$Archive: /AAMS/Components/bizTravelAgency/bzOnlineStatus.vb $
'$Modtime: 11/09/09 4:56p $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizTravelAgency

    Public Class bzOnlineStatus
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzOnlineStatus"
        Const gstrList_OUTPUT = "<MS_LISTONLINESTATUS_OUTPUT><Status StatusCode='' OnlineStatus='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTONLINESTATUS_OUTPUT>"

        Const strVIEW_INPUT = "<MS_VIEWONLINESTATUS_INPUT><StatusCode /></MS_VIEWONLINESTATUS_INPUT>"
        Const srtVIEW_OUTPUT = "<MS_VIEWONLINESTATUS_OUTPUT><Status OnlineStatus='' StatusCode='' SegExpected='' UnitCost='' BC_ONLINE_CATG_ID='' NPUnitCost='' LKUnitCost='' BDUnitCost='' BTUnitCost='' MLUnitCost='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWONLINESTATUS_OUTPUT>"


        Const strSEARCH_INPUT = "<MS_SEARCHONLINESTATUS_INPUT><StatusCode></StatusCode><OnlineStatus></OnlineStatus><SegExpected></SegExpected></MS_SEARCHONLINESTATUS_INPUT>"
        Const strSEARCH_OUTPUT = "<MS_SEARCHONLINESTATUS_OUTPUT><Status OnlineStatus='' StatusCode='' SegExpected=''  UnitCost=''  NPUnitCost='' LKUnitCost='' BDUnitCost='' BTUnitCost='' MLUnitCost='' ></Status><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHONLINESTATUS_OUTPUT>"


        Const StrADD_OUTPUT = "<MS_ADDONLINESTATUS_OUTPUT><Status OnlineStatus='' StatusCode=''/></MS_ADDONLINESTATUS_OUTPUT>"
        Const strDELETE_INPUT = "<MS_DELETEONLINESTATUS_INPUT><StatusCode /></MS_DELETEONLINESTATUS_INPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETEONLINESTATUS_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETEONLINESTATUS_OUTPUT>"

        Const strUPDATE_INPUT = "<MS_UPDATEONLINESTATUS_INPUT><Status Action=''   OnlineStatus='' StatusCode='' SegExpected='' UnitCost='' BC_ONLINE_CATG_ID=''/></MS_UPDATEONLINESTATUS_INPUT>"
        Const strUPDATE_OUTPUT = "<MS_UPDATEONLINESTATUS_OUTPUT><Status Action='' OnlineStatus='' StatusCode='' SegExpected='' UnitCost='' BC_ONLINE_CATG_ID='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEONLINESTATUS_OUTPUT>"

        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add
            'Declare Output XML String
            '***********************************************************************
            'Purpose: 
            'Input:None
            'Output:Xml Document
            '<MS_ADDONLINESTATUS_OUTPUT>
            '	<Status ACTION='' OnlineStatus="" StatusCode=""/>
            '</MS_ADDONLINESTATUS_OUTPUT>
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
            'Purpose:This function deletes a Online Status.
            'Input:XmlDocument
            '<MS_DELETEONLINESTATUS_INPUT>
            '	<StatusCode />
            '</MS_DELETEONLINESTATUS_INPUT>
            'Output :
            '<MS_DELETEONLINESTATUS_OUTPUT>
            '	<Errors Status="">
            '		<Error Code="" Description="" />
            '	</Errors>
            '</MS_DELETEONLINESTATUS_OUTPUT>
            '************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Delete"
            Dim strStatusCode As String

            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0
                strStatusCode = DeleteDoc.DocumentElement.SelectSingleNode("StatusCode").InnerText.Trim
                If strStatusCode = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_ONLINESTATUS"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"

                    .Parameters.Add(New SqlParameter("@STATUSCODE", SqlDbType.Char, 6))
                    .Parameters("@STATUSCODE").Value = strStatusCode

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
                bzShared.FillErrorStatus(objDeleteDocOutput, "101", "Online Status in Use!")
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
            '<MS_SEARCHONLINESTATUS_INPUT>
            '	<OnlineStatus></OnlineStatus>
            '</MS_SEARCHONLINESTATUS_INPUT>

            'Output :
            '<MS_SEARCHONLINESTATUS_OUTPUT>
            '	<Status OnlineStatus="" StatusCode=""></Status>
            '	<Errors Status="">
            '		<Error Code="" Description="" />
            '	</Errors>
            '</MS_SEARCHONLINESTATUS_OUTPUT>
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strOnlineStatus As String = ""
            Dim strStatusCode As String = ""
            Dim strSegmentExpected As String = ""


            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean


            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strOnlineStatus = (SearchDoc.DocumentElement.SelectSingleNode("OnlineStatus").InnerText.Trim())
                strStatusCode = (SearchDoc.DocumentElement.SelectSingleNode("StatusCode").InnerText.Trim())
                strSegmentExpected = (SearchDoc.DocumentElement.SelectSingleNode("SegExpected").InnerText.Trim())


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
                    .CommandText = "UP_SER_TA_ONLINESTATUS"
                    .Connection = objSqlConnection

                    .Parameters.Add("@STATUSCODE", SqlDbType.VarChar, 6)
                    .Parameters("@STATUSCODE").Value = strStatusCode

                    .Parameters.Add("@ONLINESTATUS", SqlDbType.VarChar, 30)
                    .Parameters("@ONLINESTATUS").Value = strOnlineStatus


                    .Parameters.Add("@SEGMENTEXPECTED", SqlDbType.VarChar, 30)
                    .Parameters("@SEGMENTEXPECTED").Value = strSegmentExpected


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
                        If strSortBy = "SegExpected" Then strSortBy = "SegmentExpected"
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
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("Status")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("OnlineStatus").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ONLINESTATUS")))
                    objAptNodeClone.Attributes("StatusCode").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("STATUSCODE")))
                    objAptNodeClone.Attributes("SegExpected").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SEGMENTEXPECTED")))
                    objAptNodeClone.Attributes("UnitCost").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("UnitCost")))

           
                    objAptNodeClone.Attributes("NPUnitCost").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NPUnitCost")))
                    objAptNodeClone.Attributes("LKUnitCost").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LKUnitCost")))
                    objAptNodeClone.Attributes("BDUnitCost").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("BDUnitCost")))
                    objAptNodeClone.Attributes("BTUnitCost").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("BTUnitCost")))
                    objAptNodeClone.Attributes("MLUnitCost").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MLUnitCost")))

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
            '<MS_UPDATEONLINESTATUS_INPUT>
            '	<Status ACTION='' OnlineStatus="" StatusCode="" UnitCost='' BC_ONLINE_CATG_ID="" />
            '</MS_UPDATEONLINESTATUS_INPUT>


            'Output :
            '<MS_UPDATEONLINESTATUS_OUTPUT>
            '	<Status ACTION='' OnlineStatus="" StatusCode="" BC_ONLINE_CATG_ID="" />
            '	<Errors Status="">
            '		<Error Code="" Description="" />
            '	</Errors>
            '</MS_UPDATEONLINESTATUS_OUTPUT>

            '************************************************************************
            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objUpdateDocOutput As New XmlDocument
            Dim strStatusCode As String
            Dim strOnlineStatus As String
            Dim strSegmentExpected As String
            Dim strUnitCost As String
            Dim strBC_ONLINE_CATG_ID As String

            Dim intRecordsAffected As Int32
            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)

                With objUpdateDocOutput.DocumentElement.SelectSingleNode("Status")
                    .Attributes("Action").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Status").Attributes("Action").InnerText
                    .Attributes("OnlineStatus").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Status").Attributes("OnlineStatus").InnerText
                    .Attributes("StatusCode").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Status").Attributes("StatusCode").InnerText
                    .Attributes("SegExpected").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Status").Attributes("SegExpected").InnerText
                    .Attributes("UnitCost").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Status").Attributes("UnitCost").InnerText
                    .Attributes("BC_ONLINE_CATG_ID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Status").Attributes("BC_ONLINE_CATG_ID").InnerText

                End With

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("Status")
                    strAction = ((.Attributes("Action").InnerText).Trim).ToString
                    strStatusCode = ((.Attributes("StatusCode").InnerText).Trim).ToString
                    strOnlineStatus = ((.Attributes("OnlineStatus").InnerText).Trim).ToString
                    strSegmentExpected = ((.Attributes("SegExpected").InnerText).Trim).ToString
                    strUnitCost = ((.Attributes("UnitCost").InnerText).Trim).ToString
                    strBC_ONLINE_CATG_ID = ((.Attributes("BC_ONLINE_CATG_ID").InnerText).Trim).ToString
                    If strAction = "I" Or strAction = "U" Then
                        If strStatusCode = "" Then
                            Throw (New AAMSException("Status Code can't be blank."))
                        ElseIf strOnlineStatus = "" Then
                            Throw (New AAMSException("Online Status can't be blank."))
                        End If
                    Else
                        Throw (New AAMSException("Invalid Action Code."))
                    End If

                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_ONLINESTATUS"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction

                    .Parameters.Add(New SqlParameter("@ONLINESTATUS", SqlDbType.VarChar, 30))
                    .Parameters("@ONLINESTATUS").Value = strOnlineStatus

                    .Parameters.Add(New SqlParameter("@STATUSCODE", SqlDbType.Char, 6))
                    .Parameters("@STATUSCODE").Value = strStatusCode

                    .Parameters.Add(New SqlParameter("@PROTECTED", SqlDbType.Bit))
                    .Parameters("@PROTECTED").Value = 0

                    .Parameters.Add(New SqlParameter("@SegmentExpected", SqlDbType.VarChar, 30))
                    .Parameters("@SegmentExpected").Value = strSegmentExpected

                    .Parameters.Add(New SqlParameter("@UnitCost", SqlDbType.Float))
                


                    If UpdateDoc.DocumentElement.SelectSingleNode("Status").Attributes("UnitCost").InnerText <> "" Then
                        .Parameters("@UnitCost").Value = CDbl(strUnitCost)
                    Else
                        .Parameters("@UnitCost").Value = DBNull.Value
                    End If



                    .Parameters.Add("@BC_ONLINE_CATG_ID", SqlDbType.Int)
                    If UpdateDoc.DocumentElement.SelectSingleNode("Status").Attributes("BC_ONLINE_CATG_ID").InnerText <> "" Then
                        .Parameters("@BC_ONLINE_CATG_ID").Value = CDbl(strBC_ONLINE_CATG_ID)
                    Else
                        .Parameters("@BC_ONLINE_CATG_ID").Value = DBNull.Value
                    End If

                    .Parameters.Add(New SqlParameter("@RETUNID", SqlDbType.Int))
                    .Parameters("@RETUNID").Direction = ParameterDirection.Output

                    .Parameters("@RETUNID").Value = 0
                    .Connection.Open()

                    intRecordsAffected = .ExecuteNonQuery()

                    If UCase(strAction) = "I" Then
                        intRetId = .Parameters("@RETUNID").Value
                        If intRetId = 0 Then
                            Throw (New AAMSException("Unable to insert!"))
                        ElseIf intRetId = -1 Then
                            Throw (New AAMSException("Status code already exists. Please enter another Status Code"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                        End If
                    ElseIf UCase(strAction) = "U" Then
                        intRetId = .Parameters("@RETUNID").Value
                        If intRetId = 0 Then
                            Throw (New AAMSException("Unable to update!"))
                        ElseIf intRetId = -1 Then
                            Throw (New AAMSException("Status code already exists!"))
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
                        bzShared.FillErrorStatus(objUpdateDocOutput, "101", "Status Code already exists. Please enter another Status Code")
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
            '<MS_VIEWONLINESTATUS_INPUT>
            '	<StatusCode />
            '</MS_VIEWONLINESTATUS_INPUT>

            'Output :
            '<MS_VIEWONLINESTATUS_OUTPUT>
            '	<Status OnlineStatus="" StatusCode="" BC_ONLINE_CATG_ID="" />
            '	<Errors Status="">
            '		<Error Code="" Description="" />
            '	</Errors>
            '</MS_VIEWONLINESTATUS_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strStatusCode As String
            Const strMETHOD_NAME As String = "View"
            Dim blnRecordFound As Boolean
            Try
                objOutputXml.LoadXml(srtVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strStatusCode = IndexDoc.DocumentElement.SelectSingleNode("StatusCode").InnerText.Trim
                If strStatusCode = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_ONLINESTATUS"
                    .Connection = objSqlConnection
                    .Parameters.Add("@STATUSCODE", SqlDbType.Char, 6)
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@STATUSCODE").Value = strStatusCode
                    .Parameters("@ACTION").Value = "S"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("Status")
                        .Attributes("OnlineStatus").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ONLINESTATUS")) & "")
                        .Attributes("StatusCode").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("STATUSCODE")) & "")
                        .Attributes("SegExpected").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SEGMENTEXPECTED")) & "")
                        .Attributes("UnitCost").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("UnitCost")) & "")
                        .Attributes("BC_ONLINE_CATG_ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("BC_ONLINE_CATG_ID")) & "")


                        ''NPUnitCost='' LKUnitCost='' BDUnitCost='' BTUnitCost='' MLUnitCost=''

                        .Attributes("NPUnitCost").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NPUnitCost")) & "")
                        .Attributes("LKUnitCost").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LKUnitCost")) & "")
                        .Attributes("BDUnitCost").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("BDUnitCost")) & "")
                        .Attributes("BTUnitCost").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("BTUnitCost")) & "")
                        .Attributes("MLUnitCost").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MLUnitCost")) & "")


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
            '<MS_LISTONLINESTATUS_OUTPUT>
            '	<Status StatusCode='' OnlineStatus='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_LISTONLINESTATUS_OUTPUT>
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
                    .CommandText = "UP_LST_TA_ONLINE_STATUS"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("Status")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("StatusCode").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("StatusCode")))
                    objAptNodeClone.Attributes("OnlineStatus").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OnlineStatus")))
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