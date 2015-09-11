'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Tapan $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizMaster/bzCity.vb $
'$Workfile: bzCity.vb $
'$Revision: 23 $
'$Archive: /AAMS/Components/bizMaster/bzCity.vb $
'$Modtime: 17/07/08 5:34p $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizMaster
    Public Class bzCity
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzCity"
        Const gstrList_OUTPUT = "<MS_LISTCITY_OUTPUT><CITY CityID='' CityCode='' City_Name='' Country_Name='' Aoffice='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTCITY_OUTPUT>"
        Const srtVIEW_OUTPUT = "<MS_VIEWCITY_OUTPUT><CITY_DETAIL CityID='' CityCode='' City_Name='' Aoffice='' CountryID='' StateID='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWCITY_OUTPUT>"

        Const strSEARCH_OUTPUT = "<MS_SEARCHCITY_OUTPUT><CITY_DETAIL CityID='' CityCode='' City_Name='' Aoffice='' StateID='' State='' CountryID='' Country='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHCITY_OUTPUT>"
        Const StrADDCITY_OUTPUT = "<MS_UPDATECITY_INPUT><CITY_DETAIL Action='' CityID='' CityCode='' City_Name='' Aoffice='' CountryID='' StateID='' /></MS_UPDATECITY_INPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETECITY_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETECITY_OUTPUT>"
        Const strUPDATE_OUTPUT = "<MS_UPDATECITY_OUTPUT><CITY_DETAIL Action='' CityID='' CityCode='' City_Name='' Aoffice='' CountryID='' StateID='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATECITY_OUTPUT>"

        Const strCITYDETAIL_OUTPUT = "<MS_GETCITYCODE_OUTPUT> <CITY_DETAIL CityID='' CityCode='' City_Name='' Aoffice='' CountryID='' StateID='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_GETCITYCODE_OUTPUT>"

        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add
            'Declare Output XML String
            '***********************************************************************
            'Purpose: 
            'Input:None
            'Output:Xml Document
            '<MS_UPDATECITY_INPUT>
            '	<CITY_DETAIL Action="" CityID="" CityCode="" City_Name="" Aoffice="" CountryID="" StateID="" />
            '</MS_UPDATECITY_INPUT>  
            '**************************************************************************
            Dim strMETHOD_NAME As String = "Add"
            Dim objXMLDoc As New XmlDocument
            Try
                objXMLDoc.LoadXml(StrADDCITY_OUTPUT)
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
            '<MS_DELETECITY_INPUT>
            '	<CityID></CityID>
            '</MS_DELETECITY_INPUT>           
            'Output :
            '<MS_DELETECITY_OUTPUT>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_DELETECITY_OUTPUT>            
            '************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Delete"
            Dim strCITY_id As String

            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0
                strCITY_id = DeleteDoc.DocumentElement.SelectSingleNode("CityID").InnerText.Trim
                If strCITY_id = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_CITY"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"
                    .Parameters.Add(New SqlParameter("@CITYID", SqlDbType.Int))
                    .Parameters("@CITYID").Value = strCITY_id
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
            '<MS_SEARCHCITY_INPUT>
            '	<City_Name></City_Name>
            '</MS_SEARCHCITY_INPUT>

            'Output :
            '<MS_SEARCHCITY_OUTPUT>
            '	<CITY_DETAIL CityID="" CityCode="" City_Name="" Aoffice="" StateID="" State="" CountryID="" Country="" />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_SEARCHCITY_OUTPUT>
            '        '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strCT_NAME As String

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strCT_NAME = (SearchDoc.DocumentElement.SelectSingleNode("City_Name").InnerText.Trim())
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
                    .CommandText = "UP_SER_MS_CITY"
                    .Connection = objSqlConnection
                    .Parameters.Add("@CITY_NAME", SqlDbType.VarChar, 40)
                    .Parameters("@CITY_NAME").Value = strCT_NAME
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
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("CITY_DETAIL")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("CityID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITYID")))
                    objAptNodeClone.Attributes("CityCode").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITYCODE")))
                    objAptNodeClone.Attributes("City_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITY_NAME")))
                    objAptNodeClone.Attributes("Aoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AOFFICE")))
                    objAptNodeClone.Attributes("State").InnerText = (objSqlReader.GetValue(objSqlReader.GetOrdinal("STATE_NAME")))
                    objAptNodeClone.Attributes("Country").InnerText = (objSqlReader.GetValue(objSqlReader.GetOrdinal("COUNTRY_NAME")))
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
            '***********************************************************************
            'Purpose:This function Inserts/Updates City.
            'Input  :
            '<MS_UPDATECITY_INPUT>
            '	<CITY_DETAIL Action="" CityID="" CityCode="" City_Name="" Aoffice="" CountryID="" StateID="" />
            '</MS_UPDATECITY_INPUT>  
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
            Dim strCT_CODE As String
            Dim strCT_NAME As String
            Dim strAoffice As String
            Dim intCT_ID As Integer
            Dim intCOUNTRYID As Integer
            Dim intSTATEID As Integer
            Dim intRecordsAffected As Int32
            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)

                With objUpdateDocOutput.DocumentElement.SelectSingleNode("CITY_DETAIL")
                    .Attributes("Action").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("Action").InnerText
                    .Attributes("CityID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("CityID").InnerText
                    .Attributes("CityCode").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("CityCode").InnerText
                    .Attributes("City_Name").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("City_Name").InnerText
                    .Attributes("Aoffice").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("Aoffice").InnerText
                    .Attributes("CountryID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("CountryID").InnerText
                    .Attributes("StateID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("StateID").InnerText
                End With

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("CITY_DETAIL")
                    strAction = ((.Attributes("Action").InnerText).Trim).ToString
                    strCT_CODE = ((.Attributes("CityCode").InnerText).Trim).ToString
                    strCT_NAME = ((.Attributes("City_Name").InnerText).Trim).ToString
                    If ((.Attributes("CityID").InnerText).Trim = "") Then
                        intCT_ID = 0
                    Else
                        intCT_ID = ((.Attributes("CityID").InnerText).Trim)
                    End If
                    strAoffice = ((.Attributes("Aoffice").InnerText).Trim).ToString
                    If ((.Attributes("CountryID").InnerText).Trim = "") Then
                        intCOUNTRYID = 0
                    Else
                        intCOUNTRYID = ((.Attributes("CountryID").InnerText).Trim)
                    End If
                    If ((.Attributes("StateID").InnerText).Trim = "") Then
                        intSTATEID = 0
                    Else
                        intSTATEID = ((.Attributes("StateID").InnerText).Trim)
                    End If



                    If strAction = "I" Or strAction = "U" Then
                        If strCT_CODE = "" Then
                            Throw (New AAMSException("City code can't be blank."))
                        ElseIf strCT_NAME = "" Then
                            Throw (New AAMSException("City Name can't be blank."))
                        End If
                    Else
                        Throw (New AAMSException("Invalid Action Code."))
                    End If

                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_CITY"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction

                    .Parameters.Add(New SqlParameter("@CITYCODE", SqlDbType.VarChar, 3))
                    .Parameters("@CITYCODE").Value = strCT_CODE

                    .Parameters.Add(New SqlParameter("@CITY_NAME", SqlDbType.VarChar, 40))
                    .Parameters("@CITY_NAME").Value = strCT_NAME

                    .Parameters.Add(New SqlParameter("@CITYID", SqlDbType.Int))
                    .Parameters("@CITYID").Value = intCT_ID

                    .Parameters.Add(New SqlParameter("@AOFFICE", SqlDbType.VarChar, 3))
                    If (strAoffice = "") Then
                        .Parameters("@AOFFICE").Value = DBNull.Value
                    Else
                        .Parameters("@AOFFICE").Value = strAoffice
                    End If
                    .Parameters.Add(New SqlParameter("@COUNTRYID", SqlDbType.SmallInt, 4))
                    If (intCOUNTRYID = 0) Then
                        .Parameters("@COUNTRYID").Value = DBNull.Value
                    Else
                        .Parameters("@COUNTRYID").Value = intCOUNTRYID
                    End If


                    .Parameters.Add(New SqlParameter("@STATEID", SqlDbType.SmallInt, 4))
                    If (intSTATEID = 0) Then
                        .Parameters("@STATEID").Value = DBNull.Value
                    Else
                        .Parameters("@STATEID").Value = intSTATEID
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
                            Throw (New AAMSException("City Code Already Exists!"))
                        End If
                        If intRetId = 0 Then
                            Throw (New AAMSException("City Name Already Exists!"))
                        End If
                        With objUpdateDocOutput.DocumentElement.SelectSingleNode("CITY_DETAIL")
                            .Attributes("CityID").InnerText = intRetId
                        End With
                    ElseIf UCase(strAction) = "U" Then
                        intRetId = .Parameters("@RETURNID").Value
                        If intRetId = 0 Then
                            Throw (New AAMSException("Unable to update!"))
                        End If
                    End If

                End With
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                If intRetId = 0 Then
                    bzShared.FillErrorStatus(objUpdateDocOutput, "101", "City already exists. Please enter another City.")
                Else
                    bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                End If
                Return objUpdateDocOutput
            Catch Exec As Exception
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                Return objUpdateDocOutput
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try

            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Return objUpdateDocOutput

        End Function

        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.View
            '***********************************************************************
            'Purpose:This function gives details of City.
            'Input  :
            '<MS_VIEWCITY_INPUT>
            '	<CityID></CityID>
            '</MS_VIEWCITY_INPUT>
            'Output :
            '<MS_VIEWCITY_OUTPUT>
            '	<CITY_DETAIL CityID="" CityCode="" City_Name="" Aoffice="" CountryID="" StateID="" />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_VIEWCITY_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strCT_ID As String
            Const strMETHOD_NAME As String = "View"

            Try
                objOutputXml.LoadXml(srtVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strCT_ID = IndexDoc.DocumentElement.SelectSingleNode("CityID").InnerText.Trim
                If strCT_ID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_CITY"
                    .Connection = objSqlConnection
                    .Parameters.Add("@CITYID", SqlDbType.Char, 3)
                    .Parameters("@CITYID").Value = strCT_ID
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    Dim blnRecordFound As Boolean
                    If blnRecordFound = False Then
                        With objOutputXml.DocumentElement.SelectSingleNode("CITY_DETAIL")
                            .Attributes("CityID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITYID")))
                            .Attributes("CityCode").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITYCODE")))
                            .Attributes("City_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITY_NAME")))
                            .Attributes("Aoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AOFFICE")))
                            .Attributes("CountryID").InnerText = Convert.ToInt16(Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("COUNTRYID"))))
                            .Attributes("StateID").InnerText = Convert.ToInt16(Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("STATEID"))))
                        End With
                    End If
                    blnRecordFound = True
                Loop
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
            objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Return objOutputXml
        End Function

        Public Function List() As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: List out City 
            'Input  : 
            'Output :  
            '<MS_LISTCITY_OUTPUT>
            '    <CITY CityID="" CityCode="" City_Name="" Country_Name="" />
            '    <Errors Status=''>
            '        <Error Code='' Description='' />
            '    </Errors>
            '</MS_LISTCITY_OUTPUT>
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
                    .CommandText = "UP_LST_MS_CITY"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("CITY")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)

                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("CityID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITYID")))
                    objAptNodeClone.Attributes("CityCode").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITYCODE")))
                    objAptNodeClone.Attributes("City_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITY_NAME")))
                    objAptNodeClone.Attributes("Country_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Country_Name")) & "")
                    objAptNodeClone.Attributes("Aoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Aoffice")) & "")
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
        Public Function GetCityDetails(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function gives details of a City according to CityName.
            'Input  :
            '<MS_GETCITYCODE_INPUT>
            '	<City_Name></City_Name>
            '</MS_GETCITYCODE_INPUT>
            'Output :
            '<MS_GETCITYCODE_OUTPUT>
            '	<CITY_DETAIL CityID="" CityCode="" City_Name="" Aoffice="" CountryID="" StateID="" />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_GETCITYCODE_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strCityName As String
            Const strMETHOD_NAME As String = "GetCityDetails"

            Try
                objOutputXml.LoadXml(strCITYDETAIL_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strCityName = IndexDoc.DocumentElement.SelectSingleNode("City_Name").InnerText.Trim
                If strCityName = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_GET_CITY_DETAILS"
                    .Connection = objSqlConnection

                    .Parameters.Add("@CITY_NAME", SqlDbType.VarChar, 100)
                    .Parameters("@CITY_NAME").Value = strCityName

                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    Dim blnRecordFound As Boolean
                    If blnRecordFound = False Then
                        With objOutputXml.DocumentElement.SelectSingleNode("CITY_DETAIL")
                            .Attributes("CityID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITYID")) & "")
                            .Attributes("CityCode").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITYCODE")) & "")
                            .Attributes("City_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITY_NAME")) & "")
                            .Attributes("Aoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AOFFICE")) & "")
                            .Attributes("CountryID").InnerText = Convert.ToInt16(Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("COUNTRYID"))) & "")
                            .Attributes("StateID").InnerText = Convert.ToInt16(Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("STATEID"))) & "")
                        End With
                    End If
                    blnRecordFound = True
                Loop
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
            objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Return objOutputXml
        End Function
    End Class
End Namespace
