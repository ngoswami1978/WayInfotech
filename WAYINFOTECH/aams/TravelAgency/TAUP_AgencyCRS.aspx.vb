
Partial Class TravelAgency_TAUP_AgencyCRS
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
            'hdCity.Value = Session("a")
            hdCity.Value = Session("CityName")
            txtOfficeId.Attributes.Add("readonly", "readonly")
            Session("PageName") = Request.Url.ToString()
            objeAAMS.ExpirePageCache()
            lblError.Text = String.Empty
            If ViewState("TRN") Is Nothing Then
                btnAdd.Text = "Add"
            End If
            drpCRS.Focus()
            If (Session("Action") IsNot Nothing) Then
                hdLcode.Value = Session("Action").ToString().Split("|").GetValue(1)
            End If
            'btnHistory.Attributes.Add("onclick", "return PopupCRSHistoryPageForCRS();")

            btnAdd.Attributes.Add("onclick", "return ValidationForCRS();")

            drpCRS.Attributes.Add("onclick", "return PopuporWriteTesxtForCRS();")
            drpCRS.Attributes.Add("onchange", "return PopuporWriteTesxtForCRS();")
            '***************************************************************************************
            'Code of Security Check
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            ' #######################################
            ' ########## This code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Crs Details']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Crs Details']").Attributes("Value").Value)

                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                    End If

                    If strBuilder(1) = "0" Then
                        btnAdd.Enabled = False
                    End If


                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            ' #######################################
            ' ########## End of code used for enable/disable 
            ' ########## the button according to rights

            If Not Page.IsPostBack Then

                'objeAAMS.BindDropDown(drpCRS, "CRS", True)
                objeAAMS.BindDropDown(drpCRS, "CRSTEXT", True, 1)
                Session("AgencyCRS") = Nothing
                If (Session("Action") IsNot Nothing) Then
                    If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                        ' If strBuilder(0) = "1" Then
                        AgencyCRSView()
                        'End If
                    End If
                End If

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    '*********************************************************************************************************
    '****************************Method for View Agency *****************************************************
    '*********************************************************************************************************
    Private Sub AgencyCRSView()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAgencyCRSUse As New AAMS.bizTravelAgency.bzAgency
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim RowNo As Integer
        Dim dv As DataView
        Try

            ' #######################################
            ' ########## This code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Crs Details']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Crs Details']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Exit Sub
                    End If
                End If

            End If
            ' #######################################
            ' ########## End of code used for enable/disable 
            ' ########## the button according to rights


            objInputXml.LoadXml("<UP_GETCRSDETAILS_INPUT><LCODE></LCODE></UP_GETCRSDETAILS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerXml = Session("Action").ToString().Split("|").GetValue(1)
            'Here Back end Method Call
            objOutputXml = objbzAgencyCRSUse.GetAgencyCrsUseDetails(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                Dim objIAppendXml, objOAppendXml As New XmlDocument
                Dim objIXml As New XmlDocument
                Dim objxmldocfrag As XmlDocumentFragment
                Dim objNode As XmlNode
                Dim objNodeList As XmlNodeList

                objIAppendXml.LoadXml("<Document><CRS RN='' TRN='' LCODE='' DATE='' CURRENTID='' CRS='' OFFICEID='' Action='' /></Document>")
                If Session("AgencyCRS") Is Nothing Then
                    objOAppendXml.LoadXml("<UP_UPDATECRSDETAILS_INPUT></UP_UPDATECRSDETAILS_INPUT>")
                Else
                    objOAppendXml.LoadXml(Convert.ToString(Session("AgencyCRS")))
                End If
                objNodeList = objOutputXml.DocumentElement.SelectNodes("CRS")
                RowNo = 1
                For Each objNode In objNodeList
                    ' objIAppendXml.DocumentElement.SelectSingleNode("IPAddress").Attributes("IP").Value = objNode.Attributes("IP").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("CRS").Attributes("RN").Value = objNode.Attributes("RN").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("CRS").Attributes("TRN").Value = RowNo
                    objIAppendXml.DocumentElement.SelectSingleNode("CRS").Attributes("LCODE").Value = objNode.Attributes("LCODE").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("CRS").Attributes("CURRENTID").Value = objNode.Attributes("CURRENTID").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("CRS").Attributes("CRS").Value = objNode.Attributes("CRS").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("CRS").Attributes("OFFICEID").Value = objNode.Attributes("OFFICEID").Value
                    If (objNode.Attributes("RN").Value = "") Then
                        objIAppendXml.DocumentElement.SelectSingleNode("CRS").Attributes("Action").Value = "I"
                    Else
                        objIAppendXml.DocumentElement.SelectSingleNode("CRS").Attributes("Action").Value = "U"
                    End If
                    objxmldocfrag = objOAppendXml.CreateDocumentFragment()
                    objxmldocfrag.InnerXml = objIAppendXml.DocumentElement.InnerXml
                    objOAppendXml.DocumentElement.AppendChild(objxmldocfrag)
                    RowNo = RowNo + 1
                Next

                Session("AgencyCRS") = objOAppendXml.OuterXml

                objXmlReader = New XmlNodeReader(objOAppendXml) 'New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)

                dv = ds.Tables("CRS").DefaultView
                dv.RowFilter = "Action<>'D'"

                '@ Code For Sorting
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "CRS"
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

                grdCRSAgency.DataSource = Nothing
                grdCRSAgency.DataBind()

                    grdCRSAgency.DataSource = dv
                    'grdCRSAgency.DataSource = ds.Tables("CRS")
                grdCRSAgency.DataBind()
                SetImageForSorting(grdCRSAgency)
                Else
                    grdCRSAgency.DataSource = String.Empty
                    grdCRSAgency.DataBind()
                    'lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            objbzAgencyCRSUse = Nothing
            ds = Nothing
        End Try
    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Dim objInputXml As New XmlDocument
        Dim objIxml As New XmlDocument
        Dim objxmldocfrag As XmlDocumentFragment
        Dim objXmlReader As XmlReader
        Dim ds As New DataSet
        Dim objXMLNode As XmlNode
        ' Dim objNodeList As XmlNodeList
        Dim Rowno As Integer
        Dim objNode As XmlNode
        Dim dv As DataView

        Try
            objIxml.LoadXml("<Document><CRS RN='' TRN='' LCODE='' DATE='' CURRENTID='' CRS='' OFFICEID='' Action=''  /></Document>")
            If Session("AgencyCRS") Is Nothing Then
                objInputXml.LoadXml("<UP_UPDATECRSDETAILS_INPUT></UP_UPDATECRSDETAILS_INPUT>")
            Else
                objInputXml.LoadXml(Convert.ToString(Session("AgencyCRS")))
            End If
            If Not ViewState("TRN") Is Nothing Then
                objXMLNode = objInputXml.DocumentElement.SelectSingleNode("CRS[@TRN='" & ViewState("TRN") & "']")
                'TempRowno = objXMLNode.Attributes("TRN").Value
                objInputXml.DocumentElement.RemoveChild(objXMLNode)
            End If
            If drpCurrentID.SelectedValue.ToUpper = "Y" Then
                If objInputXml.DocumentElement.SelectNodes("CRS[@CURRENTID='Y' and @Action!='D']").Count = 0 Then

                    objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("LCODE").Value = Session("Action").ToString().Split("|").GetValue(1) ' "1212" 'Session("Action").ToString().Split("|").GetValue(1)
                    objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("CURRENTID").Value = drpCurrentID.SelectedValue
                    objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("CRS").Value = drpCRS.SelectedValue
                    objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("OFFICEID").Value = txtOfficeId.Text

                    If Not ViewState("RN") Is Nothing Then
                        If (ViewState("RN") <> "") Then
                            objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("RN").Value = Convert.ToInt32(ViewState("RN"))
                        End If
                        ViewState("RN") = Nothing
                    End If
                    If Not ViewState("TRN") Is Nothing Then
                        objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("TRN").Value = Convert.ToInt32(ViewState("TRN"))
                        ViewState("TRN") = Nothing
                    End If
                    If (objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("RN").Value = "") Then
                        objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("Action").Value = "I"
                    Else
                        objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("Action").Value = "U"
                    End If
                    objxmldocfrag = objInputXml.CreateDocumentFragment()
                    objxmldocfrag.InnerXml = objIxml.DocumentElement.InnerXml
                    objInputXml.DocumentElement.AppendChild(objxmldocfrag)
                    'Session("AgencyCRS") = objInputXml.OuterXml
                Else
                    lblError.Text = "You have not right to add more than one CurrentID 'True'"
                    If Not ViewState("TRN") Is Nothing Then
                        'TempRowno = objXMLNode.Attributes("TRN").Value
                        objInputXml.DocumentElement.AppendChild(objXMLNode)
                        ViewState("RN") = Nothing
                        ViewState("TRN") = Nothing
                        AgencyCRSReset()
                    End If
                    Exit Sub
                End If
            Else
                'If Not ViewState("TRN") Is Nothing Then
                '    objXMLNode = objInputXml.DocumentElement.SelectSingleNode("CRS[@TRN='" & ViewState("TRN") & "']")
                '    objInputXml.DocumentElement.RemoveChild(objXMLNode)
                'End If

                objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("LCODE").Value = Session("Action").ToString().Split("|").GetValue(1) '"1212" 'Session("Action").ToString().Split("|").GetValue(1)
                objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("CURRENTID").Value = drpCurrentID.SelectedValue
                objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("CRS").Value = drpCRS.SelectedValue
                objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("OFFICEID").Value = txtOfficeId.Text
                If Not ViewState("RN") Is Nothing Then
                    If (ViewState("RN").ToString() <> "") Then
                        objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("RN").Value = Convert.ToInt32(ViewState("RN"))
                    End If
                    ViewState("RN") = Nothing
                End If
                If Not ViewState("TRN") Is Nothing Then
                    ViewState("TRN") = Nothing
                    objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("TRN").Value = Convert.ToInt32(ViewState("TRN"))
                End If
                If (objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("RN").Value = "") Then
                    objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("Action").Value = "I"
                Else
                    objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("Action").Value = "U"
                End If
                objxmldocfrag = objInputXml.CreateDocumentFragment()
                objxmldocfrag.InnerXml = objIxml.DocumentElement.InnerXml
                objInputXml.DocumentElement.AppendChild(objxmldocfrag)


            End If
            ' objNodeList = objInputXml.DocumentElement.SelectNodes("CRS")
            Rowno = 1
            For Each objNode In objInputXml.DocumentElement.SelectNodes("CRS")
                ' objIAppendXml.DocumentElement.SelectSingleNode("IPAddress").Attributes("IP").Value = objNode.Attributes("IP").Value
                objNode.Attributes("TRN").Value = Rowno
                Rowno += 1
            Next

            Session("AgencyCRS") = objInputXml.OuterXml

            objXmlReader = New XmlNodeReader(objInputXml)
            ds.ReadXml(objXmlReader)
            dv = ds.Tables("CRS").DefaultView
            dv.RowFilter = "Action<>'D'"

            '@ Code For Sorting
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "CRS"
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
            'dv.Sort = "TRN"


            grdCRSAgency.DataSource = Nothing
            grdCRSAgency.DataBind()

            grdCRSAgency.DataSource = dv

            'grdCRSAgency.DataSource = ds.Tables("CRS")
            grdCRSAgency.DataBind()
            AgencyCRSReset()
            SetImageForSorting(grdCRSAgency)
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
        Dim objbzAgencyCRs As New AAMS.bizTravelAgency.bzAgencyCRSUse
        Dim intCountCurrentId As Integer
        Try
            If Not Session("AgencyCRS") Is Nothing Then
                objInputXml.LoadXml(Session("AgencyCRS"))

                objIAppendXml.LoadXml("<Document><CRS RN=''  LCODE='' DATE='' CURRENTID='' CRS='' OFFICEID='' Action='' EMPLOYEEID='' /></Document>")

                objOAppendXml.LoadXml("<UP_UPDATECRSDETAILS_INPUT></UP_UPDATECRSDETAILS_INPUT>")

                objNodeList = objInputXml.DocumentElement.SelectNodes("CRS")
                intCountCurrentId = 0
                For Each objNode In objNodeList
                    ' objIAppendXml.DocumentElement.SelectSingleNode("IPAddress").Attributes("IP").Value = objNode.Attributes("IP").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("CRS").Attributes("RN").Value = objNode.Attributes("RN").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("CRS").Attributes("LCODE").Value = objNode.Attributes("LCODE").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("CRS").Attributes("CURRENTID").Value = objNode.Attributes("CURRENTID").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("CRS").Attributes("CRS").Value = objNode.Attributes("CRS").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("CRS").Attributes("OFFICEID").Value = objNode.Attributes("OFFICEID").Value
                    If objNode.Attributes("CURRENTID").Value = "Y" And objNode.Attributes("Action").Value <> "D" Then
                        intCountCurrentId = intCountCurrentId + 1
                    End If
                    objIAppendXml.DocumentElement.SelectSingleNode("CRS").Attributes("Action").Value = objNode.Attributes("Action").Value
                    If Not Session("LoginSession") Is Nothing Then

                        objIAppendXml.DocumentElement.SelectSingleNode("CRS").Attributes("EMPLOYEEID").Value = Session("LoginSession").ToString().Split("|")(0)

                    End If
                    objxmldocfrag = objOAppendXml.CreateDocumentFragment()
                    objxmldocfrag.InnerXml = objIAppendXml.DocumentElement.InnerXml
                    objOAppendXml.DocumentElement.AppendChild(objxmldocfrag)
                   
                Next
                If intCountCurrentId > 1 Then
                    lblError.Text = "You have not right to add more than one CurrentID 'True'"
                    Exit Sub
                End If
                'Here Back end Method Call
                'objOutputXml = objbzEmployee.Update(objInputXml)
                objOutputXml = objbzAgencyCRs.Update(objOAppendXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                        lblError.Text = objeAAMSMessage.messUpdate
                    Else
                        lblError.Text = objeAAMSMessage.messInsert
                    End If
                    Session("AgencyCRS") = Nothing
                    AgencyCRSView()
                    ' SetImageForSorting(grdCRSAgency)
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
                btnAdd.Text = "Add"
            Else
                lblError.Text = "Please add atleast one CRS."
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

  
    Private Sub AgencyEdit(ByVal strRN As String)
        Dim objInputXml As New XmlDocument
        Try
            If Not Session("AgencyCRS") Is Nothing Then
                objInputXml.LoadXml(Convert.ToString(Session("AgencyCRS")))
                With objInputXml.DocumentElement.SelectSingleNode("CRS[@TRN='" & strRN & "']")
                    'With objInputXml.DocumentElement.SelectSingleNode("CRS[@RN='" & strRN & "']")
                    drpCRS.SelectedValue = .Attributes("CRS").Value
                    drpCurrentID.SelectedValue = .Attributes("CURRENTID").Value
                    txtOfficeId.Text = .Attributes("OFFICEID").Value
                    ViewState("RN") = .Attributes("RN").Value
                    ViewState("TRN") = .Attributes("TRN").Value
                End With
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub AgencyDelete(ByVal strRN As String)
        Dim objInputXml As New XmlDocument
        Dim objXmlReader As XmlReader
        Dim ds As New DataSet
        Dim dv As DataView
        Dim Rowno As Integer
        Dim objNode As XmlNode
        Dim strRN1 As String = ""
        Try
            If Not Session("AgencyCRS") Is Nothing Then
                objInputXml.LoadXml(Convert.ToString(Session("AgencyCRS")))
                ' objInputXml.DocumentElement.RemoveChild(objInputXml.DocumentElement.SelectSingleNode("CRS[@RN='" & strRN & "']"))
                If (objInputXml.DocumentElement.SelectNodes("CRS[@TRN='" & strRN & "' and @RN='']").Count > 0) Then
                    'If (objInputXml.DocumentElement.SelectNodes("CRS[@TRN='" & strRN1 & "']").Count > 0) Then
                    objInputXml.DocumentElement.RemoveChild(objInputXml.DocumentElement.SelectSingleNode("CRS[@TRN='" & strRN & "']"))
                    Rowno = 1
                    For Each objNode In objInputXml.DocumentElement.SelectNodes("CRS")
                        ' objIAppendXml.DocumentElement.SelectSingleNode("IPAddress").Attributes("IP").Value = objNode.Attributes("IP").Value
                        objNode.Attributes("TRN").Value = Rowno
                        Rowno += 1
                    Next
                Else
                    With objInputXml.DocumentElement.SelectSingleNode("CRS[@TRN='" & strRN & "']")
                        objInputXml.DocumentElement.SelectSingleNode("CRS[@TRN='" & strRN & "']").Attributes("Action").Value = "D"
                    End With
                    'End If
                End If

                objXmlReader = New XmlNodeReader(objInputXml)
                ds.ReadXml(objXmlReader)
                dv = ds.Tables("CRS").DefaultView
                dv.RowFilter = "Action<>'D'"
                '@ Code For Sorting
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "CRS"
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

                ' dv.Sort = "TRN"
                grdCRSAgency.DataSource = Nothing
                grdCRSAgency.DataBind()

                grdCRSAgency.DataSource = dv ' ds.Tables("CRS[@Action!='D']") ' ds.Tables("CRS[@Action!='D']")
                grdCRSAgency.DataBind()
                Session("AgencyCRS") = objInputXml.OuterXml
                ViewState("RN") = Nothing
                ViewState("TRN") = Nothing
                AgencyCRSReset()
                SetImageForSorting(grdCRSAgency)
            Else
                grdCRSAgency.DataSource = String.Empty
                grdCRSAgency.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub AgencyCRSReset()
        Try
            txtOfficeId.Text = String.Empty
            drpCRS.SelectedIndex = 0
            drpCurrentID.SelectedIndex = 0
            btnAdd.Text = "Add"
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
        
    End Sub

  

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            AgencyCRSReset()
            Session("AgencyCRS") = Nothing
            ViewState("RN") = Nothing
            ViewState("TRN") = Nothing
            AgencyCRSView()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Response.Redirect("", False)
        Catch ex As Exception
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

    Protected Sub grdCRSAgency_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCRSAgency.Sorted

    End Sub

    Protected Sub grdCRSAgency_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdCRSAgency.Sorting
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
            grdCRSAgency.DataSource = Nothing
            grdCRSAgency.DataBind()
            If Session("AgencyCRS") IsNot Nothing Then
                objOutputXml.LoadXml(Convert.ToString(Session("AgencyCRS")))


                objXmlReader = New XmlNodeReader(objOutputXml) 'New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)

                dv = ds.Tables("CRS").DefaultView
                dv.RowFilter = "Action<>'D'"

                '@ Code For Sorting
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "CRS"
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

                grdCRSAgency.DataSource = dv

                grdCRSAgency.DataBind()

                SetImageForSorting(grdCRSAgency)
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdCRSAgency_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdCRSAgency.RowCommand
        Try
            If e.CommandName = "EditX" Then
                AgencyEdit(e.CommandArgument)
                btnAdd.Text = "Update"

                Dim objSecurityXml As New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Crs Details']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Crs Details']").Attributes("Value").Value)
                        If strBuilder(2) = "1" Then
                            btnAdd.Enabled = True
                        End If
                    End If
                Else
                    strBuilder = objeAAMS.SecurityCheck(31)
                End If
            End If
            If e.CommandName = "DeleteX" Then
                txtOfficeId.Text = String.Empty
                drpCRS.SelectedIndex = 0
                drpCurrentID.SelectedIndex = 0
                AgencyDelete(e.CommandArgument)

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdCRSAgency_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdCRSAgency.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim linkEdit As LinkButton
            linkEdit = e.Row.FindControl("lnkEdit")
            Dim linkDelete As LinkButton
            linkDelete = e.Row.FindControl("lnkDelete")

            'linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdstrPriorityId.Value & "'" & ");")
            'linkDelete.Attributes.Add("onclick", "return DeleteFunctionForCRS(" & "'" & hdstrPriorityId.Value & "'" & ");")
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Crs Details']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Crs Details']").Attributes("Value").Value)
                    If strBuilder(3) = "0" Then
                        linkDelete.Enabled = False
                    End If
                    If strBuilder(3) = "1" Then
                        linkDelete.Attributes.Add("onclick", "return DeleteFunctionForCRS();")
                    End If
                    If strBuilder(2) = "0" Then
                        linkEdit.Enabled = False
                    End If
                Else
                    'linkDelete.Enabled = True
                    'linkEdit.Enabled = True
                    'linkDelete.Enabled = False
                    'linkEdit.Enabled = False
                End If
            Else
                linkDelete.Enabled = True
                linkEdit.Enabled = True
                linkDelete.Attributes.Add("onclick", "return DeleteFunctionForCRS();")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
