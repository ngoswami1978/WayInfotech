Imports System.Math
Public Class FrmManoj

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim obj As New bizTravelAgency.bzOnlineStatus
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<MS_UPDATEONLINESTATUS_INPUT><Status Action='I' BC_ONLINE_CATG_ID='17' OnlineStatus='Puja Goel' StatusCode='Testab' SegExpected='' UnitCost='' /> </MS_UPDATEONLINESTATUS_INPUT>")
        '  objXmlInput.LoadXml("<MS_UPDATEONLINESTATUS_INPUT><Status Action='U' OnlineStatus='BOOKIT PRO' StatusCode='BIP' SegExpected='' UnitCost='10'  BC_ONLINE_CATG_ID=''/></MS_UPDATEONLINESTATUS_INPUT>")
        objXmlOutput = obj.Update(objXmlInput)
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim obj As New bizTravelAgency.bzOnlineStatus
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<MS_DELETEONLINESTATUS_INPUT><StatusCode>ACT</StatusCode></MS_DELETEONLINESTATUS_INPUT>")
        objXmlOutput = obj.Delete(objXmlInput)
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim obj As New bizTravelAgency.bzAgencyStaff
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        'objXmlInput.LoadXml("<MS_SEARCHONLINESTATUS_INPUT><StatusCode>act</StatusCode><OnlineStatus>acti</OnlineStatus></MS_SEARCHONLINESTATUS_INPUT>")
        objXmlInput.LoadXml("<TA_SEARCHSTAFF_INPUT><STAFFNAME>SHIV</STAFFNAME><AGENCYNAME>Pl Worldways Ltd.</AGENCYNAME><OFFICEID>DELPL324B</OFFICEID><EMPLOYEEID>24</EMPLOYEEID><PAGE_NO></PAGE_NO><PAGE_SIZE></PAGE_SIZE><SORT_BY></SORT_BY><DESC></DESC></TA_SEARCHSTAFF_INPUT>")


        objXmlOutput = obj.Search(objXmlInput)
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim obj As New bizTravelAgency.bzOnlineStatus
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<MS_VIEWONLINESTATUS_INPUT><StatusCode>BIP</StatusCode></MS_VIEWONLINESTATUS_INPUT>")
        objXmlOutput = obj.View(objXmlInput)
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim obj As New bizTravelAgency.bzOnlineStatus
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        'objXmlInput.LoadXml("<MS_UPDATEONLINESTATUS_INPUT><Status Action='I' OnlineStatus='Active123' SegExpected = '10' StatusCode='ACT' UnitCost = '1223.05'/></MS_UPDATEONLINESTATUS_INPUT>")
        objXmlInput.LoadXml("<MS_UPDATEONLINESTATUS_INPUT><Status Action='I' BC_ONLINE_CATG_ID='' OnlineStatus='Dialog wimax' StatusCode='Dwimax' SegExpected='' UnitCost='' /></MS_UPDATEONLINESTATUS_INPUT>")

        objXmlOutput = obj.Update(objXmlInput)
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim obj As New bizTravelAgency.bzAgencyPriority
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<MS_UPDATEAGENCYPRIORITY_INPUT><PRIORITY ACTION='I'  PRIORITYID='' PRIORITYNAME='High' PROTECTED='0' /></MS_UPDATEAGENCYPRIORITY_INPUT>")
        objXmlOutput = obj.Update(objXmlInput)
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim obj As New bizTravelAgency.bzAgencyPriority
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<MS_UPDATEAGENCYPRIORITY_INPUT><PRIORITY ACTION='U'  PRIORITYID='1' PRIORITYNAME='Super High' PROTECTED='0' /></MS_UPDATEAGENCYPRIORITY_INPUT>")
        objXmlOutput = obj.Update(objXmlInput)
    End Sub

    Private Sub Button6_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click, Button11.Click, Button13.Click
        'Dim obj As New bizTravelAgency.bzAgencyType
        'Dim objXmlInput As New Xml.XmlDocument
        'Dim objXmlOutput As New Xml.XmlDocument
        'objXmlOutput = obj.List()
        'Dim obj As New bizTravelAgency.bzAgency 'Product
        'Dim objXmlInput As New Xml.XmlDocument
        'Dim objXmlOutput As New Xml.XmlDocument
        ''objXmlInput.LoadXml("<MS_GETPCINSTALLATION_INPUT><LCODE>220</LCODE></MS_GETPCINSTALLATION_INPUT>")
        'objXmlInput.LoadXml("<MS_GETMISCINSTALLATION_INPUT><LCode>171</LCode></MS_GETMISCINSTALLATION_INPUT>")
        'objXmlOutput = obj.GetInstalledMiscHW(objXmlInput)

        'objXmlInput.LoadXml("<MS_GETPCINSTALLATION_INPUT><LCODE>171</LCODE></MS_GETPCINSTALLATION_INPUT>")
        'objXmlOutput = obj.GetInstalledPC(objXmlInput)

        'Dim obj As New bizTravelAgency.bzAgencyStaff 'Product
        'Dim objXmlInput As New Xml.XmlDocument
        'Dim objXmlOutput As New Xml.XmlDocument
        'objXmlInput.LoadXml("<TA_SEARCHSTAFF_INPUT><STAFFNAME>a</STAFFNAME><AGENCYNAME></AGENCYNAME></TA_SEARCHSTAFF_INPUT>")
        'objXmlOutput = obj.Search(objXmlInput)


        'Dim obj As New bizTravelAgency.bzAgencyProduct
        'Dim objXmlInput As New Xml.XmlDocument
        'Dim objXmlOutput As New Xml.XmlDocument
        'objXmlInput.LoadXml("<MS_GETAGENCYPRODUCT_INPUT><LCODE>9</LCODE></MS_GETAGENCYPRODUCT_INPUT>")
        'objXmlOutput = obj.GetDetails(objXmlInput)



        Dim obj As New bizTravelAgency.bzAgency
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        'objXmlInput.LoadXml("<RP_AGENCY_INPUT><AGENCY NAME='a' LOCATION_SHORT_NAME='' City_Name='' Country_Name='' StatusCode='' Aoffice='' OFFICEID='' Crs='' ADDRESS='' AgencyStatusId='' AgencyTypeId='' EMAIL='' DATE_ONLINE='' DATE_OFFLINE='' FAX='' FILENO='' IATA_TID='' EmployeeID='' Limited_To_Aoffice='' Limited_To_Region='' Limited_To_OwnAagency='' SecurityRegionID='' ></AGENCY></RP_AGENCY_INPUT>")
        objXmlInput.LoadXml("<RP_AGENCY_INPUT><AGENCY NAME='' LOCATION_SHORT_NAME='' City_Name='' Country_Name='' StatusCode='' Aoffice='' OFFICEID='' Crs='' ADDRESS='' AgencyStatusId='' AgencyTypeId='' EMAIL='' DATE_ONLINE='' DATE_OFFLINE='' FAX='' FILENO='' IATA_TID='' EmployeeID='24' Limited_To_Aoffice='' Limited_To_Region='' Limited_To_OwnAagency='0' SecurityRegionID='33' /></RP_AGENCY_INPUT>")
        objXmlOutput = obj.AgencyList(objXmlInput)


    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        'Dim obj As New bizTravelAgency.bzAgencyNotes

        Dim obj As New bizTravelAgency.bzAgencyType
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        'objXmlInput.LoadXml("<MS_UPDATEAGENCYNOTES_INPUT><AGENCYNOTES LCode='9619' EmployeeID='24' Notes='This is for testing purpose' /></MS_UPDATEAGENCYNOTES_INPUT>")
        'objXmlOutput = obj.Update(objXmlInput)
        objXmlInput.LoadXml("<MS_UPDATEAGENCYTYPE_INPUT><AgencyType ACTION='I' Agency_Type_Name='abc' AgencyTypeId='' AT_MAX_BDR_FAV='23' AT_MAX_CC_CALLS='43'></AgencyType></MS_UPDATEAGENCYTYPE_INPUT>")
        objXmlOutput = obj.Update(objXmlInput)

    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Dim obj As New bizTravelAgency.bzAgencyProduct
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        'objXmlInput.LoadXml("<MS_UPDATEAGENCYPRODUCT_INPUT><AGENCYPRODUCT Action='I' ROWID='' PRODUCTID='24' LCODE='51' TERMINALS_ONLINE='12' DATE_INSTALLATION='' /></MS_UPDATEAGENCYPRODUCT_INPUT>")
        objXmlInput.LoadXml("<MS_UPDATEAGENCYPRODUCT_INPUT><AGENCYPRODUCT TempRowCount='1' PRODUCTNAME='computer' Action='I' ROWID='' PRODUCTID='5' LCODE='20' TERMINALS_ONLINE='' DATE_INSTALLATION='20071205' /><AGENCYPRODUCT TempRowCount='2' PRODUCTNAME='KeyBord' Action='I' ROWID='' PRODUCTID='6' LCODE='20' TERMINALS_ONLINE='7' DATE_INSTALLATION='20071224' /><AGENCYPRODUCT TempRowCount='3' PRODUCTNAME='CPU' Action='I' ROWID='' PRODUCTID='7' LCODE='20' TERMINALS_ONLINE='89' DATE_INSTALLATION='20071221' /></MS_UPDATEAGENCYPRODUCT_INPUT>")
        objXmlOutput = obj.Update(objXmlInput)
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim obj As New bizTravelAgency.bzAgencyStaff
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        'objXmlInput.LoadXml("<MS_UPDATEAGENCYPRODUCT_INPUT><AGENCYPRODUCT Action='I' ROWID='' PRODUCTID='24' LCODE='51' TERMINALS_ONLINE='12' DATE_INSTALLATION='' /></MS_UPDATEAGENCYPRODUCT_INPUT>")
        objXmlInput.LoadXml("<TA_SEARCHSTAFF_INPUT><STAFFNAME></STAFFNAME><AGENCYNAME></AGENCYNAME></TA_SEARCHSTAFF_INPUT>")
        objXmlOutput = obj.Search(objXmlInput)
    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        Dim obj As New bizTravelAgency.bzMiscInstallation
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        'objXmlInput.LoadXml("<TA_UPDATEMISCINSTALLATION_INPUT><MISCINSTALLATION ACTION='U' ROWID='133128' LCODE='2' DATE='20080428' DATEDE='20080428' EQUIPMENTTYPE='30' EQUIPMENTNUMBER='30' QTY='34' ORDERNUMBER='' ORDERNUMBERDE='' CHALLANNO='34' LOGGEDBY='24' /></TA_UPDATEMISCINSTALLATION_INPUT>")
        'objXmlInput.LoadXml("<TA_UPDATEMISCINSTALLATION_INPUT><MISCINSTALLATION ACTION='X' ROWID='133125' LCODE='2' DATE='20080429' DATEDE='20080429' EQUIPMENTTYPE='67' EQUIPMENTNUMBER='67' QTY='12' ORDERNUMBER='' ORDERNUMBERDE='' CHALLANNO='546745' LOGGEDBY='24' /></TA_UPDATEMISCINSTALLATION_INPUT>")
        objXmlInput.LoadXml("<TA_UPDATEMISCINSTALLATION_INPUT><MISCINSTALLATION ACTION='X' ROWID='133137' LCODE='2' DATE='20080430' DATEDE='20080430' EQUIPMENTTYPE='LBL' EQUIPMENTNUMBER='40006' QTY='5' ORDERNUMBER='' ORDERNUMBERDE='' CHALLANNO='600' LOGGEDBY='24' /></TA_UPDATEMISCINSTALLATION_INPUT>")
        objXmlOutput = obj.Update(objXmlInput)
    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        Dim obj As New bizTravelAgency.bzMiscInstallation
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        'objXmlInput.LoadXml("<MS_UPDATEAGENCYPRODUCT_INPUT><AGENCYPRODUCT Action='I' ROWID='' PRODUCTID='24' LCODE='51' TERMINALS_ONLINE='12' DATE_INSTALLATION='' /></MS_UPDATEAGENCYPRODUCT_INPUT>")
        objXmlInput.LoadXml("<MS_GETMISCINSTALLATION_INPUT><LCode>1251</LCode></MS_GETMISCINSTALLATION_INPUT>")
        objXmlOutput = obj.GetInstalledMiscHW(objXmlInput)
    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Dim obj As New bizTravelAgency.bzMiscInstallation
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        'objXmlInput.LoadXml("<MS_UPDATEAGENCYPRODUCT_INPUT><AGENCYPRODUCT Action='I' ROWID='' PRODUCTID='24' LCODE='51' TERMINALS_ONLINE='12' DATE_INSTALLATION='' /></MS_UPDATEAGENCYPRODUCT_INPUT>")
        objXmlInput.LoadXml("<TA_DELETEMISCINSTALLATION_INPUT><ROWID>133120</ROWID></TA_DELETEMISCINSTALLATION_INPUT>")
        objXmlOutput = obj.Delete(objXmlInput)

    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        Dim obj As New bizTravelAgency.bzMiscInstallation
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        'objXmlInput.LoadXml("<MS_UPDATEAGENCYPRODUCT_INPUT><AGENCYPRODUCT Action='I' ROWID='' PRODUCTID='24' LCODE='51' TERMINALS_ONLINE='12' DATE_INSTALLATION='' /></MS_UPDATEAGENCYPRODUCT_INPUT>")
        objXmlInput.LoadXml("<TA_VIEWMISCINSTALLATION_INPUT><ROWID>2738</ROWID></TA_VIEWMISCINSTALLATION_INPUT>")
        objXmlOutput = obj.View(objXmlInput)
    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        Dim obj As New bizTravelAgency.bzMiscInstallation
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        'objXmlInput.LoadXml("<MS_UPDATEAGENCYPRODUCT_INPUT><AGENCYPRODUCT Action='I' ROWID='' PRODUCTID='24' LCODE='51' TERMINALS_ONLINE='12' DATE_INSTALLATION='' /></MS_UPDATEAGENCYPRODUCT_INPUT>")
        objXmlInput.LoadXml("<TA_HISTORYMISCINSTALLATION_INPUT><ROWID>2738</ROWID></TA_HISTORYMISCINSTALLATION_INPUT>")
        objXmlOutput = obj.GetMiscInstallationHistory(objXmlInput)
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Dim obj As New bizTravelAgency.bzAgencyTarget
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<PR_AGENCYEMPLIST_INPUT><City_Id>115</City_Id></PR_AGENCYEMPLIST_INPUT>")
        objXmlOutput = obj.GetAgencyEmpList(objXmlInput)
    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        Dim obj As New bizTravelAgency.bzAgencyTarget
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<TA_AGENCYTARGET_INPUT><City_Id></City_Id><CityName>Agra</CityName><Year>2007</Year><Month>4</Month><SalesManNameId></SalesManNameId><RESP_1A></RESP_1A><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency></TA_AGENCYTARGET_INPUT>")

        '@CityName='Agra'                                                                                                ,
        '@YEAR=2009,
        '@MONTH=2,
        '@SALESPERSON=NULL,
        '@RESP_1A=NULL,
        '@LIMITED_TO_AOFFICE=NULL,
        '@LIMITED_TO_REGION=null,
        '@LIMITED_TO_OWNAAGENCY=NULL,
        '@EmployeeID=24



'<TA_AGENCYTARGET_INPUT>
 ' <CityName>New Delhi</CityName> 
 ' <City_Id>187</City_Id> 
 ' <Year>2007</Year> 
 ' <RESP_1A>24</RESP_1A> 
 ' <Month>2</Month> 
 ' <SalesManNameId /> 
 ' <Increment>0</Increment> 
 ' <Decrement>0</Decrement> 
 ' <TargetValuePer>0</TargetValuePer> 
 ' <Limited_To_Aoffice /> 
 ' <Limited_To_Region /> 
 ' <Limited_To_OwnAagency /> 
 ' </TA_AGENCYTARGET_INPUT>


        objXmlOutput = obj.Search(objXmlInput)
    End Sub

    Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click
        Dim obj As New bizTravelAgency.bzAgencyTarget
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument

        'old update xml input
        'Dim str_INPUT As String = "<TA_UPDATEAGENCYTARGET_INPUT>"
        'str_INPUT = str_INPUT + "<Target Action='I'  LCode='26652' Year='2007'  Month='2' Target='1000'   SalesPersonId='24'/> "
        'str_INPUT = str_INPUT + "<Target Action='I'  LCode='5387'  Year='2007'  Month='2' Target='1500'   SalesPersonId='24'/> "
        'str_INPUT = str_INPUT + "<Target Action='I' LCode='10982'  Year='2007'  Month='2' Target='6770'   SalesPersonId='24' />"
        'str_INPUT = str_INPUT + "<Target Action='I' LCode='13430'  Year='2007'  Month='2' Target='250'    SalesPersonId='24' />"
        'str_INPUT = str_INPUT + "<Target Action='I' LCode='2535'   Year='2007'  Month='2' Target='2500'   SalesPersonId='24' />"
        'str_INPUT = str_INPUT + "<Target Action='I' LCode='21187'  Year='2007'  Month='2' Target='500'    SalesPersonId='24' />"
        'str_INPUT = str_INPUT + "<Target Action='I' LCode='24922'  Year='2007'  Month='2' Target='250'    SalesPersonId='24' />"
        'str_INPUT = str_INPUT + "<Target Action='I' LCode='25312'  Year='2007'  Month='2' Target='250'    SalesPersonId='24' />"
        'str_INPUT = str_INPUT + "</TA_UPDATEAGENCYTARGET_INPUT>"
        'objXmlInput.LoadXml(str_INPUT)
        'objXmlOutput = obj.Update(objXmlInput)

        Dim str_INPUT As String = "<TA_AGENCYTARGET_OUTPUT><Total>0</Total><Errors Status='FALSE'><Error Code='' Description=''/></Errors> " & _
        "<TARGET LCode='35483' Year='2010' Month='2' AgencyName='testabhi' OfficeId='KTMVS3847' Address='a/22def' SalesPersonId='' SalesManName='' LoginId='' Target='0'/>" & _
        "<TARGET LCode='2876' Year='2010' Month='2' AgencyName='Travel Corporation (India) Pvt Ltd' OfficeId='AGRTC3121' Address='54 Taj Rd  Htl Clarks Shirazs' SalesPersonId='202713' SalesManName='Shailendra Rawat' LoginId='' Target='120'/>" & _
        "<TARGET LCode='23029' Year='2010' Month='2' AgencyName='Travel Corporation Of India' OfficeId='' Address='Opp Hari Parvat StationHariparvat' SalesPersonId='202713' SalesManName='Shailendra Rawat' LoginId='' Target='10'/>" & _
        "<TARGET LCode='29029' Year='2010' Month='2' AgencyName='Cox And Kings   Agra' OfficeId='' Address='O P Impex Intl 1st Flr 5 Awagarh' SalesPersonId='202713' SalesManName='Shailendra Rawat' LoginId='' Target='1500'/>" & _
        "<TARGET LCode='5218' Year='2010' Month='2' AgencyName='Mercury Travels' OfficeId='' Address='54, Taj Road' SalesPersonId='202713' SalesManName='Shailendra Rawat' LoginId='' Target='1200'/>" & _
        "<TARGET LCode='34717' Year='2010' Month='2' AgencyName='International Travel Service' OfficeId='AGRVS3163' Address='Subhash Nagar Guhla Road Cheeka,Teh. Guhla,Distt. Kaithal (Haryana)' SalesPersonId='203008' SalesManName='garima Sharma' LoginId='' Target='1300'/>" & _
        "<TARGET LCode='12078' Year='2010' Month='2' AgencyName='International Travel House' OfficeId='AGRTH3101' Address='Welcom Group Mughal SheratonTaj Ganj' SalesPersonId='202713' SalesManName='Shailendra Rawat' LoginId='' Target='0'/>" & _
        "<TARGET LCode='2661' Year='2010' Month='2' AgencyName='Pleasure Tours' OfficeId='AGRVS3115' Address='507 The Mall' SalesPersonId='202713' SalesManName='Shailendra Rawat' LoginId='' Target='0'/>" & _
        "<TARGET LCode='20265' Year='2010' Month='2' AgencyName='A B I Travel Organizers' OfficeId='' Address='9-10 Suresh Plaza, Opp Sanjay  PlaceM.G Road' SalesPersonId='202713' SalesManName='Shailendra Rawat' LoginId='' Target='15000'/>" & _
        "<TARGET LCode='22318' Year='2010' Month='2' AgencyName='Prime Travels' OfficeId='' Address='Plot No 26 Sector 23 Transport N' SalesPersonId='202713' SalesManName='Shailendra Rawat' LoginId='' Target='0'/>" & _
        "<TARGET LCode='5226' Year='2010' Month='2' AgencyName='Jet Air Ltd' OfficeId='' Address='54, Tai Road,Hotel Clark Shiraj' SalesPersonId='202713' SalesManName='Shailendra Rawat' LoginId='' Target='0'/>" & _
        "<TARGET LCode='2' Year='2010' Month='2' AgencyName='Bird Travels Pvt Ltd' OfficeId='AGRFF2120' Address='Hotel Clarks Shiraj54,Taj_road' SalesPersonId='202713' SalesManName='Shailendra Rawat' LoginId='' Target='0'/>" & _
        "<TARGET LCode='30203' Year='2010' Month='2' AgencyName='Raj Travel Holidays' OfficeId='AGRVS3153'  Address='Hotel Clarks Shiral, 54 Taj Road,Agra' SalesPersonId='202713' SalesManName='Shailendra Rawat' LoginId='' Target='0'/>" & _
        "<TARGET LCode='18340' Year='2010' Month='2' AgencyName='Shikhar Travels' OfficeId='' Address='Welcome Hotel, Mughal Sheraton' SalesPersonId='202713' SalesManName='Shailendra Rawat' LoginId='' Target='0'/>" & _
        "</TA_AGENCYTARGET_OUTPUT>"

        objXmlInput.LoadXml(str_INPUT)
        objXmlOutput = obj.Update(objXmlInput)

'<TA_AGENCYTARGET_OUTPUT>
'<TARGET LCode='7890' Year='' Month='' AgencyName='Sushrut Travels' OfficeId='AKDVS3101' Address='Chaudhary Hospital CompoundCivil Lines' SalesPersonId='202742' SalesManName='Aniruddha Brahma' LoginId=''' Target='' /> 
'</TA_AGENCYTARGET_OUTPUT>


        '<TA_AGENCYTARGET_OUTPUT>
        '<TARGET LCode='26825' Year='2008' Month='7' AgencyName='Eternal Glories' OfficeId='AGRVS3141' Address='408, Shubham Appartments, Halwai Ki BagichiAgra (Up)-282002' SalesPersonId='24' SalesManName='' Target='55' /> 
        '<TARGET LCode='2' Year='2008' Month='7' AgencyName='Bird Travels Pvt Ltd' OfficeId='AGRFF2120' Address='Hotel Clarks Shiraj54,Taj_road' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='567' /> 
        '<TARGET LCode='5218' Year='2008' Month='7' AgencyName='Mercury Travels' OfficeId='' Address='54, Taj Road' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='569' /> 
        '<TARGET LCode='5225' Year='2008' Month='7' AgencyName='Touraids (I) Travel Services' OfficeId='AGRVS3106' Address='A-17 ,Shopping ArcadeSadar Bazar' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='572' /> 
        '<TARGET LCode='5226' Year='2008' Month='7' AgencyName='Jet Air Ltd' OfficeId='AGRVS3119' Address='54, Tai Road,Hotel Clark Shiraj' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1026' /> 
        '<TARGET LCode='6944' Year='2008' Month='7' AgencyName='Alpine Travels  Tours' OfficeId='AGRVS3111' Address='Holi Gate Crossing,Kotwali RoadMadhura(U.P) - 281001' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1026' /> 
        '<TARGET LCode='6946' Year='2008' Month='7' AgencyName='Adwings' OfficeId='AGRVS3102' Address='42 2 G1 Hari World CommercialSanjay Place,' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1080' /> 
        '<TARGET LCode='8266' Year='2008' Month='7' AgencyName='Taj International Travels' OfficeId='AGRVS3105' Address='178, Karim Commercial ComplexFatehabad Road' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1085' /> 
        '<TARGET LCode='8925' Year='2008' Month='7' AgencyName='Outbounds' OfficeId='' Address='31/173, Hari Nagar82,Shamshabad Road' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1085' /> 
        '<TARGET LCode='10857' Year='2008' Month='7' AgencyName='Malik Express Travel Enterprises' OfficeId='AGRVS3109' Address='Home 9-japan House, Marris RoadSamad Road, Lic Building,Aligarh' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1085' /> 
        '<TARGET LCode='11015' Year='2008' Month='7' AgencyName='Nirvana Tours India' OfficeId='AGRVS3114' Address='50, A Taj RoadSadar Bazaar, Agra Cantt' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1085' /> 
        '<TARGET LCode='12078' Year='2008' Month='7' AgencyName='International Travel House' OfficeId='AGRTH3101' Address='Welcom Group Mughal SheratonTaj Ganj' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1085' /> 
        '<TARGET LCode='12287' Year='2008' Month='7' AgencyName='Amadeus India Pvt Ltd' OfficeId='AGR1A0980' Address='Hotel Clarks Shiraj' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1085' /> 
        '<TARGET LCode='12369' Year='2008' Month='7' AgencyName='Lovely Tours  Travels Pvt Ltd' OfficeId='AGRVS3104' Address='Block 51/1-5, Anupam Plaza, Ii Phase,1st Floor, Sanjay Place' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1085' /> 
        '<TARGET LCode='12451' Year='2008' Month='7' AgencyName='Alpha Travel Corporation' OfficeId='' Address='F-1, Akhilesh TowerHari Parvat' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1085' /> 
        '<TARGET LCode='12628' Year='2008' Month='7' AgencyName='Radha Travels' OfficeId='AGRVS3108' Address='Agarwal MarketLoi Bazar.Vrindaban' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1085' /> 
        '<TARGET LCode='14066' Year='2008' Month='7' AgencyName='Gopal Travels' OfficeId='AGRVS3112' Address='149,Vikas Bazar.Mathura' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1085' /> 
        '<TARGET LCode='14076' Year='2008' Month='7' AgencyName='Hare Krsna C/o. Vishwa Travels P Ltd' OfficeId='AGRVS3113' Address='Shop No. 1, 248 Iskon Gurukul Bldg,Raman Reti Vrindavan (Mathura)' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1085' /> 
        '<TARGET LCode='15249' Year='2008' Month='7' AgencyName='Cal India Tour  Travels' OfficeId='AGRVS3116' Address='4/395 First FloorGwalior Road' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1085' /> 
        '<TARGET LCode='2661' Year='2008' Month='7' AgencyName='Pleasure Tours' OfficeId='AGRVS3115' Address='507 The Mall' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1085' /> 
        '<TARGET LCode='2739' Year='2008' Month='7' AgencyName='Sita World Travel' OfficeId='' Address='A-2 Shopping ArcadeSadar Bazar' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1085' /> 
        '<TARGET LCode='2876' Year='2008' Month='7' AgencyName='Travel Corporation (India) Pvt Ltd' OfficeId='AGRTC3121' Address='54 Taj Rd Htl Clarks Shirazs' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1085' /> 
        '<TARGET LCode='16829' Year='2008' Month='7' AgencyName='See India Travel' OfficeId='AGRVS3117' Address='1st Floor, Vimal TowerNear Holiday Inn, Sanjay Place' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1085' /> 
        '<TARGET LCode='18277' Year='2008' Month='7' AgencyName='Naveen International' OfficeId='' Address='Near Kedia Guest House Bawari Ga' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1085' /> 
        '<TARGET LCode='18340' Year='2008' Month='7' AgencyName='Shikhar Travels' OfficeId='' Address='Welcome Hotel, Mughal Sheraton' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1085' /> 
        '<TARGET LCode='20265' Year='2008' Month='7' AgencyName='A B I Travel Organizers' OfficeId='AGRVS3122' Address='9-10 Suresh Plaza, Opp Sanjay PlaceM.G Road' SalesPersonId='12' SalesManName='Ankur Bhatia' Target='1085' /> 
        '<TARGET LCode='21532' Year='2008' Month='7' AgencyName='Select Vacations' OfficeId='AGRVS3123' Address='T.S.Sehra Building, Abc Crossing, Bansal NgrFatehabad Rd' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1085' /> 
        '<TARGET LCode='22318' Year='2008' Month='7' AgencyName='Prime Travels' OfficeId='' Address='Plot No 26 Sector 23 Transport N' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1085' /> 
        '<TARGET LCode='23029' Year='2008' Month='7' AgencyName='Travel Corporation Of India' OfficeId='AGRTC3129' Address='Opp Hari Parvat StationHariparvat' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1085' /> 
        '<TARGET LCode='25085' Year='2008' Month='7' AgencyName='Peak Tours  Travels' OfficeId='' Address='12-13 Ugf Shree Heera Plaza Bye' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1085' /> 
        '<TARGET LCode='25931' Year='2008' Month='7' AgencyName='Trail Blazer Tours India Pvt Ltd' OfficeId='AGRVS3137' Address='C/o Travel Bureau, Nr Hotel Taj View,Fatehabad Road' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1085' /> 
        '<TARGET LCode='26013' Year='2008' Month='7' AgencyName='Indus Holidays  Tours (I) Pvt Ltd' OfficeId='AGRVS3138' Address='Lgf-3, Nalanda Complex, Nr. Chhama EnclaveMaruti Estate, Agra' SalesPersonId='' SalesManName='' Target='1085' /> 
        '<TARGET LCode='26240' Year='2008' Month='7' AgencyName='Trail Blazer Tours I Pvt' OfficeId='' Address='2 Crown Towers Opp Hotel Itc Mug' SalesPersonId='8011' SalesManName='Abhishek Bhattacharya' Target='1085' /> 
        '<TARGET LCode='26286' Year='2008' Month='7' AgencyName='Travelex Tours  Travel' OfficeId='' Address='Bl-14, Rashmi Palace, Bye Pass RoadKamala Nagar, Agra' SalesPersonId='' SalesManName='' Target='1174' /> 
        '</TA_AGENCYTARGET_OUTPUT>



    End Sub
    Private Sub Button24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button24.Click

        Dim obj As New bizTravelAgency.bzAgencyTarget
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        'exec [UP_SER_TA_SALESMAN_TARGET] NULL, 2007, 2, NULL , 145
        objXmlInput.LoadXml("<TA_HISTORY_AGENCYTARGET_INPUT><TARGET LCode='12078' Year='2008' Month='7' SalesPersonId='8011' Resp_1a='24'  PAGE_NO='1' PAGE_SIZE='25' SORT_BY='ChangedData' DESC='FALSE' /></TA_HISTORY_AGENCYTARGET_INPUT>")

'TARGET Action="" LCode="21187" Year="2008" Month="7" SalesPersonId="8484" Resp_1a="24" PAGE_NO="1" PAGE_SIZE="25" SORT_BY="EmpName" DESC="FALSE"
        'objXmlInput.LoadXml("<TA_HISTORY_AGENCYTARGET_INPUT><TARGET Action='' LCode='2535' Year='2007' Month='2' SalesPersonId='24'  /></TA_HISTORY_AGENCYTARGET_INPUT>")
        objXmlOutput = obj.History(objXmlInput)


        '        str_INPUT = str_INPUT + "<TARGET LCode='10982' Year='2008' Month='7' AgencyName='M Iqbal Travel Agent' OfficeId='' Address='B,24 Falshrutinagar,Station Road' SalesPersonId='' SalesManName='' Target='170' /> "
        '       str_INPUT = str_INPUT + "<TARGET LCode='2535' Year='2008' Month='7' AgencyName='M Iqbal Travel Agent' OfficeId='' Address='B,24 Falshrutinagar,Station Road' SalesPersonId='' SalesManName='' Target='180' /> "



    End Sub

    Private Sub Button25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button25.Click
        Dim obj As New bizTravelAgency.bzAgencyTarget
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument

        objXmlInput.LoadXml("<TA_AGENCYTARGET_PREV_MONTH_INPUT><City_Id></City_Id><CityName>New Delhi</CityName><PYear>2008</PYear><PMonth>7</PMonth><Year>2008</Year><Month>7</Month><SalesManNameId></SalesManNameId><RESP_1A></RESP_1A><Increment>1</Increment><Decrement>1</Decrement><TargetValuePer>10</TargetValuePer><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency></TA_AGENCYTARGET_PREV_MONTH_INPUT>")

        'Dim str_INPUT As String = "<TA_AGENCYTARGET_OUTPUT>"
        'str_INPUT = str_INPUT + " <TARGET LCode='2'   Year='2008' Month='4' AgencyName='Bird Travels Pvt Ltd' OfficeId='AGRFF2120' Address='Hotel Clarks Shiraj54,Taj_road' SalesPersonId='202713' SalesManName='Shailendra Rawat' LoginId='' Target='0' />"
        'str_INPUT = str_INPUT + "<TARGET LCode='2661' Year='2008' Month='4' AgencyName='Pleasure Tours' OfficeId='AGRVS3115' Address='507 The Mall' SalesPersonId='202713' SalesManName='Shailendra Rawat' LoginId='' Target='0' /> "
        'str_INPUT = str_INPUT + "<TARGET LCode='2739' Year='2008' Month='4' AgencyName='Sita World Travel' OfficeId='' Address='A-2 Shopping ArcadeSadar Bazar' SalesPersonId='202713' SalesManName='Shailendra Rawat' LoginId='' Target='0' /> "
        'str_INPUT = str_INPUT + "<TARGET LCode='2876' Year='2008' Month='4' AgencyName='Travel Corporation (India) Pvt Ltd' OfficeId='AGRTC3121' Address='54 Taj Rd Htl Clarks Shirazs' SalesPersonId='202713' SalesManName='Shailendra Rawat' LoginId='' Target='0' /> "
        'str_INPUT = str_INPUT + "<TARGET LCode='5218' Year='2008' Month='4' AgencyName='Mercury Travels' OfficeId='' Address='54, Taj Road' SalesPersonId='202713' SalesManName='Shailendra Rawat' LoginId='' Target='0' /> "
        'str_INPUT = str_INPUT + "<TARGET LCode='5225' Year='2008' Month='4' AgencyName='Touraids (I) Travel Services' OfficeId='AGRVS3106' Address='A-17 ,Shopping ArcadeSadar Bazar' SalesPersonId='202713' SalesManName='Shailendra Rawat' LoginId='' Target='0' /> "
        'str_INPUT = str_INPUT + "<TARGET LCode='5226' Year='2008' Month='4' AgencyName='Jet Air Ltd' OfficeId='AGRVS3119' Address='54, Tai Road,Hotel Clark Shiraj' SalesPersonId='202713' SalesManName='Shailendra Rawat' LoginId='' Target='0' /> "
        'str_INPUT = str_INPUT + "<TARGET LCode='6944' Year='2008' Month='4' AgencyName='Alpine Travels  Tours' OfficeId='AGRVS3111' Address='Holi Gate Crossing,Kotwali RoadMadhura(U.P) - 281001' SalesPersonId='202713' SalesManName='Shailendra Rawat' LoginId='' Target='0' /> "
        'str_INPUT = str_INPUT + "<TARGET LCode='6946' Year='2008' Month='4' AgencyName='Adwings' OfficeId='AGRVS3102' Address='42 2 G1 Hari World CommercialSanjay Place,' SalesPersonId='202713' SalesManName='Shailendra Rawat' LoginId='' Target='0' /> "
        'str_INPUT = str_INPUT + "<TARGET LCode='8266' Year='2008' Month='4' AgencyName='Taj International Travels' OfficeId='AGRVS3105' Address='178, Karim Commercial ComplexFatehabad Road' SalesPersonId='202713' SalesManName='Shailendra Rawat' LoginId='' Target='0' /> "
        'str_INPUT = str_INPUT + "<TARGET LCode='8925' Year='2008' Month='4' AgencyName='Outbounds' OfficeId='' Address='31/173, Hari Nagar82,Shamshabad Road' SalesPersonId='202713' SalesManName='Shailendra Rawat' LoginId='' Target='0' /> "
        'str_INPUT = str_INPUT + "</TA_AGENCYTARGET_OUTPUT>"
        'objXmlInput.LoadXml(str_INPUT)

        obj.Search_Prev_Target(objXmlInput)

    End Sub

    Private Sub Button26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button26.Click
        '    Dim X As Integer
        '    X = Round(900 + (900 * Val(3)) / 100)

        '    'GridArray.value(lngCounter, 8) = Round(lngTarget + (lngTarget * Val(txtPercent.Text)) / 100)
        '    'lngTotalTarget = lngTotalTarget + GridArray.value(lngCounter, 8)


        '    MsgBox(X)

    End Sub

    Private Sub Button27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button27.Click

    
        Dim obj As New bizTravelAgency.bzAgencyTarget
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument

         Dim str_INPUT As String = "<TA_AGENCYTARGET_PREV_MONTH_OUTPUT>"
             str_INPUT = str_INPUT + "<TARGET LCode='20981' Year='2008' Month='8' AgencyName='Tanvi Travels' OfficeId='RPRVS3118' Address='Opp Bukhari Petrol Pump, Link Road,Bilaspur, Chattisgarh' SalesPersonId='202726' SalesManName='Neeraj Rathod' LoginId='' Target='0' /> "
             str_INPUT = str_INPUT + "<TARGET LCode='22375' Year='2008' Month='8' AgencyName='New Era Holiday' OfficeId='PABVS3101' Address='Near Hanuman Temple, Bus Stand Square,Bilaspur (Cg)' SalesPersonId='202726' SalesManName='Neeraj Rathod' LoginId='' Target='0' /> "
             str_INPUT = str_INPUT + "<TARGET LCode='23920' Year='2008' Month='8' AgencyName='Vyas Travels' OfficeId='IDRVS3175' Address='First Floor, Rajeev Plaza,Bilaspur' SalesPersonId='202726' SalesManName='Neeraj Rathod' LoginId='' Target='0' /> "
             str_INPUT = str_INPUT + "<CHANGE Increment='1' Decrement='0' TargetValuePer='10' CityName='Bilaspur' /> "
            str_INPUT = str_INPUT + "</TA_AGENCYTARGET_PREV_MONTH_OUTPUT>"
            objXmlInput.LoadXml(str_INPUT)
            objXmlOutput = obj.Search_Agency_Target(objXmlInput)

'             str_INPUT = str_INPUT + "  <TARGET LCode='13430' Year='2008' Month='6' AgencyName='Indo Overseas Mgmnt Serv Pvt Ltd' OfficeId='BDQVS3134' Address='F 6/7,Shree Narmada Arcade,Old N H No.8Nr S A Motor,Gidc Estate,Ankleshwar(E)' SalesPersonId='8484' SalesManName='Saurabh Shah' LoginId='' Target='0' /> "
 '            str_INPUT = str_INPUT + " <TARGET LCode='21187' Year='2008' Month='6' AgencyName='Ami Tours And Travels' OfficeId='BDQVS3223' Address='42,Golden Plaza,Opp Telephone Exchange' SalesPersonId='8484' SalesManName='Saurabh Shah' LoginId='' Target='0' /> "
 '            str_INPUT = str_INPUT + "  <TARGET LCode='24922' Year='2008' Month='6' AgencyName='Falah Forex Pvt Ltd' OfficeId='BDQVS3254' Address='36/ 1st Floor,Sheth Complex MohmedpuraBharuch' SalesPersonId='8484' SalesManName='Saurabh Shah' LoginId='' Target='0' /> "
 '            str_INPUT = str_INPUT + " <TARGET LCode='25312' Year='2008' Month='6' AgencyName='Citizen Travel Agency' OfficeId='BDQVS3258' Address='Tekri. St. Atq,P.O. Kosamdi, Ta-ankieshwarBharuch' SalesPersonId='8484' SalesManName='Saurabh Shah' LoginId='' Target='0' /> "
 '            str_INPUT = str_INPUT + "  <TARGET LCode='26652' Year='2008' Month='6' AgencyName='Prominent Tours And Travels Pvt Ltd' OfficeId='BDQVS3273' Address='110, First Floor, Vadilonu Ghar,Kasak Circle, Bharuch' SalesPersonId='8484' SalesManName='Saurabh Shah' LoginId='' Target='0' /> "
 '            str_INPUT = str_INPUT + "<CHANGE Increment='0' Decrement='1' TargetValuePer='50' CityName='Bharuch' /> "
 '            str_INPUT = str_INPUT + "</TA_AGENCYTARGET_PREV_MONTH_OUTPUT>"
    End Sub
    
    Private Sub Button28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button28.Click
        Dim obj As New bizTravelAgency.bzAgencyTarget
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        Dim str_INPUT As String = "<TA_AGENCYTARGET_OUTPUT>"
        str_INPUT = str_INPUT + "<Errors Status='FALSE'>"
        str_INPUT = str_INPUT + "<Error Code='' Description='' />"
        str_INPUT = str_INPUT + "</Errors>"
        str_INPUT = str_INPUT + "<Total>0</Total>"

        str_INPUT = str_INPUT + "<TARGET LCode='29232' Year='2009' Month='1' AgencyName='test344' OfficeId='' Address='ytutyytu' SalesPersonId='' SalesManName='' LoginId='' Target='200'/>"
        str_INPUT = str_INPUT + "<TARGET LCode='16057' Year='2009' Month='1' AgencyName='Fcm_travel_solutions(India)Ltd' OfficeId='BLRFG3481' Address='Opp Indian Embassy, Interior Ministry RoadKabul- Afghanisthan' SalesPersonId='202710' SalesManName='Vani' LoginId='' Target='100'/>"
        str_INPUT = str_INPUT + "<TARGET LCode='29233' Year='2009' Month='1' AgencyName='testr' OfficeId='' Address='12334' SalesPersonId='' SalesManName='' LoginId='' Target='300'/>"
        str_INPUT = str_INPUT + "</TA_AGENCYTARGET_OUTPUT>"
        objXmlInput.LoadXml(str_INPUT)

        objXmlOutput = obj.Search_Agency_target1(objXmlInput)
    End Sub

    Private Sub Button29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button29.Click
   
        '<MS_SEARCHONLINESTATUS_OUTPUT> 
        '  <PAGE PAGE_COUNT="3" TOTAL_ROWS="51" /> 
        '    <Errors Status="FALSE">
        '  <Error Code="" Description="" /> 
        '  </Errors>
        '  <Status OnlineStatus="1A RES" StatusCode="1A RES" SegExpected ="" /> 
        '  <Status OnlineStatus="BT OFF ID ON MMDS" StatusCode="MMD O" SegExpected =""  /> 
        '  </MS_SEARCHONLINESTATUS_OUTPUT>

        Dim obj As New bizTravelAgency.bzOnlineStatus
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        Dim str_INPUT As String = "<MS_SEARCHONLINESTATUS_INPUT><OnlineStatus />   <StatusCode />   <SegExpected />   <PAGE_NO>1</PAGE_NO>   <PAGE_SIZE>100</PAGE_SIZE>   <SORT_BY>StatusCode</SORT_BY>   <DESC>TRUE</DESC>   </MS_SEARCHONLINESTATUS_INPUT>"
        'Dim str_INPUT As String = "<MS_SEARCHONLINESTATUS_INPUT>"
        'str_INPUT = str_INPUT + "<OnlineStatus/><StatusCode>RLL H</StatusCode><SegExpected/><PAGE_NO>1</PAGE_NO><PAGE_SIZE>2</PAGE_SIZE><SORT_BY>StatusCode</SORT_BY><DESC>FALSE</DESC>"
        'str_INPUT = str_INPUT + "</MS_SEARCHONLINESTATUS_INPUT>"
        objXmlInput.LoadXml(str_INPUT)
        objXmlOutput = obj.Search(objXmlInput)
    End Sub

    'Private Sub Button30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button30.Click

    '    '<MS_VIEWONLINESTATUS_INPUT><StatusCode/></MS_VIEWONLINESTATUS_INPUT>

    '    Dim obj As New bizTravelAgency.bzOnlineStatus
    '    Dim objXmlInput As New Xml.XmlDocument
    '    Dim objXmlOutput As New Xml.XmlDocument
    '    Dim str_INPUT As String = "<MS_VIEWONLINESTATUS_INPUT><StatusCode>RLL H</StatusCode></MS_VIEWONLINESTATUS_INPUT>"
    '    objXmlInput.LoadXml(str_INPUT)
    '    objXmlOutput = obj.View(objXmlInput)


    '    '<MS_VIEWONLINESTATUS_OUTPUT>
    '    '  <Status OnlineStatus="BOOKIT PRO" StatusCode="BIP"   SegExpected="" /> 
    '    '   <Errors Status="FALSE">
    '    '  <Error Code="" Description="" /> 
    '    '  </Errors>
    '    '  </MS_VIEWONLINESTATUS_OUTPUT>





    'End Sub

    'Private Sub Button31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button31.Click

    '    Dim obj As New bizTravelAgency.bzOnlineStatus
    '    Dim objXmlInput As New Xml.XmlDocument
    '    Dim objXmlOutput As New Xml.XmlDocument
    '    Dim str_INPUT As String = "<MS_UPDATEONLINESTATUS_INPUT><Status Action='U' OnlineStatus='BSNL LL' StatusCode='BT BSL' SegExpected='500' /></MS_UPDATEONLINESTATUS_INPUT>"

    '    objXmlInput.LoadXml(str_INPUT)
    '    objXmlOutput = obj.Update(objXmlInput)

    'End Sub

    Private Sub Button30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button30.Click
        Dim obj As New bizTravelAgency.bzPCInstallation
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        Dim str_INPUT As String = "<GET_PTYPEFOTTER_INPUT><EMPLOYEEID>169</EMPLOYEEID></GET_PTYPEFOTTER_INPUT>"
        objXmlInput.LoadXml(str_INPUT)
        objXmlOutput = obj.GetPTypeFooterDetails(objXmlInput)

    End Sub

    Private Sub Button31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button31.Click
        Dim obj As New bizTravelAgency.bzAgency1ARespChange
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument

        'Dim str_INPUT As String = "<TA_SEARCHAGENCY_1A_INPUT><ACTION>S</ACTION><LOCATION_CODE></LOCATION_CODE><NAME>Cox & Kings (India) Lmited</NAME><CHAIN_CODE>168</CHAIN_CODE><GROUP_NAME /><OFFICEID>BOMCX2355</OFFICEID><CITY_NAME>Mumbai</CITY_NAME><COUNTRY_NAME>India</COUNTRY_NAME><ONLINE_STATUS>BTLLV</ONLINE_STATUS><AOFFICE>BOM</AOFFICE><CRS>1W</CRS><AGENCY_TYPE>G</AGENCY_TYPE><PRIORITY>Average</PRIORITY><RESP_ASSGN_FROM>Krishna R Vishwakarma</RESP_ASSGN_FROM><RESP_ASSGN_TO>Admin</RESP_ASSGN_TO><PAGE_NO /> <PAGE_SIZE /><SORT_BY /><DESC /></TA_SEARCHAGENCY_1A_INPUT>"
        'Dim str_INPUT As String = "<TA_SEARCHAGENCY_1A_INPUT><ACTION>S</ACTION><LOCATION_CODE></LOCATION_CODE><NAME>Cox & Kings (Jw Marriott)</NAME><CHAIN_CODE></CHAIN_CODE><GROUP_NAME/><OFFICEID></OFFICEID><CITY_NAME></CITY_NAME><COUNTRY_NAME>India</COUNTRY_NAME><ONLINE_STATUS></ONLINE_STATUS><AOFFICE></AOFFICE><CRS></CRS><AGENCY_TYPE></AGENCY_TYPE><PRIORITY></PRIORITY><RESP_ASSGN_FROM></RESP_ASSGN_FROM><RESP_ASSGN_TO></RESP_ASSGN_TO><PAGE_NO /> <PAGE_SIZE /><SORT_BY /><DESC /></TA_SEARCHAGENCY_1A_INPUT>"
        'objXmlInput.LoadXml(str_INPUT)

        objXmlInput.Load("c:\A1out.xml")
        objXmlOutput = obj.Search(objXmlInput)

    End Sub

    ''CODE FOR ALLOCATION

    Private Sub Button32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button32.Click
        Dim objXml As New Xml.XmlDocument
        Dim obj As New bizTravelAgency.bzAgency1ARespChange
        'Dim strInputXML As String = "<TA_UPDATEAGENCY_1A_INPUT><ACTION>U</ACTION><LOCATION_CODE>737</LOCATION_CODE><NAME /><CHAIN_CODE /><GROUP_NAME /><OFFICEID /><CITY_NAME /><COUNTRY_NAME /><ONLINE_STATUS /><AOFFICE /><CRS /><AGENCY_TYPE /><PRIORITY /><RESP_ASSGN_FROM /><RESP_ASSGN_TO>ABC</RESP_ASSGN_TO><RESP_ASSGN_TO_ID></RESP_ASSGN_TO_ID></TA_UPDATEAGENCY_1A_INPUT>"

        Dim strInputXML As String = "<TA_UPDATEAGENCY_1A_INPUT><ACTION>U</ACTION><LOCATION_CODE>10334</LOCATION_CODE><NAME /><CHAIN_CODE /><GROUP_NAME /><OFFICEID /><CITY_NAME /><COUNTRY_NAME /><ONLINE_STATUS /><AOFFICE /><CRS /><AGENCY_TYPE /><PRIORITY /><RESP_ASSGN_FROM /><RESP_ASSGN_TO>Bhavani Iyer</RESP_ASSGN_TO><RESP_ASSGN_TO_ID>132</RESP_ASSGN_TO_ID><EMPLOYEEID>24</EMPLOYEEID></TA_UPDATEAGENCY_1A_INPUT>"


        objXml.LoadXml(strInputXML)
        'objXml.Load("c:\AAL.XML")

        objXml = obj.Update(objXml)

    End Sub
    ''CODE FOR DE-ALLOCATION
    Private Sub Button33_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button33.Click
        Dim objXml As New Xml.XmlDocument
        Dim obj As New bizTravelAgency.bzAgency1ARespChange
        'Dim strInputXML As String = "<TA_UPDATEAGENCY_1A_INPUT><ACTION>D</ACTION><LOCATION_CODE>90,190</LOCATION_CODE><NAME /><CHAIN_CODE /><GROUP_NAME /><OFFICEID /><CITY_NAME /><COUNTRY_NAME /><ONLINE_STATUS /><AOFFICE /><CRS /><AGENCY_TYPE /><PRIORITY /><RESP_ASSGN_FROM /><RESP_ASSGN_TO></RESP_ASSGN_TO><RESP_ASSGN_TO_ID></RESP_ASSGN_TO_ID></TA_UPDATEAGENCY_1A_INPUT>"
        'objXml.LoadXml(strInputXML)

        objXml.Load("c:\dall.xml")
        objXml = obj.Deallocate(objXml)

    End Sub

    Private Sub Button34_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button34.Click
        Dim objXml As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        Dim obj As New bizTravelAgency.bzPCInstallation
        Dim strInputXML As String = "<GET_PTYPEFOTTER_INPUT><LOCATION_CODE>9778</LOCATION_CODE></GET_PTYPEFOTTER_INPUT>"
        objXml.LoadXml(strInputXML)
        objXmlOutput = obj.GetPTypeFooterDetails(objXml)

    End Sub

    Private Sub Button35_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button35.Click

        Dim objXml As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        Dim obj As New bizTravelAgency.bzAgency1ARespChange
        Dim strInputXML As String = "<TA_HISTORY_RESP1A_INPUT><Lcode>768</Lcode><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></TA_HISTORY_RESP1A_INPUT>"

        objXml.LoadXml(strInputXML)
        objXmlOutput = obj.History(objXml)






    End Sub

    Private Sub FrmManoj_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button36_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button36.Click
        ''for agency notes

        Dim obj As New bizTravelAgency.bzAgencyNotes
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<MS_GETAGENCYNOTES_INPUT><LCode>26064</LCode><SORT_BY>EmployeeName</SORT_BY><DESC>TRUE</DESC></MS_GETAGENCYNOTES_INPUT>")
        objXmlOutput = obj.GetDetails(objXmlInput)

    End Sub

    Private Sub Button37_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button37.Click

        Dim obj As New bizTravelAgency.bzPCDeInstallation
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<MS_ORDER_CHALLAN_PC_DETAILS_INPUT><LCODE>20265</LCODE><ORDERNUMBER>2011/1/21424242</ORDERNUMBER><ROWID>1224412</ROWID></MS_ORDER_CHALLAN_PC_DETAILS_INPUT>")
        objXmlOutput = obj.CheckOrderChallanSerialAgainstDeInstallation(objXmlInput)

    End Sub

    Private Sub Button38_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button38.Click
        Try
            Dim obj As New bizTravelAgency.bzOrder
            Dim objInputXml, objOutputXml As New Xml.XmlDocument
            Dim strinput As String
            'strinput = ("<MS_UPDATEORDERS_INPUT><ORDERS ORDERID='94862' ORDERTYPEID='450' LCODE='10333' ORDER_NUMBER='' ORDERSTATUSID='7' PROCESSEDBYID='24' PLANID='' ISPID='' RESEND_DATE_MKT='' RECEIVED_DATE_MKT='' INSTALLATION_DUE_DATE='' EXPECTED_INSTALLATION_DATE='' APR='' OPC='' APC='' NewOrder='T' OFFICEID1='' OFFICEID='DELVS3652' RECEIVING_OFFICEID='' MSG_SEND_DATE='' APPLIED_DATE='' SENDBACK_DATE='' APPROVAL_DATE='20110210' RECEIVED_DATE='20110210' PROCESSED_DATE='' REMARKS='' PENDINGWITHID='' EMPLOYEEID='24' ATID='ABCD1234,9878POLD'/></MS_UPDATEORDERS_INPUT>")
            'objInputXml.LoadXml(strinput)
            'objOutputXml = obj.Update(objInputXml)
            'strinput = "<MS_VIEWORDER_INPUT><ORDERID>94862</ORDERID></MS_VIEWORDER_INPUT>"
            'objInputXml.LoadXml(strinput)
            'objOutputXml = obj.View(objInputXml)


            strinput = "<TA_GET_ATID_INPUT><LCODE>10333</LCODE><ORDER_NO>2011/2/12</ORDER_NO></TA_GET_ATID_INPUT>"
            objInputXml.LoadXml(strinput)
            objOutputXml = obj.GET_ATID(objInputXml)


        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button39_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button39.Click
        Dim obj As New bizTravelAgency.bzAgencyCRSUse
        Dim objInputXml, objOutputXml As New Xml.XmlDocument
        Dim strinput As String
        strinput = "<UP_UPDATECRSDETAILS_INPUT><CRS RN='84536' LCODE='38770' DATE='' CURRENTID='N' CRS='1A' OFFICEID='DELAR329W' Action='U' EMPLOYEEID='24' /><CRS RN='84535' LCODE='38770' DATE='' CURRENTID='Y' CRS='1A' OFFICEID='DELAR32:E' Action='U' EMPLOYEEID='24' /></UP_UPDATECRSDETAILS_INPUT>"
        objInputXml.LoadXml(strinput)
        objOutputXml = obj.Update(objInputXml)


    End Sub

    Private Sub Button40_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button40.Click
        'Dim obj As New bizTravelAgency.bzOrderType
        'Dim objInputXml, objOutputXml As New Xml.XmlDocument
        'Dim strinput As String
        'strinput = "<MS_UPDATEORDERTYPE_INPUT><ORDERTYPE ACTION='U' ORDERTYPEID='13' ForPCType='None' ORDER_TYPE_NAME='Add Change For IL Line' IsDeleted='' IsISPOrder='1' IsChallanOrder='0' IsTrainingOrder='0' IshardwareOrder='0' OrderTypeCategoryID='9' NewConnectivity="" OldConnectivity='' OrderTrackingRequired='' TimeRequired='22' CANCELLATION='0' NEW_ORDER='1' DESCRIPTION='add Change'/></MS_UPDATEORDERTYPE_INPUT>"
        'objInputXml.LoadXml(strinput)
        'objOutputXml = obj.Update(objInputXml)


        Dim obj As New bizTravelAgency.bzOrderType
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<MS_UPDATEORDERTYPE_INPUT><ORDERTYPE ACTION='U' ORDERTYPEID='13' ForPCType='None' ORDER_TYPE_NAME='Add Change For IL Line' IsDeleted='' IsISPOrder='1' IsChallanOrder='0' IsTrainingOrder='0' IshardwareOrder='0' OrderTypeCategoryID='9' NewConnectivity='' OldConnectivity='' OrderTrackingRequired='1' TimeRequired='22' CANCELLATION='0' NEW_ORDER='1' DESCRIPTION='add Change' /></MS_UPDATEORDERTYPE_INPUT>")
        objXmlOutput = obj.Update(objXmlInput)


    End Sub

    Private Sub Button41_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button41.Click

        Dim obj As New bizTravelAgency.bzOrder
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<MS_UPDATEORDERS_INPUT><ORDERS ORDERID='82782' ORDERTYPEID='210' LCODE='6988' ORDER_NUMBER='2009/8/464' ORDERSTATUSID='7' PROCESSEDBYID='24' PLANID='' ISPID='' RESEND_DATE_MKT='' RECEIVED_DATE_MKT='' INSTALLATION_DUE_DATE='' EXPECTED_INSTALLATION_DATE='20090827' APR='' OPC='200' APC='' NewOrder='T' OFFICEID1='' OFFICEID='DELWI2202' RECEIVING_OFFICEID='' MSG_SEND_DATE='20090826' APPLIED_DATE='20090826' SENDBACK_DATE='' APPROVAL_DATE='20090826' RECEIVED_DATE='20090826' PROCESSED_DATE='' REMARKS='694 to 743 add' PENDINGWITHID='0' EMPLOYEEID='24' ATID='589115D1,5891157C,58914604,5892E0EC,5892E0ED,5892E0EE,5892E0EF,5892E0FA,5892E0F0,5892E0F1,5892E0F2,5892E0F3,5892E0F4,5892E0F5,5892E0F6,5892E0F7,5892E0F8,5892E0F9,5892E10A,5892E10B,5892E10C,5892E10D,5892E10E,5892E10F,5892E100,5892E101,5892E102,5892E103,5892E104,5892E105,5892E106,5892E107,5892E108,5892E109,5892E11A,5892E11B,5892E11E,5892E11F,5892E110,5892E111,5892E112,5892E113,5892E114,5892E115,5892E116,5892E117,5892E118,5892E119,58923891,589271F2,589115D1,5891157C,58914604,5892E0EC,5892E0ED,5892E0EE,5892E0EF,5892E0FA,5892E0F0,5892E0F1,5892E0F2,5892E0F3,5892E0F4,5892E0F5,5892E0F6,5892E0F7,5892E0F8,5892E0F9,5892E10A,5892E10B,5892E10C,5892E10D,5892E10E,5892E10F,5892E100,5892E101,5892E102,5892E103,5892E104,5892E105,5892E106,5892E107,5892E108,5892E109,5892E11A,5892E11B,5892E11E,5892E11F,5892E110,5892E111,5892E112,5892E113,5892E114,5892E115,5892E116,5892E117,5892E118,5892E119,58923891,589271F2,589115D1,5891157C,58914604,5892E0EC,5892E0ED,5892E0EE,5892E0EF,5892E0FA,5892E0F0,5892E0F1,5892E0F2,5892E0F3,5892E0F4,5892E0F5,5892E0F6,5892E0F7,5892E0F8,5892E0F9,5892E10A,5892E10B,5892E10C,5892E10D,5892E10E,5892E10F,5892E100,5892E101,5892E102,5892E103,5892E104,5892E105,5892E106,5892E107,5892E108,5892E109,5892E11A,5892E11B,5892E11E,5892E11F,5892E110,5892E111,5892E112,5892E113,5892E114,5892E115,5892E116,5892E117,5892E118,5892E119,58923891,589271F2,589115D1,5891157C,58914604,5892E0EC,5892E0ED,5892E0EE,5892E0EF,5892E0FA,5892E0F0,5892E0F1,5892E0F2,5892E0F3,5892E0F4,5892E0F5,5892E0F6,5892E0F7,5892E0F8,5892E0F9,5892E10A,5892E10B,5892E10C,5892E10D,5892E10E,5892E10F,5892E100,5892E101,5892E102,5892E103,5892E104,5892E105,5892E106,5892E107,5892E108,5892E109,5892E11A,5892E11B,5892E11E,5892E11F,5892E110,5892E111,5892E112,5892E113,5892E114,5892E115,5892E116,5892E117,5892E118,5892E119,58923891,589271F2' COMP_VERTICAL='1' /></MS_UPDATEORDERS_INPUT>")
        objXmlOutput = obj.Update(objXmlInput)





    End Sub
End Class
