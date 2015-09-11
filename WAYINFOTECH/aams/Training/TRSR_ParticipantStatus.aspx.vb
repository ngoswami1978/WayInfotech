
Partial Class Training_TRSR_ParticipantStatus
    Inherits System.Web.UI.Page

#Region "Global Variable Declarations."
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
	Dim objeAAMSMessage As New eAAMSMessage
	Public strBuilder As New StringBuilder
#End Region
#Region "Page Filter()"
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
#End Region
#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblError.Text = ""
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            ' Checking security.
            CheckSecurity()
            If Not IsPostBack Then
                txtParticipantStatus.Focus()

              
            End If
            '   Deleting records.
            If (hdParticipantStatusId.Value <> "") Then
                DeleteRecords()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
	End Sub
#End Region

#Region "btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click"
	Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
		Response.Redirect("TRUP_ParticipantStatus.aspx?Action=I")
	End Sub
#End Region

#Region "btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click"
	Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
		Try
            BindData(PageOperation.Search)
		Catch ex As Exception
			lblError.Text = ex.Message
		End Try
	End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
	Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
		Response.Redirect("TRSR_ParticipantStatus.aspx")
	End Sub
#End Region

#Region "BindData()"
    Private Sub BindData(ByVal Operation As Integer)
        ' lblError.Text = ""
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzParticipantStatus As New AAMS.bizTraining.bzParticipantStatus
        Try
            'objInputXml.LoadXml("<TR_SEARCH_PTSTATUS_INPUT><TR_PARTSTATUS_NAME></TR_PARTSTATUS_NAME></TR_SEARCH_PTSTATUS_INPUT> ")
            objInputXml.LoadXml("<TR_SEARCH_PTSTATUS_INPUT><TR_PARTSTATUS_NAME></TR_PARTSTATUS_NAME><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></TR_SEARCH_PTSTATUS_INPUT> ")

            objInputXml.DocumentElement.SelectSingleNode("TR_PARTSTATUS_NAME").InnerText = txtParticipantStatus.Text.Trim()

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
                    ViewState("SortName") = "TR_PARTSTATUS_NAME"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "TR_PARTSTATUS_NAME" '"LOCATION_CODE"
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

            objOutputXml = objbzParticipantStatus.Search(objInputXml)



            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If
                ViewState("PrevSearching") = objInputXml.OuterXml

                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                If ds.Tables("PTSTATUS").Rows.Count <> 0 Then
                    gvDomain.DataSource = ds.Tables("PTSTATUS")
                    gvDomain.DataBind()


                    '@ Code Added For Paging And Sorting In case Of Delete The Record

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
                    txtRecordOnCurrentPage.Text = ds.Tables("PTSTATUS").Rows.Count.ToString
                    txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value


                    ' @ Added Code To Show Image'
                    Dim imgUp As New Image
                    imgUp.ImageUrl = "~/Images/Sortup.gif"
                    Dim imgDown As New Image
                    imgDown.ImageUrl = "~/Images/Sortdown.gif"

                    Select Case ViewState("SortName").ToString
                        Case "TR_PARTSTATUS_NAME"
                            Select Case ViewState("Desc").ToString
                                Case "FALSE"
                                    gvDomain.HeaderRow.Cells(0).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvDomain.HeaderRow.Cells(0).Controls.Add(imgDown)
                            End Select
                    End Select
                    ' @ Added Code To Show Image'




                Else
                    gvDomain.DataSource = Nothing
                    gvDomain.DataBind()
                    txtTotalRecordCount.Text = "0"
                    txtRecordOnCurrentPage.Text = "0"
                End If
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                gvDomain.DataSource = Nothing
                gvDomain.DataBind()
                txtTotalRecordCount.Text = "0"
                txtRecordOnCurrentPage.Text = "0"
                pnlPaging.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
#End Region

#Region "DeleteRecords()"
	Private Sub DeleteRecords()
		Try
			Dim objInputXml, objOutputXml As New XmlDocument
			Dim objbzParticipantStatus As New AAMS.bizTraining.bzParticipantStatus
			If hdParticipantStatusId.Value <> "" Then
				objInputXml.LoadXml("<TR_DELETE_PTSTATUS_INPUT><PTSTATUS TR_PARTSTATUS_ID='' /></TR_DELETE_PTSTATUS_INPUT> ")
				objInputXml.DocumentElement.SelectSingleNode("PTSTATUS").Attributes("TR_PARTSTATUS_ID").Value = hdParticipantStatusId.Value
				hdParticipantStatusId.Value = ""
				objOutputXml = objbzParticipantStatus.Delete(objInputXml)


             
				If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    lblError.Text = objeAAMSMessage.messDelete
                    ' ###################################################################
                    '@ Code Added For Paging And Sorting In case Of Delete The Record
                    ' ###################################################################
                    Dim CurrentPage As Integer = CInt(IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue))
                    If txtRecordOnCurrentPage.Text = "1" Then
                        If CurrentPage - 1 > 0 Then
                            ddlPageNumber.SelectedValue = (CurrentPage - 1).ToString
                        Else
                            ddlPageNumber.SelectedValue = "1"
                        End If
                    End If
                    ' ###################################################################
                    '@ End of Code Added For Paging And Sorting In case Of Delete The Record
                    ' ###################################################################
                    BindData(PageOperation.Search)
                   
				Else
					lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
				End If
			End If
		Catch ex As Exception
			lblError.Text = ex.Message
		End Try
	End Sub
#End Region

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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Participant Status']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Participant Status']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSearch.Enabled = False

                End If
                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                End If
                If strBuilder(4) = "0" Then
                    btnExport.Enabled = False
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "gvDomain_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDomain.RowDataBound"
	Protected Sub gvDomain_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDomain.RowDataBound
		Try
			If e.Row.RowIndex < 0 Then
				Exit Sub
			End If
			'************* Code for edit link ****************************************************************

			Dim hdDomain As HiddenField
			Dim btnEdit As LinkButton
			hdDomain = CType(e.Row.FindControl("hdDomain"), HiddenField)
			btnEdit = CType(e.Row.FindControl("lnkEdit"), LinkButton)
			'************* end code for edit ***************************************************************** 

			'************* Code for Delete link ****************************************************************
			Dim btnDelete As LinkButton
			btnDelete = CType(e.Row.FindControl("lnkDelete"), LinkButton)
            'btnEdit.Attributes.Add("OnClick", "javascript:return Edit('" + hdDomain.Value + "');")
            ' btnDelete.Attributes.Add("OnClick", "javascript:return Delete('" + hdDomain.Value + "');")

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Participant Status']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Participant Status']").Attributes("Value").Value)
                End If
                If strBuilder(3) = "0" Then
                    btnDelete.Enabled = False
                Else
                    btnDelete.Attributes.Add("OnClick", "javascript:return Delete('" + hdDomain.Value + "');")
                End If
                'If strBuilder(2) = "0" Then
                '    btnEdit.Enabled = False
                'Else
                '    btnEdit.Attributes.Add("OnClick", "javascript:return Edit('" + hdDomain.Value + "');")
                'End If
                btnEdit.Attributes.Add("OnClick", "javascript:return Edit('" + objED.Encrypt(hdDomain.Value) + "');")
            Else
                btnEdit.Attributes.Add("OnClick", "javascript:return Edit('" + objED.Encrypt(hdDomain.Value) + "');")
                btnDelete.Attributes.Add("OnClick", "javascript:return Delete('" + hdDomain.Value + "');")

            End If

		Catch ex As Exception
			lblError.Text = ex.Message
		End Try
	End Sub
#End Region


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

    Protected Sub gvDomain_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDomain.Sorted
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gvDomain_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvDomain.Sorting
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
    Sub Export(ByVal objOutputXml As XmlDocument)
        Dim objExport As New ExportExcel
        Dim strArray() As String = {"Participant Status"}
        Dim intArray() As Integer = {1}
        objExport.ExportDetails(objOutputXml, "PTSTATUS", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportPARTICIPANTSTATUS.xls")


    End Sub
    'End Code For Export

End Class
