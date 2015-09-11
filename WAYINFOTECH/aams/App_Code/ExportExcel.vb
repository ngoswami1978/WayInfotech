Imports Microsoft.VisualBasic
Imports System.io
Imports System.Xml
Imports System.Xml.Xsl
Imports System.Xml.XPath


Public Class ExportExcel


    ' Methods
    Public Sub New()
        Me.appType = "Web"
        Me.response = HttpContext.Current.Response
    End Sub



    Public Sub New(ByVal ApplicationType As String)
        Me.appType = ApplicationType
        If ((Me.appType <> "Web") AndAlso (Me.appType <> "Win")) Then
            Throw New Exception("Provide valid application format (Web/Win)")
        End If
        If (Me.appType = "Web") Then
            Me.response = HttpContext.Current.Response
        End If
    End Sub



    Private Function ConvertXmlToDataTable(ByVal xmlDoc As XmlDocument, ByVal strTableName As String) As DataTable
        ' xdoc.LoadXml(xmlDoc)
        ' Dim xdoc As New XmlDocument
        Dim dt As New Data.DataTable
        Dim objXmlAttrColl As XmlAttributeCollection
        Dim objXmlTempAttr As XmlAttribute
        Dim xNodeList As XmlNodeList
        Dim xNode As XmlNode
        Dim intCount As Integer = 0
        Dim intCountRow As Integer = 0
        Dim dtR As Data.DataRow
        xNodeList = xmlDoc.DocumentElement.SelectNodes(strTableName)
        If xNodeList.Count > 0 Then
            xNode = xNodeList(0)
            For Each objXmlTempAttr In xNode.Attributes
                dt.Columns.Add(objXmlTempAttr.Name.ToString())
            Next
        End If


        For Each xNode In xNodeList
            objXmlAttrColl = xNode.Attributes
            intCountRow = 0
            dtR = Nothing
            '  If intCount > 0 Then
            dtR = dt.NewRow
            ' End If
            For Each objXmlTempAttr In objXmlAttrColl
                ' If intCount = 0 Then
                'dt.Columns.Add(objXmlTempAttr.Name.ToString())
                ' Else
                '  dtR = dt.NewRow
                dtR(intCountRow) = objXmlTempAttr.Value.Replace(vbCrLf, "")
                intCountRow = intCountRow + 1
                ' End If
            Next
            If Not dtR Is Nothing Then dt.Rows.Add(dtR)
            intCount = intCount + 1
        Next
        Return dt
    End Function

    Private Sub CreateStylesheet(ByVal writer As XmlTextWriter, ByVal sHeaders As String(), ByVal sFileds As String(), ByVal FormatType As ExportFormat)
        Try
            Dim ns As String = "http://www.w3.org/1999/XSL/Transform"
            writer.Formatting = Formatting.Indented
            writer.WriteStartDocument()
            writer.WriteStartElement("xsl", "stylesheet", ns)
            writer.WriteAttributeString("version", "1.0")
            writer.WriteStartElement("xsl:output")
            writer.WriteAttributeString("method", "text")
            writer.WriteAttributeString("version", "8.0")
            writer.WriteEndElement()
            writer.WriteStartElement("xsl:template")
            writer.WriteAttributeString("match", "/")
            Dim i As Integer
            For i = 0 To sHeaders.Length - 1
                writer.WriteString("""")
                writer.WriteStartElement("xsl:value-of")
                writer.WriteAttributeString("select", ("'" & sHeaders(i) & "'"))
                writer.WriteEndElement()
                writer.WriteString("""")
                If (i <> (sFileds.Length - 1)) Then
                    writer.WriteString(IIf((FormatType = ExportFormat.CSV), ",", ChrW(9)))
                End If
            Next i
            writer.WriteStartElement("xsl:for-each")
            writer.WriteAttributeString("select", "Export/Values")
            writer.WriteString(ChrW(13) & ChrW(10))
            Dim j As Integer
            For j = 0 To sFileds.Length - 1
                writer.WriteString("""")
                writer.WriteStartElement("xsl:value-of")
                writer.WriteAttributeString("select", sFileds(j))
                writer.WriteEndElement()
                writer.WriteString("""")
                If (j <> (sFileds.Length - 1)) Then
                    writer.WriteString(IIf((FormatType = ExportFormat.CSV), ",", ChrW(9)))
                End If
            Next j
            writer.WriteEndElement()
            writer.WriteEndElement()
            writer.WriteEndElement()
            writer.WriteEndDocument()
        Catch exception As Exception
            Throw exception
        End Try
    End Sub

    Private Sub CreateStylesheet(ByVal writer As XmlTextWriter, ByVal sHeaders As ArrayList, ByVal sFileds As String(), ByVal FormatType As ExportFormat)
        Try
            Dim ns As String = "http://www.w3.org/1999/XSL/Transform"
            writer.Formatting = Formatting.Indented
            writer.WriteStartDocument()
            writer.WriteStartElement("xsl", "stylesheet", ns)
            writer.WriteAttributeString("version", "1.0")
            writer.WriteStartElement("xsl:output")
            writer.WriteAttributeString("method", "text")
            writer.WriteAttributeString("version", "8.0")
            writer.WriteEndElement()
            writer.WriteStartElement("xsl:template")
            writer.WriteAttributeString("match", "/")
            Dim i As Integer
            For i = 0 To sHeaders.Count - 1
                writer.WriteString("""")
                writer.WriteStartElement("xsl:value-of")
                writer.WriteAttributeString("select", ("'" & sHeaders(i) & "'"))
                writer.WriteEndElement()
                writer.WriteString("""")
                If (i <> (sFileds.Length - 1)) Then
                    writer.WriteString(IIf((FormatType = ExportFormat.CSV), ",", ChrW(9)))
                End If
            Next i
            writer.WriteStartElement("xsl:for-each")
            writer.WriteAttributeString("select", "Export/Values")
            writer.WriteString(ChrW(13) & ChrW(10))
            Dim j As Integer
            For j = 0 To sFileds.Length - 1
                writer.WriteString("""")
                writer.WriteStartElement("xsl:value-of")
                writer.WriteAttributeString("select", sFileds(j))
                writer.WriteEndElement()
                writer.WriteString("""")
                If (j <> (sFileds.Length - 1)) Then
                    writer.WriteString(IIf((FormatType = ExportFormat.CSV), ",", ChrW(9)))
                End If
            Next j
            writer.WriteEndElement()
            writer.WriteEndElement()
            writer.WriteEndElement()
            writer.WriteEndDocument()
        Catch exception As Exception
            Throw exception
        End Try
    End Sub

    Private Sub Export_with_XSLT_Web(ByVal dsExport As DataSet, ByVal sHeaders As String(), ByVal sFileds As String(), ByVal FormatType As ExportFormat, ByVal FileName As String)
        Try
            Me.response.Clear()
            Me.response.Buffer = True
            If (FormatType = ExportFormat.CSV) Then
                Me.response.ContentType = "text/csv"
                Me.response.AppendHeader("content-disposition", ("attachment; filename=" & FileName))
            Else
                Me.response.ContentType = "application/vnd.ms-excel"
                Me.response.AppendHeader("content-disposition", ("attachment; filename=" & FileName))
            End If
            Dim w As New MemoryStream
            Dim writer As New XmlTextWriter(w, Encoding.UTF8)
            Me.CreateStylesheet(writer, sHeaders, sFileds, FormatType)
            writer.Flush()
            w.Seek(0, SeekOrigin.Begin)
            Dim document As New XmlDataDocument(dsExport)


            'New code for faster execution  23feb09

            Dim objXslTrainsform As New XslCompiledTransform
            objXslTrainsform.Load(New XmlTextReader(w))
            Dim writer2 As New StringWriter
            objXslTrainsform.Transform(document, Nothing, XmlWriter.Create(writer2, objXslTrainsform.OutputSettings))
            ''End code  23feb09



            ''Old code updated on 23feb09
            ' Dim transform As New XslTransform
            'transform.Load(New XmlTextReader(w), Nothing, Nothing)
            ' Dim writer2 As New StringWriter
            ' transform.Transform(DirectCast(document, IXPathNavigable), Nothing, DirectCast(writer2, TextWriter), Nothing)
            ''End code updated on 23feb09

            Me.response.Write(writer2.ToString)
            response.Flush()
            writer2.Close()
            writer.Close()
            w.Close()
            Me.response.End()
            '   Catch exception As ThreadAbortException
            ' Dim message As String = Exception.Message
        Catch exception2 As Exception
            Throw exception2
        End Try
    End Sub

   




    Private Sub Export_with_XSLT_Web(ByVal dsExport As DataSet, ByVal sHeaders As ArrayList, ByVal sFileds As String(), ByVal FormatType As ExportFormat, ByVal FileName As String)
        Try
            Me.response.Clear()
            Me.response.Buffer = True
            If (FormatType = ExportFormat.CSV) Then
                Me.response.ContentType = "text/csv"
                Me.response.AppendHeader("content-disposition", ("attachment; filename=" & FileName))
            Else
                Me.response.ContentType = "application/vnd.ms-excel"
                Me.response.AppendHeader("content-disposition", ("attachment; filename=" & FileName))
            End If
            Dim w As New MemoryStream
            Dim writer As New XmlTextWriter(w, Encoding.UTF8)
            Me.CreateStylesheet(writer, sHeaders, sFileds, FormatType)
            writer.Flush()
            w.Seek(0, SeekOrigin.Begin)
            Dim document As New XmlDataDocument(dsExport)

            'New code for faster execution  23feb09

            Dim objXslTrainsform As New XslCompiledTransform
            objXslTrainsform.Load(New XmlTextReader(w))
            Dim writer2 As New StringWriter
            objXslTrainsform.Transform(document, Nothing, XmlWriter.Create(writer2, objXslTrainsform.OutputSettings))
            ''End code  23feb09

            ''Old code updated on 23feb09
            '  Dim transform As New XslTransform
            '  transform.Load(New XmlTextReader(w), Nothing, Nothing)
            '  Dim writer2 As New StringWriter
            '  transform.Transform(DirectCast(document, IXPathNavigable), Nothing, DirectCast(writer2, TextWriter), Nothing)
            ''End code updated on 23feb09

            Me.response.Write(writer2.ToString)
            response.Flush()
            writer2.Close()
            writer.Close()
            w.Close()
            Me.response.End()
            '   Catch exception As ThreadAbortException
            ' Dim message As String = Exception.Message
        Catch exception2 As Exception
            Throw exception2
        End Try
    End Sub

    Private Sub Export_with_XSLT_Windows(ByVal dsExport As DataSet, ByVal sHeaders As String(), ByVal sFileds As String(), ByVal FormatType As ExportFormat, ByVal FileName As String)
        Try
            Dim w As New MemoryStream
            Dim writer As New XmlTextWriter(w, Encoding.UTF8)
            Me.CreateStylesheet(writer, sHeaders, sFileds, FormatType)
            writer.Flush()
            w.Seek(0, SeekOrigin.Begin)
            Dim document As New XmlDataDocument(dsExport)
            Dim transform As New XslTransform
            transform.Load(New XmlTextReader(w), Nothing, Nothing)
            Dim writer2 As New StringWriter
            transform.Transform(DirectCast(document, IXPathNavigable), Nothing, DirectCast(writer2, TextWriter), Nothing)
            Dim writer3 As New StreamWriter(FileName)
            writer3.WriteLine(writer2.ToString)
            writer3.Close()
            writer2.Close()
            writer.Close()
            w.Close()
        Catch exception As Exception
            Throw exception
        End Try
    End Sub

    Public Sub ExportDetails(ByVal objXml As XmlDocument, ByVal strTableName As String, ByVal FormatType As ExportFormat, ByVal FileName As String)
        Try
            Dim DetailsTable As New DataTable
            DetailsTable = ConvertXmlToDataTable(objXml, strTableName)
            If (DetailsTable.Rows.Count = 0) Then
                Throw New Exception("There are no details to export.")
            End If
            Dim dsExport As New DataSet("Export")
            Dim table As DataTable = DetailsTable.Copy
            table.TableName = "Values"
            dsExport.Tables.Add(table)
            Dim sHeaders As String() = New String(table.Columns.Count - 1) {}
            Dim sFileds As String() = New String(table.Columns.Count - 1) {}
            Dim i As Integer
            For i = 0 To table.Columns.Count - 1
                sHeaders(i) = table.Columns.Item(i).ColumnName
                sFileds(i) = table.Columns.Item(i).ColumnName
            Next i
            If (Me.appType = "Web") Then
                Me.Export_with_XSLT_Web(dsExport, sHeaders, sFileds, FormatType, FileName)
            ElseIf (Me.appType = "Win") Then
                Me.Export_with_XSLT_Windows(dsExport, sHeaders, sFileds, FormatType, FileName)
            End If
        Catch exception As Exception
            Throw exception
        End Try
    End Sub


    Public Sub ExportDetails(ByVal DetailsTable As DataTable, ByVal FormatType As ExportFormat, ByVal FileName As String)
        Try
            If (DetailsTable.Rows.Count = 0) Then
                Throw New Exception("There are no details to export.")
            End If
            Dim dsExport As New DataSet("Export")
            Dim table As DataTable = DetailsTable.Copy
            table.TableName = "Values"
            dsExport.Tables.Add(table)
            Dim sHeaders As String() = New String(table.Columns.Count - 1) {}
            Dim sFileds As String() = New String(table.Columns.Count - 1) {}
            Dim i As Integer
            For i = 0 To table.Columns.Count - 1
                sHeaders(i) = table.Columns.Item(i).ColumnName
                sFileds(i) = table.Columns.Item(i).ColumnName
            Next i
            If (Me.appType = "Web") Then
                Me.Export_with_XSLT_Web(dsExport, sHeaders, sFileds, FormatType, FileName)
            ElseIf (Me.appType = "Win") Then
                Me.Export_with_XSLT_Windows(dsExport, sHeaders, sFileds, FormatType, FileName)
            End If
        Catch exception As Exception
            Throw exception
        End Try
    End Sub

    Public Sub ExportDetails(ByVal DetailsTable As DataTable, ByVal ColumnList As Integer(), ByVal FormatType As ExportFormat, ByVal FileName As String)
        Try
            If (DetailsTable.Rows.Count = 0) Then
                Throw New Exception("There are no details to export")
            End If
            Dim dsExport As New DataSet("Export")
            Dim table As DataTable = DetailsTable.Copy
            table.TableName = "Values"
            dsExport.Tables.Add(table)
            If (ColumnList.Length > table.Columns.Count) Then
                Throw New Exception("ExportColumn List should not exceed Total Columns")
            End If
            Dim sHeaders As String() = New String(ColumnList.Length - 1) {}
            Dim sFileds As String() = New String(ColumnList.Length - 1) {}
            Dim i As Integer
            For i = 0 To ColumnList.Length - 1
                If ((ColumnList(i) < 0) OrElse (ColumnList(i) >= table.Columns.Count)) Then
                    Throw New Exception("ExportColumn Number should not exceed Total Columns Range")
                End If
                sHeaders(i) = table.Columns.Item(ColumnList(i)).ColumnName
                sFileds(i) = table.Columns.Item(ColumnList(i)).ColumnName
            Next i
            If (Me.appType = "Web") Then
                Me.Export_with_XSLT_Web(dsExport, sHeaders, sFileds, FormatType, FileName)
            ElseIf (Me.appType = "Win") Then
                Me.Export_with_XSLT_Windows(dsExport, sHeaders, sFileds, FormatType, FileName)
            End If
        Catch exception As Exception
            Throw exception
        End Try
    End Sub

    Public Sub ExportDetails(ByVal objXml As XmlDocument, ByVal strTableName As String, ByVal ColumnList As Integer(), ByVal Headers As String(), ByVal FormatType As ExportFormat, ByVal FileName As String)
        Try
            Dim DetailsTable As New DataTable
            DetailsTable = ConvertXmlToDataTable(objXml, strTableName)
            If (DetailsTable.Rows.Count = 0) Then
                Throw New Exception("There are no details to export")
            End If
            Dim dsExport As New DataSet("Export")
            Dim table As DataTable = DetailsTable.Copy
            table.TableName = "Values"
            dsExport.Tables.Add(table)
            If (ColumnList.Length <> Headers.Length) Then
                Throw New Exception("ExportColumn List and Headers List should be of same length")
            End If
            If ((ColumnList.Length > table.Columns.Count) OrElse (Headers.Length > table.Columns.Count)) Then
                Throw New Exception("ExportColumn List should not exceed Total Columns")
            End If
            Dim sFileds As String() = New String(ColumnList.Length - 1) {}
            Dim i As Integer
            For i = 0 To ColumnList.Length - 1
                If ((ColumnList(i) < 0) OrElse (ColumnList(i) >= table.Columns.Count)) Then
                    Throw New Exception("ExportColumn Number should not exceed Total Columns Range")
                End If
                sFileds(i) = table.Columns.Item(ColumnList(i)).ColumnName
            Next i
            If (Me.appType = "Web") Then
                Me.Export_with_XSLT_Web(dsExport, Headers, sFileds, FormatType, FileName)
            ElseIf (Me.appType = "Win") Then
                Me.Export_with_XSLT_Windows(dsExport, Headers, sFileds, FormatType, FileName)
            End If
        Catch exception As Exception
            Throw exception
        End Try
    End Sub

    Public Sub ExportDetails(ByVal objXml As XmlDocument, ByVal strTableName As String, ByVal ColumnList As ArrayList, ByVal Headers As ArrayList, ByVal FormatType As ExportFormat, ByVal FileName As String)
        Try
            ' Dim Headers1() As String
            'Headers1 = Headers.ToArray
            Dim DetailsTable As New DataTable
            DetailsTable = ConvertXmlToDataTable(objXml, strTableName)
            If (DetailsTable.Rows.Count = 0) Then
                Throw New Exception("There are no details to export")
            End If
            Dim dsExport As New DataSet("Export")
            Dim table As DataTable = DetailsTable.Copy
            table.TableName = "Values"
            dsExport.Tables.Add(table)
            If (ColumnList.Count <> Headers.Count) Then
                Throw New Exception("ExportColumn List and Headers List should be of same length")
            End If
            If ((ColumnList.Count > table.Columns.Count) OrElse (Headers.Count > table.Columns.Count)) Then
                Throw New Exception("ExportColumn List should not exceed Total Columns")
            End If
            Dim sFileds As String() = New String(ColumnList.Count - 1) {}
            Dim i As Integer
            For i = 0 To ColumnList.Count - 1
                If ((ColumnList(i) < 0) OrElse (ColumnList(i) >= table.Columns.Count)) Then
                    Throw New Exception("ExportColumn Number should not exceed Total Columns Range")
                End If
                sFileds(i) = table.Columns.Item(CInt(ColumnList(i))).ColumnName
            Next i
            If (Me.appType = "Web") Then
                Me.Export_with_XSLT_Web(dsExport, Headers, sFileds, FormatType, FileName)
            ElseIf (Me.appType = "Win") Then
                'Me.Export_with_XSLT_Windows(dsExport, Headers, sFileds, FormatType, FileName)
            End If
        Catch exception As Exception
            Throw exception
        End Try
    End Sub

    Public Sub ExportDetails(ByVal DetailsTable As DataTable, ByVal ColumnList As Integer(), ByVal Headers As String(), ByVal FormatType As ExportFormat, ByVal FileName As String)
        Try
            If (DetailsTable.Rows.Count = 0) Then
                Throw New Exception("There are no details to export")
            End If
            Dim dsExport As New DataSet("Export")
            Dim table As DataTable = DetailsTable.Copy
            table.TableName = "Values"
            dsExport.Tables.Add(table)
            If (ColumnList.Length <> Headers.Length) Then
                Throw New Exception("ExportColumn List and Headers List should be of same length")
            End If
            If ((ColumnList.Length > table.Columns.Count) OrElse (Headers.Length > table.Columns.Count)) Then
                Throw New Exception("ExportColumn List should not exceed Total Columns")
            End If
            Dim sFileds As String() = New String(ColumnList.Length - 1) {}
            Dim i As Integer
            For i = 0 To ColumnList.Length - 1
                If ((ColumnList(i) < 0) OrElse (ColumnList(i) >= table.Columns.Count)) Then
                    Throw New Exception("ExportColumn Number should not exceed Total Columns Range")
                End If
                sFileds(i) = table.Columns.Item(ColumnList(i)).ColumnName
            Next i
            If (Me.appType = "Web") Then
                Me.Export_with_XSLT_Web(dsExport, Headers, sFileds, FormatType, FileName)
            ElseIf (Me.appType = "Win") Then
                Me.Export_with_XSLT_Windows(dsExport, Headers, sFileds, FormatType, FileName)
            End If
        Catch exception As Exception
            Throw exception
        End Try
    End Sub
    


    ' Fields
    Private appType As String
    Private response As HttpResponse

    ' Nested Types
    Public Enum ExportFormat
        ' Fields
        CSV = 1
        Excel = 2
    End Enum



End Class
