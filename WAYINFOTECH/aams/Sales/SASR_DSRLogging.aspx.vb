
Partial Class Sales_SASR_DSRLogging
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Dim str As String
    Dim strBuilder As New StringBuilder
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Dim strManagerLog As String = String.Empty

        Session("PageName") = strurl
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If
        lblError.Text = ""

        Callpostdata()
        If Request.QueryString("IsManager") IsNot Nothing Then
            strManagerLog = Request.QueryString("IsManager")
            If strManagerLog = "True" Then
                lblPlannedUnplanned.Text = "I have Planned Visit, Please"
                hdIsManager.Value = "1"
                btnSearch.Style.Add("display", "none")
                BtnSave.Style.Add("display", "none")
                BtnReset.Style.Add("display", "none")
            Else
                lblPlannedUnplanned.Text = "I have Unplanned Visit, Please"
                hdIsManager.Value = "0"
            End If
        Else
            lblPlannedUnplanned.Text = "I have Unplanned Visit, Please"
            hdIsManager.Value = "0"
        End If

        grdDSRReport.PageSize = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
        Img_DSRDate.Attributes.Add("onmousedown", "SelectDate('" & txtDateOfDSR.ClientID.ToString() & "','" & Img_DSRDate.ClientID.ToString() & "');")
        btnSearch.Attributes.Add("onclick", "return  CallValidation();")
        BtnSave.Attributes.Add("onclick", "return  CallValidationForSave();")
        If Not IsPostBack Then
            Session("AgencyDSRLOGDataSource") = Nothing
            Session("AgencyDSRLOGXML") = Nothing
            Session("FinalDSRLOGSesseionXML") = Nothing
            hdBackDateAllow.Value = ""
            'Show / Hide Options
            If hdAdvanceSearch.Value.Trim = "1" Then
                btnUp.ImageUrl = "../images/up.jpg"
                PnlShowUnhideColumns.Visible = True
                hdAdvanceSearch.Value = "0"
            Else
                btnUp.ImageUrl = "../images/down.jpg"
                PnlShowUnhideColumns.Visible = False
                hdAdvanceSearch.Value = "1"
            End If
        End If
        CheckSecurity()

       

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Try
            If Session("Security") Is Nothing Then
                Response.Redirect("~/SupportPages/TimeOutSession.aspx?Logout=True", False)
                'ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession(), True)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Rating_Script_" + Me.ClientID.ToString(), objeAAMS.CheckSession(), True)
                Exit Sub
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Session("AgencyDSRLOGDataSource") = Nothing
            Session("AgencyDSRLOGXML") = Nothing
            Session("FinalDSRLOGSesseionXML") = Nothing

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                lblError.Text = "Session is expired."
                Exit Sub
            End If

            DSRLogSearch()


        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click

        Dim objOutputXml As New XmlDocument
        Dim objInputSavexml As New XmlDocument
        Dim objOutputSavexml As New XmlDocument
        Dim objsalesDSRNode, objsalesDSRNodeClone As XmlNode
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            lblError.Text = "Session is expired."
            Exit Sub
        End If

        Dim objsalesDSR As New AAMS.bizSales.bzDSR

        Dim Rowno As Integer
        Try
            If grdDSRReport.Rows.Count = 0 Then
                lblError.Text = "There is not any data for saving."
                Exit Sub
            End If

            If Session("AgencyDSRLOGXML") IsNot Nothing Then
                objOutputXml.LoadXml(Session("AgencyDSRLOGXML").ToString)
                objInputSavexml.LoadXml("<UP_SL_LOG_DSR_INPUT> <DSRLOG ROWID=''  DSR_VISIT_ID=''  PREDATE='' LCODE='' VISITED='' REASON_REMARKS=''  RESP_1A=''  PLAN_VISIT_DONE='' UNPLAN_VISIT_DONE='' UNPLANVISIT='' VISITSUBTYPE=''  DETAILEDDISCUSSIONISSUEREPORTED=''  COMPETITIONINFO_MKTINFOREMARKS='' STATUS=''  FOLLOWUPREMARKS=''  PREVIOUSREMARKS='' INTIME ='' OUTTIME='' EMPLOYEEID='' OBJ_VISITCOUNT=''  /> </UP_SL_LOG_DSR_INPUT>")
                objsalesDSRNode = objInputSavexml.DocumentElement.SelectSingleNode("DSRLOG")
                objsalesDSRNodeClone = objsalesDSRNode.CloneNode(True)


                For Rowno = 0 To grdDSRReport.Rows.Count - 1

                    Dim StrRowId As String = CType(grdDSRReport.Rows(Rowno).FindControl("hdRowId"), HiddenField).Value
                    Dim ChkVisitYesNo As CheckBox = CType(grdDSRReport.Rows(Rowno).FindControl("ChkVisitYesNo"), CheckBox)
                    Dim txtReasonNotVisitPlan As TextBox = CType(grdDSRReport.Rows(Rowno).FindControl("txtReasonNotVisitPlan"), TextBox)
                    ' Dim hdDSRVistedId As String = CType(grdDSRReport.Rows(Rowno).FindControl("hdDSRVistedId"), HiddenField).Value
                    Dim ObjVisitCount As String = CType(grdDSRReport.Rows(Rowno).FindControl("HdObjVisitCount"), HiddenField).Value
                  
                    Dim objNodeDSRLOG As XmlNode
                    If objOutputXml.DocumentElement.SelectNodes("DSRLOG[@ROWID='" + StrRowId + "']").Count > 0 Then
                        objNodeDSRLOG = objOutputXml.DocumentElement.SelectSingleNode("DSRLOG[@ROWID='" + StrRowId + "']")
                        Dim strVisitedId As String = objNodeDSRLOG.Attributes("DSR_VISIT_ID").Value
                        Dim StrUnpllanedVisit As String = objNodeDSRLOG.Attributes("UNPLANVISIT").Value
                        Dim StrAgencyName As String = objNodeDSRLOG.Attributes("AGENCY_NAME").Value
                        Dim StrLCODE As String = objNodeDSRLOG.Attributes("LCODE").Value
                        Dim StrChainCode As String = objNodeDSRLOG.Attributes("CHAIN_CODE").Value
                        Dim StrOfficeId As String = objNodeDSRLOG.Attributes("OFFICEID").Value
                        Dim StrInTime As String = objNodeDSRLOG.Attributes("INTIME").Value
                        Dim StrManagerName As String = objNodeDSRLOG.Attributes("MANAGERNAME").Value

                        If StrUnpllanedVisit.Trim.ToUpper = "TRUE" Then
                            If ChkVisitYesNo.Checked = True Then
                                If strVisitedId.Trim.Length = 0 Then
                                    lblError.Text = "Visit Details for Unplanned  visit is mandaory for the agency  [" + StrAgencyName + "]"
                                    Exit Sub
                                End If
                            Else

                            End If

                        Else ' Planned Visited
                            If ChkVisitYesNo.Checked = True Then
                                If strVisitedId.Trim.Length = 0 Or StrInTime.Trim.Length = 0 Then
                                    lblError.Text = "Visit Details for planned visit  is mandaory for the agency  [" + StrAgencyName + "]"
                                    Exit Sub
                                End If

                            Else
                                If txtReasonNotVisitPlan.Text.Trim.Length = 0 Then
                                    lblError.Text = "Reason for not planned visit  is mandatory for the agency  [" + StrAgencyName + "]" + ""
                                    Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                                    scriptManager.SetFocus(txtReasonNotVisitPlan.ClientID)
                                    Exit Sub
                                End If
                                If txtReasonNotVisitPlan.Text.Trim.Length > 500 Then
                                    Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                                    lblError.Text = "Remark can't  be greater than 500"
                                    scriptManager.SetFocus(txtReasonNotVisitPlan.ClientID)
                                    Exit Sub
                                End If
                            End If

                        End If


                        '@ Start of Making Input xml                        

                        With objsalesDSRNodeClone
                            .Attributes("ROWID").Value = StrRowId
                            .Attributes("DSR_VISIT_ID").Value = strVisitedId
                            .Attributes("PREDATE").Value = objNodeDSRLOG.Attributes("PREDATE").Value
                            .Attributes("LCODE").Value = StrLCODE
                            '  .Attributes("OFFICEID").Value = StrOfficeId
                            '   .Attributes("CHAIN_CODE").Value = StrChainCode

                            .Attributes("INTIME").Value = objNodeDSRLOG.Attributes("INTIME").Value
                            .Attributes("OUTTIME").Value = objNodeDSRLOG.Attributes("OUTTIME").Value

                            If ChkVisitYesNo.Checked = True Then
                                .Attributes("VISITED").Value = "Y"
                                .Attributes("PLAN_VISIT_DONE").Value = "1"
                                .Attributes("UNPLAN_VISIT_DONE").Value = "0"
                                If StrUnpllanedVisit.Trim.ToUpper = "TRUE" Then
                                    .Attributes("UNPLAN_VISIT_DONE").Value = "1"
                                    .Attributes("PLAN_VISIT_DONE").Value = "0"
                                End If
                            Else
                                .Attributes("VISITED").Value = "N"
                                .Attributes("REASON_REMARKS").Value = txtReasonNotVisitPlan.Text
                                .Attributes("PLAN_VISIT_DONE").Value = "0"
                                .Attributes("UNPLAN_VISIT_DONE").Value = "0"
                                .Attributes("INTIME").Value = ""
                                .Attributes("OUTTIME").Value = ""
                            End If
                            .Attributes("RESP_1A").Value = objNodeDSRLOG.Attributes("RESP_1A").Value
                          
                           

                            .Attributes("UNPLANVISIT").Value = StrUnpllanedVisit
                            If Not Session("LoginSession") Is Nothing Then
                                .Attributes("EMPLOYEEID").Value = Session("LoginSession").ToString().Split("|")(0)
                            End If
                            .Attributes("OBJ_VISITCOUNT").Value = ObjVisitCount

                        End With
                        objInputSavexml.DocumentElement.AppendChild(objsalesDSRNodeClone)
                        objsalesDSRNodeClone = objsalesDSRNode.CloneNode(True)
                        '@ End  of Making Input xml 

                    End If
                Next
                If objInputSavexml.DocumentElement.SelectNodes("DSRLOG[@ROWID='']").Count > 0 Then
                    objInputSavexml.DocumentElement.RemoveChild(objInputSavexml.DocumentElement.SelectSingleNode("DSRLOG[@ROWID='']"))
                End If


                objOutputSavexml = objsalesDSR.UpdateVisitMaster(objInputSavexml)
                Try
                    objInputSavexml.Save("C:\DSRMasterSaveLogInput.xml")
                    objOutputSavexml.Save("C:\DSRMasterSaveLogOutput.xml")
                Catch ex As Exception

                End Try

                If objOutputSavexml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    DSRLogSearch(objInputSavexml.DocumentElement.SelectSingleNode("DSRLOG").Attributes("PREDATE").Value)
                    CheckSecurity()
                    lblError.Text = objeAAMSMessage.messUpdate '
                Else
                    lblError.Text = objOutputSavexml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If

            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub BtnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Try
            Response.Redirect(Request.Url.ToString, False)
        Catch ex As Exception

        End Try

    End Sub
    Private Sub DSRLogSearch()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbzDSR As New AAMS.bizSales.bzDSR
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            Dim objSecurityXml As New XmlDocument
            lblError.Text = ""

            objInputXml.LoadXml("<SL_SEARCH_LOG_DSR_INPUT><PREDATE/>		<RESP_1A/><EMPLOYEEID/><MANAGER></MANAGER></SL_SEARCH_LOG_DSR_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("PREDATE").InnerText = objeAAMS.ConvertTextDate(txtDateOfDSR.Text)

            Dim objEmpXml As New XmlDocument
            Dim UserId As String
            objEmpXml.LoadXml(Session("Security"))
            UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
            objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = UserId
            objInputXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = UserId

            objInputXml.DocumentElement.SelectSingleNode("MANAGER").InnerText = hdIsManager.Value

            objOutputXml = objbzDSR.Search(objInputXml)
            ' objOutputXml.Load("C:\SL_SEARCH_LOG_DSR_OUTPUT.xml")
            Try
                objOutputXml.Save("c:\DSRSearchLogOut.xml")
                objInputXml.Save("c:\DSRSearchLogIn.xml")
            Catch ex As Exception

            End Try
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                ViewState("PrevSearching") = objInputXml.OuterXml

                grdDSRReport.DataSource = Nothing
                grdDSRReport.DataBind()

                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)

                Session("AgencyDSRLOGDataSource") = ds.Tables("DSRLOG")
                Session("AgencyDSRLOGXML") = objOutputXml.OuterXml
                Session("FinalDSRLOGSesseionXML") = objOutputXml.OuterXml

                grdDSRReport.DataSource = ds.Tables("DSRLOG")
                ddlPageNumber.SelectedValue = 1
                grdDSRReport.PageIndex = 0
                txtRecordCount.Text = ds.Tables("DSRLOG").Rows.Count.ToString()
                '    txtTotalTarget.Text = hdTotal.Value
                Sorting("")
                BindControlsForNavigation(grdDSRReport.PageCount)
                'pnlPaging.Visible = True
                '
            Else
                grdDSRReport.DataSource = Nothing
                grdDSRReport.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
                ' PnlShowUnhideColumns.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub DSRLogSearch(ByVal Predate As Integer)

        Session("AgencyDSRLOGDataSource") = Nothing
        Session("AgencyDSRLOGXML") = Nothing
        Session("FinalDSRLOGSesseionXML") = Nothing

        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbzDSR As New AAMS.bizSales.bzDSR
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            Dim objSecurityXml As New XmlDocument
            lblError.Text = ""

            objInputXml.LoadXml("<SL_SEARCH_LOG_DSR_INPUT><PREDATE/>		<RESP_1A/><EMPLOYEEID/><MANAGER></MANAGER></SL_SEARCH_LOG_DSR_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("PREDATE").InnerText = Predate

            Dim objEmpXml As New XmlDocument
            Dim UserId As String
            objEmpXml.LoadXml(Session("Security"))
            UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
            objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = UserId
            objInputXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = UserId

            objInputXml.DocumentElement.SelectSingleNode("MANAGER").InnerText = hdIsManager.Value

            objOutputXml = objbzDSR.Search(objInputXml)
            ' objOutputXml.Load("C:\SL_SEARCH_LOG_DSR_OUTPUT.xml")
            Try
                objOutputXml.Save("c:\DSRSearchLogOut.xml")
                objInputXml.Save("c:\DSRSearchLogIn.xml")
            Catch ex As Exception

            End Try
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                ViewState("PrevSearching") = objInputXml.OuterXml


                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)

                Session("AgencyDSRLOGDataSource") = ds.Tables("DSRLOG")
                Session("AgencyDSRLOGXML") = objOutputXml.OuterXml
                Session("FinalDSRLOGSesseionXML") = objOutputXml.OuterXml

                grdDSRReport.DataSource = ds.Tables("DSRLOG")
                ddlPageNumber.SelectedValue = 1
                grdDSRReport.PageIndex = 0
                txtRecordCount.Text = ds.Tables("DSRLOG").Rows.Count.ToString()
                '    txtTotalTarget.Text = hdTotal.Value
                Sorting()
                BindControlsForNavigation(grdDSRReport.PageCount)
                'pnlPaging.Visible = True
                '
            Else
                grdDSRReport.DataSource = Nothing
                grdDSRReport.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False

            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
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
                If ViewState("Direction").ToString.ToUpper = "ASC" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Direction").ToString.ToUpper = "DESC" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If
            End If
        Next
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        lblError.Text = ""
        Try
            ' UpdatesessionWhenMovethepage()
            grdDSRReport.PageIndex = ddlPageNumber.SelectedValue - 1
            Sorting()
            BindControlsForNavigation(grdDSRReport.PageCount)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

      
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            '  UpdatesessionWhenMovethepage()

            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            lblError.Text = ""
            grdDSRReport.PageIndex = ddlPageNumber.SelectedValue - 1
            Sorting()
            BindControlsForNavigation(grdDSRReport.PageCount)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    Private Sub BindControlsForNavigation(ByVal CrrentPageNo As String)
        'pnlPaging.Visible = True
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
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        '  UpdatesessionWhenMovethepage()
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            lblError.Text = ""
            grdDSRReport.PageIndex = ddlPageNumber.SelectedValue - 1
            Sorting()
            BindControlsForNavigation(grdDSRReport.PageCount)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
        
       
    End Sub

    Private Sub Sorting(Optional ByVal UpdateRemark = "YES")
        Dim dt As DataTable
        Try
            If UpdateRemark = "YES" Then
                UpdateSessionWhenRearkIsnotUpdatedInSession()
            End If

            If Session("AgencyDSRLOGDataSource") IsNot Nothing Then
                dt = CType(Session("AgencyDSRLOGDataSource"), DataTable)
                grdDSRReport.DataSource = dt
                grdDSRReport.DataBind()

                Dim TotalWidth As Int64
                TotalWidth = 0
                Dim TotalLeftWidth As Int64
                TotalLeftWidth = 0

                Dim InvisibleColumnCount As Integer = 0

                For intclmn As Integer = 0 To grdDSRReport.Columns.Count - 1
                    If grdDSRReport.HeaderRow.Cells(intclmn).Visible = True Then
                        TotalWidth = TotalWidth + grdDSRReport.Columns(intclmn).ItemStyle.Width.Value

                        If intclmn <= 17 Then
                            TotalLeftWidth = TotalLeftWidth + grdDSRReport.Columns(intclmn).ItemStyle.Width.Value
                        End If
                    Else
                        InvisibleColumnCount = InvisibleColumnCount + 1
                    End If
                Next
                grdDSRReport.Width = TotalWidth
                '  PnlShowUnhideColumns.Visible = True

                'SetImageForSorting(grdAgencyTarget)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdDSRReport_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdDSRReport.RowCommand

        Dim objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As DataSet

        Try
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                lblError.Text = "Session is expired."
                Exit Sub
            End If
            If Session("AgencyDSRLOGXML") IsNot Nothing Then
                objOutputXml.LoadXml(Session("AgencyDSRLOGXML").ToString)
                If e.CommandName = "Deletex" Then
                    If objOutputXml.DocumentElement.SelectNodes("DSRLOG[@ROWID='" + e.CommandArgument.ToString.Trim + "']").Count > 0 Then
                        objOutputXml.DocumentElement.RemoveChild(objOutputXml.DocumentElement.SelectSingleNode("DSRLOG[@ROWID='" + e.CommandArgument.ToString.Trim + "']"))
                        Session("AgencyDSRLOGXML") = objOutputXml.OuterXml
                    End If
                    If objOutputXml.DocumentElement.SelectNodes("DSRLOG").Count > 0 Then
                        objXmlReader = New XmlNodeReader(objOutputXml)
                        ds = New DataSet
                        ds.ReadXml(objXmlReader)
                        Session("AgencyDSRLOGDataSource") = ds.Tables("DSRLOG")
                        Session("FinalDSRLOGSesseionXML") = objOutputXml.OuterXml
                        Session("AgencyDSRLOGXML") = objOutputXml.OuterXml
                        Sorting()
                    Else
                        grdDSRReport.DataSource = Nothing
                        grdDSRReport.DataBind()
                        'PnlShowUnhideColumns.Visible = False
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdDSRReport_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDSRReport.RowDataBound
        Dim objSecurityXml As New XmlDocument
        Dim LnkVisitDetail As LinkButton
        Dim LnkDelete As LinkButton
        Dim objOutputXml As New XmlDocument
        Try

            If e.Row.RowType = DataControlRowType.Header Then
                If Session("FinalDSRLOGSesseionXML") IsNot Nothing Then
                    objOutputXml.LoadXml(Session("FinalDSRLOGSesseionXML").ToString)
                    If objOutputXml.DocumentElement.SelectSingleNode("Permission") IsNot Nothing Then
                        hdBackDateAllow.Value = objOutputXml.DocumentElement.SelectSingleNode("Permission").Attributes("Allowed").Value.Trim.ToUpper
                    End If
                End If
            End If

            '@ Start of  Code For Visible /Invisible Items
            If e.Row.RowType = DataControlRowType.Header Or e.Row.RowType = DataControlRowType.DataRow Or e.Row.RowType = DataControlRowType.Footer Then
                'Dim hdSelectYear, hdSelectMonth As HiddenField
                'hdSelectYear = CType(grdAgencyTarget.Rows(1).FindControl("hdYear"), HiddenField)
                'hdSelectMonth = CType(grdAgencyTarget.Rows(1).FindControl("hdMonth"), HiddenField)
                'If Session("FinalSesseionXML") IsNot Nothing Then
                '    objInputXml.LoadXml(Session("FinalSesseionXML").ToString)
                '    If DateTime.DaysInMonth(objInputXml.DocumentElement.SelectSingleNode("TARGET").Attributes("YEAR").Value, objInputXml.DocumentElement.SelectSingleNode("TARGET").Attributes("MONTH").Value) = 30 Then
                '        e.Row.Cells(48).Visible = False
                '    End If
                '    If DateTime.DaysInMonth(objInputXml.DocumentElement.SelectSingleNode("TARGET").Attributes("YEAR").Value, objInputXml.DocumentElement.SelectSingleNode("TARGET").Attributes("MONTH").Value) = 29 Then
                '        e.Row.Cells(47).Visible = False
                '        e.Row.Cells(48).Visible = False

                '    End If
                '    If DateTime.DaysInMonth(objInputXml.DocumentElement.SelectSingleNode("TARGET").Attributes("YEAR").Value, objInputXml.DocumentElement.SelectSingleNode("TARGET").Attributes("MONTH").Value) = 28 Then
                '        e.Row.Cells(46).Visible = False
                '        e.Row.Cells(47).Visible = False
                '        e.Row.Cells(48).Visible = False
                '    End If

                'End If



                If ChkLcode.Checked = True Then
                    e.Row.Cells(1).Visible = True
                Else
                    e.Row.Cells(1).Visible = False
                End If

                If ChkChaincode.Checked = True Then
                    e.Row.Cells(2).Visible = True
                Else
                    e.Row.Cells(2).Visible = False
                End If

                If ChkAddress.Checked = True Then
                    e.Row.Cells(4).Visible = True
                Else
                    e.Row.Cells(4).Visible = False
                End If

                If ChkCity.Checked = True Then
                    e.Row.Cells(5).Visible = True
                Else
                    e.Row.Cells(5).Visible = False
                End If

                If ChkCateg.Checked = True Then
                    e.Row.Cells(6).Visible = True
                Else
                    e.Row.Cells(6).Visible = False
                End If

                If ChkMIDT.Checked = True Then
                    e.Row.Cells(8).Visible = True
                Else
                    e.Row.Cells(8).Visible = False
                End If

                If ChkBIDT.Checked = True Then
                    e.Row.Cells(9).Visible = True
                Else
                    e.Row.Cells(9).Visible = False
                End If

                If ChkSalesObj.Checked = True Then
                    e.Row.Cells(15).Visible = True
                Else
                    e.Row.Cells(15).Visible = False
                End If

                If ChkBCommit.Checked = True Then
                    e.Row.Cells(10).Visible = True
                Else
                    e.Row.Cells(10).Visible = False
                End If

                If ChkMinSeg.Checked = True Then
                    e.Row.Cells(11).Visible = True
                Else
                    e.Row.Cells(11).Visible = False
                End If

                If ChkPastMotive.Checked = True Then
                    e.Row.Cells(12).Visible = True
                Else
                    e.Row.Cells(12).Visible = False
                End If

                If ChkSalesObj.Checked = True Then
                    e.Row.Cells(15).Visible = True
                Else
                    e.Row.Cells(15).Visible = False
                End If

                If ChkPlanVisitDone.Checked = True Then
                    e.Row.Cells(16).Visible = True
                Else
                    e.Row.Cells(16).Visible = False
                End If

                If ChkUnPlanVisitDone.Checked = True Then
                    e.Row.Cells(17).Visible = True
                Else
                    e.Row.Cells(17).Visible = False
                End If

                'If ChkVisitSubType.Checked = True Then
                '    e.Row.Cells(18).Visible = True
                'Else
                '    e.Row.Cells(18).Visible = False
                'End If


                If ChkDesig.Checked = True Then
                    e.Row.Cells(25).Visible = True
                Else
                    e.Row.Cells(25).Visible = False
                End If

                If ChkPersonMet.Checked = True Then
                    e.Row.Cells(26).Visible = True
                Else
                    e.Row.Cells(26).Visible = False
                End If

                'If ChkPendingIssue.Checked = True Then
                '    e.Row.Cells(21).Visible = True
                'Else
                '    e.Row.Cells(21).Visible = False
                'End If

                If ChkLogDate.Checked = True Then
                    e.Row.Cells(27).Visible = True
                Else
                    e.Row.Cells(27).Visible = False
                End If

                If ChkAcBYManger.Checked = True Then
                    e.Row.Cells(30).Visible = True
                Else
                    e.Row.Cells(30).Visible = False
                End If

                If ChkAcBYRepManger.Checked = True Then
                    e.Row.Cells(31).Visible = True
                Else
                    e.Row.Cells(31).Visible = False
                End If



            End If


            '@ End of  Code For Visible /Invisible Items

            If e.Row.RowType = DataControlRowType.DataRow Then

                LnkDelete = e.Row.FindControl("LnkDelete")

                'Dim txtTarget As TextBox = CType(e.Row.FindControl("txtTarget"), TextBox)
                'txtTarget.Enabled = True
                'Dim VisitDate As String = txtDateOfDSR.Text.Trim
                Dim txtReasonNotVisitPlan As TextBox = CType(e.Row.FindControl("txtReasonNotVisitPlan"), TextBox)
                Dim ChkVisitYesNo As CheckBox
                Dim hdDSRVistedId, hdRowId, HdResID, HdUnpllanedVisit, HdVisitYesNo, HdLcode, HdChainCode, HdPreDate As HiddenField

                Dim HdPlannedVisitDone, HdUnPlannedVisitDone As HiddenField
                HdPlannedVisitDone = CType(e.Row.FindControl("HdPlannedVisitDone"), HiddenField)
                HdUnPlannedVisitDone = CType(e.Row.FindControl("HdUnPlannedVisitDone"), HiddenField)
                Dim LblPlannedVisitDone, LblUnPlannedVisitDone As Label
                LblPlannedVisitDone = CType(e.Row.FindControl("LblPlannedVisitDone"), Label)
                LblUnPlannedVisitDone = CType(e.Row.FindControl("LblUnPlannedVisitDone"), Label)
                LblPlannedVisitDone.Text = HdPlannedVisitDone.Value
                LblUnPlannedVisitDone.Text = HdUnPlannedVisitDone.Value

                hdDSRVistedId = CType(e.Row.FindControl("hdDSRVistedId"), HiddenField)
                HdResID = CType(e.Row.FindControl("HdResID"), HiddenField)
                hdRowId = CType(e.Row.FindControl("hdRowId"), HiddenField)
                HdUnpllanedVisit = CType(e.Row.FindControl("HdUnpllanedVisit"), HiddenField)

                HdVisitYesNo = CType(e.Row.FindControl("HdVisitYesNo"), HiddenField)
                HdLcode = CType(e.Row.FindControl("HdLcode"), HiddenField)
                HdChainCode = CType(e.Row.FindControl("HdChainCode"), HiddenField)
                HdPreDate = CType(e.Row.FindControl("HdPreDate"), HiddenField)

                Dim VisitDate As String = HdPreDate.Value

                ChkVisitYesNo = CType(e.Row.FindControl("ChkVisitYesNo"), CheckBox)

                If hdDSRVistedId.Value.Trim.Length > 0 Then
                    ChkVisitYesNo.Enabled = False
                End If



                If HdVisitYesNo.Value.Trim.ToUpper = "Y" Then
                    ChkVisitYesNo.Checked = True
                    txtReasonNotVisitPlan.CssClass = "textboxgrey"
                    txtReasonNotVisitPlan.ReadOnly = True
                    txtReasonNotVisitPlan.Text = ""
                Else
                    ChkVisitYesNo.Checked = False
                    txtReasonNotVisitPlan.ReadOnly = False
                    txtReasonNotVisitPlan.CssClass = "textbox"
                End If

                LnkDelete.Visible = False

                If HdUnpllanedVisit.Value.Trim.ToString.ToUpper = "TRUE" Then
                    ChkVisitYesNo.Checked = True
                    ChkVisitYesNo.Enabled = False
                    txtReasonNotVisitPlan.CssClass = "textboxgrey"
                    txtReasonNotVisitPlan.ReadOnly = True
                    If hdDSRVistedId.Value.Trim.Length = 0 Then  'If Visit Details is not Added for Unplanned Visit then it will be deleted otherwise not deleted
                        LnkDelete.Visible = True
                    End If
                End If

                ' ChkVisitYesNo.Attributes.Add("onclick", "javascript:return OpenVisitYESNO('" + hdRowId.Value + "','" + hdDSRVistedId.Value + "','" + HdUnpllanedVisit.Value + "','" + HdLcode.Value + "','" + txtReasonNotVisitPlan.ClientID + "','" + ChkVisitYesNo.ClientID + "','" + HdPlannedVisitDone.ClientID + "','" + HdUnPlannedVisitDone.ClientID + "')")

                ChkVisitYesNo.Attributes.Add("GetRowId", e.Row.RowIndex.ToString)
                LnkVisitDetail = e.Row.FindControl("LnkVisitDetail")
                LnkVisitDetail.Attributes.Add("onclick", "javascript:return OpenVistDetails('" + hdRowId.Value + "','" + hdDSRVistedId.Value + "','" + HdUnpllanedVisit.Value + "','" + HdResID.Value + "','" + HdLcode.Value + "','" + HdChainCode.Value + "' ,'" + VisitDate + "','" + hdIsManager.Value + "')")

                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DSR Logging']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DSR Logging']").Attributes("Value").Value)
                        If strBuilder(2) = "0" Then
                            BtnSave.Enabled = False
                        Else
                            BtnSave.Enabled = True
                        End If
                    Else
                        BtnSave.Enabled = False
                    End If
                Else
                    BtnSave.Enabled = True
                End If



                If HdVisitYesNo.Value.Trim.ToUpper = "Y" Then
                    LnkVisitDetail.Enabled = True
                Else
                    LnkVisitDetail.Enabled = False
                    LnkVisitDetail.Attributes.Clear()
                End If

                '###### New Code ############################################################################
                Try

                    '<DSR_TARGET_DAYS_CHECK>True</DSR_TARGET_DAYS_CHECK> 
                    '<DSR_TARGET_DAYS>5</DSR_TARGET_DAYS> 

                    Dim ObjXmlNodeDSR_TARGET_DAYS_CHECK As XmlNode
                    Dim ObjXmlNodeDSR_TARGET_DAYS As XmlNode
                    ObjXmlNodeDSR_TARGET_DAYS_CHECK = objSecurityXml.DocumentElement.SelectSingleNode("DSR_TARGET_DAYS_CHECK")
                    ObjXmlNodeDSR_TARGET_DAYS = objSecurityXml.DocumentElement.SelectSingleNode("DSR_TARGET_DAYS")
                    If ObjXmlNodeDSR_TARGET_DAYS_CHECK IsNot Nothing AndAlso ObjXmlNodeDSR_TARGET_DAYS IsNot Nothing Then
                        If ObjXmlNodeDSR_TARGET_DAYS_CHECK.InnerText.Trim.ToUpper = "TRUE" Then
                            Dim NoOfBackDaysCheckFormConfig As Integer = Val(ObjXmlNodeDSR_TARGET_DAYS.InnerText)
                            Dim StrDay As String = VisitDate.Trim.Substring(6, 2)
                            Dim StrMonth As String = VisitDate.Trim.Substring(4, 2)
                            Dim StrYear As String = VisitDate.Trim.Substring(0, 4)
                            Dim DsrDate As New Date(StrYear, StrMonth, StrDay)
                            Dim TodayDate As New Date(Now.Year, Now.Month, Now.Day)
                            Dim intdaysDiff As Long = DateDiff(DateInterval.Day, DsrDate, TodayDate)
                            If (intdaysDiff >= NoOfBackDaysCheckFormConfig Or intdaysDiff < 0) Then
                                If hdBackDateAllow.Value = "TRUE" Then
                                Else
                                    BtnSave.Enabled = False
                                    ChkVisitYesNo.Enabled = False
                                    txtReasonNotVisitPlan.CssClass = "textboxgrey"
                                    txtReasonNotVisitPlan.ReadOnly = True
                                End If
                            End If
                        Else
                            If hdBackDateAllow.Value = "TRUE" Then
                            Else
                                BtnSave.Enabled = False
                                ChkVisitYesNo.Enabled = False
                                txtReasonNotVisitPlan.CssClass = "textboxgrey"
                                txtReasonNotVisitPlan.ReadOnly = True
                            End If
                        End If
                    Else
                        If hdBackDateAllow.Value = "TRUE" Then
                        Else
                            BtnSave.Enabled = False
                            ChkVisitYesNo.Enabled = False
                            txtReasonNotVisitPlan.CssClass = "textboxgrey"
                            txtReasonNotVisitPlan.ReadOnly = True
                        End If

                    End If
                Catch ex As Exception
                End Try

                '##################################################################################
                ''###### Prev New Code ############################################################################
                'Try
                '    '@  Start of Code for Modification of DSR Back Dated
                '    If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                '        If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Back Date Entry on DSR Visit']").Count <> 0 Then
                '            strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Back Date Entry on DSR Visit']").Attributes("Value").Value)
                '            If strBuilder(1) = "1" Or strBuilder(2) = "1" Then  ' If They have back date entry then any back dated entries is allowed
                '            Else ' If They have back date entry then N no of days form config defined back dated entries is allowed

                '                '<DSR_TARGET_DAYS_CHECK>True</DSR_TARGET_DAYS_CHECK> 
                '                '<DSR_TARGET_DAYS>5</DSR_TARGET_DAYS> 

                '                Dim ObjXmlNodeDSR_TARGET_DAYS_CHECK As XmlNode
                '                Dim ObjXmlNodeDSR_TARGET_DAYS As XmlNode

                '                ObjXmlNodeDSR_TARGET_DAYS_CHECK = objSecurityXml.DocumentElement.SelectSingleNode("DSR_TARGET_DAYS_CHECK")
                '                ObjXmlNodeDSR_TARGET_DAYS = objSecurityXml.DocumentElement.SelectSingleNode("DSR_TARGET_DAYS")
                '                If ObjXmlNodeDSR_TARGET_DAYS_CHECK IsNot Nothing AndAlso ObjXmlNodeDSR_TARGET_DAYS IsNot Nothing Then
                '                    If ObjXmlNodeDSR_TARGET_DAYS_CHECK.InnerText.Trim.ToUpper = "TRUE" Then
                '                        Dim NoOfBackDaysCheckFormConfig As Integer = Val(ObjXmlNodeDSR_TARGET_DAYS.InnerText)
                '                        Dim StrDay As String = VisitDate.Trim.Substring(6, 2)
                '                        Dim StrMonth As String = VisitDate.Trim.Substring(4, 2)
                '                        Dim StrYear As String = VisitDate.Trim.Substring(0, 4)
                '                        Dim DsrDate As New Date(StrYear, StrMonth, StrDay)
                '                        Dim TodayDate As New Date(Now.Year, Now.Month, Now.Day)
                '                        Dim intdaysDiff As Long = DateDiff(DateInterval.Day, DsrDate, TodayDate)
                '                        If (intdaysDiff >= NoOfBackDaysCheckFormConfig Or intdaysDiff < 0) Then
                '                            BtnSave.Enabled = False
                '                            ChkVisitYesNo.Enabled = False
                '                            txtReasonNotVisitPlan.CssClass = "textboxgrey"
                '                            txtReasonNotVisitPlan.ReadOnly = True
                '                        End If
                '                    Else
                '                        BtnSave.Enabled = False
                '                        ChkVisitYesNo.Enabled = False
                '                        txtReasonNotVisitPlan.CssClass = "textboxgrey"
                '                        txtReasonNotVisitPlan.ReadOnly = True
                '                    End If
                '                Else
                '                    BtnSave.Enabled = False
                '                    ChkVisitYesNo.Enabled = False
                '                    txtReasonNotVisitPlan.CssClass = "textboxgrey"
                '                    txtReasonNotVisitPlan.ReadOnly = True
                '                End If
                '            End If
                '        Else
                '            BtnSave.Enabled = False
                '            ChkVisitYesNo.Enabled = False
                '            txtReasonNotVisitPlan.CssClass = "textboxgrey"
                '            txtReasonNotVisitPlan.ReadOnly = True
                '        End If
                '    End If

                '    '@  End of Code for Modification of DSR Back Dated
                'Catch ex As Exception
                'End Try

                ''##################################################################################


                '@ Start For Colorcode Implementation
                Dim hdColorCode As HiddenField
                hdColorCode = CType(e.Row.FindControl("hdColorCode"), HiddenField)
                Dim ImgColorCode As Image
                ImgColorCode = CType(e.Row.FindControl("ImgColorCode"), Image)
                If hdColorCode.Value.Trim.ToUpper = "RED" Then
                    ImgColorCode.ImageUrl = "~/Images/Redflag.gif"
                ElseIf hdColorCode.Value.Trim.ToUpper = "PINK" Then
                    ImgColorCode.ImageUrl = "~/Images/Pinkflag.gif"
                Else
                    ImgColorCode.Visible = False
                End If
                '@ End For Colorcode Implementation
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Protected Sub ChkAcBYManger_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkAcBYManger.CheckedChanged
        Sorting()
    End Sub

    Protected Sub ChkAcBYRepManger_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkAcBYRepManger.CheckedChanged
        Sorting()
    End Sub

    Protected Sub ChkBCommit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkBCommit.CheckedChanged
        Sorting()
    End Sub

    Protected Sub ChkBIDT_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkBIDT.CheckedChanged
        Sorting()
    End Sub

    Protected Sub ChkSalesObj_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkSalesObj.CheckedChanged
        Sorting()
    End Sub

    Protected Sub ChkCity_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkCity.CheckedChanged
        Sorting()
    End Sub

    Protected Sub ChkCateg_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkCateg.CheckedChanged
        Sorting()
    End Sub

    Protected Sub ChkChaincode_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkChaincode.CheckedChanged
        Sorting()
    End Sub

    Protected Sub ChkAddress_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkAddress.CheckedChanged
        Sorting()
    End Sub


    Protected Sub ChkDesig_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkDesig.CheckedChanged
        Sorting()
    End Sub

    Protected Sub ChkLogDate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkLogDate.CheckedChanged
        Sorting()
    End Sub

    Protected Sub ChkMIDT_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkMIDT.CheckedChanged
        Sorting()
    End Sub

    Protected Sub ChkMinSeg_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkMinSeg.CheckedChanged
        Sorting()
    End Sub

    Protected Sub ChkPastMotive_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkPastMotive.CheckedChanged
        Sorting()
    End Sub

    'Protected Sub ChkPendingIssue_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkPendingIssue.CheckedChanged
    '    Sorting()
    'End Sub

    Protected Sub ChkPersonMet_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkPersonMet.CheckedChanged
        Sorting()
    End Sub

    Protected Sub ChkPlanVisitDone_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkPlanVisitDone.CheckedChanged
        Sorting()
    End Sub

    Protected Sub ChkUnPlanVisitDone_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkUnPlanVisitDone.CheckedChanged
        Sorting()
    End Sub

    'Protected Sub ChkVisitSubType_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkVisitSubType.CheckedChanged
    '    Sorting()
    'End Sub

    Protected Sub ChkLcode_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkLcode.CheckedChanged
        Sorting()
    End Sub
    

#Region "CheckSecurity()::This method is used for security check."
    Private Sub CheckSecurity()
        Try

            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DSR Logging']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DSR Logging']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                    If strBuilder(2) = "0" Then  ' I have to Ask
                        BtnSave.Enabled = False
                    End If
                Else
                    Response.Redirect("~/NoRights.aspx")
                    Exit Sub
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            '@ Start of Right for seenh the BIDT/MIDt/Daily Booking data
            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        ChkBIDT.Visible = False
                        ChkBIDT.Checked = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DailyBookings']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DailyBookings']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        ChkPastMotive.Visible = False
                        ChkPastMotive.Checked = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If


            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        ChkMIDT.Visible = False
                        ChkMIDT.Checked = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            '@ End  of Right for seenh the BIDT/MIDt/Daily Booking data
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub BtnAppendUnplannedData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAppendUnplannedData.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objTarget As New AAMS.bizSales.bzAgencyTarget
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            If Session("AgencyDSRLOGXML") IsNot Nothing Then
                objOutputXml.LoadXml(Session("AgencyDSRLOGXML").ToString)
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                Session("AgencyDSRLOGDataSource") = ds.Tables("DSRLOG")
                Session("AgencyDSRLOGXML") = objOutputXml.OuterXml
                Session("FinalDSRLOGSesseionXML") = objOutputXml.OuterXml
                UpdateSessionWhenRearkIsnotUpdatedInSession()
                Sorting()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnUpdateRemark_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateRemark.Click
        Try
            UpdateSessionWhenRearkIsnotUpdatedInSession()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub UpdateSessionWhenRearkIsnotUpdatedInSession()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objTarget As New AAMS.bizSales.bzAgencyTarget
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim RequiredChangedSeesion As Boolean = False
        Try
            If Session("AgencyDSRLOGXML") IsNot Nothing Then
                objOutputXml.LoadXml(Session("AgencyDSRLOGXML").ToString)
                '@ Start of  If remark is not updating in session for plan not visited then update the remark
                For Rowno As Integer = 0 To grdDSRReport.Rows.Count - 1
                    Dim StrRowId As String = CType(grdDSRReport.Rows(Rowno).FindControl("hdRowId"), HiddenField).Value
                    Dim ChkVisitYesNo As CheckBox = CType(grdDSRReport.Rows(Rowno).FindControl("ChkVisitYesNo"), CheckBox)
                    Dim txtReasonNotVisitPlan As TextBox = CType(grdDSRReport.Rows(Rowno).FindControl("txtReasonNotVisitPlan"), TextBox)
                    If ChkVisitYesNo.Checked = False Then
                        Dim objNodeDSRLOG As XmlNode
                        If objOutputXml.DocumentElement.SelectNodes("DSRLOG[@ROWID='" + StrRowId + "']").Count > 0 Then
                            objNodeDSRLOG = objOutputXml.DocumentElement.SelectSingleNode("DSRLOG[@ROWID='" + StrRowId + "']")
                            If txtReasonNotVisitPlan.Text.Trim.Length > 0 Then
                                If objNodeDSRLOG.Attributes("REASON_REMARKS").Value.ToString.Trim <> txtReasonNotVisitPlan.Text.ToString.Trim Then
                                    objNodeDSRLOG.Attributes("REASON_REMARKS").Value = txtReasonNotVisitPlan.Text
                                    RequiredChangedSeesion = True
                                End If
                            End If
                        End If
                    Else
                        ' txtReasonNotVisitPlan.Text = ""

                    End If
                Next
                If RequiredChangedSeesion = True Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    Session("AgencyDSRLOGDataSource") = ds.Tables("DSRLOG")
                    Session("AgencyDSRLOGXML") = objOutputXml.OuterXml
                    Session("FinalDSRLOGSesseionXML") = objOutputXml.OuterXml
                End If
                '@ End of  If remark is not updating in session for plan not visited then update the remark              
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub Callpostdata()
        ChkLcode.Attributes.Add("onclick", "return PostData();")
        ChkChaincode.Attributes.Add("onclick", "return PostData();")
        ChkCateg.Attributes.Add("onclick", "return PostData();")
        ChkCity.Attributes.Add("onclick", "return PostData();")
        ChkDesig.Attributes.Add("onclick", "return PostData();")
        ChkMinSeg.Attributes.Add("onclick", "return PostData();")
        ChkBCommit.Attributes.Add("onclick", "return PostData();")
        ChkPastMotive.Attributes.Add("onclick", "return PostData();")
        ChkMIDT.Attributes.Add("onclick", "return PostData();")
        ChkBIDT.Attributes.Add("onclick", "return PostData();")
        ChkAddress.Attributes.Add("onclick", "return PostData();")
        ChkLogDate.Attributes.Add("onclick", "return PostData();")
        ChkPlanVisitDone.Attributes.Add("onclick", "return PostData();")
        ChkUnPlanVisitDone.Attributes.Add("onclick", "return PostData();")
        ChkSalesObj.Attributes.Add("onclick", "return PostData();")
        'ChkVisitSubType.Attributes.Add("onclick", "return PostData();")
        ChkPersonMet.Attributes.Add("onclick", "return PostData();")
        ChkAcBYManger.Attributes.Add("onclick", "return PostData();")
        ChkAcBYRepManger.Attributes.Add("onclick", "return PostData();")
      

    End Sub
    Protected Sub ChkVisitYesNo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                lblError.Text = "Session is expired."
                Exit Sub
            End If
            If Session("AgencyDSRLOGXML") IsNot Nothing Then
                objOutputXml.LoadXml(Session("AgencyDSRLOGXML").ToString)
                Dim ChkVisitYesNo As CheckBox = CType(sender, CheckBox)
                Dim index As Integer = Integer.Parse(ChkVisitYesNo.Attributes("GetRowId"))
                Dim gvrow As GridViewRow = grdDSRReport.Rows(index)
                Dim StrRowId As String = CType(gvrow.FindControl("hdRowId"), HiddenField).Value

                Dim HdVisitYesNo, hdDSRVistedId, HdResID, HdUnpllanedVisit, HdLcode, HdChainCode, HdPreDate As HiddenField
                hdDSRVistedId = CType(gvrow.FindControl("hdDSRVistedId"), HiddenField)
                HdResID = CType(gvrow.FindControl("HdResID"), HiddenField)
                HdUnpllanedVisit = CType(gvrow.FindControl("HdUnpllanedVisit"), HiddenField)
                HdLcode = CType(gvrow.FindControl("HdLcode"), HiddenField)
                HdChainCode = CType(gvrow.FindControl("HdChainCode"), HiddenField)
                HdPreDate = CType(gvrow.FindControl("HdPreDate"), HiddenField)
                HdVisitYesNo = CType(gvrow.FindControl("HdVisitYesNo"), HiddenField)



                Dim txtReasonNotVisitPlan As TextBox = CType(gvrow.FindControl("txtReasonNotVisitPlan"), TextBox)

                Dim LnkVisitDetail As LinkButton = CType(gvrow.FindControl("LnkVisitDetail"), LinkButton)
                LnkVisitDetail.Enabled = False

                Dim LblPlannedVisitDone As Label = CType(gvrow.FindControl("LblPlannedVisitDone"), Label)
                Dim LblUnPlannedVisitDone As Label = CType(gvrow.FindControl("LblUnPlannedVisitDone"), Label)

                Dim objNodeDSRLOG As XmlNode
                If objOutputXml.DocumentElement.SelectNodes("DSRLOG[@ROWID='" + StrRowId + "']").Count > 0 Then
                    objNodeDSRLOG = objOutputXml.DocumentElement.SelectSingleNode("DSRLOG[@ROWID='" + StrRowId + "']")
                    Dim StrUnpllanedVisit As String = objNodeDSRLOG.Attributes("UNPLANVISIT").Value

                    If ChkVisitYesNo.Checked = True Then
                        HdVisitYesNo.Value = "Y"
                        txtReasonNotVisitPlan.Text = ""
                        txtReasonNotVisitPlan.ReadOnly = True
                        txtReasonNotVisitPlan.CssClass = "textboxgrey"
                        LnkVisitDetail.Enabled = True

                        objNodeDSRLOG.Attributes("VISITED").Value = "Y"

                        objNodeDSRLOG.Attributes("PLAN_VISIT_DONE").Value = "1"
                        objNodeDSRLOG.Attributes("UNPLAN_VISIT_DONE").Value = "0"

                        LblPlannedVisitDone.Text = "1"
                        LblUnPlannedVisitDone.Text = "0"

                        If StrUnpllanedVisit.Trim.ToString.ToLower = "TRUE" Then
                            objNodeDSRLOG.Attributes("UNPLAN_VISIT_DONE").Value = "1"
                            objNodeDSRLOG.Attributes("PLAN_VISIT_DONE").Value = "0"
                            LblUnPlannedVisitDone.Text = "1"
                            LblPlannedVisitDone.Text = "0"
                        End If
                    Else
                        txtReasonNotVisitPlan.Text = objNodeDSRLOG.Attributes("REASON_REMARKS").Value

                        objNodeDSRLOG.Attributes("VISITED").Value = ""
                        If StrUnpllanedVisit.Trim.ToString.ToLower = "TRUE" Then
                            txtReasonNotVisitPlan.ReadOnly = True
                            txtReasonNotVisitPlan.CssClass = "textboxgrey"
                        Else
                            txtReasonNotVisitPlan.ReadOnly = False
                            txtReasonNotVisitPlan.CssClass = "textbox"
                        End If


                        objNodeDSRLOG.Attributes("PLAN_VISIT_DONE").Value = "0"
                        objNodeDSRLOG.Attributes("UNPLAN_VISIT_DONE").Value = "0"
                        LblPlannedVisitDone.Text = "0"
                        LblUnPlannedVisitDone.Text = "0"
                        HdVisitYesNo.Value = ""
                    End If

                    Session("AgencyDSRLOGXML") = objOutputXml.OuterXml
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    Session("AgencyDSRLOGDataSource") = ds.Tables("DSRLOG")
                    Session("AgencyDSRLOGXML") = objOutputXml.OuterXml
                    Session("FinalDSRLOGSesseionXML") = objOutputXml.OuterXml


                    Dim VisitDate As String = HdPreDate.Value
                    LnkVisitDetail.Attributes.Add("onclick", "javascript:return OpenVistDetails('" + StrRowId + "','" + hdDSRVistedId.Value + "','" + HdUnpllanedVisit.Value + "','" + HdResID.Value + "','" + HdLcode.Value + "','" + HdChainCode.Value + "','" + VisitDate + "')")

                   

                    If ChkVisitYesNo.Checked = True Then
                        LnkVisitDetail.Enabled = True
                    Else
                        LnkVisitDetail.Enabled = False
                        LnkVisitDetail.Attributes.Clear()
                    End If

                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnUp_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            If hdAdvanceSearch.Value.Trim = "0" Then

                btnUp.ImageUrl = "../images/down.jpg"
                PnlShowUnhideColumns.Visible = False
                hdAdvanceSearch.Value = "1"
            Else
                btnUp.ImageUrl = "../images/up.jpg"
                PnlShowUnhideColumns.Visible = True
                hdAdvanceSearch.Value = "0"
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Protected Sub lnkAdvance_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If hdAdvanceSearch.Value.Trim = "0" Then
                btnUp.ImageUrl = "../images/down.jpg"
                PnlShowUnhideColumns.Visible = False
                hdAdvanceSearch.Value = "1"
            Else
                btnUp.ImageUrl = "../images/up.jpg"
                PnlShowUnhideColumns.Visible = True
                hdAdvanceSearch.Value = "0"
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


End Class
