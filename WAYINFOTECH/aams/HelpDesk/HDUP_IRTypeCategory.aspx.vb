'##########################################################
'######   Page Name -- HelpDesk_HDUP_IRType      #########  
'######   Date 30-Sep 2010  ###########################    
'############   Developed By Abhishek  ####################
'##########################################################

Partial Class HelpDesk_HDUP_IRTypeCategory
    Inherits System.Web.UI.Page

    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            btnSave.Attributes.Add("onclick", "return validateIRCategory();")

            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IR Type Category']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IR Type Category']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnSave.Enabled = False
                        btnNew.Enabled = False
                    End If

                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                    End If

                    If Request.QueryString("Action") IsNot Nothing Then
                        If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                            If strBuilder(2) = "0" Then
                                btnSave.Enabled = False
                            End If
                        End If
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If


            If Not Page.IsPostBack Then
                If Request.QueryString("Action") IsNot Nothing Then
                    If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                        ViewIRCateg()
                    End If
                    If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                        lblError.Text = objeAAMSMessage.messInsert
                    End If
                End If

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Try
                If (Not Request.QueryString("Action") = Nothing) Then

                    Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                    Dim objhdIRTypeCate As New AAMS.bizHelpDesk.bzIRTypeCategory

                    objInputXml.LoadXml("<HD_UPDATEIR_CATEGORIES_INPUT><IR_CATEGORIES HD_IR_TYCAT_ID='' HD_IR_TYCAT_NAME='' /></HD_UPDATEIR_CATEGORIES_INPUT>")

                    If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
                        objInputXml.DocumentElement.SelectSingleNode("IR_CATEGORIES").Attributes("HD_IR_TYCAT_ID").Value = objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString().Trim)
                    End If
                    objInputXml.DocumentElement.SelectSingleNode("IR_CATEGORIES").Attributes("HD_IR_TYCAT_NAME").Value = txtIRTypeCat.Text.Trim()
                    objOutputXml = objhdIRTypeCate.Update(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
                            lblError.Text = objeAAMSMessage.messUpdate
                        Else
                            Response.Redirect("HDUP_IRTypeCategory.aspx?Action=US|" & objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("IR_CATEGORIES").Attributes("HD_IR_TYCAT_ID").Value.Trim), False)
                            lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."

                        End If
                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            End Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub ViewIRCateg()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument

            Dim objhdIRTypeCate As New AAMS.bizHelpDesk.bzIRTypeCategory

            objInputXml.LoadXml("<HD_VIEWIR_CATEGORIES_INPUT><HD_IR_TYCAT_ID></HD_IR_TYCAT_ID></HD_VIEWIR_CATEGORIES_INPUT>")
            If Request.QueryString("Action") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("HD_IR_TYCAT_ID").InnerText = objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString().Trim)
            End If

            objOutputXml = objhdIRTypeCate.View(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                txtIRTypeCat.Text = objOutputXml.DocumentElement.SelectSingleNode("IR_CATEGORIES").Attributes("HD_IR_TYCAT_NAME").Value.Trim()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If Request.QueryString("Action") IsNot Nothing Then
                If Request.QueryString("Action") <> "I" Then
                    ViewIRCateg()
                Else
                    txtIRTypeCat.Text = ""
                End If
            Else
                txtIRTypeCat.Text = ""
            End If
           
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

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Response.Redirect("HDUP_IRTypeCategory.aspx?Action=I", False)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
       
    End Sub
End Class

