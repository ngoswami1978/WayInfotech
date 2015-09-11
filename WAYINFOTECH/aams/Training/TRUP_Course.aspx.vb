
Partial Class Training_TRUP_Course
    Inherits System.Web.UI.Page

#Region "Global Variable Declaration"
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Public strBuilder As New StringBuilder
    Protected strIndex As String
    Protected flagPermission As String
    Dim str As String
#End Region

#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

       
            Session("PageName") = Request.Url.ToString()

            CheckSecurity()
            If Not Page.IsPostBack Then
                btnSave.Attributes.Add("onClick", "return ValidateFormCourse();")
                Bindata()
                ' Bind()
                '  BindExcercise()
                objeAAMS.BindDropDown(ddlLevel, "COURSELEVEL", True, 1)
                objeAAMS.BindDropDown(ddlDocument, "DOCUMENT", True, 2)
                BindManuals()

                If Not Request.QueryString("Action") Is Nothing And Not Request.QueryString("CourseID") Is Nothing Then
                    hdCourseId.Value = objED.Decrypt(Request.QueryString("CourseID").ToString().Trim())
                    ViewDetails()
                    'TestQuestionDetails()
                End If
            End If
            HideShowDay(IIf(txtNoOfTest.Text = "", 0, txtNoOfTest.Text))
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region " HideShowDay(ByVal days As Integer)"
    Sub HideShowDay(ByVal days As Integer)
        If chkRequired.Checked = True And chkRequired.Enabled = True Then
            txtNoOfTest.CssClass = "textbox"
            txtNoOfTest.ReadOnly = False
        Else
            txtNoOfTest.CssClass = "textboxgrey"
            txtNoOfTest.ReadOnly = True
        End If

        If days > -1 Then
            trDay1.Attributes.Add("style", "display:none")
            trDay2.Attributes.Add("style", "display:none")
            trDay3.Attributes.Add("style", "display:none")
            trDay4.Attributes.Add("style", "display:none")
            trDay5.Attributes.Add("style", "display:none")
            trDay6.Attributes.Add("style", "display:none")
            trDay7.Attributes.Add("style", "display:none")
            trDay8.Attributes.Add("style", "display:none")
            trDay9.Attributes.Add("style", "display:none")

            'For Day 1
            If days > 0 Then
                trDay1.Attributes.Add("style", "display:block")
            End If
            'For Day 2
            If days > 1 Then
                trDay2.Attributes.Add("style", "display:block")
            End If
            'For Day 3
            If days > 2 Then
                trDay3.Attributes.Add("style", "display:block")
            End If
            'For Day 4
            If days > 3 Then
                trDay4.Attributes.Add("style", "display:block")
            End If
            'For Day 5
            If days > 4 Then
                trDay5.Attributes.Add("style", "display:block")
            End If
            'For Day 6
            If days > 5 Then
                trDay6.Attributes.Add("style", "display:block")
            End If
            'For Day 7
            If days > 6 Then
                trDay7.Attributes.Add("style", "display:block")
            End If
            'For Day 8
            If days > 7 Then
                trDay8.Attributes.Add("style", "display:block")
            End If
            'For Day 9
            If days > 8 Then
                trDay9.Attributes.Add("style", "display:block")
            End If


        End If

    End Sub
#End Region

#Region "Bindata()"
    Sub Bindata()

        Dim TabText As New ArrayList()

        Try

            TabText.Add("Details")

            ' TabText.Add("Exercise")

            theTabStrip.DataSource = TabText

            theTabStrip.DataBind()

        Catch ex As Exception

            lblError.Text = ex.Message.ToString

        End Try

    End Sub
#End Region

#Region "theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound"
    Protected Sub theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound

        Dim Button1 As Button

        Button1 = e.Item.FindControl("Button1")

        If e.Item.ItemIndex = 0 Then

            Button1.CssClass = "headingtab"

        End If

        Button1 = e.Item.FindControl("Button1")

        '  Button1.Attributes.Add("onclick", "return ColorMethod('" & Button1.ClientID.ToString() & "',2);")
        Button1.Attributes.Add("onclick", "return ColorMethodCourse('" & Button1.ClientID.ToString() & "',1);")

    End Sub
#End Region

#Region "BindManuals()"

    Private Sub BindManuals()
        Dim objOutputXml As XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objCourse As New AAMS.bizTraining.bzCourse
            objOutputXml = New XmlDocument
            objOutputXml = objCourse.ListManuals()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvManuals.DataSource = ds.Tables("MANUALS")
                gvManuals.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim objInputXml As New XmlDocument
            Dim objOutputXml As New XmlDocument
            Dim objCourse As New AAMS.bizTraining.bzCourse
            ' Load Input Xml.


            objInputXml.LoadXml("<TR_UPDATECOURSE_INPUT><COURSE TR_COURSE_ID='' TR_COURSE_NAME='' TR_COURSE_DESC='' TR_COURSE_DURATION='' TR_COURSE_NO_TEST='' TR_COURSE_INTERNAL='' TR_COURSE_MAX_MARKS='' DOCUMENTID='' TR_COURSELEVEL_ID='' SHOWONWEB='' TR_TESTREQUIRED='' TR_PRACTICAL_MARKS =''><MANUALS TR_MANUAL_ID=''/><VDAY TR_COURSE_MAX_MARKS='' DAYS='' /></COURSE></TR_UPDATECOURSE_INPUT>")
            If (hdCourseId.Value <> "") Then
                objInputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_COURSE_ID").Value = hdCourseId.Value
            Else
                objInputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_COURSE_ID").Value = ""
            End If

            objInputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_COURSE_NAME").Value = txtCourse.Text.Trim()
            objInputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_COURSE_DESC").Value = txtCourseDescription.Text.Trim()
            objInputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_COURSE_DURATION").Value = txtDuration.Text.Trim()
            If txtNoOfTest.Text = "" Then
                objInputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_COURSE_NO_TEST").Value = Request.Form("txtNoOfTest")
            Else
                objInputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_COURSE_NO_TEST").Value = txtNoOfTest.Text
            End If
            objInputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_COURSE_MAX_MARKS").Value = "" 'txtMarks.Text.Trim()
            objInputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("DOCUMENTID").Value = ddlDocument.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_COURSELEVEL_ID").Value = ddlLevel.SelectedValue

            objInputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_PRACTICAL_MARKS").Value = txtPractMarks.Text
            If chkRequired.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_TESTREQUIRED").Value = "1"
            Else
                objInputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_TESTREQUIRED").Value = "0"

            End If


            If chkInternalCourse.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_COURSE_INTERNAL").Value = "1"
                'Else
                '    objInputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_COURSE_INTERNAL").Value = "0"
            End If

            If chlShowOnWeb.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("SHOWONWEB").Value = "1"
                'Else
                '    objInputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("SHOWONWEB").Value = "0"
            End If

            '   code for manuals start.
            Dim objNode, objCloneNode As XmlNode
            objNode = objInputXml.DocumentElement.SelectSingleNode("COURSE/MANUALS")
            objCloneNode = objNode.CloneNode(True)
            objInputXml.DocumentElement.SelectSingleNode("COURSE").RemoveChild(objNode)

            For i As Integer = 0 To gvManuals.Rows.Count - 1

                If CType(gvManuals.Rows(i).Cells(0).FindControl("chkSelect"), HtmlInputCheckBox).Checked = True Then
                    objCloneNode.Attributes("TR_MANUAL_ID").Value = CType(gvManuals.Rows(i).Cells(0).FindControl("hdDataID"), HiddenField).Value
                    objInputXml.DocumentElement.SelectSingleNode("COURSE").AppendChild(objCloneNode)
                    objCloneNode = objNode.CloneNode(True)
                End If

            Next

            '   code for manuals end.

            ' Code for day wise marks start.
            If chkRequired.Checked = True Then
                Dim objNodeMarks, objCloneNodeMarks As XmlNode
                objNodeMarks = objInputXml.DocumentElement.SelectSingleNode("COURSE/VDAY")
                objCloneNodeMarks = objNodeMarks.CloneNode(True)
                objInputXml.DocumentElement.SelectSingleNode("COURSE").RemoveChild(objNodeMarks)
                Dim intDuration As Integer = 0
                If txtNoOfTest.Text = "" Then
                    If Request.Form("txtNoOfTest") = "" Then
                        lblError.Text = "No of Test is mandatory."
                        Exit Sub
                    Else
                        intDuration = Request.Form("txtNoOfTest")
                    End If

                Else
                    intDuration = txtNoOfTest.Text
                End If

                Dim txt As TextBox
                For i As Integer = 1 To intDuration
                    txt = CType(Page.FindControl("txtDay" + i.ToString), TextBox)
                    If txt IsNot Nothing Then
                        If txt.Text <> "" Then
                            objCloneNodeMarks.Attributes("TR_COURSE_MAX_MARKS").Value = txt.Text
                            objCloneNodeMarks.Attributes("DAYS").Value = i
                            objInputXml.DocumentElement.SelectSingleNode("COURSE").AppendChild(objCloneNodeMarks)
                            objCloneNodeMarks = objNodeMarks.CloneNode(True)
                        Else
                            lblError.Text = "Test " + i.ToString() + " Marks is mandatory."
                            Exit Sub
                        End If
                    End If


                Next

            End If

            ' Code for day wise marks end.




            ' Calling update method for update.
            objOutputXml = objCourse.Update(objInputXml)
            '   Checking error status. 
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If (Request.QueryString("Action").ToString().ToUpper() = "U" Or hdCourseId.Value <> "") Then
                    hdCourseId.Value = objOutputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_COURSE_ID").Value.Trim()
                    lblError.Text = objeAAMSMessage.messUpdate ' Record updated successfully.
                Else
                    hdCourseId.Value = objOutputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_COURSE_ID").Value.Trim()
                    lblError.Text = objeAAMSMessage.messInsert ' Record inserted successfully.

                End If
                CheckSecurity()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

            If chkRequired.Checked = True Then
                Dim intDuration1 As Integer = 0
                If txtNoOfTest.Text = "" Then
                    If Request.Form("txtNoOfTest") <> "" Then
                        intDuration1 = Request.Form("txtNoOfTest")
                    End If
                Else
                    intDuration1 = txtNoOfTest.Text
                End If
                txtNoOfTest.Text = intDuration1.ToString
                HideShowDay(intDuration1)
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click"
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("TRUP_Course.aspx?Action=I")
    End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        If hdCourseId.Value <> "" Then
            ViewDetails()
            ' TestQuestionDetails()
        Else
            Response.Redirect("TRUP_Course.aspx?Action=I")
        End If

    End Sub
#End Region

#Region "TestQuestionDetails()"
    Sub TestQuestionDetails()
        Dim objInPutXml As New XmlDocument
        Dim objOutPutXml As New XmlDocument
        Dim objTrainingRoom As New AAMS.bizTraining.bzTrainingRoom
        objInPutXml.LoadXml("<TR_COURSE_INPUT><TR_COURSE_ID /></TR_COURSE_INPUT>")
        objInPutXml.DocumentElement.SelectSingleNode("TR_COURSE_ID").InnerText = hdCourseId.Value
        objOutPutXml = objTrainingRoom.QuestionSetCount_Course(objInPutXml)
        If objOutPutXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            If Val(objOutPutXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("COURSECOUNT").Value) > 0 Then
                chkRequired.Enabled = False
                txtNoOfTest.ReadOnly = True
                txtNoOfTest.CssClass = "textboxgrey"
                txtDay1.ReadOnly = True
                txtDay2.ReadOnly = True
                txtDay3.ReadOnly = True
                txtDay4.ReadOnly = True
                txtDay5.ReadOnly = True
                txtDay6.ReadOnly = True
                txtDay7.ReadOnly = True
                txtDay8.ReadOnly = True
                txtDay9.ReadOnly = True
                txtDay1.CssClass = "textboxgrey"
                txtDay2.CssClass = "textboxgrey"
                txtDay3.CssClass = "textboxgrey"
                txtDay4.CssClass = "textboxgrey"
                txtDay5.CssClass = "textboxgrey"
                txtDay6.CssClass = "textboxgrey"
                txtDay7.CssClass = "textboxgrey"
                txtDay8.CssClass = "textboxgrey"
                txtDay9.CssClass = "textboxgrey"

            End If
        End If

        'Output :
        '<TR_COURSE__OUTPUT>
        '<DETAILS COURSECOUNT=''/>
        '<Errors Status=''>
        ' <Error Code='' Description=''/>
        '</Errors>
        '</TR_COURSE__OUTPUT>

    End Sub
#End Region

#Region "ViewDetails()"
    Private Sub ViewDetails()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objCourse As New AAMS.bizTraining.bzCourse
        Try
            objInputXml.LoadXml("<TR_VIEWCOURSE_INPUT><TR_COURSE_ID/></TR_VIEWCOURSE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("TR_COURSE_ID").InnerText = hdCourseId.Value ' Request.QueryString("TeamID").ToString().Trim()
            objOutputXml = objCourse.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                txtCourse.Text = objOutputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_COURSE_NAME").Value
                txtDuration.Text = objOutputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_COURSE_DURATION").Value
                txtNoOfTest.Text = objOutputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_COURSE_NO_TEST").Value
                txtCourseDescription.Text = objOutputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_COURSE_DESC").Value
                ' txtMarks.Text = objOutputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_COURSE_MAX_MARKS").Value
                'txtCourse.Text = objOutputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_COURSE_NAME").Value
                Dim li As New ListItem
                li = ddlDocument.Items.FindByValue(objOutputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("DOCUMENTID").Value)
                If li IsNot Nothing Then
                    ddlDocument.SelectedValue = li.Value
                End If
                'ddlDocument.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("DOCUMENTID").Value
                ddlLevel.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_COURSELEVEL_ID").Value

                txtPractMarks.Text = objOutputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_PRACTICAL_MARKS").Value

                If objOutputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("SHOWONWEB").Value = "1" Then
                    chlShowOnWeb.Checked = True
                Else
                    chlShowOnWeb.Checked = False
                End If

                If objOutputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_COURSE_INTERNAL").Value = "1" Then
                    chkInternalCourse.Checked = True
                Else
                    chkInternalCourse.Checked = False
                End If
                ' test required
                If objOutputXml.DocumentElement.SelectSingleNode("COURSE").Attributes("TR_TESTREQUIRED").Value = "1" Then
                    chkRequired.Checked = True

                    ' Setting Daywise Marks start
                    Dim objNodeListMarks As XmlNodeList
                    Dim objNodeMarks As XmlNode
                    '  Dim strDay As String = ""
                    Dim txt As TextBox
                    objNodeListMarks = objOutputXml.DocumentElement.SelectNodes("COURSE/VDAY")
                    For Each objNodeMarks In objNodeListMarks
                        ' strDay = objNodeMarks.Attributes("DAY").Value.Trim()
                        For i As Integer = 1 To objNodeListMarks.Count
                            txt = CType(Page.FindControl("txtDay" + objNodeMarks.Attributes("DAYS").Value), TextBox)
                            If txt IsNot Nothing Then
                                txt.Text = objNodeMarks.Attributes("TR_COURSE_MAX_MARKS").Value
                            End If
                        Next
                    Next

                    ' Set Daywise Marks end
                Else
                    chkRequired.Checked = False
                End If


                ' set grid checkbox as per DB values 
                Dim objNodeList As XmlNodeList
                Dim objNodeManual As XmlNode
                Dim strId As String = ""
                objNodeList = objOutputXml.DocumentElement.SelectNodes("COURSE/MANUALS")

                For Each objNodeManual In objNodeList
                    strId = objNodeManual.Attributes("TR_MANUAL_ID").Value.Trim()
                    For i As Integer = 0 To gvManuals.Rows.Count - 1
                        If CType(gvManuals.Rows(i).Cells(0).FindControl("hdDataID"), HiddenField).Value = strId Then
                            CType(gvManuals.Rows(i).Cells(0).FindControl("chkSelect"), HtmlInputCheckBox).Checked = True
                        End If
                    Next
                Next




            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
#End Region

#Region "Bind()"
    'Private Sub Bind()
    '    Dim objDT As New DataTable
    '    Dim objCol1 As New DataColumn("ManualID")
    '    Dim objCol2 As New DataColumn("Manual")
    '    Dim objRow As DataRow
    '    objDT.Columns.Add(objCol1)
    '    objDT.Columns.Add(objCol2)
    '    objRow = objDT.NewRow
    '    objRow(0) = "1"
    '    objRow(1) = "Amadeus Email HandOut"
    '    objDT.Rows.Add(objRow)
    '    objRow = objDT.NewRow
    '    objRow(0) = "2"
    '    objRow(1) = "Soft Skill HandOut"
    '    objDT.Rows.Add(objRow)
    '    gvManuals.DataSource = objDT
    '    gvManuals.DataBind()

    'End Sub
#End Region

#Region "BindExercise"
    'Private Sub BindExcercise()
    '    Dim objDT As New DataTable
    '    Dim objCol1 As New DataColumn("ExerciseID")
    '    Dim objCol2 As New DataColumn("ExerciseTitle")
    '    Dim objRow As DataRow
    '    objDT.Columns.Add(objCol1)
    '    objDT.Columns.Add(objCol2)
    '    objRow = objDT.NewRow
    '    objRow(0) = "1"
    '    objRow(1) = "Enter the world of Amadeus"
    '    objDT.Rows.Add(objRow)
    '    objRow = objDT.NewRow
    '    objRow(0) = "2"
    '    objRow(1) = "Enter the world of automated ticketing"
    '    objDT.Rows.Add(objRow)
    '    gvExercise.DataSource = objDT
    '    gvExercise.DataBind()

    'End Sub
#End Region

#Region "CheckSecurity()::This method is used for security check."
    Private Sub CheckSecurity()
        Try
            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Course']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Course']").Attributes("Value").Value)
                End If

                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                End If

                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    btnSave.Enabled = False
                End If
                If strBuilder(2) = "0" And (hdCourseId.Value <> "" Or Request.QueryString("CourseID") IsNot Nothing) Then
                    btnSave.Enabled = False
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
#Region "Page Filter()"
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
#End Region
End Class
