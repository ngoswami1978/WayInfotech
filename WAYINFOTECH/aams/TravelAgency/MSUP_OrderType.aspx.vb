Imports System.Xml
Imports System.Data
Partial Class Setup_MSUP_OrderType
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt

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
            ' This code is used for Expiration of Page From Cache
          '  objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.

            '  ddlOrderTypeChallan.Attributes.Add("onblur", "return validate();")



            If Not Page.IsPostBack Then
                ' Load Data For Editing Mode
                objeAAMS.BindDropDown(ddlOrderTypeCat, "OrderTypeCategory", True)
                objeAAMS.BindDropDown(drpLstNewConn, "NewConnectivity", True)
                objeAAMS.BindDropDown(drplstOldConn, "OldConnectivity", True)
                objeAAMS.BindDropDown(ddlCorporateCode, "CorporateCode", True)
               
            End If          
            If Not Page.IsPostBack Then
                ' Load Data For Editing Mode
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Then
                   ViewOrderType()
                End If
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                    lblError.Text = objeAAMSMessage.messInsert
                    ViewOrderType()
                End If
            End If

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Type']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Type']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("~/NoRights.aspx", False)
                    Exit Sub
                End If
                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    'btnSave.Enabled = False 
                End If
                If Not Request.QueryString("Action") Is Nothing Then
                    If strBuilder(2) = "0" Then
                        btnSave.Enabled = False

                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If (Not Request.QueryString("Action") = Nothing) Then
                Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                Dim objbzOrderType As New AAMS.bizTravelAgency.bzOrderType
                objInputXml.LoadXml("<MS_UPDATEORDERTYPE_INPUT><ORDERTYPE ACTION=''  ORDERTYPEID='' ForPCType=''  ORDER_TYPE_NAME='' IsDeleted=''  IsISPOrder='' IsChallanOrder='' IsTrainingOrder='' IshardwareOrder='' OrderTypeCategoryID='' NewConnectivity='' OldConnectivity='' OrderTrackingRequired='' TimeRequired='' CANCELLATION='' NEW_ORDER='' DESCRIPTION=''/><RowID></RowID></MS_UPDATEORDERTYPE_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("ForPCType").Value = ddlPcType.Text
                objInputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("ORDER_TYPE_NAME").Value = txtOrderType.Text

                objInputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsISPOrder").Value = IIf(ddlOrderTypeISP.SelectedValue = "Yes", "1", "0")
                objInputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsChallanOrder").Value = IIf(ddlOrderTypeChallan.SelectedValue = "Yes", "1", "0")
                objInputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsTrainingOrder").Value = IIf(ddlOrderTypeTraning.SelectedValue = "Yes", "1", "0")
                objInputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IshardwareOrder").Value = IIf(ddlOrderTypeHardware.SelectedValue = "Yes", "1", "0")

                objInputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("OrderTypeCategoryID").Value = ddlOrderTypeCat.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("NewConnectivity").Value = drpLstNewConn.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("OldConnectivity").Value = drplstOldConn.SelectedValue

                objInputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("OrderTrackingRequired").Value = IIf(chkOrderTrackingRequired.Checked = True, "1", "0")

                objInputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("TimeRequired").Value = txtDaysReq.Text
                objInputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("CANCELLATION").Value = IIf(RdlNewCancel.SelectedValue = "1", "1", "0")
                objInputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("NEW_ORDER").Value = IIf(RdlNewCancel.SelectedValue = "0", "1", "0")
                objInputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("DESCRIPTION").Value = txtDescription.Text

                'If chkOrderEnabled.Checked Then
                '    objInputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsDeleted").Value = "1"
                'Else
                '    objInputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsDeleted").Value = "0"
                'End If

               If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
                    objInputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("ORDERTYPEID").InnerText = objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString().ToUpper())
                    objInputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("ACTION").InnerText = "U"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("ORDERTYPEID").InnerText = ""
                    objInputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("ACTION").InnerText = "I"
                End If
                '<ORDERTYPE ACTION="I" ORDERTYPEID="" ORDER_TYPE_NAME="aa" IsDeleted="" IsISPOrder="1"
                ' OrderTypeCategoryID="1" NewConnectivity="ACT" OldConnectivity="DEA" OrderTrackingRequired="1" 
                'TimeRequired="1" CANCELLATION="0" NEW_ORDER="1" DESCRIPTION="vvvvvvvv" /> 

                If Not Session("CorporateCode") Is Nothing Then
                    Dim objCorporateCode As New XmlDocument
                    Dim objNode As XmlNode
                    Dim objNode1 As XmlNode
                    Dim objNodeList As XmlNodeList
                    Dim intCount As Integer = 0
                    objCorporateCode.LoadXml(Session("CorporateCode"))
                    Dim I As Int16 = objCorporateCode.DocumentElement.SelectNodes("QUALIFIERS").Count
                    If I > 0 Then
                        '************** Fill table with data **************
                        objNodeList = objCorporateCode.DocumentElement.SelectNodes("QUALIFIERS")
                        For Each objNode In objNodeList
                            If objNode.Attributes("RowID").InnerText <> "" Then
                                If intCount = 0 Then
                                    objInputXml.DocumentElement.SelectSingleNode("RowID").InnerText = objNode.Attributes("RowID").InnerText
                                    intCount = 1
                                Else
                                    objNode1 = objInputXml.DocumentElement.SelectSingleNode("RowID").CloneNode(True)
                                    objNode1.InnerText = objNode.Attributes("RowID").InnerText
                                    objInputXml.DocumentElement.AppendChild(objNode1)
                                End If
                            End If
                        Next
                    End If
                Else
                    Dim node1 As XmlNode
                    node1 = objInputXml.SelectSingleNode("MS_UPDATEORDERTYPE_INPUT/RowID")
                    objInputXml.DocumentElement.RemoveChild(node1)
                End If

                

                objOutputXml = objbzOrderType.Update(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
                        lblError.Text = objeAAMSMessage.messUpdate ' "Record updated successfully."
                        ' Session("CorporateCode") = Nothing
                    Else
                        Response.Redirect("MSUP_OrderType.aspx?Action=US|" & objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("ORDERTYPEID").Value))
                        'lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."
                        Session("CorporateCode") = Nothing
                    End If
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Session("CorporateCode") = Nothing
        Response.Redirect("MSUP_OrderType.aspx?Action=I|")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click


        If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
            lblError.Text = ""
            ViewOrderType()
        Else
            Session("CorporateCode") = Nothing
            ddlOrderTypeCat.SelectedIndex = 0
            txtOrderType.Text = ""
            txtDescription.Text = ""
            txtDaysReq.Text = ""
            '            chkIspOrdr.Checked = False
            '            chkOrderTrackingRequired.Checked = False
            RdlNewCancel.SelectedValue = "0"
            ddlPcType.SelectedIndex = 0
            drpLstNewConn.SelectedIndex = 0
            drplstOldConn.SelectedIndex = 0
            ddlCorporateCode.SelectedIndex = 0
            gvCorporateCode.DataSource = Nothing
            gvCorporateCode.DataBind()
        End If

       
    End Sub
    Sub ViewOrderType()
        Try

            Dim objInputXml, objOutputXml As New XmlDocument
            Dim dt As New DataTable
            'Dim objXmlReader As XmlNodeReader

            Dim objbzOrderType As New AAMS.bizTravelAgency.bzOrderType
            objInputXml.LoadXml("<MS_VIEWORDERTYPE_INPUT><ORDERTYPEID></ORDERTYPEID></MS_VIEWORDERTYPE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText = objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString())
            'Here Back end Method Call
            objOutputXml = objbzOrderType.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                ddlPcType.Text = objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("ForPCType").Value
                txtOrderType.Text = objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("ORDER_TYPE_NAME").Value

                If objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsISPOrder").Value.Trim().ToUpper() = "TRUE" Then
                    ddlOrderTypeISP.SelectedIndex = 0
                Else
                    ddlOrderTypeISP.SelectedIndex = 1
                End If
                If objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsChallanOrder").Value.Trim().ToUpper() = "TRUE" Then
                    ddlOrderTypeChallan.SelectedIndex = 0
                Else
                    ddlOrderTypeChallan.SelectedIndex = 1
                End If
                If objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsTrainingOrder").Value.Trim().ToUpper() = "TRUE" Then
                    ddlOrderTypeTraning.SelectedIndex = 0
                Else
                    ddlOrderTypeTraning.SelectedIndex = 1
                End If
                If objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IshardwareOrder").Value.Trim().ToUpper() = "TRUE" Then
                    ddlOrderTypeHardware.SelectedIndex = 0
                Else
                    ddlOrderTypeHardware.SelectedIndex = 1
                End If

                ddlOrderTypeCat.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("OrderTypeCategoryID").Value
                drpLstNewConn.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("NewConnectivity").Value
                drplstOldConn.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("OldConnectivity").Value

                If objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("OrderTrackingRequired").Value.Trim().ToUpper() = "TRUE" Then
                    chkOrderTrackingRequired.Checked = True 'objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("OrderTrackingRequired").Value
                Else
                    chkOrderTrackingRequired.Checked = False
                End If

                txtDaysReq.Text = objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("TimeRequired").Value
                RdlNewCancel.SelectedValue = IIf(objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("NEW_ORDER").Value = "0", "1", "0")
                txtDescription.Text = objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("DESCRIPTION").Value


                'If objOutputXml.DocumentElement.SelectSingleNode("ORDERTYPE").Attributes("IsDeleted").Value.Trim().ToUpper() = "TRUE" Then
                '    chkOrderEnabled.Checked = True
                'Else
                '    chkOrderEnabled.Checked = False
                'End If


                Dim objNode As XmlNode
                Dim objNodeList As XmlNodeList

                Dim I As Int16 = objOutputXml.DocumentElement.SelectNodes("CorporateCode").Count
                If I > 0 Then
                    '************** Fill table with data **************
                    objNodeList = objOutputXml.DocumentElement.SelectNodes("CorporateCode")
                    Dim dcRowId As New DataColumn("RowID", GetType(String))
                    Dim dcCode As New DataColumn("Code", GetType(String))
                    Dim dcQualifier As New DataColumn("Qualifier", GetType(String))
                    Dim dcDescription As New DataColumn("Description", GetType(String))
                    dt.Columns.Add(dcRowId)
                    dt.Columns.Add(dcCode)
                    dt.Columns.Add(dcQualifier)
                    dt.Columns.Add(dcDescription)
                    For Each objNode In objNodeList

                        Dim drCorporateCode As DataRow
                        drCorporateCode = dt.NewRow
                        drCorporateCode("RowID") = objNode.Attributes("RowID").InnerText
                        drCorporateCode("Code") = objNode.Attributes("Code").InnerText
                        drCorporateCode("Qualifier") = objNode.Attributes("Qualifier").InnerText
                        drCorporateCode("Description") = objNode.Attributes("Description").InnerText
                        dt.Rows.Add(drCorporateCode)

                        Dim objCorporateCodeXml As New XmlDocument
                        Dim objNode1 As XmlNode
                        Dim objNodeClone As XmlNode
                        Dim strError As String = ""
                        Dim strCorporateCodeXml As String = "<Input> <QUALIFIERS RowID='' Code='' Qualifier='' Description='' /></Input>"
                        If Session("CorporateCode") Is Nothing Then
                            objCorporateCodeXml.LoadXml(strCorporateCodeXml)
                        Else
                            objCorporateCodeXml.LoadXml(Session("CorporateCode"))
                        End If
                        objNode1 = objCorporateCodeXml.DocumentElement.SelectSingleNode("QUALIFIERS")
                        objNodeClone = objNode1.CloneNode(True)
                        With objNodeClone
                            'For Each objNodeClone In objNode
                            objNodeClone.Attributes("RowID").InnerText = objNode.Attributes("RowID").InnerText
                            objNodeClone.Attributes("Code").InnerText = objNode.Attributes("Code").InnerText
                            objNodeClone.Attributes("Qualifier").InnerText = objNode.Attributes("Qualifier").InnerText
                            objNodeClone.Attributes("Description").InnerText = objNode.Attributes("Description").InnerText
                        End With
                        objCorporateCodeXml.DocumentElement.AppendChild(objNodeClone)
                        Session("CorporateCode") = objCorporateCodeXml.InnerXml
                    Next
                    If dt.Rows.Count > 0 Then
                        gvCorporateCode.DataSource = dt
                        gvCorporateCode.DataBind()
                    End If
                End If
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#Region "Code To Add and Delete Corporate Code "

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If ddlCorporateCode.SelectedValue <> "" Then


            Dim objCorporateCodeXml As New XmlDocument
            Dim objNode As XmlNode
            Dim objNodechk As XmlNode
            Dim objNodeClone As XmlNode
            Dim strError As String = ""
            Dim strCorporateCodeXml As String = "<Input> <QUALIFIERS RowID='' Code='' Qualifier='' Description='' /></Input>"
            Try
                If Session("CorporateCode") Is Nothing Then
                    objCorporateCodeXml.LoadXml(strCorporateCodeXml)
                Else
                    objCorporateCodeXml.LoadXml(Session("CorporateCode"))
                    objNode = objCorporateCodeXml.DocumentElement.SelectSingleNode("QUALIFIERS[(@RowID='" & ddlCorporateCode.SelectedValue.Split("|").GetValue(0) & "' )]")
                    If Not objNode Is Nothing Then
                        lblError.Text = "Entry already exist."
                        Exit Sub
                    Else
                        lblError.Text = ""
                    End If
                End If
                objNode = objCorporateCodeXml.DocumentElement.SelectSingleNode("QUALIFIERS")
                objNodeClone = objNode.CloneNode(True)
                With objNodeClone
                    'For Each objNodeClone In objNode
                    objNodeClone.Attributes("RowID").InnerText = ddlCorporateCode.SelectedValue.Split("|").GetValue(0)
                    objNodeClone.Attributes("Code").InnerText = ddlCorporateCode.Items(ddlCorporateCode.SelectedIndex).Text.Trim
                    objNodeClone.Attributes("Qualifier").InnerText = ddlCorporateCode.SelectedValue.Split("|").GetValue(1)
                    objNodeClone.Attributes("Description").InnerText = ddlCorporateCode.SelectedValue.Split("|").GetValue(2)
                End With
                objCorporateCodeXml.DocumentElement.AppendChild(objNodeClone)
                Session("CorporateCode") = objCorporateCodeXml.InnerXml
                BindGrid()
            Catch ex As Exception
                lblError.Text = ex.Message
            Finally
                objCorporateCodeXml = Nothing
                objNode = Nothing
                objNodechk = Nothing
                objNodeClone = Nothing
            End Try
        Else
            lblError.Text = ""
        End If
    End Sub
    Private Sub BindGrid()
        Dim objCorporateCode As XmlDocument
        Dim objNode As XmlNode
        Dim objNodeList As XmlNodeList

        Try
            objCorporateCode = New XmlDocument
            Dim dt As New DataTable
            Dim dcRowId As New DataColumn("RowId", GetType(String))
            Dim dcCode As New DataColumn("Code", GetType(String))
            Dim dcQualifier As New DataColumn("Qualifier", GetType(String))
            Dim dcDescription As New DataColumn("Description", GetType(String))
            dt.Columns.Add(dcRowId)
            dt.Columns.Add(dcCode)
            dt.Columns.Add(dcQualifier)
            dt.Columns.Add(dcDescription)
            If Not Session("CorporateCode") Is Nothing Then
                objCorporateCode.LoadXml(Session("CorporateCode"))
                Dim I As Int16 = objCorporateCode.DocumentElement.SelectNodes("QUALIFIERS").Count
                If I > 0 Then
                    '************** Fill table with data **************
                    objNodeList = objCorporateCode.DocumentElement.SelectNodes("QUALIFIERS")
                    Dim drCorporateCode As DataRow
                    For Each objNode In objNodeList
                        drCorporateCode = dt.NewRow
                        drCorporateCode("RowId") = objNode.Attributes("RowID").InnerText
                        drCorporateCode("Code") = objNode.Attributes("Code").InnerText
                        drCorporateCode("Qualifier") = objNode.Attributes("Qualifier").InnerText
                        drCorporateCode("Description") = objNode.Attributes("Description").InnerText
                        dt.Rows.Add(drCorporateCode)
                        If drCorporateCode("Code") = "" Or drCorporateCode("RowId") = "" Then
                            dt.Rows.Remove(drCorporateCode)
                        End If
                    Next
                End If
                gvCorporateCode.DataSource = dt
                gvCorporateCode.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            objCorporateCode = Nothing
            objNode = Nothing
            objNodeList = Nothing
        End Try

    End Sub
    Private Sub BindGrid(ByVal sno As String)

        Dim objCorporateCode As XmlDocument
        Dim objNode As XmlNode
        Dim objNodeList As XmlNodeList
        Dim objnewCorporateCode As XmlDocument
        Dim objdelNode As XmlNode
        Dim intCount As Integer = 1

        Try
            Dim dt As New DataTable
            Dim dcRowId As New DataColumn("RowId", GetType(String))
            Dim dcCode As New DataColumn("Code", GetType(String))
            Dim dcQualifier As New DataColumn("Qualifier", GetType(String))
            Dim dcDescription As New DataColumn("Description", GetType(String))
            dt.Columns.Add(dcRowId)
            dt.Columns.Add(dcCode)
            dt.Columns.Add(dcQualifier)
            dt.Columns.Add(dcDescription)

            objCorporateCode = New XmlDocument
            objnewCorporateCode = New XmlDocument
            If Not Session("CorporateCode") Is Nothing Then
                objCorporateCode.LoadXml(Session("CorporateCode"))
                objnewCorporateCode.LoadXml(Session("CorporateCode"))

                objdelNode = objnewCorporateCode.DocumentElement.SelectSingleNode("QUALIFIERS[@RowID='" & sno & "']")

                objnewCorporateCode.DocumentElement.RemoveChild(objdelNode)

                Dim I As Int16 = objnewCorporateCode.DocumentElement.SelectNodes("QUALIFIERS").Count
                If I > 0 Then
                    '************** Fill table with data **************
                    objNodeList = objnewCorporateCode.DocumentElement.SelectNodes("QUALIFIERS")
                    Dim drCorporateCode As DataRow
                    For Each objNode In objNodeList
                        drCorporateCode = dt.NewRow
                        drCorporateCode = dt.NewRow
                        drCorporateCode("RowId") = objNode.Attributes("RowID").InnerText
                        drCorporateCode("Code") = objNode.Attributes("Code").InnerText
                        drCorporateCode("Qualifier") = objNode.Attributes("Qualifier").InnerText
                        drCorporateCode("Description") = objNode.Attributes("Description").InnerText
                        dt.Rows.Add(drCorporateCode)
                        If drCorporateCode("Code") = "" Or drCorporateCode("RowId") = "" Then
                            dt.Rows.Remove(drCorporateCode)
                        End If
                    Next
                End If

                gvCorporateCode.DataSource = dt
                gvCorporateCode.DataBind()
                Session("CorporateCode") = objnewCorporateCode.InnerXml
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            objCorporateCode = Nothing
            objNode = Nothing
            objNodeList = Nothing
            intCount = Nothing
        End Try

    End Sub
    Protected Sub gvCorporateCode_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvCorporateCode.RowCommand
        If e.CommandName = "DeleteX" Then
            Dim sno As String = e.CommandArgument.ToString
            BindGrid(sno)
        End If
    End Sub
    Protected Sub gvCorporateCode_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles gvCorporateCode.RowDeleted

    End Sub
    Protected Sub gvCorporateCode_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvCorporateCode.RowDeleting

    End Sub
#End Region

    
    
End Class
