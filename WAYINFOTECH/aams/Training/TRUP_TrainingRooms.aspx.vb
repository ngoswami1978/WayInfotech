
Partial Class Training_TRUP_TrainingRooms
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMS As New eAAMS
#Region "Page Filter()"
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        txtTrainingRoom.Focus()
        Try
            '   Checking Security.
            CheckSecurity()
            If Not Page.IsPostBack Then
                btnAdd.Attributes.Add("onClick", "return ValidateAdd();")
                objeAAMS.BindDropDown(ddlCourse, "COURSE", True, 2)
                ' objeAAMS.BindDropDown(ddlAOffice, "AOFFICE", True)
                FillData()
                If Not Request.QueryString("Location_ID") Is Nothing Then
                    hdPageLocationID.Value = objED.Decrypt(Request.QueryString("Location_ID").ToString)
                End If
                If hdPageLocationID.Value <> "" Then
                    ViewRecords()

                    '****************** Code By pankaj  *********************
                    ddlCourse.Enabled = True
                    btnAdd.Enabled = True
                    ViewCourseAgencyTypeMaxParticipants()
                    '******************  end *********************
                Else
                    ddlCourse.Enabled = False
                    btnAdd.Enabled = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Sub FillData()
        ddlAOffice.Items.Clear()
        Dim objInputXml, objOutputXml As New XmlDocument
        '  Dim objXmlReader As XmlNodeReader
        Dim objNodeList As XmlNodeList
        Dim ds As New DataSet
        Dim objbzTrainingRoom As New AAMS.bizTraining.bzTrainingRoom
        objOutputXml = objbzTrainingRoom.ListAoffice()
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objNodeList = objOutputXml.DocumentElement.SelectNodes("AOFFICE")
            For Each objNode As XmlNode In objNodeList
                Dim li As New ListItem(objNode.Attributes("Aoffice").Value, objNode.Attributes("City").Value & "|" & objNode.Attributes("Aoffice").Value)
                ddlAOffice.Items.Add(li)
            Next

            'objXmlReader = New XmlNodeReader(objOutputXml)
            'ds.ReadXml(objXmlReader)
            'ddlAOffice.DataSource = ds.Tables("AOFFICE")
            'ddlAOffice.DataTextField = "Aoffice"
            'ddlAOffice.DataValueField = "City"
            'ddlAOffice.DataBind()
            '  ddlAOffice.Items.Remove(New ListItem("BIS", "187"))
        End If
        ddlAOffice.Items.Insert(0, New ListItem("--Select One--", ""))

        Dim objbzCity As New AAMS.bizMaster.bzCity
        Dim objOutputXml1 As New XmlDocument
        objOutputXml1 = objbzCity.List()
        If objOutputXml1.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            hdCity.Value = objOutputXml1.OuterXml
        End If
        ddlCity.Items.Insert(0, New ListItem("--Select One--", ""))

    End Sub

    Sub ViewRecords()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzTrainingRoom As New AAMS.bizTraining.bzTrainingRoom
        objInputXml.LoadXml("<TR_VIEWTRAININGROOM_INPUT><TR_CLOCATION_ID /></TR_VIEWTRAININGROOM_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("TR_CLOCATION_ID").InnerText = hdPageLocationID.Value
        objOutputXml = objbzTrainingRoom.View(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '<TRAININGROOM TR_CLOCATION_ID="10905" AOFFICE="DEL" TR_CLOCATION_MAXNBPART="1" 
            'TR_CLOCATION_NAME="DEL 1111" TR_CLOCATION_PROTECT="False" TR_CLOCATION_ONSITE="0" CITYID="187" /> 
            With objOutputXml.DocumentElement.SelectSingleNode("TRAININGROOM")
                ddlAOffice.ClearSelection()
                ddlAOffice.Items.FindByText(.Attributes("AOFFICE").Value).Selected = True
                chkOnSite.Checked = IIf(.Attributes("TR_CLOCATION_ONSITE").Value = "0", False, True)
                
                FillCity(.Attributes("AOFFICE").Value)
                Dim li As New ListItem
                li = ddlCity.Items.FindByValue(.Attributes("CITYID").Value)
                If li IsNot Nothing Then
                    ddlCity.SelectedValue = li.Value
                End If
                'ddlCity.SelectedValue = .Attributes("CITYID").Value
                If chkOnSite.Checked = True Then
                    ddlCity.Enabled = False
                Else
                    ddlCity.Enabled = True
                End If
                txtTrainingRoom.Text = .Attributes("TR_CLOCATION_NAME").Value
                txtNoOfParticipant.Text = .Attributes("TR_CLOCATION_MAXNBPART").Value
            End With
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzTrainingRoom As New AAMS.bizTraining.bzTrainingRoom
        Dim intVar As Int32
        Dim objDocFrag As XmlDocumentFragment
        Dim objMaxPartXmlDoc As New XmlDocument
        Try
            objInputXml.LoadXml("<TR_UPDATETRAININGROOM_INPUT><TRAININGROOM TR_CLOCATION_ID='' AOFFICE='' CITYID='' TR_CLOCATION_MAXNBPART='' TR_CLOCATION_NAME='' TR_CLOCATION_PROTECT='' TR_CLOCATION_ONSITE='' /></TR_UPDATETRAININGROOM_INPUT>")
            With objInputXml.DocumentElement.SelectSingleNode("TRAININGROOM")
                .Attributes("AOFFICE").Value = ddlAOffice.SelectedItem.Text

                'Added on 25feb09
                If chkOnSite.Checked Then
                    .Attributes("CITYID").Value = ddlAOffice.SelectedValue.Split("|").GetValue(0)
                    Dim li As New ListItem
                    li = ddlCity.Items.FindByValue(ddlAOffice.SelectedValue.Split("|").GetValue(0))
                    If li IsNot Nothing Then
                        ddlCity.SelectedValue = li.Value
                    Else
                        lblError.Text = "City is not defined for the selected AOffice"
                        Exit Sub
                    End If
                    '  ddlCity.SelectedValue = ddlAOffice.SelectedValue.Split("|").GetValue(0)
                Else
                    .Attributes("CITYID").Value = ddlCity.SelectedValue
                End If
                'End
                'Commented on 25 feb 09 'Because ddlcity.selectedvalue giving wrong value when checkbox on site is checked
                'If ddlCity.SelectedValue = "" Then
                '    .Attributes("CITYID").Value = hdCityID.Value
                'Else
                '    .Attributes("CITYID").Value = ddlCity.SelectedValue
                'End If
                'End code
                .Attributes("TR_CLOCATION_MAXNBPART").Value = txtNoOfParticipant.Text
                .Attributes("TR_CLOCATION_NAME").Value = txtTrainingRoom.Text
                .Attributes("TR_CLOCATION_ONSITE").Value = IIf(chkOnSite.Checked = True, "1", "0")
                If hdPageLocationID.Value <> "" Then
                    .Attributes("TR_CLOCATION_ID").Value = hdPageLocationID.Value
                End If
            End With
            ' Code Added On 28 Jan 09
            ' Code Added By Pankaj
            If gvMaxParticipants.Rows.Count > 0 Then
                If hdData.Value <> "" Then
                    objMaxPartXmlDoc.LoadXml(hdData.Value)
                    ' Removing Blank Node
                    For Each objDelNode As XmlNode In objMaxPartXmlDoc.DocumentElement.SelectNodes("COURSE[@COURSE_ID='']")
                        objMaxPartXmlDoc.DocumentElement.RemoveChild(objDelNode)
                    Next
                    'Updating Values In Xml
                    Dim strCourseID As String
                    Dim hdID As HiddenField
                    Dim objTempNode As XmlNode
                    For intVar = 0 To gvMaxParticipants.Rows.Count - 1
                        hdID = CType(gvMaxParticipants.Rows(intVar).FindControl("hdCourseId"), HiddenField)
                        If Not hdID Is Nothing Then
                            strCourseID = hdID.Value
                            objTempNode = objMaxPartXmlDoc.DocumentElement.SelectSingleNode("COURSE[@COURSE_ID='" + strCourseID + "']")
                            If Not objTempNode Is Nothing Then
                                objTempNode.Attributes("A").Value = CType(gvMaxParticipants.Rows(intVar).FindControl("txtA"), TextBox).Text
                                objTempNode.Attributes("B").Value = CType(gvMaxParticipants.Rows(intVar).FindControl("txtB"), TextBox).Text
                                objTempNode.Attributes("C").Value = CType(gvMaxParticipants.Rows(intVar).FindControl("txtC"), TextBox).Text
                                objTempNode.Attributes("D").Value = CType(gvMaxParticipants.Rows(intVar).FindControl("txtD"), TextBox).Text
                                objTempNode.Attributes("E").Value = CType(gvMaxParticipants.Rows(intVar).FindControl("txtE"), TextBox).Text
                                objTempNode.Attributes("G").Value = CType(gvMaxParticipants.Rows(intVar).FindControl("txtG"), TextBox).Text
                            End If
                        End If
                    Next
                    objDocFrag = objInputXml.CreateDocumentFragment()
                    objDocFrag.InnerXml = objMaxPartXmlDoc.DocumentElement.InnerXml
                    objInputXml.DocumentElement.AppendChild(objDocFrag)
                End If

            End If

            'End
            'Here Back end Method Call
            objOutputXml = objbzTrainingRoom.Update(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If hdPageLocationID.Value = "" Then
                    lblError.Text = objeAAMSMessage.messInsert
                    hdPageLocationID.Value = objOutputXml.DocumentElement.SelectSingleNode("TRAININGROOM").Attributes("TR_CLOCATION_ID").Value
                    ' Code Added On 28 Jan 09
                    ' Code Added By Pankaj
                    ddlCourse.Enabled = True
                    btnAdd.Enabled = True
                    ViewCourseAgencyTypeMaxParticipants()
                    ' End
                Else
                    lblError.Text = objeAAMSMessage.messUpdate
                End If
                If chkOnSite.Checked = True Then
                    ddlCity.Enabled = False
                Else
                    ddlCity.Enabled = True
                End If
                '   Checking Security.
                CheckSecurity()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
           
        End Try
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("TRUP_TrainingRooms.aspx?Action=I")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If hdPageLocationID.Value <> "" Then
                ViewRecords()
                '****************** Code By pankaj  *********************
                ddlCourse.Enabled = True
                btnAdd.Enabled = True
                ddlCourse.SelectedIndex = 0
                ViewCourseAgencyTypeMaxParticipants()
                '******************  end *********************
            Else
                Dim strQueryString As String = Request.QueryString.ToString
                Response.Redirect("TRUP_TrainingRooms.aspx?" + strQueryString)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub

    Protected Sub ddlAOffice_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAOffice.SelectedIndexChanged
        FillCity()
    End Sub

    Sub FillCity()
        If ddlAOffice.SelectedValue = "" Then
            ddlCity.Items.Clear()
            ddlCity.Items.Insert(0, New ListItem("--Select One--", ""))
        Else
            ddlCity.Items.Clear()
            If hdCity.Value <> "" Then
                Dim objOutputXml As New XmlDocument
                Dim objNodeList As XmlNodeList
                Dim ds As New DataSet
                objOutputXml.LoadXml(hdCity.Value)
                objNodeList = objOutputXml.DocumentElement.SelectNodes("CITY[@Aoffice='" + ddlAOffice.SelectedItem.Text + "']")
                For Each objNode As XmlNode In objNodeList



                    Dim li As New ListItem(objNode.Attributes("City_Name").Value, objNode.Attributes("CityID").Value)
                    ddlCity.Items.Add(li)

                Next
            End If
            ddlCity.Items.Insert(0, New ListItem("--Select One--", ""))
        End If
    End Sub

    Sub FillCity(ByVal Aoffice As String)
        ddlCity.Items.Clear()
        If hdCity.Value <> "" Then
            Dim objOutputXml As New XmlDocument
            Dim objNodeList As XmlNodeList
            Dim ds As New DataSet
            objOutputXml.LoadXml(hdCity.Value)
            objNodeList = objOutputXml.DocumentElement.SelectNodes("CITY[@Aoffice='" + Aoffice + "']")
            For Each objNode As XmlNode In objNodeList
                Dim li As New ListItem(objNode.Attributes("City_Name").Value, objNode.Attributes("CityID").Value)
                ddlCity.Items.Add(li)
            Next
        End If
        ddlCity.Items.Insert(0, New ListItem("--Select One--", ""))

    End Sub

#Region "CheckSecurity()::This method is used for security check."
    Private Sub CheckSecurity()
        Try
            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Training Room']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Training Room']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                End If

                If strBuilder(1) = "0" Then
                    btnAdd.Enabled = False
                    btnNew.Enabled = False
                    btnSave.Enabled = False
                End If
                If strBuilder(2) = "0" And (hdPageLocationID.Value <> "" Or Request.QueryString("Location_ID") IsNot Nothing) Then
                    btnAdd.Enabled = False
                    btnSave.Enabled = False
                End If

                If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                    btnAdd.Enabled = True
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

#Region "ViewCourseAgencyTypeMaxParticipants()"
    Private Sub ViewCourseAgencyTypeMaxParticipants()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzTrainingRoom As New AAMS.bizTraining.bzTrainingRoom
        Try
            objInputXml.LoadXml("<TR_VIEWTRAININGROOM_INPUT><TR_CLOCATION_ID /></TR_VIEWTRAININGROOM_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("TR_CLOCATION_ID").InnerText = hdPageLocationID.Value
            objOutputXml = objbzTrainingRoom.ViewAgencyWiseMaxPart(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                'objOutputXml.Load("c:\admin\TR_VIEWTRAININGROOM_COURSE_MAXPARTICIPANT_OUTPUT1.xml")
                hdData.Value = objOutputXml.OuterXml.ToString
                BindGrid()
            End If


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "BindGrid()"
    Private Sub BindGrid()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim objXmlNodeList As XmlNodeList
        Dim ds As New DataSet
        Try
            objOutputXml.LoadXml(hdData.Value)
            objXmlNodeList = objOutputXml.DocumentElement.SelectNodes("COURSE[@COURSE_ID='']")
            ' Removing Blank Node
            For Each objDelNode As XmlNode In objXmlNodeList
                objOutputXml.DocumentElement.RemoveChild(objDelNode)
            Next
            ViewState("SortName") = "COURSE_NAME"
            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "ASC"
            End If

            Dim strSorting As String = "COURSE_NAME " + ViewState("Desc").ToString().Trim
            If objOutputXml.DocumentElement.SelectNodes("COURSE").Count > 0 Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                If ds.Tables("COURSE").Rows.Count > 0 Then
                    Dim objDv As DataView
                    objDv = ds.Tables("COURSE").DefaultView
                    objDv.Sort = strSorting
                    gvMaxParticipants.DataSource = objDv
                    gvMaxParticipants.DataBind()
                    SetImageForSorting(gvMaxParticipants)
                End If
            Else
                gvMaxParticipants.DataSource = Nothing
                gvMaxParticipants.DataBind()

                'Dim objDTable As DataTable
                'Dim objNewRow As DataRow
                'objDTable = New DataTable("COURSE")
                'Dim objCol As DataColumn
                'objCol = New DataColumn

                'objCol = New DataColumn("COURSE_ID")
                'objCol.DataType = System.Type.GetType("System.String")
                'objDTable.Columns.Add(objCol)

                'objCol = New DataColumn("COURSE_NAME")
                'objCol.DataType = System.Type.GetType("System.String")
                'objDTable.Columns.Add(objCol)

                'objCol = New DataColumn("A")
                'objCol.DataType = System.Type.GetType("System.String")
                'objDTable.Columns.Add(objCol)

                'objCol = New DataColumn("B")
                'objCol.DataType = System.Type.GetType("System.String")
                'objDTable.Columns.Add(objCol)

                'objCol = New DataColumn("C")
                'objCol.DataType = System.Type.GetType("System.String")
                'objDTable.Columns.Add(objCol)

                'objCol = New DataColumn("D")
                'objCol.DataType = System.Type.GetType("System.String")
                'objDTable.Columns.Add(objCol)

                'objCol = New DataColumn("E")
                'objCol.DataType = System.Type.GetType("System.String")
                'objDTable.Columns.Add(objCol)

                'objCol = New DataColumn("G")
                'objCol.DataType = System.Type.GetType("System.String")
                'objDTable.Columns.Add(objCol)
                'objNewRow = objDTable.NewRow()
                ''objNewRow("COURSE_ID") = ""
                ''objNewRow("COURSE_NAME") = ""
                ''objNewRow("A") = ""
                ''objNewRow("B") = ""
                ''objNewRow("C") = ""
                ''objNewRow("D") = ""
                ''objNewRow("E") = ""
                ''objNewRow("G") = ""
                'objDTable.Rows.Add(objNewRow)
                'objDTable.AcceptChanges()
                'gvMaxParticipants.DataSource = objDTable
                'gvMaxParticipants.DataBind()
                'gvMaxParticipants.ShowHeader = True
            End If
            If objOutputXml.DocumentElement.SelectSingleNode("COURSE") Is Nothing Then
                hdData.Value = "<TR_VIEWTRAININGROOM_COURSE_MAXPARTICIPANT_OUTPUT><COURSE COURSE_ID='' COURSE_NAME='' A='' B='' C='' D='' E='' G='' /></TR_VIEWTRAININGROOM_COURSE_MAXPARTICIPANT_OUTPUT>"
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click"
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim objXmlDoc As New XmlDocument
        Dim objXmlNode As XmlNode
        Dim objXmlNodeClone As XmlNode
        Dim strCourseId As String
        Dim intVar As Integer
        Try
            ' Updating Values
            Dim strID As String
            Dim hdID As HiddenField
            Dim objTempNode As XmlNode

            If hdData.Value <> "" Then
                objXmlDoc.LoadXml(hdData.Value)
                For intVar = 0 To gvMaxParticipants.Rows.Count - 1
                    hdID = CType(gvMaxParticipants.Rows(intVar).FindControl("hdCourseId"), HiddenField)
                    If Not hdID Is Nothing Then
                        strID = hdID.Value
                        objTempNode = objXmlDoc.DocumentElement.SelectSingleNode("COURSE[@COURSE_ID='" + strID + "']")
                        If Not objTempNode Is Nothing Then
                            objTempNode.Attributes("A").Value = CType(gvMaxParticipants.Rows(intVar).FindControl("txtA"), TextBox).Text
                            objTempNode.Attributes("B").Value = CType(gvMaxParticipants.Rows(intVar).FindControl("txtB"), TextBox).Text
                            objTempNode.Attributes("C").Value = CType(gvMaxParticipants.Rows(intVar).FindControl("txtC"), TextBox).Text
                            objTempNode.Attributes("D").Value = CType(gvMaxParticipants.Rows(intVar).FindControl("txtD"), TextBox).Text
                            objTempNode.Attributes("E").Value = CType(gvMaxParticipants.Rows(intVar).FindControl("txtE"), TextBox).Text
                            objTempNode.Attributes("G").Value = CType(gvMaxParticipants.Rows(intVar).FindControl("txtG"), TextBox).Text
                        End If
                    End If
                Next
                hdData.Value = objXmlDoc.OuterXml.ToString
            End If
            'end
            If ddlCourse.SelectedValue <> "" Then
                strCourseId = ddlCourse.SelectedValue
                ' objXmlDoc.LoadXml(hdData.Value)
                objXmlNode = objXmlDoc.DocumentElement.SelectSingleNode("COURSE[@COURSE_ID='" + strCourseId + "']")
                If Not objXmlNode Is Nothing Then
                    lblError.Text = "Course already added."
                Else
                    objXmlNodeClone = objXmlDoc.DocumentElement.SelectSingleNode("COURSE").CloneNode(True)
                    objXmlNodeClone.Attributes("COURSE_ID").Value = strCourseId
                    objXmlNodeClone.Attributes("COURSE_NAME").Value = ddlCourse.SelectedItem.Text
                    objXmlNodeClone.Attributes("A").Value = ""
                    objXmlNodeClone.Attributes("B").Value = ""
                    objXmlNodeClone.Attributes("C").Value = ""
                    objXmlNodeClone.Attributes("D").Value = ""
                    objXmlNodeClone.Attributes("E").Value = ""
                    objXmlNodeClone.Attributes("G").Value = ""
                    objXmlDoc.DocumentElement.AppendChild(objXmlNodeClone)
                End If
                hdData.Value = objXmlDoc.OuterXml.ToString
                BindGrid()
            Else
                lblError.Text = "Course is mandatory."
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "gvMaxParticipants_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMaxParticipants.RowCommand"
    Protected Sub gvMaxParticipants_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMaxParticipants.RowCommand
        Dim objXmlDoc As New XmlDocument
        Dim objXmlNode As XmlNode
        Dim intVar As Integer
        Dim objTempNode As XmlNode
        Dim hdID As HiddenField
        Dim strID As String
        Try

            If hdData.Value <> "" Then
                objXmlDoc.LoadXml(hdData.Value)
                For intVar = 0 To gvMaxParticipants.Rows.Count - 1
                    hdID = CType(gvMaxParticipants.Rows(intVar).FindControl("hdCourseId"), HiddenField)
                    If Not hdID Is Nothing Then
                        strID = hdID.Value
                        objTempNode = objXmlDoc.DocumentElement.SelectSingleNode("COURSE[@COURSE_ID='" + strID + "']")
                        If Not objTempNode Is Nothing Then
                            objTempNode.Attributes("A").Value = CType(gvMaxParticipants.Rows(intVar).FindControl("txtA"), TextBox).Text
                            objTempNode.Attributes("B").Value = CType(gvMaxParticipants.Rows(intVar).FindControl("txtB"), TextBox).Text
                            objTempNode.Attributes("C").Value = CType(gvMaxParticipants.Rows(intVar).FindControl("txtC"), TextBox).Text
                            objTempNode.Attributes("D").Value = CType(gvMaxParticipants.Rows(intVar).FindControl("txtD"), TextBox).Text
                            objTempNode.Attributes("E").Value = CType(gvMaxParticipants.Rows(intVar).FindControl("txtE"), TextBox).Text
                            objTempNode.Attributes("G").Value = CType(gvMaxParticipants.Rows(intVar).FindControl("txtG"), TextBox).Text
                        End If
                    End If
                Next
                hdData.Value = objXmlDoc.OuterXml.ToString
            End If
            If e.CommandName = "DeleteX" Then
                ' uadating values
                'If hdData.Value <> "" Then
                '    objXmlDoc.LoadXml(hdData.Value)
                '    For intVar = 0 To gvMaxParticipants.Rows.Count - 1
                '        hdID = CType(gvMaxParticipants.Rows(intVar).FindControl("hdCourseId"), HiddenField)
                '        If Not hdID Is Nothing Then
                '            strID = hdID.Value
                '            objTempNode = objXmlDoc.DocumentElement.SelectSingleNode("COURSE[@COURSE_ID='" + strID + "']")
                '            If Not objTempNode Is Nothing Then
                '                objTempNode.Attributes("A").Value = CType(gvMaxParticipants.Rows(intVar).FindControl("txtA"), TextBox).Text
                '                objTempNode.Attributes("B").Value = CType(gvMaxParticipants.Rows(intVar).FindControl("txtB"), TextBox).Text
                '                objTempNode.Attributes("C").Value = CType(gvMaxParticipants.Rows(intVar).FindControl("txtC"), TextBox).Text
                '                objTempNode.Attributes("D").Value = CType(gvMaxParticipants.Rows(intVar).FindControl("txtD"), TextBox).Text
                '                objTempNode.Attributes("E").Value = CType(gvMaxParticipants.Rows(intVar).FindControl("txtE"), TextBox).Text
                '                objTempNode.Attributes("G").Value = CType(gvMaxParticipants.Rows(intVar).FindControl("txtG"), TextBox).Text
                '            End If
                '        End If
                '    Next
                '    hdData.Value = objXmlDoc.OuterXml.ToString
                'End If
                'objXmlDoc.LoadXml(hdData.Value)
                objXmlNode = objXmlDoc.DocumentElement.SelectSingleNode("COURSE[@COURSE_ID='" + e.CommandArgument + "']")
                objXmlDoc.DocumentElement.RemoveChild(objXmlNode)
                hdData.Value = objXmlDoc.OuterXml.ToString
                BindGrid()
                ' lblError.Text = objeAAMSMessage.messDelete
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "gvMaxParticipants_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvMaxParticipants.RowDataBound"
    Protected Sub gvMaxParticipants_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvMaxParticipants.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
        'If e.Row.Cells(0).Text.Trim() = "&nbsp;" Or e.Row.Cells(0).Text.Trim() = "" Then
        '    e.Row.Cells.Clear()
        '    Exit Sub
        'End If

        CType(e.Row.Cells(1).Controls(1), TextBox).Attributes.Add("onkeyup", "checknumeric('" + CType(e.Row.Cells(1).Controls(1), TextBox).ClientID + "')")
        CType(e.Row.Cells(2).Controls(1), TextBox).Attributes.Add("onkeyup", "checknumeric('" + CType(e.Row.Cells(2).Controls(1), TextBox).ClientID + "')")
        CType(e.Row.Cells(3).Controls(1), TextBox).Attributes.Add("onkeyup", "checknumeric('" + CType(e.Row.Cells(3).Controls(1), TextBox).ClientID + "')")
        CType(e.Row.Cells(4).Controls(1), TextBox).Attributes.Add("onkeyup", "checknumeric('" + CType(e.Row.Cells(4).Controls(1), TextBox).ClientID + "')")
        CType(e.Row.Cells(5).Controls(1), TextBox).Attributes.Add("onkeyup", "checknumeric('" + CType(e.Row.Cells(5).Controls(1), TextBox).ClientID + "')")
        CType(e.Row.Cells(6).Controls(1), TextBox).Attributes.Add("onkeyup", "checknumeric('" + CType(e.Row.Cells(6).Controls(1), TextBox).ClientID + "')")
    End Sub
#End Region

#Region "gvMaxParticipants_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvMaxParticipants.Sorted"
    Protected Sub gvMaxParticipants_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvMaxParticipants.Sorted

    End Sub
#End Region

#Region "gvMaxParticipants_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvMaxParticipants.Sorting"
    Protected Sub gvMaxParticipants_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvMaxParticipants.Sorting
        Try
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ViewState("Desc") = "DESC"
            Else
                If ViewState("SortName") = SortName Then
                    If ViewState("Desc") = "ASC" Then
                        ViewState("Desc") = "DESC"
                    Else
                        ViewState("Desc") = "ASC"
                    End If
                Else
                    ViewState("SortName") = SortName
                    ViewState("Desc") = "DESC"
                End If
            End If
            BindGrid()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "SetImageForSorting(ByVal grd As GridView)"
    Private Sub SetImageForSorting(ByVal grd As GridView)
        Dim imgUp As New Image
        imgUp.ImageUrl = "~/Images/Sortup.gif"
        Dim imgDown As New Image
        imgDown.ImageUrl = "~/Images/Sortdown.gif"
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Dim intcol As Integer = grd.Columns.IndexOf(field)
                If ViewState("Desc") = "DESC" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If
                If ViewState("Desc") = "ASC" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
            End If
        Next
    End Sub
#End Region

    'Protected Sub gvCourse_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCourse.RowDataBound
    '    Dim objInputXml, objOutputXml As New XmlDocument
    '    Dim objXmlReader As XmlNodeReader
    '    Dim ds As New DataSet
    '    Dim objDTable As DataTable
    '    Dim objNewRow As DataRow

    '    Try
    '        If e.Row.RowIndex < 0 Then
    '            Exit Sub
    '        End If
    '        Dim gvPart As GridView
    '        Dim hdCOURSE_ID As HiddenField
    '        gvPart = CType(e.Row.FindControl("gvParticipants"), GridView)
    '        hdCOURSE_ID = CType(e.Row.FindControl("hdCourseID"), HiddenField)
    '        If gvPart IsNot Nothing Then
    '            'objeAAMS.BindDropDown(ddl, "PARTCIPANTSTATUS", True)

    '            objOutputXml.Load("c:\admin\TR_VIEWTRAININGROOM_COURSE_MAXPARTICIPANT_OUTPUT.xml")


    '            objDTable = New DataTable("DETAILS")
    '            Dim objCol As DataColumn
    '            objCol = New DataColumn

    '            objCol = New DataColumn("AGENCY_TYPE")
    '            objCol.DataType = System.Type.GetType("System.String")
    '            objDTable.Columns.Add(objCol)

    '            objCol = New DataColumn("MAX_NO_PARTICIPANTS")
    '            objCol.DataType = System.Type.GetType("System.String")
    '            objDTable.Columns.Add(objCol)

    '            Dim objXmlCourseNode As XmlNode

    '            objXmlCourseNode = objOutputXml.DocumentElement.SelectSingleNode("COURSE[@COURSE_ID='" + hdCOURSE_ID.Value.Trim + "']")
    '            For Each objDetails As XmlNode In objXmlCourseNode.SelectNodes("DETAILS")
    '                objNewRow = objDTable.NewRow()
    '                objNewRow("AGENCY_TYPE") = objDetails.Attributes("AGENCY_TYPE").InnerText
    '                objNewRow("MAX_NO_PARTICIPANTS") = objDetails.Attributes("MAX_NO_PARTICIPANTS").InnerText
    '                objDTable.Rows.Add(objNewRow)
    '            Next



    '            objDTable.AcceptChanges()
    '            If objDTable.Rows.Count <> 0 Then
    '                gvPart.DataSource = objDTable
    '                gvPart.DataBind()
    '            End If

    '        End If
    '    Catch ex As Exception

    '    End Try

    'End Sub

End Class
