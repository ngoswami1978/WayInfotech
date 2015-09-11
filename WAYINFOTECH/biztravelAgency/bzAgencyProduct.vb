'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Manojgarg $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizTravelAgency/bzAgencyProduct.vb $
'$Workfile: bzAgencyProduct.vb $
'$Revision: 11 $
'$Archive: /AAMS/Components/bizTravelAgency/bzAgencyProduct.vb $
'$Modtime: 17/12/07 6:07p $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizTravelAgency
    Public Class bzAgencyProduct
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzAgencyProduct"
        Const StrADD_OUTPUT = "<MS_UPDATEAGENCYPRODUCT_INPUT><AGENCYPRODUCT Action='' ROWID='' PRODUCTID='' LCODE='' TERMINALS_ONLINE='' DATE_INSTALLATION='' /></MS_UPDATEAGENCYPRODUCT_INPUT>"
        Const strDELETE_INPUT = "<MS_DELETEAGENCYPRODUCT_INPUT><ROWID></ROWID></MS_DELETEAGENCYPRODUCT_INPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETEAGENCYPRODUCT_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETEAGENCYPRODUCT_OUTPUT>"
        Const strUPDATE_INPUT = "<MS_UPDATEAGENCYPRODUCT_INPUT><AGENCYPRODUCT Action='' ROWID='' PRODUCTID='' LCODE='' TERMINALS_ONLINE='' DATE_INSTALLATION='' /></MS_UPDATEAGENCYPRODUCT_INPUT>"
        Const strUPDATE_OUTPUT = "<MS_UPDATEAGENCYPRODUCT_OUTPUT><AGENCYPRODUCT Action='' ROWID='' PRODUCTID='' LCODE='' TERMINALS_ONLINE='' DATE_INSTALLATION='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEAGENCYPRODUCT_OUTPUT>"
        Const strGETDETAILS_INPUT = "<MS_GETAGENCYPRODUCT_INPUT><LCODE></LCODE></MS_GETAGENCYPRODUCT_INPUT>"
        Const strGETDETAILS_OUTPUT = "<MS_GETAGENCYPRODUCT_OUTPUT><AGENCYPRODUCT ROWID='' PRODUCTID='' PRODUCTNAME='' TERMINALS_ONLINE='' DATE_INSTALLATION='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_GETAGENCYPRODUCT_OUTPUT>"

        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add
            'Declare Output XML String
            '***********************************************************************
            'Purpose: 
            'Input:None
            'Output:Xml Document
            '<MS_UPDATEAGENCYPRODUCT_INPUT><AGENCYPRODUCT Action='' ROWID='' PRODUCTID='' LCODE='' TERMINALS_ONLINE='' DATE_INSTALLATION='' /></MS_UPDATEAGENCYPRODUCT_INPUT>
            '**************************************************************************
            Dim strMETHOD_NAME As String = "Add"
            Dim objXMLDoc As New XmlDocument
            Try
                objXMLDoc.LoadXml(StrADD_OUTPUT)
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
            '<MS_DELETEAGENCYPRODUCT_INPUT>
            '	<ROWID></ROWID>
            '</MS_DELETEAGENCYPRODUCT_INPUT>     
            'Output :
            '<MS_DELETEAGENCYPRODUCT_OUTPUT>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_DELETEAGENCYPRODUCT_OUTPUT>        
            '************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Delete"
            Dim strRowID As String

            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0
                strRowID = DeleteDoc.DocumentElement.SelectSingleNode("ROWID").InnerText.Trim
                If strRowID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_PRODUCTS_INSTALLED"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"
                    .Parameters.Add(New SqlParameter("@ROWID", SqlDbType.Int))
                    .Parameters("@ROWID").Value = strRowID
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
            '---------Not to be implemented----------
        End Function

        Public Function Update(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Update
            '***********************************************************************
            'Purpose:This function SAVE details of Agency Product.
            'Input  :
            '<MS_UPDATEAGENCYPRODUCT_INPUT>
            '	<AGENCYPRODUCT Action='' ROWID='' PRODUCTID='' LCODE='' TERMINALS_ONLINE='' DATE_INSTALLATION='' />
            '</MS_UPDATEAGENCYPRODUCT_INPUT>
            'Output :
            '<MS_UPDATEAGENCYPRODUCT_OUTPUT>
            '	<AGENCYPRODUCT Action='' ROWID='' PRODUCTID='' LCODE='' TERMINALS_ONLINE='' DATE_INSTALLATION='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_UPDATEAGENCYPRODUCT_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objTran As SqlTransaction
            Dim objNode As XmlNode
            Dim objUpdateDocOutput As New XmlDocument
            'Dim objAptNode, objAptNodeClone As XmlNode
            'Dim strLCODE As String
            Dim strAction As String
            'Dim strROWID As String
            'Dim strPRODUCTID As String
            'Dim strTERMINALS_ONLINE As String
            'Dim dtDATE_INSTALLATION As Long
            'Dim intRetId As Integer
            'Dim intRecordsAffected As Int32
            Const strMETHOD_NAME As String = "Update"
            objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)

            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)

                'ADDING PARAMETERS IN STORED PROCEDURE
                objSqlConnection.Open()
                objTran = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)
                With objSqlCommand
                    .Connection = objSqlConnection
                    .Transaction = objTran
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_PRODUCTS_INSTALLED"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters.Add(New SqlParameter("@ROWID", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@PRODUCTID", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@TERMINALS_ONLINE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@DATE_INSTALLATION", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@RETUNID", SqlDbType.Int))
                    .Parameters("@RETUNID").Direction = ParameterDirection.Output
                    .Parameters("@RETUNID").Value = ""


                    For Each objNode In UpdateDoc.DocumentElement.SelectNodes("AGENCYPRODUCT")
                        strAction = objNode.Attributes("Action").InnerText
                        .Parameters("@Action").Value = strAction
                        If strAction = "I" Then
                            .Parameters("@ROWID").Value = vbNullString
                        ElseIf strAction = "U" Or strAction = "D" Then
                            .Parameters("@ROWID").Value = objNode.Attributes("ROWID").InnerText
                        End If
                        If strAction = "I" Or strAction = "U" Or strAction = "D" Then
                            If objNode.Attributes("LCODE").InnerText = "" Then
                                Throw (New AAMSException("Agency Code can't be blank."))
                            ElseIf objNode.Attributes("PRODUCTID").InnerText = "" Then
                                Throw (New AAMSException("Product can't be blank."))
                            ElseIf objNode.Attributes("DATE_INSTALLATION").InnerText = "" Then
                                Throw (New AAMSException("Installation Date can't be blank."))
                            End If
                        Else
                            Throw (New AAMSException("Invalid Action Code."))
                        End If
                        .Parameters("@PRODUCTID").Value = objNode.Attributes("PRODUCTID").InnerText
                        .Parameters("@LCODE").Value = objNode.Attributes("LCODE").InnerText
                        If objNode.Attributes("TERMINALS_ONLINE").InnerText <> "" Then
                            .Parameters("@TERMINALS_ONLINE").Value = objNode.Attributes("TERMINALS_ONLINE").InnerText
                        Else
                            .Parameters("@TERMINALS_ONLINE").Value = DBNull.Value
                        End If
                        If objNode.Attributes("DATE_INSTALLATION").InnerText <> "" Then
                            .Parameters("@DATE_INSTALLATION").Value = objNode.Attributes("DATE_INSTALLATION").InnerText
                            'Else
                            '    .Parameters("@DATE_INSTALLATION").Value = Now.Year & Now.Month & Now.Day
                        End If

                        .ExecuteNonQuery()

                        'objAptNode = objUpdateDocOutput.DocumentElement.SelectSingleNode("AGENCYPRODUCT")
                        'objAptNodeClone = objAptNode.CloneNode(True)
                        'objUpdateDocOutput.DocumentElement.RemoveChild(objAptNode)
                        'With objUpdateDocOutput.DocumentElement.SelectSingleNode("AGENCYPRODUCT")
                        '    objUpdateDocOutput.DocumentElement.SelectSingleNode("AGENCYPRODUCT").Attributes("Action").InnerText = objNode.Attributes("Action").InnerText
                        '    objUpdateDocOutput.DocumentElement.SelectSingleNode("AGENCYPRODUCT").Attributes("ROWID").InnerText = objNode.Attributes("ROWID").InnerText
                        '    objUpdateDocOutput.DocumentElement.SelectSingleNode("AGENCYPRODUCT").Attributes("PRODUCTID").InnerText = objNode.Attributes("PRODUCTID").InnerText
                        '    objUpdateDocOutput.DocumentElement.SelectSingleNode("AGENCYPRODUCT").Attributes("LCODE").InnerText = objNode.Attributes("LCODE").InnerText
                        '    objUpdateDocOutput.DocumentElement.SelectSingleNode("AGENCYPRODUCT").Attributes("TERMINALS_ONLINE").InnerText = objNode.Attributes("TERMINALS_ONLINE").InnerText
                        '    objUpdateDocOutput.DocumentElement.SelectSingleNode("AGENCYPRODUCT").Attributes("DATE_INSTALLATION").InnerText = objNode.Attributes("DATE_INSTALLATION").InnerText
                        'End With
                        'objUpdateDocOutput.DocumentElement.AppendChild(objAptNodeClone)
                        'objAptNodeClone = objAptNode.CloneNode(True)

                    Next
                End With
                objTran.Commit()
                objSqlConnection.Close()

                objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                If objSqlConnection.State <> ConnectionState.Closed Then
                    If Not objTran Is Nothing Then
                        objTran.Rollback()
                    End If
                    objSqlConnection.Close()
                End If
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
                bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objUpdateDocOutput
        End Function

        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.View
            '---------Not to be implemented----------
        End Function

        Public Function GetDetails(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the Agency product, based on the given field value
            'Input  : 
            '<MS_GETAGENCYPRODUCT_INPUT>
            '	<LCODE></LCODE>
            '</MS_GETAGENCYPRODUCT_INPUT>
            'Output :  
            '<MS_GETAGENCYPRODUCT_OUTPUT>
            '	<AGENCYPRODUCT ROWID='' PRODUCTID='' PRODUCTNAME='' TERMINALS_ONLINE='' DATE_INSTALLATION='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_GETAGENCYPRODUCT_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strLCODE As String
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "GetDetails"

            objOutputXml.LoadXml(strGETDETAILS_OUTPUT)

            Try
                strLCODE = SearchDoc.DocumentElement.SelectSingleNode("LCODE").InnerText.Trim
                If strLCODE = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_PRODUCTS_INSTALLED"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters.Add(New SqlParameter("@ROWID", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@PRODUCTID", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@TERMINALS_ONLINE", SqlDbType.Int))
                    .Parameters("@Action").Value = "S"
                    .Parameters("@ROWID").Value = vbNullString
                    .Parameters("@PRODUCTID").Value = vbNullString
                    .Parameters("@LCODE").Value = strLCODE
                    .Parameters("@TERMINALS_ONLINE").Value = vbNullString
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AGENCYPRODUCT")
                objAptNodeClone = objAptNode.CloneNode(True)
                If objSqlReader.HasRows = True Then objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("ROWID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ROWID")) & "")
                    objAptNodeClone.Attributes("PRODUCTID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRODUCTID")) & "")
                    objAptNodeClone.Attributes("PRODUCTNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRODUCTNAME")) & "")
                    objAptNodeClone.Attributes("TERMINALS_ONLINE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("TERMINALS_ONLINE")) & "")
                    objAptNodeClone.Attributes("DATE_INSTALLATION").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE_INSTALLATION")) & "")
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
