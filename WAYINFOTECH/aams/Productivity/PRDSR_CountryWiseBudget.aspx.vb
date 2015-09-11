Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Partial Class Productivity_PRDSR_CountryWiseBudget
    Inherits System.Web.UI.Page
    Dim objMsg As New eAAMSMessage
    Dim objEams As New eAAMS
    Dim tempXml As New XmlDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim strurl As String = Request.Url.ToString()
        'Session("PageName") = strurl
        '' This code is used for Expiration of Page From Cache
        'objEams.ExpirePageCache()

        ''btnPrint.Attributes.Add("onclick", "return CallPrint('grdvMarketShare')")
        ''rdSummaryOption.Attributes.Add("onclick", "return showHideBreakup()")
        '' This code is usedc for checking session handler according to user login.
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objEams.CheckSession())
            Exit Sub
        End If
        btnSearch.Attributes.Add("onclick", "return chkSearch();")
        btnSave.Attributes.Add("onclick", "return validateTarget();")
        btnHistory.Attributes.Add("onclick", "return ShowHistory();")
        If Not Page.IsPostBack Then
            LoadAllControls()
            btnSearch_Click(sender, e)
        End If

        hdCountry.Value = drpCountry.SelectedValue.Trim()
        hdYear.Value = drpYear.SelectedValue.Trim()

        Dim strBuilder As New StringBuilder
        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Country Wise Budget']").Count <> 0 Then
                strBuilder = objEams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Country Wise Budget']").Attributes("Value").Value)
                If strBuilder(0) = "0" Then
                    btnSearch.Enabled = False
                    btnHistory.Enabled = False
                    Response.Redirect("~/NoRights.aspx")
                    Exit Sub
                End If
                If strBuilder(1) = "0" Then
                    btnSave.Enabled = False
                End If

            End If
        Else
            strBuilder = objEams.SecurityCheck(31)
        End If

        'If Convert.ToInt16(drpYear.SelectedItem.Text.Trim()) >= DateTime.Now.Year Then
        '    grdvCountryWiseBudget.Enabled = True
        'Else
        '    grdvCountryWiseBudget.Enabled = False
        'End If
    End Sub
    Private Sub LoadAllControls()
        Try

            objEams.BindDropDown(drpCountry, "COUNTRY", False)

            'Dim dtYear As New DateTime
            'Dim counter As Integer
            'For counter = DateTime.Now.Year - 10 To DateTime.Now.Year + 1
            '    drpYear.Items.Add(counter.ToString())
            'Next


            Dim I As Integer
            Dim lstItem As ListItem
            'drpMonths.Items.Insert(0, "All")
            'drpYears.Items.Insert(0, "All")
           

            'Dim intYear As Int32 = DateTime.Now.Year - 3


            For I = DateTime.Now.Year + 1 To DateTime.Now.Year - 10 Step -1
                lstItem = New ListItem(I.ToString(), I.ToString())
                drpYear.Items.Add(lstItem)
            Next



            drpYear.Items.FindByText(DateTime.Now.Year.ToString).Selected = True
            drpCountry.Items.FindByText("India").Selected = True





            'tempXml.LoadXml("<PR_SEARCH_COUNTRYBUDGETTARGET_OUTPUT><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='1' YEAR='' TARGET='' USERID=''/><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='2' YEAR='' TARGET='' USERID=''/><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='3' YEAR='' TARGET='' USERID=''/><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='4' YEAR='' TARGET='' USERID=''/><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='5' YEAR='' TARGET='' USERID=''/><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='6' YEAR='' TARGET='' USERID=''/><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='7' YEAR='' TARGET='' USERID=''/><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='8' YEAR='' TARGET='' USERID=''/><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='9' YEAR='' TARGET='' USERID=''/><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='10' YEAR='' TARGET='' USERID=''/><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='11' YEAR='' TARGET='' USERID=''/><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='12' YEAR='' TARGET='' USERID=''/><Errors Status=''><Error Code='' Description=''/></Errors></PR_SEARCH_COUNTRYBUDGETTARGET_OUTPUT>")
            'Dim xNode As XmlNode

            'For Each xNode In tempXml.DocumentElement.SelectNodes("COUNTRYBUDGETTARGET")
            '    xNode.Attributes("YEAR").Value = drpYear.SelectedItem.Text.Trim()
            'Next

            'Dim dSet As New DataSet
            'Dim rdr As XmlNodeReader
            'rdr = New XmlNodeReader(tempXml)
            'dSet.ReadXml(rdr)
            'grdvCountryWiseBudget.DataSource = dSet.Tables("COUNTRYBUDGETTARGET").DefaultView
            'grdvCountryWiseBudget.DataBind()

           
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim objInputXml, objOutputXml, ObjTempData As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        ' Dim objNode As XmlNode
        Dim ds As New DataSet
        Dim objbzDailyBooking As New AAMS.bizProductivity.bzCountryBudgetTarget


        'If chkAir.Checked = True And chkCar.Checked = True And chkHotel.Checked = True And chkAirBreakUp.Checked = False Then
        objInputXml.LoadXml("<PR_SEARCH_COUNTRYBUDGETTARGET_INPUT><COUNTRYID></COUNTRYID><YEAR></YEAR></PR_SEARCH_COUNTRYBUDGETTARGET_INPUT>")
        'End If
        With objInputXml.DocumentElement

            .SelectSingleNode("COUNTRYID").InnerText = drpCountry.SelectedValue.Trim()
            .SelectSingleNode("YEAR").InnerText = drpYear.SelectedValue.Trim()
        End With

        objOutputXml = objbzDailyBooking.Search(objInputXml)
        Dim xNodes, xNodes1 As XmlNode
        'ObjTempData.LoadXml(" <PR_SEARCH_COUNTRYBUDGETTARGET_OUTPUT><Errors Status='FALSE'><Error Code='' Description='' /> </Errors><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='1' YEAR='' TARGET='' USERID='' /> <COUNTRYBUDGETTARGET COUNTRYID='' MONTH='2' YEAR='' TARGET='' USERID='' /> <COUNTRYBUDGETTARGET COUNTRYID='' MONTH='3' YEAR='' TARGET='' USERID='' /><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='4' YEAR='' TARGET='' USERID='' />  <COUNTRYBUDGETTARGET COUNTRYID='' MONTH='5' YEAR='' TARGET='' USERID='' /> <COUNTRYBUDGETTARGET COUNTRYID='' MONTH='6' YEAR='' TARGET='' USERID='' /> <COUNTRYBUDGETTARGET COUNTRYID='' MONTH='7' YEAR='' TARGET='' USERID='' /> <COUNTRYBUDGETTARGET COUNTRYID='' MONTH='8' YEAR='' TARGET='' USERID='' /> <COUNTRYBUDGETTARGET COUNTRYID='' MONTH='9' YEAR='' TARGET='' USERID='' /> <COUNTRYBUDGETTARGET COUNTRYID='' MONTH='10' YEAR='' TARGET='' USERID='' /> <COUNTRYBUDGETTARGET COUNTRYID='' MONTH='11' YEAR='' TARGET='' USERID='' /> <COUNTRYBUDGETTARGET COUNTRYID='' MONTH='12' YEAR='' TARGET='' USERID='' /> </PR_SEARCH_COUNTRYBUDGETTARGET_OUTPUT>")

        tempXml.LoadXml("<PR_SEARCH_COUNTRYBUDGETTARGET_OUTPUT><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='1' YEAR='' TARGET='' USERID=''/><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='2' YEAR='' TARGET='' USERID=''/><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='3' YEAR='' TARGET='' USERID=''/><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='4' YEAR='' TARGET='' USERID=''/><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='5' YEAR='' TARGET='' USERID=''/><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='6' YEAR='' TARGET='' USERID=''/><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='7' YEAR='' TARGET='' USERID=''/><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='8' YEAR='' TARGET='' USERID=''/><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='9' YEAR='' TARGET='' USERID=''/><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='10' YEAR='' TARGET='' USERID=''/><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='11' YEAR='' TARGET='' USERID=''/><COUNTRYBUDGETTARGET COUNTRYID='' MONTH='12' YEAR='' TARGET='' USERID=''/><Errors Status=''><Error Code='' Description=''/></Errors></PR_SEARCH_COUNTRYBUDGETTARGET_OUTPUT>")

        For Each xNodes In objOutputXml.DocumentElement.SelectNodes("COUNTRYBUDGETTARGET")
            For Each xNodes1 In tempXml.DocumentElement.SelectNodes("COUNTRYBUDGETTARGET")
                If xNodes.Attributes("MONTH").Value.Trim() = xNodes1.Attributes("MONTH").Value.Trim() Then
                    xNodes1.Attributes("TARGET").Value = xNodes.Attributes("TARGET").Value
                End If
                xNodes1.Attributes("YEAR").Value = drpYear.SelectedValue.Trim()
                xNodes1.Attributes("USERID").Value = xNodes.Attributes("USERID").Value
            Next
        Next


        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(tempXml)
            ds.ReadXml(objXmlReader)
            lblError.Text = ""
            grdvCountryWiseBudget.DataSource = ds.Tables("COUNTRYBUDGETTARGET")
            grdvCountryWiseBudget.DataBind()
        Else
            For Each xNodes1 In tempXml.DocumentElement.SelectNodes("COUNTRYBUDGETTARGET")
                xNodes1.Attributes("YEAR").Value = drpYear.SelectedValue.Trim()
            Next
            objXmlReader = New XmlNodeReader(tempXml)
            ds.ReadXml(objXmlReader)
            grdvCountryWiseBudget.DataSource = ds.Tables("COUNTRYBUDGETTARGET")
            grdvCountryWiseBudget.DataBind()
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            'Dim a As Int16
            Dim xNodes As XmlNode
            Dim objOutPutXml As New XmlDocument
            Dim objbzDailyBooking As New AAMS.bizProductivity.bzCountryBudgetTarget
            tempXml.LoadXml("<PR_UPDATE_COUNTRYBUDGETTARGET_INPUT><COUNTRYBUDGETTARGET Action='' COUNTRYID='' YEAR='' MONTH='1' TARGET='' USERID=''/><COUNTRYBUDGETTARGET Action='' COUNTRYID='' YEAR='' MONTH='2' TARGET='' USERID=''/><COUNTRYBUDGETTARGET Action='' COUNTRYID='' YEAR='' MONTH='3' TARGET='' USERID=''/><COUNTRYBUDGETTARGET Action='' COUNTRYID='' YEAR='' MONTH='4' TARGET='' USERID=''/><COUNTRYBUDGETTARGET Action='' COUNTRYID='' YEAR='' MONTH='5' TARGET='' USERID=''/><COUNTRYBUDGETTARGET Action='' COUNTRYID='' YEAR='' MONTH='6' TARGET='' USERID=''/><COUNTRYBUDGETTARGET Action='' COUNTRYID='' YEAR='' MONTH='7' TARGET='' USERID=''/><COUNTRYBUDGETTARGET Action='' COUNTRYID='' YEAR='' MONTH='8' TARGET='' USERID=''/><COUNTRYBUDGETTARGET Action='' COUNTRYID='' YEAR='' MONTH='9' TARGET='' USERID=''/><COUNTRYBUDGETTARGET Action='' COUNTRYID='' YEAR='' MONTH='10' TARGET='' USERID=''/><COUNTRYBUDGETTARGET Action='' COUNTRYID='' YEAR='' MONTH='11' TARGET='' USERID=''/><COUNTRYBUDGETTARGET Action='' COUNTRYID='' YEAR='' MONTH='12' TARGET='' USERID=''/></PR_UPDATE_COUNTRYBUDGETTARGET_INPUT>")

            'For a = 0 To grdvCountryWiseBudget.Rows.Count - 1
            '    For Each xNodes In tempXml.DocumentElement.SelectNodes("COUNTRYBUDGETTARGET")
            '        If CType(grdvCountryWiseBudget.Rows(a).FindControl("hdMonthID"), HiddenField).Value.Trim() = xNodes.Attributes("MONTH").Value.Trim() Then
            '            xNodes.Attributes("COUNTRYID").Value = drpCountry.SelectedValue.Trim()
            '            xNodes.Attributes("YEAR").Value = drpYear.SelectedItem.Text.Trim() 'CType(grdvCountryWiseBudget.Rows(a).FindControl("lblYear"), Label).Text.Trim()
            '            xNodes.Attributes("TARGET").Value = Request.Form(CType(grdvCountryWiseBudget.Rows(a).FindControl("txtTarget"), TextBox).ID)
            '        End If
            '    Next
            'Next

            For Each grdRow As GridViewRow In grdvCountryWiseBudget.Rows
                For Each xNodes In tempXml.DocumentElement.SelectNodes("COUNTRYBUDGETTARGET")
                    If CType(grdRow.FindControl("hdMonthID"), HiddenField).Value.Trim() = xNodes.Attributes("MONTH").Value.Trim() Then
                        xNodes.Attributes("COUNTRYID").Value = drpCountry.SelectedValue.Trim()
                        xNodes.Attributes("YEAR").Value = drpYear.SelectedItem.Text.Trim() 'CType(grdvCountryWiseBudget.Rows(a).FindControl("lblYear"), Label).Text.Trim()
                        Dim id As String
                        id = CType(grdRow.FindControl("txtTarget"), TextBox).UniqueID
                        Dim strValue As String
                        strValue = Request.Form(id)
                        xNodes.Attributes("TARGET").Value = strValue
                        CType(grdRow.FindControl("txtTarget"), TextBox).Text = strValue
                    End If
                Next
            Next


            Dim objEmpXml As New XmlDocument
            objEmpXml.LoadXml(Session("Security"))
            Dim UserId As String
            UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()

            If Session("Security") IsNot Nothing Then
                For Each xNodes In tempXml.DocumentElement.SelectNodes("COUNTRYBUDGETTARGET")
                    xNodes.Attributes("USERID").Value = UserId
                    xNodes.Attributes("Action").Value = "I"
                    If xNodes.Attributes("TARGET").Value.Trim() = "" Then
                        tempXml.DocumentElement.RemoveChild(xNodes)
                    End If
                Next
            End If
            

            objOutPutXml = objbzDailyBooking.Update(tempXml)


            If objOutPutXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objMsg.messUpdate
            Else
                lblError.Text = objOutPutXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvCountryWiseBudget_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvCountryWiseBudget.RowDataBound
        Dim objSecurityXml As New XmlDocument
        Dim linkEdit As New LinkButton
        Dim str As String = ""
        Dim yr As String
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            ' Dim lnkHistory As LinkButton
            'hdMonthID
            Dim hdMon As HiddenField
            linkEdit = CType(e.Row.FindControl("lnkEdit"), LinkButton)
            hdMon = CType(e.Row.FindControl("hdMonthID"), HiddenField)
           Dim txt As New TextBox
            txt = CType(e.Row.FindControl("txtTarget"), TextBox)
            yr = drpYear.SelectedValue

            'CType(e.Row.Cells(2).Controls(1), TextBox).Attributes.Add("onblur", "validateResult('" + CType(e.Row.Cells(2).Controls(1), TextBox).ClientID + "')")
            Dim strBuilder As New StringBuilder

            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Country Wise Budget']").Count <> 0 Then
                    strBuilder = objEams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Country Wise Budget']").Attributes("Value").Value)
                    If strBuilder(2) = "0" Then
                        linkEdit.Enabled = False

                    Else
                        linkEdit.Attributes.Add("onclick", "return ChangeControlStatus('" + txt.ClientID + "','" + hdMon.Value.Trim() + "')")
                    End If
                Else
                    linkEdit.Enabled = False
                End If
            Else
                linkEdit.Enabled = True
                linkEdit.Attributes.Add("onclick", "return ChangeControlStatus('" + txt.ClientID + "','" + hdMon.Value.Trim() + "')")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.OriginalString.Trim())
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
