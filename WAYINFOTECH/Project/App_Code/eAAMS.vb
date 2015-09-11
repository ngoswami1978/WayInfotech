Imports System.Xml
Imports System.Data
Imports Microsoft.VisualBasic
Imports WAY.bizMaster
Imports WAY
Imports WAY.bizShared.bzShared
Imports System.Globalization

Public Class eAAMS
    Public Sub BindDropDown(ByVal drpDownList As DropDownList, ByVal strType As String, ByVal bolSelect As Boolean, Optional ByVal SelectOption As Integer = 1)
        Dim objOutputXml As XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Select Case strType

            '' ######################################## Start of Code ########################################
            ''@ This is used in productivity/market module for agency group classification type like AA/AB/AC etc.
            'Case "AGENCYGROUPCLASSTYPE"
            '    Dim ObjbzAgencyType As New WAY.bizTravelAgency.bzAgencyType
            '    objOutputXml = New XmlDocument
            '    objOutputXml = ObjbzAgencyType.ListGroupAgencyType
            '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '        objXmlReader = New XmlNodeReader(objOutputXml)
            '        ds.ReadXml(objXmlReader)
            '        drpDownList.DataSource = ds.Tables("AGENCYGROUPTYPE")
            '        drpDownList.DataTextField = "TYPE"
            '        drpDownList.DataValueField = "TYPEID"
            '        drpDownList.DataBind()
            '    End If
            '    '@ This is used in productivity/market module for agency group classification type like AA/AB/AC etc.
            '    ' ######################################## End of Code ########################################

            'Case "GROUP_CLASSIFICATION"
            '    Dim objAgencyGroup As New WAY.bizMaster.bzAgencyGroup
            '    objOutputXml = New XmlDocument
            '    objOutputXml = objAgencyGroup.ListGroupClassificationType()
            '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '        objXmlReader = New XmlNodeReader(objOutputXml)
            '        ds.ReadXml(objXmlReader)
            '        drpDownList.DataSource = ds.Tables("GROUPCLASSIFICATION_TYPE")
            '        drpDownList.DataTextField = "Group_Classification_Name"
            '        drpDownList.DataValueField = "GroupClassificationID"
            '        drpDownList.DataBind()
            '    End If
        End Select
        If SelectOption = 1 Then
            drpDownList.Items.Insert(0, New ListItem("--Select One--", "")) ' In Search/New/Edit Form For Mandatory Field
        ElseIf SelectOption = 2 Then
            drpDownList.Items.Insert(0, New ListItem("", "")) 'In Edit/New Form For Non Mandatory Field
        ElseIf SelectOption = 3 Then
            drpDownList.Items.Insert(0, New ListItem("--All--", "")) ' In Search Form For Non Mandatory Field
        ElseIf SelectOption = 4 Then
        End If
    End Sub
    Public Function CheckSession() As String
        Dim loginScript As String = ""
        loginScript = " <noscript><meta http-equiv='REFRESH' content='1; URL=../Information.aspx' /> </noscript><script language='javascript'>" & _
       "parent.location.href='../SupportPages/TimeOutSession.aspx?Logout=True'; " & _
       "</script>"
        Return loginScript
    End Function
    Public Function ConvertDate(ByVal intDate As String) As Date
        Try
            Dim dtDateFrom As New Date(Left(CType(intDate, String), 4), Mid(CType(intDate, String), 5, 2), Right(CType(intDate, String), 2))
            Return dtDateFrom
        Catch ex As Exception
            Return CDate("1/1/1900")
        End Try
    End Function

    Public Function ConvertTextDate(ByVal dt As String) As String '''''''to convert dd/mm/yyyy to yyyymmdd
        Dim lstrarrDate As Array
        Dim lstrdtyear As String
        Dim lstrdtmonth As String
        Dim lstrdtday As String
        Dim lstrDate As String
        If dt <> "" Then
            lstrarrDate = Split(dt, "/", -1, 1)
            lstrdtyear = lstrarrDate(2)
            lstrdtmonth = lstrarrDate(1)
            lstrdtday = lstrarrDate(0)
            If CType(lstrarrDate(1), String).Length = 1 Then
                lstrdtmonth = "0" + lstrarrDate(1)
            Else
                lstrdtmonth = lstrarrDate(1)
            End If
            If CType(lstrarrDate(0), String).Length = 1 Then
                lstrdtday = "0" + lstrarrDate(0)
            Else
                lstrdtday = lstrarrDate(0)
            End If

            lstrDate = lstrdtyear & lstrdtmonth & lstrdtday
            Return lstrDate
        Else
            Return dt
        End If
    End Function
    Public Sub ExpirePageCache()
        'HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'HttpContext.Current.Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        'HttpContext.Current.Response.Cache.SetLastModified(DateTime.Now)
        'HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(False)
    End Sub
    Public Function SecurityCheck(ByVal intValue As Integer) As StringBuilder
        Dim builSecurity As New StringBuilder
        Dim ViewRight, AddRight, ModifyRight, DeleteRight, PrintRight As String
        ViewRight = 0 : AddRight = 0 : ModifyRight = 0 : DeleteRight = 0 : PrintRight = 0

        'Code for restricting user to log multiple times at a same time 

        If System.Configuration.ConfigurationManager.AppSettings("MULTIPLE_LOGIN") IsNot Nothing Then
            If System.Configuration.ConfigurationManager.AppSettings("MULTIPLE_LOGIN").ToString = "1" Then
                GoTo StartFromHere
            End If
            Dim str As String = RestrictMultiUserByMultiIp()
            If str <> "True" Then
                HttpContext.Current.Session("RestrictedUser") = "Yes"
                builSecurity.Append(ViewRight)
                'Index 1 Add
                builSecurity.Append(AddRight)
                'Index 2 Modify
                builSecurity.Append(ModifyRight)
                'Index 3 Delete
                builSecurity.Append(DeleteRight)
                'Index 4 Print
                builSecurity.Append(PrintRight)
                Return builSecurity
                Exit Function
            Else
                HttpContext.Current.Session("RestrictedUser") = "No"
            End If
        Else
            Dim str As String = RestrictMultiUserByMultiIp()
            If str <> "True" Then
                HttpContext.Current.Session("RestrictedUser") = "Yes"
                builSecurity.Append(ViewRight)
                'Index 1 Add
                builSecurity.Append(AddRight)
                'Index 2 Modify
                builSecurity.Append(ModifyRight)
                'Index 3 Delete
                builSecurity.Append(DeleteRight)
                'Index 4 Print
                builSecurity.Append(PrintRight)
                Return builSecurity
                Exit Function
            Else
                HttpContext.Current.Session("RestrictedUser") = "No"
            End If
            'End code
        End If

StartFromHere:
        Select Case intValue
            Case 1
                ViewRight = "1"
            Case 2, 3
                ViewRight = "1"
                AddRight = "1"
            Case 4, 5
                ViewRight = "1"
                ModifyRight = "1"
            Case 6, 7
                ViewRight = "1"
                ModifyRight = "1"
                AddRight = "1"
            Case 8, 9
                ViewRight = "1"
                DeleteRight = "1"
            Case 10, 11
                ViewRight = "1"
                DeleteRight = "1"
                AddRight = "1"
            Case 12, 13
                ViewRight = "1"
                DeleteRight = "1"
                ModifyRight = "1"
            Case 14, 15
                ViewRight = "1"
                DeleteRight = "1"
                ModifyRight = "1"
                AddRight = "1"
            Case 16, 17
                ViewRight = "1"
                PrintRight = "1"
            Case 18, 19
                ViewRight = "1"
                PrintRight = "1"
                AddRight = "1"
            Case 20, 21
                ViewRight = "1"
                PrintRight = "1"
                ModifyRight = "1"
            Case 22, 23
                ViewRight = "1"
                PrintRight = "1"
                AddRight = "1"
                ModifyRight = "1"
            Case 24, 25
                ViewRight = "1"
                PrintRight = "1"
                DeleteRight = "1"
            Case 26, 27
                ViewRight = "1"
                PrintRight = "1"
                DeleteRight = "1"
                AddRight = "1"
            Case 28, 29
                ViewRight = "1"
                PrintRight = "1"
                DeleteRight = "1"
                ModifyRight = "1"
            Case 30, 31
                ViewRight = "1"
                PrintRight = "1"
                DeleteRight = "1"
                ModifyRight = "1"
                AddRight = "1"
        End Select
       
        If HttpContext.Current.Session("LoginSession").ToString().Split("|").GetValue(1).ToString() = "1" Then
            builSecurity.Append(1)
            builSecurity.Append(1)
            builSecurity.Append(1)
            builSecurity.Append(1)
            builSecurity.Append(1)
            Return builSecurity
        Else
            'Index 0 View
            builSecurity.Append(ViewRight)
            'Index 1 Add
            builSecurity.Append(AddRight)
            'Index 2 Modify
            builSecurity.Append(ModifyRight)
            'Index 3 Delete
            builSecurity.Append(DeleteRight)
            'Index 4 Print
            builSecurity.Append(PrintRight)
            Return builSecurity
        End If
    End Function
    Public Function ConvertTextDateBlank(ByVal dt As String) As String '''''''to convert dd/mm/yyyy to yyyymmdd
        Dim lstrarrDate As Array
        If dt.Trim = "" Then
            Return dt
        End If
        lstrarrDate = Split(dt, "/", -1, 1)
        Dim lstrdtyear As String
        Dim lstrdtmonth As String
        Dim lstrdtday As String
        Dim lstrDate As String

        lstrdtyear = lstrarrDate(2)
        lstrdtmonth = lstrarrDate(1)
        lstrdtday = lstrarrDate(0)
        If CType(lstrarrDate(1), Integer) < 2 Then
            lstrdtmonth = "0" + lstrarrDate(1)
        Else
            lstrdtmonth = lstrarrDate(1)
        End If
        If CType(lstrarrDate(0), Integer) < 2 Then
            lstrdtday = "0" + lstrarrDate(0)
        Else
            lstrdtday = lstrarrDate(0)
        End If

        lstrDate = lstrdtyear & lstrdtmonth & lstrdtday
        Return lstrDate
    End Function
    Public Function ConvertDateBlank(ByVal intDate As String) As String
        If intDate.Trim = "" Then
            Return intDate
        End If
        Try
            Dim dtDateFrom As New Date(Left(CType(intDate, String), 4), Mid(CType(intDate, String), 5, 2), Right(CType(intDate, String), 2))
            Return dtDateFrom.ToString("dd/MM/yyyy")
        Catch ex As Exception
            Return CDate("1/1/1900")
        End Try
    End Function
    'This function returns 1 or 0 according to permission of view 
    Public Function ReturnViewPermission(ByVal strInput As String, ByVal SecurityOptionSubName As String) As String
        Dim intStatus As Integer = 0
        Dim str As String = ""
        Dim objInXml As New XmlDocument
        objInXml.LoadXml(strInput)
        str = objInXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='" + SecurityOptionSubName + "']").Attributes("Value").Value
        Dim strBuilder As New StringBuilder
        If (objInXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            strBuilder = SecurityCheck(str)
            If strBuilder(0) <> "0" Then
                intStatus = 1
            End If
        Else
            intStatus = 1
        End If

        Return intStatus.ToString
    End Function

    'For Example GetDateFormat(txtdate.Text, "dd/MM/yyyy", "yyyyMMdd", "/")
    Public Function GetDateFormat(ByVal objDate As Object, ByVal dateInFormat As String, ByVal dateOutFormat As String, ByVal dateSepChar As String) As String
        Dim str As String = ""
        If objDate.Trim = "" Then
        Else
            Try
                If dateInFormat.Equals("yyyyMMdd") Then
                    str = DateTime.ParseExact(objDate, dateInFormat, System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat).ToString(dateOutFormat)
                Else
                    Dim ln As Integer = objDate.ToString().Length
                    If ln = 8 And dateInFormat.Equals("dd/MM/yyyy") Then
                        dateInFormat = "dd/MM/yy"
                    End If
                    Dim dt As New DateTime()
                    Dim dtfi As New DateTimeFormatInfo
                    dtfi.ShortDatePattern = dateInFormat
                    dtfi.DateSeparator = dateSepChar
                    dt = Convert.ToDateTime(objDate, dtfi)
                    str = dt.ToString(dateOutFormat)
                End If

            Catch ex As Exception
                str = "0"
            End Try
        End If
        Return str
    End Function

    Public Function EmployeeID(ByVal strInput As String) As String
        Dim str As String = ""
        Dim objInXml As New XmlDocument
        objInXml.LoadXml(strInput)
        str = objInXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText
        Return str
    End Function

    Public Function EmployeeName(ByVal strInput As String) As String
        Dim str As String = ""
        Dim objInXml As New XmlDocument
        objInXml.LoadXml(strInput)
        str = objInXml.DocumentElement.SelectSingleNode("Employee_Name").InnerText
        Return str
    End Function

    Public Function RestrictMultiUserByMultiIp() As String
        Dim strReturnExistedUser As String = ""
        Dim strIp As String
        strIp = HttpContext.Current.Request.Params("HTTP_X_FORWARDED_FOR")
        If strIp = "" Or strIp Is Nothing Then
            strIp = HttpContext.Current.Request.UserHostAddress
        End If
        Dim dtUserApplicationDetails As New DataTable
        dtUserApplicationDetails = CType(HttpContext.Current.Application("dtUserApplicationDetails"), DataTable)
        If dtUserApplicationDetails IsNot Nothing Then
            Dim dr As DataRow = dtUserApplicationDetails.Rows.Find(HttpContext.Current.Session("UserId"))
            If dr IsNot Nothing Then
                If dr("IPAddress") <> strIp Then
                    strReturnExistedUser = "False"
                Else
                    strReturnExistedUser = "True"
                End If

            Else
                strReturnExistedUser = "True"
            End If
        Else
            strReturnExistedUser = "True"
        End If
        Return strReturnExistedUser
    End Function
End Class

