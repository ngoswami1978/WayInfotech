
Public Class frmTapan
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'List
        Dim obj As New bizMaster.bzCity
        Dim objXml As New Xml.XmlDocument
        objXml = obj.List()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'Add
        Dim obj As New bizMaster.bzCity
        Dim objXml As New Xml.XmlDocument
        objXml = obj.Add()
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        'Update/Insert
        Dim obj As New bizMaster.bzEmployee
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_SEARCHDEPARTMENTEMPLOYEE_INPUT><Employee_Type>Productivity</Employee_Type></MS_SEARCHDEPARTMENTEMPLOYEE_INPUT>")
        objXml = obj.ListDepartmentEmployees(objXml)
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'Search
        Dim obj As New bizMaster.bzCity
        Dim objXml As New Xml.XmlDocument
        Dim strSearchCityXML As String = "<MS_SEARCHCITY_INPUT><City_Name></City_Name><PAGE_NO>1</PAGE_NO><PAGE_SIZE>100</PAGE_SIZE><SORT_BY></SORT_BY><DESC></DESC></MS_SEARCHCITY_INPUT>"
        objXml.LoadXml(strSearchCityXML)
        objXml = obj.Search(objXml)
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim obj As New bizMaster.bzCountryState
        Dim objXml As New Xml.XmlDocument
        objXml = obj.List()
    End Sub
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim obj As New bizMaster.bzAgencyGroup
        Dim objXml As New Xml.XmlDocument
        objXml = obj.ListGroupClassificationType()
        objXml = obj.ListGroupType()
        objXml = obj.ListPriority()
    End Sub
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        'Add Agency
        Dim obj As New bizMaster.bzAgencyGroup
        Dim objXml As New Xml.XmlDocument
        objXml = obj.Add()
    End Sub
    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        'Search Agency
        Dim obj As New bizMaster.bzAgencyGroup
        Dim objXml As New Xml.XmlDocument
        Dim strSEARCH_INPUT As String = "<MS_SEARCHGROUP_INPUT><EmployeeID></EmployeeID><Chain_Code>37</Chain_Code><Chain_Name></Chain_Name><City_Name></City_Name><GroupTypeID></GroupTypeID><Aoffice></Aoffice><MainGroup>False</MainGroup><PAGE_NO>1</PAGE_NO><PAGE_SIZE>100</PAGE_SIZE><SORT_BY></SORT_BY><DESC></DESC></MS_SEARCHGROUP_INPUT>"
        objXml.LoadXml(strSEARCH_INPUT)
        objXml = obj.Search(objXml)
    End Sub
    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        'Update Agency GROUP
        Dim obj As New bizMaster.bzAgencyGroup
        'Dim strGROUP_INPUT As String = "<MS_UPDATEGROUP_INPUT><GROUP ACTION='U' Chain_Code='55' Chain_Name='SMITA WORLD TRAVELS' ContactPerson='' Affiliation='' Priority_Auto_Update='1' Grouped='0' PK_Bkngs='' PriorityID='2' GroupTypeID='2' Aoffice='HYD' CityID='153' Responsible_Location='1' CQualifier='A' CCode='C' MainGroup='True' Address='test address' GroupClassificationID='1' AccountManager='202985' PANNO='ABCDE' EmployeeId='24'/> </MS_UPDATEGROUP_INPUT>"
        Dim strGROUP_INPUT As String = "<MS_UPDATEGROUP_INPUT><GROUP ACTION='U' Chain_Code='25243' Chain_Name='FLY WORLD' ContactPerson='' Affiliation='' Priority_Auto_Update='1' Grouped='0' PK_Bkngs='' PriorityID='2' GroupTypeID='2' Aoffice='MAA' CityID='133' Responsible_Location='1' CQualifier='' CCode='' MainGroup='False' Address='' EmployeeId='24' GroupClassificationID='' Group_Classification_Name='' AccountManager='' PANNO='' GROUP_OFFICEID='' COMP_VERTICAL='' /></MS_UPDATEGROUP_INPUT>"

        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(strGROUP_INPUT)
        objXml = obj.Update(objXml)
    End Sub
    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim obj As New bizMaster.bzAgencyGroup
        Dim strGROUP_INPUT As String = "<MS_DELETEGROUP_INPUT><Chain_Code>55</Chain_Code></MS_DELETEGROUP_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(strGROUP_INPUT)
        objXml = obj.Delete(objXml)
    End Sub
    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        'Agency view
        Dim obj As New bizMaster.bzAgencyGroup
        Dim strGROUP_INPUT As String = "<MS_VIEWGROUP_INPUT><Chain_Code>55</Chain_Code></MS_VIEWGROUP_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(strGROUP_INPUT)
        objXml = obj.View(objXml)
    End Sub
    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        'Add IPPool
    End Sub
    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Search IPPool
    End Sub
    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        ''Insert/Update IP Pool
        Dim obj As New bizMaster.bzIPPool
        'Dim strInputXML As String = "<MS_UPDATEIPPOOL_INPUT><IPPOOL Action='I' PoolID='' PoolName='ISP' Aoffice='Del' DepartmentID='18' IPAddress='192.168.12.182' /></MS_UPDATEIPPOOL_INPUT>"
        'Dim strInputXML As String = "<MS_UPDATEIPPOOL_INPUT><IPPOOL Action='U' PoolID='49' PoolName='Technical' Aoffice='DEL' DepartmentID='18' IPAddress='' /></MS_UPDATEIPPOOL_INPUT>"
        Dim strInputXML As String = "<MS_UPDATEIPPOOL_INPUT>"
        strInputXML = strInputXML & "<IPPOOL PoolID='' PoolName='MARKETING_TEST121221' Aoffice='DEL' DepartmentID='18'>"
        strInputXML = strInputXML & "<IPAddress>192.168.12.149</IPAddress>"
        strInputXML = strInputXML & "<IPAddress>192.168.12.150</IPAddress>"
        strInputXML = strInputXML & "<IPAddress>192.168.12.151</IPAddress>"
        strInputXML = strInputXML & "<IPAddress>192.168.12.152</IPAddress>"
        strInputXML = strInputXML & "<IPAddress>192.168.12.153</IPAddress>"
        strInputXML = strInputXML & "<IPAddress>192.168.12.154</IPAddress>"
        strInputXML = strInputXML & "<IPAddress>192.168.12.155</IPAddress>"
        strInputXML = strInputXML & "<IPAddress>192.168.12.156</IPAddress>"
        strInputXML = strInputXML & "<IPAddress>192.168.12.157</IPAddress>"
        strInputXML = strInputXML & "<IPAddress>192.168.12.158</IPAddress>"
        strInputXML = strInputXML & "<IPAddress>192.168.12.159</IPAddress>"
        strInputXML = strInputXML & "<IPAddress>192.168.12.160</IPAddress>"
        strInputXML = strInputXML & "</IPPOOL>"
        strInputXML = strInputXML & "</MS_UPDATEIPPOOL_INPUT>"

        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(strInputXML)
        objXml = obj.Update(objXml)
    End Sub
    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        'Delete IPPool
        Dim obj As New bizMaster.bzIPPool
        Dim strInputXML As String = "<MS_DELETEIPPOOL_INPUT><PoolID>76</PoolID></MS_DELETEIPPOOL_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(strInputXML)
        objXml = obj.Delete(objXml)
    End Sub
    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        'View IP Pool
        Dim obj As New bizMaster.bzIPPool
        Dim strInputXML As String = "<MS_VIEWIPPOOL_INPUT><PoolID>85</PoolID></MS_VIEWIPPOOL_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(strInputXML)
        objXml = obj.View(objXml)
    End Sub
    Private Sub cmdIPPoolSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdIPPoolSearch.Click
        Dim obj As New bizMaster.bzIPPool
        Dim strInputXML As String = "<MS_SEARCHIPPOOL_INPUT><PoolName></PoolName><Aoffice></Aoffice><Department_Name></Department_Name><PAGE_NO>1</PAGE_NO><PAGE_SIZE>100</PAGE_SIZE><SORT_BY></SORT_BY><DESC></DESC></MS_SEARCHIPPOOL_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(strInputXML)
        objXml = obj.Search(objXml)
    End Sub

    Private Sub cmdSecAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSecAdd.Click
        Dim objXml As New Xml.XmlDocument
        Dim obj As New bizMaster.bzSecurityRegion
        Dim strUpdateInputXml As String = ""
        objXml.LoadXml(strUpdateInputXml)
        objXml = obj.Add()
    End Sub
    Private Sub cmdSecUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSecUpdate.Click
        Dim objXml As New Xml.XmlDocument
        Dim obj As New bizMaster.bzSecurityRegion
        Dim strInputXML As String = "<MS_UPDATESECURITYREGION_INPUT>"
        strInputXML = strInputXML & "<SECURITYREGION RegionID='' Name='ABCDE'>"
        ' strInputXML = strInputXML & "<Aoffice>DEL</Aoffice>"
        strInputXML = strInputXML & "<Aoffice>BAN</Aoffice>"
        strInputXML = strInputXML & "<Aoffice>HYD</Aoffice>"
        strInputXML = strInputXML & "</SECURITYREGION>"
        strInputXML = strInputXML & "</MS_UPDATESECURITYREGION_INPUT>"
        objXml.LoadXml(strInputXML)
        objXml = obj.Update(objXml)
    End Sub
    Private Sub cmdSecSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSecSearch.Click
        Dim obj As New bizMaster.bzSecurityRegion
        Dim objXml As New Xml.XmlDocument
        Dim strSearchInputXml As String = "<MS_SEARCHSECURITYREGION_INPUT><Name></Name><PAGE_NO>1</PAGE_NO><PAGE_SIZE>10</PAGE_SIZE><SORT_BY></SORT_BY><DESC></DESC></MS_SEARCHSECURITYREGION_INPUT>"
        objXml.LoadXml(strSearchInputXml)
        objXml = obj.Search(objXml)
    End Sub
    Private Sub cmdSecDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSecDelete.Click
        Dim objXml As New Xml.XmlDocument
        Dim obj As New bizMaster.bzSecurityRegion
        Dim strDeleteInputXml As String = "<MS_DELETESECURITYREGION_INPUT><RegionID>37</RegionID></MS_DELETESECURITYREGION_INPUT>"
        objXml.LoadXml(strDeleteInputXml)
        objXml = obj.Delete(objXml)
    End Sub
    Private Sub cmdSecView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSecView.Click
        Dim objXml As New Xml.XmlDocument
        Dim obj As New bizMaster.bzSecurityRegion
        Dim strViewInputXml As String = "<MS_VIEWSECURITYREGION_INPUT><RegionID>11</RegionID></MS_VIEWSECURITYREGION_INPUT>"
        objXml.LoadXml(strViewInputXml)
        objXml = obj.View(objXml)
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAofficecity.Click

        Dim objXml As New Xml.XmlDocument
        Dim obj As New bizMaster.bzAOffice
        Dim strViewInputXml As String = "<MS_SEARCHAOFFICECITY_INPUT><Aoffice>DEL</Aoffice></MS_SEARCHAOFFICECITY_INPUT>"
        objXml.LoadXml(strViewInputXml)
        objXml = obj.SearchAofficeCity(objXml)

    End Sub

    Private Sub cmdLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLogin.Click
        Dim objXml As New Xml.XmlDocument
        Dim obj As New bizMaster.bzEmployee
        Dim strViewInputXml As String = "<MS_LOGIN_INPUT><Login>admin</Login><Password>aams</Password><IPAddress></IPAddress></MS_LOGIN_INPUT>"
        'Dim strViewInputXml As String = "<MS_LOGIN_INPUT><Login>saurav</Login><Password>aams</Password><IPAddress>192.168.12.149</IPAddress></MS_LOGIN_INPUT>"
        objXml.LoadXml(strViewInputXml)
        objXml = obj.Login(objXml)
    End Sub
    Private Sub cmdChangePassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChangePassword.Click
        Dim objXml As New Xml.XmlDocument
        Dim obj As New bizMaster.bzEmployee
        Dim strViewInputXml As String = "<MS_CHANGEPASSWORD_INPUT><EmployeeID>1212</EmployeeID><OldPassword>12345</OldPassword><NewPassword>12345</NewPassword></MS_CHANGEPASSWORD_INPUT>"
        objXml.LoadXml(strViewInputXml)
        objXml = obj.ChangePassword(objXml)
    End Sub

    Private Sub cmdCityDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCityDelete.Click
        Dim obj As New bizMaster.bzCity
        Dim objXml As New Xml.XmlDocument
        Dim strXML As String = "<MS_DELETECITY_INPUT><CityID>34</CityID></MS_DELETECITY_INPUT>"
        objXml.LoadXml(strXML)
        objXml = obj.Delete(objXml)
    End Sub
    Private Sub cmdList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdList.Click
        Dim objXml As New Xml.XmlDocument
        Dim obj As New bizMaster.bzSecurityRegion
        objXml = obj.List()
    End Sub
    Private Sub cmdCityDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCityDetails.Click
        Dim obj As New bizMaster.bzCity
        Dim strGROUP_INPUT As String = "<MS_GETCITYCODE_INPUT><City_Name>Agra</City_Name></MS_GETCITYCODE_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(strGROUP_INPUT)
        objXml = obj.GetCityDetails(objXml)
    End Sub
    Private Sub Button12_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        'Employee Search
        Dim obj As New bizMaster.bzEmployee
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        Dim strInput As String
        strInput = "<MS_SEARCHEMPLOYEE_INPUT>  <Employee_Name></Employee_Name>   <DepartmentID />   <Aoffice />   <Designation />   <SecurityOptionID />   <AgreementSigned>3</AgreementSigned>   <Limited_To_Aoffice />   <Limited_To_Region />   <SecurityRegionID />   <Request>1</Request>   <PAGE_NO>1</PAGE_NO>   <PAGE_SIZE>5</PAGE_SIZE>   <SORT_BY>Employee_Name</SORT_BY>   <DESC>FALSE</DESC>   </MS_SEARCHEMPLOYEE_INPUT>"

        objXmlInput.LoadXml(strInput)
        objXmlOutput = obj.Search(objXmlInput)
    End Sub

    Private Sub Button14_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Try
            Dim obj As New bizMaster.bzEmployee
            Dim objXmlInput As New Xml.XmlDocument
            Dim objXmlOutput As New Xml.XmlDocument
            Dim strInput As String
            strInput = "<MS_UPDATEEMPLOYEEGROUP_INPUT>  <EmployeeID>59</EmployeeID>   <Chain_Code>16839</Chain_Code>   <Chain_Code>14799</Chain_Code>   <Chain_Code>9746</Chain_Code>   <Chain_Code>14021</Chain_Code>   <Chain_Code>13622</Chain_Code></MS_UPDATEEMPLOYEEGROUP_INPUT>"
            objXmlInput.LoadXml(strInput)
            objXmlOutput = obj.SaveGroupAssigned(objXmlInput)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        Try
            Dim obj As New bizMaster.bzEmailGroup
            Dim objXmlInput As New Xml.XmlDocument
            Dim objXmlOutput As New Xml.XmlDocument
            Dim strInput As String
            strInput = "<MS_SEARCHEMAILGROUP_INPUT><GROUPDETAIL GroupName='' EmployeeID='' GroupMNC='' GroupISP='' GroupTraining='' GroupAoffice='' DepartmentID='' GroupId='7' PAGE_NO='' PAGE_SIZE='' SORT_BY='' DESC='' /></MS_SEARCHEMAILGROUP_INPUT>"
            objXmlInput.LoadXml(strInput)
            objXmlOutput = obj.Search(objXmlInput)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        Try
            Dim obj As New bizMaster.bzConfiguration
            Dim strInputXML As String = "<MS_SEARCHCONFIGRURABLE_INPUT><CCA_ID></CCA_ID><PAGE_NO></PAGE_NO><PAGE_SIZE></PAGE_SIZE><SORT_BY></SORT_BY><DESC></DESC></MS_SEARCHCONFIGRURABLE_INPUT>"
            Dim objXml As New Xml.XmlDocument
            objXml.LoadXml(strInputXML)
            objXml = obj.Search(objXml)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Try
            Dim obj As New bizMaster.bzConfiguration
            Dim objXml As New Xml.XmlDocument
            objXml = obj.List()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        Try
            Dim obj As New bizMaster.bzConfiguration
            Dim objXmlInput As New Xml.XmlDocument
            Dim objXmlOutput As New Xml.XmlDocument
            Dim strInput As String
            'strInput = "<MS_UPDATECONFIGRURABLE_INPUT><CONFIGRURABLE CCA_ID='6' CCA_NAME='' FIELD_NAME='DEFAULT_HD_FEEDBACK_STATUS' FIELD_VALUE='190' Remarks='Default HelpDesk Feedback Status' Active='TRUE' /><CONFIGRURABLE CCA_ID='6' CCA_NAME='' FIELD_NAME='DEFAULT_HD_PTR_FOLLOWUP' FIELD_VALUE='57' Remarks='Default HelpDesk PTR Follow up' Active='TRUE' /> <CONFIGRURABLE CCA_ID='6' CCA_NAME='' FIELD_NAME='DEFAULT_HD_PTR_SEVERITY' FIELD_VALUE='71' Remarks='Default HelpDesk PTR Severity' Active='TRUE' /> <CONFIGRURABLE CCA_ID='6' CCA_NAME='' FIELD_NAME='DEFAULT_HD_PTR_STATUS' FIELD_VALUE='49' Remarks='Default HelpDesk PTR Status' Active='TRUE' /><CONFIGRURABLE CCA_ID='6' CCA_NAME='' FIELD_NAME='DEFAULT_HD_PTR_TYPE' FIELD_VALUE='75' Remarks='Default HelpDesk PTR Type' Active='TRUE' /> </MS_UPDATECONFIGRURABLE_INPUT>"
            strInput = "<MS_UPDATECONFIGRURABLE_INPUT><CONFIGRURABLE CCA_ID='5' CCA_NAME='DEFAULT_PARTICIPANT_STATUS' FIELD_NAME='DEFAULT_PARTICIPANT_STATUS' FIELD_VALUE='10003,10005,10006' Remarks='To Send Auto Mails To changed Status in Traning' Active='TRUE' /></MS_UPDATECONFIGRURABLE_INPUT>"
            objXmlInput.LoadXml(strInput)
            objXmlOutput = obj.Update(objXmlInput)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        'ListFirstForm
        Dim obj As New bizMaster.bzEmployee
        Dim objXml As New Xml.XmlDocument
        objXml = obj.ListFirstForm()
    End Sub

    Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click
        Try
            Dim obj As New bizMaster.bzPermission
            Dim objXml As New Xml.XmlDocument
            objXml = obj.ListSecurityGroup()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button24.Click
        Try
            Dim obj As New bizMaster.bzEmployee
            Dim objXml As New Xml.XmlDocument
            ' Dim strInput As String = "<MS_UPDATEEMPLOYEE_HDDEFAULT_INPUT><EMPLOYEE_HDDEFAULT ACTION='I' EMPLOYEEID='138' FUN_ASSIGNEDTO='2' TEC_ASSIGNEDTO='8134' CONTACT_TYPE_ID='1' /></MS_UPDATEEMPLOYEE_HDDEFAULT_INPUT>"
            Dim strInput As String = "<MS_UPDATEEMPLOYEE_HDDEFAULT_INPUT><EMPLOYEE_HDDEFAULT ACTION='U' EMPLOYEEID='24' FUN_ASSIGNEDTO='2' TEC_ASSIGNEDTO='' CONTACT_TYPE_ID='4' TEC_CONTACT_TYPE_ID='' ChangedBy='24' /></MS_UPDATEEMPLOYEE_HDDEFAULT_INPUT>"
            objXml.LoadXml(strInput)
            obj.UpdateHDDefault(objXml)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button25.Click
        Try
            Dim obj As New bizMaster.bzEmployee
            Dim objXml As New Xml.XmlDocument
            objXml.LoadXml("<MS_VIEWEMPLOYEE_HDDEFAULT_INPUT><EmployeeID>138</EmployeeID></MS_VIEWEMPLOYEE_HDDEFAULT_INPUT>")
            obj.ViewHDDefault(objXml)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button26.Click
        Dim objXml As New Xml.XmlDocument
        Dim obj As New bizMaster.bzSecurityRegion
        objXml = obj.List1()
    End Sub

    Private Sub Button27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button27.Click
        Try
            Dim obj As New bizMaster.bzEmployee
            Dim objXml As New Xml.XmlDocument
            Dim strInput As String = "<MS_UPDATEEMPLOYEEIP_INPUT><EmployeeID>202778</EmployeeID><IPRESTRICTION>1</IPRESTRICTION><ChangedBy>24</ChangedBy><IPAddress IP='192.168.94.115'></IPAddress><IPAddress IP='192.168.94.116'></IPAddress><IPAddress IP='192.168.94.117'></IPAddress><IPAddress IP='192.168.94.114'></IPAddress></MS_UPDATEEMPLOYEEIP_INPUT>"
            objXml.LoadXml(strInput)
            obj.AssignIP(objXml)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button28.Click
        Try
            Dim obj As New bizMaster.bzEmployee
            Dim objXml As New Xml.XmlDocument
            Dim strInput As String = " <MS_UPDATEEMPLOYEE_HDDEFAULT_INPUT> <EMPLOYEE_HDDEFAULT ACTION='U' EMPLOYEEID='202778' FUN_ASSIGNEDTO='3' TEC_ASSIGNEDTO='' CONTACT_TYPE_ID='' TEC_CONTACT_TYPE_ID='3' ChangedBy='24' />   </MS_UPDATEEMPLOYEE_HDDEFAULT_INPUT>"
            objXml.LoadXml(strInput)
            obj.UpdateHDDefault(objXml)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button29.Click
        Try
            Dim obj As New bizMaster.bzEmployee
            Dim objXml As New Xml.XmlDocument
            Dim strInput As String = "<MS_EMPLOYEE_HISTORY_INPUT><EMPLOYEEID>8321</EMPLOYEEID></MS_EMPLOYEE_HISTORY_INPUT>"
            objXml.LoadXml(strInput)
            obj.GetEmployeeHistory(objXml)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button30.Click
        Try
            Dim obj As New bizMaster.bzEmployee
            Dim objXml As New Xml.XmlDocument
            Dim strInput As String = "<MS_UPDATEEMPLOYEE_INPUT><Employee EmployeeID='8321' Aoffice='DEL' DepartmentID='5' Limited_To_OwnAgency='0' Limited_To_Region='1' Limited_To_Aoffice='0' Cell_Phone='9871370968' Email='ngoswami@amadeus.co.in' Employee_Name='ANKUSH KUMAR' ManagerID='31' LoginRequired='True' Login='ankushk' Password='amadeus' FirstForm='Agency' Designation='Team Leader' ImmediateSupervisorID='31' SecurityRegionID='33' DateStart='20050614' DateEnd='' CityId='187' ChangePassword='1' PwdExpire='0' AgreementSigned='1' Request='1' GroupTypeID='' Show_Prod_ISUPERVISOR='False' IPRestriction='True' IPAddress='127.0.0.0' ChangedBy='24' /><EmailID>ngoswami@amadeus.co.in</EmailID></MS_UPDATEEMPLOYEE_INPUT>"
            objXml.LoadXml(strInput)
            obj.Update(objXml)
        Catch ex As Exception

        End Try
    End Sub
End Class