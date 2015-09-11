
Partial Class Incentive_INCSR_Pay_PaymentReceived
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim strBuilder As New StringBuilder

    Dim dsFooter As DataSet

#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblError.Text = ""
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl


            BtnSearch.Attributes.Add("onClick", "return ValidateForm();")
            BtnExport.Attributes.Add("onClick", "return ValidateForm();")
            CheckSecurity()

            If Not Page.IsPostBack Then
                BindDropDowns()
                Dim dtMonth As String = Month(DateTime.Now)
                Dim dtYear As String = Year(DateTime.Now)
                ' drpMonths.SelectedIndex = Convert.ToInt16(dtMonth) - 1
                'drpYears.Text = dtYear
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region


#Region "BindDropDowns()"
    Private Sub BindDropDowns()
        Try
            Dim I As Integer
            For I = 1 To 12
                drpMonths.Items.Add(New ListItem(MonthName(I), I.ToString))
            Next
            drpMonths.Items.Insert(0, New ListItem("--All--", ""))


            Dim IntYearFrom As Integer = 3
            Dim IntYearTo As Integer = 3
            'If System.Configuration.ConfigurationManager.AppSettings("YearFrom") IsNot Nothing Then
            '    IntYearFrom = Val(System.Configuration.ConfigurationManager.AppSettings("YearFrom"))
            'End If
            'If System.Configuration.ConfigurationManager.AppSettings("YearTo") IsNot Nothing Then
            '    IntYearTo = Val(System.Configuration.ConfigurationManager.AppSettings("YearTo"))
            'End If


            For I = DateTime.Now.Year + IntYearFrom To DateTime.Now.Year - IntYearTo Step -1
                drpYears.Items.Add(New ListItem(I.ToString, I.ToString))
            Next
            drpYears.Items.Insert(0, New ListItem("--All--", ""))

            '  drpYears.SelectedValue = DateTime.Now.Year

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect(Request.Url.ToString)
    End Sub
#End Region
   

#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If

            SearchPaymentReceived()


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            SearchPaymentReceived()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            SearchPaymentReceived()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub GvIncPayReceived_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvIncPayReceived.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub GvIncPayReceived_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GvIncPayReceived.Sorting
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

            SearchPaymentReceived()
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
        Dim objReadaer As XmlNodeReader
        Dim objbizIncetive As New AAMS.bizIncetive.bzPaymentReceived

        Try



            objInputXml.LoadXml("<INC_SEARCH_PAYMENT_RECEIVED_INPUT> <Month></Month> <Year></Year> <Pending></Pending> <EmployeeID></EmployeeID><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INC_SEARCH_PAYMENT_RECEIVED_INPUT>")
            If (drpMonths.SelectedValue <> "") Then
                objInputXml.DocumentElement.SelectSingleNode("Month").InnerText = Format(Val(drpMonths.SelectedValue), "00")
            End If
            ' If (drpYears.SelectedIndex <> 0) Then
            If drpYears.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("Year").InnerText = drpYears.SelectedValue

            End If

            If ChkPenChequeCreation.Checked Then
                objInputXml.DocumentElement.SelectSingleNode("Pending").InnerText = "True"
            Else
                objInputXml.DocumentElement.SelectSingleNode("Pending").InnerText = "False"
            End If


            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "CHAIN_CODE"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "CHAIN_CODE"
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
            '<PAYMENT_RECEIVED 
            'PAYMENT_ID="3" CHAIN_CODE="413"
            '            CHAIN_NAME = "AKBAR TRAVELS OF INDIA"
            'AOFFICE="BOM" ACCOUNTSMANAGER=""
            ' CHQ_NO="" CHQ_AMOUNT="" CHQ_DELIVERED_TO=""
            ' CHQ_DELIVERED_NAME="" BC_ID="2" /> 



            objOutPutxml = objbizIncetive.Search(objInputXml)
            If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then


                Dim objExport As New ExportExcel
                Dim strArray() As String = {"BCaseID", "Payment Id", "Chain Code", "Group Name", "Aoffice ", "Account Manager ", "Cheque No.", "Cheque Amount ", "Cheque Delivered To"}
                Dim intArray() As Integer = {9, 0, 1, 2, 3, 4, 5, 6, 8}

                objExport.ExportDetails(objOutPutxml, "PAYMENT_RECEIVED", intArray, strArray, ExportExcel.ExportFormat.Excel, "IncPayReceived.xls")

            Else
                GvIncPayReceived.DataSource = Nothing
                GvIncPayReceived.DataBind()
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



#Region " SearchPaymentReceived()"
    Private Sub SearchPaymentReceived()
        Dim objInputXml As New XmlDocument
        Dim objOutPutxml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        Dim objbzPaymentReceived As New AAMS.bizIncetive.bzPaymentReceived
        Dim ds As New DataSet
        Dim objReadaer As XmlNodeReader
        Try
            objInputXml.LoadXml("<INC_SEARCH_PAYMENT_RECEIVED_INPUT> <Month></Month> <Year></Year><Pending></Pending> <EmployeeID></EmployeeID><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INC_SEARCH_PAYMENT_RECEIVED_INPUT>")


            If (drpMonths.SelectedValue <> "") Then
                objInputXml.DocumentElement.SelectSingleNode("Month").InnerText = Format(Val(drpMonths.SelectedValue), "00")
            End If
            ' If (drpYears.SelectedIndex <> 0) Then
            If drpYears.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("Year").InnerText = drpYears.SelectedValue

            End If

            If ChkPenChequeCreation.Checked Then
                objInputXml.DocumentElement.SelectSingleNode("Pending").InnerText = "True"
            Else
                objInputXml.DocumentElement.SelectSingleNode("Pending").InnerText = "False"
            End If

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
                ViewState("SortName") = "CHAIN_CODE"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "CHAIN_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            objOutPutxml = objbzPaymentReceived.Search(objInputXml)



            ' objxmlout.Save("C:\Inetpub\wwwroot\STYLUS\AAMS\RP_VOLUMECLASSIFICATION_OUTPUT1.xml")
            If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then



                objReadaer = New XmlNodeReader(objOutPutxml)
                ds.ReadXml(objReadaer)
                dsFooter = New DataSet
                dsFooter = ds

                GvIncPayReceived.DataSource = ds.Tables("PAYMENT_RECEIVED")
                GvIncPayReceived.DataBind()


                '@ Code Added For Paging And Sorting 
                pnlPaging.Visible = True
                BindControlsForNavigation(objOutPutxml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)


                txtTotalRecordCount.Text = objOutPutxml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                SetImageForSorting(GvIncPayReceived)

                '@ End of Code Added For Paging And Sorting 
            Else
                GvIncPayReceived.DataSource = Nothing
                GvIncPayReceived.DataBind()
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

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        Try
            SearchPaymentReceived()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub GvIncPayReceived_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvIncPayReceived.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim hdPayID As HiddenField
            hdPayID = e.Row.FindControl("hdPayID")

            Dim hdChainCode As HiddenField
            hdChainCode = e.Row.FindControl("hdChainCode")

            Dim strEncriptedChainCode = objED.Encrypt(hdChainCode.Value.Trim)


            Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            linkEdit = e.Row.FindControl("linkEdit")


            '@ Start of code  Added on 22 Feb 2011
            Dim StrPaymentPeriod As String = ""
            Dim HDPLBCYCLE As HiddenField
            Dim HDPLBPERIODFROM As HiddenField
            Dim HDPLBPERIODTO As HiddenField
            Dim HDMONTH As HiddenField
            Dim HDYEAR As HiddenField
            HDPLBCYCLE = CType(e.Row.FindControl("HDPLBCYCLE"), HiddenField)
            If HDPLBCYCLE.Value.Trim.ToUpper = "TRUE" Then
                HDPLBCYCLE.Value = "1"
            Else
                HDPLBCYCLE.Value = "0"
            End If
            HDMONTH = CType(e.Row.FindControl("HDMONTH"), HiddenField)
            HDYEAR = CType(e.Row.FindControl("HDYEAR"), HiddenField)
            HDPLBPERIODFROM = CType(e.Row.FindControl("HDPLBPERIODFROM"), HiddenField)
            HDPLBPERIODFROM.Value = objeAAMS.ConvertDate(HDPLBPERIODFROM.Value).ToString("dd/MM/yyyy")
            HDPLBPERIODTO = CType(e.Row.FindControl("HDPLBPERIODTO"), HiddenField)
            HDPLBPERIODTO.Value = objeAAMS.ConvertDate(HDPLBPERIODTO.Value).ToString("dd/MM/yyyy")
            StrPaymentPeriod = HDPLBPERIODFROM.Value.Trim + "-" + HDPLBPERIODTO.Value.Trim
            '@ End of code Added on 22 Feb 2011


            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Incentive Payment Received']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Incentive Payment Received']").Attributes("Value").Value)

                    If strBuilder(2) = "0" Then
                        linkEdit.Disabled = True
                    Else
                        linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdPayID.Value) & "','" & strEncriptedChainCode & "','" & StrPaymentPeriod.ToString.Trim & "','" & HDPLBCYCLE.Value.Trim & "','" & HDMONTH.Value.ToString.Trim & "','" & HDYEAR.Value.ToString.Trim & "'" & ");")
                    End If

                End If
            Else
                linkEdit.Disabled = False
                strBuilder = objeAAMS.SecurityCheck(31)
                linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdPayID.Value) & "','" & strEncriptedChainCode & "','" & StrPaymentPeriod.ToString.Trim & "','" & HDPLBCYCLE.Value.Trim & "','" & HDMONTH.Value.ToString.Trim & "','" & HDYEAR.Value.ToString.Trim & "'" & ");")

            End If



           

        End If
    End Sub

#Region "CheckSecurity()::This method is used for security check."
    Private Sub CheckSecurity()
        Try
            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Incentive Payment Received']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Incentive Payment Received']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    btnSearch.Enabled = False
                    Response.Redirect("../NoRights.aspx")
                End If
                '  If strBuilder(1) = "0" Then
                '  btnNew.Enabled = False
                'End If
                If strBuilder(4) = "0" Then
                    BtnExport.Enabled = False
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
End Class
