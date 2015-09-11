
Partial Class Sales_SASR_AllowBackDatedDSR
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
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
        
        Try
            gvDSRBackDated.PageSize = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
            imgDSRDate.Attributes.Add("onmousedown", "SelectDate('" & txtDSRDate.ClientID.ToString() & "','" & imgDSRDate.ClientID.ToString() & "');")
            BtnAdd.Attributes.Add("onclick", "return ValidateData();")
            BtnHistory.Attributes.Add("onclick", "return LoadHistory();")

            If Not Page.IsPostBack Then
                fillSalesName()
                Session("DSRBackDatedDataSource") = Nothing
                Session("FinalDSRBackDated") = Nothing
                GetDatasource()
                BtnCancel.Enabled = False
            End If
            CheckSecurity()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub GetDatasource()
        Session("DSRBackDatedDataSource") = Nothing
        Session("FinalDSRBackDated") = Nothing
        Try
            Try
                Dim objInputXml, objOutputXml As New XmlDocument
                Dim objbzDSR As New AAMS.bizSales.bzDSR
                Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                Dim objSecurityXml As New XmlDocument
                lblError.Text = ""
                objInputXml.LoadXml("<SL_SEARCH_DSR_BACKDATE_INPUT><EMPLOYEEID/><RESP_1A/><DSRDATE/><PAGE_NO></PAGE_NO><PAGE_SIZE></PAGE_SIZE><SORT_BY></SORT_BY><DESC></DESC></SL_SEARCH_DSR_BACKDATE_INPUT>")

                If drpSalesPerson.SelectedValue <> "" Then
                    objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = drpSalesPerson.SelectedValue
                End If
         
                If Not Session("LoginSession") Is Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = Session("LoginSession").ToString().Split("|")(0)
                End If
                objInputXml.DocumentElement.SelectSingleNode("DSRDATE").InnerText = objeAAMS.ConvertTextDate(txtDSRDate.Text)

                objOutputXml = objbzDSR.Search_BackDate_DSR(objInputXml)
                '   objOutputXml.Load("C:\SL_SEARCH_DSR_BACKDATE_OUTPUT.xml")
                Try
                    objOutputXml.Save("c:\DSRBackDatedtOutXml.xml")
                    objInputXml.Save("c:\DSRBackDatedtInXml.xml")
                Catch ex As Exception

                End Try
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    ViewState("PrevSearching") = objInputXml.OuterXml
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    Session("DSRBackDatedDataSource") = ds.Tables("DSR_BACKDATE")
                    Session("FinalDSRBackDated") = objOutputXml.OuterXml
                    gvDSRBackDated.DataSource = ds.Tables("DSR_BACKDATE")
                    ddlPageNumber.SelectedValue = 1
                    gvDSRBackDated.PageIndex = 0
                    Sorting()
                    BindControlsForNavigation(gvDSRBackDated.PageCount)
                    pnlPaging.Visible = True
                Else
                    gvDSRBackDated.DataSource = Nothing
                    gvDSRBackDated.DataBind()
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value <> objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value Then
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If

                    pnlPaging.Visible = False
                End If
            Catch ex As Exception
                lblError.Text = ex.Message.ToString
            End Try
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub BtnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.ToString, False)
        Catch ex As Exception
        End Try
    End Sub


    Public Sub fillSalesName()
        drpSalesPerson.Items.Clear()
        Dim objbzDSR As New AAMS.bizSales.bzDSR
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objEmpXml As New XmlDocument
        Dim objInputXml, objOutputXml As New XmlDocument
        Try

            Dim UserId As String
            objEmpXml.LoadXml(Session("Security"))
            UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
            objInputXml.LoadXml("<SL_LST_POPULATE_MANANGER_EMPLOYEE_INPUT><EMPLOYEEID/></SL_LST_POPULATE_MANANGER_EMPLOYEE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = UserId
            objOutputXml = objbzDSR.List_PopulateManagerEmployee(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                drpSalesPerson.DataSource = ds.Tables("EMP")
                drpSalesPerson.DataTextField = "EMPLOYEE_NAME"
                drpSalesPerson.DataValueField = "EMPLOYEEID"
                drpSalesPerson.DataBind()
            End If
            drpSalesPerson.Items.Insert(0, New ListItem("--Selct One--", ""))
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Private Sub Sorting()
        Dim dv As DataView
        Try
            If Session("DSRBackDatedDataSource") IsNot Nothing Then
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "DSRDATE"
                    ViewState("Direction") = "DESC"
                End If
                '<DSR_BACKDATE EMPLOYEEID='24' EMPLOYEENAME ='admin'  RESP_1A='2' RESP_NAME='Jitender' DSRDATE='20110702' DSRCODE='' ROWID='1' LOGDATE='' />
                Dim dt As DataTable
                dt = CType(Session("DSRBackDatedDataSource"), DataTable)

                Dim dt1 As New DataTable

                For i As Integer = 0 To dt.Columns.Count - 1
                    Dim current_col, new_col As New DataColumn
                    current_col = dt.Columns(i)
                    new_col = New DataColumn(current_col.ColumnName, Type.GetType(current_col.DataType.FullName))
                    new_col.AllowDBNull = True
                    dt1.Columns.Add(new_col)
                Next

                dt1.Columns("EMPLOYEEID").DataType = Type.GetType("System.Int32")
                'dt1.Columns("EMPLOYEENAME").DataType = Type.GetType("System.Int32")
                dt1.Columns("RESP_1A").DataType = Type.GetType("System.Int32")
                'dt1.Columns("RESP_NAME").DataType = Type.GetType("System.Int32")
                dt1.Columns("DSRDATE").DataType = Type.GetType("System.Int32")
                dt1.Columns("DSRCODE").DataType = Type.GetType("System.Int32")
                dt1.Columns("ROWID").DataType = Type.GetType("System.Int32")
                'dt1.Columns("LOGDATE").DataType = Type.GetType("System.Int32")


                Dim i1 As Integer
                For i1 = 0 To dt.Rows.Count - 1
                    Dim row As DataRow = dt1.NewRow()
                    If dt.Rows(i1)("EMPLOYEEID").ToString.Trim.Length > 0 Then
                        row("EMPLOYEEID") = dt.Rows(i1)("EMPLOYEEID")
                    End If
                    If dt.Rows(i1)("EMPLOYEENAME").ToString.Trim.Length > 0 Then
                        row("EMPLOYEENAME") = dt.Rows(i1)("EMPLOYEENAME")
                    End If

                    If dt.Rows(i1)("RESP_1A").ToString.Trim.Length > 0 Then
                        row("RESP_1A") = dt.Rows(i1)("RESP_1A")
                    End If

                    If dt.Rows(i1)("RESP_NAME").ToString.Trim.Length > 0 Then
                        row("RESP_NAME") = dt.Rows(i1)("RESP_NAME")
                    End If

                    If dt.Rows(i1)("DSRDATE").ToString.Trim.Length > 0 Then
                        row("DSRDATE") = dt.Rows(i1)("DSRDATE")
                    End If

                    If dt.Rows(i1)("ROWID").ToString.Trim.Length > 0 Then
                        row("ROWID") = dt.Rows(i1)("ROWID")
                    End If

                    If dt.Rows(i1)("LOGDATE").ToString.Trim.Length > 0 Then
                        row("LOGDATE") = dt.Rows(i1)("LOGDATE")
                    End If

                    If dt.Rows(i1)("DSRCODE").ToString.Trim.Length > 0 Then
                        row("DSRCODE") = dt.Rows(i1)("DSRCODE")
                    End If


                    dt1.Rows.Add(row)
                    dt1.AcceptChanges()
                Next
                dt1.AcceptChanges()
                dv = New DataView(dt1)
                dv.Sort = ViewState("SortName").ToString() + " " + ViewState("Direction").ToString()

                dv.RowFilter = "ROWID > 0 "

                gvDSRBackDated.DataSource = dv
                gvDSRBackDated.DataBind()
                SetImageForSorting(gvDSRBackDated)
                txtRecordCount.Text = dv.Count
            End If
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
        Try
            lblError.Text = ""
            gvDSRBackDated.PageIndex = ddlPageNumber.SelectedValue - 1
            Sorting()
            BindControlsForNavigation(gvDSRBackDated.PageCount)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click


        If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
            ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
        End If
        lblError.Text = ""
        gvDSRBackDated.PageIndex = ddlPageNumber.SelectedValue - 1
        Sorting()
        BindControlsForNavigation(gvDSRBackDated.PageCount)
    End Sub
    Private Sub BindControlsForNavigation(ByVal CrrentPageNo As String)
        pnlPaging.Visible = True
        Dim count As Integer = CInt(CrrentPageNo)
        Dim selectedValue As String = IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue)
        If count <> ddlPageNumber.Items.Count Then
            selectedValue = 1
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


            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            lblError.Text = ""
            gvDSRBackDated.PageIndex = ddlPageNumber.SelectedValue - 1
            Sorting()
            BindControlsForNavigation(gvDSRBackDated.PageCount)

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim RowNum As Integer
        Dim objDetailsNode As XmlNode
        Dim objDetailsCloneNode As XmlNode
        Dim xmlDoc As New XmlDocument
        Try

            If drpSalesPerson.SelectedValue.Trim.Length = 0 Then
                lblError.Text = "Assigned to is mandatory."
                Exit Sub
            End If
            If txtDSRDate.Text.Trim.Length = 0 Then
                lblError.Text = "Date is mandatory."
                Exit Sub
            End If

            Dim objXmlDoc As New XmlDocument
            If Session("FinalDSRBackDated") Is Nothing Then
                objXmlDoc.LoadXml("<SL_SEARCH_DSR_BACKDATE_OUTPUT> <PAGE PAGE_COUNT='' TOTAL_ROWS='' />  <Errors Status='FALSE'>    <Error Code='' Description='' />  </Errors>  <DSR_BACKDATE EMPLOYEEID='' EMPLOYEENAME='' RESP_1A='' RESP_NAME='' DSRDATE='' DSRCODE='' ROWID='' LOGDATE='' ID='' /></SL_SEARCH_DSR_BACKDATE_OUTPUT>")
                Session("FinalDSRBackDated") = objXmlDoc.OuterXml
            End If
            If Session("FinalDSRBackDated") IsNot Nothing Then
                objXmlDoc.LoadXml(Session("FinalDSRBackDated"))
                If BtnAdd.Text.Trim.ToUpper = "UPDATE" Then
                    If HdRowId.Value.Trim.Length > 0 Then
                        objDetailsNode = objXmlDoc.DocumentElement.SelectSingleNode("DSR_BACKDATE[@ROWID=" + HdRowId.Value.Trim + "]")
                        If Not objDetailsNode Is Nothing Then

                            '@ Start of For Checking dulicate data
                            Dim objnodeclone As XmlNode = objDetailsNode.CloneNode(True)
                            For Each objattributes As XmlAttribute In objnodeclone.Attributes
                                objattributes.Value = ""
                            Next

                            If drpSalesPerson.SelectedValue <> "" Then
                                objnodeclone.Attributes("RESP_1A").Value = drpSalesPerson.SelectedValue
                                objnodeclone.Attributes("RESP_NAME").Value = drpSalesPerson.SelectedItem.Text
                            End If
                            objnodeclone.Attributes("DSRDATE").Value = objeAAMS.ConvertTextDate(txtDSRDate.Text.Trim)
                            objnodeclone.Attributes("ROWID").Value = HdRowId.Value.Trim

                            If DuplicateData(objnodeclone) = True Then
                                lblError.Text = "Duplicate Record."
                                Exit Sub
                            End If
                            '@ End of For Checking dulicate data


                            If drpSalesPerson.SelectedValue <> "" Then
                                objDetailsNode.Attributes("RESP_1A").Value = drpSalesPerson.SelectedValue
                                objDetailsNode.Attributes("RESP_NAME").Value = drpSalesPerson.SelectedItem.Text
                            End If
                            objDetailsNode.Attributes("DSRDATE").Value = objeAAMS.ConvertTextDate(txtDSRDate.Text.Trim)


                            HdRowId.Value = ""
                            BtnAdd.Text = "Add"
                            clearcontrol()
                            BtnCancel.Enabled = False
                        End If
                    End If
                Else  ' @In Add 
                    objDetailsNode = objXmlDoc.DocumentElement.SelectSingleNode("DSR_BACKDATE")
                    objDetailsCloneNode = objDetailsNode.CloneNode(True)

                    For Each objattributes As XmlAttribute In objDetailsCloneNode.Attributes
                        objattributes.Value = ""
                    Next
                    ' <DSR_BACKDATE EMPLOYEEID="24" EMPLOYEENAME="admin" RESP_1A="2" RESP_NAME="Jitender" DSRDATE="20110702" DSRCODE="" ROWID="1" LOGDATE="" />
                   

                    If Not Session("LoginSession") Is Nothing Then
                        objDetailsCloneNode.Attributes("EMPLOYEEID").Value = Session("LoginSession").ToString().Split("|")(0)
                        If Session("Security") IsNot Nothing Then
                            xmlDoc.LoadXml(Session("Security").ToString)
                            objDetailsCloneNode.Attributes("EMPLOYEENAME").Value = xmlDoc.DocumentElement.SelectSingleNode("Employee_Name").InnerText
                        End If
                    End If
                    If drpSalesPerson.SelectedValue <> "" Then
                        objDetailsCloneNode.Attributes("RESP_1A").Value = drpSalesPerson.SelectedValue
                        objDetailsCloneNode.Attributes("RESP_NAME").Value = drpSalesPerson.SelectedItem.Text
                    End If


                    objDetailsCloneNode.Attributes("DSRDATE").Value = objeAAMS.ConvertTextDate(txtDSRDate.Text.Trim)
                    objDetailsCloneNode.Attributes("DSRCODE").Value = ""
                    objDetailsCloneNode.Attributes("ROWID").Value = gvDSRBackDated.Rows.Count + 1
                    objDetailsCloneNode.Attributes("LOGDATE").Value = ""

                    '@ Start of Duplicate Checking
                    If DuplicateData(objDetailsCloneNode) = True Then
                        lblError.Text = "Duplicate Record."
                        Exit Sub
                    End If
                    '@ End of Duplicate Checking

                    objXmlDoc.DocumentElement.AppendChild(objDetailsCloneNode)


                    HdRowId.Value = ""
                    BtnAdd.Text = "Add"
                    clearcontrol()
                    BtnCancel.Enabled = False
                    '@Start of UpdateSequenceNo
                    RowNum = 1
                    For Each objnode As XmlNode In objXmlDoc.DocumentElement.SelectNodes("DSR_BACKDATE")
                        If objnode.Attributes("ROWID").Value <> "" Then
                            objnode.Attributes("ROWID").Value = RowNum
                            RowNum = RowNum + 1
                        End If
                    Next
                    '@End of UpdateSequenceNo
                End If

                objXmlReader = New XmlNodeReader(objXmlDoc)
                ds.ReadXml(objXmlReader)
                Session("DSRBackDatedDataSource") = ds.Tables("DSR_BACKDATE")
                Session("FinalDSRBackDated") = objXmlDoc.OuterXml

                Sorting()
                BindControlsForNavigation(gvDSRBackDated.PageCount)
                pnlPaging.Visible = True
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    Protected Sub gvDSRBackDated_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvDSRBackDated.RowCommand
        Dim objXmlReader As XmlNodeReader
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzDSR As New AAMS.bizSales.bzDSR

        lblError.Text = ""
       
        Dim ds As New DataSet
        Try
            Dim strCommandArgs As String = ""
            Dim objXmlDoc As New XmlDocument
            Dim objDetailsNode As XmlNode
            If e.CommandName = "DeleteX" Then
                If Session("FinalDSRBackDated") IsNot Nothing Then
                    strCommandArgs = e.CommandArgument
                    objXmlDoc.LoadXml(Session("FinalDSRBackDated"))
                    objDetailsNode = objXmlDoc.DocumentElement.SelectSingleNode("DSR_BACKDATE[@ROWID='" + strCommandArgs.Trim + "']")
                    If objDetailsNode IsNot Nothing Then

                        '@ Start For Deletion in Database
                        If objDetailsNode.Attributes("ID").Value <> "" Then
                            objInputXml.LoadXml("<SL_DELETE_DSR_BACKDATE_INPUT><ID></ID>	</SL_DELETE_DSR_BACKDATE_INPUT>")
                            objInputXml.DocumentElement.SelectSingleNode("ID").InnerText = objDetailsNode.Attributes("ID").Value

                            objOutputXml = objbzDSR.Delete_DSRBackDate(objInputXml)
                            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "TRUE" Then
                                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                                Exit Sub
                            End If
                        End If

                      
                        '@ End For Deletion in Database

                        objXmlDoc.DocumentElement.RemoveChild(objDetailsNode)

                        If objXmlDoc.DocumentElement.SelectNodes("DSR_BACKDATE[@ROWID!='']").Count > 0 Then
                            '@Start of  UpdateSequenceNo
                            Dim RowNum As Integer = 1
                            For Each objnode As XmlNode In objXmlDoc.DocumentElement.SelectNodes("DSR_BACKDATE")
                                If objnode.Attributes("ROWID").Value <> "" Then
                                    objnode.Attributes("ROWID").Value = RowNum
                                    RowNum = RowNum + 1
                                End If
                            Next
                            '@End of UpdateSequenceNo
                            objXmlReader = New XmlNodeReader(objXmlDoc)
                            ds.ReadXml(objXmlReader)
                            Session("DSRBackDatedDataSource") = ds.Tables("DSR_BACKDATE")
                            Session("FinalDSRBackDated") = objXmlDoc.OuterXml
                            txtRecordCount.Text = ds.Tables("DSR_BACKDATE").Rows.Count
                            Sorting()
                            BindControlsForNavigation(gvDSRBackDated.PageCount)
                        Else
                            Session("DSRBackDatedDataSource") = Nothing
                            Session("FinalDSRBackDated") = Nothing
                            txtRecordCount.Text = 0
                            gvDSRBackDated.DataSource = Nothing
                            gvDSRBackDated.DataBind()
                            pnlPaging.Visible = False
                        End If
                      
                    End If
                Else
                End If
            End If
            If e.CommandName = "EditX" Then
                strCommandArgs = e.CommandArgument
                objXmlDoc.LoadXml(Session("FinalDSRBackDated"))

                objDetailsNode = objXmlDoc.DocumentElement.SelectSingleNode("DSR_BACKDATE[@ROWID='" + strCommandArgs.Trim + "']")
                If objDetailsNode IsNot Nothing Then
                    With objDetailsNode
                        If .Attributes("RESP_1A") IsNot Nothing Then
                            Dim li As New ListItem
                            li = drpSalesPerson.Items.FindByValue(.Attributes("RESP_1A").Value)
                            If li IsNot Nothing Then
                                drpSalesPerson.SelectedValue = li.Value
                            End If
                        End If

                        If .Attributes("DSRDATE") IsNot Nothing Then
                            txtDSRDate.Text = ConvertToCalenderDate(.Attributes("DSRDATE").Value)
                        End If
                        BtnAdd.Text = "Update"
                        BtnCancel.Enabled = True
                        HdRowId.Value = strCommandArgs
                    End With
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub


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

    Protected Sub gvDSRBackDated_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDSRBackDated.RowDataBound
        Dim objSecurityXml As New XmlDocument
        Dim lnkEdit As New LinkButton
        Dim lnkDelete As New LinkButton
        Dim objInputXml As New XmlDocument
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim HdRowId, hdResId, hdDSRCODE, hdDSRDate, hdLoggedDate, hdEmplyeeId As HiddenField
                HdRowId = CType(e.Row.FindControl("HdRowId"), HiddenField)
                'hdResId = CType(e.Row.FindControl("hdResId"), HiddenField)
                hdDSRCODE = CType(e.Row.FindControl("hdDSRCODE"), HiddenField)
                'hdDSRDate = CType(e.Row.FindControl("hdDSRDate"), HiddenField)
                'hdLoggedDate = CType(e.Row.FindControl("hdLoggedDate"), HiddenField)
                'hdEmplyeeId = CType(e.Row.FindControl("hdEmplyeeId"), HiddenField)

                If e.Row.Cells(0).Text.Trim.Length = 8 Then
                    e.Row.Cells(0).Text = ConvertToCalenderDate(e.Row.Cells(0).Text)
                End If


                lnkEdit = e.Row.FindControl("lnkEdit")
                lnkDelete = e.Row.FindControl("lnkDelete")

                Dim strBuilder As New StringBuilder

                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Back Date Entry on DSR Visit']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Back Date Entry on DSR Visit']").Attributes("Value").Value)
                        If strBuilder(2) = "0" Then
                            lnkEdit.Enabled = False
                        End If
                        If strBuilder(3) = "0" Then
                            lnkDelete.Enabled = False
                        End If
                    End If
                Else
                    strBuilder = objeAAMS.SecurityCheck(31)
                End If

                If hdDSRCODE.Value.Trim.Length > 0 Then
                    lnkEdit.Enabled = False
                    lnkDelete.Enabled = False
                End If

            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub

    Protected Sub gvDSRBackDated_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvDSRBackDated.Sorting
        lblError.Text = ""

        Try
            Dim SortName As String = e.SortExpression

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ViewState("Direction") = "DESC"
            Else
                If ViewState("SortName") = SortName Then
                    If ViewState("Direction") = "asc" Then
                        ViewState("Direction") = "desc"
                    Else
                        ViewState("Direction") = "asc"
                    End If
                Else
                    ViewState("SortName") = SortName
                    ViewState("Direction") = "DESC"
                End If
            End If

            Sorting()

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub clearcontrol()
        Try
            drpSalesPerson.SelectedValue = ""
            txtDSRDate.Text = ""
            HdRowId.Value = ""

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub BtnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        clearcontrol()
        BtnAdd.Text = "Add"
        BtnCancel.Enabled = False
    End Sub

    Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If gvDSRBackDated.Rows.Count = 0 Then
            lblError.Text = "There is not any data for saving."
            Exit Sub
        End If
        Dim objinputXml As New XmlDocument
        Dim objFinalinputXml As New XmlDocument
        Dim objOutputXml As New XmlDocument
        Dim objbzDSR As New AAMS.bizSales.bzDSR
        Try
            If Session("FinalDSRBackDated") IsNot Nothing Then
                objinputXml.LoadXml(Session("FinalDSRBackDated").ToString)
                objFinalinputXml.LoadXml("<SL_UPDATE_DSR_BACKDATE_INPUT></SL_UPDATE_DSR_BACKDATE_INPUT>")

                For Each objxmlnode As XmlNode In objinputXml.DocumentElement.SelectNodes("DSR_BACKDATE[@ROWID!='' ]")
                    objFinalinputXml.DocumentElement.AppendChild(objFinalinputXml.ImportNode(objxmlnode, True))
                Next
                If objFinalinputXml.DocumentElement.SelectNodes("DSR_BACKDATE").Count > 0 Then
                    objOutputXml = objbzDSR.Update_BackDate_DSR(objFinalinputXml)
                    Try
                        objFinalinputXml.Save("c:\DSRBackDateInput.xml")
                    Catch ex As Exception

                    End Try

                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        GetDatasource()
                        lblError.Text = objeAAMSMessage.messUpdate
                        CheckSecurity()
                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                Else
                    lblError.Text = "There is not any data for saving."
                    Exit Sub
                End If
            Else
                ' lblError.Text = ""
            End If
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Back Date Entry on DSR Visit']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Back Date Entry on DSR Visit']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                    If strBuilder(1) = "0" Then
                        BtnAdd.Enabled = False
                    End If

                    If strBuilder(1) = "0" AndAlso strBuilder(2) = "0" Then
                        BtnSave.Enabled = False
                    End If

                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Function DuplicateData(ByVal objCurrentnode As XmlNode)
        Dim blnFound As Boolean = False
        Dim objAddXmlDoc As New XmlDocument
        Dim TotAttributes As Integer = 0
        Try
            If Session("FinalDSRBackDated") IsNot Nothing Then
                objAddXmlDoc.LoadXml(Session("FinalDSRBackDated").ToString)
                For Each objnode As XmlNode In objAddXmlDoc.DocumentElement.SelectNodes("DSR_BACKDATE")

                    If objnode.Attributes("RESP_1A").Value.Trim.ToUpper = objCurrentnode.Attributes("RESP_1A").Value.Trim.ToUpper Then
                        blnFound = True
                    Else
                        blnFound = False
                    End If
                    If objnode.Attributes("DSRDATE").Value.Trim.ToUpper = objCurrentnode.Attributes("DSRDATE").Value.Trim.ToUpper.ToUpper And blnFound = True Then
                        blnFound = True
                    Else
                        blnFound = False
                    End If
                    If objnode.Attributes("ROWID").Value.Trim.ToUpper <> objCurrentnode.Attributes("ROWID").Value.Trim.ToUpper And blnFound = True Then
                        blnFound = True
                    Else
                        blnFound = False
                    End If

                    'If objnode.Attributes("EMPLOYEEID").Value.Trim.ToUpper = objCurrentnode.Attributes("EMPLOYEEID").Value.Trim.ToUpper And blnFound = True Then
                    '    blnFound = True
                    'Else
                    '    blnFound = False
                    'End If

                    If blnFound = True Then
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            Return blnFound
        End Try
        Return blnFound
    End Function
#End Region

End Class
