'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Ashishsrivastava $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizTravelAgency/bzAgencyCompetition.vb $
'$Workfile: bzAgencyCompetition.vb $
'$Revision: 9 $
'$Archive: /AAMS/Components/bizTravelAgency/bzAgencyCompetition.vb $
'$Modtime: 20/09/11 11:34a $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizTravelAgency

    Public Class bzAgencyCompetition
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzAgencyCompetition"
        Const strADDAGENCYCOMPETITIONUSE_OUTPUT = "<UP_UPDATECOMPETITIONDETAILS_OUTPUT><COMPETITION_DETAILS CRSID='' DATE_END='' DATE_START='' SOLE_USER='' DIAL_BACKUP='' ONLINESTATUSID='' PRINTER_COUNT='' PC_COUNT='' Action='' /><Errors Status=''><Error Code='' Description='' /></Errors></UP_UPDATECOMPETITIONDETAILS_OUTPUT>"
        Const strDELETE_INPUT = "<MS_DELETEAGENCYCOMPETITIONDETAILS_INPUT><CRSID></CRSID><LOCATION_CODE></LOCATION_CODE></MS_DELETEAGENCYCOMPETITIONDETAILS_INPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETEAGENCYCOMPETITIONDETAILS_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETEAGENCYCOMPETITIONDETAILS_OUTPUT>"

        Const strUPDATE_INPUT = "<UP_UPDATECOMPETITIONDETAILS_INPUT><COMPETITION_DETAILS ACTION ='' LOCATION_CODE ='' CRSID='' DATE_END='' DATE_START='' SOLE_USER='' DIAL_BACKUP='' ONLINESTATUSID='' PRINTER_COUNT='' PC_COUNT='' CommercialDetails='' /></UP_UPDATECOMPETITIONDETAILS_INPUT>"
        Const strUPDATE_OUTPUT = "<UP_UPDATECOMPETITIONDETAILS_OUTPUT><COMPETITION_DETAILS LOCATION_CODE ='' CRSID='' DATE_END='' DATE_START='' SOLE_USER='' DIAL_BACKUP='' ONLINESTATUSID='' PRINTER_COUNT='' PC_COUNT=''  CommercialDetails='' /><Errors Status=''><Error Code='' Description='' /></Errors></UP_UPDATECOMPETITIONDETAILS_OUTPUT>"

        Const strVIEW_INPUT = "<MS_VIEWAGENCYCOMPETITIONDETAIL_INPUT><CRSID></CRSID><LOCATION_CODE></LOCATION_CODE></MS_VIEWAGENCYCOMPETITIONDETAIL_INPUT>"
        Const strVIEW_OUTPUT = "<MS_VIEWAGENCYCOMPETITIONDETAIL_OUTPUT><CRS LOCATION_CODE ='' CRSID='' DATE_END='' DATE_START='' SOLE_USER='' DIAL_BACKUP='' ONLINESTATUSID='' PRINTER_COUNT='' PC_COUNT='' CommercialDetails=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWAGENCYCOMPETITIONDETAIL_OUTPUT>"

        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add
            'Declare Output XML String
            '***********************************************************************
            'Purpose: 
            'Input:None
            'Output:Xml Document
            '<UP_UPDATECOMPETITIONDETAILS_OUTPUT>
            '<COMPETITION_DETAILS CRSID='' DATE_END='' DATE_START='' SOLE_USER='' DIAL_BACKUP='' ONLINESTATUSID='' PRINTER_COUNT='' PC_COUNT='' Action='' />
            '   <Errors Status=''>
            '       <Error Code='' Description='' />
            '   </Errors>
            '</UP_UPDATECOMPETITIONDETAILS_OUTPUT>
            '**************************************************************************
            Dim strMETHOD_NAME As String = "Add"
            Dim objXMLDoc As New XmlDocument
            Try
                objXMLDoc.LoadXml(strADDAGENCYCOMPETITIONUSE_OUTPUT)
            Catch Exec As Exception
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objXMLDoc, "101", Exec.Message)
                Return objXMLDoc
            End Try
            Return objXMLDoc

        End Function

        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Delete
            '***********************************************************************
            'Purpose:This function deletes a Agency Competition.
            'Input:XmlDocument
            '<MS_DELETEAGENCYCOMPETITIONDETAILS_INPUT>
            '<CRSID></CRSID=''>
            '<LOCATION_CODE><LOCATION_CODE/>
            '</MS_DELETEAGENCYCOMPETITIONDETAILS_INPUT>

            'Output :
            '<MS_DELETEAGENCYCOMPETITIONDETAILS_OUTPUT>
            '   <Errors Status=''>
            '       <Error Code='' Description='' />
            '   </Errors>
            '</MS_DELETEAGENCYCOMPETITIONDETAILS_OUTPUT>
            '************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Delete"
            Dim strAGENCYCRSUSEID_id As String
            Dim strAGENCYLCODEID_id As String


            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0
                strAGENCYCRSUSEID_id = DeleteDoc.DocumentElement.SelectSingleNode("CRSID").InnerText.Trim
                strAGENCYLCODEID_id = DeleteDoc.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText.Trim
                If strAGENCYCRSUSEID_id = "" Or strAGENCYLCODEID_id = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_COMPETITIONDETAILS"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"
                    .Parameters.Add(New SqlParameter("@CRSID", SqlDbType.Int))
                    .Parameters("@CRSID").Value = strAGENCYCRSUSEID_id

                    .Parameters.Add(New SqlParameter("@LOCATION_CODE", SqlDbType.Int))
                    .Parameters("@LOCATION_CODE").Value = strAGENCYLCODEID_id

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
            '---------Not to be implemented----------
        End Function

        Public Function Update(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Update
            '**********************************OLD XML INPUT*************************
            'Purpose:This function Inserts/Updates Agency Competition Details.
            'Input  :
            '<UP_UPDATECOMPETITIONDETAILS_INPUT>
            '<COMPETITION_DETAILS ACTION ='' LOCATION_CODE = '' CRSID='' DATE_END = '' DATE_START='' SOLE_USER='' DIAL_BACKUP='' ONLINESTATUSID='' PRINTER_COUNT='' PC_COUNT='' />
            '</UP_UPDATECOMPETITIONDETAILS_INPUT>

            '################################# NEW XML INPUT ###########################
            '<UP_UPDATECOMPETITIONDETAILS_INPUT>
            '<COMPETITION_DETAILS ACTION="U" LOCATION_CODE="229" CRSID="3" DATE_END="20071219" DATE_START="20071217" SOLE_USER="True" DIAL_BACKUP="True" ONLINESTATUSID="X25" PRINTER_COUNT="2" PC_COUNT="2" /> 
            '<COMPETITION_DETAILS ACTION="D" LOCATION_CODE="229" CRSID="2" DATE_END="20071219" DATE_START="20071217" SOLE_USER="True" DIAL_BACKUP="True" ONLINESTATUSID="X25" PRINTER_COUNT="2" PC_COUNT="2" /> 
            '<COMPETITION_DETAILS ACTION="I" LOCATION_CODE="229" CRSID="3" DATE_END="" DATE_START="" SOLE_USER="False" DIAL_BACKUP="False" ONLINESTATUSID="" PRINTER_COUNT="" PC_COUNT="" /> 
            '<COMPETITION_DETAILS ACTION="I" LOCATION_CODE="229" CRSID="3" DATE_END="" DATE_START="" SOLE_USER="False" DIAL_BACKUP="False" ONLINESTATUSID="" PRINTER_COUNT="" PC_COUNT="" /> 
            '</UP_UPDATECOMPETITIONDETAILS_INPUT>

            'Output :
            '<UP_UPDATECOMPETITIONDETAILS_OUTPUT>
            '<COMPETITION_DETAILS LOCATION_CODE ='' CRSID='' DATE_END='' DATE_START='' SOLE_USER='' DIAL_BACKUP='' ONLINESTATUSID='' PRINTER_COUNT='' PC_COUNT='' />
            '<Errors Status=''>
            '   <Error Code='' Description='' />
            '</Errors>
            '</UP_UPDATECOMPETITIONDETAILS_OUTPUT>            
            '************************************************************************

            '####################  CODE WRITTEN BY ASHISH SRIVASTAVA ON DATE 14-12-2007 ##############
            Dim strAction As String
            ' Dim intRetId As Integer
            Dim objSqlCommand As New SqlCommand
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objUpdateDocOutput As New XmlDocument
            'Dim intRecordsAffected As Int32
            Dim objNode As XmlNode
            Dim objTran As SqlTransaction  'added by ashish
            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)
                objSqlConnection.Open()
                objTran = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)

                'make parameter
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .Transaction = objTran
                    .CommandText = "UP_SRO_TA_COMPETITIONDETAILS"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters.Add(New SqlParameter("@CRSID", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@LOCATION_CODE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@DATE_END", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@DATE_START", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@SOLE_USER", SqlDbType.Char, 1))
                    .Parameters.Add(New SqlParameter("@DIAL_BACKUP", SqlDbType.Char, 1))
                    .Parameters.Add(New SqlParameter("@ONLINESTATUSID", SqlDbType.Char, 6))
                    .Parameters.Add(New SqlParameter("@PRINTER_COUNT", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@PC_COUNT", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@CommercialDetails", SqlDbType.VarChar, 1000))
                    .Parameters.Add(New SqlParameter("@RETUNID", SqlDbType.Int))
                    .Parameters("@RETUNID").Direction = ParameterDirection.Output
                    .Parameters("@RETUNID").Value = 0
             
                    For Each objNode In UpdateDoc.DocumentElement.SelectNodes("COMPETITION_DETAILS")
                        strAction = objNode.Attributes("ACTION").InnerText
                        .Parameters("@Action").Value = strAction

                        'If strAction = "I" Then
                        '    .Parameters("@RN").Value = vbNullString
                        'ElseIf strAction = "U" Or strAction = "D" Then
                        '    .Parameters("@RN").Value = objNode.Attributes("RN").InnerText
                        'End If

                        '" <COMPETITION_DETAILS Action='U' LOCATION_CODE='229' CRSID='3' DATE_END='20071219' DATE_START='20071217' SOLE_USER='True' DIAL_BACKUP='True' ONLINESTATUSID='X25' PRINTER_COUNT='2' PC_COUNT='2' /> " & _

                        If strAction = "I" Or strAction = "U" Or strAction = "D" Then
                            If objNode.Attributes("LOCATION_CODE").InnerText = "" Then
                                Throw (New AAMSException("Location Code can't be blank."))
                            ElseIf objNode.Attributes("CRSID").InnerText = "" Then
                                Throw (New AAMSException("CRSID ID can't be blank."))
                            ElseIf objNode.Attributes("SOLE_USER").InnerText = "" Then
                                Throw (New AAMSException("Sole User can't be blank."))
                            ElseIf objNode.Attributes("DIAL_BACKUP").InnerText = "" Then
                                Throw (New AAMSException("Dail Backup can't be blank."))
                            ElseIf objNode.Attributes("PRINTER_COUNT").InnerText = "" Then
                                Throw (New AAMSException("Printer Count can't be blank."))
                            ElseIf objNode.Attributes("PC_COUNT").InnerText = "" Then
                                Throw (New AAMSException("PC can't be blank."))
                            End If
                        Else
                            Throw (New AAMSException("Invalid Action Code."))
                        End If
                        '<COMPETITION_DETAILS ACTION="U" LOCATION_CODE="229" CRSID="3" DATE_END="20071219" DATE_START="20071217" SOLE_USER="True" DIAL_BACKUP="True" ONLINESTATUSID="X25" PRINTER_COUNT="2" PC_COUNT="2" /> 

                        .Parameters("@LOCATION_CODE").Value = objNode.Attributes("LOCATION_CODE").InnerText
                        .Parameters("@CRSID").Value = objNode.Attributes("CRSID").InnerText

                        If objNode.Attributes("DATE_END").InnerText <> "" Then
                            .Parameters("@DATE_END").Value = objNode.Attributes("DATE_END").InnerText
                        Else
                            .Parameters("@DATE_END").Value = DBNull.Value
                        End If

                        If objNode.Attributes("DATE_START").InnerText <> "" Then
                            .Parameters("@DATE_START").Value = objNode.Attributes("DATE_START").InnerText
                        Else
                            .Parameters("@DATE_START").Value = DBNull.Value
                        End If

                        .Parameters("@SOLE_USER").Value = objNode.Attributes("SOLE_USER").InnerText
                        .Parameters("@DIAL_BACKUP").Value = objNode.Attributes("DIAL_BACKUP").InnerText
                        .Parameters("@ONLINESTATUSID").Value = objNode.Attributes("ONLINESTATUSID").InnerText
                        .Parameters("@PRINTER_COUNT").Value = objNode.Attributes("PRINTER_COUNT").InnerText
                        .Parameters("@PC_COUNT").Value = objNode.Attributes("PC_COUNT").InnerText
                        .Parameters("@CommercialDetails").Value = objNode.Attributes("CommercialDetails").InnerText

                        .ExecuteNonQuery()
                    Next
                End With
                objTran.Commit()
                objSqlConnection.Close()
                objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"

            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                If objSqlConnection.State <> ConnectionState.Closed Then
                    If Not objTran Is Nothing Then
                        objTran.Rollback()
                    End If
                    objSqlConnection.Close()
                End If
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
                bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objUpdateDocOutput




            '####################  END OF THE CODE ##################################################



            '' ''Dim intRetId As Integer
            '' ''Dim intCRSId As Integer
            '' ''Dim intLcodeId As Integer

            '' ''Dim strAction As String = ""
            '' ''Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            '' ''Dim objSqlCommand As New SqlCommand
            '' ''Dim objUpdateDocOutput As New XmlDocument
            '' ''Dim intRecordsAffected As Int32
            '' ''Dim objNode As XmlNode
            '' ''Const strMETHOD_NAME As String = "Update"
            '' ''Try
            '' ''    objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)

            '' ''    objNode = objUpdateDocOutput.DocumentElement.SelectSingleNode("COMPETITION_DETAILS")
            '' ''    objUpdateDocOutput.DocumentElement.RemoveChild(objNode)
            '' ''    objUpdateDocOutput.DocumentElement.AppendChild(objUpdateDocOutput.ImportNode(UpdateDoc.DocumentElement.SelectSingleNode("COMPETITION_DETAILS"), True))

            '' ''    'Retrieving & Checking Details from Input XMLDocument
            '' ''    With UpdateDoc.DocumentElement.SelectSingleNode("COMPETITION_DETAILS")

            '' ''        If (.Attributes("LOCATION_CODE").InnerText).Trim = "" Then
            '' ''            Throw (New AAMSException("Location Code can't be blank."))
            '' ''        End If
            '' ''        If (.Attributes("CRSID").InnerText).Trim = "" Then
            '' ''            Throw (New AAMSException("CRS Code can't be blank."))
            '' ''        End If
            '' ''    End With

            '' ''    'ADDING PARAMETERS IN STORED PROCEDURE
            '' ''    With objSqlCommand
            '' ''        .Connection = objSqlConnection
            '' ''        .CommandType = CommandType.StoredProcedure
            '' ''        .CommandText = "UP_SRO_TA_COMPETITIONDETAILS"

            '' ''        .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
            '' ''        .Parameters.Add(New SqlParameter("@CRSID", SqlDbType.Int))
            '' ''        .Parameters.Add(New SqlParameter("@LOCATION_CODE", SqlDbType.Int))
            '' ''        .Parameters.Add(New SqlParameter("@DATE_END", SqlDbType.Int))
            '' ''        .Parameters.Add(New SqlParameter("@DATE_START", SqlDbType.Int))
            '' ''        .Parameters.Add(New SqlParameter("@SOLE_USER", SqlDbType.Bit))
            '' ''        .Parameters.Add(New SqlParameter("@DIAL_BACKUP", SqlDbType.Bit))
            '' ''        .Parameters.Add(New SqlParameter("@ONLINESTATUSID", SqlDbType.Char, 6))
            '' ''        .Parameters.Add(New SqlParameter("@PRINTER_COUNT", SqlDbType.Int))
            '' ''        .Parameters.Add(New SqlParameter("@PC_COUNT", SqlDbType.Int))

            '' ''        .Parameters.Add(New SqlParameter("@RETUNID", SqlDbType.Int))
            '' ''        .Parameters("@RETUNID").Direction = ParameterDirection.Output
            '' ''        .Parameters("@RETUNID").Value = 0
            '' ''    End With

            '' ''    With UpdateDoc.DocumentElement.SelectSingleNode("COMPETITION_DETAILS")
            '' ''        strAction = .Attributes("ACTION").InnerText
            '' ''        objSqlCommand.Parameters("@ACTION").Value = strAction

            '' ''        If (.Attributes("CRSID").InnerText).Trim <> "" Then
            '' ''            objSqlCommand.Parameters("@CRSID").Value = CInt(.Attributes("CRSID").InnerText)
            '' ''        End If
            '' ''        If (.Attributes("LOCATION_CODE").InnerText).Trim <> "" Then
            '' ''            objSqlCommand.Parameters("@LOCATION_CODE").Value = CInt(.Attributes("LOCATION_CODE").InnerText)
            '' ''        End If
            '' ''        If (.Attributes("DATE_START").InnerText).Trim <> "" Then
            '' ''            objSqlCommand.Parameters("@DATE_START").Value = CInt(.Attributes("DATE_START").InnerText)
            '' ''        End If
            '' ''        If (.Attributes("DATE_END").InnerText).Trim <> "" Then
            '' ''            objSqlCommand.Parameters("@DATE_END").Value = CInt(.Attributes("DATE_END").InnerText)
            '' ''        End If
            '' ''        If (.Attributes("SOLE_USER").InnerText).Trim <> "" Then
            '' ''            objSqlCommand.Parameters("@SOLE_USER").Value = CInt(.Attributes("SOLE_USER").InnerText)
            '' ''        End If
            '' ''        If (.Attributes("DIAL_BACKUP").InnerText).Trim <> "" Then
            '' ''            objSqlCommand.Parameters("@DIAL_BACKUP").Value = CInt(.Attributes("DIAL_BACKUP").InnerText)
            '' ''        End If
            '' ''        If (.Attributes("ONLINESTATUSID").InnerText).Trim <> "" Then
            '' ''            objSqlCommand.Parameters("@ONLINESTATUSID").Value = .Attributes("ONLINESTATUSID").InnerText
            '' ''        End If
            '' ''        If (.Attributes("PRINTER_COUNT").InnerText).Trim <> "" Then
            '' ''            objSqlCommand.Parameters("@PRINTER_COUNT").Value = CInt(.Attributes("PRINTER_COUNT").InnerText)
            '' ''        End If
            '' ''        If (.Attributes("PC_COUNT").InnerText).Trim <> "" Then
            '' ''            objSqlCommand.Parameters("@PC_COUNT").Value = CInt(.Attributes("PC_COUNT").InnerText)
            '' ''        End If

            '' ''    End With

            '' ''    objSqlCommand.Connection.Open()
            '' ''    intRecordsAffected = objSqlCommand.ExecuteNonQuery()
            '' ''    intRetId = objSqlCommand.Parameters("@RETUNID").Value

            '' ''    If UCase(strAction) = "I" Then
            '' ''        intRetId = objSqlCommand.Parameters("@RETUNID").Value

            '' ''        If intRetId = -1 Then
            '' ''            Throw (New AAMSException("Agency Competition Detail Already Exists!"))
            '' ''        Else
            '' ''            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            '' ''            With objUpdateDocOutput.DocumentElement.SelectSingleNode("COMPETITION_DETAILS")
            '' ''                .Attributes("CRSID").InnerText = intCRSId
            '' ''                .Attributes("LOCATION_CODE").InnerText = intLcodeId
            '' ''            End With
            '' ''        End If
            '' ''    ElseIf UCase(strAction) = "U" Then
            '' ''        intRetId = objSqlCommand.Parameters("@RETUNID").Value
            '' ''        If intRetId <= 0 Then
            '' ''            Throw (New AAMSException("Unable to update!"))
            '' ''        Else
            '' ''            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            '' ''        End If
            '' ''    End If

            '' ''Catch Exec As AAMSException
            '' ''    'CATCHING AAMS EXCEPTIONS
            '' ''    bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
            '' ''    bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
            '' ''    Return objUpdateDocOutput
            '' ''Catch Exec As Exception
            '' ''    bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
            '' ''    bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
            '' ''    Return objUpdateDocOutput
            '' ''Finally
            '' ''    If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
            '' ''    objSqlCommand.Dispose()
            '' ''End Try
            '' ''Return objUpdateDocOutput

        End Function

        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.View
            '***********************************************************************
            'Purpose:This function gives details of Agency Competition Details.
            'Input  :
            '<MS_VIEWAGENCYCOMPETITIONDETAIL_INPUT>
            '<CRSID></CRSID>
            '<LOCATION_CODE></LOCATION_CODE>
            '</MS_VIEWAGENCYCOMPETITIONDETAIL_INPUT>

            'Output :
            '<MS_VIEWAGENCYCOMPETITIONDETAIL_OUTPUT>
            '<CRS LOCATION_CODE ='' CRSID='' DATE_END='' DATE_START='' SOLE_USER='' DIAL_BACKUP='' ONLINESTATUSID='' PRINTER_COUNT='' PC_COUNT='' />
            '   <Errors Status=''>
            '       <Error Code='' Description='' />
            '   </Errors>
            '</MS_VIEWAGENCYCOMPETITIONDETAIL_OUTPUT>            
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strAgencyCRS_ID As String
            Dim strAgencyLCODE_ID As String
            Const strMETHOD_NAME As String = "View"
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(strVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument 
                strAgencyCRS_ID = IndexDoc.DocumentElement.SelectSingleNode("CRSID").InnerText.Trim
                strAgencyLCODE_ID = IndexDoc.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText.Trim

                If strAgencyCRS_ID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_COMPETITIONDETAILS"
                    .Connection = objSqlConnection

                    .Parameters.Add("@CRSID", SqlDbType.Int)
                    .Parameters("@CRSID").Value = strAgencyCRS_ID

                    .Parameters.Add("@LOCATION_CODE", SqlDbType.Int)
                    .Parameters("@LOCATION_CODE").Value = strAgencyLCODE_ID

                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"
                End With

                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("CRS")
                        .Attributes("CRSID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CRSID")) & "")
                        .Attributes("LOCATION_CODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOCATION_CODE")) & "")
                        .Attributes("DATE_END").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE_END")) & "")
                        .Attributes("DATE_START").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE_START")) & "")
                        .Attributes("SOLE_USER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SOLE_USER")) & "")
                        .Attributes("DIAL_BACKUP").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DIAL_BACKUP")) & "")
                        .Attributes("ONLINESTATUSID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ONLINESTATUSID")) & "")
                        .Attributes("PRINTER_COUNT").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRINTER_COUNT")) & "")
                        .Attributes("PC_COUNT").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PC_COUNT")) & "")
                        .Attributes("CommercialDetails").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CommercialDetails")) & "")
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

        Public Function GetDetails(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '---------Not to be implemented----------
        End Function
    End Class

End Namespace
