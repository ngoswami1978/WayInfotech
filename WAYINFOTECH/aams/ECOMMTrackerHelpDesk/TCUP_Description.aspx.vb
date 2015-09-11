
Partial Class ETHelpDesk_HDUP_Description
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
        End If
        Try
            If Not Request.QueryString("Action") Is Nothing Then
                hdPageStatus.Value = Request.QueryString("Action").ToString
            End If
            If Not Request.QueryString("LCode") Is Nothing Then
                hdPageLCode.Value = Request.QueryString("LCode").ToString
            End If
            If Not Request.QueryString("HD_RE_ID") Is Nothing Then
                hdPageHD_RE_ID.Value = Request.QueryString("HD_RE_ID").ToString
            End If
            'Security code
            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ET Log HD Call']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ET Log HD Call']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            'End
            If Not Page.IsPostBack Then
                btnSave.Attributes.Add("onClick", "return ValidateForm();")
                BindRep()
                BindData()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    Sub BindRep()
        Dim TabText As New ArrayList()
        TabText.Add("Call")
        TabText.Add("Description")
        TabText.Add("Solution")
        theTabStrip.DataSource = TabText
        theTabStrip.DataBind()
    End Sub
    Sub BindData()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        '        Dim objCall As New AAMS.bizETrackerHelpDesk.bzCall
        Dim objCall As New AAMS.bizETrackerHelpDesk.bzCall

        Try

            objInputXml.LoadXml("<HD_GETREQUESTDESCRIPTION_INPUT><HD_RE_ID></HD_RE_ID></HD_GETREQUESTDESCRIPTION_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = hdPageHD_RE_ID.Value
            objOutputXml = objCall.ViewRequestDesc(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)

                If ds.Tables("REQUESTDESCRIPTION").Rows.Count <> 0 Then
                    gvDescription.DataSource = ds.Tables("REQUESTDESCRIPTION")
                    gvDescription.DataBind()

                Else
                    gvDescription.DataSource = String.Empty
                    gvDescription.DataBind()
                End If

            Else
                If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value <> "No Record Found!") Then
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                Else
                    ' code for binding header when no record found.
                    Dim dt As New DataTable
                    Dim dc As New DataColumn("EmployeeName", GetType(String))
                    Dim dc1 As New DataColumn("DATETIME", GetType(String))
                    Dim dc2 As New DataColumn("ACTION_TAKEN", GetType(String))
                    Dim dr As DataRow
                    dt.Columns.Add(dc)
                    dt.Columns.Add(dc1)
                    dt.Columns.Add(dc2)
                    dr = dt.NewRow()
                    dt.Rows.Add(dr)
                    gvDescription.DataSource = dt
                    gvDescription.DataBind()

                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
    Protected Sub theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound
        Dim Button1 As Button
        Button1 = e.Item.FindControl("Button1")
        If e.Item.ItemIndex = 1 Then
            Button1.CssClass = "headingtab"
        End If
        If Request.QueryString("Action").ToString() = "I" Then
            Button1.Enabled = False
        End If
        Button1 = e.Item.FindControl("Button1")
        Button1.Attributes.Add("onclick", "return ColorMethod('" & Button1.ClientID.ToString() & "',3);")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            txtDescription.Text = ""
            lblError.Text = ""
            BindData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim objInputXml As New XmlDocument
            Dim objOutputXml As New XmlDocument
            '            Dim objCall As New AAMS.bizETrackerHelpDesk.bzCall
            Dim objCall As New AAMS.bizETrackerHelpDesk.bzCall

            ' Load Input Xml.
            objInputXml.LoadXml("<HD_UPDATEREQUESTDESCRIPTION_INPUT><REQUESTDESCRIPTION HD_RE_ID='' EmployeeID='' ACTION_TAKEN='' DATETIME=''/></HD_UPDATEREQUESTDESCRIPTION_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("REQUESTDESCRIPTION").Attributes("HD_RE_ID").Value = hdPageHD_RE_ID.Value
            objInputXml.DocumentElement.SelectSingleNode("REQUESTDESCRIPTION").Attributes("EmployeeID").Value = Session("LoginSession").ToString().Split("|")(0)
            objInputXml.DocumentElement.SelectSingleNode("REQUESTDESCRIPTION").Attributes("ACTION_TAKEN").Value = txtDescription.Text
            objInputXml.DocumentElement.SelectSingleNode("REQUESTDESCRIPTION").Attributes("DATETIME").Value = ""

            ' Calling update method for update.
            objOutputXml = objCall.SaveRequestDesc(objInputXml)
            '   Checking error status. 
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                BindData()
                lblError.Text = objeAAMSMessage.messInsert
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
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
End Class
