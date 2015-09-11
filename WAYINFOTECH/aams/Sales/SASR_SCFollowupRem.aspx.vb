
Partial Class Sales_SASR_SCFollowupRem
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())

            lblError.Text = "Session is expired."
            Exit Sub
        End If
        lblError.Text = ""
        BtnSave.Attributes.Add("onclick", "return MandatoryData();")
        If Not IsPostBack Then

            If Request.QueryString("VisitDate") IsNot Nothing Then
                Dim strDSRDate As String = Request.QueryString("VisitDate").ToString
                hdVisitDATE.Value = Request.QueryString("VisitDate").ToString
                lblHeading.Text = "Followup Remarks " + "Visit Date : " + strDSRDate.Substring(6, 2) + "-" + MonthName(strDSRDate.Substring(4, 2)) + "-" + strDSRDate.Substring(0, 4)
                Me.Title = lblHeading.Text
            End If

            If Request.QueryString("Lcode") IsNot Nothing Then
                hdLCode.Value = Request.QueryString("Lcode").ToString
            End If

            If Request.QueryString("DSR_VISIT_ID") IsNot Nothing Then
                hdDSR_VISIT_ID.Value = Request.QueryString("DSR_VISIT_ID").ToString
            End If
            If Request.QueryString("SCDETAILID") IsNot Nothing Then
                hdDSR_SC_DETAIL_ID.Value = Request.QueryString("SCDETAILID").ToString
            End If
            If Request.QueryString("STATUSID") IsNot Nothing Then
                If Request.QueryString("STATUSID").ToString.Trim.Split("|")(1) = "1" Then
                    BtnSave.Enabled = False
                End If
            End If

          
            BindData()
        End If

    End Sub

    Private Sub BindData()
        Dim ds As New DataSet
        Dim objXmlReader As XmlNodeReader
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzServiceCallStatus As New AAMS.bizSales.bzServiceCallStatus
        Dim strVisitType As String = ""
        Try
            objInputXml.LoadXml("<UP_VIEW_SL_SERVICECALL_FOLLOWUP_INPUT><DSR_SC_DETAIL_ID />	<DSR_VISIT_ID />	<PREDATE />	<LCODE /><RESP_1A/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></UP_VIEW_SL_SERVICECALL_FOLLOWUP_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("DSR_VISIT_ID").InnerText = hdDSR_VISIT_ID.Value
            'objInputXml.DocumentElement.SelectSingleNode("REMARKS_TYPE").InnerText = strVisitType
            objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdLCode.Value
            objInputXml.DocumentElement.SelectSingleNode("PREDATE").InnerText = hdVisitDATE.Value
            objInputXml.DocumentElement.SelectSingleNode("DSR_SC_DETAIL_ID").InnerText = hdDSR_SC_DETAIL_ID.Value

            Dim objEmpXml As New XmlDocument
            Dim UserId As String
            objEmpXml.LoadXml(Session("Security"))
            UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
            objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = UserId

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
                ViewState("SortName") = "DATETIME"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "DATETIME" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If



            'Here Back end Method Call         

            objOutputXml = objbzServiceCallStatus.View_ServiceCall_FollowUp(objInputXml)
            Try
                objInputXml.Save("c:\admin\DSRFolowRemViewInput.xml")
                objOutputXml.Save("c:\admin\DSRFolowRemViewOut.xml")
            Catch ex As Exception
            End Try


            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If objOutputXml.DocumentElement.SelectSingleNode("DSR") IsNot Nothing Then
                    With objOutputXml.DocumentElement.SelectSingleNode("DSR")
                        TxtAssignedTo.Text = .Attributes("ASSIGNEDTO").Value
                        txtCloserDate.Text = .Attributes("CLOSERDATE").Value
                        txtTargetCloserDate.Text = .Attributes("TARGETCLOSERDATE").Value
                        TxtDept.Text = .Attributes("DEPARTMENT").Value
                        txtDepttSpecific.Text = .Attributes("DEPTTSPECIFIC").Value
                        TxtServiceStatus.Text = .Attributes("STATUS").Value
                        txtDetailedDiscussion.Text = .Attributes("DETAILEDDISCISSUEREPORTED").Value

                       
                    End With
                End If


                '  '<DSR DSR_SC_DETAIL_ID='' DSR_VISIT_ID='' PREDATE='' LCODE='' DEPARTMENT='' DEPTTSPECIFIC=''
                'DETAILEDDISCISSUEREPORTED='' STATUS='' CLOSERDATE='' TARGETCLOSERDATE='' />

                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)

                If ds.Tables("DETAILS") IsNot Nothing Then
                    If ds.Tables("DETAILS").Rows.Count = 1 Then
                        If ds.Tables("DETAILS").Rows(0)("RESP_1A").ToString.Length = 0 Then
                            gvRemarks.DataSource = Nothing
                            gvRemarks.DataBind()
                            txtTotalRecordCount.Text = ""
                            pnlPaging.Visible = False
                            Exit Sub
                        End If
                    End If
                    gvRemarks.DataSource = ds.Tables("DETAILS")
                    gvRemarks.DataBind()
                    ' ##################################################################
                    '@ Code Added For Paging And Sorting In case Of 
                    ' ###################################################################
                    BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                    txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                    SetImageForSorting(gvRemarks)
                    ' ###################################################################
                    '@ End of Code Added For Paging And Sorting In case 
                    ' ###################################################################
                    pnlPaging.Visible = True
                Else
                    gvRemarks.DataSource = Nothing
                    gvRemarks.DataBind()
                    txtTotalRecordCount.Text = ""
                    pnlPaging.Visible = False
                End If




            Else
                gvRemarks.DataSource = Nothing
                gvRemarks.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                txtTotalRecordCount.Text = ""
                pnlPaging.Visible = False
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            txtTotalRecordCount.Text = ""
            pnlPaging.Visible = False
        End Try
    End Sub
    Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Dim ds As New DataSet
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim ObjbzServiceCallStatus As New AAMS.bizSales.bzServiceCallStatus
        Dim strVisitType As String = ""
        Try
            objInputXml.LoadXml("<UP_UPDATE_SL_SERVICECALL_FOLLOWUP_INPUT>	<DETAILS DSR_VISIT_ID ='' DSR_SC_DETAIL_ID='' SC_FOLLOWUP_REMARKS='' RESP_1A='' LCODE ='' PREDATE=''    /></UP_UPDATE_SL_SERVICECALL_FOLLOWUP_INPUT>")

            With objInputXml.DocumentElement.SelectSingleNode("DETAILS")
                .Attributes("DSR_VISIT_ID").Value = hdDSR_VISIT_ID.Value
                .Attributes("DSR_SC_DETAIL_ID").Value = hdDSR_SC_DETAIL_ID.Value

                .Attributes("LCODE").Value = hdLCode.Value
                .Attributes("PREDATE").Value = hdVisitDATE.Value
                .Attributes("SC_FOLLOWUP_REMARKS").Value = txtFollowupRem.Text

                Dim objEmpXml As New XmlDocument
                Dim UserId As String
                objEmpXml.LoadXml(Session("Security"))
                UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()

                .Attributes("RESP_1A").InnerText = UserId


            End With

         
            'Here Back end Method Call

            objOutputXml = ObjbzServiceCallStatus.Update_ServiceCall_FollowUp(objInputXml)

            Try
                objInputXml.Save("c:\admin\DSRFolowRemSaveInput.xml")
                objOutputXml.Save("c:\admin\DSRFolowRemSaveOut.xml")
            Catch ex As Exception
            End Try

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                BindData()
                txtFollowupRem.Text = ""
                lblError.Text = objeAAMSMessage.messUpdate
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            BindData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            BindData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try

            BindData()

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvRemarks_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvRemarks.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvRemarks_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvRemarks.Sorting
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

            BindData()


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
End Class
