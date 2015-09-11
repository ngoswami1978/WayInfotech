Public Class NeerajTest

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        'VIEW AIRLINEOFFICE
        Dim obj As New bizMaster.bzStyle
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_VIEWAIRLINEOFFICE_INPUT><AR_OF_ID>4</AR_OF_ID></MS_VIEWAIRLINEOFFICE_INPUT>")
        objXml = obj.View(objXml)
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim obj As New bizMaster.bzStyle
        Dim objXml As New Xml.XmlDocument
        ' objXml.LoadXml("<MS_UPDATEAIRLINEOFFICE_INPUT><AIRLINE_OFFICE Action='I' AR_OF_ID='' Airline_Code='AR' AR_OF_Address='SDSD' Aoffice='DEL' /></MS_UPDATEAIRLINEOFFICE_INPUT>")
        objXml.LoadXml("<MS_UPDATEAIRLINEOFFICE_INPUT><AIRLINE_OFFICE Action='I' AR_OF_ID='' Airline_Code='PK' AR_OF_Address='fgfg' Aoffice='' /></MS_UPDATEAIRLINEOFFICE_INPUT>")
        objXml = obj.Update(objXml)

    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim obj As New bizMaster.bzStyle
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_UPDATESTYLE_INPUT><STYLE Action='U' W_StyleId='22' BarcodeNo='fgd1212' StyleName='fdgdfg121' DesignNo='dfgdfg121' ShadeNo='' MRP='0' /></MS_UPDATESTYLE_INPUT>")
        objXml = obj.Update(objXml)
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim obj As New bizMaster.bzStyle
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_DELETEAIRLINEOFFICE_INPUT><AR_OF_ID>7</AR_OF_ID></MS_DELETEAIRLINEOFFICE_INPUT>")
        objXml = obj.Delete(objXml)
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim obj As New bizMaster.bzStyle
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_SEARCHSTYLE_INPUT><BarcodeNo>B</BarcodeNo><StyleName /><DesignNo /><ShadeNo /><MRP /><PAGE_NO /><PAGE_SIZE /><SORT_BY /><DESC /></MS_SEARCHSTYLE_INPUT>")

        objXml = obj.Search(objXml)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim obj As New bizMaster.bzStyle
        Dim objXml As New Xml.XmlDocument
        'objXml.LoadXml("<MS_UPDATESTYLE_ORDER_INPUT><STYLE Action='I' W_StyleOrderID='' W_StyleId='1' BarcodeNo='' StyleName='' DesignNo='' ShadeNo='' MRP='' Qty='5000' Remarks='Test' /></MS_UPDATESTYLE_ORDER_INPUT>")
        objXml.LoadXml("<MS_UPDATESTYLE_ORDER_INPUT><STYLE Action='I' W_StyleId='19' BarcodeNo='' StyleName='' DesignNo='' ShadeNo='' MRP='' Qty='710' Remarks='' /></MS_UPDATESTYLE_ORDER_INPUT>")

        objXml = obj.UpdateStyleOrder(objXml)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ''EMP SEARCH
        Dim obj As New bizMaster.bzEmployee
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_SEARCHEMPLOYEE_INPUT><PhoneNo></PhoneNo><Email></Email><Employee_Name>Administrator</Employee_Name><SecurityOptionID></SecurityOptionID><SecurityRegionID></SecurityRegionID><Request /><Sec_Group_ID /><PAGE_NO /><PAGE_SIZE /><SORT_BY /><DESC /><INC></INC></MS_SEARCHEMPLOYEE_INPUT>")

        objXml = obj.Search(objXml)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ''update
        Dim obj As New bizMaster.bzEmployee
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_EMPLOYEE_UPDATE_INPUT><Employee Action='' Cell_Phone='989889' Login='ashish' Email='admin@gmail.com' EmployeeID='7' Employee_name='Ashish Srivastava' Password='test' Firstform='' Changepassword='' Pwdexpire='' IPrestriction='' ContactPersonName='123' /></MS_EMPLOYEE_UPDATE_INPUT>")
        objXml = obj.Update(objXml)

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        ''view
        Dim obj As New bizMaster.bzEmployee
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_VIEWEMPLOYEE_INPUT><EmployeeID>10</EmployeeID></MS_VIEWEMPLOYEE_INPUT>")
        objXml = obj.View(objXml)

    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        ''DELETE
        Dim obj As New bizMaster.bzEmployee
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_DELETE_EMP_INPUT><EMPLOYEEID>10</EMPLOYEEID></MS_DELETE_EMP_INPUT>")
        objXml = obj.Delete(objXml)

    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Dim obj As New bizMaster.bzEmployee
        Dim objXml As New Xml.XmlDocument

        objXml.LoadXml("<MS_LOGIN_INPUT><Login>neeraj</Login><Password>test</Password><IPAddress></IPAddress></MS_LOGIN_INPUT>")
        objXml = obj.Login(objXml)

    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Dim obj As New bizMaster.bzEmployee
        Dim objXml As New Xml.XmlDocument

        objXml.LoadXml("<MS_CHANGEPASSWORD_INPUT><EmployeeID>5</EmployeeID><OldPassword>admin1</OldPassword><NewPassword>amadeus</NewPassword></MS_CHANGEPASSWORD_INPUT>")
        objXml = obj.ChangePassword(objXml)

    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Dim obj As New bizMaster.bzStyle
        Dim objXml As New Xml.XmlDocument

        objXml.LoadXml("<UP_LIST_STYLE_INPUT><TYPE>3</TYPE></UP_LIST_STYLE_INPUT>")
        objXml = obj.List(objXml)
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Dim obj As New bizMaster.bzStyle
        Dim objXml As New Xml.XmlDocument

        objXml.LoadXml("<INV_VIEW_W_STYLEORDER_INPUT><W_StyleOrderID>2</W_StyleOrderID></INV_VIEW_W_STYLEORDER_INPUT>")

        objXml = obj.OrderView(objXml)

    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click

        Dim obj As New bizMaster.bzStyle
        Dim objXml As New Xml.XmlDocument

        objXml.LoadXml("<INV_UPDATE_W_ORDER_INPUT><ORDERHEADER W_StyleOrderID='' Totqty='120' Remarks='tst' OrderDate='20111021' LoggedBy='24' LoggedByName='Admin' OrderNumber=''><ORDERDETAILS W_StyleOrderID='' qty='100' W_StyleId='1' StyleName='test' DesignNo='test' ShadeNo='test' MRP='30' /> <ORDERDETAILS W_StyleOrderID='' qty='20' W_StyleId='2' StyleName='test' DesignNo='test' ShadeNo='test' MRP='30' /> </ORDERHEADER></INV_UPDATE_W_ORDER_INPUT>")

        objXml = obj.UpdateOrderBooking(objXml)

    End Sub

    Private Sub NeerajTest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class