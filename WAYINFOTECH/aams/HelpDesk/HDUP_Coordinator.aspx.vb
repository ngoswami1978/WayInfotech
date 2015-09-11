Partial Class HelpDesk_MSUP_Coordinator
    Inherits System.Web.UI.Page

#Region "Global variable Declaration"
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
#End Region

#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Code for Binding Grid when value is selected from popup
        Dim sta As String = hdEmpData.Value
        'Checking value whether it has selected something or not
        If sta <> "" Then
            ViewState("DataFromPopUp") = "Yes"
            BindValue()
        End If
        'Code End
        If Not Page.IsPostBack Then
            btnAdd.Attributes.Add("onClick", "return ValidateForm();")
            objeAAMS.BindDropDown(drpAoffice, "AOFFICE", True, 1)
            If Not Request.QueryString("Action") Is Nothing Then
                If Request.QueryString("Action") = "U" Then
                    BindData()
                    If Not Request.QueryString("Msg") Is Nothing Then
                        lblError.Text = objeAAMSMessage.messInsert
                    End If
                End If
            End If
            ' Checking security.
            CheckSecurity()
        End If
        If Session("EmployeePageName") IsNot Nothing Then
            hdEmployeePageName.Value = Session("EmployeePageName")
        End If
        If hdAOffice.Value <> "" Then
            drpAoffice.SelectedValue = hdAOffice.Value

            '  txtEmpName.Text = hdEmpName.Value
        End If


    End Sub
#End Region
    'Code For binding grid when value is selected from popup
    Sub BindValue()
        Try
            Dim objDS As New DataSet()
            Dim objXmlDoc As New XmlDocument
            Dim objXmlNode As XmlNode
            Dim objXmlNodeClone As XmlNode
            Dim objXmlNodeLst As XmlNodeList
            Dim strArray() As String = hdEmpData.Value.Split(",")
            Dim strEmpId As New ArrayList

            '<HD_UPDATECOORDINATOR_INPUT>
            '	<COORDINATOR Action='' Aoffice='' COORDINATOR_TYPE=''>
            '		<EMPLOYEE EMPLOYEE_ID='' />
            '	</COORDINATOR>
            '</HD_UPDATECOORDINATOR_INPUT>

            objXmlDoc.LoadXml("<HD_UPDATECOORDINATOR_INPUT><COORDINATOR Action='' Aoffice='' COORDINATOR_TYPE=''><EMPLOYEE EMPLOYEE_ID='' EMPLOYEE_NAME=''/></COORDINATOR></HD_UPDATECOORDINATOR_INPUT>")
            'Creating xml based on value selected from pop up
            For i As Integer = 0 To strArray.Length - 1
                If i = 0 Then
                    objXmlDoc.DocumentElement.SelectSingleNode("COORDINATOR").Attributes("Action").Value = "I"
                    objXmlDoc.DocumentElement.SelectSingleNode("COORDINATOR").Attributes("COORDINATOR_TYPE").Value = IIf(rbtCord1.Checked = True, "1", "2")
                    objXmlDoc.DocumentElement.SelectSingleNode("COORDINATOR").Attributes("Aoffice").Value = drpAoffice.SelectedValue
                    objXmlDoc.DocumentElement.SelectSingleNode("COORDINATOR/EMPLOYEE").Attributes("EMPLOYEE_ID").Value = strArray(0).Split("|").GetValue(1).ToString
                    objXmlDoc.DocumentElement.SelectSingleNode("COORDINATOR/EMPLOYEE").Attributes("EMPLOYEE_NAME").Value = strArray(0).Split("|").GetValue(2).ToString
                    strEmpId.Add(strArray(i).Split("|").GetValue(1).ToString)
                Else
                    objXmlNodeClone = objXmlDoc.DocumentElement.SelectSingleNode("COORDINATOR/EMPLOYEE").CloneNode(True)
                    
                    objXmlNodeClone.Attributes("EMPLOYEE_ID").Value = strArray(i).Split("|").GetValue(1).ToString
                    objXmlNodeClone.Attributes("EMPLOYEE_NAME").Value = strArray(i).Split("|").GetValue(2).ToString

                    objXmlDoc.DocumentElement.SelectSingleNode("COORDINATOR").AppendChild(objXmlNodeClone)
                    strEmpId.Add(strArray(i).Split("|").GetValue(1).ToString)
                End If
            Next
            'code End
            Dim xhash As New Hashtable
            'Code For removing duplicate nodes from the input xml
            For i As Integer = 0 To strEmpId.Count - 1

                objXmlNodeLst = objXmlDoc.DocumentElement.SelectNodes("COORDINATOR/EMPLOYEE[@EMPLOYEE_ID='" + strEmpId(i) + "']")
                If objXmlNodeLst.Count > 1 Then
                    For j As Integer = 0 To objXmlNodeLst.Count - 1
                        objXmlNode = objXmlNodeLst(j)
                        If j = 0 Then
                            
                        Else
                            objXmlDoc.SelectSingleNode("HD_UPDATECOORDINATOR_INPUT/COORDINATOR").RemoveChild(objXmlNode)
                        End If
                    Next
                End If
            Next
            
            Dim objXmlReader As XmlNodeReader
            objXmlReader = New XmlNodeReader(objXmlDoc)
            hdFinalVal.Value = objXmlDoc.InnerXml.ToString 'Storing final xml after removing duplicate nodes 
            objDS.ReadXml(objXmlReader)
            If objDS.Tables("EMPLOYEE").Rows.Count <> 0 Then
                '  gvEmployee.DataSource = objDS.Tables("EMPLOYEE")
                '  gvEmployee.DataBind()

                Dim dv As DataView
                dv = objDS.Tables("EMPLOYEE").DefaultView
                '  ViewState("SortName"), ViewState("Desc")

                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "EMPLOYEE_NAME"
                    ViewState("Desc") = "ASC"
                End If
                dv.Sort = ViewState("SortName") & " " & ViewState("Desc")


                gvEmployee.DataSource = dv
                gvEmployee.DataBind()


                Dim imgUp As New Image
                imgUp.ImageUrl = "~/Images/Sortup.gif"
                Dim imgDown As New Image
                imgDown.ImageUrl = "~/Images/Sortdown.gif"

                Select Case ViewState("SortName").ToString
                    Case "EMPLOYEE_NAME"
                        Select Case ViewState("Desc").ToString
                            Case "ASC"
                                gvEmployee.HeaderRow.Cells(1).Controls.Add(imgUp)
                            Case "DESC"
                                gvEmployee.HeaderRow.Cells(1).Controls.Add(imgDown)
                        End Select

                End Select
            End If




        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

#Region "btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click"
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim objDS As New DataSet()
            Dim objXmlDoc As New XmlDocument
            Dim objXmlNode As XmlNode
            Dim objXmlNodeClone As XmlNode
            '  <HD_UPDATECOORDINATOR_INPUT><COORDINATOR Action='' COORDINATOR_TYPE='' Aoffice='' EmployeeID='' /></HD_UPDATECOORDINATOR_INPUT>
            If Request.QueryString("Action") = "U" Then
                ' Code Start for update.

                objXmlDoc.LoadXml(CType(ViewState("EmpDetailsUpdate"), String))
                Dim objXmlNodeLst As XmlNodeList
                Dim objXmlNodeChk As XmlNode
                'dfdsfd
                objXmlNodeLst = objXmlDoc.DocumentElement.SelectNodes("COORDINATOR[@Action='D']")
                For Each objXmlNodeChk In objXmlNodeLst
                    If objXmlNodeChk.Attributes("EmployeeID").Value = hdEmpID.Value Then
                        objXmlNodeChk.Attributes("Action").Value = "U"
                        'lblError.Text = "Employee Already Exists!"
                        'Exit Sub
                        GoTo label
                    End If
                Next

                objXmlNodeLst = objXmlDoc.DocumentElement.SelectNodes("COORDINATOR[@Action != 'D']")
                For Each objXmlNodeChk In objXmlNodeLst
                    If objXmlNodeChk.Attributes("EmployeeID").Value = hdEmpID.Value Then
                        lblError.Text = "Employee Already Exists!"
                        Exit Sub
                    End If
                Next
                'sfsf
                'objXmlNodeLst = objXmlDoc.DocumentElement.SelectNodes("COORDINATOR")
                'For Each objXmlNodeChk In objXmlNodeLst
                '    If objXmlNodeChk.Attributes("EmployeeID").Value = hdEmpID.Value Then
                '        lblError.Text = "Employee Already Exists!"
                '        Exit Sub
                '    End If
                'Next
                objXmlNodeClone = objXmlDoc.DocumentElement.SelectSingleNode("COORDINATOR").CloneNode(True)
                objXmlNodeClone.Attributes("Action").Value = "I"
                objXmlNodeClone.Attributes("EmployeeID").Value = hdEmpID.Value
                objXmlNodeClone.Attributes("Employee_Name").Value = hdEmpName.Value
                objXmlNodeClone.Attributes("COORDINATOR_TYPE").Value = IIf(rbtCord1.Checked = True, "1", "2")
                objXmlNodeClone.Attributes("Aoffice").Value = drpAoffice.SelectedValue
                objXmlDoc.DocumentElement.AppendChild(objXmlNodeClone)
label:
                ViewState("EmpDetailsUpdate") = objXmlDoc.OuterXml

                Dim objXmlReader As XmlNodeReader
                objXmlReader = New XmlNodeReader(objXmlDoc)
                objDS.ReadXml(objXmlReader)
                If objDS.Tables("COORDINATOR").Rows.Count <> 0 Then
                    Dim dv As DataView
                    dv = objDS.Tables("COORDINATOR").DefaultView
                    dv.RowFilter = "Action<>'D'"
                    gvEmployee.DataSource = dv
                    gvEmployee.DataBind()
                Else
                    ViewState("EmpDetailsUpdate") = Nothing
                    gvEmployee.DataSource = Nothing
                    gvEmployee.DataBind()
                End If

                ' Code End for update

            ElseIf ViewState("EmpDetails") Is Nothing Then
                objXmlDoc.LoadXml("<Employees><COORDINATOR Action='' EmployeeID='' Employee_Name='' Aoffice='' /></Employees>")
                objXmlNode = objXmlDoc.DocumentElement.SelectSingleNode("COORDINATOR")
                objXmlNode.Attributes("Action").Value = "I"
                objXmlNode.Attributes("EmployeeID").Value = hdEmpID.Value
                objXmlNode.Attributes("Employee_Name").Value = hdEmpName.Value

                ViewState("EmpDetails") = objXmlDoc.OuterXml
                Dim objXmlReader1 As XmlNodeReader
                objXmlReader1 = New XmlNodeReader(objXmlDoc)
                objDS.ReadXml(objXmlReader1)
                If objDS.Tables("COORDINATOR").Rows.Count <> 0 Then
                    gvEmployee.DataSource = objDS.Tables("COORDINATOR")
                    gvEmployee.DataBind()
                Else
                    ViewState("EmpDetails") = Nothing
                    gvEmployee.DataSource = Nothing
                    gvEmployee.DataBind()
                End If
            Else
                objXmlDoc.LoadXml(CType(ViewState("EmpDetails"), String))

                Dim objXmlNodeLst As XmlNodeList
                Dim objXmlNodeChk As XmlNode
                objXmlNodeLst = objXmlDoc.DocumentElement.SelectNodes("COORDINATOR")
                For Each objXmlNodeChk In objXmlNodeLst
                    If objXmlNodeChk.Attributes("EmployeeID").Value = hdEmpID.Value Then
                        lblError.Text = "Employee Already Exists!"
                        Exit Sub
                    End If
                Next
                objXmlNodeClone = objXmlDoc.DocumentElement.SelectSingleNode("COORDINATOR").CloneNode(True)
                objXmlNodeClone.Attributes("Action").Value = "I"
                objXmlNodeClone.Attributes("EmployeeID").Value = hdEmpID.Value
                objXmlNodeClone.Attributes("Employee_Name").Value = hdEmpName.Value
                objXmlDoc.DocumentElement.AppendChild(objXmlNodeClone)
                ViewState("EmpDetails") = objXmlDoc.OuterXml

                Dim objXmlReader1 As XmlNodeReader
                objXmlReader1 = New XmlNodeReader(objXmlDoc)
                objDS.ReadXml(objXmlReader1)
                If objDS.Tables("COORDINATOR").Rows.Count <> 0 Then
                    gvEmployee.DataSource = objDS.Tables("COORDINATOR")
                    gvEmployee.DataBind()
                Else
                    ViewState("EmpDetails") = Nothing
                    gvEmployee.DataSource = Nothing
                    gvEmployee.DataBind()
                End If

            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "gvEmployee_RowCommand()"
    Protected Sub gvEmployee_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvEmployee.RowCommand

        Dim strISP As String = ""
        Dim objXmlInputDoc As New XmlDocument()
        Dim objXmlISPNode As XmlNode
        Dim objDS As New DataSet()
        Dim objXmlReader As XmlNodeReader
        Try
            If e.CommandName = "Deletex" Then
                strISP = e.CommandArgument.ToString()
                If hdFinalVal.Value <> "" Then
                    objXmlInputDoc.LoadXml(hdFinalVal.Value)
                    objXmlISPNode = objXmlInputDoc.DocumentElement.SelectSingleNode("COORDINATOR/EMPLOYEE[@EMPLOYEE_ID='" + strISP + "']")
                    'If Request.QueryString("Action") = "U" Then
                    '    'objXmlISPNode.Attributes("Action").Value = "D"
                    '    'code for updating status of hidden value 
                    '    Dim strArray() As String = hdEmpData.Value.Split(",")
                    '    Dim strEmpId As New ArrayList
                    '    For i As Integer = 0 To strArray.Length - 1
                    '        strEmpId.Add(strArray(i))
                    '    Next


                    '    'code for binding hiddenvalue after removing updating enteries
                    '    For i As Integer = 0 To strEmpId.Count - 1
                    '        If i = 0 Then
                    '            hdEmpData.Value = strEmpId(i)
                    '        Else
                    '            hdEmpData.Value = hdEmpData.Value & "," & strEmpId(i)
                    '        End If
                    '    Next
                    '    'code end
                    'Else

                    objXmlInputDoc.DocumentElement.SelectSingleNode("COORDINATOR").RemoveChild(objXmlISPNode)
                    'code for removing multiple enteries from hidden value 
                    Dim strArray() As String = hdEmpData.Value.Split(",")
                    Dim strEmpId As New ArrayList
                    Dim strInd As New ArrayList
                    For i As Integer = 0 To strArray.Length - 1
                        strEmpId.Add(strArray(i))
                    Next

                    For i As Integer = 0 To strEmpId.Count - 1
                        Dim strvalue As String = strEmpId(i)
                        If strvalue.Split("|").GetValue(1).ToString = strISP Then
                            strInd.Add(strEmpId(i))
                        End If
                    Next
                    For i As Integer = 0 To strInd.Count - 1
                        strEmpId.Remove(strInd(i))
                    Next
                    'code end

                    'code for binding hiddenvalue after removing multiple enteries
                    For i As Integer = 0 To strEmpId.Count - 1
                        If i = 0 Then
                            hdEmpData.Value = strEmpId(i)
                        Else
                            hdEmpData.Value = hdEmpData.Value & "," & strEmpId(i)
                        End If
                    Next
                    'code end

                End If
                hdFinalVal.Value = objXmlInputDoc.OuterXml.ToString()
                objXmlReader = New XmlNodeReader(objXmlInputDoc)
                objDS.ReadXml(objXmlReader)


                If Not objDS.Tables("EMPLOYEE") Is Nothing Then
                    If objDS.Tables("EMPLOYEE").Rows.Count <> 0 Then

                        ' gvEmployee.DataSource = objDS.Tables("EMPLOYEE")
                        '  gvEmployee.DataBind()
                        Dim dv As DataView
                        dv = objDS.Tables("EMPLOYEE").DefaultView

                        '  ViewState("SortName"), ViewState("Desc")
                        dv.Sort = ViewState("SortName") & " " & ViewState("Desc")


                        gvEmployee.DataSource = dv
                        gvEmployee.DataBind()



                        Dim imgUp As New Image
                        imgUp.ImageUrl = "~/Images/Sortup.gif"
                        Dim imgDown As New Image
                        imgDown.ImageUrl = "~/Images/Sortdown.gif"

                        Select Case ViewState("SortName").ToString
                            Case "EMPLOYEE_NAME"
                                Select Case ViewState("Desc").ToString
                                    Case "ASC"
                                        gvEmployee.HeaderRow.Cells(1).Controls.Add(imgUp)
                                    Case "DESC"
                                        gvEmployee.HeaderRow.Cells(1).Controls.Add(imgDown)
                                End Select

                        End Select

                    Else
                        gvEmployee.DataSource = Nothing
                        gvEmployee.DataBind()
                    End If
                Else
                    hdEmpData.Value = ""
                    gvEmployee.DataSource = Nothing
                    gvEmployee.DataBind()
                End If
            End If
            ' End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim intRowsCnt As Integer = 0
        Dim objInputXmlDoc As New XmlDocument
        Dim objOutputXmlDoc As New XmlDocument
        Dim objCoordinator As New AAMS.bizHelpDesk.bzCoordinator
        Try
            'If Not Request.QueryString("Action") Is Nothing Then
            'If Not Request.QueryString("Action") = "U" Then
            intRowsCnt = gvEmployee.Rows.Count
            If (intRowsCnt = 0) Then
                lblError.Text = "Please add atleast one employee."
            Else
                CreateFNode()
            End If
            ' Else
            '  CreateFNode()
            ' End If
            '  End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            'If Not Request.QueryString("Action") Is Nothing Then
            '    lblError.Text = ""
            '    If Request.QueryString("Action") = "U" Then
            '        BindData()
            '    Else
            '        'rbtCord1.Checked = True
            '        'drpAoffice.Enabled = True
            '        'drpAoffice.SelectedIndex = 0
            '        ''  txtEmpName.Text = ""
            '        'hdAOffice.Value = ""
            '        'hdEmpID.Value = ""
            '        'hdEmpName.Value = ""
            '        Response.Redirect("HDUP_Coordinator.aspx?Action=I")

            '    End If

            Response.Redirect(Request.Url.ToString())

            ' End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click"
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("HDUP_Coordinator.aspx?Action=I")
    End Sub
#End Region

#Region "CreateFNode()"
    Private Sub CreateFNode()
        Dim objOutputXml As New XmlDocument
        Dim objXmlDoc As New XmlDocument
        Dim objXmlNode As XmlNode
        Dim objXmlNodeCord As XmlNode
        Dim objXmlNodeClone As XmlNode
        Dim intCnt As Integer = 0
        Dim intVar As Integer
        Dim strAoffice As String
        Dim strCoordinatorType As String = ""
        Dim objCordinator As New AAMS.bizHelpDesk.bzCoordinator
        Try
            If rbtCord1.Checked = True Then
                strCoordinatorType = 1
            End If
            If rbtCord2.Checked = True Then
                strCoordinatorType = 2
            End If
            strAoffice = drpAoffice.SelectedValue

            objXmlDoc.LoadXml("<HD_UPDATECOORDINATOR_INPUT><COORDINATOR Action='' Aoffice='' COORDINATOR_TYPE=''><EMPLOYEE EMPLOYEE_ID='' /></COORDINATOR></HD_UPDATECOORDINATOR_INPUT>")
            objXmlNode = objXmlDoc.DocumentElement.SelectSingleNode("COORDINATOR/EMPLOYEE")
            objXmlNodeCord = objXmlDoc.DocumentElement.SelectSingleNode("COORDINATOR")
            For intVar = 0 To gvEmployee.Rows.Count - 1
                Dim strEmpID As String = gvEmployee.Rows(intVar).Cells(0).Text
                If intCnt = 0 Then
                    objXmlNodeCord.Attributes("Action").Value = "I"
                    objXmlNodeCord.Attributes("COORDINATOR_TYPE").Value = strCoordinatorType
                    objXmlNodeCord.Attributes("Aoffice").Value = strAoffice
                    objXmlNode.Attributes("EMPLOYEE_ID").Value = gvEmployee.Rows(intVar).Cells(0).Text
                    intCnt = 1
                Else
                    objXmlNodeClone = objXmlDoc.DocumentElement.SelectSingleNode("COORDINATOR/EMPLOYEE").CloneNode(True)
                    objXmlNodeClone.Attributes("EMPLOYEE_ID").Value = gvEmployee.Rows(intVar).Cells(0).Text
                    objXmlDoc.DocumentElement.SelectSingleNode("COORDINATOR").AppendChild(objXmlNodeClone)
                End If
            Next
            objOutputXml = objCordinator.Update(objXmlDoc)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If (Request.QueryString("Action").ToString().ToUpper() = "U") Then
                    lblError.Text = objeAAMSMessage.messUpdate ' Record updated successfully.
                Else
                    ' lblError.Text = objeAAMSMessage.messInsert ' Record inserted successfully.
                    Response.Redirect("HDUP_Coordinator.aspx?Action=U&Aoffice=" + objED.Encrypt(strAoffice) + "&CoType=" + objED.Encrypt(strCoordinatorType) + "&Msg=I")

                End If
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "BindData()"
    Private Sub BindData(Optional ByVal strSortingColumn As String = "EMPLOYEE_NAME", Optional ByVal strSortOrder As String = "ASC")
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objCordinator As New AAMS.bizHelpDesk.bzCoordinator
        Try
            drpAoffice.SelectedValue = objED.Decrypt(Request.QueryString("Aoffice"))


            If objED.Decrypt(Request.QueryString("CoType")) = 1 Then
                rbtCord1.Checked = True
            ElseIf objED.Decrypt(Request.QueryString("CoType")) = 2 Then
                rbtCord2.Checked = True
            End If
            '   rbtCord2.Enabled = False
            '  rbtCord1.Enabled = False

            objInputXml.LoadXml("<HD_GETCOORDINATOR_INPUT><COORDINATOR_TYPE /><Aoffice /></HD_GETCOORDINATOR_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("COORDINATOR_TYPE").InnerText = objED.Decrypt(Request.QueryString("CoType"))
            objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = objED.Decrypt(Request.QueryString("Aoffice"))
            objOutputXml = objCordinator.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
               

                If ds.Tables("EMPLOYEE").Rows.Count <> 0 Then
                    Dim dv As DataView
                    dv = ds.Tables("EMPLOYEE").DefaultView
                    If strSortingColumn <> "" Then
                        dv.Sort = strSortingColumn & " " & strSortOrder
                    End If

                    gvEmployee.DataSource = dv
                    gvEmployee.DataBind()

                    ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    ' @ Added Code To Show Image'

                    If ViewState("SortName") Is Nothing Then
                        ViewState("SortName") = "EMPLOYEE_NAME"
                        ViewState("Desc") = "ASC"
                    End If


                    Dim imgUp As New Image
                    imgUp.ImageUrl = "~/Images/Sortup.gif"
                    Dim imgDown As New Image
                    imgDown.ImageUrl = "~/Images/Sortdown.gif"

                    Select Case ViewState("SortName").ToString
                        Case "EMPLOYEE_NAME"
                            Select Case ViewState("Desc").ToString
                                Case "ASC"
                                    gvEmployee.HeaderRow.Cells(1).Controls.Add(imgUp)
                                Case "DESC"
                                    gvEmployee.HeaderRow.Cells(1).Controls.Add(imgDown)
                            End Select

                    End Select
                    '  Added Code To Show Image'

                    '  gvEmployee.DataSource = ds.Tables("EMPLOYEE")
                    ' gvEmployee.DataBind()





                    ' Updating values in xml
                    '<COORDINATOR Action='' COORDINATOR_TYPE='' Aoffice='' EmployeeID='' />
                    Dim strCoordinatorType As String = ""
                    Dim strAoffice As String = ""
                    Dim objXmlDoc As New XmlDocument
                    Dim objXmlNode, objXmlNodeClone As XmlNode

                    If rbtCord1.Checked = True Then
                        strCoordinatorType = 1
                    End If
                    If rbtCord2.Checked = True Then
                        strCoordinatorType = 2
                    End If
                    strAoffice = drpAoffice.SelectedValue

                    Dim objXmlNodeCord As XmlNode
                    Dim intVar, intCnt As Integer
                    objXmlDoc.LoadXml("<HD_UPDATECOORDINATOR_INPUT><COORDINATOR Action='' Aoffice='' COORDINATOR_TYPE=''><EMPLOYEE EMPLOYEE_ID='' /></COORDINATOR></HD_UPDATECOORDINATOR_INPUT>")
                    objXmlNode = objXmlDoc.DocumentElement.SelectSingleNode("COORDINATOR/EMPLOYEE")
                    objXmlNodeCord = objXmlDoc.DocumentElement.SelectSingleNode("COORDINATOR")
                    For intVar = 0 To gvEmployee.Rows.Count - 1
                        Dim strEmpID As String = gvEmployee.Rows(intVar).Cells(0).Text
                        If intCnt = 0 Then
                            objXmlNodeCord.Attributes("Action").Value = "I"
                            objXmlNodeCord.Attributes("COORDINATOR_TYPE").Value = strCoordinatorType
                            objXmlNodeCord.Attributes("Aoffice").Value = strAoffice
                            objXmlNode.Attributes("EMPLOYEE_ID").Value = gvEmployee.Rows(intVar).Cells(0).Text
                            hdEmpData.Value = "I|" & gvEmployee.Rows(intVar).Cells(0).Text & "|" & gvEmployee.Rows(intVar).Cells(1).Text
                            intCnt = 1
                        Else
                            objXmlNodeClone = objXmlDoc.DocumentElement.SelectSingleNode("COORDINATOR/EMPLOYEE").CloneNode(True)
                            objXmlNodeClone.Attributes("EMPLOYEE_ID").Value = gvEmployee.Rows(intVar).Cells(0).Text
                            hdEmpData.Value = hdEmpData.Value + ",I|" & gvEmployee.Rows(intVar).Cells(0).Text & "|" & gvEmployee.Rows(intVar).Cells(1).Text
                            objXmlDoc.DocumentElement.SelectSingleNode("COORDINATOR").AppendChild(objXmlNodeClone)
                        End If
                    Next
                    hdFinalVal.Value = objXmlDoc.OuterXml.ToString()
                Else
                    gvEmployee.DataSource = Nothing
                    gvEmployee.DataBind()
                End If

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
#End Region

#Region "gvEmployee_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvEmployee.RowDataBound"
    Protected Sub gvEmployee_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvEmployee.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If

            '************* Code for Delete link ****************************************************************
            Dim btnDelete1 As LinkButton
            btnDelete1 = CType(e.Row.FindControl("btnDelete"), LinkButton)
            ' btnDelete1.Attributes.Add("OnClick", "javascript:return Delete();")
            '************* end code for delete link *****************************************************************
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Coordinator']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Coordinator']").Attributes("Value").Value)
                End If
                If strBuilder(3) = "0" Then
                    btnDelete1.Enabled = False
                End If
                
            Else
                btnDelete1.Attributes.Add("OnClick", "javascript:return Delete();")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "CheckSecurity()::This method is used for security check."
    Private Sub CheckSecurity()
        Try
            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Coordinator']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Coordinator']").Attributes("Value").Value)
                End If
                'If strBuilder(0) = "0" Then
                '    btnSave.Enabled = False
                '    btnAdd.Enabled = False
                'End If
                'If strBuilder(1) = "0" Then
                '    btnSave.Enabled = False
                '    btnNew.Enabled = False
                'End If
                '''''''''''''''''''''''''''''''''''''''''''''''''
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                    btnAdd.Enabled = False
                End If

                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    btnSave.Enabled = False
                    btnAdd.Enabled = False
                End If
                If strBuilder(2) = "0" And (hdID.Value <> "" Or Request.QueryString("CoType") IsNot Nothing) Then
                    btnSave.Enabled = False
                    btnAdd.Enabled = False
                End If

                If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                    btnSave.Enabled = True
                    btnAdd.Enabled = False
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub gvEmployee_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvEmployee.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvEmployee_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvEmployee.Sorting
        Try


            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ViewState("Desc") = "ASC"
            Else
                If ViewState("SortName") = SortName Then
                    If ViewState("Desc") = "DESC" Then
                        ViewState("Desc") = "ASC"
                    Else
                        ViewState("Desc") = "DESC"
                    End If
                Else
                    ViewState("SortName") = SortName
                    ViewState("Desc") = "ASC"
                End If
            End If
            If ViewState("DataFromPopUp") Is Nothing Then
                BindData(ViewState("SortName"), ViewState("Desc"))
            Else
                BindValue()
            End If

            ' BindData(PageOperation.Search)

        Catch ex As Exception
            lblError.Text = ex.Message
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
