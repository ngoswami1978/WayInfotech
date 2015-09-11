Imports System.IO
Imports System.Text
Partial Class TravelAgency_TASR_GroupCase
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS


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
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
          
            '  btnChangeView.Attributes.Add("onclick", " return ChangeView();")
            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Group Case']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Group Case']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                    End If
                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                    End If

                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Non Billable Segments']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Non Billable Segments']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        ChkOrignalBook.Enabled = False
                        ChkOrignalBook.Checked = False
                        ChkOrignalBook.Visible = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                ChkOrignalBook.Visible = True
                ChkOrignalBook.Enabled = True
            End If
            If Not IsPostBack Then
                BindAllControl()
                If Request.QueryString("Lcode") IsNot Nothing Then
                    hdLCode.Value = Request.QueryString("Lcode").ToString
                End If
                If Request.QueryString("Lcode") IsNot Nothing Then
                    hdLCode.Value = Request.QueryString("Lcode")
                End If

            End If
            If Request.QueryString("Lcode") Is Nothing Then
                lblError.Text = "Invalid URL"
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub BindAllControl()
        Try
            Dim i, j As Integer
            'For j = 1990 To DateTime.Now.Year
            '    drpYear.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
            '    i += 1
            'Next
            For j = DateTime.Now.Year To 1990 Step -1
                drpYear.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                i += 1
            Next

            drpYear.SelectedValue = DateTime.Now.Year

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Private Sub GetAgencyGroupCase(ByVal Lcode As String)
        ' AgencyView(Lcode)
        GetAllDetailsLikeSummaryAndDataForGrid(Lcode)
    End Sub
    Private Sub GetAllDetailsLikeSummaryAndDataForGrid(ByVal Lcode As String)
        hdMonDisp.Value = ""
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzbzBIDT As New AAMS.bizProductivity.bzBIDT
        Try
            objInputXml.LoadXml("<UP_GROUPCASE_INPUT><LCODE/><IYEAR/><USEORIGINAL/><SUMMERYTYPE>2</SUMMERYTYPE><LIMITED_TO_AOFFICE/> <LIMITED_TO_REGION/> <LIMITED_TO_OWNAAGENCY/></UP_GROUPCASE_INPUT> ")
            objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerXml = Lcode 'Request.QueryString("Lcode")
            objInputXml.DocumentElement.SelectSingleNode("IYEAR").InnerXml = drpYear.SelectedValue
            If ChkOrignalBook.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("USEORIGINAL").InnerText = "Y"
            Else
                objInputXml.DocumentElement.SelectSingleNode("USEORIGINAL").InnerText = "N"
            End If

            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = ""


            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
                End If

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = 1
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = 0
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = 0
                End If
            End If
            'Here Back end Method Call          
            objOutputXml = objbzbzBIDT.GroupCaseProductivity(objInputXml)
            'objOutputXml.LoadXml("<UP_GROUPCASE_OUTPUT><LCODEDETAILS LCODE ='' AGENCYNAME = '' ADDRESS ='' ADDRESS1='' CITY ='' COUNTRY ='' IATA ='' ONLINE ='' OFFICEID =''/> <SUMMERYDETAILS EFFICIENCY_PERCENTAGE_CURRENTYEAR = '' DEFICIT_CURRENTYEAR ='' DEFICIT_PERCENTAGE_CURRENTYEAR ='' AMADEUS_AVERAGE_LASTYEAR = '' AMADEUS_AVERAGE_CURRENTYEAR = '' TOTAL_POTENTIAL_LASTYEAR ='' TOTAL_POTENTIAL_CURRENTYEAR =''/> <BIDTDETAILS MONTH='' PRODUCTIVITY=''/><MIDTDETAILS YEAR='' CRS='' PRODUCTIVITY_AVG = ''/> <Errors Status='FALSE'> <Error Code='' Description=''/></Errors></UP_GROUPCASE_OUTPUT>")
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                ' @ Agency Data Binds
                With objOutputXml.DocumentElement.SelectSingleNode("LCODEDETAILS")
                    txtAgencyName.Text = .Attributes("AGENCYNAME").Value()
                    txtAdd.Text = .Attributes("ADDRESS").Value()
                    ' txtAdd1.Text = .Attributes("ADDRERSS1").Value()
                    txtCity.Text = .Attributes("CITY").Value()
                    txtCountry.Text = .Attributes("COUNTRY").Value()
                    txtIATA.Text = .Attributes("IATA").Value()
                    txtOnline.Text = .Attributes("ONLINE").Value()
                    txtOfficeId.Text = .Attributes("OFFICEID").Value()
                End With
                ' @ All Other Data Binds
                With objOutputXml.DocumentElement.SelectSingleNode("SUMMERYDETAILS")

                    '<SUMMERYDETAILS EFFICIENCY_PERCENTAGE_CURRENTYEAR = '' 
                    'DEFICIT_CURRENTYEAR ='' DEFICIT_PERCENTAGE_CURRENTYEAR =''
                    ' AMADEUS_AVERAGE_LASTYEAR = '' AMADEUS_AVERAGE_CURRENTYEAR = ''
                    ' TOTAL_POTENTIAL_LASTYEAR ='' TOTAL_POTENTIAL_CURRENTYEAR =''/>
                    'LitEffCurr.Text = drpYear.SelectedValue
                    txtEff.Text = .Attributes("EFFICIENCY_PERCENTAGE_CURRENTYEAR").Value()
                    'LitDef.Text = drpYear.SelectedValue
                    txtDef.Text = .Attributes("DEFICIT_CURRENTYEAR").Value()
                    ' LitDefPer.Text = drpYear.SelectedValue
                    txtDefPer.Text = .Attributes("DEFICIT_PERCENTAGE_CURRENTYEAR").Value()
                    LitAP.Text = drpYear.SelectedValue - 1
                    txtAmadAvgPrev.Text = .Attributes("AMADEUS_AVERAGE_LASTYEAR").Value()
                    LitAN.Text = drpYear.SelectedValue
                    txtAmadAvgCurr.Text = .Attributes("AMADEUS_AVERAGE_CURRENTYEAR").Value()
                    LitPP.Text = drpYear.SelectedValue - 1
                    txtPotPrev.Text = .Attributes("TOTAL_POTENTIAL_LASTYEAR").Value()
                    LitPN.Text = drpYear.SelectedValue
                    txtPotCur.Text = .Attributes("TOTAL_POTENTIAL_CURRENTYEAR").Value()

                End With

                ' @ BIDT Data Binds
                ds = New DataSet
                ' objXmlReader = New XmlNodeReader(objOutputXml.DocumentElement.SelectNodes("BIDTDETAILS"))
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdvBidtDetails.DataSource = ds.Tables("BIDTDETAILS")
                grdvBidtDetails.DataBind()

                ' @ MIDT Other Data Binds
                ds = New DataSet
                ' objXmlReader = New XmlNodeReader(objOutputXml.DocumentElement.SelectSingleNode("MIDTDETAILS"))
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                GvAvgCrs.DataSource = ds.Tables("MIDTDETAILS")
                GvAvgCrs.DataBind()
                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                '@ Now Assingn Value From BIDT Lat Row
                LitEffCurr.Text = ds.Tables("BIDTDETAILS").Rows(ds.Tables("BIDTDETAILS").Rows.Count - 1)(1).ToString()
                LitDef.Text = ds.Tables("BIDTDETAILS").Rows(ds.Tables("BIDTDETAILS").Rows.Count - 1)(1).ToString()
                LitDefPer.Text = ds.Tables("BIDTDETAILS").Rows(ds.Tables("BIDTDETAILS").Rows.Count - 1)(1).ToString()

                Dim k As Integer

                For k = 0 To ds.Tables("BIDTDETAILS").Rows.Count - 1
                    If ds.Tables("BIDTDETAILS").Rows(k)(1).ToString().Split("'")(0).ToString.Trim = "JANUARY" Then
                        If hdMonDisp.Value.Trim.Length = 0 Then
                            hdMonDisp.Value = "1"
                        Else
                            hdMonDisp.Value = hdMonDisp.Value + "|" + "1"
                        End If

                    End If
                    If ds.Tables("BIDTDETAILS").Rows(k)(1).ToString().Split("'")(0).ToString.Trim = "FEBRUARY" Then
                        If hdMonDisp.Value.Trim.Length = 0 Then
                            hdMonDisp.Value = "2"
                        Else
                            hdMonDisp.Value = hdMonDisp.Value + "|" + "2"
                        End If
                    End If

                    If ds.Tables("BIDTDETAILS").Rows(k)(1).ToString().Split("'")(0).ToString.Trim = "MARCH" Then
                        If hdMonDisp.Value.Trim.Length = 0 Then
                            hdMonDisp.Value = "3"
                        Else
                            hdMonDisp.Value = hdMonDisp.Value + "|" + "3"
                        End If
                    End If
                    If ds.Tables("BIDTDETAILS").Rows(k)(1).ToString().Split("'")(0).ToString.Trim = "APRIL" Then
                        If hdMonDisp.Value.Trim.Length = 0 Then
                            hdMonDisp.Value = "4"
                        Else
                            hdMonDisp.Value = hdMonDisp.Value + "|" + "4"
                        End If
                    End If
                    If ds.Tables("BIDTDETAILS").Rows(k)(1).ToString().Split("'")(0).ToString.Trim = "MAY" Then
                        If hdMonDisp.Value.Trim.Length = 0 Then
                            hdMonDisp.Value = "5"
                        Else
                            hdMonDisp.Value = hdMonDisp.Value + "|" + "5"
                        End If
                    End If
                    If ds.Tables("BIDTDETAILS").Rows(k)(1).ToString().Split("'")(0).ToString.Trim = "JUNE" Then
                        If hdMonDisp.Value.Trim.Length = 0 Then
                            hdMonDisp.Value = "6"
                        Else
                            hdMonDisp.Value = hdMonDisp.Value + "|" + "6"
                        End If
                    End If
                    If ds.Tables("BIDTDETAILS").Rows(k)(1).ToString().Split("'")(0).ToString.Trim = "JULY" Then
                        If hdMonDisp.Value.Trim.Length = 0 Then
                            hdMonDisp.Value = "7"
                        Else
                            hdMonDisp.Value = hdMonDisp.Value + "|" + "7"
                        End If
                    End If
                    If ds.Tables("BIDTDETAILS").Rows(k)(1).ToString().Split("'")(0).ToString.Trim = "AUGUST" Then
                        If hdMonDisp.Value.Trim.Length = 0 Then
                            hdMonDisp.Value = "8"
                        Else
                            hdMonDisp.Value = hdMonDisp.Value + "|" + "8"
                        End If
                    End If
                    If ds.Tables("BIDTDETAILS").Rows(k)(1).ToString().Split("'")(0).ToString.Trim = "SEPTEMBER" Then
                        If hdMonDisp.Value.Trim.Length = 0 Then
                            hdMonDisp.Value = "9"
                        Else
                            hdMonDisp.Value = hdMonDisp.Value + "|" + "9"
                        End If
                    End If
                    If ds.Tables("BIDTDETAILS").Rows(k)(1).ToString().Split("'")(0).ToString.Trim = "OCTOBER" Then
                        If hdMonDisp.Value.Trim.Length = 0 Then
                            hdMonDisp.Value = "10"
                        Else
                            hdMonDisp.Value = hdMonDisp.Value + "|" + "10"
                        End If
                    End If
                    If ds.Tables("BIDTDETAILS").Rows(k)(1).ToString().Split("'")(0).ToString.Trim = "NOVEMBER" Then
                        If hdMonDisp.Value.Trim.Length = 0 Then
                            hdMonDisp.Value = "11"
                        Else
                            hdMonDisp.Value = hdMonDisp.Value + "|" + "11"
                        End If
                    End If
                    If ds.Tables("BIDTDETAILS").Rows(k)(1).ToString().Split("'")(0).ToString.Trim = "DECEMBER" Then
                        If hdMonDisp.Value.Trim.Length = 0 Then
                            hdMonDisp.Value = "12"
                        Else
                            hdMonDisp.Value = hdMonDisp.Value + "|" + "12"
                        End If
                    End If
                    'JANUARY() ' 2006 899 
                    'FEBRUARY() ' 2006 1406 
                    'MARCH() ' 2006 1573 
                    'APRIL() ' 2006 1770 
                    'MAY() ' 2006 1626 
                    'JUNE() ' 2006 987 
                    'JULY() ' 2006 1344 
                    'AUGUST() ' 2006 951 
                    'SEPTEMBER() ' 2006 1184 
                    'OCTOBER() ' 2006 1072 
                    'NOVEMBER() ' 2006 1331 
                    'DECEMBER() ' 2006 
                Next

                'hdMonDisp.Value = ds.Tables("BIDTDETAILS").Rows.Count.ToString



                'If ds.Tables("BIDTDETAILS").Rows.Count = 1 And LitEffCurr.Text.Trim.Length = 0 Then
                'hdMonDisp.Value = "0"
                'End If
                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

            Else
                grdvBidtDetails.DataSource = Nothing
                grdvBidtDetails.DataBind()
                GvAvgCrs.DataSource = Nothing
                GvAvgCrs.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            objbzbzBIDT = Nothing
        End Try
    End Sub

    'Private Sub AgencyView(ByVal Lcode As String)
    '    Dim objInputXml, objOutputXml As New XmlDocument
    '    Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
    '    Try
    '        objInputXml.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")
    '        objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerXml = Lcode 'Request.QueryString("Lcode")
    '        'Here Back end Method Call

    '        ' hdLcode.Value = Session("Action").ToString().Split("|").GetValue(1)
    '        objOutputXml = objbzAgency.View(objInputXml)
    '        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '            With objOutputXml.DocumentElement.SelectSingleNode("Agency")
    '                txtAgencyName.Text = .Attributes("NAME").Value()
    '                txtAdd.Text = .Attributes("ADDRESS").Value()
    '                txtCity.Text = .Attributes("CITY").Value()
    '                txtCountry.Text = .Attributes("COUNTRY").Value()
    '                txtIATA.Text = .Attributes("IATA_TID").Value()
    '                txtOnline.Text = .Attributes("ONLINE_STATUS").Value()
    '                txtOfficeId.Text = .Attributes("OFFICEID").Value()
    '            End With
    '        Else
    '            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = ex.Message.ToString
    '    Finally
    '        objInputXml = Nothing
    '        objOutputXml = Nothing
    '        objbzAgency = Nothing
    '    End Try
    'End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        ' PnlAgencyDetails.Visible = False
        ' drpYear.SelectedValue = DateTime.Now.Year
        'drpYear.Enabled = True
        Try
            PnlAgencyDetails.Visible = False
            drpYear.SelectedValue = DateTime.Now.Year
            drpYear.Enabled = True
            GVNav.DataSource = Nothing
            GVNav.DataBind()
            Session("DataSource") = Nothing
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Session("DataSource") = Nothing
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzbzBIDT As New AAMS.bizProductivity.bzBIDT
        Try
            If Request.QueryString("Lcode") IsNot Nothing Then
                'objInputXml.LoadXml("<PR_SEARCH_AGENCY_GROUPCASE_INPUT><FYEAR/><LCODE/><USEORIGINAL/></PR_SEARCH_AGENCY_GROUPCASE_INPUT>")
                objInputXml.LoadXml("<PR_SEARCH_AGENCY_GROUPCASE_INPUT><YEAR/><LCODE/><SUMMERYTYPE>1</SUMMERYTYPE></PR_SEARCH_AGENCY_GROUPCASE_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("YEAR").InnerText = drpYear.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = Request.QueryString("Lcode")
               
                objOutputXml = objbzbzBIDT.GroupCaseLocationList(objInputXml)

                '  objOutputXml.LoadXml("<PR_SEARCH_AGENCY_GROUPCASE_OUTPUT><AgencyDetails Lcode='2' Year='2008'></AgencyDetails><AgencyDetails Lcode='99' Year='2008'></AgencyDetails><AgencyDetails Lcode='131' Year='2008'></AgencyDetails><Errors Status='FALSE'><Error Code='' Description='' /> </Errors></PR_SEARCH_AGENCY_GROUPCASE_OUTPUT>")

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    Session("DataSource") = ds.Tables("AgencyDetails")
                    GVNav.PageIndex = 0
                    GVNav.DataSource = CType(Session("DataSource"), DataTable)
                    GVNav.DataBind()
                    PnlAgencyDetails.Visible = True
                    drpYear.Enabled = False
                Else
                    GVNav.DataSource = Nothing
                    GVNav.DataBind()
                    PnlAgencyDetails.Visible = False
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
   
    Protected Sub GVNav_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GVNav.PageIndexChanging
        Try
            lblError.Text = ""
            GVNav.PageIndex = e.NewPageIndex
            If Session("DataSource") IsNot Nothing Then
                GVNav.DataSource = CType(Session("DataSource"), DataTable)
                GVNav.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Function GetData(ByVal Lcode As Object) As String
        Try
            GetAgencyGroupCase(Lcode)
            hdLCode.Value = Lcode
            Return ""
        Catch ex As Exception
            lblError.Text = ex.Message
            Return ""
        Finally
        End Try
    End Function

    Protected Sub ChkOrignalBook_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkOrignalBook.CheckedChanged
        Try
            GetAllDetailsLikeSummaryAndDataForGrid(hdLCode.Value)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class


