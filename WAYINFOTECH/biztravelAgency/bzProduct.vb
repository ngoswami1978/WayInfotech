'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Tapan $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizTravelAgency/bzProduct.vb $
'$Workfile: bzProduct.vb $
'$Revision: 15 $
'$Archive: /AAMS/Components/bizTravelAgency/bzProduct.vb $
'$Modtime: 18/07/08 10:46a $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizTravelAgency

    Public Class bzProduct
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzOrderType"
        Const srtLIST_OUTPUT = "<MS_LISTPRODUCTS_OUTPUT><PRODUCT PRODUCTID='' PRODUCTNAME='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTPRODUCTS_OUTPUT>"
        Const srtVIEW_OUTPUT = "<MS_VIEWPRODUCTS_OUTPUT><PRODUCT PRODUCTID='' PRODUCTNAME='' VERSION='' EDITION='' HDD_REQUIREMENT='' RAM_REQUIREMENT='' CPU_REQUIREMENT='' OSID_REQUIREMENT='' SEGMENT_REQUIRED='' PER_INSTALLATION='' LIST_PRICE='' PRODUCT_DESCRIPTION='' PROVIDER_CRS='' ProductGroupID=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWPRODUCTS_OUTPUT>"
        Const strSEARCH_OUTPUT = "<MS_SEARCHPRODUCTS_OUTPUT><PRODUCT productGroupName='' PRODUCTNAME='' PRODUCTID='' VERSION=''/><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHPRODUCTS_OUTPUT>"

        Const StrADD_OUTPUT = "<MS_ADDPRODUCTS_OUTPUT><PRODUCT ACTION=''  PRODUCTID='' PRODUCTNAME='' VERSION='' EDITION='' HDD_REQUIREMENT='' RAM_REQUIREMENT='' CPU_REQUIREMENT='' OSID_REQUIREMENT='' SEGMENT_REQUIRED='' PER_INSTALLATION='' LIST_PRICE='' PRODUCT_DESCRIPTION='' PROVIDER_CRS='' ProductGroupID=''/></MS_ADDPRODUCTS_OUTPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETEPRODUCTS_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETEPRODUCTS_OUTPUT>"
        Const strUPDATE_OUTPUT = "<MS_UPDATEPRODUCTS_OUTPUT><PRODUCT ACTION=''  PRODUCTID='' PRODUCTNAME='' VERSION='' EDITION='' HDD_REQUIREMENT='' RAM_REQUIREMENT='' CPU_REQUIREMENT='' OSID_REQUIREMENT='' SEGMENT_REQUIRED='' PER_INSTALLATION='' LIST_PRICE='' PRODUCT_DESCRIPTION='' PROVIDER_CRS='' ProductGroupID=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEPRODUCTS_OUTPUT>"
        Public Function Add() As System.Xml.XmlDocument Implements AAMS.bizInterface.BizLayerI.Add
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

        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements AAMS.bizInterface.BizLayerI.Delete
            '***********************************************************************
            'Purpose:This function deletes a city.
            'Input:XmlDocument
            '        
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
            Dim strPRODUCT_id As String

            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0
                strPRODUCT_id = DeleteDoc.DocumentElement.SelectSingleNode("PRODUCTID").InnerText.Trim
                If strPRODUCT_id = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_PRODUCTS"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"
                    .Parameters.Add(New SqlParameter("@PRODUCTID", SqlDbType.Int))
                    .Parameters("@PRODUCTID").Value = strPRODUCT_id
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

        Public Function Search(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements AAMS.bizInterface.BizLayerI.Search
            '***********************************************************************
            'Purpose:This function gives search results based on choosen search criterion.
            'Input  :- 
            '<MS_SEARCHORDERSTATUS_INPUT>
            '	<ORDER_STATUS_NAME/>
            '</MS_SEARCHORDERSTATUS_INPUT>

            'Output :
            '<MS_SEARCHPRODUCTS_OUTPUT><PRODUCT productGroupName='' PRODUCTNAME='' PRODUCTID='' VERSION=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHPRODUCTS_OUTPUT>

            '        '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim intGroupID As Integer
            Dim strProductName As String
            Dim intCRS As String
            Dim intOS As Integer
            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strProductName = (SearchDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("PRODUCTNAME").InnerText.Trim())

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
                    .CommandText = "UP_SER_TA_PRODUCTS"
                    .Connection = objSqlConnection
                    .Parameters.Add("@ProductGroupID", SqlDbType.Int)
                    If SearchDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("productGroupId").InnerText.Trim() = "" Then
                        .Parameters("@ProductGroupID").Value = DBNull.Value
                    Else
                        .Parameters("@ProductGroupID").Value = CInt(SearchDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("productGroupId").InnerText.Trim())
                    End If

                    .Parameters.Add("@PRODUCTNAME", SqlDbType.VarChar, 50)
                    If strProductName <> "" Then
                        .Parameters("@PRODUCTNAME").Value = strProductName
                    Else
                        .Parameters("@PRODUCTNAME").Value = DBNull.Value
                    End If
                    .Parameters.Add("@OSID", SqlDbType.Int)
                    If SearchDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("OSId").InnerText.Trim() = "" Then
                        .Parameters("@OSID").Value = DBNull.Value
                    Else
                        .Parameters("@OSID").Value = CInt(SearchDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("OSId").InnerText.Trim())
                    End If
                    .Parameters.Add("@CRSCode", SqlDbType.Int)
                    If SearchDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("CRSCode").InnerText.Trim = "" Then
                        .Parameters("@CRSCode").Value = DBNull.Value
                    Else
                        .Parameters("@CRSCode").Value = CInt(SearchDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("CRSCode").InnerText.Trim)
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
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("PRODUCT")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("productGroupName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ProductGroupName")))
                    objAptNodeClone.Attributes("PRODUCTNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRODUCTNAME")))
                    objAptNodeClone.Attributes("PRODUCTID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRODUCTID")))
                    objAptNodeClone.Attributes("VERSION").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("VERSION")))

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
        Public Function Update(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements AAMS.bizInterface.BizLayerI.Update
            '***********************************************************************
            'Purpose:This function Inserts/Updates City.
            'Input  :
            '<MS_SEARCHORDERSTATUS_OUTPUT>
            '	<ORDERSTATUS ACTION='' ORDER_STATUS_NAME="" ORDERSTATUSID="">
            '</MS_SEARCHORDERSTATUS_OUTPUT>
            'Output :
            '<MS_UPDATEPRODUCTS_OUTPUT><PRODUCT ACTION=''  PRODUCTID='' PRODUCTNAME='' VERSION='' EDITION='' HDD_REQUIREMENT='' RAM_REQUIREMENT='' CPU_REQUIREMENT='' OSID_REQUIREMENT='' SEGMENT_REQUIRED='' PER_INSTALLATION='' LIST_PRICE='' PRODUCT_DESCRIPTION='' PROVIDER_CRS='' ProductGroupID=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEPRODUCTS_OUTPUT>
            '************************************************************************
            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objUpdateDocOutput As New XmlDocument
            Dim strST_NAME As String
            Dim intRecordsAffected As Int32
            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)
                With objUpdateDocOutput.DocumentElement.SelectSingleNode("PRODUCT")
                    .Attributes("ACTION").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("ACTION").InnerText
                    .Attributes("PRODUCTID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("PRODUCTID").InnerText
                    .Attributes("PRODUCTNAME").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("PRODUCTNAME").InnerText
                    .Attributes("VERSION").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("VERSION").InnerText
                    .Attributes("EDITION").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("EDITION").InnerText
                    .Attributes("HDD_REQUIREMENT").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("HDD_REQUIREMENT").InnerText
                    .Attributes("RAM_REQUIREMENT").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("RAM_REQUIREMENT").InnerText
                    .Attributes("CPU_REQUIREMENT").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("CPU_REQUIREMENT").InnerText
                    .Attributes("OSID_REQUIREMENT").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("OSID_REQUIREMENT").InnerText
                    .Attributes("SEGMENT_REQUIRED").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("SEGMENT_REQUIRED").InnerText
                    .Attributes("PER_INSTALLATION").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("PER_INSTALLATION").InnerText
                    .Attributes("LIST_PRICE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("LIST_PRICE").InnerText
                    .Attributes("PRODUCT_DESCRIPTION").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("PRODUCT_DESCRIPTION").InnerText
                    .Attributes("PROVIDER_CRS").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("PROVIDER_CRS").InnerText
                    .Attributes("ProductGroupID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("ProductGroupID").InnerText
                End With


                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT")
                    strAction = ((.Attributes("ACTION").InnerText).Trim).ToString
                    strST_NAME = ((.Attributes("PRODUCTNAME").InnerText).Trim).ToString

                    If strAction = "I" Or strAction = "U" Then
                        If strST_NAME = "" Then
                            Throw (New AAMSException("Order Type can't be blank."))
                        End If
                    Else
                        Throw (New AAMSException("Invalid Action Code."))
                    End If
                End With

                'ADDING PARAMETERS IN STORED PROCEDURE



                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_PRODUCTS"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction
                    .Parameters.Add(New SqlParameter("@PRODUCTID", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("PRODUCTID").InnerText = "" Then
                        .Parameters("@PRODUCTID").Value = DBNull.Value
                    Else
                        .Parameters("@PRODUCTID").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("PRODUCTID").InnerText)
                    End If
                    .Parameters.Add(New SqlParameter("@PRODUCT_DESCRIPTION", SqlDbType.VarChar, 100))
                    If UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("PRODUCT_DESCRIPTION").InnerText = "" Then
                        .Parameters("@PRODUCT_DESCRIPTION").Value = DBNull.Value
                    Else
                        .Parameters("@PRODUCT_DESCRIPTION").Value = UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("PRODUCT_DESCRIPTION").InnerText
                    End If
                    .Parameters.Add(New SqlParameter("@LIST_PRICE", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("LIST_PRICE").InnerText = "" Then
                        .Parameters("@LIST_PRICE").Value = DBNull.Value
                    Else
                        .Parameters("@LIST_PRICE").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("LIST_PRICE").InnerText)
                    End If
                    .Parameters.Add(New SqlParameter("@PER_INSTALLATION", SqlDbType.Bit))
                    If UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("PER_INSTALLATION").InnerText = "" Then
                        .Parameters("@PER_INSTALLATION").Value = DBNull.Value
                    Else
                        .Parameters("@PER_INSTALLATION").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("PER_INSTALLATION").InnerText)
                    End If
                    .Parameters.Add(New SqlParameter("@SEGMENT_REQUIRED", SqlDbType.SmallInt))
                    If UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("SEGMENT_REQUIRED").InnerText = "" Then
                        .Parameters("@SEGMENT_REQUIRED").Value = DBNull.Value
                    Else
                        .Parameters("@SEGMENT_REQUIRED").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("SEGMENT_REQUIRED").InnerText)
                    End If
                    .Parameters.Add(New SqlParameter("@OSID_REQUIREMENT", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("OSID_REQUIREMENT").InnerText = "" Then
                        .Parameters("@OSID_REQUIREMENT").Value = DBNull.Value
                    Else
                        .Parameters("@OSID_REQUIREMENT").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("OSID_REQUIREMENT").InnerText)
                    End If
                    .Parameters.Add(New SqlParameter("@CPU_REQUIREMENT", SqlDbType.SmallInt))
                    If UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("CPU_REQUIREMENT").InnerText = "" Then
                        .Parameters("@CPU_REQUIREMENT").Value = DBNull.Value
                    Else
                        .Parameters("@CPU_REQUIREMENT").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("CPU_REQUIREMENT").InnerText)
                    End If
                    .Parameters.Add(New SqlParameter("@RAM_REQUIREMENT", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("RAM_REQUIREMENT").InnerText = "" Then
                        .Parameters("@RAM_REQUIREMENT").Value = DBNull.Value
                    Else
                        .Parameters("@RAM_REQUIREMENT").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("RAM_REQUIREMENT").InnerText)
                    End If
                    .Parameters.Add(New SqlParameter("@HDD_REQUIREMENT", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("HDD_REQUIREMENT").InnerText = "" Then
                        .Parameters("@HDD_REQUIREMENT").Value = DBNull.Value
                    Else
                        .Parameters("@HDD_REQUIREMENT").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("HDD_REQUIREMENT").InnerText)
                    End If
                    .Parameters.Add(New SqlParameter("@EDITION", SqlDbType.VarChar, 30))
                    If UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("EDITION").InnerText = "" Then
                        .Parameters("@EDITION").Value = DBNull.Value
                    Else
                        .Parameters("@EDITION").Value = UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("EDITION").InnerText
                    End If
                    .Parameters.Add(New SqlParameter("@VERSION", SqlDbType.VarChar, 10))
                    If UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("VERSION").InnerText = "" Then
                        .Parameters("@VERSION").Value = DBNull.Value
                    Else
                        .Parameters("@VERSION").Value = UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("VERSION").InnerText
                    End If
                    .Parameters.Add(New SqlParameter("@PRODUCTNAME", SqlDbType.VarChar, 50))
                    If UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("PRODUCTNAME").InnerText = "" Then
                        .Parameters("@PRODUCTNAME").Value = DBNull.Value
                    Else
                        .Parameters("@PRODUCTNAME").Value = UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("PRODUCTNAME").InnerText
                    End If
                    .Parameters.Add(New SqlParameter("@PROVIDER_CRS", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("PROVIDER_CRS").InnerText = "" Then
                        .Parameters("@PROVIDER_CRS").Value = DBNull.Value
                    Else
                        .Parameters("@PROVIDER_CRS").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("PROVIDER_CRS").InnerText)
                    End If
                    .Parameters.Add(New SqlParameter("@ProductGroupID", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("ProductGroupID").InnerText = "" Then
                        .Parameters("@ProductGroupID").Value = DBNull.Value
                    Else
                        .Parameters("@ProductGroupID").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("PRODUCT").Attributes("ProductGroupID").InnerText)
                    End If


                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0

                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED
                    .Connection.Open()
                    intRecordsAffected = .ExecuteNonQuery()
                    If UCase(strAction) = "I" Then
                        intRetId = .Parameters("@RETURNID").Value
                        If intRetId = -1 Then
                            Throw (New AAMSException("Product Already Exists!"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                            With objUpdateDocOutput.DocumentElement.SelectSingleNode("PRODUCT")
                                .Attributes("PRODUCTID").InnerText = intRetId
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

        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements AAMS.bizInterface.BizLayerI.View
            '***********************************************************************
            'Purpose:This function gives details of City.
            'Input  :
            '<MS_SEARCHORDERSTATUS_INPUT>
            '	<ORDERSTATUSID/>
            '</MS_SEARCHORDERSTATUS_INPUT>

            'Output :
            '<MS_VIEWPRODUCTS_OUTPUT><PRODUCT PRODUCTID='' PRODUCTNAME='' VERSION='' EDITION='' HDD_REQUIREMENT='' RAM_REQUIREMENT='' CPU_REQUIREMENT='' OSID_REQUIREMENT='' SEGMENT_REQUIRED='' PER_INSTALLATION='' LIST_PRICE='' PRODUCT_DESCRIPTION='' PROVIDER_CRS='' ProductGroupID=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWPRODUCTS_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strProductId As String
            Const strMETHOD_NAME As String = "View"
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(srtVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strProductId = IndexDoc.DocumentElement.SelectSingleNode("PRODUCTID").InnerText.Trim
                If strProductId = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_PRODUCTS"
                    .Connection = objSqlConnection
                    .Parameters.Add("@PRODUCTID", SqlDbType.Int)
                    .Parameters("@PRODUCTID").Value = strProductId
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("PRODUCT")
                        .Attributes("PRODUCTID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRODUCTID")))
                        .Attributes("PRODUCTNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRODUCTNAME")) & "")
                        .Attributes("VERSION").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("VERSION")) & "")
                        .Attributes("EDITION").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EDITION")) & "")
                        .Attributes("HDD_REQUIREMENT").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("HDD_REQUIREMENT")) & "")
                        .Attributes("RAM_REQUIREMENT").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("RAM_REQUIREMENT")) & "")
                        .Attributes("CPU_REQUIREMENT").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CPU_REQUIREMENT")) & "")
                        .Attributes("OSID_REQUIREMENT").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OSID_REQUIREMENT")) & "")
                        .Attributes("SEGMENT_REQUIRED").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SEGMENT_REQUIRED")) & "")
                        .Attributes("PER_INSTALLATION").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PER_INSTALLATION")) & "")
                        .Attributes("LIST_PRICE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LIST_PRICE")) & "")
                        .Attributes("PRODUCT_DESCRIPTION").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRODUCT_DESCRIPTION")) & "")
                        .Attributes("PROVIDER_CRS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PROVIDER_CRS")) & "")
                        .Attributes("ProductGroupID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ProductGroupID")) & "")
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
            '<MS_LISTPRODUCTS_OUTPUT>
            '    <PRODUCT PRODUCTID='' PRODUCTNAME='' />
            '    <Errors Status=''>
            '        <Error Code='' Description='' />
            '    </Errors>
            '</MS_LISTPRODUCTS_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "List"

            objOutputXml.LoadXml(srtLIST_OUTPUT)

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LST_TA_PRODUCTS"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("PRODUCT")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("PRODUCTID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRODUCTID")))
                    objAptNodeClone.Attributes("PRODUCTNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRODUCTNAME")))
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
