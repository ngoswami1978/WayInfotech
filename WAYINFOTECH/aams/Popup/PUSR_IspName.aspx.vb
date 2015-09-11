'#######################################################
'############   Page Name --PUSR_IspName  #########  
'############   Date 21 Jan 2008  #################
'############   Developed By Abhishek  #################
'#######################################################
Imports System.Data

Partial Class Popup_PUSR_IspName
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objeAAMSMessage As New eAAMSMessage


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
            '   Dim Lcode As String
            Dim objInputXmlAgency, objOutputXmlAgency As New XmlDocument
            Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If
            btnReset.Attributes.Add("onclick", "return ISPReset();")
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Isp']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Isp']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        Response.Redirect("../NoRights.aspx")
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            If Not Page.IsPostBack Then

                objeAAMS.BindDropDown(drpCityName, "CITY", True)
            End If
            If Request.QueryString("CityNmae") IsNot Nothing Then
                Dim li As New ListItem
                li = drpCityName.Items.FindByText(Request.QueryString("CityNmae"))
                If (li IsNot Nothing) Then
                    li.Selected = True
                End If
            End If
            
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
      
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbzISP As New AAMS.bizISP.bzISP
            objInputXml.LoadXml("<IS_SEARCHISP_INPUT><Name></Name><CityID></CityID></IS_SEARCHISP_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("Name").InnerText = txtISPName.Text.Trim()
            objInputXml.DocumentElement.SelectSingleNode("CityID").InnerText = drpCityName.SelectedValue
            'Here Back end Method Call
            objOutputXml = objbzISP.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdvISP.DataSource = ds.Tables("ISP").DefaultView
                grdvISP.DataBind()
                lblError.Text = ""
            Else
                grdvISP.DataSource = Nothing
                grdvISP.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    
    Protected Sub grdvISP_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdvISP.RowCommand
        Try
            Dim strjscript As String = "<script language=""javascript"">"
            strjscript &= "window.returnValue='" + e.CommandArgument.ToString() + "'; window.close();"
            strjscript = strjscript & "</script" & ">"
            Me.litIspName.Text = strjscript
        Catch ex As Exception
        End Try
    End Sub

   
End Class
