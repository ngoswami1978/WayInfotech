Imports System.Xml
Imports System.Data
Partial Class ISP_ISP_OrderSendMail
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt

#Region "Code for Filter "
    'Code Added by M. K
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    'Code Added by M. K
#End Region

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim strEmailList As String = ""
        Session("EmailList") = Nothing
        Dim listitem2 As ListItem
        Try

            If (lstEmail.Items.FindByValue(txtEmail.Text) Is Nothing) Then
                lstEmail.Items.Insert(0, New ListItem(txtEmail.Text, txtEmail.Text))
            End If
            For Each listitem2 In lstEmail.Items
                If (strEmailList.Trim().Length = 0) Then
                    strEmailList = listitem2.Text
                Else
                    strEmailList += "," + listitem2.Text
                End If
            Next
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            If (strEmailList.Trim().Length > 0) Then
                Session("EmailList") = strEmailList
            Else
                Session("EmailList") = Nothing
            End If
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strEmailList As String = ""
        Try
            Session("PrevIspDetails") = Nothing

            If Request.QueryString("Lcode") Is Nothing Then
                Exit Sub
            ElseIf (Request.QueryString("Lcode").ToString().Length = 0) Then
                Exit Sub
            End If



            Dim Lcode As String = "-1"
            Dim OrderType As String = "-1"
            Dim CHAIN_CODE As String = "-1"
            Dim OrderId As String = "-1"
            Session("PageName") = Request.Url.ToString()
            objeAAMS.ExpirePageCache()

            btnPrevIspDetails.CssClass = "headingtabactive"
            BtnSendingMail.CssClass = "headingtab"
            btnIspOrder.CssClass = "headingtabactive"

            'Code of Security Check
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            btnAdd.Attributes.Add("onClick", "return Validate();")

            BtnEmailGroup.Attributes.Add("onClick", "return SelectEmailGroup();")


            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            Dim strBuilder As New StringBuilder()

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OrderSendMail']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OrderSendMail']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                    End If
                    If strBuilder(1) = "0" Then
                        btnSendMail.Disabled = True
                        btnAdd.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            '###########################################################
            ' Find Lcode On The basis of Order Id
            If (Not IsPostBack) Then

                Dim listitem2 As ListItem
                Session("EmailList") = Nothing
                '   objeAAMS.BindDropDown(drpLstGroupName, "AgencyGroup", False)   
                If Request.QueryString("Action") IsNot Nothing Then
                    If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                        'Dim OrderId As String = Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString().ToUpper()
                    
                    End If


                    '#############################################################
                    Dim objInputXml, objOutputXml As New XmlDocument
                    Dim objInputXmlForMail, objOutputXmlForMail As New XmlDocument
                    Dim objInputXml2, objOutputXml2 As New XmlDocument
                    Dim objXmlReader As XmlNodeReader
                    Dim objInputTempXml, objOutputTempXml As New XmlDocument
                    Dim ds As New DataSet
                    'Dim objXmlReader As XmlNodeReader
                    Dim blnGroupISP As Boolean
                    Dim blnMNC As Boolean
                    Dim strAoffice As String = ""

                    Dim objbzOrderType As New AAMS.bizTravelAgency.bzOrderType
                    Dim objOrderSendmail As New AAMS.bizMaster.bzEmailGroup
                    Dim objAgencyMNC As New AAMS.bizTravelAgency.bzAgency
                    Dim objMailTemplate As New AAMS.bizMaster.bzEmailTemplates

                    blnGroupISP = True

                    ' @End of  Code for GroupISP on the basis of Order Type

                    ' @ Code for MNC/NON-MNC BY LCODE on the basis of Order Type
                    objInputXml2.LoadXml("<UP_GETAGENCYGROUP_INPUT><LOCATION_CODE></LOCATION_CODE></UP_GETAGENCYGROUP_INPUT>")


                    Dim DecreptedISPOrderID As String
                    DecreptedISPOrderID = objED.Decrypt(Request.QueryString("IspOrderId").ToString())

                    Dim DecreptedLcode As String
                    DecreptedLcode = objED.Decrypt(Request.QueryString("Lcode").ToString())

                    Lcode = DecreptedLcode
                    hdLcode.Value = Lcode
                    OrderType = ""
                    hdFileno.Value = ""
                    hdOrderId.Value = DecreptedISPOrderID

                    objInputXml2.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = Lcode ' Session("Action").ToString().Split("|").GetValue(1).ToString()
                    'Here Back end Method Call
                    objOutputXml2 = objAgencyMNC.AgencyGroup_Type(objInputXml2)
                    If objOutputXml2.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        ' blnGroupISP = objOutputXml2.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsISPOrder").Value
                        CHAIN_CODE = objOutputXml2.DocumentElement.SelectSingleNode("AGENCYGROUPTYPE").Attributes("CHAIN_CODE").Value()
                        If (objOutputXml2.DocumentElement.SelectSingleNode("AGENCYGROUPTYPE").Attributes("GroupTypeID").Value() = "1") Then
                            blnMNC = True
                        Else
                            blnMNC = False
                        End If
                        If (objOutputXml2.DocumentElement.SelectSingleNode("AGENCYGROUPTYPE").Attributes("GroupTypeID").Value() = "1") Then
                            strAoffice = objOutputXml2.DocumentElement.SelectSingleNode("AGENCYGROUPTYPE").Attributes("AOffice").Value()
                        Else
                            strAoffice = objOutputXml2.DocumentElement.SelectSingleNode("AGENCYGROUPTYPE").Attributes("AOffice").Value() ' ""
                        End If
                    End If




                    ' @ End of Code for MNC/NON-MNC BY LCODE on the basis of Order Type

                    objInputXmlForMail.LoadXml("<MS_GETEMAIL_INPUT><GROUPDETAIL GroupMNC='' GroupISP='' GroupTraining='' GroupAoffice='' Aoffice='' TrainingAoffice='' /></MS_GETEMAIL_INPUT>")
                    With objInputXmlForMail.DocumentElement.SelectSingleNode("GROUPDETAIL")


                        .Attributes("GroupTraining").Value() = 0
                        .Attributes("TrainingAoffice").Value() = ""
                        .Attributes("GroupAoffice").Value() = 1
                        .Attributes("Aoffice").Value() = strAoffice '""


                        If blnMNC = True Then
                            .Attributes("GroupMNC").Value() = 1
                        Else
                            .Attributes("GroupMNC").Value() = 0
                        End If

                        If (blnGroupISP = True) Then
                            .Attributes("GroupISP").Value() = 1
                        Else
                            .Attributes("GroupISP").Value() = 0
                        End If


                    End With
                    objOutputXmlForMail = objOrderSendmail.GetEmailID2s(objInputXmlForMail)
                    'Here Back end Method Call
                    '  objOutputXml = objbzOrder.Delete(objInputXml)
                    If objOutputXmlForMail.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        ' @ Code for MNC/NON-MNC BY LCODE on the basis of Order Type

                        objXmlReader = New XmlNodeReader(objOutputXmlForMail)
                        ds.ReadXml(objXmlReader)

                        lstEmail.DataSource = ds.Tables("EMAILDETAIL")
                        lstEmail.DataTextField = "Email"
                        lstEmail.DataValueField = "Email"
                        lstEmail.DataBind()
                        For Each listitem2 In lstEmail.Items
                            If (strEmailList.Trim().Length = 0) Then
                                strEmailList = listitem2.Text
                            Else
                                strEmailList += "," + listitem2.Text
                            End If
                        Next

                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            If (strEmailList.Trim().Length > 0) Then
                Session("EmailList") = strEmailList
            Else
                Session("EmailList") = Nothing
            End If
            If hdViewEmailDetailsByDept.Value = "1" Then
                BindEmailDataByGroupId()
            End If
        End Try
    End Sub

    'Protected Sub btnAddemail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddemail.Click
    '    Dim strEmailList As String = ""
    '    Session("EmailList") = Nothing
    '    Try
    '        Dim listitem As ListItem
    '        For Each listitem In ChkMailList.Items
    '            If listitem.Selected Then
    '                If (lstEmail.Items.FindByValue(listitem.Value) Is Nothing) Then
    '                    lstEmail.Items.Insert(0, New ListItem(listitem.Text, listitem.Value))
    '                    If (strEmailList.Trim().Length = 0) Then
    '                        strEmailList = listitem.Text
    '                    Else
    '                        strEmailList += "," + listitem.Text
    '                    End If
    '                    'ChkMailList.Items.Remove(listitem.Value)
    '                End If
    '            End If
    '        Next
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '    Finally
    '        If (strEmailList.Trim().Length > 0) Then
    '            Session("EmailList") = strEmailList
    '        Else
    '            Session("EmailList") = Nothing
    '        End If
    '    End Try
    'End Sub  

    Protected Sub btnRemove_Click_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove_Click.Click
        Dim strEmailList As String = ""
        Session("EmailList") = Nothing
        Try
            Dim listitem As ListItem
            Dim listitem2 As ListItem
            Dim i As Integer
            Dim count As Integer = lstEmail.Items.Count
            For i = 0 To count - 1
                For Each listitem In lstEmail.Items
                    If listitem.Selected Then
                        lstEmail.Items.Remove(listitem.Text)
                        Exit For
                    End If
                Next
            Next i
            For Each listitem2 In lstEmail.Items
                If (strEmailList.Trim().Length = 0) Then
                    strEmailList = listitem2.Text
                Else
                    strEmailList += "," + listitem2.Text
                End If
            Next
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            If (strEmailList.Trim().Length > 0) Then
                Session("EmailList") = strEmailList
            Else
                Session("EmailList") = Nothing
            End If
        End Try
    End Sub
    Private Sub BindEmailDataByGroupId()
        If hdGroupId.Value = "" Or hdGroupId.Value = "0" Then
            Exit Sub
        End If
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim strEmailList As String = ""
        If Session("EmailList") IsNot Nothing Then
            strEmailList = Session("EmailList").ToString
        End If

        Dim i As Integer
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim listitem2 As ListItem
        Dim objbzEmmail As New AAMS.bizMaster.bzEmailGroup

        Try
            objInputXml.LoadXml("<MS_SEARCHEMAILGROUP_INPUT><GROUPDETAIL GroupName='' EmployeeID='' GroupMNC='' GroupISP='' GroupTraining='' GroupAoffice='' DepartmentID='' GroupID='' PAGE_NO='' PAGE_SIZE=''  SORT_BY='' DESC=''  /></MS_SEARCHEMAILGROUP_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupID").InnerText = hdGroupId.Value
            objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("SORT_BY").InnerText = "GroupName"
            objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("DESC").InnerText = "FALSE"


            objOutputXml = objbzEmmail.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                Dim strNewMail() As String
                strNewMail = ds.Tables("GROUP_DETAIL").Rows(0)("EmailID").Split(",")

                For i = 0 To strNewMail.Length - 1
                    If strNewMail(i).ToString().Trim().Length > 0 Then
                        If (lstEmail.Items.FindByValue(strNewMail(i).ToString().Trim()) Is Nothing) Then
                            lstEmail.Items.Insert(0, New ListItem(strNewMail(i).ToString().Trim(), strNewMail(i).ToString().Trim()))
                        End If
                    End If
                Next
            Else
                ' lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value ' Xmlnode1.SelectSingleNode("Errors/Error").Attributes("Description").InnerText.ToString()

            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            strEmailList = ""
            hdGroupId.Value = "0"
            objInputXml = Nothing
            objOutputXml = Nothing
            For Each listitem2 In lstEmail.Items
                If (strEmailList.Trim().Length = 0) Then
                    strEmailList = listitem2.Text
                Else
                    strEmailList += "," + listitem2.Text
                End If
            Next
            If (strEmailList.Trim().Length > 0) Then
                Session("EmailList") = strEmailList
            Else
                Session("EmailList") = Nothing
            End If
        End Try
    End Sub

    Protected Sub btnPrevIspDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrevIspDetails.Click
        Try
            If Request.QueryString("IspOrderId").ToString() IsNot Nothing And Request.QueryString("Lcode").ToString() IsNot Nothing Then
                ' Dim EncreptedISPOrderID As String
                ' EncreptedISPOrderID = objED.Decrypt(Request.QueryString("IspOrderId").ToString())
                Session("PrevIspDetails") = "PrevIspDetailsShow"
                '@ Getting  Decrrepted Data 
                '   Dim EncryptedLcode As String
                '  EncryptedLcode = objED.Decrypt(Request.QueryString("Lcode").ToString())

                Response.Redirect("ISPUP_Order.aspx?IspOrderId=" + Request.QueryString("IspOrderId").ToString() + "&Lcode=" + Request.QueryString("Lcode").ToString(), False)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
      

    End Sub

    Protected Sub btnIspOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIspOrder.Click

        Try

      
            'Dim EncreptedISPOrderID As String
            'EncreptedISPOrderID = objED.Decrypt(Request.QueryString("IspOrderId").ToString())

            ''@ Getting  Decrrepted Data 
            'Dim EncryptedLcode As String
            'EncryptedLcode = objED.Decrypt(Request.QueryString("Lcode").ToString())
            If Request.QueryString("IspOrderId").ToString() IsNot Nothing And Request.QueryString("Lcode").ToString() IsNot Nothing Then
                Response.Redirect("ISPUP_Order.aspx?IspOrderId=" + Request.QueryString("IspOrderId").ToString() + "&Lcode=" + Request.QueryString("Lcode").ToString(), False)
            End If

            '@ End of Getting Decrepted Data
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub BtnSendingMail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSendingMail.Click
        Try
            If Request.QueryString("IspOrderId").ToString() IsNot Nothing And Request.QueryString("Lcode").ToString() IsNot Nothing Then
                Response.Redirect("ISP_OrderSendMail.aspx?Action=U|&Id=7&IspOrderId=" + Request.QueryString("IspOrderId") + "&Lcode=" + Request.QueryString("Lcode").ToString(), False)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try



    End Sub
End Class


  

