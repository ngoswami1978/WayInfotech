'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Ashishsrivastava $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizTravelAgency/bzAgencyCRSUse.vb $
'$Workfile: bzAgencyCRSUse.vb $
'$Revision: 11 $
'$Archive: /AAMS/Components/bizTravelAgency/bzAgencyCRSUse.vb $
'$Modtime: 9/03/11 2:22p $

Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizTravelAgency
    Public Class bzAgencyCRSUse
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzAgencyCRSUse"
        Const strADDAGENCYCRSUSE_OUTPUT = "<UP_UPDATECRSDETAILS_OUTPUT><CRS RN='' LCODE='' DATE='' CURRENTID='' CRS='' OFFICEID='' Action='' /><Errors Status=''><Error Code='' Description='' /></Errors></UP_UPDATECRSDETAILS_OUTPUT>"
        Const strDELETE_INPUT = "<MS_DELETEAGENCYCRSDETAILS_INPUT><RN></RN></MS_DELETEAGENCYCRSDETAILS_INPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETEAGENCYCRSDETAILS_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETEAGENCYCRSDETAILS_OUTPUT>"
        Const strUPDATE_INPUT = "<UP_UPDATECRSDETAILS_INPUT><CRS RN='' LCODE='' DATE='' CURRENTID='' CRS='' OFFICEID='' EMPLOYEEID='' Action='' /></UP_UPDATECRSDETAILS_INPUT>"
        Const strUPDATE_OUTPUT = "<UP_UPDATECRSDETAILS_OUTPUT><CRS RN='' LCODE='' DATE='' CURRENTID='' CRS='' OFFICEID='' Action='' /><Errors Status=''><Error Code='' Description='' /></Errors></UP_UPDATECRSDETAILS_OUTPUT>"
        Const strVIEW_INPUT = "<MS_VIEWAGENCYCRSDETAIL_INPUT><RN></RN></MS_VIEWAGENCYCRSDETAIL_INPUT>"
        Const strVIEW_OUTPUT = "<MS_VIEWAGENCYCRSDETAIL_OUTPUT><CRS RN='' LCODE='' DATE='' CURRENTID='' CRS='' OFFICEID='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWAGENCYCRSDETAIL_OUTPUT>"
        Const strHISTORY_INPUT = "<MS_GETHISTORYCRSUSE_INPUT><LCODE></LCODE></MS_GETHISTORYCRSUSE_INPUT>"
        Const strHISTORY_OUTPUT = "<MS_GETHISTORYCRSUSE_OUTPUT><HISTORYDETAIL  LCODE='' EMPLOYEENAME='' CHANGEDATA='' DATETIME='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_GETHISTORYCRSUSE_OUTPUT>"


        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add
            'Declare Output XML String
            '***********************************************************************
            'Purpose: 
            'Input:None
            'Output:Xml Document
            '<UP_UPDATECRSDETAILS_OUTPUT>
            '<CRS RN='' LCODE='' DATE='' CURRENTID='' CRS='' OFFICEID='' Action='' />
            '   <Errors Status=''>
            '       <Error Code='' Description='' />
            '   </Errors>
            '</UP_UPDATECRSDETAILS_OUTPUT>
            '**************************************************************************
            Dim strMETHOD_NAME As String = "Add"
            Dim objXMLDoc As New XmlDocument
            Try
                objXMLDoc.LoadXml(strADDAGENCYCRSUSE_OUTPUT)
            Catch Exec As Exception
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objXMLDoc, "101", Exec.Message)
                Return objXMLDoc
            End Try
            Return objXMLDoc

        End Function

        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Delete
            '***********************************************************************
            'Purpose:This function deletes a Agency Staff.

            'Input:XmlDocument
            '<MS_DELETEAGENCYCRSDETAILS_INPUT>
            '<RN></RN>
            '</MS_DELETEAGENCYCRSDETAILS_INPUT>
            'Output :
            '<MS_DELETEAGENCYCRSDETAILS_OUTPUT>
            '   <Errors Status=''>
            '       <Error Code='' Description='' />
            '   </Errors><
            '/MS_DELETEAGENCYCRSDETAILS_OUTPUT>    
            '************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Delete"
            Dim strAGENCYCRSUSEID_id As String

            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0
                strAGENCYCRSUSEID_id = DeleteDoc.DocumentElement.SelectSingleNode("RN").InnerText.Trim
                If strAGENCYCRSUSEID_id = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_CRSUSE"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"
                    .Parameters.Add(New SqlParameter("@RN", SqlDbType.Int))
                    .Parameters("@RN").Value = strAGENCYCRSUSEID_id
                    objSqlCommand.Connection.Open()
                    intRecordsAffected = .ExecuteNonQuery()
                    objSqlCommand.Connection.Close()
                End With

                'Checking whether record is deleted successfully or not
                If intRecordsAffected > 0 Then
                    objDeleteDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    Return (objDeleteDocOutput)
                Else
                    objDeleteDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "TRUE"
                    Call bzShared.FillErrorStatus(objDeleteDocOutput, "101", "Record has not been deleted!")
                    Return (objDeleteDocOutput)
                End If
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objDeleteDocOutput, "101", Exec.Message)
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                'Fill Error Status
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objDeleteDocOutput, "101", Exec.Message)
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objDeleteDocOutput

        End Function

        Public Function Search(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Search
            '---------Not to be implemented----------
        End Function

        Public Function Update(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Update
            '***********************************************************************
            'Purpose:This function Inserts/Updates Agency CRS Details.
            'Input  :
            '<UP_UPDATECRSDETAILS_INPUT>
            '<CRS RN='' LCODE='' DATE='' CURRENTID='' CRS='' OFFICEID='' EMPLOYEEID ='' Action=''  />
            '</UP_UPDATECRSDETAILS_INPUT>


            '#########################################################################'
            'Purpose:This function Inserts/Updates/DELETE Agency CRS Details.
            ' CODE WRITTEN BY ASHISH SRIVASTAVA ON DATE 13-12-2007
            '############################  NEW XML  ###############################'
            '<UP_UPDATECRSDETAILS_INPUT>
            ' <CRS RN="157" LCODE="220" DATE="" CURRENTID="N" CRS="1W" OFFICEID="TRTRTT" Action="U" /> 
            '<CRS RN="155" LCODE="220" DATE="" CURRENTID="N" CRS="1P" OFFICEID="AS" Action="U" /> 
            '<CRS RN="158" LCODE="220" DATE="" CURRENTID="N" CRS="IG" OFFICEID="RTRTH" Action="U" /> 
            ' <CRS RN="161" LCODE="220" DATE="" CURRENTID="N" CRS="4" OFFICEID="22222" Action="U" /> 
            '<CRS RN="162" LCODE="220" DATE="" CURRENTID="N" CRS="4" OFFICEID="222222" Action="D" /> 
            '<CRS RN="22" LCODE="220" DATE="" CURRENTID="N" CRS="1W" OFFICEID="0B32" Action="U" /> 
            ' <CRS RN="159" LCODE="220" DATE="" CURRENTID="N" CRS="1B" OFFICEID="7869787Y8678658" Action="U" /> 
            '<CRS RN="160" LCODE="220" DATE="" CURRENTID="N" CRS="IG" OFFICEID="980890890808908" Action="U" /> 
            '<CRS RN="" LCODE="220" DATE="" CURRENTID="T" CRS="IG" OFFICEID="ytu" Action="I" /> 
            '</UP_UPDATECRSDETAILS_INPUT>
            ''###############################  END INPUT XML  #########################'

            'Output :
            '<UP_UPDATECRSDETAILS_OUTPUT>
            '<CRS RN='' LCODE='' DATE='' CURRENTID='' CRS='' OFFICEID=''  />
            '   <Errors Status=''>
            '       <Error Code='' Description='' />
            '   </Errors>
            '</UP_UPDATECRSDETAILS_OUTPUT>
            '************************************************************************
            Dim strAction As String
            Dim objSqlCommand As New SqlCommand
            Dim objHisSqlCommand As New SqlCommand

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objUpdateDocOutput As New XmlDocument
            Dim intRecordsAffected As Int32
            Dim objNode As XmlNode
            Dim objTran As SqlTransaction  'added by ashish
            Dim intRetId As Integer
            Dim intRetRegistrationId As Integer
            Dim objOutputXml1 As New XmlDocument
            Dim objOutputXml As New XmlDocument
            Dim boolTrueOfficeId As Boolean = False

            Const strMETHOD_NAME As String = "Update"

            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)
                objSqlConnection.Open()
                objTran = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)

                'Parameter
                With objSqlCommand
                    .Connection = objSqlConnection
                    .Transaction = objTran
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_CRSUSE"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters.Add(New SqlParameter("@RN", SqlDbType.BigInt))
                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@DATE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@CURRENTID", SqlDbType.Char, 1))
                    .Parameters.Add(New SqlParameter("@CRSCODE", SqlDbType.Char, 2))
                    .Parameters.Add(New SqlParameter("@OFFICEID", SqlDbType.VarChar, 15))
                    .Parameters.Add(New SqlParameter("@RETUNID", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@RETURN_REGISTRATIONID", SqlDbType.Int))

                    .Parameters.Add(New SqlParameter("@XMLOUTPUT", SqlDbType.Xml))

                    .Parameters("@RETUNID").Direction = ParameterDirection.Output

                    '.Parameters("@RETUNID").Value = ""

                    .Parameters("@RETURN_REGISTRATIONID").Direction = ParameterDirection.Output
                    '.Parameters("@RETURN_REGISTRATIONID").Value = ""

                    .Parameters("@XMLOUTPUT").Direction = ParameterDirection.Output
                    '.Parameters("@XMLOUTPUT").Value = ""

                End With

                With objHisSqlCommand
                    .Connection = objSqlConnection
                    .Transaction = objTran
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_CRSUSEHISTORY"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters.Add(New SqlParameter("@RN", SqlDbType.BigInt))
                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@DATE", SqlDbType.Int))
                    .Parameters.Add(New SqlParameter("@CURRENTID", SqlDbType.Char, 1))
                    .Parameters.Add(New SqlParameter("@CRSCODE", SqlDbType.Char, 2))
                    .Parameters.Add(New SqlParameter("@OFFICEID", SqlDbType.VarChar, 15))
                    .Parameters.Add(New SqlParameter("@EMPLOYEEID", SqlDbType.VarChar, 15))
                    .Parameters.Add(New SqlParameter("@ID", SqlDbType.Int))
                    .Parameters("@ID").Direction = ParameterDirection.Output
                    .Parameters("@ID").Value = ""
                End With


                For Each objNode In UpdateDoc.DocumentElement.SelectNodes("CRS")
                    strAction = objNode.Attributes("Action").InnerText
                    objSqlCommand.Parameters("@Action").Value = strAction
                    objHisSqlCommand.Parameters("@Action").Value = strAction

                    If strAction = "I" Then
                        objSqlCommand.Parameters("@RN").Value = vbNullString
                    ElseIf strAction = "U" Or strAction = "D" Then
                        objSqlCommand.Parameters("@RN").Value = objNode.Attributes("RN").InnerText
                        objHisSqlCommand.Parameters("@RN").Value = objNode.Attributes("RN").InnerText
                    End If

                    If strAction = "I" Or strAction = "U" Or strAction = "D" Then
                        If objNode.Attributes("LCODE").InnerText = "" Then
                            Throw (New AAMSException("Location Code can't be blank."))
                        ElseIf objNode.Attributes("CURRENTID").InnerText = "" Then
                            Throw (New AAMSException("Current ID can't be blank."))
                        ElseIf objNode.Attributes("CRS").InnerText = "" Then
                            Throw (New AAMSException("CRS can't be blank."))
                        ElseIf objNode.Attributes("OFFICEID").InnerText = "" Then
                            Throw (New AAMSException("Office ID can't be blank."))
                        End If
                    Else
                        Throw (New AAMSException("Invalid Action Code."))
                    End If

                    objSqlCommand.Parameters("@LCODE").Value = objNode.Attributes("LCODE").InnerText
                    objHisSqlCommand.Parameters("@LCODE").Value = objNode.Attributes("LCODE").InnerText

                    objSqlCommand.Parameters("@CURRENTID").Value = objNode.Attributes("CURRENTID").InnerText
                    objHisSqlCommand.Parameters("@CURRENTID").Value = objNode.Attributes("CURRENTID").InnerText

                    objSqlCommand.Parameters("@CRSCODE").Value = objNode.Attributes("CRS").InnerText
                    objHisSqlCommand.Parameters("@CRSCODE").Value = objNode.Attributes("CRS").InnerText

                    objSqlCommand.Parameters("@OFFICEID").Value = objNode.Attributes("OFFICEID").InnerText
                    objHisSqlCommand.Parameters("@OFFICEID").Value = objNode.Attributes("OFFICEID").InnerText

                    objHisSqlCommand.Parameters("@EMPLOYEEID").Value = objNode.Attributes("EMPLOYEEID").InnerText

                    objSqlCommand.Parameters("@RETUNID").Value = ""
                    objSqlCommand.Parameters("@RETURN_REGISTRATIONID").Value = ""
                    objSqlCommand.Parameters("@XMLOUTPUT").Value = ""


                    objSqlCommand.Transaction = objTran
                    objHisSqlCommand.Transaction = objTran
                    If UCase(strAction) = "U" Or UCase(strAction) = "I" Then
                        intRecordsAffected = objHisSqlCommand.ExecuteNonQuery()

                    End If

                    intRecordsAffected = objSqlCommand.ExecuteNonQuery()
                    intRetId = objSqlCommand.Parameters("@RETUNID").Value

                    intRetRegistrationId = objSqlCommand.Parameters("@RETURN_REGISTRATIONID").Value
                    If intRetRegistrationId = 100 Then
                        boolTrueOfficeId = True
                        If objSqlCommand.Parameters("@XMLOUTPUT").Value <> "" Then
                            objOutputXml1.LoadXml(objSqlCommand.Parameters("@XMLOUTPUT").Value)
                            objOutputXml = objOutputXml1
                        End If
                    End If
                Next

                objTran.Commit()
                objSqlConnection.Close()
                objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"


                '############## WRITE CODE FOR SENDMAIL TO TRAINING DEPARTMENT WHEN 1A OFFICEID GET TRUE
                If boolTrueOfficeId = True Then
                    Dim objSqlCommandMailTemplate As New SqlCommand
                    Dim objSqlReaderMailTemplate As SqlDataReader
                    Dim objMailTemplateXML As New XmlDocument
                    Dim strMailTemplateXML As String = "<X><Details MailTemplateName='' MailTemplate=''/></X>"
                    Dim strMailTemplate As String = ""
                    Dim strLETTERTYPE As String = "", strEMAILIDTO As String = "", strCONTACT_PERSON_EMAILIDTO As String = "", strEMAILIDCC As String = "", strRegion As String = ""
                    Dim strSIGNATURE As String = "", strDESIGNATION As String = ""
                    Dim strLetter As String = "'"
                    Dim strCountryCode As String, strDate As String = "", strAgency As String = "", strAddress As String = "", strCity As String = ""
                    Dim strCountrySiteAddress As String = ""
                    Dim strCountryWiseSignature As String = ""
                    Dim strEmailFrom As String, strEmailTo As String, strEmailCC As String, strEmailSubject As String, strEmailBody As String, strSIGNATURE1 As String = "", strUID As String = "", strPWD As String = ""

                    Dim ObjSendMail As bizUtility.bzEmail
                    Dim objInputSendMailXml As New XmlDocument
                    Dim boolSendMailStatus As Boolean

                    Dim blnRecordFound As Boolean


                    objMailTemplateXML.LoadXml(strMailTemplateXML)
                    strLETTERTYPE = "Training Admin"
                    ' -- "Please visit our GGAMAINTRX (Where [[X]] is the region office lies in) page on" & _
                    objSqlConnection.Open()

                    With objSqlCommandMailTemplate
                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "[UP_SRO_EMAIL_TEMPLATES]"
                        .Connection = objSqlConnection

                        .Parameters.Add("@ACTION", SqlDbType.Char, 1)
                        .Parameters("@ACTION").Value = "V"

                        .Parameters.Add("@MailTemplateName", SqlDbType.VarChar, 45)
                        .Parameters("@MailTemplateName").Value = "ORDER_MAIL_TRAINING_DEPARTMENT"

                    End With

                    objSqlReaderMailTemplate = objSqlCommandMailTemplate.ExecuteReader()

                    Do While objSqlReaderMailTemplate.Read()
                        strMailTemplate = Trim(objSqlReaderMailTemplate.GetValue(objSqlReaderMailTemplate.GetOrdinal("MailTemplate")) & "") ' Get participant AutoMail Template
                    Loop
                    objSqlReaderMailTemplate.Close()
                    objSqlCommandMailTemplate.Dispose()

                    strLetter = strMailTemplate

                    strCONTACT_PERSON_EMAILIDTO = objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("CONTACT_PERSON_EMAILIDTO").InnerText.Trim
                    strEMAILIDTO = objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("EMAILID_TO").InnerText.Trim
                    strEMAILIDCC = objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("EMAILID_CC").InnerText.Trim

                    strRegion = objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("REGION").InnerText.Trim
                    strCountryCode = objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("COUNTRY_CODE").InnerText.Trim
                    strSIGNATURE1 = objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("SIGNATURE").InnerText.Trim

                    strDate = objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("DATE").InnerText.Trim
                    strAgency = objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("AGENCY").InnerText.Trim
                    strAddress = objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("ADDRESS").InnerText.Trim
                    strCity = objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("CITY").InnerText.Trim
                    strUID = objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("UID").InnerText.Trim
                    strPWD = objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("PWD").InnerText.Trim


                    strLetter = strLetter.Replace("[[COUNTRY_CODE]]", strCountryCode)

                    Select Case strCountryCode
                        Case "IN"
                            strCountrySiteAddress = "www.amadeus.co.in"
                            strCountryWiseSignature = "Amadeus India Training"
                        Case "LK"
                            strCountrySiteAddress = "http://www.amadeus.lk"
                            strCountryWiseSignature = "Amadeus Srilanka Training"
                        Case "BD"
                            strCountrySiteAddress = "www.amadeus.co.in"
                            strCountryWiseSignature = "Amadeus GTD Bangla Training"
                        Case "NP"
                            strCountrySiteAddress = "www.amadeus.co.in"
                            strCountryWiseSignature = "Amadeus Nepal Training"
                        Case "BT"
                            strCountrySiteAddress = "www.amadeus.co.in"
                            strCountryWiseSignature = "Amadeus India Training"
                        Case "ML"
                            strCountrySiteAddress = "http://www.amadeus.lk"
                            strCountryWiseSignature = "Amadeus Srilanka Training"
                    End Select

                    strLetter = strLetter.Replace("[[REGISTERONLINE_SITE_ADDRESS]]", strCountrySiteAddress)
                    strLetter = strLetter.Replace("[[SIGNATURE]]", strSIGNATURE1)

                    strLetter = strLetter.Replace("[UID]", strUID)
                    strLetter = strLetter.Replace("[PWD]", strPWD)
                    strLetter = strLetter.Replace("[DATE]", strDate)
                    strLetter = strLetter.Replace("[AGENCY]", strAgency)
                    strLetter = strLetter.Replace("[ADDRESS]", strAddress)
                    strLetter = strLetter.Replace("[CITY]", strCity)


                    ' If strRegion.ToUpper.Trim = "CMB" Or strRegion.ToUpper.Trim = "DAC" Or strRegion.ToUpper.Trim = "KTM" Then
                    If strCountryCode.ToUpper <> "IN" Then
                        strLetter = strLetter.Replace("[[X]]", "A")
                    ElseIf strRegion.Length > 1 Then
                        strLetter = strLetter.Replace("[[X]]", Left(strRegion, 1))
                    End If

                    strEmailFrom = System.Configuration.ConfigurationSettings.AppSettings("MAIL_SOURCE").ToString
                    'strEmailTo = strEMAILIDTO
                    'strEmailCC = strEMAILIDCC
                    strEmailTo = strCONTACT_PERSON_EMAILIDTO
                    strEmailBody = strLetter
                    strEmailSubject = strLETTERTYPE

                    Const strSendMail_INPUT = "<SENDMAILIMMEDIATE_INPUT><MAIL_DETAILS DESTINATION_TO='' SUBJECT='' MESSAGE='' SOURCE='' DESTINATION_CC='' DESTINATION_BCC=''/></SENDMAILIMMEDIATE_INPUT>"
                    objInputSendMailXml.LoadXml(strSendMail_INPUT)

                    With objInputSendMailXml.SelectSingleNode("SENDMAILIMMEDIATE_INPUT/MAIL_DETAILS")
                        .Attributes("SOURCE").InnerText = strEmailFrom
                        .Attributes("DESTINATION_TO").InnerText = strEmailTo
                        .Attributes("DESTINATION_CC").InnerText = strEmailCC
                        .Attributes("SUBJECT").InnerText = strEmailSubject
                        .Attributes("MESSAGE").InnerText = strEmailBody
                    End With

                    If strCONTACT_PERSON_EMAILIDTO.Trim <> "" Then
                        ObjSendMail = New bizUtility.bzEmail
                        boolSendMailStatus = ObjSendMail.SendMail(objInputSendMailXml)
                        'boolSendMailStatus = True
                        If boolSendMailStatus = False Then
                            blnRecordFound = False
                            'Throw (New AAMSException("Unable to send Mails !"))
                        Else
                            blnRecordFound = True
                        End If
                    End If
                End If

                'end ############## WRITE CODE FOR SENDMAIL TO TRAINING DEPARTMENT WHEN 1A OFFICEID GET TRUE


            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                If objSqlConnection.State <> ConnectionState.Closed Then
                    If Not objTran Is Nothing Then
                        objTran.Rollback()
                    End If
                    objSqlConnection.Close()
                End If
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                'Fill Error Status
                If objSqlConnection.State <> ConnectionState.Closed Then
                    If Not objTran Is Nothing Then
                        objTran.Rollback()
                    End If
                    objSqlConnection.Close()
                End If
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objUpdateDocOutput



            ' ##################   PREVCODE COMMENTED ON DATE 13-12-2007   ########################## 
            ' objNode = objUpdateDocOutput.DocumentElement.SelectSingleNode("CRS")
            ' objUpdateDocOutput.DocumentElement.RemoveChild(objNode)
            ' objUpdateDocOutput.DocumentElement.AppendChild(objUpdateDocOutput.ImportNode(UpdateDoc.DocumentElement.SelectSingleNode("CRS"), True))

            'Retrieving & Checking Details from Input XMLDocument
            'With UpdateDoc.DocumentElement.SelectSingleNode("CRS")
            'If ((.Attributes("RN").InnerText).Trim) = "" Then
            ' strAction = "I"
            ' Else
            ' strAction = "U"
            ' End If
            'If (.Attributes("LCODE").InnerText).Trim = "" Then
            ' Throw (New AAMSException("Location Code can't be blank."))
            ' End If
            'If (.Attributes("OFFICEID").InnerText).Trim = "" Then
            ' Throw (New AAMSException("Office Id Code can't be blank."))
            ' End If
            'If (.Attributes("CRS").InnerText).Trim = "" Then
            ' Throw (New AAMSException("CRS Code can't be blank."))
            'End If
            'End With

            'ADDING PARAMETERS IN STORED PROCEDURE

            'With UpdateDoc.DocumentElement.SelectSingleNode("CRS")
            ' objSqlCommand.Parameters("@ACTION").Value = strAction'

            'If (.Attributes("RN").InnerText).Trim <> "" Then
            ' objSqlCommand.Parameters("@RN").Value = .Attributes("RN").InnerText
            ' End If

            'If (.Attributes("LCODE").InnerText).Trim <> "" Then
            ' objSqlCommand.Parameters("@LCODE").Value = CInt(.Attributes("LCODE").InnerText)
            ' End If

            'If (.Attributes("CRS").InnerText).Trim <> "" Then
            ' objSqlCommand.Parameters("@CRSCODE").Value = .Attributes("CRS").InnerText
            ' End If

            'If (.Attributes("OFFICEID").InnerText).Trim <> "" Then
            ' objSqlCommand.Parameters("@OFFICEID").Value = .Attributes("OFFICEID").InnerText
            ' End If

            'If (.Attributes("CURRENTID").InnerText).Trim <> "" Then
            ' objSqlCommand.Parameters("@CURRENTID").Value = CInt(.Attributes("CURRENTID").InnerText)
            'End If

            'If (.Attributes("DATE").InnerText).Trim <> "" Then
            ' objSqlCommand.Parameters("@DATE").Value = CInt(.Attributes("DATE").InnerText)
            ' End If
            'End With

            'objSqlCommand.Connection.Open()
            'intRecordsAffected = objSqlCommand.ExecuteNonQuery()
            'intRetId = objSqlCommand.Parameters("@RETUNID").Value

            ' If UCase(strAction) = "I" Then
            ' intRetId = objSqlCommand.Parameters("@RETUNID").Value'

            'If intRetId = -1 Then
            ' Throw (New AAMSException("Agency Crs Use Already Exists!"))
            ' Else
            '    objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            '    With objUpdateDocOutput.DocumentElement.SelectSingleNode("CRS")
            '        .Attributes("RN").InnerText = intRetId
            '    End With
            '    End If
            '    ElseIf UCase(strAction) = "U" Then
            '    intRetId = objSqlCommand.Parameters("@RETUNID").Value
            '    If intRetId <= 0 Then
            '        Throw (New AAMSException("Unable to update!"))
            '    Else
            '        objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            '    End If
            '    End If

            'Catch Exec As AAMSException
            '    'CATCHING AAMS EXCEPTIONS
            '    bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
            '    bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
            '    Return objUpdateDocOutput
            'Catch Exec As Exception
            '    bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
            '    bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
            '    Return objUpdateDocOutput
            'Finally
            '    If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
            '    objSqlCommand.Dispose()
            'End Try
            'Return objUpdateDocOutput
            '#########################################################################################

        End Function

        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.View
            '***********************************************************************
            'Purpose:This function gives details of Agency Crs Use Details.
            'Input  :
            '<MS_VIEWAGENCYCRSDETAIL_INPUT>
            '   <RN></RN>
            '</MS_VIEWAGENCYCRSDETAIL_INPUT>

            'Output :
            '<MS_VIEWAGENCYCRSDETAIL_OUTPUT>
            '<CRS RN='' LCODE='' DATE='' CURRENTID='' CRS='' OFFICEID='' />
            '   <Errors Status=''>
            '       <Error Code='' Description='' />
            '   </Errors>
            '</MS_VIEWAGENCYCRSDETAIL_OUTPUT>"
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strAgencyStaff_ID As String
            Const strMETHOD_NAME As String = "View"
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(strVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument 
                strAgencyStaff_ID = IndexDoc.DocumentElement.SelectSingleNode("RN").InnerText.Trim
                If strAgencyStaff_ID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_CRSUSE"
                    .Connection = objSqlConnection

                    .Parameters.Add("@RN", SqlDbType.Int)
                    .Parameters("@RN").Value = strAgencyStaff_ID

                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"
                End With

                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("CRS")
                        .Attributes("RN").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("RN")) & "")
                        .Attributes("LCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")) & "")
                        .Attributes("CRS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CRS")) & "")
                        .Attributes("DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE")) & "")
                        .Attributes("CURRENTID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CURRENTID")) & "")
                        .Attributes("OFFICEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OFFICEID")) & "")
                    End With
                    blnRecordFound = True
                Loop

                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                End If
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objOutputXml, "103", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function

        Public Function GetDetails(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '---------Not to be implemented----------
        End Function

        Public Function GetHistory(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function gives details of City.
            'Input  :
            '<MS_GETHISTORYCRSUSE_INPUT>
            '   <LCODE></LCODE>
            '</MS_GETHISTORYCRSUSE_INPUT>

            'Output :
            '<MS_GETHISTORYCRSUSE_OUTPUT>
            '   <HISTORYDETAIL  LCODE='' EMPLOYEENAME='' CHANGEDATA='' DATETIME='' />
            '       <Errors Status=''>
            '		    <Error Code='' Description='' />
            '   	</Errors>
            '</MS_GETHISTORYCRSUSE_OUTPUT>            
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim strOder_ID As String
            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean
            Const strMETHOD_NAME As String = "GetHistory"
            Dim blnRecordFound As Boolean

            Try
                objOutputXml.LoadXml(strHISTORY_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strOder_ID = SearchDoc.DocumentElement.SelectSingleNode("LCODE").InnerText.Trim

                If strOder_ID = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                'Paging Section    
                If SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim() <> "" Then
                        intPageNo = SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim()
                    End If
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim() <> "" Then
                        intPageSize = SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim()
                    End If
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("SORT_BY") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim() <> "" Then
                        strSortBy = SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim()
                    End If
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("DESC") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("DESC").InnerText.Trim().ToUpper = "TRUE" Then
                        blnDesc = True
                    Else
                        blnDesc = False
                    End If
                End If
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_GET_TA_CRSUSEHISTORY"
                    .Connection = objSqlConnection
                    .Parameters.Add("@LCODE", SqlDbType.Int)
                    .Parameters("@LCODE").Value = strOder_ID
                    .Parameters.Add("@PAGE_NO", SqlDbType.Int)
                    If intPageNo = 0 Then
                        .Parameters("@PAGE_NO").Value = DBNull.Value
                    Else
                        .Parameters("@PAGE_NO").Value = intPageNo
                    End If

                    .Parameters.Add("@PAGE_SIZE", SqlDbType.Int)
                    If intPageSize = 0 Then
                        .Parameters("@PAGE_SIZE").Value = DBNull.Value
                    Else
                        .Parameters("@PAGE_SIZE").Value = intPageSize
                    End If

                    .Parameters.Add("@SORT_BY", SqlDbType.VarChar, 100)
                    If strSortBy = "" Then
                        .Parameters("@SORT_BY").Value = DBNull.Value
                    Else
                        .Parameters("@SORT_BY").Value = strSortBy
                    End If

                    .Parameters.Add("@DESC", SqlDbType.Bit)
                    If blnDesc = True Then
                        .Parameters("@DESC").Value = 1
                    Else
                        .Parameters("@DESC").Value = 0
                    End If

                    .Parameters.Add(New SqlParameter("@TOTALROWS", SqlDbType.BigInt))
                    .Parameters("@TOTALROWS").Direction = ParameterDirection.Output
                    .Parameters("@TOTALROWS").Value = 0
                End With

                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("HISTORYDETAIL")

                If objSqlReader.HasRows Then
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.RemoveChild(objAptNode)
                End If

                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("EMPLOYEENAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Employee_Name")) & "")
                    objAptNodeClone.Attributes("DATETIME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DateTime")) & "")
                    objAptNodeClone.Attributes("CHANGEDATA").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHANGEDATA")) & "")

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                    blnRecordFound = True
                Loop
                objSqlReader.Close()
                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").InnerText = Val(objSqlCommand.Parameters("@TOTALROWS").Value)
                    If intPageSize = 0 Then
                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = 0
                    Else
                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = Math.Ceiling(objSqlCommand.Parameters("@TOTALROWS").Value / intPageSize)
                    End If

                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                End If
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objOutputXml, "103", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function

    End Class
End Namespace

