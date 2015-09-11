'####################################################
'######## Developed By Abhishek  on 6th May 2011 #### 
'####################################################
Partial Class Sales_SASR_AgencyTarget
    Inherits System.Web.UI.Page
    ' Implements ICallbackEventHandler

    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim str As String
    Dim strBuilder As New StringBuilder
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If
        lblError.Text = ""
        PnlLegand.Visible = False
        '  PnlShowUnhideColumns.Visible = False

       

        'If Session("UpdateData") IsNot Nothing Then
        '    Session("UpdateData") = Nothing
        '    Sorting()
        'End If
        Try
            grdAgencyTarget.PageSize = ConfigurationManager.AppSettings("PAGE_SIZE").ToString

            btnSearch.Attributes.Add("onclick", "return MandatoryFunction();")

            BtnExport.Attributes.Add("onclick", "return ExportData();")
            btn_Select.Attributes.Add("onclick", "return MandatoryFunction();")
            btn_VSelect.Attributes.Add("onclick", "return MandatoryFunction();")
            chbPrevious.Attributes.Add("onClick", "return Enableall();")
            ChkPrevVisPlan.Attributes.Add("onClick", "return EnableVall();")
            rdSummaryOption1.Attributes.Add("onClick", "radionewexisting();")
            rdSummaryOption2.Attributes.Add("onClick", "radionewexisting();")
            btnIncrease.Attributes.Add("onclick", "return CheckValidation();")


            'txtAgencyName.Attributes.Add("onkeydown", "return ActDeAct();")
            'txtLcode.Attributes.Add("onfocusout", "return EnableDisableTargetGroupProductivity();")
            'txtAgencyName.Attributes.Add("onfocusout", "return ActDecTargetLcode();")


            txtAgencyName.Attributes.Add("onkeydown", "return ActDeAct();")
            txtLcode.Attributes.Add("onkeydown", "return ActDeAct();")


            '  btnSearch.Attributes.Add("onclick", "return CheckValidation();")
            Callpostdata()

            If rdSummaryOption1.Checked = True Then
                btnIncrease.Text = "Increase"
            End If
            If rdSummaryOption2.Checked = True Then
                btnIncrease.Text = "Decrease"
            End If
            hdSales.Value = Request.Form(drpSalesPerson.UniqueID)
            

            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(drpTarAoffice, "AOFFICE", True, 3)
                objeAAMS.BindDropDown(drpTarRegion, "REGION1", True, 3)
                'objeAAMS.BindDropDown(DlstAgencyCateg, "AGENCYTYPE", True, 3)
                objeAAMS.BindDropDown(drpTarCountry, "COUNTRY", False, 3)
                FillAgencyCategory()
                BindAll()
                fillSalesName()
                FillVisitPlanCategory()

            End If

            If chbPrevious.Checked = False Then
                If DateTime.Now.Month = 1 Then
                    drpPreviousMonth.SelectedValue = 12
                    drpPreviousYear.SelectedValue = DateTime.Now.Year - 1
                Else
                    drpPreviousYear.SelectedValue = DateTime.Now.Year
                    drpPreviousMonth.SelectedValue = Date.Today.Month - 1
                End If
            End If
            If ChkPrevVisPlan.Checked = False Then
                If DateTime.Now.Month = 1 Then
                    drpVPreviousMonth.SelectedValue = 12
                    drpVPreviousYear.SelectedValue = DateTime.Now.Year - 1
                Else
                    drpVPreviousYear.SelectedValue = DateTime.Now.Year
                    drpVPreviousMonth.SelectedValue = Date.Today.Month - 1
                End If
            End If
            CheckSecurity()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub



    Protected Sub btnIncrease_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIncrease.Click
        lblError.Text = ""
        UpdatesessionWhenMovethepage()

        If grdAgencyTarget.Rows.Count = 0 Then
            Exit Sub
        End If
        Dim objInputXml, objtempXml, objAgencyXml As New XmlDocument
        Dim objOutputXml As New XmlDocument
        '  Dim objbzAgencyTargetSave As New AAMS.bizSales.bzAgencyTarget
        Dim TargetValue As System.Int32
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        TargetValue = 0
        Try

            If Session("FinalSesseionXML") IsNot Nothing Then
                objAgencyXml.LoadXml(Session("FinalSesseionXML").ToString)
                For Each objnode As XmlNode In objAgencyXml.DocumentElement.SelectNodes("TARGET")

                    If objnode.Attributes("SEGSTARGET").Value.Trim.Length > 0 Then

                        If rdSummaryOption1.Checked = True Then
                            objnode.Attributes("SEGSTARGET").Value = CInt(objnode.Attributes("SEGSTARGET").Value + (objnode.Attributes("SEGSTARGET").Value * Val(txtTarget.Text) * 0.01))
                        End If
                        If rdSummaryOption2.Checked = True Then
                            objnode.Attributes("SEGSTARGET").Value = CInt(objnode.Attributes("SEGSTARGET").Value - (objnode.Attributes("SEGSTARGET").Value * Val(txtTarget.Text) * 0.01))
                        End If

                        TargetValue = TargetValue + Val(objnode.Attributes("SEGSTARGET").Value)
                    End If
                Next

                '  objAgencyXml.DocumentElement.SelectSingleNode("TOTAL").InnerText = TargetValue
                objAgencyXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("SEGSTARGET").Value = TargetValue


                objXmlReader = New XmlNodeReader(objAgencyXml)
                ds.ReadXml(objXmlReader)
                Session("AgencyTargetDataSource") = ds.Tables("TARGET")
                If Not Session("AgencySearchTargetXML") Is Nothing Then
                    Session("AgencySearchTargetXML") = objAgencyXml.OuterXml
                End If
                Session("FinalSesseionXML") = objAgencyXml.OuterXml
                hdTotal.Value = objAgencyXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("SEGSTARGET").Value 'objAgencyXml.DocumentElement.SelectSingleNode("TOTAL").InnerText
                txtTotalTarget.Text = hdTotal.Value
                Sorting()
                BindControlsForNavigation(grdAgencyTarget.PageCount)

            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub BtnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Try
            Response.Redirect(Request.Url.ToString, False)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If grdAgencyTarget.Rows.Count = 0 Then
            lblError.Text = "There is not any data for saving."
            Exit Sub
        End If
        Dim objAgencyXml As New XmlDocument
        Dim objInputXml As New XmlDocument
        'Dim objNodeClone, objXmlNodeClone As XmlNode
        Dim objOutputXml As New XmlDocument
        Dim objbzAgencyTargetSave As New AAMS.bizSales.bzAgencyTarget

        Try

            '@ Start of code  Update The Data for Changed Target In current Window
            UpdatesessionWhenMovethepage()
            '@ End of code for Update The Data for Changed Target In current Window
            If Session("FinalSesseionXML") IsNot Nothing Then
                objAgencyXml.LoadXml(Session("FinalSesseionXML").ToString)

                'objInputXml.LoadXml("<TA_AGENCYTARGET_OUTPUT><TARGET LCODE='' LoginId='' YEAR='' MONTH='' AgencyName='' OfficeId='' Address='' SalesPersonId='' SalesManName=''  SEGSTARGET='' /></TA_AGENCYTARGET_OUTPUT>")
                'objNodeClone = objInputXml.DocumentElement.SelectSingleNode("TARGET")
                'objXmlNodeClone = objNodeClone.CloneNode(True)


                Dim hdMonth, hdYear As HiddenField
                hdMonth = CType(grdAgencyTarget.Rows(0).FindControl("hdMonth"), HiddenField)
                hdYear = CType(grdAgencyTarget.Rows(0).FindControl("hdYear"), HiddenField)



                For Each objnode As XmlNode In objAgencyXml.DocumentElement.SelectNodes("TARGET")
                    objnode.Attributes("YEAR").Value = hdYear.Value
                    objnode.Attributes("MONTH").Value = hdMonth.Value

                    'objXmlNodeClone.Attributes("LCODE").Value = objXmlNodeClone.Attributes("LCODE").Value
                    'objXmlNodeClone.Attributes("LoginId").Value = objXmlNodeClone.Attributes("LoginId").Value
                    'objXmlNodeClone.Attributes("YEAR").Value = objXmlNodeClone.Attributes("YEAR").Value
                    'objXmlNodeClone.Attributes("MONTH").Value = objXmlNodeClone.Attributes("MONTH").Value

                    'objXmlNodeClone.Attributes("AgencyName").Value = objXmlNodeClone.Attributes("AgencyName").Value
                    'objXmlNodeClone.Attributes("OfficeId").Value = objXmlNodeClone.Attributes("OfficeId").Value
                    'objXmlNodeClone.Attributes("Address").Value = objXmlNodeClone.Attributes("Address").Value
                    'objXmlNodeClone.Attributes("SalesPersonId").Value = objXmlNodeClone.Attributes("SalesPersonId").Value
                    'objXmlNodeClone.Attributes("SalesManName").Value = objXmlNodeClone.Attributes("SalesManName").Value
                    'objXmlNodeClone.Attributes("OffiTargetceId").Value = objXmlNodeClone.Attributes("SEGSTARGET").Value

                    'objInputXml.DocumentElement.AppendChild(objXmlNodeClone)
                    'objXmlNodeClone = objNodeClone.CloneNode(True)

                Next

                '@ start of New code Added              

                '@ Start For Previous Month User can't change the data
                Dim BlnForPrevMonth As Boolean = False
                If Val(Now.Year) >= Val(hdYear.Value) Then
                    If Val(Now.Year) = Val(hdYear.Value) Then
                        If Val(Now.Month) > Val(hdMonth.Value) Then
                            BlnForPrevMonth = True
                        End If
                    Else
                         BlnForPrevMonth = True
                    End If
                End If

                Dim BlnForNextMonth As Boolean = False
                If Val(Now.Year) >= Val(hdYear.Value) Then
                    If Val(Now.Year) = Val(hdYear.Value) Then
                        If Val(Now.Month) < Val(hdMonth.Value) Then
                            BlnForNextMonth = True
                        End If
                    Else
                        BlnForNextMonth = True
                    End If
                End If

                Dim BlnForCurrentMonth As Boolean = False

                If Val(Now.Month) = Val(hdMonth.Value) AndAlso Val(Now.Year) = Val(hdYear.Value) Then
                    BlnForCurrentMonth = True
                End If

                If BlnForCurrentMonth = True Then ' Check only for current Month
                    '@ if Lode Exist in current page then
                    For Rowno As Integer = 0 To grdAgencyTarget.Rows.Count - 1
                        Dim strlGridcode As String = CType(grdAgencyTarget.Rows(Rowno).FindControl("hdlcode"), HiddenField).Value
                        Dim strMonth As String = hdMonth.Value
                        Dim strYear As String = hdYear.Value
                        If objAgencyXml.DocumentElement.SelectNodes("TARGET[@LCODE='" + strlGridcode + "' and @YEAR='" + strYear + "' and @MONTH='" + strMonth + "'  ]").Count > 0 Then
                            Dim objnode As XmlNode = objAgencyXml.DocumentElement.SelectSingleNode("TARGET[@LCODE='" + strlGridcode + "' and @YEAR='" + strYear + "' and @MONTH='" + strMonth + "'  ]")

                            If objnode.Attributes("M_CHK_VT").Value.ToUpper = "TRUE" Then ''This upper part of if added by Tapan Nath
                                If Val(objnode.Attributes("VISITCOUNT").Value) > 0 Then
                                    If Val(objnode.Attributes("VISITTARGET").Value) < Val(objnode.Attributes("VISITCOUNT").Value) Then
                                        If (objnode.Attributes("OFFICEID").Value.Trim.Length) > 0 Then
                                            lblError.Text = "Visit Plan should be equal to or Greater than the objective visit target  for the agency [ " + objnode.Attributes("AGENCYNAME").Value + " ] and LCode [  " + strlGridcode.Trim + " ]"
                                            Sorting()
                                            Exit Sub
                                        End If

                                    End If
                                End If
                            End If
                        End If
                    Next
                    '@ if Lode Exist in next page then
                    For Each objnode As XmlNode In objAgencyXml.DocumentElement.SelectNodes("TARGET")
                        Dim strlSessionLcode As String = objnode.Attributes("LCODE").Value
                        Dim blnNotFoundinCurrentPage As Boolean = False
                        For Rowno As Integer = 0 To grdAgencyTarget.Rows.Count - 1
                            Dim strlGridcode As String = CType(grdAgencyTarget.Rows(Rowno).FindControl("hdlcode"), HiddenField).Value
                            Dim strMonth As String = hdMonth.Value
                            Dim strYear As String = hdYear.Value
                            If strlSessionLcode.Trim = strlGridcode.Trim Then
                                blnNotFoundinCurrentPage = True
                                Exit For
                            End If
                        Next Rowno
                        If blnNotFoundinCurrentPage = False Then
                            If objnode.Attributes("M_CHK_VT").Value.ToUpper = "TRUE" Then ''This upper part of if added by Tapan Nath
                                If Val(objnode.Attributes("VISITCOUNT").Value) > 0 Then
                                    If Val(objnode.Attributes("VISITTARGET").Value) < Val(objnode.Attributes("VISITCOUNT").Value) Then
                                        If (objnode.Attributes("OFFICEID").Value.Trim.Length) > 0 Then
                                            lblError.Text = "Visit Plan should be equal to or Greater than the objective visit target  for the agency [ " + objnode.Attributes("AGENCYNAME").Value + " ] and  LCode [ " + strlSessionLcode.Trim + " ]"
                                            Sorting()
                                            Exit Sub
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Next
                    '@ end of new code added

                End If
               
                '  objInputXml.DocumentElement.RemoveChild(objInputXml.DocumentElement.SelectSingleNode("TARGET[@LCODE='']"))

                objOutputXml = objbzAgencyTargetSave.Update(objAgencyXml)
                Try
                    objAgencyXml.Save("c:\TargetInputSave.xml")
                Catch ex As Exception

                End Try

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    AgencyTargetSearch(hdYear.Value, hdMonth.Value)
                    lblError.Text = objeAAMSMessage.messUpdate '
                    CheckSecurity()
                    HdPrevVisitMonth.Value = ""
                    HdPrevVisitYear.Value = ""
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            Else
                ' lblError.Text = ""
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            ' System.Threading.Thread.Sleep(10000)
            Session("AgencyTargetDataSource") = Nothing
            Session("AgencySearchTargetXML") = Nothing
            Session("FinalSesseionXML") = Nothing

            AgencyTargetSearch()
            HdPrevVisitMonth.Value = ""
            HdPrevVisitYear.Value = ""

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub


    Private Sub AgencyTargetSearch()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objTarget As New AAMS.bizSales.bzAgencyTarget
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            Dim objSecurityXml As New XmlDocument
            lblError.Text = ""

            objInputXml.LoadXml("<TA_AGENCYTARGET_INPUT><CITY_ID /> <RESP_1A></RESP_1A> <MONTH></MONTH> <YEAR></YEAR> <VISITDONE></VISITDONE> <LIMITED_TO_AOFFICE /> <LIMITED_TO_REGION></LIMITED_TO_REGION> <LIMITED_TO_OWNAAGENCY /> <EMPLOYEEID></EMPLOYEEID> <PTARGET></PTARGET><PVISIT></PVISIT><SEARCHTYPE></SEARCHTYPE><AOFFICE></AOFFICE><REGION></REGION><CITY></CITY><ACATEGORY></ACATEGORY><AGENCY_NAME></AGENCY_NAME><LCODE></LCODE><CHAIN_CODE></CHAIN_CODE><OFFICEID></OFFICEID><WHOLEGROUP></WHOLEGROUP><COUNTRY></COUNTRY><VISIT_CATEGORY></VISIT_CATEGORY></TA_AGENCYTARGET_INPUT>")

            '<AGENCY_NAME></AGENCY_NAME><LCODE></LCODE><OFFICEID></OFFICEID><WHOLEGROUP></WHOLEGROUP><COUNTRY></COUNTRY><VISIT_CATEGORY></VISIT_CATEGORY>
            objInputXml.DocumentElement.SelectSingleNode("SEARCHTYPE").InnerText = "SEARCH"
            If drpTarCity.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("CITY_ID").InnerText = drpTarCity.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = drpTarCity.SelectedItem.Text
            End If
            objInputXml.DocumentElement.SelectSingleNode("YEAR").InnerText = drpYear.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("MONTH").InnerText = drpMonth.SelectedValue
            '  objInputXml.DocumentElement.SelectSingleNode("VISITDONE").InnerText = drpMonth.SelectedValue

            If Request("drpSalesPerson") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = Request("drpSalesPerson") '
            Else
                objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = drpSalesPerson.SelectedValue
            End If

            If (drpTarAoffice.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = drpTarAoffice.SelectedItem.Text
            End If

            If (drpTarRegion.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = drpTarRegion.SelectedItem.Text
            End If

            'If (DlstAgencyCateg.SelectedIndex <> 0) Then
            '    objInputXml.DocumentElement.SelectSingleNode("ACATEGORY").InnerText = DlstAgencyCateg.SelectedValue
            'End If
            Dim strActg As String = ""
            For Each Item As ListItem In ChkLstAgencyCategory.Items
                If Item.Selected = True Then
                    If strActg.Trim.Length = 0 Then
                        strActg = Item.Text
                    Else
                        strActg = strActg + "," + Item.Text
                    End If
                End If
            Next

            If (strActg.Trim.Length > 0) Then
                objInputXml.DocumentElement.SelectSingleNode("ACATEGORY").InnerText = strActg
            End If



            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = ""

            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectNodes("LIMITED_TO_AOFFICE").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
                End If

                If (objSecurityXml.DocumentElement.SelectNodes("LIMITED_TO_REGION").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
                End If
                Dim objEmpXml As New XmlDocument
                Dim UserId As String
                objEmpXml.LoadXml(Session("Security"))
                UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                If (objSecurityXml.DocumentElement.SelectNodes("LIMITED_TO_OWNAAGENCY").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText) = "True" Then
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = UserId
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = 0
                End If

            End If
            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If
            objInputXml.DocumentElement.SelectSingleNode("PTARGET").InnerText = "FALSE"
            objInputXml.DocumentElement.SelectSingleNode("PVISIT").InnerText = "FALSE"

            objInputXml.DocumentElement.SelectSingleNode("AGENCY_NAME").InnerText = txtAgencyName.Text
            objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = txtLcode.Text
            'objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = txtChainCode.Text
            objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerText = txtOfficeID.Text
            If chkGroupProductivity.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "True"
            Else
                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "False"
            End If
            If drpTarCountry.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("COUNTRY").InnerText = drpTarCountry.SelectedItem.Text
            End If

            'objInputXml.DocumentElement.SelectSingleNode("VISIT_CATEGORY").InnerText = "" ''Work latter

            Dim strVisitCat As String = ""
            For Each Item As ListItem In ChkLstVisitPlanType.Items
                If Item.Selected = True Then
                    If strVisitCat.Trim.Length = 0 Then
                        strVisitCat = Item.Value
                    Else
                        strVisitCat = strVisitCat + "," + Item.Value
                    End If
                End If
            Next

            If (strVisitCat.Trim.Length > 0) Then
                objInputXml.DocumentElement.SelectSingleNode("VISIT_CATEGORY").InnerText = strVisitCat
            End If


            objOutputXml = objTarget.Search(objInputXml)
            ' objOutputXml.Load("C:\ATargetOutXml.xml")
            Try
                objOutputXml.Save("c:\ATargetOutXml.xml")
                objInputXml.Save("c:\ATargetInputXml.xml")
            Catch ex As Exception

            End Try
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                ViewState("PrevSearching") = objInputXml.OuterXml

                hdTotal.Value = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("SEGSTARGET").Value  'objOutputXml.DocumentElement.SelectSingleNode("TOTAL").InnerText
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                Session("AgencyTargetDataSource") = ds.Tables("TARGET")
                Session("AgencySearchTargetXML") = objOutputXml.OuterXml
                Session("FinalSesseionXML") = objOutputXml.OuterXml

                grdAgencyTarget.DataSource = ds.Tables("TARGET")
                ddlPageNumber.SelectedValue = 1
                grdAgencyTarget.PageIndex = 0
                txtRecordCount.Text = ds.Tables("TARGET").Rows.Count.ToString()
                txtTotalTarget.Text = hdTotal.Value
                Sorting()
                BindControlsForNavigation(grdAgencyTarget.PageCount)
                pnlPaging.Visible = True
                chbPrevious.Checked = False
                ChkPrevVisPlan.Checked = False
            Else
                grdAgencyTarget.DataSource = Nothing
                grdAgencyTarget.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
                chbPrevious.Checked = False
                ChkPrevVisPlan.Checked = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub AgencyTargetSearch(ByVal StrYear As String, ByVal StrMonth As String)
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objTarget As New AAMS.bizSales.bzAgencyTarget
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            Dim objSecurityXml As New XmlDocument
            lblError.Text = ""


            'objInputXml.LoadXml("<TA_AGENCYTARGET_INPUT><CITY_ID /> <RESP_1A></RESP_1A> <MONTH></MONTH> <YEAR></YEAR> <VISITDONE></VISITDONE> <LIMITED_TO_AOFFICE /> <LIMITED_TO_REGION></LIMITED_TO_REGION> <LIMITED_TO_OWNAAGENCY /> <EMPLOYEEID></EMPLOYEEID> <PTARGET></PTARGET><PVISIT></PVISIT><SEARCHTYPE></SEARCHTYPE><AOFFICE></AOFFICE><REGION></REGION><CITY></CITY><ACATEGORY></ACATEGORY></TA_AGENCYTARGET_INPUT>")
            objInputXml.LoadXml("<TA_AGENCYTARGET_INPUT><CITY_ID /> <RESP_1A></RESP_1A> <MONTH></MONTH> <YEAR></YEAR> <VISITDONE></VISITDONE> <LIMITED_TO_AOFFICE /> <LIMITED_TO_REGION></LIMITED_TO_REGION> <LIMITED_TO_OWNAAGENCY /> <EMPLOYEEID></EMPLOYEEID> <PTARGET></PTARGET><PVISIT></PVISIT><SEARCHTYPE></SEARCHTYPE><AOFFICE></AOFFICE><REGION></REGION><CITY></CITY><ACATEGORY></ACATEGORY><AGENCY_NAME></AGENCY_NAME><LCODE></LCODE><CHAIN_CODE></CHAIN_CODE><OFFICEID></OFFICEID><WHOLEGROUP></WHOLEGROUP><COUNTRY></COUNTRY><VISIT_CATEGORY></VISIT_CATEGORY></TA_AGENCYTARGET_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("SEARCHTYPE").InnerText = "SEARCH"

            If drpTarCity.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("CITY_ID").InnerText = drpTarCity.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = drpTarCity.SelectedItem.Text
            End If
            objInputXml.DocumentElement.SelectSingleNode("YEAR").InnerText = StrYear
            objInputXml.DocumentElement.SelectSingleNode("MONTH").InnerText = StrMonth
            '  objInputXml.DocumentElement.SelectSingleNode("VISITDONE").InnerText = drpMonth.SelectedValue

            If Request("drpSalesPerson") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = Request("drpSalesPerson") '
            Else
                objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = drpSalesPerson.SelectedValue
            End If

            If Request("drpTarAoffice") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = Request("drpTarAoffice")
            Else
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = drpTarAoffice.SelectedItem.Text
            End If

            If (drpTarRegion.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = drpTarRegion.SelectedItem.Text
            End If

            Dim strActg As String = ""
            For Each Item As ListItem In ChkLstAgencyCategory.Items
                If Item.Selected = True Then
                    If strActg.Trim.Length = 0 Then
                        strActg = Item.Text
                    Else
                        strActg = strActg + "," + Item.Text
                    End If
                End If
            Next

            If (strActg.Trim.Length > 0) Then
                objInputXml.DocumentElement.SelectSingleNode("ACATEGORY").InnerText = strActg
            End If



            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = ""

            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectNodes("LIMITED_TO_AOFFICE").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
                End If

                If (objSecurityXml.DocumentElement.SelectNodes("LIMITED_TO_REGION").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
                End If
                Dim objEmpXml As New XmlDocument
                Dim UserId As String
                objEmpXml.LoadXml(Session("Security"))
                UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                If (objSecurityXml.DocumentElement.SelectNodes("LIMITED_TO_OWNAAGENCY").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText) = "True" Then
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = UserId
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = 0
                End If

            End If
            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If
            objInputXml.DocumentElement.SelectSingleNode("PTARGET").InnerText = "FALSE"
            objInputXml.DocumentElement.SelectSingleNode("PVISIT").InnerText = "FALSE"

            ''Added
            objInputXml.DocumentElement.SelectSingleNode("AGENCY_NAME").InnerText = txtAgencyName.Text
            objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = txtLcode.Text
            'objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = txtChainCode.Text
            objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerText = txtOfficeID.Text
            If chkGroupProductivity.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "True"
            Else
                objInputXml.DocumentElement.SelectSingleNode("WHOLEGROUP").InnerText = "False"
            End If
            If drpTarCountry.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("COUNTRY").InnerText = drpTarCountry.SelectedItem.Text
            End If

            'objInputXml.DocumentElement.SelectSingleNode("VISIT_CATEGORY").InnerText = "" ''Work latter

            Dim strVisitCat As String = ""
            For Each Item As ListItem In ChkLstVisitPlanType.Items
                If Item.Selected = True Then
                    If strVisitCat.Trim.Length = 0 Then
                        strVisitCat = Item.Value
                    Else
                        strVisitCat = strVisitCat + "," + Item.Value
                    End If
                End If
            Next

            If (strVisitCat.Trim.Length > 0) Then
                objInputXml.DocumentElement.SelectSingleNode("VISIT_CATEGORY").InnerText = strVisitCat
            End If

            ''Added

            objOutputXml = objTarget.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                hdTotal.Value = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("SEGSTARGET").Value 'objOutputXml.DocumentElement.SelectSingleNode("TOTAL").InnerText
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                Session("AgencyTargetDataSource") = ds.Tables("TARGET")
                Session("AgencySearchTargetXML") = objOutputXml.OuterXml
                Session("FinalSesseionXML") = objOutputXml.OuterXml

                grdAgencyTarget.DataSource = ds.Tables("TARGET")
                ddlPageNumber.SelectedValue = 1
                grdAgencyTarget.PageIndex = 0
                txtRecordCount.Text = ds.Tables("TARGET").Rows.Count.ToString()
                txtTotalTarget.Text = hdTotal.Value
                Sorting()
                BindControlsForNavigation(grdAgencyTarget.PageCount)
                pnlPaging.Visible = True
                chbPrevious.Checked = False
                ChkPrevVisPlan.Checked = False
            Else
                grdAgencyTarget.DataSource = Nothing
                grdAgencyTarget.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
                chbPrevious.Checked = False
                ChkPrevVisPlan.Checked = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

   

    Protected Sub btn_Select_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Select.Click
        Try
            lblError.Text = ""
            If grdAgencyTarget.Rows.Count = 0 Then
                lblError.Text = "Please first click on search."
                Exit Sub
            End If

            PreviousAgencyTargetSearch()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Protected Sub btn_VSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_VSelect.Click
        Try
            lblError.Text = ""
            If grdAgencyTarget.Rows.Count = 0 Then
                lblError.Text = "Please first click on search."
                Exit Sub
            End If

            PreviousVisitAgencyTargetSearch()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub BtnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExport.Click
        Try
            '     ExportData()
            If grdAgencyTarget.Rows.Count > 0 Then
                If chkCalender.Checked Then
                    PnlLegand.Visible = True
                Else
                    PnlLegand.Visible = False
                End If
                PnlShowUnhideColumns.Visible = True
            End If


        Catch ex As Exception

        End Try
    End Sub

    Public Sub fillSalesName()
        drpSalesPerson.Items.Clear()
        Dim objSales As New AAMS.bizTravelAgency.bzAgencyTarget
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objEmpXml As New XmlDocument
        Dim objInputXml, objOutputXml As New XmlDocument
        Try

            'Dim UserId As String
            'objEmpXml.LoadXml(Session("Security"))
            'UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
            objInputXml.LoadXml("<PR_AGENCYEMPLIST_INPUT><City_Id></City_Id></PR_AGENCYEMPLIST_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("City_Id").InnerText = drpTarCity.SelectedValue
            objOutputXml = objSales.GetAgencyEmpList(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                drpSalesPerson.DataSource = ds.Tables("TARGET")
                drpSalesPerson.DataTextField = "SalesManName"
                drpSalesPerson.DataValueField = "SalesManId"
                drpSalesPerson.DataBind()
            End If
            drpSalesPerson.Items.Insert(0, New ListItem("--All--", ""))


            ''@ Code For Selection of Default User as a 1A Responsibility

            If Session("Security") IsNot Nothing Then
                Dim UserId As String = ""
                objEmpXml.LoadXml(Session("Security"))
                UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                Dim li As ListItem
                li = drpSalesPerson.Items.FindByValue(UserId)
                If li IsNot Nothing Then
                    drpSalesPerson.SelectedValue = li.Value
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub

    Private Sub BindAll()

        ' Dim objSales As New AAMS.bizSales.bzAgencyTarget
        drpYear.Items.Clear()
        drpPreviousYear.Items.Clear()
        drpVPreviousYear.Items.Clear()
        Dim i, j As Integer
        Try
            i = 0

            objeAAMS.BindDropDown(drpTarCity, "CITY", True, 3)

            For j = DateTime.Now.Year + 1 To 1990 Step -1
                drpYear.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                i += 1
            Next
            i = 0
            For j = DateTime.Now.Year To 1990 Step -1
                drpPreviousYear.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                drpVPreviousYear.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                i += 1
            Next

            drpYear.SelectedValue = DateTime.Now.Year
            drpMonth.SelectedValue = Date.Today.Month

            If drpMonth.SelectedValue = 1 Then
                drpPreviousMonth.SelectedValue = 12
                drpPreviousYear.SelectedValue = DateTime.Now.Year - 1
                drpVPreviousMonth.SelectedValue = 12
                drpVPreviousYear.SelectedValue = DateTime.Now.Year - 1
            Else
                drpPreviousYear.SelectedValue = DateTime.Now.Year
                drpPreviousMonth.SelectedValue = Date.Today.Month - 1
                drpVPreviousYear.SelectedValue = DateTime.Now.Year
                drpVPreviousMonth.SelectedValue = Date.Today.Month - 1
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Protected Sub drpTarCity_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpTarCity.SelectedIndexChanged
        Try
            fillSalesName()
        Catch ex As Exception
        Finally
            If grdAgencyTarget.Rows.Count > 0 Then
                PnlLegand.Visible = True

                PnlShowUnhideColumns.Visible = True
            End If
        End Try


    End Sub

#Region "CheckSecurity()::This method is used for security check."
    Private Sub CheckSecurity()
        Dim strBuilder As New StringBuilder
        Dim objSecurityXml As New XmlDocument
        ' Dim ObjXmlNodeSales_Default As XmlNode
        Try
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Target']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Target']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                    If strBuilder(2) = "0" Then  ' I have to Ask
                        BtnSave.Enabled = False
                    Else
                        BtnSave.Enabled = True
                    End If
                    If strBuilder(4) = "0" Then
                        BtnExport.Enabled = False
                    End If
                Else
                    BtnSave.Enabled = False
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If



            '@ Start of Right for seenh the BIDT/MIDt/Daily Booking data
            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        ChkBIDT.Visible = False
                        ChkBIDT.Checked = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DailyBookings']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DailyBookings']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        ChkPastMotive.Visible = False
                        ChkPastMotive.Checked = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If


            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        ChkMIDT.Visible = False
                        ChkMIDT.Checked = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            '@ End  of Right for seenh the BIDT/MIDt/Daily Booking data





        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Private Sub Sorting()
        Dim dv As DataView
        Try
     
            If Session("AgencyTargetDataSource") IsNot Nothing Then
                ' dv = New DataView(Session("AgencyTargetDataSource"))

                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "AGENCYNAME"
                    ViewState("Direction") = "desc"
                End If

                Dim dt As DataTable
                dt = CType(Session("AgencyTargetDataSource"), DataTable)

                Dim dt1 As New DataTable

                For i As Integer = 0 To dt.Columns.Count - 1
                    Dim current_col, new_col As New DataColumn
                    current_col = dt.Columns(i)
                    new_col = New DataColumn(current_col.ColumnName, Type.GetType(current_col.DataType.FullName))
                    new_col.AllowDBNull = True
                    dt1.Columns.Add(new_col)
                Next

                ' MONTH="2" YEAR="2011" LCODE="2" CHAINCODE="2" AGENCYNAME="abcs" 
                'ADDRESS="rt" OFFICEID="delbr0006" CITY="delhi" GROUP_CATG="A" 
                'RESP1A_ID="3" RESP1A_NAME="Rana" EMPLOYEEID="22" AVGBIDT="55"
                ' AVGMIDT="55" PASTMOTIVE="55" BUSINESSCCOMMIT="56" MINIUMSEGS="5" 
                'VISITCOUNT="4" SEGSTARGET="100" VISITTARGET="1" PVISITDONE="" UVISITDONE=""
                ' LOGDATE="" D1="" D2="" D3="" D4="" D5="" D6="" D7="" D8="2" D9="" D10="" D11="" D12="" D13="" D14="" D15="" D16="" D17="" D18="" D19="" D20="" D21="" D22="1" D23="" D24="" D25="" D26="" D27="" D28="" D29="" D30="" D31="" PLANNED_DAYS="8|22"

                dt1.Columns("LCODE").DataType = Type.GetType("System.Int32")
                dt1.Columns("CHAINCODE").DataType = Type.GetType("System.Int32")
                dt1.Columns("SEGSTARGET").DataType = Type.GetType("System.Int32")
                dt1.Columns("AVGBIDT").DataType = Type.GetType("System.Int32")
                dt1.Columns("AVGMIDT").DataType = Type.GetType("System.Int32")
                dt1.Columns("PASTMOTIVE").DataType = Type.GetType("System.Int32")
                dt1.Columns("BUSINESSCCOMMIT").DataType = Type.GetType("System.Int32")
                dt1.Columns("MINIUMSEGS").DataType = Type.GetType("System.Int32")

                dt1.Columns("VISITCOUNT").DataType = Type.GetType("System.Int32")
                dt1.Columns("VISITTARGET").DataType = Type.GetType("System.Int32")
                dt1.Columns("PVISITDONE").DataType = Type.GetType("System.Int32")
                dt1.Columns("UVISITDONE").DataType = Type.GetType("System.Int32")


                Dim i1 As Integer
                For i1 = 0 To dt.Rows.Count - 1
                    Dim row As DataRow = dt1.NewRow()
                    'row.ItemArray = dt.Rows(i1).ItemArray

                    '  <TARGET MONTH="5" YEAR="2011" LCODE="31189" CHAINCODE="16" AGENCYNAME="Acme"
                    ' ADDRESS="Plot No-63,Udyog Vihar" OFFICEID="DELVS34BB" CITY="Gurgaon"
                    ' GROUP_CATG="DE" RESP1A_ID="202815" RESP1A_NAME="Vaibhav Sharma" 
                    'EMPLOYEEID="24" AVGBIDT="" AVGMIDT="" PASTMOTIVE=""
                    ' BUSINESSCCOMMIT="" MINIUMSEGS="" VISITCOUNT="25"
                    ' SEGSTARGET="" VISITTARGET="" PVISITDONE=""
                    ' UVISITDONE="" LOGDATE="" D1="" D2="" D3="" D4="" D5="" D6="" D7="" D8="" D9="" D10="" D11="" D12="" D13="" D14="" D15="" D16="" D17="" D18="" D19="" D20="" D21="" D22="" D23="" D24="" D25="" D26="" D27="" D28="" D29="" D30="" D31="" PLANNED_DAYS="" />

                    row("MONTH") = dt.Rows(i1)("MONTH")
                    row("YEAR") = dt.Rows(i1)("YEAR")
                    If dt.Rows(i1)("LCODE").ToString.Trim.Length > 0 Then
                        row("LCODE") = dt.Rows(i1)("LCODE")
                    End If
                    If dt.Rows(i1)("CHAINCODE").ToString.Trim.Length > 0 Then
                        row("CHAINCODE") = dt.Rows(i1)("CHAINCODE")
                    End If
                    row("AGENCYNAME") = dt.Rows(i1)("AGENCYNAME")
                    row("ADDRESS") = dt.Rows(i1)("ADDRESS")
                    row("OFFICEID") = dt.Rows(i1)("OFFICEID")
                    row("CITY") = dt.Rows(i1)("CITY")

                    row("GROUP_CATG") = dt.Rows(i1)("GROUP_CATG")
                    row("RESP1A_ID") = dt.Rows(i1)("RESP1A_ID")
                    row("RESP1A_NAME") = dt.Rows(i1)("RESP1A_NAME")
                    row("EMPLOYEEID") = dt.Rows(i1)("EMPLOYEEID")



                    If dt.Rows(i1)("AVGBIDT").ToString.Trim.Length > 0 Then
                        row("AVGBIDT") = dt.Rows(i1)("AVGBIDT")
                    End If
                    If dt.Rows(i1)("AVGMIDT").ToString.Trim.Length > 0 Then
                        row("AVGMIDT") = dt.Rows(i1)("AVGMIDT")
                    End If

                    If dt.Rows(i1)("PASTMOTIVE").ToString.Trim.Length > 0 Then
                        row("PASTMOTIVE") = dt.Rows(i1)("PASTMOTIVE")
                    End If

                    If dt.Rows(i1)("BUSINESSCCOMMIT").ToString.Trim.Length > 0 Then
                        row("BUSINESSCCOMMIT") = dt.Rows(i1)("BUSINESSCCOMMIT")
                    End If

                    If dt.Rows(i1)("MINIUMSEGS").ToString.Trim.Length > 0 Then
                        row("MINIUMSEGS") = dt.Rows(i1)("MINIUMSEGS")
                    End If

                    If dt.Rows(i1)("VISITCOUNT").ToString.Trim.Length > 0 Then
                        row("VISITCOUNT") = dt.Rows(i1)("VISITCOUNT")
                    End If


                    If dt.Rows(i1)("SEGSTARGET").ToString.Trim.Length > 0 Then
                        row("SEGSTARGET") = dt.Rows(i1)("SEGSTARGET")
                    End If

                    If dt.Rows(i1)("VISITTARGET").ToString.Trim.Length > 0 Then
                        row("VISITTARGET") = dt.Rows(i1)("VISITTARGET")
                    End If
                    If dt.Rows(i1)("PVISITDONE").ToString.Trim.Length > 0 Then
                        row("PVISITDONE") = dt.Rows(i1)("PVISITDONE")
                    End If
                    If dt.Rows(i1)("UVISITDONE").ToString.Trim.Length > 0 Then
                        row("UVISITDONE") = dt.Rows(i1)("UVISITDONE")
                    End If

                    row("LOGDATE") = dt.Rows(i1)("LOGDATE")

                    row("D1") = dt.Rows(i1)("D1")
                    row("D2") = dt.Rows(i1)("D2")
                    row("D3") = dt.Rows(i1)("D3")
                    row("D4") = dt.Rows(i1)("D4")
                    row("D5") = dt.Rows(i1)("D5")
                    row("D6") = dt.Rows(i1)("D6")
                    row("D7") = dt.Rows(i1)("D7")
                    row("D8") = dt.Rows(i1)("D8")
                    row("D9") = dt.Rows(i1)("D9")
                    row("D10") = dt.Rows(i1)("D10")

                    row("D11") = dt.Rows(i1)("D11")
                    row("D12") = dt.Rows(i1)("D12")
                    row("D13") = dt.Rows(i1)("D13")
                    row("D14") = dt.Rows(i1)("D14")
                    row("D15") = dt.Rows(i1)("D15")
                    row("D16") = dt.Rows(i1)("D16")
                    row("D17") = dt.Rows(i1)("D17")
                    row("D18") = dt.Rows(i1)("D18")
                    row("D19") = dt.Rows(i1)("D19")
                    row("D20") = dt.Rows(i1)("D20")

                    row("D21") = dt.Rows(i1)("D21")
                    row("D22") = dt.Rows(i1)("D22")
                    row("D23") = dt.Rows(i1)("D23")
                    row("D24") = dt.Rows(i1)("D24")
                    row("D25") = dt.Rows(i1)("D25")
                    row("D26") = dt.Rows(i1)("D26")
                    row("D27") = dt.Rows(i1)("D27")
                    row("D28") = dt.Rows(i1)("D28")
                    row("D29") = dt.Rows(i1)("D29")
                    row("D30") = dt.Rows(i1)("D30")
                    row("D31") = dt.Rows(i1)("D31")


                    row("PLANNED_DAYS") = dt.Rows(i1)("PLANNED_DAYS")

                    ''Added By Tapan Nath
                    row("COLORCODE") = dt.Rows(i1)("COLORCODE")
                    row("M_CHK_VT") = dt.Rows(i1)("M_CHK_VT")
                    ''Added By Tapan Nath


                    dt1.Rows.Add(row)
                    dt1.AcceptChanges()


                Next
                dt1.AcceptChanges()
                dv = New DataView(dt1)

                dv.Sort = ViewState("SortName").ToString() + " " + ViewState("Direction").ToString()
                grdAgencyTarget.DataSource = dv
                grdAgencyTarget.DataBind()

                Dim TotalWidth As Int64
                TotalWidth = 0
                Dim InvisibleColumnCount As Integer = 0
                For intclmn As Integer = 0 To grdAgencyTarget.Columns.Count - 1
                    If grdAgencyTarget.HeaderRow.Cells(intclmn).Visible = True Then
                        TotalWidth = TotalWidth + grdAgencyTarget.Columns(intclmn).ItemStyle.Width.Value
                    Else
                        InvisibleColumnCount = InvisibleColumnCount + 1
                    End If

                Next


                grdAgencyTarget.Width = TotalWidth
                'LbltdLeft.Width = TotalLeftWidth - (InvisibleColumnCount * 3)
                'LblPlannedDays.Width = TotalLeftWidth - TotalInvisibleWidth
                'LblPlannedDays.Visible = True
                If chkCalender.Checked = True Then
                    PnlLegand.Visible = True
                Else
                    PnlLegand.Visible = False
                End If

                PnlLegand.Width = TotalWidth
                Dim LeftWidth As Int64 = 0
                LeftWidth = (1070 + (9 - InvisibleColumnCount) * 33)

                If TotalWidth > LeftWidth Then
                    LblLeftLegand.Width = TotalWidth - LeftWidth + 190
                End If

                PnlShowUnhideColumns.Visible = True

                SetImageForSorting(grdAgencyTarget)

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub SetImageForSorting(ByVal grd As GridView)
        Dim imgUp As New Image
        imgUp.ImageUrl = "~/Images/Sortup.gif"
        Dim imgDown As New Image
        imgDown.ImageUrl = "~/Images/Sortdown.gif"
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                If ViewState("SortName").ToString().Trim() <> "COLORCODE" Then
                    Dim intcol As Integer = grd.Columns.IndexOf(field)
                    If ViewState("Direction").ToString.ToUpper = "ASC" Then
                        grd.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                    End If
                    If ViewState("Direction").ToString.ToUpper = "DESC" Then
                        grd.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                    End If
                End If
            End If
        Next
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            lblError.Text = ""
            UpdatesessionWhenMovethepage()
            grdAgencyTarget.PageIndex = ddlPageNumber.SelectedValue - 1
            Sorting()
            BindControlsForNavigation(grdAgencyTarget.PageCount)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        UpdatesessionWhenMovethepage()

        If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
            ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
        End If
        lblError.Text = ""
        grdAgencyTarget.PageIndex = ddlPageNumber.SelectedValue - 1
        Sorting()
        BindControlsForNavigation(grdAgencyTarget.PageCount)
    End Sub
    Private Sub BindControlsForNavigation(ByVal CrrentPageNo As String)
        pnlPaging.Visible = True
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
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try

            UpdatesessionWhenMovethepage()
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            lblError.Text = ""
            grdAgencyTarget.PageIndex = ddlPageNumber.SelectedValue - 1
            Sorting()
            BindControlsForNavigation(grdAgencyTarget.PageCount)

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub UpdatesessionWhenMovethepage()
        If grdAgencyTarget.Rows.Count = 0 Then
            Exit Sub
        End If
        Dim objInputXml, objtempXml, objAgencyXml As New XmlDocument
        Dim objOutputXml As New XmlDocument
        Dim objbzAgencyTargetSave As New AAMS.bizSales.bzAgencyTarget
        Dim Rowno As Integer
        Dim TargetValue As System.Int32
        TargetValue = 0
        Try

            If Session("FinalSesseionXML") IsNot Nothing Then
                objAgencyXml.LoadXml(Session("FinalSesseionXML").ToString)
                Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                Dim hdMonth, hdYear As HiddenField

                hdMonth = CType(grdAgencyTarget.Rows(0).FindControl("hdMonth"), HiddenField)
                hdYear = CType(grdAgencyTarget.Rows(0).FindControl("hdYear"), HiddenField)

                For Rowno = 0 To grdAgencyTarget.Rows.Count - 1
                    Dim strlcode As String = CType(grdAgencyTarget.Rows(Rowno).FindControl("hdlcode"), HiddenField).Value
                    Dim strMonth As String = hdMonth.Value
                    Dim strYear As String = hdYear.Value
                    Dim strTarget As String = CType(grdAgencyTarget.Rows(Rowno).FindControl("txtTarget"), TextBox).Text
                    Dim dblTarget As Long = 0
                    If strTarget.Trim.Length = 0 Then
                        dblTarget = 0
                    Else
                        dblTarget = strTarget
                    End If

                    If objAgencyXml.DocumentElement.SelectNodes("TARGET[@LCODE='" + strlcode + "' and @YEAR='" + strYear + "' and @MONTH='" + strMonth + "'  ]").Count > 0 Then

                        If rdSummaryOption1.Checked = True Then
                            If strTarget.Trim.Length = 0 Then
                                objAgencyXml.DocumentElement.SelectSingleNode("TARGET[@LCODE='" + strlcode + "' and @YEAR='" + strYear + "' and @MONTH='" + strMonth + "' ]").Attributes("SEGSTARGET").Value = 0
                            Else
                                objAgencyXml.DocumentElement.SelectSingleNode("TARGET[@LCODE='" + strlcode + "' and @YEAR='" + strYear + "' and @MONTH='" + strMonth + "'  ]").Attributes("SEGSTARGET").Value = (dblTarget)
                            End If
                        Else
                            If strTarget.Trim.Length = 0 Then
                                objAgencyXml.DocumentElement.SelectSingleNode("TARGET[@LCODE='" + strlcode + "' and @YEAR='" + strYear + "' and @MONTH='" + strMonth + "' ]").Attributes("SEGSTARGET").Value = 0
                            Else
                                objAgencyXml.DocumentElement.SelectSingleNode("TARGET[@LCODE='" + strlcode + "' and @YEAR='" + strYear + "' and @MONTH='" + strMonth + "'  ]").Attributes("SEGSTARGET").Value = dblTarget
                            End If
                        End If
                    End If

                Next

                For Each objnode As XmlNode In objAgencyXml.DocumentElement.SelectNodes("TARGET")
                    objnode.Attributes("YEAR").Value = hdYear.Value
                    objnode.Attributes("MONTH").Value = hdMonth.Value
                    If objnode.Attributes("SEGSTARGET").Value.Trim.Length > 0 Then
                        TargetValue = TargetValue + Val(objnode.Attributes("SEGSTARGET").Value)
                    End If
                Next


                ' objAgencyXml.DocumentElement.SelectSingleNode("TOTAL").InnerText = TargetValue
                objAgencyXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("SEGSTARGET").Value = TargetValue

                objXmlReader = New XmlNodeReader(objAgencyXml)
                ds.ReadXml(objXmlReader)
                Session("AgencyTargetDataSource") = ds.Tables("TARGET")
                If Not Session("AgencySearchTargetXML") Is Nothing Then
                    Session("AgencySearchTargetXML") = objAgencyXml.OuterXml
                End If
                Session("FinalSesseionXML") = objAgencyXml.OuterXml
                hdTotal.Value = objAgencyXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("SEGSTARGET").Value 'objAgencyXml.DocumentElement.SelectSingleNode("TOTAL").InnerText
                txtTotalTarget.Text = hdTotal.Value

            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub PreviousAgencyTargetSearch()

        '######################################################################################
        'Start New Code
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objTargetPrevious As New AAMS.bizSales.bzAgencyTarget
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim dv As New DataView
        Try
            If Session("FinalSesseionXML") IsNot Nothing Then
                objInputXml.LoadXml(Session("FinalSesseionXML").ToString)

                For Each objnode As XmlNode In objInputXml.DocumentElement.SelectNodes("TARGET")
                    objnode.Attributes("YEAR").Value = drpPreviousYear.SelectedValue
                    objnode.Attributes("MONTH").Value = drpPreviousMonth.SelectedValue
                Next

                objOutputXml = objTargetPrevious.GetTarget(objInputXml)

                Try
                    objInputXml.Save("c:\PrevTargetInput.xml")
                    objOutputXml.Save("c:\PrevTargetOutput.xml")
                Catch ex As Exception

                End Try

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    Dim hdMonth, hdYear As HiddenField
                    hdMonth = CType(grdAgencyTarget.Rows(0).FindControl("hdMonth"), HiddenField)
                    hdYear = CType(grdAgencyTarget.Rows(0).FindControl("hdYear"), HiddenField)

                    For Each objnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("TARGET")
                        objnode.Attributes("YEAR").Value = hdYear.Value
                        objnode.Attributes("MONTH").Value = hdMonth.Value
                    Next

                    hdTotal.Value = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("SEGSTARGET").Value 'objOutputXml.DocumentElement.SelectSingleNode("TOTAL").InnerText
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    Session("AgencySelectTargetXML") = objOutputXml.OuterXml
                    Session("AgencyTargetDataSource") = ds.Tables("TARGET")
                    Session("FinalSesseionXML") = objOutputXml.OuterXml
                    ddlPageNumber.SelectedValue = 1
                    grdAgencyTarget.PageIndex = 0
                    Sorting()

                    BindControlsForNavigation(grdAgencyTarget.PageCount)
                    txtTotalTarget.Text = hdTotal.Value
                    '   hdTargetList.Value = objOutputXml.OuterXml
                    pnlPaging.Visible = True
                    lblError.Text = ""
                    txtRecordCount.Text = ds.Tables("TARGET").Rows.Count.ToString()
                Else
                    grdAgencyTarget.DataSource = Nothing
                    grdAgencyTarget.DataBind()
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    pnlPaging.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub PreviousVisitAgencyTargetSearch()

        '######################################################################################
        'Start New Code
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objTargetPrevious As New AAMS.bizSales.bzAgencyTarget
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim dv As New DataView
        Try
            If Session("FinalSesseionXML") IsNot Nothing Then
                objInputXml.LoadXml(Session("FinalSesseionXML").ToString)

                For Each objnode As XmlNode In objInputXml.DocumentElement.SelectNodes("TARGET")
                    objnode.Attributes("YEAR").Value = drpVPreviousYear.SelectedValue
                    objnode.Attributes("MONTH").Value = drpVPreviousMonth.SelectedValue
                Next

                objOutputXml = objTargetPrevious.GetVisit(objInputXml)

                Try
                    objInputXml.Save("c:\PrevVisitTargetInput.xml")
                    objOutputXml.Save("c:\PrevVisitTargetOutput.xml")
                Catch ex As Exception

                End Try
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    Dim hdMonth, hdYear As HiddenField
                    hdMonth = CType(grdAgencyTarget.Rows(0).FindControl("hdMonth"), HiddenField)
                    hdYear = CType(grdAgencyTarget.Rows(0).FindControl("hdYear"), HiddenField)

                    For Each objnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("TARGET")
                        objnode.Attributes("YEAR").Value = hdYear.Value
                        objnode.Attributes("MONTH").Value = hdMonth.Value
                    Next

                    hdTotal.Value = objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("SEGSTARGET").Value 'objOutputXml.DocumentElement.SelectSingleNode("TOTAL").InnerText
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    Session("AgencySelectTargetXML") = objOutputXml.OuterXml
                    Session("AgencyTargetDataSource") = ds.Tables("TARGET")
                    Session("FinalSesseionXML") = objOutputXml.OuterXml
                    ddlPageNumber.SelectedValue = 1
                    grdAgencyTarget.PageIndex = 0
                    Sorting()

                    BindControlsForNavigation(grdAgencyTarget.PageCount)
                    txtTotalTarget.Text = hdTotal.Value
                    '   hdTargetList.Value = objOutputXml.OuterXml
                    pnlPaging.Visible = True
                    lblError.Text = ""
                    txtRecordCount.Text = ds.Tables("TARGET").Rows.Count.ToString()

                    HdPrevVisitMonth.Value = drpVPreviousMonth.SelectedValue
                    HdPrevVisitYear.Value = drpVPreviousYear.SelectedValue

                Else
                    grdAgencyTarget.DataSource = Nothing
                    grdAgencyTarget.DataBind()
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    pnlPaging.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdAgencyTarget_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdAgencyTarget.RowDataBound

        Dim objSecurityXml As New XmlDocument
        Dim lnkHistory As New LinkButton
        Dim LnkCalender As New LinkButton
        Dim objInputXml As New XmlDocument

        Try

            '@ Start of  Code For Visible /Invisible Items
            If e.Row.RowType = DataControlRowType.Header Or e.Row.RowType = DataControlRowType.DataRow Or e.Row.RowType = DataControlRowType.Footer Then

                'Dim hdSelectYear, hdSelectMonth As HiddenField
                'hdSelectYear = CType(grdAgencyTarget.Rows(1).FindControl("hdYear"), HiddenField)
                'hdSelectMonth = CType(grdAgencyTarget.Rows(1).FindControl("hdMonth"), HiddenField)
                If Session("FinalSesseionXML") IsNot Nothing Then
                    objInputXml.LoadXml(Session("FinalSesseionXML").ToString)
                    If chkCalender.Checked = True Then
                        e.Row.Cells(20).Visible = True
                        e.Row.Cells(21).Visible = True
                        e.Row.Cells(22).Visible = True
                        e.Row.Cells(23).Visible = True
                        e.Row.Cells(24).Visible = True
                        e.Row.Cells(25).Visible = True
                        e.Row.Cells(26).Visible = True
                        e.Row.Cells(27).Visible = True
                        e.Row.Cells(28).Visible = True
                        e.Row.Cells(29).Visible = True
                        e.Row.Cells(30).Visible = True
                        e.Row.Cells(31).Visible = True
                        e.Row.Cells(32).Visible = True
                        e.Row.Cells(33).Visible = True
                        e.Row.Cells(34).Visible = True
                        e.Row.Cells(35).Visible = True
                        e.Row.Cells(36).Visible = True
                        e.Row.Cells(37).Visible = True
                        e.Row.Cells(38).Visible = True
                        e.Row.Cells(39).Visible = True

                        e.Row.Cells(40).Visible = True
                        e.Row.Cells(41).Visible = True
                        e.Row.Cells(42).Visible = True
                        e.Row.Cells(43).Visible = True
                        e.Row.Cells(44).Visible = True
                        e.Row.Cells(45).Visible = True
                        e.Row.Cells(46).Visible = True
                        e.Row.Cells(47).Visible = True
                        e.Row.Cells(48).Visible = True
                        e.Row.Cells(49).Visible = True

                        e.Row.Cells(50).Visible = True

                    Else
                        e.Row.Cells(20).Visible = False
                        e.Row.Cells(21).Visible = False
                        e.Row.Cells(22).Visible = False
                        e.Row.Cells(23).Visible = False
                        e.Row.Cells(24).Visible = False
                        e.Row.Cells(25).Visible = False
                        e.Row.Cells(26).Visible = False
                        e.Row.Cells(27).Visible = False
                        e.Row.Cells(28).Visible = False
                        e.Row.Cells(29).Visible = False
                        e.Row.Cells(30).Visible = False
                        e.Row.Cells(31).Visible = False
                        e.Row.Cells(32).Visible = False
                        e.Row.Cells(33).Visible = False
                        e.Row.Cells(34).Visible = False
                        e.Row.Cells(35).Visible = False
                        e.Row.Cells(36).Visible = False
                        e.Row.Cells(37).Visible = False
                        e.Row.Cells(38).Visible = False
                        e.Row.Cells(39).Visible = False

                        e.Row.Cells(40).Visible = False
                        e.Row.Cells(41).Visible = False
                        e.Row.Cells(42).Visible = False
                        e.Row.Cells(43).Visible = False
                        e.Row.Cells(44).Visible = False
                        e.Row.Cells(45).Visible = False
                        e.Row.Cells(46).Visible = False
                        e.Row.Cells(47).Visible = False
                        e.Row.Cells(48).Visible = False
                        e.Row.Cells(49).Visible = False

                        e.Row.Cells(50).Visible = False

                    End If


                    If DateTime.DaysInMonth(objInputXml.DocumentElement.SelectSingleNode("TARGET").Attributes("YEAR").Value, objInputXml.DocumentElement.SelectSingleNode("TARGET").Attributes("MONTH").Value) = 30 Then
                        e.Row.Cells(50).Visible = False
                    End If
                    If DateTime.DaysInMonth(objInputXml.DocumentElement.SelectSingleNode("TARGET").Attributes("YEAR").Value, objInputXml.DocumentElement.SelectSingleNode("TARGET").Attributes("MONTH").Value) = 29 Then
                        e.Row.Cells(49).Visible = False
                        e.Row.Cells(50).Visible = False

                    End If
                    If DateTime.DaysInMonth(objInputXml.DocumentElement.SelectSingleNode("TARGET").Attributes("YEAR").Value, objInputXml.DocumentElement.SelectSingleNode("TARGET").Attributes("MONTH").Value) = 28 Then
                        e.Row.Cells(48).Visible = False
                        e.Row.Cells(49).Visible = False
                        e.Row.Cells(50).Visible = False
                    End If


                    If e.Row.RowType = DataControlRowType.Footer Then
                        ' PAGE_TOTAL AVGBIDT ='' AVGMIDT='' PASTMOTIVE='' SEGSTARGET='' VISITCOUNT='' VISITTARGET ='' PVISITDONE='' UVISITDONE='' MINIUMSEGS=''/>
                        e.Row.Cells(10).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("SEGSTARGET").Value
                        e.Row.Cells(11).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("VISITCOUNT").Value
                        e.Row.Cells(12).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("VISITTARGET").Value
                        e.Row.Cells(13).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("PVISITDONE").Value
                        e.Row.Cells(14).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("UVISITDONE").Value
                        e.Row.Cells(15).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("AVGMIDT").Value
                        e.Row.Cells(16).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("AVGBIDT").Value

                        e.Row.Cells(17).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("PASTMOTIVE").Value
                        e.Row.Cells(18).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("MINIUMSEGS").Value

                        e.Row.Cells(20).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D1").Value
                        e.Row.Cells(21).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D2").Value
                        e.Row.Cells(22).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D3").Value
                        e.Row.Cells(23).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D4").Value
                        e.Row.Cells(24).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D5").Value
                        e.Row.Cells(25).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D6").Value
                        e.Row.Cells(26).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D7").Value
                        e.Row.Cells(27).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D8").Value
                        e.Row.Cells(28).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D9").Value
                        e.Row.Cells(29).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D10").Value
                        e.Row.Cells(30).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D11").Value
                        e.Row.Cells(31).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D12").Value
                        e.Row.Cells(32).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D13").Value
                        e.Row.Cells(33).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D14").Value
                        e.Row.Cells(34).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D15").Value

                        e.Row.Cells(35).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D16").Value
                        e.Row.Cells(36).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D17").Value
                        e.Row.Cells(37).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D18").Value
                        e.Row.Cells(38).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D19").Value
                        e.Row.Cells(39).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D20").Value

                        e.Row.Cells(40).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D21").Value
                        e.Row.Cells(41).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D22").Value
                        e.Row.Cells(42).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D23").Value
                        e.Row.Cells(43).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D24").Value
                        e.Row.Cells(44).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D25").Value
                        e.Row.Cells(45).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D26").Value
                        e.Row.Cells(46).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D27").Value
                        e.Row.Cells(47).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D28").Value
                        e.Row.Cells(48).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D29").Value
                        e.Row.Cells(49).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D30").Value

                        e.Row.Cells(50).Text = objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("D31").Value



                    End If

                End If



                If ChkLcode.Checked = True Then
                    e.Row.Cells(2).Visible = True
                Else
                    e.Row.Cells(2).Visible = False
                End If
                If ChkChaincode.Checked = True Then
                    e.Row.Cells(3).Visible = True
                Else
                    e.Row.Cells(3).Visible = False
                End If

                If chkOfficeID.Checked = True Then
                    e.Row.Cells(5).Visible = True
                Else
                    e.Row.Cells(5).Visible = False
                End If
                If chkAddress.Checked = True Then
                    e.Row.Cells(6).Visible = True
                Else
                    e.Row.Cells(6).Visible = False
                End If


                If ChkCity.Checked = True Then
                    e.Row.Cells(7).Visible = True
                Else
                    e.Row.Cells(7).Visible = False
                End If

                If ChkCateg.Checked = True Then
                    e.Row.Cells(8).Visible = True
                Else
                    e.Row.Cells(8).Visible = False
                End If

                If chkResp1a.Checked = True Then
                    e.Row.Cells(9).Visible = True
                Else
                    e.Row.Cells(9).Visible = False
                End If

                If chkPVisit.Checked = True Then
                    e.Row.Cells(13).Visible = True
                Else
                    e.Row.Cells(13).Visible = False
                End If
                If chkUPVisit.Checked = True Then
                    e.Row.Cells(14).Visible = True
                Else
                    e.Row.Cells(14).Visible = False
                End If




                If ChkMIDT.Checked = True Then
                    e.Row.Cells(15).Visible = True
                Else
                    e.Row.Cells(15).Visible = False
                End If
                If ChkBIDT.Checked = True Then
                    e.Row.Cells(16).Visible = True
                Else
                    e.Row.Cells(16).Visible = False
                End If

              

               

                If ChkPastMotive.Checked = True Then
                    e.Row.Cells(17).Visible = True
                Else
                    e.Row.Cells(17).Visible = False
                End If

                If ChkMinSeg.Checked = True Then
                    e.Row.Cells(18).Visible = True
                Else
                    e.Row.Cells(18).Visible = False
                End If

                If ChkBCommit.Checked = True Then
                    e.Row.Cells(19).Visible = True
                Else
                    e.Row.Cells(19).Visible = False
                End If

              

            End If


            '@ End of  Code For Visible /Invisible Items

            If e.Row.RowType = DataControlRowType.DataRow Then

           

                Dim txtTarget As TextBox = CType(e.Row.FindControl("txtTarget"), TextBox)
                txtTarget.Enabled = True


                Dim hdLcode, hdSalesId As HiddenField
                Dim HdMaxVisit, hdYear, hdMonth, HdUserDefinedVisit, HdUserDefinedVisitDays As HiddenField
                hdLcode = CType(e.Row.FindControl("hdlcode"), HiddenField)
                hdSalesId = CType(e.Row.FindControl("hdSalesId"), HiddenField)


                hdYear = CType(e.Row.FindControl("hdYear"), HiddenField)
                hdMonth = CType(e.Row.FindControl("hdMonth"), HiddenField)
                HdUserDefinedVisit = CType(e.Row.FindControl("HdUserDefinedVisit"), HiddenField)
                HdMaxVisit = CType(e.Row.FindControl("HdMaxVisit"), HiddenField)
                HdUserDefinedVisitDays = CType(e.Row.FindControl("HdUserDefinedVisitDays"), HiddenField)

                'Added By Tapan Nath
                Dim hdchkVisitTarget As HiddenField
                hdchkVisitTarget = CType(e.Row.FindControl("hdchkVisitTarget"), HiddenField)
                'Added By Tapan Nath

                '@ Start of  Making Color For Calender Date
                '1 PLAN DAY BLUE
                '2 PLAN DAY VISITED GREEN
                '3 UNPLAN DAY VISITED YELLOW
                '4 PLAN DAY NOT VISITED RED

                For i As Integer = 1 To 31
                    Dim StrLblDate As String = "LblDate" + i.ToString
                    Dim lblDate As Label = CType(e.Row.FindControl(StrLblDate), Label)

                    If lblDate IsNot Nothing Then
                        Try
                            Dim CurrentDate As New DateTime(CInt(Val(hdYear.Value)), CInt(Val(hdMonth.Value)), i)
                            If CurrentDate.DayOfWeek = DayOfWeek.Saturday Or CurrentDate.DayOfWeek = DayOfWeek.Sunday Then
                                lblDate.BackColor = Drawing.Color.Gray
                            End If
                        Catch ex As Exception

                        End Try

                        If lblDate.Text.Trim = "1" Then
                            lblDate.BackColor = Drawing.Color.Blue
                        ElseIf lblDate.Text.Trim = "2" Then
                            lblDate.BackColor = Drawing.Color.Green
                        ElseIf lblDate.Text.Trim = "3" Then
                            lblDate.BackColor = Drawing.Color.Yellow
                        ElseIf lblDate.Text.Trim = "4" Then
                            lblDate.BackColor = Drawing.Color.Red
                        ElseIf lblDate.Text.Trim = "5" Then
                            lblDate.BackColor = Drawing.Color.Purple
                        ElseIf lblDate.Text.Trim = "6" Then
                            lblDate.BackColor = Drawing.Color.Fuchsia
                        ElseIf lblDate.Text.Trim = "7" Then
                            lblDate.BackColor = Drawing.Color.DarkSalmon
                        End If

                        'if DayOfWeek.Saturday



                        lblDate.Text = " "
                        lblDate.Width = "30"
                        lblDate.Height = "30"
                    End If
                Next
                '  @ End of  Making Color For Calender Date


             
                objSecurityXml.LoadXml(Session("Security"))
                '####################################################################
                '@ Start of Codition for Modification of Agency Target             

                '@ Start For User only change or save the data on 1 to 7th days for current Month and also allow on any days of cuurent month for next month in advance.

                Dim BlnForCurrentMonth As Boolean = False
                If Val(Now.Month) = Val(hdMonth.Value) AndAlso Val(Now.Year) = Val(hdYear.Value) Then
                    BlnForCurrentMonth = True
                End If

                If BlnForCurrentMonth = True Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                        Dim ObjXmlNodeSales_Default As XmlNode
                        ObjXmlNodeSales_Default = objSecurityXml.DocumentElement.SelectSingleNode("Sales_Default/SL_Default")
                        If ObjXmlNodeSales_Default IsNot Nothing Then
                            If ObjXmlNodeSales_Default.Attributes("TARGET_CHECK").Value.Trim.ToUpper = "TRUE" Then
                                If Now.Day <= Val(ObjXmlNodeSales_Default.Attributes("VISIT_TARGET_DAYS").Value) Then
                                    BtnSave.Enabled = True
                                Else
                                    Dim txtTarget1 As TextBox = CType(e.Row.FindControl("txtTarget"), TextBox)
                                    BtnSave.Enabled = False
                                    LnkCalender.Attributes.Clear()
                                    LnkCalender.Enabled = False
                                    txtTarget1.Enabled = False
                                End If
                            End If
                        End If
                    End If
                End If
                '@ End For User only change or save the data on 1 to 7th days for current Month and also allow on any days of cuurent month for next month in advance.

                '@ Start For Previous Month User can't change the data
                If Val(Now.Year) >= Val(hdYear.Value) Then
                    If Val(Now.Year) = Val(hdYear.Value) Then
                        If Val(Now.Month) > Val(hdMonth.Value) Then
                            Dim txtTarget1 As TextBox = CType(e.Row.FindControl("txtTarget"), TextBox)
                            BtnSave.Enabled = False
                            LnkCalender.Attributes.Clear()
                            LnkCalender.Enabled = False
                            txtTarget1.Enabled = False
                        End If
                    Else
                        Dim txtTarget1 As TextBox = CType(e.Row.FindControl("txtTarget"), TextBox)
                        BtnSave.Enabled = False
                        LnkCalender.Attributes.Clear()
                        LnkCalender.Enabled = False
                        txtTarget1.Enabled = False
                    End If
                End If

                '@ End For Previous Month User can't change the data
                '####################################################################


                LnkCalender = e.Row.FindControl("LnkCalender")

                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Target']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Target']").Attributes("Value").Value)
                        If strBuilder(2) = "0" Then
                            LnkCalender.Enabled = False
                            txtTarget.Enabled = False
                            BtnSave.Enabled = False
                        Else
                            txtTarget.Enabled = True
                            LnkCalender.Enabled = True
                            LnkCalender.Attributes.Add("onclick", "javascript:return OpenPlanVisitCalender('" + hdMonth.Value + "','" + hdYear.Value + "','" + HdMaxVisit.Value + "','" + HdUserDefinedVisit.Value + "','" + HdUserDefinedVisitDays.Value + "','" + hdLcode.Value + "','" + hdSalesId.Value + "','" + hdchkVisitTarget.Value + "')")
                        End If
                    Else
                        LnkCalender.Enabled = False
                        txtTarget.Enabled = False
                        BtnSave.Enabled = False
                    End If
                Else
                    txtTarget.Enabled = True
                    LnkCalender.Enabled = True
                    LnkCalender.Attributes.Add("onclick", "javascript:return OpenPlanVisitCalender('" + hdMonth.Value + "','" + hdYear.Value + "','" + HdMaxVisit.Value + "','" + HdUserDefinedVisit.Value + "','" + HdUserDefinedVisitDays.Value + "','" + hdLcode.Value + "','" + hdSalesId.Value + "','" + hdchkVisitTarget.Value + "')")
                End If

                lnkHistory = e.Row.FindControl("lnkHistory")
                lnkHistory.Attributes.Add("onclick", "javascript:return OpenHistoryFunction('" + hdMonth.Value + "','" + hdYear.Value + "','" + HdMaxVisit.Value + "','" + HdUserDefinedVisit.Value + "','" + HdUserDefinedVisitDays.Value + "','" + hdLcode.Value + "','" + hdSalesId.Value + "')")

                '@ Start For Colorcode Implementation
                Dim hdColorCode As HiddenField
                hdColorCode = CType(e.Row.FindControl("hdColorCode"), HiddenField)
                Dim ImgColorCode As Image
                ImgColorCode = CType(e.Row.FindControl("ImgColorCode"), Image)
                If hdColorCode.Value.Trim.ToUpper = "RED" Then
                    ImgColorCode.ImageUrl = "~/Images/Redflag.gif"
                ElseIf hdColorCode.Value.Trim.ToUpper = "PINK" Then
                    ImgColorCode.ImageUrl = "~/Images/Pinkflag.gif"
                Else
                    ImgColorCode.Visible = False
                End If
                '@ End For Colorcode Implementation


            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub

    Protected Sub grdAgencyTarget_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdAgencyTarget.Sorting

        lblError.Text = ""
        Dim dv As DataView
        Try
            Dim SortName As String = e.SortExpression

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ViewState("Direction") = "desc"
            Else
                If ViewState("SortName") = SortName Then
                    If ViewState("Direction") = "asc" Then
                        ViewState("Direction") = "desc"
                    Else
                        ViewState("Direction") = "asc"
                    End If
                Else
                    ViewState("SortName") = SortName
                    ViewState("Direction") = "desc"
                End If
            End If

            UpdatesessionWhenMovethepage()
            Sorting()
            'If Session("AgencyTargetDataSource") IsNot Nothing Then

            '    Dim dt As DataTable
            '    dt = CType(Session("AgencyTargetDataSource"), DataTable)

            '    Dim dt1 As New DataTable

            '    For i As Integer = 0 To dt.Columns.Count - 1
            '        Dim current_col, new_col As New DataColumn
            '        current_col = dt.Columns(i)
            '        new_col = New DataColumn(current_col.ColumnName, Type.GetType(current_col.DataType.FullName))
            '        dt1.Columns.Add(new_col)
            '    Next

            '    dt1.Columns("SEGSTARGET").DataType = Type.GetType("System.Int32")
            '    Dim i1 As Integer
            '    For i1 = 0 To dt.Rows.Count - 1
            '        Dim row As DataRow = dt1.NewRow()
            '        row.ItemArray = dt.Rows(i1).ItemArray
            '        dt1.Rows.Add(row)
            '    Next
            '    dt1.AcceptChanges()
            '    dv = New DataView(dt1)
            '    dv.Sort = ViewState("SortName").ToString + " " + ViewState("Direction").ToString()

            '    grdAgencyTarget.DataSource = dv
            '    grdAgencyTarget.DataBind()
            '    Session("AgencyTargetSorted") = grdAgencyTarget.DataSource
            '    SetImageForSorting(grdAgencyTarget)

            'End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Try
            If Session("Security") Is Nothing Then
                Response.Redirect("~/SupportPages/TimeOutSession.aspx?Logout=True", False)
                'ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession(), True)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Rating_Script_" + Me.ClientID.ToString(), objeAAMS.CheckSession(), True)
                'Exit Sub
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ChkBCommit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkBCommit.CheckedChanged
        Dim objInputxml As New XmlDocument
        Try

            If grdAgencyTarget.Rows.Count > 0 Then
                If FuncInputXmlAsChanged() = False Then
                    UpdatesessionWhenMovethepage()
                    Sorting()
                Else

                    objInputxml.LoadXml(ViewState("PrevSearching").ToString)
                    If objInputxml.DocumentElement.SelectSingleNode("SEARCHTYPE").ToString().ToUpper = "SEARCH" Then
                        AgencyTargetSearch()
                    Else
                        ' AgencyTargetCreate()
                    End If
                End If
            Else
                lblError.Text = "Please first click on search"
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try


       
    End Sub

    Protected Sub chkCalender_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCalender.CheckedChanged
        Dim objInputxml As New XmlDocument
        Try

            If grdAgencyTarget.Rows.Count > 0 Then
                If FuncInputXmlAsChanged() = False Then
                    UpdatesessionWhenMovethepage()
                    Sorting()
                Else

                    objInputxml.LoadXml(ViewState("PrevSearching").ToString)
                    If objInputxml.DocumentElement.SelectSingleNode("SEARCHTYPE").ToString().ToUpper = "SEARCH" Then
                        AgencyTargetSearch()
                    Else
                        ' AgencyTargetCreate()
                    End If
                End If
            Else
                lblError.Text = "Please first click on search"
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub ChkBIDT_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkBIDT.CheckedChanged
        Dim objInputxml As New XmlDocument
        Try


            If grdAgencyTarget.Rows.Count > 0 Then
                If FuncInputXmlAsChanged() = False Then
                    UpdatesessionWhenMovethepage()
                    Sorting()
                Else

                    objInputxml.LoadXml(ViewState("PrevSearching").ToString)
                    If objInputxml.DocumentElement.SelectSingleNode("SEARCHTYPE").ToString().ToUpper = "SEARCH" Then
                        AgencyTargetSearch()
                    Else
                        '  AgencyTargetCreate()
                    End If
                End If
            Else
                lblError.Text = "Please first click on search"
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub ChkCateg_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkCateg.CheckedChanged
        Dim objInputxml As New XmlDocument
        Try
            If grdAgencyTarget.Rows.Count > 0 Then
                If FuncInputXmlAsChanged() = False Then
                    UpdatesessionWhenMovethepage()
                    Sorting()
                Else
                    objInputxml.LoadXml(ViewState("PrevSearching").ToString)
                    If objInputxml.DocumentElement.SelectSingleNode("SEARCHTYPE").ToString().ToUpper = "SEARCH" Then
                        AgencyTargetSearch()
                    Else
                        ' AgencyTargetCreate()
                    End If
                End If
            Else
                lblError.Text = "Please first click on search"
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

      
    End Sub

    Protected Sub ChkChaincode_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkChaincode.CheckedChanged
        Dim objInputxml As New XmlDocument
        Try
            If grdAgencyTarget.Rows.Count > 0 Then
                If FuncInputXmlAsChanged() = False Then
                    UpdatesessionWhenMovethepage()
                    Sorting()
                Else

                    objInputxml.LoadXml(ViewState("PrevSearching").ToString)
                    If objInputxml.DocumentElement.SelectSingleNode("SEARCHTYPE").ToString().ToUpper = "SEARCH" Then
                        AgencyTargetSearch()
                    Else
                        ' AgencyTargetCreate()
                    End If
                End If
            Else
                lblError.Text = "Please first click on search"
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

      
    End Sub

    Protected Sub ChkCity_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkCity.CheckedChanged
        Dim objInputxml As New XmlDocument
        Try

            If grdAgencyTarget.Rows.Count > 0 Then
                If FuncInputXmlAsChanged() = False Then
                    UpdatesessionWhenMovethepage()
                    Sorting()
                Else
                    objInputxml.LoadXml(ViewState("PrevSearching").ToString)
                    If objInputxml.DocumentElement.SelectSingleNode("SEARCHTYPE").ToString().ToUpper = "SEARCH" Then
                        AgencyTargetSearch()
                    Else
                        ' AgencyTargetCreate()
                    End If
                End If
            Else
                lblError.Text = "Please first click on search"
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
       

    End Sub

    Protected Sub ChkLcode_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkLcode.CheckedChanged
        Try
            Dim objInputxml As New XmlDocument
            Try
                If grdAgencyTarget.Rows.Count > 0 Then
                    If FuncInputXmlAsChanged() = False Then
                        UpdatesessionWhenMovethepage()
                        Sorting()
                    Else
                        objInputxml.LoadXml(ViewState("PrevSearching").ToString)
                        If objInputxml.DocumentElement.SelectSingleNode("SEARCHTYPE").ToString().ToUpper = "SEARCH" Then
                            AgencyTargetSearch()
                        Else
                            ' AgencyTargetCreate()
                        End If
                    End If
                Else
                    lblError.Text = "Please first click on search"
                End If

            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
         
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ChkMIDT_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkMIDT.CheckedChanged
        Dim objInputxml As New XmlDocument
        Try
            If grdAgencyTarget.Rows.Count > 0 Then
                If FuncInputXmlAsChanged() = False Then
                    UpdatesessionWhenMovethepage()
                    Sorting()
                Else
                    objInputxml.LoadXml(ViewState("PrevSearching").ToString)
                    If objInputxml.DocumentElement.SelectSingleNode("SEARCHTYPE").ToString().ToUpper = "SEARCH" Then
                        AgencyTargetSearch()
                    Else
                        '  AgencyTargetCreate()
                    End If
                End If
            Else
                lblError.Text = "Please first click on search"
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ChkMinSeg_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkMinSeg.CheckedChanged
        Dim objInputxml As New XmlDocument
        Try
            If grdAgencyTarget.Rows.Count > 0 Then
                If FuncInputXmlAsChanged() = False Then
                    UpdatesessionWhenMovethepage()
                    Sorting()
                Else
                    objInputxml.LoadXml(ViewState("PrevSearching").ToString)
                    If objInputxml.DocumentElement.SelectSingleNode("SEARCHTYPE").ToString().ToUpper = "SEARCH" Then
                        AgencyTargetSearch()
                    Else
                        'AgencyTargetCreate()
                    End If
                End If
            Else
                lblError.Text = "Please first click on search"
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
     
    End Sub

    Protected Sub ChkPastMotive_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkPastMotive.CheckedChanged
        Dim objInputxml As New XmlDocument
        Try
            If grdAgencyTarget.Rows.Count > 0 Then
                If FuncInputXmlAsChanged() = False Then
                    UpdatesessionWhenMovethepage()
                    Sorting()
                Else
                    objInputxml.LoadXml(ViewState("PrevSearching").ToString)
                    If objInputxml.DocumentElement.SelectSingleNode("SEARCHTYPE").ToString().ToUpper = "SEARCH" Then
                        AgencyTargetSearch()
                    Else
                        ' AgencyTargetCreate()
                    End If
                End If
            Else
                lblError.Text = "Please first click on search"
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
      
    End Sub
    Protected Sub chkOfficeID_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkOfficeID.CheckedChanged
        Dim objInputxml As New XmlDocument
        Try
            If grdAgencyTarget.Rows.Count > 0 Then
                If FuncInputXmlAsChanged() = False Then
                    UpdatesessionWhenMovethepage()
                    Sorting()
                Else
                    objInputxml.LoadXml(ViewState("PrevSearching").ToString)
                    If objInputxml.DocumentElement.SelectSingleNode("SEARCHTYPE").ToString().ToUpper = "SEARCH" Then
                        AgencyTargetSearch()
                    Else
                        ' AgencyTargetCreate()
                    End If
                End If
            Else
                lblError.Text = "Please first click on search"
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub chkAddress_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAddress.CheckedChanged
        Dim objInputxml As New XmlDocument
        Try
            If grdAgencyTarget.Rows.Count > 0 Then
                If FuncInputXmlAsChanged() = False Then
                    UpdatesessionWhenMovethepage()
                    Sorting()
                Else
                    objInputxml.LoadXml(ViewState("PrevSearching").ToString)
                    If objInputxml.DocumentElement.SelectSingleNode("SEARCHTYPE").ToString().ToUpper = "SEARCH" Then
                        AgencyTargetSearch()
                    Else
                        ' AgencyTargetCreate()
                    End If
                End If
            Else
                lblError.Text = "Please first click on search"
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub chkResp1a_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkResp1a.CheckedChanged
        Dim objInputxml As New XmlDocument
        Try
            If grdAgencyTarget.Rows.Count > 0 Then
                If FuncInputXmlAsChanged() = False Then
                    UpdatesessionWhenMovethepage()
                    Sorting()
                Else
                    objInputxml.LoadXml(ViewState("PrevSearching").ToString)
                    If objInputxml.DocumentElement.SelectSingleNode("SEARCHTYPE").ToString().ToUpper = "SEARCH" Then
                        AgencyTargetSearch()
                    Else
                        ' AgencyTargetCreate()
                    End If
                End If
            Else
                lblError.Text = "Please first click on search"
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub chkUPVisit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkUPVisit.CheckedChanged
        Dim objInputxml As New XmlDocument
        Try
            If grdAgencyTarget.Rows.Count > 0 Then
                If FuncInputXmlAsChanged() = False Then
                    UpdatesessionWhenMovethepage()
                    Sorting()
                Else
                    objInputxml.LoadXml(ViewState("PrevSearching").ToString)
                    If objInputxml.DocumentElement.SelectSingleNode("SEARCHTYPE").ToString().ToUpper = "SEARCH" Then
                        AgencyTargetSearch()
                    Else
                        ' AgencyTargetCreate()
                    End If
                End If
            Else
                lblError.Text = "Please first click on search"
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub chkPVisit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPVisit.CheckedChanged
        Dim objInputxml As New XmlDocument
        Try
            If grdAgencyTarget.Rows.Count > 0 Then
                If FuncInputXmlAsChanged() = False Then
                    UpdatesessionWhenMovethepage()
                    Sorting()
                Else
                    objInputxml.LoadXml(ViewState("PrevSearching").ToString)
                    If objInputxml.DocumentElement.SelectSingleNode("SEARCHTYPE").ToString().ToUpper = "SEARCH" Then
                        AgencyTargetSearch()
                    Else
                        ' AgencyTargetCreate()
                    End If
                End If
            Else
                lblError.Text = "Please first click on search"
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Function FuncInputXmlAsChanged() As Boolean
        Dim blnInputXmlChanged As Boolean = False
        Dim objInputXml As New XmlDocument

        Try
            If ViewState("PrevSearching") IsNot Nothing Then
                Dim objTempInputXml As New XmlDocument
                Dim objNodeList As XmlNodeList
                objTempInputXml.LoadXml(ViewState("PrevSearching"))
                objNodeList = objTempInputXml.DocumentElement.ChildNodes

                For Each objNode As XmlNode In objNodeList
                    If objNode.Name = "MONTH" Or objNode.Name = "YEAR" Or objNode.Name = "CITY_ID" Or objNode.Name = "RESP_1A" Then

                        If objNode.Name = "MONTH" Then
                            If objNode.InnerText <> drpMonth.SelectedValue Then
                                blnInputXmlChanged = True
                            End If
                        End If

                        If objNode.Name = "YEAR" Then
                            If objNode.InnerText <> drpYear.SelectedValue Then
                                blnInputXmlChanged = True
                            End If
                        End If

                        If objNode.Name = "CITY_ID" Then
                            If objNode.InnerText <> drpTarCity.SelectedValue Then
                                blnInputXmlChanged = True
                            End If
                        End If
                        If objNode.Name = "RESP_1A" Then
                            If objNode.InnerText <> drpSalesPerson.SelectedValue Then
                                blnInputXmlChanged = True
                            End If
                        End If
                    End If
                Next
            End If
            Return blnInputXmlChanged
        Catch ex As Exception
            Return blnInputXmlChanged
        End Try
    End Function

    Private Sub ExportData()
        Session("TargetOutputExportXml") = Nothing
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            lblError.Text = "Session is expired."
            Exit Sub
        End If
        Dim objInputXml As New XmlDocument
        Dim objOutputXml As New XmlDocument
        Dim Finaloutputxml As New XmlDocument
        Dim objTarget As New AAMS.bizSales.bzAgencyTarget

        If grdAgencyTarget.Rows.Count > 0 Then

            If ViewState("PrevSearching") IsNot Nothing Then
                ' Session("InputAtargetExportXml") = ViewState("PrevSearching").ToString
                objInputXml.LoadXml(ViewState("PrevSearching").ToString)
                objOutputXml = objTarget.Search(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    Dim objreader As XmlNodeReader
                    objreader = New XmlNodeReader(objOutputXml)
                    Dim ds As New DataSet
                    ds.ReadXml(objreader)

                    If ChkLcode.Checked = False Then
                        ds.Tables("TARGET").Columns.Remove("LCODE")
                    End If
                    If ChkChaincode.Checked = False Then
                        ds.Tables("TARGET").Columns.Remove("CHAINCODE")

                    End If
                    If ChkCity.Checked = False Then
                        ds.Tables("TARGET").Columns.Remove("CITY")
                    End If

                    If ChkCateg.Checked = False Then
                        ds.Tables("TARGET").Columns.Remove("GROUP_CATG")
                    End If

                    If ChkMIDT.Checked = False Then
                        ds.Tables("TARGET").Columns.Remove("AVGMIDT")
                    End If
                    If ChkBIDT.Checked = False Then
                        ds.Tables("TARGET").Columns.Remove("AVGBIDT")
                    End If

                    If ChkBCommit.Checked = False Then
                        ds.Tables("TARGET").Columns.Remove("BUSINESSCCOMMIT")
                    End If

                    If ChkMinSeg.Checked = False Then
                        ds.Tables("TARGET").Columns.Remove("MINIUMSEGS")
                    End If

                    If ChkPastMotive.Checked = False Then
                        ds.Tables("TARGET").Columns.Remove("PASTMOTIVE")
                    End If

                    If chkAddress.Checked = False Then
                        ds.Tables("TARGET").Columns.Remove("ADDRESS")
                    End If
                    If chkOfficeID.Checked = False Then
                        ds.Tables("TARGET").Columns.Remove("OFFICEID")
                    End If
                    If chkResp1a.Checked = False Then
                        ds.Tables("TARGET").Columns.Remove("RESP1A_NAME")
                    End If
                    If chkPVisit.Checked = False Then
                        ds.Tables("TARGET").Columns.Remove("PVISITDONE")
                    End If
                    If chkUPVisit.Checked = False Then
                        ds.Tables("TARGET").Columns.Remove("UVISITDONE")
                    End If
                    If chkCalender.Checked = False Then
                        ds.Tables("TARGET").Columns.Remove("D1")
                        ds.Tables("TARGET").Columns.Remove("D2")
                        ds.Tables("TARGET").Columns.Remove("D3")
                        ds.Tables("TARGET").Columns.Remove("D4")
                        ds.Tables("TARGET").Columns.Remove("D5")
                        ds.Tables("TARGET").Columns.Remove("D6")
                        ds.Tables("TARGET").Columns.Remove("D7")
                        ds.Tables("TARGET").Columns.Remove("D8")
                        ds.Tables("TARGET").Columns.Remove("D9")
                        ds.Tables("TARGET").Columns.Remove("D10")

                        ds.Tables("TARGET").Columns.Remove("D11")
                        ds.Tables("TARGET").Columns.Remove("D12")
                        ds.Tables("TARGET").Columns.Remove("D13")
                        ds.Tables("TARGET").Columns.Remove("D14")
                        ds.Tables("TARGET").Columns.Remove("D15")
                        ds.Tables("TARGET").Columns.Remove("D16")
                        ds.Tables("TARGET").Columns.Remove("D17")
                        ds.Tables("TARGET").Columns.Remove("D18")
                        ds.Tables("TARGET").Columns.Remove("D19")
                        ds.Tables("TARGET").Columns.Remove("D20")
                        ds.Tables("TARGET").Columns.Remove("D21")
                        ds.Tables("TARGET").Columns.Remove("D22")
                        ds.Tables("TARGET").Columns.Remove("D23")
                        ds.Tables("TARGET").Columns.Remove("D24")
                        ds.Tables("TARGET").Columns.Remove("D25")
                        ds.Tables("TARGET").Columns.Remove("D26")
                        ds.Tables("TARGET").Columns.Remove("D27")
                        ds.Tables("TARGET").Columns.Remove("D28")
                        ds.Tables("TARGET").Columns.Remove("D29")
                        ds.Tables("TARGET").Columns.Remove("D30")
                        ds.Tables("TARGET").Columns.Remove("D31")

                    End If

                    ds.Tables("TARGET").Columns.Remove("COLORCODE")
                    ds.Tables("TARGET").Columns.Remove("M_CHK_VT")

                    ds.Tables("TARGET").AcceptChanges()


                    For i As Integer = 0 To ds.Tables("TARGET").Columns.Count - 1
                        ds.Tables("TARGET").Columns(i).ColumnMapping = MappingType.Attribute
                    Next

                    Finaloutputxml.LoadXml(ds.GetXml())

                    Session("TargetOutputExportXml") = Finaloutputxml.OuterXml

                    Response.Redirect("SASR_AtargetExport.aspx?Type=Export", False)
                End If
            Else
                lblError.Text = "Please first click on search."
            End If
        Else
            lblError.Text = "Please first click on search."
        End If

    End Sub
    Private Sub FillAgencyCategory()
        Dim objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzAgencyType As New AAMS.bizTravelAgency.bzAgencyType
        Try
            objOutputXml = New XmlDocument
            objOutputXml = objbzAgencyType.List
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                ChkLstAgencyCategory.DataSource = ds.Tables("AGENCYTYPE")
                ChkLstAgencyCategory.DataTextField = "Agency_Type_Name"
                ChkLstAgencyCategory.DataValueField = "AgencyTypeId"
                ChkLstAgencyCategory.DataBind()
                'ChkLstAgencyCategory.Items.Insert(ChkLstAgencyCategory.Items.Count, New ListItem("All", ""))
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub FillVisitPlanCategory()
        Dim objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzAgencyType As New AAMS.bizSales.bzAgencyTarget
        Try
            objOutputXml = New XmlDocument
            objOutputXml = objbzAgencyType.lstVisitColor
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                ChkLstVisitPlanType.DataSource = ds.Tables("VISITTYPE")
                ChkLstVisitPlanType.DataTextField = "DAY_PLAN_NAME"
                ChkLstVisitPlanType.DataValueField = "DAY_PLAN_COLOUR_ID"
                ChkLstVisitPlanType.DataBind()
                'ChkLstAgencyCategory.Items.Insert(ChkLstAgencyCategory.Items.Count, New ListItem("All", ""))
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Btnexp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnexp.Click
        BtnExport_Click(sender, e)
        ExportData()
    End Sub
    Private Sub Callpostdata()
        ChkLcode.Attributes.Add("onclick", "return PostData();")
        ChkChaincode.Attributes.Add("onclick", "return PostData();")
        ChkCateg.Attributes.Add("onclick", "return PostData();")
        ChkCity.Attributes.Add("onclick", "return PostData();")
        ChkMinSeg.Attributes.Add("onclick", "return PostData();")
        ChkBCommit.Attributes.Add("onclick", "return PostData();")
        ChkPastMotive.Attributes.Add("onclick", "return PostData();")
        ChkMIDT.Attributes.Add("onclick", "return PostData();")
        ChkBIDT.Attributes.Add("onclick", "return PostData();")
        chkCalender.Attributes.Add("onclick", "return PostData();")
        chkResp1a.Attributes.Add("onclick", "return PostData();")
        chkAddress.Attributes.Add("onclick", "return PostData();")
        chkOfficeID.Attributes.Add("onclick", "return PostData();")
        chkPVisit.Attributes.Add("onclick", "return PostData();")
        chkUPVisit.Attributes.Add("onclick", "return PostData();")

    End Sub
    'Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
    '    Return str
    'End Function

    'Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
    '    '    If eventArgument <> "" Then
    '    Dim objSales As New AAMS.bizTravelAgency.bzAgencyTarget
    '    Dim objInputXml, objOutputXml As New XmlDocument
    '    Dim id As String = eventArgument
    '    objInputXml.LoadXml("<PR_AGENCYEMPLIST_INPUT><City_Id></City_Id></PR_AGENCYEMPLIST_INPUT>")
    '    objInputXml.DocumentElement.SelectSingleNode("City_Id").InnerText = id
    '    ' objInputXml.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = "2"
    '    objOutputXml = objSales.GetAgencyEmpList(objInputXml)
    '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '        str = objOutputXml.OuterXml
    '    Else
    '        str = ""
    '    End If
    '    ' End If
    'End Sub
  
    Protected Sub BtnRefreshGrid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnRefreshGrid.Click
        Try
            If Session("UpdateData") IsNot Nothing Then
                Session("UpdateData") = Nothing
                UpdatesessionWhenMovethepage()
                Sorting()
            End If
        Catch ex As Exception

        End Try
    End Sub

   
  
End Class
