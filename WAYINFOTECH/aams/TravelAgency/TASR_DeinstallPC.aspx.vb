#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data
#End Region
Partial Class TravelAgency_TASR_DeinstallPC
    Inherits System.Web.UI.Page
#Region "Page Level Variables/Objects"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim Lcode As String
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblError.Text = ""
        txt1ATotal.Text = "0"
        txtAgencyTotal.Text = "0"
        Session("PageName") = Request.Url.ToString() '"TravelAgency_TAUP_AgencyPcInstallation"
        ' This code is used for Expiration of Page From Cache
        objeAAMS.ExpirePageCache()
        ' This code is usedc for checking session handler according to user login.
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If
        If (Session("Action") IsNot Nothing) Then
            If (Session("Action").ToString().Split("|").Length >= 2) Then
                Lcode = Session("Action").ToString().Split("|").GetValue(1)
            Else
                lblError.Text = "Lcode is not exist."
                Exit Sub
            End If
        Else
            lblError.Text = "Lcode is not exist."
            Exit Sub
        End If
        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PC Deinstallation']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PC Deinstallation']").Attributes("Value").Value)
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
            ' AgencyView()
            If strBuilder(0) = "1" Then
                GetAgencyPcDeinstallDetails()
            End If
        End If
    End Sub
#Region " GetAgencyPcDeinstallDetails Procedure"
    Private Sub GetAgencyPcDeinstallDetails()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        '  Dim objNode As XmlNode
        Try
            If (Session("Action") IsNot Nothing) Then
                Lcode = Session("Action").ToString().Split("|").GetValue(1)

                Dim objbzAgencyPcDeInstall As New AAMS.bizTravelAgency.bzPCDeInstallation
                '<PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
                'objInputXml.LoadXml("<MS_GETPCINSTALLATION_INPUT><LCODE></LCODE></MS_GETPCINSTALLATION_INPUT>")
                objInputXml.LoadXml("<MS_GETPCINSTALLATION_INPUT><LCODE></LCODE><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_GETPCINSTALLATION_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = Lcode

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
                objOutputXml = objbzAgencyPcDeInstall.RptDeInstalledPC(objInputXml)

                'For Each objNode In objOutputXml.DocumentElement.SelectNodes("PCINSTALLATION")
                '    objNode.Attributes("DATE").Value = objeAAMS.ConvertDateBlank(objNode.Attributes("DATE").Value)
                '    objNode.Attributes("LoggedDateTime").Value = objeAAMS.ConvertDateBlank(objNode.Attributes("LoggedDateTime").Value)
                'Next
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    ViewState("PrevSearching") = objInputXml.OuterXml

                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    gvPcDeInstallation.DataSource = ds.Tables("PCDEINSTALLATION")
                    gvPcDeInstallation.DataBind()
                    If objOutputXml.DocumentElement.SelectSingleNode("TOTAL") IsNot Nothing Then
                        txt1ATotal.Text = objOutputXml.DocumentElement.SelectSingleNode("TOTAL").Attributes("A1PC").Value
                        txtAgencyTotal.Text = objOutputXml.DocumentElement.SelectSingleNode("TOTAL").Attributes("AGENCYPC").Value
                    End If


                    ' ##################################################################
                    '@ Code Added For Paging And Sorting In case Of 
                    ' ###################################################################
                    ' BindControlsForNavigation(2)
                    BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                    txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                    SetImageForSorting(gvPcDeInstallation)
                    ' ###################################################################
                    '@ End of Code Added For Paging And Sorting In case 
                    ' ###################################################################

                    pnlPaging.Visible = True

                Else
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    gvPcDeInstallation.DataSource = ds.Tables("PCDEINSTALLATION")
                    gvPcDeInstallation.DataBind()
                    txt1ATotal.Text = "0"
                    txtAgencyTotal.Text = "0"
                    pnlPaging.Visible = False
                    txtTotalRecordCount.Text = "0"
                    'lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If

            Else
                lblError.Text = "Lcode is not exist."
                pnlPaging.Visible = False
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            pnlPaging.Visible = False
        End Try
    End Sub
#End Region
#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            GetAgencyPcDeinstallDetails()

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            GetAgencyPcDeinstallDetails()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            GetAgencyPcDeinstallDetails()

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvPcDeInstallation_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvPcDeInstallation.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvPcDeInstallation_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvPcDeInstallation.Sorting
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
            GetAgencyPcDeinstallDetails()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
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

#End Region

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim ds As New DataSet
        Dim objExport As New ExportExcel
        '  Dim objNode As XmlNode
        Try
            If (Session("Action") IsNot Nothing) Then
                Lcode = Session("Action").ToString().Split("|").GetValue(1)

                Dim objbzAgencyPcDeInstall As New AAMS.bizTravelAgency.bzPCDeInstallation
                '<PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
                'objInputXml.LoadXml("<MS_GETPCINSTALLATION_INPUT><LCODE></LCODE></MS_GETPCINSTALLATION_INPUT>")
                objInputXml.LoadXml("<MS_GETPCINSTALLATION_INPUT><LCODE></LCODE><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_GETPCINSTALLATION_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = Lcode
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
                objOutputXml = objbzAgencyPcDeInstall.RptDeInstalledPC(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    ViewState("PrevSearching") = objInputXml.OuterXml
                    '
                    '<PCDEINSTALLATION 
                    ' INSTALLATIONDATE = "12-Jul-00"
                    ' DEINSTALLATIONDATE = "18-Dec-02"
                    ' CPUTYPE = "M10"
                    ' CPUNO = "TGSZD"
                    ' MONTYPE = "MD2"
                    ' MONNO = "MY09753T466325H2138"
                    ' KBDTYPE = "KBD"
                    ' KBDNO = "388430614009"
                    ' MSETYPE = "MSE"
                    ' MSENO = "LZB90301410"
                    ' CDRNO = ""
                    ' INSORDERNUMBER = ""
                    ' DEINSORDERNUMBER = "0"
                    ' REMARKS = ""
                    ' CHALLANNUMBER = ""
                    ' LOGGEDBY = ""
                    ' LOGGEDDATETIME = ""
                    ' ROWID = "2406"
                    ' LCODE="261" /> 

                    'Dim objExport As New ExportExcel
                    Dim strArray() As String = {"install Date", "DeInstall Date", "CPU Type", "CPU No.", "Monitor Type", "Monitor No.", "Keyboard Type", "Keyboard No.", "Mouse Type", "Mouse No.", "CDR No.", "Install Order No.", "Deinstall Order No.", "Remarks", "Challan No.", "Logged By", "Logged Datetime"}
                    Dim intArray() As Integer = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16}
                    objExport.ExportDetails(objOutputXml, "PCDEINSTALLATION", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportDeinstallPC.xls")
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub AgencyView()
        'Dim Lcode As String
        'If (Session("Action") IsNot Nothing) Then
        '    Lcode = Session("Action").ToString().Split("|").GetValue(1)
        '    Dim objInputXml, objOutputXml As New XmlDocument
        '    Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency

        '    objInputXml.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")

        '    objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = Lcode ''objED.Decrypt(Request.QueryString("Lcode").Trim())

        '    objOutputXml = objbzAgency.View(objInputXml)

        '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
        '        With objOutputXml.DocumentElement.SelectSingleNode("Agency")
        '            txtAgencyName.Text = .Attributes("NAME").Value()
        '            txtAddress.Text = .Attributes("ADDRESS").Value()
        '            txtCity.Text = .Attributes("CITY").Value()
        '            txtCountry.Text = .Attributes("COUNTRY").Value()
        '            If .Attributes("DATE_ONLINE").Value() <> "" Then
        '                txtOnlineDate.Text = objeAAMS.ConvertDate(.Attributes("DATE_ONLINE").Value).ToString("dd/MM/yyyy")
        '            End If
        '            txtOnlineStatus.Text = .Attributes("ONLINE_STATUS").Value()

        '        End With
        '    Else
        '        '  lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        '    End If
        'End If
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.OriginalString, False)
        Catch ex As Exception

        Finally

        End Try
    End Sub
End Class
