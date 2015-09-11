Imports System.Xml
Imports System.Data
Partial Class Order_MSUP_Product
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt

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
            ' This code is used for Expiration of Page From Cache
            '   objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            If Not Page.IsPostBack Then
                ' Load Data For Editing Mode
                objeAAMS.BindDropDown(ddlGroupName, "PRODUCTGROUP", True)
                objeAAMS.BindDropDown(ddlCrs, "PROVIDERCRS", True)
                objeAAMS.BindDropDown(ddlOS, "OS", True)

            End If


            If Not Page.IsPostBack Then
                ' Load Data For Editing Mode
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Then
                    ViewProduct()
                End If
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                    lblError.Text = objeAAMSMessage.messInsert
                    ViewProduct()
                End If
            End If



            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency product']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency product']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("~/NoRights.aspx", False)
                    Exit Sub
                End If
                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    'btnSave.Enabled = False
                End If
                If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
                    If strBuilder(2) = "0" Then
                        btnSave.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If (Not Request.QueryString("Action") = Nothing) Then

                Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                Dim objbzProduct As New AAMS.bizTravelAgency.bzProduct
                objInputXml.LoadXml("<MS_UPDATEPRODUCTS_INPUT><PRODUCT ACTION=''  PRODUCTID='' PRODUCTNAME='' VERSION='' EDITION='' HDD_REQUIREMENT='' RAM_REQUIREMENT='' CPU_REQUIREMENT='' OSID_REQUIREMENT='' SEGMENT_REQUIRED='' PER_INSTALLATION='' LIST_PRICE='' PRODUCT_DESCRIPTION='' PROVIDER_CRS='' ProductGroupID='' /> </MS_UPDATEPRODUCTS_INPUT>")
                objInputXml.SelectSingleNode("MS_UPDATEPRODUCTS_INPUT/PRODUCT").Attributes("PRODUCTNAME").Value = txtProductName.Text
                objInputXml.SelectSingleNode("MS_UPDATEPRODUCTS_INPUT/PRODUCT").Attributes("ProductGroupID").InnerText = ddlGroupName.Text
                objInputXml.SelectSingleNode("MS_UPDATEPRODUCTS_INPUT/PRODUCT").Attributes("VERSION").InnerText = txtVersion.Text
                objInputXml.SelectSingleNode("MS_UPDATEPRODUCTS_INPUT/PRODUCT").Attributes("EDITION").InnerText = txtEdition.Text
                objInputXml.SelectSingleNode("MS_UPDATEPRODUCTS_INPUT/PRODUCT").Attributes("PROVIDER_CRS").InnerText = ddlCrs.Text
                objInputXml.SelectSingleNode("MS_UPDATEPRODUCTS_INPUT/PRODUCT").Attributes("LIST_PRICE").InnerText = txtListPrice.Text
                objInputXml.SelectSingleNode("MS_UPDATEPRODUCTS_INPUT/PRODUCT").Attributes("SEGMENT_REQUIRED").InnerText = txtSegmentReq.Text
                objInputXml.SelectSingleNode("MS_UPDATEPRODUCTS_INPUT/PRODUCT").Attributes("PER_INSTALLATION").InnerText = IIf(chbInsReq.Checked, "1", "0")
                objInputXml.SelectSingleNode("MS_UPDATEPRODUCTS_INPUT/PRODUCT").Attributes("OSID_REQUIREMENT").InnerText = ddlOS.Text
                objInputXml.SelectSingleNode("MS_UPDATEPRODUCTS_INPUT/PRODUCT").Attributes("RAM_REQUIREMENT").InnerText = txtRam.Text
                objInputXml.SelectSingleNode("MS_UPDATEPRODUCTS_INPUT/PRODUCT").Attributes("HDD_REQUIREMENT").InnerText = txtHdd.Text
                objInputXml.SelectSingleNode("MS_UPDATEPRODUCTS_INPUT/PRODUCT").Attributes("CPU_REQUIREMENT").InnerText = txtCpu.Text
                objInputXml.SelectSingleNode("MS_UPDATEPRODUCTS_INPUT/PRODUCT").Attributes("PRODUCT_DESCRIPTION").InnerText = txtremarks.Text
                If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
                    objInputXml.SelectSingleNode("MS_UPDATEPRODUCTS_INPUT/PRODUCT").Attributes("ACTION").Value = "U"
                    objInputXml.SelectSingleNode("MS_UPDATEPRODUCTS_INPUT/PRODUCT").Attributes("PRODUCTID").Value = objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString().ToUpper())
                Else
                    objInputXml.SelectSingleNode("MS_UPDATEPRODUCTS_INPUT/PRODUCT").Attributes("ACTION").Value = "I"
                    objInputXml.SelectSingleNode("MS_UPDATEPRODUCTS_INPUT/PRODUCT").Attributes("PRODUCTID").Value = ""
                End If

                objOutputXml = objbzProduct.Update(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
                        lblError.Text = objeAAMSMessage.messUpdate ' "Record updated successfully."
                    Else
                        Response.Redirect("MSUP_Product.aspx?Action=US|" & objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("PRODUCT").Attributes("PRODUCTID").Value))
                        lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."

                    End If
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("MSUP_Product.aspx?Action=I|")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
            lblError.Text = ""
            ViewProduct()
        Else
            ddlGroupName.SelectedIndex = 0
            txtProductName.Text = ""
            txtVersion.Text = ""
            txtEdition.Text = ""
            ddlCrs.SelectedIndex = 0
            ddlOS.SelectedIndex = 0
            txtListPrice.Text = ""
            txtSegmentReq.Text = ""
            chbInsReq.Checked = False
            txtRam.Text = ""
            txtremarks.Text = ""
            txtHdd.Text = ""
            txtCpu.Text = ""
            lblError.Text = ""
        End If

       
    End Sub

    Sub ViewProduct()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            'Dim objXmlReader As XmlNodeReader
            Dim objbzProduct As New AAMS.bizTravelAgency.bzProduct
            objInputXml.LoadXml("<MS_VIEWPRODUCTS_INPUT><PRODUCTID></PRODUCTID></MS_VIEWPRODUCTS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("PRODUCTID").InnerText = objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString())
            'Here Back end Method Call
            objOutputXml = objbzProduct.View(objInputXml)
            '<MS_VIEWPRODUCTS_OUTPUT>
            '  <PRODUCT PRODUCTID="1" PRODUCTNAME="a" VERSION="1" EDITION="1" HDD_REQUIREMENT="" RAM_REQUIREMENT="" CPU_REQUIREMENT="" OSID_REQUIREMENT="1" SEGMENT_REQUIRED="" PER_INSTALLATION="False" LIST_PRICE="" PRODUCT_DESCRIPTION="" PROVIDER_CRS="1" ProductGroupID="3" /> 
            '- <Errors Status="FALSE">
            '  <Error Code="" Description="" /> 
            '  </Errors>
            '  </MS_VIEWPRODUCTS_OUTPUT>
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ddlGroupName.Text = objOutputXml.SelectSingleNode("MS_VIEWPRODUCTS_OUTPUT/PRODUCT").Attributes("ProductGroupID").InnerText
                txtProductName.Text = objOutputXml.SelectSingleNode("MS_VIEWPRODUCTS_OUTPUT/PRODUCT").Attributes("PRODUCTNAME").InnerText
                txtVersion.Text = objOutputXml.SelectSingleNode("MS_VIEWPRODUCTS_OUTPUT/PRODUCT").Attributes("VERSION").InnerText
                txtEdition.Text = objOutputXml.SelectSingleNode("MS_VIEWPRODUCTS_OUTPUT/PRODUCT").Attributes("EDITION").InnerText
                ddlCrs.Text = objOutputXml.SelectSingleNode("MS_VIEWPRODUCTS_OUTPUT/PRODUCT").Attributes("PROVIDER_CRS").InnerText
                txtListPrice.Text = objOutputXml.SelectSingleNode("MS_VIEWPRODUCTS_OUTPUT/PRODUCT").Attributes("LIST_PRICE").InnerText
                txtSegmentReq.Text = objOutputXml.SelectSingleNode("MS_VIEWPRODUCTS_OUTPUT/PRODUCT").Attributes("SEGMENT_REQUIRED").InnerText
                chbInsReq.Checked = objOutputXml.SelectSingleNode("MS_VIEWPRODUCTS_OUTPUT/PRODUCT").Attributes("PER_INSTALLATION").InnerText
                ddlOS.Text = objOutputXml.SelectSingleNode("MS_VIEWPRODUCTS_OUTPUT/PRODUCT").Attributes("OSID_REQUIREMENT").InnerText
                txtRam.Text = objOutputXml.SelectSingleNode("MS_VIEWPRODUCTS_OUTPUT/PRODUCT").Attributes("RAM_REQUIREMENT").InnerText
                txtHdd.Text = objOutputXml.SelectSingleNode("MS_VIEWPRODUCTS_OUTPUT/PRODUCT").Attributes("HDD_REQUIREMENT").InnerText
                txtCpu.Text = objOutputXml.SelectSingleNode("MS_VIEWPRODUCTS_OUTPUT/PRODUCT").Attributes("CPU_REQUIREMENT").InnerText
                txtremarks.Text = objOutputXml.SelectSingleNode("MS_VIEWPRODUCTS_OUTPUT/PRODUCT").Attributes("PRODUCT_DESCRIPTION").InnerText
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
