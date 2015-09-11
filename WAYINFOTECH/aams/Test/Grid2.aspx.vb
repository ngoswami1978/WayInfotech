Imports System.Data
Partial Class Test_Grid2
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim dt As New DataTable
        dt.Columns.Add("Id")
        dt.Columns.Add("Name")
        Dim dr As DataRow
        dr = dt.NewRow
        dr(0) = 1
        dr(1) = "Ram"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr(0) = 2
        dr(1) = "Mohan"
        dt.Rows.Add(dr)

        GridView1.DataSource = dt
        GridView1.DataBind()

    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Try
            If e.CommandName = "EditX" Then


                Dim dt As New DataTable
                dt.Columns.Add("Id")
                dt.Columns.Add("Name")
                Dim dr As DataRow
                For Each gv As GridViewRow In GridView1.Rows

                    dr = dt.NewRow
                    dr(0) = gv.Cells(0).Text
                    dr(1) = gv.Cells(1).Text
                    dt.Rows.Add(dr)
                Next

                Dim dv As DataView = dt.DefaultView
                dv.RowFilter = "Id='" + e.CommandArgument.ToString + "'"
                txtId.Text = dv(0)(0).ToString
                txtName.Text = dv(0)(1).ToString


            End If
            If e.CommandName = "DeleteX" Then
                Dim dt As New DataTable
                dt.Columns.Add("Id")
                dt.Columns.Add("Name")
                Dim dr As DataRow
                For Each gv As GridViewRow In GridView1.Rows
                    If e.CommandArgument.ToString <> gv.Cells(0).Text Then
                        dr = dt.NewRow
                        dr(0) = gv.Cells(0).Text
                        dr(1) = gv.Cells(1).Text
                        dt.Rows.Add(dr)
                    End If
                Next
                GridView1.DataSource = dt
                GridView1.DataBind()

            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing

    End Sub
End Class
