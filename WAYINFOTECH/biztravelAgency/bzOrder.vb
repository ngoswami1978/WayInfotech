'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Neeraj $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizTravelAgency/bzOrder.vb $
'$Workfile: bzOrder.vb $
'$Revision: 70 $
'$Archive: /AAMS/Components/bizTravelAgency/bzOrder.vb $
'$Modtime: 9/19/11 7:07p $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared

Namespace bizTravelAgency
    Public Class bzOrder
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzOrder"
        Const srtGET_OUTPUT = "<MS_GETORDERS_OUTPUT><ORDERS ORDERID='' ORDER_NUMBER='' ORDER_TYPE_NAME='' ORDER_STATUS_NAME='' APPROVAL_DATE='' APPLIED_DATE='' OFFICEID='' OFFICEID1='' PENDINGWITHNAME='' REMARKS='' APC='' OPC='' APR='' ISORDERTYPEID='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_GETORDERS_OUTPUT>"
        Const srtVIEW_OUTPUT = "<MS_VIEWORDERS_OUTPUT><ORDERS ORDERID='' ORDERTYPE='' ORDERTYPEID='' LCODE='' NAME='' ADDRESS='' ADDRESS1='' FILENO ='' ORDER_NUMBER='' ORDERSTATUSID='' PROCESSEDBYID='' PROCESSEDBYNAME='' PLANID='' ISPNAME='' ISPID='' RESEND_DATE_MKT='' RECEIVED_DATE_MKT='' INSTALLATION_DUE_DATE='' EXPECTED_INSTALLATION_DATE='' APR='' OPC='' APC='' NewOrder='' OFFICEID1='' OFFICEID='' RECEIVING_OFFICEID='' MSG_SEND_DATE='' APPLIED_DATE='' SENDBACK_DATE='' APPROVAL_DATE='' RECEIVED_DATE='' PROCESSED_DATE='' REMARKS='' ATID='' PENDINGWITHID='' PENDINGWITHNAME='' NPID='' CHAIN_CODE='' CHAIN_NAME='' NEW_ORD_MSG='' LCOUNT='' /> <Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWORDERS_OUTPUT>"

        Const strSEARCH_OUTPUT = "<UP_SEARCHORDER_OUTPUT><ORDERS ORDERID='' AGENCYNAME='' ADDRESS='' ADDRESS1='' Region='' CITY='' COUNTRY='' ORDER_NUMBER='' ORDER_TYPE_NAME='' ORDER_STATUS_NAME='' APPROVAL_DATE='' RECEIVED_DATE='' ODDICEID='' OFFICEID1='' MSG_SEND_DATE='' INSTALLATION_DUE_DATE='' PENDINGWITHNAME='' RECEIVED_DATE_MKT='' APC='' RESEND_DATE_MKT='' REMARKS='' COMP_VERTICAL=''/><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></UP_SEARCHORDER_OUTPUT>"

        Const StrADD_OUTPUT = "<MS_UPDATEORDERS_INPUT><ORDERS ORDERID='' ORDERTYPEID='' LCODE='' ORDER_NUMBER='' ORDERSTATUSID='' PROCESSEDBYID='' PLANID='' ISPID='' RESEND_DATE_MKT='' RECEIVED_DATE_MKT='' INSTALLATION_DUE_DATE='' EXPECTED_INSTALLATION_DATE='' APR='' OPC='' APC='' NewOrder='' OFFICEID1='' OFFICEID='' RECEIVING_OFFICEID='' MSG_SEND_DATE='' APPLIED_DATE='' SENDBACK_DATE='' APPROVAL_DATE='' RECEIVED_DATE='' PROCESSED_DATE='' REMARKS='' PENDINGWITHID='' EMPLOYEEID='' /></MS_UPDATEORDERS_INPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETEORDER_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETEORDER_OUTPUT>"

        Const strUPDATE_OUTPUT = "<MS_UPDATEORDERS_OUTPUT><ORDERS ORDERID='' ORDERTYPEID='' LCODE='' ORDER_NUMBER='' ORDERSTATUSID='' PROCESSEDBYID='' PLANID='' ISPID='' RESEND_DATE_MKT='' RECEIVED_DATE_MKT='' INSTALLATION_DUE_DATE='' EXPECTED_INSTALLATION_DATE='' APR='' OPC='' APC='' NewOrder='' OFFICEID1='' OFFICEID='' RECEIVING_OFFICEID='' MSG_SEND_DATE='' APPLIED_DATE='' SENDBACK_DATE='' APPROVAL_DATE='' RECEIVED_DATE='' PROCESSED_DATE='' REMARKS='' PENDINGWITHID='' ATID='' COMP_VERTICAL='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEORDERS_OUTPUT>"

        Const strHISTORY_INPUT = "<MS_GETHISTORYORDER_INPUT><ORDERID></ORDERID></MS_GETHISTORYORDER_INPUT>"
        Const strHISTORY_OUTPUT = "<MS_GETHISTORYORDER_OUTPUT><HISTORYDETAIL  ORDERID='' EMPLOYEENAME='' CHANGEDATA='' DATETIME='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_GETHISTORYORDER_OUTPUT>"
        Const strSCANDOCUMENT_INPUT = "<TA_GETSCANNEDDOCUMENT_INPUT><FileNo></FileNo><Order_No></Order_No></TA_GETSCANNEDDOCUMENT_INPUT>"
        Const strSCANDOCUMENT_OUTPUT = "<TA_GETSCANNEDDOCUMENT_OUTPUT><Document ID='' FileNo='' Order_No='' Status='' FileOrder='' DocType='' Order_Type='' ContentType='' EmailFrom=''	EmailTo	=''	EmailSubject='' EmailBody='' PDFDocFileName=''/><Errors Status=''><Error Code='' Description='' /></Errors></TA_GETSCANNEDDOCUMENT_OUTPUT>"
        Const strMISCSCANDOCUMENT_INPUT = "<TA_GETMISCSCANNEDDOCUMENT_INPUT><FileNo></FileNo><Order_No></Order_No></TA_GETMISCSCANNEDDOCUMENT_INPUT>"
        Const strMISCSCANDOCUMENT_OUTPUT = "<TA_GETMISCSCANNEDDOCUMENT_OUTPUT><Document ID='' Document ='' FileNo='' Order_No='' Status='' FileOrder='' DocType='' Order_Type='' ContentType='' EmailFrom='' EmailTo=''	EmailSubject='' EmailBody='' PDFDocFileName='' /><Errors Status=''><Error Code='' Description='' /></Errors></TA_GETMISCSCANNEDDOCUMENT_OUTPUT>"
        Const strSCANDOCUMENTIMAGE_INPUT = "<TA_GETSCANNEDIMAGE_INPUT><ID></ID></TA_GETSCANNEDIMAGE_INPUT>"
        Const strSCANDOCUMENTIMAGE_OUTPUT = "<TA_GETSCANNEDIMAGE_OUTPUT><ID></ID><Document></Document><Errors Status=''><Error Code='' Description='' /></Errors></TA_GETSCANNEDIMAGE_OUTPUT>"
        Const strGetFileNumber_OUTPUT = "<T_C_FILLING_FILENO><FILENUMBER FileNo='' /><Errors Status=''><Error Code='' Description='' /></Errors></T_C_FILLING_FILENO>"

        Const strISPORDER_INPUT = "<TA_UPDATEISPORDER_INPUT><ORDERDETAILS ApplicationNo='' ApprovalDate='' ApprovedBY='' CAFNumber='' CancellationDate='' CancellationReason='' CircuitDeliveryDate='' CommissionedOn='' CreationDate='' DeletedFlag='' ECommissionedOn='' ISPOrderID='' ISPPlanID='' LCODE='' LoggedBy='' MDNNumber='' OrderDate='' OrderNumber='' OrderStatusID='' Remarks='' UserName=''></ORDERDETAILS></TA_UPDATEISPORDER_INPUT>"
        Const strISPORDER_OUTPUT = "<TA_UPDATEISPORDER_OUTPUT><ORDERDETAILS ApplicationNo='' ApprovalDate='' ApprovedBY='' CAFNumber='' CancellationDate='' CancellationReason='' CircuitDeliveryDate='' CommissionedOn='' CreationDate='' DeletedFlag='' ECommissionedOn='' ISPOrderID='' ISPPlanID='' LCODE='' LoggedBy='' MDNNumber='' OrderDate='' OrderNumber='' OrderStatusID='' Remarks='' UserName=''></ORDERDETAILS></TA_UPDATEISPORDER_OUTPUT>"

        Const strEmialDetails_INPUT = "<UP_GETORDEREMAILDETAILS_INPUT><ID></ID></UP_GETORDEREMAILDETAILS_INPUT>"
        Const strEmialDetails_OUTPUT = "<UP_GETORDEREMAILDETAILS_OUTPUT><MailDetail ID='' EmailSubject='' EmailFrom='' EmailTo='' EmailBody='' /><Errors Status=''><Error Code='' Description='' /></Errors></UP_GETORDEREMAILDETAILS_OUTPUT>"
        Const strLISTORDERINGCONFIGVALUE_OUTPUT = "<TA_LISTCONFIG_OUTPUT><CONFIGVALUE CCA_NAME='' FIELD_NAME='' FIELD_VALUE='' /><Errors Status=''><Error Code='' Description='' /></Errors></TA_LISTCONFIG_OUTPUT>"
        Const strORDERNUMBER_OUTPUT = "<TA_ORIGINAL_OUTPUT><ORDER ORDER_ID='' ORDER_NUMBER=''/><Errors Status=''><Error Code='' Description=''/></Errors></TA_ORIGINAL_OUTPUT>"

        Const strUpdateOrderRemarks_OUTPUT = "<TA_UPDATEAGENCYORDERREMARKSDETAILS_OUTPUT><Errors Status=''><Error Code='' Description=''/></Errors></TA_UPDATEAGENCYORDERREMARKSDETAILS_OUTPUT>"
        Const strLISTCANCELLEDORDER_OUTPUT = "<TA_LISTORDERCONFIG_OUTPUT><CONFIGVALUE CCA_NAME='' FIELD_NAME='' FIELD_VALUE='' /><Errors Status=''><Error Code='' Description='' /></Errors></TA_LISTORDERCONFIG_OUTPUT>"
        Const strGetATID = "<TA_GET_ATID_OUTPUT><ATID ORDERID='' ATID = ''  /><Errors Status=''><Error Code='' Description='' /></Errors></TA_GET_ATID_OUTPUT>"

        Public Function GET_ATID(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function gives details of City.
            'Input  :

            '<TA_GET_ATID_INPUT>
            '	 <ATID LCODE="" ORDER_NO=""  />
            '</TA_GET_ATID_INPUT>

            '<TA_GET_ATID_OUTPUT>
            '     	<ATID ORDERID='' ATID = ''  />
            '            	<Errors Status=''>
            '            		<Error Code='' Description='' />
            '            	</Errors>
            '</TA_GET_ATID_OUTPUT>            
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strOrderNo As String
            Dim intLcode As Integer

            Const strMETHOD_NAME As String = "GET_ATID"
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(strGetATID)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start

                intLcode = IndexDoc.DocumentElement.SelectSingleNode("LCODE").InnerText.Trim
                If Len(intLcode) <= 0 Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                strOrderNo = IndexDoc.DocumentElement.SelectSingleNode("ORDER_NO").InnerText.Trim
                If strOrderNo = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_TA_GETORDER_ATID"
                    .Connection = objSqlConnection
                    .Parameters.Add("@LCODE", SqlDbType.Int)
                    .Parameters("@LCODE").Value = intLcode

                    .Parameters.Add("@ORDER_NO", SqlDbType.VarChar, 15)
                    .Parameters("@ORDER_NO").Value = strOrderNo
                End With

                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("ATID")
                        .Attributes("ORDERID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDERID")) & "")
                        .Attributes("ATID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ATID")) & "")

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

        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add
            'Declare Output XML String
            '***********************************************************************
            'Purpose: 
            'Input:None
            'Output:Xml Document
            '<MS_UPDATEORDERS_INPUT>
            '    <ORDERS ORDERID='' ORDERTYPEID='' LCODE='' ORDER_NUMBER='' ORDERSTATUSID='' PROCESSEDBYID='' PLANID='' ISPID='' RESEND_DATE_MKT='' 
            '        RECEIVED_DATE_MKT='' INSTALLATION_DUE_DATE='' EXPECTED_INSTALLATION_DATE='' APR='' OPC='' APC='' NewOrder='' OFFICEID1='' 
            '        OFFICEID='' RECEIVING_OFFICEID='' MSG_SEND_DATE='' APPLIED_DATE='' SENDBACK_DATE='' APPROVAL_DATE='' RECEIVED_DATE='' 
            '         PROCESSED_DATE='' REMARKS='' PENDINGWITHID='' />
            '</MS_UPDATEORDERS_INPUT>
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
            '<MS_DELETEORDER_INPUT>
            '	<ORDERID></ORDERID>
            '</MS_DELETEORDER_INPUT>           
            'Output :
            '<MS_DELETEORDER_OUTPUT>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_DELETEORDER_OUTPUT>            
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
                strORDER_id = DeleteDoc.DocumentElement.SelectSingleNode("ORDERID").InnerText.Trim
                If strORDER_id = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_ORDERS"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"
                    .Parameters.Add(New SqlParameter("@ORDERID", SqlDbType.Int))
                    .Parameters("@ORDERID").Value = strORDER_id
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
            '<UP_SEARCHORDER_INPUT>
            '	<LCODE />
            '	<ORDER_NUMBER />
            '	<ORDERTYPEID />
            '	<ORDERSTATUSID />
            '	<REGION />
            '	<AGENCYNAME />
            '	<GROUPDATA />
            '	<City />
            '	<Country />
            '	<MSG_SEND_DATE_FROM />
            '	<SENDBACK_DATE_FROM />
            '	<APPROVAL_DATE_FROM />
            '	<RECEIVED_DATE_FROM />
            '	<PROCESSED_DATE_FROM />
            '	<MSG_SEND_DATE_TO />
            '	<SENDBACK_DATE_TO />
            '	<APPROVAL_DATE_TO />
            '	<RECEIVED_DATE_TO />
            '	<PROCESSED_DATE_TO />
            '	<Limited_To_OwnAagency>223344</Limited_To_OwnAagency> <!--Send The Location Code -->
            '	<Limited_To_Aoffice>DEL</Limited_To_Aoffice> <!--Send The Aoffice -->
            '	<Limited_To_Region>123</Limited_To_Region> <!--Send The Security Region ID -->
            '</UP_SEARCHORDER_INPUT>            
            'Output :
            '<UP_SEARCHORDER_OUTPUT>
            '	<ORDERS ORDERID='' AGENCYNAME='' ADDRESS='' ADDRESS1='' Region='' CITY='' COUNTRY='' ORDER_NUMBER='' ORDER_TYPE_NAME='' ORDER_STATUS_NAME='' 
            '		 APPROVAL_DATE='' RECEIVED_DATE='' ODDICEID='' OFFICEID1='' MSG_SEND_DATE='' INSTALLATION_DUE_DATE='' PENDINGWITHNAME='' 
            '		RECEIVED_DATE_MKT='' APC='' RESEND_DATE_MKT='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</UP_SEARCHORDER_OUTPUT>

            '        '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean


            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)
            Try
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

                Dim strEmployeeID As String = ""
                If SearchDoc.DocumentElement.SelectSingleNode("EmployeeID") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim() <> "" Then
                        strEmployeeID = SearchDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                    End If
                End If

                Dim strCOMP_VERTICAL As String = ""
                If SearchDoc.DocumentElement.SelectSingleNode("COMP_VERTICAL") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText.Trim() <> "" Then
                        strCOMP_VERTICAL = SearchDoc.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText.Trim()
                    End If
                End If

                With SearchDoc.DocumentElement
                    'If .SelectSingleNode("Limited_To_OwnAagency").InnerXml = "" And .SelectSingleNode("Limited_To_Region").InnerXml = "" And .SelectSingleNode("Limited_To_Aoffice").InnerXml = "" Then
                    'Throw (New AAMSException("Incomplete Parameters"))
                    'End If
                End With
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SER_TA_ORDERS"
                    .Connection = objSqlConnection
                    .Parameters.Add("@LCODE", SqlDbType.Int)
                    .Parameters.Add("@ORDERNUMBER", SqlDbType.VarChar, 12)
                    .Parameters.Add("@ORDERTYPEID", SqlDbType.Int)
                    .Parameters.Add("@ORDERSTATUSID", SqlDbType.Int)
                    .Parameters.Add("@AOFFICE", SqlDbType.Char, 3)
                    .Parameters.Add("@REGION", SqlDbType.VarChar, 10)
                    .Parameters.Add("@AGENCYNAME", SqlDbType.VarChar, 50)
                    .Parameters.Add("@GROUPDATA", SqlDbType.Int)
                    .Parameters.Add("@City", SqlDbType.VarChar, 30)
                    .Parameters.Add("@Country", SqlDbType.VarChar, 25)
                    .Parameters.Add("@OwnAagency", SqlDbType.Int)
                    .Parameters.Add("@SecRegionID", SqlDbType.Int)
                    .Parameters.Add("@MSG_SEND_DATE_FROM", SqlDbType.Int)
                    .Parameters.Add("@MSG_SEND_DATE_TO", SqlDbType.Int)
                    .Parameters.Add("@SENDBACK_DATE_FROM", SqlDbType.Int)
                    .Parameters.Add("@SENDBACK_DATE_TO", SqlDbType.Int)
                    .Parameters.Add("@APPROVAL_DATE_FROM", SqlDbType.Int)
                    .Parameters.Add("@APPROVAL_DATE_TO", SqlDbType.Int)
                    .Parameters.Add("@RECEIVED_DATE_FROM", SqlDbType.Int)
                    .Parameters.Add("@RECEIVED_DATE_TO", SqlDbType.Int)
                    .Parameters.Add("@PROCESSED_DATE_FROM", SqlDbType.Int)
                    .Parameters.Add("@PROCESSED_DATE_TO", SqlDbType.Int)

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

                    .Parameters.Add("@EMPLOYEEID", SqlDbType.Int)
                    If strEmployeeID = "" Then
                        .Parameters("@EMPLOYEEID").Value = DBNull.Value
                    Else
                        .Parameters("@EMPLOYEEID").Value = strEmployeeID
                    End If

                    .Parameters.Add("@COMP_VERTICAL", SqlDbType.SmallInt)
                    If strCOMP_VERTICAL = "" Then
                        .Parameters("@COMP_VERTICAL").Value = DBNull.Value
                    Else
                        .Parameters("@COMP_VERTICAL").Value = CInt(strCOMP_VERTICAL)
                    End If
                End With

                With SearchDoc.DocumentElement
                    If (.SelectSingleNode("LCODE").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@LCODE").Value = CInt(.SelectSingleNode("LCODE").InnerXml)
                    End If
                    If (.SelectSingleNode("ORDER_NUMBER").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@ORDERNUMBER").Value = .SelectSingleNode("ORDER_NUMBER").InnerXml
                    End If
                    If (.SelectSingleNode("ORDERTYPEID").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@ORDERTYPEID").Value = CInt(.SelectSingleNode("ORDERTYPEID").InnerXml)
                    End If
                    If (.SelectSingleNode("ORDERSTATUSID").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@ORDERSTATUSID").Value = CInt(.SelectSingleNode("ORDERSTATUSID").InnerXml)
                    End If
                    If (.SelectSingleNode("Limited_To_Aoffice").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@AOFFICE").Value = .SelectSingleNode("Limited_To_Aoffice").InnerXml
                    End If
                    If (.SelectSingleNode("REGION").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@REGION").Value = .SelectSingleNode("REGION").InnerXml
                    End If
                    If (.SelectSingleNode("AGENCYNAME").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@AGENCYNAME").Value = .SelectSingleNode("AGENCYNAME").InnerXml
                    End If
                    If (.SelectSingleNode("GROUPDATA").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@GROUPDATA").Value = CInt(.SelectSingleNode("GROUPDATA").InnerXml)
                    Else
                        objSqlCommand.Parameters("@GROUPDATA").Value = 0
                    End If
                    If (.SelectSingleNode("City").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@City").Value = .SelectSingleNode("City").InnerXml
                    End If
                    If (.SelectSingleNode("Country").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@Country").Value = .SelectSingleNode("Country").InnerXml
                    End If
                    If (.SelectSingleNode("Limited_To_OwnAagency").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@OwnAagency").Value = CInt(.SelectSingleNode("Limited_To_OwnAagency").InnerXml)
                    End If
                    If (.SelectSingleNode("Limited_To_Region").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@SecRegionID").Value = CInt(.SelectSingleNode("Limited_To_Region").InnerXml)
                    End If
                    If (.SelectSingleNode("MSG_SEND_DATE_FROM").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@MSG_SEND_DATE_FROM").Value = CInt(.SelectSingleNode("MSG_SEND_DATE_FROM").InnerXml)
                    End If
                    If (.SelectSingleNode("MSG_SEND_DATE_TO").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@MSG_SEND_DATE_TO").Value = CInt(.SelectSingleNode("MSG_SEND_DATE_TO").InnerXml)
                    End If
                    If (.SelectSingleNode("SENDBACK_DATE_FROM").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@SENDBACK_DATE_FROM").Value = CInt(.SelectSingleNode("SENDBACK_DATE_FROM").InnerXml)
                    End If
                    If (.SelectSingleNode("SENDBACK_DATE_TO").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@SENDBACK_DATE_TO").Value = CInt(.SelectSingleNode("SENDBACK_DATE_TO").InnerXml)
                    End If
                    If (.SelectSingleNode("APPROVAL_DATE_FROM").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@APPROVAL_DATE_FROM").Value = CInt(.SelectSingleNode("APPROVAL_DATE_FROM").InnerXml)
                    End If
                    If (.SelectSingleNode("APPROVAL_DATE_TO").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@APPROVAL_DATE_TO").Value = CInt(.SelectSingleNode("APPROVAL_DATE_TO").InnerXml)
                    End If
                    If (.SelectSingleNode("RECEIVED_DATE_FROM").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@RECEIVED_DATE_FROM").Value = CInt(.SelectSingleNode("RECEIVED_DATE_FROM").InnerXml)
                    End If
                    If (.SelectSingleNode("RECEIVED_DATE_TO").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@RECEIVED_DATE_TO").Value = CInt(.SelectSingleNode("RECEIVED_DATE_TO").InnerXml)
                    End If
                    If (.SelectSingleNode("PROCESSED_DATE_FROM").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@PROCESSED_DATE_FROM").Value = CInt(.SelectSingleNode("PROCESSED_DATE_FROM").InnerXml)
                    End If
                    If (.SelectSingleNode("PROCESSED_DATE_TO").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@PROCESSED_DATE_TO").Value = CInt(.SelectSingleNode("PROCESSED_DATE_TO").InnerXml)
                    End If
                End With


                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("ORDERS")
                objAptNodeClone = objAptNode.CloneNode(True)
                If objSqlReader.HasRows = True Then objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True

                    objAptNodeClone.Attributes("ORDERID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDERID")) & "")
                    objAptNodeClone.Attributes("ORDER_TYPE_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDER_TYPE_NAME")) & "")
                    objAptNodeClone.Attributes("AGENCYNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NAME")) & "")
                    objAptNodeClone.Attributes("ADDRESS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ADDRESS")) & "")
                    objAptNodeClone.Attributes("ADDRESS1").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ADDRESS1")) & "")
                    objAptNodeClone.Attributes("ORDER_NUMBER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDER_NUMBER")) & "")
                    objAptNodeClone.Attributes("ORDER_STATUS_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDER_STATUS_NAME")) & "")
                    objAptNodeClone.Attributes("Region").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Region")) & "")
                    objAptNodeClone.Attributes("CITY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITY")) & "")
                    objAptNodeClone.Attributes("COUNTRY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("COUNTRY")) & "")
                    objAptNodeClone.Attributes("RESEND_DATE_MKT").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("RESEND_DATE_MKT")) & "")
                    objAptNodeClone.Attributes("RECEIVED_DATE_MKT").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("RECEIVED_DATE_MKT")) & "")
                    objAptNodeClone.Attributes("INSTALLATION_DUE_DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("INSTALLATION_DUE_DATE")) & "")
                    objAptNodeClone.Attributes("APC").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("APC")) & "")
                    objAptNodeClone.Attributes("OFFICEID1").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OFFICEID1")) & "")
                    objAptNodeClone.Attributes("ODDICEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OFFICEID")) & "")
                    objAptNodeClone.Attributes("MSG_SEND_DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MSG_SEND_DATE")) & "")
                    objAptNodeClone.Attributes("APPROVAL_DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("APPROVAL_DATE")) & "")
                    objAptNodeClone.Attributes("RECEIVED_DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("RECEIVED_DATE")) & "")
                    objAptNodeClone.Attributes("PENDINGWITHNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PENDING_WITH_NAME")) & "")
                    objAptNodeClone.Attributes("REMARKS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("REMARKS")) & "")


                    strCOMP_VERTICAL = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("COMP_VERTICAL")) & "")
                    objAptNodeClone.Attributes("COMP_VERTICAL").InnerText = IIf(strCOMP_VERTICAL = "1", "Amadeus", IIf(strCOMP_VERTICAL = "2", "ResBird", (IIf(strCOMP_VERTICAL = "3", "Non1A", ""))))

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
            '<MS_UPDATEORDERS_INPUT>
            '    <ORDERS ORDERID='' ORDERTYPEID='' LCODE='' ORDER_NUMBER='' ORDERSTATUSID='' PROCESSEDBYID='' PLANID='' ISPID='' RESEND_DATE_MKT='' 
            '        RECEIVED_DATE_MKT='' INSTALLATION_DUE_DATE='' EXPECTED_INSTALLATION_DATE='' APR='' OPC='' APC='' NewOrder='' OFFICEID1='' 
            '        OFFICEID='' RECEIVING_OFFICEID='' MSG_SEND_DATE='' APPLIED_DATE='' SENDBACK_DATE='' APPROVAL_DATE='' RECEIVED_DATE='' 
            '        PROCESSED_DATE='' REMARKS='' PENDINGWITHID='' EMPLOYEEID='' ATID='' />
            '</MS_UPDATEORDERS_INPUT>
            'Output :
            '<MS_UPDATEORDERS_OUTPUT>
            '    <ORDERS ORDERID='' ORDERTYPEID='' LCODE='' ORDER_NUMBER='' ORDERSTATUSID='' PROCESSEDBYID='' PLANID='' ISPID='' RESEND_DATE_MKT='' 
            '        RECEIVED_DATE_MKT='' INSTALLATION_DUE_DATE='' EXPECTED_INSTALLATION_DATE='' APR='' OPC='' APC='' NewOrder='' OFFICEID1='' 
            '        OFFICEID='' RECEIVING_OFFICEID='' MSG_SEND_DATE='' APPLIED_DATE='' SENDBACK_DATE='' APPROVAL_DATE='' RECEIVED_DATE='' 
            '         PROCESSED_DATE='' REMARKS='' PENDINGWITHID='' ATID='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_UPDATEORDERS_OUTPUT>
            '************************************************************************
            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objHisSqlCommand As New SqlCommand

            '' Dim objISPOrderCommand As New SqlCommand

            Dim objTran As SqlTransaction

            Dim objUpdateDocOutput As New XmlDocument
            Dim intRecordsAffected As Int32
            Dim objNode As XmlNode
            Const strMETHOD_NAME As String = "Update"
            Dim strCOMP_VERTICAL As String = ""
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)

                objNode = objUpdateDocOutput.DocumentElement.SelectSingleNode("ORDERS")
                objUpdateDocOutput.DocumentElement.RemoveChild(objNode)
                objUpdateDocOutput.DocumentElement.AppendChild(objUpdateDocOutput.ImportNode(UpdateDoc.DocumentElement.SelectSingleNode("ORDERS"), True))

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("ORDERS")
                    If ((.Attributes("ORDERID").InnerText).Trim) = "" Then
                        strAction = "I"
                    Else
                        strAction = "U"
                    End If
                    If (.Attributes("ORDERSTATUSID").InnerText).Trim = "" Then
                        Throw (New AAMSException("Order Status can't be blank."))
                    End If
                    If (.Attributes("ORDERTYPEID").InnerText).Trim = "" Then
                        Throw (New AAMSException("Order Type can't be blank."))
                    End If
                    If (.Attributes("ORDERTYPEID").InnerText).Trim = "" Then
                        Throw (New AAMSException("Order Type can't be blank."))
                    End If
                    If (.Attributes("LCODE").InnerText).Trim = "" Then
                        Throw (New AAMSException("Agency Name can't be blank."))
                    End If
                    If (.Attributes("RECEIVED_DATE").InnerText).Trim = "" Then
                        Throw (New AAMSException("Order recevied date can't be blank."))
                    End If

                    'If (.Attributes("ATID").InnerText).Trim = "" Then
                    '    Throw (New AAMSException("ATID can't be blank."))
                    'End If
                    If .Attributes("COMP_VERTICAL") IsNot Nothing Then
                        strCOMP_VERTICAL = .Attributes("COMP_VERTICAL").InnerText
                    End If

                    'If strCOMP_VERTICAL = "" Then Throw (New AAMSException("Invalid Company vertical."))
                    'If strCOMP_VERTICAL = "3" Then Throw (New AAMSException("Please select either Amadeus or ResBird to process the order."))


                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_ORDERS"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters.Add(New SqlParameter("@ORDERID", SqlDbType.BigInt))
                    .Parameters.Add(New SqlParameter("@ORDERTYPEID", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@OTRNO1", SqlDbType.Char, 10))
                    .Parameters.Add(New SqlParameter("@OTRNO", SqlDbType.Char, 10))
                    .Parameters.Add(New SqlParameter("@EPICNO", SqlDbType.Char, 10))
                    .Parameters.Add(New SqlParameter("@ORDER_NUMBER", SqlDbType.Char, 12))
                    .Parameters.Add(New SqlParameter("@ORDERSTATUSID", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@PROCESSEDBYID", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@PLANID", SqlDbType.BigInt))
                    .Parameters.Add(New SqlParameter("@ISPID", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@LOOPCOST", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@APR", SqlDbType.SmallInt))
                    .Parameters.Add(New SqlParameter("@OPC", SqlDbType.SmallInt))
                    .Parameters.Add(New SqlParameter("@APC", SqlDbType.SmallInt))
                    .Parameters.Add(New SqlParameter("@NewOrder", SqlDbType.Char, 1))
                    .Parameters.Add(New SqlParameter("@SITA_CODE", SqlDbType.Char, 15))
                    .Parameters.Add(New SqlParameter("@OFFICEID1", SqlDbType.Char, 15))
                    .Parameters.Add(New SqlParameter("@OFFICEID", SqlDbType.Char, 15))
                    .Parameters.Add(New SqlParameter("@RECEIVING_OFFICEID", SqlDbType.Char, 15))
                    .Parameters.Add(New SqlParameter("@REMARKS", SqlDbType.VarChar, 1000))
                    .Parameters.Add(New SqlParameter("@PENDINGWITHID", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@PROCESSED_DATE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@RECEIVED_DATE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@APPROVAL_DATE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@SENDBACK_DATE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@APPLIED_DATE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@MSG_SEND_DATE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@RESEND_DATE_MKT", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@RECEIVED_DATE_MKT", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@INSTALLATION_DUE_DATE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@EXPECTED_INSTALLATION_DATE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@LOOPCOST_SITA_ORDER_DATE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@LOOPCOST_RECV_DATE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@LOOPCOST_SEND_MSG_DATE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@EMPLOYEEID", SqlDbType.Int))
                    '.Parameters.Add(New SqlParameter("@ATID", SqlDbType.VarChar, 224))
                    .Parameters.Add(New SqlParameter("@ATID", SqlDbType.VarChar, 1900))


                    .Parameters.Add(New SqlParameter("@COMP_VERTICAL", SqlDbType.SmallInt))

                    .Parameters.Add(New SqlParameter("@ID", SqlDbType.Int))
                    .Parameters("@ID").Direction = ParameterDirection.Output
                    .Parameters("@ID").Value = 0
                End With

                'ADDING PARAMETERS FOR HISTORY IN STORED PROCEDURE
                With objHisSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_ORDERSHISTORY"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters.Add(New SqlParameter("@ORDERID", SqlDbType.BigInt))
                    .Parameters.Add(New SqlParameter("@ORDERTYPEID", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@OTRNO1", SqlDbType.Char, 10))
                    .Parameters.Add(New SqlParameter("@OTRNO", SqlDbType.Char, 10))
                    .Parameters.Add(New SqlParameter("@EPICNO", SqlDbType.Char, 10))
                    .Parameters.Add(New SqlParameter("@ORDER_NUMBER", SqlDbType.Char, 12))
                    .Parameters.Add(New SqlParameter("@ORDERSTATUSID", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@PROCESSEDBYID", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@PLANID", SqlDbType.VarChar, 20))
                    .Parameters.Add(New SqlParameter("@ISPID", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@LOOPCOST", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@APR", SqlDbType.SmallInt))
                    .Parameters.Add(New SqlParameter("@OPC", SqlDbType.SmallInt))
                    .Parameters.Add(New SqlParameter("@APC", SqlDbType.SmallInt))
                    .Parameters.Add(New SqlParameter("@NewOrder", SqlDbType.Char, 1))
                    .Parameters.Add(New SqlParameter("@SITA_CODE", SqlDbType.Char, 15))
                    .Parameters.Add(New SqlParameter("@OFFICEID1", SqlDbType.Char, 15))
                    .Parameters.Add(New SqlParameter("@OFFICEID", SqlDbType.Char, 15))
                    .Parameters.Add(New SqlParameter("@RECEIVING_OFFICEID", SqlDbType.Char, 15))
                    .Parameters.Add(New SqlParameter("@REMARKS", SqlDbType.VarChar, 1000))
                    .Parameters.Add(New SqlParameter("@PENDINGWITHID", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@PROCESSED_DATE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@RECEIVED_DATE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@APPROVAL_DATE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@SENDBACK_DATE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@APPLIED_DATE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@MSG_SEND_DATE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@RESEND_DATE_MKT", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@RECEIVED_DATE_MKT", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@INSTALLATION_DUE_DATE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@EXPECTED_INSTALLATION_DATE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@LOOPCOST_SITA_ORDER_DATE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@LOOPCOST_RECV_DATE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@LOOPCOST_SEND_MSG_DATE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@EMPLOYEEID", SqlDbType.Int))

                    .Parameters.Add(New SqlParameter("@COMP_VERTICAL", SqlDbType.SmallInt))

                    .Parameters.Add(New SqlParameter("@ID", SqlDbType.Int))
                    .Parameters("@ID").Direction = ParameterDirection.Output
                    .Parameters("@ID").Value = 0
                End With
                '***************************************************************************************


                With UpdateDoc.DocumentElement.SelectSingleNode("ORDERS")
                    objSqlCommand.Parameters("@ACTION").Value = strAction
                    objHisSqlCommand.Parameters("@ACTION").Value = strAction

                    If (.Attributes("ORDERID").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@ORDERID").Value = CInt(.Attributes("ORDERID").InnerXml)
                        objHisSqlCommand.Parameters("@ORDERID").Value = CInt(.Attributes("ORDERID").InnerXml)
                    End If
                    If (.Attributes("ORDERTYPEID").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@ORDERTYPEID").Value = CInt(.Attributes("ORDERTYPEID").InnerXml)
                        objHisSqlCommand.Parameters("@ORDERTYPEID").Value = CInt(.Attributes("ORDERTYPEID").InnerXml)
                    End If
                    If (.Attributes("LCODE").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@LCODE").Value = CInt(.Attributes("LCODE").InnerXml)
                        objHisSqlCommand.Parameters("@LCODE").Value = CInt(.Attributes("LCODE").InnerXml)
                    End If
                    If (.Attributes("ORDER_NUMBER").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@ORDER_NUMBER").Value = .Attributes("ORDER_NUMBER").InnerXml
                        objHisSqlCommand.Parameters("@ORDER_NUMBER").Value = .Attributes("ORDER_NUMBER").InnerXml
                    End If
                    If (.Attributes("ORDERSTATUSID").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@ORDERSTATUSID").Value = CInt(.Attributes("ORDERSTATUSID").InnerXml)
                        objHisSqlCommand.Parameters("@ORDERSTATUSID").Value = CInt(.Attributes("ORDERSTATUSID").InnerXml)
                    End If
                    If (.Attributes("PROCESSEDBYID").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@PROCESSEDBYID").Value = CInt(.Attributes("PROCESSEDBYID").InnerXml)
                        objHisSqlCommand.Parameters("@PROCESSEDBYID").Value = CInt(.Attributes("PROCESSEDBYID").InnerXml)
                    End If
                    If (.Attributes("PLANID").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@PLANID").Value = CInt(.Attributes("PLANID").InnerXml & "")
                        objHisSqlCommand.Parameters("@PLANID").Value = .Attributes("PLANID").InnerXml
                    End If
                    If (.Attributes("ISPID").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@ISPID").Value = CInt(.Attributes("ISPID").InnerXml)
                        objHisSqlCommand.Parameters("@ISPID").Value = CInt(.Attributes("ISPID").InnerXml)
                    End If
                    If (.Attributes("RESEND_DATE_MKT").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@RESEND_DATE_MKT").Value = CInt(.Attributes("RESEND_DATE_MKT").InnerXml)
                        objHisSqlCommand.Parameters("@RESEND_DATE_MKT").Value = CInt(.Attributes("RESEND_DATE_MKT").InnerXml)
                    End If
                    If (.Attributes("RECEIVED_DATE_MKT").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@RECEIVED_DATE_MKT").Value = CInt(.Attributes("RECEIVED_DATE_MKT").InnerXml)
                        objHisSqlCommand.Parameters("@RECEIVED_DATE_MKT").Value = CInt(.Attributes("RECEIVED_DATE_MKT").InnerXml)
                    End If
                    If (.Attributes("INSTALLATION_DUE_DATE").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@INSTALLATION_DUE_DATE").Value = CInt(.Attributes("INSTALLATION_DUE_DATE").InnerXml)
                        objHisSqlCommand.Parameters("@INSTALLATION_DUE_DATE").Value = CInt(.Attributes("INSTALLATION_DUE_DATE").InnerXml)
                    End If
                    If (.Attributes("EXPECTED_INSTALLATION_DATE").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@EXPECTED_INSTALLATION_DATE").Value = CInt(.Attributes("EXPECTED_INSTALLATION_DATE").InnerXml)
                        objHisSqlCommand.Parameters("@EXPECTED_INSTALLATION_DATE").Value = CInt(.Attributes("EXPECTED_INSTALLATION_DATE").InnerXml)
                    End If
                    If (.Attributes("APR").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@APR").Value = CInt(.Attributes("APR").InnerXml)
                        objHisSqlCommand.Parameters("@APR").Value = CInt(.Attributes("APR").InnerXml)
                    End If
                    If (.Attributes("OPC").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@OPC").Value = CInt(.Attributes("OPC").InnerXml)
                        objHisSqlCommand.Parameters("@OPC").Value = CInt(.Attributes("OPC").InnerXml)
                    End If
                    If (.Attributes("APC").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@APC").Value = CInt(.Attributes("APC").InnerXml)
                        objHisSqlCommand.Parameters("@APC").Value = CInt(.Attributes("APC").InnerXml)
                    End If
                    If (.Attributes("NewOrder").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@NewOrder").Value = .Attributes("NewOrder").InnerXml
                        objHisSqlCommand.Parameters("@NewOrder").Value = .Attributes("NewOrder").InnerXml
                    End If
                    If (.Attributes("OFFICEID1").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@OFFICEID1").Value = .Attributes("OFFICEID1").InnerXml
                        objHisSqlCommand.Parameters("@OFFICEID1").Value = .Attributes("OFFICEID1").InnerXml
                    End If
                    If (.Attributes("OFFICEID").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@OFFICEID").Value = .Attributes("OFFICEID").InnerXml
                        objHisSqlCommand.Parameters("@OFFICEID").Value = .Attributes("OFFICEID").InnerXml
                    End If
                    If (.Attributes("RECEIVING_OFFICEID").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@RECEIVING_OFFICEID").Value = .Attributes("RECEIVING_OFFICEID").InnerXml
                        objHisSqlCommand.Parameters("@RECEIVING_OFFICEID").Value = .Attributes("RECEIVING_OFFICEID").InnerXml
                    End If
                    If (.Attributes("MSG_SEND_DATE").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@MSG_SEND_DATE").Value = CInt(.Attributes("MSG_SEND_DATE").InnerXml)
                        objHisSqlCommand.Parameters("@MSG_SEND_DATE").Value = CInt(.Attributes("MSG_SEND_DATE").InnerXml)
                    End If
                    If (.Attributes("SENDBACK_DATE").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@SENDBACK_DATE").Value = CInt(.Attributes("SENDBACK_DATE").InnerXml)
                        objHisSqlCommand.Parameters("@SENDBACK_DATE").Value = CInt(.Attributes("SENDBACK_DATE").InnerXml)
                    End If
                    If (.Attributes("APPROVAL_DATE").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@APPROVAL_DATE").Value = CInt(.Attributes("APPROVAL_DATE").InnerXml)
                        objHisSqlCommand.Parameters("@APPROVAL_DATE").Value = CInt(.Attributes("APPROVAL_DATE").InnerXml)
                    End If
                    If (.Attributes("RECEIVED_DATE").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@RECEIVED_DATE").Value = CInt(.Attributes("RECEIVED_DATE").InnerXml)
                        objHisSqlCommand.Parameters("@RECEIVED_DATE").Value = CInt(.Attributes("RECEIVED_DATE").InnerXml)
                    End If
                    If (.Attributes("APPLIED_DATE").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@APPLIED_DATE").Value = CInt(.Attributes("APPLIED_DATE").InnerXml)
                        objHisSqlCommand.Parameters("@APPLIED_DATE").Value = CInt(.Attributes("APPLIED_DATE").InnerXml)
                    End If
                    If (.Attributes("PROCESSED_DATE").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@PROCESSED_DATE").Value = CInt(.Attributes("PROCESSED_DATE").InnerXml)
                        objHisSqlCommand.Parameters("@PROCESSED_DATE").Value = CInt(.Attributes("PROCESSED_DATE").InnerXml)
                    End If
                    If (.Attributes("REMARKS").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@REMARKS").Value = .Attributes("REMARKS").InnerText.Trim
                        objHisSqlCommand.Parameters("@REMARKS").Value = .Attributes("REMARKS").InnerText.Trim
                    End If
                    If (.Attributes("PENDINGWITHID").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@PENDINGWITHID").Value = .Attributes("PENDINGWITHID").InnerXml
                        objHisSqlCommand.Parameters("@PENDINGWITHID").Value = .Attributes("PENDINGWITHID").InnerXml
                    End If
                    
                    If (.Attributes("EMPLOYEEID").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@EMPLOYEEID").Value = .Attributes("EMPLOYEEID").InnerXml
                        objHisSqlCommand.Parameters("@EMPLOYEEID").Value = .Attributes("EMPLOYEEID").InnerXml
                    End If


                    If (.Attributes("ATID").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@ATID").Value = .Attributes("ATID").InnerXml
                        'objHisSqlCommand.Parameters("@ATID").Value = .Attributes("ATID").InnerXml
                    End If


                  
                    If strCOMP_VERTICAL = "" Then
                        objSqlCommand.Parameters("@COMP_VERTICAL").Value = DBNull.Value
                        objHisSqlCommand.Parameters("@COMP_VERTICAL").Value = DBNull.Value
                    Else
                        objSqlCommand.Parameters("@COMP_VERTICAL").Value = CInt(strCOMP_VERTICAL)
                        objHisSqlCommand.Parameters("@COMP_VERTICAL").Value = CInt(strCOMP_VERTICAL)
                    End If

                End With

                objSqlCommand.Connection.Open()
                objTran = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)
                objSqlCommand.Transaction = objTran
                objHisSqlCommand.Transaction = objTran


                '**** Save History
                If UCase(strAction) = "U" Then
                    intRecordsAffected = objHisSqlCommand.ExecuteNonQuery()
                    intRetId = objHisSqlCommand.Parameters("@ID").Value
                End If
                '**** End

                intRecordsAffected = objSqlCommand.ExecuteNonQuery()


                intRetId = objSqlCommand.Parameters("@ID").Value
                If UCase(strAction) = "I" Then
                    intRetId = objSqlCommand.Parameters("@ID").Value
                    If intRetId = -1 Then
                        Throw (New AAMSException("Order Already Exists!"))
                    Else
                        objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                        With objUpdateDocOutput.DocumentElement.SelectSingleNode("ORDERS")
                            .Attributes("ORDERID").InnerText = intRetId
                        End With
                    End If
                ElseIf UCase(strAction) = "U" Then
                    intRetId = objSqlCommand.Parameters("@ID").Value
                    If intRetId <= 0 Then
                        Throw (New AAMSException("Unable to update!"))
                    Else
                        objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    End If
                End If
                objTran.Commit()
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                If Not objTran Is Nothing Then
                    objTran.Rollback()
                End If
                bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                Return objUpdateDocOutput
            Catch Exec As Exception
                If Not objTran Is Nothing Then
                    objTran.Rollback()
                End If
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                Return objUpdateDocOutput
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
                objHisSqlCommand.Dispose()
            End Try
            Return objUpdateDocOutput
        End Function

        Public Function UpdateOrderRemarks(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************            
            'Purpose: To list out the those  record having fileno status is zero, 
            'Input  : 
            '<TA_UPDATEAGENCYORDERREMARKSDETAILS_INPUT>
            '	<DETAILS ORDERID='' REMARKS=''/>
            '</TA_UPDATEAGENCYORDERREMARKSDETAILS_INPUT>

            'Output :  
            '<TA_UPDATEAGENCYORDERREMARKSDETAILS_OUTPUT>
            '	<Errors Status="">
            '		<Error Code="" Description=""/>
            '	</Errors>
            '</TA_UPDATEAGENCYORDERREMARKSDETAILS_OUTPUT>
            '***********************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim strOrderID As String = ""
            Dim strOrderRemarks As String = ""

            Dim intRetId As Integer
            Dim intRecordsAffected As Integer

            Dim strMETHOD_NAME As String = "UpdateOrderRemarks"

            objOutputXml.LoadXml(strUpdateOrderRemarks_OUTPUT)
            Try
                If UpdateDoc.DocumentElement.SelectSingleNode("DETAILS").Attributes("ORDERID") IsNot Nothing Then
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAILS").Attributes("ORDERID").InnerText <> "" Then
                        strOrderID = UpdateDoc.DocumentElement.SelectSingleNode("DETAILS").Attributes("ORDERID").InnerText
                    End If
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("DETAILS").Attributes("REMARKS") IsNot Nothing Then
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAILS").Attributes("REMARKS").InnerText <> "" Then
                        strOrderRemarks = UpdateDoc.DocumentElement.SelectSingleNode("DETAILS").Attributes("REMARKS").InnerText
                    End If
                End If
                If strOrderID = "" Then
                    Throw (New AAMSException("Invalid Parameter Input OrderID!"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_SRO_TA_ORDERREMARKS]"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@ORDERID", SqlDbType.Int))
                    .Parameters("@ORDERID").Value = Val(strOrderID)

                    .Parameters.Add(New SqlParameter("@REMARKS", SqlDbType.VarChar, 300))
                    .Parameters("@REMARKS").Value = strOrderRemarks


                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0

                    .Connection.Open()
                    intRecordsAffected = .ExecuteNonQuery()

                    intRetId = .Parameters("@RETURNID").Value

                    If intRetId = 0 Then
                        Throw (New AAMSException("Unable to Update!"))
                    Else
                        'objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("ORDERID").InnerText = strOrderID
                        'objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("REMARKS").InnerText = strOrderRemarks

                        objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    End If
                End With

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
            '<MS_VIEWORDER_INPUT>
            '	<ORDERID/>
            '</MS_VIEWORDER_INPUT>
            'Output :
            '<MS_VIEWORDERS_OUTPUT>
            '	<ORDERS ORDERID='' ORDERTYPE = '' ORDERTYPEID='' LCODE='' NAME='' ADDRESS='' ADDRESS1='' FILENO='' ORDER_NUMBER='' ORDERSTATUSID='' PROCESSEDBYID='' PROCESSEDBYNAME='' 
            '		 PLANID='' ISPNAME='' ISPID='' RESEND_DATE_MKT='' RECEIVED_DATE_MKT='' INSTALLATION_DUE_DATE='' EXPECTED_INSTALLATION_DATE='' APR='' OPC='' 
            '		 APC='' NewOrder='' OFFICEID1='' OFFICEID='' RECEIVING_OFFICEID='' MSG_SEND_DATE='' APPLIED_DATE='' SENDBACK_DATE='' APPROVAL_DATE='' 
            '		 RECEIVED_DATE='' PROCESSED_DATE='' REMARKS='' PENDINGWITHID='' PENDINGWITHNAME='' NPID='' ATID='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_VIEWORDERS_OUTPUT>            
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strOder_ID As String
            Const strMETHOD_NAME As String = "View"
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(srtVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strOder_ID = IndexDoc.DocumentElement.SelectSingleNode("ORDERID").InnerText.Trim
                If strOder_ID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_ORDERS"
                    .Connection = objSqlConnection
                    .Parameters.Add("@ORDERID", SqlDbType.Int)
                    .Parameters("@ORDERID").Value = strOder_ID
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("ORDERS")
                        .Attributes("ORDERID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDERID")) & "")
                        .Attributes("ORDERTYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDERTYPE")) & "")
                        .Attributes("ORDERTYPEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDERTYPEID")) & "")
                        .Attributes("LCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")) & "")
                        .Attributes("NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NAME")) & "")
                        .Attributes("ADDRESS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ADDRESS")) & "")
                        .Attributes("ADDRESS1").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ADDRESS1")) & "")
                        .Attributes("FILENO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FILENO")) & "")
                        .Attributes("ORDER_NUMBER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDER_NUMBER")) & "")
                        .Attributes("ORDERSTATUSID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDERSTATUSID")) & "")
                        .Attributes("PROCESSEDBYID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PROCESSEDBYID")) & "")
                        .Attributes("PROCESSEDBYNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PROCESSEDBY_NAME")) & "")
                        .Attributes("PLANID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PLANID")) & "")
                        .Attributes("ISPNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ISPNAME")) & "")
                        .Attributes("ISPID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ISPID")) & "")
                        .Attributes("RESEND_DATE_MKT").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("RESEND_DATE_MKT")) & "")
                        .Attributes("RECEIVED_DATE_MKT").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("RECEIVED_DATE_MKT")) & "")
                        .Attributes("INSTALLATION_DUE_DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("INSTALLATION_DUE_DATE")) & "")
                        .Attributes("EXPECTED_INSTALLATION_DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EXPECTED_INSTALLATION_DATE")) & "")
                        .Attributes("APR").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("APR")) & "")
                        .Attributes("OPC").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OPC")) & "")
                        .Attributes("APC").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("APC")) & "")
                        .Attributes("NewOrder").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NewOrder")) & "")
                        .Attributes("OFFICEID1").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OFFICEID1")) & "")
                        .Attributes("OFFICEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OFFICEID")) & "")
                        .Attributes("RECEIVING_OFFICEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("RECEIVING_OFFICEID")) & "")
                        .Attributes("MSG_SEND_DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MSG_SEND_DATE")) & "")
                        .Attributes("APPLIED_DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("APPLIED_DATE")) & "")
                        .Attributes("SENDBACK_DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SENDBACK_DATE")) & "")
                        .Attributes("APPROVAL_DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("APPROVAL_DATE")) & "")
                        .Attributes("RECEIVED_DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("RECEIVED_DATE")) & "")
                        .Attributes("PROCESSED_DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PROCESSED_DATE")) & "")
                        .Attributes("REMARKS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("REMARKS")) & "")

                        .Attributes("ATID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ATID")) & "")  ''AADED BY ASHISH

                        .Attributes("PENDINGWITHID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PENDINGWITHID")) & "")
                        .Attributes("PENDINGWITHNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PENDING_WITH_NAME")) & "")
                        .Attributes("NPID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NPID")) & "")
                        .Attributes("CHAIN_CODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHAIN_CODE")) & "")
                        .Attributes("CHAIN_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHAIN_NAME")) & "")
                        .Attributes("NEW_ORD_MSG").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NEW_ORDER_MSG")) & "")
                        .Attributes("LCOUNT").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCOUNT")) & "")

                    End With
                    blnRecordFound = True
                Loop
                objSqlReader.NextResult()

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
        Public Function GetDetails(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function gives search results based on choosen search criterion.
            'Input  :- 
            '<MS_GETORDERS_INPUT>
            '	<LCODE />
            '</MS_GETORDERS_INPUT>            
            'Output :
            '<MS_GETORDERS_OUTPUT>
            '	<ORDERS ORDERID='' ORDER_NUMBER='' ORDER_TYPE_NAME='' ORDER_STATUS_NAME='' APPROVAL_DATE=''
            '		APPLIED_DATE='' ODDICEID='' OFFICEID1='' PENDINGWITHNAME='' REMARKS='' APC='' OPC='' APR='' />
            '   <PAGE PAGE_COUNT='' TOTAL_ROWS=''/>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_GETORDERS_OUTPUT>
            '        '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Const strMETHOD_NAME As String = "GetDetails"
            objOutputXml.LoadXml(srtGET_OUTPUT)
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

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_GET_TA_ORDERS"
                    .Connection = objSqlConnection
                    .Parameters.Add("@LCODE", SqlDbType.Int)

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

                With SearchDoc.DocumentElement
                    objSqlCommand.Parameters("@LCODE").Value = CInt(.SelectSingleNode("LCODE").InnerXml)
                End With



                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("ORDERS")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("ORDERID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDERID")) & "")
                    objAptNodeClone.Attributes("ORDER_TYPE_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDER_TYPE_NAME")) & "")
                    objAptNodeClone.Attributes("ORDER_NUMBER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDER_NUMBER")) & "")
                    objAptNodeClone.Attributes("ORDER_STATUS_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDER_STATUS_NAME")) & "")
                    objAptNodeClone.Attributes("REMARKS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("REMARKS")) & "")
                    objAptNodeClone.Attributes("APC").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("APC")) & "")
                    objAptNodeClone.Attributes("OPC").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OPC")) & "")
                    objAptNodeClone.Attributes("APR").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("APR")) & "")
                    objAptNodeClone.Attributes("OFFICEID1").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OFFICEID1")) & "")
                    objAptNodeClone.Attributes("OFFICEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OFFICEID")) & "")
                    objAptNodeClone.Attributes("APPLIED_DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("APPLIED_DATE")) & "")
                    objAptNodeClone.Attributes("APPROVAL_DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("APPROVAL_DATE")) & "")
                    objAptNodeClone.Attributes("PENDINGWITHNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PENDING_WITH_NAME")) & "")
                    objAptNodeClone.Attributes("ISORDERTYPEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ISORDERTYPEID")) & "")


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
        Public Function GetHistory(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function gives details of City.
            'Input  :
            '<MS_GETHISTORYORDER_INPUT>
            '   <ORDERID></ORDERID>
            '</MS_GETHISTORYORDER_INPUT>

            'Output :
            '<MS_GETHISTORYORDER_OUTPUT>
            '   <HISTORYDETAIL  ORDERID='' EMPLOYEENAME='' CHANGEDATA='' DATETIME='' />
            '       <Errors Status=''>
            '		    <Error Code='' Description='' />
            '   	</Errors>
            '</MS_GETHISTORYORDER_OUTPUT>            
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim strOder_ID As String
            Const strMETHOD_NAME As String = "GetHistory"
            Dim blnRecordFound As Boolean
            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean
            Try
                objOutputXml.LoadXml(strHISTORY_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strOder_ID = SearchDoc.DocumentElement.SelectSingleNode("ORDERID").InnerText.Trim
                If strOder_ID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Paging Section    
                If SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim() <> "" Then
                        intPageNo = SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim()
                    End If
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim() <> "" Then
                        intPageSize = SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim()
                    End If
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("SORT_BY") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim() <> "" Then
                        strSortBy = SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim()
                    End If
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("DESC") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("DESC").InnerText.Trim().ToUpper = "TRUE" Then
                        blnDesc = True
                    Else
                        blnDesc = False
                    End If
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_GET_TA_ORDERHISTORY"
                    .Connection = objSqlConnection
                    .Parameters.Add("@ORDERID", SqlDbType.Int)
                    .Parameters("@ORDERID").Value = strOder_ID
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

                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("HISTORYDETAIL")

                If objSqlReader.HasRows Then
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.RemoveChild(objAptNode)
                End If

                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("EMPLOYEENAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Employee_Name")) & "")
                    objAptNodeClone.Attributes("DATETIME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DateTime")) & "")
                    objAptNodeClone.Attributes("CHANGEDATA").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHANGEDATA")) & "")

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)

                    blnRecordFound = True
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
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objOutputXml, "103", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function

        Public Function GetScannedDocument(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function gives details Scanned Document
            'Input  :
            '<TA_GETSCANNEDDOCUMENT_INPUT>
            '<LCode></LCode>
            '<FileNo></FileNo>
            '<Order_No></Order_No>
            '</TA_GETSCANNEDDOCUMENT_INPUT>

            'Output :
            '<TA_GETSCANNEDDOCUMENT_OUTPUT>
            '<Document ID='' FileNo='' Order_No='' Status='' FileOrder='' DocType='' Order_Type='' ContentType='' EmailFrom=''	EmailTo	=''	EmailSubject='' EmailBody=''	PDFDocFileName='' />
            '<Errors Status=''><Error Code='' Description='' />
            '</Errors>
            '</TA_GETSCANNEDDOCUMENT_OUTPUT>            
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetDOCConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim strFileNo As Integer
            Dim strOrderNo As String
            Dim intLCode As UInteger

            Const strMETHOD_NAME As String = "GetScannedDocument"
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(strSCANDOCUMENT_OUTPUT)

                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                If SearchDoc.DocumentElement.SelectSingleNode("FileNo").InnerText.Trim <> "" Then
                    strFileNo = SearchDoc.DocumentElement.SelectSingleNode("FileNo").InnerText.Trim
                End If

                strOrderNo = SearchDoc.DocumentElement.SelectSingleNode("Order_No").InnerText.Trim
                If SearchDoc.DocumentElement.SelectSingleNode("LCode").InnerText.Trim <> "" Then
                    intLCode = SearchDoc.DocumentElement.SelectSingleNode("LCode").InnerText.Trim
                End If
                If intLCode = 0 Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_GET_TA_GETSCANNEDDOCUMENT"
                    .Connection = objSqlConnection

                    .Parameters.Add("@LCODE", SqlDbType.BigInt)
                    .Parameters("@LCODE").Value = intLCode

                    .Parameters.Add("@FILENO", SqlDbType.Int)
                    If strFileNo = 0 Then
                        .Parameters("@FILENO").Value = DBNull.Value
                    Else
                        .Parameters("@FILENO").Value = strFileNo
                    End If


                    .Parameters.Add("@ORDERNO", SqlDbType.VarChar, 40)
                    .Parameters("@ORDERNO").Value = strOrderNo

                End With

                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("Document")

                If objSqlReader.HasRows Then
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.RemoveChild(objAptNode)
                End If

                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ID")) & "")
                    objAptNodeClone.Attributes("FileNo").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FileNo")) & "")
                    objAptNodeClone.Attributes("Order_No").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Order_No")) & "")
                    objAptNodeClone.Attributes("Status").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Status")) & "")
                    objAptNodeClone.Attributes("FileOrder").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FileOrder")) & "")
                    objAptNodeClone.Attributes("DocType").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DocType")) & "")
                    objAptNodeClone.Attributes("Order_Type").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Order_Type")) & "")

                    objAptNodeClone.Attributes("ContentType").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ContentType")) & "")
                    objAptNodeClone.Attributes("EmailSubject").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailSubject")) & "")
                    objAptNodeClone.Attributes("EmailFrom").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailFrom")) & "")
                    objAptNodeClone.Attributes("EmailTo").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailTo")) & "")
                    objAptNodeClone.Attributes("EmailBody").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailBody")) & "")
                    objAptNodeClone.Attributes("PDFDocFileName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PDFDocFileName")) & "")

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)

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

        Public Function GetMiscDocumentDetail(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function gives details Scanned Document
            'Input  :
            '<TA_GETSCANNEDDOCUMENT_INPUT>
            '<LCode></LCode>
            '<FileNo></FileNo>
            '</TA_GETSCANNEDDOCUMENT_INPUT>

            'Output :
            '<TA_GETSCANNEDDOCUMENT_OUTPUT>
            '<Document ID='' FileNo='' Order_No='' Status='' FileOrder='' DocType='' Order_Type='' ContentType='' EmailFrom=''	EmailTo	=''	EmailSubject='' EmailBody=''	PDFDocFileName='' />
            '<Errors Status=''><Error Code='' Description='' />
            '</Errors>
            '</TA_GETSCANNEDDOCUMENT_OUTPUT>            
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetDOCConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim strFileNo As Integer
            Dim intLCode As UInteger
            Dim strOrderNo As String

            Const strMETHOD_NAME As String = "GetMiscDocumentDetail"
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(strSCANDOCUMENT_OUTPUT)

                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                If SearchDoc.DocumentElement.SelectSingleNode("FileNo").InnerText.Trim <> "" Then
                    strFileNo = SearchDoc.DocumentElement.SelectSingleNode("FileNo").InnerText.Trim
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("LCode").InnerText.Trim <> "" Then
                    intLCode = SearchDoc.DocumentElement.SelectSingleNode("LCode").InnerText.Trim
                End If

                If intLCode = 0 Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_GET_TA_GETMISCSCANNEDDOCUMENTDETAIL"
                    .Connection = objSqlConnection

                    .Parameters.Add("@LCODE", SqlDbType.BigInt)
                    .Parameters("@LCODE").Value = intLCode

                    .Parameters.Add("@FILENO", SqlDbType.Int)
                    If strFileNo = 0 Then
                        .Parameters("@FILENO").Value = DBNull.Value
                    Else
                        .Parameters("@FILENO").Value = strFileNo
                    End If


                End With

                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("Document")

                If objSqlReader.HasRows Then
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.RemoveChild(objAptNode)
                End If

                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ID")) & "")
                    objAptNodeClone.Attributes("FileNo").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FileNo")) & "")
                    objAptNodeClone.Attributes("Order_No").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Order_No")) & "")
                    objAptNodeClone.Attributes("Status").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Status")) & "")
                    objAptNodeClone.Attributes("FileOrder").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FileOrder")) & "")
                    objAptNodeClone.Attributes("DocType").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DocType")) & "")
                    objAptNodeClone.Attributes("Order_Type").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Order_Type")) & "")

                    objAptNodeClone.Attributes("ContentType").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ContentType")) & "")
                    objAptNodeClone.Attributes("EmailSubject").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailSubject")) & "")
                    objAptNodeClone.Attributes("EmailFrom").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailFrom")) & "")
                    objAptNodeClone.Attributes("EmailTo").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailTo")) & "")
                    objAptNodeClone.Attributes("EmailBody").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailBody")) & "")
                    objAptNodeClone.Attributes("PDFDocFileName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PDFDocFileName")) & "")

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)

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

        Public Function GetMiscScannedDocument(ByVal SearchDoc As System.Xml.XmlDocument) As DataSet
            '***********************************************************************
            'Purpose:This function gives details Scanned Document
            'Input  :
            '<TA_GETMISCSCANNEDDOCUMENT_INPUT>
            '<LCode></LCode>
            '<FileNo></FileNo>
            '</TA_GETMISCSCANNEDDOCUMENT_INPUT>

            'Output :
            '<TA_GETMISCSCANNEDDOCUMENT_OUTPUT>
            '<Document ID='' Document ='' FileNo='' Order_No='' Status='' FileOrder='' DocType='' Order_Type='' ContentType='' EmailFrom=''	EmailTo	=''	EmailSubject ='' EmailBody='' PDFDocFileName=''/>
            '<Errors Status=''><Error Code='' Description='' />
            '</Errors>
            '</TA_GETMISCSCANNEDDOCUMENT_OUTPUT>            
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetDOCConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim dsScannedDocument As DataSet
            Dim daScannedDocument As SqlDataAdapter

            Dim strFileNo As Integer
            Dim intLcode As UInteger
            Const strMETHOD_NAME As String = "GetMiscScannedDocument"
            Try

                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                If SearchDoc.DocumentElement.SelectSingleNode("FileNo").InnerText.Trim <> "" Then
                    strFileNo = SearchDoc.DocumentElement.SelectSingleNode("FileNo").InnerText.Trim
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("LCode") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("LCode").InnerText.Trim <> "" Then
                        intLcode = SearchDoc.DocumentElement.SelectSingleNode("LCode").InnerText.Trim
                    End If
                End If


                If intLcode = 0 Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                dsScannedDocument = New DataSet()
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_GET_TA_GETMISCSCANNEDDOCUMENT"
                    .Connection = objSqlConnection

                    .Parameters.Add("@LCODE", SqlDbType.BigInt)
                    .Parameters("@LCODE").Value = intLcode

                    .Parameters.Add("@FILENO", SqlDbType.Int)
                    If strFileNo = 0 Then
                        .Parameters("@FILENO").Value = DBNull.Value
                    Else
                        .Parameters("@FILENO").Value = strFileNo
                    End If

                    .Connection.Open()
                End With
                daScannedDocument = New SqlDataAdapter(objSqlCommand)
                daScannedDocument.Fill(dsScannedDocument)


            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                Return dsScannedDocument
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)

                Return dsScannedDocument
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return dsScannedDocument
        End Function

        Public Function GetControlsImagesforPermission(ByVal SearchDoc As System.Xml.XmlDocument) As DataSet
            '***********************************************************************
            'Purpose:This function gives details Scanned Document
            'Input  :
            '<TA_GETCONTROLIMAGE_INPUT>
            '<SecurityOptionID></SecurityOptionID>
            '</TA_GETCONTROLIMAGE_INPUT>

            'Output :
            '<TA_GETCONTROLIMAGE_OUTPUT>
            '<Document SecurityOptionID='' Image ='' />
            '<Errors Status=''><Error Code='' Description='' />
            '</Errors>
            '</TA_GETCONTROLIMAGE_OUTPUT>            
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim dsScannedDocument As DataSet
            Dim daScannedDocument As SqlDataAdapter

            Dim strFileNo As Integer
            Dim intLcode As UInteger
            Const strMETHOD_NAME As String = "GetControlImage"
            Try

                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                If SearchDoc.DocumentElement.SelectSingleNode("SecurityOptionID").InnerText.Trim <> "" Then
                    strFileNo = SearchDoc.DocumentElement.SelectSingleNode("SecurityOptionID").InnerText.Trim
                End If
                
                dsScannedDocument = New DataSet()
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_GET_TA_PAGES_CONTROL_IMAGE"
                    .Connection = objSqlConnection

                    .Parameters.Add("@SecurityOptionID", SqlDbType.Int)
                    .Parameters("@SecurityOptionID").Value = strFileNo

                    .Connection.Open()
                End With
                daScannedDocument = New SqlDataAdapter(objSqlCommand)
                daScannedDocument.Fill(dsScannedDocument)


            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                Return dsScannedDocument
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)

                Return dsScannedDocument
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return dsScannedDocument
        End Function

        Public Function GetEmailDetails(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************           
            'Purpose:This function gives details Email Subject, To , From ,Body

            'Input  :
            '<UP_GETORDEREMAILDETAILS_INPUT>
            '<ID></ID>
            '</UP_GETORDEREMAILDETAILS_INPUT>

            'Output :
            '<UP_GETORDEREMAILDETAILS_OUTPUT>
            '<MailDetail ID='' EmailSubject='' EmailFrom='' EmailTo='' EmailBody='' />
            '<Errors Status=''>
            '   <Error Code='' Description='' />
            '</Errors>
            '</UP_GETORDEREMAILDETAILS_OUTPUT>
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetDOCConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode As XmlNode, objAptNodeClone As XmlNode
            Dim blnRecordFound As Boolean
            Dim objSqlReader As SqlDataReader
            Dim strID As Integer

            Const strMETHOD_NAME As String = "GetEmailDetails"

            Try
                objOutputXml.LoadXml(strEmialDetails_OUTPUT)

                If SearchDoc.DocumentElement.SelectSingleNode("ID").InnerText.Trim = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                strID = CInt(SearchDoc.DocumentElement.SelectSingleNode("ID").InnerText.Trim & "")

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_GET_TA_ORDEREMAILS"
                    .Connection = objSqlConnection

                    .Parameters.Add("@ID", SqlDbType.Int)
                    .Parameters("@ID").Value = strID

                End With

                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("MailDetail")
                objAptNodeClone = objAptNode.CloneNode(True)

                If objSqlReader.HasRows = True Then
                    objOutputXml.DocumentElement.RemoveChild(objAptNode)
                End If

                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ID")))
                    objAptNodeClone.Attributes("EmailSubject").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailSubject")) & "")
                    objAptNodeClone.Attributes("EmailFrom").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailFrom")) & "")
                    objAptNodeClone.Attributes("EmailTo").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailTo")) & "")
                    objAptNodeClone.Attributes("EmailBody").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailBody")) & "")

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
        Public Function GetScannedImage(ByVal SearchDoc As System.Xml.XmlDocument) As DataSet

            '***********************************************************************           
            'Purpose:This function gives details of Scaned Document Image 
            'Input  :
            '<TA_GETSCANNEDIMAGE_INPUT>
            '   <ID></ID>
            '</TA_GETSCANNEDIMAGE_INPUT>

            'Output :
            '<TA_GETSCANNEDIMAGE_OUTPUT>
            '<ID></ID>
            '<Document></Document>
            '   <Errors Status=''>
            '       <Error Code='' Description='' />
            '   </Errors>
            '</TA_GETSCANNEDIMAGE_OUTPUT>
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetDOCConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim strID As Integer

            Dim dsScannedDocument As DataSet
            Dim daScannedDocument As SqlDataAdapter

            Const strMETHOD_NAME As String = "GetScannedDocumentImage"

            Try

                objOutputXml.LoadXml(strSCANDOCUMENTIMAGE_OUTPUT)

                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strID = SearchDoc.DocumentElement.SelectSingleNode("ID").InnerText.Trim

                If Len(strID) = 0 Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                dsScannedDocument = New DataSet()
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_GET_TA_GETSCANNEDDOCUMENTIMAGE"
                    .Connection = objSqlConnection

                    .Parameters.Add("@ID", SqlDbType.Int)
                    .Parameters("@ID").Value = strID
                    .Connection.Open()

                End With
                daScannedDocument = New SqlDataAdapter(objSqlCommand)
                daScannedDocument.Fill(dsScannedDocument)

            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                Return dsScannedDocument
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                Return dsScannedDocument

            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return dsScannedDocument

        End Function

        Public Function GetFileNoScannedDocument(ByVal SearchDoc As System.Xml.XmlDocument) As DataSet

            '***********************************************************************           
            'Purpose:This function gives details Scanned Document Image
            'Input  :
            '<TA_GETSCANNEDIMAGE_INPUT>
            '   <ID></ID>
            '</TA_GETSCANNEDIMAGE_INPUT>

            'Output :
            '<TA_GETSCANNEDIMAGE_OUTPUT>
            '<ID></ID>
            '<Document></Document>
            '   <Errors Status=''>
            '       <Error Code='' Description='' />
            '   </Errors>
            '</TA_GETSCANNEDIMAGE_OUTPUT>
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetDOCConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim strID As Integer

            Dim dsScannedDocument As DataSet
            Dim daScannedDocument As SqlDataAdapter

            Const strMETHOD_NAME As String = "GetScannedDocumentImage"

            Try

                objOutputXml.LoadXml(strSCANDOCUMENTIMAGE_OUTPUT)

                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strID = SearchDoc.DocumentElement.SelectSingleNode("ID").InnerText.Trim

                If Len(strID) = 0 Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                dsScannedDocument = New DataSet()
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_GET_TA_GETSCANNEDDOCUMENTIMAGE"
                    .Connection = objSqlConnection

                    .Parameters.Add("@ID", SqlDbType.Int)
                    .Parameters("@ID").Value = strID
                    .Connection.Open()

                End With
                daScannedDocument = New SqlDataAdapter(objSqlCommand)
                daScannedDocument.Fill(dsScannedDocument)

            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                Return dsScannedDocument
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                Return dsScannedDocument

            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return dsScannedDocument

        End Function
        Public Function GetFileNumber() As System.Xml.XmlDocument
            '***********************************************************************
            '#############################################################################
            ' Method Written by Ashish Srivastava on date 21-Dec-2007
            '############################################################################
            'Purpose: To list out the those  record having fileno status is zero, 
            'Input  : 

            'Output :  
            '<T_C_FILLING_FILENO>
            '<FileNo />
            '< /T_C_FILLING_FILENO>

            '<T_C_AGENCY_FILENO>
            '<FILENUMBER Status='' FileNo='' />
            '<Errors Status=''><Error Code='' Description='' /></Errors>
            '</T_C_AGENCY_FILENO>"
            Dim objSqlConnection As New SqlConnection(bzShared.GetDOCConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "GetFileNumber"

            objOutputXml.LoadXml(strGetFileNumber_OUTPUT)
            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_GET_TA_LIST_FILENO"
                    .Connection = objSqlConnection
                End With

                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("FILENUMBER")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("FileNo").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FileNo")))
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
        Public Function ListOrderingConfigValue() As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the CRS record, based on the given field value
            'Input  : 
            'Output :  
            '<HD_LISTPTRCONFIG_OUTPUT>
            '	<CONFIGVALUE CCA_NAME='' FIELD_NAME='' FIELD_VALUE='' />
            '    <Errors Status=''>
            '	    <Error Code='' Description='' />
            '	</Errors>
            '</HD_LISTPTRCONFIG_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "ListPTRConfigValue"

            objOutputXml.LoadXml(strLISTORDERINGCONFIGVALUE_OUTPUT)

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_LST_MS_CONFIG]"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("CONFIGVALUE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("CCA_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CCA_NAME")) & "")
                    objAptNodeClone.Attributes("FIELD_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FIELD_NAME")) & "")
                    objAptNodeClone.Attributes("FIELD_VALUE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FIELD_VALUE")) & "")
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

        Public Function GetOrderNumber(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument

            'Purpose: To list out the CRS record, based on the given field value
            'Input  : 
            '<TA_ORIGINAL_INPUT>
            '	<LCODE></LCODE>
            '</TA_ORIGINAL_INPUT>

            'Output :  
            '<TA_ORIGINAL_OUTPUT>
            '	<ORDER ORDER_ID="" ORDER_NUMBER=""/>
            '	<Errors Status="">
            '		<Error Code="" Description=""/>
            '	</Errors>
            '</TA_ORIGINAL_OUTPUT>

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strLCODE As String

            Dim strMETHOD_NAME As String = "ListPTRConfigValue"

            objOutputXml.LoadXml(strORDERNUMBER_OUTPUT)

            '--------Retrieving & Checking Details from Input XMLDocument ------Start
            strLCODE = SearchDoc.DocumentElement.SelectSingleNode("LCODE").InnerText.Trim

            If (strLCODE) = "" Then
                Throw (New AAMSException("Incomplete Parameters Location"))
            End If

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_LST_TA_ORDER]"
                    .Connection = objSqlConnection

                    .Parameters.Add("@LCODE", SqlDbType.BigInt)
                    .Parameters("@LCODE").Value = Val(strLCODE)
                    .Connection.Open()
                End With

                'retrieving the records according to the List Criteria

                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("ORDER")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("ORDER_ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDERID")) & "")
                    objAptNodeClone.Attributes("ORDER_NUMBER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDER_NUMBER")) & "")

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

        Public Function ListCancellOrderConfigValue() As System.Xml.XmlDocument

            '***********************************************************************
            'Purpose: To list out the CRS record, based on the given field value
            'Input  : 
            'Output :  
            '<TA_LISTORDERCONFIG_OUTPUT>
            '	<CONFIGVALUE CCA_NAME='' FIELD_NAME='' FIELD_VALUE='' />
            '    <Errors Status=''>
            '	    <Error Code='' Description='' />
            '	</Errors>
            '</TA_LISTORDERCONFIG_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "ListPTRConfigValue"

            objOutputXml.LoadXml(strLISTCANCELLEDORDER_OUTPUT)

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_LST_HD_CANCEL_ORDER]"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("CONFIGVALUE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("CCA_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CCA_NAME")) & "")
                    objAptNodeClone.Attributes("FIELD_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FIELD_NAME")) & "")
                    objAptNodeClone.Attributes("FIELD_VALUE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FIELD_VALUE")) & "")
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
        Public Function GetScannedDocumentWeb(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function gives details Scanned Document
            'Input  :
            '<TA_GETSCANNEDDOCUMENT_INPUT>
            '<LCode></LCode>
            '<FileNo></FileNo>
            '<Order_No></Order_No>
            '<ORDER_ID></ORDER_ID>
            '</TA_GETSCANNEDDOCUMENT_INPUT>

            'Output :
            '<TA_GETSCANNEDDOCUMENT_OUTPUT>
            '<Document ID='' FileNo='' Order_No='' Status='' FileOrder='' DocType='' Order_Type='' ContentType='' EmailFrom=''	EmailTo	=''	EmailSubject='' EmailBody=''	PDFDocFileName='' />
            '<Errors Status=''><Error Code='' Description='' />
            '</Errors>
            '</TA_GETSCANNEDDOCUMENT_OUTPUT>            
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetDOCConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim strFileNo As Integer
            Dim strOrderNo As String
            Dim intLCode As UInteger
            Dim strORDER_ID As String = ""

            Const strMETHOD_NAME As String = "GetScannedDocument"
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(strSCANDOCUMENT_OUTPUT)

                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                If SearchDoc.DocumentElement.SelectSingleNode("ORDERID") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("ORDERID").InnerText.Trim <> "" Then
                        strORDER_ID = SearchDoc.DocumentElement.SelectSingleNode("ORDERID").InnerText.Trim
                    End If
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("FileNo").InnerText.Trim <> "" Then
                    strFileNo = SearchDoc.DocumentElement.SelectSingleNode("FileNo").InnerText.Trim
                End If

                strOrderNo = SearchDoc.DocumentElement.SelectSingleNode("Order_No").InnerText.Trim
                If SearchDoc.DocumentElement.SelectSingleNode("LCode").InnerText.Trim <> "" Then
                    intLCode = SearchDoc.DocumentElement.SelectSingleNode("LCode").InnerText.Trim
                End If

                If intLCode = 0 Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_GET_TA_GETSCANNEDDOCUMENT_WEB]"
                    .Connection = objSqlConnection

                    .Parameters.Add("@LCODE", SqlDbType.BigInt)
                    .Parameters("@LCODE").Value = intLCode

                    .Parameters.Add("@FILENO", SqlDbType.Int)
                    If strFileNo = 0 Then
                        .Parameters("@FILENO").Value = DBNull.Value
                    Else
                        .Parameters("@FILENO").Value = strFileNo
                    End If

                    .Parameters.Add("@ORDERID", SqlDbType.Int)
                    If strORDER_ID = "" Then
                        .Parameters("@ORDERID").Value = DBNull.Value
                    Else
                        .Parameters("@ORDERID").Value = strORDER_ID
                    End If

                    .Parameters.Add("@ORDERNO", SqlDbType.VarChar, 40)
                    .Parameters("@ORDERNO").Value = strOrderNo

                End With

                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("Document")

                If objSqlReader.HasRows Then
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.RemoveChild(objAptNode)
                End If

                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ID")) & "")
                    objAptNodeClone.Attributes("FileNo").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FileNo")) & "")
                    objAptNodeClone.Attributes("Order_No").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Order_No")) & "")
                    objAptNodeClone.Attributes("Status").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Status")) & "")
                    objAptNodeClone.Attributes("FileOrder").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FileOrder")) & "")
                    objAptNodeClone.Attributes("DocType").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DocType")) & "")
                    objAptNodeClone.Attributes("Order_Type").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Order_Type")) & "")

                    objAptNodeClone.Attributes("ContentType").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ContentType")) & "")
                    objAptNodeClone.Attributes("EmailSubject").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailSubject")) & "")
                    objAptNodeClone.Attributes("EmailFrom").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailFrom")) & "")
                    objAptNodeClone.Attributes("EmailTo").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailTo")) & "")
                    objAptNodeClone.Attributes("EmailBody").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailBody")) & "")
                    objAptNodeClone.Attributes("PDFDocFileName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PDFDocFileName")) & "")

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)

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

        Public Function GetMiscDocumentDetailWeb(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function gives details Scanned Document
            'Input  :
            '<TA_GETSCANNEDDOCUMENT_INPUT>
            '<LCode></LCode>
            '<FileNo></FileNo>
            '</TA_GETSCANNEDDOCUMENT_INPUT>

            'Output :
            '<TA_GETSCANNEDDOCUMENT_OUTPUT>
            '<Document ID='' FileNo='' Order_No='' Status='' FileOrder='' DocType='' Order_Type='' ContentType='' EmailFrom=''	EmailTo	=''	EmailSubject='' EmailBody=''	PDFDocFileName='' />
            '<Errors Status=''><Error Code='' Description='' />
            '</Errors>
            '</TA_GETSCANNEDDOCUMENT_OUTPUT>            
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetDOCConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim strFileNo As Integer
            Dim intLCode As UInteger
            Dim strOrderNo As String

            Const strMETHOD_NAME As String = "GetMiscDocumentDetail"
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(strSCANDOCUMENT_OUTPUT)

                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                If SearchDoc.DocumentElement.SelectSingleNode("FileNo").InnerText.Trim <> "" Then
                    strFileNo = SearchDoc.DocumentElement.SelectSingleNode("FileNo").InnerText.Trim
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("LCode").InnerText.Trim <> "" Then
                    intLCode = SearchDoc.DocumentElement.SelectSingleNode("LCode").InnerText.Trim
                End If

                If intLCode = 0 Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_GET_TA_GETMISCSCANNEDDOCUMENTDETAIL_WEB"
                    .Connection = objSqlConnection

                    .Parameters.Add("@LCODE", SqlDbType.BigInt)
                    .Parameters("@LCODE").Value = intLCode

                    .Parameters.Add("@FILENO", SqlDbType.Int)
                    If strFileNo = 0 Then
                        .Parameters("@FILENO").Value = DBNull.Value
                    Else
                        .Parameters("@FILENO").Value = strFileNo
                    End If


                End With

                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("Document")

                If objSqlReader.HasRows Then
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.RemoveChild(objAptNode)
                End If

                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ID")) & "")
                    objAptNodeClone.Attributes("FileNo").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FileNo")) & "")
                    objAptNodeClone.Attributes("Order_No").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Order_No")) & "")
                    objAptNodeClone.Attributes("Status").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Status")) & "")
                    objAptNodeClone.Attributes("FileOrder").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FileOrder")) & "")
                    objAptNodeClone.Attributes("DocType").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DocType")) & "")
                    objAptNodeClone.Attributes("Order_Type").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Order_Type")) & "")

                    objAptNodeClone.Attributes("ContentType").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ContentType")) & "")
                    objAptNodeClone.Attributes("EmailSubject").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailSubject")) & "")
                    objAptNodeClone.Attributes("EmailFrom").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailFrom")) & "")
                    objAptNodeClone.Attributes("EmailTo").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailTo")) & "")
                    objAptNodeClone.Attributes("EmailBody").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailBody")) & "")
                    objAptNodeClone.Attributes("PDFDocFileName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PDFDocFileName")) & "")

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)

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

