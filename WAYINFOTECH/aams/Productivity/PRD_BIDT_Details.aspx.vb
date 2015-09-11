Imports System.IO
Imports System.Text
Partial Class Productivity_PRD_BIDT_Details
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim FooterDataset As DataSet
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            FooterDataset = New DataSet
            btnPrint.Attributes.Add("onclick", "CallPrint('grdvBidtDetails')")
            txtTotlaProductivity.Text = "0"
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            btnSearch.Attributes.Add("onclick", "return CheckValidation();")
            btnExport.Attributes.Add("onclick", "return CheckValidation();")
            If Not IsPostBack Then


                Dim strBuilder As New StringBuilder
                Dim objSecurityXml As New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity Details']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity Details']").Attributes("Value").Value)
                        If strBuilder(0) = "0" Then
                            btnSearch.Enabled = False
                            Response.Redirect("~/NoRights.aspx")
                            Exit Sub
                        End If
                        If strBuilder(4) = "0" Then
                            btnExport.Enabled = False
                        End If
                    End If
                Else
                    strBuilder = objeAAMS.SecurityCheck(31)
                End If
                objSecurityXml = New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A_Productivity_Air']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A_Productivity_Air']").Attributes("Value").Value)
                        If strBuilder(0) = "0" Then
                            ChkABooking.Items(0).Enabled = False
                            ChkABooking.Items(0).Selected = False
                        End If
                    End If
                Else
                    strBuilder = objeAAMS.SecurityCheck(31)
                End If
                objSecurityXml = New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A_Productivity_Car']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A_Productivity_Car']").Attributes("Value").Value)
                        If strBuilder(0) = "0" Then
                            ChkABooking.Items(1).Enabled = False

                        End If
                    End If
                Else
                    strBuilder = objeAAMS.SecurityCheck(31)
                End If
                objSecurityXml = New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A_Productivity_Hotel']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A_Productivity_Hotel']").Attributes("Value").Value)
                        If strBuilder(0) = "0" Then
                            ChkABooking.Items(2).Enabled = False
                        End If
                    End If
                Else
                    strBuilder = objeAAMS.SecurityCheck(31)
                End If
                objSecurityXml = New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A_Productivity_Insurance']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A_Productivity_Insurance']").Attributes("Value").Value)
                        If strBuilder(0) = "0" Then
                            ChkABooking.Items(3).Enabled = False
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
                    ChkOrignalBook.Checked = False
                End If

            End If

            If Not IsPostBack Then
                BindAllControl()
                ' var parameter="Fmonth=" + FMonth  + 
                '"&TMonth=" + TMonth + "&FYear=" + FYear + .
                ' "&TYear=" + TYear +  "&Lcode=" + Lcode + 
                ' "&Aoff=" + Aoff +  "&GrData=" + GrData +  
                '"&UseOrig=" + UseOrig  +  "&ResId=" + ResId  +  
                '"&Air=" + Air  +  "&Car=" + Car +  "&Hotel=" 
                '+ Hotel +  "&Insurance=" + Insurance + "&LimAoff=" + LimAoff + 
                '"&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff ;
                If Request.QueryString("Fmonth") IsNot Nothing Then
                    drpMonthFrom.SelectedValue = Request.QueryString("Fmonth").ToString
                End If
                If Request.QueryString("TMonth") IsNot Nothing Then
                    drpMonthTo.SelectedValue = Request.QueryString("TMonth").ToString
                End If
                If Request.QueryString("FYear") IsNot Nothing Then
                    drpYearFrom.SelectedValue = Request.QueryString("FYear").ToString
                End If
                If Request.QueryString("TYear") IsNot Nothing Then
                    drpYearTo.SelectedValue = Request.QueryString("TYear").ToString
                End If
                If Request.QueryString("Air") IsNot Nothing Then
                    If Request.QueryString("Air").ToString = "1" Then
                        ChkABooking.Items(0).Selected = True
                    Else
                        ChkABooking.Items(0).Selected = False
                    End If
                End If
                If Request.QueryString("Car") IsNot Nothing Then
                    If Request.QueryString("Car").ToString = "1" Then
                        ChkABooking.Items(1).Selected = True
                    Else
                        ChkABooking.Items(1).Selected = False
                    End If

                End If
                If Request.QueryString("Hotel") IsNot Nothing Then
                    If Request.QueryString("Hotel").ToString = "1" Then
                        ChkABooking.Items(2).Selected = True
                    Else
                        ChkABooking.Items(2).Selected = False
                    End If

                End If
                If Request.QueryString("Insurance") IsNot Nothing Then
                    If Request.QueryString("Insurance").ToString = "1" Then
                        ChkABooking.Items(3).Selected = True
                    Else
                        ChkABooking.Items(3).Selected = False
                    End If
                End If
                If Request.QueryString("Agency") IsNot Nothing Then
                    txtAgencyName.Text = Request.QueryString("Agency").ToString
                    txtAgencyName.Text = Server.HtmlDecode(txtAgencyName.Text)
                End If
                If Request.QueryString("Add") IsNot Nothing Then
                    txtAdd.Text = Request.QueryString("Add").ToString
                    txtAdd.Text = Server.HtmlDecode(txtAdd.Text)
                End If
                If Request.QueryString("City") IsNot Nothing Then
                    txtCity.Text = Request.QueryString("City").ToString
                End If
                If Request.QueryString("Country") IsNot Nothing Then
                    txtCountry.Text = Request.QueryString("Country").ToString
                End If
                If Request.QueryString("UseOrig") IsNot Nothing Then
                    If Request.QueryString("UseOrig").ToString = "Y" Then
                        ChkOrignalBook.Checked = True
                    Else
                        ChkOrignalBook.Checked = False
                    End If
                End If
                If Request.QueryString("GrData") IsNot Nothing Then
                    If Request.QueryString("GrData").ToString = "1" Then
                        ChkWholeGroup.Checked = True
                    Else
                        ChkWholeGroup.Checked = False
                    End If
                End If
                txtTotlaProductivity.Text = "0"
                If Request.QueryString("Popup2") Is Nothing Then
                    BIDTDetailsSearch()
                End If

            End If

            If Not IsPostBack Then
                AgencyView()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BIDTDetailsSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzbzBIDT As New AAMS.bizProductivity.bzBIDT
        Try
            txtTotlaProductivity.Text = "0"
            ' objInputXml.LoadXml("<PR_SEARCH_PR_1A_PRODUCTIVITY_DETAILS_BIDT_INPUT><FMONTH/><FYEAR/><TMONTH/><TYEAR/><LCODE/><AOFFICE/><GROUPDATA/><USEORIGINAL/><RESPONSIBLESTAFFID/><AIR/><HOTEL/><CAR/><INSURANCE/><LIMITED_TO_AOFFICE/><LIMITED_TO_REGION/><LIMITED_TO_OWNAAGENCY/></PR_SEARCH_PR_1A_PRODUCTIVITY_DETAILS_BIDT_INPUT>")

            objInputXml.LoadXml("<PR_SEARCH_PR_1A_PRODUCTIVITY_DETAILS_BIDT_INPUT><FMONTH/><FYEAR/><TMONTH/><TYEAR/><LCODE/><AOFFICE/><GROUPDATA/><USEORIGINAL/><RESPONSIBLESTAFFID/><AIR/><HOTEL/><CAR/><INSURANCE/><LIMITED_TO_AOFFICE/><LIMITED_TO_REGION/><LIMITED_TO_OWNAAGENCY/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></PR_SEARCH_PR_1A_PRODUCTIVITY_DETAILS_BIDT_INPUT>")
            '<PR_SEARCH_PR_1A_PRODUCTIVITY_DETAILS_BIDT_INPUT>
            '<FMONTH/><FYEAR/><TMONTH/><TYEAR/><LCODE/><AOFFICE/>
            '<GROUPDATA/><USEORIGINAL/><RESPONSIBLESTAFFID/>
            '<AIR/><HOTEL/><CAR/><INSURANCE/><LIMITED_TO_AOFFICE/>
            '<LIMITED_TO_REGION/><LIMITED_TO_OWNAAGENCY/>
            ' <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
            '</PR_SEARCH_PR_1A_PRODUCTIVITY_DETAILS_BIDT_INPUT>
            objInputXml.DocumentElement.SelectSingleNode("FMONTH").InnerText = drpMonthFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("TMONTH").InnerText = drpMonthTo.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("FYEAR").InnerText = drpYearFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("TYEAR").InnerText = drpYearTo.SelectedValue



            If Request.QueryString("Lcode") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = objED.Decrypt(Request.QueryString("Lcode").ToString())
            End If
            If ChkABooking.Items(0).Selected = True Then
                objInputXml.DocumentElement.SelectSingleNode("AIR").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("AIR").InnerText = 0
            End If
            If ChkABooking.Items(1).Selected = True Then
                objInputXml.DocumentElement.SelectSingleNode("CAR").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("CAR").InnerText = 0

            End If
            If ChkABooking.Items(2).Selected = True Then
                objInputXml.DocumentElement.SelectSingleNode("HOTEL").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("HOTEL").InnerText = 0
            End If
            If ChkABooking.Items(3).Selected = True Then
                objInputXml.DocumentElement.SelectSingleNode("INSURANCE").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("INSURANCE").InnerText = 0
            End If
            If Request.QueryString("Aoff") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = Request.QueryString("Aoff").ToString
            End If

            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If

            If ChkWholeGroup.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("GROUPDATA").InnerText = "1"
            Else
                objInputXml.DocumentElement.SelectSingleNode("GROUPDATA").InnerText = "0"
            End If
            If ChkOrignalBook.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("USEORIGINAL").InnerText = "Y"
            Else
                objInputXml.DocumentElement.SelectSingleNode("USEORIGINAL").InnerText = "N"
            End If
            If Request.QueryString("LimAoff") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = Request.QueryString("LimAoff").ToString
            End If
            If Request.QueryString("LimReg") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = Request.QueryString("LimReg").ToString
            End If
            If Request.QueryString("LimOwnOff") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = Request.QueryString("LimOwnOff").ToString
            End If


            'Start CODE for sorting and paging

            If ViewState("PrevSearching") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
            Else
                Dim objTempInputXml As New XmlDocument
                Dim objNodeList As XmlNodeList

                objTempInputXml.LoadXml(ViewState("PrevSearching"))
                objNodeList = objTempInputXml.DocumentElement.ChildNodes
                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                For Each objNode As XmlNode In objNodeList
                    If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                        If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                            objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                            ddlPageNumber.SelectedValue = "1"
                        End If
                    End If
                Next
            End If


            objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "LOCATION_CODE"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "LOCATION_CODE"
            Else
                If ViewState("PrevSearching") Is Nothing Then
                Else
                    Dim objTempInputXml As New XmlDocument
                    Dim objNodeList As XmlNodeList
                    objTempInputXml.LoadXml(ViewState("PrevSearching"))
                    objNodeList = objTempInputXml.DocumentElement.ChildNodes
                    For Each objNode As XmlNode In objNodeList
                        If objNode.Name = "AIR" Or objNode.Name = "CAR" Or objNode.Name = "HOTEL" Or objNode.Name = "INSURANCE" Then
                            If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                                If ViewState("SortName") = "AIR" Or ViewState("SortName") = "CAR" Or ViewState("SortName") = "HOTEL" Or ViewState("SortName") = "INSURANCE" Then
                                    ViewState("SortName") = "LOCATION_CODE"
                                    ViewState("Desc") = "FALSE"
                                End If
                            End If
                        End If
                    Next
                End If


                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")

              

            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                

                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            'End Code for paging and sorting

            objOutputXml = objbzbzBIDT.BIDTProductivityDetail(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDataset = New DataSet
                FooterDataset = ds

                grdvBidtDetails.DataSource = ds.Tables("BIDTDETAILS")
                grdvBidtDetails.DataBind()
                'txtRecordCount.Text = ds.Tables("BIDTDETAILS").Rows.Count.ToString
                ' ##################################################################
                '@ Code Added For Paging And Sorting 
                ' ###################################################################
                BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)

                txtRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' @ Added Code To Show Image'                   
                ' grdNewFormat.SortExpression = ViewState("SortName")
                SetImageForSorting(grdvBidtDetails)
                ' @ Added Code To Show Image'
                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' ###################################################################
                '@ End of Code Added For Paging And Sorting 
                ' ###################################################################

                pnlPaging.Visible = True

            Else
                grdvBidtDetails.DataSource = Nothing
                grdvBidtDetails.DataBind()
                txtRecordCount.Text = "0"
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
            End If


        Catch ex As Exception
            lblError.Text = ex.Message.ToString
            pnlPaging.Visible = False
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing

        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BIDTDetailsSearch()
    End Sub
    Private Sub BindAllControl()
        Try
            Dim i, j As Integer
            drpMonthTo.SelectedValue = "12"
            drpMonthFrom.SelectedValue = "1"
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

  
    Protected Sub grdvBidtDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvBidtDetails.RowDataBound
        Try
            Dim objSecurityXml As New XmlDocument

            If ChkABooking.Items(0).Selected = True Then
                e.Row.Cells(6).Visible = True
            Else
                e.Row.Cells(6).Visible = False
            End If
            If ChkABooking.Items(1).Selected = True Then
                e.Row.Cells(7).Visible = True
            Else
                e.Row.Cells(7).Visible = False
            End If
            If ChkABooking.Items(2).Selected = True Then
                e.Row.Cells(8).Visible = True
            Else
                e.Row.Cells(8).Visible = False
            End If
            If ChkABooking.Items(3).Selected = True Then
                e.Row.Cells(9).Visible = True
            Else
                e.Row.Cells(9).Visible = False
            End If

            If ChkOrignalBook.Checked = True Then
                e.Row.Cells(11).Visible = True
                e.Row.Cells(12).Visible = True
            Else
                e.Row.Cells(11).Visible = False
                e.Row.Cells(12).Visible = False
            End If
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If

            Dim gdata, Aoff, useorignal, air, car, hotel, insurance, ResStaffId, LimAoff, LimReg, LimOwnOff As String
            gdata = ""
            Aoff = ""
            useorignal = ""
            air = ""
            car = ""
            hotel = ""
            insurance = ""
            ResStaffId = ""
            LimAoff = ""
            LimReg = ""
            LimOwnOff = ""

            If ChkWholeGroup.Checked = True Then
                gdata = "1"
            Else
                gdata = "0"
            End If

            If ChkOrignalBook.Checked = True Then
                useorignal = "Y"
            Else
                useorignal = "N"
            End If

            If ChkABooking.Items(0).Selected = True Then
                air = "1"
            Else
                air = "0"
            End If
            If ChkABooking.Items(1).Selected = True Then
                car = "1"
            Else
                car = "0"

            End If
            If ChkABooking.Items(2).Selected = True Then
                hotel = "1"
            Else
                hotel = "0"
            End If
            If ChkABooking.Items(3).Selected = True Then
                insurance = "1"
            Else
                insurance = "0"
            End If
            If Not Session("LoginSession") Is Nothing Then
                ResStaffId = Session("LoginSession").ToString().Split("|")(0)
            End If
            ResStaffId = ResStaffId.Replace("'", "")
            If Request.QueryString("LimAoff") IsNot Nothing Then
                LimAoff = Request.QueryString("LimAoff").ToString
            Else
                LimAoff = ""
            End If
            If Request.QueryString("LimReg") IsNot Nothing Then
                LimReg = Request.QueryString("LimReg").ToString
            Else
                LimReg = ""
            End If
            If Request.QueryString("LimOwnOff") IsNot Nothing Then
                LimOwnOff = Request.QueryString("LimOwnOff").ToString
            Else
                LimOwnOff = ""
            End If

            If Request.QueryString("Aoff") IsNot Nothing Then
                Aoff = Request.QueryString("Aoff").ToString
            Else
                Aoff = ""
            End If
            Dim linkABreakUp As System.Web.UI.HtmlControls.HtmlAnchor

            Dim hdCountry, hdAdd As HiddenField
            hdCountry = CType(e.Row.FindControl("hdCountry"), HiddenField)
            hdAdd = CType(e.Row.FindControl("hdAdd"), HiddenField)
            hdAdd.Value = hdAdd.Value.Replace(vbCrLf, "\n")
            hdAdd.Value = hdAdd.Value.Replace("'", "")
            hdAdd.Value = Server.UrlEncode(hdAdd.Value)
            'FMonth,TMonth,FYear,
            'TYear, Lcode, Aoff,
            'GrData, UseOrig, ResId, air, car,
            'hotel, insurance, LimAoff, LimReg,
            'LimOwnOff, Agency, Add, City, Country

            linkABreakUp = CType(e.Row.FindControl("linkABreakUp"), System.Web.UI.HtmlControls.HtmlAnchor)

            linkABreakUp.Attributes.Add("OnClick", "javascript:return SelectBreakFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(0).Text.Trim) + "','" + Aoff + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(txtAgencyName.Text) + "','" + hdAdd.Value + "','" + e.Row.Cells(2).Text.ToString + "','" + hdCountry.Value + "');")
            Dim hdTot As HiddenField
            hdTot = CType(e.Row.FindControl("hdTot"), HiddenField)
            '  txtTotlaProductivity.Text = Val(txtTotlaProductivity.Text) + Val(hdTot.Value)

            If FooterDataset IsNot Nothing Then
                If FooterDataset.Tables("PAGE_TOTAL") IsNot Nothing Then
                    txtTotlaProductivity.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("TOTAL").ToString
                End If
            End If

            

        Catch ex As Exception

        End Try
       
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click

        grdvBidtDetails.AllowSorting = False
        grdvBidtDetails.HeaderStyle.ForeColor = Drawing.Color.Black
        BIDTDetailsExport()

        'BIDTDetailsSearch2()
        'If grdvBidtDetails.Rows.Count > 0 Then
        '    'PrepareGridViewForExport(grdvBidtDetails)
        '    grdvBidtDetails.Columns(11).Visible = False
        '    ExportGridView(grdvBidtDetails, "BIDTDetails.xls")
        'End If

    End Sub

    Private Sub ExportGridView(ByVal gv2 As GridView, ByVal FileName As String)

        Dim attachment As String = "attachment; filename=" + FileName
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/ms-excel"
        Dim sw As New StringWriter
        Dim pp As String
        Dim htw As New HtmlTextWriter(sw)
        Dim frm As New HtmlForm()
        gv2.Parent.Controls.Add(frm)
        frm.Attributes("runat") = "server"
        frm.Controls.Add(gv2)
        frm.RenderControl(htw)
        pp = sw.ToString()
        pp = pp.ToString().Replace("Action", "")
        pp = pp.ToString().Replace("Details", "")
        pp = pp.ToString().Replace("CRS", "")
        pp = pp.ToString().Replace("CRS Details", "")
        pp = pp.ToString().Replace("1 A BreakUp", "")

        Response.Write(pp.ToString())
        Response.End()

    End Sub

    'Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    'End Sub
    Private Sub PrepareGridViewForExport(ByVal gv As Control)
        'LinkButton lb = new LinkButton();
        Dim l As New Literal
        Dim name As String = ""
        Dim lb As New LinkButton

        Dim i As Int32
        For i = 0 To gv.Controls.Count - 1
            If (gv.Controls(i).GetType Is GetType(LinkButton)) Then
                l.Text = CType(gv.Controls(i), LinkButton).Text
                gv.Controls.Remove(gv.Controls(i))
                gv.Controls.AddAt(i, l)

            End If

            If (gv.Controls(i).HasControls()) Then
                PrepareGridViewForExport(gv.Controls(i))
            End If

        Next
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
            'pnlPaging.Visible = False
            ' ddlPageNumber.Visible = False
            lnkNext.Visible = False
            lnkPrev.Visible = False
        Else
            'ddlPageNumber.Visible = True
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


    Private Sub BIDTDetailsSearch2()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzbzBIDT As New AAMS.bizProductivity.bzBIDT
        Try
            ' txtTotlaProductivity.Text = "0"
            'objInputXml.LoadXml("<PR_SEARCH_PR_1A_PRODUCTIVITY_DETAILS_BIDT_INPUT><FMONTH/><FYEAR/><TMONTH/><TYEAR/><LCODE/><AOFFICE/><GROUPDATA/><USEORIGINAL/><RESPONSIBLESTAFFID/><AIR/><HOTEL/><CAR/><INSURANCE/><LIMITED_TO_AOFFICE/><LIMITED_TO_REGION/><LIMITED_TO_OWNAAGENCY/></PR_SEARCH_PR_1A_PRODUCTIVITY_DETAILS_BIDT_INPUT>")
            objInputXml.LoadXml("<PR_SEARCH_PR_1A_PRODUCTIVITY_DETAILS_BIDT_INPUT><FMONTH/><FYEAR/><TMONTH/><TYEAR/><LCODE/><AOFFICE/><GROUPDATA/><USEORIGINAL/><RESPONSIBLESTAFFID/><AIR/><HOTEL/><CAR/><INSURANCE/><LIMITED_TO_AOFFICE/><LIMITED_TO_REGION/><LIMITED_TO_OWNAAGENCY/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></PR_SEARCH_PR_1A_PRODUCTIVITY_DETAILS_BIDT_INPUT>")
            '<PR_SEARCH_PR_1A_PRODUCTIVITY_DETAILS_BIDT_INPUT>
            '<FMONTH/><FYEAR/><TMONTH/><TYEAR/><LCODE/><AOFFICE/>
            '<GROUPDATA/><USEORIGINAL/><RESPONSIBLESTAFFID/>
            '<AIR/><HOTEL/><CAR/><INSURANCE/><LIMITED_TO_AOFFICE/>
            '<LIMITED_TO_REGION/><LIMITED_TO_OWNAAGENCY/>
            ' <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
            '</PR_SEARCH_PR_1A_PRODUCTIVITY_DETAILS_BIDT_INPUT>
            objInputXml.DocumentElement.SelectSingleNode("FMONTH").InnerText = drpMonthFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("TMONTH").InnerText = drpMonthTo.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("FYEAR").InnerText = drpYearFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("TYEAR").InnerText = drpYearTo.SelectedValue



            If Request.QueryString("Lcode") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = objED.Decrypt(Request.QueryString("Lcode").ToString().Trim)
            End If
            If ChkABooking.Items(0).Selected = True Then
                objInputXml.DocumentElement.SelectSingleNode("AIR").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("AIR").InnerText = 0
            End If
            If ChkABooking.Items(1).Selected = True Then
                objInputXml.DocumentElement.SelectSingleNode("CAR").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("CAR").InnerText = 0

            End If
            If ChkABooking.Items(2).Selected = True Then
                objInputXml.DocumentElement.SelectSingleNode("HOTEL").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("HOTEL").InnerText = 0
            End If
            If ChkABooking.Items(3).Selected = True Then
                objInputXml.DocumentElement.SelectSingleNode("INSURANCE").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("INSURANCE").InnerText = 0
            End If
            If Request.QueryString("Aoff") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = Request.QueryString("Aoff").ToString
            End If

            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If

            If ChkWholeGroup.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("GROUPDATA").InnerText = "1"
            Else
                objInputXml.DocumentElement.SelectSingleNode("GROUPDATA").InnerText = "0"
            End If
            If ChkOrignalBook.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("USEORIGINAL").InnerText = "Y"
            Else
                objInputXml.DocumentElement.SelectSingleNode("USEORIGINAL").InnerText = "N"
            End If
            If Request.QueryString("LimAoff") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = Request.QueryString("LimAoff").ToString
            End If
            If Request.QueryString("LimReg") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = Request.QueryString("LimReg").ToString
            End If
            If Request.QueryString("LimOwnOff") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = Request.QueryString("LimOwnOff").ToString
            End If


            'Start CODE for sorting and paging

           


            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "LOCATION_CODE"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "LOCATION_CODE"
            Else
                If ViewState("PrevSearching") Is Nothing Then
                Else
                    Dim objTempInputXml As New XmlDocument
                    Dim objNodeList As XmlNodeList
                    objTempInputXml.LoadXml(ViewState("PrevSearching"))
                    objNodeList = objTempInputXml.DocumentElement.ChildNodes
                    For Each objNode As XmlNode In objNodeList
                        If objNode.Name = "AIR" Or objNode.Name = "CAR" Or objNode.Name = "HOTEL" Or objNode.Name = "INSURANCE" Then
                            If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                                If ViewState("SortName") = "AIR" Or ViewState("SortName") = "CAR" Or ViewState("SortName") = "HOTEL" Or ViewState("SortName") = "INSURANCE" Then
                                    ViewState("SortName") = "LOCATION_CODE"
                                    ViewState("Desc") = "FALSE"
                                End If
                            End If
                        End If
                    Next
                End If

                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")

              
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
               

                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            'End Code for paging and sorting
            grdvBidtDetails.AllowSorting = False
            grdvBidtDetails.HeaderStyle.ForeColor = Drawing.Color.Black

            objOutputXml = objbzbzBIDT.BIDTProductivityDetail(objInputXml)

            'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '    ViewState("PrevSearching") = objInputXml.OuterXml
            'End If

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDataset = New DataSet
                FooterDataset = ds
                grdvBidtDetails.DataSource = ds.Tables("BIDTDETAILS")
                grdvBidtDetails.DataBind()

            Else
                grdvBidtDetails.DataSource = Nothing
                grdvBidtDetails.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                'pnlPaging.Visible =False 
            End If


        Catch ex As Exception
            lblError.Text = ex.Message.ToString
            'pnlPaging.Visible =False 
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing

        End Try
    End Sub
#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            BIDTDetailsSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            BIDTDetailsSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            BIDTDetailsSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvBidtDetails_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvBidtDetails.Sorted
        Try

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub grdvBidtDetails_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvBidtDetails.Sorting
        Try
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                '  ViewState("Desc") = "FALSE"
                '@ Added Code For Default descending sorting order on first time  of following Fields      
                ' @AIR, CAR, HOTEL, INSURANCE, TOTAL, PASSIVE, WITHPASSIVE,
                If SortName.Trim().ToUpper = "AIR" Or SortName.Trim().ToUpper = "CAR" Or SortName.Trim().ToUpper = "HOTEL" Or SortName.Trim().ToUpper = "INSURANCE" Or SortName.Trim().ToUpper = "TOTAL" Or SortName.Trim().ToUpper = "PASSIVE" Or SortName.Trim().ToUpper = "WITHPASSIVE" Then
                    ViewState("Desc") = "TRUE"
                Else
                    ViewState("Desc") = "FALSE"
                End If
                '@ End of Added Code For Default descending sorting order on first time  of following Fields

            Else
                If ViewState("SortName") = SortName Then
                    If ViewState("Desc") = "TRUE" Then
                        ViewState("Desc") = "FALSE"
                    Else
                        ViewState("Desc") = "TRUE"
                    End If
                Else
                    ViewState("SortName") = SortName
                    ' ViewState("Desc") = "FALSE"
                    '@ Added Code For Default descending sorting order on first time  of following Fields      
                    ' @AIR, CAR, HOTEL, INSURANCE, TOTAL, PASSIVE, WITHPASSIVE,
                    If SortName.Trim().ToUpper = "AIR" Or SortName.Trim().ToUpper = "CAR" Or SortName.Trim().ToUpper = "HOTEL" Or SortName.Trim().ToUpper = "INSURANCE" Or SortName.Trim().ToUpper = "TOTAL" Or SortName.Trim().ToUpper = "PASSIVE" Or SortName.Trim().ToUpper = "WITHPASSIVE" Then
                        ViewState("Desc") = "TRUE"
                    Else
                        ViewState("Desc") = "FALSE"
                    End If
                    '@ End of Added Code For Default descending sorting order on first time  of following Fields

                End If
            End If
            BIDTDetailsSearch()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub grdvBidtDetails_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvBidtDetails.RowCreated
        Try
            'If e.Row.RowType = DataControlRowType.Header Then
            '    Dim priceField As BoundField = grdvBidtDetails.Columns(1)
            '    priceField.HeaderText = "abc"
            '    priceField.SortExpression = "ADDRESS"
            '    ' priceField.SortExpression =
            '    priceField.HtmlEncode = True
            'End If
        Catch ex As Exception
        End Try
    End Sub
#End Region

    Protected Sub ChkWholeGroup_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkWholeGroup.CheckedChanged
        Try
            BIDTDetailsSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ChkOrignalBook_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkOrignalBook.CheckedChanged
        Try
            BIDTDetailsSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Private Sub BIDTDetailsExport()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzbzBIDT As New AAMS.bizProductivity.bzBIDT
        Try
            ' txtTotlaProductivity.Text = "0"
            'objInputXml.LoadXml("<PR_SEARCH_PR_1A_PRODUCTIVITY_DETAILS_BIDT_INPUT><FMONTH/><FYEAR/><TMONTH/><TYEAR/><LCODE/><AOFFICE/><GROUPDATA/><USEORIGINAL/><RESPONSIBLESTAFFID/><AIR/><HOTEL/><CAR/><INSURANCE/><LIMITED_TO_AOFFICE/><LIMITED_TO_REGION/><LIMITED_TO_OWNAAGENCY/></PR_SEARCH_PR_1A_PRODUCTIVITY_DETAILS_BIDT_INPUT>")
            objInputXml.LoadXml("<PR_SEARCH_PR_1A_PRODUCTIVITY_DETAILS_BIDT_INPUT><FMONTH/><FYEAR/><TMONTH/><TYEAR/><LCODE/><AOFFICE/><GROUPDATA/><USEORIGINAL/><RESPONSIBLESTAFFID/><AIR/><HOTEL/><CAR/><INSURANCE/><LIMITED_TO_AOFFICE/><LIMITED_TO_REGION/><LIMITED_TO_OWNAAGENCY/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></PR_SEARCH_PR_1A_PRODUCTIVITY_DETAILS_BIDT_INPUT>")
            '<PR_SEARCH_PR_1A_PRODUCTIVITY_DETAILS_BIDT_INPUT>
            '<FMONTH/><FYEAR/><TMONTH/><TYEAR/><LCODE/><AOFFICE/>
            '<GROUPDATA/><USEORIGINAL/><RESPONSIBLESTAFFID/>
            '<AIR/><HOTEL/><CAR/><INSURANCE/><LIMITED_TO_AOFFICE/>
            '<LIMITED_TO_REGION/><LIMITED_TO_OWNAAGENCY/>
            ' <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
            '</PR_SEARCH_PR_1A_PRODUCTIVITY_DETAILS_BIDT_INPUT>
            objInputXml.DocumentElement.SelectSingleNode("FMONTH").InnerText = drpMonthFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("TMONTH").InnerText = drpMonthTo.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("FYEAR").InnerText = drpYearFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("TYEAR").InnerText = drpYearTo.SelectedValue



            If Request.QueryString("Lcode") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = objED.Decrypt(Request.QueryString("Lcode").ToString.Trim)
            End If
            If ChkABooking.Items(0).Selected = True Then
                objInputXml.DocumentElement.SelectSingleNode("AIR").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("AIR").InnerText = 0
            End If
            If ChkABooking.Items(1).Selected = True Then
                objInputXml.DocumentElement.SelectSingleNode("CAR").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("CAR").InnerText = 0

            End If
            If ChkABooking.Items(2).Selected = True Then
                objInputXml.DocumentElement.SelectSingleNode("HOTEL").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("HOTEL").InnerText = 0
            End If
            If ChkABooking.Items(3).Selected = True Then
                objInputXml.DocumentElement.SelectSingleNode("INSURANCE").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("INSURANCE").InnerText = 0
            End If
            If Request.QueryString("Aoff") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = Request.QueryString("Aoff").ToString
            End If

            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If

            If ChkWholeGroup.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("GROUPDATA").InnerText = "1"
            Else
                objInputXml.DocumentElement.SelectSingleNode("GROUPDATA").InnerText = "0"
            End If
            If ChkOrignalBook.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("USEORIGINAL").InnerText = "Y"
            Else
                objInputXml.DocumentElement.SelectSingleNode("USEORIGINAL").InnerText = "N"
            End If
            If Request.QueryString("LimAoff") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = Request.QueryString("LimAoff").ToString
            End If
            If Request.QueryString("LimReg") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = Request.QueryString("LimReg").ToString
            End If
            If Request.QueryString("LimOwnOff") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = Request.QueryString("LimOwnOff").ToString
            End If


            'Start CODE for sorting and paging
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "LOCATION_CODE"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "LOCATION_CODE"
            Else
                If ViewState("PrevSearching") Is Nothing Then
                Else
                    Dim objTempInputXml As New XmlDocument
                    Dim objNodeList As XmlNodeList
                    objTempInputXml.LoadXml(ViewState("PrevSearching"))
                    objNodeList = objTempInputXml.DocumentElement.ChildNodes
                    For Each objNode As XmlNode In objNodeList
                        If objNode.Name = "AIR" Or objNode.Name = "CAR" Or objNode.Name = "HOTEL" Or objNode.Name = "INSURANCE" Then
                            If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                                If ViewState("SortName") = "AIR" Or ViewState("SortName") = "CAR" Or ViewState("SortName") = "HOTEL" Or ViewState("SortName") = "INSURANCE" Then
                                    ViewState("SortName") = "LOCATION_CODE"
                                    ViewState("Desc") = "FALSE"
                                End If
                            End If
                        End If
                    Next
                End If

                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")


            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else


                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            'End Code for paging and sorting
            grdvBidtDetails.AllowSorting = False
            grdvBidtDetails.HeaderStyle.ForeColor = Drawing.Color.Black

            objOutputXml = objbzbzBIDT.BIDTProductivityDetail(objInputXml)

            'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '    ViewState("PrevSearching") = objInputXml.OuterXml
            'End If

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDataset = New DataSet
                FooterDataset = ds
                grdvBidtDetails.DataSource = ds.Tables("BIDTDETAILS")
                grdvBidtDetails.DataBind()

                '@ Code For Exporting the Data

                Dim objOutputXmlExport As New XmlDocument
                Dim objXmlNode, objXmlNodeClone As XmlNode
                objOutputXmlExport.LoadXml(ds.GetXml().ToString)

                objXmlNode = objOutputXmlExport.DocumentElement.SelectSingleNode("BIDTDETAILS")
                objXmlNodeClone = objXmlNode.CloneNode(True)


                'For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                '    XmlAttr.Value = ""
                'Next
                'With objXmlNodeClone
                '    '.Attributes(8).Value = "Total"
                '    '.Attributes("AIR_NETBOOKINGS").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("AIR_NETBOOKINGS").ToString
                '    '.Attributes("CAR_NETBOOKINGS").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("CAR_NETBOOKINGS").ToString
                '    '.Attributes("HOTEL_NETBOOKINGS").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("HOTEL_NETBOOKINGS").ToString
                '    '.Attributes("INSURANCE_NETBOOKINGS").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("INSURANCE_NETBOOKINGS").ToString
                '    '.Attributes("AVERAGE").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("AVERAGE").ToString
                'End With

                '  objOutputXmlExport.DocumentElement.AppendChild(objXmlNodeClone)

                Dim objExport As New ExportExcel
                Dim IntInvisible As Integer = 0
                For intclmn As Integer = 0 To grdvBidtDetails.HeaderRow.Cells.Count - 2
                    If grdvBidtDetails.HeaderRow.Cells(intclmn).Visible = False Then
                        IntInvisible = IntInvisible + 1
                    End If
                Next
                Dim strArray(grdvBidtDetails.HeaderRow.Cells.Count - 2 - IntInvisible) As String
                Dim intArray(grdvBidtDetails.HeaderRow.Cells.Count - 2 - IntInvisible) As Integer

                Dim intclmnVis As Integer = 0
                For intclmn As Integer = 0 To objXmlNodeClone.Attributes.Count - 1
                    If grdvBidtDetails.HeaderRow.Cells(intclmn).Visible = True Then
                        strArray(intclmnVis) = grdvBidtDetails.Columns(intclmn).HeaderText 'objOutputXml.DocumentElement.SelectSingleNode("BIDTMONTHLYBREAKUP").Attributes(intclmn - 1).Name
                        '@ Finding Position From xml Related with Header Text
                        For kk As Integer = 0 To objXmlNodeClone.Attributes.Count - 1
                            If objXmlNodeClone.Attributes(kk).Name.Trim = grdvBidtDetails.Columns(intclmn).SortExpression.Trim Then
                                intArray(intclmnVis) = kk
                                intclmnVis = intclmnVis + 1
                                Exit For
                            End If
                        Next kk
                    End If
                Next intclmn

                objExport.ExportDetails(objOutputXmlExport, "BIDTDETAILS", intArray, strArray, ExportExcel.ExportFormat.Excel, "BIDTDetails.xls")




            Else
                grdvBidtDetails.DataSource = Nothing
                grdvBidtDetails.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
                txtRecordCount.Text = "0"
                txtTotlaProductivity.Text = "0"
            End If


        Catch ex As Exception
            lblError.Text = ex.Message.ToString
            pnlPaging.Visible = False
            txtRecordCount.Text = "0"
            txtTotlaProductivity.Text = "0"
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing

        End Try
    End Sub

    Private Sub AgencyView()

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
        objInputXml.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")
        If Request.QueryString("Lcode") IsNot Nothing Then
            objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = objED.Decrypt(Request.QueryString("Lcode").ToString())
        End If
        objOutputXml = objbzAgency.View(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            With objOutputXml.DocumentElement.SelectSingleNode("Agency")
                txtAgencyName.Text = .Attributes("NAME").Value()
                Dim strAddress As String = .Attributes("ADDRESS").Value()
                strAddress &= " "
                strAddress &= .Attributes("ADDRESS1").Value()
                txtAdd.Text = strAddress '.Attributes("ADDRESS").Value()
                txtCity.Text = .Attributes("CITY").Value()
                txtCountry.Text = .Attributes("COUNTRY").Value()
            End With
        End If
    End Sub
End Class
