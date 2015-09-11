
Partial Class Setup_MSSR_Configuration
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim imgUp As New Image
    Dim imgDown As New Image

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
            Session("PageName") = Request.Url.ToString() '"Setup/MSSR_AirLineOffice.aspx"
            objeAAMS.ExpirePageCache()

            If Request.QueryString("Admin") Is Nothing Then
                Response.Redirect("~/Norights.aspx")
                Exit Sub
            End If
            If Request.QueryString("Admin") <> "Admin" Then
                Response.Redirect("~/Norights.aspx")
                Exit Sub
            End If

            'Code for Paging $ Sorting
            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgDown.ImageUrl = "~/Images/Sortdown.gif"

            'Code of Security Check
            btnSave.Attributes.Add("onClick", "return ValidateConfiguration();")
            btnSearch.Attributes.Add("onClick", "return ValidateSearch();")

            If Session("Security") Is Nothing Then
                '  ClientScript.RegisterStartupScript(Me.GetType(),"loginScript", objeAAMS.CheckSession())
                ' Exit Sub
            End If
            ' btnNew.Attributes.Add("onclick", "return NewFunction();")
            lblError.Text = String.Empty
            If Not Page.IsPostBack Then
                ''Code to Populate Drop Down for Aoffice
                objeAAMS.BindDropDown(drpConfigCriteria, "SETCONFIGURATION", False, 1)
            End If

            'Dim objSecurityXml As New XmlDocument
            'objSecurityXml.LoadXml(Session("Security"))
            'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Configuration']").Count <> 0 Then
            '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Configuration']").Attributes("Value").Value)
            '    End If
            '    If strBuilder(0) = "0" Then
            '        Response.Redirect("../NoRights.aspx", False)
            '        btnSearch.Enabled = False

            '    End If
            '    If strBuilder(1) = "0" Then
            '        btnSave.Enabled = False

            '    End If
            'Else
            '    strBuilder = objeAAMS.SecurityCheck(31)
            'End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.OriginalString)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub ConfigSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzConfiguration As New AAMS.bizMaster.bzConfiguration

        

        objInputXml.LoadXml("<MS_SEARCHCONFIGRURABLE_INPUT><CCA_ID></CCA_ID><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHCONFIGRURABLE_INPUT>")

        objInputXml.DocumentElement.SelectSingleNode("CCA_ID").InnerText = drpConfigCriteria.SelectedValue.Trim()


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
            ViewState("SortName") = "FIELD_NAME"
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = " FIELD_NAME" '"LOCATION_CODE"
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
        objOutputXml = objbzConfiguration.Search(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)

            lblError.Text = ""

            ViewState("PrevSearching") = objInputXml.OuterXml

            GrdvConfig.DataSource = ds.Tables("CONFIGRURABLE")
            GrdvConfig.DataBind()

            PagingCommon(objOutputXml)
            Dim intcol As Integer = GetSortColumnIndex(GrdvConfig)
            If ViewState("Desc") = "FALSE" Then
                GrdvConfig.HeaderRow.Cells(intcol).Controls.Add(imgUp)
            End If
            If ViewState("Desc") = "TRUE" Then
                GrdvConfig.HeaderRow.Cells(intcol).Controls.Add(imgDown)
            End If

        Else
            GrdvConfig.DataSource = Nothing
            GrdvConfig.DataBind()
            pnlPaging.Visible = False
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
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

    
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            'Input
            ' <MS_UPDATECONFIGRURABLE_INPUT>
            '  <CONFIGRURABLE CCA_ID='' CCA_NAME='' FIELD_NAME='' FIELD_VALUE='' Remarks='' Active=''/>
            '  <Errors Status=''>
            '  <Error Code='' Description=''/>
            '  </Errors>
            '</MS_UPDATECONFIGRURABLE_INPUT>

            'Output
            '<MS_UPDATECONFIGRURABLE_OUTPUT>
            ' <CONFIGRURABLE CCA_ID='' CCA_NAME='' FIELD_NAME='' FIELD_VALUE='' Remarks='' Active=''/>
            ' <PAGE PAGE_COUNT='' TOTAL_ROWS=''/>
            ' <Errors Status=''>
            ' <Error Code='' Description=''/>
            ' </Errors>
            '</MS_UPDATECONFIGRURABLE_OUTPUT>

            Try
                If (Page.IsValid) Then
                    Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                    Dim objbzConfiguration As New AAMS.bizMaster.bzConfiguration
                    Dim objChildNode, objChildNodeClone As XmlNode

                    objInputXml.LoadXml("<MS_UPDATECONFIGRURABLE_INPUT><CONFIGRURABLE CCA_ID='' CCA_NAME='' FIELD_NAME='' FIELD_VALUE='' Remarks='' Active=''/></MS_UPDATECONFIGRURABLE_INPUT>")

                    objChildNode = objInputXml.DocumentElement.SelectSingleNode("CONFIGRURABLE")
                    objChildNodeClone = objChildNode.CloneNode(True)
                    objInputXml.DocumentElement.RemoveChild(objChildNode)
                    Dim count As Integer

                    For count = 0 To GrdvConfig.Rows.Count - 1
                        objChildNodeClone.Attributes("CCA_ID").Value = CType(GrdvConfig.Rows(count).Cells(0).FindControl("hdCCA_ID"), HiddenField).Value
                        objChildNodeClone.Attributes("CCA_NAME").Value = ""
                        objChildNodeClone.Attributes("FIELD_NAME").Value = CType(GrdvConfig.Rows(count).Cells(0).FindControl("lblFieldName"), Label).Text
                        objChildNodeClone.Attributes("FIELD_VALUE").Value = CType(GrdvConfig.Rows(count).Cells(1).FindControl("txtValue"), TextBox).Text
                        objChildNodeClone.Attributes("Remarks").Value = CType(GrdvConfig.Rows(count).Cells(3).FindControl("txtRemarks"), TextBox).Text
                        objChildNodeClone.Attributes("Active").Value = IIf(CType(GrdvConfig.Rows(count).Cells(2).FindControl("chkActive"), CheckBox).Checked, "TRUE", "FALSE")
                        objInputXml.DocumentElement.AppendChild(objChildNodeClone)
                        objChildNodeClone = objChildNode.CloneNode(True)
                    Next
                    objOutputXml = objbzConfiguration.Update(objInputXml)

                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        lblError.Text = objeAAMSMessage.messUpdate
                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If

            Catch ex As Exception

            End Try
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            ConfigSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            ConfigSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            ConfigSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub GrdvConfig_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GrdvConfig.Sorting
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
        ConfigSearch()
    End Sub
#End Region

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try

            '<MS_SEARCHCONFIGRURABLE_INPUT>

            ' <CCA_ID></CCA_ID>

            '</MS_SEARCHCONFIGRURABLE_INPUT>

            'Output

            '<MS_SEARCHCONFIGRURABLE_OUTPUT>

            ' <CONFIGRURABLE CCA_ID='' FIELD_NAME='' FIELD_VALUE='' Remarks='' Active=''/>

            ' <PAGE PAGE_COUNT='' TOTAL_ROWS=''/>

            ' <Errors Status=''>

            ' <Error Code='' Description=''/>

            ' </Errors>

            '</MS_SEARCHCONFIGRURABLE_OUTPUT>

            ConfigSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub drpConfigCriteria_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpConfigCriteria.SelectedIndexChanged
        Try



            If drpConfigCriteria.SelectedIndex <> 0 Then
                ConfigSearch()
            Else
                GrdvConfig.DataSource = Nothing
                GrdvConfig.DataBind()
                pnlPaging.Visible = False
            End If

            'Dim objSecurityXml As New XmlDocument
            'objSecurityXml.LoadXml(Session("Security"))
            'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Configuration']").Count <> 0 Then
            '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Configuration']").Attributes("Value").Value)
            '    End If
            '    If strBuilder(0) = "1" Then
            '        If drpConfigCriteria.SelectedIndex <> 0 Then
            '            ConfigSearch()
            '        Else
            '            GrdvConfig.DataSource = Nothing
            '            GrdvConfig.DataBind()
            '            pnlPaging.Visible = False
            '        End If

            '    End If

            'Else
            '    strBuilder = objeAAMS.SecurityCheck(31)
            'End If



            
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
       

    End Sub
End Class
