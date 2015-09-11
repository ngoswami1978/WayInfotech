
Partial Class Market_MTSR_AirLineWiseMktShareDetails
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim dset As DataSet

#Region "Code for Filter "

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
         


            'If Session("Security") Is Nothing Then
            '    ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            '    Exit Sub
            'End If
            'Dim objSecurityXml As New XmlDocument
            'objSecurityXml.LoadXml(Session("Security"))
            'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Group']").Count <> 0 Then
            '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Group']").Attributes("Value").Value)
            '    End If
            'Else
            '    strBuilder = objeAAMS.SecurityCheck(31)
            'End If

            'parameter="Fmonth=" + FMonth  + "&TMonth=" + TMonth + "&FYear=" + FYear +  "&TYear=" + TYear +  "&Aoff=" + Aoff + "&LimAoff=" +  LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff + "&AirCode=" + AirCode  ;
            If (Not Page.IsPostBack) Then
                If (Request.QueryString("AirCode") IsNot Nothing) Then
                    ViewAirlineDetails()
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Sub ViewAirlineDetails()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim rorno As Integer
        Try

            Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT

           ' objInputXml.LoadXml("<PR_SEARCH_AIRLINEWISEMARKETSHAREDETAILS_INPUT> <AIRLINE_CODE></AIRLINE_CODE><BreakupType></BreakupType>  <SMonth></SMonth> <SYear></SYear>  <EMonth></EMonth>  <EYear></EYear> <Limited_To_Aoffice></Limited_To_Aoffice>  <Limited_To_OwnAagency></Limited_To_OwnAagency>  <Limited_To_Region></Limited_To_Region>  <PAGE_NO></PAGE_NO>  <PAGE_SIZE></PAGE_SIZE>  <SORT_BY></SORT_BY>  <DESC>FALSE</DESC></PR_SEARCH_AIRLINEWISEMARKETSHAREDETAILS_INPUT>")
            objInputXml.LoadXml("<PR_SEARCH_AIRLINEWISEMARKETSHAREDETAILS_INPUT> <GroupTypeID></GroupTypeID><AIRLINE_CODE></AIRLINE_CODE><CITY></CITY><COUNTRY></COUNTRY><Aoffice></Aoffice><Region></Region><ONLINE_CARRIER></ONLINE_CARRIER><BreakupType></BreakupType>  <SMonth></SMonth> <SYear></SYear>  <EMonth></EMonth>  <EYear></EYear> <Limited_To_Aoffice></Limited_To_Aoffice>  <Limited_To_OwnAagency></Limited_To_OwnAagency>  <Limited_To_Region></Limited_To_Region>  <PAGE_NO></PAGE_NO>  <PAGE_SIZE></PAGE_SIZE>  <SORT_BY></SORT_BY>  <DESC>FALSE</DESC></PR_SEARCH_AIRLINEWISEMARKETSHAREDETAILS_INPUT>")

            '<PR_SEARCH_AIRLINEWISEMARKETSHAREDETAILS_INPUT>
            ' <GroupTypeID>
            '  <AIRLINE_CODE></AIRLINE_CODE>
            '<BreakupType></BreakupType>
            '<CITY></CITY>
            '<COUNTRY></COUNTRY>
            '<Aoffice></Aoffice>
            '<Region></Region>
            '<ONLINE_CARRIER></ONLINE_CARRIER>
            '  <SMonth></SMonth>
            '  <SYear></SYear>
            '  <EMonth></EMonth>
            '  <EYear></EYear>
            '  <Limited_To_Aoffice></Limited_To_Aoffice>
            '  <Limited_To_OwnAagency></Limited_To_OwnAagency>
            '  <Limited_To_Region></Limited_To_Region>
            '  <PAGE_NO>1</PAGE_NO>
            '  <PAGE_SIZE>25</PAGE_SIZE>
            '  <SORT_BY>AgencyName</SORT_BY>
            '  <DESC>FALSE</DESC>
            '</PR_SEARCH_AIRLINEWISEMARKETSHAREDETAILS_INPUT>
            '")

            'parameter="Fmonth=" + FMonth  + "&TMonth=" + TMonth + "&FYear=" + FYear +  "&TYear=" + TYear +  "&Aoff=" + Aoff + "&LimAoff=" +  LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff + "&AirCode=" + AirCode  ;

            If (Request.QueryString("AirCode") IsNot Nothing) Then

                objInputXml.DocumentElement.SelectSingleNode("AIRLINE_CODE").InnerText = Request.QueryString("AirCode").ToString
            End If

            '@ Added on 11/02/10
            If (Request.QueryString("City") IsNot Nothing) Then

                objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = Request.QueryString("City").ToString
            End If

            If (Request.QueryString("Country") IsNot Nothing) Then

                objInputXml.DocumentElement.SelectSingleNode("COUNTRY").InnerText = Request.QueryString("Country").ToString
            End If

            If (Request.QueryString("Region") IsNot Nothing) Then

                objInputXml.DocumentElement.SelectSingleNode("Region").InnerText = Request.QueryString("Region").ToString
            End If

            If (Request.QueryString("OnCarr") IsNot Nothing) Then

                objInputXml.DocumentElement.SelectSingleNode("ONLINE_CARRIER").InnerText = Request.QueryString("OnCarr").ToString
            End If

            If (Request.QueryString("Aoff") IsNot Nothing) Then

                objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = Request.QueryString("Aoff").ToString
            End If

            If (Request.QueryString("GType") IsNot Nothing) Then

                objInputXml.DocumentElement.SelectSingleNode("GroupTypeID").InnerText = Request.QueryString("GType").ToString
            End If

            If (Request.QueryString("BreakupType") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("BreakupType").InnerText = Request.QueryString("BreakupType").ToString
            End If




            '@ Added on 11/02/10





            If (Request.QueryString("Fmonth") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("SMonth").InnerText = Request.QueryString("Fmonth").ToString

            End If
            If (Request.QueryString("TMonth") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("EMonth").InnerText = Request.QueryString("TMonth").ToString
            End If
            If (Request.QueryString("FYear") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("SYear").InnerText = Request.QueryString("FYear").ToString
            End If
            If (Request.QueryString("TYear") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("EYear").InnerText = Request.QueryString("TYear").ToString
            End If

            If (Request.QueryString("Aoff") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = Request.QueryString("Aoff").ToString
            End If
            If (Request.QueryString("LimOwnOff") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = Request.QueryString("LimOwnOff").ToString
            End If

            If (Request.QueryString("LimReg") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = Request.QueryString("LimReg").ToString
            End If

            If (Request.QueryString("BreakupType") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("BreakupType").InnerText = Request.QueryString("BreakupType").ToString
            End If


            '@ Coding For Paging Ansd sorting
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

            'End Code for paging and sorting

            'Here Back end Method Call
            objOutputXml = objbzMIDT.AirLineWiseMarketShareDetails(objInputXml)

            ' objOutputXml.LoadXml("<PR_SEARCH_AIRLINEWISEMARKETSHAREDETAILS_OUTPUT> <AIRLINEWISEMARKETSHAREDETAILS LCODE='' AgencyName='sdfdsfds dsfh sdfui dsfui dsfui sdfjk fdsjk fdssfjk ' city='' A='2' B='' G='' P='' W='' TOTAL='' /> <PAGE_TOTAL A='6' B='6' G='' P='' W='' TOTAL='6'/>   <PAGE PAGE_COUNT='1' TOTAL_ROWS='1'/>  <Errors Status='FALSE'> <Error Code='' Description='' />    </Errors></PR_SEARCH_AIRLINEWISEMARKETSHAREDETAILS_OUTPUT>")


            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)

                dset = New DataSet
                dset = ds

                gvAirlineDetails.DataSource = ds.Tables("AIRLINEWISEMARKETSHAREDETAILS").DefaultView
                gvAirlineDetails.DataBind()

                ' ##################################################################
                '@ Code Added For Paging And Sorting In case Of 
                ' ###################################################################
                'BindControlsForNavigation(2)
                BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                SetImageForSorting(gvAirlineDetails)
                ' ###################################################################
                '@ End of Code Added For Paging And Sorting In case 
                ' ###################################################################

                pnlPaging.Visible = True

            Else
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                pnlPaging.Visible = False
                gvAirlineDetails.DataSource = Nothing
                gvAirlineDetails.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            pnlPaging.Visible = False
        End Try
    End Sub


    Protected Sub gvAirlineDetails_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAirlineDetails.RowCommand
        'Dim strjscript As String = "<script language=""javascript"">"
        'strjscript &= "window.returnValue='" + e.CommandArgument.ToString() + "'; window.close();"
        'strjscript = strjscript & "</script" & ">"
        'Me.litAgencyGroup.Text = strjscript
    End Sub
#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            If (Request.QueryString("AirCode") IsNot Nothing) Then
                ViewAirlineDetails()
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
            If (Request.QueryString("AirCode") IsNot Nothing) Then
                ViewAirlineDetails()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            If (Request.QueryString("AirCode") IsNot Nothing) Then
                ViewAirlineDetails()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvAirlineDetails_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvAirlineDetails.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvAirlineDetails_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvAirlineDetails.Sorting
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
            If (Request.QueryString("AirCode") IsNot Nothing) Then
                ViewAirlineDetails()
            End If

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

    Protected Sub gvAirlineDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAirlineDetails.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                ' e.Row.Cells(3).Text = ""
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                If dset IsNot Nothing Then
                    e.Row.Cells(4).Text = " Total : "
                    e.Row.Cells(6).Text = dset.Tables("PAGE_TOTAL").Rows(0)("A").ToString()
                    e.Row.Cells(7).Text = dset.Tables("PAGE_TOTAL").Rows(0)("B").ToString()
                    e.Row.Cells(8).Text = dset.Tables("PAGE_TOTAL").Rows(0)("G").ToString()
                    e.Row.Cells(9).Text = dset.Tables("PAGE_TOTAL").Rows(0)("P").ToString()
                    e.Row.Cells(10).Text = dset.Tables("PAGE_TOTAL").Rows(0)("W").ToString()
                    e.Row.Cells(11).Text = dset.Tables("PAGE_TOTAL").Rows(0)("TOTAL").ToString()
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
End Class
