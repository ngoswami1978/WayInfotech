Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml

Partial Class BirdresHelpDesk_HDUP_Feedback
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl

            'btnNew.Attributes.Add("onclick", "return InsertFeedback();")
            btnSave.Attributes.Add("onclick", "return ValidatFeedback();")
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If
            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If
            If Not Page.IsPostBack Then
                bindQuestins_Status()
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Then
                    ViewFeedbackDetails()
                    imgAgency.Visible = False
                    imgLogged.Visible = False
                    DisableQuestionStatus()
                End If
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                    ViewFeedbackDetails()
                    imgAgency.Visible = False
                    'txtEmpName.CssClass = "textboxgrey"
                    imgLogged.Visible = False
                    lblError.Text = objeAAMSMessage.messInsert
                    DisableQuestionStatus()

                End If

                If Not ((Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U") Or (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US")) Then
                    Dim xDoc As New XmlDocument
                    If Session("Security") IsNot Nothing Then
                        xDoc.LoadXml(Session("Security"))
                        hdEmpID.Value = xDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                        hdLogedByName.Value = xDoc.DocumentElement.SelectSingleNode("Employee_Name").InnerText.Trim()
                        txtLogedByName.Text = hdLogedByName.Value
                    End If
                End If
            End If



            '*********************Security Segment**************************************************
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Feedback']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Feedback']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                    btnNew.Enabled = False
                End If
                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                End If
                If strBuilder(2) = "0" Then
                    btnSave.Enabled = False
                End If

            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            '    '*********************End of Security Segment*****************************************************
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub ViewFeedbackDetails()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument

            Dim objhdFeedbackView As New AAMS.bizBRHelpDesk.bzFeedback
            objInputXml.LoadXml("<HD_VIEWFEEDBACK_INPUT><FEEDBACK_ID /></HD_VIEWFEEDBACK_INPUT>")
            If Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString() IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("FEEDBACK_ID").InnerText = Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()
            End If
            objOutputXml = objhdFeedbackView.View(objInputXml)
            With objOutputXml.DocumentElement.SelectSingleNode("FEEDBACK")
                txtAgencyName.Text = .Attributes("AGENCYNAME").Value.Trim()
                txtAgencyAddress.Text = .Attributes("ADDRESS").Value.Trim()
                'txtAssignedDt.Text = .Attributes("DATETIME").Value.Trim()
                txtFeedBackNo.Text = .Attributes("FEEDBACK_ID").Value.Trim()
                txtCity.Text = .Attributes("CITY").Value.Trim()
                txtCountry.Text = .Attributes("COUNTRY").Value.Trim()
                txtFax.Text = .Attributes("FAX").Value.Trim()
                txtOfficeID.Text = .Attributes("OFFICEID").Value.Trim()
                txtPhone.Text = .Attributes("PHONE").Value.Trim()
                txtRemarks.Text = .Attributes("SEGGESTION").Value.Trim()

                hdLcode.Value = .Attributes("LCODE").Value.Trim()
                hdAddress.Value = .Attributes("ADDRESS").Value.Trim()
                txtFeedbkDt.Text = .Attributes("DATETIME").Value.Trim()

                If .Attributes("EmployeeID").Value.Trim().Length <> 0 Then
                    hdEmpID.Value = .Attributes("EmployeeID").Value.Trim()
                    txtLogedByName.Text = .Attributes("EmployeeName").Value.Trim()
                End If
            End With
            'Following code is written for setting the status of all Questions
            Dim xmNode As XmlNode
            Dim xNodeList As XmlNodeList
            xNodeList = objOutputXml.DocumentElement.SelectNodes("FEEDBACK/DETAILS")
            For Each xmNode In xNodeList
                AssignQuestionStatus(xmNode.Attributes("QUESTION_ID").Value.Trim(), xmNode.Attributes("STATUS_ID").Value.Trim())
            Next
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub bindQuestins_Status()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objFeedback As New AAMS.bizBRHelpDesk.bzFeedback
            Dim objReader As XmlNodeReader
            Dim dSet As New DataSet
            objOutputXml = objFeedback.ListFeedbackQuestions()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objReader = New XmlNodeReader(objOutputXml)
                dSet.ReadXml(objReader)
                grdvFeedback.DataSource = dSet.Tables("QUESTION")
                grdvFeedback.DataBind()
                lblError.Text = ""
            Else
                grdvFeedback.DataSource = Nothing
                grdvFeedback.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdvFeedback_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvFeedback.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim drpQuestStatus As New DropDownList
            drpQuestStatus = CType(e.Row.FindControl("drpQuestStatus"), DropDownList)
            objeAAMS.BindDropDown(drpQuestStatus, "BRHDQUESTIONSTATUS", True)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub AssignQuestionStatus(ByVal QID As String, ByVal StatusID As String)
        Try

            Dim rowCounter As Integer
            Dim drpStatus As New DropDownList
            Dim lblQuestNo As New Label
            For rowCounter = 0 To grdvFeedback.Rows.Count - 1
                drpStatus = CType(grdvFeedback.Rows(rowCounter).FindControl("drpQuestStatus"), DropDownList)
                lblQuestNo = CType(grdvFeedback.Rows(rowCounter).FindControl("lblQuestionID"), Label)
                If lblQuestNo.Text.Trim() = QID.Trim() Then
                    CType(grdvFeedback.Rows(rowCounter).FindControl("drpQuestStatus"), DropDownList).SelectedValue = StatusID
                End If
            Next
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try
            If (Not Request.QueryString("Action") = Nothing) Then

                Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                Dim objhdPtrSave As New AAMS.bizBRHelpDesk.bzFeedback
                Dim objChildNode, objChildNodeClone, objParentNode As XmlNode
                objInputXml.LoadXml("<HD_UPDATECALLSTATUS_INPUT><FEEDBACK FEEDBACK_ID='' LCODE='' EmployeeID='' SEGGESTION=''><DETAILS QUESTION_ID='' STATUS_ID=''/></FEEDBACK></HD_UPDATECALLSTATUS_INPUT>")

                If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
                    objInputXml.DocumentElement.SelectSingleNode("FEEDBACK").Attributes("FEEDBACK_ID").Value = Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()
                End If


                With objInputXml.DocumentElement.SelectSingleNode("FEEDBACK")
                    .Attributes("LCODE").Value = hdLcode.Value.Trim()
                    .Attributes("EmployeeID").Value = hdEmpID.Value.Trim()
                    .Attributes("SEGGESTION").Value = txtRemarks.Text
                End With

                objParentNode = objInputXml.DocumentElement.SelectSingleNode("FEEDBACK")
                objChildNode = objInputXml.DocumentElement.SelectSingleNode("FEEDBACK/DETAILS")
                objChildNodeClone = objChildNode.CloneNode(True)
                objParentNode.RemoveChild(objChildNode)

                Dim rowCounter As Integer = 0
                For rowCounter = 0 To grdvFeedback.Rows.Count - 1
                    '***************** Code for Questions Check *************************
                    If Not ((Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U") Or (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US")) Then
                        If CType(grdvFeedback.Rows(rowCounter).FindControl("drpQuestStatus"), DropDownList).SelectedIndex = 0 Then
                            lblError.Text = "All Question Status is Mandatory"
                            CType(grdvFeedback.Rows(rowCounter).FindControl("drpQuestStatus"), DropDownList).Focus()
                            Exit Sub
                        End If
                    End If
                    '***************** Code for Questions Check *************************
                    objChildNodeClone.Attributes("QUESTION_ID").Value = CType(grdvFeedback.Rows(rowCounter).FindControl("lblQuestionID"), Label).Text.Trim()
                    objChildNodeClone.Attributes("STATUS_ID").Value = CType(grdvFeedback.Rows(rowCounter).FindControl("drpQuestStatus"), DropDownList).SelectedValue.Trim()
                    objParentNode.AppendChild(objChildNodeClone)
                    objChildNodeClone = objChildNode.CloneNode(True)
                Next

                objOutputXml = objhdPtrSave.Update(objInputXml)

                txtAgencyAddress.Text = Request("txtAgencyAddress")

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If ((Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U") Or (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US")) Then
                        lblError.Text = objeAAMSMessage.messUpdate ' "Record updated successfully."
                        'Response.Redirect("HDUP_Feedback.aspx?Action=U|" & objOutputXml.DocumentElement.SelectSingleNode("FEEDBACK").Attributes("FEEDBACK_ID").Value, False)
                    Else
                        ' lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."
                        Response.Redirect("HDUP_Feedback.aspx?Action=US|" & objOutputXml.DocumentElement.SelectSingleNode("FEEDBACK").Attributes("FEEDBACK_ID").Value, False)

                    End If
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Response.Redirect("HDUP_Feedback.aspx?Action=I|")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U") Or (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
                Response.Redirect("HDUP_Feedback.aspx?Action=U|" + Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString())
            Else
                Response.Redirect("HDUP_Feedback.aspx?Action=I|")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub DisableQuestionStatus()
        Try
            Dim rowCounter As Integer = 0
            For rowCounter = 0 To grdvFeedback.Rows.Count - 1
                CType(grdvFeedback.Rows(rowCounter).FindControl("drpQuestStatus"), DropDownList).Enabled = False
            Next
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
End Class
