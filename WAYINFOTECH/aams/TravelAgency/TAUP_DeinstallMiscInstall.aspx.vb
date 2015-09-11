Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml

Partial Class TravelAgency_TAUP_DeinstallMiscInstall
    Inherits System.Web.UI.Page
    Implements System.Web.UI.ICallbackEventHandler
    Dim objEaams As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strResult As String


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
            btnClose.Attributes.Add("onclick", "return closeWindow();")
            ' txtChallanNo.Attributes.Add("onfocusout", "return validateChallanNo();")
            txtChallanNo.Focus()
            Dim m As ClientScriptManager = Me.ClientScript
            strResult = m.GetCallbackEventReference(Me, "args", "ReceiveServerData", "'this is context from server'")
            Dim strCallback As String = "function CallServer(args,context){" + strResult + ";}"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objEaams.CheckSession())
                Exit Sub
            End If

            If Not Page.IsPostBack Then
                txtDtInstall.Text = Format(Now, "dd/MM/yyyy")
            End If

            'Security Check for Override Challan Number
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OVERRIDE_CHALLAN_NO']").Count <> 0 Then
                    strBuilder = objEaams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OVERRIDE_CHALLAN_NO']").Attributes("Value").Value)
                    If strBuilder(0) = "1" Then
                        hdOverRide.Value = "1"
                    Else
                        hdOverRide.Value = "0"
                    End If


                    strBuilder = objEaams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='USE_BACKDATED_CHALLAN']").Attributes("Value").Value)
                    If strBuilder(0) = "1" Then
                        hdOverRideBackDate.Value = "1"
                    Else
                        hdOverRideBackDate.Value = "0"
                    End If

                    strBuilder = objEaams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OVERRIDE_CHALLAN_SERIAL_NO']").Attributes("Value").Value)
                    If strBuilder(0) = "1" Then
                        hdOverRideSerialNo.Value = "1"
                    Else
                        hdOverRideSerialNo.Value = "0"
                    End If
                End If
            Else
                hdOverRide.Value = "1"
                hdOverRideBackDate.Value = "1"
                hdOverRideSerialNo.Value = "1"
                strBuilder = objEaams.SecurityCheck(31)
            End If

            'End of Sec. Check for Override Challan Number


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
        Return strResult
    End Function

    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
        Try
            Dim id As String
            id = eventArgument
            Dim objbzChallan As New AAMS.bizInventory.bzChallan
            Dim objOutputXml As New XmlDocument
            Dim objInputXml As New XmlDocument

            If id.Split("|").GetValue(1) = "1" Then

                '<INV_SEARCH_CHALLAN_OUTPUT><CHALLAN ChallanID='' ChallanNumber ='' CreationDate='' ChallanDate='' ChallanCategory='' ChallanType='' SupplierName='' AgencyName='' OfficeID='' GodownName='' RGodownName='' LCODE='' /><Errors Status=''><Error Code='' Description='' /></Errors></INV_SEARCH_CHALLAN_OUTPUT>

                'objInputXml.LoadXml("<INV_GETCHALLANSERIAL_INPUT><SerialNo></SerialNo><Type></Type></INV_GETCHALLANSERIAL_INPUT>")
                objInputXml.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo></INV_SEARCH_CHALLAN_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("ChallanNumber").InnerText = id.Split("|").GetValue(0)
                objInputXml.DocumentElement.SelectSingleNode("ChallanCategory").InnerText = "1"
                objOutputXml = objbzChallan.Search(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    Dim strAgencyName As String
                    strAgencyName = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("AgencyName").Value
                    Dim strOfficeID As String
                    strOfficeID = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("OfficeID").Value
                    If (Session("Action") IsNot Nothing) Then
                        If (Session("Action").ToString().Split("|").Length >= 2) Then
                            If objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("LCODE").Value <> Session("Action").ToString().Split("|").GetValue(1) Then
                                strResult = "1|" & id.Split("|").GetValue(0) & "|" & strAgencyName & "|" & strOfficeID
                            End If
                        Else
                            strResult = "-1|Session Expire||"
                        End If
                    End If
                Else
                    If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value = "No Record Found!") Then
                        strResult = "0"
                    Else
                        strResult = "-1|" & objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            strResult = "-1|" & ex.Message
        End Try

    End Sub




    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If txtChallanNo.Text = "" Or txtChallanNo.Text.Trim.Length <= 0 Then

                If hdFlagMiscInstall.Value <> "1" Then
                    pnlErroMsg.Visible = True
                    pnlDeInstall.Visible = True
                    lblValidationMsg.Text = "Challan No. is blank. Want to continue ?"
                    disableControls()
                    Exit Sub
                Else
                    'If hdOverRide.Value = "0" Then
                    '    pnlErroMsg.Visible = True
                    '    pnlDeInstall.Visible = True
                    '    lblValidationMsg.Text = "You don't have enough rights to DeInstall H/W for this agency"
                    '    Exit Sub
                    'End If

                    'If hdOverRideBackDate.Value = "0" Then
                    '    pnlErroMsg.Visible = True
                    '    pnlDeInstall.Visible = True
                    '    lblValidationMsg.Text = "You don't have enough rights to DeInstall BackDate H/W for this agency"
                    '    Exit Sub
                    'End If
                    SaveDesinstall()
                End If
            Else
                If hdYesNo.Value <> "0" Or hdYesNo.Value = "" Then
                    ValidateChallan()
                End If

                If hdFlagMiscInstall.Value = "1" Then
                    If hdValidChallan.Value <> "1" Then
                        If hdOverRide.Value = "0" Then
                            pnlErroMsg.Visible = False
                            pnlDeInstall.Visible = True
                            lblError.Text = "You don't have enough rights to DeInstall H/W"
                            disableControls()
                            Exit Sub
                        Else
                            SaveDesinstall()
                        End If
                    Else
                        SaveDesinstall()
                    End If
                    
                End If


                'If hdOverRideBackDate.Value = "0" Then
                '    pnlErroMsg.Visible = True
                '    pnlDeInstall.Visible = True
                '    lblValidationMsg.Text = "You don't have enough rights to DeInstall BackDate H/W for this agency"
                '    Exit Sub
                'End If


                End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYes.Click


        If hdYesNo.Value = "1" Then
            hdYesNo.Value = "0"
        End If

        If hdOverRide.Value = "0" Or hdOverRide.Value = "" Then
            If txtChallanNo.Text.Trim() = "" Or txtChallanNo.Text.Trim().Length <= 0 Then
                lblError.Text = "You don't have enough rights to DeInstall H/W without a ChallanNo. "
            Else
                lblError.Text = "You don't have enough rights to DeInstall H/W with  InValid ChallanNo. "
            End If
            pnlErroMsg.Visible = False
            ' pnlDeInstall.Visible = False
            hdFlagMiscInstall.Value = "0"
            disableControls()
        Else
            hdFlagMiscInstall.Value = "1"
            btnSave_Click(sender, e)
        End If

    End Sub

    Protected Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click
        pnlErroMsg.Visible = False
        pnlDeInstall.Visible = True
        hdFlagMiscInstall.Value = "0"
        EnableControls()
    End Sub

    Private Sub SaveDesinstall()
        Dim objInputXml_View, objOutputXml_View As New XmlDocument
        Dim objtaMiscRecord As New AAMS.bizTravelAgency.bzMiscInstallation
        objInputXml_View.LoadXml("<TA_VIEWMISCINSTALLATION_INPUT><ROWID /></TA_VIEWMISCINSTALLATION_INPUT>")

        ' If hdFlagMiscInstall.Value = "1" Then
        If Request.QueryString("RowId") IsNot Nothing Then
            objInputXml_View.DocumentElement.SelectSingleNode("ROWID").InnerXml = Request.QueryString("RowId").ToString()
        Else
            lblError.Text = "Invalid Action"
            Exit Sub
        End If
        objOutputXml_View = objtaMiscRecord.View(objInputXml_View)
        'End of View





        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objtaMiscInst As New AAMS.bizTravelAgency.bzMiscInstallation

        objInputXml.LoadXml("<TA_UPDATEMISCINSTALLATION_INPUT><MISCINSTALLATION ACTION='' USE_BACKDATED_CHALLAN='' ROWID='' LCODE='' DATE='' DATEDE='' EQUIPMENTTYPE='' EQUIPMENTNUMBER='' QTY='' ORDERNUMBER='' ORDERNUMBERDE='' CHALLANNO='' LOGGEDBY='' /></TA_UPDATEMISCINSTALLATION_INPUT>")

        With objInputXml.DocumentElement.SelectSingleNode("MISCINSTALLATION")
            .Attributes("ACTION").Value() = "X"
            .Attributes("ROWID").Value() = Request.QueryString("RowId").ToString()
            If (Session("Action") IsNot Nothing) Then
                If (Session("Action").ToString().Split("|").Length >= 2) Then
                    .Attributes("LCODE").Value() = Session("Action").ToString().Split("|").GetValue(1)
                Else
                    lblError.Text = "Lcode is not exist."
                    Exit Sub
                End If
            Else
                lblError.Text = "Lcode is not exist."
                Exit Sub
            End If

            .Attributes("DATE").Value() = objOutputXml_View.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("DATE").Value.Trim()
            .Attributes("DATEDE").Value() = objEaams.ConvertTextDate(txtDtInstall.Text.Trim())
            .Attributes("EQUIPMENTTYPE").Value() = objOutputXml_View.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("EQUIPMENTTYPE").Value.Trim()
            .Attributes("EQUIPMENTNUMBER").Value() = objOutputXml_View.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("EQUIPMENTNUMBER").Value.Trim()
            .Attributes("QTY").Value() = objOutputXml_View.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("QTY").Value.Trim()
            .Attributes("ORDERNUMBER").Value() = objOutputXml_View.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("ORDERNUMBER").Value.Trim()
            .Attributes("ORDERNUMBERDE").Value() = ""
            .Attributes("CHALLANNO").Value() = txtChallanNo.Text.Trim()
            .Attributes("USE_BACKDATED_CHALLAN").Value = hdOverRideBackDate.Value

            Dim objSecXml As New XmlDocument
            objSecXml.LoadXml(Session("Security"))
            ' txtCreatedBy.Text = objSecXml.DocumentElement.SelectSingleNode("Employee_Name").InnerText.Trim()
            .Attributes("LOGGEDBY").Value() = objSecXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
        End With

        objOutputXml = objtaMiscInst.Update(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            lblError.Text = objeAAMSMessage.messInsert
            hdMsg.Value = "1"
            ClientScript.RegisterStartupScript(Me.GetType(), "keys", "<script type='text/javascript' language='javascript'>window.opener.document.getElementById('hdDeinstalled').value='1';   window.opener.document.forms['form1'].submit();window.close()</script> ")

        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            pnlErroMsg.Visible = False
            hdMsg.Value = "0"
        End If
        ' End If


    End Sub

    Private Sub ValidateChallan()

        hdFlagMiscInstall.Value = "0"
        hdYesNo.Value = "0"
        hdValidChallan.Value = "0"
        Dim objbzChallan As New AAMS.bizInventory.bzChallan
        Dim objOutputXml As New XmlDocument
        Dim objInputXml As New XmlDocument

        Dim strChallanNo As String = txtChallanNo.Text.Trim

        objInputXml.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCH_CHALLAN_INPUT>")
        'objInputXml.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo></INV_SEARCH_CHALLAN_INPUT>")

        objInputXml.DocumentElement.SelectSingleNode("ChallanNumber").InnerText = txtChallanNo.Text.Trim
        objInputXml.DocumentElement.SelectSingleNode("ChallanCategory").InnerText = "1"
        objOutputXml = objbzChallan.Search(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            hdFlagMiscInstall.Value = "1"
            hdValidChallan.Value = "1"
        ElseIf objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value = "No Record Found!" Then
            pnlDeInstall.Visible = True
            pnlErroMsg.Visible = True
            lblValidationMsg.Text = "Given challan number does not exist .Do you want to Continue?"
            hdYesNo.Value = "1"
            disableControls()
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If

    End Sub
    Private Sub disableControls()
        btnSave.Enabled = False
        txtChallanNo.ReadOnly = True
        txtChallanNo.CssClass = "textboxgrey"
        imgDateInstall.Disabled = True
    End Sub
    Private Sub EnableControls()
        btnSave.Enabled = True
        txtChallanNo.ReadOnly = False
        txtChallanNo.CssClass = "textbox"
        imgDateInstall.Disabled = False
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.OriginalString)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
