'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Ashishsrivastava $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizTravelAgency/bzPCInstallation.vb $
'$Workfile: bzPCInstallation.vb $
'$Revision: 26 $
'$Archive: /AAMS/Components/bizTravelAgency/bzPCInstallation.vb $
'$Modtime: 6/01/10 1:04p $

Imports System.Xml
Imports System.Data
Imports System.Data.SqlClient
Imports AAMS.bizShared
Imports Microsoft.VisualBasic
Imports System.Globalization
Namespace bizTravelAgency
    Public Class bzPCInstallation
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzPCInstallation"

        Const strPCINSTALL_VIEW_INPUT = "<UP_TA_VIEW_PCINSTALLATION_INPUT><ROWID /></UP_TA_VIEW_PCINSTALLATION_INPUT>"""
        Const strPCINSTALL_VIEW_OUTPUT = "<UP_TA_VIEW_PCINSTALLATION_OUTPUT><DETAIL ROWID = '' LCODE ='' DATE ='' CPUTYPE = '' CPUNO ='' MONTYPE ='' MONNO = '' KBDTYPE ='' KBDNO ='' ADDLRAM='' MSETYPE ='' MSENO ='' OrderNumber = '' REMARKS = ''  CHALLANNUMBER = '' CDRNO  = '' PCTYPE ='' /><Errors Status=''><Error Code='' Description='' /></Errors></UP_TA_VIEW_PCINSTALLATION_OUTPUT>"

        Const strPCINSTALL_UPDATE_INPUT = "<UP_TA_UPDATE_PCINSTALLATION_INPUT><DETAIL ACTION = '' LCODE ='' DATE  = ''   CPUTYPE ='' CPUNO = ''  MONTYPE  = ''  MONNO  = '' KBDTYPE = ''  KBDNO  =''  CDRNO=''  MSETYPE  = '' MSENO  = ''  ADDLRAM='' OrderNumber  = '' Qty ='' REMARKS  = '' CHALLANDATE  =''    CHALLANNUMBER  = '' LoggedBy  ='' LoggedDateTime  = ''  CHALLANSTATUS  = '' ROWID=''  PCTYPE =''  USE_BACKDATED_CHALLAN='' OVERRIDE_CHALLAN_NO ='' OVERRIDE_CHALLAN_SERIAL_NO='' OVERRIDE_ORDER_NO=''	/></UP_TA_UPDATE_PCINSTALLATION_INPUT>"
        Const strPCINSTALL_UPDATE_OUTPUT = "<UP_TA_UPDATE_PCINSTALLATION_OUTPUT><DETAIL LCODE ='' DATE  = ''   CPUTYPE ='' CPUNO = ''  MONTYPE  = ''  MONNO  = '' KBDTYPE = ''  KBDNO  ='' CDRNO=''  MSETYPE  = '' MSENO  = ''  ADDLRAM='' OrderNumber  = '' REMARKS  = '' CHALLANDATE  ='' CHALLANNUMBER  = '' LoggedBy  ='' LoggedDateTime  = ''  CHALLANSTATUS  = '' ROWID=''  PCTYPE ='' USE_BACKDATED_CHALLAN ='' OVERRIDE_CHALLAN_NO ='' OVERRIDE_CHALLAN_SERIAL_NO='' OVERRIDE_ORDER_NO='' /><Errors Status=''><Error Code='' Description='' /></Errors></UP_TA_UPDATE_PCINSTALLATION_OUTPUT>"

        Const strPCINSTALL_DELETE_INPUT = "<UP_TA_DELETE_PCINSTALLATION_INPUT><ROWID /></UP_TA_DELETE_PCINSTALLATION_INPUT>"
        Const strPCINSTALL_DELETE_OUTPUT = "<UP_TA_DELETE_PCINSTALLATION_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></UP_TA_DELETE_PCINSTALLATION_OUTPUT>"
        
        Const strOrderInstallation_OUTPUT = "<MS_GET_ORDERINSTALLATIONHISTORY_OUTPUT> <DETAIL  DATETIME ='' CPUTYPE ='' CPUNO ='' ADDLRAM= '' MONTYPE  = ''  MONNO ='' KBDTYPE = '' KBDNO ='' CDRNO ='' MSETYPE ='' MSENO = '' IOrderNumber = '' DOrderNumber = '' REMARKS  = '' CHALLANNUMBER  = '' ActionTaken = '' UserName = ''  LoggedDateTime= '' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_GET_ORDERINSTALLATIONHISTORY_OUTPUT>"

        Const strOrderPCount_INPUT = "<MS_GET_ORDERS_PC_COUNT_INPUT><LCODE/><ORDERNUMBER/><NEWORDER/><NoOfPCs/><PCType/></MS_GET_ORDERS_PC_COUNT_INPUT>"
        Const strOrderPCount_OUTPUT = "<MS_GET_ORDERS_PC_COUNT_OUTPUT> <DETAIL  LCODE=''  ORDER_NUMBER =''  NEWORDER=''  TOTAMADEUSPC='' TOTMISC='' TOTAGENCYPC=''   MISCINSTALLED=''  PCINSTALLED=''  PCDINSTALLED=''   MISCDINSTALLED =''  AGENCYINSTALLED =''  AGENCYDINSTALLED ='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_GET_ORDERS_PC_COUNT_OUTPUT>"

        Const strPTYPEFOOTER_INPUT = "<GET_PTYPEFOTTER_INPUT><LOCATION_CODE></LOCATION_CODE></GET_PTYPEFOTTER_INPUT>"
        Const strPTYPEFOOTER_OUTPUT = "<GET_PTYPEFOTTER_OUTPUT><PTYPEFOTTEROUTPUT COMPANY='' EMPLOYEE_NAME='' ADDRESS='' PHONE='' FAX='' CITY='' COUNTRY_NAME='' REG_OFFICE_ADDRESS=''  Reg_Tel='' Reg_Fax='' /><Errors Status=''><Error Code='' Description='' /></Errors></GET_PTYPEFOTTER_OUTPUT>"

        Public Function GetPTypeFooterDetails(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            ''created by Ashish on 29-jul-2009
            'Purpose:This function gives footers details of Agency Connectivity.
            'Input  :
            '<GET_PTYPEFOTTER_INPUT><EMPLOYEEID></EMPLOYEEID></GET_PTYPEFOTTER_INPUT>

            'Output :
            '<GET_PTYPEFOTTER_OUTPUT>
            '<PTYPEFOTTEROUTPUT COMPANY='' EMPLOYEE_NAME='' ADDRESS='' PHONE='' FAX='' CITY='' COUNTRY_NAME='' REG_OFFICE_ADDRESS='' REG_TEL='' REG_FAX='' />
            '<Errors Status=''><Error Code='' Description='' /></Errors>
            '</GET_PTYPEFOTTER_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim intlOCATION_CODE As String
            Const strMETHOD_NAME As String = "GetPTypeFooterDetails"
            Dim blnRecordFound As Boolean
            'Dim intPageNo, intPageSize As Integer
            'Dim strSortBy As String
            'Dim blnDesc As Boolean


            Try
                objOutputXml.LoadXml(strPTYPEFOOTER_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                intlOCATION_CODE = SearchDoc.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText.Trim
                If intlOCATION_CODE = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If


                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_RPT_GET_FOOTER_PTYPE_CHALLAN"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@lOCATION_CODE", SqlDbType.BigInt))
                    .Parameters("@lOCATION_CODE").Value = intlOCATION_CODE
                End With

                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("PTYPEFOTTEROUTPUT")

                If objSqlReader.HasRows Then
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.RemoveChild(objAptNode)
                End If

                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("COMPANY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("COMPANY")) & "")
                    objAptNodeClone.Attributes("EMPLOYEE_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMPLOYEE_NAME")) & "")
                    objAptNodeClone.Attributes("ADDRESS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ADDRESS")) & "")
                    objAptNodeClone.Attributes("PHONE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PHONE")) & "")
                    objAptNodeClone.Attributes("FAX").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FAX")) & "")
                    objAptNodeClone.Attributes("CITY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITY")) & "")
                    objAptNodeClone.Attributes("COUNTRY_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("COUNTRY_NAME")) & "")
                    If Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Country_Name"))).ToUpper = "INDIA" Then
                        objAptNodeClone.Attributes("REG_OFFICE_ADDRESS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("REGD_OFFICE_ADDRESS")) & "")
                        objAptNodeClone.Attributes("Reg_Tel").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Reg_Tel")) & "")
                        objAptNodeClone.Attributes("Reg_Fax").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Reg_Fax")) & "")
                    Else
                        objAptNodeClone.Attributes("REG_OFFICE_ADDRESS").InnerText = ""
                        objAptNodeClone.Attributes("Reg_Tel").InnerText = ""
                        objAptNodeClone.Attributes("Reg_Fax").InnerText = ""
                    End If

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                    blnRecordFound = True
                Loop
                objSqlReader.Close()
                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    'objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").InnerText = Val(objSqlCommand.Parameters("@TOTALROWS").Value)
                    'If intPageSize = 0 Then
                    '    objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = 0
                    'Else
                    '    objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = Math.Ceiling(objSqlCommand.Parameters("@TOTALROWS").Value / intPageSize)
                    'End If
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

        End Function
        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Delete
            '***********************************************************************
            'Purpose:This function deletes a PCInstallation.
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
            Dim strROWID As String

            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strPCINSTALL_DELETE_OUTPUT)

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
                    .CommandText = "UP_SRO_TA_PCINSTALLATION"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"

                    .Parameters.Add(New SqlParameter("@ROWID", SqlDbType.Int))
                    .Parameters("@ROWID").Value = CInt(strROWID)
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
                'CATCHING AAMS EXCEPTIONSU
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
        Public Function RestrictEquipmentList() As System.Xml.XmlDocument
            'OutPut
            '<TA_GET_RESTRICTED_EQUIPMENT_CODE_OUTPUT>
            '   <EGROUPCODE EQUIPMENT_CODE = '' />
            '</TA_GET_RESTRICTED_EQUIPMENT_CODE_OUTPUT>
            '--------------------------------------------------------------------------

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objGetEgroupListCommand As New SqlCommand
            Dim objGetEcodeXML As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Const strMETHOD_NAME As String = "RestrictEquipmentList"

            Try
                '---------------METHOD TO GET RESTRICTED EGROUP CODE
                With objGetEgroupListCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LIST_TA_RESTRICTEQUIPMENT_CODE"

                    .Parameters.Add(New SqlParameter("@RETURNXML", SqlDbType.Xml))
                    .Parameters("@RETURNXML").Direction = ParameterDirection.Output
                    .Parameters("@RETURNXML").Value = ""
                End With
                objGetEgroupListCommand.Connection.Open()
                objSqlReader = objGetEgroupListCommand.ExecuteReader()
                objGetEcodeXML.LoadXml(objGetEgroupListCommand.Parameters("@RETURNXML").Value)
                'END---------------METHOD TO GET RESTRICTED EGROUP CODE

            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objGetEcodeXML, "101", Exec.Message)
                Return objGetEcodeXML
            Catch exec As Exception
                'msgbox(exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bzShared.FillErrorStatus(objGetEcodeXML, "103", exec.Message)
                Return objGetEcodeXML
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objGetEgroupListCommand.Dispose()
            End Try

            Return objGetEcodeXML
        End Function
        Public Function Update(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Update

            '***********************************************************************
            'Purpose:This function Inserts/Updates PCInstallation.
            'Input  :
            '<UP_TA_UPDATE_PCINSTALLATION_INPUT>
            '<DETAIL ACTION ='' LCODE ='' DATE  = ''   CPUTYPE ='' CPUNO = ''  MONTYPE  = ''  MONNO  = '' KBDTYPE = ''  
            '       KBDNO  =''  CDRNO=''  MSETYPE  = '' MSENO  = ''  ADDLRAM = '' OrderNumber  = '' Qty ='' REMARKS  = '' CHALLANDATE  =''    
            '       CHALLANNUMBER  = '' LoggedBy  ='' LoggedDateTime  = ''  CHALLANSTATUS  = '' ROWID=''  PCTYPE =''
            '       USE_BACKDATED_CHALLAN='' OVERRIDE_CHALLAN_NO ='' OVERRIDE_CHALLAN_SERIAL_NO='' OVERRIDE_ORDER_NO='' />
            '</UP_TA_UPDATE_PCINSTALLATION_INPUT>"

            'Output
            '<UP_TA_UPDATE_PCINSTALLATION_OUTPUT>
            '<DETAIL LCODE ='' DATE  = ''   CPUTYPE ='' CPUNO = ''  MONTYPE  = ''  MONNO  = '' KBDTYPE = ''  
            '       KBDNO  ='' CDRNO=''  MSETYPE  = '' MSENO  = ''  ADDLRAM = ''  OrderNumber  = '' REMARKS  = '' CHALLANDATE  ='' 
            '       CHALLANNUMBER  = '' LoggedBy  ='' LoggedDateTime  = ''  CHALLANSTATUS  = '' ROWID=''  PCTYPE ='' 
            '       USE_BACKDATED_CHALLAN ='' OVERRIDE_CHALLAN_NO ='' OVERRIDE_CHALLAN_SERIAL_NO='' OVERRIDE_ORDER_NO='' />
            '<Errors Status=''>
            '   <Error Code='' Description='' />
            '</Errors>
            '</UP_TA_UPDATE_PCINSTALLATION_OUTPUT>"
            '************************************************************************

            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objHisSqlCommand As New SqlCommand
            Dim objGetEgroupListCommand As New SqlCommand
            Dim objSqlRowCommand As New SqlCommand
            Dim objSqlRowCommand1 As New SqlCommand
            Dim objTran As SqlTransaction
            Dim objUpdateDocOutput As New XmlDocument
            Dim objGetEcodeXML As New XmlDocument
            Dim objNode As XmlNode
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim intRecordsAffected As Int32
            Dim objSqlReader As SqlDataReader
            Dim strProductList As New ArrayList


            Dim strLcode As String
            Dim strDATE As String = ""
            Dim strCPUTYPE As String = ""
            Dim strCPUNO As String = ""
            Dim strMONTYPE As String = ""
            Dim strMONNO As String = ""
            Dim strKBDTYPE As String = ""
            Dim strKBDNO As String = ""
            Dim strCDRNO As String = ""
            Dim strMSETYPE As String = ""
            Dim strMSENO As String = ""
            Dim STRAddlRam As String = ""
            Dim strOrderNumber As String = ""
            Dim strREMARKS As String = ""
            Dim strCHALLANNUMBER As String = ""
            Dim strLoggedBy As String = ""
            Dim strROWID As String = ""
            Dim strPCTYPE As String = ""
            Dim strErrorMessage As String = ""
            Dim intQty As Integer
            Dim strtempAction As String = String.Empty

            '------Security Related Variables
            Dim boolOverrideBackDateChallan As Boolean = False
            Dim boolOverrideChallanNo As Boolean = False
            Dim boolOverrideChallanSerialNo As Boolean = False
            Dim boolOverrideOrderNo As Boolean = False
            'END------Security Related Variables

            Const strMETHOD_NAME As String = "Update"

            Try
                '---------------METHOD TO GET RESTRICTED EGROUP CODE
                ''With objGetEgroupListCommand
                ''    .Connection = objSqlConnection
                ''    .CommandType = CommandType.StoredProcedure
                ''    .CommandText = "UP_LIST_TA_RESTRICTEQUIPMENT_CODE"

                ''    .Parameters.Add(New SqlParameter("@RETURNXML", SqlDbType.Xml))
                ''    .Parameters("@RETURNXML").Direction = ParameterDirection.Output
                ''    .Parameters("@RETURNXML").Value = ""
                ''End With
                ''objGetEgroupListCommand.Connection.Open()
                ''objSqlReader = objGetEgroupListCommand.ExecuteReader()
                ''objGetEcodeXML.LoadXml(objGetEgroupListCommand.Parameters("@RETURNXML").Value)

                ''For Each objNode In objGetEcodeXML.DocumentElement.SelectNodes("EGROUPCODE")
                ''    strProductList.Add(objNode.Attributes("EQUIPMENT_CODE").InnerText)
                ''Next
                ''objGetEgroupListCommand.Connection.Close()
                'END---------------METHOD TO GET RESTRICTED EGROUP CODE

                objUpdateDocOutput.LoadXml(strPCINSTALL_UPDATE_OUTPUT)
                With objUpdateDocOutput.DocumentElement.SelectSingleNode("DETAIL")
                    .Attributes("LCODE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("LCODE").InnerText
                    strLcode = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("LCODE").InnerText

                    .Attributes("DATE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("DATE").InnerText
                    strDATE = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("DATE").InnerText

                    .Attributes("CPUTYPE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUTYPE").InnerText
                    strCPUTYPE = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUTYPE").InnerText

                    .Attributes("CPUNO").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUNO").InnerText
                    strCPUNO = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUNO").InnerText

                    .Attributes("MONTYPE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONTYPE").InnerText
                    strMONTYPE = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONTYPE").InnerText

                    .Attributes("MONNO").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONNO").InnerText
                    strMONNO = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONNO").InnerText

                    .Attributes("KBDTYPE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("KBDTYPE").InnerText
                    strKBDTYPE = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("KBDTYPE").InnerText

                    .Attributes("KBDNO").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("KBDNO").InnerText
                    strKBDNO = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("KBDNO").InnerText

                    .Attributes("CDRNO").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CDRNO").InnerText
                    strCDRNO = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CDRNO").InnerText

                    .Attributes("MSETYPE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("MSETYPE").InnerText
                    strMSETYPE = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("MSETYPE").InnerText

                    .Attributes("MSENO").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("MSENO").InnerText
                    strMSENO = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("MSENO").InnerText

                    .Attributes("ADDLRAM").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("ADDLRAM").InnerText
                    STRAddlRam = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("ADDLRAM").InnerText

                    .Attributes("OrderNumber").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("OrderNumber").InnerText
                    strOrderNumber = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("OrderNumber").InnerText

                    .Attributes("REMARKS").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("REMARKS").InnerText
                    strREMARKS = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("REMARKS").InnerText

                    .Attributes("CHALLANDATE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANDATE").InnerText

                    .Attributes("CHALLANNUMBER").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANNUMBER").InnerText
                    strCHALLANNUMBER = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANNUMBER").InnerText

                    .Attributes("LoggedBy").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("LoggedBy").InnerText
                    strLoggedBy = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("LoggedBy").InnerText

                    .Attributes("LoggedDateTime").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("LoggedDateTime").InnerText

                    .Attributes("CHALLANSTATUS").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANSTATUS").InnerText

                    .Attributes("ROWID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("ROWID").InnerText
                    strROWID = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("ROWID").InnerText

                    .Attributes("PCTYPE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("PCTYPE").InnerText
                    strPCTYPE = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("PCTYPE").InnerText

                    .Attributes("USE_BACKDATED_CHALLAN").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("USE_BACKDATED_CHALLAN").InnerText
                    boolOverrideBackDateChallan = CBool(UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("USE_BACKDATED_CHALLAN").InnerText)

                    .Attributes("OVERRIDE_CHALLAN_NO").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_CHALLAN_NO").InnerText
                    boolOverrideChallanNo = CBool(UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_CHALLAN_NO").InnerText)

                    .Attributes("OVERRIDE_CHALLAN_SERIAL_NO").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_CHALLAN_SERIAL_NO").InnerText
                    boolOverrideChallanSerialNo = CBool(UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_CHALLAN_SERIAL_NO").InnerText)

                    .Attributes("OVERRIDE_ORDER_NO").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_ORDER_NO").InnerText
                    boolOverrideOrderNo = (UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_ORDER_NO").InnerText)

                    intQty = Val(Trim((UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("Qty").InnerText) & ""))

                End With


                ' ''---------CHECK BACK DATE CHALLN      --OlD Code
                ''If boolOverrideBackDateChallan = False Then
                ''    Dim CurrentDate As String
                ''    CurrentDate = DateTime.Now.Today.ToString("dd/MM/yyyy")

                ''    CurrentDate = GetDateFormat(CurrentDate, "dd/MM/yyyy", "yyyyMMdd", "/")
                ''    If strDATE.Trim() <> "" Then
                ''        If CInt(strDATE) < CInt(CurrentDate) Then
                ''            strErrorMessage = "Cann''t use Back Dated challan."
                ''            Throw (New AAMSException(strErrorMessage))
                ''        End If
                ''    End If
                ''End If
                ' ''---------CHECK BACK DATE CHALLN




                '##############  GET DATE  FROM CONFIG FILE ####################################
                Dim cmd As New SqlCommand
                Dim intConfigDays As Integer
                cmd.CommandText = "UP_GET_TA_GETCHALLANBACKDATE"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Connection = objSqlConnection
                objSqlConnection.Open()
                intConfigDays = cmd.ExecuteScalar()
                objSqlConnection.Close()
                cmd.Dispose()
                '######################################################################################    
                ' ''---------CHECK BACK DATE CHALLN    Code Modified on 19th Feb by Ashish
                If boolOverrideBackDateChallan = False Then
                    Dim CurrentDate As Date
                    CurrentDate = DateTime.Now.Today()

                    CurrentDate = DateAdd(DateInterval.Day, -(intConfigDays), CDate(CurrentDate))
                    'MsgBox(CurrentDate)

                    'CurrentDate = GetDateFormat(CurrentDate, "dd/MM/yyyy", "yyyyMMdd", "/")


                    Dim tmp As String = strDATE
                    tmp = tmp.Substring(4, 2) + "/" + tmp.Substring(6, 2) + "/" + tmp.Substring(0, 4)



                    If strDATE.Trim() <> "" Then
                        ' If CInt(strDATE) < CInt(CurrentDate) Then
                        If DateDiff(DateInterval.Day, CDate(CurrentDate), CDate(tmp)) < 0 Then

                            strErrorMessage = "Cann't use Back Dated challan."
                            Throw (New AAMSException(strErrorMessage))
                        End If
                    End If
                End If
                ' ''--------- End Code Modified on 19th Feb by Ashish


                ''Code Modified on 19th Feb by Ashish
                'If boolOverrideBackDateChallan = False Then
                '    Dim CurrentDate As Date
                '    'CurrentDate = DateTime.Now.Today.ToString("dd/MM/yyyy")
                '    ' CurrentDate = DateAdd(DateInterval.Day, -(intConfigDays), CDate(CurrentDate))
                '    CurrentDate = DateAdd(DateInterval.Day, -(intConfigDays), Today)
                '    'MsgBox(CurrentDate)
                '    'CurrentDate = GetDateFormat(CurrentDate, "dd/MM/yyyy", "yyyyMMdd", "/")
                '    Dim tmp As String = strDATE
                '    tmp = tmp.Substring(6, 2) + "/" + tmp.Substring(4, 2) + "/" + tmp.Substring(0, 4)
                '    If strDATE.Trim() <> "" Then
                '        ' If CInt(strDATE) < CInt(CurrentDate) Then
                '        If DateDiff(DateInterval.Day, CDate(CurrentDate), CDate(tmp)) < 0 Then
                '            strErrorMessage = "Cann''t use Back Dated challan."
                '            Throw (New AAMSException(strErrorMessage))
                '        End If
                '    End If
                'End If

                ''Code Modified on 19th Feb by Ashish

                With UpdateDoc.DocumentElement.SelectSingleNode("DETAIL")
                    strROWID = ((.Attributes("ROWID").InnerText).Trim).ToString
                    If strROWID = "" Then
                        strtempAction = "I"
                    Else
                        strtempAction = "U"
                    End If
                End With


                '---------CHECK NUMBER OF PC INSTALLED AND DINSTALLAED IN CASE OF AMADEUS PC AND AGENCY PC
                If boolOverrideOrderNo = False Then
                    Dim objInputPCCountXml As New XmlDocument
                    'Dim str_INPUT As String = "<MS_GET_ORDERS_PC_COUNT_INPUT><LCODE>10</LCODE><ORDERNUMBER>2004/5/321</ORDERNUMBER><NEWORDER>T</NEWORDER><NoOfPCs>1</NoOfPCs><PCType>1</PCType></MS_GET_ORDERS_PC_COUNT_INPUT>"
                    Dim str_INPUT As String = "<MS_GET_ORDERS_PC_COUNT_INPUT><LCODE/><ORDERNUMBER/><NEWORDER/><NoOfPCs/><PCType/></MS_GET_ORDERS_PC_COUNT_INPUT>"
                    objInputPCCountXml.LoadXml(str_INPUT)

                    With objInputPCCountXml.DocumentElement
                        .SelectSingleNode("LCODE").InnerText = strLcode
                        .SelectSingleNode("ORDERNUMBER").InnerText = strOrderNumber
                        .SelectSingleNode("NEWORDER").InnerText = "T"
                        .SelectSingleNode("NoOfPCs").InnerText = intQty
                        .SelectSingleNode("PCType").InnerText = strPCTYPE
                    End With
                    If strOrderNumber.Trim <> "00/00/00" Then
                        If strtempAction = "I" Then
                            objInputPCCountXml = CheckForNoOfPC(objInputPCCountXml)
                            If objInputPCCountXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "TRUE" Then
                                strErrorMessage = objInputPCCountXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").InnerText
                                Throw (New AAMSException(strErrorMessage))
                            End If
                        End If
                    End If
                End If
                'END---------CHECK NUMBER OF PC INSTALLED AND DINSTALLAED IN CASE OF AMADEUS PC AND AGENCY PC

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("DETAIL")
                    strROWID = ((.Attributes("ROWID").InnerText).Trim).ToString
                    If strROWID = "" Then
                        strAction = "I"
                    Else
                        strAction = "U"
                    End If

                    strLcode = ((.Attributes("LCODE").InnerText).Trim).ToString
                    If strLcode = "" Then
                        Throw (New AAMSException("Location Code can't be blank"))
                    End If

                    strCPUTYPE = ((.Attributes("CPUTYPE").InnerText).Trim).ToString
                    If strCPUTYPE = "" And strPCTYPE = "1" Then 'CASE 1APC INSTALLATION
                        Throw (New AAMSException("CPUTYPE can't be blank"))
                    End If

                    If strPCTYPE = "" Then 'CASE 1APC INSTALLATION
                        Throw (New AAMSException("Invalid Parameter PCTYPE can't be blank."))
                    End If

                    If strAction = "" Then
                        Throw (New AAMSException("Invalid Action Code."))
                    End If

                    If strOrderNumber = "" Then
                        Throw (New AAMSException("Order Number can't be blank."))
                    End If
                    If strCHALLANNUMBER = "" Then
                        Throw (New AAMSException("Challan Number can't be blank."))
                    End If
                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_PCINSTALLATION"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction

                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("LCODE").InnerText.Trim = "" Then
                        .Parameters("@LCODE").Value = DBNull.Value
                    Else
                        .Parameters("@LCODE").Value = CInt(strLcode)
                    End If

                    .Parameters.Add(New SqlParameter("@DATE", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("DATE").InnerText.Trim = "" Then
                        .Parameters("@DATE").Value = DBNull.Value
                    Else
                        .Parameters("@DATE").Value = CInt(strDATE)
                    End If

                    .Parameters.Add(New SqlParameter("@CPUTYPE", SqlDbType.Char, 3))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUTYPE").InnerText.Trim = "" Then
                        .Parameters("@CPUTYPE").Value = DBNull.Value
                    Else
                        .Parameters("@CPUTYPE").Value = strCPUTYPE
                    End If

                    .Parameters.Add(New SqlParameter("@CPUNO", SqlDbType.VarChar, 25))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUNO").InnerText.Trim = "" Then
                        .Parameters("@CPUNO").Value = DBNull.Value
                    Else
                        .Parameters("@CPUNO").Value = strCPUNO
                    End If

                    .Parameters.Add(New SqlParameter("@MONTYPE", SqlDbType.Char, 3))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONTYPE").InnerText.Trim = "" Then
                        .Parameters("@MONTYPE").Value = DBNull.Value
                    Else
                        .Parameters("@MONTYPE").Value = strMONTYPE
                    End If

                    .Parameters.Add(New SqlParameter("@MONNO", SqlDbType.VarChar, 25))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONNO").InnerText.Trim = "" Then
                        .Parameters("@MONNO").Value = DBNull.Value
                    Else
                        .Parameters("@MONNO").Value = strMONNO
                    End If

                    .Parameters.Add(New SqlParameter("@KBDTYPE", SqlDbType.Char, 3))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("KBDTYPE").InnerText.Trim = "" Then
                        .Parameters("@KBDTYPE").Value = DBNull.Value
                    Else
                        .Parameters("@KBDTYPE").Value = strKBDTYPE
                    End If

                    .Parameters.Add(New SqlParameter("@KBDNO", SqlDbType.VarChar, 25))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("KBDNO").InnerText.Trim = "" Then
                        .Parameters("@KBDNO").Value = DBNull.Value
                    Else
                        .Parameters("@KBDNO").Value = strKBDNO
                    End If

                    .Parameters.Add(New SqlParameter("@CDRNO", SqlDbType.VarChar, 25))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CDRNO").InnerText.Trim = "" Then
                        .Parameters("@CDRNO").Value = DBNull.Value
                    Else
                        .Parameters("@CDRNO").Value = strCDRNO
                    End If

                    .Parameters.Add(New SqlParameter("@ADDLRAM", SqlDbType.VarChar, 25))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("ADDLRAM").InnerText.Trim = "" Then
                        .Parameters("@ADDLRAM").Value = DBNull.Value
                    Else
                        .Parameters("@ADDLRAM").Value = STRAddlRam
                    End If

                    .Parameters.Add(New SqlParameter("@MSETYPE", SqlDbType.Char, 3))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("MSETYPE").InnerText.Trim = "" Then
                        .Parameters("@MSETYPE").Value = DBNull.Value
                    Else
                        .Parameters("@MSETYPE").Value = strMSETYPE
                    End If

                    .Parameters.Add(New SqlParameter("@MSENO", SqlDbType.VarChar, 25))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("MSENO").InnerText.Trim = "" Then
                        .Parameters("@MSENO").Value = DBNull.Value
                    Else
                        .Parameters("@MSENO").Value = strMSENO
                    End If

                    .Parameters.Add(New SqlParameter("@OrderNumber", SqlDbType.Char, 12))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("OrderNumber").InnerText.Trim = "" Then
                        .Parameters("@OrderNumber").Value = DBNull.Value
                    Else
                        .Parameters("@OrderNumber").Value = strOrderNumber
                    End If

                    .Parameters.Add(New SqlParameter("@REMARKS", SqlDbType.VarChar, 100))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("REMARKS").InnerText.Trim = "" Then
                        .Parameters("@REMARKS").Value = DBNull.Value
                    Else
                        .Parameters("@REMARKS").Value = strREMARKS
                    End If

                    .Parameters.Add(New SqlParameter("@CHALLANNUMBER", SqlDbType.VarChar, 17))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANNUMBER").InnerText.Trim = "" Then
                        .Parameters("@CHALLANNUMBER").Value = DBNull.Value
                    Else
                        .Parameters("@CHALLANNUMBER").Value = strCHALLANNUMBER
                    End If

                    .Parameters.Add(New SqlParameter("@LoggedBy", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("LoggedBy").InnerText.Trim = "" Then
                        .Parameters("@LoggedBy").Value = DBNull.Value
                    Else
                        .Parameters("@LoggedBy").Value = CInt(strLoggedBy)
                    End If

                    .Parameters.Add(New SqlParameter("@ROWID", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("ROWID").InnerText.Trim = "" Then
                        .Parameters("@ROWID").Value = DBNull.Value
                    Else
                        .Parameters("@ROWID").Value = CInt(strROWID)
                    End If

                    .Parameters.Add(New SqlParameter("@PCTYPE", SqlDbType.Char, 1))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("PCTYPE").InnerText.Trim = "" Then
                        .Parameters("@PCTYPE").Value = DBNull.Value
                    Else
                        .Parameters("@PCTYPE").Value = strPCTYPE
                    End If

                    .Parameters.Add(New SqlParameter("@QTY", SqlDbType.Int))
                    If Val(intQty) = 0 Then
                        .Parameters("@QTY").Value = 0
                    Else
                        .Parameters("@QTY").Value = intQty
                    End If

                    .Parameters.Add(New SqlParameter("@RETUNID", SqlDbType.Int))
                    .Parameters("@RETUNID").Direction = ParameterDirection.Output
                    .Parameters("@RETUNID").Value = 0
                End With

                objSqlCommand.Connection.Open()
                objTran = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)
                objSqlCommand.Transaction = objTran

                intRecordsAffected = objSqlCommand.ExecuteNonQuery()
                objTran.Commit()


                intRetId = objSqlCommand.Parameters("@RETUNID").Value
                If UCase(strAction) = "I" Then
                    intRetId = objSqlCommand.Parameters("@RETUNID").Value
                    objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    With objUpdateDocOutput.DocumentElement.SelectSingleNode("DETAIL")
                        .Attributes("ROWID").InnerText = intRetId
                    End With
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
                If objSqlConnection.State <> ConnectionState.Closed Then
                    If Not objTran Is Nothing Then
                        objTran.Rollback()
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
            Return objUpdateDocOutput
        End Function
        Public Function ValidateInstallationInput(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument

            '***********************************************************************
            'Purpose:This function Inserts/Updates PCInstallation.
            'Input  :
            '<UP_TA_UPDATE_PCINSTALLATION_INPUT>
            '<DETAIL LCODE ='' DATE  = ''   CPUTYPE ='' CPUNO = ''  MONTYPE  = ''  MONNO  = '' KBDTYPE = ''  
            '       KBDNO  =''  CDRNO=''  MSETYPE  = '' MSENO  = ''  OrderNumber  = '' REMARKS  = '' CHALLANDATE  =''    
            '       CHALLANNUMBER  = '' LoggedBy  ='' LoggedDateTime  = ''  CHALLANSTATUS  = '' ROWID=''  PCTYPE =''  
            '       USE_BACKDATED_CHALLAN='' OVERRIDE_CHALLAN_NO ='' OVERRIDE_CHALLAN_SERIAL_NO='' OVERRIDE_ORDER_NO='' />
            '</UP_TA_UPDATE_PCINSTALLATION_INPUT>"

            'Output
            '<UP_TA_UPDATE_PCINSTALLATION_OUTPUT>
            '<DETAIL LCODE ='' DATE  = ''   CPUTYPE ='' CPUNO = ''  MONTYPE  = ''  MONNO  = '' KBDTYPE = ''  
            '       KBDNO  ='' CDRNO=''  MSETYPE  = '' MSENO  = ''  OrderNumber  = '' REMARKS  = '' CHALLANDATE  ='' 
            '       CHALLANNUMBER  = '' LoggedBy  ='' LoggedDateTime  = ''  CHALLANSTATUS  = '' ROWID=''  PCTYPE ='' 
            '       USE_BACKDATED_CHALLAN ='' OVERRIDE_CHALLAN_NO ='' OVERRIDE_CHALLAN_SERIAL_NO='' OVERRIDE_ORDER_NO='' />
            '<Errors Status=''>
            '   <Error Code='' Description='' />
            '</Errors>
            '</UP_TA_UPDATE_PCINSTALLATION_OUTPUT>"
            '************************************************************************

            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objHisSqlCommand As New SqlCommand
            Dim objGetEgroupListCommand As New SqlCommand
            Dim objSqlRowCommand As New SqlCommand
            Dim objSqlRowCommand1 As New SqlCommand
            Dim objTran As SqlTransaction
            Dim objUpdateDocOutput As New XmlDocument
            Dim objGetEcodeXML As New XmlDocument
            Dim objNode As XmlNode
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim intRecordsAffected As Int32
            Dim objSqlReader As SqlDataReader
            Dim strProductList As New ArrayList
            Dim strErrorMessage As String


            Dim strLcode As String
            Dim strDATE As String = ""
            Dim strCPUTYPE As String = ""
            Dim strCPUNO As String = ""
            Dim strMONTYPE As String = ""
            Dim strMONNO As String = ""
            Dim strKBDTYPE As String = ""
            Dim strKBDNO As String = ""
            Dim strCDRNO As String = ""
            Dim strMSETYPE As String = ""
            Dim strMSENO As String = ""
            Dim strOrderNumber As String = ""
            Dim strREMARKS As String = ""
            Dim strCHALLANNUMBER As String = ""
            Dim strLoggedBy As String = ""
            Dim strROWID As String = ""
            Dim strPCTYPE As String = ""

            '------Security Related Variables
            Dim boolOverrideBackDateChallan As Boolean = False
            Dim boolOverrideChallanNo As Boolean = False
            Dim boolOverrideChallanSerialNo As Boolean = False
            Dim boolOverrideOrderNo As Boolean = False
            'END------Security Related Variables

            Const strMETHOD_NAME As String = "ValidateInstallationInput"

            Try
                '---------------METHOD TO GET RESTRICTED EGROUP CODE
                With objGetEgroupListCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LIST_TA_OTH_EGROUPCODE"

                    .Parameters.Add(New SqlParameter("@RETURNXML", SqlDbType.Xml))
                    .Parameters("@RETURNXML").Direction = ParameterDirection.Output
                    .Parameters("@RETURNXML").Value = ""
                End With
                objGetEgroupListCommand.Connection.Open()
                objSqlReader = objGetEgroupListCommand.ExecuteReader()
                objGetEcodeXML.LoadXml(objGetEgroupListCommand.Parameters("@RETURNXML").Value)

                For Each objNode In objGetEcodeXML.DocumentElement.SelectNodes("EGROUPCODE")
                    strProductList.Add(objNode.Attributes("EQUIPMENT_CODE").InnerText)
                Next
                'END---------------METHOD TO GET RESTRICTED EGROUP CODE

                objUpdateDocOutput.LoadXml(strPCINSTALL_UPDATE_OUTPUT)
                With objUpdateDocOutput.DocumentElement.SelectSingleNode("DETAIL")
                    .Attributes("LCODE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("LCODE").InnerText
                    strLcode = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("LCODE").InnerText

                    .Attributes("DATE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("DATE").InnerText
                    strDATE = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("DATE").InnerText

                    .Attributes("CPUTYPE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUTYPE").InnerText
                    strCPUTYPE = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUTYPE").InnerText

                    .Attributes("CPUNO").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUNO").InnerText
                    strCPUNO = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUNO").InnerText

                    .Attributes("MONTYPE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONTYPE").InnerText
                    strMONTYPE = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONTYPE").InnerText

                    .Attributes("MONNO").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONNO").InnerText
                    strMONNO = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONNO").InnerText

                    .Attributes("KBDTYPE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("KBDTYPE").InnerText
                    strKBDTYPE = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("KBDTYPE").InnerText

                    .Attributes("KBDNO").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("KBDNO").InnerText
                    strKBDNO = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("KBDNO").InnerText

                    .Attributes("CDRNO").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CDRNO").InnerText
                    strCDRNO = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CDRNO").InnerText

                    .Attributes("MSETYPE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("MSETYPE").InnerText
                    strMSETYPE = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("MSETYPE").InnerText

                    .Attributes("MSENO").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("MSENO").InnerText
                    strMSENO = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("MSENO").InnerText

                    .Attributes("OrderNumber").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("OrderNumber").InnerText
                    strOrderNumber = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("OrderNumber").InnerText

                    .Attributes("REMARKS").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("REMARKS").InnerText
                    strREMARKS = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("REMARKS").InnerText

                    .Attributes("CHALLANDATE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANDATE").InnerText

                    .Attributes("CHALLANNUMBER").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANNUMBER").InnerText
                    strCHALLANNUMBER = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANDATE").InnerText

                    .Attributes("LoggedBy").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("LoggedBy").InnerText
                    strLoggedBy = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("LoggedBy").InnerText

                    .Attributes("LoggedDateTime").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("LoggedDateTime").InnerText

                    .Attributes("CHALLANSTATUS").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANSTATUS").InnerText

                    .Attributes("ROWID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("ROWID").InnerText
                    strROWID = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("ROWID").InnerText

                    .Attributes("PCTYPE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("PCTYPE").InnerText
                    strPCTYPE = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("PCTYPE").InnerText

                    .Attributes("USE_BACKDATED_CHALLAN").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("USE_BACKDATED_CHALLAN").InnerText
                    boolOverrideBackDateChallan = CBool(UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("USE_BACKDATED_CHALLAN").InnerText)

                    .Attributes("OVERRIDE_CHALLAN_NO").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_CHALLAN_NO").InnerText
                    boolOverrideChallanNo = CBool(UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_CHALLAN_NO").InnerText)

                    .Attributes("OVERRIDE_CHALLAN_SERIAL_NO").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_CHALLAN_SERIAL_NO").InnerText
                    boolOverrideChallanSerialNo = CBool(UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_CHALLAN_SERIAL_NO").InnerText)

                    .Attributes("OVERRIDE_ORDER_NO").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_ORDER_NO").InnerText
                    boolOverrideOrderNo = (UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_ORDER_NO").InnerText)

                End With

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("DETAIL")
                    strROWID = ((.Attributes("ROWID").InnerText).Trim).ToString
                    If strROWID = "" Then
                        strAction = "I"
                    Else
                        strAction = "U"
                    End If

                    strLcode = ((.Attributes("LCODE").InnerText).Trim).ToString
                    If strLcode = "" Then
                        Throw (New AAMSException("Location Code can't be blank"))
                    End If

                    strCPUTYPE = ((.Attributes("CPUTYPE").InnerText).Trim).ToString
                    If strLcode = "" Then
                        Throw (New AAMSException("CPUTYPE can't be blank"))
                    End If

                    If strAction = "" Then
                        Throw (New AAMSException("Invalid Action Code."))
                    End If
                End With

                '---------CHECK BACK DATE CHALLN
                If boolOverrideBackDateChallan = False Then
                    Dim CurrentDate As String
                    CurrentDate = DateTime.Now.Today.ToString("dd/MM/yyyy")

                    CurrentDate = GetDateFormat(CurrentDate, "dd/MM/yyyy", "yyyyMMdd", "/")
                    If strDATE.Trim() <> "" Then
                        If CInt(strDATE) < CInt(CurrentDate) Then
                            strErrorMessage = "Cann''t use Back Dated challan."
                            Throw (New AAMSException(strErrorMessage))
                        End If
                    End If
                End If
                '---------CHECK BACK DATE CHALLN

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_PCINSTALLATION"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction

                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("LCODE").InnerText.Trim = "" Then
                        .Parameters("@LCODE").Value = DBNull.Value
                    Else
                        .Parameters("@LCODE").Value = CInt(strLcode)
                    End If

                    .Parameters.Add(New SqlParameter("@DATE", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("DATE").InnerText.Trim = "" Then
                        .Parameters("@DATE").Value = DBNull.Value
                    Else
                        .Parameters("@DATE").Value = CInt(strDATE)
                    End If

                    .Parameters.Add(New SqlParameter("@CPUTYPE", SqlDbType.Char, 3))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUTYPE").InnerText.Trim = "" Then
                        .Parameters("@CPUTYPE").Value = DBNull.Value
                    Else
                        .Parameters("@CPUTYPE").Value = strCPUTYPE
                    End If

                    .Parameters.Add(New SqlParameter("@CPUNO", SqlDbType.VarChar, 25))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUNO").InnerText.Trim = "" Then
                        .Parameters("@CPUNO").Value = DBNull.Value
                    Else
                        .Parameters("@CPUNO").Value = strCPUNO
                    End If

                    .Parameters.Add(New SqlParameter("@MONTYPE", SqlDbType.Char, 3))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONTYPE").InnerText.Trim = "" Then
                        .Parameters("@MONTYPE").Value = DBNull.Value
                    Else
                        .Parameters("@MONTYPE").Value = strMONTYPE
                    End If

                    .Parameters.Add(New SqlParameter("@MONNO", SqlDbType.VarChar, 25))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONNO").InnerText.Trim = "" Then
                        .Parameters("@MONNO").Value = DBNull.Value
                    Else
                        .Parameters("@MONNO").Value = strMONNO
                    End If

                    .Parameters.Add(New SqlParameter("@KBDTYPE", SqlDbType.Char, 3))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("KBDTYPE").InnerText.Trim = "" Then
                        .Parameters("@KBDTYPE").Value = DBNull.Value
                    Else
                        .Parameters("@KBDTYPE").Value = strKBDTYPE
                    End If

                    .Parameters.Add(New SqlParameter("@KBDNO", SqlDbType.VarChar, 25))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("KBDNO").InnerText.Trim = "" Then
                        .Parameters("@KBDNO").Value = DBNull.Value
                    Else
                        .Parameters("@KBDNO").Value = strKBDNO
                    End If

                    .Parameters.Add(New SqlParameter("@CDRNO", SqlDbType.VarChar, 25))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CDRNO").InnerText.Trim = "" Then
                        .Parameters("@CDRNO").Value = DBNull.Value
                    Else
                        .Parameters("@CDRNO").Value = strCDRNO
                    End If

                    .Parameters.Add(New SqlParameter("@MSETYPE", SqlDbType.Char, 3))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("MSETYPE").InnerText.Trim = "" Then
                        .Parameters("@MSETYPE").Value = DBNull.Value
                    Else
                        .Parameters("@MSETYPE").Value = strMSETYPE
                    End If

                    .Parameters.Add(New SqlParameter("@MSENO", SqlDbType.VarChar, 25))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("MSENO").InnerText.Trim = "" Then
                        .Parameters("@MSENO").Value = DBNull.Value
                    Else
                        .Parameters("@MSENO").Value = strMSENO
                    End If

                    .Parameters.Add(New SqlParameter("@OrderNumber", SqlDbType.Char, 12))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("OrderNumber").InnerText.Trim = "" Then
                        .Parameters("@OrderNumber").Value = DBNull.Value
                    Else
                        .Parameters("@OrderNumber").Value = strOrderNumber
                    End If

                    .Parameters.Add(New SqlParameter("@REMARKS", SqlDbType.VarChar, 100))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("REMARKS").InnerText.Trim = "" Then
                        .Parameters("@REMARKS").Value = DBNull.Value
                    Else
                        .Parameters("@REMARKS").Value = strOrderNumber
                    End If

                    .Parameters.Add(New SqlParameter("@CHALLANNUMBER", SqlDbType.VarChar, 17))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANNUMBER").InnerText.Trim = "" Then
                        .Parameters("@CHALLANNUMBER").Value = DBNull.Value
                    Else
                        .Parameters("@CHALLANNUMBER").Value = strOrderNumber
                    End If

                    .Parameters.Add(New SqlParameter("@LoggedBy", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("LoggedBy").InnerText.Trim = "" Then
                        .Parameters("@LoggedBy").Value = DBNull.Value
                    Else
                        .Parameters("@LoggedBy").Value = CInt(strLoggedBy)
                    End If

                    .Parameters.Add(New SqlParameter("@ROWID", SqlDbType.Int))
                    If UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("ROWID").InnerText.Trim = "" Then
                        .Parameters("@ROWID").Value = DBNull.Value
                    Else
                        .Parameters("@ROWID").Value = CInt(strROWID)
                    End If

                    .Parameters.Add(New SqlParameter("@RETUNID", SqlDbType.Int))
                    .Parameters("@RETUNID").Direction = ParameterDirection.Output
                    .Parameters("@RETUNID").Value = 0

                End With

                objSqlCommand.Connection.Open()
                objTran = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)
                objSqlCommand.Transaction = objTran

                intRecordsAffected = objSqlCommand.ExecuteNonQuery()
                objTran.Commit()


                intRetId = objSqlCommand.Parameters("@RETUNID").Value
                If UCase(strAction) = "I" Then
                    intRetId = objSqlCommand.Parameters("@RETUNID").Value
                    objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    With objUpdateDocOutput.DocumentElement.SelectSingleNode("DETAIL")
                        .Attributes("ROWID").InnerText = intRetId
                    End With
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
                If objSqlConnection.State <> ConnectionState.Closed Then
                    If Not objTran Is Nothing Then
                        objTran.Rollback()
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
            Return objUpdateDocOutput
        End Function
        Public Function GetOrderInstallationHistory(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function gives History of Agency Connectivity.
            'Input  :
            '<MS_GET_ORDERINSTALLATIONHISTORY_INPUT>					
            '       <ROWID/>                    
            '</MS_GET_ORDERINSTALLATIONHISTORY_INPUT>

            'Output :
            '<MS_GET_ORDERINSTALLATIONHISTORY_OUTPUT> 
            '   <DETAIL     DATETIME ='' CPUTYPE ='' CPUNO ='' MONTYPE  = ''  MONNO ='' KBDTYPE = ''
            '               KBDNO ='' CDRNO ='' MSETYPE ='' MSENO = '' ADDLRAM='' IOrderNumber = '' DOrderNumber = '' 
            '               REMARKS  = '' CHALLANNUMBER  = '' ActionTaken = '' UserName = ''  LoggedDateTime= '' / >
            '<Errors Status=''>
            '   <Error Code='' Description='' />
            '</Errors>
            '</MS_GET_ORDERINSTALLATIONHISTORY_OUTPUT>

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim intRowID As String
            Const strMETHOD_NAME As String = "GetOrderInstallationHistory"
            Dim blnRecordFound As Boolean
            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean


            Try
                objOutputXml.LoadXml(strOrderInstallation_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                intRowID = SearchDoc.DocumentElement.SelectSingleNode("ROWID").InnerText.Trim
                If intRowID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
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
                    .CommandText = "UP_GET_TA_PCINSTALLATIONHISTORY"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@ROWID", SqlDbType.Int))
                    .Parameters("@ROWID").Value = intRowID
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
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("DETAIL")

                If objSqlReader.HasRows Then
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.RemoveChild(objAptNode)
                End If

                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("DATETIME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATETIME")) & "")
                    objAptNodeClone.Attributes("CPUTYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CPUTYPE")) & "")
                    objAptNodeClone.Attributes("CPUNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CPUNO")) & "")
                    objAptNodeClone.Attributes("MONTYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MONTYPE")) & "")
                    objAptNodeClone.Attributes("MONNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MONNO")) & "")
                    objAptNodeClone.Attributes("KBDTYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("KBDTYPE")) & "")
                    objAptNodeClone.Attributes("KBDNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("KBDNO")) & "")
                    objAptNodeClone.Attributes("CDRNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CDRNO")) & "")
                    objAptNodeClone.Attributes("ADDLRAM").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ADDLRAM")) & "")
                    objAptNodeClone.Attributes("MSETYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MSETYPE")) & "")
                    objAptNodeClone.Attributes("MSENO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MSENO")) & "")
                    objAptNodeClone.Attributes("IOrderNumber").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("IOrderNumber")) & "")
                    objAptNodeClone.Attributes("DOrderNumber").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DOrderNumber")) & "")
                    objAptNodeClone.Attributes("REMARKS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("REMARKS")) & "")
                    objAptNodeClone.Attributes("CHALLANNUMBER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHALLANNUMBER")) & "")
                    objAptNodeClone.Attributes("ActionTaken").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ACTIONTAKEN")) & "")
                    objAptNodeClone.Attributes("UserName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("USERNAME")) & "")
                    objAptNodeClone.Attributes("LoggedDateTime").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOGGEDDATETIME")) & "")

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                    blnRecordFound = True
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
        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.View
            '***********************************************************************
            'Purpose:This function gives details of PCInstallation.
            'Input  :
            '<UP_TA_VIEW_PCINSTALLATION_INPUT>
            '   <ROWID />
            '</UP_TA_VIEW_PCINSTALLATION_INPUT>"

            'Output :
            '<UP_TA_VIEW_PCINSTALLATION_OUTPUT>
            '   <DETAIL ROWID = '' LCODE ='' DATE ='' CPUTYPE = '' CPUNO ='' MONTYPE ='' MONNO = '' KBDTYPE ='' KBDNO ='' MSETYPE ='' MSENO ='' ADDLRAM='' OrderNumber = '' REMARKS = ''  CHALLANNUMBER = '' CDRNO  = '' PCTYPE ='' />
            '<Errors Status=''>
            '   <Error Code='' Description='' />
            '</Errors>
            '</UP_TA_VIEW_PCINSTALLATION_OUTPUT>

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strRowID As String
            Const strMETHOD_NAME As String = "View"
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(strPCINSTALL_VIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strRowID = IndexDoc.DocumentElement.SelectSingleNode("ROWID").InnerText.Trim
                If strRowID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_PCINSTALLATION"
                    .Connection = objSqlConnection
                    .Parameters.Add("@ACTION", SqlDbType.Char, 1)
                    .Parameters("@ACTION").Value = "S"

                    .Parameters.Add("@ROWID", SqlDbType.BigInt)
                    .Parameters("@ROWID").Value = CInt(strRowID)

                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("DETAIL")
                        .Attributes("ROWID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ROWID")) & "")
                        .Attributes("LCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")) & "")
                        .Attributes("DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE")) & "")
                        .Attributes("CPUTYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CPUTYPE")) & "")
                        .Attributes("CPUNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CPUNO")) & "")
                        .Attributes("MONTYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MONTYPE")) & "")
                        .Attributes("MONNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MONNO")) & "")
                        .Attributes("KBDTYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("KBDTYPE")) & "")
                        .Attributes("KBDNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("KBDNO")) & "")
                        .Attributes("CDRNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CDRNO")) & "")
                        .Attributes("MSETYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MSETYPE")) & "")
                        .Attributes("MSENO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MSENO")) & "")
                        .Attributes("ADDLRAM").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ADDLRAM")) & "")
                        .Attributes("OrderNumber").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OrderNumber")) & "")
                        .Attributes("REMARKS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("REMARKS")) & "")
                        .Attributes("CHALLANNUMBER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHALLANNUMBER")) & "")
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
        Function CheckForNoOfPC(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument

            '***********************************************************************
            'Purpose:This function gives History of Agency Connectivity.
            'Input  :
            '<MS_GET_ORDERS_PC_COUNT_INPUT>					
            '   <LCODE/>                    
            '   <ORDERNUMBER/>
            '   <NEWORDER/>
            '   <NoOfPCs/>
            '   <PCType/>
            '</MS_GET_ORDERS_PC_COUNT_INPUT>

            'Output :
            '<MS_GET_ORDERS_PC_COUNT_OUTPUT> 
            '   <DETAIL  LCODE=''  ORDER_NUMBER =''  NEWORDER=''  TOTAMADEUSPC='' TOTMISC=''  
            '            TOTAGENCYPC=''  MISCINSTALLED=''  PCINSTALLED=''  PCDINSTALLED=''  
            '            MISCDINSTALLED =''  AGENCYINSTALLED =''  AGENCYDINSTALLED ='' />
            '<Errors Status=''>
            '   <Error Code='' Description='' />
            '</Errors>
            '</MS_GET_ORDERS_PC_COUNT_OUTPUT>

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim strLcode As String
            Dim strOrderNumber As String
            Dim strNewNumber As String
            Dim strNoOfPCs As String
            Dim strPCType As String
            Dim CheckNoOfPC As Boolean = True

            '------------VARIABLES USED FOR PC INSTALLATION/ DINSTALLATION VALIDATION
            Dim TotAmadeusPC As Integer
            Dim PCInstalled As Integer
            Dim PCDInstalled As Integer
            Dim TotAgencyPC As Integer
            Dim AgencyInstalled As Integer
            Dim AgencyDInstalled As Integer
            Dim NoOfPCs As Integer
            'END------------VARIABLES USED FOR PC INSTALLATION/ DINSTALLATION VALIDATION

            Const strMETHOD_NAME As String = "CheckForNoOfPC"
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(strOrderPCount_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strLcode = SearchDoc.DocumentElement.SelectSingleNode("LCODE").InnerText.Trim
                strOrderNumber = SearchDoc.DocumentElement.SelectSingleNode("ORDERNUMBER").InnerText.Trim
                strNewNumber = SearchDoc.DocumentElement.SelectSingleNode("NEWORDER").InnerText.Trim
                strNoOfPCs = SearchDoc.DocumentElement.SelectSingleNode("NoOfPCs").InnerText.Trim
                strPCType = SearchDoc.DocumentElement.SelectSingleNode("PCType").InnerText.Trim

                If strLcode = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                If strOrderNumber = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                If strNewNumber = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                If strPCType = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_GET_TA_OTH_ORDERS_PC_COUNT"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.BigInt))
                    .Parameters("@LCODE").Value = CInt(strLcode)

                    .Parameters.Add(New SqlParameter("@ORDERNUMBER", SqlDbType.VarChar, 12))
                    .Parameters("@ORDERNUMBER").Value = strOrderNumber

                    .Parameters.Add(New SqlParameter("@NEWORDER", SqlDbType.Char, 1))
                    .Parameters("@NEWORDER").Value = strNewNumber
                End With

                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("DETAIL")

                If objSqlReader.HasRows Then
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.RemoveChild(objAptNode)
                End If

                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("LCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")) & "")
                    objAptNodeClone.Attributes("ORDER_NUMBER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDER_NUMBER")) & "")
                    objAptNodeClone.Attributes("NEWORDER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NEWORDER")) & "")

                    objAptNodeClone.Attributes("TOTAMADEUSPC").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("TOTAMADEUSPC")) & "")
                    TotAmadeusPC = CInt(Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("TOTAMADEUSPC")) & ""))

                    objAptNodeClone.Attributes("TOTMISC").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("TOTMISC")) & "")

                    objAptNodeClone.Attributes("TOTAGENCYPC").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("TOTAGENCYPC")) & "")
                    TotAgencyPC = CInt(Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("TOTAGENCYPC")) & ""))

                    objAptNodeClone.Attributes("PCINSTALLED").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PCINSTALLED")) & "")
                    PCInstalled = CInt(Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PCINSTALLED")) & ""))
                    objAptNodeClone.Attributes("PCDINSTALLED").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PCDINSTALLED")) & "")
                    PCDInstalled = CInt(Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PCDINSTALLED")) & ""))

                    objAptNodeClone.Attributes("AGENCYINSTALLED").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AGENCYINSTALLED")) & "")
                    AgencyInstalled = CInt(Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AGENCYINSTALLED")) & ""))
                    objAptNodeClone.Attributes("AGENCYDINSTALLED").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AGENCYDINSTALLED")) & "")
                    AgencyDInstalled = CInt(Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AGENCYDINSTALLED")) & ""))

                    objAptNodeClone.Attributes("MISCINSTALLED").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MISCINSTALLED")) & "")
                    objAptNodeClone.Attributes("MISCDINSTALLED").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MISCDINSTALLED")) & "")

                    NoOfPCs = CInt(strNoOfPCs)

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                    blnRecordFound = True
                Loop

                If blnRecordFound = False Then
                    CheckNoOfPC = False
                    bzShared.FillErrorStatus(objOutputXml, "101", "Invalid Order Number!")
                Else
                    'objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"

                    If strPCType = "1" Then 'VALIDATION FOR 1APC
                        If strNewNumber = "T" Then
                            If TotAmadeusPC > 0 Then
                                If TotAmadeusPC = PCInstalled Then
                                    CheckNoOfPC = False
                                    Throw (New AAMSException("Total No.of PC already installed for this Order Number"))
                                End If
                            Else
                                Throw (New AAMSException("No.of PC defined to install for this Order Number is less than or equal to zero"))
                                CheckNoOfPC = False
                            End If
                        Else
                            If TotAmadeusPC > 0 Then
                                If TotAmadeusPC = PCDInstalled Then
                                    CheckNoOfPC = False
                                    Throw (New AAMSException("Total No.of PC already deinstalled for this Order Number"))
                                End If
                            Else
                                CheckNoOfPC = False
                                Throw (New AAMSException("No.of PC defined to deinstall for this Order Number is less than or equal to zero"))
                            End If
                        End If
                    End If
                    If strPCType = "0" Then 'VALIDATION FOR AGENCY PC
                        If strNewNumber = "T" Then
                            If TotAgencyPC > 0 Then
                                If TotAgencyPC = AgencyInstalled Then
                                    CheckNoOfPC = False
                                    Throw (New AAMSException("Total No.of Agency PC already installed for this Order Number"))
                                ElseIf (TotAgencyPC < (AgencyInstalled + NoOfPCs)) Then
                                    CheckNoOfPC = False
                                    Throw (New AAMSException("Now only " & TotAgencyPC - AgencyInstalled & " PC(s) can be installed using this Order Number"))
                                End If
                            Else
                                CheckNoOfPC = False
                                Throw (New AAMSException("No.of Agency PC defined to install for this Order Number is less than or equal to zero"))
                            End If
                        Else
                            If TotAgencyPC > 0 Then
                                If TotAgencyPC = AgencyDInstalled Then
                                    CheckNoOfPC = False
                                    Throw (New AAMSException("Total No.of Agency PC already deinstall for this Order Number"))
                                End If
                            Else
                                CheckNoOfPC = False
                                Throw (New AAMSException("No.of Agency PC defined to deinstall for this Order Number is less than or equal to zero"))
                            End If
                        End If
                    End If
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
        'For Example GetDateFormat(txtdate.Text, "dd/MM/yyyy", "yyyyMMdd", "/")
        Public Function GetDateFormat(ByVal objDate As Object, ByVal dateInFormat As String, ByVal dateOutFormat As String, ByVal dateSepChar As String) As String
            Dim str As String = ""
            If objDate.Trim = "" Then
            Else
                Try
                    If dateInFormat.Equals("yyyyMMdd") Then
                        str = DateTime.ParseExact(objDate, dateInFormat, System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat).ToString(dateOutFormat)
                    Else
                        Dim ln As Integer = objDate.ToString().Length
                        If ln = 8 And dateInFormat.Equals("dd/MM/yyyy") Then
                            dateInFormat = "dd/MM/yy"
                        End If
                        Dim dt As New DateTime()
                        Dim dtfi As New DateTimeFormatInfo
                        dtfi.ShortDatePattern = dateInFormat
                        dtfi.DateSeparator = dateSepChar
                        dt = Convert.ToDateTime(objDate, dtfi)
                        str = dt.ToString(dateOutFormat)
                    End If

                Catch ex As Exception
                    str = "0"
                End Try
            End If
            Return str
        End Function
        Public Function ViewReplacement(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function gives details of PCInstallation.
            'Input  :
            '<UP_TA_VIEW_PCINSTALLATION_INPUT>
            '   <ROWID />
            '</UP_TA_VIEW_PCINSTALLATION_INPUT>"

            'Output :
            '<UP_TA_VIEW_PCINSTALLATION_OUTPUT>
            '   <DETAIL ROWID = '' LCODE ='' DATE ='' CPUTYPE = '' CPUNO ='' MONTYPE ='' MONNO = '' KBDTYPE ='' KBDNO ='' MSETYPE ='' MSENO ='' ADDLRAM='' OrderNumber = '' REMARKS = ''  CHALLANNUMBER = '' CDRNO  = '' PCTYPE ='' />
            '<Errors Status=''>
            '   <Error Code='' Description='' />
            '</Errors>
            '</UP_TA_VIEW_PCINSTALLATION_OUTPUT>

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strRowID As String
            Const strMETHOD_NAME As String = "View"
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(strPCINSTALL_VIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strRowID = IndexDoc.DocumentElement.SelectSingleNode("ROWID").InnerText.Trim
                If strRowID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_PCINSTALLATION"
                    .Connection = objSqlConnection
                    .Parameters.Add("@ACTION", SqlDbType.Char, 1)
                    .Parameters("@ACTION").Value = "V"

                    .Parameters.Add("@ROWID", SqlDbType.BigInt)
                    .Parameters("@ROWID").Value = CInt(strRowID)

                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("DETAIL")
                        .Attributes("ROWID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ROWID")) & "")
                        .Attributes("LCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")) & "")
                        .Attributes("DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE")) & "")
                        .Attributes("CPUTYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CPUTYPE")) & "")
                        .Attributes("CPUNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CPUNO")) & "")
                        .Attributes("MONTYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MONTYPE")) & "")
                        .Attributes("MONNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MONNO")) & "")
                        .Attributes("KBDTYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("KBDTYPE")) & "")
                        .Attributes("KBDNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("KBDNO")) & "")
                        .Attributes("CDRNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CDRNO")) & "")
                        .Attributes("MSETYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MSETYPE")) & "")
                        .Attributes("MSENO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MSENO")) & "")
                        .Attributes("ADDLRAM").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ADDLRAM")) & "")
                        .Attributes("OrderNumber").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OrderNumber")) & "")
                        .Attributes("REMARKS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("REMARKS")) & "")
                        .Attributes("CHALLANNUMBER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHALLANNUMBER")) & "")
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
    End Class
End Namespace
