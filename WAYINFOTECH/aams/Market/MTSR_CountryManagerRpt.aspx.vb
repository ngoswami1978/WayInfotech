Imports System.IO

Partial Class Market_MTSR_CountryManagerRpt
    Inherits System.Web.UI.Page
    Dim objEaams As New eAAMS
    Dim imgUp As New Image
    Dim imgDown As New Image
    Dim dsCommon As DataSet
    Dim dsMarketSum As DataSet
    Dim objDictionary As New HybridDictionary

    Private Sub AllNonDescColumnDefault()
        Try
            objDictionary.Add("LCODE", "LCODE")
            objDictionary.Add("CHAIN_CODE", "CHAIN_CODE")
            objDictionary.Add("NAME", "NAME")
            objDictionary.Add("CITY", "CITY")
            objDictionary.Add("OFFICEID", "OFFICEID")
            objDictionary.Add("ONLINE_STATUS", "ONLINE_STATUS")
            objDictionary.Add("SALESEXECUTIVE", "SALESEXECUTIVE")
            objDictionary.Add("STATION", "STATION")
            objDictionary.Add("MNC", "MNC")
        Catch ex As Exception

        End Try
    End Sub
  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = "Market/MTSR_CountryManagerRpt.aspx"
            objEaams.ExpirePageCache()

            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgDown.ImageUrl = "~/Images/Sortdown.gif"

            Page.MaintainScrollPositionOnPostBack = True

            drpAOffice1.Attributes.Add("onkeyup", "return gotop('drpAOffice1')")
            drpRegion.Attributes.Add("onkeyup", "return gotop('drpRegion')")
            drpCity1.Attributes.Add("onkeyup", "return gotop('drpCity1')")
            drpCountry.Attributes.Add("onkeyup", "return gotop('drpCountry')")

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objEaams.CheckSession())
            End If
            btnExport.Attributes.Add("onclick", "return ValidateCMR();")
            btnSearch.Attributes.Add("onclick", "return ValidateCMR();")


            '&&&&&&&&& Code for security purpose &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            Dim strBuilder As New StringBuilder

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Country Manager Report']").Count <> 0 Then
                    strBuilder = objEaams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Country Manager Report']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnSearch.Enabled = False
                    End If
                    If strBuilder(4) = "0" Then
                        btnExport.Enabled = False
                    End If
                End If
            Else
                strBuilder = objEaams.SecurityCheck(31)
            End If

            '&&&&&&&&& Code for security purpose &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&


            '*****************Delete Functionality




            If Not Page.IsPostBack Then
                bindAllDropdowns()

            End If




        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub bindAllDropdowns()
        Try
            objEaams.BindDropDown(drpAOffice1, "AOFFICE", True, 3)
            objEaams.BindDropDown(drpCity1, "CITY", True, 3)
            objEaams.BindDropDown(drpCountry, "COUNTRY", True, 3)
            objEaams.BindDropDown(drpRegion, "REGION", True, 3)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
    '************ Declaration Section *******************************
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objCountryManager As New AAMS.bizProductivity.bzMIDT
        Dim dSet As New DataSet
        Dim objNodeReader As XmlNodeReader
        rowData.Visible = False
        dsCommon = Nothing
        dsMarketSum = Nothing
        Try
            Dim objXdoc As New XmlDocument
            If Session("Security") IsNot Nothing Then
                objXdoc.LoadXml(Session("Security"))
            Else
                lblError.Text = "Login Again"
                Exit Sub
            End If
            objInputXml.LoadXml("<PR_RPT_COUNTRY_MANGER_INPUT><COUNTRY></COUNTRY> <CITY></CITY><AgencyName></AgencyName><LCode></LCode><Aoffice></Aoffice><Region></Region><Date></Date><Employee_ID></Employee_ID><PAGE_NO></PAGE_NO><PAGE_SIZE></PAGE_SIZE><SORT_BY></SORT_BY><DESC></DESC><CHAIN_CODE></CHAIN_CODE></PR_RPT_COUNTRY_MANGER_INPUT>")
            With objInputXml.DocumentElement
                If drpCountry.SelectedIndex > 0 Then
                    .SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text.Trim()
                Else
                    .SelectSingleNode("COUNTRY").InnerText = ""
                End If
                If drpCity1.SelectedIndex > 0 Then
                    .SelectSingleNode("CITY").InnerText = drpCity1.SelectedItem.Text.Trim()
                Else
                    .SelectSingleNode("CITY").InnerText = ""
                End If
                .SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text.Trim()

                .SelectSingleNode("LCode").InnerText = hdAgencyNameId.Value.Trim()

                'Added by Tapan Nath 14/03/2011
                If txtLcode.Text.Trim <> "" Then
                    .SelectSingleNode("LCode").InnerXml = txtLcode.Text.Trim
                End If

                If txtChaincode.Text.Trim <> "" Then
                    .SelectSingleNode("CHAIN_CODE").InnerXml = txtChaincode.Text.Trim
                End If
                'Added by Tapan Nath 14/03/2011


                If drpAOffice1.SelectedIndex > 0 Then
                    .SelectSingleNode("Aoffice").InnerText = drpAOffice1.SelectedItem.Text.Trim()
                Else
                    .SelectSingleNode("Aoffice").InnerText = ""
                End If
                If drpRegion.SelectedIndex > 0 Then
                    .SelectSingleNode("Region").InnerText = drpRegion.SelectedItem.Text.Trim()
                Else
                    .SelectSingleNode("Region").InnerText = ""
                End If
                If txtDate.Text.Trim().Length > 0 Then
                    .SelectSingleNode("Date").InnerText = objEaams.ConvertTextDate(txtDate.Text.Trim())
                End If

                .SelectSingleNode("Employee_ID").InnerText = objXdoc.DocumentElement.SelectSingleNode("EmployeeID").InnerXml



                'Start CODE for sorting and paging

                'If ViewState("PrevSearching") Is Nothing Then
                '    objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                'Else
                '    Dim objTempInputXml As New XmlDocument
                '    Dim objNodeList As XmlNodeList

                '    objTempInputXml.LoadXml(ViewState("PrevSearching"))
                '    objNodeList = objTempInputXml.DocumentElement.ChildNodes
                '    objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                '    For Each objNode As XmlNode In objNodeList
                '        If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                '            If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                '                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                '                ddlPageNumber.SelectedValue = "1"
                '            End If
                '        End If
                '    Next
                'End If


                objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = "" 'ConfigurationManager.AppSettings("PAGE_SIZE").ToString


                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "NAME"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "NAME" '"LOCATION_CODE"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
                End If

                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                Else

                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
                End If

                'End Code for paging and sorting


            End With

            'Output
            '<PR_RPT_COUNTRY_MANGER_OUTPUT>
            ' <COUNTRY_MANGER LCODE='' CHAIN_CODE='' NAME='' CITY='' OFFICEID='' ONLINE_STATUS='' MIDTTOTAL_PMONTH='' MIDTTOTAL_P2MONTH='' MIDTTOTAL_P3MONTH='' MIDTTOTAL_P4MONTH='' PROJECTION_CUR_MONTH='' NETBOOKINGS_CUR_MONTH='' NETBOOKINGS_PREV_MONTH='' BIDT2_NETBOOKINGS='' SEGS_COMP_TO_HW='' SEGS_COMP_TO_LINES='' AVG_SEGSHW_SEGS='' AVG_SEGSLINE_SEGS='' AVE_SEGSBIDT_2='' MKTSHDIFF_1_2='' MKTSHDIFF_2_3='' MKTSHDIFF_2_4='' A_PREVPER='' A_PREV2PER='' A_PREV3PER='' A_PREV4PER='' A_PREV='' A_PREV2='' A_PREV3='' A_PREV4='' B_PREV='' B_PREV2='' B_PREV3='' B_PREV4='' G_PREV='' G_PREV2='' G_PREV3='' G_PREV4='' P_PREV='' P_PREV2='' P_PREV3='' P_PREV4='' W_PREV='' W_PREV2='' W_PREV3='' W_PREV4='' TARGET='' NO_OF_1A_PIV_HW='' NO_OF_1A_OLD_HW='' NO_OF_PTYPE_HW='' MISC='' SALESEXECUTIVE='' STATION='' MNC='' />
            ' <COUNTRY_MANGER_SUMMARY MIDTTOTAL_PMONTH='' MIDTTOTAL_P2MONTH='' MIDTTOTAL_P3MONTH='' MIDTTOTAL_P4MONTH='' PROJECTION_CUR_MONTH='' NETBOOKINGS_CUR_MONTH='' NETBOOKINGS_PREV_MONTH='' BIDT2_NETBOOKINGS='' SEGS_COMP_TO_HW='' SEGS_COMP_TO_LINES='' AVG_SEGSHW_SEGS='' AVG_SEGSLINE_SEGS='' AVE_SEGSBIDT_2='' MKTSHDIFF_1_2='' MKTSHDIFF_2_3='' MKTSHDIFF_2_4='' A_PREVPER='' A_PREV2PER='' A_PREV3PER='' A_PREV4PER='' A_PREV='' A_PREV2='' A_PREV3='' A_PREV4='' B_PREV='' B_PREV2='' B_PREV3='' B_PREV4='' G_PREV='' G_PREV2='' G_PREV3='' G_PREV4='' P_PREV='' P_PREV2='' P_PREV3='' P_PREV4='' W_PREV='' W_PREV2='' W_PREV3='' W_PREV4='' TARGET='' NO_OF_1A_PIV_HW='' NO_OF_1A_OLD_HW='' NO_OF_PTYPE_HW='' MISC='' />
            ' <COUNTRY_MANGER_TOTAL A_PREV='' A_PREV2='' A_PREV3='' A_PREV4='' B_PREV='' B_PREV2='' B_PREV3='' B_PREV4='' G_PREV='' G_PREV2='' G_PREV3='' G_PREV4='' P_PREV='' P_PREV2='' P_PREV3='' P_PREV4='' W_PREV='' W_PREV2='' W_PREV3='' W_PREV4=''/>
            ' <REPORT_HEADER STATUS='' REPORTDATE=''/>
            ' <Errors Status=''>
            ' <Error Code='' Description=''/>
            ' </Errors>
            '</PR_RPT_COUNTRY_MANGER_OUTPUT>

            objOutputXml = objCountryManager.Rpt_CountryManager(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                objNodeReader = New XmlNodeReader(objOutputXml)
                lblError.Text = ""

                grdCmr.AllowSorting = False
                grdCmr.EnableViewState = True
                pnlPaging.Visible = False

                Dim objOutputXmlExport As New XmlDocument
                Dim objXmlNode, objXmlNodeClone As XmlNode
                objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("COUNTRY_MANGER")
                objXmlNodeClone = objXmlNode.CloneNode(True)
                For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                    XmlAttr.Value = ""
                Next
                'For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes
                '    Dim strAttribut As String = xmlAttrTotal.Name
                '    If objXmlNode.Attributes(strAttribut) IsNot Nothing Then
                '        objXmlNodeClone.Attributes(strAttribut).Value = xmlAttrTotal.Value
                '    End If
                '    If objXmlNode.Attributes("Aoffice") IsNot Nothing Then
                '        objXmlNodeClone.Attributes("Aoffice").Value = "Total"
                '    End If
                'Next

                'objOutputXml.DocumentElement.AppendChild(objXmlNodeClone)

                Dim dSetBind As New DataTable
                Dim dCol As DataColumn
                For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("COUNTRY_MANGER").Attributes
                    Dim strAttribut As String = xmlAttrTotal.Name
                    dCol = New DataColumn(strAttribut, System.Type.GetType("System.String"))
                    dSetBind.Columns.Add(dCol)
                Next

                Dim dRow As DataRow
                dRow = dSetBind.NewRow()
                For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("COUNTRY_MANGER").Attributes
                    Dim strAttribut As String = xmlAttrTotal.Name
                    dRow(strAttribut) = xmlAttrTotal.Value
                Next

                dSetBind.Rows.Add(dRow)

                grdCmr.DataSource = dSetBind
                grdCmr.DataBind()


                Dim objExport As New ExportExcel
                Dim IntInvisible As Integer = 0
                For intclmn As Integer = 0 To grdCmr.HeaderRow.Cells.Count - 1
                    If grdCmr.Columns(intclmn).Visible = False Then
                        IntInvisible = IntInvisible + 1
                    End If
                Next
                Dim strArray(grdCmr.HeaderRow.Cells.Count - 2 - IntInvisible) As String

                Dim intArray(grdCmr.HeaderRow.Cells.Count - 2 - IntInvisible) As Integer
                Dim intclmnVis As Integer = 0

                For intclmn As Integer = 0 To objXmlNode.Attributes.Count - 1

                    If grdCmr.Columns(intclmn).Visible = True Then
                        strArray(intclmnVis) = grdCmr.HeaderRow.Cells(intclmn).Text
                        For kk As Integer = 0 To objXmlNode.Attributes.Count - 1

                            If objXmlNode.Attributes(kk).Name.Trim = grdCmr.Columns(intclmn).SortExpression.Trim Then

                                intArray(intclmnVis) = kk

                                intclmnVis = intclmnVis + 1

                                Exit For

                            End If

                        Next kk

                    End If

                Next intclmn
                objExport.ExportDetails(objOutputXml, "COUNTRY_MANGER", intArray, strArray, ExportExcel.ExportFormat.Excel, "CountryManager.xls")
            Else
                grdCmr.DataSource = Nothing 'dSet.Tables("COUNTRY_MANGER").DefaultView
                grdCmr.DataBind()
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

            End If


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.OriginalString)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Function GetNumericSuffix(ByVal a As Integer) As String
        Dim strSuff As String = ""
        Select Case a
            Case 1
                strSuff = "First"

            Case 2
                strSuff = "nd"
            Case 3
                strSuff = "rd"
            Case Else
                strSuff = "rd"
        End Select
        Return strSuff
    End Function

    'Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Try

    '        '************ Declaration Section *******************************
    '        Dim objInputXml, objOutputXml As New XmlDocument
    '        Dim objCountryManager As New AAMS.bizProductivity.bzMIDT
    '        Dim dSet As New DataSet
    '        Dim objRdr As XmlNodeReader
    '        Dim objXnode As XmlNode
    '        Dim objExApplication As New Excel.Application
    '        Dim objExWorkBooks As Excel.Workbooks
    '        Dim objExWorkBook As Excel.Workbook
    '        Dim objExWorkSheet As Excel.Worksheet
    '        Dim objTemplatePath As String
    '        '************ End of  Declaration Section *******************************
    '        ' File.Delete("C:\Mukund_Personal\Template\DailyBookingCountryNew.xls")

    '        Try

    '            Dim objXdoc As New XmlDocument
    '            If Session("Security") IsNot Nothing Then
    '                objXdoc.LoadXml(Session("Security"))
    '            Else
    '                lblError.Text = "Login Again"
    '                Exit Sub
    '            End If
    '            Dim strFileName As String = objXdoc.DocumentElement.SelectSingleNode("EmployeeID").InnerXml

    '            strFileName = "CRM" + strFileName + ".xls"

    '            If File.Exists(Server.MapPath("~\Template\" + strFileName)) Then
    '                File.Delete(Server.MapPath("~\Template\" + strFileName))
    '            End If



    '            objTemplatePath = Server.MapPath("~\Template\MTSR_CMRTemplate.xls")

    '            objInputXml.LoadXml("<PR_RPT_COUNTRY_MANGER_INPUT><COUNTRY></COUNTRY> <CITY></CITY><AgencyName></AgencyName><LCode></LCode><Aoffice></Aoffice><Region></Region><Date></Date><Employee_ID></Employee_ID></PR_RPT_COUNTRY_MANGER_INPUT>")



    '            With objInputXml.DocumentElement
    '                If drpCountry.SelectedIndex > 0 Then
    '                    .SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text.Trim()
    '                Else
    '                    .SelectSingleNode("COUNTRY").InnerText = ""
    '                End If
    '                If drpCity1.SelectedIndex > 0 Then
    '                    .SelectSingleNode("CITY").InnerText = drpCity1.SelectedItem.Text.Trim()
    '                Else
    '                    .SelectSingleNode("CITY").InnerText = ""
    '                End If
    '                .SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text.Trim()

    '                .SelectSingleNode("LCode").InnerText = hdAgencyNameId.Value.Trim()

    '                If drpAOffice1.SelectedIndex > 0 Then
    '                    .SelectSingleNode("Aoffice").InnerText = drpAOffice1.SelectedItem.Text.Trim()
    '                Else
    '                    .SelectSingleNode("Aoffice").InnerText = ""
    '                End If
    '                If drpRegion.SelectedIndex > 0 Then
    '                    .SelectSingleNode("Region").InnerText = drpRegion.SelectedItem.Text.Trim()
    '                Else
    '                    .SelectSingleNode("Region").InnerText = ""
    '                End If
    '                If txtDate.Text.Trim().Length > 0 Then
    '                    .SelectSingleNode("Date").InnerText = objEaams.ConvertTextDateBlank(txtDate.Text.Trim())
    '                End If

    '                .SelectSingleNode("Employee_ID").InnerText = objXdoc.DocumentElement.SelectSingleNode("EmployeeID").InnerXml
    '            End With





    '            objOutputXml = objCountryManager.Rpt_CountryManager(objInputXml)

    '            Dim objRakesh As New RakeshExportExcel


    '            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

    '                objRakesh.exportToExcel(objOutputXml, "COUNTRY_MANGER", "Rakesh.xls")

    '                Exit Sub
    '                objRdr = New XmlNodeReader(objOutputXml)
    '                dSet.ReadXml(objRdr)
    '                ExportToExcel(dSet)
    '            Else
    '                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '            End If

    '        Catch ex As Exception
    '            lblError.Text = ex.Message
    '        Finally
    '            System.Runtime.InteropServices.Marshal.ReleaseComObject(objExApplication)
    '            objExWorkSheet = Nothing
    '            objExWorkBook = Nothing
    '            objExWorkBooks = Nothing
    '            objExApplication = Nothing
    '            GC.Collect()
    '        End Try

    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '    End Try
    'End Sub

    'Private Sub ExportToExcel(ByVal ds As DataSet)

    '    '<PR_RPT_COUNTRY_MANGER_OUTPUT>
    '    '	<COUNTRY_MANGER CHAIN_CODE='' NAME='' CITY='' ONLINE_STATUS='' OFFICEID='' SALESEXECUTIVE=''  AOFFICE='' 
    '    '      LCODE='' MNC='' PROJECTION_CUR_MONTH='' NETBOOKINGS_CUR_MONTH='' NETBOOKINGS_PREV_MONTH='' BIDT2_NETBOOKINGS='' MOTI_COM_TO_HW=''           
    '    '      MOT_COM_TO_CONECTIVITY='' AVE_SEGHW_MOT='' AVE_SEG_CONVHW='' AVE_SEG_BIDT_P='' MKTSHDIFF_1-2='' MKTSHDIFF_1-3='' MKTSH_P='' MKTSH_P2='' MKTSH_P3='' 
    '    '       A_PREV=''  A_PREV2='' A_PREV3='' B_PREV=''  B_PREV2=''  B_PREV3='' G_PREV=''  G_PREV2='' G_PREV3='' 
    '    '       P_PREV=''  P_PREV2='' P_PREV3=''  W_PREV=''  W_PREV2='' W_PREV3='' 
    '    '       MIDTTOTAL_PMONTH='' MIDTTOTAL_P2MONTH='' MIDTTOTAL_P3MONTH='' TARGET='' NO_OF_1A_HW='' 
    '    '       NO_OF_PTYPE_HW='' MISC=''/>
    '    '	<Errors Status=''>
    '    '		<Error Code='' Description=''/>
    '    '	</Errors>
    '    '</PR_RPT_COUNTRY_MANGER_OUTPUT>
    '    Dim ProjF, MotivesG, MotivesH, BIDTI, MoticomJ, MotcomtoConectivityK, AvesegHWMotL, AveSegM, AVEsegN As Double

    '    ProjF = 0
    '    MotivesG = 0
    '    MotivesH = 0
    '    BIDTI = 0
    '    MoticomJ = 0
    '    MotcomtoConectivityK = 0
    '    AvesegHWMotL = 0
    '    AveSegM = 0
    '    AVEsegN = 0
    '    Dim objExApplication As New Excel.Application
    '    Dim objExWorkBooks As Excel.Workbooks
    '    Dim objExWorkBook As Excel.Workbook
    '    Dim objExWorkSheet As Excel.Worksheet
    '    objExApplication = New Excel.Application



    '    Try

    '        Dim rs As ADODB.Recordset
    '        rs = New ADODB.Recordset
    '        rs.CursorLocation = ADODB.CursorLocationEnum.adUseClient
    '        rs.CursorType = ADODB.CursorTypeEnum.adOpenDynamic
    '        rs.LockType = ADODB.LockTypeEnum.adLockOptimistic
    '        With rs.Fields
    '            .Append("CHAIN_CODE", ADODB.DataTypeEnum.adVarChar, 50, ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("NAME", ADODB.DataTypeEnum.adVarChar, 50, ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("CITY", ADODB.DataTypeEnum.adVarChar, 50, ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("ONLINE_STATUS", ADODB.DataTypeEnum.adVarChar, 50, ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("OFFICEID", ADODB.DataTypeEnum.adVarChar, 9, ADODB.FieldAttributeEnum.adFldIsNullable)

    '            .Append("PROJECTION_CUR_MONTH", ADODB.DataTypeEnum.adDecimal, 10) ', 50, ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("NETBOOKINGS_CUR_MONTH", ADODB.DataTypeEnum.adDecimal, 10) ', 50, ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("NETBOOKINGS_PREV_MONTH", ADODB.DataTypeEnum.adDecimal, 10) ' 50, ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("BIDT2_NETBOOKINGS", ADODB.DataTypeEnum.adDecimal, 10) ' ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("MOTI_COM_TO_HW", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)

    '            .Append("MOT_COM_TO_CONECTIVITY", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("AVE_SEGHW_MOT", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("AVE_SEG_CONVHW", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("AVE_SEG_BIDT_P", ADODB.DataTypeEnum.adDecimal, 3) ', ADODB.FieldAttributeEnum.adFldIsNullable)

    '            .Append("MKTSHDIFF_1-2", ADODB.DataTypeEnum.adDecimal, 3) ' ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("MKTSHDIFF_1-3", ADODB.DataTypeEnum.adDecimal, 10) ' ADODB.FieldAttributeEnum.adFldIsNullable)

    '            .Append("MKTSH_P", ADODB.DataTypeEnum.adDecimal, 10) ' ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("MKTSH_P2", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("MKTSH_P3", ADODB.DataTypeEnum.adDecimal, 10) ' ADODB.FieldAttributeEnum.adFldIsNullable)

    '            .Append("A_PREV", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("A_PREV2", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("A_PREV3", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)

    '            .Append("B_PREV", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("B_PREV2", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("B_PREV3", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)

    '            .Append("G_PREV", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("G_PREV2", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("G_PREV3", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)

    '            .Append("P_PREV", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("P_PREV2", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("P_PREV3", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("W_PREV", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("W_PREV2", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("W_PREV3", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)

    '            .Append("MIDTTOTAL_PMONTH", ADODB.DataTypeEnum.adDecimal, 10) ' 50, ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("MIDTTOTAL_P2MONTH", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("MIDTTOTAL_P3MONTH", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("TARGET", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("NO_OF_1A_HW", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("NO_OF_PTYPE_HW", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("MISC", ADODB.DataTypeEnum.adDecimal, 10) ', ADODB.FieldAttributeEnum.adFldIsNullable)

    '            .Append("SALESEXECUTIVE", ADODB.DataTypeEnum.adVarChar, 50, ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("AOFFICE", ADODB.DataTypeEnum.adVarChar, 3, ADODB.FieldAttributeEnum.adFldIsNullable)
    '            .Append("LCODE", ADODB.DataTypeEnum.adVarChar, 50, ADODB.FieldAttributeEnum.adFldIsNullable)
    '            ' .Append("MNC", ADODB.DataTypeEnum.adVarChar, 50, ADODB.FieldAttributeEnum.adFldIsNullable)
    '        End With



    '        Dim strFileName, objTemplatePath As String
    '        strFileName = "24"

    '        strFileName = "CRM" + strFileName + ".xls"

    '        If File.Exists(Server.MapPath("~\Template\" + strFileName)) Then
    '            File.Delete(Server.MapPath("~\Template\" + strFileName))
    '        End If



    '        objTemplatePath = Server.MapPath("~\Template\MTSR_CMRTemplate.xls")




    '        objExWorkBooks = objExApplication.Workbooks
    '        objExWorkBook = objExWorkBooks.Open(objTemplatePath)
    '        objExWorkSheet = objExApplication.ActiveSheet



    '        rs.Open()

    '        Dim loDataRow As DataRow
    '        Dim loDataColumn As DataColumn
    '        Dim loColumnIndex As Integer
    '        Dim rowCounter As Integer = 9


    '        For Each loDataRow In ds.Tables("COUNTRY_MANGER").Rows
    '            rs.AddNew()
    '            loColumnIndex = 0

    '            For Each loDataColumn In ds.Tables("COUNTRY_MANGER").Columns
    '                If loDataColumn.ColumnName <> "MNC" Then
    '                    rs.Fields(loColumnIndex).Value = loDataRow(loDataColumn.ColumnName).ToString.Trim & ""
    '                    loColumnIndex += 1
    '                End If
    '            Next




    '            'ProjF += Val(loDataRow("PROJECTION_CUR_MONTH").ToString.Trim)
    '            'MotivesG += Val(loDataRow("NETBOOKINGS_CUR_MONTH").ToString.Trim)
    '            'MotivesH += Val(loDataRow("NETBOOKINGS_PREV_MONTH").ToString.Trim)
    '            'BIDTI += Val(loDataRow("BIDT2_NETBOOKINGS").ToString.Trim)
    '            'MoticomJ += Val(loDataRow("MOTI_COM_TO_HW").ToString.Trim)
    '            'MotcomtoConectivityK += Val(loDataRow("MOT_COM_TO_CONECTIVITY").ToString.Trim)




    '            '    'Calculated Columns

    '            Dim intTotalLM As Double = (Val(Val(loDataRow("PROJECTION_CUR_MONTH").ToString())) + Val(Val(loDataRow("NETBOOKINGS_PREV_MONTH").ToString())) + Val(Val(loDataRow("BIDT2_NETBOOKINGS").ToString()))) / 3

    '            Dim intAvgSegL As Double = intTotalLM - Val(loDataRow("MOTI_COM_TO_HW").ToString())
    '            ' objExWorkSheet.Cells(rowCounter, 12) = intAvgSegL.ToString() 'Calculate objXnode.Attributes("AVE_SEGHW_MOT").InnerText.ToString()
    '            rs(12).Value = intAvgSegL.ToString()

    '            ' AvesegHWMotL += intAvgSegL

    '            Dim intAvgSegM As Double = intTotalLM - Val(loDataRow("MOT_COM_TO_CONECTIVITY").ToString())
    '            'objExWorkSheet.Cells(rowCounter, 13) = intAvgSegM.ToString() 'Calculate objXnode.Attributes("AVE_SEG_CONVHW").InnerText.ToString()
    '            rs(13).Value = intAvgSegM.ToString()

    '            Dim intTotalN As Double = (Val(loDataRow("PROJECTION_CUR_MONTH").ToString()) + Val(loDataRow("NETBOOKINGS_PREV_MONTH").ToString()) - Val(loDataRow("BIDT2_NETBOOKINGS").ToString())) / 100

    '            rs(14).Value = intTotalN.ToString() 'Calculate objXnode.Attributes("AVE_SEG_BIDT_P").InnerText.ToString()

    '            'AveSegM += intAvgSegM
    '            'AVEsegN += intTotalN

    '            '    '=IF(AI13=0,0,T13/AI13)/ 100

    '            Dim intTotalQ As Double = 0
    '            If Val(loDataRow("MIDTTOTAL_PMONTH").ToString()) = 0 Then
    '                intTotalQ = 0
    '            Else
    '                intTotalQ = Val(loDataRow("A_PREV").ToString()) / Val(loDataRow("MIDTTOTAL_PMONTH").ToString())
    '            End If
    '            intTotalQ = intTotalQ / 100
    '            rs(17).Value = intTotalQ.ToString() 'Calculated  objXnode.Attributes("MKTSH_P").InnerText.ToString()



    '            '    '=IF(AJ19=0,0,U19/AJ19)/ 100
    '            Dim intTotalR As Double = 0
    '            If Val(loDataRow("MIDTTOTAL_P2MONTH").ToString()) = 0 Then
    '                intTotalR = 0
    '            Else
    '                intTotalR = Val(loDataRow("A_PREV2").ToString()) / Val(loDataRow("MIDTTOTAL_P2MONTH").ToString())
    '            End If
    '            intTotalR = intTotalR / 100
    '            rs(18).Value = intTotalR.ToString() 'Calculated  objXnode.Attributes("MKTSH_P2").InnerText.ToString()


    '            '    '=IF(AK19=0,0,V19/AK19)

    '            Dim intTotalS As Double = 0
    '            If Val(loDataRow("MIDTTOTAL_P3MONTH").ToString()) = 0 Then
    '                intTotalS = 0
    '            Else
    '                intTotalS = Val(loDataRow("A_PREV3").ToString()) / Val(loDataRow("MIDTTOTAL_P3MONTH").ToString())
    '            End If
    '            intTotalS = intTotalS / 100
    '            rs(19).Value = intTotalS.ToString() 'Calculated objXnode.Attributes("MKTSH_P3").InnerText.ToString()



    '            Dim valO As Double = intTotalQ - intTotalR / 100
    '            rs(15).Value = valO.ToString() 'Calculated objXnode.Attributes("MKTSHDIFF_1-2").InnerText.ToString()


    '            Dim valP As Double = intTotalQ - intTotalS / 100
    '            rs(16).Value = valP.ToString() 'Calculated objXnode.Attributes("MKTSHDIFF_1-3").InnerText.ToString()




    '            '    'End of Calculated Columns

    '            'Formatting by Row on MNC or Non-MNC
    '            If loDataRow("MNC").ToString.Trim = "1" Then
    '                objExWorkSheet.Cells.Range("A" + rowCounter.ToString(), "AR" + rowCounter.ToString()).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange)
    '            ElseIf loDataRow("MNC").ToString.Trim = "2" Then
    '                objExWorkSheet.Cells.Range("A" + rowCounter.ToString(), "AR" + rowCounter.ToString()).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver)

    '            End If
    '            'Formatting by Row on MNC or Non-MNC



    '            If intAvgSegL > -100.0 And intAvgSegL < -50.0 Then
    '                objExWorkSheet.Cells.Range("L" + rowCounter.ToString(), "L" + rowCounter.ToString()).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)
    '            ElseIf intAvgSegL > -300.0 And intAvgSegL < -101.0 Then
    '                objExWorkSheet.Cells.Range("L" + rowCounter.ToString(), "L" + rowCounter.ToString()).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
    '            ElseIf intAvgSegL < -301.0 Then
    '                objExWorkSheet.Cells.Range("L" + rowCounter.ToString(), "L" + rowCounter.ToString()).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Violet)
    '            End If

    '            If intAvgSegM > -100 And intAvgSegM < -50 Then
    '                objExWorkSheet.Cells.Range("M" + rowCounter.ToString(), "M" + rowCounter.ToString()).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)
    '            ElseIf intAvgSegM > -300 And intAvgSegM < -101 Then
    '                objExWorkSheet.Cells.Range("M" + rowCounter.ToString(), "M" + rowCounter.ToString()).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
    '            ElseIf intAvgSegM < -301 Then
    '                objExWorkSheet.Cells.Range("M" + rowCounter.ToString(), "M" + rowCounter.ToString()).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Violet)
    '            End If

    '            If intTotalN > -100 And intTotalN < -50 Then
    '                objExWorkSheet.Cells.Range("N" + rowCounter.ToString(), "N" + rowCounter.ToString()).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)
    '            ElseIf intTotalN > -300 And intTotalN < -101 Then
    '                objExWorkSheet.Cells.Range("M" + rowCounter.ToString(), "N" + rowCounter.ToString()).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
    '            ElseIf intTotalN < -301 Then
    '                objExWorkSheet.Cells.Range("N" + rowCounter.ToString(), "N" + rowCounter.ToString()).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Violet)
    '            End If


    '            'Condition for Formatting of Mkt Segment

    '            If valO < -101 Then
    '                objExWorkSheet.Cells.Range("O" + rowCounter.ToString(), "O" + rowCounter.ToString()).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Tomato)
    '            ElseIf valO > -40 And valO < -10 Then
    '                objExWorkSheet.Cells.Range("O" + rowCounter.ToString(), "O" + rowCounter.ToString()).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.SpringGreen)
    '            ElseIf valO > -101 And valO < -41 Then
    '                objExWorkSheet.Cells.Range("O" + rowCounter.ToString(), "O" + rowCounter.ToString()).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.SteelBlue)
    '            End If

    '            If valP < -101 Then
    '                objExWorkSheet.Cells.Range("O" + rowCounter.ToString(), "O" + rowCounter.ToString()).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Tomato)
    '            ElseIf valP > -40 And valP < -10 Then
    '                objExWorkSheet.Cells.Range("O" + rowCounter.ToString(), "O" + rowCounter.ToString()).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.SpringGreen)
    '            ElseIf valP > -101 And valP < -41 Then
    '                objExWorkSheet.Cells.Range("O" + rowCounter.ToString(), "O" + rowCounter.ToString()).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.SteelBlue)
    '            End If

    '            rs.Update()
    '            rowCounter += 1
    '        Next

    '        objExWorkSheet.Range("A9").CopyFromRecordset(rs)



    '        'Code Segment written for Making Header dynamically

    '        Dim strDateValue As String() = txtDate.Text.Split("/")

    '        Dim MonthCounter As Integer = Convert.ToInt32(strDateValue(1))
    '        Dim SelectedDate As Integer = Convert.ToInt32(strDateValue(0))

    '        Dim prevMonthCounter, prev2MonthCounter, prev3MonthCounter As Integer

    '        If MonthCounter = 1 Then
    '            prevMonthCounter = 12
    '        Else
    '            prevMonthCounter = MonthCounter - 1
    '        End If

    '        Dim tempval As Integer

    '        If MonthCounter <= 2 Then
    '            tempval = MonthCounter - 2
    '            prev2MonthCounter = tempval + 12
    '        Else
    '            prev2MonthCounter = MonthCounter - 2
    '        End If

    '        If MonthCounter <= 3 Then
    '            tempval = MonthCounter - 3
    '            prev3MonthCounter = tempval + 12
    '        Else
    '            prev3MonthCounter = MonthCounter - 3
    '        End If


    '        Dim strCurrent As String = MonthName(MonthCounter).ToString().ToUpper().Substring(0, 3)
    '        Dim strPrev As String = MonthName(prevMonthCounter).ToString().ToUpper().Substring(0, 3)
    '        Dim strCurrent1 As String = MonthName(prev2MonthCounter).ToString().ToUpper().Substring(0, 3)
    '        Dim strCurrent2 As String = MonthName(prev3MonthCounter).ToString().ToUpper().Substring(0, 3)


    '        objExWorkSheet.Cells(8, 6) = "Proj " + strCurrent
    '        objExWorkSheet.Cells(8, 7) = "Motives " + strCurrent + SelectedDate.ToString()
    '        objExWorkSheet.Cells(8, 8) = "Motives " + strPrev
    '        objExWorkSheet.Cells(8, 9) = "BIDT " + strCurrent2
    '        objExWorkSheet.Cells(8, 14) = "AVE SEG." + strCurrent1 + "  BIDT"
    '        objExWorkSheet.Cells(8, 15) = "MKT SH DIFF " + strPrev + "-" + strCurrent1
    '        objExWorkSheet.Cells(8, 16) = "MKT SH DIFF " + strPrev + "-" + strCurrent2

    '        objExWorkSheet.Cells(8, 17) = "MKT SH " + strPrev
    '        objExWorkSheet.Cells(8, 18) = "MKT SH " + strCurrent1
    '        objExWorkSheet.Cells(8, 19) = "MKT SH " + strCurrent2

    '        objExWorkSheet.Cells(8, 20) = "1A " + strPrev
    '        objExWorkSheet.Cells(8, 21) = "1A " + strCurrent1
    '        objExWorkSheet.Cells(8, 22) = "1A " + strCurrent2


    '        objExWorkSheet.Cells(8, 23) = "1B " + strPrev
    '        objExWorkSheet.Cells(8, 24) = "1B " + strCurrent1
    '        objExWorkSheet.Cells(8, 25) = "1B " + strCurrent2


    '        objExWorkSheet.Cells(8, 26) = "1G " + strPrev
    '        objExWorkSheet.Cells(8, 27) = "1G " + strCurrent1
    '        objExWorkSheet.Cells(8, 28) = "1G " + strCurrent2


    '        objExWorkSheet.Cells(8, 29) = "1P " + strPrev
    '        objExWorkSheet.Cells(8, 30) = "1P " + strCurrent1
    '        objExWorkSheet.Cells(8, 31) = "1P " + strCurrent2

    '        objExWorkSheet.Cells(8, 32) = "1W " + strPrev
    '        objExWorkSheet.Cells(8, 33) = "1W " + strCurrent1
    '        objExWorkSheet.Cells(8, 34) = "1W " + strCurrent2

    '        objExWorkSheet.Cells(8, 35) = "Total " + strPrev
    '        objExWorkSheet.Cells(8, 36) = "Total " + strCurrent1
    '        objExWorkSheet.Cells(8, 37) = "Total " + strCurrent2

    '        objExWorkSheet.Cells(8, 38) = "Target 1A " + strPrev

    '        'Code Segment written for Making Header dynamically


    '        'Setting Total value on Top 

    '        'objExWorkSheet.Cells(7, 6) = ProjF
    '        'objExWorkSheet.Cells(7, 7) = MotivesG
    '        'objExWorkSheet.Cells(7, 8) = MotivesH
    '        'objExWorkSheet.Cells(7, 9) = BIDTI
    '        'objExWorkSheet.Cells(7, 10) = MoticomJ
    '        'objExWorkSheet.Cells(7, 11) = MotcomtoConectivityK
    '        'objExWorkSheet.Cells(7, 12) = AvesegHWMotL
    '        'objExWorkSheet.Cells(7, 13) = AveSegM
    '        'objExWorkSheet.Cells(7, 14) = AVEsegN

    '        Dim formula As String
    '        formula = "=SUBTOTAL(9, F9" + ":F" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("F7", "F7").Formula = formula


    '        formula = "=SUBTOTAL(9, G9" + ":G" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("G7", "G7").Formula = formula

    '        formula = "=SUBTOTAL(9, H9" + ":H" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("H7", "H7").Formula = formula

    '        formula = "=SUBTOTAL(9, I9" + ":I" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("I7", "I7").Formula = formula

    '        formula = "=SUBTOTAL(9, J9" + (rowCounter - 1).ToString() + ":J" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("J7", "J7").Formula = formula

    '        formula = "=SUBTOTAL(9, K9" + ":K" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("K7", "K7").Formula = formula

    '        formula = "=SUBTOTAL(9, L9" + ":L" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("L7", "L7").Formula = formula

    '        formula = "=SUBTOTAL(9, M9" + ":M" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("M7", "M7").Formula = formula

    '        formula = "=SUBTOTAL(9, N9" + ":N" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("N7", "N7").Formula = formula





    '        '************
    '        formula = "=SUBTOTAL(9, T9" + ":T" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("T7", "T7").Formula = formula

    '        formula = "=SUBTOTAL(9, U9" + ":U" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("U7", "U7").Formula = formula

    '        formula = "=SUBTOTAL(9, V9" + ":V" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("V7", "V7").Formula = formula

    '        formula = "=SUBTOTAL(9, W9" + ":W" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("W7", "W7").Formula = formula

    '        formula = "=SUBTOTAL(9, X9" + ":X" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("X7", "X7").Formula = formula

    '        formula = "=SUBTOTAL(9, Y9" + ":Y" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("Y7", "Y7").Formula = formula

    '        formula = "=SUBTOTAL(9, Z9" + ":Z" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("Z7", "Z7").Formula = formula

    '        formula = "=SUBTOTAL(9, AA9" + ":AA" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("AA7", "AA7").Formula = formula

    '        formula = "=SUBTOTAL(9, AB9" + ":AB" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("AB7", "AB7").Formula = formula

    '        formula = "=SUBTOTAL(9, AC9" + ":AC" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("AC7", "AC7").Formula = formula

    '        formula = "=SUBTOTAL(9, AD9" + ":AD" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("AD7", "AD7").Formula = formula

    '        formula = "=SUBTOTAL(9, AF9" + ":AF" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("AF7", "AF7").Formula = formula

    '        formula = "=SUBTOTAL(9, AG9" + ":AG" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("AG7", "AG7").Formula = formula

    '        formula = "=SUBTOTAL(9, AH9" + ":AH" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("AH7", "AH7").Formula = formula

    '        formula = "=SUBTOTAL(9, AI9" + ":AI" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("AI7", "AI7").Formula = formula

    '        formula = "=SUBTOTAL(9, AJ9" + ":AJ" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("AJ7", "AJ7").Formula = formula

    '        formula = "=SUBTOTAL(9, AK9" + ":AK" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("AK7", "AK7").Formula = formula

    '        formula = "=SUBTOTAL(9, AL9" + ":AL" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("AL7", "AL7").Formula = formula

    '        formula = "=SUBTOTAL(9, AM9" + ":AM" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("AM7", "AM7").Formula = formula

    '        formula = "=SUBTOTAL(9, AN9" + ":AN" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("AN7", "AN7").Formula = formula

    '        formula = "=SUBTOTAL(9, AO9" + ":AO" + (rowCounter - 1).ToString() + ")"
    '        objExWorkSheet.Range("AO7", "AO7").Formula = formula



    '        'Dim formula As String = "=ROUND(SUM(H24" + ":H" + (rowCounter + ttlRows - 2).ToString() + ")*100/SUM(L24:L" + (rowCounter + ttlRows - 2).ToString() + "),2)-100"

    '        'Setting Total value on Top 
    '        '    Dim formula As String = "=ROUND(SUM(H24" + ":H" + (rowCounter + ttlRows-2).ToString() + ")*100/SUM(L24:L" + (rowCounter + ttlRows-2).ToString() + "),2)-100"
    '        '    objExWorkSheet.Range("M" + FormulaRow.ToString(), "M" + FormulaRow.ToString()).Formula = formula '"=ROUND(SUM(H24:H54)*100/SUM(L24:L54),2)-100"

    '        objTemplatePath = Server.MapPath("~\Template\" + strFileName)
    '        objExWorkBook.SaveAs(objTemplatePath)
    '        objExWorkBook.Close()
    '        objExWorkBooks.Close()
    '        objExApplication.Quit()


    '        Dim filePath As String = objTemplatePath ' Server.MapPath(specify the file path on server here) 
    '        Dim targetFile As System.IO.FileInfo = New System.IO.FileInfo(filePath)
    '        If targetFile.Exists Then
    '            Response.Clear()
    '            Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name)
    '            Response.AddHeader("Content-Length", targetFile.Length.ToString)
    '            Response.ContentType = "excel"
    '            Response.WriteFile(targetFile.FullName)
    '        End If

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    Finally

    '        System.Runtime.InteropServices.Marshal.ReleaseComObject(objExApplication)
    '        objExWorkSheet = Nothing
    '        objExWorkBook = Nothing
    '        objExWorkBooks = Nothing
    '        objExApplication = Nothing
    '        GC.Collect()
    '    End Try
    'End Sub

    'Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click

    '    '************ Declaration Section *******************************
    '    Dim objInputXml, objOutputXml As New XmlDocument
    '    Dim objCountryManager As New AAMS.bizProductivity.bzMIDT
    '    Dim dSet As New DataSet
    '    Dim objRdr As XmlNodeReader
    '    Dim objXnode As XmlNode
    '    Dim objTemplatePath As String
    '    '************ End of  Declaration Section *******************************
    '    ' File.Delete("C:\Mukund_Personal\Template\DailyBookingCountryNew.xls")

    '    Try

    '        Dim objXdoc As New XmlDocument
    '        If Session("Security") IsNot Nothing Then
    '            objXdoc.LoadXml(Session("Security"))
    '        Else
    '            lblError.Text = "Login Again"
    '            Exit Sub
    '        End If


    '        objInputXml.LoadXml("<PR_RPT_COUNTRY_MANGER_INPUT><COUNTRY></COUNTRY> <CITY></CITY><AgencyName></AgencyName><LCode></LCode><Aoffice></Aoffice><Region></Region><Date></Date><Employee_ID></Employee_ID></PR_RPT_COUNTRY_MANGER_INPUT>")



    '        With objInputXml.DocumentElement
    '            If drpCountry.SelectedIndex > 0 Then
    '                .SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text.Trim()
    '            Else
    '                .SelectSingleNode("COUNTRY").InnerText = ""
    '            End If
    '            If drpCity1.SelectedIndex > 0 Then
    '                .SelectSingleNode("CITY").InnerText = drpCity1.SelectedItem.Text.Trim()
    '            Else
    '                .SelectSingleNode("CITY").InnerText = ""
    '            End If
    '            .SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text.Trim()

    '            .SelectSingleNode("LCode").InnerText = hdAgencyNameId.Value.Trim()

    '            If drpAOffice1.SelectedIndex > 0 Then
    '                .SelectSingleNode("Aoffice").InnerText = drpAOffice1.SelectedItem.Text.Trim()
    '            Else
    '                .SelectSingleNode("Aoffice").InnerText = ""
    '            End If
    '            If drpRegion.SelectedIndex > 0 Then
    '                .SelectSingleNode("Region").InnerText = drpRegion.SelectedItem.Text.Trim()
    '            Else
    '                .SelectSingleNode("Region").InnerText = ""
    '            End If
    '            If txtDate.Text.Trim().Length > 0 Then
    '                .SelectSingleNode("Date").InnerText = objEaams.ConvertTextDateBlank(txtDate.Text.Trim())
    '            End If

    '            .SelectSingleNode("Employee_ID").InnerText = objXdoc.DocumentElement.SelectSingleNode("EmployeeID").InnerXml
    '        End With

    '        objOutputXml = objCountryManager.Rpt_CountryManager(objInputXml)
    '        Dim objExport As New ExportExcelMukund
    '        Dim intArray(objOutputXml.DocumentElement.SelectSingleNode("COUNTRY_MANGER").Attributes.Count - 1) As Integer
    '        Dim strArray(objOutputXml.DocumentElement.SelectSingleNode("COUNTRY_MANGER").Attributes.Count - 1) As String
    '        Dim objAtt As XmlAttribute
    '        Dim colCounter As Integer = 0
    '        For Each objAtt In objOutputXml.DocumentElement.SelectSingleNode("COUNTRY_MANGER").Attributes
    '            intArray(colCounter) = colCounter
    '            strArray(colCounter) = objAtt.Name
    '            colCounter += 1
    '        Next

    '        objExport.ExportDetails(objOutputXml, "COUNTRY_MANGER", intArray, strArray, ExportExcel.ExportFormat.CSV, "CountryManager.csv")


    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            bindData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try


    End Sub
    Private Sub bindData()
        '************ Declaration Section *******************************
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objCountryManager As New AAMS.bizProductivity.bzMIDT
        Dim dSet As New DataSet
        Dim objNodeReader As XmlNodeReader

        Try
            Dim objXdoc As New XmlDocument
            If Session("Security") IsNot Nothing Then
                objXdoc.LoadXml(Session("Security"))
            Else
                lblError.Text = "Login Again"
                Exit Sub
            End If
            objInputXml.LoadXml("<PR_RPT_COUNTRY_MANGER_INPUT><COUNTRY></COUNTRY> <CITY></CITY><AgencyName></AgencyName><LCode></LCode><Aoffice></Aoffice><Region></Region><Date></Date><Employee_ID></Employee_ID><PAGE_NO></PAGE_NO><PAGE_SIZE></PAGE_SIZE><SORT_BY></SORT_BY><DESC></DESC><CHAIN_CODE></CHAIN_CODE></PR_RPT_COUNTRY_MANGER_INPUT>")
            With objInputXml.DocumentElement
                If drpCountry.SelectedIndex > 0 Then
                    .SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text.Trim()
                Else
                    .SelectSingleNode("COUNTRY").InnerText = ""
                End If
                If drpCity1.SelectedIndex > 0 Then
                    .SelectSingleNode("CITY").InnerText = drpCity1.SelectedItem.Text.Trim()
                Else
                    .SelectSingleNode("CITY").InnerText = ""
                End If
                .SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text.Trim()

                .SelectSingleNode("LCode").InnerText = hdAgencyNameId.Value.Trim()

                'Added by Tapan Nath 14/03/2011
                If txtLcode.Text.Trim <> "" Then
                    .SelectSingleNode("LCode").InnerText = txtLcode.Text.Trim
                End If

                If txtChaincode.Text.Trim <> "" Then
                    .SelectSingleNode("CHAIN_CODE").InnerText = txtChaincode.Text.Trim
                End If
                'Added by Tapan Nath 14/03/2011

                If drpAOffice1.SelectedIndex > 0 Then
                    .SelectSingleNode("Aoffice").InnerText = drpAOffice1.SelectedItem.Text.Trim()
                Else
                    .SelectSingleNode("Aoffice").InnerText = ""
                End If
                If drpRegion.SelectedIndex > 0 Then
                    .SelectSingleNode("Region").InnerText = drpRegion.SelectedItem.Text.Trim()
                Else
                    .SelectSingleNode("Region").InnerText = ""
                End If
                If txtDate.Text.Trim().Length > 0 Then
                    .SelectSingleNode("Date").InnerText = objEaams.ConvertTextDate(txtDate.Text.Trim())
                End If

                .SelectSingleNode("Employee_ID").InnerText = objXdoc.DocumentElement.SelectSingleNode("EmployeeID").InnerXml



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
                    ViewState("SortName") = "NAME"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "NAME" '"LOCATION_CODE"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
                End If

                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                Else

                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
                End If

                'End Code for paging and sorting


            End With

            'Output
            '<PR_RPT_COUNTRY_MANGER_OUTPUT>
            ' <COUNTRY_MANGER LCODE='' CHAIN_CODE='' NAME='' CITY='' OFFICEID='' ONLINE_STATUS='' MIDTTOTAL_PMONTH='' MIDTTOTAL_P2MONTH='' MIDTTOTAL_P3MONTH='' MIDTTOTAL_P4MONTH='' PROJECTION_CUR_MONTH='' NETBOOKINGS_CUR_MONTH='' NETBOOKINGS_PREV_MONTH='' BIDT2_NETBOOKINGS='' SEGS_COMP_TO_HW='' SEGS_COMP_TO_LINES='' AVG_SEGSHW_SEGS='' AVG_SEGSLINE_SEGS='' AVE_SEGSBIDT_2='' MKTSHDIFF_1_2='' MKTSHDIFF_2_3='' MKTSHDIFF_2_4='' A_PREVPER='' A_PREV2PER='' A_PREV3PER='' A_PREV4PER='' A_PREV='' A_PREV2='' A_PREV3='' A_PREV4='' B_PREV='' B_PREV2='' B_PREV3='' B_PREV4='' G_PREV='' G_PREV2='' G_PREV3='' G_PREV4='' P_PREV='' P_PREV2='' P_PREV3='' P_PREV4='' W_PREV='' W_PREV2='' W_PREV3='' W_PREV4='' TARGET='' NO_OF_1A_PIV_HW='' NO_OF_1A_OLD_HW='' NO_OF_PTYPE_HW='' MISC='' SALESEXECUTIVE='' STATION='' MNC='' />
            ' <COUNTRY_MANGER_SUMMARY MIDTTOTAL_PMONTH='' MIDTTOTAL_P2MONTH='' MIDTTOTAL_P3MONTH='' MIDTTOTAL_P4MONTH='' PROJECTION_CUR_MONTH='' NETBOOKINGS_CUR_MONTH='' NETBOOKINGS_PREV_MONTH='' BIDT2_NETBOOKINGS='' SEGS_COMP_TO_HW='' SEGS_COMP_TO_LINES='' AVG_SEGSHW_SEGS='' AVG_SEGSLINE_SEGS='' AVE_SEGSBIDT_2='' MKTSHDIFF_1_2='' MKTSHDIFF_2_3='' MKTSHDIFF_2_4='' A_PREVPER='' A_PREV2PER='' A_PREV3PER='' A_PREV4PER='' A_PREV='' A_PREV2='' A_PREV3='' A_PREV4='' B_PREV='' B_PREV2='' B_PREV3='' B_PREV4='' G_PREV='' G_PREV2='' G_PREV3='' G_PREV4='' P_PREV='' P_PREV2='' P_PREV3='' P_PREV4='' W_PREV='' W_PREV2='' W_PREV3='' W_PREV4='' TARGET='' NO_OF_1A_PIV_HW='' NO_OF_1A_OLD_HW='' NO_OF_PTYPE_HW='' MISC='' />
            ' <COUNTRY_MANGER_TOTAL A_PREV='' A_PREV2='' A_PREV3='' A_PREV4='' B_PREV='' B_PREV2='' B_PREV3='' B_PREV4='' G_PREV='' G_PREV2='' G_PREV3='' G_PREV4='' P_PREV='' P_PREV2='' P_PREV3='' P_PREV4='' W_PREV='' W_PREV2='' W_PREV3='' W_PREV4=''/>
            ' <REPORT_HEADER STATUS='' REPORTDATE=''/>
            ' <Errors Status=''>
            ' <Error Code='' Description=''/>
            ' </Errors>
            '</PR_RPT_COUNTRY_MANGER_OUTPUT>

            objOutputXml = objCountryManager.Rpt_CountryManager(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objNodeReader = New XmlNodeReader(objOutputXml)
                dSet.ReadXml(objNodeReader)

                dsCommon = New DataSet()
                dsCommon = dSet

                'Showing Market Summary Information
                Dim objXmlNode, objXmlNodeClone As XmlNode
                objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("COUNTRY_MANGER")
                objXmlNodeClone = objXmlNode.CloneNode(True)
                For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                    XmlAttr.Value = ""
                Next

                For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                    For Each xmlAttSumary As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("COUNTRY_MANGER_TOTAL").Attributes
                        If XmlAttr.Name = xmlAttSumary.Name Then
                            XmlAttr.Value = xmlAttSumary.Value
                        End If
                    Next
                Next

                Dim objTempXml As New XmlDocument
                objTempXml.LoadXml("<PR_RPT_COUNTRY_MANGER_OUTPUT></PR_RPT_COUNTRY_MANGER_OUTPUT>")
                objTempXml.DocumentElement.AppendChild(objTempXml.ImportNode(objXmlNodeClone, True))
                dsMarketSum = New DataSet()
                Dim objMktReader As XmlNodeReader
                objMktReader = New XmlNodeReader(objTempXml)
                dsMarketSum.ReadXml(objMktReader)
               'End of Showing Market Summary Information

                grdCmr.DataSource = dSet.Tables("COUNTRY_MANGER").DefaultView
                grdCmr.DataBind()
                rowData.Visible = True
                lblError.Text = ""
               



                PagingCommon(objOutputXml)
                Dim intcol As Integer = GetSortColumnIndexMukund(grdCmr)
                If ViewState("Desc") = "FALSE" Then
                    grdCmr.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "TRUE" Then
                    grdCmr.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If
                pnlPaging.Visible = True
            Else
                dsCommon = Nothing
                dsMarketSum = Nothing
                grdCmr.DataSource = Nothing 'dSet.Tables("COUNTRY_MANGER").DefaultView
                grdCmr.DataBind()
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                rowData.Visible = False
            End If


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    'Protected Sub grdvDailyBookingsAll_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvDailyBookingsAll.RowDataBound
    '    Try
    '        If e.Row.RowType = DataControlRowType.Footer Then
    '            If FooterDs IsNot Nothing Then
    '                e.Row.Cells(11).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("TARGET").ToString()
    '                'e.Row.Cells(11).Text = hdTARGET.Value
    '                e.Row.Cells(12).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("TARGETPERDAY").ToString() 'hdTARGETPERDAY.Value
    '                e.Row.Cells(13).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("AverageBookings").ToString() 'hdAverageBookings.Value.Trim()

    '                e.Row.Cells(14).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Netbookings").ToString() ' hdNetbookings.Value

    '                e.Row.Cells(15).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Passive").ToString() ' hdNetbookings.Value
    '                e.Row.Cells(16).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("WithPassive").ToString() ' hdNetbookings.Value

    '                e.Row.Cells(17).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_Netbookings").ToString() 'hdCar_Netbookings.Value
    '                e.Row.Cells(18).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_Netbookings").ToString() ' hdHotel_Netbookings.Value


    '                Dim counter As Integer = 19
    '                Dim datecounter As Integer
    '                'Dim strDt As String = "D"
    '                'Dim strCar As String = "Car_D"
    '                'Dim strHotel As String = "Hotel_D"

    '                For datecounter = 1 To 31
    '                    e.Row.Cells(counter).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D" + datecounter.ToString()).ToString()
    '                    counter += 1
    '                    e.Row.Cells(counter).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D" + datecounter.ToString()).ToString()
    '                    counter += 1
    '                    e.Row.Cells(counter).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D" + datecounter.ToString()).ToString()
    '                    counter += 1
    '                Next

    '                e.Row.Cells(112).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Performance").ToString() ' hdPerformance.Value
    '            End If
    '        End If

    '        'If e.Row.RowIndex < 0 Then
    '        '    Exit Sub
    '        'End If
    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            If CType(e.Row.FindControl("lblLcode"), Label).Text.Trim().Length = 0 Then
    '                e.Row.Cells.Clear()
    '                Exit Sub
    '            End If
    '            'showDetails(lcode)
    '            'lnkDetails
    '            Dim lnkBtn As System.Web.UI.HtmlControls.HtmlAnchor
    '            lnkBtn = e.Row.FindControl("lnkDetails")

    '            Dim strl As String = CType(e.Row.FindControl("lblLcode"), Label).Text.Trim()

    '            'lnkBtn.Attributes.Add("onclick", "return showDetailsDAILYB('" + strl + "');")


    '            lnkBtn.Attributes.Add("onclick", "return showDetailsDAILYB('" & objED.Encrypt(strl) & "');")

    '            Dim intMont As Integer = drpMonth.SelectedIndex + 1
    '            If intMont = 2 Then
    '                If Convert.ToInt16(drpYear.SelectedValue) Mod 4 = 0 Then
    '                    grdvDailyBookingsAll.Columns(106).Visible = False
    '                    grdvDailyBookingsAll.Columns(107).Visible = False
    '                    grdvDailyBookingsAll.Columns(108).Visible = False
    '                    grdvDailyBookingsAll.Columns(109).Visible = False
    '                    grdvDailyBookingsAll.Columns(110).Visible = False
    '                    grdvDailyBookingsAll.Columns(111).Visible = False
    '                Else
    '                    'grdvDailyBookingsAll.Columns(101).Visible = False
    '                    'grdvDailyBookingsAll.Columns(102).Visible = False
    '                    grdvDailyBookingsAll.Columns(103).Visible = False
    '                    grdvDailyBookingsAll.Columns(104).Visible = False
    '                    grdvDailyBookingsAll.Columns(105).Visible = False
    '                    grdvDailyBookingsAll.Columns(106).Visible = False
    '                    grdvDailyBookingsAll.Columns(107).Visible = False
    '                    grdvDailyBookingsAll.Columns(108).Visible = False
    '                    grdvDailyBookingsAll.Columns(109).Visible = False
    '                    grdvDailyBookingsAll.Columns(110).Visible = False
    '                    grdvDailyBookingsAll.Columns(111).Visible = False
    '                End If

    '            End If
    '            If intMont <> 2 Then
    '                grdvDailyBookingsAll.Columns(103).Visible = True
    '                grdvDailyBookingsAll.Columns(104).Visible = True
    '                grdvDailyBookingsAll.Columns(105).Visible = True
    '                grdvDailyBookingsAll.Columns(106).Visible = True
    '                grdvDailyBookingsAll.Columns(107).Visible = True
    '                grdvDailyBookingsAll.Columns(108).Visible = True
    '                grdvDailyBookingsAll.Columns(109).Visible = True
    '                grdvDailyBookingsAll.Columns(110).Visible = True
    '                grdvDailyBookingsAll.Columns(111).Visible = True
    '            End If


    '            If intMont = 4 Or intMont = 6 Or intMont = 9 Or intMont = 11 Then
    '                grdvDailyBookingsAll.Columns(109).Visible = False
    '                grdvDailyBookingsAll.Columns(110).Visible = False
    '                grdvDailyBookingsAll.Columns(111).Visible = False
    '            ElseIf intMont = 1 Or intMont = 3 Or intMont = 5 Or intMont = 7 Or intMont = 8 Or intMont = 10 Or intMont = 12 Then
    '                grdvDailyBookingsAll.Columns(109).Visible = True
    '                grdvDailyBookingsAll.Columns(110).Visible = True
    '                grdvDailyBookingsAll.Columns(111).Visible = True
    '            End If
    '        End If
    '        grdvDailyBookingsAll.Columns(10).Visible = False
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '    End Try
    'End Sub

    'Protected Sub grdvDailyBookingsAll_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvDailyBookingsAll.RowCreated
    '    Try
    '        Dim grvRow As GridViewRow
    '        grvRow = e.Row
    '        Dim dtFormat As String
    '        dtFormat = (drpMonth.SelectedIndex + 1).ToString() & "/" & drpYear.SelectedItem.Text.Trim()
    '        Dim cellcounter As Integer = 19
    '        Dim datecounter As Integer
    '        If grvRow.RowType = DataControlRowType.Header Then

    '            If grdvDailyBookingsAll.AllowSorting = True Then
    '                If chkOriginalBk.Checked = True Then
    '                    CType(grvRow.Cells(16).Controls(0), LinkButton).Text = "Total Air"
    '                End If
    '            Else
    '                grvRow.Cells(16).Text = "Total Air"
    '            End If

    '            If grdvDailyBookingsAll.AllowSorting = True Then
    '                For datecounter = 1 To 31
    '                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = datecounter.ToString() & "/" & dtFormat
    '                    cellcounter += 1
    '                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = datecounter.ToString() & "/" & dtFormat & " Car"
    '                    cellcounter += 1
    '                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = datecounter.ToString() & "/" & dtFormat & " Hotel"
    '                    cellcounter += 1
    '                Next
    '            Else
    '                For datecounter = 1 To 31
    '                    grvRow.Cells(cellcounter).Text = datecounter.ToString() & "/" & dtFormat
    '                    cellcounter += 1
    '                    grvRow.Cells(cellcounter).Text = datecounter.ToString() & "/" & dtFormat & " Car"
    '                    cellcounter += 1
    '                    grvRow.Cells(cellcounter).Text = datecounter.ToString() & "/" & dtFormat & " Hotel"
    '                    cellcounter += 1
    '                Next
    '            End If

    '        End If
    '        grdvDailyBookingsAll.Columns(10).Visible = False

    '        If chkShowAddress.Checked = True Then
    '            grdvDailyBookingsAll.Columns(4).Visible = True
    '        Else
    '            grdvDailyBookingsAll.Columns(4).Visible = False
    '        End If
    '        If chkShowChaniCode.Checked = True Then
    '            grdvDailyBookingsAll.Columns(2).Visible = True
    '        Else
    '            grdvDailyBookingsAll.Columns(2).Visible = False
    '        End If
    '        '15,16
    '        If chkOriginalBk.Checked = False Then
    '            grdvDailyBookingsAll.Columns(15).Visible = False
    '            grdvDailyBookingsAll.Columns(16).Visible = False
    '        Else
    '            grdvDailyBookingsAll.Columns(15).Visible = True
    '            grdvDailyBookingsAll.Columns(16).Visible = True
    '        End If


    '        Dim intMont As Integer = drpMonth.SelectedIndex + 1
    '        If intMont = 2 Then
    '            If Convert.ToInt16(drpYear.SelectedValue) Mod 4 = 0 Then
    '                grdvDailyBookingsAll.Columns(106).Visible = False
    '                grdvDailyBookingsAll.Columns(107).Visible = False
    '                grdvDailyBookingsAll.Columns(108).Visible = False
    '                grdvDailyBookingsAll.Columns(109).Visible = False
    '                grdvDailyBookingsAll.Columns(110).Visible = False
    '                grdvDailyBookingsAll.Columns(111).Visible = False
    '            Else
    '                'grdvDailyBookingsAll.Columns(101).Visible = False
    '                'grdvDailyBookingsAll.Columns(102).Visible = False
    '                grdvDailyBookingsAll.Columns(103).Visible = False
    '                grdvDailyBookingsAll.Columns(104).Visible = False
    '                grdvDailyBookingsAll.Columns(105).Visible = False
    '                grdvDailyBookingsAll.Columns(106).Visible = False
    '                grdvDailyBookingsAll.Columns(107).Visible = False
    '                grdvDailyBookingsAll.Columns(108).Visible = False
    '                grdvDailyBookingsAll.Columns(109).Visible = False
    '                grdvDailyBookingsAll.Columns(110).Visible = False
    '                grdvDailyBookingsAll.Columns(111).Visible = False
    '            End If

    '        End If
    '        If intMont <> 2 Then
    '            grdvDailyBookingsAll.Columns(103).Visible = True
    '            grdvDailyBookingsAll.Columns(104).Visible = True
    '            grdvDailyBookingsAll.Columns(105).Visible = True
    '            grdvDailyBookingsAll.Columns(106).Visible = True
    '            grdvDailyBookingsAll.Columns(107).Visible = True
    '            grdvDailyBookingsAll.Columns(108).Visible = True
    '            grdvDailyBookingsAll.Columns(109).Visible = True
    '            grdvDailyBookingsAll.Columns(110).Visible = True
    '            grdvDailyBookingsAll.Columns(111).Visible = True
    '        End If


    '        If intMont = 4 Or intMont = 6 Or intMont = 9 Or intMont = 11 Then
    '            grdvDailyBookingsAll.Columns(109).Visible = False
    '            grdvDailyBookingsAll.Columns(110).Visible = False
    '            grdvDailyBookingsAll.Columns(111).Visible = False
    '        ElseIf intMont = 1 Or intMont = 3 Or intMont = 5 Or intMont = 7 Or intMont = 8 Or intMont = 10 Or intMont = 12 Then
    '            grdvDailyBookingsAll.Columns(109).Visible = True
    '            grdvDailyBookingsAll.Columns(110).Visible = True
    '            grdvDailyBookingsAll.Columns(111).Visible = True
    '        End If

    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '    End Try
    'End Sub
    

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            BindData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Function GetSortColumnIndexMukund(ByVal grd As GridView) As Int16
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Return grd.Columns.IndexOf(field)
            End If
        Next
        Return -1
    End Function
    Private Sub PagingCommon(ByVal objOutputXml As XmlDocument)
        pnlPaging.Visible = True
        Dim count As Integer = CInt(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
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
            'pnlPaging.Visible = False
            ' ddlPageNumber.Visible = False
            lnkNext.Visible = False
            lnkPrev.Visible = False
        Else
            'ddlPageNumber.Visible = True
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
        txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
    End Sub

#Region "Sort Function"
    Sub SortCall(ByVal SortName As String)
        AllNonDescColumnDefault()
        If ViewState("SortName") Is Nothing Then
            ViewState("SortName") = SortName
            ViewState("Desc") = "FALSE"
            If Not objDictionary.Contains(SortName) Then
                ViewState("Desc") = "TRUE"
            End If
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
                If Not objDictionary.Contains(SortName) Then
                    ViewState("Desc") = "TRUE"
                End If
            End If
        End If
        BindData()
    End Sub
#End Region

    Protected Sub grdCmr_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdCmr.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            BindData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            BindData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdCmr_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdCmr.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim hdMNC As HiddenField
                hdMNC = e.Row.FindControl("hdMNC")
                If hdMNC.Value = "Sole" Then
                    e.Row.BackColor = Drawing.Color.SkyBlue
                End If
                If hdMNC.Value = "Multi" Then
                    e.Row.BackColor = Drawing.Color.Orange
                End If
                If hdMNC.Value = "MNC" Then
                    e.Row.BackColor = Drawing.Color.Silver
                End If
               End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdCmr_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdCmr.RowCreated
        Try


            Dim grvRow As GridViewRow
            grvRow = e.Row
            Dim dtValue As String = Request.Form("txtDate").Trim().Split("/").GetValue(0)
            Dim monthval As Integer = Convert.ToInt32(Request.Form("txtDate").Trim().Split("/").GetValue(1))
            Dim yearval As Integer = Convert.ToInt32(Request.Form("txtDate").Trim().Split("/").GetValue(2))
            Dim lastMonth1 As Integer
            Dim lastMonth2 As Integer
            Dim lastMonth3 As Integer
            Dim lastMonth4 As Integer



            If monthval = 1 Then
                lastMonth1 = 12
            Else
                lastMonth1 = monthval - 1
            End If


            If monthval <= 2 Then
                If monthval = 2 Then
                    lastMonth2 = 12
                Else
                    lastMonth2 = 11
                End If
            Else
                lastMonth2 = monthval - 2
            End If


            If monthval <= 3 Then
                If monthval = 3 Then
                    lastMonth3 = 12
                ElseIf monthval = 2 Then
                    lastMonth3 = 11
                Else
                    lastMonth3 = 10
                End If
            Else
                lastMonth3 = monthval - 3
            End If


            If monthval <= 4 Then
                If monthval = 4 Then
                    lastMonth4 = 12
                ElseIf monthval = 3 Then
                    lastMonth4 = 11
                ElseIf monthval = 2 Then
                    lastMonth4 = 10
                Else
                    lastMonth4 = 9
                End If
            Else
                lastMonth4 = monthval - 4
            End If




            Dim cellcounter As Integer = 6
            If grvRow.RowType = DataControlRowType.Header Then
                If grdCmr.AllowSorting = True Then
                   CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "Total " + Left(MonthName(lastMonth1), 3)
                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "Total " + Left(MonthName(lastMonth2), 3)
                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "Total " + Left(MonthName(lastMonth3), 3)
                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "Total " + Left(MonthName(lastMonth4), 3)

                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "Proj.  " + MonthName(monthval)

                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "Motives till." + MonthName(monthval) + " " + dtValue

                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "Motives " + MonthName(lastMonth1)

                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "BIDT  " + MonthName(lastMonth2)

                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "Segments Comp to H/W"

                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "Segments Comp to lines"

                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "Avg. Seg-H/W Seg"

                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "Avg. Seg-Line Seg"

                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "Avg. Seg-BIDT " & Left(MonthName(lastMonth2), 3)






                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "Mkt. Sh" & Left(MonthName(lastMonth1), 3) & "-" & Left(MonthName(lastMonth2), 3)

                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "Mkt. Sh" & Left(MonthName(lastMonth2), 3) & "-" & Left(MonthName(lastMonth3), 3)

                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "Mkt. Sh" & Left(MonthName(lastMonth2), 3) & "-" & Left(MonthName(lastMonth4), 3)


                    'For Showing 1A in Percentage

                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "1A " & Left(MonthName(lastMonth1), 3)

                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "1A " & Left(MonthName(lastMonth2), 3)

                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "1A " & Left(MonthName(lastMonth3), 3)

                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "1A " & Left(MonthName(lastMonth4), 3)
                    'End of For Showing 1A in Percentage



                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "1A " & Left(MonthName(lastMonth1), 3)
                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "1A " & Left(MonthName(lastMonth2), 3)
                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "1A " & Left(MonthName(lastMonth3), 3)
                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "1A " & Left(MonthName(lastMonth4), 3)



                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "1B " & Left(MonthName(lastMonth1), 3)

                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "1B " & Left(MonthName(lastMonth2), 3)


                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "1B " & Left(MonthName(lastMonth3), 3)

                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "1B " & Left(MonthName(lastMonth4), 3)



                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "1G " & Left(MonthName(lastMonth1), 3)
                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "1G " & Left(MonthName(lastMonth2), 3)
                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "1G " & Left(MonthName(lastMonth3), 3)
                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "1G " & Left(MonthName(lastMonth4), 3)






                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "1P " & Left(MonthName(lastMonth1), 3)
                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "1P " & Left(MonthName(lastMonth2), 3)
                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "1P " & Left(MonthName(lastMonth3), 3)
                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "1P " & Left(MonthName(lastMonth4), 3)



                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "1W " & Left(MonthName(lastMonth1), 3)
                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "1W " & Left(MonthName(lastMonth2), 3)
                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "1W " & Left(MonthName(lastMonth3), 3)
                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "1W " & Left(MonthName(lastMonth4), 3)





                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "Target 1A " & Left(MonthName(lastMonth1), 3)


                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "No. of 1A PIV H/W"


                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "No. of 1A Old H/W"

                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "No. of P-type H/W"


                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "MISC"

                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "Sales Executive"

                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = "Station"

                Else

                    grvRow.Cells(cellcounter).Text = "Total " + Left(MonthName(lastMonth1), 3)
                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "Total " + Left(MonthName(lastMonth2), 3)
                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "Total " + Left(MonthName(lastMonth3), 3)
                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "Total " + Left(MonthName(lastMonth4), 3)

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "Proj.  " + MonthName(monthval)

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "Motives till." + MonthName(monthval) + " " + dtValue

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "Motives " + MonthName(lastMonth1)

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "BIDT  " + MonthName(lastMonth2)

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "Segments Comp to H/W"

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "Segments Comp to lines"

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "Avg. Seg-H/W Seg"

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "Avg. Seg-Line Seg"

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "Avg. Seg-BIDT " & Left(MonthName(lastMonth2), 3)






                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "Mkt. Sh" & Left(MonthName(lastMonth1), 3) & "-" & Left(MonthName(lastMonth2), 3)

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "Mkt. Sh" & Left(MonthName(lastMonth2), 3) & "-" & Left(MonthName(lastMonth3), 3)

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "Mkt. Sh" & Left(MonthName(lastMonth2), 3) & "-" & Left(MonthName(lastMonth4), 3)


                    'For Showing 1A in Percentage

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "1A " & Left(MonthName(lastMonth1), 3)

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "1A " & Left(MonthName(lastMonth2), 3)

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "1A " & Left(MonthName(lastMonth3), 3)

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "1A " & Left(MonthName(lastMonth4), 3)
                    'End of For Showing 1A in Percentage



                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "1A " & Left(MonthName(lastMonth1), 3)
                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "1A " & Left(MonthName(lastMonth2), 3)
                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "1A " & Left(MonthName(lastMonth3), 3)
                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "1A " & Left(MonthName(lastMonth4), 3)



                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "1B " & Left(MonthName(lastMonth1), 3)

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "1B " & Left(MonthName(lastMonth2), 3)


                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "1B " & Left(MonthName(lastMonth3), 3)

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "1B " & Left(MonthName(lastMonth4), 3)



                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "1G " & Left(MonthName(lastMonth1), 3)
                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "1G " & Left(MonthName(lastMonth2), 3)
                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "1G " & Left(MonthName(lastMonth3), 3)
                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "1G " & Left(MonthName(lastMonth4), 3)






                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "1P " & Left(MonthName(lastMonth1), 3)
                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "1P " & Left(MonthName(lastMonth2), 3)
                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "1P " & Left(MonthName(lastMonth3), 3)
                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "1P " & Left(MonthName(lastMonth4), 3)



                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "1W " & Left(MonthName(lastMonth1), 3)
                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "1W " & Left(MonthName(lastMonth2), 3)
                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "1W " & Left(MonthName(lastMonth3), 3)
                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "1W " & Left(MonthName(lastMonth4), 3)





                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "Target 1A " & Left(MonthName(lastMonth1), 3)

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "No. of 1A PIV H/W"


                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "No. of 1A Old H/W"

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "No. of P-type H/W"


                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "MISC"

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "Sales Executive"

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = "Station"

                End If

            End If

            ' <COUNTRY_MANGER_SUMMARY 
            'MIDTTOTAL_PMONTH="177" MIDTTOTAL_P2MONTH="408" MIDTTOTAL_P3MONTH="136" MIDTTOTAL_P4MONTH="640" PROJECTION_CUR_MONTH="217" 
            'NETBOOKINGS_CUR_MONTH="80" NETBOOKINGS_PREV_MONTH="44" BIDT2_NETBOOKINGS="402" SEGS_COMP_TO_HW="100" SEGS_COMP_TO_LINES="0" 
            'AVG_SEGSHW_SEGS="121" AVG_SEGSLINE_SEGS="0" AVE_SEGSBIDT_2="-272" MKTSHDIFF_1_2="-65" MKTSHDIFF_2_3="3" MKTSHDIFF_2_4="4" 
            'A_PREVPER="38" A_PREV2PER="103" A_PREV3PER="100" A_PREV4PER="99" A_PREV="67" A_PREV2="419" A_PREV3="136" A_PREV4="632" B_PREV="0" 
            'B_PREV2="0" B_PREV3="0" B_PREV4="0" G_PREV="110" G_PREV2="-11" G_PREV3="0" G_PREV4="8" P_PREV="0" P_PREV2="0" P_PREV3="0" P_PREV4="0" 
            'W_PREV="0" W_PREV2="0" W_PREV3="0" W_PREV4="0" 
            'TARGET="300" NO_OF_1A_PIV_HW="1" NO_OF_1A_OLD_HW="1" NO_OF_PTYPE_HW="2" MISC="0" />

            ' <COUNTRY_MANGER_TOTAL 
            'A_PREV='' A_PREV2='' A_PREV3='' A_PREV4='' 
            'B_PREV='' B_PREV2='' B_PREV3='' B_PREV4='' 
            'G_PREV='' G_PREV2='' G_PREV3='' G_PREV4='' 
            'P_PREV='' P_PREV2='' P_PREV3='' P_PREV4='' 
            'W_PREV='' W_PREV2='' W_PREV3='' W_PREV4=''/>
            If e.Row.RowType = DataControlRowType.Footer Then

                Dim valSummary As String = ""
                Dim valMkt As String = ""

                Dim strHtml As String = ""
                strHtml = "<table><tr><td>" & valSummary & "</td></tr> <tr style='color:Black;' ><td>" & valMkt & "</td></tr></table>"
                cellcounter = 5
                If dsCommon IsNot Nothing Then
                    grvRow.Cells(cellcounter).Text = "Total"
                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("MIDTTOTAL_PMONTH")

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("MIDTTOTAL_P2MONTH")

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("MIDTTOTAL_P3MONTH")
                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("MIDTTOTAL_P4MONTH")

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("PROJECTION_CUR_MONTH")

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("NETBOOKINGS_CUR_MONTH")

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("NETBOOKINGS_PREV_MONTH")

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("BIDT2_NETBOOKINGS")

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("SEGS_COMP_TO_HW")

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("SEGS_COMP_TO_LINES")

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("AVG_SEGSHW_SEGS")

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("AVG_SEGSLINE_SEGS")

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("AVE_SEGSBIDT_2")






                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("MKTSHDIFF_1_2")

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("MKTSHDIFF_2_3")

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("MKTSHDIFF_2_4")


                    'For Showing 1A in Percentage

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("A_PREVPER")

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("A_PREV2PER")

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("A_PREV3PER")

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("A_PREV4PER")
                    'End of For Showing 1A in Percentage



                    cellcounter += 1
                    valSummary = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("A_PREV")
                    valMkt = dsMarketSum.Tables("COUNTRY_MANGER").Rows(0)("A_PREV")
                    strHtml = "<table><tr><td>" & valSummary & "</td></tr> <tr style='color:Black;' ><td>" & valMkt & "</td></tr></table>"
                    grvRow.Cells(cellcounter).Text = strHtml 'dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("A_PREV")


                    cellcounter += 1
                    valSummary = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("A_PREV2")
                    valMkt = dsMarketSum.Tables("COUNTRY_MANGER").Rows(0)("A_PREV2")
                    strHtml = "<table><tr><td>" & valSummary & "</td></tr> <tr style='color:Black;' ><td>" & valMkt & "</td></tr></table>"
                    grvRow.Cells(cellcounter).Text = strHtml 'dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("A_PREV2")

                    cellcounter += 1
                    valSummary = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("A_PREV3")
                    valMkt = dsMarketSum.Tables("COUNTRY_MANGER").Rows(0)("A_PREV3")
                    strHtml = "<table><tr><td>" & valSummary & "</td></tr> <tr style='color:Black;' ><td>" & valMkt & "</td></tr></table>"
                    grvRow.Cells(cellcounter).Text = strHtml 'dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("A_PREV3")


                    cellcounter += 1
                    valSummary = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("A_PREV4")
                    valMkt = dsMarketSum.Tables("COUNTRY_MANGER").Rows(0)("A_PREV4")
                    strHtml = "<table><tr><td>" & valSummary & "</td></tr> <tr style='color:Black;' ><td>" & valMkt & "</td></tr></table>"
                    grvRow.Cells(cellcounter).Text = strHtml 'dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("A_PREV4")



                    cellcounter += 1
                    valSummary = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("B_PREV")
                    valMkt = dsMarketSum.Tables("COUNTRY_MANGER").Rows(0)("B_PREV")
                    strHtml = "<table><tr><td>" & valSummary & "</td></tr> <tr style='color:Black;' ><td>" & valMkt & "</td></tr></table>"
                    grvRow.Cells(cellcounter).Text = strHtml 'dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("B_PREV")

                    cellcounter += 1
                    valSummary = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("B_PREV2")
                    valMkt = dsMarketSum.Tables("COUNTRY_MANGER").Rows(0)("B_PREV2")
                    strHtml = "<table><tr><td>" & valSummary & "</td></tr> <tr style='color:Black;' ><td>" & valMkt & "</td></tr></table>"
                    grvRow.Cells(cellcounter).Text = strHtml 'dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("B_PREV2")


                    cellcounter += 1
                    valSummary = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("B_PREV3")
                    valMkt = dsMarketSum.Tables("COUNTRY_MANGER").Rows(0)("B_PREV3")
                    strHtml = "<table><tr><td>" & valSummary & "</td></tr> <tr style='color:Black;' ><td>" & valMkt & "</td></tr></table>"
                    grvRow.Cells(cellcounter).Text = strHtml 'dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("B_PREV3")

                    cellcounter += 1
                    valSummary = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("B_PREV4")
                    valMkt = dsMarketSum.Tables("COUNTRY_MANGER").Rows(0)("B_PREV4")
                    strHtml = "<table><tr><td>" & valSummary & "</td></tr> <tr style='color:Black;' ><td>" & valMkt & "</td></tr></table>"
                    grvRow.Cells(cellcounter).Text = strHtml 'dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("B_PREV4")



                    cellcounter += 1
                    valSummary = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("G_PREV")
                    valMkt = dsMarketSum.Tables("COUNTRY_MANGER").Rows(0)("G_PREV")
                    strHtml = "<table><tr><td>" & valSummary & "</td></tr> <tr style='color:Black;' ><td>" & valMkt & "</td></tr></table>"
                    grvRow.Cells(cellcounter).Text = strHtml 'dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("G_PREV")

                    cellcounter += 1
                    valSummary = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("G_PREV2")
                    valMkt = dsMarketSum.Tables("COUNTRY_MANGER").Rows(0)("G_PREV2")
                    strHtml = "<table><tr><td>" & valSummary & "</td></tr> <tr style='color:Black;' ><td>" & valMkt & "</td></tr></table>"
                    grvRow.Cells(cellcounter).Text = strHtml 'dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("G_PREV2")

                    cellcounter += 1
                    valSummary = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("G_PREV3")
                    valMkt = dsMarketSum.Tables("COUNTRY_MANGER").Rows(0)("G_PREV3")
                    strHtml = "<table><tr><td>" & valSummary & "</td></tr> <tr style='color:Black;' ><td>" & valMkt & "</td></tr></table>"
                    grvRow.Cells(cellcounter).Text = strHtml 'dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("G_PREV3")

                    cellcounter += 1
                    valSummary = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("G_PREV4")
                    valMkt = dsMarketSum.Tables("COUNTRY_MANGER").Rows(0)("G_PREV4")
                    strHtml = "<table><tr><td>" & valSummary & "</td></tr> <tr style='color:Black;' ><td>" & valMkt & "</td></tr></table>"
                    grvRow.Cells(cellcounter).Text = strHtml 'dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("G_PREV4")





                    '1p
                    cellcounter += 1
                    valSummary = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("P_PREV")
                    valMkt = dsMarketSum.Tables("COUNTRY_MANGER").Rows(0)("P_PREV")
                    strHtml = "<table><tr><td>" & valSummary & "</td></tr> <tr style='color:Black;' ><td>" & valMkt & "</td></tr></table>"
                    grvRow.Cells(cellcounter).Text = strHtml 'dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("P_PREV")

                    cellcounter += 1
                    valSummary = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("P_PREV2")
                    valMkt = dsMarketSum.Tables("COUNTRY_MANGER").Rows(0)("P_PREV2")
                    strHtml = "<table><tr><td>" & valSummary & "</td></tr> <tr style='color:Black;' ><td>" & valMkt & "</td></tr></table>"
                    grvRow.Cells(cellcounter).Text = strHtml 'dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("P_PREV2")

                    cellcounter += 1
                    valSummary = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("P_PREV3")
                    valMkt = dsMarketSum.Tables("COUNTRY_MANGER").Rows(0)("P_PREV3")
                    strHtml = "<table><tr><td>" & valSummary & "</td></tr> <tr style='color:Black;' ><td>" & valMkt & "</td></tr></table>"
                    grvRow.Cells(cellcounter).Text = strHtml 'dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("P_PREV3")

                    cellcounter += 1
                    valSummary = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("P_PREV4")
                    valMkt = dsMarketSum.Tables("COUNTRY_MANGER").Rows(0)("P_PREV4")
                    strHtml = "<table><tr><td>" & valSummary & "</td></tr> <tr style='color:Black;' ><td>" & valMkt & "</td></tr></table>"
                    grvRow.Cells(cellcounter).Text = strHtml 'dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("P_PREV4")


                    '1W
                    cellcounter += 1
                    valSummary = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("W_PREV")
                    valMkt = dsMarketSum.Tables("COUNTRY_MANGER").Rows(0)("W_PREV")
                    strHtml = "<table><tr><td>" & valSummary & "</td></tr> <tr style='color:Black;' ><td>" & valMkt & "</td></tr></table>"
                    grvRow.Cells(cellcounter).Text = strHtml 'dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("W_PREV")

                    cellcounter += 1
                    valSummary = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("W_PREV2")
                    valMkt = dsMarketSum.Tables("COUNTRY_MANGER").Rows(0)("W_PREV2")
                    strHtml = "<table><tr><td>" & valSummary & "</td></tr> <tr style='color:Black;' ><td>" & valMkt & "</td></tr></table>"
                    grvRow.Cells(cellcounter).Text = strHtml 'dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("W_PREV2")

                    cellcounter += 1
                    valSummary = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("W_PREV3")
                    valMkt = dsMarketSum.Tables("COUNTRY_MANGER").Rows(0)("W_PREV3")
                    strHtml = "<table><tr><td>" & valSummary & "</td></tr> <tr style='color:Black;' ><td>" & valMkt & "</td></tr></table>"
                    grvRow.Cells(cellcounter).Text = strHtml 'dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("W_PREV3")

                    cellcounter += 1
                    valSummary = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("W_PREV4")
                    valMkt = dsMarketSum.Tables("COUNTRY_MANGER").Rows(0)("W_PREV4")
                    strHtml = "<table><tr><td>" & valSummary & "</td></tr> <tr style='color:Black;' ><td>" & valMkt & "</td></tr></table>"
                    grvRow.Cells(cellcounter).Text = strHtml ' dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("W_PREV4")





                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("TARGET")

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("NO_OF_1A_PIV_HW")


                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("NO_OF_1A_OLD_HW")

                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("NO_OF_PTYPE_HW")


                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = dsCommon.Tables("COUNTRY_MANGER_SUMMARY").Rows(0)("MISC")

                    'cellcounter += 1
                    'grvRow.Cells(cellcounter).Text = "Sales Executive"

                    'cellcounter += 1
                    'grvRow.Cells(cellcounter).Text = "Station"
                End If
            End If

            If dsCommon IsNot Nothing Then
                If dsCommon.Tables("REPORT_HEADER").Rows(0)("PMONTH_MIDT").ToString().ToUpper() <> "TRUE" Then
                    grvRow.Cells(6).Visible = False
                    grvRow.Cells(22).Visible = False
                    grvRow.Cells(26).Visible = False
                    grvRow.Cells(30).Visible = False
                    grvRow.Cells(34).Visible = False
                    grvRow.Cells(38).Visible = False
                    grvRow.Cells(42).Visible = False
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub
  
End Class
