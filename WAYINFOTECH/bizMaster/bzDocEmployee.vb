'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Neeraj $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizMaster/bzDocEmployee.vb $
'$Workfile: bzDocEmployee.vb $
'$Revision: 3 $
'$Archive: /AAMS/Components/bizMaster/bzDocEmployee.vb $
'$Modtime: 1/03/08 11:04a $
Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared

Namespace bizMaster
    Public Class bzDocEmployee
        Implements bizInterface.BizLayerI

        Const strClass_NAME = "bzDocEmployee"
        Const strDoc_INPUT = "<DOCUMENTEMPLOYEE_INPUT><DOCUMENT Aoffice=''></DOCUMENTEMPLOYEE_INPUT>"
        Const strDoc_EMPLOYEE_OUTPUT = "<DOCUMENTEMPLOYEE_OUTPUT><DOCUMENT DESIGNATION =''  EmployeeName='' /><Errors Status=''><Error Code='' Description=''/></Errors></DOCUMENTEMPLOYEE_OUTPUT>"


        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add

        End Function

        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Delete

        End Function

        Public Function Search(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Search

        End Function

        Public Function Update(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Update

        End Function

        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.View

        End Function
        Public Function List(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the Airline record, based on the given field value
            'Input  : 
            '<DOCUMENTEMPLOYEE_INPUT>
            '   <DOCUMENT Aoffice='' >
            '</DOCUMENTEMPLOYEE_INPUT>

            'Output :  
            '<DOCUMENTEMPLOYEE_OUTPUT>
            '<DOCUMENT DESIGNATION =''  EmployeeName='' />
            '<Errors Status="">
            '   <Error Code='' Description=''/>
            '</Errors>
            '</DOCUMENTEMPLOYEE_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strAoffice As String
            Dim strMETHOD_NAME As String = "List"

            objOutputXml.LoadXml(strDoc_EMPLOYEE_OUTPUT)

            '--------Retrieving & Checking Details from Input XMLDocument ------Start
            strAoffice = IndexDoc.DocumentElement.SelectSingleNode("DOCUMENT").Attributes("Aoffice").InnerText.Trim & ""

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_GET_HD_DOC_EMPLOYEES"
                    .Connection = objSqlConnection
                    .Parameters.Add("@AOFFICE", SqlDbType.Char, 3)

                    If strAoffice = "" Then
                        .Parameters("@AOFFICE").Value = DBNull.Value
                    Else
                        .Parameters("@AOFFICE").Value = strAoffice
                    End If
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument

                Do While objSqlReader.Read
                    Dim blnRecordFound As Boolean
                    If blnRecordFound = False Then
                        With objOutputXml.DocumentElement.SelectSingleNode("DOCUMENT")
                            .Attributes("DESIGNATION").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DESIGNATION")))
                            .Attributes("EmployeeName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMPLOYEENAME")))
                            blnRecordFound = True
                        End With
                    Else
                        objAptNode = objOutputXml.DocumentElement.SelectSingleNode("DOCUMENT")
                        objAptNodeClone = objAptNode.CloneNode(True)
                        objAptNodeClone.Attributes("DESIGNATION").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DESIGNATION")))
                        objAptNodeClone.Attributes("EmployeeName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMPLOYEENAME")))
                        objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    End If
                Loop
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
            objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Return objOutputXml
        End Function
    End Class
End Namespace
