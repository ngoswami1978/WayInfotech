'#############################################################
'############   Page Name -- HelpDesk_HDSR_RequestType #######
'############   Date 26-February 2007  ########################
'############   Developed By Abhishek  #######################
'#############################################################
#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data
#End Region
Partial Class ETHelpDesk_HDSR_RequestType
    Inherits System.Web.UI.Page
#Region "Page level variable/Objects Declaration"
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
#End Region
#Region "Page_Load Event Declaration"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objInputXml As New XmlDocument
            Session("PageName") = Request.Url.ToString() '"HelpDesk/HDSR_RequestType.aspx"
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            btnSearch.Attributes.Add("onclick", "return RequestTypeMandatory();")
            btnReset.Attributes.Add("onclick", "return RequestTypeReset();")
            btnNew.Attributes.Add("onclick", "return NewHDSRRequestType();")
            If Not Page.IsPostBack Then
                '  objeAAMS.BindDropDown(drpCatName, "CategoryName", True)
                If Not Session("Act") Is Nothing Then
                    If Session("Act").ToString().Split("|").GetValue(0) = "D" Then

                        drpCatName.SelectedValue = Session("Act").ToString().Split("|").GetValue(2)
                        txtType.Text = Session("Act").ToString().Split("|").GetValue(3)
                        txtTypeToPrint.Text = Session("Act").ToString().Split("|").GetValue(4)
                        RequestTypeSearch()
                        lblError.Text = objeAAMSMessage.messDelete
                        Session("Act") = Nothing
                    End If
                End If
            End If
            If Not Request.QueryString("Action") Is Nothing Then
                If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
                    RequestTypeDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))
                End If
            End If

            ' #######################################
            ' ########## This Code code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='City']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='City']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnSearch.Enabled = False
                    End If
                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
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
#End Region    'End Sub
#Region "btnSearch_Click"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            RequestTypeSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "Method for search Request Type"
    Private Sub RequestTypeSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            'Dim objbzbzRequestType As New AAMS.bizETrackerHelpDesk.bzRequestType
            Dim objbzbzRequestType As New AAMS.bizETrackerHelpDesk.bzRequestType

            objInputXml.LoadXml("<HD_SEARCHREQUESTTYPE_INPUT><HD_RETYPE_NAME /><HD_RE_TYCAT_NAME/><HD_RETYPE_PRINT/></HD_SEARCHREQUESTTYPE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("HD_RETYPE_NAME").InnerText = txtType.Text
            If drpCatName.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("HD_RE_TYCAT_NAME").InnerText = drpCatName.SelectedValue
            End If
            objInputXml.DocumentElement.SelectSingleNode("HD_RETYPE_PRINT").InnerText = txtTypeToPrint.Text
            'Here Back end Method Call

            objOutputXml = objbzbzRequestType.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvRequestType.DataSource = ds.Tables("RequestType")
                gvRequestType.DataBind()
            Else
                gvRequestType.DataSource = Nothing
                gvRequestType.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region " Sub Procedure Called for deletion of RequestType"
    Sub RequestTypeDelete(ByVal strRequestTypeId As String)
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            '            Dim objbzbzRequestType As New AAMS.bizETrackerHelpDesk.bzRequestType
            Dim objbzbzRequestType As New AAMS.bizETrackerHelpDesk.bzRequestType


            objInputXml.LoadXml("<HD_DELETEREQUESTTYPE_INPUT><HD_RETYPE_ID/></HD_DELETEREQUESTTYPE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("HD_RETYPE_ID").InnerText = strRequestTypeId
            'Here Back end Method Call

            objOutputXml = objbzbzRequestType.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                'lblError.Text = objeAAMSMessage.messDelete '"Record deleted successfully."
                Session("Act") = Request.QueryString("Action")
                Response.Redirect("HDSR_RequestType.aspx", False)
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "gvRequestType_RowDataBound Event fired on every row creation in  gridview"
    Protected Sub gvRequestType_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvRequestType.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim hdstrHDRETYPEID As HiddenField
            Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            linkEdit = e.Row.FindControl("linkEdit")
            Dim linkDelete As System.Web.UI.HtmlControls.HtmlAnchor
            linkDelete = e.Row.FindControl("linkDelete")
            hdstrHDRETYPEID = e.Row.FindControl("HDRETYPEID")

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='City']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='City']").Attributes("Value").Value)
                    If strBuilder(3) = "0" Then
                        linkDelete.Disabled = True
                    Else
                        linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdstrHDRETYPEID.Value & ");")
                    End If
                    If strBuilder(2) = "0" Then
                        linkEdit.Disabled = True
                    Else
                        linkEdit.Attributes.Add("onclick", "return EditFunction(" & hdstrHDRETYPEID.Value & ");")
                    End If
                Else
                End If
            Else
                linkDelete.Disabled = False
                linkEdit.Disabled = False
                linkEdit.Attributes.Add("onclick", "return EditFunction(" & hdstrHDRETYPEID.Value & ");")
                linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdstrHDRETYPEID.Value & ");")
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
