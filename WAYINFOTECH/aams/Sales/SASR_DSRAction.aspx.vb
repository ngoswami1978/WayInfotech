
Partial Class Sales_SASR_DSRAction
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Dim str As String
    Dim strBuilder As New StringBuilder

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If
        lblError.Text = ""

        imgDateFrom.Attributes.Add("onmousedown", "SelectDate('" & txtDateFrom.ClientID.ToString() & "','" & imgDateFrom.ClientID.ToString() & "');")
        imgDateTo.Attributes.Add("onmousedown", "SelectDate('" & txtDateTo.ClientID.ToString() & "','" & imgDateTo.ClientID.ToString() & "');")

        btnSearch.Attributes.Add("onclick", "return  ValidateForm();")
        btnExport.Attributes.Add("onclick", "return  ExportData();")
        drpVisitSubType.Attributes.Add("onchange", "OpenStrategicCallType();")

        Callpostdata()
      

        CheckSecurity()

        If Not IsPostBack Then
            objeAAMS.BindDropDown(drpRegion, "REGION1", True, 3)
            FillDepartment()
            fillAssignedBy()
            fillAssignedTO()
            fillStatus()
            Session("DSRACTIONXML") = Nothing
            'Show / Hide Options
            If hdAdvanceSearch.Value.Trim = "1" Then
                btnUp.ImageUrl = "../images/up.jpg"
                PnlShowUnhideColumns.Visible = True
            Else
                btnUp.ImageUrl = "../images/down.jpg"
                PnlShowUnhideColumns.Visible = False
            End If
        End If
        If drpVisitSubType.SelectedValue <> "2" Then
            drpStrategicCallType.Enabled = False
            drpStrategicCallType.SelectedValue = ""
        Else
            drpStrategicCallType.Enabled = True
        End If

        If (HdDSRCODE.Value <> "" And hdDSR_DETAILID.Value <> "") Then
            DeleteRecords()
        End If
    End Sub
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
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
    Private Sub FillDepartment()
        Dim objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzDepartment As New AAMS.bizMaster.bzDepartment
        Try
            objOutputXml = New XmlDocument
            objOutputXml = objbzDepartment.List
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                ChkLstDepartment.DataSource = ds.Tables("DEPARTMENT")
                ChkLstDepartment.DataTextField = "Department_Name"
                ChkLstDepartment.DataValueField = "DepartmentID"
                ChkLstDepartment.DataBind()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Sub fillAssignedBy()
        DlstAssignedBy.Items.Clear()
        Dim objSales As New AAMS.bizTravelAgency.bzAgencyTarget
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objInputXml, objOutputXml As New XmlDocument
        Try
            Dim objEmpXml As New XmlDocument
            Dim UserId As String
            objEmpXml.LoadXml(Session("Security"))
            UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
            objInputXml.LoadXml("<PR_AGENCYEMPLIST_INPUT><City_Id></City_Id></PR_AGENCYEMPLIST_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("City_Id").InnerText = ""
            objOutputXml = objSales.GetAgencyEmpList(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                DlstAssignedBy.DataSource = ds.Tables("TARGET")
                DlstAssignedBy.DataTextField = "SalesManName"
                DlstAssignedBy.DataValueField = "SalesManId"
                DlstAssignedBy.DataBind()
            End If
            DlstAssignedBy.Items.Insert(0, New ListItem("--All--", ""))


        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Public Sub fillAssignedTO()
        DlstAssignedTo.Items.Clear()
        Dim objbzDSRAction As New AAMS.bizSales.bzDSRAction
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objInputXml, objOutputXml As New XmlDocument
        Try
          
            objOutputXml = objbzDSRAction.ListAssignedTo()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                DlstAssignedTo.DataSource = ds.Tables("ASSIGN")
                DlstAssignedTo.DataTextField = "ASSIGNEDTONAME"
                DlstAssignedTo.DataValueField = "ASSIGNEDTO"
                DlstAssignedTo.DataBind()
            Else
                DlstAssignedTo.DataSource = Nothing
                DlstAssignedTo.DataBind()

            End If
            DlstAssignedTo.Items.Insert(0, New ListItem("--All--", ""))


        Catch ex As Exception
            lblError.Text = ex.Message
            DlstAssignedTo.DataSource = Nothing
            DlstAssignedTo.DataBind()
        Finally
        End Try
    End Sub
    Public Sub fillStatus()
        drpStatus.Items.Clear()
        Dim objbzDSRAction As New AAMS.bizSales.bzDSRAction
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objInputXml, objOutputXml As New XmlDocument
        drpStatus.Items.Clear()
        Try
            objOutputXml = objbzDSRAction.ListDSRActionStatus()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                Dim finalOutputxml As New XmlDocument
                finalOutputxml.LoadXml("<SL_LIST_DSR_ACTION_STATUS_OUTPUT><Errors Status='FALSE'>  <Error Code='' Description='' />   </Errors>   </SL_LIST_DSR_ACTION_STATUS_OUTPUT>")

                If drpVisitSubType.SelectedValue = "1" Then
                    For Each objxmlnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("STATUS")
                        If objxmlnode.Attributes("STATUSID").Value.Trim("|")(0) = "1" Then 'Service
                            finalOutputxml.DocumentElement.AppendChild(finalOutputxml.ImportNode(objxmlnode, True))
                        End If
                    Next
                ElseIf drpVisitSubType.SelectedValue = "2" Then
                    If drpStrategicCallType.SelectedValue = "1" Then
                        For Each objxmlnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("STATUS")
                            If objxmlnode.Attributes("STATUSID").Value.Trim("|")(0) = "2" Then  'Target
                                finalOutputxml.DocumentElement.AppendChild(finalOutputxml.ImportNode(objxmlnode, True))
                            End If
                        Next
                    ElseIf drpStrategicCallType.SelectedValue = "2" Then
                        For Each objxmlnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("STATUS")
                            If objxmlnode.Attributes("STATUSID").Value.Trim("|")(0) = "3" Then 'Retention
                                finalOutputxml.DocumentElement.AppendChild(finalOutputxml.ImportNode(objxmlnode, True))
                            End If
                        Next
                    ElseIf drpStrategicCallType.SelectedValue = "3" Then
                        For Each objxmlnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("STATUS")
                            If objxmlnode.Attributes("STATUSID").Value.Trim("|")(0) = "4" Then 'Air Non Air
                                finalOutputxml.DocumentElement.AppendChild(finalOutputxml.ImportNode(objxmlnode, True))
                            End If
                        Next
                    Else
                        For Each objxmlnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("STATUS")
                            If objxmlnode.Attributes("STATUSID").Value.Trim("|")(0) = "2" Or objxmlnode.Attributes("STATUSID").Value.Trim("|")(0) = "3" Or objxmlnode.Attributes("STATUSID").Value.Trim("|")(0) = "4" Then
                                finalOutputxml.DocumentElement.AppendChild(finalOutputxml.ImportNode(objxmlnode, True))
                            End If
                        Next
                    End If
                  
                Else
                    finalOutputxml.LoadXml(objOutputXml.OuterXml)
                End If


                If finalOutputxml.DocumentElement.SelectNodes("STATUS").Count > 0 Then

                    objXmlReader = New XmlNodeReader(finalOutputxml)
                    ds.ReadXml(objXmlReader)
                    drpStatus.DataSource = ds.Tables("STATUS")
                    drpStatus.DataTextField = "STATUS_NAME"
                    drpStatus.DataValueField = "STATUSID"
                    drpStatus.DataBind()
                Else
                    drpStatus.DataSource = Nothing
                    drpStatus.DataBind()
                End If
            Else
                drpStatus.DataSource = Nothing
                drpStatus.DataBind()
            End If
            drpStatus.Items.Insert(0, New ListItem("--All--", ""))


        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
      
    End Sub

    Protected Sub drpVisitSubType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpVisitSubType.SelectedIndexChanged
        fillStatus()
    End Sub

    Protected Sub drpStrategicCallType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpStrategicCallType.SelectedIndexChanged
        fillStatus()
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Session("DSRACTIONXML") = Nothing
            DSRAction()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub


    Protected Sub BtnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.ToString, False)
        Catch ex As Exception
        End Try

    End Sub
    Private Sub DSRAction()

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzDSRAction As New AAMS.bizSales.bzDSRAction
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        lblError.Text = ""
        Try

            objInputXml.LoadXml("<SL_SEARCH_DSR_ACTION_INPUT> <AGENCY_NAME>  </AGENCY_NAME>    <OFFICEID>  </OFFICEID>  <DSRCODE>  </DSRCODE> <LCODE>  </LCODE>    <ASSIGNEDBY>  </ASSIGNEDBY>  <VISITSUBTYPE>  </VISITSUBTYPE>  <STRATEGICTYPE>  </STRATEGICTYPE>    <ASSIGNEDTO>  </ASSIGNEDTO>  <DATEFROM></DATEFROM>  <DATETO></DATETO>  <STATUS></STATUS>   <DEPARTMENT>  </DEPARTMENT>   <REGION>  </REGION>   <EMPLOYEEID></EMPLOYEEID>  <RESP1A />  <PAGE_NO></PAGE_NO>  <PAGE_SIZE></PAGE_SIZE>  <SORT_BY></SORT_BY>  <DESC></DESC>  </SL_SEARCH_DSR_ACTION_INPUT>")


            '<SL_SEARCH_DSR_ACTION_INPUT>
            ' <AGENCY_NAME>
            '  </AGENCY_NAME>
            '    <OFFICEID>
            '  </OFFICEID>
            '  <DSRCODE>
            '  </DSRCODE>
            ' <LCODE>
            '  </LCODE> 
            '   <ASSIGNEDBY>
            '  </ASSIGNEDBY>
            '  <VISITSUBTYPE>
            '  </VISITSUBTYPE>
            '  <STRATEGICTYPE>
            '  </STRATEGICTYPE>  
            '  <ASSIGNEDTO>
            '  </ASSIGNEDTO>
            '  <DATEFROM>20110802</DATEFROM>
            '  <DATETO>20110808</DATETO>
            '  <STATUS>FALSE</STATUS>
            '   <DEPARTMENT>
            '  </DEPARTMENT> 
            '  <REGION>
            '  </REGION> 
            '  <EMPLOYEEID>21</EMPLOYEEID>
            '  <RESP1A />
            '  <PAGE_NO></PAGE_NO>
            '  <PAGE_SIZE></PAGE_SIZE>
            '  <SORT_BY></SORT_BY>
            '  <DESC></DESC>  
            '</SL_SEARCH_DSR_ACTION_INPUT>
            objInputXml.DocumentElement.SelectSingleNode("AGENCY_NAME").InnerText = txtAgencyName.Text
            objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerText = txtOfficeID.Text
            objInputXml.DocumentElement.SelectSingleNode("DSRCODE").InnerText = txtDRSCode.Text
            objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = txtLcode1.Text
            objInputXml.DocumentElement.SelectSingleNode("ASSIGNEDBY").InnerText = DlstAssignedBy.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("VISITSUBTYPE").InnerText = drpVisitSubType.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("STRATEGICTYPE").InnerText = drpStrategicCallType.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("ASSIGNEDTO").InnerText = DlstAssignedTo.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("DATEFROM").InnerText = objeAAMS.ConvertTextDate(txtDateFrom.Text)
            objInputXml.DocumentElement.SelectSingleNode("DATETO").InnerText = objeAAMS.ConvertTextDate(txtDateTo.Text)
            objInputXml.DocumentElement.SelectSingleNode("STATUS").InnerText = drpStatus.SelectedValue
            Dim strDepartment As String = ""
            For Each Item As ListItem In ChkLstDepartment.Items
                If Item.Selected = True Then
                    If strDepartment.Trim.Length = 0 Then
                        strDepartment = Item.Value
                    Else
                        strDepartment = strDepartment + "," + Item.Value
                    End If
                End If
            Next

            If (strDepartment.Trim.Length > 0) Then
                objInputXml.DocumentElement.SelectSingleNode("DEPARTMENT").InnerText = strDepartment
            End If




            If (drpRegion.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = drpRegion.SelectedItem.Text
            End If

         
            Dim objEmpXml As New XmlDocument
            Dim UserId As String
            objEmpXml.LoadXml(Session("Security"))
            UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
            objInputXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = UserId

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
                ViewState("SortName") = "NAME"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "NAME"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "TRUE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

          
            '  objOutputXml.Load("c:\SL_SEARCH_DSR_ACTION_OUPUT.xml")

            objOutputXml = objbzDSRAction.Search(objInputXml)
            Try
                objOutputXml.Save("c:\SearchDSRActionOuput.xml")
                objInputXml.Save("c:\SearchDSRActionInput.xml")
            Catch ex As Exception
            End Try
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                Session("DSRACTIONXML") = objOutputXml.OuterXml
                BindData()
            Else
                gvDSRAction.DataSource = Nothing
                gvDSRAction.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                txtRecordCount.Text = "0"
                pnlPaging.Visible = False
                '  PnlShowUnhideColumns.Visible = False

            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
            pnlPaging.Visible = False
            ' PnlShowUnhideColumns.Visible = False
        End Try
    End Sub
    Protected Sub gvDSRAction_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDSRAction.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message

        End Try
    End Sub

    Protected Sub gvDSRAction_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvDSRAction.Sorting
        Try
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ViewState("Desc") = "TRUE"
            Else
                If ViewState("SortName") = SortName Then
                    If ViewState("Desc") = "TRUE" Then
                        ViewState("Desc") = "FALSE"
                    Else
                        ViewState("Desc") = "TRUE"
                    End If
                Else
                    ViewState("SortName") = SortName
                    ViewState("Desc") = "TRUE"
                End If
            End If

            DSRAction()
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
                If ViewState("SortName").ToString().Trim() <> "COLORCODE" Then
                    Dim intcol As Integer = grd.Columns.IndexOf(field)
                    If ViewState("Desc") = "FALSE" Then
                        grd.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                    End If
                    If ViewState("Desc") = "TRUE" Then
                        grd.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                    End If
                End If

            End If
        Next
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged

        Try
            lblError.Text = ""
            gvDSRAction.PageIndex = ddlPageNumber.SelectedValue - 1

            DSRAction()
        Catch ex As Exception
            lblError.Text = ex.Message

        End Try

        ' BindControlsForNavigation(gvDSRAction.PageCount)
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            lblError.Text = ""
            gvDSRAction.PageIndex = ddlPageNumber.SelectedValue - 1
            DSRAction()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try


        ' BindControlsForNavigation(gvDSRAction.PageCount)
    End Sub
    Private Sub BindControlsForNavigation(ByVal CrrentPageNo As String)
        pnlPaging.Visible = True
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

        Try
            '  UpdatesessionWhenMovethepage()
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            lblError.Text = ""
            gvDSRAction.PageIndex = ddlPageNumber.SelectedValue - 1

            DSRAction()
            '  BindControlsForNavigation(gvDSRAction.PageCount)
        Catch ex As Exception
            lblError.Text = ex.Message

        End Try
    End Sub

    Protected Sub gvDSRAction_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDSRAction.RowDataBound
        Dim objSecurityXml As New XmlDocument
        Dim lnkEdit As New LinkButton
        Dim lnkDelete As New LinkButton
        Dim objInputXml As New XmlDocument
        Dim objOutputXml As New XmlDocument
        Try

            '@ Start of  Code For Visible /Invisible Items
            If e.Row.RowType = DataControlRowType.Header Or e.Row.RowType = DataControlRowType.DataRow Or e.Row.RowType = DataControlRowType.Footer Then

                If ChkLcode.Checked = True Then
                    e.Row.Cells(2).Visible = True
                Else
                    e.Row.Cells(2).Visible = False
                End If

                If ChkAssignedBy.Checked = True Then
                    e.Row.Cells(9).Visible = True
                Else
                    e.Row.Cells(9).Visible = False
                End If

                If ChkDepartment.Checked = True Then
                    e.Row.Cells(10).Visible = True
                Else
                    e.Row.Cells(10).Visible = False
                End If
                If ChkAssignedTo.Checked = True Then
                    e.Row.Cells(11).Visible = True
                Else
                    e.Row.Cells(11).Visible = False
                End If
                If ChkVisitSubType.Checked = True Then
                    e.Row.Cells(12).Visible = True
                Else
                    e.Row.Cells(12).Visible = False
                End If
                If ChkRegion.Checked = True Then
                    e.Row.Cells(13).Visible = True
                Else
                    e.Row.Cells(13).Visible = False
                End If
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim VisitDate As String = ""
                Dim hdDSRID, hdDSR_DETAILID, HdLcode, HdStatus, HdVISITSUBTYPE, HdSTRATEGICTYPE, hdPreDate As HiddenField
                hdDSRID = CType(e.Row.FindControl("hdDSRID"), HiddenField)
                hdDSR_DETAILID = CType(e.Row.FindControl("hdDSR_DETAILID"), HiddenField)
                HdLcode = CType(e.Row.FindControl("HdLcode"), HiddenField)
                HdVISITSUBTYPE = CType(e.Row.FindControl("HdVISITSUBTYPE"), HiddenField)
                HdSTRATEGICTYPE = CType(e.Row.FindControl("HdSTRATEGICTYPE"), HiddenField)
                hdPreDate = CType(e.Row.FindControl("hdPreDate"), HiddenField)
                HdStatus = CType(e.Row.FindControl("HdStatus"), HiddenField)

                VisitDate = hdPreDate.Value
                VisitDate = objeAAMS.GetDateFormat(VisitDate.ToString.Trim, "dd-MM-yy", "yyyyMMdd", "-")

                'If e.Row.Cells(4).Text.Trim.Length = 8 Then
                '    e.Row.Cells(4).Text = ConvertToCalenderDate(e.Row.Cells(4).Text)
                'End If


                If e.Row.Cells(5).Text.Trim.Length = 8 Then
                    e.Row.Cells(5).Text = ConvertToCalenderDate(e.Row.Cells(5).Text)
                End If

                lnkEdit = e.Row.FindControl("lnkEdit")
                lnkDelete = e.Row.FindControl("lnkDelete")

                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DSR ACTION']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DSR ACTION']").Attributes("Value").Value)
                        If strBuilder(2) = "0" Then
                            lnkEdit.Enabled = False
                        Else
                            lnkEdit.Attributes.Add("onclick", "javascript:return OpenDetails('" + hdDSRID.Value + "','" + hdDSR_DETAILID.Value + "','" + HdVISITSUBTYPE.Value + "','" + HdSTRATEGICTYPE.Value + "','" + HdLcode.Value + "','" + VisitDate + "')")
                        End If

                        If strBuilder(3) = "0" Then
                            lnkDelete.Enabled = False
                        Else
                            lnkDelete.Attributes.Add("onclick", "javascript:return DeleteDetails('" + hdDSRID.Value + "','" + hdDSR_DETAILID.Value + "')")
                        End If
                    Else
                        lnkEdit.Enabled = False
                        lnkDelete.Enabled = False
                    End If
                Else
                    lnkDelete.Enabled = True
                    lnkEdit.Enabled = True
                    lnkEdit.Attributes.Add("onclick", "javascript:return OpenDetails('" + hdDSRID.Value + "','" + hdDSR_DETAILID.Value + "','" + HdVISITSUBTYPE.Value + "','" + HdSTRATEGICTYPE.Value + "','" + HdLcode.Value + "','" + VisitDate + "')")
                    lnkDelete.Attributes.Add("onclick", "javascript:return DeleteDetails('" + hdDSRID.Value + "','" + hdDSR_DETAILID.Value + "')")
                End If

                'If HdStatus.Value.Trim.ToUpper = "Y" Then
                '    lnkEdit.Enabled = True
                'Else
                '    lnkEdit.Enabled = False
                '    lnkEdit.Attributes.Clear()
                'End If
                lnkDelete.Enabled = False
                lnkDelete.Attributes.Clear()

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
        End Try
    End Sub


    Private Sub BindData()
        If Session("Security") Is Nothing Then
            lblError.Text = "Session is expired."
            Exit Sub
        End If

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objTarget As New AAMS.bizSales.bzAgencyTarget
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim TotalWidth As Int64
        TotalWidth = 0
        Dim TotalLeftWidth As Int64
        TotalLeftWidth = 0
        Try
            If Session("DSRACTIONXML") IsNot Nothing Then
                objOutputXml.LoadXml(Session("DSRACTIONXML").ToString)
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                If ds.Tables("DSRACTIONDETAILS").Rows.Count > 0 Then
                    gvDSRAction.DataSource = ds.Tables("DSRACTIONDETAILS")
                    gvDSRAction.DataBind()
                    '@ Code Added For Paging And Sorting 
                    pnlPaging.Visible = True
                    BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                    txtRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                    SetImageForSorting(gvDSRAction)
                    Session("DSRACTIONXML") = objOutputXml.OuterXml

                    Dim InvisibleColumnCount As Integer = 0

                    For intclmn As Integer = 0 To gvDSRAction.Columns.Count - 1
                        If gvDSRAction.HeaderRow.Cells(intclmn).Visible = True Then
                            TotalWidth = TotalWidth + gvDSRAction.Columns(intclmn).ItemStyle.Width.Value
                        End If
                    Next

                    gvDSRAction.Width = TotalWidth
                    ' PnlShowUnhideColumns.Visible = True
                End If
            Else

            End If
        Catch ex As Exception
            lblError.Text = ex.Message

        End Try
    End Sub
#Region "CheckSecurity()::This method is used for security check."
    Private Sub CheckSecurity()
        Try

            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DSR ACTION']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DSR ACTION']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If

                    If strBuilder(4) = "0" Then
                        btnExport.Enabled = False
                    Else
                        btnExport.Enabled = True
                    End If


                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub Exportdata(ByVal objOutputXml As XmlDocument)
      
    End Sub
#Region "DeleteRecords()"
    Private Sub DeleteRecords()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzGroupCategorySales As New AAMS.bizSales.bzGroupCategorySales
        Try
            objInputXml.LoadXml("<SL_DELETEDSRACTION_INPUT><DSRCODE></DSRCODE><DSR_DETAILID></DSR_DETAILID></SL_DELETEDSRACTION_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("DSRCODE").InnerText = HdDSRCODE.Value
            objInputXml.DocumentElement.SelectSingleNode("DSR_DETAILID").InnerText = hdDSR_DETAILID.Value
            HdDSRCODE.Value = ""
            hdDSR_DETAILID.Value = ""
            objOutputXml = objbzGroupCategorySales.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messDelete
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
            BindData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "ConvertToCalenderDate(ByVal strDateInInterger As String) As String"
    Private Function ConvertToCalenderDate(ByVal strDateInInterger As String) As String
        Try
            If strDateInInterger.Trim.Length = 8 Then
                strDateInInterger = strDateInInterger.Substring(6, 2) + "/" + strDateInInterger.Substring(4, 2) + "/" + strDateInInterger.Substring(0, 4)
            End If
        Catch ex As Exception
        End Try
        Return strDateInInterger
    End Function
#End Region

#End Region

    Protected Sub Btnexp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnexp.Click
        ExportData()
    End Sub

    Private Sub exportData()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzDSRAction As New AAMS.bizSales.bzDSRAction
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        lblError.Text = ""
        Dim NotShowColumnArray As New ArrayList
        Session("NotShowColumnArrayForDSRAction") = Nothing
        Session("DSRActionExport") = Nothing
        Try

            objInputXml.LoadXml("<SL_SEARCH_DSR_ACTION_INPUT> <AGENCY_NAME>  </AGENCY_NAME>    <OFFICEID>  </OFFICEID>  <DSRCODE>  </DSRCODE> <LCODE>  </LCODE>    <ASSIGNEDBY>  </ASSIGNEDBY>  <VISITSUBTYPE>  </VISITSUBTYPE>  <STRATEGICTYPE>  </STRATEGICTYPE>    <ASSIGNEDTO>  </ASSIGNEDTO>  <DATEFROM></DATEFROM>  <DATETO></DATETO>  <STATUS></STATUS>   <DEPARTMENT>  </DEPARTMENT>   <REGION>  </REGION>   <EMPLOYEEID></EMPLOYEEID>  <RESP1A />  <PAGE_NO></PAGE_NO>  <PAGE_SIZE></PAGE_SIZE>  <SORT_BY></SORT_BY>  <DESC></DESC>  </SL_SEARCH_DSR_ACTION_INPUT>")


            '<SL_SEARCH_DSR_ACTION_INPUT>
            ' <AGENCY_NAME>
            '  </AGENCY_NAME>
            '    <OFFICEID>
            '  </OFFICEID>
            '  <DSRCODE>
            '  </DSRCODE>
            ' <LCODE>
            '  </LCODE> 
            '   <ASSIGNEDBY>
            '  </ASSIGNEDBY>
            '  <VISITSUBTYPE>
            '  </VISITSUBTYPE>
            '  <STRATEGICTYPE>
            '  </STRATEGICTYPE>  
            '  <ASSIGNEDTO>
            '  </ASSIGNEDTO>
            '  <DATEFROM>20110802</DATEFROM>
            '  <DATETO>20110808</DATETO>
            '  <STATUS>FALSE</STATUS>
            '   <DEPARTMENT>
            '  </DEPARTMENT> 
            '  <REGION>
            '  </REGION> 
            '  <EMPLOYEEID>21</EMPLOYEEID>
            '  <RESP1A />
            '  <PAGE_NO></PAGE_NO>
            '  <PAGE_SIZE></PAGE_SIZE>
            '  <SORT_BY></SORT_BY>
            '  <DESC></DESC>  
            '</SL_SEARCH_DSR_ACTION_INPUT>
            objInputXml.DocumentElement.SelectSingleNode("AGENCY_NAME").InnerText = txtAgencyName.Text
            objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerText = txtOfficeID.Text
            objInputXml.DocumentElement.SelectSingleNode("DSRCODE").InnerText = txtDRSCode.Text
            objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = txtLcode1.Text
            objInputXml.DocumentElement.SelectSingleNode("ASSIGNEDBY").InnerText = DlstAssignedBy.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("VISITSUBTYPE").InnerText = drpVisitSubType.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("STRATEGICTYPE").InnerText = drpStrategicCallType.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("ASSIGNEDTO").InnerText = DlstAssignedTo.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("DATEFROM").InnerText = objeAAMS.ConvertTextDate(txtDateFrom.Text)
            objInputXml.DocumentElement.SelectSingleNode("DATETO").InnerText = objeAAMS.ConvertTextDate(txtDateTo.Text)
            objInputXml.DocumentElement.SelectSingleNode("STATUS").InnerText = drpStatus.SelectedValue
            Dim strDepartment As String = ""
            For Each Item As ListItem In ChkLstDepartment.Items
                If Item.Selected = True Then
                    If strDepartment.Trim.Length = 0 Then
                        strDepartment = Item.Value
                    Else
                        strDepartment = strDepartment + "," + Item.Value
                    End If
                End If
            Next

            If (strDepartment.Trim.Length > 0) Then
                objInputXml.DocumentElement.SelectSingleNode("DEPARTMENT").InnerText = strDepartment
            End If


          
            If (drpRegion.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = drpRegion.SelectedItem.Text
            End If


            Dim objEmpXml As New XmlDocument
            Dim UserId As String
            objEmpXml.LoadXml(Session("Security"))
            UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
            objInputXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = UserId

            ' @ Code For Setting Input Xml 


            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "NAME"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "NAME"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "TRUE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If
            ' objOutputXml.Load("c:\SL_SEARCH_DSR_ACTION_OUPUT.xml")

            objOutputXml = objbzDSRAction.Search(objInputXml)
            Try
                objOutputXml.Save("c:\SearchDSRActionOuput.xml")
                objInputXml.Save("c:\SearchDSRActionInput.xml")
            Catch ex As Exception
            End Try
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml


                NotShowColumnArray.Add("DSR_DETAIL_ID")
                NotShowColumnArray.Add("CHAIN_CODE")
                NotShowColumnArray.Add("ADDRESS")
                NotShowColumnArray.Add("ASSIGNEDBYID")
                NotShowColumnArray.Add("ASSIGNEDTOID")
                NotShowColumnArray.Add("STRATEGICTYPE")
                NotShowColumnArray.Add("COLORCODE")

                If ChkLcode.Checked = False Then
                    NotShowColumnArray.Add("LCODE")
                End If

                If ChkAssignedBy.Checked = False Then
                    NotShowColumnArray.Add("ASSIGNEDBYNAME")
                End If

                If ChkDepartment.Checked = False Then
                    NotShowColumnArray.Add("DEPARTMENT")
                End If
                If ChkAssignedTo.Checked = False Then
                    NotShowColumnArray.Add("ASSIGNEDTONAME")
                End If
                If ChkVisitSubType.Checked = False Then
                    NotShowColumnArray.Add("VISITSUBTYPE")
                End If
                If ChkRegion.Checked = False Then
                    NotShowColumnArray.Add("REGION")
                End If


                Session("NotShowColumnArrayForDSRAction") = NotShowColumnArray
                Session("DSRActionExport") = objOutputXml.OuterXml

                Response.Redirect("SASR_AtargetExport.aspx?EXPORTDSRACTIONDATA=DSRACTION", False)


            Else
                gvDSRAction.DataSource = Nothing
                gvDSRAction.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                txtRecordCount.Text = "0"
                pnlPaging.Visible = False
                PnlShowUnhideColumns.Visible = False
                Session("NotShowColumnArrayForDSRAction") = NotShowColumnArray.Add("DSR_DETAIL_ID")

                objOutputXml.LoadXml("<SL_SEARCH_DSR_ACTION_OUPUT> <DSRACTIONDETAILS DSRCODE='' DSR_DETAIL_ID='' LCODE='' CHAIN_CODE='' NAME='' ADDRESS='' OFFICEID='' AOFFICE='' TARGET_CLOSER_DATETIME='' DSR_DATETIME='' STATUS='' ASSIGNEDBYID='' ASSIGNEDBYNAME='' ASSIGNEDTOID='' ASSIGNEDTONAME='' VISITSUBTYPE='' STRATEGICTYPE='' DEPARTMENT='' REGION='' /></SL_SEARCH_DSR_ACTION_OUPUT>")
                Session("DSRActionExport") = objOutputXml.OuterXml
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
            pnlPaging.Visible = False
            PnlShowUnhideColumns.Visible = False
        End Try
    End Sub
    Private Sub Callpostdata()
        ChkLcode.Attributes.Add("onclick", "return PostData();")
        ChkAssignedBy.Attributes.Add("onclick", "return PostData();")
        ChkVisitSubType.Attributes.Add("onclick", "return PostData();")
        ChkAssignedTo.Attributes.Add("onclick", "return PostData();")
        ChkRegion.Attributes.Add("onclick", "return PostData();")
        ChkDepartment.Attributes.Add("onclick", "return PostData();")
    End Sub
    Protected Sub ChkAssignedTo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkAssignedTo.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkDepartment_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkDepartment.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkAssignedBy_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkAssignedBy.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkLcode_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkLcode.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkRegion_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkRegion.CheckedChanged
        BindData()
    End Sub

    Protected Sub ChkVisitSubType_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkVisitSubType.CheckedChanged
        BindData()
    End Sub
    Protected Sub BtnAuoPostback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAuoPostback.Click
        BindData()
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
