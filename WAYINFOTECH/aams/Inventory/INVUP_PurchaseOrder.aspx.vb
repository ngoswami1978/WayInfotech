Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Partial Class Inventory_INVUP_PurchaseOrder
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Protected strIndex As String
    Protected flagPermission As String
    Dim objED As New EncyrptDeCyrpt
    Shared counter As Integer
    Shared UpdateRow As Integer
    Shared totalCost As Double = 0
    Dim imgDown As New Image
    Dim imgUp As New Image
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        totalCost = 0
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        btnAdd.Attributes.Add("onclick", "return ProductValidation();")
        btnSave.Attributes.Add("onclick", "return ValidateSupplier();")
        drpProductName.Attributes.Add("onchange", "return validateProductQty();")
        'If Request("txtDeliveryDt") IsNot Nothing Then validateProductQty
        '    txtDeliveryDt.Text = Request("txtDeliveryDt").ToString()
        'End If
        'Supplier_Inv

        '***************************************************************************************
        imgUp.ImageUrl = "~/Images/Sortup.gif"
        imgDown.ImageUrl = "~/Images/Sortdown.gif"
        '***************************************************************************************

        'Dim XdocDel As New XmlDocument
        'XdocDel.LoadXml("<INV_VIEW_PURCHASEORDER_OUTPUT><PURCHASEORDER PurchaseOrderID='' Supplier_Inv='' SupplierID='' SupplierName='' Address='' OrderDate='' CreationDate='' LateDeliveryP='' LatePaymentP='' PaymentTerms='' PODescription='' EmployeeID='' Employee_Name='' Notes='' ApprovedBy='' ApprovedByName='' ApprovalDate='' TOTALCOST='' TAX='' NETCOST=''><PURCHASEORDERDETAILS TempRow='' PRODUCTID='' PRODUCTNAME='' Qty='' Rate='' Tax='' Warranty='' WarrantyOnSite='' DeliveryDate='' GodownID='' GodownName=''/></PURCHASEORDER><Errors Status=''><Error Code='' Description='' /></Errors></INV_VIEW_PURCHASEORDER_OUTPUT>")
        'DelNode = XdocDel.DocumentElement.SelectSingleNode("PURCHASEORDER/PURCHASEORDERDETAILS")

        Try
            If hdTabType.Value = "" Then
                hdTabType.Value = 0
            Else
                strIndex = hdTabType.Value
            End If
            If Request.QueryString("TabType") IsNot Nothing Then
                hdTabType.Value = Request.QueryString("TabType")
            End If

            If Request("hdSupplierID") IsNot Nothing Then
                txtSupplierName.Text = Request("txtSupplierName")
                txtAddress.Text = Request("txtAddress")
            End If

            If Request("hdEmpID") IsNot Nothing Then
                txtLogedByName.Text = Request("txtLogedByName")
            End If
            If Request("txtPurchaseOrderNo") IsNot Nothing Then
                txtPurchaseOrderNo.Text = Request("txtPurchaseOrderNo")
            End If

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If
            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CHALLAN PURCHASEORDER']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CHALLAN PURCHASEORDER']").Attributes("Value").Value)
                End If
                'If strBuilder(0) = "0" Then
                '    btnSave.Enabled = False
                'End If
                'If strBuilder(1) = "0" Then
                '    btnNew.Enabled = False
                'End If
                'When View rights disabled
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                End If
                'When Add rights disabled
                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    btnSave.Enabled = False
                End If
                'When modify rights disabled and Add rights enabled
                If strBuilder(2) = "0" And Request.QueryString("Action") = "U" Then
                    btnSave.Enabled = False
                End If
                'When modify rights Enabled and Add rights disabled
                If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                    btnSave.Enabled = True
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If


            If hdDeleteFlag.Value <> "" Then
                DeleteProduct()
            End If
            If hdEditFlag.Value <> "" Then
                EditProducts()
            End If


            If Not Page.IsPostBack Then

                'Sorting 
                ViewState("SortName") = "PRODUCTNAME"
                ViewState("Desc") = "TRUE"
                'End of Sorting
                Bindata()
                BindDataDropdown()
                If Request.QueryString("Action") IsNot Nothing Then
                    If Request.QueryString("Action") = "U" Then
                        If Request.QueryString("PurchaseID") IsNot Nothing Then
                            ViewPurchaseDetails()
                        End If
                    Else
                        If Session("Security") IsNot Nothing Then
                            Dim objSecXml As New XmlDocument
                            objSecXml.LoadXml(Session("Security"))
                            txtCreatedBy.Text = objSecXml.DocumentElement.SelectSingleNode("Employee_Name").InnerText.Trim()
                            hdCreatedby.Value = objSecXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                        End If

                    End If
                End If
                If Request.QueryString("MSG") IsNot Nothing Then
                    If Request.QueryString("MSG") = "M" Then
                        lblError.Text = objeAAMSMessage.messUpdate
                    End If
                End If
            End If


        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Sub Bindata()
        Dim TabText As New ArrayList()
        Try
            TabText.Add("Order")
            TabText.Add("Product Details")
            theTabStrip.DataSource = TabText
            theTabStrip.DataBind()
            hideTabStrip(strIndex)
            Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound
        Try
            Dim Button1 As Button
            Button1 = e.Item.FindControl("Button1")
            If e.Item.ItemIndex = 0 Then
                Button1.CssClass = "headingtab"
            End If
            Button1 = e.Item.FindControl("Button1")
            Button1.Attributes.Add("onclick", "return ColorMethod('" & Button1.ClientID.ToString() & "',2);")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub hideTabStrip(ByVal strIndex As String)
        Dim strScript As String = ""
        Select Case strIndex
            Case "0"
                strScript = "<script language='javascript'>" & _
                "document.getElementById('pnlCall').style.display ='block'; " & _
                "document.getElementById('pnlDescription').style.display  ='none';" & _
                "</script>"

            Case "1"
                strScript = "<script language='javascript'>" & _
               "document.getElementById('pnlCall').style.display ='none'; " & _
               "document.getElementById('pnlDescription').style.display  ='block';" & _
               "</script>"
           End Select
        'ClientScript.RegisterStartupScript(Me.GetType(),"strScript", strScript)
    End Sub




    Private Sub ViewPurchaseDetails()
        Try

            ' <INV_VIEW_PURCHASEORDER_OUTPUT>
            ' <PURCHASEORDER PurchaseOrderID='' Supplier_Inv='' SupplierID='' SupplierName='' Address='' OrderDate='' CreationDate='' LateDeliveryP='' LatePaymentP='' PaymentTerms='' PODescription='' EmployeeID='' Employee_Name='' Notes='' ApprovedBy='' ApprovedByName='' ApprovalDate='' TOTALCOST='' TAX='' NETCOST=''>
            '  <PURCHASEORDERDETAILS PRODUCTID='' PRODUCTNAME='' Qty='' Rate='' Tax='' Warranty='' WarrantyOnSite='' DeliveryDate='' GodownID='' GodownName=''/>
            ' </PURCHASEORDER>
            ' <Errors Status=''>
            '<Error Code='' Description='' />
            '  </Errors>
            ' </INV_VIEW_PURCHASEORDER_OUTPUT>




            Dim objInputOrderXml, objOutputOrderXml As New XmlDocument


            Dim objbzPurchaseOrder As New AAMS.bizInventory.bzPurchaseOrder
            objInputOrderXml.LoadXml("<INV_VIEW_PURCHASEORDER_INPUT><PurchaseOrderID></PurchaseOrderID></INV_VIEW_PURCHASEORDER_INPUT>")
            objInputOrderXml.DocumentElement.SelectSingleNode("PurchaseOrderID").InnerText = objED.Decrypt(Request.QueryString("PurchaseID").Trim())

            txtPurchaseOrderNo.Text = objED.Decrypt(Request.QueryString("PurchaseID").Trim())

            objOutputOrderXml = objbzPurchaseOrder.View(objInputOrderXml)


            If objOutputOrderXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                With objOutputOrderXml.DocumentElement.SelectSingleNode("PURCHASEORDER")
                    hdPurchaseID.Value = .Attributes("PurchaseOrderID").Value.Trim()
                    hdSupplierID.Value = .Attributes("SupplierID").Value.Trim()
                    txtSupplierInv.Text = .Attributes("Supplier_Inv").Value.Trim()
                    txtSupplierName.Text = .Attributes("SupplierName").Value.Trim()
                    txtAddress.Text = .Attributes("Address").Value.Trim()
                    txtOrderDt.Text = objeAAMS.ConvertDateBlank(.Attributes("OrderDate").Value.Trim())
                    txtCreationDt.Text = .Attributes("CreationDate").Value.Trim() 'wrong
                    txtLastDeliveryPenaly.Text = .Attributes("LateDeliveryP").Value.Trim()
                    txtLastPaymentPenalty.Text = .Attributes("LatePaymentP").Value.Trim()
                    txtPaymentTerms.Text = .Attributes("PaymentTerms").Value.Trim()
                    txtDescription.Text = .Attributes("PODescription").Value.Trim()
                    txtNotes.Text = .Attributes("Notes").Value.Trim()
                    txtCreatedBy.Text = .Attributes("Employee_Name").Value.Trim() 'check
                    hdCreatedby.Value = .Attributes("EmployeeID").Value.Trim()
                    If .Attributes("ApprovalDate").Value.Trim().Length <> 0 Then
                        txtAppovalDt.Text = objeAAMS.ConvertDateBlank(.Attributes("ApprovalDate").Value.Trim())
                    End If
                    txtLogedByName.Text = .Attributes("ApprovedByName").Value.Trim() 'check 
                    hdEmpID.Value = .Attributes("ApprovedBy").Value.Trim()

                End With
            End If

            Dim objAppendXml As New XmlDocument
            objAppendXml.LoadXml("<INV_VIEW_PURCHASEORDER_OUTPUT><PURCHASEORDER PurchaseOrderID='' Supplier_Inv='' SupplierID='' SupplierName='' Address='' OrderDate='' CreationDate='' LateDeliveryP='' LatePaymentP='' PaymentTerms='' PODescription='' EmployeeID='' Employee_Name='' Notes='' ApprovedBy='' ApprovedByName='' ApprovalDate='' TOTALCOST='' TAX='' NETCOST=''><PURCHASEORDERDETAILS TempRow='' PRODUCTID='' PRODUCTNAME='' Qty='' Rate='' Tax='' Warranty='' WarrantyOnSite='' DeliveryDate='' GodownID='' GodownName='' MAINTAIN_BALANCE='' MAINTAIN_BALANCE_BY=''/></PURCHASEORDER><Errors Status=''><Error Code='' Description='' /></Errors></INV_VIEW_PURCHASEORDER_OUTPUT>")

            With objAppendXml.DocumentElement.SelectSingleNode("PURCHASEORDER")
                .Attributes("PurchaseOrderID").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("PURCHASEORDER").Attributes("PurchaseOrderID").Value
                .Attributes("SupplierID").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("PURCHASEORDER").Attributes("SupplierID").Value
                .Attributes("Supplier_Inv").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("PURCHASEORDER").Attributes("Supplier_Inv").Value
                .Attributes("SupplierName").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("PURCHASEORDER").Attributes("SupplierName").Value
                .Attributes("Address").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("PURCHASEORDER").Attributes("Address").Value
                .Attributes("OrderDate").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("PURCHASEORDER").Attributes("OrderDate").Value
                .Attributes("CreationDate").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("PURCHASEORDER").Attributes("CreationDate").Value
                .Attributes("LateDeliveryP").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("PURCHASEORDER").Attributes("LateDeliveryP").Value
                .Attributes("LatePaymentP").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("PURCHASEORDER").Attributes("LatePaymentP").Value
                .Attributes("PaymentTerms").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("PURCHASEORDER").Attributes("PaymentTerms").Value
                .Attributes("PODescription").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("PURCHASEORDER").Attributes("PODescription").Value
                .Attributes("EmployeeID").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("PURCHASEORDER").Attributes("EmployeeID").Value
                .Attributes("Employee_Name").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("PURCHASEORDER").Attributes("Employee_Name").Value
                .Attributes("Notes").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("PURCHASEORDER").Attributes("Notes").Value
                .Attributes("ApprovedBy").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("PURCHASEORDER").Attributes("ApprovedBy").Value
                '.Attributes("ApprovedByName").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("PURCHASEORDER").Attributes("ApprovedByName").Value
                .Attributes("ApprovalDate").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("PURCHASEORDER").Attributes("ApprovalDate").Value
                .Attributes("TOTALCOST").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("PURCHASEORDER").Attributes("TOTALCOST").Value
                .Attributes("TAX").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("PURCHASEORDER").Attributes("TAX").Value
                .Attributes("NETCOST").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("PURCHASEORDER").Attributes("NETCOST").Value
            End With
            Dim objNodeList As XmlNodeList
            Dim objNode, ChildNodeClone As XmlNode
            objNodeList = objOutputOrderXml.DocumentElement.SelectNodes("PURCHASEORDER/PURCHASEORDERDETAILS")


            Dim childNode As XmlNode = objAppendXml.DocumentElement.SelectSingleNode("PURCHASEORDER/PURCHASEORDERDETAILS")
            Dim ParenNode As XmlNode = objAppendXml.DocumentElement.SelectSingleNode("PURCHASEORDER")
            ChildNodeClone = childNode.CloneNode(True)
            ParenNode.RemoveChild(childNode)


            counter = 0

            For Each objNode In objNodeList
                ' <PURCHASEORDERDETAILS  PRODUCTID='' PRODUCTNAME='' Qty='' Rate='' Tax='' Warranty='
                ' WarrantyOnSite='' DeliveryDate='' GodownID='' GodownName=''/>
                ChildNodeClone.Attributes("PRODUCTID").Value = objNode.Attributes("PRODUCTID").Value
                ChildNodeClone.Attributes("PRODUCTNAME").Value = objNode.Attributes("PRODUCTNAME").Value
                ChildNodeClone.Attributes("Qty").Value = objNode.Attributes("Qty").Value
                ChildNodeClone.Attributes("Rate").Value = objNode.Attributes("Rate").Value
                ChildNodeClone.Attributes("Tax").Value = objNode.Attributes("Tax").Value
                ChildNodeClone.Attributes("Warranty").Value = objNode.Attributes("Warranty").Value
                ChildNodeClone.Attributes("WarrantyOnSite").Value = objNode.Attributes("WarrantyOnSite").Value
                If objNode.Attributes("DeliveryDate").Value.Trim().Length <> 0 Then
                    ChildNodeClone.Attributes("DeliveryDate").Value = objeAAMS.ConvertDateBlank(objNode.Attributes("DeliveryDate").Value.Trim())
                End If
                ChildNodeClone.Attributes("GodownID").Value = objNode.Attributes("GodownID").Value
                ChildNodeClone.Attributes("GodownName").Value = objNode.Attributes("GodownName").Value

                ChildNodeClone.Attributes("MAINTAIN_BALANCE").Value = objNode.Attributes("MAINTAIN_BALANCE").Value
                ChildNodeClone.Attributes("MAINTAIN_BALANCE_BY").Value = objNode.Attributes("MAINTAIN_BALANCE_BY").Value

                counter = counter + 1
                ChildNodeClone.Attributes("TempRow").Value = counter
                ParenNode.AppendChild(ChildNodeClone)
                ChildNodeClone = childNode.CloneNode(True)
            Next

            hdTempXml.Value = objAppendXml.OuterXml


           
            'DisplayProductListSort("PRODUCTNAME")

            BindProductDetails("PRODUCTNAME")
           

            
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Private Sub BindProductDetails(ByVal strrColName As String)
        Try
            Dim objNewXml As New XmlDataDocument
            If hdTempXml.Value <> "" Then
                objNewXml.LoadXml(hdTempXml.Value)
                Dim objNode As XmlNode
                objNode = objNewXml.DocumentElement.SelectSingleNode("PURCHASEORDER/PURCHASEORDERDETAILS[@PRODUCTID='']")
                If objNode IsNot Nothing Then
                    objNewXml.DocumentElement.SelectSingleNode("PURCHASEORDER").RemoveChild(objNode)
                End If
                Dim objReader As XmlReader
                Dim dSet As New DataSet
                objReader = New XmlNodeReader(objNewXml)
                dSet.ReadXml(objReader)

                If ViewState("Desc") = "FALSE" Then
                    strrColName &= " "
                    strrColName &= "ASC"
                End If
                If ViewState("Desc") = "TRUE" Then
                    strrColName &= " "
                    strrColName &= "DESC"
                End If



                If dSet.Tables("PURCHASEORDERDETAILS") IsNot Nothing Then
                    If dSet.Tables("PURCHASEORDERDETAILS").Rows.Count > 0 Then
                        dSet.Tables("PURCHASEORDERDETAILS").DefaultView.Sort = strrColName

                        grdvProduct.DataSource = dSet.Tables("PURCHASEORDERDETAILS")
                        grdvProduct.DataBind()

                        Dim intcol As Integer = GetSortColumnIndex(grdvProduct)

                        If ViewState("Desc") = "FALSE" Then
                            grdvProduct.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                        End If

                        If ViewState("Desc") = "TRUE" Then
                            grdvProduct.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                        End If


                    End If

                    txtTotalCost.Text = Format(totalCost, "0.00")
                Else
                    grdvProduct.DataSource = Nothing
                    grdvProduct.DataBind()
                End If
            Else
                grdvProduct.DataSource = Nothing
                grdvProduct.DataBind()
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


            Dim objoutXml As New XmlDocument
            Dim AppendedXml As New XmlDocument
            Dim ChildNodeClone As XmlNode
            'objoutXml.LoadXml("<INV_VIEW_PURCHASEORDER_OUTPUT><PURCHASEORDER Supplier_Inv='' PurchaseOrderID='' SupplierID='' SupplierName='' Address='' OrderDate='' CreationDate='' LateDeliveryP='' LatePaymentP='' PaymentTerms='' PODescription='' EmployeeID='' Employee_Name='' Notes='' ApprovedBy='' ApprovedByName='' ApprovalDate='' TOTALCOST='' TAX='' NETCOST=''><PURCHASEORDERDETAILS TempRow='' PRODUCTID='' PRODUCTNAME='' Qty='' Rate='' Tax='' Warranty='' WarrantyOnSite='' DeliveryDate='' GodownID='' GodownName=''/></PURCHASEORDER><Errors Status=''><Error Code='' Description='' /></Errors></INV_VIEW_PURCHASEORDER_OUTPUT>")


            ' ParenNode.RemoveChild(childNode)

            If btnAdd.Text.Trim().ToUpper() = "UPDATE" Then
                AppendedXml.LoadXml(hdTempXml.Value)


                'Code for Restricting unique Godown and Product

                'If hdProductUniqe.Value.Trim().Split("|").GetValue(0) <> drpProductName.SelectedValue.Trim().Split("|").GetValue(0) Then
                Dim intProductCounter As Int16 = AppendedXml.DocumentElement.SelectNodes("PURCHASEORDER/PURCHASEORDERDETAILS[@PRODUCTID='" & drpProductName.SelectedValue.Trim().Split("|").GetValue(0) & "'][@GodownID='" & drpGodown.SelectedValue & "']").Count
                If intProductCounter > 1 Then
                    lblError.Text = "Product Already Added to List"
                    Exit Sub
                End If

                
                'End If

                'If hdGodownUnique.Value <> drpGodown.SelectedValue Then

                '    Dim intGodownCounter As XmlNode = AppendedXml.DocumentElement.SelectSingleNode("PURCHASEORDER/PURCHASEORDERDETAILS[@GodownID='" & drpGodown.SelectedValue & "'][@]")

                '    If intGodownCounter IsNot Nothing Then
                '        lblError.Text = "Product Already Added from this Godown"
                '        Exit Sub
                '    End If
                'Else

                'End If




                'Code for Restricting unique Godown and Product


                Dim childNode As XmlNode = AppendedXml.DocumentElement.SelectSingleNode("PURCHASEORDER/PURCHASEORDERDETAILS")
                Dim ParenNode As XmlNode = AppendedXml.DocumentElement.SelectSingleNode("PURCHASEORDER")
                ChildNodeClone = childNode.CloneNode(True)
                ChildNodeClone.Attributes("PRODUCTID").Value = drpProductName.SelectedValue.Trim().Split("|").GetValue(0)

                ChildNodeClone.Attributes("MAINTAIN_BALANCE").Value = drpProductName.SelectedValue.Trim().Split("|").GetValue(1)
                ChildNodeClone.Attributes("MAINTAIN_BALANCE_BY").Value = drpProductName.SelectedValue.Trim().Split("|").GetValue(2)

                ChildNodeClone.Attributes("PRODUCTNAME").Value = drpProductName.SelectedItem.Text
                ChildNodeClone.Attributes("Qty").Value = txtQty.Text.Trim()
                ChildNodeClone.Attributes("Rate").Value = txtRates.Text.Trim()
                ChildNodeClone.Attributes("Tax").Value = txtTax.Text.Trim()
                ChildNodeClone.Attributes("Warranty").Value = txtWarranty.Text.Trim()

                If chkOnsite.Checked Then
                    ChildNodeClone.Attributes("WarrantyOnSite").Value = "True"
                Else
                    ChildNodeClone.Attributes("WarrantyOnSite").Value = "False"
                End If

                ChildNodeClone.Attributes("DeliveryDate").Value = Request("txtDeliveryDt") '.Text.Trim()
                ChildNodeClone.Attributes("GodownID").Value = drpGodown.SelectedValue
                ChildNodeClone.Attributes("GodownName").Value = drpGodown.SelectedItem.Text
                Dim objXmlNodeOld As XmlNode
                objXmlNodeOld = AppendedXml.DocumentElement.SelectSingleNode("PURCHASEORDER/PURCHASEORDERDETAILS[@TempRow='" + UpdateRow.ToString() + "']")
                ChildNodeClone.Attributes("TempRow").Value = objXmlNodeOld.Attributes("TempRow").Value.Trim()

                AppendedXml.DocumentElement.SelectSingleNode("PURCHASEORDER").ReplaceChild(ChildNodeClone, objXmlNodeOld) '   .AppendChild(ChildNodeClone)
                hdTempXml.Value = AppendedXml.OuterXml


                ''''''''''''''
                Dim intProductCounterOld As Int16 = AppendedXml.DocumentElement.SelectNodes("PURCHASEORDER/PURCHASEORDERDETAILS[@PRODUCTID='" & drpProductName.SelectedValue.Trim().Split("|").GetValue(0) & "'][@GodownID='" & drpGodown.SelectedValue & "']").Count
                If intProductCounterOld > 1 Then
                    lblError.Text = "Product Already Added to List"
                    AppendedXml.DocumentElement.SelectSingleNode("PURCHASEORDER").ReplaceChild(objXmlNodeOld, ChildNodeClone) '   .AppendChild(ChildNodeClone)
                    hdTempXml.Value = AppendedXml.OuterXml
                    Exit Sub
                End If

                ''''''''''''''''

                'BindProductDetails()
                btnAdd.Text = "Add"
                hdEditFlag.Value = ""
                lblError.Text = "Records Updated"

                'Binding Due to Removing Scraped Product from List
                drpProductName.Items.Clear()
                objeAAMS.BindDropDown(drpProductName, "EQUIPMENTCODEWITH_MAINTAIN_BALANCE_BY", True)

            ElseIf hdTempXml.Value <> "" Then
                AppendedXml.LoadXml(hdTempXml.Value)

                'Code for Restricting unique Godown and Product

                Dim objuniqProductNode As XmlNode
                '  Dim objuniqGodownNode As XmlNode

                objuniqProductNode = AppendedXml.DocumentElement.SelectSingleNode("PURCHASEORDER/PURCHASEORDERDETAILS[@PRODUCTID='" & drpProductName.SelectedValue.Trim().Split("|").GetValue(0) & "']")
                If objuniqProductNode IsNot Nothing Then
                    If objuniqProductNode.Attributes("GodownID").Value = drpGodown.SelectedValue Then
                        lblError.Text = "Product Already Added to List"
                        Exit Sub
                    End If
                End If

                'objuniqGodownNode = AppendedXml.DocumentElement.SelectSingleNode("PURCHASEORDER/PURCHASEORDERDETAILS[@GodownID='" & drpGodown.SelectedValue & "']")

                'If objuniqGodownNode IsNot Nothing Then
                '    lblError.Text = "Product Already Added from this Godown"
                '    Exit Sub
                'End If

                'Code for Restricting unique Godown and Product



                Dim childNode As XmlNode = AppendedXml.DocumentElement.SelectSingleNode("PURCHASEORDER/PURCHASEORDERDETAILS")


                'Segment for adding a blank chlid Node

                If childNode Is Nothing Then
                    AppendedXml.LoadXml("<INV_VIEW_PURCHASEORDER_OUTPUT><PURCHASEORDER PurchaseOrderID='' Supplier_Inv='' SupplierID='' SupplierName='' Address='' OrderDate='' CreationDate='' LateDeliveryP='' LatePaymentP='' PaymentTerms='' PODescription='' EmployeeID='' Employee_Name='' Notes='' ApprovedBy='' ApprovedByName='' ApprovalDate='' TOTALCOST='' TAX='' NETCOST=''><PURCHASEORDERDETAILS TempRow='' PRODUCTID='' PRODUCTNAME='' Qty='' Rate='' Tax='' Warranty='' WarrantyOnSite='' DeliveryDate='' GodownID='' GodownName='' MAINTAIN_BALANCE='' MAINTAIN_BALANCE_BY=''/></PURCHASEORDER><Errors Status=''><Error Code='' Description='' /></Errors></INV_VIEW_PURCHASEORDER_OUTPUT>")
                    childNode = AppendedXml.DocumentElement.SelectSingleNode("PURCHASEORDER/PURCHASEORDERDETAILS")

                End If

                Dim ParenNode As XmlNode = AppendedXml.DocumentElement.SelectSingleNode("PURCHASEORDER")

                'Segment for adding a blank chlid Node  End

                ChildNodeClone = childNode.CloneNode(True)
                ChildNodeClone.Attributes("PRODUCTID").Value = drpProductName.SelectedValue.Trim().Split("|").GetValue(0)
                ChildNodeClone.Attributes("PRODUCTNAME").Value = drpProductName.SelectedItem.Text

                ChildNodeClone.Attributes("MAINTAIN_BALANCE").Value = drpProductName.SelectedValue.Trim().Split("|").GetValue(1)
                ChildNodeClone.Attributes("MAINTAIN_BALANCE_BY").Value = drpProductName.SelectedValue.Trim().Split("|").GetValue(2)

                ChildNodeClone.Attributes("Qty").Value = txtQty.Text.Trim()
                ChildNodeClone.Attributes("Rate").Value = txtRates.Text.Trim()
                ChildNodeClone.Attributes("Tax").Value = txtTax.Text.Trim()
                ChildNodeClone.Attributes("Warranty").Value = txtWarranty.Text.Trim()

                If chkOnsite.Checked Then
                    ChildNodeClone.Attributes("WarrantyOnSite").Value = "True"
                Else
                    ChildNodeClone.Attributes("WarrantyOnSite").Value = "False"
                End If

                ChildNodeClone.Attributes("DeliveryDate").Value = Request("txtDeliveryDt").ToString().Trim() 'txtDeliveryDt.Text.Trim()
                ChildNodeClone.Attributes("GodownID").Value = drpGodown.SelectedValue
                ChildNodeClone.Attributes("GodownName").Value = drpGodown.SelectedItem.Text
                counter = counter + 1
                ChildNodeClone.Attributes("TempRow").Value = counter
                AppendedXml.DocumentElement.SelectSingleNode("PURCHASEORDER").AppendChild(ChildNodeClone)
                hdTempXml.Value = AppendedXml.OuterXml
                lblError.Text = "Records Added"

                'BindProductDetails()
            Else
                AppendedXml.LoadXml("<INV_VIEW_PURCHASEORDER_OUTPUT><PURCHASEORDER PurchaseOrderID='' Supplier_Inv='' SupplierID='' SupplierName='' Address='' OrderDate='' CreationDate='' LateDeliveryP='' LatePaymentP='' PaymentTerms='' PODescription='' EmployeeID='' Employee_Name='' Notes='' ApprovedBy='' ApprovedByName='' ApprovalDate='' TOTALCOST='' TAX='' NETCOST=''><PURCHASEORDERDETAILS TempRow='' PRODUCTID='' PRODUCTNAME='' Qty='' Rate='' Tax='' Warranty='' WarrantyOnSite='' DeliveryDate='' GodownID='' GodownName='' MAINTAIN_BALANCE='' MAINTAIN_BALANCE_BY='' /></PURCHASEORDER><Errors Status=''><Error Code='' Description='' /></Errors></INV_VIEW_PURCHASEORDER_OUTPUT>")
                Dim childNode As XmlNode = AppendedXml.DocumentElement.SelectSingleNode("PURCHASEORDER/PURCHASEORDERDETAILS")
                Dim ParenNode As XmlNode = AppendedXml.DocumentElement.SelectSingleNode("PURCHASEORDER")
                ChildNodeClone = childNode.CloneNode(True)
                ChildNodeClone.Attributes("PRODUCTID").Value = drpProductName.SelectedValue.Trim().Split("|").GetValue(0)
                ChildNodeClone.Attributes("PRODUCTNAME").Value = drpProductName.SelectedItem.Text

                ChildNodeClone.Attributes("MAINTAIN_BALANCE").Value = drpProductName.SelectedValue.Trim().Split("|").GetValue(1)
                ChildNodeClone.Attributes("MAINTAIN_BALANCE_BY").Value = drpProductName.SelectedValue.Trim().Split("|").GetValue(2)


                ChildNodeClone.Attributes("Qty").Value = txtQty.Text.Trim()
                ChildNodeClone.Attributes("Rate").Value = txtRates.Text.Trim()
                ChildNodeClone.Attributes("Tax").Value = txtTax.Text.Trim()
                ChildNodeClone.Attributes("Warranty").Value = txtWarranty.Text.Trim()

                If chkOnsite.Checked Then
                    ChildNodeClone.Attributes("WarrantyOnSite").Value = "True"
                Else
                    ChildNodeClone.Attributes("WarrantyOnSite").Value = "False"
                End If
                ChildNodeClone.Attributes("DeliveryDate").Value = Request("txtDeliveryDt") '.Text.Trim()
                ChildNodeClone.Attributes("GodownID").Value = drpGodown.SelectedValue
                ChildNodeClone.Attributes("GodownName").Value = drpGodown.SelectedItem.Text
                counter = counter + 1
                ChildNodeClone.Attributes("TempRow").Value = counter
                AppendedXml.DocumentElement.SelectSingleNode("PURCHASEORDER").AppendChild(ChildNodeClone)
                hdTempXml.Value = AppendedXml.OuterXml
                lblError.Text = "Records Added"

            End If
            drpGodown.SelectedIndex = 0
            drpProductName.SelectedIndex = 0
            txtQty.Text = ""
            txtWarranty.Text = ""
            txtTax.Text = ""
            chkOnsite.Checked = False
            txtRates.Text = ""
            txtDeliveryDt.Text = ""
            txtQty.ReadOnly = False
            txtQty.CssClass = "textbox"

            hdGodownUnique.Value = ""
            hdProductUniqe.Value = ""

            Dim strCol As String

            If ViewState("SortName") IsNot Nothing Then
                strCol = ViewState("SortName").ToString()
            Else
                strCol = "PRODUCTNAME"
            End If

            BindProductDetails(strCol)

            ' BindProductDetails()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub BindDataDropdown()
        Try
            ' objeAAMS.BindDropDown(drpGodown, "GODOWN", True)
            FillGodown()
            objeAAMS.BindDropDown(drpProductName, "EQUIPMENTCODEWITH_MAINTAIN_BALANCE_BY", True)
            ' objeAAMS.BindDropDown(drpProductName, "EQUIPMENTCODE", True)

            'EQUIPMENTCODE
            ' objeAAMS.BindDropDown(drpCreatedBy, "EMPLOYEE", True)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Sub FillGodown()

        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))


        ' This code binds godown on the basis of logged user
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            Try
                drpGodown.Items.Clear()
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
                    drpGodown.DataSource = ds.Tables("GODOWN").DefaultView
                    drpGodown.DataTextField = "GODOWNNAME"
                    drpGodown.DataValueField = "GODOWNID"
                    drpGodown.DataBind()
                Else
                    If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value <> "No Record Found") Then
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            Finally
                drpGodown.Items.Insert(0, New ListItem("---Select One---", ""))
            End Try
        Else
            objeAAMS.BindDropDown(drpGodown, "GODOWN", True)
        End If


    End Sub

    Protected Sub grdvProduct_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvProduct.RowDataBound
        Try

            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If

            Dim hdDelFlag As HiddenField
            Dim btnEdit As LinkButton
            Dim btnDelete As LinkButton


            hdDelFlag = CType(e.Row.FindControl("hdRowID"), HiddenField)

            btnEdit = CType(e.Row.FindControl("linkEdit"), LinkButton)
            btnDelete = CType(e.Row.FindControl("linkDelete"), LinkButton)


            If CType(e.Row.FindControl("lblWarrentyOnsite"), Label).Text.ToUpper() = "TRUE" Then
                CType(e.Row.FindControl("lblWarrentyOnsite"), Label).Text = "Yes"
            Else
                CType(e.Row.FindControl("lblWarrentyOnsite"), Label).Text = "No"
            End If

            Dim qty, rate, tax As String

            qty = CType(e.Row.FindControl("lblQty"), Label).Text.Trim()
            rate = CType(e.Row.FindControl("lblRate"), Label).Text.Trim()
            tax = CType(e.Row.FindControl("bllTax"), Label).Text.Trim()
            'If qty.Trim.Length <> 0 Or qty IsNot  Then
            If CType(e.Row.FindControl("hdMaintain"), HiddenField).Value.ToUpper = "FALSE" And CType(e.Row.FindControl("hdMaintainby"), HiddenField).Value.ToUpper = "FALSE" Then
                totalCost += Convert.ToDouble(Val(rate & "")) + Convert.ToDouble(Val(tax & ""))
            Else
                totalCost += (Convert.ToDouble(Val(qty & "")) * Convert.ToDouble(Val(rate & ""))) + Convert.ToDouble(Val(tax & ""))
            End If


            ' Dim lblTcost As Label
            If CType(e.Row.FindControl("hdMaintain"), HiddenField).Value.ToUpper = "FALSE" And CType(e.Row.FindControl("hdMaintainby"), HiddenField).Value.ToUpper = "FALSE" Then
                CType(e.Row.FindControl("lblTotal"), Label).Text = (Convert.ToDouble(Val(rate & ""))) + Convert.ToDouble(Val(tax & ""))
            Else
                CType(e.Row.FindControl("lblTotal"), Label).Text = (Convert.ToDouble(Val(qty & "")) * Convert.ToDouble(Val(rate & ""))) + Convert.ToDouble(Val(tax & ""))
            End If


            'End If


            'Dim objSecurityXml As New XmlDocument
            'objSecurityXml.LoadXml(Session("Security"))
            'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Pruchase Order']").Count <> 0 Then
            '        strBuilder = objEaams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Pruchase Order']").Attributes("Value").Value)
            '    End If
            '    If strBuilder(3) = "0" Then
            '        btnDelete.Enabled = False
            '    Else
            '        btnDelete.Attributes.Add("OnClick", "javascript:return DeleteFunction('" + hdDelFlag.Value + "');")
            '    End If
            '    If strBuilder(2) = "0" Then
            '        btnEdit.Enabled = False
            '    Else
            '        btnEdit.Attributes.Add("OnClick", "javascript:return EditFunction('" + hdDelFlag.Value + "');")
            '    End If

            'Else
            '    btnDelete.Attributes.Add("OnClick", "javascript:return DeleteFunction('" + hdDelFlag.Value + "');")
            '    btnEdit.Attributes.Add("OnClick", "javascript:return EditFunction('" + hdDelFlag.Value + "');")
            'End If

            btnDelete.Attributes.Add("OnClick", "javascript:return DeleteFunction('" + hdDelFlag.Value + "');")
            btnEdit.Attributes.Add("OnClick", "javascript:return EditFunction('" + hdDelFlag.Value + "');")

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub DeleteProduct()
        Try
           Dim objInputXml, objOutputXml As New XmlDocument
            Dim objXmlNode As XmlNode
            objInputXml.LoadXml(hdTempXml.Value)
            If objInputXml.DocumentElement.SelectNodes("PURCHASEORDER/PURCHASEORDERDETAILS").Count > 1 Then
                objXmlNode = objInputXml.DocumentElement.SelectSingleNode("PURCHASEORDER/PURCHASEORDERDETAILS[@TempRow='" + hdDeleteFlag.Value + "']")
                objInputXml.DocumentElement.SelectSingleNode("PURCHASEORDER").RemoveChild(objXmlNode)
                lblError.Text = "Records Removed" 'objeAAMSMessage.messDelete
                hdDeleteFlag.Value = ""
                hdTempXml.Value = objInputXml.OuterXml
               Else
                lblError.Text = "Single Product List cannot be removed"
            End If

            Dim strCol As String

            If ViewState("SortName") IsNot Nothing Then
                strCol = ViewState("SortName").ToString()
            Else
                strCol = "PRODUCTNAME"
            End If
            txtTotalCost.Text = "0.0"
            BindProductDetails(strCol)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            hdTabType.Value = 0

            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbzProductOrder As New AAMS.bizInventory.bzPurchaseOrder

            objInputXml.LoadXml("<INV_UPDATE_PURCHASEORDER_INPUT><PURCHASEORDER Supplier_Inv='' PurchaseOrderID='' SupplierID='' OrderDate='' LateDeliveryP='' LatePaymentP='' PaymentTerms='' PODescription='' EmployeeID='' Notes='' ApprovedBy='' ApprovalDate=''><PURCHASEORDERDETAILS PRODUCTID='' Qty='' Rate='' Tax='' Warranty='' WarrantyOnSite='' DeliveryDate='' GodownID=''/></PURCHASEORDER></INV_UPDATE_PURCHASEORDER_INPUT>")

            With objInputXml.DocumentElement.SelectSingleNode("PURCHASEORDER")
                If Request.QueryString("Action") IsNot Nothing Then
                    If Request.QueryString("Action") = "U" Then
                        .Attributes("PurchaseOrderID").Value = objED.Decrypt(Request.QueryString("PurchaseID").Trim())
                    Else
                        .Attributes("PurchaseOrderID").Value = ""
                    End If
                End If
                .Attributes("Supplier_Inv").Value = txtSupplierInv.Text.Trim()
                .Attributes("SupplierID").Value = hdSupplierID.Value
                .Attributes("OrderDate").Value = objeAAMS.ConvertTextDate(txtOrderDt.Text)
                .Attributes("LateDeliveryP").Value = txtLastDeliveryPenaly.Text.Trim()
                .Attributes("LatePaymentP").Value = txtLastPaymentPenalty.Text.Trim()
                .Attributes("PaymentTerms").Value = txtPaymentTerms.Text.Trim()
                .Attributes("PODescription").Value = txtDescription.Text.Trim()

                If Session("Security") IsNot Nothing Then
                    Dim objSecurityXml As New XmlDocument
                    objSecurityXml.LoadXml(Session("Security"))
                    .Attributes("EmployeeID").Value = objSecurityXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                End If
                .Attributes("Notes").Value = txtNotes.Text
                .Attributes("ApprovedBy").Value = hdEmpID.Value.Trim()
                .Attributes("ApprovalDate").Value = objeAAMS.ConvertTextDate(Request("txtAppovalDt"))

            End With
            Dim rowC As Integer = 0
            Dim ChildNodeClone As XmlNode
            Dim childNode As XmlNode = objInputXml.DocumentElement.SelectSingleNode("PURCHASEORDER/PURCHASEORDERDETAILS")
            Dim ParenNode As XmlNode = objInputXml.DocumentElement.SelectSingleNode("PURCHASEORDER")

            ChildNodeClone = childNode.CloneNode(True)
            ParenNode.RemoveChild(childNode)


            Dim counterGrid As Integer = 0
            Dim objxmlTotal As New XmlDocument


            If hdTempXml.Value Is Nothing Or hdTempXml.Value = "" Then
                lblError.Text = "Add Product Details"
                Exit Sub
            End If

            objxmlTotal.LoadXml(hdTempXml.Value)
            Dim xNodesT As XmlNode


            Dim bolChk As Boolean = objxmlTotal.DocumentElement.SelectSingleNode("PURCHASEORDER").HasChildNodes

            If Not bolChk Then
                lblError.Text = "Add Product Details"
                Exit Sub
            End If




            ' For counterGrid = 0 To grdvProduct.Rows.Count - 1
            For Each xNodesT In objxmlTotal.DocumentElement.SelectNodes("PURCHASEORDER/PURCHASEORDERDETAILS")
                ' <PURCHASEORDERDETAILS  PRODUCTID='' PRODUCTNAME='' Qty='' Rate='' Tax='' Warranty='
                ' WarrantyOnSite='' DeliveryDate='' GodownID='' GodownName=''/>
                'ChildNodeClone.Attributes("PRODUCTID").Value = CType(grdvProduct.Rows(counterGrid).FindControl("hdProductID"), HiddenField).Value.Trim()
                'ChildNodeClone.Attributes("PRODUCTNAME").Value = CType(grdvProduct.Rows(counterGrid).FindControl("lblProduct"), Label).Text.Trim()
                'ChildNodeClone.Attributes("Qty").Value = CType(grdvProduct.Rows(counterGrid).FindControl("lblQty"), Label).Text.Trim()
                'ChildNodeClone.Attributes("Rate").Value = CType(grdvProduct.Rows(counterGrid).FindControl("lblRate"), Label).Text.Trim()
                'ChildNodeClone.Attributes("Tax").Value = CType(grdvProduct.Rows(counterGrid).FindControl("bllTax"), Label).Text.Trim()
                'ChildNodeClone.Attributes("Warranty").Value = CType(grdvProduct.Rows(counterGrid).FindControl("lblWarrenty"), Label).Text.Trim()
                'ChildNodeClone.Attributes("WarrantyOnSite").Value = CType(grdvProduct.Rows(counterGrid).FindControl("lblWarrentyOnsite"), Label).Text.Trim()
                'ChildNodeClone.Attributes("DeliveryDate").Value = CType(grdvProduct.Rows(counterGrid).FindControl("lblDeliveryDt"), Label).Text.Trim()
                'ChildNodeClone.Attributes("GodownID").Value = CType(grdvProduct.Rows(counterGrid).FindControl("hdGodownID"), HiddenField).Value.Trim()
                ChildNodeClone.Attributes("PRODUCTID").Value = xNodesT.Attributes("PRODUCTID").Value.Trim()
                ' ChildNodeClone.Attributes("PRODUCTNAME").Value = xNodesT.Attributes("PRODUCTNAME").Value.Trim()
                ChildNodeClone.Attributes("Qty").Value = xNodesT.Attributes("Qty").Value.Trim()
                ChildNodeClone.Attributes("Rate").Value = xNodesT.Attributes("Rate").Value.Trim()
                ChildNodeClone.Attributes("Tax").Value = xNodesT.Attributes("Tax").Value.Trim()
                ChildNodeClone.Attributes("Warranty").Value = xNodesT.Attributes("Warranty").Value.Trim()
                ChildNodeClone.Attributes("WarrantyOnSite").Value = xNodesT.Attributes("WarrantyOnSite").Value.Trim()
                ChildNodeClone.Attributes("DeliveryDate").Value = objeAAMS.ConvertTextDate(xNodesT.Attributes("DeliveryDate").Value.Trim())
                ChildNodeClone.Attributes("GodownID").Value = xNodesT.Attributes("GodownID").Value.Trim()


                ParenNode.AppendChild(ChildNodeClone)
                ChildNodeClone = childNode.CloneNode(True)
            Next
            ' Next


            'For Removing Empty Records
            Dim objNode As XmlNode
            objNode = objInputXml.DocumentElement.SelectSingleNode("PURCHASEORDER/PURCHASEORDERDETAILS[@PRODUCTID='']")
            If objNode IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("PURCHASEORDER").RemoveChild(objNode)
            End If
            'End of Removing Records

            Dim bol As Boolean = objInputXml.DocumentElement.SelectSingleNode("PURCHASEORDER").HasChildNodes
            If Not bol Then
                lblError.Text = "Add Product Details"
                Exit Sub
            End If


            objOutputXml = objbzProductOrder.Update(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                'lblError.Text = objeAAMSMessage.messUpdate ' "Record updated successfully."
                Response.Redirect("INVUP_PurchaseOrder.aspx?MSG=M&Action=U&PurchaseID=" + objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("PURCHASEORDER").Attributes("PurchaseOrderID").Value.Trim()))
                'INVUP_PurchaseOrder.aspx?Action=U&PurchaseID="+PruchaseOrderID
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Response.Redirect("INVUP_PurchaseOrder.aspx?Action=I")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub EditProducts()
        hdTabType.Value = 1

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlNode As XmlNode
        objInputXml.LoadXml(hdTempXml.Value)
        objXmlNode = objInputXml.DocumentElement.SelectSingleNode("PURCHASEORDER/PURCHASEORDERDETAILS[@TempRow='" + hdEditFlag.Value + "']")
        '<PURCHASEORDERDETAILS TempRow='' PRODUCTID='' PRODUCTNAME='' Qty='' Rate='' Tax='
        ' Warranty='' WarrantyOnSite='' DeliveryDate='' GodownID='' GodownName=''/>
        Dim li As New ListItem
        li = drpProductName.Items.FindByText(objXmlNode.Attributes("PRODUCTNAME").Value.Trim())

        'Code for Inserting a Product in Product Dropdown

        ' drpDownList.Items.Add(li)

        ' li.Value = objXmlNode.Attributes("DESCRIPTION").Value, objXmlNode.Attributes("PRODUCTID").Value & "|" & objNode.Attributes("MAINTAIN_BALANCE_BY").Value & "|" & objNode.Attributes("MAINTAIN_BALANCE").Value & "|" & objNode.Attributes("EGROUP_CODE").Value
        If li Is Nothing Then
            Dim liNew As New ListItem(objXmlNode.Attributes("PRODUCTNAME").Value, objXmlNode.Attributes("PRODUCTID").Value & "|" & objXmlNode.Attributes("MAINTAIN_BALANCE_BY").Value & "|" & objXmlNode.Attributes("MAINTAIN_BALANCE").Value & "|MUKUND")
            drpProductName.Items.Insert(1, liNew)
            li = liNew
        End If




        drpProductName.SelectedValue = li.Value

        'Maintaining Uniqueness 
        hdProductUniqe.Value = li.Value
        'Maintaining Uniqueness 

        Dim str As String()
        str = drpProductName.SelectedValue.Trim().Split("|")
        If str(1).ToUpper() = "FALSE" And str(2).ToUpper() = "FALSE" Then
            txtQty.Enabled = False
            txtQty.CssClass = "textboxgrey"
            hdReadonlyFlag.Value = "-1"
        Else
            txtQty.Enabled = True
            txtQty.CssClass = "textbox"
        End If
        'If li IsNot Nothing Then
        '    If li.Selected = False Then
        '        li.Selected = True
        '    End If
        'End If
        '  drpProductName.Items.FindByValue(objXmlNode.Attributes("PRODUCTID").Value.Trim()).Selected = True
        txtQty.Text = objXmlNode.Attributes("Qty").Value.Trim()
        txtRates.Text = objXmlNode.Attributes("Rate").Value.Trim()
        txtTax.Text = objXmlNode.Attributes("Tax").Value.Trim()
        txtWarranty.Text = objXmlNode.Attributes("Warranty").Value.Trim()
        If objXmlNode.Attributes("WarrantyOnSite").Value.Trim().ToUpper() = "TRUE" Then
            chkOnsite.Checked = True
        Else
            chkOnsite.Checked = False
        End If
        txtDeliveryDt.Text = objXmlNode.Attributes("DeliveryDate").Value.Trim()
        drpGodown.SelectedValue = objXmlNode.Attributes("GodownID").Value.Trim()
        'Maintaining Uniqueness 
        hdGodownUnique.Value = objXmlNode.Attributes("GodownID").Value.Trim()
        'Maintaining Uniqueness 

        btnAdd.Text = "Update"
        UpdateRow = objXmlNode.Attributes("TempRow").Value.Trim()
        hdEditFlag.Value = ""
        lblError.Text = ""
        'objInputXml.DocumentElement.SelectSingleNode("PURCHASEORDER").RemoveChild(objXmlNode)
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.OriginalString)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvProduct_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvProduct.Sorting
        Try
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
            BindProductDetails(e.SortExpression.ToString)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub drpProductName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpProductName.SelectedIndexChanged

    End Sub
End Class
