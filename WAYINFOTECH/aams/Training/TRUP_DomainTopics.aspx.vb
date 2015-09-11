
Partial Class Training_TRUP_DomainTopics
    Inherits System.Web.UI.Page

#Region "Global Variable Declaration"
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
	Dim objeAAMSMessage As New eAAMSMessage
	Public strBuilder As New StringBuilder
	Dim objxmldocfrag As XmlDocumentFragment
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
#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		Try
            ' Checking security.
            CheckSecurity()
            If Not IsPostBack Then
                btnSave.Attributes.Add("onClick", "return ValidateForm();")
                btnAdd.Attributes.Add("onClick", "return ValidateTopics();")
                Dim strurl As String = Request.Url.ToString()
                Session("AddTopic") = Nothing
                Session("PageName") = strurl
                FillCourse()
                FillDomain()
                'objeAAMS.BindDropDown(ddlDomain, "DOMAIN", True, 1)
                btnSave.Attributes.Add("onClick", "return ValidateForm();")
                'btnReset.Attributes.Add("onClick", "return ClearControls();")
                ' Checking Query String for update .
                If Not Request.QueryString("Action") = Nothing And Not Request.QueryString("CourseId") = Nothing Then
                    hdID.Value = objED.Decrypt(Request.QueryString("CourseId").ToString().Trim())
                    ViewDetails()
                End If

            Else
                'txtCourseTitle.Text = Request.Form(txtCourseTitle.ClientID)
                If hdnCopyCourseId.Value <> String.Empty Then
                    AppendToMain(hdnCopyCourseId.Value)
                    hdnCopyCourseId.Value = String.Empty
                End If
            End If


		Catch ex As Exception
			lblError.Text = ex.Message
		End Try
	End Sub
#End Region
    Sub FillCourse()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objCourse As New AAMS.bizTraining.bzCourse
        ddlCourseTitle.Items.Clear()
        Try
            objInputXml.LoadXml("<TR_SEARCH_COURSE_INPUT><TR_COURSE_NAME /><TR_COURSELEVEL_ID /><ShowOnWeb /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></TR_SEARCH_COURSE_INPUT>")
            objOutputXml = objCourse.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                For Each objnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("COURSE")
                    Dim li As New ListItem(objnode.Attributes("TR_COURSE_NAME").Value, objnode.Attributes("TR_COURSE_ID").Value)
                    ddlCourseTitle.Items.Add(li)
                Next


                'objXmlReader = New XmlNodeReader(objOutputXml)
                'ds.ReadXml(objXmlReader)
                'If ds.Tables("COURSE").Rows.Count <> 0 Then
                '    ddlCourseTitle.DataSource = ds.Tables("COURSE")
                '    ddlCourseTitle.DataTextField = ""
                '    ddlCourseTitle.DataValueField = ""
                '    ddlCourseTitle.DataBind()
                'Else
                '    ddlCourseTitle.DataSource = Nothing
                '    ddlCourseTitle.DataBind()
                'End If

            Else

                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            ddlCourseTitle.Items.Insert(0, New ListItem("--Select One--", ""))
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub

    Sub FillDomain()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzDomain As New AAMS.bizTraining.bzDomain
        ddlDomain.Items.Clear()
        Try
            objInputXml.LoadXml("<TR_SEARCH_DOMAIN_INPUT><TR_VALTOPICDOM_NAME></TR_VALTOPICDOM_NAME><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></TR_SEARCH_DOMAIN_INPUT>")
            objOutputXml = objbzDomain.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                For Each objnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("DOMAIN")
                    Dim li As New ListItem(objnode.Attributes("TR_VALTOPICDOM_NAME").Value, objnode.Attributes("TR_VALTOPICDOM_ID").Value & "|" & objnode.Attributes("TR_DOMAIN_ORDER").Value)
                    ddlDomain.Items.Add(li)
                Next
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            ddlDomain.Items.Insert(0, New ListItem("--Select One--", ""))
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
#Region "btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click"
	Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
		Try
			Dim objInputXml As New XmlDocument
			Dim objOutputXml As New XmlDocument
            Dim objbzTopic As New AAMS.bizTraining.bzTopic
            Dim arlCourseId As New ArrayList
            If gvTopic.Rows.Count = 0 Then
                lblError.Text = "Please add topics."
                Exit Sub
            End If
			' Load Input Xml.
			objInputXml.LoadXml(Session("AddTopic"))

            For Each objNode As XmlNode In objInputXml.DocumentElement.SelectNodes("TOPIC")
                If arlCourseId.Contains(objNode.Attributes("TR_COURSE_ID").Value) Then
                Else
                    arlCourseId.Add(objNode.Attributes("TR_COURSE_ID").Value)
                End If
            Next
            If arlCourseId.Count > 1 Then
                lblError.Text = "You have selected multiple courses"
                Exit Sub
            End If



            ' Calling update method for update.
            objOutputXml = objbzTopic.Update(objInputXml)
            '   Checking error status. 
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If (hdID.Value <> "") Then
                    'hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("TOPIC").Attributes("TempRowID").Value.Trim()
                    'hdID.Value = hdCourseID.Value
                    hdID.Value = ddlCourseTitle.SelectedValue.Split("|").GetValue(0)
                    lblError.Text = objeAAMSMessage.messUpdate ' Record updated successfully.
                    ViewDetails()
                Else
                    'hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("TOPIC").Attributes("TempRowID").Value.Trim()
                    hdID.Value = ddlCourseTitle.SelectedValue.Split("|").GetValue(0)
                    lblError.Text = objeAAMSMessage.messInsert ' Record inserted successfully.
                    ViewDetails()
                End If
                ' Checking security.
                CheckSecurity()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
	End Sub
#End Region

#Region "btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click"
	Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
		Try
			Response.Redirect("TRUP_DomainTopics.aspx?Action=I")
		Catch ex As Exception
			lblError.Text = ex.Message
		End Try
	End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
	Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
		Try
			If hdID.Value = "" Then
                ddlCourseTitle.SelectedValue = ""
                lblError.Text = ""
                ddlDomain.SelectedValue = ""
                txtTopicNo.Text = ""
                txtTopics.Text = ""
			Else
				' Calling View Method.
                ' If Not Request.QueryString("ContactID") Is Nothing Then
                lblError.Text = ""
                ViewDetails()
                ddlDomain.SelectedValue = ""
                txtTopicNo.Text = ""
                txtTopics.Text = ""
				'End If
			End If
		Catch ex As Exception
			lblError.Text = ex.Message
		End Try
	End Sub
#End Region

#Region "ViewDetails()"
	Private Sub ViewDetails()
		Dim objInputXml, objOutputXml As New XmlDocument
		Dim objbzTopic As New AAMS.bizTraining.bzTopic
		Try
            objInputXml.LoadXml("<TR_VIEW_TOPIC_INPUT><TOPIC TR_COURSE_ID=''/></TR_VIEW_TOPIC_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("TOPIC").Attributes("TR_COURSE_ID").InnerText = hdID.Value
			objOutputXml = objbzTopic.View(objInputXml)
			If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                '  BindXml(objOutputXml)
                FillValues(objOutputXml)
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

    Sub BindXml(ByRef objXml As XmlDocument)
        '<TOPIC TR_CVALTOPIC_ID="41"
        Dim intcn As Integer = 1
        For Each objNode As XmlNode In objXml.DocumentElement.SelectNodes("TOPIC")
            objNode.Attributes("TempRowID").Value = intcn.ToString
            intcn = intcn + 1
        Next
    End Sub

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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Feedback Topic']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Feedback Topic']").Attributes("Value").Value)
                End If

                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                End If

                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    btnSave.Enabled = False
                End If
                If strBuilder(2) = "0" And (hdID.Value <> "" Or Request.QueryString("CourseId") IsNot Nothing) Then
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

#Region "btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click"
	Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        ' If ViewState("TR_CVALTOPIC_ID") Is Nothing Then
        AddToGrid()
        '  Else
        '  UpdateToGrid()
        '  End If



	End Sub
#End Region

#Region "FillEditValues"
	Protected Sub FillEditValues(ByVal strID As String)
		Dim objX As New XmlDocument
		Dim xnode As XmlNode
		objX.LoadXml(Session("AddTopic"))
		xnode = objX.DocumentElement.SelectSingleNode("TOPIC[@TR_CVALTOPIC_ID='" & strID & "']")
		If Not xnode Is Nothing Then
			With xnode
                ddlDomain.SelectedValue = (.Attributes("TR_VALTOPICDOM_ID").Value & "|" & .Attributes("TR_DOMAIN_ORDER").Value)
                txtTopics.Text = .Attributes("TR_TOPICS").Value
                txtTopicNo.Text = .Attributes("TR_CVALTOPICS_ORDER").Value
			End With
            ViewState("TempRowID") = xnode.Attributes("TempRowID").Value ' strID
		End If

	End Sub
#End Region

#Region "DeleteItem"
	Protected Function DeleteItem(ByVal strID As String) As Boolean
		Dim objX As New XmlDocument
		Dim xnode As XmlNode
		If Not Session("AddTopic") Is Nothing Then
			objX.LoadXml(Session("AddTopic"))
            xnode = objX.DocumentElement.SelectSingleNode("TOPIC[@TempRowID='" & strID & "']")
            If Not xnode Is Nothing Then
                If xnode.Attributes("TR_CVALTOPIC_ID").Value = "" Then
                    objX.DocumentElement.RemoveChild(xnode)
                    Session("AddTopic") = objX.OuterXml
                    If objX.DocumentElement.SelectNodes("TOPIC").Count = 0 Then
                        Session("AddTopic") = Nothing
                    End If
                Else
                    xnode.Attributes("ACTION").Value = "D"
                    Session("AddTopic") = objX.OuterXml
                End If
                
                showGrid(objX)
            End If
        End If
	End Function
#End Region

#Region "AddToGrid"
    'Protected Sub UpdateToGrid()
    '    Dim xMain As New XmlDocument
    '    Dim strInput As String = "<TR_UPDATE_TOPIC_INPUT><TOPIC TR_CVALTOPIC_ID=''  TR_VALTOPICDOM_ID='' TR_VALTOPICDOM_NAME='' TR_TOPICS='' TR_COURSE_ID='' TR_COURSE_NAME='' TR_CVALTOPICS_ORDER=''/></TR_UPDATE_TOPIC_INPUT>"
    '    Dim xDoc As New XmlDocument
    '    Dim xnode As XmlNode
    '    xDoc.LoadXml(strInput)
    '    If Session("AddTopic") Is Nothing Then
    '        xMain.LoadXml("<TR_UPDATE_TOPIC_INPUT></TR_UPDATE_TOPIC_INPUT>")
    '    Else
    '        xMain.LoadXml(Session("AddTopic"))
    '    End If

    '    With xDoc.DocumentElement.SelectSingleNode("TOPIC")
    '        If ViewState("TR_CVALTOPIC_ID") Is Nothing Then
    '            If Not Session("AddTopic") Is Nothing Then
    '                xMain.LoadXml(Session("AddTopic"))
    '                ' start Checking duplicate topics before adding it  .
    '                Dim objDupNode As XmlNode
    '                objDupNode = xMain.DocumentElement.SelectSingleNode("TOPIC[@TR_VALTOPICDOM_ID='" + ddlDomain.SelectedValue + "'][@TR_TOPICS='" + txtTopics.Text + "']")
    '                If objDupNode IsNot Nothing Then
    '                    lblError.Text = "Topic already added with this domain."
    '                    Exit Sub
    '                End If
    '                ' end Checking duplicate topics before adding it  .
    '                If xMain.DocumentElement.SelectNodes("TOPIC").Count > 0 Then
    '                    .Attributes("TR_CVALTOPIC_ID").Value = CInt(xMain.DocumentElement.SelectNodes("TOPIC").Item(xMain.DocumentElement.SelectNodes("TOPIC").Count - 1).Attributes("TR_CVALTOPIC_ID").Value) + 1
    '                Else
    '                    .Attributes("TR_CVALTOPIC_ID").Value = xMain.DocumentElement.SelectNodes("TOPIC").Count + 1
    '                End If
    '            Else
    '                .Attributes("TR_CVALTOPIC_ID").Value = "1"
    '            End If
    '        Else
    '            If Not Session("AddTopic") Is Nothing Then
    '                xMain.LoadXml(Session("AddTopic"))

    '                ' start Checking duplicate topics before adding it  .
    '                Dim objDupNode As XmlNode
    '                objDupNode = xMain.DocumentElement.SelectSingleNode("TOPIC[@TR_VALTOPICDOM_ID='" + ddlDomain.SelectedValue + "'][@TR_TOPICS='" + txtTopics.Text + "'][@TR_CVALTOPIC_ID != '" + ViewState("TR_CVALTOPIC_ID") + "']")
    '                If objDupNode IsNot Nothing Then
    '                    lblError.Text = "Topic already added with this domain."
    '                    Exit Sub
    '                End If
    '                ' end Checking duplicate topics before adding it  .

    '                xnode = xMain.DocumentElement.SelectSingleNode("TOPIC[@TR_CVALTOPIC_ID='" & ViewState("TR_CVALTOPIC_ID") & "']")
    '                If Not xnode Is Nothing Then
    '                    .Attributes("TR_CVALTOPIC_ID").Value = xnode.Attributes("TR_CVALTOPIC_ID").Value
    '                End If
    '            End If
    '        End If
    '        .Attributes("TR_VALTOPICDOM_ID").Value = ddlDomain.SelectedValue
    '        .Attributes("TR_VALTOPICDOM_NAME").Value = ddlDomain.SelectedItem.Text
    '        .Attributes("TR_TOPICS").Value = txtTopics.Text
    '        .Attributes("TR_COURSE_ID").Value = hdCourseID.Value
    '        .Attributes("TR_COURSE_NAME").Value = txtCourseTitle.Text
    '    End With

    '    If Not ViewState("TR_CVALTOPIC_ID") Is Nothing And ViewState("TR_CVALTOPIC_ID") <> String.Empty Then
    '        Dim objXMLNode As XmlNode
    '        objXMLNode = xMain.DocumentElement.SelectSingleNode("TOPIC[@TR_CVALTOPIC_ID='" & ViewState("TR_CVALTOPIC_ID").ToString & "']")
    '        xMain.DocumentElement.RemoveChild(objXMLNode)
    '        ViewState("TR_CVALTOPIC_ID") = Nothing
    '    End If

    '    objxmldocfrag = xMain.CreateDocumentFragment()
    '    objxmldocfrag.InnerXml = xDoc.DocumentElement.SelectSingleNode("TOPIC").OuterXml
    '    xMain.DocumentElement.AppendChild(objxmldocfrag)
    '    Session("AddTopic") = xMain.OuterXml
    '    showGrid(xMain)
    '    'ddlDomain.SelectedIndex = 0
    '    txtTopics.Text = String.Empty
    'End Sub
    Protected Sub AddToGrid()
	Try
        Dim xMain As New XmlDocument
        Dim strInput As String = "<TR_UPDATE_TOPIC_INPUT><TOPIC TR_CVALTOPIC_ID='' ACTION='' TempRowID=''  TR_VALTOPICDOM_ID='' TR_VALTOPICDOM_NAME='' TR_DOMAIN_ORDER='' TR_TOPICS='' TR_COURSE_ID='' TR_COURSE_NAME='' TR_CVALTOPICS_ORDER=''/></TR_UPDATE_TOPIC_INPUT>"
        Dim xDoc As New XmlDocument
        Dim xnode As XmlNode
        xDoc.LoadXml(strInput)
        If Session("AddTopic") Is Nothing Then
            xMain.LoadXml("<TR_UPDATE_TOPIC_INPUT></TR_UPDATE_TOPIC_INPUT>")
        Else
            xMain.LoadXml(Session("AddTopic"))
        End If

        With xDoc.DocumentElement.SelectSingleNode("TOPIC")
            If ViewState("TempRowID") Is Nothing Then
                If Not Session("AddTopic") Is Nothing Then
                    xMain.LoadXml(Session("AddTopic"))
                    ' start Checking duplicate topics before adding it  .
                    Dim objDupNode As XmlNode
                    objDupNode = xMain.DocumentElement.SelectSingleNode("TOPIC[@TR_VALTOPICDOM_ID='" + ddlDomain.SelectedValue.Split("|").GetValue(0) + "'][@TR_TOPICS='" + txtTopics.Text + "'][@ACTION != 'D']")
                    If objDupNode IsNot Nothing Then
                        lblError.Text = "Topic already added with this domain."
                        Exit Sub
                    End If
                    ' end Checking duplicate topics before adding it  .
                    If xMain.DocumentElement.SelectNodes("TOPIC").Count > 0 Then
                        .Attributes("TempRowID").Value = CInt(xMain.DocumentElement.SelectNodes("TOPIC").Item(xMain.DocumentElement.SelectNodes("TOPIC").Count - 1).Attributes("TempRowID").Value) + 1
                    Else
                        .Attributes("TempRowID").Value = xMain.DocumentElement.SelectNodes("TOPIC").Count + 1
                    End If
                Else
                    .Attributes("TempRowID").Value = "1"
                End If
            Else
                If Not Session("AddTopic") Is Nothing Then
                    xMain.LoadXml(Session("AddTopic"))

                    ' start Checking duplicate topics before adding it  .
                    Dim objDupNode As XmlNode
                    objDupNode = xMain.DocumentElement.SelectSingleNode("TOPIC[@TR_VALTOPICDOM_ID='" + ddlDomain.SelectedValue.Split("|").GetValue(0) + "'][@TR_TOPICS='" + txtTopics.Text + "'][@TempRowID != '" + ViewState("TempRowID") + "'][@ACTION != 'D']")
                    If objDupNode IsNot Nothing Then
                        lblError.Text = "Topic already added with this domain."
                        Exit Sub
                    End If
                    ' end Checking duplicate topics before adding it  .

                    xnode = xMain.DocumentElement.SelectSingleNode("TOPIC[@TempRowID='" & ViewState("TempRowID") & "']")
                    If Not xnode Is Nothing Then
                        .Attributes("TempRowID").Value = xnode.Attributes("TempRowID").Value
                    End If
                End If
            End If
            .Attributes("TR_VALTOPICDOM_ID").Value = ddlDomain.SelectedValue.Split("|").GetValue(0)
            .Attributes("TR_DOMAIN_ORDER").Value = ddlDomain.SelectedValue.Split("|").GetValue(1)
            .Attributes("TR_VALTOPICDOM_NAME").Value = ddlDomain.SelectedItem.Text
            .Attributes("TR_TOPICS").Value = txtTopics.Text
            '.Attributes("TR_COURSE_ID").Value = hdCourseID.Value
            .Attributes("TR_COURSE_ID").Value = ddlCourseTitle.SelectedValue
            '.Attributes("TR_COURSE_NAME").Value = txtCourseTitle.Text
            .Attributes("TR_COURSE_NAME").Value = ddlCourseTitle.SelectedItem.Text
            .Attributes("TR_CVALTOPICS_ORDER").Value = txtTopicNo.Text



            If Not ViewState("TempRowID") Is Nothing And ViewState("TempRowID") <> String.Empty Then
                Dim objXMLNode As XmlNode
                objXMLNode = xMain.DocumentElement.SelectSingleNode("TOPIC[@TempRowID='" & ViewState("TempRowID").ToString & "']")
                .Attributes("TR_CVALTOPIC_ID").Value = objXMLNode.Attributes("TR_CVALTOPIC_ID").Value
                xMain.DocumentElement.RemoveChild(objXMLNode)
                ViewState("TempRowID") = Nothing
            End If

        End With
        objxmldocfrag = xMain.CreateDocumentFragment()
        objxmldocfrag.InnerXml = xDoc.DocumentElement.SelectSingleNode("TOPIC").OuterXml
        xMain.DocumentElement.AppendChild(objxmldocfrag)
        Session("AddTopic") = xMain.OuterXml
        showGrid(xMain)
        'ddlDomain.SelectedIndex = 0
        txtTopics.Text = String.Empty
        txtTopicNo.Text = ""
     Catch ex As Exception
			lblError.Text = ex.Message.ToString
	End Try
    End Sub
#End Region

#Region "showGrid"
	Private Sub showGrid(ByVal xmlDoc As XmlDocument)
        If xmlDoc.DocumentElement.SelectNodes("TOPIC").Count > 0 Then
            Dim ds As New DataSet
            Dim objXmlReader As XmlReader
            objXmlReader = New XmlNodeReader(xmlDoc)
            ds.ReadXml(objXmlReader)
            Dim dv As DataView
            dv = ds.Tables("TOPIC").DefaultView
            dv.RowFilter = "ACTION <> 'D'"

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "TR_VALTOPICDOM_NAME"
                ViewState("Desc") = "ASC"
            End If
            dv.Sort = ViewState("SortName") & " " & ViewState("Desc")


            gvTopic.DataSource = dv
            gvTopic.DataBind()


            Dim imgUp As New Image
            imgUp.ImageUrl = "~/Images/Sortup.gif"
            Dim imgDown As New Image
            imgDown.ImageUrl = "~/Images/Sortdown.gif"

            Select Case ViewState("SortName").ToString
                Case "TR_VALTOPICDOM_NAME"
                    Select Case ViewState("Desc").ToString
                        Case "ASC"
                            gvTopic.HeaderRow.Cells(1).Controls.Add(imgUp)
                        Case "DESC"
                            gvTopic.HeaderRow.Cells(1).Controls.Add(imgDown)
                    End Select
                Case "TR_TOPICS"
                    Select Case ViewState("Desc").ToString
                        Case "ASC"
                            gvTopic.HeaderRow.Cells(3).Controls.Add(imgUp)
                        Case "DESC"
                            gvTopic.HeaderRow.Cells(3).Controls.Add(imgDown)
                    End Select

            End Select
        Else
            gvTopic.DataSource = Nothing
            gvTopic.DataBind()
        End If
	End Sub
#End Region

#Region "gvTopic_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTopic.RowCommand"
	Protected Sub gvTopic_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTopic.RowCommand
		If e.CommandName = "EditX" Then
			FillEditValues(e.CommandArgument)
		ElseIf e.CommandName = "DeleteX" Then
			DeleteItem(e.CommandArgument)
		End If
	End Sub
#End Region

#Region "FillValues"
	Protected Sub FillValues(ByVal objx As XmlDocument)
		With objx.DocumentElement.SelectSingleNode("TOPIC")
			hdCourseID.Value = .Attributes("TR_COURSE_ID").Value
            'txtCourseTitle.Text = .Attributes("TR_COURSE_NAME").Value
            ddlCourseTitle.SelectedValue = .Attributes("TR_COURSE_ID").Value
		End With
		showGrid(objx)
		Dim xNode As XmlNode
		xNode = objx.DocumentElement.SelectSingleNode("Errors")
		If Not xNode Is Nothing Then
			objx.DocumentElement.RemoveChild(xNode)
		End If
		Session("AddTopic") = objx.OuterXml
	End Sub
#End Region

#Region "AppendToMain"
	Protected Sub AppendToMain(ByVal strId As String)
		Dim objInputXml, objOutputXml As New XmlDocument
		Dim objbzTopic As New AAMS.bizTraining.bzTopic
        Dim xnode As XmlNode

        objInputXml.LoadXml("<TR_VIEW_TOPIC_INPUT><TOPIC TR_COURSE_ID=''/></TR_VIEW_TOPIC_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("TOPIC").Attributes("TR_COURSE_ID").InnerText = strId
        objOutputXml = objbzTopic.View(objInputXml)

        '      objInputXml.LoadXml("<TR_SEARCH_TOPIC_INPUT><TR_VALTOPICDOM_ID></TR_VALTOPICDOM_ID><TR_COURSE_ID></TR_COURSE_ID> <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></TR_SEARCH_TOPIC_INPUT>")
        'objInputXml.DocumentElement.SelectSingleNode("TR_COURSE_ID").InnerText = strId
        'objOutputXml = objbzTopic.Search(objInputXml)
		If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
			If Session("AddTopic") Is Nothing Then
				objInputXml.LoadXml("<TR_UPDATE_TOPIC_INPUT></TR_UPDATE_TOPIC_INPUT>")
			Else
				objInputXml.LoadXml(Session("AddTopic"))
			End If
			For Each xnode In objOutputXml.DocumentElement.SelectNodes("TOPIC")
				AppendData(xnode, objInputXml)
			Next
			showGrid(objInputXml)
		End If
	End Sub
#End Region

#Region "AppendData"
	Protected Sub AppendData(ByVal XDataNode As XmlNode, ByVal objInputXml As XmlDocument)
        Dim strInput As String = "<TR_UPDATE_TOPIC_INPUT><TOPIC TR_CVALTOPIC_ID='' ACTION='' TempRowID='' TR_VALTOPICDOM_ID='' TR_VALTOPICDOM_NAME='' TR_DOMAIN_ORDER='' TR_TOPICS='' TR_COURSE_ID='' TR_COURSE_NAME='' TR_CVALTOPICS_ORDER=''/></TR_UPDATE_TOPIC_INPUT>"
		Dim xDoc As New XmlDocument
		xDoc.LoadXml(strInput)
		With xDoc.DocumentElement.SelectSingleNode("TOPIC")
			If Not Session("AddTopic") Is Nothing Then
				If objInputXml.DocumentElement.SelectNodes("TOPIC").Count > 0 Then
                    .Attributes("TempRowID").Value = CInt(objInputXml.DocumentElement.SelectNodes("TOPIC").Item(objInputXml.DocumentElement.SelectNodes("TOPIC").Count - 1).Attributes("TempRowID").Value) + 1
				Else
                    .Attributes("TempRowID").Value = objInputXml.DocumentElement.SelectNodes("TOPIC").Count + 1
				End If
			Else
                .Attributes("TempRowID").Value = "1"
            End If
            Dim objDupNode As XmlNode
            objDupNode = objInputXml.DocumentElement.SelectSingleNode("TOPIC[@TR_VALTOPICDOM_ID='" + XDataNode.Attributes("TR_VALTOPICDOM_ID").Value + "'][@TR_TOPICS='" + XDataNode.Attributes("TR_TOPICS").Value + "'][@ACTION != 'D']")
            If objDupNode IsNot Nothing Then
                ' lblError.Text = "Topic already added with this domain."
                Exit Sub
            End If

            .Attributes("TR_VALTOPICDOM_ID").Value = XDataNode.Attributes("TR_VALTOPICDOM_ID").Value
            .Attributes("TR_VALTOPICDOM_NAME").Value = XDataNode.Attributes("TR_VALTOPICDOM_NAME").Value
            .Attributes("TR_TOPICS").Value = XDataNode.Attributes("TR_TOPICS").Value
            '.Attributes("TR_COURSE_ID").Value = hdCourseID.Value ' XDataNode.Attributes("TR_COURSE_ID").Value
            ' .Attributes("TR_COURSE_NAME").Value = txtCourseTitle.Text ' XDataNode.Attributes("TR_COURSE_NAME").Value
            .Attributes("TR_COURSE_ID").Value = ddlCourseTitle.SelectedValue
            .Attributes("TR_COURSE_NAME").Value = ddlCourseTitle.SelectedItem.Text
            .Attributes("ACTION").Value = ""
            .Attributes("TR_DOMAIN_ORDER").Value = XDataNode.Attributes("TR_DOMAIN_ORDER").Value
            .Attributes("TR_CVALTOPICS_ORDER").Value = XDataNode.Attributes("TR_CVALTOPICS_ORDER").Value

            objxmldocfrag = objInputXml.CreateDocumentFragment()
            objxmldocfrag.InnerXml = xDoc.DocumentElement.SelectSingleNode("TOPIC").OuterXml
            objInputXml.DocumentElement.AppendChild(objxmldocfrag)
            Session("AddTopic") = objInputXml.OuterXml
        End With
	End Sub
#End Region

    Protected Sub gvTopic_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvTopic.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If

            ' Code for delete msg confirmation.
            Dim btnDelete As LinkButton
            btnDelete = CType(e.Row.FindControl("lnkDelete"), LinkButton)
            btnDelete.Attributes.Add("OnClick", "javascript:return Delete();")

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvTopic_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvTopic.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvTopic_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvTopic.Sorting
        Try


            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ViewState("Desc") = "ASC"
            Else
                If ViewState("SortName") = SortName Then
                    If ViewState("Desc") = "DESC" Then
                        ViewState("Desc") = "ASC"
                    Else
                        ViewState("Desc") = "DESC"
                    End If
                Else
                    ViewState("SortName") = SortName
                    ViewState("Desc") = "ASC"
                End If
            End If
           

            ' Code For Grid Binding.

            If Not Session("AddTopic") Is Nothing Then
                Dim objXmlDoc As New XmlDocument()
                objXmlDoc.LoadXml(Session("AddTopic").ToString())
                showGrid(objXmlDoc)
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    
   
    Protected Sub btnPrintTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintTopic.Click
        Dim objInputXml, objOutputXml, objFeedbackStatusInput, objFeedbackStatusOutput As New XmlDocument
        Dim objbzTopic As New AAMS.bizTraining.bzTopic
        Dim objbzFeedBackLevel As New AAMS.bizTraining.bzFeedBackLevel
        Dim objXmlNode As XmlNode
        If hdID.Value <> "" Then


            Try
                objInputXml.LoadXml("<TR_VIEW_TOPIC_INPUT><TOPIC TR_COURSE_ID=''/></TR_VIEW_TOPIC_INPUT>")
                objFeedbackStatusInput.LoadXml("<TR_SEARCH_PARTMOOD_INPUT><TR_PART_MOOD_NAME></TR_PART_MOOD_NAME><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></TR_SEARCH_PARTMOOD_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("TOPIC").Attributes("TR_COURSE_ID").InnerText = hdID.Value
                objOutputXml = objbzTopic.View(objInputXml)
                objFeedbackStatusOutput = objbzFeedBackLevel.Search(objFeedbackStatusInput)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    For Each objXmlNode In objFeedbackStatusOutput.DocumentElement.SelectNodes("PTMOOD")
                        Dim objDocFrag As XmlDocumentFragment
                        objDocFrag = objOutputXml.CreateDocumentFragment()
                        objDocFrag.InnerXml = objXmlNode.OuterXml
                        objOutputXml.DocumentElement.AppendChild(objDocFrag)
                    Next

                    Session("FeedbackTopics") = objOutputXml.OuterXml
                    Response.Redirect("../RPSR_ReportShow.aspx?Case=FeedbackTopics")
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
        End If
    End Sub
End Class
