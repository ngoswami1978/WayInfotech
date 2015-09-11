Imports System.Xml
Partial Class Setup_MSSR_ChangePassword
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As StringBuilder

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
            Session("PageName") = "Setup/MSSR_ChangePassword.aspx"
            btnChange.Attributes.Add("onclick", "return CheckNewConfirmPassword();")
            btnReset.Attributes.Add("onclick", "return ConfirmPasswordReset();")
            objeAAMS.ExpirePageCache()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then

                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Change Password']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Change Password']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnChange.Enabled = False
                    End If
                    If strBuilder(1) = "0" Then
                        btnChange.Enabled = False
                    End If
                    'Code End for security
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    '***************************************************************************************************************
    '****************************Code for Change Password**********************************************************
    '***************************************************************************************************************
    Protected Sub btnChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChange.Click
        Dim objInputXml As New XmlDocument
        Dim objOutputXml As New XmlDocument
        Dim objbzEmployee As New WAY.bizMaster.bzEmployee
        Dim objSessionXml As New XmlDocument

        ''Done by Neeraj Goswami
        Dim objOutputXmlReserverUserPassword As New XmlDocument
        Dim strFIELD_VALUE As String = ""
        Dim strArr() As String
        Dim blnReserverPassFound As Boolean = False
        objOutputXmlReserverUserPassword = objbzEmployee.GetReserveUsernamePassword()

        If objOutputXmlReserverUserPassword.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            strFIELD_VALUE = objOutputXmlReserverUserPassword.DocumentElement.SelectSingleNode("RESERVE").Attributes("FIELD_VALUE").Value
            strArr = strFIELD_VALUE.Split(",")
            For count As Integer = 0 To strArr.Length - 1
                If txtNewPassword.Text.Trim().ToString().ToUpper().Contains(strArr(count).ToString().Trim().ToUpper()) Then
                    blnReserverPassFound = True
                    Exit For
                End If
            Next
        Else
            lblError.Text = objOutputXmlReserverUserPassword.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If

        If blnReserverPassFound = True Then
            lblError.Text = "You cannot use password keyword as New password/please try again.."
            Exit Sub
        End If

        objSessionXml.LoadXml(Session("Security").ToString())
        objInputXml.LoadXml("<MS_CHANGEPASSWORD_INPUT><EmployeeID></EmployeeID><OldPassword></OldPassword><NewPassword></NewPassword></MS_CHANGEPASSWORD_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = objSessionXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText
        objInputXml.DocumentElement.SelectSingleNode("OldPassword").InnerText = txtOldPassword.Text
        objInputXml.DocumentElement.SelectSingleNode("NewPassword").InnerText = txtNewPassword.Text
        'Here Back end Method Call
        objOutputXml = objbzEmployee.ChangePassword(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            lblError.Text = objeAAMSMessage.messPassword
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
End Class
