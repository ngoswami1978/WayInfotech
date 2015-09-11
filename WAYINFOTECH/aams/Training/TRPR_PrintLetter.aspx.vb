
Partial Class Training_TRPR_PrintLetter
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl

        Try

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If Not Page.IsPostBack Then
                If Not Request.QueryString("LetterType") Is Nothing Then
                    hdLetterType.Value = Request.QueryString("LetterType").ToString()
                End If

                If Not Session("LetterType") Is Nothing Then
                    'hdLetterType.Value = Session("LetterType")
                End If


            End If
            BindData()

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub
    Private Sub BindData()
        '        <TR_VIEWLETTERS_OUTPUT>
        '<DETAILS TR_CLETTER_ID='' TR_CLETTER='' TR_CLETTER_DATESEND='' EMPLOYEE_ID='' TR_COURSES_ID='' LCODE='' STAFFNAME='' TR_CLETTER_DATERESEND='' />
        '<Errors Status="">
        '<Error Code="" Description="" />
        '</Errors>
        '</TR_VIEWLETTERS_OUTPUT>


        '<TR_VIEWLETTERS_INPUT>
        '<DETAILS TR_CLETTER_IDS ='' LETTERTYPE='' />
        '</TR_VIEWLETTERS_INPUT>

        Dim objInputXml As New XmlDocument
        Dim objOutputXml As New XmlDocument
        objInputXml.LoadXml("<TR_VIEWLETTERS_INPUT><DETAILS TR_CLETTER_IDS ='' LETTERTYPE='' /></TR_VIEWLETTERS_INPUT>")
        Dim objds As New DataSet
        Dim xreader As XmlReader
        objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("TR_CLETTER_IDS").Value = Session("LetterIDs")
        objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("LETTERTYPE").Value = hdLetterType.Value
        Dim objbzParticipant As New AAMS.bizTraining.bzParticipant
        objOutputXml = objbzParticipant.ViewLetters_Print_Email(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            xreader = New XmlNodeReader(objOutputXml)
            objds.ReadXml(xreader)
            GridView1.DataSource = objds.Tables("DETAILS")
            GridView1.DataBind()
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If

    End Sub

    Private Sub GetTemplateInvitation(ByVal txtLetter As TextBox)
        Dim strbody As String = ""
        Dim objInputTempXml, objOutputTempXml, objSecurityXml As New XmlDocument
        Dim objbzbzTemplate As New AAMS.bizMaster.bzTemplate
        Dim ds As New DataSet
        Dim strAoffice As String = "DEL"
        Try
            objInputTempXml.LoadXml("<HD_DOCUMENTTEMPLATE_INPUT><TEMPLATES TemplateName='' AOFFICE ='' AIRLINECODE ='' /></HD_DOCUMENTTEMPLATE_INPUT>")
            objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("TemplateName").Value() = "Invitation Letter"
            objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("AOFFICE").Value() = strAoffice
            objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("AIRLINECODE").Value() = ""
            objOutputTempXml = objbzbzTemplate.GetDocumentTemplate(objInputTempXml)
            Session("Letter") = objOutputTempXml.OuterXml
            If objOutputTempXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                strbody = objOutputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("BDRTemplate").Value
                Dim strDate As String = ""
                strDate = System.DateTime.Now().ToString("dd MMM yy")
                strbody = strbody.Replace("<<DATE>>", " " & strDate)
                txtLetter.Text = strbody
                ' td1.InnerHtml = strbody
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
   
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
        Dim txt As TextBox
        txt = CType(e.Row.Cells(0).FindControl("txtLetter"), TextBox)
        'txt.Style.Clear()
        'txt.Attributes.Clear()
        txt.CssClass = "ScrollColor"

        'Dim lbl As Label
        'lbl = CType(e.Row.Cells(0).FindControl("lblLetter"), Label)

        'GetTemplateInvitation(txt)
    End Sub
End Class
