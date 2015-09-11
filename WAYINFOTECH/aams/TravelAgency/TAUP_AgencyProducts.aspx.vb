
Partial Class TravelAgency_TAUP_AgencyProducts
    Inherits System.Web.UI.Page
#Region "Global Declaration Section"
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim counter As Integer = 0
    Dim imgDown As New Image
    Dim imgUp As New Image
    'Developed by Mukund
#End Region

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
            Session("PageName") = Request.Url.ToString

            btnAdd.Attributes.Add("onclick", "return ChkEmptyProductNameAgencyProduct();")
            lblError.Text = String.Empty


            '***************************************************************************************
            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgDown.ImageUrl = "~/Images/Sortdown.gif"
            '***************************************************************************************

            objeAAMS.ExpirePageCache()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If



            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency product']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency product']").Attributes("Value").Value)
                    If strBuilder(1) = "0" Then
                        btnSve.Enabled = False
                        btnAdd.Enabled = False
                    End If

                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                    End If

                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If



            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(drpProductList, "PRODUCTS", True, 1)
                Dim strLcode As String = Nothing
                ''Code For  Updating of AgencyProduct Product Details  
                ' DisplayProductList()
                If Request.QueryString("Action") Is Nothing Then
                    Session("AgencyProduct") = Nothing
                End If



                If Request.QueryString("Action") IsNot Nothing Then
                    If Request.QueryString("Action").Trim = "US" Then
                        lblError.Text = objeAAMSMessage.messUpdate
                    End If
                End If

                ViewState("SortName") = "PRODUCTNAME"
                ViewState("Desc") = "TRUE"
                DisplayProductListSort("PRODUCTNAME")



                If (Not Request.QueryString("Action") = Nothing) Then
                    If (Request.QueryString("Action") = "U") Then
                        If (Not Request.QueryString("TempRowno") = Nothing) Then
                            ' ViewIPPoolGroup method  called for binding the controls  
                            ViewAgencyProduct(Request.QueryString("TempRowno"))
                            btnAdd.Text = "Update"
                        End If

                    End If



                    If (Request.QueryString("Action") = "D") Then
                        If (Not Request.QueryString("TempRowno") = Nothing) Then
                            ' ViewIPPoolGroup method  called for binding the controls  
                            DeleteRecords(Request.QueryString("TempRowno"))
                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Private Sub ViewAgencyProduct(ByVal Rowno As Integer)

        Dim objInputXml As New XmlDocument
        Dim objIxml As New XmlDocument
        '        Dim objXMLNode As XmlNode
        lblError.Text = ""

        Try
            objIxml.LoadXml(Session("AgencyProduct"))

            If (objIxml.DocumentElement.SelectNodes("AGENCYPRODUCT[@TempRowCount='" & Rowno & "']").Count <> 0) Then
                'objXMLNode = objIxml.DocumentElement.SelectSingleNode("AGENCYPRODUCT[@TempRowCount='" & Rowno & "']")
                drpProductList.SelectedValue = objIxml.DocumentElement.SelectSingleNode("AGENCYPRODUCT[@TempRowCount='" & Rowno & "']").Attributes("PRODUCTID").Value
                txtInstallDate.Text = (objIxml.DocumentElement.SelectSingleNode("AGENCYPRODUCT[@TempRowCount='" & Rowno & "']").Attributes("DATE_INSTALLATION").Value)
                txtTerminalOnline.Text = objIxml.DocumentElement.SelectSingleNode("AGENCYPRODUCT[@TempRowCount='" & Rowno & "']").Attributes("TERMINALS_ONLINE").Value
            End If

        Catch ex As Exception

        End Try


    End Sub
    Private Sub DeleteRecords(ByVal RowNo As Integer)
        Try
            Dim objInputXml As New XmlDocument
            Dim objIxml As New XmlDocument
            Dim objOutputXml As New XmlDocument
            objIxml.LoadXml(Session("AgencyProduct"))
            Dim objNode As XmlNode
            Dim objNodeList As XmlNodeList
            objNodeList = objIxml.DocumentElement.SelectNodes("AGENCYPRODUCT")
            Dim counter As Integer = 0
            '            Dim strddRowno As HiddenField
            ' strddRowno = gvAgencyProduct.Rows(RowNo - 1).FindControl("hdRowNo")
            ' If strddRowno.Value = "" Then
            For Each objNode In objIxml.DocumentElement.SelectNodes("AGENCYPRODUCT")
                If (objNode.Attributes("ROWID").Value = "") Then
                    If (objNode.Attributes("TempRowCount").Value = RowNo) Then
                        objIxml.DocumentElement.RemoveChild(objNode)
                        lblError.Text = objeAAMSMessage.messDelete
                        Exit For
                    End If
                Else
                    If (objNode.Attributes("TempRowCount").Value = RowNo) Then
                        objNode.Attributes("Action").Value = "D"
                        lblError.Text = objeAAMSMessage.messDelete

                    End If
                End If
            Next
            ' Else
            ' End If
            counter = 1
            For Each objNode In objIxml.DocumentElement.SelectNodes("AGENCYPRODUCT")
                objNode.Attributes("TempRowCount").Value = counter
                counter += 1
            Next
            Session("AgencyProduct") = objIxml.OuterXml
            Session("TempRowNo") = counter - 1
            DisplayProductList()
            'Dim objXmlReader As XmlNodeReader
            'Dim ds As New DataSet
            'objXmlReader = New XmlNodeReader(objIxml)
            'ds.ReadXml(objXmlReader)
            'gvAgencyProduct.DataSource = ds.Tables("AGENCYPRODUCT").DefaultView
            'gvAgencyProduct.DataBind()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub


    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            AddRecordsTemprory()
            drpProductList.SelectedIndex = 0
            txtInstallDate.Text = ""
            txtTerminalOnline.Text = ""
            btnAdd.Text = "Add"
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub AddRecordsTemprory()

        Dim objInputXml As New XmlDocument
        Dim objIxml As New XmlDocument
        Dim objxmldocfrag As XmlDocumentFragment
        '        Dim objXmlReader As XmlReader
        Dim ds As New DataSet
        ' Dim objXMLNode As XmlNode
        Dim objNode As XmlNode
        Dim RowNo As Integer
        Try
            objIxml.LoadXml("<MS_UPDATEAGENCYPRODUCT_INPUT><AGENCYPRODUCT TempRowCount='' PRODUCTNAME=''  Action='' ROWID='' PRODUCTID='' LCODE='' TERMINALS_ONLINE='' DATE_INSTALLATION='' /></MS_UPDATEAGENCYPRODUCT_INPUT>")
            If Session("AgencyProduct") Is Nothing Then
                objInputXml.LoadXml("<MS_UPDATEAGENCYPRODUCT_INPUT></MS_UPDATEAGENCYPRODUCT_INPUT>")
            Else
                objInputXml.LoadXml(Convert.ToString(Session("AgencyProduct")))
            End If
            'If Not ViewState("TempRowNo") Is Nothing Then
            '    objXMLNode = objInputXml.DocumentElement.SelectSingleNode("AGENCYPRODUCT[@TempRowCount='" & ViewState("TempRowNo") & "']")
            '    objInputXml.DocumentElement.RemoveChild(objXMLNode)
            'End If
            If (Not Request.QueryString("Action") = Nothing) Then
                If (Request.QueryString("Action") = "U") Then
                    If (Not Request.QueryString("TempRowno") = Nothing) Then
                        RowNo = Convert.ToInt32(Request.QueryString("TempRowno"))
                        For Each objNode In objInputXml.DocumentElement.SelectNodes("AGENCYPRODUCT")
                            If (objNode.Attributes("TempRowCount").Value = RowNo) Then
                                If (objNode.Attributes("ROWID").Value = "") Then
                                    objNode.Attributes("Action").Value = "I"
                                    lblError.Text = objeAAMSMessage.messInsert
                                Else
                                    objNode.Attributes("Action").Value = "U"
                                    lblError.Text = objeAAMSMessage.messInsert
                                End If
                                ' objNode.Attributes("TempRowCount").Value = Convert.ToInt32(ViewState("TempRowNo")) + 1
                                objNode.Attributes("PRODUCTID").Value = drpProductList.SelectedValue
                                If Not Session("Action").ToString().Split("|").GetValue(1) Is Nothing Then
                                    objNode.Attributes("LCODE").Value = Session("Action").ToString().Split("|").GetValue(1)
                                Else
                                    lblError.Text = "Location Code Not Found"
                                End If
                                objNode.Attributes("TERMINALS_ONLINE").Value = txtTerminalOnline.Text.Trim()
                                objNode.Attributes("DATE_INSTALLATION").Value = (txtInstallDate.Text)
                                objNode.Attributes("PRODUCTNAME").Value = drpProductList.SelectedItem.Text
                                lblError.Text = objeAAMSMessage.messInsert
                                Exit For
                            End If
                        Next
                    End If
                End If
            End If
            If (Request.QueryString("Action") <> "U") Then
                With objIxml.DocumentElement.SelectSingleNode("AGENCYPRODUCT")
                    .Attributes("TempRowCount").Value = Convert.ToInt32(Session("TempRowNo")) + 1
                    .Attributes("Action").Value = "I"
                    .Attributes("PRODUCTID").Value = drpProductList.SelectedValue
                    If Not Session("Action").ToString().Split("|").GetValue(1) Is Nothing Then
                        .Attributes("LCODE").Value = Session("Action").ToString().Split("|").GetValue(1)
                    Else
                        lblError.Text = "Location Code Not Found"
                    End If
                    .Attributes("TERMINALS_ONLINE").Value = txtTerminalOnline.Text.Trim()
                    .Attributes("DATE_INSTALLATION").Value = (txtInstallDate.Text)
                    .Attributes("PRODUCTNAME").Value = drpProductList.SelectedItem.Text
                    lblError.Text = objeAAMSMessage.messInsert

                End With
                Session("TempRowNo") = Convert.ToInt32(Session("TempRowNo")) + 1
                objxmldocfrag = objInputXml.CreateDocumentFragment()
                objxmldocfrag.InnerXml = objIxml.DocumentElement.InnerXml
                objInputXml.DocumentElement.AppendChild(objxmldocfrag)
            End If



            Session("AgencyProduct") = objInputXml.OuterXml
            DisplayProductList()

            If (Not Request.QueryString("Action") = Nothing) Then
                If (Request.QueryString("Action") = "U") Then
                    Response.Redirect("TAUP_AgencyProducts.aspx?Id=6&Action=I", False)
                End If
            End If
            'AgencyComtitionReset()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub
    Private Sub DisplayProductList()
        Dim objInputXml, objOutputXml, objOutputXml2 As New XmlDocument
        Dim objbzAgencyProduct As New AAMS.bizTravelAgency.bzAgencyProduct
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objIAppendXml, objOAppendXml As New XmlDocument
        Dim objIXml As New XmlDocument
        Dim objxmldocfrag As XmlDocumentFragment
        Dim objNode As XmlNode
        Dim objNodeList As XmlNodeList
        '        Dim rowno As Integer
        Try

            If (Session("AgencyProduct") Is Nothing) Then
                objInputXml.LoadXml("<MS_GETAGENCYPRODUCT_INPUT><LCODE /></MS_GETAGENCYPRODUCT_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerXml = Session("Action").ToString().Split("|").GetValue(1)
                'Here Back end Method Call
                objOutputXml = objbzAgencyProduct.GetDetails(objInputXml)

                ' If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then


                objIAppendXml.LoadXml("<MS_UPDATEAGENCYPRODUCT_INPUT><AGENCYPRODUCT TempRowCount='' PRODUCTNAME=''  Action='' ROWID='' PRODUCTID='' LCODE='' TERMINALS_ONLINE='' DATE_INSTALLATION='' /></MS_UPDATEAGENCYPRODUCT_INPUT>")

                'If Session("AgencyProduct") Is Nothing Then
                objOAppendXml.LoadXml("<MS_UPDATEAGENCYPRODUCT_INPUT></MS_UPDATEAGENCYPRODUCT_INPUT>")
                'Else
                '    objOAppendXml.LoadXml(Convert.ToString(Session("AgencyProduct")))
                'End If

                objNodeList = objOutputXml.DocumentElement.SelectNodes("AGENCYPRODUCT")


                Dim counter As Integer = 0
                For Each objNode In objNodeList

                    'If objNode.Attributes("PRODUCTNAME").Value = "" Then
                    '    Continue For
                    'End If

                    counter = counter + 1
                    objIAppendXml.DocumentElement.SelectSingleNode("AGENCYPRODUCT").Attributes("TempRowCount").Value = counter

                    With objIAppendXml.DocumentElement.SelectSingleNode("AGENCYPRODUCT")
                        .Attributes("ROWID").Value = objNode.Attributes("ROWID").Value
                        .Attributes("PRODUCTID").Value = objNode.Attributes("PRODUCTID").Value
                        .Attributes("LCODE").Value = Session("Action").ToString().Split("|").GetValue(1)
                        .Attributes("TERMINALS_ONLINE").Value = objNode.Attributes("TERMINALS_ONLINE").Value
                        .Attributes("DATE_INSTALLATION").Value = objeAAMS.ConvertDateBlank(objNode.Attributes("DATE_INSTALLATION").InnerText)
                        .Attributes("PRODUCTNAME").Value = objNode.Attributes("PRODUCTNAME").Value
                        .Attributes("Action").Value = "U"
                    End With

                    objxmldocfrag = objOAppendXml.CreateDocumentFragment()
                    objxmldocfrag.InnerXml = objIAppendXml.DocumentElement.InnerXml
                    objOAppendXml.DocumentElement.AppendChild(objxmldocfrag)
                Next

                Session("AgencyProduct") = objOAppendXml.OuterXml
                Session("TempRowNo") = counter

                objXmlReader = New XmlNodeReader(objOAppendXml)
                ds.ReadXml(objXmlReader)
                gvAgencyProduct.DataSource = ds.Tables("AGENCYPRODUCT").DefaultView
                gvAgencyProduct.DataBind()
                'Else
                '    gvAgencyProduct.DataSource = ds.Tables("AGENCYPRODUCT").DefaultView
                '    gvAgencyProduct.DataBind()
                '    'lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                '   End If


            Else

                objOutputXml2.LoadXml(Session("AgencyProduct"))
                For Each objNode In objOutputXml2.DocumentElement.SelectNodes("AGENCYPRODUCT")
                    If (objNode.Attributes("ROWID").Value <> "") Then
                        If (objNode.Attributes("Action").Value = "D") Then
                            objOutputXml2.DocumentElement.RemoveChild(objNode)
                        End If
                    End If
                Next

                objIAppendXml.LoadXml(objOutputXml2.OuterXml)
                objXmlReader = New XmlNodeReader(objIAppendXml)

                ds.ReadXml(objXmlReader)
                'For rowno = 0 To ds.Tables("AGENCYPRODUCT").Rows.Count - 1
                '    ds.Tables("AGENCYPRODUCT").Rows(rowno)("DATE_INSTALLATION") = objeAAMS.ConvertDateBlank(Convert.ToInt32(ds.Tables("AGENCYPRODUCT").Rows(rowno)("DATE_INSTALLATION")))
                'Next
                If ds.Tables("AGENCYPRODUCT") IsNot Nothing Then
                    gvAgencyProduct.DataSource = ds.Tables("AGENCYPRODUCT").DefaultView
                    gvAgencyProduct.DataBind()
                Else
                    objOutputXml2.LoadXml("<MS_UPDATEAGENCYPRODUCT_INPUT><AGENCYPRODUCT TempRowCount='1' PRODUCTNAME='' Action='U' ROWID='' PRODUCTID='' LCODE='' TERMINALS_ONLINE='' DATE_INSTALLATION='' /> </MS_UPDATEAGENCYPRODUCT_INPUT>")
                    objIAppendXml.LoadXml(objOutputXml2.OuterXml)
                    objXmlReader = New XmlNodeReader(objIAppendXml)
                    ds.ReadXml(objXmlReader)
                    gvAgencyProduct.DataSource = ds.Tables("AGENCYPRODUCT").DefaultView
                    gvAgencyProduct.DataBind()
                End If


            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            ' objbzAgency = Nothing
            ds = Nothing
        End Try
    End Sub

    Protected Sub gvAgencyProduct_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAgencyProduct.RowDataBound

        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
        Dim str As String = CType(e.Row.FindControl("lblproductname"), Label).Text.Trim()    'e.Row.Cells(0).ToString()
        If (str.Trim().Length = 0) Then
            e.Row.Cells.Clear()
            Exit Sub
        End If

    End Sub

    Protected Sub btnSve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSve.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objtaAgencyProduct As New AAMS.bizTravelAgency.bzAgencyProduct
        Dim objNode As XmlNode
        Try
            If Not Session("AgencyProduct") Is Nothing Then
                objInputXml.LoadXml(Session("AgencyProduct"))
                'Here Back end Method Call
                For Each objNode In objInputXml.DocumentElement.SelectNodes("AGENCYPRODUCT")
                    If (objNode.Attributes("PRODUCTNAME").Value = "") Then
                        objInputXml.DocumentElement.RemoveChild(objNode)
                        Continue For
                    End If
                    objNode.Attributes("DATE_INSTALLATION").Value = objeAAMS.ConvertTextDate(objNode.Attributes("DATE_INSTALLATION").Value)
                Next

                objOutputXml = objtaAgencyProduct.Update(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    ' DisplayProductList()

                    Session("AgencyProduct") = Nothing
                    Session("TempRowNo") = Nothing

                    Response.Redirect("TAUP_AgencyProducts.aspx?Id=6&Action=US")

                    'lblError.Text = "Records Saved Successfully"

                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            Else

            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            objtaAgencyProduct = Nothing
        End Try

    End Sub

    Protected Sub gvAgencyProduct_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAgencyProduct.RowCommand

        If (e.CommandName = "DeleteX") Then

            Response.Redirect("TAUP_AgencyProducts.aspx?Id=6&Action=D&TempRowno=" & e.CommandArgument)

        End If

        If (e.CommandName = "EditX") Then
            Response.Redirect("TAUP_AgencyProducts.aspx?Id=6&Action=U&TempRowno=" & e.CommandArgument)

        End If


    End Sub

    Protected Sub gvAgencyProduct_RowUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdatedEventArgs) Handles gvAgencyProduct.RowUpdated

    End Sub

    Protected Sub gvAgencyProduct_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles gvAgencyProduct.RowDeleted

    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Session("AgencyProduct") = Nothing
        Session("TempRowNo") = Nothing
        'txtInstallDate.Text = ""
        'txtTerminalOnline.Text = ""
        'drpProductList.SelectedIndex = 0
        Response.Redirect("TAUP_AgencyProducts.aspx?Id=6", False)
    End Sub

    Protected Sub gvAgencyProduct_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvAgencyProduct.Sorting
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
            DisplayProductListSort(e.SortExpression.ToString)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DisplayProductListSort(ByVal strCol As String)
        Dim objInputXml, objOutputXml, objOutputXml2 As New XmlDocument
        Dim objbzAgencyProduct As New AAMS.bizTravelAgency.bzAgencyProduct
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objIAppendXml, objOAppendXml As New XmlDocument
        Dim objIXml As New XmlDocument
        Dim objxmldocfrag As XmlDocumentFragment
        Dim objNode As XmlNode
        Dim objNodeList As XmlNodeList
        '        Dim rowno As Integer
        Dim dveiw As DataView
        Try

            If (Session("AgencyProduct") Is Nothing) Then
                objInputXml.LoadXml("<MS_GETAGENCYPRODUCT_INPUT><LCODE /></MS_GETAGENCYPRODUCT_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerXml = Session("Action").ToString().Split("|").GetValue(1)
                'Here Back end Method Call
                objOutputXml = objbzAgencyProduct.GetDetails(objInputXml)

                ' If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then


                objIAppendXml.LoadXml("<MS_UPDATEAGENCYPRODUCT_INPUT><AGENCYPRODUCT TempRowCount='' PRODUCTNAME=''  Action='' ROWID='' PRODUCTID='' LCODE='' TERMINALS_ONLINE='' DATE_INSTALLATION='' /></MS_UPDATEAGENCYPRODUCT_INPUT>")

                'If Session("AgencyProduct") Is Nothing Then
                objOAppendXml.LoadXml("<MS_UPDATEAGENCYPRODUCT_INPUT></MS_UPDATEAGENCYPRODUCT_INPUT>")
                'Else
                '    objOAppendXml.LoadXml(Convert.ToString(Session("AgencyProduct")))
                'End If

                objNodeList = objOutputXml.DocumentElement.SelectNodes("AGENCYPRODUCT")


                Dim counter As Integer = 0
                For Each objNode In objNodeList

                    'If objNode.Attributes("PRODUCTNAME").Value = "" Then
                    '    Continue For
                    'End If

                    counter = counter + 1
                    objIAppendXml.DocumentElement.SelectSingleNode("AGENCYPRODUCT").Attributes("TempRowCount").Value = counter

                    With objIAppendXml.DocumentElement.SelectSingleNode("AGENCYPRODUCT")
                        .Attributes("ROWID").Value = objNode.Attributes("ROWID").Value
                        .Attributes("PRODUCTID").Value = objNode.Attributes("PRODUCTID").Value
                        .Attributes("LCODE").Value = Session("Action").ToString().Split("|").GetValue(1)
                        .Attributes("TERMINALS_ONLINE").Value = objNode.Attributes("TERMINALS_ONLINE").Value
                        .Attributes("DATE_INSTALLATION").Value = objeAAMS.ConvertDateBlank(objNode.Attributes("DATE_INSTALLATION").InnerText)
                        .Attributes("PRODUCTNAME").Value = objNode.Attributes("PRODUCTNAME").Value
                        .Attributes("Action").Value = "U"
                    End With

                    objxmldocfrag = objOAppendXml.CreateDocumentFragment()
                    objxmldocfrag.InnerXml = objIAppendXml.DocumentElement.InnerXml
                    objOAppendXml.DocumentElement.AppendChild(objxmldocfrag)
                Next

                Session("AgencyProduct") = objOAppendXml.OuterXml
                Session("TempRowNo") = counter

                objXmlReader = New XmlNodeReader(objOAppendXml)
                ds.ReadXml(objXmlReader)


                dveiw = ds.Tables("AGENCYPRODUCT").DefaultView
                dveiw.Sort = strCol

                gvAgencyProduct.DataSource = dveiw 'ds.Tables("AGENCYPRODUCT").DefaultView
                gvAgencyProduct.DataBind()
                'Else
                '    gvAgencyProduct.DataSource = ds.Tables("AGENCYPRODUCT").DefaultView
                '    gvAgencyProduct.DataBind()
                '    'lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                '   End If


            Else

                objOutputXml2.LoadXml(Session("AgencyProduct"))
                For Each objNode In objOutputXml2.DocumentElement.SelectNodes("AGENCYPRODUCT")
                    If (objNode.Attributes("ROWID").Value <> "") Then
                        If (objNode.Attributes("Action").Value = "D") Then
                            objOutputXml2.DocumentElement.RemoveChild(objNode)
                        End If
                    End If
                Next

                objIAppendXml.LoadXml(objOutputXml2.OuterXml)
                objXmlReader = New XmlNodeReader(objIAppendXml)

                ds.ReadXml(objXmlReader)
                'For rowno = 0 To ds.Tables("AGENCYPRODUCT").Rows.Count - 1
                '    ds.Tables("AGENCYPRODUCT").Rows(rowno)("DATE_INSTALLATION") = objeAAMS.ConvertDateBlank(Convert.ToInt32(ds.Tables("AGENCYPRODUCT").Rows(rowno)("DATE_INSTALLATION")))
                'Next
                dveiw = ds.Tables("AGENCYPRODUCT").DefaultView
                If ViewState("Desc") = "FALSE" Then
                    strCol &= " "
                    strCol &= "ASC"
                End If
                If ViewState("Desc") = "TRUE" Then
                    strCol &= " "
                    strCol &= "DESC"
                End If

                dveiw.Sort = strCol

                Dim dTable As New DataTable



                gvAgencyProduct.DataSource = dveiw ' ds.Tables("AGENCYPRODUCT").DefaultView
                gvAgencyProduct.DataBind()


                Dim intcol As Integer = GetSortColumnIndex(gvAgencyProduct)

                If ViewState("Desc") = "FALSE" Then
                    gvAgencyProduct.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If

                If ViewState("Desc") = "TRUE" Then
                    gvAgencyProduct.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If

            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            ' objbzAgency = Nothing
            ds = Nothing
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

End Class
