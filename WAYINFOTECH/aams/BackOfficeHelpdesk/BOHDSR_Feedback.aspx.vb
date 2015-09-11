Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml

Partial Class BOHelpDesk_HDSR_Feedback
    Inherits System.Web.UI.Page
    Dim objEaams As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objeAAMSMessage As New eAAMSMessage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = Request.Url.ToString
            objEaams.ExpirePageCache()

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objEaams.CheckSession())
                Exit Sub
            End If
            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If

            btnSearch.Attributes.Add("onclick", "return ValidateDates();")
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Feedback']").Count <> 0 Then
                    strBuilder = objEaams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Feedback']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnSearch.Enabled = False
                    End If
                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                    End If
                End If
            Else
                strBuilder = objEaams.SecurityCheck(31)
            End If

            '*****************Delete Functionality
            'If Request.QueryString("Action") IsNot Nothing Then
            '    If Request.QueryString("Action").Split("|").GetValue(0).ToString.ToUpper = "D" Then
            '        DeleteFeedback(Request.QueryString("Action").Split("|").GetValue(1).ToString.Trim())
            '        SearchFeedback()
            '        lblError.Text = objeAAMSMessage.messDelete
            '    End If
            'End If
            '*****************End of Delete 


            If hdDeleteFlag.Value <> "" Then
                DeleteFeedback(hdDeleteFlag.Value.Trim())
                SearchFeedback()
            End If


            If Not Page.IsPostBack Then
                BindDropDowns()



            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub BindDropDowns()
        Try
            objEaams.BindDropDown(drp1Aoffice, "AOFFICE", True)
            'objEaams.BindDropDown(drpLoggedBy, "HDASSIGNEDTO", True)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub SearchFeedback()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objReader As XmlNodeReader
            Dim dSet As New DataSet
            objInputXml.LoadXml("<HD_SEARCHFEEDBACK_INPUT><AGENCYNAME/><DATEFROM/><DATETO/><LOGGEDBY/><AOFFICE/><FEEDBACK_ID/></HD_SEARCHFEEDBACK_INPUT>")
            With objInputXml.DocumentElement

                .SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text.Trim()

                If txtFeedBackFromDt.Text IsNot Nothing Then
                    .SelectSingleNode("DATEFROM").InnerText = objEaams.ConvertTextDate(Request("txtFeedBackFromDt"))
                End If

                If txtFeedbackDtTo.Text IsNot Nothing Then
                    .SelectSingleNode("DATETO").InnerText = objEaams.ConvertTextDate(Request("txtFeedbackDtTo"))
                End If

                If txtPendingWith.Text.Trim().Length <> 0 Then
                    .SelectSingleNode("LOGGEDBY").InnerText = txtPendingWith.Text.Trim()
                End If

                If drp1Aoffice.SelectedIndex <> 0 Then
                    .SelectSingleNode("AOFFICE").InnerText = drp1Aoffice.SelectedItem.Text
                End If

                .SelectSingleNode("FEEDBACK_ID").InnerText = txtFeedBackNo.Text.Trim()
            End With

            Dim objFeedback As New AAMS.bizBOHelpDesk.bzFeedback
            objOutputXml = objFeedback.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objReader = New XmlNodeReader(objOutputXml)
                dSet.ReadXml(objReader)
                grdvFeedback.DataSource = dSet.Tables("FEEDBACK")
                grdvFeedback.DataBind()
                lblError.Text = ""
            Else
                grdvFeedback.DataSource = Nothing
                grdvFeedback.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
            If txtFeedBackFromDt.Text IsNot Nothing Then
                txtFeedBackFromDt.Text = Request("txtFeedBackFromDt")
            End If

            If txtFeedbackDtTo.Text IsNot Nothing Then
                txtFeedbackDtTo.Text = Request("txtFeedbackDtTo")
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub DeleteFeedback(ByVal feedbackID As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Try
            Dim objFbDele As New AAMS.bizBOHelpDesk.bzFeedback

            objInputXml.LoadXml("<HD_DELETEFEEDBACK_INPUT><FEEDBACK_ID /></HD_DELETEFEEDBACK_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("FEEDBACK_ID").InnerText = feedbackID
            'Call a function
            objOutputXml = objFbDele.Delete(objInputXml)
            hdDeleteFlag.Value = ""
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messDelete
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvFeedback_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvFeedback.RowDataBound
        Try

            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim hdFeedbackID As New HiddenField
            Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            linkEdit = e.Row.FindControl("linkEdit")
            Dim linkDelete As LinkButton
            linkDelete = e.Row.FindControl("linkDelete")
            hdFeedbackID = e.Row.FindControl("hdFeedbackID")
            Dim objeaams As New eAAMS


            'For Select Section 
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Feedback']").Count <> 0 Then
                    strBuilder = objeaams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Feedback']").Attributes("Value").Value)
                End If
                If strBuilder(3) = "0" Then
                    linkDelete.Enabled = False ' = True
                Else
                    linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdFeedbackID.Value & "');")
                End If
                If strBuilder(2) = "0" Then
                    linkEdit.Disabled = True
                Else
                    linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdFeedbackID.Value & "');")

                End If
            Else
                linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdFeedbackID.Value & "' );")
                linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdFeedbackID.Value & "');")
            End If

            'linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdFeedbackID.Value & "' );")
            'linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdFeedbackID.Value & "');")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            SearchFeedback()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Response.Redirect("BOHDUP_Feedback.aspx?Action=I|")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            txtAgencyName.Text = ""
            txtFeedbackDtTo.Text = ""
            txtFeedBackFromDt.Text = ""
            txtFeedBackNo.Text = ""
            drp1Aoffice.SelectedValue = ""
            'drpLoggedBy.SelectedValue = ""
            txtPendingWith.Text = ""
            lblError.Text = ""
            grdvFeedback.DataSource = Nothing
            grdvFeedback.DataBind()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Protected Sub txtAgencyName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgencyName.TextChanged

    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
End Class
