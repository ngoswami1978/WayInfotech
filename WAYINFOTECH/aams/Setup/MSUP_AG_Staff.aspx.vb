
Partial Class Setup_MSUP_AG_Staff
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Sub Bindata()
        Dim TabText As New ArrayList()
        Try

            TabText.Add("Agency Group")
            TabText.Add("CRS Details")
            TabText.Add("Competition")
            TabText.Add("Staff")
            TabText.Add("PC")
            TabText.Add("Contract")
            TabText.Add("Business Case")

            theTabStrip.DataSource = TabText
            theTabStrip.DataBind()

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Protected Sub theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound
        Dim Button1 As Button
        Button1 = e.Item.FindControl("Button1")
        If e.Item.ItemIndex = 3 Then
            Button1.CssClass = "headingtab"
        End If

        '@ Start of Code Added For Removing Tab On the basis of Right (Added By Abhishek)
        If Session("Security") IsNot Nothing Then
            Dim strButtonText As String
            strButtonText = Button1.Text
            Select Case strButtonText
                Case "Agency Group"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Agency Group") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "CRS Details"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Group CRSDetails") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Competition"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Group Competition") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Staff"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Group Staff") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "PC"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Group PC") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Contract"
                    'If objeAAMS.ReturnViewPermission(Session("Security"),"Group Contract") = "0" Then
                    Button1.CssClass = "displayNone"
                    ' End If
                Case "Business Case"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Business Case") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If

            End Select
        End If
        '@ End of Code Added For Removing Tab On the basis of Right (Added By Abhishek)


        Button1 = e.Item.FindControl("Button1")

        Button1.Attributes.Add("onclick", "return TabMethodAgencyGroup('" & Button1.ClientID.ToString() & "',7);")


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If Not Page.IsPostBack Then
                Bindata()
                If Request.QueryString("Chain_Code") IsNot Nothing Then
                    hdEnChainCode.Value = Request.QueryString("Chain_Code")
                    hdChainCode.Value = objED.Decrypt(hdEnChainCode.Value)
                End If

                If hdChainCode.Value <> "" Then
                    ViewData(PageOperation.Search)
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub

    Sub ViewData(ByVal Operation As Integer)
        'Input  : 
        '<UP_GETAGENCYSTAFFDETAILS_INPUT>
        '<CHAIN_CODE</CHAIN_CODE> 
        '<ResponsibleStaffID></ResponsibleStaffID>
        '</UP_GETAGENCYSTAFFDETAILS_INPUT>

        'Output :  
        '<UP_GETAGENCYSTAFFDETAILS_OUTPUT>
        '<AGENCYSTAFF LCODE=''  AGNECY=''    AGENCYSTAFFID=''     STAFFNAME=''      DESIGNATION=''     CHAIN_CODE='' />
        '<Errors Status=''>
        '<Error Code='' Description='' />
        '</Errors>
        '</UP_GETAGENCYSTAFFDETAILS_OUTPUT>





        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objAgency As New AAMS.bizTravelAgency.bzAgency
        Try
            objInputXml.LoadXml("<UP_GETAGENCYSTAFFDETAILS_INPUT><CHAIN_CODE></CHAIN_CODE><ResponsibleStaffID></ResponsibleStaffID><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></UP_GETAGENCYSTAFFDETAILS_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = hdChainCode.Value
            If Session("Security") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("ResponsibleStaffID").InnerText = objeAAMS.EmployeeID(Session("Security"))
            End If



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
                    ViewState("SortName") = "LCODE"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "LCODE" '"LOCATION_CODE"
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

            objOutputXml = objAgency.GetAgencyGroupStaffDetails(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                ViewState("AgencyName") = Nothing
                If Operation = PageOperation.Export Then
                    'Export(objOutputXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                If ds.Tables("AGENCYSTAFF").Rows.Count <> 0 Then
                    gvAgencyStaff.DataSource = ds.Tables("AGENCYSTAFF")
                    gvAgencyStaff.DataBind()

                    'Code Added For Paging And Sorting In case Of Delete The Record
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
                    hdRecordOnCurrentPage.Value = ds.Tables("AGENCYSTAFF").Rows.Count.ToString
                    txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value


                    '  SetImageForSorting(gvAgencyStaff)
                    '  Added Code To Show Image'

                    ' End of Code Added For Paging And Sorting In case Of Delete The Record
                Else
                    gvAgencyStaff.DataSource = Nothing
                    gvAgencyStaff.DataBind()
                    txtTotalRecordCount.Text = "0"
                    hdRecordOnCurrentPage.Value = "0"
                    pnlPaging.Visible = False
                End If

            Else
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            If Val(txtTotalRecordCount.Text.Trim) > 0 Then
                btnExport.Visible = True
            Else
                btnExport.Visible = False
            End If
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

#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            ViewData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            ViewData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            ViewData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub



    Protected Sub gvAgencyStaff_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvAgencyStaff.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvAgencyStaff_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvAgencyStaff.Sorting
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
            ViewData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Enum PageOperation
        Search = 1
        Export = 2
    End Enum

    Protected Sub gvAgencyStaff_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAgencyStaff.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If ViewState("AgencyName") Is Nothing Then
                ViewState("AgencyName") = e.Row.Cells(0).Text.Trim()
                Return
            Else
                If ViewState("AgencyName") = e.Row.Cells(0).Text.Trim() Then
                    e.Row.Cells(1).Text = ""
                    e.Row.Cells(0).Text = ""
                Else
                    ViewState("AgencyName") = e.Row.Cells(0).Text.Trim()
                End If
            End If
        End If
    End Sub
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objAgency As New AAMS.bizTravelAgency.bzAgency
        Try
            objInputXml.LoadXml("<UP_GETAGENCYSTAFFDETAILS_INPUT><CHAIN_CODE></CHAIN_CODE><ResponsibleStaffID></ResponsibleStaffID><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></UP_GETAGENCYSTAFFDETAILS_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = hdChainCode.Value
            If Session("Security") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("ResponsibleStaffID").InnerText = objeAAMS.EmployeeID(Session("Security"))
            End If



          

            objOutputXml = objAgency.GetAgencyGroupStaffDetails(objInputXml)

            '            <AGENCYSTAFF
            '            LCODE = "14996"                            0
            '            AGENCY = "Riya Travels & Tours Pvt Ltd"    1
            '            AGENCYSTAFFID = "11299869"                 2
            '            STAFFNAME = "JALLEEL"                      3
            '            DESIGNATION = ""                           4
            '            CHAIN_CODE = "794"                         5
            '/> 



            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                Dim strArray() As String = {"LCode", "Agency Name", "Name", "Designation"}
                Dim intArray() As Integer = {0, 1, 3, 4}
                Dim objExport As New ExportExcel
                objExport.ExportDetails(objOutputXml, "AGENCYSTAFF", intArray, strArray, ExportExcel.ExportFormat.Excel, "GroupStaffDetails.xls")
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
End Class
