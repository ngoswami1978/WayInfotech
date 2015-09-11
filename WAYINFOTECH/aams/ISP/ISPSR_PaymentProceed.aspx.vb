'##################################################################
'############   Page Name -- ISPSR_PaymentProceed  ###############
'############   Date 20-December 2007  ############################
'############   Developed By Abhishek  ############################
'##################################################################
Partial Class ISP_ISPSR_PaymentProceed
    Inherits System.Web.UI.Page
    Implements System.Web.UI.ICallbackEventHandler
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim str As String
    WithEvents chkSelect As CheckBox
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strError As String = ""
            Session("PageName") = Request.Url.ToString()
            objeAAMS.ExpirePageCache()

            btnDisplay.Attributes.Add("onclick", "return CheckValidation();")
            btnSearch.Attributes.Add("onclick", "return CheckValidation();")

            drpCity.Attributes.Add("onkeyup", "return gotop('drpCity');")
            drpCountry.Attributes.Add("onkeyup", "return gotop('drpCountry');")
            drpIspname.Attributes.Add("onkeyup", "return gotop('drpIspname');")
            txtAgencyName.Attributes.Add("onkeydown", "return ActDeAct();")
            ' btnSelectAll.Attributes.Add("onclick", "return SelectAll();")
            ' btnDeSelectAll.Attributes.Add("onclick", "return DeSelectAll();")
            '  btnReset.Attributes.Add("onclick", "return IspOrderReset();")
            lblError.Text = String.Empty
            '***************************************************************************************
            'Code of Security Check
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            gvIspPaymentRec.PageSize = ConfigurationManager.AppSettings("PAGE_SIZE").ToString

            Dim m As ClientScriptManager = Me.ClientScript
            str = m.GetCallbackEventReference(Me, "args", "ReceiveServerData", "'this is context from server'")
            Dim strCallback As String = "function CallServer(args,context){" + str + ";}"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)
            ' @ Code added for Firing event by checkbox of Grid

            For i As Integer = 0 To gvIspPaymentRec.Rows.Count - 1

                'Dim m As ClientScriptManager = Me.ClientScript
                'str = m.GetCallbackEventReference(Me, "args", "ReceiveServerData", "'this is context from server'")
                'Dim strCallback As String = "function CallServer(args,context){" + str + ";}"
                'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)
                ''     addhandler chkSelect.Checked   chkSelect_CheckedChanged
                Dim hdPANumber As HiddenField
                Dim hdRowno As HiddenField
                Dim chkPTID As CheckBox
                hdPANumber = gvIspPaymentRec.Rows(i).FindControl("hdPANumber")
                chkPTID = gvIspPaymentRec.Rows(i).FindControl("chkPTID")
                hdRowno = gvIspPaymentRec.Rows(i).FindControl("hdRowno")

                Dim id As String = chkPTID.ClientID
                If hdPANumber.Value = "" Then
                    chkPTID.Attributes.Add("onclick", "return SendAoffice('" + id + "','" + hdRowno.Value + "');")
                End If


            Next

            btnPayment.Attributes.Add("onclick", "return DoPayment('btnPayment');")

            '@ Code added for Firing event by checkbox of Grid

          

            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ISP Payment Process']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ISP Payment Process']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnSearch.Enabled = False
                    End If
                    If strBuilder(4) = "0" Then
                        btnDisplay.Enabled = False
                    End If
                    If strBuilder(1) = "0" Then
                        btnPayment.Enabled = False
                    End If

                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            If Not Page.IsPostBack Then
                Session("Checked") = Nothing
                Session("PaymentRecDataSource") = Nothing
                If Request.QueryString("PaymentDetailsofPANumber") IsNot Nothing Then
                    ' Bind The Data in PaymentReceived Details Grid

                    If Request.QueryString("Popup") Is Nothing Then
                        lnkClose.Visible = False
                    Else
                        lnkClose.Visible = True
                    End If

                    ProRec1.Text = "Received"
                    ProRec2.Text = "Received"
                    pnlGrid.Visible = False
                    pnlSearch.Visible = False
                    pnlDopaymentDetails.Visible = True
                    PaymentReceivedDetails(Request.QueryString("PaymentDetailsofPANumber").ToString(), strError)
                    If (strError.Length > 0) Then
                        lblError.Text = strError.ToString
                    End If
                Else
                    lnkClose.Visible = False
                    ProRec1.Text = "Process"
                    ProRec2.Text = "Process"
                    BindAllControl()
                End If
            End If
         
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim objInputIspOrderSearchXml, objOutputIspOrderSearchXml As New XmlDocument
        Dim objbzISPOrder As New AAMS.bizISP.bzISPOrder
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        hdMonth.Value = ""
        hdYear.Value = ""
        Dim objXmlReader As XmlNodeReader
        Try
            '<PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
            'objInputIspOrderSearchXml.LoadXml("<IS_SEARCHPAYMENTPROCEED_INPUT><LCode/><AgencyName /><City /><Country /><ISPName /><Month /><Year /><PANumber/><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency></IS_SEARCHPAYMENTPROCEED_INPUT>")
            'objInputIspOrderSearchXml.LoadXml("<IS_SEARCHPAYMENTPROCEED_INPUT><LCode/><AgencyName /><City /><Country /><ISPName /><Month /><Year /><PANumber/><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></IS_SEARCHPAYMENTPROCEED_INPUT>")

            objInputIspOrderSearchXml.LoadXml("<IS_SEARCHPAYMENTPROCEED_INPUT><LCode/><AgencyName /><City /><Country /><ISPName /><Month /><Year /><PANumber/><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><EmployeeID/></IS_SEARCHPAYMENTPROCEED_INPUT>")
            objInputIspOrderSearchXml.DocumentElement.SelectSingleNode("AgencyName").InnerText = Request("txtAgencyName")


            If Not Session("LoginSession") Is Nothing Then
                objInputIspOrderSearchXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If


            If (Request.Form("txtAgencyName") <> "" And hdAgencyName.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyName.Value = "") Then
                objInputIspOrderSearchXml.DocumentElement.SelectSingleNode("LCode").InnerText = ""
            Else
                objInputIspOrderSearchXml.DocumentElement.SelectSingleNode("LCode").InnerText = hdAgencyName.Value.Trim()

            End If
            If (drpCity.SelectedIndex <> 0) Then
                objInputIspOrderSearchXml.DocumentElement.SelectSingleNode("City").InnerText = Trim(drpCity.SelectedItem.Text)
            End If
            If (drpCountry.SelectedIndex <> 0) Then
                objInputIspOrderSearchXml.DocumentElement.SelectSingleNode("Country").InnerText = Trim(drpCountry.SelectedItem.Text)
            End If

            If (drpIspname.SelectedIndex <> 0) Then
                objInputIspOrderSearchXml.DocumentElement.SelectSingleNode("ISPName").InnerText = drpIspname.SelectedItem.Text
            End If
            objInputIspOrderSearchXml.DocumentElement.SelectSingleNode("Month").InnerText = drpMonthFrom.SelectedValue
            'objInputIspOrderReportXml.DocumentElement.SelectSingleNode("PeriodToMonth").InnerText = drpMonthTo.SelectedValue
            objInputIspOrderSearchXml.DocumentElement.SelectSingleNode("Year").InnerText = drpYearFrom.SelectedValue
            'objInputIspOrderReportXml.DocumentElement.SelectSingleNode("PeriodToYear").InnerText = drpYearTo.SelectedValue

            '<Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency>
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                'objSecurityXml.DocumentElement.SelectNodes("
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            objInputIspOrderSearchXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        Else
                            objInputIspOrderSearchXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                        End If
                    Else
                        objInputIspOrderSearchXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                    End If
                Else
                    objInputIspOrderSearchXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            objInputIspOrderSearchXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            objInputIspOrderSearchXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                        End If
                    Else
                        objInputIspOrderSearchXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                    End If
                Else
                    objInputIspOrderSearchXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        '  objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = 1
                        If Not Session("LoginSession") Is Nothing Then
                            objInputIspOrderSearchXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = Session("LoginSession").ToString().Split("|")(0)
                        End If
                    Else
                        objInputIspOrderSearchXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                    End If
                    'objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                Else
                    objInputIspOrderSearchXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                End If

                'End If

            End If
            'Here Back end Method Call
            hdChechedItem.Value = "0"

            Session("PaymentRecDataSource") = Nothing
            ddlPageNumber.SelectedValue = 1
            hdUpdateForSessionXml.Value = ""
            objOutputIspOrderSearchXml = objbzISPOrder.PaymentProceedReport(objInputIspOrderSearchXml)
            ' objOutputIspOrderSearchXml.LoadXml("<IS_SEARCHPAYMENTPROCEED_OUTPUT><PAYMENTPROCEED  ISPOrderID='126' Month='2' Year='2002' SlNo='1' UserName='34535435'  NPID ='43' CostActivityMonth='5' ISPName='Sify Mail' AgencyName='Cox ruetheruit retuertiuergf utioreytioureteriotu' Address='D-14 fjkhr 78 fjkdtkjghr jkretrt'  City='Delhi' OfficeID='54654' Status='Pending' OnlineDate='02/05/2005' CancellationDate='02/05/2006'  StartDate='' EndDate=''  DaysUsed='23' ProRate='34' ISPRentalCharges='6'  VATPercentage='5' ISPInstallationCharges='10' VATAmount='20' PANumber='' ></PAYMENTPROCEED><PAYMENTPROCEED ISPOrderID='457' Month='3' Year='2003' SlNo='2' UserName='34535435'  NPID ='41' CostActivityMonth='5' ISPName='Sify Mail' AgencyName='Cox' Address='D-14'  City='Delhi' OfficeID='54654' Status='Pending' OnlineDate='02/05/2005' CancellationDate='02/05/2006'  StartDate='' EndDate=''  DaysUsed='23' ProRate='34' ISPRentalCharges='6'  VATPercentage='5' ISPInstallationCharges='14' VATAmount='40' PANumber='44' ></PAYMENTPROCEED><PAYMENTPROCEED ISPOrderID='123' SlNo='3' UserName='34535435'  NPID ='44' CostActivityMonth='5' ISPName='Sify Mail' AgencyName='Cox' Address='D-14'  City='Delhi' OfficeID='54654' Status='Pending' OnlineDate='02/05/2005' CancellationDate='02/05/2006'  StartDate='' EndDate=''  DaysUsed='23' ProRate='34' ISPRentalCharges='6'  VATPercentage='5' ISPInstallationCharges='10' VATAmount='50' PANumber='' ></PAYMENTPROCEED><PAYMENTPROCEED  ISPOrderID='234' SlNo='4' UserName='34535435'  NPID ='34' CostActivityMonth='5' ISPName='Sify Mail' AgencyName='Cox' Address='D-14'  City='Delhi' OfficeID='54654' Status='Pending' OnlineDate='02/05/2005' CancellationDate='02/05/2006'  StartDate='' EndDate=''  DaysUsed='23' ProRate='34' ISPRentalCharges='6'  VATPercentage='5' ISPInstallationCharges='10' VATAmount='70' PANumber='67' ></PAYMENTPROCEED><Errors Status='FALSE'><Error Code='' Description='' /></Errors></IS_SEARCHPAYMENTPROCEED_OUTPUT>")

            If objOutputIspOrderSearchXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                hdUpdateForSessionXml.Value = objOutputIspOrderSearchXml.OuterXml

                For Each objNode As XmlNode In objOutputIspOrderSearchXml.DocumentElement.SelectNodes("PAYMENTPROCEED")
                    objNode.Attributes("CostActivityMonth").Value = drpMonthFrom.SelectedValue
                Next
                objXmlReader = New XmlNodeReader(objOutputIspOrderSearchXml)
                ds.ReadXml(objXmlReader)


                '@ Added Code for Sorting and Paging
                Dim dsets As New DataSet
                dsets = ds
                Session("PaymentRecDataSource") = ds
                Dim Clmn As String = ""
                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                End If
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "NPID"
                    If ViewState("Desc") = "FALSE" Then
                        bubbleSortAsc(ds, ViewState("SortName"), dsets)
                    Else
                        bubbleSortDesc(ds, ViewState("SortName"), dsets)
                    End If
                Else
                    If ViewState("Desc") = "FALSE" Then
                        bubbleSortAsc(ds, ViewState("SortName"), dsets)
                    Else
                        bubbleSortDesc(ds, ViewState("SortName"), dsets)
                    End If
                End If

                gvIspPaymentRec.PageIndex = 0

                gvIspPaymentRec.DataSource = Nothing
                gvIspPaymentRec.DataBind()

                '@ Added Code for Sorting and Paging

                gvIspPaymentRec.DataSource = ds.Tables("PAYMENTPROCEED")
                gvIspPaymentRec.DataBind()
                btnSelectAll.Visible = True
                btnPayment.Visible = True
                btnDeSelectAll.Visible = False
                hdMonth.Value = drpMonthFrom.SelectedValue
                hdYear.Value = drpYearFrom.SelectedValue
                gvIspPaymentRec.HeaderRow.Cells(15).Text = "(VAT " + ds.Tables("PAYMENTPROCEED").Rows(0)("VATPercentage").ToString + " )"
                gvIspPaymentRec.HeaderRow.Cells(17).Text = "(VAT " + ds.Tables("PAYMENTPROCEED").Rows(0)("VATPercentage").ToString + " )"

                txtRecordCount.Text = objOutputIspOrderSearchXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                SetImageForSorting(gvIspPaymentRec)
                pnlPaging.Visible = True
                BindControlsForNavigation(gvIspPaymentRec.PageCount)

            Else
                gvIspPaymentRec.DataSource = Nothing
                gvIspPaymentRec.DataBind()
                btnSelectAll.Visible = False
                btnPayment.Visible = False
                btnDeSelectAll.Visible = False
                hdMonth.Value = ""
                hdYear.Value = ""
                lblError.Text = objOutputIspOrderSearchXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
                hdUpdateForSessionXml.Value = ""
            End If
        Catch ex As Exception
            lblError.Text = ex.Message

        Finally
            objInputIspOrderSearchXml = Nothing
            objOutputIspOrderSearchXml = Nothing
            objbzISPOrder = Nothing
        End Try
    End Sub
    Protected Sub btnDisplay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisplay.Click
        Dim objInputIspOrderReportXml, objOutputIspOrderReportXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        Dim objbzISPOrder As New AAMS.bizISP.bzISPOrder
        Try
            '<PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>

            'objInputIspOrderReportXml.LoadXml("<IS_SEARCHPAYMENTPROCEED_INPUT><AgencyName /><City /><Country /><ISPName /><PeriodFromMonth /><PeriodFromYear /><PeriodToMonth /><PeriodToYear /></IS_SEARCHPAYMENTPROCEED_INPUT>")
            'objInputIspOrderReportXml.LoadXml("<RP_PAYMENTPROCEED_INPUT><LCode/><AgencyName /><City /><Country /><ISPName /><Month /><Year /><PANumber/><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency></RP_PAYMENTPROCEED_INPUT>")
            'objInputIspOrderReportXml.LoadXml("<RP_PAYMENTPROCEED_INPUT><LCode/><AgencyName /><City /><Country /><ISPName /><Month /><Year /><PANumber/><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></RP_PAYMENTPROCEED_INPUT>")

            objInputIspOrderReportXml.LoadXml("<RP_PAYMENTPROCEED_INPUT><LCode/><AgencyName /><City /><Country /><ISPName /><Month /><Year /><PANumber/><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><EmployeeID/></RP_PAYMENTPROCEED_INPUT>")


            If Not Session("LoginSession") Is Nothing Then
                objInputIspOrderReportXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If

            If (Request.Form("txtAgencyName") <> "" And hdAgencyName.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyName.Value = "") Then
                objInputIspOrderReportXml.DocumentElement.SelectSingleNode("LCode").InnerText = ""
            Else
                objInputIspOrderReportXml.DocumentElement.SelectSingleNode("LCode").InnerText = hdAgencyName.Value.Trim()

            End If
            objInputIspOrderReportXml.DocumentElement.SelectSingleNode("AgencyName").InnerText = Request("txtAgencyName")
            If (drpCity.SelectedIndex <> 0) Then
                objInputIspOrderReportXml.DocumentElement.SelectSingleNode("City").InnerText = Trim(drpCity.SelectedItem.Text)
            End If
            If (drpCountry.SelectedIndex <> 0) Then
                objInputIspOrderReportXml.DocumentElement.SelectSingleNode("Country").InnerText = Trim(drpCountry.SelectedItem.Text)
            End If

            If (drpIspname.SelectedIndex <> 0) Then
                objInputIspOrderReportXml.DocumentElement.SelectSingleNode("ISPName").InnerText = drpIspname.SelectedItem.Text
            End If
            objInputIspOrderReportXml.DocumentElement.SelectSingleNode("Month").InnerText = drpMonthFrom.SelectedValue
            'objInputIspOrderReportXml.DocumentElement.SelectSingleNode("PeriodToMonth").InnerText = drpMonthTo.SelectedValue
            objInputIspOrderReportXml.DocumentElement.SelectSingleNode("Year").InnerText = drpYearFrom.SelectedValue
            'objInputIspOrderReportXml.DocumentElement.SelectSingleNode("PeriodToYear").InnerText = drpYearTo.SelectedValue

            '<Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency>
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                'objSecurityXml.DocumentElement.SelectNodes("
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            objInputIspOrderReportXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        Else
                            objInputIspOrderReportXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                        End If
                    Else
                        objInputIspOrderReportXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                    End If
                Else
                    objInputIspOrderReportXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            objInputIspOrderReportXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            objInputIspOrderReportXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                        End If
                    Else
                        objInputIspOrderReportXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                    End If
                Else
                    objInputIspOrderReportXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        '  objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = 1
                        If Not Session("LoginSession") Is Nothing Then
                            objInputIspOrderReportXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = Session("LoginSession").ToString().Split("|")(0)
                        End If
                    Else
                        objInputIspOrderReportXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                    End If
                    'objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                Else
                    objInputIspOrderReportXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                End If

                'End If

            End If

            'Here Back end Method Call
            'objOutputIspOrderReportXml = objbzISPOrder.PaymentProceedReport(objInputIspOrderReportXml)
            objOutputIspOrderReportXml = objbzISPOrder.PaymentProceedReport(objInputIspOrderReportXml)
            'objOutputIspOrderReportXml.LoadXml("<IS_SEARCHPAYMENTPROCEED_OUTPUT><PAYMENTPROCEED  SlNo='1' UserName='34535435'  NPID ='43' CostActivityMonth='5' ISPName='Sify Mail' AgencyName='Cox' Address='D-14'  City='Delhi' OfficeID='54654' Status='Pending' OnlineDate='02/05/2005' CancellationDate='02/05/2006'  StartDate='' EndDate=''  DaysUsed='23' ProRate='34' ISPRentalCharges='6'  VATPercentage='5' ISPInstallationCharges='10' VATAmount='20' PTID='' ></PAYMENTPROCEED><PAYMENTPROCEED  SlNo='2' UserName='34535435'  NPID ='41' CostActivityMonth='5' ISPName='Sify Mail' AgencyName='Cox' Address='D-14'  City='Delhi' OfficeID='54654' Status='Pending' OnlineDate='02/05/2005' CancellationDate='02/05/2006'  StartDate='' EndDate=''  DaysUsed='23' ProRate='34' ISPRentalCharges='6'  VATPercentage='5' ISPInstallationCharges='14' VATAmount='40' PTID='44' ></PAYMENTPROCEED><PAYMENTPROCEED  SlNo='3' UserName='34535435'  NPID ='44' CostActivityMonth='5' ISPName='Sify Mail' AgencyName='Cox' Address='D-14'  City='Delhi' OfficeID='54654' Status='Pending' OnlineDate='02/05/2005' CancellationDate='02/05/2006'  StartDate='' EndDate=''  DaysUsed='23' ProRate='34' ISPRentalCharges='6'  VATPercentage='5' ISPInstallationCharges='10' VATAmount='50' PTID='' ></PAYMENTPROCEED><PAYMENTPROCEED  SlNo='4' UserName='34535435'  NPID ='34' CostActivityMonth='5' ISPName='Sify Mail' AgencyName='Cox' Address='D-14'  City='Delhi' OfficeID='54654' Status='Pending' OnlineDate='02/05/2005' CancellationDate='02/05/2006'  StartDate='' EndDate=''  DaysUsed='23' ProRate='34' ISPRentalCharges='6'  VATPercentage='5' ISPInstallationCharges='10' VATAmount='70' PTID='67' ></PAYMENTPROCEED><Errors Status='FALSE'><Error Code='' Description='' /></Errors></IS_SEARCHPAYMENTPROCEED_OUTPUT>")
            If objOutputIspOrderReportXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                For Each objNode As XmlNode In objOutputIspOrderReportXml.DocumentElement.SelectNodes("PAYMENTPROCEED")
                    objNode.Attributes("CostActivityMonth").Value = drpMonthFrom.SelectedItem.Text
                Next
                Session("ISPPaymentProceed") = objOutputIspOrderReportXml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=ISPPaymentProceed", False)
            Else


                gvIspPaymentRec.DataSource = Nothing
                gvIspPaymentRec.DataBind()
                btnSelectAll.Visible = False
                btnPayment.Visible = False
                btnDeSelectAll.Visible = False
                hdMonth.Value = ""
                hdYear.Value = ""

                lblError.Text = objOutputIspOrderReportXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message

        Finally
            objInputIspOrderReportXml = Nothing
            objOutputIspOrderReportXml = Nothing
            objbzISPOrder = Nothing
        End Try
    End Sub

    Private Sub BindAllControl()
        Try
            Dim i, j As Integer
            objeAAMS.BindDropDown(drpCity, "CITY", True, 3)
            objeAAMS.BindDropDown(drpCountry, "COUNTRY", True, 3)
            objeAAMS.BindDropDown(drpIspname, "ISPLIST", True, 3)
            i = 0
            'For j = 1 To 12
            '    drpMonthFrom.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
            '    drpMonthTo.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
            '    i += 1
            'Next
            'drpMonthFrom.SelectedIndex = 0
            'drpMonthTo.SelectedIndex = 0
            'i = 0
            For j = DateTime.Now.Year To 1990 Step -1
                drpYearFrom.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                '  drpYearTo.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                i += 1
            Next
            drpYearFrom.SelectedValue = DateTime.Now.Year
            drpMonthFrom.SelectedValue = DateTime.Now.Month
            '  drpYearTo.SelectedValue = DateTime.Now.Year
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            'txtAgencyName.Text = ""
            'drpCity.SelectedIndex = 0
            'drpCountry.SelectedIndex = 0
            'drpIspname.SelectedIndex = 0
            'drpMonthFrom.SelectedValue = DateTime.Now.Month
            ''drpMonthTo.SelectedValue = DateTime.Now.Month
            'drpYearFrom.SelectedValue = DateTime.Now.Year
            '' drpYearTo.SelectedValue = DateTime.Now.Year
            'gvIspPaymentRec.DataSource = Nothing
            'gvIspPaymentRec.DataBind()
            'btnDeSelectAll.Visible = False
            'btnSelectAll.Visible = False
            'btnPayment.Visible = False
            Response.Redirect("ISPSR_PaymentProceed.aspx", False)
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub gvIspPaymentRec_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvIspPaymentRec.RowDataBound
        Try

            If (e.Row.RowIndex < 0) Then
                Exit Sub
            End If
            Dim hdPANumber As HiddenField
            Dim hdRowno As HiddenField
            Dim chkPTID As CheckBox
            hdPANumber = e.Row.FindControl("hdPANumber")
            chkPTID = e.Row.FindControl("chkPTID")
            Dim hdCheckUncheckStatus As HiddenField
            hdCheckUncheckStatus = e.Row.FindControl("hdCheckUncheckStatus")

            hdRowno = e.Row.FindControl("hdRowno")

            If hdPANumber.Value = "" Then
                chkPTID.Visible = True
                ' chkPTID.Attributes.Add("onclick", "return SelectAndUpdateSession();")
                Dim id As String
                id = chkPTID.ClientID
                If hdCheckUncheckStatus.Value = "" Or hdCheckUncheckStatus.Value = "False" Then
                    chkPTID.Checked = False
                Else
                    chkPTID.Checked = True
                End If
                chkPTID.Attributes.Add("onclick", "return SendAoffice('" + id + "','" + hdRowno.Value + "');")
                ' chkPTID.Attributes.Add("onclick", "return SendAoffice('" + id + "');")
            Else
                chkPTID.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        Try
            Dim objXmlReader As XmlNodeReader
            Dim ds2 As New DataSet
            Dim objOutputIspOrderSearchXml As New XmlDocument

            Dim chkPTID As CheckBox
            Dim hdPANumber As HiddenField
            Dim i As Integer
            If (gvIspPaymentRec.Rows.Count <= 0) Then
                lblError.Text = "There is no No Row For Selection."
            End If
            For i = 0 To gvIspPaymentRec.Rows.Count - 1

                hdPANumber = gvIspPaymentRec.Rows(i).FindControl("hdPANumber")
                chkPTID = gvIspPaymentRec.Rows(i).FindControl("chkPTID")
                If hdPANumber.Value = "" Then
                    chkPTID.Visible = True
                    chkPTID.Checked = True
                Else
                    chkPTID.Visible = False
                    chkPTID.Checked = False
                End If
            Next

            btnSelectAll.Visible = False
            btnDeSelectAll.Visible = True

            '@Now Update the SessionDataource as well as  hdUpdateForSessionXml

            If Session("PaymentRecDataSource") IsNot Nothing Then
                Dim dset As DataSet = CType(Session("PaymentRecDataSource"), DataSet)

                hdUpdateForSessionXml.Value = dset.GetXml()

                If hdUpdateForSessionXml.Value <> "" Then
                    '  gvIspPaymentRec.PageIndex = CInt(ddlPageNumber.SelectedValue) - 1
                    objOutputIspOrderSearchXml.LoadXml(hdUpdateForSessionXml.Value)
                    For Each objxmlnode As XmlNode In objOutputIspOrderSearchXml.DocumentElement.SelectNodes("PAYMENTPROCEED")
                        objxmlnode.Attributes("CheckUncheckStatus").Value = "True"
                    Next
                    hdUpdateForSessionXml.Value = objOutputIspOrderSearchXml.OuterXml
                    objOutputIspOrderSearchXml.LoadXml(hdUpdateForSessionXml.Value)
                    objXmlReader = New XmlNodeReader(objOutputIspOrderSearchXml)
                    ds2.ReadXml(objXmlReader)
                    Session("PaymentRecDataSource") = ds2

                    If objOutputIspOrderSearchXml.DocumentElement.SelectNodes("PAYMENTPROCEED[@CheckUncheckStatus ='True']").Count > 0 Then
                        Session("Checked") = "TRUE"
                        hdChechedItem.Value = "1"
                    Else
                        Session("Checked") = "FALSE"
                        hdChechedItem.Value = "0"
                    End If


                End If

                SetImageForSorting(gvIspPaymentRec)

            End If
            '@


        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnDeSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeSelectAll.Click

        Dim objXmlReader As XmlNodeReader
        Dim ds2 As New DataSet
        Dim objOutputIspOrderSearchXml As New XmlDocument
        Dim chkPTID As CheckBox
        Dim hdPANumber As HiddenField
        Dim i As Integer
        Try
            If (gvIspPaymentRec.Rows.Count <= 0) Then
                lblError.Text = "There is no No Row For DeSelection."
            End If
            For i = 0 To gvIspPaymentRec.Rows.Count - 1

                hdPANumber = gvIspPaymentRec.Rows(i).FindControl("hdPANumber")
                chkPTID = gvIspPaymentRec.Rows(i).FindControl("chkPTID")
                If hdPANumber.Value = "" Then
                    chkPTID.Visible = True
                    chkPTID.Checked = False
                Else
                    chkPTID.Visible = False
                    chkPTID.Checked = False
                End If
            Next
            btnDeSelectAll.Visible = False
            btnSelectAll.Visible = True

            '@Now Update the SessionDataource as well as  hdUpdateForSessionXml

            If Session("PaymentRecDataSource") IsNot Nothing Then
                Dim dset As DataSet = CType(Session("PaymentRecDataSource"), DataSet)

                hdUpdateForSessionXml.Value = dset.GetXml()

                If hdUpdateForSessionXml.Value <> "" Then
                    '  gvIspPaymentRec.PageIndex = CInt(ddlPageNumber.SelectedValue) - 1
                    objOutputIspOrderSearchXml.LoadXml(hdUpdateForSessionXml.Value)
                    For Each objxmlnode As XmlNode In objOutputIspOrderSearchXml.DocumentElement.SelectNodes("PAYMENTPROCEED")
                        objxmlnode.Attributes("CheckUncheckStatus").Value = ""
                    Next
                    hdUpdateForSessionXml.Value = objOutputIspOrderSearchXml.OuterXml
                    objOutputIspOrderSearchXml.LoadXml(hdUpdateForSessionXml.Value)
                    objXmlReader = New XmlNodeReader(objOutputIspOrderSearchXml)
                    ds2.ReadXml(objXmlReader)
                    Session("PaymentRecDataSource") = ds2
                    Session("Checked") = "FALSE"
                    hdChechedItem.Value = "0"
                End If
                SetImageForSorting(gvIspPaymentRec)
            End If
            '@

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    ' Protected Sub btnPayment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPayment.Click
    'CallDoPayment()
    ' End Sub

    'Private Sub Dopayment()
    '    Try
    '        Dim chkPTID As CheckBox
    '        Dim hdPANumber As HiddenField
    '        Dim hdAmt As HiddenField
    '        Dim hdISPOrderID As HiddenField
    '        Dim hdMonths As HiddenField
    '        Dim hdYears As HiddenField

    '        Dim strISPOrderID As String = ""
    '        Dim i As Integer
    '        Dim TotalAmt As Decimal = 0
    '        If (gvIspPaymentRec.Rows.Count > 0) Then
    '            For i = 0 To gvIspPaymentRec.Rows.Count - 1
    '                hdMonths = gvIspPaymentRec.Rows(i).FindControl("hdMonths")
    '                hdYears = gvIspPaymentRec.Rows(i).FindControl("hdYears")

    '                hdISPOrderID = gvIspPaymentRec.Rows(i).FindControl("hdISPOrderID")

    '                hdPANumber = gvIspPaymentRec.Rows(i).FindControl("hdPANumber")
    '                chkPTID = gvIspPaymentRec.Rows(i).FindControl("chkPTID")
    '                hdAmt = gvIspPaymentRec.Rows(i).FindControl("hdAmt")

    '                If hdPANumber.Value = "" Then
    '                    chkPTID.Visible = True
    '                    If chkPTID.Checked = True Then
    '                        If (hdAmt.Value.Length > 0) Then
    '                            TotalAmt += Convert.ToDecimal(hdAmt.Value)
    '                            If (strISPOrderID.Trim().Length = 0) Then
    '                                strISPOrderID = hdISPOrderID.Value
    '                            Else
    '                                strISPOrderID += "|" + hdISPOrderID.Value
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '            Next

    '            If (strISPOrderID.Trim().Length = 0) Then
    '                lblError.Text = "There is no item selected to do Payment."
    '                Exit Sub
    '            End If

    '            ClientScript.RegisterStartupScript(Me.GetType(), "Open1", "<script language='javascript'>" & _
    '                                 "PopupWindow=window.open('ISPUP_PaymentReceived.aspx?TotalAmt=" & TotalAmt & "&ISPOrderID=" & strISPOrderID & "&Month=" & hdMonth.Value & "&Year=" & hdYear.Value & "','IspPay','height=400px,width=830px,top=150,left=150,scrollbars=0,status=1')" & _
    '                                 ";PopupWindow.focus();</script>")

    '            'Page.RegisterClientScriptBlock("Open1", "<script language='javascript'>" & _
    '            '"parent.location.href='ISPUP_PaymentReceived.aspx?TotalAmt=" & TotalAmt & "',null,'height=600,width=880,top=30,left=20,scrollbars=1,resizable=1')" & _
    '            '"</script>")
    '        Else
    '            lblError.Text = "There is no row to do Payment."
    '        End If

    '    Catch ex As Exception
    '        lblError.Text = ex.Message.ToString
    '    End Try
    'End Sub
    Private Sub PaymentReceivedDetails(ByVal PaymentDetailsofPANumber As String, ByRef strError As String)
        Dim objInputPaymentDetailsXml, objOutputIPaymentDetailsXml As New XmlDocument
        Dim objbzISPOrder As New AAMS.bizISP.bzISPOrder
        Dim ds As New DataSet

        Dim objXmlReader As XmlNodeReader
        Try
            '<PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
            objInputPaymentDetailsXml.LoadXml("<IS_SEARCHPAYMENTPROCEED_INPUT><LCode/><AgencyName /><City /><Country /><ISPName /><Month /><Year /><PANumber/><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></IS_SEARCHPAYMENTPROCEED_INPUT>")
            ' objInputPaymentDetailsXml.LoadXml("<IS_SEARCHPAYMENTPROCEED_INPUT><LCode/><AgencyName /><City /><Country /><ISPName /><Month /><Year /><PANumber/></IS_SEARCHPAYMENTPROCEED_INPUT>")
            objInputPaymentDetailsXml.DocumentElement.SelectSingleNode("PANumber").InnerText = PaymentDetailsofPANumber

            If (Request.QueryString("PMonth") IsNot Nothing) Then
                objInputPaymentDetailsXml.DocumentElement.SelectSingleNode("Month").InnerText = Request.QueryString("PMonth").ToString
            End If
            If (Request.QueryString("PYear") IsNot Nothing) Then
                objInputPaymentDetailsXml.DocumentElement.SelectSingleNode("Year").InnerText = Request.QueryString("PYear").ToString
            End If

            'Here Back end Method Call
            objOutputIPaymentDetailsXml = objbzISPOrder.PaymentProceedReport(objInputPaymentDetailsXml)
            ' objOutputIspOrderSearchXml.LoadXml("<IS_SEARCHPAYMENTPROCEED_OUTPUT><PAYMENTPROCEED  ISPOrderID='126' SlNo='1' UserName='34535435'  NPID ='43' CostActivityMonth='5' ISPName='Sify Mail' AgencyName='Cox' Address='D-14'  City='Delhi' OfficeID='54654' Status='Pending' OnlineDate='02/05/2005' CancellationDate='02/05/2006'  StartDate='' EndDate=''  DaysUsed='23' ProRate='34' ISPRentalCharges='6'  VATPercentage='5' ISPInstallationCharges='10' VATAmount='20' PANumber='' ></PAYMENTPROCEED><PAYMENTPROCEED ISPOrderID='457' SlNo='2' UserName='34535435'  NPID ='41' CostActivityMonth='5' ISPName='Sify Mail' AgencyName='Cox' Address='D-14'  City='Delhi' OfficeID='54654' Status='Pending' OnlineDate='02/05/2005' CancellationDate='02/05/2006'  StartDate='' EndDate=''  DaysUsed='23' ProRate='34' ISPRentalCharges='6'  VATPercentage='5' ISPInstallationCharges='14' VATAmount='40' PANumber='44' ></PAYMENTPROCEED><PAYMENTPROCEED ISPOrderID='123' SlNo='3' UserName='34535435'  NPID ='44' CostActivityMonth='5' ISPName='Sify Mail' AgencyName='Cox' Address='D-14'  City='Delhi' OfficeID='54654' Status='Pending' OnlineDate='02/05/2005' CancellationDate='02/05/2006'  StartDate='' EndDate=''  DaysUsed='23' ProRate='34' ISPRentalCharges='6'  VATPercentage='5' ISPInstallationCharges='10' VATAmount='50' PTID='' ></PAYMENTPROCEED><PAYMENTPROCEED  ISPOrderID='234' SlNo='4' UserName='34535435'  NPID ='34' CostActivityMonth='5' ISPName='Sify Mail' AgencyName='Cox' Address='D-14'  City='Delhi' OfficeID='54654' Status='Pending' OnlineDate='02/05/2005' CancellationDate='02/05/2006'  StartDate='' EndDate=''  DaysUsed='23' ProRate='34' ISPRentalCharges='6'  VATPercentage='5' ISPInstallationCharges='10' VATAmount='70' PTID='67' ></PAYMENTPROCEED><Errors Status='FALSE'><Error Code='' Description='' /></Errors></IS_SEARCHPAYMENTPROCEED_OUTPUT>")

            If objOutputIPaymentDetailsXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If (Request.QueryString("PMonth") IsNot Nothing) Then
                    'For Each objNode As XmlNode In objOutputIPaymentDetailsXml.DocumentElement.SelectNodes("PAYMENTPROCEED")
                    '    objNode.Attributes("CostActivityMonth").Value = MonthName(Val(Request.QueryString("PMonth")))
                    'Next
                End If


                pnlDopaymentDetails.Visible = True
                objXmlReader = New XmlNodeReader(objOutputIPaymentDetailsXml)
                ds.ReadXml(objXmlReader)
                GvDopaymentDetails.DataSource = ds.Tables("PAYMENTPROCEED")
                GvDopaymentDetails.DataBind()

                GvDopaymentDetails.HeaderRow.Cells(14).Text = "(VAT " + ds.Tables("PAYMENTPROCEED").Rows(0)("VATPercentage").ToString + " )"
                GvDopaymentDetails.HeaderRow.Cells(16).Text = "(VAT " + ds.Tables("PAYMENTPROCEED").Rows(0)("VATPercentage").ToString + " )"


            Else
                GvDopaymentDetails.DataSource = Nothing
                GvDopaymentDetails.DataBind()
                pnlDopaymentDetails.Visible = False
                lblError.Text = objOutputIPaymentDetailsXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            strError = ex.Message
            pnlDopaymentDetails.Visible = False
        Finally
            objInputPaymentDetailsXml = Nothing
            objOutputIPaymentDetailsXml = Nothing
            objbzISPOrder = Nothing
        End Try
    End Sub
#Region "Code for Filter "
    'Code Added by Abhishek
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    'Code Added by Abhishek
#End Region


#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Dim objXmlReader As XmlNodeReader
        Dim ds2 As New DataSet
        Dim objOutputIspOrderSearchXml As New XmlDocument
        Try
            lblError.Text = ""
            'If hdUpdateForSessionXml.Value <> "" Then
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If

            ' grdvAirlineProductivityCom.PageIndex = ddlPageNumber.SelectedValue
            '  grdvAirlineProductivityCom.PageIndex = ddlPageNumber.SelectedValue - 1
            gvIspPaymentRec.PageIndex = CInt(ddlPageNumber.SelectedValue) - 1

            'objOutputIspOrderSearchXml.LoadXml(hdUpdateForSessionXml.Value)
            'objXmlReader = New XmlNodeReader(objOutputIspOrderSearchXml)
            'ds2.ReadXml(objXmlReader)
            'Session("PaymentRecDataSource") = ds2

            If Session("PaymentRecDataSource") IsNot Nothing Then
                '  Dim dv As New DataView
                Dim ds As DataSet = CType(Session("PaymentRecDataSource"), DataSet)
                ' Dim dv As DataView
                Dim dsets As New DataSet
                dsets = ds
                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                End If
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "NPID" '"VALUE"
                    If ViewState("Desc") = "FALSE" Then
                        bubbleSortAsc(ds, ViewState("SortName"), dsets)
                    Else
                        bubbleSortDesc(ds, ViewState("SortName"), dsets)
                    End If
                Else
                    If ViewState("Desc") = "FALSE" Then
                        bubbleSortAsc(ds, ViewState("SortName"), dsets)
                    Else
                        bubbleSortDesc(ds, ViewState("SortName"), dsets)
                    End If
                End If
                gvIspPaymentRec.DataSource = ds.Tables("PAYMENTPROCEED") 'dv
                gvIspPaymentRec.DataBind()
                SetImageForSorting(gvIspPaymentRec)
                gvIspPaymentRec.HeaderRow.Cells(15).Text = "(VAT " + ds.Tables("PAYMENTPROCEED").Rows(0)("VATPercentage").ToString + " )"
                gvIspPaymentRec.HeaderRow.Cells(17).Text = "(VAT " + ds.Tables("PAYMENTPROCEED").Rows(0)("VATPercentage").ToString + " )"

                ' @ Code For Totals in Footer
                Dim intRow, IntColno As Integer

                ' @ End ofCode For Totals in Footer
            End If
            BindControlsForNavigation(gvIspPaymentRec.PageCount)
            'End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Dim objXmlReader As XmlNodeReader
        Dim ds2 As New DataSet
        Dim objOutputIspOrderSearchXml As New XmlDocument

        Try
            lblError.Text = ""
            '  If hdUpdateForSessionXml.Value <> "" Then

            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If

            ' grdvAirlineProductivityCom.PageIndex = ddlPageNumber.SelectedValue
            '  grdvAirlineProductivityCom.PageIndex = ddlPageNumber.SelectedValue - 1
            gvIspPaymentRec.PageIndex = CInt(ddlPageNumber.SelectedValue) - 1

            'objOutputIspOrderSearchXml.LoadXml(hdUpdateForSessionXml.Value)
            'objXmlReader = New XmlNodeReader(objOutputIspOrderSearchXml)
            'ds2.ReadXml(objXmlReader)
            'Session("PaymentRecDataSource") = ds2

            If Session("PaymentRecDataSource") IsNot Nothing Then
                '  Dim dv As New DataView
                Dim ds As DataSet = CType(Session("PaymentRecDataSource"), DataSet)

                ' Dim dv As DataView
                Dim dsets As New DataSet
                dsets = ds
                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                End If
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "NPID" '"VALUE"
                    If ViewState("Desc") = "FALSE" Then
                        bubbleSortAsc(ds, ViewState("SortName"), dsets)
                    Else
                        bubbleSortDesc(ds, ViewState("SortName"), dsets)
                    End If
                Else
                    If ViewState("Desc") = "FALSE" Then
                        bubbleSortAsc(ds, ViewState("SortName"), dsets)
                    Else
                        bubbleSortDesc(ds, ViewState("SortName"), dsets)
                    End If
                End If
                gvIspPaymentRec.DataSource = ds.Tables("PAYMENTPROCEED") 'dv
                gvIspPaymentRec.DataBind()
                SetImageForSorting(gvIspPaymentRec)
                gvIspPaymentRec.HeaderRow.Cells(15).Text = "(VAT " + ds.Tables("PAYMENTPROCEED").Rows(0)("VATPercentage").ToString + " )"
                gvIspPaymentRec.HeaderRow.Cells(17).Text = "(VAT " + ds.Tables("PAYMENTPROCEED").Rows(0)("VATPercentage").ToString + " )"


            End If
            BindControlsForNavigation(gvIspPaymentRec.PageCount)
            ' End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Dim objXmlReader As XmlNodeReader
        Dim ds2 As New DataSet
        Dim objOutputIspOrderSearchXml As New XmlDocument
        Try

            lblError.Text = ""
            'If hdUpdateForSessionXml.Value <> "" Then
            gvIspPaymentRec.PageIndex = CInt(ddlPageNumber.SelectedValue) - 1


            '    objOutputIspOrderSearchXml.LoadXml(hdUpdateForSessionXml.Value)
            '    objXmlReader = New XmlNodeReader(objOutputIspOrderSearchXml)
            '    ds2.ReadXml(objXmlReader)
            '    Session("PaymentRecDataSource") = ds2

            If Session("PaymentRecDataSource") IsNot Nothing Then
                '  Dim dv As New DataView
                Dim ds As DataSet = CType(Session("PaymentRecDataSource"), DataSet)

                ' Dim dv As DataView
                Dim dsets As New DataSet
                dsets = ds
                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                End If
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "NPID" '"VALUE"
                  
                    If ViewState("Desc") = "FALSE" Then
                        bubbleSortAsc(ds, ViewState("SortName"), dsets)
                    Else
                        bubbleSortDesc(ds, ViewState("SortName"), dsets)
                    End If
                Else
                    If ViewState("Desc") = "FALSE" Then
                        bubbleSortAsc(ds, ViewState("SortName"), dsets)
                    Else
                        bubbleSortDesc(ds, ViewState("SortName"), dsets)
                    End If
                End If
                gvIspPaymentRec.DataSource = ds.Tables("PAYMENTPROCEED") 'dv
                gvIspPaymentRec.DataBind()

                SetImageForSorting(gvIspPaymentRec)
                gvIspPaymentRec.HeaderRow.Cells(15).Text = "(VAT " + ds.Tables("PAYMENTPROCEED").Rows(0)("VATPercentage").ToString + " )"
                gvIspPaymentRec.HeaderRow.Cells(17).Text = "(VAT " + ds.Tables("PAYMENTPROCEED").Rows(0)("VATPercentage").ToString + " )"


            End If
            BindControlsForNavigation(gvIspPaymentRec.PageCount)
            'End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub



    Private Sub BindControlsForNavigation(ByVal CrrentPageNo As String)
        ' ##################################################################
        '@ Code Added For Paging And Sorting
        ' ###################################################################
        pnlPaging.Visible = True
        '  Dim count As Integer = 0
        Dim count As Integer = CInt(CrrentPageNo)
        Dim selectedValue As String = IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue)
        If count <> ddlPageNumber.Items.Count Then
            ddlPageNumber.Items.Clear()
            For i As Integer = 1 To count
                ddlPageNumber.Items.Add(i.ToString)
            Next
        End If
        ddlPageNumber.SelectedValue = selectedValue
        'Code for hiding prev and next button based on count
        If count = 1 Then
            lnkNext.Visible = False
            lnkPrev.Visible = False
        Else

            lnkPrev.Visible = True
            lnkNext.Visible = True
        End If

        'Code for hiding next button when pagenumber is equal to page count
        If ddlPageNumber.SelectedValue = count.ToString Then
            lnkNext.Visible = False
        Else
            lnkNext.Visible = True
        End If

        'Code for hiding prev button when pagenumber is 1
        If ddlPageNumber.SelectedValue = "1" Then
            lnkPrev.Visible = False
        Else
            lnkPrev.Visible = True
        End If

        ' ###################################################################
        '@ End of Code Added For Paging And Sorting 
        ' ###################################################################
    End Sub


    Private Sub bubbleSortAsc(ByRef dset As DataSet, ByVal Clmn As String, ByVal dset2 As DataSet)
        '  If Clmn = "VALUE" Then
        If Clmn = "ISPRentalCharges" Or Clmn = "VATAmount" Or Clmn = "ProRate" Or Clmn = "VATPercentage" Or Clmn = "ISPInstallationCharges" Or Clmn = "VATAmount" Or Clmn = "I_VAT" Or Clmn = "Amount" Then
            Dim i, j As Integer
            Dim SortedDataset As New DataSet
            For i = 0 To dset.Tables("PAYMENTPROCEED").Rows.Count - 2
                For j = 0 To dset.Tables("PAYMENTPROCEED").Rows.Count - 2 - i
                    If CType(dset.Tables("PAYMENTPROCEED").Rows(j)(Clmn).ToString, Decimal) > CType(dset.Tables("PAYMENTPROCEED").Rows(j + 1)(Clmn).ToString, Decimal) Then

                        Dim objOutputXml As New XmlDocument

                        objOutputXml.LoadXml("<RP_PAYMENTPROCEED_OUTPUT><PAYMENTPROCEED RowNo='' CheckUncheckStatus='' ISPOrderID='' Month='' Year='' NPID='' CostActivityMonth='' ISPName='' AgencyName='' Address='' Address1='' City='' OfficeID='' Status='' UserName='' OnlineDate='' CancellationDate='' StartDate='' EndDate='' DaysUsed='' ProRate='' ISPRentalCharges='' VATPercentage='' ISPInstallationCharges='' VATAmount='' Amount='' PANumber='' I_VAT='' CAFNumber='' StaticIP='' /> </RP_PAYMENTPROCEED_OUTPUT>")
                        With objOutputXml.DocumentElement("PAYMENTPROCEED")

                            .Attributes("RowNo").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(0)
                            .Attributes("CheckUncheckStatus").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(1)
                            .Attributes("ISPOrderID").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(2)
                            .Attributes("Month").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(3)
                            .Attributes("Year").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(4)
                            .Attributes("NPID").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(5)
                            .Attributes("CostActivityMonth").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(6)
                            .Attributes("ISPName").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(7)
                            .Attributes("AgencyName").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(8)
                            .Attributes("Address").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(9)
                            .Attributes("Address1").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(10)
                            .Attributes("City").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(11)
                            .Attributes("OfficeID").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(12)
                            .Attributes("Status").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(13)
                            .Attributes("UserName").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(14)


                            .Attributes("OnlineDate").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(15)
                            .Attributes("CancellationDate").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(16)
                            .Attributes("StartDate").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(17)
                            .Attributes("EndDate").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(18)
                            .Attributes("DaysUsed").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(19)
                            .Attributes("ProRate").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(20)
                            .Attributes("ISPRentalCharges").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(21)
                            .Attributes("VATPercentage").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(22)
                            .Attributes("ISPInstallationCharges").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(23)
                            .Attributes("VATAmount").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(24)

                            .Attributes("Amount").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(25)
                            .Attributes("PANumber").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(26)
                            .Attributes("I_VAT").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(27)
                            .Attributes("CAFNumber").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(28)
                            .Attributes("StaticIP").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(29)

                        End With

                        dset.Tables("PAYMENTPROCEED").Rows(j)(0) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(0)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(1) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(1)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(2) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(2)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(3) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(3)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(4) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(4)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(5) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(5)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(6) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(6)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(7) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(7)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(8) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(8)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(9) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(9)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(10) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(10)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(11) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(11)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(12) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(12)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(13) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(13)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(14) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(14)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(15) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(15)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(16) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(16)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(17) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(17)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(18) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(18)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(19) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(19)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(20) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(20)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(21) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(21)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(22) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(22)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(23) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(23)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(24) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(24)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(25) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(25)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(26) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(26)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(27) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(27)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(28) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(28)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(29) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(29)

                        'RowNo=''
                        'CheckUncheckStatus=''
                        '  ISPOrderID=''
                        '   Month='' 
                        'Year=''
                        ' NPID='' 
                        ' CostActivityMonth=''
                        '  ISPName=''
                        '   AgencyName=''
                        '   Address=''
                        '    Address1=''
                        '	 City=''
                        '	  OfficeID=''
                        '	   Status='' 
                        '	   UserName='' 
                        '	   OnlineDate=''
                        '	    CancellationDate=''
                        '		 StartDate=''
                        '		  EndDate=''
                        '		  DaysUsed=''
                        '		   ProRate='' 
                        '		   ISPRentalCharges=''
                        '		    VATPercentage='' 
                        '			ISPInstallationCharges=''
                        '			 VATAmount='' 
                        '			 Amount=''
                        '			  PANumber=''
                        '			   I_VAT=''
                        '			    CAFNumber=''
                        '				 StaticIP='' /> 
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(0) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("RowNo").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(1) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("CheckUncheckStatus").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(2) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("ISPOrderID").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(3) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("Month").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(4) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("Year").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(5) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("NPID").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(6) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("CostActivityMonth").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(7) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("ISPName").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(8) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("AgencyName").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(9) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("Address").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(10) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("Address1").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(11) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("City").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(12) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("OfficeID").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(13) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("Status").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(14) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("UserName").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(15) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("OnlineDate").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(16) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("CancellationDate").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(17) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("StartDate").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(18) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("EndDate").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(19) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("DaysUsed").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(20) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("ProRate").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(21) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("ISPRentalCharges").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(22) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("VATPercentage").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(23) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("ISPInstallationCharges").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(24) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("VATAmount").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(25) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("Amount").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(26) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("PANumber").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(27) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("I_VAT").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(28) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("CAFNumber").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(29) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("StaticIP").Value()

                        dset.AcceptChanges()
                    End If
                Next j
            Next i
        Else
            Dim i, j As Integer
            Dim SortedDataset As New DataSet
            For i = 0 To dset.Tables("PAYMENTPROCEED").Rows.Count - 2
                For j = 0 To dset.Tables("PAYMENTPROCEED").Rows.Count - 2 - i
                    If String.Compare(dset.Tables("PAYMENTPROCEED").Rows(j)(Clmn).ToString.Trim, dset.Tables("PAYMENTPROCEED").Rows(j + 1)(Clmn).ToString.Trim) > 0 Then
                        Dim objOutputXml As New XmlDocument
                        objOutputXml.LoadXml("<RP_PAYMENTPROCEED_OUTPUT><PAYMENTPROCEED RowNo='' CheckUncheckStatus='' ISPOrderID='' Month='' Year='' NPID='' CostActivityMonth='' ISPName='' AgencyName='' Address='' Address1='' City='' OfficeID='' Status='' UserName='' OnlineDate='' CancellationDate='' StartDate='' EndDate='' DaysUsed='' ProRate='' ISPRentalCharges='' VATPercentage='' ISPInstallationCharges='' VATAmount='' Amount='' PANumber='' I_VAT='' CAFNumber='' StaticIP='' /> </RP_PAYMENTPROCEED_OUTPUT>")
                        With objOutputXml.DocumentElement("PAYMENTPROCEED")

                            .Attributes("RowNo").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(0)
                            .Attributes("CheckUncheckStatus").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(1)
                            .Attributes("ISPOrderID").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(2)
                            .Attributes("Month").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(3)
                            .Attributes("Year").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(4)
                            .Attributes("NPID").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(5)
                            .Attributes("CostActivityMonth").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(6)
                            .Attributes("ISPName").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(7)
                            .Attributes("AgencyName").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(8)
                            .Attributes("Address").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(9)
                            .Attributes("Address1").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(10)
                            .Attributes("City").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(11)
                            .Attributes("OfficeID").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(12)
                            .Attributes("Status").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(13)
                            .Attributes("UserName").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(14)


                            .Attributes("OnlineDate").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(15)
                            .Attributes("CancellationDate").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(16)
                            .Attributes("StartDate").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(17)
                            .Attributes("EndDate").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(18)
                            .Attributes("DaysUsed").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(19)
                            .Attributes("ProRate").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(20)
                            .Attributes("ISPRentalCharges").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(21)
                            .Attributes("VATPercentage").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(22)
                            .Attributes("ISPInstallationCharges").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(23)
                            .Attributes("VATAmount").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(24)

                            .Attributes("Amount").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(25)
                            .Attributes("PANumber").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(26)
                            .Attributes("I_VAT").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(27)
                            .Attributes("CAFNumber").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(28)
                            .Attributes("StaticIP").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(29)

                        End With

                        dset.Tables("PAYMENTPROCEED").Rows(j)(0) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(0)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(1) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(1)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(2) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(2)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(3) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(3)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(4) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(4)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(5) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(5)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(6) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(6)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(7) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(7)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(8) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(8)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(9) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(9)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(10) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(10)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(11) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(11)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(12) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(12)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(13) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(13)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(14) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(14)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(15) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(15)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(16) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(16)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(17) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(17)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(18) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(18)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(19) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(19)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(20) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(20)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(21) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(21)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(22) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(22)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(23) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(23)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(24) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(24)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(25) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(25)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(26) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(26)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(27) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(27)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(28) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(28)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(29) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(29)

                        'RowNo=''
                        'CheckUncheckStatus=''
                        '  ISPOrderID=''
                        '   Month='' 
                        'Year=''
                        ' NPID='' 
                        ' CostActivityMonth=''
                        '  ISPName=''
                        '   AgencyName=''
                        '   Address=''
                        '    Address1=''
                        '	 City=''
                        '	  OfficeID=''
                        '	   Status='' 
                        '	   UserName='' 
                        '	   OnlineDate=''
                        '	    CancellationDate=''
                        '		 StartDate=''
                        '		  EndDate=''
                        '		  DaysUsed=''
                        '		   ProRate='' 
                        '		   ISPRentalCharges=''
                        '		    VATPercentage='' 
                        '			ISPInstallationCharges=''
                        '			 VATAmount='' 
                        '			 Amount=''
                        '			  PANumber=''
                        '			   I_VAT=''
                        '			    CAFNumber=''
                        '				 StaticIP='' /> 
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(0) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("RowNo").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(1) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("CheckUncheckStatus").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(2) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("ISPOrderID").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(3) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("Month").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(4) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("Year").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(5) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("NPID").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(6) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("CostActivityMonth").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(7) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("ISPName").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(8) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("AgencyName").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(9) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("Address").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(10) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("Address1").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(11) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("City").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(12) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("OfficeID").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(13) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("Status").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(14) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("UserName").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(15) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("OnlineDate").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(16) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("CancellationDate").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(17) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("StartDate").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(18) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("EndDate").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(19) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("DaysUsed").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(20) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("ProRate").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(21) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("ISPRentalCharges").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(22) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("VATPercentage").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(23) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("ISPInstallationCharges").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(24) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("VATAmount").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(25) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("Amount").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(26) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("PANumber").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(27) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("I_VAT").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(28) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("CAFNumber").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(29) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("StaticIP").Value()

                        dset.AcceptChanges()
                    End If
                Next j
            Next i
            'Dim i, j As Integer
            'Dim SortedDataset As New DataSet
            'For i = 0 To dset.Tables("DETAILS").Rows.Count - 2
            '    For j = 0 To dset.Tables("DETAILS").Rows.Count - 2 - i
            '        If String.Compare(dset.Tables("DETAILS").Rows(j)(Clmn).ToString.Trim, dset.Tables("DETAILS").Rows(j + 1)(Clmn).ToString.Trim) > 0 Then
            '            Dim objOutputXml As New XmlDocument
            '            objOutputXml.LoadXml("<PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_OUTPUT><DETAILS  TYPE = ''  VALUE = '' AMADEUS =''   ABACUS =''  GALILEO =''  WORLDSPAN =''  SABREDOMESTIC ='' TOTAL =''   TYPE_PER=''  AMADEUS_PER =''  ABACUS_PER =''   GALILEO_PER =''  WORLDSPAN_PER =''  SABREDOMESTIC_PER = ''  /></PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_OUTPUT>")
            '            With objOutputXml.DocumentElement("DETAILS")
            '                .Attributes("TYPE").Value = dset.Tables("DETAILS").Rows(j)(0)
            '                .Attributes("VALUE").Value = dset.Tables("DETAILS").Rows(j)(1)
            '                .Attributes("AMADEUS").Value = dset.Tables("DETAILS").Rows(j)(2)
            '                .Attributes("ABACUS").Value = dset.Tables("DETAILS").Rows(j)(3)
            '                .Attributes("GALILEO").Value = dset.Tables("DETAILS").Rows(j)(4)
            '                .Attributes("WORLDSPAN").Value = dset.Tables("DETAILS").Rows(j)(5)
            '                .Attributes("SABREDOMESTIC").Value = dset.Tables("DETAILS").Rows(j)(6)
            '                .Attributes("TOTAL").Value = dset.Tables("DETAILS").Rows(j)(7)
            '                .Attributes("TYPE_PER").Value = dset.Tables("DETAILS").Rows(j)(8)
            '                .Attributes("AMADEUS_PER").Value = dset.Tables("DETAILS").Rows(j)(9)
            '                .Attributes("ABACUS_PER").Value = dset.Tables("DETAILS").Rows(j)(10)
            '                .Attributes("GALILEO_PER").Value = dset.Tables("DETAILS").Rows(j)(11)
            '                .Attributes("WORLDSPAN_PER").Value = dset.Tables("DETAILS").Rows(j)(12)
            '                .Attributes("SABREDOMESTIC_PER").Value = dset.Tables("DETAILS").Rows(j)(13)
            '            End With

            '            dset.Tables("DETAILS").Rows(j)(0) = dset2.Tables("DETAILS").Rows(j + 1)(0)
            '            dset.Tables("DETAILS").Rows(j)(1) = dset2.Tables("DETAILS").Rows(j + 1)(1)
            '            dset.Tables("DETAILS").Rows(j)(2) = dset2.Tables("DETAILS").Rows(j + 1)(2)
            '            dset.Tables("DETAILS").Rows(j)(3) = dset2.Tables("DETAILS").Rows(j + 1)(3)
            '            dset.Tables("DETAILS").Rows(j)(4) = dset2.Tables("DETAILS").Rows(j + 1)(4)
            '            dset.Tables("DETAILS").Rows(j)(5) = dset2.Tables("DETAILS").Rows(j + 1)(5)
            '            dset.Tables("DETAILS").Rows(j)(6) = dset2.Tables("DETAILS").Rows(j + 1)(6)
            '            dset.Tables("DETAILS").Rows(j)(7) = dset2.Tables("DETAILS").Rows(j + 1)(7)
            '            dset.Tables("DETAILS").Rows(j)(8) = dset2.Tables("DETAILS").Rows(j + 1)(8)
            '            dset.Tables("DETAILS").Rows(j)(9) = dset2.Tables("DETAILS").Rows(j + 1)(9)
            '            dset.Tables("DETAILS").Rows(j)(10) = dset2.Tables("DETAILS").Rows(j + 1)(10)
            '            dset.Tables("DETAILS").Rows(j)(11) = dset2.Tables("DETAILS").Rows(j + 1)(11)
            '            dset.Tables("DETAILS").Rows(j)(12) = dset2.Tables("DETAILS").Rows(j + 1)(12)
            '            dset.Tables("DETAILS").Rows(j)(13) = dset2.Tables("DETAILS").Rows(j + 1)(13)


            '            dset.Tables("DETAILS").Rows(j + 1)(0) = objOutputXml.DocumentElement("DETAILS").Attributes("TYPE").Value()
            '            dset.Tables("DETAILS").Rows(j + 1)(1) = objOutputXml.DocumentElement("DETAILS").Attributes("VALUE").Value()
            '            dset.Tables("DETAILS").Rows(j + 1)(2) = objOutputXml.DocumentElement("DETAILS").Attributes("AMADEUS").Value()
            '            dset.Tables("DETAILS").Rows(j + 1)(3) = objOutputXml.DocumentElement("DETAILS").Attributes("ABACUS").Value()
            '            dset.Tables("DETAILS").Rows(j + 1)(4) = objOutputXml.DocumentElement("DETAILS").Attributes("GALILEO").Value()
            '            dset.Tables("DETAILS").Rows(j + 1)(5) = objOutputXml.DocumentElement("DETAILS").Attributes("WORLDSPAN").Value()
            '            dset.Tables("DETAILS").Rows(j + 1)(6) = objOutputXml.DocumentElement("DETAILS").Attributes("SABREDOMESTIC").Value()
            '            dset.Tables("DETAILS").Rows(j + 1)(7) = objOutputXml.DocumentElement("DETAILS").Attributes("TOTAL").Value()
            '            dset.Tables("DETAILS").Rows(j + 1)(8) = objOutputXml.DocumentElement("DETAILS").Attributes("TYPE_PER").Value()
            '            dset.Tables("DETAILS").Rows(j + 1)(9) = objOutputXml.DocumentElement("DETAILS").Attributes("AMADEUS_PER").Value()
            '            dset.Tables("DETAILS").Rows(j + 1)(10) = objOutputXml.DocumentElement("DETAILS").Attributes("ABACUS_PER").Value()
            '            dset.Tables("DETAILS").Rows(j + 1)(11) = objOutputXml.DocumentElement("DETAILS").Attributes("GALILEO_PER").Value()
            '            dset.Tables("DETAILS").Rows(j + 1)(12) = objOutputXml.DocumentElement("DETAILS").Attributes("WORLDSPAN_PER").Value()
            '            dset.Tables("DETAILS").Rows(j + 1)(13) = objOutputXml.DocumentElement("DETAILS").Attributes("SABREDOMESTIC_PER").Value()

            '            dset.AcceptChanges()
            '        End If
            '    Next j
            'Next i
        End If
    End Sub
    Private Sub bubbleSortDesc(ByRef dset As DataSet, ByVal Clmn As String, ByVal dset2 As DataSet)

        'If Clmn = "VALUE" Then
        If Clmn = "ISPRentalCharges" Or Clmn = "VATAmount" Or Clmn = "ProRate" Or Clmn = "VATPercentage" Or Clmn = "ISPInstallationCharges" Or Clmn = "VATAmount" Or Clmn = "I_VAT" Or Clmn = "Amount" Then
            Dim i, j As Integer
            Dim SortedDataset As New DataSet
            For i = 0 To dset.Tables("PAYMENTPROCEED").Rows.Count - 2
                For j = 0 To dset.Tables("PAYMENTPROCEED").Rows.Count - 2 - i
                    If CType(dset.Tables("PAYMENTPROCEED").Rows(j)(Clmn).ToString, Decimal) < CType(dset.Tables("PAYMENTPROCEED").Rows(j + 1)(Clmn).ToString, Decimal) Then
                        Dim objOutputXml As New XmlDocument
                        objOutputXml.LoadXml("<RP_PAYMENTPROCEED_OUTPUT><PAYMENTPROCEED RowNo='' CheckUncheckStatus='' ISPOrderID='' Month='' Year='' NPID='' CostActivityMonth='' ISPName='' AgencyName='' Address='' Address1='' City='' OfficeID='' Status='' UserName='' OnlineDate='' CancellationDate='' StartDate='' EndDate='' DaysUsed='' ProRate='' ISPRentalCharges='' VATPercentage='' ISPInstallationCharges='' VATAmount='' Amount='' PANumber='' I_VAT='' CAFNumber='' StaticIP='' /></RP_PAYMENTPROCEED_OUTPUT> ")
                        With objOutputXml.DocumentElement("PAYMENTPROCEED")

                            .Attributes("RowNo").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(0)
                            .Attributes("CheckUncheckStatus").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(1)
                            .Attributes("ISPOrderID").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(2)
                            .Attributes("Month").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(3)
                            .Attributes("Year").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(4)
                            .Attributes("NPID").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(5)
                            .Attributes("CostActivityMonth").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(6)
                            .Attributes("ISPName").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(7)
                            .Attributes("AgencyName").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(8)
                            .Attributes("Address").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(9)
                            .Attributes("Address1").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(10)
                            .Attributes("City").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(11)
                            .Attributes("OfficeID").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(12)
                            .Attributes("Status").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(13)
                            .Attributes("UserName").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(14)


                            .Attributes("OnlineDate").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(15)
                            .Attributes("CancellationDate").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(16)
                            .Attributes("StartDate").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(17)
                            .Attributes("EndDate").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(18)
                            .Attributes("DaysUsed").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(19)
                            .Attributes("ProRate").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(20)
                            .Attributes("ISPRentalCharges").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(21)
                            .Attributes("VATPercentage").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(22)
                            .Attributes("ISPInstallationCharges").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(23)
                            .Attributes("VATAmount").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(24)

                            .Attributes("Amount").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(25)
                            .Attributes("PANumber").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(26)
                            .Attributes("I_VAT").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(27)
                            .Attributes("CAFNumber").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(28)
                            .Attributes("StaticIP").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(29)

                        End With

                        dset.Tables("PAYMENTPROCEED").Rows(j)(0) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(0)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(1) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(1)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(2) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(2)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(3) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(3)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(4) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(4)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(5) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(5)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(6) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(6)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(7) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(7)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(8) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(8)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(9) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(9)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(10) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(10)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(11) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(11)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(12) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(12)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(13) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(13)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(14) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(14)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(15) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(15)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(16) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(16)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(17) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(17)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(18) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(18)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(19) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(19)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(20) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(20)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(21) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(21)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(22) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(22)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(23) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(23)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(24) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(24)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(25) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(25)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(26) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(26)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(27) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(27)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(28) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(28)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(29) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(29)

                        'RowNo=''
                        'CheckUncheckStatus=''
                        '  ISPOrderID=''
                        '   Month='' 
                        'Year=''
                        ' NPID='' 
                        ' CostActivityMonth=''
                        '  ISPName=''
                        '   AgencyName=''
                        '   Address=''
                        '    Address1=''
                        '	 City=''
                        '	  OfficeID=''
                        '	   Status='' 
                        '	   UserName='' 
                        '	   OnlineDate=''
                        '	    CancellationDate=''
                        '		 StartDate=''
                        '		  EndDate=''
                        '		  DaysUsed=''
                        '		   ProRate='' 
                        '		   ISPRentalCharges=''
                        '		    VATPercentage='' 
                        '			ISPInstallationCharges=''
                        '			 VATAmount='' 
                        '			 Amount=''
                        '			  PANumber=''
                        '			   I_VAT=''
                        '			    CAFNumber=''
                        '				 StaticIP='' /> 
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(0) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("RowNo").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(1) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("CheckUncheckStatus").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(2) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("ISPOrderID").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(3) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("Month").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(4) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("Year").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(5) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("NPID").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(6) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("CostActivityMonth").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(7) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("ISPName").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(8) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("AgencyName").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(9) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("Address").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(10) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("Address1").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(11) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("City").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(12) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("OfficeID").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(13) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("Status").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(14) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("UserName").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(15) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("OnlineDate").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(16) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("CancellationDate").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(17) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("StartDate").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(18) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("EndDate").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(19) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("DaysUsed").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(20) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("ProRate").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(21) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("ISPRentalCharges").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(22) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("VATPercentage").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(23) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("ISPInstallationCharges").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(24) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("VATAmount").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(25) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("Amount").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(26) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("PANumber").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(27) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("I_VAT").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(28) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("CAFNumber").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(29) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("StaticIP").Value()

                        dset.AcceptChanges()
                    End If
                Next j
            Next i

        Else

            Dim i, j As Integer
            Dim SortedDataset As New DataSet
            For i = 0 To dset.Tables("PAYMENTPROCEED").Rows.Count - 2
                For j = 0 To dset.Tables("PAYMENTPROCEED").Rows.Count - 2 - i
                    If String.Compare(dset.Tables("PAYMENTPROCEED").Rows(j)(Clmn).ToString.Trim, dset.Tables("PAYMENTPROCEED").Rows(j + 1)(Clmn).ToString.Trim) < 0 Then
                        Dim objOutputXml As New XmlDocument
                        objOutputXml.LoadXml("<RP_PAYMENTPROCEED_OUTPUT><PAYMENTPROCEED RowNo='' CheckUncheckStatus='' ISPOrderID='' Month='' Year='' NPID='' CostActivityMonth='' ISPName='' AgencyName='' Address='' Address1='' City='' OfficeID='' Status='' UserName='' OnlineDate='' CancellationDate='' StartDate='' EndDate='' DaysUsed='' ProRate='' ISPRentalCharges='' VATPercentage='' ISPInstallationCharges='' VATAmount='' Amount='' PANumber='' I_VAT='' CAFNumber='' StaticIP='' /></RP_PAYMENTPROCEED_OUTPUT> ")
                        With objOutputXml.DocumentElement("PAYMENTPROCEED")

                            .Attributes("RowNo").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(0)
                            .Attributes("CheckUncheckStatus").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(1)
                            .Attributes("ISPOrderID").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(2)
                            .Attributes("Month").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(3)
                            .Attributes("Year").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(4)
                            .Attributes("NPID").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(5)
                            .Attributes("CostActivityMonth").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(6)
                            .Attributes("ISPName").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(7)
                            .Attributes("AgencyName").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(8)
                            .Attributes("Address").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(9)
                            .Attributes("Address1").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(10)
                            .Attributes("City").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(11)
                            .Attributes("OfficeID").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(12)
                            .Attributes("Status").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(13)
                            .Attributes("UserName").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(14)


                            .Attributes("OnlineDate").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(15)
                            .Attributes("CancellationDate").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(16)
                            .Attributes("StartDate").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(17)
                            .Attributes("EndDate").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(18)
                            .Attributes("DaysUsed").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(19)
                            .Attributes("ProRate").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(20)
                            .Attributes("ISPRentalCharges").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(21)
                            .Attributes("VATPercentage").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(22)
                            .Attributes("ISPInstallationCharges").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(23)
                            .Attributes("VATAmount").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(24)

                            .Attributes("Amount").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(25)
                            .Attributes("PANumber").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(26)
                            .Attributes("I_VAT").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(27)
                            .Attributes("CAFNumber").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(28)
                            .Attributes("StaticIP").Value = dset.Tables("PAYMENTPROCEED").Rows(j)(29)

                        End With

                        dset.Tables("PAYMENTPROCEED").Rows(j)(0) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(0)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(1) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(1)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(2) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(2)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(3) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(3)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(4) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(4)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(5) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(5)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(6) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(6)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(7) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(7)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(8) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(8)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(9) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(9)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(10) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(10)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(11) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(11)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(12) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(12)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(13) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(13)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(14) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(14)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(15) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(15)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(16) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(16)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(17) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(17)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(18) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(18)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(19) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(19)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(20) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(20)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(21) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(21)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(22) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(22)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(23) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(23)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(24) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(24)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(25) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(25)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(26) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(26)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(27) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(27)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(28) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(28)
                        dset.Tables("PAYMENTPROCEED").Rows(j)(29) = dset2.Tables("PAYMENTPROCEED").Rows(j + 1)(29)

                        'RowNo=''
                        'CheckUncheckStatus=''
                        '  ISPOrderID=''
                        '   Month='' 
                        'Year=''
                        ' NPID='' 
                        ' CostActivityMonth=''
                        '  ISPName=''
                        '   AgencyName=''
                        '   Address=''
                        '    Address1=''
                        '	 City=''
                        '	  OfficeID=''
                        '	   Status='' 
                        '	   UserName='' 
                        '	   OnlineDate=''
                        '	    CancellationDate=''
                        '		 StartDate=''
                        '		  EndDate=''
                        '		  DaysUsed=''
                        '		   ProRate='' 
                        '		   ISPRentalCharges=''
                        '		    VATPercentage='' 
                        '			ISPInstallationCharges=''
                        '			 VATAmount='' 
                        '			 Amount=''
                        '			  PANumber=''
                        '			   I_VAT=''
                        '			    CAFNumber=''
                        '				 StaticIP='' /> 
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(0) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("RowNo").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(1) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("CheckUncheckStatus").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(2) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("ISPOrderID").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(3) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("Month").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(4) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("Year").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(5) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("NPID").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(6) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("CostActivityMonth").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(7) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("ISPName").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(8) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("AgencyName").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(9) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("Address").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(10) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("Address1").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(11) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("City").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(12) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("OfficeID").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(13) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("Status").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(14) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("UserName").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(15) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("OnlineDate").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(16) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("CancellationDate").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(17) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("StartDate").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(18) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("EndDate").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(19) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("DaysUsed").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(20) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("ProRate").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(21) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("ISPRentalCharges").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(22) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("VATPercentage").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(23) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("ISPInstallationCharges").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(24) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("VATAmount").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(25) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("Amount").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(26) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("PANumber").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(27) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("I_VAT").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(28) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("CAFNumber").Value()
                        dset.Tables("PAYMENTPROCEED").Rows(j + 1)(29) = objOutputXml.DocumentElement("PAYMENTPROCEED").Attributes("StaticIP").Value()

                        dset.AcceptChanges()
                    End If
                Next j
            Next i

        End If
    End Sub

    Private Sub SetImageForSorting(ByVal grd As GridView)
        Dim imgUp As New Image
        imgUp.ImageUrl = "~/Images/Sortup.gif"
        Dim imgDown As New Image
        imgDown.ImageUrl = "~/Images/Sortdown.gif"
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Dim intcol As Integer = grd.Columns.IndexOf(field)
                If ViewState("Desc") = "FALSE" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "TRUE" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If
            End If
        Next
    End Sub


#End Region



#Region " GetCallbackResult Procedure is fired internally by ICallbackEventHandler Interface  "
    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
        Return str
    End Function
#End Region
#Region " RaiseCallbackEvent Procedure is fired internally by ICallbackEventHandler Interface  "
    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent

        Dim objXmlReader As XmlNodeReader
        Dim ds2 As New DataSet
        Dim objOutputIspOrderSearchXml As New XmlDocument
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim strArr() As String
        strArr = eventArgument.Split("|")

        If strArr(4).ToString = "Check" Then
            Dim Rowno As String = strArr(2)
            Dim CheckStatus As String = strArr(3)
            str = strArr(0) + "!" + "Check"
            If Session("PaymentRecDataSource") IsNot Nothing Then
                Dim dset As DataSet = CType(Session("PaymentRecDataSource"), DataSet)
                hdUpdateForSessionXml.Value = dset.GetXml()
                If hdUpdateForSessionXml.Value <> "" Then
                    objOutputIspOrderSearchXml.LoadXml(hdUpdateForSessionXml.Value)
                    If objOutputIspOrderSearchXml.DocumentElement.SelectNodes("PAYMENTPROCEED[@RowNo='" + Rowno + "']").Count > 0 Then
                        If CheckStatus.ToString.ToUpper = "TRUE" Then
                            objOutputIspOrderSearchXml.DocumentElement.SelectSingleNode("PAYMENTPROCEED[@RowNo='" + Rowno + "']").Attributes("CheckUncheckStatus").Value = "True"
                        Else
                            objOutputIspOrderSearchXml.DocumentElement.SelectSingleNode("PAYMENTPROCEED[@RowNo='" + Rowno + "']").Attributes("CheckUncheckStatus").Value = ""
                        End If
                    End If
                    hdUpdateForSessionXml.Value = objOutputIspOrderSearchXml.OuterXml
                    objOutputIspOrderSearchXml.LoadXml(hdUpdateForSessionXml.Value)
                    objXmlReader = New XmlNodeReader(objOutputIspOrderSearchXml)
                    ds2.ReadXml(objXmlReader)
                    Session("PaymentRecDataSource") = ds2
                    If objOutputIspOrderSearchXml.DocumentElement.SelectNodes("PAYMENTPROCEED[@CheckUncheckStatus ='True']").Count > 0 Then
                        Session("Checked") = "TRUE"
                        hdChechedItem.Value = "1"
                        str = strArr(0) + "!" + "Check" + "!" + "CheckItemExist"
                    Else
                        Session("Checked") = "FALSE"
                        str = strArr(0) + "!" + "Check" + "!" + "CheckItemNotExist"
                    End If


                End If
            End If
        ElseIf strArr(4).ToString = "Do" Then
            '  str = strArr(0) + "|" + "Do"
            str = strArr(0) + "!" + "Do" + "!" + +"!" + +"!" + +"!" + ""
            'CallDoPayment()
            'Dim hdAmt As String
            'Dim hdISPOrderID As String
            'Dim hdMonths As String
            'Dim hdYears As String

            'Dim strISPOrderID As String = ""
            'Dim i As Integer
            'Dim TotalAmt As Decimal = 0

            'If Session("PaymentRecDataSource") IsNot Nothing Then
            '    Dim dset As DataSet = CType(Session("PaymentRecDataSource"), DataSet)
            '    objOutputIspOrderSearchXml.LoadXml(dset.GetXml())
            '    '  gvIspPaymentRec.PageIndex = CInt(ddlPageNumber.SelectedValue) - 1

            '    For Each objxmlnode As XmlNode In objOutputIspOrderSearchXml.DocumentElement.SelectNodes("PAYMENTPROCEED[@CheckUncheckStatus ='True']")
            '        hdAmt = objxmlnode.Attributes("Amount").Value
            '        hdISPOrderID = objxmlnode.Attributes("ISPOrderID").Value
            '        hdMonths = objxmlnode.Attributes("Month").Value
            '        hdYears = objxmlnode.Attributes("Year").Value
            '        If (hdAmt.ToString.Length > 0) Then
            '            TotalAmt += Convert.ToDecimal(hdAmt)
            '            If (strISPOrderID.Trim().Length = 0) Then
            '                strISPOrderID = hdISPOrderID
            '            Else
            '                strISPOrderID += "|" + hdISPOrderID
            '            End If
            '        End If
            '    Next

            '    If (strISPOrderID.Trim().Length = 0) Then
            '        lblError.Text = "There is no item selected to do Payment."
            '        Exit Sub
            '    End If


            '    str = strArr(0) + "!" + "Do" + "!" + TotalAmt.ToString + "!" + strISPOrderID + "!" + hdMonths.ToString + "!" + hdYears.ToString

            '    'ClientScript.RegisterStartupScript(Me.GetType(), "Open1", "<script language='javascript'>" & _
            '    '                     "PopupWindow=window.open('ISPUP_PaymentReceived.aspx?TotalAmt=" & TotalAmt & "&ISPOrderID=" & strISPOrderID & "&Month=" & hdMonth.Value & "&Year=" & hdYear.Value & "','IspPay','height=400px,width=830px,top=150,left=150,scrollbars=0,status=1')" & _
            '    '                     ";PopupWindow.focus();</script>")

            'End If
        End If

    End Sub
#End Region

    Protected Sub chkSelect_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSelect.CheckedChanged

    End Sub

    'Private Sub CallDoPayment()
    '    Dim hdAmt As String
    '    Dim hdISPOrderID As String
    '    Dim hdCostActivityMonths As String
    '    Dim hdYears As String

    '    Dim strISPOrderID As String = ""
    '    Dim i As Integer
    '    Dim TotalAmt As Decimal = 0
    '    Dim objOutputIspOrderSearchXml As New XmlDocument
    '    If Session("PaymentRecDataSource") IsNot Nothing Then
    '        Dim dset As DataSet = CType(Session("PaymentRecDataSource"), DataSet)
    '        objOutputIspOrderSearchXml.LoadXml(dset.GetXml())
    '        '  gvIspPaymentRec.PageIndex = CInt(ddlPageNumber.SelectedValue) - 1

    '        For Each objxmlnode As XmlNode In objOutputIspOrderSearchXml.DocumentElement.SelectNodes("PAYMENTPROCEED[@CheckUncheckStatus ='True']")
    '            hdAmt = objxmlnode.Attributes("Amount").Value
    '            hdISPOrderID = objxmlnode.Attributes("ISPOrderID").Value
    '            hdCostActivityMonths = objxmlnode.Attributes("CostActivityMonth").Value
    '            hdYears = objxmlnode.Attributes("Year").Value
    '            If (hdAmt.ToString.Length > 0) Then
    '                TotalAmt += Convert.ToDecimal(hdAmt)
    '                If (strISPOrderID.Trim().Length = 0) Then
    '                    strISPOrderID = hdISPOrderID
    '                Else
    '                    strISPOrderID += "|" + hdISPOrderID
    '                End If
    '            End If
    '        Next

    '        If (strISPOrderID.Trim().Length = 0) Then
    '            lblError.Text = "There is no item selected to do Payment."
    '            Exit Sub
    '        End If

    '        ClientScript.RegisterStartupScript(Me.GetType(), "Open1", "<script language='javascript'>" & _
    '                             "PopupWindow=window.open('ISPUP_PaymentReceived.aspx?TotalAmt=" & TotalAmt & "&ISPOrderID=" & strISPOrderID & "&Month=" & hdMonth.Value & "&Year=" & hdYear.Value & "','IspPay','height=400px,width=830px,top=150,left=150,scrollbars=0,status=1')" & _
    '                             ";PopupWindow.focus();</script>")

    '    End If
    'End Sub

   
   
    Protected Sub gvIspPaymentRec_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvIspPaymentRec.Sorting
        Try
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ViewState("Desc") = "FALSE"
            Else
                If ViewState("SortName") = SortName Then
                    If ViewState("Desc") = "TRUE" Then
                        ViewState("Desc") = "FALSE"
                    Else
                        ViewState("Desc") = "TRUE"
                    End If
                Else
                    ViewState("SortName") = SortName
                    ViewState("Desc") = "FALSE"
                End If
            End If
            If Session("PaymentRecDataSource") IsNot Nothing Then
                Dim dv As New DataView
                Dim ds As DataSet = CType(Session("PaymentRecDataSource"), DataSet)
                Dim dsets As New DataSet
                dsets = ds
                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                End If
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "NPID"
                    If ViewState("Desc") = "FALSE" Then
                        bubbleSortAsc(ds, "NPID", dsets)
                    Else
                        bubbleSortDesc(ds, "NPID", dsets)
                    End If
                Else
                    If ViewState("Desc") = "FALSE" Then
                        bubbleSortAsc(ds, ViewState("SortName"), dsets)
                    Else
                        bubbleSortDesc(ds, ViewState("SortName"), dsets)
                    End If
                End If
                gvIspPaymentRec.DataSource = ds.Tables("PAYMENTPROCEED") 'dv
                gvIspPaymentRec.DataBind()
                SetImageForSorting(gvIspPaymentRec)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class

