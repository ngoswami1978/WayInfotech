
Partial Class Training_TRUP_Register
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Protected strIndex As String
    Protected flagPermission As String
    Dim str As String
    Dim objED As New EncyrptDeCyrpt
    Public strBuilder As New StringBuilder
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
            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If

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
            '        btnNew.Enabled = False
            '    End If
            'Else
            '    strBuilder = objeAAMS.SecurityCheck(31)
            'End If
           
            txtStartDateRegisterTab.Text = Request.Form("txtStartDateRegisterTab")
            txtCourseTitleRegisterTab.Text = Request.Form("txtCourseTitleRegisterTab")
            txtCourseLevelRegisterTab.Text = Request.Form("txtCourseLevelRegisterTab")
            txtTrainingRoomRegisterTab.Text = Request.Form("txtTrainingRoomRegisterTab")
            txtMaxNoParticipantRegisterTab.Text = Request.Form("txtMaxNoParticipantRegisterTab")

            'Code commented by neraj as on date  14/01/2010
            'If hdCourseSessionBasketPopup.Value <> "" Then
            '    FillGrid(hdCourseSessionBasketPopup.Value)
            '    hdCourseSessionBasketPopup.Value = ""
            'End If

            If hdBasketListPopUpPage.Value <> "" Then
                'Dim strArray() As String = hdBasketListPopUpPage.Value.Split(",")
                'Dim strhdBasketListPopUpPage As String = ""

                'For Each strvalue As String In strArray
                '    strhdBasketListPopUpPage = CStr(strvalue)
                '    If strhdBasketListPopUpPage <> "" Then
                '        FillGrid(strhdBasketListPopUpPage, "B")
                '    End If
                'Next
                FillGrid(hdBasketListPopUpPage.Value, "B")
                hdBasketListPopUpPage.Value = ""
            End If

            If hdCourseSessionEmployeePopup.Value <> "" Then
                FillGrid(hdCourseSessionEmployeePopup.Value)
                hdCourseSessionEmployeePopup.Value = ""
            End If

            If hdCourseSessionPeoplePopup.Value <> "" Then
                FillGrid(hdCourseSessionPeoplePopup.Value)
                hdCourseSessionPeoplePopup.Value = ""
            End If
            ' Checking Security
            CheckSecurity()
            If Not Page.IsPostBack Then
                Bindata()
                DefaultStatusID()
                ' BindTrainer()
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
                    ViewData()
                    ViewRecords()
                    CheckTestQuestions()
                Else

                End If
            End If




        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Sub DefaultStatusID()
        Dim obj As New AAMS.bizTraining.bzParticipant
        Dim objXmlOutput As New XmlDocument
        objXmlOutput = obj.Default_Participant_Status
        If objXmlOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            hdDefaultStatusID.Value = objXmlOutput.DocumentElement.SelectSingleNode("DETAILS").Attributes("TR_PARTSTATUS_ID").Value
        End If

        '<TR_PARTICIPANT_STATUS_OUTPUT><DETAILS TR_PARTSTATUS_ID =''
        'TR_PARTSTATUS_NAME  ='' /><Errors Status=""><Error Code=""
        'Description=""/></Errors></TR_PARTICIPANT_STATUS_OUTPUT>

    End Sub
    Sub FillGrid(ByVal str As String, Optional ByVal strPopUpPageType As String = "")

        Dim objOutPutXml As New XmlDocument
        Dim strArray(0) As String
        objOutPutXml.LoadXml("<Root><Register><Details Recordtype='' Type='' Id='' AgencyName='' StaffName='' Ack='' TR_COURSEP_ID='' TR_COURSEP_RESULT='' TR_PRACTICAL_MARKS='' TR_PERCENTAGE_MARKS='' LCODE='' AGENCYSTAFFID='' TR_COURSEP_LOGIN='' TR_PARTSTATUS_ID='' TR_COURSEP_PWD='' TR_COURSES_ID='' TR_COURSEP_REMARKS=''  EMPLOYEEID='' TR_PR_AGENCY='' TR_CLETTER_DATESEND='' TR_CLETTER_DATERESEND='' TR_PCLETTER_DATESEND='' TR_PARTSTATUS_NAME='' TR_BASKET_REQUEST_ID='' TOTAL_THEORY_MARKS='' TOTAL_PRACTICAL_MARKS=''   /></Register></Root>")

        Dim objNode, objCloneNode As XmlNode
        objNode = objOutPutXml.DocumentElement.SelectSingleNode("Register/Details")
        objCloneNode = objNode.CloneNode(True)

        If str <> "" Then
            strArray = str.Split(",")
        End If

        If hdData.Value = "" And strArray.Length = 1 Then
            ' objOutPutXml.LoadXml("<TR_UPDATEPARTICIPANT_INPUT><PARTICIPANT TR_COURSEP_ID='' TR_COURSEP_RESULT='' LCODE='' AGENCYSTAFFID='' TR_COURSEP_LOGIN='' TR_PARTSTATUS_ID='' TR_COURSEP_PWD='' TR_COURSES_ID='' TR_COURSEP_REMARKS='' TR_COURSEP_ACK='' EMPLOYEEID='' TR_PR_AGENCY='' /></TR_UPDATEPARTICIPANT_INPUT>")

            objOutPutXml.DocumentElement.SelectSingleNode("Register").RemoveChild(objNode)
            objCloneNode.Attributes("Recordtype").Value = "1"
            objCloneNode.Attributes("Type").Value = str.Split("|").GetValue(0)
            objCloneNode.Attributes("Id").Value = str.Split("|").GetValue(1)
            objCloneNode.Attributes("AgencyName").Value = str.Split("|").GetValue(2)
            objCloneNode.Attributes("StaffName").Value = str.Split("|").GetValue(3)
            objCloneNode.Attributes("Ack").Value = "false"
            'Incase of employee (we are inserting branch id in LCODE )
            objCloneNode.Attributes("LCODE").Value = str.Split("|").GetValue(4)

            objCloneNode.Attributes("TR_PARTSTATUS_ID").Value = ""

            'objCloneNode.Attributes("EMPLOYEEID").Value = objeAAMS.EmployeeID(Session("Security"))
            If str.Split("|").GetValue(0) = "B" Then
                objCloneNode.Attributes("EMPLOYEEID").Value = objeAAMS.EmployeeID(Session("Security"))
                objCloneNode.Attributes("TR_BASKET_REQUEST_ID").Value = str.Split("|").GetValue(5)
            Else
                objCloneNode.Attributes("EMPLOYEEID").Value = "" 'str.Split("|").GetValue(4)
            End If

            If str.Split("|").GetValue(0) = "E" Or str.Split("|").GetValue(0) = "B" Or str.Split("|").GetValue(0) = "S" Then
                objCloneNode.Attributes("AGENCYSTAFFID").Value = str.Split("|").GetValue(1)
            Else
                objCloneNode.Attributes("AGENCYSTAFFID").Value = str.Split("|").GetValue(4)
            End If
            If str.Split("|").GetValue(0) = "E" Then
                objCloneNode.Attributes("TR_PR_AGENCY").Value = "0"
            Else
                objCloneNode.Attributes("TR_PR_AGENCY").Value = "1"
            End If

            objOutPutXml.DocumentElement.SelectSingleNode("Register").AppendChild(objCloneNode)
            hdData.Value = objOutPutXml.OuterXml
        Else
            If hdData.Value <> "" Then
                objOutPutXml.LoadXml(hdData.Value)
            Else
                objOutPutXml.DocumentElement.SelectSingleNode("Register").RemoveChild(objNode)
            End If

            Dim objCheckNode, objCheckNode1 As XmlNode

            If strPopUpPageType = "B" Then

                str = ""
                For Each str In strArray
                    objCheckNode = objOutPutXml.DocumentElement.SelectSingleNode("Register/Details[@Type='" + str.Split("|").GetValue(0) + "' and @Id='" + str.Split("|").GetValue(1) + "' ]")
                    objCheckNode1 = objOutPutXml.DocumentElement.SelectSingleNode("Register/Details[@AGENCYSTAFFID='" + str.Split("|").GetValue(1) + "' and @LCODE='" + str.Split("|").GetValue(4) + "' ]")
                    If objCheckNode Is Nothing And objCheckNode1 Is Nothing Then
                        'objNode = objOutPutXml.DocumentElement.SelectSingleNode("Register/Details")
                        'objCloneNode = objNode.CloneNode(True)
                        objCloneNode.Attributes("Recordtype").Value = "1"
                        objCloneNode.Attributes("Type").Value = str.Split("|").GetValue(0)
                        objCloneNode.Attributes("Id").Value = str.Split("|").GetValue(1)
                        objCloneNode.Attributes("AgencyName").Value = str.Split("|").GetValue(2)
                        objCloneNode.Attributes("StaffName").Value = str.Split("|").GetValue(3)
                        objCloneNode.Attributes("Ack").Value = "false"
                        objCloneNode.Attributes("LCODE").Value = str.Split("|").GetValue(4)
                        If str.Split("|").GetValue(0) = "E" Or str.Split("|").GetValue(0) = "B" Or str.Split("|").GetValue(0) = "S" Then
                            objCloneNode.Attributes("AGENCYSTAFFID").Value = str.Split("|").GetValue(1)
                        Else
                            objCloneNode.Attributes("AGENCYSTAFFID").Value = str.Split("|").GetValue(4)
                        End If

                        objCloneNode.Attributes("TR_PARTSTATUS_ID").Value = ""
                        If str.Split("|").GetValue(0) = "B" Then
                            objCloneNode.Attributes("EMPLOYEEID").Value = objeAAMS.EmployeeID(Session("Security"))
                            objCloneNode.Attributes("TR_BASKET_REQUEST_ID").Value = str.Split("|").GetValue(5)
                        Else
                            objCloneNode.Attributes("EMPLOYEEID").Value = "" 'str.Split("|").GetValue(4)
                        End If
                        If str.Split("|").GetValue(0) = "E" Then
                            objCloneNode.Attributes("TR_PR_AGENCY").Value = "0"
                        Else
                            objCloneNode.Attributes("TR_PR_AGENCY").Value = "1"
                        End If
                        objOutPutXml.DocumentElement.SelectSingleNode("Register").AppendChild(objCloneNode)
                        objCloneNode = objNode.CloneNode(True)
                        hdData.Value = objOutPutXml.OuterXml
                    Else
                        lblError.Text = "Already Added"
                    End If
                Next
            Else
                objCheckNode = objOutPutXml.DocumentElement.SelectSingleNode("Register/Details[@Type='" + str.Split("|").GetValue(0) + "' and @Id='" + str.Split("|").GetValue(1) + "' ]")
                objCheckNode1 = objOutPutXml.DocumentElement.SelectSingleNode("Register/Details[@AGENCYSTAFFID='" + str.Split("|").GetValue(1) + "' and @LCODE='" + str.Split("|").GetValue(4) + "' ]")

                If objCheckNode Is Nothing And objCheckNode1 Is Nothing Then
                    'objNode = objOutPutXml.DocumentElement.SelectSingleNode("Register/Details")
                    'objCloneNode = objNode.CloneNode(True)
                    objCloneNode.Attributes("Recordtype").Value = "1"
                    objCloneNode.Attributes("Type").Value = str.Split("|").GetValue(0)
                    objCloneNode.Attributes("Id").Value = str.Split("|").GetValue(1)
                    objCloneNode.Attributes("AgencyName").Value = str.Split("|").GetValue(2)
                    objCloneNode.Attributes("StaffName").Value = str.Split("|").GetValue(3)
                    objCloneNode.Attributes("Ack").Value = "false"
                    objCloneNode.Attributes("LCODE").Value = str.Split("|").GetValue(4)
                    If str.Split("|").GetValue(0) = "E" Or str.Split("|").GetValue(0) = "B" Or str.Split("|").GetValue(0) = "S" Then
                        objCloneNode.Attributes("AGENCYSTAFFID").Value = str.Split("|").GetValue(1)
                    Else
                        objCloneNode.Attributes("AGENCYSTAFFID").Value = str.Split("|").GetValue(4)
                    End If

                    objCloneNode.Attributes("TR_PARTSTATUS_ID").Value = ""
                    If str.Split("|").GetValue(0) = "B" Then
                        objCloneNode.Attributes("EMPLOYEEID").Value = objeAAMS.EmployeeID(Session("Security"))
                        objCloneNode.Attributes("TR_BASKET_REQUEST_ID").Value = str.Split("|").GetValue(5)
                    Else
                        objCloneNode.Attributes("EMPLOYEEID").Value = "" 'str.Split("|").GetValue(4)
                    End If
                    If str.Split("|").GetValue(0) = "E" Then
                        objCloneNode.Attributes("TR_PR_AGENCY").Value = "0"
                    Else
                        objCloneNode.Attributes("TR_PR_AGENCY").Value = "1"
                    End If
                    objOutPutXml.DocumentElement.SelectSingleNode("Register").AppendChild(objCloneNode)
                    hdData.Value = objOutPutXml.OuterXml
                Else
                    lblError.Text = "Already Added"
                End If
            End If
        End If


        FillGrid(PageOperation.Sorting)
        'Dim objNodeReader As XmlNodeReader
        'Dim ds As New DataSet
        'objNodeReader = New XmlNodeReader(objOutPutXml)
        'ds.ReadXml(objNodeReader)
        'gvRegisterTab.DataSource = ds.Tables("Details")
        'gvRegisterTab.DataBind()

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
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Register Participant']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Register Participant']").Attributes("Value").Value)
            End If
            If strBuilder(0) = "0" Then
                Response.Redirect("../NoRights.aspx")
                btnSave.Enabled = False
                btnEmployee.Enabled = False
                btnPeople.Enabled = False
                btnBasket.Enabled = False
                btnEmailAll.Enabled = False
                btnPrintAll.Enabled = False

            End If

            If strBuilder(1) = "0" Then
                btnSave.Enabled = False
                btnNew.Enabled = False
                btnEmployee.Enabled = False
                btnPeople.Enabled = False
                btnBasket.Enabled = False
                btnEmailAll.Enabled = False
                btnPrintAll.Enabled = False
            End If
            If strBuilder(2) = "0" And (hdPageCourseSessionID.Value <> "" Or Request.QueryString("CourseSessionID") IsNot Nothing) Then
                btnSave.Enabled = False
            End If

            If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                btnSave.Enabled = True
            End If
            If strBuilder(4) = "0" Then
                btnEmailAll.Enabled = False
                btnPrintAll.Enabled = False
                btnExport.Enabled = False
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
        Button1.Attributes.Add("onclick", "return ColorMethodRegister('" & Button1.ClientID.ToString() & "',3);")

    End Sub


    Sub ViewData()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzCourseSession As New AAMS.bizTraining.bzCourseSession
        objInputXml.LoadXml("<TR_VIEWCOURSES_INPUT> <TR_COURSES_ID /></TR_VIEWCOURSES_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("TR_COURSES_ID").InnerText = hdPageCourseSessionID.Value

        objOutputXml = objbzCourseSession.View(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            With objOutputXml.DocumentElement.SelectSingleNode("COURSES")

                '<COURSES TR_COURSES_ID="1" TR_COURSES_REQ_CONFIG="test" EMPLOYEE_ID2="8018" EMPLOYEE_ID1="227" 
                'TR_COURSES_NBPART="0" TR_COURSES_EXPECT_DATE="5/2/2008" TR_CLOCATION_ID="2" TR_COURSE_ID="91063" 
                'TR_CLOCATION_NAME="DEL Onsite" TRAINER1="Sarita Chaturvedi" TRAINER2="M.A.Jojomon" 
                'TR_CLOCATION_MAXNBPART="0" TR_CLETTER_ID="" TR_COURSES_END_DATE="5/6/2008" TR_COURSES_START_TIME="1728" 
                'TR_COURSES_END_TIME="1728" ACTUALNOOFPART="0" INTERNAL="False" /> 
                txtCourseTitleRegisterTab.Text = .Attributes("TR_COURSE_NAME").Value
                txtCourseLevelRegisterTab.Text = .Attributes("TR_COURSELEVEL_NAME").Value
                txtNMCTrainersRegisterTab.Text = .Attributes("TRAINER1").Value
                If .Attributes("TRAINER2").Value <> "" Then
                    txtNMCTrainersRegisterTab.Text = txtNMCTrainersRegisterTab.Text & "," & .Attributes("TRAINER2").Value
                End If

                hdCourseID.Value = .Attributes("TR_COURSE_ID").Value
                hdEnCourseID.Value = objED.Encrypt(hdCourseID.Value)
                hdTrainingRoomPage.Value = .Attributes("TR_CLOCATION_ID").Value
                txtTrainingRoomRegisterTab.Text = .Attributes("TR_CLOCATION_NAME").Value
                txtMaxNoParticipantRegisterTab.Text = .Attributes("TR_CLOCATION_MAXNBPART").Value
                hdEnPageAoffice.Value = objED.Encrypt(.Attributes("AOFFICE").Value)

                Dim strDate As String = .Attributes("TR_COURSES_EXPECT_DATE").Value
                If strDate <> "" Then
                    Dim day As String = strDate.Split("/").GetValue(0)
                    Dim month As String = strDate.Split("/").GetValue(1)
                    If day.Length = 1 Then
                        day = "0" & day
                    End If
                    If month.Length = 1 Then
                        month = "0" & month
                    End If
                    strDate = day & "/" & month & "/" & strDate.Split("/").GetValue(2)
                    txtStartDateRegisterTab.Text = objeAAMS.GetDateFormat(strDate, "MM/dd/yyyy", "dd/MM/yyyy", "/")
                    txtStartDateRegisterTab.Text = txtStartDateRegisterTab.Text & " " & .Attributes("TR_COURSES_START_TIME").Value.Substring(0, 2) & ":" & .Attributes("TR_COURSES_START_TIME").Value.Substring(2)
                End If
                'Start Code to store Total marks for the test
                hdTotalPracticalMarks.Value = Val(.Attributes("PRACTICAL_MARKS").Value)
                hdTotalTheoryMarks.Value = Val(.Attributes("THEORY_MARKS").Value)
                hdTotalMarks.Value = (Val(.Attributes("PRACTICAL_MARKS").Value) + Val(.Attributes("THEORY_MARKS").Value)).ToString
                txtTotalMarks.Text = hdTotalMarks.Value
                'End Code
            End With


        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
    Sub ViewRecords()
       
        '<TR_VIEWPARTICIPANT_INPUT>
        ' <TR_COURSES_ID />
        '</TR_VIEWPARTICIPANT_INPUT>

        

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzParticipant As New AAMS.bizTraining.bzParticipant
        objInputXml.LoadXml("<TR_VIEWPARTICIPANT_INPUT> <TR_COURSES_ID /></TR_VIEWPARTICIPANT_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("TR_COURSES_ID").InnerText = hdPageCourseSessionID.Value

        objOutputXml = objbzParticipant.View(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            Dim objNodeList As XmlNodeList
            objNodeList = objOutputXml.DocumentElement.SelectNodes("PARTICIPANT")
            For Each objNodeParent As XmlNode In objNodeList
                'Output :
                '<TR_VIEWPARTICIPANT_OUTPUT>
                '<PARTICIPANT TR_COURSEP_ID="9" TR_COURSEP_RESULT="6.00" LCODE="137" AGENCYNAME="DEL" 
                'AGENCYSTAFFID="202702" PARTICIPANTNAME="Nivin Verma" TR_CLETTER_DATESEND="" 
                'TR_CLETTER_DATERESEND="" TR_PARTSTATUS_ID="5" TR_PARTSTATUS_NAME="confirmed" TR_COURSEP_LOGIN="9" 
                'TR_COURSEP_PWD="9041685723" TR_COURSEP_REMARKS="test" TR_COURSEP_ACK="True" TR_PCLETTER_ID="" 
                'TR_CLETTER_ID="" EMPLOYEEID="" TR_PR_AGENCY="False" /> 

                ' <Errors Status=''>
                ' <Error Code='' Description='' />
                ' </Errors>
                '</TR_VIEWPARTICIPANT_OUTPUT>

                ''''''''''''''''''''
                If hdData.Value = "" Then

                    objOutputXml.LoadXml("<Root><Register><Details Recordtype='' Type='' Id='' AgencyName='' StaffName='' Ack='' TR_COURSEP_ID='' TR_COURSEP_RESULT='' TR_PRACTICAL_MARKS='' TR_PERCENTAGE_MARKS='' LCODE='' AGENCYSTAFFID='' TR_COURSEP_LOGIN='' TR_PARTSTATUS_ID='' TR_COURSEP_PWD='' TR_COURSES_ID='' TR_COURSEP_REMARKS=''  EMPLOYEEID='' TR_PR_AGENCY='' TR_CLETTER_DATESEND='' TR_CLETTER_DATERESEND='' TR_PCLETTER_DATESEND='' TR_PARTSTATUS_NAME='' TR_BASKET_REQUEST_ID='' TOTAL_THEORY_MARKS='' TOTAL_PRACTICAL_MARKS=''  /></Register></Root>")
                    Dim objNode, objCloneNode As XmlNode
                    objNode = objOutputXml.DocumentElement.SelectSingleNode("Register/Details")
                    objCloneNode = objNode.CloneNode(True)
                    objOutputXml.DocumentElement.SelectSingleNode("Register").RemoveChild(objNode)
                    objCloneNode.Attributes("Recordtype").Value = "0"
                    objCloneNode.Attributes("Type").Value = ""
                    objCloneNode.Attributes("Id").Value = ""
                    objCloneNode.Attributes("AgencyName").Value = objNodeParent.Attributes("AGENCYNAME").Value
                    objCloneNode.Attributes("StaffName").Value = objNodeParent.Attributes("PARTICIPANTNAME").Value

                    objCloneNode.Attributes("TR_CLETTER_DATESEND").Value = objNodeParent.Attributes("TR_CLETTER_DATESEND").Value
                    objCloneNode.Attributes("TR_CLETTER_DATERESEND").Value = objNodeParent.Attributes("TR_CLETTER_DATERESEND").Value

                    objCloneNode.Attributes("TR_PCLETTER_DATESEND").Value = objNodeParent.Attributes("TR_PCLETTER_DATESEND").Value

                    objCloneNode.Attributes("TR_COURSEP_LOGIN").Value = objNodeParent.Attributes("TR_COURSEP_LOGIN").Value
                    objCloneNode.Attributes("TR_COURSEP_PWD").Value = objNodeParent.Attributes("TR_COURSEP_PWD").Value
                    objCloneNode.Attributes("TR_COURSEP_REMARKS").Value = objNodeParent.Attributes("TR_COURSEP_REMARKS").Value
                    objCloneNode.Attributes("TR_COURSEP_RESULT").Value = objNodeParent.Attributes("TR_COURSEP_RESULT").Value
                    objCloneNode.Attributes("TR_PRACTICAL_MARKS").Value = objNodeParent.Attributes("TR_PRACTICAL_MARKS").Value

                    objCloneNode.Attributes("TR_PERCENTAGE_MARKS").Value = objNodeParent.Attributes("TR_PERCENTAGE_MARKS").Value

                    objCloneNode.Attributes("Ack").Value = IIf(objNodeParent.Attributes("TR_COURSEP_ACK").Value = "1", "True", "False")
                    objCloneNode.Attributes("LCODE").Value = objNodeParent.Attributes("LCODE").Value
                    'Added on 5 feb 09 for basket participants 
                    objCloneNode.Attributes("TR_BASKET_REQUEST_ID").Value = objNodeParent.Attributes("TR_BASKET_REQUEST_ID").Value
                    'end
                    objCloneNode.Attributes("AGENCYSTAFFID").Value = objNodeParent.Attributes("AGENCYSTAFFID").Value
                    objCloneNode.Attributes("TR_PARTSTATUS_ID").Value = objNodeParent.Attributes("TR_PARTSTATUS_ID").Value
                    objCloneNode.Attributes("TR_PARTSTATUS_NAME").Value = objNodeParent.Attributes("TR_PARTSTATUS_NAME").Value
                    objCloneNode.Attributes("EMPLOYEEID").Value = objNodeParent.Attributes("EMPLOYEEID").Value
                    If objNodeParent.Attributes("TR_PR_AGENCY").Value = "False" Then
                        objCloneNode.Attributes("TR_PR_AGENCY").Value = "0"
                    ElseIf objNodeParent.Attributes("TR_PR_AGENCY").Value = "True" Then
                        objCloneNode.Attributes("TR_PR_AGENCY").Value = "1"
                    Else
                        objCloneNode.Attributes("TR_PR_AGENCY").Value = objNodeParent.Attributes("TR_PR_AGENCY").Value
                    End If

                    objCloneNode.Attributes("TR_COURSEP_ID").Value = objNodeParent.Attributes("TR_COURSEP_ID").Value
                    objCloneNode.Attributes("TOTAL_THEORY_MARKS").Value = objNodeParent.Attributes("TOTAL_THEORY_MARKS").Value
                    objCloneNode.Attributes("TOTAL_PRACTICAL_MARKS").Value = objNodeParent.Attributes("TOTAL_PRACTICAL_MARKS").Value


                    hdParticipantID.Value = objNodeParent.Attributes("TR_COURSEP_ID").Value
                    objOutputXml.DocumentElement.SelectSingleNode("Register").AppendChild(objCloneNode)
                    hdData.Value = objOutputXml.OuterXml

                Else
                    objOutputXml.LoadXml(hdData.Value)
                    Dim objNode, objCloneNode, objCheckNode As XmlNode
                    '  objCheckNode = objOutputXml.DocumentElement.SelectSingleNode("Register/Details[@Type='" + str.Split("|").GetValue(0) + "' and @Id='" + str.Split("|").GetValue(1) + "' and Recordtype='1']")
                    '  If objCheckNode Is Nothing Then
                    objNode = objOutputXml.DocumentElement.SelectSingleNode("Register/Details")
                    objCloneNode = objNode.CloneNode(True)
                    objCloneNode.Attributes("Recordtype").Value = "0"
                    objCloneNode.Attributes("Type").Value = ""
                    objCloneNode.Attributes("Id").Value = ""
                    objCloneNode.Attributes("AgencyName").Value = objNodeParent.Attributes("AGENCYNAME").Value
                    objCloneNode.Attributes("StaffName").Value = objNodeParent.Attributes("PARTICIPANTNAME").Value
                    objCloneNode.Attributes("TR_PCLETTER_DATESEND").Value = objNodeParent.Attributes("TR_PCLETTER_DATESEND").Value
                    objCloneNode.Attributes("TR_CLETTER_DATESEND").Value = objNodeParent.Attributes("TR_CLETTER_DATESEND").Value
                    objCloneNode.Attributes("TR_CLETTER_DATERESEND").Value = objNodeParent.Attributes("TR_CLETTER_DATERESEND").Value
                    objCloneNode.Attributes("TR_COURSEP_LOGIN").Value = objNodeParent.Attributes("TR_COURSEP_LOGIN").Value
                    objCloneNode.Attributes("TR_COURSEP_PWD").Value = objNodeParent.Attributes("TR_COURSEP_PWD").Value
                    objCloneNode.Attributes("TR_COURSEP_REMARKS").Value = objNodeParent.Attributes("TR_COURSEP_REMARKS").Value
                    objCloneNode.Attributes("TR_COURSEP_RESULT").Value = objNodeParent.Attributes("TR_COURSEP_RESULT").Value
                    objCloneNode.Attributes("TR_PRACTICAL_MARKS").Value = objNodeParent.Attributes("TR_PRACTICAL_MARKS").Value

                    objCloneNode.Attributes("TR_PERCENTAGE_MARKS").Value = objNodeParent.Attributes("TR_PERCENTAGE_MARKS").Value

                    objCloneNode.Attributes("Ack").Value = IIf(objNodeParent.Attributes("TR_COURSEP_ACK").Value = "1", "True", "False")
                    objCloneNode.Attributes("LCODE").Value = objNodeParent.Attributes("LCODE").Value
                    'Added on 5 feb 09 for basket participants 
                    objCloneNode.Attributes("TR_BASKET_REQUEST_ID").Value = objNodeParent.Attributes("TR_BASKET_REQUEST_ID").Value
                    'end
                    objCloneNode.Attributes("AGENCYSTAFFID").Value = objNodeParent.Attributes("AGENCYSTAFFID").Value
                    objCloneNode.Attributes("TR_PARTSTATUS_ID").Value = objNodeParent.Attributes("TR_PARTSTATUS_ID").Value
                    objCloneNode.Attributes("TR_PARTSTATUS_NAME").Value = objNodeParent.Attributes("TR_PARTSTATUS_NAME").Value

                    objCloneNode.Attributes("EMPLOYEEID").Value = objNodeParent.Attributes("EMPLOYEEID").Value
                    If objNodeParent.Attributes("TR_PR_AGENCY").Value = "False" Then
                        objCloneNode.Attributes("TR_PR_AGENCY").Value = "0"
                    ElseIf objNodeParent.Attributes("TR_PR_AGENCY").Value = "True" Then
                        objCloneNode.Attributes("TR_PR_AGENCY").Value = "1"
                    Else
                        objCloneNode.Attributes("TR_PR_AGENCY").Value = objNodeParent.Attributes("TR_PR_AGENCY").Value
                    End If
                    objCloneNode.Attributes("TOTAL_THEORY_MARKS").Value = objNodeParent.Attributes("TOTAL_THEORY_MARKS").Value
                    objCloneNode.Attributes("TOTAL_PRACTICAL_MARKS").Value = objNodeParent.Attributes("TOTAL_PRACTICAL_MARKS").Value

                    objCloneNode.Attributes("TR_COURSEP_ID").Value = objNodeParent.Attributes("TR_COURSEP_ID").Value
                    objOutputXml.DocumentElement.SelectSingleNode("Register").AppendChild(objCloneNode)
                    hdData.Value = objOutputXml.OuterXml
                    ' Else
                    '  lblError.Text = "Already Added"
                    ' End If
                End If


        '''''''''''''''''




            Next
            If hdData.Value <> "" Then
                If ViewState("SortName") IsNot Nothing Then
                    FillGrid(PageOperation.Sorting, ViewState("SortName"), ViewState("Desc"))
                Else
                    FillGrid(PageOperation.Sorting)
                End If
            End If

        ElseIf (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value <> "No Record Found!") Then
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

        End If

    End Sub

    Sub CheckTestQuestions()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzCourse As New AAMS.bizTraining.bzCourse
        objInputXml.LoadXml("<TR_CHECK_TEST_QUESTIONS_INPUT><TR_COURSES_ID /></TR_CHECK_TEST_QUESTIONS_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("TR_COURSES_ID").InnerText = hdPageCourseSessionID.Value

        objOutputXml = objbzCourse.CHECK_TEST_QUESTIONS(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            hdCheckQuestion.Value = "True" 'It means user can save the record and register the participants
        Else
            hdCheckQuestion.Value = "False" 'It means user can't save the record and cann't register the participants
            '  btnSave.Enabled = False
            'btnPeople.Enabled = False
            'btnEmployee.Enabled = False
            'btnBasket.Enabled = False
            'lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If

    End Sub
    Sub BindXml()
        Dim objOutputXml As New XmlDocument
        If gvRegisterTab.Rows.Count > 0 Then

            Dim objNodeList As XmlNodeList
            Dim objNode As XmlNode
            objOutputXml.LoadXml(hdData.Value)

            objNodeList = objOutputXml.DocumentElement.SelectNodes("Register/Details")

            'Details Recordtype='' Type='' Id='' AgencyName='' StaffName='' Ack='' TR_COURSEP_ID=''
            ' TR_COURSEP_RESULT='' LCODE='' AGENCYSTAFFID='' TR_COURSEP_LOGIN='' TR_PARTSTATUS_ID='' 
            'TR_COURSEP_PWD='' TR_COURSES_ID='' TR_COURSEP_REMARKS=''  EMPLOYEEID='' TR_PR_AGENCY=''

            'hdUniqueID|Type|Id|Recordtype|TR_COURSEP_ID

            'For i As Integer = 0 To objNodeList.Count - 1
            '    objNode = objNodeList(i)
            '    objNode.Attributes("Ack").Value = IIf(CType(gvRegisterTab.Rows(i).Cells(9).FindControl("chkAck"), CheckBox).Checked, "1", "0")
            '    If CType(gvRegisterTab.Rows(i).Cells(5).FindControl("txtResult"), TextBox).Text <> "" Then
            '        objNode.Attributes("TR_COURSEP_RESULT").Value = CType(gvRegisterTab.Rows(i).Cells(5).FindControl("txtResult"), TextBox).Text
            '    Else
            '        objNode.Attributes("TR_COURSEP_RESULT").Value = Request.Form(CType(gvRegisterTab.Rows(i).Cells(5).FindControl("txtResult"), TextBox).UniqueID)
            '    End If

            '    If CType(gvRegisterTab.Rows(i).Cells(6).FindControl("txtPResult"), TextBox).Text <> "" Then
            '        objNode.Attributes("TR_PRACTICAL_MARKS").Value = CType(gvRegisterTab.Rows(i).Cells(6).FindControl("txtPResult"), TextBox).Text
            '    Else
            '        objNode.Attributes("TR_PRACTICAL_MARKS").Value = Request.Form(CType(gvRegisterTab.Rows(i).Cells(6).FindControl("txtPResult"), TextBox).UniqueID)
            '    End If
            '    objNode.Attributes("TR_COURSEP_REMARKS").Value = CType(gvRegisterTab.Rows(i).Cells(10).FindControl("txtRemarks"), TextBox).Text
            '    objNode.Attributes("TR_PARTSTATUS_ID").Value = CType(gvRegisterTab.Rows(i).Cells(4).FindControl("ddlStatus"), DropDownList).SelectedValue
            'Next
            'hdData.Value = objOutputXml.OuterXml
            'New Code added on 8 Aug 08 
            'In place of code given Above
            For Each grdRow As GridViewRow In gvRegisterTab.Rows
                Dim strUniqueValue As String
                strUniqueValue = CType(grdRow.FindControl("hdUniqueID"), HiddenField).Value
                If strUniqueValue.Split("|").GetValue(2) = "0" Then
                    objNode = objOutputXml.DocumentElement.SelectSingleNode("Register/Details[@TR_COURSEP_ID='" + strUniqueValue.Split("|").GetValue(3) + "']")
                    objNode.Attributes("Ack").Value = IIf(CType(grdRow.Cells(9).FindControl("chkAck"), CheckBox).Checked, "1", "0")
                    If CType(grdRow.Cells(5).FindControl("txtResult"), TextBox).Text <> "" Then
                        objNode.Attributes("TR_COURSEP_RESULT").Value = CType(grdRow.Cells(5).FindControl("txtResult"), TextBox).Text
                    Else
                        objNode.Attributes("TR_COURSEP_RESULT").Value = Request.Form(CType(grdRow.Cells(5).FindControl("txtResult"), TextBox).UniqueID)
                    End If

                    If CType(grdRow.Cells(6).FindControl("txtPResult"), TextBox).Text <> "" Then
                        objNode.Attributes("TR_PRACTICAL_MARKS").Value = CType(grdRow.Cells(6).FindControl("txtPResult"), TextBox).Text
                    Else
                        objNode.Attributes("TR_PRACTICAL_MARKS").Value = Request.Form(CType(grdRow.Cells(6).FindControl("txtPResult"), TextBox).UniqueID)
                    End If
                    objNode.Attributes("TR_COURSEP_REMARKS").Value = CType(grdRow.Cells(11).FindControl("txtRemarks"), TextBox).Text
                    objNode.Attributes("TR_PARTSTATUS_ID").Value = CType(grdRow.Cells(4).FindControl("ddlStatus"), DropDownList).SelectedValue
                    objNode.Attributes("TR_PARTSTATUS_NAME").Value = CType(grdRow.Cells(4).FindControl("ddlStatus"), DropDownList).SelectedItem.Text
                Else
                    objNode = objOutputXml.DocumentElement.SelectSingleNode("Register/Details[@Type='" + strUniqueValue.Split("|").GetValue(0) + "' and @Id='" + strUniqueValue.Split("|").GetValue(1) + "']")
                    objNode.Attributes("Ack").Value = IIf(CType(grdRow.Cells(10).FindControl("chkAck"), CheckBox).Checked, "1", "0")
                    If CType(grdRow.Cells(5).FindControl("txtResult"), TextBox).Text <> "" Then
                        objNode.Attributes("TR_COURSEP_RESULT").Value = CType(grdRow.Cells(5).FindControl("txtResult"), TextBox).Text
                    Else
                        objNode.Attributes("TR_COURSEP_RESULT").Value = Request.Form(CType(grdRow.Cells(5).FindControl("txtResult"), TextBox).UniqueID)
                    End If

                    If CType(grdRow.Cells(6).FindControl("txtPResult"), TextBox).Text <> "" Then
                        objNode.Attributes("TR_PRACTICAL_MARKS").Value = CType(grdRow.Cells(6).FindControl("txtPResult"), TextBox).Text
                    Else
                        objNode.Attributes("TR_PRACTICAL_MARKS").Value = Request.Form(CType(grdRow.Cells(6).FindControl("txtPResult"), TextBox).UniqueID)
                    End If
                    objNode.Attributes("TR_COURSEP_REMARKS").Value = CType(grdRow.Cells(11).FindControl("txtRemarks"), TextBox).Text
                    objNode.Attributes("TR_PARTSTATUS_ID").Value = CType(grdRow.Cells(4).FindControl("ddlStatus"), DropDownList).SelectedValue
                    objNode.Attributes("TR_PARTSTATUS_NAME").Value = CType(grdRow.Cells(4).FindControl("ddlStatus"), DropDownList).SelectedItem.Text
                End If
            Next

           

           
        End If

        hdData.Value = objOutputXml.OuterXml
    End Sub

    Sub FillGrid(ByVal pageOperation As Integer, Optional ByVal strSortingColumn As String = "AgencyName", Optional ByVal strSortOrder As String = "ASC")
        Dim objGrid As New XmlDocument
        objGrid.LoadXml(hdData.Value)
        Dim objNodeReader As XmlNodeReader
        Dim ds As New DataSet
        objNodeReader = New XmlNodeReader(objGrid)
        ds.ReadXml(objNodeReader)
        Dim dv As DataView
        If ds.Tables("Details") IsNot Nothing Then
            dv = ds.Tables("Details").DefaultView
            If strSortingColumn <> "" Then
                dv.Sort = strSortingColumn & " " & strSortOrder
            End If
            gvRegisterTab.DataSource = dv
            gvRegisterTab.DataBind()
            Dim imgUp As New Image
            imgUp.ImageUrl = "~/Images/Sortup.gif"
            Dim imgDown As New Image
            imgDown.ImageUrl = "~/Images/Sortdown.gif"

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "AgencyName"
                ViewState("Desc") = "ASC"
            End If

            Select Case ViewState("SortName")
                Case "AgencyName"
                    Select Case ViewState("Desc")
                        Case "ASC"
                            gvRegisterTab.HeaderRow.Cells(0).Controls.Add(imgUp)
                        Case "DESC"
                            gvRegisterTab.HeaderRow.Cells(0).Controls.Add(imgDown)
                    End Select
                Case "StaffName"
                    Select Case ViewState("Desc")
                        Case "ASC"
                            gvRegisterTab.HeaderRow.Cells(1).Controls.Add(imgUp)
                        Case "DESC"
                            gvRegisterTab.HeaderRow.Cells(1).Controls.Add(imgDown)
                    End Select
                Case "TR_CLETTER_DATESEND"
                    Select Case ViewState("Desc")
                        Case "ASC"
                            gvRegisterTab.HeaderRow.Cells(2).Controls.Add(imgUp)
                        Case "DESC"
                            gvRegisterTab.HeaderRow.Cells(2).Controls.Add(imgDown)

                    End Select


                Case "TR_PCLETTER_DATESEND"
                    Select Case ViewState("Desc")
                        Case "ASC"
                            gvRegisterTab.HeaderRow.Cells(3).Controls.Add(imgUp)
                        Case "DESC"
                            gvRegisterTab.HeaderRow.Cells(3).Controls.Add(imgDown)
                    End Select
                Case "TR_PARTSTATUS_ID"
                    Select Case ViewState("Desc")
                        Case "ASC"
                            gvRegisterTab.HeaderRow.Cells(4).Controls.Add(imgUp)
                        Case "DESC"
                            gvRegisterTab.HeaderRow.Cells(4).Controls.Add(imgDown)
                    End Select
                Case "TR_COURSEP_RESULT"
                    Select Case ViewState("Desc")
                        Case "ASC"
                            gvRegisterTab.HeaderRow.Cells(5).Controls.Add(imgUp)
                        Case "DESC"
                            gvRegisterTab.HeaderRow.Cells(5).Controls.Add(imgDown)

                    End Select
                Case "TR_PRACTICAL_MARKS"
                    Select Case ViewState("Desc")
                        Case "ASC"
                            gvRegisterTab.HeaderRow.Cells(6).Controls.Add(imgUp)
                        Case "DESC"
                            gvRegisterTab.HeaderRow.Cells(6).Controls.Add(imgDown)
                    End Select
                Case "TR_PERCENTAGE_MARKS"
                    Select Case ViewState("Desc")
                        Case "ASC"
                            gvRegisterTab.HeaderRow.Cells(7).Controls.Add(imgUp)
                        Case "DESC"
                            gvRegisterTab.HeaderRow.Cells(7).Controls.Add(imgDown)
                    End Select

                Case "TR_COURSEP_LOGIN"
                    Select Case ViewState("Desc")
                        Case "ASC"
                            gvRegisterTab.HeaderRow.Cells(8).Controls.Add(imgUp)
                        Case "DESC"
                            gvRegisterTab.HeaderRow.Cells(8).Controls.Add(imgDown)
                    End Select
                Case "TR_COURSEP_PWD"
                    Select Case ViewState("Desc")
                        Case "ASC"
                            gvRegisterTab.HeaderRow.Cells(9).Controls.Add(imgUp)
                        Case "DESC"
                            gvRegisterTab.HeaderRow.Cells(9).Controls.Add(imgDown)

                    End Select
                Case "TR_COURSEP_REMARKS"
                    Select Case ViewState("Desc")
                        Case "ASC"
                            gvRegisterTab.HeaderRow.Cells(11).Controls.Add(imgUp)
                        Case "DESC"
                            gvRegisterTab.HeaderRow.Cells(11).Controls.Add(imgDown)
                    End Select
            End Select
        Else

            gvRegisterTab.DataSource = Nothing
            gvRegisterTab.DataBind()
        End If

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        '  <TR_UPDATEPARTICIPANT_INPUT>
        ' <PARTICIPANT TR_COURSEP_ID='' TR_COURSEP_RESULT='' LCODE='' AGENCYSTAFFID='' TR_COURSEP_LOGIN='' TR_PARTSTATUS_ID='' TR_COURSEP_PWD='' TR_COURSES_ID='' TR_COURSEP_REMARKS='' TR_COURSEP_ACK='' EMPLOYEEID='' TR_PR_AGENCY='' />
        '</TR_UPDATEPARTICIPANT_INPUT>

        'Output :
        '<TR_UPDATEPARTICIPANT_OUTPUT>
        ' <PARTICIPANT TR_COURSEP_ID='' TR_COURSEP_RESULT='' LCODE='' AGENCYSTAFFID='' TR_COURSEP_LOGIN='' TR_PARTSTATUS_ID='' TR_COURSEP_PWD='' TR_COURSES_ID='' TR_COURSEP_REMARKS='' TR_COURSEP_ACK='' EMPLOYEEID='' TR_PR_AGENCY='' />
        ' <Errors Status=''><Error Code='' Description='' /></Errors>
        '</TR_UPDATEPARTICIPANT_OUTPUT>
        'CheckTestQuestions()
        'If hdCheckQuestion.Value = "False" Then
        'Exit Sub
        'End If
        If hdData.Value <> "" Then
            If gvRegisterTab.Rows.Count Then
                BindXml()
            End If
            Dim objInputXml, objOutputXml, objInputXmlTemp As New XmlDocument
            Dim objNodeList As XmlNodeList
            Dim objbzParticipant As New AAMS.bizTraining.bzParticipant
            Try
                objInputXml.LoadXml("<TR_UPDATEPARTICIPANT_INPUT><PARTICIPANT TR_COURSEP_ID='' TR_COURSEP_RESULT='' TR_PRACTICAL_MARKS='' TR_PERCENTAGE_MARKS='' LCODE='' AGENCYSTAFFID='' TR_COURSEP_LOGIN='' TR_PARTSTATUS_ID='' TR_COURSEP_PWD='' TR_COURSES_ID='' TR_COURSEP_REMARKS='' TR_COURSEP_ACK='' EMPLOYEEID='' TR_PR_AGENCY='' TR_PARTSTATUS_NAME='' TR_BASKET_REQUEST_ID='' /><EMP EMPLOYEEID=''/></TR_UPDATEPARTICIPANT_INPUT>")

                objInputXmlTemp.LoadXml(hdData.Value)
                objNodeList = objInputXmlTemp.DocumentElement.SelectNodes("Register/Details")

                Dim objNodeParticipant, objCloneNodeParticipant As XmlNode
                objNodeParticipant = objInputXml.DocumentElement.SelectSingleNode("PARTICIPANT")
                objCloneNodeParticipant = objNodeParticipant.CloneNode(True)
                objInputXml.DocumentElement.RemoveChild(objNodeParticipant)
                For Each objNode As XmlNode In objNodeList
                    With objCloneNodeParticipant

                        .Attributes("TR_COURSEP_ID").Value = objNode.Attributes("TR_COURSEP_ID").Value
                        .Attributes("TR_COURSEP_RESULT").Value = objNode.Attributes("TR_COURSEP_RESULT").Value
                        .Attributes("TR_PRACTICAL_MARKS").Value = objNode.Attributes("TR_PRACTICAL_MARKS").Value
                        Dim dclTheoryresult As Decimal = Val(objNode.Attributes("TR_COURSEP_RESULT").Value)
                        Dim dclPracticalresult As Decimal = Val(objNode.Attributes("TR_PRACTICAL_MARKS").Value)
                        Dim dclPercentage As Decimal

                        If hdTotalMarks.Value <> "0" And (dclTheoryresult + dclPracticalresult) <> 0 Then
                            dclPercentage = ((dclTheoryresult + dclPracticalresult) / Convert.ToInt32(hdTotalMarks.Value)) * 100
                            .Attributes("TR_PERCENTAGE_MARKS").Value = dclPercentage.ToString()
                        End If




                        .Attributes("LCODE").Value = objNode.Attributes("LCODE").Value
                        .Attributes("TR_BASKET_REQUEST_ID").Value = objNode.Attributes("TR_BASKET_REQUEST_ID").Value
                        .Attributes("AGENCYSTAFFID").Value = objNode.Attributes("AGENCYSTAFFID").Value
                        .Attributes("TR_PARTSTATUS_ID").Value = objNode.Attributes("TR_PARTSTATUS_ID").Value
                        .Attributes("TR_PARTSTATUS_NAME").Value = objNode.Attributes("TR_PARTSTATUS_NAME").Value
                        .Attributes("TR_COURSES_ID").Value = hdPageCourseSessionID.Value
                        .Attributes("TR_COURSEP_REMARKS").Value = objNode.Attributes("TR_COURSEP_REMARKS").Value

                        .Attributes("TR_COURSEP_ACK").Value = objNode.Attributes("Ack").Value
                        .Attributes("EMPLOYEEID").Value = objNode.Attributes("EMPLOYEEID").Value
                        .Attributes("TR_PR_AGENCY").Value = objNode.Attributes("TR_PR_AGENCY").Value
                        .Attributes("TR_COURSEP_LOGIN").Value = objNode.Attributes("TR_COURSEP_LOGIN").Value
                        .Attributes("TR_COURSEP_PWD").Value = objNode.Attributes("TR_COURSEP_PWD").Value

                    End With
                    objInputXml.DocumentElement.AppendChild(objCloneNodeParticipant)
                    objCloneNodeParticipant = objNodeParticipant.CloneNode(True)
                Next

                'added for history on 2 feb 09
                '<EMP EMPLOYEEID='24'/>
                If Session("Security") IsNot Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("EMP").Attributes("EMPLOYEEID").Value = objeAAMS.EmployeeID(Session("Security"))
                End If

                'end
                'Here Back end Method Call
                objOutputXml = objbzParticipant.Update(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If hdParticipantID.Value = "" Then
                        lblError.Text = objeAAMSMessage.messInsert
                        hdParticipantID.Value = objOutputXml.DocumentElement.SelectSingleNode("PARTICIPANT").Attributes("TR_COURSEP_ID").Value
                    Else
                        lblError.Text = objeAAMSMessage.messUpdate
                    End If
                    hdData.Value = ""
                    ViewRecords()

                    If ViewState("SortName") IsNot Nothing Then
                        FillGrid(PageOperation.Sorting, ViewState("SortName"), ViewState("Desc"))
                    Else
                        FillGrid(PageOperation.Sorting)
                    End If
                    CheckSecurity()

                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            Catch ex As Exception
                lblError.Text = ex.Message.ToString
            Finally

            End Try
        End If
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("TRUP_CourseSession.aspx?Action=I")
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

    Protected Sub gvRegisterTab_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvRegisterTab.RowCommand
        Dim strID As String = ""
        Dim objXmlDoc As New XmlDocument
        Dim objXmlNode As XmlNode
        Try
            If e.CommandName = "DeleteX" Then
                strID = e.CommandArgument.ToString()
                objXmlDoc.LoadXml(hdData.Value)
                If strID.Split("|").GetValue(2) = "0" Then
                    Dim objInputXml, objOutputXml As New XmlDocument
                    Dim objbzParticipant As New AAMS.bizTraining.bzParticipant
                    objInputXml.LoadXml("<TR_DELETEPARTICIPANT_INPUT> <TR_COURSEP_ID /></TR_DELETEPARTICIPANT_INPUT>")
                    objInputXml.DocumentElement.SelectSingleNode("TR_COURSEP_ID").InnerText = strID.Split("|").GetValue(3)

                    objOutputXml = objbzParticipant.Delete(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        objXmlNode = objXmlDoc.DocumentElement.SelectSingleNode("Register/Details[@TR_COURSEP_ID='" + strID.Split("|").GetValue(3) + "']")
                        objXmlDoc.DocumentElement.SelectSingleNode("Register").RemoveChild(objXmlNode)
                        hdData.Value = objXmlDoc.OuterXml
                        If ViewState("SortName") IsNot Nothing Then
                            FillGrid(PageOperation.Sorting, ViewState("SortName"), ViewState("Desc"))
                        Else
                            FillGrid(PageOperation.Sorting)
                        End If
                        'Dim objNodeReader As XmlNodeReader
                        'Dim ds As New DataSet
                        'objNodeReader = New XmlNodeReader(objXmlDoc)
                        'ds.ReadXml(objNodeReader)
                        'gvRegisterTab.DataSource = ds.Tables("Details")
                        'gvRegisterTab.DataBind()
                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                Else
                    objXmlNode = objXmlDoc.DocumentElement.SelectSingleNode("Register/Details[@Type='" + strID.Split("|").GetValue(0) + "' and @Id='" + strID.Split("|").GetValue(1) + "']")
                    objXmlDoc.DocumentElement.SelectSingleNode("Register").RemoveChild(objXmlNode)
                    hdData.Value = objXmlDoc.OuterXml
                    If ViewState("SortName") IsNot Nothing Then
                        FillGrid(PageOperation.Sorting, ViewState("SortName"), ViewState("Desc"))
                    Else
                        FillGrid(PageOperation.Sorting)
                    End If
                    'Dim objNodeReader As XmlNodeReader
                    'Dim ds As New DataSet
                    'objNodeReader = New XmlNodeReader(objXmlDoc)
                    'ds.ReadXml(objNodeReader)
                    'gvRegisterTab.DataSource = ds.Tables("Details")
                    'gvRegisterTab.DataBind()
                End If
            End If
            If e.CommandName = "SelectX" Then
                strID = e.CommandArgument.ToString()
                objXmlDoc.LoadXml(hdData.Value)
                If strID.Split("|").GetValue(2) = "0" Then
                    '  AGENCYSTAFFID(TR_PR_AGENCY) EMPLOYEEID
                    objXmlNode = objXmlDoc.DocumentElement.SelectSingleNode("Register/Details[@TR_COURSEP_ID='" + strID.Split("|").GetValue(3) + "']")
                    If objXmlNode.Attributes("TR_PR_AGENCY").Value = "0" Then
                        Session("Action") = "U|" & objXmlNode.Attributes("AGENCYSTAFFID").Value & ""
                        ClientScript.RegisterClientScriptBlock(Me.GetType, "TR_E_Page", "<script language='javascript'>window.open('../setup/MSUP_Employee.aspx?PageLink=TR','TR_E_Page_Detail','left=10,width=900,toolbar=0,status=1,resizable=yes,scrollbars=1')</script>")
                    Else
                        ClientScript.RegisterClientScriptBlock(Me.GetType, "TR_S_Page", "<script language='javascript'>window.open('../TravelAgency/TAUP_AgencyStaff1.aspx?Action=U&PageLink=TR&ID=" + objED.Encrypt(objXmlNode.Attributes("AGENCYSTAFFID").Value) + "','TR_S_Page_Detail','toolbar=0,status=1,resizable=yes')</script>")
                    End If
                Else
                    If strID.Split("|").GetValue(0) = "E" Then
                        Session("Action") = "U|" & strID.Split("|").GetValue(1) & ""
                        ClientScript.RegisterClientScriptBlock(Me.GetType, "TR_E_Page", "<script language='javascript'>window.open('../setup/MSUP_Employee.aspx?PageLink=TR','TR_E_Page_Detail','left=10,width=900,toolbar=0,status=1,resizable=yes,scrollbars=1')</script>")
                    Else
                        ClientScript.RegisterClientScriptBlock(Me.GetType, "TR_S_Page", "<script language='javascript'>window.open('../TravelAgency/TAUP_AgencyStaff1.aspx?Action=U&PageLink=TR&ID=" + objED.Encrypt(strID.Split("|").GetValue(1)) + "','TR_S_Page_Detail','toolbar=0,status=1,resizable=yes')</script>")


                    End If

                    'objXmlNode = objXmlDoc.DocumentElement.SelectSingleNode("Register/Details[@Type='" + strID.Split("|").GetValue(0) + "' and @Id='" + strID.Split("|").GetValue(1) + "']")
                    'objXmlDoc.DocumentElement.SelectSingleNode("Register").RemoveChild(objXmlNode)
                    'hdData.Value = objXmlDoc.OuterXml
                    'Dim objNodeReader As XmlNodeReader
                    'Dim ds As New DataSet
                    'objNodeReader = New XmlNodeReader(objXmlDoc)
                    'ds.ReadXml(objNodeReader)
                    'gvRegisterTab.DataSource = ds.Tables("Details")
                    'gvRegisterTab.DataBind()

                End If
               
            End If
           If e.CommandName = "InvitationX" Then
                strID = e.CommandArgument.ToString()
                ClientScript.RegisterClientScriptBlock(Me.GetType, "TR_I_Page1", "<script language='javascript'>window.open('TRSR_Letter.aspx?Action=U&LetterType=Invitation&ID=" + objED.Encrypt(hdPageCourseSessionID.Value) & "&LCODE=" + objED.Encrypt(strID.Split("|").GetValue(0)) & "&TR_COURSEP_ID=" + objED.Encrypt(strID.Split("|").GetValue(1)) & "&StaffName=" + (strID.Split("|").GetValue(2)) + " ','TR_S_Page_Detail1','height=650,width=900,toolbar=0,scrollbars=1,resizable=yes')</script>")
            End If
            If e.CommandName = "DistinctionX" Then
                strID = e.CommandArgument.ToString()
                ClientScript.RegisterClientScriptBlock(Me.GetType, "TR_I_Page2", "<script language='javascript'>window.open('TRSR_Letter.aspx?Action=U&LetterType=Distinction&ID=" + objED.Encrypt(hdPageCourseSessionID.Value) & "&LCODE=" + objED.Encrypt(strID.Split("|").GetValue(0)) & "&TR_COURSEP_ID=" + objED.Encrypt(strID.Split("|").GetValue(1)) & "&StaffName=" + objED.Encrypt(strID.Split("|").GetValue(2)) + " ','TR_S_Page_Detail2','height=650,width=900,toolbar=0,scrollbars=1,resizable=yes')</script>")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvRegisterTab_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvRegisterTab.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
        Dim ddl As DropDownList
        ddl = CType(e.Row.Cells(4).Controls(1), DropDownList)
        If ddl IsNot Nothing Then
            objeAAMS.BindDropDown(ddl, "PARTCIPANTSTATUS", True)
        End If
        ddl.SelectedValue = DataBinder.Eval(e.Row.DataItem, "TR_PARTSTATUS_ID")
        If ddl.SelectedValue = "" Then
            'If ConfigurationManager.AppSettings("DEFAULT_PARTICIPANT_STATUS") IsNot Nothing Then
            If hdDefaultStatusID.Value <> "" Then
                ddl.SelectedValue = hdDefaultStatusID.Value ' ConfigurationManager.AppSettings("DEFAULT_PARTICIPANT_STATUS").ToString
            End If

        End If
        If ddl.SelectedItem.Text.ToLower = "certified" Or ddl.SelectedItem.Text.ToLower = "not certified" Then
            ' CType(e.Row.Cells(5).Controls(1), TextBox).CssClass = "textbox"
            ' CType(e.Row.Cells(5).Controls(1), TextBox).ReadOnly = False
            ' CType(e.Row.Cells(6).Controls(1), TextBox).CssClass = "textbox"
            'CType(e.Row.Cells(6).Controls(1), TextBox).ReadOnly = False
        Else
            ' CType(e.Row.Cells(5).Controls(1), TextBox).CssClass = "textboxgrey"
            ' CType(e.Row.Cells(5).Controls(1), TextBox).ReadOnly = True

        End If

        CType(e.Row.Cells(5).Controls(1), TextBox).Text = DataBinder.Eval(e.Row.DataItem, "TR_COURSEP_RESULT")
        CType(e.Row.Cells(5).Controls(1), TextBox).Attributes.Add("onblur", "validateResultRegister('" + CType(e.Row.Cells(6).Controls(1), TextBox).ClientID + "')")

        CType(e.Row.Cells(6).Controls(1), TextBox).Text = DataBinder.Eval(e.Row.DataItem, "TR_PRACTICAL_MARKS")
        CType(e.Row.Cells(6).Controls(1), TextBox).Attributes.Add("onblur", "validateResultRegister('" + CType(e.Row.Cells(6).Controls(1), TextBox).ClientID + "','" + CType(e.Row.Cells(6).Controls(3), HtmlInputHidden).ClientID + "')")
        CType(e.Row.Cells(6).Controls(1), TextBox).Attributes.Add("onkeypress", "validateResultRegister('" + CType(e.Row.Cells(6).Controls(1), TextBox).ClientID + "','" + CType(e.Row.Cells(6).Controls(3), HtmlInputHidden).ClientID + "')")



        Dim dclTheoryresult As Decimal = Val(DataBinder.Eval(e.Row.DataItem, "TR_COURSEP_RESULT"))
        Dim dclPracticalresult As Decimal = Val(DataBinder.Eval(e.Row.DataItem, "TR_PRACTICAL_MARKS"))

        CType(e.Row.Cells(6).Controls(3), HtmlInputHidden).Value = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TOTAL_PRACTICAL_MARKS"))
        Dim dclTotalTheoryresult As Decimal = Val(DataBinder.Eval(e.Row.DataItem, "TOTAL_THEORY_MARKS"))
        Dim dclTotalPracticalresult As Decimal = Val(DataBinder.Eval(e.Row.DataItem, "TOTAL_PRACTICAL_MARKS"))

        Dim dclPercentage As Decimal

        'If hdTotalMarks.Value <> "0" And (dclTheoryresult + dclPracticalresult) <> 0 Then

        '    'dclPercentage = ((dclTheoryresult + dclPracticalresult) / Convert.ToInt32(dclTotalTheoryresult + dclTotalPracticalresult)) * 100
        '    If (dclTotalTheoryresult + dclTotalPracticalresult) <> 0 Then
        '        dclPercentage = ((dclTheoryresult + dclPracticalresult) / Convert.ToInt32(dclTotalTheoryresult + dclTotalPracticalresult)) * 100
        '    Else
        '        dclPercentage = ((dclTheoryresult + dclPracticalresult) / Convert.ToInt32(hdTotalMarks.Value)) * 100
        '    End If


        '    dclPercentage = IIf(dclTheoryresult <> 0, dclPercentage, 0)
        '    CType(e.Row.Cells(7).Controls(1), TextBox).Text = IIf(dclPercentage <> 0, dclPercentage.ToString("00.00"), "")
        'ElseIf (1 = 1) Then

        'End If


        If (dclTotalTheoryresult + dclTotalPracticalresult) <> "0" And (dclTheoryresult + dclPracticalresult) <> 0 Then
            dclPercentage = ((dclTheoryresult + dclPracticalresult) / Convert.ToInt32(dclTotalTheoryresult + dclTotalPracticalresult)) * 100
            'dclPercentage = IIf(dclTheoryresult <> 0, dclPercentage, 0)
            CType(e.Row.Cells(7).Controls(1), TextBox).Text = IIf(dclPercentage <> 0, dclPercentage.ToString("00.00"), "")
        ElseIf (hdTotalMarks.Value <> "0" And (dclTheoryresult + dclPracticalresult) <> 0) Then
            dclPercentage = ((dclTheoryresult + dclPracticalresult) / Convert.ToInt32(hdTotalMarks.Value)) * 100
            ' dclPercentage = IIf(dclTheoryresult <> 0, dclPercentage, 0)
            CType(e.Row.Cells(7).Controls(1), TextBox).Text = IIf(dclPercentage <> 0, dclPercentage.ToString("00.00"), "")
        End If


        CType(e.Row.Cells(11).Controls(1), TextBox).Text = DataBinder.Eval(e.Row.DataItem, "TR_COURSEP_REMARKS")

        Dim lnkInvitation As LinkButton = CType(e.Row.Cells(12).FindControl("lnkInvitation"), LinkButton)
        Dim lnkDistinction As LinkButton = CType(e.Row.Cells(12).FindControl("lnkDistinction"), LinkButton)
        Dim lnkHistory As LinkButton = CType(e.Row.Cells(12).FindControl("lnkHistory"), LinkButton)
        Dim strTR_COURSEP_ID As String = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TR_COURSEP_ID")).Trim
        If strTR_COURSEP_ID = "" Then
            lnkInvitation.Enabled = False
            lnkDistinction.Enabled = False
            lnkHistory.Enabled = False
        Else 'If DataBinder.Eval(e.Row.DataItem, "TR_PR_AGENCY") = "1" Then 'This condition is for Staff and Basket (Previously it was asked)
            lnkHistory.Attributes.Add("onclick", "return ShowWorkOrderHistory('" + objED.Encrypt(strTR_COURSEP_ID) + "')")
            If ddl.SelectedItem.Text.ToUpper = "CONFIRMED" Then
                lnkInvitation.Enabled = True
            Else
                lnkInvitation.Enabled = False
            End If
            If ddl.SelectedItem.Text.ToUpper = "CERTIFIED" Then
                ' Dim dclTheoryresult As Decimal = Val(DataBinder.Eval(e.Row.DataItem, "TR_COURSEP_RESULT"))
                'Dim dclPracticalresult As Decimal = Val(DataBinder.Eval(e.Row.DataItem, "TR_PRACTICAL_MARKS"))
                'Dim dclPercentage As Decimal
                ' dclPercentage = ((dclTheoryresult + dclPracticalresult) / Convert.ToInt32(hdTotalMarks.Value)) * 100

                'Dim dclPercentage As Decimal = Val(DataBinder.Eval(e.Row.DataItem, "TR_PERCENTAGE_MARKS"))

                If dclPercentage >= 90 Then
                    lnkDistinction.Enabled = True
                Else
                    lnkDistinction.Enabled = False

                End If
            Else
                lnkDistinction.Enabled = False
            End If

            'Else
            '  lnkInvitation.Enabled = False 'disabled for employee (Previously it was asked)
            '  lnkDistinction.Enabled = False 'disabled for employee
        End If

        Dim btnInvitation As LinkButton
        Dim btnDistinction As LinkButton
        Dim btnDelete As LinkButton
        Dim btnSelect As LinkButton
        btnSelect = CType(e.Row.FindControl("lnkSelect"), LinkButton)
        btnDelete = CType(e.Row.FindControl("lnkDelete"), LinkButton)
        btnInvitation = CType(e.Row.FindControl("lnkInvitation"), LinkButton)
        btnDistinction = CType(e.Row.FindControl("lnkDistinction"), LinkButton)

        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Register Participant']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Register Participant']").Attributes("Value").Value)
            End If

            If strBuilder(3) = "0" Then
                btnDelete.Enabled = False
                btnInvitation.Enabled = False
                btnDistinction.Enabled = False

            End If

        End If
    End Sub

    Protected Sub gvRegisterTab_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvRegisterTab.Sorted

    End Sub

    Protected Sub gvRegisterTab_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvRegisterTab.Sorting
        Try
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ViewState("Desc") = "ASC"
            Else
                If ViewState("SortName") = SortName Then
                    If ViewState("Desc") = "ASC" Then
                        ViewState("Desc") = "DESC"
                    Else
                        ViewState("Desc") = "ASC"
                    End If
                Else
                    ViewState("SortName") = SortName
                    ViewState("Desc") = "ASC"
                End If
            End If
            BindXml()
            FillGrid(PageOperation.Sorting, SortName, ViewState("Desc"))
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Enum PageOperation
        Sorting = 0
    End Enum

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If hdData.Value <> "" Then
            Dim objOutputXml As New XmlDocument
            Dim objInPutXml As New XmlDocument
            Dim objExport As New ExportExcel
            Dim dclTheoryresult As Decimal
            Dim dclPracticalresult As Decimal
            Dim dclTotalTheoryresult As Decimal
            Dim dclTotalPracticalresult As Decimal
            Dim dclPercentage As Decimal
            Dim strArray() As String = {"Agency Name", "Participant Name", "Letter Send On", "Dis.Letter Send On", "Status", "T Marks", "P Marks", "% Scored", "Login", "Password", "Request Id", "Remarks"}
            Dim intArray() As Integer = {3, 4, 19, 21, 22, 7, 8, 9, 12, 14, 23, 16}
            objOutputXml.LoadXml(hdData.Value)
            objInPutXml.LoadXml("<Root></Root>")

            

            For Each objNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("//Details")
                dclTheoryresult = Val(objNode.Attributes("TR_COURSEP_RESULT").Value)
                dclPracticalresult = Val(objNode.Attributes("TR_PRACTICAL_MARKS").Value)
                dclTotalTheoryresult = Val(objNode.Attributes("TOTAL_THEORY_MARKS").Value)
                dclTotalPracticalresult = Val(objNode.Attributes("TOTAL_PRACTICAL_MARKS").Value)

                If (dclTotalTheoryresult + dclTotalPracticalresult) <> "0" And (dclTheoryresult + dclPracticalresult) <> 0 Then
                    dclPercentage = ((dclTheoryresult + dclPracticalresult) / Convert.ToInt32(dclTotalTheoryresult + dclTotalPracticalresult)) * 100
                    dclPercentage = IIf(dclTheoryresult <> 0, dclPercentage, 0)
                    objNode.Attributes("TR_PERCENTAGE_MARKS").Value = IIf(dclPercentage <> 0, dclPercentage.ToString("00.00"), "")
                End If
            Next

           


            Dim objDocFrag As XmlDocumentFragment
            objDocFrag = objInPutXml.CreateDocumentFragment()
            objDocFrag.InnerXml = objOutputXml.DocumentElement.SelectSingleNode("Register").InnerXml
            objInPutXml.DocumentElement.AppendChild(objDocFrag)


            objExport.ExportDetails(objInPutXml, "Details", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportRegisteredParticipant.xls")
        Else
            lblError.Text = "No Record found"
        End If

    End Sub
End Class


