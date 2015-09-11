Imports System.Xml
Imports System.Data

Partial Class Setup_MSUP_ManageAirLine
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Public strBuilder As New StringBuilder
    Dim objEn As New EncyrptDeCyrpt


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
            lblError.Text = String.Empty

            btnSave.Attributes.Add("onclick", "return CheckMendatoty();")
            btnNew.Attributes.Add("onclick", "return NewFunction();")

            If Not Page.IsPostBack Then
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Then
                    txtAirlineCode.CssClass = "textboxgrey"
                    txtAirlineCode.ReadOnly = True
                    ViewAirline()
                    If Request.QueryString("Msg").ToString().ToUpper() = "I" Then
                        lblError.Text = objeAAMSMessage.messInsert
                    End If
                Else
                    txtAirlineCode.CssClass = "textbox"
                    txtAirlineCode.ReadOnly = False
                End If
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                    Response.Redirect("MSUP_ManageAirLine.aspx?Msg=I&Action=U|" + Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString().ToUpper())
                    'lblError.Text = objeAAMSMessage.messInsert
                    'ViewAirline()
                End If
            End If


            '***************************************************************************************
            'Code of Security Check
            'objeAAMS.ExpirePageCache()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Airline']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Airline']").Attributes("Value").Value)

                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                    End If

                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                        btnSave.Enabled = False
                    End If
                    If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Then
                        If strBuilder(2) = "0" Then
                            btnSave.Enabled = False
                        Else
                            btnSave.Enabled = True
                        End If
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    ' Load Data For Editing Mode.
    Sub ViewAirline()
        If (Not Request.QueryString("Action") = Nothing) Then
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim ds As New DataSet
            Dim objbzAirline As New AAMS.bizMaster.bzAirline
            objInputXml.LoadXml("<MS_VIEWAIRLINE_INPUT><Airline_Code></Airline_Code></MS_VIEWAIRLINE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("Airline_Code").InnerText = objEn.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString.Trim)

            'Here Back end Method Call
            objOutputXml = objbzAirline.View(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                BindCityData(objOutputXml)
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        End If
    End Sub

    Sub BindCityData(ByVal objOutputXml As XmlDocument)
        With objOutputXml.DocumentElement.SelectSingleNode("AIRLINE")
            ViewState("AirlineCode") = .Attributes("Airline_Code").InnerText
            txtAirlineCode.Text = .Attributes("Airline_Code").InnerText
            txtAirlineName.Text = .Attributes("Name").InnerText
            chekOnlineCarrier.Checked = .Attributes("Online_Carrier").InnerText
        End With
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If (IsValid) Then
            If (Not Request.QueryString("Action") = Nothing) Then

                Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                'Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                Dim objbzAirline As New AAMS.bizMaster.bzAirline
                objInputXml.LoadXml("<MS_UPDATEAIRLINE_INPUT><AIRLINE Action='' Airline_Code='' Name='' Online_Carrier='' /></MS_UPDATEAIRLINE_INPUT>")

                If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "US") Then
                    objInputXml.DocumentElement.SelectSingleNode("AIRLINE").Attributes("Action").Value = "U"
                    objInputXml.DocumentElement.SelectSingleNode("AIRLINE").Attributes("Airline_Code").Value = ViewState("AirlineCode")
                Else
                    objInputXml.DocumentElement.SelectSingleNode("AIRLINE").Attributes("Action").Value = "I"
                    objInputXml.DocumentElement.SelectSingleNode("AIRLINE").Attributes("Airline_Code").Value = txtAirlineCode.Text.ToString()
                End If

                objInputXml.DocumentElement.SelectSingleNode("AIRLINE").Attributes("Name").Value = txtAirlineName.Text.ToString()
                objInputXml.DocumentElement.SelectSingleNode("AIRLINE").Attributes("Online_Carrier").Value = IIf(chekOnlineCarrier.Checked = False, 0, 1)
                
                'Here Back end Method Call
                objOutputXml = objbzAirline.Update(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "US") Then
                        lblError.Text = objeAAMSMessage.messUpdate
                    Else
                        Response.Redirect("MSUP_ManageAirLine.aspx?Action=US|" & objEn.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("AIRLINE").Attributes("Airline_Code").Value.Trim))
                    End If
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
        End If
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "US" Then
            ViewAirline()
        Else
            txtAirlineCode.Text = String.Empty
            txtAirlineName.Text = String.Empty
            chekOnlineCarrier.Checked = False
        End If
    End Sub
End Class
