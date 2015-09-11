Imports System.Xml
Imports System.Data
Imports System.Data.SqlClient
Partial Class TravelAgency_TASR_DeinstallMisc
    Inherits System.Web.UI.Page

#Region "Page Level Variables/Objects"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim imgDown As New Image
    Dim imgUp As New Image
    Dim Lcode As String
#End Region

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
                If Response.ContentType = "text/html" Then
                    Response.Filter = New TrimStream(Response.Filter)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgDown.ImageUrl = "~/Images/Sortdown.gif"
            Session("PageName") = Request.Url.ToString()
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If

            If (Session("Action") IsNot Nothing) Then
                If (Session("Action").ToString().Split("|").Length >= 2) Then
                    Lcode = Session("Action").ToString().Split("|").GetValue(1)
                Else
                    lblError.Text = "Lcode is not exist."
                    ' btnNew.Enabled = False
                    Exit Sub
                End If
            Else
                lblError.Text = "Lcode is not exist."
                '    Exit Sub
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MiscDeinstallation']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MiscDeinstallation']").Attributes("Value").Value)

                    If strBuilder(4) = "0" Then
                        btnExport.Enabled = False
                    End If
                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                    End If

                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            If Not Page.IsPostBack Then


            End If


            'Dim CurrentPage As Integer = CInt(IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue))
            'If txtRecordOnCurrReco.Text = "1" Then
            '    If CurrentPage - 1 > 0 Then
            '        ddlPageNumber.SelectedValue = (CurrentPage - 1).ToString
            '    Else
            '        ddlPageNumber.SelectedValue = "1"
            '    End If
            'End If

            If Not Page.IsPostBack Then
                ' AgencyView()
                If strBuilder(0) = "1" Then
                    GetAgencyMiscInstallationDetails()
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim ds As New DataSet
        Dim objExport As New ExportExcel
        Try
            If (Session("Action") IsNot Nothing) Then
                Lcode = Session("Action").ToString().Split("|").GetValue(1)

                Dim objbzMiscDeInstallation As New AAMS.bizTravelAgency.bzMiscInstallation

                objInputXml.LoadXml("<MS_GETMISCDEINSTALLATION_INPUT><LCode></LCode> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></MS_GETMISCDEINSTALLATION_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("LCode").InnerText = Lcode ' "229"

                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "DEINSTALLATIONDATE"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "DEINSTALLATIONDATE" '"LOCATION_CODE"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
                End If

                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
                End If

                'End Code for paging and sorting

                'Here Back end Method Call
                objOutputXml = objbzMiscDeInstallation.RptDeInstalledMiscHW(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    ViewState("PrevSearching") = objInputXml.OuterXml
                    '                      <GETMISCDEINSTALLATION 
                    '                    Lcode = "261"
                    '                    INSTALLATIONDATE = "13-Nov-2003"
                    '                    DEINSTALLATIONDATE = "13-Mar-2008"
                    '                    EQUIPMENT_TYPE = "TK3"
                    '                    EQUIPMENT_NO = "WA3E000236"
                    'QTY="1" CHALLANNUMBER="" 
                    '                    LOGGEDBY = "Ajith Kizhakkumpat"
                    '                    LOGGEDDATETIME = "13-Mar-2008 04:36:23 PM"
                    'ROWID="1205365" /> 


                    'Dim objExport As New ExportExcel
                    Dim strArray() As String = {"install Date", "DeInstall Date", "Eqipment Type", "Eqipment No.", "Eqipment Qty", "Challan No.", "Logged By", "Logged Datetime"}
                    Dim intArray() As Integer = {1, 2, 3, 4, 5, 6, 7, 8}
                    objExport.ExportDetails(objOutputXml, "GETMISCDEINSTALLATION", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportDeinstallMisc.xls")
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        Finally

        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.OriginalString, False)
        Catch ex As Exception

        Finally

        End Try
    End Sub

#Region " GetAgencyMiscInstallationDetails Procedure"
    Private Sub GetAgencyMiscInstallationDetails()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objNode As XmlNode
        Try
            If (Session("Action") IsNot Nothing) Then
                Lcode = Session("Action").ToString().Split("|").GetValue(1)

                Dim objbzMiscDeInstallation As New AAMS.bizTravelAgency.bzMiscInstallation

                objInputXml.LoadXml("<MS_GETMISCDEINSTALLATION_INPUT><LCode></LCode> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></MS_GETMISCDEINSTALLATION_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("LCode").InnerText = Lcode ' "229"

                'Start CODE for sorting and paging

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
                    ViewState("SortName") = "DEINSTALLATIONDATE"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "DEINSTALLATIONDATE" '"LOCATION_CODE"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
                End If

                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
                End If

                'End Code for paging and sorting

                'Here Back end Method Call
                objOutputXml = objbzMiscDeInstallation.RptDeInstalledMiscHW(objInputXml)

                For Each objNode In objOutputXml.DocumentElement.SelectNodes("GETMISCDEINSTALLATION")
                    '  objNode.Attributes("DATE").Value = objNode.Attributes("INSDATE").Value.Trim()
                    ' objNode.Attributes("DEINSDATE").Value = objNode.Attributes("DEINSDATE").Value.Trim()
                    '  objNode.Attributes("LoggedDateTime").Value = objNode.Attributes("LoggedDateTime").Value.Trim()
                Next
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)

                    txtRecordOnCurrReco.Text = gvMiscDeInstallation.Rows.Count


                    ViewState("PrevSearching") = objInputXml.OuterXml

                    gvMiscDeInstallation.DataSource = ds.Tables("GETMISCDEINSTALLATION")
                    gvMiscDeInstallation.DataBind()

                    txtRecordOnCurrReco.Text = gvMiscDeInstallation.Rows.Count.ToString()
                    PagingCommon(objOutputXml)

                    Dim intcol As Integer = GetSortColumnIndex(gvMiscDeInstallation)
                    If ViewState("Desc") = "FALSE" Then
                        gvMiscDeInstallation.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                    End If
                    If ViewState("Desc") = "TRUE" Then
                        gvMiscDeInstallation.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                    End If

                Else
                    'objOutputXml.LoadXml("<UP_GETAGENCYSTAFFDETAILS_OUTPUT><AGENCYSTAFF LCODE='' AGENCYSTAFFID='-1' STAFFNAME='' DESIGNATION='' /><Errors Status='FALSE'><Error Code='' Description='' /></Errors></UP_GETAGENCYSTAFFDETAILS_OUTPUT>")
                    'objXmlReader = New XmlNodeReader(objOutputXml)
                    'ds.ReadXml(objXmlReader)
                    'gvMiscInstallation.DataSource = String.Empty
                    'gvMiscInstallation.DataBind()
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    gvMiscDeInstallation.DataSource = ds.Tables("GETMISCDEINSTALLATION")
                    gvMiscDeInstallation.DataBind()

                    'lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If

            Else
                lblError.Text = "Lcode is not exist."
            End If

        Catch ex As Exception
            lblError.Text = ex.Message

        End Try
    End Sub
#End Region

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            GetAgencyMiscInstallationDetails()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            GetAgencyMiscInstallationDetails()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            GetAgencyMiscInstallationDetails()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Private Function GetSortColumnIndex(ByVal grd As GridView) As Int16
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Return grd.Columns.IndexOf(field)
            End If
        Next
        Return -1
    End Function

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

    Protected Sub gvMiscDeInstallation_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvMiscDeInstallation.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvMiscDeInstallation_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvMiscDeInstallation.Sorting
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
            GetAgencyMiscInstallationDetails()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

End Class
