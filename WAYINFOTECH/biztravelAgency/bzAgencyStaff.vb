'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Tapan $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizTravelAgency/bzAgencyStaff.vb $
'$Workfile: bzAgencyStaff.vb $
'$Revision: 25 $
'$Archive: /AAMS/Components/bizTravelAgency/bzAgencyStaff.vb $
'$Modtime: 21/09/11 4:25p $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizTravelAgency
    Public Class bzAgencyStaff
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzAgencyStaff"
        Const StrSEARCH_INPUT = "<TA_SEARCHSTAFF_INPUT><SIGNINID></SIGNINID><STAFFNAME></STAFFNAME><AGENCYNAME></AGENCYNAME></TA_SEARCHSTAFF_INPUT>"
        Const StrSEARCH_OUTPUT = "<TA_SEARCHSTAFF_OUTPUT><STAFF AGENCYSTAFFID='' LCODE='' AGENCYNAME='' ADDRESS='' ADDRESS1='' CITY='' COUNTRY='' PHONE='' FAX='' SIGNINID='' STAFFNAME='' DESIGNATION='' EMAIL='' MOBILENO='' MARTIALSTATUS='' FIRSTNAME='' MIDDLENAME='' SURNAME=''/><PAGE PAGE_COUNT='' TOTAL_ROWS='' /><Errors Status=''><Error Code='' Description='' /></Errors></TA_SEARCHSTAFF_OUTPUT>"

        Const strADDAGENCYSTAFF_OUTPUT = "<MS_UPDATEAGENCYSTAFFDETAILS_INPUT><AGENCYSTAFF AGENCYSTAFFID='' LCODE='' STAFFNAME='' DESIGNATION='' DOW='' DOB='' CORRESPONDENCE='' RESPONSIBLE='' FAX='' PHONE='' EMAIL='' NOTES='' /></MS_UPDATEAGENCYSTAFFDETAILS_INPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETEAGENCYSTAFFDETAILS_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETEAGENCYSTAFFDETAILS_OUTPUT>"
        Const strUPDATE_OUTPUT = "<MS_UPDATEAGENCYSTAFFDETAILS_OUTPUT><AGENCYSTAFF AGENCYSTAFFID='' LCODE='' STAFFNAME='' DESIGNATION='' DOW='' DOB='' CORRESPONDENCE='' RESPONSIBLE='' FAX='' PHONE='' EMAIL='' NOTES='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEAGENCYSTAFFDETAILS_OUTPUT>"
        Const strVIEW_INPUT = "<MS_VIEWAGENCYSTAFF_INPUT><AGENCYSTAFFID></AGENCYSTAFFID></MS_VIEWAGENCYSTAFF_INPUT>"

        Const strVIEW_OUTPUT = "<MS_VIEWAGENCYSTAFF_OUTPUT><AGENCYSTAFF AGENCYSTAFFID='' LCODE='' OFFICEID='' STAFFNAME='' DESIGNATION='' DOW='' DOB='' CORRESPONDENCE='' RESPONSIBLE='' FAX = '' PHONE='' EMAIL='' NOTES='' CONTACT_PERSON='' SIGNINID='' TITLE='' FIRSTNAME='' MIDDLENAME='' SURNAME='' MOBILENO='' MARTIALSTATUS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWAGENCYSTAFF_OUTPUT>"
        Const srtGETAGENCYSTAFFDETAILS_OUTPUT = "<UP_GETAGENCYSTAFFDETAILS_OUTPUT><AGENCYSTAFF LCODE='' AGENCYSTAFFID='' STAFFNAME='' DESIGNATION='' EMAILID='' /><Errors Status=''><Error Code='' Description='' /></Errors></UP_GETAGENCYSTAFFDETAILS_OUTPUT>"


        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add
            'Declare Output XML String
            '***********************************************************************
            'Purpose: 
            'Input:None
            'Output:Xml Document
            '<MS_UPDATEAGENCYSTAFFDETAILS_INPUT>
            '<AGENCYSTAFF AGENCYSTAFFID='' LCODE='' STAFFNAME='' DESIGNATION='' DOW='' DOB='' CORRESPONDENCE='' RESPONSIBLE='' FAX='' PHONE='' EMAIL='' NOTES='' />
            '</MS_UPDATEAGENCYSTAFFDETAILS_INPUT>
            '**************************************************************************
            Dim strMETHOD_NAME As String = "Add"
            Dim objXMLDoc As New XmlDocument
            Try
                objXMLDoc.LoadXml(strADDAGENCYSTAFF_OUTPUT)
            Catch Exec As Exception
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objXMLDoc, "101", Exec.Message)
                Return objXMLDoc
            End Try
            Return objXMLDoc
        End Function

        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Delete
            '***********************************************************************
            'Purpose:This function deletes a Agency Staff.

            'Input:XmlDocument
            '<MS_DELETEAGENCYSTAFFDETAILS_INPUT>
            '<AGENCYSTAFFID></AGENCYSTAFFID>
            '</MS_DELETEAGENCYSTAFFDETAILS_INPUT>

            'Output :
            '<MS_DELETEAGENCYSTAFFDETAILS_OUTPUT>
            '   <Errors Status=''>
            '        <Error Code='' Description='' />
            '   </Errors>
            '</MS_DELETEAGENCYSTAFFDETAILS_OUTPUT>
            '************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Delete"
            Dim strAGENCYSTAFFID_id As String

            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0
                strAGENCYSTAFFID_id = DeleteDoc.DocumentElement.SelectSingleNode("AGENCYSTAFFID").InnerText.Trim
                If strAGENCYSTAFFID_id = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_AGENCYSTAFFDETAILS"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"
                    .Parameters.Add(New SqlParameter("@AGENCYSTAFFID", SqlDbType.Int))
                    .Parameters("@AGENCYSTAFFID").Value = strAGENCYSTAFFID_id
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
            '---------implemented by ManojGarg ----------
            '***********************************************************************
            'Purpose:This function gives search results based on choosen search criterion.
            'Input  :- 
            '<TA_SEARCHSTAFF_INPUT><STAFFNAME></STAFFNAME><AGENCYNAME></AGENCYNAME>
            '<EmployeeID></EmployeeID>
            '<Limited_To_Aoffice></Limited_To_Aoffice>
            '<Limited_To_Region></Limited_To_Region>
            '<Limited_To_OwnAagency></Limited_To_OwnAagency>
            '<Source></Source>
            '</TA_SEARCHSTAFF_INPUT>
            'Output :
            '<TA_SEARCHSTAFF_OUTPUT>
            '	<STAFF AGENCYSTAFFID='' STAFFNAME='' LCODE='' AGENCYNAME=''
            '	ADDRESS='' ADDRESS1='' CITY='' COUNTRY='' PHONE='' FAX='' />
            '   <PAGE PAGE_COUNT='' TOTAL_ROWS='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</TA_SEARCHSTAFF_OUTPUT>
            ''************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader

            Dim strLimitedToAoffice As String
            Dim strSource As String = ""
            Dim intLimitedToRegion, intLimitedToOwnAgency As Integer, intRESP_1A As Integer, intLcode As Integer
            'Dim objDS As DataSet
            'Dim objSqlDA As New SqlDataAdapter

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Dim blnRecordFound As Boolean
            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(StrSEARCH_OUTPUT)


            If SearchDoc.DocumentElement.SelectSingleNode("EmployeeID") IsNot Nothing Then
                If SearchDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim() <> "" Then
                    strLimitedToAoffice = SearchDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                End If
            End If

            If SearchDoc.DocumentElement.SelectSingleNode("Limited_To_Region") IsNot Nothing Then
                If SearchDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.Trim() = "" Then
                    intLimitedToRegion = 0
                Else
                    intLimitedToRegion = SearchDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.Trim()
                End If
            End If
            If SearchDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAagency") IsNot Nothing Then
                If SearchDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText.Trim() = "" Then
                    intLimitedToOwnAgency = 0
                Else
                    intLimitedToOwnAgency = SearchDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText.Trim()
                End If
            End If

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
            Dim strEmployeeID As String = "", strSigninID As String = ""
            '
            If SearchDoc.DocumentElement.SelectSingleNode("SIGNINID") IsNot Nothing Then
                If SearchDoc.DocumentElement.SelectSingleNode("SIGNINID").InnerText.Trim() <> "" Then
                    strSigninID = SearchDoc.DocumentElement.SelectSingleNode("SIGNINID").InnerText.Trim()
                End If
            End If

            If SearchDoc.DocumentElement.SelectSingleNode("EmployeeID") IsNot Nothing Then
                If SearchDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim() <> "" Then
                    strEmployeeID = SearchDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                End If
            End If
            If SearchDoc.DocumentElement.SelectSingleNode("Source") IsNot Nothing Then
                If SearchDoc.DocumentElement.SelectSingleNode("Source").InnerText.Trim() <> "" Then
                    strSource = SearchDoc.DocumentElement.SelectSingleNode("Source").InnerText.Trim()
                End If
            End If
            If SearchDoc.DocumentElement.SelectSingleNode("LCODE") IsNot Nothing Then
                If SearchDoc.DocumentElement.SelectSingleNode("LCODE").InnerText.Trim() <> "" Then
                    intLcode = SearchDoc.DocumentElement.SelectSingleNode("LCODE").InnerText.Trim()
                End If
            End If
            Dim strSTYPE As String = ""
            If SearchDoc.DocumentElement.SelectSingleNode("STYPE") IsNot Nothing Then
                If SearchDoc.DocumentElement.SelectSingleNode("STYPE").InnerText.Trim() <> "" Then
                    strSTYPE = SearchDoc.DocumentElement.SelectSingleNode("STYPE").InnerText.Trim()
                End If
            End If


            Try

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SER_TA_AGENCY_SEARCH"
                    .Connection = objSqlConnection
                    .Parameters.Add("@SIGNINID", SqlDbType.VarChar, 40)
                    .Parameters.Add("@STAFFNAME", SqlDbType.VarChar, 40)
                    .Parameters.Add("@AGENCYNAME", SqlDbType.VarChar, 50)
                    .Parameters.Add("@OFFICEID", SqlDbType.VarChar, 50)
                    If strSigninID = "" Then
                        .Parameters("@SIGNINID").Value = DBNull.Value
                    Else
                        .Parameters("@SIGNINID").Value = strSigninID
                    End If
                    If SearchDoc.DocumentElement.SelectSingleNode("STAFFNAME").InnerText = "" Then
                        .Parameters("@STAFFNAME").Value = DBNull.Value
                    Else
                        .Parameters("@STAFFNAME").Value = SearchDoc.DocumentElement.SelectSingleNode("STAFFNAME").InnerText
                    End If

                    If SearchDoc.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = "" Then
                        .Parameters("@AGENCYNAME").Value = DBNull.Value
                    Else
                        .Parameters("@AGENCYNAME").Value = SearchDoc.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText
                    End If

                    If SearchDoc.DocumentElement.SelectSingleNode("OFFICEID").InnerText = "" Then
                        .Parameters("@OFFICEID").Value = DBNull.Value
                    Else
                        .Parameters("@OFFICEID").Value = SearchDoc.DocumentElement.SelectSingleNode("OFFICEID").InnerText
                    End If

                    .Parameters.Add("@RESP_1A", SqlDbType.Int)
                    If intRESP_1A = 0 Then
                        .Parameters("@RESP_1A").Value = DBNull.Value
                    Else
                        .Parameters("@RESP_1A").Value = intRESP_1A
                    End If

                    .Parameters.Add("@LIMITED_TO_AOFFICE", SqlDbType.Char, 3)
                    .Parameters("@LIMITED_TO_AOFFICE").Value = strLimitedToAoffice

                    .Parameters.Add("@LIMITED_TO_REGION", SqlDbType.Int)
                    If intLimitedToRegion = 0 Then
                        .Parameters("@LIMITED_TO_REGION").Value = DBNull.Value
                    Else
                        .Parameters("@LIMITED_TO_REGION").Value = intLimitedToRegion
                    End If

                    .Parameters.Add("@LIMITED_TO_OWNAAGENCY", SqlDbType.Int) 'Location_code
                    If intLimitedToOwnAgency = 0 Then
                        .Parameters("@LIMITED_TO_OWNAAGENCY").Value = DBNull.Value
                    Else
                        .Parameters("@LIMITED_TO_OWNAAGENCY").Value = intLimitedToOwnAgency
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

                    .Parameters.Add("@EMPLOYEEID", SqlDbType.Int)
                    If strEmployeeID = "" Then
                        .Parameters("@EMPLOYEEID").Value = DBNull.Value
                    Else
                        .Parameters("@EMPLOYEEID").Value = strEmployeeID
                    End If

                    .Parameters.Add("@SOURCE", SqlDbType.VarChar, 30)
                    If strSource = "" Then
                        .Parameters("@SOURCE").Value = DBNull.Value
                    Else
                        .Parameters("@SOURCE").Value = strSource
                    End If

                    .Parameters.Add("@LCODE", SqlDbType.Int)
                    If intLcode = 0 Then
                        .Parameters("@LCODE").Value = DBNull.Value
                    Else
                        .Parameters("@LCODE").Value = intLcode
                    End If

                    .Parameters.Add("@STYPE", SqlDbType.VarChar, 50)
                    If strSTYPE = "" Then
                        .Parameters("@STYPE").Value = DBNull.Value
                    Else
                        .Parameters("@STYPE").Value = strSTYPE
                    End If
                End With

                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'objSqlDA.SelectCommand = objSqlCommand
                'objDS = New DataSet
                'objSqlDA.Fill(objDS)


                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("STAFF")
                objAptNodeClone = objAptNode.CloneNode(True)
                If objSqlReader.HasRows = True Then objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("AGENCYSTAFFID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AGENCYSTAFFID")) & "")
                    objAptNodeClone.Attributes("LCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")) & "")
                    objAptNodeClone.Attributes("AGENCYNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NAME")) & "")
                    objAptNodeClone.Attributes("ADDRESS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ADDRESS")) & "")
                    objAptNodeClone.Attributes("ADDRESS1").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ADDRESS1")) & "")
                    objAptNodeClone.Attributes("CITY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITY")) & "")
                    objAptNodeClone.Attributes("COUNTRY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("COUNTRY")) & "")
                    objAptNodeClone.Attributes("PHONE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PHONE")) & "")
                    objAptNodeClone.Attributes("FAX").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FAX")) & "")
                    objAptNodeClone.Attributes("DESIGNATION").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DESIGNATION")) & "")

                    objAptNodeClone.Attributes("SIGNINID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SIGNINID")) & "")
                    objAptNodeClone.Attributes("STAFFNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("STAFFNAME")) & "")
                    objAptNodeClone.Attributes("DESIGNATION").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DESIGNATION")) & "")
                    objAptNodeClone.Attributes("EMAIL").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMAIL")) & "")
                    objAptNodeClone.Attributes("MOBILENO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MOBILENO")) & "")
                    objAptNodeClone.Attributes("MARTIALSTATUS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MARTIALSTATUS")) & "")

                    objAptNodeClone.Attributes("FIRSTNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FIRSTNAME")) & "")
                    objAptNodeClone.Attributes("MIDDLENAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MIDDLENAME")) & "")
                    objAptNodeClone.Attributes("SURNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SURNAME")) & "")

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop
                objSqlReader.Close()
                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").InnerText = objSqlCommand.Parameters("@TOTALROWS").Value
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
            'Purpose:This function Inserts/Updates Agency Staff Details.
            'Input  :
            '<MS_UPDATEAGENCYSTAFFDETAILS_INPUT>
            '<AGENCYSTAFF   AGENCYSTAFFID='' LCODE='' STAFFNAME='' DESIGNATION='' DOW='' DOB='' CORRESPONDENCE=''
            '               RESPONSIBLE='' FAX='' PHONE='' EMAIL='' NOTES='' CONTACT_PERSON=''/>
            '</MS_UPDATEAGENCYSTAFFDETAILS_INPUT>

            'Output :
            '<MS_UPDATEAGENCYSTAFFDETAILS_OUTPUT>
            '	<AGENCYSTAFF AGENCYSTAFFID='' LCODE='' STAFFNAME='' DESIGNATION='' DOW='' DOB='' CORRESPONDENCE=''
            '		RESPONSIBLE='' FAX='' PHONE='' EMAIL='' NOTES='' CONTACT_PERSON=''/>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_UPDATEAGENCYSTAFFDETAILS_OUTPUT>
            '************************************************************************

            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objUpdateDocOutput As New XmlDocument
            Dim intRecordsAffected As Int32
            Dim objNode As XmlNode
            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)

                objNode = objUpdateDocOutput.DocumentElement.SelectSingleNode("AGENCYSTAFF")
                objUpdateDocOutput.DocumentElement.RemoveChild(objNode)
                objUpdateDocOutput.DocumentElement.AppendChild(objUpdateDocOutput.ImportNode(UpdateDoc.DocumentElement.SelectSingleNode("AGENCYSTAFF"), True))

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("AGENCYSTAFF")
                    If ((.Attributes("AGENCYSTAFFID").InnerText).Trim) = "" Then
                        strAction = "I"
                    Else
                        strAction = "U"
                    End If
                    If (.Attributes("SOURCE").InnerText).Trim.ToUpper <> "ORDER" Then
                        If (.Attributes("SIGNINID").InnerText).Trim = "" Then Throw (New AAMSException("Signin ID can't be blank."))
                    End If
                    If (.Attributes("FIRSTNAME").InnerText).Trim = "" Then Throw (New AAMSException("FistName can't be blank."))
                    If (.Attributes("SURNAME").InnerText).Trim = "" Then Throw (New AAMSException("Surnamne can't be blank."))
                    If (.Attributes("DESIGNATION").InnerText).Trim = "" Then Throw (New AAMSException("Designation can't be blank."))
                    If (.Attributes("EMAIL").InnerText).Trim = "" Then Throw (New AAMSException("EmailID can't be blank."))
                    If (.Attributes("MOBILENO").InnerText).Trim = "" Then Throw (New AAMSException("Mobile Number can't be blank."))
                    If (.Attributes("DOB").InnerText).Trim = "" Then Throw (New AAMSException("Date of Birth can't be blank."))
                    If (.Attributes("MARTIALSTATUS").InnerText).Trim <> "" Then
                        If (.Attributes("DOW").InnerText).Trim = "" Then Throw (New AAMSException("Date of Wedding can't be blank."))
                    End If
                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_AGENCYSTAFFDETAILS"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters.Add(New SqlParameter("@AGENCYSTAFFID", SqlDbType.BigInt))
                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@STAFFNAME", SqlDbType.VarChar, 40))
                    .Parameters.Add(New SqlParameter("@DESIGNATION", SqlDbType.VarChar, 40))
                    .Parameters.Add(New SqlParameter("@DOW", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@DOB", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@CORRESPONDENCE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@RESPONSIBLE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@FAX", SqlDbType.VarChar, 30))
                    .Parameters.Add(New SqlParameter("@PHONE", SqlDbType.VarChar, 30))
                    .Parameters.Add(New SqlParameter("@EMAIL", SqlDbType.VarChar, 100))
                    .Parameters.Add(New SqlParameter("@CONTACTPERSON", SqlDbType.Bit))
                    .Parameters.Add(New SqlParameter("@NOTES", SqlDbType.VarChar, 300))
                    ''Added
                    .Parameters.Add(New SqlParameter("@SIGNINID", SqlDbType.VarChar, 10))
                    .Parameters.Add(New SqlParameter("@TITLE", SqlDbType.VarChar, 3))
                    .Parameters.Add(New SqlParameter("@FIRSTNAME", SqlDbType.VarChar, 50))
                    .Parameters.Add(New SqlParameter("@MIDDLENAME", SqlDbType.VarChar, 50))
                    .Parameters.Add(New SqlParameter("@SURNAME", SqlDbType.VarChar, 50))
                    .Parameters.Add(New SqlParameter("@MOBILENO", SqlDbType.VarChar, 10))
                    .Parameters.Add(New SqlParameter("@MARTIALSTATUS", SqlDbType.VarChar, 3))

                    ''Added
                    .Parameters.Add(New SqlParameter("@RETUNID", SqlDbType.Int))
                    .Parameters("@RETUNID").Direction = ParameterDirection.Output
                    .Parameters("@RETUNID").Value = 0

                End With

                With UpdateDoc.DocumentElement.SelectSingleNode("AGENCYSTAFF")
                    objSqlCommand.Parameters("@ACTION").Value = strAction

                    If (.Attributes("AGENCYSTAFFID").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@AGENCYSTAFFID").Value = .Attributes("AGENCYSTAFFID").InnerText
                    End If

                    If (.Attributes("LCODE").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@LCODE").Value = CInt(.Attributes("LCODE").InnerText)
                    End If
                    
                    If (.Attributes("STAFFNAME").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@STAFFNAME").Value = .Attributes("STAFFNAME").InnerText
                    End If

                    If (.Attributes("DOW").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@DOW").Value = CInt(.Attributes("DOW").InnerText)
                    End If

                    If (.Attributes("DOB").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@DOB").Value = CInt(.Attributes("DOB").InnerText)
                    End If

                    If (.Attributes("CORRESPONDENCE").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@CORRESPONDENCE").Value = CInt(.Attributes("CORRESPONDENCE").InnerText)
                    End If

                    If (.Attributes("DESIGNATION").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@DESIGNATION").Value = .Attributes("DESIGNATION").InnerText
                    End If

                    If (.Attributes("RESPONSIBLE").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@RESPONSIBLE").Value = CInt(.Attributes("RESPONSIBLE").InnerText)
                    End If

                    If (.Attributes("FAX").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@FAX").Value = .Attributes("FAX").InnerText
                    End If

                    If (.Attributes("PHONE").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@PHONE").Value = .Attributes("PHONE").InnerText
                    End If

                    If (.Attributes("EMAIL").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@EMAIL").Value = .Attributes("EMAIL").InnerText
                    End If

                    If (.Attributes("NOTES").InnerText).Trim <> "" Then
                        objSqlCommand.Parameters("@NOTES").Value = .Attributes("NOTES").InnerText
                    End If

                    If (.Attributes("CONTACT_PERSON")) IsNot Nothing Then
                        If (.Attributes("CONTACT_PERSON").InnerText).Trim.ToUpper = "TRUE" Then
                            objSqlCommand.Parameters("@CONTACTPERSON").Value = 1
                        Else
                            objSqlCommand.Parameters("@CONTACTPERSON").Value = DBNull.Value
                        End If
                    End If


                    ''Added
                    If (.Attributes("SIGNINID")) IsNot Nothing Then
                        If (.Attributes("SIGNINID").InnerText).Trim <> "" Then
                            objSqlCommand.Parameters("@SIGNINID").Value = .Attributes("SIGNINID").InnerText.Trim
                        Else
                            objSqlCommand.Parameters("@SIGNINID").Value = DBNull.Value
                        End If
                    End If

                    If (.Attributes("TITLE")) IsNot Nothing Then
                        If (.Attributes("TITLE").InnerText).Trim <> "" Then
                            objSqlCommand.Parameters("@TITLE").Value = .Attributes("TITLE").InnerText.Trim
                        Else
                            objSqlCommand.Parameters("@TITLE").Value = DBNull.Value
                        End If
                    End If

                    If (.Attributes("FIRSTNAME")) IsNot Nothing Then
                        If (.Attributes("FIRSTNAME").InnerText).Trim <> "" Then
                            objSqlCommand.Parameters("@FIRSTNAME").Value = .Attributes("FIRSTNAME").InnerText.Trim
                        Else
                            objSqlCommand.Parameters("@FIRSTNAME").Value = DBNull.Value
                        End If
                    End If


                    If (.Attributes("MIDDLENAME")) IsNot Nothing Then
                        If (.Attributes("MIDDLENAME").InnerText).Trim <> "" Then
                            objSqlCommand.Parameters("@MIDDLENAME").Value = .Attributes("MIDDLENAME").InnerText.Trim
                        Else
                            objSqlCommand.Parameters("@MIDDLENAME").Value = DBNull.Value
                        End If
                    End If

                    If (.Attributes("SURNAME")) IsNot Nothing Then
                        If (.Attributes("SURNAME").InnerText).Trim <> "" Then
                            objSqlCommand.Parameters("@SURNAME").Value = .Attributes("SURNAME").InnerText.Trim
                        Else
                            objSqlCommand.Parameters("@SURNAME").Value = DBNull.Value
                        End If
                    End If

                    If (.Attributes("MOBILENO")) IsNot Nothing Then
                        If (.Attributes("MOBILENO").InnerText).Trim <> "" Then
                            objSqlCommand.Parameters("@MOBILENO").Value = .Attributes("MOBILENO").InnerText.Trim
                        Else
                            objSqlCommand.Parameters("@MOBILENO").Value = DBNull.Value
                        End If
                    End If

                    If (.Attributes("MARTIALSTATUS")) IsNot Nothing Then
                        If (.Attributes("MARTIALSTATUS").InnerText).Trim <> "" Then
                            objSqlCommand.Parameters("@MARTIALSTATUS").Value = .Attributes("MARTIALSTATUS").InnerText.Trim
                        Else
                            objSqlCommand.Parameters("@MARTIALSTATUS").Value = DBNull.Value
                        End If
                    End If
                End With

                objSqlCommand.Connection.Open()
                intRecordsAffected = objSqlCommand.ExecuteNonQuery()
                intRetId = objSqlCommand.Parameters("@RETUNID").Value

                If UCase(strAction) = "I" Then
                    intRetId = objSqlCommand.Parameters("@RETUNID").Value

                    If intRetId = -1 Then
                        Throw (New AAMSException("Agency Staff Already Exists!"))
                    Else
                        objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                        With objUpdateDocOutput.DocumentElement.SelectSingleNode("AGENCYSTAFF")
                            .Attributes("AGENCYSTAFFID").InnerText = intRetId
                        End With
                    End If
                ElseIf UCase(strAction) = "U" Then
                    intRetId = objSqlCommand.Parameters("@RETUNID").Value
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
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                Return objUpdateDocOutput
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objUpdateDocOutput

        End Function


        Public Function GetAgencyStaffContactPersonDetail(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the Agency Staff List, based on the given field value
            'Input  : 
            '<UP_GETAGENCYSTAFFDETAILS_INPUT>
            '<LCODE></LCODE>
            '</UP_GETAGENCYSTAFFDETAILS_INPUT>

            'Output :  
            '<UP_GETAGENCYSTAFFDETAILS_OUTPUT>
            '<AGENCYSTAFF LCODE='' AGENCYSTAFFID='' STAFFNAME='' DESIGNATION='' EMAILID='' />
            '<Errors Status=''>
            '<Error Code='' Description='' />
            '</Errors>
            '</UP_GETAGENCYSTAFFDETAILS_OUTPUT>
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strLCODE As String
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "GetAgencyStaffContactPersonDetails"

            objOutputXml.LoadXml(srtGETAGENCYSTAFFDETAILS_OUTPUT)

            Try

                If SearchDoc.DocumentElement.SelectSingleNode("LCODE").InnerText <> "" Then
                    strLCODE = SearchDoc.DocumentElement.SelectSingleNode("LCODE").InnerText
                Else
                    strLCODE = ""
                End If
                If strLCODE = "" Then
                    Throw (New AAMSException("Agency Location Code can't be blank."))
                End If
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_GET_TA_AGENCYSTAFFCONTACTPERSON_DETAILS]"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    .Parameters("@LCODE").Value = strLCODE

                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF")
                If objSqlReader.HasRows Then
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.RemoveChild(objAptNode)
                End If

                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("LCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")))
                    objAptNodeClone.Attributes("AGENCYSTAFFID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AGENCYSTAFFID")))
                    objAptNodeClone.Attributes("STAFFNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("STAFFNAME")))
                    objAptNodeClone.Attributes("DESIGNATION").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DESIGNATION")))
                    objAptNodeClone.Attributes("EMAILID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMAILID")))

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
            'Purpose:This function gives details of Agency Staff Details.
            'Input  :
            '<MS_VIEWAGENCYSTAFF_INPUT>
            '<AGENCYSTAFFID></AGENCYSTAFFID>
            '</MS_VIEWAGENCYSTAFF_INPUT>

            'Output :
            '<MS_VIEWAGENCYSTAFF_OUTPUT>
            '<AGENCYSTAFF AGENCYSTAFFID='' LCODE='' STAFFNAME='' DESIGNATION='' DOW='' DOB='' CORRESPONDENCE='' RESPONSIBLE='' FAX = '' PHONE='' EMAIL='' NOTES='' />
            '   <Errors Status=''>
            '       <Error Code='' Description='' />
            '   </Errors>
            '</MS_VIEWAGENCYSTAFF_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strAgencyStaff_ID As String
            Const strMETHOD_NAME As String = "View"
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(strVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument 
                strAgencyStaff_ID = IndexDoc.DocumentElement.SelectSingleNode("AGENCYSTAFFID").InnerText.Trim
                If strAgencyStaff_ID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_AGENCYSTAFFDETAILS"
                    .Connection = objSqlConnection

                    .Parameters.Add("@AGENCYSTAFFID", SqlDbType.Int)
                    .Parameters("@AGENCYSTAFFID").Value = strAgencyStaff_ID

                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF")
                        .Attributes("AGENCYSTAFFID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AGENCYSTAFFID")) & "")
                        .Attributes("LCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")) & "")
                        .Attributes("OFFICEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OFFICEID")) & "")
                        .Attributes("STAFFNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("STAFFNAME")) & "")
                        .Attributes("DESIGNATION").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DESIGNATION")) & "")
                        .Attributes("DOW").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DOW")) & "")
                        .Attributes("DOB").InnerText = Replace(Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DOB")) & ""), "/1900", "")
                        .Attributes("CORRESPONDENCE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CORRESPONDENCE")) & "")
                        .Attributes("RESPONSIBLE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("RESPONSIBLE")) & "")
                        .Attributes("PHONE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PHONE")) & "")
                        .Attributes("FAX").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FAX")) & "")
                        .Attributes("EMAIL").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMAIL")) & "")
                        .Attributes("NOTES").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NOTES")) & "")
                        .Attributes("CONTACT_PERSON").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CONTACT_PERSON")) & "")

                        .Attributes("SIGNINID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SIGNINID")) & "")
                        .Attributes("TITLE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("TITLE")) & "")
                        .Attributes("FIRSTNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FIRSTNAME")) & "")
                        .Attributes("MIDDLENAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MIDDLENAME")) & "")
                        .Attributes("SURNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SURNAME")) & "")
                        .Attributes("MOBILENO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MOBILENO")) & "")
                        .Attributes("MARTIALSTATUS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MARTIALSTATUS")) & "")



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

        Public Function GetCallerName(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument

            '***********************************************************************
            'Purpose: To list out the Agency Staff List, based on the given field value
            'Input  : 
            '<UP_GETAGENCYSTAFFDETAILS_INPUT>
            '   <LCODE></LCODE>
            '   <OFFICEID></OFFICEID>
            '   <EMPLOYEEID></EMPLOYEEID>
            '</UP_GETAGENCYSTAFFDETAILS_INPUT>

            'Output :  
            '<UP_GETAGENCYSTAFFDETAILS_OUTPUT>
            '<AGENCYSTAFF STAFFNAME=''  />
            '<Errors Status=''>
            '<Error Code='' Description='' />
            '</Errors>
            '</UP_GETAGENCYSTAFFDETAILS_OUTPUT>
            '************************************************************************
            Const strGETAGENCYSTAFFDETAILS_OUTPUT = "<UP_GETAGENCYSTAFFDETAILS_OUTPUT><AS SN='' /><Errors Status=''><Error Code='' Description='' /></Errors></UP_GETAGENCYSTAFFDETAILS_OUTPUT>"
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strLCODE As String = "", strOfficeID As String = "", strEmployeeID As String = ""
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "GetDetails"

            objOutputXml.LoadXml(strGETAGENCYSTAFFDETAILS_OUTPUT)

            Try

                If SearchDoc.DocumentElement.SelectSingleNode("LCODE").InnerText <> "" Then
                    strLCODE = SearchDoc.DocumentElement.SelectSingleNode("LCODE").InnerText
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("OFFICEID").InnerText <> "" Then
                    strOfficeID = SearchDoc.DocumentElement.SelectSingleNode("OFFICEID").InnerText
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText <> "" Then
                    strEmployeeID = SearchDoc.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText
                End If
                If strLCODE = "" AndAlso strOfficeID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                If strEmployeeID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_GET_TA_AGENCYCALLER]"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    If strLCODE = "" Then
                        .Parameters("@LCODE").Value = DBNull.Value
                    Else
                        .Parameters("@LCODE").Value = strLCODE
                    End If


                    .Parameters.Add(New SqlParameter("@OFFICEID", SqlDbType.VarChar, 9))
                    If strOfficeID = "" Then
                        .Parameters("@OFFICEID").Value = DBNull.Value
                    Else
                        .Parameters("@OFFICEID").Value = strOfficeID
                    End If


                    .Parameters.Add(New SqlParameter("@EMPLOYEEID", SqlDbType.Int))
                    .Parameters("@EMPLOYEEID").Value = strEmployeeID

                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AS")
                If objSqlReader.HasRows Then
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.RemoveChild(objAptNode)
                End If

                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("SN").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("STAFFNAME")))
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
