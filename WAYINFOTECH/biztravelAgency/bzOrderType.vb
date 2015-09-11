'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Neeraj $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizTravelAgency/bzOrderType.vb $
'$Workfile: bzOrderType.vb $
'$Revision: 26 $
'$Archive: /AAMS/Components/bizTravelAgency/bzOrderType.vb $
'$Modtime: 6/18/10 1:50p $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizTravelAgency
    Public Class bzOrderType
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzOrderType"
        Const gstrList_OUTPUT = "<UP_LISTORDERTYPE_OUTPUT><ORDER_TYPE ORDERTYPEID='' ORDER_TYPE_NAME='' /><Errors Status=''><Error Code='' Description='' /></Errors></UP_LISTORDERTYPE_OUTPUT>"
        Const srtVIEW_OUTPUT = "<MS_VIEWORDERTYPE_OUTPUT><ORDERTYPE ORDERTYPEID='' ORDER_TYPE_NAME='' ForPCType='' IsDeleted='' IsISPOrder='' IsChallanOrder ='' IsTrainingOrder='' IshardwareOrder='' OrderTypeCategoryID='' NewConnectivity='' OldConnectivity='' OrderTrackingRequired='' TimeRequired='' CANCELLATION='' NEW_ORDER='' DESCRIPTION='' EXPECTED_INSTALLATION_DATE='' /><CorporateCode RowID='' Code=''  Qualifier='' Description=''  /><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWORDERTYPE_OUTPUT>"

        Const strSEARCH_OUTPUT = "<MS_SEARCHORDERTYPE_OUTPUT><ORDERTYPE OrderTypeCategoryName='' ORDERTYPEID='' ORDER_TYPE_NAME='' TimeRequired=''/><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHORDERTYPE_OUTPUT>"

        Const StrADD_OUTPUT = "<MS_ADDORDERTYPE_OUTPUT><ORDERTYPE ACTION=''  ORDERTYPEID='' ORDER_TYPE_NAME='' IsDeleted='' IsISPOrder='' OrderTypeCategoryID='' NewConnectivity='' OldConnectivity='' OrderTrackingRequired='' TimeRequired='' CANCELLATION='' NEW_ORDER='' DESCRIPTION=''/><RowID></RowID></MS_ADDORDERTYPE_OUTPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETEORDERTYPE_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETEORDERTYPE_OUTPUT>"
        Const strUPDATE_OUTPUT = "<MS_UPDATEORDERTYPE_OUTPUT><ORDERTYPE ACTION=''  ORDERTYPEID='' ORDER_TYPE_NAME='' IsDeleted='' IsISPOrder='' IsChallanOrder ='' IsTrainingOrder='' IshardwareOrder='' OrderTypeCategoryID='' NewConnectivity='' OldConnectivity='' OrderTrackingRequired='' TimeRequired='' CANCELLATION='' NEW_ORDER='' DESCRIPTION=''/><RowID></RowID><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEORDERTYPE_OUTPUT>"
        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add
            'Declare Output XML String
            '***********************************************************************
            'Purpose: 
            'Input:None
            'Output:Xml Document
            '<MS_ADDORDERTYPE_OUTPUT><ORDERTYPE ACTION=''  ORDERTYPEID='' ORDER_TYPE_NAME='' IsDeleted='' IsISPOrder='' OrderTypeCategoryID='' NewConnectivity='' OldConnectivity='' OrderTrackingRequired='' TimeRequired='' CANCELLATION='' NEW_ORDER='' DESCRIPTION=''/></MS_ADDORDERTYPE_OUTPUT>
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
            Dim intRetId As Integer
            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0
                strORDER_id = DeleteDoc.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText.Trim
                If strORDER_id = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_ORDER_TYPE"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"
                    .Parameters.Add(New SqlParameter("@ORDERTYPEID", SqlDbType.Int))
                    .Parameters("@ORDERTYPEID").Value = strORDER_id

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0

                    objSqlCommand.Connection.Open()
                    intRecordsAffected = .ExecuteNonQuery()
                    objSqlCommand.Connection.Close()
                End With
                intRetId = objSqlCommand.Parameters("@RETURNID").Value


                'Checking whether record is deleted successfully or not
                If intRetId > 0 Then
                    objDeleteDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    Return (objDeleteDocOutput)
                ElseIf intRetId = -1 Then
                    objDeleteDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "TRUE"
                    Call bzShared.FillErrorStatus(objDeleteDocOutput, "101", "Order Type in use!")
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
            '<MS_SEARCHORDERTYPE_OUTPUT><ORDERTYPE OrderTypeCategoryName='' ORDERTYPEID='' ORDER_TYPE_NAME='' TimeRequired=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHORDERTYPE_OUTPUT>

            '        '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strST_NAME As String
            Dim strCT_NAME As String
            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strST_NAME = (SearchDoc.DocumentElement.SelectSingleNode("ORDER_TYPE_NAME").InnerText.Trim())
                strCT_NAME = (SearchDoc.DocumentElement.SelectSingleNode("OrderTypeCategoryName").InnerText.Trim())
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
                    .CommandText = "UP_SER_TA_ORDER_TYPE"
                    .Connection = objSqlConnection
                    .Parameters.Add("@ORDER_TYPE_NAME", SqlDbType.VarChar, 40)
                    If strST_NAME <> "" Then
                        .Parameters("@ORDER_TYPE_NAME").Value = strST_NAME
                    Else
                        .Parameters("@ORDER_TYPE_NAME").Value = DBNull.Value
                    End If
                    .Parameters.Add("@OrderTypeCategoryName", SqlDbType.VarChar, 100)
                    If strCT_NAME <> "" Then
                        .Parameters("@OrderTypeCategoryName").Value = strCT_NAME
                    Else
                        .Parameters("@OrderTypeCategoryName").Value = DBNull.Value
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
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("ORDERTYPEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDERTYPEID")))
                    objAptNodeClone.Attributes("ORDER_TYPE_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDER_TYPE_NAME")))
                    objAptNodeClone.Attributes("TimeRequired").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("TimeRequired")))
                    objAptNodeClone.Attributes("OrderTypeCategoryName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OrderTypeCategoryName")))

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
            '<MS_UPDATEORDERTYPE_INPUT>
            '   <ORDERTYPE ACTION="" ORDERTYPEID="" ForPCType="" ORDER_TYPE_NAME="" IsDeleted="" IsISPOrder=""  IsChallanOrder="" IsTrainingOrder="" IshardwareOrder="" OrderTypeCategoryID="" NewConnectivity="" OldConnectivity="" OrderTrackingRequired="" TimeRequired="" CANCELLATION="" NEW_ORDER="" DESCRIPTION="" />
            '<RowID></RowID>
            '</MS_UPDATEORDERTYPE_INPUT>
            
            'Output :
            '<MS_UPDATEORDERTYPE_OUTPUT>
            '   <ORDERTYPE ACTION=''  ORDERTYPEID='' ORDER_TYPE_NAME='' IsDeleted='' IsISPOrder='' OrderTypeCategoryID='' NewConnectivity='' OldConnectivity='' OrderTrackingRequired='' TimeRequired='' CANCELLATION='' NEW_ORDER='' DESCRIPTION=''/>
            '   <Errors Status=''>
            '       <Error Code='' Description='' />
            '</Errors>
            '</MS_UPDATEORDERTYPE_OUTPUT>
            '************************************************************************
            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objSqlRowCommand As New SqlCommand
            Dim objSqlRowCommand1 As New SqlCommand
            Dim objTran As SqlTransaction
            Dim objUpdateDocOutput As New XmlDocument
            Dim strST_NAME As String
            Dim objNode As XmlNode
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim intRecordsAffected As Int32
            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)
                With objUpdateDocOutput.DocumentElement.SelectSingleNode("ORDERTYPE")
                    .Attributes("ACTION").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("ACTION").InnerText
                    .Attributes("ORDERTYPEID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("ORDERTYPEID").InnerText
                    .Attributes("IsDeleted").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsDeleted").InnerText
                    .Attributes("IsISPOrder").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsISPOrder").InnerText

                    .Attributes("IsChallanOrder").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsChallanOrder").InnerText
                    .Attributes("IsTrainingOrder").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsTrainingOrder").InnerText
                    .Attributes("IshardwareOrder").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IshardwareOrder").InnerText

                    .Attributes("ORDER_TYPE_NAME").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("ORDER_TYPE_NAME").InnerText
                    .Attributes("OrderTypeCategoryID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("OrderTypeCategoryID").InnerText
                    .Attributes("NewConnectivity").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("NewConnectivity").InnerText
                    .Attributes("OldConnectivity").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("OldConnectivity").InnerText
                    .Attributes("OrderTrackingRequired").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("OrderTrackingRequired").InnerText
                    .Attributes("TimeRequired").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("TimeRequired").InnerText
                    .Attributes("CANCELLATION").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("CANCELLATION").InnerText
                    .Attributes("NEW_ORDER").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("NEW_ORDER").InnerText
                    .Attributes("DESCRIPTION").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("DESCRIPTION").InnerText
                End With


                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE")
                    strAction = ((.Attributes("ACTION").InnerText).Trim).ToString
                    strST_NAME = ((.Attributes("ORDER_TYPE_NAME").InnerText).Trim).ToString

                    If strAction = "I" Or strAction = "U" Then
                        If strST_NAME = "" Then
                            Throw (New AAMSException("Order Type can't be blank."))
                        End If
                    Else
                        Throw (New AAMSException("Invalid Action Code."))
                    End If
                End With
                objAptNode = objUpdateDocOutput.DocumentElement.SelectSingleNode("RowID")
                objAptNodeClone = objAptNode.CloneNode(True)
                objUpdateDocOutput.DocumentElement.RemoveChild(objAptNode)

                For Each objNode In UpdateDoc.DocumentElement.SelectNodes("RowID")
                    objAptNodeClone.InnerText = objNode.InnerText
                    objUpdateDocOutput.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Next

                'ADDING PARAMETERS IN STORED PROCEDURE

                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_ORDER_TYPE"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction
                    .Parameters.Add(New SqlParameter("@ORDERTYPEID", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("ORDERTYPEID").InnerText.Trim = "" Then
                        .Parameters("@ORDERTYPEID").Value = DBNull.Value
                    Else
                        .Parameters("@ORDERTYPEID").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("ORDERTYPEID").InnerText)
                    End If

                    .Parameters.Add(New SqlParameter("@ORDER_TYPE_NAME", SqlDbType.VarChar, 40))
                    .Parameters("@ORDER_TYPE_NAME").Value = strST_NAME

                    .Parameters.Add(New SqlParameter("@IsISPOrder", SqlDbType.Bit))
                    If UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsISPOrder").InnerText.Trim = "" Then
                        .Parameters("@IsISPOrder").Value = DBNull.Value
                    Else
                        .Parameters("@IsISPOrder").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsISPOrder").InnerText)
                    End If

                    .Parameters.Add(New SqlParameter("@IsChallanOrder", SqlDbType.Bit))
                    If UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsChallanOrder").InnerText.Trim = "" Then
                        .Parameters("@IsChallanOrder").Value = DBNull.Value
                    Else
                        .Parameters("@IsChallanOrder").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsChallanOrder").InnerText)
                    End If

                    .Parameters.Add(New SqlParameter("@IsTrainingOrder", SqlDbType.Bit))
                    If UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsTrainingOrder").InnerText.Trim = "" Then
                        .Parameters("@IsTrainingOrder").Value = DBNull.Value
                    Else
                        .Parameters("@IsTrainingOrder").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsTrainingOrder").InnerText)
                    End If

                    .Parameters.Add(New SqlParameter("@IshardwareOrder", SqlDbType.Bit))
                    If UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IshardwareOrder").InnerText.Trim = "" Then
                        .Parameters("@IshardwareOrder").Value = DBNull.Value
                    Else
                        .Parameters("@IshardwareOrder").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IshardwareOrder").InnerText)
                    End If

                    .Parameters.Add(New SqlParameter("@IsDeleted", SqlDbType.Bit))
                    If UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsDeleted").InnerText.Trim = "" Then
                        .Parameters("@IsDeleted").Value = DBNull.Value
                    Else
                        .Parameters("@IsDeleted").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsDeleted").InnerText)
                    End If

                    .Parameters.Add(New SqlParameter("@ForPCType", SqlDbType.Char, 10))
                    If UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("ForPCType").InnerText = "" Then
                        .Parameters("@ForPCType").Value = DBNull.Value
                    Else
                        .Parameters("@ForPCType").Value = UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("ForPCType").InnerText
                    End If
                    .Parameters.Add(New SqlParameter("@OrderTypeCategoryID", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("OrderTypeCategoryID").InnerText = "" Then
                        .Parameters("@OrderTypeCategoryID").Value = DBNull.Value
                    Else
                        .Parameters("@OrderTypeCategoryID").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("OrderTypeCategoryID").InnerText)
                    End If
                    .Parameters.Add(New SqlParameter("@NewConnectivity", SqlDbType.Char, 6))
                    If UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("NewConnectivity").InnerText = "" Then
                        .Parameters("@NewConnectivity").Value = DBNull.Value
                    Else
                        .Parameters("@NewConnectivity").Value = UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("NewConnectivity").InnerText
                    End If
                    .Parameters.Add(New SqlParameter("@OldConnectivity", SqlDbType.Char, 6))
                    If UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("OldConnectivity").InnerText = "" Then
                        .Parameters("@OldConnectivity").Value = DBNull.Value
                    Else
                        .Parameters("@OldConnectivity").Value = UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("OldConnectivity").InnerText
                    End If
                    .Parameters.Add(New SqlParameter("@OrderTrackingRequired", SqlDbType.Bit))
                    If UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("OrderTrackingRequired").InnerText.Trim = "" Then
                        .Parameters("@OrderTrackingRequired").Value = DBNull.Value
                    Else
                        .Parameters("@OrderTrackingRequired").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("OrderTrackingRequired").InnerText)
                    End If
                    .Parameters.Add(New SqlParameter("@TimeRequired", SqlDbType.SmallInt))
                    If UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("TimeRequired").InnerText = "" Then
                        .Parameters("@TimeRequired").Value = DBNull.Value
                    Else
                        .Parameters("@TimeRequired").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("TimeRequired").InnerText)
                    End If
                    .Parameters.Add(New SqlParameter("@CANCELLATION", SqlDbType.Bit))
                    If UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("CANCELLATION").InnerText = "" Then
                        .Parameters("@CANCELLATION").Value = DBNull.Value
                    Else
                        .Parameters("@CANCELLATION").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("CANCELLATION").InnerText)
                    End If
                    .Parameters.Add(New SqlParameter("@NEW_ORDER", SqlDbType.Char, 1))
                    If UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("NEW_ORDER").InnerText = "" Then
                        .Parameters("@NEW_ORDER").Value = DBNull.Value
                    Else
                        .Parameters("@NEW_ORDER").Value = UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("NEW_ORDER").InnerText
                    End If

                    .Parameters.Add(New SqlParameter("@DESCRIPTION", SqlDbType.VarChar, 300))
                    If UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("DESCRIPTION").InnerText = "" Then
                        .Parameters("@DESCRIPTION").Value = DBNull.Value
                    Else
                        .Parameters("@DESCRIPTION").Value = UpdateDoc.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("DESCRIPTION").InnerText
                    End If


                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0

                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED
                    .Connection.Open()
                    intRecordsAffected = .ExecuteNonQuery()
                End With

                intRetId = objSqlCommand.Parameters("@RETURNID").Value

                If intRetId > 0 Then
                    With objSqlRowCommand
                        .Connection = objSqlConnection
                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "UP_SRO_TA_OFFICEID_Q_TYPE"
                        .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                        .Parameters("@ACTION").Value = "D"
                        .Parameters.Add(New SqlParameter("@ORDERTYPEID", SqlDbType.Int))
                        .Parameters("@ORDERTYPEID").Value = intRetId
                        .ExecuteNonQuery()
                    End With

                    objTran = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)
                    With objSqlRowCommand1
                        .Connection = objSqlConnection
                        .Transaction = objTran
                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "UP_SRO_TA_OFFICEID_Q_TYPE"
                        .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                        .Parameters.Add(New SqlParameter("@ORDERTYPEID", SqlDbType.Int))
                        .Parameters.Add(New SqlParameter("@RowID", SqlDbType.Int))
                    End With

                    For Each objNode In UpdateDoc.DocumentElement.SelectNodes("RowID")
                        With objSqlRowCommand1
                            .Parameters("@ACTION").Value = "I"
                            .Parameters("@ORDERTYPEID").Value = intRetId
                            .Parameters("@RowID").Value = objNode.InnerText
                            .ExecuteNonQuery()
                        End With
                    Next
                    objTran.Commit()
                    objSqlConnection.Close()
                End If

                If UCase(strAction) = "I" Then
                    intRetId = objSqlCommand.Parameters("@RETURNID").Value
                    If intRetId = -1 Then
                        Throw (New AAMSException("Order Type Already Exists!"))
                    Else
                        objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                        With objUpdateDocOutput.DocumentElement.SelectSingleNode("ORDERTYPE")
                            .Attributes("ORDERTYPEID").InnerText = intRetId
                        End With
                    End If
                ElseIf UCase(strAction) = "U" Then
                    intRetId = objSqlCommand.Parameters("@RETURNID").Value
                    If intRetId <= 0 Then
                        Throw (New AAMSException("Unable to update!"))
                    Else
                        objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    End If
                End If


            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                Return objUpdateDocOutput
            Catch Exec As Exception
                If objSqlConnection.State <> ConnectionState.Closed Then
                    If Not objTran Is Nothing Then
                        objTran.Rollback()
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
            Dim objAptNode, objAptNodeClone As XmlNode
            Const strMETHOD_NAME As String = "View"
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(srtVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strST_ID = IndexDoc.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText.Trim
                If strST_ID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_ORDER_TYPE"
                    .Connection = objSqlConnection
                    .Parameters.Add("@ORDERTYPEID", SqlDbType.Int)
                    .Parameters("@ORDERTYPEID").Value = strST_ID
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE")
                        .Attributes("ORDERTYPEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDERTYPEID")) & "")
                        .Attributes("ORDER_TYPE_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDER_TYPE_NAME")) & "")
                        .Attributes("TimeRequired").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("TimeRequired")) & "")
                        .Attributes("ForPCType").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ForPCType")) & "")
                        .Attributes("OrderTypeCategoryID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OrderTypeCategoryId")) & "")

                        .Attributes("IsDeleted").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("IsDeleted")) & "")
                        .Attributes("IsISPOrder").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("IsISPOrder")) & "")
                        .Attributes("IsChallanOrder").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("IsChallanOrder")) & "")
                        .Attributes("IsTrainingOrder").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("IsTrainingOrder")) & "")
                        .Attributes("IshardwareOrder").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("IshardwareOrder")) & "")

                        .Attributes("NewConnectivity").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NewConnectivity")) & "")
                        .Attributes("OldConnectivity").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OldConnectivity")) & "")
                        .Attributes("OrderTrackingRequired").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OrderTrackingRequired")) & "")
                        .Attributes("CANCELLATION").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CANCELLATION")) & "")
                        .Attributes("NEW_ORDER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NEW_ORDER")) & "")
                        .Attributes("DESCRIPTION").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DESCRIPTION")) & "")
                        .Attributes("EXPECTED_INSTALLATION_DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EXPECTED_INSTALLATION_DATE")) & "")
                    End With
                    blnRecordFound = True
                Loop
                objSqlReader.NextResult()

                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("CorporateCode")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)

                Do While objSqlReader.Read()
                    objAptNodeClone.Attributes("RowID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("RowID")) & "")
                    objAptNodeClone.Attributes("Code").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Code")) & "")
                    objAptNodeClone.Attributes("Qualifier").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Qualifier")) & "")
                    objAptNodeClone.Attributes("Description").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Description")) & "")
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
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
        Public Function List(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the CRS record, based on the given field value
            'Input  : 
            '<UP_LISTORDERTYPE_INPUT>
            '	<ORDER_TYPE></ORDER_TYPE>
            '	<!-- PASS 0 FOR NEW ORDER AND 1 FOR CANCELLATION -->
            '</UP_LISTORDERTYPE_INPUT>
            'Output :  
            '<UP_LISTORDERTYPE_OUTPUT>
            '	<ORDER_TYPE ORDERTYPEID='' ORDER_TYPE_NAME='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</UP_LISTORDERTYPE_OUTPUT>
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
                    .CommandText = "UP_LST_TA_ORDER_TYPE"
                    .Connection = objSqlConnection
                    .Parameters.Add("@ORDERTYPE", SqlDbType.Int)
                    .Parameters("@ORDERTYPE").Value = CInt(IndexDoc.DocumentElement.SelectSingleNode("ORDER_TYPE").InnerXml)
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("ORDER_TYPE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("ORDERTYPEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDERTYPEID")))
                    objAptNodeClone.Attributes("ORDER_TYPE_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDER_TYPE_NAME")))
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

        Public Function List1(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the CRS record, based on the given field value
            'Input  : 
            '<UP_LISTORDERTYPE_INPUT>
            '	<ORDER_TYPE></ORDER_TYPE>
            '	<!-- PASS 0 FOR NEW ORDER AND 1 FOR CANCELLATION -->
            '</UP_LISTORDERTYPE_INPUT>
            'Output :  
            '<UP_LISTORDERTYPE_OUTPUT>
            '	<ORDER_TYPE ORDERTYPEID='' ORDER_TYPE_NAME='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</UP_LISTORDERTYPE_OUTPUT>
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
                    .CommandText = "UP_LST_TA_ORDER_TYPE_TRAINING"
                    .Connection = objSqlConnection
                    .Parameters.Add("@ORDERTYPE", SqlDbType.Int)
                    .Parameters("@ORDERTYPE").Value = CInt(IndexDoc.DocumentElement.SelectSingleNode("ORDER_TYPE").InnerXml)
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("ORDER_TYPE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("ORDERTYPEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDERTYPEID")))
                    objAptNodeClone.Attributes("ORDER_TYPE_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDER_TYPE_NAME")))
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
