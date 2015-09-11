Imports AjaxControlToolkit
Partial Class Incentive_INCSR_PaymentProcess
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt

    Dim strBuilder As New StringBuilder

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strError As String = ""
        Session("PageName") = Request.Url.ToString()
        objeAAMS.ExpirePageCache()
        btnSearch.Attributes.Add("onclick", "return CheckValidation();")
        btnExport.Attributes.Add("onclick", "return CheckValidation();")
        chkLstNIDTField.Attributes.Add("onclick", "return CheckOrUnckeckItemFromQlaification()")

        '  btnReset.Attributes.Add("onclick", "return IspOrderReset();")
        lblError.Text = String.Empty
        '***************************************************************************************
        'Code of Security Check
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            lblError.Text = "Session is expired."
            Exit Sub
        End If

        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Incentive Payment Process']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Incentive Payment Process']").Attributes("Value").Value)
                If strBuilder(0) = "0" Then
                    btnSearch.Enabled = False
                End If ' 
                If strBuilder(0) = "0" Then
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
        If (Not IsPostBack) Then
            BindAllControl()
            '   BindAverageQualification()
        End If
        lblQualError.Text = ""
        UpdQualPnl.Update()
    End Sub
    Private Sub BindAllControl()
        Try
            Dim i, j As Integer

            Dim IntYearFrom As Integer = 3
            Dim IntYearTo As Integer = 3
            'If System.Configuration.ConfigurationManager.AppSettings("YearFrom") IsNot Nothing Then
            '    IntYearFrom = Val(System.Configuration.ConfigurationManager.AppSettings("YearFrom"))
            'End If
            'If System.Configuration.ConfigurationManager.AppSettings("YearTo") IsNot Nothing Then
            '    IntYearTo = Val(System.Configuration.ConfigurationManager.AppSettings("YearTo"))
            'End If

            For j = DateTime.Now.Year + IntYearFrom To DateTime.Now.Year - IntYearTo Step -1
                drpYearFrom.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                i += 1
            Next


            drpYearFrom.SelectedValue = DateTime.Now.Year
            drpMonthFrom.SelectedValue = DateTime.Now.Month
            objeAAMS.BindDropDown(drpCity, "CITY", True, 3)
            objeAAMS.BindDropDown(drpCountry, "COUNTRY", True, 3)
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            BindData(PageOperation.Search)
            'If ViewState("MsgUpdae") IsNot Nothing Then
            '    lblError.Text = ViewState("MsgUpdae").ToString
            '    ViewState("MsgUpdae") = Nothing
            'End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub BindData(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzBCPaymentProcess As New AAMS.bizIncetive.bzBCPaymentProcess
        Dim ds As New DataSet
        Dim objXmlReader As XmlNodeReader
        Try
            objInputXml.LoadXml("<INC_SER_PAYMENTPROCESS_INPUT> <CHAIN_CODE></CHAIN_CODE> <CHAINNAME></CHAINNAME> <CITY></CITY> <COUNTRY></COUNTRY> <MONTH></MONTH> <YEAR></YEAR> <PAGE_NO></PAGE_NO> <PAGE_SIZE></PAGE_SIZE> <SORT_BY></SORT_BY> <DESC></DESC><EmployeeID></EmployeeID></INC_SER_PAYMENTPROCESS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = txtChainCode.Text
            objInputXml.DocumentElement.SelectSingleNode("CHAINNAME").InnerText = txtGroupName.Text

            If drpCity.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text
            End If
            If drpCountry.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedValue
            End If
            objInputXml.DocumentElement.SelectSingleNode("MONTH").InnerText = drpMonthFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("YEAR").InnerText = drpYearFrom.SelectedValue

            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If

            'Start CODE for sorting and paging
            If Operation = PageOperation.Search Then
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
            End If
            'End Code for paging and sorting
            'Here Back end Method Call
            objOutputXml = objbzBCPaymentProcess.Search(objInputXml)
            'objOutputXml.Load("C:\admin\PaymentSearch.xml")
            Try
                objOutputXml.Save("C:\admin\PaymentSearch.xml")
            Catch ex As Exception

            End Try

            '  objOutputXml.LoadXml("<UP_SER_INC_PAYMENTPROCESS_OUTPUT> <PAYMENTPROCESS BC_ID='87' CHAIN_CODE='12503' CHAIN_NAME='' CITY='' AOFFICE='' ACCOUNTMANAGER_NAME='' PAYMENT_CYCLE_NAME='' PAYMENTPERIOD_FROM='' PAYMENTPERIOD_TO='' PAYMENTTYPE = 'U'      UPFRONTFIRSTTIME = 'TRUE' UPFRONTTYPE='1'  /> <PAGE PAGE_COUNT='1' TOTAL_ROWS='1'/> <Errors Status='False'>  <Error Code='' Description='' /> </Errors></UP_SER_INC_PAYMENTPROCESS_OUTPUT>")
            '  objOutputXml.Save("C:\PaymentSearch.xml")
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                DivFAColor.Visible = True
                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                GvProcessPayment.DataSource = ds.Tables("PAYMENTPROCESS")
                GvProcessPayment.DataBind()
                'Code Added For Paging And Sorting In case Of Delete The Record
                pnlPaging.Visible = True
                Dim count As Integer = CInt(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
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
                'hdRecordOnCurrentPage.Value = ds.Tables("PAYMENTRECEIVED").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                ' @ Added Code To Show Image'
                SetImageForSorting(GvProcessPayment)
                '  Added Code To Show Image  '
                ' End of Code Added For Paging And Sorting In case Of Delete The Record

                Dim TotalWidth As Int64
                TotalWidth = 0
                For intclmn As Integer = 0 To GvProcessPayment.Columns.Count - 1
                    If GvProcessPayment.HeaderRow.Cells(intclmn).Visible = True Then
                        TotalWidth = TotalWidth + GvProcessPayment.Columns(intclmn).ItemStyle.Width.Value
                    End If
                Next
                GvProcessPayment.Width = TotalWidth

            Else
                GvProcessPayment.DataSource = Nothing
                GvProcessPayment.DataBind()
                txtTotalRecordCount.Text = "0"
                'hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            objOutputXml = Nothing
            objOutputXml = Nothing
            objbzBCPaymentProcess = Nothing
        End Try
    End Sub

    Protected Sub GvProcessPayment_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvProcessPayment.RowCommand
        Try
            If e.CommandName = "SelectModQual" Then
                Dim RowNum As Integer = Val(e.CommandArgument.ToString().Split("|")(0))
                Dim GvRow As GridViewRow
                'Dim LnkModQualification As LinkButton = CType(GvProcessPayment.Rows(RowNum).FindControl("LnkModQualification"), LinkButton)
                GvRow = GvProcessPayment.Rows(RowNum)
                BindNIDTFieldForQualification(GvRow, e.CommandArgument.ToString)

            End If
        Catch ex As Exception
        Finally
            SetImageForSorting(GvProcessPayment)
        End Try
    End Sub

    Protected Sub GvProcessPayment_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvProcessPayment.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If

            Dim strPayPeriod As Label
            Dim strPermisionUserFromConfig As String
            Dim strArr() As String
            Dim strArrSub() As String
            Dim strMonth As String
            Dim strYear As String
            Dim strFinal As String = ""
            Dim strFirstDate As String = ""
            Dim strSecoundDate As String = ""
            Dim StrPaymentPeriod As String = ""
            Dim count As Integer
            Dim HdPLBCycle As HiddenField
            HdPLBCycle = CType(e.Row.FindControl("HdPLBCycle"), HiddenField)
            If HdPLBCycle.Value.Trim.ToUpper.Contains("PLB") Then
                HdPLBCycle.Value = "1"
            Else
                HdPLBCycle.Value = "0"
            End If

            
            strPayPeriod = CType(e.Row.FindControl("lblPaymentFromto"), Label)
            StrPaymentPeriod = CType(e.Row.FindControl("lblPaymentFromto"), Label).Text

            strPermisionUserFromConfig = strPayPeriod.Text

            strArr = strPermisionUserFromConfig.Split("-")
            For count = 0 To strArr.Length - 1
                strArrSub = strArr(count).Split("/")
                strYear = strArrSub(2)
                strMonth = MonthName(strArrSub(1), True).ToUpper.ToString()
                strFinal = strMonth & "-" & strYear
                If count = 0 Then
                    strFirstDate = strFinal
                ElseIf count = 1 Then
                    strSecoundDate = strFinal
                End If
            Next

            strFinal = strFirstDate.ToUpper.ToString() & " to " & strSecoundDate.ToUpper.ToString()
            strPayPeriod.Text = strFinal

            Dim hdPayType As HiddenField
            hdPayType = CType(e.Row.FindControl("hdPayType"), HiddenField)
            Dim HdFirstTime As HiddenField
            HdFirstTime = CType(e.Row.FindControl("HdFirstTime"), HiddenField)
            Dim hdInput As HiddenField
            hdInput = CType(e.Row.FindControl("hdInput"), HiddenField)
            Dim lnkPayment As LinkButton
            lnkPayment = CType(e.Row.FindControl("lnkPayment"), LinkButton)

            Dim lnkPaymentMenu As LinkButton
            lnkPaymentMenu = CType(e.Row.FindControl("lnkPaymentMenu"), LinkButton)

            Dim found As Control = e.Row.FindControl("menu1")
            Dim lnkHistory As LinkButton
            lnkHistory = CType(e.Row.FindControl("lnkHistory"), LinkButton)
            Dim lnkPaymentSheet As LinkButton
            lnkPaymentSheet = CType(e.Row.FindControl("lnkPaymentSheet"), LinkButton)

            Dim hdDecChainCode As String = hdInput.Value.ToString().Split("|")(1)
            Dim hdEnChainCode As String = objED.Encrypt(hdDecChainCode)
            Dim HdCurPayNo As HiddenField
            HdCurPayNo = CType(e.Row.FindControl("HdCurPayNo"), HiddenField)
            Dim HdMonth As HiddenField
            HdMonth = CType(e.Row.FindControl("HdMonth"), HiddenField)
            HdMonth.Value = drpMonthFrom.SelectedValue
            Dim HdYear As HiddenField
            HdYear = CType(e.Row.FindControl("HdYear"), HiddenField)
            HdYear.Value = drpYearFrom.SelectedValue

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Incentive Payment Process']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Incentive Payment Process']").Attributes("Value").Value)
                    If strBuilder(1) = "0" Then
                        lnkPayment.Enabled = False
                    Else
                        lnkPayment.Attributes.Add("onclick", "return Payment(" & "'" & hdInput.Value.ToString().Split("|")(0) & "'" + ",'" & hdEnChainCode & "'" + ",'" & drpMonthFrom.SelectedValue & "'" + ",'" & drpYearFrom.SelectedValue & "','" & HdFirstTime.Value & "','" & hdPayType.Value.ToString.Trim.ToUpper & "','" & HdCurPayNo.Value.ToString.Trim.ToUpper & "','" & StrPaymentPeriod.ToString.Trim & "','" & HdPLBCycle.Value.Trim & "'" & ");")
                        'lnkPaymentMenu.Attributes.Add("onmouseover", "javascript:return display_menu(this, " & "'" & hdInput.Value.ToString().Split("|")(0) & "'" & ");")
                        'lnkPaymentMenu.Attributes.Add("onmouseout", "javascript:return Autohide_menu(" & "'div" & hdInput.Value.ToString().Split("|")(0) & "'" & ");")
                        '  lnkPaymentMenu.Attributes.Add("onmouseover", "javascript:return display_menu(this, " & "'" & (e.Row.RowIndex + 1).ToString & "'" & ");")
                        ' lnkPaymentMenu.Attributes.Add("onmouseout", "javascript:return Autohide_menu(" & "'div" & (e.Row.RowIndex + 1).ToString & "'" & ");")
                    End If
                Else
                    lnkPayment.Enabled = False
                End If
            Else
                ' lnkPayment.Attributes.Add("onclick", "return Payment(" & "'" & hdInput.Value.ToString().Split("|")(0) & "'" + ",'" & hdEnChainCode & "'" + ",'" & drpMonthFrom.SelectedValue & "'" + ",'" & drpYearFrom.SelectedValue & "','" & HdFirstTime.Value & "','" & hdPayType.Value.ToString.Trim.ToUpper & "','" & HdCurPayNo.Value.ToString.Trim.ToUpper & "'" & ");")
                lnkPayment.Attributes.Add("onclick", "return Payment(" & "'" & hdInput.Value.ToString().Split("|")(0) & "'" + ",'" & hdEnChainCode & "'" + ",'" & drpMonthFrom.SelectedValue & "'" + ",'" & drpYearFrom.SelectedValue & "','" & HdFirstTime.Value & "','" & hdPayType.Value.ToString.Trim.ToUpper & "','" & HdCurPayNo.Value.ToString.Trim.ToUpper & "','" & StrPaymentPeriod.ToString.Trim & "','" & HdPLBCycle.Value.Trim & "'" & ");")
                'lnkPaymentMenu.Attributes.Add("onmouseover", "javascript:return display_menu(this, " & "'div" & hdInput.Value.ToString().Split("|")(0) & "'" & ");")
                'lnkPaymentMenu.Attributes.Add("onmouseout", "javascript:return Autohide_menu(" & "'div" & hdInput.Value.ToString().Split("|")(0) & "'" & ");")
                'lnkPaymentMenu.Attributes.Add("onmouseover", "javascript:return display_menu(this, " & "'div" & (e.Row.RowIndex + 1).ToString & "'" & ");")
                'lnkPaymentMenu.Attributes.Add("onmouseout", "javascript:return Autohide_menu(" & "'div" & (e.Row.RowIndex + 1).ToString & "'" & ");")

            End If

          
            lnkHistory.Attributes.Add("onclick", "return History(" & "'" & hdInput.Value.ToString().Split("|")(0) & "'" + ",'" & hdEnChainCode & "'" + ",'" & drpMonthFrom.SelectedValue & "'" + ",'" & drpYearFrom.SelectedValue & "','" & HdFirstTime.Value & "','" & hdPayType.Value.ToString.Trim.ToUpper & "','" & HdCurPayNo.Value.ToString.Trim.ToUpper & "','" & StrPaymentPeriod.ToString.Trim & "','" & HdPLBCycle.Value.Trim & "'" & ");")
            Dim LnkModQualification As LinkButton
            LnkModQualification = CType(e.Row.FindControl("LnkModQualification"), LinkButton)

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Qualification Average']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Qualification Average']").Attributes("Value").Value)
                    If strBuilder(2) = "0" Then
                        LnkModQualification.Enabled = False
                    Else
                        LnkModQualification.Enabled = True
                    End If
                Else
                    LnkModQualification.Enabled = False
                End If
            Else
                LnkModQualification.Enabled = True
            End If

            'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PaymentSheet']").Count <> 0 Then
            '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PaymentSheet']").Attributes("Value").Value)
            '        If strBuilder(1) = "0" Then
            '            lnkPaymentSheet.Enabled = False
            '        Else
            '            lnkPaymentSheet.Attributes.Add("onclick", "return PaymentSheet(" & "'" & hdInput.Value.ToString().Split("|")(0) & "'" + ",'" & hdEnChainCode & "'" + ",'" & drpMonthFrom.SelectedValue & "'" + ",'" & drpYearFrom.SelectedValue & "','" & HdFirstTime.Value & "','" & hdPayType.Value.ToString.Trim.ToUpper & "','" & HdCurPayNo.Value.ToString.Trim.ToUpper & "'" & ");")
            '        End If

            '    Else
            '        lnkPaymentSheet.Enabled = False
            '    End If
            'Else
            '    lnkPaymentSheet.Attributes.Add("onclick", "return PaymentSheet(" & "'" & hdInput.Value.ToString().Split("|")(0) & "'" + ",'" & hdEnChainCode & "'" + ",'" & drpMonthFrom.SelectedValue & "'" + ",'" & drpYearFrom.SelectedValue & "','" & HdFirstTime.Value & "','" & hdPayType.Value.ToString.Trim.ToUpper & "','" & HdCurPayNo.Value.ToString.Trim.ToUpper & "'" & ");")
            'End If

            lnkPaymentMenu.Attributes.Add("onmouseover", "javascript:return display_menu(this, " & "'div" & (e.Row.RowIndex + 1).ToString & "'" & ");")
            lnkPaymentMenu.Attributes.Add("onmouseout", "javascript:return Autohide_menu(" & "'div" & (e.Row.RowIndex + 1).ToString & "'" & ");")


            Dim HdRejected As HiddenField = CType(e.Row.FindControl("HdRejected"), HiddenField)
            Dim HdPACreated As HiddenField = CType(e.Row.FindControl("HdPACreated"), HiddenField)
            Dim HdSkipPayment As HiddenField = CType(e.Row.FindControl("HdSkipPayment"), HiddenField)
            If HdRejected.Value.Trim.ToUpper = "TRUE" Then
                e.Row.BackColor = Drawing.Color.Red
                e.Row.ForeColor = Drawing.Color.Black
            End If

            If HdPACreated.Value.Trim.ToUpper = "TRUE" Then
                e.Row.BackColor = Drawing.Color.LightSeaGreen
                e.Row.ForeColor = Drawing.Color.Black
            End If

            If HdSkipPayment.Value.Trim.ToUpper = "TRUE" Then
                e.Row.BackColor = Drawing.Color.Yellow
                e.Row.ForeColor = Drawing.Color.Black
            End If


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            BindData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            BindData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            BindData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub GvProcessPayment_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvProcessPayment.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub GvProcessPayment_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GvProcessPayment.Sorting
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
            BindData(PageOperation.Search)

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    'Code for Export
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            BindData(PageOperation.Export)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    Enum PageOperation
        Search = 1
        Export = 2
    End Enum
    Sub Export(ByVal objOutputIspOrderPaySearchXml As XmlDocument)
        For Each objNode As XmlNode In objOutputIspOrderPaySearchXml.DocumentElement.SelectNodes("PAYMENTPROCESS")
            objNode.Attributes("PAYMENTPERIOD_FROM").Value = objNode.Attributes("PAYMENTPERIOD_FROM").Value + " - " + objNode.Attributes("PAYMENTPERIOD_TO").Value
        Next
        Dim objExport As New ExportExcel
        Dim strArray() As String = {"BCaseId", "Chain Code", "Group Name", "Billing Cycle", "City", "Aoffice", "Payment Period", "Account Manager", "Cheque Received", "Qualification Average"}
        Dim intArray() As Integer = {0, 1, 2, 6, 3, 4, 7, 5, 13, 15}
        ' <PAYMENTPROCESS BC_ID="1" CHAIN_CODE="11597" CHAIN_NAME="AGARTALA TRAVELS" CITY="Agartala" AOFFICE="IXA" ACCOUNTMANAGER_NAME="" PAYMENT_CYCLE_NAME="Qtrly" PAYMENTPERIOD_FROM="1/1/2010" PAYMENTPERIOD_TO="31/03/2010" PAYMENTTYPE="P" UPFRONTFIRSTTIME="False" UPFRONTTYPE="1" INC_TYPE_ID="2" BR_CHK_RECEIVED="" NO_OF_PAYMENT="3" QualiAvgNIDTFIELDS="" /> 
        objExport.ExportDetails(objOutputIspOrderPaySearchXml, "PAYMENTPROCESS", intArray, strArray, ExportExcel.ExportFormat.Excel, "IncPaymentProcess.xls")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect(Request.Url.ToString())
    End Sub

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
    Private Sub BindNIDTFieldForQualification(ByVal GvRow As GridViewRow, ByVal InputParam As String)

        Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase
        Dim objBCPaymentProcess As New AAMS.bizIncetive.bzBCPaymentProcess
        Dim dsNIDTField As DataSet
        Dim objXmlReader As XmlNodeReader
        Dim objOutputXml As New XmlDocument
        Dim objInputViewQualXml As New XmlDocument
        Dim objOutputViewQualXml As New XmlDocument
        Dim objSelectedQualOutputXml As New XmlDocument
        Dim HdPLBCycle As HiddenField
        '  Dim objNode As XmlNode
        HdInputData.Value = ""
        txtRem.Text = ""

        Try
            objOutputXml = objbzBusinessCase.List_NIDTFields()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                dsNIDTField = New DataSet
                objXmlReader = New XmlNodeReader(objOutputXml)
                dsNIDTField.ReadXml(objXmlReader)
                chkLstNIDTField.DataSource = dsNIDTField.Tables("NIDTFIELDS")
                chkLstNIDTField.DataTextField = "NIDT_FIELDS_NAME"
                chkLstNIDTField.DataValueField = "NIDT_FIELDS_ID"
                chkLstNIDTField.DataBind()
                '@ Now Select item on the basis of user selection
                For i As Integer = 0 To chkLstNIDTField.Items.Count - 1
                    '   chkLstNIDTField.Items(i).Enabled = True
                    chkLstNIDTField.Items(i).Selected = False
                Next

                objInputViewQualXml.LoadXml("<INC_VIEW_QUALIFY_INPUT><BC_ID/><PAYMENTID/><MONTH/><YEAR/><PLB/></INC_VIEW_QUALIFY_INPUT>")

                Dim HdMonth As HiddenField
                HdMonth = CType(GvRow.FindControl("HdMonth"), HiddenField)

                HdPLBCycle = CType(GvRow.FindControl("HdPLBCycle"), HiddenField)

                Dim HdYear As HiddenField
                HdYear = CType(GvRow.FindControl("HdYear"), HiddenField)
                Dim BcId As String = InputParam.Split("|")(1).ToString
                Dim strChaincode As String = InputParam.Split("|")(2).ToString

                HdInputData.Value = BcId.ToString + "|" + strChaincode + "|" + HdMonth.Value.ToString + "|" + HdYear.Value

                objInputViewQualXml.DocumentElement.SelectSingleNode("BC_ID").InnerText = BcId
                objInputViewQualXml.DocumentElement.SelectSingleNode("PAYMENTID").InnerText = ""
                objInputViewQualXml.DocumentElement.SelectSingleNode("MONTH").InnerText = HdMonth.Value
                objInputViewQualXml.DocumentElement.SelectSingleNode("YEAR").InnerText = HdYear.Value

                If HdPLBCycle.Value.Trim = "1" Then
                    objInputViewQualXml.DocumentElement.SelectSingleNode("PLB").InnerText = "True"
                    HdPLB.value = "True"
                Else
                    objInputViewQualXml.DocumentElement.SelectSingleNode("PLB").InnerText = "False"
                    HdPLB.value = "False"
                End If


                objOutputViewQualXml = objBCPaymentProcess.ViewQualification(objInputViewQualXml)
                Dim count As Integer = 0
                HdSelectedCount.Value = count

                If objOutputViewQualXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    Dim objArrayToSelect As New ArrayList(objOutputViewQualXml.DocumentElement.SelectSingleNode("QUALIFYDETAILS").Attributes("QualiAvgNIDTFIELDS").Value.Trim().Split(","))
                    For i As Integer = 0 To chkLstNIDTField.Items.Count - 1
                        For j As Integer = 0 To objArrayToSelect.Count - 1
                            If chkLstNIDTField.Items(i).Value.Trim() = objArrayToSelect(j).ToString().Trim() Then
                                chkLstNIDTField.Items(i).Selected = True
                                count = count + 1
                            End If
                        Next
                    Next
                    txtRem.Text = "" 'objOutputViewQualXml.DocumentElement.SelectSingleNode("QUALIFYDETAILS").Attributes("REMARK").Value
                    HdRem.value = txtRem.Text
                    HdSelectedCount.Value = count
                End If
                EnableDisableCase()
                Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                scriptManager.SetFocus(txtRem.ClientID)

                mdlPopUpExt.Show()

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objInputQualXml As New XmlDocument
        Dim objOutputQualXml As New XmlDocument
        Dim objBCPaymentProcess As New AAMS.bizIncetive.bzBCPaymentProcess
        Try
            System.Threading.Thread.Sleep(100)

            objInputQualXml.LoadXml("<INC_UPDATE_QUALIFY_INPUT><BC_ID/><PAYMENTID/><MONTH/><YEAR/><QualiAvgNIDTFIELDS/><REMARK/><EMPLOYEE_ID/><QualiAvgName></QualiAvgName><PLB/></INC_UPDATE_QUALIFY_INPUT>")
            Dim count As Integer = 0
            Dim strSelectedItem As String = ""
            Dim strSelectedNIDTName As String = ""

            For i As Integer = 0 To chkLstNIDTField.Items.Count - 1
                If chkLstNIDTField.Items(i).Selected = True Then
                    count = count + 1
                    If strSelectedItem.Trim.Length = 0 Then
                        strSelectedItem = chkLstNIDTField.Items(i).Value
                    Else
                        strSelectedItem = strSelectedItem + "," + chkLstNIDTField.Items(i).Value
                    End If
                    If strSelectedNIDTName.Trim.Length = 0 Then
                        strSelectedNIDTName = chkLstNIDTField.Items(i).Text.Trim
                    Else
                        strSelectedNIDTName = strSelectedNIDTName + "," + chkLstNIDTField.Items(i).Text.Trim
                    End If

                End If
            Next
            If count = 0 Then
                lblRemError.Text = "Airline data is mandatory."
                txtRem.Focus()
                mdlPopUpExt.Show()
                Exit Sub
            End If

            If txtRem.Text.Trim.Length = 0 Then
                If (count > 0) Then
                    lblRemError.Text = "Remark is mandatory."
                    txtRem.Focus()
                    mdlPopUpExt.Show()
                    Exit Sub
                End If
            End If


            ' If txtRem.Text.Trim.Length = 0 Then
            If (Val(HdSelectedCount.Value) <> count) Then
                If String.Compare(txtRem.Text, hdrem.value) = 0 Then
                    'lblRemError.Text = "Please change the remark."
                    'txtRem.Focus()
                    'mdlPopUpExt.Show()
                    'Exit Sub
                End If
            End If
            ' End If

            If txtRem.Text.Trim.Length > 8000 Then
                lblRemError.Text = "Remark can't be greater than 8000."
                mdlPopUpExt.Show()
                Exit Sub
            End If


            Dim strBcId As String = HdInputData.Value.Split("|")(0).ToString
            Dim strChaincode As String = HdInputData.Value.Split("|")(1).ToString

            Dim strMonth As String = HdInputData.Value.Split("|")(2).ToString
            Dim strYear As String = HdInputData.Value.Split("|")(3).ToString

            objInputQualXml.DocumentElement.SelectSingleNode("BC_ID").InnerText = strBcId
            objInputQualXml.DocumentElement.SelectSingleNode("PAYMENTID").InnerText = ""
            objInputQualXml.DocumentElement.SelectSingleNode("MONTH").InnerText = strMonth
            objInputQualXml.DocumentElement.SelectSingleNode("YEAR").InnerText = strYear

            If Not Session("LoginSession") Is Nothing Then
                objInputQualXml.DocumentElement.SelectSingleNode("EMPLOYEE_ID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            Else
                lblRemError.Text = "Session is expired."
                mdlPopUpExt.Show()
                Exit Sub
            End If

            objInputQualXml.DocumentElement.SelectSingleNode("QualiAvgNIDTFIELDS").InnerText = strSelectedItem
            objInputQualXml.DocumentElement.SelectSingleNode("REMARK").InnerText = txtRem.Text

            objInputQualXml.DocumentElement.SelectSingleNode("QualiAvgName").InnerText = strSelectedNIDTName


            objInputQualXml.DocumentElement.SelectSingleNode("PLB").InnerText = HdPLB.value

          

            objOutputQualXml = objBCPaymentProcess.UpdateQualification(objInputQualXml)

            If objOutputQualXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                BindData(PageOperation.Search)
                lblRemError.Text = "Qualification average updated successfully."
                HdSelectedCount.Value = count
                EnableDisableCase()

                lblQualError.Text = "Qualification average updated successfully."
                UpdQualPnl.Update()
                '  mdlPopUpExt.Show()
                mdlPopUpExt.Hide()
            Else
                lblRemError.Text = objOutputQualXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                EnableDisableCase()
                mdlPopUpExt.Show()
                SetImageForSorting(GvProcessPayment)
                lblQualError.Text = ""
                UpdQualPnl.Update()

            End If
        Catch ex As Exception
            SetImageForSorting(GvProcessPayment)
            EnableDisableCase()
            mdlPopUpExt.Show()
            lblRemError.Text = ex.Message
        End Try
    End Sub

    Private Sub EnableDisableCase()
        'Dim StrProductivity As String = ""
        'Try
        '    'Code for Case Name & Criteria
        '    For i As Integer = 0 To chkLstNIDTField.Items.Count - 1
        '        chkLstNIDTField.Items(i).Enabled = True
        '    Next

        '    For i As Integer = 0 To chkLstNIDTField.Items.Count - 1
        '        If chkLstNIDTField.Items(i).Selected = True Then
        '            If chkLstNIDTField.Items(i).Text.ToUpper() = "PRODUCTIVITY" Then
        '                StrProductivity = "1"
        '                Exit For
        '            Else
        '                StrProductivity = "0"
        '            End If
        '        End If
        '    Next
        '    If StrProductivity = "1" Then
        '        For i As Integer = 0 To chkLstNIDTField.Items.Count - 1
        '            If chkLstNIDTField.Items(i).Text.ToUpper() <> "PRODUCTIVITY" Then
        '                chkLstNIDTField.Items(i).Enabled = False
        '            End If
        '        Next
        '    ElseIf StrProductivity = "0" Then
        '        For i As Integer = 0 To chkLstNIDTField.Items.Count - 1
        '            If chkLstNIDTField.Items(i).Text.ToUpper() = "PRODUCTIVITY" Then
        '                chkLstNIDTField.Items(i).Enabled = False
        '            End If
        '        Next
        '    End If

        'Catch ex As Exception
        '    lblError.Text = ex.Message
        'End Try
    End Sub

    Protected Sub chkLstNIDTField_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkLstNIDTField.SelectedIndexChanged
        Try
            EnableDisableCase()
            mdlPopUpExt.Show()
            SetImageForSorting(GvProcessPayment)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BindAverageQualification()
        Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase
        Dim objBCPaymentProcess As New AAMS.bizIncetive.bzBCPaymentProcess
        Dim dsNIDTField As DataSet
        Dim objXmlReader As XmlNodeReader
        Dim objOutputXml As New XmlDocument
        Try
            objOutputXml = objbzBusinessCase.List_NIDTFields()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                dsNIDTField = New DataSet
                objXmlReader = New XmlNodeReader(objOutputXml)
                dsNIDTField.ReadXml(objXmlReader)
                chkLstNIDTField.DataSource = dsNIDTField.Tables("NIDTFIELDS")
                chkLstNIDTField.DataTextField = "NIDT_FIELDS_NAME"
                chkLstNIDTField.DataValueField = "NIDT_FIELDS_ID"
                chkLstNIDTField.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Try
            If Session("Security") Is Nothing Then
                Response.Redirect("~/SupportPages/TimeOutSession.aspx?Logout=True", False)
                'ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession(), True)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Rating_Script_" + Me.ClientID.ToString(), objeAAMS.CheckSession(), True)
                'Exit Sub
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class
