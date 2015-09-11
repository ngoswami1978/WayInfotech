Imports System.Xml
Imports System.Data.SqlClient
Imports WAY.bizShared.bzShared
Namespace bizMaster
    Public Class bzPermission
        Const strClass_NAME = "bzPermission"
        Const strPERMISSIONVIEW_OUTPUT = "<MS_VIEWEMPLOYEEPERMISSION_OUTPUT><SECURITY_OPTIONS><SECURITY_OPTION SecurityOptionID='' Value='' /></SECURITY_OPTIONS><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWEMPLOYEEPERMISSION_OUTPUT>"
        Const gstrList_OUTPUT = "<MS_LISTPERMISSION_OUTPUT><SECURITY_OPTIONS><SECURITY_OPTION Sec_Group='' Sec_Group_ID='' SecurityOptionID='' SecurityOptionSubName=''></SECURITY_OPTION></SECURITY_OPTIONS><Errors Status =''><Error Code='' Description=''></Error></Errors></MS_LISTPERMISSION_OUTPUT>"
        Const gstrListSecGroup_OUTPUT = "<MS_LISTSECURITYGROUP_OUTPUT><SECURITYGROUP Sec_Group='' Sec_Group_ID=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTSECURITYGROUP_OUTPUT>"
        Const gstrListGroup_OUTPUT = "<MS_SECURITY_OUTPUT><SECURITYGROUP SEC_GROUP='' SECURITY_OPTION_SUBNAME=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SECURITY_OUTPUT>"
        Const strPERMISSIONUPDATE_OUTPUT = "<MS_UPDATEEMPLOYEEPERMISSION_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEEMPLOYEEPERMISSION_OUTPUT>"

        Public Function Add() As System.Xml.XmlDocument

        End Function

        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument

        End Function

        Public Function Search(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument

        End Function

        Public Function Update(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument

        End Function

        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument

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
            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strMETHOD_NAME As String = "List"

            objOutputXml.LoadXml(gstrList_OUTPUT)

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LST_MS_W_PERMISSION"
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

            Catch Exec As bizShared.AAMSException
                'CATCHING AAMS EXCEPTIONS
                bizShared.bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch exec As Exception
                'msgbox(exec.ToString)
                bizShared.bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bizShared.bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
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
            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strMETHOD_NAME As String = "ListSecurityGroup"

            objOutputXml.LoadXml(gstrListSecGroup_OUTPUT)
            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_LST_MS_W_SECURITY_OPTION_GROUP]"
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
            Catch Exec As bizShared.AAMSException
                'CATCHING AAMS EXCEPTIONS
                bizShared.bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml

            Catch exec As Exception
                'msgbox(exec.ToString)
                bizShared.bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bizShared.bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
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
            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString())
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
                    Throw (New bizShared.AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_LST_MS_W_SECURITYNAME]"
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
                    bizShared.bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                End If

            Catch Exec As bizShared.AAMSException
                'CATCHING AAMS EXCEPTIONS
                bizShared.bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml

            Catch exec As Exception
                'msgbox(exec.ToString)
                bizShared.bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bizShared.bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            'objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Return objOutputXml
        End Function

        Public Function GetControlsImagesforPermission(ByVal SearchDoc As System.Xml.XmlDocument) As DataSet
            '***********************************************************************
            'Purpose:This function gives details Scanned Document
            'Input  :
            '<TA_GETCONTROLIMAGE_INPUT>
            '<SecurityOptionID></SecurityOptionID>
            '</TA_GETCONTROLIMAGE_INPUT>

            'Output :
            '<TA_GETCONTROLIMAGE_OUTPUT>
            '<Document SecurityOptionID='' Image ='' />
            '<Errors Status=''><Error Code='' Description='' />
            '</Errors>
            '</TA_GETCONTROLIMAGE_OUTPUT>            
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim dsScannedDocument As DataSet
            Dim daScannedDocument As SqlDataAdapter

            Dim strFileNo As Integer
            Dim intLcode As UInteger
            Const strMETHOD_NAME As String = "GetControlImage"
            Try

                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                If SearchDoc.DocumentElement.SelectSingleNode("SecurityOptionID").InnerText.Trim <> "" Then
                    strFileNo = SearchDoc.DocumentElement.SelectSingleNode("SecurityOptionID").InnerText.Trim
                End If

                dsScannedDocument = New DataSet()
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_GET_TA_W_PAGES_CONTROL_IMAGE"
                    .Connection = objSqlConnection

                    .Parameters.Add("@SecurityOptionID", SqlDbType.Int)
                    .Parameters("@SecurityOptionID").Value = strFileNo

                    .Connection.Open()
                End With
                daScannedDocument = New SqlDataAdapter(objSqlCommand)
                daScannedDocument.Fill(dsScannedDocument)


            Catch Exec As bizShared.AAMSException
                'CATCHING AAMS EXCEPTIONS
                Return dsScannedDocument
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                bizShared.bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)

                Return dsScannedDocument
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return dsScannedDocument
        End Function

        Public Function SavePermissions(ByVal UpdateDoc As XmlDocument) As XmlDocument
            '***********************************************************************
            'Purpose:This function SAVE details of employee permission.
            'Input  :
            '<MS_UPDATEEMPLOYEEPERMISSION_INPUT>
            '	<EmployeeID></EmployeeID>'
            '	<SECURITY_OPTIONS>
            '		<SECURITY_OPTION SecurityOptionID="" Value="" />
            '	</SECURITY_OPTIONS>
            '</MS_UPDATEEMPLOYEEPERMISSION_INPUT>
            'Output :
            '<MS_UPDATEEMPLOYEEPERMISSION_OUTPUT>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_UPDATEEMPLOYEEPERMISSION_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objSqlCommand1 As New SqlCommand
            Dim objSqlCommand2 As New SqlCommand
            Dim objTran As SqlTransaction
            Dim objTranHistory As SqlTransaction
            Dim objOutputXml As New XmlDocument
            Dim objPermissionOutputXml As New XmlDocument
            Dim strEMP_ID, strLoginEMP_ID As String
            Dim objNode As XmlNode
            Const strMETHOD_NAME As String = "SavePermissions"
            Dim intCn As Integer = 0
            Dim objHistoryNode As XmlNode
            Dim strchangedData As String = ""
            objOutputXml.LoadXml(strPERMISSIONUPDATE_OUTPUT)

            Try
                strEMP_ID = UpdateDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim
                If UpdateDoc.DocumentElement.SelectSingleNode("LoginEmployeeID") IsNot Nothing Then
                    strLoginEMP_ID = UpdateDoc.DocumentElement.SelectSingleNode("LoginEmployeeID").InnerText.Trim
                End If

                If strEMP_ID = "" Then
                    Throw (New bizShared.AAMSException("Incomplete Parameters"))
                End If

                'Code for history
                Dim objInputXml As New XmlDocument
                objInputXml.LoadXml("<MS_VIEWEMPLOYEEPERMISSION_INPUT><EmployeeID></EmployeeID></MS_VIEWEMPLOYEEPERMISSION_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerXml = strEMP_ID
                'Here Back end Method Call

                objPermissionOutputXml = GetPermissions(objInputXml)
                If objPermissionOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    intCn = objPermissionOutputXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION").Count
                    If intCn <> 0 Then
                        With objSqlCommand2
                            .Connection = objSqlConnection
                            .Connection.Open()
                            objTranHistory = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)

                            .Transaction = objTranHistory

                            .CommandType = CommandType.StoredProcedure
                            .CommandText = "UP_SRO_MS_W_EMP_ACCESSLEVEL_LOG"
                            .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                            .Parameters("@ACTION").Value = "I"
                            .Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.Int))
                            .Parameters.Add(New SqlParameter("@MODIFYBY", SqlDbType.Int))
                            .Parameters.Add(New SqlParameter("@SECURITYOPTIONID", SqlDbType.Int))
                            .Parameters.Add(New SqlParameter("@CHANGEDDATA", SqlDbType.VarChar, 1000))
                            .Parameters("@EmployeeID").Value = strEMP_ID
                            .Parameters("@MODIFYBY").Value = strLoginEMP_ID
                            For Each objHistoryNode In objPermissionOutputXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION")
                                For Each objUpdateNode As XmlNode In UpdateDoc.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionID='" + objHistoryNode.Attributes("SecurityOptionID").Value + "']")
                                    If Convert.ToString(objHistoryNode.Attributes("Value").Value).Trim <> Convert.ToString(objUpdateNode.Attributes("Value").Value).Trim Then
                                        .Parameters("@SECURITYOPTIONID").Value = objHistoryNode.Attributes("SecurityOptionID").Value
                                        strchangedData = SecurityCheck(objHistoryNode.Attributes("Value").Value) & " To " & SecurityCheck(objUpdateNode.Attributes("Value").Value)
                                        .Parameters("@CHANGEDDATA").Value = strchangedData
                                        .ExecuteNonQuery()
                                    End If
                                Next

                            Next
                            objTranHistory.Commit()
                        End With
                        If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                    End If
                End If
                'End code for history

                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_W_EMP_ACCESSLEVEL"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"
                    .Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.Int))
                    .Parameters("@EmployeeID").Value = strEMP_ID
                    .Connection.Open()
                    .ExecuteNonQuery()
                End With

                objTran = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)
                With objSqlCommand1
                    .Connection = objSqlConnection
                    .Transaction = objTran
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_W_EMP_ACCESSLEVEL"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@SecurityOptionID", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@Value", SqlDbType.Int))
                    For Each objNode In UpdateDoc.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION")
                        .Parameters("@ACTION").Value = "I"
                        .Parameters("@EmployeeID").Value = strEMP_ID
                        .Parameters("@SecurityOptionID").Value = objNode.Attributes("SecurityOptionID").InnerText
                        .Parameters("@Value").Value = objNode.Attributes("Value").InnerText
                        .ExecuteNonQuery()
                    Next
                End With
                objTran.Commit()
                objSqlConnection.Close()
                'Checking whether record is deleted successfully or not
                objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Catch Exec As bizShared.AAMSException
                'CATCHING AAMS EXCEPTIONS
                bizShared.bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                'Fill Error Status
                If objSqlConnection.State <> ConnectionState.Closed Then
                    If Not objTranHistory Is Nothing Then
                        objTranHistory.Rollback()
                    End If
                    If Not objTran Is Nothing Then
                        objTran.Rollback()
                    End If
                    objSqlConnection.Close()
                End If
                bizShared.bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bizShared.bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function

        Public Function SecurityCheck(ByVal intValue As Integer) As String
            Dim strReturnValue As String = ""
            Dim ViewRight, AddRight, ModifyRight, DeleteRight, PrintRight As String
            ViewRight = 0 : AddRight = 0 : ModifyRight = 0 : DeleteRight = 0 : PrintRight = 0


            Select Case intValue
                Case 1
                    ViewRight = "1"
                Case 2, 3
                    ViewRight = "1"
                    AddRight = "1"
                Case 4, 5
                    ViewRight = "1"
                    ModifyRight = "1"
                Case 6, 7
                    ViewRight = "1"
                    ModifyRight = "1"
                    AddRight = "1"
                Case 8, 9
                    ViewRight = "1"
                    DeleteRight = "1"
                Case 10, 11
                    ViewRight = "1"
                    DeleteRight = "1"
                    AddRight = "1"
                Case 12, 13
                    ViewRight = "1"
                    DeleteRight = "1"
                    ModifyRight = "1"
                Case 14, 15
                    ViewRight = "1"
                    DeleteRight = "1"
                    ModifyRight = "1"
                    AddRight = "1"
                Case 16, 17
                    ViewRight = "1"
                    PrintRight = "1"
                Case 18, 19
                    ViewRight = "1"
                    PrintRight = "1"
                    AddRight = "1"
                Case 20, 21
                    ViewRight = "1"
                    PrintRight = "1"
                    ModifyRight = "1"
                Case 22, 23
                    ViewRight = "1"
                    PrintRight = "1"
                    AddRight = "1"
                    ModifyRight = "1"
                Case 24, 25
                    ViewRight = "1"
                    PrintRight = "1"
                    DeleteRight = "1"
                Case 26, 27
                    ViewRight = "1"
                    PrintRight = "1"
                    DeleteRight = "1"
                    AddRight = "1"
                Case 28, 29
                    ViewRight = "1"
                    PrintRight = "1"
                    DeleteRight = "1"
                    ModifyRight = "1"
                Case 30, 31
                    ViewRight = "1"
                    PrintRight = "1"
                    DeleteRight = "1"
                    ModifyRight = "1"
                    AddRight = "1"
            End Select

            If ViewRight = 1 Then
                strReturnValue = "View,"
            End If
            If AddRight = 1 Then
                strReturnValue = strReturnValue & "Add,"
            End If
            If ModifyRight = 1 Then
                strReturnValue = strReturnValue & "Modify,"
            End If
            If DeleteRight = 1 Then
                strReturnValue = strReturnValue & "Delete,"
            End If
            If PrintRight = 1 Then
                strReturnValue = strReturnValue & "Print,"
            End If

            If strReturnValue <> "" Then
                strReturnValue = Left(strReturnValue, strReturnValue.Length - 1)
            End If

            Return strReturnValue
        End Function


        Public Function GetPermissions(ByVal IndexDoc As XmlDocument) As XmlDocument
            '***********************************************************************
            'Purpose:This function gives details of employee permissions.
            'Input  :
            '<MS_VIEWEMPLOYEEPERMISSION_INPUT>
            '	<EmployeeID></EmployeeID>
            '</MS_VIEWEMPLOYEEPERMISSION_INPUT>
            'Output :
            '<MS_VIEWEMPLOYEEPERMISSION_OUTPUT>
            '	<SECURITY_OPTIONS>
            '		<SECURITY_OPTION SecurityOptionID="" Value="" />
            '	</SECURITY_OPTIONS>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_VIEWEMPLOYEEPERMISSION_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strEMP_ID As String
            Const strMETHOD_NAME As String = "GetPermissions"
            Dim objEMPNode, objEMPNodeClone As XmlNode
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(strPERMISSIONVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strEMP_ID = IndexDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim
                If strEMP_ID = "" Then
                    Throw (New bizShared.AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_W_EMP_ACCESSLEVEL"
                    .Connection = objSqlConnection
                    .Parameters.Add("@EmployeeID", SqlDbType.Int)
                    .Parameters("@EmployeeID").Value = strEMP_ID
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                objEMPNode = objOutputXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION")
                objEMPNodeClone = objEMPNode.CloneNode(True)
                objOutputXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS").RemoveChild(objEMPNode)

                Do While objSqlReader.Read()
                    'If blnRecordFound = False Then
                    objEMPNodeClone.Attributes("SecurityOptionID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SecurityOptionID")) & "")
                    objEMPNodeClone.Attributes("Value").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Value")) & "")
                    objOutputXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS").AppendChild(objEMPNodeClone)
                    objEMPNodeClone = objEMPNode.CloneNode(True)
                    'End If
                    blnRecordFound = True
                Loop
                If blnRecordFound = False Then
                    bizShared.bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                End If
            Catch Exec As bizShared.AAMSException
                'CATCHING AAMS EXCEPTIONS
                bizShared.bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                bizShared.bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bizShared.bzShared.FillErrorStatus(objOutputXml, "103", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function
    End Class
End Namespace
