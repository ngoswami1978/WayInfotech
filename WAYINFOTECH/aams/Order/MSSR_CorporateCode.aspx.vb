'#############################################################
'############   Page Name -- Order_MSSR_CorporateCode  #######
'############   Date 29-November 2007  ########################
'############   Developed By Abhishek  #######################
'#############################################################
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data

Partial Class Order_MSSR_CorporateCode
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objInputXml As New XmlDocument
            Session("PageName") = "Order/Order_MSSR_CorporateCode"
            ' This code is used for Expiration of Page From Cache
            'objeAAMS.ExpirePageCache()
            '' This code is usedc for checking session handler according to user login.
            'If Session("Security") Is Nothing Then
            '    ClientScript.RegisterStartupScript(Me.GetType(),"loginScript", objeAAMS.CheckSession())

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
                btnSearch.Attributes.Add("onclick", "return CorporateCodeMandatory();")
                btnReset.Attributes.Add("onclick", "return CorporateCodeReset();")
                CorporateCodeSearch()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Try
                lblError.Text = ""
                CorporateCodeSearch()
            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("MSUP_CorporateCode.aspx?Action=I")
    End Sub
    Private Sub CorporateCodeSearch()
        'Dim objInputXml, objOutputXml As New XmlDocument
        'Dim objXmlReader As XmlNodeReader
        'Dim ds As New DataSet
        Dim dtCorporateCodeDetails As New DataTable
        dtCorporateCodeDetails.Columns.Add("ChainID", GetType(Integer))
        dtCorporateCodeDetails.Columns.Add("CCode", GetType(String))
        dtCorporateCodeDetails.Columns.Add("CQualifier", GetType(String))
        dtCorporateCodeDetails.Columns.Add("Description", GetType(String))
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
            DrowCorporateCodeDetails("ChainID") = 1
            DrowCorporateCodeDetails("CCode") = "CD"
            DrowCorporateCodeDetails("CQualifier") = "1"
            DrowCorporateCodeDetails("Description") = "Good"
            dtCorporateCodeDetails.Rows.Add(DrowCorporateCodeDetails)
            DrowCorporateCodeDetails = dtCorporateCodeDetails.NewRow()
            DrowCorporateCodeDetails("ChainID") = 2
            DrowCorporateCodeDetails("CCode") = "AB"
            DrowCorporateCodeDetails("CQualifier") = "2"
            DrowCorporateCodeDetails("Description") = "Very Good"
            dtCorporateCodeDetails.Rows.Add(DrowCorporateCodeDetails)
            DrowCorporateCodeDetails = dtCorporateCodeDetails.NewRow()
            DrowCorporateCodeDetails("ChainID") = 3
            DrowCorporateCodeDetails("CCode") = "ED"
            DrowCorporateCodeDetails("CQualifier") = "3"
            DrowCorporateCodeDetails("Description") = "Very Very  good"
            dtCorporateCodeDetails.Rows.Add(DrowCorporateCodeDetails)

            gvCorporateCode.DataSource = dtCorporateCodeDetails
            gvCorporateCode.DataBind()

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvCorporateCode_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvCorporateCode.RowCommand
        Try
            'Code for Edit Data
            If e.CommandName = "EditX" Then
                Response.Redirect("MSUP_CorporateCode.aspx?Action=U&ChainID=" & e.CommandArgument)
            End If
            'Code for Delete Data
            If e.CommandName = "DeleteX" Then
                'CorporateCodeDelete(e.CommandArgument)
                'CorporateCodeSearch()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvCorporateCode_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCorporateCode.RowDataBound
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
End Class
