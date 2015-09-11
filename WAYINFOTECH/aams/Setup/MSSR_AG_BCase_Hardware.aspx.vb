
Partial Class Setup_MSSR_AG_BCase_Hardware
    Inherits System.Web.UI.Page

    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
                LoadData(hdChainCode.Value)
            End If

        End If
    End Sub
    Private Sub LoadData(ByVal strChainCode As String)

        Dim objInputXml As New XmlDocument
        Dim objOutPutxml As New XmlDocument
        Dim ds As New DataSet
        Dim objReadaer As XmlNodeReader
        Try

            objInputXml.LoadXml("<ISP_SEARCH_INPUT><CHAIN_CODE></CHAIN_CODE><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><EmployeeID/></ISP_SEARCH_INPUT>")



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
                ViewState("SortName") = "HARDWARE"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "HARDWARE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            ' @ Code For Setting Input Xml 


            ' objOutPutxml = objbzRequestBDR.Search(objInputXml)


            objOutPutxml.LoadXml("<PR_SEARCH_BGROUPAGENCYMIDT_OUTPUT><GROUPHARDWARE HARDWARE='A ( Existing)' UNITCOST='1900' NUM='0' TOTALCOST='0'></GROUPHARDWARE><GROUPHARDWARE HARDWARE='A (Old more than 3 years)' UNITCOST='950' NUM='0' TOTALCOST='0'></GROUPHARDWARE><GROUPHARDWARE HARDWARE='B New (Existing)' UNITCOST='' NUM='950' TOTALCOST='0'></GROUPHARDWARE><GROUPHARDWARE HARDWARE='B OLD ( Existing)' UNITCOST='0' NUM='0' TOTALCOST=''></GROUPHARDWARE><PAGE PAGE_COUNT='1' TOTAL_ROWS='4'></PAGE><Errors Status='FALSE'><Error Code='' Description='' /></Errors></PR_SEARCH_BGROUPAGENCYMIDT_OUTPUT>")

            If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If

            If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                objReadaer = New XmlNodeReader(objOutPutxml)
                ds.ReadXml(objReadaer)
                GvHardware.DataSource = ds.Tables("GROUPHARDWARE")
                GvHardware.DataBind()

                '@ Code Added For Paging And Sorting 
                pnlPaging.Visible = True
                BindControlsForNavigation(objOutPutxml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)


                txtTotalRecordCount.Text = objOutPutxml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                SetImageForSorting(GvHardware)

                '@ End of Code Added For Paging And Sorting 

            Else
                GvHardware.DataSource = Nothing
                GvHardware.DataBind()
                lblError.Text = objOutPutxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

                txtTotalRecordCount.Text = "0"

                pnlPaging.Visible = False
            End If
        Catch ex As Exception
            pnlPaging.Visible = False
            lblError.Text = ex.Message
        End Try
        'Dim objInput As New XmlDocument
        'Dim objOutPut As New XmlDocument
        'objOutPut.LoadXml("<PR_SEARCH_GROUPHARDWARE_OUTPUT><GROUPHARDWARE HARDWARE='' UNITCOST='' NUM='' TOTALCOST=''></GROUPHARDWARE></PR_SEARCH_GROUPHARDWARE_OUTPUT>")

        'Dim ds As New DataSet
        'Dim objReadaer As XmlNodeReader
        'objReadaer = New XmlNodeReader(objOutPut)
        'ds.ReadXml(objReadaer)
        'GvHardware.DataSource = ds.Tables("GROUPHARDWARE")
        'GvHardware.DataBind()
        'Try

        'Catch ex As Exception

        'End Try


    End Sub
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

            BindSubTabdata()

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub


    Sub BindSubTabdata()
        Dim TabText As New ArrayList()
        Try

            TabText.Add("Group MIDT")
            TabText.Add("Agency MIDT")
            TabText.Add("Connectivity")
            TabText.Add("Hardware")
            TabText.Add("Incentive")
            theTabSubGroupStrip.DataSource = TabText
            theTabSubGroupStrip.DataBind()

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub


    Protected Sub theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound
        Dim Button1 As Button
        Button1 = e.Item.FindControl("Button1")
        If e.Item.ItemIndex = 6 Then
            Button1.CssClass = "headingtab"
        End If
        Button1 = e.Item.FindControl("Button1")

        Button1.Attributes.Add("onclick", "return TabMethodAgencyGroup('" & Button1.ClientID.ToString() & "',7);")


    End Sub

    Protected Sub theTabSubGroupStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabSubGroupStrip.ItemDataBound
        Dim Button2 As Button
        Button2 = e.Item.FindControl("Button2")
        If e.Item.ItemIndex = 3 Then
            Button2.CssClass = "headingtab"
        End If
        Button2 = e.Item.FindControl("Button2")

        Button2.Attributes.Add("onclick", "return TabMethodAgencySubGroup('" & Button2.ClientID.ToString() & "',5);")


    End Sub



   
    Protected Sub BtnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnPrint.Click

    End Sub

  

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click

    End Sub

    Protected Sub GvHardware_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvHardware.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.Footer Then

                e.Row.Cells(2).Text = "Total"
                e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(3).Text = "0"
                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
                'e.Row.Cells(4).Text = "0"
                'e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right

            End If
        Catch ex As Exception

        End Try
       
    End Sub


#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            If hdChainCode.Value <> "" Then
                LoadData(hdChainCode.Value)
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            If hdChainCode.Value <> "" Then
                LoadData(hdChainCode.Value)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            If hdChainCode.Value <> "" Then
                LoadData(hdChainCode.Value)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    

    Protected Sub GvHardware_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvHardware.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub GvHardware_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GvHardware.Sorting
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
            If hdChainCode.Value <> "" Then
                LoadData(hdChainCode.Value)
            End If

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
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
#End Region

#Region "Code for Filter "
    'Code Added by Abhishek
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    'Code Added by Abhishek
#End Region

#Region "Code for Export "
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click

        Dim objInputXml As New XmlDocument
        Dim objOutPutxml As New XmlDocument
        Dim ds As New DataSet
        Dim objReadaer As XmlNodeReader
        Try

            objInputXml.LoadXml("<ISP_SEARCH_INPUT><CHAIN_CODE></CHAIN_CODE><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><EmployeeID/></ISP_SEARCH_INPUT>")

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "NAME"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "NAME"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            ' @ Code For Setting Input Xml 


            ' objOutPutxml = objbzRequestBDR.Search(objInputXml)


            objOutPutxml.LoadXml("<PR_SEARCH_BGROUPAGENCYMIDT_OUTPUT><BGROUPAGENCYMIDT NAME='' ADDRESS='' OFFICEID='' A='' B='' G='' P='' W='' TTP=''></BGROUPAGENCYMIDT><PAGE PAGE_COUNT='1' TOTAL_ROWS='1'></PAGE><Errors Status='FALSE'><Error Code='' Description='' /></Errors></PR_SEARCH_BGROUPAGENCYMIDT_OUTPUT>")
            If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                'objReadaer = New XmlNodeReader(objOutPutxml)
                'ds.ReadXml(objReadaer)
                'GvBAgencyMIDT.DataSource = ds.Tables("BGROUPAGENCYMIDT")
                'GvBAgencyMIDT.DataBind()

                'Dim objExport As New ExportExcel
                'Dim strArray() As String = {"LTR No", "Agency Name", "Agency Address", "Requested Type", "BDR Tickets", "Date Sent", "Status", "Airline", "Airline Aoffice Address", "Send By"}
                'Dim intArray() As Integer = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}
                'objExport.ExportDetails(objOutPutxml, "BDRLetter", intArray, strArray, ExportExcel.ExportFormat.Excel, "BDRDetails.xls")

            Else
                GvHardware.DataSource = Nothing
                GvHardware.DataBind()
                lblError.Text = objOutPutxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                txtTotalRecordCount.Text = "0"
                pnlPaging.Visible = False
            End If
        Catch ex As Exception
            pnlPaging.Visible = False
            lblError.Text = ex.Message
        End Try

    End Sub
#End Region

End Class
