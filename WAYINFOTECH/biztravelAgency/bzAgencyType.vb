'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Tapan $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizTravelAgency/bzAgencyType.vb $
'$Workfile: bzAgencyType.vb $
'$Revision: 12 $
'$Archive: /AAMS/Components/bizTravelAgency/bzAgencyType.vb $
'$Modtime: 28/12/10 4:09p $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizTravelAgency
    Public Class bzAgencyType
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzAgencyType"
        Const gstrList_OUTPUT = "<MS_LISTAGENCYTYPE_OUTPUT><AGENCYTYPE AgencyTypeId='' Agency_Type_Name='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTAGENCYTYPE_OUTPUT>"
        Const srtVIEW_OUTPUT = "<MS_VIEWAGENCYTYPE_OUTPUT><AgencyType Agency_Type_Name='' AgencyTypeId='' AT_MAX_BDR_FAV='' AT_MAX_CC_CALLS=''></AgencyType><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWAGENCYTYPE_OUTPUT>"
        Const strSEARCH_OUTPUT = "<MS_SEARCHAGENCYTYPE_OUTPUT><AgencyType Agency_Type_Name='' AgencyTypeId='' ></AgencyType><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHAGENCYTYPE_OUTPUT>"

        Const StrADD_OUTPUT = "<MS_ADDAGENCYTYPE_OUTPUT><AgencyType ACTION='' Agency_Type_Name='' AgencyTypeId='' AT_MAX_BDR_FAV='' AT_MAX_CC_CALLS=''></AgencyType></MS_ADDAGENCYTYPE_OUTPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETEAGENCYTYPE_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETEAGENCYTYPE_OUTPUT>"
        Const strUPDATE_OUTPUT = "<MS_UPDATEAGENCYTYPE_OUTPUT><AgencyType ACTION='' Agency_Type_Name='' AgencyTypeId='' AT_MAX_BDR_FAV='' AT_MAX_CC_CALLS=''></AgencyType><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEAGENCYTYPE_OUTPUT>"
        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add
            'Declare Output XML String
            '***********************************************************************
            'Purpose: 
            'Input:None
            'Output:Xml Document
            '<MS_ADDPRODUCTGROUP_OUTPUT><ProductGroup ACTION='' ProductGroupName='' ProductGroupId=''/></MS_ADDPRODUCTGROUP_OUTPUT>
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
                strORDER_id = DeleteDoc.DocumentElement.SelectSingleNode("AgencyTypeId").InnerText.Trim
                If strORDER_id = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_TYPE"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"
                    .Parameters.Add(New SqlParameter("@AgencyTypeId", SqlDbType.Int))
                    .Parameters("@AgencyTypeId").Value = strORDER_id
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
            '<MS_SEARCHOFFICEIDQUALIFIERS_OUTPUT><Qualifiers RowID='' Code='' Qualifier='' Description=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHOFFICEIDQUALIFIERS_OUTPUT>

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

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SER_TA_AGENCY_TYPE"
                    .Connection = objSqlConnection
                    .Parameters.Add("@Agency_Type_Name", SqlDbType.VarChar, 20)
                    If SearchDoc.DocumentElement.SelectSingleNode("Agency_Type_Name").InnerText = "" Then
                        .Parameters("@Agency_Type_Name").Value = DBNull.Value
                    Else
                        .Parameters("@Agency_Type_Name").Value = SearchDoc.DocumentElement.SelectSingleNode("Agency_Type_Name").InnerText
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
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AgencyType")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("Agency_Type_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Agency_Type_Name")))
                    objAptNodeClone.Attributes("AgencyTypeId").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AgencyTypeId")))

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
            '<MS_UPDATEOFFICEIDAT_MAX_BDR_FAVS_OUTPUT><AT_MAX_BDR_FAVs ACTION=''  AgencyTypeId=''  Agency_Type_Name='' AT_MAX_BDR_FAV='' AT_MAX_CC_CALLS=''/><Errors Status=''><Error Agency_Type_Name='' AT_MAX_CC_CALLS='' /></Errors></MS_UPDATEOFFICEIDAT_MAX_BDR_FAVS_OUTPUT>
            '************************************************************************
            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objUpdateDocOutput As New XmlDocument
            Dim intRecordsAffected As Int32
            Dim strST_NAME As String
            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)

                With objUpdateDocOutput.DocumentElement.SelectSingleNode("AgencyType")
                    .Attributes("ACTION").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AgencyType").Attributes("ACTION").InnerText
                    .Attributes("AgencyTypeId").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AgencyType").Attributes("AgencyTypeId").InnerText
                    .Attributes("Agency_Type_Name").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AgencyType").Attributes("Agency_Type_Name").InnerText
                    .Attributes("AT_MAX_BDR_FAV").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AgencyType").Attributes("AT_MAX_BDR_FAV").InnerText
                    .Attributes("AT_MAX_CC_CALLS").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AgencyType").Attributes("AT_MAX_CC_CALLS").InnerText
                End With

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("AgencyType")
                    strAction = ((.Attributes("ACTION").InnerText).Trim).ToString
                    strST_NAME = ((.Attributes("Agency_Type_Name").InnerText).Trim).ToString

                    If strAction = "I" Or strAction = "U" Then
                        If strST_NAME = "" Then
                            Throw (New AAMSException("Agency Type Name can't be blank."))
                        End If
                    Else
                        Throw (New AAMSException("Invalid Action Code."))
                    End If
                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_TYPE"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction

                    .Parameters.Add(New SqlParameter("@AgencyTypeId", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("AgencyType").Attributes("AgencyTypeId").InnerText = "" Then
                        .Parameters("@AgencyTypeId").Value = DBNull.Value
                    Else
                        .Parameters("@AgencyTypeId").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("AgencyType").Attributes("AgencyTypeId").InnerText)
                    End If
                    .Parameters.Add(New SqlParameter("@Agency_Type_Name", SqlDbType.VarChar, 20))
                    If UpdateDoc.DocumentElement.SelectSingleNode("AgencyType").Attributes("Agency_Type_Name").InnerText = "" Then
                        .Parameters("@Agency_Type_Name").Value = DBNull.Value
                    Else
                        .Parameters("@Agency_Type_Name").Value = UpdateDoc.DocumentElement.SelectSingleNode("AgencyType").Attributes("Agency_Type_Name").InnerText
                    End If
                    .Parameters.Add(New SqlParameter("@AT_MAX_BDR_FAV", SqlDbType.TinyInt))
                    If UpdateDoc.DocumentElement.SelectSingleNode("AgencyType").Attributes("AT_MAX_BDR_FAV").InnerText = "" Then
                        .Parameters("@AT_MAX_BDR_FAV").Value = DBNull.Value
                    Else
                        .Parameters("@AT_MAX_BDR_FAV").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("AgencyType").Attributes("AT_MAX_BDR_FAV").InnerText)
                    End If
                    .Parameters.Add(New SqlParameter("@AT_MAX_CC_CALLS", SqlDbType.TinyInt))
                    If UpdateDoc.DocumentElement.SelectSingleNode("AgencyType").Attributes("AT_MAX_CC_CALLS").InnerText = "" Then
                        .Parameters("@AT_MAX_CC_CALLS").Value = DBNull.Value
                    Else
                        .Parameters("@AT_MAX_CC_CALLS").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("AgencyType").Attributes("AT_MAX_CC_CALLS").InnerText)
                    End If


                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0

                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED
                    .Connection.Open()
                    intRecordsAffected = .ExecuteNonQuery()
                    If UCase(strAction) = "I" Then
                        intRetId = .Parameters("@RETURNID").Value
                        If intRetId = 0 Then
                            Throw (New AAMSException("Unable to insert!"))
                        ElseIf intRetId = -1 Then
                            Throw (New AAMSException("Agency type already exists. Please enter another agency type"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                            With objUpdateDocOutput.DocumentElement.SelectSingleNode("AgencyType")
                                .Attributes("AgencyTypeId").InnerText = intRetId
                            End With
                        End If
                    ElseIf UCase(strAction) = "U" Then
                        intRetId = .Parameters("@RETURNID").Value
                        If intRetId = -1 Then
                            Throw (New AAMSException("Agency Type Already Exists!"))
                        ElseIf intRetId > 0 Then
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                        Else
                            Throw (New AAMSException("Unable to Upadate!"))
                        End If
                    End If

                End With
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
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
        Public Function List() As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the AgencyType record, based on the given field value
            'Input  : 
            'Output :  
            '<MS_LISTAGENCYTYPE_OUTPUT>
            '	<AGENCYTYPE AgencyTypeId='' Agency_Type_Name='' />
            '	<Errors Status="">
            '		<Error Code="" Description="" />
            '	</Errors>
            '</MS_LISTAGENCYTYPE_OUTPUT>
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
                    .CommandText = "UP_LST_TA_TYPE"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AGENCYTYPE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("AgencyTypeId").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AgencyTypeId")))
                    objAptNodeClone.Attributes("Agency_Type_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Agency_Type_Name")))
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
        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.View
            '***********************************************************************
            'Purpose:This function gives details of City.
            'Input  :
            '<MS_SEARCHORDERSTATUS_INPUT>
            '	<ORDERSTATUSID/>
            '</MS_SEARCHORDERSTATUS_INPUT>
            'Output :
            '<MS_VIEWAGENCYTYPE_OUTPUT>
            '   <AgencyType Agency_Type_Name='' AgencyTypeId='' AT_MAX_BDR_FAV='' AT_MAX_CC_CALLS=''></AgencyType>
            '   <Errors Status=''>
            '       <Error Code='' Description='' />
            '   </Errors>
            '</MS_VIEWAGENCYTYPE_OUTPUT> 
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
                strST_ID = IndexDoc.DocumentElement.SelectSingleNode("AgencyTypeId").InnerText.Trim
                If strST_ID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_TYPE"
                    .Connection = objSqlConnection
                    .Parameters.Add("@AgencyTypeId", SqlDbType.Int)
                    .Parameters("@AgencyTypeId").Value = strST_ID
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("AgencyType")
                        .Attributes("Agency_Type_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Agency_Type_Name")) & "")
                        .Attributes("AgencyTypeId").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AgencyTypeId")) & "")
                        .Attributes("AT_MAX_BDR_FAV").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AT_MAX_BDR_FAV")) & "")
                        .Attributes("AT_MAX_CC_CALLS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AT_MAX_CC_CALLS")) & "")
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



        Public Function ListGroupAgencyType() As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the AgencyType record, based on the given field value
            'Input  : 
            'Output :  
            '<MS_LISTAGENCYTYPE_OUTPUT>
            '	<AGENCYGROUPTYPE TYPEID='' TYPE='' />
            '	<Errors Status="">
            '		<Error Code="" Description="" />
            '	</Errors>
            '</MS_LISTAGENCYTYPE_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "ListGroupAgencyType"
            Const strGroupAgencyType = "<MS_LISTAGENCYGROUPTYPE_OUTPUT><AGENCYGROUPTYPE TYPEID='' TYPE='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTAGENCYGROUPTYPE_OUTPUT>"

            objOutputXml.LoadXml(strGroupAgencyType)

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_LST_TA_GROUP_AGNECY_TYPE]"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AGENCYGROUPTYPE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("TYPEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("TYPEID")) & "")
                    objAptNodeClone.Attributes("TYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("TYPE")) & "")
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
