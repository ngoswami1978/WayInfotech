'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Tapan $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizShared/bzShared.vb $
'$Workfile: bzShared.vb $
'$Revision: 6 $
'$Archive: /AAMS/Components/bizShared/bzShared.vb $
'$Modtime: 19/12/09 10:58a $

Imports System.IO
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Configuration

Namespace bizShared
    Public Class bzShared
        Private Const Key As Integer = 1793
        '************************************************************* 
        ' Purpose:
        '           Checks the passed string for Special Characters.
        ' Inputs:
        '     	    String to be checked
        ' Returns:
        '        	True : If string contains only AlphaNumerice & Spaces
        '        	False: If string contains Characters other than AlphaNumerice & Spaces
        '*************************************************************
        Public Shared Function CheckSplCharacter(ByVal strParam As String) As Boolean
            Dim lintCounter As Integer
            For lintCounter = 1 To Len(strParam)
                If (Not (UCase(Mid(strParam, lintCounter, 1)) Like "[A-Z]")) Then
                    If (Not (UCase(Mid(strParam, lintCounter, 1)) Like "[0-9]")) Then
                        If (Not (UCase(Mid(strParam, lintCounter, 1)) Like " ")) Then
                            Return False
                            Exit Function
                        End If
                    End If
                End If
            Next
            Return True
        End Function

        '************************************************************* 
        ' Purpose:
        '           Converts the Passed Integer in Date 
        ' Inputs:
        '     	    Integer in format (YYYYMMDD)
        ' Returns:
        '        	Converted Date
        '*************************************************************
        Public Shared Function ConvertDate(ByVal intDate As Integer) As Date
            Dim dtDateFrom As New Date(Left(CType(intDate, String), 4), Mid(CType(intDate, String), 5, 2), Right(CType(intDate, String), 2))
            Return dtDateFrom
        End Function

        '************************************************************* 
        ' Purpose:
        '           Converts the Passed Date in Integer
        ' Inputs:
        '     	    Date
        ' Returns:
        '        	Integer in format (YYYYMMDD)
        '*************************************************************
        Public Shared Function ConvertDate(ByVal dtDate As Date) As Integer
            Return CType(Format(dtDate, "yyyyMMdd"), Integer)
        End Function

        '************************************************************* 
        ' Purpose:
        '           Converts the Passed String Date in Integer
        ' Inputs:
        '     	    String Date
        ' Returns:
        '        	Integer in format (YYYYMMDD)
        '*************************************************************
        Public Shared Function ConvertDate(ByVal strDate As String) As Integer
            Dim arrDate As Array
            arrDate = Split(strDate, "/", -1, 1)
            Dim strDate1 As String
            strDate1 = arrDate(2)
            If CType(arrDate(0), Integer) < 10 Then
                strDate1 = strDate1 + "0" + arrDate(0)
            Else
                strDate1 = strDate1 + arrDate(0)
            End If
            If CType(arrDate(1), Integer) < 10 Then
                strDate1 = strDate1 + "0" + arrDate(1)
            Else
                strDate1 = strDate1 + arrDate(1)
            End If
            Return strDate1
        End Function

        '************************************************************* 
        ' Purpose:
        '           Converts the Passed String Date in Integer
        ' Inputs:
        '     	    Date
        ' Returns:
        '        	Date in String
        '*************************************************************
        Public Shared Function ConvertDateToStr(ByVal dtmDate As Date) As String
            Dim lstrDate As String
            Dim lstrDatePart As String

            lstrDate = ""
            If Not IsDate(dtmDate) Then Return lstrDate

            lstrDate = DatePart(DateInterval.Year, dtmDate)

            lstrDatePart = DatePart(DateInterval.Month, dtmDate)
            lstrDate = lstrDate & IIf(Len(lstrDatePart) = 2, lstrDatePart, "0" & lstrDatePart)

            lstrDatePart = DatePart(DateInterval.Day, dtmDate)
            lstrDate = lstrDate & IIf(Len(lstrDatePart) = 2, lstrDatePart, "0" & lstrDatePart)

            Return lstrDate
        End Function

        '*************************************************************
        ' Purpose:
        '           Retrieve the Configuration Value for the Field
        ' Inputs:
        '     	    String: Field Name
        ' Returns:
        '        	String: Field Value
        '*************************************************************
        Public Shared Function GetConfigValue(ByVal strFIELD_NAME As String) As String
            Dim strValue As String
            Dim objConnection As New SqlConnection(GetConnectionString)
            Dim objCommand As New SqlCommand
            With objCommand
                .CommandText = "UP_GET_CONFIG_VALUE"
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@FD_NAME", SqlDbType.Char, 25)
                .Parameters("@FD_NAME").Value = strFIELD_NAME
            End With
            Try
                'OPEN CONNECTION
                objConnection.Open()
                objCommand.Connection = objConnection
                strValue = objCommand.ExecuteScalar
                Return strValue
                'CLOSE CONNECTION
            Catch EX As Exception
                Return ""
            Finally
                If Not objConnection.State = ConnectionState.Closed Then objConnection.Close()
                objCommand.Dispose()
            End Try
        End Function

        '*************************************************************
        ' Purpose:
        '           Returns the Week Day Number
        ' Inputs:
        '     	    Byte: WeekDay
        ' Returns:
        '        	Byte: WeekDay increased by 1
        '*************************************************************
        Public Shared Function DayofWeek(ByVal WeekDay As Byte) As Byte
            Return WeekDay + 1
        End Function

        '***************************************************************************** 
        ' Purpose:
        '           Function is user to maintai the Error Log.
        ' Inputs:
        '           gstrMODULE_NAME
        '                     Class Name
        '           lstrMETHOD_NAME
        '                     Method Name          
        '           objExcep
        '                     Exception 
        ' Returns:   
        '          None
        '*****************************************************************************
        Public Shared Sub LogWrite(ByVal lstrMODULE_NAME As String, ByVal lstrMETHOD_NAME As String, ByVal objExcep As Exception)
            'Use A Static Path to store the log file
            Dim lstrLogPath As String

            lstrLogPath = System.Configuration.ConfigurationSettings.AppSettings("LogPath").Trim 'Trim(My.MySettings.Default.LogPath)
            If lstrLogPath = "" Then
                lstrLogPath = "C:\Admin"
            End If
            If Not Directory.Exists(lstrLogPath) Then
                Directory.CreateDirectory(lstrLogPath)
            End If
            Dim strpath As String = lstrLogPath & "\" & lstrMODULE_NAME & ConvertDateToStr(Now.Date) & ".log"
            Dim objfilestream As New FileStream(strpath, FileMode.Append, FileAccess.Write, FileShare.Write)
            Dim ObjFilewriter As New StreamWriter(objfilestream)
            'It Maintain date , time ,source of error, and message decription of the error
            ObjFilewriter.WriteLine("***************************************************************")
            ObjFilewriter.WriteLine("Class Name:" & lstrMODULE_NAME & " Method Name:" & lstrMETHOD_NAME & " " & Today & ControlChars.Tab & Now & ControlChars.Tab & objExcep.Source.ToString & ControlChars.Tab & objExcep.Message.ToString & ControlChars.Tab & objExcep.HelpLink & ControlChars.Tab & objExcep.StackTrace.ToString)
            ObjFilewriter.WriteLine("***************************************************************")
            ObjFilewriter.Flush()
            ObjFilewriter.Close()
        End Sub

        '***************************************************************************** 
        ' Purpose:
        '           Function is user to maintai the Error Log.
        ' Inputs:
        '           gstrMODULE_NAME
        '                     Class Name
        '           lstrMETHOD_NAME
        '                     Method Name          
        '           objExcep
        '                     Exception 
        ' Returns:   
        '          None
        '*****************************************************************************
        Public Shared Sub LogWriteText(ByVal lstrMODULE_NAME As String, ByVal lstrMETHOD_NAME As String, ByVal strtext As String, ByVal strLineNo As String)
            'Use A Static Path to store the log file
            Dim lstrLogPath As String

            lstrLogPath = System.Configuration.ConfigurationSettings.AppSettings("LogPath").Trim 'Trim(My.MySettings.Default.LogPath)
            If lstrLogPath = "" Then
                lstrLogPath = "C:\Admin"
            End If
            If Not Directory.Exists(lstrLogPath) Then
                Directory.CreateDirectory(lstrLogPath)
            End If
            Dim strpath As String = lstrLogPath & "\" & lstrMODULE_NAME & ConvertDateToStr(Now.Date) & ".log"
            Dim objfilestream As New FileStream(strpath, FileMode.Append, FileAccess.Write, FileShare.Write)
            Dim ObjFilewriter As New StreamWriter(objfilestream)
            'It Maintain date , time ,source of error, and message decription of the error
            ObjFilewriter.WriteLine("***************************************************************")
            ObjFilewriter.WriteLine("Class Name:" & lstrMODULE_NAME & " Method Name:" & lstrMETHOD_NAME & " " & Today & ControlChars.Tab & Now & ControlChars.Tab & strtext & ControlChars.Tab & strLineNo)
            ObjFilewriter.WriteLine("***************************************************************")
            ObjFilewriter.Flush()
            ObjFilewriter.Close()
        End Sub
        '***************************************************************************** 
        ' Purpose:
        '           Function is user to fill the error status
        ' Inputs:
        '           objXml
        '                     xmldocument
        '           strErrCode
        '                     Error Code          
        '           strErrDesc
        '                     Error Description
        ' Returns:   
        '          XMLDocument
        '*****************************************************************************
        Public Shared Sub FillErrorStatus(ByRef objXml As Xml.XmlDocument, ByVal strErrCode As String, ByVal strErrDesc As String)
            Dim objErrNode As XmlNode
            objErrNode = objXml.DocumentElement.SelectSingleNode("Errors")
            objErrNode.Attributes("Status").Value = "TRUE"
            objErrNode = objXml.DocumentElement.SelectSingleNode("Errors/Error")
            objErrNode.Attributes("Code").Value = strErrCode
            objErrNode.Attributes("Description").Value = strErrDesc
        End Sub

        '***************************************************************************** 
        ' Purpose:
        ' Function that will build/create the global connection string
        ' Inputs:
        ' LoginName,Server,Database,Password from-config file
        ' Returns: 
        ' strConString
        '*****************************************************************************
        Public Shared Function GetConnectionString() As String
            Dim lstrServerName As String
            Dim lstrDBName As String
            Dim lstrLoginName As String
            Dim lstrPassword As String
            Dim lstrConString As String
            Dim lstrMaxPoolSize As String
            Dim lstrNet As String
            Dim lstrTrusted As String
            Dim objBarcode As New IPOSS.bizBarcode.bzBarcode

            'Initialize to values given in app.config



            lstrLoginName = System.Configuration.ConfigurationSettings.AppSettings("LoginName").Trim 'Trim(My.MySettings.Default.LoginName)
            lstrServerName = System.Configuration.ConfigurationSettings.AppSettings("Server").Trim  'Trim(My.MySettings.Default.Server)
            lstrDBName = System.Configuration.ConfigurationSettings.AppSettings("Database").Trim  'Trim(My.MySettings.Default.Database)
            lstrPassword = objBarcode.Decrypt(Trim(System.Configuration.ConfigurationSettings.AppSettings("Passkey")), Key) 'objBarcode.Decrypt(Trim(My.MySettings.Default.Passkey), Key)
            lstrMaxPoolSize = System.Configuration.ConfigurationSettings.AppSettings("MaxConnectionPool").Trim 'Trim(My.MySettings.Default.MaxConnectionPool)
            lstrNet = System.Configuration.ConfigurationSettings.AppSettings("CommProtocol").Trim 'Trim(My.MySettings.Default.CommProtocol)
            lstrTrusted = System.Configuration.ConfigurationSettings.AppSettings("TrustedConnection").Trim 'Trim(My.MySettings.Default.TrustedConnection)

            'If values are not present in app.config, set them to default values
            If lstrLoginName = "" Then lstrLoginName = "sa"
            If lstrPassword = "" Then lstrPassword = "dbsa"
            If lstrServerName = "" Then lstrServerName = lstrServerName
            If lstrDBName = "" Then lstrDBName = lstrDBName
            If lstrMaxPoolSize = "" Then lstrMaxPoolSize = 5

            'Finally, create the connection string
            lstrConString = "server=" & lstrServerName & ";database= " & lstrDBName & ";user id=" & lstrLoginName & ";password=" & lstrPassword & ";Pooling=True; Min Pool Size=0; Max Pool Size=" & Val(lstrMaxPoolSize) & ";Trusted_Connection=" & lstrTrusted & ";Net = " & lstrNet

            'LogWrite("ConnnectionString", "ConnectionString", New ExecutionEngineException("lstrConString"))

            lstrServerName = ""
            lstrDBName = ""
            lstrLoginName = ""
            lstrPassword = ""
            Return lstrConString
        End Function

        '***************************************************************************** 
        ' Purpose:
        ' Function that will build/create the global connection string
        ' Inputs:
        ' LoginName,Server,Database,Password from-config file
        ' Returns: 
        ' strConString
        '*****************************************************************************
        Public Shared Function GetDOCConnectionString() As String
            Dim lstrServerName As String
            Dim lstrDBName As String
            Dim lstrLoginName As String
            Dim lstrPassword As String
            Dim lstrConString As String
            Dim lstrMaxPoolSize As String
            Dim lstrNet As String
            Dim lstrTrusted As String
            Dim objBarcode As New IPOSS.bizBarcode.bzBarcode

            'Initialize to values given in app.config



            lstrLoginName = System.Configuration.ConfigurationSettings.AppSettings("DOCLoginName").Trim 'Trim(My.MySettings.Default.LoginName)
            lstrServerName = System.Configuration.ConfigurationSettings.AppSettings("DOCServer").Trim  'Trim(My.MySettings.Default.Server)
            lstrDBName = System.Configuration.ConfigurationSettings.AppSettings("DOCDatabase").Trim  'Trim(My.MySettings.Default.Database)
            lstrPassword = objBarcode.Decrypt(Trim(System.Configuration.ConfigurationSettings.AppSettings("DOCPasskey")), Key) 'objBarcode.Decrypt(Trim(My.MySettings.Default.Passkey), Key)
            lstrMaxPoolSize = System.Configuration.ConfigurationSettings.AppSettings("MaxConnectionPool").Trim 'Trim(My.MySettings.Default.MaxConnectionPool)
            lstrNet = System.Configuration.ConfigurationSettings.AppSettings("CommProtocol").Trim 'Trim(My.MySettings.Default.CommProtocol)
            lstrTrusted = System.Configuration.ConfigurationSettings.AppSettings("TrustedConnection").Trim 'Trim(My.MySettings.Default.TrustedConnection)

            'If values are not present in app.config, set them to default values
            If lstrLoginName = "" Then lstrLoginName = "sa"
            If lstrPassword = "" Then lstrPassword = "dbsa"
            If lstrServerName = "" Then lstrServerName = lstrServerName
            If lstrDBName = "" Then lstrDBName = lstrDBName
            If lstrMaxPoolSize = "" Then lstrMaxPoolSize = 5

            'Finally, create the connection string
            lstrConString = "server=" & lstrServerName & ";database= " & lstrDBName & ";user id=" & lstrLoginName & ";password=" & lstrPassword & ";Pooling=True; Min Pool Size=0; Max Pool Size=" & Val(lstrMaxPoolSize) & ";Trusted_Connection=" & lstrTrusted & ";Net = " & lstrNet

            'LogWrite("ConnnectionString", "ConnectionString", New ExecutionEngineException("lstrConString"))

            lstrServerName = ""
            lstrDBName = ""
            lstrLoginName = ""
            lstrPassword = ""
            Return lstrConString
        End Function


        '***************************************************************************** 
        ' Purpose:
        ' Function that will build/create the global connection string
        ' Inputs:
        ' LoginName,Server,Database,Password from-config file
        ' Returns: 
        ' strConString
        '*****************************************************************************
        Public Shared Function GetNIDTConnectionString() As String
            Dim lstrServerName As String
            Dim lstrDBName As String
            Dim lstrLoginName As String
            Dim lstrPassword As String
            Dim lstrConString As String
            Dim lstrMaxPoolSize As String
            Dim lstrNet As String
            Dim lstrTrusted As String
            Dim objBarcode As New IPOSS.bizBarcode.bzBarcode

            'Initialize to values given in app.config



            lstrLoginName = System.Configuration.ConfigurationSettings.AppSettings("NIDTLoginName").Trim 'Trim(My.MySettings.Default.LoginName)
            lstrServerName = System.Configuration.ConfigurationSettings.AppSettings("NIDTServer").Trim  'Trim(My.MySettings.Default.Server)
            lstrDBName = System.Configuration.ConfigurationSettings.AppSettings("NIDTDatabase").Trim  'Trim(My.MySettings.Default.Database)
            lstrPassword = objBarcode.Decrypt(Trim(System.Configuration.ConfigurationSettings.AppSettings("NIDTPasskey")), Key) 'objBarcode.Decrypt(Trim(My.MySettings.Default.Passkey), Key)
            lstrMaxPoolSize = System.Configuration.ConfigurationSettings.AppSettings("MaxConnectionPool").Trim 'Trim(My.MySettings.Default.MaxConnectionPool)
            lstrNet = System.Configuration.ConfigurationSettings.AppSettings("CommProtocol").Trim 'Trim(My.MySettings.Default.CommProtocol)
            lstrTrusted = System.Configuration.ConfigurationSettings.AppSettings("TrustedConnection").Trim 'Trim(My.MySettings.Default.TrustedConnection)

            'If values are not present in app.config, set them to default values
            If lstrLoginName = "" Then lstrLoginName = "sa"
            If lstrPassword = "" Then lstrPassword = "nidt010"
            If lstrServerName = "" Then lstrServerName = lstrServerName
            If lstrDBName = "" Then lstrDBName = lstrDBName
            If lstrMaxPoolSize = "" Then lstrMaxPoolSize = 5

            'Finally, create the connection string
            lstrConString = "server=" & lstrServerName & ";database= " & lstrDBName & ";user id=" & lstrLoginName & ";password=" & lstrPassword & ";Pooling=True; Min Pool Size=0; Max Pool Size=" & Val(lstrMaxPoolSize) & ";Trusted_Connection=" & lstrTrusted & ";Net = " & lstrNet

            'LogWrite("ConnnectionString", "ConnectionString", New ExecutionEngineException("lstrConString"))

            lstrServerName = ""
            lstrDBName = ""
            lstrLoginName = ""
            lstrPassword = ""
            Return lstrConString
        End Function


        Public Shared Function GetDateInt(ByVal dt As String) As Integer
            ''**********************************************************************
            '   Sub
            ''**********************************************************************
            'DESC :To make the date in a proper format
            'Input : string
            'Output: Integer
            Dim arrDate As Array
            arrDate = Split(dt, "/", -1, 1)
            Dim dt1 As String
            dt1 = arrDate(2)

            If CType(arrDate(1), Integer) < 10 Then
                dt1 = dt1 + "0" + CStr(CInt(arrDate(1)))
            Else
                dt1 = dt1 + arrDate(1)
            End If
            If CType(arrDate(0), Integer) < 10 Then
                dt1 = dt1 + "0" + CStr(CInt(arrDate(0)))
            Else
                dt1 = dt1 + arrDate(0)
            End If
            Return dt1
        End Function

        
    End Class
End Namespace