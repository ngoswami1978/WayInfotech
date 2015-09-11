'#######################################################
'############   Page Name -- MSUP_IPPool.aspx  #########  
'############   Date 13-November 2007  #################
'############   Developed By Abhishek  #################
'#######################################################
#Region "Required Namespace Declaration"
Imports System.Data
Imports System.Xml
#End Region
Partial Class Setup_MSUP_IPPool
    Inherits System.Web.UI.Page
#Region "Page Level Variables/Objects Declaration"
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


#Region "Page_Load Event"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            ' Session("PageName") = "Setup/MSUP_IPPool.aspx"

            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            lblError.Text = ""

            ' #######################################
            ' ########## This Code code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IP Pool']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IP Pool']").Attributes("Value").Value)

                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                    End If

                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                        ' btnSave.Enabled = False
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
            btnSave.Attributes.Add("onclick", "return CheckMandatotyForSave();")
            btnAdd.Attributes.Add("onclick", "return CheckMandatotyForAdd();")
            btnNew.Attributes.Add("onclick", "return NewMSUPIPPool();")
            If Not Page.IsPostBack Then
                ' btnReset.Attributes.Add("onclick", "return IPPoolDataReset();")

                '*******************************************************************
                ''Code For 
                objeAAMS.BindDropDown(drpLstAoffice, "AOFFICE", True)
                objeAAMS.BindDropDown(drpLstDepartment, "DepartmentName", True)
                '*******************************************************************
                ' Load Data For Editing Mode
                If (Not Request.QueryString("Action") = Nothing) Then
                    If (Request.QueryString("Action").ToUpper = "U") Then
                        If (Not Request.QueryString("PoolId") = Nothing) Then
                            ' ViewIPPoolGroup method  called for binding the controls  
                            ViewIPPoolGroup()
                            If (Request.QueryString("Msg") = "A") Then
                                lblError.Text = objeAAMSMessage.messInsert
                            End If
                            If (Request.QueryString("Msg") = "U") Then
                                lblError.Text = objeAAMSMessage.messUpdate
                            End If
                        End If
                    End If
                End If
            End If
           
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
            End If
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            Dim li As ListItem
                            li = drpLstAoffice.Items.FindByValue(objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText)
                            If li IsNot Nothing Then
                                drpLstAoffice.SelectedValue = li.Value
                            End If
                        End If
                        drpLstAoffice.Enabled = False
                    End If
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    ' When User Click on New Button then Same Page is loaded By Passing Action='I'
    'Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
    '    Response.Redirect("MSUP_IPPool.aspx?Action=I")
    'End Sub
#Region "ViewIPPoolGroup Procedure called for editing the data"
    Sub ViewIPPoolGroup()
        Try
            If (Not Request.QueryString("Action") = Nothing) Then
                Dim objInputXml, objOutputXml As New XmlDocument
                'Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                Dim objbzIPPool As New AAMS.bizMaster.bzIPPool
                objInputXml.LoadXml("<MS_VIEWIPPOOL_INPUT><PoolID></PoolID></MS_VIEWIPPOOL_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("PoolID").InnerText = objEn.Decrypt(Request.QueryString("PoolId").ToString().Trim)
                'Here Back end Method Call
                objOutputXml = objbzIPPool.View(objInputXml)
                'objOutputXml.LoadXml("<MS_VIEWIPPOOL_OUTPUT><IPPOOL PoolID='49' PoolName='Technical' Aoffice='DEL' DepartmentID='18'><IPAddress>192.168.9.4</IPAddress><IPAddress>192.168.9.5</IPAddress></IPPOOL><Errors Status='FALSE'><Error Code='' Description='' /></Errors></MS_VIEWIPPOOL_OUTPUT>") 'objbzIPPool.View(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    ' if Record is found then call a method BindIPPoolData(objOutputXml)  for binding the cotrols
                    txtPoolName.Text = objOutputXml.DocumentElement.SelectSingleNode("IPPOOL").Attributes("PoolName").InnerXml
                    txtIpAddress.Text = "" ' objOutputXml.DocumentElement.SelectSingleNode("IPPOOL").Attributes("IPAddress").InnerXml
                    drpLstAoffice.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("IPPOOL").Attributes("Aoffice").InnerXml
                    drpLstDepartment.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("IPPOOL").Attributes("DepartmentID").InnerXml
                    BindIpdetails(objOutputXml)
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "BindIpdetails Procedure is called for binding the data of IP POOl "
    Sub BindIpdetails(ByVal objOutputXml As XmlDocument)
        Try
            Dim dtIpPoolDetails As New DataTable
            Dim DrowIpPoolDetails As DataRow
            ' Dim objXmlReader As XmlNodeReader
            Dim objIpAddNode As XmlNode
            'Dim Row As Integer
            dtIpPoolDetails.Columns.Add("IPAddress", GetType(String))
            For Each objIpAddNode In objOutputXml.DocumentElement.SelectNodes("IPPOOL/IPAddress")
                DrowIpPoolDetails = dtIpPoolDetails.NewRow
                DrowIpPoolDetails("IPAddress") = objIpAddNode.InnerText
                dtIpPoolDetails.Rows.Add(DrowIpPoolDetails)
            Next
            dbgrdIpPool.DataSource = dtIpPoolDetails 'ds.Tables("IPPOOL")
            dbgrdIpPool.DataBind()
        Catch ex As Exception
            lblError.Text = ex.Message
            dbgrdIpPool.DataSource = Nothing
            dbgrdIpPool.DataBind()
        End Try
    End Sub
#End Region
#Region " btnAdd_Click Event is fired for Adding an IP Address To Grid For a Particular IP POOL"
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If (IsValid) Then
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbzIPPool As New AAMS.bizMaster.bzIPPool
            Dim RowNo As Integer
            Dim dtIpPoolDetails As New DataTable
            dtIpPoolDetails.Columns.Add("IPAddress", GetType(String))
            Dim DrowIpPoolDetails As DataRow
            Try
                If (txtIpAddress.Text.Trim.Length > 0) Then

                    If (txtIpAddress.Text.Trim().Split(".").Length <> 4) Then
                        lblError.Text = "IP address is not valid."
                        Exit Sub
                    End If
                    Dim ArrIp() As String = txtIpAddress.Text.Trim().Split(".")
                    For RowNo = 0 To ArrIp.Length - 1
                        '  lblError.Text = ArrIp(RowNo).Trim().Length.ToString()
                        If (ArrIp(RowNo).Trim().Length < 1 Or ArrIp(RowNo).Trim().Length > 3) Then
                            lblError.Text = "IP address is not valid."
                            Exit Sub
                        End If
                    Next RowNo

                End If
                'strPoolId = 17 ' objOutPutXml.DocumentElement.SelectSingleNode("IPPOOL").Attributes("PoolId").Value
                For RowNo = 0 To dbgrdIpPool.Rows.Count - 1
                    DrowIpPoolDetails = dtIpPoolDetails.NewRow
                    DrowIpPoolDetails("IPAddress") = dbgrdIpPool.Rows(RowNo).Cells(0).Text
                    If (dbgrdIpPool.Rows(RowNo).Cells(0).Text = txtIpAddress.Text.Trim) Then
                        lblError.Text = "IP addrees already exist."
                        Exit Sub
                    End If
                    dtIpPoolDetails.Rows.Add(DrowIpPoolDetails)
                Next
                DrowIpPoolDetails = dtIpPoolDetails.NewRow
                DrowIpPoolDetails("IPAddress") = txtIpAddress.Text
                dtIpPoolDetails.Rows.Add(DrowIpPoolDetails)

                dbgrdIpPool.DataSource = dtIpPoolDetails
                dbgrdIpPool.DataBind()
                txtIpAddress.Text = ""
                'lblError.Text = objeAAMSMessage.messInsert
            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
        End If
    End Sub
#End Region

#Region " This btnSave_Click is used For Update the Master the record ( That is Only PoolName,Aoffice,DepartmentId "
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If (IsValid) Then
            Dim objInputXml As New XmlDataDocument
            Dim objOutputXml As New XmlDocument
            Dim RowNo As Integer
            Dim objParentNode, objChildNode, objChildNodeClone As XmlNode
            Dim objbzIPPool As New AAMS.bizMaster.bzIPPool
            Try
                If dbgrdIpPool.Rows.Count = 0 Then
                    lblError.Text = "No any IP address exist."
                    Exit Sub
                End If
                objInputXml.LoadXml("<MS_UPDATEIPPOOL_INPUT><IPPOOL PoolID='' PoolName='' Aoffice='' DepartmentID=''><IPAddress></IPAddress></IPPOOL></MS_UPDATEIPPOOL_INPUT>")

                objParentNode = objInputXml.DocumentElement.SelectSingleNode("IPPOOL")
                objChildNode = objInputXml.DocumentElement.SelectSingleNode("IPPOOL/IPAddress")
                objChildNodeClone = objChildNode.CloneNode(True)
                objParentNode.RemoveChild(objChildNode)
                With objParentNode
                    If Request.QueryString("Action").ToUpper = "U" Then
                        .Attributes("PoolID").Value() = objEn.Decrypt(Request.QueryString("PoolId").Trim)
                    End If
                    .Attributes("PoolName").Value() = txtPoolName.Text
                    If drpLstAoffice.SelectedIndex <> 0 Then
                        .Attributes("Aoffice").Value() = drpLstAoffice.SelectedValue
                    Else
                        .Attributes("Aoffice").Value() = ""
                    End If
                    If drpLstDepartment.SelectedIndex <> 0 Then
                        .Attributes("DepartmentID").Value() = drpLstDepartment.SelectedValue
                    Else
                        .Attributes("DepartmentID").Value() = ""
                    End If
                End With

                For RowNo = 0 To dbgrdIpPool.Rows.Count - 1
                    objChildNodeClone.InnerText = dbgrdIpPool.Rows(RowNo).Cells(0).Text
                    objParentNode.AppendChild(objChildNodeClone)
                    objChildNodeClone = objChildNode.CloneNode(True)
                Next
                'Here Back end Method Call
                objOutputXml = objbzIPPool.Update(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If Request.QueryString("Action").ToUpper = "U" Then
                        lblError.Text = objeAAMSMessage.messUpdate '"Record Updated successfully."
                        txtIpAddress.Text = ""
                        Response.Redirect("MSUP_IPPool.aspx?Action=U&Msg=U&PoolID=" & objEn.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("IPPOOL").Attributes("PoolID").Value.Trim))
                    Else
                        lblError.Text = objeAAMSMessage.messInsert '"Record Added successfully."
                        'strPoolId = objOutputXml.DocumentElement.SelectSingleNode("IPPOOL").Attributes("PoolID").Value
                        txtIpAddress.Text = ""
                        Response.Redirect("MSUP_IPPool.aspx?Action=U&Msg=A&PoolID=" & objEn.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("IPPOOL").Attributes("PoolID").Value.Trim))
                        'ReBindIPPoolDetailsGrid(strPoolId)
                    End If
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            End Try

        End If
    End Sub
#End Region
   
#Region " This dbgrdIpPool_RowCommand Event is used for Deleting/Editing the record From GridView"
    Protected Sub dbgrdIpPool_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dbgrdIpPool.RowCommand

        Dim RowNo As Integer
        Dim dtIpPoolDetails As New DataTable
        Dim DrowIpPoolDetails As DataRow
        Dim strIpAddress As String
        Try
            dtIpPoolDetails.Columns.Add("IPAddress", GetType(String))
            If e.CommandName = "Deletex" Then
                strIpAddress = e.CommandArgument.ToString()
                For RowNo = 0 To dbgrdIpPool.Rows.Count - 1
                    If (dbgrdIpPool.Rows(RowNo).Cells(0).Text <> strIpAddress) Then
                        DrowIpPoolDetails = dtIpPoolDetails.NewRow
                        DrowIpPoolDetails("IPAddress") = dbgrdIpPool.Rows(RowNo).Cells(0).Text
                        dtIpPoolDetails.Rows.Add(DrowIpPoolDetails)
                    End If
                Next
                dbgrdIpPool.DataSource = dtIpPoolDetails
                dbgrdIpPool.DataBind()
                lblError.Text = objeAAMSMessage.messDelete
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region " btnReset_Click Event"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If (Request.QueryString("Action") = "U") Then
                ViewIPPoolGroup()
            Else
                txtPoolName.Text = ""
                txtIpAddress.Text = ""
                'drpLstAoffice.SelectedIndex = 0
                drpLstDepartment.SelectedIndex = 0
                dbgrdIpPool.DataSource = Nothing
                dbgrdIpPool.DataBind()
                txtPoolName.Focus()
            End If
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
        lblError.Text = ""
    End Sub
#End Region
End Class
