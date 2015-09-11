Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml

Partial Class ISP_ISPUP_OrderStatus
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim eaamsObj As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblError.Text = ""
            btnSave.Attributes.Add("onclick", "return validateISP();")
            btnNew.Attributes.Add("onclick", "return InsertISPOrderStatus();")
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl


            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspOrderStatus']").Count <> 0 Then
                    strBuilder = eaamsObj.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspOrderStatus']").Attributes("Value").Value)

                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If

                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                    End If

                    If Request.QueryString("Action") IsNot Nothing Then
                        If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                            If strBuilder(2) = "0" Then
                                btnSave.Enabled = False
                            End If
                        End If
                    End If
                End If
            Else
                strBuilder = eaamsObj.SecurityCheck(31)
            End If


            If Not Page.IsPostBack Then
                If Request.QueryString("Action") IsNot Nothing Then
                    If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                        'txtIspOrderStatusName.ReadOnly = True
                        'txtIspOrderStatusName.CssClass = "textboxgrey"
                        ViewISP_OrderStatus()
                    End If
                    If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                        lblError.Text = objeAAMSMessage.messInsert
                    End If
                End If

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Try
                If (Not Request.QueryString("Action") = Nothing) Then

                    Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                    Dim objbzIspOrdrStatus As New AAMS.bizISP.bzISPOrderStatus

                    objInputXml.LoadXml("<ISP_UPDATEORDERSTATUS_INPUT><OrderStatus OrderStatusID='' StatusName='' CancelOrder='' ApprovedOrder='' /> </ISP_UPDATEORDERSTATUS_INPUT>")    '"<IS_UPDATEISP_INPUT><ISP ISPID='' Name='' CityID='' Address='' PinCode='' CTCName='' Phone='' Fax='' Email='' /></IS_UPDATEISP_INPUT>")
                    If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then

                        '@ Getting  Decrepted Data 
                        Dim DecreptedFeasibleOrderStatusID As String
                        DecreptedFeasibleOrderStatusID = objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString())
                        '@ End of Getting Decrepted Data

                        objInputXml.DocumentElement.SelectSingleNode("OrderStatus").Attributes("OrderStatusID").Value = DecreptedFeasibleOrderStatusID

                        'objInputXml.DocumentElement.SelectSingleNode("OrderStatus").Attributes("OrderStatusID").Value = Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()
                    End If

                    If rdBtnAppCanc.SelectedValue = "Cancellation" Then
                        objInputXml.DocumentElement.SelectSingleNode("OrderStatus").Attributes("CancelOrder").Value = "1"
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("OrderStatus").Attributes("CancelOrder").Value = "0"
                    End If

                    If rdBtnAppCanc.SelectedValue = "Approved" Then
                        objInputXml.DocumentElement.SelectSingleNode("OrderStatus").Attributes("ApprovedOrder").Value = "1"
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("OrderStatus").Attributes("ApprovedOrder").Value = "0"
                    End If

                    'If chkCancellation.Checked Then
                    '    objInputXml.DocumentElement.SelectSingleNode("OrderStatus").Attributes("CancelOrder").Value = "1"
                    'Else
                    '    objInputXml.DocumentElement.SelectSingleNode("OrderStatus").Attributes("CancelOrder").Value = "0"
                    'End If

                    'If chkOrderApproved.Checked Then
                    '    objInputXml.DocumentElement.SelectSingleNode("OrderStatus").Attributes("ApprovedOrder").Value = "1"
                    'Else
                    '    objInputXml.DocumentElement.SelectSingleNode("OrderStatus").Attributes("ApprovedOrder").Value = "0"
                    'End If

                    objInputXml.DocumentElement.SelectSingleNode("OrderStatus").Attributes("StatusName").Value = txtIspOrderStatusName.Text.Trim()

                    objOutputXml = objbzIspOrdrStatus.Update(objInputXml)

                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
                            lblError.Text = objeAAMSMessage.messUpdate ' "Record updated successfully."
                        Else


                            '@ Getting  Encrepted Data 
                            Dim EncreptedFeasibleOrderStatusID As String
                            EncreptedFeasibleOrderStatusID = objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("OrderStatus").Attributes("OrderStatusID").Value)
                            '@ End of Getting Encrepted Data
                            Response.Redirect("ISPUP_OrderStatus.aspx?Action=US|" & EncreptedFeasibleOrderStatusID, False)
                            'Response.Redirect("ISPUP_OrderStatus.aspx?Action=US|" & objOutputXml.DocumentElement.SelectSingleNode("OrderStatus").Attributes("OrderStatusID").Value, False)
                            lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."

                        End If
                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            End Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If Request.QueryString("Action") IsNot Nothing Then
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                    ViewISP_OrderStatus()
                Else
                    txtIspOrderStatusName.Text = ""
                    'chkCancellation.Checked = False
                    'chkOrderApproved.Checked = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Sub ViewISP_OrderStatus()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument

            Dim objbzIspOStatus As New AAMS.bizISP.bzISPOrderStatus
            objInputXml.LoadXml("<ISP_VIEWORDERSTATUS_INPUT><OrderStatusID/></ISP_VIEWORDERSTATUS_INPUT>")
            If Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString() IsNot Nothing Then
                '@ Getting  Decrrepted Data 
                Dim DecreptedOrderStatusID As String
                DecreptedOrderStatusID = objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString())
                '@ End of Getting Decrepted Data

                objInputXml.DocumentElement.SelectSingleNode("OrderStatusID").InnerText = DecreptedOrderStatusID
                'objInputXml.DocumentElement.SelectSingleNode("OrderStatusID").InnerText = Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()


            End If

            objOutputXml = objbzIspOStatus.View(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
               
                txtIspOrderStatusName.Text = objOutputXml.DocumentElement.SelectSingleNode("OrderStatus").Attributes("StatusName").Value.Trim()

                If objOutputXml.DocumentElement.SelectSingleNode("OrderStatus").Attributes("CancelOrder").Value.Trim() = "True" Then
                    rdBtnAppCanc.SelectedValue = "Cancellation"
                End If

                If objOutputXml.DocumentElement.SelectSingleNode("OrderStatus").Attributes("ApprovedOrder").Value.Trim() = "True" Then
                    rdBtnAppCanc.SelectedValue = "Approved"
                End If

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click

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
End Class
