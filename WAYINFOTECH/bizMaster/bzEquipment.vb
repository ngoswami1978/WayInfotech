'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Neeraj $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizMaster/bzEquipment.vb $
'$Workfile: bzEquipment.vb $
'$Revision: 27 $
'$Archive: /AAMS/Components/bizMaster/bzEquipment.vb $
'$Modtime: 9/24/10 12:44p $
Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizMaster
    Public Class bzEquipment
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzEquipment"
        Const srtLIST_OUTPUT = "<MS_LISTEQUIPMENT_OUTPUT><EQUIPMENT PRODUCTID='' EGROUP_CODE ='' EQUIPMENT_CODE='' DESCRIPTION='' MAINTAIN_BALANCE='' MAINTAIN_BALANCE_BY='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTEQUIPMENT_OUTPUT>"

        
        Const strSEARCH_OUTPUT = "<MS_SEARCHEQUIPMENT_OUTPUT><EQUIPMENT EQUIPMENT_CODE='' EGROUP_CODE='' CONFIG='' DESCRIPTION='' MAINTAIN_BALANCE_BY='' CPU_SPEED='' RAM='' VRAM='' HDD='' PRINTER_SPEED='' P_SPEED_MEASURE='' MONITOR_SIZE='' MODEM_SPEED='' LAN_CARD_TYPE='' LAN_CARD_SPEED='' WAN_CARD_TYPE='' PCI_SLOTS='' ISA_SLOTS='' REMARKS='' MAINTAIN_BALANCE='' PRODUCTID='' CDR_SPEED='' SegExpected='' UnitCost=''  NPUnitCost='' LKUnitCost=''	BDUnitCost='' BTUnitCost='' MLUnitCost=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHEQUIPMENT_OUTPUT>"

        Const StrADD_OUTPUT = ""
        Const srtLIST1_OUTPUT = "<MS_LISTEQUIPMENT_OUTPUT><EQUIPMENT PRODUCTID='' EQUIPMENT_CODE=''  DESCRIPTION='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTEQUIPMENT_OUTPUT>"

        Const strUPDATE_INPUT = "<MS_UPDATEEQUIPMENT_INPUT><EQUIPMENT_DETAIL Action='' Productid='' Equipment_Code=''  Egroup_Code=''  Equipment_Cat_ID='' Config='' Description='' Maintain_Balance_By='' Cpu_Speed='' Cdr_Speed='' Ram='' Vram='' Hdd='' Printer_Speed=''   P_Speed_Measure='' Monitor_Size='' Modem_Speed='' Lan_Card_Type='' Lan_Card_Speed='' Wan_Card_Type='' Pci_Slots='' Isa_Slots='' Remarks='' Maintain_Balance=''  SegExpected='' /></MS_UPDATEEQUIPMENT_INPUT>"
        Const strUPDATE_OUTPUT = "<MS_UPDATEEQUIPMENT_INPUT><EQUIPMENT_DETAIL Action='' Productid='' Equipment_Code=''  Egroup_Code='' Equipment_Cat_ID='' Config='' Description='' Maintain_Balance_By='' Cpu_Speed='' Cdr_Speed='' Ram='' Vram='' Hdd='' Printer_Speed=''   P_Speed_Measure='' Monitor_Size='' Modem_Speed='' Lan_Card_Type='' Lan_Card_Speed='' Wan_Card_Type='' Pci_Slots='' Isa_Slots='' Remarks='' Maintain_Balance='' SegExpected='' UnitCost=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEEQUIPMENT_INPUT>"

        Const strDELETE_INPUT = "<MS_DELETEEQUIPMENT_INPUT><Productid></Productid></MS_DELETEEQUIPMENT_INPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETEEQUIPMENT_INPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETEEQUIPMENT_INPUT>"

        Const strVIEW_INPUT = "<MS_VIEWEQUIPMENT_INPUT><Productid></Productid></MS_VIEWEQUIPMENT_INPUT>"
        Const strVIEW_OUTPUT = "<MS_VIEWEQUIPMENT_OUTPUT><EQUIPMENT_DETAIL Productid='' Equipment_Code='' Equipment_Cat_IDS='' Equipment_Cat_ID=''  Egroup_Code='' Config='' Description='' Maintain_Balance_By='' Cpu_Speed='' Cdr_Speed='' Ram='' Vram='' Hdd='' Printer_Speed='' P_Speed_Measure='' Monitor_Size='' Modem_Speed='' Lan_Card_Type='' Lan_Card_Speed='' Wan_Card_Type='' Pci_Slots='' Isa_Slots='' Remarks='' Maintain_Balance=''  SegExpected='' UnitCost='' NPUnitCost='' LKUnitCost=''	BDUnitCost='' BTUnitCost='' MLUnitCost=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWEQUIPMENT_OUTPUT>"

        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add

        End Function

        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Delete

            'INPUT XML
            '<MS_DELETEEQUIPMENT_INPUT>
            '<Productid></Productid>
            '</MS_DELETEEQUIPMENT_INPUT>>

            'OUTPUT XML
            '<MS_DELETEEQUIPMENT_OUTPUT>
            '	<Errors Status=''><Error Code='' Description='' />
            '	</Errors>
            '</MS_DELETEEQUIPMENT__OUTPUT>
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "DELETE"
            Dim intProductID As String
            Dim objDeleteDocOutput As New XmlDocument
            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0
                intProductID = DeleteDoc.DocumentElement.SelectSingleNode("Productid").InnerText.Trim
                If Len(intProductID) <= 0 Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_INV_SRO_EQUIPMENT_MASTER"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"
                    .Parameters.Add(New SqlParameter("@PRODUCTID", SqlDbType.Int))
                    .Parameters("@PRODUCTID").Value = intProductID
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
            '<MS_SEARCHEQUIPMENT_INPUT>
            '	<EGROUP_CODE />
            '	<EQUIPMENT_CODE />
            '   <DESCRIPTION />
            '   <CONFIG />
            '</MS_SEARCHEQUIPMENT_INPUT>

            'Output :
            '<MS_SEARCHEQUIPMENT_OUTPUT>
            '	<EQUIPMENT EQUIPMENT_CODE=''   EGROUP_CODE='' CONFIG='' DESCRIPTION='' MAINTAIN_BALANCE_BY='' CPU_SPEED='' RAM='' VRAM='' HDD='' PRINTER_SPEED='' P_SPEED_MEASURE='' 
            '    MONITOR_SIZE='' MODEM_SPEED='' LAN_CARD_TYPE='' LAN_CARD_SPEED='' WAN_CARD_TYPE='' PCI_SLOTS='' ISA_SLOTS='' REMARKS='' MAINTAIN_BALANCE='' PRODUCTID='' CDR_SPEED='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_SEARCHEQUIPMENT_OUTPUT>

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strEGROUP_CODE As String, strEQUIPMENT_CODE As String, strDESCRIPTION As String, strCONFIG As String

            '-- FOR PAGING VARIABLES
            Dim intRowTotal As Integer
            Dim objDocFrag As XmlDocumentFragment
            Dim objFooterTotalXml As New XmlDocument
            Dim intPAGE_NO As Integer
            Dim intPAGE_SIZE As Integer
            Dim str_SORT_BY As String
            Dim intDESC As Integer



            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strEGROUP_CODE = (SearchDoc.DocumentElement.SelectSingleNode("EGROUP_CODE").InnerText.Trim())
                strEQUIPMENT_CODE = (SearchDoc.DocumentElement.SelectSingleNode("EQUIPMENT_CODE").InnerText.Trim())
                strDESCRIPTION = (SearchDoc.DocumentElement.SelectSingleNode("DESCRIPTION").InnerText.Trim())
                strCONFIG = (SearchDoc.DocumentElement.SelectSingleNode("CONFIG").InnerText.Trim())


                '-- PAGING PARAMETER'S
                If SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim() = "" Then
                    intPAGE_NO = 0
                Else
                    intPAGE_NO = SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim() = "" Then
                    intPAGE_SIZE = 0
                Else
                    intPAGE_SIZE = SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim() = "" Then
                    str_SORT_BY = "AGENCYNAME"
                Else

                    str_SORT_BY = SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim()
                End If

                If UCase(SearchDoc.DocumentElement.SelectSingleNode("DESC").InnerText.Trim()) = "FALSE" Then
                    intDESC = 0
                Else
                    intDESC = 1
                End If


                '--------Retrieving & Checking Details from Input XMLDocument ------End
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SER_MS_EQUIPMENT_MASTER"
                    .Connection = objSqlConnection

                    .Parameters.Add("@EGROUP_CODE", SqlDbType.VarChar, 3)
                    If strEGROUP_CODE <> "" Then
                        .Parameters("@EGROUP_CODE").Value = Trim(strEGROUP_CODE)
                    Else
                        .Parameters("@EGROUP_CODE").Value = DBNull.Value
                    End If

                    .Parameters.Add("@EQUIPMENT_CODE", SqlDbType.VarChar, 3)
                    If strEQUIPMENT_CODE <> "" Then
                        .Parameters("@EQUIPMENT_CODE").Value = Trim(strEQUIPMENT_CODE)
                    Else
                        .Parameters("@EQUIPMENT_CODE").Value = DBNull.Value
                    End If


                    .Parameters.Add("@DESCRIPTION", SqlDbType.VarChar, 25)
                    If strDESCRIPTION <> "" Then
                        .Parameters("@DESCRIPTION").Value = Trim(strDESCRIPTION)
                    Else
                        .Parameters("@DESCRIPTION").Value = DBNull.Value
                    End If

                    .Parameters.Add("@CONFIG", SqlDbType.VarChar, 100)
                    If strCONFIG <> "" Then
                        .Parameters("@CONFIG").Value = Trim(strCONFIG)
                    Else
                        .Parameters("@CONFIG").Value = DBNull.Value
                    End If
                    .Parameters.Add("@PAGE_NO", SqlDbType.Int)
                    If intPAGE_NO = 0 Then
                        .Parameters("@PAGE_NO").Value = DBNull.Value
                    Else
                        .Parameters("@PAGE_NO").Value = intPAGE_NO
                    End If

                    .Parameters.Add("@PAGE_SIZE", SqlDbType.Int)
                    If intPAGE_SIZE = 0 Then
                        .Parameters("@PAGE_SIZE").Value = DBNull.Value
                    Else
                        .Parameters("@PAGE_SIZE").Value = intPAGE_SIZE
                    End If



                    .Parameters.Add("@SORT_BY", SqlDbType.VarChar, 100)
                    If str_SORT_BY = "" Then
                        .Parameters("@SORT_BY").Value = DBNull.Value
                    Else

                        If str_SORT_BY = "SegExpected" Then str_SORT_BY = "SegmentExpected"
                        .Parameters("@SORT_BY").Value = str_SORT_BY
                    End If

                    .Parameters.Add("@DESC", SqlDbType.Int)
                    If intDESC = 0 Then
                        .Parameters("@DESC").Value = 0
                    Else
                        .Parameters("@DESC").Value = 1
                    End If

                    .Parameters.Add("@TOTALROWS", SqlDbType.BigInt)
                    .Parameters("@TOTALROWS").Direction = ParameterDirection.Output
                    .Parameters("@TOTALROWS").Value = 0

                    .Parameters.Add("@TOTALSUMXML", SqlDbType.Xml)
                    .Parameters("@TOTALSUMXML").Direction = ParameterDirection.Output
                    .Parameters("@TOTALSUMXML").Value = ""
                End With

                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("EQUIPMENT_CODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EQUIPMENT_CODE")) & "")
                    objAptNodeClone.Attributes("EGROUP_CODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EGROUP_CODE")) & "")
                    objAptNodeClone.Attributes("CONFIG").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CONFIG")) & "")
                    objAptNodeClone.Attributes("DESCRIPTION").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DESCRIPTION")) & "")
                    objAptNodeClone.Attributes("MAINTAIN_BALANCE_BY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MAINTAIN_BALANCE_BY")) & "")
                    objAptNodeClone.Attributes("CPU_SPEED").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CPU_SPEED")) & "")
                    objAptNodeClone.Attributes("RAM").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("RAM")) & "")
                    objAptNodeClone.Attributes("VRAM").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("VRAM")) & "")
                    objAptNodeClone.Attributes("HDD").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("HDD")) & "")
                    objAptNodeClone.Attributes("PRINTER_SPEED").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRINTER_SPEED")) & "")
                    objAptNodeClone.Attributes("P_SPEED_MEASURE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("P_SPEED_MEASURE")) & "")
                    objAptNodeClone.Attributes("MONITOR_SIZE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MONITOR_SIZE")) & "")
                    objAptNodeClone.Attributes("MODEM_SPEED").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MODEM_SPEED")) & "")
                    objAptNodeClone.Attributes("LAN_CARD_TYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LAN_CARD_TYPE")) & "")
                    objAptNodeClone.Attributes("LAN_CARD_SPEED").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LAN_CARD_SPEED")) & "")
                    objAptNodeClone.Attributes("WAN_CARD_TYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("WAN_CARD_TYPE")) & "")
                    objAptNodeClone.Attributes("PCI_SLOTS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PCI_SLOTS")) & "")
                    objAptNodeClone.Attributes("ISA_SLOTS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ISA_SLOTS")) & "")
                    objAptNodeClone.Attributes("REMARKS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("REMARKS")) & "")
                    objAptNodeClone.Attributes("MAINTAIN_BALANCE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MAINTAIN_BALANCE")) & "")
                    objAptNodeClone.Attributes("PRODUCTID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRODUCTID")) & "")
                    objAptNodeClone.Attributes("CDR_SPEED").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CDR_SPEED")) & "")
                    'SegExpected
                    objAptNodeClone.Attributes("SegExpected").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SEGMENTEXPECTED")) & "")
                    objAptNodeClone.Attributes("UnitCost").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("UnitCost")) & "")
                    objAptNodeClone.Attributes("NPUnitCost").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NPUnitCost")) & "")
                    objAptNodeClone.Attributes("LKUnitCost").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LKUnitCost")) & "")
                    objAptNodeClone.Attributes("BDUnitCost").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("BDUnitCost")) & "")
                    objAptNodeClone.Attributes("BTUnitCost").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("BTUnitCost")) & "")
                    objAptNodeClone.Attributes("MLUnitCost").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MLUnitCost")) & "")


                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop
                objSqlReader.Close()
                intRowTotal = objSqlCommand.Parameters("@TOTALROWS").Value
                If intRowTotal >= 0 Then
                    objFooterTotalXml.LoadXml(objSqlCommand.Parameters("@TOTALSUMXML").Value)
                    objDocFrag = objOutputXml.CreateDocumentFragment()
                    objDocFrag.InnerXml = objFooterTotalXml.DocumentElement.SelectSingleNode("PAGE").OuterXml
                    objOutputXml.DocumentElement.AppendChild(objDocFrag)
                End If

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

        Public Function Update(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Update

            '***********************************************************************
            'Purpose:This function Inserts/Updates Equipment.
            'Input  :
            '<MS_UPDATEEQUIPMENT_INPUT>
            '<EQUIPMENT_DETAIL Action='' Productid='' Equipment_Code='' Equipment_Cat_ID=''  Egroup_Code='' Config='' Description='' Maintain_Balance_By='' Cpu_Speed='' 
            'Cdr_Speed='' Ram='' Vram='' Hdd='' Printer_Speed=''   P_Speed_Measure='' Monitor_Size='' Modem_Speed='' 
            'Lan_Card_Type='' Lan_Card_Speed='' Wan_Card_Type='' Pci_Slots='' Isa_Slots='' Remarks='' Maintain_Balance='' Crd_Speed=''/>
            '</MS_UPDATEEQUIPMENT_INPUT>  


            'Output :
            '<MS_UPDATEEQUIPMENT_OUTPUT>
            '<EQUIPMENT_DETAIL  Config='' Description='' Maintain_Balance_By='' Cpu_Speed='' Equipment_Cat_ID=''
            'Cdr_Speed='' Ram='' Vram='' Hdd='' Printer_Speed=''   P_Speed_Measure='' Monitor_Size='' Modem_Speed='' 
            'Lan_Card_Type='' Lan_Card_Speed='' Wan_Card_Type='' Pci_Slots='' Isa_Slots='' Remarks='' Maintain_Balance=''/>
            '<Errors Status=''><Error Code='' Description='' /></Errors>
            '</MS_UPDATEEQUIPMENT_OUTPUT>
            '************************************************************************
            Dim intRetId As String = ""
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objUpdateDocOutput As New XmlDocument

            Dim intProductid As Integer
            Dim strEquipment_Code As String
            Dim strEquip_cat As String = ""
            Dim strEgroup_Code As String
            Dim strConfig As String
            Dim strDescription As String
            Dim intMaintain_Balance_By As Integer

            Dim strCpu_Speed As String
            Dim strRam As String
            Dim strVram As String
            Dim strHdd As String

            Dim strPrinter_Speed As String
            Dim strP_Speed_Measure As String
            Dim intMonitor_Size As Integer
            Dim strModem_Speed As String

            Dim intLan_Card_Type As Integer
            Dim strLan_Card_Speed As String
            Dim intWan_Card_Type As Integer
            Dim intPci_Slots As Integer
            Dim intIsa_Slots As Integer
            Dim strRemarks As String
            Dim intMaintain_Balance As Integer
            Dim strCDR_SPEED As String
            Dim str_SegExpected As String = ""
            Dim strUnitcost As String = ""
            Dim intRecordsAffected As Int32
            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)

                With objUpdateDocOutput.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL")
                    .Attributes("Action").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Action").InnerText
                    .Attributes("Productid").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Productid").InnerText
                    .Attributes("Equipment_Code").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Equipment_Code").InnerText
                    .Attributes("Egroup_Code").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Egroup_Code").InnerText

                    .Attributes("Config").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Config").InnerText
                    .Attributes("Description").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Description").InnerText
                    .Attributes("Maintain_Balance_By").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Maintain_Balance_By").InnerText
                    .Attributes("Cpu_Speed").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Cpu_Speed").InnerText
                    .Attributes("Cdr_Speed").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Cdr_Speed").InnerText
                    .Attributes("Ram").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Ram").InnerText
                    .Attributes("Vram").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Vram").InnerText
                    .Attributes("Hdd").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Hdd").InnerText
                    .Attributes("Printer_Speed").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Printer_Speed").InnerText
                    .Attributes("P_Speed_Measure").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("P_Speed_Measure").InnerText
                    .Attributes("Monitor_Size").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Monitor_Size").InnerText
                    .Attributes("Modem_Speed").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Modem_Speed").InnerText
                    .Attributes("Lan_Card_Type").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Lan_Card_Type").InnerText
                    .Attributes("Lan_Card_Speed").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Lan_Card_Speed").InnerText
                    .Attributes("Wan_Card_Type").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Wan_Card_Type").InnerText
                    .Attributes("Pci_Slots").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Pci_Slots").InnerText
                    .Attributes("Isa_Slots").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Isa_Slots").InnerText
                    .Attributes("Remarks").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Remarks").InnerText
                    .Attributes("Maintain_Balance").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Maintain_Balance").InnerText
                    'SegExpected
                    .Attributes("SegExpected").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("SegExpected").InnerText
                    If UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("UnitCost") IsNot Nothing Then
                        .Attributes("UnitCost").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("UnitCost").InnerText
                        strUnitcost = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("UnitCost").InnerText.ToString
                    End If
                    If UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Equipment_Cat_ID") IsNot Nothing Then
                        .Attributes("Equipment_Cat_ID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Equipment_Cat_ID").InnerText
                    End If

                End With

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL")
                    strAction = ((.Attributes("Action").InnerText).Trim).ToString


                    If ((.Attributes("Productid").InnerText).Trim = "") Then
                        intProductid = 0
                    Else
                        intProductid = ((.Attributes("Productid").InnerText).Trim)
                    End If


                    If ((.Attributes("Equipment_Code").InnerText).Trim = "") Then
                        strEquipment_Code = vbNullString
                    Else
                        strEquipment_Code = ((.Attributes("Equipment_Code").InnerText).Trim)
                    End If

                    If ((.Attributes("Egroup_Code").InnerText).Trim = "") Then
                        strEgroup_Code = vbNullString
                    Else
                        strEgroup_Code = ((.Attributes("Egroup_Code").InnerText).Trim)
                    End If
                    If UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Equipment_Cat_ID") IsNot Nothing Then
                        If ((.Attributes("Equipment_Cat_ID").InnerText).Trim = "") Then
                            strEquip_cat = ""
                        Else
                            strEquip_cat = ((.Attributes("Equipment_Cat_ID").InnerText).Trim)
                        End If
                    End If

                    'strEquipment_Code = ((.Attributes("Equipment_Code").InnerText).Trim).ToString
                    'strEgroup_Code = ((.Attributes("Egroup_Code").InnerText).Trim).ToString
                    strConfig = ((.Attributes("Config").InnerText).Trim).ToString
                    strDescription = ((.Attributes("Description").InnerText).Trim).ToString

                    'intMaintain_Balance_By = ((.Attributes("Maintain_Balance_By").InnerText).Trim).ToString
                    If ((.Attributes("Maintain_Balance_By").InnerText).Trim = "") Then
                        intMaintain_Balance_By = 0
                    Else
                        intMaintain_Balance_By = ((.Attributes("Maintain_Balance_By").InnerText).Trim)
                    End If

                    'intCpu_Speed = ((.Attributes("Cpu_Speed").InnerText).Trim).ToString

                    If ((.Attributes("Cpu_Speed").InnerText).Trim = "") Then
                        strCpu_Speed = ""
                    Else
                        strCpu_Speed = ((.Attributes("Cpu_Speed").InnerText).Trim)
                    End If

                    ' intCDR_SPEED = ((.Attributes("Cdr_Speed").InnerText).Trim).ToString

                    If ((.Attributes("Cdr_Speed").InnerText).Trim = "") Then
                        strCDR_SPEED = ""
                    Else
                        strCDR_SPEED = ((.Attributes("Cdr_Speed").InnerText).Trim)
                    End If

                    'intRam = ((.Attributes("Ram").InnerText).Trim).ToString

                    If ((.Attributes("Ram").InnerText).Trim = "") Then
                        strRam = ""
                    Else
                        strRam = ((.Attributes("Ram").InnerText).Trim)
                    End If


                    'intVram = ((.Attributes("Vram").InnerText).Trim).ToString

                    If ((.Attributes("Vram").InnerText).Trim = "") Then
                        strVram = ""
                    Else
                        strVram = ((.Attributes("Vram").InnerText).Trim)
                    End If


                    'intHdd = ((.Attributes("Hdd").InnerText).Trim).ToString

                    If ((.Attributes("Hdd").InnerText).Trim = "") Then
                        strHdd = ""
                    Else
                        strHdd = ((.Attributes("Hdd").InnerText).Trim)
                    End If


                    'intPrinter_Speed = ((.Attributes("Printer_Speed").InnerText).Trim).ToString

                    If ((.Attributes("Printer_Speed").InnerText).Trim = "") Then
                        strPrinter_Speed = ""
                    Else
                        strPrinter_Speed = ((.Attributes("Printer_Speed").InnerText).Trim)
                    End If

                    strP_Speed_Measure = ((.Attributes("P_Speed_Measure").InnerText).Trim).ToString

                    'intMonitor_Size = ((.Attributes("Monitor_Size").InnerText).Trim).ToString

                    If ((.Attributes("Monitor_Size").InnerText).Trim = "") Then
                        intMonitor_Size = 0
                    Else
                        intMonitor_Size = ((.Attributes("Monitor_Size").InnerText).Trim)
                    End If


                    'intModem_Speed = ((.Attributes("Modem_Speed").InnerText).Trim).ToString

                    If ((.Attributes("Modem_Speed").InnerText).Trim = "") Then
                        strModem_Speed = ""
                    Else
                        strModem_Speed = ((.Attributes("Modem_Speed").InnerText).Trim)
                    End If



                    'intLan_Card_Type = ((.Attributes("Lan_Card_Type").InnerText).Trim).ToString


                    If ((.Attributes("Lan_Card_Type").InnerText).Trim = "") Then
                        intLan_Card_Type = 0
                    Else
                        intLan_Card_Type = ((.Attributes("Lan_Card_Type").InnerText).Trim)
                    End If


                    'intLan_Card_Speed = ((.Attributes("Lan_Card_Speed").InnerText).Trim).ToString


                    If ((.Attributes("Lan_Card_Speed").InnerText).Trim = "") Then
                        strLan_Card_Speed = ""
                    Else
                        strLan_Card_Speed = ((.Attributes("Lan_Card_Speed").InnerText).Trim)
                    End If


                    'intWan_Card_Type = ((.Attributes("Wan_Card_Type").InnerText).Trim).ToString

                    If ((.Attributes("Wan_Card_Type").InnerText).Trim = "") Then
                        intWan_Card_Type = 0
                    Else
                        intWan_Card_Type = ((.Attributes("Wan_Card_Type").InnerText).Trim)
                    End If


                    'intPci_Slots = ((.Attributes("Pci_Slots").InnerText).Trim).ToString

                    If ((.Attributes("Pci_Slots").InnerText).Trim = "") Then
                        intPci_Slots = 0
                    Else
                        intPci_Slots = ((.Attributes("Pci_Slots").InnerText).Trim)
                    End If

                    'intIsa_Slots = ((.Attributes("Isa_Slots").InnerText).Trim).ToString
                    If ((.Attributes("Isa_Slots").InnerText).Trim = "") Then
                        intIsa_Slots = 0
                    Else
                        intIsa_Slots = ((.Attributes("Isa_Slots").InnerText).Trim)
                    End If



                    strRemarks = ((.Attributes("Remarks").InnerText).Trim).ToString

                    'intMaintain_Balance = ((.Attributes("Maintain_Balance").InnerText).Trim).ToString
                    If ((.Attributes("Maintain_Balance").InnerText).Trim = "") Then
                        intMaintain_Balance = 0
                    Else
                        intMaintain_Balance = ((.Attributes("Maintain_Balance").InnerText).Trim)
                    End If


                    str_SegExpected = ((.Attributes("SegExpected").InnerText).Trim).ToString

                    If strAction = "I" Or strAction = "U" Then

                        If Len(intProductid) <= 0 Then
                            Throw (New AAMSException("Product ID can't be blank."))
                        End If

                        If strEquipment_Code = "" Then
                            Throw (New AAMSException("Equipment Code can't be blank."))
                        End If

                        If strEgroup_Code = "" Then
                            Throw (New AAMSException("Equipment Group can't be blank."))
                        End If
                    Else
                        Throw (New AAMSException("Invalid Action Code."))
                    End If
                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_INV_SRO_EQUIPMENT_MASTER"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction

                    .Parameters.Add(New SqlParameter("@Productid", SqlDbType.BigInt))
                    .Parameters("@Productid").Value = intProductid

                    .Parameters.Add(New SqlParameter("@EQUIPMENT_CODE", SqlDbType.Char, 3))
                    .Parameters("@EQUIPMENT_CODE").Value = strEquipment_Code

                    .Parameters.Add(New SqlParameter("@EGROUP_CODE", SqlDbType.Char, 3))
                    .Parameters("@EGROUP_CODE").Value = strEgroup_Code

                    'Equipment_Cat_ID

                    '@ Change by Neeraj to Insert multiple Category at single time
                    .Parameters.Add(New SqlParameter("@Equipment_Cat_ID", SqlDbType.VarChar, 1000))
                    If strEquip_cat = "" Then
                        .Parameters("@Equipment_Cat_ID").Value = DBNull.Value
                    Else
                        .Parameters("@Equipment_Cat_ID").Value = strEquip_cat
                    End If
                    'end @ Change by Neeraj to Insert multiple Category at single time

                    .Parameters.Add(New SqlParameter("@CONFIG", SqlDbType.Char, 100))
                    .Parameters("@CONFIG").Value = strConfig

                    .Parameters.Add(New SqlParameter("@DESCRIPTION", SqlDbType.Char, 25))
                    .Parameters("@DESCRIPTION").Value = strDescription

                    .Parameters.Add(New SqlParameter("@MAINTAIN_BALANCE_BY", SqlDbType.Int))
                    .Parameters("@MAINTAIN_BALANCE_BY").Value = intMaintain_Balance_By

                    .Parameters.Add(New SqlParameter("@CPU_SPEED", SqlDbType.VarChar, 50))
                    .Parameters("@CPU_SPEED").Value = strCpu_Speed

                    .Parameters.Add(New SqlParameter("@RAM", SqlDbType.VarChar, 50))
                    .Parameters("@RAM").Value = strRam

                    .Parameters.Add(New SqlParameter("@VRAM", SqlDbType.VarChar, 50))
                    .Parameters("@VRAM").Value = strVram

                    .Parameters.Add(New SqlParameter("@HDD", SqlDbType.VarChar, 50))
                    .Parameters("@HDD").Value = strHdd

                    .Parameters.Add(New SqlParameter("@PRINTER_SPEED", SqlDbType.VarChar, 50))
                    .Parameters("@PRINTER_SPEED").Value = strPrinter_Speed

                    .Parameters.Add(New SqlParameter("@P_SPEED_MEASURE", SqlDbType.Char, 2))
                    .Parameters("@P_SPEED_MEASURE").Value = strP_Speed_Measure

                    .Parameters.Add(New SqlParameter("@MONITOR_SIZE", SqlDbType.Int))
                    .Parameters("@MONITOR_SIZE").Value = intMonitor_Size

                    .Parameters.Add(New SqlParameter("@MODEM_SPEED", SqlDbType.VarChar, 50))
                    .Parameters("@MODEM_SPEED").Value = strModem_Speed

                    .Parameters.Add(New SqlParameter("@LAN_CARD_TYPE", SqlDbType.Int))
                    .Parameters("@LAN_CARD_TYPE").Value = intLan_Card_Type

                    .Parameters.Add(New SqlParameter("@LAN_CARD_SPEED", SqlDbType.VarChar, 50))
                    .Parameters("@LAN_CARD_SPEED").Value = strLan_Card_Speed

                    ' .Parameters.Add(New SqlParameter("@LAN_CARD_TYPE", SqlDbType.Int))
                    ' .Parameters("@LAN_CARD_TYPE").Value = intLan_Card_Type

                    .Parameters.Add(New SqlParameter("@WAN_CARD_TYPE", SqlDbType.Int))
                    .Parameters("@WAN_CARD_TYPE").Value = intWan_Card_Type

                    '.Parameters.Add(New SqlParameter("@LAN_CARD_TYPE", SqlDbType.Int))
                    '.Parameters("@LAN_CARD_TYPE").Value = intLan_Card_Type

                    .Parameters.Add(New SqlParameter("@PCI_SLOTS", SqlDbType.Int))
                    .Parameters("@PCI_SLOTS").Value = intPci_Slots

                    .Parameters.Add(New SqlParameter("@ISA_SLOTS", SqlDbType.Int))
                    .Parameters("@ISA_SLOTS").Value = intIsa_Slots

                    .Parameters.Add(New SqlParameter("@REMARKS", SqlDbType.Char, 300))
                    .Parameters("@REMARKS").Value = strRemarks

                    .Parameters.Add(New SqlParameter("@MAINTAIN_BALANCE", SqlDbType.Int))
                    .Parameters("@MAINTAIN_BALANCE").Value = intMaintain_Balance

                    .Parameters.Add(New SqlParameter("@CDR_SPEED", SqlDbType.VarChar, 50))
                    .Parameters("@CDR_SPEED").Value = strCDR_SPEED


                    .Parameters.Add(New SqlParameter("@SEGMENTEXPECTED", SqlDbType.Char, 30))
                    .Parameters("@SEGMENTEXPECTED").Value = str_SegExpected


                    .Parameters.Add("@UNITCOST", SqlDbType.Decimal)
                    If strUnitcost = "" Then
                        .Parameters("@UNITCOST").Value = DBNull.Value
                    Else
                        .Parameters("@UNITCOST").Value = Convert.ToDecimal(strUnitcost)
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
                            Throw (New AAMSException("Equipment Code Already Exists!"))
                        ElseIf intRetId = 0 Then
                            Throw (New AAMSException("Equipment Name Already Exists!"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Egroup_Code").InnerText = strEgroup_Code
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Productid").InnerText = intRetId
                        End If
                    ElseIf UCase(strAction) = "U" Then
                        intRetId = .Parameters("@RETURNID").Value

                        If intRetId = 0 Then
                            Throw (New AAMSException("Equipment Code / Name Already Exists!"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Productid").InnerText = intRetId
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

        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.View
            ' INPUT XML
            '<MS_VIEWEQUIPMENT_INPUT>
            '<Productid></Productid>
            '</MS_VIEWEQUIPMENT_INPUT>


            ' INPUT OUTPUT
            '<MS_VIEWEQUIPMENT_OUTPUT>
            '	<EQUIPMENT_DETAIL Productid='' Equipment_Code='' Equipment_Cat_IDS='' Equipment_Cat_ID='' Egroup_Code='' Config='' Description='' Maintain_Balance_By='' Cpu_Speed='' 
            '	 Cdr_Speed='' Ram='' Vram='' Hdd='' Printer_Speed=''   P_Speed_Measure='' Monitor_Size='' Modem_Speed='' 
            '	 Lan_Card_Type='' Lan_Card_Speed='' Wan_Card_Type='' Pci_Slots='' Isa_Slots='' Remarks='' Maintain_Balance='' />	
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_VIEWEQUIPMENT_OUTPUT>


            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim intProductId As Integer
            Const strMETHOD_NAME As String = "View"

            Try
                objOutputXml.LoadXml(strVIEW_OUTPUT)

                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                intProductId = IndexDoc.DocumentElement.SelectSingleNode("Productid").InnerText.Trim
                If Len(intProductId) <= 0 Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_INV_SRO_EQUIPMENT_MASTER"
                    .Connection = objSqlConnection

                    .Parameters.Add("Productid", SqlDbType.Int)
                    .Parameters("Productid").Value = intProductId

                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"

                    .Parameters.Add("@RETURNID", SqlDbType.Int)
                    .Parameters("@RETURNID").Value = 0

                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    Dim blnRecordFound As Boolean
                    If blnRecordFound = False Then
                        With objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL")
                            '.Attributes("AR_OF_ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AR_OF_ID")))
                            .Attributes("Productid").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Productid")))
                            .Attributes("Equipment_Code").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Equipment_Code")))
                            .Attributes("Equipment_Cat_ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Equipment_Cat_ID")))
                            .Attributes("Equipment_Cat_IDS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EQUIPMENT_CAT_IDS")))

                            .Attributes("Egroup_Code").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Egroup_Code")))
                            .Attributes("Config").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Config")))
                            .Attributes("Description").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Description")))
                            .Attributes("Maintain_Balance_By").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Maintain_Balance_By")))
                            .Attributes("Cpu_Speed").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Cpu_Speed")))
                            .Attributes("Cdr_Speed").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Cdr_Speed")))
                            .Attributes("Ram").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Ram")))
                            .Attributes("Vram").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Vram")))
                            .Attributes("Hdd").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Hdd")))
                            .Attributes("Printer_Speed").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Printer_Speed")))
                            .Attributes("P_Speed_Measure").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("P_Speed_Measure")))
                            .Attributes("Monitor_Size").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Monitor_Size")))
                            .Attributes("Modem_Speed").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Modem_Speed")))
                            .Attributes("Lan_Card_Type").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Lan_Card_Type")))
                            .Attributes("Lan_Card_Speed").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Lan_Card_Speed")))
                            .Attributes("Wan_Card_Type").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Wan_Card_Type")))
                            .Attributes("Pci_Slots").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Pci_Slots")))
                            .Attributes("Isa_Slots").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Isa_Slots")))
                            .Attributes("Remarks").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Remarks")))
                            .Attributes("Maintain_Balance").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Maintain_Balance")))
                            '.Attributes("Crd_Speed").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("EQUIPMENT_DETAIL").Attributes("Crd_Speed.InnerText").InnerText
                            'SegExpected
                            .Attributes("SegExpected").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SEGMENTEXPECTED")))
                            .Attributes("UnitCost").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("UnitCost")) & "")


                            .Attributes("NPUnitCost").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NPUnitCost")) & "")
                            .Attributes("LKUnitCost").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LKUnitCost")) & "")
                            .Attributes("BDUnitCost").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("BDUnitCost")) & "")
                            .Attributes("BTUnitCost").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("BTUnitCost")) & "")
                            .Attributes("MLUnitCost").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MLUnitCost")) & "")


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
            'Purpose: To list out the CRS record, based on the given field value
            'Input  : 
            'Output :  
            '<MS_LISTEQUIPMENT_OUTPUT><EQUIPMENT PRODUCTID='' EGROUP_CODE ='' EQUIPMENT_CODE='' DESCRIPTION='' MAINTAIN_BALANCE_BY='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTEQUIPMENT_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "List"

            objOutputXml.LoadXml(srtLIST_OUTPUT)

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LST_MS_EQUIPMENT_MASTER"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("PRODUCTID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRODUCTID")) & "")
                    objAptNodeClone.Attributes("EGROUP_CODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EGROUP_CODE")) & "")
                    objAptNodeClone.Attributes("EQUIPMENT_CODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EQUIPMENT_CODE")) & "")
                    objAptNodeClone.Attributes("DESCRIPTION").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DESCRIPTION")) & "")
                    objAptNodeClone.Attributes("MAINTAIN_BALANCE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MAINTAIN_BALANCE")) & "")
                    objAptNodeClone.Attributes("MAINTAIN_BALANCE_BY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MAINTAIN_BALANCE_BY")) & "")
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

        Public Function List2() As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the CRS record, based on the given field value
            'Input  : 
            'Output :  
            '<MS_LISTEQUIPMENT_OUTPUT><EQUIPMENT PRODUCTID='' EGROUP_CODE ='' EQUIPMENT_CODE='' DESCRIPTION='' MAINTAIN_BALANCE_BY='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTEQUIPMENT_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "List"

            objOutputXml.LoadXml(srtLIST_OUTPUT)

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LST_MS_EQUIPMENT_MASTER_WITHOUTSCRAP"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("PRODUCTID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRODUCTID")) & "")
                    objAptNodeClone.Attributes("EGROUP_CODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EGROUP_CODE")) & "")
                    objAptNodeClone.Attributes("EQUIPMENT_CODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EQUIPMENT_CODE")) & "")
                    objAptNodeClone.Attributes("DESCRIPTION").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DESCRIPTION")) & "")
                    objAptNodeClone.Attributes("MAINTAIN_BALANCE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MAINTAIN_BALANCE")) & "")
                    objAptNodeClone.Attributes("MAINTAIN_BALANCE_BY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MAINTAIN_BALANCE_BY")) & "")
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

        Public Function List1(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the CRS record, based on the given field value
            'Input  : 
            '<MS_LISTEQUIPMENT_INPUT><EGROUP_CODE/></MS_LISTEQUIPMENT_INPUT>
            'Output :  
            '<MS_LISTEQUIPMENT_OUTPUT><EQUIPMENT PRODUCTID='' EQUIPMENT_CODE='' DESCRIPTION='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTEQUIPMENT_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "List1"
            Dim strEGROUP_CODE As String = ""
            objOutputXml.LoadXml(srtLIST1_OUTPUT)
            Try
                strEGROUP_CODE = IndexDoc.DocumentElement.SelectSingleNode("EGROUP_CODE").InnerText.Trim
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LST_MS_EQUIPMENT_MASTER1"
                    .Connection = objSqlConnection

                    .Parameters.Add("@EGROUP_CODE", SqlDbType.VarChar, 3)
                    If strEGROUP_CODE <> "" Then
                        .Parameters("@EGROUP_CODE").Value = Trim(strEGROUP_CODE)
                    Else
                        .Parameters("@EGROUP_CODE").Value = DBNull.Value
                    End If
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("EQUIPMENT")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("PRODUCTID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRODUCTID")) & "")
                    objAptNodeClone.Attributes("EQUIPMENT_CODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EQUIPMENT_CODE")) & "")
                    objAptNodeClone.Attributes("DESCRIPTION").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DESCRIPTION")) & "")
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
