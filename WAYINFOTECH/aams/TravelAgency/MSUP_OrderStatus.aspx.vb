'##############################################################
'############   Page Name -- Order_MSUP_OrderStatus  ##########
'############   Date 29-November 2007  ########################
'############   Developed By Mukund  ########################
'##############################################################
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data
Partial Class Order_MSUP_OrderStatus
    Inherits System.Web.UI.Page
#Region "Global Declaration Section"
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Public strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
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

#Region "Form Load Events Definition"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblError.Text = ""
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            objeAAMS.ExpirePageCache()

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            btnSave.Attributes.Add("onclick", "return CheckMandatotyOrderStaus();")
            btnNew.Attributes.Add("onclick", "return NewFunction();")

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Status']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Status']").Attributes("Value").Value)
                End If

                If strBuilder(0) = "0" Then
                    Response.Redirect("~/NoRights.aspx", False)
                    Exit Sub
                End If
                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    btnSave.Enabled = False
                End If
                If strBuilder(2) = "0" And strBuilder(1) = "0" Then 'Add =false /Modify= false
                    btnSave.Enabled = False
                End If
                If strBuilder(2) = "0" And strBuilder(1) = "1" Then 'Modify =false /Add= True
                    If Request.QueryString("Action").ToString() = "U" Then
                        btnSave.Enabled = False
                    Else
                        btnSave.Enabled = True
                    End If
                End If
                If strBuilder(2) = "1" And strBuilder(1) = "0" Then 'Modify= true/Add =false
                    If Request.QueryString("Action").ToString() = "U" Then
                        btnSave.Enabled = True
                    Else
                        btnSave.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            If Not Page.IsPostBack Then
                If Not Request.QueryString("Action") Is Nothing Then
                    If Request.QueryString("Action").ToString().Trim().ToUpper() = "U" Then
                        OrderStatusView()
                        If (Request.QueryString("Msg") = "A") Then
                            lblError.Text = objeAAMSMessage.messInsert
                        End If
                        If (Request.QueryString("Msg") = "U") Then
                            lblError.Text = objeAAMSMessage.messUpdate
                        End If
                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Reset Button Definition"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        If Not Request.QueryString("Action") Is Nothing Then
            If Request.QueryString("Action").ToString().Trim().ToUpper() = "U" Then
                OrderStatusView()
            Else
                txtOrderStatus.Text = ""
            End If
        End If
    End Sub
#End Region

#Region "Save Button Event Definition"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objtaOrderStatus As New AAMS.bizTravelAgency.bzOrderStatus
        objInputXml.LoadXml("<MS_UPDATEORDERSTATUS_INPUT><ORDERSTATUS ACTION='' ORDER_STATUS_NAME='' ORDERSTATUSID='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATEORDERSTATUS_INPUT>")
        With objInputXml.DocumentElement.SelectSingleNode("ORDERSTATUS")
            If Request.QueryString("ACTION").ToString().Trim().ToUpper = "U" Then
                .Attributes("ACTION").Value() = "U"
                .Attributes("ORDERSTATUSID").Value() = ViewState("OrderStID")
                .Attributes("ORDER_STATUS_NAME").Value() = txtOrderStatus.Text.Trim()
            Else
                .Attributes("ACTION").Value() = "I"
                .Attributes("ORDERSTATUSID").Value() = ""
                .Attributes("ORDER_STATUS_NAME").Value() = txtOrderStatus.Text.Trim()
            End If
        End With
        objOutputXml = objtaOrderStatus.Update(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

            If Request.QueryString("Action").ToUpper = "U" Then
                Response.Redirect("MSUP_OrderStatus.aspx?&Msg=U&Action=U&OrderStID=" & objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("ORDERSTATUS").Attributes("ORDERSTATUSID").Value), False)
                lblError.Text = objeAAMSMessage.messUpdate '"Record updated successfully."
            Else
                lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."
                Response.Redirect("MSUP_OrderStatus.aspx?&Msg=A&Action=U&OrderStID=" & objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("ORDERSTATUS").Attributes("ORDERSTATUSID").Value), False)
            End If

            'If Request.QueryString("Action").ToString().ToUpper() = "U" Then
            '    lblError.Text = "Updated successfully."
            'Else
            '    lblError.Text = "Record Added successfully."
            'End If

        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
#End Region

#Region "Control's Values Binding"
    Private Sub OrderStatusView()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objtaOrderStatus As New AAMS.bizTravelAgency.bzOrderStatus
        objInputXml.LoadXml("<MS_VIEWORDERSTATUS_INPUT><ORDERSTATUSID /></MS_VIEWORDERSTATUS_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("ORDERSTATUSID").InnerXml = objED.Decrypt(Request.QueryString("OrderStID").ToString())
        objOutputXml = objtaOrderStatus.View(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            With objOutputXml.DocumentElement.SelectSingleNode("ORDERSTATUS")
                ViewState("OrderStID") = .Attributes("ORDERSTATUSID").Value()
                txtOrderStatus.Text = .Attributes("ORDER_STATUS_NAME").Value()
            End With
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
#End Region
End Class
