Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data

Partial Class ISP_ISPUP_FeasibilityStatus
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblError.Text = ""
            Session("PageName") = "ISP/ISPUP_FeasibilityStatus.aspx"

            btnNew.Attributes.Add("onclick", "return NewFunction();")

            objeAAMS.ExpirePageCache()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspFeasibilityStatus']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspFeasibilityStatus']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("~/NoRights.aspx")
                    Exit Sub
                End If

                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            If (Not Page.IsPostBack) Then
                If Not Request.QueryString("Action") Is Nothing Then
                    If Request.QueryString("Action").ToUpper() = "U" Or Request.QueryString("Action").ToUpper() = "US" Then
                        ViewFeasibilityStatus()
                        txtFeasibilityStatus.Focus()
                    End If
                    If Request.QueryString("Action").ToUpper() = "US" Then
                        lblError.Text = objeAAMSMessage.messInsert
                    End If
                End If
            End If


            If Not Request.QueryString("Action") Is Nothing Then
                If Request.QueryString("Action").ToUpper() = "U" Or Request.QueryString("Action").ToUpper() = "US" Then
                    If strBuilder(2) = "0" Then
                        btnSave.Enabled = False
                    End If
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub ViewFeasibilityStatus()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objtFeasibilityStatusId As New AAMS.bizISP.bzISPFeasibleStatus
            objInputXml.LoadXml("<ISP_VIEWFEASIBILESTATUS_INPUT><FeasibleStatusID></FeasibleStatusID></ISP_VIEWFEASIBILESTATUS_INPUT>")

            '@ Getting  Decrrepted Data 
            Dim DecreptedFeasibleStatusID As String
            DecreptedFeasibleStatusID = objED.Decrypt(Request.QueryString("FeasibleStatusID").ToString())
            '@ End of Getting Decrepted Data
            objInputXml.DocumentElement.SelectSingleNode("FeasibleStatusID").InnerXml = DecreptedFeasibleStatusID

            'objInputXml.DocumentElement.SelectSingleNode("FeasibleStatusID").InnerXml = Request.QueryString("FeasibleStatusID").ToString()

            'Here Back end Method Call
            objOutputXml = objtFeasibilityStatusId.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                txtFeasibilityStatus.Text = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_STATUS").Attributes("Name").Value()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objtFeasibilityStatusId As New AAMS.bizISP.bzISPFeasibleStatus

        Try
            objInputXml.LoadXml("<ISP_UPDATEFEASIBILESTATUS_INPUT><FEASIBILE_STATUS FeasibleStatusID=''  Name=''/></ISP_UPDATEFEASIBILESTATUS_INPUT>")
            With objInputXml.DocumentElement.SelectSingleNode("FEASIBILE_STATUS")

                If Request.QueryString("ACTION").ToString().Trim().ToUpper = "U" Or Request.QueryString("ACTION").ToString().Trim().ToUpper = "US" Then
                    .Attributes("Name").Value() = txtFeasibilityStatus.Text.Trim()

                    '@ Getting  Decrepted Data 
                    Dim DecreptedFeasibleStatusID As String
                    DecreptedFeasibleStatusID = objED.Decrypt(Request.QueryString("FeasibleStatusID").ToString())
                    '@ End of Getting Decrepted Data

                    .Attributes("FeasibleStatusID").Value() = DecreptedFeasibleStatusID
                    ' .Attributes("FeasibleStatusID").Value() = Request.QueryString("FeasibleStatusID")
                Else
                    .Attributes("Name").Value() = txtFeasibilityStatus.Text.Trim()
                    .Attributes("FeasibleStatusID").Value() = String.Empty
                End If

            End With
            objOutputXml = objtFeasibilityStatusId.Update(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Dim CheckBoxOb As String = objOutputXml.DocumentElement.SelectSingleNode("FEASIBILE_STATUS").Attributes("FeasibleStatusID").Value().Trim()

                '@ Getting  Encrepted Data 
                Dim EncreptedFeasibleStatusID As String
                EncreptedFeasibleStatusID = objED.Encrypt(CheckBoxOb)
                '@ End of Getting Encrepted Data


                If (Request.QueryString("Action").ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().ToUpper() = "US") Then
                    lblError.Text = objeAAMSMessage.messUpdate
                Else
                    lblError.Text = objeAAMSMessage.messInsert

                    Response.Redirect("ISPUP_FeasibilityStatus.aspx?Action=US&FeasibleStatusID=" + EncreptedFeasibleStatusID)
                    'Response.Redirect("ISPUP_FeasibilityStatus.aspx?Action=U&FeasibleStatusID=" & CheckBoxOb)

                End If
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        If Not Request.QueryString("Action") Is Nothing Then
            If (Request.QueryString("Action").ToUpper() = "U" Or Request.QueryString("Action").ToUpper() = "US") Then
                ViewFeasibilityStatus()
                txtFeasibilityStatus.Focus()
            Else
                txtFeasibilityStatus.Text = ""
            End If
        End If
    End Sub
#Region "Code for Filter "
    'Code Added by Abhishek
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    'Code Added by Abhishek
#End Region
End Class
