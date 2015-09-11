
Partial Class Incentive_INCRPT_VolumeBased
    Inherits System.Web.UI.Page
    Dim dsFooter As DataSet
    Dim objeAAMS As New eAAMS
#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblError.Text = ""
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                lblError.Text = "Session is expired."
                Exit Sub
            End If
            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='VolumeClassification']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='VolumeClassification']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                    If strBuilder(0) = "0" Then
                        btnDisplayReport.Enabled = False
                    End If
                    If strBuilder(4) = "0" Then
                        BtnExport.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If


            If Not Page.IsPostBack Then
                BindDropDowns()
                Dim dtMonth As String = Month(DateTime.Now)
                Dim dtYear As String = Year(DateTime.Now)
                drpMonths.SelectedIndex = Convert.ToInt16(dtMonth) - 1
                drpYears.Text = dtYear
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnDisplayReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisplayReport.Click"
    Protected Sub btnDisplayReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisplayReport.Click
        Try
            VolumeBasedReport()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
#End Region

#Region "BindDropDowns()"
    Private Sub BindDropDowns()
        Try
            Dim I As Integer
           
            For I = 1 To 12
                drpMonths.Items.Add(New ListItem(MonthName(I)))
            Next


            Dim IntYearFrom As Integer = 3
            Dim IntYearTo As Integer = 3
            'If System.Configuration.ConfigurationManager.AppSettings("YearFrom") IsNot Nothing Then
            '    IntYearFrom = Val(System.Configuration.ConfigurationManager.AppSettings("YearFrom"))
            'End If
            'If System.Configuration.ConfigurationManager.AppSettings("YearTo") IsNot Nothing Then
            '    IntYearTo = Val(System.Configuration.ConfigurationManager.AppSettings("YearTo"))
            'End If


            For I = DateTime.Now.Year + IntYearFrom To DateTime.Now.Year - IntYearTo Step -1
                drpYears.Items.Add(I)
            Next

            drpYears.SelectedValue = DateTime.Now.Year

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region " VolumeBasedReport()"
    Private Sub VolumeBasedReport()
        Dim objInputXml As New XmlDocument
        Dim objOutPutxml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        Dim objbizIncetive As New AAMS.bizIncetive.bzAgencyClassification
        Dim ds As New DataSet
        Dim objReadaer As XmlNodeReader
        Try
            ' objInputXml.LoadXml("<RP_VOLUMECLASSIFICATION_INPUT><Month></Month><Year></Year></RP_VOLUMECLASSIFICATION_INPUT>")

            objInputXml.LoadXml("<RP_VOLUMECLASSIFICATION_INPUT><Month></Month><Year></Year><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></RP_VOLUMECLASSIFICATION_INPUT>")


            ' If (drpMonths.SelectedIndex <> 0) Then
            objInputXml.DocumentElement.SelectSingleNode("Month").InnerText = Format(Val(drpMonths.SelectedIndex + 1), "00")
            ' End If
            ' If (drpYears.SelectedIndex <> 0) Then
            objInputXml.DocumentElement.SelectSingleNode("Year").InnerText = Val(drpYears.SelectedValue)



            ' @ Code For Setting Input Xml 
            If ViewState("PrevSearching") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
            Else
                Dim objTempInputXml As New XmlDocument
                Dim objNodeList As XmlNodeList

                objTempInputXml.LoadXml(ViewState("PrevSearching"))
                objNodeList = objTempInputXml.DocumentElement.ChildNodes
                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                For Each objNode1 As XmlNode In objNodeList
                    If objNode1.Name <> "PAGE_NO" And objNode1.Name <> "SORT_BY" And objNode1.Name <> "DESC" And objNode1.Name <> "PAGE_SIZE" Then
                        If objNode1.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode1.Name.ToString).InnerText Then
                            objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                            ddlPageNumber.SelectedValue = "1"
                        End If
                    End If
                Next
            End If
            objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "Type"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Type"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            objOutPutxml = objbizIncetive.rpt_AgencyVolumeClassification(objInputXml)



            ' objxmlout.Save("C:\Inetpub\wwwroot\STYLUS\AAMS\RP_VOLUMECLASSIFICATION_OUTPUT1.xml")
            If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then



                objReadaer = New XmlNodeReader(objOutPutxml)
                ds.ReadXml(objReadaer)
                dsFooter = New DataSet
                dsFooter = ds

                GvIncVol.DataSource = ds.Tables("VOLUMECLASSIFICATION")
                GvIncVol.DataBind()


                '@ Code Added For Paging And Sorting 
                pnlPaging.Visible = True
                BindControlsForNavigation(objOutPutxml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)


                txtTotalRecordCount.Text = objOutPutxml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                SetImageForSorting(GvIncVol)

                '@ End of Code Added For Paging And Sorting 
            Else
                GvIncVol.DataSource = Nothing
                GvIncVol.DataBind()
                lblError.Text = objOutPutxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

                txtTotalRecordCount.Text = "0"

                pnlPaging.Visible = False
            End If

            ' objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = objEaams.EmployeeID(Session("Security"))
            'End If
            'Here Back end Method Call
            ' Session("VolumeBasedReport") = objInputXml.OuterXml
            ' Response.Redirect("../RPSR_ReportShow.aspx?Case=VolumeBasedReport", False)
        Catch ex As Exception
            Throw ex
        Finally
            objInputXml = Nothing
        End Try
    End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("INCRPT_VolumeBased.aspx")
    End Sub
#End Region

    Protected Sub GvIncVol_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvIncVol.RowCreated
        Try
            Dim grvRow As GridViewRow
            grvRow = e.Row
            If e.Row.RowType = DataControlRowType.Header Then
                Dim strMonthYear As String = drpMonths.SelectedValue.ToString.Substring(0, 3) + " " + drpYears.SelectedValue
                If GvIncVol.AllowSorting = True Then
                    CType(grvRow.Cells(1).Controls(0), LinkButton).Text = "1A " + strMonthYear
                    CType(grvRow.Cells(2).Controls(0), LinkButton).Text = "1B " + strMonthYear
                    CType(grvRow.Cells(3).Controls(0), LinkButton).Text = "1G " + strMonthYear
                    CType(grvRow.Cells(4).Controls(0), LinkButton).Text = "1P " + strMonthYear
                    CType(grvRow.Cells(5).Controls(0), LinkButton).Text = "1W " + strMonthYear

                Else
                    e.Row.Cells(1).Text = "1A " + strMonthYear
                    e.Row.Cells(2).Text = "1B " + strMonthYear
                    e.Row.Cells(3).Text = "1G " + strMonthYear
                    e.Row.Cells(4).Text = "1P " + strMonthYear
                    e.Row.Cells(5).Text = "1W " + strMonthYear

                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

 

    Protected Sub GvIncVol_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvIncVol.RowDataBound
        Try
            '<PAGE_TOTAL A="1787584" B="552123" G="1253408" 
            'P="50422" W="5366" Total="3648903" Per="0" No_of_Locations="11188"
            'MNC="1319" Sole="4821" Multi="1426" Non1A="4941" /> 

            If e.Row.RowType = DataControlRowType.Footer Then
                If dsFooter IsNot Nothing Then
                    e.Row.Cells(0).Text = "Total"
                    e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Left
                    e.Row.Cells(1).Text = dsFooter.Tables("PAGE_TOTAL").Rows(0)("A").ToString()
                    e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
                    e.Row.Cells(2).Text = dsFooter.Tables("PAGE_TOTAL").Rows(0)("B").ToString()
                    e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
                    e.Row.Cells(3).Text = dsFooter.Tables("PAGE_TOTAL").Rows(0)("G").ToString()
                    e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
                    e.Row.Cells(4).Text = dsFooter.Tables("PAGE_TOTAL").Rows(0)("P").ToString()
                    e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
                    e.Row.Cells(5).Text = dsFooter.Tables("PAGE_TOTAL").Rows(0)("W").ToString()
                    e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
                    e.Row.Cells(6).Text = dsFooter.Tables("PAGE_TOTAL").Rows(0)("Total").ToString()
                    e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Right
                    e.Row.Cells(7).Text = dsFooter.Tables("PAGE_TOTAL").Rows(0)("Per").ToString()
                    e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Right
                    e.Row.Cells(8).Text = dsFooter.Tables("PAGE_TOTAL").Rows(0)("No_of_Locations").ToString()
                    e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Right
                    e.Row.Cells(9).Text = dsFooter.Tables("PAGE_TOTAL").Rows(0)("MNC").ToString()
                    e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Right
                    e.Row.Cells(10).Text = dsFooter.Tables("PAGE_TOTAL").Rows(0)("Sole").ToString()
                    e.Row.Cells(10).HorizontalAlign = HorizontalAlign.Right
                    e.Row.Cells(11).Text = dsFooter.Tables("PAGE_TOTAL").Rows(0)("Multi").ToString()
                    e.Row.Cells(11).HorizontalAlign = HorizontalAlign.Right
                    e.Row.Cells(12).Text = dsFooter.Tables("PAGE_TOTAL").Rows(0)("Non1A").ToString()
                    e.Row.Cells(12).HorizontalAlign = HorizontalAlign.Right
                End If
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Right

                e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(10).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(11).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(12).HorizontalAlign = HorizontalAlign.Right
            End If

        Catch ex As Exception

        End Try
    End Sub

#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If

            VolumeBasedReport()


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            VolumeBasedReport()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            VolumeBasedReport()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub GvIncVol_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvIncVol.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub GvIncVol_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GvIncVol.Sorting
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

            VolumeBasedReport()
            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
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
#End Region

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

#Region "Code for Export "
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExport.Click

        Dim objInputXml As New XmlDocument
        Dim objOutPutxml As New XmlDocument
        Dim ds As New DataSet
        '  Dim objReadaer As XmlNodeReader
        Dim objbizIncetive As New AAMS.bizIncetive.bzAgencyClassification
        Dim strMonthYear As String = drpMonths.SelectedValue.ToString.Substring(0, 3) + " " + drpYears.SelectedValue
        Try

            '  objInputXml.LoadXml("<ISP_SEARCH_INPUT><CHAIN_CODE></CHAIN_CODE><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><EmployeeID/></ISP_SEARCH_INPUT>")

            objInputXml.LoadXml("<RP_VOLUMECLASSIFICATION_INPUT><Month></Month><Year></Year><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></RP_VOLUMECLASSIFICATION_INPUT>")
            ' If (drpMonths.SelectedIndex <> 0) Then
            objInputXml.DocumentElement.SelectSingleNode("Month").InnerText = Format(Val(drpMonths.SelectedIndex + 1), "00")
            ' End If
            ' If (drpYears.SelectedIndex <> 0) Then
            objInputXml.DocumentElement.SelectSingleNode("Year").InnerText = Val(drpYears.SelectedValue)

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "NAME"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "NAME"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            ' @ Code For Setting Input Xml 


            objOutPutxml = objbizIncetive.rpt_AgencyVolumeClassification(objInputXml)
            If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                'objReadaer = New XmlNodeReader(objOutPutxml)
                'ds.ReadXml(objReadaer)
                'GvBAgencyMIDT.DataSource = ds.Tables("BGROUPAGENCYMIDT")
                'GvBAgencyMIDT.DataBind()


                ' Dim objOutputXmlExport As New XmlDocument
                Dim objXmlNode, objXmlNodeClone As XmlNode
                objXmlNode = objOutPutxml.DocumentElement.SelectSingleNode("VOLUMECLASSIFICATION")
                objXmlNodeClone = objXmlNode.CloneNode(True)
                For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                    XmlAttr.Value = ""
                Next

                '<PAGE_TOTAL A="1787584" B="552123" G="1253408" 
                'P="50422" W="5366" Total="3648903" Per="0" No_of_Locations="11188"
                'MNC="1319" Sole="4821" Multi="1426" Non1A="4941" /> 

                objXmlNodeClone.Attributes("Type").Value = " Total : "
                objXmlNodeClone.Attributes("A").Value = objOutPutxml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("A").Value
                objXmlNodeClone.Attributes("B").Value = objOutPutxml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("B").Value
                objXmlNodeClone.Attributes("G").Value = objOutPutxml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("G").Value
                objXmlNodeClone.Attributes("P").Value = objOutPutxml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("P").Value
                objXmlNodeClone.Attributes("W").Value = objOutPutxml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("W").Value
                objXmlNodeClone.Attributes("Total").Value = objOutPutxml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("Total").Value
                objXmlNodeClone.Attributes("Per").Value = objOutPutxml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("Per").Value
                objXmlNodeClone.Attributes("No_of_Locations").Value = objOutPutxml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("No_of_Locations").Value
                objXmlNodeClone.Attributes("MNC").Value = objOutPutxml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("MNC").Value
                objXmlNodeClone.Attributes("Sole").Value = objOutPutxml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("Sole").Value
                objXmlNodeClone.Attributes("Multi").Value = objOutPutxml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("Multi").Value
                objXmlNodeClone.Attributes("Non1A").Value = objOutPutxml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("Non1A").Value

                objOutPutxml.DocumentElement.AppendChild(objXmlNodeClone)


                Dim objExport As New ExportExcel
                Dim strArray() As String = {"Type", "1A" + strMonthYear, "1B " + strMonthYear, "1G " + strMonthYear, "1P" + strMonthYear, "1W " + strMonthYear, "Total", "Percentage (%)", "No. Of Location", "MNC", "Sole", "Multi", "Non1A"}
                Dim intArray() As Integer = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12}




                objExport.ExportDetails(objOutPutxml, "VOLUMECLASSIFICATION", intArray, strArray, ExportExcel.ExportFormat.Excel, "IncVol.xls")

            Else
                GvIncVol.DataSource = Nothing
                GvIncVol.DataBind()
                lblError.Text = objOutPutxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                txtTotalRecordCount.Text = "0"
                pnlPaging.Visible = False
            End If
        Catch ex As Exception
            pnlPaging.Visible = False
            lblError.Text = ex.Message
        End Try

    End Sub
#End Region
End Class
