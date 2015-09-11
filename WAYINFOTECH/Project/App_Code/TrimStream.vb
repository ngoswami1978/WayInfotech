Imports Microsoft.VisualBasic

Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Text
Public Class TrimStream
    Inherits Stream
    Private stream As Stream
    Private streamWriter As StreamWriter
    Private _sink As Stream
    Private _position As Long
    Private dec As Decoder

    Public Sub New(ByVal stm As Stream)
        stream = stm
        streamWriter = New StreamWriter(stream, System.Text.Encoding.UTF8)
        _sink = stm
        dec = Encoding.UTF8.GetDecoder()
    End Sub

    
    Private bNewLine As Boolean = False
    
    Private bLastCharGT As Boolean = False
    
#Region " Code that will override Stream Properties and methods. "
    ' The following members of Stream must be overridden.

    Public Overrides ReadOnly Property CanRead() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property CanSeek() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property CanWrite() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property Length() As Long
        Get
            Return 0
        End Get
    End Property

    Public Overrides Property Position() As Long
        Get
            Return _position
        End Get
        Set(ByVal Value As Long)
            _position = Value
        End Set
    End Property

    Public Overrides Function Seek(ByVal offset As Long, _
           ByVal direction As System.IO.SeekOrigin) As Long
        Return _sink.Seek(offset, direction)
    End Function 'Seek


    Public Overrides Sub SetLength(ByVal length As Long)
        _sink.SetLength(length)
    End Sub 'SetLength


    Public Overrides Sub Close()
        _sink.Close()
    End Sub 'Close


    Public Overrides Sub Flush()
        _sink.Flush()
    End Sub 'Flush


    Public Overrides Function Read(ByVal MyBuffer() As Byte, _
      ByVal offset As Integer, ByVal count As Integer) As Integer
        _sink.Read(MyBuffer, offset, count)
    End Function

#End Region

    Private arBlanks As Char() = Nothing

    Public Overloads Overrides Sub Write(ByVal buffer As Byte(), ByVal offset As Integer, ByVal count As Integer)
        Dim nCharCnt As Integer = dec.GetCharCount(buffer, offset, count)
        Dim result As Char() = New Char(nCharCnt - 1) {}
        Dim nDecoded As Integer = dec.GetChars(buffer, offset, count, result, 0)

        If nDecoded <= 0 Then
            Return
        End If

        Dim nFirstNonBlank As Integer = -1
        'position of first non-black line char
        Dim nLastNonBlank As Integer = -1
        'position of last non-black line char
        Dim nFirstLineChar As Integer = 0
        'position of first line char (any)

        Dim bFirstLine As Boolean = True
        For nPos As Integer = 0 To nDecoded

            Dim bLastLine As Boolean = nPos = nDecoded

            Dim c As Char
            If nPos < nDecoded Then
                c = result(nPos)
            Else
                c = Chr(10)
            End If
            'IIf((nPos <= nDecoded), result(nPos), Chr(10))
            If c = Chr(10) Then
                'handle new line
                'first line, and we have important

                'white chars from previous Write() call

                If bFirstLine AndAlso (arBlanks IsNot Nothing) Then
                    'current line contains non-blank chars

                    '- write white chars from previous call

                    If nFirstNonBlank >= 0 Then
                        If arBlanks.Length > 0 Then
                            streamWriter.Write(arBlanks, 0, arBlanks.Length)
                        End If

                        arBlanks = Nothing
                        nFirstNonBlank = 0
                        bNewLine = False
                    End If
                End If
                bFirstLine = False

                'current line contains any non-white chars - write them

                If nFirstNonBlank >= 0 Then
                    If bNewLine AndAlso (result(nFirstNonBlank) <> "<"c) Then
                        streamWriter.WriteLine()
                        'write new line char ?

                    End If

                    'write current line (trimmed)

                    streamWriter.Write(result, nFirstNonBlank, nLastNonBlank - nFirstNonBlank + 1)

                    'setting variables...

                    If Not bLastLine Then
                        nFirstNonBlank = -1
                        nLastNonBlank = -1
                        nFirstLineChar = nPos + 1
                    End If
                    bNewLine = Not bLastCharGT
                    bLastCharGT = False
                End If

                If bLastLine Then
                    'last line - optionally remember white chars from its end

                    'empty line and we don't have any

                    'white chars from previous call - nothing to do

                    If (arBlanks Is Nothing) AndAlso (nFirstNonBlank < 0) Then
                    ElseIf nLastNonBlank < nDecoded - 1 Then
                        'there was white chars at end of this line

                        Dim nNumBlanks As Integer, nFirstBlank As Integer
                        If nLastNonBlank < 0 Then
                            nNumBlanks = nDecoded - nFirstLineChar
                            nFirstBlank = nFirstLineChar
                        Else
                            nNumBlanks = nDecoded - nLastNonBlank - 1
                            nFirstBlank = nLastNonBlank + 1
                        End If

                        If (arBlanks Is Nothing) OrElse (arBlanks.Length <= 0) Then
                            'create new array?

                            arBlanks = New Char(nNumBlanks - 1) {}
                            Array.Copy(result, nFirstBlank, arBlanks, 0, nNumBlanks)
                        Else
                            'append at end of existsing array
                            Dim ar As Char() = New Char(arBlanks.Length + nNumBlanks - 1) {}
                            arBlanks.CopyTo(ar, 0)
                            Array.Copy(result, nFirstBlank, ar, arBlanks.Length, nNumBlanks)
                            arBlanks = ar
                        End If
                    Else
                        'line without white-chars at end

                        '- mark this using zero-sized array

                        arBlanks = New Char(-1) {}
                    End If

                    'set variable...

                    bNewLine = False
                End If
            ElseIf Not [Char].IsWhiteSpace(c) Then

                'handle non-white chars
                If nFirstNonBlank < 0 Then
                    nFirstNonBlank = nPos
                End If
                nLastNonBlank = nPos
                bLastCharGT = (c = ">"c)
            End If
        Next

        streamWriter.Flush()
    End Sub

    ' other overrided Stream functions and properties goes here

End Class
