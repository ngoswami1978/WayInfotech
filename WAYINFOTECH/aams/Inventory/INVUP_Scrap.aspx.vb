
Partial Class Inventory_INVUP_Scrap
    Inherits System.Web.UI.Page
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
#Region "Global Variable Declaration"
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
#End Region

#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl

        Try
            'If Request.QueryString("TabType") IsNot Nothing Then
            '    hdTabType.Value = Request.QueryString("TabType")
            'End If
            'btnSave.Enabled = True

           
            If Not Page.IsPostBack Then
                ' objeAAMS.BindDropDown(ddlGodown, "GODOWN", True)
                FillGodown()
                objeAAMS.BindDropDown(ddlProduct, "EQUIPMENTCODEWITH_MAINTAIN_BALANCE_BY", True)
                btnAdd.Attributes.Add("onClick", "return ValidateProduct();")
                btnSave.Attributes.Add("onClick", "return ValidateForm();")
                If Not Request.QueryString("TrashID") Is Nothing Then
                    hdScrapID.Value = objED.Decrypt(Request.QueryString("TrashID"))
                End If

                If hdScrapID.Value <> "" Then
                    ViewRecords() ' View scrap details.
                Else
                    ' Disabling buttons in case of new.
                    btnExecute.Enabled = False
                    ' btnPrint.Enabled = False
                End If
            End If
            '   Checking Security.
            CheckSecurity()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub
#End Region

#Region "FillGodown::Populating Godown DropDown"
    Sub FillGodown()

        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))


        ' This code binds godown on the basis of logged user
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            Try
                ddlGodown.Items.Clear()
                Dim objbzGodown As New AAMS.bizInventory.bzGodown
                Dim objInputXml As New XmlDocument
                Dim objOutputXml As New XmlDocument
                Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                objInputXml.LoadXml("<INV_LISTGODOWN_INPUT><AOFFICE/><CITYNAME/><REGION/></INV_LISTGODOWN_INPUT>")
                'If  ChallanRegionWiseGodown value is "1 or 2" then fill Region id else cityname
                If Session("Security") IsNot Nothing Then
                    Dim strRegionId As String = objeAAMS.SecurityRegionID(Session("Security"))
                    objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = strRegionId

                    '    'Commented on 27 may 08
                    '    'If  ChallanRegionWiseGodown value isnot "1 or 2" then return -100
                    '    If strRegionId = "-100" Then
                    '        'if challan category is customer and agency is selected from popup than city is passed (but in this case we can pass aoffice too)
                    '        'else aoffice will be passed
                    '        If ddlChallanCategory.SelectedValue = "1" And txtCity.Text <> "" Then
                    '            objInputXml.DocumentElement.SelectSingleNode("CITYNAME").InnerText = txtCity.Text
                    '        Else
                    '            objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = objeAAMS.AOffice(Session("Security"))
                    '        End If

                    '    Else
                    '        objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = strRegionId
                    '    End If
                    'Else
                    '    objInputXml.DocumentElement.SelectSingleNode("CITYNAME").InnerText = txtCity.Text
                End If
                'End Comment
                objOutputXml = objbzGodown.ListGodownAoffice(objInputXml)
                'objOutputXml.LoadXml("<INV_AGENCYPENGINDORDER_OUTPUT><Errors Status='FALSE'><Error Code='' Description='' /> </Errors><AGENCYORDER ORDER_NUMBER='2008/2/366' ORDER_TYPE_NAME='vista Add Term(1A  P)' ORDER_STATUS_NAME='Pending' APPROVAL_DATE='' RECEIVED_DATE='2/15/2008' APC='2' /><AGENCYORDER ORDER_NUMBER='2008/2/367' ORDER_TYPE_NAME='vista Add Term(1A  P)' ORDER_STATUS_NAME='Pending' APPROVAL_DATE='' RECEIVED_DATE='2/15/2008' APC='2' /></INV_AGENCYPENGINDORDER_OUTPUT>")
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    ddlGodown.DataSource = ds.Tables("GODOWN").DefaultView
                    ddlGodown.DataTextField = "GODOWNNAME"
                    ddlGodown.DataValueField = "GODOWNID"
                    ddlGodown.DataBind()
                Else
                    If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value <> "No Record Found") Then
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            Finally
                ddlGodown.Items.Insert(0, New ListItem("Select One", ""))
            End Try
        Else
            objeAAMS.BindDropDown(ddlGodown, "GODOWN", True, 1)
        End If


    End Sub
#End Region

#Region "btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objInputXml, objTempInputXml, objOutputXml As New XmlDocument
        Dim objbzScrap As New AAMS.bizInventory.bzScrap
        Dim objDocFrag As XmlDocumentFragment
        Dim intCnt As Integer = 0


        Try
            '            <INV_UPDATE_SCRAP_INPUT>
            '	<SCRAP TrashID='' GodownID=''  LoggedDate=''  LoggedBy='' >
            '			<SCRAPDETAILS	ProductID='' Qty='' SerialNumber='' />    
            '	</SCRAP>		
            '</INV_UPDATE_SCRAP_INPUT>
            BindXml()
            ManageOrder()
            ' validating values in grid.
            intCnt = gvProduct.Rows.Count
            If intCnt <> 0 Then
                objInputXml.LoadXml("<INV_UPDATE_SCRAP_INPUT><SCRAP TrashID='' GodownID=''  LoggedDate=''  LoggedByID='' ></SCRAP></INV_UPDATE_SCRAP_INPUT>")
                objDocFrag = objInputXml.CreateDocumentFragment()
                objTempInputXml.LoadXml(hdProductList.Value)
                objDocFrag.InnerXml = objTempInputXml.DocumentElement.SelectSingleNode("SCRAP").InnerXml
                objInputXml.DocumentElement.SelectSingleNode("SCRAP").AppendChild(objDocFrag)

                Dim objNodeList As XmlNodeList
                objNodeList = objInputXml.DocumentElement.SelectNodes("SCRAP/SCRAPDETAILS[@ProductID='']")
                For Each objNode As XmlNode In objNodeList
                    objInputXml.DocumentElement.SelectSingleNode("SCRAP").RemoveChild(objNode)
                Next

                With objInputXml.DocumentElement.SelectSingleNode("SCRAP")
                    If hdScrapID.Value <> "" Then
                        .Attributes("TrashID").Value = hdScrapID.Value
                    End If
                    .Attributes("GodownID").Value = ddlGodown.SelectedValue
                    .Attributes("LoggedByID").Value = Session("LoginSession").ToString().Split("|")(0) 'txtLoggedBy.Text
                End With

                'Here Back end Method Call
                objOutputXml = objbzScrap.Update(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If hdScrapID.Value = "" Then
                        lblError.Text = objeAAMSMessage.messInsert
                        hdScrapID.Value = objOutputXml.DocumentElement.SelectSingleNode("SCRAP").Attributes("TrashID").Value
                        ' hdPageStatus.Value = "U"
                    Else
                        lblError.Text = objeAAMSMessage.messUpdate
                    End If
                    ddlProduct.SelectedIndex = 0
                    txtQuantity.Text = ""
                    ViewRecords()

                    '   Checking Security.
                    CheckSecurity()

                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    BindProduct()
                End If

            Else

                lblError.Text = "Add atleast one product for scrap."
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally

        End Try
    End Sub
#End Region

#Region "btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click"
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("INVUP_Scrap.aspx?Action=I")
    End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        ' Response.Redirect("INVUP_Scrap.aspx?Action=I")
        Try
            If hdScrapID.Value <> "" Then
                lblError.Text = ""
                ViewRecords()
            Else
                Response.Redirect("INVUP_Scrap.aspx?Action=I")
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
#End Region

#Region "btnExecute_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExecute.Click"
    Protected Sub btnExecute_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExecute.Click

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzScrap As New AAMS.bizInventory.bzScrap

        Try
            objInputXml.LoadXml("<INV_SCRAP_EXECUTE_INPUT><TrashID></TrashID><GodownID></GodownID><ProductId></ProductId></INV_SCRAP_EXECUTE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("TrashID").InnerText = hdScrapID.Value
            objInputXml.DocumentElement.SelectSingleNode("GodownID").InnerText = ddlGodown.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("ProductId").InnerText = "0"
            'Here Back end Method Call
            objOutputXml = objbzScrap.Execute(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                hdProductList.Value = objOutputXml.OuterXml
                lblError.Text = "Scrap executed successfully"
                ManageOrder()
                BindProduct()
                For Each ctrl As Control In Page.Controls
                    ChangeControlStatus(ctrl)
                Next
                '   ClientScript.RegisterHiddenField("hdPrintLabel", "0")
                '  ClientScript.RegisterClientScriptBlock(Me.GetType, "strChallanPage", "<script> if(confirm('Do You Want to print Serial Number on Labels?')==true){document.getElementById('hdPrintLabel').value='1';}else{document.getElementById('hdPrintLabel').value='0';}</script>")

                ' ClientScript.RegisterClientScriptInclude(Me.GetType, "asd", "../JavaScript/PrintLabel.js")
                ' If hdPrintLabel.Value = "1" Then
                'lblError.Text = "Label Print"
                'Else
                '   lblError.Text = "Not Print"
                'End If
            Else
                BindProduct()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally

        End Try

    End Sub
#End Region

#Region "btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click"
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim objOutputXml, objSearchNode As New XmlDocument

            Dim ds As New DataSet
            BindXml()  ' USed to fill serial number in xml from grid.
            If hdProductList.Value <> "" Then
                objOutputXml.LoadXml(hdProductList.Value)
            Else
                objOutputXml.LoadXml("<Root><SCRAP><SCRAPDETAILS	LineNumber='' ProductID='' ProductName='' Qty='' SerialNumber='' MAINTAIN_BALANCE_BY='' MAINTAIN_BALANCE=''  /></SCRAP></Root>")

            End If

            Dim objXmlNodeClone As XmlNode
            Dim cn As Integer = 0
            Try
                If ddlProduct.SelectedValue.Split("|").GetValue(1).ToString.ToUpper = "FALSE" And ddlProduct.SelectedValue.Split("|").GetValue(2).ToString.ToUpper = "FALSE" Then
                    cn = -1
                Else
                    cn = Convert.ToInt32(txtQuantity.Text)
                End If
            Catch ex As Exception
                cn = -1
            End Try

            If cn > 0 Or cn = -1 Then
                If ddlProduct.SelectedValue.Split("|").GetValue(1).ToString.ToUpper = "FALSE" And ddlProduct.SelectedValue.Split("|").GetValue(2).ToString.ToUpper = "FALSE" Then
                    cn = 1
                    Dim objNode As XmlNode = Nothing
                    objNode = objOutputXml.DocumentElement.SelectSingleNode("SCRAP/SCRAPDETAILS[@ProductID='" + ddlProduct.SelectedValue.Split("|").GetValue(0) + "']")
                    If objNode Is Nothing Then
                        objXmlNodeClone = objOutputXml.DocumentElement.SelectSingleNode("SCRAP/SCRAPDETAILS").CloneNode(True)
                        With objXmlNodeClone
                            .Attributes("ProductID").Value = ddlProduct.SelectedValue.Split("|").GetValue(0)
                            .Attributes("ProductName").Value = ddlProduct.SelectedItem.Text
                            .Attributes("Qty").Value = ""
                            .Attributes("MAINTAIN_BALANCE_BY").Value = ddlProduct.SelectedValue.Split("|").GetValue(1)
                            .Attributes("MAINTAIN_BALANCE").Value = ddlProduct.SelectedValue.Split("|").GetValue(2)
                            objOutputXml.DocumentElement.SelectSingleNode("SCRAP").AppendChild(objXmlNodeClone)
                        End With
                    End If
                Else
                    If ddlProduct.SelectedValue.Split("|").GetValue(1).ToString.ToUpper = "FALSE" And ddlProduct.SelectedValue.Split("|").GetValue(2).ToString.ToUpper = "TRUE" Then
                        Dim objNodeListTemp As XmlNodeList = objOutputXml.DocumentElement.SelectNodes("SCRAP/SCRAPDETAILS[@ProductID='" + ddlProduct.SelectedValue.Split("|").GetValue(0) + "']")
                        If objNodeListTemp.Count > cn Then
                            Dim iTemp As Integer = 0
                            For Each objNodeTemp As XmlNode In objNodeListTemp
                                If iTemp < objNodeListTemp.Count - cn Then
                                    objOutputXml.DocumentElement.SelectSingleNode("SCRAP").RemoveChild(objNodeTemp)
                                    iTemp = iTemp + 1
                                End If
                            Next
                        Else
                            For i As Integer = 0 To cn - 1 - objNodeListTemp.Count
                                objXmlNodeClone = objOutputXml.DocumentElement.SelectSingleNode("SCRAP/SCRAPDETAILS").CloneNode(True)
                                With objXmlNodeClone
                                    .Attributes("ProductID").Value = ddlProduct.SelectedValue.Split("|").GetValue(0)
                                    .Attributes("ProductName").Value = ddlProduct.SelectedItem.Text
                                    .Attributes("Qty").Value = txtQuantity.Text
                                    .Attributes("MAINTAIN_BALANCE_BY").Value = ddlProduct.SelectedValue.Split("|").GetValue(1)
                                    .Attributes("MAINTAIN_BALANCE").Value = ddlProduct.SelectedValue.Split("|").GetValue(2)
                                    objOutputXml.DocumentElement.SelectSingleNode("SCRAP").AppendChild(objXmlNodeClone)
                                End With
                            Next
                        End If
                    Else
                        Dim objNode As XmlNode
                        objNode = objOutputXml.DocumentElement.SelectSingleNode("SCRAP/SCRAPDETAILS[@ProductID='" + ddlProduct.SelectedValue.Split("|").GetValue(0) + "']")
                        If objNode IsNot Nothing Then
                            objNode.Attributes("Qty").Value = cn.ToString '(cn + Convert.ToInt32(objNode.Attributes("Qty").Value)).ToString
                        Else
                            objXmlNodeClone = objOutputXml.DocumentElement.SelectSingleNode("SCRAP/SCRAPDETAILS").CloneNode(True)
                            With objXmlNodeClone
                                .Attributes("ProductID").Value = ddlProduct.SelectedValue.Split("|").GetValue(0)
                                .Attributes("ProductName").Value = ddlProduct.SelectedItem.Text
                                .Attributes("Qty").Value = txtQuantity.Text
                                .Attributes("MAINTAIN_BALANCE_BY").Value = ddlProduct.SelectedValue.Split("|").GetValue(1)
                                .Attributes("MAINTAIN_BALANCE").Value = ddlProduct.SelectedValue.Split("|").GetValue(2)
                                objOutputXml.DocumentElement.SelectSingleNode("SCRAP").AppendChild(objXmlNodeClone)
                            End With
                        End If
                    End If
                End If
                hdProductList.Value = objOutputXml.OuterXml
                ManageOrder()
                BindProduct()
            Else
                BindProduct()
            End If
            





            'Dim objNode As XmlNode
            'objNode = objOutputXml.DocumentElement.SelectSingleNode("SCRAP/SCRAPDETAILS[@ProductID='" + ddlProduct.SelectedValue.Split("|").GetValue(0) + "']")
            'For i As Integer = 0 To cn - 1
            '    objXmlNodeClone = objOutputXml.DocumentElement.SelectSingleNode("SCRAP/SCRAPDETAILS").CloneNode(True)

            '    With objXmlNodeClone
            '        .Attributes("ProductID").Value = ddlProduct.SelectedValue.Split("|").GetValue(0)
            '        .Attributes("ProductName").Value = ddlProduct.SelectedItem.Text
            '        .Attributes("Qty").Value = txtQuantity.Text
            '        objOutputXml.DocumentElement.SelectSingleNode("SCRAP").AppendChild(objXmlNodeClone)

            '    End With
            'Next


           

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
        'If ddlProduct.SelectedValue.Split("|").GetValue(1).ToString.ToUpper = "FALSE" And ddlProduct.SelectedValue.Split("|").GetValue(2).ToString.ToUpper = "FALSE" Then
        '    txtQuantity.ReadOnly = True
        'Else
        '    txtQuantity.ReadOnly = False
        'End If

    End Sub
#End Region

#Region "gvProduct_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvProduct.RowCommand"
    Protected Sub gvProduct_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvProduct.RowCommand
        Dim strSno As String
        strSno = e.CommandArgument.ToString()
        Dim objOutputXml As New XmlDocument
        Dim objSearchNode As XmlNode
        objOutputXml.LoadXml(hdProductList.Value)
        objSearchNode = objOutputXml.DocumentElement.SelectSingleNode("SCRAP/SCRAPDETAILS[@LineNumber='" + strSno + "']")
        objOutputXml.DocumentElement.SelectSingleNode("SCRAP").RemoveChild(objSearchNode)
        hdProductList.Value = objOutputXml.OuterXml
        BindXml(1)
        ManageOrder()
        BindProduct()
    End Sub
#End Region

#Region "ManageOrder()Code to maintain sequence of xml as same as grid"
    Sub ManageOrder()
        Dim objOutputXml, objSearchNode As New XmlDocument
        Dim objtempXml As New XmlDocument
        Dim ar As New ArrayList
        Dim objXmlNodeClone As XmlNode
        If hdProductList.Value <> "" Then


            objOutputXml.LoadXml(hdProductList.Value)
            objtempXml.LoadXml("<Root><SCRAP><SCRAPDETAILS	LineNumber='' ProductID='' ProductName='' Qty='' SerialNumber='' MAINTAIN_BALANCE_BY='' MAINTAIN_BALANCE=''/></SCRAP></Root>")

            Dim objNodeList As XmlNodeList
            objNodeList = objOutputXml.DocumentElement.SelectNodes("SCRAP/SCRAPDETAILS[@ProductID='']")
            For Each objNode As XmlNode In objNodeList
                objOutputXml.DocumentElement.SelectSingleNode("SCRAP").RemoveChild(objNode)
            Next

            objNodeList = objOutputXml.DocumentElement.SelectNodes("SCRAP/SCRAPDETAILS")
            For Each objNode As XmlNode In objNodeList
                If ar.Contains(objNode.Attributes("ProductID").Value) Then
                Else
                    ar.Add(objNode.Attributes("ProductID").Value)
                End If
            Next
            Dim LineNumber As Integer = 1
            For i As Integer = 0 To ar.Count - 1
                objNodeList = objOutputXml.DocumentElement.SelectNodes("SCRAP/SCRAPDETAILS[@ProductID='" + ar(i) + "']")
                For Each objNode As XmlNode In objNodeList
                    objXmlNodeClone = objtempXml.DocumentElement.SelectSingleNode("SCRAP/SCRAPDETAILS").CloneNode(True)
                    With objXmlNodeClone
                        .Attributes("LineNumber").Value = LineNumber
                        .Attributes("ProductID").Value = objNode.Attributes("ProductID").Value
                        .Attributes("ProductName").Value = objNode.Attributes("ProductName").Value
                        .Attributes("Qty").Value = objNode.Attributes("Qty").Value
                        .Attributes("SerialNumber").Value = objNode.Attributes("SerialNumber").Value
                        .Attributes("MAINTAIN_BALANCE_BY").Value = objNode.Attributes("MAINTAIN_BALANCE_BY").Value
                        .Attributes("MAINTAIN_BALANCE").Value = objNode.Attributes("MAINTAIN_BALANCE").Value

                        objtempXml.DocumentElement.SelectSingleNode("SCRAP").AppendChild(objXmlNodeClone)
                        LineNumber = LineNumber + 1
                    End With
                Next
            Next

            objOutputXml.DocumentElement.SelectSingleNode("SCRAP").RemoveAll()

            Dim objDocFrag As XmlDocumentFragment

            objDocFrag = objOutputXml.CreateDocumentFragment()
            objDocFrag.InnerXml = objtempXml.DocumentElement.SelectSingleNode("SCRAP").InnerXml
            objOutputXml.DocumentElement.SelectSingleNode("SCRAP").AppendChild(objDocFrag)

            Dim objNodeList1 As XmlNodeList
            objNodeList1 = objOutputXml.DocumentElement.SelectNodes("SCRAP/SCRAPDETAILS")
            For Each objNode As XmlNode In objNodeList1
                'Dim objNodeList2 As XmlNodeList
                'objNodeList2 = objOutputXml.DocumentElement.SelectNodes("SCRAP/SCRAPDETAILS[@ProductID='" + objNode.Attributes("ProductID").Value + "']")
                'If objNodeList2.Count <> 0 Then
                '    objNode.Attributes("Qty").Value = objNodeList2.Count
                'End If
                Dim objNodeList2 As XmlNodeList
                objNodeList2 = objOutputXml.DocumentElement.SelectNodes("SCRAP/SCRAPDETAILS[@ProductID='" + objNode.Attributes("ProductID").Value + "']")
                If objNodeList2.Count = 1 Then
                    If objNodeList2.Item(0).Attributes("MAINTAIN_BALANCE_BY").Value.ToString.ToUpper = "FALSE" And objNodeList2.Item(0).Attributes("MAINTAIN_BALANCE").Value.ToString.ToUpper = "TRUE" Then
                        objNode.Attributes("Qty").Value = objNodeList2.Count
                    End If
                Else '
                    objNode.Attributes("Qty").Value = objNodeList2.Count
                End If
                If (objNodeList2.Item(0).Attributes("MAINTAIN_BALANCE_BY").Value.ToString.ToUpper = "FALSE" And objNodeList2.Item(0).Attributes("MAINTAIN_BALANCE").Value.ToString.ToUpper = "FALSE") Or objNodeList2.Item(0).Attributes("MAINTAIN_BALANCE_BY").Value.ToString.ToUpper = "" And objNodeList2.Item(0).Attributes("MAINTAIN_BALANCE").Value.ToString.ToUpper = "" Then
                    objNode.Attributes("Qty").Value = ""
                End If
            Next

            hdProductList.Value = objOutputXml.OuterXml
        End If
    End Sub
#End Region

#Region "BindProduct()Binding Product Grid With Data"
    Sub BindProduct()
        If hdProductList.Value <> "" Then
            Dim objOutputXml, objSearchNode As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            objOutputXml.LoadXml(hdProductList.Value)

            Dim objNodeList As XmlNodeList
            objNodeList = objOutputXml.DocumentElement.SelectNodes("SCRAP/SCRAPDETAILS[@ProductID='']")
            For Each objNode As XmlNode In objNodeList
                objOutputXml.DocumentElement.SelectSingleNode("SCRAP").RemoveChild(objNode)
            Next
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            If ds.Tables("SCRAPDETAILS") IsNot Nothing Then
                Dim dv As DataView
                dv = ds.Tables("SCRAPDETAILS").DefaultView
                gvProduct.DataSource = dv
                gvProduct.DataBind()
            Else
                gvProduct.DataSource = Nothing
                gvProduct.DataBind()
            End If
        End If
    End Sub
#End Region

#Region "BindXml(ByVal st As Integer)"
    Sub BindXml(ByVal st As Integer)
        Dim objOutputXml As New XmlDocument
        If gvProduct.Rows.Count > 0 Then
            Dim objNodeList As XmlNodeList
            Dim objNode As XmlNode
            objOutputXml.LoadXml(hdProductList.Value)

            objNodeList = objOutputXml.DocumentElement.SelectNodes("SCRAP/SCRAPDETAILS[@ProductID != '']")
            For i As Integer = 0 To objNodeList.Count - 1
                objNode = objNodeList(i)
                For j As Integer = 0 To gvProduct.Rows.Count - 1
                    If gvProduct.Rows(j).Cells(0).Text = objNode.Attributes("LineNumber").Value Then
                        If CType(gvProduct.Rows(i).Cells(4).FindControl("txtVenderSerial"), TextBox) IsNot Nothing Then
                            objNode.Attributes("SerialNumber").Value = CType(gvProduct.Rows(j).Cells(4).FindControl("txtVenderSerial"), TextBox).Text
                        End If
                    End If
                Next
            Next
            hdProductList.Value = objOutputXml.OuterXml
        End If
    End Sub
#End Region

#Region "BindXml()"
    Sub BindXml()
        Dim objOutputXml As New XmlDocument
        If gvProduct.Rows.Count > 0 Then

            Dim objNodeList As XmlNodeList
            Dim objNode As XmlNode
            objOutputXml.LoadXml(hdProductList.Value)
            objNodeList = objOutputXml.DocumentElement.SelectNodes("SCRAP/SCRAPDETAILS[@ProductID != '']")
            For i As Integer = 0 To objNodeList.Count - 1
                objNode = objNodeList(i)
                If CType(gvProduct.Rows(i).Cells(3).FindControl("txtVenderSerial"), TextBox) IsNot Nothing Then
                    objNode.Attributes("SerialNumber").Value = CType(gvProduct.Rows(i).Cells(3).FindControl("txtVenderSerial"), TextBox).Text
                End If

            Next
        End If
        hdProductList.Value = objOutputXml.OuterXml
    End Sub
#End Region

#Region "BindXml(ByVal st As String)"
    Sub BindXml(ByVal st As String)
        Dim objOutputXml As New XmlDocument
        If gvProduct.Rows.Count > 0 Then
            Dim objNodeList As XmlNodeList
            Dim objNode As XmlNode
            objOutputXml.LoadXml(hdProductList.Value)
            objNodeList = objOutputXml.DocumentElement.SelectNodes("SCRAP/SCRAPDETAILS[@ProductID != '']")
            For i As Integer = 0 To objNodeList.Count - 1
                objNode = objNodeList(i)
                If CType(gvProduct.Rows(i).Cells(3).FindControl("txtVenderSerial"), TextBox) IsNot Nothing Then
                    objNode.Attributes("SerialNumber").Value = CType(gvProduct.Rows(i).Cells(3).FindControl("txtVenderSerial"), TextBox).Text
                End If
            Next
            hdProductList.Value = objOutputXml.OuterXml
        End If
    End Sub
#End Region

#Region "ViewRecords() View records from data base"
    Sub ViewRecords()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzScrap As New AAMS.bizInventory.bzScrap
        objInputXml.LoadXml("<INV_VIEW_SCRAP_INPUT><TrashID></TrashID></INV_VIEW_SCRAP_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("TrashID").InnerText = hdScrapID.Value
        'Here Back end Method Call 

        objOutputXml = objbzScrap.View(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            With objOutputXml.DocumentElement.SelectSingleNode("SCRAP")
                txtTrashID.Text = .Attributes("TrashID").Value
                txtLoggedDate.Text = .Attributes("LoggedDate").Value
                txtLoggedBy.Text = .Attributes("LoggedByName").Value
                ddlGodown.SelectedValue = .Attributes("GodownID").Value
                ddlGodown.Enabled = False
                hdProductList.Value = objOutputXml.OuterXml
                btnExecute.Enabled = True
                ' btnPrint.Enabled = True
                ManageOrder()
                BindProduct()
                If .Attributes("ExecuteDate").Value <> "" Then
                    For Each ctrl As Control In Page.Controls
                        ChangeControlStatus(ctrl)
                    Next
                End If
            End With
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
#End Region

#Region "ChangeControlStatus(ByVal c As Control)"
    Sub ChangeControlStatus(ByVal c As Control)
        If c.Controls.Count > 0 Then
            For Each ctrl As Control In c.Controls
                ChangeControlStatus(ctrl)
            Next
        End If


        If TypeOf c Is TextBox Then
            DirectCast(c, TextBox).ReadOnly = True
        ElseIf TypeOf c Is Button Then
            'If DirectCast(c, Button).ID = "btnPrint" Or DirectCast(c, Button).ID = "btnNew" Then
            If DirectCast(c, Button).ID = "btnNew" Then
            Else
                DirectCast(c, Button).Enabled = False
            End If
        ElseIf TypeOf c Is RadioButton Then
            DirectCast(c, RadioButton).Enabled = False
        ElseIf TypeOf c Is ImageButton Then
            DirectCast(c, ImageButton).Enabled = False
        ElseIf TypeOf c Is CheckBox Then
            DirectCast(c, CheckBox).Enabled = False
        ElseIf TypeOf c Is DropDownList Then
            DirectCast(c, DropDownList).Enabled = False
        ElseIf TypeOf c Is HyperLink Then
            DirectCast(c, HyperLink).Enabled = False
        ElseIf TypeOf c Is LinkButton Then
            DirectCast(c, LinkButton).Enabled = False


        End If
    End Sub
#End Region

#Region "gvProduct_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvProduct.RowDataBound"
    Protected Sub gvProduct_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvProduct.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
        'If (DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE_BY").ToString.ToUpper = "FALSE" And DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE").ToString.ToUpper = "FALSE") Or (DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE_BY").ToString.ToUpper = "TRUE" And DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE").ToString.ToUpper = "FALSE") Or (DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE_BY").ToString.ToUpper = "TRUE" And DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE").ToString.ToUpper = "TRUE") Then
        '    e.Row.Cells(4).Controls.Clear()
        '    e.Row.Cells(4).Text = DataBinder.Eval(e.Row.DataItem, "VenderSerialNo")
        'End If
        Dim strProductID As String
        strProductID = DataBinder.Eval(e.Row.DataItem, "ProductID")
        If e.Row.RowIndex = 0 Then
            ViewState("ProductID") = strProductID
        Else
            If strProductID = ViewState("ProductID") Then
                e.Row.Cells(1).Text = ""
                e.Row.Cells(2).Text = ""
            End If
        End If
        ViewState("ProductID") = strProductID

        ''''''''''''''''''''''''
        If (DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE_BY").ToString.ToUpper = "FALSE" And DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE").ToString.ToUpper = "FALSE") Or (DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE_BY").ToString.ToUpper = "TRUE" And DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE").ToString.ToUpper = "FALSE") Or (DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE_BY").ToString.ToUpper = "TRUE" And DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE").ToString.ToUpper = "TRUE") Then
            e.Row.Cells(3).Controls.Clear()
            e.Row.Cells(3).Text = DataBinder.Eval(e.Row.DataItem, "SerialNumber")
        End If

        '''''''''''''''''''''''''''''
    End Sub
#End Region

    Private Sub CheckSecurity()
        Try
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If


            '*************************** Checking Security Start**********************
            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE SCRAP']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE SCRAP']").Attributes("Value").Value)
                End If

                'When View rights disabled
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                    btnExecute.Enabled = False
                End If
                'When Add rights disabled
                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    btnSave.Enabled = False
                    ' btnExecute.Enabled = False
                End If
                'When modify rights disabled and Add rights enabled
                If strBuilder(2) = "0" And (hdScrapID.Value <> "" Or Request.QueryString("TrashID") IsNot Nothing) Then
                    btnSave.Enabled = False
                    btnExecute.Enabled = False
                End If
                'When modify rights Enabled and Add rights disabled
                If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                    btnSave.Enabled = True
                    ' btnExecute.Enabled = False
                End If


            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            '*************************** Checking Security End**********************

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

End Class
