
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Math

Namespace bizMaster
    Public Class bzEmployee
        Const strClass_NAME = "bzEmployee"
        Const strCHANGEPASSWORD_OUTPUT = "<MS_CHANGEPASSWORD_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_CHANGEPASSWORD_OUTPUT>"
        Const strEMPLOYEEHISTORY_OUTPUT = "<MS_HISTORY_EMPLOYEEPERMISSION_OUTPUT><EMPLOYEE USER='' LOGDATE='' Sec_Group='' SEC_GROUP_ID='' SecurityOptionSubName='' CHANGEDDATA=''/><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description=''/></Errors></MS_HISTORY_EMPLOYEEPERMISSION_OUTPUT>"
        Const strLOGIN_OUTPUT = "<MS_LOGIN_OUTPUT><Administrator></Administrator><EmployeeID></EmployeeID><EmailID></EmailID><Login></Login><Employee_Name></Employee_Name><Aoffice></Aoffice><DepartmentID></DepartmentID><Designation></Designation><Limited_To_OwnAgency></Limited_To_OwnAgency><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><SecurityRegionID></SecurityRegionID><ManagerID></ManagerID><DSR_TARGET_DAYS_CHECK></DSR_TARGET_DAYS_CHECK><DSR_TARGET_DAYS></DSR_TARGET_DAYS><ImmediateSupervisorID></ImmediateSupervisorID><FirstForm></FirstForm><ChangePassword></ChangePassword><FirstLoginDone></FirstLoginDone><ForceToChangePassword></ForceToChangePassword><Request></Request><Manager></Manager><SECURITY_OPTIONS><SECURITY_OPTION SecurityOptionID='' SecurityOptionSubName='' Value='' /></SECURITY_OPTIONS><DisplayFirstForm><FirstFormDetails ID='' Name='' URL='' Module='' /></DisplayFirstForm><HelpDesk_Default><HD_Default FUN_ASSIGNEDTO='' TEC_ASSIGNEDTO='' CONTACT_TYPE_ID='' TEC_CONTACT_TYPE_ID=''/></HelpDesk_Default><Sales_Default><SL_Default TARGET_CHECK='' VISIT_TARGET_DAYS=''/></Sales_Default><Errors Status=''><Error Code='' Description='' /></Errors></MS_LOGIN_OUTPUT>"
        Const strReserveUsernamePassword_OUTPUT = "<MS_RESERVE_USERNAME_PASSWORD_OUTPUT><RESERVE  FIELD_VALUE=''/><Errors Status='False'><Error Code='' Description=''/></Errors></MS_RESERVE_USERNAME_PASSWORD_OUTPUT>"
        Const srtVIEW_OUTPUT = "<MS_VIEWEMPLOYEE_OUTPUT><Employee EmployeeID='' Cell_phone='' Login='' Password='' Email='' Employee_name='' Firstform=''  Changepassword ='' Pwdexpire ='' ContactPersonName='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWEMPLOYEE_OUTPUT>"
        Const strSEARCH_OUTPUT = "<MS_SEARCHEMPLOYEE_OUTPUT><Employee EmployeeID='' Employee_Name='' City_Name='' Login='' IPRestriction='' Request='' Cell_Phone='' Email='' ContactPersonName=''  /><PAGE PAGE_COUNT='' TOTAL_ROWS='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHEMPLOYEE_OUTPUT>"
        Const strUPDATE_OUTPUT = "<MS_UPDATEEMPLOYEE_OUTPUT><Employee Cell_Phone='' Login='' Email='' Employee_name='' EmployeeID='' Password='' Firstform='' Changepassword='' Pwdexpire='' IPrestriction='' ContactPersonName=''/><Errors Status=''><Error Code='' Description=''/></Errors></MS_UPDATEEMPLOYEE_OUTPUT>"
        Const strDELETE_OUTPUT = "<EMP_DELETED_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></EMP_DELETED_OUTPUT>"
        Const srtLIST_OUTPUT = "<MS_LISTMPLOYEE_OUTPUT><EMPLOYEE EMPLOYEEID='' EMPLOYEENAME='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTMPLOYEE_OUTPUT>"


        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Input

            '<SL_DELETED_STRATEGIC_VISIT_STATUS_INPUT>
            '	<SV_STATUSID />
            '</SL_DELETED_STRATEGIC_VISIT_STATUS_INPUT>


            ' 'Output

            '<EMP_DELETED_OUTPUT>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</EMP_DELETED_OUTPUT>

            '************************************************
            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Delete"
            Dim strEmployeeID As String

            Dim objDeleteDocOutput As New XmlDocument
            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)
            Try
                intRecordsAffected = 0
                strEmployeeID = DeleteDoc.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText.Trim
                If strEmployeeID = "" Then
                    'Throw (New AAMSException("Incomplete Parameters"))
                    Throw (New bizShared.AAMSException("Employee Name can't be blank."))
                End If

                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_W_EMPLOYEES"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"

                    .Parameters.Add(New SqlParameter("@EMPLOYEEID", SqlDbType.Int))
                    .Parameters("@EMPLOYEEID").Value = CInt(strEmployeeID)
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
                    ' Call bzShared.FillErrorStatus(objDeleteDocOutput, "101", "Record has not been deleted!")
                    Return (objDeleteDocOutput)
                End If
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                'Fill Error Status
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objDeleteDocOutput

        End Function
        Public Function Login(ByVal IndexDoc As XmlDocument) As XmlDocument
            '***********************************************************************
            'Purpose:This function authenticates user.
            'Input  :
            '<MS_LOGIN_INPUT>
            '	<Login></Login>
            '	<Password></Password>
            '   <IPAddress></IPAddress>
            '</MS_LOGIN_INPUT>

            'Output :
            '  <MS_LOGIN_OUTPUT>
            '       <Administrator></Administrator>
            '	    <EmployeeID></EmployeeID>
            '	    <EmailID></EmailID>
            '	    <Login></Login>
            '	    <Employee_Name></Employee_Name>
            '	    <Aoffice></Aoffice>
            '	    <DepartmentID></DepartmentID>
            '	    <Designation></Designation>
            '	    <Limited_To_OwnAgency></Limited_To_OwnAgency>
            '	    <Limited_To_Aoffice></Limited_To_Aoffice>
            '	    <Limited_To_Region></Limited_To_Region>
            '	    <SecurityRegionID></SecurityRegionID>
            '	    <ManagerID></ManagerID>
            '       <Manager></Manager>
            '	    <ImmediateSupervisorID></ImmediateSupervisorID>
            '       <FirstForm></FirstForm>
            '       <ChangePassword></ChangePassword>
            '       <FirstLoginDone></FirstLoginDone>
            '       <ForceToChangePassword></ForceToChangePassword>    
            '       <Request></Request>    
            '	<SECURITY_OPTIONS>
            '		    <SECURITY_OPTION SecurityOptionID='' SecurityOptionSubname='' Value="" />
            '	</SECURITY_OPTIONS>
            '   <DisplayFirstForm>
            '	    <FirstFormDetails ID='' Name='' URL='' Module='' />
            '   </DisplayFirstForm>
            '   <HelpDesk_Default>
            '	    <HD_Default FUN_ASSIGNEDTO='' TEC_ASSIGNEDTO='' CONTACT_TYPE_ID=''/>
            '   </HelpDesk_Default>
            '   <Sales_Default>
            '	    <SL_Default TARGET_CHECK='' VISIT_TARGET_DAYS=''/>
            '   </Sales_Default>
            '	    <Errors Status="FALSE">
            '		    <Error Code="" Description="" />
            '	    </Errors>
            '</MS_LOGIN_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strLogin As String
            Dim strPassword As String
            Dim strIPAddress As String
            Dim blnRecordFound As Boolean
            Dim objAptNode As XmlNode, objAptNodeClone As XmlNode
            Dim objAptNodeNew As XmlNode, objAptNodeCloneNew As XmlNode
            'Dim onjCrypto As New AAMS.bizCryptography.bzCryptography
            Const strMETHOD_NAME As String = "login"
            Try
                objOutputXml.LoadXml(strLOGIN_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strLogin = IndexDoc.DocumentElement.SelectSingleNode("Login").InnerText.Trim
                strPassword = IndexDoc.DocumentElement.SelectSingleNode("Password").InnerText.Trim
                strIPAddress = IndexDoc.DocumentElement.SelectSingleNode("IPAddress").InnerText.Trim
                If strLogin = "" Or strPassword = "" Then
                    Throw (New bizShared.AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_OTH_MS_W_USER_AUTHENTICATION]"
                    .Connection = objSqlConnection
                    .Parameters.Add("@USER_LOGIN", SqlDbType.VarChar, 25)
                    .Parameters("@USER_LOGIN").Value = strLogin

                    .Parameters.Add("@USER_PASSWORD", SqlDbType.VarChar, 100)
                    .Parameters("@USER_PASSWORD").Value = strPassword 'onjCrypto.Encrypt(strPassword)

                    .Parameters.Add("@IPADDRESS", SqlDbType.VarChar, 15)
                    .Parameters("@IPADDRESS").Value = strIPAddress

                End With
                ' RETRIVEING RECORD THROUGH STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    If blnRecordFound = False Then
                        objOutputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmployeeID")))
                        objOutputXml.DocumentElement.SelectSingleNode("EmailID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Email")) & "")
                        objOutputXml.DocumentElement.SelectSingleNode("Login").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Login")))
                        objOutputXml.DocumentElement.SelectSingleNode("Administrator").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Administrator")))
                        objOutputXml.DocumentElement.SelectSingleNode("Employee_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Employee_Name")))
                    End If
                    blnRecordFound = True
                Loop
                If blnRecordFound = True Then
                    objAptNode = objOutputXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION")
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS").RemoveChild(objAptNode)
                    objSqlReader.NextResult()
                    Do While objSqlReader.Read()
                        objAptNodeClone.Attributes("SecurityOptionID").Value = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SecurityOptionID")))
                        objAptNodeClone.Attributes("SecurityOptionSubName").Value = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SecurityOptionSubname")))
                        objAptNodeClone.Attributes("Value").Value = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Value")))
                        objOutputXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS").AppendChild(objAptNodeClone)
                        objAptNodeClone = objAptNode.CloneNode(True)
                    Loop
                End If
                If blnRecordFound = False Then
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "TRUE"
                    Throw (New bizShared.AAMSException("Invalid Username or Password."))
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
            objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Return objOutputXml
        End Function
        Public Function Search(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function gives search results based on choosen search criterion.
            'Input  :- 
            '<MS_SEARCHEMPLOYEE_INPUT><Employee_Name></Employee_Name><PhoneNo></PhoneNo><Email></Email><SecurityOptionID></SecurityOptionID><SecurityRegionID></SecurityRegionID><Request /><Sec_Group_ID /><PAGE_NO /><PAGE_SIZE /><SORT_BY /><DESC /><INC></INC></MS_SEARCHEMPLOYEE_INPUT>

            'Output :
            '<MS_SEARCHEMPLOYEE_OUTPUT>
            '	<Employee EmployeeID='' Employee_Name='' Login='' IPRestriction='' Cell_Phone='' Email='' />
            '	<PAGE PAGE_COUNT='' TOTAL_ROWS='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_SEARCHEMPLOYEE_OUTPUT>

            ''************************************************************************
            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Dim intSec_Group_ID As Integer
            Dim strINC As String = "F"


            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)

            If SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim() <> "" Then
                intPageNo = SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim()
            End If
            If SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim() <> "" Then
                intPageSize = SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim()
            End If

            If SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim() <> "" Then
                strSortBy = SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim()
            End If

            If SearchDoc.DocumentElement.SelectSingleNode("DESC").InnerText.Trim().ToUpper = "TRUE" Then
                blnDesc = True
            Else
                blnDesc = False
            End If


            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SER_W_EMPLOYEES"
                    .Connection = objSqlConnection

                    .Parameters.Add("@Employee_Name", SqlDbType.VarChar, 50)
                    If (SearchDoc.DocumentElement.SelectSingleNode("Employee_Name").InnerText.Trim = "") Then
                        .Parameters("@Employee_Name").Value = DBNull.Value
                    Else
                        .Parameters("@Employee_Name").Value = SearchDoc.DocumentElement.SelectSingleNode("Employee_Name").InnerText.Trim
                    End If

                    .Parameters.Add("@PhoneNo", SqlDbType.VarChar, 50)

                    If (SearchDoc.DocumentElement.SelectSingleNode("PhoneNo").InnerText.Trim = "") Then
                        .Parameters("@PhoneNo").Value = DBNull.Value
                    Else
                        .Parameters("@PhoneNo").Value = SearchDoc.DocumentElement.SelectSingleNode("PhoneNo").InnerText.Trim
                    End If

                    .Parameters.Add("@Email", SqlDbType.VarChar, 50)
                    If (SearchDoc.DocumentElement.SelectSingleNode("Email").InnerText.Trim = "") Then
                        .Parameters("@Email").Value = DBNull.Value
                    Else
                        .Parameters("@Email").Value = SearchDoc.DocumentElement.SelectSingleNode("Email").InnerText.Trim
                    End If

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

                    .Parameters.Add("@INC", SqlDbType.Char, 1)
                    .Parameters("@INC").Value = strINC

                    .Parameters.Add(New SqlParameter("@TOTALROWS", SqlDbType.BigInt))
                    .Parameters("@TOTALROWS").Direction = ParameterDirection.Output
                    .Parameters("@TOTALROWS").Value = 0

                End With

                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("Employee")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("EmployeeID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmployeeID")))
                    objAptNodeClone.Attributes("Employee_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Employee_Name")) & "")
                    objAptNodeClone.Attributes("Login").InnerText = (objSqlReader.GetValue(objSqlReader.GetOrdinal("Login")) & "")

                    If objSqlReader.GetValue(objSqlReader.GetOrdinal("IPRestriction")) = True Then
                        objAptNodeClone.Attributes("IPRestriction").InnerText = 1
                    Else
                        objAptNodeClone.Attributes("IPRestriction").InnerText = 0
                    End If
                    objAptNodeClone.Attributes("Cell_Phone").InnerText = (objSqlReader.GetValue(objSqlReader.GetOrdinal("CELL_PHONE")) & "")
                    objAptNodeClone.Attributes("Email").InnerText = (objSqlReader.GetValue(objSqlReader.GetOrdinal("Email")) & "")
                    objAptNodeClone.Attributes("ContactPersonName").InnerText = (objSqlReader.GetValue(objSqlReader.GetOrdinal("CONTACT_PERSON_NAME")) & "")
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop
                objSqlReader.Close()
                If blnRecordFound = False Then
                    bizShared.bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").InnerText = Val(objSqlCommand.Parameters("@TOTALROWS").Value)
                    If intPageSize = 0 Then
                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = 0
                    Else
                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = Math.Ceiling(Val(objSqlCommand.Parameters("@TOTALROWS").Value) / intPageSize)
                    End If
                End If
            Catch Exec As bizShared.AAMSException
                'CATCHING AAMS EXCEPTIONS
                bizShared.bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                'msgbox(Exec.ToString)
                bizShared.bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bizShared.bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try

            Return objOutputXml
        End Function
        Public Function Update(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function Inserts/Updates employee.
            'Input  :
            '<MS_UPDATEEMPLOYEE_INPUT>
            '  <Employee Cell_phone='',Login='',Email='',Employee_name='',Password='',Firstform='',Changepassword='',Pwdexpire='',IPrestriction=''/>
            '  <EmailID/>
            '</MS_UPDATEEMPLOYEE_INPUT>

            'Output :
            '<MS_UPDATEEMPLOYEE_OUTPUT>
            '   <Employee Cell_phone='',Login='',Email='',Employee_name='',Password='',Firstform='',Changepassword='',Pwdexpire='',IPrestriction=''/>
            '   <Errors Status=''><Error Code='' Description='' /></Errors>
            '</MS_UPDATEEMPLOYEE_OUTPUT>
            '************************************************************************
            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objSqlCommand1 As New SqlCommand

            Dim objSqlCommandHistory As New SqlCommand
            Dim objTransaction As SqlTransaction

            Dim objUpdateDocOutput As New XmlDocument
            Dim intEmployeeID As Integer
            Dim strCell_Phone As String
            Dim strLogin As String
            Dim strEmail As String
            Dim strEmployee_Name As String
            Dim intIPRestriction As Integer
            Dim strPassword As String
            Dim strFirstForm As String
            Dim intRequestEmpID As Integer
            Dim intChangePassword As Integer
            Dim intPwdExpire As Integer
            Dim intRecordsAffected As Int32
            Const strMETHOD_NAME As String = "Update"
            Dim strFromEmailId As String = ""
            Dim blnMailBlock As Boolean
            Dim strContactPersonName As String = ""

            Try

                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)

                If Not UpdateDoc.DocumentElement.SelectSingleNode("Email") Is Nothing Then
                    strFromEmailId = UpdateDoc.DocumentElement.SelectSingleNode("Email").InnerText
                End If


                With objUpdateDocOutput.DocumentElement.SelectSingleNode("Employee")
                    If UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("EmployeeID").InnerText = "" Then
                        .Attributes("EmployeeID").InnerText = ""
                        intRequestEmpID = 0
                    Else
                        intRequestEmpID = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("EmployeeID").InnerText
                        .Attributes("EmployeeID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("EmployeeID").InnerText
                    End If

                    .Attributes("Cell_Phone").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("Cell_Phone").InnerText
                    .Attributes("Login").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("Login").InnerText
                    .Attributes("Email").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("Email").InnerText
                    .Attributes("Employee_name").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("Employee_name").InnerText
                    .Attributes("Password").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("Password").InnerText
                    .Attributes("Firstform").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("Firstform").InnerText
                    .Attributes("Changepassword").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("Changepassword").InnerText
                    .Attributes("Pwdexpire").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("Pwdexpire").InnerText
                    .Attributes("IPrestriction").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("IPrestriction").InnerText
                    .Attributes("ContactPersonName").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Employee").Attributes("ContactPersonName").InnerText

                End With

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("Employee")
                    If ((.Attributes("EmployeeID").InnerText).Trim).ToString <> "" Then
                        intEmployeeID = ((.Attributes("EmployeeID").InnerText).Trim).ToString
                        strAction = "U"

                    Else
                        intEmployeeID = 0
                        strAction = "I"
                    End If

                    strCell_Phone = (.Attributes("Cell_Phone").InnerText)
                    strLogin = ((.Attributes("Login").InnerText).Trim).ToString
                    strEmail = ((.Attributes("Email").InnerText).Trim).ToString
                    strEmployee_Name = (.Attributes("Employee_name").InnerText)
                    strPassword = ((.Attributes("Password").InnerText).Trim).ToString
                    strFirstForm = (.Attributes("Firstform").InnerText)
                    strContactPersonName = (.Attributes("ContactPersonName").InnerText)

                    If ((.Attributes("Changepassword").InnerText).Trim).ToString <> "" Then
                        intChangePassword = ((.Attributes("Changepassword").InnerText).Trim).ToString
                    End If

                    If ((.Attributes("Pwdexpire").InnerText).Trim).ToString <> "" Then
                        intPwdExpire = ((.Attributes("Pwdexpire").InnerText).Trim).ToString
                    End If

                    If ((.Attributes("IPrestriction").InnerText).Trim).ToString <> "" Then
                        intIPRestriction = ((.Attributes("IPrestriction").InnerText).Trim).ToString
                    End If

                    If strAction = "I" Or strAction = "U" Then
                        If strEmployee_Name = "" Then
                            Throw (New bizShared.AAMSException("Employee Name can't be blank."))
                        End If

                        If strLogin = "" Then
                            Throw (New bizShared.AAMSException("LoginId can't be blank."))
                        End If

                        If strPassword = "" Then
                            Throw (New bizShared.AAMSException("Password can't be blank."))
                        End If

                        If strContactPersonName = "" Then
                            Throw (New bizShared.AAMSException("Contact Person Name can't be blank."))
                        End If

                    Else
                        Throw (New bizShared.AAMSException("Invalid Action Code."))
                    End If
                End With


                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_W_EMPLOYEES"


                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction

                    .Parameters.Add(New SqlParameter("@EMPLOYEEID", SqlDbType.Int))
                    If intEmployeeID = 0 Then
                        .Parameters("@EMPLOYEEID").Value = vbNullString
                    Else
                        .Parameters("@EMPLOYEEID").Value = intEmployeeID
                    End If

                    .Parameters.Add(New SqlParameter("@EMAIL", SqlDbType.VarChar, 30))
                    .Parameters("@EMAIL").Value = strEmail

                    .Parameters.Add(New SqlParameter("@EMPLOYEE_NAME", SqlDbType.Char, 40))
                    .Parameters("@EMPLOYEE_NAME").Value = strEmployee_Name

                    .Parameters.Add(New SqlParameter("@LOGIN", SqlDbType.VarChar, 50))
                    .Parameters("@LOGIN").Value = strLogin

                    .Parameters.Add(New SqlParameter("@CELL_PHONE", SqlDbType.VarChar, 30))
                    .Parameters("@CELL_PHONE").Value = strCell_Phone

                    .Parameters.Add(New SqlParameter("@Password", SqlDbType.VarChar, 50))
                    .Parameters("@Password").Value = strPassword

                    .Parameters.Add(New SqlParameter("@FIRSTFORM", SqlDbType.VarChar, 100))
                    .Parameters("@FIRSTFORM").Value = strFirstForm

                    .Parameters.Add(New SqlParameter("@ChangePassword", SqlDbType.Int))
                    .Parameters("@ChangePassword").Value = intChangePassword

                    .Parameters.Add(New SqlParameter("@PwdExpire", SqlDbType.Int))
                    .Parameters("@PwdExpire").Value = intPwdExpire

                    .Parameters.Add(New SqlParameter("@IPrestriction", SqlDbType.Bit))
                    .Parameters("@IPrestriction").Value = intIPRestriction


                    .Parameters.Add(New SqlParameter("@CONTACT_PERSON_NAME", SqlDbType.VarChar, 40))
                    .Parameters("@CONTACT_PERSON_NAME").Value = strContactPersonName


                    .Parameters.Add(New SqlParameter("@RETUNID", SqlDbType.Int))
                    .Parameters("@RETUNID").Direction = ParameterDirection.Output
                    .Parameters("@RETUNID").Value = ""

                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED
                    .Connection.Open()

                    objTransaction = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)
                    .Transaction = objTransaction

                    intRecordsAffected = .ExecuteNonQuery()

                    If UCase(strAction) = "I" Then
                        intRetId = .Parameters("@RETUNID").Value
                        If intRetId = 0 Then
                            Throw (New bizShared.AAMSException("Unable to insert!"))
                        ElseIf intRetId = -1 Then
                            Throw (New bizShared.AAMSException("Login already exists. Please enter another login"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Employee").Attributes("EmployeeID").InnerText = intRetId
                        End If
                    ElseIf UCase(strAction) = "U" Then
                        intRetId = .Parameters("@RETUNID").Value
                        If intRetId = 0 Then
                            Throw (New bizShared.AAMSException("Unable to update!"))
                        ElseIf intRetId = -1 Then
                            Throw (New bizShared.AAMSException("Login already exists. Please enter another login"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                        End If
                        intRetId = intEmployeeID
                    End If
                End With
                objTransaction.Commit()
                objSqlCommand.Connection.Close()


            Catch Exec As WAY.bizShared.AAMSException
                'CATCHING AAMS EXCEPTIONS
                If Not objTransaction Is Nothing Then
                    objTransaction.Rollback()
                End If
                bizShared.bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                Return objUpdateDocOutput
            Catch Exec As Exception
                If Not objTransaction Is Nothing Then
                    objTransaction.Rollback()
                End If
                If intRetId = 0 Then
                    bizShared.bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                    If blnMailBlock = True Then
                        bizShared.bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                    Else
                        If strAction = "I" Then
                            bizShared.bzShared.FillErrorStatus(objUpdateDocOutput, "101", "Login already exists. Please enter another login")
                        Else
                            bizShared.bzShared.FillErrorStatus(objUpdateDocOutput, "101", "Unable to update!")
                        End If
                    End If
                    Return objUpdateDocOutput
                Else
                    bizShared.bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                    bizShared.bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                    Return objUpdateDocOutput
                End If

            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objUpdateDocOutput
        End Function
        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function gives details of employee.
            'Input  : '<MS_VIEWEMPLOYEE_INPUT>
            '	<EmployeeID></EmployeeID>
            '</MS_VIEWEMPLOYEE_INPUT>


            'Output :
            '<MS_VIEWEMPLOYEE_OUTPUT>
            '	<Employee EmployeeID='' Aoffice='' DepartmentID='' Limited_To_OwnAgency='' Limited_To_Region=''
            '		Limited_To_Aoffice='' Cell_Phone='' Login='' Email='' Employee_Name='' ManagerID='' IPRestriction=''
            '		Password='' FirstForm='' Designation='' ImmediateSupervisorID='' SecurityRegionID='' DateStart=''
            '		DateEnd='' CityId='' ChangePassword ='' PwdExpire =''AgreementSigned='' Request='' ContactPersonName=''/>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_VIEWEMPLOYEE_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strEmployeeID As String
            Const strMETHOD_NAME As String = "View"
            Dim blnRecordFound As Boolean
            Try
                objOutputXml.LoadXml(srtVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strEmployeeID = IndexDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim
                If strEmployeeID = "" Then
                    Throw (New WAY.bizShared.AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_W_EMPLOYEES"
                    .Connection = objSqlConnection
                    .Parameters.Add("@EMPLOYEEID", SqlDbType.Int)
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@EMPLOYEEID").Value = strEmployeeID
                    .Parameters("@ACTION").Value = "S"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("Employee")

                        'EmployeeID='' Cell_phone='' Login='' Password='' Email='' Employee_name='' Firstform=''  Changepassword ='' Pwdexpire =''
                        .Attributes("EmployeeID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmployeeID")))
                        .Attributes("Cell_phone").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Cell_phone")))
                        .Attributes("Login").InnerText = objSqlReader.GetValue(objSqlReader.GetOrdinal("Login")) & ""
                        .Attributes("Password").InnerText = objSqlReader.GetValue(objSqlReader.GetOrdinal("Password")) & ""
                        .Attributes("Email").InnerText = objSqlReader.GetValue(objSqlReader.GetOrdinal("Email")) & ""
                        .Attributes("Employee_name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Employee_name")) & "")
                        .Attributes("Firstform").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Firstform")) & "")
                        .Attributes("Changepassword").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Changepassword")) & "")
                        .Attributes("Password").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PASSWORD")) & "")
                        .Attributes("ContactPersonName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CONTACT_PERSON_NAME")) & "")
                    End With
                    blnRecordFound = True
                Loop
                If blnRecordFound = False Then
                    bizShared.bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                End If
            Catch Exec As WAY.bizShared.AAMSException
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

        Public Function GetReserveUsernamePassword() As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the airport record, based on the given field value
            'Input  : 
            'Output :  
            '<MS_RESERVE_USERNAME_PASSWORD_OUTPUT>
            '   <RESERVE  FIELD_VALUE=''/>
            '  <Errors Status="False">
            '     <Error Code="" Description=""/>
            ' </Errors>
            '</MS_RESERVE_USERNAME_PASSWORD_OUTPUT> 

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "GetReserveUsernamePassword"

            objOutputXml.LoadXml(strReserveUsernamePassword_OUTPUT)

            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_OTH_MS_W_RESERVE_USERNAME_PASSWORD"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("RESERVE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("FIELD_VALUE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FIELD_VALUE")))
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop

                If (blnRecordFound = False) Then
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
            Return objOutputXml
        End Function

        Public Function EmployeePermissionHistory(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To return employee permission history
            'Input  : 
            '<MS_HISTORY_EMPLOYEEPERMISSION_INPUT>
            '  <ACTION/><EMPLOYEEID/><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/>
            ' </MS_HISTORY_EMPLOYEEPERMISSION_INPUT>

            ' OutPut :
            ' <MS_HISTORY_EMPLOYEEPERMISSION_OUTPUT>
            '   <EMPLOYEE USER='' LOGDATE='' Sec_Group='' SEC_GROUP_ID='' SecurityOptionSubName='' CHANGEDDATA=''/>
            '   <PAGE PAGE_COUNT='' TOTAL_ROWS=''/>
            '   <Errors Status=''><Error Code='' Description=''/></Errors>
            '</MS_HISTORY_EMPLOYEEPERMISSION_OUTPUT>

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean

            ''paging
            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean
            ''end


            Dim strMETHOD_NAME As String = "EmployeeHistory"

            objOutputXml.LoadXml(strEMPLOYEEHISTORY_OUTPUT)

            Try

                ''paging
                If SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText <> "" Then
                    intPageNo = SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText <> "" Then
                    intPageSize = SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText <> "" Then
                    strSortBy = SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("DESC").InnerText.ToUpper = "TRUE" Then
                    blnDesc = True
                Else
                    blnDesc = False
                End If
                ''end paging
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_SER_MS_W_EMP_ACCESSLEVEL_LOG]"
                    .Connection = objSqlConnection
                    .Parameters.Add("@ACTION", SqlDbType.Char, 1)
                    .Parameters("@ACTION").Value = "S"

                    .Parameters.Add("@EMPLOYEEID", SqlDbType.Int)
                    If (SearchDoc.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText.Trim = "") Then
                        bizShared.bzShared.FillErrorStatus(objOutputXml, "101", "EmployeeID is mandatory !")
                    Else
                        .Parameters("@EMPLOYEEID").Value = SearchDoc.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText.Trim
                    End If

                    'paging 
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
                    'end paging
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("EMPLOYEE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("USER").InnerText = Convert.ToString(objSqlReader.GetValue(objSqlReader.GetOrdinal("USER"))).Trim
                    objAptNodeClone.Attributes("LOGDATE").InnerText = Convert.ToString(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOGDATE"))).Trim
                    objAptNodeClone.Attributes("Sec_Group").InnerText = Convert.ToString(objSqlReader.GetValue(objSqlReader.GetOrdinal("Sec_Group"))).Trim
                    objAptNodeClone.Attributes("SEC_GROUP_ID").InnerText = Convert.ToString(objSqlReader.GetValue(objSqlReader.GetOrdinal("SEC_GROUP_ID"))).Trim
                    objAptNodeClone.Attributes("SecurityOptionSubName").InnerText = Convert.ToString(objSqlReader.GetValue(objSqlReader.GetOrdinal("SecurityOptionSubName"))).Trim
                    objAptNodeClone.Attributes("CHANGEDDATA").InnerText = Convert.ToString(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHANGEDDATA"))).Trim
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop
                objSqlReader.Close()

                If (blnRecordFound = False) Then
                    bizShared.bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").InnerText = objSqlCommand.Parameters("@TOTALROWS").Value
                    If intPageSize = 0 Then
                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = 0
                    Else
                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = Ceiling(objSqlCommand.Parameters("@TOTALROWS").Value / intPageSize)
                    End If
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
            Return objOutputXml
        End Function
        Public Function ChangePassword(ByVal IndexDoc As XmlDocument) As XmlDocument
            '***********************************************************************
            'Purpose: This function will change the Old Password of Employee.
            'Input: Xmldocument
            '<MS_CHANGEPASSWORD_INPUT>
            '	<EmployeeID></EmployeeID>
            '	<OldPassword></OldPassword>
            '	<NewPassword></NewPassword>
            '</MS_CHANGEPASSWORD_INPUT>

            'Output: Xmldocument
            '<MS_CHANGEPASSWORD_OUTPUT>
            '	<Errors Status="FALSE">
            '		<Error Code="" Description="" />
            '	</Errors>
            '</MS_CHANGEPASSWORD_OUTPUT>
            '***********************************************************************
            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString())
            'Dim onjCrypto As New AAMS.bizCryptography.bzCryptography
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument

            Dim intEmployeeID As Integer
            Dim strOldPassword As String
            Dim strNewPassword As String
            Dim intRetId As Integer

            Const strMETHOD_NAME As String = "ChangePassword"
            Try
                objOutputXml.LoadXml(strCHANGEPASSWORD_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                intEmployeeID = IndexDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim
                strOldPassword = IndexDoc.DocumentElement.SelectSingleNode("OldPassword").InnerText.Trim
                strNewPassword = IndexDoc.DocumentElement.SelectSingleNode("NewPassword").InnerText.Trim

                If intEmployeeID = 0 Or strOldPassword = "" Or strNewPassword = "" Then
                    Throw (New bizShared.AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_OTH_MS_W_USER_CHANGE_PASSWORD"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@EMPLOYEEID", SqlDbType.Int))
                    .Parameters("@EMPLOYEEID").Value = intEmployeeID

                    .Parameters.Add(New SqlParameter("@USER_OLD_PASSWORD", SqlDbType.Char, 100))
                    .Parameters("@USER_OLD_PASSWORD").Value = strOldPassword 'onjCrypto.Encrypt(strOldPassword)

                    .Parameters.Add(New SqlParameter("@USER_NEW_PASSWORD", SqlDbType.Char, 100))
                    .Parameters("@USER_NEW_PASSWORD").Value = strNewPassword 'onjCrypto.Encrypt(strNewPassword)

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0
                    .Connection.Open()
                    .ExecuteNonQuery()
                    intRetId = .Parameters("@RETURNID").Value
                End With

                If intRetId = -1 Then
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "TRUE"
                    Throw (New bizShared.AAMSException("Invalid Username/Inactive user."))
                End If
                If intRetId = -100 Then
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "TRUE"
                    Throw (New bizShared.AAMSException("New password should not be old password."))
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
            objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Return objOutputXml
        End Function

        Public Function List() As System.Xml.XmlDocument
            '---------INPUT XML-----------------------------------------------------------------
            '<MS_LISTMPLOYEE_OUTPUT>
            '	<EMPLOYEE EMPLOYEEID='' EMPLOYEENAME='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_LISTMPLOYEE_OUTPUT>
            '------------------------------------------------------------------------------------

            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "List"
            Dim strType As String = String.Empty
            objOutputXml.LoadXml(srtLIST_OUTPUT)

            Try

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LST_W_EMPLOYEE_LIST"
                    .Connection = objSqlConnection
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("EMPLOYEE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("EMPLOYEEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMPLOYEEID")) & "")
                    objAptNodeClone.Attributes("EMPLOYEENAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMPLOYEENAME")) & "")
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop

                If blnRecordFound = False Then
                    bizShared.bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found")
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

            Return objOutputXml
        End Function

    End Class
End Namespace