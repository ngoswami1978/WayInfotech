Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Partial Class BOHelpDesk_HDSR_Ptr
    Inherits System.Web.UI.Page
    Dim objEaams As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objeAAMSMessage As New eAAMSMessage
    Dim imgDown As New Image
    Dim imgUp As New Image
    Dim objED As New EncyrptDeCyrpt

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = Request.Url.ToString
            objEaams.ExpirePageCache()


            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgDown.ImageUrl = "~/Images/Sortdown.gif"


            ' btnNew.Attributes.Add("onclick", "return InsertPtrAssignee();")
            drp1aOffice.Attributes.Add("onkeyup", "return gotop('drp1aOffice')")
            drpAssignedTo.Attributes.Add("onkeyup", "return gotop('drpAssignedTo')")
            drpFollowup.Attributes.Add("onkeyup", "return gotop('drpFollowup')")
            drpPtrStatus.Attributes.Add("onkeyup", "return gotop('drpPtrStatus')")
            drpPtrTypCat.Attributes.Add("onkeyup", "return gotop('drpPtrTypCat')")
            drpPtrType.Attributes.Add("onkeyup", "return gotop('drpPtrType')")
            drpSeverity.Attributes.Add("onkeyup", "return gotop('drpSeverity')")


            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objEaams.CheckSession())
                Exit Sub
            End If
            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO PTR']").Count <> 0 Then
                    strBuilder = objEaams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO PTR']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnSearch.Enabled = False
                    End If
                    If strBuilder(4) = "0" Then
                        btnExport.Enabled = False
                    End If
                End If
            Else
                strBuilder = objEaams.SecurityCheck(31)
            End If

            '*****************Delete Functionality
            'If Request.QueryString("Action") IsNot Nothing Then
            '    If Request.QueryString("Action").Split("|").GetValue(0).ToString.ToUpper = "D" Then
            '        HDPtrDelelete(Request.QueryString("Action").Split("|").GetValue(1).ToString.Trim())
            '        GetSearchResult()
            '        lblError.Text = objeAAMSMessage.messDelete
            '    End If
            'End If
            '*****************End of Delete 
            If hdDeleteFlag.Value <> "" Then
                HDPtrDelelete(hdDeleteFlag.Value.Trim())
                Dim CurrentPage As Integer = CInt(IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue))
                If txtRecordOnCurrReco.Text = "1" Then
                    If CurrentPage - 1 > 0 Then
                        ddlPageNumber.SelectedValue = (CurrentPage - 1).ToString
                    Else
                        ddlPageNumber.SelectedValue = "1"
                    End If
                End If
                GetSearchResult()
            End If



            If Not Page.IsPostBack Then

                btnSearch.Attributes.Add("onClick", "return ValidateForm();")
                btnExport.Attributes.Add("onClick", "return ValidateForm();")
                BindDropDowns()

                Dim strDate As String = DateTime.Now.ToShortDateString()
                strDate = Format(Convert.ToDateTime(strDate), "dd/MM/yyyy")
                txtOpenDtTo.Text = strDate
                strDate = DateTime.Now.AddDays(-7).ToShortDateString() 'Format(DateAdd(DateInterval.Day, 7, Convert.ToDateTime(strDate)), "dd/MM/yyyy")
                strDate = Format(Convert.ToDateTime(strDate), "dd/MM/yyyy") 'DateAdd(DateInterval.Day, -7, Convert.ToDateTime(strDate))
                txtOpenDtFrm.Text = strDate
            End If

            '   Checking Permission For Own Office start.
            If objEaams.Limited_To_Aoffice(Session("Security")) <> "" Then
                drp1aOffice.SelectedValue = objEaams.Limited_To_Aoffice(Session("Security"))
                drp1aOffice.Enabled = False
            End If

            '   Checking Permission For Own Office end.

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub BindDropDowns()
        Try
            objEaams.BindDropDown(drp1aOffice, "AOFFICE", True, 3)
            objEaams.BindDropDown(drpFollowup, "BOHDFOLLOWUP", True, 3)
            'objEaams.BindDropDown(drpPtrStatus, "BOHDPTRCALLSTATUS", True, 3)
            objEaams.BindDropDown(drpPtrStatus, "BOHDPTRSTATUS", True, 3)
            objEaams.BindDropDown(drpPtrTypCat, "BOHDPTRTYPECAT", True, 3)
            objEaams.BindDropDown(drpPtrType, "BOHDPTRTYPE", True, 3)
            objEaams.BindDropDown(drpSeverity, "BOHDPTRSEVERITY", True, 3)
            objEaams.BindDropDown(drpAssignedTo, "BOHDASSIGNEDTO", True, 3)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            GetSearchResult()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub GetSearchResult()
        '   <HD_SEARCH_PTR_INPUT> 
        '	<LCODE></LCODE>
        '	<NAME></NAME>	
        '	<GROUPDATA></GROUPDATA>	
        '	<HD_RE_ID></HD_RE_ID>
        '	<HD_PTR_REF></HD_PTR_REF>
        '	<HD_PTR_TITLE></HD_PTR_TITLE>
        '	<HD_STATUS_ID></HD_STATUS_ID>
        '	<HD_PTR_SEV_ID></HD_PTR_SEV_ID>
        '	<HD_PTR_FOLLOWUP_ID></HD_PTR_FOLLOWUP_ID>	
        '	<LOGGEDBYID></LOGGEDBYID>
        '	<ASSIGNEEID></ASSIGNEEID>
        '	<ASSIGNED_DATE></ASSIGNED_DATE>	
        '	<HD_PTR_TYPE_ID></HD_PTR_TYPE_ID>	
        '	<HD_PTR_TYCAT_ID></HD_PTR_TYCAT_ID>
        '	<AOFFICE></AOFFICE>
        '	<HD_PTR_OPENDATE_FROM></HD_PTR_OPENDATE_FROM>
        '	<HD_PTR_OPENDATE_TO></HD_PTR_OPENDATE_TO>
        '	<HD_PTR_CLOSEDATE_FROM></HD_PTR_CLOSEDATE_FROM>
        '	<HD_PTR_CLOSEDATE_TO></HD_PTR_CLOSEDATE_TO>
        '	<RESP_1A></RESP_1A>
        '	<LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY>
        '</HD_SEARCH_PTR_INPUT>


        '<HD_SEARCH_PTR_OUTPUT>
        '	<PTR HD_PTR_ID='' HD_RE_ID='' HD_PTR_REF='' NAME='' ADDRESS='' HD_PTR_TITLE='' HD_STATUS_NAME='' HD_PTR_FOLLOWUP_NAME='' HD_PTR_SEV_NAME='' HD_PTR_TYPE_NAME='' Employee_Name='' HD_PTR_OPENDATE='' HD_PTR_CLOSEDATE='' ASSIGNEE_NAME='' ASSIGNED_DATE=''/>
        '	  <Errors Status=''>
        '        	<Error Code='' Description='' />
        '           </Errors>
        '</HD_SEARCH_PTR_OUTPUT>
        Try
            Dim objHdInputX, objHdOutputX As New XmlDocument
            Dim objReader As XmlNodeReader
            Dim dSet As New DataSet
            objHdInputX.LoadXml("<HD_SEARCH_PTR_INPUT><LCODE></LCODE><NAME></NAME><GROUPDATA></GROUPDATA><HD_RE_ID></HD_RE_ID><HD_PTR_REF></HD_PTR_REF><HD_PTR_TITLE></HD_PTR_TITLE><HD_STATUS_ID></HD_STATUS_ID><HD_PTR_SEV_ID></HD_PTR_SEV_ID><HD_PTR_FOLLOWUP_ID></HD_PTR_FOLLOWUP_ID><LOGGEDBY></LOGGEDBY><ASSIGNEEID></ASSIGNEEID><ASSIGNED_DATE></ASSIGNED_DATE><HD_PTR_TYPE_ID></HD_PTR_TYPE_ID><HD_PTR_TYCAT_ID></HD_PTR_TYCAT_ID><AOFFICE></AOFFICE><HD_PTR_OPENDATE_FROM></HD_PTR_OPENDATE_FROM><HD_PTR_OPENDATE_TO></HD_PTR_OPENDATE_TO><HD_PTR_CLOSEDATE_FROM></HD_PTR_CLOSEDATE_FROM><HD_PTR_CLOSEDATE_TO></HD_PTR_CLOSEDATE_TO><RESP_1A></RESP_1A><LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY><LIMITED_TO_AOFFICE></LIMITED_TO_AOFFICE><LIMITED_TO_REGION></LIMITED_TO_REGION> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCH_PTR_INPUT>")
            With objHdInputX.DocumentElement
                If txtAgencyName.Text.Trim().Length <> 0 Then
                    .SelectSingleNode("LCODE").InnerText = hdLcode.Value.Trim()
                    .SelectSingleNode("NAME").InnerText = txtAgencyName.Text.Trim()
                End If
                'If chkWholeGroup.Checked Then
                '    .SelectSingleNode("GROUPDATA").InnerText = "TRUE"
                'Else
                '    .SelectSingleNode("GROUPDATA").InnerText = "FALSE"
                'End If
                .SelectSingleNode("HD_RE_ID").InnerText = txtLtrNo.Text.Trim()
                .SelectSingleNode("HD_PTR_REF").InnerText = txtPtrNo.Text.Trim()
                .SelectSingleNode("HD_PTR_TITLE").InnerText = txtPtrTitle.Text.Trim()
                If drpPtrStatus.SelectedIndex <> 0 Then
                    .SelectSingleNode("HD_STATUS_ID").InnerText = drpPtrStatus.SelectedValue
                End If
                If drpSeverity.SelectedIndex <> 0 Then
                    .SelectSingleNode("HD_PTR_SEV_ID").InnerText = drpSeverity.SelectedValue
                End If
                If drpFollowup.SelectedIndex <> 0 Then
                    .SelectSingleNode("HD_PTR_FOLLOWUP_ID").InnerText = drpFollowup.SelectedValue
                End If



                .SelectSingleNode("LOGGEDBY").InnerText = txtPendingWith.Text.Trim()


                '.SelectSingleNode("LOGGEDBYID").InnerText = "" 'Need for Discussion

                If drpAssignedTo.SelectedIndex <> 0 Then
                    .SelectSingleNode("ASSIGNEEID").InnerText = drpAssignedTo.SelectedValue
                End If

                .SelectSingleNode("ASSIGNED_DATE").InnerText = objEaams.ConvertTextDate(txtAssignedDt.Text.Trim())

                If drpPtrType.SelectedIndex <> 0 Then
                    .SelectSingleNode("HD_PTR_TYPE_ID").InnerText = drpPtrType.SelectedValue.Trim()
                End If

                If drpPtrTypCat.SelectedIndex <> 0 Then
                    .SelectSingleNode("HD_PTR_TYCAT_ID").InnerText = drpPtrTypCat.SelectedValue
                End If
                If drp1aOffice.SelectedIndex <> 0 Then
                    .SelectSingleNode("AOFFICE").InnerText = drp1aOffice.SelectedValue.Trim()
                End If

                .SelectSingleNode("HD_PTR_OPENDATE_FROM").InnerText = objEaams.ConvertTextDate(txtOpenDtFrm.Text.Trim())
                .SelectSingleNode("HD_PTR_OPENDATE_TO").InnerText = objEaams.ConvertTextDate(txtOpenDtTo.Text.Trim())
                .SelectSingleNode("HD_PTR_CLOSEDATE_FROM").InnerText = objEaams.ConvertTextDate(txtCloseDtFrm.Text.Trim())
                .SelectSingleNode("HD_PTR_CLOSEDATE_TO").InnerText = objEaams.ConvertTextDate(txtCloseDtTo.Text.Trim())
                'Following Statement is Written for 1A Responsiblity
                Dim str As String()
                str = Session("LoginSession").ToString().Split("|")
                .SelectSingleNode("RESP_1A").InnerText = str(0)

                'Following Statement is written for Limited to own Agency
                Dim xDoc As New XmlDocument
                xDoc.LoadXml(Session("Security"))

                .SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = xDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText  'Login time u find Need for discussion

                If xDoc.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText.ToUpper = "TRUE" Then
                    .SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = xDoc.DocumentElement.SelectSingleNode("Aoffice").InnerText  'Login time u find Need for discussion
                End If

                If xDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper() = "TRUE" Then
                    .SelectSingleNode("LIMITED_TO_REGION").InnerText = xDoc.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText   'Login time u find Need for discussion
                End If

            End With
            Dim objHdPtrSearch As New AAMS.bizBOHelpDesk.bzPTR


            'Start CODE for sorting and paging



            If ViewState("PrevSearching") Is Nothing Then
                objHdInputX.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
            Else
                Dim objTempInputXml As New XmlDocument
                Dim objNodeList As XmlNodeList

                objTempInputXml.LoadXml(ViewState("PrevSearching"))
                objNodeList = objTempInputXml.DocumentElement.ChildNodes
                objHdInputX.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                For Each objNode As XmlNode In objNodeList
                    If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                        If objNode.InnerText <> objHdInputX.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                            objHdInputX.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                            ddlPageNumber.SelectedValue = "1"
                        End If
                    End If
                Next
            End If


            objHdInputX.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString


            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "HD_PTR_REF"
                objHdInputX.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "HD_PTR_REF" '"LOCATION_CODE"
            Else
                objHdInputX.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objHdInputX.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objHdInputX.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            'End Code for paging and sorting

            'Here Back end Method Call
            objHdOutputX = objHdPtrSearch.Search(objHdInputX)
            ' objHdOutputX.LoadXml("<HD_SEARCH_PTR_OUTPUT><PTR HD_PTR_ID='1' HD_RE_ID='10002' HD_PTR_REF='43534' NAME='Test Ptre' ADDRESS='Test Address' HD_PTR_TITLE='Test Title' HD_STATUS_NAME='Ok' HD_PTR_FOLLOWUP_NAME='Test Follow' HD_PTR_SEV_NAME='sdfsd' HD_PTR_TYPE_NAME='sdfsdf' Employee_Name='' HD_PTR_OPENDATE='' HD_PTR_CLOSEDATE='20000202' ASSIGNEE_NAME='Mukesh' ASSIGNED_DATE='20000202'/><Errors Status='FALSE'><Error Code='' Description='' /></Errors></HD_SEARCH_PTR_OUTPUT>")
            If objHdOutputX.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objReader = New XmlNodeReader(objHdOutputX)
                dSet.ReadXml(objReader)
                ViewState("PrevSearching") = objHdInputX.OuterXml
                lblError.Text = ""
                grdvPtr.DataSource = dSet.Tables("PTR")
                grdvPtr.DataBind()
                txtRecordOnCurrReco.Text = grdvPtr.Rows.Count.ToString()
                PagingCommon(objHdOutputX)
                Dim intcol As Integer = GetSortColumnIndex(grdvPtr)
                If ViewState("Desc") = "FALSE" Then
                    grdvPtr.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "TRUE" Then
                    grdvPtr.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If

                ' lblError.Text = ""
            Else
                grdvPtr.DataSource = Nothing
                grdvPtr.DataBind()
                txtRecordOnCurrReco.Text = "0"
                pnlPaging.Visible = False
                lblError.Text = objHdOutputX.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub grdvPtr_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvPtr.RowDataBound
        Try

            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim hdPtrID As New HiddenField
            Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            linkEdit = e.Row.FindControl("linkEdit")
            Dim linkDelete As LinkButton
            linkDelete = e.Row.FindControl("linkDelete")
            hdPtrID = e.Row.FindControl("hdPtrID")
            Dim objeaams As New eAAMS
            'If CType(e.Row.FindControl("lblOpenDt"), Label).Text.Trim().Length <> 0 Then
            '    CType(e.Row.FindControl("lblOpenDt"), Label).Text = objeaams.ConvertDateBlank(CType(e.Row.FindControl("lblOpenDt"), Label).Text.Trim())
            'End If
            'If CType(e.Row.FindControl("lblClosenDt"), Label).Text.Trim().Length <> 0 Then
            '    CType(e.Row.FindControl("lblClosenDt"), Label).Text = objeaams.ConvertDateBlank(CType(e.Row.FindControl("lblClosenDt"), Label).Text.Trim())
            'End If

            'If CType(e.Row.FindControl("lblAssigneeDt"), Label).Text.Trim().Length <> 0 Then
            '    CType(e.Row.FindControl("lblAssigneeDt"), Label).Text = objeaams.ConvertDateBlank(CType(e.Row.FindControl("lblAssigneeDt"), Label).Text.Trim())
            'End If


            'For Select Section 
            Dim lnkSelects As New System.Web.UI.WebControls.LinkButton
            lnkSelects = e.Row.FindControl("lnkSelect")

            If (Request.QueryString("PopUp")) Is Nothing Then
                lnkSelects.Visible = False
            Else
                lnkSelects.Visible = True
                lnkSelects.Attributes.Add("OnClick", "return SelectFunction('" & lnkSelects.CommandArgument & "');")
            End If

            'lnkSelects.Attributes.Add("OnClick", "return SelectFunction('" & lnkSelects.CommandArgument & "');")

            'End of Select Section


            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO PTR']").Count <> 0 Then
                    strBuilder = objeaams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO PTR']").Attributes("Value").Value)
                End If
                If strBuilder(3) = "0" Then
                    linkDelete.Enabled = False '.Disabled = True
                Else
                    linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdPtrID.Value & "');")
                End If
                'If strBuilder(2) = "0" Then
                '    linkEdit.Disabled = True
                'Else
                '    linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdPtrID.Value & "');")

                'End If
                '  linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdPtrID.Value) & "');")
                linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdPtrID.Value & "');")
            Else
                'linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdPtrID.Value) & "' );")
                linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdPtrID.Value & "' );")
                linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdPtrID.Value & "');")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub HDPtrDelelete(ByVal PtrID As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Try
            Dim objhdPtrDel As New AAMS.bizBOHelpDesk.bzPTR

            objInputXml.LoadXml("<HD_DELETE_PTR_INPUT><HD_PTR_ID></HD_PTR_ID></HD_DELETE_PTR_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("HD_PTR_ID").InnerText = PtrID
            hdDeleteFlag.Value = ""
            'Call a function
            objOutputXml = objhdPtrDel.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messDelete
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            txtAgencyName.Text = ""
            txtAssignedDt.Text = ""
            txtCloseDtFrm.Text = ""
            txtCloseDtTo.Text = ""
            txtLtrNo.Text = ""
            txtOpenDtFrm.Text = ""
            txtOpenDtTo.Text = ""
            txtPendingWith.Text = ""
            txtPtrNo.Text = ""
            hdLcode.Value = ""
            drp1aOffice.SelectedIndex = 0
            drpAssignedTo.SelectedIndex = 0
            drpFollowup.SelectedIndex = 0
            drpPtrStatus.SelectedIndex = 0
            drpPtrTypCat.SelectedIndex = 0
            drpPtrType.SelectedIndex = 0
            drpSeverity.SelectedIndex = 0

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvPtr_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvPtr.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

#Region "Sort Function"
    Sub SortCall(ByVal SortName As String)

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
        GetSearchResult()
    End Sub
#End Region
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            GetSearchResult()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            GetSearchResult()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            GetSearchResult()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub PagingCommon(ByVal objOutputXml As XmlDocument)
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
        txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
    End Sub

    Private Function GetSortColumnIndex(ByVal grd As GridView) As Int16
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Return grd.Columns.IndexOf(field)
            End If
        Next
        Return 0
    End Function

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            Dim objHdInputX, objHdOutputX As New XmlDocument
            Dim objReader As XmlNodeReader
            Dim dSet As New DataSet
            Dim objExport As New ExportExcel
            Dim intArray(13) As Integer
            Dim strArray(13) As String
            objHdInputX.LoadXml("<HD_SEARCH_PTR_INPUT><LCODE></LCODE><NAME></NAME><GROUPDATA></GROUPDATA><HD_RE_ID></HD_RE_ID><HD_PTR_REF></HD_PTR_REF><HD_PTR_TITLE></HD_PTR_TITLE><HD_STATUS_ID></HD_STATUS_ID><HD_PTR_SEV_ID></HD_PTR_SEV_ID><HD_PTR_FOLLOWUP_ID></HD_PTR_FOLLOWUP_ID><LOGGEDBY></LOGGEDBY><ASSIGNEEID></ASSIGNEEID><ASSIGNED_DATE></ASSIGNED_DATE><HD_PTR_TYPE_ID></HD_PTR_TYPE_ID><HD_PTR_TYCAT_ID></HD_PTR_TYCAT_ID><AOFFICE></AOFFICE><HD_PTR_OPENDATE_FROM></HD_PTR_OPENDATE_FROM><HD_PTR_OPENDATE_TO></HD_PTR_OPENDATE_TO><HD_PTR_CLOSEDATE_FROM></HD_PTR_CLOSEDATE_FROM><HD_PTR_CLOSEDATE_TO></HD_PTR_CLOSEDATE_TO><RESP_1A></RESP_1A><LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY><LIMITED_TO_AOFFICE></LIMITED_TO_AOFFICE><LIMITED_TO_REGION></LIMITED_TO_REGION> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCH_PTR_INPUT>")
            With objHdInputX.DocumentElement
                If txtAgencyName.Text.Trim().Length <> 0 Then
                    .SelectSingleNode("LCODE").InnerText = hdLcode.Value.Trim()
                    .SelectSingleNode("NAME").InnerText = txtAgencyName.Text.Trim()
                End If
                'If chkWholeGroup.Checked Then
                '    .SelectSingleNode("GROUPDATA").InnerText = "TRUE"
                'Else
                '    .SelectSingleNode("GROUPDATA").InnerText = "FALSE"
                'End If
                .SelectSingleNode("HD_RE_ID").InnerText = txtLtrNo.Text.Trim()
                .SelectSingleNode("HD_PTR_REF").InnerText = txtPtrNo.Text.Trim()
                .SelectSingleNode("HD_PTR_TITLE").InnerText = txtPtrTitle.Text.Trim()
                If drpPtrStatus.SelectedIndex <> 0 Then
                    .SelectSingleNode("HD_STATUS_ID").InnerText = drpPtrStatus.SelectedValue
                End If
                If drpSeverity.SelectedIndex <> 0 Then
                    .SelectSingleNode("HD_PTR_SEV_ID").InnerText = drpSeverity.SelectedValue
                End If
                If drpFollowup.SelectedIndex <> 0 Then
                    .SelectSingleNode("HD_PTR_FOLLOWUP_ID").InnerText = drpFollowup.SelectedValue
                End If



                .SelectSingleNode("LOGGEDBY").InnerText = txtPendingWith.Text.Trim()


                '.SelectSingleNode("LOGGEDBYID").InnerText = "" 'Need for Discussion

                If drpAssignedTo.SelectedIndex <> 0 Then
                    .SelectSingleNode("ASSIGNEEID").InnerText = drpAssignedTo.SelectedValue
                End If

                .SelectSingleNode("ASSIGNED_DATE").InnerText = objEaams.ConvertTextDate(txtAssignedDt.Text.Trim())

                If drpPtrType.SelectedIndex <> 0 Then
                    .SelectSingleNode("HD_PTR_TYPE_ID").InnerText = drpPtrType.SelectedValue.Trim()
                End If

                If drpPtrTypCat.SelectedIndex <> 0 Then
                    .SelectSingleNode("HD_PTR_TYCAT_ID").InnerText = drpPtrTypCat.SelectedValue
                End If
                If drp1aOffice.SelectedIndex <> 0 Then
                    .SelectSingleNode("AOFFICE").InnerText = drp1aOffice.SelectedValue.Trim()
                End If

                .SelectSingleNode("HD_PTR_OPENDATE_FROM").InnerText = objEaams.ConvertTextDate(txtOpenDtFrm.Text.Trim())
                .SelectSingleNode("HD_PTR_OPENDATE_TO").InnerText = objEaams.ConvertTextDate(txtOpenDtTo.Text.Trim())
                .SelectSingleNode("HD_PTR_CLOSEDATE_FROM").InnerText = objEaams.ConvertTextDate(txtCloseDtFrm.Text.Trim())
                .SelectSingleNode("HD_PTR_CLOSEDATE_TO").InnerText = objEaams.ConvertTextDate(txtCloseDtTo.Text.Trim())
                'Following Statement is Written for 1A Responsiblity
                Dim str As String()
                str = Session("LoginSession").ToString().Split("|")
                .SelectSingleNode("RESP_1A").InnerText = str(0)

                'Following Statement is written for Limited to own Agency
                Dim xDoc As New XmlDocument
                xDoc.LoadXml(Session("Security"))

                .SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = xDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText  'Login time u find Need for discussion

                If xDoc.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText.ToUpper = "TRUE" Then
                    .SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = xDoc.DocumentElement.SelectSingleNode("Aoffice").InnerText  'Login time u find Need for discussion
                End If

                If xDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper() = "TRUE" Then
                    .SelectSingleNode("LIMITED_TO_REGION").InnerText = xDoc.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText   'Login time u find Need for discussion
                End If

            End With
            Dim objHdPtrSearch As New AAMS.bizBOHelpDesk.bzPTR


            'Start CODE for sorting and paging

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "HD_PTR_REF"
                objHdInputX.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "HD_PTR_REF" '"LOCATION_CODE"
            Else
                objHdInputX.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objHdInputX.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objHdInputX.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            'End Code for paging and sorting

            'Here Back end Method Call
            objHdOutputX = objHdPtrSearch.Search(objHdInputX)
            ' objHdOutputX.LoadXml("<HD_SEARCH_PTR_OUTPUT><PTR HD_PTR_ID='1' HD_RE_ID='10002' HD_PTR_REF='43534' NAME='Test Ptre' ADDRESS='Test Address' HD_PTR_TITLE='Test Title' HD_STATUS_NAME='Ok' HD_PTR_FOLLOWUP_NAME='Test Follow' HD_PTR_SEV_NAME='sdfsd' HD_PTR_TYPE_NAME='sdfsdf' Employee_Name='' HD_PTR_OPENDATE='' HD_PTR_CLOSEDATE='20000202' ASSIGNEE_NAME='Mukesh' ASSIGNED_DATE='20000202'/><Errors Status='FALSE'><Error Code='' Description='' /></Errors></HD_SEARCH_PTR_OUTPUT>")
            If objHdOutputX.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objReader = New XmlNodeReader(objHdOutputX)
                dSet.ReadXml(objReader)
                ViewState("PrevSearching") = objHdInputX.OuterXml
                lblError.Text = ""
                grdvPtr.DataSource = dSet.Tables("PTR")
                grdvPtr.DataBind()

                PagingCommon(objHdOutputX)
                Dim intcol As Integer = GetSortColumnIndex(grdvPtr)
                If ViewState("Desc") = "FALSE" Then
                    grdvPtr.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "TRUE" Then
                    grdvPtr.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If

                intArray(0) = 1
                strArray(0) = "LTRNo."

                intArray(1) = 2
                strArray(1) = "PTR No."

                intArray(2) = 3
                strArray(2) = "Agency Name"

                intArray(3) = 4
                strArray(3) = "Address"

                intArray(4) = 5
                strArray(4) = "PTR Title"

                intArray(5) = 6
                strArray(5) = "PTR Status"

                intArray(6) = 7
                strArray(6) = "Followup Name"



                intArray(7) = 8
                strArray(7) = "Severity Name"

                intArray(8) = 9
                strArray(8) = "PTR Type Name"

                intArray(9) = 10
                strArray(9) = "Employee Name"

                intArray(10) = 11
                strArray(10) = "Open Date"

                intArray(11) = 12
                strArray(11) = "Close Date"

                intArray(12) = 13
                strArray(12) = "Assignee Name"

                intArray(13) = 14
                strArray(13) = "Assigned Date"

                objExport.ExportDetails(objHdOutputX, "PTR", intArray, strArray, ExportExcel.ExportFormat.Excel, "Ptr.xls")


                ' lblError.Text = ""
            Else
                grdvPtr.DataSource = Nothing
                grdvPtr.DataBind()
                pnlPaging.Visible = False
                lblError.Text = objHdOutputX.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
End Class
