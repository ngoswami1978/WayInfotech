
Partial Class Training_TRUP_ParticipantBasket
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

        Try

            ' Checking Security.
            CheckSecurity()
            If Not Page.IsPostBack Then
                txtPreferredDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
                objeAAMS.BindDropDown(ddlAOffice, "AOFFICE", True, 1)
                'objeAAMS.BindDropDown(ddlStatus, "PARTCIPANTSTATUS", True)
                objeAAMS.BindDropDown(ddlCourse, "COURSE", True, 1)
                If Not Session("Security") Is Nothing Then
                    Dim objtemp As New XmlDocument
                    objtemp.LoadXml(Session("Security"))
                    txtLoggedBy.Text = objtemp.DocumentElement.SelectSingleNode("Login").InnerText
                End If
                If Not Request.QueryString("Action") Is Nothing Then
                    hdPageStatus.Value = Request.QueryString("Action").ToString
                End If
                If Not Request.QueryString("BasketID") Is Nothing Then
                    hdPageBasketID.Value = objED.Decrypt(Request.QueryString("BasketID").ToString)
                End If
                If Not Request.QueryString("Status") Is Nothing Then
                    hdStatus.Value = Request.QueryString("Status").ToString
                End If

                If hdPageBasketID.Value <> "" Then
                    ViewRecords()
                Else
                End If

                If hdStatus.Value.ToUpper = "CLOSED" Then
                    '  disabledControls()
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub

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
                hdPageBasketID.Value = .Attributes("TR_BASKET_REQUEST_ID").Value
                txtRequestID.Text = .Attributes("TR_BASKET_REQUEST_ID").Value
                Dim strDate As String = .Attributes("TR_BASKET_DATE").Value
                If strDate <> "" Then
                    Dim day As String = strDate.Split("/").GetValue(1)
                    Dim month As String = strDate.Split("/").GetValue(0)
                    If day.Length = 1 Then
                        day = "0" & day
                    End If
                    If month.Length = 1 Then
                        month = "0" & month
                    End If
                    txtDate.Text = day & "/" & month & "/" & strDate.Split("/").GetValue(2).ToString.Split(" ").GetValue(0)
                End If

                Dim strPreferredDate As String = .Attributes("PREFERREDDATE").Value
                If strPreferredDate <> "" Then
                    Dim day As String = strPreferredDate.Split("/").GetValue(1)
                    Dim month As String = strPreferredDate.Split("/").GetValue(0)
                    If day.Length = 1 Then
                        day = "0" & day
                    End If
                    If month.Length = 1 Then
                        month = "0" & month
                    End If
                    txtPreferredDate.Text = day & "/" & month & "/" & strPreferredDate.Split("/").GetValue(2)
                Else
                    txtPreferredDate.Text = strPreferredDate
                End If

                txtDate.ReadOnly = True
                txtDate.CssClass = "textboxgrey"

                txtPreferredDate.ReadOnly = True
                txtPreferredDate.CssClass = "textboxgrey"
                Img2.Visible = False
                Img1.Visible = False
                ' imgDate.Style.Add("display", "none")
                'txtDate.Text = .Attributes("TR_BASKET_DATE").Value
                txtRemarks.Text = .Attributes("TR_BASKET_REMARKS").Value


                ddlCourse.SelectedValue = .Attributes("TR_COURSE_ID").Value
                txtLoggedBy.Text = .Attributes("EMPLOYEENAME").Value

                'Add on 17 jan 09
                ddlAOffice.Enabled = False
                imgPreferredDate.Style.Add("display", "none")
                'end code
                ddlAOffice.SelectedValue = .Attributes("AOFFICE").Value
              '  ddlStatus.SelectedValue = .Attributes("TR_PARTSTATUS_ID").Value
                txtAgencyStaff.Text = .Attributes("STAFFNAME").Value
                hdAgencyStaffNameParticipantBasket.Value = .Attributes("AGENCYSTAFFID").Value





                
                hdAgencyNameParticipantBasket.Value = .Attributes("LCODE").Value
                txtAgencyName.Text = .Attributes("AGENCYNAME").Value
                txtAddress.Text = .Attributes("ADDRESS").Value
                txtCountry.Text = .Attributes("COUNTRY").Value
                txtCity.Text = .Attributes("CITY").Value
                txtPhone.Text = .Attributes("PHONE").Value
                txtFax.Text = .Attributes("FAX").Value
                txtOnlineStatus.Text = .Attributes("ONLINESTATUS").Value
                txtPriority.Text = .Attributes("PRIORITY").Value

            End With
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    'Function to disable controls if request.querysting(status) has a value "closed"
    Sub disabledControls()
        Img2.Visible = False
        Img1.Visible = False
        '  imgDate.Style.Add("display", "none")
        imgPreferredDate.Style.Add("display", "none")
        ' imgPreferredDate.Visible = False
        ' txtDate.ReadOnly = True
        '  txtDate.CssClass = "textboxgrey"
        txtPreferredDate.ReadOnly = True
        txtPreferredDate.CssClass = "textboxgrey"
        ddlAOffice.Enabled = False
        ddlStatus.Enabled = False
        ddlCourse.Enabled = False
        txtRemarks.CssClass = "textboxgrey"
        txtRemarks.ReadOnly = True
        btnSave.Enabled = False
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzParticipantsBasket As New AAMS.bizTraining.bzParticipantsBasket
        Try
            objInputXml.LoadXml("<TR_UPDATEBASKET_INPUT><BASKET TR_BASKET_REQUEST_ID='' TR_BASKET_DATE='' TR_BASKET_REMARKS='' TR_COURSE_ID='' TR_PARTSTATUS_ID=''  EMPLOYEEID='' LCODE='' AOFFICE='' AGENCYSTAFFID='' RECEIVEDFROMWEB='' PREFERREDDATE='' /></TR_UPDATEBASKET_INPUT>")
            With objInputXml.DocumentElement.SelectSingleNode("BASKET")
                ' .Attributes("TR_BASKET_DATE").Value = objeAAMS.GetDateFormat(txtDate.Text, "dd/MM/yyyy", "yyyyMMdd", "/")
                .Attributes("TR_BASKET_REMARKS").Value = txtRemarks.Text
                .Attributes("TR_COURSE_ID").Value = ddlCourse.SelectedValue
                .Attributes("TR_PARTSTATUS_ID").Value ="" ' ddlStatus.SelectedValue
                If Not Session("LoginSession") Is Nothing Then
                    .Attributes("EMPLOYEEID").Value = Session("LoginSession").ToString.Split("|").GetValue(0)
                End If
                .Attributes("LCODE").Value = hdAgencyNameParticipantBasket.Value.Split("|").GetValue(0)

                .Attributes("AOFFICE").Value = ddlAOffice.SelectedValue
                .Attributes("AGENCYSTAFFID").Value = hdAgencyStaffNameParticipantBasket.Value
                .Attributes("RECEIVEDFROMWEB").Value = "0"
                .Attributes("PREFERREDDATE").Value = objeAAMS.GetDateFormat(txtPreferredDate.Text, "dd/MM/yyyy", "yyyyMMdd", "/")



                If hdPageBasketID.Value <> "" Then
                    .Attributes("TR_BASKET_REQUEST_ID").Value = hdPageBasketID.Value
                End If

            End With

            'Here Back end Method Call
            objOutputXml = objbzParticipantsBasket.Update(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                
                If hdPageBasketID.Value = "" Then
                    lblError.Text = objeAAMSMessage.messInsert
                    hdPageBasketID.Value = objOutputXml.DocumentElement.SelectSingleNode("BASKET").Attributes("TR_BASKET_REQUEST_ID").Value
                    txtRequestID.Text = hdPageBasketID.Value
                    txtDate.ReadOnly = True
                    txtDate.CssClass = "textboxgrey"
                    '  imgDate.Visible = False
                Else
                    lblError.Text = objeAAMSMessage.messUpdate
                End If
                '   Check Security.
                CheckSecurity()
               
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
 
            txtAgencyName.Text = Request.Form("txtAgencyName")
            txtAddress.Text = Request.Form("txtAddress")
            txtAgencyStaff.Text = Request.Form("txtAgencyStaff")
            txtCity.Text = Request.Form("txtCity")
            txtCountry.Text = Request.Form("txtCountry")
            txtFax.Text = Request.Form("txtFax")
            txtOnlineStatus.Text = Request.Form("txtOnlineStatus")
            txtPhone.Text = Request.Form("txtPhone")
            txtPriority.Text = Request.Form("txtPriority")
            'Dim strData As String = hdAgencyNameParticipantBasket.Value
            'txtAgencyName.Text = strData.Split("|").GetValue(1)
            'txtAddress.Text = strData.Split("|").GetValue(8)
            'txtCity.Text = strData.Split("|").GetValue(2)
            'txtCountry.Text = strData.Split("|").GetValue(3)
            'txtPhone.Text = strData.Split("|").GetValue(4)
            'txtFax.Text = strData.Split("|").GetValue(6)
            'txtOnlineStatus.Text = strData.Split("|").GetValue(7)
            'txtAgencyName.Text = Request.Form("txtAgencyName")

        End Try
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("TRUP_ParticipantBasket.aspx?Action=I")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If hdPageBasketID.Value <> "" Then
                ViewRecords()
            Else
                Dim strQueryString As String = Request.QueryString.ToString
                Response.Redirect("TRUP_ParticipantBasket.aspx?" + strQueryString)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Participant Basket']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Participant Basket']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                End If

                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    btnSave.Enabled = False
                End If
                If strBuilder(2) = "0" And (hdPageBasketID.Value <> "" Or Request.QueryString("BasketID") IsNot Nothing) Then
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
End Class
