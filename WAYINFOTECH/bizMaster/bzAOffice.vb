'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Tapan $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizMaster/bzAOffice.vb $
'$Workfile: bzAOffice.vb $
'$Revision: 17 $
'$Archive: /AAMS/Components/bizMaster/bzAOffice.vb $
'$Modtime: 2/06/09 1:54p $
'$Last Modtime: 20/11/07 6:15p (Add method "SearchAofficeCity")

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizMaster

    Public Class bzAOffice
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzAOffice"
        Const strList_OUTPUT = "<MS_LISTAOFFICE_OUTPUT><AOFFICE Aoffice='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTAOFFICE_OUTPUT>"
        Const strVIEW_INPUT = "<MS_VIEWAOFFICE_INPUT><Aoffice></Aoffice></MS_VIEWAOFFICE_INPUT>"
        Const srtVIEW_OUTPUT = "<MS_VIEWAOFFICE_OUTPUT><AOFFICE Aoffice='' Region='' Address='' RegionalHQ='' BRLimit='' BR_ID='' Fax='' Pincode=''	CityID='' Phone='' /><Errors Status=''>	<Error Code='' Description='' /></Errors></MS_VIEWAOFFICE_OUTPUT>"
        Const strSEARCH_INPUT = "<MS_SEARCHAOFFICE_INPUT><Aoffice></Aoffice></MS_SEARCHAOFFICE_INPUT>"
        Const strSEARCH_OUTPUT = "<MS_SEARCHAOFFICE_OUTPUT><AOFFICE Aoffice='' City_Name='' Country_Name='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHAOFFICE_OUTPUT>"

        Const strSEARCHAOFFICECITY_OUTPUT = "<MS_SEARCHAOFFICECITY_OUTPUT><AOFFICECITY Aoffice='' CityID='' City_Name='' Region=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHAOFFICECITY_OUTPUT>"
        Const StrADDAOFFICE_OUTPUT = "<MS_UPDATEAOFFICE_INPUT><AOFFICE Action='' Aoffice='' Region='' Address='' RegionalHQ='' BRLimit='' BR_ID='' Fax='' Pincode='' CityID='' Phone='' /></MS_UPDATEAOFFICE_INPUT>"
        Const strDELETE_INPUT = "<MS_DELETEAOFFICE_INPUT><Aoffice></Aoffice></MS_DELETEAOFFICE_INPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETEAOFFICE_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETEAOFFICE_OUTPUT>"
        Const strUPDATE_INPUT = "<MS_UPDATEAOFFICE_INPUT><AOFFICE Action='' Aoffice='' Region='' Address='' RegionalHQ='' BRLimit='' BR_ID='' Fax='' Pincode='' CityID='' Phone='' /></MS_UPDATEAOFFICE_INPUT>"
        Const strUPDATE_OUTPUT = "<MS_UPDATEAOFFICE_OUTPUT><AOFFICE Action='' Aoffice='' Region='' Address='' RegionalHQ='' BRLimit='' BR_ID='' Fax='' Pincode='' CityID='' Phone='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEAOFFICE_OUTPUT>"

        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add
            'Declare Output XML String
            '***********************************************************************
            'Purpose: 
            'Input:None
            'Output:Xml Document
            '<MS_UPDATEAOFFICE_INPUT>
            '   <AOFFICE Action='' Aoffice='' Region='' Address='' RegionalHQ='' BRLimit='' BR_ID='' Fax='' Pincode='' CityID='' Phone='' />
            '</MS_UPDATEAOFFICE_INPUT>
            '**************************************************************************
            Dim strMETHOD_NAME As String = "Add"
            Dim objXMLDoc As New XmlDocument
            Try
                objXMLDoc.LoadXml(StrADDAOFFICE_OUTPUT)
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
            '<MS_DELETEAOFFICE_INPUT>
            '	<Aoffice></Aoffice>
            '</MS_DELETEAOFFICE_INPUT>

            'Output :
            '<MS_DELETEAOFFICE_OUTPUT>
            '	<Errors Status=''>
            '       <Error Code='' Description='' />
            '   </Errors>
            '</MS_DELETEAOFFICE_OUTPUT>
            '************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Delete"
            Dim strAoffice As String

            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0
                strAoffice = DeleteDoc.DocumentElement.SelectSingleNode("Aoffice").InnerText.Trim
                If strAoffice = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_AOFFICE"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"

                    .Parameters.Add(New SqlParameter("@AOFFICE", SqlDbType.VarChar, 3))
                    .Parameters("@AOFFICE").Value = strAoffice

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
                bzShared.FillErrorStatus(objDeleteDocOutput, "101", "Aoffice in Use!")
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
            '<MS_SEARCHAOFFICE_INPUT>
            '	<Aoffice></Aoffice>
            '</MS_SEARCHAOFFICE_INPUT>

            'Output :
            '<MS_SEARCHAOFFICE_OUTPUT>
            '<AOFFICE Aoffice='' City_Name='' Country_Name='' />
            '   <Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_SEARCHAOFFICE_OUTPUT>
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strAoffice As String = ""

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean


            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strAoffice = (SearchDoc.DocumentElement.SelectSingleNode("Aoffice").InnerText.Trim())

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
                    .CommandText = "UP_SER_MS_AOFFICE"
                    .Connection = objSqlConnection

                    .Parameters.Add("@AOFFICE", SqlDbType.VarChar, 3)
                    .Parameters("@AOFFICE").Value = strAoffice

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
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AOFFICE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("Aoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AOFFICE")))
                    objAptNodeClone.Attributes("City_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITY_NAME")))
                    objAptNodeClone.Attributes("Country_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("COUNTRY_NAME")))
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
            '<MS_UPDATEAOFFICE_INPUT>
            '	<AOFFICE Action='' Aoffice='' Region='' Address='' RegionalHQ='' BRLimit='' BR_ID='' Fax=''	Pincode='' CityID='' Phone='' />
            '</MS_UPDATEAOFFICE_INPUT>

            'Output :
            '<MS_UPDATEAOFFICE_OUTPUT>
            '   <AOFFICE Action='' Aoffice='' Region='' Address='' RegionalHQ='' BRLimit='' BR_ID='' Fax=''
            '		Pincode='' CityID='' Phone='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_UPDATEAOFFICE_OUTPUT>
            '************************************************************************
            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objUpdateDocOutput As New XmlDocument
            Dim strAoffice As String
            Dim strRegion As String
            Dim strAddress As String
            Dim strRegionalHQ As String
            Dim intBRLimit As Integer
            'Dim intBR_ID As Integer
            Dim strFax As String
            Dim strPincode As String
            Dim strCityID As String
            Dim strPhone As String

            Dim intRecordsAffected As Int32
            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)

                With objUpdateDocOutput.DocumentElement.SelectSingleNode("AOFFICE")
                    .Attributes("Action").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AOFFICE").Attributes("Action").InnerText
                    .Attributes("Aoffice").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AOFFICE").Attributes("Aoffice").InnerText
                    .Attributes("Region").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AOFFICE").Attributes("Region").InnerText
                    .Attributes("Address").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AOFFICE").Attributes("Address").InnerText
                    .Attributes("RegionalHQ").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AOFFICE").Attributes("RegionalHQ").InnerText
                    .Attributes("BRLimit").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AOFFICE").Attributes("BRLimit").InnerText
                    '.Attributes("BR_ID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AOFFICE").Attributes("BR_ID").InnerText
                    .Attributes("Fax").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AOFFICE").Attributes("Fax").InnerText
                    .Attributes("Pincode").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AOFFICE").Attributes("Pincode").InnerText
                    .Attributes("CityID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AOFFICE").Attributes("CityID").InnerText
                    .Attributes("Phone").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("AOFFICE").Attributes("Phone").InnerText
                End With

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("AOFFICE")
                    strAction = ((.Attributes("Action").InnerText).Trim).ToString
                    strAoffice = ((.Attributes("Aoffice").InnerText).Trim).ToString
                    strRegion = ((.Attributes("Region").InnerText).Trim).ToString
                    strAddress = ((.Attributes("Address").InnerText).Trim).ToString
                    strRegionalHQ = ((.Attributes("RegionalHQ").InnerText).Trim).ToString
                    intBRLimit = (.Attributes("BRLimit").InnerText)
                    ' intBR_ID = (.Attributes("BR_ID").InnerText)
                    strFax = ((.Attributes("Fax").InnerText).Trim).ToString
                    strPincode = ((.Attributes("Pincode").InnerText).Trim).ToString
                    strCityID = (.Attributes("CityID").InnerText)
                    strPhone = ((.Attributes("Phone").InnerText).Trim).ToString

                    If strAction = "I" Or strAction = "U" Then
                        If strAoffice = "" Then
                            Throw (New AAMSException("Aoffice can't be blank."))
                        ElseIf strCityID = "" Then
                            Throw (New AAMSException("City can't be blank."))
                        End If
                    Else
                        Throw (New AAMSException("Invalid Action Code."))
                    End If

                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_AOFFICE"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction

                    .Parameters.Add(New SqlParameter("@AOFFICE", SqlDbType.VarChar, 3))
                    .Parameters("@AOFFICE").Value = strAoffice

                    .Parameters.Add(New SqlParameter("@REGION", SqlDbType.VarChar, 10))
                    .Parameters("@REGION").Value = strRegion

                    .Parameters.Add(New SqlParameter("@ADDRESS", SqlDbType.VarChar, 255))
                    .Parameters("@ADDRESS").Value = strAddress


                    .Parameters.Add(New SqlParameter("@REGIONALHQ", SqlDbType.VarChar, 3))
                    .Parameters("@REGIONALHQ").Value = strRegionalHQ

                    .Parameters.Add(New SqlParameter("@BRLIMIT", SqlDbType.Int))
                    .Parameters("@BRLIMIT").Value = intBRLimit

                    '.Parameters.Add(New SqlParameter("@BRID", SqlDbType.Int))
                    '.Parameters("@BRID").Value = intBR_ID


                    .Parameters.Add(New SqlParameter("@FAX", SqlDbType.VarChar, 30))
                    .Parameters("@FAX").Value = strFax

                    .Parameters.Add(New SqlParameter("@PINCODE", SqlDbType.VarChar, 30))
                    .Parameters("@PINCODE").Value = strPincode

                    .Parameters.Add(New SqlParameter("@CITYID", SqlDbType.Int))
                    .Parameters("@CITYID").Value = strCityID

                    .Parameters.Add(New SqlParameter("@PHONE", SqlDbType.VarChar, 30))
                    .Parameters("@PHONE").Value = strPhone


                    .Parameters.Add(New SqlParameter("@RETUNID", SqlDbType.Int))
                    .Parameters("@RETUNID").Direction = ParameterDirection.Output

                    .Parameters("@RETUNID").Value = ""

                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED
                    .Connection.Open()

                    intRecordsAffected = .ExecuteNonQuery()

                    If UCase(strAction) = "I" Then
                        intRetId = .Parameters("@RETUNID").Value
                        If intRetId = 0 Then
                            Throw (New AAMSException("Unable to insert!"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
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
                        bzShared.FillErrorStatus(objUpdateDocOutput, "101", "Aoffice code already exists. Please enter another Aoffice")
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
            '<MS_VIEWAOFFICE_INPUT>
            '   <Aoffice></Aoffice>
            '</MS_VIEWAOFFICE_INPUT>

            'Output :
            '<MS_VIEWAOFFICE_OUTPUT>
            '	<AOFFICE Aoffice='' Region='' Address='' RegionalHQ='' BRLimit='' BR_ID='' Fax='' Pincode=''
            '		CityID='' Phone='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_VIEWAOFFICE_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strAoffice As String
            Const strMETHOD_NAME As String = "View"
            Dim blnRecordFound As Boolean
            Try
                objOutputXml.LoadXml(srtVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strAoffice = IndexDoc.DocumentElement.SelectSingleNode("Aoffice").InnerText.Trim
                If strAoffice = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_AOFFICE"
                    .Connection = objSqlConnection
                    .Parameters.Add("@AOFFICE", SqlDbType.Char, 3)
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@AOFFICE").Value = strAoffice
                    .Parameters("@ACTION").Value = "S"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("AOFFICE")
                        .Attributes("Aoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AOFFICE")) & "")
                        .Attributes("Region").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("REGION")) & "")
                        .Attributes("Address").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ADDRESS")) & "")
                        .Attributes("RegionalHQ").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("REGIONALHQ")) & "")
                        .Attributes("BRLimit").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("BRLIMIT")) & "")
                        .Attributes("BR_ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("BR_ID")) & "")
                        .Attributes("Fax").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FAX")) & "")
                        .Attributes("Pincode").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PINCODE")) & "")
                        .Attributes("CityID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITY")) & "")
                        .Attributes("Phone").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PHONE")) & "")
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
            '<MS_LISTAOFFICE_OUTPUT>
            '	<AOFFICE Aoffice='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_LISTAOFFICE_OUTPUT>
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
                    .CommandText = "UP_LST_MS_AOFFICE"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AOFFICE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)

                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("Aoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AOFFICE")))
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


        Public Function ListHQ() As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the airport record, based on the given field value
            'Input  : 
            'Output :  
            '<MS_LISTAOFFICEHQ_OUTPUT>
            '	<AOFFICE Aoffice='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_LISTAOFFICEHQ_OUTPUT>
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
                    .CommandText = "UP_LST_MS_AOFFICEHQ"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AOFFICE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)

                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("Aoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AOFFICE")))
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
        Public Function SearchAofficeCity(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            ''Added by Tapan Nath on 20/11/2007
            '***********************************************************************
            ''Purpose : To display CityList according to Aoffice
            'Input  : 
            '<MS_SEARCHAOFFICECITY_INPUT>
            '   <Aoffice></Aoffice>
            '</MS_SEARCHAOFFICECITY_INPUT>

            'Output :  
            '<MS_SEARCHAOFFICECITY_OUTPUT>
            '	<AOFFICECITY Aoffice='' CityID='' City_Name='' Region=''/>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_SEARCHAOFFICECITY_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strAoffice As String = ""
            Dim strMETHOD_NAME As String = "SearchAofficeCity"

            objOutputXml.LoadXml(strSEARCHAOFFICECITY_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strAoffice = (IndexDoc.DocumentElement.SelectSingleNode("Aoffice").InnerText.Trim())
                '--------Retrieving & Checking Details from Input XMLDocument ------End
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SER_MS_AOFFICECITY"
                    .Parameters.Add("@AOFFICE", SqlDbType.VarChar, 3)
                    .Parameters("@AOFFICE").Value = strAoffice
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AOFFICECITY")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)

                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("Aoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Aoffice")))
                    objAptNodeClone.Attributes("CityID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CityID")))
                    objAptNodeClone.Attributes("City_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("City_Name")))
                    objAptNodeClone.Attributes("Region").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Region")))
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
