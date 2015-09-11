
Partial Class Incentive_INCSR_Bcase
    Inherits System.Web.UI.Page

    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim strBuilder As New StringBuilder




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl

        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If

        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Business Case']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Business Case']").Attributes("Value").Value)
                If strBuilder(0) = "0" Then
                    btnSearch.Enabled = False
                End If ' 
                If strBuilder(0) = "0" Then
                    Response.Redirect("~/NoRights.aspx")
                    Exit Sub
                End If
                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                End If
                If strBuilder(4) = "0" Then
                    btnExport.Enabled = False
                End If ' 

            End If
        Else
            strBuilder = objeAAMS.SecurityCheck(31)
        End If

        btnSearch.Attributes.Add("onClick", "return ValidateForm();")
        btnExport.Attributes.Add("onClick", "return ValidateForm();")
        btnNew.Attributes.Add("onclick", "return NewMSUPManageAgencyGroupFromIncentive();")
        DlstPayType.Attributes.Add("onclick", "return PayType();")
        'ChkPLB.Attributes.Add("onclick", "return PLBType();")

        DlstPLBApplicable.Attributes.Add("onclick", "return PLBType();")

        If Not Page.IsPostBack Then

            If Request.QueryString("Chain_Code") IsNot Nothing Then
                ' LoadData()
            End If

        End If

        If DlstPayType.SelectedValue = "1" Then
            TrPaymentTerm.Attributes.Add("class", "displayBlock")
        Else
            TrPaymentTerm.Attributes.Add("class", "displayNone")
        End If
        'If ChkPLB.Checked = True Then
        '    TRPLBType.Attributes.Add("class", "displayBlock")
        'Else
        '    TRPLBType.Attributes.Add("class", "displayNone")
        'End If
        If DlstPLBApplicable.SelectedValue = "1" Then
            TRPLBType.Attributes.Add("class", "displayBlock")
        Else
            TRPLBType.Attributes.Add("class", "displayNone")
        End If

    End Sub

    Private Sub LoadData()
        Dim objInputXml As New XmlDocument
        Dim objOutPutxml As New XmlDocument
        Dim ds As DataSet
        Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase

        Dim objReadaer As XmlNodeReader
        Try

            ' <INC_SEARCH_BUSINESSCASE_INPUT><BC_ID></BC_ID><CHAIN_CODE></CHAIN_CODE><BC_EFFECTIVE_FROM></BC_EFFECTIVE_FROM><BC_VALID_TILL></BC_VALID_TILL><PAGE_NO></PAGE_NO><PAGE_SIZE></PAGE_SIZE><EmployeeID></EmployeeID></INC_SEARCH_BUSINESSCASE_INPUT>
            objInputXml.LoadXml("<INC_SEARCH_BUSINESSCASE_INPUT><BC_ID></BC_ID><CHAIN_CODE></CHAIN_CODE><BC_EFFECTIVE_FROM></BC_EFFECTIVE_FROM><BC_VALID_TILL></BC_VALID_TILL><PAGE_NO></PAGE_NO><PAGE_SIZE></PAGE_SIZE><SORT_BY/><DESC/><EmployeeID></EmployeeID><INC_TYPE_ID></INC_TYPE_ID><PAYMENTTYPEID></PAYMENTTYPEID><UPFRONTTYPEID></UPFRONTTYPEID><PLBTYPEID></PLBTYPEID><ADJUSTABLE></ADJUSTABLE><PLBSLAB></PLBSLAB></INC_SEARCH_BUSINESSCASE_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("BC_ID").InnerText = txtBCID.Text
            objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = txtChainCode.Text

            objInputXml.DocumentElement.SelectSingleNode("BC_EFFECTIVE_FROM").InnerText = objeAAMS.GetDateFormat(Request.Form("txtStartDateFrom"), "dd/MM/yyyy", "yyyyMMdd", "/")
            objInputXml.DocumentElement.SelectSingleNode("BC_VALID_TILL").InnerText = objeAAMS.GetDateFormat(Request.Form("txtStartDateTo"), "dd/MM/yyyy", "yyyyMMdd", "/")


            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If

            ' -----------------------------------------------------------------
            ' Start of  New code   Added on 22/12/10
            objInputXml.DocumentElement.SelectSingleNode("INC_TYPE_ID").InnerText = DlstPayType.SelectedValue

            If DlstPayType.SelectedValue = "1" Then
                objInputXml.DocumentElement.SelectSingleNode("UPFRONTTYPEID").InnerText = DlstPayTerm.SelectedValue
            Else
                objInputXml.DocumentElement.SelectSingleNode("UPFRONTTYPEID").InnerText = ""
            End If

            objInputXml.DocumentElement.SelectSingleNode("PAYMENTTYPEID").InnerText = DlstAdjustment.SelectedValue



            If DlstPLBApplicable.SelectedValue = "" Then
                objInputXml.DocumentElement.SelectSingleNode("PLBSLAB").InnerText = ""
                objInputXml.DocumentElement.SelectSingleNode("PLBTYPEID").InnerText = ""
            ElseIf DlstPLBApplicable.SelectedValue = "1" Then
                objInputXml.DocumentElement.SelectSingleNode("PLBSLAB").InnerText = "TRUE"
                objInputXml.DocumentElement.SelectSingleNode("PLBTYPEID").InnerText = DsltPLBType.SelectedValue
            Else
                objInputXml.DocumentElement.SelectSingleNode("PLBSLAB").InnerText = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("PLBTYPEID").InnerText = ""
            End If

            If DsltSignUpAdjstable.SelectedValue = "" Then
                objInputXml.DocumentElement.SelectSingleNode("ADJUSTABLE").InnerText = ""
            ElseIf DsltSignUpAdjstable.SelectedValue = "1" Then
                objInputXml.DocumentElement.SelectSingleNode("ADJUSTABLE").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("ADJUSTABLE").InnerText = "FALSE"
            End If


            '  End of  New code   Added on 22/12/10
            '-----------------------------------------------------------------------


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
                ViewState("SortName") = "BC_EFFECTIVE_FROM"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "BC_EFFECTIVE_FROM"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

           
           


            objOutPutxml = objbzBusinessCase.Search(objInputXml)

            Try
                ' objInputXml.Save("C:\InputBase.XML")
            Catch ex As Exception
            End Try

            'objOutPutxml.LoadXml("<INC_SEARCH_BUSINESSCASE_OUTPUT><BUSINESSCASE BC_ID='1' CHAIN_CODE='' GROUP_NAME=''  BC_DTTI_EFFECTIVE_FROM='1 Jan 2007' BC_DTTI_VALID_TILL='31 Dec 2007' INC_TYPE_NAME ='Fixed Deals' Active=''/><BUSINESSCASE BC_ID='1' CHAIN_CODE='' GROUP_NAME=''  BC_DTTI_EFFECTIVE_FROM='1 Jan 2008' BC_DTTI_VALID_TILL='31 Dec 2008' INC_TYPE_NAME='Fixed Deals' Active ='1' />	<Errors Status='False'>	<Error Code='' Description='' /></Errors></INC_SEARCH_BUSINESSCASE_OUTPUT>")


            If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                ViewState("PrevSearching") = objInputXml.OuterXml
                ViewState("ChainCode") = Nothing

                ds = New DataSet
                objReadaer = New XmlNodeReader(objOutPutxml)
                ds.ReadXml(objReadaer)
                GvBCaseDeals.DataSource = ds.Tables("BUSINESSCASE")
                GvBCaseDeals.DataBind()

                '@ Code Added For Paging And Sorting 
                pnlPaging.Visible = True
                BindControlsForNavigation(objOutPutxml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)


                txtTotalRecordCount.Text = objOutPutxml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                SetImageForSorting(GvBCaseDeals)

                '@ End of Code Added For Paging And Sorting 

                DivFAColor.Visible = True
            Else
                GvBCaseDeals.DataSource = Nothing
                GvBCaseDeals.DataBind()
                lblError.Text = objOutPutxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

                txtTotalRecordCount.Text = "0"

                pnlPaging.Visible = False

                DivFAColor.Visible = False
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            pnlPaging.Visible = False
            DivFAColor.Visible = False
        End Try
    End Sub
    Protected Sub GvBCaseDeals_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvBCaseDeals.RowDataBound
        Try



            If e.Row.RowType = DataControlRowType.DataRow Then

                '  e.Row.Cells(0).Text = (e.Row.RowIndex + 1).ToString


                '  If Request.QueryString("Chain_Code") IsNot Nothing Then

                Dim hdBCaseID As HiddenField
                hdBCaseID = e.Row.FindControl("hdBCaseID")

                Dim hdChainCode As HiddenField
                hdChainCode = e.Row.FindControl("hdChainCode")


                Dim strEncriptedChainCode As String = objED.Encrypt(hdChainCode.Value.Trim)



                Dim hdActive As HiddenField
                hdActive = e.Row.FindControl("hdActive")

                If hdActive.Value.Trim.ToUpper() = "TRUE" Then
                    e.Row.BackColor = Drawing.Color.LightSeaGreen
                    e.Row.ForeColor = Drawing.Color.Black
                End If


                Dim linkDetails As System.Web.UI.HtmlControls.HtmlAnchor
                linkDetails = e.Row.FindControl("linkDetails")



                Dim linkViewDoc As System.Web.UI.HtmlControls.HtmlAnchor
                linkViewDoc = e.Row.FindControl("linkViewDoc")


                Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
                linkEdit = e.Row.FindControl("linkEdit")

                'linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdBCaseID.Value & "','" & strEncriptedChainCode & "');")
                'linkDetails.Attributes.Add("onclick", "return DetailsFunction('" & hdBCaseID.Value & "','" & strEncriptedChainCode & "');")
                'linkViewDoc.Attributes.Add("onclick", "return ViewDocFunction('" & hdBCaseID.Value & "','" & strEncriptedChainCode & "');")


                Dim strBuilder As New StringBuilder
                Dim objSecurityXml As New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Business Case']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Business Case']").Attributes("Value").Value)
                        If strBuilder(0) = "0" Then
                            linkDetails.Disabled = True
                        Else
                            linkDetails.Attributes.Add("onclick", "return DetailsFunction('" & hdBCaseID.Value & "','" & strEncriptedChainCode & "');")
                        End If

                        If strBuilder(2) = "0" Then
                            linkEdit.Disabled = True
                        Else
                            linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdBCaseID.Value & "','" & strEncriptedChainCode & "');")
                        End If

                    End If
                Else
                    linkEdit.Disabled = False
                    linkDetails.Disabled = False
                    linkViewDoc.Disabled = False

                    strBuilder = objeAAMS.SecurityCheck(31)
                    linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdBCaseID.Value & "','" & strEncriptedChainCode & "');")
                    linkDetails.Attributes.Add("onclick", "return DetailsFunction('" & hdBCaseID.Value & "','" & strEncriptedChainCode & "');")
                    linkViewDoc.Attributes.Add("onclick", "return ViewDocFunction('" & hdBCaseID.Value & "','" & strEncriptedChainCode & "');")

                End If



                '##################################################################
                'If ViewState("ChainCode") Is Nothing Then
                '    ViewState("ChainCode") = e.Row.Cells(2).Text.Trim()
                '    Return
                'Else
                '    If ViewState("ChainCode") = e.Row.Cells(2).Text.Trim() Then
                '        e.Row.Cells(2).Text = ""
                '        e.Row.Cells(3).Text = ""
                '        ' e.Row.Cells(0).Text = ""

                '    Else
                '        ViewState("ChainCode") = e.Row.Cells(2).Text.Trim()
                '    End If
                'End If
                '##################################################################

            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        LoadData()
    End Sub


#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            LoadData()

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            LoadData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            LoadData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub GvBCaseDeals_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvBCaseDeals.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub GvBCaseDeals_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GvBCaseDeals.Sorting
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

            LoadData()


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
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click

        Dim objInputXml As New XmlDocument
        Dim objOutPutxml As New XmlDocument
        Dim ds As New DataSet
        '        Dim objReadaer As XmlNodeReader
        Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase

        Try

            ' <INC_SEARCH_BUSINESSCASE_INPUT><BC_ID></BC_ID><CHAIN_CODE></CHAIN_CODE><BC_EFFECTIVE_FROM></BC_EFFECTIVE_FROM><BC_VALID_TILL></BC_VALID_TILL><PAGE_NO></PAGE_NO><PAGE_SIZE></PAGE_SIZE><EmployeeID></EmployeeID></INC_SEARCH_BUSINESSCASE_INPUT>
            '  objInputXml.LoadXml("<INC_SEARCH_BUSINESSCASE_INPUT><BC_ID></BC_ID><CHAIN_CODE></CHAIN_CODE><BC_EFFECTIVE_FROM></BC_EFFECTIVE_FROM><BC_VALID_TILL></BC_VALID_TILL><PAGE_NO></PAGE_NO><PAGE_SIZE></PAGE_SIZE><SORT_BY/><DESC/><EmployeeID></EmployeeID></INC_SEARCH_BUSINESSCASE_INPUT>")

            objInputXml.LoadXml("<INC_SEARCH_BUSINESSCASE_INPUT><BC_ID></BC_ID><CHAIN_CODE></CHAIN_CODE><BC_EFFECTIVE_FROM></BC_EFFECTIVE_FROM><BC_VALID_TILL></BC_VALID_TILL><PAGE_NO></PAGE_NO><PAGE_SIZE></PAGE_SIZE><SORT_BY/><DESC/><EmployeeID></EmployeeID><INC_TYPE_ID></INC_TYPE_ID><PAYMENTTYPEID></PAYMENTTYPEID><UPFRONTTYPEID></UPFRONTTYPEID><PLBTYPEID></PLBTYPEID><ADJUSTABLE></ADJUSTABLE><PLBSLAB></PLBSLAB></INC_SEARCH_BUSINESSCASE_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("BC_ID").InnerText = txtBCID.Text
            objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = txtChainCode.Text

            objInputXml.DocumentElement.SelectSingleNode("BC_EFFECTIVE_FROM").InnerText = objeAAMS.GetDateFormat(Request.Form("txtStartDateFrom"), "dd/MM/yyyy", "yyyyMMdd", "/")
            objInputXml.DocumentElement.SelectSingleNode("BC_VALID_TILL").InnerText = objeAAMS.GetDateFormat(Request.Form("txtStartDateTo"), "dd/MM/yyyy", "yyyyMMdd", "/")


            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "BC_EFFECTIVE_FROM"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "BC_EFFECTIVE_FROM"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            ' -----------------------------------------------------------------
            ' Start of  New code   Added on 22/12/10
            objInputXml.DocumentElement.SelectSingleNode("INC_TYPE_ID").InnerText = DlstPayType.SelectedValue

            If DlstPayType.SelectedValue = "1" Then
                objInputXml.DocumentElement.SelectSingleNode("UPFRONTTYPEID").InnerText = DlstPayTerm.SelectedValue
            Else
                objInputXml.DocumentElement.SelectSingleNode("UPFRONTTYPEID").InnerText = ""
            End If

            objInputXml.DocumentElement.SelectSingleNode("PAYMENTTYPEID").InnerText = DlstAdjustment.SelectedValue


            If DlstPLBApplicable.SelectedValue = "" Then
                objInputXml.DocumentElement.SelectSingleNode("PLBSLAB").InnerText = ""
                objInputXml.DocumentElement.SelectSingleNode("PLBTYPEID").InnerText = ""
            ElseIf DlstPLBApplicable.SelectedValue = "1" Then
                objInputXml.DocumentElement.SelectSingleNode("PLBSLAB").InnerText = "TRUE"
                objInputXml.DocumentElement.SelectSingleNode("PLBTYPEID").InnerText = DsltPLBType.SelectedValue
            Else
                objInputXml.DocumentElement.SelectSingleNode("PLBSLAB").InnerText = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("PLBTYPEID").InnerText = ""
            End If

            If DsltSignUpAdjstable.SelectedValue = "" Then
                objInputXml.DocumentElement.SelectSingleNode("ADJUSTABLE").InnerText = ""
            ElseIf DsltSignUpAdjstable.SelectedValue = "1" Then
                objInputXml.DocumentElement.SelectSingleNode("ADJUSTABLE").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("ADJUSTABLE").InnerText = "FALSE"
            End If


            '  End of  New code   Added on 22/12/10
            '-----------------------------------------------------------------------


            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

          
            objOutPutxml = objbzBusinessCase.Search(objInputXml)

            '<BUSINESSCASE 
            '           BC_ID = "28"                                        0
            '           CHAIN_CODE = "900"                                  1
            '           GROUP_NAME = "CONCAB TRAVEL SERVICES PVT LTD"       2       
            '           BC_EFFECTIVE_FROM = "01-Nov-07"                     3
            '           BC_VALID_TILL = "31-Oct-10"                         4
            '           INC_TYPE_NAME = "Post Payment"                      5
            '           Active = ""                                         6
            '           APPROVAL_STATUS_NAME = "Final Approved"             7
            '           PAYMENT_CYCLE_NAME = "Bi-annual"                    8
            '           PAYMENTTYPENAME = "Ratebased"                       9
            '           UPFRONTTYPENAME = ""                                10
            '           PLBTYPENAME = ""                                    11
            '           ADJUSTABLE=""                                       12
            ' /> 


            If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                Dim objExport As New ExportExcel
                'Dim strArray() As String = {"BCaseId", "Chain Code", "Group Name", "Deal Type", "Valid From", "Valid Till ", "Status"}
                'Dim intArray() As Integer = {0, 1, 2, 5, 3, 4, 7}

                Dim strArray() As String = {"BCaseId", "Chain Code", "Group Name", "Deal Type", "Payment Term", "Payment Cycle", "Adjustment", "PLB Type Name", "Signup Adjustment", "Valid From", "Valid Till ", "Status"}
                Dim intArray() As Integer = {0, 1, 2, 5, 10, 8, 9, 11, 12, 3, 4, 7}

                objExport.ExportDetails(objOutPutxml, "BUSINESSCASE", intArray, strArray, ExportExcel.ExportFormat.Excel, "BCase.xls")

            Else
                GvBCaseDeals.DataSource = Nothing
                GvBCaseDeals.DataBind()
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
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.ToString, False)
        Catch ex As Exception

        End Try
    End Sub

End Class
