'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Neeraj $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizMaster/bzAgencyGroup.vb $
'$Workfile: bzAgencyGroup.vb $
'$Revision: 47 $
'$Archive: /AAMS/Components/bizMaster/bzAgencyGroup.vb $
'$Modtime: 9/22/11 3:51p $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizMaster

    Public Class bzAgencyGroup
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzAgencyGroup"

        Const gstrList_OUTPUT = "<MS_LISTGROUPTYPE_OUTPUT><GROUP_TYPE GroupTypeID='' GroupTypeName='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTGROUPTYPE_OUTPUT>"
        Const gstrListGroupClassification_OUTPUT = "<MS_LISTGROUPCLASSIFICATIONTYPE_OUTPUT><GROUPCLASSIFICATION_TYPE GroupClassificationID='' Group_Classification_Name='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTGROUPCLASSIFICATIONTYPE_OUTPUT>"

        Const pstrList_OUTPUT = "<MS_LISTPRIORITY_OUTPUT><PRIORITY PriorityID='' PriorityName='' StatusType=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTPRIORITY_OUTPUT>"
        Const strVIEW_OUTPUT = "<MS_VIEWGROUP_OUTPUT><GROUP Chain_Code='' Chain_Name='' ContactPerson='' Affiliation='' Priority_Auto_Update='' Grouped='' PK_Bkngs='' PriorityID='' GroupTypeID='' Aoffice='' CityID='' Responsible_Location='' CQualifier='' CCode='' MainGroup='' Address='' GroupClassificationID=''  Group_Classification_Name='' AccountManager='' AccountManagerName='' Region='' PANNO='' GROUP_OFFICEID ='' COMP_VERTICAL=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWGROUP_OUTPUT>"

        Const strSEARCH_OUTPUT = "<MS_SEARCHGROUP_OUTPUT><GROUP Chain_Code='' Chain_Name='' City_Name='' GroupTypeName='' Aoffice='' ChainSelected='' Main='' Group_Classification_Name='' PANNO='' SecurityRegionIds='' COMP_VERTICAL=''/><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHGROUP_OUTPUT>"

        Const strADDGROUP_OUTPUT = "<MS_UPDATEGROUP_INPUT><GROUP ACTION='' Chain_Code='' Chain_Name='' ContactPerson='' Affiliation='' Priority_Auto_Update='' Grouped='' PK_Bkngs='' PriorityID='' GroupTypeID='' Aoffice='' City_Name='' Responsible_Location='' CQualifier='' CCode=''  MainGroup='' Address='' /></MS_UPDATEGROUP_INPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETEGROUP_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETEGROUP_OUTPUT>"
        Const strUPDATE_OUTPUT = "<MS_UPDATEGROUP_OUTPUT><GROUP ACTION='' Chain_Code='' Chain_Name='' ContactPerson='' Affiliation='' Priority_Auto_Update='' Grouped='' PK_Bkngs='' PriorityID='' GroupTypeID='' Aoffice='' CityID='' Responsible_Location='' CQualifier='' CCode='' MainGroup='' Address='' EmployeeId='' GroupClassificationID=''  AccountManager='' PANNO='' GROUP_OFFICEID ='' COMP_VERTICAL=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEGROUP_OUTPUT>"
        Const strListAgencyGroup = "<MS_AGENCYGROUPLIST_OUTPUT><AGENCYGROUP ADDRESS='' CHAIN_CODE='' AGENCYGROUPNAME = ''></AGENCYGROUP> <Errors Status=''><Error Code='' Description='' /></Errors></MS_AGENCYGROUPLIST_OUTPUT>"

        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add
            'Declare Output XML String
            '***********************************************************************
            'Purpose: 
            'Input:None
            'XML
            '<MS_UPDATEGROUP_INPUT>
            '   <GROUP ACTION='' Chain_Code='' Chain_Name='' ContactPerson='' Affiliation='' Priority_Auto_Update='' 
            '   Grouped='' PK_Bkngs='' PriorityID='' GroupTypeID='' Aoffice='' City_Name='' Responsible_Location='' 
            '  CQualifier='' CCode='' />
            ' </MS_UPDATEGROUP_INPUT>
            '**************************************************************************

            Dim strMETHOD_NAME As String = "Add"
            Dim objXMLDoc As New XmlDocument
            Try
                objXMLDoc.LoadXml(strADDGROUP_OUTPUT)
            Catch Exec As Exception
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objXMLDoc, "101", Exec.Message)
                Return objXMLDoc
            End Try
            Return objXMLDoc
        End Function
        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Delete
            '***********************************************************************
            'Purpose:This function deletes a group.
            'Input:XmlDocument
            '        <MS_DELETEGROUP_INPUT>
            '	<Chain_Code></Chain_Code>
            '</MS_DELETEGROUP_INPUT>

            'Output :
            '<MS_DELETEGROUP_OUTPUT>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_DELETEGROUP_OUTPUT>
            '************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objTransaction As SqlTransaction
            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Delete"
            Dim intChaincode As Integer

            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0

                intChaincode = DeleteDoc.DocumentElement.SelectSingleNode("Chain_Code").InnerText.Trim
                If intChaincode = 0 Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_GROUP"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"
                    .Parameters.Add(New SqlParameter("@CHAINCODE", SqlDbType.Int))
                    .Parameters("@CHAINCODE").Value = intChaincode
                    objSqlCommand.Connection.Open()
                    objTransaction = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)
                    .Transaction = objTransaction
                    intRecordsAffected = .ExecuteNonQuery()
                    objTransaction.Commit()
                    .Connection.Close()
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
                If objSqlConnection.State <> ConnectionState.Closed Then
                    If Not objTransaction Is Nothing Then
                        objTransaction.Rollback()
                    End If
                    objSqlConnection.Close()
                End If
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
            ' <MS_SEARCHGROUP_INPUT>
            '   	<Chain_Code></Chain_Code>
            '	    <Chain_Name></Chain_Name>
            '	    <City_Name></City_Name>
            '	    <GroupTypeID></GroupTypeID>
            '	    <Aoffice></Aoffice>
            '       <MainGroup></MainGroup>
            '       <EmployeeID></EmployeeID>
            '       <SECURITYREGIONID/>
            '       <PAGE_NO></PAGE_NO>
            '       <PAGE_SIZE></PAGE_SIZE>
            '       <SORT_BY></SORT_BY>
            '     <DESC></DESC>
            '</MS_SEARCHGROUP_INPUT>

            'Output :
            '<MS_SEARCHGROUP_OUTPUT>
            '	<GROUP Chain_Code='' Chain_Name='' City_Name='' GroupTypeName='' Aoffice='' PANNO=''/>
            '   <PAGE PAGE_COUNT='' TOTAL_ROWS=''/>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_SEARCHGROUP_OUTPUT>
            ' ************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim intChaincode As Integer, strGroupname As String, strAoffice As String
            Dim intGroupTypeID As Integer, strCityName As String
            Dim strSecRegionId As String = String.Empty
            Dim strEmployeeID As String = ""
            Dim blnMainGroup As Boolean

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)
            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start

                If Not SearchDoc.DocumentElement.SelectSingleNode("EmployeeID") Is Nothing Then
                    strEmployeeID = SearchDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText
                End If
                If Not SearchDoc.DocumentElement.SelectSingleNode("SECURITYREGIONID") Is Nothing Then
                    strSecRegionId = SearchDoc.DocumentElement.SelectSingleNode("SECURITYREGIONID").InnerText
                End If
                If (SearchDoc.DocumentElement.SelectSingleNode("Chain_Code").InnerText.Trim() = "") Then
                    intChaincode = 0
                Else
                    intChaincode = (SearchDoc.DocumentElement.SelectSingleNode("Chain_Code").InnerText.Trim())
                End If

                If (SearchDoc.DocumentElement.SelectSingleNode("Chain_Name").InnerText.Trim() = "") Then
                    strGroupname = vbNullString
                Else
                    strGroupname = (SearchDoc.DocumentElement.SelectSingleNode("Chain_Name").InnerText.Trim())
                End If

                If (SearchDoc.DocumentElement.SelectSingleNode("City_Name").InnerText.Trim() = "") Then
                    strCityName = vbNullString
                Else
                    strCityName = (SearchDoc.DocumentElement.SelectSingleNode("City_Name").InnerText.Trim())
                End If

                If (SearchDoc.DocumentElement.SelectSingleNode("GroupTypeID").InnerText.Trim() = "") Then
                    intGroupTypeID = 0
                Else
                    intGroupTypeID = (SearchDoc.DocumentElement.SelectSingleNode("GroupTypeID").InnerText.Trim())
                End If

                If (SearchDoc.DocumentElement.SelectSingleNode("Aoffice").InnerText.Trim() = "") Then
                    strAoffice = vbNullString
                Else
                    strAoffice = (SearchDoc.DocumentElement.SelectSingleNode("Aoffice").InnerText.Trim())
                End If

                If (SearchDoc.DocumentElement.SelectSingleNode("MainGroup").InnerText.Trim() = "True") Then
                    blnMainGroup = True
                End If


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
                    .CommandText = "UP_SER_MS_GROUP"
                    .Connection = objSqlConnection

                    .Parameters.Add("@CHAINCODE", SqlDbType.Int)
                    If (intChaincode = 0) Then
                        .Parameters("@CHAINCODE").Value = DBNull.Value
                    Else
                        .Parameters("@CHAINCODE").Value = intChaincode
                    End If

                    .Parameters.Add("@GROUPNAME", SqlDbType.VarChar, 200)
                    .Parameters("@GROUPNAME").Value = strGroupname

                    .Parameters.Add("@CITY", SqlDbType.VarChar, 200)
                    .Parameters("@CITY").Value = strCityName

                    .Parameters.Add("@GROUPTYPEID", SqlDbType.Int)
                    If intGroupTypeID = 0 Then
                        .Parameters("@GROUPTYPEID").Value = DBNull.Value
                    Else
                        .Parameters("@GROUPTYPEID").Value = intGroupTypeID
                    End If

                    .Parameters.Add("@AOFFICE", SqlDbType.VarChar, 3)
                    .Parameters("@AOFFICE").Value = strAoffice

                    .Parameters.Add(New SqlParameter("@MainGroup", SqlDbType.Bit))
                    If blnMainGroup = True Then
                        .Parameters("@MainGroup").Value = 1
                    Else
                        .Parameters("@MainGroup").Value = DBNull.Value
                    End If

                    .Parameters.Add(New SqlParameter("@EMPLOYEEID", SqlDbType.Int))
                    If strEmployeeID <> "" Then
                        .Parameters("@EMPLOYEEID").Value = CInt(strEmployeeID)
                    Else
                        .Parameters("@EMPLOYEEID").Value = DBNull.Value
                    End If

                    .Parameters.Add(New SqlParameter("@SECURITYREGIONID", SqlDbType.Int))
                    If strSecRegionId <> "" Then
                        .Parameters("@SECURITYREGIONID").Value = Val(strSecRegionId & "")
                    Else
                        .Parameters("@SECURITYREGIONID").Value = DBNull.Value
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
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("GROUP")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("Chain_Code").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Chain_Code")) & "")
                    objAptNodeClone.Attributes("Chain_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Chain_Name")) & "")
                    objAptNodeClone.Attributes("City_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITY_NAME")) & "")
                    objAptNodeClone.Attributes("GroupTypeName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AGENCYTYPE")) & "")
                    objAptNodeClone.Attributes("Aoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AOFFICE")) & "")
                    objAptNodeClone.Attributes("Main").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MAIN")) & "")
                    objAptNodeClone.Attributes("SecurityRegionIds").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SECURITYREGIONIDS")) & "")

                    If Val(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHAINSELECTED")) & "") <> 0 Then
                        objAptNodeClone.Attributes("ChainSelected").InnerText = "True"
                    Else
                        objAptNodeClone.Attributes("ChainSelected").InnerText = "False"
                    End If
                    objAptNodeClone.Attributes("Group_Classification_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Group_Classification_Name")) & "")
                    objAptNodeClone.Attributes("PANNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PANNO")) & "")

                    Dim strCOMP_VERTICAL As String = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("COMP_VERTICAL")) & "")
                    objAptNodeClone.Attributes("COMP_VERTICAL").InnerText = IIf(strCOMP_VERTICAL = "1", "Amadeus", IIf(strCOMP_VERTICAL = "2", "ResBird", (IIf(strCOMP_VERTICAL = "3", "Non1A", ""))))



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
            'objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Return objOutputXml
        End Function
        Public Function Update(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Update
            '***********************************************************************
            'Purpose:This function Inserts/Updates Group.
            'Input  :
            '<MS_UPDATEGROUP_INPUT>
            '    <GROUP ACTION='' Chain_Code='' Chain_Name='' ContactPerson='' Affiliation='' Priority_Auto_Update='' 
            '       Grouped='' PK_Bkngs='' PriorityID='' GroupTypeID='' Aoffice='' CityID='' Responsible_Location='' 
            '       CQualifier='' CCode='' MainGroup='' Address='' EmployeeId='' GroupClassificationID='' AccountManager='' PAN='' GROUP_OFFICEID ='' />
            '   </MS_UPDATEGROUP_INPUT>

            'Output  :
            '<MS_UPDATEGROUP_OUTPUT>
            '   <GROUP ACTION='' Chain_Code='' Chain_Name='' ContactPerson='' Affiliation='' Priority_Auto_Update='' 
            '       Grouped='' PK_Bkngs='' PriorityID='' GroupTypeID='' Aoffice='' CityID='' Responsible_Location='' 
            '       CQualifier='' CCode='' MainGroup='' Address='' GroupClassificationID='' AccountManager='' PAN='' GROUP_OFFICEID =''/>
            '
            '       <Errors Status=''>
            '           <Error Code='' Description='' />
            '       </Errors>
            '</MS_UPDATEGROUP_OUTPUT>
            '************************************************************************

            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objTransaction As SqlTransaction
            Dim objSqlCommand As New SqlCommand
            Dim objUpdateDocOutput As New XmlDocument

            Dim intChaincode As Integer
            Dim strGroupname As String
            Dim intPriorityID As Integer
            Dim intGroupTypeID As Integer
            Dim strAoffice As String
            Dim intCityID As Integer
            Dim strCorp_code As String
            Dim strCorp_qualifier As String
            Dim blnMainGroup As Boolean
            Dim strAddress As String = vbNullString
            Dim intGroupClassificationID As Integer
            Dim intAccountManager As Integer


            ''These are the default values
            Dim strContactPerName As String = vbNullString
            Dim strAffiliation As String = "NA"
            Dim intPri_Auto_Update As Integer = 1
            Dim strPK_Bkngs As String = vbNullString
            Dim intGrouped As Integer = 0
            Dim intResponsible_Location As Integer = 0
            Dim strEmployeeId As String = ""
            Dim strPanNo As String = ""
            Dim StrGroupOfficeID As String = ""
            Dim strCOMP_VERTICAL As String = ""
            ''These are the default values

            Dim intRecordsAffected As Int32
            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)

                With objUpdateDocOutput.DocumentElement.SelectSingleNode("GROUP")
                    .Attributes("ACTION").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("ACTION").InnerText
                    .Attributes("Chain_Code").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("Chain_Code").InnerText
                    .Attributes("Chain_Name").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("Chain_Name").InnerText
                    .Attributes("ContactPerson").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("ContactPerson").InnerText
                    .Attributes("Affiliation").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("Affiliation").InnerText
                    .Attributes("Priority_Auto_Update").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("Priority_Auto_Update").InnerText
                    .Attributes("Grouped").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("Grouped").InnerText
                    .Attributes("PK_Bkngs").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("PK_Bkngs").InnerText
                    .Attributes("PriorityID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("PriorityID").InnerText
                    .Attributes("GroupTypeID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("GroupTypeID").InnerText
                    .Attributes("Aoffice").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("Aoffice").InnerText
                    .Attributes("CityID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("CityID").InnerText
                    .Attributes("Responsible_Location").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("Responsible_Location").InnerText
                    .Attributes("EmployeeId").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("EmployeeId").InnerText

                    .Attributes("CCode").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("CCode").InnerText
                    .Attributes("CQualifier").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("CQualifier").InnerText

                    .Attributes("MainGroup").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("MainGroup").InnerText
                    .Attributes("Address").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("Address").InnerText
                    If UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("GroupClassificationID") IsNot Nothing Then
                        .Attributes("GroupClassificationID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("GroupClassificationID").InnerText
                    End If

                    If UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("AccountManager") IsNot Nothing Then
                        .Attributes("AccountManager").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("AccountManager").InnerText
                    End If
                    If UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("PANNO") IsNot Nothing Then
                        .Attributes("PANNO").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("PANNO").InnerText
                        strPanNo = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("PANNO").InnerText
                    End If
                    If UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("GROUP_OFFICEID") IsNot Nothing Then
                        .Attributes("GROUP_OFFICEID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("GROUP_OFFICEID").InnerText
                        StrGroupOfficeID = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("GROUP_OFFICEID").InnerText
                    End If

                    If UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("COMP_VERTICAL") IsNot Nothing Then
                        If UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("COMP_VERTICAL").InnerText <> "" Then
                            .Attributes("COMP_VERTICAL").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("COMP_VERTICAL").InnerText
                            strCOMP_VERTICAL = UpdateDoc.DocumentElement.SelectSingleNode("GROUP").Attributes("COMP_VERTICAL").InnerText
                        End If
                    End If

                End With


                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("GROUP")
                    strAction = ((.Attributes("ACTION").InnerText).Trim).ToString

                    If ((.Attributes("Chain_Code").InnerText).Trim = "") Then
                        intChaincode = 0
                    Else
                        intChaincode = ((.Attributes("Chain_Code").InnerText).Trim)
                    End If
                    strGroupname = ((.Attributes("Chain_Name").InnerText).Trim).ToString
                    If ((.Attributes("PriorityID").InnerText).Trim = "") Then
                        intPriorityID = 0
                    Else
                        intPriorityID = ((.Attributes("PriorityID").InnerText).Trim)
                    End If
                    If ((.Attributes("GroupTypeID").InnerText).Trim = "") Then
                        intGroupTypeID = 0
                    Else
                        intGroupTypeID = ((.Attributes("GroupTypeID").InnerText).Trim)
                    End If
                    strAoffice = ((.Attributes("Aoffice").InnerText).Trim).ToString

                    If ((.Attributes("CityID").InnerText).Trim = "") Then
                        intCityID = 0
                    Else
                        intCityID = ((.Attributes("CityID").InnerText).Trim).ToString
                    End If

                    strCorp_code = ((.Attributes("CCode").InnerText).Trim).ToString
                    strCorp_qualifier = ((.Attributes("CQualifier").InnerText).Trim).ToString

                    If ((.Attributes("MainGroup").InnerText).Trim = "True") Then
                        blnMainGroup = True
                    Else
                        blnMainGroup = False
                    End If

                    If ((.Attributes("Address").InnerText).Trim <> "") Then
                        strAddress = ((.Attributes("Address").InnerText).Trim).ToString
                    End If

                    If .Attributes("GroupClassificationID") IsNot Nothing Then
                        If ((.Attributes("GroupClassificationID").InnerText).Trim <> "") Then
                            intGroupClassificationID = .Attributes("GroupClassificationID").InnerText
                        End If
                    End If

                    If .Attributes("AccountManager") IsNot Nothing Then
                        If ((.Attributes("AccountManager").InnerText).Trim <> "") Then
                            intAccountManager = .Attributes("AccountManager").InnerText
                        End If
                    End If

                    If .Attributes("EmployeeId") IsNot Nothing Then
                        If ((.Attributes("EmployeeId").InnerText).Trim <> "") Then
                            strEmployeeId = .Attributes("EmployeeId").InnerText
                        End If
                    End If

                    If strAction = "I" Or strAction = "U" Then
                        'If strCOMP_VERTICAL = "" Then Throw (New AAMSException("Select Company Vertical."))
                        If strAction = "U" Then
                            If intChaincode = 0 Then
                                Throw (New AAMSException("Chain code can't be blank."))
                            ElseIf strGroupname = "" Then
                                Throw (New AAMSException("Group Name can't be blank."))
                            End If
                        End If
                    Else
                        Throw (New AAMSException("Invalid Action Code."))
                    End If

                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_GROUP"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction
                    .Parameters.Add(New SqlParameter("@CHAINCODE", SqlDbType.Int))

                    If strAction = "I" Then
                        If intChaincode = 0 Then
                            .Parameters("@CHAINCODE").Value = DBNull.Value
                        End If
                    Else
                        .Parameters("@CHAINCODE").Value = intChaincode
                    End If


                    .Parameters.Add(New SqlParameter("@GROUPNAME", SqlDbType.VarChar, 200))
                    .Parameters("@GROUPNAME").Value = strGroupname

                    .Parameters.Add(New SqlParameter("@AOFFICE", SqlDbType.VarChar, 3))
                    .Parameters("@AOFFICE").Value = strAoffice

                    .Parameters.Add(New SqlParameter("@CITYID", SqlDbType.Int))
                    .Parameters("@CITYID").Value = intCityID

                    .Parameters.Add(New SqlParameter("@PRIORITYID", SqlDbType.Int))
                    .Parameters("@PRIORITYID").Value = intPriorityID

                    .Parameters.Add(New SqlParameter("@CORP_CODE", SqlDbType.VarChar, 10))
                    .Parameters("@CORP_CODE").Value = strCorp_code


                    .Parameters.Add(New SqlParameter("@CORP_QUALIFIER", SqlDbType.VarChar, 10))
                    .Parameters("@CORP_QUALIFIER").Value = strCorp_qualifier

                    .Parameters.Add(New SqlParameter("@GROUPTYPEID", SqlDbType.Int))
                    If intGroupTypeID = 0 Then
                        .Parameters("@GROUPTYPEID").Value = DBNull.Value
                    Else
                        .Parameters("@GROUPTYPEID").Value = intGroupTypeID
                    End If

                    .Parameters.Add(New SqlParameter("@MainGroup", SqlDbType.Bit))
                    If blnMainGroup = True Then
                        .Parameters("@MainGroup").Value = 1
                    Else
                        .Parameters("@MainGroup").Value = 0
                    End If

                    .Parameters.Add(New SqlParameter("@ADDRESS", SqlDbType.VarChar, 100))
                    .Parameters("@ADDRESS").Value = strAddress


                    .Parameters.Add(New SqlParameter("@GroupClassificationID", SqlDbType.SmallInt))
                    If intGroupClassificationID = 0 Then
                        .Parameters("@GroupClassificationID").Value = DBNull.Value
                    Else
                        .Parameters("@GroupClassificationID").Value = intGroupClassificationID
                    End If


                    .Parameters.Add(New SqlParameter("@AccountManager", SqlDbType.Int))
                    If intAccountManager = 0 Then
                        .Parameters("@AccountManager").Value = DBNull.Value
                    Else
                        .Parameters("@AccountManager").Value = intAccountManager
                    End If

                    .Parameters.Add(New SqlParameter("@EmployeeId", SqlDbType.Int))
                    If strEmployeeId = "" Then
                        .Parameters("@EmployeeId").Value = DBNull.Value
                    Else
                        .Parameters("@EmployeeId").Value = CInt(strEmployeeId)
                    End If

                    .Parameters.Add(New SqlParameter("@PANNO", SqlDbType.VarChar, 25))
                    If strPanNo = "" Then
                        .Parameters("@PANNO").Value = DBNull.Value
                    Else
                        .Parameters("@PANNO").Value = strPanNo
                    End If

                    .Parameters.Add(New SqlParameter("@GROUP_OFFICEID", SqlDbType.VarChar, 9))
                    If StrGroupOfficeID = "" Then
                        .Parameters("@GROUP_OFFICEID").Value = DBNull.Value
                    Else
                        .Parameters("@GROUP_OFFICEID").Value = StrGroupOfficeID
                    End If


                    .Parameters.Add(New SqlParameter("@COMP_VERTICAL", SqlDbType.SmallInt))
                    If strCOMP_VERTICAL = "" Then
                        .Parameters("@COMP_VERTICAL").Value = DBNull.Value
                    Else
                        .Parameters("@COMP_VERTICAL").Value = CInt(strCOMP_VERTICAL)
                    End If


                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0

                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED

                    .Connection.Open()
                    objTransaction = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)
                    .Transaction = objTransaction
                    intRecordsAffected = .ExecuteNonQuery()
                    objTransaction.Commit()
                    .Connection.Close()
                    intRetId = .Parameters("@RETURNID").Value
                    If UCase(strAction) = "I" Then
                        If intRetId = 0 Then
                            Throw (New AAMSException("Unable to Insert!"))
                        End If
                        With objUpdateDocOutput.DocumentElement.SelectSingleNode("GROUP")
                            .Attributes("Chain_Code").InnerText = intRetId
                        End With
                    ElseIf UCase(strAction) = "U" Then
                        If intRetId = 0 Then
                            Throw (New AAMSException("Unable to update!"))
                        End If
                    End If
                End With
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                If intRetId = 0 Then
                    bzShared.FillErrorStatus(objUpdateDocOutput, "101", "Group already exists. Please enter another Group.")
                Else
                    bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                End If
                Return objUpdateDocOutput
            Catch Exec As Exception
                If objSqlConnection.State <> ConnectionState.Closed Then
                    If Not objTransaction Is Nothing Then
                        objTransaction.Rollback()
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

            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Return objUpdateDocOutput
        End Function
        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.View
            '***********************************************************************
            'Purpose:This function gives details of City.
            'Input  :
            '<MS_VIEWGROUP_INPUT>
            '	<Chain_Code></Chain_Code>
            '</MS_VIEWGROUP_INPUT>

            'Output :

            'Const strVIEW_OUTPUT = "<MS_VIEWGROUP_OUTPUT><GROUP Chain_Code='' Chain_Name='' ContactPerson='' Affiliation='' Priority_Auto_Update=''Grouped='' PK_Bkngs='' 
            'PriorityName='' GroupTypeName='' Aoffice='' 
            'City_Name='' Responsible_Location='' CQualifier='' CCode='' GroupOfficeID ='' />
            '<Errors Status=''><Error Code='' Description='' MainGroup='' Address='' /></Errors></MS_VIEWGROUP_OUTPUT>"

            '<MS_VIEWGROUP_OUTPUT>
            '	<GROUP Chain_Code='' Chain_Name='' ContactPerson='' Affiliation='' Priority_Auto_Update=''
            '		Grouped='' PK_Bkngs='' PriorityID='' GroupTypeID='' Aoffice='' Responsible_Location='' MainGroup='' Address='' GroupClassificationID=''  Group_Classification_Name='' AccountManager='' AccountManagerName='' Region='' PANNO='' GROUP_OFFICEID='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_VIEWGROUP_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim intChaincode As Integer
            Const strMETHOD_NAME As String = "View"

            Try
                objOutputXml.LoadXml(strVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                intChaincode = IndexDoc.DocumentElement.SelectSingleNode("Chain_Code").InnerText.Trim
                If intChaincode = 0 Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_GROUP"
                    .Connection = objSqlConnection
                    .Parameters.Add("@CHAINCODE", SqlDbType.Int)
                    .Parameters("@CHAINCODE").Value = intChaincode
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    Dim blnRecordFound As Boolean
                    If blnRecordFound = False Then
                        With objOutputXml.DocumentElement.SelectSingleNode("GROUP")
                            .Attributes("Chain_Code").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Chain_Code")) & "")
                            .Attributes("Chain_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Chain_Name")) & "")
                            .Attributes("ContactPerson").InnerText = objSqlReader.GetValue(objSqlReader.GetOrdinal("ContactPerson") & "")
                            .Attributes("Priority_Auto_Update").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Priority_Auto_Update")) & "")
                            .Attributes("Grouped").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Grouped")) & "")
                            .Attributes("PK_Bkngs").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PK_Bkngs")) & "")
                            .Attributes("Aoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Aoffice")) & "")
                            .Attributes("PriorityID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PriorityID")) & "")
                            .Attributes("GroupTypeID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("GroupTypeID")) & "")
                            .Attributes("CQualifier").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CQualifier")) & "")
                            .Attributes("CCode").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CCode")) & "")
                            .Attributes("CityID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CityID")) & "")
                            .Attributes("MainGroup").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MainGroup")) & "")
                            .Attributes("Address").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Address")) & "")
                            .Attributes("GroupClassificationID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("GroupClassificationID")) & "")
                            .Attributes("Group_Classification_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Group_Classification_Name")) & "")
                            .Attributes("AccountManager").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AccountManager")) & "")
                            .Attributes("AccountManagerName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AccountManagerName")) & "")
                            .Attributes("Region").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Region")) & "")
                            .Attributes("PANNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PANNO")) & "")
                            .Attributes("GROUP_OFFICEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("GROUP_OFFICEID")) & "")
                            .Attributes("COMP_VERTICAL").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("COMP_VERTICAL")) & "")

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

        Public Function ListAgencyGroup() As System.Xml.XmlDocument
            'Purpose:This function gives search results based on choosen search criterion.
            'Output:
            '<MS_AGENCYGROUPLIST_OUTPUT>
            '	<AGENCYGROUP CHAIN_CODE = '' AGENCYGROUPNAME = '' ADDRESS = ''></AGENCYGROUP>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_AGENCYGROUPLIST_OUTPUT>

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "List"

            objOutputXml.LoadXml(strListAgencyGroup)

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LST_TA_AGENCYGROUP"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AGENCYGROUP")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("CHAIN_CODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHAIN_CODE")))
                    objAptNodeClone.Attributes("AGENCYGROUPNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AGENCYGROUPNAME")))
                    objAptNodeClone.Attributes("ADDRESS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ADDRESS")))

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

        Public Function ListGroupType() As System.Xml.XmlDocument
            'Output XML:
            ' <MS_LISTGROUPTYPE_OUTPUT>
            '	<GROUP_TYPE GroupTypeID='' GroupTypeName='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_LISTGROUPTYPE_OUTPUT>
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "ListGroupType"
            objOutputXml.LoadXml(gstrList_OUTPUT)
            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LST_MS_GROUP_TYPE"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("GROUP_TYPE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)

                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("GroupTypeID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("GROUPTYPEID")))
                    objAptNodeClone.Attributes("GroupTypeName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("GROUPTYPENAME")))
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
        Public Function ListPriority() As System.Xml.XmlDocument
            'Output XML:
            '<MS_LISTPRIORITY_OUTPUT>
            '	<PRIORITY PriorityID='' PriorityName='' StatusType=''/>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_LISTPRIORITY_OUTPUT>

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "ListPriority"
            objOutputXml.LoadXml(pstrList_OUTPUT)
            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LST_MS_PRIORITY"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("PRIORITY")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)

                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("PriorityID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRIORITYID")))
                    objAptNodeClone.Attributes("PriorityName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRIORITYNAME")))
                    objAptNodeClone.Attributes("StatusType").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("StatusType")))

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

        Public Function ListGroupClassificationType() As System.Xml.XmlDocument
            'Output XML:
            ' <MS_LISTGROUPCLASSIFICATIONTYPE_OUTPUT>
            '	<GROUPCLASSIFICATION_TYPE GroupClassificationID='' Group_Classification_Name='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_LISTGROUPCLASSIFICATIONTYPE_OUTPUT>

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "ListGroupClassificationType"
            objOutputXml.LoadXml(gstrListGroupClassification_OUTPUT)
            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_LST_MS_GROUPCLASSIFICAITON_TYPE]"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("GROUPCLASSIFICATION_TYPE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)

                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("GroupClassificationID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("GroupClassificationID")) & "")
                    objAptNodeClone.Attributes("Group_Classification_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Group_Classification_Name")) & "")
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