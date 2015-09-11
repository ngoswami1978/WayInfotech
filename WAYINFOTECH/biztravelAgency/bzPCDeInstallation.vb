'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Ashishsrivastava $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizTravelAgency/bzPCDeInstallation.vb $
'$Workfile: bzPCDeInstallation.vb $
'$Revision: 9 $
'$Archive: /AAMS/Components/bizTravelAgency/bzPCDeInstallation.vb $
'$Modtime: 28/01/11 5:29p $

Imports System.Xml
Imports System.Data
Imports System.Data.SqlClient
Imports AAMS.bizShared
Imports Microsoft.VisualBasic
Imports System.Globalization
Namespace bizTravelAgency
    Public Class bzPCDeInstallation
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzPCDeInstallation"
        Const strPCDEINSTALLATION_INPUT = "<UP_TA_PCDEINSTALLATION_INPUT><DETAIL ACTION ='X' LCODE ='' DATE  = ''   CPUTYPE ='' CPUNO = ''  MONTYPE  = ''  MONNO  = '' KBDTYPE = ''  KBDNO  =''  CDRNO=''  MSETYPE  = '' MSENO  = ''  ADDLRAM= '' OrderNumber  = '' Qty =''  REMARKS  = '' CHALLANDATE  ='' CHALLANNUMBER  = '' LoggedBy  ='' LoggedDateTime  = ''  CHALLANSTATUS  = '' ROWID=''  PCTYPE ='' USE_BACKDATED_CHALLAN='' OVERRIDE_CHALLAN_NO ='' OVERRIDE_CHALLAN_SERIAL_NO='' OVERRIDE_ORDER_NO=''/></UP_TA_PCDEINSTALLATION_INPUT>"
        Const strPCDEINSTALLATION_OUTPUT = "<UP_TA_PCDEINSTALLATION_OUTPUT><DETAIL LCODE ='' DATE  = ''   CPUTYPE ='' CPUNO = ''  MONTYPE  = ''  MONNO  = '' KBDTYPE = ''  KBDNO  ='' CDRNO=''  MSETYPE  = '' MSENO  = ''  ADDLRAM='' OrderNumber  = '' REMARKS  = '' CHALLANDATE  ='' CHALLANNUMBER  = '' LoggedBy  ='' LoggedDateTime  = ''  CHALLANSTATUS  = '' ROWID=''  PCTYPE ='' USE_BACKDATED_CHALLAN ='' OVERRIDE_CHALLAN_NO ='' OVERRIDE_CHALLAN_SERIAL_NO='' OVERRIDE_ORDER_NO='' /><Errors Status=''><Error Code='' Description='' /></Errors></UP_TA_PCDEINSTALLATION_OUTPUT>"
        Const strPCRpt_Deinstallation = "<MS_GETPCDEINSTALLATION_OUTPUT><PCDEINSTALLATION INSTALLATIONDATE='' DEINSTALLATIONDATE='' CPUTYPE='' CPUNO='' MONTYPE='' MONNO='' KBDTYPE='' KBDNO='' MSETYPE='' MSENO='' CDRNO='' INSORDERNUMBER='' DEINSORDERNUMBER='' REMARKS='' CHALLANNUMBER='' LOGGEDBY='' LOGGEDDATETIME='' ROWID='' LCODE=''/><TOTAL A1PC='' AGENCYPC=''/><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_GETPCDEINSTALLATION_OUTPUT>"
        Const strOrderChallanPCDetails = "<MS_ORDER_CHALLAN_PC_DETAILS_OUTPUT><PCDETAILS CHALLANID='' CHALLAN_NUMBER='' LCODE='' SERIAL_NUMBER='' EGROUP_CODE='' FLAG=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_ORDER_CHALLAN_PC_DETAILS_OUTPUT>"

        Public Function CheckOrderChallanSerialAgainstDeInstallation(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function gives search results based on choosen search criterion.
            'Input  :- 
            '<MS_ORDER_CHALLAN_PC_DETAILS_INPUT>
            '	<LCODE>20265</LCODE><ORDERNUMBER>2011/1/2</ORDERNUMBER><ROWID>1224412</ROWID>
            '</MS_ORDER_CHALLAN_PC_DETAILS_INPUT>


            '<MS_ORDER_CHALLAN_PC_DETAILS_OUTPUT>
            '	<PCDETAILS CHALLANID='' CHALLAN_NUMBER='' LCODE='' SERIAL_NUMBER='' EGROUP_CODE='' FLAG=''/>
            '	<Errors Status=''><Error Code='' Description='' /></Errors>
            '</MS_ORDER_CHALLAN_PC_DETAILS_OUTPUT>

            ''************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim blnFlag As Boolean


            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strOrderChallanPCDetails)
            Try


                Dim strLcode As String = ""
                If SearchDoc.DocumentElement.SelectSingleNode("LCODE") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("LCODE").InnerText.Trim() <> "" Then
                        strLcode = SearchDoc.DocumentElement.SelectSingleNode("LCODE").InnerText.Trim()
                    End If
                End If


                Dim strOrder_No As String = ""
                If SearchDoc.DocumentElement.SelectSingleNode("ORDERNUMBER") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("ORDERNUMBER").InnerText.Trim() <> "" Then
                        strOrder_No = SearchDoc.DocumentElement.SelectSingleNode("ORDERNUMBER").InnerText.Trim()
                    End If
                End If


                Dim strRowID As String = ""
                If SearchDoc.DocumentElement.SelectSingleNode("ROWID") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("ROWID").InnerText.Trim() <> "" Then
                        strRowID = SearchDoc.DocumentElement.SelectSingleNode("ROWID").InnerText.Trim()
                    End If
                End If




                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_INV_GET_ORDER_CHALLAN_PC_DETAILS"
                    .Connection = objSqlConnection
                    .Parameters.Add("@LCODE", SqlDbType.Int)
                    .Parameters.Add("@ORDERNUMBER", SqlDbType.VarChar, 20)
                    .Parameters.Add("@ROWID", SqlDbType.Int)

                End With

                With SearchDoc.DocumentElement
                    If (.SelectSingleNode("LCODE").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@LCODE").Value = CInt(.SelectSingleNode("LCODE").InnerXml)
                    End If
                    If (.SelectSingleNode("ORDERNUMBER").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@ORDERNUMBER").Value = .SelectSingleNode("ORDERNUMBER").InnerXml
                    End If
                    If (.SelectSingleNode("ROWID").InnerXml.Trim <> "") Then
                        objSqlCommand.Parameters("@ROWID").Value = CInt(.SelectSingleNode("ROWID").InnerXml)
                    End If

                End With


                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("PCDETAILS")
                objAptNodeClone = objAptNode.CloneNode(True)
                If objSqlReader.HasRows = True Then objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    'Const strOrderChallanPCDetails = "<MS_ORDER_CHALLAN_PC_DETAILS_OUTPUT><PCDETAILS CHALLANID='' CHALLAN_NUMBER='' LCODE='' SERIAL_NUMBER='' EGROUP_CODE='' FLAG=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_ORDER_CHALLAN_PC_DETAILS_OUTPUT>"

                    objAptNodeClone.Attributes("CHALLANID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHALLANID")) & "")
                    objAptNodeClone.Attributes("CHALLAN_NUMBER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHALLAN_NUMBER")) & "")
                    objAptNodeClone.Attributes("LCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")) & "")
                    objAptNodeClone.Attributes("SERIAL_NUMBER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SERIAL_NUMBER")) & "")
                    objAptNodeClone.Attributes("EGROUP_CODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EGROUP_CODE")) & "")

                    objAptNodeClone.Attributes("FLAG").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FLAG")) & "")
                    If Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FLAG")) & "") = 1 Then
                        blnFlag = True
                    Else
                        blnFlag = False
                        Exit Do
                    End If
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop
                objSqlReader.Close()

                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                ElseIf blnFlag = False Then
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "True"
                    objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").InnerText = "Given Serial No :" & objAptNodeClone.Attributes("SERIAL_NUMBER").InnerText & " does not exist in challan/orderNo"
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

        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add

        End Function

        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Delete

        End Function

        Public Function Search(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Search

        End Function

        Public Function Update(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Update
            '***********************************************************************
            'Purpose:This function Inserts/Updates PCDEInstallation.
            'Input  :
            '<UP_TA_PCDEINSTALLATION_INPUT>
            '<DETAIL    ACTION ='X' LCODE ='' DATE  = ''   CPUTYPE ='' CPUNO = ''  MONTYPE  = ''  MONNO  = '' 
            '           KBDTYPE = ''  KBDNO  =''  CDRNO=''  MSETYPE  = '' MSENO  = ''  ADDLRAM='' OrderNumber  = '' Qty =''
            '           REMARKS  = '' CHALLANDATE  ='' CHALLANNUMBER  = '' LoggedBy  ='' LoggedDateTime  = ''  
            '           CHALLANSTATUS  = '' ROWID=''  PCTYPE ='' USE_BACKDATED_CHALLAN='' 
            '           OVERRIDE_CHALLAN_NO ='' OVERRIDE_CHALLAN_SERIAL_NO='' OVERRIDE_ORDER_NO=''/>
            '</UP_TA_PCDEINSTALLATION_INPUT>

            'Output
            '<UP_TA_PCDEINSTALLATION_OUTPUT>
            '<DETAIL    LCODE ='' DATE  = ''   CPUTYPE ='' CPUNO = ''  MONTYPE  = ''  MONNO  = '' KBDTYPE = ''  
            '           KBDNO  ='' CDRNO=''  MSETYPE  = '' MSENO  = ''  ADDLRAM= '' OrderNumber  = '' REMARKS  = '' 
            '           CHALLANDATE  ='' CHALLANNUMBER  = '' LoggedBy  ='' LoggedDateTime  = ''  
            '           CHALLANSTATUS  = '' ROWID=''  PCTYPE ='' USE_BACKDATED_CHALLAN ='' OVERRIDE_CHALLAN_NO ='' 
            '           OVERRIDE_CHALLAN_SERIAL_NO='' OVERRIDE_ORDER_NO='' />
            '<Errors Status=''>
            '   <Error Code='' Description='' />
            '</Errors>
            '</UP_TA_PCDEINSTALLATION_OUTPUT>
            '************************************************************************

            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
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
            Dim strADDLRAM As String = ""
            Dim strOrderNumber As String = ""
            Dim strREMARKS As String = ""
            Dim strCHALLANNUMBER As String = ""
            Dim strLoggedBy As String = ""
            Dim strROWID As String = ""
            Dim strPCTYPE As String = ""
            Dim strErrorMessage As String = ""
            Dim intQty As Integer


            '------Security Related Variables
            Dim boolOverrideBackDateChallan As Boolean = False
            Dim boolOverrideChallanNo As Boolean = False
            Dim boolOverrideChallanSerialNo As Boolean = False
            Dim boolOverrideOrderNo As Boolean = False
            'END------Security Related Variables

            Const strMETHOD_NAME As String = "Update"

            Try
                
                objUpdateDocOutput.LoadXml(strPCDEINSTALLATION_OUTPUT)
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

                    .Attributes("ADDLRAM").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("ADDLRAM").InnerText
                    strADDLRAM = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("ADDLRAM").InnerText

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

                    intQty = Val(Trim(UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("Qty").InnerText & ""))

                End With

                '---------CHECK NUMBER OF PC INSTALLED AND DINSTALLAED IN CASE OF AMADEUS PC AND AGENCY PC
                Dim objInputPCCountXml As New XmlDocument
                'Dim str_INPUT As String = "<MS_GET_ORDERS_PC_COUNT_INPUT><LCODE>10</LCODE><ORDERNUMBER>2004/5/321</ORDERNUMBER><NEWORDER>T</NEWORDER><NoOfPCs>1</NoOfPCs><PCType>1</PCType></MS_GET_ORDERS_PC_COUNT_INPUT>"
                Dim str_INPUT As String = "<MS_GET_ORDERS_PC_COUNT_INPUT><LCODE/><ORDERNUMBER/><NEWORDER/><NoOfPCs/><PCType/></MS_GET_ORDERS_PC_COUNT_INPUT>"
                objInputPCCountXml.LoadXml(str_INPUT)

                With objInputPCCountXml.DocumentElement
                    .SelectSingleNode("LCODE").InnerText = strLcode
                    .SelectSingleNode("ORDERNUMBER").InnerText = strOrderNumber
                    .SelectSingleNode("NEWORDER").InnerText = "f"
                    .SelectSingleNode("NoOfPCs").InnerText = intQty
                    .SelectSingleNode("PCType").InnerText = strPCTYPE
                End With
                Dim ObjInstallation As New bzPCInstallation

                If strOrderNumber.Trim <> "00/00/00" Then
                    objInputPCCountXml = ObjInstallation.CheckForNoOfPC(objInputPCCountXml)
                    If objInputPCCountXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "TRUE" Then
                        strErrorMessage = objInputPCCountXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").InnerText
                        Throw (New AAMSException(strErrorMessage))
                    End If
                End If
                'END---------CHECK NUMBER OF PC INSTALLED AND DINSTALLAED IN CASE OF AMADEUS PC AND AGENCY PC


                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("DETAIL")
                    strROWID = ((.Attributes("ROWID").InnerText).Trim).ToString
                    If strROWID <> "" Then
                        strAction = "X" 'CASE DEINSTALLATION
                    Else
                        Throw (New AAMSException("Invalid Paremeter RowId."))
                    End If
                    If strAction = "" Then
                        Throw (New AAMSException("Invalid Action Code."))
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
                        .Parameters("@ADDLRAM").Value = strADDLRAM
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

                If UCase(strAction) = "X" Then
                    If intRetId <> -1 Then
                        objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                        With objUpdateDocOutput.DocumentElement.SelectSingleNode("DETAIL")
                            .Attributes("ROWID").InnerText = intRetId
                        End With
                    Else
                        Throw (New AAMSException("Unable to update!"))
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

        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.View

        End Function

        Public Function RptDeInstalledPC(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the Misc Installation record, based on the given field value
            'Input  : 
            '<MS_GETPCDEINSTALLATION_INPUT>
            '<LCODE></LCODE><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
            '</MS_GETPCDEINSTALLATION_INPUT>

            'Output :  
            '<MS_GETPCDEINSTALLATION_OUTPUT>
            '	<PCDEINSTALLATION INSTALLATIONDATE="" DEINSTALLATIONDATE="" CPUTYPE="" CPUNO="" MONTYPE="" MONNO="" KBDTYPE="" KBDNO="" MSETYPE="" MSENO="" CDRNO="" INSORDERNUMBER="" DEINSORDERNUMBER="" REMARKS="" CHALLANNUMBER="" LOGGEDBY="" LOGGEDDATETIME=""
            '	                  ROWID="" LCODE=""/>
            '	<TOTAL A1PC="" AGENCYPC=""/>
            '	<ERRORS STATUS="">
            '		<ERROR CODE="" DESCRIPTION=""></ERROR>
            '	</ERRORS>
            '</MS_GETPCDEINSTALLATION_OUTPUT>
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strLCODE As String
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "GetDeInstalledPC"
            Dim intA1PC As Integer = 0
            Dim intAGENCYPC As Integer = 0

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean
            objOutputXml.LoadXml(strPCRpt_Deinstallation)

            Try
                If SearchDoc.DocumentElement.SelectSingleNode("LCODE").InnerText <> "" Then
                    strLCODE = SearchDoc.DocumentElement.SelectSingleNode("LCODE").InnerText
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
                    .CommandText = "[UP_RPT_TA_PCDEINSTALLATION]"
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

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = ""

                    .Parameters.Add(New SqlParameter("@AGENCYPC", SqlDbType.Int))
                    .Parameters("@AGENCYPC").Direction = ParameterDirection.Output
                    .Parameters("@AGENCYPC").Value = 0

                    .Parameters.Add(New SqlParameter("@A1PC", SqlDbType.Int))
                    .Parameters("@A1PC").Direction = ParameterDirection.Output
                    .Parameters("@A1PC").Value = 0

                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("PCDEINSTALLATION")
                objAptNodeClone = objAptNode.CloneNode(True)
                If objSqlReader.HasRows = True Then objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("ROWID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ROWID")))
                    objAptNodeClone.Attributes("LCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")) & "")
                    objAptNodeClone.Attributes("INSTALLATIONDATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("INSDATE")) & "")
                    objAptNodeClone.Attributes("DEINSTALLATIONDATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DEINSDATE")) & "")

                    objAptNodeClone.Attributes("CPUTYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CPUTYPE")) & "")
                    objAptNodeClone.Attributes("CPUNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CPUNO")) & "")

                    If Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CPUTYPE")) & "") = "CPP" And Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MONTYPE")) & "") = "MMP" Then
                        intAGENCYPC = intAGENCYPC + 1
                    Else
                        intA1PC = intA1PC + 1
                    End If

                    objAptNodeClone.Attributes("MONTYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MONTYPE")) & "")
                    objAptNodeClone.Attributes("MONNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MONNO")) & "")
                    objAptNodeClone.Attributes("KBDTYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("KBDTYPE")) & "")
                    objAptNodeClone.Attributes("KBDNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("KBDNO")) & "")
                    objAptNodeClone.Attributes("MSETYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MSETYPE")) & "")
                    objAptNodeClone.Attributes("MSENO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MSENO")) & "")
                    objAptNodeClone.Attributes("CDRNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CDRNO")) & "")
                    objAptNodeClone.Attributes("INSORDERNUMBER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("INSORDERNUMBER")) & "")
                    objAptNodeClone.Attributes("DEINSORDERNUMBER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DEINSORDERNUMBER")) & "")
                    objAptNodeClone.Attributes("REMARKS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("REMARKS")) & "")
                    objAptNodeClone.Attributes("CHALLANNUMBER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHALLANNUMBER")) & "")
                    objAptNodeClone.Attributes("LOGGEDBY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LoggedBy")) & "")
                    objAptNodeClone.Attributes("LOGGEDDATETIME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LoggedDateTime")) & "")

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop

                objSqlReader.Close()
                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found")
                Else
                    objOutputXml.DocumentElement("TOTAL").Attributes("A1PC").InnerText = Val(objSqlCommand.Parameters("@A1PC").Value)
                    objOutputXml.DocumentElement("TOTAL").Attributes("AGENCYPC").InnerText = Val(objSqlCommand.Parameters("@AGENCYPC").Value)
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
