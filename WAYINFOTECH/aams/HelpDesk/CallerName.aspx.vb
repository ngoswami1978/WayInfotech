
Partial Class HelpDesk_CallerName
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objbzAgencyStaff As New AAMS.bizTravelAgency.bzAgencyStaff
        Dim objInputXml As New XmlDocument
        Dim objOXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument

        Try
            objInputXml.LoadXml("<TA_SEARCHSTAFF_INPUT><STAFFNAME></STAFFNAME><AGENCYNAME></AGENCYNAME><OFFICEID/><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><EmployeeID/><Source/></TA_SEARCHSTAFF_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("STAFFNAME").InnerText = Request.QueryString("q")
            objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = "rao" 'strContextKey.Split("|").GetValue(0)
            objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerText = "DELVS382G" 'strContextKey.Split("|").GetValue(1)

            '   objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = strContextKey.Split("|").Get Value(2)
            '  objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = strContextKey.Split("|").GetValue(3)
            ' objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = strContextKey.Split("|").GetValue(4)
            objInputXml.DocumentElement.SelectSingleNode("Source").InnerText = "Helpdesk"

            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        End If
                    End If
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        End If
                    End If
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        If Not Session("LoginSession") Is Nothing Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = Session("LoginSession").ToString().Split("|")(0)
                        End If
                    End If
                End If

            End If
            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If

            objOXml = objbzAgencyStaff.Search(objInputXml)

            If objOXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                For Each objNode As XmlNode In objOXml.DocumentElement.SelectNodes("STAFF")
                    Response.Write(objNode.Attributes("STAFFNAME").InnerText + Environment.NewLine)
                Next

            Else
                Response.Write("No Record Found")
            End If

        Catch ex As Exception
        End Try
    End Sub
End Class
