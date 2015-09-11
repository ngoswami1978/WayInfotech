
Partial Class Training_TRSR_Letter
    Inherits System.Web.UI.Page

    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objED As New EncyrptDeCyrpt
    Dim strLetterType As String = ""
    Dim strAoffice As String = ""
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
#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            Session("PageName") = Request.Url.ToString()
            ' Checking security
            CheckSecurity()
            strLetterType = Request.QueryString("LetterType")
            If strLetterType = "Invitation" Then
                txtLetter.ReadOnly = False
                txtLetter.CssClass = "textbox"
            Else
                txtLetter.ReadOnly = True
                txtLetter.CssClass = "textboxgrey"
                ' txtReSendDate.ReadOnly = True
                'txtLetter.CssClass = "textboxgrey"

            End If

            txtLetter.CssClass = "displayNone"

            If Not IsPostBack Then
                btnEmail.Attributes.Add("onClick", "return ValidateFormTrainingLetter();")
                btnSave.Attributes.Add("onClick", "return ValidateSaveTrainingLetter();")
                btnPrint.Attributes.Add("onClick", "return TransferTextTrainingLetter();")
                Dim strDate As String = ""
                ' strDate = System.DateTime.Now.ToString("dd/MM/yyyy")
                '  strDate = System.DateTime.Now.ToString("dd MMM yy")
                strDate = System.DateTime.Now.ToString("dd MMM ") + "," + System.DateTime.Now.ToString("yy")

                ' txtStartDateFrom.Text = strDate
                ' txtReSendDate.Text = strDate
                '  BindDropDown(ddlAuth, "AurizedSignatory", False)
                ' AgencyView()
                ViewCourseSession()
                ViewParRecords()
                BindDropDown(ddlAuth, "AurizedSignatory", False)


                If hdLetterId.Value <> "" And strLetterType = "Invitation" Then
                    '   Get Saved Invitation Letter.
                    LetterView()
                ElseIf hdLetterId.Value = "" And strLetterType = "Invitation" Then
                    '   Get Default Invitation Letter.
                    GetTemplateInvitation()
                ElseIf hdLetterId.Value <> "" And strLetterType = "Distinction" Then
                    '   Get Saved Distinction Letter.
                    LetterDisTinctionView()
                ElseIf hdLetterId.Value = "" And strLetterType = "Distinction" Then
                    '   Get Default Distinction Letter.
                    GetTemplateDistinction()
                End If
            End If




        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        txtLetter.Text = hdnmsg.Value ' hdRptLetter.Value

        If Request.QueryString("LetterType") = "Invitation" Then
            SaveInvitation(PageOperation.Save)
        Else
            SaveDestinction(PageOperation.Save)
        End If
    End Sub
#End Region

#Region "btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click"
    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try

            txtLetter.Text = hdnmsg.Value ' hdRptLetter.Value
            If Request.QueryString("LetterType") = "Invitation" Then
                SaveInvitation(PageOperation.Print)
            Else
                SaveDestinction(PageOperation.Print)
            End If
            '  Response.Redirect("/AAMS/RPSR_ReportShow.aspx?Case=Letter")
            '  ClientScript.RegisterStartupScript(Me.GetType(),"OpenLetter", "<script language='javascript'>window.open('../RPSR_ReportShow.aspx?Case=Letter',null,'toolbar=0,status=1,resizable=yes')</script>")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click

        Response.Redirect("TRSR_Letter.aspx?" + Request.QueryString.ToString())
        'Response.Clear()
        'Response.AddHeader("content-disposition", "attachment;filename=test.doc")
        'Response.Charset = ""
        'Response.ContentType = "application/vnd.doc"
        'Dim stringWrite As New System.IO.StringWriter
        ''  System.IO.StringWriter(stringWrite = New System.IO.StringWriter())
        'Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)
        '' System.Web.UI.HtmlTextWriter(htmlWrite = New HtmlTextWriter(stringWrite))
        'txtLetter.RenderControl(htmlWrite)
        'Response.Write(stringWrite.ToString())
        'Response.End()


    End Sub
#End Region

#Region "ViewRecords()"
    Sub ViewRecords()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzParticipantsBasket As New AAMS.bizTraining.bzParticipantsBasket
        objInputXml.LoadXml("<TR_VIEWBASKET_INPUT><TR_BASKET_REQUEST_ID /></TR_VIEWBASKET_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("TR_BASKET_REQUEST_ID").InnerText = hdPageBasketID.Value

        objOutputXml = objbzParticipantsBasket.View(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            With objOutputXml.DocumentElement.SelectSingleNode("BASKET")
                '<TR_VIEWBASKET_OUTPUT><BASKET TR_BASKET_REQUEST_ID='' TR_BASKET_DATE='' TR_BASKET_REMARKS=''
                ' TR_COURSE_ID='' EMPLOYEEID=''  LCODE='' AOFFICE='' AGENCYSTAFFID='' RECEIVEDFROMWEB='' PREFERREDDATE='' 
                'AGENCYNAME='' ADDRESS='' CITY='' COUNTRY='' PHONE='' FAX='' OFFICEID='' ONLINESTATUS='' EMPLOYEENAME=''
                ' STAFFNAME='' PRIORITY='' /><Errors Status=''><Error Code='' Description='' /></Errors></TR_VIEWBASKET_OUTPUT>

                txtAgencyName.Text = .Attributes("AGENCYNAME").Value
                txtAddress.Text = .Attributes("ADDRESS").Value
                txtCountry.Text = .Attributes("COUNTRY").Value
                txtCity.Text = .Attributes("CITY").Value
                txtPhone.Text = .Attributes("PHONE").Value
                txtFax.Text = .Attributes("FAX").Value
                txtOnlineStatus.Text = .Attributes("ONLINESTATUS").Value
                txtPriority.Text = .Attributes("PRIORITY").Value
                txtAgencyStaff.Text = .Attributes("STAFFNAME").Value

            End With
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
#End Region

#Region "BindDropDown(ByVal drpDownList As DropDownList, ByVal strType As String, ByVal bolSelect As Boolean)"
    Public Sub BindDropDown(ByVal drpDownList As DropDownList, ByVal strType As String, ByVal bolSelect As Boolean)
        Try
            Dim objInputAurizedSignatoryXml, objSecurityXml, objOutputAurizedSignatoryXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim objbzDocEmployee As New AAMS.bizMaster.bzDocEmployee
            Dim ds As New DataSet
            'Dim strAoffice As String = ""

            'If Session("Security") IsNot Nothing Then
            '    objSecurityXml.LoadXml(Session("Security"))
            '    If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
            '        strAoffice = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
            '    Else
            '        strAoffice = ""
            '    End If
            'End If
            drpDownList.Items.Clear()
            Select Case strType
                Case "AurizedSignatory"
                    objInputAurizedSignatoryXml.LoadXml("<DOCUMENTEMPLOYEE_INPUT><DOCUMENT Aoffice='' /></DOCUMENTEMPLOYEE_INPUT>")
                    objInputAurizedSignatoryXml.DocumentElement.SelectSingleNode("DOCUMENT").Attributes("Aoffice").InnerText = strAoffice

                    'Here Back end Method Call
                    objOutputAurizedSignatoryXml = objbzDocEmployee.List(objInputAurizedSignatoryXml)

                    If objOutputAurizedSignatoryXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        objXmlReader = New XmlNodeReader(objOutputAurizedSignatoryXml)
                        ds.ReadXml(objXmlReader)
                        drpDownList.DataSource = ds.Tables("DOCUMENT")
                        drpDownList.DataTextField = "EmployeeName"
                        drpDownList.DataValueField = "DESIGNATION"
                        drpDownList.DataBind()
                        If ds.Tables("DOCUMENT").Rows.Count > 0 Then
                            ddlAuth.SelectedIndex = 0
                        End If
                    End If
            End Select
            If bolSelect = True Then
                drpDownList.Items.Insert(0, New ListItem("---Select One---", ""))
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
#End Region

#Region "GetTemplate()"
    Private Sub GetTemplateDistinction()

        Try
            Dim strbody As String = ""
            Dim objInputTempXml, objOutputTempXml, objSecurityXml As New XmlDocument
            Dim objbzbzTemplate As New AAMS.bizMaster.bzTemplate
            Dim ds As New DataSet
            '  Dim strAoffice As String = ""

            'If Session("Security") IsNot Nothing Then
            '    objSecurityXml.LoadXml(Session("Security"))
            '    If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
            '        strAoffice = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
            '    Else
            '        strAoffice = ""
            '    End If
            'End If

            '#####################################################################
            ' Getting Template 

            objInputTempXml.LoadXml("<HD_DOCUMENTTEMPLATE_INPUT><TEMPLATES TemplateName='' AOFFICE ='' AIRLINECODE ='' /></HD_DOCUMENTTEMPLATE_INPUT>")
            objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("TemplateName").Value() = "Distinction Letter"
            objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("AOFFICE").Value() = strAoffice
            objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("AIRLINECODE").Value() = ""
            objOutputTempXml = objbzbzTemplate.GetDocumentTemplate(objInputTempXml)
            If objOutputTempXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                '  hdTemplateVersion.Value = objOutputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("T_G_DOC_VERSION").Value
                strbody = objOutputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("BDRTemplate").Value
                '###############################################################################
                '@ Now find the value of Body parts on the basis of Lcode
                Dim strDate As String = ""
                'strDate = System.DateTime.Now().ToString("dd MMM yy")
                strDate = System.DateTime.Now.ToString("dd MMM ") + "," + System.DateTime.Now.ToString("yy")
                strbody = strbody.Replace("[[DATE]]", " " & strDate)



                If txtAgencyName.Text.Trim.Length > 0 Then
                    strbody = strbody.Replace("[[AGENCYNAME]]", " " & txtAgencyName.Text)
                End If

                ' Incase of employee
                If hdAgency.Value.Trim.Length > 0 Then
                    strbody = strbody.Replace("[[AGENCYNAME]]", " " & hdAgency.Value)
                End If


                If txtAgencyStaff.Text.Trim.Length > 0 Then
                    strbody = strbody.Replace("[[AGENCYSTAFFNAME]]", " " & hdStaffName.Value)
                End If

                ' In case of employee. 
                If hdStaffName.Value.Trim.Length > 0 Then
                    strbody = strbody.Replace("[[AGENCYSTAFFNAME]]", " " & hdStaffName.Value)
                End If

                If ddlAuth.SelectedValue.Length > 0 Then
                    strbody = strbody.Replace("[[DESIGNATION]]", " " & ddlAuth.SelectedValue)
                End If

                If ddlAuth.SelectedValue.Length > 0 Then
                    strbody = strbody.Replace("[[SIGNATURE]]", " " & ddlAuth.SelectedItem.Text)
                End If

                If hdCourseName.Value.Length > 0 Then
                    strbody = strbody.Replace("[[COURSENAME]]", " " & hdCourseName.Value)
                End If

                If hdResult.Value.Length > 0 Then
                    Dim dclMarksobtained As Decimal = Val(hdResult.Value)
                    Dim dclPercentage As Decimal
                    dclPercentage = (dclMarksobtained / Val(hdTotalMarks.Value)) * 100
                    strbody = strbody.Replace("[[RESULT]]", " " & dclPercentage.ToString("00.00"))
                Else
                    strbody = strbody.Replace("[[RESULT]]", "  ")
                End If


                txtLetter.Text = strbody
                Session("Letter") = txtLetter.Text

            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Private Sub GetTemplateInvitation()

        Try
            Dim strbody As String = ""
            Dim objInputTempXml, objOutputTempXml, objSecurityXml As New XmlDocument
            Dim objbzbzTemplate As New AAMS.bizMaster.bzTemplate
            Dim ds As New DataSet
            '  Dim strAoffice As String = ""

            'If Session("Security") IsNot Nothing Then
            '    objSecurityXml.LoadXml(Session("Security"))
            '    If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
            '        strAoffice = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
            '    Else
            '        strAoffice = ""
            '    End If
            'End If

            '#####################################################################
            ' Getting Template 

            objInputTempXml.LoadXml("<HD_DOCUMENTTEMPLATE_INPUT><TEMPLATES TemplateName='' AOFFICE ='' AIRLINECODE ='' /></HD_DOCUMENTTEMPLATE_INPUT>")
            objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("TemplateName").Value() = "Invitation Letter"
            objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("AOFFICE").Value() = strAoffice
            objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("AIRLINECODE").Value() = ""
            objOutputTempXml = objbzbzTemplate.GetDocumentTemplate(objInputTempXml)
            Session("Letter") = objOutputTempXml.OuterXml
            If objOutputTempXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                '  hdTemplateVersion.Value = objOutputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("T_G_DOC_VERSION").Value
                strbody = objOutputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("BDRTemplate").Value
                '###############################################################################
                ' Replacing following values 
                '[[AGENCYSTAFFNAME]]
                '[[AGENCYNAME]]
                '[[AGENCYADDRESS]]
                '[[AGENCYCITY]]
                '[[COURSENAME]]
                '[[FROMDATE]]
                '[[STARTTIME]]
                '[[ENDTIME]]
                '[[ENDDATE]]
                '[[SIGNATURE]]
                '[[DESIGNATION]]
                '[[TR_CLOCATION_NAME]]
                '[[COUNTRY_CODE]]
                '[[REGISTERONLINE_SITE_ADDRESS]]



                Dim strDate As String = ""
                ' strDate = System.DateTime.Now().ToString("dd MMM yy")

                strDate = System.DateTime.Now.ToString("dd MMM ") + "," + System.DateTime.Now.ToString("yy")

                strbody = strbody.Replace("[[DATE]]", " " & strDate)

                If txtAgencyName.Text.Trim.Length > 0 Then
                    strbody = strbody.Replace("[[AGENCYNAME]]", " " & txtAgencyName.Text)
                End If

                ' Incase of employee
                If hdAgency.Value.Trim.Length > 0 Then
                    strbody = strbody.Replace("[[AGENCYNAME]]", " " & hdAgency.Value)
                End If

                ' If Agency Contact Person Present Then Replace AgencyStaff Name With Contact Person 
                ' Otherwise AgencyStaff replace With Participant Name.
                If txtAgencyStaff.Text.Trim.Length > 0 Then
                    strbody = strbody.Replace("[[AGENCYSTAFFNAME]]", " " & txtAgencyStaff.Text)
                Else
                    If hdStaffName.Value.Trim.Length > 0 Then
                        strbody = strbody.Replace("[[AGENCYSTAFFNAME]]", " " & hdStaffName.Value)
                    End If

                End If

                If txtAddress.Text.Trim.Length > 0 Then
                    strbody = strbody.Replace("[[AGENCYADDRESS]]", " " & txtAddress.Text)
                End If

                ' Incase of employee.
                If hdEmpAddress.Value.Trim.Length > 0 Then
                    strbody = strbody.Replace("[[AGENCYADDRESS]]", " " & hdEmpAddress.Value)
                End If

                If txtCity.Text.Trim.Length > 0 Then
                    strbody = strbody.Replace("[[AGENCYCITY]]", " " & txtCity.Text)
                End If
                ' Incase of employee.
                If hdEmpCity.Value.Trim.Length > 0 Then
                    strbody = strbody.Replace("[[AGENCYCITY]]", " " & hdEmpCity.Value)
                End If

                If ddlAuth.SelectedValue.Length > 0 Then
                    strbody = strbody.Replace("[[DESIGNATION]]", " " & ddlAuth.SelectedValue)
                End If

                If ddlAuth.SelectedValue.Length > 0 Then
                    strbody = strbody.Replace("[[SIGNATURE]]", " " & ddlAuth.SelectedItem.Text)
                End If

                If hdCourseName.Value.Length > 0 Then
                    strbody = strbody.Replace("[[COURSENAME]]", " " & hdCourseName.Value)
                End If

                If hdStartDate.Value.Length > 0 Then
                    strbody = strbody.Replace("[[FROMDATE]]", " " & hdStartDate.Value)
                End If

                If hdEndDate.Value.Length > 0 Then
                    strbody = strbody.Replace("[[ENDDATE]]", " " & hdEndDate.Value)
                End If

                If hdStartTime.Value.Length > 0 Then
                    strbody = strbody.Replace("[[STARTTIME]]", " " & hdStartTime.Value)
                End If

                If hdEndTime.Value.Length > 0 Then
                    strbody = strbody.Replace("[[ENDTIME]]", " " & hdEndTime.Value)
                End If

                If hdTrainingLocation.Value.Length > 0 Then
                    strbody = strbody.Replace("[[LOCATION]]", " " & hdTrainingLocation.Value)
                End If

                If hdCountryCode.Value.Length > 0 Then
                    strbody = strbody.Replace("[[COUNTRY_CODE]]", Trim(hdCountryCode.Value & ""))
                End If

                If hdRegister_online_Site.Value.Length > 0 Then
                    strbody = strbody.Replace("[[REGISTERONLINE_SITE_ADDRESS]]", " " & hdRegister_online_Site.Value)
                End If

                txtLetter.Text = strbody
                Session("Letter") = txtLetter.Text


            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

#End Region

#Region "AgencyView()"
    Private Sub AgencyView()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
        Try
            objInputXml.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerXml = objED.Decrypt(Request.QueryString("LCODE"))   '"137" 
            objOutputXml = objbzAgency.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXml.DocumentElement.SelectSingleNode("Agency")
                    txtAgencyName.Text = .Attributes("NAME").Value()
                    txtAddress.Text = .Attributes("ADDRESS").Value() + " " + .Attributes("ADDRESS1").Value()
                    txtCountry.Text = .Attributes("COUNTRY").Value()
                    txtCity.Text = .Attributes("CITY").Value()
                    txtPhone.Text = .Attributes("PHONE").Value()
                    txtFax.Text = .Attributes("FAX").Value()
                    txtOnlineStatus.Text = .Attributes("ONLINE_STATUS").Value
                    ' txtPriority.Text = .Attributes("PRIORITYID").Value
                    '  txtAgencyStaff.Text = Request.QueryString("StaffName")
                    'txtAgencyStaff.Text = "John"

                End With

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            objbzAgency = Nothing
        End Try
    End Sub
#End Region

#Region "ContactPersonOfAgency()"
    Private Sub ContactPersonOfAgency()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgencyStaff
        Try
            objInputXml.LoadXml("<UP_GETAGENCYSTAFFDETAILS_INPUT><LCODE></LCODE></UP_GETAGENCYSTAFFDETAILS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerXml = objED.Decrypt(Request.QueryString("LCODE"))   '"137" 
            objOutputXml = objbzAgency.GetAgencyStaffContactPersonDetail(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF")
                    txtAgencyStaff.Text = .Attributes("STAFFNAME").Value()
                    txtEmail.Text = .Attributes("EMAILID").Value()
                End With
            Else
                If objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value <> "No Record Found" Then
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If

            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            objbzAgency = Nothing
        End Try
    End Sub
#End Region

#Region "ViewCourseSession()"
    Private Sub ViewCourseSession()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzCourseSession As New AAMS.bizTraining.bzCourseSession
        Try
            objInputXml.LoadXml("<TR_VIEWCOURSES_INPUT><TR_COURSES_ID /></TR_VIEWCOURSES_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("TR_COURSES_ID").InnerXml = objED.Decrypt(Request.QueryString("ID")) '"7" 
            objOutputXml = objbzCourseSession.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXml.DocumentElement.SelectSingleNode("COURSES")
                    hdTrainingLocation.Value = .Attributes("TR_CLOCATION_NAME").Value()
                    hdCountryCode.Value = .Attributes("COUNTRY_CODE").Value()
                    hdRegister_online_Site.Value = .Attributes("REGISTERONLINE_SITE_ADDRESS").Value()
                    hdCourseName.Value = .Attributes("TR_COURSE_NAME").Value()

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
                        hdStartDate.Value = objeAAMS.GetDateFormat(strDate, "MM/dd/yyyy", "dd/MM/yyyy", "/")
                        hdStartTime.Value = .Attributes("TR_COURSES_START_TIME").Value.Substring(0, 2) & ":" & .Attributes("TR_COURSES_START_TIME").Value.Substring(2, 2)
                        If .Attributes("TR_COURSES_END_TIME").Value <> "" Then
                            hdEndTime.Value = .Attributes("TR_COURSES_END_TIME").Value.Substring(0, 2) & ":" & .Attributes("TR_COURSES_END_TIME").Value.Substring(2, 2)
                        End If
                    End If

                    Dim strEndDate As String = .Attributes("TR_COURSES_END_DATE").Value
                    If strEndDate <> "" Then
                        Dim day As String = strEndDate.Split("/").GetValue(0)
                        Dim month As String = strEndDate.Split("/").GetValue(1)
                        If day.Length = 1 Then
                            day = "0" & day
                        End If
                        If month.Length = 1 Then
                            month = "0" & month
                        End If
                        strEndDate = day & "/" & month & "/" & strEndDate.Split("/").GetValue(2)
                        hdEndDate.Value = objeAAMS.GetDateFormat(strEndDate, "MM/dd/yyyy", "dd/MM/yyyy", "/")

                    End If

                    'Start Code to store Total marks for the test
                    hdTotalPracticalMarks.Value = Val(.Attributes("PRACTICAL_MARKS").Value)
                    hdTotalTheoryMarks.Value = Val(.Attributes("THEORY_MARKS").Value)
                    ' hdTotalMarks.Value = (Val(.Attributes("PRACTICAL_MARKS").Value) + Val(.Attributes("THEORY_MARKS").Value)).ToString
                    ' txtTotalMarks.Text = hdTotalMarks.Value
                    'End Code
                    ' Getting Aoffice For Signatory
                    If Not .Attributes("AOFFICE") Is Nothing Then
                        strAoffice = .Attributes("AOFFICE").Value
                    End If

                End With
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            objbzCourseSession = Nothing
        End Try
    End Sub
#End Region

    Private Sub ViewParRecords()

        '<TR_VIEWPARTICIPANT_INPUT>
        ' <TR_COURSES_ID />
        '</TR_VIEWPARTICIPANT_INPUT>

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzParticipant As New AAMS.bizTraining.bzParticipant
        objInputXml.LoadXml("<TR_VIEWPARTICIPANT_INPUT> <TR_COURSES_ID /></TR_VIEWPARTICIPANT_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("TR_COURSES_ID").InnerText = objED.Decrypt(Request.QueryString("ID"))

        objOutputXml = objbzParticipant.View(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            Dim objNodeList As XmlNodeList
            Dim objNode As XmlNode

            '<TR_VIEWPARTICIPANT_OUTPUT>
            '	<Errors Status="FALSE">
            '		<Error Code="" Description=""/>
            '	</Errors>
            '	<PARTICIPANT TR_COURSEP_ID="165" TR_COURSEP_RESULT="15" LCODE="137" AGENCYNAME="DEL" AGENCYSTAFFID="202524" PARTICIPANTNAME="Amit Kumar" TR_CLETTER_DATESEND="" TR_CLETTER_DATERESEND="14-Jul-2008" TR_PARTSTATUS_ID="1" TR_PARTSTATUS_NAME="certified"
            '	             TR_COURSEP_LOGIN="165" TR_COURSEP_PWD="64752" TR_COURSEP_REMARKS="" TR_COURSEP_ACK="False" TR_PCLETTER_ID="14" TR_CLETTER_ID="88" EMPLOYEEID="" TR_PR_AGENCY="False" EMAILID="rchawla@amadeus.co.in"
            '	             AGENCYADDRESS="Plot No.7, Local Shopping Centre Sector -C, Pocket 6  7 Vasant Kunj" AGENCYCITY="New Delhi" TR_PRACTICAL_MARKS=""/>
            '</TR_VIEWPARTICIPANT_OUTPUT>

            Dim strId As String = ""
            strId = objED.Decrypt(Request.QueryString("TR_COURSEP_ID"))
            hdCourseParID.Value = strId
            objNode = objOutputXml.DocumentElement.SelectSingleNode("PARTICIPANT[@TR_COURSEP_ID='" + strId + "']")
            'If Not objNode.Attributes("AOFFICE") Is Nothing Then
            '    strAoffice = objNode.Attributes("AOFFICE").Value
            'End If

            '   Code Modified For Employee Start
            If objNode.Attributes("TR_PR_AGENCY").Value = "True" Then
                AgencyView()
                pnlAgency.Visible = True
            Else
                ' Hiding Panel Of Agency.
                pnlAgency.Visible = False
                hdAgency.Value = objNode.Attributes("AGENCYNAME").Value
                hdEmpAddress.Value = objNode.Attributes("AGENCYADDRESS").Value
                hdEmpCity.Value = objNode.Attributes("AGENCYCITY").Value

            End If

            '   Code Modified For Employee End
            hdStaffId.Value = objNode.Attributes("AGENCYSTAFFID").Value
            hdResult.Value = Val(objNode.Attributes("TR_COURSEP_RESULT").Value) + Val(objNode.Attributes("TR_PRACTICAL_MARKS").Value)
            hdTotalMarks.Value = Val(objNode.Attributes("TOTAL_THEORY_MARKS").Value) + Val(objNode.Attributes("TOTAL_PRACTICAL_MARKS").Value)
            ' Incase of Distiction letter AgencyStaff Name Display Participant Name.
            If Request.QueryString("LetterType") = "Distinction" Then
                hdStaffName.Value = objNode.Attributes("PARTICIPANTNAME").Value
                txtAgencyStaff.Text = objNode.Attributes("PARTICIPANTNAME").Value

                'If objNode.Attributes("EMAILID") IsNot Nothing Then
                '    txtEmail.Text = objNode.Attributes("EMAILID").Value
                'End If
                ' Calling for Email ID Of Agency Staff.

                If objNode.Attributes("TR_PR_AGENCY").Value = "True" Then
                    ViewAgencyStaff()
                Else
                    'Calling Function For Getting Employee Email ID. 
                    'txtEmail.Text = objNode.Attributes("EMAILID").Value

                    'txtEmail.Text = ""
                    GetEmailIdOfEmployee()

                End If

            ElseIf Request.QueryString("LetterType") = "Invitation" Then
                hdStaffName.Value = objNode.Attributes("CONTACT_PERSON").Value
                txtAgencyStaff.Text = objNode.Attributes("CONTACT_PERSON").Value
                txtEmail.Text = objNode.Attributes("EMAILID").Value
                ' If Participant Comes from Agency Then mail to Agency Contact Person.
                'Setting Agency contact person and email id In other case Mail to Participant Email ID.

                ' ContactPersonOfAgency()
                ' EMPLOYEEID<>"" means Participant from Basket .
                'If objNode.Attributes("EMPLOYEEID").Value <> "" Then
                '    If objNode.Attributes("EMAILID") IsNot Nothing Then
                '        hdStaffName.Value = objNode.Attributes("PARTICIPANTNAME").Value
                '        txtAgencyStaff.Text = objNode.Attributes("PARTICIPANTNAME").Value
                '        txtEmail.Text = objNode.Attributes("EMAILID").Value
                '    End If
                'End If
                'If Not objNode.Attributes("EMAILID") Is Nothing Then
                '    txtEmail.Text = objNode.Attributes("EMAILID").Value
                'End If

            End If


            If Request.QueryString("LetterType") = "Invitation" Then
                hdLetterId.Value = objNode.Attributes("TR_CLETTER_ID").Value
            Else
                hdLetterId.Value = objNode.Attributes("TR_PCLETTER_ID").Value
            End If



        ElseIf (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value <> "No Record Found!") Then
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

        End If

    End Sub

    Private Sub LetterView()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzCourseLetter As New AAMS.bizTraining.bzCourseLetter
        Try
            objInputXml.LoadXml("<TR_VIEWCLETTER_INPUT><TR_CLETTER_ID /></TR_VIEWCLETTER_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("TR_CLETTER_ID").InnerXml = hdLetterId.Value
            objOutputXml = objbzCourseLetter.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXml.DocumentElement.SelectSingleNode("DETAILS")
                    txtLetter.Text = .Attributes("TR_CLETTER").Value()
                    txtStartDateFrom.Text = .Attributes("TR_CLETTER_DATESEND").Value()
                    txtReSendDate.Text = .Attributes("TR_CLETTER_DATERESEND").Value()
                End With
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            objbzCourseLetter = Nothing
        End Try
    End Sub

    Private Sub SaveInvitation(Optional ByVal strButton As Integer = 3)
        Try
            Dim objInputXml As New XmlDocument
            Dim objOutputXml As New XmlDocument
            Dim objCourseLetter As New AAMS.bizTraining.bzCourseLetter
            ' Load Input Xml.
            objInputXml.LoadXml("<TR_UPDATECLETTER_INPUT><DETAILS TR_CLETTER_ID='' TR_CLETTER='' TR_CLETTER_DATESEND='' EMPLOYEE_ID='' TR_COURSES_ID='' LCODE='' TR_CLETTER_DATERESEND='' MODE='' TYPE='S' EmailID='' TR_COURSEP_ID='' /></TR_UPDATECLETTER_INPUT>")
            If (hdLetterId.Value <> "") Then
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("TR_CLETTER_ID").Value = hdLetterId.Value
            Else
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("TR_CLETTER_ID").Value = ""
            End If
            objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("TR_CLETTER").Value = hdnmsg.Value ' hdRptLetter.Value hdRptLetter.Value ' txtLetter.Text
            objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("EMPLOYEE_ID").Value = hdStaffId.Value
            objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("TR_COURSES_ID").Value = objED.Decrypt(Request.QueryString("ID"))
            objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("LCODE").Value = objED.Decrypt(Request.QueryString("LCODE"))
            objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("TR_COURSEP_ID").Value = hdCourseParID.Value

            'If txtStartDateFrom.Text <> "" Then
            '    objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("TR_CLETTER_DATESEND").Value = objeAAMS.GetDateFormat(txtStartDateFrom.Text, "dd/MM/yyyy", "MM/dd/yyyy", "/")
            'End If
            'If txtReSendDate.Text <> "" Then
            '    objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("TR_CLETTER_DATERESEND").Value = objeAAMS.GetDateFormat(txtReSendDate.Text, "dd/MM/yyyy", "MM/dd/yyyy", "/")
            'End If



            ' Calling update method for update.

            If PageOperation.Email = strButton Then
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("MODE").Value = "E"
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("EmailID").Value = txtEmail.Text
            End If

            If PageOperation.Print = strButton Then
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("MODE").Value = "P"
            End If

            objOutputXml = objCourseLetter.Update(objInputXml)
            '   Checking error status. 
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ' txtStartDateFrom.Text = objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("TR_CLETTER_DATESEND").Value.Trim()
                ' txtReSendDate.Text = objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("TR_CLETTER_DATERESEND").Value.Trim()
                'If (Request.QueryString("Action").ToString().ToUpper() = "U" Or hdLetterId.Value <> "") Then
                If (hdLetterId.Value <> "") Then
                    hdLetterId.Value = objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("TR_CLETTER_ID").Value.Trim()
                    If PageOperation.Save = strButton Then
                        lblError.Text = objeAAMSMessage.messUpdate
                    End If

                Else
                    hdLetterId.Value = objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("TR_CLETTER_ID").Value.Trim()
                    If PageOperation.Save = strButton Then
                        lblError.Text = objeAAMSMessage.messInsert
                    End If
                End If
                If strButton = PageOperation.Print Then
                    ' Session("Letter") = txtLetter.Text
                    Session("Letter") = hdRptLetter.Value
                    ClientScript.RegisterClientScriptBlock(Me.GetType, "TR_I_Page5", "<script language='javascript'>window.open('../RPSR_ReportShow.aspx?Case=Letter','TR_S_Page_Detail21','height=650,width=900,toolbar=0,scrollbars=1,resizable=yes')</script>")
                End If
                If PageOperation.Email = strButton Then
                    lblError.Text = "Emailed Successfully"
                End If
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub SaveDestinction(Optional ByVal strButton As Integer = 0)
        Try
            Dim objInputXml As New XmlDocument
            Dim objOutputXml As New XmlDocument
            Dim objCourseParLetter As New AAMS.bizTraining.bzParticipantLetter
            ' Load Input Xml.
            objInputXml.LoadXml("<TR_UPDATEPCLETTER_INPUT><DETAILS TR_PCLETTER_ID='' TR_PCLETTER='' TR_PCLETTER_DATESEND='' EMPLOYEE_ID='' TR_COURSEP_ID='' MODE='' TYPE='S' EmailID='' /></TR_UPDATEPCLETTER_INPUT>")
            If (hdLetterId.Value <> "") Then
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("TR_PCLETTER_ID").Value = hdLetterId.Value
            Else
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("TR_PCLETTER_ID").Value = ""
            End If
            objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("TR_PCLETTER").Value = hdnmsg.Value ' hdRptLetter.Value hdRptLetter.Value ' txtLetter.Text
            objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("EMPLOYEE_ID").Value = hdStaffId.Value
            objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("TR_COURSEP_ID").Value = objED.Decrypt(Request.QueryString("TR_COURSEP_ID"))
            '  objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("LCODE").Value = Request.QueryString("LCODE")

            'If txtStartDateFrom.Text <> "" Then
            '    objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("TR_PCLETTER_DATESEND").Value = objeAAMS.GetDateFormat(txtStartDateFrom.Text, "dd/MM/yyyy", "MM/dd/yyyy", "/")
            'End If
            'If txtReSendDate.Text <> "" Then
            '    objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("TR_PCLETTER_DATESEND").Value = objeAAMS.GetDateFormat(txtReSendDate.Text, "dd/MM/yyyy", "MM/dd/yyyy", "/")
            'End If



            ' Calling update method for update.
            If PageOperation.Email = strButton Then
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("MODE").Value = "E"
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("EmailID").Value = txtEmail.Text
            End If

            If PageOperation.Print = strButton Then
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("MODE").Value = "P"
            End If

            objOutputXml = objCourseParLetter.Update(objInputXml)
            '   Checking error status. 
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                'If (Request.QueryString("Action").ToString().ToUpper() = "U" Or hdLetterId.Value <> "") Then
                If (hdLetterId.Value <> "") Then
                    hdLetterId.Value = objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("TR_PCLETTER_ID").Value.Trim()
                    If PageOperation.Save = strButton Then
                        lblError.Text = objeAAMSMessage.messUpdate ' Record updated successfully.
                    End If
                Else
                    hdLetterId.Value = objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("TR_PCLETTER_ID").Value.Trim()
                    If PageOperation.Save Then
                        lblError.Text = objeAAMSMessage.messInsert ' Record inserted successfully.
                    End If
                End If
                If strButton = PageOperation.Print Then
                    '  Session("Letter") = txtLetter.Text
                    Session("Letter") = hdRptLetter.Value
                    ClientScript.RegisterClientScriptBlock(Me.GetType, "TR_I_Page5", "<script language='javascript'>window.open('../RPSR_ReportShow.aspx?Case=Letter','TR_S_Page_Detail21','height=650,width=900,toolbar=0,scrollbars=1,resizable=yes')</script>")
                End If
                If PageOperation.Email = strButton Then
                    lblError.Text = "Emailed Successfully"
                End If
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub LetterDisTinctionView()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzParLetter As New AAMS.bizTraining.bzParticipantLetter
        Try
            objInputXml.LoadXml("<TR_VIEWPCLETTER_INPUT><TR_PCLETTER_ID /></TR_VIEWPCLETTER_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("TR_PCLETTER_ID").InnerXml = hdLetterId.Value
            objOutputXml = objbzParLetter.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXml.DocumentElement.SelectSingleNode("DETAILS")
                    txtLetter.Text = .Attributes("TR_PCLETTER").Value()
                    txtStartDateFrom.Text = .Attributes("TR_PCLETTER_DATESEND").Value()
                    txtReSendDate.Text = .Attributes("TR_PCLETTER_DATERESEND").Value()

                    
                End With
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            objbzParLetter = Nothing
        End Try
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Course Session']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Course Session']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                    btnPrint.Enabled = False
                End If

            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region


    Protected Sub btnEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEmail.Click
        Try
            txtLetter.Text = hdnmsg.Value ' hdRptLetter.Value
            If Request.QueryString("LetterType") = "Invitation" Then
                SaveInvitation(PageOperation.Email)
            Else
                SaveDestinction(PageOperation.Email)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub 
    Protected Enum PageOperation
        Email = 1
        Print = 2
        Save = 3

    End Enum

#Region " ViewAgencyStaff Procedure"
    Private Sub ViewAgencyStaff()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objAgencyInputXml, objAgencyOutputXml As New XmlDocument
        Dim objbzAgencyStaff As New AAMS.bizTravelAgency.bzAgencyStaff
        Try
            objInputXml.LoadXml("<MS_VIEWAGENCYSTAFF_INPUT><AGENCYSTAFFID></AGENCYSTAFFID></MS_VIEWAGENCYSTAFF_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("AGENCYSTAFFID").InnerText = hdStaffId.Value
            'Here Back end Method Call
            objOutputXml = objbzAgencyStaff.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                txtEmail.Text = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("EMAIL").InnerText
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Private Sub GetEmailIdOfEmployee()

        '<TR_PARTICIPANTLIST_INPUT>
        ' <TR_COURSES_ID />
        ' <MODE />
        ' <LETTERTYPE />
        ' </TR_PARTICIPANTLIST_INPUT>
        Dim objInputXml, objOutputXml As New XmlDocument

        Dim objbzParticipant As New AAMS.bizTraining.bzParticipantsBasket
        Try
            objInputXml.LoadXml("<TR_PARTICIPANTLIST_INPUT><TR_COURSES_ID /><MODE /><LETTERTYPE /></TR_PARTICIPANTLIST_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("TR_COURSES_ID").InnerText = objED.Decrypt(Request.QueryString("ID"))
            objInputXml.DocumentElement.SelectSingleNode("MODE").InnerText = "P"
            objInputXml.DocumentElement.SelectSingleNode("LETTERTYPE").InnerText = "Distinction Letter"
            objOutputXml = objbzParticipant.CourseSessionParticipantList(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Dim strPartId As String = ""
                strPartId = objED.Decrypt(Request.QueryString("TR_COURSEP_ID"))
                If Not objOutputXml.DocumentElement.SelectSingleNode("DETRAILS[@TR_COURSEP_ID='" + strPartId + "']") Is Nothing Then
                    txtEmail.Text = objOutputXml.DocumentElement.SelectSingleNode("DETRAILS[@TR_COURSEP_ID='" + strPartId + "']").Attributes("EMAILID").Value
                End If

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub



End Class
