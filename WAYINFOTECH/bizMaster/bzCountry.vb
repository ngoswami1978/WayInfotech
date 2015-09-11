

'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Varun $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizMaster/bzCountry.vb $
'$Workfile: bzCountry.vb $
'$Revision: 6 $
'$Archive: /AAMS/Components/bizMaster/bzCountry.vb $
'$Modtime: 11/27/07 10:40a $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizMaster
    Public Class bzCountry
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzCountry"
        Const gstrList_OUTPUT = "<MS_LISTCOUNTRY_OUTPUT><COUNTRY CountryID='' Country_Code='' Country_Name='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTCOUNTRY_OUTPUT>"
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
        Public Function List() As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the airport record, based on the given field value
            'Input  : 
            'Output :  
            '<MS_LISTCOUNTRY_OUTPUT>
            '<COUNTRY CountryID='' Country_Code='' Country_Name='' />
            '<Errors Status=''>
            '<Error Code='' Description='' />
            '</Errors>
            '</MS_LISTCOUNTRY_OUTPUT>
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean

            Dim strMETHOD_NAME As String = "List"

            objOutputXml.LoadXml(gstrList_OUTPUT)

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LST_MS_COUNTRY"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("COUNTRY")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)

                Do While objSqlReader.Read
                    objAptNodeClone.Attributes("CountryID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CountryID")))
                    objAptNodeClone.Attributes("Country_Code").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Country_Code")))
                    objAptNodeClone.Attributes("Country_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Country_Name")))
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                    blnRecordFound = True
                Loop
                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found !")
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

