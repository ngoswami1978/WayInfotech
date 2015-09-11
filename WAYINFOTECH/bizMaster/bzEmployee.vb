'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Neeraj $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizMaster/bzEmployee.vb $
'$Workfile: bzEmployee.vb $
'$Revision: 105 $
'$Archive: /AAMS/Components/bizMaster/bzEmployee.vb $
'$Modtime: 8/08/11 5:43p $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Imports System.Math

Namespace bizMaster

    Public Class bzEmployee
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzEmployee"
        Const StrADDEMPLOYEE_OUTPUT = "<MS_UPDATEEMPLOYEE_INPUT><Employee EmployeeID='' Aoffice='' DepartmentID='' Limited_To_OwnAgency='' Limited_To_Region='' Limited_To_Aoffice='' Cell_Phone='' Login='' Email='' Employee_Name='' ManagerID='' IPRestriction='' Password='' FirstForm='' Designation='' ImmediateSupervisorID='' SecurityRegionID=''  DateStart='' DateEnd='' CityId='' /></MS_UPDATEEMPLOYEE_INPUT>"
        Const strSEARCH_OUTPUT = "<MS_SEARCHEMPLOYEE_OUTPUT><Employee EmployeeID='' Employee_Name='' Aoffice='' Department_Name='' DateStart='' DateEnd='' City_Name='' Login='' IPRestriction='' BR_ID = '' Request='' Cell_Phone='' Designation='' Email=''/><PAGE PAGE_COUNT='' TOTAL_ROWS='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHEMPLOYEE_OUTPUT>"
        Const srtIPVIEW_OUTPUT = "<MS_VIEWEMPLOYEEIP_OUTPUT><IPRESTRICTION /><IPAddress IP=''></IPAddress><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWEMPLOYEEIP_OUTPUT>"
        Const srtGROUPVIEW_OUTPUT = "<MS_VIEWEMPLOYEESUPERVISORY_OUTPUT><Supervisory DomainID='' DomainName='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWEMPLOYEESUPERVISORY_OUTPUT>"
        Const srtEMPGROUPVIEW_OUTPUT = "<MS_VIEWEMPLOYEEGROUP_OUTPUT><Chain_Code></Chain_Code><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWEMPLOYEEGROUP_OUTPUT>"
        Const srtGROUPUPDATE_OUTPUT = "<MS_UPDATEEMPLOYEESUPERVISORY_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEEMPLOYEESUPERVISORY_OUTPUT>"
        Const srtIPUPDATE_OUTPUT = "<MS_UPDATEEMPLOYEEIP_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEEMPLOYEEIP_OUTPUT>"
        Const srtEMPGROUPUPDATE_OUTPUT = "<MS_UPDATEEMPLOYEEGROUP_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEEMPLOYEEGROUP_OUTPUT>"
        Const strUPDATE_INPUT = "<MS_UPDATEEMPLOYEE_INPUT><Employee EmployeeID='' Aoffice='' DepartmentID='' Limited_To_OwnAgency='' Limited_To_Region='' Limited_To_Aoffice='' Cell_Phone='' Login='' Email='' Employee_Name='' ManagerID='' IPRestriction='' Password='' FirstForm='' Designation='' ImmediateSupervisorID='' SecurityRegionID=''  DateStart='' DateEnd='' CityId='' ChangePassword='' PwdExpire='' AgreementSigned='' Request=''/></MS_UPDATEEMPLOYEE_INPUT>" ' Changed for Login
        Const strUPDATE_OUTPUT = "<MS_UPDATEEMPLOYEE_OUTPUT><Employee EmployeeID='' Aoffice='' DepartmentID='' Limited_To_OwnAgency='' Limited_To_Region='' Limited_To_Aoffice='' Cell_Phone='' LoginRequired='' Login='' Email='' Employee_Name='' ManagerID='' IPRestriction='' IPAddress='' Password='' FirstForm='' Designation='' ImmediateSupervisorID='' SecurityRegionID=''  DateStart='' DateEnd='' CityId='' ChangePassword='' PwdExpire='' AgreementSigned=''  FirstLoginDone='' ForceToChangePassword='' Request='' GroupTypeID='' Show_Prod_ISUPERVISOR=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEEMPLOYEE_OUTPUT>"

        Const strVIEW_INPUT = "<MS_VIEWEMPLOYEE_INPUT><EmployeeID></EmployeeID></MS_VIEWEMPLOYEE_INPUT>"
        Const srtVIEW_OUTPUT = "<MS_VIEWEMPLOYEE_OUTPUT><Employee EmployeeID='' Aoffice='' DepartmentID='' Limited_To_OwnAgency='' Limited_To_Region='' Limited_To_Aoffice='' Cell_Phone='' LoginRequired='' Login='' Password='' Email='' Employee_Name='' ManagerID='' IPRestriction='' IPAddress=''  FirstForm='' Designation='' ImmediateSupervisorID='' SecurityRegionID='' DateStart='' DateEnd='' CityId='' ChangePassword ='' PwdExpire ='' AgreementSigned ='' Request='' GroupTypeID='' Show_Prod_ISUPERVISOR='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWEMPLOYEE_OUTPUT>"

        Const strLOGIN_OUTPUT = "<MS_LOGIN_OUTPUT><Administrator></Administrator><EmployeeID></EmployeeID><EmailID></EmailID><Login></Login><Employee_Name></Employee_Name><Aoffice></Aoffice><DepartmentID></DepartmentID><Designation></Designation><Limited_To_OwnAgency></Limited_To_OwnAgency><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><SecurityRegionID></SecurityRegionID><ManagerID></ManagerID><DSR_TARGET_DAYS_CHECK></DSR_TARGET_DAYS_CHECK><DSR_TARGET_DAYS></DSR_TARGET_DAYS><ImmediateSupervisorID></ImmediateSupervisorID><FirstForm></FirstForm><ChangePassword></ChangePassword><FirstLoginDone></FirstLoginDone><ForceToChangePassword></ForceToChangePassword><Request></Request><Manager></Manager><SECURITY_OPTIONS><SECURITY_OPTION SecurityOptionID='' SecurityOptionSubName='' Value='' /></SECURITY_OPTIONS><DisplayFirstForm><FirstFormDetails ID='' Name='' URL='' Module='' /></DisplayFirstForm><HelpDesk_Default><HD_Default FUN_ASSIGNEDTO='' TEC_ASSIGNEDTO='' CONTACT_TYPE_ID='' TEC_CONTACT_TYPE_ID=''/></HelpDesk_Default><Sales_Default><SL_Default TARGET_CHECK='' VISIT_TARGET_DAYS=''/></Sales_Default><Errors Status=''><Error Code='' Description='' /></Errors></MS_LOGIN_OUTPUT>"

        Const strDELETE_INPUT = "<MS_DELETEEMPLOYEE_INPUT><EmployeeID></EmployeeID></MS_DELETEEMPLOYEE_INPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETEEMPLOYEE_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETEEMPLOYEE_OUTPUT>"
        Const strPERMISSIONVIEW_INPUT = "<MS_VIEWEMPLOYEEPERMISSION_INPUT><EmployeeID></EmployeeID></MS_VIEWEMPLOYEEPERMISSION_INPUT>"
        Const strPERMISSIONVIEW_OUTPUT = "<MS_VIEWEMPLOYEEPERMISSION_OUTPUT><SECURITY_OPTIONS><SECURITY_OPTION SecurityOptionID='' Value='' /></SECURITY_OPTIONS><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWEMPLOYEEPERMISSION_OUTPUT>"
        Const strPERMISSIONUPDATE_INPUT = "<MS_UPDATEEMPLOYEEPERMISSION_INPUT><EmployeeID></EmployeeID><SECURITY_OPTIONS><SECURITY_OPTION SecurityOptionID='' Value='' /></SECURITY_OPTIONS></MS_UPDATEEMPLOYEEPERMISSION_INPUT>"
        Const strPERMISSIONUPDATE_OUTPUT = "<MS_UPDATEEMPLOYEEPERMISSION_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEEMPLOYEEPERMISSION_OUTPUT>"
        Const strMANAGERLIST_OUTPUT = "<MS_EMPLOYEE_OUTPUT><EMPLOYEE EmployeeID='' Employee_Name='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_EMPLOYEE_OUTPUT>"
        Const strLIST_OUTPUT = "<MS_EMPLOYEE_OUTPUT><EMPLOYEE EmployeeID='' Employee_Name='' DepartmentName=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_EMPLOYEE_OUTPUT>"
        Const strCHANGEPASSWORD_OUTPUT = "<MS_CHANGEPASSWORD_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_CHANGEPASSWORD_OUTPUT>"
        Const strREGISTRATIONID_OUTPUT = "<MS_UPDATEREGISTRATIONID_OUTPUT><REGISTRATION LCODE='' USERNAME='' PASSWORD='' TRAININGSTATUS='' /><Errors Status='FALSE'><Error Code='' Description=''/></Errors></MS_UPDATEREGISTRATIONID_OUTPUT>"
        Const strFIRSTFORM_OUTPUT = "<MS_EMPLOYEE_FIRSTFORM_OUTPUT><EMPLOYEE_FIRSTFORM MODULE='' DISPLAYFORMNAME='' FORMNAME='' URL=''/><Errors Status=''><Error Code='' Description=''/></Errors></MS_EMPLOYEE_FIRSTFORM_OUTPUT>"
        Const strEMPLOYEEHISTORY_OUTPUT = "<MS_HISTORY_EMPLOYEEPERMISSION_OUTPUT><EMPLOYEE USER='' LOGDATE='' Sec_Group='' SEC_GROUP_ID='' SecurityOptionSubName='' CHANGEDDATA=''/><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description=''/></Errors></MS_HISTORY_EMPLOYEEPERMISSION_OUTPUT>"
        Const strReserveUsernamePassword_OUTPUT = "<MS_RESERVE_USERNAME_PASSWORD_OUTPUT><RESERVE  FIELD_VALUE=''/><Errors Status='False'><Error Code='' Description=''/></Errors></MS_RESERVE_USERNAME_PASSWORD_OUTPUT>"
        Const strGetEmployeeListDeptWise = "<MS_DEPT_EMPLOYEE_OUTPUT><EMPLOYEE  EmployeeID='' Employee_Name=''/><Errors Status='False'><Error Code='' Description=''/></Errors></MS_DEPT_EMPLOYEE_OUTPUT>"

        Public Function GetEmployeeListDeptWise(ByVal IndexDoc As XmlDocument) As XmlDocument
            '***********************************************************************
            '<MS_DEPT_EMPLOYEE_INPUT>
            '   <DEPT></DEPT>
            '</MS_DEPT_EMPLOYEE_INPUT> 
            '---------------------------------
            '<MS_DEPT_EMPLOYEE_OUTPUT>
            '   <EMPLOYEE  EmployeeID="" Employee_Name=""/>
            '  <Errors Status="False">
            '     <Error Code="" Description=""/>
            ' </Errors>
            '</MS_DEPT_EMPLOYEE_OUTPUT> 
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strDETP_ID As String
            Const strMETHOD_NAME As String = "GetEmployeeListDeptWise"
            Dim objEMPNode, objEMPNodeClone As XmlNode
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(strGetEmployeeListDeptWise)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strDETP_ID = IndexDoc.DocumentElement.SelectSingleNode("DEPT").InnerText.Trim
                If strDETP_ID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LST_MS_DEPTWISE_EMPLOYEE_LIST"
                    .Connection = objSqlConnection
                    .Parameters.Add("@DEPT", SqlDbType.Int)
                    .Parameters("@DEPT").Value = strDETP_ID

                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                objEMPNode = objOutputXml.DocumentElement.SelectSingleNode("EMPLOYEE")
                objEMPNodeClone = objEMPNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objEMPNode)

                Do While objSqlReader.Read()

                    objEMPNodeClone.Attributes("EmployeeID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmployeeID")))
                    objEMPNodeClone.Attributes("Employee_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMPLOYEE_NAME")))
                    objOutputXml.DocumentElement.AppendChild(objEMPNodeClone)
                    objEMPNodeClone = objEMPNode.CloneNode(True)

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
            '<MS_UPDATEEMPLOYEE_INPUT>
            '   <Employee EmployeeID='' Aoffice='' DepartmentID='' Limited_To_OwnAgency='' Limited_To_Region='' Limited_To_Aoffice='' Cell_Phone='' Login='' Email='' Employee_Name='' ManagerID='' IPRestriction='' Password='' FirstForm='' Designation='' ImmediateSupervisorID='' SecurityRegionID=''  DateStart='' DateEnd='' CityId='' />
            '</MS_UPDATEEMPLOYEE_INPUT>
            '**************************************************************************
            Dim strMETHOD_NAME As String = "Add"
            Dim objXMLDoc As New XmlDocument
            Try
                objXMLDoc.LoadXml(StrADDEMPLOYEE_OUTPUT)
            Catch Exec As Exception
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objXMLDoc, "101", Exec.Message)
                Return objXMLDoc
            End Try
            Return objXMLDoc
        End Function

        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Delete
            '***********************************************************************
            'Purpose:This function deletes a employee.
            'Input:XmlDocument


            'Output :
            '<MS_DELETEEMPLOYEE_OUTPUT>
            '   <Errors Status=''>
            '	    <Error Code='' Description='' />
            '   </Errors>
            '</MS_DELETEEMPLOYEE_OUTPUT>

            '************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Delete"
            Dim intEmployeeID As Integer
            Dim objDeleteDocOutput As New XmlDocument
            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0
                If DeleteDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim <> "" Then
                    intEmployeeID = DeleteDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim
                End If
                If intEmployeeID = 0 Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_EMPLOYEES"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"

                    .Parameters.Add(New SqlParameter("@EMPLOYEEID", SqlDbType.Int))
                    .Parameters("@EMPLOYEEID").Value = intEmployeeID

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
                bzShared.FillErrorStatus(objDeleteDocOutput, "101", "Employee in Use!")
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
            '<MS_SEARCHEMPLOYEE_INPUT>
            '	<Employee_Name></Employee_Name>
            '	<DepartmentID></DepartmentID>
            '	<Aoffice></Aoffice>
            '   <Designation></Designation>
            '   <Sec_Group_ID></Sec_Group_ID>
            '   <SecurityOptionID ></SecurityOptionID>
            '   <Request></Request>
            '   <PAGE_NO></PAGE_NO>
            '   <PAGE_SIZE></PAGE_SIZE>
            '   <SORT_BY></SORT_BY>
            '   <DESC></DESC>
            '   <INC>T</INC>
            '</MS_SEARCHEMPLOYEE_INPUT>

            'Output :
            '<MS_SEARCHEMPLOYEE_OUTPUT>
            '	<Employee EmployeeID='' Employee_Name='' Aoffice='' Department_Name='' DateStart='' DateEnd='' City_Name='' Login='' BR_ID='' Request=''/>
            '   <PAGE PAGE_COUNT='' TOTAL_ROWS='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_SEARCHEMPLOYEE_OUTPUT>
            ''************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Dim intSec_Group_ID As Integer
            Dim strINC As String = "F"


            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)

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
            If SearchDoc.DocumentElement.SelectSingleNode("Sec_Group_ID") IsNot Nothing Then
                If SearchDoc.DocumentElement.SelectSingleNode("Sec_Group_ID").InnerText.Trim() <> "" Then
                    intSec_Group_ID = SearchDoc.DocumentElement.SelectSingleNode("Sec_Group_ID").InnerText.Trim()
                End If
            End If

            If SearchDoc.DocumentElement.SelectSingleNode("INC") IsNot Nothing Then
                If SearchDoc.DocumentElement.SelectSingleNode("INC").InnerText.Trim() <> "" Then
                    strINC = SearchDoc.DocumentElement.SelectSingleNode("INC").InnerText.Trim()
                End If
            End If



            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SER_EMPLOYEES"
                    .Connection = objSqlConnection
                    .Parameters.Add("@Employee_Name", SqlDbType.VarChar, 50)
                    If (SearchDoc.DocumentElement.SelectSingleNode("Employee_Name").InnerText.Trim = "") Then
                        .Parameters("@Employee_Name").Value = DBNull.Value
                    Else
                        .Parameters("@Employee_Name").Value = SearchDoc.DocumentElement.SelectSingleNode("Employee_Name").InnerText.Trim
                    End If
                    .Parameters.Add("@DEPARTMENTID", SqlDbType.Int)
                    If (SearchDoc.DocumentElement.SelectSingleNode("DepartmentID").InnerText.Trim = "") Then
                        .Parameters("@DEPARTMENTID").Value = DBNull.Value
                    Else
                        .Parameters("@DEPARTMENTID").Value = SearchDoc.DocumentElement.SelectSingleNode("DepartmentID").InnerText.Trim
                    End If
                    .Parameters.Add("@AOFFICE", SqlDbType.VarChar, 3)
                    If (SearchDoc.DocumentElement.SelectSingleNode("Aoffice").InnerText.Trim = "") Then
                        .Parameters("@AOFFICE").Value = DBNull.Value
                    Else
                        .Parameters("@AOFFICE").Value = SearchDoc.DocumentElement.SelectSingleNode("Aoffice").InnerText.Trim
                    End If

                    .Parameters.Add("@Designation", SqlDbType.VarChar, 100)
                    If (SearchDoc.DocumentElement.SelectSingleNode("Designation").InnerText.Trim = "") Then
                        .Parameters("@Designation").Value = DBNull.Value
                    Else
                        .Parameters("@Designation").Value = SearchDoc.DocumentElement.SelectSingleNode("Designation").InnerText.Trim
                    End If

                    .Parameters.Add("@SECURITYGROUPID", SqlDbType.Int)
                    If intSec_Group_ID = 0 Then
                        .Parameters("@SECURITYGROUPID").Value = DBNull.Value
                    Else
                        .Parameters("@SECURITYGROUPID").Value = intSec_Group_ID
                    End If


                    .Parameters.Add("@SecurityOptionID", SqlDbType.Int)
                    If (SearchDoc.DocumentElement.SelectSingleNode("SecurityOptionID").InnerText.Trim = "") Then
                        .Parameters("@SecurityOptionID").Value = DBNull.Value
                    Else
                        .Parameters("@SecurityOptionID").Value = SearchDoc.DocumentElement.SelectSingleNode("SecurityOptionID").InnerText.Trim
                    End If

                    .Parameters.Add("@AgreementSigned", SqlDbType.Bit)
                    If (SearchDoc.DocumentElement.SelectSingleNode("AgreementSigned").InnerText.Trim = "1") Then
                        .Parameters("@AgreementSigned").Value = 1
                    ElseIf (SearchDoc.DocumentElement.SelectSingleNode("AgreementSigned").InnerText.Trim = "2") Then
                        .Parameters("@AgreementSigned").Value = 0
                    ElseIf (SearchDoc.DocumentElement.SelectSingleNode("AgreementSigned").InnerText.Trim = "3") Then
                        .Parameters("@AgreementSigned").Value = DBNull.Value
                    End If

                    .Parameters.Add("@LIMITED_TO_AOFFICE", SqlDbType.VarChar, 3)
                    If SearchDoc.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText.Trim <> "" Then
                        .Parameters("@LIMITED_TO_AOFFICE").Value = SearchDoc.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText.Trim
                    Else
                        .Parameters("@LIMITED_TO_AOFFICE").Value = DBNull.Value
                    End If

                    .Parameters.Add("@SECURITYREGIONID", SqlDbType.Int)
                    If SearchDoc.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText.Trim <> "" Then
                        .Parameters("@SECURITYREGIONID").Value = SearchDoc.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText.Trim
                    Else
                        .Parameters("@SECURITYREGIONID").Value = DBNull.Value
                    End If

                    .Parameters.Add("@LIMITED_TO_REGION", SqlDbType.Int)
                    If SearchDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.Trim <> "" Then
                        .Parameters("@LIMITED_TO_REGION").Value = SearchDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.Trim
                    Else
                        .Parameters("@LIMITED_TO_REGION").Value = DBNull.Value
                    End If

                    If SearchDoc.DocumentElement.SelectSingleNode("Request") IsNot Nothing Then
                        .Parameters.Add("@Request", SqlDbType.Bit)
                        If SearchDoc.DocumentElement.SelectSingleNode("Request").InnerText.Trim <> "" Then
                            .Parameters("@Request").Value = Val(SearchDoc.DocumentElement.SelectSingleNode("Request").InnerText.Trim & "")
                        Else
                            .Parameters("@Request").Value = DBNull.Value
                        End If
                    Else
                        .Parameters.Add("@Request", SqlDbType.Bit)
                        .Parameters("@Request").Value = DBNull.Value
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

                    .Parameters.Add("@INC", SqlDbType.Char, 1)
                    .Parameters("@INC").Value = strINC

                    .Parameters.Add(New SqlParameter("@TOTALROWS", SqlDbType.BigInt))
                    .Parameters("@TOTALROWS").Direction = ParameterDirection.Output
                    .Parameters("@TOTALROWS").Value = 0

                End With

                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("Employee")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("EmployeeID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmployeeID")))
                    objAptNodeClone.Attributes("Employee_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Employee_Name")) & "")
                    objAptNodeClone.Attributes("Department_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Department_Name")) & "")
                    objAptNodeClone.Attributes("Aoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Aoffice")) & "")
                    objAptNodeClone.Attributes("DateStart").InnerText = (objSqlReader.GetValue(objSqlReader.GetOrdinal("DateStart")) & "")
                    objAptNodeClone.Attributes("DateEnd").InnerText = (objSqlReader.GetValue(objSqlReader.GetOrdinal("DateEnd")) & "")
                    objAptNodeClone.Attributes("City_Name").InnerText = (objSqlReader.GetValue(objSqlReader.GetOrdinal("City_Name")) & "")
                    objAptNodeClone.Attributes("Login").InnerText = (objSqlReader.GetValue(objSqlReader.GetOrdinal("Login")) & "")
                    objAptNodeClone.Attributes("BR_ID").InnerText = (objSqlReader.GetValue(objSqlReader.GetOrdinal("BR_ID")) & "")
                    objAptNodeClone.Attributes("Request").InnerText = (objSqlReader.GetValue(objSqlReader.GetOrdinal("Request")) & "")

                    If objSqlReader.GetValue(objSqlReader.GetOrdinal("IPRestriction")) = True Then
                        objAptNodeClone.Attributes("IPRestriction").InnerText = 1
                    Else
                        objAptNodeClone.Attributes("IPRestriction").InnerText = 0
                    End If
                    objAptNodeClone.Attributes("Cell_Phone").InnerText = (objSqlReader.GetValue(objSqlReader.GetOrdinal("Cell_Phone")) & "")

                    objAptNodeClone.Attributes("Designation").InnerText = (objSqlReader.GetValue(objSqlReader.GetOrdinal("Designation")) & "")
                    objAptNodeClone.Attributes("Email").InnerText = (objSqlReader.GetValue(objSqlReader.GetOrdinal("Email")) & "")
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop
                objSqlReader.Close()
                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").InnerText = Val(objSqlCommand.Parameters("@TOTALROWS").Value)
                    If intPageSize = 0 Then
                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = 0
                    Else
                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = Math.Ceiling(Val(objSqlCommand.Parameters("@TOTALROWS").Value) / intPageSize)
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
            'Purpose:This function Inserts/Updates employee.
            'Input  :
            '<MS_UPDATEEMPLOYEE_INPUT>
            '  <Employee EmployeeID='' Aoffice='' DepartmentID='' Limited_To_OwnAgency='' Limited_To_Region='' Limited_To_Aoffice='' Cell_Phone='' Email='' Employee_Name='' ManagerID='' LoginRequired='' Login='' Password='' FirstForm='' Designation='' ImmediateSupervisorID='' SecurityRegionID=''  DateStart='' DateEnd='' CityId='' ChangePassword='' PwdExpire='' AgreementSigned='' Request='' GroupTypeID='' Show_Prod_ISUPERVISOR='' IPRestriction='' IPAddress=''/>
            '  <EmailID/>
            '</MS_UPDATEEMPLOYEE_INPUT>

            'Output :
            '<MS_UPDATEEMPLOYEE_OUTPUT>
            '   <Employee EmployeeID='' Aoffice='' DepartmentID='' Limited_To_OwnAgency='' Limited_To_Region='' Limited_To_Aoffice='' Cell_Phone='' Login='' Email='' Employee_Name='' ManagerID='' IPRestriction='' Password='' FirstForm='' Designation='' ImmediateSupervisorID='' SecurityRegionID=''  DateStart='' DateEnd='' CityId='' ChangePassword='' PwdExpire='' AgreementSigned=''  FirstLoginDone='' ForceToChangePassword='' GroupTypeID='' Show_Prod_ISUPERVISOR=''/>
            '   <Errors Status=''><Error Code='' Description='' /></Errors>
            '</MS_UPDATEEMPLOYEE_OUTPUT>
            '************************************************************************
            'Dim onjCrypto As New AAMS.bizCryptography.bzCryptography
            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objSqlCommand1 As New SqlCommand

            Dim objSqlCommandHistory As New SqlCommand
            Dim objTransaction As SqlTransaction

            Dim objUpdateDocOutput As New XmlDocument
            Dim intEmployeeID As Integer
            Dim strAoffice As String
            Dim intDepartmentID As Integer
            Dim intLimited_To_OwnAgency As Integer
            Dim intLimited_To_Region As Integer
            Dim intLimited_To_Aoffice As Integer
            Dim strCell_Phone As String
            Dim strLogin As String
            Dim strEmail As String
            Dim strEmployee_Name As String
            Dim intManagerID As Integer
            Dim intIPRestriction As Integer
            Dim strPassword As String
            Dim strFirstForm As String
            Dim strDesignation As String
            Dim intImmediateSupervisorID As Integer
            Dim intSecurityRegionID As Integer
            Dim intDateStart As Integer
            Dim intDateEnd As Integer
            Dim intCityId As Integer
            Dim intRequest As Integer
            Dim intTempRequest As Integer
            Dim intViewRequest As Integer

            Dim intRequestEmpID As Integer

            Dim intChangePassword As Integer
            Dim intPwdExpire As Integer
            Dim intAgreementSigned As Integer
            Dim intGroupTypeID As Integer
            Dim intRecordsAffected As Int32
            Const strMETHOD_NAME As String = "Update"
            Dim strFromEmailId As String = ""
            Dim blnMailBlock As Boolean
            Try
                blnMailBlock = False
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)
                ' Storing Email ID For Mail.
                If Not UpdateDoc.DocumentElement.SelectSingleNode("EmailID") Is Nothing Then
                    strFromEmailId = UpdateDoc.DocumentElement.SelectSingleNode("EmailID").InnerText
                End If



                With objUpdateDocOutput.DocumentElement.SelectSingleNode("Employee")
                    If UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("EmployeeID").InnerText = "" Then
                        .Attributes("EmployeeID").InnerText = ""
                        intRequestEmpID = 0
                    Else
                        intRequestEmpID = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("EmployeeID").InnerText
                        .Attributes("EmployeeID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("EmployeeID").InnerText
                    End If
                    .Attributes("Aoffice").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("Aoffice").InnerText
                    .Attributes("DepartmentID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("DepartmentID").InnerText
                    .Attributes("Limited_To_OwnAgency").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("Limited_To_OwnAgency").InnerText
                    .Attributes("Limited_To_Region").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("Limited_To_Region").InnerText
                    .Attributes("Limited_To_Aoffice").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("Limited_To_Aoffice").InnerText
                    .Attributes("Cell_Phone").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("Cell_Phone").InnerText
                    .Attributes("LoginRequired").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("LoginRequired").InnerText
                    .Attributes("Login").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("Login").InnerText
                    .Attributes("Password").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("Password").InnerText

                    .Attributes("Email").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("Email").InnerText
                    .Attributes("Employee_Name").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("Employee_Name").InnerText
                    .Attributes("ManagerID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("ManagerID").InnerText


                    .Attributes("FirstForm").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("FirstForm").InnerText
                    .Attributes("Designation").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("Designation").InnerText
                    .Attributes("ImmediateSupervisorID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("ImmediateSupervisorID").InnerText
                    .Attributes("SecurityRegionID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("SecurityRegionID").InnerText
                    .Attributes("DateStart").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("DateStart").InnerText
                    .Attributes("DateEnd").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("DateEnd").InnerText
                    .Attributes("CityId").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("CityId").InnerText

                    .Attributes("ChangePassword").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("ChangePassword").InnerText
                    .Attributes("PwdExpire").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("PwdExpire").InnerText
                    .Attributes("AgreementSigned").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("AgreementSigned").InnerText
                    .Attributes("Request").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("Request").InnerText

                    .Attributes("GroupTypeID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("GroupTypeID").InnerText
                    .Attributes("Show_Prod_ISUPERVISOR").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("Show_Prod_ISUPERVISOR").InnerText

                    .Attributes("IPRestriction").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("IPRestriction").InnerText
                    .Attributes("IPAddress").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("IPAddress").InnerText
                End With


                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("Employee")
                    If ((.Attributes("EmployeeID").InnerText).Trim).ToString <> "" Then
                        intEmployeeID = ((.Attributes("EmployeeID").InnerText).Trim).ToString
                        strAction = "U"

                        '' CALL VIEW FOR UESR REQUEST 
                        Dim objXmlInput As New Xml.XmlDocument
                        Dim objXmlOutput As New Xml.XmlDocument
                        objXmlInput.LoadXml("<MS_VIEWEMPLOYEE_INPUT><EmployeeID>" & intEmployeeID & " </EmployeeID></MS_VIEWEMPLOYEE_INPUT>")
                        objXmlOutput = View(objXmlInput)

                        intViewRequest = IIf(objXmlOutput.DocumentElement.SelectSingleNode("Employee").Attributes("Request").InnerText = "True", 1, 0)

                    Else
                        intEmployeeID = 0
                        strAction = "I"
                    End If
                    strAoffice = ((.Attributes("Aoffice").InnerText).Trim).ToString
                    If ((.Attributes("DepartmentID").InnerText).Trim).ToString <> "" Then
                        intDepartmentID = ((.Attributes("DepartmentID").InnerText).Trim).ToString
                    Else
                        intDepartmentID = 0
                    End If
                    If ((.Attributes("Limited_To_OwnAgency").InnerText).Trim).ToString <> "" Then
                        intLimited_To_OwnAgency = ((.Attributes("Limited_To_OwnAgency").InnerText).Trim).ToString
                    End If
                    If ((.Attributes("Limited_To_Region").InnerText).Trim).ToString <> "" Then
                        intLimited_To_Region = ((.Attributes("Limited_To_Region").InnerText).Trim).ToString
                    End If
                    If (.Attributes("Limited_To_Aoffice").InnerText) <> "" Then
                        intLimited_To_Aoffice = (.Attributes("Limited_To_Aoffice").InnerText)
                    End If
                    strCell_Phone = (.Attributes("Cell_Phone").InnerText)
                    strLogin = ((.Attributes("Login").InnerText).Trim).ToString
                    strEmail = ((.Attributes("Email").InnerText).Trim).ToString
                    strEmployee_Name = (.Attributes("Employee_Name").InnerText)
                    If ((.Attributes("ManagerID").InnerText).Trim).ToString <> "" Then
                        intManagerID = ((.Attributes("ManagerID").InnerText).Trim).ToString
                    End If
                    'If ((.Attributes("IPRestriction").InnerText).Trim).ToString <> "" Then
                    '    intIPRestriction = ((.Attributes("IPRestriction").InnerText).Trim).ToString
                    'End If
                    strPassword = ((.Attributes("Password").InnerText).Trim).ToString
                    strFirstForm = (.Attributes("FirstForm").InnerText)
                    strDesignation = (.Attributes("Designation").InnerText)
                    If ((.Attributes("ImmediateSupervisorID").InnerText).Trim).ToString <> "" Then
                        intImmediateSupervisorID = ((.Attributes("ImmediateSupervisorID").InnerText).Trim).ToString
                    End If
                    If ((.Attributes("SecurityRegionID").InnerText).Trim).ToString <> "" Then
                        intSecurityRegionID = ((.Attributes("SecurityRegionID").InnerText).Trim).ToString
                    End If
                    If (.Attributes("DateStart").InnerText) <> "" Then
                        intDateStart = (.Attributes("DateStart").InnerText)
                    End If
                    If ((.Attributes("DateEnd").InnerText).Trim).ToString <> "" Then
                        intDateEnd = ((.Attributes("DateEnd").InnerText).Trim).ToString
                    End If
                    If ((.Attributes("CityId").InnerText).Trim).ToString <> "" Then
                        intCityId = ((.Attributes("CityId").InnerText).Trim).ToString
                    End If

                    If ((.Attributes("ChangePassword").InnerText).Trim).ToString <> "" Then
                        intChangePassword = ((.Attributes("ChangePassword").InnerText).Trim).ToString
                    End If

                    If ((.Attributes("PwdExpire").InnerText).Trim).ToString <> "" Then
                        intPwdExpire = ((.Attributes("PwdExpire").InnerText).Trim).ToString
                    End If

                    If ((.Attributes("AgreementSigned").InnerText).Trim).ToString <> "" Then
                        intAgreementSigned = ((.Attributes("AgreementSigned").InnerText).Trim).ToString
                    End If

                    If ((.Attributes("Request").InnerText).Trim).ToString <> "" Then
                        intRequest = Val(((.Attributes("Request").InnerText).Trim).ToString & "")
                    End If

                    If ((.Attributes("GroupTypeID").InnerText).Trim).ToString <> "" Then
                        intGroupTypeID = ((.Attributes("GroupTypeID").InnerText).Trim).ToString
                    End If

                    If strAction = "I" Or strAction = "U" Then
                        If strAoffice = "" Then
                            Throw (New AAMSException("Employee Name can't be blank."))
                        ElseIf intDepartmentID = 0 Then
                            Throw (New AAMSException("Department can't be blank."))
                        ElseIf strAoffice = "" Then
                            Throw (New AAMSException("1a office can't be blank."))
                        End If
                    Else
                        Throw (New AAMSException("Invalid Action Code."))
                    End If

                End With


                ''if update
                If UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("EmployeeID").InnerText <> "" Then
                    With objSqlCommandHistory
                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "[UP_SRO_MS_EMPLOYEE_HISTORY]"
                        .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                        .Parameters("@ACTION").Value = "I"

                        .Parameters.Add(New SqlParameter("@EMPLOYEEID", SqlDbType.Int))
                        If UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("EmployeeID").InnerText <> "" Then
                            .Parameters("@EMPLOYEEID").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("EmployeeID").InnerText)
                        Else
                            .Parameters("@EMPLOYEEID").Value = DBNull.Value
                        End If

                        .Parameters.Add(New SqlParameter("@CHANGEDBY", SqlDbType.Int))
                        If UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("ChangedBy").InnerText <> "" Then
                            .Parameters("@CHANGEDBY").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("ChangedBy").InnerText)
                        Else
                            .Parameters("@CHANGEDBY").Value = DBNull.Value
                        End If

                        .Parameters.Add(New SqlParameter("@HISTORYID", SqlDbType.Int))
                        .Parameters("@HISTORYID").Value = 1

                        .Parameters.Add(New SqlParameter("@INPUTXML", SqlDbType.Xml))
                        .Parameters("@INPUTXML").Value = UpdateDoc.OuterXml

                        .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                        .Parameters("@RETURNID").Direction = ParameterDirection.Output
                        .Parameters("@RETURNID").Value = 0

                    End With
                End If

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_EMPLOYEES"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction

                    .Parameters.Add(New SqlParameter("@EMPLOYEEID", SqlDbType.Int))
                    If intEmployeeID = 0 Then
                        .Parameters("@EMPLOYEEID").Value = vbNullString
                    Else
                        .Parameters("@EMPLOYEEID").Value = intEmployeeID
                    End If

                    .Parameters.Add(New SqlParameter("@AOFFICE", SqlDbType.VarChar, 3))
                    .Parameters("@AOFFICE").Value = strAoffice

                    .Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.Int))
                    .Parameters("@DepartmentID").Value = intDepartmentID

                    .Parameters.Add(New SqlParameter("@LIMITED_TO_OWNAGENCY", SqlDbType.Bit))
                    .Parameters("@LIMITED_TO_OWNAGENCY").Value = intLimited_To_OwnAgency

                    .Parameters.Add(New SqlParameter("@LIMITED_TO_REGION", SqlDbType.Bit))
                    .Parameters("@LIMITED_TO_REGION").Value = intLimited_To_Region

                    .Parameters.Add(New SqlParameter("@LIMITED_TO_AOFFICE", SqlDbType.Bit))
                    .Parameters("@LIMITED_TO_AOFFICE").Value = intLimited_To_Aoffice

                    .Parameters.Add(New SqlParameter("@CELL_PHONE", SqlDbType.VarChar, 30))
                    .Parameters("@CELL_PHONE").Value = strCell_Phone


                    .Parameters.Add(New SqlParameter("@LOGIN", SqlDbType.VarChar, 20))
                    .Parameters.Add(New SqlParameter("@Password", SqlDbType.VarChar, 300))
                    .Parameters.Add(New SqlParameter("@LOGINREQUIRED", SqlDbType.Bit))
                    If UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("LoginRequired").InnerText.ToUpper = "TRUE" Then
                        .Parameters("@LOGIN").Value = strLogin
                        '.Parameters("@Password").Value = onjCrypto.Encrypt(strPassword)
                        .Parameters("@Password").Value = strPassword
                        .Parameters("@LOGINREQUIRED").Value = 1
                    Else
                        .Parameters("@LOGIN").Value = DBNull.Value
                        '.Parameters("@Password").Value = onjCrypto.Encrypt(strPassword)
                        .Parameters("@Password").Value = DBNull.Value
                        .Parameters("@LOGINREQUIRED").Value = DBNull.Value
                    End If


                    .Parameters.Add(New SqlParameter("@EMAIL", SqlDbType.VarChar, 30))
                    .Parameters("@EMAIL").Value = strEmail

                    .Parameters.Add(New SqlParameter("@EMPLOYEE_NAME", SqlDbType.Char, 40))
                    .Parameters("@EMPLOYEE_NAME").Value = strEmployee_Name

                    .Parameters.Add(New SqlParameter("@MANAGERID", SqlDbType.Int))
                    .Parameters("@MANAGERID").Value = intManagerID


                    .Parameters.Add(New SqlParameter("@IPRESTRICTION", SqlDbType.Bit))
                    .Parameters.Add(New SqlParameter("@IPADDRESS", SqlDbType.VarChar, 20))
                    If UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("IPRestriction").InnerText.ToUpper = "TRUE" Then
                        .Parameters("@IPRESTRICTION").Value = 1
                        .Parameters("@IPADDRESS").Value = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("IPAddress").InnerText
                    Else
                        .Parameters("@IPRESTRICTION").Value = DBNull.Value
                        .Parameters("@IPADDRESS").Value = DBNull.Value
                    End If

                    .Parameters.Add(New SqlParameter("@FIRSTFORM", SqlDbType.VarChar, 100))
                    .Parameters("@FIRSTFORM").Value = strFirstForm


                    .Parameters.Add(New SqlParameter("@DESIGNATION", SqlDbType.VarChar, 100))
                    .Parameters("@DESIGNATION").Value = strDesignation

                    .Parameters.Add(New SqlParameter("@IMMEDIATESUPERVISORID", SqlDbType.Int))
                    .Parameters("@IMMEDIATESUPERVISORID").Value = intImmediateSupervisorID

                    .Parameters.Add(New SqlParameter("@SECURITYREGIONID", SqlDbType.Int))
                    .Parameters("@SECURITYREGIONID").Value = intSecurityRegionID

                    .Parameters.Add(New SqlParameter("@DATESTART", SqlDbType.Int))
                    If intDateStart = 0 Then
                        .Parameters("@DATESTART").Value = DBNull.Value
                    Else
                        .Parameters("@DATESTART").Value = intDateStart
                    End If

                    .Parameters.Add(New SqlParameter("@DATEEND", SqlDbType.Int))
                    If intDateEnd = 0 Then
                        .Parameters("@DATEEND").Value = DBNull.Value
                    Else
                        .Parameters("@DATEEND").Value = intDateEnd
                    End If


                    .Parameters.Add(New SqlParameter("@CITYID", SqlDbType.Int))
                    .Parameters("@CITYID").Value = intCityId

                    .Parameters.Add(New SqlParameter("@ChangePassword", SqlDbType.Int))
                    .Parameters("@ChangePassword").Value = intChangePassword

                    .Parameters.Add(New SqlParameter("@PwdExpire", SqlDbType.Int))
                    .Parameters("@PwdExpire").Value = intPwdExpire

                    .Parameters.Add(New SqlParameter("@AgreementSigned", SqlDbType.Int))
                    .Parameters("@AgreementSigned").Value = intAgreementSigned

                    .Parameters.Add(New SqlParameter("@Request", SqlDbType.Bit))
                    .Parameters("@Request").Value = intRequest

                    .Parameters.Add(New SqlParameter("@GroupTypeID", SqlDbType.SmallInt))
                    If intGroupTypeID = 0 Then
                        .Parameters("@GroupTypeID").Value = DBNull.Value
                    Else
                        .Parameters("@GroupTypeID").Value = intGroupTypeID
                    End If

                    .Parameters.Add(New SqlParameter("@Show_Prod_ISUPERVISOR", SqlDbType.Bit))
                    If UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("Show_Prod_ISUPERVISOR").InnerText.ToUpper = "TRUE" Then .Parameters("@Show_Prod_ISUPERVISOR").Value = 1
                    If UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("Show_Prod_ISUPERVISOR").InnerText.ToUpper = "FALSE" Then .Parameters("@Show_Prod_ISUPERVISOR").Value = 0
                    If UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("Show_Prod_ISUPERVISOR").InnerText.ToUpper = "" Then .Parameters("@Show_Prod_ISUPERVISOR").Value = DBNull.Value


                    .Parameters.Add(New SqlParameter("@RETUNID", SqlDbType.Int))
                    .Parameters("@RETUNID").Direction = ParameterDirection.Output
                    .Parameters("@RETUNID").Value = ""

                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED
                    .Connection.Open()

                    objTransaction = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)
                    objSqlCommandHistory.Connection = objSqlConnection
                    objSqlCommand.Transaction = objTransaction
                    objSqlCommandHistory.Transaction = objTransaction
                    .Transaction = objTransaction

                    If UCase(strAction) = "U" Then intRecordsAffected = objSqlCommandHistory.ExecuteNonQuery() 'Employee History
                    intRecordsAffected = .ExecuteNonQuery()
                    If UCase(strAction) = "I" Then
                        intRetId = .Parameters("@RETUNID").Value
                        If intRetId = 0 Then
                            Throw (New AAMSException("Unable to insert!"))
                        ElseIf intRetId = -1 Then
                            Throw (New AAMSException("Login already exists. Please enter another login"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Employee").Attributes("EmployeeID").InnerText = intRetId
                        End If
                    ElseIf UCase(strAction) = "U" Then
                        intRetId = .Parameters("@RETUNID").Value
                        If intRetId = 0 Then
                            Throw (New AAMSException("Unable to update!"))
                        ElseIf intRetId = -1 Then
                            Throw (New AAMSException("Login already exists. Please enter another login"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                        End If
                        intRetId = intEmployeeID
                    End If
                End With
                objTransaction.Commit()
                objSqlCommand.Connection.Close()
                'SEND EMAIL TO THE EMPLOYEES HAVING REQUEST FOR THE LOGIN


                Dim strEmaileIds As String = ""
                Dim strLetter As String = ""
                Dim objSqlReader As SqlDataReader
                Dim objAptNode, objAptNodeClone As XmlNode
                Dim objOutputXml As New XmlDocument
                Dim blnRecordFound As Boolean
                Dim strLOGIN_REQUEST_LETTER As String = ""
                Dim strLOGIN_CREATION_LETTER As String = ""
                Dim strDepartment As String = ""
                Dim strLogin1 As String = ""
                Dim strPassword1 As String = ""


                Dim ObjSendMail As bizUtility.bzEmail
                Dim objInputSendMailXml As New XmlDocument
                Dim strEmailFrom As String, strEmailTo As String, strEmailSubject As String, strEmailBody As String
                Dim boolSendMailStatus As Boolean

                '' ############################# GET LETTER TEMPLATE  ,  EMAIL ID       ####################################
                '' ############################# ADDING PARAMETERS IN STORED PROCEDURE  #################################### 
                If intRetId > 1 Then
                    blnMailBlock = True
                    With objSqlCommand1
                        .Connection = objSqlConnection
                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "[UP_GET_REQUEST_EMAIL]"

                        .Parameters.Add(New SqlParameter("@EMPLOYEEID", SqlDbType.BigInt))
                        If intRetId = 0 Then
                            .Parameters("@EMPLOYEEID").Value = vbNullString
                        Else
                            .Parameters("@EMPLOYEEID").Value = intRetId
                        End If
                    End With

                    objSqlCommand1.Connection.Open()
                    objSqlReader = objSqlCommand1.ExecuteReader()
                    objOutputXml.LoadXml("<UP_GET_EMAILSLIST><DETAILS MANAGEREMAILSID='' CONFIGEMIALSID='' LOGIN_REQUEST_LETTER='' LOGIN_CREATION_LETTER='' DEPARTMENTNAME='' LOGIN='' PASSWORD='' REQUEST=''/><Errors Status='FALSE'>	<Error Code='' Description=''></Error>	</Errors></UP_GET_EMAILSLIST>")

                    'READING AND APPENDING RECORDS INTO THE OUTPUT XMLDOCUMENT
                    objAptNode = objOutputXml.DocumentElement.SelectSingleNode("DETAILS")
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.RemoveChild(objAptNode)
                    Do While objSqlReader.Read()
                        blnRecordFound = True
                        objAptNodeClone.Attributes("MANAGEREMAILSID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MANAGEREMAILSID")) & "")
                        objAptNodeClone.Attributes("CONFIGEMIALSID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CONFIGEMIALSID")) & "")
                        objAptNodeClone.Attributes("LOGIN_REQUEST_LETTER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOGIN_REQUEST_LETTER")) & "")
                        objAptNodeClone.Attributes("LOGIN_CREATION_LETTER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOGIN_CREATION_LETTER")) & "")
                        objAptNodeClone.Attributes("DEPARTMENTNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DEPARTMENTNAME")) & "")

                        objAptNodeClone.Attributes("LOGIN").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOGIN")) & "")
                        objAptNodeClone.Attributes("PASSWORD").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PASSWORD")) & "")


                        strEmaileIds = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMP_EMAIL")) & "") & "," & Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("IMMEDIATESUPERVISORID_EMAILSID")) & "") & "," & Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MANAGEREMAILSID")) & "") & " , " & Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CONFIGEMIALSID")) & "")
                        strLOGIN_REQUEST_LETTER = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOGIN_REQUEST_LETTER")) & "")
                        strLOGIN_CREATION_LETTER = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOGIN_CREATION_LETTER")) & "")

                        strDepartment = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DEPARTMENTNAME")) & "")
                        strLogin1 = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOGIN")) & "")
                        strPassword1 = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PASSWORD")) & "")
                        intTempRequest = IIf(CBool(Val(Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("REQUEST")) & ""))) = "True", 1, 0)

                        If strDepartment.Trim.Length > 0 Then
                            strLOGIN_REQUEST_LETTER = strLOGIN_REQUEST_LETTER.Replace("[[Department Name]]", "" & strDepartment)
                        End If

                        If strLogin1.Trim.Length > 0 Then
                            strLOGIN_CREATION_LETTER = strLOGIN_CREATION_LETTER.Replace("[[UserName]]", "" & strLogin1)
                        End If
                        If strPassword1.Trim.Length > 0 Then
                            strLOGIN_CREATION_LETTER = strLOGIN_CREATION_LETTER.Replace("[[passowrd]]", "" & strPassword1)
                        End If

                        objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                        objAptNodeClone = objAptNode.CloneNode(True)
                        objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    Loop
                    objSqlReader.Close()
                    ''

                    '  If intRequest = 0 And intTempRequest = 0 Then
                    If intRequest = 0 And intTempRequest = 0 And intRequestEmpID = 0 Then
                        If strFromEmailId.Trim = "" Then
                            strEmailFrom = "admin@aams.com"
                        Else
                            strEmailFrom = strFromEmailId
                        End If

                        strEmailTo = strEmaileIds
                        strEmailBody = strLOGIN_REQUEST_LETTER
                        strEmailSubject = "Login Request"

                        Const strSendMail_INPUT = "<SENDMAILIMMEDIATE_INPUT><MAIL_DETAILS DESTINATION_TO='' DESTINATION_TO_NAME='' SUBJECT='' SOURCE='' MESSAGE='' DESTINATION_CC='' DESTINATION_BCC='' ATTACHMENT_FILE=''/></SENDMAILIMMEDIATE_INPUT>"

                        objInputSendMailXml.LoadXml(strSendMail_INPUT)

                        With objInputSendMailXml.SelectSingleNode("SENDMAILIMMEDIATE_INPUT/MAIL_DETAILS")
                            .Attributes("SOURCE").InnerText = strEmailFrom
                            .Attributes("DESTINATION_TO").InnerText = strEmaileIds
                            .Attributes("SUBJECT").InnerText = strEmailSubject
                            .Attributes("MESSAGE").InnerText = strEmailBody
                        End With
                        ObjSendMail = New bizUtility.bzEmail
                        boolSendMailStatus = ObjSendMail.SendMail(objInputSendMailXml)
                        If boolSendMailStatus = False Then
                            'Throw (New AAMSException("Unable to send Mails for the Requesting User!"))
                        End If

                    ElseIf intViewRequest = 0 And intTempRequest = 1 Then
                        '    If intRequestEmpID = 0 And intRequest = 0 Then
                        If strFromEmailId.Trim = "" Then
                            strEmailFrom = "admin@aams.com"
                        Else
                            strEmailFrom = strFromEmailId
                        End If
                        strEmailTo = strEmaileIds
                        strEmailBody = strLOGIN_CREATION_LETTER
                        strEmailSubject = "Login Request"

                        Const strSendMail_INPUT = "<SENDMAILIMMEDIATE_INPUT><MAIL_DETAILS DESTINATION_TO='' DESTINATION_TO_NAME='' SUBJECT='' SOURCE='' MESSAGE='' DESTINATION_CC='' DESTINATION_BCC='' ATTACHMENT_FILE=''/></SENDMAILIMMEDIATE_INPUT>"

                        objInputSendMailXml.LoadXml(strSendMail_INPUT)

                        With objInputSendMailXml.SelectSingleNode("SENDMAILIMMEDIATE_INPUT/MAIL_DETAILS")
                            .Attributes("SOURCE").InnerText = strEmailFrom
                            .Attributes("DESTINATION_TO").InnerText = strEmaileIds
                            .Attributes("SUBJECT").InnerText = strEmailSubject
                            .Attributes("MESSAGE").InnerText = strEmailBody
                        End With
                        ObjSendMail = New bizUtility.bzEmail
                        boolSendMailStatus = ObjSendMail.SendMail(objInputSendMailXml)
                        If boolSendMailStatus = False Then
                            'Throw (New AAMSException("Unable to send Mails !"))
                        End If
                    End If
                End If
                'End With

            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                If Not objTransaction Is Nothing Then
                    objTransaction.Rollback()
                End If
                bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                Return objUpdateDocOutput
            Catch Exec As Exception
                If Not objTransaction Is Nothing Then
                    objTransaction.Rollback()
                End If
                If intRetId = 0 Then
                    bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                    If blnMailBlock = True Then
                        bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                    Else
                        If strAction = "I" Then
                            bzShared.FillErrorStatus(objUpdateDocOutput, "101", "Login already exists. Please enter another login")
                        Else
                            bzShared.FillErrorStatus(objUpdateDocOutput, "101", "Unable to update!")
                        End If
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
                objSqlCommand1.Dispose()
                objSqlCommandHistory.Dispose()
            End Try


            Return objUpdateDocOutput
        End Function

        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.View
            '***********************************************************************
            'Purpose:This function gives details of employee.
            'Input  : '<MS_VIEWEMPLOYEE_INPUT>
            '	<EmployeeID></EmployeeID>
            '</MS_VIEWEMPLOYEE_INPUT>


            'Output :
            '<MS_VIEWEMPLOYEE_OUTPUT>
            '	<Employee EmployeeID='' Aoffice='' DepartmentID='' Limited_To_OwnAgency='' Limited_To_Region=''
            '		Limited_To_Aoffice='' Cell_Phone='' Login='' Email='' Employee_Name='' ManagerID='' IPRestriction=''
            '		Password='' FirstForm='' Designation='' ImmediateSupervisorID='' SecurityRegionID='' DateStart=''
            '		DateEnd='' CityId='' ChangePassword ='' PwdExpire =''AgreementSigned='' Request=''/>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_VIEWEMPLOYEE_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            'Dim onjCrypto As New AAMS.bizCryptography.bzCryptography
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strEmployeeID As String
            Const strMETHOD_NAME As String = "View"
            Dim blnRecordFound As Boolean
            Try
                objOutputXml.LoadXml(srtVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strEmployeeID = IndexDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim
                If strEmployeeID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_EMPLOYEES"
                    .Connection = objSqlConnection
                    .Parameters.Add("@EMPLOYEEID", SqlDbType.Int)
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@EMPLOYEEID").Value = strEmployeeID
                    .Parameters("@ACTION").Value = "S"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("Employee")
                        .Attributes("EmployeeID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMPLOYEEID")))
                        .Attributes("Aoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AOFFICE")))
                        .Attributes("DepartmentID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DEPARTMENTID"))) & ""
                        .Attributes("Limited_To_OwnAgency").InnerText = objSqlReader.GetValue(objSqlReader.GetOrdinal("LIMITED_TO_OWNAGENCY")) & ""
                        .Attributes("Limited_To_Region").InnerText = objSqlReader.GetValue(objSqlReader.GetOrdinal("LIMITED_TO_REGION")) & ""
                        .Attributes("Limited_To_Aoffice").InnerText = objSqlReader.GetValue(objSqlReader.GetOrdinal("LIMITED_TO_AOFFICE")) & ""
                        .Attributes("Cell_Phone").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CELL_PHONE")) & "")

                        .Attributes("LoginRequired").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LoginRequired")) & "")
                        .Attributes("Login").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOGIN")) & "")
                        '.Attributes("Password").InnerText = onjCrypto.Decrypt(Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PASSWORD")) & ""))
                        .Attributes("Password").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PASSWORD")) & "")


                        .Attributes("Email").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMAIL")) & "")
                        .Attributes("Employee_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMPLOYEE_NAME")) & "")

                        .Attributes("ManagerID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MANAGERID")) & "")
                        .Attributes("FirstForm").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FIRSTFORM")) & "")
                        .Attributes("Designation").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DESIGNATION")) & "")
                        .Attributes("ImmediateSupervisorID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("IMMEDIATESUPERVISORID")) & "")
                        .Attributes("SecurityRegionID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SECURITYREGIONID")) & "")
                        .Attributes("DateStart").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATESTART")) & "")

                        .Attributes("DateEnd").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATEEND")) & "")
                        .Attributes("CityId").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITYID")) & "")

                        .Attributes("ChangePassword").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ChangePassword")) & "")
                        .Attributes("PwdExpire").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PwdExpire")) & "")
                        .Attributes("AgreementSigned").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AgreementSigned")) & "")
                        .Attributes("Request").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Request")) & "")

                        .Attributes("GroupTypeID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("GroupTypeID")) & "")
                        .Attributes("Show_Prod_ISUPERVISOR").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Show_Prod_ISUPERVISOR")) & "")

                        .Attributes("IPRestriction").InnerText = objSqlReader.GetValue(objSqlReader.GetOrdinal("IPRESTRICTION")) & ""
                        .Attributes("IPAddress").InnerText = objSqlReader.GetValue(objSqlReader.GetOrdinal("IPAddress")) & ""

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
            '<MS_EMPLOYEE_OUTPUT>
            '   <EMPLOYEE  EmployeeID="" Employee_Name=""/>
            '  <Errors Status="False">
            '     <Error Code="" Description=""/>
            ' </Errors>
            '</MS_EMPLOYEE_OUTPUT> 

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "List"

            objOutputXml.LoadXml(strLIST_OUTPUT)

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LST_MS_EMPLOYEE"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("EMPLOYEE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("EmployeeID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMPLOYEEID")))
                    objAptNodeClone.Attributes("Employee_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NAME")))
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
        Public Function ListAssignedToEmployee() As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the airport record, based on the given field value
            'Input  : 
            'Output :  
            '<MS_EMPLOYEE_OUTPUT>
            '   <EMPLOYEE  EmployeeID="" Employee_Name=""/>
            '  <Errors Status="False">
            '     <Error Code="" Description=""/>
            ' </Errors>
            '</MS_EMPLOYEE_OUTPUT> 

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "List"

            objOutputXml.LoadXml(strLIST_OUTPUT)

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LST_MS_ASSIGNEDTO"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("EMPLOYEE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("EmployeeID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMPLOYEEID")))
                    objAptNodeClone.Attributes("Employee_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NAME")))
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

        Public Function ListManager() As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the airport record, based on the given field value
            'Input  : 
            'Output :  
            '<MS_EMPLOYEE_OUTPUT>
            '   <EMPLOYEE  EmployeeID="" Employee_Name=""/>
            '  <Errors Status="False">
            '     <Error Code="" Description=""/>
            ' </Errors>
            '</MS_EMPLOYEE_OUTPUT> 

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "ListManagerName"

            objOutputXml.LoadXml(strMANAGERLIST_OUTPUT)

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LST_MS_MANAGER"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("EMPLOYEE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("EmployeeID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMPLOYEEID")))
                    objAptNodeClone.Attributes("Employee_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NAME")))
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

        Public Function Login(ByVal IndexDoc As XmlDocument) As XmlDocument
            '***********************************************************************
            'Purpose:This function authenticates user.
            'Input  :
            '<MS_LOGIN_INPUT>
            '	<Login></Login>
            '	<Password></Password>
            '   <IPAddress></IPAddress>
            '</MS_LOGIN_INPUT>

            'Output :
            '  <MS_LOGIN_OUTPUT>
            '       <Administrator></Administrator>
            '	    <EmployeeID></EmployeeID>
            '	    <EmailID></EmailID>
            '	    <Login></Login>
            '	    <Employee_Name></Employee_Name>
            '	    <Aoffice></Aoffice>
            '	    <DepartmentID></DepartmentID>
            '	    <Designation></Designation>
            '	    <Limited_To_OwnAgency></Limited_To_OwnAgency>
            '	    <Limited_To_Aoffice></Limited_To_Aoffice>
            '	    <Limited_To_Region></Limited_To_Region>
            '	    <SecurityRegionID></SecurityRegionID>
            '	    <ManagerID></ManagerID>
            '       <Manager></Manager>
            '	    <ImmediateSupervisorID></ImmediateSupervisorID>
            '       <FirstForm></FirstForm>
            '       <ChangePassword></ChangePassword>
            '       <FirstLoginDone></FirstLoginDone>
            '       <ForceToChangePassword></ForceToChangePassword>    
            '       <Request></Request>    
            '	<SECURITY_OPTIONS>
            '		    <SECURITY_OPTION SecurityOptionID='' SecurityOptionSubname='' Value="" />
            '	</SECURITY_OPTIONS>
            '   <DisplayFirstForm>
            '	    <FirstFormDetails ID='' Name='' URL='' Module='' />
            '   </DisplayFirstForm>
            '   <HelpDesk_Default>
            '	    <HD_Default FUN_ASSIGNEDTO='' TEC_ASSIGNEDTO='' CONTACT_TYPE_ID=''/>
            '   </HelpDesk_Default>
            '   <Sales_Default>
            '	    <SL_Default TARGET_CHECK='' VISIT_TARGET_DAYS=''/>
            '   </Sales_Default>
            '	    <Errors Status="FALSE">
            '		    <Error Code="" Description="" />
            '	    </Errors>
            '</MS_LOGIN_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strLogin As String
            Dim strPassword As String
            Dim strIPAddress As String
            Dim blnRecordFound As Boolean
            Dim objAptNode As XmlNode, objAptNodeClone As XmlNode
            Dim objAptNodeNew As XmlNode, objAptNodeCloneNew As XmlNode
            'Dim onjCrypto As New AAMS.bizCryptography.bzCryptography
            Const strMETHOD_NAME As String = "login"
            Try
                objOutputXml.LoadXml(strLOGIN_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strLogin = IndexDoc.DocumentElement.SelectSingleNode("Login").InnerText.Trim
                strPassword = IndexDoc.DocumentElement.SelectSingleNode("Password").InnerText.Trim
                strIPAddress = IndexDoc.DocumentElement.SelectSingleNode("IPAddress").InnerText.Trim
                If strLogin = "" Or strPassword = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_OTH_MS_USER_AUTHENTICATION"
                    .Connection = objSqlConnection
                    .Parameters.Add("@USER_LOGIN", SqlDbType.VarChar, 25)
                    .Parameters("@USER_LOGIN").Value = strLogin

                    .Parameters.Add("@USER_PASSWORD", SqlDbType.VarChar, 100)
                    .Parameters("@USER_PASSWORD").Value = strPassword 'onjCrypto.Encrypt(strPassword)

                    .Parameters.Add("@IPADDRESS", SqlDbType.VarChar, 15)
                    .Parameters("@IPADDRESS").Value = strIPAddress

                End With
                ' RETRIVEING RECORD THROUGH STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    If blnRecordFound = False Then
                        objOutputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmployeeID")))
                        objOutputXml.DocumentElement.SelectSingleNode("EmailID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Email")) & "")
                        objOutputXml.DocumentElement.SelectSingleNode("Login").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Login")))
                        objOutputXml.DocumentElement.SelectSingleNode("Administrator").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Administrator")))
                        objOutputXml.DocumentElement.SelectSingleNode("Employee_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Employee_Name")))
                        objOutputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Aoffice")))
                        objOutputXml.DocumentElement.SelectSingleNode("DepartmentID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DepartmentID")))
                        objOutputXml.DocumentElement.SelectSingleNode("Designation").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Designation")))
                        objOutputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Limited_To_OwnAgency")))
                        objOutputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Limited_To_Aoffice")))
                        objOutputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Limited_To_Region")))
                        objOutputXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SecurityRegionID")))
                        objOutputXml.DocumentElement.SelectSingleNode("ManagerID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ManagerID")))
                        objOutputXml.DocumentElement.SelectSingleNode("ImmediateSupervisorID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ImmediateSupervisorID")))
                        objOutputXml.DocumentElement.SelectSingleNode("FirstForm").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FirstForm")))
                        objOutputXml.DocumentElement.SelectSingleNode("ChangePassword").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ChangePassword")))
                        objOutputXml.DocumentElement.SelectSingleNode("FirstLoginDone").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FirstLoginDone")))
                        objOutputXml.DocumentElement.SelectSingleNode("ForceToChangePassword").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ForceToChangePassword")))
                        objOutputXml.DocumentElement.SelectSingleNode("Request").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Request")))

                        If objOutputXml.DocumentElement.SelectSingleNode("Manager") IsNot Nothing Then
                            objOutputXml.DocumentElement.SelectSingleNode("Manager").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Manager")))
                        End If
                        If objOutputXml.DocumentElement.SelectSingleNode("DSR_TARGET_DAYS_CHECK") IsNot Nothing Then
                            objOutputXml.DocumentElement.SelectSingleNode("DSR_TARGET_DAYS_CHECK").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DSR_TARGET_DAYS_CHECK")))
                        End If
                        If objOutputXml.DocumentElement.SelectSingleNode("DSR_TARGET_DAYS") IsNot Nothing Then
                            objOutputXml.DocumentElement.SelectSingleNode("DSR_TARGET_DAYS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DSR_TARGET_DAYS")))
                        End If

                        With objOutputXml.DocumentElement.SelectSingleNode("DisplayFirstForm/FirstFormDetails")
                            .Attributes("ID").Value = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ID")) & "")
                            .Attributes("Name").Value = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("formName")) & "")
                            .Attributes("URL").Value = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("URL")) & "")
                            .Attributes("Module").Value = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Module")) & "")
                        End With

                        With objOutputXml.DocumentElement.SelectSingleNode("HelpDesk_Default/HD_Default")
                            .Attributes("FUN_ASSIGNEDTO").Value = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FUN_ASSIGNEDTO")) & "")
                            .Attributes("TEC_ASSIGNEDTO").Value = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("TEC_ASSIGNEDTO")) & "")
                            .Attributes("CONTACT_TYPE_ID").Value = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CONTACT_TYPE_ID")) & "")
                            .Attributes("TEC_CONTACT_TYPE_ID").Value = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("TEC_CONTACT_TYPE_ID")) & "")
                        End With

                        With objOutputXml.DocumentElement.SelectSingleNode("Sales_Default/SL_Default")
                            .Attributes("TARGET_CHECK").Value = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("TARGET_CHECK")) & "")
                            .Attributes("VISIT_TARGET_DAYS").Value = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("VISIT_TARGET_DAYS")) & "")
                        End With

                    End If
                    blnRecordFound = True
                Loop
                If blnRecordFound = True Then
                    objAptNode = objOutputXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION")
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS").RemoveChild(objAptNode)
                    objSqlReader.NextResult()
                    Do While objSqlReader.Read()
                        objAptNodeClone.Attributes("SecurityOptionID").Value = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SecurityOptionID")))
                        objAptNodeClone.Attributes("SecurityOptionSubName").Value = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SecurityOptionSubname")))
                        objAptNodeClone.Attributes("Value").Value = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Value")))
                        objOutputXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS").AppendChild(objAptNodeClone)
                        objAptNodeClone = objAptNode.CloneNode(True)
                    Loop
                End If
                If blnRecordFound = False Then
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "TRUE"
                    Throw (New AAMSException("Invalid Username or Password."))
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
            objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Return objOutputXml
        End Function

        Public Function ChangePassword(ByVal IndexDoc As XmlDocument) As XmlDocument
            '***********************************************************************
            'Purpose: This function will change the Old Password of Employee.
            'Input: Xmldocument
            '<MS_CHANGEPASSWORD_INPUT>
            '	<EmployeeID></EmployeeID>
            '	<OldPassword></OldPassword>
            '	<NewPassword></NewPassword>
            '</MS_CHANGEPASSWORD_INPUT>

            'Output: Xmldocument
            '<MS_CHANGEPASSWORD_OUTPUT>
            '	<Errors Status="FALSE">
            '		<Error Code="" Description="" />
            '	</Errors>
            '</MS_CHANGEPASSWORD_OUTPUT>
            '***********************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            'Dim onjCrypto As New AAMS.bizCryptography.bzCryptography
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument

            Dim intEmployeeID As Integer
            Dim strOldPassword As String
            Dim strNewPassword As String
            Dim intRetId As Integer

            Const strMETHOD_NAME As String = "ChangePassword"
            Try
                objOutputXml.LoadXml(strCHANGEPASSWORD_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                intEmployeeID = IndexDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim
                strOldPassword = IndexDoc.DocumentElement.SelectSingleNode("OldPassword").InnerText.Trim
                strNewPassword = IndexDoc.DocumentElement.SelectSingleNode("NewPassword").InnerText.Trim

                If intEmployeeID = 0 Or strOldPassword = "" Or strNewPassword = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_OTH_MS_USER_CHANGE_PASSWORD"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@EMPLOYEEID", SqlDbType.Int))
                    .Parameters("@EMPLOYEEID").Value = intEmployeeID

                    .Parameters.Add(New SqlParameter("@USER_OLD_PASSWORD", SqlDbType.Char, 100))
                    .Parameters("@USER_OLD_PASSWORD").Value = strOldPassword 'onjCrypto.Encrypt(strOldPassword)

                    .Parameters.Add(New SqlParameter("@USER_NEW_PASSWORD", SqlDbType.Char, 100))
                    .Parameters("@USER_NEW_PASSWORD").Value = strNewPassword 'onjCrypto.Encrypt(strNewPassword)

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0
                    .Connection.Open()
                    .ExecuteNonQuery()
                    intRetId = .Parameters("@RETURNID").Value
                End With

                If intRetId = -1 Then
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "TRUE"
                    Throw (New AAMSException("Invalid Username/Inactive user."))
                End If
                If intRetId = -100 Then
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "TRUE"
                    Throw (New AAMSException("New password should not be old password."))
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
            objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Return objOutputXml
        End Function

        Public Function GetPermissions(ByVal IndexDoc As XmlDocument) As XmlDocument
            '***********************************************************************
            'Purpose:This function gives details of employee permissions.
            'Input  :
            '<MS_VIEWEMPLOYEEPERMISSION_INPUT>
            '	<EmployeeID></EmployeeID>
            '</MS_VIEWEMPLOYEEPERMISSION_INPUT>
            'Output :
            '<MS_VIEWEMPLOYEEPERMISSION_OUTPUT>
            '	<SECURITY_OPTIONS>
            '		<SECURITY_OPTION SecurityOptionID="" Value="" />
            '	</SECURITY_OPTIONS>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_VIEWEMPLOYEEPERMISSION_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strEMP_ID As String
            Const strMETHOD_NAME As String = "GetPermissions"
            Dim objEMPNode, objEMPNodeClone As XmlNode
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(strPERMISSIONVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strEMP_ID = IndexDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim
                If strEMP_ID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_EMP_ACCESSLEVEL"
                    .Connection = objSqlConnection
                    .Parameters.Add("@EmployeeID", SqlDbType.Int)
                    .Parameters("@EmployeeID").Value = strEMP_ID
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                objEMPNode = objOutputXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION")
                objEMPNodeClone = objEMPNode.CloneNode(True)
                objOutputXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS").RemoveChild(objEMPNode)

                Do While objSqlReader.Read()
                    'If blnRecordFound = False Then
                    objEMPNodeClone.Attributes("SecurityOptionID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SecurityOptionID")) & "")
                    objEMPNodeClone.Attributes("Value").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Value")) & "")
                    objOutputXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS").AppendChild(objEMPNodeClone)
                    objEMPNodeClone = objEMPNode.CloneNode(True)
                    'End If
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

        Public Function SavePermissions(ByVal UpdateDoc As XmlDocument) As XmlDocument
            '***********************************************************************
            'Purpose:This function SAVE details of employee permission.
            'Input  :
            '<MS_UPDATEEMPLOYEEPERMISSION_INPUT>
            '	<EmployeeID></EmployeeID>'
            '	<SECURITY_OPTIONS>
            '		<SECURITY_OPTION SecurityOptionID="" Value="" />
            '	</SECURITY_OPTIONS>
            '</MS_UPDATEEMPLOYEEPERMISSION_INPUT>
            'Output :
            '<MS_UPDATEEMPLOYEEPERMISSION_OUTPUT>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_UPDATEEMPLOYEEPERMISSION_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objSqlCommand1 As New SqlCommand
            Dim objSqlCommand2 As New SqlCommand
            Dim objTran As SqlTransaction
            Dim objTranHistory As SqlTransaction
            Dim objOutputXml As New XmlDocument
            Dim objPermissionOutputXml As New XmlDocument
            Dim strEMP_ID, strLoginEMP_ID As String
            Dim objNode As XmlNode
            Const strMETHOD_NAME As String = "SavePermissions"
            Dim intCn As Integer = 0
            Dim objHistoryNode As XmlNode
            Dim strchangedData As String = ""
            objOutputXml.LoadXml(strPERMISSIONUPDATE_OUTPUT)

            Try
                strEMP_ID = UpdateDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim
                If UpdateDoc.DocumentElement.SelectSingleNode("LoginEmployeeID") IsNot Nothing Then
                    strLoginEMP_ID = UpdateDoc.DocumentElement.SelectSingleNode("LoginEmployeeID").InnerText.Trim
                End If

                If strEMP_ID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                'Code for history
                Dim objInputXml As New XmlDocument
                objInputXml.LoadXml("<MS_VIEWEMPLOYEEPERMISSION_INPUT><EmployeeID></EmployeeID></MS_VIEWEMPLOYEEPERMISSION_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerXml = strEMP_ID
                'Here Back end Method Call

                objPermissionOutputXml = GetPermissions(objInputXml)
                If objPermissionOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    intCn = objPermissionOutputXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION").Count
                    If intCn <> 0 Then
                        With objSqlCommand2
                            .Connection = objSqlConnection
                            .Connection.Open()
                            objTranHistory = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)

                            .Transaction = objTranHistory

                            .CommandType = CommandType.StoredProcedure
                            .CommandText = "UP_SRO_MS_EMP_ACCESSLEVEL_LOG"
                            .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                            .Parameters("@ACTION").Value = "I"
                            .Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.Int))
                            .Parameters.Add(New SqlParameter("@MODIFYBY", SqlDbType.Int))
                            .Parameters.Add(New SqlParameter("@SECURITYOPTIONID", SqlDbType.Int))
                            .Parameters.Add(New SqlParameter("@CHANGEDDATA", SqlDbType.VarChar, 1000))
                            .Parameters("@EmployeeID").Value = strEMP_ID
                            .Parameters("@MODIFYBY").Value = strLoginEMP_ID
                            For Each objHistoryNode In objPermissionOutputXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION")
                                For Each objUpdateNode As XmlNode In UpdateDoc.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionID='" + objHistoryNode.Attributes("SecurityOptionID").Value + "']")
                                    If Convert.ToString(objHistoryNode.Attributes("Value").Value).Trim <> Convert.ToString(objUpdateNode.Attributes("Value").Value).Trim Then
                                        .Parameters("@SECURITYOPTIONID").Value = objHistoryNode.Attributes("SecurityOptionID").Value
                                        strchangedData = SecurityCheck(objHistoryNode.Attributes("Value").Value) & " To " & SecurityCheck(objUpdateNode.Attributes("Value").Value)
                                        .Parameters("@CHANGEDDATA").Value = strchangedData
                                        .ExecuteNonQuery()
                                    End If
                                Next

                            Next
                            objTranHistory.Commit()
                        End With
                        If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                    End If

                End If


                'End code for history


                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_EMP_ACCESSLEVEL"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"
                    .Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.Int))
                    .Parameters("@EmployeeID").Value = strEMP_ID
                    .Connection.Open()
                    .ExecuteNonQuery()
                End With

                objTran = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)
                With objSqlCommand1
                    .Connection = objSqlConnection
                    .Transaction = objTran
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_EMP_ACCESSlEVEL"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@SecurityOptionID", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@Value", SqlDbType.Int))
                    For Each objNode In UpdateDoc.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION")
                        .Parameters("@ACTION").Value = "I"
                        .Parameters("@EmployeeID").Value = strEMP_ID
                        .Parameters("@SecurityOptionID").Value = objNode.Attributes("SecurityOptionID").InnerText
                        .Parameters("@Value").Value = objNode.Attributes("Value").InnerText
                        .ExecuteNonQuery()
                    Next
                End With
                objTran.Commit()
                objSqlConnection.Close()
                'Checking whether record is deleted successfully or not
                objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                'Fill Error Status
                If objSqlConnection.State <> ConnectionState.Closed Then
                    If Not objTranHistory Is Nothing Then
                        objTranHistory.Rollback()
                    End If
                    If Not objTran Is Nothing Then
                        objTran.Rollback()
                    End If
                    objSqlConnection.Close()
                End If
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function

        Public Function EmployeePermissionHistory(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To return employee permission history
            'Input  : 
            '<MS_HISTORY_EMPLOYEEPERMISSION_INPUT>
            '  <ACTION/><EMPLOYEEID/><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/>
            ' </MS_HISTORY_EMPLOYEEPERMISSION_INPUT>

            ' OutPut :
            ' <MS_HISTORY_EMPLOYEEPERMISSION_OUTPUT>
            '   <EMPLOYEE USER='' LOGDATE='' Sec_Group='' SEC_GROUP_ID='' SecurityOptionSubName='' CHANGEDDATA=''/>
            '   <PAGE PAGE_COUNT='' TOTAL_ROWS=''/>
            '   <Errors Status=''><Error Code='' Description=''/></Errors>
            '</MS_HISTORY_EMPLOYEEPERMISSION_OUTPUT>

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean

            ''paging
            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean
            ''end


            Dim strMETHOD_NAME As String = "EmployeeHistory"

            objOutputXml.LoadXml(strEMPLOYEEHISTORY_OUTPUT)

            Try

                ''paging
                If SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText <> "" Then
                    intPageNo = SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText <> "" Then
                    intPageSize = SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText <> "" Then
                    strSortBy = SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("DESC").InnerText.ToUpper = "TRUE" Then
                    blnDesc = True
                Else
                    blnDesc = False
                End If


                ''end paging

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_SER_MS_EMP_ACCESSLEVEL_LOG]"
                    .Connection = objSqlConnection
                    .Parameters.Add("@ACTION", SqlDbType.Char, 1)
                    .Parameters("@ACTION").Value = "S"

                    .Parameters.Add("@EMPLOYEEID", SqlDbType.Int)
                    If (SearchDoc.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText.Trim = "") Then
                        bzShared.FillErrorStatus(objOutputXml, "101", "EmployeeID is mandatory !")
                    Else
                        .Parameters("@EMPLOYEEID").Value = SearchDoc.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText.Trim
                    End If

                    'paging 
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
                    'end paging


                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("EMPLOYEE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("USER").InnerText = Convert.ToString(objSqlReader.GetValue(objSqlReader.GetOrdinal("USER"))).Trim
                    objAptNodeClone.Attributes("LOGDATE").InnerText = Convert.ToString(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOGDATE"))).Trim
                    objAptNodeClone.Attributes("Sec_Group").InnerText = Convert.ToString(objSqlReader.GetValue(objSqlReader.GetOrdinal("Sec_Group"))).Trim
                    objAptNodeClone.Attributes("SEC_GROUP_ID").InnerText = Convert.ToString(objSqlReader.GetValue(objSqlReader.GetOrdinal("SEC_GROUP_ID"))).Trim
                    objAptNodeClone.Attributes("SecurityOptionSubName").InnerText = Convert.ToString(objSqlReader.GetValue(objSqlReader.GetOrdinal("SecurityOptionSubName"))).Trim
                    objAptNodeClone.Attributes("CHANGEDDATA").InnerText = Convert.ToString(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHANGEDDATA"))).Trim
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop
                objSqlReader.Close()

                If (blnRecordFound = False) Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").InnerText = objSqlCommand.Parameters("@TOTALROWS").Value
                    If intPageSize = 0 Then
                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = 0
                    Else
                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = Ceiling(objSqlCommand.Parameters("@TOTALROWS").Value / intPageSize)
                    End If
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

        Public Function GetGroupAssigned(ByVal IndexDoc As XmlDocument) As XmlDocument
            '***********************************************************************
            'Purpose:This function gives details of City.
            'Input  :
            '<MS_VIEWEMPLOYEEGROUP_INPUT>
            '	<EmployeeID></EmployeeID>
            '</MS_VIEWEMPLOYEEGROUP_INPUT>

            'Output :
            '<MS_VIEWEMPLOYEEGROUP_OUTPUT>
            '	<Chain_Code></Chain_Code>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_VIEWEMPLOYEEGROUP_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strEMP_ID As String
            Const strMETHOD_NAME As String = "GetGroupAssigned"
            Dim objEMPNode, objEMPNodeClone As XmlNode
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(srtEMPGROUPVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strEMP_ID = IndexDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim
                If strEMP_ID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_EMP_AGENCY_GROUP"
                    .Connection = objSqlConnection
                    .Parameters.Add("@EmployeeID", SqlDbType.Int)
                    .Parameters("@EmployeeID").Value = strEMP_ID
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                objEMPNode = objOutputXml.DocumentElement.SelectSingleNode("Chain_Code")
                objEMPNodeClone = objEMPNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objEMPNode)

                Do While objSqlReader.Read()
                    objEMPNodeClone.InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Chain_Code")))
                    objOutputXml.DocumentElement.AppendChild(objEMPNodeClone)
                    objEMPNodeClone = objEMPNode.CloneNode(True)
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

        Public Function SaveGroupAssigned(ByVal UpdateDoc As XmlDocument) As XmlDocument
            '***********************************************************************
            'Purpose:This function SAVE details of GROUP.
            'Input  :
            '<MS_UPDATEEMPLOYEESUPERVISORY_INPUT>
            '	<EmployeeID></EmployeeID>
            '	<Supervisory DomainID='' DomainName='' />
            '</MS_UPDATEEMPLOYEESUPERVISORY_INPUT>
            'Output :
            '<MS_UPDATEEMPLOYEESUPERVISORY_OUTPUT>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_UPDATEEMPLOYEESUPERVISORY_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objSqlCommand1 As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objTran As SqlTransaction
            Dim strEMP_ID As String
            Dim objNode As XmlNode
            Dim objSqlCommandHistory As New SqlCommand
            Const strMETHOD_NAME As String = "SaveGroupAssigned"
            Dim intNoRecordsAffected As Integer

            objOutputXml.LoadXml(srtEMPGROUPUPDATE_OUTPUT)

            Try
                strEMP_ID = UpdateDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim
                If strEMP_ID = "" Then Throw (New AAMSException("Incomplete Parameters"))


                ' objSqlCommand.Connection.Open()
                objSqlConnection.Open()
                objTran = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)

                'Command for History
                With objSqlCommandHistory
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .Transaction = objTran
                    .CommandText = "UP_SRO_MS_EMPLOYEE_HISTORY"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "I"

                    .Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.Int))
                    .Parameters("@EmployeeID").Value = strEMP_ID

                    .Parameters.Add(New SqlParameter("@CHANGEDBY", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("ChangedBy").InnerText <> "" Then
                        .Parameters("@CHANGEDBY").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("ChangedBy").InnerText)
                    Else
                        .Parameters("@CHANGEDBY").Value = DBNull.Value
                    End If

                    .Parameters.Add(New SqlParameter("@HISTORYID", SqlDbType.Int))
                    .Parameters("@HISTORYID").Value = 2

                    .Parameters.Add(New SqlParameter("@INPUTXML", SqlDbType.Xml))
                    .Parameters("@INPUTXML").Value = UpdateDoc.OuterXml

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0

                End With
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_EMP_AGENCY_GROUP"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"
                    .Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.Int))
                    .Parameters("@EmployeeID").Value = strEMP_ID
                    .Transaction = objTran
                    '.ExecuteNonQuery()
                End With

                With objSqlCommand1
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_EMP_AGENCY_GROUP"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@Chain_Code", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@INPUTXML", SqlDbType.Xml))
                End With

                'Added by Tapan Nath
                With objSqlCommand1
                    .Parameters("@ACTION").Value = "I"
                    .Parameters("@EmployeeID").Value = strEMP_ID
                    .Parameters("@Chain_Code").Value = DBNull.Value 'objNode.InnerText
                    .Parameters("@INPUTXML").Value = UpdateDoc.OuterXml
                    .Transaction = objTran
                    '.ExecuteNonQuery()
                End With

                intNoRecordsAffected = objSqlCommandHistory.ExecuteNonQuery
                intNoRecordsAffected = objSqlCommand.ExecuteNonQuery
                intNoRecordsAffected = objSqlCommand1.ExecuteNonQuery

                'For Each objNode In UpdateDoc.DocumentElement.SelectNodes("Chain_Code")
                '    With objSqlCommand1
                '        .Parameters("@ACTION").Value = "I"
                '        .Parameters("@EmployeeID").Value = strEMP_ID
                '        .Parameters("@Chain_Code").Value = objNode.InnerText
                '        .Parameters("@Chain_Code").Value = ""
                '        .ExecuteNonQuery()
                '    End With
                'Next
                objTran.Commit()
                objSqlConnection.Close()
                'Checking whether record is deleted successfully or not
                objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
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
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function
        Public Function GetAssignedIP(ByVal IndexDoc As XmlDocument) As XmlDocument
            '***********************************************************************
            'Purpose:This function gives details of City.
            'Input  :
            '<MS_VIEWEMPLOYEEIP_INPUT>
            '	<EmployeeID></EmployeeID>
            '</MS_VIEWEMPLOYEEIP_INPUT>
            'Output :
            '<MS_VIEWEMPLOYEEIP_OUTPUT>
            '	<IPAddress IP=''></IPAddress>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_VIEWEMPLOYEEIP_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader

            Dim strEMP_ID As String, intIPRestrict As Integer
            Const strMETHOD_NAME As String = "GetAssignedIP"
            Dim objEMPNode, objEMPNodeClone As XmlNode
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(srtIPVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strEMP_ID = IndexDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim
                If strEMP_ID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_EMP_PERMITTED_IP"
                    .Connection = objSqlConnection
                    .Parameters.Add("@EmployeeID", SqlDbType.Int)
                    .Parameters("@EmployeeID").Value = strEMP_ID
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                objEMPNode = objOutputXml.DocumentElement.SelectSingleNode("IPAddress")
                objEMPNodeClone = objEMPNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objEMPNode)

                Do While objSqlReader.Read()
                    If Not IsDBNull(objSqlReader.GetValue(objSqlReader.GetOrdinal("IPRESTRICTION"))) Then
                        If (objSqlReader.GetValue(objSqlReader.GetOrdinal("IPRESTRICTION")) & "") = True Then
                            intIPRestrict = 1
                        Else
                            intIPRestrict = 0
                        End If
                    Else
                        intIPRestrict = 0
                    End If

                    If Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("IPAddress")) & "") <> "" Then
                        objEMPNodeClone.Attributes("IP").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("IPAddress")) & "")
                        objOutputXml.DocumentElement.AppendChild(objEMPNodeClone)
                        objEMPNodeClone = objEMPNode.CloneNode(True)
                    End If
                    blnRecordFound = True
                Loop
                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    objOutputXml.DocumentElement.SelectSingleNode("IPRESTRICTION").InnerText = intIPRestrict
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

        Public Function AssignIP(ByVal UpdateDoc As XmlDocument) As XmlDocument
            '***********************************************************************
            'Purpose:This function SAVE details of GROUP.
            'Input  :
            '<MS_UPDATEEMPLOYEEIP_INPUT>
            '	<IPAddress IP=''></IPAddress> <IPRESTRICTION>1</IPRESTRICTION>
            '</MS_UPDATEEMPLOYEEIP_INPUT>
            'Output :
            '<MS_UPDATEEMPLOYEEIP_OUTPUT>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_UPDATEEMPLOYEEIP_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objSqlCommand1 As New SqlCommand
            Dim objSqlCommandHistory As New SqlCommand
            Dim objTran As SqlTransaction
            Dim objOutputXml As New XmlDocument
            Dim strEMP_ID As String
            Dim objNode As XmlNode
            Const strMETHOD_NAME As String = "AssignIP"

            objOutputXml.LoadXml(srtIPUPDATE_OUTPUT)

            Try
                strEMP_ID = UpdateDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim
                If strEMP_ID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                If UpdateDoc.DocumentElement.SelectSingleNode("ChangedBy").InnerText.Trim = "" Then Throw (New AAMSException("Incomplete Parameters"))

                objSqlConnection.Open()
                objTran = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)

                'History
                With objSqlCommandHistory
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .Transaction = objTran
                    .CommandText = "UP_SRO_MS_EMPLOYEE_HISTORY"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "I"

                    .Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.Int))
                    .Parameters("@EmployeeID").Value = strEMP_ID

                    .Parameters.Add(New SqlParameter("@CHANGEDBY", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("ChangedBy").InnerText <> "" Then
                        .Parameters("@CHANGEDBY").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("ChangedBy").InnerText.Trim)
                    Else
                        .Parameters("@CHANGEDBY").Value = DBNull.Value
                    End If

                    .Parameters.Add(New SqlParameter("@HISTORYID", SqlDbType.Int))
                    .Parameters("@HISTORYID").Value = 3

                    .Parameters.Add(New SqlParameter("@INPUTXML", SqlDbType.Xml))
                    .Parameters("@INPUTXML").Value = UpdateDoc.OuterXml

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0

                End With
                'History



                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .Transaction = objTran
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_EMP_PERMITTED_IP"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"
                    .Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.Int))
                    .Parameters("@EmployeeID").Value = strEMP_ID
                    'objSqlCommand.Connection.Open()
                    '.ExecuteNonQuery()
                End With

                With objSqlCommand1
                    .Connection = objSqlConnection
                    .Transaction = objTran
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_EMP_PERMITTED_IP"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@IPAddress", SqlDbType.VarChar, 15))
                    .Parameters.Add(New SqlParameter("@IPRESTRICTION", SqlDbType.Bit))
                    If Val(UpdateDoc.DocumentElement.SelectSingleNode("IPRESTRICTION").InnerText & "") = 0 Then
                        .Parameters("@IPRESTRICTION").Value = 0
                    Else
                        .Parameters("@IPRESTRICTION").Value = 1 'UpdateDoc.DocumentElement.SelectSingleNode("IPRESTRICTION").InnerText
                    End If
                End With

                objSqlCommandHistory.ExecuteNonQuery()
                objSqlCommand.ExecuteNonQuery()
                For Each objNode In UpdateDoc.DocumentElement.SelectNodes("IPAddress")
                    With objSqlCommand1
                        .Parameters("@ACTION").Value = "I"
                        .Parameters("@EmployeeID").Value = strEMP_ID
                        .Parameters("@IPAddress").Value = objNode.Attributes("IP").InnerText
                        .ExecuteNonQuery()
                    End With
                Next



                objTran.Commit()
                objSqlConnection.Close()
                'Checking whether record is deleted successfully or not
                objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
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
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function

        Public Function GetSupervisoryRights(ByVal IndexDoc As XmlDocument) As XmlDocument
            '***********************************************************************
            'Purpose:This function gives details of City.
            'Input  :
            '<MS_VIEWEMPLOYEESUPERVISORY_INPUT>
            '	<EmployeeID></EmployeeID>
            '</MS_VIEWEMPLOYEESUPERVISORY_INPUT>
            'Output :
            '<MS_VIEWEMPLOYEESUPERVISORY_OUTPUT>
            '	<Supervisory DomainID='' DomainName='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_VIEWEMPLOYEESUPERVISORY_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strEMP_ID As String
            Const strMETHOD_NAME As String = "GetSupervisoryRights"
            Dim objEMPNode, objEMPNodeClone As XmlNode
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(srtGROUPVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strEMP_ID = IndexDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim
                If strEMP_ID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_EMP_SUPERVISOR_ACCESS"
                    .Connection = objSqlConnection
                    .Parameters.Add("@EmployeeID", SqlDbType.Int)
                    .Parameters("@EmployeeID").Value = strEMP_ID
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                objEMPNode = objOutputXml.DocumentElement.SelectSingleNode("Supervisory")
                objEMPNodeClone = objEMPNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objEMPNode)

                Do While objSqlReader.Read()

                    objEMPNodeClone.Attributes("DomainID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DomainID")))
                    objEMPNodeClone.Attributes("DomainName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DomainName")))
                    objOutputXml.DocumentElement.AppendChild(objEMPNodeClone)
                    objEMPNodeClone = objEMPNode.CloneNode(True)

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

        Public Function SaveSupervisoryRights(ByVal UpdateDoc As XmlDocument) As XmlDocument
            '***********************************************************************
            'Purpose:This function SAVE details of GROUP.
            'Input  :
            '<MS_UPDATEEMPLOYEESUPERVISORY_INPUT>
            '	<EmployeeID></EmployeeID>
            '	<Supervisory DomainID='' DomainName='' />
            '</MS_UPDATEEMPLOYEESUPERVISORY_INPUT>
            'Output :
            '<MS_UPDATEEMPLOYEESUPERVISORY_OUTPUT>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_UPDATEEMPLOYEESUPERVISORY_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objSqlCommand1 As New SqlCommand
            Dim objSqlCommandHistory As New SqlCommand
            Dim objTran As SqlTransaction
            Dim objOutputXml As New XmlDocument
            Dim strEMP_ID As String
            Dim objNode As XmlNode
            Const strMETHOD_NAME As String = "SaveGroupAssigned"

            objOutputXml.LoadXml(srtGROUPUPDATE_OUTPUT)

            Try
                strEMP_ID = UpdateDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim
                If strEMP_ID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                If UpdateDoc.DocumentElement.SelectSingleNode("ChangedBy").InnerText = "" Then Throw (New AAMSException("Incomplete Parameters"))


                objSqlConnection.Open()
                objTran = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)

                'Command for History
                With objSqlCommandHistory
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .Transaction = objTran
                    .CommandText = "UP_SRO_MS_EMPLOYEE_HISTORY"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "I"

                    .Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.Int))
                    .Parameters("@EmployeeID").Value = strEMP_ID

                    .Parameters.Add(New SqlParameter("@CHANGEDBY", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("ChangedBy").InnerText.Trim  <> "" Then
                        .Parameters("@CHANGEDBY").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("ChangedBy").InnerText.Trim)
                    Else
                        .Parameters("@CHANGEDBY").Value = DBNull.Value
                    End If

                    .Parameters.Add(New SqlParameter("@HISTORYID", SqlDbType.Int))
                    .Parameters("@HISTORYID").Value = 4

                    .Parameters.Add(New SqlParameter("@INPUTXML", SqlDbType.Xml))
                    .Parameters("@INPUTXML").Value = UpdateDoc.OuterXml

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0
                    .ExecuteNonQuery()
                End With
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .Transaction = objTran
                    .CommandText = "UP_SRO_EMP_SUPERVISOR_ACCESS"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"
                    .Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.Int))
                    .Parameters("@EmployeeID").Value = strEMP_ID
                    'objSqlCommand.Connection.Open()
                    .ExecuteNonQuery()
                End With
                With objSqlCommand1
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_EMP_SUPERVISOR_ACCESS"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@DomainID", SqlDbType.Int))
                End With

                For Each objNode In UpdateDoc.DocumentElement.SelectNodes("Supervisory")
                    With objSqlCommand1
                        .Parameters("@ACTION").Value = "I"
                        .Parameters("@EmployeeID").Value = strEMP_ID
                        .Parameters("@DomainID").Value = objNode.Attributes("DomainID").InnerText
                        .Transaction = objTran
                        .ExecuteNonQuery()
                    End With
                Next
                objTran.Commit()
                objSqlConnection.Close()
                'Checking whether record is deleted successfully or not
                objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
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
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
                objSqlCommand1.Dispose()
                objSqlCommandHistory.Dispose()
            End Try
            Return objOutputXml
        End Function
        Public Function ListDepartmentEmployees(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the Employees accroding to specified Department
            'Input  : 
            '<MS_SEARCHDEPARTMENTEMPLOYEE_INPUT>
            '	<Employee_Type></Employee_Type>
            '	<Aoffice></Aoffice>
            '</MS_SEARCHDEPARTMENTEMPLOYEE_INPUT>

            'Output :  
            '<MS_EMPLOYEE_OUTPUT>
            '   <EMPLOYEE  EmployeeID="" Employee_Name="" DepartmentName=''/>
            '  <Errors Status="False">
            '     <Error Code="" Description=""/>
            ' </Errors>
            '</MS_EMPLOYEE_OUTPUT> 

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strEmployeeType As String
            Dim strAoffice As String

            Dim strMETHOD_NAME As String = "ListDepartmentEmployees"

            objOutputXml.LoadXml(strLIST_OUTPUT)

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_LST_MS_DEPARMENT_EMPLOYEE]"
                    .Connection = objSqlConnection

                    .Parameters.Add("@Employee_Type", SqlDbType.VarChar, 100)
                    If (SearchDoc.DocumentElement.SelectSingleNode("Employee_Type").InnerText.Trim = "") Then
                        .Parameters("@Employee_Type").Value = DBNull.Value
                    Else
                        .Parameters("@Employee_Type").Value = SearchDoc.DocumentElement.SelectSingleNode("Employee_Type").InnerText.Trim
                    End If

                    .Parameters.Add("@Aoffice", SqlDbType.Char, 3)
                    If (SearchDoc.DocumentElement.SelectSingleNode("Aoffice").InnerText.Trim = "") Then
                        .Parameters("@Aoffice").Value = DBNull.Value
                    Else
                        .Parameters("@Aoffice").Value = SearchDoc.DocumentElement.SelectSingleNode("Aoffice").InnerText.Trim
                    End If
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("EMPLOYEE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("EmployeeID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMPLOYEEID")))
                    objAptNodeClone.Attributes("Employee_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NAME")))
                    objAptNodeClone.Attributes("DepartmentName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DEPARTMENTNAME")))
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

        Public Function UpdateRegistrationID(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function Inserts/Updates registrationid.
            'Input  :
            '<MS_UPDATEREGISTRATIONID_INPUT>
            '	<REGISTRATION ACTION='' LCODE='' USERNAME='' PASSWORD='' TRAININGSTATUS='' />
            '</MSI_UPDATEREGISTRATIONID_INPUT>

            'Output :
            '<MS_UPDATEREGISTRATIONID_OUTPUT>
            '<REGISTRATION LCODE='' USERNAME='' PASSWORD='' TRAININGSTATUS=''/>
            '<Errors Status="FALSE">
            '	<Error Code="" Description=""/>
            '</Errors>
            '</MS_UPDATEREGISTRATIONID_OUTPUT>
            '************************************************************************
            Dim intRetId As String = ""
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objSqlReader As SqlDataReader
            Dim objUpdateDocOutput As New XmlDocument


            Dim strLcode As String
            Dim strUserName As String
            Dim strPASSWORD As String
            Dim strTrnStatus As String


            Dim blnRecordFound As Boolean

            Dim intRecordsAffected As Int32
            Const strMETHOD_NAME As String = "UpdateRegistrationID"
            Try
                objUpdateDocOutput.LoadXml(strREGISTRATIONID_OUTPUT)

                With objUpdateDocOutput.DocumentElement.SelectSingleNode("REGISTRATION")
                    .Attributes("LCODE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("REGISTRATION").Attributes("LCODE").InnerText
                    .Attributes("USERNAME").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("REGISTRATION").Attributes("USERNAME").InnerText
                    .Attributes("PASSWORD").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("REGISTRATION").Attributes("PASSWORD").InnerText
                    .Attributes("TRAININGSTATUS").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("REGISTRATION").Attributes("TRAININGSTATUS").InnerText
                End With

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("REGISTRATION")
                    strAction = ((.Attributes("ACTION").InnerText).Trim).ToString

                    strLcode = ((.Attributes("LCODE").InnerText).Trim).ToString
                    strUserName = ((.Attributes("USERNAME").InnerText).Trim).ToString
                    strPASSWORD = ((.Attributes("PASSWORD").InnerText).Trim).ToString
                    strTrnStatus = ((.Attributes("TRAININGSTATUS").InnerText).Trim).ToString

                    If strAction = "I" Or strAction = "U" Or strAction = "S" Then
                        If strAction = "I" Or strAction = "U" Then
                            If strLcode = "" Then
                                Throw (New AAMSException("Location Code can't be blank."))
                            End If
                            If strUserName = "" Then
                                Throw (New AAMSException("UserName can't be blank."))
                            End If
                            If strPASSWORD = "" Then
                                Throw (New AAMSException("Password Name can't be blank."))
                            End If
                        End If
                        If strAction = "S" Then
                            If strLcode = "" Then
                                Throw (New AAMSException("Location Code can't be blank."))
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
                    .CommandText = "UP_SRO_MS_REGISTRATIONID"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction

                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.BigInt))
                    .Parameters("@LCODE").Value = strLcode

                    .Parameters.Add(New SqlParameter("@USERNAME", SqlDbType.VarChar, 100))
                    .Parameters("@USERNAME").Value = strUserName

                    .Parameters.Add(New SqlParameter("@PASSWORD", SqlDbType.VarChar, 100))
                    .Parameters("@PASSWORD").Value = strPASSWORD

                    .Parameters.Add(New SqlParameter("@TRAININGSTATUS", SqlDbType.Int))
                    If strTrnStatus = "" Then
                        .Parameters("@TRAININGSTATUS").Value = DBNull.Value
                    Else
                        .Parameters("@TRAININGSTATUS").Value = CInt(strTrnStatus)
                    End If

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output

                    .Parameters("@RETURNID").Value = 0

                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED
                    .Connection.Open()
                    If UCase(strAction) = "S" Then
                        objSqlReader = objSqlCommand.ExecuteReader()
                    Else
                        intRecordsAffected = .ExecuteNonQuery()
                    End If


                    If UCase(strAction) = "I" Then
                        intRetId = .Parameters("@RETURNID").Value
                        If intRetId = -1 Then
                            Throw (New AAMSException("User name already exists in other Agency!"))
                        ElseIf intRetId = 0 Then
                            Throw (New AAMSException("User Name Already Exists!"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("REGISTRATION").Attributes("LCODE").InnerText = strLcode
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("REGISTRATION").Attributes("USERNAME").InnerText = strUserName
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("REGISTRATION").Attributes("PASSWORD").InnerText = strPASSWORD
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("REGISTRATION").Attributes("TRAININGSTATUS").InnerText = strTrnStatus

                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                        End If

                    ElseIf UCase(strAction) = "U" Then
                        intRetId = .Parameters("@RETURNID").Value
                        If intRetId = -1 Then
                            Throw (New AAMSException("User name already exists in other Agency!"))
                        End If
                        If intRetId = 0 Then
                            Throw (New AAMSException("UserName Already Exists !"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                        End If
                    ElseIf UCase(strAction) = "S" Then
                        Do While objSqlReader.Read()
                            blnRecordFound = True
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("REGISTRATION").Attributes("LCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")) & "")
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("REGISTRATION").Attributes("USERNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("USERNAME")) & "")
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("REGISTRATION").Attributes("PASSWORD").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PASSWORD")) & "")
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("REGISTRATION").Attributes("TRAININGSTATUS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("TR_STATUS_NAME")) & "")
                        Loop
                        If blnRecordFound = False Then
                            Throw (New AAMSException("No Record found !"))
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
        Public Function ListFirstForm() As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the airport record, based on the given field value
            'Input  : 
            'Output :  
            '<MS_EMPLOYEE_FIRSTFORM_OUTPUT>
            '	<EMPLOYEE_FIRSTFORM MODULE='' DISPLAYFORMNAME='' FORMNAME='' URL=''/>
            '	<Errors Status=''>
            '		<Error Code='' Description=''/>
            '	</Errors>
            '</MS_EMPLOYEE_FIRSTFORM_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "ListFirstForm"

            objOutputXml.LoadXml(strFIRSTFORM_OUTPUT)

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_LST_MS_FIRSTFORM]"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("EMPLOYEE_FIRSTFORM")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("MODULE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MODULE")) & "")
                    objAptNodeClone.Attributes("DISPLAYFORMNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DISPLAYFORMNAME")) & "")
                    objAptNodeClone.Attributes("FORMNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FORMNAME")) & "")
                    objAptNodeClone.Attributes("URL").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("URL")) & "")
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

        Public Function SecurityCheck(ByVal intValue As Integer) As String
            Dim strReturnValue As String = ""
            Dim ViewRight, AddRight, ModifyRight, DeleteRight, PrintRight As String
            ViewRight = 0 : AddRight = 0 : ModifyRight = 0 : DeleteRight = 0 : PrintRight = 0


            Select Case intValue
                Case 1
                    ViewRight = "1"
                Case 2, 3
                    ViewRight = "1"
                    AddRight = "1"
                Case 4, 5
                    ViewRight = "1"
                    ModifyRight = "1"
                Case 6, 7
                    ViewRight = "1"
                    ModifyRight = "1"
                    AddRight = "1"
                Case 8, 9
                    ViewRight = "1"
                    DeleteRight = "1"
                Case 10, 11
                    ViewRight = "1"
                    DeleteRight = "1"
                    AddRight = "1"
                Case 12, 13
                    ViewRight = "1"
                    DeleteRight = "1"
                    ModifyRight = "1"
                Case 14, 15
                    ViewRight = "1"
                    DeleteRight = "1"
                    ModifyRight = "1"
                    AddRight = "1"
                Case 16, 17
                    ViewRight = "1"
                    PrintRight = "1"
                Case 18, 19
                    ViewRight = "1"
                    PrintRight = "1"
                    AddRight = "1"
                Case 20, 21
                    ViewRight = "1"
                    PrintRight = "1"
                    ModifyRight = "1"
                Case 22, 23
                    ViewRight = "1"
                    PrintRight = "1"
                    AddRight = "1"
                    ModifyRight = "1"
                Case 24, 25
                    ViewRight = "1"
                    PrintRight = "1"
                    DeleteRight = "1"
                Case 26, 27
                    ViewRight = "1"
                    PrintRight = "1"
                    DeleteRight = "1"
                    AddRight = "1"
                Case 28, 29
                    ViewRight = "1"
                    PrintRight = "1"
                    DeleteRight = "1"
                    ModifyRight = "1"
                Case 30, 31
                    ViewRight = "1"
                    PrintRight = "1"
                    DeleteRight = "1"
                    ModifyRight = "1"
                    AddRight = "1"
            End Select

            If ViewRight = 1 Then
                strReturnValue = "View,"
            End If
            If AddRight = 1 Then
                strReturnValue = strReturnValue & "Add,"
            End If
            If ModifyRight = 1 Then
                strReturnValue = strReturnValue & "Modify,"
            End If
            If DeleteRight = 1 Then
                strReturnValue = strReturnValue & "Delete,"
            End If
            If PrintRight = 1 Then
                strReturnValue = strReturnValue & "Print,"
            End If

            If strReturnValue <> "" Then
                strReturnValue = Left(strReturnValue, strReturnValue.Length - 1)
            End If

            Return strReturnValue
        End Function

        Public Function UpdateHDDefault(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            '<MS_UPDATEEMPLOYEE_HDDEFAULT_INPUT>
            '	<EMPLOYEE_HDDEFAULT ACTION='' EMPLOYEEID='' FUN_ASSIGNEDTO='' TEC_ASSIGNEDTO='' CONTACT_TYPE_ID='' TEC_CONTACT_TYPE_ID='' />
            '</MS_UPDATEEMPLOYEE_HDDEFAULT_INPUT>


            '<MS_UPDATEEMPLOYEE_HDDEFAULT_OUTPUT>
            '	<EMPLOYEE_HDDEFAULT ACTION='' EMPLOYEEID='' FUN_ASSIGNEDTO='' TEC_ASSIGNEDTO='' CONTACT_TYPE_ID='' TEC_CONTACT_TYPE_ID=''/>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_UPDATEEMPLOYEE_HDDEFAULT_OUTPUT>
            '************************************************************************
            Const strUPDATEHDDefault_OUTPUT = "<MS_UPDATEEMPLOYEE_HDDEFAULT_OUTPUT><EMPLOYEE_HDDEFAULT ACTION='' EMPLOYEEID='' FUN_ASSIGNEDTO='' TEC_ASSIGNEDTO='' CONTACT_TYPE_ID='' TEC_CONTACT_TYPE_ID=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEEMPLOYEE_HDDEFAULT_OUTPUT>"
            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objSqlCommandHistory As New SqlCommand
            Dim objTran As SqlTransaction
            Dim objUpdateDocOutput As New XmlDocument
            Dim intRecordsAffected As Int32
            Const strMETHOD_NAME As String = "Update"
            If UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT").Attributes("EMPLOYEEID").InnerText = "" Then Throw (New AAMSException("Incomplete Parameter."))
            If UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT").Attributes("ChangedBy").InnerText = "" Then Throw (New AAMSException("Incomplete Parameter."))
            Try
                objUpdateDocOutput.LoadXml(strUPDATEHDDefault_OUTPUT)
                With objUpdateDocOutput.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT")
                    .Attributes("ACTION").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT").Attributes("ACTION").InnerText
                    .Attributes("EMPLOYEEID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT").Attributes("EMPLOYEEID").InnerText
                    .Attributes("FUN_ASSIGNEDTO").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT").Attributes("FUN_ASSIGNEDTO").InnerText
                    .Attributes("TEC_ASSIGNEDTO").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT").Attributes("TEC_ASSIGNEDTO").InnerText
                    .Attributes("CONTACT_TYPE_ID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT").Attributes("CONTACT_TYPE_ID").InnerText
                    .Attributes("TEC_CONTACT_TYPE_ID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT").Attributes("TEC_CONTACT_TYPE_ID").InnerText
                End With
                'Retrieving & Checking Details from Input XMLDocument
                strAction = UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT").Attributes("ACTION").InnerText.Trim
                If Not (strAction = "I" Or strAction = "U") Then Throw (New AAMSException("Invalid Action Code."))
                'ADDING PARAMETERS IN STORED PROCEDURE

                objSqlConnection.Open()
                objTran = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)
                'Command for History
                With objSqlCommandHistory
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .Transaction = objTran
                    .CommandText = "UP_SRO_MS_EMPLOYEE_HISTORY"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "I"

                    .Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.Int))
                    .Parameters("@EmployeeID").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT").Attributes("EMPLOYEEID").InnerText)

                    .Parameters.Add(New SqlParameter("@CHANGEDBY", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT").Attributes("ChangedBy").InnerText = "" Then
                        .Parameters("@CHANGEDBY").Value = DBNull.Value
                    Else
                        .Parameters("@CHANGEDBY").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT").Attributes("ChangedBy").InnerText)
                    End If

                    .Parameters.Add(New SqlParameter("@HISTORYID", SqlDbType.Int))
                    .Parameters("@HISTORYID").Value = 5

                    .Parameters.Add(New SqlParameter("@INPUTXML", SqlDbType.Xml))
                    .Parameters("@INPUTXML").Value = UpdateDoc.OuterXml

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0
                End With
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_HD_EMPLOYEEDEFAULT"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction

                    .Parameters.Add(New SqlParameter("@EMPLOYEEID", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT").Attributes("EMPLOYEEID").InnerText = "" Then
                        .Parameters("@EMPLOYEEID").Value = DBNull.Value
                    Else
                        .Parameters("@EMPLOYEEID").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT").Attributes("EMPLOYEEID").InnerText)
                    End If

                    .Parameters.Add(New SqlParameter("@FUN_ASSIGNEDTO", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT").Attributes("FUN_ASSIGNEDTO").InnerText = "" Then
                        .Parameters("@FUN_ASSIGNEDTO").Value = DBNull.Value
                    Else
                        .Parameters("@FUN_ASSIGNEDTO").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT").Attributes("FUN_ASSIGNEDTO").InnerText)
                    End If

                    .Parameters.Add(New SqlParameter("@TEC_ASSIGNEDTO", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT").Attributes("TEC_ASSIGNEDTO").InnerText = "" Then
                        .Parameters("@TEC_ASSIGNEDTO").Value = DBNull.Value
                    Else
                        .Parameters("@TEC_ASSIGNEDTO").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT").Attributes("TEC_ASSIGNEDTO").InnerText)
                    End If


                    .Parameters.Add(New SqlParameter("@CONTACT_TYPE_ID", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT").Attributes("CONTACT_TYPE_ID").InnerText = "" Then
                        .Parameters("@CONTACT_TYPE_ID").Value = DBNull.Value
                    Else
                        .Parameters("@CONTACT_TYPE_ID").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT").Attributes("CONTACT_TYPE_ID").InnerText)
                    End If

                    .Parameters.Add(New SqlParameter("@TEC_CONTACT_TYPE_ID", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT").Attributes("TEC_CONTACT_TYPE_ID").InnerText = "" Then
                        .Parameters("@TEC_CONTACT_TYPE_ID").Value = DBNull.Value
                    Else
                        .Parameters("@TEC_CONTACT_TYPE_ID").Value = CInt(UpdateDoc.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT").Attributes("TEC_CONTACT_TYPE_ID").InnerText)
                    End If

                    'TEC_CONTACT_TYPE_ID
                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0

                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED
                    '.Connection.Open()
                    .Transaction = objTran
                    If strAction = "U" Then intRecordsAffected = objSqlCommandHistory.ExecuteNonQuery()
                    intRecordsAffected = .ExecuteNonQuery()
                    intRetId = .Parameters("@RETURNID").Value
                    If intRetId <= 0 Then
                        If strAction = "I" Then Throw (New AAMSException("Unable to insert!"))
                        If strAction = "U" Then Throw (New AAMSException("Unable to update!"))
                    Else
                        objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    End If
                End With
                objTran.Commit()
                objSqlConnection.Close()

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
                objSqlCommandHistory.Dispose()
            End Try
            Return objUpdateDocOutput

        End Function

        Public Function ViewHDDefault(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function gives details of Employee.
            '<MS_VIEWEMPLOYEE_HDDEFAULT_INPUT>
            '	<EMPLOYEEID></EMPLOYEEID>
            '</MS_VIEWEMPLOYEE_HDDEFAULT_INPUT>

            '<MS_VIEWEMPLOYEE_HDDEFAULT_OUTPUT>
            '	<EMPLOYEE_HDDEFAULT EMPLOYEEID='' FUN_ASSIGNEDTO='' TEC_ASSIGNEDTO='' CONTACT_TYPE_ID='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_VIEWEMPLOYEE_HDDEFAULT_OUTPUT>
            '************************************************************************
            Const strVIEWHDDefault_OUTPUT = "<MS_VIEWEMPLOYEE_HDDEFAULT_OUTPUT><EMPLOYEE_HDDEFAULT EMPLOYEEID='' FUN_ASSIGNEDTO='' TEC_ASSIGNEDTO='' CONTACT_TYPE_ID='' TEC_CONTACT_TYPE_ID=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWEMPLOYEE_HDDEFAULT_OUTPUT>"
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strEMPLOYEEID As String = ""
            Const strMETHOD_NAME As String = "ViewHDDefault"
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(strVIEWHDDefault_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strEMPLOYEEID = IndexDoc.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText.Trim
                If strEMPLOYEEID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_HD_EMPLOYEEDEFAULT"
                    .Connection = objSqlConnection
                    .Parameters.Add("@EMPLOYEEID", SqlDbType.Int)
                    .Parameters("@EMPLOYEEID").Value = strEMPLOYEEID

                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT")
                        .Attributes("EMPLOYEEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMPLOYEEID")))
                        .Attributes("FUN_ASSIGNEDTO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FUN_ASSIGNEDTO")) & "")
                        .Attributes("TEC_ASSIGNEDTO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("TEC_ASSIGNEDTO")) & "")

                        .Attributes("CONTACT_TYPE_ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CONTACT_TYPE_ID")) & "")
                        .Attributes("TEC_CONTACT_TYPE_ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("TEC_CONTACT_TYPE_ID")) & "")
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

        Public Function GetReserveUsernamePassword() As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the airport record, based on the given field value
            'Input  : 
            'Output :  
            '<MS_RESERVE_USERNAME_PASSWORD_OUTPUT>
            '   <RESERVE  FIELD_VALUE=''/>
            '  <Errors Status="False">
            '     <Error Code="" Description=""/>
            ' </Errors>
            '</MS_RESERVE_USERNAME_PASSWORD_OUTPUT> 

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "GetReserveUsernamePassword"

            objOutputXml.LoadXml(strReserveUsernamePassword_OUTPUT)

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_OTH_MS_RESERVE_USERNAME_PASSWORD"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("RESERVE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("FIELD_VALUE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FIELD_VALUE")))
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


        Public Function ListHistory() As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the airport record, based on the given field value
            'Input  : 
            'Output :  
            '<MS_EMPLOYEE_OUTPUT>
            '   <EMPLOYEE  EmployeeID="" Employee_Name=""/>
            '  <Errors Status="False">
            '     <Error Code="" Description=""/>
            ' </Errors>
            '</MS_EMPLOYEE_OUTPUT> 

            '************************************************************************
            Const strLISTHISTORY_OUTPUT = "<MS_EMPLOYEEHISTORY_OUTPUT><EMPLOYEEHISTORY HISTORYID='1' HISTORY_NAME='Employee'/><EMPLOYEEHISTORY HISTORYID='2' HISTORY_NAME='Group'/><EMPLOYEEHISTORY HISTORYID='3' HISTORY_NAME='IP'/><EMPLOYEEHISTORY HISTORYID='4' HISTORY_NAME='Supervisory'/><EMPLOYEEHISTORY HISTORYID='5' HISTORY_NAME='Helpdesk'/><Errors Status='False'><Error Code='' Description=''/></Errors></MS_EMPLOYEEHISTORY_OUTPUT>"
            Dim objOutputXml As New XmlDocument
            Dim blnRecordFound As Boolean = True
            Dim strMETHOD_NAME As String = "ListHistory"

            objOutputXml.LoadXml(strLISTHISTORY_OUTPUT)

            Try

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

            End Try
            Return objOutputXml
        End Function


        Public Function GetEmployeeHistory(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function gives search results based on choosen search criterion.
            'Input  :- 
            '<MS_EMPLOYEE_HISTORY_INPUT>
            '	<EMPLOYEEID></EMPLOYEEID>           
            '</MS_EMPLOYEE_HISTORY_INPUT>

            'Output :
            '<MS_EMPLOYEE_HISTORY_OUTPUT>	
            '	<EMPLOYEE_HISTORY EMPLOYEEID='' HISTORYID ='' CHANGEDDATA='' ADDED='' REMOVED='' CHANGEDBY='' DATETIME=''/>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_EMPLOYEE_HISTORY_OUTPUT>

            ''************************************************************************
            Const strEMPLOYEEHISTORY_OUTPUT = "<MS_EMPLOYEE_HISTORY_OUTPUT><EMPLOYEE_HISTORY EMPLOYEEID='' HISTORYID ='' CHANGEDDATA='' ADDED='' REMOVED='' CHANGEDBY='' DATETIME=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_EMPLOYEE_HISTORY_OUTPUT>"
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strEmployeeID As String = ""


            Const strMETHOD_NAME As String = "GetEmployeeHistory"
            objOutputXml.LoadXml(strEMPLOYEEHISTORY_OUTPUT)
            strEmployeeID = SearchDoc.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText.Trim()
            Try

                '--------Retrieving & Checking Details from Input XMLDocument ------End
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_SRO_MS_EMPLOYEE_HISTORY]"
                    .Connection = objSqlConnection
                    .Parameters.Add("@ACTION", SqlDbType.Char, 1)
                    .Parameters("@ACTION").Value = "S"

                    .Parameters.Add("@EMPLOYEEID", SqlDbType.Int)
                    If strEmployeeID = "" Then
                        .Parameters("@EMPLOYEEID").Value = DBNull.Value
                    Else
                        .Parameters("@EMPLOYEEID").Value = strEmployeeID
                    End If
                End With

                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()


                objOutputXml.DocumentElement.RemoveChild(objOutputXml.DocumentElement.SelectSingleNode("EMPLOYEE_HISTORY"))

                Dim objDocFrag1 As XmlDocumentFragment



                Do While objSqlReader.Read()
                    blnRecordFound = True
                    Dim objOutputXml_NEW As New XmlDocument
                    objOutputXml_NEW.LoadXml(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHANGEDDATA")))

                    objDocFrag1 = objOutputXml.CreateDocumentFragment()
                    objDocFrag1.InnerXml = objOutputXml_NEW.DocumentElement.SelectSingleNode("EMPLOYEE_HISTORY").OuterXml 'objOutputXml_NEW.OuterXml
                    objOutputXml.DocumentElement.AppendChild(objDocFrag1)
                    objOutputXml_NEW = Nothing
                Loop
                'objOutputXml_NEW.AppendChild(objOutputXml_NEW.DocumentElement.SelectSingleNode("EMPLOYEE_HISTORY").OuterXml)
                objSqlReader.Close()
               
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
    End Class
End Namespace