'##############################################################
'############   Page Name -- HelpDesk_HDSR_BDR          #######
'############   Date 27-February 2007  ########################
'############   Developed By Abhishek  ########################
'##############################################################
#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data
#End Region
Partial Class BOHelpDesk_HDSR_BDR
    Inherits System.Web.UI.Page

#Region "Page level variable/Objects Declaration"
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
#End Region
#Region "Page_Load Event Declaration"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblError.Text = ""
            Dim objInputXml, objInputXmlBDRSerachParam As New XmlDocument
            Session("PageName") = Request.Url.ToString() '"HelpDesk/HDSR_RequestType.aspx"
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If

            btnSearch.Attributes.Add("onclick", "return BDRMandatory();")
            ' btnReset.Attributes.Add("onclick", "return BDRReset();")
            drpReqType.Attributes.Add("onkeyup", "return gotop('drpReqType');")
            drpAirLine.Attributes.Add("onkeyup", "return gotop('drpAirLine');")
            drp1Aoffice.Attributes.Add("onkeyup", "return gotop('drp1Aoffice');")
            txtAgencyName.Attributes.Add("onkeydown", "return ActDeAct();")
            ' btnNew.Attributes.Add("onclick", "return NewHDSRRequestType();")
            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(drpReqType, "BOListFuntionalCategory", True, 3)
                ' objeAAMS.BindDropDown(drpBdrSentBy, "EMPLOYEE", True)
                objeAAMS.BindDropDown(drpAirLine, "AIRLINE", True, 3)
                objeAAMS.BindDropDown(drp1Aoffice, "AOFFICE", True, 3)

            End If
            ' #######################################
            ' ########## This Code code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO BDRLetter']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO BDRLetter']").Attributes("Value").Value)

                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                    End If
                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                    If strBuilder(4) = "0" Then
                        btnExport.Enabled = False
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


            '   Checking Permission For Own Office start.
            If objeAAMS.Limited_To_Aoffice(Session("Security")) <> "" Then
                drp1Aoffice.SelectedValue = objeAAMS.Limited_To_Aoffice(Session("Security"))
                drp1Aoffice.Enabled = False
            End If

            If hdDeleteBDRID.Value <> "" Then
                BdrDelete(hdDeleteBDRID.Value)
            End If

            '   Checking Permission For Own Office end.

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region    'End Sub
#Region "btnSearch_Click"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            BdrSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "Method for search Bdr"
    Private Sub BdrSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim objSecurityXml As New XmlDocument
        Dim ds As New DataSet
        Try
            '<PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
            Dim objbzRequestBDR As New AAMS.bizBOHelpDesk.bzRequestBDR
            'objInputXml.LoadXml("<HD_SEARCHBDRLETTER_INPUT> <AGENCY></AGENCY><LCODE></LCODE><WHOLEGROUP></WHOLEGROUP><HD_RE_BDR_ID></HD_RE_BDR_ID><HD_RE_BDR_TICKETS></HD_RE_BDR_TICKETS> <LTRNO></LTRNO> <HD_RE_BDR_SENDBYID></HD_RE_BDR_SENDBYID><AIRLINE_CODE></AIRLINE_CODE><AOFFICE></AOFFICE> <AIRLINE_OFFICE></AIRLINE_OFFICE> <CALL_CATEGORY_ID></CALL_CATEGORY_ID> <BDRLOGGEDDATEFROM></BDRLOGGEDDATEFROM> <BDRLOGGEDDATETO></BDRLOGGEDDATETO> <RESP_1A></RESP_1A> <Limited_To_OwnAagency></Limited_To_OwnAagency> <Limited_To_Aoffice></Limited_To_Aoffice> <Limited_To_Region></Limited_To_Region> </HD_SEARCHBDRLETTER_INPUT>")
            ' objInputXml.LoadXml("<HD_SEARCHBDRLETTER_INPUT> <AGENCY></AGENCY><LCODE></LCODE><WHOLEGROUP></WHOLEGROUP><HD_RE_BDR_ID></HD_RE_BDR_ID><HD_RE_BDR_TICKETS></HD_RE_BDR_TICKETS> <LTRNO></LTRNO> <HD_RE_BDR_SENDBYNAME></HD_RE_BDR_SENDBYNAME><AIRLINE_CODE></AIRLINE_CODE><AOFFICE></AOFFICE> <AIRLINE_OFFICE></AIRLINE_OFFICE> <CALL_CATEGORY_ID></CALL_CATEGORY_ID> <BDRLOGGEDDATEFROM></BDRLOGGEDDATEFROM> <BDRLOGGEDDATETO></BDRLOGGEDDATETO> <RESP_1A></RESP_1A> <Limited_To_OwnAagency></Limited_To_OwnAagency> <Limited_To_Aoffice></Limited_To_Aoffice> <Limited_To_Region></Limited_To_Region> </HD_SEARCHBDRLETTER_INPUT>")
            objInputXml.LoadXml("<HD_SEARCHBDRLETTER_INPUT> <AGENCY></AGENCY><LCODE></LCODE><WHOLEGROUP></WHOLEGROUP><HD_RE_BDR_ID></HD_RE_BDR_ID><HD_RE_BDR_TICKETS></HD_RE_BDR_TICKETS> <LTRNO></LTRNO> <HD_RE_BDR_SENDBYNAME></HD_RE_BDR_SENDBYNAME><AIRLINE_CODE></AIRLINE_CODE><AOFFICE></AOFFICE> <AIRLINE_OFFICE></AIRLINE_OFFICE> <CALL_CATEGORY_ID></CALL_CATEGORY_ID> <BDRLOGGEDDATEFROM></BDRLOGGEDDATEFROM> <BDRLOGGEDDATETO></BDRLOGGEDDATETO> <RESP_1A></RESP_1A> <Limited_To_OwnAagency></Limited_To_OwnAagency> <Limited_To_Aoffice></Limited_To_Aoffice> <Limited_To_Region></Limited_To_Region> <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHBDRLETTER_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("AGENCY").InnerText = txtAgencyName.Text


            'If Request.Form("txtAgencyName") = "" And hdAgencyNameId.Value = "" Then
            '    objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = ""
            'Else
            '    objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyNameId.Value.Trim()
            'End If

            If (Request.Form("txtAgencyName") <> "" And hdAgencyNameId.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyNameId.Value = "") Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = ""
            Else
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyNameId.Value.Trim()
            End If

            'If (chkWholeGroup.Checked = True) Then
            '    objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "1" ' CInt(chkWholeGroup.Checked)
            'Else
            '    objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "0" ' CInt(chkWholeGroup.Checked)
            'End If
            objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "0"
            objInputXml.DocumentElement.SelectSingleNode("HD_RE_BDR_ID").InnerText = txtBDrId.Text
            objInputXml.DocumentElement.SelectSingleNode("HD_RE_BDR_TICKETS").InnerText = txtBDRTicket.Text
            objInputXml.DocumentElement.SelectSingleNode("LTRNO").InnerText = txtLtrNo.Text
            'If (drpBdrSentBy.SelectedIndex <> 0) Then
            '    objInputXml.DocumentElement.SelectSingleNode("HD_RE_BDR_SENDBYID").InnerText = drpBdrSentBy.SelectedValue
            'End If
            'If (drpBdrSentBy.SelectedIndex <> 0) Then
            ' objInputXml.DocumentElement.SelectSingleNode("HD_RE_BDR_SENDBYID").InnerText = hdEmployeeId.Value 'txtBdrSentBy.Text  drpBdrSentBy.SelectedValue
            'End If
            objInputXml.DocumentElement.SelectSingleNode("HD_RE_BDR_SENDBYNAME").InnerText = txtBdrSentBy.Text ' drpBdrSentBy.SelectedValue
            If (drpAirLine.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AIRLINE_CODE").InnerText = drpAirLine.SelectedValue
            End If
            If (drp1Aoffice.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = drp1Aoffice.SelectedValue
            End If

            objInputXml.DocumentElement.SelectSingleNode("AIRLINE_OFFICE").InnerText = txtAirLineoffice.Text
            If (drpReqType.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("CALL_CATEGORY_ID").InnerText = drpReqType.SelectedValue
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


            objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If


            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = 1
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = 0
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = 0
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                End If
            End If

            'Start CODE for sorting and paging

            If ViewState("PrevSearching") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
            Else
                Dim objTempInputXml As New XmlDocument
                Dim objNodeList As XmlNodeList

                objTempInputXml.LoadXml(ViewState("PrevSearching"))
                objNodeList = objTempInputXml.DocumentElement.ChildNodes
                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                For Each objNode As XmlNode In objNodeList
                    If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                        If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                            objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                            ddlPageNumber.SelectedValue = "1"
                        End If
                    End If
                Next
            End If


            objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "LTRNO"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "LTRNO" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If




            'Here Back end Method Call
            'objOutputXml.LoadXml("<HD_SEARCHBDRLETTER_OUTPUT><BDRLetter HD_RE_BDR_ID = '1'  LTRNO ='830'  AGENCYNAME ='Cox'   ADDRESS='A/435435'  QUERYTYPE ='4'  HD_RE_BDR_TICKETS ='444' HD_RE_BDR_DATESEND =''  STATUS=''   AIRLINE='' AIRLINEOFFICEADDRESS=''  HD_RE_BDR_SENDBY='' /><Errors Status='FALSE'> <Error Code='' Description='' /></Errors></HD_SEARCHBDRLETTER_OUTPUT>")
            objOutputXml = objbzRequestBDR.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If


            Session("BDRSerachParam") = objInputXml.OuterXml

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvBdr.DataSource = ds.Tables("BDRLetter")
                gvBdr.DataBind()

                '@ Code Added For Paging And Sorting 
                pnlPaging.Visible = True
                BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                txtRecordOnCurrentPage.Text = ds.Tables("BDRLetter").Rows.Count.ToString

                txtRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                SetImageForSorting(gvBdr)

                '@ End of Code Added For Paging And Sorting 
            Else
                gvBdr.DataSource = Nothing
                gvBdr.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

                txtRecordCount.Text = "0"
                txtRecordOnCurrentPage.Text = "0"
                pnlPaging.Visible = False
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            pnlPaging.Visible = False
        Finally
            'txtBdrSentBy.Text = hdEmployeeName.Value
            If Request("txtBDRLoggedDateFrom") IsNot Nothing Then
                txtBDRLoggedDateFrom.Text = Request("txtBDRLoggedDateFrom")
            End If
            If Request("txtBDRLoggedDateTo") IsNot Nothing Then
                txtBDRLoggedDateTo.Text = Request("txtBDRLoggedDateTo")
            End If


        End Try


    End Sub
#End Region

#Region " Sub Procedure Called for deletion of RequestType"
    Sub BdrDelete(ByVal strBdrId As String)
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbbzRequestBDR As New AAMS.bizBOHelpDesk.bzRequestBDR

            objInputXml.LoadXml("<HD_DELETEBDRLETTER_INPUT><HD_RE_BDR_ID/></HD_DELETEBDRLETTER_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("HD_RE_BDR_ID").InnerText = strBdrId
            hdDeleteBDRID.Value = ""

            'Here Back end Method Call


            objOutputXml = objbbzRequestBDR.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messDelete '"Record deleted successfully."
                'Session("Act") = Request.QueryString("Action")
                'If (Request.QueryString("PopUp")) Is Nothing Then
                '    Response.Redirect("HDSR_Bdr.aspx", False)
                'Else
                '    Response.Redirect("HDSR_Bdr.aspx?Popup=T", False)
                'End If

                ' ###################################################################
                '@ Code Added For Paging And Sorting In case Of Delete The Record
                ' ###################################################################
                Dim CurrentPage As Integer = CInt(IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue))
                If txtRecordOnCurrentPage.Text = "1" Then
                    If CurrentPage - 1 > 0 Then
                        ddlPageNumber.SelectedValue = (CurrentPage - 1).ToString
                    Else
                        ddlPageNumber.SelectedValue = "1"
                    End If
                End If
                ' ###################################################################
                '@ End of Code Added For Paging And Sorting In case Of Delete The Record
                ' ###################################################################
                BdrSearch()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "gvBdr_RowDataBound Event fired on every row creation in  gridview"
    Protected Sub gvBdr_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvBdr.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If

            '#############################################################
            ' Code added For Selecting an Items 

            'Dim lnkSelect As System.Web.UI.HtmlControls.HtmlAnchor
            Dim lnkSelect As LinkButton
            'Dim hdSelect As HiddenField
            'hdSelect = (CType(e.Row.FindControl("hdSelect"), HiddenField))
            lnkSelect = e.Row.FindControl("lnkSelect")
            If (Request.QueryString("PopUp")) Is Nothing Then
                lnkSelect.Visible = False
            Else
                lnkSelect.Visible = True
                'lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & hdSelect.Value & "');")
                lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & lnkSelect.CommandArgument & "');")
            End If

            '#############################################################
            Dim hdstrHDHDREBDRID As HiddenField
            Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            linkEdit = e.Row.FindControl("linkEdit")
            Dim btnDelete As LinkButton
            btnDelete = e.Row.FindControl("btnDelete")
            hdstrHDHDREBDRID = e.Row.FindControl("HDHDREBDRID")

            '@ Code Used For Encription/Decription
            Dim EnstrQueryStringForHDREBDRID As String

            EnstrQueryStringForHDREBDRID = objED.Encrypt(hdstrHDHDREBDRID.Value) 'Commented on 13 apr 10


            '@ End of Code Used For Encription/Decription


            'Dim linkHistory As System.Web.UI.HtmlControls.HtmlAnchor
            'linkHistory = e.Row.FindControl("linkHistory")
            'linkHistory.Attributes.Add("onclick", "return PopupHistoryPage(" & "'" & hdstrHDHDREBDRID.Value & "'" & ");")

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO BDRLetter']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO BDRLetter']").Attributes("Value").Value)
                    If strBuilder(3) = "0" Then
                        btnDelete.Enabled = False
                    Else
                        btnDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdstrHDHDREBDRID.Value & ");")

                    End If
                    'If strBuilder(2) = "0" Then
                    '    linkEdit.Disabled = True
                    'Else
                    '    linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdstrHDHDREBDRID.Value) & "');")
                    'End If
                    linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdstrHDHDREBDRID.Value & "');")
                Else
                    linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdstrHDHDREBDRID.Value & "');")
                End If
            Else
                btnDelete.Enabled = True
                linkEdit.Disabled = False
                linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdstrHDHDREBDRID.Value & "');")
                ' linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdstrHDHDREBDRID.Value & ");")
                btnDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdstrHDHDREBDRID.Value & ");")
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region


#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            BdrSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            BdrSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            BdrSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvBdr_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvBdr.Sorted
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gvBdr_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvBdr.Sorting
        Try
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ViewState("Desc") = "FALSE"
            Else
                If ViewState("SortName") = SortName Then
                    If ViewState("Desc") = "TRUE" Then
                        ViewState("Desc") = "FALSE"
                    Else
                        ViewState("Desc") = "TRUE"
                    End If
                Else
                    ViewState("SortName") = SortName
                    ViewState("Desc") = "FALSE"
                End If
            End If
            BdrSearch()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub gvBdr_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvBdr.RowCreated
        Try
            'Dim grvRow As GridViewRow
            'grvRow = e.Row
            'If e.Row.RowType = DataControlRowType.Header Then
            '    If gvManageAgencyGroup.AllowSorting = True Then
            '        CType(grvRow.Cells(0).Controls(0), LinkButton).Text = rdSummOpt.SelectedItem.Text
            '    Else
            '        e.Row.Cells(0).Text = rdSummOpt.SelectedItem.Text
            '    End If
            'End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub BindControlsForNavigation(ByVal CrrentPageNo As String)
        ' ##################################################################
        '@ Code Added For Paging And Sorting
        ' ###################################################################
        pnlPaging.Visible = True
        '  Dim count As Integer = 0
        Dim count As Integer = CInt(CrrentPageNo)
        Dim selectedValue As String = IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue)
        If count <> ddlPageNumber.Items.Count Then
            ddlPageNumber.Items.Clear()
            For i As Integer = 1 To count
                ddlPageNumber.Items.Add(i.ToString)
            Next
        End If
        ddlPageNumber.SelectedValue = selectedValue
        'Code for hiding prev and next button based on count
        If count = 1 Then
            lnkNext.Visible = False
            lnkPrev.Visible = False
        Else

            lnkPrev.Visible = True
            lnkNext.Visible = True
        End If

        'Code for hiding next button when pagenumber is equal to page count
        If ddlPageNumber.SelectedValue = count.ToString Then
            lnkNext.Visible = False
        Else
            lnkNext.Visible = True
        End If

        'Code for hiding prev button when pagenumber is 1
        If ddlPageNumber.SelectedValue = "1" Then
            lnkPrev.Visible = False
        Else
            lnkPrev.Visible = True
        End If

        ' ###################################################################
        '@ End of Code Added For Paging And Sorting 
        ' ###################################################################
    End Sub
    Private Sub SetImageForSorting(ByVal grd As GridView)
        Dim imgUp As New Image
        imgUp.ImageUrl = "~/Images/Sortup.gif"
        Dim imgDown As New Image
        imgDown.ImageUrl = "~/Images/Sortdown.gif"
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Dim intcol As Integer = grd.Columns.IndexOf(field)
                If ViewState("Desc") = "FALSE" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "TRUE" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If
            End If
        Next
    End Sub

#End Region

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            '  Response.Redirect("HDSR_BDR.aspx", False)
            Response.Redirect(Request.Url.ToString(), False)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim objSecurityXml As New XmlDocument
        Dim ds As New DataSet
        Try
            '<PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
            Dim objbzRequestBDR As New AAMS.bizBOHelpDesk.bzRequestBDR
            'objInputXml.LoadXml("<HD_SEARCHBDRLETTER_INPUT> <AGENCY></AGENCY><LCODE></LCODE><WHOLEGROUP></WHOLEGROUP><HD_RE_BDR_ID></HD_RE_BDR_ID><HD_RE_BDR_TICKETS></HD_RE_BDR_TICKETS> <LTRNO></LTRNO> <HD_RE_BDR_SENDBYID></HD_RE_BDR_SENDBYID><AIRLINE_CODE></AIRLINE_CODE><AOFFICE></AOFFICE> <AIRLINE_OFFICE></AIRLINE_OFFICE> <CALL_CATEGORY_ID></CALL_CATEGORY_ID> <BDRLOGGEDDATEFROM></BDRLOGGEDDATEFROM> <BDRLOGGEDDATETO></BDRLOGGEDDATETO> <RESP_1A></RESP_1A> <Limited_To_OwnAagency></Limited_To_OwnAagency> <Limited_To_Aoffice></Limited_To_Aoffice> <Limited_To_Region></Limited_To_Region> </HD_SEARCHBDRLETTER_INPUT>")
            ' objInputXml.LoadXml("<HD_SEARCHBDRLETTER_INPUT> <AGENCY></AGENCY><LCODE></LCODE><WHOLEGROUP></WHOLEGROUP><HD_RE_BDR_ID></HD_RE_BDR_ID><HD_RE_BDR_TICKETS></HD_RE_BDR_TICKETS> <LTRNO></LTRNO> <HD_RE_BDR_SENDBYNAME></HD_RE_BDR_SENDBYNAME><AIRLINE_CODE></AIRLINE_CODE><AOFFICE></AOFFICE> <AIRLINE_OFFICE></AIRLINE_OFFICE> <CALL_CATEGORY_ID></CALL_CATEGORY_ID> <BDRLOGGEDDATEFROM></BDRLOGGEDDATEFROM> <BDRLOGGEDDATETO></BDRLOGGEDDATETO> <RESP_1A></RESP_1A> <Limited_To_OwnAagency></Limited_To_OwnAagency> <Limited_To_Aoffice></Limited_To_Aoffice> <Limited_To_Region></Limited_To_Region> </HD_SEARCHBDRLETTER_INPUT>")
            objInputXml.LoadXml("<HD_SEARCHBDRLETTER_INPUT> <AGENCY></AGENCY><LCODE></LCODE><WHOLEGROUP></WHOLEGROUP><HD_RE_BDR_ID></HD_RE_BDR_ID><HD_RE_BDR_TICKETS></HD_RE_BDR_TICKETS> <LTRNO></LTRNO> <HD_RE_BDR_SENDBYNAME></HD_RE_BDR_SENDBYNAME><AIRLINE_CODE></AIRLINE_CODE><AOFFICE></AOFFICE> <AIRLINE_OFFICE></AIRLINE_OFFICE> <CALL_CATEGORY_ID></CALL_CATEGORY_ID> <BDRLOGGEDDATEFROM></BDRLOGGEDDATEFROM> <BDRLOGGEDDATETO></BDRLOGGEDDATETO> <RESP_1A></RESP_1A> <Limited_To_OwnAagency></Limited_To_OwnAagency> <Limited_To_Aoffice></Limited_To_Aoffice> <Limited_To_Region></Limited_To_Region> <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHBDRLETTER_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("AGENCY").InnerText = txtAgencyName.Text


            If Request.Form("txtAgencyName") = "" And hdAgencyNameId.Value = "" Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = ""
            Else
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyNameId.Value.Trim()
            End If

            'If (chkWholeGroup.Checked = True) Then
            '    objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "1" ' CInt(chkWholeGroup.Checked)
            'Else
            '    objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "0" ' CInt(chkWholeGroup.Checked)
            'End If
            objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "0"
            objInputXml.DocumentElement.SelectSingleNode("HD_RE_BDR_ID").InnerText = txtBDrId.Text
            objInputXml.DocumentElement.SelectSingleNode("HD_RE_BDR_TICKETS").InnerText = txtBDRTicket.Text
            objInputXml.DocumentElement.SelectSingleNode("LTRNO").InnerText = txtLtrNo.Text
            'If (drpBdrSentBy.SelectedIndex <> 0) Then
            '    objInputXml.DocumentElement.SelectSingleNode("HD_RE_BDR_SENDBYID").InnerText = drpBdrSentBy.SelectedValue
            'End If
            'If (drpBdrSentBy.SelectedIndex <> 0) Then
            ' objInputXml.DocumentElement.SelectSingleNode("HD_RE_BDR_SENDBYID").InnerText = hdEmployeeId.Value 'txtBdrSentBy.Text  drpBdrSentBy.SelectedValue
            'End If
            objInputXml.DocumentElement.SelectSingleNode("HD_RE_BDR_SENDBYNAME").InnerText = txtBdrSentBy.Text ' drpBdrSentBy.SelectedValue
            If (drpAirLine.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AIRLINE_CODE").InnerText = drpAirLine.SelectedValue
            End If
            If (drp1Aoffice.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = drp1Aoffice.SelectedValue
            End If

            objInputXml.DocumentElement.SelectSingleNode("AIRLINE_OFFICE").InnerText = txtAirLineoffice.Text
            If (drpReqType.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("CALL_CATEGORY_ID").InnerText = drpReqType.SelectedValue
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


            objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If


            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = 1
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = 0
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = 0
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                End If
            End If

            'Start CODE for sorting and paging    
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "LTRNO"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "LTRNO" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            'Here Back end Method Call
            'objOutputXml.LoadXml("<HD_SEARCHBDRLETTER_OUTPUT><BDRLetter HD_RE_BDR_ID = '1'  LTRNO ='830'  AGENCYNAME ='Cox'   ADDRESS='A/435435'  QUERYTYPE ='4'  HD_RE_BDR_TICKETS ='444' HD_RE_BDR_DATESEND =''  STATUS=''   AIRLINE='' AIRLINEOFFICEADDRESS=''  HD_RE_BDR_SENDBY='' /><Errors Status='FALSE'> <Error Code='' Description='' /></Errors></HD_SEARCHBDRLETTER_OUTPUT>")
            objOutputXml = objbzRequestBDR.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvBdr.DataSource = ds.Tables("BDRLetter")
                gvBdr.DataBind()

                Dim objExport As New ExportExcel
                Dim strArray() As String = {"LTR No", "Agency Name", "Agency Address", "Requested Type", "BDR Tickets", "Date Sent", "Status", "Airline", "Airline Aoffice Address", "Send By"}
                Dim intArray() As Integer = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}
                objExport.ExportDetails(objOutputXml, "BDRLetter", intArray, strArray, ExportExcel.ExportFormat.Excel, "BDRDetails.xls")



            Else
                gvBdr.DataSource = Nothing
                gvBdr.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            pnlPaging.Visible = False
        Finally
            'txtBdrSentBy.Text = hdEmployeeName.Value
            If Request("txtBDRLoggedDateFrom") IsNot Nothing Then
                txtBDRLoggedDateFrom.Text = Request("txtBDRLoggedDateFrom")
            End If
            If Request("txtBDRLoggedDateTo") IsNot Nothing Then
                txtBDRLoggedDateTo.Text = Request("txtBDRLoggedDateTo")
            End If
        End Try
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
End Class
