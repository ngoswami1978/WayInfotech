Imports System.IO
Imports System.Text
Partial Class TravelAgency_TASR_GroupCaseChangeView
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
            If Request.QueryString("Lcode") IsNot Nothing Then
                'hdLCode.Value = Request.QueryString("Lcode").ToString
            End If

            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Group Case']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Group Case']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                    End If
                    If strBuilder(4) = "0" Then
                        btnExport.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            If Not IsPostBack Then
                If Request.QueryString("Lcode") IsNot Nothing Then
                    ShowData(Request.QueryString("Lcode"), "ShowDetails")
                End If

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
   
    Protected Sub btnShowSummary_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShowSummary.Click
        Try
            If Request.QueryString("Lcode") IsNot Nothing Then
                If btnShowSummary.Text.Trim.ToUpper = "HIDE SUMMARY" Then
                    ShowData(Request.QueryString("Lcode"), "ShowDetails")
                    btnShowSummary.Text = "Show Summary"
                Else
                    'ShowData(Request.QueryString("Lcode"), "SummaryDetails")
                    btnShowSummary.Text = "Hide Summary"
                    PnlDetails.Visible = False
                    PnlSummary.Visible = True
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub ShowData(ByVal Lcode As String, ByVal DetailsType As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzbzBIDT As New AAMS.bizProductivity.bzBIDT
        Try
            If DetailsType = "ShowDetails" Then

                If Request.QueryString("Lcode") IsNot Nothing Then
                    objInputXml.LoadXml("<UP_GROUPCASE_INPUT><LCODE/><IYEAR/><USEORIGINAL/><SUMMERYTYPE>3</SUMMERYTYPE><LIMITED_TO_AOFFICE/> <LIMITED_TO_REGION/> <LIMITED_TO_OWNAAGENCY/></UP_GROUPCASE_INPUT> ")
                    objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerXml = Lcode 'Request.QueryString("Lcode")
                    If Request.QueryString("Year") IsNot Nothing Then
                        objInputXml.DocumentElement.SelectSingleNode("IYEAR").InnerXml = Request.QueryString("Year")
                    End If
                    If Request.QueryString("USEORIGINAL") IsNot Nothing Then
                        objInputXml.DocumentElement.SelectSingleNode("USEORIGINAL").InnerText = Request.QueryString("USEORIGINAL")
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

                    '    objOutputXml.LoadXml("<UP_GROUPCASEDETAILS_OUTPUT><GROUPDETAILS AGENCYNAME='' ADDRESS='' ONLINE='' ONLINEDATE='' OFFICEID='' AGENCYPPC='' APC='' TKTPRINTER='' PRINTER='' MINREQ='' APREV='' BPREV='' GPREV='' PPREV='' WPREV='' POTENCURR='' ACURR='' BCURR='' GCURR='' PCURR='' WCURR='' POTENPCURR='' AVGCURR='' AVGPREV='' EFFICIENCY='' DEFICIT='' DEFICITPER=''>   </GROUPDETAILS>   <GROUPDETAILS AGENCYNAME='' ADDRESS='' ONLINE='' ONLINEDATE='' OFFICEID='' AGENCYPPC='' APC='' TKTPRINTER='' PRINTER='' MINREQ='' APREV='' BPREV='' GPREV='' PPREV='' WPREV='' POTENCURR='' ACURR='' BCURR='' GCURR='' PCURR='' WCURR='' POTENPCURR='' AVGCURR='' AVGPREV='' EFFICIENCY='' DEFICIT='' DEFICITPER=''>   </GROUPDETAILS>   <Errors Status='FALSE'> <Error Code='' Description=''/></Errors>   </UP_GROUPCASEDETAILS_OUTPUT>")
                    ' objOutputXml.LoadXml("<UP_GROUPCASEDETAILS_OUTPUT><GROUPDETAILS AGENCYNAME='' ADDRESS='' ONLINE='' ONLINEDATE=''  OFFICEID='' AGENCYPPC=''  APC='' TKTPRINTER='' PRINTER='' MINREQ='' APREV='' BPREV='' GPREV='' PPREV='' WPREV=''  POTENPCURR='' ACURR='' BCURR='' GCURR='' PCURR='' WCURR=''  POTENCURR='' JAN_PROD='' FEB_PROD='' MAR_PROD='' APR_PROD='' MAY_PROD='' JUN_PROD='' JUL_PROD='' AUG_PROD='' SEP_PROD='' OCT_PROD='' NOV_PROD='' DEC_PROD=''   AVGCURR='' AVGPREV='' EFFICIENCY='' DEFICIT='' DEFICITPER=''> </GROUPDETAILS> <Errors Status='FALSE'><Error Code='' Description=''/> </Errors> </UP_GROUPCASEDETAILS_OUTPUT>")
                    objOutputXml = objbzbzBIDT.GroupCaseProductivity(objInputXml)

                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        objXmlReader = New XmlNodeReader(objOutputXml)
                        ds.ReadXml(objXmlReader)
                        GvDetails.DataSource = ds.Tables("GROUPDETAILS")
                        GvDetails.DataBind()

                        Dim intRow, IntColno As Integer
                        GvDetails.FooterRow.Cells(4).Text = "Total"

                        For IntColno = 5 To GvDetails.Rows(0).Cells.Count - 1
                            GvDetails.HeaderRow.Cells(IntColno).HorizontalAlign = HorizontalAlign.Right
                            GvDetails.FooterRow.Cells(IntColno).HorizontalAlign = HorizontalAlign.Right

                            GvDetails.FooterRow.Cells(IntColno).Text = 0
                        Next
                        For intRow = 0 To GvDetails.Rows.Count - 1
                            For IntColno = 5 To GvDetails.Rows(0).Cells.Count - 1
                                GvDetails.Rows(intRow).Cells(IntColno).HorizontalAlign = HorizontalAlign.Right
                                Dim sum As Double
                                If GvDetails.Rows(intRow).Cells(IntColno).Text.Trim.Length > 0 Then
                                    If GvDetails.Rows(intRow).Cells(IntColno).Text.Trim <> "&nbsp;" Then
                                        sum = GvDetails.Rows(intRow).Cells(IntColno).Text
                                        GvDetails.FooterRow.Cells(IntColno).Text = CType(GvDetails.FooterRow.Cells(IntColno).Text, Decimal) + sum
                                    End If
                                End If
                                GvDetails.FooterRow.Cells(IntColno).Text = String.Format("{0:d}", GvDetails.FooterRow.Cells(IntColno).Text.ToString)
                            Next
                        Next

                        txtRecordCount.Text = ds.Tables("GROUPDETAILS").Rows.Count.ToString

                        ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                        '@ Now Making SummaryDetails
                        '  Dim objOutputSummaryXml As New XmlDocument
                        'objOutputSummaryXml.LoadXml("<UP_GROUPSUMMARY_OUTPUT><SUMMARY ColumnName='' Data='' ></UP_GROUPSUMMARY_OUTPUT")
                        Dim SummaryDataTable As New DataTable
                        SummaryDataTable.Columns.Add("Name")
                        SummaryDataTable.Columns.Add("Data")

                        Dim ArrMonDispList As New ArrayList(Request.QueryString("MonDisp").ToString.Split("|"))
                        For IntColno = 10 To GvDetails.Rows(0).Cells.Count - 1
                            Dim dr As DataRow
                            dr = SummaryDataTable.NewRow
                            dr(0) = "Total" + " " + GvDetails.HeaderRow.Cells(IntColno).Text
                            dr(1) = GvDetails.FooterRow.Cells(IntColno).Text
                            SummaryDataTable.Rows.Add(dr)
                            If Request.QueryString("MonDisp") IsNot Nothing Then
                                If IntColno >= 22 And IntColno <= 33 Then
                                    dr(0) = "Total 1A Productivity for " + " " + GvDetails.HeaderRow.Cells(IntColno).Text
                                    If Not ArrMonDispList.Contains((IntColno - 22 + 1).ToString) Then
                                        SummaryDataTable.Rows.Remove(dr)
                                    End If

                                End If
                            End If
                            ' If GvDetails.HeaderRow.Cells(IntColno).Visible = True Then



                            'SummaryDataTable.Rows.Add(dr)
                            'End If
                        Next


                       


                        GvSummary.DataSource = SummaryDataTable
                        GvSummary.DataBind()
                        ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@


                    Else
                        GvDetails.DataSource = Nothing
                        GvDetails.DataBind()
                        GvSummary.DataSource = Nothing
                        GvSummary.DataBind()
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
                PnlDetails.Visible = True
                PnlSummary.Visible = False
                ' btnShowSummary.Text = "Show Summary"

            ElseIf DetailsType = "SummaryDetails" Then

                If Request.QueryString("Lcode") IsNot Nothing Then
                    ''objInputXml.LoadXml("<PR_SEARCH_AGENCY_GROUPCASE_INPUT><FYEAR/><LCODE/><USEORIGINAL/></PR_SEARCH_AGENCY_GROUPCASE_INPUT>")
                    'objInputXml.LoadXml("<PR_SEARCH_AGENCY_GROUPCASE_INPUT><FYEAR/><LCODE/></PR_SEARCH_AGENCY_GROUPCASE_INPUT>")
                    'objInputXml.DocumentElement.SelectSingleNode("FYEAR").InnerText = Request.QueryString("Year")
                    'objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = Request.QueryString("Lcode")

                    '' objOutputXml = objbzbzBIDT.BIDTProductivityDetail(objInputXml)

                    'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    '    objXmlReader = New XmlNodeReader(objOutputXml)
                    '    ds.ReadXml(objXmlReader)
                    '    GvSummary.DataSource = ds.Tables("SUMMARY")
                    '    GvSummary.DataBind()
                    'Else
                    '    GvSummary.DataSource = Nothing
                    '    GvSummary.DataBind()
                    '    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    'End If
                End If
                PnlDetails.Visible = False
                PnlSummary.Visible = True
                'btnShowSummary.Text = "Show Summary"
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If Request.QueryString("Lcode") IsNot Nothing Then
            ShowData(Request.QueryString("Lcode"), "ShowDetails")
        End If
        If GvDetails.Rows.Count > 0 Then
            PrepareGridViewForExport(GvDetails)
            ExportGridView(GvDetails, "GroupCase.xls")
        End If

    End Sub

    Private Sub ExportGridView(ByVal gv2, ByVal FileName)
        GvDetails.Caption = "<b>Group Case</b>"
        GvDetails.CaptionAlign = TableCaptionAlign.Left
        Dim attachment As String = "attachment; filename=" + FileName
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/ms-excel"
        Dim sw As New StringWriter
        Dim pp As String
        Dim htw As New HtmlTextWriter(sw)
        Dim frm As New HtmlForm()
        gv2.Parent.Controls.Add(frm)


        GvSummary.Caption = "<b>Summary Report</b>"
        GvSummary.Parent.Controls.Add(frm)



        frm.Attributes("runat") = "server"
        frm.Controls.Add(gv2)
        frm.Controls.Add(GvSummary)

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

    Protected Sub GvDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvDetails.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                If Request.QueryString("Year") IsNot Nothing Then
                    Dim CurrYear As Integer = CInt(Request.QueryString("Year"))
                    Dim PrevYear As Integer = CurrYear - 1
                    e.Row.Cells(10).Text = "1A-" + PrevYear.ToString
                    e.Row.Cells(11).Text = "1B-" + PrevYear.ToString
                    e.Row.Cells(12).Text = "1G-" + PrevYear.ToString
                    e.Row.Cells(13).Text = "1P-" + PrevYear.ToString
                    e.Row.Cells(14).Text = "1W-" + PrevYear.ToString
                    e.Row.Cells(15).Text = "Potential-" + PrevYear.ToString
                    e.Row.Cells(16).Text = "1A-" + CurrYear.ToString
                    e.Row.Cells(17).Text = "1B-" + CurrYear.ToString
                    e.Row.Cells(18).Text = "1G-" + CurrYear.ToString
                    e.Row.Cells(19).Text = "1P-" + CurrYear.ToString
                    e.Row.Cells(20).Text = "1W-" + CurrYear.ToString
                    e.Row.Cells(21).Text = "Potential-" + CurrYear.ToString

                    e.Row.Cells(22).Text = "JAN-" + CurrYear.ToString
                    e.Row.Cells(23).Text = "FEB-" + CurrYear.ToString
                    e.Row.Cells(24).Text = "MAR-" + CurrYear.ToString
                    e.Row.Cells(25).Text = "APR-" + CurrYear.ToString
                    e.Row.Cells(26).Text = "MAY-" + CurrYear.ToString
                    e.Row.Cells(27).Text = "JUNE-" + CurrYear.ToString
                    e.Row.Cells(28).Text = "JULY-" + CurrYear.ToString
                    e.Row.Cells(29).Text = "AUG-" + CurrYear.ToString
                    e.Row.Cells(30).Text = "SEP-" + CurrYear.ToString
                    e.Row.Cells(31).Text = "OCT-" + CurrYear.ToString
                    e.Row.Cells(32).Text = "NOV-" + CurrYear.ToString
                    e.Row.Cells(33).Text = "DEC-" + CurrYear.ToString

                    e.Row.Cells(34).Text = "1A Avg-" + CurrYear.ToString
                    e.Row.Cells(35).Text = "1A Avg-" + PrevYear.ToString
                   
                End If
               
            End If
            ' If e.Row.RowIndex < 0 Then
            '  Exit Sub
            ' End If
            Dim k As Integer
            If Request.QueryString("MonDisp") IsNot Nothing Then

                Dim ArrMonDispList As New ArrayList(Request.QueryString("MonDisp").ToString.Split("|"))
                For k = 22 To 33
                    If ArrMonDispList.Contains((k - 22 + 1).ToString) Then
                        e.Row.Cells(k).Visible = True
                    Else
                        e.Row.Cells(k).Visible = False
                    End If

                Next

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
