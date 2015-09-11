
Partial Class Incentive_INCUP_Pay_ApprovalLevel
    Inherits System.Web.UI.Page

#Region "Global Variable Declaration"
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            txtLevel1.Text = Request.Form("txtlevel1")
            txtLevel2.Text = Request.Form("txtlevel2")
            txtLevel3.Text = Request.Form("txtLevel3")
            txtLevel4.Text = Request.Form("txtLevel4")
            txtLevel5.Text = Request.Form("txtLevel5")
            txtLevel6.Text = Request.Form("txtLevel6")
            txtLevel7.Text = Request.Form("txtLevel7")
            txtLevel8.Text = Request.Form("txtLevel8")
            txtLevel9.Text = Request.Form("txtLevel9")

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If Not IsPostBack Then
                Dim strurl As String = Request.Url.ToString()
                Session("PageName") = strurl
                btnSave.Attributes.Add("onClick", "return ValidateForm();")
                objeAAMS.BindDropDown(ddlAOffice, "AOFFICE", True, 3)
                If Session("EmployeePageName") IsNot Nothing Then
                    hdEmployeePageName.Value = Session("EmployeePageName")
                End If
                ' Checking Query String for update .
                If Not Request.QueryString("Action") = Nothing And Not Request.QueryString("ID") = Nothing Then
                    hdID.Value = objED.Decrypt(Request.QueryString("ID").ToString().Trim())
                    ViewDetails()
                End If

            End If



            ' Checking security.
            CheckSecurity()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#Region "ViewDetails()"
    Private Sub ViewDetails()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objApprovalQue As New AAMS.bizIncetive.bzPaymentApprovalLavel
        Try
            '<UP_VIEW_INC_APPROVAL_OUTPUT><AOFFICE></AOFFICE><LEVEL></LEVEL ><DETAILS EMPLOYEEID='' EMPLOYEENAME=''/><Errors Status=''>
            '<Error Code='' Description='' /></Errors></UP_VIEW_INC_APPROVAL_OUTPUT>

            objInputXml.LoadXml("<UP_VIEW_INC_PAYMENT_APPROVAL_INPUT><AOFFICE/></UP_VIEW_INC_PAYMENT_APPROVAL_INPUT >")
            objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = hdID.Value
            objOutputXml = objApprovalQue.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ' txtContactType.Text = objOutputXml.DocumentElement.SelectSingleNode("CONTACT_TYPE").Attributes("CONTACT_TYPE_NAME").Value
                ddlAOffice.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText
                txtNoOfLevel.Text = objOutputXml.DocumentElement.SelectSingleNode("LEVEL").InnerText
                'For i = 1 To CInt(txtNoOfLevel.Text)

                'Next

                ' Setting Daywise Marks start
                Dim objNodeListMarks As XmlNodeList
                Dim objNodeMarks As XmlNode
                '  Dim strDay As String = ""
                Dim txt As TextBox
                Dim ChkFinalApproved As CheckBox

                Dim hdEmpid As HtmlInputHidden
                objNodeListMarks = objOutputXml.DocumentElement.SelectNodes("DETAILS")
                For Each objNodeMarks In objNodeListMarks
                    ' strDay = objNodeMarks.Attributes("DAY").Value.Trim()
                    ' For i As Integer = 1 To objNodeListMarks.Count
                    hdEmpid = CType(Page.FindControl("hdLevel" + objNodeMarks.Attributes("LEVEL").Value), HtmlInputHidden)
                    txt = CType(Page.FindControl("txtLevel" + objNodeMarks.Attributes("LEVEL").Value), TextBox)
                    If txt IsNot Nothing Then
                        txt.Text = objNodeMarks.Attributes("EMPLOYEENAME").Value
                        hdEmpid.Value = objNodeMarks.Attributes("EMPLOYEEID").Value
                    End If
                    'Next

                    ChkFinalApproved = CType(Page.FindControl("ChkFinApproved" + objNodeMarks.Attributes("LEVEL").Value), CheckBox)

                    If ChkFinalApproved IsNot Nothing Then
                        If objNodeMarks.Attributes("ALLOWEDFINAL_APPROVAL").Value = "1" Then
                            ChkFinalApproved.Checked = True
                        Else
                            ChkFinalApproved.Checked = False
                        End If

                    End If

                Next

                ' Set Daywise Marks end

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

#Region "CheckSecurity()::This method is used for security check."
    Private Sub CheckSecurity()
        Try
            ' This code is used for checking session handler according to user login.
          
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Payment Approval Level']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Payment Approval Level']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                End If

                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    btnSave.Enabled = False
                End If
                If strBuilder(2) = "0" And (hdID.Value <> "" Or Request.QueryString("ID") IsNot Nothing) Then
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

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim objInputXml As New XmlDocument
            Dim objOutputXml As New XmlDocument
            Dim objApprovalQue As New AAMS.bizIncetive.bzPaymentApprovalLavel
            ' Load Input Xml.
            '  objInputXml.LoadXml("<UP_UPDATE_INC_PAYMENT_APPROVAL_INPUT>   <AOFFICE></AOFFICE>     <LEVELCOUNT></LEVELCOUNT>      <DETAILS LEVEL='' EMPLOYEEID='' EMPLOYEENAME=''/></UP_UPDATE_INC_PAYMENT_APPROVAL_INPUT >")
            objInputXml.LoadXml("<UP_UPDATE_INC_PAYMENT_APPROVAL_INPUT>   <AOFFICE></AOFFICE>     <LEVELCOUNT></LEVELCOUNT> <LOGINEMPLOYEEID></LOGINEMPLOYEEID>     <DETAILS LEVEL='' EMPLOYEEID='' EMPLOYEENAME='' ALLOWEDFINAL_APPROVAL='' /></UP_UPDATE_INC_PAYMENT_APPROVAL_INPUT >")

            objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = ddlAOffice.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("LEVELCOUNT").InnerText = txtNoOfLevel.Text.Trim


            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LOGINEMPLOYEEID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If


            Dim objNodeMarks, objCloneNodeMarks As XmlNode
            objNodeMarks = objInputXml.DocumentElement.SelectSingleNode("DETAILS")
            objCloneNodeMarks = objNodeMarks.CloneNode(True)
            objInputXml.DocumentElement.RemoveChild(objNodeMarks)

            Dim ChkFinalApproved As CheckBox
            Dim intFinalApproved As Integer = 0

            Dim txt As TextBox
            Dim hdEmpid As HtmlInputHidden
            For i As Integer = 1 To CInt(txtNoOfLevel.Text.Trim)

                ChkFinalApproved = CType(Page.FindControl("ChkFinApproved" + i.ToString), CheckBox)

                txt = CType(Page.FindControl("txtLevel" + i.ToString), TextBox)
                hdEmpid = CType(Page.FindControl("hdLevel" + i.ToString), HtmlInputHidden)
                If txt IsNot Nothing Then
                    If txt.Text <> "" Then
                        objCloneNodeMarks.Attributes("EMPLOYEEID").Value = hdEmpid.Value
                        objCloneNodeMarks.Attributes("EMPLOYEENAME").Value = txt.Text
                        objCloneNodeMarks.Attributes("LEVEL").Value = i



                        If Not ChkFinalApproved Is Nothing Then
                            If ChkFinalApproved.Checked = True Then
                                objCloneNodeMarks.Attributes("ALLOWEDFINAL_APPROVAL").Value = "1"
                            Else
                                objCloneNodeMarks.Attributes("ALLOWEDFINAL_APPROVAL").Value = "0"
                            End If
                        End If

                        objInputXml.DocumentElement.AppendChild(objCloneNodeMarks)
                        objCloneNodeMarks = objNodeMarks.CloneNode(True)
                    Else
                        lblError.Text = "Level " + i.ToString() + " is mandatory."
                        Exit Sub
                    End If
                End If
                If ChkFinalApproved.Checked = True Then
                    intFinalApproved = intFinalApproved + 1
                End If

            Next


            If intFinalApproved <= 0 Then
                lblError.Text = "Finally Approved is mandatory."
                Exit Sub
            End If

            'If intFinalApproved > 1 Then
            'lblError.Text = "Finally Approved can't be selected more than one."
            'Exit Sub
            ' End If


            ' Calling update method for update.
            objOutputXml = objApprovalQue.Update(objInputXml)
            '   Checking error status. 
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                '  Response.Redirect("INCUP_ApprovalLevel.aspx?Action=U&ID=" + objED.Encrypt(ddlAOffice.SelectedValue))

                If (Request.QueryString("Action").ToString().ToUpper() = "U" Or hdID.Value <> "") Then
                    hdID.Value = ddlAOffice.SelectedValue 'objOutputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText.Trim()
                    lblError.Text = objeAAMSMessage.messUpdate ' Record updated successfully.
                Else
                    hdID.Value = ddlAOffice.SelectedValue 'objOutputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText.Trim()
                    lblError.Text = objeAAMSMessage.messInsert ' Record inserted successfully.

                End If
                ' CheckSecurity()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("INCUP_Pay_ApprovalLevel.aspx?Action=I")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If hdID.Value = "" Then
                Response.Redirect("INCUP_Pay_ApprovalLevel.aspx?Action=I")
            Else
                ' Calling View Method.
                ViewDetails()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
