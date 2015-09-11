Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml

Partial Class Setup_MSUP_OrderTypeCategory
    Inherits System.Web.UI.Page
#Region "Global Variable Declaration"
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
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

#Region "Form Load Declaration Section"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim strurl As String = Request.Url.ToString()

            Session("PageName") = strurl
            lblError.Text = ""
            btnSave.Attributes.Add("onclick", "return ChkMandatotyOrderTypeCat();")
            btnNew.Attributes.Add("onclick", "return NewFunction();")
            If Not Page.IsPostBack Then
                If Not Request.QueryString("Action") Is Nothing Then
                    If Request.QueryString("Action").ToString().Trim().ToUpper() = "U" Then
                        OrderTypeCategoryView()
                    End If
                    If Request.QueryString("Action").ToString().Trim().ToUpper() = "US" Then
                        lblError.Text = objeAAMSMessage.messInsert
                        OrderTypeCategoryView()
                    End If
                End If

            End If
            objeAAMS.ExpirePageCache()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            'If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Type Category']").Count <> 0 Then
            '    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Type Category']").Attributes("Value").Value)
            'End If
            'If strBuilder(1) = "0" Then
            '    btnNew.Enabled = False
            'End If

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Type Category']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Type Category']").Attributes("Value").Value)
                End If
                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("~/NoRights.aspx", False)
                    Exit Sub
                End If

                'If Request.QueryString("Action").ToString().Trim().ToUpper() = "US" Then
                '    If strBuilder(2) = "0" Then
                '        btnSave.Enabled = False
                '    End If
                'End If
                If Not Request.QueryString("Action") Is Nothing Then
                    If strBuilder(2) = "0" Then
                        btnSave.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If


        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Controls DataBinding in case of Update"
    Private Sub OrderTypeCategoryView()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objtaOrderTypeCat As New AAMS.bizTravelAgency.bzOrderCategory
            objInputXml.LoadXml("<MS_VIEWORDERTYPECATEGORY_INPUT><OrderTypeCategoryID /></MS_VIEWORDERTYPECATEGORY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("OrderTypeCategoryID").InnerXml = objED.Decrypt(Request.QueryString("CategoryID").ToString())
            'Here Back end Method Call
            objOutputXml = objtaOrderTypeCat.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                txtOrderTypeCat.Text = objOutputXml.DocumentElement.SelectSingleNode("CATEGORY ").Attributes("OrderTypeCategoryName").Value()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "Save Button Event Definition"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objtaOrderTypeCate As New AAMS.bizTravelAgency.bzOrderCategory
            objInputXml.LoadXml("<MS_UPDATEORDERTYPECATEGORY_INPUT><CATEGORY ACTION='' OrderTypeCategoryID='' OrderTypeCategoryName='' /></MS_UPDATEORDERTYPECATEGORY_INPUT>")
            With objInputXml.DocumentElement.SelectSingleNode("CATEGORY")
                If Request.QueryString("ACTION").ToString().Trim().ToUpper = "U" Or Request.QueryString("ACTION").ToString().Trim().ToUpper = "US" Then
                    .Attributes("ACTION").Value() = "U"
                    .Attributes("OrderTypeCategoryID").Value() = objED.Decrypt(Request.QueryString("CategoryID").ToString())
                    .Attributes("OrderTypeCategoryName").Value() = txtOrderTypeCat.Text.Trim()
                Else
                    .Attributes("ACTION").Value() = "I"
                    .Attributes("OrderTypeCategoryID").Value() = ""
                    .Attributes("OrderTypeCategoryName").Value() = txtOrderTypeCat.Text.Trim()
                End If
            End With
            objOutputXml = objtaOrderTypeCate.Update(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If Request.QueryString("Action").ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().ToUpper() = "US" Then
                    lblError.Text = objeAAMSMessage.messUpdate
                Else
                    Response.Redirect("MSUP_OrderTypeCategory.aspx?Action=US&CategoryID=" & objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("CATEGORY").Attributes("OrderTypeCategoryID").Value))
                End If
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "Reset Button Event Definition"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If Not Request.QueryString("Action") Is Nothing Then
                If Request.QueryString("Action").ToString().Trim().ToUpper() = "U" Then
                    OrderTypeCategoryView()
                Else
                    txtOrderTypeCat.Text = ""
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
#End Region
End Class
