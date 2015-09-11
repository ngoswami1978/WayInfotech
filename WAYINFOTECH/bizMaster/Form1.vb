Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim obj As New bizMaster.bzAOffice
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<MS_UPDATEAOFFICE_INPUT><AOFFICE Action='I' Aoffice='BOM' Region='North' Address='delhi' RegionalHQ='india' BRLimit='10' BR_ID='' Fax='3241445' Pincode='453255' CityID='1' Phone='3214324' /></MS_UPDATEAOFFICE_INPUT>")
        objXmlOutput = obj.Update(objXmlInput)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim obj As New bizMaster.bzAOffice
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        '        objXmlInput.LoadXml("<MS_UPDATEAOFFICE_INPUT><AOFFICE Action='U' Aoffice='del' Region='North' Address='delhi' RegionalHQ='india' BRLimit='10' BR_ID='1' Fax='3241445' Pincode='453255' CityID='1' Phone='3214324' /></MS_UPDATEAOFFICE_INPUT>")
        objXmlInput.LoadXml("<MS_UPDATEAOFFICE_INPUT><AOFFICE Action='U' Aoffice='cal' Region='EAST' Address='sdf' RegionalHQ='0' BRLimit='10' BR_ID='0' Fax='232' Pincode='2323' CityID='2' Phone='2323' /></MS_UPDATEAOFFICE_INPUT>")


        objXmlOutput = obj.Update(objXmlInput)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim obj As New bizMaster.bzAOffice
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<MS_DELETEAOFFICE_INPUT><Aoffice>DEL</Aoffice></MS_DELETEAOFFICE_INPUT>")
        objXmlOutput = obj.Delete(objXmlInput)
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim obj As New bizMaster.bzAOffice
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<MS_SEARCHAOFFICE_INPUT><Aoffice></Aoffice><PAGE_NO>1</PAGE_NO><PAGE_SIZE>100</PAGE_SIZE><SORT_BY></SORT_BY><DESC></DESC></MS_SEARCHAOFFICE_INPUT>")
        objXmlOutput = obj.Search(objXmlInput)
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim obj As New bizMaster.bzAOffice
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<MS_VIEWAOFFICE_INPUT><Aoffice>BHP</Aoffice></MS_VIEWAOFFICE_INPUT>")
        objXmlOutput = obj.View(objXmlInput)
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim obj As New bizMaster.bzAOffice
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument

        objXmlOutput = obj.ListHQ()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim obj As New bizMaster.bzCountryState
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<MS_SEARCHCOUNTRYSTATE_INPUT><State_Name></State_Name><CountryID>1</CountryID></MS_SEARCHCOUNTRYSTATE_INPUT>")
        objXmlOutput = obj.Search(objXmlInput)
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim obj As New bizMaster.bzEquipment
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlOutput = obj.List()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim obj As New bizMaster.bzCountryState
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<MS_VIEWCOUNTRYSTATE_INPUT><StateID>1</StateID></MS_VIEWCOUNTRYSTATE_INPUT>")
        objXmlOutput = obj.View(objXmlInput)
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim obj As New bizMaster.bzCountryState
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        '        objXmlInput.LoadXml("<MS_UPDATEAOFFICE_INPUT><AOFFICE Action='U' Aoffice='del' Region='North' Address='delhi' RegionalHQ='india' BRLimit='10' BR_ID='1' Fax='3241445' Pincode='453255' CityID='1' Phone='3214324' /></MS_UPDATEAOFFICE_INPUT>")
        objXmlInput.LoadXml("<MS_UPDATECOUNTRYSTATE_INPUT>><STATE Action='I' StateID='' State_Name='Bombay' CountryID='1' />	</MS_UPDATECOUNTRYSTATE_INPUT>")


        objXmlOutput = obj.Update(objXmlInput)
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Dim obj As New bizMaster.bzEmailGroup
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<MS_DELETEEMAILGROUP_INPUT><GroupID>38</GroupID></MS_DELETEEMAILGROUP_INPUT>")
        objXmlOutput = obj.Delete(objXmlInput)
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Dim obj As New bizMaster.bzEmployee
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        'Dim strinput = "<MS_UPDATEEMPLOYEE_INPUT><Employee EmployeeID='202778' Aoffice='CMB' DepartmentID='7' Limited_To_OwnAgency='0' Limited_To_Region='1' Limited_To_Aoffice='0' Cell_Phone='222222222' Email='ngoswami@amadeus.co.in' Employee_Name='Tapan Kumar Nath' ManagerID='8254' LoginRequired='True' Login='tapannath' Password='tapannath' FirstForm='Agency' Designation='Software Engineer' ImmediateSupervisorID='8133' SecurityRegionID='6' DateStart='20090120' DateEnd='' CityId='187' ChangePassword='1' PwdExpire='0' AgreementSigned='1' Request='1' GroupTypeID='' Show_Prod_ISUPERVISOR='True' IPRestriction='True' IPAddress='192.168.94.117' ChangedBy='24' /> <EmailID>ngoswami@amadeus.co.in</EmailID> </MS_UPDATEEMPLOYEE_INPUT>"
        'objXmlInput.LoadXml("<MS_UPDATEEMPLOYEE_INPUT><Employee EmployeeID='' Aoffice='AGR' DepartmentID='3' Limited_To_OwnAgency='1' Limited_To_Region='0' Limited_To_Aoffice='0' Cell_Phone='9898999' Email='testtest@testtest.com' Employee_Name='testtest' ManagerID='8368' LoginRequired='True' Login='testtest' Password='testtest' FirstForm='Employee' Designation='Accounts Officer Western Region' ImmediateSupervisorID='8368' SecurityRegionID='' DateStart='' DateEnd='' CityId='295' ChangePassword='1' PwdExpire='0' AgreementSigned='0' Request='0' GroupTypeID='' Show_Prod_ISUPERVISOR='False' IPRestriction='True' IPAddress='192.168.94.117' /><EmailID>ngoswami@amadeus.co.in</EmailID></MS_UPDATEEMPLOYEE_INPUT>")
        Dim strinput As String = "<MS_UPDATEEMPLOYEE_INPUT><Employee EmployeeID='203295' Aoffice='DEL' DepartmentID='37' Limited_To_OwnAgency='0' Limited_To_Region='1' Limited_To_Aoffice='0' Cell_Phone='9999049998' Email='nilabh@amadeus.co.in' Employee_Name='Nilabh Shrivastava' ManagerID='118' LoginRequired='True' Login='nilabh' Password='nilabh' FirstForm='' Designation='Manager E Commerce' ImmediateSupervisorID='203258' SecurityRegionID='11' DateStart='19000101' DateEnd='' CityId='187' ChangePassword='1' PwdExpire='0' AgreementSigned='1' Request='0' GroupTypeID='' Show_Prod_ISUPERVISOR='False' IPRestriction='True' IPAddress='192.168.9.185' ChangedBy='24' /><EmailID>vsabharwal@amadeus.co.in</EmailID></MS_UPDATEEMPLOYEE_INPUT>"
        objXmlInput.LoadXml(strinput)
        objXmlOutput = obj.Update(objXmlInput)

    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Dim obj As New bizMaster.bzEmployee
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<MS_VIEWEMPLOYEE_INPUT><EmployeeID>8011</EmployeeID></MS_VIEWEMPLOYEE_INPUT>")
        objXmlOutput = obj.View(objXmlInput)
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Dim obj As New bizMaster.bzEmployee
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<MS_DELETEEMPLOYEE_INPUT><EmployeeID>27</EmployeeID></MS_DELETEEMPLOYEE_INPUT>")
        objXmlOutput = obj.Delete(objXmlInput)
    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        Dim obj As New bizMaster.bzEmployee
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<MS_SEARCHEMPLOYEE_INPUT><Employee_Name /><DepartmentID /><Aoffice /><Designation /><SecurityOptionID /><AgreementSigned>False</AgreementSigned><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><SecurityRegionID></SecurityRegionID></MS_SEARCHEMPLOYEE_INPUT>")
        objXmlOutput = obj.Search(objXmlInput)
    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Dim obj As New bizMaster.bzEmployee
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        '        objXmlInput.LoadXml("<MS_UPDATEAOFFICE_INPUT><AOFFICE Action='U' Aoffice='del' Region='North' Address='delhi' RegionalHQ='india' BRLimit='10' BR_ID='1' Fax='3241445' Pincode='453255' CityID='1' Phone='3214324' /></MS_UPDATEAOFFICE_INPUT>")
        'objXmlInput.LoadXml("<MS_UPDATEEMPLOYEEPERMISSION_INPUT><EmployeeID>21</EmployeeID><SECURITY_OPTIONS><SECURITY_OPTION SecurityOptionID='2' Value='1' /><SECURITY_OPTION SecurityOptionID='3' Value='1' /></SECURITY_OPTIONS></MS_UPDATEEMPLOYEEPERMISSION_INPUT>")
        'objXmlOutput = obj.SavePermissions(objXmlInput)
        objXmlInput.LoadXml("<MS_UPDATEEMPLOYEEIP_INPUT><EmployeeID>40</EmployeeID><IPRESTRICTION></IPRESTRICTION><IPAddress IP='1.1.1.2' /><IPAddress IP='1.1.1.3' /><IPAddress IP='1.1.1.4' /></MS_UPDATEEMPLOYEEIP_INPUT>")
        objXmlOutput = obj.AssignIP(objXmlInput)

    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        Dim obj As New bizMaster.bzEmployee
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        'objXmlInput.LoadXml("<MS_VIEWEMPLOYEESUPERVISORY_INPUT><EmployeeID>21</EmployeeID></MS_VIEWEMPLOYEESUPERVISORY_INPUT>")
        objXmlInput.LoadXml("<MS_VIEWEMPLOYEEIP_INPUT><EmployeeID>21</EmployeeID></MS_VIEWEMPLOYEEIP_INPUT>")
        objXmlOutput = obj.GetAssignedIP(objXmlInput)
    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        Dim obj As New bizMaster.bzEquipment
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<MS_LISTEQUIPMENT_INPUT><EGROUP_CODE>kbd</EGROUP_CODE></MS_LISTEQUIPMENT_INPUT>")
        objXmlOutput = obj.List1(objXmlInput)
    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        Dim obj As New bizMaster.bzEmployee
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlOutput = obj.ListManager()
    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        Dim objXml As New Xml.XmlDocument
        Dim obj As New bizMaster.bzEmployee
        Dim strViewInputXml As String = "<MS_LOGIN_INPUT><Login>admin</Login><Password>admin</Password><IPAddress>127.0.0.1</IPAddress></MS_LOGIN_INPUT>"
        'Dim strViewInputXml As String = "<MS_LOGIN_INPUT><Login>saurav</Login><Password>aams</Password><IPAddress>192.168.12.149</IPAddress></MS_LOGIN_INPUT>"
        objXml.LoadXml(strViewInputXml)
        objXml = obj.Login(objXml)
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Dim obj As New bizMaster.bzPermission
        Dim objXml As New Xml.XmlDocument
        objXml = obj.List()
    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        'Dim objInputXml As New Xml.XmlDocument
        'Dim objOutputXml As New Xml.XmlDocument
        'Dim obj As New bizMaster.bzEmployee
        'objOutputXml = obj.List
    End Sub

    Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click
        Dim obj As New bizMaster.bzEmployee
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlOutput = obj.GetReserveUsernamePassword

    End Sub
End Class