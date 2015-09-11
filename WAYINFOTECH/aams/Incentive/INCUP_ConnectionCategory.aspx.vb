
Partial Class Incentive_INCUP_ConnectionCategory
    Inherits System.Web.UI.Page
#Region " Page Level Variables/ Objects"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim objEn As New EncyrptDeCyrpt

#End Region


#Region "Code for Filter "
    'Code Added by Abhishek
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Connectivity Category']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Connectivity Category']").Attributes("Value").Value)

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
            btnNew.Attributes.Add("onclick", "return NewConnectionCategory();")
            btnSave.Attributes.Add("onclick", "return CheckMandatoty();")
            Dim objbzSecurityRegion As New AAMS.bizIncetive.bzEquipment

            If Not IsPostBack Then
                objeAAMS.BindDropDown(drpCountry, "COUNTRY", True, 1)
            End If


            If hdOnlineListPopUpPage.Value <> "" Then

                FillProductPopup()

            End If

            If Not Page.IsPostBack Then
                ' ListOnlineConnectivityDesc()
                ChkUnitCostMan.Checked = True
                If (Request.QueryString("Action") = "I") Then
                    ChkUnitCostMan.Checked = True
                End If

                '*******************************************************************
                ''Code For 
                '*******************************************************************
                ' Load Data For Editing Mode
                If (Request.QueryString("Action") = "U") Then
                    ViewConnectionCategory()
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
    '#Region " Show List of ListOnlineConnectivityDesc"
    '    Sub ListOnlineConnectivityDesc()
    '        Try
    '            Dim objbzConnectivity As New AAMS.bizIncetive.bzConnectivity
    '            Dim objInputXml, objOutputXml As New XmlDocument
    '            Dim objXmlReader As XmlNodeReader
    '            Dim ds As New DataSet
    '            objOutputXml = objbzConnectivity.CONN_CAT_List
    '            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '                objXmlReader = New XmlNodeReader(objOutputXml)
    '                ds.ReadXml(objXmlReader)
    '                ds.Tables("CONN_CAT").Columns.Add("Select")
    '                '              
    '                '<INC_LIST_CONN_CAT_OUTPUT>
    '                '	<CONN_CAT ONLINESTATUS='' STATUSCODE='' />
    '                '</INC_LIST_CONN_CAT_OUTPUT>

    '                dbgrdConnectionCategoryDesc.DataSource = ds.Tables("CONN_CAT")
    '                dbgrdConnectionCategoryDesc.DataBind()
    '            Else
    '                dbgrdConnectionCategoryDesc.DataSource = Nothing
    '                dbgrdConnectionCategoryDesc.DataBind()

    '                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '            End If
    '        Catch ex As Exception
    '            lblError.Text = ex.Message
    '        End Try
    '    End Sub
    '#End Region
    '#Region "BindConnectionCategoryDesc Procedure is called for binding the all List of Aoffice Details who belong to Security Region"
    '    Sub BindConnectionCategoryDesc(ByVal objOutputXml As XmlDocument)
    '        Try
    '            ' Dim objXmlReader As XmlNodeReader
    '            Dim dtCONN_CAT As New DataTable("CONN_CAT")
    '            Dim DrowIpPoolDetails As DataRow
    '            ' Dim objXmlReader As XmlNodeReader
    '            Dim objIpAddNode As XmlNode
    '            Dim Row As Integer
    '            dtCONN_CAT.Columns.Add("ONLINESTATUS", GetType(String))
    '            dtCONN_CAT.Columns.Add("STATUSCODE", GetType(String))
    '            dtCONN_CAT.Columns.Add("Select", GetType(Boolean))

    '            'Here Back end Method Call
    '            '           <INC_UPDATE_CONN_CAT_OUTPUT>
    '            '	<CONN_CAT  BC_ONLINE_CATG_ID='' BC_ONLINE_CATG_NAME=''>
    '            '		<ONLINESTATUS></ONLINESTATUS>
    '            '	</CONN_CAT>
    '            '	<Errors Status=''>
    '            '		<Error Code='' Description='' />
    '            '	</Errors>
    '            '</INC_UPDATE_CONN_CAT_OUTPUT>

    '            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '                For Row = 0 To dbgrdConnectionCategoryDesc.Rows.Count - 1
    '                    DrowIpPoolDetails = dtCONN_CAT.NewRow

    '                    Dim hdStatusCode As HiddenField = CType(dbgrdConnectionCategoryDesc.Rows(Row).FindControl("hdStatusCode"), HiddenField)


    '                    DrowIpPoolDetails("ONLINESTATUS") = dbgrdConnectionCategoryDesc.Rows(Row).Cells(0).Text
    '                    DrowIpPoolDetails("STATUSCODE") = hdStatusCode.Value

    '                    For Each objIpAddNode In objOutputXml.DocumentElement.SelectNodes("BC_CONN/ONLINESTATUSCODE")

    '                        If (hdStatusCode.Value = objIpAddNode.InnerText) Then
    '                            'If (dbgrdCategoryProductDesc.Rows(Row).Cells(0).Text = objIpAddNode.InnerText) Then
    '                            DrowIpPoolDetails("Select") = True
    '                            Exit For
    '                        Else
    '                            DrowIpPoolDetails("Select") = False
    '                        End If
    '                    Next
    '                    dtCONN_CAT.Rows.Add(DrowIpPoolDetails)
    '                Next Row
    '                dbgrdConnectionCategoryDesc.DataSource = dtCONN_CAT
    '                dbgrdConnectionCategoryDesc.DataBind()
    '            Else
    '                dbgrdConnectionCategoryDesc.DataSource = Nothing
    '                dbgrdConnectionCategoryDesc.DataBind()
    '                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '            End If
    '        Catch ex As Exception
    '            lblError.Text = ex.Message
    '        End Try
    '    End Sub
    '#End Region
#Region "ViewConnectionCategory Procedure is called for View Security Region"
    Sub ViewConnectionCategory()
        Try
            If (Not Request.QueryString("Action") = Nothing) Then
                Dim objInputXml, objOutputXml As New XmlDocument
                'Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                Dim objXmlReader As XmlNodeReader
                Dim objbzConnectivity As New AAMS.bizIncetive.bzConnectivity
                '   objInputXml.LoadXml("<MS_VIEWSECURITYREGION_INPUT><RegionID></RegionID></MS_VIEWSECURITYREGION_INPUT>")
                objInputXml.LoadXml("<INC_VIEW_CONN_CAT_INPUT>	<BC_ONLINE_CATG_ID></BC_ONLINE_CATG_ID></INC_VIEW_CONN_CAT_INPUT>")

                objInputXml.DocumentElement.SelectSingleNode("BC_ONLINE_CATG_ID").InnerText = objEn.Decrypt(Request.QueryString("OnlineCategID").ToString().Trim)
                '               <INC_UPDATE_CONN_CAT_OUTPUT>
                '	<CONN_CAT  BC_ONLINE_CATG_ID='' BC_ONLINE_CATG_NAME=''  BC_ONLINE_CATG_COST='' UNITCOST_MANDATORY='' COUNTRYID=''  >
                '      <ONLINE  STATUSCODE='' ONLINESTATUS='' ></ONLINE>                		
                '	</CONN_CAT>
                '	<Errors Status=''>
                '		<Error Code='' Description='' />
                '	</Errors>
                '</INC_UPDATE_CONN_CAT_OUTPUT>
                objOutputXml = objbzConnectivity.View(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)

                    hdOnlineList.Value = objOutputXml.OuterXml

                    '  Dim li As New ListItem(objOutputXml.DocumentElement.SelectSingleNode("CONN_CAT").Attributes("BC_ONLINE_CATG_NAME").InnerText)

                    If objOutputXml.DocumentElement.SelectSingleNode("CONN_CAT").Attributes("COUNTRYID") IsNot Nothing Then
                        Dim li As ListItem = drpCountry.Items.FindByValue(objOutputXml.DocumentElement.SelectSingleNode("CONN_CAT").Attributes("COUNTRYID").InnerText)
                        If li IsNot Nothing Then
                            drpCountry.SelectedValue = li.Value
                        End If
                    End If


                    txtConnectionCategName.Text = objOutputXml.DocumentElement.SelectSingleNode("CONN_CAT").Attributes("BC_ONLINE_CATG_NAME").InnerText
                    txtUnitCost.Text = objOutputXml.DocumentElement.SelectSingleNode("CONN_CAT").Attributes("BC_ONLINE_CATG_COST").InnerText
                    ' if Record is found then call a method BindAofficeDetails(objOutputXml)  for binding the cotrols
                    '  BindConnectionCategoryDesc(objOutputXml)

                    'If objOutputXml.DocumentElement.SelectSingleNode("CONN_CAT").Attributes("UNITCOST_MANDATORY").InnerText.Trim.ToUpper = "TRUE" Then
                    '    ChkUnitCostMan.Checked = True
                    'Else
                    '    ChkUnitCostMan.Checked = False
                    'End If

                    dbgrdConnectionCategoryDesc.DataSource = ds.Tables("ONLINE")
                    dbgrdConnectionCategoryDesc.DataBind()



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
                    Dim objbzConnectivity As New AAMS.bizIncetive.bzConnectivity
                    ' objInputXml.LoadXml("<MS_UPDATESECURITYREGION_INPUT><SECURITYREGION RegionID='' Name=''><Aoffice></Aoffice></SECURITYREGION></MS_UPDATESECURITYREGION_INPUT>")


                    objInputXml.LoadXml("<INC_UPDATE_CONN_CAT_INPUT><CONN_CAT BC_ONLINE_CATG_ID='' BC_ONLINE_CATG_NAME='' BC_ONLINE_CATG_COST='' UNITCOST_MANDATORY='' COUNTRYID='' COUNTRYNAME ='' ><ONLINESTATUS></ONLINESTATUS>  </CONN_CAT></INC_UPDATE_CONN_CAT_INPUT>")

                    'Reading and Appending records into the Input XMLDocument
                    objParentNode = objInputXml.DocumentElement.SelectSingleNode("CONN_CAT")
                    If (Request.QueryString("Action") = "U") Then
                        objParentNode.Attributes("BC_ONLINE_CATG_ID").Value = objEn.Decrypt(Request.QueryString("OnlineCategID").Trim)
                    End If
                    objParentNode.Attributes("BC_ONLINE_CATG_NAME").Value = txtConnectionCategName.Text

                    objParentNode.Attributes("BC_ONLINE_CATG_COST").Value = txtUnitCost.Text


                    If txtUnitCost.Text.Trim.Length > 0 Then
                        objParentNode.Attributes("UNITCOST_MANDATORY").Value = True
                    Else
                        objParentNode.Attributes("UNITCOST_MANDATORY").Value = False
                    End If

                    If drpCountry.SelectedValue <> "" Then
                        objParentNode.Attributes("COUNTRYID").Value = drpCountry.SelectedValue.ToString
                    End If


                    objChildNode = objInputXml.DocumentElement.SelectSingleNode("CONN_CAT/ONLINESTATUS")



                    objChildNodeClone = objChildNode.CloneNode(True)
                    objParentNode.RemoveChild(objChildNode)
                    For Rowno = 0 To dbgrdConnectionCategoryDesc.Rows.Count - 1
                        'objAptchildNodeClone.Attributes("Aoffice").InnerText = dbgrdManageSecurityRegion.Rows(Rowno).Cells(0).Text
                       
                        Dim hdStatusCode As HiddenField = CType(dbgrdConnectionCategoryDesc.Rows(Rowno).FindControl("hdStatusCode"), HiddenField)

                        objChildNodeClone.InnerText = hdStatusCode.Value 'dbgrdCategoryProductDesc.Rows(Rowno).Cells(0).Text 'strAoffice
                        objParentNode.AppendChild(objChildNodeClone)
                        objChildNodeClone = objChildNode.CloneNode(True)

                    Next Rowno

                    If objInputXml.DocumentElement.SelectNodes("CONN_CAT/ONLINESTATUS").Count <= 0 Then
                        lblError.Text = "Status is mandatory."
                        Exit Sub
                    End If

                    'Here Back end Method Call
                    objOutputXml = objbzConnectivity.Update(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        If (Request.QueryString("Action") = "U") Then

                            lblError.Text = objeAAMSMessage.messUpdate '"Updated Successfully."
                            Response.Redirect("INCUP_ConnectionCategory.aspx?&Msg=U&Action=U&OnlineCategID=" & objEn.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("CONN_CAT").Attributes("BC_ONLINE_CATG_ID").Value.Trim), False)
                        Else
                            lblError.Text = objeAAMSMessage.messInsert ' "Added Successfully."
                            Response.Redirect("INCUP_ConnectionCategory.aspx?&Msg=A&Action=U&OnlineCategID=" & objEn.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("CONN_CAT").Attributes("BC_ONLINE_CATG_ID").Value.Trim), False)
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

        Dim ProductListPopUpPage As String = hdOnlineListPopUpPage.Value



        If hdOnlineList.Value.Trim.Length <= 0 Then
            hdOnlineList.Value = "<INC_VIEW_CONN_CAT_OUTPUT> <CONN_CAT  BC_ONLINE_CATG_ID='' BC_ONLINE_CATG_NAME=''  BC_ONLINE_CATG_COST='' UNITCOST_MANDATORY=''  > </CONN_CAT>	<Errors Status='False'><Error Code='' Description='' />	</Errors></INC_VIEW_CONN_CAT_OUTPUT>"
        End If

        '' If dbgrdCategoryProductDesc.Rows.Count > 0 Then
        If hdOnlineList.Value.Trim.Length > 0 Then


            Dim objxmlNode, objChildNode As XmlNode

            objOutputXml.LoadXml(hdOnlineList.Value)

            ''*****************************************************************************************
            '' OUTPUT
            ''****************************************************************************************
            ''<INC_VIEW_EQUP_CAT_OUTPUT>
            '	<CONN_CAT  BC_ONLINE_CATG_ID='' BC_ONLINE_CATG_NAME=''  BC_ONLINE_CATG_COST='' UNITCOST_MANDATORY=''  >
            '   <ONLINE  STATUSCODE='' ONLINESTATUS='' ></ONLINE>                		
            '</CONN_CAT>
            '<Errors Status=''>
            '	<Error Code='' Description='' />
            '</Errors>
            ''</INC_VIEW_EQUP_CAT_OUTPUT>
            ''*******************************************************************************************



            Dim strArray() As String = ProductListPopUpPage.Split(",")
            Dim strStatusCode As String = ""

            Dim objxmlInput As New XmlDocument
            objxmlInput.LoadXml("<CONN_CAT  BC_ONLINE_CATG_ID='' BC_ONLINE_CATG_NAME=''  BC_ONLINE_CATG_COST='' UNITCOST_MANDATORY=''  >  <ONLINE  STATUSCODE='' ONLINESTATUS='' ></ONLINE> </CONN_CAT>")

            objChildNode = objxmlInput.SelectSingleNode("CONN_CAT/ONLINE")

            For i As Integer = 0 To strArray.Length - 1
                strStatusCode = strArray(i).Split("|")(0).Trim
                objxmlNode = objOutputXml.DocumentElement.SelectSingleNode("CONN_CAT/ONLINE[@STATUSCODE = '" + strStatusCode.ToString.Trim + "']")
                If objxmlNode Is Nothing Then

                    With objChildNode
                        .Attributes("STATUSCODE").Value = strStatusCode
                        .Attributes("ONLINESTATUS").Value = strArray(i).Split("|")(1).Trim
                        
                    End With


                    objOutputXml.DocumentElement.SelectSingleNode("CONN_CAT").AppendChild(objOutputXml.ImportNode(objChildNode, True))
                End If
            Next
        End If
        ' End If
        hdOnlineList.Value = objOutputXml.OuterXml
        If hdOnlineList.Value.Trim.Length > 0 Then
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)

                dbgrdConnectionCategoryDesc.DataSource = ds.Tables("ONLINE")
                dbgrdConnectionCategoryDesc.DataBind()

            End If
        End If
        hdOnlineListPopUpPage.Value = ""
    End Sub

   

    Protected Sub dbgrdConnectionCategoryDesc_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dbgrdConnectionCategoryDesc.RowCommand
        Try

            Dim strStatusCode As String
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            strStatusCode = e.CommandArgument.ToString
            Dim objOutputXml As New XmlDocument
            Dim objSearchNode As XmlNode

            If hdOnlineList.Value.Trim.Length > 0 Then
                objOutputXml.LoadXml(hdOnlineList.Value)
                objSearchNode = objOutputXml.DocumentElement.SelectSingleNode("CONN_CAT/ONLINE[@STATUSCODE = '" + strStatusCode.ToString.Trim + "']")

                If objSearchNode IsNot Nothing Then
                    objOutputXml.DocumentElement.SelectSingleNode("CONN_CAT").RemoveChild(objSearchNode)
                End If

                hdOnlineList.Value = objOutputXml.OuterXml

                If hdOnlineList.Value.Trim.Length > 0 Then
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                        objXmlReader = New XmlNodeReader(objOutputXml)
                        ds.ReadXml(objXmlReader)

                        dbgrdConnectionCategoryDesc.DataSource = ds.Tables("ONLINE")
                        dbgrdConnectionCategoryDesc.DataBind()

                    End If
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub
End Class
