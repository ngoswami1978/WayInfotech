
Partial Class TravelAgency_MSSR_UpdateResponsiblility
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim str As String
    WithEvents chkChecked As CheckBox
    Dim objED As New EncyrptDeCyrpt
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strError As String = ""
        Session("PageName") = Request.Url.ToString()
        objeAAMS.ExpirePageCache()
        lblError.Text = ""
        Try
            If Request("txtResTo") IsNot Nothing Then
                txtResTo.Text = Request("txtResTo")
            End If


            btnSearch.Attributes.Add("onclick", "return CheckValidation();")
            lblError.Text = String.Empty
            '***************************************************************************************
            'Code of Security Check
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            txtAgencyName.Attributes.Add("onkeydown", "return ResetLcode();")
            txtGroupName.Attributes.Add("onkeydown", "return ResetChainCode();")

            txtResTo.Attributes.Add("readonly", "true")

            gvAResp.PageSize = ConfigurationManager.AppSettings("PAGE_SIZE").ToString

            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Update 1A Resposibility']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Update 1A Resposibility']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnSearch.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            If Not Page.IsPostBack Then
                Session("Checked") = Nothing
                Session("DataSource") = Nothing

                If Session("EmployeePageName") IsNot Nothing Then
                    hdEmployeePageName.Value = Session("EmployeePageName")
                End If

                BindAllControl()
            End If

            System.Threading.Thread.Sleep(100)
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
       
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        SearchData()
    End Sub
    Private Sub SearchData(Optional ByVal Inputstrxml As String = "")
        Dim objInputSearchXml, objOutputSearchXml As New XmlDocument
        Dim objbzAgency1ARespChange As New AAMS.bizTravelAgency.bzAgency1ARespChange
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim RowNo As Integer
        Dim dv As DataView
        '<TA_SEARCHAGENCY_1A_INPUT>
        '	<ACTION>S</ACTION>
        '	<LOCATION_CODE></LOCATION_CODE>
        '	<NAME></NAME>
        '	<CHAIN_CODE></CHAIN_CODE>
        '	<GROUP_NAME ></GROUP_NAME>
        '	<OFFICEID></OFFICEID>
        '	<CITY_NAME></CITY_NAME>
        '	 <COUNTRY_NAME></COUNTRY_NAME>
        '	<ONLINE_STATUS></ONLINE_STATUS>
        '	<AOFFICE></AOFFICE>
        '	<CRS></CRS>
        '	<AGENCY_TYPE></AGENCY_TYPE>
        '	<PRIORITY></PRIORITY>
        '	<RESP_ASSGN_FROM></RESP_ASSGN_FROM>
        '	<RESP_ASSGN_TO></RESP_ASSGN_TO>
        '	<PAGE_NO></PAGE_NO>
        '	<PAGE_SIZE></PAGE_SIZE>
        '	<SORT_BY></SORT_BY>
        '	<DESC></DESC>
        '</TA_SEARCHAGENCY_1A_INPUT>

        Try

            objInputSearchXml.LoadXml("<TA_SEARCHAGENCY_1A_INPUT><ACTION></ACTION><LOCATION_CODE></LOCATION_CODE>	<NAME></NAME>	<CHAIN_CODE></CHAIN_CODE><GROUP_NAME ></GROUP_NAME>	<OFFICEID></OFFICEID>	<CITY_NAME></CITY_NAME>	 <COUNTRY_NAME></COUNTRY_NAME>	<ONLINE_STATUS></ONLINE_STATUS>	<AOFFICE></AOFFICE>	<CRS></CRS>	<AGENCY_TYPE></AGENCY_TYPE>	<PRIORITY></PRIORITY>	<RESP_ASSGN_FROM></RESP_ASSGN_FROM>	<RESP_ASSGN_TO></RESP_ASSGN_TO>	<PAGE_NO></PAGE_NO>	<PAGE_SIZE></PAGE_SIZE>	<SORT_BY></SORT_BY>	<DESC></DESC><COMP_VERTICAL></COMP_VERTICAL></TA_SEARCHAGENCY_1A_INPUT>")
            objInputSearchXml.DocumentElement.SelectSingleNode("ACTION").InnerText = "S"


            If (Request.Form("txtAgencyName") <> "" And hdAgencyName.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyName.Value = "") Then
                objInputSearchXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = ""
            Else
                objInputSearchXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = hdAgencyName.Value.Trim()

            End If
            If objInputSearchXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText.Trim.Length <= 0 Then
                objInputSearchXml.DocumentElement.SelectSingleNode("NAME").InnerText = Request("txtAgencyName")
            End If


            If (dlstCity.SelectedIndex <> 0) Then
                objInputSearchXml.DocumentElement.SelectSingleNode("CITY_NAME").InnerText = Trim(dlstCity.SelectedItem.Text)
            End If
            If (dlstCountry.SelectedIndex <> 0) Then
                objInputSearchXml.DocumentElement.SelectSingleNode("COUNTRY_NAME").InnerText = Trim(dlstCountry.SelectedItem.Text)
            End If


            If (Request.Form("txtGroupName") <> "" And hdChainId.Value = "") Or (Request.Form("txtGroupName") = "" Or hdChainId.Value = "") Then
                objInputSearchXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = ""
            Else
                objInputSearchXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = hdChainId.Value.Trim()

            End If
            If objInputSearchXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText.Trim.Length <= 0 Then
                objInputSearchXml.DocumentElement.SelectSingleNode("GROUP_NAME").InnerText = txtGroupName.Text
            End If




            objInputSearchXml.DocumentElement.SelectSingleNode("OFFICEID").InnerText = txtOfficeID.Text

            If (dlstOnlineStatus.SelectedIndex <> 0) Then
                objInputSearchXml.DocumentElement.SelectSingleNode("ONLINE_STATUS").InnerText = Trim(dlstOnlineStatus.SelectedValue)
            End If

            If (dlstAoffice.SelectedIndex <> 0) Then
                objInputSearchXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = Trim(dlstAoffice.SelectedValue)
            End If
            If (dlstCRS.SelectedIndex <> 0) Then
                objInputSearchXml.DocumentElement.SelectSingleNode("CRS").InnerText = Trim(dlstCRS.SelectedValue) ' Trim(dlstCRS.SelectedValue)
            End If

            If (dlstAType.SelectedIndex <> 0) Then
                objInputSearchXml.DocumentElement.SelectSingleNode("AGENCY_TYPE").InnerText = Trim(dlstAType.SelectedItem.Text)
            End If

            If (dlstPriority.SelectedIndex <> 0) Then
                objInputSearchXml.DocumentElement.SelectSingleNode("PRIORITY").InnerText = Trim(dlstPriority.SelectedItem.Text)
            End If

            'If (dlstAResFrom.SelectedIndex <> 0) Then
            '    objInputSearchXml.DocumentElement.SelectSingleNode("RESP_ASSGN_FROM").InnerText = Trim(dlstAResFrom.SelectedItem.Text)
            'End If
            objInputSearchXml.DocumentElement.SelectSingleNode("RESP_ASSGN_FROM").InnerText = txtResFrom.Text

            'If (dlstAResTo.SelectedIndex <> 0) Then
            objInputSearchXml.DocumentElement.SelectSingleNode("RESP_ASSGN_TO").InnerText = txtResTo.Text
            'End If

            If DlstCompVertical.SelectedValue <> "" Then
                objInputSearchXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
            End If


            'Here Back end Method Call
            hdChechedItem.Value = "0"
            Session("DataSource") = Nothing
            ddlPageNumber.SelectedValue = 1
            hdUpdateForSessionXml.Value = ""



            If Inputstrxml.Trim.Length > 0 Then
                objInputSearchXml.LoadXml(Inputstrxml)
            End If

            objOutputSearchXml = objbzAgency1ARespChange.Search(objInputSearchXml)
            ' objOutputSearchXml.Save("C:\ARes3.xml")
            ' objOutputSearchXml.Load("C:\ARes3.xml")
            ' objOutputSearchXml.LoadXml("<IS_SEARCHPAYMENTPROCEED_OUTPUT><PAYMENTPROCEED  ISPOrderID='126' Month='2' Year='2002' SlNo='1' UserName='34535435'  NPID ='43' CostActivityMonth='5' ISPName='Sify Mail' AgencyName='Cox ruetheruit retuertiuergf utioreytioureteriotu' Address='D-14 fjkhr 78 fjkdtkjghr jkretrt'  City='Delhi' OfficeID='54654' Status='Pending' OnlineDate='02/05/2005' CancellationDate='02/05/2006'  StartDate='' EndDate=''  DaysUsed='23' ProRate='34' ISPRentalCharges='6'  VATPercentage='5' ISPInstallationCharges='10' VATAmount='20' PANumber='' ></PAYMENTPROCEED><PAYMENTPROCEED ISPOrderID='457' Month='3' Year='2003' SlNo='2' UserName='34535435'  NPID ='41' CostActivityMonth='5' ISPName='Sify Mail' AgencyName='Cox' Address='D-14'  City='Delhi' OfficeID='54654' Status='Pending' OnlineDate='02/05/2005' CancellationDate='02/05/2006'  StartDate='' EndDate=''  DaysUsed='23' ProRate='34' ISPRentalCharges='6'  VATPercentage='5' ISPInstallationCharges='14' VATAmount='40' PANumber='44' ></PAYMENTPROCEED><PAYMENTPROCEED ISPOrderID='123' SlNo='3' UserName='34535435'  NPID ='44' CostActivityMonth='5' ISPName='Sify Mail' AgencyName='Cox' Address='D-14'  City='Delhi' OfficeID='54654' Status='Pending' OnlineDate='02/05/2005' CancellationDate='02/05/2006'  StartDate='' EndDate=''  DaysUsed='23' ProRate='34' ISPRentalCharges='6'  VATPercentage='5' ISPInstallationCharges='10' VATAmount='50' PANumber='' ></PAYMENTPROCEED><PAYMENTPROCEED  ISPOrderID='234' SlNo='4' UserName='34535435'  NPID ='34' CostActivityMonth='5' ISPName='Sify Mail' AgencyName='Cox' Address='D-14'  City='Delhi' OfficeID='54654' Status='Pending' OnlineDate='02/05/2005' CancellationDate='02/05/2006'  StartDate='' EndDate=''  DaysUsed='23' ProRate='34' ISPRentalCharges='6'  VATPercentage='5' ISPInstallationCharges='10' VATAmount='70' PANumber='67' ></PAYMENTPROCEED><Errors Status='FALSE'><Error Code='' Description='' /></Errors></IS_SEARCHPAYMENTPROCEED_OUTPUT>")
            ViewState("InputXml") = Nothing
            If objOutputSearchXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("InputXml") = objInputSearchXml.OuterXml
                hdUpdateForSessionXml.Value = objOutputSearchXml.OuterXml
                objXmlReader = New XmlNodeReader(objOutputSearchXml)
                ds.ReadXml(objXmlReader)

                '@ Added Code for Sorting and Paging            
                Session("DataSource") = ds
                gvAResp.PageIndex = 0
                Sorting()
                '@ Added Code for Sorting and Paging             
                btnSelectAll.Visible = True
                btnDeSelectAll.Visible = False
                txtRecordCount.Text = objOutputSearchXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                SetImageForSorting(gvAResp)
                pnlPaging.Visible = True
                '  BindControlsForNavigation(gvAResp.PageCount)             
                BindControlsForNavigation(TotalPageNo)

            Else
                gvAResp.DataSource = Nothing
                gvAResp.DataBind()
                btnSelectAll.Visible = False
                btnDeSelectAll.Visible = False
                lblError.Text = objOutputSearchXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
                hdUpdateForSessionXml.Value = ""
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            objInputSearchXml = Nothing
            objOutputSearchXml = Nothing
            ' objbzISPOrder = Nothing
            hdUpdateForSessionXml.Value = ""
            If Session("Security") Is Nothing Then
                lblError.Text = "Session is expired."
            End If
        End Try
    End Sub

    Protected Sub btnSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        Try
            Dim objXmlReader As XmlNodeReader
            Dim ds2 As New DataSet
            Dim objOutputSearchXml As New XmlDocument

            Dim hdLcode As HiddenField
            Dim chkChecked As CheckBox
            Dim HdCheckedUnChecked As HiddenField
            Dim i As Integer
            'If (gvAResp.Rows.Count <= 0) Then
            '    lblError.Text = "There is no No Row For Selection."
            'End If
            For i = 0 To gvAResp.Rows.Count - 1
                hdLcode = gvAResp.Rows(i).FindControl("hdLCode")
                HdCheckedUnChecked = gvAResp.Rows(i).FindControl("HdCheckedUnChecked")
                chkChecked = CType(gvAResp.Rows(i).FindControl("chkChecked"), CheckBox)

                HdCheckedUnChecked.Value = "TRUE"

                If HdCheckedUnChecked.Value = "" Or HdCheckedUnChecked.Value.Trim.ToUpper = "FALSE" Then
                    chkChecked.Checked = False
                Else
                    chkChecked.Checked = True
                End If

            Next
            btnSelectAll.Visible = False
            btnDeSelectAll.Visible = True

            '@Now Update the SessionDataource as well as  hdUpdateForSessionXml

            If Session("DataSource") IsNot Nothing Then
                Dim dset As DataSet = CType(Session("DataSource"), DataSet)
                hdUpdateForSessionXml.Value = dset.GetXml()
                If hdUpdateForSessionXml.Value <> "" Then
                    objOutputSearchXml.LoadXml(hdUpdateForSessionXml.Value)

                    For Each objxmlnode As XmlNode In objOutputSearchXml.DocumentElement.SelectNodes("RESP")
                        objxmlnode.Attributes("GET_CHECKED").Value = "TRUE"
                    Next
                    hdUpdateForSessionXml.Value = objOutputSearchXml.OuterXml
                    objOutputSearchXml.LoadXml(hdUpdateForSessionXml.Value)
                    objXmlReader = New XmlNodeReader(objOutputSearchXml)
                    ds2.ReadXml(objXmlReader)
                    Session("DataSource") = ds2

                    If objOutputSearchXml.DocumentElement.SelectNodes("RESP[@GET_CHECKED ='TRUE']").Count > 0 Then
                        Session("Checked") = "TRUE"
                        hdChechedItem.Value = "1"
                    Else
                        Session("Checked") = "FALSE"
                        hdChechedItem.Value = "0"
                    End If
                    Sorting()
                    '@ End of Code For Sorting
                End If
                SetImageForSorting(gvAResp)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            hdUpdateForSessionXml.Value = ""
            If Session("Security") Is Nothing Then
                lblError.Text = "Session is expired."
            End If
        End Try
    End Sub

    Protected Sub btnDeSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeSelectAll.Click

        Dim objXmlReader As XmlNodeReader
        Dim ds2 As New DataSet
        Dim objOutputSearchXml As New XmlDocument
        Dim hdLcode As HiddenField
        Dim chkChecked As CheckBox
        Dim HdCheckedUnChecked As HiddenField
        Dim i As Integer
        Try
            'If (gvAResp.Rows.Count <= 0) Then
            '    lblError.Text = "There is no No Row For DeSelection."
            'End If

            For i = 0 To gvAResp.Rows.Count - 1
                hdLcode = gvAResp.Rows(i).FindControl("hdLCode")
                HdCheckedUnChecked = gvAResp.Rows(i).FindControl("HdCheckedUnChecked")
                chkChecked = CType(gvAResp.Rows(i).FindControl("chkChecked"), CheckBox)

                HdCheckedUnChecked.Value = ""

                If HdCheckedUnChecked.Value = "" Or HdCheckedUnChecked.Value.Trim.ToUpper = "FALSE" Then
                    chkChecked.Checked = False
                Else
                    chkChecked.Checked = True
                End If

            Next

            btnDeSelectAll.Visible = False
            btnSelectAll.Visible = True

            '@Now Update the SessionDataource as well as  hdUpdateForSessionXml

            If Session("DataSource") IsNot Nothing Then
                Dim dset As DataSet = CType(Session("DataSource"), DataSet)
                hdUpdateForSessionXml.Value = dset.GetXml()
                If hdUpdateForSessionXml.Value <> "" Then
                    objOutputSearchXml.LoadXml(hdUpdateForSessionXml.Value)

                    For Each objxmlnode As XmlNode In objOutputSearchXml.DocumentElement.SelectNodes("RESP")
                        objxmlnode.Attributes("GET_CHECKED").Value = ""
                    Next

                    hdUpdateForSessionXml.Value = objOutputSearchXml.OuterXml
                    objOutputSearchXml.LoadXml(hdUpdateForSessionXml.Value)
                    objXmlReader = New XmlNodeReader(objOutputSearchXml)
                    ds2.ReadXml(objXmlReader)
                    Session("DataSource") = ds2
                    Session("Checked") = "FALSE"
                    hdChechedItem.Value = "0"

                    '@ Code For Sorting
                    Sorting()


                End If
                SetImageForSorting(gvAResp)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            hdUpdateForSessionXml.Value = ""
            If Session("Security") Is Nothing Then
                lblError.Text = "Session is expired."
            End If

        End Try
    End Sub
#Region "BindAllControl()"
    Private Sub BindAllControl()
        Try
            objeAAMS.BindDropDown(dlstCity, "CITY", True, 3)
            objeAAMS.BindDropDown(dlstCountry, "COUNTRY", True, 3)
            objeAAMS.BindDropDown(dlstAoffice, "AOFFICE", True, 3)
            objeAAMS.BindDropDown(dlstAType, "AGENCYTYPE", True, 3)
            objeAAMS.BindDropDown(dlstOnlineStatus, "ONLINESTATUSCODE", True, 3)
            objeAAMS.BindDropDown(dlstPriority, "PRIORITY", True, 3)
            objeAAMS.BindDropDown(dlstCRS, "CRS", True, 3)
            'objeAAMS.BindDropDown(dlstAResFrom, "EMPLOYEE", True, 3)
            'objeAAMS.BindDropDown(dlstAResTo, "EMPLOYEE", True, 3)
            objeAAMS.BindDropDown(DlstCompVertical, "CompanyVertical", True, 3)
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub
#End Region

#Region "Code for Filter "
    'Code Added by Abhishek
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        'If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
        '    If Response.ContentType = "text/html" Then
        '        Response.Filter = New TrimStream(Response.Filter)
        '    End If
        'End If
    End Sub
    'Code Added by Abhishek
#End Region


#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            lblError.Text = ""
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            gvAResp.PageIndex = CInt(ddlPageNumber.SelectedValue) - 1
            changeSessionDataonpagingornavigation()
            If Session("DataSource") IsNot Nothing Then
                Sorting()
                SetImageForSorting(gvAResp)
            End If
            '  BindControlsForNavigation(gvAResp.PageCount)
            BindControlsForNavigation(TotalPageNo)
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            If Session("Security") Is Nothing Then
                lblError.Text = "Session is expired."
            End If
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            lblError.Text = ""
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            gvAResp.PageIndex = CInt(ddlPageNumber.SelectedValue) - 1

            changeSessionDataonpagingornavigation()
            If Session("DataSource") IsNot Nothing Then
                Sorting()
                SetImageForSorting(gvAResp)
            End If
            'BindControlsForNavigation(gvAResp.PageCount)
            BindControlsForNavigation(TotalPageNo)
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            If Session("Security") Is Nothing Then
                lblError.Text = "Session is expired."
            End If
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            lblError.Text = ""
            gvAResp.PageIndex = CInt(ddlPageNumber.SelectedValue) - 1
            changeSessionDataonpagingornavigation()
            If Session("DataSource") IsNot Nothing Then
                Sorting()
                SetImageForSorting(gvAResp)
            End If
            '   BindControlsForNavigation(gvAResp.PageCount)
            BindControlsForNavigation(TotalPageNo)
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            If Session("Security") Is Nothing Then
                lblError.Text = "Session is expired."
            End If
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

   
    Protected Sub chkChecked_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objXmlReader As XmlNodeReader
        Dim ds2 As New DataSet
        Dim objOutputSearchXml As New XmlDocument
        Dim hdLcode As HiddenField
        Dim chkChecked As CheckBox
        Dim HdCheckedUnChecked As HiddenField

        Try

            chkChecked = CType(sender, CheckBox)
            Dim index As Integer = Integer.Parse(chkChecked.Attributes("GridRowNo"))
            Dim gvrow As GridViewRow = gvAResp.Rows(index)
            hdLcode = gvrow.FindControl("hdLCode")
            HdCheckedUnChecked = gvrow.FindControl("HdCheckedUnChecked")

            '@Now Update the SessionDataource as well as  hdUpdateForSessionXml

            If Session("DataSource") IsNot Nothing Then
                Dim dset As DataSet = CType(Session("DataSource"), DataSet)
                hdUpdateForSessionXml.Value = dset.GetXml()
                If hdUpdateForSessionXml.Value <> "" Then
                    objOutputSearchXml.LoadXml(hdUpdateForSessionXml.Value)

                    For Each objxmlnode As XmlNode In objOutputSearchXml.DocumentElement.SelectNodes("RESP[@LOCATION_CODE='" + hdLcode.Value + "']")
                        ' objxmlnode.Attributes("GET_CHECKED").Value = ""
                        If chkChecked.Checked Then
                            objxmlnode.Attributes("GET_CHECKED").Value = "True"
                        Else
                            objxmlnode.Attributes("GET_CHECKED").Value = ""
                        End If
                    Next

                    hdUpdateForSessionXml.Value = objOutputSearchXml.OuterXml
                    objOutputSearchXml.LoadXml(hdUpdateForSessionXml.Value)
                    objXmlReader = New XmlNodeReader(objOutputSearchXml)
                    ds2.ReadXml(objXmlReader)
                    Session("DataSource") = ds2

                    If objOutputSearchXml.DocumentElement.SelectNodes("RESP[@GET_CHECKED ='True']").Count > 0 Then
                        Session("Checked") = "TRUE"
                        hdChechedItem.Value = "1"
                    Else
                        Session("Checked") = "FALSE"
                        hdChechedItem.Value = "0"
                    End If


                End If
                SetImageForSorting(gvAResp)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            hdUpdateForSessionXml.Value = ""
            If Session("Security") Is Nothing Then
                lblError.Text = "Session is expired."
            End If
        End Try

    End Sub

    Protected Sub gvAResp_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvAResp.Sorting
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
            If Session("DataSource") IsNot Nothing Then
                Sorting()
                SetImageForSorting(gvAResp)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            If Session("Security") Is Nothing Then
                lblError.Text = "Session is expired."
            End If
        End Try
    End Sub
    Protected Sub gvAResp_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAResp.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim hdLcode As HiddenField
                Dim chkChecked As CheckBox
                Dim HdCheckedUnChecked As HiddenField

                hdLcode = e.Row.FindControl("hdLCode")
                HdCheckedUnChecked = e.Row.FindControl("HdCheckedUnChecked")
                chkChecked = CType(e.Row.FindControl("chkChecked"), CheckBox)

                If HdCheckedUnChecked.Value = "" Or HdCheckedUnChecked.Value.Trim.ToUpper = "FALSE" Then
                    chkChecked.Checked = False
                Else
                    chkChecked.Checked = True
                End If
                chkChecked.Attributes.Add("GridRowNo", e.Row.RowIndex.ToString)

                Dim lnkHistory As New LinkButton
                lnkHistory = e.Row.FindControl("lnkHistory")
                lnkHistory.Attributes.Add("onclick", "javascript:return HistoryForResUpdate('" + objED.Encrypt(hdLcode.Value) + "')")

            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub BtnAllocate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAllocate.Click
        Try
            Dim objXmlReader As XmlNodeReader
            Dim ds2 As New DataSet
            Dim objOutputSearchXml As New XmlDocument
            Dim objInputSearchXml As New XmlDocument

            Dim objbzAgency1ARespChange As New AAMS.bizTravelAgency.bzAgency1ARespChange
            objInputSearchXml.LoadXml("<TA_UPDATEAGENCY_1A_INPUT><ACTION>U</ACTION><LOCATION_CODE></LOCATION_CODE>	<NAME></NAME>	<CHAIN_CODE></CHAIN_CODE>	<GROUP_NAME ></GROUP_NAME>	<OFFICEID></OFFICEID>	<CITY_NAME></CITY_NAME>	<COUNTRY_NAME></COUNTRY_NAME>	<ONLINE_STATUS></ONLINE_STATUS>	<AOFFICE></AOFFICE>	<CRS></CRS>	<AGENCY_TYPE></AGENCY_TYPE>	<PRIORITY></PRIORITY>	<RESP_ASSGN_FROM></RESP_ASSGN_FROM>	<RESP_ASSGN_TO></RESP_ASSGN_TO><RESP_ASSGN_TO_ID></RESP_ASSGN_TO_ID><EMPLOYEEID></EMPLOYEEID></TA_UPDATEAGENCY_1A_INPUT>")

            If (gvAResp.Rows.Count <= 0) Then
                lblError.Text = "There is no item is selected for Allocation."
            End If
            changeSessionDataonpagingornavigation()
            Dim strAssignFrom As String = ""
            Dim strLCode As String = ""
            If Session("DataSource") IsNot Nothing Then
                Dim dset As DataSet = CType(Session("DataSource"), DataSet)
                hdUpdateForSessionXml.Value = dset.GetXml()
                If hdUpdateForSessionXml.Value <> "" Then
                    objOutputSearchXml.LoadXml(hdUpdateForSessionXml.Value)
                    For Each objxmlnode As XmlNode In objOutputSearchXml.DocumentElement.SelectNodes("RESP")
                        If objxmlnode.Attributes("GET_CHECKED").Value.Trim.ToUpper = "TRUE" Then

                            If strLCode.Trim.Length = 0 Then
                                strLCode = objxmlnode.Attributes("LOCATION_CODE").Value
                            Else
                                strLCode = strLCode + "," + objxmlnode.Attributes("LOCATION_CODE").Value
                            End If

                            If strAssignFrom.Trim.Length = 0 Then
                                strAssignFrom = objxmlnode.Attributes("RESP_ASSGN_FROM").Value
                            Else
                                strAssignFrom = strAssignFrom + "," + objxmlnode.Attributes("RESP_ASSGN_FROM").Value
                            End If
                        End If
                    Next
                    objInputSearchXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = strLCode
                    ' objInputSearchXml.DocumentElement.SelectSingleNode("RESP_ASSGN_FROM").InnerText = strAssignFrom
                    ' If dlstAResTo.SelectedValue <> "" Then
                    objInputSearchXml.DocumentElement.SelectSingleNode("RESP_ASSGN_TO").InnerText = txtResTo.Text ' dlstAResTo.SelectedItem.Text

                    'End If

                    objInputSearchXml.DocumentElement.SelectSingleNode("RESP_ASSGN_TO_ID").InnerText = hdResTo.Value

                    If Not Session("LoginSession") Is Nothing Then
                        objInputSearchXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = Session("LoginSession").ToString().Split("|")(0)
                    End If

                    If strLCode.Trim.Length = 0 Then
                        lblError.Text = "There is no item is selected for Allocation."
                        SetImageForSorting(gvAResp)
                        Exit Sub
                    End If

                    objOutputSearchXml = objbzAgency1ARespChange.Update(objInputSearchXml)
                    If objOutputSearchXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        'If ViewState("InputXml") IsNot Nothing Then
                        '    SearchData(ViewState("InputXml").ToString)
                        'End If

                        Dim dsets As DataSet = CType(Session("DataSource"), DataSet)
                        hdUpdateForSessionXml.Value = dsets.GetXml()
                        dsets = Nothing
                        If hdUpdateForSessionXml.Value <> "" Then
                            objOutputSearchXml.LoadXml(hdUpdateForSessionXml.Value)

                            For Each objxmlnode As XmlNode In objOutputSearchXml.DocumentElement.SelectNodes("RESP")

                                If objxmlnode.Attributes("GET_CHECKED").Value.Trim.ToUpper = "TRUE" Then
                                    ' If dlstAResTo.SelectedValue <> "" Then
                                    objxmlnode.Attributes("RESP_ASSGN_FROM").Value = txtResTo.Text ' dlstAResTo.SelectedItem.Text
                                    'Else
                                    'objxmlnode.Attributes("RESP_ASSGN_FROM").Value = ""
                                    ' End If
                                End If


                                objxmlnode.Attributes("GET_CHECKED").Value = ""  'Whether to uncheck all selected item or not

                            Next

                            ds2 = New DataSet
                            objXmlReader = New XmlNodeReader(objOutputSearchXml)
                            ds2.ReadXml(objXmlReader)
                            Session("DataSource") = ds2

                            Sorting()
                            btnSelectAll.Visible = True
                            btnDeSelectAll.Visible = False

                        End If
                        lblError.Text = objeAAMSMessage.messUpdate
                    Else
                        lblError.Text = objOutputSearchXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            hdUpdateForSessionXml.Value = ""
            If Session("Security") Is Nothing Then
                lblError.Text = "Session is expired."
            End If
        End Try
    End Sub

    Protected Sub BtnDeAllocate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDeAllocate.Click
        Try
            Dim objXmlReader As XmlNodeReader
            Dim ds2 As New DataSet
            Dim objOutputSearchXml As New XmlDocument
            Dim objInputSearchXml As New XmlDocument
            Dim objbzAgency1ARespChange As New AAMS.bizTravelAgency.bzAgency1ARespChange
            objInputSearchXml.LoadXml("<TA_UPDATEAGENCY_1A_INPUT><ACTION>D</ACTION><LOCATION_CODE></LOCATION_CODE>	<NAME></NAME>	<CHAIN_CODE></CHAIN_CODE>	<GROUP_NAME ></GROUP_NAME>	<OFFICEID></OFFICEID>	<CITY_NAME></CITY_NAME>	<COUNTRY_NAME></COUNTRY_NAME>	<ONLINE_STATUS></ONLINE_STATUS>	<AOFFICE></AOFFICE>	<CRS></CRS>	<AGENCY_TYPE></AGENCY_TYPE>	<PRIORITY></PRIORITY>	<RESP_ASSGN_FROM></RESP_ASSGN_FROM>	<RESP_ASSGN_TO></RESP_ASSGN_TO><RESP_ASSGN_TO_ID></RESP_ASSGN_TO_ID><EMPLOYEEID></EMPLOYEEID></TA_UPDATEAGENCY_1A_INPUT>")


            If (gvAResp.Rows.Count <= 0) Then
                lblError.Text = "There is no item is selected for Allocation."
            End If

            Dim strAssignFrom As String = ""
            Dim strLCode As String = ""

            changeSessionDataonpagingornavigation()

            If Session("DataSource") IsNot Nothing Then
                Dim dset As DataSet = CType(Session("DataSource"), DataSet)
                hdUpdateForSessionXml.Value = dset.GetXml()
                If hdUpdateForSessionXml.Value <> "" Then
                    objOutputSearchXml.LoadXml(hdUpdateForSessionXml.Value)
                    For Each objxmlnode As XmlNode In objOutputSearchXml.DocumentElement.SelectNodes("RESP")
                        If objxmlnode.Attributes("GET_CHECKED").Value.Trim.ToUpper = "TRUE" Then
                            If strLCode.Trim.Length = 0 Then
                                strLCode = objxmlnode.Attributes("LOCATION_CODE").Value
                            Else
                                strLCode = strLCode + "," + objxmlnode.Attributes("LOCATION_CODE").Value
                            End If

                            If strAssignFrom.Trim.Length = 0 Then
                                strAssignFrom = objxmlnode.Attributes("RESP_ASSGN_FROM").Value
                            Else
                                strAssignFrom = strAssignFrom + "," + objxmlnode.Attributes("RESP_ASSGN_FROM").Value
                            End If

                        End If
                    Next

                    objInputSearchXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = strLCode
                    ' objInputSearchXml.DocumentElement.SelectSingleNode("RESP_ASSGN_FROM").InnerText = strAssignFrom
                    ' If dlstAResTo.SelectedValue <> "" Then
                    objInputSearchXml.DocumentElement.SelectSingleNode("RESP_ASSGN_TO").InnerText = txtResTo.Text 'dlstAResTo.SelectedItem.Text
                    'End If

                    If strLCode.Trim.Length = 0 Then
                        lblError.Text = "There is no item is selected for Allocation."
                        Exit Sub
                        SetImageForSorting(gvAResp)
                    End If

                    If Not Session("LoginSession") Is Nothing Then
                        objInputSearchXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = Session("LoginSession").ToString().Split("|")(0)
                    End If

                    objOutputSearchXml = objbzAgency1ARespChange.Deallocate(objInputSearchXml)

                    If objOutputSearchXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        'If ViewState("InputXml") IsNot Nothing Then
                        '    SearchData(ViewState("InputXml").ToString)
                        'End If
                        Dim dsets As DataSet = CType(Session("DataSource"), DataSet)
                        hdUpdateForSessionXml.Value = dsets.GetXml()
                        dsets = Nothing
                        If hdUpdateForSessionXml.Value <> "" Then
                            objOutputSearchXml.LoadXml(hdUpdateForSessionXml.Value)

                            For Each objxmlnode As XmlNode In objOutputSearchXml.DocumentElement.SelectNodes("RESP")
                                If objxmlnode.Attributes("GET_CHECKED").Value.Trim.ToUpper = "TRUE" Then
                                    objxmlnode.Attributes("RESP_ASSGN_FROM").Value = ""
                                End If
                                objxmlnode.Attributes("GET_CHECKED").Value = ""  'Whether to uncheck all selected item or not

                            Next

                            ds2 = New DataSet
                            objXmlReader = New XmlNodeReader(objOutputSearchXml)
                            ds2.ReadXml(objXmlReader)
                            Session("DataSource") = ds2
                            Sorting()
                            btnSelectAll.Visible = True
                            btnDeSelectAll.Visible = False

                        End If
                        lblError.Text = objeAAMSMessage.messUpdate
                    Else
                        lblError.Text = objOutputSearchXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            hdUpdateForSessionXml.Value = ""
            If Session("Security") Is Nothing Then
                lblError.Text = "Session is expired."
            End If
        End Try
    End Sub

    Private Sub Sorting()
        Dim dv As DataView
        Dim intPageSize As Integer
        Dim intRowCount As Integer = 0
        Dim dtTemp As New DataTable
        intPageSize = CInt(ConfigurationManager.AppSettings("PAGE_SIZE").ToString)
        Try
            If Session("DataSource") IsNot Nothing Then
                Dim ds As DataSet = CType(Session("DataSource"), DataSet)
                '@Making Struture of the new table ( For User defined data struture
                Dim dtNew As New DataTable
                dtNew.TableName = "RESP"
                For Each clmn As DataColumn In ds.Tables("RESP").Columns
                    Dim c1 As New DataColumn(clmn.ColumnName)
                    c1.AllowDBNull = True
                    If c1.ColumnName = "LOCATION_CODE" Or c1.ColumnName = "CHAIN_CODE" Then
                        c1.DataType = GetType(Int32)
                    Else
                        c1.DataType = GetType(String)
                    End If
                    dtNew.Columns.Add(c1)
                Next

                '@Making Struture of the new table ( For User defined data struture

                '@ Code For making sorted dataview

                For introwno As Integer = 0 To ds.Tables("RESP").Rows.Count - 1
                    Dim row As DataRow = dtNew.NewRow()
                    row.ItemArray = ds.Tables("RESP").Rows(introwno).ItemArray
                    dtNew.Rows.Add(row)
                Next
                dtNew.AcceptChanges()
                dv = dtNew.DefaultView
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "LOCATION_CODE"
                End If
                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                End If

                If ViewState("Desc") = "TRUE" Then
                    dv.Sort = ViewState("SortName").ToString + "    DESC "
                Else
                    dv.Sort = ViewState("SortName").ToString + "    ASC "
                End If

                '@Code For making sorted dataview
                '###########################################


                '@ Code for Filtering the pagesize record from all record 
                ' #########################################
                intRowCount = ds.Tables("RESP").Rows.Count
                Dim count As Integer = Math.Ceiling(intRowCount / intPageSize)
                Dim selectedValue As String = IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue)
                Dim PageNo As Integer
                PageNo = selectedValue
                Dim intStartRecordNo As Integer = 0
                Dim intEndRecordNo As Integer = 0
                intStartRecordNo = (PageNo - 1) * intPageSize
                intEndRecordNo = PageNo * intPageSize - 1

                For i As Integer = 0 To dv.Table.Columns.Count - 1
                    dtTemp.Columns.Add(dv.Table.Columns(i).ColumnName)
                Next

                For j As Integer = intStartRecordNo To intEndRecordNo
                    If intRowCount > j Then
                        Dim row As DataRow = dtTemp.NewRow()
                        row.ItemArray = dv.Item(j).Row.ItemArray 'dv.ids.Tables("RESP").Rows(j).ItemArray
                        dtTemp.Rows.Add(row)
                    End If
                Next

                '###########################################
                '@ Code for Filtering the pagesize record from all record 
                ' #########################################

                dtTemp.AcceptChanges()
                ' dv = dtNew.DefaultView
                dv = dtTemp.DefaultView 'ds.Tables("RESP").DefaultView 
                '@ End of Code For Sorting

                gvAResp.DataSource = dv
                gvAResp.DataBind()
            End If
        Catch ex As Exception
        Finally
            If Session("Security") Is Nothing Then
                lblError.Text = "Session is expired."
            End If
        End Try

    End Sub

   
    Private Function TotalPageNo() As Integer
        Dim Pagecount As Integer
        Pagecount = 1
        Try
            If Session("DataSource") IsNot Nothing Then
                Dim ds As DataSet = CType(Session("DataSource"), DataSet)
                Dim intRowCount As Integer
                Dim intPageSize As Integer
                intRowCount = ds.Tables("RESP").Rows.Count
                intPageSize = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
                Pagecount = Math.Ceiling(intRowCount / intPageSize)
            End If
        Catch ex As Exception
        End Try
        Return Pagecount
    End Function

    Private Sub changeSessionDataonpagingornavigation()
        Dim objOutputSearchXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim hdLcode As HiddenField
        Dim chkChecked As CheckBox
        Dim ds2 As New DataSet
        Dim HdCheckedUnChecked As HiddenField
        Dim i As Integer
        If Session("DataSource") IsNot Nothing Then
            Dim dset As DataSet = CType(Session("DataSource"), DataSet)
            hdUpdateForSessionXml.Value = dset.GetXml()
            If hdUpdateForSessionXml.Value <> "" Then
                objOutputSearchXml.LoadXml(hdUpdateForSessionXml.Value)
                For i = 0 To gvAResp.Rows.Count - 1
                    hdLcode = gvAResp.Rows(i).FindControl("hdLCode")
                    HdCheckedUnChecked = gvAResp.Rows(i).FindControl("HdCheckedUnChecked")
                    chkChecked = CType(gvAResp.Rows(i).FindControl("chkChecked"), CheckBox)
                    For Each objxmlnode As XmlNode In objOutputSearchXml.DocumentElement.SelectNodes("RESP[@LOCATION_CODE='" + hdLcode.Value + "']")
                        ' objxmlnode.Attributes("GET_CHECKED").Value = ""
                        If chkChecked.Checked Then
                            objxmlnode.Attributes("GET_CHECKED").Value = "TRUE"
                        Else
                            objxmlnode.Attributes("GET_CHECKED").Value = ""
                        End If
                    Next
                Next
                hdUpdateForSessionXml.Value = objOutputSearchXml.OuterXml
                objOutputSearchXml.LoadXml(hdUpdateForSessionXml.Value)
                objXmlReader = New XmlNodeReader(objOutputSearchXml)
                ds2.ReadXml(objXmlReader)
                Session("DataSource") = ds2
                hdUpdateForSessionXml.Value = ""

            End If
        End If
    End Sub

  
  
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.ToString, False)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CheckAllForPageLevel()
        Dim objOutputSearchXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim hdLcode As HiddenField
        Dim chkChecked As CheckBox
        Dim ds2 As New DataSet
        Dim HdCheckedUnChecked As HiddenField
        Dim i As Integer
        Try
            If Session("DataSource") IsNot Nothing Then
                Dim dset As DataSet = CType(Session("DataSource"), DataSet)
                hdUpdateForSessionXml.Value = dset.GetXml()
                If hdUpdateForSessionXml.Value <> "" Then
                    objOutputSearchXml.LoadXml(hdUpdateForSessionXml.Value)
                    For i = 0 To gvAResp.Rows.Count - 1
                        hdLcode = gvAResp.Rows(i).FindControl("hdLCode")
                        HdCheckedUnChecked = gvAResp.Rows(i).FindControl("HdCheckedUnChecked")
                        chkChecked = CType(gvAResp.Rows(i).FindControl("chkChecked"), CheckBox)
                        HdCheckedUnChecked.Value = "TRUE"
                        If HdCheckedUnChecked.Value = "" Or HdCheckedUnChecked.Value.Trim.ToUpper = "FALSE" Then
                            chkChecked.Checked = False
                        Else
                            chkChecked.Checked = True
                        End If
                        For Each objxmlnode As XmlNode In objOutputSearchXml.DocumentElement.SelectNodes("RESP[@LOCATION_CODE='" + hdLcode.Value + "']")
                            ' objxmlnode.Attributes("GET_CHECKED").Value = ""
                            If chkChecked.Checked Then
                                objxmlnode.Attributes("GET_CHECKED").Value = "TRUE"
                            Else
                                objxmlnode.Attributes("GET_CHECKED").Value = ""
                            End If
                        Next
                    Next
                    hdUpdateForSessionXml.Value = objOutputSearchXml.OuterXml
                    objOutputSearchXml.LoadXml(hdUpdateForSessionXml.Value)
                    objXmlReader = New XmlNodeReader(objOutputSearchXml)
                    ds2.ReadXml(objXmlReader)
                    Session("DataSource") = ds2

                    If objOutputSearchXml.DocumentElement.SelectNodes("RESP[@GET_CHECKED ='TRUE']").Count > 0 Then
                        Session("Checked") = "TRUE"
                        hdChechedItem.Value = "1"
                    Else
                        Session("Checked") = "FALSE"
                        hdChechedItem.Value = "0"
                    End If
                    Sorting()
                    SetImageForSorting(gvAResp)
                    hdUpdateForSessionXml.Value = ""
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            If Session("Security") Is Nothing Then
                lblError.Text = "Session is expired."
            End If
        End Try
    End Sub
    Private Sub UncheckCheckAllForPageLevel()
        Dim objOutputSearchXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim hdLcode As HiddenField
        Dim chkChecked As CheckBox
        Dim ds2 As New DataSet
        Dim HdCheckedUnChecked As HiddenField

        Dim i As Integer
        Try
            If Session("DataSource") IsNot Nothing Then
                Dim dset As DataSet = CType(Session("DataSource"), DataSet)
                hdUpdateForSessionXml.Value = dset.GetXml()
                If hdUpdateForSessionXml.Value <> "" Then
                    objOutputSearchXml.LoadXml(hdUpdateForSessionXml.Value)
                    For i = 0 To gvAResp.Rows.Count - 1
                        hdLcode = gvAResp.Rows(i).FindControl("hdLCode")
                        HdCheckedUnChecked = gvAResp.Rows(i).FindControl("HdCheckedUnChecked")
                        chkChecked = CType(gvAResp.Rows(i).FindControl("chkChecked"), CheckBox)
                        HdCheckedUnChecked.Value = ""
                        If HdCheckedUnChecked.Value = "" Or HdCheckedUnChecked.Value.Trim.ToUpper = "FALSE" Then
                            chkChecked.Checked = False
                        Else
                            chkChecked.Checked = True
                        End If
                        For Each objxmlnode As XmlNode In objOutputSearchXml.DocumentElement.SelectNodes("RESP[@LOCATION_CODE='" + hdLcode.Value + "']")
                            ' objxmlnode.Attributes("GET_CHECKED").Value = ""
                            If chkChecked.Checked Then
                                objxmlnode.Attributes("GET_CHECKED").Value = "TRUE"
                            Else
                                objxmlnode.Attributes("GET_CHECKED").Value = ""
                            End If
                        Next
                    Next
                    hdUpdateForSessionXml.Value = objOutputSearchXml.OuterXml
                    objOutputSearchXml.LoadXml(hdUpdateForSessionXml.Value)
                    objXmlReader = New XmlNodeReader(objOutputSearchXml)
                    ds2.ReadXml(objXmlReader)
                    Session("DataSource") = ds2
                    If objOutputSearchXml.DocumentElement.SelectNodes("RESP[@GET_CHECKED ='TRUE']").Count > 0 Then
                        Session("Checked") = "TRUE"
                        hdChechedItem.Value = "1"
                    Else
                        Session("Checked") = "FALSE"
                        hdChechedItem.Value = "0"
                    End If
                    Sorting()
                    SetImageForSorting(gvAResp)
                    hdUpdateForSessionXml.Value = ""
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            If Session("Security") Is Nothing Then
                lblError.Text = "Session is expired."
            End If
        End Try
    End Sub
    Protected Sub chkCheckedHeader_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            Dim chkChecked As CheckBox
            chkChecked = CType(sender, CheckBox)
            If chkChecked.Checked = True Then

                CheckAllForPageLevel()
                chkChecked.Checked = True

                CType(gvAResp.HeaderRow.FindControl("chkCheckedHeader"), CheckBox).Checked = True
            Else

                UncheckCheckAllForPageLevel()
                chkChecked.Checked = False
                CType(gvAResp.HeaderRow.FindControl("chkCheckedHeader"), CheckBox).Checked = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally

            If Session("Security") Is Nothing Then
                lblError.Text = "Session is expired."
            End If
        End Try

    End Sub
End Class
