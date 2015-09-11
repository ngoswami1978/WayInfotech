
Partial Class Setup_MSUP_EmployeeSupervisory
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Protected strIndex As String
    Protected flagPermission As String
    Public strBuilder As New StringBuilder
    Dim objEn As New EncyrptDeCyrpt


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
        '***************************************************************************************
        '*****************************Check Session*********************************************
        Try

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            '***************************************************************************************
            If Session("Action").ToString().Split("|").GetValue(0) = "I" Then
                BindSuperVisoryRight()
            End If

            'security check.
            CheckSecurity()
            If Not Page.IsPostBack Then
                Bindata()
                If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                    BindSuperVisoryRight()
                    ViewSupervisoryRight()
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
#Region "CheckSecurity()::This method is used for security check."
    Private Sub CheckSecurity()
        Try
            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee-Supervisory']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee-Supervisory']").Attributes("Value").Value)
                End If

                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                End If

                If strBuilder(0) = "0" Then
                    btnSuperSave.Enabled = False
                End If

                If strBuilder(2) = "0" Then
                    btnSuperSave.Enabled = False
                End If

            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    Sub Bindata()
        Dim TabText As New ArrayList()
        TabText.Add("Employee")
        TabText.Add("Permission")
        TabText.Add("Group")
        TabText.Add("IP")
        TabText.Add("Supervisory")
        TabText.Add("HelpDesk")

        theTabStrip.DataSource = TabText
        theTabStrip.DataBind()
    End Sub
    Protected Sub theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound
        Dim Button1 As Button
        Button1 = e.Item.FindControl("Button1")
        If e.Item.ItemIndex = 4 Then
            Button1.CssClass = "headingtab"
        End If
        If Session("Action").ToString().Split("|").GetValue(0) = "I" Then
            Button1.Enabled = False
        End If
        If Button1.Text = "Supervisory" Then
            Button1.Width = 80
        End If

        If Session("Security") IsNot Nothing Then
            Dim strButtonText As String
            strButtonText = Button1.Text
            Select Case strButtonText
                Case "Employee"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Employee") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Permission"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Employee-Permission") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If

                Case "Group"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Employee-ChainCode") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If

                Case "IP"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Employee-Ip") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If

                Case "Supervisory"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Employee-Supervisory") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "HelpDesk"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Employee-HelpDesk") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If

            End Select
        End If
        Button1 = e.Item.FindControl("Button1")
        Button1.Attributes.Add("onclick", "return ColorMethod('" & Button1.ClientID.ToString() & "',6);")
    End Sub
    Private Sub BindSuperVisoryRight()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzSupervisorRights As New AAMS.bizMaster.bzSupervisorRights
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            'Here Back end Method Call
            objOutputXml = objbzSupervisorRights.List()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdSuperVisor.DataSource = ds.Tables("Supervisory")
                grdSuperVisor.DataBind()
            Else

                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            objbzSupervisorRights = Nothing
            ds = Nothing
        End Try
    End Sub

    Private Sub ViewSupervisoryRight()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
        BindSuperVisoryRight()
        objInputXml.LoadXml("<MS_VIEWEMPLOYEESUPERVISORY_INPUT><EmployeeID></EmployeeID></MS_VIEWEMPLOYEESUPERVISORY_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerXml = Session("Action").ToString().Split("|").GetValue(1)
        'Here Back end Method Call
        objOutputXml = objbzEmployee.GetSupervisoryRights(objInputXml)
        Session("ViewEmployeeData") = Session("ViewEmployeeData").ToString().Remove(8, 1).Insert(8, 1)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            With objOutputXml.DocumentElement.SelectSingleNode("Employee")
                Dim superGridItem As DataGridItem
                Dim chkSuperVisor As CheckBox
                Dim hdDomainId As HiddenField
                For Each superGridItem In grdSuperVisor.Items
                    hdDomainId = superGridItem.FindControl("hdDomainId")
                    chkSuperVisor = superGridItem.FindControl("chkSuperVisor")
                    If objOutputXml.DocumentElement.SelectNodes("Supervisory[@DomainID= '" & hdDomainId.Value & "' ]").Count <> 0 Then
                        chkSuperVisor.Checked = True
                    End If
                Next
            End With
        Else
            If objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value = "No Record Found!" Then

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        End If
    End Sub
    Protected Sub btnSuperSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSuperSave.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objIxml As New XmlDocument
        Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
        objIxml.LoadXml("<Document><Supervisory DomainID='' DomainName='' /></Document>")
        objInputXml.LoadXml("<MS_UPDATEEMPLOYEESUPERVISORY_INPUT><EmployeeID></EmployeeID><ChangedBy></ChangedBy></MS_UPDATEEMPLOYEESUPERVISORY_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("Action").ToString().Split("|").GetValue(1)
        ''added by ashish
        objInputXml.DocumentElement.SelectSingleNode("ChangedBy").InnerText = Session("LoginSession").ToString().Split("|")(0)  ''login user
        ''end


        Dim superGridItem As DataGridItem
        Dim chkSuperVisor As CheckBox
        Dim lblDomainName As Label
        Dim hdDomainId As HiddenField
        Try
            Dim objxmldocfrag As XmlDocumentFragment

            For Each superGridItem In grdSuperVisor.Items
                lblDomainName = superGridItem.FindControl("lblDomainName")
                chkSuperVisor = superGridItem.FindControl("chkSuperVisor")
                hdDomainId = superGridItem.FindControl("hdDomainId")
                If chkSuperVisor.Checked = True Then
                    objIxml.DocumentElement.SelectSingleNode("Supervisory").Attributes("DomainID").Value = hdDomainId.Value
                    objIxml.DocumentElement.SelectSingleNode("Supervisory").Attributes("DomainName").Value = lblDomainName.Text
                    objxmldocfrag = objInputXml.CreateDocumentFragment()
                    objxmldocfrag.InnerXml = objIxml.DocumentElement.InnerXml
                    objInputXml.DocumentElement.AppendChild(objxmldocfrag)
                End If
            Next
            'Here Back end Method Call
            objOutputXml = objbzEmployee.SaveSupervisoryRights(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                    lblError.Text = objeAAMSMessage.messUpdate
                Else
                    lblError.Text = objeAAMSMessage.messInsert
                End If
                'security check.
                CheckSecurity()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub


    Protected Sub btnSuperReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSuperReset.Click
        ViewSupervisoryRight()
    End Sub
End Class
