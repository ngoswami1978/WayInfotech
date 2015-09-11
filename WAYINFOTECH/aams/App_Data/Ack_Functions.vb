Imports Microsoft.VisualBasic
Imports System.Xml


Public Class Ack_Functions

    Public gintManageAdminUser = 1
    Public gintManageOffice = 2
    Public gintManageBasicPoints = 3
    Public gintManageRestrictedOffice = 4
    Public gintRedemptionOptions = 5
    Public gintRedemptionRequests = 6
    Public gintEarnedPointsReport = 7
    Public gintUserSummaryReport = 8
    Public gintActivatedUserReport = 9
    Public gintUploadSalesData = 10
    Public gintManageTravelAgentUser = 11

    Public Function GetSecurityId(ByVal strXmlInput As String, ByVal strElement As String) As Integer
        Try
            Dim objNode As XmlNode
            Dim objNodeList As XmlNodeList
            Dim objxmlUserInfo As New XmlDocument
            objxmlUserInfo.LoadXml(strXmlInput)
            If IsNumeric(strElement) Then
                'Return objxmlUserInfo.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[normalize-space(@SECURITY_OPTION_ID)=" + strElement.ToString() + "]").Attributes("SEC_VALUE").InnerText
                If Not objxmlUserInfo.DocumentElement.SelectSingleNode("SECURITY_OPTION[normalize-space(@SECURITY_OPTION_ID)=" + strElement.ToString() + "]") Is Nothing Then
                    Return objxmlUserInfo.DocumentElement.SelectSingleNode("SECURITY_OPTION[normalize-space(@SECURITY_OPTION_ID)=" + strElement.ToString() + "]").Attributes("SEC_VALUE").InnerText
                End If
            Else
                objNodeList = objxmlUserInfo.GetElementsByTagName("PAX_TYPE_CODE")
                For Each objNode In objNodeList
                    If (objNode.InnerText = Trim(strElement)) Then
                        Return 1
                    End If
                Next
            End If
            Return 0
        Catch ex As Exception
        End Try
    End Function


    Public Function CheckSecurity(ByVal SecOp As Integer, ByVal SecValue As Integer, ByVal objXmlSecurityInput As XmlDocument) As Boolean
        Return True
        Dim IntSecurity_option As Int16
        Dim objNode As XmlNode = objXmlSecurityInput.DocumentElement.SelectSingleNode("//SECURITY_OPTIONS/SECURITY_OPTION[@SECURITY_OPTION_ID='" + SecOp.ToString + "']")
        Dim IntSecurity_value As Int16

        If objNode Is Nothing Then
            Return False
        Else
            IntSecurity_option = CType(objNode.Attributes("SECURITY_OPTION_ID").InnerText, Integer)

            IntSecurity_value = CType(objNode.Attributes("SEC_VALUE").InnerText, Integer)

            If IntSecurity_option = SecOp And IntSecurity_value >= SecValue Then
                Return True
            Else
                Return False
            End If
        End If

    End Function

    Public Function FillComboBox(ByRef p_objcboName As System.Web.UI.WebControls.DropDownList, ByVal p_objxdoc As XmlDocument, ByVal p_strXmlTag As String, ByVal p_strDataTextField As String, ByVal p_strDataValueField As String, Optional ByVal p_boolInsertSelectOne As Boolean = False) As Boolean
        Dim boolValuetoReturn As Boolean = False
        Dim p_item As New ListItem
        Try
            If Not p_objxdoc Is Nothing Then
                If p_objxdoc.OuterXml = String.Empty Then
                    boolValuetoReturn = False
                Else
                    Dim objxmldoc As New XmlDocument
                    Dim objXmlNodeList As XmlNodeList
                    Dim objXmlNode As XmlNode
                    p_objcboName.Items.Clear()
                    If p_boolInsertSelectOne = True Then
                        p_item.Value = String.Empty
                        p_item.Text = "--Select One--"
                        p_objcboName.Items.Add(p_item)
                    End If
                    objxmldoc.LoadXml(p_objxdoc.OuterXml)
                    objXmlNodeList = objxmldoc.DocumentElement.SelectNodes(p_strXmlTag)
                    If objXmlNodeList.Count > 0 Then
                        For Each objXmlNode In objXmlNodeList
                            p_objcboName.Items.Add(New ListItem(objXmlNode.Attributes(p_strDataValueField).Value, objXmlNode.Attributes(p_strDataTextField).Value))
                        Next
                    Else
                        p_item.Value = String.Empty
                        p_item.Text = "--Select One--"
                        p_objcboName.Items.Add(p_item)
                    End If
                    boolValuetoReturn = True
                End If
            Else
                boolValuetoReturn = False
            End If
        Catch ex As Exception
            boolValuetoReturn = False
        Finally
            FillComboBox = boolValuetoReturn
        End Try
    End Function
    '<Errors Status=''><Error Code='' Description='' /></Errors>

    Public Function CheckError(ByVal objXmlDocument As XmlDocument) As String
        Dim strError As String
        strError = String.Empty
        If objXmlDocument.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "TRUE" Then
            strError = objXmlDocument.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

        End If
        CheckError = strError
    End Function

    Public Function GetDateInInteger(ByVal strdate As String) As String
        Dim intRetValue As String
        intRetValue = CInt((Convert.ToDateTime(strdate)).ToString("yyyyMMdd"))
        Return (intRetValue)
    End Function


    Public Sub Fill_Combo_Dates(ByRef p_objcboName As System.Web.UI.WebControls.DropDownList, ByVal intValue As Integer, Optional ByVal blnselected As Boolean = False)
        Dim intDay, intmonth, intyear, intctr As Integer
        Dim intselectedval As Integer
        Dim strctr As String
        Dim p_item As New ListItem
        Try


            intDay = Now.Day()
            intmonth = Now.Month()
            intyear = Now.Year
            Select Case intValue
                Case 1
                    For intctr = 1 To 31
                        If intctr.ToString.Length < 2 Then
                            strctr = "0" & intctr
                        Else
                            strctr = intctr
                        End If
                        p_objcboName.Items.Add(strctr)
                        If intctr = intDay Then
                            intselectedval = intctr - 1
                        End If
                    Next
                    If blnselected = True Then
                        p_objcboName.SelectedIndex = intselectedval
                    End If
                Case 2
                    For intctr = 1 To 12

                        p_objcboName.Items.Add(GetMonthName(intctr))
                        If intctr = intmonth Then
                            intselectedval = intctr - 1
                        End If
                    Next
                    If blnselected = True Then
                        p_objcboName.SelectedIndex = intselectedval
                    End If
                    'p_objcboName.Items.FindByValue(GetMonthName(intmonth)).Selected = True

                Case 3
                    Dim intcnt As Integer
                    For intctr = 1900 To Now.Year
                        intcnt = intcnt + 1
                        p_objcboName.Items.Add(intctr)
                        If intctr = intyear Then
                            intselectedval = intcnt - 1
                        End If
                    Next
                    If blnselected = True Then
                        p_objcboName.SelectedIndex = intselectedval
                    End If
                    'p_objcboName.Items.FindByText(intyear).Selected = True
            End Select


        Catch ex As Exception

        End Try
    End Sub
    Public Function GetMonthName(ByVal monthId As Int16) As String
        Dim strRetValue As String
        Select Case monthId
            Case "1"
                strRetValue = "Jan"
            Case "2"
                strRetValue = "Feb"
            Case "3"
                strRetValue = "Mar"
            Case "4"
                strRetValue = "Apr"
            Case "5"
                strRetValue = "May"
            Case "6"
                strRetValue = "June"
            Case "7"
                strRetValue = "July"
            Case "8"
                strRetValue = "Aug"
            Case "9"
                strRetValue = "Sep"
            Case "10"
                strRetValue = "Oct"
            Case "11"
                strRetValue = "Nov"
            Case "12"
                strRetValue = "Dec"
        End Select
        Return strRetValue
    End Function

    Public Function BindGrid(ByRef p_objGrdName As System.Web.UI.WebControls.GridView, ByVal objXmlGrid As XmlDocument, ByVal strNode As String) As Boolean
        Try
            Dim dsGrid As New Data.DataSet
            Dim reader As XmlNodeReader = New XmlNodeReader(objXmlGrid)
            dsGrid.ReadXml(reader)
            p_objGrdName.DataSource = dsGrid.Tables(strNode)
            p_objGrdName.DataBind()
        Catch ex As Exception

        End Try
    End Function


    Public Function ConvertDateFormat(ByVal strDate As String) As String
        Try
            strDate = strDate.Substring(4, 2) & "/" & strDate.Substring(6, 2) & "/" & strDate.Substring(0, 4)
            strDate = (Convert.ToDateTime(strDate)).ToString("ddMMMyyyy")
            Return strDate
        Catch ex As Exception

        End Try
    End Function
    'Public Sub SetCurrentValuesForParameterField(ByRef myReport As XmlDataDocument, ByVal strParameterName As String, ByVal strParameterValue As String)
    '    Try
    '        Dim currentParameterValues As ParameterValues = New ParameterValues()
    '        Dim myParameterDiscreteValue As ParameterDiscreteValue = New ParameterDiscreteValue()
    '        myParameterDiscreteValue.Value = strParameterValue
    '        currentParameterValues.Add(myParameterDiscreteValue)
    '        Dim myParameterFieldDefinitions As ParameterCollection = myReport.DataDefinition.ParameterFields
    '        Dim myParameterFieldDefinition As ParameterFieldDefinition = myParameterFieldDefinitions(strParameterName)
    '        myParameterFieldDefinition.ApplyCurrentValues(currentParameterValues)
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Public Sub ExpireCache()

        HttpContext.Current.Response.Buffer = True
        HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1D)
        HttpContext.Current.Response.Expires = -1500
        HttpContext.Current.Response.CacheControl = "no-cache"
    End Sub

    Public Sub ExpirePageCache()
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        HttpContext.Current.Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        HttpContext.Current.Response.Cache.SetLastModified(DateTime.Now)
        HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(False)
    End Sub
End Class
