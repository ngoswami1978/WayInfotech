
Partial Class LoginControlPanel
    Inherits System.Web.UI.Page

    Protected Sub gvLogin_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvLogin.RowCommand
        Dim strID As String = ""
        Dim rowIndex As Integer
        Dim primaryKey(1) As String
        Try
            If e.CommandName = "LogOutX" Then
                strID = e.CommandArgument.ToString()
                primaryKey(0) = strID.Split("|").GetValue(0)
                primaryKey(1) = strID.Split("|").GetValue(1)
                rowIndex = CType(Application("LoginLog"), DataSet).Tables("UserLog").Rows.IndexOf(CType(Application("LoginLog"), DataSet).Tables("UserLog").Rows.Find(primaryKey))
                If rowIndex <> -1 Then
                    CType(Application("LoginLog"), DataSet).Tables("UserLog").Rows(rowIndex).Delete()
                    CType(Application("LoginLog"), DataSet).Tables("UserLog").AcceptChanges()
                    Bindata()
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblError.Text = ""
            If Not Page.IsPostBack Then
                Bindata()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Sub Bindata()
        Dim dt As New DataTable
        Dim dv As DataView
        dt = CType(Application("dtUserApplicationDetails"), DataTable)
        dv = dt.DefaultView
        '  If txtUserName.Text <> "" Then
        dv.RowFilter = "UserId like  '%" + txtUserName.Text.Replace("'", "''") + "%'"
        'End If
        If dv.Count > 0 Then
            '@ Code For Sorting
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "UserId"
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
            gvLogin.DataSource = dv
            gvLogin.DataBind()
            txtRecordCount.Text = dv.Count
            SetImageForSorting(gvLogin)
            pnlPaging.Visible = True
        Else
            gvLogin.DataSource = Nothing
            gvLogin.DataBind()
            lblError.Text = "Currently No User in Login State"
            txtRecordCount.Text = 0
            pnlPaging.Visible = False
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim dt As New DataTable
        Dim dv As DataView
        Try

            dt = CType(Application("dtUserApplicationDetails"), DataTable)
            dv = dt.DefaultView
            ' If txtUserName.Text <> "" Then
            dv.RowFilter = "UserId like  '%" + txtUserName.Text.Replace("'", "''") + "%'"
            '  End If
            If dv.Count > 0 Then
                '@ Code For Sorting
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "UserId"
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


                gvLogin.DataSource = dv
                gvLogin.DataBind()
                txtRecordCount.Text = dv.Count
                SetImageForSorting(gvLogin)
                pnlPaging.Visible = True
            Else
                gvLogin.DataSource = Nothing
                gvLogin.DataBind()
                lblError.Text = "Currently No User in Login State"
                txtRecordCount.Text = ""
                pnlPaging.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            gvLogin.DataSource = Nothing
            gvLogin.DataBind()
            txtRecordCount.Text = ""
            pnlPaging.Visible = False
        End Try

    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("LoginControlPanel.aspx")
    End Sub

    Protected Sub gvLogin_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvLogin.Sorted

    End Sub

    Protected Sub gvLogin_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvLogin.Sorting
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
            Bindata()

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
