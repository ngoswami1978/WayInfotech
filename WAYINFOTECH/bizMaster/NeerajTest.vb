Public Class NeerajTest

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Dim obj As New bizMaster.bzAirline
        Dim objXml As New Xml.XmlDocument
        objXml = obj.List()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim obj As New bizMaster.bzAirline
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_VIEWAIRLINE_INPUT><Airline_Code>FF</Airline_Code></MS_VIEWAIRLINE_INPUT>")
        objXml = obj.View(objXml)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        '*******FOR INSERT
        Dim obj As New bizMaster.bzAirline
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_UPDATEAIRLINE_INPUT><AIRLINE Action='I' Airline_Code='gq' Name='ganqqqq' Online_Carrier = '0' /></MS_UPDATEAIRLINE_INPUT>")
        objXml = obj.Update(objXml)
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        '*******FOR UPDATE
        Dim obj As New bizMaster.bzAirline
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_UPDATEAIRLINE_INPUT><AIRLINE Action='U' Airline_Code='JW' Name='JAQUARE' Online_Carrier = '0' /></MS_UPDATEAIRLINE_INPUT>")
        objXml = obj.Update(objXml)
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        '*******FOR UPDATE
        Dim obj As New bizMaster.bzAirline
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_DELETEAIRLINE_INPUT><Airline_Code>JW</Airline_Code></MS_DELETEAIRLINE_INPUT>")
        objXml = obj.Delete(objXml)
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        'VIEW AIRLINEOFFICE
        Dim obj As New bizMaster.bzAirlineOffice
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_VIEWAIRLINEOFFICE_INPUT><AR_OF_ID>4</AR_OF_ID></MS_VIEWAIRLINEOFFICE_INPUT>")
        objXml = obj.View(objXml)
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim obj As New bizMaster.bzAirlineOffice
        Dim objXml As New Xml.XmlDocument
        ' objXml.LoadXml("<MS_UPDATEAIRLINEOFFICE_INPUT><AIRLINE_OFFICE Action='I' AR_OF_ID='' Airline_Code='AR' AR_OF_Address='SDSD' Aoffice='DEL' /></MS_UPDATEAIRLINEOFFICE_INPUT>")
        objXml.LoadXml("<MS_UPDATEAIRLINEOFFICE_INPUT><AIRLINE_OFFICE Action='I' AR_OF_ID='' Airline_Code='PK' AR_OF_Address='fgfg' Aoffice='' /></MS_UPDATEAIRLINEOFFICE_INPUT>")
        objXml = obj.Update(objXml)

    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim obj As New bizMaster.bzAirlineOffice
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_UPDATEAIRLINEOFFICE_INPUT><AIRLINE_OFFICE Action='U' AR_OF_ID='0' Airline_Code='FF' AR_OF_Address='NEW DELHI' Aoffice='DEL' /></MS_UPDATEAIRLINEOFFICE_INPUT>")
        objXml = obj.Update(objXml)
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim obj As New bizMaster.bzAirlineOffice
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_DELETEAIRLINEOFFICE_INPUT><AR_OF_ID>7</AR_OF_ID></MS_DELETEAIRLINEOFFICE_INPUT>")
        objXml = obj.Delete(objXml)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim obj As New bizMaster.bzAirline
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_SEARCHAIRLINE_INPUT><Airline_Code></Airline_Code><Name></Name><Online_Carrier>0</Online_Carrier><PAGE_NO>1</PAGE_NO><PAGE_SIZE>100</PAGE_SIZE><SORT_BY></SORT_BY><DESC></DESC></MS_SEARCHAIRLINE_INPUT>")

        objXml = obj.Search(objXml)
    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        Dim obj As New bizMaster.bzDesignation
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_SEARCHDESIGNATION_INPUT><Designation></Designation></MS_SEARCHDESIGNATION_INPUT>")
        ' objXml = obj.View(objXml)
        objXml = obj.Search(objXml)
    End Sub

    
    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        'VIEW DESIGNATION
        Dim obj As New bizMaster.bzDesignation
        Dim objXml As New Xml.XmlDocument
        'objXml.LoadXml("<MS_VIEWDESIGNATION_INPUT><DesignationID>33</DesignationID></MS_VIEWDESIGNATION_INPUT>")
        objXml.LoadXml("<MS_VIEWDESIGNATION_INPUT><DesignationID>15</DesignationID></MS_VIEWDESIGNATION_INPUT>")


        objXml = obj.View(objXml)
    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        Dim obj As New bizMaster.bzDesignation
        Dim objXml As New Xml.XmlDocument
        Dim strScevalue As String = ""
        strScevalue = " <MS_UPDATEDESIGNATION_INPUT><DESIGNATION DesignationID='' Designation='NetworkEngineer'> "
        strScevalue = strScevalue & "<SECURITY SecurityOptionID = '2' Value = '2' />"
        strScevalue = strScevalue & "<SECURITY SecurityOptionID = '3' Value = '3' />"
        strScevalue = strScevalue & "<SECURITY SecurityOptionID = '4' Value = '31' />"
        strScevalue = strScevalue & "<SECURITY SecurityOptionID = '5' Value = '22' />"
        strScevalue = strScevalue & "<SECURITY SecurityOptionID = '6' Value = '4' />"
        strScevalue = strScevalue & "<SECURITY SecurityOptionID = '7' Value = '5' />"
        strScevalue = strScevalue & "<SECURITY SecurityOptionID = '8' Value = '16' />"
        strScevalue = strScevalue & "<SECURITY SecurityOptionID = '9' Value = '23' />"
        strScevalue = strScevalue & "<SECURITY SecurityOptionID = '10' Value = '4' />"
        strScevalue = strScevalue & "<SECURITY SecurityOptionID = '11' Value = '16' />"
        strScevalue = strScevalue & "<SECURITY SecurityOptionID = '12' Value = '17' />"
        strScevalue = strScevalue & "<SECURITY SecurityOptionID = '13' Value = '18' />"
        strScevalue = strScevalue & "<SECURITY SecurityOptionID = '14' Value = '9' />"
        strScevalue = strScevalue & "<SECURITY SecurityOptionID = '15' Value = '10' />"
        strScevalue = strScevalue & "<SECURITY SecurityOptionID = '16' Value = '11' />"
        strScevalue = strScevalue & "</DESIGNATION></MS_UPDATEDESIGNATION_INPUT>"

        objXml.LoadXml(strScevalue)

        objXml = obj.Update(objXml)

    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        Dim obj As New bizMaster.bzDesignation
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_DELETEDESIGNATION_INPUT><DesignationID>33</DesignationID></MS_DELETEDESIGNATION_INPUT>")
        objXml = obj.Delete(objXml)
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Dim obj As New bizMaster.bzDesignation
        Dim objXml As New Xml.XmlDocument
        Dim strScevalue As String = ""

        strScevalue = "<MS_UPDATEDESIGNATION_INPUT><DESIGNATION DesignationID='107' Designation='drfdsfsdfdsf' /> </MS_UPDATEDESIGNATION_INPUT>"

        ''strScevalue = " <MS_UPDATEDESIGNATION_INPUT><DESIGNATION DesignationID='33' Designation='NetworkEngine'> "
        ''strScevalue = strScevalue & "<SECURITY SecurityOptionID = '2' Value = '2' />"
        ''strScevalue = strScevalue & "<SECURITY SecurityOptionID = '3' Value = '2' />"
        ''strScevalue = strScevalue & "<SECURITY SecurityOptionID = '4' Value = '31' />"
        ''strScevalue = strScevalue & "<SECURITY SecurityOptionID = '5' Value = '42' />"
        ''strScevalue = strScevalue & "<SECURITY SecurityOptionID = '6' Value = '5' />"
        ''strScevalue = strScevalue & "<SECURITY SecurityOptionID = '7' Value = '6' />"
        ''strScevalue = strScevalue & "<SECURITY SecurityOptionID = '8' Value = '36' />"
        ''strScevalue = strScevalue & "<SECURITY SecurityOptionID = '9' Value = '33' />"
        ''strScevalue = strScevalue & "<SECURITY SecurityOptionID = '10' Value = '3' />"
        ''strScevalue = strScevalue & "<SECURITY SecurityOptionID = '11' Value = '36' />"
        ''strScevalue = strScevalue & "<SECURITY SecurityOptionID = '12' Value = '3' />"
        ''strScevalue = strScevalue & "<SECURITY SecurityOptionID = '13' Value = '3' />"
        ''strScevalue = strScevalue & "<SECURITY SecurityOptionID = '14' Value = '3' />"
        ''strScevalue = strScevalue & "<SECURITY SecurityOptionID = '15' Value = '3' />"
        ''strScevalue = strScevalue & "<SECURITY SecurityOptionID = '16' Value = '3' />"
        ''strScevalue = strScevalue & "</DESIGNATION></MS_UPDATEDESIGNATION_INPUT>"

        objXml.LoadXml(strScevalue)
        objXml = obj.Update(objXml)

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim obj As New bizMaster.bzAirlineOffice
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_SEARCHAIRLINEOFFICE_INPUT><Airline_Code></Airline_Code><Airline_Name>sdasd</Airline_Name><Aoffice></Aoffice><PAGE_NO>1</PAGE_NO><PAGE_SIZE>100</PAGE_SIZE><SORT_BY></SORT_BY><DESC></DESC></MS_SEARCHAIRLINEOFFICE_INPUT>")

        objXml = obj.Search(objXml)
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Dim obj As New bizMaster.bzDesignation
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_LISTDESIGNATION_OUTPUT><Designation DesignationID='' Designation='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_LISTDESIGNATION_OUTPUT>")
        objXml = obj.List()
    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        Dim obj As New bizMaster.bzPermission
        Dim objXml As New Xml.XmlDocument
        objXml = obj.List()
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click

    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        Dim obj As New bizMaster.bzAOffice
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_UPDATEAOFFICE_INPUT><AOFFICE Action='I' Aoffice='HHH' Region='SOUTH' Address='Vasant Vihar' RegionalHQ='2' BRLimit='0' BR_ID='0' Fax='436546' Pincode='3456345' CityID='378' Phone='' />  </MS_UPDATEAOFFICE_INPUT>")
        objXml = obj.Update(objXml)

    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Dim obj As New bizMaster.bzAgencyGroup
        Dim objXml As New Xml.XmlDocument
        objXml = obj.ListAgencyGroup

    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        Dim obj As New bizMaster.bzDocEmployee
        Dim objXml As New Xml.XmlDocument
        Try
            objXml.LoadXml("<DOCUMENTEMPLOYEE_INPUT><DOCUMENT Aoffice='DEL'/></DOCUMENTEMPLOYEE_INPUT>")
            objXml = obj.List(objXml)
        Catch ex As Exception
        Finally
            objXml = Nothing
        End Try

    End Sub

    Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click
        Dim obj As New bizMaster.bzTemplate
        Dim objXml As New Xml.XmlDocument
        Try
            objXml.LoadXml("<HD_DOCUMENTTEMPLATE_INPUT><TEMPLATES TemplateName='BDR Letter' AOFFICE  ='DEL' AIRLINECODE ='' /></HD_DOCUMENTTEMPLATE_INPUT>")
            objXml = obj.GetDocumentTemplate(objXml)
        Catch ex As Exception
        Finally
            objXml = Nothing
        End Try

    End Sub

    Private Sub btnEquipSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEquipSearch.Click
        'already exist
        Dim obj As New bizMaster.bzEquipment
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_SEARCHEQUIPMENT_INPUT><EGROUP_CODE>CPU</EGROUP_CODE><EQUIPMENT_CODE></EQUIPMENT_CODE><DESCRIPTION></DESCRIPTION><CONFIG></CONFIG> <SORT_BY>EGROUP_CODE</SORT_BY><DESC>0</DESC><PAGE_NO>0</PAGE_NO><PAGE_SIZE>0</PAGE_SIZE></MS_SEARCHEQUIPMENT_INPUT>")

        objXml = obj.Search(objXml)


        '<MS_SEARCHEQUIPMENT_INPUT>
        '	<EGROUP_CODE />
        '	<EQUIPMENT_CODE />
        '   <DESCRIPTION />
        '   <CONFIG />
        '</MS_SEARCHEQUIPMENT_INPUT>


    End Sub

    Private Sub btnEquipModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEquipModify.Click
        Dim obj As New bizMaster.bzEquipment
        Dim objXml As New Xml.XmlDocument
        'objXml.LoadXml("<MS_UPDATEAIRLINE_INPUT><AIRLINE Action='U' Airline_Code='JW' Name='JAQUARE' Online_Carrier = '0' /></MS_UPDATEAIRLINE_INPUT>")
        'objXml.LoadXml("<MS_UPDATEEQUIPMENT_INPUT><EQUIPMENT_DETAIL Action='U' Productid='10' Equipment_Code='8HB' Egroup_Code='LRD' Config='' Description='test' Maintain_Balance_By='0' Cpu_Speed='' Cdr_Speed='' Ram='' Vram='' Hdd='' Printer_Speed='' P_Speed_Measure='P' Monitor_Size='' Modem_Speed='' Lan_Card_Type='' Lan_Card_Speed='' Wan_Card_Type='' Pci_Slots='' Isa_Slots='' Remarks='' Maintain_Balance='' SegExpected='' /></MS_UPDATEEQUIPMENT_INPUT>")
        '<EQUIPMENT_DETAIL                                          Action="I" Productid="" Equipment_Code="123" Egroup_Code="RTR" Config="" Description="test" Maintain_Balance_By="0" Cpu_Speed="" Cdr_Speed="" Ram="" Vram="" Hdd="" Printer_Speed="" P_Speed_Measure="" Monitor_Size="" Modem_Speed="" Lan_Card_Type="" Lan_Card_Speed="" Wan_Card_Type="" Pci_Slots="" Isa_Slots="" Remarks="" Maintain_Balance="False" /> 

        objXml.Load("c:\equipment.xml")
        objXml = obj.Update(objXml)
    End Sub

    Private Sub btnEquipDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEquipDelete.Click


        Dim obj As New bizMaster.bzEquipment
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_DELETEEQUIPMENT_INPUT><Productid>341</Productid></MS_DELETEEQUIPMENT_INPUT>")
        objXml = obj.Delete(objXml)

    End Sub

    Private Sub btnEquipView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEquipView.Click
        Dim obj As New bizMaster.bzEquipment
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_VIEWEQUIPMENT_INPUT><Productid>34</Productid></MS_VIEWEQUIPMENT_INPUT>")
        objXml = obj.View(objXml)

    End Sub

    Private Sub btnRegionList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegionList.Click
        Dim strMsgCBD As String
        Dim strL() As String
        strMsgCBD = "h+f+g+i"

        Dim arrMsgSubCBD() As String
        arrMsgSubCBD = strMsgCBD.Split("+")
        Array.Resize(arrMsgSubCBD, 0)

        strMsgCBD = "h+f+i"

        arrMsgSubCBD = strMsgCBD.Split("+")


        Dim obj As New bizMaster.bzRegion
        Dim objXml As New Xml.XmlDocument
        objXml = obj.RegionList()
    End Sub

    Private Sub Button24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button24.Click
        '*******FOR INSERT
        Dim obj As New bizMaster.bzEmployee
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_UPDATEREGISTRATIONID_INPUT><REGISTRATION ACTION='U' LCODE='2' USERNAME='AMADEUS' PASSWORD='AMADEUS' /></MS_UPDATEREGISTRATIONID_INPUT>")
        objXml = obj.UpdateRegistrationID(objXml)
    End Sub

    Private Sub Button25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button25.Click
        Dim obj As New bizMaster.bzEmployee
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument

        'objXmlInput.LoadXml("<MS_UPDATEEMPLOYEE_INPUT><Employee EmployeeID='43' Aoffice='DEL' DepartmentID='18' Limited_To_OwnAgency='' Limited_To_Region='' Limited_To_Aoffice='' Cell_Phone='34343948' Login='Neeraj' Email='ngoswami@amadeus.co.in' Employee_Name='Neeraj Goswami' ManagerID='43' IPRestriction='2' Password='neeraj' FirstForm='MSSR_Employee' Designation='manager' ImmediateSupervisorID='43' SecurityRegionID='1' DateStart='20071124' DateEnd='20071130' CityId='8' ChangePassword='1' PwdExpire='1' AgreementSigned='1'/></MS_UPDATEEMPLOYEE_INPUT>")
        objXmlInput.LoadXml("<MS_UPDATEEMPLOYEE_INPUT><Employee EmployeeID='' Aoffice='DEL' DepartmentID='1' Limited_To_OwnAgency='0' Limited_To_Region='1' Limited_To_Aoffice='0' Cell_Phone='55555' Login='TEST123412342' Email='ajamwal@amadeus.in' Employee_Name='Abhay Jamwal' ManagerID='8264' IPRestriction='0' Password='aams' FirstForm='MSSR_Employee' Designation='Manager- Markets' ImmediateSupervisorID='200354' SecurityRegionID='5' DateStart='20060522' DateEnd='' CityId='187' ChangePassword='0' PwdExpire='0' AgreementSigned='0' Request='0' /></MS_UPDATEEMPLOYEE_INPUT>")


        objXmlOutput = obj.Update(objXmlInput)

    End Sub

    Private Sub Button26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button26.Click
        Dim obj As New bizMaster.bzEmployee
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<MS_SEARCHEMPLOYEE_INPUT><Employee_Name /><DepartmentID /><Aoffice /><Designation /><SecurityOptionID /><AgreementSigned>False</AgreementSigned><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><SecurityRegionID></SecurityRegionID><Request></Request><PAGE_NO></PAGE_NO><PAGE_SIZE></PAGE_SIZE><SORT_BY></SORT_BY><DESC></DESC> </MS_SEARCHEMPLOYEE_INPUT>")
        objXmlOutput = obj.Search(objXmlInput)

    End Sub

    Private Sub Button27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button27.Click

        Dim obj As New bizMaster.bzEmailGroup
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.Load("c:/error.xml")
        objXmlOutput = obj.Search(objXmlInput)

    End Sub

    Private Sub btnEquipAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEquipAdd.Click

    End Sub

    Private Sub Button28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button28.Click
        Dim obj As New bizMaster.bzEmployee
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<MS_UPDATEREGISTRATIONID_INPUT><REGISTRATION ACTION='U' LCODE='2' USERNAME='A' PASSWORD='A' TRAININGSTATUS='12' /></MS_UPDATEREGISTRATIONID_INPUT>")
        objXmlOutput = obj.UpdateRegistrationID(objXmlInput)
    End Sub

    Private Sub btnEquipList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEquipList.Click

    End Sub

    Private Sub Button29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button29.Click
        Dim obj As New bizMaster.bzPermission
        Dim objXml, objXmlInput As New Xml.XmlDocument

        objXmlInput.LoadXml("<MS_SECURITY_INPUT><GROUPID>6</GROUPID></MS_SECURITY_INPUT>")
        objXml = obj.ListSecurityGroupFilter(objXmlInput)

    End Sub
End Class