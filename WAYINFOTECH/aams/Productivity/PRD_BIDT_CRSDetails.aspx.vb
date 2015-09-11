Imports system.IO
Imports system.Text
Partial Class Productivity_PRD_BIDT_CRSDetails
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim FooterDataset As DataSet
    Dim objED As New EncyrptDeCyrpt
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
            btnPrint.Attributes.Add("onclick", "CallPrint('grdvBidtCrsDetails')")
            lblError.Text = ""
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            btnSearch.Attributes.Add("onclick", "return CheckValidation();")
            btnExport.Attributes.Add("onclick", "return CheckValidation();")
            btnGraph.Attributes.Add("onclick", "return CheckValidation();")

            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Attributes("Value").Value)
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

            'ChkAirBreak.Attributes.Add("onclick", "return EnableCarrierType2();")
            '  drpAirLine.Attributes.Add("onchange", "return EnableCarrierType3();")
            'drpAirLine.Attributes.Add("onchange", "return EnableCarrierType2();")
            drpAirLine.Attributes.Add("onkeyup", "return gotops('drpAirLine');")

            If Not IsPostBack Then

                txtRecordCount.Text = "0"
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

                'If Request.QueryString("Agency") IsNot Nothing Then
                '    txtAgencyName.Text = Request.QueryString("Agency").ToString
                '    txtAgencyName.Text = Server.HtmlDecode(txtAgencyName.Text)
                'End If
                'If Request.QueryString("Add") IsNot Nothing Then
                '    txtAdd.Text = Request.QueryString("Add").ToString
                '    txtAdd.Text = Server.HtmlDecode(txtAdd.Text)
                'End If
                'If Request.QueryString("City") IsNot Nothing Then
                '    txtCity.Text = Request.QueryString("City").ToString
                'End If
                'If Request.QueryString("Country") IsNot Nothing Then
                '    txtCountry.Text = Request.QueryString("Country").ToString
                'End If
                If Request.QueryString("GrData") IsNot Nothing Then
                    If Request.QueryString("GrData").ToString = "1" Then
                        ChkWholeGroup.Checked = True
                    Else
                        ChkWholeGroup.Checked = False
                    End If
                End If
                txtTotlaProductivity.Text = "0"
                If Request.QueryString("Popup2") Is Nothing Then
                    BIDTCrsDetailsSearch()
                End If
            End If

            

        Catch ex As Exception
        Finally

            'Dim str As String = Request("ChkAirBreak")
            'Dim str2 As String = Request("ChkAirBreak")
        End Try
    End Sub
    Private Sub BIDTCrsDetailsSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
        Try
            ' objInputXml.LoadXml(" <PR_SEARCHMIDTDETAILS_INPUT><MONTHFROM /><YEARFROM /><MONTHTO /><YEARTO /> <LCODE /> <WHOLEGROUP /><SHOWAIRBREAKUP />  <AOFFICE /> <REGION />  <AIRLINECODE />  <RESPONSIBLESTAFFID />  <SALESPERSONID />  <CARRIERTYPE />  <LIMITED_TO_REGION />   <LIMITED_TO_OWNAAGENCY />   <LIMITED_TO_AOFFICE />   </PR_SEARCHMIDTDETAILS_INPUT>")
            objInputXml.LoadXml(" <PR_SEARCHMIDTDETAILS_INPUT><MONTHFROM /><YEARFROM /><MONTHTO /><YEARTO /> <LCODE /> <WHOLEGROUP /><SHOWAIRBREAKUP />  <AOFFICE /> <REGION />  <AIRLINECODE />  <RESPONSIBLESTAFFID />  <SALESPERSONID />  <CARRIERTYPE />  <LIMITED_TO_REGION />   <LIMITED_TO_OWNAAGENCY />   <LIMITED_TO_AOFFICE />  <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/> </PR_SEARCHMIDTDETAILS_INPUT>")
            '         <PR_SEARCHMIDTDETAILS_INPUT>
            '<MONTHFROM /> 
            '<YEARFROM /> 
            '<MONTHTO /> 
            '<YEARTO /> 
            '<LCODE /> 
            '<WHOLEGROUP /> 
            '<SHOWAIRBREAKUP /> 
            '<AOFFICE /> 
            '<REGION /> 
            '<AIRLINECODE /> 
            '<RESPONSIBLESTAFFID /> 
            '<SALESPERSONID /> 
            '<CARRIERTYPE /> 
            '<LIMITED_TO_REGION /> 
            '<LIMITED_TO_OWNAAGENCY /> 
            '<LIMITED_TO_AOFFICE /> 
            '</PR_SEARCHMIDTDETAILS_INPUT>
            ' <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
            objInputXml.DocumentElement.SelectSingleNode("MONTHFROM").InnerText = drpMonthFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("MONTHTO").InnerText = drpMonthTo.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("YEARFROM").InnerText = drpYearFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("YEARTO").InnerText = drpYearTo.SelectedValue

            txtTotlaProductivity.Text = "0"

            If Request.QueryString("Lcode") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = objED.Decrypt(Request.QueryString("Lcode").ToString.Trim)
            End If

            If Request.QueryString("Aoff") IsNot Nothing Then
                'objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = objED.Decrypt(Request.QueryString("Aoff").ToString.Trim)
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = (Request.QueryString("Aoff").ToString.Trim)
            End If
            If drpAirLine.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("AIRLINECODE").InnerText = drpAirLine.SelectedValue
            End If


            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If
            If ChkWholeGroup.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "Y"
            Else
                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "N"
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
            If ChkAirBreak.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("SHOWAIRBREAKUP").InnerText = "1"
                objInputXml.DocumentElement.SelectSingleNode("CARRIERTYPE").InnerText = RdCarryType.SelectedValue
            Else
                objInputXml.DocumentElement.SelectSingleNode("SHOWAIRBREAKUP").InnerText = "0"
            End If

            'If drpAirLine.SelectedIndex <> 0 Then
            '    objInputXml.DocumentElement.SelectSingleNode("SHOWAIRBREAKUP").InnerText = "1"
            '    objInputXml.DocumentElement.SelectSingleNode("CARRIERTYPE").InnerText = RdCarryType.SelectedValue
            'End If

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
                ViewState("SortName") = "MONTHYEAR"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "MONTHYEAR" '"LOCATION_CODE"
            Else
                If ViewState("PrevSearching") Is Nothing Then
                Else
                    Dim objTempInputXml As New XmlDocument
                    Dim objNodeList As XmlNodeList
                    objTempInputXml.LoadXml(ViewState("PrevSearching"))
                    objNodeList = objTempInputXml.DocumentElement.ChildNodes
                    For Each objNode As XmlNode In objNodeList
                        If objNode.Name = "WHOLEGROUP" Then
                            If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                                If ChkWholeGroup.Checked = False Then
                                    If ViewState("SortName") = "ADDRESS" Or ViewState("SortName") = "CITY" Or ViewState("SortName") = "COUNTRY" Then

                                        ViewState("SortName") = "MONTHYEAR"
                                        ViewState("Desc") = "FALSE"
                                    End If
                                End If
                            End If
                        End If
                        If objNode.Name = "SHOWAIRBREAKUP" Then
                            If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                                If ViewState("SortName") = "AIRLINENAME" Then

                                    ViewState("SortName") = "MONTHYEAR"
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


            objOutputXml = objbzMIDT.SearchDetails(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDataset = New DataSet
                FooterDataset = ds
                grdvBidtCrsDetails.DataSource = ds.Tables("MIDTDETAILS")
                grdvBidtCrsDetails.DataBind()
                ' txtRecordCount.Text = ds.Tables("MIDTDETAILS").Rows.Count.ToString



                ' ##################################################################
                '@ Code Added For Paging And Sorting In case Of 
                ' ###################################################################
                BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                txtRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                SetImageForSorting(grdvBidtCrsDetails)
                ' ###################################################################
                '@ End of Code Added For Paging And Sorting In case 
                ' ###################################################################

                pnlPaging.Visible = True
            Else
                grdvBidtCrsDetails.DataSource = Nothing
                grdvBidtCrsDetails.DataBind()
                txtRecordCount.Text = "0"
                txtTotlaProductivity.Text = "0"
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
        txtRecordCount.Text = "0"
        BIDTCrsDetailsSearch()
    End Sub
    Private Sub BindAllControl()
        Try
            objeAAMS.BindDropDown(drpAirLine, "AIRLINE", True, 1)
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

            'Code for Filling Address
            AgencyView()
          
            'End of Code on 17th Feb

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub grdvBidtCrsDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvBidtCrsDetails.RowDataBound
        Try
            Dim objSecurityXml As New XmlDocument

            If ChkWholeGroup.Checked = True Then
                e.Row.Cells(0).Visible = True
                e.Row.Cells(1).Visible = True
                e.Row.Cells(2).Visible = True
            Else
                e.Row.Cells(0).Visible = False
                e.Row.Cells(1).Visible = False
                e.Row.Cells(2).Visible = False
            End If
            If ChkAirBreak.Checked = False Then
                e.Row.Cells(4).Visible = False
            Else
                e.Row.Cells(4).Visible = True
            End If

            'If drpAirLine.SelectedIndex <> 0 Then
            '    e.Row.Cells(4).Visible = True
            'Else
            '    e.Row.Cells(4).Visible = False
            'End If
            If e.Row.RowIndex < 0 Then

                Exit Sub
            End If
            Dim hdTot As HiddenField
            hdTot = CType(e.Row.FindControl("hdTot"), HiddenField)
            'txtTotlaProductivity.Text = Val(txtTotlaProductivity.Text) + Val(hdTot.Value)
            If FooterDataset IsNot Nothing Then
                If FooterDataset.Tables("PAGE_TOTAL") IsNot Nothing Then
                    txtTotlaProductivity.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("TOTAL").ToString
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            txtRecordCount.Text = "0"
            grdvBidtCrsDetails.AllowSorting = False
            grdvBidtCrsDetails.HeaderStyle.ForeColor = Drawing.Color.Black
            BIDTCrsDetailsExport()
            'BIDTCrsDetailsSearch2()

            'If grdvBidtCrsDetails.Rows.Count > 0 Then
            '    'PrepareGridViewForExport(grdvBidtCrsDetails)
            '    grdvBidtCrsDetails.Columns(grdvBidtCrsDetails.Columns.Count - 1).Visible = False
            '    ' grdvBidtCrsDetails.Columns(10).Visible = False
            '    ExportGridView(grdvBidtCrsDetails)
            'End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Private Sub ExportGridView(ByVal gv2 As GridView)
        Dim attachment As String = "attachment; filename=CRSDetails.xls"
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/ms-excel"
        Dim sw As New StringWriter
        Dim htw As New HtmlTextWriter(sw)
        Dim frm As New HtmlForm()
        gv2.Parent.Controls.Add(frm)
        frm.Attributes("runat") = "server"
        frm.Controls.Add(gv2)
        frm.RenderControl(htw)
        Response.Write(sw.ToString())
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

    Protected Sub btnGraph_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGraph.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
        Try
            ' objInputXml.LoadXml(" <PR_SEARCHMIDTDETAILS_INPUT><MONTHFROM /><YEARFROM /><MONTHTO /><YEARTO /> <LCODE /> <WHOLEGROUP /><SHOWAIRBREAKUP />  <AOFFICE /> <REGION />  <AIRLINECODE />  <RESPONSIBLESTAFFID />  <SALESPERSONID />  <CARRIERTYPE />  <LIMITED_TO_REGION />   <LIMITED_TO_OWNAAGENCY />   <LIMITED_TO_AOFFICE />   </PR_SEARCHMIDTDETAILS_INPUT>")
            objInputXml.LoadXml(" <PR_SEARCHMIDTDETAILS_INPUT><MONTHFROM /><YEARFROM /><MONTHTO /><YEARTO /> <LCODE /> <WHOLEGROUP /><SHOWAIRBREAKUP />  <AOFFICE /> <REGION />  <AIRLINECODE />  <RESPONSIBLESTAFFID />  <SALESPERSONID />  <CARRIERTYPE />  <LIMITED_TO_REGION />   <LIMITED_TO_OWNAAGENCY />   <LIMITED_TO_AOFFICE />  <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/> </PR_SEARCHMIDTDETAILS_INPUT>")
            '         <PR_SEARCHMIDTDETAILS_INPUT>
            '<MONTHFROM /> 
            '<YEARFROM /> 
            '<MONTHTO /> 
            '<YEARTO /> 
            '<LCODE /> 
            '<WHOLEGROUP /> 
            '<SHOWAIRBREAKUP /> 
            '<AOFFICE /> 
            '<REGION /> 
            '<AIRLINECODE /> 
            '<RESPONSIBLESTAFFID /> 
            '<SALESPERSONID /> 
            '<CARRIERTYPE /> 
            '<LIMITED_TO_REGION /> 
            '<LIMITED_TO_OWNAAGENCY /> 
            '<LIMITED_TO_AOFFICE /> 
            '</PR_SEARCHMIDTDETAILS_INPUT>
            objInputXml.DocumentElement.SelectSingleNode("MONTHFROM").InnerText = drpMonthFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("MONTHTO").InnerText = drpMonthTo.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("YEARFROM").InnerText = drpYearFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("YEARTO").InnerText = drpYearTo.SelectedValue

            txtTotlaProductivity.Text = "0"

            If Request.QueryString("Lcode") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = objED.Decrypt(Request.QueryString("Lcode").ToString.Trim)
            End If

            If Request.QueryString("Aoff") IsNot Nothing Then
                '   objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = objED.Decrypt(Request.QueryString("Aoff").ToString.Trim)
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = (Request.QueryString("Aoff").ToString.Trim)
            End If
            If drpAirLine.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("AIRLINECODE").InnerText = drpAirLine.SelectedValue
            End If


            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If
            If ChkWholeGroup.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "Y"
            Else
                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "N"
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
            If ChkAirBreak.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("SHOWAIRBREAKUP").InnerText = "1"
                objInputXml.DocumentElement.SelectSingleNode("CARRIERTYPE").InnerText = RdCarryType.SelectedValue
            Else
                objInputXml.DocumentElement.SelectSingleNode("SHOWAIRBREAKUP").InnerText = "0"
            End If

            'Start Code for paging and sorting

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "ADDRESS"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "ADDRESS" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            'End Code for paging and sorting

            objOutputXml = objbzMIDT.SearchDetails(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Session("CRSDETAILS") = objOutputXml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=CRSDETAILS")
            Else
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
        '@ End of Code Added For Paging 
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




    ' ###################################################################
#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            BIDTCrsDetailsSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            BIDTCrsDetailsSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            BIDTCrsDetailsSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvBidtCrsDetails_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvBidtCrsDetails.Sorted
        Try

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub grdvBidtCrsDetails_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvBidtCrsDetails.Sorting
        Try
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ' ViewState("Desc") = "FALSE"
                '@ Added Code For Default descending sorting order on first time  of following Fields      
                ' @ A, B, P, G, W, TOTAL,
                If SortName.Trim().ToUpper = "A" Or SortName.Trim().ToUpper = "B" Or SortName.Trim().ToUpper = "P" Or SortName.Trim().ToUpper = "G" Or SortName.Trim().ToUpper = "W" Or SortName.Trim().ToUpper = "TOTAL" Then
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
                    ' @ A, B, P, G, W, TOTAL,
                    If SortName.Trim().ToUpper = "A" Or SortName.Trim().ToUpper = "B" Or SortName.Trim().ToUpper = "P" Or SortName.Trim().ToUpper = "G" Or SortName.Trim().ToUpper = "W" Or SortName.Trim().ToUpper = "TOTAL" Then
                        ViewState("Desc") = "TRUE"
                    Else
                        ViewState("Desc") = "FALSE"
                    End If
                    '@ End of Added Code For Default descending sorting order on first time  of following Fields


                End If
            End If
            BIDTCrsDetailsSearch()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub grdvBidtCrsDetails_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvBidtCrsDetails.RowCreated
        Try

        Catch ex As Exception
        End Try
    End Sub



#End Region
    ' ###################################################################

    Protected Sub ChkWholeGroup_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkWholeGroup.CheckedChanged
        Try
            txtTotlaProductivity.Text = 0
            txtRecordCount.Text = 0
            pnlPaging.Visible = False
            BIDTCrsDetailsSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub BIDTCrsDetailsSearch2()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
        Try
            ' objInputXml.LoadXml(" <PR_SEARCHMIDTDETAILS_INPUT><MONTHFROM /><YEARFROM /><MONTHTO /><YEARTO /> <LCODE /> <WHOLEGROUP /><SHOWAIRBREAKUP />  <AOFFICE /> <REGION />  <AIRLINECODE />  <RESPONSIBLESTAFFID />  <SALESPERSONID />  <CARRIERTYPE />  <LIMITED_TO_REGION />   <LIMITED_TO_OWNAAGENCY />   <LIMITED_TO_AOFFICE />   </PR_SEARCHMIDTDETAILS_INPUT>")
            objInputXml.LoadXml(" <PR_SEARCHMIDTDETAILS_INPUT><MONTHFROM /><YEARFROM /><MONTHTO /><YEARTO /> <LCODE /> <WHOLEGROUP /><SHOWAIRBREAKUP />  <AOFFICE /> <REGION />  <AIRLINECODE />  <RESPONSIBLESTAFFID />  <SALESPERSONID />  <CARRIERTYPE />  <LIMITED_TO_REGION />   <LIMITED_TO_OWNAAGENCY />   <LIMITED_TO_AOFFICE />  <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/> </PR_SEARCHMIDTDETAILS_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("MONTHFROM").InnerText = drpMonthFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("MONTHTO").InnerText = drpMonthTo.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("YEARFROM").InnerText = drpYearFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("YEARTO").InnerText = drpYearTo.SelectedValue

            txtTotlaProductivity.Text = "0"

            If Request.QueryString("Lcode") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = objED.Decrypt(Request.QueryString("Lcode").ToString.Trim)
            End If

            If Request.QueryString("Aoff") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = objED.Decrypt(Request.QueryString("Aoff").ToString.Trim)
            End If
            If drpAirLine.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("AIRLINECODE").InnerText = drpAirLine.SelectedValue
            End If


            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If
            If ChkWholeGroup.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "Y"
            Else
                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "N"
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
            If ChkAirBreak.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("SHOWAIRBREAKUP").InnerText = "1"
                objInputXml.DocumentElement.SelectSingleNode("CARRIERTYPE").InnerText = RdCarryType.SelectedValue
            Else
                objInputXml.DocumentElement.SelectSingleNode("SHOWAIRBREAKUP").InnerText = "0"
            End If

            'If drpAirLine.SelectedIndex <> 0 Then
            '    objInputXml.DocumentElement.SelectSingleNode("SHOWAIRBREAKUP").InnerText = "1"
            '    objInputXml.DocumentElement.SelectSingleNode("CARRIERTYPE").InnerText = RdCarryType.SelectedValue
            'End If



            'Start Code for paging and sorting

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "ADDRESS"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "ADDRESS" '"LOCATION_CODE"
            Else
                If ViewState("PrevSearching") Is Nothing Then
                Else
                    Dim objTempInputXml As New XmlDocument
                    Dim objNodeList As XmlNodeList
                    objTempInputXml.LoadXml(ViewState("PrevSearching"))
                    objNodeList = objTempInputXml.DocumentElement.ChildNodes
                    For Each objNode As XmlNode In objNodeList
                        If objNode.Name = "WHOLEGROUP" Then
                            If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                                If ChkWholeGroup.Checked = False Then
                                    If ViewState("SortName") = "ADDRESS" Or ViewState("SortName") = "CITY" Or ViewState("SortName") = "COUNTRY" Then

                                        ViewState("SortName") = "MONTHYEAR"
                                        ViewState("Desc") = "FALSE"
                                    End If
                                End If
                            End If
                        End If
                        If objNode.Name = "SHOWAIRBREAKUP" Then
                            If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                                If ViewState("SortName") = "AIRLINENAME" Then

                                    ViewState("SortName") = "MONTHYEAR"
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


            objOutputXml = objbzMIDT.SearchDetails(objInputXml)

            'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '    ViewState("PrevSearching") = objInputXml.OuterXml
            'End If

            grdvBidtCrsDetails.AllowSorting = False
            grdvBidtCrsDetails.HeaderStyle.ForeColor = Drawing.Color.Black
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDataset = New DataSet
                FooterDataset = ds
                grdvBidtCrsDetails.DataSource = ds.Tables("MIDTDETAILS")
                grdvBidtCrsDetails.DataBind()



                'pnlPaging.Visible = True
            Else
                grdvBidtCrsDetails.DataSource = Nothing
                grdvBidtCrsDetails.DataBind()
                '  txtRecordCount.Text = "0"
                ' txtTotlaProductivity.Text = "0"
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                ' pnlPaging.Visible = False
            End If

            'Dim myCurrentItem As DataListItem
            'Dim cur As DataGridItem
            'Dim cur2 As GridViewRow
            'Dim field As DataControlField
            'Dim field2 As datacontrolro

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
            ' pnlPaging.Visible = False
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing

        End Try
    End Sub
    Private Sub BIDTCrsDetailsExport()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
        Try
            ' objInputXml.LoadXml(" <PR_SEARCHMIDTDETAILS_INPUT><MONTHFROM /><YEARFROM /><MONTHTO /><YEARTO /> <LCODE /> <WHOLEGROUP /><SHOWAIRBREAKUP />  <AOFFICE /> <REGION />  <AIRLINECODE />  <RESPONSIBLESTAFFID />  <SALESPERSONID />  <CARRIERTYPE />  <LIMITED_TO_REGION />   <LIMITED_TO_OWNAAGENCY />   <LIMITED_TO_AOFFICE />   </PR_SEARCHMIDTDETAILS_INPUT>")
            objInputXml.LoadXml(" <PR_SEARCHMIDTDETAILS_INPUT><MONTHFROM /><YEARFROM /><MONTHTO /><YEARTO /> <LCODE /> <WHOLEGROUP /><SHOWAIRBREAKUP />  <AOFFICE /> <REGION />  <AIRLINECODE />  <RESPONSIBLESTAFFID />  <SALESPERSONID />  <CARRIERTYPE />  <LIMITED_TO_REGION />   <LIMITED_TO_OWNAAGENCY />   <LIMITED_TO_AOFFICE />  <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/> </PR_SEARCHMIDTDETAILS_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("MONTHFROM").InnerText = drpMonthFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("MONTHTO").InnerText = drpMonthTo.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("YEARFROM").InnerText = drpYearFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("YEARTO").InnerText = drpYearTo.SelectedValue

            txtTotlaProductivity.Text = "0"

            If Request.QueryString("Lcode") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = objED.Decrypt(Request.QueryString("Lcode").ToString.Trim)
            End If

            If Request.QueryString("Aoff") IsNot Nothing Then
                'objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = objED.Decrypt(Request.QueryString("Aoff").ToString.Trim)
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = (Request.QueryString("Aoff").ToString.Trim)
            End If
            If drpAirLine.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("AIRLINECODE").InnerText = drpAirLine.SelectedValue
            End If


            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If
            If ChkWholeGroup.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "Y"
            Else
                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "N"
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
            If ChkAirBreak.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("SHOWAIRBREAKUP").InnerText = "1"
                objInputXml.DocumentElement.SelectSingleNode("CARRIERTYPE").InnerText = RdCarryType.SelectedValue
            Else
                objInputXml.DocumentElement.SelectSingleNode("SHOWAIRBREAKUP").InnerText = "0"
            End If

            If drpAirLine.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("SHOWAIRBREAKUP").InnerText = "1"
                objInputXml.DocumentElement.SelectSingleNode("CARRIERTYPE").InnerText = RdCarryType.SelectedValue
            End If



            'Start Code for paging and sorting

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "ADDRESS"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "ADDRESS" '"LOCATION_CODE"
            Else
                If ViewState("PrevSearching") Is Nothing Then
                Else
                    Dim objTempInputXml As New XmlDocument
                    Dim objNodeList As XmlNodeList
                    objTempInputXml.LoadXml(ViewState("PrevSearching"))
                    objNodeList = objTempInputXml.DocumentElement.ChildNodes
                    For Each objNode As XmlNode In objNodeList
                        If objNode.Name = "WHOLEGROUP" Then
                            If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                                If ChkWholeGroup.Checked = False Then
                                    If ViewState("SortName") = "ADDRESS" Or ViewState("SortName") = "CITY" Or ViewState("SortName") = "COUNTRY" Then

                                        ViewState("SortName") = "MONTHYEAR"
                                        ViewState("Desc") = "FALSE"
                                    End If
                                End If
                            End If
                        End If
                        If objNode.Name = "SHOWAIRBREAKUP" Then
                            If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                                If ViewState("SortName") = "AIRLINENAME" Then

                                    ViewState("SortName") = "MONTHYEAR"
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


            objOutputXml = objbzMIDT.SearchDetails(objInputXml)

            'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '    ViewState("PrevSearching") = objInputXml.OuterXml
            'End If

            grdvBidtCrsDetails.AllowSorting = False
            grdvBidtCrsDetails.HeaderStyle.ForeColor = Drawing.Color.Black
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDataset = New DataSet
                FooterDataset = ds
                grdvBidtCrsDetails.DataSource = ds.Tables("MIDTDETAILS")
                grdvBidtCrsDetails.DataBind()

                '@ Code For Exporting the Data

                Dim objOutputXmlExport As New XmlDocument
                Dim objXmlNode, objXmlNodeClone As XmlNode
                objOutputXmlExport.LoadXml(ds.GetXml().ToString)

                objXmlNode = objOutputXmlExport.DocumentElement.SelectSingleNode("MIDTDETAILS")
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
                For intclmn As Integer = 0 To grdvBidtCrsDetails.HeaderRow.Cells.Count - 2
                    If grdvBidtCrsDetails.HeaderRow.Cells(intclmn).Visible = False Then
                        IntInvisible = IntInvisible + 1
                    End If
                Next
                Dim strArray(grdvBidtCrsDetails.HeaderRow.Cells.Count - 2 - IntInvisible) As String
                Dim intArray(grdvBidtCrsDetails.HeaderRow.Cells.Count - 2 - IntInvisible) As Integer

                Dim intclmnVis As Integer = 0


                For intclmn As Integer = 0 To objXmlNodeClone.Attributes.Count - 2
                    If grdvBidtCrsDetails.HeaderRow.Cells(intclmn).Visible = True Then
                        strArray(intclmnVis) = grdvBidtCrsDetails.Columns(intclmn).HeaderText.ToString().Replace("'", " ") 'objOutputXml.DocumentElement.SelectSingleNode("BIDTMONTHLYBREAKUP").Attributes(intclmn - 1).Name
                        '@ Finding Position From xml Related with Header Text
                        For kk As Integer = 0 To objXmlNodeClone.Attributes.Count - 1
                            If objXmlNodeClone.Attributes(kk).Name.Trim = grdvBidtCrsDetails.Columns(intclmn).SortExpression.Trim Then
                                intArray(intclmnVis) = kk
                                intclmnVis = intclmnVis + 1
                                Exit For
                            End If
                        Next kk
                    End If
                Next intclmn

                objExport.ExportDetails(objOutputXmlExport, "MIDTDETAILS", intArray, strArray, ExportExcel.ExportFormat.Excel, "CRSDetails.xls")


                '@ end of Code For Exporting the Data

                'pnlPaging.Visible = True
            Else
                grdvBidtCrsDetails.DataSource = Nothing
                grdvBidtCrsDetails.DataBind()
                txtRecordCount.Text = "0"
                txtTotlaProductivity.Text = "0"
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
            End If

            'Dim myCurrentItem As DataListItem
            'Dim cur As DataGridItem
            'Dim cur2 As GridViewRow
            'Dim field As DataControlField
            'Dim field2 As datacontrolro

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
            pnlPaging.Visible = False
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing

        End Try
    End Sub

    Protected Sub drpAirLine_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpAirLine.SelectedIndexChanged
        Try
            txtTotlaProductivity.Text = 0
            txtRecordCount.Text = 0
            pnlPaging.Visible = False
            If drpAirLine.SelectedIndex = 0 Then
                ChkAirBreak.Checked = False
                RdCarryType.Enabled = False
                ChkAirBreak.Enabled = True
            Else
                ChkAirBreak.Checked = True
                RdCarryType.Enabled = False
                ChkAirBreak.Enabled = False
                RdCarryType.SelectedValue = ""
            End If
            BIDTCrsDetailsSearch()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ChkAirBreak_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkAirBreak.CheckedChanged
        txtTotlaProductivity.Text = 0
        txtRecordCount.Text = 0
        pnlPaging.Visible = False
        If ChkAirBreak.Checked = True Then
            If drpAirLine.SelectedIndex = 0 Then
                RdCarryType.Enabled = True
            Else
                RdCarryType.Enabled = False
            End If
        Else
            If drpAirLine.SelectedIndex = 0 Then
                RdCarryType.Enabled = False
            Else
                RdCarryType.Enabled = False
            End If
        End If
        BIDTCrsDetailsSearch()
    End Sub

    Protected Sub RdCarryType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RdCarryType.SelectedIndexChanged
        txtTotlaProductivity.Text = 0
        txtRecordCount.Text = 0
        pnlPaging.Visible = False
        BIDTCrsDetailsSearch()

    End Sub

    Private Sub AgencyView()
        ' If (Session("Action") IsNot Nothing) Then

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency

        objInputXml.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")

        'objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = Lcode ''objED.Decrypt(Request.QueryString("Lcode").Trim())


        If Request.QueryString("Lcode") IsNot Nothing Then
            objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = objED.Decrypt(Request.QueryString("Lcode").ToString.Trim)
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
        Else
            '  lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
        '  End If
    End Sub

End Class









'Imports system.IO
'Imports system.Text
'Partial Class Productivity_PRD_BIDT_CRSDetails
'    Inherits System.Web.UI.Page
'    Dim objeAAMS As New eAAMS
'    Dim FooterDataset As DataSet
'    Dim objED As New EncyrptDeCyrpt
'    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
'        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
'            If Response.ContentType = "text/html" Then
'                Response.Filter = New TrimStream(Response.Filter)
'            End If
'        End If
'    End Sub
'    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
'        Try
'            FooterDataset = New DataSet
'            btnPrint.Attributes.Add("onclick", "CallPrint('grdvBidtCrsDetails')")
'            lblError.Text = ""
'            If Session("Security") Is Nothing Then
'                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
'                Exit Sub
'            End If
'            btnSearch.Attributes.Add("onclick", "return CheckValidation();")

'            Dim strBuilder As New StringBuilder
'            Dim objSecurityXml As New XmlDocument
'            objSecurityXml.LoadXml(Session("Security"))
'            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
'                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Count <> 0 Then
'                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Attributes("Value").Value)
'                    If strBuilder(0) = "0" Then
'                        btnSearch.Enabled = False
'                        Response.Redirect("~/NoRights.aspx", False)
'                        Exit Sub
'                    End If
'                    If strBuilder(4) = "0" Then
'                        btnExport.Enabled = False
'                    End If
'                End If
'            Else
'                strBuilder = objeAAMS.SecurityCheck(31)
'            End If


'            If Not IsPostBack Then
'                ChkAirBreak.Attributes.Add("onclick", "return EnableCarrierType2();")
'                drpAirLine.Attributes.Add("onchange", "return EnableCarrierType();")
'                drpAirLine.Attributes.Add("onkeyup", "return gotops('drpAirLine');")
'                drpAirLine.Attributes.Add("onchange", "return EnableCarrierType3();")
'                txtRecordCount.Text = "0"
'                BindAllControl()
'                ' var parameter="Fmonth=" + FMonth  + 
'                '"&TMonth=" + TMonth + "&FYear=" + FYear + .
'                ' "&TYear=" + TYear +  "&Lcode=" + Lcode + 
'                ' "&Aoff=" + Aoff +  "&GrData=" + GrData +  
'                '"&UseOrig=" + UseOrig  +  "&ResId=" + ResId  +  
'                '"&Air=" + Air  +  "&Car=" + Car +  "&Hotel=" 
'                '+ Hotel +  "&Insurance=" + Insurance + "&LimAoff=" + LimAoff + 
'                '"&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff ;
'                If Request.QueryString("Fmonth") IsNot Nothing Then
'                    drpMonthFrom.SelectedValue = Request.QueryString("Fmonth").ToString
'                End If
'                If Request.QueryString("TMonth") IsNot Nothing Then
'                    drpMonthTo.SelectedValue = Request.QueryString("TMonth").ToString
'                End If
'                If Request.QueryString("FYear") IsNot Nothing Then
'                    drpYearFrom.SelectedValue = Request.QueryString("FYear").ToString
'                End If
'                If Request.QueryString("TYear") IsNot Nothing Then
'                    drpYearTo.SelectedValue = Request.QueryString("TYear").ToString
'                End If

'                If Request.QueryString("Agency") IsNot Nothing Then
'                    txtAgencyName.Text = Request.QueryString("Agency").ToString
'                    txtAgencyName.Text = Server.HtmlDecode(txtAgencyName.Text)
'                End If
'                If Request.QueryString("Add") IsNot Nothing Then
'                    txtAdd.Text = Request.QueryString("Add").ToString
'                    txtAdd.Text = Server.HtmlDecode(txtAdd.Text)
'                End If
'                If Request.QueryString("City") IsNot Nothing Then
'                    txtCity.Text = Request.QueryString("City").ToString
'                End If
'                If Request.QueryString("Country") IsNot Nothing Then
'                    txtCountry.Text = Request.QueryString("Country").ToString
'                End If
'                If Request.QueryString("GrData") IsNot Nothing Then
'                    If Request.QueryString("GrData").ToString = "1" Then
'                        ChkWholeGroup.Checked = True
'                    Else
'                        ChkWholeGroup.Checked = False
'                    End If
'                End If
'                txtTotlaProductivity.Text = "0"
'                If Request.QueryString("Popup2") Is Nothing Then
'                    BIDTCrsDetailsSearch()
'                End If
'            End If
'        Catch ex As Exception

'        End Try
'    End Sub
'    Private Sub BIDTCrsDetailsSearch()
'        Dim objInputXml, objOutputXml As New XmlDocument
'        Dim objXmlReader As XmlNodeReader
'        Dim ds As New DataSet
'        Dim objSecurityXml As New XmlDocument
'        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
'        Try
'            ' objInputXml.LoadXml(" <PR_SEARCHMIDTDETAILS_INPUT><MONTHFROM /><YEARFROM /><MONTHTO /><YEARTO /> <LCODE /> <WHOLEGROUP /><SHOWAIRBREAKUP />  <AOFFICE /> <REGION />  <AIRLINECODE />  <RESPONSIBLESTAFFID />  <SALESPERSONID />  <CARRIERTYPE />  <LIMITED_TO_REGION />   <LIMITED_TO_OWNAAGENCY />   <LIMITED_TO_AOFFICE />   </PR_SEARCHMIDTDETAILS_INPUT>")
'            objInputXml.LoadXml(" <PR_SEARCHMIDTDETAILS_INPUT><MONTHFROM /><YEARFROM /><MONTHTO /><YEARTO /> <LCODE /> <WHOLEGROUP /><SHOWAIRBREAKUP />  <AOFFICE /> <REGION />  <AIRLINECODE />  <RESPONSIBLESTAFFID />  <SALESPERSONID />  <CARRIERTYPE />  <LIMITED_TO_REGION />   <LIMITED_TO_OWNAAGENCY />   <LIMITED_TO_AOFFICE />  <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/> </PR_SEARCHMIDTDETAILS_INPUT>")
'            '         <PR_SEARCHMIDTDETAILS_INPUT>
'            '<MONTHFROM /> 
'            '<YEARFROM /> 
'            '<MONTHTO /> 
'            '<YEARTO /> 
'            '<LCODE /> 
'            '<WHOLEGROUP /> 
'            '<SHOWAIRBREAKUP /> 
'            '<AOFFICE /> 
'            '<REGION /> 
'            '<AIRLINECODE /> 
'            '<RESPONSIBLESTAFFID /> 
'            '<SALESPERSONID /> 
'            '<CARRIERTYPE /> 
'            '<LIMITED_TO_REGION /> 
'            '<LIMITED_TO_OWNAAGENCY /> 
'            '<LIMITED_TO_AOFFICE /> 
'            '</PR_SEARCHMIDTDETAILS_INPUT>
'            ' <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
'            objInputXml.DocumentElement.SelectSingleNode("MONTHFROM").InnerText = drpMonthFrom.SelectedValue
'            objInputXml.DocumentElement.SelectSingleNode("MONTHTO").InnerText = drpMonthTo.SelectedValue
'            objInputXml.DocumentElement.SelectSingleNode("YEARFROM").InnerText = drpYearFrom.SelectedValue
'            objInputXml.DocumentElement.SelectSingleNode("YEARTO").InnerText = drpYearTo.SelectedValue

'            txtTotlaProductivity.Text = "0"

'            If Request.QueryString("Lcode") IsNot Nothing Then
'                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = objED.Decrypt(Request.QueryString("Lcode").ToString.Trim)
'            End If

'            If Request.QueryString("Aoff") IsNot Nothing Then
'                'objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = objED.Decrypt(Request.QueryString("Aoff").ToString.Trim)
'                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = (Request.QueryString("Aoff").ToString.Trim)
'            End If
'            If drpAirLine.SelectedIndex <> 0 Then
'                objInputXml.DocumentElement.SelectSingleNode("AIRLINECODE").InnerText = drpAirLine.SelectedValue
'            End If


'            If Not Session("LoginSession") Is Nothing Then
'                objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = Session("LoginSession").ToString().Split("|")(0)
'            End If
'            If ChkWholeGroup.Checked = True Then
'                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "Y"
'            Else
'                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "N"
'            End If

'            If Request.QueryString("LimAoff") IsNot Nothing Then
'                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = Request.QueryString("LimAoff").ToString
'            End If
'            If Request.QueryString("LimReg") IsNot Nothing Then
'                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = Request.QueryString("LimReg").ToString
'            End If
'            If Request.QueryString("LimOwnOff") IsNot Nothing Then
'                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = Request.QueryString("LimOwnOff").ToString
'            End If

'            If ChkAirBreak.Checked = True Then
'                objInputXml.DocumentElement.SelectSingleNode("SHOWAIRBREAKUP").InnerText = "1"
'                objInputXml.DocumentElement.SelectSingleNode("CARRIERTYPE").InnerText = RdCarryType.SelectedValue
'            Else
'                objInputXml.DocumentElement.SelectSingleNode("SHOWAIRBREAKUP").InnerText = "0"
'            End If

'            'If drpAirLine.SelectedIndex <> 0 Then
'            '    objInputXml.DocumentElement.SelectSingleNode("SHOWAIRBREAKUP").InnerText = "1"
'            '    objInputXml.DocumentElement.SelectSingleNode("CARRIERTYPE").InnerText = RdCarryType.SelectedValue
'            'End If

'            'Start CODE for sorting and paging

'            If ViewState("PrevSearching") Is Nothing Then
'                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
'            Else
'                Dim objTempInputXml As New XmlDocument
'                Dim objNodeList As XmlNodeList

'                objTempInputXml.LoadXml(ViewState("PrevSearching"))
'                objNodeList = objTempInputXml.DocumentElement.ChildNodes
'                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
'                For Each objNode As XmlNode In objNodeList
'                    If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
'                        If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
'                            objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
'                            ddlPageNumber.SelectedValue = "1"
'                        End If
'                    End If
'                Next
'            End If


'            objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
'            If ViewState("SortName") Is Nothing Then
'                ViewState("SortName") = "MONTHYEAR"
'                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "MONTHYEAR" '"LOCATION_CODE"
'            Else
'                If ViewState("PrevSearching") Is Nothing Then
'                Else
'                    Dim objTempInputXml As New XmlDocument
'                    Dim objNodeList As XmlNodeList
'                    objTempInputXml.LoadXml(ViewState("PrevSearching"))
'                    objNodeList = objTempInputXml.DocumentElement.ChildNodes
'                    For Each objNode As XmlNode In objNodeList
'                        If objNode.Name = "WHOLEGROUP" Then
'                            If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
'                                If ChkWholeGroup.Checked = False Then
'                                    If ViewState("SortName") = "ADDRESS" Or ViewState("SortName") = "CITY" Or ViewState("SortName") = "COUNTRY" Then

'                                        ViewState("SortName") = "MONTHYEAR"
'                                        ViewState("Desc") = "FALSE"
'                                    End If
'                                End If
'                            End If
'                        End If
'                        If objNode.Name = "SHOWAIRBREAKUP" Then
'                            If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
'                                If ViewState("SortName") = "AIRLINENAME" Then

'                                    ViewState("SortName") = "MONTHYEAR"
'                                    ViewState("Desc") = "FALSE"
'                                End If
'                            End If
'                        End If
'                    Next
'                End If

'                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
'            End If

'            If ViewState("Desc") Is Nothing Then
'                ViewState("Desc") = "FALSE"
'                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
'            Else
'                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
'            End If

'            'End Code for paging and sorting


'            objOutputXml = objbzMIDT.SearchDetails(objInputXml)

'            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
'                ViewState("PrevSearching") = objInputXml.OuterXml
'            End If

'            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
'                objXmlReader = New XmlNodeReader(objOutputXml)
'                ds.ReadXml(objXmlReader)
'                FooterDataset = New DataSet
'                FooterDataset = ds
'                grdvBidtCrsDetails.DataSource = ds.Tables("MIDTDETAILS")
'                grdvBidtCrsDetails.DataBind()
'                ' txtRecordCount.Text = ds.Tables("MIDTDETAILS").Rows.Count.ToString



'                ' ##################################################################
'                '@ Code Added For Paging And Sorting In case Of 
'                ' ###################################################################
'                BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
'                txtRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
'                SetImageForSorting(grdvBidtCrsDetails)
'                ' ###################################################################
'                '@ End of Code Added For Paging And Sorting In case 
'                ' ###################################################################

'                pnlPaging.Visible = True
'            Else
'                grdvBidtCrsDetails.DataSource = Nothing
'                grdvBidtCrsDetails.DataBind()
'                txtRecordCount.Text = "0"
'                txtTotlaProductivity.Text = "0"
'                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
'                pnlPaging.Visible = False
'            End If


'        Catch ex As Exception
'            lblError.Text = ex.Message.ToString
'            pnlPaging.Visible = False
'        Finally
'            objInputXml = Nothing
'            objOutputXml = Nothing

'        End Try
'    End Sub
'    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
'        txtRecordCount.Text = "0"
'        BIDTCrsDetailsSearch()
'    End Sub
'    Private Sub BindAllControl()
'        Try
'            objeAAMS.BindDropDown(drpAirLine, "AIRLINE", True, 1)
'            Dim i, j As Integer
'            drpMonthTo.SelectedValue = "12"
'            drpMonthFrom.SelectedValue = "1"
'            For j = 1990 To DateTime.Now.Year
'                drpYearFrom.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
'                drpYearTo.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
'                i += 1
'            Next
'            drpYearFrom.SelectedValue = DateTime.Now.Year
'            drpYearTo.SelectedValue = DateTime.Now.Year
'        Catch ex As Exception
'            lblError.Text = ex.Message.ToString
'        End Try
'    End Sub

'    Protected Sub grdvBidtCrsDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvBidtCrsDetails.RowDataBound
'        Try
'            Dim objSecurityXml As New XmlDocument

'            If ChkWholeGroup.Checked = True Then
'                e.Row.Cells(0).Visible = True
'                e.Row.Cells(1).Visible = True
'                e.Row.Cells(2).Visible = True
'            Else
'                e.Row.Cells(0).Visible = False
'                e.Row.Cells(1).Visible = False
'                e.Row.Cells(2).Visible = False
'            End If
'            If ChkAirBreak.Checked = False Then
'                e.Row.Cells(4).Visible = False
'            Else
'                e.Row.Cells(4).Visible = True
'            End If

'            'If drpAirLine.SelectedIndex <> 0 Then
'            '    e.Row.Cells(4).Visible = True
'            'Else
'            '    e.Row.Cells(4).Visible = False
'            'End If
'            If e.Row.RowIndex < 0 Then

'                Exit Sub
'            End If
'            Dim hdTot As HiddenField
'            hdTot = CType(e.Row.FindControl("hdTot"), HiddenField)
'            'txtTotlaProductivity.Text = Val(txtTotlaProductivity.Text) + Val(hdTot.Value)
'            If FooterDataset IsNot Nothing Then
'                If FooterDataset.Tables("PAGE_TOTAL") IsNot Nothing Then
'                    txtTotlaProductivity.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("TOTAL").ToString
'                End If
'            End If

'        Catch ex As Exception

'        End Try
'    End Sub

'    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
'        Try
'            txtRecordCount.Text = "0"
'            grdvBidtCrsDetails.AllowSorting = False
'            grdvBidtCrsDetails.HeaderStyle.ForeColor = Drawing.Color.Black
'            BIDTCrsDetailsExport()
'            'BIDTCrsDetailsSearch2()

'            'If grdvBidtCrsDetails.Rows.Count > 0 Then
'            '    'PrepareGridViewForExport(grdvBidtCrsDetails)
'            '    grdvBidtCrsDetails.Columns(grdvBidtCrsDetails.Columns.Count - 1).Visible = False
'            '    ' grdvBidtCrsDetails.Columns(10).Visible = False
'            '    ExportGridView(grdvBidtCrsDetails)
'            'End If
'        Catch ex As Exception
'            lblError.Text = ex.Message
'        End Try

'    End Sub

'    Private Sub ExportGridView(ByVal gv2 As GridView)
'        Dim attachment As String = "attachment; filename=CRSDetails.xls"
'        Response.ClearContent()
'        Response.AddHeader("content-disposition", attachment)
'        Response.ContentType = "application/ms-excel"
'        Dim sw As New StringWriter
'        Dim htw As New HtmlTextWriter(sw)
'        Dim frm As New HtmlForm()
'        gv2.Parent.Controls.Add(frm)
'        frm.Attributes("runat") = "server"
'        frm.Controls.Add(gv2)
'        frm.RenderControl(htw)
'        Response.Write(sw.ToString())
'        Response.End()

'    End Sub

'    'Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

'    'End Sub
'    Private Sub PrepareGridViewForExport(ByVal gv As Control)
'        'LinkButton lb = new LinkButton();
'        Dim l As New Literal
'        Dim name As String = ""
'        Dim lb As New LinkButton

'        Dim i As Int32
'        For i = 0 To gv.Controls.Count - 1
'            If (gv.Controls(i).GetType Is GetType(LinkButton)) Then
'                l.Text = CType(gv.Controls(i), LinkButton).Text
'                gv.Controls.Remove(gv.Controls(i))
'                gv.Controls.AddAt(i, l)

'            End If

'            If (gv.Controls(i).HasControls()) Then
'                PrepareGridViewForExport(gv.Controls(i))
'            End If

'        Next
'    End Sub

'    Protected Sub btnGraph_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGraph.Click
'        Dim objInputXml, objOutputXml As New XmlDocument
'        Dim objXmlReader As XmlNodeReader
'        Dim ds As New DataSet
'        Dim objSecurityXml As New XmlDocument
'        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
'        Try
'            ' objInputXml.LoadXml(" <PR_SEARCHMIDTDETAILS_INPUT><MONTHFROM /><YEARFROM /><MONTHTO /><YEARTO /> <LCODE /> <WHOLEGROUP /><SHOWAIRBREAKUP />  <AOFFICE /> <REGION />  <AIRLINECODE />  <RESPONSIBLESTAFFID />  <SALESPERSONID />  <CARRIERTYPE />  <LIMITED_TO_REGION />   <LIMITED_TO_OWNAAGENCY />   <LIMITED_TO_AOFFICE />   </PR_SEARCHMIDTDETAILS_INPUT>")
'            objInputXml.LoadXml(" <PR_SEARCHMIDTDETAILS_INPUT><MONTHFROM /><YEARFROM /><MONTHTO /><YEARTO /> <LCODE /> <WHOLEGROUP /><SHOWAIRBREAKUP />  <AOFFICE /> <REGION />  <AIRLINECODE />  <RESPONSIBLESTAFFID />  <SALESPERSONID />  <CARRIERTYPE />  <LIMITED_TO_REGION />   <LIMITED_TO_OWNAAGENCY />   <LIMITED_TO_AOFFICE />  <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/> </PR_SEARCHMIDTDETAILS_INPUT>")
'            '         <PR_SEARCHMIDTDETAILS_INPUT>
'            '<MONTHFROM /> 
'            '<YEARFROM /> 
'            '<MONTHTO /> 
'            '<YEARTO /> 
'            '<LCODE /> 
'            '<WHOLEGROUP /> 
'            '<SHOWAIRBREAKUP /> 
'            '<AOFFICE /> 
'            '<REGION /> 
'            '<AIRLINECODE /> 
'            '<RESPONSIBLESTAFFID /> 
'            '<SALESPERSONID /> 
'            '<CARRIERTYPE /> 
'            '<LIMITED_TO_REGION /> 
'            '<LIMITED_TO_OWNAAGENCY /> 
'            '<LIMITED_TO_AOFFICE /> 
'            '</PR_SEARCHMIDTDETAILS_INPUT>
'            objInputXml.DocumentElement.SelectSingleNode("MONTHFROM").InnerText = drpMonthFrom.SelectedValue
'            objInputXml.DocumentElement.SelectSingleNode("MONTHTO").InnerText = drpMonthTo.SelectedValue
'            objInputXml.DocumentElement.SelectSingleNode("YEARFROM").InnerText = drpYearFrom.SelectedValue
'            objInputXml.DocumentElement.SelectSingleNode("YEARTO").InnerText = drpYearTo.SelectedValue

'            txtTotlaProductivity.Text = "0"

'            If Request.QueryString("Lcode") IsNot Nothing Then
'                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = objED.Decrypt(Request.QueryString("Lcode").ToString.Trim)
'            End If

'            If Request.QueryString("Aoff") IsNot Nothing Then
'                '   objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = objED.Decrypt(Request.QueryString("Aoff").ToString.Trim)
'                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = (Request.QueryString("Aoff").ToString.Trim)
'            End If
'            If drpAirLine.SelectedIndex <> 0 Then
'                objInputXml.DocumentElement.SelectSingleNode("AIRLINECODE").InnerText = drpAirLine.SelectedValue
'            End If


'            If Not Session("LoginSession") Is Nothing Then
'                objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = Session("LoginSession").ToString().Split("|")(0)
'            End If
'            If ChkWholeGroup.Checked = True Then
'                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "Y"
'            Else
'                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "N"
'            End If

'            If Request.QueryString("LimAoff") IsNot Nothing Then
'                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = Request.QueryString("LimAoff").ToString
'            End If
'            If Request.QueryString("LimReg") IsNot Nothing Then
'                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = Request.QueryString("LimReg").ToString
'            End If
'            If Request.QueryString("LimOwnOff") IsNot Nothing Then
'                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = Request.QueryString("LimOwnOff").ToString
'            End If
'            If ChkAirBreak.Checked = True Then
'                objInputXml.DocumentElement.SelectSingleNode("SHOWAIRBREAKUP").InnerText = "1"
'                objInputXml.DocumentElement.SelectSingleNode("CARRIERTYPE").InnerText = RdCarryType.SelectedValue
'            Else
'                objInputXml.DocumentElement.SelectSingleNode("SHOWAIRBREAKUP").InnerText = "0"
'            End If

'            'Start Code for paging and sorting

'            If ViewState("SortName") Is Nothing Then
'                ViewState("SortName") = "ADDRESS"
'                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "ADDRESS" '"LOCATION_CODE"
'            Else
'                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
'            End If

'            If ViewState("Desc") Is Nothing Then
'                ViewState("Desc") = "FALSE"
'                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
'            Else
'                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
'            End If

'            'End Code for paging and sorting

'            objOutputXml = objbzMIDT.SearchDetails(objInputXml)

'            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
'                Session("CRSDETAILS") = objOutputXml.OuterXml
'                Response.Redirect("../RPSR_ReportShow.aspx?Case=CRSDETAILS")
'            Else
'                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
'                pnlPaging.Visible = False
'                txtRecordCount.Text = "0"
'                txtTotlaProductivity.Text = "0"
'            End If


'        Catch ex As Exception
'            lblError.Text = ex.Message.ToString
'            pnlPaging.Visible = False
'            txtRecordCount.Text = "0"
'            txtTotlaProductivity.Text = "0"
'        Finally
'            objInputXml = Nothing
'            objOutputXml = Nothing

'        End Try


'    End Sub


'    Private Sub BindControlsForNavigation(ByVal CrrentPageNo As String)
'        ' ##################################################################
'        '@ Code Added For Paging And Sorting 
'        ' ###################################################################
'        pnlPaging.Visible = True
'        '  Dim count As Integer = 0
'        Dim count As Integer = CInt(CrrentPageNo)
'        Dim selectedValue As String = IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue)
'        If count <> ddlPageNumber.Items.Count Then
'            ddlPageNumber.Items.Clear()
'            For i As Integer = 1 To count
'                ddlPageNumber.Items.Add(i.ToString)
'            Next
'        End If
'        ddlPageNumber.SelectedValue = selectedValue
'        'Code for hiding prev and next button based on count
'        If count = 1 Then
'            'pnlPaging.Visible = False
'            ' ddlPageNumber.Visible = False
'            lnkNext.Visible = False
'            lnkPrev.Visible = False
'        Else
'            'ddlPageNumber.Visible = True
'            lnkPrev.Visible = True
'            lnkNext.Visible = True
'        End If

'        'Code for hiding next button when pagenumber is equal to page count
'        If ddlPageNumber.SelectedValue = count.ToString Then
'            lnkNext.Visible = False
'        Else
'            lnkNext.Visible = True
'        End If

'        'Code for hiding prev button when pagenumber is 1
'        If ddlPageNumber.SelectedValue = "1" Then
'            lnkPrev.Visible = False
'        Else
'            lnkPrev.Visible = True
'        End If

'        ' ###################################################################
'        '@ End of Code Added For Paging 
'        ' ###################################################################
'    End Sub
'    Private Sub SetImageForSorting(ByVal grd As GridView)
'        Dim imgUp As New Image
'        imgUp.ImageUrl = "~/Images/Sortup.gif"
'        Dim imgDown As New Image
'        imgDown.ImageUrl = "~/Images/Sortdown.gif"
'        Dim field As DataControlField
'        For Each field In grd.Columns
'            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
'                Dim intcol As Integer = grd.Columns.IndexOf(field)
'                If ViewState("Desc") = "FALSE" Then
'                    grd.HeaderRow.Cells(intcol).Controls.Add(imgUp)
'                End If
'                If ViewState("Desc") = "TRUE" Then
'                    grd.HeaderRow.Cells(intcol).Controls.Add(imgDown)
'                End If
'            End If
'        Next
'    End Sub




'    ' ###################################################################
'#Region "Code for Paging And sorting."
'    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
'        Try
'            If ddlPageNumber.SelectedValue <> "1" Then
'                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
'            End If
'            BIDTCrsDetailsSearch()
'        Catch ex As Exception
'            lblError.Text = ex.Message
'        End Try
'    End Sub

'    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
'        Try
'            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
'                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
'            End If
'            BIDTCrsDetailsSearch()
'        Catch ex As Exception
'            lblError.Text = ex.Message
'        End Try
'    End Sub

'    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
'        Try
'            BIDTCrsDetailsSearch()
'        Catch ex As Exception
'            lblError.Text = ex.Message
'        End Try
'    End Sub

'    Protected Sub grdvBidtCrsDetails_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvBidtCrsDetails.Sorted
'        Try

'        Catch ex As Exception

'        End Try
'    End Sub
'    Protected Sub grdvBidtCrsDetails_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvBidtCrsDetails.Sorting
'        Try
'            Dim SortName As String = e.SortExpression
'            If ViewState("SortName") Is Nothing Then
'                ViewState("SortName") = SortName
'                ViewState("Desc") = "FALSE"
'            Else
'                If ViewState("SortName") = SortName Then
'                    If ViewState("Desc") = "TRUE" Then
'                        ViewState("Desc") = "FALSE"
'                    Else
'                        ViewState("Desc") = "TRUE"
'                    End If
'                Else
'                    ViewState("SortName") = SortName
'                    ViewState("Desc") = "FALSE"
'                End If
'            End If
'            BIDTCrsDetailsSearch()

'            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
'        Catch ex As Exception
'            lblError.Text = ex.Message
'        End Try
'    End Sub
'    Protected Sub grdvBidtCrsDetails_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvBidtCrsDetails.RowCreated
'        Try

'        Catch ex As Exception
'        End Try
'    End Sub



'#End Region
'    ' ###################################################################

'    Protected Sub ChkWholeGroup_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkWholeGroup.CheckedChanged
'        Try
'            BIDTCrsDetailsSearch()
'        Catch ex As Exception
'            lblError.Text = ex.Message
'        End Try
'    End Sub

'    Private Sub BIDTCrsDetailsSearch2()
'        Dim objInputXml, objOutputXml As New XmlDocument
'        Dim objXmlReader As XmlNodeReader
'        Dim ds As New DataSet
'        Dim objSecurityXml As New XmlDocument
'        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
'        Try
'            ' objInputXml.LoadXml(" <PR_SEARCHMIDTDETAILS_INPUT><MONTHFROM /><YEARFROM /><MONTHTO /><YEARTO /> <LCODE /> <WHOLEGROUP /><SHOWAIRBREAKUP />  <AOFFICE /> <REGION />  <AIRLINECODE />  <RESPONSIBLESTAFFID />  <SALESPERSONID />  <CARRIERTYPE />  <LIMITED_TO_REGION />   <LIMITED_TO_OWNAAGENCY />   <LIMITED_TO_AOFFICE />   </PR_SEARCHMIDTDETAILS_INPUT>")
'            objInputXml.LoadXml(" <PR_SEARCHMIDTDETAILS_INPUT><MONTHFROM /><YEARFROM /><MONTHTO /><YEARTO /> <LCODE /> <WHOLEGROUP /><SHOWAIRBREAKUP />  <AOFFICE /> <REGION />  <AIRLINECODE />  <RESPONSIBLESTAFFID />  <SALESPERSONID />  <CARRIERTYPE />  <LIMITED_TO_REGION />   <LIMITED_TO_OWNAAGENCY />   <LIMITED_TO_AOFFICE />  <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/> </PR_SEARCHMIDTDETAILS_INPUT>")

'            objInputXml.DocumentElement.SelectSingleNode("MONTHFROM").InnerText = drpMonthFrom.SelectedValue
'            objInputXml.DocumentElement.SelectSingleNode("MONTHTO").InnerText = drpMonthTo.SelectedValue
'            objInputXml.DocumentElement.SelectSingleNode("YEARFROM").InnerText = drpYearFrom.SelectedValue
'            objInputXml.DocumentElement.SelectSingleNode("YEARTO").InnerText = drpYearTo.SelectedValue

'            txtTotlaProductivity.Text = "0"

'            If Request.QueryString("Lcode") IsNot Nothing Then
'                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = objED.Decrypt(Request.QueryString("Lcode").ToString.Trim)
'            End If

'            If Request.QueryString("Aoff") IsNot Nothing Then
'                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = objED.Decrypt(Request.QueryString("Aoff").ToString.Trim)
'            End If
'            If drpAirLine.SelectedIndex <> 0 Then
'                objInputXml.DocumentElement.SelectSingleNode("AIRLINECODE").InnerText = drpAirLine.SelectedValue
'            End If


'            If Not Session("LoginSession") Is Nothing Then
'                objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = Session("LoginSession").ToString().Split("|")(0)
'            End If
'            If ChkWholeGroup.Checked = True Then
'                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "Y"
'            Else
'                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "N"
'            End If

'            If Request.QueryString("LimAoff") IsNot Nothing Then
'                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = Request.QueryString("LimAoff").ToString
'            End If
'            If Request.QueryString("LimReg") IsNot Nothing Then
'                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = Request.QueryString("LimReg").ToString
'            End If
'            If Request.QueryString("LimOwnOff") IsNot Nothing Then
'                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = Request.QueryString("LimOwnOff").ToString
'            End If
'            If ChkAirBreak.Checked = True Then
'                objInputXml.DocumentElement.SelectSingleNode("SHOWAIRBREAKUP").InnerText = "1"
'                objInputXml.DocumentElement.SelectSingleNode("CARRIERTYPE").InnerText = RdCarryType.SelectedValue
'            Else
'                objInputXml.DocumentElement.SelectSingleNode("SHOWAIRBREAKUP").InnerText = "0"
'            End If

'            'If drpAirLine.SelectedIndex <> 0 Then
'            '    objInputXml.DocumentElement.SelectSingleNode("SHOWAIRBREAKUP").InnerText = "1"
'            '    objInputXml.DocumentElement.SelectSingleNode("CARRIERTYPE").InnerText = RdCarryType.SelectedValue
'            'End If



'            'Start Code for paging and sorting

'            If ViewState("SortName") Is Nothing Then
'                ViewState("SortName") = "ADDRESS"
'                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "ADDRESS" '"LOCATION_CODE"
'            Else
'                If ViewState("PrevSearching") Is Nothing Then
'                Else
'                    Dim objTempInputXml As New XmlDocument
'                    Dim objNodeList As XmlNodeList
'                    objTempInputXml.LoadXml(ViewState("PrevSearching"))
'                    objNodeList = objTempInputXml.DocumentElement.ChildNodes
'                    For Each objNode As XmlNode In objNodeList
'                        If objNode.Name = "WHOLEGROUP" Then
'                            If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
'                                If ChkWholeGroup.Checked = False Then
'                                    If ViewState("SortName") = "ADDRESS" Or ViewState("SortName") = "CITY" Or ViewState("SortName") = "COUNTRY" Then

'                                        ViewState("SortName") = "MONTHYEAR"
'                                        ViewState("Desc") = "FALSE"
'                                    End If
'                                End If
'                            End If
'                        End If
'                        If objNode.Name = "SHOWAIRBREAKUP" Then
'                            If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
'                                If ViewState("SortName") = "AIRLINENAME" Then

'                                    ViewState("SortName") = "MONTHYEAR"
'                                    ViewState("Desc") = "FALSE"
'                                End If
'                            End If
'                        End If
'                    Next
'                End If

'                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
'            End If

'            If ViewState("Desc") Is Nothing Then
'                ViewState("Desc") = "FALSE"
'                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
'            Else
'                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
'            End If

'            'End Code for paging and sorting


'            objOutputXml = objbzMIDT.SearchDetails(objInputXml)

'            'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
'            '    ViewState("PrevSearching") = objInputXml.OuterXml
'            'End If

'            grdvBidtCrsDetails.AllowSorting = False
'            grdvBidtCrsDetails.HeaderStyle.ForeColor = Drawing.Color.Black
'            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
'                objXmlReader = New XmlNodeReader(objOutputXml)
'                ds.ReadXml(objXmlReader)
'                FooterDataset = New DataSet
'                FooterDataset = ds
'                grdvBidtCrsDetails.DataSource = ds.Tables("MIDTDETAILS")
'                grdvBidtCrsDetails.DataBind()



'                'pnlPaging.Visible = True
'            Else
'                grdvBidtCrsDetails.DataSource = Nothing
'                grdvBidtCrsDetails.DataBind()
'                '  txtRecordCount.Text = "0"
'                ' txtTotlaProductivity.Text = "0"
'                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
'                ' pnlPaging.Visible = False
'            End If

'            'Dim myCurrentItem As DataListItem
'            'Dim cur As DataGridItem
'            'Dim cur2 As GridViewRow
'            'Dim field As DataControlField
'            'Dim field2 As datacontrolro

'        Catch ex As Exception
'            lblError.Text = ex.Message.ToString
'            ' pnlPaging.Visible = False
'        Finally
'            objInputXml = Nothing
'            objOutputXml = Nothing

'        End Try
'    End Sub
'    Private Sub BIDTCrsDetailsExport()
'        Dim objInputXml, objOutputXml As New XmlDocument
'        Dim objXmlReader As XmlNodeReader
'        Dim ds As New DataSet
'        Dim objSecurityXml As New XmlDocument
'        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
'        Try
'            ' objInputXml.LoadXml(" <PR_SEARCHMIDTDETAILS_INPUT><MONTHFROM /><YEARFROM /><MONTHTO /><YEARTO /> <LCODE /> <WHOLEGROUP /><SHOWAIRBREAKUP />  <AOFFICE /> <REGION />  <AIRLINECODE />  <RESPONSIBLESTAFFID />  <SALESPERSONID />  <CARRIERTYPE />  <LIMITED_TO_REGION />   <LIMITED_TO_OWNAAGENCY />   <LIMITED_TO_AOFFICE />   </PR_SEARCHMIDTDETAILS_INPUT>")
'            objInputXml.LoadXml(" <PR_SEARCHMIDTDETAILS_INPUT><MONTHFROM /><YEARFROM /><MONTHTO /><YEARTO /> <LCODE /> <WHOLEGROUP /><SHOWAIRBREAKUP />  <AOFFICE /> <REGION />  <AIRLINECODE />  <RESPONSIBLESTAFFID />  <SALESPERSONID />  <CARRIERTYPE />  <LIMITED_TO_REGION />   <LIMITED_TO_OWNAAGENCY />   <LIMITED_TO_AOFFICE />  <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/> </PR_SEARCHMIDTDETAILS_INPUT>")

'            objInputXml.DocumentElement.SelectSingleNode("MONTHFROM").InnerText = drpMonthFrom.SelectedValue
'            objInputXml.DocumentElement.SelectSingleNode("MONTHTO").InnerText = drpMonthTo.SelectedValue
'            objInputXml.DocumentElement.SelectSingleNode("YEARFROM").InnerText = drpYearFrom.SelectedValue
'            objInputXml.DocumentElement.SelectSingleNode("YEARTO").InnerText = drpYearTo.SelectedValue

'            txtTotlaProductivity.Text = "0"

'            If Request.QueryString("Lcode") IsNot Nothing Then
'                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = objED.Decrypt(Request.QueryString("Lcode").ToString.Trim)
'            End If

'            If Request.QueryString("Aoff") IsNot Nothing Then
'                'objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = objED.Decrypt(Request.QueryString("Aoff").ToString.Trim)
'                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = (Request.QueryString("Aoff").ToString.Trim)
'            End If
'            If drpAirLine.SelectedIndex <> 0 Then
'                objInputXml.DocumentElement.SelectSingleNode("AIRLINECODE").InnerText = drpAirLine.SelectedValue
'            End If


'            If Not Session("LoginSession") Is Nothing Then
'                objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = Session("LoginSession").ToString().Split("|")(0)
'            End If
'            If ChkWholeGroup.Checked = True Then
'                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "Y"
'            Else
'                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "N"
'            End If

'            If Request.QueryString("LimAoff") IsNot Nothing Then
'                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = Request.QueryString("LimAoff").ToString
'            End If
'            If Request.QueryString("LimReg") IsNot Nothing Then
'                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = Request.QueryString("LimReg").ToString
'            End If
'            If Request.QueryString("LimOwnOff") IsNot Nothing Then
'                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = Request.QueryString("LimOwnOff").ToString
'            End If
'            If ChkAirBreak.Checked = True Then
'                objInputXml.DocumentElement.SelectSingleNode("SHOWAIRBREAKUP").InnerText = "1"
'                objInputXml.DocumentElement.SelectSingleNode("CARRIERTYPE").InnerText = RdCarryType.SelectedValue
'            Else
'                objInputXml.DocumentElement.SelectSingleNode("SHOWAIRBREAKUP").InnerText = "0"
'            End If

'            If drpAirLine.SelectedIndex <> 0 Then
'                objInputXml.DocumentElement.SelectSingleNode("SHOWAIRBREAKUP").InnerText = "1"
'                objInputXml.DocumentElement.SelectSingleNode("CARRIERTYPE").InnerText = RdCarryType.SelectedValue
'            End If



'            'Start Code for paging and sorting

'            If ViewState("SortName") Is Nothing Then
'                ViewState("SortName") = "ADDRESS"
'                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "ADDRESS" '"LOCATION_CODE"
'            Else
'                If ViewState("PrevSearching") Is Nothing Then
'                Else
'                    Dim objTempInputXml As New XmlDocument
'                    Dim objNodeList As XmlNodeList
'                    objTempInputXml.LoadXml(ViewState("PrevSearching"))
'                    objNodeList = objTempInputXml.DocumentElement.ChildNodes
'                    For Each objNode As XmlNode In objNodeList
'                        If objNode.Name = "WHOLEGROUP" Then
'                            If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
'                                If ChkWholeGroup.Checked = False Then
'                                    If ViewState("SortName") = "ADDRESS" Or ViewState("SortName") = "CITY" Or ViewState("SortName") = "COUNTRY" Then

'                                        ViewState("SortName") = "MONTHYEAR"
'                                        ViewState("Desc") = "FALSE"
'                                    End If
'                                End If
'                            End If
'                        End If
'                        If objNode.Name = "SHOWAIRBREAKUP" Then
'                            If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
'                                If ViewState("SortName") = "AIRLINENAME" Then

'                                    ViewState("SortName") = "MONTHYEAR"
'                                    ViewState("Desc") = "FALSE"
'                                End If
'                            End If
'                        End If
'                    Next
'                End If

'                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
'            End If

'            If ViewState("Desc") Is Nothing Then
'                ViewState("Desc") = "FALSE"
'                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
'            Else
'                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
'            End If

'            'End Code for paging and sorting


'            objOutputXml = objbzMIDT.SearchDetails(objInputXml)

'            'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
'            '    ViewState("PrevSearching") = objInputXml.OuterXml
'            'End If

'            grdvBidtCrsDetails.AllowSorting = False
'            grdvBidtCrsDetails.HeaderStyle.ForeColor = Drawing.Color.Black
'            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
'                objXmlReader = New XmlNodeReader(objOutputXml)
'                ds.ReadXml(objXmlReader)
'                FooterDataset = New DataSet
'                FooterDataset = ds
'                grdvBidtCrsDetails.DataSource = ds.Tables("MIDTDETAILS")
'                grdvBidtCrsDetails.DataBind()

'                '@ Code For Exporting the Data

'                Dim objOutputXmlExport As New XmlDocument
'                Dim objXmlNode, objXmlNodeClone As XmlNode
'                objOutputXmlExport.LoadXml(ds.GetXml().ToString)

'                objXmlNode = objOutputXmlExport.DocumentElement.SelectSingleNode("MIDTDETAILS")
'                objXmlNodeClone = objXmlNode.CloneNode(True)


'                'For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
'                '    XmlAttr.Value = ""
'                'Next
'                'With objXmlNodeClone
'                '    '.Attributes(8).Value = "Total"
'                '    '.Attributes("AIR_NETBOOKINGS").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("AIR_NETBOOKINGS").ToString
'                '    '.Attributes("CAR_NETBOOKINGS").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("CAR_NETBOOKINGS").ToString
'                '    '.Attributes("HOTEL_NETBOOKINGS").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("HOTEL_NETBOOKINGS").ToString
'                '    '.Attributes("INSURANCE_NETBOOKINGS").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("INSURANCE_NETBOOKINGS").ToString
'                '    '.Attributes("AVERAGE").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("AVERAGE").ToString
'                'End With

'                '  objOutputXmlExport.DocumentElement.AppendChild(objXmlNodeClone)

'                Dim objExport As New ExportExcel
'                Dim IntInvisible As Integer = 0
'                For intclmn As Integer = 0 To grdvBidtCrsDetails.HeaderRow.Cells.Count - 2
'                    If grdvBidtCrsDetails.HeaderRow.Cells(intclmn).Visible = False Then
'                        IntInvisible = IntInvisible + 1
'                    End If
'                Next
'                Dim strArray(grdvBidtCrsDetails.HeaderRow.Cells.Count - 2 - IntInvisible) As String
'                Dim intArray(grdvBidtCrsDetails.HeaderRow.Cells.Count - 2 - IntInvisible) As Integer

'                Dim intclmnVis As Integer = 0


'                For intclmn As Integer = 0 To objXmlNodeClone.Attributes.Count - 2
'                    If grdvBidtCrsDetails.HeaderRow.Cells(intclmn).Visible = True Then
'                        strArray(intclmnVis) = grdvBidtCrsDetails.Columns(intclmn).HeaderText.ToString().Replace("'", " ") 'objOutputXml.DocumentElement.SelectSingleNode("BIDTMONTHLYBREAKUP").Attributes(intclmn - 1).Name
'                        '@ Finding Position From xml Related with Header Text
'                        For kk As Integer = 0 To objXmlNodeClone.Attributes.Count - 1
'                            If objXmlNodeClone.Attributes(kk).Name.Trim = grdvBidtCrsDetails.Columns(intclmn).SortExpression.Trim Then
'                                intArray(intclmnVis) = kk
'                                intclmnVis = intclmnVis + 1
'                                Exit For
'                            End If
'                        Next kk
'                    End If
'                Next intclmn

'                objExport.ExportDetails(objOutputXmlExport, "MIDTDETAILS", intArray, strArray, ExportExcel.ExportFormat.Excel, "CRSDetails.xls")


'                '@ end of Code For Exporting the Data

'                'pnlPaging.Visible = True
'            Else
'                grdvBidtCrsDetails.DataSource = Nothing
'                grdvBidtCrsDetails.DataBind()
'                txtRecordCount.Text = "0"
'                txtTotlaProductivity.Text = "0"
'                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
'                pnlPaging.Visible = False
'            End If

'            'Dim myCurrentItem As DataListItem
'            'Dim cur As DataGridItem
'            'Dim cur2 As GridViewRow
'            'Dim field As DataControlField
'            'Dim field2 As datacontrolro

'        Catch ex As Exception
'            lblError.Text = ex.Message.ToString
'            pnlPaging.Visible = False
'        Finally
'            objInputXml = Nothing
'            objOutputXml = Nothing

'        End Try
'    End Sub
'End Class

