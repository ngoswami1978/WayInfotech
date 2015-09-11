Imports System.Data
Imports System.Xml
Partial Class Inventory_InvSR_EquipmentList
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            EquipmentSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = Request.Url.ToString

            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='EquipmentMaster']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='EquipmentMaster']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                  
                End If
            Else
                objeAAMS.SecurityCheck(31)
            End If
            If Not Page.IsPostBack Then
                btnSelect.Visible = False
                objeAAMS.BindDropDown(drpEquipmentGroup, "EQUIPMENTGROUP", True, 3)
            End If
          
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub EquipmentSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzEquipment As New AAMS.bizMaster.bzEquipment
        Try
            objInputXml.LoadXml("<MS_SEARCHEQUIPMENT_INPUT><EGROUP_CODE /><EQUIPMENT_CODE /><DESCRIPTION /><CONFIG /><SegExpected></SegExpected><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHEQUIPMENT_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("EQUIPMENT_CODE").InnerText = Trim(txtEquipmentType.Text)
            objInputXml.DocumentElement.SelectSingleNode("DESCRIPTION").InnerText = (txtEquipmentDescription.Text)
            If (drpEquipmentGroup.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("EGROUP_CODE").InnerText = drpEquipmentGroup.SelectedItem.Text
            End If
            objInputXml.DocumentElement.SelectSingleNode("CONFIG").InnerText = Trim(txtEquipmentConfiguration.Text)
          
            objInputXml.DocumentElement.SelectSingleNode("SegExpected").InnerText = ""

            objOutputXml = objbzEquipment.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                ViewState("PrevSearching") = objInputXml.OuterXml

                grdEquipment.DataSource = ds.Tables("EQUIPMENT")
                grdEquipment.DataBind()

                btnSelect.Visible = True
            Else
                grdEquipment.DataSource = String.Empty
                grdEquipment.DataBind()
                btnSelect.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect(Request.Url.ToString, False)
    End Sub
End Class
