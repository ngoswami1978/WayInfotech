Imports System.Xml
Imports System.Data

Partial Class Order_MSUP_MailGroup
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objbzEmail As AAMS.bizMaster.bzEmailGroup
    Dim objbzOffice As AAMS.bizMaster.bzAOffice
    Dim groupid As Int16
    Dim strBuilder As New StringBuilder

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

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("MSUP_MailGroup.aspx?id=-1")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        btnAddemail.Attributes.Add("OnClick", "javascript:return checkselectedemail()")
        btnRemove.Attributes.Add("OnClick", "javascript:return checkselectedemail2()")
        btnSave.Attributes.Add("OnClick", "javascript:return validatepage()")
        btnAdd.Attributes.Add("OnClick", "javascript:return checkemail()")

        Dim objOutputXml, listXML, xml1 As New XmlDocument
        listXML.LoadXml("<EMAIL><EMAIL_DETAILS ID='' EMAIL='' /></EMAIL>")
        objbzEmail = New AAMS.bizMaster.bzEmailGroup
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If
        Try
            If IsPostBack = False Then
                FillOffice()
                objOutputXml = objbzEmail.GetEmailIDs()
                Session("OriginalXML1") = objOutputXml.InnerXml
                Session("ListXML") = Nothing
                Session("MainXML") = Nothing
                Dim ErrorNode As XmlNode
                ErrorNode = objOutputXml.DocumentElement.SelectSingleNode("Errors")
                If ErrorNode.Attributes("Status").Value.ToUpper = "FALSE" Then
                    Session("MainXML") = objOutputXml
                    Dim xmlnodemain As XmlNode = CType(Session("MainXML"), XmlDocument).DocumentElement.SelectSingleNode("Errors")
                    CType(Session("MainXML"), XmlDocument).DocumentElement.RemoveChild(xmlnodemain)
                    bindTreeview()
                Else
                    lblError.Text = ErrorNode.SelectSingleNode("Errors/Error").Attributes("Description").InnerText.ToString()
                End If
                groupid = Request.QueryString("id")
                If groupid > 0 Then
                    ViewGroup()
                    If Request.QueryString("msg") Is Nothing Then
                        lblError.Text = ""
                    Else
                        lblError.Text = Request.QueryString("msg").ToString()
                    End If
                Else
                    div1.Attributes.Add("style", "display:none")
                    ddlOffice.Attributes.Add("style", "display:none")
                    Session("ListXML") = listXML
                End If
                objOutputXml = Nothing
                listXML = Nothing
                xml1 = Nothing
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OfficeId']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OfficeId']").Attributes("Value").Value)
                End If
                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                End If
                If Not Request.QueryString("Action") Is Nothing Then
                    If (Request.QueryString("Action").ToUpper() = "E") Then
                        If strBuilder(2) = "0" Then
                            btnSave.Enabled = False
                        End If
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objOutputXml = Nothing
            objbzEmail = Nothing
        End Try
    End Sub
    Protected Sub treeview1_TreeNodeDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles treeview1.TreeNodeDataBound
        If e.Node.Depth > 0 Then
            If e.Node.Depth = 1 Then
                e.Node.Text = "<span onclick='javascript:return f1(1," + e.Node.Value + ");'>" & e.Node.Text & "</span>"
            Else
                e.Node.Text = "<span onclick='javascript:return f1(2," + e.Node.Value + ");'>" & e.Node.Text & "</span>"
            End If
        Else
            e.Node.Text = "<span onclick='javascript:return f2();'>" & e.Node.Text & "</span>"
        End If


    End Sub
    Protected Sub btnAdd1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddemail.Click
        Dim objXML1, objinputXml, MainXML1, listXML1 As New XmlDocument
        Dim Xmlnode1, Xmlnode2, Xmlnode3, Xmlnode4 As XmlNode
        Dim nodelist, nodelist2 As XmlNodeList
        Dim existemail As Boolean

        ' objinputXml.LoadXml("<EMAIL><EMAIL_DETAILS ID='' AOFFICE=''/></EMAIL>")
        Try
            Dim id, id2, ID3 As Integer
            id = Convert.ToInt32(hdnTree1.Value)
            id2 = Convert.ToInt32(hdnTree2.Value)
            hdnTree1.Value = ""
            hdnTree2.Value = ""
            listXML1 = CType(Session("ListXML"), XmlDocument)
            nodelist2 = listXML1.DocumentElement.SelectNodes("EMAIL_DETAILS[@ID!='']")
            Dim i As Integer = nodelist2.Count
            MainXML1 = CType(Session("MainXML"), XmlDocument)
            If id = 1 Then
                Xmlnode1 = MainXML1.DocumentElement.SelectSingleNode("AOFFICE_DETAILS[@ID='" & id2 & "']")
                nodelist = Xmlnode1.SelectNodes("EMPLOYEE_DETAIL")
                For Each Xmlnode2 In nodelist
                    If i = 0 Then
                        Xmlnode3 = listXML1.DocumentElement.SelectSingleNode("EMAIL_DETAILS")
                        Xmlnode4 = Xmlnode3.CloneNode(True)
                        Xmlnode4.Attributes("ID").InnerText = Xmlnode2.Attributes("EMPLOYEEID").InnerText
                        Xmlnode4.Attributes("EMAIL").InnerText = Xmlnode2.Attributes("EMAIL").InnerText
                        existemail = CHECK_DUPLICATE_EMAIL(listXML1, Xmlnode2.Attributes("EMAIL").InnerText.ToString())
                        If existemail = False Then
                            listXML1.DocumentElement.AppendChild(Xmlnode4)
                        Else
                            Throw (New Exception("Email Id Already Exist"))
                        End If
                    Else
                        Xmlnode3 = listXML1.DocumentElement.SelectSingleNode("EMAIL_DETAILS")
                        Xmlnode4 = Xmlnode3.CloneNode(True)
                        Xmlnode4.Attributes("ID").InnerText = Xmlnode2.Attributes("EMPLOYEEID").InnerText
                        Xmlnode4.Attributes("EMAIL").InnerText = Xmlnode2.Attributes("EMAIL").InnerText
                        existemail = CHECK_DUPLICATE_EMAIL(listXML1, Xmlnode2.Attributes("EMAIL").InnerText.ToString())
                        If existemail = False Then
                            listXML1.DocumentElement.AppendChild(Xmlnode4)
                        Else
                            Throw (New Exception("Email Id Already Exist"))
                        End If
                    End If
                Next
                MainXML1.DocumentElement.RemoveChild(Xmlnode1)
            ElseIf id = 2 Then
                Xmlnode1 = MainXML1.DocumentElement.SelectSingleNode("AOFFICE_DETAILS/EMPLOYEE_DETAIL[@EMPLOYEEID='" & id2 & "']")
                Xmlnode2 = MainXML1.DocumentElement.SelectSingleNode("AOFFICE_DETAILS/EMPLOYEE_DETAIL[@EMPLOYEEID='" & id2 & "']").ParentNode
                nodelist = Xmlnode2.SelectNodes("EMPLOYEE_DETAIL")
                ID3 = Xmlnode2.Attributes("ID").InnerText
                Xmlnode3 = listXML1.DocumentElement.SelectSingleNode("EMAIL_DETAILS")
                Xmlnode4 = Xmlnode3.CloneNode(True)
                Xmlnode4.Attributes("ID").InnerText = Xmlnode1.Attributes("EMPLOYEEID").InnerText
                Xmlnode4.Attributes("EMAIL").InnerText = Xmlnode1.Attributes("EMAIL").InnerText
                existemail = CHECK_DUPLICATE_EMAIL(listXML1, Xmlnode1.Attributes("EMAIL").InnerText.ToString())
                If existemail = False Then
                    listXML1.DocumentElement.AppendChild(Xmlnode4)
                Else
                    Throw (New Exception("Email Id Already Exist"))
                End If
                listXML1.DocumentElement.AppendChild(Xmlnode4)
                If nodelist.Count > 1 Then
                    MainXML1.DocumentElement.SelectSingleNode("AOFFICE_DETAILS[@ID='" & ID3 & "']").RemoveChild(Xmlnode1)
                Else
                    MainXML1.DocumentElement.RemoveChild(Xmlnode2)
                End If

            End If
            Session("MainXML") = MainXML1
            Session("ListXML") = listXML1
            bindTreeview()
            bindlistbox()
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message()
        Finally
            objinputXml = Nothing
            Xmlnode1 = Nothing
            Xmlnode2 = Nothing
            nodelist = Nothing
        End Try
    End Sub
    Public Sub bindTreeview()
        Dim MainXML1 As New XmlDocument
        MainXML1 = CType(Session("MainXML"), XmlDocument)
        Try
            xmldatasource1.Data = MainXML1.InnerXml
            xmldatasource1.DataBind()

        Catch ex As Exception
            lblError.Text = ex.ToString()
        Finally
            MainXML1 = Nothing
        End Try
    End Sub
    Public Sub bindlistbox()
        Dim listXML As XmlDocument
        Try
            ListBox1.Items.Clear()
            listXML = Session("ListXML")
            Dim nodelist As XmlNodeList
            Dim xmlnode1 As XmlNode
            nodelist = listXML.DocumentElement.SelectNodes("EMAIL_DETAILS[@EMAIL!='']")
            For Each xmlnode1 In nodelist
                ListBox1.Items.Add(xmlnode1.Attributes("EMAIL").InnerText)
            Next
        Catch ex As Exception
            lblError.Text = ex.ToString()
        Finally
            listXML = Nothing
        End Try

    End Sub
    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Dim objXml1, MainXML1, ListXML1 As New XmlDocument
        Dim XmlFrag As XmlDocumentFragment
        Dim node1, node2, node3, node4, node5, node6, node7, node8 As XmlNode
        Dim id1 As Integer
        Dim strxml As String
        Try
            MainXML1 = CType(Session("MainXML"), XmlDocument)
            ListXML1 = CType(Session("ListXML"), XmlDocument)
            strxml = CType(Session("OriginalXML1"), String)
            objXml1.LoadXml(strxml)
            Dim str As String
            str = hdnTree3.Value
            node7 = objXml1.DocumentElement.SelectSingleNode("AOFFICE_DETAILS/EMPLOYEE_DETAIL[@EMAIL='" & str & "']")
            If node7 Is Nothing Then
                node2 = ListXML1.DocumentElement.SelectSingleNode("EMAIL_DETAILS[@EMAIL='" & str & "']")
                ListXML1.DocumentElement.RemoveChild(node2)
                Session("ListXML") = ListXML1
            Else
                node1 = objXml1.DocumentElement.SelectSingleNode("AOFFICE_DETAILS/EMPLOYEE_DETAIL[@EMAIL='" & str & "']").ParentNode
                node3 = node1.SelectSingleNode("EMPLOYEE_DETAIL[@EMAIL='" & str & "']")
                id1 = node1.Attributes("ID").InnerText

                node2 = MainXML1.DocumentElement.SelectSingleNode("AOFFICE_DETAILS[@ID='" & id1 & "']")
                If node2 Is Nothing Then ' check parent node exist or not?
                    XmlFrag = MainXML1.CreateDocumentFragment()
                    XmlFrag.InnerXml = "<AOFFICE_DETAILS ID='' AOFFICE=''><EMPLOYEE_DETAIL EMPLOYEEID='' EMPLOYEE_NAME='' EMAIL=''/></AOFFICE_DETAILS>"
                    XmlFrag.SelectSingleNode("AOFFICE_DETAILS").Attributes("ID").InnerText = id1
                    XmlFrag.SelectSingleNode("AOFFICE_DETAILS").Attributes("AOFFICE").InnerText = node1.Attributes("AOFFICE").InnerText
                    XmlFrag.SelectSingleNode("AOFFICE_DETAILS/EMPLOYEE_DETAIL").Attributes("EMPLOYEEID").InnerText = node3.Attributes("EMPLOYEEID").InnerText
                    XmlFrag.SelectSingleNode("AOFFICE_DETAILS/EMPLOYEE_DETAIL").Attributes("EMPLOYEE_NAME").InnerText = node3.Attributes("EMPLOYEE_NAME").InnerText
                    XmlFrag.SelectSingleNode("AOFFICE_DETAILS/EMPLOYEE_DETAIL").Attributes("EMAIL").InnerText = node3.Attributes("EMAIL").InnerText
                    MainXML1.DocumentElement.AppendChild(XmlFrag)
                    node6 = ListXML1.DocumentElement.SelectSingleNode("EMAIL_DETAILS[@EMAIL='" & str & "']")
                    ListXML1.DocumentElement.RemoveChild(node6)
                Else
                    node4 = MainXML1.DocumentElement.SelectSingleNode("AOFFICE_DETAILS/EMPLOYEE_DETAIL")
                    node5 = node4.CloneNode(True)
                    node5.Attributes("EMPLOYEEID").InnerText = node3.Attributes("EMPLOYEEID").InnerText
                    node5.Attributes("EMPLOYEE_NAME").InnerText = node3.Attributes("EMPLOYEE_NAME").InnerText
                    node5.Attributes("EMAIL").InnerText = node3.Attributes("EMAIL").InnerText
                    ' to check whether this email already exisit in tree node or not
                    Dim stremail As String = node3.Attributes("EMAIL").InnerText
                    node8 = node2.SelectSingleNode("EMPLOYEE_DETAIL[@EMAIL='" & stremail & "']")
                    If node8 Is Nothing Then
                        MainXML1.DocumentElement.SelectSingleNode("AOFFICE_DETAILS[@ID='" & id1 & "']").AppendChild(node5)
                    End If
                    ' end here
                    node6 = ListXML1.DocumentElement.SelectSingleNode("EMAIL_DETAILS[@EMAIL='" & str & "']")
                    ListXML1.DocumentElement.RemoveChild(node6)
                End If
                Session("MainXML") = MainXML1
                Session("ListXML") = ListXML1
            End If
            bindTreeview()
            bindlistbox()
        Catch ex As Exception
        Finally
            objXml1 = Nothing
            MainXML1 = Nothing
            ListXML1 = Nothing
            node1 = Nothing
            node2 = Nothing
            node3 = Nothing
            node4 = Nothing
            node5 = Nothing
            node6 = Nothing
            XmlFrag = Nothing
            lblError.Text = ""
        End Try

    End Sub
    Public Sub ViewGroup()
        Dim viewxml, Viewoutput, listxml As New XmlDocument
        Dim gmnc, gisp, goff, gtrg As Boolean
        Dim StrOffice As String
        StrOffice = ""
        Dim nodelist, nodelist2 As XmlNodeList
        Dim ErrorNode, xmlnode1, xmlnode2, xmlnode3, xmlnode4 As XmlNode
        viewxml.LoadXml("<MS_VIEWEMAILGROUP_INPUT><GroupID></GroupID></MS_VIEWEMAILGROUP_INPUT>")
        objbzEmail = New AAMS.bizMaster.bzEmailGroup
        Try
            listxml.LoadXml("<EMAIL><EMAIL_DETAILS ID='' EMAIL='' /></EMAIL>")
            viewxml.DocumentElement.SelectSingleNode("GroupID").InnerText = groupid
            Viewoutput = objbzEmail.View(viewxml)
            ErrorNode = Viewoutput.DocumentElement.SelectSingleNode("Errors")
            If ErrorNode.Attributes("Status").InnerText.ToUpper = "FALSE" Then
                txtGroupName.Text = Viewoutput.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupName").InnerText
                gmnc = CBool(Viewoutput.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupMNC").InnerText)
                gisp = CBool(Viewoutput.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupISP").InnerText)
                goff = CBool(Viewoutput.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupAoffice").InnerText)
                gtrg = CBool(Viewoutput.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupTraining").InnerText)
                If gmnc = True Then
                    ddlGrpType.SelectedValue = 1
                End If
                If gisp = True Then
                    ddlGrpType.SelectedValue = 2
                End If
                If goff = True Then
                    ddlGrpType.SelectedValue = 3
                    StrOffice = Viewoutput.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("Aoffice").InnerText
                End If
                If gtrg = True Then
                    ddlGrpType.SelectedValue = 4
                    StrOffice = Viewoutput.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("TrainingAoffice").InnerText
                End If
                If goff = True Or gtrg = True Then
                    div1.Visible = True
                    ddlOffice.Visible = True
                Else
                    div1.Attributes.Add("style", "display:none")
                    ddlOffice.Attributes.Add("style", "display:none")
                End If


                nodelist = Viewoutput.DocumentElement.SelectNodes("GROUP_DETAIL/EMAIL_DETAILS")
                If nodelist.Count > 0 Then
                    xmlnode1 = listxml.DocumentElement.SelectSingleNode("EMAIL_DETAILS")
                    xmlnode2 = xmlnode1.CloneNode(True)
                    listxml.DocumentElement.RemoveChild(xmlnode1)
                    Dim I As Int16 = 0
                    For Each xmlnode3 In nodelist
                        I = I + 1
                        xmlnode2.Attributes("ID").InnerText = I
                        xmlnode2.Attributes("EMAIL").InnerText = xmlnode3.Attributes("Email").InnerText
                        listxml.DocumentElement.AppendChild(xmlnode2)
                        xmlnode2 = xmlnode1.CloneNode(True)
                        ' Remove from exisiting tree node
                        'deleteEmail(xmlnode3.Attributes("Email").InnerText)
                        ' end here
                    Next
                    Session("ListXML") = listxml
                    nodelist2 = listxml.DocumentElement.SelectNodes("EMAIL_DETAILS[@EMAIL!='']")

                    For Each xmlnode4 In nodelist2

                        deleteEmail(xmlnode4.Attributes("EMAIL").InnerText)
                        'Dim stremail As String = xmlnode4.Attributes("EMAIL").InnerText
                    Next
                    bindlistbox()
                    If StrOffice <> "" Then
                        ddlOffice.Items.FindByValue(StrOffice).Selected = True
                    End If
                End If
            Else
                lblError.Text = ErrorNode.SelectSingleNode("Errors/Error").Attributes("Description").InnerText.ToString()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            Viewoutput = Nothing
            viewxml = Nothing
            ErrorNode = Nothing
            objbzEmail = Nothing
        End Try

    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim emailstr As String
        Dim listXML1 As New XmlDocument
        Dim Xmlnode3, Xmlnode4 As XmlNode
        Dim nodelist2 As XmlNodeList
        Dim existemail As Boolean
        emailstr = txtEmail.Text.Trim()
        Try
            listXML1 = Session("listXML")
            nodelist2 = listXML1.DocumentElement.SelectNodes("EMAIL_DETAILS[@ID!='']")
            Dim i As Integer = nodelist2.Count
            Xmlnode3 = listXML1.DocumentElement.SelectSingleNode("EMAIL_DETAILS")
            Xmlnode4 = Xmlnode3.CloneNode(True)
            Xmlnode4.Attributes("ID").InnerText = i + 1
            Xmlnode4.Attributes("EMAIL").InnerText = emailstr
            existemail = CHECK_DUPLICATE_EMAIL(listXML1, emailstr)
            If existemail = False Then
                listXML1.DocumentElement.AppendChild(Xmlnode4)
            Else
                Throw (New Exception("Email Id Already Exist"))
            End If
            listXML1.DocumentElement.AppendChild(Xmlnode4)
            Session("ListXML") = listXML1
            bindlistbox()
            txtEmail.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message()
        Finally
            listXML1 = Nothing
            nodelist2 = Nothing
        End Try
    End Sub
    Public Function CHECK_DUPLICATE_EMAIL(ByVal Xml1 As XmlDocument, ByVal str1 As String) As Boolean
        Dim Xmllist As XmlDocument
        Dim nodelist As XmlNodeList
        Dim xmlnode1 As XmlNode
        'Dim str1 As String
        Try
            Xmllist = Xml1
            nodelist = Xmllist.DocumentElement.SelectNodes("EMAIL_DETAILS[@ID!='']")
            For Each xmlnode1 In nodelist
                Dim str2 As String
                str2 = xmlnode1.Attributes("EMAIL").InnerText
                If str2 = str1 Then
                    Return True
                End If
            Next
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message()
        Finally
            Xmllist = Nothing
            nodelist = Nothing
            xmlnode1 = Nothing
        End Try
        Return False
    End Function
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim inputxml, outputxml, listXML, securityxml As New XmlDocument
        Dim Xmlnode1, xmlnode2, ErrorNode As XmlNode
        Dim nodelist As XmlNodeList
        Dim strmsg As String
        Dim strErrorstaus As String = ""
        objbzEmail = New AAMS.bizMaster.bzEmailGroup
        Try
            groupid = Request.QueryString("id")
            inputxml.LoadXml("<MS_UPDATEEMAILGROUP_INPUT><GROUP_DETAIL ACTION='' GroupID='' GroupName='' EmployeeID='' GroupMNC='' GroupISP='' GroupTraining='' GroupAoffice='' Aoffice='' TrainingAoffice=''><EMAIL_DETAILS Email=''/>	</GROUP_DETAIL></MS_UPDATEEMAILGROUP_INPUT>")
            If groupid < 0 Then
                inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("ACTION").InnerText = "I"
                'inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupID").InnerText = "I"
            Else
                inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("ACTION").InnerText = "U"
                inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupID").InnerText = groupid
            End If
            inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupName").InnerText = txtGroupName.Text.Trim()
            If ddlGrpType.SelectedValue = 1 Then
                inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupMNC").InnerText = 1
                inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupISP").InnerText = ""
                inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupTraining").InnerText = ""
                inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupAoffice").InnerText = ""
            ElseIf ddlGrpType.SelectedValue = 2 Then
                inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupMNC").InnerText = ""
                inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupISP").InnerText = 1
                inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupTraining").InnerText = ""
                inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupAoffice").InnerText = ""
            ElseIf ddlGrpType.SelectedValue = 3 Then
                inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupMNC").InnerText = ""
                inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupISP").InnerText = ""
                inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupTraining").InnerText = ""
                inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupAoffice").InnerText = 1
                inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("Aoffice").InnerText = ddlOffice.SelectedValue
            ElseIf ddlGrpType.SelectedValue = 4 Then
                inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupMNC").InnerText = ""
                inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupISP").InnerText = ""
                inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupTraining").InnerText = 1
                inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupAoffice").InnerText = ""
                inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("TrainingAoffice").InnerText = ddlOffice.SelectedValue
            End If
            ' get employee id
            securityxml.LoadXml(CType(Session("Security"), String))

            '  get employee id ends here
            inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("EmployeeID").InnerText = securityxml.DocumentElement.SelectSingleNode("EmployeeID").InnerText

            listXML = Session("ListXML")
            nodelist = listXML.DocumentElement.SelectNodes("EMAIL_DETAILS[@ID!='']")
            If nodelist.Count <= 0 Then
                Throw (New Exception("Email Id Already Exist"))
            Else
                Xmlnode1 = inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL/EMAIL_DETAILS")
                xmlnode2 = Xmlnode1.CloneNode(True)
                inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").RemoveChild(Xmlnode1)
                For Each n2 As XmlNode In nodelist
                    xmlnode2.Attributes("Email").InnerText = n2.Attributes("EMAIL").InnerText
                    inputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").AppendChild(xmlnode2)
                    xmlnode2 = Xmlnode1.CloneNode(True)
                Next
            End If

            outputxml = objbzEmail.Update(inputxml)
            ErrorNode = outputxml.DocumentElement.SelectSingleNode("Errors")
            groupid = outputxml.DocumentElement.SelectSingleNode("GROUP_DETAIL").Attributes("GroupID").InnerText
            strErrorstaus = ErrorNode.Attributes("Status").InnerText
        Catch ex As Exception
            lblError.Text = ex.Message()
        Finally
            inputxml = Nothing
            outputxml = Nothing
            If strErrorstaus.ToUpper = "FALSE" Then
                Session("ListXML") = Nothing
                Session("MainXML") = Nothing
                If Request.QueryString("id") = -1 Then
                    strmsg = "Record Added Successfully"
                Else
                    strmsg = "Record Updated Successfully"
                End If
                Response.Redirect("MSUP_MAILGroup.aspx?id=" & groupid & "&msg=" & strmsg)
            Else
                lblError.Text = ErrorNode.SelectSingleNode("Error").Attributes("Description").InnerText
            End If
        End Try
    End Sub
#Region "Fill Office "
    Private Sub FillOffice()
        Try
            Dim objXmlListOfficeOutput As New XmlDocument
            objbzOffice = New AAMS.bizMaster.bzAOffice
            Dim ObjNodeStatus As XmlNode
            Dim ds As New DataSet
            Dim strErrorStatus As String
            '*******************List function of bzCountry is called to populate the DropdownList ***************************
            objXmlListOfficeOutput = objbzOffice.List()
            '******************* End List function of bzCountry  ***************************
            '*********************** Error Status is Checked  *******************************************
            ObjNodeStatus = objXmlListOfficeOutput.DocumentElement.SelectSingleNode("Errors")
            strErrorStatus = Trim(ObjNodeStatus.Attributes("Status").InnerText)
            If UCase(strErrorStatus) = "TRUE" Then
                lblError.Text = ObjNodeStatus.SelectSingleNode("Error").Attributes("Description").InnerText
                Exit Sub
            End If
            '*********************** End Error Status Checked  *******************************************

            ds.ReadXml(New System.IO.StringReader(objXmlListOfficeOutput.InnerXml))
            ddlOffice.DataSource = ds.Tables("AOFFICE")
            ddlOffice.DataTextField = ds.Tables("AOFFICE").Columns("Aoffice").ColumnName
            ddlOffice.DataValueField = ds.Tables("AOFFICE").Columns("Aoffice").ColumnName
            ddlOffice.DataBind()
            ddlOffice.Items.Insert(0, New ListItem("Select One", "0"))
            'ddlOffice.SelectedIndex = 0

        Catch Exec As XmlException
            lblError.Text = Exec.Message
        Catch Exec As Exception
            lblError.Text = Exec.Message
        Finally
            objbzOffice = Nothing
        End Try
    End Sub
#End Region
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        If Request.QueryString("id") = -1 Then
            Response.Redirect("MSUP_MAILGroup.aspx?id=-1")
        Else
            Response.Redirect("MSUP_MAILGroup.aspx?id=" & Request.QueryString("id"))
        End If
    End Sub
    Private Sub deleteEmail(ByVal str As String)
        Dim objXml1, MainXML1, ListXML1 As New XmlDocument
        Dim XmlFrag As XmlDocumentFragment
        Dim node1, node2, node3, node4, node5, node6, node7, Xmlnode1, Xmlnode2 As XmlNode
        Dim nodelist As XmlNodeList
        Dim id1 As Integer
        ' Dim strxml As String
        Try
            MainXML1 = CType(Session("MainXML"), XmlDocument)
            ListXML1 = CType(Session("ListXML"), XmlDocument)
            'strxml = CType(MainXML1.OuterXml, String)
            'objXml1.LoadXml(strxml)
            node7 = MainXML1.DocumentElement.SelectSingleNode("AOFFICE_DETAILS/EMPLOYEE_DETAIL[@EMAIL='" & str & "']")
            If node7 Is Nothing Then
                node2 = ListXML1.DocumentElement.SelectSingleNode("EMAIL_DETAILS[@EMAIL='" & str & "']")
                ' ListXML1.DocumentElement.RemoveChild(node2)
                'Session("ListXML") = ListXML1
            Else
                node1 = MainXML1.DocumentElement.SelectSingleNode("AOFFICE_DETAILS/EMPLOYEE_DETAIL[@EMAIL='" & str & "']").ParentNode
                node3 = node1.SelectSingleNode("EMPLOYEE_DETAIL[@EMAIL='" & str & "']")
                id1 = node1.Attributes("ID").InnerText

                node2 = MainXML1.DocumentElement.SelectSingleNode("AOFFICE_DETAILS[@ID='" & id1 & "']")
                If node2 Is Nothing Then ' check parent node exist or not?
                    XmlFrag = MainXML1.CreateDocumentFragment()
                    XmlFrag.InnerXml = "<AOFFICE_DETAILS ID='' AOFFICE=''><EMPLOYEE_DETAIL EMPLOYEEID='' EMPLOYEE_NAME='' EMAIL=''/></AOFFICE_DETAILS>"
                    XmlFrag.SelectSingleNode("AOFFICE_DETAILS").Attributes("ID").InnerText = id1
                    XmlFrag.SelectSingleNode("AOFFICE_DETAILS").Attributes("AOFFICE").InnerText = node1.Attributes("AOFFICE").InnerText
                    XmlFrag.SelectSingleNode("AOFFICE_DETAILS/EMPLOYEE_DETAIL").Attributes("EMPLOYEEID").InnerText = node3.Attributes("EMPLOYEEID").InnerText
                    XmlFrag.SelectSingleNode("AOFFICE_DETAILS/EMPLOYEE_DETAIL").Attributes("EMPLOYEE_NAME").InnerText = node3.Attributes("EMPLOYEE_NAME").InnerText
                    XmlFrag.SelectSingleNode("AOFFICE_DETAILS/EMPLOYEE_DETAIL").Attributes("EMAIL").InnerText = node3.Attributes("EMAIL").InnerText
                    MainXML1.DocumentElement.AppendChild(XmlFrag)
                    node6 = ListXML1.DocumentElement.SelectSingleNode("EMAIL_DETAILS[@EMAIL='" & str & "']")
                    ListXML1.DocumentElement.RemoveChild(node6)
                Else
                    Xmlnode1 = MainXML1.DocumentElement.SelectSingleNode("AOFFICE_DETAILS/EMPLOYEE_DETAIL[@EMAIL='" & str & "']")
                    Xmlnode2 = MainXML1.DocumentElement.SelectSingleNode("AOFFICE_DETAILS/EMPLOYEE_DETAIL[@EMAIL='" & str & "']").ParentNode
                    nodelist = Xmlnode2.SelectNodes("EMPLOYEE_DETAIL")

                    If nodelist.Count > 1 Then
                        MainXML1.DocumentElement.SelectSingleNode("AOFFICE_DETAILS[@ID='" & id1 & "']").RemoveChild(node3)
                    Else
                        MainXML1.DocumentElement.RemoveChild(Xmlnode2)
                    End If
                    'MainXML1.DocumentElement.SelectSingleNode("AOFFICE_DETAILS[@ID='" & id1 & "']").RemoveChild(node3)
                   
                End If
                Session("MainXML") = MainXML1
                Session("ListXML") = ListXML1
            End If
            bindTreeview()
            bindlistbox()
        Catch ex As Exception
        Finally
            objXml1 = Nothing
            MainXML1 = Nothing
            ListXML1 = Nothing
            node1 = Nothing
            node2 = Nothing
            node3 = Nothing
            node4 = Nothing
            node5 = Nothing
            node6 = Nothing
            XmlFrag = Nothing

        End Try

    End Sub
End Class
