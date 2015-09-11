Public Class frmNeeraj

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim obj As New bizTravelAgency.bzPCInstallation
            Dim objXml As New Xml.XmlDocument
            'Dim str_INPUT As String = "<UP_TA_UPDATE_PCINSTALLATION_INPUT><DETAIL LCODE ='2' DATE  = '20080409'   CPUTYPE ='SI2' CPUNO = 'L9A8256'  MONTYPE  = 'MI1'  MONNO  = 'V224897' KBDTYPE = 'KBD'  KBDNO  = '123'  CDRNO=''  MSETYPE  = 'MSE' MSENO  = '9899846293'  OrderNumber  = '' REMARKS  = 'Test Be neraj' CHALLANDATE  ='' CHALLANNUMBER  = 'AGR/018/7/0005' LoggedBy  ='59' LoggedDateTime  = ''  CHALLANSTATUS  = '' ROWID=''  PCTYPE ='1'  USE_BACKDATED_CHALLAN='FALSE' OVERRIDE_CHALLAN_NO ='FALSE' OVERRIDE_CHALLAN_SERIAL_NO='FALSE' OVERRIDE_ORDER_NO='FALSE' /></UP_TA_UPDATE_PCINSTALLATION_INPUT>"
            'Dim str_INPUT As String = "<UP_TA_UPDATE_PCINSTALLATION_INPUT><DETAIL LCODE='2' DATE='20080409' CPUTYPE='' CPUNO='' MONTYPE='' MONNO='' KBDTYPE='' KBDNO='' CDRNO='' MSETYPE='' MSENO='' OrderNumber='7868768' Qty='1' REMARKS='' CHALLANDATE='' CHALLANNUMBER='' LoggedBy='24' LoggedDateTime='' CHALLANSTATUS='' ROWID='' PCTYPE='1' USE_BACKDATED_CHALLAN='False' OVERRIDE_CHALLAN_NO='False' OVERRIDE_CHALLAN_SERIAL_NO='False' OVERRIDE_ORDER_NO='False' /></UP_TA_UPDATE_PCINSTALLATION_INPUT>"

            Dim str_INPUT As String = "<UP_TA_UPDATE_PCINSTALLATION_INPUT><DETAIL LCODE='27409' DATE='20090112' CPUTYPE='' ADDLRAM='' CPUNO='' MONTYPE='' MONNO='' KBDTYPE='' KBDNO='' CDRNO='' MSETYPE='' MSENO='' OrderNumber='2009/1/6' Qty='1' REMARKS='' CHALLANDATE='' CHALLANNUMBER='' LoggedBy='24' LoggedDateTime='' CHALLANSTATUS='' ROWID='' PCTYPE='0' USE_BACKDATED_CHALLAN='False' OVERRIDE_CHALLAN_NO='False' OVERRIDE_CHALLAN_SERIAL_NO='False' OVERRIDE_ORDER_NO='False' /></UP_TA_UPDATE_PCINSTALLATION_INPUT>"

            objXml.LoadXml(str_INPUT)
            objXml = obj.Update(objXml)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Try
            Dim obj As New bizTravelAgency.bzPCInstallation
            Dim objXml As New Xml.XmlDocument
            Dim str_INPUT As String = "<MS_GET_ORDERINSTALLATIONHISTORY_INPUT><ROWID>154409</ROWID></MS_GET_ORDERINSTALLATIONHISTORY_INPUT>"
            objXml.LoadXml(str_INPUT)
            objXml = obj.GetOrderInstallationHistory(objXml)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Dim obj As New bizTravelAgency.bzPCInstallation
            Dim objXml As New Xml.XmlDocument
            Dim objOUTPUTXml As New Xml.XmlDocument

            Dim str_INPUT As String = "<UP_TA_VIEW_PCINSTALLATION_INPUT><ROWID>2617</ROWID></UP_TA_VIEW_PCINSTALLATION_INPUT>"
            objXml.LoadXml(str_INPUT)
            objXml = obj.View(objXml)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Try
            Dim obj As New bizTravelAgency.bzAgency
            Dim objXml As New Xml.XmlDocument
            Dim objOUTPUTXml As New Xml.XmlDocument

            Dim str_INPUT As String = "<MS_GETPCINSTALLATION_INPUT><LCODE>19146</LCODE></MS_GETPCINSTALLATION_INPUT>"
            objXml.LoadXml(str_INPUT)
            objXml = obj.GetInstalledPC(objXml)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            Dim obj As New bizTravelAgency.bzPCReplacement
            Dim objXml As New Xml.XmlDocument

            'Dim str_INPUT As String = "<UP_TA_PCREPLACEMENT_INPUT><DETAIL ACTION ='R' LCODE ='2' DATE  = '20080410'   CPUTYPE ='SI2' CPUNO = 'L9A8256'  MONTYPE  = 'MI1'  MONNO  = 'V224897' KBDTYPE = 'KBD'  KBDNO  ='111'  CDRNO=''  MSETYPE  = 'MSE' MSENO  = '23-10000'  OrderNumber  = 'AGR/018/7/0002' Qty ='1' REMARKS  = 'TEST NEERAJ PDREPLACEMENT' CHALLANDATE  ='' CHALLANNUMBER  = '' LoggedBy  ='24' LoggedDateTime  = ''  CHALLANSTATUS  = '' ROWID='5'  PCTYPE ='1' USE_BACKDATED_CHALLAN ='FALSE' OVERRIDE_CHALLAN_NO ='FALSE' OVERRIDE_CHALLAN_SERIAL_NO='FALSE' OVERRIDE_ORDER_NO='FALSE'	/></UP_TA_PCREPLACEMENT_INPUT>"
            'objXml.LoadXml(str_INPUT)
            'objXml = obj.Update(objXml)


            


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            Dim obj As New bizTravelAgency.bzPCDeInstallation
            Dim objXml As New Xml.XmlDocument

            'Dim str_INPUT As String = "<UP_TA_PCDEINSTALLATION_INPUT><DETAIL ACTION ='X' LCODE ='2' DATE  = '20080410'   CPUTYPE ='CPP' CPUNO = '101'  MONTYPE  = 'MMP'  MONNO  = '102' KBDTYPE = 'KBP'  KBDNO  ='122'  CDRNO='1001'  MSETYPE  = 'MSE' MSENO  = '45'  OrderNumber  = '1212' Qty ='5'  REMARKS  = 'TEST DEINSTALL' CHALLANDATE  ='' CHALLANNUMBER  = 'AGR/018/7/0005' LoggedBy  ='24' LoggedDateTime  = ''  CHALLANSTATUS  = '' ROWID = '3865'  PCTYPE ='2' USE_BACKDATED_CHALLAN='FALSE' OVERRIDE_CHALLAN_NO ='FALSE' OVERRIDE_CHALLAN_SERIAL_NO='FALSE' OVERRIDE_ORDER_NO='FALSE'/></UP_TA_PCDEINSTALLATION_INPUT>"

            Dim str_INPUT As String = "<UP_TA_PCDEINSTALLATION_INPUT><DETAIL ACTION='X' LCODE='32495' ADDLRAM='' DATE='20110124' CPUTYPE='' CPUNO='' MONTYPE='' MONNO='' KBDTYPE='' KBDNO='' CDRNO='' MSETYPE='' MSENO='' OrderNumber='2011/1/500' Qty='1' REMARKS='' CHALLANDATE='' CHALLANNUMBER='0' LoggedBy='24' LoggedDateTime='' CHALLANSTATUS='' ROWID='1211467' PCTYPE='0' USE_BACKDATED_CHALLAN='True' OVERRIDE_CHALLAN_NO='True' OVERRIDE_CHALLAN_SERIAL_NO='True' OVERRIDE_ORDER_NO='True' /> </UP_TA_PCDEINSTALLATION_INPUT>"

            objXml.LoadXml(str_INPUT)
            objXml = obj.Update(objXml)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            Dim obj As New bizTravelAgency.bzPCInstallation
            Dim objXml As New Xml.XmlDocument

            Dim str_INPUT As String = "<UP_TA_UPDATE_PCINSTALLATION_INPUT><DETAIL LCODE='26064' DATE='20090109' CPUTYPE='' CPUNO='' MONTYPE='' MONNO='' KBDTYPE='' KBDNO='' CDRNO='' MSETYPE='' MSENO='' ADDLRAM='' OrderNumber='7868768' Qty='5' REMARKS='' CHALLANDATE='' CHALLANNUMBER='' LoggedBy='24' LoggedDateTime='' CHALLANSTATUS='' ROWID='' PCTYPE='0' USE_BACKDATED_CHALLAN='False' OVERRIDE_CHALLAN_NO='False' OVERRIDE_CHALLAN_SERIAL_NO='False' OVERRIDE_ORDER_NO='False' /></UP_TA_UPDATE_PCINSTALLATION_INPUT>"
            'Dim str_INPUT As String = "<UP_TA_PCDEINSTALLATION_INPUT><DETAIL ACTION ='X' LCODE ='2' DATE  = '20080410'   CPUTYPE ='CPP' CPUNO = '101'  MONTYPE  = 'MMP'  MONNO  = '102' KBDTYPE = 'KBP'  KBDNO  ='122'  CDRNO='1001'  MSETYPE  = 'MSE' MSENO  = '45'  OrderNumber  = '1212' Qty ='1'  REMARKS  = 'TEST DEINSTALL' CHALLANDATE  ='' CHALLANNUMBER  = 'AGR/018/7/0005' LoggedBy  ='24' LoggedDateTime  = ''  CHALLANSTATUS  = '' ROWID = '3865'  PCTYPE ='1' USE_BACKDATED_CHALLAN='FALSE' OVERRIDE_CHALLAN_NO ='FALSE' OVERRIDE_CHALLAN_SERIAL_NO='FALSE' OVERRIDE_ORDER_NO='FALSE'/></UP_TA_PCDEINSTALLATION_INPUT>"
            objXml.LoadXml(str_INPUT)
            objXml = obj.Update(objXml)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try

    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Try
            Dim obj As New bizTravelAgency.bzPCInstallation
            Dim objXml As New Xml.XmlDocument
            Dim str_INPUT As String = "<MS_GET_ORDERS_PC_COUNT_INPUT><LCODE>10</LCODE><ORDERNUMBER>2004/5/321</ORDERNUMBER><NEWORDER>T</NEWORDER><NoOfPCs>1</NoOfPCs><PCType>1</PCType></MS_GET_ORDERS_PC_COUNT_INPUT>"

            objXml.LoadXml(str_INPUT)
            objXml = obj.CheckForNoOfPC(objXml)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try

    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Try

            Dim objOutputXml As New Xml.XmlDocument
            Dim bzOrder As New AAMS.bizTravelAgency.bzOrder
            objOutputXml = bzOrder.GetFileNumber()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try


    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Try

            Dim objOutputXml As New Xml.XmlDocument
            Dim objXml As New Xml.XmlDocument

            Dim bzagency As New AAMS.bizTravelAgency.bzAgency
            Dim str_INPUT As String = "<UP_UPDATEFILENO_INPUT><FILENO>11</FILENO><LOCATION_CODE>8309</LOCATION_CODE><STATUS></STATUS></UP_UPDATEFILENO_INPUT>"
            objXml.LoadXml(str_INPUT)
            objOutputXml = bzagency.SaveAgencyFileno(objXml)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try

    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click

        Dim objOutputXml As New Xml.XmlDocument
        Dim objXml As New Xml.XmlDocument

        Dim bzagency As New AAMS.bizTravelAgency.bzOrder
        Dim str_INPUT As String = "<TA_UPDATEAGENCYORDERREMARKSDETAILS_INPUT><DETAILS ORDERID='1953' REMARKS='tested by neeraj'/></TA_UPDATEAGENCYORDERREMARKSDETAILS_INPUT>"

        Try

            objXml.LoadXml(str_INPUT)
            objOutputXml = bzagency.UpdateOrderRemarks(objXml)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Dim obj As New bizTravelAgency.bzAgency
        'Dim str_INPUT As String = "<TA_SEARCHAGENCY_INPUT><OFFICEID></OFFICEID></TA_SEARCHAGENCY_INPUT>"
        Dim str_INPUT As String = "<TA_SEARCHAGENCY_INPUT><OFFICEID>RMBRR3001</OFFICEID><BR_HD>TRUE</BR_HD></TA_SEARCHAGENCY_INPUT>"

        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(str_INPUT)
        objXml = obj.OfficeID_AgencySearch(objXml)
    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click

        Dim obj As New bizTravelAgency.bzAgency
        Dim objXml As New Xml.XmlDocument
        Dim str_INPUT As String = "<TA_UPDATEAGENCY_INPUT><Agency LOCATION_CODE='28225' Chain_Name='test' Chain_Code='7332' IATA_TID='NON' EMAIL='' FAX='' PHONE='' COUNTRY='India' PINCODE='' CITY='Agra' ADDRESS1='' ADDRESS='Haryana' NAME='Test by Mukund' Aoffice='AGR' CONTACT_PERSON_ID='' ORDERNUMBER_BACKUP='' ORDERNUMBER_PRIMARY='' INSTALL_DATE_BACKUP='' INSTALL_DATE_PRIMARY='' ONLINE_STATUS_BACKUP='' AGENCYSTATUSID='' AGENCYTYPEID='' LOCATION_SHORT_NAME='' PRIORITYID='' RESP_1A='' RESP_1A_NAME='' WWW_ADDRESS='' ONLINE_STATUS='' DATE_OFFLINE='' DATE_ONLINE='' FILENO='' INCLUDE_IN_CCR='False' INCLUDE_IN_CCR_REASON='' CUSTOMER_CATEGORY_ID='' EMPLOYEEID='24' IPAddressID='76' IPAddress='3.3.3.3'/></TA_UPDATEAGENCY_INPUT>"

        objXml.LoadXml(str_INPUT)
        objXml = obj.Update(objXml)

    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        Dim obj As New bizTravelAgency.bzOrder
        Dim objXml As New Xml.XmlDocument
        Dim str_INPUT As String = "<TA_ORIGINAL_INPUT><LCODE>1234</LCODE></TA_ORIGINAL_INPUT>"
        objXml.LoadXml(str_INPUT)
        objXml = obj.GetOrderNumber(objXml)

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

    End Sub


    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim obj As New bizTravelAgency.bzAgency
        Dim objXml As New Xml.XmlDocument
        Dim str_INPUT As String = "<UP_GETCOMPETITIONDETAILS_INPUT><CHAIN_CODE>163</CHAIN_CODE><ResponsibleStaffID>24</ResponsibleStaffID></UP_GETCOMPETITIONDETAILS_INPUT>"
        objXml.LoadXml(str_INPUT)
        objXml = obj.GetAgencyGroupCompetitionDetails(objXml)
    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Dim obj As New bizTravelAgency.bzAgency
        Dim objXml As New Xml.XmlDocument
        Dim str_INPUT As String = "<UP_GETAGENCYSTAFFDETAILS_INPUT><CHAIN_CODE>3831</CHAIN_CODE><ResponsibleStaffID>24</ResponsibleStaffID></UP_GETAGENCYSTAFFDETAILS_INPUT>"

        '<UP_GETAGENCYSTAFFDETAILS_INPUT>

        '<CHAIN_CODE</CHAIN_CODE> 

        ' <ResponsibleStaffID></ResponsibleStaffID>

        '</UP_GETAGENCYSTAFFDETAILS_INPUT>

        objXml.LoadXml(str_INPUT)
        objXml = obj.GetAgencyGroupStaffDetails(objXml)

    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        Dim obj As New bizTravelAgency.bzAgency
        Dim objXml As New Xml.XmlDocument
        Dim str_INPUT As String = "<UP_GETCRSDETAILS_INPUT><CHAIN_CODE>163</CHAIN_CODE><ResponsibleStaffID>24</ResponsibleStaffID></UP_GETCRSDETAILS_INPUT>"
        objXml.LoadXml(str_INPUT)
        objXml = obj.GetAgencyGroupCrsUseDetails(objXml)

    End Sub

    Private Sub Button18_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        Dim obj As New bizTravelAgency.bzAgency
        Dim objXml As New Xml.XmlDocument
        Dim str_INPUT As String = "<TA_SEARCHAGENCY_INPUT><OFFICEID>DACVS3400</OFFICEID></TA_SEARCHAGENCY_INPUT>"
        objXml.LoadXml(str_INPUT)
        objXml = obj.GetAgencyAddressDetails(objXml)
    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        Dim obj As New bizTravelAgency.bzOrderType
        Dim objXml As New Xml.XmlDocument
        Dim str_INPUT As String = "<MS_SEARCHORDERSTATUS_INPUT><ORDERTYPEID>346</ORDERTYPEID></MS_SEARCHORDERSTATUS_INPUT>"
        'IndexDoc.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText.Trim()
        objXml.LoadXml(str_INPUT)
        objXml = obj.View(objXml)


    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Dim obj As New bizTravelAgency.bzOrder
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<MS_GETAGENCYPRODUCT_INPUT><PAGE_NO>1</PAGE_NO><PAGE_SIZE>50</PAGE_SIZE><SORT_BY>nameTEST</SORT_BY><DESC>true</DESC><LCODE>1783</LCODE></MS_GETAGENCYPRODUCT_INPUT>")
        objXmlOutput = obj.GetDetails(objXmlInput)

    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click

        Dim ds As DataSet
        Dim objOutputXml As New Xml.XmlDocument
        Dim bzOrder As New AAMS.bizTravelAgency.bzOrder
        Dim objXmlInput As New Xml.XmlDocument
        Dim objXmlOutput As New Xml.XmlDocument
        objXmlInput.LoadXml("<TA_GETCONTROLIMAGE_INPUT><SecurityOptionID>5</SecurityOptionID></TA_GETCONTROLIMAGE_INPUT>")
        ds = bzOrder.GetControlsImagesforPermission(objXmlInput)

    End Sub
End Class