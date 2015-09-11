
Partial Class TravelAgency_MSUP_Agency
    Inherits System.Web.UI.Page
    Implements System.Web.UI.ICallbackEventHandler
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim str As String
    Dim strBuilder As StringBuilder
    Const strClass_NAME As String = "TAUP_Agency.aspx"
    Dim objED As New EncyrptDeCyrpt

    '#######################################################
    '@ Note drpIataStatus -- This is Previouly Priority
    '#######################################################
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            lblError.Text = ""
            Session("PageName") = Request.Url.ToString()
            objeAAMS.ExpirePageCache()
            txtIPAddress.Attributes.Add("onkeydown", "return onlyDeleteAllowedManageAgency('txtIPAddress')")
            'Code of Security Check
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If

            btnSave.Attributes.Add("onclick", "return ValidateAgencyManageAgency();")
            'btnMiscViewDoc.Attributes.Add("onclick", "return ViewMiscDocManageAgency();")
            ' rdCCRoster.Attributes.Add("onclick", "return CheckSelection();")
            btnNew.Attributes.Add("onclick", "return NewFunctionManageAgency();")
            '***************************************************************************************

            ' @ Call ApplySecurityoption() for applying Security Check

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))



            ' ApplySecurityoption()

            'If rdCCRoster.Checked = True Then
            '    txtReason.Enabled = True
            '    txtReason.CssClass = "textbox"
            'Else
            '    txtReason.Enabled = False
            '    txtReason.CssClass = "textboxgrey"

            'End If

            Dim m As ClientScriptManager = Me.ClientScript
            str = m.GetCallbackEventReference(Me, "args", "ReceiveServerDataManageAgency", "'this is context from server'")
            Dim strCallback As String = "function CallServer(args,context){" + str + ";}"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)
            drpCity.Attributes.Add("OnChange", "return SendCustomerIDManageAgency('drpCity');")


            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Attributes("Value").Value)

                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                    End If

                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            '@ #################  End of  Security For Agency Details

            If Not Page.IsPostBack Then


                BindAllControl()
                If (Session("Msg") IsNot Nothing) Then
                    lblError.Text = Session("Msg").ToString()
                    '@ #################   Security For Agency Details When User save a new record and no rights of edit the agency then no any controls is enabled
                    If (Session("Msg").ToString() = objeAAMSMessage.messInsert) Then
                        If strBuilder(2) = "0" Then
                            AgencyView()
                            Session("Action") = "I"
                            btnSave.Enabled = False
                            Session("Msg") = Nothing
                            ApplySecurityForSomecontrols()
                            ApplySecurityoption()
                            GetOtherDetails()

                            '@ #################   Security For Modify Agency 1A Responsibility
                            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify 1AResponsibiltiy']").Count <> 0 Then
                                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify 1AResponsibiltiy']").Attributes("Value").Value)
                                If strBuilder(2) = "1" Then
                                    btnSave.Enabled = True
                                End If
                            End If
                            '@ #################  End  Security For Modify Agency 1A Responsibility

                            'Exit Sub
                        End If
                    End If
                    Session("Msg") = Nothing
                End If
                ' BindAllControl()
                If Session("Action") Is Nothing Then
                    If Not Request.QueryString("Action") Is Nothing Then
                        Session("Action") = Request.QueryString("Action")
                        If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "U" Then

                            Session("Action") = Request.QueryString("Action").Replace(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString, objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString))

                            AgencyView()
                        End If
                    Else
                        Session("Action") = "I"
                        If Session("Action").ToString().Split("|").GetValue(0) = "U" Then

                            Session("Action") = Request.QueryString("Action").Replace(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString, objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString))

                            AgencyView()
                        End If
                    End If
                Else
                    If Not Request.QueryString("Action") Is Nothing Then
                        Session("Action") = Request.QueryString("Action")

                    End If
                    If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                        If Not Request.QueryString("Action") Is Nothing Then
                            Session("Action") = Request.QueryString("Action").Replace(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString, objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString))

                        Else
                            'Session("Action") = Request.QueryString("Action").Replace(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString, objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString))
                        End If


                        AgencyView()
                    End If
                End If
                ' lblError.Text = Session("Action").ToString()
            End If
            ApplySecurityoption()

            'Dim m As ClientScriptManager = Me.ClientScript
            'str = m.GetCallbackEventReference(Me, "args", "ReceiveServerDataManageAgency", "'this is context from server'")
            'Dim strCallback As String = "function CallServer(args,context){" + str + ";}"
            'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)
            'drpCity.Attributes.Add("OnChange", "return SendCustomerIDManageAgency('drpCity');")


            'Code Added by Mukund
            '************************************************************************
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='View Misc Documents']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='View Misc Documents']").Attributes("Value").Value)
                End If
                If Not strBuilder Is Nothing Then
                    If strBuilder(0) = "0" Then
                        btnMiscViewDoc.Disabled = True
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            'Code Added by Mukund
            '************************************************************************
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Print PType Challan']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Print PType Challan']").Attributes("Value").Value)
                End If
                If Not strBuilder Is Nothing Then
                    If strBuilder(0) = "0" Then
                        txtPtypeChallan.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If


            '************************************************************************
            'objSecurityXml.LoadXml(Session("Security"))
            'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Count <> 0 Then
            '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Attributes("Value").Value)
            '    End If
            '    If Not strBuilder Is Nothing Then
            '        If strBuilder(0) = "0" Then
            '            Button1.Disabled = True
            '        End If
            '    End If
            'Else
            '    strBuilder = objeAAMS.SecurityCheck(31)
            'End If

            '************************************************************************
            '************************************************************************
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Group Case']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Group Case']").Attributes("Value").Value)
                End If
                If Not strBuilder Is Nothing Then
                    If strBuilder(0) = "0" Then
                        btnGroupCase.Disabled = True
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            '************************************************************************
            '************************************************************************
            'objSecurityXml.LoadXml(Session("Security"))
            'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Count <> 0 Then
            '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Attributes("Value").Value)
            '    End If
            '    If Not strBuilder Is Nothing Then
            '        If strBuilder(0) = "0" Then
            'btnOrderFeasability.Enabled = False
            '        End If
            '    End If
            'Else
            '    strBuilder = objeAAMS.SecurityCheck(31)
            'End If

            '************************************************************************

            '************************************************************************
            'objSecurityXml.LoadXml(Session("Security"))
            'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Count <> 0 Then
            '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Attributes("Value").Value)
            '    End If
            '    If Not strBuilder Is Nothing Then
            '        If strBuilder(0) = "0" Then
            'btnConnectivityHistory.Disabled = True
            '        End If
            '    End If
            'Else
            '    strBuilder = objeAAMS.SecurityCheck(31)
            'End If

            '************************************************************************
            '************************************************************************
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DailyBookings']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DailyBookings']").Attributes("Value").Value)
                End If
                If Not strBuilder Is Nothing Then
                    If strBuilder(0) = "0" Then
                        btnDailyBooking.Disabled = True
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            '************************************************************************


            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity Details']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity Details']").Attributes("Value").Value)
                End If
                If Not strBuilder Is Nothing Then
                    If strBuilder(0) = "0" Then
                        btnAProductivity.Disabled = True
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Attributes("Value").Value)
                End If
                If Not strBuilder Is Nothing Then
                    If strBuilder(0) = "0" Then
                        btnAllCrsProductivity.Disabled = True
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OrderFeasibilityReport']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OrderFeasibilityReport']").Attributes("Value").Value)
                End If
                If Not strBuilder Is Nothing Then
                    If strBuilder(0) = "0" Then
                        btnOrderFeasability.Disabled = True
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            '***************************************************************************
            Dim LimAoff, LimReg, LimOwnOff As String
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            LimAoff = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        Else
                            LimAoff = ""

                        End If
                    Else
                        LimAoff = ""
                    End If
                Else
                    LimAoff = ""
                End If

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            LimReg = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            LimReg = ""
                        End If
                    Else
                        LimReg = ""
                    End If
                Else
                    LimReg = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        LimOwnOff = "1"
                    Else
                        LimOwnOff = "0"
                    End If
                    'objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                Else
                    LimOwnOff = "0"
                End If
            End If



            'Security Check by Mukund Called a function
            CheckSecurity()
            'End of Security Check

            'hdLimAoff.Value = LimAoff
            'hdLimReg.Value = LimReg
            'hdLimOwnOff.Value = LimOwnOff


            'Code by Mukund for Daily Booking Link
            If hdLcode.Value.Trim.Length > 0 Then
                hdEnLcode.Value = objED.Encrypt(hdLcode.Value.Trim)

            End If
            If txtAoffice.Text.Trim.Trim.Length > 0 Then
                hdEnAoffice.Value = objED.Encrypt(txtAoffice.Text.Trim)
            End If

            'Encryption Code Segment



        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub ApplySecurityoption()
        Try
            ' #######################################
            ' ########## This code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
            objSecurityXml.LoadXml(Session("Security"))

            ''@ #################   Security For Agency Details

            '@ #################   Security For Modify Agency Contact Details

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Attributes("Value").Value)
                    If (Session("Action") IsNot Nothing) Then
                        If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                            If strBuilder(2) = "0" Then
                                txtAddress1.CssClass = "textboxgrey"
                                txtAddress1.ReadOnly = True
                                txtAddress2.CssClass = "textboxgrey"
                                txtAddress2.ReadOnly = True
                                txtName.CssClass = "textboxgrey"
                                txtName.ReadOnly = True

                                txtPinCode.CssClass = "textboxgrey"
                                txtPinCode.ReadOnly = True
                                txtOfficeName.CssClass = "textboxgrey"
                                txtOfficeName.ReadOnly = True
                                'drpStatus.Enabled = False
                                'drpStatus.CssClass = "textboxgrey"

                                drpCustomerCategory.Enabled = False
                                drpCustomerCategory.CssClass = "textboxgrey"

                                img2.Attributes.Add("onClick", "<script>Javascript:'#';</script>")


                                IMG1.Attributes.Add("onClick", "<script>Javascript:'#';</script>")
                            Else
                                txtAddress1.CssClass = "textbox"
                                txtAddress1.ReadOnly = False
                                txtAddress2.CssClass = "textbox"
                                txtAddress2.ReadOnly = False
                                txtName.CssClass = "textbox"
                                txtName.ReadOnly = False

                                txtPinCode.CssClass = "textbox"
                                txtPinCode.ReadOnly = False
                                txtOfficeName.CssClass = "textbox"
                                txtOfficeName.ReadOnly = False
                                'drpStatus.Enabled = True
                                'drpStatus.CssClass = "textbox"

                                drpCustomerCategory.Enabled = True
                                drpCustomerCategory.CssClass = "textbox"

                                '   IMG1.Attributes.Add("onClick", "<script>Javascript:'#';</script>")
                            End If
                        End If
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                txtAddress1.CssClass = "textbox"
                txtAddress1.ReadOnly = False
                txtAddress2.CssClass = "textbox"
                txtAddress2.ReadOnly = False
                txtName.CssClass = "textbox"
                txtName.ReadOnly = False

                txtPinCode.CssClass = "textbox"
                txtPinCode.ReadOnly = False
                txtOfficeName.CssClass = "textbox"
                txtOfficeName.ReadOnly = False
                'drpStatus.Enabled = True
                'drpStatus.CssClass = "textbox"
                drpCustomerCategory.Enabled = True
                drpCustomerCategory.CssClass = "textbox"

                '  IMG1.Attributes.Add("onClick", "<script>Javascript:'#';</script>")
            End If
            '@ #################  End of  Security For Agency Details

            '@ #################   Security For Modify Agency Contact Details

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Agency Contact Info']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Agency Contact Info']").Attributes("Value").Value)
                    If (Session("Action") IsNot Nothing) Then
                        If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                            If strBuilder(2) = "0" Then
                                drpCity.Enabled = False
                                drpCity.CssClass = "textboxgrey"
                                txtEmail.ReadOnly = True
                                txtEmail.CssClass = "textboxgrey"
                                txtPhone.ReadOnly = True
                                txtPhone.CssClass = "textboxgrey"
                                txtFax.ReadOnly = True
                                txtFax.CssClass = "textboxgrey"
                                txtIataId.ReadOnly = True
                                txtIataId.CssClass = "textboxgrey"
                                txtWevSite.ReadOnly = True
                                txtWevSite.CssClass = "textboxgrey"
                                drpStatus.Enabled = False
                                drpStatus.CssClass = "textboxgrey"

                                '@ Added By Abhishek on 22/09/09
                                txtPanNo.ReadOnly = True
                                txtPanNo.CssClass = "textboxgrey"

                                drpIataStatus.Enabled = False
                                drpIataStatus.CssClass = "textboxgrey"



                            Else
                                drpCity.Enabled = True
                                drpCity.CssClass = "textbox"
                                txtEmail.ReadOnly = False
                                txtEmail.CssClass = "textbox"
                                txtPhone.ReadOnly = False
                                txtPhone.CssClass = "textbox"
                                txtFax.ReadOnly = False
                                txtFax.CssClass = "textbox"
                                txtIataId.ReadOnly = False
                                txtIataId.CssClass = "textbox"
                                txtWevSite.ReadOnly = False
                                txtWevSite.CssClass = "textbox"
                                drpStatus.Enabled = True
                                drpStatus.CssClass = "textbox"

                                '@ Added By Abhishek on 22/09/09
                                txtPanNo.ReadOnly = False
                                txtPanNo.CssClass = "textbox"

                                drpIataStatus.Enabled = True
                                drpIataStatus.CssClass = "textbox"

                            End If
                        End If
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                drpCity.Enabled = True
                drpCity.CssClass = "textbox"
                txtEmail.ReadOnly = False
                txtEmail.CssClass = "textbox"
                txtPhone.ReadOnly = False
                txtPhone.CssClass = "textbox"
                txtFax.ReadOnly = False
                txtFax.CssClass = "textbox"
                txtIataId.ReadOnly = False
                txtIataId.CssClass = "textbox"
                txtWevSite.ReadOnly = False
                txtWevSite.CssClass = "textbox"
                drpStatus.Enabled = True
                drpStatus.CssClass = "textbox"

                '@ Added By Abhishek on 22/09/09
                txtPanNo.ReadOnly = False
                txtPanNo.CssClass = "textbox"

                drpIataStatus.Enabled = True
                drpIataStatus.CssClass = "textbox"

            End If
            '@ #################  End of   Security For Modify Agency Contact Details


            '@ #################   Security For Modify Agency Amadeus Specific Details
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Amadeus Specific']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Amadeus Specific']").Attributes("Value").Value)
                    If (Session("Action") IsNot Nothing) Then
                        If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                            If strBuilder(2) = "0" Then
                                drpCity.Enabled = False
                                drpCity.CssClass = "textboxgrey"
                                txtAoffice.ReadOnly = True
                              
                                'drpIataStatus.Enabled = False
                                'drpIataStatus.CssClass = "textboxgrey"

                                txtDateOnline.ReadOnly = True
                                txtDateOnline.CssClass = "textboxgrey"
                                txtDateOffline.ReadOnly = True
                                txtDateOffline.CssClass = "textboxgrey"
                               

                                '@ #################   Security For Modify Agency 1A Responsibility
                                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify 1AResponsibiltiy']").Count <> 0 Then
                                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify 1AResponsibiltiy']").Attributes("Value").Value)
                                    If strBuilder(2) = "0" Then

                                        img1A.Attributes.Add("onClick", "<script>Javascript:'#';</script>")
                                    End If
                                End If
                                '@ #################  End  Security For Modify Agency 1A Responsibility
                              
                                imgDateOffline.Disabled = True
                                imgDateOnline.Disabled = True

                            ElseIf strBuilder(2) = "1" Then
                                drpCity.Enabled = True
                                drpCity.CssClass = "textbox"
                                txtAoffice.ReadOnly = False
                              
                                'drpIataStatus.Enabled = True
                                'drpIataStatus.CssClass = "textbox"

                                txtDateOnline.ReadOnly = False
                                txtDateOnline.CssClass = "textbox"
                                txtDateOffline.ReadOnly = False
                                txtDateOffline.CssClass = "textbox"
                                imgDateOffline.Disabled = False
                                imgDateOnline.Disabled = False
                                '@ #################   Security For Modify Agency 1A Responsibility
                                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify 1AResponsibiltiy']").Count <> 0 Then
                                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify 1AResponsibiltiy']").Attributes("Value").Value)
                                    If strBuilder(2) = "0" Then

                                        img1A.Attributes.Add("onClick", "<script>Javascript:'#';</script>")
                                    End If
                                End If
                                '@ #################  End  Security For Modify Agency 1A Responsibility
                            End If
                        End If
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                drpCity.Enabled = True
                drpCity.CssClass = "textbox"
                txtAoffice.ReadOnly = False
              
                'drpIataStatus.Enabled = True
                'drpIataStatus.CssClass = "textbox"
                txtDateOnline.ReadOnly = False

                txtDateOnline.CssClass = "textbox"
                txtDateOffline.ReadOnly = False
                txtDateOffline.CssClass = "textbox"
                imgDateOffline.Disabled = False
                imgDateOnline.Disabled = False
            End If


            '@ #################  End of  Security For Modify Agency Amadeus Specific Details

            '@ #################   Security For Modify Agency  Connectivity


            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Connectivity Info']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Connectivity Info']").Attributes("Value").Value)
                    If (Session("Action") IsNot Nothing) Then
                        If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                            If strBuilder(2) = "0" Then
                                drpPrimaryOnlineStatus.Enabled = False
                                drpPrimaryOnlineStatus.CssClass = "textboxgrey"
                                drpBackupOnlineStatus.Enabled = False
                                drpBackupOnlineStatus.CssClass = "textboxgrey"
                                txtPrimaryOrderNumber.ReadOnly = True
                                txtPrimaryOrderNumber.CssClass = "textboxgrey"
                                txtBackupOrderNumber.ReadOnly = True
                                txtBackupOrderNumber.CssClass = "textboxgrey"
                                txtPrimaryDate.ReadOnly = True
                                txtPrimaryDate.CssClass = "textboxgrey"
                                txtBackupDate.ReadOnly = True
                                txtBackupDate.CssClass = "textboxgrey"
                                ' imgPrimaryDate.Visible = False
                                'imgPrimaryDate2.Visible = True
                                ' imgBackupDate.Visible = False
                                'imgBackupDate2.Visible = True
                                imgPrimaryDate.Disabled = True

                                imgBackupDate.Disabled = True

                            Else
                                drpPrimaryOnlineStatus.Enabled = True
                                drpPrimaryOnlineStatus.CssClass = "textbox"
                                drpBackupOnlineStatus.Enabled = True
                                drpBackupOnlineStatus.CssClass = "textbox"
                                txtPrimaryOrderNumber.ReadOnly = False
                                txtPrimaryOrderNumber.CssClass = "textbox"
                                txtBackupOrderNumber.ReadOnly = False
                                txtBackupOrderNumber.CssClass = "textbox"
                                txtPrimaryDate.ReadOnly = False
                                txtPrimaryDate.CssClass = "textbox"
                                txtBackupDate.ReadOnly = False
                                txtBackupDate.CssClass = "textbox"
                                ' imgPrimaryDate.Visible = True
                                'imgPrimaryDate2.Visible = False
                                ' imgBackupDate.Visible = True
                                'imgBackupDate2.visible = False
                                imgPrimaryDate.Disabled = False

                                imgBackupDate.Disabled = False

                            End If
                        End If
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                drpPrimaryOnlineStatus.Enabled = True
                drpPrimaryOnlineStatus.CssClass = "textbox"
                drpBackupOnlineStatus.Enabled = True
                drpBackupOnlineStatus.CssClass = "textbox"
                txtPrimaryOrderNumber.ReadOnly = False
                txtPrimaryOrderNumber.CssClass = "textbox"
                txtBackupOrderNumber.ReadOnly = False
                txtBackupOrderNumber.CssClass = "textbox"
                txtPrimaryDate.ReadOnly = False
                txtPrimaryDate.CssClass = "textbox"
                txtBackupDate.ReadOnly = False
                txtBackupDate.CssClass = "textbox"
                ' imgPrimaryDate.Visible = True
                'imgPrimaryDate2.Visible = False
                'imgBackupDate.Visible = True
                ' imgBackupDate2.Visible = False
                imgPrimaryDate.Disabled = False

                imgBackupDate.Disabled = False

            End If
            '@ #################   end of Security For Modify Agency Connectivity

            '@ #################   Security For Modify Agency View Misc Documents
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='View Misc Documents']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='View Misc Documents']").Attributes("Value").Value)
                    If (Session("Action") IsNot Nothing) Then
                        If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                            If strBuilder(0) = "0" Then
                                btnMiscViewDoc.Disabled = True
                                btnMiscViewDoc.Attributes.Add("onClick", "<script>Javascript:'#';</script>")
                            End If
                        End If
                    Else
                        'If strBuilder(1) = "0" Then
                        '    btnMiscViewDoc.Attributes.Add("onClick", "<script>Javascript:'#';</script>")
                        'End If
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)

            End If
            '@ #################  End of Security For Modify View Misc Documents



            ' #######################################
            ' ########## End of code used for enable/disable 
            ' ########## the button according to rights


            '######################################
            ' @ Code Hide on 08-08-08

            '############################################################
            '### Code for Enable/Disable the drpFileNumber dropdown
            Dim strinputForFileno As String
            strinputForFileno = "<UP_GETFINENOSTATUS_INPUT><FILENO/></UP_GETFINENOSTATUS_INPUT >"
            Dim objFileInputXml, objFileOutputXml As New XmlDocument
            objFileInputXml.LoadXml(strinputForFileno)
            objFileInputXml.DocumentElement.SelectSingleNode("FILENO").InnerText = txtFileNo.Text ' drpFileNumber.SelectedValue
            'Here Back end Method Call
            objFileOutputXml = objbzAgency.ScannedFileStatus(objFileInputXml)
            If objFileOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If (objFileOutputXml.DocumentElement.SelectSingleNode("FILESTATUS").Attributes("ScannedFileStatus").Value.ToUpper = "TRUE") Then
                    'drpFileNumber.Enabled = False
                    ' drpFileNumber.CssClass = "textboxgrey"
                Else
                    'drpFileNumber.Enabled = True
                    ' drpFileNumber.CssClass = "textbox"
                End If
            Else
                If (objFileOutputXml.DocumentElement.SelectSingleNode("FILESTATUS").Attributes("ScannedFileStatus").Value.ToUpper = "TRUE") Then
                    ' drpFileNumber.Enabled = False
                    ' drpFileNumber.CssClass = "textboxgrey"
                Else
                    'drpFileNumber.Enabled = True
                    'drpFileNumber.CssClass = "textbox"
                End If
            End If
            ' @ Code Hide on 08-08-08
            '######################################

        Catch ex As Exception

        End Try
    End Sub
    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
        Return str
    End Function

    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
        Try
            Dim objbzCity As New AAMS.bizMaster.bzCity
            Dim objOutputXml As New XmlDocument
            Dim strArr() As String
            str = "|"
            strArr = eventArgument.Split("|")
            objOutputXml = objbzCity.List()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If objOutputXml.DocumentElement.SelectNodes("CITY[@CityID='" & strArr(1) & "']").Count <> 0 Then
                    str = objOutputXml.DocumentElement.SelectSingleNode("CITY[@CityID='" & strArr(1) & "']").Attributes("Country_Name").Value
                End If
                If objOutputXml.DocumentElement.SelectNodes("CITY[@CityID='" & strArr(1) & "']").Count <> 0 Then
                    str = str & "|" & objOutputXml.DocumentElement.SelectSingleNode("CITY[@CityID='" & strArr(1) & "']").Attributes("Aoffice").Value
                End If
            Else
                str = "|"
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub
    Private Sub BindAllControl()
        Try
            objeAAMS.BindDropDown(drpCity, "CITY", True, 1)
            objeAAMS.BindDropDown(drpStatus, "AGENCYSTATUS", True, 2)
            objeAAMS.BindDropDown(drpIataStatus, "PRIORITY", True, 2)
            ' objeAAMS.BindDropDown(drpType, "AGENCYTYPE", True, 2)
            objeAAMS.BindDropDown(drpPrimaryOnlineStatus, "ONLINESTATUSCODE", True, 2)
            objeAAMS.BindDropDown(drpBackupOnlineStatus, "ONLINESTATUSCODE", True, 2)
            '  objeAAMS.BindDropDown(drpFileNumber, "FileNo", True, 2)
            objeAAMS.BindDropDown(drpCustomerCategory, "CUSTOMERCATEGORY", True, 2)

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            'objeAAMS = Nothing
        End Try

    End Sub
    '*********************************************************************************************************
    '****************************Method for View Agency *****************************************************
    '*********************************************************************************************************
    Private Sub AgencyView()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
        Dim li As ListItem
        Dim strSession As String
        ' Dim chk As String
        Const strMETHOD_NAME As String = "AgencyView"
        'Dim objbizShared As AAMS.bizShared.bzShared
        Try

            ' 

            objInputXml.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerXml = Session("Action").ToString().Split("|").GetValue(1)
            'Here Back end Method Call
            'hdOrderID.Value = Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString().ToUpper()
            'hdCity.Value = Request.QueryString("Action").ToString().Split("|").GetValue(2).ToString()
            'Session("a") = hdCity.Value

            hdLcode.Value = Session("Action").ToString().Split("|").GetValue(1)
            objOutputXml = objbzAgency.View(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXml.DocumentElement.SelectSingleNode("Agency")

                    'Added by Mukund 
                    txtIPAddress.Text = .Attributes("IPAddress").Value()
                    hdIPAddressID.Value = .Attributes("IPAddressID").Value()
                    'Added by Mukund 
                    'Added by Pankaj 
                    txtGroupClassification.Text = .Attributes("Group_Classification_Name").Value()

                    txtAgencyGroup.Text = .Attributes("Chain_Name").Value()
                    hdChainId.Value = .Attributes("Chain_Code").Value()
                    txtName.Text = .Attributes("NAME").Value()
                    txtAddress1.Text = .Attributes("ADDRESS").Value()
                    txtAddress2.Text = .Attributes("ADDRESS1").Value()
                    ' drpCity.SelectedItem.Value = .Attributes("CITY").Value()

                    If .Attributes("COMP_VERTICAL") IsNot Nothing Then
                        TxtCompVertical.Text = .Attributes("COMP_VERTICAL").Value()
                    End If


                    Session("ContactPID") = objOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes("CONTACT_PERSON_ID").Value.Trim()

                    li = drpCity.Items.FindByText(.Attributes("CITY").Value())

                    If (li IsNot Nothing) Then
                        drpCity.SelectedValue = li.Value 'li.Selected = True
                    End If
                    hdCity.Value = .Attributes("CITY").Value() 'Request("drpCity") ' Request.QueryString("Action").ToString().Split("|").GetValue(2).ToString()
                    'Session("a") = hdCity.Value
                    Session("CityName") = hdCity.Value
                    txtCountry.Text = .Attributes("COUNTRY").Value()
                    txtEmail.Text = .Attributes("EMAIL").Value()
                    txtPhone.Text = .Attributes("PHONE").Value()
                    txtFax.Text = .Attributes("FAX").Value()
                    txtPinCode.Text = .Attributes("PINCODE").Value()
                    txtOfficeName.Text = .Attributes("LOCATION_SHORT_NAME").Value()
                    drpStatus.SelectedValue = .Attributes("AGENCYSTATUSID").Value()
                    txtIataId.Text = .Attributes("IATA_TID").Value()
                    txtWevSite.Text = .Attributes("WWW_ADDRESS").Value()
                    txtAoffice.Text = .Attributes("Aoffice").Value()
                    'drpType.SelectedValue = .Attributes("AGENCYTYPEID").Value()
                    If .Attributes("DATE_ONLINE").Value() <> "" Then
                        txtDateOnline.Text = objeAAMS.ConvertDate(.Attributes("DATE_ONLINE").Value).ToString("dd/MM/yyyy")
                    End If
                    'If .Attributes("INCLUDE_IN_CCR").Value().ToUpper = "TRUE" Then
                    '    rdCCRoster.Checked = True
                    '    txtReason.Enabled = True
                    '    txtReason.CssClass = "textbox"
                    '    txtReason.Text = .Attributes("INCLUDE_IN_CCR_REASON").Value()
                    'Else
                    '    rdCCRoster.Checked = False
                    '    txtReason.Enabled = False
                    '    txtReason.Text = ""
                    '    txtReason.CssClass = "textboxgrey"
                    'End If

                    txtAResponsibility.Text = .Attributes("RESP_1A_NAME").Value()
                    hdRespId.Value = .Attributes("RESP_1A").Value()
                    drpIataStatus.SelectedValue = .Attributes("PRIORITYID").Value()
                    If .Attributes("DATE_OFFLINE").Value() <> "" Then
                        txtDateOffline.Text = objeAAMS.ConvertDate(.Attributes("DATE_OFFLINE").Value).ToString("dd/MM/yyyy")
                    End If
                    txtFileNo.Text = .Attributes("FILENO").Value()

                    'If (.Attributes("FILENO").Value() <> "") Then
                    '    li = drpFileNumber.Items.FindByText(.Attributes("FILENO").Value())
                    '    If (li Is Nothing) Then
                    '        drpFileNumber.Items.Insert(0, New ListItem(.Attributes("FILENO").Value(), .Attributes("FILENO").Value()))
                    '    End If
                    '    drpFileNumber.SelectedValue = .Attributes("FILENO").Value()
                    'End If
                    'drpFileNumber.SelectedValue = .Attributes("FILENO").Value() '"9999"


                    '######################################
                    ' @ Code Hide on 08-08-08

                    '############################################################
                    '### Code for Enable/Disable the drpFileNumber dropdown
                    Dim strinputForFileno As String
                    strinputForFileno = "<UP_GETFINENOSTATUS_INPUT><FILENO/></UP_GETFINENOSTATUS_INPUT >"
                    Dim objFileInputXml, objFileOutputXml As New XmlDocument
                    objFileInputXml.LoadXml(strinputForFileno)
                    objFileInputXml.DocumentElement.SelectSingleNode("FILENO").InnerText = txtFileNo.Text ' drpFileNumber.SelectedValue
                    'Here Back end Method Call
                    objFileOutputXml = objbzAgency.ScannedFileStatus(objFileInputXml)
                    If objFileOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        If (objFileOutputXml.DocumentElement.SelectSingleNode("FILESTATUS").Attributes("ScannedFileStatus").Value.ToUpper = "TRUE") Then
                            ' drpFileNumber.Enabled = False
                            '  drpFileNumber.CssClass = "textboxgrey"
                        Else
                            'drpFileNumber.Enabled = True
                            ' drpFileNumber.CssClass = "textbox"
                        End If
                    Else
                        If (objFileOutputXml.DocumentElement.SelectSingleNode("FILESTATUS").Attributes("ScannedFileStatus").Value.ToUpper = "TRUE") Then
                            ' drpFileNumber.Enabled = False
                            ' drpFileNumber.CssClass = "textboxgrey"
                        Else
                            'drpFileNumber.Enabled = True
                            'drpFileNumber.CssClass = "textbox"
                        End If
                    End If
                    ' @ Code Hide on 08-08-08
                    '######################################



                    '@ #################   Security For Modify Agency View Misc Documents
                    Dim objSecurityXml As New XmlDocument
                    objSecurityXml.LoadXml(Session("Security"))
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                        If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Fileno']").Count <> 0 Then
                            strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Fileno']").Attributes("Value").Value)
                            If strBuilder(2) = "0" Then
                                ' drpFileNumber.Enabled = False
                                ' drpFileNumber.CssClass = "textboxgrey"
                            Else
                                ' drpFileNumber.Enabled = True
                                ' drpFileNumber.CssClass = "dropdown"
                            End If
                        End If
                    End If
                    '@ #################  End of Security For Modify View Misc Documents


                    '############################################################

                    drpPrimaryOnlineStatus.SelectedValue = .Attributes("ONLINE_STATUS").Value()
                    drpBackupOnlineStatus.SelectedValue = .Attributes("ONLINE_STATUS_BACKUP").Value()
                    If .Attributes("INSTALL_DATE_PRIMARY").Value() <> "" Then
                        txtPrimaryDate.Text = objeAAMS.ConvertDate(.Attributes("INSTALL_DATE_PRIMARY").Value()).ToString("dd/MM/yyyy")
                    End If
                    If .Attributes("INSTALL_DATE_BACKUP").Value() <> "" Then
                        txtBackupDate.Text = objeAAMS.ConvertDate(.Attributes("INSTALL_DATE_BACKUP").Value()).ToString("dd/MM/yyyy")
                    End If
                    txtPrimaryOrderNumber.Text = .Attributes("ORDERNUMBER_PRIMARY").Value()
                    txtBackupOrderNumber.Text = .Attributes("ORDERNUMBER_BACKUP").Value()

                    hdFileNo.Value = txtFileNo.Text ' drpFileNumber.SelectedValue 'txtFileNumber.Text '.Attributes("FILENO").Value()


                    ' ######################## New Code Added For Customer Category 05/03/08

                    Dim CustomerCategory As ListItem
                    CustomerCategory = drpCustomerCategory.Items.FindByValue(.Attributes("CUSTOMER_CATEGORY_ID").Value())
                    If CustomerCategory IsNot Nothing Then
                        drpCustomerCategory.SelectedValue = CustomerCategory.Value
                        'If CustomerCategory.Selected = False Then
                        '    CustomerCategory.Selected = True
                        'End If
                    End If
                    ' ######################## End of New Code Added For Customer Category 05/03/08



                    '@ Added On 22/09/09 By Abhishek
                    If .Attributes("PANNO") IsNot Nothing Then
                        txtPanNo.Text = .Attributes("PANNO").Value
                    End If




                    strSession = Session("Action").ToString()
                    'lblError.Text = Session("Action").ToString()
                    'RebidnAofficeAndCountryByCityId(drpCity.SelectedValue)
                    ' Session("Action") = Session("Action").ToString() + "|" + hdFileNo.Value
                    'Session("Action") = strSession.ToString().Split("|")(0) & "|" & strSession.ToString().Split("|")(1) & "|" & drpFileNumber.SelectedValue & "|" & txtName.Text '.Value

                    Session("Action") = strSession.ToString().Split("|")(0) & "|" & strSession.ToString().Split("|")(1) & "|" & txtFileNo.Text & "|" & txtName.Text '.Value
                    'lblError.Text = Session("Action")
                    'lblError.Text = ""
                End With
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception

            AAMS.bizShared.bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, ex)
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            objbzAgency = Nothing
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveAgency()
    End Sub
    Private Sub SaveAgency()
        Dim objInputXml, objOutputXml As New XmlDocument
        Const strMETHOD_NAME As String = "SaveAgency"
        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
        objInputXml.LoadXml("<TA_UPDATEAGENCY_INPUT><Agency LOCATION_CODE='' Chain_Name='' Chain_Code='' IATA_TID='' EMAIL='' FAX='' PHONE='' COUNTRY='' PINCODE='' CITY='' ADDRESS1='' ADDRESS='' NAME='' Aoffice='' CONTACT_PERSON_ID='' ORDERNUMBER_BACKUP='' ORDERNUMBER_PRIMARY='' INSTALL_DATE_BACKUP='' INSTALL_DATE_PRIMARY='' ONLINE_STATUS_BACKUP='' AGENCYSTATUSID='' AGENCYTYPEID='' LOCATION_SHORT_NAME='' PRIORITYID='' RESP_1A='' RESP_1A_NAME='' WWW_ADDRESS='' ONLINE_STATUS='' DATE_OFFLINE='' DATE_ONLINE='' FILENO='' INCLUDE_IN_CCR='' INCLUDE_IN_CCR_REASON='' CUSTOMER_CATEGORY_ID='' EMPLOYEEID='' IPAddressID='' IPAddress='' PANNO=''  /></TA_UPDATEAGENCY_INPUT>")
        Try
            With objInputXml.DocumentElement.SelectSingleNode("Agency")
                If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                    .Attributes("LOCATION_CODE").Value() = Session("Action").ToString().Split("|").GetValue(1)
                End If
                .Attributes("Chain_Name").Value() = (Request.Form("txtAgencyGroup")) 'Request.Form("txtAgencyGroup") 'Server.HtmlEncode(Request.Form("txtAgencyGroup")) 'Replace(Request.Form("txtAgencyGroup"), "&", "")

                'Added by Mukund 
                .Attributes("IPAddressID").Value = hdIPAddressID.Value
                .Attributes("IPAddress").Value = Request.Form("txtIPAddress")
                'Added by Mukund 

                .Attributes("Chain_Code").Value() = hdChainId.Value
                .Attributes("NAME").Value() = (txtName.Text) 'txtName.Text 'Server.HtmlEncode(txtName.Text) 'Replace(txtName.Text, "&", "")
                .Attributes("ADDRESS").Value() = (txtAddress1.Text)
                .Attributes("ADDRESS1").Value() = (txtAddress2.Text)
                .Attributes("CITY").Value() = drpCity.SelectedItem.Text ' drpCity.SelectedValue
                .Attributes("COUNTRY").Value() = Request.Form("txtCountry")
                .Attributes("EMAIL").Value() = txtEmail.Text
                .Attributes("PHONE").Value() = txtPhone.Text
                .Attributes("FAX").Value() = txtFax.Text
                .Attributes("PINCODE").Value() = txtPinCode.Text

                .Attributes("AGENCYSTATUSID").Value() = drpStatus.SelectedValue
                .Attributes("IATA_TID").Value() = txtIataId.Text
                .Attributes("WWW_ADDRESS").Value() = txtWevSite.Text
                .Attributes("Aoffice").Value() = IIf(Request.Form("txtAoffice") IsNot Nothing, Request.Form("txtAoffice"), txtAoffice.Text)
                '.Attributes("AGENCYTYPEID").Value() = drpType.SelectedValue
                .Attributes("LOCATION_SHORT_NAME").Value() = (txtOfficeName.Text)

                If txtDateOnline.Text <> "" Then
                    .Attributes("DATE_ONLINE").Value() = objeAAMS.ConvertTextDate(txtDateOnline.Text)
                End If
                ' If rdCCRoster.Checked = True Then
                '    .Attributes("INCLUDE_IN_CCR").Value() = "True"
                '  .Attributes("INCLUDE_IN_CCR_REASON").Value() = txtReason.Text
                ' Else
                .Attributes("INCLUDE_IN_CCR").Value() = "False"
                .Attributes("INCLUDE_IN_CCR_REASON").Value() = ""
                '  End If
                ' .Attributes("INCLUDE_IN_CCR_REASON").Value() = txtReason.Text
                .Attributes("RESP_1A").Value() = hdRespId.Value
                .Attributes("RESP_1A_NAME").Value() = Request.Form("txtAResponsibility")
                .Attributes("PRIORITYID").Value() = drpIataStatus.SelectedValue
                If txtDateOffline.Text <> "" Then
                    .Attributes("DATE_OFFLINE").Value() = objeAAMS.ConvertTextDate(txtDateOffline.Text)
                End If
                .Attributes("FILENO").Value() = txtFileNo.Text 'drpFileNumber.SelectedValue
                .Attributes("ONLINE_STATUS").Value() = drpPrimaryOnlineStatus.SelectedValue
                .Attributes("ONLINE_STATUS_BACKUP").Value() = drpBackupOnlineStatus.SelectedValue
                If txtPrimaryDate.Text <> "" Then
                    .Attributes("INSTALL_DATE_PRIMARY").Value() = objeAAMS.ConvertTextDate(txtPrimaryDate.Text)
                End If
                If txtBackupDate.Text <> "" Then
                    .Attributes("INSTALL_DATE_BACKUP").Value() = objeAAMS.ConvertTextDate(txtBackupDate.Text)
                End If
                .Attributes("ORDERNUMBER_PRIMARY").Value() = txtPrimaryOrderNumber.Text
                .Attributes("ORDERNUMBER_BACKUP").Value() = txtBackupOrderNumber.Text

                If Not Session("LoginSession") Is Nothing Then

                    .Attributes("EMPLOYEEID").Value = Session("LoginSession").ToString().Split("|")(0)

                End If

                ' ######################## New Code Added For Customer Category 05/03/08

                If drpCustomerCategory.SelectedIndex <> 0 Then
                    .Attributes("CUSTOMER_CATEGORY_ID").Value = drpCustomerCategory.SelectedValue
                End If



                '@ Added On 22/09/09 By Abhishek
                .Attributes("PANNO").Value = txtPanNo.Text





                ' ######################## End of New Code Added For Customer Category 05/03/08

            End With

            'Here Back end Method Call
            objOutputXml = objbzAgency.Update(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                    lblError.Text = objeAAMSMessage.messUpdate

                    'Session("Action") = "U|" & objOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes("LOCATION_CODE").Value & "|" & drpFileNumber.SelectedValue & "|" & txtName.Text
                    Session("Action") = "U|" & objOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes("LOCATION_CODE").Value & "|" & txtFileNo.Text & "|" & txtName.Text

                Else
                    lblError.Text = objeAAMSMessage.messInsert

                    '  Session("Action") = "U|" & objOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes("LOCATION_CODE").Value & "|" & drpFileNumber.SelectedValue & "|" & txtName.Text
                    Session("Action") = "U|" & objOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes("LOCATION_CODE").Value & "|" & txtFileNo.Text & "|" & txtName.Text


                    'Response.Redirect("TAUP_Agency.aspx?Action=U|" & objOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes("LOCATION_CODE").Value, False)
                    Response.Redirect("TAUP_Agency.aspx?Action=U|" & objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes("LOCATION_CODE").Value), False)

                    Session("Msg") = objeAAMSMessage.messInsert
                End If
                AgencyView()
            Else

                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
            txtCountry.Text = objInputXml.DocumentElement.SelectSingleNode("Agency").Attributes("COUNTRY").Value
            txtAgencyGroup.Text = Server.HtmlDecode(objInputXml.DocumentElement.SelectSingleNode("Agency").Attributes("Chain_Name").Value)
            txtAoffice.Text = objInputXml.DocumentElement.SelectSingleNode("Agency").Attributes("Aoffice").Value
            txtAResponsibility.Text = objInputXml.DocumentElement.SelectSingleNode("Agency").Attributes("RESP_1A_NAME").Value

            If Request.Form("txtGroupClassification").Trim <> "" Then
                txtGroupClassification.Text = Request.Form("txtGroupClassification")
            End If


            'If (rdCCRoster.Checked = True) Then
            '    txtReason.Enabled = True
            '    txtReason.CssClass = "textbox"
            'Else
            '    txtReason.Enabled = False
            '    txtReason.Text = ""
            '    txtReason.CssClass = "textboxgrey"
            'End If
            ApplySecurityoption()

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
            AAMS.bizShared.bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, ex)
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            objbzAgency = Nothing
        End Try
    End Sub

    Protected Sub txtPtypeChallan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPtypeChallan.Click
        Dim objOxml As New XmlDocument
        Dim objInputXml As New XmlDocument
        Dim objOutputXml As New XmlDocument
        Dim intLcode As Integer = 0, strOfficeID As String = ""
        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency


        If Session("Action") IsNot Nothing Then
            If Session("Action").ToString().Split("|").Length > 1 Then
                intLcode = Session("Action").ToString().Split("|").GetValue(1)
            End If
        End If

        If intLcode <> 0 Then
            objInputXml.LoadXml("<UP_GETCRSDETAILS_INPUT><LCODE>" & intLcode & "</LCODE></UP_GETCRSDETAILS_INPUT>")
            objOutputXml = objbzAgency.GetAgencyCrsUseDetails(objInputXml)
            If objOutputXml.DocumentElement.SelectNodes("CRS[@CURRENTID='Y']").Count <> 0 Then
                strOfficeID = (objOutputXml.DocumentElement.SelectSingleNode("CRS[@CURRENTID='Y']").Attributes("OFFICEID").Value)
            End If
        End If

        objOxml.LoadXml("<RP_PTYPECHALLAN_OUTPUT><PTYPECHALLAN AgencyName='' Address='' TelNo='' FaxNo='' City='' Country='' ApprovedBy='' ReceivedBY='' OfficeID='' ChallanNumber='' Branch=''  OrderNo='' OrderType = '' OrderQty='' OrderRemark = ''  /></RP_PTYPECHALLAN_OUTPUT>")
        Try
            objOxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("AgencyName").Value = txtName.Text.Trim()
            objOxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("Address").Value = txtAddress1.Text.Trim() & " " & txtAddress2.Text.Trim()
            objOxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("TelNo").Value = txtPhone.Text.Trim()
            objOxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("FaxNo").Value = txtFax.Text.Trim()
            objOxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("City").Value = drpCity.SelectedItem.ToString()
            objOxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("Country").Value = txtCountry.Text.Trim()
            objOxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("ApprovedBy").Value = ""
            objOxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("ReceivedBY").Value = ""
            objOxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("OfficeID").Value = strOfficeID
            objOxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("ChallanNumber").Value = ""
            objOxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("Branch").Value = txtAoffice.Text.Trim()
            objOxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("OrderNo").Value = ""
            objOxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("OrderType").Value = ""
            objOxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("OrderQty").Value = ""
            objOxml.DocumentElement.SelectSingleNode("PTYPECHALLAN").Attributes("OrderRemark").Value = ""

            Session("ePTypeChallan") = objOxml.OuterXml
            Response.Redirect("../RPSR_ReportShow.aspx?Case=PTypeChallan", False)
        Catch ex As Exception
            lblError.Text = ex.Message.ToString()
        Finally
            objOxml = Nothing
            objInputXml = Nothing
            objOutputXml = Nothing
            objbzAgency = Nothing
        End Try
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        'Try
        '    Session("Action") = "I"
        '    Response.Redirect("TAUP_Agency.aspx?Action=I", False)
        'Catch ex As Exception
        '    lblError.Text = ex.Message
        'End Try
    End Sub


    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If (Request.QueryString("Action") IsNot Nothing) Then
                If (Request.QueryString("Action") = "I") Then
                    cleardata()
                    Exit Sub
                End If
            End If
            If Session("Action") Is Nothing Then
                If Not Request.QueryString("Action") Is Nothing Then
                    Session("Action") = Request.QueryString("Action")

                    If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "U" Then
                        AgencyView()
                    End If
                Else
                    If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                        AgencyView()
                    End If
                End If
            Else
                If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                    AgencyView()
                End If
            End If
            ApplySecurityoption()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub cleardata()
        Try
            txtName.Text = ""
            txtAddress1.Text = ""
            txtAddress2.Text = ""
            drpCity.SelectedIndex = 0
            txtEmail.Text = ""
            txtPhone.Text = ""
            txtFax.Text = ""
            txtPinCode.Text = ""
            txtOfficeName.Text = ""
            drpStatus.SelectedIndex = 0
            txtIataId.Text = ""
            txtWevSite.Text = ""
            ' drpType.SelectedIndex = 0
            txtDateOnline.Text = ""
            'rdCCRoster.Checked = False
            ' txtReason.Enabled = False
            'txtReason.CssClass = "textboxgrey"
            'txtReason.Text = ""
            drpIataStatus.SelectedIndex = 0
            txtDateOffline.Text = ""
            'drpFileNumber.SelectedValue
            drpPrimaryOnlineStatus.SelectedIndex = 0
            drpBackupOnlineStatus.SelectedIndex = 0
            txtPrimaryDate.Text = ""
            txtBackupDate.Text = ""
            txtPrimaryOrderNumber.Text = ""
            txtBackupOrderNumber.Text = ""
            'drpFileNumber.CssClass = "textbox"
            'drpFileNumber.Enabled = True
            drpCustomerCategory.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try


    End Sub
    Private Sub RebidnAofficeAndCountryByCityId(ByVal CityId As String)
        Try
            Dim objbzCity As New AAMS.bizMaster.bzCity
            Dim objOutputXml As New XmlDocument
            objOutputXml = objbzCity.List()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If objOutputXml.DocumentElement.SelectNodes("CITY[@CityID='" & CityId & "']").Count <> 0 Then
                    txtCountry.Text = objOutputXml.DocumentElement.SelectSingleNode("CITY[@CityID='" & CityId & "']").Attributes("Country_Name").Value
                End If
                If objOutputXml.DocumentElement.SelectNodes("CITY[@CityID='" & CityId & "']").Count <> 0 Then
                    txtAoffice.Text = (objOutputXml.DocumentElement.SelectSingleNode("CITY[@CityID='" & CityId & "']").Attributes("Aoffice").Value)
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

#Region "CheckSecurity()::This method is used for security check."
    Private Sub CheckSecurity()
        Try
            ' This code is used for checking session handler according to user login.
            'Dim strBuilder1 As New StringBuilder
            Dim A1strBuilder As New StringBuilder
            Dim ContactstrBuilder As New StringBuilder
            Dim AmadeusSpecificstrBuilder As New StringBuilder
            Dim ConnectivitystrBuilder As New StringBuilder

            Dim objSecurityXml As New XmlDocument

            objSecurityXml.LoadXml(Session("Security"))



            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Attributes("Value").Value)
            End If
            If strBuilder(1) = "0" Then
                btnNew.Enabled = False
                btnSave.Enabled = False
            End If
            If strBuilder(2) = "0" And strBuilder(1) = "0" Then 'Add =false /Modify= false
                btnSave.Enabled = False
            End If
            If strBuilder(2) = "0" And strBuilder(1) = "1" Then 'Modify =false /Add= True
                If Request.QueryString("Action") IsNot Nothing Then
                    If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "U" Then
                        btnSave.Enabled = False
                    Else
                        btnSave.Enabled = True
                    End If
                End If
            End If
            If strBuilder(2) = "1" And strBuilder(1) = "0" Then 'Modify= true/Add =false
                If Request.QueryString("Action") IsNot Nothing Then
                    If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "U" Then
                        btnSave.Enabled = True
                    Else
                        btnSave.Enabled = False
                    End If
                End If
             
            End If


            '@ #################   Security For Modify Agency 1A Responsibility
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify 1AResponsibiltiy']").Count <> 0 Then
                    A1strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify 1AResponsibiltiy']").Attributes("Value").Value)
                    If (Session("Action") IsNot Nothing) Then
                        If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                            If A1strBuilder(2) = "1" Then
                                btnSave.Enabled = True
                            End If

                        End If
                    End If
                End If
            End If
            '@ #################  End  Security For Modify Agency 1A Responsibility



            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Agency Contact Info']").Count <> 0 Then
                    ContactstrBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Agency Contact Info']").Attributes("Value").Value)
                    If (Session("Action") IsNot Nothing) Then
                        If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                            If ContactstrBuilder(2) = "1" Then
                                btnSave.Enabled = True
                            End If

                        End If
                    End If
                End If
            End If

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Amadeus Specific']").Count <> 0 Then
                    AmadeusSpecificstrBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Amadeus Specific']").Attributes("Value").Value)
                    If (Session("Action") IsNot Nothing) Then
                        If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                            If AmadeusSpecificstrBuilder(2) = "1" Then
                                btnSave.Enabled = True
                            End If
                        End If
                    End If
                End If
            End If

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Connectivity Info']").Count <> 0 Then
                    ConnectivitystrBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Connectivity Info']").Attributes("Value").Value)
                    If (Session("Action") IsNot Nothing) Then
                        If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                            If ConnectivitystrBuilder(2) = "1" Then
                                btnSave.Enabled = True
                            End If
                        End If
                    End If
                End If
            End If

            

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region


    Private Sub ApplySecurityForSomecontrols()
        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        'Code Added by Mukund
        '************************************************************************
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Print PType Challan']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Print PType Challan']").Attributes("Value").Value)
            End If
            If Not strBuilder Is Nothing Then
                If strBuilder(0) = "0" Then
                    txtPtypeChallan.Enabled = False
                End If
            End If
        Else
            strBuilder = objeAAMS.SecurityCheck(31)
        End If


        '************************************************************************
        'objSecurityXml.LoadXml(Session("Security"))
        'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
        '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Count <> 0 Then
        '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Attributes("Value").Value)
        '    End If
        '    If Not strBuilder Is Nothing Then
        '        If strBuilder(0) = "0" Then
        '            Button1.Disabled = True
        '        End If
        '    End If
        'Else
        '    strBuilder = objeAAMS.SecurityCheck(31)
        'End If

        '************************************************************************
        '************************************************************************
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Group Case']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Group Case']").Attributes("Value").Value)
            End If
            If Not strBuilder Is Nothing Then
                If strBuilder(0) = "0" Then
                    btnGroupCase.Disabled = True
                End If
            End If
        Else
            strBuilder = objeAAMS.SecurityCheck(31)
        End If

        '************************************************************************
        '************************************************************************
        'objSecurityXml.LoadXml(Session("Security"))
        'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
        '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Count <> 0 Then
        '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Attributes("Value").Value)
        '    End If
        '    If Not strBuilder Is Nothing Then
        '        If strBuilder(0) = "0" Then
        'btnOrderFeasability.Enabled = False
        '        End If
        '    End If
        'Else
        '    strBuilder = objeAAMS.SecurityCheck(31)
        'End If

        '************************************************************************

        '************************************************************************
        'objSecurityXml.LoadXml(Session("Security"))
        'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
        '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Count <> 0 Then
        '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Attributes("Value").Value)
        '    End If
        '    If Not strBuilder Is Nothing Then
        '        If strBuilder(0) = "0" Then
        'btnConnectivityHistory.Disabled = True
        '        End If
        '    End If
        'Else
        '    strBuilder = objeAAMS.SecurityCheck(31)
        'End If

        '************************************************************************
        '************************************************************************
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DailyBookings']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DailyBookings']").Attributes("Value").Value)
            End If
            If Not strBuilder Is Nothing Then
                If strBuilder(0) = "0" Then
                    btnDailyBooking.Disabled = True
                End If
            End If
        Else
            strBuilder = objeAAMS.SecurityCheck(31)
        End If

        '************************************************************************


        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity Details']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity Details']").Attributes("Value").Value)
            End If
            If Not strBuilder Is Nothing Then
                If strBuilder(0) = "0" Then
                    btnAProductivity.Disabled = True
                End If
            End If
        Else
            strBuilder = objeAAMS.SecurityCheck(31)
        End If
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Attributes("Value").Value)
            End If
            If Not strBuilder Is Nothing Then
                If strBuilder(0) = "0" Then
                    btnAllCrsProductivity.Disabled = True
                End If
            End If
        Else
            strBuilder = objeAAMS.SecurityCheck(31)
        End If
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OrderFeasibilityReport']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OrderFeasibilityReport']").Attributes("Value").Value)
            End If
            If Not strBuilder Is Nothing Then
                If strBuilder(0) = "0" Then
                    btnOrderFeasability.Disabled = True
                End If
            End If
        Else
            strBuilder = objeAAMS.SecurityCheck(31)
        End If
        '***************************************************************************

    End Sub


    Private Sub GetOtherDetails()
        '***************************************************************************
        If Session("Security") Is Nothing Then
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))

            Dim LimAoff, LimReg, LimOwnOff As String
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            LimAoff = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        Else
                            LimAoff = ""

                        End If
                    Else
                        LimAoff = ""
                    End If
                Else
                    LimAoff = ""
                End If

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            LimReg = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            LimReg = ""
                        End If
                    Else
                        LimReg = ""
                    End If
                Else
                    LimReg = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        LimOwnOff = "1"
                    Else
                        LimOwnOff = "0"
                    End If
                    'objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                Else
                    LimOwnOff = "0"
                End If
            End If
          
            'Code by Mukund for Daily Booking Link
            If hdLcode.Value.Trim.Length > 0 Then
                hdEnLcode.Value = objED.Encrypt(hdLcode.Value.Trim)

            End If
            If txtAoffice.Text.Trim.Length > 0 Then
                hdEnAoffice.Value = objED.Encrypt(txtAoffice.Text.Trim)
            End If


            'Encryption Code Segment
        End If

    End Sub

End Class


'@@@@
'Previous Code for Security

' #######################################
' ########## This code is used for enable/disable 
' ########## the button according to rights
'Dim objSecurityXml As New XmlDocument
'            objSecurityXml.LoadXml(Session("Security"))

''@ #################   Security For Agency Details
'            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
'                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Count <> 0 Then
'                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Attributes("Value").Value)
'                    If strBuilder(1) = "0" Then
'                        btnNew.Enabled = False
'                    End If
'                End If
'            Else
'                strBuilder = objeAAMS.SecurityCheck(31)
'            End If
''@ #################  End of  Security For Agency Details

''@ #################   Security For Modify Agency Contact Details

'            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
'                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Agency Contact Info']").Count <> 0 Then
'                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Agency Contact Info']").Attributes("Value").Value)
'                    If (Session("Action") IsNot Nothing) Then
'                        If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
'                            If strBuilder(2) = "0" Then
'                                txtEmail.Enabled = False
'                                txtPhone.Enabled = False
'                                txtFax.Enabled = False
'                                txtIataId.Enabled = False
'                                txtWevSite.Enabled = False
'                            End If
'                        Else
'                            If strBuilder(1) = "0" Then
'                                txtEmail.Enabled = False
'                                txtPhone.Enabled = False
'                                txtFax.Enabled = False
'                                txtIataId.Enabled = False
'                                txtWevSite.Enabled = False
'                            End If
'                        End If
'                    Else
'                        If strBuilder(1) = "0" Then
'                            txtEmail.Enabled = False
'                            txtPhone.Enabled = False
'                            txtFax.Enabled = False
'                            txtIataId.Enabled = False
'                            txtWevSite.Enabled = False
'                        End If
'                    End If
'                End If
'            Else
'                strBuilder = objeAAMS.SecurityCheck(31)
'            End If
''@ #################  End of   Security For Modify Agency Contact Details


''@ #################   Security For Modify Agency Amadeus Specific Details
'            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
'                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Amadeus Specific']").Count <> 0 Then
'                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Amadeus Specific']").Attributes("Value").Value)
'                    If (Session("Action") IsNot Nothing) Then
'                        If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
'                            If strBuilder(2) = "0" Then
'                                txtAoffice.Enabled = False
'                                drpType.Enabled = False
'                                drpPriority.Enabled = False
'                                txtDateOnline.Enabled = False
'                                txtDateOffline.Enabled = False
'                                rdCCRoster.Enabled = False
'                                drpFileNumber.Enabled = False
'                                txtReason.Enabled = False
'                                img1A.Attributes.Add("onClick", "<script>Javascript:'#';</script>")
'                            ElseIf strBuilder(2) = "1" Then
''@ #################   Security For Modify Agency 1A Responsibility
'                                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify 1AResponsibiltiy']").Count <> 0 Then
'                                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify 1AResponsibiltiy']").Attributes("Value").Value)
'                                    If strBuilder(2) = "0" Then
'                                        img1A.Attributes.Add("onClick", "<script>Javascript:'#';</script>")
'                                    End If
'                                End If
''@ #################  End  Security For Modify Agency 1A Responsibility
'                            End If
'                        Else
'                            If strBuilder(1) = "0" Then
'                                txtAoffice.Enabled = False
'                                drpType.Enabled = False
'                                drpPriority.Enabled = False
'                                txtDateOnline.Enabled = False
'                                txtDateOffline.Enabled = False
'                                rdCCRoster.Enabled = False
'                                drpFileNumber.Enabled = False
'                                txtReason.Enabled = False
'                                img1A.Attributes.Add("onClick", "<script>Javascript:'#';</script>")
'                            ElseIf strBuilder(1) = "1" Then
''@ #################   Security For Modify Agency 1A Responsibility
'                                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify 1AResponsibiltiy']").Count <> 0 Then
'                                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify 1AResponsibiltiy']").Attributes("Value").Value)
'                                    If strBuilder(1) = "0" Then
'                                        img1A.Attributes.Add("onClick", "<script>Javascript:'#';</script>")
'                                    End If
'                                End If
''@ #################  End  Security For Modify Agency 1A Responsibility
'                            End If

'                        End If
'                    Else
'                        If strBuilder(1) = "0" Then
'                            txtAoffice.Enabled = False
'                            drpType.Enabled = False
'                            drpPriority.Enabled = False
'                            txtDateOnline.Enabled = False
'                            txtDateOffline.Enabled = False
'                            rdCCRoster.Enabled = False
'                            drpFileNumber.Enabled = False
'                            txtReason.Enabled = False
'                            img1A.Attributes.Add("onClick", "<script>Javascript:'#';</script>")
'                        ElseIf strBuilder(1) = "1" Then
''@ #################   Security For Modify Agency 1A Responsibility
'                            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify 1AResponsibiltiy']").Count <> 0 Then
'                                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify 1AResponsibiltiy']").Attributes("Value").Value)
'                                If strBuilder(1) = "0" Then
'                                    img1A.Attributes.Add("onClick", "<script>Javascript:'#';</script>")
'                                End If
'                            End If
''@ #################  End  Security For Modify Agency 1A Responsibility
'                        End If
'                    End If
'                End If
'            Else
'                strBuilder = objeAAMS.SecurityCheck(31)
'            End If

''@ #################  End of  Security For Modify Agency Amadeus Specific Details

''@ #################   Security For Modify Agency  Connectivity


'            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
'                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Connectivity Info']").Count <> 0 Then
'                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Connectivity Info']").Attributes("Value").Value)
'                    If (Session("Action") IsNot Nothing) Then
'                        If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
'                            If strBuilder(2) = "0" Then
'                                drpPrimaryOnlineStatus.Enabled = False
'                                drpBackupOnlineStatus.Enabled = False
'                                txtPrimaryOrderNumber.Enabled = False
'                                txtBackupOrderNumber.Enabled = False
'                                txtPrimaryDate.Enabled = False
'                                txtBackupDate.Enabled = False
'                                imgPrimaryDate.Visible = False


'                            End If
'                        Else
'                            If strBuilder(1) = "0" Then
'                                drpPrimaryOnlineStatus.Enabled = False
'                                drpBackupOnlineStatus.Enabled = False
'                                txtPrimaryOrderNumber.Enabled = False
'                                txtBackupOrderNumber.Enabled = False
'                                txtPrimaryDate.Enabled = False
'                                txtBackupDate.Enabled = False
'                                imgPrimaryDate.Visible = False

'                            End If
'                        End If
'                    Else
'                        If strBuilder(1) = "0" Then
'                            drpPrimaryOnlineStatus.Enabled = False
'                            drpBackupOnlineStatus.Enabled = False
'                            txtPrimaryOrderNumber.Enabled = False
'                            txtBackupOrderNumber.Enabled = False
'                            txtPrimaryDate.Enabled = False
'                            txtBackupDate.Enabled = False
'                            imgPrimaryDate.Visible = False
'                        End If
'                    End If


'                End If
'            End If
''@ #################   end of Security For Modify Agency Connectivity

''@ #################   Security For Modify Agency View Misc Documents
'            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
'                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='View Misc Documents']").Count <> 0 Then
'                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='View Misc Documents']").Attributes("Value").Value)
'                    If (Session("Action") IsNot Nothing) Then
'                        If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
'                            If strBuilder(2) = "0" Then
'                                btnMiscViewDoc.Attributes.Add("onClick", "<script>Javascript:'#';</script>")
'                            End If
'                        Else
'                            If strBuilder(1) = "0" Then
'                                btnMiscViewDoc.Attributes.Add("onClick", "<script>Javascript:'#';</script>")
'                            End If
'                        End If
'                    Else
'                        If strBuilder(1) = "0" Then
'                            btnMiscViewDoc.Attributes.Add("onClick", "<script>Javascript:'#';</script>")
'                        End If
'                    End If
'                End If
'            End If
''@ #################  End of Security For Modify View Misc Documents



'' #######################################
'' ########## End of code used for enable/disable 
'' ########## the button according to rights

''@@@2