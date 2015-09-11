
Partial Class HelpDesk_HDUP_LinkedLTR
    Inherits System.Web.UI.Page
    Protected strIndex As String
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim str As String
    Dim objED As New EncyrptDeCyrpt
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Linked HD Call']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Linked HD Call']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                End If
                If strBuilder(1) = "0" Then
                    btnAdd.Enabled = False
                    btnSave.Enabled = False
                End If
                If strBuilder(2) = "0" Then
                    btnSave.Enabled = False
                End If
                If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                    btnSave.Enabled = True
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If



            If Not Page.IsPostBack Then
                hdEnFunctional.Value = objED.Encrypt("Functional") 'Storing encrypted value in hiddenfield 
                If Not Request.QueryString("strStatus") Is Nothing Then
                    hdPageStatus.Value = objED.Decrypt(Request.QueryString("strStatus").ToString)
                    hdEnPageStatus.Value = Request.QueryString("strStatus").ToString
                End If
                If Not Request.QueryString("LCode") Is Nothing Then
                    hdPageLCode.Value = objED.Decrypt(Request.QueryString("LCode").ToString)
                    hdEnPageLCode.Value = Request.QueryString("LCode").ToString
                    'If Request.QueryString("HD_RE_ID") Is Nothing Then
                    '    AgencyView()
                    'End If
                End If
                'window.location.href="HDUP_CallLog.aspx?Action=U&LCode="+LCode +"&HD_RE_ID="+ HD_RE_ID + "&strStatus=" + strStatus;               

                If Not Request.QueryString("HD_RE_ID") Is Nothing Then
                    hdPageHD_RE_ID.Value = objED.Decrypt(Request.QueryString("HD_RE_ID").ToString)
                    hdPHD_RE_ID.Value = objED.Decrypt(Request.QueryString("HD_RE_ID").ToString)
                    hdEnPageHD_RE_ID.Value = Request.QueryString("HD_RE_ID").ToString
                End If
                If Not Request.QueryString("AOFFICE") Is Nothing Then
                    hdAOffice.Value = objED.Decrypt(Request.QueryString("AOFFICE").ToString)
                    hdEnAOffice.Value = Request.QueryString("AOFFICE").ToString
                End If
                If Not Request.QueryString("FeedBackId") Is Nothing Then
                    hdFeedBackId.Value = objED.Decrypt(Request.QueryString("FeedBackId").ToString)
                    hdEnFeedBackId.Value = Request.QueryString("FeedBackId").ToString
                End If

                ' window.location.href="HDUP_LinkedLTR.aspx?Action=U&strStatus=Functional&LCode="+ LCode + "&HD_RE_ID=" + HD_RE_ID ;
                'If Not Request.QueryString("hdCHD_RE_ID") Is Nothing Then
                '    hdCHD_RE_ID.Value = Request.QueryString("hdCHD_RE_ID").ToString
                'End If

                'LoadAllControl()
                Bindata()
                ViewRecords()




                'On Reload of this page by Popup
                'If Request.QueryString("Action") IsNot Nothing Then
                '    If Request.QueryString("Action").ToUpper() = "PP" Then
                '        If Request.QueryString("LcodesMuk") IsNot Nothing Then
                '            hdPageLCode.Value = Request.QueryString("LcodesMuk")
                '        End If
                '        If Request.QueryString("HLtrCodesMuk") IsNot Nothing Then
                '            hdPageHD_RE_ID.Value = Request.QueryString("HLtrCodesMuk")
                '        End If

                '        AddViewRecords()
                '     
                '    Else
                '        ViewRecords()
                '    End If

                'End If
            End If

            'This hidden contains multiple LTR numbers separated by "," in  "hdCHD_RE_IDListPopupPage"
            If hdCHD_RE_IDListPopupPage.Value <> "" Then
                'LTR Contained "hdCHD_RE_IDListPopupPage" is transfers in "hdCHD_RE_IDList" to process further
                If hdCHD_RE_IDList.Value = "" Then
                    hdCHD_RE_IDList.Value = hdCHD_RE_IDListPopupPage.Value
                Else
                    hdCHD_RE_IDList.Value = hdCHD_RE_IDList.Value & "," & hdCHD_RE_IDListPopupPage.Value
                End If
                'After adding To "hdCHD_RE_IDList" Calls a Procedure
                AddRecords()
                hdCHD_RE_IDListPopupPage.Value = ""
                If ViewState("SortName") IsNot Nothing Then
                    BindRecords(ViewState("SortName"), ViewState("Desc"))
                Else
                    BindRecords()
                End If


            End If

            'If hdCHD_RE_IDList.Value = "" Then
            '    hdCHD_RE_IDList.Value = hdCHD_RE_ID.Value & "|"
            'Else
            '    'Deleting Duplicate child Linked LTR
            '    DeleteDuplicate()
            'End If

            'If hdAdd.Value = "1" Then
            '    If hdCHD_RE_ID.Value <> "" Then
            '        AddViewRecords()
            '    End If
            'End If

            If hdDeleteId.Value <> "" Then
                DeleteRecords()
            End If

            If hdInputXml.Value <> "" Then
                '    BindRecords()
            End If

            If hdButton.Value = "1" Then
                saveRecord()
            End If



        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
   
    Sub Bindata()
        Dim TabText As New ArrayList()
        Try
            TabText.Add("Call")
            TabText.Add("Description")
            TabText.Add("Solution")
            If hdPageStatus.Value.ToUpper <> "TECHNICAL" Then
                TabText.Add("Follow Up")
            End If
            TabText.Add("Linked LTR")
            TabText.Add("FeedBack")
            theTabStrip.DataSource = TabText
            theTabStrip.DataBind()
            ' hideTabStrip(strIndex)

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound
        Dim Button1 As Button
        Button1 = e.Item.FindControl("Button1")

        If Session("Security") IsNot Nothing Then
            Dim strButtonText As String
            strButtonText = Button1.Text
            Select Case strButtonText
                Case "Call"
                    If hdPageStatus.Value.ToUpper <> "TECHNICAL" Then
                        If objeAAMS.ReturnViewPermission(Session("Security"), "Log HD Call") = "0" Then
                            Button1.CssClass = "displayNone"
                        End If
                    Else
                        If objeAAMS.ReturnViewPermission(Session("Security"), "Log Tech Call") = "0" Then
                            Button1.CssClass = "displayNone"
                        End If
                    End If
                Case "Description"
                    If hdPageStatus.Value.ToUpper <> "TECHNICAL" Then
                        If objeAAMS.ReturnViewPermission(Session("Security"), "Log HD Call") = "0" Then
                            Button1.CssClass = "displayNone"
                        End If
                    Else
                        If objeAAMS.ReturnViewPermission(Session("Security"), "Log Tech Call") = "0" Then
                            Button1.CssClass = "displayNone"
                        End If
                    End If
                Case "Solution"
                    If hdPageStatus.Value.ToUpper <> "TECHNICAL" Then
                        If objeAAMS.ReturnViewPermission(Session("Security"), "Log HD Call") = "0" Then
                            Button1.CssClass = "displayNone"
                        End If
                    Else
                        If objeAAMS.ReturnViewPermission(Session("Security"), "Log Tech Call") = "0" Then
                            Button1.CssClass = "displayNone"
                        End If
                    End If
                Case "Follow Up"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Log HD Call") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Linked LTR"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Linked HD Call") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "FeedBack"
                    ' If objeAAMS.ReturnViewPermission(Session("Security"), "Feedback Action") = "0" Then
                    Button1.CssClass = "displayNone"
                    ' End If
            End Select
        End If


        '  Button1 = e.Item.FindControl("Button1")
        If hdPageStatus.Value = "Technical" Then
            Button1.Attributes.Add("onclick", "return ColorMethodLinkedLTR('" & Button1.ClientID.ToString() & "',4,'" & e.Item.ItemIndex & "');")
            If e.Item.ItemIndex = 3 Then
                Button1.CssClass = "headingtab"
            End If
        Else
            If e.Item.ItemIndex = 4 Then
                Button1.CssClass = "headingtab"
            End If
            Button1.Attributes.Add("onclick", "return ColorMethodLinkedLTR('" & Button1.ClientID.ToString() & "',5,'" & e.Item.ItemIndex & "');")
        End If


    End Sub


    'this method would be called when we select LTR from Pop up Window
    Sub AddRecords()
        'Set the value of error to true ie "1"
        'Error denotes any error while fetching records from backend or LTR added is same to Linked LTR 
        hdError.Value = ErrorStatus.Error_Found.ToString 'Setting Error To "1" Error Occured
        'Get the unique values excluding Parent LTR
        Dim strArray() As String = hdCHD_RE_IDList.Value.Split(",")
        Dim strLTRId As New ArrayList
        For i As Integer = 0 To strArray.Length - 1
            If hdPHD_RE_ID.Value <> strArray(i) Then 'this condition validates Linked LTR can't be same as Parent LTR
                If Not strLTRId.Contains(strArray(i)) Then 'this condition checks LTR will not added more than once
                    strLTRId.Add(strArray(i))
                End If
            End If
        Next


        'code for binding hiddenvalue after removing updating enteries
        If strLTRId.Count > 0 Then
            'Inserting Unique LTR To "hdCHD_RE_IDList"
            For i As Integer = 0 To strLTRId.Count - 1
                If i = 0 Then
                    hdCHD_RE_IDList.Value = strLTRId(i)
                Else
                    hdCHD_RE_IDList.Value = hdCHD_RE_IDList.Value & "," & strLTRId(i)
                End If
            Next
        Else 'If No Valid  Linked LTR Found
            lblError.Text = "Parent LTR and Child LTR Can't be same"
            hdCHD_RE_IDList.Value = ""
            hdInputXml.Value = ""
            Exit Sub
        End If

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzCall As New AAMS.bizHelpDesk.bzCall
        objInputXml.LoadXml("<HD_SEARCH_CALLREQUEST_INPUT><LCODE /><AgencyName /><OfficeID /><CUSTOMER_CATEGORY_ID /><LoggedBy />" & _
        "<CALLER_NAME /><HD_QUERY_GROUP_ID /><CALL_SUB_GROUP_ID /><CALL_CATEGORY_ID /><CALL_SUB_CATEGORY_ID /><HD_STATUS_ID /> " & _
        "<HD_SEVERITY_ID /><COORDINATOR1 /><COORDINATOR2 /><AssignedDatetime /><DISPOSITION_ID /><HD_RE_OPEN_DATE_FROM /> " & _
        "<HD_RE_OPEN_DATE_TO /><HD_RE_CLOSED_DATE_FROM /><HD_RE_CLOSED_DATE_TO /><Aoffice /><AgencyAoffice /><HD_RE_ID /><HD_PTR_REF /> " & _
        "<LastCall /><WO_ID /><LIMITED_TO_OWNAAGENCY /><LIMITED_TO_AOFFICE /><LIMITED_TO_REGION /><EmployeeID /><CITY /><State /><IATA_TID /><ISIATA/>" & _
        "<Region /><IsPending /><LTRs /><ASSIGNEDTO /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCH_CALLREQUEST_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("LTRs").InnerText = hdCHD_RE_IDList.Value
        If Not Session("Security") Is Nothing Then
            objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = objeAAMS.EmployeeID(Session("Security"))
        End If
        objOutputXml = objbzCall.Search(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            hdError.Value = ErrorStatus.No_Error.ToString 'Setting ErrorStatus To No Error if Everything goes Fine Else we have set an ErrorStaus to "1" in the starting of Procedure
            lblError.Text = "Record Added Successfully"
            hdSaveStatus.Value = "0"
            hdInputXml.Value = objOutputXml.OuterXml
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
    'this method would be called only First Time of Load
    Sub ViewRecords()
        Dim objInputXml, objOutputXml As New XmlDocument
        'Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzCall As New AAMS.bizHelpDesk.bzCall
        objInputXml.LoadXml("<HD_GET_LINKEDCALLREQUEST_INPUT><HD_RE_ID_PARENT></HD_RE_ID_PARENT><LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY><LIMITED_TO_AOFFICE></LIMITED_TO_AOFFICE><LIMITED_TO_REGION></LIMITED_TO_REGION></HD_GET_LINKEDCALLREQUEST_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID_PARENT").InnerText = hdPageHD_RE_ID.Value
        If Not Session("Security") Is Nothing Then
            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = objeAAMS.Limited_To_OwnAgency(Session("Security"))
            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = objeAAMS.Limited_To_Aoffice(Session("Security"))
            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = objeAAMS.Limited_To_Region(Session("Security"))
        End If

        ' Here Back end Method Call
        objOutputXml = objbzCall.GetLinkedLTRs(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            hdInputXml.Value = objOutputXml.OuterXml
            Dim objNodeList As XmlNodeList
            objNodeList = objOutputXml.DocumentElement.SelectNodes("CALLREQUEST")
            For Each objNode As XmlNode In objNodeList
                If hdCHD_RE_IDList.Value = "" Then
                    hdCHD_RE_IDList.Value = objNode.Attributes("HD_RE_ID").Value
                Else
                    hdCHD_RE_IDList.Value = hdCHD_RE_IDList.Value & "," & objNode.Attributes("HD_RE_ID").Value
                End If
            Next
            If ViewState("SortName") IsNot Nothing Then
                BindRecords(ViewState("SortName"), ViewState("Desc"))
            Else
                BindRecords()
            End If
        Else
            If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value <> "No Record Found!") Then
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                hdError.Value = "1"
            End If
            hdInputXml.Value = ""
            'Session("LinkedData") = Nothing
        End If


       
        'gvCallLog.DataSource = Nothing
        'gvCallLog.DataBind()
        'lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    End Sub

    Sub AddViewRecords()
        Dim objInputXml, objOutputXml, objSearchNode As New XmlDocument
        '  Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        If hdError.Value <> "1" Then
            If hdCHD_RE_ID.Value <> hdPHD_RE_ID.Value Then
                If hdInputXml.Value <> "" Then
                    objOutputXml.LoadXml(hdInputXml.Value)
                    Dim objXmlNodeClone As XmlNode
                    objXmlNodeClone = objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST").CloneNode(True)
                    With objXmlNodeClone
                        .Attributes("HD_RE_ID_PARENT").Value = hdPHD_RE_ID.Value
                        .Attributes("HD_RE_ID_CHILD").Value = hdCHD_RE_ID.Value
                        .Attributes("OfficeID").Value = hdOfficeID.Value
                        .Attributes("AgencyName").Value = hdAgencyName.Value
                        .Attributes("HD_RE_OPEN_DATE").Value = hdHD_RE_OPEN_DATE.Value
                        .Attributes("LoggedBy").Value = hdLoggedBy.Value
                        .Attributes("HD_QUERY_GROUP_NAME").Value = hdHD_QUERY_GROUP_NAME.Value
                        .Attributes("HD_STATUS_NAME").Value = hdHD_STATUS_NAME.Value
                        objOutputXml.DocumentElement.AppendChild(objXmlNodeClone)
                    End With
                Else
                    objOutputXml.LoadXml("<HD_GET_LINKEDCALLREQUEST_OUTPUT><CALLREQUEST HD_RE_ID_PARENT='' HD_RE_ID_CHILD='' LCODE='' OfficeID='' AgencyName='' HD_RE_OPEN_DATE='' HD_RE_CLOSED_DATE='' LoggedBy='' HD_QUERY_GROUP_NAME=''  HD_STATUS_NAME='' COLOR_CODE=''/> <Errors Status=''><Error Code='' Description='' /></Errors></HD_GET_LINKEDCALLREQUEST_OUTPUT>")
                    With objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST")
                        .Attributes("HD_RE_ID_PARENT").Value = hdPHD_RE_ID.Value
                        .Attributes("HD_RE_ID_CHILD").Value = hdCHD_RE_ID.Value
                        .Attributes("OfficeID").Value = hdOfficeID.Value
                        .Attributes("AgencyName").Value = hdAgencyName.Value
                        .Attributes("HD_RE_OPEN_DATE").Value = hdHD_RE_OPEN_DATE.Value
                        .Attributes("LoggedBy").Value = hdLoggedBy.Value
                        .Attributes("HD_QUERY_GROUP_NAME").Value = hdHD_QUERY_GROUP_NAME.Value
                        .Attributes("HD_STATUS_NAME").Value = hdHD_STATUS_NAME.Value
                    End With
                End If
                lblError.Text = "Record Added Successfully"
                hdSaveStatus.Value = "0"
                hdInputXml.Value = objOutputXml.OuterXml
            Else
                lblError.Text = "Parent LTR and Child LTR Can't be same"
                Dim strArray() As String = hdCHD_RE_IDList.Value.Split("|")
                Dim strLTRId As New ArrayList
                For i As Integer = 0 To strArray.Length - 2
                    strLTRId.Add(strArray(i))
                Next

                If strLTRId.Contains(hdCHD_RE_ID.Value) Then
                    strLTRId.Remove(hdCHD_RE_ID.Value)
                End If


                'code for binding hiddenvalue after removing updating enteries
                For i As Integer = 0 To strLTRId.Count - 1
                    If i = 0 Then
                        hdCHD_RE_IDList.Value = strLTRId(i) & "|"
                    Else
                        hdCHD_RE_IDList.Value = hdCHD_RE_IDList.Value & strLTRId(i) & "|"
                    End If
                Next
                'code end
            End If

        End If

            hdError.Value = "0"

    End Sub

    'This function Binds REcords to Grid,First it checks to ErrroSTatus Not Equal to ErrorFound and hdInputXml.value not Equal To Blank
    Sub BindRecords(Optional ByVal strSortingColumn As String = "HD_RE_ID", Optional ByVal strSortOrder As String = "ASC")
        
        If hdError.Value <> ErrorStatus.Error_Found.ToString Then
            If hdInputXml.Value <> "" Then
                Dim objOutputXml As New XmlDocument
                Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                objOutputXml.LoadXml(hdInputXml.Value)
                'This If condition Needs to check why it is written
                If hdPopupStatus.Value <> "" Then
                    Dim objTempNode As XmlNode
                    objTempNode = objOutputXml.DocumentElement.SelectSingleNode("CALLREQUEST[@HD_RE_ID='" + hdEHD_RE_ID.Value + "']")
                    If objTempNode IsNot Nothing Then
                        objTempNode.Attributes("HD_STATUS_NAME").Value = hdPopupStatus.Value
                    End If

                End If

                hdPopupStatus.Value = ""
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                Dim dv As DataView
                dv = ds.Tables("CALLREQUEST").DefaultView
                If strSortingColumn <> "" Then
                    dv.Sort = strSortingColumn & " " & strSortOrder
                End If
                gvCallLog.DataSource = dv
                gvCallLog.DataBind()

                Dim imgUp As New Image
                imgUp.ImageUrl = "~/Images/Sortup.gif"
                Dim imgDown As New Image
                imgDown.ImageUrl = "~/Images/Sortdown.gif"

                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "HD_RE_ID"
                    ViewState("Desc") = "ASC"
                End If

                Select Case ViewState("SortName").ToString
                    Case "HD_RE_ID"
                        Select Case ViewState("Desc").ToString
                            Case "ASC"
                                gvCallLog.HeaderRow.Cells(0).Controls.Add(imgUp)
                            Case "DESC"
                                gvCallLog.HeaderRow.Cells(0).Controls.Add(imgDown)
                        End Select
                    Case "OfficeID"
                        Select Case ViewState("Desc").ToString
                            Case "ASC"
                                gvCallLog.HeaderRow.Cells(1).Controls.Add(imgUp)
                            Case "DESC"
                                gvCallLog.HeaderRow.Cells(1).Controls.Add(imgDown)
                        End Select
                    Case "AgencyName"
                        Select Case ViewState("Desc").ToString
                            Case "ASC"
                                gvCallLog.HeaderRow.Cells(2).Controls.Add(imgUp)
                            Case "DESC"
                                gvCallLog.HeaderRow.Cells(2).Controls.Add(imgDown)

                        End Select


                    Case "HD_RE_OPEN_DATE"
                        Select Case ViewState("Desc").ToString
                            Case "ASC"
                                gvCallLog.HeaderRow.Cells(3).Controls.Add(imgUp)
                            Case "DESC"
                                gvCallLog.HeaderRow.Cells(3).Controls.Add(imgDown)
                        End Select
                    Case "LoggedBy"
                        Select Case ViewState("Desc").ToString
                            Case "ASC"
                                gvCallLog.HeaderRow.Cells(4).Controls.Add(imgUp)
                            Case "DESC"
                                gvCallLog.HeaderRow.Cells(4).Controls.Add(imgDown)
                        End Select
                    Case "HD_QUERY_GROUP_NAME"
                        Select Case ViewState("Desc").ToString
                            Case "ASC"
                                gvCallLog.HeaderRow.Cells(5).Controls.Add(imgUp)
                            Case "DESC"
                                gvCallLog.HeaderRow.Cells(5).Controls.Add(imgDown)

                        End Select
                    Case "HD_STATUS_NAME"
                        Select Case ViewState("Desc").ToString
                            Case "ASC"
                                gvCallLog.HeaderRow.Cells(6).Controls.Add(imgUp)
                            Case "DESC"
                                gvCallLog.HeaderRow.Cells(6).Controls.Add(imgDown)
                        End Select
                End Select

            Else
                gvCallLog.DataSource = Nothing
                gvCallLog.DataBind()
            End If
        End If
        hdError.Value = ErrorStatus.No_Error.ToString
    End Sub

    Sub DeleteDuplicate()
        'hdCHD_RE_IDList.Value = hdCHD_RE_ID.Value
        Dim strCHD_RE_ID() As String = hdCHD_RE_IDList.Value.Split("|")
        Dim cn As Integer = 0
        For i As Integer = 0 To strCHD_RE_ID.Length - 1
            If hdCHD_RE_ID.Value <> "" Then
                If strCHD_RE_ID(i) = hdCHD_RE_ID.Value Then
                    hdAdd.Value = "0"
                    cn = 1
                End If
            End If
        Next
        If cn = 0 And hdCHD_RE_ID.Value <> "" Then
            hdAdd.Value = "1"
            hdCHD_RE_IDList.Value = hdCHD_RE_IDList.Value & hdCHD_RE_ID.Value & "|"
        ElseIf cn = 1 Then
            lblError.Text = "Record Already Inserted"
        End If
    End Sub

    Sub DeleteRecords()
        If hdDeleteId.Value = "-100" Then
            Exit Sub
        End If
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlNode As XmlNode
        objInputXml.LoadXml(hdInputXml.Value)
        objXmlNode = objInputXml.DocumentElement.SelectSingleNode("CALLREQUEST[@HD_RE_ID='" + hdDeleteId.Value + "']")
        objInputXml.DocumentElement.RemoveChild(objXmlNode)

        hdSaveStatus.Value = "0"

        lblError.Text = objeAAMSMessage.messDelete
        hdInputXml.Value = objInputXml.OuterXml

        Dim strArray() As String = hdCHD_RE_IDList.Value.Split(",")
        Dim strLTRId As New ArrayList
        For i As Integer = 0 To strArray.Length - 1
            strLTRId.Add(strArray(i))
        Next
        If strLTRId.Contains(hdDeleteId.Value) Then
            strLTRId.Remove(hdDeleteId.Value)
            hdDeleteId.Value = "-100"
        End If


        'code for binding hiddenvalue after removing updating enteries
        If strLTRId.Count > 0 Then
            For i As Integer = 0 To strLTRId.Count - 1
                If i = 0 Then
                    hdCHD_RE_IDList.Value = strLTRId(i)
                Else
                    hdCHD_RE_IDList.Value = hdCHD_RE_IDList.Value & "," & strLTRId(i)
                End If
            Next
        Else 'If No Valid  Linked LTR Found
            hdCHD_RE_IDList.Value = ""
            hdInputXml.Value = ""
        End If
      
        BindRecords(ViewState("SortName"), ViewState("Desc"))
        'code end
    End Sub

    Function CheckParentChild() As Integer
        Dim strArray() As String = hdCHD_RE_IDList.Value.Split(",")
        Dim strLTRId As New ArrayList
        Dim status As Integer = 0
        For i As Integer = 0 To strArray.Length - 1
            strLTRId.Add(strArray(i))
        Next
        If strLTRId.Contains(hdPageHD_RE_ID.Value) Then
            lblError.Text = "Parent LTR and Child LTR Can't be same"
            status = 1
        End If
        Return status
    End Function
    Private Sub ViewAllRecordsAfterAdding()
        Try
            Dim objOutputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            If Session("LinkedData") IsNot Nothing Then
                objOutputXml.LoadXml(Session("LinkedData").ToString)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    gvCallLog.DataSource = ds.Tables("CALLREQUEST")
                    gvCallLog.DataBind()
                    lblError.Text = ""
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
       
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        saveRecord()
    End Sub
    Sub saveRecord()
        If hdCHD_RE_IDList.Value <> "" Or hdDeleteId.Value = "-100" Then
            Dim pcCheck As Integer = 0 ' CheckParentChild() commented on 28 th July ,now we have already checked child and Parent Ltr is same or not
            If pcCheck = 0 Then
                Dim objInputXml, objOutputXml As New XmlDocument
                Dim objXmlNodeClone, objXmlNode As XmlNode
                '  Dim objNodeList As XmlNodeList
                Dim strCHD_RE_ID() As String = hdCHD_RE_IDList.Value.Split(",")
                Dim objbzCall As New AAMS.bizHelpDesk.bzCall
                Try
                    objInputXml.LoadXml("<HD_UPDATE_CALLREQUESTLINK_INPUT><CALLREQUEST_LINK HD_RE_ID_PARENT='' ><HD_RE_ID_CHILD></HD_RE_ID_CHILD></CALLREQUEST_LINK></HD_UPDATE_CALLREQUESTLINK_INPUT>")
                    objInputXml.DocumentElement.SelectSingleNode("CALLREQUEST_LINK").Attributes("HD_RE_ID_PARENT").Value = hdPHD_RE_ID.Value
                    objXmlNode = objInputXml.DocumentElement.SelectSingleNode("CALLREQUEST_LINK/HD_RE_ID_CHILD")
                    objXmlNodeClone = objXmlNode.CloneNode(True)
                    For i As Integer = 0 To strCHD_RE_ID.Length - 1
                        If strCHD_RE_ID(i) <> "" Then
                            objXmlNodeClone = objXmlNode.CloneNode(True)
                            With objXmlNodeClone
                                .InnerText = strCHD_RE_ID(i)
                                objInputXml.DocumentElement.SelectSingleNode("CALLREQUEST_LINK").AppendChild(objXmlNodeClone)

                            End With
                        End If
                    Next
                    objInputXml.DocumentElement.SelectSingleNode("CALLREQUEST_LINK").RemoveChild(objXmlNode)
                    'This code is commented on 28 july 
                    'This code deletes blank node,this part is itself handles while creating xmlDocument
                    'objNodeList = objInputXml.DocumentElement.SelectNodes("CALLREQUEST_LINK/HD_RE_ID_CHILD")
                    'For Each objXmlNode In objNodeList
                    '    If objXmlNode.InnerText = "" Then
                    '        objInputXml.DocumentElement.SelectSingleNode("CALLREQUEST_LINK").RemoveChild(objXmlNode)
                    '    End If
                    'Next

                    objXmlNode = objInputXml.DocumentElement.SelectSingleNode("CALLREQUEST_LINK/HD_RE_ID_CHILD")

                    'Here Back end Method Call
                    If objXmlNode IsNot Nothing Or hdDeleteId.Value = "-100" Then
                        objOutputXml = objbzCall.UpdateLinkLTRs(objInputXml)

                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            ' If hdPageHD_RE_ID.Value = "" Then
                            lblError.Text = objeAAMSMessage.messInsert
                            hdSaveStatus.Value = "1"

                            If hdTabType.Value <> "" Then
                                If (hdTabType.Value = 4 And hdPageStatus.Value.ToUpper = "TECHNICAL") Or (hdTabType.Value = 5 And hdPageStatus.Value.ToUpper = "FUNCTIONAL") Then
                                    Response.Redirect("HDUP_helpDeskFeedBack.aspx?Action=U&LCode=" + hdEnPageLCode.Value + "&HD_RE_ID=" + hdEnPageHD_RE_ID.Value + "&strStatus=" + hdEnPageStatus.Value + "&TabType=" + hdTabType.Value)
                                End If

                                '"HDUP_helpDeskFeedBack.aspx?Action=U&strStatus=Technical&LCode="+ LCode + "&HD_RE_ID=" + HD_RE_ID + "&FeedBackId="+ strFeedBackId+ "&AOFFICE="+ AOFFICE ;
                                Response.Redirect("HDUP_CallLog.aspx?Action=U&LCode=" + hdEnPageLCode.Value + "&HD_RE_ID=" + hdEnPageHD_RE_ID.Value + "&strStatus=" + hdEnPageStatus.Value + "&TabType=" + hdTabType.Value)
                            End If

                            'End
                            '  Bindata()
                            '  ViewRecords()

                        Else
                            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                        End If
                    Else
                        lblError.Text = "Please select atleast one LTR"
                    End If

                Catch ex As Exception
                    lblError.Text = ex.Message.ToString
                Finally

                End Try
            End If
        End If
    End Sub

    Protected Sub gvCallLog_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCallLog.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
        '  e.Row.Attributes.Add("style", "background-color: " + DataBinder.Eval(e.Row.DataItem, "COLOR_CODE"))
        Dim hdHD_RE_ID As HiddenField
        Dim hdLCode As HiddenField
        Dim btnEdit As LinkButton
        Dim btnDelete As LinkButton
        btnEdit = CType(e.Row.FindControl("lnkEdit"), LinkButton)
        btnDelete = CType(e.Row.FindControl("lnkDelete"), LinkButton)
        hdHD_RE_ID = CType(e.Row.FindControl("hdHD_RE_ID"), HiddenField)
        hdLCode = CType(e.Row.FindControl("hdLCode"), HiddenField)
        Dim strStatus As String = e.Row.Cells(5).Text.Trim

        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Linked HD Call']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Linked HD Call']").Attributes("Value").Value)
            End If
            If strBuilder(3) = "0" Then
                btnDelete.Enabled = False
            Else
                btnDelete.Attributes.Add("OnClick", "javascript:return DeleteFunction('" + hdHD_RE_ID.Value + "','" + strStatus + "');")
            End If
            'If strBuilder(2) = "0" Then
            '    btnEdit.Enabled = False
            'Else
            btnEdit.Attributes.Add("OnClick", "javascript:return EditFunction('" + objED.Encrypt(hdLCode.Value) + "','" + objED.Encrypt(hdHD_RE_ID.Value) + "','" + objED.Encrypt(strStatus) + "');")
            ' End If

        Else

            btnDelete.Attributes.Add("OnClick", "javascript:return DeleteFunction('" + hdHD_RE_ID.Value + "');")
            btnEdit.Attributes.Add("OnClick", "javascript:return EditFunction('" + objED.Encrypt(hdLCode.Value) + "','" + objED.Encrypt(hdHD_RE_ID.Value) + "','" + objED.Encrypt(strStatus) + "');")
        End If


        '  <asp:BoundField HeaderText="OfficeID" DataField="OfficeID" />
        '<asp:BoundField HeaderText="Agency Name" DataField="OfficeID"/>
        '      <asp:BoundField HeaderText="Opened Date" DataField="HD_RE_OPEN_DATE"/>
        '                <asp:BoundField HeaderText="Call Logged By" DataField="LoggedBy"/>
        '                               <asp:BoundField HeaderText="Query Group" DataField="HD_QUERY_GROUP_NAME"/>
        '                           <asp:BoundField HeaderText="Status" DataField="HD_STHD_QUERY_GROUP_NAMEATUS_NAME"/>
        If Not Request.QueryString("Popup") Is Nothing Then
            '  btnSelect.Attributes.Add("OnClick", "javascript:return SelectFunction('" + hdHD_RE_ID.Value + "','" + strStatus + "','" + DataBinder.Eval(e.Row.DataItem, "OfficeID") + "','" + DataBinder.Eval(e.Row.DataItem, "AgencyName") + "','" + DataBinder.Eval(e.Row.DataItem, "HD_RE_OPEN_DATE") + "','" + DataBinder.Eval(e.Row.DataItem, "LoggedBy") + "','" + DataBinder.Eval(e.Row.DataItem, "HD_QUERY_GROUP_NAME") + "','" + DataBinder.Eval(e.Row.DataItem, "HD_STATUS_NAME") + "');")
        Else
            '   btnSelect.Visible = False
        End If

    End Sub

    Protected Sub gvCallLog_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCallLog.Sorted
       
    End Sub

    Protected Sub gvCallLog_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvCallLog.Sorting
        Try
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ViewState("Desc") = "ASC"
            Else
                If ViewState("SortName") = SortName Then
                    If ViewState("Desc") = "ASC" Then
                        ViewState("Desc") = "DESC"
                    Else
                        ViewState("Desc") = "ASC"
                    End If
                Else
                    ViewState("SortName") = SortName
                    ViewState("Desc") = "ASC"
                End If
            End If
            BindRecords(SortName, ViewState("Desc"))
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Enum ErrorStatus
        Error_Found = 1
        No_Error = 0
    End Enum

    
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
End Class
