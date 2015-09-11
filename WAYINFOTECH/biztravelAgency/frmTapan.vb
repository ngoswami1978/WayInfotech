Imports System.Xml
Imports System.Data
Imports System.Object

Public Class frmTapan
    Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
        Try
            Dim obj As New bizTravelAgency.bzAgency
            Dim objXml As New Xml.XmlDocument
            'Dim str_INPUT As String = "<TA_UPDATEAGENCY_INPUT><Agency LOCATION_CODE='986' Chain_Name='TEST ABC1' Chain_Code='80' IATA_TID='' EMAIL='indicatravels@airtelbroadband.com' FAX='41640210' PHONE='46010107, 46010108,' COUNTRY='India' PINCODE='110049' CITY='New Delhi' ADDRESS1='South Ex part 1' ADDRESS='D-52 Ground Floor ,N D S E,' NAME='testabc1' Aoffice='DEL' CONTACT_PERSON_ID='' ORDERNUMBER_BACKUP='' ORDERNUMBER_PRIMARY='' INSTALL_DATE_BACKUP='' INSTALL_DATE_PRIMARY='' ONLINE_STATUS_BACKUP='' AGENCYSTATUSID='' Agency_Status_Name='' AGENCYTYPEID='1' Agency_Type_Name='' LOCATION_SHORT_NAME='' PRIORITYID='2' PriorityName='' RESP_1A='113' RESP_1A_NAME='' WWW_ADDRESS='NULL' ONLINE_STATUS='' DATE_OFFLINE='' DATE_ONLINE='19941206' FILENO='80' INCLUDE_IN_CCR='False' INCLUDE_IN_CCR_REASON='' EMPLOYEEID='35'/></TA_UPDATEAGENCY_INPUT>"
            'Dim str_INPUT As String = "<TA_UPDATEAGENCY_INPUT><Agency LOCATION_CODE='28230' Chain_Name='ASEEM TRAVELS LTD' Chain_Code='8' IATA_TID='' EMAIL='' FAX='' PHONE='' COUNTRY='India' PINCODE='' CITY='Agartala' ADDRESS1='' ADDRESS='e4rt' NAME='ert' Aoffice='CCU' CONTACT_PERSON_ID='' ORDERNUMBER_BACKUP='' ORDERNUMBER_PRIMARY='' INSTALL_DATE_BACKUP='' INSTALL_DATE_PRIMARY='' ONLINE_STATUS_BACKUP='' AGENCYSTATUSID='' AGENCYTYPEID='' LOCATION_SHORT_NAME='' PRIORITYID='' RESP_1A='' RESP_1A_NAME='' WWW_ADDRESS='' ONLINE_STATUS='' DATE_OFFLINE='' DATE_ONLINE='' FILENO='15284' INCLUDE_IN_CCR='False' INCLUDE_IN_CCR_REASON='' CUSTOMER_CATEGORY_ID='4' EMPLOYEEID='24' IPAddressID='' IPAddress=''/></TA_UPDATEAGENCY_INPUT>"
            'Dim str_INPUT As String = "<TA_UPDATEAGENCY_INPUT><Agency LOCATION_CODE='12550' Chain_Name='OMEGA AIR TRAVELS AGENCY(JAI)' Chain_Code='7333' IATA_TID='14366634' EMAIL='OMEGA2004@REDIFFMAIL.COM' FAX='01482-231100' PHONE='01482-231100-231200' COUNTRY='India' PINCODE='311001' CITY='Bhilwara' ADDRESS1='Old R T O Road,Gandhinagar,Bhilwara' ADDRESS='Shop No 3,Chitrakut Complex,' NAME='Shree Omega Air Travels' Aoffice='JAI' CONTACT_PERSON_ID='' ORDERNUMBER_BACKUP='' ORDERNUMBER_PRIMARY='' INSTALL_DATE_BACKUP='' INSTALL_DATE_PRIMARY='' ONLINE_STATUS_BACKUP='' AGENCYSTATUSID='13' AGENCYTYPEID='' LOCATION_SHORT_NAME='' PRIORITYID='' RESP_1A='250' RESP_1A_NAME='Pranay kulshrestha' WWW_ADDRESS='' ONLINE_STATUS='VRINTL' DATE_OFFLINE='' DATE_ONLINE='20030317' FILENO='6378' INCLUDE_IN_CCR='False' INCLUDE_IN_CCR_REASON='' CUSTOMER_CATEGORY_ID='' EMPLOYEEID='24' IPAddressID='0' IPAddress='' /></TA_UPDATEAGENCY_INPUT>"
            Dim str_INPUT As String = "<TA_UPDATEAGENCY_INPUT><Agency LOCATION_CODE='33573' Chain_Name='OMEGA AIR TRAVELS AGENCY(JAI)' Chain_Code='7333' IATA_TID='14366634' EMAIL='OMEGA2004@REDIFFMAIL.COM' FAX='01482-231100' PHONE='01482-231100-231200' COUNTRY='India' PINCODE='311001' CITY='Bhilwara' ADDRESS1='TEST' ADDRESS='TEST,' NAME='TEST_TAPAN' Aoffice='JAI' CONTACT_PERSON_ID='' ORDERNUMBER_BACKUP='' ORDERNUMBER_PRIMARY='' INSTALL_DATE_BACKUP='' INSTALL_DATE_PRIMARY='' ONLINE_STATUS_BACKUP='' AGENCYSTATUSID='13' AGENCYTYPEID='' LOCATION_SHORT_NAME='' PRIORITYID='' RESP_1A='250' RESP_1A_NAME='Pranay kulshrestha' WWW_ADDRESS='' ONLINE_STATUS='VRINTL' DATE_OFFLINE='' DATE_ONLINE='20030317' FILENO='6378' INCLUDE_IN_CCR='False' INCLUDE_IN_CCR_REASON='' CUSTOMER_CATEGORY_ID='' EMPLOYEEID='24' IPAddressID='0' IPAddress='' PANNO='AFEPN4913A'/></TA_UPDATEAGENCY_INPUT>"

            objXml.LoadXml(str_INPUT)
            objXml = obj.Update(objXml)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub
    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        Dim obj As New bizTravelAgency.bzAgency
        Dim str_INPUT As String = "<MS_DELETEAGENCY_INPUT><LOCATION_CODE>210</LOCATION_CODE></MS_DELETEAGENCY_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(str_INPUT)
        objXml = obj.Delete(objXml)
    End Sub
    Private Sub cmdView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdView.Click
        Dim obj As New bizTravelAgency.bzAgency
        Dim str_INPUT As String = "<TA_VIEWAGENCY_INPUT><LOCATION_CODE>33573</LOCATION_CODE></TA_VIEWAGENCY_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(str_INPUT)
        objXml = obj.View(objXml)
    End Sub
    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        Dim obj As New bizTravelAgency.bzAgency
        'Dim str_INPUT As String = "<MS_SEARCHAGENCY_INPUT><NAME>abcde</NAME><LOCATION_SHORT_NAME></LOCATION_SHORT_NAME><City_Name></City_Name><Country_Name></Country_Name><StatusCode></StatusCode><Aoffice></Aoffice><OFFICEID></OFFICEID><Crs></Crs><ADDRESS></ADDRESS><AgencyStatusId></AgencyStatusId><AgencyTypeId></AgencyTypeId><EMAIL></EMAIL><DATE_ONLINE></DATE_ONLINE><DATE_OFFLINE></DATE_OFFLINE><FAX></FAX><FILENO></FILENO><IATA_TID></IATA_TID><Limited_To_Aoffice>AGR</Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency></MS_SEARCHAGENCY_INPUT>"
        'Dim str_INPUT As String = "<TA_SEARCHAGENCY_INPUT><NAME></NAME><LOCATION_SHORT_NAME></LOCATION_SHORT_NAME><City_Name></City_Name><Country_Name></Country_Name><StatusCode></StatusCode><Aoffice></Aoffice><OFFICEID></OFFICEID><Crs></Crs><ADDRESS></ADDRESS><AgencyStatusId></AgencyStatusId><AgencyTypeId></AgencyTypeId><EMAIL></EMAIL><DATE_ONLINE></DATE_ONLINE><DATE_OFFLINE></DATE_OFFLINE><FAX></FAX><FILENO></FILENO><IATA_TID></IATA_TID><EmployeeID>24</EmployeeID><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><SecurityRegionID></SecurityRegionID><PAGE_NO>1</PAGE_NO><PAGE_SIZE>50</PAGE_SIZE><SORT_BY>nameTEST</SORT_BY><DESC>true</DESC><IPAddress></IPAddress><SearchType>Starting With</SearchType><Online_Status_BackUp></Online_Status_BackUp><PRIORITYID></PRIORITYID><PHONE></PHONE><WWW_ADDRESS></WWW_ADDRESS><RESPONSIBILITY_1A></RESPONSIBILITY_1A><LOCATION_CODE></LOCATION_CODE></TA_SEARCHAGENCY_INPUT>"

        'Dim str_INPUT As String = "<TA_SEARCHAGENCY_INPUT><NAME /> <LOCATION_SHORT_NAME /> <City_Name /> <Country_Name /> <StatusCode /> <Aoffice /> <OFFICEID /> <Crs /> <ADDRESS /> <AgencyStatusId /> <AgencyTypeId /> <EMAIL /> <DATE_ONLINE /> <DATE_OFFLINE /> <FAX /> <FILENO /> <IATA_TID /> <EmployeeID>24</EmployeeID> <Limited_To_Aoffice /> <Limited_To_Region>33</Limited_To_Region> <Limited_To_OwnAagency>0</Limited_To_OwnAagency> <SecurityRegionID>33</SecurityRegionID> <IPAddress /> <SearchType>Contains</SearchType> <Online_Status_BackUp /> <PRIORITYID /> <PHONE /> <WWW_ADDRESS /> <RESPONSIBILITY_1A /> <LOCATION_CODE /> <CHAIN_CODE /> <PAGE_NO>1</PAGE_NO> <PAGE_SIZE>25</PAGE_SIZE> <SORT_BY>NAME</SORT_BY> <DESC>FALSE</DESC> <AgencyUsingBirdres>False</AgencyUsingBirdres> <BR_HD>TRUE</BR_HD> </TA_SEARCHAGENCY_INPUT>"

        'Dim str_INPUT As String = "<TA_SEARCHAGENCY_INPUT><NAME /> <LOCATION_SHORT_NAME /> <City_Name /> <Country_Name /> <StatusCode /> <Aoffice /> <OFFICEID /> <Crs /> <ADDRESS /> <AgencyStatusId /> <AgencyTypeId /> <EMAIL /> <DATE_ONLINE /> <DATE_OFFLINE /> <FAX /> <FILENO /> <IATA_TID /> <EmployeeID>24</EmployeeID> <Limited_To_Aoffice /> <Limited_To_Region>33</Limited_To_Region> <Limited_To_OwnAagency>0</Limited_To_OwnAagency> <SecurityRegionID>33</SecurityRegionID> <IPAddress /> <SearchType>Contains</SearchType> <Online_Status_BackUp /> <PRIORITYID /> <PHONE /> <WWW_ADDRESS /> <RESPONSIBILITY_1A /> <LOCATION_CODE /> <CHAIN_CODE /> <PAGE_NO>1</PAGE_NO> <PAGE_SIZE>25</PAGE_SIZE> <SORT_BY>NAME</SORT_BY> <DESC>FALSE</DESC> <AgencyUsingBirdres>True</AgencyUsingBirdres> <BR_HD>TRUE</BR_HD> </TA_SEARCHAGENCY_INPUT>"

        'Dim str_INPUT As String = "<TA_SEARCHAGENCY_INPUT><NAME /> <LOCATION_SHORT_NAME /> <City_Name /> <Country_Name /> <StatusCode /> <Aoffice /> <OFFICEID /> <Crs /> <ADDRESS /> <AgencyStatusId /> <AgencyTypeId /> <EMAIL /> <DATE_ONLINE /> <DATE_OFFLINE /> <FAX /> <FILENO /> <IATA_TID /> <EmployeeID>24</EmployeeID> <Limited_To_Aoffice /> <Limited_To_Region>33</Limited_To_Region> <Limited_To_OwnAagency>0</Limited_To_OwnAagency> <SecurityRegionID>33</SecurityRegionID> <IPAddress /> <SearchType>Contains</SearchType> <Online_Status_BackUp /> <PRIORITYID /> <PHONE /> <WWW_ADDRESS /> <RESPONSIBILITY_1A /> <LOCATION_CODE /> <CHAIN_CODE /> <PAGE_NO>1</PAGE_NO> <PAGE_SIZE>25</PAGE_SIZE> <SORT_BY>NAME</SORT_BY> <DESC>FALSE</DESC> <AgencyUsingBirdres>True</AgencyUsingBirdres> <BR_HD>TRUE</BR_HD> </TA_SEARCHAGENCY_INPUT>"

        Dim str_INPUT As String = "<TA_SEARCHAGENCY_INPUT><NAME /><LOCATION_SHORT_NAME /><City_Name /><Country_Name /><StatusCode /><Aoffice /><OFFICEID /><Crs /><ADDRESS /><AgencyStatusId /><AgencyTypeId /><EMAIL /><DATE_ONLINE /><DATE_OFFLINE /><FAX /><FILENO /><IATA_TID /><EmployeeID>24</EmployeeID><Limited_To_Aoffice /><Limited_To_Region>33</Limited_To_Region><Limited_To_OwnAagency>0</Limited_To_OwnAagency><SecurityRegionID>33</SecurityRegionID><IPAddress /><SearchType>Contains</SearchType><Online_Status_BackUp /><PRIORITYID /><PHONE /><WWW_ADDRESS /><RESPONSIBILITY_1A /><LOCATION_CODE /><CHAIN_CODE /><PAGE_NO>1</PAGE_NO><PAGE_SIZE>25</PAGE_SIZE><SORT_BY>NAME</SORT_BY><DESC>FALSE</DESC><AgencyUsingBirdres>True</AgencyUsingBirdres><BR_HD>TRUE</BR_HD></TA_SEARCHAGENCY_INPUT>"


        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(str_INPUT)
        'objXml.Load("C:/emp.xml")

        objXml = obj.Search(objXml)
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim obj As New bizTravelAgency.bzAgency
        'Dim str_INPUT As String = "<UP_GETAGENCYSTAFFDETAILS_INPUT><LCODE>310</LCODE></UP_GETAGENCYSTAFFDETAILS_INPUT>"
        Dim str_INPUT As String = "<UP_GETAGENCYSTAFFDETAILS_INPUT><LCODE>28586</LCODE></UP_GETAGENCYSTAFFDETAILS_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(str_INPUT)
        objXml = obj.GetAgencyStaffDetails(objXml)
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        Dim obj As New bizTravelAgency.bzAgencyStaff
        Dim str_INPUT As String = "<MS_UPDATEAGENCYSTAFFDETAILS_INPUT><AGENCYSTAFF AGENCYSTAFFID='11480825' LCODE='20265' STAFFNAME='TRYTRY' DESIGNATION='Accounts Officer Western Region' DOW='20110905' DOB='19920902' CORRESPONDENCE='0' RESPONSIBLE='0' FAX='' PHONE='' EMAIL='a@gmail.com' NOTES='' CONTACT_PERSON='FALSE' SIGNINID='5465zx' Title='Mr.' FIRSTNAME='TRYTRY' MIDDLENAME='' SURNAME='gupta' MOBILENO='9855677878' MARTIALSTATUS='No' /></MS_UPDATEAGENCYSTAFFDETAILS_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(str_INPUT)
        objXml = obj.Update(objXml)

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim obj As New bizTravelAgency.bzAgencyStaff
        Dim str_INPUT As String = "<MS_DELETEAGENCYSTAFFDETAILS_INPUT><AGENCYSTAFFID>157</AGENCYSTAFFID></MS_DELETEAGENCYSTAFFDETAILS_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(str_INPUT)
        objXml = obj.Delete(objXml)


    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim obj As New bizTravelAgency.bzAgencyStaff
        Dim str_INPUT As String = "<MS_VIEWAGENCYSTAFF_INPUT><AGENCYSTAFFID>11214948</AGENCYSTAFFID></MS_VIEWAGENCYSTAFF_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(str_INPUT)
        objXml = obj.View(objXml)


    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim obj As New bizTravelAgency.bzAgency
        Dim str_INPUT As String = "<UP_GETCRSDETAILS_INPUT><LCODE>1212</LCODE></UP_GETCRSDETAILS_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(str_INPUT)
        objXml = obj.GetAgencyCrsUseDetails(objXml)

    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Dim obj As New bizTravelAgency.bzAgency
        Dim str_INPUT As String = "<UP_GETCOMPETITIONDETAILS_INPUT><LOCATION_CODE>45454</LOCATION_CODE></UP_GETCOMPETITIONDETAILS_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(str_INPUT)
        objXml = obj.GetAgencyCompetitionDetails(objXml)

    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim obj As New bizTravelAgency.bzAgencyCRSUse
        'Dim str_INPUT As String = "<UP_UPDATECRSDETAILS_INPUT><CRS RN='133' LCODE='181' DATE='' CURRENTID='1' CRS='1P' OFFICEID='AMDSI3454'  /></UP_UPDATECRSDETAILS_INPUT>"
        'Dim str_INPUT As String = "<UP_UPDATECRSDETAILS_INPUT><CRS RN='' LCODE='181' DATE='' CURRENTID='1' CRS='1P' OFFICEID='NEERAJ' Action='I' /></UP_UPDATECRSDETAILS_INPUT>"


        Dim objXml As New Xml.XmlDocument
        'objXml.LoadXml(str_INPUT)

        objXml.Load("c:\crs.xml")
        objXml = obj.Update(objXml)

    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim obj As New bizTravelAgency.bzAgencyCRSUse
        Dim str_INPUT As String = "<MS_VIEWAGENCYCRSDETAIL_INPUT><RN>156</RN></MS_VIEWAGENCYCRSDETAIL_INPUT>"

        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(str_INPUT)
        objXml = obj.View(objXml)

    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim obj As New bizTravelAgency.bzAgencyCRSUse
        Dim str_INPUT As String = "<MS_DELETEAGENCYCRSDETAILS_INPUT><RN>154</RN></MS_DELETEAGENCYCRSDETAILS_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(str_INPUT)
        objXml = obj.Delete(objXml)

    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Dim obj As New bizTravelAgency.bzAgencyCompetition
        'Dim str_INPUT As String = "<MS_VIEWAGENCYCOMPETITIONDETAIL_INPUT><CRSID>2</CRSID><LOCATION_CODE>304</LOCATION_CODE></MS_VIEWAGENCYCOMPETITIONDETAIL_INPUT>"
        Dim str_INPUT As String = "<MS_VIEWAGENCYCOMPETITIONDETAIL_INPUT><CRSID>3</CRSID><LOCATION_CODE>304</LOCATION_CODE></MS_VIEWAGENCYCOMPETITIONDETAIL_INPUT>"

        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(str_INPUT)
        objXml = obj.View(objXml)

    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        Dim obj As New bizTravelAgency.bzAgencyCompetition
        'Dim str_INPUT As String = "<UP_UPDATECOMPETITIONDETAILS_INPUT><COMPETITION_DETAILS ACTION ='U' LOCATION_CODE ='231' CRSID='2' DATE_END='20071206' DATE_START='20061201' SOLE_USER='1' DIAL_BACKUP='1' ONLINESTATUSID='X25' PRINTER_COUNT='22' PC_COUNT='2' /></UP_UPDATECOMPETITIONDETAILS_INPUT>"
        Dim str_INPUT As String = "<UP_UPDATECOMPETITIONDETAILS_INPUT><COMPETITION_DETAILS ACTION='U' LOCATION_CODE='304' CRSID='3' DATE_END='' DATE_START='' SOLE_USER='0' DIAL_BACKUP='0' ONLINESTATUSID='' PRINTER_COUNT='1' PC_COUNT='1' CommercialDetails='TEST' /></UP_UPDATECOMPETITIONDETAILS_INPUT>"

        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(str_INPUT)
        objXml = obj.Update(objXml)

    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Dim obj As New bizTravelAgency.bzAgencyCompetition
        Dim str_INPUT As String = "<MS_DELETEAGENCYCOMPETITIONDETAILS_INPUT><CRSID>1212</CRSID><LOCATION_CODE>220</LOCATION_CODE></MS_DELETEAGENCYCOMPETITIONDETAILS_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(str_INPUT)
        objXml = obj.Delete(objXml)

    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click

        Dim obj As New bizTravelAgency.bzAgencyCRSUse
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument

        objXmlInput.LoadXml("<UP_UPDATECRSDETAILS_INPUT>" & _
        " <CRS RN='174' LCODE='220' DATE='' CURRENTID='Y' CRS='IP' OFFICEID='CCU' Action='U' /> " & _
        " <CRS RN='171' LCODE='220' DATE='' CURRENTID='Y' CRS='1P' OFFICEID='BOM' Action='D' /> " & _
        " <CRS RN='162' LCODE='220' DATE='' CURRENTID='N' CRS='IG' OFFICEID='BLR' Action='D' /> " & _
        " <CRS RN='163' LCODE='220' DATE='' CURRENTID='N' CRS='4' OFFICEID='ccu' Action='D' /> " & _
        " <CRS RN='166' LCODE='220' DATE='' CURRENTID='N' CRS='4' OFFICEID='Agr' Action='D' /> " & _
        " <CRS RN='168'  LCODE='220' DATE='' CURRENTID='N' CRS='1W' OFFICEID='Bom' Action='D' /> " & _
        " </UP_UPDATECRSDETAILS_INPUT>")
        objXmlOutput = obj.Update(objXmlInput)

    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Dim obj As New bizTravelAgency.bzAgencyCompetition
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
      
        objXmlInput.LoadXml("<UP_UPDATECOMPETITIONDETAILS_INPUT>" & _
               " <COMPETITION_DETAILS Action='U' LOCATION_CODE='229' CRSID='3' DATE_END='20061219' DATE_START='20071217' SOLE_USER='0' DIAL_BACKUP='0' ONLINESTATUSID='X25' PRINTER_COUNT='10' PC_COUNT='15' /> " & _
               " </UP_UPDATECOMPETITIONDETAILS_INPUT>")


   
        objXmlOutput = obj.Update(objXmlInput)
    End Sub
    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        ''get filenumber 
        Dim obj As New bizTravelAgency.bzAgency
        'Dim str_INPUT As String = "<T_C_AGENCY_FILENO><FILENUMBER FileNo='' /><Errors Status=''><Error Code='' Description='' /></Errors></T_C_AGENCY_FILENO>"
        Dim objXml As New Xml.XmlDocument
        'objXml.LoadXml(str_INPUT)
        objXml = obj.GetFileNumber()
    End Sub
    Private Sub cmdGetHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetHistory.Click
        Dim obj As New bizTravelAgency.bzAgency
        Dim str_INPUT As String = "<MS_GETAGENCYHISTORY_INPUT><LOCATION_CODE>220</LOCATION_CODE></MS_GETAGENCYHISTORY_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(str_INPUT)
        objXml = obj.GetHistory(objXml)
    End Sub
    Private Sub cmdConnectivityHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConnectivityHistory.Click
        Dim obj As New bizTravelAgency.bzAgency
        Dim str_INPUT As String = "<MS_GETAGENCYCONNECTIVITYHISTORY_INPUT><LOCATION_CODE>220</LOCATION_CODE></MS_GETAGENCYCONNECTIVITYHISTORY_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(str_INPUT)
        objXml = obj.GetConnectivityHistory(objXml)
    End Sub

    Private Sub cmdFileStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFileStatus.Click
        Dim obj As New bizTravelAgency.bzAgency
        Dim str_INPUT As String = "<UP_GETFINENOSTATUS_INPUT><FILENO>10356</FILENO></UP_GETFINENOSTATUS_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(str_INPUT)
        objXml = obj.ScannedFileStatus(objXml)
    End Sub

    Private Sub cmdScannedDocument_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdScannedDocument.Click
        Dim obj As New bizTravelAgency.bzOrder
        Dim ds As DataSet
        Dim str_INPUT As String = "<TA_GETSCANNEDIMAGE_INPUT><ID>4</ID></TA_GETSCANNEDIMAGE_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(str_INPUT)
        ds = obj.GetScannedImage(objXml)
    End Sub

    Private Sub cmdMiscDocument_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMiscDocument.Click
        Dim obj As New bizTravelAgency.bzOrder
        Dim ds As DataSet
        Dim str_INPUT As String = "<TA_GETMISCSCANNEDDOCUMENT_INPUT><LCode>1</LCode><FileNo></FileNo></TA_GETMISCSCANNEDDOCUMENT_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(str_INPUT)
        ds = obj.GetMiscScannedDocument(objXml)

    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        ''get filenumber FROM TABLE TC_FILLING 
        Dim obj As New bizTravelAgency.bzOrder
        Dim objXml As New Xml.XmlDocument
        'objXml.LoadXml(str_INPUT)
        objXml = obj.GetFileNumber()
    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        Try
            Dim obj As New bizTravelAgency.bzAgency
            Dim objXml As New Xml.XmlDocument
            Dim str_INPUT As String = "<UP_GETAGENCYGROUP_INPUT><LOCATION_CODE>662</LOCATION_CODE></UP_GETAGENCYGROUP_INPUT>"
            objXml.LoadXml(str_INPUT)
            objXml = obj.AgencyGroup_Type(objXml)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub cmdofficeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdofficeSearch.Click
        Try
            Dim obj As New bizTravelAgency.bzAgency
            Dim objXml As New Xml.XmlDocument
            'Dim str_INPUT As String = "<TA_SEARCHOFFICEID_INPUT><OFFICEID></OFFICEID><CityCode></CityCode><ALLOCATED>false</ALLOCATED><CCode></CCode><PAGE_NO>1</PAGE_NO><PAGE_SIZE>25</PAGE_SIZE><SORT_BY>NAME</SORT_BY><DESC>FALSE</DESC></TA_SEARCHOFFICEID_INPUT>"
            Dim str_INPUT As String = "<TA_SEARCHAGENCY_INPUT>   <NAME />   <LOCATION_SHORT_NAME />   <City_Name />   <Country_Name />   <StatusCode />   <Aoffice />   <OFFICEID />   <Crs />   <ADDRESS />   <AgencyStatusId />   <AgencyTypeId />   <EMAIL />   <DATE_ONLINE />   <DATE_OFFLINE />   <FAX />   <FILENO />   <IATA_TID />   <EmployeeID>24</EmployeeID>   <Limited_To_Aoffice />   <Limited_To_Region>33</Limited_To_Region>   <Limited_To_OwnAagency>0</Limited_To_OwnAagency>   <SecurityRegionID>33</SecurityRegionID>   <IPAddress />   <SearchType>Contains</SearchType>   <Online_Status_BackUp />   <PRIORITYID />   <PHONE />   <WWW_ADDRESS />   <RESPONSIBILITY_1A />   <LOCATION_CODE />   <CHAIN_CODE />   <PAGE_NO>1</PAGE_NO>   <PAGE_SIZE>25</PAGE_SIZE>   <SORT_BY>NAME</SORT_BY>   <DESC>FALSE</DESC>   <AgencyUsingBirdres>True</AgencyUsingBirdres>  <BR_HD>TRUE</BR_HD>   </TA_SEARCHAGENCY_INPUT>"
            objXml.LoadXml(str_INPUT)
            objXml = obj.Search(objXml)
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub cmdofficeUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdofficeUpdate.Click
        Try
            Dim obj As New bizTravelAgency.bzOfficeID
            Dim objXml As New Xml.XmlDocument
            Dim str_INPUT As String = "<TA_UPDATEOFFICEID_INPUT><OFFICEID OFFICEID='AFGPW3001' NAME=' ' CID='1' PROCESSING_DATE='20080121' TERMINALID='1234' REMARKS='2' /></TA_UPDATEOFFICEID_INPUT>"
            objXml.LoadXml(str_INPUT)
            objXml = obj.Update(objXml)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub
    Private Sub cmdofficeView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdofficeView.Click
        Try
            Dim obj As New bizTravelAgency.bzOfficeID
            Dim objXml As New Xml.XmlDocument
            Dim str_INPUT As String = "<TA_VIEWOFFICEID_INPUT><OFFICEID>DELD42806</OFFICEID></TA_VIEWOFFICEID_INPUT>"
            objXml.LoadXml(str_INPUT)
            objXml = obj.View(objXml)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub
    Private Sub ListCorporateCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListCorporateCode.Click
        Try
            Dim obj As New bizTravelAgency.bzCorporateCodes
            Dim objXml As New Xml.XmlDocument
            objXml = obj.ListCorporateCode()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub
    Private Sub LstOfficeType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LstOfficeType.Click
        Try
            Dim obj As New bizTravelAgency.bzOfficeID
            Dim objXml As New Xml.XmlDocument
            objXml = obj.ListOfficeType()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub
    Private Sub cmdGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGenerate.Click
        Try
            Dim obj As New bizTravelAgency.bzOfficeID
            Dim objXml As New Xml.XmlDocument
            Dim str_INPUT As String = "<TA_GENERATEOFFICEID_INPUT><GENERATEOFFICEID OFFICEID='' CityCode='CHU' CorporateCode='PW' CorporateQualifier='2' OFFICEID_TYPE_ID='1' /></TA_GENERATEOFFICEID_INPUT>"
            objXml.LoadXml(str_INPUT)
            objXml = obj.GenerateOfficeID(objXml)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub
    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Try
            Dim obj As New bizTravelAgency.bzOfficeID
            Dim objXml As New Xml.XmlDocument
            Dim str_INPUT As String = "<TA_AGENCYCORPORATECODE_INPUT><LCode>669</LCode></TA_AGENCYCORPORATECODE_INPUT>"
            objXml.LoadXml(str_INPUT)
            objXml = obj.GetAgencyCorporateCode(objXml)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        Try
            Dim obj As New bizTravelAgency.bzCorporateCodes
            Dim objXml As New Xml.XmlDocument
            Dim str_INPUT As String = "<MS_SEARCHOFFICEIDQUALIFIERS_INPUT><Code></Code><Qualifier></Qualifier><Description></Description><PAGE_NO>1</PAGE_NO><PAGE_SIZE>25</PAGE_SIZE><SORT_BY>Code</SORT_BY><DESC>FALSE</DESC></MS_SEARCHOFFICEIDQUALIFIERS_INPUT>"
            objXml.LoadXml(str_INPUT)
            objXml = obj.Search(objXml)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub
    Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click
        Try
            Dim obj As New bizTravelAgency.bzOfficeID
            Dim objXml As New Xml.XmlDocument
            Dim str_INPUT As String = "<TA_RPT_SUMMARYOFFICEID_INPUT><CityCode></CityCode><ALLOCATED></ALLOCATED><SUMMARYTYPE>2</SUMMARYTYPE></TA_RPT_SUMMARYOFFICEID_INPUT>"
            objXml.LoadXml(str_INPUT)
            objXml = obj.rptOfficeIDSummary(objXml)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim obj As New bizTravelAgency.bzAgencyStaff
        'Dim str_INPUT As String = "<TA_SEARCHSTAFF_INPUT><OFFICEID/><STAFFNAME></STAFFNAME><AGENCYNAME>cox</AGENCYNAME></TA_SEARCHSTAFF_INPUT>"
        'Dim str_INPUT As String = "<TA_SEARCHSTAFF_INPUT><LCODE>20590</LCODE><STAFFNAME></STAFFNAME><AGENCYNAME></AGENCYNAME><OFFICEID/><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><EmployeeID>24</EmployeeID><Source>Helpdesk</Source></TA_SEARCHSTAFF_INPUT>"
        Dim str_INPUT As String = "<TA_SEARCHSTAFF_INPUT><SIGNINID></SIGNINID><Source></Source><STYPE></STYPE><LCODE>13106</LCODE><STAFFNAME></STAFFNAME><AGENCYNAME/><OFFICEID>delgl3503</OFFICEID><Limited_To_Aoffice /><Limited_To_Region>33</Limited_To_Region><Limited_To_OwnAagency /><PAGE_NO /><PAGE_SIZE /><SORT_BY /> <DESC /> <EmployeeID>24</EmployeeID> </TA_SEARCHSTAFF_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(str_INPUT)
        objXml = obj.Search(objXml)
    End Sub

   
    Private Sub Button24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button24.Click
        Try
            Dim obj As New bizTravelAgency.bzAgency
            Dim str_INPUT As String = "<MS_VIEWAGENCYORDERFEASIBILITY_INPUT><LOCATION_CODE>21229</LOCATION_CODE><USEORIGINAL>True</USEORIGINAL></MS_VIEWAGENCYORDERFEASIBILITY_INPUT>"
            Dim objInputXml As New Xml.XmlDocument
            Dim objOutputXML As New Xml.XmlDocument
            objInputXml.LoadXml(str_INPUT)
            objOutputXML = obj.OrderFeasibility(objInputXml)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button25.Click
        Try
            Dim obj As New bizTravelAgency.bzAgencyNotes
            Dim objInputXml As New Xml.XmlDocument
            Dim objOutputXML As New Xml.XmlDocument
            Dim strinput As String
            strinput = "<MS_GETAGENCYNOTES_INPUT><LCode>4251</LCode></MS_GETAGENCYNOTES_INPUT>"
            objInputXml.LoadXml(strinput)
            objOutputXML = obj.GetDetails(objInputXml)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button26.Click
        'MS_GETMISCINSTALLATION_INPUT
        Try
            Dim obj As New bizTravelAgency.bzMiscInstallation
            Dim objInputXml As New Xml.XmlDocument
            Dim objOutputXML As New Xml.XmlDocument
            Dim strinput As String
            strinput = "<MS_GETMISCINSTALLATION_INPUT><LCode>20265</LCode></MS_GETMISCINSTALLATION_INPUT>"
            objInputXml.LoadXml(strinput)
            objOutputXML = obj.GetInstalledMiscHW(objInputXml)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button27.Click
        Try

            Dim obj As New bizTravelAgency.bzAgency
            Dim objInputXml As New Xml.XmlDocument
            Dim objOutputXML As New Xml.XmlDocument
            Dim strinput As String
            strinput = "<MS_GETPCINSTALLATION_INPUT><LCODE>2</LCODE></MS_GETPCINSTALLATION_INPUT>"
            objInputXml.LoadXml(strinput)
            objOutputXML = obj.GetInstalledPC(objInputXml)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button28.Click
        Try
            '<MS_GETHISTORYORDER_INPUT><ORDERID></ORDERID></MS_GETHISTORYORDER_INPUT>
            Dim obj As New bizTravelAgency.bzOrder
            Dim objInputXml As New Xml.XmlDocument
            Dim objOutputXML As New Xml.XmlDocument
            Dim strinput As String
            '<MS_GETHISTORYORDER_INPUT><ORDERID>36342</ORDERID><PAGE_NO>1</PAGE_NO><PAGE_SIZE>25</PAGE_SIZE><SORT_BY>EMPLOYEENAME</SORT_BY><DESC>FALSE</DESC></MS_GETHISTORYORDER_INPUT>
            strinput = "<MS_GETHISTORYORDER_INPUT><ORDERID>35868</ORDERID><PAGE_NO>1</PAGE_NO><PAGE_SIZE>25</PAGE_SIZE><SORT_BY>EMPLOYEENAME</SORT_BY><DESC>FALSE</DESC></MS_GETHISTORYORDER_INPUT>"
            objInputXml.LoadXml(strinput)
            objOutputXML = obj.GetHistory(objInputXml)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button29.Click
        Try
            Dim obj As New bizTravelAgency.bzAgency
            Dim objInputXml As New Xml.XmlDocument
            Dim objOutputXML As New Xml.XmlDocument
            Dim strinput As String
            strinput = "<UP_GETCOMPETITIONDETAILS_INPUT><LOCATION_CODE>25931</LOCATION_CODE></UP_GETCOMPETITIONDETAILS_INPUT>"
            objInputXml.LoadXml(strinput)
            objOutputXML = obj.GetAgencyCompetitionDetails(objInputXml)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button30.Click
        Dim obj As New bizTravelAgency.bzOrder
        Dim objXml As New Xml.XmlDocument
        objXml = obj.ListOrderingConfigValue()
    End Sub

    Private Sub Button31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button31.Click
        Try
            Dim objxml As New XmlDocument
            Dim obj As New bizTravelAgency.bzOrder
            objxml.LoadXml("<TA_GETSCANNEDDOCUMENT_INPUT><LCode>19527</LCode><FileNo></FileNo></TA_GETSCANNEDDOCUMENT_INPUT>")
            objxml = obj.GetMiscDocumentDetail(objxml)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button32.Click
        Try
            Dim objxml As New XmlDocument
            Dim obj As New bizTravelAgency.bzAgency
            Dim STRINPUT As String
            STRINPUT = "<TA_SENDMAILAGENCY_INPUT><AGENCYFILE ID='' FILENO='0' ORDERNO='2008/8/27' DOCTYPE='2' SEQUENCE='' ORDERTYPE='Add Back up Vrintl' DOCUMENT='' ContentType='2' EmailFrom='ngoswami@amadeus.co.in' EmailTo='abhishek@amadeus.co.in,ngoswami@amadeus.co.in' EmailSubject='Agency : Cosmos International Air Travels P ; Order Number : 2008/8/27' EmailBody='ABC' PDFDocFileName='' LCode='161' /></TA_SENDMAILAGENCY_INPUT>"
            objxml.LoadXml(STRINPUT)
            objxml = obj.SendMail(objxml)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub Button33_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button33.Click
        Try
            Dim obj As New bizTravelAgency.bzOrder
            Dim objInputXml As New Xml.XmlDocument
            Dim ds As DataSet
            Dim strinput As String
            strinput = "<TA_GETSCANNEDIMAGE_INPUT><ID>35987</ID></TA_GETSCANNEDIMAGE_INPUT>"
            objInputXml.LoadXml(strinput)
            ds = obj.GetScannedImage(objInputXml)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button34_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button34.Click
        Try
            Dim obj As New bizTravelAgency.bzOrder
            Dim objInputXml, objOutputXml As New Xml.XmlDocument
            Dim strinput As String
            strinput = ("<UP_TA_UPDATE_PCINSTALLATION_INPUT><DETAIL LCODE='31233' DATE='20090217' ADDLRAM='' CPUTYPE='' CPUNO='' MONTYPE='' MONNO='' KBDTYPE='' KBDNO='' CDRNO='' MSETYPE='' MSENO='' OrderNumber='2009/2/151' Qty='1' REMARKS='' CHALLANDATE='' CHALLANNUMBER='0' LoggedBy='24' LoggedDateTime=''  CHALLANSTATUS='' ROWID='' PCTYPE='0' USE_BACKDATED_CHALLAN='True' OVERRIDE_CHALLAN_NO='True' OVERRIDE_CHALLAN_SERIAL_NO='True' OVERRIDE_ORDER_NO='True'/></UP_TA_UPDATE_PCINSTALLATION_INPUT>")
            objInputXml.LoadXml(strinput)
            objOutputXml = obj.Update(objInputXml)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button36_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button36.Click
        Try
            Dim obj As New bizTravelAgency.bzPCInstallation
            Dim objInputXml, objOutputXml As New Xml.XmlDocument
            Dim strinput As String
            'strinput = ("<UP_TA_UPDATE_PCINSTALLATION_INPUT><DETAIL LCODE='31233' DATE='20090217' ADDLRAM='' CPUTYPE='' CPUNO='' MONTYPE='' MONNO='' KBDTYPE='' KBDNO='' CDRNO='' MSETYPE='' MSENO='' OrderNumber='2009/2/151' Qty='1' REMARKS='' CHALLANDATE='' CHALLANNUMBER='0' LoggedBy='24' LoggedDateTime=''  CHALLANSTATUS='' ROWID='' PCTYPE='0' USE_BACKDATED_CHALLAN='False' OVERRIDE_CHALLAN_NO='True' OVERRIDE_CHALLAN_SERIAL_NO='True' OVERRIDE_ORDER_NO='True'/></UP_TA_UPDATE_PCINSTALLATION_INPUT>")
            strinput = ("<UP_TA_UPDATE_PCINSTALLATION_INPUT><DETAIL LCODE='31190' DATE='20090205' ADDLRAM='' CPUTYPE='' CPUNO='' MONTYPE='' MONNO='' KBDTYPE='' KBDNO='' CDRNO='' MSETYPE='' MSENO='' OrderNumber='2009/1/500' Qty='1' REMARKS='re' CHALLANDATE='' CHALLANNUMBER='0' LoggedBy='202569' LoggedDateTime=''       CHALLANSTATUS='' ROWID='' PCTYPE='0' USE_BACKDATED_CHALLAN='False' OVERRIDE_CHALLAN_NO='False' OVERRIDE_CHALLAN_SERIAL_NO='False' OVERRIDE_ORDER_NO='False'/></UP_TA_UPDATE_PCINSTALLATION_INPUT>")

            objInputXml.LoadXml(strinput)
            objOutputXml = obj.Update(objInputXml)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button37_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button37.Click
        Dim obj As New bizTravelAgency.bzOrder
        Dim objInputXml, objOutputXml As New Xml.XmlDocument
        Dim strinput As String
        strinput = ("<UP_SEARCHORDER_INPUT><LCODE /> <ORDER_NUMBER /> <ORDERTYPEID /> <ORDERSTATUSID /> <REGION /> <AGENCYNAME /> <GROUPDATA>0</GROUPDATA> <City>Agra</City> <Country /> <MSG_SEND_DATE_FROM /> <SENDBACK_DATE_FROM /> <APPROVAL_DATE_FROM /> <RECEIVED_DATE_FROM /> <PROCESSED_DATE_FROM /> <MSG_SEND_DATE_TO /> <SENDBACK_DATE_TO /> <APPROVAL_DATE_TO /> <RECEIVED_DATE_TO /> <PROCESSED_DATE_TO /> <Limited_To_OwnAagency /> <Limited_To_Aoffice /> <Limited_To_Region>33</Limited_To_Region> <PAGE_NO>1</PAGE_NO> <PAGE_SIZE>25</PAGE_SIZE> <SORT_BY>AGENCYNAME</SORT_BY> <DESC>FALSE</DESC> <EmployeeID>24</EmployeeID> </UP_SEARCHORDER_INPUT>")
        'strinput = "<MS_VIEWORDER_INPUT><ORDERID>3770</ORDERID></MS_VIEWORDER_INPUT>"
        objInputXml.LoadXml(strinput)
        objOutputXml = obj.Search(objInputXml)
        'objOutputXml = obj.View(objInputXml)
    End Sub

    Private Sub Button38_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button38.Click
        'Dim obj As New bizTravelAgency.bzAgency
        Dim obj As New bizTravelAgency.bzAgencyType
        Dim objInputXml, objOutputXml As New Xml.XmlDocument
        Dim strinput As String
        strinput = ("<RP_AGENCY_INPUT><AGENCY NAME='' LOCATION_SHORT_NAME='' City_Name='Agra' Country_Name='' StatusCode='' Aoffice='' OFFICEID='' Crs='' ADDRESS='' AgencyStatusId='' AgencyTypeId='' EMAIL='' DATE_ONLINE='' DATE_OFFLINE='' FAX='' FILENO='' IATA_TID='' EmployeeID='24' Limited_To_Aoffice='' Limited_To_Region='33' Limited_To_OwnAagency='0' SecurityRegionID='33' /></RP_AGENCY_INPUT>")
        objInputXml.LoadXml(strinput)
        'objOutputXml = obj.AgencyList(objInputXml)
        objOutputXml = obj.ListGroupAgencyType
    End Sub

    Private Sub Button42_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button42.Click
        Try

            Dim obj As New bizTravelAgency.bzStaffDesignation
            Dim str_INPUT As String = "<MS_UPDATEDESIGNATION_OUTPUT><DESIGNATION DesignationID='' Designation='Tech support2'></DESIGNATION><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEDESIGNATION_OUTPUT>"
            Dim objXml As New Xml.XmlDocument
            objXml.LoadXml(str_INPUT)
            objXml = obj.Update(objXml)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button39_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button39.Click
        Dim obj As New bizTravelAgency.bzStaffDesignation
        Dim str_INPUT As String = "<MS_SEARCHDESIGNATION_INPUT><Designation>tech</Designation></MS_SEARCHDESIGNATION_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(str_INPUT)
        objXml = obj.Search(objXml)
    End Sub

    Private Sub Button40_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button40.Click
        Dim obj As New bizTravelAgency.bzStaffDesignation
        Dim str_INPUT As String = "<MS_VIEWDESIGNATION_INPUT><DesignationID>1</DesignationID></MS_VIEWDESIGNATION_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(str_INPUT)
        objXml = obj.View(objXml)
    End Sub

    Private Sub Button41_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button41.Click
        Dim obj As New bizTravelAgency.bzStaffDesignation
        Dim str_INPUT As String = "<MS_DELETEDESIGNATION_INPUT><DesignationID>1</DesignationID></MS_DELETEDESIGNATION_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(str_INPUT)
        objXml = obj.Delete(objXml)
    End Sub

    Private Sub Button43_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button43.Click
        Dim obj As New bizTravelAgency.bzStaffDesignation
        Dim objXml As New Xml.XmlDocument
        objXml = obj.List()
    End Sub

    Private Sub Button44_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button44.Click
        Try
            Dim obj As New bizTravelAgency.bzAgencyStaff
            Dim objXml As New Xml.XmlDocument
            Dim strInput As String = "<UP_GETAGENCYSTAFFDETAILS_INPUT><LCODE>3421</LCODE><OFFICEID></OFFICEID><EMPLOYEEID>24</EMPLOYEEID></UP_GETAGENCYSTAFFDETAILS_INPUT>"
            objXml.LoadXml(strInput)
            objXml = obj.GetCallerName(objXml)
        Catch ex As Exception

        End Try
    End Sub
End Class