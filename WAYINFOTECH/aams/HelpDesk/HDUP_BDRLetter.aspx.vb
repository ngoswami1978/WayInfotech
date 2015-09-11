'#####################################################################
'######   Page Name -- HelpDesk_HDUP_BDRLetter               #########  
'######   Date 27-February 2007  #####################################    
'############   Developed By Abhishek  ###############################
'#####################################################################
#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data
#End Region
Partial Class HelpDesk_HDUP_BDRLetter
    Inherits System.Web.UI.Page

#Region "Page Level Variable/Objects Declaration"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
#End Region
#Region "Page_Load Event"
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

            btnSave.Attributes.Add("onclick", "return RequestTypeCategoryMandatory();")
            ' btnNew.Attributes.Add("onclick", "return NewHDUPReqTypeCategory();")

            If Not Page.IsPostBack Then

                '*******************************************************************
                ''Code For 
                ' objeAAMS.BindDropDown(drpCatName, "CatName", True)

                '*******************************************************************
                ' Load Data For Editing Mode
                If (Request.QueryString("Action") = "U") Then
                    ViewBdrLetter()
                    ' txtCategory.Focus()
                    If (Request.QueryString("Msg") = "A") Then
                        lblError.Text = objeAAMSMessage.messInsert
                    End If
                    If (Request.QueryString("Msg") = "U") Then
                        lblError.Text = objeAAMSMessage.messUpdate
                    End If
                End If

            End If

            ' #######################################
            ' ########## This Code code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BDRLetter']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BDRLetter']").Attributes("Value").Value)
                    If strBuilder(1) = "0" Then
                        ' btnNew.Enabled = False
                        'btnSave.Enabled = False
                    End If
                    If (Request.QueryString("Action") = "U") Then
                        If strBuilder(2) = "0" Then
                            btnSave.Enabled = False
                            Response.Redirect("../NoRights.aspx")
                        End If
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            ' #######################################
            ' ########## End of code used for enable/disable 
            ' ########## the button according to rights
            ' btnReset.Attributes.Add("onclick", "return CityReset();")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region


#Region " Load ViewBdrLetter Data For Editing Mode"
    Sub ViewBdrLetter()
        Try
            If (Not Request.QueryString("Action") = Nothing) Then
                Dim objInputXml, objOutputXml As New XmlDocument
                'Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                Dim objbzRequestTypeCategory As New AAMS.bizHelpDesk.bzRequestTypeCategory
                objInputXml.LoadXml("<HD_VIEWREQUESTTYPECATEGORY_INPUT><HD_RE_TYCAT_ID /></HD_VIEWREQUESTTYPECATEGORY_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("HD_RE_TYCAT_ID").InnerText = Request.QueryString("HD_RE_TYCAT_ID").ToString()
                'Here Back end Method Call
                objOutputXml = objbzRequestTypeCategory.View(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    BindBdrLetterData(objOutputXml)
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "Call BindBdrLetterData For Binding Data in Controls"
    Sub BindBdrLetterData(ByVal objOutputXml As XmlDocument)
        Try
            ' txtAgencyName.Text = objOutputXml.DocumentElement.SelectSingleNode("RequestCategory").Attributes("HD_RE_TYCAT_NAME").InnerXml

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnSave_Click Event"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If (IsValid) Then
            Try
                'If (Not Request.QueryString("Action") = Nothing) Then

                '    Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                '    'Dim objXmlReader As XmlNodeReader
                '    Dim ds As New DataSet
                '    Dim objbzRequestTypeCategory As New AAMS.bizHelpDesk.bzRequestTypeCategory
                '    objInputXml.LoadXml("<HD_UPDATEREQUESTTYPECATEGORY_INPUT><RequestCategory HD_RE_TYCAT_ID='' HD_RE_TYCAT_NAME ='' UT_RE_TYCAT_PROTECTED ='' /></HD_UPDATEREQUESTTYPECATEGORY_INPUT>")
                '    If (Request.QueryString("HD_RE_TYCAT_ID") IsNot Nothing) Then
                '        objInputXml.DocumentElement.SelectSingleNode("RequestCategory").Attributes("HD_RE_TYCAT_ID").Value = Request.QueryString("HD_RE_TYCAT_ID")
                '    End If
                '    objInputXml.DocumentElement.SelectSingleNode("PTR_TYPE").Attributes("HD_RE_TYCAT_NAME").Value = txtCategory.Text


                '    'Here Back end Method Call
                '    objOutputXml = objbzRequestTypeCategory.Update(objInputXml)

                '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                '        If (Request.QueryString("Action") = "U") Then
                '            lblError.Text = objeAAMSMessage.messUpdate ' "Record updated successfully."
                '            Response.Redirect("HDUP_RequestTypeCategory.aspx?&Msg=U&Action=U&HD_RE_TYCAT_ID=" & objOutputXml.DocumentElement.SelectSingleNode("RequestCategory").Attributes("HD_RE_TYCAT_ID").Value, False)
                '        Else
                '            lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."
                '            Response.Redirect("HDUP_RequestTypeCategory.aspx?&Msg=A&Action=U&HD_RE_TYCAT_ID=" & objOutputXml.DocumentElement.SelectSingleNode("RequestCategory").Attributes("HD_RE_TYCAT_ID").Value, False)
                '        End If
                '    Else
                '        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                '    End If
                'End If
            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
        End If
    End Sub
#End Region
#Region "btnReset_Click Event"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If (Request.QueryString("Action") = "U") Then
                ViewBdrLetter()
                txtAgencyName.Focus()
            Else
                txtAgencyName.Text = ""
            End If
            lblError.Text = ""
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
