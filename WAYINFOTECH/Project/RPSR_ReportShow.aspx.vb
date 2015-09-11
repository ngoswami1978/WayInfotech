Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Math
Partial Class RPSR_PTypeChallanShow
    Inherits System.Web.UI.Page
    Dim objRepDocument As New CrystalDecisions.CrystalReports.Engine.ReportDocument

    Protected Sub FrmReportShow_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles FrmReportShow.Init
        Dim objxmlout As New XmlDocument
        Dim eCashXSession As New XmlDocument
        Dim objds As New DataSet
        Dim xreader As XmlReader
        If Request.QueryString("Case") IsNot Nothing Then
            '/////////
        End If
    End Sub
    Function ReturnQuarter(ByVal intMonth As Integer, ByVal intYear As Integer) As String
        'Dim intMonth = Date.Today.Month()

        Dim strQuarter As String = ""
        If (intMonth >= 1 And intMonth <= 3) Then
            strQuarter = "1 "
        ElseIf (intMonth >= 4 And intMonth <= 6) Then
            strQuarter = "2 "
        ElseIf (intMonth >= 7 And intMonth <= 9) Then
            strQuarter = "3 "
        ElseIf (intMonth >= 10 And intMonth <= 12) Then
            strQuarter = "4 "
        End If
        Return strQuarter
    End Function

#Region "ConvertNumberToWord"
    Shared Function CurrencyToWord(ByVal MyNumber)
        Dim Amt As Double = CDbl(Val(MyNumber))
        Dim Temp
        Dim Rupees, Paisa As String
        Dim DecimalPlace, iCount
        Dim Hundred, Words As String

        Dim ch As String
        Dim i As Integer
        Dim count As Integer
        Dim Alldigits As Boolean

        Dim Place(9) As String
        Place(0) = " Thousand "
        Place(2) = " Lakh "
        Place(4) = " Crore "
        Place(6) = " Arab "
        Place(8) = " Kharab "
        On Error Resume Next
        ' Convert MyNumber to a string, trimming extra spaces.

        MyNumber = Trim(Microsoft.VisualBasic.Str(MyNumber))
        '==========================Modified================================
        ' count "." if it is more than 1
        For i = 1 To Len(MyNumber)
            If Mid$(MyNumber, i, 1) = "." Then
                count = count + 1
                If count > 1 Then
                    CurrencyToWord = "Invalid Currency....!"
                    Exit Function
                End If
            End If
        Next i
        ' check all the digits are numbers
        Alldigits = True
        For i = 1 To Len(MyNumber)
            ' See if the next character is a non-digit.
            ch = Mid$(MyNumber, i, 1)
            If ch < "0" Or ch > "9" Or ch = "." Then
                If ch <> "." Then
                    ' This is not a digit.
                    Alldigits = False
                    Exit For
                End If
            End If
        Next i

        If Alldigits = False Then
            CurrencyToWord = "Invalid Currency....!"
            Exit Function
        End If
        '====================================================================

        ' Find decimal place.
        DecimalPlace = InStr(MyNumber, ".")
        '==========================Modified==================================
        If DecimalPlace = 0 Then
            If Len(MyNumber) > 13 Then
                CurrencyToWord = "Value is too large, Function accepts 13 digits before decimal point"
                Exit Function
            End If
        End If
        '====================================================================
        ' If we find decimal place...
        If DecimalPlace > 0 Then
            '==========================Modified==================================
            If DecimalPlace > 14 Then
                CurrencyToWord = "Value is too large, Function accepts 13 digits before decimal point"
                Exit Function
            End If
            '====================================================================
            ' Convert Paisa
            Temp = Left(Mid(MyNumber, DecimalPlace + 1) & "00", 2)


            ' ModiFied By Neeraj
            '   Paisa = " and " & ConvertTens(Temp) & " Paisa Only"
            If Val(DecimalPlace) > 0 Then
                Paisa = " and " & ConvertTens(Temp) & " Paisa Only"
            End If


            ' Strip off paisa from remainder to convert.
            MyNumber = Trim(Left(MyNumber, DecimalPlace - 1))
        End If

        ' Convert last 3 digits of MyNumber to Rupees in word.
        Hundred = ConvertHundred(Right(MyNumber, 3))
        '==========================Modified============
        If Len(MyNumber) <= 2 Then
            ' Append leading zeros to number.
            MyNumber = Right("000" & MyNumber, 3)
            MyNumber = Left(MyNumber, Len(MyNumber) - 3)
        Else
            ' Strip off last three digits
            MyNumber = Left(MyNumber, Len(MyNumber) - 3)
        End If
        '====================================================================
        iCount = 0
        Do While MyNumber <> ""
            'Strip last two digits
            Temp = Right(MyNumber, 2)
            If Len(MyNumber) = 1 Then
                Words = ConvertDigit(Temp) & Place(iCount) & Words
                MyNumber = Left(MyNumber, Len(MyNumber) - 1)

            Else
                '==========================Modified==================================
                If Temp <> "00" Then
                    Words = ConvertTens(Temp) & Place(iCount) & Words
                    MyNumber = Left(MyNumber, Len(MyNumber) - 2)
                Else
                    MyNumber = Left(MyNumber, Len(MyNumber) - 2)
                End If
            End If
            iCount = iCount + 2
        Loop

        '@ ModiFied By Neeraj
        'If Paisa = "" Then
        '    CurrencyToWord = "Rupees " & Words & Hundred & " Only."
        'Else
        '    CurrencyToWord = "Rupees " & Words & Hundred & Paisa
        'End If

        If Amt >= 1 Then
            If Paisa = "" Or Paisa Is Nothing Then
                'CurrencyToWord = "Rupees " & Words & Hundred & " Only."
                CurrencyToWord = Words & Hundred & " Only."
            Else
                ' CurrencyToWord = "Rupees " & Words & Hundred & Paisa
                CurrencyToWord = Words & Hundred & Paisa
            End If
        Else
            CurrencyToWord = "" & Words & Hundred & Paisa
            CurrencyToWord = CurrencyToWord.ToString.Replace("and", "")
        End If
        '=====================================================================
    End Function

    Private Shared Function ConvertHundred(ByVal MyNumber)
        Dim Result As String

        ' Exit if there is nothing to convert.
        If Val(MyNumber) = 0 Then Exit Function

        ' Append leading zeros to number.
        MyNumber = Right("000" & MyNumber, 3)

        ' Do we have a Hundred place digit to convert?
        If Left(MyNumber, 1) <> "0" Then
            Result = ConvertDigit(Left(MyNumber, 1)) & " Hundred "
        End If

        ' Do we have a tens place digit to convert?
        If Mid(MyNumber, 2, 1) <> "0" Then
            Result = Result & ConvertTens(Mid(MyNumber, 2))
        Else
            ' If not, then convert the ones place digit.
            Result = Result & ConvertDigit(Mid(MyNumber, 3))
        End If

        ConvertHundred = Trim(Result)
    End Function

    Private Shared Function ConvertTens(ByVal MyTens)
        Dim Result As String

        ' Is value between 10 and 19?
        If Val(Left(MyTens, 1)) = 1 Then
            Select Case Val(MyTens)
                Case 10 : Result = "Ten"
                Case 11 : Result = "Eleven"
                Case 12 : Result = "Twelve"
                Case 13 : Result = "Thirteen"
                Case 14 : Result = "Fourteen"
                Case 15 : Result = "Fifteen"
                Case 16 : Result = "Sixteen"
                Case 17 : Result = "Seventeen"
                Case 18 : Result = "Eighteen"
                Case 19 : Result = "Nineteen"
                Case Else
            End Select
        Else
            ' .. otherwise it's between 20 and 99.
            Select Case Val(Left(MyTens, 1))
                Case 2 : Result = "Twenty "
                Case 3 : Result = "Thirty "
                Case 4 : Result = "Forty "
                Case 5 : Result = "Fifty "
                Case 6 : Result = "Sixty "
                Case 7 : Result = "Seventy "
                Case 8 : Result = "Eighty "
                Case 9 : Result = "Ninety "
                Case Else
            End Select

            ' Convert ones place digit.
            Result = Result & ConvertDigit(Right(MyTens, 1))
        End If

        ConvertTens = Result
    End Function

    Private Shared Function ConvertDigit(ByVal MyDigit)
        Select Case Val(MyDigit)
            Case 1 : ConvertDigit = "One"
            Case 2 : ConvertDigit = "Two"
            Case 3 : ConvertDigit = "Three"
            Case 4 : ConvertDigit = "Four"
            Case 5 : ConvertDigit = "Five"
            Case 6 : ConvertDigit = "Six"
            Case 7 : ConvertDigit = "Seven"
            Case 8 : ConvertDigit = "Eight"
            Case 9 : ConvertDigit = "Nine"
            Case Else : ConvertDigit = ""
        End Select
    End Function
#End Region

    Private Sub SetParameters()
        Dim InputXML As New XmlDocument, OutputXML As New XmlDocument
        InputXML.LoadXml(Session("CityWiseBudget"))

        Dim paramfields As New ParameterFields
        Dim Pra_Month As New ParameterField()
        Dim Pra_Year As New ParameterField()
        Dim discreteValFrom As New ParameterDiscreteValue()
        Dim discreteValTo As New ParameterDiscreteValue()

        Pra_Month = New ParameterField()
        discreteValFrom = New ParameterDiscreteValue()
        Pra_Month.ParameterFieldName = "PMonth"
        discreteValFrom.Value = InputXML.DocumentElement.SelectSingleNode("Month").InnerText.Trim()
        Pra_Month.CurrentValues.Add(discreteValFrom)



        Pra_Year = New ParameterField()
        discreteValTo = New ParameterDiscreteValue()
        Pra_Year.ParameterFieldName = "PYear"
        discreteValTo.Value = InputXML.DocumentElement.SelectSingleNode("Year").InnerText.Trim()
        Pra_Year.CurrentValues.Add(discreteValTo)
        paramfields.Add(Pra_Month)
        paramfields.Add(Pra_Year)
        CRViewer1.ParameterFieldInfo = paramfields
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        hdStatus.Value = "0"
        If hdData.Value <> "" Then
            Dim objxmlout As New XmlDocument
            Dim eCashXSession As New XmlDocument
            Dim objds As New DataSet
            Dim xreader As XmlReader
            objxmlout.LoadXml("<TR_VIEWLETTERS_OUTPUT><DETAILS TR_CLETTER_ID='' TR_CLETTER='' /></TR_VIEWLETTERS_OUTPUT>")
            'Dim dt As New DataTable
            'dt.Columns.Add(New DataColumn("TR_CLETTER_ID"))
            'dt.Columns.Add(New DataColumn("TR_CLETTER"))
            'Dim dr As DataRow
            Dim arstrData() As String = hdData.Value.Split("|")
            Dim objNode, objCloneNode As XmlNode
            objNode = objxmlout.DocumentElement.SelectSingleNode("DETAILS")
            objCloneNode = objNode.CloneNode(True)
            objxmlout.DocumentElement.RemoveChild(objNode)
            For i As Integer = 0 To arstrData.Length - 1
                'dr = dt.NewRow
                'dr("TR_CLETTER_ID") = ""
                'dr("TR_CLETTER") = arstrData(i)
                'dt.Rows.Add(dr)
                objCloneNode.Attributes("TR_CLETTER_ID").Value = ""
                objCloneNode.Attributes("TR_CLETTER").Value = arstrData(i)
                objxmlout.DocumentElement.AppendChild(objCloneNode)
                objCloneNode = objNode.CloneNode(True)
            Next
            xreader = New XmlNodeReader(objxmlout)
            objds.ReadXml(xreader)
            'objds.Tables.Add(dt)
            Dim objRepDocument As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            objRepDocument.Load(Server.MapPath("Training/Reports/rptLetterAll.rpt"))
            objRepDocument.SetDataSource(objds)
            objRepDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, False, "")
        End If
    End Sub

    Private Sub BindReport(ByVal objRepDocument As ReportDocument)
        If objRepDocument IsNot Nothing Then
            CRViewer1.HasPageNavigationButtons = True
            CRViewer1.EnableViewState = True
            CRViewer1.ReportSource = objRepDocument
            CRViewer1.DataBind()
        Else
            CRViewer1.ReportSource = Nothing
            CRViewer1.DataBind()
        End If
    End Sub

    Protected Sub FrmReportShow_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles FrmReportShow.Unload
        Try
            objRepDocument.Close()
        Catch ex As Exception

        End Try
    End Sub
End Class
