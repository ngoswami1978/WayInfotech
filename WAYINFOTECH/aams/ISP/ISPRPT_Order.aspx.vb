'#############################################################
'############   Page Name -- ISPRPT_Order      ###############
'############   Date 20-December 2007  #######################
'############   Developed By Abhishek  #######################
'#############################################################
Partial Class ISP_ISPRPT_Order
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage

    

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = Request.Url.ToString()
            objeAAMS.ExpirePageCache()

            btnDisplay.Attributes.Add("onclick", "return CheckValidationForISPOrderReport();")

            drpCity.Attributes.Add("onkeyup", "return gotop('drpCity');")
            drpCountry.Attributes.Add("onkeyup", "return gotop('drpCountry');")
            drpIspname.Attributes.Add("onkeyup", "return gotop('drpIspname');")
            '  btnReset.Attributes.Add("onclick", "return IspOrderReset();")
            lblError.Text = String.Empty
            '***************************************************************************************
            'Code of Security Check
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ISP Order Report']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ISP Order Report']").Attributes("Value").Value)
                    If strBuilder(4) = "0" Then
                        btnDisplay.Enabled = False
                    End If
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnDisplay.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            If Not Page.IsPostBack Then
                BindAllControl()
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnDisplay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisplay.Click
        Dim objInputIspOrderReportXml, objOutputIspOrderReportXml As New XmlDocument
        Dim objbzISPOrder As New AAMS.bizISP.bzISPOrder
        Dim objSecurityXml As New XmlDocument
        Try
            ' objInputIspOrderReportXml.LoadXml("<RP_ORDER_INPUT><AgencyName /><City /><Country /><ISPName /><PeriodFromMonth /><PeriodFromYear /><PeriodToMonth /><PeriodToYear /><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency> <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></RP_ORDER_INPUT>")


            ' objInputIspOrderReportXml.LoadXml("<RP_ORDER_INPUT><AgencyName /><City /><Country /><ISPName /><PeriodFromMonth /><PeriodFromYear /></RP_ORDER_INPUT>")
            objInputIspOrderReportXml.LoadXml("<RP_ORDER_INPUT><AgencyName /><City /><Country /><ISPName /><PeriodFromMonth /><PeriodFromYear /><PeriodToMonth /><PeriodToYear /><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency> <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><EmployeeID/></RP_ORDER_INPUT>")

            If Not Session("LoginSession") Is Nothing Then
                objInputIspOrderReportXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0)
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
            objInputIspOrderReportXml.DocumentElement.SelectSingleNode("PeriodFromMonth").InnerText = drpMonthFrom.SelectedValue
            objInputIspOrderReportXml.DocumentElement.SelectSingleNode("PeriodToMonth").InnerText = drpMonthTo.SelectedValue
            objInputIspOrderReportXml.DocumentElement.SelectSingleNode("PeriodFromYear").InnerText = drpYearFrom.SelectedValue
            objInputIspOrderReportXml.DocumentElement.SelectSingleNode("PeriodToYear").InnerText = drpYearTo.SelectedValue
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
            objOutputIspOrderReportXml = objbzISPOrder.OrderReport(objInputIspOrderReportXml)

            If objOutputIspOrderReportXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Session("ISPOrderReport") = objOutputIspOrderReportXml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=ISPOrderReport", False)
            Else
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
                drpYearTo.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                i += 1
            Next
            drpYearFrom.SelectedValue = DateTime.Now.Year
            drpYearTo.SelectedValue = DateTime.Now.Year
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            txtAgencyName.Text = ""
            drpCity.SelectedIndex = 0
            drpCountry.SelectedIndex = 0
            drpIspname.SelectedIndex = 0
            drpMonthFrom.SelectedValue = DateTime.Now.Month
            drpMonthTo.SelectedValue = DateTime.Now.Month
            drpYearFrom.SelectedValue = DateTime.Now.Year
            drpYearTo.SelectedValue = DateTime.Now.Year
        Catch ex As Exception

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
End Class
