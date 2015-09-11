'#############################################################
'############   Page Name -- PUSR_CorporateCode        #######
'############   Date 06-January 2007  ########################
'############   Developed By Abhishek ########################
'#############################################################
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data
Partial Class Popup_PUSR_CorporateCode
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder

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
            Dim objtaCorporateCodes As New AAMS.bizTravelAgency.bzCorporateCodes

            Dim objInputXml As New XmlDocument
            Session("PageName") = "TravelAgency/MSSR_CorporateCode.aspx"
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Corporate Codes']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Corporate Codes']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        Response.Redirect("../NoRights.aspx")
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            If Not Page.IsPostBack Then
                btnReset.Attributes.Add("onclick", "return CorporateCodeReset();")
                btnSearch.Attributes.Add("onclick", "return CorporateCodeMandatory();")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Try
                lblError.Text = ""
                CorporateCodeSearch()
            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#Region "Corporate Search Function"
    Private Sub CorporateCodeSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objtaCorporateCodes As New AAMS.bizTravelAgency.bzCorporateCodes
            objInputXml.LoadXml("<MS_SEARCHOFFICEIDQUALIFIERS_INPUT><Code /><Qualifier /><Description /></MS_SEARCHOFFICEIDQUALIFIERS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("Code").InnerText = txtCorporateCode.Text.Trim()
            objInputXml.DocumentElement.SelectSingleNode("Qualifier").InnerText = txtCorporateQualifier.Text.Trim()
            objInputXml.DocumentElement.SelectSingleNode("Description").InnerText = txtDescription.Text.Trim()
            objOutputXml = objtaCorporateCodes.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value().ToString().ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvCorporateCode.DataSource = ds.Tables("Qualifiers").DefaultView
                gvCorporateCode.DataBind()
            Else
                gvCorporateCode.DataSource = Nothing
                gvCorporateCode.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub gvCorporateCode_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvCorporateCode.RowCommand
        Dim strjscript As String = "<script language=""javascript"">"
        strjscript &= "window.returnValue='" + e.CommandArgument.ToString() + "'; window.close();"
        strjscript = strjscript & "</script" & ">"
        Me.litCorporateCode.Text = strjscript
    End Sub
End Class
