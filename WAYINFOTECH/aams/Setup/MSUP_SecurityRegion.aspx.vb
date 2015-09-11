
#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data
#End Region
Partial Class Setup_MSUP_SecurityRegion
    Inherits System.Web.UI.Page
#Region " Page Level Variables/ Objects"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim objEn As New EncyrptDeCyrpt

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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Security Region']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Security Region']").Attributes("Value").Value)

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
            btnNew.Attributes.Add("onclick", "return NewMSUPSecurityRegion();")
            btnSave.Attributes.Add("onclick", "return CheckMandatoty();")
            Dim objbzSecurityRegion As New AAMS.bizMaster.bzSecurityRegion
            If Not Page.IsPostBack Then
                ListAoffice()

                '*******************************************************************
                ''Code For 
                '*******************************************************************
                ' Load Data For Editing Mode
                If (Request.QueryString("Action") = "U") Then
                    ViewSecurityRegion()
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
#Region " Show List of Aoffice"
    Sub ListAoffice()
        Try
            Dim objbzAOffice As New AAMS.bizMaster.bzAOffice
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            objOutputXml = objbzAOffice.List()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                ds.Tables("AOFFICE").Columns.Add("Select")
                dbgrdManageSecurityRegion.DataSource = ds.Tables("AOFFICE")
                dbgrdManageSecurityRegion.DataBind()
            Else
                dbgrdManageSecurityRegion.DataSource = Nothing
                dbgrdManageSecurityRegion.DataBind()

                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "BindAofficeDetails Procedure is called for binding the all List of Aoffice Details who belong to Security Region"
    Sub BindAofficeDetails(ByVal objOutputXml As XmlDocument)
        Try
            ' Dim objXmlReader As XmlNodeReader
            Dim dtAoffice As New DataTable("AOFFICE")
            Dim DrowIpPoolDetails As DataRow
            ' Dim objXmlReader As XmlNodeReader
            Dim objIpAddNode As XmlNode
            Dim Row As Integer
            dtAoffice.Columns.Add("Aoffice", GetType(String))
            dtAoffice.Columns.Add("Select", GetType(Boolean))

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                For Row = 0 To dbgrdManageSecurityRegion.Rows.Count - 1
                    DrowIpPoolDetails = dtAoffice.NewRow
                    DrowIpPoolDetails("Aoffice") = dbgrdManageSecurityRegion.Rows(Row).Cells(0).Text
                    For Each objIpAddNode In objOutputXml.DocumentElement.SelectNodes("SECURITYREGION/Aoffice")
                        If (dbgrdManageSecurityRegion.Rows(Row).Cells(0).Text = objIpAddNode.InnerText) Then
                            DrowIpPoolDetails("Select") = True
                            Exit For
                        Else
                            DrowIpPoolDetails("Select") = False
                        End If
                    Next
                    dtAoffice.Rows.Add(DrowIpPoolDetails)
                Next Row
                dbgrdManageSecurityRegion.DataSource = dtAoffice
                dbgrdManageSecurityRegion.DataBind()
            Else
                dbgrdManageSecurityRegion.DataSource = Nothing
                dbgrdManageSecurityRegion.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "ViewSecurityRegion Procedure is called for View Security Region"
    Sub ViewSecurityRegion()
        Try
            If (Not Request.QueryString("Action") = Nothing) Then
                Dim objInputXml, objOutputXml As New XmlDocument
                'Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                Dim objbzSecurityRegion As New AAMS.bizMaster.bzSecurityRegion
                objInputXml.LoadXml("<MS_VIEWSECURITYREGION_INPUT><RegionID></RegionID></MS_VIEWSECURITYREGION_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("RegionID").InnerText = objEn.Decrypt(Request.QueryString("RegionID").ToString().Trim)
                'Here Back end Method Call
                objOutputXml = objbzSecurityRegion.View(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    txtRegionName.Text = objOutputXml.DocumentElement.SelectSingleNode("SECURITYREGION").Attributes("Name").InnerXml
                    ' if Record is found then call a method BindAofficeDetails(objOutputXml)  for binding the cotrols
                    BindAofficeDetails(objOutputXml)
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
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
                    Dim objbzSecurityRegion As New AAMS.bizMaster.bzSecurityRegion
                    objInputXml.LoadXml("<MS_UPDATESECURITYREGION_INPUT><SECURITYREGION RegionID='' Name=''><Aoffice></Aoffice></SECURITYREGION></MS_UPDATESECURITYREGION_INPUT>")
                    'Reading and Appending records into the Input XMLDocument
                    objParentNode = objInputXml.DocumentElement.SelectSingleNode("SECURITYREGION")
                    If (Request.QueryString("Action") = "U") Then
                        objParentNode.Attributes("RegionID").Value = objEn.Decrypt(Request.QueryString("RegionID").Trim)
                    End If
                    objParentNode.Attributes("Name").Value = txtRegionName.Text

                    objChildNode = objInputXml.DocumentElement.SelectSingleNode("SECURITYREGION/Aoffice")
                    objChildNodeClone = objChildNode.CloneNode(True)
                    objParentNode.RemoveChild(objChildNode)
                    For Rowno = 0 To dbgrdManageSecurityRegion.Rows.Count - 1
                        'objAptchildNodeClone.Attributes("Aoffice").InnerText = dbgrdManageSecurityRegion.Rows(Rowno).Cells(0).Text
                        Dim chk As CheckBox = CType(dbgrdManageSecurityRegion.Rows(Rowno).FindControl("btnEdit"), CheckBox)
                        If (chk.Checked = True) Then
                            objChildNodeClone.InnerText = dbgrdManageSecurityRegion.Rows(Rowno).Cells(0).Text 'strAoffice
                            objParentNode.AppendChild(objChildNodeClone)
                            objChildNodeClone = objChildNode.CloneNode(True)
                        End If
                    Next Rowno
                    'Here Back end Method Call
                    objOutputXml = objbzSecurityRegion.Update(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        If (Request.QueryString("Action") = "U") Then

                            lblError.Text = objeAAMSMessage.messUpdate '"Updated Successfully."
                            Response.Redirect("MSUP_SecurityRegion.aspx?&Msg=U&Action=U&RegionID=" & objEn.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("SECURITYREGION").Attributes("RegionID").Value.Trim), False)
                        Else
                            lblError.Text = objeAAMSMessage.messInsert ' "Added Successfully."
                            Response.Redirect("MSUP_SecurityRegion.aspx?&Msg=A&Action=U&RegionID=" & objEn.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("SECURITYREGION").Attributes("RegionID").Value.Trim), False)
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
        Try
            If (Request.QueryString("Action") = "U") Then
                ViewSecurityRegion()
            Else
                txtRegionName.Text = ""
                txtRegionName.Focus()
            End If
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
        lblError.Text = ""
    End Sub
#End Region
End Class
