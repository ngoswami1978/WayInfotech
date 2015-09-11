'##############################################################
'############   Page Name -- HelpDesk_HDSR_BDRLetter    #######
'############   Date 27-February 2007  ########################
'############   Developed By Abhishek  ########################
'##############################################################
#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data
#End Region
Partial Class HelpDesk_HDSR_BDRLetter
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
            btnSearch.Attributes.Add("onclick", "return BDRLetterMandatory();")
            btnReset.Attributes.Add("onclick", "return BDRLetterReset();")
            ' btnNew.Attributes.Add("onclick", "return NewHDSRRequestType();")
            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(drpReqType, "REQUESTTYPE", True)
                objeAAMS.BindDropDown(drpBdrSentBy, "EMPLOYEE", True)
                objeAAMS.BindDropDown(drpAirLine, "AIRLINE", True)
                objeAAMS.BindDropDown(drp1Aoffice, "AOFFICE", True)
                If Not Session("Act") Is Nothing Then
                    If Session("Act").ToString().Split("|").GetValue(0) = "D" Then

                        'drpCatName.SelectedValue = Session("Act").ToString().Split("|").GetValue(2)
                        'txtType.Text = Session("Act").ToString().Split("|").GetValue(3)
                        'txtTypeToPrint.Text = Session("Act").ToString().Split("|").GetValue(4)
                        BdrLetterSearch()
                        lblError.Text = objeAAMSMessage.messDelete
                        Session("Act") = Nothing
                    End If
                End If
            End If
            If Not Request.QueryString("Action") Is Nothing Then
                If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
                    BdrLetterDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))
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
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        Response.Redirect("../NoRights.aspx")
                    End If
                    If strBuilder(1) = "0" Then
                        ' btnNew.Enabled = False
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
            BdrLetterSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "Method for search BdrLetter"
    Private Sub BdrLetterSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbzRequestBDR As New AAMS.bizHelpDesk.bzRequestBDR
            objInputXml.LoadXml("<HD_SEARCHBDRLETTER_INPUT><AGENCY></AGENCY><LCODE></LCODE><WHOLEGROUP></WHOLEGROUP><HD_RE_BDR_ID></HD_RE_BDR_ID><HD_RE_BDR_TICKETS></HD_RE_BDR_TICKETS><LTRNO></LTRNO><HD_RE_BDR_SENDBYID></HD_RE_BDR_SENDBYID><AIRLINE_CODE></AIRLINE_CODE><AOFFICE></AOFFICE><AIRLINE_OFFICE></AIRLINE_OFFICE><HD_RETYPE_ID></HD_RETYPE_ID><BDRLOGGEDDATEFROM></BDRLOGGEDDATEFROM><BDRLOGGEDDATETO></BDRLOGGEDDATETO></HD_SEARCHBDRLETTER_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("AGENCY").InnerText = txtAgencyName.Text
            objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = ""
            If (chkWholeGroup.Checked = True) Then
                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "1" ' CInt(chkWholeGroup.Checked)
            Else
                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "0" ' CInt(chkWholeGroup.Checked)
            End If

            objInputXml.DocumentElement.SelectSingleNode("HD_RE_BDR_ID").InnerText = txtBDrId.Text
            objInputXml.DocumentElement.SelectSingleNode("HD_RE_BDR_TICKETS").InnerText = txtBDRTicket.Text
            objInputXml.DocumentElement.SelectSingleNode("LTRNO").InnerText = txtLtrNo.Text
            If (drpBdrSentBy.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("HD_RE_BDR_SENDBYID").InnerText = drpBdrSentBy.SelectedValue
            End If
            If (drpAirLine.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AIRLINE_CODE").InnerText = drpAirLine.SelectedValue
            End If
            If (drp1Aoffice.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = drp1Aoffice.SelectedValue
            End If

            objInputXml.DocumentElement.SelectSingleNode("AIRLINE_OFFICE").InnerText = txtAirLineoffice.Text
            If (drpReqType.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("HD_RETYPE_ID").InnerText = drpReqType.SelectedValue
            End If
            If (Request("txtBDRLoggedDateFrom") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("BDRLOGGEDDATEFROM").InnerText = objeAAMS.ConvertTextDate(Request("txtBDRLoggedDateFrom").ToString())
            Else
                objInputXml.DocumentElement.SelectSingleNode("BDRLOGGEDDATEFROM").InnerText = objeAAMS.ConvertTextDate(txtBDRLoggedDateFrom.Text)
            End If
            If (Request("txtBDRLoggedDateTo") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("BDRLOGGEDDATETO").InnerText = objeAAMS.ConvertTextDate(Request("txtBDRLoggedDateTo").ToString())
            Else
                objInputXml.DocumentElement.SelectSingleNode("BDRLOGGEDDATETO").InnerText = objeAAMS.ConvertTextDate(txtBDRLoggedDateTo.Text)
            End If




            'Here Back end Method Call
            objOutputXml.LoadXml("<HD_SEARCHBDRLETTER_OUTPUT><BDRLetter	HD_RE_BDR_ID = '1' LTRNO ='1' AGENCYNAME ='Cox' ADDRESS='' REQUESTTYPE ='' HD_RE_BDR_TICKETS =''  HD_RE_BDR_DATESEND =''	STATUS='' AIRLINE='' AIRLINEOFFICEADDRESS='' HD_RE_BDR_SENDBY='' />	<Errors Status='FALSE'><Error Code='' Description='' /></Errors></HD_SEARCHBDRLETTER_OUTPUT>")
            ' objOutputXml = objbzRequestBDR.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvBDRLetter.DataSource = ds.Tables("BDRLetter")
                gvBDRLetter.DataBind()
            Else
                gvBDRLetter.DataSource = Nothing
                gvBDRLetter.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region " Sub Procedure Called for deletion of RequestType"
    Sub BdrLetterDelete(ByVal strBdrLetterId As String)
        Try
            'Dim objInputXml, objOutputXml As New XmlDocument
            'Dim objbzbzRequestType As New AAMS.bizHelpDesk.bzRequestType

            'objInputXml.LoadXml("<HD_DELETEREQUESTTYPE_INPUT><HD_RETYPE_ID/></HD_DELETEREQUESTTYPE_INPUT>")
            'objInputXml.DocumentElement.SelectSingleNode("HD_RETYPE_ID").InnerText = strBdrLetterId
            ''Here Back end Method Call

            'objOutputXml = objbzbzRequestType.Delete(objInputXml)
            'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '    'lblError.Text = objeAAMSMessage.messDelete '"Record deleted successfully."
            '    Session("Act") = Request.QueryString("Action")
            '    Response.Redirect("HDSR_BDRLetter.aspx", False)
            'Else
            '    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            'End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "gvBDRLetter_RowDataBound Event fired on every row creation in  gridview"
    Protected Sub gvBDRLetter_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvBDRLetter.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim hdstrHDHDREBDRID As HiddenField
            Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            linkEdit = e.Row.FindControl("linkEdit")
            Dim linkDelete As System.Web.UI.HtmlControls.HtmlAnchor
            linkDelete = e.Row.FindControl("linkDelete")
            hdstrHDHDREBDRID = e.Row.FindControl("HDHDREBDRID")

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BDRLetter']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BDRLetter']").Attributes("Value").Value)
                    If strBuilder(3) = "0" Then
                        linkDelete.Disabled = True
                    Else
                        linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdstrHDHDREBDRID.Value & ");")
                    End If
                    If strBuilder(2) = "0" Then
                        linkEdit.Disabled = True
                    Else
                        linkEdit.Attributes.Add("onclick", "return EditFunction(" & hdstrHDHDREBDRID.Value & ");")
                    End If
                Else
                End If
            Else
                linkDelete.Disabled = False
                linkEdit.Disabled = False
                linkEdit.Attributes.Add("onclick", "return EditFunction(" & hdstrHDHDREBDRID.Value & ");")
                linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdstrHDHDREBDRID.Value & ");")
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
