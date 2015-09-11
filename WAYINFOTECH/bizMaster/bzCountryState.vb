'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Tapan $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizMaster/bzCountryState.vb $
'$Workfile: bzCountryState.vb $
'$Revision: 10 $
'$Archive: /AAMS/Components/bizMaster/bzCountryState.vb $
'$Modtime: 17/07/08 6:51p $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizMaster
    Public Class bzCountryState
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzCountryState"
        Const strList_OUTPUT = "<MS_LISTSTATE_OUTPUT><STATE StateID='' StateName='' /><Errors Status=''><Error Code='' Description=''/></Errors></MS_LISTSTATE_OUTPUT>"
        Const strDELETE_INPUT = "<MS_DELETECOUNTRYSTATE_INPUT><StateID></StateID></MS_DELETECOUNTRYSTATE_INPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETECOUNTRYSTATE_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETECOUNTRYSTATE_OUTPUT>"
        Const strSEARCH_INPUT = "<MS_SEARCHCOUNTRYSTATE_INPUT><State_Name></State_Name><CountryID></CountryID></MS_SEARCHCOUNTRYSTATE_INPUT>"
        Const strSEARCH_OUTPUT = "<MS_SEARCHCOUNTRYSTATE_OUTPUT><STATE StateID='' State_Name='' CountryID='' Country_Name='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHCOUNTRYSTATE_OUTPUT>"

        Const strUPDATE_INPUT = "<MS_UPDATECOUNTRYSTATE_INPUT><STATE Action='' StateID='' State_Name='' CountryID='' /></MS_UPDATECOUNTRYSTATE_INPUT>"
        Const strUPDATE_OUTPUT = "<MS_UPDATECOUNTRYSTATE_OUTPUT><STATE Action='' StateID='' State_Name='' CountryID='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATECOUNTRYSTATE_OUTPUT>"
        Const strADDSTATE_OUTPUT = "<MS_UPDATECOUNTRYSTATE_INPUT><STATE Action='' StateID='' State_Name='' CountryID='' /></MS_UPDATECOUNTRYSTATE_INPUT>"
        Const strVIEW_INPUT = "<MS_VIEWCOUNTRYSTATE_INPUT><StateID></StateID></MS_VIEWCOUNTRYSTATE_INPUT>"
        Const srtVIEW_OUTPUT = "<MS_VIEWCOUNTRYSTATE_OUTPUT><STATE StateID='' State_Name='' CountryID='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWCOUNTRYSTATE_OUTPUT>"


        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add
            'Declare Output XML String
            '***********************************************************************
            'Purpose: 
            'Input:None
            'Output:Xml Document
            '<MS_UPDATECOUNTRYSTATE_INPUT>	
            '	<STATE Action='' StateID='' State_Name='' CountryID='' />	
            '</MS_UPDATECOUNTRYSTATE_INPUT>
            '**************************************************************************
            Dim strMETHOD_NAME As String = "Add"
            Dim objXMLDoc As New XmlDocument
            Try
                objXMLDoc.LoadXml(strADDSTATE_OUTPUT)
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
            '<MS_DELETECOUNTRYSTATE_INPUT>	
            '	<StateID></StateID>	
            '</MS_DELETECOUNTRYSTATE_INPUT>

            'Output :
            '<MS_DELETECOUNTRYSTATE_OUTPUT>		
            '	<Errors Status=''>
            '       <Error Code='' Description='' />
            '	</Errors>
            '</MS_DELETECOUNTRYSTATE_OUTPUT>
            '************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Delete"
            Dim strStateid As String

            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0
                strStateid = DeleteDoc.DocumentElement.SelectSingleNode("StateID").InnerText.Trim
                If strStateid = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_COUNTRY_STATE"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"

                    .Parameters.Add(New SqlParameter("@STATEID", SqlDbType.Int))
                    .Parameters("@STATEID").Value = strStateid

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
                bzShared.FillErrorStatus(objDeleteDocOutput, "101", "State in Use!")
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
            '<MS_SEARCHCOUNTRYSTATE_INPUT>	
            '	<State_Name></State_Name>
            '	<CountryID></CountryID>
            '</MS_SEARCHCOUNTRYSTATE_INPUT>

            'Output :
            '<MS_SEARCHCOUNTRYSTATE_OUTPUT>	
            '	<STATE StateID='' State_Name='' CountryID=''/>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_SEARCHCOUNTRYSTATE_OUTPUT>
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strState_Name As String = ""
            Dim intCountryID As Integer

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strState_Name = (SearchDoc.DocumentElement.SelectSingleNode("State_Name").InnerText.Trim())
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
                If (SearchDoc.DocumentElement.SelectSingleNode("CountryID").InnerText.Trim()) <> "" Then
                    intCountryID = (SearchDoc.DocumentElement.SelectSingleNode("CountryID").InnerText.Trim())
                End If
                '--------Retrieving & Checking Details from Input XMLDocument ------End
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SER_MS_COUNTRY_STATE"
                    .Connection = objSqlConnection

                    .Parameters.Add("@STATENAME", SqlDbType.VarChar, 50)
                    .Parameters("@STATENAME").Value = strState_Name

                    .Parameters.Add("@COUNTRYID", SqlDbType.Int)
                    If intCountryID = 0 Then
                        .Parameters("@COUNTRYID").Value = vbNullString
                    Else
                        .Parameters("@COUNTRYID").Value = intCountryID
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
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("STATE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("StateID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("StateID")))
                    objAptNodeClone.Attributes("State_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("State_Name")))
                    objAptNodeClone.Attributes("CountryID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CountryID")))
                    objAptNodeClone.Attributes("Country_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Country_Name")))
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
            '<MS_UPDATECOUNTRYSTATE_INPUT>	
            '	<STATE Action='' StateID='' State_Name='' CountryID='' />	
            '</MS_UPDATECOUNTRYSTATE_INPUT>
            'Output :
            '<MS_UPDATECOUNTRYSTATE_OUTPUT>	
            '	<STATE Action='' StateID='' State_Name='' CountryID='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_UPDATECOUNTRYSTATE_OUTPUT>
            '************************************************************************
            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objUpdateDocOutput As New XmlDocument
            Dim strStateid As String = ""
            Dim strStateName As String = ""
            Dim strcountryid As String = ""
            
            Dim intRecordsAffected As Int32
            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)

                With objUpdateDocOutput.DocumentElement.SelectSingleNode("STATE")
                    .Attributes("Action").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("STATE").Attributes("Action").InnerText
                    If .Attributes("Action").InnerText <> "I" Then
                        .Attributes("StateID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("STATE").Attributes("StateID").InnerText
                    End If
                    .Attributes("State_Name").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("STATE").Attributes("State_Name").InnerText
                    .Attributes("CountryID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("STATE").Attributes("CountryID").InnerText
                End With

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("STATE")
                    strAction = ((.Attributes("Action").InnerText).Trim).ToString
                    If Trim(strAction) <> "I" Then
                        strStateid = ((.Attributes("StateID").InnerText).Trim).ToString
                    End If
                    strStateName = ((.Attributes("State_Name").InnerText).Trim).ToString
                    strcountryid = ((.Attributes("CountryID").InnerText).Trim).ToString

                    If strAction = "I" Or strAction = "U" Then
                        If strStateName = "" Then
                            Throw (New AAMSException("State can't be blank."))
                        ElseIf strcountryid = "" Then
                            Throw (New AAMSException("Country can't be blank."))
                        End If
                    Else
                        Throw (New AAMSException("Invalid Action Code."))
                    End If

                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_COUNTRY_STATE"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction

                    .Parameters.Add(New SqlParameter("@STATEID", SqlDbType.Int))
                    If strStateid = "" Then
                        .Parameters("@STATEID").Value = vbNull
                    Else
                        .Parameters("@STATEID").Value = strStateid
                    End If

                    .Parameters.Add(New SqlParameter("@STATENAME", SqlDbType.VarChar, 50))
                    .Parameters("@STATENAME").Value = strStateName

                    .Parameters.Add(New SqlParameter("@COUNTRYID", SqlDbType.Int))
                    If strcountryid = "" Then
                        .Parameters("@COUNTRYID").Value = vbNull
                    Else
                        .Parameters("@COUNTRYID").Value = strcountryid
                    End If
                    
                    .Parameters.Add(New SqlParameter("@RETUNID", SqlDbType.Int))
                    .Parameters("@RETUNID").Direction = ParameterDirection.Output

                    .Parameters("@RETUNID").Value = ""

                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED
                    .Connection.Open()

                    intRecordsAffected = .ExecuteNonQuery()

                    If UCase(strAction) = "I" Then
                        intRetId = .Parameters("@RETUNID").Value
                        If intRetId = 0 Then
                            Throw (New Exception("Unable to insert!"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("STATE").Attributes("StateID").InnerText = intRetId
                        End If
                    ElseIf UCase(strAction) = "U" Then
                        intRetId = .Parameters("@RETUNID").Value
                        If intRetId = 0 Then
                            Throw (New AAMSException("Unable to update!"))
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
                        bzShared.FillErrorStatus(objUpdateDocOutput, "101", "State already exists. Please enter another State!")
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
            '<MS_VIEWCOUNTRYSTATE_INPUT>	
            '	<StateID></StateID>	
            '</MS_VIEWCOUNTRYSTATE_INPUT>

            'Output :
            '<MS_VIEWCOUNTRYSTATE_OUTPUT>	
            '	<STATE StateID='' State_Name='' CountryID='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_VIEWCOUNTRYSTATE_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strStateid As String = ""
            Const strMETHOD_NAME As String = "View"
            Dim blnRecordFound As Boolean
            Try
                objOutputXml.LoadXml(srtVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strStateid = IndexDoc.DocumentElement.SelectSingleNode("StateID").InnerText.Trim
                If strStateid = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_COUNTRY_STATE"
                    .Connection = objSqlConnection
                    .Parameters.Add("@STATEID", SqlDbType.Int)
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@STATEID").Value = strStateid
                    .Parameters("@ACTION").Value = "S"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("STATE")
                        .Attributes("StateID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("StateID")))
                        .Attributes("State_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("State_Name")))
                        .Attributes("CountryID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CountryID")))
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
            'Purpose: To list out the airport record, based on the given field value
            'Input  : 
            'Output :  
            ' <MS_LISTSTATE_OUTPUT>
            '	<CITY StateID="" StateName="" />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_LISTSTATE_OUTPUT>

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "List"
            objOutputXml.LoadXml(strList_OUTPUT)
            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LST_MS_COUNTRY_STATE"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("STATE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)

                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("StateID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("STATEID")))
                    objAptNodeClone.Attributes("StateName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("STATE_NAME")))
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
