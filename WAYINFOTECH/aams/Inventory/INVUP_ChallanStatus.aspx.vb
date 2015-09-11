'#########################################################################
'############   Page Name - Inventory_INVUP_ChallanStatus          #######
'############   Date 26-March 2008    ####################################
'############   Developed By Abhishek  ###################################
'#########################################################################
#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data
#End Region
Partial Class Inventory_INVUP_ChallanStatus
    Inherits System.Web.UI.Page
#Region "Page level variable/Objects Declaration"
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
#End Region
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objInputXml As New XmlDocument
            Session("PageName") = Request.Url.ToString()
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            btnSave.Attributes.Add("onclick", "return  CheckMandatoty();")

            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CHALLAN STATUS']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CHALLAN STATUS']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                End If

            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If


            If (Not IsPostBack) Then
                objeAAMS.BindDropDown(drpChallanStatus, "CHALLANSTATUS", True)
                If (Request.QueryString("RowId") IsNot Nothing) Then
                    'drpChallanStatus.SelectedValue = Request.QueryString("ChStatus")
                End If
                If (Request.QueryString("ChType") IsNot Nothing) Then

                End If

                If (Request.QueryString("ChStatus") IsNot Nothing) Then
                    drpChallanStatus.SelectedValue = Request.QueryString("ChStatus")
                End If
                If (Request.QueryString("ChDate") IsNot Nothing) Then
                    If Request.QueryString("ChDate").Trim().Length > 0 Then
                        txtChallanDate.Text = Request.QueryString("ChDate")
                    End If
                    ' Format(hdChallanDate.Value, "dd/mm/yyyy")
                    'txtChallanDate.Text = Request.QueryString("ChDate") 'objeAAMS.ConvertDateBlank(Request.QueryString("ChDate"))
                End If
                If (Request.QueryString("Msg") IsNot Nothing) Then
                    lblError.Text = objeAAMSMessage.messUpdate
                End If

            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect("INVUP_ChallanStatus.aspx", False)
            'If (Request.QueryString("ChStatus") IsNot Nothing) Then
            '    drpChallanStatus.SelectedValue = Request.QueryString("ChStatus")
            'End If
            'If (Request.QueryString("ChDate") IsNot Nothing) Then
            '    If Request.QueryString("ChDate").Trim().Length > 0 Then
            '        txtChallanDate.Text = Format(CDate(Request.QueryString("ChDate")), "dd/MM/yyyy")
            '    End If
            '    ' Format(hdChallanDate.Value, "dd/mm/yyyy")
            '    'txtChallanDate.Text = Request.QueryString("ChDate") 'objeAAMS.ConvertDateBlank(Request.QueryString("ChDate"))
            'End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If (IsValid) Then
                If (Request.QueryString("RowId") IsNot Nothing) Then
                    'drpChallanStatus.SelectedValue = Request.QueryString("ChStatus")
               

                    Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                    'Dim objXmlReader As XmlNodeReader
                    Dim ds As New DataSet
                    Dim objbzChallan As New AAMS.bizInventory.bzChallan
                    objInputXml.LoadXml("<INV_UPDATECHALLANSTATUS_INPUT><CHALLAN ACTION='' ROWID='' CHALLANDATE='' ChallanStatus='' CHALLANTYPE='' PCDATE='' /></INV_UPDATECHALLANSTATUS_INPUT>")

                    objInputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("ACTION").Value = "U"
                    If (Request.QueryString("RowId") IsNot Nothing) Then
                        objInputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("ROWID").Value = Request.QueryString("RowId")
                    End If

                    If (Request("txtChallanDate") IsNot Nothing) Then
                        objInputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("CHALLANDATE").Value = objeAAMS.ConvertTextDate(Request("txtChallanDate").ToString())
                        hdChallanDate.Value = Request("txtChallanDate").ToString()
                        'If hdChallanDate.Value.Trim.Length > 0 Then
                        '    hdChallanDate.Value = hdChallanDate.Value.Split("/")(1) + "/" + hdChallanDate.Value.Split("/")(0) + "/" + hdChallanDate.Value.Split("/")(2)
                        '    hdChallanDate.Value = Format(CDate(hdChallanDate.Value), "dd-MMM-yy")
                        'End If

                    Else
                        objInputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("CHALLANDATE").Value = objeAAMS.ConvertTextDate(txtChallanDate.Text)
                        hdChallanDate.Value = txtChallanDate.Text
                        If hdChallanDate.Value.Trim.Length > 0 Then
                            hdChallanDate.Value = hdChallanDate.Value.Split("/")(1) + "/" + hdChallanDate.Value.Split("/")(0) + "/" + hdChallanDate.Value.Split("/")(2)
                            hdChallanDate.Value = Format(CDate(hdChallanDate.Value), "dd-MMM-yy")
                        End If

                    End If
                    If (Request.QueryString("PCDate") IsNot Nothing) Then
                        If Request.QueryString("PCDate").Trim().Length > 0 Then
                            hdPCDate.Value = Format(CDate(Request.QueryString("PCDate")), "dd/MM/yyyy")
                        End If
                    End If
                    objInputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("PCDATE").Value = objeAAMS.ConvertTextDate(hdPCDate.Value)

                    objInputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("ChallanStatus").Value = drpChallanStatus.SelectedValue


                    objInputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("CHALLANTYPE").Value = Request.QueryString("ChType")

                    If hdPCDate.Value.Trim.Length > 0 Then
                        hdPCDate.Value = hdPCDate.Value.Split("/")(1) + "/" + hdPCDate.Value.Split("/")(0) + "/" + hdPCDate.Value.Split("/")(2)
                        hdPCDate.Value = Format(CDate(hdPCDate.Value), "dd-MMM-yy")
                    End If
                    ''Here Back end Method Call
                    objOutputXml = objbzChallan.UpdateChallanStatus(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        ' lblError.Text = objeAAMSMessage.messUpdate ' "Record updated successfully."  
                        Response.Redirect("INVUP_ChallanStatus.aspx?Msg=U&Popup=T&RowId=" + Request.QueryString("RowId") + "&ChStatus=" + drpChallanStatus.SelectedValue + "&ChDate=" + hdChallanDate.Value + "&ChType=" + Request.QueryString("ChType") + "&PCDate=" + hdPCDate.Value, False)
                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                Else
                    lblError.Text = "Incomplete Parameter"
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
