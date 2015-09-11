Imports System.Xml
Imports System.Data
Partial Class Training_TRSR_Order
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim str As String
    Dim objED As New EncyrptDeCyrpt
    Public strMailStatus As New StringBuilder

    Enum PageOperation
        Search = 1
        Export = 2
    End Enum
    Sub loadEmailStatus()
        drpEmailStatus.Items.Clear()

        drpEmailStatus.Items.Add("--All--")
        drpEmailStatus.Items.Add("Send")
        drpEmailStatus.Items.Add("Not Send")

        'End If

    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        OrderSearch(PageOperation.Search)
    End Sub
    Sub OrderSearch(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet

        Dim str_Date_From As String = ""
        Dim str_Date_To As String = ""

        Try
            Dim objbzOrder As New AAMS.bizTraining.bzOrder
            'objInputXml.LoadXml("<UP_SEARCHORDER_INPUT>	<LCODE />	<ORDER_NUMBER />	<ORDERTYPEID />	<ORDERSTATUSID />	<REGION />	<AGENCYNAME />	<GROUPDATA />	<City />	<Country />	<MSG_SEND_DATE_FROM />	<SENDBACK_DATE_FROM />	<APPROVAL_DATE_FROM />	<RECEIVED_DATE_FROM />	<PROCESSED_DATE_FROM />	<MSG_SEND_DATE_TO />	<SENDBACK_DATE_TO />	<APPROVAL_DATE_TO />	<RECEIVED_DATE_TO />	<PROCESSED_DATE_TO />	<Limited_To_OwnAagency></Limited_To_OwnAagency><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><EmployeeID/></UP_SEARCHORDER_INPUT>")
            'objInputXml.LoadXml("<UP_SEARCHORDER_INPUT><LCODE /> <ORDER_NUMBER/> <ORDERTYPEID/> <ORDERSTATUSID /> <AOFFICE/> <REGION /><AGENCYNAME /><GROUPDATA/><City/><Country/><MSG_SEND_DATE_FROM /><SENDBACK_DATE_FROM /><APPROVAL_DATE_FROM /><RECEIVED_DATE_FROM /><PROCESSED_DATE_FROM /><MSG_SEND_DATE_TO /><SENDBACK_DATE_TO /><APPROVAL_DATE_TO /><RECEIVED_DATE_TO /><PROCESSED_DATE_TO /><Limited_To_OwnAagency /><Limited_To_Aoffice /><Limited_To_Region/><EmployeeID/><Training_Status/><Letter_sent_status/><EMAIL_SENT_DATE_FROM/><EMAIL_SENT_DATE_TO/><OFFICEID_ONLINE_DATE_FROM/><OFFICEID_ONLINE_DATE_TO/></UP_SEARCHORDER_INPUT>")
            objInputXml.LoadXml("<UP_SEARCHORDER_INPUT><LCODE/><ORDERNUMBER/><ORDERTYPEID/><ORDERSTATUSID/><AOFFICE/><REGION/><AGENCYNAME/><GROUPDATA/><CITY/><COUNTRY/><OWNAAGENCY/><SECREGIONID/><EMPLOYEEID/><Limited_To_Aoffice/><Limited_To_OwnAagency/><Limited_To_Region/><TRAINING_STATUS/><LETTER_SENT_STATUS/><EMAIL_SENT_DATE_FROM/><EMAIL_SENT_DATE_TO/><OFFICEID_ONLINE_DATE_FROM/><OFFICEID_ONLINE_DATE_TO/></UP_SEARCHORDER_INPUT>")


            With objInputXml.DocumentElement
                If Request.Form("txtAgencyName") = "" Then
                    .SelectSingleNode("LCODE").InnerText = ""
                Else
                    .SelectSingleNode("LCODE").InnerText = hdAgencyNameId.Value.Trim()
                End If

                .SelectSingleNode("ORDERNUMBER").InnerText = txtOrderNumber.Text.Trim()
                .SelectSingleNode("ORDERTYPEID").InnerText = ddlOrderType.SelectedValue.Trim()
                .SelectSingleNode("ORDERSTATUSID").InnerText = ddlOrderStatus.SelectedValue.Trim()
                .SelectSingleNode("AOFFICE").InnerText = drpAoffice.SelectedValue.Trim()
                .SelectSingleNode("REGION").InnerText = ddlRegion.SelectedValue.Trim()
                .SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text.Trim()

                If Request.Form("txtAgencyName") = "" Then
                    .SelectSingleNode("GROUPDATA").InnerText = "0"
                Else
                    .SelectSingleNode("GROUPDATA").InnerText = IIf(chbWholeGroup.Checked, "1", "0")
                End If

                If ddlCity.SelectedIndex = 0 Then
                    .SelectSingleNode("CITY").InnerText = ""
                Else
                    .SelectSingleNode("CITY").InnerText = ddlCity.SelectedItem.Text.Trim()
                End If


                If ddlCountry.SelectedIndex = 0 Then
                    .SelectSingleNode("COUNTRY").InnerText = ""
                Else
                    .SelectSingleNode("COUNTRY").InnerText = ddlCountry.SelectedItem.Text.Trim()
                End If

                Dim xDoc As New XmlDocument
                xDoc.LoadXml(Session("Security"))

                If xDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText.ToUpper() = "TRUE" Then
                    .SelectSingleNode("Limited_To_OwnAagency").InnerText = Session("LoginSession").ToString().Split("|")(0)
                Else
                    .SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                End If

                If xDoc.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText.ToUpper = "TRUE" Then
                    .SelectSingleNode("Limited_To_Aoffice").InnerText = xDoc.DocumentElement.SelectSingleNode("Aoffice").InnerText
                End If

                If xDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper() = "TRUE" Then
                    .SelectSingleNode("Limited_To_Region").InnerText = xDoc.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                End If

                If Not Session("LoginSession") Is Nothing Then
                    .SelectSingleNode("EMPLOYEEID").InnerText = Session("LoginSession").ToString().Split("|")(0)
                End If
              
                '-------------

                If ddlTrainingStatus.SelectedIndex = 0 Then
                    .SelectSingleNode("TRAINING_STATUS").InnerText = ""
                Else

                    If ddlTrainingStatus.SelectedValue = "Blank" Then  ''modify by ashish on date  18-Feb-2011
                        .SelectSingleNode("TRAINING_STATUS").InnerText = "9992"
                    Else
                        .SelectSingleNode("TRAINING_STATUS").InnerText = ddlTrainingStatus.SelectedValue.ToString()
                    End If
                End If

                '-------------
                If drpEmailStatus.SelectedValue = "--All--" Then
                    .SelectSingleNode("LETTER_SENT_STATUS").InnerText = "1"
                ElseIf drpEmailStatus.SelectedValue = "Send" Then
                    .SelectSingleNode("LETTER_SENT_STATUS").InnerText = "2"
                ElseIf drpEmailStatus.SelectedValue = "Not Send" Then
                    .SelectSingleNode("LETTER_SENT_STATUS").InnerText = "3"
                Else
                    .SelectSingleNode("LETTER_SENT_STATUS").InnerText = "1"
                End If

                '-------------

                If Not String.IsNullOrEmpty(txtEMailSentFrom.Text) Then
                    .SelectSingleNode("EMAIL_SENT_DATE_FROM").InnerText = objeAAMS.ConvertTextDate(txtEMailSentFrom.Text.Trim())
                    str_Date_From = txtEMailSentFrom.Text

                End If

                If Not String.IsNullOrEmpty(txtEMailSentTo.Text) Then
                    .SelectSingleNode("EMAIL_SENT_DATE_TO").InnerText = objeAAMS.ConvertTextDate(txtEMailSentTo.Text.Trim())
                    str_Date_To = txtEMailSentTo.Text
                End If

                '-------------

                If Not String.IsNullOrEmpty(txtofficeIDOnlineFrom.Text) Then
                    .SelectSingleNode("OFFICEID_ONLINE_DATE_FROM").InnerText = objeAAMS.ConvertTextDate(txtofficeIDOnlineFrom.Text.Trim())
                    str_Date_From = txtofficeIDOnlineFrom.Text
                End If


                If Not String.IsNullOrEmpty(txtofficeIDOnlineTo.Text) Then
                    .SelectSingleNode("OFFICEID_ONLINE_DATE_TO").InnerText = objeAAMS.ConvertTextDate(txtofficeIDOnlineTo.Text.Trim())
                    str_Date_To = txtofficeIDOnlineTo.Text
                End If

            End With

            'Here Back end Method Call
            objOutputXml = objbzOrder.Search(objInputXml)


            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If

                Session("TrainingOrderReport") = objOutputXml.OuterXml 'objOxml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=TrainingOrderReport&FDate=" + str_Date_From + "&TDate=" + str_Date_To, False)
                'Response.Redirect("~/RPSR_ReportShow.aspx?Case=AgencyAirlineBreakupReport&FDate=" + strFromDate + "&TDate=" + strToDate, False)

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl

            txtAgencyName.Attributes.Add("onblur", "return OrderWholeGroup()")

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            txtEMailSentFrom.Attributes.Add("onblur", "return validateDateSent();")
            txtEMailSentTo.Attributes.Add("onblur", "return validateDateSent();")

       
            txtofficeIDOnlineFrom.Attributes.Add("onblur", "return validateDateSent();")
            txtofficeIDOnlineTo.Attributes.Add("onblur", "return validateDateSent();")


            btnSearch.Attributes.Add("onclick", "return validateDateSent();")



            If Not Page.IsPostBack Then
                ' Load Data For Editing Mode
                objeAAMS.BindDropDown(ddlCountry, "COUNTRY", True, 3)
                objeAAMS.BindDropDown(ddlCity, "CITY", True, 3)
                objeAAMS.BindDropDown(ddlRegion, "REGION", True, 3)
                objeAAMS.BindDropDown(ddlOrderStatus, "ORDERSTATUSTRAINING_APPROVED", True, 4)
                objeAAMS.BindDropDown(ddlTrainingStatus, "TRAININGSTATUS", True, 3)

                ddlTrainingStatus.Items.Insert(1, "Blank")
                objeAAMS.BindDropDown(drpAoffice, "AOFFICE", True, 3)

                BindOrderType("2")
                loadEmailStatus()
            End If

            If Not Page.IsPostBack Then
                If Not Session("Action") Is Nothing Then
                    If Session("Action").ToString().Split("|").GetValue(0).ToString.ToUpper = "D" Then
                        txtOrderNumber.Text = Session("Action").ToString().Split("|").GetValue(2)
                        ddlOrderType.SelectedValue = Session("Action").ToString().Split("|").GetValue(3)
                        ddlOrderStatus.SelectedValue = Session("Action").ToString().Split("|").GetValue(4)
                        ddlRegion.SelectedValue = Session("Action").ToString().Split("|").GetValue(5)
                        txtAgencyName.Text = Session("Action").ToString().Split("|").GetValue(21)
                        chbWholeGroup.Checked = IIf(Session("Action").ToString().Split("|").GetValue(7) = "1", True, False)
                        ddlCity.SelectedValue = Session("Action").ToString().Split("|").GetValue(8)
                        ddlCountry.SelectedValue = Session("Action").ToString().Split("|").GetValue(9)
                        txtEMailSentFrom.Text = Session("Action").ToString().Split("|").GetValue(10)
                        txtEMailSentTo.Text = Session("Action").ToString().Split("|").GetValue(11)
                        'txtApprovalFrom.Text = Session("Action").ToString().Split("|").GetValue(12)
                        'txtApprovalTo.Text = Session("Action").ToString().Split("|").GetValue(13)
                        'txtReceivedFrom.Text = Session("Action").ToString().Split("|").GetValue(14)
                        'txtReceivedTo.Text = Session("Action").ToString().Split("|").GetValue(15)
                        'txtProcessedFrom.Text = Session("Action").ToString().Split("|").GetValue(16)
                        'txtProcessedTo.Text = Session("Action").ToString().Split("|").GetValue(17)
                        'txtSentFrom.Text = Session("Action").ToString().Split("|").GetValue(18)
                        'txtSentTo.Text = Session("Action").ToString().Split("|").GetValue(19)


                        'hdAgencyNameId.Value = Session("Action").ToString().Split("|").GetValue(20)
                        OrderSearch(PageOperation.Search)
                        lblError.Text = objeAAMSMessage.messDelete
                        Session("Action") = Nothing
                    End If
                End If
            End If
            '***************************************************************************************

            '***************************************************************************************
            'If Not Request.QueryString("Action") Is Nothing Then
            '    If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
            '        OrderDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))
            '    End If
            'End If
            '*********************Security Segment**************************************************
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    btnSearch.Enabled = False
                    Response.Redirect("~/NoRights.aspx")
                    Exit Sub
                End If
                'If strBuilder(1) = "0" Then
                '    btnNew.Enabled = False
                'End If
                'If strBuilder(4) = "0" Then
                '    btnExport.Enabled = False
                'End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            '*********************End of Security Segment*****************************************************

            ' Code for Delete 
            If (hdOrderID.Value <> "") Then
                'OrderDelete(hdOrderID.Value)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect(Request.Url.ToString, False)
    End Sub
    Sub BindOrderType(ByVal str As String)
        Dim objbzOrderType As New AAMS.bizTravelAgency.bzOrderType
        Dim objOutputXml As New XmlDocument
        Dim objInputXml As New XmlDocument
        objInputXml.LoadXml("<UP_LISTORDERTYPE_INPUT><ORDER_TYPE></ORDER_TYPE></UP_LISTORDERTYPE_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("ORDER_TYPE").InnerText = str
        Dim objNodeList As XmlNodeList
        objOutputXml = objbzOrderType.List1(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objNodeList = objOutputXml.SelectNodes("UP_LISTORDERTYPE_OUTPUT/ORDER_TYPE")
            For Each objXmlNode As XmlNode In objNodeList
                ddlOrderType.Items.Add(New ListItem(objXmlNode.Attributes("ORDER_TYPE_NAME").InnerText, objXmlNode.Attributes("ORDERTYPEID").InnerText))
            Next
        End If
        ddlOrderType.Items.Insert(0, New ListItem("--All--", ""))
    End Sub


    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            OrderSearch(PageOperation.Export)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Sub Export(ByVal objOutputXml As XmlDocument)
        Dim objExport As New ExportExcel
     
        '  Dim strArray() As String = {"Chain Code", "Lcode", "AgencyName", "Address", "City", "Region", "AOffice", "Country", "Order Type", "Order Status", "OnLine Date", "Approval Date", "OfficeID", "Installation Due Date", "EMail", "Contact Person", "Mail Sent", "Email Sent Date", "Training Status"}
        '  Dim intArray() As Integer = {28, 26, 2, 3, 6, 5, 22, 7, 9, 10, 31, 11, 13, 16, 23, 24, 25, 30, 29}

        Dim strArray() As String = {"Chain Code", "Lcode", "AgencyName", "Address", "City", "Region", "AOffice", "Country", "Order Type", "Order Status", "OnLine Date", "Approval Date", "OfficeID", "Installation Due Date", "EMail", "Contact Person", "Mail Sent", "Email Sent Date", "Training Status"}
        Dim intArray() As Integer = {27, 25, 1, 2, 5, 4, 21, 6, 8, 9, 30, 10, 12, 15, 22, 23, 24, 29, 28}

        objExport.ExportDetails(objOutputXml, "ORDERS", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportORDER.xls")
    End Sub
End Class
