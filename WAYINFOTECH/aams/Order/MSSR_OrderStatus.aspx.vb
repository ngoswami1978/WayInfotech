'##############################################################
'############   Page Name -- Order_MSSR_OrderStatus  ##########
'############   Date 29-November 2007  ########################
'############   Developed By Abhishek  ########################
'##############################################################
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data
Partial Class Order_MSSR_OrderStatus
    Inherits System.Web.UI.Page

    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            Dim objInputXml As New XmlDocument
            Session("PageName") = "Order/Order_MSSR_OrderStatus"
            ' This code is used for Expiration of Page From Cache
            'objeAAMS.ExpirePageCache()
            '' This code is usedc for checking session handler according to user login.
            'If Session("Security") Is Nothing Then
            '    Page.RegisterStartupScript("loginScript", objeAAMS.CheckSession())

            'End If
            '' #######################################
            '' ########## This Code code is used for enable/disable 
            '' ########## the button according to rights
            'Dim objSecurityXml As New XmlDocument
            'objSecurityXml.LoadXml(Session("Security"))
            'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='City']").Count <> 0 Then
            '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='City']").Attributes("Value").Value)
            '        If strBuilder(0) = "0" Then
            '            btnSearch.Enabled = False
            '        End If
            '        If strBuilder(1) = "0" Then
            '            btnNew.Enabled = False
            '        End If
            '    End If
            'Else
            '    strBuilder = objeAAMS.SecurityCheck(31)
            'End If
            ' #######################################
            ' ########## End of code used for enable/disable 
            ' ########## the button according to rights
            If Not Page.IsPostBack Then
                'btnSearch.Attributes.Add("onclick", "return OrderStatusMandatory();")
                btnReset.Attributes.Add("onclick", "return OrderStatusReset();")
                OrderStatusSearch()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Try
                lblError.Text = ""
                OrderStatusSearch()
            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("MSUP_OrderStatus.aspx?Action=I")
    End Sub
    Private Sub OrderStatusSearch()
        'Dim objInputXml, objOutputXml As New XmlDocument
        'Dim objXmlReader As XmlNodeReader
        'Dim ds As New DataSet
        Dim dtCorporateCodeDetails As New DataTable
        dtCorporateCodeDetails.Columns.Add("ID", GetType(Integer))
        dtCorporateCodeDetails.Columns.Add("OrderStatus", GetType(String))
     
        Dim DrowCorporateCodeDetails As DataRow
        Try
            'Dim objbzCity As New AAMS.bizMaster.bzCity

            'objInputXml.LoadXml("<MS_SEARCHCITY_INPUT><City_Name></City_Name></MS_SEARCHCITY_INPUT>")
            'objInputXml.DocumentElement.SelectSingleNode("City_Name").InnerText = txtCtyName.Text
            ''Here Back end Method Call
            'objOutputXml = objbzCity.Search(objInputXml)

            'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '    objXmlReader = New XmlNodeReader(objOutputXml)
            '    ds.ReadXml(objXmlReader)
            '    gvCity.DataSource = ds.Tables("CITY_DETAIL")
            '    gvCity.DataBind()
            'Else
            '    gvCity.DataSource = Nothing
            '    gvCity.DataBind()
            '    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            'End If
            DrowCorporateCodeDetails = dtCorporateCodeDetails.NewRow()
            DrowCorporateCodeDetails("ID") = 1
            DrowCorporateCodeDetails("OrderStatus") = "Completed"
            dtCorporateCodeDetails.Rows.Add(DrowCorporateCodeDetails)

            DrowCorporateCodeDetails = dtCorporateCodeDetails.NewRow()
            DrowCorporateCodeDetails("ID") = 2
            DrowCorporateCodeDetails("OrderStatus") = "Pending"
            dtCorporateCodeDetails.Rows.Add(DrowCorporateCodeDetails)

            DrowCorporateCodeDetails = dtCorporateCodeDetails.NewRow()
            DrowCorporateCodeDetails("ID") = 3
            DrowCorporateCodeDetails("OrderStatus") = "All"
            dtCorporateCodeDetails.Rows.Add(DrowCorporateCodeDetails)

            DrowCorporateCodeDetails = dtCorporateCodeDetails.NewRow()
            DrowCorporateCodeDetails("ID") = 4
            DrowCorporateCodeDetails("OrderStatus") = "In Process"
            dtCorporateCodeDetails.Rows.Add(DrowCorporateCodeDetails)
            gvOrderStatus.DataSource = dtCorporateCodeDetails
            gvOrderStatus.DataBind()

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

 

  
    Sub CorporateCodeDelete(ByVal strCityID As String)
        Try
            'Dim objInputXml, objOutputXml As New XmlDocument
            'Dim objbzCity As New AAMS.bizMaster.bzCity
            'objInputXml.LoadXml("<MS_DELETECITY_INPUT><CityID></CityID></MS_DELETECITY_INPUT>")
            'objInputXml.DocumentElement.SelectSingleNode("CityID").InnerXml = strCityID
            ''Here Back end Method Call
            'objOutputXml = objbzCity.Delete(objInputXml)
            'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '    lblError.Text = objeAAMSMessage.messDelete '"Record deleted successfully."
            'Else
            '    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            'End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvOrderStatus_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvOrderStatus.RowCommand
        Try
            'Code for Edit Data
            If e.CommandName = "EditX" Then
                Response.Redirect("MSUP_OrderStatus.aspx?Action=U&ID=" & e.CommandArgument)
            End If
            'Code for Delete Data
            If e.CommandName = "DeleteX" Then
                'CorporateCodeDelete(e.CommandArgument)
                'OrderStatusSearch()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvOrderStatus_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvOrderStatus.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim btnDelete, btnEdit As LinkButton
            btnDelete = CType(e.Row.FindControl("btnDelete"), LinkButton)
            btnEdit = CType(e.Row.FindControl("btnEdit"), LinkButton)

            'If strBuilder(3) = "0" Then
            '    btnDelete.Enabled = False
            'Else
            btnDelete.Attributes.Add("OnClick", "return ConfirmDelete();")
            'End If
            'If strBuilder(2) = "0" Then
            '    btnEdit.Enabled = False
            'End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class

