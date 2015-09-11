Imports System.Data.SqlClient
Imports System.Data
Imports System.Xml
Partial Class Setup_MSSR_Designation
    Inherits System.Web.UI.Page


#Region "Global Declaration Sectino"
    Dim objDT As DataTable
    Dim objDR As DataRow
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim imgUp As New Image
    Dim imgDown As New Image
    Dim objEn As New EncyrptDeCyrpt
#End Region


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

#Region "Form Load Code"
    Protected Sub frmDesignation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmDesignation.Load
        Try

            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgDown.ImageUrl = "~/Images/Sortdown.gif"

            Session("PageName") = "Setup/MSSR_Designation.aspx"
            objeAAMS.ExpirePageCache()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Designation']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Designation']").Attributes("Value").Value)
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
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If


            If Not Page.IsPostBack Then
                ' btnReset.Attributes.Add("onclick", "return DesignationReset();")
                btnNew.Attributes.Add("onclick", "return NewFunction();")
                If Not Request.QueryString("Action") Is Nothing Then
                    If Request.QueryString("Action").ToString().ToUpper() = "D" Then
                        deleteRecord(Request.QueryString("DesignationId").ToString())
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "Code on New Button Click"
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "Search Button Code"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            designationSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "Designation Search Function"
    Private Sub designationSearch()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim dSet As New DataSet
            Dim objbzDesignation As New AAMS.bizMaster.bzDesignation
            objInputXml.LoadXml("<MS_SEARCHDESIGNATION_INPUT><Designation></Designation> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHDESIGNATION_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("Designation").InnerText = txtDesignation.Text.Trim()

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
                ViewState("SortName") = "Designation"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Designation" '"LOCATION_CODE"
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

            objOutputXml = objbzDesignation.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If (objOutputXml.DocumentElement.SelectSingleNode("DESIGNATION ").Attributes("DesignationID").Value <> "") Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    dSet.ReadXml(objXmlReader)
                   
                    lblError.Text = ""

                    ViewState("PrevSearching") = objInputXml.OuterXml

                    dbgrdDesignation.DataSource = dSet.Tables("Designation").DefaultView
                    dbgrdDesignation.DataBind()

                    PagingCommon(objOutputXml)
                    Dim intcol As Integer = GetSortColumnIndex(dbgrdDesignation)
                    If ViewState("Desc") = "FALSE" Then
                        dbgrdDesignation.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                    End If
                    If ViewState("Desc") = "TRUE" Then
                        dbgrdDesignation.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                    End If

                Else
                    dbgrdDesignation.DataSource = Nothing
                    dbgrdDesignation.DataBind()
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value.Trim()
                End If
            Else
                dbgrdDesignation.DataSource = Nothing
                dbgrdDesignation.DataBind()
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value.Trim()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "Delete Function"
    Sub deleteRecord(ByVal dseigCode As String)
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbzDeisg As New AAMS.bizMaster.bzDesignation
            objInputXml.LoadXml("<MS_DELETEDESIGNATION_INPUT><DesignationID></DesignationID></MS_DELETEDESIGNATION_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("DesignationID").InnerText = dseigCode
            objOutputXml = objbzDeisg.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                designationSearch()

                lblError.Text = objeAAMSMessage.messDelete
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "Gridview RowDataBound Event Codes"
    Protected Sub dbgrdDesignation_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dbgrdDesignation.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
        Dim hdStateId As HiddenField
        Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
        linkEdit = e.Row.FindControl("linkEdit")
        Dim linkDelete As System.Web.UI.HtmlControls.HtmlAnchor
        linkDelete = e.Row.FindControl("linkDelete")
        hdStateId = e.Row.FindControl("hdDesigID")
        If strBuilder(3) = "0" Then
            linkDelete.Disabled = True
        Else
            linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdStateId.Value & "');")
        End If

        'If strBuilder(2) = "0" Then
        'linkEdit.Disabled = False
        'Else
        linkEdit.Attributes.Add("onclick", "return EditValues('" & objEn.Encrypt(hdStateId.Value) & "');")
        'End If
    End Sub
#End Region

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            designationSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            designationSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            designationSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub dbgrdDesignation_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles dbgrdDesignation.Sorting
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
        designationSearch()
    End Sub
#End Region
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
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim dSet As New DataSet
            Dim objExport As New ExportExcel
            Dim intArray(0) As Integer
            Dim strArray(0) As String

            Dim objbzDesignation As New AAMS.bizMaster.bzDesignation
            objInputXml.LoadXml("<MS_SEARCHDESIGNATION_INPUT><Designation></Designation> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHDESIGNATION_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("Designation").InnerText = txtDesignation.Text.Trim()

            'Code for Paging and Sorting

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "Designation"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Designation" '"LOCATION_CODE"
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

            objOutputXml = objbzDesignation.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If (objOutputXml.DocumentElement.SelectSingleNode("DESIGNATION ").Attributes("DesignationID").Value <> "") Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    dSet.ReadXml(objXmlReader)

                    lblError.Text = ""
                    intArray(0) = 1
                    strArray(0) = "Designation"

                    objExport.ExportDetails(objOutputXml, "DESIGNATION", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportDesignation.xls")
                Else
                    dbgrdDesignation.DataSource = Nothing
                    dbgrdDesignation.DataBind()
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value.Trim()
                End If
            Else
                dbgrdDesignation.DataSource = Nothing
                dbgrdDesignation.DataBind()
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value.Trim()
            End If






        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

       
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("MSSR_Designation.aspx")
    End Sub

   
End Class
