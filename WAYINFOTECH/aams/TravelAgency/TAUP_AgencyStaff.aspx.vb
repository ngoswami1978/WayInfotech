
#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data
#End Region
Partial Class TravelAgency_TAUP_AgencyStaff
    Inherits System.Web.UI.Page
#Region "Page Level Variables/Objects"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim Lcode As String
    Dim imgDown As New Image
    Dim imgUp As New Image
#End Region

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

#Region "Page_Load Event"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
           

            Session("PageName") = Request.Url.ToString() '"TravelAgency_TAUP_AgencyStaff"
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If
            lblError.Text = String.Empty

            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgDown.ImageUrl = "~/Images/Sortdown.gif"

            If (Session("Action") IsNot Nothing) Then
                If (Session("Action").ToString().Split("|").Length >= 2) Then
                    Lcode = Session("Action").ToString().Split("|").GetValue(1)
                Else
                    lblError.Text = "Lcode is not exist."
                    btnNew.Enabled = False
                    btnSave.Enabled = False
                    Exit Sub
                End If
            Else
                lblError.Text = "Lcode is not exist."
                btnNew.Enabled = False
                btnSave.Enabled = False
                Exit Sub
            End If

            btnNew.Attributes.Add("onclick", "return TAUPAgencyStaffForStaff();")
            '  btnSave.Attributes.Add("onclick", "return AgencyStaffMandatoryForStaff();")
            btnSave.Attributes.Add("onclick", "return AgencyStaffMandatory();")

            TxtSignInNum.Attributes.Add("onkeyup", "next('TxtSignInNum','4','TxtSignInChar')")

            ' #######################################
            ' ########## This code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Staff']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Staff']").Attributes("Value").Value)
                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                        btnSave.Enabled = False
                    End If

                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                    End If

                    If (Request.QueryString("Action") = "U") Then
                        If strBuilder(2) = "0" Then
                            btnSave.Enabled = False
                        End If
                    End If

                End If
            End If
            ' #######################################
            ' ########## End of code used for enable/disable 
            ' ########## the button according to rights
            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(DlstDesg, "STAFFDESIGNATION", True, 1)
                '*******************************************************************
                If Request.QueryString("MSG") IsNot Nothing Then
                    If (Request.QueryString("MSG") = "A") Then
                        lblError.Text = objeAAMSMessage.messInsert
                    ElseIf (Request.QueryString("MSG") = "U") Then
                        lblError.Text = objeAAMSMessage.messUpdate
                    End If
                End If

                ''Code For  Updating of Agency Staff              
                If (Not Request.QueryString("Action") = Nothing) Then
                    If (Request.QueryString("Action").ToUpper = "U") Then
                        If (Not Request.QueryString("AGENCYSTAFFID") = Nothing) Then
                            ' ViewIPPoolGroup method  called for binding the controls  
                            ViewAgencyStaff()
                        End If
                    End If
                End If
                '*******************************************************************
                ' Load Data For Editing Mode

                ''Code For  Deletion of Agency Staff              
                If Not Session("Act") Is Nothing Then
                    If Session("Act").ToString().Split("|").GetValue(0) = "D" Then
                        lblError.Text = objeAAMSMessage.messDelete
                        Session("Act") = Nothing
                    End If
                End If
                ' Code For Loading All Agency Staff Details according to Current Agency (LCODE)
                'If strBuilder(0) = "1" Then


                GetAgencyStaff()
                'End If

            End If

            If Not Request.QueryString("Action") Is Nothing Then
                If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
                    AgencyStaffDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))
                End If
            End If


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region " ViewAgencyStaff Procedure"
    Private Sub ViewAgencyStaff()
        Dim objInputXml, objOutputXml As New XmlDocument

        Try
            Dim objbzAgencyStaff As New AAMS.bizTravelAgency.bzAgencyStaff
            'Lcode = Session("LCODE")
            objInputXml.LoadXml("<MS_VIEWAGENCYSTAFF_INPUT><AGENCYSTAFFID></AGENCYSTAFFID></MS_VIEWAGENCYSTAFF_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("AGENCYSTAFFID").InnerText = Request.QueryString("AGENCYSTAFFID")
            'Here Back end Method Call
            objOutputXml = objbzAgencyStaff.View(objInputXml)
           ' objOutputXml.LoadXml("<MS_VIEWAGENCYSTAFF_OUTPUT><AGENCYSTAFF AGENCYSTAFFID='1' LCODE='1' STAFFNAME='ABC' DESIGNATION='Manager' DOW='12/03/2007' DOB='12/03/1967' CORRESPONDENCE='false' RESPONSIBLE='True' FAX='5655' PHONE='565' EMAIL='test@enableinc.biz' NOTES='gffgfdg' /><Errors Status='FALSE'>	<Error Code='' Description='' /></Errors></MS_VIEWAGENCYSTAFF_OUTPUT>")

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                txtName.Text = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("STAFFNAME").InnerText
                txtDesig.Text = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("DESIGNATION").InnerText
                'txtDob.Text = objeAAMS.ConvertDateBlank(objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("DOB").InnerText)
                'txtDow.Text = objeAAMS.ConvertDateBlank(objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("DOW").InnerText)

                txtDob.Text = objeAAMS.ConvertDateBlank(objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("DOB").InnerText)
                If Right(txtDob.Text.Trim, 4).Trim = "1900" Then
                    txtDob.Text = txtDob.Text.Trim.Substring(0, 5)
                End If

                txtDow.Text = objeAAMS.ConvertDateBlank(objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("DOW").InnerText)

                If Right(txtDow.Text.Trim, 4).Trim = "1900" Then
                    txtDow.Text = txtDow.Text.Trim.Substring(0, 5)
                End If

                txtEmail.Text = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("EMAIL").InnerText
                txtFax.Text = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("FAX").InnerText
                txtPhone.Text = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("PHONE").InnerText
                txtNotes.Text = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("NOTES").InnerText
                chkCor.Checked = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("CORRESPONDENCE").InnerText
                chkRes.Checked = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("RESPONSIBLE").InnerText


                '@ Start Added by abhishek on Ist Sep 2011
               

                Dim li As New ListItem
                li = DlstTitle.Items.FindByValue(objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("TITLE").InnerText.Trim)
                If li IsNot Nothing Then
                    DlstTitle.SelectedValue = li.Value
                End If


                li = DlstDesg.Items.FindByText(objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("DESIGNATION").InnerText.Trim)
                If li IsNot Nothing Then
                    DlstDesg.SelectedValue = li.Value
                End If

                li = DlstMaritalStatus.Items.FindByValue(objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("MARTIALSTATUS").InnerText.Trim)
                If li IsNot Nothing Then
                    DlstMaritalStatus.SelectedValue = li.Value
                End If

                txtName.Text = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("FIRSTNAME").InnerText.Trim
                txtSecName.Text = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("MIDDLENAME").InnerText.Trim
                txtSurName.Text = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("SURNAME").InnerText.Trim
                TxtMob.Text = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("MOBILENO").InnerText.Trim
                If objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("SIGNINID").InnerText.Trim.Length >= 6 Then
                    TxtSignInNum.Text = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("SIGNINID").InnerText.Trim.Substring(0, 4)
                    TxtSignInChar.Text = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("SIGNINID").InnerText.Trim.Substring(4, 2)
                End If



                '@ End Added by abhishek on Ist Sep 2011


                ' #######################################
                ' ########## This code is used for enable/disable 
                ' ########## the button according to rights
                Dim objSecurityXml As New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Staff']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Staff']").Attributes("Value").Value)
                        If strBuilder(2) = "1" Then

                            btnSave.Enabled = True
                        End If

                    End If
                End If
                ' #######################################
                ' ########## End of code used for enable/disable 
                ' ########## the button according to rights


            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region " GetAgencyStaff Procedure"
    Private Sub GetAgencyStaff()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Staff']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Staff']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Exit Sub
                    End If
                End If
            End If
            If (Session("Action") IsNot Nothing) Then
                Lcode = Session("Action").ToString().Split("|").GetValue(1)

                Dim objbzAgencyStaff As New AAMS.bizTravelAgency.bzAgency

                ' objInputXml.LoadXml("<UP_GETAGENCYSTAFFDETAILS_INPUT><LCODE></LCODE></UP_GETAGENCYSTAFFDETAILS_INPUT>")
                objInputXml.LoadXml("<UP_GETAGENCYSTAFFDETAILS_INPUT><LCODE></LCODE> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></UP_GETAGENCYSTAFFDETAILS_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = Lcode ' "229"


                'Start CODE for sorting and paging

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
                    ViewState("SortName") = "STAFFNAME"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "DATE" '"LOCATION_CODE"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
                End If

                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
                End If

                'End Code for paging and sorting

                'Here Back end Method Call
                objOutputXml = objbzAgencyStaff.GetAgencyStaffDetails(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    ViewState("PrevSearching") = objInputXml.OuterXml



                    If Request.QueryString("MSG") IsNot Nothing And Session("Act") IsNot Nothing Then
                        If (Request.QueryString("MSG") = "A") Or (Request.QueryString("MSG") = "U") Or Session("Act").ToString().Split("|").GetValue(0) = "D" Then
                        Else
                            lblError.Text = ""
                        End If
                    End If




                    gvAgencyStaff.DataSource = ds.Tables("AGENCYSTAFF")
                    gvAgencyStaff.DataBind()
                    gvAgencyStaff.Visible = True

                    'txtRecordOnCurrReco.Text = gvAgencyStaff.Rows.Count.ToString()
                    PagingCommon(objOutputXml)

                    Dim intcol As Integer = GetSortColumnIndex(gvAgencyStaff)
                    If ViewState("Desc") = "FALSE" Then
                        gvAgencyStaff.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                    End If
                    If ViewState("Desc") = "TRUE" Then
                        gvAgencyStaff.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                    End If

                    'drplstConPer.DataSource = ds.Tables("AGENCYSTAFF")
                    'drplstConPer.DataTextField = "STAFFNAME"

                    'drplstConPer.DataValueField = "AGENCYSTAFFID"
                    'drplstConPer.DataBind()

                    'drplstConPer.Items.Insert(0, New ListItem("", ""))

                    bindDropDownd()

                    'Dim strContactID As String = Session("ContactPID").ToString()

                    'If strContactID.Trim.Length = 0 Then
                    '    drplstConPer.SelectedIndex = 0
                    'Else
                    '    drplstConPer.SelectedValue = strContactID   '.Items.FindByValue(strContactID.Trim())
                    'End If
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If

            Else
                lblError.Text = "Lcode is not exist."
            End If

        Catch ex As Exception
            lblError.Text = ex.Message

        End Try
    End Sub
#End Region
#Region "btnReset_Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try

            If (Request.QueryString("Action") = "U") Then
                ViewAgencyStaff()
                txtName.Focus()
            Else
                txtName.Text = String.Empty

                txtDesig.Text = String.Empty
                txtDob.Text = String.Empty
                txtDow.Text = String.Empty
                txtEmail.Text = String.Empty
                txtFax.Text = String.Empty
                txtPhone.Text = String.Empty
                txtNotes.Text = String.Empty
                chkCor.Checked = False
                chkRes.Checked = False
                If (drplstConPer.Items.Count > 0) Then
                    drplstConPer.SelectedIndex = 0
                End If
                If (DlstMaritalStatus.Items.Count > 0) Then
                    DlstMaritalStatus.SelectedIndex = 0
                End If
                If (DlstTitle.Items.Count > 0) Then
                    DlstTitle.SelectedIndex = 0
                End If

                drplstConPer.Items.Clear()
                txtSecName.Text = String.Empty
                txtSurName.Text = String.Empty
                TxtSignInNum.Text = String.Empty

                TxtSignInChar.Text = String.Empty
                TxtMob.Text = String.Empty
                If (DlstDesg.Items.Count > 0) Then
                    DlstDesg.SelectedIndex = 0
                End If

                txtName.Focus()
            End If
            GetAgencyStaff()
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region " gvAgencyStaff_RowDataBound Event"
    Protected Sub gvAgencyStaff_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAgencyStaff.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If

            Dim hdstrAGENCYSTAFFID As HiddenField
            Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            linkEdit = e.Row.FindControl("linkEdit")
            Dim linkDelete As System.Web.UI.HtmlControls.HtmlAnchor
            linkDelete = e.Row.FindControl("linkDelete")
            hdstrAGENCYSTAFFID = e.Row.FindControl("hdAGENCYSTAFFID")
            If (hdstrAGENCYSTAFFID.Value = "") Then
                e.Row.Cells.Clear()
                Exit Sub
            End If
            'linkEdit.Attributes.Add("onclick", "return EditFunctionForStaff(" & "'" & hdstrPriorityId.Value & "'" & ");")
            'linkDelete.Attributes.Add("onclick", "return DeleteFunctionForStaff(" & "'" & hdstrPriorityId.Value & "'" & ");")
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Staff']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Staff']").Attributes("Value").Value)
                    If strBuilder(3) = "0" Then
                        linkDelete.Disabled = True
                    Else
                        linkDelete.Attributes.Add("onclick", "return DeleteFunctionForStaff(" & "'" & hdstrAGENCYSTAFFID.Value & "'" & ");")
                    End If
                    If strBuilder(2) = "0" Then
                        linkEdit.Disabled = True
                    Else
                        linkEdit.Attributes.Add("onclick", "return EditFunctionForStaff(" & "'" & hdstrAGENCYSTAFFID.Value & "'" & ");")
                    End If
                Else
                    'linkDelete.Disabled = True
                    'linkEdit.Disabled = True
                    'linkEdit.Attributes.Add("onclick", "return EditFunctionForStaff(" & "'" & hdstrAGENCYSTAFFID.Value & "'" & ");")
                    'linkDelete.Attributes.Add("onclick", "return DeleteFunctionForStaff(" & "'" & hdstrAGENCYSTAFFID.Value & "'" & ");")
                End If
            Else
                linkEdit.Disabled = False
                linkEdit.Disabled = False
                linkDelete.Attributes.Add("onclick", "return DeleteFunctionForStaff(" & "'" & hdstrAGENCYSTAFFID.Value & "'" & ");")
                linkEdit.Attributes.Add("onclick", "return EditFunctionForStaff(" & "'" & hdstrAGENCYSTAFFID.Value & "'" & ");")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region " btnSave_Click"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim objInputXml As New XmlDataDocument
            Dim objOutputXml As New XmlDocument
            Dim objAgencyStaffNode As XmlNode

            Dim objbzAgencyStaff As New AAMS.bizTravelAgency.bzAgencyStaff
            If (IsValid) Then
                If (Session("Action") IsNot Nothing) Then
                    Lcode = Session("Action").ToString().Split("|").GetValue(1)
                    objInputXml.LoadXml("<MS_UPDATEAGENCYSTAFFDETAILS_INPUT><AGENCYSTAFF AGENCYSTAFFID='' LCODE='' STAFFNAME='' DESIGNATION='' DOW='' DOB='' CORRESPONDENCE=''	RESPONSIBLE='' FAX='' PHONE='' EMAIL='' NOTES='' CONTACT_PERSON=''  SIGNINID =''  TITLE=''  FIRSTNAME=''  MIDDLENAME='' SURNAME=''   MOBILENO='' MARTIALSTATUS ='' SOURCE='' /></MS_UPDATEAGENCYSTAFFDETAILS_INPUT>")
                    objAgencyStaffNode = objInputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF")
                    With objAgencyStaffNode
                        If Request.QueryString("Action") = "U" Then
                            .Attributes("AGENCYSTAFFID").Value() = Request.QueryString("AGENCYSTAFFID")
                        Else
                            ' .Attributes("AGENCYSTAFFID").Value() = Request.QueryString("AGENCYSTAFFID")
                        End If
                        .Attributes("LCODE").Value() = Lcode ' "229"
                        .Attributes("STAFFNAME").Value() = txtName.Text
                        .Attributes("DESIGNATION").Value() = txtDesig.Text
                        'If (txtDob.Text.Trim().Length > 0) Then
                        '    .Attributes("DOB").Value() = objeAAMS.ConvertTextDate(txtDob.Text) ' txtDob.Text.Trim().Split("/")(2) & txtDob.Text.Trim().Split("/")(1) & txtDob.Text.Trim().Split("/")(0)
                        'End If
                        'If (txtDow.Text.Trim().Length > 0) Then
                        '    .Attributes("DOW").Value() = objeAAMS.ConvertTextDate(txtDow.Text) 'txtDow.Text.Trim().Split("/")(2) & txtDow.Text.Trim().Split("/")(1) & txtDow.Text.Trim().Split("/")(0)
                        'End If

                        If (txtDob.Text.Trim().Length > 0) Then
                            If txtDob.Text.Trim.Length = 5 Then
                                .Attributes("DOB").Value() = objeAAMS.ConvertTextDate(txtDob.Text.Trim + "/1900")
                            Else
                                .Attributes("DOB").Value() = objeAAMS.ConvertTextDate(txtDob.Text)
                            End If

                            ' txtDob.Text.Trim().Split("/")(2) & txtDob.Text.Trim().Split("/")(1) & txtDob.Text.Trim().Split("/")(0)
                        End If
                        If (txtDow.Text.Trim().Length > 0) Then
                            ' .Attributes("DOW").Value() = objeAAMS.ConvertTextDate(txtDow.Text) 'txtDow.Text.Trim().Split("/")(2) & txtDow.Text.Trim().Split("/")(1) & txtDow.Text.Trim().Split("/")(0)

                            If txtDow.Text.Trim.Length = 5 Then
                                .Attributes("DOW").Value() = objeAAMS.ConvertTextDate(txtDow.Text.Trim + "/1900")
                            Else
                                .Attributes("DOW").Value() = objeAAMS.ConvertTextDate(txtDow.Text)
                            End If

                        End If

                        .Attributes("EMAIL").Value() = txtEmail.Text
                        .Attributes("FAX").Value() = txtFax.Text
                        .Attributes("PHONE").Value() = txtPhone.Text
                        If (chkCor.Checked) Then
                            .Attributes("CORRESPONDENCE").Value() = 1
                        Else
                            .Attributes("CORRESPONDENCE").Value() = 0
                        End If
                        If (chkRes.Checked) Then
                            .Attributes("RESPONSIBLE").Value() = 1
                        Else
                            .Attributes("RESPONSIBLE").Value() = 0
                        End If

                        .Attributes("NOTES").Value() = txtNotes.Text


                        '@  Start Added By abhishek  on Ist Sep 2011
                        'SIGNINID()
                        'TITLE
                        'FIRSTNAME()
                        'MIDDLENAME()
                        'SURNAME()
                        'MOBILENO()
                        'MARTIALSTATUS()

                        .Attributes("SIGNINID").Value() = TxtSignInNum.Text + TxtSignInChar.Text
                        .Attributes("TITLE").Value() = DlstTitle.SelectedValue
                        .Attributes("FIRSTNAME").Value() = txtName.Text
                        .Attributes("MIDDLENAME").Value() = txtSecName.Text
                        .Attributes("SURNAME").Value() = txtSurName.Text
                        .Attributes("MOBILENO").Value() = TxtMob.Text
                        If DlstDesg.SelectedValue <> "" Then
                            .Attributes("DESIGNATION").Value() = DlstDesg.SelectedItem.Text
                        End If
                        If DlstMaritalStatus.SelectedValue.Trim.ToUpper = "YES" Then
                            .Attributes("MARTIALSTATUS").Value() = "Yes"
                        Else
                            .Attributes("MARTIALSTATUS").Value() = ""
                        End If
                        .Attributes("SOURCE").Value() = "ORDER"

                        '@  End Added By abhishek  on Ist Sep 2011



                    End With


                    'Here Back end Method Call

                    objOutputXml = objbzAgencyStaff.Update(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        If Request.QueryString("Action") = "U" Then
                            Response.Redirect("TAUP_AgencyStaff.aspx?Id=3&Action=U&MSG=U&AGENCYSTAFFID=" & objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("AGENCYSTAFFID").Value, False)
                            'lblError.Text = objeAAMSMessage.messUpdate '"Record Updated successfully."
                        Else
                            'lblError.Text = objeAAMSMessage.messInsert '"Record Added successfully."
                            Response.Redirect("TAUP_AgencyStaff.aspx?Id=3&Action=U&MSG=A&AGENCYSTAFFID=" & objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("AGENCYSTAFFID").Value, False)
                        End If
                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            Else
                lblError.Text = "Lcode is not exist."
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "gencyStaffDelete procedure is used for deletion of Agency Staff"
    Sub AgencyStaffDelete(ByVal strAgencyStaffId As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Try
            'Lcode = Session("LCODE")
            Dim objbzAgencyStaff As New AAMS.bizTravelAgency.bzAgencyStaff
            objInputXml.LoadXml("<MS_DELETEAGENCYSTAFFDETAILS_INPUT><AGENCYSTAFFID></AGENCYSTAFFID></MS_DELETEAGENCYSTAFFDETAILS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("AGENCYSTAFFID").InnerText = strAgencyStaffId
            'Here Back end Method Call
            objOutputXml = objbzAgencyStaff.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                'lblError.Text = objeAAMSMessage.messDelete '"Record deleted successfully."
                Session("Act") = Request.QueryString("Action")
                Response.Redirect("TAUP_AgencyStaff.aspx?Id=3", False)
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    'Method altered by Mukund on 1-Feb 2008
    Protected Sub btnUpdateStaff_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateStaff.Click
        Dim objInputXml As New XmlDataDocument
        Dim objOutputXml As New XmlDocument
        '  Dim objAgencyStaffNode As XmlNode
        Try

            Dim objbzAgencyStaff As New AAMS.bizTravelAgency.bzAgency
            If (IsValid) Then
                If (Session("Action") IsNot Nothing) Then
                    Lcode = Session("Action").ToString().Split("|").GetValue(1)
                    objInputXml.LoadXml("<TA_UPDATEAGENCYSTAFFCONTACTPERSON_INPUT><STAFF Location_code = '' Contact_Person_Id=''/></TA_UPDATEAGENCYSTAFFCONTACTPERSON_INPUT>")

                    objInputXml.DocumentElement.SelectSingleNode("STAFF").Attributes("Location_code").InnerText = Lcode
                    If drplstConPer.SelectedIndex <> 0 Then
                        objInputXml.DocumentElement.SelectSingleNode("STAFF").Attributes("Contact_Person_Id").InnerText = drplstConPer.SelectedValue
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("STAFF").Attributes("Contact_Person_Id").InnerText = ""
                    End If

                    'Here Back end Method Call

                    objOutputXml = objbzAgencyStaff.UpdateStaffContactPerson(objInputXml)

                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        If objOutputXml.DocumentElement.SelectSingleNode("STAFF").Attributes("Contact_Person_Id").Value.Trim() <> 0 Then
                            drplstConPer.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("STAFF").Attributes("Contact_Person_Id").Value.Trim()
                            Session("ContactPID") = objOutputXml.DocumentElement.SelectSingleNode("STAFF").Attributes("Contact_Person_Id").Value.Trim()
                        Else
                            Session("ContactPID") = ""
                        End If

                        GetAgencyStaff()


                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            Else
                lblError.Text = "Lcode is not exist."
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Private Function GetSortColumnIndex(ByVal grd As GridView) As Int16
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Return grd.Columns.IndexOf(field)
            End If
        Next
        Return 0
    End Function
    Private Sub PagingCommon(ByVal objOutputXml As XmlDocument)
        pnlPaging.Visible = True
        Dim count As Integer = CInt(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
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
            'pnlPaging.Visible = False
            ' ddlPageNumber.Visible = False
            lnkNext.Visible = False
            lnkPrev.Visible = False
        Else
            'ddlPageNumber.Visible = True
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
        txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
    End Sub

#Region "Sort Function"
    Sub SortCall(ByVal SortName As String)

        If ViewState("SortName") Is Nothing Then
            ViewState("SortName") = SortName
            ViewState("Desc") = "FALSE"
        Else
            If ViewState("SortName") = SortName Then
                If ViewState("Desc") = "TRUE" Then
                    ViewState("Desc") = "FALSE"
                Else
                    ViewState("Desc") = "TRUE"
                End If
            Else
                ViewState("SortName") = SortName
                ViewState("Desc") = "FALSE"
            End If
        End If
        GetAgencyStaff()
    End Sub
#End Region

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            GetAgencyStaff()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            GetAgencyStaff()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            GetAgencyStaff()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvAgencyStaff_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvAgencyStaff.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub bindDropDownd()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Staff']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Staff']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Exit Sub
                    End If
                End If
            End If
            If (Session("Action") IsNot Nothing) Then
                Lcode = Session("Action").ToString().Split("|").GetValue(1)

                Dim objbzAgencyStaff As New AAMS.bizTravelAgency.bzAgency

                ' objInputXml.LoadXml("<UP_GETAGENCYSTAFFDETAILS_INPUT><LCODE></LCODE></UP_GETAGENCYSTAFFDETAILS_INPUT>")
                objInputXml.LoadXml("<UP_GETAGENCYSTAFFDETAILS_INPUT><LCODE></LCODE> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></UP_GETAGENCYSTAFFDETAILS_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = Lcode ' "229"

                'Here Back end Method Call
                objOutputXml = objbzAgencyStaff.GetAgencyStaffDetails(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    drplstConPer.Items.Clear()
                  

                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)

                    drplstConPer.DataSource = ds.Tables("AGENCYSTAFF")
                    drplstConPer.DataTextField = "STAFFNAME"

                    drplstConPer.DataValueField = "AGENCYSTAFFID"
                    drplstConPer.DataBind()

                    drplstConPer.Items.Insert(0, New ListItem("", ""))

                    'Dim strContactID As String = Session("ContactPID").ToString()

                    'If strContactID.Trim.Length = 0 Then
                    '    drplstConPer.SelectedIndex = 0
                    'Else
                    '    drplstConPer.SelectedValue = strContactID   '.Items.FindByValue(strContactID.Trim())
                    'End If

                    Dim strContactID As String = ""
                    If Session("ContactPID") IsNot Nothing Then
                        strContactID = Session("ContactPID").ToString()
                        Dim li As ListItem = drplstConPer.Items.FindByValue(strContactID)
                        If li IsNot Nothing Then
                            drplstConPer.SelectedValue = li.Value
                        End If

                    End If
                  
                Else
                    drplstConPer.DataSource = Nothing
                    drplstConPer.DataBind()
                End If

            Else
                lblError.Text = "Lcode is not exist."
            End If

        Catch ex As Exception
            lblError.Text = ex.Message

        End Try
    End Sub

    
    Protected Sub txtNotes_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNotes.TextChanged

    End Sub
End Class
