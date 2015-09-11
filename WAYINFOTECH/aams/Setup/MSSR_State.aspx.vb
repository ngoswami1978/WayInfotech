Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data
Partial Class Setup_MSSR_State
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = "Setup/MSSR_State.aspx"
            btnReset.Attributes.Add("onclick", "return StateReset();")
            btnNew.Attributes.Add("onclick", "return NewFunction();")
            lblError.Text = String.Empty
            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgDown.ImageUrl = "~/Images/Sortdown.gif"
            '***************************************************************************************
            '***************************************************************************************
            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(drpCountry, "COUNTRY", True, 3)
                If Not Session("Action") Is Nothing Then
                    If Session("Action").ToString().Split("|").GetValue(0).ToString.ToUpper = "D" Then
                        txtStateName.Text = Session("Action").ToString().Split("|").GetValue(2)
                        drpCountry.SelectedIndex = Session("Action").ToString().Split("|").GetValue(3)
                        StateSearch()
                        lblError.Text = objeAAMSMessage.messDelete
                        Session("Action") = Nothing
                    End If
                End If
            End If
            '***************************************************************************************
            'If Not Request.QueryString("Action") Is Nothing Then
            '    If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
            '        StateDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))
            '    End If
            'End If


            '***************************************************************************************
            'Code of Security Check
            'objeAAMS.ExpirePageCache()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='State']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='State']").Attributes("Value").Value)
            End If
            If strBuilder(1) = "0" Then
                btnNew.Enabled = False
            End If
            If strBuilder(0) = "0" Then
                Response.Redirect("../NoRights.aspx")
                btnSearch.Enabled = False
            End If
            If strBuilder(4) = "0" Then
                btnExport.Enabled = False
            End If
            'Code End for security

            '   Deleting records.
            If (hdStateID.Value <> "") Then
                StateDelete(hdStateID.Value)
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            StateSearch()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString()
        End Try
    End Sub
    'Method for search Aoffice
    Private Sub StateSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzCountryState As New AAMS.bizMaster.bzCountryState

        objInputXml.LoadXml("<MS_SEARCHCOUNTRYSTATE_INPUT><State_Name></State_Name><CountryID></CountryID> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHCOUNTRYSTATE_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("State_Name").InnerXml = Trim(txtStateName.Text)
        If drpCountry.SelectedIndex <> 0 Then
            objInputXml.DocumentElement.SelectSingleNode("CountryID").InnerXml = drpCountry.SelectedValue
        End If

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
            ViewState("SortName") = "State_Name"
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "State_Name" '"LOCATION_CODE"
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

        objOutputXml = objbzCountryState.Search(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
           

            lblError.Text = ""

            ViewState("PrevSearching") = objInputXml.OuterXml

            grdState.DataSource = ds.Tables("STATE")
            grdState.DataBind()

            PagingCommon(objOutputXml)
            Dim intcol As Integer = GetSortColumnIndex(grdState)
            If ViewState("Desc") = "FALSE" Then
                grdState.HeaderRow.Cells(intcol).Controls.Add(imgUp)
            End If
            If ViewState("Desc") = "TRUE" Then
                grdState.HeaderRow.Cells(intcol).Controls.Add(imgDown)
            End If

        Else
            grdState.DataSource = String.Empty
            grdState.DataBind()
            pnlPaging.Visible = False
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Protected Sub grdState_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdState.RowCommand
        ''Code for Edit Data
        'If e.CommandName = "EditX" Then
        '    Session("Action") = "U|" & e.CommandArgument & ""
        '    Response.Redirect("MSUP_State.aspx")
        'End If
        ''Code for Delete Date
        'If e.CommandName = "DeleteX" Then
        '    StateDelete(e.CommandArgument)
        'End If
    End Sub
    Private Sub StateDelete(ByVal strStateId As String)
        Dim objInputXml As New XmlDocument
        Dim objbzCountryState As New AAMS.bizMaster.bzCountryState
        objInputXml.LoadXml("<MS_DELETECOUNTRYSTATE_INPUT><StateID></StateID></MS_DELETECOUNTRYSTATE_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("StateID").InnerXml = strStateId
        'Here Back end Method Call
        Try
            hdStateID.Value = ""

            objInputXml = objbzCountryState.Delete(objInputXml)
            StateSearch()
            If objInputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ' Session("Action") = Request.QueryString("Action")
                ' Response.Redirect("MSSR_State.aspx")
                lblError.Text = objeAAMSMessage.messDelete
            Else
                lblError.Text = objInputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub grdState_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdState.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
        Dim hdStateId As HiddenField
        Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
        linkEdit = e.Row.FindControl("linkEdit")
        Dim linkDelete As System.Web.UI.HtmlControls.HtmlAnchor
        linkDelete = e.Row.FindControl("linkDelete")
        hdStateId = e.Row.FindControl("hdStateId")

        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='State']").Count <> 0 Then
            strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='State']").Attributes("Value").Value)
        End If
        If strBuilder(3) = "0" Then
            linkDelete.Disabled = True
        Else
            linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdStateId.Value & "');")
        End If
        ' If strBuilder(2) = "0" Then
        'linkEdit.Disabled = False
        ' Else
        linkEdit.Attributes.Add("onclick", "return EditFunction('" & objEn.Encrypt(hdStateId.Value.Trim) & "');")
        'End If
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click

        Response.Redirect(Request.Url.OriginalString)

        'txtStateName.Text = String.Empty
        'drpCountry.SelectedIndex = 0
        'grdState.DataSource = String.Empty
        'grdState.DataBind()
    End Sub

    Protected Sub grdState_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdState.Sorting
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
        StateSearch()
    End Sub
#End Region

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            StateSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            StateSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            StateSearch()
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
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzCountryState As New AAMS.bizMaster.bzCountryState
        Dim objExport As New ExportExcel
        Dim intArray(1) As Integer
        Dim strArray(1) As String

        objInputXml.LoadXml("<MS_SEARCHCOUNTRYSTATE_INPUT><State_Name></State_Name><CountryID></CountryID> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHCOUNTRYSTATE_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("State_Name").InnerXml = Trim(txtStateName.Text)
        If drpCountry.SelectedIndex <> 0 Then
            objInputXml.DocumentElement.SelectSingleNode("CountryID").InnerXml = drpCountry.SelectedValue
        End If

        'Code for Paging and Sorting

        If ViewState("SortName") Is Nothing Then
            ViewState("SortName") = "StateID"
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "StateID" '"LOCATION_CODE"
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

        objOutputXml = objbzCountryState.Search(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)


            lblError.Text = ""
            intArray(0) = 1
            strArray(0) = "State Name"

            intArray(1) = 3
            strArray(1) = "Country"

            objExport.ExportDetails(objOutputXml, "STATE", intArray, strArray, ExportExcel.ExportFormat.Excel, "States.xls")
        Else
            grdState.DataSource = String.Empty
            grdState.DataBind()
            pnlPaging.Visible = False
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
End Class
