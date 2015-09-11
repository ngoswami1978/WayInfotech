Imports System.Data
Imports System.Data.SqlClient
Imports system.io
Partial Class TravelAgency_TAUP_AgencyNotes
    Inherits System.Web.UI.Page
#Region "Global Declaration Section"
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
#End Region

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
            Session("PageName") = "TravelAgency/MSUP_AgencyNotes.aspx"
            'btnReset.Attributes.Add("onclick", "return OrderStatusReset();")
            ' btnNew.Attributes.Add("onclick", "return NewFunction();")
            btnSave.Attributes.Add("onclick", "return ChkEmptyText();")
            lblError.Text = String.Empty
            '***************************************************************************************
            'If Not Request.QueryString("Action") Is Nothing Then
            '    If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
            '        OrderStatusDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))
            '    End If
            'End If
            '***************************************************************************************
            'Code of Security Check
            objeAAMS.ExpirePageCache()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            Dim strLcode As String = Session("Action").ToString().Split("|").GetValue(1)




            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Notes']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Notes']").Attributes("Value").Value)
                    If strBuilder IsNot Nothing Then

                        If strBuilder(0) = "0" Then
                            Response.Redirect("~/NoRights.aspx")
                        End If

                        If strBuilder(1) = "0" Then
                            btnSave.Enabled = False
                        Else
                            DisplayNoteList(strLcode)
                        End If
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                DisplayNoteList(strLcode)
            End If


            '@ Start of Code for wheteher link will be display or not
            Dim StrFileNamexls As String
            StrFileNamexls = String.Empty
           
            Try
                If Session("Action") IsNot Nothing Then
                    Dim strsLcode As String = Session("Action").ToString().Split("|").GetValue(1)
                    StrFileNamexls = strsLcode + ".xls"
                    Dim fullpathxls = Path.GetFullPath(Server.MapPath("~/AgencyNotes/") + StrFileNamexls)
                    Dim name = Path.GetFileName(fullpathxls)
                    Dim ext = Path.GetExtension(fullpathxls)
                    If File.Exists(fullpathxls) Then
                        LnkInstallationDetails.Visible = True
                    End If
                End If
            Catch ex As Exception
                ex.Message.ToString()
            End Try
            '@ End of Code for wheteher link will be display or not
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Private Sub DisplayNoteList(ByVal strLcode As String)
        Try
            Dim objtaInputXml As New XmlDocument
            Dim objtaOutputXml As New XmlDocument
            Dim objxmlReader As XmlNodeReader
            Dim ds As New DataSet
            Dim objtaAgencyNotes As New AAMS.bizTravelAgency.bzAgencyNotes
            objtaInputXml.LoadXml("<MS_GETAGENCYNOTES_INPUT><LCode /><SORT_BY/><DESC/></MS_GETAGENCYNOTES_INPUT>")

            

            ''written by ashish
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "EmployeeID"
                objtaInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "EmployeeID" '"LOCATION_CODE"
            Else
                objtaInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objtaInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objtaInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            ''end here

            objtaInputXml.DocumentElement("LCode").InnerText = strLcode
            objtaOutputXml = objtaAgencyNotes.GetDetails(objtaInputXml)


            If objtaOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                objxmlReader = New XmlNodeReader(objtaOutputXml)
                ds.ReadXml(objxmlReader)
                gvAgencyNotes.DataSource = ds.Tables("AGENCYNOTES").DefaultView
                gvAgencyNotes.DataBind()

                SetImageForSorting(gvAgencyNotes)  ''ashish

            Else
                objxmlReader = New XmlNodeReader(objtaOutputXml)
                ds.ReadXml(objxmlReader)
                gvAgencyNotes.DataSource = ds.Tables("AGENCYNOTES").DefaultView
                gvAgencyNotes.DataBind()

                'lblError.Text = objtaOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception

        End Try

    End Sub
#Region " Set Image For Sorting"
    ''ashish for sorting
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

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        AddRecords()
        Dim strLcode As String = Session("Action").ToString().Split("|").GetValue(1)
        DisplayNoteList(strLcode)
    End Sub
    Private Sub AddRecords()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objtaAgencyNotes As New AAMS.bizTravelAgency.bzAgencyNotes
            objInputXml.LoadXml("<MS_UPDATEAGENCYNOTES_INPUT><AGENCYNOTES LCode='' EmployeeID='' Notes='' /></MS_UPDATEAGENCYNOTES_INPUT>")
            With objInputXml.DocumentElement.SelectSingleNode("AGENCYNOTES")
                If Not Session("Action").ToString().Split("|").GetValue(1) Is Nothing Then
                    .Attributes("LCode").Value() = Session("Action").ToString().Split("|").GetValue(1)
                Else
                    lblError.Text = "Lcode cannot be blank"
                    Return
                End If
                If Not Session("LoginSession") Is Nothing Then
                    Dim str As String()
                    str = Session("LoginSession").ToString().Split("|")
                    .Attributes("EmployeeID").Value() = str(0)
                Else
                    lblError.Text = "EmployeeID cannot be blank"
                    Return
                End If
                ' .Attributes("EmployeeID").Value() = "24"
                .Attributes("Notes").Value() = txtNotes.Text.Trim()
            End With
            objOutputXml = objtaAgencyNotes.Update(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messInsert
                txtNotes.Text = ""
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gvAgencyNotes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAgencyNotes.RowDataBound
        'Try
        '    If e.Row.RowIndex < 0 Then
        '        Exit Sub
        '    End If
        '    Dim lblNotes As Label
        '    'lblNotes
        '    lblNotes = e.Row.FindControl("lblNotes")
        '    lblNotes.Text = lblNotes.Text.Replace(vbCrLf, "<br />")
        'Catch ex As Exception
        '    lblError.Text = ex.Message
        'End Try
    End Sub

    Protected Sub LnkInstallationDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnkInstallationDetails.Click
        Dim StrFileNamexls As String
        Dim StrFileNamehtml As String
        Dim StrFileNameword As String
        Dim StrFileNamecsv As String
        Dim StrFileNametxt As String
        StrFileNamexls = String.Empty
        StrFileNamehtml = String.Empty
        StrFileNameword = String.Empty
        StrFileNamecsv = String.Empty
        StrFileNametxt = String.Empty
        Try
            If Session("Action") IsNot Nothing Then
                Dim strLcode As String = Session("Action").ToString().Split("|").GetValue(1)
                StrFileNamexls = strLcode + ".xls"
                Dim fullpathxls = Path.GetFullPath(Server.MapPath("~/AgencyNotes/") + StrFileNamexls)
                Dim name = Path.GetFileName(fullpathxls)
                Dim ext = Path.GetExtension(fullpathxls)
                Dim type As String = ""
                If File.Exists(fullpathxls) Then

                    If Not IsDBNull(ext) Then
                        ext = LCase(ext)
                    End If

                    Select Case ext
                        Case ".htm", ".html"
                            type = "text/HTML"
                        Case ".txt"
                            type = "text/plain"
                        Case ".doc", ".rtf"
                            type = "Application/msword"
                        Case ".csv", ".xls"
                            type = "Application/x-msexcel"
                        Case Else
                            type = "text/plain"
                    End Select

                    Response.AppendHeader("content-disposition", "attachment; filename=" + name)

                    If type <> "" Then
                        Response.ContentType = type
                    End If

                    Response.WriteFile(fullpathxls)
                    Response.End()
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


#Region "Code for sorting"
    ''ashish for sorting
    Protected Sub gvAgencyNotes_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvAgencyNotes.Sorting
        Try

            Dim strLocation_Code As String
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
            strLocation_Code = Session("Action").ToString().Split("|").GetValue(1)
            DisplayNoteList(strLocation_Code)

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

End Class
