'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Ashishsrivastava $
'Purpose   :This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizMaster/bzPermission.vb $
'$Workfile: bzPermission.vb $
'$Revision: 9 $
'$Archive: /AAMS/Components/bizMaster/bzPermission.vb $
'$Modtime: 21/09/11 4:55p $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizMaster
    Public Class bzPermission
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzPermission"
        Const gstrList_OUTPUT = "<MS_LISTPERMISSION_OUTPUT><SECURITY_OPTIONS><SECURITY_OPTION Sec_Group='' Sec_Group_ID='' SecurityOptionID='' SecurityOptionSubName=''></SECURITY_OPTION></SECURITY_OPTIONS><Errors Status =''><Error Code='' Description=''></Error></Errors></MS_LISTPERMISSION_OUTPUT>"
        Const gstrListSecGroup_OUTPUT = "<MS_LISTSECURITYGROUP_OUTPUT><SECURITYGROUP Sec_Group='' Sec_Group_ID=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTSECURITYGROUP_OUTPUT>"
        Const gstrListGroup_OUTPUT = "<MS_SECURITY_OUTPUT><SECURITYGROUP SEC_GROUP='' SECURITY_OPTION_SUBNAME=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SECURITY_OUTPUT>"


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
            'Purpose: To list out the Premissions Parameters
            'Output :  
            '<MS_LISTPERMISSION_OUTPUT>
            '   <SECURITY_OPTIONS>
            '       <SECURITY_OPTION Sec_Group="" Sec_Group_ID="" SecurityOptionID="" SecurityOptionSubName=""></SECURITY_OPTION>
            '   </SECURITY_OPTIONS>
            '<Errors Status="">
            '   <Error Code="" Description="">
            '   </Error>
            '</Errors>            
            '</MS_LISTPERMISSION_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strMETHOD_NAME As String = "List"

            objOutputXml.LoadXml(gstrList_OUTPUT)

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LST_MS_PERMISSION"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                Dim blnRecordFound As Boolean
                Do While objSqlReader.Read
                    If blnRecordFound = False Then
                        objAptNode = objOutputXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION")
                        objAptNodeClone = objAptNode.CloneNode(True)
                        objOutputXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS").RemoveChild(objAptNode)
                    End If
                    With objAptNodeClone
                        .Attributes("Sec_Group").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Sec_Group")))
                        .Attributes("Sec_Group_ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Sec_Group_Id")))
                        .Attributes("SecurityOptionID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("securityOptionID")))
                        .Attributes("SecurityOptionSubName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SecurityOptionSubName")))
                        objOutputXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS").AppendChild(objAptNodeClone)
                        objAptNodeClone = objAptNode.CloneNode(True)
                        blnRecordFound = True
                        objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    End With
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
        Public Function ListSecurityGroup() As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the Security Group
            'Output :  
            '<MS_LISTSECURITYGROUP_OUTPUT>
            ' <SECURITYGROUP Sec_Group="" Sec_Group_ID=""/>
            '   <Errors Status=''>
            '        <Error Code='' Description='' />
            '   </Errors>   
            '</MS_LISTSECURITYGROUP_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strMETHOD_NAME As String = "ListSecurityGroup"

            objOutputXml.LoadXml(gstrListSecGroup_OUTPUT)
            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_LST_MS_SECURITY_OPTION_GROUP]"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                Dim blnRecordFound As Boolean

                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("SECURITYGROUP")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)

                'Reading and Appending records into the Output XMLDocument

                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("Sec_Group_ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Sec_Group_ID")))
                    objAptNodeClone.Attributes("Sec_Group").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Sec_Group")))
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
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


        Public Function ListSecurityGroupFilter(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the Security Group
            'Output :  

            '<MS_SECURITY_OUTPUT>
            '<SECURITYGROUP SEC_GROUP='' SECURITY_OPTION_SUBNAME=''/>
            '<Errors Status=''><Error Code='' Description='' /></Errors>
            '</MS_SECURITY_OUTPUT>

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strMETHOD_NAME As String = "ListSecurityGroupFilter"
            Dim strGroupId As String = ""
            objOutputXml.LoadXml(gstrListGroup_OUTPUT)
            Try

                strGroupId = IndexDoc.DocumentElement.SelectSingleNode("GROUPID").InnerText.Trim
                If strGroupId = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_LST_MS_SECURITYNAME]"
                    .Connection = objSqlConnection

                    .Parameters.Add("@SEC_GROUP_ID", SqlDbType.Int)
                    .Parameters("@SEC_GROUP_ID").Value = strGroupId

                End With

                'retrieving the records according to the List Criteria
                Dim blnRecordFound As Boolean

                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("SECURITYGROUP")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)

                'Reading and Appending records into the Output XMLDocument

                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("SEC_GROUP").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SECURITYOPTIONID")))
                    objAptNodeClone.Attributes("SECURITY_OPTION_SUBNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SECURITYOPTIONSUBNAME")))
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
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

            Catch exec As Exception
                'msgbox(exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            'objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Return objOutputXml
        End Function

    End Class
End Namespace
