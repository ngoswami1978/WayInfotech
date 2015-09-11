Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml

Partial Class Setup_MSUP_MultipleOrderBooking
    Inherits System.Web.UI.Page
    Implements System.Web.UI.ICallbackEventHandler

    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Protected strIndex As String
    Protected flagPermission As String
    Dim objED As New EncyrptDeCyrpt
    Shared counter As Integer
    Shared UpdateRow As Integer
    Shared totalQty As Double = 0
    Dim imgDown As New Image
    Dim imgUp As New Image
    Dim strResult As String = ""

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        totalQty = 0
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        btnAdd.Attributes.Add("onclick", "return StyleValidation();")

        '***************************************************************************************
        imgUp.ImageUrl = "~/Images/Sortup.gif"
        imgDown.ImageUrl = "~/Images/Sortdown.gif"
        '***************************************************************************************

        Try
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If Session("EmployeePageName") IsNot Nothing Then

            End If
            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Booking']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Booking']").Attributes("Value").Value)
                End If
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
                ViewState("SortName") = "StyleName"
                ViewState("Desc") = "TRUE"
                'End of Sorting
                BindDropDown(drpQuality, "Quality", True, 1)
                BindDropDown(drpDesign, "Design", True, 1)
                BindDropDown(drpShadeNo, "ShadeNo", True, 1)

                If Request.QueryString("Action") IsNot Nothing Then
                    If Request.QueryString("Action") = "U" Then
                        If Request.QueryString("W_StyleOrderID") IsNot Nothing Then
                            ViewStyleDetails()
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

            'Client Call Back Techniq Applied In case of any drop down value Selection
            Dim m As ClientScriptManager = Me.ClientScript
            Dim strCallback As String = ""
            'lblError.Text = ""

            strResult = m.GetCallbackEventReference(Me, "args", "ReceiveServerData", "'this is context from server'")
            strCallback = "function CallServerSearch(args,context){" + strResult + ";}"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServerSearch", strCallback, True)
            drpQuality.Attributes.Add("onchange", "return FillSecurity('drpQuality');")
            drpDesign.Attributes.Add("onchange", "return FillSecurity('drpDesign');")
            drpShadeNo.Attributes.Add("onchange", "return FillSecurity('drpShadeNo');")

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
        Return strResult
    End Function

    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
        Dim id As String
        id = eventArgument.ToString()
        FillSubSecurity(id.Split("|").GetValue(0), "AutoCallBack")

    End Sub
    Public Sub FillSubSecurity(ByVal strW_StyleId As String, Optional ByVal strType As String = "Manual")
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim strXml As String = ""
        Dim objbzItemMaster As New WAY.bizMaster.bzStyle
        Dim args1 As String = ""
        Dim final As String = ""
        Dim row As DataRow

        objInputXml.LoadXml("<MS_VIEWSTYLE_INPUT><W_StyleId></W_StyleId></MS_VIEWSTYLE_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("W_StyleId").InnerText = strW_StyleId.ToString()

        Try
            objOutputXml = objbzItemMaster.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                If strType = "Manual" Then
                    drpDesign.Items.Clear()
                    drpQuality.SelectedValue = 1
                    drpDesign.SelectedValue = 1
                    drpShadeNo.SelectedValue = 1
                Else
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    For Each row In ds.Tables("STYLE").Rows
                        args1 = row("W_StyleId") + "|" + row("MRP")
                        final = args1 + final
                    Next row
                End If
                strResult = final
            Else
                strResult = ""
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub grdvProduct_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvProduct.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If

            Dim hdDelFlag As HiddenField
            Dim btnEdit As LinkButton
            Dim btnDelete As LinkButton
            Dim qty

            hdDelFlag = CType(e.Row.FindControl("hdRowID"), HiddenField)
            btnEdit = CType(e.Row.FindControl("linkEdit"), LinkButton)
            btnDelete = CType(e.Row.FindControl("linkDelete"), LinkButton)
            qty = CType(e.Row.FindControl("lblQty"), Label).Text.Trim()

            totalQty += Convert.ToDouble(Val(qty & ""))

            btnDelete.Attributes.Add("OnClick", "javascript:return DeleteFunction('" + hdDelFlag.Value + "');")
            btnEdit.Attributes.Add("OnClick", "javascript:return EditFunction('" + hdDelFlag.Value + "');")
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
            BindStyleDetails(e.SortExpression.ToString)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbzOrderBooking As New WAY.bizMaster.bzStyle

            objInputXml.LoadXml("<INV_UPDATE_W_ORDER_INPUT><ORDERHEADER W_StyleOrderID='' Totqty='' Remarks='' OrderDate='' LoggedBy='' LoggedByName='' OrderNumber=''><ORDERDETAILS W_StyleOrderID='' qty='' W_StyleId='' StyleName='' DesignNo='' ShadeNo='' MRP='' /></ORDERHEADER></INV_UPDATE_W_ORDER_INPUT>")

            With objInputXml.DocumentElement.SelectSingleNode("ORDERHEADER")
                If Request.QueryString("Action") IsNot Nothing Then
                    If Request.QueryString("Action") = "U" Then
                        .Attributes("W_StyleOrderID").Value = objED.Decrypt(Request.QueryString("W_StyleOrderID").Trim())
                    Else
                        .Attributes("W_StyleOrderID").Value = ""
                    End If
                End If

                .Attributes("Totqty").Value = txtTotQty.Text.Trim()
                .Attributes("OrderDate").Value = objeAAMS.ConvertTextDate(Now().ToShortDateString())

                If Session("Security") IsNot Nothing Then
                    Dim objSecurityXml As New XmlDocument
                    objSecurityXml.LoadXml(Session("Security"))
                    hdEmpID.Value = objSecurityXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                End If
                .Attributes("LoggedBy").Value = hdEmpID.Value.Trim()
            End With

            Dim rowC As Integer = 0
            Dim ChildNodeClone As XmlNode
            Dim childNode As XmlNode = objInputXml.DocumentElement.SelectSingleNode("ORDERHEADER/ORDERDETAILS")
            Dim ParenNode As XmlNode = objInputXml.DocumentElement.SelectSingleNode("ORDERHEADER")

            ChildNodeClone = childNode.CloneNode(True)
            ParenNode.RemoveChild(childNode)

            Dim counterGrid As Integer = 0
            Dim objxmlTotal As New XmlDocument

            If hdTempXml.Value Is Nothing Or hdTempXml.Value = "" Then
                lblError.Text = "Add Quality/Design Details"
                Exit Sub
            End If

            objxmlTotal.LoadXml(hdTempXml.Value)
            Dim xNodesT As XmlNode

            Dim bolChk As Boolean = objxmlTotal.DocumentElement.SelectSingleNode("ORDERHEADER").HasChildNodes

            If Not bolChk Then
                lblError.Text = "Add Quality/Design Details"
                Exit Sub
            End If

            For Each xNodesT In objxmlTotal.DocumentElement.SelectNodes("ORDERHEADER/ORDERDETAILS")
                ChildNodeClone.Attributes("W_StyleOrderID").Value = xNodesT.Attributes("W_StyleOrderID").Value.Trim()
                ChildNodeClone.Attributes("qty").Value = xNodesT.Attributes("qty").Value.Trim()
                ChildNodeClone.Attributes("W_StyleId").Value = xNodesT.Attributes("W_StyleId").Value.Trim()
                
                ParenNode.AppendChild(ChildNodeClone)
                ChildNodeClone = childNode.CloneNode(True)
            Next
            'For Removing Empty Records
            Dim objNode As XmlNode
            objNode = objInputXml.DocumentElement.SelectSingleNode("ORDERHEADER/ORDERDETAILS[@W_StyleOrderID='']")
            If objNode IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("ORDERHEADER").RemoveChild(objNode)
            End If
            'End of Removing Records

            Dim bol As Boolean = objInputXml.DocumentElement.SelectSingleNode("ORDERHEADER").HasChildNodes
            If Not bol Then
                lblError.Text = "Add Quality/Design Details"
                Exit Sub
            End If

            objOutputXml = objbzOrderBooking.UpdateOrderBooking(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Response.Redirect("MSUP_MultipleOrderBooking.aspx?MSG=M&Action=U&W_StyleOrderID=" + objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("ORDERHEADER").Attributes("W_StyleOrderID").Value.Trim()))
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim objoutXml As New XmlDocument
            Dim AppendedXml As New XmlDocument
            Dim ChildNodeClone As XmlNode

            If btnAdd.Text.Trim().ToUpper() = "UPDATE" Then
                AppendedXml.LoadXml(hdTempXml.Value)

                'Code for Restricting unique Godown and Product                
                Dim intProductCounter As Int16 = AppendedXml.DocumentElement.SelectNodes("ORDERHEADER/ORDERDETAILS[@StyleName='" & drpQuality.SelectedValue.Trim() & "'][@DesignNo='" & drpDesign.SelectedValue & "']").Count
                If intProductCounter > 1 Then
                    lblError.Text = "This Design is Already Added to List/ Please select other Design"
                    Exit Sub
                End If

                'Code for Restricting unique Style
                Dim childNode As XmlNode = AppendedXml.DocumentElement.SelectSingleNode("ORDERHEADER/ORDERDETAILS")
                Dim ParenNode As XmlNode = AppendedXml.DocumentElement.SelectSingleNode("ORDERHEADER")
                ChildNodeClone = childNode.CloneNode(True)

                ChildNodeClone.Attributes("W_StyleId").Value = drpQuality.SelectedValue.Trim()
                ChildNodeClone.Attributes("StyleName").Value = drpQuality.SelectedItem.Text.Trim()
                ChildNodeClone.Attributes("DesignNo").Value = drpDesign.SelectedItem.Text.Trim()
                ChildNodeClone.Attributes("ShadeNo").Value = drpShadeNo.SelectedItem.Text.Trim()
                ChildNodeClone.Attributes("qty").Value = txtQty.Text.Trim()
                If txtMrp.Text.Trim() = "" Then
                    ChildNodeClone.Attributes("MRP").Value = hdRunTimeQty.Value
                Else
                    ChildNodeClone.Attributes("MRP").Value = txtMrp.Text.Trim()
                End If
                Dim objXmlNodeOld As XmlNode
                objXmlNodeOld = AppendedXml.DocumentElement.SelectSingleNode("ORDERHEADER/ORDERDETAILS[@TempRow='" + UpdateRow.ToString() + "']")
                ChildNodeClone.Attributes("TempRow").Value = objXmlNodeOld.Attributes("TempRow").Value.Trim()

                AppendedXml.DocumentElement.SelectSingleNode("ORDERHEADER").ReplaceChild(ChildNodeClone, objXmlNodeOld)
                hdTempXml.Value = AppendedXml.OuterXml

                Dim intProductCounterOld As Int16 = AppendedXml.DocumentElement.SelectNodes("ORDERHEADER/ORDERDETAILS[@StyleName='" & drpQuality.SelectedItem.Text.Trim() & "'][@DesignNo='" & drpDesign.SelectedItem.Text.Trim() & "']").Count
                If intProductCounterOld > 1 Then
                    lblError.Text = "This Design is Already Added to List/ Please select other Design"
                    AppendedXml.DocumentElement.SelectSingleNode("PURCHASEORDER").ReplaceChild(objXmlNodeOld, ChildNodeClone) '   .AppendChild(ChildNodeClone)
                    hdTempXml.Value = AppendedXml.OuterXml
                    Exit Sub
                End If

                'BindProductDetails()
                btnAdd.Text = "Add"
                hdEditFlag.Value = ""
                lblError.Text = "Records Updated"

                'Binding Due to Removing Scraped Items from List
                drpQuality.Items.Clear()
                drpDesign.Items.Clear()
                drpShadeNo.Items.Clear()

                BindDropDown(drpQuality, "Quality", True, 1)
                BindDropDown(drpDesign, "Design", True, 1)
                BindDropDown(drpShadeNo, "ShadeNo", True, 1)
            ElseIf hdTempXml.Value <> "" Then
                AppendedXml.LoadXml(hdTempXml.Value)

                'Code for Restricting unique Godown and Product
                Dim objuniqProductNode As XmlNode
                '  Dim objuniqGodownNode As XmlNode
                objuniqProductNode = AppendedXml.DocumentElement.SelectSingleNode("ORDERHEADER/ORDERDETAILS[@StyleName='" & drpQuality.SelectedItem.Text.Trim() & "'][@DesignNo='" & drpDesign.SelectedItem.Text.Trim() & "']")
                If objuniqProductNode IsNot Nothing Then
                    lblError.Text = "This Design is Already Added to List/ Please select other Design"
                    Exit Sub
                End If

                'Code for Restricting unique Godown and Product
                Dim childNode As XmlNode = AppendedXml.DocumentElement.SelectSingleNode("ORDERHEADER/ORDERDETAILS")

                'Segment for adding a blank chlid Node
                If childNode Is Nothing Then
                    AppendedXml.LoadXml("<INV_VIEW_W_STYLEORDER_OUTPUT><ORDERHEADER W_StyleOrderID='' Totqty='' Remarks='' OrderDate='' LoggedBy='' LoggedByName='' OrderNumber=''><ORDERDETAILS TempRow='' W_StyleOrderID='' qty='' W_StyleId='' StyleName='' DesignNo='' ShadeNo='' MRP='' /></ORDERHEADER><Errors Status=''><Error Code='' Description='' /></Errors></INV_VIEW_W_STYLEORDER_OUTPUT>")
                    childNode = AppendedXml.DocumentElement.SelectSingleNode("ORDERHEADER/ORDERDETAILS")
                End If

                Dim ParenNode As XmlNode = AppendedXml.DocumentElement.SelectSingleNode("ORDERHEADER")

                'Segment for adding a blank chlid Node  End
                ChildNodeClone = childNode.CloneNode(True)
                ChildNodeClone.Attributes("W_StyleId").Value = drpQuality.SelectedValue.Trim()
                ChildNodeClone.Attributes("StyleName").Value = drpQuality.SelectedItem.Text.Trim()
                ChildNodeClone.Attributes("DesignNo").Value = drpDesign.SelectedItem.Text.Trim()
                ChildNodeClone.Attributes("ShadeNo").Value = drpShadeNo.SelectedItem.Text.Trim()
                ChildNodeClone.Attributes("qty").Value = txtQty.Text.Trim()

                If txtMrp.Text.Trim() = "" Then
                    ChildNodeClone.Attributes("MRP").Value = hdRunTimeQty.Value
                Else
                    ChildNodeClone.Attributes("MRP").Value = txtMrp.Text.Trim()
                End If

                counter = counter + 1
                ChildNodeClone.Attributes("TempRow").Value = counter
                AppendedXml.DocumentElement.SelectSingleNode("ORDERHEADER").AppendChild(ChildNodeClone)
                hdTempXml.Value = AppendedXml.OuterXml
                lblError.Text = "Records Added"
            Else
                AppendedXml.LoadXml("<INV_VIEW_W_STYLEORDER_OUTPUT><ORDERHEADER W_StyleOrderID='' Totqty='' Remarks='' OrderDate='' LoggedBy='' LoggedByName='' OrderNumber=''><ORDERDETAILS TempRow='' W_StyleOrderID='' qty='' W_StyleId='' StyleName='' DesignNo='' ShadeNo='' MRP='' /></ORDERHEADER><Errors Status=''><Error Code='' Description='' /></Errors></INV_VIEW_W_STYLEORDER_OUTPUT>")
                Dim childNode As XmlNode = AppendedXml.DocumentElement.SelectSingleNode("ORDERHEADER/ORDERDETAILS")
                Dim ParenNode As XmlNode = AppendedXml.DocumentElement.SelectSingleNode("ORDERHEADER")
                ChildNodeClone = childNode.CloneNode(True)

                ChildNodeClone.Attributes("W_StyleId").Value = drpQuality.SelectedValue.Trim()
                ChildNodeClone.Attributes("StyleName").Value = drpQuality.SelectedItem.Text.Trim()
                ChildNodeClone.Attributes("DesignNo").Value = drpDesign.SelectedItem.Text.Trim()
                ChildNodeClone.Attributes("ShadeNo").Value = drpShadeNo.SelectedItem.Text.Trim()
                ChildNodeClone.Attributes("qty").Value = txtQty.Text.Trim()
                If txtMrp.Text.Trim() = "" Then
                    ChildNodeClone.Attributes("MRP").Value = hdRunTimeQty.Value
                Else
                    ChildNodeClone.Attributes("MRP").Value = txtMrp.Text.Trim()
                End If
                counter = counter + 1
                ChildNodeClone.Attributes("TempRow").Value = counter
                AppendedXml.DocumentElement.SelectSingleNode("ORDERHEADER").AppendChild(ChildNodeClone)
                hdTempXml.Value = AppendedXml.OuterXml
                lblError.Text = "Records Added"
            End If

            drpQuality.SelectedIndex = 0
            drpDesign.SelectedIndex = 0
            drpShadeNo.SelectedIndex = 0
            txtMrp.Text = ""
            txtQty.Text = ""
            txtQty.ReadOnly = False
            txtQty.CssClass = "textbox"

            hdStyleUniqe.Value = ""

            Dim strCol As String
            If ViewState("SortName") IsNot Nothing Then
                strCol = ViewState("SortName").ToString()
            Else
                strCol = "StyleName"
            End If

            BindStyleDetails(strCol)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub ViewStyleDetails()
        Try
            Dim objInputOrderXml, objOutputOrderXml As New XmlDocument
            Dim objbzStyleOrder As New WAY.bizMaster.bzStyle

            objInputOrderXml.LoadXml("<INV_VIEW_W_STYLEORDER_INPUT><W_StyleOrderID></W_StyleOrderID></INV_VIEW_W_STYLEORDER_INPUT>")
            objInputOrderXml.DocumentElement.SelectSingleNode("W_StyleOrderID").InnerText = objED.Decrypt(Request.QueryString("W_StyleOrderID").Trim())
            txtOrderNo.Text = objED.Decrypt(Request.QueryString("W_StyleOrderID").Trim())

            objOutputOrderXml = objbzStyleOrder.OrderView(objInputOrderXml)

            If objOutputOrderXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                With objOutputOrderXml.DocumentElement.SelectSingleNode("ORDERHEADER")
                    hdStyleID.Value = .Attributes("W_StyleOrderID").Value.Trim()
                    txtCreatedBy.Text = .Attributes("LoggedByName").Value.Trim() 'check
                    txtOrderDate.Text = .Attributes("OrderDate").Value.Trim() 'check
                    hdCreatedby.Value = .Attributes("LoggedBy").Value.Trim()
                    hdEmpID.Value = .Attributes("LoggedBy").Value.Trim()
                End With
            End If

            Dim objAppendXml As New XmlDocument
            objAppendXml.LoadXml("<INV_VIEW_W_STYLEORDER_OUTPUT><ORDERHEADER W_StyleOrderID='' Totqty='' Remarks='' OrderDate='' LoggedBy='' LoggedByName='' OrderNumber=''><ORDERDETAILS TempRow='' W_StyleOrderID='' qty='' W_StyleId='' StyleName='' DesignNo='' ShadeNo='' MRP='' /></ORDERHEADER><Errors Status=''><Error Code='' Description='' /></Errors></INV_VIEW_W_STYLEORDER_OUTPUT>")

            With objAppendXml.DocumentElement.SelectSingleNode("ORDERHEADER")
                .Attributes("W_StyleOrderID").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("ORDERHEADER").Attributes("W_StyleOrderID").Value
                .Attributes("Totqty").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("ORDERHEADER").Attributes("Totqty").Value
                .Attributes("Remarks").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("ORDERHEADER").Attributes("Remarks").Value
                .Attributes("OrderDate").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("ORDERHEADER").Attributes("OrderDate").Value
                .Attributes("LoggedBy").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("ORDERHEADER").Attributes("LoggedBy").Value
                .Attributes("LoggedByName").Value = objOutputOrderXml.DocumentElement.SelectSingleNode("ORDERHEADER").Attributes("LoggedByName").Value
            End With
            Dim objNodeList As XmlNodeList
            Dim objNode, ChildNodeClone As XmlNode
            objNodeList = objOutputOrderXml.DocumentElement.SelectNodes("ORDERHEADER/ORDERDETAILS")


            Dim childNode As XmlNode = objAppendXml.DocumentElement.SelectSingleNode("ORDERHEADER/ORDERDETAILS")
            Dim ParenNode As XmlNode = objAppendXml.DocumentElement.SelectSingleNode("ORDERHEADER")
            ChildNodeClone = childNode.CloneNode(True)
            ParenNode.RemoveChild(childNode)
            counter = 0
            For Each objNode In objNodeList
                ChildNodeClone.Attributes("W_StyleOrderID").Value = objNode.Attributes("W_StyleOrderID").Value
                ChildNodeClone.Attributes("qty").Value = objNode.Attributes("qty").Value
                ChildNodeClone.Attributes("W_StyleId").Value = objNode.Attributes("W_StyleId").Value
                ChildNodeClone.Attributes("StyleName").Value = objNode.Attributes("StyleName").Value
                ChildNodeClone.Attributes("DesignNo").Value = objNode.Attributes("DesignNo").Value
                ChildNodeClone.Attributes("ShadeNo").Value = objNode.Attributes("ShadeNo").Value
                ChildNodeClone.Attributes("MRP").Value = objNode.Attributes("MRP").Value

                counter = counter + 1
                ChildNodeClone.Attributes("TempRow").Value = counter
                ParenNode.AppendChild(ChildNodeClone)
                ChildNodeClone = childNode.CloneNode(True)
            Next

            hdTempXml.Value = objAppendXml.OuterXml

            BindStyleDetails("StyleName")

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub BindStyleDetails(ByVal strrColName As String)
        Try
            Dim objNewXml As New XmlDataDocument
            If hdTempXml.Value <> "" Then
                objNewXml.LoadXml(hdTempXml.Value)
                Dim objNode As XmlNode
                objNode = objNewXml.DocumentElement.SelectSingleNode("ORDERHEADER/ORDERDETAILS[@W_StyleOrderID='']")
                If objNode IsNot Nothing Then
                    objNewXml.DocumentElement.SelectSingleNode("ORDERHEADER").RemoveChild(objNode)
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

                If dSet.Tables("ORDERDETAILS") IsNot Nothing Then
                    If dSet.Tables("ORDERDETAILS").Rows.Count > 0 Then
                        dSet.Tables("ORDERDETAILS").DefaultView.Sort = strrColName

                        grdvProduct.DataSource = dSet.Tables("ORDERDETAILS")
                        grdvProduct.DataBind()

                        Dim intcol As Integer = GetSortColumnIndex(grdvProduct)

                        If ViewState("Desc") = "FALSE" Then
                            grdvProduct.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                        End If

                        If ViewState("Desc") = "TRUE" Then
                            grdvProduct.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                        End If
                    End If
                    txtTotQty.Text = Format(totalQty, "0.00")
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
    Private Sub EditProducts()

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlNode As XmlNode
        objInputXml.LoadXml(hdTempXml.Value)

        objXmlNode = objInputXml.DocumentElement.SelectSingleNode("ORDERHEADER/ORDERDETAILS[@TempRow='" + hdEditFlag.Value + "']")
        '-- Style Drop Down Working --------------------------------------------------------------------------------------------------------
        Dim liSty As New ListItem
        liSty = drpQuality.Items.FindByText(objXmlNode.Attributes("StyleName").Value.Trim())

        If liSty Is Nothing Then
            Dim liNew As New ListItem(objXmlNode.Attributes("StyleName").Value, objXmlNode.Attributes("W_StyleId").Value)
            drpQuality.Items.Insert(1, liNew)
            liSty = liNew
        End If
        drpQuality.SelectedValue = liSty.Value
        'Maintaining Uniqueness 
        hdStyleUniqe.Value = liSty.Value
        'Maintaining Uniqueness 
        '-- Style Drop Down Working --------------------------------------------------------------------------------------------------------

        '-- Design Drop Down Working --------------------------------------------------------------------------------------------------------
        Dim liDesign As New ListItem
        liDesign = drpDesign.Items.FindByText(objXmlNode.Attributes("DesignNo").Value.Trim())

        If liDesign Is Nothing Then
            Dim liNew As New ListItem(objXmlNode.Attributes("DesignNo").Value, objXmlNode.Attributes("W_StyleId").Value)
            drpDesign.Items.Insert(1, liNew)
            liDesign = liNew
        End If
        drpDesign.SelectedValue = liDesign.Value
        '-- Design Drop Down Working --------------------------------------------------------------------------------------------------------

        '-- Design Drop Down Working --------------------------------------------------------------------------------------------------------
        Dim liShadeNo As New ListItem
        liShadeNo = drpShadeNo.Items.FindByText(objXmlNode.Attributes("ShadeNo").Value.Trim())

        If liShadeNo Is Nothing Then
            Dim liNew As New ListItem(objXmlNode.Attributes("ShadeNo").Value, objXmlNode.Attributes("W_StyleId").Value)
            drpShadeNo.Items.Insert(1, liNew)
            liShadeNo = liNew
        End If
        drpShadeNo.SelectedValue = liDesign.Value
        '-- Design Drop Down Working --------------------------------------------------------------------------------------------------------

        txtMrp.Text = objXmlNode.Attributes("MRP").Value.Trim()
        txtQty.Text = objXmlNode.Attributes("qty").Value.Trim()

        btnAdd.Text = "Update"
        UpdateRow = objXmlNode.Attributes("TempRow").Value.Trim()
        hdEditFlag.Value = ""
        lblError.Text = ""

    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.OriginalString)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub DeleteProduct()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objXmlNode As XmlNode
            objInputXml.LoadXml(hdTempXml.Value)
            If objInputXml.DocumentElement.SelectNodes("ORDERHEADER/ORDERDETAILS").Count > 1 Then
                objXmlNode = objInputXml.DocumentElement.SelectSingleNode("ORDERHEADER/ORDERDETAILS[@TempRow='" + hdDeleteFlag.Value + "']")
                objInputXml.DocumentElement.SelectSingleNode("ORDERHEADER").RemoveChild(objXmlNode)
                lblError.Text = "Records Removed"
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
            txtTotQty.Text = "0.0"
            BindStyleDetails(strCol)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Public Sub BindDropDown(ByVal drpDownList As DropDownList, ByVal strType As String, ByVal bolSelect As Boolean, Optional ByVal SelectOption As Integer = 1)
        Dim objOutputXml As XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim objXml As New XmlDocument
        Dim strInput As String = "<UP_LIST_STYLE_INPUT><TYPE></TYPE></UP_LIST_STYLE_INPUT>"

        '2- STYLENAME /QUALITY , 
        '3- DESIGNNO, 
        '4- SHADENO
        objXml.LoadXml(strInput)

        Dim ds As New DataSet
        Select Case strType
            Case "Quality"
                Dim ObjbzAgencyType As New WAY.bizMaster.bzStyle
                objOutputXml = New XmlDocument
                objXml.DocumentElement.SelectSingleNode("TYPE").InnerText = "2"
                objOutputXml = ObjbzAgencyType.List(objXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("STYLE")
                    drpDownList.DataTextField = "VALUE"
                    drpDownList.DataValueField = "ID"
                    drpDownList.DataBind()
                End If

            Case "Design"
                Dim ObjbzAgencyType As New WAY.bizMaster.bzStyle
                objOutputXml = New XmlDocument
                objXml.DocumentElement.SelectSingleNode("TYPE").InnerText = "3"
                objOutputXml = ObjbzAgencyType.List(objXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)

                    drpDownList.DataSource = ds.Tables("STYLE")
                    drpDownList.DataTextField = "VALUE"
                    drpDownList.DataValueField = "ID"
                    drpDownList.DataBind()
                End If

            Case "ShadeNo"
                Dim ObjbzAgencyType As New WAY.bizMaster.bzStyle
                objOutputXml = New XmlDocument
                objXml.DocumentElement.SelectSingleNode("TYPE").InnerText = "4"
                objOutputXml = ObjbzAgencyType.List(objXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("STYLE")
                    drpDownList.DataTextField = "VALUE"
                    drpDownList.DataValueField = "ID"
                    drpDownList.DataBind()
                End If
        End Select
        If SelectOption = 1 Then
            drpDownList.Items.Insert(0, New ListItem("--Select One--", "")) ' In Search/New/Edit Form For Mandatory Field
        End If

    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Response.Redirect("MSUP_MultipleOrderBooking.aspx?Action=I")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
