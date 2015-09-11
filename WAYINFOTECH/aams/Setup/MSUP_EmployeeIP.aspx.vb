
Partial Class Setup_MSUP_EmployeeIP
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
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl

            btnIpAdd.Attributes.Add("onclick", "return ValidateEmployeeIPPool();")
            '***************************************************************************************
            '*****************************Check Session*********************************************
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            CheckSecurity()
            '***************************************************************************************
            If Not Page.IsPostBack Then
                'Bind control for IPPool
                Session("EmployeeIP") = Nothing
                objeAAMS.BindDropDown(drpIpPool, "IPPOOL", True)
                Bindata()
                If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                    ViewIP()
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee-Ip']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee-Ip']").Attributes("Value").Value)
                End If


                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                End If

                If strBuilder(1) = "0" Then
                    btnIpSave.Enabled = False
                End If
                If strBuilder(2) = "0" And strBuilder(1) = "0" Then 'Add =false /Modify= false
                    btnIpSave.Enabled = False
                    btnIpAdd.Enabled = False
                End If
                If strBuilder(2) = "0" And strBuilder(1) = "1" Then 'Modify =false /Add= True
                    If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                        btnIpSave.Enabled = False
                        btnIpAdd.Enabled = False
                    Else
                        btnIpSave.Enabled = True
                        btnIpAdd.Enabled = True
                    End If
                End If
                If strBuilder(2) = "1" And strBuilder(1) = "0" Then 'Modify= true/Add =false
                    If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                        btnIpSave.Enabled = True
                        btnIpAdd.Enabled = True
                    Else
                        btnIpSave.Enabled = False
                        btnIpAdd.Enabled = False
                    End If
                End If
                'If strBuilder(0) = "0" Then
                '    btnIpSave.Enabled = False
                'End If

                'If strBuilder(2) = "0" Then
                '    btnIpSave.Enabled = False
                'End If

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
        If e.Item.ItemIndex = 3 Then
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
    Private Sub ViewIP()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet

        objInputXml.LoadXml("<MS_VIEWEMPLOYEEIP_INPUT><EmployeeID></EmployeeID></MS_VIEWEMPLOYEEIP_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerXml = Session("Action").ToString().Split("|").GetValue(1)
        'Here Back end Method Call
        objOutputXml = objbzEmployee.GetAssignedIP(objInputXml)

        Session("ViewEmployeeData") = Session("ViewEmployeeData").ToString().Remove(6, 1).Insert(6, 1)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            If objOutputXml.DocumentElement.SelectSingleNode("IPRESTRICTION").InnerText = "0" Then
                chkIPRestriction.Checked = False
            ElseIf objOutputXml.DocumentElement.SelectSingleNode("IPRESTRICTION").InnerText = "1" Then
                chkIPRestriction.Checked = True
            End If


            Dim objIAppendXml, objOAppendXml As New XmlDocument
            Dim objIXml As New XmlDocument
            Dim objxmldocfrag As XmlDocumentFragment
            Dim objNode As XmlNode
            Dim objNodeList As XmlNodeList

            objIAppendXml.LoadXml("<Document><IPAddress IP=''></IPAddress></Document>")
            If Session("EmployeeIP") Is Nothing Then
                objOAppendXml.LoadXml("<MS_UPDATEEMPLOYEEIP_INPUT><EmployeeID></EmployeeID><IPRESTRICTION></IPRESTRICTION><ChangedBy></ChangedBy></MS_UPDATEEMPLOYEEIP_INPUT>")
            Else
                objOAppendXml.LoadXml(Convert.ToString(Session("EmployeeIP")))
            End If
            objOAppendXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("Action").ToString().Split("|").GetValue(1)
            ''added by ashish
            objOAppendXml.DocumentElement.SelectSingleNode("ChangedBy").InnerText = Session("LoginSession").ToString().Split("|")(0)  ''login user
            ''end here
            If objOutputXml.DocumentElement.SelectSingleNode("IPRESTRICTION").InnerText = "1" Then
                chkIPRestriction.Checked = True
                drpIpPool.Enabled = True
                btnIpApply.Enabled = True
                btnIpAdd.Enabled = True
                btnIpSave.Enabled = True
                Button14.Enabled = True
                txtIP.ReadOnly = False
                txtIP.CssClass = "textbox"

            Else
                drpIpPool.Enabled = False
                txtIP.ReadOnly = True
                chkIPRestriction.Checked = False
                btnIpApply.Enabled = False
                btnIpAdd.Enabled = False

                txtIP.CssClass = "textboxgrey"
                'btnIpSave.Enabled = False
                'Button14.Enabled = False
            End If
            If chkIPRestriction.Checked = True Then
                objOAppendXml.DocumentElement.SelectSingleNode("IPRESTRICTION").InnerText = "1"
            Else
                objOAppendXml.DocumentElement.SelectSingleNode("IPRESTRICTION").InnerText = "0"
            End If

            objNodeList = objOutputXml.DocumentElement.SelectNodes("IPAddress")
            For Each objNode In objNodeList
                objIAppendXml.DocumentElement.SelectSingleNode("IPAddress").Attributes("IP").Value = objNode.Attributes("IP").Value
                objxmldocfrag = objOAppendXml.CreateDocumentFragment()
                objxmldocfrag.InnerXml = objIAppendXml.DocumentElement.InnerXml
                objOAppendXml.DocumentElement.AppendChild(objxmldocfrag)
            Next
            Session("EmployeeIP") = objOAppendXml.OuterXml
            objXmlReader = New XmlNodeReader(objOAppendXml)
            ds.ReadXml(objXmlReader)
            gvIPPool.DataSource = ds.Tables("IPAddress")
            gvIPPool.DataBind()

        Else
            'btnIpApply.Enabled = False
            'btnIpAdd.Enabled = False
            'btnIpSave.Enabled = False
            'Button14.Enabled = False
            If objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value = "No Record Found!" Then

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        End If
    End Sub
    Protected Sub btnIpAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIpAdd.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objIxml As New XmlDocument
        Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
        Dim objxmldocfrag As XmlDocumentFragment
        Dim objXmlReader As XmlReader
        Dim ds As New DataSet
        Try
            objIxml.LoadXml("<Document><IPAddress IP=''></IPAddress></Document>")
            If Session("EmployeeIP") IsNot Nothing Then
                objInputXml.LoadXml(Convert.ToString(Session("EmployeeIP")))
            Else
                objInputXml.LoadXml("<MS_UPDATEEMPLOYEEIP_INPUT><EmployeeID></EmployeeID><IPRESTRICTION></IPRESTRICTION><ChangedBy></ChangedBy></MS_UPDATEEMPLOYEEIP_INPUT>")
            End If
            objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("Action").ToString().Split("|").GetValue(1)
            If chkIPRestriction.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("IPRESTRICTION").InnerText = "1"
            Else
                objInputXml.DocumentElement.SelectSingleNode("IPRESTRICTION").InnerText = "0"

            End If
 
            objInputXml.DocumentElement.SelectSingleNode("ChangedBy").InnerText = Session("LoginSession").ToString().Split("|")(0)  ''login user
            objIxml.DocumentElement.SelectSingleNode("IPAddress").Attributes("IP").Value = txtIP.Text
            objxmldocfrag = objInputXml.CreateDocumentFragment()
            objxmldocfrag.InnerXml = objIxml.DocumentElement.InnerXml
            objInputXml.DocumentElement.AppendChild(objxmldocfrag)
            Session("EmployeeIP") = objInputXml.OuterXml

            objXmlReader = New XmlNodeReader(objInputXml)
            ds.ReadXml(objXmlReader)
            gvIPPool.DataSource = ds.Tables("IPAddress")
            gvIPPool.DataBind()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub


    Protected Sub btnIpSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIpSave.Click
        Dim objInputXml, objOutputXml As New XmlDocument

        Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
        Try
            If Session("EmployeeIP") IsNot Nothing Then
                objInputXml.LoadXml(Session("EmployeeIP"))
                If chkIPRestriction.Checked = True Then
                    objInputXml.DocumentElement.SelectSingleNode("IPRESTRICTION").InnerText = "1"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("IPRESTRICTION").InnerText = "0"
                End If
                'Here Back end Method Call
                objOutputXml = objbzEmployee.AssignIP(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                        lblError.Text = objeAAMSMessage.messUpdate
                    Else
                        lblError.Text = objeAAMSMessage.messInsert
                    End If
                    CheckSecurity()
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            Else
                lblError.Text = "Please add atleast one IP."
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub


    Protected Sub btnIpApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIpApply.Click
        Try
            ViewIPPoolGroup()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Sub ViewIPPoolGroup()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objIxml As New XmlDocument
            Dim objxmldocfrag As XmlDocumentFragment
            'Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            Dim objbzIPPool As New AAMS.bizMaster.bzIPPool
            Dim objNode As XmlNode
            Dim objNodeList As XmlNodeList
            objInputXml.LoadXml("<MS_VIEWIPPOOL_INPUT><PoolID></PoolID><PoolName></PoolName><Aoffice></Aoffice><Department_Name></Department_Name></MS_VIEWIPPOOL_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("PoolID").InnerXml = drpIpPool.SelectedValue
            'Here Back end Method Call
            objOutputXml = objbzIPPool.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                objIxml.LoadXml("<Document><IPAddress IP=''></IPAddress></Document>")
                'If Session("EmployeeIP") Is Nothing Then
                objInputXml.LoadXml("<MS_UPDATEEMPLOYEEIP_INPUT><EmployeeID></EmployeeID><IPRESTRICTION></IPRESTRICTION><ChangedBy></ChangedBy></MS_UPDATEEMPLOYEEIP_INPUT>")
                'Else
                '    objInputXml.LoadXml(Convert.ToString(Session("EmployeeIP")))
                'End If
                If chkIPRestriction.Checked = True Then
                    objInputXml.DocumentElement.SelectSingleNode("IPRESTRICTION").InnerText = "1"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("IPRESTRICTION").InnerText = "0"

                End If

                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("Action").ToString().Split("|").GetValue(1)
                objInputXml.DocumentElement.SelectSingleNode("ChangedBy").InnerText = Session("LoginSession").ToString().Split("|")(0)
                objNodeList = objOutputXml.DocumentElement.SelectNodes("IPPOOL/IPAddress")

                For Each objNode In objNodeList
                    objIxml.DocumentElement.SelectSingleNode("IPAddress").Attributes("IP").Value = objNode.InnerText
                    objxmldocfrag = objInputXml.CreateDocumentFragment()
                    objxmldocfrag.InnerXml = objIxml.DocumentElement.InnerXml
                    objInputXml.DocumentElement.AppendChild(objxmldocfrag)
                Next
                Session("EmployeeIP") = objInputXml.OuterXml

                BindIpdetails(objInputXml)
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Sub BindIpdetails(ByVal objOutputXml As XmlDocument)
        Try
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet

            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            gvIPPool.DataSource = ds.Tables("IPAddress")
            gvIPPool.DataBind()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub Button14_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button14.Click
        drpIpPool.SelectedIndex = 0
        txtIP.Text = String.Empty
        Response.Redirect("MSUP_EmployeeIP.aspx", False)
    End Sub

    'Protected Sub grdIPPool_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdIPPool.ItemCommand
    '    If e.CommandName = "DeleteX" Then
    '        DeleteIP(e.CommandArgument)
    '    End If
    'End Sub
    Sub DeleteIP(ByVal strIP As String)
        Dim objInputXml As New XmlDocument
        Dim objXmlReader As XmlReader
        Dim ds As New DataSet
        Try
            If Session("EmployeeIP") IsNot Nothing Then
                objInputXml.LoadXml(Convert.ToString(Session("EmployeeIP")))

                If objInputXml.DocumentElement.SelectNodes("IPAddress").Count > 0 Then
                    objInputXml.DocumentElement.RemoveChild(objInputXml.DocumentElement.SelectSingleNode("IPAddress[@IP='" & strIP & "']"))
                End If

                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("Action").ToString().Split("|").GetValue(1)

                If objInputXml.DocumentElement.SelectNodes("IPAddress").Count > 0 Then
                    objXmlReader = New XmlNodeReader(objInputXml)
                    ds.ReadXml(objXmlReader)
                    gvIPPool.DataSource = ds.Tables("IPAddress")
                    gvIPPool.DataBind()
                Else

                    gvIPPool.DataSource = Nothing
                    gvIPPool.DataBind()
                End If
              
                Session("EmployeeIP") = objInputXml.OuterXml
            Else
                lblError.Text = "Unable to Delete"
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub chkIPRestriction_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIPRestriction.CheckedChanged
        If chkIPRestriction.Checked Then
            drpIpPool.Enabled = True
            txtIP.ReadOnly = False
            btnIpAdd.Enabled = True
            btnIpApply.Enabled = True
            btnIpSave.Enabled = True
            Button14.Enabled = True
            txtIP.CssClass = "textbox"
        Else
            drpIpPool.Enabled = False
            drpIpPool.Enabled = False
            txtIP.ReadOnly = True
            btnIpAdd.Enabled = False
            btnIpApply.Enabled = False

            txtIP.CssClass = "textboxgrey"
            'btnIpSave.Enabled = False
            'Button14.Enabled = False
        End If

    End Sub

  

    Protected Sub gvIPPool_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvIPPool.RowCommand
        If e.CommandName = "DeleteX" Then
            DeleteIP(e.CommandArgument)
        End If
    End Sub
End Class
