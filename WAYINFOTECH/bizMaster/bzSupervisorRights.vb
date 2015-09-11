'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Varun $
'Purpose   :This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizMaster/bzSupervisorRights.vb $
'$Workfile: bzSupervisorRights.vb $
'$Revision: 2 $
'$Archive: /AAMS/Components/bizMaster/bzSupervisorRights.vb $
'$Modtime: 11/23/07 4:52p $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizMaster
    Public Class bzSupervisorRights
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzSupervisorRights"
        Const gstrList_OUTPUT = "<MS_LISTSUPERVISORY_OUTPUT><Supervisory DomainID='' DomainName='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTSUPERVISORY_OUTPUT>"

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
            'Purpose: List out City 
            'Input  : 
            'Output :  
            '<MS_LISTCITY_OUTPUT>
            '    <CITY CityID="" CityCode="" City_Name="" Country_Name="" />
            '    <Errors Status=''>
            '        <Error Code='' Description='' />
            '    </Errors>
            '</MS_LISTCITY_OUTPUT>
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
                    .CommandText = "UP_LST_MS_SECURITY_DOMAIN"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("Supervisory")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)

                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("DomainID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DomainID")))
                    objAptNodeClone.Attributes("DomainName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DomainName")))
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
    End Class

End Namespace