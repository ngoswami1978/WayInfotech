'#########################################################################
'############   Page Name - Inventory_INVUP_VendorSerialNoDetails  #######
'############   Date 24-March 2008    ####################################
'############   Developed By Abhishek  ###################################
'#########################################################################
#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data
#End Region
Partial Class Inventory_INVUP_VendorSerialNoDetails
    Inherits System.Web.UI.Page
#Region "Page level variable/Objects Declaration"
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
#End Region
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objInputXml As New XmlDocument
            Session("PageName") = Request.Url.ToString()
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            btnSave.Attributes.Add("onclick", "return  CheckMandatoty();")

            If (Not IsPostBack) Then
                If (Request.QueryString("PrdId") IsNot Nothing) Then
                    Dim EnstrPrdID As String
                    EnstrPrdID = objED.Decrypt(Request.QueryString("PrdId"))
                    hdProductId.Value = EnstrPrdID
                End If
                If (Request.QueryString("VSNO") IsNot Nothing) Then
                    Dim EnstrVSNO As String
                    EnstrVSNO = objED.Decrypt(Request.QueryString("VSNO"))
                    hdVendorSerialNo.Value = EnstrVSNO
                    txtVenSNo.Text = EnstrVSNO
                End If
                If (Request.QueryString("SNO") IsNot Nothing) Then
                    Dim EnstrSNO As String
                    EnstrSNO = objED.Decrypt(Request.QueryString("SNO"))
                    hdSerialno.Value = EnstrSNO
                    txtSNo.Text = EnstrSNO
                End If
                If (Request.QueryString("PNAME") IsNot Nothing) Then
                    Dim EnstrPNAME As String
                    EnstrPNAME = objED.Decrypt(Request.QueryString("PNAME"))
                    hdProductName.Value = EnstrPNAME
                    txtPrdName.Text = EnstrPNAME
                End If
                If (Request.QueryString("Msg") IsNot Nothing) Then
                    lblError.Text = objeAAMSMessage.messUpdate
                End If
            End If
            ' #######################################
            ' ########## This Code code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MODIFY SERIAL NO']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MODIFY SERIAL NO']").Attributes("Value").Value)
                    If strBuilder(2) = "0" Then
                        btnSave.Enabled = False
                    End If
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")

                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            ' #######################################
            ' ########## End of code used for enable/disable 
            ' ########## the button according to rights
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If (Request.QueryString("VSNO") IsNot Nothing) Then
                Dim EnstrVSNO As String
                EnstrVSNO = objED.Decrypt(Request.QueryString("VSNO"))
                txtVenSNo.Text = EnstrVSNO
            End If
            If (Request.QueryString("SNO") IsNot Nothing) Then
                Dim EnstrSNO As String
                EnstrSNO = objED.Decrypt(Request.QueryString("SNO"))
                txtSNo.Text = EnstrSNO
            End If
            If (Request.QueryString("PNAME") IsNot Nothing) Then
                Dim EnstrPNAME As String
                EnstrPNAME = objED.Decrypt(Request.QueryString("PNAME"))
                txtPrdName.Text = EnstrPNAME
            End If
            txtNewVenSNo.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If (IsValid) Then
                If (Not Request.QueryString("PrdId") = Nothing) Then

                    Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                    'Dim objXmlReader As XmlNodeReader
                    Dim ds As New DataSet
                    Dim objbzChallan As New AAMS.bizInventory.bzChallan
                    objInputXml.LoadXml("<INV_UPDATESERIALNO_INPUT><DETAILS ACTION='' PRODUCTID='' VENDERSERIALNO='' NEWVENDERSERIALNO='' SERIALNUMBER='' /></INV_UPDATESERIALNO_INPUT>")

                    objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("ACTION").Value = "U"
                    If (Request.QueryString("PrdId") IsNot Nothing) Then
                        objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("PRODUCTID").Value = hdProductId.Value 'Request.QueryString("PrdId")
                    End If
                    objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("VENDERSERIALNO").Value = txtVenSNo.Text
                    objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("NEWVENDERSERIALNO").Value = txtNewVenSNo.Text
                    objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("SERIALNUMBER").Value = txtSNo.Text

                    'Here Back end Method Call
                    objOutputXml = objbzChallan.UpdateSerialNo(objInputXml)
                    ' objOutputXml.LoadXml("<INV_UPDATESERIALNO_OUTPUT> <DETAILS ACTION='' PRODUCTID='' VENDERSERIALNO='' NEWVENDERSERIALNO='' /> <Errors Status='FALSE'> <Error Code='' Description=''/>  </Errors></INV_UPDATESERIALNO_OUTPUT>")
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        lblError.Text = objeAAMSMessage.messUpdate ' "Record updated successfully."  
                        'Response.Redirect("INVUP_VendorSerialNoDetails.aspx?Msg=U&Popup=T&PrdId=" + Request.QueryString("PrdId") + "&SNO=" + txtSNo.Text + "&VSNO=" + txtNewVenSNo.Text + "&PNAME=" + txtPrdName.Text, False)
                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                Else
                    lblError.Text = "Incomplete Parameter"
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
