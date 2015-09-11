Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Xml
Imports System.Text
Imports System.Security.Cryptography

Public Class EncyrptDeCyrpt
    Private key() As Byte = {}
    Private IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}

    Public Function Decrypt(ByVal stringToDecrypt As String, ByVal sEncryptionKey As String) As String
        Dim inputByteArray(stringToDecrypt.Length) As Byte
        Try
            key = System.Text.Encoding.UTF8.GetBytes(Left(sEncryptionKey, 8))
            Dim des As New DESCryptoServiceProvider()
            inputByteArray = Convert.FromBase64String(stringToDecrypt)
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
            Return encoding.GetString(ms.ToArray())
        Catch e As Exception
            Return e.Message
        End Try
    End Function
    'Function OverLoading just to hide sEncryptionKey from input
    Public Function Decrypt(ByVal stringToDecrypt As String) As String
        If stringToDecrypt = "" Then
            Return stringToDecrypt
            Exit Function
        End If
        Dim inputByteArray(stringToDecrypt.Length) As Byte
        inputByteArray = GetAsBytes(stringToDecrypt)
        Try
            System.Diagnostics.Debug.WriteLine(Convert.ToBase64String(inputByteArray))
            key = System.Text.Encoding.UTF8.GetBytes("12345678")
            Dim des As New DESCryptoServiceProvider()
            ' inputByteArray = Convert.FromBase64String(stringToDecrypt)
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            System.Diagnostics.Debug.WriteLine(inputByteArray)
            Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
            Return encoding.GetString(ms.ToArray())



            'Dim objDES As New DESCryptoServiceProvider
            'Dim objMemoryStream As New MemoryStream
            'Dim objCryptoStream As New CryptoStream(objMemoryStream, objDES.CreateDecryptor(byteKey, byteVector), CryptoStreamMode.Write)
            'objCryptoStream.Write(byteData, 0, byteData.Length)
            'objCryptoStream.FlushFinalBlock()



            ' convert to string


            'Dim objEncoding As System.Text.Encoding = System.Text.Encoding.UTF8


            'strValue = objEncoding.GetString(objMemoryStream.ToArray())

        Catch e As Exception
            Return e.Message
        End Try
    End Function

    Public Function Encrypt(ByVal stringToEncrypt As String, ByVal SEncryptionKey As String) As String
        Try
            key = System.Text.Encoding.UTF8.GetBytes(Left(SEncryptionKey, 8))
            Dim des As New DESCryptoServiceProvider()
            Dim inputByteArray() As Byte = Encoding.UTF8.GetBytes(stringToEncrypt)
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()

            Return Convert.ToBase64String(ms.ToArray())
        Catch e As Exception
            Return e.Message
        End Try
    End Function
    'Function OverLoading just to hide sEncryptionKey from input
    'Here we are hardcoded the sEncryptionKey 

    Public Function Encrypt(ByVal stringToEncrypt As String) As String
        Try
            If stringToEncrypt = "" Then
                Return stringToEncrypt
                Exit Function
            End If
            key = System.Text.Encoding.UTF8.GetBytes("12345678")
            Dim des As New DESCryptoServiceProvider()
            Dim inputByteArray() As Byte = Encoding.UTF8.GetBytes(stringToEncrypt)
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Return GetAsString(ms.ToArray())
        Catch e As Exception
            Return e.Message
        End Try
    End Function

    'This Function converts Encrypted to integers and alphabets
    Private Shared Function GetAsString(ByVal arrInput() As Byte) As String
        Dim i As Integer
        Dim sOutput As New StringBuilder(arrInput.Length)
        System.Diagnostics.Debug.WriteLine("Hex - Start")
        For i = 0 To arrInput.Length - 1
            System.Diagnostics.Debug.WriteLine(arrInput(i).ToString("X2") & " // " & arrInput(i).ToString())
            sOutput.Append(arrInput(i).ToString("X2"))
        Next
        System.Diagnostics.Debug.WriteLine("Hex - End")
        Return sOutput.ToString()
    End Function


    Private Shared Function GetAsBytes(ByVal strInput As String) As Byte()
        Dim length As Integer = (strInput.Length / 2)
        Dim bytes(length - 1) As Byte
        System.Diagnostics.Debug.WriteLine("Hex - Start")
        For intCounter As Integer = 0 To (length - 1)
            System.Diagnostics.Debug.WriteLine(strInput.Substring((intCounter * 2), 2) & " // " & Short.Parse(strInput.Substring((intCounter * 2), 2), System.Globalization.NumberStyles.HexNumber).ToString())
            bytes(intCounter) = Short.Parse(strInput.Substring((intCounter * 2), 2), System.Globalization.NumberStyles.HexNumber)
        Next
        System.Diagnostics.Debug.WriteLine("Hex - End")
        Return bytes
    End Function

End Class