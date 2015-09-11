
Partial Class Incentive_INVUP_EquipmentCategory
    Inherits System.Web.UI.Page

#Region " Page Level Variables/ Objects"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim objEn As New EncyrptDeCyrpt

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


#Region " Page_Load Event"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            lblError.Text = String.Empty
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            ' #######################################
            ' ########## This Code code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Equipment Category']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Equipment Category']").Attributes("Value").Value)

                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                    End If

                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                        'btnSave.Enabled = False
                    End If
                    If (Request.QueryString("Action") = "U") Then
                        If strBuilder(2) = "0" Then
                            btnSave.Enabled = False
                        End If
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            ' #######################################
            ' ########## End of code used for enable/disable 
            ' ########## the button according to rights
            'btnReset.Attributes.Add("onclick", "return SecRegionReset();")
            btnNew.Attributes.Add("onclick", "return NewMSUPEquipmentCateg();")
            btnSave.Attributes.Add("onclick", "return CheckMandatoty();")


            If Not IsPostBack Then
                objeAAMS.BindDropDown(drpCountry, "COUNTRY", True, 1)
            End If


            Dim objbzSecurityRegion As New AAMS.bizIncetive.bzEquipment

            If hdProductListPopUpPage.Value <> "" Then

                FillProductPopup()
              
            End If

            If Not Page.IsPostBack Then
                '  ListEquipMentProductDesc()
                ChkUnitCostMan.Checked = True
                If (Request.QueryString("Action") = "I") Then
                    ChkUnitCostMan.Checked = True
                End If

                '*******************************************************************
                ''Code For 
                '*******************************************************************
                ' Load Data For Editing Mode

             

                If (Request.QueryString("Action") = "U") Then
                    ViewEquipMentCategory()
                    If (Request.QueryString("Msg") = "A") Then
                        lblError.Text = objeAAMSMessage.messInsert
                    End If
                    If (Request.QueryString("Msg") = "U") Then
                        lblError.Text = objeAAMSMessage.messUpdate
                    End If
                End If

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

#End Region
    '#Region " Show List of Aoffice"
    '    Sub ListEquipMentProductDesc()
    '        Try
    '            Dim objbzEquipment As New AAMS.bizIncetive.bzEquipment
    '            Dim objInputXml, objOutputXml As New XmlDocument
    '            Dim objXmlReader As XmlNodeReader
    '            Dim ds As New DataSet
    '            objOutputXml = objbzEquipment.EQUIPMENT_List
    '            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '                objXmlReader = New XmlNodeReader(objOutputXml)
    '                ds.ReadXml(objXmlReader)
    '                ds.Tables("EQP_CAT").Columns.Add("Select")
    '                '                <INC_LIST_EQP_CAT_OUTPUT>
    '                '	<EQP_CAT PRODUCTID='' EQUIPMENT_CODE='' DESCRIPTION=''/>
    '                '</INC_LIST_EQP_CAT_OUTPUT>
    '                dbgrdCategoryProductDesc.DataSource = ds.Tables("EQP_CAT")
    '                dbgrdCategoryProductDesc.DataBind()
    '            Else
    '                dbgrdCategoryProductDesc.DataSource = Nothing
    '                dbgrdCategoryProductDesc.DataBind()

    '                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '            End If
    '        Catch ex As Exception
    '            lblError.Text = ex.Message
    '        End Try
    '    End Sub
    '#End Region
    '#Region "BindCategoryProductDesc Procedure is called for binding the all List of Aoffice Details who belong to Security Region"
    '    Sub BindCategoryProductDesc(ByVal objOutputXml As XmlDocument)
    '        Try
    '            ' Dim objXmlReader As XmlNodeReader
    '            Dim dtCategProductDesc As New DataTable("dtCategProductDesc")
    '            Dim DrowIpPoolDetails As DataRow
    '            ' Dim objXmlReader As XmlNodeReader
    '            Dim objIpAddNode As XmlNode
    '            Dim Row As Integer
    '            dtCategProductDesc.Columns.Add("PRODUCTID", GetType(String))
    '            dtCategProductDesc.Columns.Add("EQUIPMENT_CODE", GetType(String))
    '            dtCategProductDesc.Columns.Add("DESCRIPTION", GetType(String))
    '            dtCategProductDesc.Columns.Add("Select", GetType(Boolean))

    '            'Here Back end Method Call
    '            '                <INC_VIEW_EQUP_CAT_OUTPUT>
    '            '	<BC_EQP BC_EQP_CATG_ID='' BC_EQP_CATG_TYPE=''>
    '            '		<PRODUCTID></PRODUCTID>
    '            '		<DESCRIPTION></DESCRIPTION>
    '            '	</BC_EQP>
    '            '	<Errors Status=''>
    '            '		<Error Code='' Description='' />
    '            '	</Errors>
    '            '</INC_VIEW_EQUP_CAT_OUTPUT>

    '            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '                For Row = 0 To dbgrdCategoryProductDesc.Rows.Count - 1
    '                    DrowIpPoolDetails = dtCategProductDesc.NewRow

    '                    Dim hdProductId As HiddenField = CType(dbgrdCategoryProductDesc.Rows(Row).FindControl("hdProductId"), HiddenField)
    '                    Dim hdEquipCode As HiddenField = CType(dbgrdCategoryProductDesc.Rows(Row).FindControl("hdEquipCode"), HiddenField)

    '                    DrowIpPoolDetails("DESCRIPTION") = dbgrdCategoryProductDesc.Rows(Row).Cells(0).Text
    '                    DrowIpPoolDetails("EQUIPMENT_CODE") = hdEquipCode.Value
    '                    DrowIpPoolDetails("PRODUCTID") = hdProductId.Value
    '                    For Each objIpAddNode In objOutputXml.DocumentElement.SelectNodes("BC_EQP/PRODUCTID")

    '                        If (hdProductId.Value = objIpAddNode.InnerText) Then
    '                            'If (dbgrdCategoryProductDesc.Rows(Row).Cells(0).Text = objIpAddNode.InnerText) Then
    '                            DrowIpPoolDetails("Select") = True
    '                            Exit For
    '                        Else
    '                            DrowIpPoolDetails("Select") = False
    '                        End If
    '                    Next
    '                    dtCategProductDesc.Rows.Add(DrowIpPoolDetails)
    '                Next Row
    '                dbgrdCategoryProductDesc.DataSource = dtCategProductDesc
    '                dbgrdCategoryProductDesc.DataBind()
    '            Else
    '                dbgrdCategoryProductDesc.DataSource = Nothing
    '                dbgrdCategoryProductDesc.DataBind()
    '                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '            End If
    '        Catch ex As Exception
    '            lblError.Text = ex.Message
    '        End Try
    '    End Sub
    '#End Region
#Region "ViewEquipMentCategory Procedure is called for View Security Region"
    Sub ViewEquipMentCategory()
        Try
            If (Not Request.QueryString("Action") = Nothing) Then
                Dim objInputXml, objOutputXml As New XmlDocument
                'Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                Dim objXmlReader As XmlNodeReader
                Dim objbzEquipment As New AAMS.bizIncetive.bzEquipment
                '   objInputXml.LoadXml("<MS_VIEWSECURITYREGION_INPUT><RegionID></RegionID></MS_VIEWSECURITYREGION_INPUT>")
                objInputXml.LoadXml("<INC_VIEW_EQUP_CAT_INPUT>	<BC_EQP_CATG_ID></BC_EQP_CATG_ID></INC_VIEW_EQUP_CAT_INPUT>")

                objInputXml.DocumentElement.SelectSingleNode("BC_EQP_CATG_ID").InnerText = objEn.Decrypt(Request.QueryString("EquipMentCategID").ToString().Trim)
                ''*****************************************************************************************
                '' OUTPUT
                ''****************************************************************************************
                ''<INC_VIEW_EQUP_CAT_OUTPUT>
                ''	<BC_EQP BC_EQP_CATG_ID='' BC_EQP_CATG_TYPE='' BC_EQP_CATG_COST='' UNITCOST_MANDATORY='' >
                ''      <PRODUCTLIST PRODUCTID='' EQUIPMENT_CODE='' EQUIPMENT_GROUP='' DESCRIPTION='' ></PRODUCTLIST>
                ''	</BC_EQP>
                ''	<Errors Status=''>
                ''		<Error Code='' Description='' />
                ''	</Errors>
                ''</INC_VIEW_EQUP_CAT_OUTPUT>
                ''*******************************************************************************************

                '- <INC_VIEW_EQUP_CAT_OUTPUT>
                '- <BC_EQP BC_EQP_CATG_ID="25" BC_EQP_CATG_TYPE="LAPTOP" BC_EQP_CATG_COST="1000">
                '  <PRODUCTID>1</PRODUCTID> 
                '  <DESCRIPTION>114 (933 MHZ Intel PIII)</DESCRIPTION> 
                '  <PRODUCTID>2</PRODUCTID> 
                '  <DESCRIPTION>16PORT HUB</DESCRIPTION> 
                '  <PRODUCTID>3</PRODUCTID> 
                '  <DESCRIPTION>25 PIN CONNECTOR</DESCRIPTION> 
                '  </BC_EQP>
                '- <Errors Status="FALSE">
                '  <Error Code="" Description="" /> 
                '  </Errors>
                '  </INC_VIEW_EQUP_CAT_OUTPUT>

                objOutputXml = objbzEquipment.View(objInputXml)

                '    objOutputXml.LoadXml("<INC_VIEW_EQUP_CAT_OUTPUT><BC_EQP BC_EQP_CATG_ID='25' BC_EQP_CATG_TYPE='LAPTOP' BC_EQP_CATG_COST='1000' UNITCOST_MANDATORY='True' COUNTRYID='' >  <PRODUCTLIST PRODUCTID='1' EQUIPMENT_CODE='' EQUIPMENT_GROUP='' DESCRIPTION='114 (933 MHZ Intel PIII)' ></PRODUCTLIST><PRODUCTLIST PRODUCTID='2' EQUIPMENT_CODE='' EQUIPMENT_GROUP='' DESCRIPTION='16PORT HUB' ></PRODUCTLIST><PRODUCTLIST PRODUCTID='3' EQUIPMENT_CODE='' EQUIPMENT_GROUP='' DESCRIPTION='25 PIN CONNECTOR' ></PRODUCTLIST>	</BC_EQP>	<Errors Status='False'>	<Error Code='' Description='' />	</Errors></INC_VIEW_EQUP_CAT_OUTPUT>")

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)



                    hdProductList.Value = objOutputXml.OuterXml

                    txtEquipmentCateg.Text = objOutputXml.DocumentElement.SelectSingleNode("BC_EQP").Attributes("BC_EQP_CATG_TYPE").InnerText
                    txtUnitCost.Text = objOutputXml.DocumentElement.SelectSingleNode("BC_EQP").Attributes("BC_EQP_CATG_COST").InnerText

                    'If objOutputXml.DocumentElement.SelectSingleNode("BC_EQP").Attributes("UNITCOST_MANDATORY").InnerText.Trim.ToUpper = "TRUE" Then
                    '    ChkUnitCostMan.Checked = True
                    'Else
                    '    ChkUnitCostMan.Checked = False
                    'End If
                    If objOutputXml.DocumentElement.SelectSingleNode("BC_EQP").Attributes("COUNTRYID") IsNot Nothing Then
                        Dim li As ListItem = drpCountry.Items.FindByValue(objOutputXml.DocumentElement.SelectSingleNode("BC_EQP").Attributes("COUNTRYID").InnerText)
                        If li IsNot Nothing Then
                            drpCountry.SelectedValue = li.Value
                        End If
                    End If


                    dbgrdCategoryProductDesc.DataSource = ds.Tables("PRODUCTLIST")
                    dbgrdCategoryProductDesc.DataBind()

                    ' if Record is found then call a method BindAofficeDetails(objOutputXml)  for binding the cotrols
                    ' BindCategoryProductDesc(objOutputXml)
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            drpCountry.Enabled = False
        End Try
    End Sub
#End Region
    'Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
    '    Response.Redirect("MSUP_SecurityRegion.aspx?Action=I")
    'End Sub
#Region "  btnSave_Click Event"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If (IsValid) Then
            Try
                If (Not Request.QueryString("Action") = Nothing) Then
                    Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                    Dim ds As New DataSet
                    Dim Rowno As Integer
                    Dim objParentNode, objChildNode, objChildNodeClone As XmlNode
                    Dim strAoffice As String = ""
                    Dim objbzEquipment As New AAMS.bizIncetive.bzEquipment
                    ' objInputXml.LoadXml("<MS_UPDATESECURITYREGION_INPUT><SECURITYREGION RegionID='' Name=''><Aoffice></Aoffice></SECURITYREGION></MS_UPDATESECURITYREGION_INPUT>")


                    objInputXml.LoadXml("<INC_UPDATE_EQP_CAT_INPUT>	<EQP_CAT BC_EQP_CATG_ID='' BC_EQP_CATG_TYPE='' BC_EQP_CATG_COST='' UNITCOST_MANDATORY =''    COUNTRYID='' COUNTRYNAME='' >	<PRODUCTID></PRODUCTID>	</EQP_CAT></INC_UPDATE_EQP_CAT_INPUT>")

                    'Reading and Appending records into the Input XMLDocument
                    objParentNode = objInputXml.DocumentElement.SelectSingleNode("EQP_CAT")
                    If (Request.QueryString("Action") = "U") Then
                        objParentNode.Attributes("BC_EQP_CATG_ID").Value = objEn.Decrypt(Request.QueryString("EquipMentCategID").Trim)
                    End If
                    objParentNode.Attributes("BC_EQP_CATG_TYPE").Value = txtEquipmentCateg.Text
                    objParentNode.Attributes("BC_EQP_CATG_COST").Value = txtUnitCost.Text
                    If txtUnitCost.Text.Trim.Length > 0 Then
                        objParentNode.Attributes("UNITCOST_MANDATORY").Value = True
                    Else
                        objParentNode.Attributes("UNITCOST_MANDATORY").Value = False
                    End If


                    If drpCountry.SelectedValue <> "" Then
                        objParentNode.Attributes("COUNTRYID").Value = drpCountry.SelectedValue.ToString
                    End If


                    objChildNode = objInputXml.DocumentElement.SelectSingleNode("EQP_CAT/PRODUCTID")

                    objChildNodeClone = objChildNode.CloneNode(True)
                    objParentNode.RemoveChild(objChildNode)
                    For Rowno = 0 To dbgrdCategoryProductDesc.Rows.Count - 1

                        Dim hdProductId As HiddenField = CType(dbgrdCategoryProductDesc.Rows(Rowno).FindControl("hdProductId"), HiddenField)

                        objChildNodeClone.InnerText = hdProductId.Value 'dbgrdCategoryProductDesc.Rows(Rowno).Cells(0).Text 'strAoffice
                        objParentNode.AppendChild(objChildNodeClone)
                        objChildNodeClone = objChildNode.CloneNode(True)

                    Next Rowno


                    If objInputXml.DocumentElement.SelectNodes("EQP_CAT/PRODUCTID").Count <= 0 Then
                        lblError.Text = "Product is mandatory."
                        Exit Sub
                    End If
                    'Here Back end Method Call
                    objOutputXml = objbzEquipment.Update(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        If (Request.QueryString("Action") = "U") Then

                            lblError.Text = objeAAMSMessage.messUpdate '"Updated Successfully."
                            Response.Redirect("INVUP_EquipmentCategory.aspx?&Msg=U&Action=U&EquipMentCategID=" & objEn.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("EQP_CAT").Attributes("BC_EQP_CATG_ID").Value.Trim), False)
                        Else
                            lblError.Text = objeAAMSMessage.messInsert ' "Added Successfully."
                            Response.Redirect("INVUP_EquipmentCategory.aspx?&Msg=A&Action=U&EquipMentCategID=" & objEn.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("EQP_CAT").Attributes("BC_EQP_CATG_ID").Value.Trim), False)
                        End If
                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
        End If
    End Sub
#End Region
#Region " This Function Return boolean value for Checkbox"
    Protected Function FindStatus(ByVal CheckStatus As Object) As Boolean
        Try
            If (CheckStatus Is Nothing) Then
                Return False
            ElseIf (Convert.ToString(CheckStatus) = "False") Then
                Return False
            ElseIf (Convert.ToString(CheckStatus) = "True") Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region
#Region "btnReset_Click Event"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        'Try
        '    If (Request.QueryString("Action") = "U") Then
        '        ViewEquipMentCategory()
        '    Else
        '        txtEquipmentCateg.Text = ""
        '        txtEquipmentCateg.Focus()
        '    End If
        '    lblError.Text = ""
        'Catch ex As Exception
        '    lblError.Text = ex.Message
        'End Try
        'lblError.Text = ""
        Response.Redirect(Request.Url.ToString, False)
    End Sub
#End Region

    'Code to fill product list  in xml from grid
    Sub FillProductPopup()
        Dim objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objNewProduct As New Xml
        '    hdProductListPopUpPage.Value

        Dim ProductListPopUpPage As String = hdProductListPopUpPage.Value



        If hdProductList.Value.Trim.Length <= 0 Then
            hdProductList.Value = "<INC_VIEW_EQUP_CAT_OUTPUT><BC_EQP BC_EQP_CATG_ID='' BC_EQP_CATG_TYPE='' BC_EQP_CATG_COST='' UNITCOST_MANDATORY ='' >	</BC_EQP>''	<Errors Status='False'><Error Code='' Description='' /></Errors></INC_VIEW_EQUP_CAT_OUTPUT>"
        End If

        ' If dbgrdCategoryProductDesc.Rows.Count > 0 Then
        If hdProductList.Value.Trim.Length > 0 Then


            Dim objxmlNode, objChildNode As XmlNode

            objOutputXml.LoadXml(hdProductList.Value)

            ''*****************************************************************************************
            '' OUTPUT
            ''****************************************************************************************
            ''<INC_VIEW_EQUP_CAT_OUTPUT>
            ''	<BC_EQP BC_EQP_CATG_ID='' BC_EQP_CATG_TYPE='' BC_EQP_CATG_COST='' UNITCOST_MANDATORY='' >
            ''      <PRODUCTLIST PRODUCTID='' EQUIPMENT_CODE='' EQUIPMENT_GROUP='' DESCRIPTION='' ></PRODUCTLIST>
            ''	</BC_EQP>
            ''	<Errors Status=''>
            ''		<Error Code='' Description='' />
            ''	</Errors>
            ''</INC_VIEW_EQUP_CAT_OUTPUT>
            ''*******************************************************************************************

            Dim strArray() As String = ProductListPopUpPage.Split(",")
            Dim strProductId As String = ""

            Dim objxmlInput As New XmlDocument
            objxmlInput.LoadXml("<BC_EQP BC_EQP_CATG_ID='' BC_EQP_CATG_TYPE='' BC_EQP_CATG_COST='' UNITCOST_MANDATORY='' ><PRODUCTLIST PRODUCTID='' EQUIPMENT_CODE='' EQUIPMENT_GROUP='' DESCRIPTION='' ></PRODUCTLIST>	</BC_EQP>")

            objChildNode = objxmlInput.SelectSingleNode("BC_EQP/PRODUCTLIST")

            For i As Integer = 0 To strArray.Length - 1
                strProductId = strArray(i).Split("|")(0).Trim
                objxmlNode = objOutputXml.DocumentElement.SelectSingleNode("BC_EQP/PRODUCTLIST[@PRODUCTID = '" + strProductId.ToString.Trim + "']")
                If objxmlNode Is Nothing Then

                    With objChildNode
                        .Attributes("PRODUCTID").Value = strProductId
                        .Attributes("EQUIPMENT_CODE").Value = strArray(i).Split("|")(1).Trim
                        .Attributes("EQUIPMENT_GROUP").Value = strArray(i).Split("|")(2).Trim

                        .Attributes("DESCRIPTION").Value = strArray(i).Split("|")(3).Trim
                    End With


                    objOutputXml.DocumentElement.SelectSingleNode("BC_EQP").AppendChild(objOutputXml.ImportNode(objChildNode, True))
                End If
            Next
        End If
        ' End If
        hdProductList.Value = objOutputXml.OuterXml
        If hdProductList.Value.Trim.Length > 0 Then
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)

                dbgrdCategoryProductDesc.DataSource = ds.Tables("PRODUCTLIST")
                dbgrdCategoryProductDesc.DataBind()

            End If
        End If
        hdProductListPopUpPage.Value = ""
    End Sub

    Protected Sub dbgrdCategoryProductDesc_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dbgrdCategoryProductDesc.RowCommand
        Try

            Dim strProductId As String
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            strProductId = e.CommandArgument.ToString
            Dim objOutputXml As New XmlDocument
            Dim objSearchNode As XmlNode

            If hdProductList.Value.Trim.Length > 0 Then
                objOutputXml.LoadXml(hdProductList.Value)
                objSearchNode = objOutputXml.DocumentElement.SelectSingleNode("BC_EQP/PRODUCTLIST[@PRODUCTID = '" + strProductId.ToString.Trim + "']")

                If objSearchNode IsNot Nothing Then
                    objOutputXml.DocumentElement.SelectSingleNode("BC_EQP").RemoveChild(objSearchNode)
                End If

                hdProductList.Value = objOutputXml.OuterXml

                If hdProductList.Value.Trim.Length > 0 Then
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                        objXmlReader = New XmlNodeReader(objOutputXml)
                        ds.ReadXml(objXmlReader)

                        dbgrdCategoryProductDesc.DataSource = ds.Tables("PRODUCTLIST")
                        dbgrdCategoryProductDesc.DataBind()

                    End If
                End If
            End If
           
        Catch ex As Exception
        End Try
    End Sub
End Class
