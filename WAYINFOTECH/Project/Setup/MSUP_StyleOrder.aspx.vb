Imports System.Data
Imports System.Xml

Partial Class Setup_MSUP_StyleOrder
    Inherits System.Web.UI.Page

    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim objDe As New EncyrptDeCyrpt

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If

            btnSave.Attributes.Add("onclick", "return CheckMendatoty();")
            btnNew.Attributes.Add("onclick", "return NewFunction();")
            lblError.Text = String.Empty

            'Index 0 View
            'Index 1 Add
            'Index 2 Modify
            'Index 3 Delete
            'Index 4 Print
            Dim objSecurityXml As New XmlDocument
            CheckSecurity()
            If Not Page.IsPostBack Then
                'Check Condition for Action Update or Insert
                If (Not Request.QueryString("Action") = Nothing) Then
                    If (Request.QueryString("Action").ToUpper = "U") Then
                        If (Request.QueryString("Msg") = "A") Then
                            lblError.Text = "Order No : " & objDe.Decrypt(Request.QueryString("OrderID")) & " - " & "Order generated successfully."
                        End If
                        If (Request.QueryString("Msg") = "U") Then
                            lblError.Text = "Order No : " & objDe.Decrypt(Request.QueryString("OrderID")) & " - " & "Order generated successfully."
                        End If
                    End If
                End If
            End If

            If Not Page.IsPostBack Then
                '// NO Need to Bind any Control here
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If (IsValid) Then
            If (Not Request.QueryString("Action") = Nothing) Then
                Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                Dim objbzStyle As New WAY.bizMaster.bzStyle
                objInputXml.LoadXml("<MS_UPDATESTYLE_ORDER_INPUT><STYLE Action='' W_StyleOrderID='' W_StyleId='' BarcodeNo='' StyleName='' DesignNo='' ShadeNo='' MRP='' Qty='' Remarks=''/></MS_UPDATESTYLE_ORDER_INPUT>")

                With objInputXml.DocumentElement.SelectSingleNode("STYLE")
                    If Request.QueryString("Action").ToUpper = "U" Then
                        .Attributes("Action").Value() = "U"
                        .Attributes("W_StyleId").Value() = ViewState("STYLE")
                    Else
                        .Attributes("Action").Value() = "I"
                    End If

                    .Attributes("W_StyleId").Value = hdW_StyleId.Value
                    .Attributes("BarcodeNo").Value = txtBarCode.Text
                    .Attributes("StyleName").Value = txtStyleName.Text
                    .Attributes("DesignNo").Value = txtDesignName.Text
                    .Attributes("ShadeNo").Value = txtShadeNo.Text
                    .Attributes("MRP").Value = txtMRP.Text
                    .Attributes("Qty").Value = txtQty.Text
                    .Attributes("Remarks").Value = ""

                End With

                'Here Back end Method Call
                objOutputXml = objbzStyle.UpdateStyleOrder(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If Request.QueryString("Action").ToUpper = "U" Then
                        Response.Redirect("MSUP_StyleOrder.aspx?&Msg=U&Action=U&OrderID=" & objDe.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("STYLE").Attributes("W_StyleOrderID").Value.Trim), False)
                        lblError.Text = "Order No : " & objOutputXml.DocumentElement.SelectSingleNode("STYLE").Attributes("W_StyleOrderID").Value.Trim & " - " & "Order generated successfully."
                    Else
                        lblError.Text = "Order No : " & objOutputXml.DocumentElement.SelectSingleNode("STYLE").Attributes("W_StyleOrderID").Value.Trim & " - " & "Order generated successfully."
                        Response.Redirect("MSUP_StyleOrder.aspx?&Msg=A&Action=U&OrderID=" & objDe.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("STYLE").Attributes("W_StyleOrderID").Value.Trim), False)
                    End If
                    CheckSecurity()
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
        End If
    End Sub
    Private Sub CheckSecurity()
        Try
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Style Order']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Style Order']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
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
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        txtQty.Text = String.Empty
        txtBarCode.Text = String.Empty
        txtStyleName.Text = String.Empty
        txtDesignName.Text = String.Empty
        txtShadeNo.Text = String.Empty
        txtMRP.Text = String.Empty
    End Sub
End Class
