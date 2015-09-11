Partial Class TravelAgency_TAUP_AgencyCompetition
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As StringBuilder
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
            Session("PageName") = Request.Url.ToString()
            objeAAMS.ExpirePageCache()
            lblError.Text = String.Empty
            btnAdd.Text = "Add"
            drpCRSCode.Focus()
            btnAdd.Attributes.Add("onclick", "return ValidationCompetitionManageComp2();")
            '***************************************************************************************
            'Code of Security Check
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

          

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Competition Details']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Competition Details']").Attributes("Value").Value)

                    If strBuilder(1) = "0" Then
                        btnAdd.Enabled = False
                    End If

                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                    End If


                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            ' #######################################
            ' ########## End of code used for enable/disable 
            ' ########## the button according to rights
            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(drpCRSCode, "CRSCODEWITHTEXT", True, 1)
                'objeAAMS.BindDropDown(drpOnlineStatus, "OS", True)
                ' objeAAMS.BindDropDown(drpOnlineStatus, "ONLINESTATUS", True)
                objeAAMS.BindDropDown(drpOnlineStatus, "ONLINESTATUSCODE", True, 2)

              

                Session("AgencyComp") = Nothing

                If Request.QueryString("SalesVisit") IsNot Nothing Then
                    'Dim rep As Repeater = CType(MenuControl1.FindControl("theTabStrip"), Repeater)
                    'For i As Integer = 0 To rep.Items.Count - 1
                    'Next
                    Session("Action") = Request.QueryString("Action").ToString
                    MenuControl1.Visible = False
                End If

                If (Session("Action") IsNot Nothing) Then
                    If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                        ' If strBuilder(0) = "1" Then
                        AgencyCompetitionView()
                        'End If

                    End If
                End If

            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    '*********************************************************************************************************
    '****************************Method for View Agency *****************************************************
    '*********************************************************************************************************
    Private Sub AgencyCompetitionView()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim dv As DataView
        Dim Rowno As Integer
        Try

            ' #######################################
            ' ########## This code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Competition Details']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Competition Details']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Exit Sub
                    End If
                End If
            End If


            objInputXml.LoadXml("<UP_GETCOMPETITIONDETAILS_INPUT><LOCATION_CODE></LOCATION_CODE></UP_GETCOMPETITIONDETAILS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerXml = Session("Action").ToString().Split("|").GetValue(1)
            'Here Back end Method Call
            'objOutputXml = objbzAgency.View(objInputXml)
            objOutputXml = objbzAgency.GetAgencyCompetitionDetails(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                Dim objIAppendXml, objOAppendXml As New XmlDocument
                Dim objIXml As New XmlDocument
                Dim objxmldocfrag As XmlDocumentFragment
                Dim objNode As XmlNode
                Dim objNodeList As XmlNodeList

                objIAppendXml.LoadXml("<Document><COMPETITION_DETAILS ACTION ='' ComptID='' LOCATION_CODE ='' CRSID='' DATE_END='' DATE_START='' SOLE_USER='' DIAL_BACKUP='' ONLINESTATUSID='' PRINTER_COUNT='' PC_COUNT='' CommercialDetails=''  /></Document>")
                If Session("AgencyComp") Is Nothing Then
                    objOAppendXml.LoadXml("<UP_UPDATECOMPETITIONDETAILS_INPUT></UP_UPDATECOMPETITIONDETAILS_INPUT>")
                Else
                    objOAppendXml.LoadXml(Convert.ToString(Session("AgencyComp")))
                End If
                objNodeList = objOutputXml.DocumentElement.SelectNodes("COMPETITION_DETAILS")
                Rowno = 1
                For Each objNode In objNodeList
                    'If objIAppendXml.DocumentElement.SelectNodes("COMPETITION_DETAILS").Count = 0 Then
                    '    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("ComptID").Value = 1
                    'Else
                    '    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("ComptID").Value = objInputXml.DocumentElement.SelectNodes("COMPETITION_DETAILS").Item(objInputXml.DocumentElement.SelectNodes("COMPETITION_DETAILS").Count - 1).Attributes("ComptID").Value + 1
                    'End If
                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("ComptID").Value = Rowno
                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("CRSID").Value = objNode.Attributes("CRSID").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("DATE_END").Value = objeAAMS.ConvertDateBlank(objNode.Attributes("DATE_END").Value)
                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("DATE_START").Value = objeAAMS.ConvertDateBlank(objNode.Attributes("DATE_START").Value)
                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("SOLE_USER").Value = objNode.Attributes("SOLE_USER").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("DIAL_BACKUP").Value = objNode.Attributes("DIAL_BACKUP").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("ONLINESTATUSID").Value = objNode.Attributes("ONLINESTATUSID").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("PRINTER_COUNT").Value = objNode.Attributes("PRINTER_COUNT").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("PC_COUNT").Value = objNode.Attributes("PC_COUNT").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("ACTION").Value = "U"
                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("LOCATION_CODE").Value = Session("Action").ToString().Split("|").GetValue(1)

                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("CommercialDetails").Value = objNode.Attributes("CommercialDetails").Value


                    objxmldocfrag = objOAppendXml.CreateDocumentFragment()
                    objxmldocfrag.InnerXml = objIAppendXml.DocumentElement.InnerXml
                    objOAppendXml.DocumentElement.AppendChild(objxmldocfrag)
                    Rowno += 1
                Next
                Session("AgencyComp") = objOAppendXml.OuterXml

                objXmlReader = New XmlNodeReader(objOAppendXml)
                'objXmlReader = New XmlNodeReader(objOutputXml)

                ds.ReadXml(objXmlReader)
                dv = ds.Tables("COMPETITION_DETAILS").DefaultView
                dv.RowFilter = "Action<>'D'"

                '@ Code For Sorting
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "CRSID"
                End If
                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                End If

                If ViewState("Desc") = "TRUE" Then
                    dv.Sort = ViewState("SortName").ToString + "    DESC "
                Else
                    dv.Sort = ViewState("SortName").ToString + "    ASC "
                End If
                '@ End of Code For Sorting

                '  dv.Sort = "ComptID"
                grdComptitionAgency.DataSource = dv
                '   grdComptitionAgency.DataSource = ds.Tables("COMPETITION_DETAILS")
                grdComptitionAgency.DataBind()
                SetImageForSorting(grdComptitionAgency)
            Else
                grdComptitionAgency.DataSource = String.Empty
                grdComptitionAgency.DataBind()
                'lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            objbzAgency = Nothing
            ds = Nothing
        End Try
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim objInputXml As New XmlDocument
        Dim objIxml As New XmlDocument
        Dim objxmldocfrag As XmlDocumentFragment
        Dim objXmlReader As XmlReader
        Dim ds As New DataSet
        Dim dv As DataView
        Dim objXMLNode, objNode As XmlNode
        Dim Action As String
        Dim Rowno As Integer
        Dim Crscode As String
        Try
            If (drpCRSCode.Enabled = True) Then  ' Only in Add Mode
                For Rowno = 0 To grdComptitionAgency.Rows.Count - 1
                    Crscode = CType(grdComptitionAgency.Rows(Rowno).FindControl("lblCRSID"), Label).Text
                    If (Crscode = drpCRSCode.SelectedValue) Then
                        lblError.Text = "You have not right to add more than one CRS Code under an agency. "
                        Exit Sub
                    End If
                Next
            End If

            Action = "I"
            objIxml.LoadXml("<Document><COMPETITION_DETAILS LOCATION_CODE='' ComptID='' CRSID='' DATE_END='' DATE_START='' SOLE_USER='' DIAL_BACKUP='' ONLINESTATUSID='' PRINTER_COUNT='' PC_COUNT='' CommercialDetails=''  ACTION ='' /></Document>")
            If Session("AgencyComp") Is Nothing Then
                objInputXml.LoadXml("<UP_UPDATECOMPETITIONDETAILS_INPUT></UP_UPDATECOMPETITIONDETAILS_INPUT>")
            Else
                objInputXml.LoadXml(Convert.ToString(Session("AgencyComp")))
            End If
            If Not ViewState("ComptID") Is Nothing Then
                objXMLNode = objInputXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS[@ComptID='" & ViewState("ComptID") & "']")
                If (objInputXml.DocumentElement.SelectNodes("COMPETITION_DETAILS[@ComptID='" & ViewState("ComptID") & "']").Count > 0) Then
                    Action = objInputXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS[@ComptID='" & ViewState("ComptID") & "']").Attributes("ACTION").Value
                End If
                objInputXml.DocumentElement.RemoveChild(objXMLNode)
            End If


            'If objInputXml.DocumentElement.SelectNodes("COMPETITION_DETAILS").Count = 0 Then
            '    objIxml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("ComptID").Value = 1
            'Else
            '    objIxml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("ComptID").Value = objInputXml.DocumentElement.SelectNodes("COMPETITION_DETAILS").Item(objInputXml.DocumentElement.SelectNodes("COMPETITION_DETAILS").Count - 1).Attributes("ComptID").Value + 1
            'End If
            objIxml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("LOCATION_CODE").Value = Session("Action").ToString().Split("|").GetValue(1)
            objIxml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("CRSID").Value = drpCRSCode.SelectedValue
            objIxml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("DATE_END").Value = txtDateEnd.Text
            objIxml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("DATE_START").Value = txtDateStart.Text
            objIxml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("SOLE_USER").Value = drpSoleUser.SelectedValue

            objIxml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("DIAL_BACKUP").Value = drpDialBackup.SelectedValue
            objIxml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("ONLINESTATUSID").Value = drpOnlineStatus.SelectedValue
            objIxml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("PRINTER_COUNT").Value = txtPrinterCount.Text
            objIxml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("PC_COUNT").Value = txtPCCount.Text
            objIxml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("ACTION").Value = Action
            objIxml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("CommercialDetails").Value = TxtCommercialDetail.Text

            If Not ViewState("ComptID") Is Nothing Then
                ViewState("ComptID") = Nothing
            End If
            If Not ViewState("CRSID") Is Nothing Then
                ViewState("CRSID") = Nothing
            End If
            objxmldocfrag = objInputXml.CreateDocumentFragment()
            objxmldocfrag.InnerXml = objIxml.DocumentElement.InnerXml
            objInputXml.DocumentElement.AppendChild(objxmldocfrag)

            Rowno = 1
            For Each objNode In objInputXml.DocumentElement.SelectNodes("COMPETITION_DETAILS")
                ' objIAppendXml.DocumentElement.SelectSingleNode("IPAddress").Attributes("IP").Value = objNode.Attributes("IP").Value
                objNode.Attributes("ComptID").Value = Rowno
                Rowno += 1
            Next


            Session("AgencyComp") = objInputXml.OuterXml

            objXmlReader = New XmlNodeReader(objInputXml)
            ds.ReadXml(objXmlReader)
            dv = ds.Tables("COMPETITION_DETAILS").DefaultView
            dv.RowFilter = "Action<>'D'"

            '@ Code For Sorting
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "CRSID"
            End If
            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
            End If

            If ViewState("Desc") = "TRUE" Then
                dv.Sort = ViewState("SortName").ToString + "    DESC "
            Else
                dv.Sort = ViewState("SortName").ToString + "    ASC "
            End If
            '@ End of Code For Sorting

            'dv.Sort = "ComptID"


            grdComptitionAgency.DataSource = dv 'ds.Tables("COMPETITION_DETAILS[@Action!='D']")
            grdComptitionAgency.DataBind()
            AgencyComtitionReset()
            drpCRSCode.Enabled = True
            drpCRSCode.CssClass = "dropdownlist"

            SetImageForSorting(grdComptitionAgency)
            'set
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub AgencyComtitionReset()
        Try
            drpCRSCode.SelectedIndex = 0
            drpOnlineStatus.SelectedIndex = 0
            txtDateStart.Text = String.Empty
            txtDateEnd.Text = String.Empty
            drpDialBackup.SelectedIndex = 0
            drpSoleUser.SelectedIndex = 0
            txtPCCount.Text = String.Empty
            txtPrinterCount.Text = String.Empty
            TxtCommercialDetail.Text = String.Empty
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objIAppendXml, objOAppendXml As New XmlDocument
        Dim objIXml As New XmlDocument
        Dim objxmldocfrag As XmlDocumentFragment
        Dim objNode As XmlNode
        Dim objNodeList As XmlNodeList
        Dim objbzAgencyComp As New AAMS.bizTravelAgency.bzAgencyCompetition
        Try
            If Not Session("AgencyComp") Is Nothing Then
                objInputXml.LoadXml(Session("AgencyComp"))

                objIAppendXml.LoadXml("<Document><COMPETITION_DETAILS ACTION =''  LOCATION_CODE ='' CRSID='' DATE_END='' DATE_START='' SOLE_USER='' DIAL_BACKUP='' ONLINESTATUSID='' PRINTER_COUNT='' PC_COUNT='' CommercialDetails=''  /></Document>")

                objOAppendXml.LoadXml("<UP_UPDATECOMPETITIONDETAILS_INPUT></UP_UPDATECOMPETITIONDETAILS_INPUT>")

                objNodeList = objInputXml.DocumentElement.SelectNodes("COMPETITION_DETAILS")

                For Each objNode In objNodeList
                    ' objIAppendXml.DocumentElement.SelectSingleNode("IPAddress").Attributes("IP").Value = objNode.Attributes("IP").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("LOCATION_CODE").Value = objNode.Attributes("LOCATION_CODE").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("CRSID").Value = objNode.Attributes("CRSID").Value

                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("DATE_END").Value = objeAAMS.ConvertTextDate(objNode.Attributes("DATE_END").Value)
                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("DATE_START").Value = objeAAMS.ConvertTextDate(objNode.Attributes("DATE_START").Value)
                    If (objNode.Attributes("SOLE_USER").Value = "True") Then
                        objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("SOLE_USER").Value = "1" ' objNode.Attributes("SOLE_USER").Value
                    Else
                        objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("SOLE_USER").Value = "0" ' objNode.Attributes("SOLE_USER").Value
                    End If
                    If (objNode.Attributes("DIAL_BACKUP").Value = "True") Then
                        objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("DIAL_BACKUP").Value = "1" 'objNode.Attributes("DIAL_BACKUP").Value
                    Else
                        objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("DIAL_BACKUP").Value = "0" ' objNode.Attributes("DIAL_BACKUP").Value
                    End If

                    If (objNode.Attributes("PRINTER_COUNT").Value = "") Then
                        objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("PRINTER_COUNT").Value = "0"
                    Else
                        objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("PRINTER_COUNT").Value = objNode.Attributes("PRINTER_COUNT").Value
                    End If
                    If (objNode.Attributes("PC_COUNT").Value = "") Then
                        objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("PC_COUNT").Value = "0"
                    Else
                        objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("PC_COUNT").Value = objNode.Attributes("PC_COUNT").Value
                    End If

                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("ONLINESTATUSID").Value = objNode.Attributes("ONLINESTATUSID").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("ACTION").Value = objNode.Attributes("ACTION").Value

                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("CommercialDetails").Value = objNode.Attributes("CommercialDetails").Value

                    objxmldocfrag = objOAppendXml.CreateDocumentFragment()
                    objxmldocfrag.InnerXml = objIAppendXml.DocumentElement.InnerXml
                    objOAppendXml.DocumentElement.AppendChild(objxmldocfrag)

                Next

                'Here Back end Method Call
                'objOutputXml = objbzEmployee.Update(objInputXml)
                objOutputXml = objbzAgencyComp.Update(objOAppendXml)

                'Here Back end Method Call
                ' objOutputXml = objbzAgencyComp.AssignIP(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                        lblError.Text = objeAAMSMessage.messUpdate
                    Else
                        lblError.Text = objeAAMSMessage.messInsert
                    End If
                    Session("AgencyComp") = Nothing
                    AgencyCompetitionView()

                    If Request.QueryString("SalesVisit") IsNot Nothing Then
                        Dim count As String = objIAppendXml.DocumentElement.SelectNodes("COMPETITION_DETAILS[@ACTION!= 'D']").Count.ToString
                        ClientScript.RegisterStartupScript(Me.GetType(), "close", "window.parent.document.getElementById('HdCompetionCount').value=" + count + "; window.parent.document.getElementById('iframeID').src='';window.parent.document.getElementById('BtnCancel').click();", True)
                    End If

                    btnAdd.Text = "Add"
                    AgencyComtitionReset()

                    ViewState("ComptID") = Nothing
                    ViewState("CRSID") = Nothing
                    drpCRSCode.Enabled = True
                    drpCRSCode.CssClass = ""

                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If


            Else
                lblError.Text = "Please add atleast one Competition."
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString

        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            objbzAgencyComp = Nothing
        End Try
    End Sub


    Private Sub AgencyEdit(ByVal strComption As String)
        Dim objInputXml As New XmlDocument
        Try
            If Not Session("AgencyComp") Is Nothing Then
                objInputXml.LoadXml(Convert.ToString(Session("AgencyComp")))
                With objInputXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS[@ComptID='" & strComption & "']")
                    drpCRSCode.SelectedValue = .Attributes("CRSID").Value
                    drpOnlineStatus.SelectedValue = .Attributes("ONLINESTATUSID").Value
                    txtDateStart.Text = .Attributes("DATE_START").Value
                    txtDateEnd.Text = .Attributes("DATE_END").Value
                    drpDialBackup.SelectedValue = .Attributes("DIAL_BACKUP").Value
                    drpSoleUser.SelectedValue = .Attributes("SOLE_USER").Value
                    txtPCCount.Text = .Attributes("PC_COUNT").Value
                    txtPrinterCount.Text = .Attributes("PRINTER_COUNT").Value

                    TxtCommercialDetail.Text = .Attributes("CommercialDetails").Value

                    ViewState("ComptID") = .Attributes("ComptID").Value
                    ViewState("CRSID") = .Attributes("CRSID").Value
                    drpCRSCode.Enabled = False
                    drpCRSCode.CssClass = "textboxgrey"
                    btnAdd.Text = "Update"
                End With
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub AgencyDelete(ByVal strComption As String)
        Dim objInputXml As New XmlDocument
        Dim objXmlReader As XmlReader
        Dim ds As New DataSet
        Dim Rowno As Integer
        Dim objNode As XmlNode
        Dim dv As DataView
        Try
            If Not Session("AgencyComp") Is Nothing Then
                objInputXml.LoadXml(Convert.ToString(Session("AgencyComp")))

                If (objInputXml.DocumentElement.SelectNodes("COMPETITION_DETAILS[@ComptID='" & strComption & "' and @ACTION='I']").Count > 0) Then
                    'If (objInputXml.DocumentElement.SelectNodes("CRS[@TRN='" & strRN1 & "']").Count > 0) Then
                    objInputXml.DocumentElement.RemoveChild(objInputXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS[@ComptID='" & strComption & "' and @ACTION='I']"))
                    Rowno = 1
                    For Each objNode In objInputXml.DocumentElement.SelectNodes("COMPETITION_DETAILS")
                        ' objIAppendXml.DocumentElement.SelectSingleNode("IPAddress").Attributes("IP").Value = objNode.Attributes("IP").Value
                        objNode.Attributes("ComptID").Value = Rowno
                        Rowno += 1
                    Next
                Else
                    With objInputXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS[@ComptID='" & strComption & "']")
                        objInputXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS[@ComptID='" & strComption & "']").Attributes("ACTION").Value = "D"
                    End With
                    'End If
                End If

                If objInputXml.DocumentElement.SelectNodes("COMPETITION_DETAILS").Count > 0 Then
                    objXmlReader = New XmlNodeReader(objInputXml)
                    ds.ReadXml(objXmlReader)
                    dv = ds.Tables("COMPETITION_DETAILS").DefaultView
                    dv.RowFilter = "Action<>'D'"
                    '  dv.Sort = "ComptID"

                    '@ Code For Sorting
                    If ViewState("SortName") Is Nothing Then
                        ViewState("SortName") = "CRSID"
                    End If
                    If ViewState("Desc") Is Nothing Then
                        ViewState("Desc") = "FALSE"
                    End If

                    If ViewState("Desc") = "TRUE" Then
                        dv.Sort = ViewState("SortName").ToString + "    DESC "
                    Else
                        dv.Sort = ViewState("SortName").ToString + "    ASC "
                    End If
                    '@ End of Code For Sorting

                    grdComptitionAgency.DataSource = dv 'ds.Tables("COMPETITION_DETAILS")
                    grdComptitionAgency.DataBind()
                Else
                    grdComptitionAgency.DataSource = Nothing
                    grdComptitionAgency.DataBind()
                End If

                Session("AgencyComp") = objInputXml.OuterXml
                ViewState("ComptID") = Nothing
                ViewState("CRSID") = Nothing
                AgencyComtitionReset()
                drpCRSCode.Enabled = True
                drpCRSCode.CssClass = ""
                btnAdd.Text = "Add"
                SetImageForSorting(grdComptitionAgency)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            'AgencyComtitionReset()
            'Session("AgencyComp") = Nothing
            'ViewState("ComptID") = Nothing
            'ViewState("CRSID") = Nothing
            'drpCRSCode.Enabled = True
            'drpCRSCode.CssClass = "dropdown"
            'AgencyCompetitionView()
            Response.Redirect(Request.Url.ToString, False)
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub



    Protected Sub grdComptitionAgency_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdComptitionAgency.Sorted

    End Sub

    Protected Sub grdComptitionAgency_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdComptitionAgency.Sorting
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAgencyCRSUse As New AAMS.bizTravelAgency.bzAgency
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim SortName As String
        Dim dv As DataView
        Try
            SortName = e.SortExpression

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
            grdComptitionAgency.DataSource = Nothing
            grdComptitionAgency.DataBind()
            If Session("AgencyComp") IsNot Nothing Then
                objOutputXml.LoadXml(Convert.ToString(Session("AgencyComp")))


                objXmlReader = New XmlNodeReader(objOutputXml) 'New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)

                dv = ds.Tables("COMPETITION_DETAILS").DefaultView
                dv.RowFilter = "Action<>'D'"

                '@ Code For Sorting
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "CRSID"
                End If
                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                End If

                If ViewState("Desc") = "TRUE" Then
                    dv.Sort = ViewState("SortName").ToString + "    DESC "
                Else
                    dv.Sort = ViewState("SortName").ToString + "    ASC "
                End If
                '@ End of Code For Sorting
                '  dv.Sort = "TRN"          

                grdComptitionAgency.DataSource = dv

                grdComptitionAgency.DataBind()

                SetImageForSorting(grdComptitionAgency)
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdComptitionAgency_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdComptitionAgency.RowCommand
        Try
            If e.CommandName = "EditX" Then
                AgencyEdit(e.CommandArgument)
                Dim objSecurityXml As New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Competition Details']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Competition Details']").Attributes("Value").Value)
                        If strBuilder(2) = "1" Then
                            btnAdd.Enabled = True
                        End If
                    End If
                Else
                    strBuilder = objeAAMS.SecurityCheck(31)
                End If
            End If
            If e.CommandName = "DeleteX" Then
                AgencyDelete(e.CommandArgument)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub

    Protected Sub grdComptitionAgency_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdComptitionAgency.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim linkEdit As LinkButton
            linkEdit = e.Row.FindControl("lnkEdit")
            Dim linkDelete As LinkButton
            linkDelete = e.Row.FindControl("lnkDelete")

            'linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdstrPriorityId.Value & "'" & ");")
            'linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & "'" & hdstrPriorityId.Value & "'" & ");")
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Competition Details']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Competition Details']").Attributes("Value").Value)
                    If strBuilder(3) = "0" Then
                        linkDelete.Enabled = False
                    End If
                    If strBuilder(3) = "1" Then
                        linkDelete.Attributes.Add("onclick", "return DeleteFunctionManageComp();")
                    End If

                    If strBuilder(2) = "0" Then
                        linkEdit.Enabled = False
                    End If
                Else
                    ' linkDelete.Enabled = True
                    'linkEdit.Enabled = True
                    'linkDelete.Enabled = False
                    'linkEdit.Enabled = False
                End If
            Else
                linkDelete.Enabled = True
                linkEdit.Enabled = True
                linkDelete.Attributes.Add("onclick", "return DeleteFunctionManageComp();")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
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
End Class
