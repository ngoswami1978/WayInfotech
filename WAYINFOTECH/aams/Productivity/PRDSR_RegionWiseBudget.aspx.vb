Imports System.Data
Imports System.Xml
Partial Class Productivity_PRDSR_RegionWiseBudget
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim TempXml As New XmlDocument
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim i, j As Integer
            Dim s, s1, s2 As String
            s = ""
            s1 = ""
            s2 = ""
            Session("PageName") = Request.Url.ToString()
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Region Wise Budget']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Region Wise Budget']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                    If strBuilder(1) = "0" Then
                        btnSave.Enabled = False
                    End If
                End If
            Else
                objeAAMS.SecurityCheck(31)
            End If

            hdYear.Value = drpYear.SelectedValue
            hdRegion.Value = drpRegion.SelectedValue

            's1 = drpRegion.SelectedValue
            's2 = drpYear.SelectedValue
            's = s1 + "|" + s2
            btnHistory.Attributes.Add("onclick", "return ShowPTRHistory('" & s & "');")

            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(drpRegion, "REGION1", True)
                btnSearch.Attributes.Add("onclick", "return MandatoryFunction();")
                s1 = drpRegion.SelectedValue
                s2 = drpYear.SelectedValue
                s = s1 + "|" + s2
                btnHistory.Attributes.Add("onclick", "return ShowPTRHistory('" & s & "');")
                For j = 2000 To 2020
                    drpYear.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                    i += 1
                Next
                drpYear.SelectedValue = DateTime.Now.Year
                If (Request.QueryString("Msg") = "A") Then
                    lblError.Text = objeAAMSMessage.messInsert
                End If
                If (Request.QueryString("Msg") = "U") Then
                    lblError.Text = objeAAMSMessage.messUpdate
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            RegionWiseSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub RegionWiseSearch()
        Try
            Dim str As String = "<REGIONBUDGETTARGET REGIONID='' MONTH='' YEAR='' TARGET='' USERID=''/>"
            'Dim str2 As String = "<PR_SEARCH_REGIONBUDGETTARGET_OUTPUT><REGIONBUDGETTARGET REGIONID='' MONTH='' YEAR='' TARGET='' USERID=''/><Errors Status=''><Error Code='' Description=''/></Errors></PR_SEARCH_REGIONBUDGETTARGET_OUTPUT>"
            Dim objIn As New XmlDocument
            Dim objOut As New XmlDocument
            objIn.LoadXml("<PR_SEARCH_REGIONBUDGETTARGET_INPUT><REGIONID></REGIONID><YEAR></YEAR></PR_SEARCH_REGIONBUDGETTARGET_INPUT>")
            Dim objFragment As XmlDocumentFragment
            Dim objXmlReader As XmlNodeReader
            Dim objbzRegion As New AAMS.bizProductivity.bzRegionBudgetTarget
            Dim ds As New DataSet
            Dim UserId As String
            'objOut.LoadXml("<PR_SEARCH_REGIONBUDGETTARGET_INPUT><REGIONID></REGIONID><YEAR></YEAR></PR_SEARCH_REGIONBUDGETTARGET_INPUT>")
            objIn.DocumentElement.SelectSingleNode("REGIONID").InnerText = drpRegion.SelectedValue
            objIn.DocumentElement.SelectSingleNode("YEAR").InnerText = drpYear.SelectedValue
            objOut = objbzRegion.Search(objIn)
            objIn.LoadXml(str)
            'objOut.LoadXml(str2)
            Dim objEmpXml As New XmlDocument
            objEmpXml.LoadXml(Session("Security"))
            UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
            Dim objNodetemp As XmlNode
            Dim objNode As XmlNode
            Dim objNodeClone As XmlNode
            Dim objNodeList As XmlNodeList
            objFragment = objOut.CreateDocumentFragment()
            objFragment.InnerXml = str
            objOut.DocumentElement.AppendChild(objFragment)
            objNodeList = objOut.DocumentElement.SelectNodes("REGIONBUDGETTARGET")
            objNode = objOut.DocumentElement.SelectSingleNode("REGIONBUDGETTARGET[@REGIONID='']")
            objOut.DocumentElement.RemoveChild(objOut.DocumentElement.SelectSingleNode("REGIONBUDGETTARGET[@REGIONID='']"))
            objNodeClone = objNode.CloneNode(True)
            Dim i As Integer
            Dim flag As Boolean
            Dim objtempnode As XmlNode
            flag = False
            For i = 1 To 12
                flag = False
                For Each objNodetemp In objNodeList
                    If objNodetemp.Attributes("MONTH").Value = i.ToString Then
                        objNodeClone.Attributes("REGIONID").Value = objNodetemp.Attributes("REGIONID").Value
                        objNodeClone.Attributes("MONTH").Value = objNodetemp.Attributes("MONTH").Value
                        objNodeClone.Attributes("YEAR").Value = objNodetemp.Attributes("YEAR").Value
                        objNodeClone.Attributes("TARGET").Value = objNodetemp.Attributes("TARGET").Value
                        objNodeClone.Attributes("USERID").Value = objNodetemp.Attributes("USERID").Value
                        objOut.DocumentElement.AppendChild(objNodeClone)
                        objNodeClone = objNode.CloneNode(True)
                        flag = True
                    End If
                Next
                If flag = False Then
                    objNodeClone.Attributes("REGIONID").Value = "0"
                    objNodeClone.Attributes("MONTH").Value = i
                    objNodeClone.Attributes("YEAR").Value = drpYear.SelectedValue
                    objNodeClone.Attributes("TARGET").Value = "0"
                    objNodeClone.Attributes("USERID").Value = "UserId"
                    objOut.DocumentElement.AppendChild(objNodeClone)
                    objNodeClone = objNode.CloneNode(True)
                End If
            Next
            For Each objNodetemp In objNodeList
                objOut.DocumentElement.RemoveChild(objOut.DocumentElement.SelectSingleNode("REGIONBUDGETTARGET"))
            Next
            TempXml.LoadXml("<PR_SEARCH_REGIONBUDGETTARGET_OUTPUT><REGIONBUDGETTARGET REGIONID='' MONTH='1' YEAR='' TARGET='' USERID=''/><REGIONBUDGETTARGET REGIONID='' MONTH='2' YEAR='' TARGET='' USERID=''/><REGIONBUDGETTARGET REGIONID='' MONTH='3' YEAR='' TARGET='' USERID=''/><REGIONBUDGETTARGET REGIONID='' MONTH='4' YEAR='' TARGET='' USERID=''/><REGIONBUDGETTARGET REGIONID='' MONTH='5' YEAR='' TARGET='' USERID=''/><REGIONBUDGETTARGET REGIONID='' MONTH='6' YEAR='' TARGET='' USERID=''/><REGIONBUDGETTARGET REGIONID='' MONTH='7' YEAR='' TARGET='' USERID=''/><REGIONBUDGETTARGET REGIONID='' MONTH='8' YEAR='' TARGET='' USERID=''/><REGIONBUDGETTARGET REGIONID='' MONTH='9' YEAR='' TARGET='' USERID=''/><REGIONBUDGETTARGET REGIONID='' MONTH='10' YEAR='' TARGET='' USERID=''/><REGIONBUDGETTARGET REGIONID='' MONTH='11' YEAR='' TARGET='' USERID=''/><REGIONBUDGETTARGET REGIONID='' MONTH='12' YEAR='' TARGET='' USERID=''/><Errors Status=''><Error Code='' Description=''/></Errors></PR_SEARCH_REGIONBUDGETTARGET_OUTPUT>")
            If objOut.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOut)
                ds.ReadXml(objXmlReader)
                gvRegionWise.DataSource = ds.Tables("REGIONBUDGETTARGET")
                gvRegionWise.DataBind()
            Else
                For Each objtempnode In tempXml.DocumentElement.SelectNodes("REGIONBUDGETTARGET")
                    objtempnode.Attributes("YEAR").Value = drpYear.SelectedValue.Trim()
                Next
                objXmlReader = New XmlNodeReader(TempXml)
                ds.ReadXml(objXmlReader)
                gvRegionWise.DataSource = ds.Tables("REGIONBUDGETTARGET")
                gvRegionWise.DataBind()
                lblError.Text = objOut.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("PRDSR_RegionWiseBudget.aspx?Action=I")
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If gvRegionWise.Rows.Count = 0 Then
                lblError.Text = "Please Search valid Region."
                Exit Sub
            End If
            If drpRegion.SelectedValue = "" Then
                lblError.Text = "Region Name is Mandatory."
                Exit Sub
            End If
            Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
            Dim ds As New DataSet
            Dim Rowno As Integer
            Dim objParentNode As XmlNode
            Dim objChildNodeClone As XmlNode
            Dim strAoffice As String = ""
            Dim UserId As String
            Dim objbzSecurityRegion As New AAMS.bizProductivity.bzRegionBudgetTarget
            Dim objEmpXml As New XmlDocument
            objEmpXml.LoadXml(Session("Security"))
            UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
            objInputXml.LoadXml("<PR_UPDATE_REGIONBUDGETTARGET_INPUT><REGIONBUDGETTARGET Action='' 	REGIONID='' YEAR='' MONTH='' TARGET='' USERID=''/></PR_UPDATE_REGIONBUDGETTARGET_INPUT>")
            objParentNode = objInputXml.DocumentElement.SelectSingleNode("REGIONBUDGETTARGET")
            'With objInputXml.DocumentElement.SelectSingleNode("REGIONBUDGETTARGET")
            '    .Attributes("USERID").Value = "24"
            'End With
            With objInputXml.DocumentElement.SelectSingleNode("REGIONBUDGETTARGET")
                .Attributes("USERID").Value = UserId
            End With
            objChildNodeClone = objParentNode.CloneNode(True)
            For Rowno = 0 To gvRegionWise.Rows.Count - 1
                objChildNodeClone.Attributes("Action").Value() = "I"
                objChildNodeClone.Attributes("REGIONID").Value() = drpRegion.SelectedValue
                objChildNodeClone.Attributes("YEAR").Value() = drpYear.SelectedValue
                objChildNodeClone.Attributes("MONTH").Value = CType(gvRegionWise.Rows(Rowno).FindControl("hdMonth"), HiddenField).Value
                'objChildNodeClone.Attributes("MONTH").Value = CType(gvRegionWise.Rows(Rowno).FindControl("txtmonth"), Label).Text
                objChildNodeClone.Attributes("TARGET").Value = CType(gvRegionWise.Rows(Rowno).FindControl("txtTarget"), TextBox).Text
                objInputXml.DocumentElement.AppendChild(objChildNodeClone)
                objChildNodeClone = objParentNode.CloneNode(True)
            Next Rowno
            objInputXml.DocumentElement.RemoveChild(objInputXml.DocumentElement.SelectSingleNode("REGIONBUDGETTARGET[@REGIONID='']"))
            objOutputXml = objbzSecurityRegion.Update(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messInsert ' "Added Successfully."
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub gvRegionWise_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvRegionWise.RowDataBound
        Dim objSecurityXml As New XmlDocument
        Dim linkEdit As New LinkButton
        Dim str As String = ""
        Dim yr As String
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim lnkHistory As LinkButton
            linkEdit = CType(e.Row.FindControl("lnkEdit"), LinkButton)
            lnkHistory = e.Row.FindControl("lnkHistory")
            Dim txt As New TextBox
            Dim hdmon As HiddenField
            txt = CType(e.Row.FindControl("txtTarget"), TextBox)
            hdmon = CType(e.Row.FindControl("hdMonth"), HiddenField)
            yr = drpYear.SelectedValue
           
            'CType(e.Row.Cells(2).Controls(1), TextBox).Attributes.Add("onblur", "validateResult('" + CType(e.Row.Cells(2).Controls(1), TextBox).ClientID + "')")
            Dim strBuilder As New StringBuilder

            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Region Wise Budget']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Region Wise Budget']").Attributes("Value").Value)
                    If strBuilder(2) = "0" Then
                        linkEdit.Enabled = False

                    Else

                        linkEdit.Attributes.Add("onclick", "return ChangeControlStatus('" + txt.ClientID + "','" + hdmon.Value + "')")
                    End If
                Else
                    linkEdit.Enabled = False
                End If
            Else
                linkEdit.Enabled = True
                linkEdit.Attributes.Add("onclick", "return ChangeControlStatus('" + txt.ClientID + "','" + hdmon.Value + "')")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
End Class
