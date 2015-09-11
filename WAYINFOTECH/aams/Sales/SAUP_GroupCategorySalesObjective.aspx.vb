'################################################
'######## Developed By Abhishek  on 5th May 2011 ## 
'################################################
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Partial Class Sales_SAUP_GroupCategorySalesObjective
    Inherits System.Web.UI.Page
#Region "Global Variable Declaration"
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
#End Region

#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If


            If Not IsPostBack Then
                btnSave.Attributes.Add("onClick", "return ValidateForm();")
                objeAAMS.BindDropDown(drpAoffice, "AOFFICE", True, 1)
                ' objeAAMS.BindDropDown(drpGroupCategory, "GROUP_CLASSIFICATION", True, 1)
                objeAAMS.BindDropDown(drpGroupCategory, "AGENCYTYPE", True, 1)

                If Request.QueryString("Action") IsNot Nothing And Request.QueryString("GroupCat") IsNot Nothing Then
                    hdID.Value = objED.Decrypt(Request.QueryString("GroupCat").ToString().Trim())
                    hdAOffice.Value = objED.Decrypt(Request.QueryString("AOffice").ToString().Trim())
                    ViewDetails()
                End If
            End If

        
            ' Checking security.
            CheckSecurity()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objInputXml As New XmlDocument
        Dim objOutputXml As New XmlDocument
        Dim objbzGroupCategorySales As New AAMS.bizSales.bzGroupCategorySales
        Try

            objInputXml.LoadXml("<SL_UPDATEGROUP_CATEGORY_INPUT><GROUP_CATEGORY ACTION='' AOFFICE='' GROUP_CATG_ID='' GROUP_CATG_NAME='' VISITCOUNT='' /></SL_UPDATEGROUP_CATEGORY_INPUT>")

            If (hdAOffice.Value <> "") Then
                objInputXml.DocumentElement.SelectSingleNode("GROUP_CATEGORY").Attributes("ACTION").Value = "U"
                objInputXml.DocumentElement.SelectSingleNode("GROUP_CATEGORY").Attributes("AOFFICE").Value = hdAOffice.Value
            Else
                objInputXml.DocumentElement.SelectSingleNode("GROUP_CATEGORY").Attributes("ACTION").Value = "I"
                objInputXml.DocumentElement.SelectSingleNode("GROUP_CATEGORY").Attributes("AOFFICE").Value = drpAoffice.SelectedValue
            End If


            If (hdID.Value <> "") Then
                objInputXml.DocumentElement.SelectSingleNode("GROUP_CATEGORY").Attributes("GROUP_CATG_ID").Value = hdID.Value
                objInputXml.DocumentElement.SelectSingleNode("GROUP_CATEGORY").Attributes("GROUP_CATG_NAME").Value = "t"
            Else
                objInputXml.DocumentElement.SelectSingleNode("GROUP_CATEGORY").Attributes("GROUP_CATG_ID").Value = drpGroupCategory.SelectedItem.Value
                objInputXml.DocumentElement.SelectSingleNode("GROUP_CATEGORY").Attributes("GROUP_CATG_NAME").Value = "t"
            End If
            If txtVisitCount.Text <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("GROUP_CATEGORY").Attributes("VISITCOUNT").Value = txtVisitCount.Text
            End If

           
            ' Calling update method for update.
            objOutputXml = objbzGroupCategorySales.Update(objInputXml)
            '   Checking error status. 
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If (Request.QueryString("Action").ToString().ToUpper() = "U" Or hdID.Value <> "") Then
                    hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("GROUP_CATEGORY").Attributes("GROUP_CATG_ID").Value.Trim()
                    hdAOffice.Value = objOutputXml.DocumentElement.SelectSingleNode("GROUP_CATEGORY").Attributes("AOFFICE").Value.Trim()
                    lblError.Text = objeAAMSMessage.messUpdate ' Record updated successfully.
                Else
                    hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("GROUP_CATEGORY").Attributes("GROUP_CATG_ID").Value.Trim()
                    hdAOffice.Value = objOutputXml.DocumentElement.SelectSingleNode("GROUP_CATEGORY").Attributes("AOFFICE").Value.Trim()
                    lblError.Text = objeAAMSMessage.messInsert ' Record inserted successfully.
                End If
                drpGroupCategory.Enabled = False
                drpAoffice.Enabled = False
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
            CheckSecurity()

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click"
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Response.Redirect("SAUP_GroupCategorySalesObjective.aspx?Action=I", False)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If hdID.Value = "" Then
                'txtGroupCategName.Text = ""
                txtVisitCount.Text = ""
                drpAoffice.SelectedValue = ""
                drpGroupCategory.SelectedValue = ""
                drpGroupCategory.Enabled = True
                drpAoffice.Enabled = True
                lblError.Text = ""
            Else
                ViewDetails()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "ViewDetails()"
    Private Sub ViewDetails()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzGroupCategorySales As New AAMS.bizSales.bzGroupCategorySales
        Try

            objInputXml.LoadXml("<SL_VIEWGROUP_CATEGORY_INPUT><GROUP_CATG_ID></GROUP_CATG_ID><AOFFICE></AOFFICE></SL_VIEWGROUP_CATEGORY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("GROUP_CATG_ID").InnerText = hdID.Value 'Request.QueryString("GCategID").ToString().Trim()
            objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = hdAOffice.Value
            objOutputXml = objbzGroupCategorySales.View(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                drpGroupCategory.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("GROUP_CATEGORY").Attributes("GROUP_CATG_ID").Value
                drpAoffice.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("GROUP_CATEGORY").Attributes("AOFFICE").Value

                drpGroupCategory.Enabled = False
                drpAoffice.Enabled = False
                TxtVisitCount.Text = objOutputXml.DocumentElement.SelectSingleNode("GROUP_CATEGORY").Attributes("VISITCOUNT").Value
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
#End Region

#Region "CheckSecurity()::This method is used for security check."
    Private Sub CheckSecurity()
        Try
            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Sales Objective']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Sales Objective']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnSave.Enabled = False
                    End If

                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                        btnSave.Enabled = False
                    End If
                    If strBuilder(2) = "0" And (hdID.Value <> "" Or Request.QueryString("GCategID") IsNot Nothing) Then
                        btnSave.Enabled = False
                    End If

                    If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                        btnSave.Enabled = True
                    End If
                Else
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    
End Class

