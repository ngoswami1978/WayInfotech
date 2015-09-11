'#####################################################################
'############   Page Name -- TravelAgency_TAUP_AgencyStaff1     #######
'############   Date 15-December 2007  ################################
'############   Developed By MANOJ GARG  ###############################
'#####################################################################

#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data
#End Region
Partial Class TravelAgency_TAUP_AgencyStaff1
    Inherits System.Web.UI.Page
    Implements ICallbackEventHandler
    Dim Str As String
#Region "Page Level Variables/Objects"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim Lcode As String
    Dim objED As New EncyrptDeCyrpt
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
        lblError.Text = ""
        Session("PageName") = Request.Url.ToString() '"TravelAgency_TAUP_AgencyStaff"
        txtName.Attributes.Add("onkeypress", "allTextWithSpace()")
        txtSurName.Attributes.Add("onkeypress", "allTextWithSpace()")
        txtSecName.Attributes.Add("onkeypress", "allTextWithSpace()")
        ' This code is used for Expiration of Page From Cache
        objeAAMS.ExpirePageCache()
        ' This code is usedc for checking session handler according to user login.
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If

        ' #######################################
        ' ########## This Code code is used for enable/disable 
        ' ########## the button according to rights
        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Staff']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Staff']").Attributes("Value").Value)
                If strBuilder(0) = "0" Then
                    Response.Redirect("~/NoRights.aspx")
                    Exit Sub
                End If

                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                End If
                If (Request.QueryString("Action").ToUpper = "US") Then
                    If strBuilder(2) = "0" Then
                        btnSave.Enabled = False
                    End If
                End If
                If (Request.QueryString("Action") = "U") Or (Request.QueryString("Action") = "US") Then
                    If strBuilder(2) = "0" Then
                        btnSave.Enabled = False
                    End If
                End If
            End If
        Else
            strBuilder = objeAAMS.SecurityCheck(31)
        End If
        ' #######################################
        ' ########## End of code used for enable/disable 
        ' ########## the button according to rights


        txtAgencyName.Text = Request.Form("txtAgencyName")
        txtAddress.Text = Request.Form("txtAddress")
        txtCity.Text = Request.Form("txtCity")
        txtCountry.Text = Request.Form("txtCountry")
        txtEmail.Text = Request.Form("txtEmail")

        ' btnNew.Attributes.Add("onclick", "return TAUPAgencyStaff1();")
        btnSave.Attributes.Add("onclick", "return AgencyStaffMandatory();")


        Dim m As ClientScriptManager = Me.ClientScript
        Str = m.GetCallbackEventReference(Me, "args", "ReceiveServerAgencyDetails", "'this is context from server'")
        Dim strCallback As String = "function CallServerAgencyDetails(args,context){" + Str + ";}"
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServerAgencyDetails", strCallback, True)


        txtOfficeId.Attributes.Add("onfocusout", "return FillAgencyDetails()")
        TxtSignInNum.Attributes.Add("onkeyup", "next('TxtSignInNum','4','TxtSignInChar')")
        ImgAgency.Visible = True
        MrkStar.Visible = True
        If Not Page.IsPostBack Then
            'If (Request.QueryString("msg") = "A") Then
            ' lblError.Text = objeAAMSMessage.messInsert
            'End If
            objeAAMS.BindDropDown(DlstDesg, "STAFFDESIGNATION", True, 1)

            If Not Request.QueryString("Source") Is Nothing Then
                hdPageSource.Value = Request.QueryString("Source").ToString
                If Request.QueryString("Action").ToString = "I" Then
                    If Request.QueryString("Lcode") IsNot Nothing Then
                        AgencyView()
                    End If
                End If

            End If

            If (Not Request.QueryString("Action") = Nothing) Then
                If (Request.QueryString("Action").ToUpper = "UP") Then
                    If (Not Request.QueryString("ID") = Nothing) Then
                        ' ViewIPPoolGroup method  called for binding the controls  
                        lblError.Text = objeAAMSMessage.messUpdate
                        ViewAgencyStaff()
                    End If
                End If

                If (Request.QueryString("Action").ToUpper = "U") Then
                    If (Not Request.QueryString("ID") = Nothing) Then
                        ' ViewIPPoolGroup method  called for binding the controls  
                        ' lblError.Text = objeAAMSMessage.messUpdate
                        ViewAgencyStaff()
                    End If
                End If
                If (Request.QueryString("Action").ToUpper = "US") Then
                    If (Not Request.QueryString("ID") = Nothing) Then
                        ' ViewIPPoolGroup method  called for binding the controls  
                        lblError.Text = objeAAMSMessage.messInsert
                        ViewAgencyStaff()
                    End If
                End If
            End If
        End If
        If Request.QueryString("PageLink") IsNot Nothing Then
            btnSave.Enabled = False
        End If

     


    End Sub
#End Region
#Region " btnSave_Click"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim objInputXml As New XmlDataDocument
            Dim objOutputXml As New XmlDocument
            Dim objAgencyStaffNode As XmlNode

            Dim objbzAgencyStaff As New AAMS.bizTravelAgency.bzAgencyStaff


            'Lcode = Session("Action").ToString().Split("|").GetValue(1)
            'objInputXml.LoadXml("<MS_UPDATEAGENCYSTAFFDETAILS_INPUT><AGENCYSTAFF AGENCYSTAFFID='' LCODE='' STAFFNAME='' DESIGNATION='' DOW='' DOB='' CORRESPONDENCE=''	RESPONSIBLE='' FAX='' PHONE='' EMAIL='' NOTES='' CONTACT_PERSON='' /></MS_UPDATEAGENCYSTAFFDETAILS_INPUT>")

            objInputXml.LoadXml("<MS_UPDATEAGENCYSTAFFDETAILS_INPUT><AGENCYSTAFF AGENCYSTAFFID='' LCODE='' STAFFNAME='' DESIGNATION='' DOW='' DOB='' CORRESPONDENCE=''	RESPONSIBLE='' FAX='' PHONE='' EMAIL='' NOTES='' CONTACT_PERSON=''  SIGNINID =''  TITLE=''  FIRSTNAME=''  MIDDLENAME='' SURNAME=''   MOBILENO='' MARTIALSTATUS ='' SOURCE='' /></MS_UPDATEAGENCYSTAFFDETAILS_INPUT>")
           
            objAgencyStaffNode = objInputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF")
            With objAgencyStaffNode
                If Request.QueryString("Action") = "U" Or Request.QueryString("Action") = "US" Or Request.QueryString("Action") = "UP" Then
                    .Attributes("AGENCYSTAFFID").Value() = objED.Decrypt(Request.QueryString("ID").ToString().Trim())
                Else
                    ' .Attributes("AGENCYSTAFFID").Value() = Request.QueryString("AGENCYSTAFFID")
                End If
                .Attributes("LCODE").Value() = hdAgencyNameId.Value
                .Attributes("STAFFNAME").Value() = txtName.Text
                .Attributes("DESIGNATION").Value() = txtDesig.Text
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
                .Attributes("FAX").Value() = txtFax1.Text
                .Attributes("PHONE").Value() = txtPhone1.Text
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



                If chkContactPerson.Checked = True Then
                    .Attributes("CONTACT_PERSON").Value() = "TRUE"
                Else
                    .Attributes("CONTACT_PERSON").Value() = "FALSE"
                End If


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

                '@  End Added By abhishek  on Ist Sep 2011
            End With


            'Here Back end Method Call

            objOutputXml = objbzAgencyStaff.Update(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                Dim param As String = ""

                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "US" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "UP" Then
                    lblError.Text = objeAAMSMessage.messUpdate
                    'ImgAgency.Visible = False
                    'MrkStar.Visible = False

                    param = "TAUP_AgencyStaff1.aspx?Action=UP&ID=" & objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("AGENCYSTAFFID").Value)
                    If Not Request.QueryString("Source") Is Nothing Then
                        hdPageSource.Value = Request.QueryString("Source").ToString
                        param = param + "&Source=" + hdPageSource.Value
                    End If

                    If Not Request.QueryString("Lcode") Is Nothing Then
                        param = param + "&Lcode=" + Request.QueryString("Lcode")
                    End If

                    If Not Request.QueryString("AgencyName") Is Nothing Then
                        param = param + "&AgencyName=" + Request.QueryString("AgencyName")
                    End If

                    Response.Redirect(param)
                Else
                    param = "TAUP_AgencyStaff1.aspx?Action=US&ID=" & objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("AGENCYSTAFFID").Value)
                    If Not Request.QueryString("Source") Is Nothing Then
                        ' hdPageSource.Value = Request.QueryString("Source").ToString
                        param = param + "&Source=" + hdPageSource.Value
                    End If

                    If Not Request.QueryString("Lcode") Is Nothing Then
                        param = param + "&Lcode=" + Request.QueryString("Lcode")
                    End If

                    If Not Request.QueryString("AgencyName") Is Nothing Then
                        param = param + "&AgencyName=" + Request.QueryString("AgencyName")
                    End If
                    Response.Redirect(param)
                End If
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "btnReset_Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            'If (Request.QueryString("Action") = "U") Or (Request.QueryString("Action") = "US") Then
            '    ViewAgencyStaff()
            '    txtName.Focus()
            'Else
            '    txtName.Text = String.Empty
            '    txtDesig.Text = String.Empty
            '    txtDob.Text = String.Empty
            '    txtDow.Text = String.Empty
            '    txtEmail.Text = String.Empty
            '    txtFax1.Text = String.Empty
            '    txtPhone1.Text = String.Empty
            '    txtNotes.Text = String.Empty
            '    chkCor.Checked = False
            '    chkRes.Checked = False
            '    chkContactPerson.Checked = False
            '    txtName.Focus()
            'End If
            Response.Redirect(Request.Url.ToString, False)
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region " ViewAgencyStaff Procedure"
    Private Sub ViewAgencyStaff()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objAgencyInputXml, objAgencyOutputXml As New XmlDocument
        Try
            'ImgAgency.Visible = False
            'MrkStar.Visible = False
            Dim objbzAgencyStaff As New AAMS.bizTravelAgency.bzAgencyStaff
            'Lcode = Session("LCODE")
            objInputXml.LoadXml("<MS_VIEWAGENCYSTAFF_INPUT><AGENCYSTAFFID></AGENCYSTAFFID></MS_VIEWAGENCYSTAFF_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("AGENCYSTAFFID").InnerText = objED.Decrypt(Request.QueryString("ID"))
            'Here Back end Method Call
            objOutputXml = objbzAgencyStaff.View(objInputXml)
            ' objOutputXml.LoadXml("<MS_VIEWAGENCYSTAFF_OUTPUT><AGENCYSTAFF AGENCYSTAFFID='1' LCODE='1' STAFFNAME='ABC' DESIGNATION='Manager' DOW='12/03/2007' DOB='12/03/1967' CORRESPONDENCE='false' RESPONSIBLE='True' FAX='5655' PHONE='565' EMAIL='test@enableinc.biz' NOTES='gffgfdg' /><Errors Status='FALSE'>	<Error Code='' Description='' /></Errors></MS_VIEWAGENCYSTAFF_OUTPUT>")

            '<AGENCYSTAFF AGENCYSTAFFID="
            '11318643" LCODE="21" STAFFNAME="test229" 
            'DESIGNATION="Manager" DOW="" DOB="12/11/1967" 
            'CORRESPONDENCE="False" RESPONSIBLE="True" FAX="546546"
            ' PHONE="" EMAIL="test229@amadeus.co.in" NOTES="fdgf" 
            'CONTACT_PERSON="TRUE" SIGNINID="1234AB" TITLE="TIT" 
            'FIRSTNAME="NEERAJ" MIDDLENAME="KUMAR" SURNAME="GOSWAMI"
            ' MOBILENO="9899846293" MARTIALSTATUS="Yes" /> 



            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
               
                txtName.Text = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("STAFFNAME").InnerText
                txtDesig.Text = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("DESIGNATION").InnerText

                txtDob.Text = objeAAMS.ConvertDateBlank(objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("DOB").InnerText)
                If Right(txtDob.Text.Trim, 4).Trim = "1900" Then
                    txtDob.Text = txtDob.Text.Trim.Substring(0, 5)
                End If

                txtDow.Text = objeAAMS.ConvertDateBlank(objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("DOW").InnerText)

                If Right(txtDow.Text.Trim, 4).Trim = "1900" Then
                    txtDow.Text = txtDow.Text.Trim.Substring(0, 5)
                End If

                txtEmail.Text = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("EMAIL").InnerText
                txtFax1.Text = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("FAX").InnerText
                txtPhone1.Text = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("PHONE").InnerText
                txtNotes.Text = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("NOTES").InnerText
                chkCor.Checked = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("CORRESPONDENCE").InnerText
                chkRes.Checked = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("RESPONSIBLE").InnerText
                'Code Added by Mukund on 25th August
                chkContactPerson.Checked = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("CONTACT_PERSON").InnerText
                'Code Added by Mukund on 25th August

                '@ Start Added by abhishek on Ist Sep 2011
                If objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("OFFICEID") IsNot Nothing Then
                    txtOfficeId.Text = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("OFFICEID").InnerText

                End If

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


                Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
                objAgencyInputXml.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")
                objAgencyInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF").Attributes("LCODE").InnerText
                objAgencyOutputXml = objbzAgency.View(objAgencyInputXml)

                If objAgencyOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then


                    hdAgencyNameId.Value = objAgencyOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes("LOCATION_CODE").InnerText
                    txtAgencyName.Text = objAgencyOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes("NAME").InnerText.Trim
                    txtAddress.Text = objAgencyOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes("ADDRESS").InnerText.Trim & " " & objAgencyOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes("ADDRESS1").InnerText.Trim
                    txtCity.Text = objAgencyOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes("CITY").InnerText.Trim
                    txtCountry.Text = objAgencyOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes("COUNTRY").InnerText.Trim
                End If

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            'Response.Redirect("TAUP_AgencyStaff1.aspx?Action=I")
            Dim strurl As String = "TAUP_AgencyStaff1.aspx"
            Dim Param As String = "?Action=I"

            If Not Request.QueryString("Source") Is Nothing Then
                hdPageSource.Value = Request.QueryString("Source").ToString
                Param = Param + "&Source=" + hdPageSource.Value
            End If

            If Not Request.QueryString("Lcode") Is Nothing Then
                Param = Param + "&Lcode=" + Request.QueryString("Lcode")
            End If

            If Not Request.QueryString("AgencyName") Is Nothing Then
                Param = Param + "&AgencyName=" + Request.QueryString("AgencyName")
            End If

            strurl = strurl + Param

            Response.Redirect(strurl, False)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
        Return str
    End Function
    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
        Dim strInput As String
        strInput = eventArgument.ToString
        If strInput.Length >= 4 Then
            AgencyViewByOfficeID(eventArgument)

        ElseIf strInput <> "" Then
            AgencyViewByOfficeID(eventArgument)
        End If

    End Sub
    Private Sub AgencyViewByOfficeID(ByVal strOfficeID As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
        Dim strhdEnAOffice = ""
        Dim strhdEnCallAgencyName_LCODE = ""
        Try
           
            objInputXml.LoadXml("<TA_SEARCHAGENCY_INPUT><OFFICEID></OFFICEID><ResponsibleStaffID></ResponsibleStaffID></TA_SEARCHAGENCY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerText = strOfficeID
            '  objInputXml.DocumentElement.SelectSingleNode("ResponsibleStaffID").InnerText = objeAAMS.EmployeeID(Session("Security"))
            objOutputXml = objbzAgency.OfficeID_AgencySearch(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                Str = objOutputXml.OuterXml + "$" + ""

            Else
                str = ""
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

    Private Sub AgencyView()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency

        Try
            objInputXml.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = Request.QueryString("Lcode") ' objED.Decrypt(Request.QueryString("Lcode"))
            objOutputXml = objbzAgency.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXml.DocumentElement.SelectSingleNode("Agency")
                    txtAgencyName.Text = .Attributes("NAME").Value()
                    ' txtAddress.Text = .Attributes("ADDRESS").Value()
                    txtAddress.Text = .Attributes("ADDRESS").Value() + vbCrLf + .Attributes("ADDRESS1").Value()

                    txtCity.Text = .Attributes("CITY").Value()
                    txtCountry.Text = .Attributes("COUNTRY").Value()
                   
                    txtOfficeId.Text = .Attributes("OFFICEID").Value()
                   
                    ' txtEmail.Text = .Attributes("EMAIL").Value()
                    hdAgencyNameId.Value = Request.QueryString("Lcode") 'objED.Decrypt(Request.QueryString("Lcode"))

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

End Class
