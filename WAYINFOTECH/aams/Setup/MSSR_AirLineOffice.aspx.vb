Imports System.Data
Imports System.Xml

Partial Class Setup_MSSR_AirLineOffice
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim imgUp As New Image
    Dim imgDown As New Image
    Dim objEn As New EncyrptDeCyrpt

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

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            AirlineOfficeSearch()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    'Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
    '    Response.Redirect("MSUP_AirlineOffice.aspx?Action=I", False)
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

       
            Session("PageName") = Request.Url.ToString() '"Setup/MSSR_AirLineOffice.aspx"
            objeAAMS.ExpirePageCache()

            'Code for Paging $ Sorting
            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgDown.ImageUrl = "~/Images/Sortdown.gif"

            'Code of Security Check
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            btnNew.Attributes.Add("onclick", "return NewFunction();")
            lblError.Text = String.Empty
            If Not Page.IsPostBack Then
                ''Code to Populate Drop Down for Aoffice
                objeAAMS.BindDropDown(cboAoffice, "AOFFICE", True, 3)
            End If
           

            Dim objSecurityXml As New XmlDocument
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
            End If
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            Dim li As ListItem
                            li = cboAoffice.Items.FindByValue(objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText)
                            If li IsNot Nothing Then
                                cboAoffice.SelectedValue = li.Value
                            End If
                        End If
                        cboAoffice.Enabled = False
                    End If
                End If
            End If

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='AirLine Office']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='AirLine Office']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSearch.Enabled = False

                End If
                If strBuilder(4) = "0" Then
                    Button1.Enabled = False
                End If

                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            '   Deleting records.
            If (hdDelete.Value <> "") Then
                AirlineOfficeDelete(hdDelete.Value)
                Dim CurrentPage As Integer = CInt(IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue))
                If txtRecordOnCurrReco.Text = "1" Then
                    If CurrentPage - 1 > 0 Then
                        ddlPageNumber.SelectedValue = (CurrentPage - 1).ToString
                    Else
                        ddlPageNumber.SelectedValue = "1"
                    End If
                End If
                AirlineOfficeSearch()
                hdDelete.Value = ""
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    Private Sub AirlineOfficeSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzAirlineOffice As New AAMS.bizMaster.bzAirlineOffice

        objInputXml.LoadXml("<MS_SEARCHAIRLINEOFFICE_INPUT><Airline_Code></Airline_Code><Airline_Name></Airline_Name><Aoffice></Aoffice> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHAIRLINEOFFICE_INPUT>")

        objInputXml.DocumentElement.SelectSingleNode("Airline_Code").InnerText = Trim(txtAirLinecode.Text)
        objInputXml.DocumentElement.SelectSingleNode("Airline_Name").InnerText = Trim(txtAirLineName.Text)
        objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = IIf(Trim(cboAoffice.Text) = "---Select One---", "", Trim(cboAoffice.Text))



        'Code for Paging and Sorting
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
            ViewState("SortName") = "Airline_Code"
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Airline_Code" '"LOCATION_CODE"
        Else
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
        End If

        If ViewState("Desc") Is Nothing Then
            ViewState("Desc") = "FALSE"
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
        Else
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
        End If
        'End of Paging and Sorting 


        'Here Back end Method Call
        objOutputXml = objbzAirlineOffice.Search(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)

            If hdDelete.Value.Trim = "" Then
                lblError.Text = ""
            End If

            ViewState("PrevSearching") = objInputXml.OuterXml

            dbgrdManageAirLineOffice.DataSource = ds.Tables("AIRLINE_OFFICE")
            dbgrdManageAirLineOffice.DataBind()

            txtRecordOnCurrReco.Text = dbgrdManageAirLineOffice.Rows.Count.ToString

            PagingCommon(objOutputXml)
            Dim intcol As Integer = GetSortColumnIndex(dbgrdManageAirLineOffice)
            If ViewState("Desc") = "FALSE" Then
                dbgrdManageAirLineOffice.HeaderRow.Cells(intcol).Controls.Add(imgUp)
            End If
            If ViewState("Desc") = "TRUE" Then
                dbgrdManageAirLineOffice.HeaderRow.Cells(intcol).Controls.Add(imgDown)
            End If

        Else
            dbgrdManageAirLineOffice.DataSource = Nothing
            dbgrdManageAirLineOffice.DataBind()
            txtTotalRecordCount.Text = "0"
            'hdRecordOnCurrentPage.Value = "0"
            pnlPaging.Visible = False
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Protected Sub dbgrdManageAirLineOffice_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dbgrdManageAirLineOffice.RowCommand
        'Code for Edit Data
        'If e.CommandName = "EditX" Then
        '    Response.Redirect("MSUP_AirlineOffice.aspx?Action=U&AR_OF_ID=" & e.CommandArgument)
        'End If
        ''Code for Delete Date
        'If e.CommandName = "DeleteX" Then
        '    AirlineOfficeDelete(e.CommandArgument)
        '    AirlineOfficeSearch()
        'End If
    End Sub
    Sub AirlineOfficeDelete(ByVal strAirline_CODE As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAirlineOffice As New AAMS.bizMaster.bzAirlineOffice
        objInputXml.LoadXml("<MS_DELETEAIRLINEOFFICE_INPUT><AR_OF_ID></AR_OF_ID></MS_DELETEAIRLINEOFFICE_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("AR_OF_ID").InnerText = strAirline_CODE

        'Here Back end Method Call
        'hdDelete.Value = ""

        objOutputXml = objbzAirlineOffice.Delete(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            ' Session("Action") = Request.QueryString("Action")
            lblError.Text = objeAAMSMessage.messDelete
            

            ' Response.Redirect("MSSR_AirLineOffice.aspx")
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Protected Sub dbgrdManageAirLineOffice_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dbgrdManageAirLineOffice.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
        Dim i As Integer
        '#############################################################
        ' Code added For Selecting an Items 

        'Dim lnkSelect As System.Web.UI.HtmlControls.HtmlAnchor
        Dim lnkSelect As LinkButton
        'Dim hdSelect As HiddenField
        'hdSelect = (CType(e.Row.FindControl("hdSelect"), HiddenField))
        lnkSelect = e.Row.FindControl("lnkSelect")
        Dim hdAirCode As HiddenField
        hdAirCode = e.Row.FindControl("hdAirCode")
        Dim str() As String = lnkSelect.CommandArgument.Split("|")
        For i = 0 To str.Length - 1
            If i = 3 Then
                str(i) = str(i).Replace(vbCrLf, "\n")
            Else
                ' str(i) = Server.UrlEncode(str(i))
            End If
            If i = 0 Then
                lnkSelect.CommandArgument = str(i)
            Else
                lnkSelect.CommandArgument += "|" + str(i)
            End If
        Next
        Dim strcommand As String = lnkSelect.CommandArgument
        Dim BDRLetter, TempLateVersion As String
        BDRLetter = ""
        TempLateVersion = ""
        If (Request.QueryString("PopUp")) Is Nothing Then
            lnkSelect.Visible = False
        Else
            GetTemplate(BDRLetter, TempLateVersion, hdAirCode.Value)
            lnkSelect.Visible = True
            'lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & hdSelect.Value & "');")
            lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" + strcommand + "','" + BDRLetter + "','" + TempLateVersion + "');")
        End If
        '#############################################################
        Dim hdAirlineCode As HiddenField
        Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
        linkEdit = e.Row.FindControl("linkEdit")
        Dim linkDelete As System.Web.UI.HtmlControls.HtmlAnchor
        linkDelete = e.Row.FindControl("linkDelete")
        hdAirlineCode = e.Row.FindControl("hdAirlineCode")

        'linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdAirlineCode.Value & "');")
        'linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdAirlineCode.Value & "');")
        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='AirLine Office']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='AirLine Office']").Attributes("Value").Value)
            End If
            If strBuilder(3) = "0" Then
                linkDelete.Disabled = True
            Else
                If (Request.QueryString("PopUp")) Is Nothing Then
                    linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdAirlineCode.Value & "');")
                Else
                    linkDelete.Attributes.Add("onclick", "return DeleteFunction2('" & hdAirlineCode.Value & "');")
                End If
                'linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdAirlineCode.Value & "');")
            End If
            'If strBuilder(2) = "0" Then
            '    linkEdit.Disabled = False
            'Else
            linkEdit.Attributes.Add("onclick", "return EditFunction('" & objEn.Encrypt(hdAirlineCode.Value.Trim) & "');")
            ' End If
        Else
        linkDelete.Disabled = False
        linkEdit.Disabled = False
            linkEdit.Attributes.Add("onclick", "return EditFunction('" & objEn.Encrypt(hdAirlineCode.Value.Trim) & "');")
        'linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdAirlineCode.Value & "');")
        If (Request.QueryString("PopUp")) Is Nothing Then
            linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdAirlineCode.Value & "');")
        Else
            linkDelete.Attributes.Add("onclick", "return DeleteFunction2('" & hdAirlineCode.Value & "');")
        End If
        End If
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect(Request.Url.OriginalString)
        'txtAirLinecode.Text = String.Empty
        'txtAirLineName.Text = String.Empty
        'cboAoffice.SelectedIndex = 0
        'dbgrdManageAirLineOffice.DataSource = String.Empty
        'dbgrdManageAirLineOffice.DataBind()
    End Sub
    Public Sub GetTemplate(ByRef BDRLetter, ByRef TempLateVersion, ByVal AirLineCode)

        Try
            Dim strbody As String = ""
            Dim objInputTempXml, objOutputTempXml, objSecurityXml As New XmlDocument
            Dim objbzbzTemplate As New AAMS.bizMaster.bzTemplate
            Dim ds As New DataSet
            Dim strAoffice As String = ""

            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                    strAoffice = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                Else
                    strAoffice = ""
                End If
            End If
            If Request.QueryString("HD_RE_BDR_ID") IsNot Nothing Then

            End If


            '#####################################################################
            ' Getting Template for BDR Template

            objInputTempXml.LoadXml("<HD_DOCUMENTTEMPLATE_INPUT><TEMPLATES TemplateName='' AOFFICE ='' AIRLINECODE ='' /></HD_DOCUMENTTEMPLATE_INPUT>")
            objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("TemplateName").Value() = "BDR Letter" ' Session("Action").ToString().Split("|").GetValue(1).ToString()
            objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("AOFFICE").Value() = strAoffice ' hdAoffice.Value  
            objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("AIRLINECODE").Value() = AirLineCode 'hdAirLineCode.Value

            'objOutputTempXml.LoadXml("<HD_DOCUMENTTEMPLATE_OUTPUT><TEMPLATES TemplateName='' AOFFICE ='' AIRLINECODE ='' BDRTemplate = ''/><Errors Status='FALSE'><Error Code='' Description=''/></Errors></HD_DOCUMENTTEMPLATE_OUTPUT>")
            objOutputTempXml = objbzbzTemplate.GetDocumentTemplate(objInputTempXml)
            If objOutputTempXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                'hdTemplateVersion.Value = objOutputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("T_G_DOC_VERSION").Value
                strbody = objOutputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("BDRTemplate").Value
                'strcommand += "|" + Server.UrlEncode(strbody) + "|" + objOutputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("T_G_DOC_VERSION").Value
                strbody = strbody.Replace(vbCrLf, "\n")
                BDRLetter = strbody
                TempLateVersion = objOutputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("T_G_DOC_VERSION").Value
            End If

        Catch ex As Exception

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


    Protected Sub dbgrdManageAirLineOffice_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles dbgrdManageAirLineOffice.Sorting
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
        AirlineOfficeSearch()
    End Sub
#End Region

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            AirlineOfficeSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            AirlineOfficeSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            AirlineOfficeSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim objExport As New ExportExcel
        'intArray, strArray,
        Dim intArray(3) As Integer
        Dim strArray(3) As String
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzAirlineOffice As New AAMS.bizMaster.bzAirlineOffice

        objInputXml.LoadXml("<MS_SEARCHAIRLINEOFFICE_INPUT><Airline_Code></Airline_Code><Airline_Name></Airline_Name><Aoffice></Aoffice> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHAIRLINEOFFICE_INPUT>")

        objInputXml.DocumentElement.SelectSingleNode("Airline_Code").InnerText = Trim(txtAirLinecode.Text)
        objInputXml.DocumentElement.SelectSingleNode("Airline_Name").InnerText = Trim(txtAirLineName.Text)
        objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = IIf(Trim(cboAoffice.Text) = "---Select One---", "", Trim(cboAoffice.Text))

        If ViewState("SortName") Is Nothing Then
            ViewState("SortName") = "Airline_Code"
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Airline_Code" '"LOCATION_CODE"
        Else
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
        End If

        If ViewState("Desc") Is Nothing Then
            ViewState("Desc") = "FALSE"
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
        Else
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
        End If

 
        objOutputXml = objbzAirlineOffice.Search(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)

            lblError.Text = ""



            dbgrdManageAirLineOffice.DataSource = ds.Tables("AIRLINE_OFFICE")
            dbgrdManageAirLineOffice.DataBind()

           

        Else
            dbgrdManageAirLineOffice.DataSource = Nothing
            dbgrdManageAirLineOffice.DataBind()
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
        'intArray(0) = 0
        'strArray(0) = "Office ID"

        intArray(0) = 1
        strArray(0) = "AirLine Code"

        intArray(1) = 2
        strArray(1) = "AirLine Name"

        intArray(2) = 3
        strArray(2) = "Airline Address"

        intArray(3) = 4
        strArray(3) = "Aoffice"

        objExport.ExportDetails(objOutputXml, "AIRLINE_OFFICE", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportAirLineOffice.xls")

        ' objExport.ExportDetails(objOutputXml, "AIRLINE_OFFICE", ExportExcel.ExportFormat.Excel, "AirlineOffice.xls")
    End Sub
End Class
