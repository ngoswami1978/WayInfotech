
Partial Class ISP_ISPUP_IPPool
    Inherits System.Web.UI.Page
#Region "Global Declaration Section"
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Shared counter As Integer = 0
    Dim objEn As New EncyrptDeCyrpt
    Dim imgDown As New Image
    Dim imgUp As New Image
    'Developed by Mukund
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = Request.Url

            btnAdd.Attributes.Add("onclick", "return validateManageIP();")
            lblError.Text = String.Empty

            btnAdd.Attributes.Add("onclick", "return validateManageIP();")
            btnNew.Attributes.Add("onclick", "return NewIPPool();")
            '***************************************************************************************
            '***************************************************************************************
            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgDown.ImageUrl = "~/Images/Sortdown.gif"
            '***************************************************************************************

            '***************************************************************************************

            objeAAMS.ExpirePageCache()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If



            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Manage IPPool']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Manage IPPool']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                    End If

                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                        btnSave.Enabled = False
                    End If

                    If Request.QueryString("Action") IsNot Nothing Then
                        If Request.QueryString("Action").Trim = "U" Then
                            If strBuilder(2) = "0" Then
                                btnSave.Enabled = False
                            End If
                        End If
                    End If


                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If


            If hdDele.Value.Trim <> "" Then
                DeleteRecords(hdDele.Value.Trim)
                hdDele.Value = ""
            End If
            If hdEdit.Value.Trim <> "" Then
                displayUpdateData(hdEdit.Value.Trim)
                btnAdd.Text = "Update"
                'hdEdit.Value = ""
            Else
                btnAdd.Text = "Add"
            End If


            If Not Page.IsPostBack Then
                'Sorting 
                ViewState("SortName") = "IPAddress"
                ViewState("Desc") = "TRUE"
                'End of Sorting

                bindDropdown()
                If Request.QueryString("Action") IsNot Nothing Then
                    If Request.QueryString("Action").Trim = "U" Then
                        btnAdd.Text = "Update"
                        viewRecords()
                    End If
                    If Request.QueryString("Action").Trim = "UM" Then
                        lblError.Text = objeAAMSMessage.messUpdate
                        viewRecords()
                    End If
                End If
                ''Code For  Updating of AgencyProduct Product Details  


                'If (Not Request.QueryString("Action") = Nothing) Then
                '    If (Request.QueryString("Action") = "U") Then
                '        If (Not Request.QueryString("TempRowno") = Nothing) Then
                '            ' ViewIPPoolGroup method  called for binding the controls  
                '            displayUpdateData(Request.QueryString("TempRowno"))
                '            btnAdd.Text = "Update"
                '        End If

                '    End If
                'End If
            End If

            

            'If hdProviderName.Value.Trim <> "" Then
            '    drpProvider.SelectedItem.Text = hdProviderName.Value
            'End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    
    Private Sub viewRecords()
        Dim objInputXml, objOutputXml, objOutputXml2 As New XmlDocument
        Dim objbzIPPool As New AAMS.bizISP.bzIPPoolDefinition
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objIAppendXml, objOAppendXml As New XmlDocument
        Dim objIXml As New XmlDocument
        Try

            If hdFinalXml.Value.Trim Is Nothing Or hdFinalXml.Value.Trim = "" Then
                objInputXml.LoadXml("<IS_VIEW_PROVIDER_INPUT><ProviderID/></IS_VIEW_PROVIDER_INPUT>")

                If Request.QueryString("IPAddressID") IsNot Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("ProviderID").InnerXml = objEn.Decrypt(Request.QueryString("IPAddressID").Trim)
                Else
                    lblError.Text = "Invalid Data for Update"
                    Exit Sub

                End If

                'Here Back end Method Call
                objOutputXml = objbzIPPool.View(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    'Dim objAtt As XmlAttribute
                    'objAtt = objOutputXml.CreateAttribute("ROWID")

                    'Dim objAttAct As XmlAttribute
                    'objAttAct = objOutputXml.CreateAttribute("Action")

                    'Dim counter As Int16 = 0
                    'For Each xnod As XmlNode In objOutputXml.DocumentElement.SelectNodes("DETAILS")
                    '    counter = counter + 1

                    '    objAtt.Value = counter.ToString
                    '    xnod.Attributes.Append(objAtt)

                    '    xnod.Attributes.Append(objAttAct)
                    '    xnod.Attributes("Action").Value = "I"

                    'Next
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)

                    Dim strrColName As String = ViewState("SortName").ToString

                    If ViewState("Desc") = "FALSE" Then
                        strrColName &= " "
                        strrColName &= "ASC"
                    End If
                    If ViewState("Desc") = "TRUE" Then
                        strrColName &= " "
                        strrColName &= "DESC"
                    End If

                    ds.Tables("DETAILS").DefaultView.Sort = strrColName

                    grdvIPPool.DataSource = ds.Tables("DETAILS").DefaultView
                    grdvIPPool.DataBind()

                    Dim intcol As Integer = GetSortColumnIndex(grdvIPPool)

                    If ViewState("Desc") = "FALSE" Then
                        grdvIPPool.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                    End If

                    If ViewState("Desc") = "TRUE" Then
                        grdvIPPool.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                    End If

                    'Modified Code for Editing

                    If Request.QueryString("IPAddressIDOrig") IsNot Nothing Then
                        Dim displayNode As XmlNode = objOutputXml.DocumentElement.SelectSingleNode("DETAILS[@IPAddressID='" & objEn.Decrypt(Request.QueryString("IPAddressIDOrig").Trim) & "']")

                        Dim li As New ListItem
                        li = drpProvider.Items.FindByValue(displayNode.Attributes("ProviderID").Value.Trim() & "|" & displayNode.Attributes("Reserved").Value.Trim())

                        If li IsNot Nothing Then
                            drpProvider.SelectedValue = li.Value.Trim
                        End If

                        drpPOP.SelectedValue = displayNode.Attributes("PopID").Value
                        txtAccNumber.Text = displayNode.Attributes("AceNumber").Value
                        txtIPAddress.Text = displayNode.Attributes("IPAddress").Value
                        txtNoTerminal.Text = displayNode.Attributes("NumberOfTerminal").Value
                        txtRemarks.Text = displayNode.Attributes("Remarks").Value
                        txtRouterIP.Text = displayNode.Attributes("RouterIP").Value
                        txtSubNtMask.Text = displayNode.Attributes("SubnetMask").Value
                        TempEdit.Value = displayNode.Attributes("RowId").Value
                    End If

                    'End of Modification


                    hdFinalXml.Value = objOutputXml.OuterXml

                Else
                    If Request.QueryString("Action") IsNot Nothing Then
                        If Request.QueryString("Action").Trim <> "UM" Then
                            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                        End If
                    End If

                    grdvIPPool.DataSource = Nothing
                    grdvIPPool.DataBind()
                    hdFinalXml.Value = Nothing
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            ds = Nothing
        End Try
    End Sub
    Private Sub displayUpdateData(ByVal str As String)
        Try

            Dim objxmldoc, objxmlDocTemp As New XmlDocument
            Dim displayNode As XmlNode

            objxmlDocTemp.LoadXml("<IS_VIEW_PROVIDER_OUTPUT><DETAILS RowId='' Action='' IPAddressID='' IPAddress='' SubnetMask='' RouterIP='' NumberOfTerminal='' AceNumber='' Remarks='' ProviderID='' Reserved='' PopID='' Lcode=''/><Errors Status='FALSE'><Error Code='' Description=''/></Errors></IS_VIEW_PROVIDER_OUTPUT>")

            If hdFinalXml.Value.Trim IsNot Nothing Then
                objxmldoc.LoadXml(hdFinalXml.Value)

                'Else
                '    objxmldoc.LoadXml("<IS_VIEW_PROVIDER_OUTPUT><DETAILS RowId='' Action='' IPAddressID='' IPAddress='' SubnetMask='' RouterIP='' NumberOfTerminal='' AceNumber='' Remarks='' ProviderID='' PopID='' Lcode=''/><Errors Status='FALSE'><Error Code='' Description=''/></Errors></IS_VIEW_PROVIDER_OUTPUT>")
                '    counter = 0
            End If

            displayNode = objxmldoc.DocumentElement.SelectSingleNode("DETAILS[@RowId='" & str & "']")

            'Dim objList As ListItem
            'objList = drpProvider.Items.FindByValue(

            ' objList = drpProvider.Items.FindByValue(displayNode.Attributes("ProviderID").Value

            Dim li As New ListItem
            li = drpProvider.Items.FindByValue(displayNode.Attributes("ProviderID").Value.Trim() & "|" & displayNode.Attributes("Reserved").Value.Trim())

            If li IsNot Nothing Then
                drpProvider.SelectedValue = li.Value.Trim
            End If

            ' drpProvider.SelectedValue = displayNode.Attributes("ProviderID").Value
            drpPOP.SelectedValue = displayNode.Attributes("PopID").Value
            txtAccNumber.Text = displayNode.Attributes("AceNumber").Value
            txtIPAddress.Text = displayNode.Attributes("IPAddress").Value
            txtNoTerminal.Text = displayNode.Attributes("NumberOfTerminal").Value
            txtRemarks.Text = displayNode.Attributes("Remarks").Value
            txtRouterIP.Text = displayNode.Attributes("RouterIP").Value
            txtSubNtMask.Text = displayNode.Attributes("SubnetMask").Value

            ' hdFinalXml.Value = objxmldoc.OuterXml
            TempEdit.Value = hdEdit.Value
            hdEdit.Value = ""
            viewIPinGrid(objxmldoc)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
       
    End Sub
    Private Sub DeleteRecords(ByVal str As String)
        Try
            Dim objxml As New XmlDocument
            If hdFinalXml.Value <> "" Then
                objxml.LoadXml(hdFinalXml.Value)
                Dim xNode As XmlNode
                xNode = objxml.DocumentElement.SelectSingleNode("DETAILS[@RowId='" & hdDele.Value.Trim & "']")
                If xNode IsNot Nothing Then
                    If xNode.Attributes("Action").Value.Trim = "U" Then
                        objxml.DocumentElement.SelectSingleNode("DETAILS[@RowId='" & hdDele.Value.Trim & "']").Attributes("Action").Value = "D"
                    Else
                        objxml.DocumentElement.RemoveChild(xNode)
                    End If
                    'lblError.Text = objeAAMSMessage.messDelete
                End If
                
                hdDele.Value = ""

                hdFinalXml.Value = objxml.OuterXml
                viewIPinGrid(objxml)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub bindDropdown()
        Try
            objeAAMS.BindDropDown(drpProvider, "IPPROVIDER_NOT", True, 1)
            objeAAMS.BindDropDown(drpPOP, "IPPOP", True, 3)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.OriginalString)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub viewIPinGrid(ByVal xmldoc As XmlDocument)
        Try
            Dim objxmlDisplay As New XmlDocument
            '<DETAILS RowId='' Action='' IPAddressID='' IPAddress='' SubnetMask='' RouterIP='' NumberOfTerminal='' AceNumber='' Remarks='' ProviderID='' PopID='' Lcode=''/>
            objxmlDisplay.LoadXml("<IS_VIEW_PROVIDER_OUTPUT><DETAILS RowId='' Action='' IPAddressID='' IPAddress='' SubnetMask='' RouterIP='' NumberOfTerminal='' AceNumber='' Remarks='' ProviderID='' Reserved='' PopID='' Lcode=''/><Errors Status='FALSE'><Error Code='' Description=''/></Errors></IS_VIEW_PROVIDER_OUTPUT>")
            For Each xNode As XmlNode In xmldoc.DocumentElement.SelectNodes("DETAILS")
                If xNode.Attributes("Action").Value <> "D" Then
                    objxmlDisplay.DocumentElement.AppendChild(objxmlDisplay.ImportNode(xNode, True))
                End If
            Next

            objxmlDisplay.DocumentElement.RemoveChild(objxmlDisplay.DocumentElement.FirstChild)


            If objxmlDisplay.DocumentElement.SelectNodes("DETAILS").Count >= 1 Then

                Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                If objxmlDisplay.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objxmlDisplay)
                    ds.ReadXml(objXmlReader)

                    Dim strrColName As String = ViewState("SortName").ToString

                    If ViewState("Desc") = "FALSE" Then
                        strrColName &= " "
                        strrColName &= "ASC"
                    End If
                    If ViewState("Desc") = "TRUE" Then
                        strrColName &= " "
                        strrColName &= "DESC"
                    End If

                    ds.Tables("DETAILS").DefaultView.Sort = strrColName

                    grdvIPPool.DataSource = ds.Tables("DETAILS").DefaultView
                    grdvIPPool.DataBind()

                    Dim intcol As Integer = GetSortColumnIndex(grdvIPPool)

                    If ViewState("Desc") = "FALSE" Then
                        grdvIPPool.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                    End If

                    If ViewState("Desc") = "TRUE" Then
                        grdvIPPool.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                    End If

                Else
                    grdvIPPool.DataSource = Nothing
                    grdvIPPool.DataBind()
                    lblError.Text = objxmlDisplay.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            Else

                grdvIPPool.DataSource = Nothing
                grdvIPPool.DataBind()

                drpPOP.SelectedIndex = 0
                txtAccNumber.Text = ""
                txtIPAddress.Text = ""
                txtNoTerminal.Text = ""
                txtRemarks.Text = ""
                txtRouterIP.Text = ""
                txtSubNtMask.Text = ""

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Function GetSortColumnIndex(ByVal grd As GridView) As Int16
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Return grd.Columns.IndexOf(field)
            End If
        Next
        Return 0
    End Function

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim objxmldoc, objxmlDocTemp As New XmlDocument
            Dim objxmlNode As XmlNode
            Dim blnkFlag As Integer = 0

            ' hdProviderName.Value = drpProvider.SelectedItem.Text

            objxmlDocTemp.LoadXml("<IS_VIEW_PROVIDER_OUTPUT><DETAILS RowId='' Action='' IPAddressID='' IPAddress='' SubnetMask='' RouterIP='' NumberOfTerminal='' AceNumber='' Remarks='' ProviderID='' Reserved=''  PopID='' Lcode=''/><Errors Status='FALSE'><Error Code='' Description=''/></Errors></IS_VIEW_PROVIDER_OUTPUT>")

            Dim objDuplicateNode As XmlNode


            If TempEdit.Value.Trim <> "" Then
                'Update Section
                Dim str As String = TempEdit.Value
                If hdFinalXml.Value.Trim IsNot Nothing Then
                    objxmldoc.LoadXml(hdFinalXml.Value)
                End If
                'objDuplicateNode = objxmldoc.DocumentElement.SelectSingleNode("DETAILS[@IPAddress='" & txtIPAddress.Text.Trim & "']")

                'If objDuplicateNode IsNot Nothing Then
                '    If objDuplicateNode.Attributes("Action").Value.Trim <> "I" Or objDuplicateNode.Attributes("Action").Value.Trim <> "IT" Then
                '        lblError.Text = "IP Address Cann't be Duplicate"
                '        Exit Sub
                '    End If
                'End If

                'If drpProvider.SelectedValue.Trim().Split("|").GetValue(1).ToString.ToUpper = "TRUE" Then
                '    If drpPOP.SelectedIndex <> 0 Then

                '    Else

                '        hdEdit.Value = TempEdit.Value
                '        btnAdd.Text = "Update"
                '        lblError.Text = "POP is Mandatory for this Provider"
                '        Exit Sub
                '    End If
                'End If

                objxmlNode = objxmldoc.DocumentElement.SelectSingleNode("DETAILS[@RowId='" & TempEdit.Value.Trim & "']") 'objxmldoc.DocumentElement.SelectSingleNode("DETAILS")

                objxmlNode.Attributes("RowId").Value = TempEdit.Value
                objxmlNode.Attributes("Action").Value = objxmldoc.DocumentElement.SelectSingleNode("DETAILS[@RowId='" & TempEdit.Value.Trim & "']").Attributes("Action").Value.Trim
                objxmlNode.Attributes("IPAddressID").Value = objxmldoc.DocumentElement.SelectSingleNode("DETAILS[@RowId='" & TempEdit.Value.Trim & "']").Attributes("IPAddressID").Value.Trim 'Request.QueryString("IPAddressID").Trim
                objxmlNode.Attributes("IPAddress").Value = txtIPAddress.Text.Trim()
                objxmlNode.Attributes("SubnetMask").Value = txtSubNtMask.Text.Trim()
                objxmlNode.Attributes("RouterIP").Value = txtRouterIP.Text.Trim()
                objxmlNode.Attributes("NumberOfTerminal").Value = txtNoTerminal.Text.Trim()
                objxmlNode.Attributes("AceNumber").Value = txtAccNumber.Text.Trim
                objxmlNode.Attributes("Remarks").Value = txtRemarks.Text.Trim
                objxmlNode.Attributes("ProviderID").Value = drpProvider.SelectedValue.Trim.Split("|").GetValue(0)
                objxmlNode.Attributes("PopID").Value = drpPOP.SelectedValue.Trim
                objxmlNode.Attributes("Lcode").Value = objxmldoc.DocumentElement.SelectSingleNode("DETAILS[@RowId='" & TempEdit.Value.Trim & "']").Attributes("Lcode").Value.Trim
                'objxmldoc.DocumentElement.ReplaceChild(objxmlNode, objxmldoc.DocumentElement.SelectSingleNode("DETAILS[@RowId='" & TempEdit.Value.Trim & "']"))

                'Dim objOldNode As XmlNode = objxmldoc.DocumentElement.SelectSingleNode("DETAILS[@RowId='" & TempEdit.Value.Trim & "']")
                'Dim objNewNode As XmlNode = objxmldoc.DocumentElement.SelectSingleNode("DETAILS")

                'objNewNode.Attributes("RowId").Value = TempEdit.Value
                'objNewNode.Attributes("Action").Value = objxmldoc.DocumentElement.SelectSingleNode("DETAILS[@RowId='" & TempEdit.Value.Trim & "']").Attributes("Action").Value.Trim
                'objNewNode.Attributes("IPAddressID").Value = objxmldoc.DocumentElement.SelectSingleNode("DETAILS[@RowId='" & TempEdit.Value.Trim & "']").Attributes("IPAddressID").Value.Trim 'Request.QueryString("IPAddressID").Trim
                'objNewNode.Attributes("IPAddress").Value = txtIPAddress.Text.Trim()
                'objNewNode.Attributes("SubnetMask").Value = txtSubNtMask.Text.Trim()
                'objNewNode.Attributes("RouterIP").Value = txtRouterIP.Text.Trim()
                'objNewNode.Attributes("NumberOfTerminal").Value = txtNoTerminal.Text.Trim()
                'objNewNode.Attributes("AceNumber").Value = txtAccNumber.Text.Trim
                'objNewNode.Attributes("Remarks").Value = txtRemarks.Text.Trim
                'objNewNode.Attributes("ProviderID").Value = drpProvider.SelectedValue.Trim.Split("|").GetValue(0)
                'objNewNode.Attributes("PopID").Value = drpPOP.SelectedValue.Trim
                'objNewNode.Attributes("Lcode").Value = ""

                'objxmldoc.DocumentElement.RemoveChild(objOldNode)

                'objxmldoc.DocumentElement.AppendChild(objxmldoc.ImportNode(objNewNode, True))
                'lblError.Text = objeAAMSMessage.messUpdate
                hdFinalXml.Value = objxmldoc.OuterXml

                TempEdit.Value = ""
                hdEdit.Value = ""
                viewIPinGrid(objxmldoc)
                'End of Update Section
            Else
                hdEdit.Value = ""
                'Add Section 
                objxmlDocTemp.LoadXml("<IS_VIEW_PROVIDER_OUTPUT><DETAILS RowId='' Action='' IPAddressID='' IPAddress='' SubnetMask='' RouterIP='' NumberOfTerminal='' AceNumber='' Remarks='' ProviderID='' Reserved='' PopID='' Lcode=''/><Errors Status='FALSE'><Error Code='' Description=''/></Errors></IS_VIEW_PROVIDER_OUTPUT>")

                If hdFinalXml.Value.Trim <> "" Then
                    objxmldoc.LoadXml(hdFinalXml.Value)


                    'If drpProvider.SelectedValue.Trim().Split("|").GetValue(1).ToString.ToUpper = "TRUE" Then
                    '    If drpPOP.SelectedIndex = 0 Then
                    '        lblError.Text = "POP is Mandatory for this Provider"
                    '        Exit Sub
                    '    End If
                    'End If


                    'Duplicacy Node Check
                    objDuplicateNode = objxmldoc.DocumentElement.SelectSingleNode("DETAILS[@IPAddress='" & txtIPAddress.Text.Trim & "']")

                    If objDuplicateNode IsNot Nothing Then
                        If objDuplicateNode.Attributes("Action").Value.Trim = "I" Or objDuplicateNode.Attributes("Action").Value.Trim = "IT" Then
                            lblError.Text = "IP Address Cann't be Duplicate"
                            Exit Sub
                        End If
                    End If


                Else
                    objxmldoc.LoadXml("<IS_VIEW_PROVIDER_OUTPUT><DETAILS RowId='' Action='' IPAddressID='' IPAddress='' SubnetMask='' RouterIP='' NumberOfTerminal='' AceNumber='' Remarks='' ProviderID='' Reserved=''  PopID='' Lcode=''/><Errors Status='FALSE'><Error Code='' Description=''/></Errors></IS_VIEW_PROVIDER_OUTPUT>")
                    counter = 0
                    blnkFlag = 1
                End If
                For Each xnodes As XmlNode In objxmldoc.DocumentElement.SelectNodes("DETAILS")
                    counter = Val(xnodes.Attributes("RowId").Value.Trim & "")
                Next

                counter = counter + 1
                objxmlNode = objxmlDocTemp.DocumentElement.SelectSingleNode("DETAILS")
                objxmlNode.Attributes("RowId").Value = counter
                objxmlNode.Attributes("Action").Value = "IT"
                objxmlNode.Attributes("IPAddressID").Value = ""
                objxmlNode.Attributes("IPAddress").Value = txtIPAddress.Text.Trim()
                objxmlNode.Attributes("SubnetMask").Value = txtSubNtMask.Text.Trim()
                objxmlNode.Attributes("RouterIP").Value = txtRouterIP.Text.Trim()
                objxmlNode.Attributes("NumberOfTerminal").Value = txtNoTerminal.Text.Trim()
                objxmlNode.Attributes("AceNumber").Value = txtAccNumber.Text.Trim
                objxmlNode.Attributes("Remarks").Value = txtRemarks.Text.Trim
                objxmlNode.Attributes("ProviderID").Value = drpProvider.SelectedValue.Trim.Split("|").GetValue(0)
                objxmlNode.Attributes("PopID").Value = drpPOP.SelectedValue.Trim
                objxmlNode.Attributes("Lcode").Value = ""
                objxmldoc.DocumentElement.AppendChild(objxmldoc.ImportNode(objxmlNode, True))
                If blnkFlag = 1 Then
                    objxmldoc.DocumentElement.RemoveChild(objxmldoc.DocumentElement.FirstChild)
                    blnkFlag = 0
                End If
                hdFinalXml.Value = objxmldoc.OuterXml

                'lblError.Text = objeAAMSMessage.messInsert

                viewIPinGrid(objxmldoc)
                'Add Section 
            End If

            ' drpProvider.SelectedIndex = 0
            drpPOP.SelectedIndex = 0
            txtAccNumber.Text = ""
            txtIPAddress.Text = ""
            txtNoTerminal.Text = ""
            txtRemarks.Text = ""
            txtRouterIP.Text = ""
            txtSubNtMask.Text = ""

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvIPPool_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvIPPool.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If

            Dim hdstrProviderID As HiddenField

            Dim linkEdit As LinkButton
            linkEdit = e.Row.FindControl("linkEdit")
            Dim btnDelete As LinkButton
            btnDelete = e.Row.FindControl("btnDelete")

            hdstrProviderID = e.Row.FindControl("hdROWID")

            'Dim objSecurityXml As New XmlDocument
            'objSecurityXml.LoadXml(Session("Security"))
            'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Manage IPPool']").Count <> 0 Then
            '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Manage IPPool']").Attributes("Value").Value)
            '        If strBuilder(3) = "0" Then
            '            btnDelete.Enabled = False
            '        Else
            '            btnDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdstrProviderID.Value & ");")
            '        End If
            '        If strBuilder(2) = "0" Then
            '            linkEdit.Enabled = False
            '        Else
            '            linkEdit.Attributes.Add("onclick", "return EditFunction(" & hdstrProviderID.Value & ");")
            '        End If
            '    Else
            '    End If
            'Else
            '    btnDelete.Enabled = True
            '    linkEdit.Enabled = True
            linkEdit.Attributes.Add("onclick", "return EditFunction(" & hdstrProviderID.Value & ");")
            btnDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdstrProviderID.Value & ");")
            ' End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim xmlInput, xmlOutput, xmlTemp As New XmlDocument
            Dim objSave As New AAMS.bizISP.bzIPPoolDefinition

            xmlInput.LoadXml("<IS_UPDATE_PROVIDER_INPUT> <DETAILS Action='' IPAddressID='' IPAddress='' SubnetMask='' RouterIP='' NumberOfTerminal='' AceNumber='' Remarks='' ProviderID='' PopID='' Lcode='' /> </IS_UPDATE_PROVIDER_INPUT>")
            
            If hdFinalXml.Value.Trim = "" Then
                lblError.Text = "Add at least one IP Address"
                Exit Sub
            End If

            xmlTemp.LoadXml(hdFinalXml.Value)
            'Dim xAtt As XmlAttribute

            If xmlTemp.DocumentElement.SelectNodes("DETAILS").Count = 0 Then
                lblError.Text = "Add at least one IP Address"
                Exit Sub
            End If

            For Each xNode As XmlNode In xmlTemp.DocumentElement.SelectNodes("DETAILS")
                If xNode.Attributes("Action").Value.Trim = "I" Then
                    xNode.Attributes("Action").Value = "U"
                End If

                If xNode.Attributes("Action").Value.Trim = "IT" Then
                    xNode.Attributes("Action").Value = "I"
                End If

                xNode.Attributes.RemoveNamedItem("RowId")

                xmlInput.DocumentElement.AppendChild(xmlInput.ImportNode(xNode, True))
            Next
            xmlInput.DocumentElement.RemoveChild(xmlInput.DocumentElement.FirstChild)

            If xmlInput.DocumentElement.SelectNodes("DETAILS").Count = 0 Then
                lblError.Text = "Add at least one IP Address"
                Exit Sub
            End If
            xmlOutput = objSave.Update(xmlInput)

            If xmlOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messUpdate
                Response.Redirect("ISPUP_IPPool.aspx?Action=UM&IPAddressID=" + objEn.Encrypt(xmlOutput.DocumentElement.SelectSingleNode("DETAILS").Attributes("ProviderID").Value.Trim), False)
                hdFinalXml.Value = ""
                viewRecords()
            Else
                'grdvIPPool.DataSource = Nothing
                'grdvIPPool.DataBind()
                lblError.Text = xmlOutput.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvIPPool_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvIPPool.Sorting
        Try
            Dim objxml As New XmlDocument
            e.SortDirection = SortDirection.Ascending
            If ViewState("Desc") = "TRUE" Then
                e.SortDirection = SortDirection.Descending
                ViewState("Desc") = "FALSE"
            Else
                e.SortDirection = SortDirection.Ascending
                ViewState("Desc") = "TRUE"
            End If
            ViewState("SortName") = e.SortExpression.ToString
            'DisplayProductListSort(e.SortExpression.ToString)
            objxml.LoadXml(hdFinalXml.Value)
            viewIPinGrid(objxml)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
       
    End Sub
#Region "Code for Filter "
    'Code Added by Abhishek
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    'Code Added by Abhishek
#End Region
End Class
