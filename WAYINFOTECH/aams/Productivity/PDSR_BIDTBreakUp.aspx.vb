Imports System.IO
Imports System.Text
Partial Class Productivity_PDSR_BIDTBreakUp
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim TotalAddsum As Long = 0
    Dim TotalCansum As Long = 0
    Dim TotalAutosum As Long = 0
    Dim TotalNetsum As Long = 0
    Dim TotalCarNetsum As Long = 0
    Dim TotalHotelNetsum As Long = 0
    Dim TotalNBSAddsum As Long = 0
    Dim TotalNBSCansum As Long = 0
    Dim TotalNBSAutoCreditsum As Long = 0
    Dim TotalCEsum As Double = 0

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
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            btnSearch.Attributes.Add("onclick", "return CheckValidation();")
            btnExport.Attributes.Add("onclick", "return CheckValidation();")
            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Breakup']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Breakup']").Attributes("Value").Value)
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
                If Request.QueryString("GrData") IsNot Nothing Then
                    If Request.QueryString("GrData").ToString = "1" Then
                        ChkWholeGroup.Checked = True
                    Else
                        ChkWholeGroup.Checked = False
                    End If
                End If


                If Request.QueryString("UseOrig") IsNot Nothing Then
                    If Request.QueryString("UseOrig").ToString.ToUpper = "Y" Then
                        ChkShowNBS.Checked = True
                    Else
                        ChkShowNBS.Checked = False
                    End If
                End If



                BIDTBreakupSearch()
            End If
            If Not IsPostBack Then
                AgencyView()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BIDTBreakupSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzbzBIDT As New AAMS.bizProductivity.bzBIDT
        Try
            TotalAddsum = 0
            TotalCansum = 0
            TotalAutosum = 0
            TotalNetsum = 0
            TotalCarNetsum = 0
            TotalHotelNetsum = 0
            TotalNBSAddsum = 0
            TotalNBSCansum = 0
            TotalCEsum = 0


            objInputXml.LoadXml("<PR_SEARCH_PR_1A_PRODUCTIVITY_BREAKUP_BIDT_INPUT><LCODE/><FMONTH/><TMONTH/><FYEAR/><TYEAR/><GROUPDATA/><USEORIGINAL></USEORIGINAL><RESPONSIBLESTAFFID/><LIMITED_TO_AOFFICE/><LIMITED_TO_REGION/><LIMITED_TO_OWNAAGENCY/></PR_SEARCH_PR_1A_PRODUCTIVITY_BREAKUP_BIDT_INPUT>")
            ' <PR_SEARCH_PR_1A_PRODUCTIVITY_BREAKUP_BIDT_INPUT><LCODE/>
            '<FMONTH/><TMONTH/><FYEAR/><TYEAR/><GROUPDATA/>
            '<USEORIGINAL></USEORIGINAL>
            '<RESPONSIBLESTAFFID/><LIMITED_TO_AOFFICE/><LIMITED_TO_REGION/>
            '<LIMITED_TO_OWNAAGENCY/></PR_SEARCH_PR_1A_PRODUCTIVITY_BREAKUP_BIDT_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("FMONTH").InnerText = drpMonthFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("TMONTH").InnerText = drpMonthTo.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("FYEAR").InnerText = drpYearFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("TYEAR").InnerText = drpYearTo.SelectedValue



            If Request.QueryString("Lcode") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = objED.Decrypt(Request.QueryString("Lcode").ToString.Trim)
            End If



            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If
            If ChkWholeGroup.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("GROUPDATA").InnerText = "1"
            Else
                objInputXml.DocumentElement.SelectSingleNode("GROUPDATA").InnerText = "0"
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



            If ChkShowNBS.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("USEORIGINAL").InnerText = "Y"
            Else
                objInputXml.DocumentElement.SelectSingleNode("USEORIGINAL").InnerText = "N"
            End If




            objOutputXml = objbzbzBIDT.BIDTProductivityBreakup(objInputXml)

            ' If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml.DocumentElement.SelectSingleNode("AIR"))
            ds.ReadXml(objXmlReader)

            grdvAirBreakup.DataSource = ds.Tables("DETAIL")
            grdvAirBreakup.DataBind()
            objXmlReader = New XmlNodeReader(objOutputXml.DocumentElement.SelectSingleNode("CAR"))
            ds = New DataSet
            ds.ReadXml(objXmlReader)
            grdvCarBreakup.DataSource = ds.Tables("DETAIL")
            grdvCarBreakup.DataBind()
            objXmlReader = New XmlNodeReader(objOutputXml.DocumentElement.SelectSingleNode("HOTEL"))
            ds = New DataSet
            ds.ReadXml(objXmlReader)
            grdvHotelBreakup.DataSource = ds.Tables("DETAIL")
            grdvHotelBreakup.DataBind()

            'Else
            'grdvAirBreakup.DataSource = Nothing
            'grdvAirBreakup.DataBind()
            'grdvCarBreakup.DataSource = Nothing
            'grdvCarBreakup.DataBind()
            'grdvHotelBreakup.DataSource = Nothing
            'grdvHotelBreakup.DataBind()
            'lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            'End If


        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing

        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BIDTBreakupSearch()
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

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click

        BIDTBreakupSearch()

        PrepareGridViewForExport(grdvAirBreakup)
        ExportGridView(grdvAirBreakup, "ABreakup.xls")

        'PrepareGridViewForExport(grdvCarBreakup)
        'ExportGridView(grdvCarBreakup, "ABreakupCar.xls")

        'PrepareGridViewForExport(grdvHotelBreakup)
        'ExportGridView(grdvHotelBreakup, "ABreakupHotel.xls")
    End Sub
    Private Sub ExportGridView(ByVal gv2 As GridView, ByVal FileName As String)

        Dim attachment As String = "attachment; filename=" + FileName
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/ms-excel"
        Dim sw As New StringWriter
        Dim htw As New HtmlTextWriter(sw)
        Dim frm As New HtmlForm()

        gv2.Caption = "Air Bookings Breakup"

        gv2.Parent.Controls.Add(frm)

        grdvCarBreakup.Caption = "Car Bookings Breakup"

        grdvCarBreakup.Parent.Controls.Add(frm)

        grdvHotelBreakup.Caption = "Hotel Bookings Breakup"
        grdvHotelBreakup.Parent.Controls.Add(frm)


        frm.Attributes("runat") = "server"
        frm.Controls.Add(gv2)

        frm.Controls.Add(grdvCarBreakup)
        frm.Controls.Add(grdvHotelBreakup)

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

    Protected Sub grdvAirBreakup_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvAirBreakup.RowDataBound
        Try
            If ChkWholeGroup.Checked = True Then
                e.Row.Cells(0).Visible = True
                e.Row.Cells(1).Visible = True
                e.Row.Cells(2).Visible = True
            Else
                e.Row.Cells(0).Visible = False
                e.Row.Cells(1).Visible = False
                e.Row.Cells(2).Visible = False
            End If
          


            ''''New Code
            If ChkShowNBS.Checked = True Then
                e.Row.Cells(8).Visible = True
                e.Row.Cells(9).Visible = True
                e.Row.Cells(10).Visible = True
            Else
                e.Row.Cells(8).Visible = False
                e.Row.Cells(9).Visible = False
                e.Row.Cells(10).Visible = False
            End If
            ''''end new code


            If e.Row.RowType = DataControlRowType.Footer Then
                e.Row.Cells(3).Text = "Total"
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "ADDITIONS")
                TotalAddsum += sum
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                Dim TotAdd As Label
                TotAdd = CType(e.Row.FindControl("TotAdd"), Label)
                TotAdd.Text = TotalAddsum.ToString
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "CANCEL")
                TotalCansum += sum
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                Dim TotCan As Label
                TotCan = CType(e.Row.FindControl("TotCan"), Label)
                TotCan.Text = TotalCansum.ToString
            End If

            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "AUTOCREDIT")
                TotalAutosum += sum
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                Dim TotCred As Label
                TotCred = CType(e.Row.FindControl("TotCred"), Label)
                TotCred.Text = TotalAutosum.ToString
            End If


            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "NBSADDITIONS")
                TotalNBSAddsum += sum
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                Dim TotNBSAdd As Label
                TotNBSAdd = CType(e.Row.FindControl("TotNBSAddtion"), Label)
                TotNBSAdd.Text = TotalNBSAddsum.ToString
            End If


            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "NBSCANCEL")
                TotalNBSCansum += sum
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                Dim TotNBSCanSum As Label
                TotNBSCanSum = CType(e.Row.FindControl("TotNBSCancellation"), Label)
                TotNBSCanSum.Text = TotalNBSCansum.ToString
            End If



            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "NETBOOKINGS")
                TotalNetsum += sum
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                Dim TotNet As Label
                TotNet = CType(e.Row.FindControl("TotNet"), Label)
                TotNet.Text = TotalNetsum.ToString
            End If

           


            '@ Start of new Added Code
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "NBSAUTOCREDIT")
                TotalNBSAutoCreditsum += sum
            End If

            If e.Row.RowType = DataControlRowType.Footer Then
                e.Row.Cells(10).Text = TotalNBSAutoCreditsum.ToString
            End If
            '@ End of new Added Code

            '@ Start of new Added Code added on 23rd June 2011
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim sum As Double = DataBinder.Eval(e.Row.DataItem, "CE")
                TotalCEsum += sum
            End If

            If e.Row.RowType = DataControlRowType.Footer Then
                Dim LblCE As Label
                LblCE = CType(e.Row.FindControl("LblCE"), Label)
                LblCE.Text = TotalCEsum.ToString
            End If
            '@ End of new Added Code on 23rd June 2011

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdvCarBreakup_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvCarBreakup.RowDataBound
        Try

            If ChkWholeGroup.Checked = True Then
                e.Row.Cells(0).Visible = True
                e.Row.Cells(1).Visible = True
                e.Row.Cells(2).Visible = True
            Else
                e.Row.Cells(0).Visible = False
                e.Row.Cells(1).Visible = False
                e.Row.Cells(2).Visible = False
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                e.Row.Cells(3).Text = "Total"
            End If

            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "NETBOOKINGS")
                TotalCarNetsum += sum
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                Dim TotNet As Label
                TotNet = CType(e.Row.FindControl("TotNet"), Label)
                TotNet.Text = TotalCarNetsum.ToString
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdvHotelBreakup_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvHotelBreakup.RowDataBound
        Try
            If ChkWholeGroup.Checked = True Then
                e.Row.Cells(0).Visible = True
                e.Row.Cells(1).Visible = True
                e.Row.Cells(2).Visible = True
            Else
                e.Row.Cells(0).Visible = False
                e.Row.Cells(1).Visible = False
                e.Row.Cells(2).Visible = False
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                e.Row.Cells(3).Text = "Total"
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "NETBOOKINGS")
                TotalHotelNetsum += sum
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                Dim TotNet As Label
                TotNet = CType(e.Row.FindControl("TotNet"), Label)
                TotNet.Text = TotalHotelNetsum.ToString
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ChkWholeGroup_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkWholeGroup.CheckedChanged
        Try
           ' BIDTBreakupSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
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

   
    Protected Sub grdvAirBreakup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvAirBreakup.SelectedIndexChanged

    End Sub
End Class
