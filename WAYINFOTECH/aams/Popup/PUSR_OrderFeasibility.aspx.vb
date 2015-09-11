Imports System.Xml
Imports System.Data
Partial Class TravelAgency_TASR_OrderFeasibility
    Inherits System.Web.UI.Page
    Dim objEn As New EncyrptDeCyrpt

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


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Inputxml, Outputxml, OutputXml2 As New XmlDocument()
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objeAAMS As New eAAMS
        Dim objbzbzFeasibilityReport As New AAMS.bizTravelAgency.bzAgency
        Dim objSecurityXml As New XmlDocument
        Dim strBuilder As New StringBuilder
        Try
            Session("PageName") = Request.Url.ToString()
            objeAAMS.ExpirePageCache()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If Not IsPostBack Then
                Inputxml.LoadXml("<MS_VIEWAGENCYORDERFEASIBILITY_INPUT><LOCATION_CODE></LOCATION_CODE><USEORIGINAL></USEORIGINAL></MS_VIEWAGENCYORDERFEASIBILITY_INPUT>")
                If Request.QueryString("Lcode") IsNot Nothing Then
                    Inputxml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = objEn.Decrypt(Request.QueryString("Lcode").ToString.Trim)
                    If Session("Security") IsNot Nothing Then
                        objSecurityXml.LoadXml(Session("Security"))
                        objSecurityXml = New XmlDocument
                        objSecurityXml.LoadXml(Session("Security"))
                        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PASSIVE']").Count <> 0 Then
                                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PASSIVE']").Attributes("Value").Value)
                                If strBuilder(0) = "0" Then
                                    Inputxml.DocumentElement.SelectSingleNode("USEORIGINAL").InnerText = True
                                Else
                                    Inputxml.DocumentElement.SelectSingleNode("USEORIGINAL").InnerText = False
                                End If
                            End If
                        Else
                            Inputxml.DocumentElement.SelectSingleNode("USEORIGINAL").InnerText = False
                            strBuilder = objeAAMS.SecurityCheck(31)
                        End If
                    End If
                End If
                Outputxml = objbzbzFeasibilityReport.OrderFeasibility(Inputxml)
                If Outputxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    With Outputxml.DocumentElement.SelectSingleNode("AGENCYORDERFEASIBILITY")
                        txtAgencyName.Text = .Attributes("NAME").Value()
                        txtAdd.Text = .Attributes("ADDRESS").Value()
                        txtCity.Text = .Attributes("NAME").Value()
                        txtCountry.Text = .Attributes("CITY").Value()
                        txtIATA.Text = .Attributes("IATA_TID").Value()
                        txtOnlineStatus.Text = .Attributes("ONLINE_STATUS").Value()
                    End With
                    With Outputxml.DocumentElement.SelectSingleNode("AGENCYORDERFEASIBILITY/SUMMARY")
                        txt1aAvgLast12months.Text = .Attributes("LAST12MONTH1A_AVERAGE").Value()
                        txt1aAveragebefore12Months.Text = .Attributes("BEFORE_LAST12MONTH1A_AVERAGE").Value()
                        txtTotalPotential.Text = .Attributes("TOTALPOTENTIAL").Value()
                        txtTotalReq.Text = .Attributes("TOTALREQUIREMENT").Value()
                    End With
                    With Outputxml.DocumentElement.SelectSingleNode("AGENCYORDERFEASIBILITY/HARDWAREDETAILS")
                        txt1aPC.Text = .Attributes("PC1A").Value()
                        txtAgencyPC.Text = .Attributes("AGENCYPC").Value()
                        txtHCPrinter.Text = .Attributes("HCPRINTER").Value()
                        txtPrinters.Text = .Attributes("TICKETPRINTER").Value()
                    End With
                    objXmlReader = New XmlNodeReader(Outputxml)
                    ds.ReadXml(objXmlReader)
                    grdv1Aactual.DataSource = ds.Tables("PRODUCTIVITYDETAILS")
                    grdv1Aactual.DataBind()

                    'objXmlReader = New XmlNodeReader(Outputxml)
                    'ds.ReadXml(objXmlReader)
                    grdvCRS.DataSource = ds.Tables("CRSDETAILS")
                    grdvCRS.DataBind()
                Else
                    OutputXml2.LoadXml("<MS_VIEWAGENCYORDERFEASIBILITY_INPUT><AGENCYORDERFEASIBILITY LOCATION_CODE='' NAME='' ADDRESS='' CITY='' COUNTRY='' ONLINE_STATUS='' IATA_TID=''> <PRODUCTIVITYDETAILS MONTH='' PRODUCTIVITY='' /> <CRSDETAILS CRS='' AVERAGE='' /> <HARDWAREDETAILS PC1A='' AGENCYPC='' HCPRINTER='' TICKETPRINTER='' />' <SUMMARY LAST12MONTH1A_AVERAGE='' BEFORE_LAST12MONTH1A_AVERAGE='' TOTALPOTENTIAL='' TOTALREQUIREMENT='' /> </AGENCYORDERFEASIBILITY> </MS_VIEWAGENCYORDERFEASIBILITY_INPUT>")
                    objXmlReader = New XmlNodeReader(OutputXml2)
                    ds.ReadXml(objXmlReader)
                    grdv1Aactual.DataSource = ds.Tables("PRODUCTIVITYDETAILS")
                    grdv1Aactual.DataBind()
                    'objXmlReader = New XmlNodeReader(OutputXml2)
                    'ds.ReadXml(objXmlReader)
                    grdvCRS.DataSource = ds.Tables("CRSDETAILS")
                    grdvCRS.DataBind()
                End If
            End If
          
            Dim strBuilder1 As New StringBuilder
            Dim objSecurityXml1 As New XmlDocument
            objSecurityXml1.LoadXml(Session("Security"))
            If (objSecurityXml1.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml1.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OrderFeasibilityReport']").Count <> 0 Then
                    strBuilder1 = objeAAMS.SecurityCheck(objSecurityXml1.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OrderFeasibilityReport']").Attributes("Value").Value)
                    If strBuilder1(4) = "0" Then
                        btnPrint.Enabled = False
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                    'If strBuilder(0) = "0" Then
                    '    btnDisplay.Enabled = False
                    'End If

                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim Inputxml, Outputxml, OutputXml2 As New XmlDocument()
        '  Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objeAAMS As New eAAMS
        Dim objbzbzFeasibilityReport As New AAMS.bizTravelAgency.bzAgency
        Dim objSecurityXml As New XmlDocument
        Dim strBuilder As New StringBuilder
        Try
            Inputxml.LoadXml("<MS_VIEWAGENCYORDERFEASIBILITY_INPUT><LOCATION_CODE></LOCATION_CODE><USEORIGINAL></USEORIGINAL></MS_VIEWAGENCYORDERFEASIBILITY_INPUT>")
            If Request.QueryString("Lcode") IsNot Nothing Then
                Inputxml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = objEn.Decrypt(Request.QueryString("Lcode").ToString.Trim)
                If Session("Security") IsNot Nothing Then
                    objSecurityXml.LoadXml(Session("Security"))
                    objSecurityXml = New XmlDocument
                    objSecurityXml.LoadXml(Session("Security"))
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                        If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='USE_ORIGINAL_BOOKINGS']").Count <> 0 Then
                            strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='USE_ORIGINAL_BOOKINGS']").Attributes("Value").Value)
                            If strBuilder(0) = "0" Then
                                Inputxml.DocumentElement.SelectSingleNode("USEORIGINAL").InnerText = True
                            Else
                                Inputxml.DocumentElement.SelectSingleNode("USEORIGINAL").InnerText = False
                            End If
                        End If
                    Else
                        Inputxml.DocumentElement.SelectSingleNode("USEORIGINAL").InnerText = False
                        strBuilder = objeAAMS.SecurityCheck(31)
                    End If
                End If
            End If
            'Outputxml = objbzbzFeasibilityReport.OrderFeasibility(Inputxml)
            Session("eOrderFeasibilityReport") = Inputxml.OuterXml
            Response.Redirect("../RPSR_ReportShow.aspx?Case=OrderFeasibilityReport", False)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

  
End Class
