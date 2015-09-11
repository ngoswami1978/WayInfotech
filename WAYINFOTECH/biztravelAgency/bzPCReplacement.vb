'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Neeraj $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizTravelAgency/bzPCReplacement.vb $
'$Workfile: bzPCReplacement.vb $
'$Revision: 9 $
'$Archive: /AAMS/Components/bizTravelAgency/bzPCReplacement.vb $
'$Modtime: 27/03/09 12:18p $

Imports System.Xml
Imports System.Data
Imports System.Data.SqlClient
Imports AAMS.bizShared
Imports Microsoft.VisualBasic
Imports System.Globalization
Namespace bizTravelAgency
    Public Class bzPCReplacement
        Implements bizInterface.BizLayerI

        Const strClass_NAME = "bzPCReplacement"
        Const strPCINSTALL_VIEW_OUTPUT = "<UP_TA_VIEW_PCINSTALLATION_OUTPUT><DETAIL ROWID = '' LCODE ='' DATE ='' CPUTYPE = '' CPUNO ='' MONTYPE ='' MONNO = '' KBDTYPE ='' KBDNO ='' ADDLRAM='' MSETYPE ='' MSENO ='' OrderNumber = '' REMARKS = ''  CHALLANNUMBER = '' CDRNO  = '' PCTYPE ='' /><Errors Status=''><Error Code='' Description='' /></Errors></UP_TA_VIEW_PCINSTALLATION_OUTPUT>"
        Const strPCREPLACEMENT_INPUT = "<UP_TA_PCREPLACEMENT_INPUT><DETAIL ACTION ='R' LCODE ='' DATE  = ''   CPUTYPE ='' CPUNO = ''  MONTYPE  = ''  MONNO  = '' KBDTYPE = ''  KBDNO  =''  CDRNO=''  MSETYPE  = '' MSENO  = ''  ADDLRAM=''  OrderNumber  = '' Qty ='' REMARKS  = '' CHALLANDATE  ='' CHALLANNUMBER  = '' LoggedBy  ='' LoggedDateTime  = ''  CHALLANSTATUS  = '' ROWID=''  PCTYPE ='' USE_BACKDATED_CHALLAN ='' OVERRIDE_CHALLAN_NO ='' OVERRIDE_CHALLAN_SERIAL_NO='' OVERRIDE_ORDER_NO=''	/></UP_TA_PCREPLACEMENT_INPUT>"
        Const strPCREPLACEMENT_OUTPUT = "<UP_TA_PCREPLACEMENT_OUTPUT><DETAIL ACTION = '' LCODE ='' DATE  = ''   CPUTYPE ='' CPUNO = ''  MONTYPE  = ''  MONNO  = '' KBDTYPE = ''  KBDNO  ='' CDRNO=''  MSETYPE  = '' MSENO  = ''  ADDLRAM= '' OrderNumber  = '' REMARKS  = '' CHALLANDATE  ='' CHALLANNUMBER  = '' LoggedBy  ='' LoggedDateTime  = ''  CHALLANSTATUS  = '' ROWID=''  PCTYPE ='' USE_BACKDATED_CHALLAN ='' OVERRIDE_CHALLAN_NO ='' OVERRIDE_CHALLAN_SERIAL_NO='' OVERRIDE_ORDER_NO='' /><Errors Status=''><Error Code='' Description='' /></Errors></UP_TA_PCREPLACEMENT_OUTPUT>"


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
            '<DETAIL    ACTION ='R' LCODE ='' DATE  = ''   CPUTYPE ='' CPUNO = ''  MONTYPE  = ''  MONNO  = '' 
            '           KBDTYPE = ''  KBDNO  =''  CDRNO=''  MSETYPE  = '' MSENO  = ''  ADDLRAM=''  OrderNumber  = '' Qty =''
            '           REMARKS  = '' CHALLANDATE  ='' CHALLANNUMBER  = '' LoggedBy  ='' LoggedDateTime  = ''  
            '           CHALLANSTATUS  = '' ROWID=''  PCTYPE ='' USE_BACKDATED_CHALLAN='' 
            '           OVERRIDE_CHALLAN_NO ='' OVERRIDE_CHALLAN_SERIAL_NO='' OVERRIDE_ORDER_NO=''/>
            '</UP_TA_PCDEINSTALLATION_INPUT>

            'Output
            '<UP_TA_PCDEINSTALLATION_OUTPUT>
            '<DETAIL    LCODE ='' DATE  = ''   CPUTYPE ='' CPUNO = ''  MONTYPE  = ''  MONNO  = '' KBDTYPE = ''  
            '           KBDNO  ='' CDRNO=''  MSETYPE  = '' MSENO  = ''  ADDLRAM=''  OrderNumber  = '' REMARKS  = '' 
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

            '------Security Related Variables
            Dim boolOverrideBackDateChallan As Boolean = False
            Dim boolOverrideChallanNo As Boolean = False
            Dim boolOverrideChallanSerialNo As Boolean = False
            Dim boolOverrideOrderNo As Boolean = False
            'END------Security Related Variables

            Const strMETHOD_NAME As String = "Update"

            Try

                objUpdateDocOutput.LoadXml(strPCREPLACEMENT_OUTPUT)
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
                    strADDLRAM = UpdateDoc.DocumentElement.SelectSingleNode("DETAIL").Attributes("ADDLRAM").InnerText

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

                End With

                ' ''---------CHECKNUMBER OF PC INSTALLED AND DINSTALLAED IN CASE OF AMADEUS PC AND AGENCY PC
                ''Dim objInputPCCountXml As New XmlDocument
                ' ''Dim str_INPUT As String = "<MS_GET_ORDERS_PC_COUNT_INPUT><LCODE>10</LCODE><ORDERNUMBER>2004/5/321</ORDERNUMBER><NEWORDER>T</NEWORDER><NoOfPCs>1</NoOfPCs><PCType>1</PCType></MS_GET_ORDERS_PC_COUNT_INPUT>"
                ''Dim str_INPUT As String = "<MS_GET_ORDERS_PC_COUNT_INPUT><LCODE/><ORDERNUMBER/><NEWORDER/><NoOfPCs/><PCType/></MS_GET_ORDERS_PC_COUNT_INPUT>"
                ''objInputPCCountXml.LoadXml(str_INPUT)

                ''With objInputPCCountXml.DocumentElement
                ''    .SelectSingleNode("LCODE").InnerText = strLcode
                ''    .SelectSingleNode("ORDERNUMBER").InnerText = strOrderNumber
                ''    .SelectSingleNode("NEWORDER").InnerText = "T"
                ''    .SelectSingleNode("NoOfPCs").InnerText = 0
                ''    .SelectSingleNode("PCType").InnerText = strPCTYPE
                ''End With
                ''Dim ObjInstallation As New bzPCInstallation
                ''objInputPCCountXml = ObjInstallation.CheckForNoOfPC(objInputPCCountXml)
                ''If objInputPCCountXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "TRUE" Then
                ''    strErrorMessage = objInputPCCountXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").InnerText
                ''    Throw (New AAMSException(strErrorMessage))
                ''End If
                ' ''END---------CHECKNUMBER OF PC INSTALLED AND DINSTALLAED IN CASE OF AMADEUS PC AND AGENCY PC


                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("DETAIL")
                    strROWID = ((.Attributes("ROWID").InnerText).Trim).ToString
                    If strROWID <> "" Then
                        strAction = "R" 'CASE PCREPLACEMENT
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

                If UCase(strAction) = "R" Then
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

    End Class
End Namespace