'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Mukesh $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizTravelAgency/bzMiscInstallation.vb $
'$Workfile: bzMiscInstallation.vb $
'$Revision: 12 $
'$Archive: /AAMS/Components/bizTravelAgency/bzMiscInstallation.vb $
'$Modtime: 4/03/09 3:20p $

Imports System.Xml
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports AAMS.bizShared

Namespace bizTravelAgency
    Public Class bzMiscInstallation
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzMiscInstallation"
        Const StrADD_OUTPUT = "<TA_ADDMISCINSTALLATION_OUTPUT><MISCINSTALLATION ACTION='' ROWID='' LCODE='' DATE='' DATEDE='' EQUIPMENTTYPE='' EQUIPMENTNUMBER='' QTY='' ORDERNUMBER='' ORDERNUMBERDE='' CHALLANNO='' LOGGEDBY='' /><Errors Status=''><Error Code='' Description='' /></Errors></TA_ADDMISCINSTALLATION_OUTPUT>"
        Const strUPDATE_OUTPUT = "<TA_UPDATEMISCINSTALLATION_OUTPUT><MISCINSTALLATION ACTION='' ROWID='' LCODE='' DATE='' DATEDE='' EQUIPMENTTYPE='' EQUIPMENTNUMBER='' QTY='' ORDERNUMBER='' ORDERNUMBERDE='' CHALLANNO='' LOGGEDBY='' USE_BACKDATED_CHALLAN='' /><Errors Status=''><Error Code='' Description='' /></Errors></TA_UPDATEMISCINSTALLATION_OUTPUT>"
        Const strDELETE_OUTPUT = "<TA_DELETEMISCINSTALLATION_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></TA_DELETEMISCINSTALLATION_OUTPUT>"
        Const strVIEW_OUTPUT = "<TA_VIEWMISCINSTALLATION_OUTPUT><MISCINSTALLATION ROWID='' DATE='' LCODE='' EQUIPMENTTYPE='' EQUIPMENTNUMBER='' QTY='' ORDERNUMBER='' CHALLANNO='' /><Errors Status=''><Error Code='' Description='' /></Errors></TA_VIEWMISCINSTALLATION_OUTPUT>"
        Const srtGETMISCINSTALLATION_OUTPUT = "<MS_GETMISCINSTALLATION_OUTPUT><GETMISCINSTALLATION ROWID='' LCODE='' DATE='' EQUIPMENT_TYPE='' EQUIPMENT_NO='' QTY='' OrderNUmber='' CHALLANDATE='' ChallanNumber='' LoggedBy='' Employee_Name='' LoggedDateTime='' CHALLANSTATUS='' /> <TOTAL MISCPC=''/><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_GETMISCINSTALLATION_OUTPUT>"
        Const strMISCHISTORY_OUTPUT = "<TA_HISTORYMISCINSTALLATION_OUTPUT><MISCHOSTORY DATE='' MODIREPLDATE='' EQUIPMENT_TYPE='' EQUIPMENT_NO='' QTY='' CHALLANNO='' ACTION='' LOGGEDBY='' LOGGEDDATETIME='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></TA_HISTORYMISCINSTALLATION_OUTPUT>"
        Const srtRtp_MISCDEINSTALLATION_OUTPUT = "<MS_GETMISCINSTALLATION_OUTPUT><GETMISCDEINSTALLATION LCODE ='' INSTALLATIONDATE ='' DEINSTALLATIONDATE ='' EQUIPMENT_TYPE='' EQUIPMENT_NO='' QTY='' CHALLANNUMBER ='' LOGGEDBY='' LOGGEDDATETIME='' ROWID=''/><TOTAL MISCPC=''/><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description=''/></Errors></MS_GETMISCINSTALLATION_OUTPUT>"
        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add
            'Declare Output XML String
            '***********************************************************************
            'Purpose: 
            'Input:None
            'Output:Xml Document
            '<TA_ADDMISCINSTALLATION_OUTPUT><MISCINSTALLATION ACTION='' ROWID='' LCODE='' DATE='' DATEDE='' EQUIPMENTTYPE='' EQUIPMENTNUMBER='' QTY='' ORDERNUMBER='' ORDERNUMBERDE='' CHALLANNO='' LOGGEDBY='' /><Errors Status=''><Error Code='' Description='' /></Errors></TA_ADDMISCINSTALLATION_OUTPUT>
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
            '<TA_DELETEMISCINSTALLATION_INPUT>
            '	<ROWID />
            '</TA_DELETEMISCINSTALLATION_INPUT>

            'Output :
            '<TA_DELETEMISCINSTALLATION_OUTPUT>
            '	<Errors Status=''><Error Code='' Description='' />
            '	</Errors>
            '</TA_DELETEMISCINSTALLATION_OUTPUT>

            '************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Delete"
            Dim strROWID As String
            Dim intRetId As Integer
            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0
                strROWID = DeleteDoc.DocumentElement.SelectSingleNode("ROWID").InnerText.Trim
                If strROWID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_MISCINSTALLATION"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"
                    .Parameters.Add(New SqlParameter("@ROWID", SqlDbType.BigInt))
                    .Parameters("@ROWID").Value = Val(strROWID)

                    .Parameters.Add(New SqlParameter("@RETUNID", SqlDbType.Int))
                    .Parameters("@RETUNID").Direction = ParameterDirection.Output
                    .Parameters("@RETUNID").Value = 0

                    objSqlCommand.Connection.Open()
                    intRecordsAffected = .ExecuteNonQuery()
                    objSqlCommand.Connection.Close()
                End With
                intRetId = objSqlCommand.Parameters("@RETUNID").Value
                'Checking whether record is deleted successfully or not
                If intRetId > 0 Then
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
        End Function

        Public Function Update(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Update
            '***********************************************************************
            'Purpose:This function Inserts/Updates City.
            'Input  :
            '<TA_UPDATEMISCINSTALLATION_INPUT>
            '	<MISCINSTALLATION ACTION='' ROWID='' LCODE='' DATE='' DATEDE='' EQUIPMENTTYPE='' 
            '		EQUIPMENTNUMBER='' QTY='' ORDERNUMBER='' ORDERNUMBERDE='' CHALLANNO='' LOGGEDBY='' USE_BACKDATED_CHALLAN='' />
            '</TA_UPDATEMISCINSTALLATIONE_INPUT>

            'Output :
            '<TA_UPDATEMISCINSTALLATION_OUTPUT>
            '	<MISCINSTALLATION ACTION='' ROWID='' LCODE='' DATE='' DATEDE='' EQUIPMENTTYPE='' 
            '		EQUIPMENTNUMBER='' QTY='' ORDERNUMBER='' ORDERNUMBERDE='' CHALLANNO='' LOGGEDBY='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</TA_UPDATEMISCINSTALLATION_OUTPUT>

            '************************************************************************
            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objUpdateDocOutput As New XmlDocument
            Dim strMsg As String = ""

            Dim intRecordsAffected As Int32
            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)

                With objUpdateDocOutput.DocumentElement.SelectSingleNode("MISCINSTALLATION")
                    .Attributes("ACTION").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("ACTION").InnerText
                    .Attributes("ROWID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("ROWID").InnerText
                    .Attributes("LCODE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("LCODE").InnerText
                    .Attributes("DATE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("DATE").InnerText
                    .Attributes("DATEDE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("DATEDE").InnerText
                    .Attributes("EQUIPMENTTYPE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("EQUIPMENTTYPE").InnerText
                    .Attributes("EQUIPMENTNUMBER").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("EQUIPMENTNUMBER").InnerText
                    .Attributes("QTY").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("QTY").InnerText
                    .Attributes("ORDERNUMBER").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("ORDERNUMBER").InnerText
                    .Attributes("ORDERNUMBERDE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("ORDERNUMBERDE").InnerText
                    .Attributes("CHALLANNO").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("CHALLANNO").InnerText
                    .Attributes("LOGGEDBY").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("LOGGEDBY").InnerText
                    .Attributes("USE_BACKDATED_CHALLAN").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("USE_BACKDATED_CHALLAN").InnerText

                End With

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION")
                    strAction = ((.Attributes("ACTION").InnerText).Trim).ToString
                    If strAction = "I" Or strAction = "U" Or strAction = "R" Or strAction = "X" Then
                        If .Attributes("LCODE").InnerText.Trim = "" Then
                            Throw (New AAMSException("Agency can't be blank."))
                        ElseIf .Attributes("DATE").InnerText.Trim = "" Then
                            Throw (New AAMSException("Date Can't be blank."))
                        ElseIf .Attributes("EQUIPMENTTYPE").InnerText.Trim = "" Then
                            Throw (New AAMSException("Equipment Type Can't be blank."))
                        ElseIf .Attributes("EQUIPMENTNUMBER").InnerText.Trim = "" Then
                            Throw (New AAMSException("Equipment No. can't be blank."))
                        ElseIf Val(.Attributes("QTY").InnerText.Trim & "") = 0 Then
                            Throw (New AAMSException("QTY can't be zero."))
                        End If
                    Else
                        Throw (New AAMSException("Invalid Action Code."))
                    End If
                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_MISCINSTALLATION"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction

                    .Parameters.Add(New SqlParameter("@ROWID", SqlDbType.BigInt))
                    If UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("ROWID").InnerText.Trim = "" Then
                        .Parameters("@ROWID").Value = DBNull.Value
                    Else
                        .Parameters("@ROWID").Value = Val(UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("ROWID").InnerText & "")
                    End If
                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    .Parameters("@LCODE").Value = Val(UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("LCODE").InnerText & "")

                    .Parameters.Add(New SqlParameter("@DATE", SqlDbType.Int))
                    .Parameters("@DATE").Value = Val(UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("DATE").InnerText & "")

                    .Parameters.Add(New SqlParameter("@DATEDE", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("DATEDE").InnerText = "" Then
                        .Parameters("@DATEDE").Value = DBNull.Value
                    Else
                        .Parameters("@DATEDE").Value = Val(UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("DATEDE").InnerText & "")
                    End If

                    .Parameters.Add(New SqlParameter("@EQUIPMENTTYPE", SqlDbType.Char, 3))
                    .Parameters("@EQUIPMENTTYPE").Value = UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("EQUIPMENTTYPE").InnerText.Trim


                    .Parameters.Add(New SqlParameter("@EQUIPMENTNUMBER", SqlDbType.VarChar, 25))
                    .Parameters("@EQUIPMENTNUMBER").Value = UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("EQUIPMENTNUMBER").InnerText.Trim


                    .Parameters.Add(New SqlParameter("@QTY", SqlDbType.TinyInt))
                    .Parameters("@QTY").Value = Val(UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("QTY").InnerText.Trim & "")


                    .Parameters.Add(New SqlParameter("@ORDERNUMBER", SqlDbType.VarChar, 12))
                    If UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("ORDERNUMBER").InnerText.Trim = "" Then
                        .Parameters("@ORDERNUMBER").Value = DBNull.Value
                    Else
                        .Parameters("@ORDERNUMBER").Value = UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("ORDERNUMBER").InnerText.Trim
                    End If

                    .Parameters.Add(New SqlParameter("@ORDERNUMBERDE", SqlDbType.VarChar, 12))
                    If UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("ORDERNUMBERDE").InnerText.Trim = "" Then
                        .Parameters("@ORDERNUMBERDE").Value = DBNull.Value
                    Else
                        .Parameters("@ORDERNUMBERDE").Value = UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("ORDERNUMBERDE").InnerText.Trim
                    End If

                    .Parameters.Add(New SqlParameter("@CHALLANNO", SqlDbType.VarChar, 17))
                    If UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("CHALLANNO").InnerText.Trim = "" Then
                        .Parameters("@CHALLANNO").Value = DBNull.Value
                    Else
                        .Parameters("@CHALLANNO").Value = UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("CHALLANNO").InnerText.Trim
                    End If

                    .Parameters.Add(New SqlParameter("@LOGGEDBY", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("LOGGEDBY").InnerText.Trim = "" Then
                        .Parameters("@LOGGEDBY").Value = DBNull.Value
                    Else
                        .Parameters("@LOGGEDBY").Value = Val(UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("LOGGEDBY").InnerText & "")
                    End If

                    .Parameters.Add(New SqlParameter("@USE_BACKDATED_CHALLAN", SqlDbType.Bit))
                    If UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("USE_BACKDATED_CHALLAN").InnerText.Trim = "" Then
                        .Parameters("@USE_BACKDATED_CHALLAN").Value = 0
                    Else
                        .Parameters("@USE_BACKDATED_CHALLAN").Value = Val(UpdateDoc.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("USE_BACKDATED_CHALLAN").InnerText & "")
                    End If

                    .Parameters.Add(New SqlParameter("@RETUNID", SqlDbType.Int))
                    .Parameters("@RETUNID").Direction = ParameterDirection.Output
                    .Parameters("@RETUNID").Value = ""

                    .Parameters.Add(New SqlParameter("@ERRMSG", SqlDbType.VarChar, 200))
                    .Parameters("@ERRMSG").Direction = ParameterDirection.Output
                    .Parameters("@ERRMSG").Value = ""

                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED
                    .Connection.Open()

                    intRecordsAffected = .ExecuteNonQuery()

                    If UCase(strAction) = "I" Then
                        intRetId = .Parameters("@RETUNID").Value
                        strMsg = .Parameters("@ERRMSG").Value & ""
                        If intRetId <= 0 Then
                            Throw (New AAMSException(strMsg))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("ROWID").InnerText = intRetId
                        End If
                    ElseIf UCase(strAction) = "U" Or strAction = "R" Or strAction = "X" Then
                        intRetId = .Parameters("@RETUNID").Value
                        strMsg = .Parameters("@ERRMSG").Value & ""
                        If intRetId <= 0 Then
                            Throw (New AAMSException(strMsg))
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
            '<TA_VIEWMISCINSTALLATION_INPUT>
            '	<ROWID />
            '</TA_VIEWMISCINSTALLATION_INPUT>

            'Output :
            '<TA_VIEWMISCINSTALLATION_OUTPUT>
            '	<MISCINSTALLATION ACTION='' ROWID='' DATE='' LCODE='' EQUIPMENTTYPE='' 
            '		EQUIPMENTNUMBER='' QTY='' ORDERNUMBER='' CHALLANNO='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</TA_VIEWMISCINSTALLATION_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strROWID As String
            Const strMETHOD_NAME As String = "View"
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(strVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strROWID = IndexDoc.DocumentElement.SelectSingleNode("ROWID").InnerText.Trim
                If strROWID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_MISCINSTALLATION"
                    .Connection = objSqlConnection
                    .Parameters.Add("@ROWID", SqlDbType.BigInt)
                    .Parameters("@ROWID").Value = Val(strROWID)
                    .Parameters.Add("@ACTION", SqlDbType.Char, 1)
                    .Parameters("@ACTION").Value = "S"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("MISCINSTALLATION")
                        .Attributes("ROWID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ROWID")) & "")
                        If IsDBNull(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE"))) = True Then
                            .Attributes("DATE").InnerText = ""
                        Else
                            .Attributes("DATE").InnerText = Format(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE")), "yyyyMMdd")
                        End If
                        .Attributes("LCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")) & "")
                        .Attributes("EQUIPMENTTYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EQUIPMENTTYPE")) & "")
                        .Attributes("EQUIPMENTNUMBER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EQUIPMENTNUMBER")) & "")
                        .Attributes("QTY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("QTY")) & "")
                        .Attributes("ORDERNUMBER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDERNUMBER")) & "")
                        .Attributes("CHALLANNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHALLANNO")) & "")
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
        Public Function GetMiscInstallationHistory(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function gives details of City.
            'Input  :
            '<TA_HISTORYMISCINSTALLATION_INPUT>
            '	<ROWID />
            '</TA_HISTORYMISCINSTALLATION_INPUT>

            'Output :
            '<TA_HISTORYMISCINSTALLATION_OUTPUT>
            '	<MISCHOSTORY DATE='' MODIREPLDATE='' EQUIPMENT_TYPE='' EQUIPMENT_NO='' QTY='' CHALLANNO=''
            '		ACTION='' LOGGEDBY='' LOGGEDDATETIME='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</TA_HISTORYMISCINSTALLATION_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strROWID As String
            Dim objAptNode, objAptNodeClone As XmlNode
            Const strMETHOD_NAME As String = "GetMiscInstallationHistory"
            Dim blnRecordFound As Boolean

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Try
                objOutputXml.LoadXml(strMISCHISTORY_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strROWID = IndexDoc.DocumentElement.SelectSingleNode("ROWID").InnerText.Trim
                If strROWID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Paging Section    
                If IndexDoc.DocumentElement.SelectSingleNode("PAGE_NO") IsNot Nothing Then
                    If IndexDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim() <> "" Then
                        intPageNo = IndexDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim()
                    End If
                End If
                If IndexDoc.DocumentElement.SelectSingleNode("PAGE_SIZE") IsNot Nothing Then
                    If IndexDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim() <> "" Then
                        intPageSize = IndexDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim()
                    End If
                End If
                If IndexDoc.DocumentElement.SelectSingleNode("SORT_BY") IsNot Nothing Then
                    If IndexDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim() <> "" Then
                        strSortBy = IndexDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim()
                    End If
                End If

                If IndexDoc.DocumentElement.SelectSingleNode("DESC") IsNot Nothing Then
                    If IndexDoc.DocumentElement.SelectSingleNode("DESC").InnerText.Trim().ToUpper = "TRUE" Then
                        blnDesc = True
                    Else
                        blnDesc = False
                    End If
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_GET_TA_MISCINSTALLATION_HISTORY"
                    .Connection = objSqlConnection
                    .Parameters.Add("@ROWID", SqlDbType.BigInt)
                    .Parameters("@ROWID").Value = Val(strROWID)
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
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("MISCHOSTORY")
                objAptNodeClone = objAptNode.CloneNode(True)
                If objSqlReader.HasRows = True Then objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    If IsDBNull(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE"))) = True Then
                        objAptNodeClone.Attributes("DATE").InnerText = ""
                    Else
                        objAptNodeClone.Attributes("DATE").InnerText = Format(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE")), "dd-MMM-yyyy")
                    End If
                    If IsDBNull(objSqlReader.GetValue(objSqlReader.GetOrdinal("MODIREPLDATE"))) = True Then
                        objAptNodeClone.Attributes("MODIREPLDATE").InnerText = ""
                    Else
                        objAptNodeClone.Attributes("MODIREPLDATE").InnerText = Format(objSqlReader.GetValue(objSqlReader.GetOrdinal("MODIREPLDATE")), "dd-MMM-yyyy")
                    End If
                    objAptNodeClone.Attributes("EQUIPMENT_TYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EQUIPMENT_TYPE")) & "")
                    objAptNodeClone.Attributes("EQUIPMENT_NO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EQUIPMENT_NO")) & "")
                    objAptNodeClone.Attributes("QTY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("QTY")) & "")
                    objAptNodeClone.Attributes("CHALLANNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHALLANNO")) & "")
                    objAptNodeClone.Attributes("ACTION").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ACTION")) & "")
                    objAptNodeClone.Attributes("LOGGEDBY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOGGEDBY")) & "")
                    If IsDBNull(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOGGEDDATETIME"))) = True Then
                    Else
                        objAptNodeClone.Attributes("LOGGEDDATETIME").InnerText = Format(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOGGEDDATETIME")), "dd-MMM-yyyy hh:mm:ss tt")
                    End If
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
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objOutputXml, "103", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function
        Public Function GetInstalledMiscHW(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the Misc Installation record, based on the given field value
            'Input  : 
            '<MS_GETMISCINSTALLATION_INPUT>
            '	<LCode></LCode>
            '</MS_GETMISCINSTALLATION_INPUT>
            'Output :  
            '<MS_GETMISCINSTALLATION_OUTPUT>
            '	<GETPCINSTALLATION ROWID='' LCODE='' DATE='' EQUIPMENT_TYPE='' EQUIPMENT_NO='' QTY='' OrderNUmber='' CHALLANDATE='' ChallanNumber='' LoggedBy='' Employee_Name='' LoggedDateTime='' CHALLANSTATUS='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_GETMISCINSTALLATION_OUTPUT>

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strLCODE As String
            Dim blnRecordFound As Boolean
            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean
            Dim strMETHOD_NAME As String = "GetInstalledMiscHW"
            objOutputXml.LoadXml(srtGETMISCINSTALLATION_OUTPUT)
            Try
                If SearchDoc.DocumentElement.SelectSingleNode("LCode").InnerText <> "" Then
                    strLCODE = SearchDoc.DocumentElement.SelectSingleNode("LCode").InnerText
                Else
                    strLCODE = ""
                End If
                If strLCODE = "" Then
                    Throw (New AAMSException("Agency Code can't be blank."))
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
                    .CommandText = "UP_GET_TA_MISCINSTALLATION"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    .Parameters("@LCODE").Value = strLCODE

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

                    .Parameters.Add(New SqlParameter("@MISCPC", SqlDbType.Int))
                    .Parameters("@MISCPC").Direction = ParameterDirection.Output
                    .Parameters("@MISCPC").Value = 0


                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("GETMISCINSTALLATION")
                objAptNodeClone = objAptNode.CloneNode(True)
                If objSqlReader.HasRows = True Then objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("ROWID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ROWID")))
                    objAptNodeClone.Attributes("LCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")))
                    If IsDBNull(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE"))) = True Then
                    Else
                        objAptNodeClone.Attributes("DATE").InnerText = Format(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE")), "dd-MMM-yyyy")
                    End If
                    objAptNodeClone.Attributes("EQUIPMENT_TYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EQUIPMENT_TYPE")) & "")
                    objAptNodeClone.Attributes("EQUIPMENT_NO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EQUIPMENT_NO")) & "")
                    objAptNodeClone.Attributes("QTY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("QTY")) & "")
                    objAptNodeClone.Attributes("OrderNUmber").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OrderNUmber")) & "")
                    'If IsDBNull(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE"))) = True Then
                    objAptNodeClone.Attributes("CHALLANDATE").InnerText = ""
                    'Else
                    'objAptNodeClone.Attributes("CHALLANDATE").InnerText = Format(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE")), "dd-MMM-yyyy")
                    'End If
                    objAptNodeClone.Attributes("ChallanNumber").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ChallanNumber")) & "")
                    objAptNodeClone.Attributes("LoggedBy").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LoggedBy")) & "")
                    objAptNodeClone.Attributes("Employee_Name").InnerText = ""
                    If IsDBNull(objSqlReader.GetValue(objSqlReader.GetOrdinal("LoggedDateTime"))) = True Then
                        objAptNodeClone.Attributes("LoggedDateTime").InnerText = ""
                    Else
                        objAptNodeClone.Attributes("LoggedDateTime").InnerText = Format(objSqlReader.GetValue(objSqlReader.GetOrdinal("LoggedDateTime")), "dd-MMM-yyyy hh:mm:ss tt")
                    End If
                    objAptNodeClone.Attributes("CHALLANSTATUS").InnerText = ""
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop

                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found")
                Else
                    objSqlReader.Close()
                    objOutputXml.DocumentElement.SelectSingleNode("TOTAL").Attributes("MISCPC").InnerText = objSqlCommand.Parameters("@MISCPC").Value
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
        Public Function RptDeInstalledMiscHW(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the Misc Installation record, based on the given field value

            'INPUT  : 
            '<MS_GETMISCDEINSTALLATION_INPUT>
            '<LCode></LCode><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
            '</MS_GETMISCDEINSTALLATION_INPUT>

            'OUTPUT :  
            '<MS_GETMISCINSTALLATION_OUTPUT>
            '   <GETPCINSTALLATION INSTALLATIONDATE="" DEINSTALLATIONDATE="" EQUIPMENT_TYPE="" EQUIPMENT_NO="" QTY="" CHALLANNUMBER="" LOGGEDBY="" LOGGEDDATETIME="" ROWID=""/>
            '<Errors Status="">
            '		<Error Code="" Description=""/>
            '</Errors>
            '</MS_GETMISCINSTALLATION_OUTPUT>
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strLCODE As String
            Dim blnRecordFound As Boolean
            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean
            Dim strMETHOD_NAME As String = "GetDeInstalledMiscHW"
            objOutputXml.LoadXml(srtRtp_MISCDEINSTALLATION_OUTPUT)
            Try
                If SearchDoc.DocumentElement.SelectSingleNode("LCode").InnerText <> "" Then
                    strLCODE = SearchDoc.DocumentElement.SelectSingleNode("LCode").InnerText
                Else
                    strLCODE = ""
                End If
                If strLCODE = "" Then
                    Throw (New AAMSException("Agency Code can't be blank."))
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
                    .CommandText = "[UP_RPT_TA_MISCDEINSTALLATION]"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    .Parameters("@LCODE").Value = strLCODE

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

                    .Parameters.Add(New SqlParameter("@MISCPC", SqlDbType.Int))
                    .Parameters("@MISCPC").Direction = ParameterDirection.Output
                    .Parameters("@MISCPC").Value = 0


                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("GETMISCDEINSTALLATION")
                objAptNodeClone = objAptNode.CloneNode(True)
                If objSqlReader.HasRows = True Then objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("ROWID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ROWID")))
                    objAptNodeClone.Attributes("LCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")))
                    If IsDBNull(objSqlReader.GetValue(objSqlReader.GetOrdinal("INSDATE"))) = True Then
                    Else
                        objAptNodeClone.Attributes("INSTALLATIONDATE").InnerText = Format(objSqlReader.GetValue(objSqlReader.GetOrdinal("INSDATE")), "dd-MMM-yyyy")
                    End If

                    If IsDBNull(objSqlReader.GetValue(objSqlReader.GetOrdinal("DEINSDATE"))) = True Then
                    Else
                        objAptNodeClone.Attributes("DEINSTALLATIONDATE").InnerText = Format(objSqlReader.GetValue(objSqlReader.GetOrdinal("DEINSDATE")), "dd-MMM-yyyy")
                    End If
                    objAptNodeClone.Attributes("EQUIPMENT_TYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EQUIPMENT_TYPE")) & "")
                    objAptNodeClone.Attributes("EQUIPMENT_NO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EQUIPMENT_NO")) & "")
                    objAptNodeClone.Attributes("QTY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("QTY")) & "")
                    'objAptNodeClone.Attributes("InsOrderNUmber").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("InsOrderNUmber")) & "")

                    'objAptNodeClone.Attributes("DeInsOrderNUmber").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DeInsOrderNUmber")) & "")
                    'objAptNodeClone.Attributes("CHALLANDATE").InnerText = ""
                    objAptNodeClone.Attributes("CHALLANNUMBER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ChallanNumber")) & "")
                    objAptNodeClone.Attributes("LOGGEDBY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LoggedBy")) & "")
                    If IsDBNull(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOGGEDDATETIME"))) = True Then
                        objAptNodeClone.Attributes("LOGGEDDATETIME").InnerText = ""
                    Else
                        objAptNodeClone.Attributes("LOGGEDDATETIME").InnerText = Format(objSqlReader.GetValue(objSqlReader.GetOrdinal("LoggedDateTime")), "dd-MMM-yyyy hh:mm:ss tt")
                    End If

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop

                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found")
                Else
                    objSqlReader.Close()
                    objOutputXml.DocumentElement.SelectSingleNode("TOTAL").Attributes("MISCPC").InnerText = objSqlCommand.Parameters("@MISCPC").Value
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