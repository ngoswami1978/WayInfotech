
Partial Class Training_MSUP_ActiveDayWiseTest
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Protected strIndex As String
    Protected flagPermission As String
    Dim objED As New EncyrptDeCyrpt
    Dim str As String
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

        Try

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            CheckSecurity()
            'Dim strBuilder As New StringBuilder
            'Dim objSecurityXml As New XmlDocument
            'objSecurityXml.LoadXml(Session("Security"))
            'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Course Session']").Count <> 0 Then
            '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Course Session']").Attributes("Value").Value)
            '    End If
            '    If strBuilder(0) = "0" Then
            '        btnSave.Enabled = False
            '    End If
            '    If strBuilder(1) = "0" Then
            '        '   btnNew.Enabled = False
            '    End If
            'Else
            '    strBuilder = objeAAMS.SecurityCheck(31)
            'End If


            If Not Page.IsPostBack Then
                Bindata()
                If Not Request.QueryString("Duration") Is Nothing Then
                    hdDuration.Value = Request.QueryString("Duration").ToString
                End If
                If Not Request.QueryString("NoOfTest") Is Nothing Then
                    hdNoOfTest.Value = Request.QueryString("NoOfTest").ToString
                End If
                If Not Request.QueryString("Action") Is Nothing Then
                    hdPageStatus.Value = Request.QueryString("Action").ToString
                End If
                If Not Request.QueryString("CourseSessionID") Is Nothing Then
                    hdEnPageCourseSessionID.Value = Request.QueryString("CourseSessionID").ToString
                    hdPageCourseSessionID.Value = objED.Decrypt(Request.QueryString("CourseSessionID").ToString)
                End If
                If hdPageCourseSessionID.Value <> "" Then
                    ViewRecords()
                    '  ViewData()
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Private Sub CheckSecurity()
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If
        Dim strBuilder As New StringBuilder
        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Activate Test']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Activate Test']").Attributes("Value").Value)
            End If
            If strBuilder(0) = "0" Then
                Response.Redirect("../NoRights.aspx")
                btnSave.Enabled = False
            End If

            If strBuilder(1) = "0" Then
                btnSave.Enabled = False
            End If
            If strBuilder(2) = "0" And (hdPageCourseSessionID.Value <> "" Or Request.QueryString("CourseSessionID") IsNot Nothing) Then
                btnSave.Enabled = False
            End If

            If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                btnSave.Enabled = True
            End If
        Else
            strBuilder = objeAAMS.SecurityCheck(31)
        End If

    End Sub
    Sub Bindata()
        Dim TabText As New ArrayList()
        Try
            TabText.Add("Session")
            TabText.Add("Register")
            TabText.Add("Activate")
            theTabStrip.DataSource = TabText
            theTabStrip.DataBind()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound
        Dim Button1 As Button
        Button1 = e.Item.FindControl("Button1")
        If e.Item.ItemIndex = 0 Then
            Button1.CssClass = "headingtab"
        End If
        If Session("Security") IsNot Nothing Then
            Dim strButtonText As String
            strButtonText = Button1.Text
            Select Case strButtonText
                Case "Session"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Course Session") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Register"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Register Participant") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Activate"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Activate Test") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
            End Select
        End If
        Button1 = e.Item.FindControl("Button1")
        Button1.Attributes.Add("onclick", "return ColorMethodDayWiseTest('" & Button1.ClientID.ToString() & "',3);")

    End Sub


    Sub ViewRecords()

        '       <TR_VIEWPARTICIPANT_TEST_CONFIG_INPUT> 
        '    <TR_COURSES_ID/>
        '</TR_VIEWPARTICIPANT_TEST_CONFIG_INPUT>

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzParticipant As New AAMS.bizTraining.bzTraining
        objInputXml.LoadXml("<TR_VIEWPARTICIPANT_TEST_CONFIG_INPUT> <TR_COURSES_ID /><DURATION/></TR_VIEWPARTICIPANT_TEST_CONFIG_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("TR_COURSES_ID").InnerText = hdPageCourseSessionID.Value
        objInputXml.DocumentElement.SelectSingleNode("DURATION").InnerText = hdNoOfTest.Value
        objOutputXml = objbzParticipant.ViewDayWiseConfig(objInputXml)
        'objOutputXml.Load("c:/test.xml")
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            hdData.Value = objOutputXml.OuterXml
            Dim objNodeReader As XmlNodeReader
            Dim ds As New DataSet
            objNodeReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objNodeReader)
            gvParticipantTab.DataSource = ds.Tables("DETAILS")
            gvParticipantTab.DataBind()
            Dim intDuration As Integer
            intDuration = CInt(objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("DURATION").Value)
            For i As Integer = 1 To 9
                If intDuration >= i Then
                    gvParticipantTab.Columns(i).Visible = True
                Else
                    gvParticipantTab.Columns(i).Visible = False
                End If
            Next
            'FillGrid()
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If

    End Sub


    Sub BindXml()
        Dim objOutputXml As New XmlDocument
        If gvParticipantTab.Rows.Count > 0 Then
            Dim objNodeList As XmlNodeList
            Dim objNode As XmlNode
            objOutputXml.LoadXml(hdData.Value)
            objNodeList = objOutputXml.DocumentElement.SelectNodes("DETAILS")
            Dim intDuration As Integer
            intDuration = CInt(objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("DURATION").Value)
            For i As Integer = 0 To objNodeList.Count - 1
                '<DETAILS TR_COURSES_ID TR_COURSEP_ID PARTICIPANTNAME= DAY1 = DAY2 = DAY3 DAY4 =DURATION ='''        FEEDBACK='0/1'
                objNode = objNodeList(i)
                objNode.Attributes("PARTICIPANTNAME").Value = gvParticipantTab.Rows(i).Cells(0).Text
                objNode.Attributes("TR_COURSEP_ID").Value = CType(gvParticipantTab.Rows(i).FindControl("hdTR_COURSEP_ID"), HiddenField).Value
                For j As Integer = 1 To 9
                    If intDuration >= j Then
                        objNode.Attributes("DAY" & j).Value = IIf(CType(gvParticipantTab.Rows(i).Cells(1).FindControl("chkDay" & j), CheckBox).Checked = True, "1", "0")
                    End If
                Next
                objNode.Attributes("FEEDBACK").Value = IIf(CType(gvParticipantTab.Rows(i).Cells(10).FindControl("chkFeedBack"), CheckBox).Checked = True, "1", "0")
            Next
        End If
        ' objOutputXml.DocumentElement.RemoveChild(objOutputXml.DocumentElement.SelectSingleNode("Errors"))
        hdData.Value = objOutputXml.OuterXml
    End Sub


    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If hdData.Value <> "" Then
            If gvParticipantTab.Rows.Count Then
                BindXml()
            End If
            Dim objInputXml, objOutputXml As New XmlDocument

            Dim objbzTraining As New AAMS.bizTraining.bzTraining
            Try
                objInputXml.LoadXml(hdData.Value)
                'Here Back end Method Call
                objOutputXml = objbzTraining.UpdateDaywiseConfiguration(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    lblError.Text = "FeedBack / Test Activated."
                    hdData.Value = ""
                    ViewRecords()
                    CheckSecurity()
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            Finally

            End Try
        End If
    End Sub


    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If hdPageCourseSessionID.Value <> "" Then
                hdData.Value = ""
                ViewRecords()
            Else
                Dim strQueryString As String = Request.QueryString.ToString
                Response.Redirect("TRUP_CourseSession.aspx?" + strQueryString)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub


    Protected Sub gvParticipantTab_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvParticipantTab.RowDataBound
        'TR_COURSES_ID='' 'TR_COURSEP_ID='' ''DAY1 = '''DAY2 = '''DAY3 = '''DAY4 = '''DAY5 = '''DAY6 = '''DAY7 = '''DAY8 = '''DAY9 = '''DURATION ='''FEEDBACK=''
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
        Dim strday1 As String = DataBinder.Eval(e.Row.DataItem, "DAY1")
        Dim strday2 As String = DataBinder.Eval(e.Row.DataItem, "DAY2")
        Dim strday3 As String = DataBinder.Eval(e.Row.DataItem, "DAY3")
        Dim strday4 As String = DataBinder.Eval(e.Row.DataItem, "DAY4")
        Dim strday5 As String = DataBinder.Eval(e.Row.DataItem, "DAY5")
        Dim strday6 As String = DataBinder.Eval(e.Row.DataItem, "DAY6")
        Dim strday7 As String = DataBinder.Eval(e.Row.DataItem, "DAY7")
        Dim strday8 As String = DataBinder.Eval(e.Row.DataItem, "DAY8")
        Dim strday9 As String = DataBinder.Eval(e.Row.DataItem, "DAY9")
        Dim strParticipantId As String = objED.Encrypt(DataBinder.Eval(e.Row.DataItem, "TR_COURSEP_ID"))
        Dim strFeedback As String = DataBinder.Eval(e.Row.DataItem, "FEEDBACK")
        CType(e.Row.Cells(1).Controls(3), LinkButton).Attributes.Add("onclick", "return PopupPageDayWiseTest(1,'" + strParticipantId + "',1)")
        CType(e.Row.Cells(2).Controls(3), LinkButton).Attributes.Add("onclick", "return PopupPageDayWiseTest(1,'" + strParticipantId + "',2)")
        CType(e.Row.Cells(3).Controls(3), LinkButton).Attributes.Add("onclick", "return PopupPageDayWiseTest(1,'" + strParticipantId + "',3)")
        CType(e.Row.Cells(4).Controls(3), LinkButton).Attributes.Add("onclick", "return PopupPageDayWiseTest(1,'" + strParticipantId + "',4)")
        CType(e.Row.Cells(5).Controls(3), LinkButton).Attributes.Add("onclick", "return PopupPageDayWiseTest(1,'" + strParticipantId + "',5)")
        CType(e.Row.Cells(6).Controls(3), LinkButton).Attributes.Add("onclick", "return PopupPageDayWiseTest(1,'" + strParticipantId + "',6)")
        CType(e.Row.Cells(7).Controls(3), LinkButton).Attributes.Add("onclick", "return PopupPageDayWiseTest(1,'" + strParticipantId + "',7)")
        CType(e.Row.Cells(8).Controls(3), LinkButton).Attributes.Add("onclick", "return PopupPageDayWiseTest(1,'" + strParticipantId + "',8)")
        CType(e.Row.Cells(9).Controls(3), LinkButton).Attributes.Add("onclick", "return PopupPageDayWiseTest(1,'" + strParticipantId + "',9)")
        CType(e.Row.Cells(10).Controls(3), LinkButton).Attributes.Add("onclick", "return PopupPageDayWiseTest(2,'" + strParticipantId + "',10)")

        Select Case strday1
            Case "0"
                CType(e.Row.Cells(1).Controls(1), CheckBox).Checked = False
            Case "1"
                CType(e.Row.Cells(1).Controls(1), CheckBox).Checked = True
        End Select

        Select Case strday2
            Case "0"
                CType(e.Row.Cells(2).Controls(1), CheckBox).Checked = False
            Case "1"
                CType(e.Row.Cells(2).Controls(1), CheckBox).Checked = True
        End Select

        Select Case strday3
            Case "0"
                CType(e.Row.Cells(3).Controls(1), CheckBox).Checked = False
            Case "1"
                CType(e.Row.Cells(3).Controls(1), CheckBox).Checked = True
        End Select

        Select Case strday4
            Case "0"
                CType(e.Row.Cells(4).Controls(1), CheckBox).Checked = False
            Case "1"
                CType(e.Row.Cells(4).Controls(1), CheckBox).Checked = True
        End Select

        Select Case strday5
            Case "0"
                CType(e.Row.Cells(5).Controls(1), CheckBox).Checked = False
            Case "1"
                CType(e.Row.Cells(5).Controls(1), CheckBox).Checked = True
        End Select

        Select Case strday6
            Case "0"
                CType(e.Row.Cells(6).Controls(1), CheckBox).Checked = False
            Case "1"
                CType(e.Row.Cells(6).Controls(1), CheckBox).Checked = True
        End Select

        Select Case strday7
            Case "0"
                CType(e.Row.Cells(7).Controls(1), CheckBox).Checked = False
            Case "1"
                CType(e.Row.Cells(7).Controls(1), CheckBox).Checked = True
        End Select

        Select Case strday8
            Case "0"
                CType(e.Row.Cells(8).Controls(1), CheckBox).Checked = False
            Case "1"
                CType(e.Row.Cells(8).Controls(1), CheckBox).Checked = True
        End Select
        Select Case strday9
            Case "0"
                CType(e.Row.Cells(9).Controls(1), CheckBox).Checked = False
            Case "1"
                CType(e.Row.Cells(9).Controls(1), CheckBox).Checked = True
        End Select

        Select Case strFeedback
            Case ""
                e.Row.Cells(10).Visible = False
            Case "0"
                CType(e.Row.Cells(10).Controls(1), CheckBox).Checked = False
            Case "1"
                CType(e.Row.Cells(10).Controls(1), CheckBox).Checked = True
        End Select


    End Sub

    
End Class
