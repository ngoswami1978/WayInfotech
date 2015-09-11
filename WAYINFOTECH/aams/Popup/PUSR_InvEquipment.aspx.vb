Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml

Partial Class Popup_PUSR_InvEquipment
    Inherits System.Web.UI.Page


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
            btnReset.Attributes.Add("onclick", "return ResetEquipment();")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            EquipmentSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub EquipmentSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim objSecurityXml As New XmlDocument
        Dim ds As New DataSet
        Dim objbzEquipment As New AAMS.bizMaster.bzEquipment
        objInputXml.LoadXml("<MS_SEARCHEQUIPMENT_INPUT><EGROUP_CODE /><EQUIPMENT_CODE /><DESCRIPTION /><CONFIG /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHEQUIPMENT_INPUT>")
        With objInputXml.DocumentElement
            .SelectSingleNode("EGROUP_CODE").InnerText = txtEquipGroup.Text.Trim()
            .SelectSingleNode("EQUIPMENT_CODE").InnerText = txtEquipmentCode.Text.Trim()
            .SelectSingleNode("DESCRIPTION").InnerText = txtEquipmentDesc.Text.Trim()
            .SelectSingleNode("CONFIG").InnerText = txtEquipConfig.Text.Trim()
        End With
        'Here Back end Method Call
        objOutputXml = objbzEquipment.Search(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            grdEquipment.DataSource = ds.Tables("EQUIPMENT")
            grdEquipment.DataBind()
            lblError.Text = ""
        Else
            grdEquipment.DataSource = String.Empty
            grdEquipment.DataBind()
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Protected Sub grdEquipment_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdEquipment.ItemCommand
        If e.CommandName = "SelectX" Then
            Dim strjscript As String = "<script language=""javascript"">"
            strjscript &= "window.returnValue='" + e.CommandArgument.ToString() + "'; window.close();"
            strjscript = strjscript & "</script" & ">"
            Me.litEmployee.Text = strjscript
        End If
    End Sub
End Class
