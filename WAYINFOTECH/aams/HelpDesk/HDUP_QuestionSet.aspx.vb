Imports System.Data
Imports System.Xml
Partial Class HelpDesk_HDUP_QuestionSet
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Public Shared strId As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim i, j As Integer
            Session("PageName") = Request.Url.ToString()
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If
            '  btnSave.Attributes.Add("onclick", "return chkSave();")
            btnSave.Attributes.Add("onclick", "return validateGrid()")
            If Not Page.IsPostBack Then

                For j = 1990 To DateTime.Now.Year
                    drpYear.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                    i += 1
                    drpYear.SelectedValue = DateTime.Now.Year
                Next

                If Request.QueryString("Action").ToString() = "U" Or Request.QueryString("Action").ToString() = "US" Then
                    QuestionView()
                End If
                '***************************************************************************************
                If Request.QueryString("Action").ToString() = "I" Then
                    lblError.Text = objeAAMSMessage.messInsert
                    QuestionSet()
                End If
                If Request.QueryString("Action").ToString() = "US" Then
                    lblError.Text = objeAAMSMessage.messInsert
                    'QuestionView()
                End If

            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Feedback Questionset']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Feedback Questionset']").Attributes("Value").Value)

                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnSave.Enabled = False
                    End If
                    If strBuilder(1) = "0" Then
                        btnSave.Enabled = False
                    End If
                End If
            Else
                objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub QuestionSet()
        strId = Nothing
        Dim tempXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        tempXml.LoadXml("<HD_VIEWFEEDBACKQUESTION_INPUT><SET ID='' LOGGEDBY='' MONTH='' YEAR=''><QUESTIONS QUESTION_NO='1' QUESTION_TITLE='' /><QUESTIONS QUESTION_NO='2' QUESTION_TITLE='' /><QUESTIONS QUESTION_NO='3' QUESTION_TITLE='' /><QUESTIONS QUESTION_NO='4' QUESTION_TITLE='' /><QUESTIONS QUESTION_NO='5' QUESTION_TITLE='' /><QUESTIONS QUESTION_NO='6' QUESTION_TITLE='' /><QUESTIONS QUESTION_NO='7' QUESTION_TITLE='' /><QUESTIONS QUESTION_NO='8' QUESTION_TITLE='' /><QUESTIONS QUESTION_NO='9' QUESTION_TITLE='' /><QUESTIONS QUESTION_NO='10' QUESTION_TITLE='' /></SET><Errors Status=''> <Error Code='' Description='' /></Errors></HD_VIEWFEEDBACKQUESTION_INPUT>")
        objXmlReader = New XmlNodeReader(tempXml)
        ds.ReadXml(objXmlReader)
        lblError.Text = ""
        gvQuestions.DataSource = ds.Tables("QUESTIONS")
        gvQuestions.DataBind()
    End Sub
    Private Sub QuestionView()
        Try
            Dim objViewQ As New AAMS.bizHelpDesk.bzFeedbackSet
            Dim ds As New DataSet
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            '            Dim objNodeList As XmlNodeList
            '            Dim objNodeQ As XmlNode


            objInputXml.LoadXml("<HD_VIEWFEEDBACKQUESTION_INPUT><SETID></SETID></HD_VIEWFEEDBACKQUESTION_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("SETID").InnerText = objED.Decrypt(Request.QueryString("SetID").ToString())
            objOutputXml = objViewQ.View(objInputXml)
            strId = objOutputXml.DocumentElement.SelectSingleNode("SET").Attributes("ID").Value
            drpMonth.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("SET").Attributes("MONTH").Value
            drpYear.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("SET").Attributes("YEAR").Value
            'Dim hdId As HiddenField
            'hdId = CType(Page.FindControl("hdId"), HiddenField)
            'hdId.Value = strId
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvQuestions.DataSource = ds.Tables("QUESTIONS")
                gvQuestions.DataBind()
            Else
                gvQuestions.DataSource = Nothing
                gvQuestions.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        'Response.Redirect("HDUP_QuestionSet.aspx?Action=I")
        Try
            If Request.QueryString("Action").ToString() = "U" Or Request.QueryString("Action").ToString() = "US" Then
                QuestionView()
            Else
                drpMonth.SelectedIndex = 0
                drpYear.SelectedValue = DateTime.Now.Year
                QuestionSet()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try


            '            Dim i As Integer
            If drpMonth.SelectedValue = 0 Then
                lblError.Text = "Please select a valid Month."
                Exit Sub
            End If
            If gvQuestions.Rows.Count = 0 Then
                lblError.Text = "Please Search valid Month."
                Exit Sub
            End If
           
            Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
            Dim objQuesSave As New AAMS.bizHelpDesk.bzFeedbackSet
            Dim ds As New DataSet
            '            Dim Rowno As Integer
            Dim objParentNode As XmlNode
            Dim objChildNodeClone As XmlNode
            Dim objEmpXml As New XmlDocument
            Dim UserId As String
            objEmpXml.LoadXml(Session("Security"))
            UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
            objInputXml.LoadXml("<HD_UPDATEFEEDBACKQUESTION_INPUT><SET ID='' LOGGEDBY='' MONTH='' YEAR=''><QUESTIONS QUESTION_NO='' QUESTION_TITTLE='' /></SET></HD_UPDATEFEEDBACKQUESTION_INPUT>")
            'If strId <> Nothing Then
            '    objInputXml.DocumentElement.SelectSingleNode("SET").Attributes("ID").Value() = strId
            'Else
            '    objInputXml.DocumentElement.SelectSingleNode("SET").Attributes("ID").Value() = String.Empty
            'End If


            If Request.QueryString("ACTION").ToString().Trim().ToUpper = "U" Or Request.QueryString("ACTION").ToString().Trim().ToUpper = "US" Then
                objInputXml.DocumentElement.SelectSingleNode("SET").Attributes("ID").Value() = objED.Decrypt(Request.QueryString("SetID"))
            Else
                objInputXml.DocumentElement.SelectSingleNode("SET").Attributes("ID").Value() = String.Empty
            End If


            objInputXml.DocumentElement.SelectSingleNode("SET").Attributes("LOGGEDBY").Value() = UserId
            objInputXml.DocumentElement.SelectSingleNode("SET").Attributes("MONTH").Value() = drpMonth.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("SET").Attributes("YEAR").Value = drpYear.SelectedValue
            objParentNode = objInputXml.DocumentElement.SelectSingleNode("SET/QUESTIONS")
            objChildNodeClone = objParentNode.CloneNode(True)
            'For Rowno = 0 To gvQuestions.Rows.Count - 1
            '    objChildNodeClone.Attributes("QUESTION_NO").Value() = CType(gvQuestions.Rows(Rowno).FindControl("txtQuestionNo"), Label).Text
            '    objChildNodeClone.Attributes("QUESTION_TITTLE").Value() = CType(gvQuestions.Rows(Rowno).FindControl("txtQuestion"), TextBox).Text
            '    objInputXml.DocumentElement.SelectSingleNode("SET").AppendChild(objChildNodeClone)
            '    objChildNodeClone = objParentNode.CloneNode(True)
            'Next
            For Each grdRow As GridViewRow In gvQuestions.Rows
                objChildNodeClone.Attributes("QUESTION_NO").Value() = CType(grdRow.FindControl("txtQuestionNo"), Label).Text
                Dim id As String
                id = CType(grdRow.FindControl("txtQuestion"), TextBox).UniqueID
                Dim strValue As String
                strValue = Request.Form(id)
                CType(grdRow.FindControl("txtQuestion"), TextBox).Text = strValue
                objChildNodeClone.Attributes("QUESTION_TITTLE").Value() = strValue
                objInputXml.DocumentElement.SelectSingleNode("SET").AppendChild(objChildNodeClone)
                objChildNodeClone = objParentNode.CloneNode(True)
            Next
            objInputXml.DocumentElement.SelectSingleNode("SET").RemoveChild(objInputXml.DocumentElement.SelectSingleNode("SET/QUESTIONS[@QUESTION_NO='']"))
            'objInputXml.DocumentElement.RemoveChild(objInputXml.DocumentElement.SelectSingleNode("SET/QUESTIONS[@QUESTION_NO='']"))
            objOutputXml = objQuesSave.Update(objInputXml)
            Dim CheckBoxOb As String = objOutputXml.DocumentElement.SelectSingleNode("SET").Attributes("ID").Value().Trim()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                'lblError.Text = objeAAMSMessage.messInsert ' "Added Successfully."
                If (Request.QueryString("Action").ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().ToUpper() = "US") Then
                    lblError.Text = objeAAMSMessage.messUpdate
                Else
                    lblError.Text = objeAAMSMessage.messInsert
                    Response.Redirect("HDUP_QuestionSet.aspx?Action=US&SetID=" + objED.Encrypt(CheckBoxOb))

                End If
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvQuestions_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvQuestions.RowDataBound
        Dim objSecurityXml As New XmlDocument
        Dim linkEdit As New LinkButton
        Dim str As String = ""
        '        Dim yr As String
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            ' Dim lnkHistory As LinkButton
            linkEdit = CType(e.Row.FindControl("lnkEdit"), LinkButton)

            Dim txt As New TextBox

            txt = CType(e.Row.FindControl("txtQuestion"), TextBox)



            Dim strBuilder As New StringBuilder

            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Feedback Questionset']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Feedback Questionset']").Attributes("Value").Value)
                    If strBuilder(2) = "0" Then
                        linkEdit.Enabled = False

                    Else

                        linkEdit.Attributes.Add("onclick", "return ChangeControlStatus('" + txt.ClientID + "')")
                    End If
                Else
                    linkEdit.Enabled = False
                End If
            Else
                linkEdit.Enabled = True
                linkEdit.Attributes.Add("onclick", "return ChangeControlStatus('" + txt.ClientID + "')")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Response.Redirect("HDUP_QuestionSet.aspx?Action=I")
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
