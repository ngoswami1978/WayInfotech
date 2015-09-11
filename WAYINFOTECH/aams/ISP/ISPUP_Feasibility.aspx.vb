Imports System.Data
Imports System.Xml
Partial Class ISP_ISPUP_Feasibility
    Inherits System.Web.UI.Page
    Public objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
#Region "Page_Load()"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = Request.Url.ToString()
            '   Checking Security  Start.
            CheckSecurity()
            txtAgencyName.Text = Request.Form("txtAgencyName")
            txtAddress.Text = Request.Form("txtAddress")
            txtConcernPerson.Text = Request.Form("txtConcernPerson")
            txtFax.Text = Request.Form("txtFax")
            txtPhone.Text = Request.Form("txtPhone")
            txtPinCode.Text = Request.Form("txtPinCode")
            txtCity.Text = Request.Form("txtCity")
            txtCountry.Text = Request.Form("txtCountry")
            txtOfficeId.Text = Request.Form("txtOfficeId")
            'Binding City And Country DropDown.
            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(drpCountry, "COUNTRY", True)
                objeAAMS.BindDropDown(drpIspName, "ISPLIST", True)
                btnSave.Attributes.Add("onClick", "return ValidateForm();")
            End If
            '   Session Clear For Mail. 
            Session("EmailList") = Nothing
            '   Code For Insert Start.
            If Request.QueryString("Action") = "I" Then
                If (Not Page.IsPostBack) Then
                    btnUpdate.Visible = False
                    rbtExistingAgency.Attributes.Add("onClick", "radionewexisting(2);")
                    rbtNewAgency.Attributes.Add("onClick", "radionewexisting(2);")
                    btnAdd.Attributes.Add("onClick", "return ValidateISP();")
                    objeAAMS.BindDropDown(ddlFeasibleStatus, "FeasibilityStatus", True)
                    ddlFeasibleStatus.SelectedValue = ddlFeasibleStatus.Items.FindByText("Pending").Value
                    drpCity.Style.Add("display", "none")
                    drpCountry.Style.Add("display", "none")
                End If
                pnlFStatus.Visible = False
                btnUpdate.Visible = False
            End If
            '   Code For Insert End.
            '   Code For Update Start.
            If Request.QueryString("Action") = "U" Then
                If (Not Page.IsPostBack) Then
                    btnUpdate.Attributes.Add("onClick", "return ValidateISPUpdate();")
                    btnAdd.Attributes.Add("onClick", "return ValidateISP();")
                    objeAAMS.BindDropDown(ddlFeasibleStatus, "FeasibilityStatus", True)
                    rbtNewAgency.Enabled = False
                    rbtExistingAgency.Enabled = False
                    BindData()
                End If
                '  Code For Mail date Update start
                If Session("ISPMailDate") IsNot Nothing Then
                    UpdateMailDate()
                End If
                '  Code For Mail date Update end
            End If
            '   Code For Displaying Message Start.
            If Not Request.QueryString("Message") Is Nothing Then
                If Request.QueryString("Message") = "A" Then
                    lblError.Text = objeAAMSMessage.messInsert
                ElseIf Request.QueryString("Message") = "U" Then
                    lblError.Text = objeAAMSMessage.messUpdate
                End If
            End If
            If Not Request.QueryString("Reset") Is Nothing Then
                lblError.Text = ""
            End If
            '   Code For Displaying Message End.
            '   Show Hide Controls.
            ShowHideControls()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnSave_Click"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objFRequest As New AAMS.bizISP.bzISPFeasibleRequest
        Dim objInputXml, objOutputXml, objISPXmlDoc As New XmlDocument
        Dim objXmlNode As XmlNode
        Try
            If hdData.Value = "" Then
                objInputXml.LoadXml("<ISP_UPDATEFEASIBILEREQUEST_INPUT><FEASIBILE_REQUEST LCODE='' CityID='' RequestID='' LoggedDatetime='' LoggedBy='' Remarks='' AGENCYNAME='' ADDRESS='' COUNTRYID='' FAX='' PHONE='' PINCODE='' CONCERNED_PERSON=''></FEASIBILE_REQUEST></ISP_UPDATEFEASIBILEREQUEST_INPUT>")
            Else
                objInputXml.LoadXml(hdData.Value)
            End If
            ' objInputXml.LoadXml("<ISP_UPDATEFEASIBILEREQUEST_INPUT><FEASIBILE_REQUEST LCODE='' CityID='' RequestID='' LoggedDatetime='' LoggedBy='' Remarks='' AGENCYNAME='' ADDRESS='' COUNTRYID='' FAX='' PHONE='' PINCODE='' CONCERNED_PERSON=''></FEASIBILE_REQUEST></ISP_UPDATEFEASIBILEREQUEST_INPUT>")
            objXmlNode = objInputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST")
            If (rbtNewAgency.Checked = True) Then
                objXmlNode.Attributes("LCODE").Value = ""
                objXmlNode.Attributes("CityID").Value = drpCity.SelectedValue
                objXmlNode.Attributes("COUNTRYID").Value = drpCountry.SelectedValue
            Else
                objXmlNode.Attributes("LCODE").Value = hdlcode.Value
                objXmlNode.Attributes("CityID").Value = hdCity.Value
                objXmlNode.Attributes("COUNTRYID").Value = hdCountry.Value
            End If
            ' Adding Agency Details
            objXmlNode.Attributes("AGENCYNAME").Value = txtAgencyName.Text
            objXmlNode.Attributes("ADDRESS").Value = txtAddress.Text
            objXmlNode.Attributes("FAX").Value = txtFax.Text
            objXmlNode.Attributes("PHONE").Value = txtPhone.Text
            objXmlNode.Attributes("PINCODE").Value = txtPinCode.Text
            objXmlNode.Attributes("CONCERNED_PERSON").Value = txtConcernPerson.Text
            objXmlNode.Attributes("RequestID").Value = txtRequestId.Text
            objXmlNode.Attributes("LoggedDatetime").Value = txtLoggedDate.Text
            objXmlNode.Attributes("LoggedBy").Value = Session("LoginSession").Split("|").GetValue(0).ToString()
            objXmlNode.Attributes("Remarks").Value = txtRemarks.Text
            ' Removing Blank Node.
            Dim objBlankNode As XmlNode
            objBlankNode = objInputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST/REQUEST_DETAILS[@ISPName='']")
            If Not objBlankNode Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").RemoveChild(objBlankNode)
            End If
            ' Calling Back End Method For Update.
            objOutputXml = objFRequest.Update(objInputXml)
            ' Checking Error Statuus.
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Dim strRequestID As String = ""
                strRequestID = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("RequestID").Value
                '@ Code Used For Encription/Decription
                Dim EnstrQueryStringForstrRequestID As String
                EnstrQueryStringForstrRequestID = objED.Encrypt(strRequestID)
                '@ End of Code Used For Encription/Decription
                If Request.QueryString("Action") = "I" Then
                    Response.Redirect("ISPUP_Feasibility.aspx?Action=U&FeasibilityRequestID=" + EnstrQueryStringForstrRequestID + "&Message=A")
                Else
                    Response.Redirect("ISPUP_Feasibility.aspx?Action=U&FeasibilityRequestID=" + EnstrQueryStringForstrRequestID + "&Message=U")
                End If
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnReset_Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        'If Request.QueryString("Action") = "I" Then
        '    Response.Redirect("ISPUP_Feasibility.aspx?Action=I")
        'ElseIf Request.QueryString("Action") = "U" Then
        '    ViewState("ViewRequest") = Nothing
        '    BindData()
        '    lblError.Text = ""
        'End If
        'Response.Redirect("ISPUP_Feasibility.aspx?" + Request.QueryString().ToString())
        Response.Redirect("ISPUP_Feasibility.aspx?" + Request.QueryString().ToString() + "&Reset=1")

    End Sub
#End Region

#Region "btnAdd_Click()"
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim objDS As New DataSet()
        Dim objXmlDoc As New XmlDocument
        Dim objXmlNode As XmlNode
        Dim objXmlNodeClone As XmlNode
        Dim objXmlNodeChk As XmlNode
        Try
            If hdData.Value = "" Then
                ' <ISP_UPDATEFEASIBILEREQUEST_INPUT><FEASIBILE_REQUEST LCODE='' CityID='' RequestID='' LoggedDatetime='' LoggedBy='' Remarks='' AGENCYNAME='' ADDRESS='' COUNTRYID='' FAX='' PHONE='' PINCODE='' CONCERNED_PERSON=''><REQUEST_DETAILS Action='' ISPID='' ISPName='' FeasibleStatusID='' FeasibleStatusName='' FeasibleDate='' MailSendDatetime=''/></FEASIBILE_REQUEST></ISP_UPDATEFEASIBILEREQUEST_INPUT>
                'objXmlDoc.LoadXml("<ISP><REQUEST_DETAILS Action='' ISPID='' ISPName=''  FeasibleStatusID='' FeasibleStatusName='' FeasibleDate='' MailSendDatetime='' /></ISP>")
                objXmlDoc.LoadXml("<ISP_UPDATEFEASIBILEREQUEST_INPUT><FEASIBILE_REQUEST LCODE='' CityID='' RequestID='' LoggedDatetime='' LoggedBy='' Remarks='' AGENCYNAME='' ADDRESS='' COUNTRYID='' FAX='' PHONE='' PINCODE='' CONCERNED_PERSON=''><REQUEST_DETAILS Action='' ISPID='' ISPName='' FeasibleStatusID='' FeasibleStatusName='' FeasibleDate='' MailSendDatetime=''/></FEASIBILE_REQUEST></ISP_UPDATEFEASIBILEREQUEST_INPUT>")
                objXmlNode = objXmlDoc.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST/REQUEST_DETAILS")
                objXmlNode.Attributes("Action").Value = "I"
                objXmlNode.Attributes("ISPID").Value = drpIspName.SelectedValue
                objXmlNode.Attributes("ISPName").Value = drpIspName.SelectedItem.Text
                hdData.Value = objXmlDoc.OuterXml
            Else
                objXmlDoc.LoadXml(hdData.Value)
                ' Adding Blank Node
                If objXmlDoc.DocumentElement.SelectNodes("FEASIBILE_REQUEST/REQUEST_DETAILS").Count = 0 Then
                    Dim objDocFrag As XmlDocumentFragment
                    Dim objXmlDoc1 As New XmlDocument
                    objXmlDoc1.LoadXml("<Root><REQUEST_DETAILS Action='' ISPID='' ISPName='' FeasibleStatusID='' FeasibleStatusName='' FeasibleDate='' MailSendDatetime=''/></Root>")
                    objDocFrag = objXmlDoc.CreateDocumentFragment()
                    objDocFrag.InnerXml = objXmlDoc1.DocumentElement.InnerXml
                    objXmlDoc.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").AppendChild(objDocFrag)
                    '  hdData.Value = objXmlDoc.OuterXml
                End If
                objXmlNodeChk = objXmlDoc.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST/REQUEST_DETAILS[@ISPID='" + drpIspName.SelectedValue + "'][@Action != 'R']")
                ' Check Duplicate ISP
                If Not objXmlNodeChk Is Nothing Then
                    lblError.Text = "ISP Already Exists!"
                    Exit Sub
                End If
                objXmlNodeClone = objXmlDoc.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST/REQUEST_DETAILS").CloneNode(True)
                objXmlNodeClone.Attributes("Action").Value = "I"
                objXmlNodeClone.Attributes("ISPID").Value = drpIspName.SelectedValue
                objXmlNodeClone.Attributes("ISPName").Value = drpIspName.SelectedItem.Text
                objXmlNodeClone.Attributes("FeasibleStatusID").Value = ""
                objXmlNodeClone.Attributes("FeasibleStatusName").Value = ""
                objXmlNodeClone.Attributes("FeasibleDate").Value = ""
                objXmlNodeClone.Attributes("MailSendDatetime").Value = ""
                objXmlDoc.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").AppendChild(objXmlNodeClone)
                hdData.Value = objXmlDoc.OuterXml
            End If
            ' Binding ISP Details In DataGrid.
            BindGrid()
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region


#Region "btnNew_Click()"
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("ISPUP_Feasibility.aspx?Action=I")
    End Sub
#End Region

#Region "BindDropDownIspName()"
    Sub BindDropDownIspName(ByVal drpDownList As DropDownList, ByVal strType As String, ByVal bolSelect As Boolean)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim objInputXmlAgency, objOutputXmlAgency As New XmlDocument
        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
        Dim objbzISP As New AAMS.bizISP.bzISP
        Dim ds As New DataSet
        Dim objbzCity As New AAMS.bizMaster.bzCity
        Dim objXmlNodeList As XmlNodeList
        Dim objXmlCityNode As XmlNode
        drpDownList.Items.Clear()
        Select Case strType
            Case "ISPLISTByAgencyCity"
                objInputXml.LoadXml("<IS_LISTISP_INPUT><CITY></CITY></IS_LISTISP_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("CITY").InnerXml = drpCity.SelectedItem.Text
                objOutputXml = New XmlDocument
                objOutputXml = objbzISP.List1(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("ISP")
                    drpDownList.DataTextField = "Name"
                    drpDownList.DataValueField = "ISPID"
                    drpDownList.DataBind()
                End If
                If bolSelect = True Then
                    drpDownList.Items.Insert(0, New ListItem("Select One", ""))
                End If
            Case "CITY"
                objOutputXml = New XmlDocument
                If drpCountry.SelectedIndex <> 0 Then
                    objOutputXml = objbzCity.List()
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        objXmlNodeList = objOutputXml.DocumentElement.SelectNodes("CITY[@Country_Name='" + drpCountry.SelectedItem.Text + "']")
                        For Each objXmlCityNode In objXmlNodeList
                            drpDownList.Items.Add(New ListItem(objXmlCityNode.Attributes("City_Name").Value, objXmlCityNode.Attributes("CityID").Value))
                        Next
                    End If
                End If
                If bolSelect = True Then
                    drpDownList.Items.Insert(0, New ListItem("Select One", ""))
                End If
        End Select
    End Sub
#End Region

#Region "BindData()"
    Private Sub BindData()
        Dim strReqId As String = ""
        Dim objFRequest As New AAMS.bizISP.bzISPFeasibleRequest
        Dim objInputXml, objOutputXml, objISPXmlDoc As New XmlDocument
        Try
            strReqId = Request.QueryString("FeasibilityRequestID")
            objInputXml.LoadXml("<ISP_VIEWFEASIBILEREQUEST_INPUT><RequestID></RequestID><Name></Name></ISP_VIEWFEASIBILEREQUEST_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("Name").InnerXml = String.Empty

            '@ Getting  Decrrepted Data 
            Dim DecreptedRequestID As String
            DecreptedRequestID = objED.Decrypt(strReqId.ToString)
            DecreptedRequestID = DecreptedRequestID.Split("|")(0)
            '@ End of Getting Decrepted Data

            'objInputXml.DocumentElement.SelectSingleNode("RequestID").InnerXml = strReqId.Split("|")(0)

            objInputXml.DocumentElement.SelectSingleNode("RequestID").InnerXml = DecreptedRequestID

            'Calling View Method
            'If ViewState("ViewRequest") Is Nothing Then
            '    objOutputXml = objFRequest.View(objInputXml)
            'Else
            '    objOutputXml.LoadXml(ViewState("ViewRequest").ToString())

            'End If

            If hdData.Value = "" Then
                objOutputXml = objFRequest.View(objInputXml)
            Else
                objOutputXml.LoadXml(hdData.Value)
            End If

            ' Removing Blank Node
            'Dim objBlankNode As XmlNode
            'objBlankNode = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST/REQUEST_DETAILS[@ISPName='']")
            'If Not objBlankNode Is Nothing Then
            '    objOutputXml.DocumentElement("FEASIBILE_REQUEST").RemoveChild(objBlankNode)
            'End If

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ' ViewState("ViewRequest") = objOutputXml.OuterXml.ToString()
                hdData.Value = objOutputXml.OuterXml.ToString()
                Dim strAgency As String = ""

                strAgency = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("LCODE").InnerText
                If strAgency = "" Then
                    rbtNewAgency.Checked = True
                    drpCountry.Items.FindByText(objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("COUNTRY").InnerText).Selected = True
                    BindDropDownIspName(drpCity, "CITY", True)
                    '  drpCity.Items.FindByText(objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("CITY").InnerText).Selected = True
                    drpCity.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("CITY").InnerText
                Else
                    rbtExistingAgency.Checked = True
                    txtCountry.Text = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("COUNTRY").InnerText
                    txtCity.Text = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("CityName").InnerText
                    hdCity.Value = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("CITY").InnerText
                    hdCountry.Value = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("COUNTRYID").InnerText
                End If

                hdlcode.Value = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("LCODE").InnerText
                txtAgencyName.Text = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("AGENCYNAME").InnerText
                txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("ADDRESS").InnerText
                txtFax.Text = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("FAX").InnerText
                txtPinCode.Text = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("PINCODE").InnerText
                txtPhone.Text = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("PHONE").InnerText
                txtOfficeId.Text = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("OFFICEID").InnerText
                txtConcernPerson.Text = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("CONCERNED_PERSON").InnerText

                txtRequestId.Text = DecreptedRequestID 'Request.QueryString("FeasibilityRequestID").ToString().Split("|").GetValue(0).ToString()

                'txtLoggedDate.Text = objeAAMS.ConvertDate(objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("LoggedDatetime").InnerText)
                Dim strDate As String = ""
                strDate = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("LoggedDatetime").InnerText
                txtLoggedDate.Text = strDate.Substring(6, 2) + "/" + strDate.Substring(4, 2) + "/" + strDate.Substring(0, 4)
                txtLoggedBy.Text = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("LoggedBy").InnerText
                txtRemarks.Text = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("Remarks").InnerText

                BindGrid()
                ' pnlFStatus.Visible = True
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If



        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
#End Region

#Region "btnUpdate_Click()"
    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim objXmlDoc As New XmlDocument()
        Dim objXmlNode As XmlNode
        Dim strSPID As String = ""
        Dim strDate As String = ""
        Try
            strSPID = drpIspName.SelectedValue
            If hdData.Value <> "" Then
                strDate = txtFeasibilityDate.Text.Trim
                objXmlDoc.LoadXml(hdData.Value)
                objXmlNode = objXmlDoc.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST/REQUEST_DETAILS[@ISPID='" + strSPID + "']")
                objXmlNode.Attributes("FeasibleStatusID").Value = ddlFeasibleStatus.SelectedValue
                objXmlNode.Attributes("FeasibleStatusName").Value = ddlFeasibleStatus.SelectedItem.Text
                If strDate.Trim <> "" Then
                    objXmlNode.Attributes("FeasibleDate").Value = strDate.Split("/")(2) + strDate.Split("/")(1) + strDate.Split("/")(0)
                Else
                    objXmlNode.Attributes("FeasibleDate").Value = ""
                End If
                hdData.Value = objXmlDoc.OuterXml.ToString()
            End If
            BindGrid()
            'lblError.Text = "Feasibility Status Updated."
            ddlFeasibleStatus.SelectedIndex = 0
            drpIspName.SelectedIndex = 0
            drpIspName.Enabled = True
            txtFeasibilityDate.Text = ""
            btnUpdate.Visible = False
            pnlFStatus.Visible = False
            btnAdd.Visible = True
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "gvISPUpdate_RowCommand"
    Protected Sub gvISPUpdate_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvISPUpdate.RowCommand
        Dim strISPID As String = ""
        Dim strFeasibleStatusID As String = ""
        Dim strFeasibleDate As String = ""
        Try
            ' Dim btnMail As LinkButton = CType(e.CommandSource, LinkButton)
            lblError.Text = ""
            If e.CommandName = "UpdateX" Then
                strISPID = e.CommandArgument.ToString()
                strFeasibleDate = strISPID.Split("|")(2)
                drpIspName.SelectedValue = strISPID.Split("|")(0)
                drpIspName.Enabled = False
                ddlFeasibleStatus.SelectedValue = strISPID.Split("|")(1)
                txtFeasibilityDate.Text = ""
                If (strFeasibleDate <> "") Then
                    txtFeasibilityDate.Text = strFeasibleDate.Substring(6, 2) + "/" + strFeasibleDate.Substring(4, 2) + "/" + strFeasibleDate.Substring(0, 4)
                End If
                btnAdd.Visible = False
                pnlFStatus.Visible = True
                btnUpdate.Visible = True
            End If
            If e.CommandName = "DeleteX" Then
                ' Code For Deleting ISP ViewState("ViewRequest").
                Dim objXmlDeleteNode As XmlNode
                Dim objXmlInputDelete As New XmlDocument
                strISPID = e.CommandArgument.ToString()
                If hdData.Value <> "" Then
                    objXmlInputDelete.LoadXml(hdData.Value)
                    'objXmlDeleteNode = objXmlInputDelete.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST/REQUEST_DETAILS[@ISPID='" + strISPID + "']")
                    'If Not objXmlDeleteNode Is Nothing Then
                    '    If objXmlDeleteNode.Attributes("Action").InnerText.ToUpper = "U" Then
                    '        objXmlDeleteNode.Attributes("Action").InnerText = "R"
                    '    ElseIf objXmlDeleteNode.Attributes("Action").InnerText.ToUpper = "I" Then
                    '        objXmlInputDelete.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").RemoveChild(objXmlDeleteNode)
                    '    End If
                    'End If
                    For Each objXmlDeleteNode In objXmlInputDelete.DocumentElement.SelectNodes("FEASIBILE_REQUEST/REQUEST_DETAILS[@ISPID='" + strISPID + "']")
                        If Not objXmlDeleteNode Is Nothing Then
                            If objXmlDeleteNode.Attributes("Action").InnerText.ToUpper = "U" Then
                                objXmlDeleteNode.Attributes("Action").InnerText = "R"
                            ElseIf objXmlDeleteNode.Attributes("Action").InnerText.ToUpper = "I" Then
                                objXmlInputDelete.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").RemoveChild(objXmlDeleteNode)
                            End If
                        End If
                    Next
                    hdData.Value = objXmlInputDelete.OuterXml
                    BindGrid()
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "ConvertDate()"
    Public Function ConvertDate(ByVal strDate As String) As String
        Dim strConvertedDate As String = ""
        Try
            If strDate <> "" And strDate <> "0" Then
                strConvertedDate = strDate.Substring(6, 2) + "/" + strDate.Substring(4, 2) + "/" + strDate.Substring(0, 4)
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
        Return strConvertedDate
    End Function
#End Region

#Region "gvISPUpdate_RowDataBound()"
    Protected Sub gvISPUpdate_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvISPUpdate.RowDataBound
        Try
            Dim strISPID As String = ""
            If (e.Row.RowIndex < 0) Then
                Exit Sub
            End If
            Dim btnMail As LinkButton = e.Row.FindControl("btnMail")
            Dim btnBTMail As LinkButton = e.Row.FindControl("btnBTMail")
            Dim btnUpdate As LinkButton = e.Row.FindControl("btnEdit")
            Dim hdISPID As HiddenField = e.Row.FindControl("hdISPID")
            Dim strReqId As String = ""


            Dim strRowStatus As String = ""
            strRowStatus = DataBinder.Eval(e.Row.DataItem, "Action")

            If strRowStatus = "I" Then
                ' Enabling Disabling Controls
                btnUpdate.Enabled = False
                btnMail.Enabled = False
                btnBTMail.Enabled = False
            Else

                '@ Getting  Decrrepted Data 
                Dim DecreptedRequestID As String
                DecreptedRequestID = objED.Decrypt(Request.QueryString("FeasibilityRequestID").ToString())
                DecreptedRequestID = DecreptedRequestID.Split("|")(0).ToString()
                '@ End of Getting Decrepted Data

                strReqId = DecreptedRequestID 'Request.QueryString("FeasibilityRequestID").Split("|")(0)
                strISPID = hdISPID.Value 'e.Row.FindControl("hdISPID")
                btnMail.Attributes.Add("onclick", "return SendEmail('" + strReqId + "','" + strISPID + "');")
                btnBTMail.Attributes.Add("onclick", "return SendBTEmail('" + strReqId + "','" + strISPID + "');")


                ' Code by Pankaj
                ' Date 08-Jan-2009
                ' Desc : Check security For Update Status.

                '  Dim btnUpdate As LinkButton
                btnUpdate = CType(e.Row.FindControl("btnEdit"), LinkButton)
                Dim objSecurityXml As New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='UPDATE_ISP_FEASIBILITY']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='UPDATE_ISP_FEASIBILITY']").Attributes("Value").Value)
                    End If
                    If strBuilder(2) = "0" Then
                        btnUpdate.Enabled = False
                    Else
                        btnUpdate.Enabled = True
                    End If
                Else
                    btnUpdate.Enabled = True
                End If
            End If




        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "CheckSecurity()::Checking Security Rights."
    Private Sub CheckSecurity()
        Dim objSecurityXml As New XmlDocument
        Try
            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If

            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then

                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspFeasibilityRequest']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspFeasibilityRequest']").Attributes("Value").Value)
                    If Request.QueryString("Action") = "U" Then
                        If strBuilder(0) = "0" Then
                            btnSave.Enabled = False
                            Response.Redirect("../NoRights.aspx")
                        End If
                        If strBuilder(2) = "0" Then
                            btnSave.Enabled = False
                        End If
                    End If

                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub drpCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpCountry.SelectedIndexChanged
        Try
            BindDropDownIspName(drpCity, "CITY", True)
            ' Code For Maintain Sorting Image In New Request Start.
            If gvISPUpdate.Rows.Count > 0 Then
                SetImageForSorting(gvISPUpdate)
            End If
            ' Code For Maintain Sorting Image In New Request End.
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub ShowHideControls()
        Try
            If rbtNewAgency.Checked = True Then
                txtCity.Style.Add("display", "none")
                txtCountry.Style.Add("display", "none")
                drpCity.Style.Add("display", "block")
                drpCountry.Style.Add("display", "block")
                txtAgencyName.ReadOnly = False
                txtAgencyName.CssClass = "textbox"
                txtAddress.ReadOnly = False
                txtAddress.CssClass = "textbox"
                txtFax.ReadOnly = False
                txtFax.CssClass = "textbox"
                txtPhone.ReadOnly = False
                txtPhone.CssClass = "textbox"
                txtConcernPerson.ReadOnly = False
                txtConcernPerson.CssClass = "textbox"
                txtPinCode.ReadOnly = False
                txtPinCode.CssClass = "textbox"
                img1.Style.Add("display", "none")
            Else
                txtCity.Style.Add("display", "block")
                txtCountry.Style.Add("display", "block")
                drpCity.Style.Add("display", "none")
                drpCountry.Style.Add("display", "none")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub UpdateMailDate()
        Dim objOutputXml As New XmlDocument
        Try
            'Start Code For Update Mail Send Date 
            ' Session("ISPMailDate") = Request.QueryString("RequestId") + "|" + Request.QueryString("ISPID")
            If Session("ISPMailDate") IsNot Nothing Then
                If hdData.Value <> "" Then
                    objOutputXml.LoadXml(hdData.Value)
                    objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST/REQUEST_DETAILS[@ISPID='" + Session("ISPMailDate").ToString().Split("|").GetValue(1) + "']").Attributes("MailSendDatetime").InnerText = Session("ISPMailDate").ToString().Split("|").GetValue(2)
                    hdData.Value = objOutputXml.OuterXml.ToString()
                    BindGrid()
                End If
                Session("ISPMailDate") = Nothing
                Exit Sub
            End If
            'End Code For Update Mail Send Date 
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            objOutputXml = Nothing
        End Try
    End Sub

    Protected Sub gvISPUpdate_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvISPUpdate.Sorted
        Try
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvISPUpdate_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvISPUpdate.Sorting
        Dim SortName As String = e.SortExpression
        Try
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ViewState("Desc") = "ASC"
            Else
                If ViewState("SortName") = SortName Then
                    If ViewState("Desc") = "DESC" Then
                        ViewState("Desc") = "ASC"
                    Else
                        ViewState("Desc") = "DESC"
                    End If
                Else
                    ViewState("SortName") = SortName
                    ViewState("Desc") = "ASC"
                End If
            End If
            BindGrid()
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

    Private Sub SetImageForSorting(ByVal grd As GridView)
        Dim imgUp As New Image
        imgUp.ImageUrl = "~/Images/Sortup.gif"
        Dim imgDown As New Image
        imgDown.ImageUrl = "~/Images/Sortdown.gif"
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Dim intcol As Integer = grd.Columns.IndexOf(field)
                If ViewState("Desc") = "ASC" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "DESC" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If
            End If
        Next
    End Sub

    Private Sub BindGrid()
        Dim objXmlReader As XmlNodeReader
        Dim objDS As New DataSet()
        Dim objXmlDoc As New XmlDocument
        Dim dv As DataView
        Try
            If hdData.Value <> "" Then
                objXmlDoc.LoadXml(hdData.Value)
                If objXmlDoc.DocumentElement.SelectNodes("FEASIBILE_REQUEST/REQUEST_DETAILS").Count > 0 Then
                    ' Exit when only one row present and that is blank node.
                    If objXmlDoc.DocumentElement.SelectNodes("FEASIBILE_REQUEST/REQUEST_DETAILS").Count = 1 Then
                        If objXmlDoc.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST/REQUEST_DETAILS").Attributes("ISPName").InnerText = "" Then
                            gvISPUpdate.DataSource = Nothing
                            gvISPUpdate.DataBind()
                            Exit Sub
                        End If
                    End If
                    objXmlReader = New XmlNodeReader(objXmlDoc)
                    objDS.ReadXml(objXmlReader)
                    If objDS.Tables("REQUEST_DETAILS").Rows.Count > 0 Then
                        dv = objDS.Tables("REQUEST_DETAILS").DefaultView
                        dv.RowFilter = "ISPName <> '' and Action <> 'R'"
                        If ViewState("SortName") Is Nothing Then
                            ViewState("SortName") = "ISPName"
                            ViewState("Desc") = "ASC"
                        End If
                        dv.Sort = ViewState("SortName") & " " & ViewState("Desc")
                        gvISPUpdate.DataSource = dv
                        gvISPUpdate.DataBind()
                        SetImageForSorting(gvISPUpdate)
                    Else
                        gvISPUpdate.DataSource = Nothing
                        gvISPUpdate.DataBind()
                    End If
                Else
                    gvISPUpdate.DataSource = Nothing
                    gvISPUpdate.DataBind()
                End If
            Else
                gvISPUpdate.DataSource = Nothing
                gvISPUpdate.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class


'Imports System.Data
'Imports System.Xml
'Partial Class ISP_ISPUP_Feasibility
'    Inherits System.Web.UI.Page
'    Public objeAAMS As New eAAMS
'    Dim objeAAMSMessage As New eAAMSMessage
'    Dim strBuilder As New StringBuilder
'    Dim objED As New EncyrptDeCyrpt
'#Region "Page_Load()"
'    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
'        Try
'            Session("PageName") = Request.Url.ToString()
'            '   Checking Security  Start.
'            CheckSecurity()
'            txtAgencyName.Text = Request.Form("txtAgencyName")
'            txtAddress.Text = Request.Form("txtAddress")
'            txtConcernPerson.Text = Request.Form("txtConcernPerson")
'            txtFax.Text = Request.Form("txtFax")
'            txtPhone.Text = Request.Form("txtPhone")
'            txtPinCode.Text = Request.Form("txtPinCode")
'            txtCity.Text = Request.Form("txtCity")
'            txtCountry.Text = Request.Form("txtCountry")
'            txtOfficeId.Text = Request.Form("txtOfficeId")
'            'Binding City And Country DropDown.
'            If Not Page.IsPostBack Then
'                objeAAMS.BindDropDown(drpCountry, "COUNTRY", True)
'                objeAAMS.BindDropDown(drpIspName, "ISPLIST", True)
'                btnSave.Attributes.Add("onClick", "return ValidateForm();")
'            End If
'            '   Session Clear For Mail. 
'            Session("EmailList") = Nothing
'            '   Code For Insert Start.
'            If Request.QueryString("Action") = "I" Then
'                If (Not Page.IsPostBack) Then
'                    btnUpdate.Visible = False
'                    rbtExistingAgency.Attributes.Add("onClick", "radionewexisting(2);")
'                    rbtNewAgency.Attributes.Add("onClick", "radionewexisting(2);")
'                    btnAdd.Attributes.Add("onClick", "return ValidateISP();")
'                    objeAAMS.BindDropDown(ddlFeasibleStatus, "FeasibilityStatus", True)
'                    ddlFeasibleStatus.SelectedValue = ddlFeasibleStatus.Items.FindByText("Pending").Value
'                    drpCity.Style.Add("display", "none")
'                    drpCountry.Style.Add("display", "none")
'                End If
'                pnlFStatus.Visible = False
'                btnUpdate.Visible = False
'            End If
'            '   Code For Insert End.
'            '   Code For Update Start.
'            If Request.QueryString("Action") = "U" Then
'                If (Not Page.IsPostBack) Then
'                    btnUpdate.Attributes.Add("onClick", "return ValidateISPUpdate();")
'                    btnAdd.Attributes.Add("onClick", "return ValidateISP();")
'                    objeAAMS.BindDropDown(ddlFeasibleStatus, "FeasibilityStatus", True)
'                    rbtNewAgency.Enabled = False
'                    rbtExistingAgency.Enabled = False
'                    BindData()
'                End If
'                '  Code For Mail date Update start
'                If Session("ISPMailDate") IsNot Nothing Then
'                    UpdateMailDate()
'                End If
'                '  Code For Mail date Update end
'            End If
'            '   Code For Displaying Message Start.
'            If Not Request.QueryString("Message") Is Nothing Then
'                If Request.QueryString("Message") = "A" Then
'                    lblError.Text = objeAAMSMessage.messInsert
'                ElseIf Request.QueryString("Message") = "U" Then
'                    lblError.Text = objeAAMSMessage.messUpdate
'                End If
'            End If
'            If Not Request.QueryString("Reset") Is Nothing Then
'                lblError.Text = ""
'            End If
'            '   Code For Displaying Message End.
'            '   Show Hide Controls.
'            ShowHideControls()
'        Catch ex As Exception
'            lblError.Text = ex.Message
'        End Try
'    End Sub
'#End Region

'#Region "btnSave_Click"
'    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
'        Dim objFRequest As New AAMS.bizISP.bzISPFeasibleRequest
'        Dim objInputXml, objOutputXml, objISPXmlDoc As New XmlDocument
'        Dim objXmlNode As XmlNode
'        Try
'            If hdData.Value = "" Then
'                objInputXml.LoadXml("<ISP_UPDATEFEASIBILEREQUEST_INPUT><FEASIBILE_REQUEST LCODE='' CityID='' RequestID='' LoggedDatetime='' LoggedBy='' Remarks='' AGENCYNAME='' ADDRESS='' COUNTRYID='' FAX='' PHONE='' PINCODE='' CONCERNED_PERSON=''></FEASIBILE_REQUEST></ISP_UPDATEFEASIBILEREQUEST_INPUT>")
'            Else
'                objInputXml.LoadXml(hdData.Value)
'            End If
'            ' objInputXml.LoadXml("<ISP_UPDATEFEASIBILEREQUEST_INPUT><FEASIBILE_REQUEST LCODE='' CityID='' RequestID='' LoggedDatetime='' LoggedBy='' Remarks='' AGENCYNAME='' ADDRESS='' COUNTRYID='' FAX='' PHONE='' PINCODE='' CONCERNED_PERSON=''></FEASIBILE_REQUEST></ISP_UPDATEFEASIBILEREQUEST_INPUT>")
'            objXmlNode = objInputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST")
'            If (rbtNewAgency.Checked = True) Then
'                objXmlNode.Attributes("LCODE").Value = ""
'                objXmlNode.Attributes("CityID").Value = drpCity.SelectedValue
'                objXmlNode.Attributes("COUNTRYID").Value = drpCountry.SelectedValue
'            Else
'                objXmlNode.Attributes("LCODE").Value = hdlcode.Value
'                objXmlNode.Attributes("CityID").Value = hdCity.Value
'                objXmlNode.Attributes("COUNTRYID").Value = hdCountry.Value
'            End If
'            ' Adding Agency Details
'            objXmlNode.Attributes("AGENCYNAME").Value = txtAgencyName.Text
'            objXmlNode.Attributes("ADDRESS").Value = txtAddress.Text
'            objXmlNode.Attributes("FAX").Value = txtFax.Text
'            objXmlNode.Attributes("PHONE").Value = txtPhone.Text
'            objXmlNode.Attributes("PINCODE").Value = txtPinCode.Text
'            objXmlNode.Attributes("CONCERNED_PERSON").Value = txtConcernPerson.Text
'            objXmlNode.Attributes("RequestID").Value = txtRequestId.Text
'            objXmlNode.Attributes("LoggedDatetime").Value = txtLoggedDate.Text
'            objXmlNode.Attributes("LoggedBy").Value = Session("LoginSession").Split("|").GetValue(0).ToString()
'            objXmlNode.Attributes("Remarks").Value = txtRemarks.Text
'            ' Removing Blank Node.
'            Dim objBlankNode As XmlNode
'            objBlankNode = objInputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST/REQUEST_DETAILS[@ISPName='']")
'            If Not objBlankNode Is Nothing Then
'                objInputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").RemoveChild(objBlankNode)
'            End If
'            ' Calling Back End Method For Update.
'            objOutputXml = objFRequest.Update(objInputXml)
'            ' Checking Error Statuus.
'            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
'                Dim strRequestID As String = ""
'                strRequestID = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("RequestID").Value
'                '@ Code Used For Encription/Decription
'                Dim EnstrQueryStringForstrRequestID As String
'                EnstrQueryStringForstrRequestID = objED.Encrypt(strRequestID)
'                '@ End of Code Used For Encription/Decription
'                If Request.QueryString("Action") = "I" Then
'                    Response.Redirect("ISPUP_Feasibility.aspx?Action=U&FeasibilityRequestID=" + EnstrQueryStringForstrRequestID + "&Message=A")
'                Else
'                    Response.Redirect("ISPUP_Feasibility.aspx?Action=U&FeasibilityRequestID=" + EnstrQueryStringForstrRequestID + "&Message=U")
'                End If
'            Else
'                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
'            End If
'        Catch ex As Exception
'            lblError.Text = ex.Message
'        End Try
'    End Sub
'#End Region

'#Region "btnReset_Click"
'    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
'        'If Request.QueryString("Action") = "I" Then
'        '    Response.Redirect("ISPUP_Feasibility.aspx?Action=I")
'        'ElseIf Request.QueryString("Action") = "U" Then
'        '    ViewState("ViewRequest") = Nothing
'        '    BindData()
'        '    lblError.Text = ""
'        'End If
'        'Response.Redirect("ISPUP_Feasibility.aspx?" + Request.QueryString().ToString())
'        Response.Redirect("ISPUP_Feasibility.aspx?" + Request.QueryString().ToString() + "&Reset=1")

'    End Sub
'#End Region

'#Region "btnAdd_Click()"
'    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
'        Dim objDS As New DataSet()
'        Dim objXmlDoc As New XmlDocument
'        Dim objXmlNode As XmlNode
'        Dim objXmlNodeClone As XmlNode
'        Dim objXmlNodeChk As XmlNode
'        Try
'            If hdData.Value = "" Then
'                ' <ISP_UPDATEFEASIBILEREQUEST_INPUT><FEASIBILE_REQUEST LCODE='' CityID='' RequestID='' LoggedDatetime='' LoggedBy='' Remarks='' AGENCYNAME='' ADDRESS='' COUNTRYID='' FAX='' PHONE='' PINCODE='' CONCERNED_PERSON=''><REQUEST_DETAILS Action='' ISPID='' ISPName='' FeasibleStatusID='' FeasibleStatusName='' FeasibleDate='' MailSendDatetime=''/></FEASIBILE_REQUEST></ISP_UPDATEFEASIBILEREQUEST_INPUT>
'                'objXmlDoc.LoadXml("<ISP><REQUEST_DETAILS Action='' ISPID='' ISPName=''  FeasibleStatusID='' FeasibleStatusName='' FeasibleDate='' MailSendDatetime='' /></ISP>")
'                objXmlDoc.LoadXml("<ISP_UPDATEFEASIBILEREQUEST_INPUT><FEASIBILE_REQUEST LCODE='' CityID='' RequestID='' LoggedDatetime='' LoggedBy='' Remarks='' AGENCYNAME='' ADDRESS='' COUNTRYID='' FAX='' PHONE='' PINCODE='' CONCERNED_PERSON=''><REQUEST_DETAILS Action='' ISPID='' ISPName='' FeasibleStatusID='' FeasibleStatusName='' FeasibleDate='' MailSendDatetime=''/></FEASIBILE_REQUEST></ISP_UPDATEFEASIBILEREQUEST_INPUT>")
'                objXmlNode = objXmlDoc.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST/REQUEST_DETAILS")
'                objXmlNode.Attributes("Action").Value = "I"
'                objXmlNode.Attributes("ISPID").Value = drpIspName.SelectedValue
'                objXmlNode.Attributes("ISPName").Value = drpIspName.SelectedItem.Text
'                hdData.Value = objXmlDoc.OuterXml
'            Else
'                objXmlDoc.LoadXml(hdData.Value)
'                ' Adding Blank Node
'                If objXmlDoc.DocumentElement.SelectNodes("FEASIBILE_REQUEST/REQUEST_DETAILS").Count = 0 Then
'                    Dim objDocFrag As XmlDocumentFragment
'                    Dim objXmlDoc1 As New XmlDocument
'                    objXmlDoc1.LoadXml("<Root><REQUEST_DETAILS Action='' ISPID='' ISPName='' FeasibleStatusID='' FeasibleStatusName='' FeasibleDate='' MailSendDatetime=''/></Root>")
'                    objDocFrag = objXmlDoc.CreateDocumentFragment()
'                    objDocFrag.InnerXml = objXmlDoc1.DocumentElement.InnerXml
'                    objXmlDoc.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").AppendChild(objDocFrag)
'                    '  hdData.Value = objXmlDoc.OuterXml
'                End If
'                objXmlNodeChk = objXmlDoc.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST/REQUEST_DETAILS[@ISPID='" + drpIspName.SelectedValue + "'][@Action != 'R']")
'                ' Check Duplicate ISP
'                If Not objXmlNodeChk Is Nothing Then
'                    lblError.Text = "ISP Already Exists!"
'                    Exit Sub
'                End If
'                objXmlNodeClone = objXmlDoc.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST/REQUEST_DETAILS").CloneNode(True)
'                objXmlNodeClone.Attributes("Action").Value = "I"
'                objXmlNodeClone.Attributes("ISPID").Value = drpIspName.SelectedValue
'                objXmlNodeClone.Attributes("ISPName").Value = drpIspName.SelectedItem.Text
'                objXmlNodeClone.Attributes("FeasibleStatusID").Value = ""
'                objXmlNodeClone.Attributes("FeasibleStatusName").Value = ""
'                objXmlNodeClone.Attributes("FeasibleDate").Value = ""
'                objXmlNodeClone.Attributes("MailSendDatetime").Value = ""
'                objXmlDoc.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").AppendChild(objXmlNodeClone)
'                hdData.Value = objXmlDoc.OuterXml
'            End If
'            ' Binding ISP Details In DataGrid.
'            BindGrid()
'            lblError.Text = ""
'        Catch ex As Exception
'            lblError.Text = ex.Message
'        End Try
'    End Sub
'#End Region


'#Region "btnNew_Click()"
'    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
'        Response.Redirect("ISPUP_Feasibility.aspx?Action=I")
'    End Sub
'#End Region

'#Region "BindDropDownIspName()"
'    Sub BindDropDownIspName(ByVal drpDownList As DropDownList, ByVal strType As String, ByVal bolSelect As Boolean)
'        Dim objInputXml, objOutputXml As New XmlDocument
'        Dim objXmlReader As XmlNodeReader
'        Dim objInputXmlAgency, objOutputXmlAgency As New XmlDocument
'        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
'        Dim objbzISP As New AAMS.bizISP.bzISP
'        Dim ds As New DataSet
'        Dim objbzCity As New AAMS.bizMaster.bzCity
'        Dim objXmlNodeList As XmlNodeList
'        Dim objXmlCityNode As XmlNode
'        drpDownList.Items.Clear()
'        Select Case strType
'            Case "ISPLISTByAgencyCity"
'                objInputXml.LoadXml("<IS_LISTISP_INPUT><CITY></CITY></IS_LISTISP_INPUT>")
'                objInputXml.DocumentElement.SelectSingleNode("CITY").InnerXml = drpCity.SelectedItem.Text
'                objOutputXml = New XmlDocument
'                objOutputXml = objbzISP.List1(objInputXml)
'                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
'                    objXmlReader = New XmlNodeReader(objOutputXml)
'                    ds.ReadXml(objXmlReader)
'                    drpDownList.DataSource = ds.Tables("ISP")
'                    drpDownList.DataTextField = "Name"
'                    drpDownList.DataValueField = "ISPID"
'                    drpDownList.DataBind()
'                End If
'                If bolSelect = True Then
'                    drpDownList.Items.Insert(0, New ListItem("Select One", ""))
'                End If
'            Case "CITY"
'                objOutputXml = New XmlDocument
'                If drpCountry.SelectedIndex <> 0 Then
'                    objOutputXml = objbzCity.List()
'                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
'                        objXmlNodeList = objOutputXml.DocumentElement.SelectNodes("CITY[@Country_Name='" + drpCountry.SelectedItem.Text + "']")
'                        For Each objXmlCityNode In objXmlNodeList
'                            drpDownList.Items.Add(New ListItem(objXmlCityNode.Attributes("City_Name").Value, objXmlCityNode.Attributes("CityID").Value))
'                        Next
'                    End If
'                End If
'                If bolSelect = True Then
'                    drpDownList.Items.Insert(0, New ListItem("Select One", ""))
'                End If
'        End Select
'    End Sub
'#End Region

'#Region "BindData()"
'    Private Sub BindData()
'        Dim strReqId As String = ""
'        Dim objFRequest As New AAMS.bizISP.bzISPFeasibleRequest
'        Dim objInputXml, objOutputXml, objISPXmlDoc As New XmlDocument
'        Try
'            strReqId = Request.QueryString("FeasibilityRequestID")
'            objInputXml.LoadXml("<ISP_VIEWFEASIBILEREQUEST_INPUT><RequestID></RequestID><Name></Name></ISP_VIEWFEASIBILEREQUEST_INPUT>")
'            objInputXml.DocumentElement.SelectSingleNode("Name").InnerXml = String.Empty

'            '@ Getting  Decrrepted Data 
'            Dim DecreptedRequestID As String
'            DecreptedRequestID = objED.Decrypt(strReqId.ToString)
'            DecreptedRequestID = DecreptedRequestID.Split("|")(0)
'            '@ End of Getting Decrepted Data

'            'objInputXml.DocumentElement.SelectSingleNode("RequestID").InnerXml = strReqId.Split("|")(0)

'            objInputXml.DocumentElement.SelectSingleNode("RequestID").InnerXml = DecreptedRequestID

'            'Calling View Method
'            'If ViewState("ViewRequest") Is Nothing Then
'            '    objOutputXml = objFRequest.View(objInputXml)
'            'Else
'            '    objOutputXml.LoadXml(ViewState("ViewRequest").ToString())

'            'End If

'            If hdData.Value = "" Then
'                objOutputXml = objFRequest.View(objInputXml)
'            Else
'                objOutputXml.LoadXml(hdData.Value)
'            End If

'            ' Removing Blank Node
'            'Dim objBlankNode As XmlNode
'            'objBlankNode = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST/REQUEST_DETAILS[@ISPName='']")
'            'If Not objBlankNode Is Nothing Then
'            '    objOutputXml.DocumentElement("FEASIBILE_REQUEST").RemoveChild(objBlankNode)
'            'End If

'            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
'                ' ViewState("ViewRequest") = objOutputXml.OuterXml.ToString()
'                hdData.Value = objOutputXml.OuterXml.ToString()
'                Dim strAgency As String = ""

'                strAgency = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("LCODE").InnerText
'                If strAgency = "" Then
'                    rbtNewAgency.Checked = True
'                    drpCountry.Items.FindByText(objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("COUNTRY").InnerText).Selected = True
'                    BindDropDownIspName(drpCity, "CITY", True)
'                    '  drpCity.Items.FindByText(objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("CITY").InnerText).Selected = True
'                    drpCity.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("CITY").InnerText
'                Else
'                    rbtExistingAgency.Checked = True
'                    txtCountry.Text = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("COUNTRY").InnerText
'                    txtCity.Text = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("CityName").InnerText
'                    hdCity.Value = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("CITY").InnerText
'                    hdCountry.Value = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("COUNTRYID").InnerText
'                End If

'                hdlcode.Value = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("LCODE").InnerText
'                txtAgencyName.Text = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("AGENCYNAME").InnerText
'                txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("ADDRESS").InnerText
'                txtFax.Text = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("FAX").InnerText
'                txtPinCode.Text = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("PINCODE").InnerText
'                txtPhone.Text = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("PHONE").InnerText
'                txtOfficeId.Text = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("OFFICEID").InnerText
'                txtConcernPerson.Text = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("CONCERNED_PERSON").InnerText

'                txtRequestId.Text = DecreptedRequestID 'Request.QueryString("FeasibilityRequestID").ToString().Split("|").GetValue(0).ToString()

'                'txtLoggedDate.Text = objeAAMS.ConvertDate(objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("LoggedDatetime").InnerText)
'                Dim strDate As String = ""
'                strDate = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("LoggedDatetime").InnerText
'                txtLoggedDate.Text = strDate.Substring(6, 2) + "/" + strDate.Substring(4, 2) + "/" + strDate.Substring(0, 4)
'                txtLoggedBy.Text = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("LoggedBy").InnerText
'                txtRemarks.Text = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("Remarks").InnerText

'                BindGrid()
'                ' pnlFStatus.Visible = True
'            Else
'                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
'            End If



'        Catch ex As Exception
'            lblError.Text = ex.Message
'        End Try

'    End Sub
'#End Region

'#Region "btnUpdate_Click()"
'    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
'        Dim objXmlDoc As New XmlDocument()
'        Dim objXmlNode As XmlNode
'        Dim strSPID As String = ""
'        Dim strDate As String = ""
'        Try
'            strSPID = drpIspName.SelectedValue
'            If hdData.Value <> "" Then
'                strDate = txtFeasibilityDate.Text.Trim
'                objXmlDoc.LoadXml(hdData.Value)
'                objXmlNode = objXmlDoc.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST/REQUEST_DETAILS[@ISPID='" + strSPID + "']")
'                objXmlNode.Attributes("FeasibleStatusID").Value = ddlFeasibleStatus.SelectedValue
'                objXmlNode.Attributes("FeasibleStatusName").Value = ddlFeasibleStatus.SelectedItem.Text
'                If strDate.Trim <> "" Then
'                    objXmlNode.Attributes("FeasibleDate").Value = strDate.Split("/")(2) + strDate.Split("/")(1) + strDate.Split("/")(0)
'                Else
'                    objXmlNode.Attributes("FeasibleDate").Value = ""
'                End If
'                hdData.Value = objXmlDoc.OuterXml.ToString()
'            End If
'            BindGrid()
'            'lblError.Text = "Feasibility Status Updated."
'            ddlFeasibleStatus.SelectedIndex = 0
'            drpIspName.SelectedIndex = 0
'            drpIspName.Enabled = True
'            txtFeasibilityDate.Text = ""
'            btnUpdate.Visible = False
'            pnlFStatus.Visible = False
'            btnAdd.Visible = True
'            lblError.Text = ""
'        Catch ex As Exception
'            lblError.Text = ex.Message
'        End Try
'    End Sub
'#End Region

'#Region "gvISPUpdate_RowCommand"
'    Protected Sub gvISPUpdate_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvISPUpdate.RowCommand
'        Dim strISPID As String = ""
'        Dim strFeasibleStatusID As String = ""
'        Dim strFeasibleDate As String = ""
'        Try
'            ' Dim btnMail As LinkButton = CType(e.CommandSource, LinkButton)
'            lblError.Text = ""
'            If e.CommandName = "UpdateX" Then
'                strISPID = e.CommandArgument.ToString()
'                strFeasibleDate = strISPID.Split("|")(2)
'                drpIspName.SelectedValue = strISPID.Split("|")(0)
'                drpIspName.Enabled = False
'                ddlFeasibleStatus.SelectedValue = strISPID.Split("|")(1)
'                txtFeasibilityDate.Text = ""
'                If (strFeasibleDate <> "") Then
'                    txtFeasibilityDate.Text = strFeasibleDate.Substring(6, 2) + "/" + strFeasibleDate.Substring(4, 2) + "/" + strFeasibleDate.Substring(0, 4)
'                End If
'                btnAdd.Visible = False
'                pnlFStatus.Visible = True
'                btnUpdate.Visible = True
'            End If
'            If e.CommandName = "DeleteX" Then
'                ' Code For Deleting ISP ViewState("ViewRequest").
'                Dim objXmlDeleteNode As XmlNode
'                Dim objXmlInputDelete As New XmlDocument
'                strISPID = e.CommandArgument.ToString()
'                If hdData.Value <> "" Then
'                    objXmlInputDelete.LoadXml(hdData.Value)
'                    'objXmlDeleteNode = objXmlInputDelete.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST/REQUEST_DETAILS[@ISPID='" + strISPID + "']")
'                    'If Not objXmlDeleteNode Is Nothing Then
'                    '    If objXmlDeleteNode.Attributes("Action").InnerText.ToUpper = "U" Then
'                    '        objXmlDeleteNode.Attributes("Action").InnerText = "R"
'                    '    ElseIf objXmlDeleteNode.Attributes("Action").InnerText.ToUpper = "I" Then
'                    '        objXmlInputDelete.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").RemoveChild(objXmlDeleteNode)
'                    '    End If
'                    'End If
'                    For Each objXmlDeleteNode In objXmlInputDelete.DocumentElement.SelectNodes("FEASIBILE_REQUEST/REQUEST_DETAILS[@ISPID='" + strISPID + "']")
'                        If Not objXmlDeleteNode Is Nothing Then
'                            If objXmlDeleteNode.Attributes("Action").InnerText.ToUpper = "U" Then
'                                objXmlDeleteNode.Attributes("Action").InnerText = "R"
'                            ElseIf objXmlDeleteNode.Attributes("Action").InnerText.ToUpper = "I" Then
'                                objXmlInputDelete.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").RemoveChild(objXmlDeleteNode)
'                            End If
'                        End If
'                    Next
'                    hdData.Value = objXmlInputDelete.OuterXml
'                    BindGrid()
'                End If
'            End If
'        Catch ex As Exception
'            lblError.Text = ex.Message
'        End Try
'    End Sub
'#End Region

'#Region "ConvertDate()"
'    Public Function ConvertDate(ByVal strDate As String) As String
'        Dim strConvertedDate As String = ""
'        Try
'            If strDate <> "" And strDate <> "0" Then
'                strConvertedDate = strDate.Substring(6, 2) + "/" + strDate.Substring(4, 2) + "/" + strDate.Substring(0, 4)
'            End If

'        Catch ex As Exception
'            lblError.Text = ex.Message
'        End Try
'        Return strConvertedDate
'    End Function
'#End Region

'#Region "gvISPUpdate_RowDataBound()"
'    Protected Sub gvISPUpdate_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvISPUpdate.RowDataBound
'        Try
'            Dim strISPID As String = ""
'            If (e.Row.RowIndex < 0) Then
'                Exit Sub
'            End If
'            Dim btnMail As LinkButton = e.Row.FindControl("btnMail")
'            Dim btnBTMail As LinkButton = e.Row.FindControl("btnBTMail")
'            Dim btnUpdate As LinkButton = e.Row.FindControl("btnEdit")
'            Dim hdISPID As HiddenField = e.Row.FindControl("hdISPID")
'            Dim strReqId As String = ""


'            Dim strRowStatus As String = ""
'            strRowStatus = DataBinder.Eval(e.Row.DataItem, "Action")

'            If strRowStatus = "I" Then
'                ' Enabling Disabling Controls
'                btnUpdate.Enabled = False
'                btnMail.Enabled = False
'                btnBTMail.Enabled = False
'            Else

'                '@ Getting  Decrrepted Data 
'                Dim DecreptedRequestID As String
'                DecreptedRequestID = objED.Decrypt(Request.QueryString("FeasibilityRequestID").ToString())
'                DecreptedRequestID = DecreptedRequestID.Split("|")(0).ToString()
'                '@ End of Getting Decrepted Data

'                strReqId = DecreptedRequestID 'Request.QueryString("FeasibilityRequestID").Split("|")(0)
'                strISPID = hdISPID.Value 'e.Row.FindControl("hdISPID")
'                btnMail.Attributes.Add("onclick", "return SendEmail('" + strReqId + "','" + strISPID + "');")
'                btnBTMail.Attributes.Add("onclick", "return SendBTEmail('" + strReqId + "','" + strISPID + "');")


'                ' Code by Pankaj
'                ' Date 08-Jan-2009
'                ' Desc : Check security For Update Status.

'                '  Dim btnUpdate As LinkButton
'                btnUpdate = CType(e.Row.FindControl("btnEdit"), LinkButton)
'                Dim objSecurityXml As New XmlDocument
'                objSecurityXml.LoadXml(Session("Security"))
'                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
'                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='UPDATE_ISP_FEASIBILITY']").Count <> 0 Then
'                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='UPDATE_ISP_FEASIBILITY']").Attributes("Value").Value)
'                    End If
'                    If strBuilder(2) = "0" Then
'                        btnUpdate.Enabled = False
'                    Else
'                        btnUpdate.Enabled = True
'                    End If
'                Else
'                    btnUpdate.Enabled = True
'                End If
'            End If




'        Catch ex As Exception
'            lblError.Text = ex.Message
'        End Try
'    End Sub
'#End Region

'#Region "CheckSecurity()::Checking Security Rights."
'    Private Sub CheckSecurity()
'        Dim objSecurityXml As New XmlDocument
'        Try
'            ' This code is used for checking session handler according to user login.
'            If Session("Security") Is Nothing Then
'                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
'            End If

'            objSecurityXml.LoadXml(Session("Security"))
'            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then

'                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspFeasibilityRequest']").Count <> 0 Then
'                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspFeasibilityRequest']").Attributes("Value").Value)
'                    If Request.QueryString("Action") = "U" Then
'                        If strBuilder(0) = "0" Then
'                            btnSave.Enabled = False
'                            Response.Redirect("../NoRights.aspx")
'                        End If
'                        If strBuilder(2) = "0" Then
'                            btnSave.Enabled = False
'                        End If
'                    End If

'                    If strBuilder(1) = "0" Then
'                        btnNew.Enabled = False
'                    End If
'                End If
'            Else
'                strBuilder = objeAAMS.SecurityCheck(31)
'            End If
'        Catch ex As Exception
'            lblError.Text = ex.Message
'        End Try
'    End Sub
'#End Region

'    Protected Sub drpCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpCountry.SelectedIndexChanged
'        Try
'            BindDropDownIspName(drpCity, "CITY", True)
'            ' Code For Maintain Sorting Image In New Request Start.
'            If gvISPUpdate.Rows.Count > 0 Then
'                SetImageForSorting(gvISPUpdate)
'            End If
'            ' Code For Maintain Sorting Image In New Request End.
'        Catch ex As Exception
'            lblError.Text = ex.Message
'        End Try
'    End Sub

'    Private Sub ShowHideControls()
'        Try
'            If rbtNewAgency.Checked = True Then
'                txtCity.Style.Add("display", "none")
'                txtCountry.Style.Add("display", "none")
'                drpCity.Style.Add("display", "block")
'                drpCountry.Style.Add("display", "block")
'                txtAgencyName.ReadOnly = False
'                txtAgencyName.CssClass = "textbox"
'                txtAddress.ReadOnly = False
'                txtAddress.CssClass = "textbox"
'                txtFax.ReadOnly = False
'                txtFax.CssClass = "textbox"
'                txtPhone.ReadOnly = False
'                txtPhone.CssClass = "textbox"
'                txtConcernPerson.ReadOnly = False
'                txtConcernPerson.CssClass = "textbox"
'                txtPinCode.ReadOnly = False
'                txtPinCode.CssClass = "textbox"
'                img1.Style.Add("display", "none")
'            Else
'                txtCity.Style.Add("display", "block")
'                txtCountry.Style.Add("display", "block")
'                drpCity.Style.Add("display", "none")
'                drpCountry.Style.Add("display", "none")
'            End If
'        Catch ex As Exception
'            lblError.Text = ex.Message
'        End Try
'    End Sub

'    Private Sub UpdateMailDate()
'        Dim objOutputXml As New XmlDocument
'        Try
'            'Start Code For Update Mail Send Date 
'            ' Session("ISPMailDate") = Request.QueryString("RequestId") + "|" + Request.QueryString("ISPID")
'            If Session("ISPMailDate") IsNot Nothing Then
'                If hdData.Value <> "" Then
'                    objOutputXml.LoadXml(hdData.Value)
'                    objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST/REQUEST_DETAILS[@ISPID='" + Session("ISPMailDate").ToString().Split("|").GetValue(1) + "']").Attributes("MailSendDatetime").InnerText = Session("ISPMailDate").ToString().Split("|").GetValue(2)
'                    hdData.Value = objOutputXml.OuterXml.ToString()
'                    BindGrid()
'                End If
'                Session("ISPMailDate") = Nothing
'                Exit Sub
'            End If
'            'End Code For Update Mail Send Date 
'        Catch ex As Exception
'            lblError.Text = ex.Message
'        Finally
'            objOutputXml = Nothing
'        End Try
'    End Sub

'    Protected Sub gvISPUpdate_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvISPUpdate.Sorted
'        Try
'        Catch ex As Exception
'            lblError.Text = ex.Message
'        End Try
'    End Sub

'    Protected Sub gvISPUpdate_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvISPUpdate.Sorting
'        Dim SortName As String = e.SortExpression
'        Try
'            If ViewState("SortName") Is Nothing Then
'                ViewState("SortName") = SortName
'                ViewState("Desc") = "ASC"
'            Else
'                If ViewState("SortName") = SortName Then
'                    If ViewState("Desc") = "DESC" Then
'                        ViewState("Desc") = "ASC"
'                    Else
'                        ViewState("Desc") = "DESC"
'                    End If
'                Else
'                    ViewState("SortName") = SortName
'                    ViewState("Desc") = "ASC"
'                End If
'            End If
'            BindGrid()
'        Catch ex As Exception
'            lblError.Text = ex.Message
'        End Try
'    End Sub
'#Region "Code for Filter "
'    'Code Added by Abhishek
'    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
'        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
'            If Response.ContentType = "text/html" Then
'                Response.Filter = New TrimStream(Response.Filter)
'            End If
'        End If
'    End Sub
'    'Code Added by Abhishek
'#End Region

'    Private Sub SetImageForSorting(ByVal grd As GridView)
'        Dim imgUp As New Image
'        imgUp.ImageUrl = "~/Images/Sortup.gif"
'        Dim imgDown As New Image
'        imgDown.ImageUrl = "~/Images/Sortdown.gif"
'        Dim field As DataControlField
'        For Each field In grd.Columns
'            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
'                Dim intcol As Integer = grd.Columns.IndexOf(field)
'                If ViewState("Desc") = "ASC" Then
'                    grd.HeaderRow.Cells(intcol).Controls.Add(imgUp)
'                End If
'                If ViewState("Desc") = "DESC" Then
'                    grd.HeaderRow.Cells(intcol).Controls.Add(imgDown)
'                End If
'            End If
'        Next
'    End Sub

'    Private Sub BindGrid()
'        Dim objXmlReader As XmlNodeReader
'        Dim objDS As New DataSet()
'        Dim objXmlDoc As New XmlDocument
'        Dim dv As DataView
'        Try
'            If hdData.Value <> "" Then
'                objXmlDoc.LoadXml(hdData.Value)
'                If objXmlDoc.DocumentElement.SelectNodes("FEASIBILE_REQUEST/REQUEST_DETAILS").Count > 0 Then
'                    ' Exit when only one row present and that is blank node.
'                    If objXmlDoc.DocumentElement.SelectNodes("FEASIBILE_REQUEST/REQUEST_DETAILS").Count = 1 Then
'                        If objXmlDoc.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST/REQUEST_DETAILS").Attributes("ISPName").InnerText = "" Then
'                            gvISPUpdate.DataSource = Nothing
'                            gvISPUpdate.DataBind()
'                            Exit Sub
'                        End If
'                    End If
'                    objXmlReader = New XmlNodeReader(objXmlDoc)
'                    objDS.ReadXml(objXmlReader)
'                    If objDS.Tables("REQUEST_DETAILS").Rows.Count > 0 Then
'                        dv = objDS.Tables("REQUEST_DETAILS").DefaultView
'                        dv.RowFilter = "ISPName <> '' and Action <> 'R'"
'                        If ViewState("SortName") Is Nothing Then
'                            ViewState("SortName") = "ISPName"
'                            ViewState("Desc") = "ASC"
'                        End If
'                        dv.Sort = ViewState("SortName") & " " & ViewState("Desc")
'                        gvISPUpdate.DataSource = dv
'                        gvISPUpdate.DataBind()
'                        SetImageForSorting(gvISPUpdate)
'                    Else
'                        gvISPUpdate.DataSource = Nothing
'                        gvISPUpdate.DataBind()
'                    End If
'                Else
'                    gvISPUpdate.DataSource = Nothing
'                    gvISPUpdate.DataBind()
'                End If
'            Else
'                gvISPUpdate.DataSource = Nothing
'                gvISPUpdate.DataBind()
'            End If
'        Catch ex As Exception
'            lblError.Text = ex.Message
'        End Try
'    End Sub
'End Class
