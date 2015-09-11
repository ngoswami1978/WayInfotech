'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Neeraj $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizMaster/bzTemplate.vb $
'$Workfile: bzTemplate.vb $
'$Revision: 3 $
'$Archive: /AAMS/Components/bizMaster/bzTemplate.vb $
'$Modtime: 1/03/08 1:06p $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared

Namespace bizMaster
    Public Class bzTemplate
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzTemplate"

        Const strTemplate_INPUT = "<HD_DOCUMENTTEMPLATE_INPUT><TEMPLATES TemplateName='' AOFFICE  ='' AIRLINECODE ='' /></HD_DOCUMENTTEMPLATE_INPUT>"
        Const strTemplate_OUTPUT = "<HD_DOCUMENTTEMPLATE_OUTPUT><TEMPLATES TemplateName='' AOFFICE  ='' AIRLINECODE ='' BDRTemplate = '' T_G_DOC_VERSION =''/><Errors Status=''><Error Code='' Description=''/></Errors></HD_DOCUMENTTEMPLATE_OUTPUT>"

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

        Public Function GetDocumentTemplate(ByVal GetDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '**************************************************************************************************
            'input :
            '<HD_DOCUMENTTEMPLATE_INPUT>
            '   <TEMPLATES TemplateName='' AOFFICE  ='' AIRLINECODE =''/ >
            '</HD_DOCUMENTTEMPLATE_INPUT>

            'output
            '<HD_DOCUMENTTEMPLATE_OUTPUT>
            '<TEMPLATES TemplateName='' AOFFICE  ='' AIRLINECODE ='' BDRTemplate = '' T_G_DOC_VERSION = ''>
            '<Errors Status=''>
            '   <Error Code='' Description=''/>
            '</Errors>
            '</HD_DOCUMENTTEMPLATE_OUTPUT>
            '**************************************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim STRTemplateName As String = ""
            Dim STRAOFFICE As String = ""
            Dim STRAIRLINECODE As String = ""

            Const strMETHOD_NAME As String = "View"
            Dim blnRecordFound As Boolean
            Try
                objOutputXml.LoadXml(strTemplate_OUTPUT)

                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                STRTemplateName = GetDoc.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("TemplateName").InnerText
                STRAOFFICE = GetDoc.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("AOFFICE").InnerText
                STRAIRLINECODE = GetDoc.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("AIRLINECODE").InnerText

                If STRTemplateName = "" Then
                    Throw New Exception("Enter Template Name!")
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_DOCUMENT_TEMPLATES"
                    .Connection = objSqlConnection

                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "V"

                    .Parameters.Add("@TemplateName", SqlDbType.VarChar, 45)
                    .Parameters("@TemplateName").Value = STRTemplateName

                    .Parameters.Add("@AOFFICE", SqlDbType.Char, 3)
                    If STRAOFFICE = "" Then
                        .Parameters("@AOFFICE").Value = DBNull.Value
                    Else
                        .Parameters("@AOFFICE").Value = STRAOFFICE
                    End If

                    .Parameters.Add("@AIRLINECODE", SqlDbType.Char, 2)
                    If STRAIRLINECODE = "" Then
                        .Parameters("@AIRLINECODE").Value = DBNull.Value
                    Else
                        .Parameters("@AIRLINECODE").Value = STRAIRLINECODE
                    End If

                End With

                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("TEMPLATES")
                        .Attributes("BDRTemplate").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("BDRTemplate")) & "")
                        .Attributes("AOFFICE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AOFFICE")) & "")
                        .Attributes("AIRLINECODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AIRLINECODE")) & "")
                        .Attributes("BDRTemplate").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("BDRTemplate")))
                        .Attributes("T_G_DOC_VERSION").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("T_G_DOC_VERSION")) & "")
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
