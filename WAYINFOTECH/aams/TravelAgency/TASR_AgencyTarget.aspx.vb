Partial Class TravelAgency_MSSR_AgencyTarget
    Inherits System.Web.UI.Page
    Implements ICallbackEventHandler

    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim str As String
    Dim strBuilder As New StringBuilder

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
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If
        Try
            grdAgencyTarget.PageSize = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
            btnSearch.Attributes.Add("onclick", "return MandatoryFunction();")
            btnDisplay.Attributes.Add("onclick", "return MandatoryFunction();")
            btn_Select.Attributes.Add("onclick", "return MandatoryFunction();")
            chbPrevious.Attributes.Add("onClick", "return Enableall();")
            rdSummaryOption1.Attributes.Add("onClick", "radionewexisting();")
            rdSummaryOption2.Attributes.Add("onClick", "radionewexisting();")
            btnIncrease.Attributes.Add("onclick", "return CheckValidation();")
            If rdSummaryOption1.Checked = True Then
                btnIncrease.Text = "Increase"
            End If
            If rdSummaryOption2.Checked = True Then
                btnIncrease.Text = "Decrease"
            End If
            Dim m As ClientScriptManager = Me.ClientScript
            str = m.GetCallbackEventReference(Me, "args", "ReceiveServerData", "'this is context from server'")
            Dim strCallback As String = "function CallServer(args,context){" + str + ";}"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)
            drpCity.Attributes.Add("OnChange", "return fillCategoryName('drpSalesPerson');")
            hdSales.Value = Request.Form(drpSalesPerson.UniqueID)
            If Not Page.IsPostBack Then
                '  hdSearch.Value = 1

                BindAll()
                objeAAMS.BindDropDown(drpCity, "CITY", True, 3)
                btnSave.Attributes.Add("onClick", "return ValidateForm();")
                '@ Added on 12 march 2009
                fillSalesName()
                '@ Added on 12 march 2009

            End If
            If chbPrevious.Checked = False Then
                If DateTime.Now.Month = 1 Then

                    drpPreviousMonth.SelectedValue = 12
                    drpPreviousYear.SelectedValue = DateTime.Now.Year - 1
                Else
                    drpPreviousYear.SelectedValue = DateTime.Now.Year
                    drpPreviousMonth.SelectedValue = Date.Today.Month - 1
                End If
            End If
            CheckSecurity()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
#Region "CheckSecurity()::This method is used for security check."
    Private Sub CheckSecurity()
        Try

            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='AgencyTarget']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='AgencyTarget']").Attributes("Value").Value)
                End If
                If strBuilder(1) = "0" Then
                    btnSave.Enabled = False
                End If
                If strBuilder(4) = "0" Then
                    btnDisplay.Enabled = False
                End If
                If strBuilder(0) = "0" Then
                    btnSearch.Enabled = False
                    Response.Redirect("~/NoRights.aspx")
                    Exit Sub
                End If
                If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                    btnSave.Enabled = True
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
        Return str
    End Function


    Private Sub BindAll()
        Try
            Dim objSales As New AAMS.bizTravelAgency.bzAgencyTarget
            Dim i, j As Integer
            objeAAMS.BindDropDown(drpCity, "CITY", True, 3)
            For j = DateTime.Now.Year To 1990 Step -1
                drpYear.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                drpPreviousYear.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                i += 1
            Next
            drpYear.SelectedValue = DateTime.Now.Year
            'drpPreviousYear.SelectedValue = DateTime.Now.Year
            drpMonth.SelectedValue = Date.Today.Month
            'drpPreviousMonth.SelectedValue = Date.Today.Month - 1
            If drpMonth.SelectedValue = 1 Then
                drpPreviousMonth.SelectedValue = 12
                drpPreviousYear.SelectedValue = DateTime.Now.Year - 1
            Else
                drpPreviousYear.SelectedValue = DateTime.Now.Year
                drpPreviousMonth.SelectedValue = Date.Today.Month - 1
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            AgencyTargetSearch()
            fillSalesName()
            If drpSalesPerson.Items.FindByValue(hdSales.Value) IsNot Nothing Then
                drpSalesPerson.SelectedValue = hdSales.Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub AgencyTargetSearch()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objTarget As New AAMS.bizTravelAgency.bzAgencyTarget
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            Dim objSecurityXml As New XmlDocument
            Dim objbzbzBIDT As New AAMS.bizProductivity.bzBIDT

            lblError.Text = ""

            'objInputXml.LoadXml("<TA_AGENCYTARGET_INPUT><CityName></CityName><City_Id></City_Id><Year></Year><RESP_1A></RESP_1A><Month></Month><SalesManNameId></SalesManNameId><Increment></Increment><Decrement></Decrement><TargetValuePer></TargetValuePer><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency></TA_AGENCYTARGET_INPUT>")
            objInputXml.LoadXml("<TA_AGENCYTARGET_INPUT><CityName></CityName><City_Id></City_Id><Year></Year><RESP_1A></RESP_1A><Month></Month><SalesManNameId></SalesManNameId><Increment></Increment><Decrement></Decrement><TargetValuePer></TargetValuePer><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><EmployeeID/></TA_AGENCYTARGET_INPUT>")

            If drpCity.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("City_Id").InnerText = drpCity.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("CityName").InnerText = drpCity.SelectedItem.Text
            End If
            objInputXml.DocumentElement.SelectSingleNode("Year").InnerText = drpYear.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("Month").InnerText = drpMonth.SelectedValue
            If Request("drpSalesPerson") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("SalesManNameId").InnerText = Request("drpSalesPerson") '
            Else

                objInputXml.DocumentElement.SelectSingleNode("SalesManNameId").InnerText = drpSalesPerson.SelectedValue
            End If

            objInputXml.DocumentElement.SelectSingleNode("Increment").InnerText = 0
            objInputXml.DocumentElement.SelectSingleNode("Decrement").InnerText = 0
            objInputXml.DocumentElement.SelectSingleNode("TargetValuePer").InnerText = 0
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""

            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))


                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                End If

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                End If
                Dim objEmpXml As New XmlDocument
                Dim UserId As String
                objEmpXml.LoadXml(Session("Security"))
                UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = UserId
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = 0
                End If

            End If
            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If
            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If

            objOutputXml = objTarget.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                hdTotal.Value = objOutputXml.DocumentElement.SelectSingleNode("Total").InnerText
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                Session("AgencyTargetDataSource") = ds.Tables("TARGET")
                Session("AgencySearchTargetXML") = objOutputXml.OuterXml
                Session("AgencySelectTargetXML") = Nothing
                Session("AgencyIncXML") = Nothing

                Session("FinalSesseionXML") = objOutputXml.OuterXml

                grdAgencyTarget.DataSource = ds.Tables("TARGET")
                ddlPageNumber.SelectedValue = 1
                grdAgencyTarget.PageIndex = 0
                txtRecordCount.Text = ds.Tables("TARGET").Rows.Count.ToString()
                txtTotalTarget.Text = hdTotal.Value
                Sorting()
                BindControlsForNavigation(grdAgencyTarget.PageCount)
                pnlPaging.Visible = True
                chbPrevious.Checked = False
            Else
                grdAgencyTarget.DataSource = Nothing
                grdAgencyTarget.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
                chbPrevious.Checked = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub Sorting()
        Dim dv As DataView
        If Session("AgencyTargetDataSource") IsNot Nothing Then
            dv = New DataView(Session("AgencyTargetDataSource"))

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "AgencyName"
                ViewState("Direction") = "asc"
            End If

            Dim dt As DataTable
            dt = CType(Session("AgencyTargetDataSource"), DataTable)

            Dim dt1 As New DataTable

            For i As Integer = 0 To dt.Columns.Count - 1
                Dim current_col, new_col As New DataColumn
                current_col = dt.Columns(i)
                new_col = New DataColumn(current_col.ColumnName, Type.GetType(current_col.DataType.FullName))
                dt1.Columns.Add(new_col)
            Next
            dt1.Columns("TARGET").DataType = Type.GetType("System.Int32")
            Dim i1 As Integer
            For i1 = 0 To dt.Rows.Count - 1
                Dim row As DataRow = dt1.NewRow()
                row.ItemArray = dt.Rows(i1).ItemArray
                dt1.Rows.Add(row)
            Next
            dt1.AcceptChanges()
            dv = New DataView(dt1)


            dv.Sort = ViewState("SortName").ToString() + " " + ViewState("Direction").ToString()
            grdAgencyTarget.DataSource = dv
            grdAgencyTarget.DataBind()
            Dim imgUp As New Image
            imgUp.ImageUrl = "~/Images/Sortup.gif"
            Dim imgDown As New Image
            imgDown.ImageUrl = "~/Images/Sortdown.gif"

            Select Case ViewState("SortName").ToString()
                Case "AgencyName"
                    Select Case ViewState("Direction").ToString()
                        Case "desc"
                            grdAgencyTarget.HeaderRow.Cells(0).Controls.Add(imgDown)
                        Case "asc"
                            grdAgencyTarget.HeaderRow.Cells(0).Controls.Add(imgUp)
                    End Select
                Case "OfficeId"
                    Select Case ViewState("Direction").ToString()
                        Case "desc"
                            grdAgencyTarget.HeaderRow.Cells(1).Controls.Add(imgDown)
                        Case "asc"
                            grdAgencyTarget.HeaderRow.Cells(1).Controls.Add(imgUp)
                    End Select
                Case "Address"
                    Select Case ViewState("Direction").ToString()
                        Case "desc"
                            grdAgencyTarget.HeaderRow.Cells(2).Controls.Add(imgDown)
                        Case "asc"
                            grdAgencyTarget.HeaderRow.Cells(2).Controls.Add(imgUp)
                    End Select
                Case "SalesManName"
                    Select Case ViewState("Direction").ToString()
                        Case "desc"
                            grdAgencyTarget.HeaderRow.Cells(3).Controls.Add(imgDown)
                        Case "asc"
                            grdAgencyTarget.HeaderRow.Cells(3).Controls.Add(imgUp)
                    End Select
                Case "TARGET"
                    Select Case ViewState("Direction").ToString()
                        Case "desc"
                            grdAgencyTarget.HeaderRow.Cells(4).Controls.Add(imgDown)
                        Case "asc"
                            grdAgencyTarget.HeaderRow.Cells(4).Controls.Add(imgUp)
                    End Select
            End Select
            Session("AgencyTargetSorted") = dv
        End If
    End Sub

    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
        '    If eventArgument <> "" Then
        Dim objSales As New AAMS.bizTravelAgency.bzAgencyTarget
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim id As String = eventArgument
        objInputXml.LoadXml("<PR_AGENCYEMPLIST_INPUT><City_Id></City_Id></PR_AGENCYEMPLIST_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("City_Id").InnerText = id
        ' objInputXml.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = "2"
        objOutputXml = objSales.GetAgencyEmpList(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            str = objOutputXml.OuterXml
        Else
            str = ""
        End If
        ' End If
    End Sub


    'Protected Sub btnIncrease_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIncrease.Click
    '    Try
    '        If grdAgencyTarget.Rows.Count = 0 Then
    '            Exit Sub
    '        End If
    '        lblError.Text = ""
    '        Dim objInputXml, objAgencyXml, objOutputXml, objSearchNode As New XmlDocument
    '        Dim objtempXml As New XmlDocument

    '        Dim objXmlNodeClone As XmlNode

    '        Dim objXmlReader As XmlNodeReader
    '        Dim objNodeClone As XmlNode
    '        Dim Rowno As Integer

    '        Dim ds As New DataSet
    '        Dim objTargetIncrease As New AAMS.bizTravelAgency.bzAgencyTarget
    '        If Not Session("AgencySearchTargetXML") Is Nothing Then
    '            objAgencyXml.LoadXml(Session("AgencySearchTargetXML"))
    '            Session("AgencySelectTargetXML") = Nothing
    '            Session("AgencyIncXML") = Nothing
    '        ElseIf Not Session("AgencyIncXML") Is Nothing Then
    '            objAgencyXml.LoadXml(Session("AgencyIncXML"))
    '            Session("AgencySearchTargetXML") = Nothing
    '            Session("AgencySelectTargetXML") = Nothing
    '        ElseIf Not Session("AgencySelectTargetXML") Is Nothing Then
    '            objAgencyXml.LoadXml(Session("AgencySelectTargetXML"))
    '            Session("AgencySearchTargetXML") = Nothing
    '            Session("AgencyIncXML") = Nothing
    '        End If

    '        '##########################################################
    '        ' @ Start of  Code Commented by Abhishek.
    '        '##########################################################

    '        'objtempXml.LoadXml(" <TA_AGENCYTARGET_OUTPUT><TARGET LCode='' Year='' LoginId='' Month='' AgencyName='' OfficeId='' Address='' SalesPersonId='' SalesManName=''  Target='' /><Change Increment='' Decrement='' TargetValuePer='' CityName=''/></TA_AGENCYTARGET_OUTPUT>")
    '        'objNodeClone = objtempXml.DocumentElement.SelectSingleNode("TARGET")
    '        'objXmlNodeClone = objNodeClone.CloneNode(True)
    '        'For Rowno = 0 To grdAgencyTarget.Rows.Count - 1
    '        '    'For Each objNode As XmlNode In objNodeListhdlcode
    '        '    With objXmlNodeClone
    '        '        .Attributes("LCode").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("hdlcode"), HiddenField).Value
    '        '        If chbPrevious.Checked = True Then
    '        '            .Attributes("Year").Value = drpPreviousYear.SelectedValue
    '        '            .Attributes("Month").Value = drpPreviousMonth.SelectedValue
    '        '        Else
    '        '            .Attributes("Year").Value = drpYear.SelectedValue
    '        '            .Attributes("Month").Value = drpMonth.SelectedValue
    '        '        End If
    '        '        If (grdAgencyTarget.Rows(Rowno).Cells(0).Text <> "&nbsp;") Then
    '        '            .Attributes("AgencyName").Value = grdAgencyTarget.Rows(Rowno).Cells(0).Text
    '        '        End If
    '        '        If grdAgencyTarget.Rows(Rowno).Cells(1).Text <> "&nbsp;" Then
    '        '            .Attributes("OfficeId").Value = grdAgencyTarget.Rows(Rowno).Cells(1).Text
    '        '        End If
    '        '        If grdAgencyTarget.Rows(Rowno).Cells(2).Text <> "&nbsp;" Then
    '        '            .Attributes("Address").Value = grdAgencyTarget.Rows(Rowno).Cells(2).Text
    '        '        End If
    '        '        ' .Attributes("LoginId").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("hdResp_1A"), HiddenField).Value
    '        '        .Attributes("SalesPersonId").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("hdSalesId"), HiddenField).Value
    '        '        If grdAgencyTarget.Rows(Rowno).Cells(3).Text <> "&nbsp;" Then
    '        '            .Attributes("SalesManName").Value = grdAgencyTarget.Rows(Rowno).Cells(3).Text
    '        '        End If

    '        '        If Not Session("LoginSession") Is Nothing Then
    '        '            .Attributes("LoginId").Value = Session("LoginSession").ToString().Split("|")(0)
    '        '        End If
    '        '        If rdSummaryOption1.Checked = True Then
    '        '            If CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text = "" Then
    '        '                CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text = 0
    '        '                .Attributes("Target").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text
    '        '            Else
    '        '                .Attributes("Target").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text
    '        '            End If
    '        '        Else
    '        '            If CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text = "" Then
    '        '                'CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text = 0
    '        '                .Attributes("Target").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text
    '        '            Else
    '        '                .Attributes("Target").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text
    '        '            End If
    '        '        End If
    '        '        objtempXml.DocumentElement.AppendChild(objXmlNodeClone)
    '        '        objXmlNodeClone = objNodeClone.CloneNode(True)
    '        '    End With
    '        '    ' Next
    '        'Next Rowno
    '        'objtempXml.DocumentElement.RemoveChild(objtempXml.DocumentElement.SelectSingleNode("TARGET[@LCode='']"))
    '        'Dim xnode1, xnode2, xnode3 As XmlNode
    '        'Dim txml As New XmlDocument
    '        'Dim nl, nl2 As XmlNodeList
    '        'nl2 = objAgencyXml.DocumentElement.SelectNodes("TARGET")
    '        'nl = objtempXml.DocumentElement.SelectNodes("TARGET")

    '        'For Each xnode1 In nl2
    '        '    For Each xnode2 In nl
    '        '        If xnode2.Attributes("LCode").Value = xnode1.Attributes("LCode").Value And xnode2.Attributes("Year").Value = xnode1.Attributes("Year").Value And xnode2.Attributes("Month").Value = xnode1.Attributes("Month").Value And xnode2.Attributes("Target").Value <> xnode1.Attributes("Target").Value Then
    '        '            xnode3 = objAgencyXml.ImportNode(xnode2, True)
    '        '            objAgencyXml.DocumentElement.InsertAfter(xnode3, xnode1)
    '        '            objAgencyXml.DocumentElement.RemoveChild(xnode1)
    '        '            Exit For
    '        '        End If
    '        '    Next
    '        'Next
    '        'Dim elem As XmlElement

    '        'elem = objAgencyXml.CreateElement("CHANGE")

    '        'Dim attr1 As XmlAttribute
    '        'Dim attr2 As XmlAttribute
    '        'attr1 = objAgencyXml.CreateAttribute("Increment")
    '        'attr2 = objAgencyXml.CreateAttribute("Decrement")
    '        ''If rdSummaryOption.Items(0).Selected = True Then
    '        'If rdSummaryOption1.Checked = True Then
    '        '    attr1.Value = 1
    '        '    elem.Attributes.Append(attr1)
    '        '    attr2.Value = 0
    '        '    elem.Attributes.Append(attr2)
    '        'Else
    '        '    attr1.Value = 0
    '        '    elem.Attributes.Append(attr1)
    '        '    attr2.Value = 1
    '        '    elem.Attributes.Append(attr2)
    '        'End If
    '        'Dim attr3 As XmlAttribute
    '        'attr3 = objAgencyXml.CreateAttribute("TargetValuePer")
    '        'attr3.Value = txtTarget.Text
    '        'elem.Attributes.Append(attr3)
    '        'Dim attr4 As XmlAttribute
    '        'attr4 = objAgencyXml.CreateAttribute("CityName")
    '        'attr4.Value = drpCity.SelectedItem.Text
    '        'elem.Attributes.Append(attr4)
    '        'objAgencyXml.DocumentElement.AppendChild(elem)
    '        'Dim errornode As XmlNode
    '        'errornode = objAgencyXml.DocumentElement.SelectSingleNode("Errors")
    '        'objAgencyXml.DocumentElement.RemoveChild(errornode)
    '        '' totalnode = objAgencyXml.DocumentElement.SelectSingleNode("Total")
    '        '' objAgencyXml.DocumentElement.RemoveChild(totalnode)
    '        'objOutputXml = objTargetIncrease.Search_Agency_Target(objAgencyXml)


    '        '##########################################################
    '        ' @ End of  Code Commented by Abhishek.
    '        '##########################################################


    '        '##########################################################
    '        ' @ Start of  Code Added by Abhishek.
    '        '##########################################################


    '        For Rowno = 0 To grdAgencyTarget.Rows.Count - 1
    '            Dim strlcode As String = CType(grdAgencyTarget.Rows(Rowno).FindControl("hdlcode"), HiddenField).Value
    '            Dim strMonth As String = drpMonth.SelectedValue
    '            Dim strYear As String = drpYear.SelectedValue
    '            Dim strTarget As String = CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text
    '            Dim dblTarget As Long = 0
    '            If strTarget.Trim.Length = 0 Then
    '                dblTarget = 0
    '            Else
    '                dblTarget = strTarget
    '            End If
    '            If objAgencyXml.DocumentElement.SelectNodes("TARGET[@LCode='" + strlcode + "' and @Year='" + strYear + "' and @Month='" + strMonth + "'  ]").Count > 0 Then

    '                If rdSummaryOption1.Checked = True Then
    '                    If strTarget.Trim.Length = 0 Then
    '                        objAgencyXml.DocumentElement.SelectSingleNode("TARGET[@LCode='" + strlcode + "' and @Year='" + strYear + "' and @Month='" + strMonth + "' ]").Attributes("Target").Value = 0
    '                    Else
    '                        objAgencyXml.DocumentElement.SelectSingleNode("TARGET[@LCode='" + strlcode + "' and @Year='" + strYear + "' and @Month='" + strMonth + "'  ]").Attributes("Target").Value = (dblTarget + CInt(txtTarget.Text * 0.01 * dblTarget))
    '                    End If
    '                Else
    '                    If strTarget.Trim.Length = 0 Then
    '                        objAgencyXml.DocumentElement.SelectSingleNode("TARGET[@LCode='" + strlcode + "' and @Year='" + strYear + "' and @Month='" + strMonth + "' ]").Attributes("Target").Value = 0
    '                    Else
    '                        objAgencyXml.DocumentElement.SelectSingleNode("TARGET[@LCode='" + strlcode + "' and @Year='" + strYear + "' and @Month='" + strMonth + "'  ]").Attributes("Target").Value = dblTarget - CInt(txtTarget.Text * 0.01 * dblTarget)
    '                    End If
    '                End If
    '            End If
    '            '   objAgencyXml.DocumentElement.SelectNodes("TARGET[@LCode='" + strlcode + " ']")

    '            '  Response.Write(objAgencyXml.DocumentElement.SelectSingleNode("TARGET[@LCode='" + strlcode + "' and @Year='" + strYear + "' and @Month='" + strMonth + "'  ]").Attributes("Target").Value)
    '        Next
    '        objXmlReader = New XmlNodeReader(objAgencyXml)
    '        ds.ReadXml(objXmlReader)
    '        Session("AgencyTargetDataSource") = ds.Tables("TARGET")
    '        Session("AgencyIncXML") = objAgencyXml.OuterXml
    '        Session("AgencySearchTargetXML") = Nothing
    '        Session("AgencySelectTargetXML") = Nothing
    '        txtRecordCount.Text = ds.Tables("TARGET").Rows.Count.ToString()
    '        BindControlsForNavigation(grdAgencyTarget.PageCount)
    '        grdAgencyTarget.DataSource = ds.Tables("TARGET")
    '        grdAgencyTarget.DataBind()
    '        Sorting()
    '        lblError.Text = ""
    '        ' Session("AgencyIncXML") = objOutputXml.OuterXml

    '        '##########################################################
    '        ' @ End of  Code Added by Abhishek.
    '        '##########################################################


    '        '##########################################################
    '        ' @ Start of  Code Commented by Abhishek.
    '        '##########################################################

    '        'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '        '    hdTotal.Value = objOutputXml.DocumentElement.SelectSingleNode("Total").InnerText
    '        '    txtTotalTarget.Text = hdTotal.Value
    '        '    objXmlReader = New XmlNodeReader(objOutputXml)
    '        '    ds.ReadXml(objXmlReader)
    '        '    Session("AgencyTargetDataSource") = ds.Tables("TARGET")
    '        '    Session("AgencyIncXML") = objOutputXml.OuterXml
    '        '    Session("AgencySearchTargetXML") = Nothing
    '        '    Session("AgencySelectTargetXML") = Nothing
    '        '    txtRecordCount.Text = ds.Tables("TARGET").Rows.Count.ToString()
    '        '    BindControlsForNavigation(grdAgencyTarget.PageCount)
    '        '    Sorting()
    '        '    lblError.Text = ""
    '        'Else
    '        '    grdAgencyTarget.DataSource = String.Empty
    '        '    grdAgencyTarget.DataBind()
    '        '    pnlPaging.Visible = False
    '        '    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '        'End If

    '        '##########################################################
    '        ' @ End of  Code Commented by Abhishek.
    '        '##########################################################
    '    Catch ex As Exception
    '        lblError.Text = ex.Message.ToString
    '    End Try
    'End Sub

    Protected Sub btnIncrease_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIncrease.Click
        Try
            If grdAgencyTarget.Rows.Count = 0 Then
                Exit Sub
            End If
            lblError.Text = ""
            Dim objInputXml, objAgencyXml, objOutputXml, objSearchNode As New XmlDocument
            Dim objtempXml As New XmlDocument

            Dim objXmlNodeClone As XmlNode

            Dim objXmlReader As XmlNodeReader
            Dim objNodeClone As XmlNode
            Dim Rowno As Integer

            Dim ds As New DataSet
            Dim objTargetIncrease As New AAMS.bizTravelAgency.bzAgencyTarget
            'If Not Session("AgencySearchTargetXML") Is Nothing Then
            '    objAgencyXml.LoadXml(Session("AgencySearchTargetXML"))
            '    Session("AgencySelectTargetXML") = Nothing
            '    Session("AgencyIncXML") = Nothing
            'ElseIf Not Session("AgencyIncXML") Is Nothing Then
            '    objAgencyXml.LoadXml(Session("AgencyIncXML"))
            '    Session("AgencySearchTargetXML") = Nothing
            '    Session("AgencySelectTargetXML") = Nothing
            'ElseIf Not Session("AgencySelectTargetXML") Is Nothing Then
            '    objAgencyXml.LoadXml(Session("AgencySelectTargetXML"))
            '    Session("AgencySearchTargetXML") = Nothing
            '    Session("AgencyIncXML") = Nothing
            'End If

            If Session("FinalSesseionXML") IsNot Nothing Then
                objAgencyXml.LoadXml(Session("FinalSesseionXML").ToString)
            End If

            objtempXml.LoadXml(" <TA_AGENCYTARGET_OUTPUT><TARGET LCode='' Year='' LoginId='' Month='' AgencyName='' OfficeId='' Address='' SalesPersonId='' SalesManName=''  Target='' /><Change Increment='' Decrement='' TargetValuePer='' CityName=''/></TA_AGENCYTARGET_OUTPUT>")
            objNodeClone = objtempXml.DocumentElement.SelectSingleNode("TARGET")
            objXmlNodeClone = objNodeClone.CloneNode(True)


            '@ New Added Code For Month And Year
            Dim hdMonth, hdYear As HiddenField
            hdMonth = CType(grdAgencyTarget.Rows(0).FindControl("hdMonth"), HiddenField)
            hdYear = CType(grdAgencyTarget.Rows(0).FindControl("hdYear"), HiddenField)

            '@  End of New Added Code For Month And Year

            For Rowno = 0 To grdAgencyTarget.Rows.Count - 1
                'For Each objNode As XmlNode In objNodeListhdlcode
                With objXmlNodeClone
                    .Attributes("LCode").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("hdlcode"), HiddenField).Value




                    'If chbPrevious.Checked = True Then
                    '    .Attributes("Year").Value = drpPreviousYear.SelectedValue
                    '    .Attributes("Month").Value = drpPreviousMonth.SelectedValue
                    'Else
                    '.Attributes("Year").Value = drpYear.SelectedValue
                    '.Attributes("Month").Value = drpMonth.SelectedValue
                    'End If


                    ' .Attributes("Year").Value = drpYear.SelectedValue
                    '.Attributes("Month").Value = drpMonth.SelectedValue

                    .Attributes("Year").Value = hdYear.Value
                    .Attributes("Month").Value = hdMonth.Value


                    If (grdAgencyTarget.Rows(Rowno).Cells(0).Text <> "&nbsp;") Then
                        .Attributes("AgencyName").Value = grdAgencyTarget.Rows(Rowno).Cells(0).Text
                    End If
                    If grdAgencyTarget.Rows(Rowno).Cells(1).Text <> "&nbsp;" Then
                        .Attributes("OfficeId").Value = grdAgencyTarget.Rows(Rowno).Cells(1).Text
                    End If
                    If grdAgencyTarget.Rows(Rowno).Cells(2).Text <> "&nbsp;" Then
                        .Attributes("Address").Value = grdAgencyTarget.Rows(Rowno).Cells(2).Text
                    End If
                    ' .Attributes("LoginId").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("hdResp_1A"), HiddenField).Value
                    .Attributes("SalesPersonId").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("hdSalesId"), HiddenField).Value
                    If grdAgencyTarget.Rows(Rowno).Cells(3).Text <> "&nbsp;" Then
                        .Attributes("SalesManName").Value = grdAgencyTarget.Rows(Rowno).Cells(3).Text
                    End If

                    If Not Session("LoginSession") Is Nothing Then
                        .Attributes("LoginId").Value = Session("LoginSession").ToString().Split("|")(0)
                    End If
                    If rdSummaryOption1.Checked = True Then
                        If CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text = "" Then
                            CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text = 0
                            .Attributes("Target").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text
                        Else
                            .Attributes("Target").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text
                        End If
                    Else
                        If CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text = "" Then
                            'CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text = 0
                            .Attributes("Target").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text
                        Else
                            .Attributes("Target").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text
                        End If
                    End If
                    objtempXml.DocumentElement.AppendChild(objXmlNodeClone)
                    objXmlNodeClone = objNodeClone.CloneNode(True)
                End With
                ' Next
            Next Rowno
            objtempXml.DocumentElement.RemoveChild(objtempXml.DocumentElement.SelectSingleNode("TARGET[@LCode='']"))
            Dim xnode1, xnode2, xnode3 As XmlNode
            Dim txml As New XmlDocument
            Dim nl, nl2 As XmlNodeList
            nl2 = objAgencyXml.DocumentElement.SelectNodes("TARGET")
            nl = objtempXml.DocumentElement.SelectNodes("TARGET")

            For Each xnode1 In nl2
                For Each xnode2 In nl
                    If xnode2.Attributes("LCode").Value = xnode1.Attributes("LCode").Value And xnode2.Attributes("Year").Value = xnode1.Attributes("Year").Value And xnode2.Attributes("Month").Value = xnode1.Attributes("Month").Value And xnode2.Attributes("Target").Value <> xnode1.Attributes("Target").Value Then
                        xnode3 = objAgencyXml.ImportNode(xnode2, True)
                        objAgencyXml.DocumentElement.InsertAfter(xnode3, xnode1)
                        objAgencyXml.DocumentElement.RemoveChild(xnode1)
                        Exit For
                    End If
                Next
            Next
            Dim elem As XmlElement

            elem = objAgencyXml.CreateElement("CHANGE")

            Dim attr1 As XmlAttribute
            Dim attr2 As XmlAttribute
            attr1 = objAgencyXml.CreateAttribute("Increment")
            attr2 = objAgencyXml.CreateAttribute("Decrement")
            'If rdSummaryOption.Items(0).Selected = True Then
            If rdSummaryOption1.Checked = True Then
                attr1.Value = 1
                elem.Attributes.Append(attr1)
                attr2.Value = 0
                elem.Attributes.Append(attr2)
            Else
                attr1.Value = 0
                elem.Attributes.Append(attr1)
                attr2.Value = 1
                elem.Attributes.Append(attr2)
            End If
            Dim attr3 As XmlAttribute
            attr3 = objAgencyXml.CreateAttribute("TargetValuePer")
            attr3.Value = txtTarget.Text
            elem.Attributes.Append(attr3)
            Dim attr4 As XmlAttribute
            attr4 = objAgencyXml.CreateAttribute("CityName")
            attr4.Value = drpCity.SelectedItem.Text
            elem.Attributes.Append(attr4)
            objAgencyXml.DocumentElement.AppendChild(elem)
            Dim errornode As XmlNode
            errornode = objAgencyXml.DocumentElement.SelectSingleNode("Errors")
            objAgencyXml.DocumentElement.RemoveChild(errornode)
            ' totalnode = objAgencyXml.DocumentElement.SelectSingleNode("Total")
            ' objAgencyXml.DocumentElement.RemoveChild(totalnode)
            objOutputXml = objTargetIncrease.Search_Agency_Target(objAgencyXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then


                For Each objnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("TARGET")
                    'objnode.Attributes("Year").Value = drpYear.SelectedValue
                    'objnode.Attributes("Month").Value = drpMonth.SelectedValue
                    objnode.Attributes("Year").Value = hdYear.Value
                    objnode.Attributes("Month").Value = hdMonth.Value
                Next


                hdTotal.Value = objOutputXml.DocumentElement.SelectSingleNode("Total").InnerText
                txtTotalTarget.Text = hdTotal.Value
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                Session("AgencyTargetDataSource") = ds.Tables("TARGET")
                Session("AgencyIncXML") = objOutputXml.OuterXml
                Session("AgencySearchTargetXML") = Nothing
                Session("AgencySelectTargetXML") = Nothing

                Session("FinalSesseionXML") = objOutputXml.OuterXml

                txtRecordCount.Text = ds.Tables("TARGET").Rows.Count.ToString()
                BindControlsForNavigation(grdAgencyTarget.PageCount)
                Sorting()
                lblError.Text = ""
            Else
                grdAgencyTarget.DataSource = String.Empty
                grdAgencyTarget.DataBind()
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If grdAgencyTarget.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim objInputXml, objtempXml, objAgencyXml As New XmlDocument
        Dim objOutputXml As New XmlDocument
        Dim objbzAgencyTargetSave As New AAMS.bizTravelAgency.bzAgencyTarget
        Dim intCnt As Integer = 0
        Dim xnode1, xnode2, xnode3 As XmlNode
        Dim txml As New XmlDocument
        Dim nl, nl2, nl3 As XmlNodeList
        Dim objXmlNodeClone As XmlNode


        Dim objNodeClone As XmlNode
        Dim Rowno As Integer

        ' Dim Rowno As Integer
        Try
            intCnt = grdAgencyTarget.Rows.Count




            '########################################

            'If chbPrevious.Checked = False Then
            If intCnt <> 0 Then
                If Session("FinalSesseionXML") IsNot Nothing Then
                    objAgencyXml.LoadXml(Session("FinalSesseionXML").ToString)
                    objtempXml.LoadXml(" <TA_AGENCYTARGET_OUTPUT><TARGET LCode='' LoginId='' Year='' Month='' AgencyName='' OfficeId='' Address='' SalesPersonId='' SalesManName=''  Target='' /></TA_AGENCYTARGET_OUTPUT>")
                    objNodeClone = objtempXml.DocumentElement.SelectSingleNode("TARGET")
                    ' objAgencyXml.LoadXml(Session("AgencyTargetXML"))


                    '@ New Added Code For Month And Year
                    Dim hdMonth, hdYear As HiddenField
                    hdMonth = CType(grdAgencyTarget.Rows(0).FindControl("hdMonth"), HiddenField)
                    hdYear = CType(grdAgencyTarget.Rows(0).FindControl("hdYear"), HiddenField)

                    '@  End of New Added Code For Month And Year


                    objXmlNodeClone = objNodeClone.CloneNode(True)
                    For Rowno = 0 To grdAgencyTarget.Rows.Count - 1
                        'For Each objNode As XmlNode In objNodeListhdlcode
                        With objXmlNodeClone
                            .Attributes("LCode").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("hdlcode"), HiddenField).Value

                            ' .Attributes("Year").Value = drpYear.SelectedValue
                            ' .Attributes("Month").Value = drpMonth.SelectedValue

                            .Attributes("Year").Value = hdYear.Value 'drpYear.SelectedValue
                            .Attributes("Month").Value = hdMonth.Value '.SelectedValue

                            If Not Session("LoginSession") Is Nothing Then
                                .Attributes("LoginId").Value = Session("LoginSession").ToString().Split("|")(0)
                            End If

                            If (grdAgencyTarget.Rows(Rowno).Cells(0).Text <> "&nbsp;") Then
                                .Attributes("AgencyName").Value = grdAgencyTarget.Rows(Rowno).Cells(0).Text
                            End If
                            If grdAgencyTarget.Rows(Rowno).Cells(1).Text <> "&nbsp;" Then
                                .Attributes("OfficeId").Value = grdAgencyTarget.Rows(Rowno).Cells(1).Text
                            End If
                            If grdAgencyTarget.Rows(Rowno).Cells(2).Text <> "&nbsp;" Then
                                .Attributes("Address").Value = grdAgencyTarget.Rows(Rowno).Cells(2).Text
                            End If
                            'If CType(grdAgencyTarget.Rows(Rowno).FindControl("hdSalesId"), HiddenField).Value = "" Then
                            'Else
                            .Attributes("SalesPersonId").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("hdSalesId"), HiddenField).Value
                            'End If
                            If grdAgencyTarget.Rows(Rowno).Cells(3).Text <> "&nbsp;" Then
                                .Attributes("SalesManName").Value = grdAgencyTarget.Rows(Rowno).Cells(3).Text
                            End If
                            'If rdSummaryOption.Items(0).Selected = True Then
                            If rdSummaryOption1.Checked = True Then
                                If CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text = "" Then
                                    CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text = 0
                                    .Attributes("Target").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text
                                Else
                                    .Attributes("Target").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text
                                End If
                            Else
                                If CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text = "" Then
                                    'CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text = 0
                                    .Attributes("Target").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text
                                Else
                                    .Attributes("Target").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text
                                End If
                            End If
                            objtempXml.DocumentElement.AppendChild(objXmlNodeClone)
                            objXmlNodeClone = objNodeClone.CloneNode(True)
                        End With
                        ' Next
                    Next Rowno
                    objtempXml.DocumentElement.RemoveChild(objtempXml.DocumentElement.SelectSingleNode("TARGET[@LCode='']"))

                    nl2 = objAgencyXml.DocumentElement.SelectNodes("TARGET")
                    nl = objtempXml.DocumentElement.SelectNodes("TARGET")

                    For Each xnode1 In nl2
                        For Each xnode2 In nl
                            If xnode2.Attributes("LCode").Value = xnode1.Attributes("LCode").Value And xnode2.Attributes("Year").Value = xnode1.Attributes("Year").Value And xnode2.Attributes("Month").Value = xnode1.Attributes("Month").Value And xnode2.Attributes("Target").Value <> xnode1.Attributes("Target").Value Then
                                'objAgencyXml.DocumentElement.RemoveChild(xnode1)
                                'xnode3 = objAgencyXml.ImportNode(xnode2, True)
                                'objAgencyXml.DocumentElement.AppendChild(xnode3)
                                xnode3 = objAgencyXml.ImportNode(xnode2, True)
                                objAgencyXml.DocumentElement.InsertAfter(xnode3, xnode1)
                                objAgencyXml.DocumentElement.RemoveChild(xnode1)
                                Exit For
                            End If
                        Next
                    Next

                    Dim errornode, totalnode, Citynode As XmlNode
                    errornode = objAgencyXml.DocumentElement.SelectSingleNode("Errors")
                    If Not errornode Is Nothing Then
                        objAgencyXml.DocumentElement.RemoveChild(errornode)
                    End If
                    totalnode = objAgencyXml.DocumentElement.SelectSingleNode("Total")
                    If Not totalnode Is Nothing Then
                        objAgencyXml.DocumentElement.RemoveChild(totalnode)
                    End If
                    Citynode = objAgencyXml.DocumentElement.SelectSingleNode("CityName")
                    If Not Citynode Is Nothing Then
                        objAgencyXml.DocumentElement.RemoveChild(Citynode)
                    End If

                    nl3 = objAgencyXml.DocumentElement.SelectNodes("TARGET")
                    For Each xnode3 In nl3
                        If xnode3.Attributes("LoginId").Value = "" Then
                            xnode3.Attributes("LoginId").Value = Session("LoginSession").ToString().Split("|")(0)
                            ' objAgencyXml.DocumentElement.AppendChild(xnode3)
                        End If
                    Next
                    objInputXml.LoadXml(objAgencyXml.InnerXml)


                    For Each objnode As XmlNode In objInputXml.DocumentElement.SelectNodes("TARGET")
                        objnode.Attributes("Year").Value = hdYear.Value 'drpYear.SelectedValue
                        objnode.Attributes("Month").Value = hdMonth.Value 'drpMonth.SelectedValue
                    Next


                    objOutputXml = objbzAgencyTargetSave.Update(objInputXml)


                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                        '  If Not Session("AgencySearchTargetXML") Is Nothing Then
                        ' AgencyTargetSearch()
                        UpdateSearchTargetAfterSave(hdYear.Value, hdMonth.Value)
                        'End If
                        lblError.Text = objeAAMSMessage.messUpdate ' "Added Successfully."
                        CheckSecurity()
                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value


                    End If


                End If


            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    'Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
    '    If grdAgencyTarget.Rows.Count = 0 Then
    '        Exit Sub
    '    End If

    '    Dim objInputXml, objtempXml, objAgencyXml As New XmlDocument
    '    Dim objOutputXml As New XmlDocument
    '    Dim objbzAgencyTargetSave As New AAMS.bizTravelAgency.bzAgencyTarget
    '    Dim intCnt As Integer = 0
    '    Dim xnode1, xnode2, xnode3 As XmlNode
    '    Dim txml As New XmlDocument
    '    Dim nl, nl2, nl3 As XmlNodeList

    '    Dim objXmlNodeClone As XmlNode


    '    Dim objNodeClone As XmlNode
    '    Dim Rowno As Integer

    '    ' Dim Rowno As Integer
    '    Try
    '        intCnt = grdAgencyTarget.Rows.Count

    '        If Not Session("AgencySearchTargetXML") Is Nothing Then
    '            objAgencyXml.LoadXml(Session("AgencySearchTargetXML"))
    '            Session("AgencyIncXML") = Nothing
    '            Session("AgencySelectTargetXML") = Nothing
    '        ElseIf Not Session("AgencyIncXML") Is Nothing Then
    '            If chbPrevious.Checked = True Then
    '                Exit Sub
    '            End If
    '            objAgencyXml.LoadXml(Session("AgencyIncXML"))
    '            Session("AgencySearchTargetXML") = Nothing
    '            Session("AgencySelectTargetXML") = Nothing
    '        ElseIf Not Session("AgencySelectTargetXML") Is Nothing Then
    '            PreviousAgencyTargetSearch()
    '            Exit Sub
    '        End If


    '        '##########################################################
    '        ' @ Start of  Code Commented by Abhishek.
    '        '##########################################################

    '        'If intCnt <> 0 Then
    '        '    objtempXml.LoadXml(" <TA_AGENCYTARGET_OUTPUT><TARGET LCode='' LoginId='' Year='' Month='' AgencyName='' OfficeId='' Address='' SalesPersonId='' SalesManName=''  Target='' /></TA_AGENCYTARGET_OUTPUT>")
    '        '    objNodeClone = objtempXml.DocumentElement.SelectSingleNode("TARGET")
    '        '    ' objAgencyXml.LoadXml(Session("AgencyTargetXML"))

    '        '    objXmlNodeClone = objNodeClone.CloneNode(True)
    '        '    For Rowno = 0 To grdAgencyTarget.Rows.Count - 1
    '        '        'For Each objNode As XmlNode In objNodeListhdlcode
    '        '        With objXmlNodeClone
    '        '            .Attributes("LCode").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("hdlcode"), HiddenField).Value
    '        '            'If chbPrevious.Checked = True Then
    '        '            If Not Session("AgencySelectTargetXML") Is Nothing Then
    '        '                .Attributes("Year").Value = drpPreviousYear.SelectedValue
    '        '                .Attributes("Month").Value = drpPreviousMonth.SelectedValue
    '        '            ElseIf Not Session("AgencySearchTargetXML") Is Nothing Then
    '        '                .Attributes("Year").Value = drpYear.SelectedValue
    '        '                .Attributes("Month").Value = drpMonth.SelectedValue
    '        '            ElseIf Not Session("AgencyIncXML") Is Nothing Then

    '        '                .Attributes("Year").Value = drpYear.SelectedValue
    '        '                .Attributes("Month").Value = drpMonth.SelectedValue

    '        '            End If

    '        '            If Not Session("LoginSession") Is Nothing Then
    '        '                .Attributes("LoginId").Value = Session("LoginSession").ToString().Split("|")(0)
    '        '            End If

    '        '            If (grdAgencyTarget.Rows(Rowno).Cells(0).Text <> "&nbsp;") Then
    '        '                .Attributes("AgencyName").Value = grdAgencyTarget.Rows(Rowno).Cells(0).Text
    '        '            End If
    '        '            If grdAgencyTarget.Rows(Rowno).Cells(1).Text <> "&nbsp;" Then
    '        '                .Attributes("OfficeId").Value = grdAgencyTarget.Rows(Rowno).Cells(1).Text
    '        '            End If
    '        '            If grdAgencyTarget.Rows(Rowno).Cells(2).Text <> "&nbsp;" Then
    '        '                .Attributes("Address").Value = grdAgencyTarget.Rows(Rowno).Cells(2).Text
    '        '            End If
    '        '            'If CType(grdAgencyTarget.Rows(Rowno).FindControl("hdSalesId"), HiddenField).Value = "" Then
    '        '            'Else
    '        '            .Attributes("SalesPersonId").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("hdSalesId"), HiddenField).Value
    '        '            'End If
    '        '            If grdAgencyTarget.Rows(Rowno).Cells(3).Text <> "&nbsp;" Then
    '        '                .Attributes("SalesManName").Value = grdAgencyTarget.Rows(Rowno).Cells(3).Text
    '        '            End If
    '        '            'If rdSummaryOption.Items(0).Selected = True Then
    '        '            If rdSummaryOption1.Checked = True Then
    '        '                If CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text = "" Then
    '        '                    CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text = 0
    '        '                    .Attributes("Target").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text
    '        '                Else
    '        '                    .Attributes("Target").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text
    '        '                End If
    '        '            Else
    '        '                If CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text = "" Then
    '        '                    'CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text = 0
    '        '                    .Attributes("Target").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text
    '        '                Else
    '        '                    .Attributes("Target").Value = CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text
    '        '                End If
    '        '            End If
    '        '            objtempXml.DocumentElement.AppendChild(objXmlNodeClone)
    '        '            objXmlNodeClone = objNodeClone.CloneNode(True)
    '        '        End With
    '        '        ' Next
    '        '    Next Rowno
    '        '    objtempXml.DocumentElement.RemoveChild(objtempXml.DocumentElement.SelectSingleNode("TARGET[@LCode='']"))

    '        '    nl2 = objAgencyXml.DocumentElement.SelectNodes("TARGET")
    '        '    nl = objtempXml.DocumentElement.SelectNodes("TARGET")

    '        '    For Each xnode1 In nl2
    '        '        For Each xnode2 In nl
    '        '            If xnode2.Attributes("LCode").Value = xnode1.Attributes("LCode").Value And xnode2.Attributes("Year").Value = xnode1.Attributes("Year").Value And xnode2.Attributes("Month").Value = xnode1.Attributes("Month").Value And xnode2.Attributes("Target").Value <> xnode1.Attributes("Target").Value Then
    '        '                'objAgencyXml.DocumentElement.RemoveChild(xnode1)
    '        '                'xnode3 = objAgencyXml.ImportNode(xnode2, True)
    '        '                'objAgencyXml.DocumentElement.AppendChild(xnode3)
    '        '                xnode3 = objAgencyXml.ImportNode(xnode2, True)
    '        '                objAgencyXml.DocumentElement.InsertAfter(xnode3, xnode1)
    '        '                objAgencyXml.DocumentElement.RemoveChild(xnode1)
    '        '                Exit For
    '        '            End If
    '        '        Next
    '        '    Next

    '        '    Dim errornode, totalnode, Citynode As XmlNode
    '        '    errornode = objAgencyXml.DocumentElement.SelectSingleNode("Errors")
    '        '    If Not errornode Is Nothing Then
    '        '        objAgencyXml.DocumentElement.RemoveChild(errornode)
    '        '    End If
    '        '    totalnode = objAgencyXml.DocumentElement.SelectSingleNode("Total")
    '        '    If Not totalnode Is Nothing Then
    '        '        objAgencyXml.DocumentElement.RemoveChild(totalnode)
    '        '    End If
    '        '    Citynode = objAgencyXml.DocumentElement.SelectSingleNode("CityName")
    '        '    If Not Citynode Is Nothing Then
    '        '        objAgencyXml.DocumentElement.RemoveChild(Citynode)
    '        '    End If

    '        '    nl3 = objAgencyXml.DocumentElement.SelectNodes("TARGET")
    '        '    For Each xnode3 In nl3
    '        '        If xnode3.Attributes("LoginId").Value = "" Then
    '        '            xnode3.Attributes("LoginId").Value = Session("LoginSession").ToString().Split("|")(0)
    '        '            ' objAgencyXml.DocumentElement.AppendChild(xnode3)
    '        '        End If
    '        '    Next
    '        '    objInputXml.LoadXml(objAgencyXml.InnerXml)
    '        'End If
    '        '' End If

    '        '##########################################################
    '        ' @ End  of  Code Commented by Abhishek.
    '        '##########################################################

    '        '##########################################################
    '        ' @ Start  of  Code Added by Abhishek.
    '        '##########################################################
    '        objInputXml.LoadXml(objAgencyXml.InnerXml)

    '        '##########################################################
    '        ' @ End  of  Code Added by Abhishek.
    '        '##########################################################


    '        objOutputXml = objbzAgencyTargetSave.Update(objInputXml)
    '        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

    '            If Not Session("AgencySearchTargetXML") Is Nothing Then
    '                AgencyTargetSearch()
    '            End If
    '            lblError.Text = objeAAMSMessage.messUpdate ' "Added Successfully."
    '            CheckSecurity()
    '        Else
    '            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

    '        End If
    '    Catch ex As Exception
    '        lblError.Text = ex.Message.ToString
    '    End Try
    'End Sub


    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("TASR_AgencyTarget.aspx")
    End Sub

    'Protected Sub grdAgencyTarget_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdAgencyTarget.PageIndexChanging
    '    Try
    '        lblError.Text = ""
    '        grdAgencyTarget.PageIndex = e.NewPageIndex
    '        If Session("AgencyTargetDataSource") IsNot Nothing Then
    '            If Not Session("AgencyTargetSorted") Is Nothing Then
    '                grdAgencyTarget.DataSource = CType(Session("AgencyTargetSorted"), DataView)
    '                grdAgencyTarget.DataBind()
    '            Else
    '                grdAgencyTarget.DataSource = CType(Session("AgencyTargetDataSource"), DataView)
    '                Session("AgencyTargetPaged") = grdAgencyTarget.DataSource
    '                grdAgencyTarget.DataBind()

    '            End If
    '            ''@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

    '            Dim imgUp As New Image
    '            imgUp.ImageUrl = "~/Images/Sortup.gif"
    '            Dim imgDown As New Image
    '            imgDown.ImageUrl = "~/Images/Sortdown.gif"

    '            Select Case ViewState("SortName")
    '                Case "AgencyName"
    '                    Select Case ViewState("Direction")
    '                        Case "desc"
    '                            grdAgencyTarget.HeaderRow.Cells(0).Controls.Add(imgDown)
    '                        Case "asc"
    '                            grdAgencyTarget.HeaderRow.Cells(0).Controls.Add(imgUp)
    '                    End Select
    '                Case "OfficeId"
    '                    Select Case ViewState("Direction")
    '                        Case "desc"
    '                            grdAgencyTarget.HeaderRow.Cells(1).Controls.Add(imgDown)
    '                        Case "asc"
    '                            grdAgencyTarget.HeaderRow.Cells(1).Controls.Add(imgUp)
    '                    End Select
    '                Case "Address"
    '                    Select Case ViewState("Direction")
    '                        Case "desc"
    '                            grdAgencyTarget.HeaderRow.Cells(2).Controls.Add(imgDown)
    '                        Case "asc"
    '                            grdAgencyTarget.HeaderRow.Cells(2).Controls.Add(imgUp)

    '                    End Select
    '                Case "SalesManName"
    '                    Select Case ViewState("Direction")
    '                        Case "desc"
    '                            grdAgencyTarget.HeaderRow.Cells(3).Controls.Add(imgDown)
    '                        Case "asc"
    '                            grdAgencyTarget.HeaderRow.Cells(3).Controls.Add(imgUp)
    '                    End Select
    '                Case "TARGET"
    '                    Select Case ViewState("Direction")
    '                        Case "desc"
    '                            grdAgencyTarget.HeaderRow.Cells(4).Controls.Add(imgDown)
    '                        Case "asc"
    '                            grdAgencyTarget.HeaderRow.Cells(4).Controls.Add(imgUp)

    '                    End Select
    '            End Select





    '            '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

    '        End If

    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '    End Try
    'End Sub


    Protected Sub grdAgencyTarget_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdAgencyTarget.Sorting
        Try
            lblError.Text = ""
            Dim dv As DataView
            Dim SortName As String = e.SortExpression

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ViewState("Direction") = "asc"
            Else
                If ViewState("SortName") = SortName Then
                    If ViewState("Direction") = "asc" Then
                        ViewState("Direction") = "desc"
                    Else
                        ViewState("Direction") = "asc"
                    End If
                Else
                    ViewState("SortName") = SortName
                    ViewState("Direction") = "asc"
                End If
            End If

            UpdatesessionWhenMovethepage()

            If Session("AgencyTargetDataSource") IsNot Nothing Then
                If Not Session("AgencyTargetPaged") Is Nothing Then
                    'grdAgencyTarget.DataSource = CType(Session("AgencyTargetPaged"), DataTable)
                    ' grdAgencyTarget.DataBind()
                    dv = New DataView(Session("AgencyTargetPaged"))

                    'ndt = Session("AgencyTargetPaged")
                    'ndt.Columns.Add(New DataColumn("TARGET1", GetType(Integer)))
                    'dv = New DataView(ndt)
                Else
                    ' grdAgencyTarget.DataSource = CType(Session("AgencyTargetDataSource"), DataTable)
                    'Session("AgencyTargetPaged") = grdAgencyTarget.DataSource

                    'grdAgencyTarget.DataBind()
                    dv = New DataView(Session("AgencyTargetDataSource"))

                    'ndt = Session("AgencyTargetDataSource")
                    'ndt.Columns.Add(New DataColumn("TARGET1", GetType(Integer)))
                    'dv = New DataView(ndt)
                End If
                'dv = New DataView(Session("AgencyTargetDataSource"))
                Dim dt As DataTable
                dt = CType(Session("AgencyTargetDataSource"), DataTable)

                Dim dt1 As New DataTable

                For i As Integer = 0 To dt.Columns.Count - 1
                    Dim current_col, new_col As New DataColumn
                    current_col = dt.Columns(i)
                    new_col = New DataColumn(current_col.ColumnName, Type.GetType(current_col.DataType.FullName))
                    dt1.Columns.Add(new_col)
                Next

                dt1.Columns("TARGET").DataType = Type.GetType("System.Int32")
                Dim i1 As Integer
                For i1 = 0 To dt.Rows.Count - 1
                    Dim row As DataRow = dt1.NewRow()
                    row.ItemArray = dt.Rows(i1).ItemArray
                    dt1.Rows.Add(row)
                Next
                dt1.AcceptChanges()
                dv = New DataView(dt1)
                dv.Sort = ViewState("SortName").ToString + " " + ViewState("Direction").ToString()

                grdAgencyTarget.DataSource = dv
                grdAgencyTarget.DataBind()
                Session("AgencyTargetSorted") = grdAgencyTarget.DataSource

                Dim imgUp As New Image
                imgUp.ImageUrl = "~/Images/Sortup.gif"
                Dim imgDown As New Image
                imgDown.ImageUrl = "~/Images/Sortdown.gif"

                Select Case ViewState("SortName").ToString()
                    Case "AgencyName"
                        Select Case ViewState("Direction").ToString()
                            Case "desc"
                                grdAgencyTarget.HeaderRow.Cells(0).Controls.Add(imgDown)
                            Case "asc"
                                grdAgencyTarget.HeaderRow.Cells(0).Controls.Add(imgUp)
                        End Select
                    Case "OfficeId"
                        Select Case ViewState("Direction").ToString()
                            Case "desc"
                                grdAgencyTarget.HeaderRow.Cells(1).Controls.Add(imgDown)
                            Case "asc"
                                grdAgencyTarget.HeaderRow.Cells(1).Controls.Add(imgUp)
                        End Select
                    Case "Address"
                        Select Case ViewState("Direction").ToString()
                            Case "desc"
                                grdAgencyTarget.HeaderRow.Cells(2).Controls.Add(imgDown)
                            Case "asc"
                                grdAgencyTarget.HeaderRow.Cells(2).Controls.Add(imgUp)

                        End Select
                    Case "SalesManName"
                        Select Case ViewState("Direction").ToString()
                            Case "desc"
                                grdAgencyTarget.HeaderRow.Cells(3).Controls.Add(imgDown)
                            Case "asc"
                                grdAgencyTarget.HeaderRow.Cells(3).Controls.Add(imgUp)
                        End Select
                    Case "TARGET"
                        Select Case ViewState("Direction").ToString()
                            Case "desc"
                                grdAgencyTarget.HeaderRow.Cells(4).Controls.Add(imgDown)
                            Case "asc"
                                grdAgencyTarget.HeaderRow.Cells(4).Controls.Add(imgUp)

                        End Select
                End Select
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdAgencyTarget_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdAgencyTarget.RowDataBound
        Dim objSecurityXml As New XmlDocument
        Dim lnkHistory As New LinkButton

        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If


            ''@ Added Code For Month And Year
            'Dim hdMonth, hdYear As HiddenField
            'hdMonth = CType(e.Row.FindControl("hdMonth"), HiddenField)
            'hdYear = CType(e.Row.FindControl("hdYear"), HiddenField)

            'hdMonth.Value = drpMonth.SelectedValue
            'hdYear.Value = drpYear.SelectedValue

            'If ViewState("hdMonth") IsNot Nothing Then
            '    hdMonth.Value = ViewState("hdMonth").ToString
            'End If
            'If ViewState("hdYear") IsNot Nothing Then
            '    hdYear.Value = ViewState("hdYear").ToString 'drpYear.SelectedValue
            'End If
            ''@ Added Code

            Dim hdLcode, hdSalesId As HiddenField
            hdLcode = CType(e.Row.FindControl("hdlcode"), HiddenField)
            hdSalesId = CType(e.Row.FindControl("hdSalesId"), HiddenField)
            hdLcode.Value = hdLcode.Value.Replace(vbCrLf, "\n")
            hdLcode.Value = Server.UrlEncode(hdLcode.Value)
            hdLcode.Value = hdLcode.Value.Replace("'", "")
            hdSalesId.Value = hdSalesId.Value.Replace(vbCrLf, "\n")
            hdSalesId.Value = Server.UrlEncode(hdSalesId.Value)
            hdSalesId.Value = hdSalesId.Value.Replace("'", "")
            lnkHistory = e.Row.FindControl("lnkHistory")
            lnkHistory.Attributes.Add("onclick", "javascript:return HistoryFunction('" + hdLcode.Value + "','" + hdSalesId.Value + "')")
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Public Sub fillSalesName()
        '  If drpCity.SelectedValue <> "" Then
        drpSalesPerson.Items.Clear()
        Dim objSales As New AAMS.bizTravelAgency.bzAgencyTarget
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objInputXml, objOutputXml As New XmlDocument
        ' objInputXml.LoadXml("<MS_SEARCH_CALLCATEGORY_INPUT>	<CALL_CATEGORY_NAME/>	<CALL_SUB_GROUP_ID/><HD_QUERY_GROUP_ID/></MS_SEARCH_CALLCATEGORY_INPUT>")
        objInputXml.LoadXml("<PR_AGENCYEMPLIST_INPUT><City_Id></City_Id></PR_AGENCYEMPLIST_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("City_Id").InnerText = drpCity.SelectedValue
        objOutputXml = objSales.GetAgencyEmpList(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            drpSalesPerson.DataSource = ds.Tables("TARGET")
            drpSalesPerson.DataTextField = "SalesManName"
            drpSalesPerson.DataValueField = "SalesManId"
            drpSalesPerson.DataBind()
        End If
        drpSalesPerson.Items.Insert(0, New ListItem("--All--", ""))
        'drpSalesPerson.Items.Insert(0, New ListItem("--Select One--", ""))
        'Else
        ' drpSalesPerson.Items.Clear()
        ' drpSalesPerson.Items.Insert(0, New ListItem("--All--", ""))
        ' End If


    End Sub


    Protected Sub btn_Select_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Select.Click
        Try
            lblError.Text = ""
            If grdAgencyTarget.Rows.Count = 0 Then
                fillSalesName()
                If drpSalesPerson.Items.FindByValue(hdSales.Value) IsNot Nothing Then
                    drpSalesPerson.SelectedValue = hdSales.Value
                End If
                ' drpSalesPerson.SelectedValue = hdSales.Value
                lblError.Text = "First click on search."
                Exit Sub
            End If
            PreviousAgencyTargetSearch()
            fillSalesName()
            If drpSalesPerson.Items.FindByValue(hdSales.Value) IsNot Nothing Then
                drpSalesPerson.SelectedValue = hdSales.Value
            End If
            '  drpSalesPerson.SelectedValue = hdSales.Value
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub PreviousAgencyTargetSearch()

        '######################################################################################
        'Start New Code
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objTargetPrevious As New AAMS.bizTravelAgency.bzAgencyTarget
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim dv As New DataView
        Try
            If Session("FinalSesseionXML") IsNot Nothing Then
                objInputXml.LoadXml(Session("FinalSesseionXML").ToString)


                For Each objnode As XmlNode In objInputXml.DocumentElement.SelectNodes("TARGET")
                    objnode.Attributes("Year").Value = drpPreviousYear.SelectedValue
                    objnode.Attributes("Month").Value = drpPreviousMonth.SelectedValue
                Next



                objOutputXml = objTargetPrevious.Search_Agency_target1(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then


                    '@ New Added Code For Month And Year
                    Dim hdMonth, hdYear As HiddenField
                    hdMonth = CType(grdAgencyTarget.Rows(0).FindControl("hdMonth"), HiddenField)
                    hdYear = CType(grdAgencyTarget.Rows(0).FindControl("hdYear"), HiddenField)

                    ' ViewState("hdMonth") = hdMonth.Value
                    '  ViewState("hdYear") = hdYear.Value

                    '@  End of New Added Code For Month And Year

                    For Each objnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("TARGET")
                        objnode.Attributes("Year").Value = hdYear.Value ' drpYear.SelectedValue
                        objnode.Attributes("Month").Value = hdMonth.Value ' drpMonth.SelectedValue
                    Next

                    hdTotal.Value = objOutputXml.DocumentElement.SelectSingleNode("Total").InnerText
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    Session("AgencySelectTargetXML") = objOutputXml.OuterXml
                    'Session("AgencySelectTargetXML2") = objOutputXml.OuterXml
                    Session("AgencySearchTargetXML") = Nothing
                    Session("AgencyIncXML") = Nothing
                    Session("AgencyTargetDataSource") = ds.Tables("TARGET")

                    Session("FinalSesseionXML") = objOutputXml.OuterXml
                    ddlPageNumber.SelectedValue = 1
                    grdAgencyTarget.PageIndex = 0
                    Sorting()

                    BindControlsForNavigation(grdAgencyTarget.PageCount)
                    txtTotalTarget.Text = hdTotal.Value
                    hdTargetList.Value = objOutputXml.OuterXml
                    pnlPaging.Visible = True
                    lblError.Text = ""
                    txtRecordCount.Text = ds.Tables("TARGET").Rows.Count.ToString()
                Else
                    grdAgencyTarget.DataSource = Nothing
                    grdAgencyTarget.DataBind()
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    pnlPaging.Visible = False
                End If
            End If

            '######################################################################################

            '######################################################################################
            'End New Code
            '######################################################################################

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnDisplay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisplay.Click
        Try
            lblError.Text = ""
            'If grdAgencyTarget.Rows.Count = 0 Then
            '    Exit Sub
            'End If
            AgencyTargetReport()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub AgencyTargetReport()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objTarget As New AAMS.bizTravelAgency.bzAgencyTarget
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzbzBIDT As New AAMS.bizProductivity.bzBIDT
        Dim objTargetPrevious As New AAMS.bizTravelAgency.bzAgencyTarget
        lblError.Text = ""
        Try

            lblError.Text = ""

            'objInputXml.LoadXml("<TA_AGENCYTARGET_INPUT><CityName></CityName><City_Id></City_Id><Year></Year><RESP_1A></RESP_1A><Month></Month><SalesManNameId></SalesManNameId><Increment></Increment><Decrement></Decrement><TargetValuePer></TargetValuePer><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency></TA_AGENCYTARGET_INPUT>")
            objInputXml.LoadXml("<TA_AGENCYTARGET_INPUT><CityName></CityName><City_Id></City_Id><Year></Year><RESP_1A></RESP_1A><Month></Month><SalesManNameId></SalesManNameId><Increment></Increment><Decrement></Decrement><TargetValuePer></TargetValuePer><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><EmployeeID/></TA_AGENCYTARGET_INPUT>")

            If drpCity.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("City_Id").InnerText = drpCity.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("CityName").InnerText = drpCity.SelectedItem.Text
            End If
            objInputXml.DocumentElement.SelectSingleNode("Year").InnerText = drpYear.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("Month").InnerText = drpMonth.SelectedValue
            '  objInputXml.DocumentElement.SelectSingleNode("SalesManNameId").InnerText = drpSalesPerson.SelectedValue

            If Request("drpSalesPerson") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("SalesManNameId").InnerText = Request("drpSalesPerson") '
            Else

                objInputXml.DocumentElement.SelectSingleNode("SalesManNameId").InnerText = drpSalesPerson.SelectedValue
            End If
            objInputXml.DocumentElement.SelectSingleNode("Increment").InnerText = 0
            objInputXml.DocumentElement.SelectSingleNode("Decrement").InnerText = 0
            objInputXml.DocumentElement.SelectSingleNode("TargetValuePer").InnerText = 0
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""

            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))


                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                End If

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                End If
                Dim objEmpXml As New XmlDocument
                Dim UserId As String
                objEmpXml.LoadXml(Session("Security"))
                UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = UserId
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = 0
                End If

            End If
            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If
            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If

            objOutputXml = objTarget.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                Session("IP_ATarget") = objInputXml.OuterXml
                Session("eInventory_Pc") = objInputXml.OuterXml
                Session("ATarget") = objOutputXml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=AgencyTarget", False)
            Else
                'grdAgencyTarget.DataSource = Nothing
                'grdAgencyTarget.DataBind()
                'pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If



        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub


    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        lblError.Text = ""
        UpdatesessionWhenMovethepage()
        grdAgencyTarget.PageIndex = ddlPageNumber.SelectedValue - 1
        Sorting()
        BindControlsForNavigation(grdAgencyTarget.PageCount)
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        UpdatesessionWhenMovethepage()

        If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
            ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
        End If
        lblError.Text = ""
        grdAgencyTarget.PageIndex = ddlPageNumber.SelectedValue - 1
        Sorting()
        BindControlsForNavigation(grdAgencyTarget.PageCount)
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
        UpdatesessionWhenMovethepage()
        If ddlPageNumber.SelectedValue <> "1" Then
            ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
        End If
        lblError.Text = ""
        grdAgencyTarget.PageIndex = ddlPageNumber.SelectedValue - 1
        Sorting()
        BindControlsForNavigation(grdAgencyTarget.PageCount)
    End Sub

    Private Sub UpdatesessionWhenMovethepage()
        If grdAgencyTarget.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim objInputXml, objtempXml, objAgencyXml As New XmlDocument
        Dim objOutputXml As New XmlDocument
        Dim objbzAgencyTargetSave As New AAMS.bizTravelAgency.bzAgencyTarget

        Dim Rowno As Integer
        Dim TargetValue As System.Int32
        TargetValue = 0
        ' Dim Rowno As Integer
        Try

            If Session("FinalSesseionXML") IsNot Nothing Then
                objAgencyXml.LoadXml(Session("FinalSesseionXML").ToString)



                Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet


                '@ New Added Code For Month And Year
                Dim hdMonth, hdYear As HiddenField
                hdMonth = CType(grdAgencyTarget.Rows(0).FindControl("hdMonth"), HiddenField)
                hdYear = CType(grdAgencyTarget.Rows(0).FindControl("hdYear"), HiddenField)

                '@  End of New Added Code For Month And Year


                For Rowno = 0 To grdAgencyTarget.Rows.Count - 1
                    Dim strlcode As String = CType(grdAgencyTarget.Rows(Rowno).FindControl("hdlcode"), HiddenField).Value
                    Dim strMonth As String = hdMonth.Value ' drpMonth.SelectedValue
                    Dim strYear As String = hdYear.Value '.SelectedValue
                    Dim strTarget As String = CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text
                    Dim dblTarget As Long = 0
                    If strTarget.Trim.Length = 0 Then
                        dblTarget = 0
                    Else
                        dblTarget = strTarget
                    End If

                    If objAgencyXml.DocumentElement.SelectNodes("TARGET[@LCode='" + strlcode + "' and @Year='" + strYear + "' and @Month='" + strMonth + "'  ]").Count > 0 Then

                        If rdSummaryOption1.Checked = True Then
                            If strTarget.Trim.Length = 0 Then
                                objAgencyXml.DocumentElement.SelectSingleNode("TARGET[@LCode='" + strlcode + "' and @Year='" + strYear + "' and @Month='" + strMonth + "' ]").Attributes("Target").Value = 0
                            Else
                                objAgencyXml.DocumentElement.SelectSingleNode("TARGET[@LCode='" + strlcode + "' and @Year='" + strYear + "' and @Month='" + strMonth + "'  ]").Attributes("Target").Value = (dblTarget)
                            End If
                        Else
                            If strTarget.Trim.Length = 0 Then
                                objAgencyXml.DocumentElement.SelectSingleNode("TARGET[@LCode='" + strlcode + "' and @Year='" + strYear + "' and @Month='" + strMonth + "' ]").Attributes("Target").Value = 0
                            Else
                                objAgencyXml.DocumentElement.SelectSingleNode("TARGET[@LCode='" + strlcode + "' and @Year='" + strYear + "' and @Month='" + strMonth + "'  ]").Attributes("Target").Value = dblTarget
                            End If
                        End If
                    End If
                    '   objAgencyXml.DocumentElement.SelectNodes("TARGET[@LCode='" + strlcode + " ']")

                    '  Response.Write(objAgencyXml.DocumentElement.SelectSingleNode("TARGET[@LCode='" + strlcode + "' and @Year='" + strYear + "' and @Month='" + strMonth + "'  ]").Attributes("Target").Value)
                Next

                For Each objnode As XmlNode In objAgencyXml.DocumentElement.SelectNodes("TARGET")
                    objnode.Attributes("Year").Value = hdYear.Value 'drpYear.SelectedValue
                    objnode.Attributes("Month").Value = hdMonth.Value ' drpMonth.SelectedValue
                    If objnode.Attributes("Target").Value.Trim.Length > 0 Then
                        TargetValue = TargetValue + objnode.Attributes("Target").Value
                    End If
                Next

                objAgencyXml.DocumentElement.SelectSingleNode("Total").InnerText = TargetValue


                objXmlReader = New XmlNodeReader(objAgencyXml)
                ds.ReadXml(objXmlReader)
                Session("AgencyTargetDataSource") = ds.Tables("TARGET")
                If Not Session("AgencySearchTargetXML") Is Nothing Then
                    Session("AgencySearchTargetXML") = objAgencyXml.OuterXml
                ElseIf Not Session("AgencyIncXML") Is Nothing Then
                    Session("AgencyIncXML") = objAgencyXml.OuterXml
                ElseIf Not Session("AgencySelectTargetXML") Is Nothing Then
                    Session("AgencySelectTargetXML") = objAgencyXml.OuterXml
                End If

                Session("FinalSesseionXML") = objAgencyXml.OuterXml
                hdTotal.Value = objAgencyXml.DocumentElement.SelectSingleNode("Total").InnerText
                txtTotalTarget.Text = hdTotal.Value

            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub UpdateSearchTargetAfterSave(ByVal Year As String, ByVal Month As String)
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objTarget As New AAMS.bizTravelAgency.bzAgencyTarget
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            Dim objSecurityXml As New XmlDocument
            Dim objbzbzBIDT As New AAMS.bizProductivity.bzBIDT

            lblError.Text = ""

            'objInputXml.LoadXml("<TA_AGENCYTARGET_INPUT><CityName></CityName><City_Id></City_Id><Year></Year><RESP_1A></RESP_1A><Month></Month><SalesManNameId></SalesManNameId><Increment></Increment><Decrement></Decrement><TargetValuePer></TargetValuePer><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency></TA_AGENCYTARGET_INPUT>")
            objInputXml.LoadXml("<TA_AGENCYTARGET_INPUT><CityName></CityName><City_Id></City_Id><Year></Year><RESP_1A></RESP_1A><Month></Month><SalesManNameId></SalesManNameId><Increment></Increment><Decrement></Decrement><TargetValuePer></TargetValuePer><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><EmployeeID/></TA_AGENCYTARGET_INPUT>")

            If drpCity.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("City_Id").InnerText = drpCity.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("CityName").InnerText = drpCity.SelectedItem.Text
            End If
            objInputXml.DocumentElement.SelectSingleNode("Year").InnerText = Year ' drpYear.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("Month").InnerText = Month 'drpMonth.SelectedValue
            If Request("drpSalesPerson") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("SalesManNameId").InnerText = Request("drpSalesPerson") '
            Else

                objInputXml.DocumentElement.SelectSingleNode("SalesManNameId").InnerText = drpSalesPerson.SelectedValue
            End If

            objInputXml.DocumentElement.SelectSingleNode("Increment").InnerText = 0
            objInputXml.DocumentElement.SelectSingleNode("Decrement").InnerText = 0
            objInputXml.DocumentElement.SelectSingleNode("TargetValuePer").InnerText = 0
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""

            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))


                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                End If

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                End If
                Dim objEmpXml As New XmlDocument
                Dim UserId As String
                objEmpXml.LoadXml(Session("Security"))
                UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = UserId
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = 0
                End If

            End If
            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If
            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If

            objOutputXml = objTarget.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                hdTotal.Value = objOutputXml.DocumentElement.SelectSingleNode("Total").InnerText
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                Session("AgencyTargetDataSource") = ds.Tables("TARGET")
                Session("AgencySearchTargetXML") = objOutputXml.OuterXml
                Session("AgencySelectTargetXML") = Nothing
                Session("AgencyIncXML") = Nothing

                Session("FinalSesseionXML") = objOutputXml.OuterXml

                grdAgencyTarget.DataSource = ds.Tables("TARGET")
                ddlPageNumber.SelectedValue = 1
                grdAgencyTarget.PageIndex = 0
                txtRecordCount.Text = ds.Tables("TARGET").Rows.Count.ToString()
                txtTotalTarget.Text = hdTotal.Value
                Sorting()
                BindControlsForNavigation(grdAgencyTarget.PageCount)
                pnlPaging.Visible = True
                chbPrevious.Checked = False
            Else
                grdAgencyTarget.DataSource = Nothing
                grdAgencyTarget.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
                chbPrevious.Checked = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

End Class

