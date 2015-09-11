
Partial Class Sales_SASR_DSRReport
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Dim str As String
    Dim strBuilder As New StringBuilder
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl

        btnManagerVisitLog.Visible = False
        ChkLoggedByManager.Visible = False
        ChkManagerCallLog.Visible = False

        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If
        lblError.Text = ""
        grdDSRReport.PageSize = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
        Img_DSRDateF.Attributes.Add("onmousedown", "SelectDate('" & txtDateOfDSRF.ClientID.ToString() & "','" & Img_DSRDateF.ClientID.ToString() & "');")
        Img_DSRDateT.Attributes.Add("onmousedown", "SelectDate('" & txtDateOfDSRT.ClientID.ToString() & "','" & Img_DSRDateT.ClientID.ToString() & "');")

        DlstVisitSubType.Attributes.Add("onchange", "OpenStrategicCallType();")

        btnSearch.Attributes.Add("onclick", "return  CallValidation();")
        BtnExport.Attributes.Add("onclick", "return  ExportData();")

        txtAgencyName.Attributes.Add("onkeydown", "return ActDeAct();")
        TxtLCode.Attributes.Add("onkeydown", "return ActDeAct();")

        TxtLCode.Attributes.Add("onfocusout", "return EnableDisableGroupProductivity();")

        CallPostData()
        

        CheckSecurity()

        If Not IsPostBack Then

            objeAAMS.BindDropDown(DlstCity, "CITY", True, 3)
            objeAAMS.BindDropDown(DlstCountry, "COUNTRY", False, 3)
            objeAAMS.BindDropDown(DlstAoffice, "AOFFICE", True, 3)
            objeAAMS.BindDropDown(drpRegion, "REGION1", True, 3)
            objeAAMS.BindDropDown(DlstRetReason, "RetentionReason", True, 3)
            ' objeAAMS.BindDropDown(ChkLstAgencyCategory, "AGENCYTYPE", True, 3)
            FillAgenctCategory()
            fillSalesName()
            FillDepartment()
            fillStatus()
            Session("AgencyDSRSEARCHLOGtXML") = Nothing

            'Show / Hide Options
            If hdAdvanceSearch.Value.Trim = "1" Then
                btnUp.ImageUrl = "../images/up.jpg"
                PnlShowUnhideColumns.Visible = True
            Else
                btnUp.ImageUrl = "../images/down.jpg"
                PnlShowUnhideColumns.Visible = False
            End If
        End If

        Try
            If DlstVisitSubType.SelectedValue = "2" Then
                drpStrategicCallType.Enabled = True
                '  DlstRetReason.Enabled = True
                If drpStrategicCallType.SelectedValue = "2" Or drpStrategicCallType.SelectedValue = "" Then
                    DlstRetReason.Enabled = True
                Else
                    DlstRetReason.Enabled = False
                    DlstRetReason.SelectedValue = ""
                End If

            ElseIf DlstVisitSubType.SelectedValue = "1" Then
                drpStrategicCallType.Enabled = False
                drpStrategicCallType.SelectedValue = ""
                DlstRetReason.Enabled = False
                DlstRetReason.SelectedValue = ""
            Else
                drpStrategicCallType.Enabled = False
                drpStrategicCallType.SelectedValue = ""

                If drpStrategicCallType.SelectedValue = "2" Or drpStrategicCallType.SelectedValue = "" Then
                    DlstRetReason.Enabled = True
                Else
                    DlstRetReason.Enabled = False
                    DlstRetReason.SelectedValue = ""
                End If
            End If

           


        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Try
            If Session("Security") Is Nothing Then
                Response.Redirect("~/SupportPages/TimeOutSession.aspx?Logout=True", False)
                'ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession(), True)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Rating_Script_" + Me.ClientID.ToString(), objeAAMS.CheckSession(), True)
                Exit Sub
            End If
        Catch ex As Exception
            ModalLoading.Hide()
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Session("AgencyDSRSEARCHLOGtXML") = Nothing
            DSRLogSearch()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
            ModalLoading.Hide()
        End Try
    End Sub


    Protected Sub BtnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Try
            Response.Redirect(Request.Url.ToString, False)
        Catch ex As Exception

        End Try

    End Sub
    Private Sub DSRLogSearch()

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objTarget As New AAMS.bizSales.bzAgencyTarget
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        lblError.Text = ""
        Try

            '  objInputXml.LoadXml("<SL_SEARCH_LOG_DSR_INPUT> <DATEFROM/><DATETO/> <PENDING/><VISITSUBTYPE/><EMPLOYEEID/><RESP1A/><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/><LIMITED_TO_AOFFICE /> <LIMITED_TO_REGION></LIMITED_TO_REGION> <LIMITED_TO_OWNAAGENCY /> <LCODE></LCODE> <OFFICEID></OFFICEID> <AGENCY_NAME></AGENCY_NAME><AOFFICE></AOFFICE><REGION></REGION><ACATEGORY></ACATEGORY><MANAGERCALLS></MANAGERCALLS><VISITEDYES></VISITEDYES><VISITEDNO></VISITEDNO></SL_SEARCH_LOG_DSR_INPUT>")
            objInputXml.LoadXml("<SL_SEARCH_LOG_DSR_INPUT><DATEFROM /><DATETO /><PENDING /><VISITSUBTYPE /><EMPLOYEEID /><RESP1A /><PAGE_NO /><PAGE_SIZE /><SORT_BY /><DESC /><LIMITED_TO_AOFFICE /><LIMITED_TO_REGION></LIMITED_TO_REGION><LIMITED_TO_OWNAAGENCY /><LCODE></LCODE><OFFICEID></OFFICEID><AGENCY_NAME></AGENCY_NAME><AOFFICE></AOFFICE><REGION></REGION><ACATEGORY></ACATEGORY><MANAGERCALLS></MANAGERCALLS><VISITEDYES></VISITEDYES><VISITEDNO></VISITEDNO><GROUPDATA>0</GROUPDATA><CITYID></CITYID><COUNTRYID></COUNTRYID><SVR_REASONID></SVR_REASONID><DEPARTMENTIDS></DEPARTMENTIDS><CHAIN_CODE></CHAIN_CODE><STRATEGICTYPE></STRATEGICTYPE><STATUS></STATUS></SL_SEARCH_LOG_DSR_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("DATEFROM").InnerText = objeAAMS.ConvertTextDate(txtDateOfDSRF.Text)
            objInputXml.DocumentElement.SelectSingleNode("DATETO").InnerText = objeAAMS.ConvertTextDate(txtDateOfDSRT.Text)

            objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = TxtLCode.Text
            objInputXml.DocumentElement.SelectSingleNode("AGENCY_NAME").InnerText = txtAgencyName.Text
            objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerText = TxtOfficeID.Text




            objInputXml.DocumentElement.SelectSingleNode("VISITSUBTYPE").InnerText = DlstVisitSubType.SelectedValue

            If ChkPenIssue.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("PENDING").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("PENDING").InnerText = "FALSE"
            End If

            If (DlstAoffice.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = DlstAoffice.SelectedItem.Text
            End If

            If (drpRegion.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = drpRegion.SelectedItem.Text
            End If

            Dim strActg As String = ""
            For Each Item As ListItem In ChkLstAgencyCategory.Items
                If Item.Selected = True Then
                    If strActg.Trim.Length = 0 Then
                        strActg = Item.Text
                    Else
                        strActg = strActg + "," + Item.Text
                    End If
                End If
            Next

            If (strActg.Trim.Length > 0) Then
                objInputXml.DocumentElement.SelectSingleNode("ACATEGORY").InnerText = strActg
            End If

            'Dim strDepartment As String = ""
            'For Each Item As ListItem In ChkLstDepartment.Items
            '    If Item.Selected = True Then
            '        If strDepartment.Trim.Length = 0 Then
            '            strDepartment = Item.Value
            '        Else
            '            strDepartment = strDepartment + "," + Item.Value
            '        End If
            '    End If
            'Next
            'If (strDepartment.Trim.Length > 0) Then
            '    objInputXml.DocumentElement.SelectSingleNode("DEPARTMENTIDS").InnerText = strDepartment
            'End If

            If drpStatus.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("STATUS").InnerText = drpStatus.SelectedValue

            End If

            If chkGroupProductivity.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("GROUPDATA").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("GROUPDATA").InnerText = "FALSE"
            End If
            If DlstCity.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("CITYID").InnerText = DlstCountry.SelectedValue
            End If

            If DlstCountry.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("COUNTRYID").InnerText = DlstCountry.SelectedItem.Text
            End If
            If DlstRetReason.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("SVR_REASONID").InnerText = DlstRetReason.SelectedValue
            End If

            If drpStrategicCallType.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("STRATEGICTYPE").InnerText = drpStrategicCallType.SelectedValue

            End If
            objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = TxtChainCode.Text



            Dim objEmpXml As New XmlDocument
            Dim UserId As String
            objEmpXml.LoadXml(Session("Security"))
            UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()

            If Drp1ARes.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("RESP1A").InnerText = Drp1ARes.SelectedValue
            End If


            objInputXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = UserId

            If ChkManagerCallLog.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("MANAGERCALLS").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("MANAGERCALLS").InnerText = "FALSE"
            End If

            If ChkY.Checked = True And ChkN.Checked = False Then
                'dv.RowFilter = "VISITED='" + "Y" + "'"
                objInputXml.DocumentElement.SelectSingleNode("VISITEDYES").InnerText = "TRUE"
                objInputXml.DocumentElement.SelectSingleNode("VISITEDNO").InnerText = "FALSE"
            End If
            If ChkN.Checked = True And ChkY.Checked = False Then
                'dv.RowFilter = "VISITED='" + "N" + "'"
                objInputXml.DocumentElement.SelectSingleNode("VISITEDYES").InnerText = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("VISITEDNO").InnerText = "TRUE"
            End If
            If ChkN.Checked = True And ChkY.Checked = True Then
                ' dv.RowFilter = "VISITED IN ('Y', 'N')"
                objInputXml.DocumentElement.SelectSingleNode("VISITEDYES").InnerText = "TRUE"
                objInputXml.DocumentElement.SelectSingleNode("VISITEDNO").InnerText = "TRUE"
            End If
            If ChkN.Checked = False And ChkY.Checked = False Then
                ' dv.RowFilter = "VISITED='" + "Y" + "'"
                objInputXml.DocumentElement.SelectSingleNode("VISITEDYES").InnerText = "TRUE"
                objInputXml.DocumentElement.SelectSingleNode("VISITEDNO").InnerText = "FALSE"
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
                ViewState("SortName") = "AGENCY_NAME"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AGENCY_NAME"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "TRUE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

          

            '  objOutputXml.Load("c:\SearchDSROuputXml.xml")

            objOutputXml = objTarget.SearchDSR(objInputXml)




            ' objOutputXml.Load("C:\SL_SEARCH_LOG_DSR_OUTPUT.xml")
            Try
                objOutputXml.Save("c:\SearchDSROuputXml.xml")
                objInputXml.Save("c:\SearchDSRInpuXml.xml")
            Catch ex As Exception
            End Try
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                Session("AgencyDSRSEARCHLOGtXML") = objOutputXml.OuterXml
                BindData()
            Else
                grdDSRReport.DataSource = Nothing
                grdDSRReport.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                txtRecordCount.Text = "0"
                pnlPaging.Visible = False
                '  PnlShowUnhideColumns.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
            pnlPaging.Visible = False
            'PnlShowUnhideColumns.Visible = False
        End Try
    End Sub
    Protected Sub grdDSRReport_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdDSRReport.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
            ModalLoading.Hide()
        End Try
    End Sub

    Protected Sub grdDSRReport_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdDSRReport.Sorting
        Try
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ViewState("Desc") = "TRUE"
            Else
                If ViewState("SortName") = SortName Then
                    If ViewState("Desc") = "TRUE" Then
                        ViewState("Desc") = "FALSE"
                    Else
                        ViewState("Desc") = "TRUE"
                    End If
                Else
                    ViewState("SortName") = SortName
                    ViewState("Desc") = "TRUE"
                End If
            End If
            '  BindData()
            DSRLogSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
            ModalLoading.Hide()
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
                If ViewState("SortName").ToString().Trim() <> "COLORCODE" Then
                    Dim intcol As Integer = grd.Columns.IndexOf(field)
                    If ViewState("Desc") = "FALSE" Then
                        grd.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                    End If
                    If ViewState("Desc") = "TRUE" Then
                        grd.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                    End If
                End If

            End If
        Next
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged

        Try
            lblError.Text = ""
            grdDSRReport.PageIndex = ddlPageNumber.SelectedValue - 1
            'BindData()
            DSRLogSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
            ModalLoading.Hide()
        End Try

        ' BindControlsForNavigation(grdDSRReport.PageCount)
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            lblError.Text = ""
            grdDSRReport.PageIndex = ddlPageNumber.SelectedValue - 1
            DSRLogSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
            ModalLoading.Hide()
        End Try


        ' BindControlsForNavigation(grdDSRReport.PageCount)
    End Sub
    Private Sub BindControlsForNavigation(ByVal CrrentPageNo As String)
        pnlPaging.Visible = True
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
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click

        Try
            '  UpdatesessionWhenMovethepage()
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            lblError.Text = ""
            grdDSRReport.PageIndex = ddlPageNumber.SelectedValue - 1
            ' BindData()
            DSRLogSearch()
            '  BindControlsForNavigation(grdDSRReport.PageCount)
        Catch ex As Exception
            lblError.Text = ex.Message
            ModalLoading.Hide()
        End Try
    End Sub

    Protected Sub grdDSRReport_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDSRReport.RowDataBound

        Dim objSecurityXml As New XmlDocument

        Dim LnkVisitDetail As New LinkButton
        Dim objInputXml As New XmlDocument
        Dim objOutputXml As New XmlDocument

        Try

            '@ Start of  Code For Visible /Invisible Items
            If e.Row.RowType = DataControlRowType.Header Or e.Row.RowType = DataControlRowType.DataRow Or e.Row.RowType = DataControlRowType.Footer Then

                If ChkDSRCode.Checked = True Then
                    e.Row.Cells(2).Visible = True
                Else
                    e.Row.Cells(2).Visible = False
                End If
                If ChkLcode.Checked = True Then
                    e.Row.Cells(3).Visible = True
                Else
                    e.Row.Cells(3).Visible = False
                End If

                If ChkChaincode.Checked = True Then
                    e.Row.Cells(4).Visible = True
                Else
                    e.Row.Cells(4).Visible = False
                End If

                If ChkAddress.Checked = True Then
                    e.Row.Cells(6).Visible = True
                Else
                    e.Row.Cells(6).Visible = False
                End If


                If ChkCity.Checked = True Then
                    e.Row.Cells(7).Visible = True
                Else
                    e.Row.Cells(7).Visible = False
                End If

                If ChkCateg.Checked = True Then
                    e.Row.Cells(8).Visible = True
                Else
                    e.Row.Cells(8).Visible = False
                End If

                If ChkOfficeID.Checked = True Then
                    e.Row.Cells(9).Visible = True
                Else
                    e.Row.Cells(9).Visible = False
                End If

                If ChkSalesExec.Checked = True Then
                    e.Row.Cells(10).Visible = True
                Else
                    e.Row.Cells(10).Visible = False
                End If
                If ChkVisitYesNo.Checked = True Then
                    e.Row.Cells(11).Visible = True
                Else
                    e.Row.Cells(11).Visible = False
                End If

                If ChkReasonRemNotVisited.Checked = True Then
                    e.Row.Cells(12).Visible = True
                Else
                    e.Row.Cells(12).Visible = False
                End If

                If ChkSegstarget.Checked = True Then
                    e.Row.Cells(13).Visible = True
                Else
                    e.Row.Cells(13).Visible = False
                End If

                If ChkSalesObjVisit.Checked = True Then
                    e.Row.Cells(14).Visible = True
                Else
                    e.Row.Cells(14).Visible = False
                End If

                If ChkVisittargets.Checked = True Then
                    e.Row.Cells(15).Visible = True
                Else
                    e.Row.Cells(15).Visible = False
                End If

                If ChkPlanVisitDone.Checked = True Then
                    e.Row.Cells(16).Visible = True
                Else
                    e.Row.Cells(16).Visible = False
                End If

                If ChkUnPlanVisitDone.Checked = True Then
                    e.Row.Cells(17).Visible = True
                Else
                    e.Row.Cells(17).Visible = False
                End If

                If ChkMIDT.Checked = True Then
                    e.Row.Cells(18).Visible = True
                Else
                    e.Row.Cells(18).Visible = False
                End If

                If ChkMIDTLatest.Checked = True Then
                    e.Row.Cells(19).Visible = True
                Else
                    e.Row.Cells(19).Visible = False
                End If


                If ChkBIDT.Checked = True Then
                    e.Row.Cells(20).Visible = True
                Else
                    e.Row.Cells(20).Visible = False
                End If

                If ChkBIDTLatest.Checked = True Then
                    e.Row.Cells(21).Visible = True
                Else
                    e.Row.Cells(21).Visible = False
                End If

                If ChkPastMotive.Checked = True Then
                    e.Row.Cells(22).Visible = True
                Else
                    e.Row.Cells(22).Visible = False
                End If

               
                If ChkMinSeg.Checked = True Then
                    e.Row.Cells(23).Visible = True
                Else
                    e.Row.Cells(23).Visible = False
                End If

                If ChkBCommit.Checked = True Then
                    e.Row.Cells(24).Visible = True
                Else
                    e.Row.Cells(24).Visible = False
                End If


                If ChkVisitSubType.Checked = True Then
                    e.Row.Cells(25).Visible = True
                Else
                    e.Row.Cells(25).Visible = False
                End If

                If ChkMarketInfo.Checked = True Then
                    e.Row.Cells(27).Visible = True
                Else
                    e.Row.Cells(27).Visible = False
                End If

                If ChkPendingIssue.Checked = True Then
                    e.Row.Cells(28).Visible = True
                Else
                    e.Row.Cells(28).Visible = False
                End If

                If ChkFollowupRem.Checked = True Then
                    e.Row.Cells(29).Visible = True
                Else
                    e.Row.Cells(29).Visible = False
                End If
                If ChkPrevRem1.Checked = True Then
                    e.Row.Cells(30).Visible = True
                Else
                    e.Row.Cells(30).Visible = False
                End If
                If ChkPrevRem2.Checked = True Then
                    e.Row.Cells(31).Visible = True
                Else
                    e.Row.Cells(31).Visible = False
                End If



                If ChkDesig.Checked = True Then
                    e.Row.Cells(32).Visible = True
                Else
                    e.Row.Cells(32).Visible = False
                End If

                If ChkPersonMet.Checked = True Then
                    e.Row.Cells(33).Visible = True
                Else
                    e.Row.Cells(33).Visible = False
                End If

              

                If ChkLogDate.Checked = True Then
                    e.Row.Cells(34).Visible = True
                Else
                    e.Row.Cells(34).Visible = False
                End If

                If ChkInOutTime.Checked = True Then
                    e.Row.Cells(35).Visible = True
                    e.Row.Cells(36).Visible = True
                Else
                    e.Row.Cells(35).Visible = False
                    e.Row.Cells(36).Visible = False
                End If


                If ChkAcBYManger.Checked = True Then
                    e.Row.Cells(37).Visible = True
                Else
                    e.Row.Cells(37).Visible = False
                End If

                If ChkAcBYRepManger.Checked = True Then
                    e.Row.Cells(38).Visible = True
                Else
                    e.Row.Cells(38).Visible = False
                End If

                If ChkAcBycolleague.Checked = True Then
                    e.Row.Cells(39).Visible = True
                Else
                    e.Row.Cells(39).Visible = False
                End If

                If ChkJointCallremarks.Checked = True Then
                    e.Row.Cells(40).Visible = True
                Else
                    e.Row.Cells(40).Visible = False
                End If

                If ChkAssignedTo.Checked = True Then
                    e.Row.Cells(41).Visible = True
                    e.Row.Cells(42).Visible = True
                Else
                    e.Row.Cells(41).Visible = False
                    e.Row.Cells(42).Visible = False
                End If

                If ChkLoggedByManager.Visible = True Then
                    If ChkLoggedByManager.Checked = True Then
                        e.Row.Cells(43).Visible = True
                    Else
                        e.Row.Cells(43).Visible = False
                    End If
                Else
                    e.Row.Cells(43).Visible = False
                End If

                If ChkVisitDate.Checked = True Then
                    e.Row.Cells(44).Visible = True
                Else
                    e.Row.Cells(44).Visible = False
                End If

                If ChkDepartment.Checked = True Then
                    e.Row.Cells(45).Visible = True
                Else
                    e.Row.Cells(45).Visible = False
                End If
                If ChkDeptSpecific.Checked = True Then
                    e.Row.Cells(46).Visible = True
                Else
                    e.Row.Cells(46).Visible = False
                End If
                If ChkTarCloserDate.Checked = True Then
                    e.Row.Cells(47).Visible = True
                Else
                    e.Row.Cells(47).Visible = False
                End If

                If ChkCloserDate.Checked = True Then
                    e.Row.Cells(48).Visible = True
                Else
                    e.Row.Cells(48).Visible = False
                End If

                If ChkRetReason.Checked = True Then
                    e.Row.Cells(49).Visible = True
                Else
                    e.Row.Cells(49).Visible = False
                End If
                If ChkCPS.Checked = True Then
                    e.Row.Cells(50).Visible = True
                Else
                    e.Row.Cells(50).Visible = False
                End If
                If ChkNewCPS.Checked = True Then
                    e.Row.Cells(51).Visible = True
                Else
                    e.Row.Cells(51).Visible = False
                End If

                If Chk1aAppNewDeal.Checked = True Then
                    e.Row.Cells(52).Visible = True
                Else
                    e.Row.Cells(52).Visible = False
                End If

            
                If ChkSignOnConvDate.Checked = True Then
                    e.Row.Cells(53).Visible = True
                Else
                    e.Row.Cells(53).Visible = False
                End If
                If ChkInsDate.Checked = True Then
                    e.Row.Cells(54).Visible = True
                Else
                    e.Row.Cells(54).Visible = False
                End If
                If ChkProdName.Checked = True Then
                    e.Row.Cells(55).Visible = True
                Else
                    e.Row.Cells(55).Visible = False
                End If
                If ChkRevenue.Checked = True Then
                    e.Row.Cells(56).Visible = True
                Else
                    e.Row.Cells(56).Visible = False
                End If

                If ChkTargerSegPer.Checked = True Then
                    e.Row.Cells(57).Visible = True
                Else
                    e.Row.Cells(57).Visible = False
                End If
               
               



                If e.Row.RowType = DataControlRowType.Footer Then

                    If Session("AgencyDSRSEARCHLOGtXML") IsNot Nothing Then
                        objOutputXml.LoadXml(Session("AgencyDSRSEARCHLOGtXML").ToString)
                        e.Row.Cells(5).Text = "Total"
                        '    <PAGE_TOTAL BIDT="0" MIDT="0" A1DAILYMOTIVES="0" SEGSTARGET="244" OBJ_VISITCOUNT="30" VISITTARGET="30" PVISITDONE="1" UVISITDONE="1" BCMINSEGMENT="2400" /> 

                        e.Row.Cells(13).Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("SEGSTARGET").Value
                        e.Row.Cells(14).Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("OBJ_VISITCOUNT").Value
                        e.Row.Cells(15).Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("VISITTARGET").Value
                        e.Row.Cells(16).Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("PVISITDONE").Value
                        e.Row.Cells(17).Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("UVISITDONE").Value
                        e.Row.Cells(18).Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("MIDT").Value
                        ' e.Row.Cells(19).Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("BIDT").Value
                        e.Row.Cells(20).Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("BIDT").Value
                        '  e.Row.Cells(21).Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("BIDT").Value

                        e.Row.Cells(22).Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("A1DAILYMOTIVES").Value

                        e.Row.Cells(23).Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("BCMINSEGMENT").Value
                    End If
                End If
            End If

            '@ End of  Code For Visible /Invisible Items
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim VisitDate As String = ""
                Dim strIsManager As String = ""
                'Dim txtTarget As TextBox = CType(e.Row.FindControl("txtTarget"), TextBox)
                'txtTarget.Enabled = True

                Dim hdDSRVistedId, hdRowId, HdResID, HdUnpllanedVisit, HdVisitYesNo, HdLcode, HdChainCode, HdPreDate, HdIsManager As HiddenField
                hdDSRVistedId = CType(e.Row.FindControl("hdDSRVistedId"), HiddenField)
                HdResID = CType(e.Row.FindControl("HdResID"), HiddenField)
                hdRowId = CType(e.Row.FindControl("hdRowId"), HiddenField)
                HdUnpllanedVisit = CType(e.Row.FindControl("HdUnpllanedVisit"), HiddenField)

                HdVisitYesNo = CType(e.Row.FindControl("HdVisitYesNo"), HiddenField)
                HdLcode = CType(e.Row.FindControl("HdLcode"), HiddenField)
                HdChainCode = CType(e.Row.FindControl("HdChainCode"), HiddenField)
                HdPreDate = CType(e.Row.FindControl("HdPreDate"), HiddenField)
                HdIsManager = CType(e.Row.FindControl("HdIsManager"), HiddenField)

                If (HdIsManager.Value = "True") Then
                    strIsManager = "1"
                Else
                    strIsManager = "0"
                End If

                VisitDate = HdPreDate.Value

                VisitDate = objeAAMS.GetDateFormat(VisitDate.ToString.Trim, "dd-MM-yy", "yyyyMMdd", "-")

                'If HdVisitYesNo.Value.Trim.ToUpper = "Y" Then
                '    ChkVisitYesNo.Checked = True
                'Else
                '    ChkVisitYesNo.Checked = False
                'End If

                LnkVisitDetail = e.Row.FindControl("LnkVisitDetail")

                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Account Management']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Account Management']").Attributes("Value").Value)
                        If strBuilder(2) = "0" Then
                            LnkVisitDetail.Enabled = False

                        Else
                            LnkVisitDetail.Attributes.Add("onclick", "javascript:return OpenVistDetails('" + hdRowId.Value + "','" + hdDSRVistedId.Value + "','" + HdUnpllanedVisit.Value + "','" + HdResID.Value + "','" + HdLcode.Value + "','" + HdChainCode.Value + "','" + VisitDate + "','" + strIsManager + "')")
                        End If
                    Else
                        LnkVisitDetail.Enabled = False
                    End If
                Else
                    LnkVisitDetail.Enabled = True
                    LnkVisitDetail.Attributes.Add("onclick", "javascript:return OpenVistDetails('" + hdRowId.Value + "','" + hdDSRVistedId.Value + "','" + HdUnpllanedVisit.Value + "','" + HdResID.Value + "','" + HdLcode.Value + "','" + HdChainCode.Value + "','" + VisitDate + "','" + strIsManager + "')")
                End If

                'If Now.Day <= 7 Or Now.Day = DateTime.DaysInMonth(Now.Year, Now.Month - 1) Then
                '    BtnSave.Enabled = True
                'Else
                '    Dim txtTarget As TextBox = CType(e.Row.FindControl("txtTarget"), TextBox)
                '    BtnSave.Enabled = False
                '    LnkCalender.Attributes.Clear()
                '    LnkCalender.Enabled = False
                '    txtTarget.Enabled = False
                'End If

                If HdVisitYesNo.Value.Trim.ToUpper = "Y" Then
                    LnkVisitDetail.Enabled = True
                Else
                    LnkVisitDetail.Enabled = False
                    LnkVisitDetail.Attributes.Clear()
                End If

                '@ Start For Colorcode Implementation
                Dim hdColorCode As HiddenField
                hdColorCode = CType(e.Row.FindControl("hdColorCode"), HiddenField)
                Dim ImgColorCode As Image
                ImgColorCode = CType(e.Row.FindControl("ImgColorCode"), Image)
                If hdColorCode.Value.Trim.ToUpper = "RED" Then
                    ImgColorCode.ImageUrl = "~/Images/Redflag.gif"
                ElseIf hdColorCode.Value.Trim.ToUpper = "PINK" Then
                    ImgColorCode.ImageUrl = "~/Images/Pinkflag.gif"
                Else
                    ImgColorCode.Visible = False
                End If
                '@ End For Colorcode Implementation


            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ChkAcBYManger_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkAcBYManger.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkAcBYRepManger_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkAcBYRepManger.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkBCommit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkBCommit.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkBIDT_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkBIDT.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkCity_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkCity.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkCateg_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkCateg.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkChaincode_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkChaincode.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkDesig_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkDesig.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkLogDate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkLogDate.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkMIDT_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkMIDT.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkMinSeg_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkMinSeg.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkPastMotive_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkPastMotive.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkPendingIssue_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkPendingIssue.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkPersonMet_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkPersonMet.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkPlanVisitDone_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkPlanVisitDone.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkUnPlanVisitDone_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkUnPlanVisitDone.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkVisitSubType_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkVisitSubType.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkLcode_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkLcode.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkSalesObjVisit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkSalesObjVisit.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkAcBycolleague_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkAcBycolleague.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkAddress_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkAddress.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkJointCallremarks_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkJointCallremarks.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkSegstarget_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkSegstarget.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkVisittargets_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkVisittargets.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkAssignedTo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkAssignedTo.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkReasonRemNotVisited_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkReasonRemNotVisited.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkLoggedByManager_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkLoggedByManager.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkY_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkY.CheckedChanged
        BindData("Y")
    End Sub
    Protected Sub ChkN_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkN.CheckedChanged
        BindData("N")
    End Sub
    Protected Sub ChkOfficeID_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkOfficeID.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkSalesExec_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkSalesExec.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkMarketInfo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkMarketInfo.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkFollowupRem_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkFollowupRem.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkPrevRem1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkPrevRem1.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkPrevRem2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkPrevRem2.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkVisitYesNo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkVisitYesNo.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkInOutTime_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkInOutTime.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkVisitDate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkVisitDate.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkDSRCode_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkDSRCode.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkMIDTLatest_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkMIDTLatest.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkBIDTLatest_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkBIDTLatest.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkDepartment_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkDepartment.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkDeptSpecific_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkDeptSpecific.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkTarCloserDate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkTarCloserDate.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkCloserDate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkCloserDate.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkRetReason_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkRetReason.CheckedChanged
        BindData()
    End Sub
    Protected Sub Chk1aAppNewDeal_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Chk1aAppNewDeal.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkNewCPS_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkNewCPS.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkCPS_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkCPS.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkSignOnConvDate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkSignOnConvDate.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkTargerSegPer_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkTargerSegPer.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkProdName_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkProdName.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkRevenue_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkRevenue.CheckedChanged
        BindData()
    End Sub
    Protected Sub ChkInsDate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkInsDate.CheckedChanged
        BindData()
    End Sub

    Private Sub BindData(Optional ByVal strVisited As String = "Y")
        If Session("Security") Is Nothing Then
            lblError.Text = "Session is expired."
            Exit Sub
        End If

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objTarget As New AAMS.bizSales.bzAgencyTarget
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim TotalWidth As Int64
        Dim dv As DataView
        TotalWidth = 0
        Dim TotalLeftWidth As Int64
        TotalLeftWidth = 0
        Try
            If Session("AgencyDSRSEARCHLOGtXML") IsNot Nothing Then
                objOutputXml.LoadXml(Session("AgencyDSRSEARCHLOGtXML").ToString)
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)

                dv = ds.Tables("DSRLOG").DefaultView

                If ds.Tables("DSRLOG").Rows.Count > 0 Then

                    'If ChkY.Checked = True And ChkN.Checked = False Then
                    '    dv.RowFilter = "VISITED='" + "Y" + "'"
                    'End If
                    'If ChkN.Checked = True And ChkY.Checked = False Then
                    '    dv.RowFilter = "VISITED='" + "N" + "'"
                    'End If
                    'If ChkN.Checked = True And ChkY.Checked = True Then
                    '    dv.RowFilter = "VISITED IN ('Y', 'N')"
                    'End If
                    'If ChkN.Checked = False And ChkY.Checked = False Then
                    '    dv.RowFilter = "VISITED='" + "Y" + "'"
                    'End If

                    'grdDSRReport.DataSource = ds.Tables("DSRLOG").DefaultView
                    If dv.Count > 0 Then
                        grdDSRReport.DataSource = dv
                        grdDSRReport.DataBind()
                        '@ Code Added For Paging And Sorting 
                        pnlPaging.Visible = True
                        BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                        txtRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                        SetImageForSorting(grdDSRReport)
                        Session("AgencyDSRSEARCHLOGtXML") = objOutputXml.OuterXml
                        Dim InvisibleColumnCount As Integer = 0

                        For intclmn As Integer = 0 To grdDSRReport.Columns.Count - 1
                            If grdDSRReport.HeaderRow.Cells(intclmn).Visible = True Then
                                TotalWidth = TotalWidth + grdDSRReport.Columns(intclmn).ItemStyle.Width.Value
                            End If
                        Next
                        grdDSRReport.Width = TotalWidth
                        ' PnlShowUnhideColumns.Visible = True
                    Else
                        grdDSRReport.DataSource = Nothing
                        grdDSRReport.DataBind()
                        pnlPaging.Visible = False

                    End If
                Else
                    grdDSRReport.DataSource = Nothing
                    grdDSRReport.DataBind()
                    pnlPaging.Visible = False
                End If
            Else
                ' DSRLogSearch()
                grdDSRReport.DataSource = Nothing
                grdDSRReport.DataBind()
                pnlPaging.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            ModalLoading.Hide()
        End Try
    End Sub
    Private Sub ExportData()
        Session("DSREXPORTINPUTXML") = Nothing
        Session("NotShowColumnArray") = Nothing
        Try
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                lblError.Text = "Session is expired."
                Exit Sub
            End If
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objTarget As New AAMS.bizSales.bzAgencyTarget
            Dim ds As New DataSet
            Dim objSecurityXml As New XmlDocument

            ' If grdDSRReport.Rows.Count > 0 Then
            'objInputXml.LoadXml("<SL_SEARCH_LOG_DSR_INPUT> <DATEFROM/><DATETO/> <PENDING/><VISITSUBTYPE/><EMPLOYEEID/><RESP1A/><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/><LIMITED_TO_AOFFICE /> <LIMITED_TO_REGION></LIMITED_TO_REGION> <LIMITED_TO_OWNAAGENCY /> <LCODE></LCODE> <OFFICEID></OFFICEID> <AGENCY_NAME></AGENCY_NAME><AOFFICE></AOFFICE><REGION></REGION><ACATEGORY></ACATEGORY><MANAGERCALLS></MANAGERCALLS><VISITEDYES></VISITEDYES><VISITEDNO></VISITEDNO></SL_SEARCH_LOG_DSR_INPUT>")

            objInputXml.LoadXml("<SL_SEARCH_LOG_DSR_INPUT><DATEFROM /><DATETO /><PENDING /><VISITSUBTYPE /><EMPLOYEEID /><RESP1A /><PAGE_NO /><PAGE_SIZE /><SORT_BY /><DESC /><LIMITED_TO_AOFFICE /><LIMITED_TO_REGION></LIMITED_TO_REGION><LIMITED_TO_OWNAAGENCY /><LCODE></LCODE><OFFICEID></OFFICEID><AGENCY_NAME></AGENCY_NAME><AOFFICE></AOFFICE><REGION></REGION><ACATEGORY></ACATEGORY><MANAGERCALLS></MANAGERCALLS><VISITEDYES></VISITEDYES><VISITEDNO></VISITEDNO><GROUPDATA></GROUPDATA><CITYID></CITYID><COUNTRYID></COUNTRYID><SVR_REASONID></SVR_REASONID><DEPARTMENTIDS></DEPARTMENTIDS><CHAIN_CODE></CHAIN_CODE><STRATEGICTYPE></STRATEGICTYPE><STATUS></STATUS></SL_SEARCH_LOG_DSR_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("DATEFROM").InnerText = objeAAMS.ConvertTextDate(txtDateOfDSRF.Text)
            objInputXml.DocumentElement.SelectSingleNode("DATETO").InnerText = objeAAMS.ConvertTextDate(txtDateOfDSRT.Text)
            objInputXml.DocumentElement.SelectSingleNode("VISITSUBTYPE").InnerText = DlstVisitSubType.SelectedValue

            objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = TxtLCode.Text
            objInputXml.DocumentElement.SelectSingleNode("AGENCY_NAME").InnerText = txtAgencyName.Text
            objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerText = TxtOfficeID.Text



            If ChkPenIssue.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("PENDING").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("PENDING").InnerText = "FALSE"
            End If

            If (DlstAoffice.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = DlstAoffice.SelectedItem.Text
            End If

            If (drpRegion.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = drpRegion.SelectedItem.Text
            End If


            Dim strActg As String = ""
            For Each Item As ListItem In ChkLstAgencyCategory.Items
                If Item.Selected = True Then
                    If strActg.Trim.Length = 0 Then
                        strActg = Item.Text
                    Else
                        strActg = strActg + "," + Item.Text
                    End If
                End If
            Next

            If (strActg.Trim.Length > 0) Then
                objInputXml.DocumentElement.SelectSingleNode("ACATEGORY").InnerText = strActg
            End If

            'Dim strDepartment As String = ""
            'For Each Item As ListItem In ChkLstDepartment.Items
            '    If Item.Selected = True Then
            '        If strDepartment.Trim.Length = 0 Then
            '            strDepartment = Item.Value
            '        Else
            '            strDepartment = strDepartment + "," + Item.Value
            '        End If
            '    End If
            'Next
            'If (strDepartment.Trim.Length > 0) Then
            '    objInputXml.DocumentElement.SelectSingleNode("DEPARTMENTIDS").InnerText = strDepartment
            'End If
            If drpStatus.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("STATUS").InnerText = drpStatus.SelectedValue

            End If
           
            If chkGroupProductivity.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("GROUPDATA").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("GROUPDATA").InnerText = "FALSE"
            End If
            If DlstCity.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("CITYID").InnerText = DlstCountry.SelectedValue
            End If

            If DlstCountry.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("COUNTRYID").InnerText = DlstCountry.SelectedItem.Text
            End If
            If DlstRetReason.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("SVR_REASONID").InnerText = DlstRetReason.SelectedValue
            End If
            If drpStrategicCallType.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("STRATEGICTYPE").InnerText = drpStrategicCallType.SelectedValue

            End If
           
            objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = TxtChainCode.Text

            Dim objEmpXml As New XmlDocument
            Dim UserId As String
            objEmpXml.LoadXml(Session("Security"))
            UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()

            If Drp1ARes.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("RESP1A").InnerText = Drp1ARes.SelectedValue
            End If

            objInputXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = UserId

            If ChkManagerCallLog.checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("MANAGERCALLS").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("MANAGERCALLS").InnerText = "FALSE"
            End If


            If ChkY.Checked = True And ChkN.Checked = False Then
                'dv.RowFilter = "VISITED='" + "Y" + "'"
                objInputXml.DocumentElement.SelectSingleNode("VISITEDYES").InnerText = "TRUE"
                objInputXml.DocumentElement.SelectSingleNode("VISITEDNO").InnerText = "FALSE"
            End If
            If ChkN.Checked = True And ChkY.Checked = False Then
                'dv.RowFilter = "VISITED='" + "N" + "'"
                objInputXml.DocumentElement.SelectSingleNode("VISITEDYES").InnerText = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("VISITEDNO").InnerText = "TRUE"
            End If
            If ChkN.Checked = True And ChkY.Checked = True Then
                ' dv.RowFilter = "VISITED IN ('Y', 'N')"
                objInputXml.DocumentElement.SelectSingleNode("VISITEDYES").InnerText = "TRUE"
                objInputXml.DocumentElement.SelectSingleNode("VISITEDNO").InnerText = "TRUE"
            End If
            If ChkN.Checked = False And ChkY.Checked = False Then
                ' dv.RowFilter = "VISITED='" + "Y" + "'"
                objInputXml.DocumentElement.SelectSingleNode("VISITEDYES").InnerText = "TRUE"
                objInputXml.DocumentElement.SelectSingleNode("VISITEDNO").InnerText = "FALSE"
            End If



            Session("DSREXPORTINPUTXML") = objInputXml.OuterXml


            Dim NotShowColumnArray As New ArrayList
            '  DSRLOG ROWID='1' DSR_VISIT_ID='' PREDATE='12-May-2011'
            ' AGENCY_NAME='Abc' ADDRESS='A/21 hjsd fhjsd sdh sdffds sdf'
            ' LCODE='2' OFFICEID='DELBR0006' DESIGNATION='rt' PERSONMET=''
            ' PENDINGISSUES='' BCCOMMIT='2'
            ' BCMINSEGMENT='2' MANAGERID='33' MANAGERNAME='Admin' HODID=''
            ' HODNAME='' CITYID='' CITY_NAME='DELHI'
            ' CHAIN_CODE='2' A1DAILYMOTIVES='323' MIDT='3333' BIDT='33'
            ' VISITED='' REASON_REMARKS=''
            'OBJ_VISITCOUNT='' PLAN_VISIT_DONE='' UNPLAN_VISIT_DONE='' 
            'VISIT_TYPE_NAME='' DISCUSSION_ISSUE_REMARKS=''
            'COMPETITION_MKT_INFO_REMARKS='' STATUS_NAME='' FOLLOWUP_REMARKS='' 
            'PREV_REMARKS1=''
            'PREV_REMARKS1DATE='' PREV_REMARKS2DATE='' PREV_REMARKS2='' 
            'INTIME='11:10' OUTTIME='15:10'
            'GROUP_CATG_NAME='A' VISITCOUNT='2' LOGDATE='' RESP_NAME='' 
            'RESP_1A='24' UNPLANVISIT='FALSE'

            If ChkDSRCode.Checked = False Then
                NotShowColumnArray.Add("DSR_VISIT_ID")
            End If
            If ChkLcode.Checked = False Then
                NotShowColumnArray.Add("LCODE")
            End If
            If ChkChaincode.Checked = False Then
                NotShowColumnArray.Add("CHAIN_CODE")
            End If

            If ChkAddress.Checked = False Then
                NotShowColumnArray.Add("ADDRESS")
            End If





            If ChkCity.Checked = False Then
                NotShowColumnArray.Add("CITY_NAME")
            End If
            If ChkCateg.Checked = False Then
                NotShowColumnArray.Add("GROUP_CATG_NAME")
            End If
            If ChkMIDT.Checked = False Then
                NotShowColumnArray.Add("MIDT")
            End If
            If ChkBIDT.Checked = False Then
                NotShowColumnArray.Add("BIDT")
            End If
            If ChkBCommit.Checked = False Then
                NotShowColumnArray.Add("BCCOMMIT")
            End If
            If ChkMinSeg.Checked = False Then
                NotShowColumnArray.Add("BCMINSEGMENT")
            End If
            If ChkPastMotive.Checked = False Then
                NotShowColumnArray.Add("A1DAILYMOTIVES")
            End If
            If ChkPlanVisitDone.Checked = False Then
                NotShowColumnArray.Add("PLAN_VISIT_DONE")
            End If
            If ChkUnPlanVisitDone.Checked = False Then
                NotShowColumnArray.Add("UNPLAN_VISIT_DONE")
            End If
            If ChkVisitSubType.Checked = False Then
                NotShowColumnArray.Add("VISIT_TYPE_NAME")
            End If
            If ChkDesig.Checked = False Then
                NotShowColumnArray.Add("DESIGNATION")
            End If
            If ChkPersonMet.Checked = False Then
                NotShowColumnArray.Add("PERSONMET")
            End If

            If ChkPendingIssue.Checked = False Then
                NotShowColumnArray.Add("STATUS_NAME")
            End If
            If ChkLogDate.Checked = False Then
                NotShowColumnArray.Add("LOGDATE")
            End If
            If ChkAcBYManger.Checked = False Then
                NotShowColumnArray.Add("MANAGERNAME")
            End If

            If ChkAcBYRepManger.Checked = False Then
                NotShowColumnArray.Add("HODNAME")
            End If


            If ChkSalesObjVisit.Checked = False Then
                NotShowColumnArray.Add("OBJ_VISITCOUNT")
            End If

            If ChkJointCallremarks.Checked = False Then
                NotShowColumnArray.Add("JOINTCALLREMARKS")
            End If
            If ChkAcBycolleague.Checked = False Then
                NotShowColumnArray.Add("STAFFNAME")
            End If

            If ChkSegstarget.Checked = False Then
                NotShowColumnArray.Add("SEGSTARGET")
            End If

            If ChkAssignedTo.Checked = False Then
                ' SR_ASSIGNTO='' ST_ASSIGNTO=''
                NotShowColumnArray.Add("SR_ASSIGNTO")
                NotShowColumnArray.Add("ST_ASSIGNTO")
            End If

            If ChkVisittargets.Checked = False Then
                NotShowColumnArray.Add("VISITTARGET")
            End If
            If ChkReasonRemNotVisited.Checked = False Then
                NotShowColumnArray.Add("REASON_REMARKS")
            End If

            If ChkLoggedByManager.Checked = False Or ChkLoggedByManager.Visible = False Then
                NotShowColumnArray.Add("LOGMANAGERNAME")
            End If

            If ChkOfficeID.Checked = False Then
                NotShowColumnArray.Add("OFFICEID")
            End If
            If ChkSalesExec.Checked = False Then
                NotShowColumnArray.Add("RESP_NAME")
            End If
            If ChkVisitYesNo.Checked = False Then
                NotShowColumnArray.Add("VISITED")
            End If
            If ChkMarketInfo.Checked = False Then
                NotShowColumnArray.Add("COMPETITION_MKT_INFO_REMARKS")
            End If
            If ChkFollowupRem.Checked = False Then
                NotShowColumnArray.Add("FOLLOWUP_REMARKS")
            End If
            If ChkPrevRem1.Checked = False Then
                NotShowColumnArray.Add("PREV_REMARKS1")
            End If
            If ChkPrevRem2.Checked = False Then
                NotShowColumnArray.Add("PREV_REMARKS2")
            End If

            If ChkInOutTime.Checked = False Then
                NotShowColumnArray.Add("INTIME")
                NotShowColumnArray.Add("OUTTIME")
            End If

            If ChkVisitDate.Checked = False Then
                NotShowColumnArray.Add("DATE")
            End If
            'Start of  Explicit Hide Column
            NotShowColumnArray.Add("PENDINGISSUES")
            NotShowColumnArray.Add("MANAGER")
            NotShowColumnArray.Add("COLORCODE")
            'NotShowColumnArray.Add("VISIT_TYPE_NAME")
            'NotShowColumnArray.Add("DISCUSSION_ISSUE_REMARKS")
            'NotShowColumnArray.Add("COMPETITION_MKT_INFO_REMARKS")
            'NotShowColumnArray.Add("FOLLOWUP_REMARKS")
            'NotShowColumnArray.Add("PREV_REMARKS1")
            'NotShowColumnArray.Add("PREV_REMARKS2")
            'NotShowColumnArray.Add("INTIME")
            'NotShowColumnArray.Add("OUTTIME")
            ' NotShowColumnArray.Add("STATUS_NAME")

            'ST_DEPARTMENT()
            'SC_DEPARTMENT_SPECIFIC()
            'ST_TARGET_CLOSERDATE()
            'ST_CLOSERDATE()
            'ST_RETENTION_REASON()
            'ST_CPS()
            'ST_NEWCPS()
            'ST_A1APPROVED_NEW_DEAL()
            'ST_STR_SIGNON_DATE()
            'ST_STR_INSTALLATION_DATE()
            'ST_STR_PRODUCT()
            'ST_STR_REVENUE()
            'ST_STR_TARGET_SEG()
            'MIDT_LATEST()
            'BIDT_LATEST()


            If ChkMIDTLatest.Checked = False Then
                NotShowColumnArray.Add("MIDT_LATEST")
            End If
            If ChkBIDTLatest.Checked = False Then
                NotShowColumnArray.Add("BIDT_LATEST")
            End If
            If ChkDepartment.Checked = False Then
                NotShowColumnArray.Add("ST_DEPARTMENT")
            End If
            If ChkDeptSpecific.Checked = False Then
                NotShowColumnArray.Add("SC_DEPARTMENT_SPECIFIC")
            End If
            If ChkTarCloserDate.Checked = False Then
                NotShowColumnArray.Add("ST_TARGET_CLOSERDATE")
            End If
            If ChkCloserDate.Checked = False Then
                NotShowColumnArray.Add("ST_CLOSERDATE")
            End If
            If ChkRetReason.Checked = False Then
                NotShowColumnArray.Add("ST_RETENTION_REASON")
            End If
            If ChkCPS.Checked = False Then
                NotShowColumnArray.Add("ST_CPS")
            End If
            If ChkNewCPS.Checked = False Then
                NotShowColumnArray.Add("ST_NEWCPS")
            End If
            If Chk1aAppNewDeal.Checked = False Then
                NotShowColumnArray.Add("ST_A1APPROVED_NEW_DEAL")
            End If

            If ChkSignOnConvDate.Checked = False Then
                NotShowColumnArray.Add("ST_STR_SIGNON_DATE")
            End If
            If ChkInsDate.Checked = False Then
                NotShowColumnArray.Add("ST_STR_INSTALLATION_DATE")
            End If
            If ChkProdName.Checked = False Then
                NotShowColumnArray.Add("ST_STR_PRODUCT")
            End If
            If ChkRevenue.Checked = False Then
                NotShowColumnArray.Add("ST_STR_REVENUE")
            End If

            If ChkTargerSegPer.Checked = False Then
                NotShowColumnArray.Add("ST_STR_TARGET_SEG")
            End If

            'End of  Explicit Hide Column



            Session("NotShowColumnArray") = NotShowColumnArray

            Response.Redirect("SASR_AtargetExport.aspx?EXPORTDSRDATA=DSR", False)

            'Else

            'End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
   
    Protected Sub Btnexp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnexp.Click
        ExportData()
    End Sub


#Region "CheckSecurity()::This method is used for security check."
    Private Sub CheckSecurity()
        Try

            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Account Management']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Account Management']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If

                    If strBuilder(4) = "0" Then
                        BtnExport.Enabled = False
                    Else
                        BtnExport.Enabled = True
                    End If

                    If objSecurityXml.DocumentElement.SelectSingleNode("Manager").InnerText = "True" Then
                        btnManagerVisitLog.Visible = True
                        ChkLoggedByManager.Visible = True
                        ChkManagerCallLog.Visible = True
                    Else
                        btnManagerVisitLog.Visible = False
                        ChkLoggedByManager.Visible = False
                        ChkManagerCallLog.Visible = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            If Now.Day <= 7 Or Now.Day = DateTime.DaysInMonth(Now.Year, Now.Month - 1) Then
                '   BtnSave.Enabled = True
            Else
                ' BtnSave.Enabled = False
            End If

            '@ Start of Right for seenh the BIDT/MIDt/Daily Booking data
            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        ChkBIDT.Visible = False
                        ChkBIDT.Checked = False
                        ChkBIDTLatest.Visible = False
                        ChkBIDTLatest.Checked = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DailyBookings']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DailyBookings']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        ChkPastMotive.Visible = False
                        ChkPastMotive.Checked = False
                      
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If


            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        ChkMIDT.Visible = False
                        ChkMIDT.Checked = False
                        ChkMIDTLatest.Visible = False
                        ChkMIDTLatest.Checked = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            '@ End  of Right for seenh the BIDT/MIDt/Daily Booking data
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub Sc1_AsyncPostBackError(ByVal sender As Object, ByVal e As System.Web.UI.AsyncPostBackErrorEventArgs) Handles Sc1.AsyncPostBackError


        ''Sc1.AsyncPostBackErrorMessage = "<B>" & _
        ''         e.Exception.Message & "</B><Br />" & _
        ''         e.Exception.StackTrace.Replace(vbCrLf, "<br />")
        If (e.Exception.Data("ExtraInfo") <> Nothing) Then
            Sc1.AsyncPostBackErrorMessage = _
               e.Exception.Message & _
               e.Exception.Data("ExtraInfo").ToString()
        Else
            Sc1.AsyncPostBackErrorMessage = _
               "An unspecified error occurred."
        End If

    End Sub
    Public Sub fillSalesName()
        Drp1ARes.Items.Clear()
        Dim objSales As New AAMS.bizTravelAgency.bzAgencyTarget
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objInputXml, objOutputXml As New XmlDocument
        Try
            Dim objEmpXml As New XmlDocument
            Dim UserId As String
            objEmpXml.LoadXml(Session("Security"))
            UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
            objInputXml.LoadXml("<PR_AGENCYEMPLIST_INPUT><City_Id></City_Id></PR_AGENCYEMPLIST_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("City_Id").InnerText = ""
            objOutputXml = objSales.GetAgencyEmpList(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                Drp1ARes.DataSource = ds.Tables("TARGET")
                Drp1ARes.DataTextField = "SalesManName"
                Drp1ARes.DataValueField = "SalesManId"
                Drp1ARes.DataBind()
            End If
            Drp1ARes.Items.Insert(0, New ListItem("--All--", ""))

           
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub

    Private Sub FillAgenctCategory()
        Dim objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzAgencyType As New AAMS.bizTravelAgency.bzAgencyType
        Try
            objOutputXml = New XmlDocument
            objOutputXml = objbzAgencyType.List
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                ChkLstAgencyCategory.DataSource = ds.Tables("AGENCYTYPE")
                ChkLstAgencyCategory.DataTextField = "Agency_Type_Name"
                ChkLstAgencyCategory.DataValueField = "AgencyTypeId"
                ChkLstAgencyCategory.DataBind()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Callpostdata()
        ChkLcode.Attributes.Add("onclick", "return PostData();")
        ChkChaincode.Attributes.Add("onclick", "return PostData();")
        ChkCateg.Attributes.Add("onclick", "return PostData();")
        ChkCity.Attributes.Add("onclick", "return PostData();")


        ChkAddress.Attributes.Add("onclick", "return PostData();")
        ChkSalesObjVisit.Attributes.Add("onclick", "return PostData();")
        ChkSegstarget.Attributes.Add("onclick", "return PostData();")
        ChkDesig.Attributes.Add("onclick", "return PostData();")

        ChkMinSeg.Attributes.Add("onclick", "return PostData();")
        ChkBCommit.Attributes.Add("onclick", "return PostData();")
        ChkPastMotive.Attributes.Add("onclick", "return PostData();")
        ChkMIDT.Attributes.Add("onclick", "return PostData();")
        ChkBIDT.Attributes.Add("onclick", "return PostData();")
        ChkLogDate.Attributes.Add("onclick", "return PostData();")
        ChkPlanVisitDone.Attributes.Add("onclick", "return PostData();")
        ChkUnPlanVisitDone.Attributes.Add("onclick", "return PostData();")
        ChkVisitSubType.Attributes.Add("onclick", "return PostData();")
        ChkAssignedTo.Attributes.Add("onclick", "return PostData();")
        ChkAcBycolleague.Attributes.Add("onclick", "return PostData();")
        ChkPersonMet.Attributes.Add("onclick", "return PostData();")
        ChkAcBYManger.Attributes.Add("onclick", "return PostData();")
        ChkAcBYRepManger.Attributes.Add("onclick", "return PostData();")
        ChkJointCallremarks.Attributes.Add("onclick", "return PostData();")
        ChkReasonRemNotVisited.Attributes.Add("onclick", "return PostData();")
        ChkLoggedByManager.Attributes.Add("onclick", "return PostData();")
        ChkPendingIssue.Attributes.Add("onclick", "return PostData();")
        ChkVisittargets.Attributes.Add("onclick", "return PostData();")
        ChkOfficeID.Attributes.Add("onclick", "return PostData();")
        ChkSalesExec.Attributes.Add("onclick", "return PostData();")
        ChkVisitYesNo.Attributes.Add("onclick", "return PostData();")

        ChkMarketInfo.Attributes.Add("onclick", "return PostData();")
        ChkFollowupRem.Attributes.Add("onclick", "return PostData();")
        ChkPrevRem1.Attributes.Add("onclick", "return PostData();")
        ChkPrevRem2.Attributes.Add("onclick", "return PostData();")
        ChkInOutTime.Attributes.Add("onclick", "return PostData();")
        ChkVisitDate.Attributes.Add("onclick", "return PostData();")
        ChkDSRCode.Attributes.Add("onclick", "return PostData();")

        ChkMIDTLatest.Attributes.Add("onclick", "return PostData();")
        ChkBIDTLatest.Attributes.Add("onclick", "return PostData();")
        ChkDepartment.Attributes.Add("onclick", "return PostData();")
        ChkDeptSpecific.Attributes.Add("onclick", "return PostData();")
        ChkTarCloserDate.Attributes.Add("onclick", "return PostData();")
        ChkCloserDate.Attributes.Add("onclick", "return PostData();")
        ChkRetReason.Attributes.Add("onclick", "return PostData();")
        Chk1aAppNewDeal.Attributes.Add("onclick", "return PostData();")
        ChkNewCPS.Attributes.Add("onclick", "return PostData();")
        ChkCPS.Attributes.Add("onclick", "return PostData();")
        ChkSignOnConvDate.Attributes.Add("onclick", "return PostData();")
        ChkTargerSegPer.Attributes.Add("onclick", "return PostData();")
        ChkProdName.Attributes.Add("onclick", "return PostData();")
        ChkRevenue.Attributes.Add("onclick", "return PostData();")
        ChkInsDate.Attributes.Add("onclick", "return PostData();")

    End Sub
    Protected Sub btnManagerVisitLog_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("../Sales/SASR_DSRLogging.aspx?IsManager=True")
    End Sub

    
    
    Protected Sub btnUp_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            If hdAdvanceSearch.Value.Trim = "0" Then

                btnUp.ImageUrl = "../images/down.jpg"
                PnlShowUnhideColumns.Visible = False
                hdAdvanceSearch.Value = "1"
            Else
                btnUp.ImageUrl = "../images/up.jpg"
                PnlShowUnhideColumns.Visible = True
                hdAdvanceSearch.Value = "0"
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkAdvance_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If hdAdvanceSearch.Value.Trim = "0" Then

                btnUp.ImageUrl = "../images/down.jpg"
                PnlShowUnhideColumns.Visible = False
                hdAdvanceSearch.Value = "1"
            Else
                btnUp.ImageUrl = "../images/up.jpg"
                PnlShowUnhideColumns.Visible = True
                hdAdvanceSearch.Value = "0"
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub FillDepartment()
        'Dim objOutputXml As New XmlDocument
        'Dim objXmlReader As XmlNodeReader
        'Dim ds As New DataSet
        'Dim objbzDepartment As New AAMS.bizMaster.bzDepartment
        'Try
        '    objOutputXml = New XmlDocument
        '    objOutputXml = objbzDepartment.List
        '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
        '        objXmlReader = New XmlNodeReader(objOutputXml)
        '        ds.ReadXml(objXmlReader)
        '        ChkLstDepartment.DataSource = ds.Tables("DEPARTMENT")
        '        ChkLstDepartment.DataTextField = "Department_Name"
        '        ChkLstDepartment.DataValueField = "DepartmentID"
        '        ChkLstDepartment.DataBind()
        '    End If
        'Catch ex As Exception
        'End Try
    End Sub

    Public Sub fillStatus()
        drpStatus.Items.Clear()
        Dim objbzDSRAction As New AAMS.bizSales.bzDSRAction
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objInputXml, objOutputXml As New XmlDocument
        drpStatus.Items.Clear()
        Try
            objOutputXml = objbzDSRAction.ListDSRActionStatus()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                Dim finalOutputxml As New XmlDocument
                finalOutputxml.LoadXml("<SL_LIST_DSR_ACTION_STATUS_OUTPUT><Errors Status='FALSE'>  <Error Code='' Description='' />   </Errors>   </SL_LIST_DSR_ACTION_STATUS_OUTPUT>")

                If DlstVisitSubType.SelectedValue = "1" Then
                    For Each objxmlnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("STATUS")
                        If objxmlnode.Attributes("STATUSID").Value.Trim("|")(0) = "1" Then 'Service
                            finalOutputxml.DocumentElement.AppendChild(finalOutputxml.ImportNode(objxmlnode, True))
                        End If
                    Next
                ElseIf DlstVisitSubType.SelectedValue = "2" Then
                    If drpStrategicCallType.SelectedValue = "1" Then
                        For Each objxmlnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("STATUS")
                            If objxmlnode.Attributes("STATUSID").Value.Trim("|")(0) = "2" Then  'Target
                                finalOutputxml.DocumentElement.AppendChild(finalOutputxml.ImportNode(objxmlnode, True))
                            End If
                        Next
                    ElseIf drpStrategicCallType.SelectedValue = "2" Then
                        For Each objxmlnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("STATUS")
                            If objxmlnode.Attributes("STATUSID").Value.Trim("|")(0) = "3" Then 'Retention
                                finalOutputxml.DocumentElement.AppendChild(finalOutputxml.ImportNode(objxmlnode, True))
                            End If
                        Next
                    ElseIf drpStrategicCallType.SelectedValue = "3" Then
                        For Each objxmlnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("STATUS")
                            If objxmlnode.Attributes("STATUSID").Value.Trim("|")(0) = "4" Then 'Air Non Air
                                finalOutputxml.DocumentElement.AppendChild(finalOutputxml.ImportNode(objxmlnode, True))
                            End If
                        Next
                    Else
                        For Each objxmlnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("STATUS")
                            If objxmlnode.Attributes("STATUSID").Value.Trim("|")(0) = "2" Or objxmlnode.Attributes("STATUSID").Value.Trim("|")(0) = "3" Or objxmlnode.Attributes("STATUSID").Value.Trim("|")(0) = "4" Then
                                finalOutputxml.DocumentElement.AppendChild(finalOutputxml.ImportNode(objxmlnode, True))
                            End If
                        Next
                    End If

                Else
                    finalOutputxml.LoadXml(objOutputXml.OuterXml)
                End If


                If finalOutputxml.DocumentElement.SelectNodes("STATUS").Count > 0 Then

                    objXmlReader = New XmlNodeReader(finalOutputxml)
                    ds.ReadXml(objXmlReader)
                    drpStatus.DataSource = ds.Tables("STATUS")
                    drpStatus.DataTextField = "STATUS_NAME"
                    drpStatus.DataValueField = "STATUSID"
                    drpStatus.DataBind()
                Else
                    drpStatus.DataSource = Nothing
                    drpStatus.DataBind()
                End If
            Else
                drpStatus.DataSource = Nothing
                drpStatus.DataBind()
            End If
            drpStatus.Items.Insert(0, New ListItem("--All--", ""))


        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub

    Protected Sub DlstVisitSubType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DlstVisitSubType.SelectedIndexChanged
        fillStatus()
    End Sub

    Protected Sub drpStrategicCallType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpStrategicCallType.SelectedIndexChanged
        fillStatus()
    End Sub

  
   
       
 
End Class

