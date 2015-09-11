'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Ashishsrivastava $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizTravelAgency/bzAgency.vb $
'$Workfile: bzAgency.vb $
'$Revision: 166 $
'$Archive: /AAMS/Components/bizTravelAgency/bzAgency.vb $
'$Modtime: 20/09/11 12:35p $

Imports System.Xml
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports AAMS.bizShared
Imports System.Math
Namespace bizTravelAgency
    Public Class bzAgency
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzAgency"
        Const strVIEW_OUTPUT = "<TA_VIEWAGENCY_OUTPUT><Agency LOCATION_CODE='' Chain_Code='' Chain_Name='' IATA_TID='' EMAIL='' FAX='' PHONE='' COUNTRY='' PINCODE='' CITY='' ADDRESS1='' ADDRESS='' NAME='' Aoffice='' CONTACT_PERSON_ID='' CONTACT_PERSON_NAME='' ORDERNUMBER_BACKUP='' ORDERNUMBER_PRIMARY='' INSTALL_DATE_BACKUP='' INSTALL_DATE_PRIMARY='' ONLINE_STATUS_BACKUP='' AGENCYSTATUSID='' AGENCYTYPEID='' LOCATION_SHORT_NAME='' PRIORITYID='' RESP_1A='' RESP_1A_NAME='' WWW_ADDRESS='' ONLINE_STATUS='' DATE_OFFLINE='' DATE_ONLINE='' FILENO='' INCLUDE_IN_CCR='' INCLUDE_IN_CCR_REASON='' OFFICEID='' CUSTOMER_CATEGORY_ID='' IPAddressID='' IPAddress='' Group_Classification_Name='' PANNO='' BRAGENCY='' COMP_VERTICAL=''/> <Errors Status=''><Error Code='' Description='' /></Errors></TA_VIEWAGENCY_OUTPUT>"

        Const strOrderFeasibility_OUTPUT = "<MS_VIEWAGENCYORDERFEASIBILITY_INPUT><AGENCYORDERFEASIBILITY LOCATION_CODE=''  NAME='' ADDRESS='' CITY='' COUNTRY='' ONLINE_STATUS='' IATA_TID=''><PRODUCTIVITYDETAILS MONTH='' PRODUCTIVITY='' /><CRSDETAILS CRS='' AVERAGE='' /><HARDWAREDETAILS PC1A='' AGENCYPC='' HCPRINTER='' TICKETPRINTER='' /><SUMMARY LAST12MONTH1A_AVERAGE='' BEFORE_LAST12MONTH1A_AVERAGE='' TOTALPOTENTIAL='' TOTALREQUIREMENT='' /></AGENCYORDERFEASIBILITY><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWAGENCYORDERFEASIBILITY_INPUT>"
        Const strUPDATE_OUTPUT = "<TA_UPDATEAGENCY_OUTPUT><Agency LOCATION_CODE='' Chain_Code='' IATA_TID='' EMAIL='' FAX='' PHONE='' COUNTRY='' PINCODE='' CITY='' ADDRESS1='' ADDRESS='' NAME='' Aoffice='' CONTACT_PERSON_ID='' ORDERNUMBER_BACKUP='' ORDERNUMBER_PRIMARY='' INSTALL_DATE_BACKUP='' INSTALL_DATE_PRIMARY='' ONLINE_STATUS_BACKUP='' AGENCYSTATUSID='' AGENCYTYPEID='' LOCATION_SHORT_NAME='' PRIORITYID='' RESP_1A='' WWW_ADDRESS='' ONLINE_STATUS='' DATE_OFFLINE='' DATE_ONLINE='' FILENO='' INCLUDE_IN_CCR='' INCLUDE_IN_CCR_REASON='' CUSTOMER_CATEGORY_ID='' EMPLOYEEID='' IPAddress='' /> <Errors Status=''> <Error Code='' Description='' /></Errors></TA_UPDATEAGENCY_OUTPUT>"
        Const strSEARCH_OUTPUT = "<TA_SEARCHAGENCY_OUTPUT><AGNECY LOCATION_CODE='' CHAIN_CODE='' OfficeID='' NAME='' ADDRESS='' ADDRESS1='' CITY='' COUNTRY='' PHONE='' FAX='' ONLINE_STATUS='' Aoffice=''  PINCODE='' Email='' CONTACT_PERSON='' CONTACT_PERSON_ID='' CITYID='' COUNTRYID='' COMP_VERTICAL=''/><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></TA_SEARCHAGENCY_OUTPUT>"

        Const strADDAGENCY_OUTPUT = "<TA_UPDATEAGENCY_INPUT><Agency LOCATION_CODE='' Chain_Code='' IATA_TID='' EMAIL='' FAX='' PHONE='' COUNTRY='' PINCODE='' CITY='' ADDRESS1='' ADDRESS='' NAME='' Aoffice='' CONTACT_PERSON_ID='' ORDERNUMBER_BACKUP='' ORDERNUMBER_PRIMARY='' INSTALL_DATE_BACKUP='' INSTALL_DATE_PRIMARY='' ONLINE_STATUS_BACKUP='' AGENCYSTATUSID='' AGENCYTYPEID='' LOCATION_SHORT_NAME='' PRIORITYID='' RESP_1A='' WWW_ADDRESS='' ONLINE_STATUS='' DATE_OFFLINE='' DATE_ONLINE='' FILENO='' INCLUDE_IN_CCR='' INCLUDE_IN_CCR_REASON='' EMPLOYEEID=''/></TA_UPDATEAGENCY_INPUT>"
        Const strDELETE_OUTPUT = "<TA_DELETEAGENCY_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></TA_DELETEAGENCY_OUTPUT>"
        Const srtGETMISCINSTALLATION_INPUT = "<MS_GETMISCINSTALLATION_INPUT><LCode></LCode></MS_GETMISCINSTALLATION_INPUT>"
        Const srtGETMISCINSTALLATION_OUTPUT = "<MS_GETMISCINSTALLATION_OUTPUT><GETMISCINSTALLATION ROWID='' LCODE='' DATE='' EQUIPMENT_TYPE='' EQUIPMENT_NO='' QTY='' OrderNUmber='' CHALLANDATE='' ChallanNumber='' LoggedBy='' Employee_Name='' LoggedDateTime='' CHALLANSTATUS='' /><TOTAL MISCPC='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_GETMISCINSTALLATION_OUTPUT>"

        Const srtGETPCINSTALLATION_INPUT = "<MS_GETPCINSTALLATION_INPUT><LCODE></LCODE></MS_GETPCINSTALLATION_INPUT>"
        Const srtGETPCINSTALLATION_OUTPUT = "<MS_GETPCINSTALLATION_OUTPUT><PCINSTALLATION ROWID='' LCODE='' DATE='' CPUTYPE='' CPUNO='' MONTYPE='' MONNO='' KBDTYPE='' KBDNO='' MSETYPE='' MSENO='' OrderNumber='' REMARKS='' CHALLANDATE='' CHALLANNUMBER='' LoggedBy='' Employee_Name='' LoggedDateTime='' CHALLANSTATUS='' CDRNO='' LastModifiedDate='' /><TOTAL A1PC='' AGENCYPC='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_GETPCINSTALLATION_OUTPUT>"
        Const srtGETAGENCYGROUPPCINSTALLATION_OUTPUT = "<MS_GETPCINSTALLATION_OUTPUT><PCINSTALLATION ROWID='' LCODE='' AGENCY='' DATE='' CPUTYPE='' CPUNO='' MONTYPE='' MONNO='' KBDTYPE='' KBDNO='' MSETYPE='' MSENO='' OrderNumber='' REMARKS='' CHALLANDATE='' CHALLANNUMBER='' LoggedBy='' Employee_Name='' LoggedDateTime='' CHALLANSTATUS='' CDRNO='' LastModifiedDate='' /><TOTAL A1PC='' AGENCYPC='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_GETPCINSTALLATION_OUTPUT>"

        Const srtGETAGENCYSTAFFDETAILS_INPUT = "<UP_GETAGENCYSTAFFDETAILS_INPUT><LCODE></LCODE></UP_GETAGENCYSTAFFDETAILS_INPUT>"
        Const srtGETAGENCYSTAFFDETAILS_OUTPUT = "<UP_GETAGENCYSTAFFDETAILS_OUTPUT><AGENCYSTAFF LCODE='' AGENCYSTAFFID='' STAFFNAME='' DESIGNATION='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></UP_GETAGENCYSTAFFDETAILS_OUTPUT>"

        Const srtGETAGENCYGROUPSTAFFDETAILS_INPUT = "<UP_GETAGENCYSTAFFDETAILS_INPUT><LCODE></LCODE></UP_GETAGENCYSTAFFDETAILS_INPUT>"  ''BY ASHISH
        Const srtGETAGENCYGROUPSTAFFDETAILS_OUTPUT = "<UP_GETAGENCYSTAFFDETAILS_OUTPUT><AGENCYSTAFF LCODE='' AGENCY=''  AGENCYSTAFFID='' STAFFNAME='' DESIGNATION='' CHAIN_CODE='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></UP_GETAGENCYSTAFFDETAILS_OUTPUT>" ''BY ASHISH




        Const srtGETAGENCYCRSUSEDETAILS_INPUT = "<UP_GETCRSDETAILS_INPUT><LCODE></LCODE></UP_GETCRSDETAILS_INPUT>"

        Const srtGETAGENCYGROUPCOMPETITIONDETAILS_OUTPUT = "<UP_GETCOMPETITIONDETAILS_OUTPUT><COMPETITION_DETAILS LCODE ='' AGENCY='' CRSID=''  DATE_END='' DATE_START='' SOLE_USER='' DIAL_BACKUP='' ONLINESTATUSID='' PRINTER_COUNT='' PC_COUNT='' Action='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></UP_GETCOMPETITIONDETAILS_OUTPUT>"

        Const srtGETAGENCYCRSUSEDETAILS_OUTPUT = "<UP_GETCRSDETAILS_OUTPUT><CRS RN='' LCODE='' DATE='' CURRENTID='' CRS='' OFFICEID='' Action='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></UP_GETCRSDETAILS_OUTPUT>"
        Const srtGETAGENCYGROUPCRSUSEDETAILS_OUTPUT = "<UP_GETCRSDETAILS_OUTPUT><CRS RN='' LCODE='' AGENCY='' DATE='' CURRENTID='' CRS='' OFFICEID='' Action='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></UP_GETCRSDETAILS_OUTPUT>"

        Const srtGETAGENCYCOMPETITIONDETAILS_INPUT = "<UP_GETCOMPETITIONDETAILS_INPUT><LOCATION_CODE></LOCATION_CODE></UP_GETCOMPETITIONDETAILS_INPUT>"

        Const srtGETAGENCYCOMPETITIONDETAILS_OUTPUT = "<UP_GETCOMPETITIONDETAILS_OUTPUT><COMPETITION_DETAILS CRSID='' LOCATION_CODE ='' DATE_END='' DATE_START='' SOLE_USER='' DIAL_BACKUP='' ONLINESTATUSID='' PRINTER_COUNT='' PC_COUNT='' Action='' CommercialDetails='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></UP_GETCOMPETITIONDETAILS_OUTPUT>"

        Const strAGENCYHISTORY_OUTPUT = "<MS_GETAGENCYHISTORY_OUTPUT><AGENCYHISTORY Employee_Name='' DateTime='' ChangeData='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_GETAGENCYHISTORY_OUTPUT>"

        Const strAGENCYCONNECTIVITYHISTORY_OUTPUT = "<MS_GETAGENCYCONNECTIVITYHISTORY_OUTPUT><AGENCYCONNECTIVITYHISTORY LCode='' Online_Status='' ConnectivityType= '' InstallDate='' EndDate ='' ChangeBy='' ChangeDate='' Reason='' IOrderNumber='' DOrderNumber='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_GETAGENCYCONNECTIVITYHISTORY_OUTPUT>"

        Const strGetFileNumber_OUTPUT = "<T_C_AGENCY_FILENO><FILENUMBER FileNo='' /><Errors Status=''><Error Code='' Description='' /></Errors></T_C_AGENCY_FILENO>"
        Const strUPDATEFileno_OUTPUT = "<UP_UPDATEFILENO_OUTPUT><FILEDETAILS FILENO=''/><Errors Status=''><Error Code='' Description='' /></Errors></UP_UPDATEFILENO_OUTPUT>"
        Const strUPDATEFileno_INPUT = "<UP_UPDATEFILENO_INPUT> <FILENO/> <LOCATION_CODE/> </UP_UPDATEFILENO_INPUT>"
        Const strGETFilenoTransfer_OUTPUT = "<TA_GETAGENCYFILENODETAILS_OUTPUT><DETAILS ID='' LCode='' FILENO='' DOCTYPE='' ORDER_NO ='' FILEORDER ='' ORDER_TYPE ='' ContentType='' EmailFrom='' EmailTo=''	EmailSubject='' EmailBody='' PDFDocFileName=''/><Errors Status=''><Error Code='' Description='' /></Errors></TA_GETAGENCYFILENODETAILS_OUTPUT>"
        Const strUPDATEFilenoTransfer_INPUT = "<TA_UPDATEAGENCYFILENODETAILS_INPUT><AGENCYFILE ID='' FILENO=''  ORDERNO ='' DOCTYPE ='' SEQUENCE='' ORDERTYPE='' DOCUMENT = '' ContentType='' EmailFrom=''	EmailTo	=''	EmailSubject ='' EmailBody='' PDFDocFileName=''/></TA_UPDATEAGENCYFILENODETAILS_INPUT>"
        Const strUPDATEFilenoTransfer_OUTPUT = "<TA_UPDATEAGENCYFILENODETAILS_OUTPUT><AGENCYFILE ID='' LCode='' FILENO=''  ORDERNO ='' DOCTYPE ='' SEQUENCE='' ORDERTYPE='' ContentType='' EmailFrom=''	EmailTo	=''	EmailSubject='' EmailBody='' NewPDFDocFileName=''/><Errors Status=''><Error Code='' Description='' /></Errors></TA_UPDATEAGENCYFILENODETAILS_OUTPUT>"

        Const strGETAGENCYAOFFICE_OUTPUT = "<UP_GETAGENCYAOFFICE_OUTPUT><AGENCYGROUPTYPE LOCATION_CODE='' GroupTypeID='' AOffice='' CHAIN_CODE=''/><Errors Status=''><Error Code='' Description='' /></Errors></UP_GETAGENCYAOFFICE_OUTPUT>"

        Const strRP_AGENCY_INPUT = "<RP_AGENCY_INPUT><AGENCY NAME='' LOCATION_SHORT_NAME='' City_Name='' Country_Name='' StatusCode='' Aoffice='' OFFICEID='' Crs='' ADDRESS='' AgencyStatusId='' AgencyTypeId='' EMAIL='' DATE_ONLINE='' DATE_OFFLINE='' FAX='' FILENO='' IATA_TID='' EmployeeID='' Limited_To_Aoffice='' Limited_To_Region='' Limited_To_OwnAagency='' SecurityRegionID='' ></AGENCY></RP_AGENCY_INPUT>"
        'Const strRP_AGENCY_OUTPUT = "<RP_AGENCY_OUTPUT><AGENCY LCODE='' NAME='' PRIMARY_CONECTIVITY='' BACKUP_CONECTIVITY='' FILENUMBER='' OFFICEID='' INSTALLATIONDATE='' PANNO='' PHONE='' STAFFNAME=''/><Errors Status=''><Error Code='' Description='' /></Errors></RP_AGENCY_OUTPUT>"
        Const strRP_AGENCY_OUTPUT = "<RP_AGENCY_OUTPUT><AGENCY LCODE='' CHAIN_CODE='' CHAIN_NAME='' NAME='' ADDRESS='' CITY='' COUNTRY='' OFFICEID='' AOFFICE='' RESP_1A='' PRIMARY_CONECTIVITY='' INSTALLATIONDATE='' EMAIL='' PHONE='' FAX='' AGENCYSTATUS_NAME='' IATA_TID='' WWW_ADDRESS='' AGENCYTYPE='' PANNO='' STAFFNAME='' COMP_VERTICAL=''/><Errors Status=''><Error Code='' Description='' /></Errors></RP_AGENCY_OUTPUT>"

        Const strSendMail_INPUT = "<SENDMAILIMMEDIATE_INPUT><MAIL_DETAILS DESTINATION_TO='' SUBJECT='' MESSAGE='' SOURCE='' DESTINATION_CC='' DESTINATION_BCC=''/></SENDMAILIMMEDIATE_INPUT>"
        Const strAgencySendMail_INPUT = "<TA_SENDMAILAGENCY_INPUT><AGENCYFILE ID='' FILENO='' ORDERNO='' ORDERTYPE='' ContentType='' DOCTYPE='' EmailBody='' EmailFrom='' EmailSubject='' EmailTo=''></AGENCYFILE></TA_SENDMAILAGENCY_INPUT>"
        Const strAgencySendMail_OUTPUT = "<TA_SENDMAILAGENCY_OUTPUT><AGENCYFILE FILENO='' ID='' ORDERNO='' ORDERTYPE='' SEQUENCE='' ContentType='' DOCTYPE='' EmailBody='' EmailFrom='' EmailStatus='' EmailSubject='' EmailTo='' ></AGENCYFILE><Errors Status=''><Error Code='' Description=''></Error></Errors></TA_SENDMAILAGENCY_OUTPUT>"

        Const strAgencyStaffContactPerson_INPUT = "<TA_UPDATEAGENCYSTAFFCONTACTPERSON_INPUT><STAFF Location_code = '' Contact_Person_Id=''/></TA_UPDATEAGENCYSTAFFCONTACTPERSON_INPUT>"
        Const strAgencyStaffContactPerson_OUTPUT = "<TA_UPDATEAGENCYSTAFFCONTACTPERSON_OUTPUT><STAFF Location_code = '' Contact_Person_Id='' /><Errors Status=''><Error Code='' Description='' /></Errors></TA_UPDATEAGENCYSTAFFCONTACTPERSON_OUTPUT>"

        Const strDELETEScanedFile_INPUT = "<TA_DELETEAGENCYFILENODETAILS_INPUT><AGENCYFILE ID='' FILENO=''  ORDERNO ='' DOCTYPE ='' SEQUENCE='' ORDERTYPE='' DOCUMENT ='' ContentType='' EmailFrom=''	EmailTo	='' EmailSubject='' EmailBody='' PDFDocFileName=''/></TA_DELETEAGENCYFILENODETAILS_INPUT>"
        Const strDELETEScanedFile_OUTPUT = "<TA_DELETEAGENCYFILENODETAILS_OUTPUT><AGENCYFILE ID='' FILENO=''  ORDERNO ='' DOCTYPE ='' SEQUENCE='' ORDERTYPE='' DOCUMENT ='' ContentType='' EmailFrom=''	EmailTo	='' EmailSubject='' EmailBody='' PDFDocFileName=''/><Errors Status=''><Error Code='' Description='' /></Errors></TA_DELETEAGENCYFILENODETAILS_OUTPUT>"

        Private Const Key As Integer = 1793

        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add
            'Purpose:This function Add a Agency.
            'Declare Output XML String
            '***********************************************************************
            'Purpose: 
            'Input:None
            'XML
            '<MS_UPDATEAGENCY_INPUT>
            '	<Agency LOCATION_CODE='' Chain_Code='' IATA_TID='' EMAIL='' FAX='' PHONE='' COUNTRY='' PINCODE=''
            '		CITY='' ADDRESS1='' ADDRESS='' NAME='' Aoffice='' CONTACT_PERSON_ID='' ORDERNUMBER_BACKUP=''
            '		ORDERNUMBER_PRIMARY='' INSTALL_DATE_BACKUP='' INSTALL_DATE_PRIMARY='' ONLINE_STATUS_BACKUP=''
            '		AGENCYSTATUSID='' AGENCYTYPEID='' LOCATION_SHORT_NAME='' PRIORITYID='' RESP_1A='' WWW_ADDRESS=''
            '		ONLINE_STATUS='' DATE_OFFLINE='' DATE_ONLINE='' FILENO='' INCLUDE_IN_CCR='' INCLUDE_IN_CCR_REASON='' />
            '</MS_UPDATEAGENCY_INPUT>
            '**************************************************************************
            Dim strMETHOD_NAME As String = "Add"
            Dim objXMLDoc As New XmlDocument
            Try
                objXMLDoc.LoadXml(strADDAGENCY_OUTPUT)
            Catch Exec As Exception
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objXMLDoc, "101", Exec.Message)
                Return objXMLDoc
            End Try
            Return objXMLDoc
        End Function
        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Delete
            '***********************************************************************
            'Purpose:This function deletes a Agency.
            'Input:XmlDocument
            '<MS_DELETEAGENCY_INPUT>
            '	<LOCATION_CODE></LOCATION_CODE>
            '</MS_DELETEAGENCY_INPUT>

            'Output :
            '<MS_DELETEAGENCY_OUTPUT>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_DELETEAGENCY_OUTPUT>
            '************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Delete"
            Dim strLocationCode As String

            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0
                strLocationCode = DeleteDoc.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText.Trim
                If strLocationCode = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_LOCATION_MASTER"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"
                    .Parameters.Add(New SqlParameter("@LOCATION_CODE", SqlDbType.Int))
                    .Parameters("@LOCATION_CODE").Value = strLocationCode
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
            'Purpose:This function gives search results based on choosen search criterion.
            'Input  :- 
            '<MS_SEARCHAGENCY_INPUT>
            '	<NAME></NAME>
            '	<LOCATION_SHORT_NAME></LOCATION_SHORT_NAME>
            '	<City_Name></City_Name>
            '	<Country_Name></Country_Name>
            '	<StatusCode></StatusCode>
            '	<Aoffice></Aoffice>
            '	<OFFICEID></OFFICEID>
            '	<Crs></Crs>
            '	<ADDRESS></ADDRESS>
            '	<AgencyStatusId></AgencyStatusId>
            '	<AgencyTypeId></AgencyTypeId>
            '	<EMAIL></EMAIL>
            '	<DATE_ONLINE></DATE_ONLINE>
            '	<DATE_OFFLINE></DATE_OFFLINE>
            '	<FAX></FAX>
            '	<FILENO></FILENO>
            '	<IATA_TID></IATA_TID>
            '   <IPAddress></IPAddress>
            '	<Limited_To_Aoffice>DEL</Limited_To_Aoffice> <!--Send The Aoffice -->
            '	<Limited_To_Region>123</Limited_To_Region> <!--Send The Security Region ID -->
            '	<Limited_To_OwnAagency>223344</Limited_To_OwnAagency> <!--Send The Location Code -->            '   
            '   <PAGE_NO></PAGE_NO>
            '   <PAGE_SIZE></PAGE_SIZE>
            '   <SORT_BY></SORT_BY>
            '   <DESC></DESC>            
            '   <SearchType></SearchType>
            '   <Online_Status_BackUp></Online_Status_BackUp>
            '   <PRIORITYID></PRIORITYID>
            '   <PHONE></PHONE>
            '   <WWW_ADDRESS></WWW_ADDRESS>
            '   <RESPONSIBILITY_1A></RESPONSIBILITY_1A>
            '   <LOCATION_CODE></LOCATION_CODE>
            '   <CHAIN_CODE></CHAIN_CODE>
            '</MS_SEARCHAGENCY_INPUT>

            'Output:
            '<MS_SEARCHAGENCY_OUTPUT>
            '	<AGNECY LOCATION_CODE='' CHAIN_CODE='' OfficeID='' NAME=''  ADDRESS='' ADDRESS1='' CITY='' COUNTRY='' ONLINE_STATUS='' PINCODE='' Email='' CONTACT_PERSON='' CONTACT_PERSON_ID='' />
            '   <PAGE PAGE_COUNT='' TOTAL_ROWS='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_SEARCHAGENCY_OUTPUT>
            '-----------------------------------------------------------------------------
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean


            Dim strAgencyName As String = vbNullString, strLocationShortName As String = vbNullString, strCity As String = vbNullString, strCountry As String = vbNullString, strOnlineStatus As String = vbNullString, strAoffice As String = vbNullString, strOfficeID As String = vbNullString, strCrs As String = vbNullString, strAddress As String = vbNullString, strEmail As String = vbNullString, strFax As String = vbNullString, strIATAID As String = vbNullString, strLimitedToAoffice As String = vbNullString
            Dim intFileNo, intAgencystatusID, intAgencyTypeID, intLimitedToRegion, intLimitedToOwnAgency As Integer, intRESP_1A As Integer
            Dim intDateOnline_from As Integer, intDateOnline_to As Integer, intDateOffline_from As Integer, intDateOffline_to As Integer
            Dim intDateOnline As Integer, intDateOffline As Integer
            Dim strIPAddress As String

            Dim strSearchType As String
            Dim strOnline_Status_BackUp As String
            Dim intPRIORITYID As Integer
            Dim strPHONE As String
            Dim strWWW_ADDRESS As String
            Dim strRESPONSIBILITY_1A As String 'For Search textbox
            Dim intLOCATION_CODE As UInt64

            Dim strSearchByName, strSearchByValue As String

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean
            Dim strChainCode As String = ""


            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                If SearchDoc.DocumentElement.SelectSingleNode("SearchType") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("SearchType").InnerText.Trim() <> "" Then
                        strSearchType = SearchDoc.DocumentElement.SelectSingleNode("SearchType").InnerText.Trim()
                    End If
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("NAME").InnerText.Trim() <> "" Then
                    strAgencyName = SearchDoc.DocumentElement.SelectSingleNode("NAME").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("LOCATION_SHORT_NAME").InnerText.Trim() <> "" Then
                    strLocationShortName = SearchDoc.DocumentElement.SelectSingleNode("LOCATION_SHORT_NAME").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("OFFICEID").InnerText.Trim() <> "" Then
                    strOfficeID = SearchDoc.DocumentElement.SelectSingleNode("OFFICEID").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("City_Name").InnerText.Trim() <> "" Then
                    strCity = SearchDoc.DocumentElement.SelectSingleNode("City_Name").InnerText.Trim()
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("Country_Name").InnerText.Trim() <> "" Then
                    strCountry = SearchDoc.DocumentElement.SelectSingleNode("Country_Name").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("Aoffice").InnerText.Trim() <> "" Then
                    strAoffice = SearchDoc.DocumentElement.SelectSingleNode("Aoffice").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("Crs").InnerText.Trim() <> "" Then
                    strCrs = SearchDoc.DocumentElement.SelectSingleNode("Crs").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("LOCATION_CODE") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText.Trim() <> "" Then
                        intLOCATION_CODE = SearchDoc.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText.Trim()
                    End If
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("CHAIN_CODE") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText.Trim() <> "" Then
                        strChainCode = SearchDoc.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText.Trim()
                    End If
                End If

                Dim strWholeGroup As String = ""
                If SearchDoc.DocumentElement.SelectSingleNode("WholeGroup") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("WholeGroup").InnerText.Trim().ToUpper = "TRUE" Then
                        strWholeGroup = "TRUE"
                    End If
                End If

                Dim strCOMP_VERTICAL As String = ""
                If SearchDoc.DocumentElement.SelectSingleNode("COMP_VERTICAL") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText.Trim() <> "" Then
                        strCOMP_VERTICAL = SearchDoc.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText.Trim()
                    End If
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("ADDRESS").InnerText.Trim() <> "" Then
                    strAddress = SearchDoc.DocumentElement.SelectSingleNode("ADDRESS").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("StatusCode").InnerText.Trim() <> "" Then
                    strOnlineStatus = SearchDoc.DocumentElement.SelectSingleNode("StatusCode").InnerText.Trim() ''Doubt
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("AgencyStatusId").InnerText.Trim() = "" Then
                    intAgencystatusID = 0
                Else
                    intAgencystatusID = SearchDoc.DocumentElement.SelectSingleNode("AgencyStatusId").InnerText.Trim()
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("AgencyTypeId").InnerText.Trim() = "" Then
                    intAgencyTypeID = 0
                Else
                    intAgencyTypeID = SearchDoc.DocumentElement.SelectSingleNode("AgencyTypeId").InnerText.Trim()
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("EMAIL").InnerText.Trim() <> "" Then
                    strEmail = SearchDoc.DocumentElement.SelectSingleNode("EMAIL").InnerText.Trim()
                End If

                'Done by neeraj  as on date 28-04-2011
                If SearchDoc.DocumentElement.SelectSingleNode("DATE_ONLINE") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("DATE_ONLINE").InnerText.Trim() <> "" Then
                        intDateOnline = SearchDoc.DocumentElement.SelectSingleNode("DATE_ONLINE").InnerText.Trim()
                    End If
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("DATE_OFFLINE") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("DATE_OFFLINE").InnerText.Trim() <> "" Then
                        intDateOffline = SearchDoc.DocumentElement.SelectSingleNode("DATE_OFFLINE").InnerText.Trim()
                    End If
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("DATE_ONLINE_FROM") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("DATE_ONLINE_FROM").InnerText.Trim() <> "" Then
                        intDateOnline_from = SearchDoc.DocumentElement.SelectSingleNode("DATE_ONLINE_FROM").InnerText.Trim()
                    End If
                End If
                

                If SearchDoc.DocumentElement.SelectSingleNode("DATE_ONLINE_TO") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("DATE_ONLINE_TO").InnerText.Trim() <> "" Then
                        intDateOnline_to = SearchDoc.DocumentElement.SelectSingleNode("DATE_ONLINE_TO").InnerText.Trim()
                    End If
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("DATE_OFFLINE_FROM") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("DATE_OFFLINE_FROM").InnerText.Trim() <> "" Then
                        intDateOffline_from = SearchDoc.DocumentElement.SelectSingleNode("DATE_OFFLINE_FROM").InnerText.Trim()
                    End If
                End If
                
                If SearchDoc.DocumentElement.SelectSingleNode("DATE_OFFLINE_TO") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("DATE_OFFLINE_TO").InnerText.Trim() <> "" Then
                        intDateOffline_to = SearchDoc.DocumentElement.SelectSingleNode("DATE_OFFLINE_TO").InnerText.Trim()
                    End If
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("FAX").InnerText.Trim() <> "" Then
                    strFax = SearchDoc.DocumentElement.SelectSingleNode("FAX").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("FILENO").InnerText.Trim() = "" Then
                    intFileNo = 0
                Else
                    intFileNo = SearchDoc.DocumentElement.SelectSingleNode("FILENO").InnerText.Trim()
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("IATA_TID").InnerText.Trim() <> "" Then
                    strIATAID = SearchDoc.DocumentElement.SelectSingleNode("IATA_TID").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("IPAddress") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("IPAddress").InnerText.Trim() <> "" Then
                        strIPAddress = SearchDoc.DocumentElement.SelectSingleNode("IPAddress").InnerText.Trim()
                    End If
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("Online_Status_BackUp") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("Online_Status_BackUp").InnerText.Trim() <> "" Then
                        strOnline_Status_BackUp = SearchDoc.DocumentElement.SelectSingleNode("Online_Status_BackUp").InnerText.Trim()
                    End If
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("PHONE") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("PHONE").InnerText.Trim() <> "" Then
                        strPHONE = SearchDoc.DocumentElement.SelectSingleNode("PHONE").InnerText.Trim()
                    End If
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("WWW_ADDRESS") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("WWW_ADDRESS").InnerText.Trim() <> "" Then
                        strWWW_ADDRESS = SearchDoc.DocumentElement.SelectSingleNode("WWW_ADDRESS").InnerText.Trim()
                    End If
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("PRIORITYID") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("PRIORITYID").InnerText.Trim() <> "" Then
                        intPRIORITYID = SearchDoc.DocumentElement.SelectSingleNode("PRIORITYID").InnerText.Trim()
                    End If
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim() <> "" Then
                    intRESP_1A = SearchDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("RESPONSIBILITY_1A") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("RESPONSIBILITY_1A").InnerText.Trim() <> "" Then
                        strRESPONSIBILITY_1A = SearchDoc.DocumentElement.SelectSingleNode("RESPONSIBILITY_1A").InnerText.Trim()
                    End If
                End If

                Dim blnBrAgency As Boolean
                If SearchDoc.DocumentElement.SelectSingleNode("AgencyUsingBirdres") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("AgencyUsingBirdres").InnerText.Trim().ToUpper = "TRUE" Then
                        blnBrAgency = True
                    Else
                        blnBrAgency = False
                    End If
                End If

                Dim strOrderNo As String = ""
                If SearchDoc.DocumentElement.SelectSingleNode("ORDERNO") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("ORDERNO").InnerText.Trim() <> "" Then
                        strOrderNo = SearchDoc.DocumentElement.SelectSingleNode("ORDERNO").InnerText.Trim()
                    End If
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText.Trim() <> "" Then
                    strLimitedToAoffice = SearchDoc.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText.Trim()
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.Trim() = "" Then
                    intLimitedToRegion = 0
                Else
                    intLimitedToRegion = SearchDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.Trim()
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText.Trim() = "" Then
                    intLimitedToOwnAgency = 0
                Else
                    intLimitedToOwnAgency = SearchDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim() <> "" Then
                    intPageNo = SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim()
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim() <> "" Then
                    intPageSize = SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim() <> "" Then
                    strSortBy = SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("DESC").InnerText.Trim().ToUpper = "TRUE" Then
                    blnDesc = True
                Else
                    blnDesc = False
                End If


                Dim strBR_HD As String = ""
                If SearchDoc.DocumentElement.SelectSingleNode("BR_HD") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("BR_HD").InnerText.Trim().ToUpper = "TRUE" Then
                        strBR_HD = "TRUE"
                    End If
                End If
                '--------Retrieving & Checking Details from Input XMLDocument ------End
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    If strBR_HD = "TRUE" Then
                        .CommandText = "[UP_SER_BR_TA_LOCATION_MASTER]"
                    Else
                        .CommandText = "[UP_SER_TA_LOCATION_MASTER]"
                    End If

                    .Parameters.Add("@SEARCHTYPE", SqlDbType.VarChar, 20)
                    If strSearchType = "" Then
                        .Parameters("@SEARCHTYPE").Value = DBNull.Value
                    Else
                        .Parameters("@SEARCHTYPE").Value = strSearchType
                    End If

                    .Connection = objSqlConnection
                    .Parameters.Add("@NAME", SqlDbType.VarChar, 200)
                    .Parameters("@NAME").Value = strAgencyName

                    .Parameters.Add("@LOCATION_SHORT_NAME", SqlDbType.VarChar, 50)
                    .Parameters("@LOCATION_SHORT_NAME").Value = strLocationShortName

                    .Parameters.Add("@OFFICEID", SqlDbType.VarChar, 10)
                    .Parameters("@OFFICEID").Value = strOfficeID

                    .Parameters.Add("@CITY", SqlDbType.VarChar, 80)
                    .Parameters("@CITY").Value = strCity

                    .Parameters.Add("@COUNTRY", SqlDbType.VarChar, 50)
                    .Parameters("@COUNTRY").Value = strCountry

                    .Parameters.Add("@AOFFICE", SqlDbType.VarChar, 3)
                    .Parameters("@AOFFICE").Value = strAoffice

                    .Parameters.Add("@CRS", SqlDbType.VarChar, 5)
                    .Parameters("@CRS").Value = strCrs

                    .Parameters.Add("@LOCATION_CODE", SqlDbType.BigInt)
                    If intLOCATION_CODE = 0 Then
                        .Parameters("@LOCATION_CODE").Value = DBNull.Value
                    Else
                        .Parameters("@LOCATION_CODE").Value = intLOCATION_CODE
                    End If

                    .Parameters.Add("@Chain_Code", SqlDbType.Int)
                    If strChainCode = "" Then
                        .Parameters("@Chain_Code").Value = DBNull.Value
                    Else
                        .Parameters("@Chain_Code").Value = strChainCode
                    End If

                    .Parameters.Add("@WHOLEGROUP", SqlDbType.Bit)
                    If strWholeGroup = "TRUE" Then
                        .Parameters("@WHOLEGROUP").Value = 1
                    Else
                        .Parameters("@WHOLEGROUP").Value = DBNull.Value
                    End If


                    .Parameters.Add("@COMP_VERTICAL", SqlDbType.SmallInt)
                    If strCOMP_VERTICAL = "" Then
                        .Parameters("@COMP_VERTICAL").Value = DBNull.Value
                    Else
                        .Parameters("@COMP_VERTICAL").Value = Val(strCOMP_VERTICAL)
                    End If

                    .Parameters.Add("@ADDRESS", SqlDbType.VarChar, 200)
                    .Parameters("@ADDRESS").Value = strAddress

                    .Parameters.Add("@ONLINE_STATUS", SqlDbType.VarChar, 20)
                    .Parameters("@ONLINE_STATUS").Value = strOnlineStatus


                    .Parameters.Add("@AGENCYSTATUSID", SqlDbType.Int)
                    If intAgencystatusID = 0 Then
                        .Parameters("@AGENCYSTATUSID").Value = DBNull.Value
                    Else
                        .Parameters("@AGENCYSTATUSID").Value = intAgencystatusID
                    End If

                    .Parameters.Add("@AGENCYTYPEID", SqlDbType.Int)
                    If intAgencyTypeID = 0 Then
                        .Parameters("@AGENCYTYPEID").Value = DBNull.Value
                    Else
                        .Parameters("@AGENCYTYPEID").Value = intAgencyTypeID
                    End If

                    .Parameters.Add("@EMAIL", SqlDbType.VarChar, 80)
                    .Parameters("@EMAIL").Value = strEmail

                    .Parameters.Add("@DATE_ONLINE", SqlDbType.Int)
                    If intDateOnline = 0 Then
                        .Parameters("@DATE_ONLINE").Value = DBNull.Value
                    Else
                        .Parameters("@DATE_ONLINE").Value = intDateOnline
                    End If

                    .Parameters.Add("@DATE_OFFLINE", SqlDbType.Int)
                    If intDateOffline = 0 Then
                        .Parameters("@DATE_OFFLINE").Value = DBNull.Value
                    Else
                        .Parameters("@DATE_OFFLINE").Value = intDateOffline
                    End If

                    .Parameters.Add("@DATE_ONLINE_FROM", SqlDbType.Int)
                    If intDateOnline_from = 0 Then
                        .Parameters("@DATE_ONLINE_FROM").Value = DBNull.Value
                    Else
                        .Parameters("@DATE_ONLINE_FROM").Value = intDateOnline_from
                    End If

                    .Parameters.Add("@DATE_ONLINE_TO", SqlDbType.Int)
                    If intDateOnline_to = 0 Then
                        .Parameters("@DATE_ONLINE_TO").Value = DBNull.Value
                    Else
                        .Parameters("@DATE_ONLINE_TO").Value = intDateOnline_to
                    End If

                    .Parameters.Add("@DATE_OFFLINE_FROM", SqlDbType.Int)
                    If intDateOffline_from = 0 Then
                        .Parameters("@DATE_OFFLINE_FROM").Value = DBNull.Value
                    Else
                        .Parameters("@DATE_OFFLINE_FROM").Value = intDateOffline_from
                    End If

                    .Parameters.Add("@DATE_OFFLINE_TO", SqlDbType.Int)
                    If intDateOffline_to = 0 Then
                        .Parameters("@DATE_OFFLINE_TO").Value = DBNull.Value
                    Else
                        .Parameters("@DATE_OFFLINE_TO").Value = intDateOffline_to
                    End If

                    .Parameters.Add("@FAX", SqlDbType.VarChar, 20)
                    .Parameters("@FAX").Value = strFax

                    .Parameters.Add("@FILENO", SqlDbType.Int)
                    If intFileNo = 0 Then
                        .Parameters("@FILENO").Value = DBNull.Value
                    Else
                        .Parameters("@FILENO").Value = intFileNo
                    End If

                    .Parameters.Add("@IATA_TID", SqlDbType.VarChar, 20)
                    .Parameters("@IATA_TID").Value = strIATAID

                    .Parameters.Add("@IPADDRESS", SqlDbType.VarChar, 15)
                    If strIPAddress = "" Then
                        .Parameters("@IPADDRESS").Value = DBNull.Value
                    Else
                        .Parameters("@IPADDRESS").Value = strIPAddress
                    End If

                    .Parameters.Add("@ONLINE_STATUS_BACKUP", SqlDbType.VarChar, 20)
                    If strOnline_Status_BackUp = "" Then
                        .Parameters("@ONLINE_STATUS_BACKUP").Value = DBNull.Value
                    Else
                        .Parameters("@ONLINE_STATUS_BACKUP").Value = strOnline_Status_BackUp
                    End If

                    .Parameters.Add("@PHONE", SqlDbType.VarChar, 50)
                    If strPHONE = "" Then
                        .Parameters("@PHONE").Value = DBNull.Value
                    Else
                        .Parameters("@PHONE").Value = strPHONE
                    End If

                    .Parameters.Add("@WWW_ADDRESS", SqlDbType.VarChar, 50)
                    If strWWW_ADDRESS = "" Then
                        .Parameters("@WWW_ADDRESS").Value = DBNull.Value
                    Else
                        .Parameters("@WWW_ADDRESS").Value = strWWW_ADDRESS
                    End If

                    .Parameters.Add("@PRIORITYID", SqlDbType.Int)
                    If intPRIORITYID = 0 Then
                        .Parameters("@PRIORITYID").Value = DBNull.Value
                    Else
                        .Parameters("@PRIORITYID").Value = intPRIORITYID
                    End If

                    .Parameters.Add("@RESPONSIBILITY_1A", SqlDbType.VarChar, 50)
                    If strRESPONSIBILITY_1A = "" Then
                        .Parameters("@RESPONSIBILITY_1A").Value = DBNull.Value
                    Else
                        .Parameters("@RESPONSIBILITY_1A").Value = strRESPONSIBILITY_1A
                    End If

                    .Parameters.Add("@RESP_1A", SqlDbType.Int)
                    If intRESP_1A = 0 Then
                        .Parameters("@RESP_1A").Value = DBNull.Value
                    Else
                        .Parameters("@RESP_1A").Value = intRESP_1A
                    End If

                    .Parameters.Add("@BRAGENCY", SqlDbType.Bit)
                    If blnBrAgency = True Then
                        .Parameters("@BRAGENCY").Value = 1
                    Else
                        .Parameters("@BRAGENCY").Value = 0
                    End If

                    .Parameters.Add("@ORDERNO", SqlDbType.VarChar, 20)
                    If strOrderNo = "" Then
                        .Parameters("@ORDERNO").Value = DBNull.Value
                    Else
                        .Parameters("@ORDERNO").Value = strOrderNo
                    End If


                    .Parameters.Add("@LIMITED_TO_AOFFICE", SqlDbType.Char, 3)
                    .Parameters("@LIMITED_TO_AOFFICE").Value = strLimitedToAoffice

                    .Parameters.Add("@LIMITED_TO_REGION", SqlDbType.Int)
                    If intLimitedToRegion = 0 Then
                        .Parameters("@LIMITED_TO_REGION").Value = DBNull.Value
                    Else
                        .Parameters("@LIMITED_TO_REGION").Value = intLimitedToRegion
                    End If

                    .Parameters.Add("@LIMITED_TO_OWNAAGENCY", SqlDbType.Int) 'Location_code
                    If intLimitedToOwnAgency = 0 Then
                        .Parameters("@LIMITED_TO_OWNAAGENCY").Value = DBNull.Value
                    Else
                        .Parameters("@LIMITED_TO_OWNAAGENCY").Value = intLimitedToOwnAgency
                    End If


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

                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AGNECY")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("LOCATION_CODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOCATION_CODE")))
                    objAptNodeClone.Attributes("CHAIN_CODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHAIN_CODE")))
                    objAptNodeClone.Attributes("OfficeID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OfficeID")) & "")
                    objAptNodeClone.Attributes("NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NAME")))
                    objAptNodeClone.Attributes("ADDRESS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ADDRESS")) & "")
                    objAptNodeClone.Attributes("ADDRESS1").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ADDRESS1")) & "")
                    objAptNodeClone.Attributes("CITY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITY")) & "")
                    objAptNodeClone.Attributes("COUNTRY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("COUNTRY")) & "")
                    objAptNodeClone.Attributes("ONLINE_STATUS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ONLINE_STATUS")) & "")
                    objAptNodeClone.Attributes("PHONE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PHONE")) & "")
                    objAptNodeClone.Attributes("FAX").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FAX")) & "")
                    objAptNodeClone.Attributes("Aoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Aoffice")) & "")

                    objAptNodeClone.Attributes("PINCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PINCODE")) & "")
                    objAptNodeClone.Attributes("Email").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Email")) & "")

                    objAptNodeClone.Attributes("CONTACT_PERSON").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CONTACT_PERSON")) & "")
                    objAptNodeClone.Attributes("CONTACT_PERSON_ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CONTACT_PERSON_ID")) & "")

                    objAptNodeClone.Attributes("CITYID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITYID")) & "")
                    objAptNodeClone.Attributes("COUNTRYID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("COUNTRYID")) & "")


                    strCOMP_VERTICAL = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("COMP_VERTICAL")) & "")
                    objAptNodeClone.Attributes("COMP_VERTICAL").InnerText = IIf(strCOMP_VERTICAL = "1", "Amadeus", IIf(strCOMP_VERTICAL = "2", "ResBird", (IIf(strCOMP_VERTICAL = "3", "Non1A", ""))))

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop
                objSqlReader.Close()
                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").InnerText = objSqlCommand.Parameters("@TOTALROWS").Value
                    If intPageSize = 0 Then
                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = 0
                    Else
                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = Ceiling(objSqlCommand.Parameters("@TOTALROWS").Value / intPageSize)
                    End If
                End If
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                'msgbox(Exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function
        Public Function OfficeID_AgencySearch(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            'Purpose:This function gives search results based on choosen search criterion.
            'Input  :- 
            '<TA_SEARCHAGENCY_INPUT>
            '	<OFFICEID>AMDWI2119</OFFICEID>
            '   <ResponsibleStaffID></ResponsibleStaffID>
            '</TA_SEARCHAGENCY_INPUT>

            'Output:
            '<MS_SEARCHAGENCY_OUTPUT>
            '	<AGNECY LOCATION_CODE='' CHAIN_CODE='' OfficeID='' NAME=''  ADDRESS='' ADDRESS1='' CITY='' COUNTRY='' ONLINE_STATUS='' PINCODE='' Email='' CONTACT_PERSON='' CONTACT_PERSON_ID='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_SEARCHAGENCY_OUTPUT>
            '-----------------------------------------------------------------------------
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean

            Dim strOfficeID As String = vbNullString
            Dim intFileNo, intAgencystatusID, intAgencyTypeID, intLimitedToRegion, intLimitedToOwnAgency As Integer, intRESP_1A As Integer
            Dim intDateOnline As Integer, intDateOffline As Integer
            Dim strIPAddress As String

            Dim strSearchType As String
            Dim strOnline_Status_BackUp As String
            Dim intPRIORITYID As Integer
            Dim strPHONE As String
            Dim strWWW_ADDRESS As String
            Dim strRESPONSIBILITY_1A As String 'For Search textbox
            Dim intLOCATION_CODE As UInt64

            Dim strSearchByName, strSearchByValue As String

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean


            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start

                If SearchDoc.DocumentElement.SelectSingleNode("OFFICEID").InnerText.Trim() <> "" Then
                    strOfficeID = SearchDoc.DocumentElement.SelectSingleNode("OFFICEID").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("ResponsibleStaffID") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("ResponsibleStaffID").InnerText.Trim() <> "" Then
                        strRESPONSIBILITY_1A = SearchDoc.DocumentElement.SelectSingleNode("ResponsibleStaffID").InnerText.Trim()
                    End If
                End If

                Dim strBR_HD As String = ""
                If SearchDoc.DocumentElement.SelectSingleNode("BR_HD") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("BR_HD").InnerText.Trim().ToUpper = "TRUE" Then
                        strBR_HD = "TRUE"
                    End If
                End If

                If strOfficeID = "" Then
                    Throw (New AAMSException("OfficeID can't be blank."))
                End If

                '--------Retrieving & Checking Details from Input XMLDocument ------End
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    If strBR_HD = "TRUE" Then
                        .CommandText = "[UP_SER_BR_TA_LOCATION_MASTER_ON_OFFICEID]"
                    Else
                        .CommandText = "[UP_SER_TA_LOCATION_MASTER_ON_OFFICEID]"
                    End If

                    .Connection = objSqlConnection
                    .Parameters.Add("@OFFICEID", SqlDbType.VarChar, 10)
                    .Parameters("@OFFICEID").Value = strOfficeID

                    .Parameters.Add("@ResponsibleStaffID", SqlDbType.Int)

                    If strRESPONSIBILITY_1A <> "" Then
                        .Parameters("@ResponsibleStaffID").Value = Val(strRESPONSIBILITY_1A)
                    End If



                End With

                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AGNECY")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("LOCATION_CODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOCATION_CODE")))
                    objAptNodeClone.Attributes("CHAIN_CODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHAIN_CODE")))
                    objAptNodeClone.Attributes("OfficeID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OfficeID")) & "")
                    objAptNodeClone.Attributes("NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NAME")))
                    objAptNodeClone.Attributes("ADDRESS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ADDRESS")) & "")
                    objAptNodeClone.Attributes("ADDRESS1").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ADDRESS1")) & "")
                    objAptNodeClone.Attributes("CITY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITY")) & "")
                    objAptNodeClone.Attributes("COUNTRY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("COUNTRY")) & "")
                    objAptNodeClone.Attributes("ONLINE_STATUS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ONLINE_STATUS")) & "")
                    objAptNodeClone.Attributes("PHONE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PHONE")) & "")
                    objAptNodeClone.Attributes("FAX").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FAX")) & "")
                    objAptNodeClone.Attributes("Aoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Aoffice")) & "")

                    objAptNodeClone.Attributes("PINCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PINCODE")) & "")
                    objAptNodeClone.Attributes("Email").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Email")) & "")

                    objAptNodeClone.Attributes("CONTACT_PERSON").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CONTACT_PERSON")) & "")
                    objAptNodeClone.Attributes("CONTACT_PERSON_ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CONTACT_PERSON_ID")) & "")

                    objAptNodeClone.Attributes("CITYID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITYID")) & "")
                    objAptNodeClone.Attributes("COUNTRYID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("COUNTRYID")) & "")

                    Dim strCOMP_VERTICAL As String = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("COMP_VERTICAL")) & "")
                    objAptNodeClone.Attributes("COMP_VERTICAL").InnerText = IIf(strCOMP_VERTICAL = "1", "Amadeus", IIf(strCOMP_VERTICAL = "2", "ResBird", (IIf(strCOMP_VERTICAL = "3", "Non1A", ""))))


                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop
                objSqlReader.Close()
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
                'msgbox(Exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function
        Public Function Update(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Update
            'Purpose:This function Inserts/Updates a Travel Agency.
            'Input  :
            '<MS_UPDATEAGENCY_INPUT>
            '	<Agency LOCATION_CODE='' Chain_Code='' IATA_TID='' EMAIL='' FAX='' PHONE='' COUNTRY='' PINCODE=''
            '		CITY='' ADDRESS1='' ADDRESS='' NAME='' Aoffice='' CONTACT_PERSON_ID='' ORDERNUMBER_BACKUP=''
            '		ORDERNUMBER_PRIMARY='' INSTALL_DATE_BACKUP='' INSTALL_DATE_PRIMARY='' ONLINE_STATUS_BACKUP=''
            '		AGENCYSTATUSID='' AGENCYTYPEID='' LOCATION_SHORT_NAME='' PRIORITYID='' RESP_1A='' WWW_ADDRESS=''
            '		ONLINE_STATUS='' DATE_OFFLINE='' DATE_ONLINE='' FILENO='' INCLUDE_IN_CCR='' INCLUDE_IN_CCR_REASON='' PINCODE='' IPAddressID='' IPAddress='' />
            '</MS_UPDATEAGENCY_INPUT>

            'Output  :
            '<MS_UPDATEAGENCY_OUTPUT>
            '	<Agency LOCATION_CODE='' Chain_Code='' IATA_TID='' EMAIL='' FAX='' PHONE='' COUNTRY='' PINCODE=''
            '		CITY='' ADDRESS1='' ADDRESS='' NAME='' Aoffice='' CONTACT_PERSON_ID='' ORDERNUMBER_BACKUP=''
            '		ORDERNUMBER_PRIMARY='' INSTALL_DATE_BACKUP='' INSTALL_DATE_PRIMARY='' ONLINE_STATUS_BACKUP=''
            '		AGENCYSTATUSID='' AGENCYTYPEID='' LOCATION_SHORT_NAME='' PRIORITYID='' RESP_1A='' WWW_ADDRESS=''
            '		ONLINE_STATUS='' DATE_OFFLINE='' DATE_ONLINE='' FILENO='' INCLUDE_IN_CCR='' INCLUDE_IN_CCR_REASON='' PINCODE='' IPAddress=''  />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_UPDATEAGENCY_OUTPUT>


            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objUpdateDocOutput As New XmlDocument

            'Agency details
            Dim intChainCode As Integer, intLocationCode As Integer, strAgencyName As String = vbNullString, strAddress As String = vbNullString, strAddress1 As String = vbNullString
            Dim strCity As String = vbNullString, strCountry As String = vbNullString, strEmail As String = vbNullString, strPhone As String = vbNullString
            Dim strFax As String = vbNullString, strPIN As String = vbNullString, strLocationShortName As String = vbNullString
            Dim intAgencystatusID As Integer, intCONTACTPERSONID As Integer
            Dim strIATAID As String = vbNullString, strWebsite As String = vbNullString
            Dim strChangeData As String = vbNullString
            Dim intCUSTOMER_CATEGORY_ID As Integer
            'Amadeus specific
            Dim strAoffice As String = vbNullString, int1AResponsiblity As Integer, intAgencyTypeID As Integer, intPriorityID As Integer
            Dim blnIncludeCCRoster As Boolean, intFileNo As Integer, strReason As String
            Dim intDateOnline As Integer, intDateOffline As Integer, intInstallDatePrimary As Integer, intInstallDateBackup As Integer

            ''Connectivity
            Dim strOnlineSatus As String = vbNullString, strOnlineStatusBackup As String = vbNullString, strOrdernumberPrimary As String = vbNullString
            Dim strOrdernumber_Backup As String = vbNullString
            Dim intEmployeeID As Integer
            Dim strCngConnectivityData As String = vbNullString
            Dim strConAttributes As String = vbNullString
            Dim strIPAddressID As String
            Dim strIPAddress As String
            Dim intChainCode_old As Integer

            Dim blnMaintainHistory As Boolean
            strConAttributes = ""
            strChangeData = ""
            Dim strACCOUNTS_MGR_OLD As String = "", strACCOUNTS_MGR_OLD_EMAIL As String = "", strACCOUNTS_MGR_NEW As String = "", strACCOUNTS_MGR_NEW_EMAIL As String = "", strCHAIN_CODE_OLD As String = ""
            Dim boolSendMailStatus As Boolean
            Dim objInputSendMailXml As New XmlDocument
            Dim strEmailBody As String = "", strEmailBodyNew = ""
            Dim strSource As String = ""
            Dim strPANNO As String = ""
            Dim blnBRAgency As Boolean
            Dim blnSendMailAddressChange As Boolean = False
            ''Transaction
            Dim objTran As SqlTransaction
            Dim intRecordsAffected As Int32

            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)
                With objUpdateDocOutput.DocumentElement.SelectSingleNode("Agency")
                    .Attributes("LOCATION_CODE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("LOCATION_CODE").InnerText
                    .Attributes("Chain_Code").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("Chain_Code").InnerText
                    .Attributes("IATA_TID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("IATA_TID").InnerText
                    .Attributes("EMAIL").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("EMAIL").InnerText
                    .Attributes("FAX").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("FAX").InnerText
                    .Attributes("PHONE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("PHONE").InnerText
                    .Attributes("COUNTRY").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("COUNTRY").InnerText
                    .Attributes("CITY").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("CITY").InnerText
                    .Attributes("ADDRESS1").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("ADDRESS1").InnerText
                    .Attributes("ADDRESS").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("ADDRESS").InnerText
                    .Attributes("NAME").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("NAME").InnerText
                    .Attributes("Aoffice").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("Aoffice").InnerText
                    .Attributes("CONTACT_PERSON_ID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("CONTACT_PERSON_ID").InnerText
                    .Attributes("ORDERNUMBER_BACKUP").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("ORDERNUMBER_BACKUP").InnerText
                    .Attributes("ORDERNUMBER_PRIMARY").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("ORDERNUMBER_PRIMARY").InnerText
                    .Attributes("INSTALL_DATE_BACKUP").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("INSTALL_DATE_BACKUP").InnerText
                    .Attributes("INSTALL_DATE_PRIMARY").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("INSTALL_DATE_PRIMARY").InnerText
                    .Attributes("ONLINE_STATUS_BACKUP").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("ONLINE_STATUS_BACKUP").InnerText
                    .Attributes("AGENCYSTATUSID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("AGENCYSTATUSID").InnerText
                    .Attributes("AGENCYTYPEID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("AGENCYTYPEID").InnerText
                    .Attributes("LOCATION_SHORT_NAME").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("LOCATION_SHORT_NAME").InnerText
                    .Attributes("PRIORITYID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("PRIORITYID").InnerText
                    .Attributes("RESP_1A").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("RESP_1A").InnerText
                    .Attributes("WWW_ADDRESS").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("WWW_ADDRESS").InnerText
                    .Attributes("ONLINE_STATUS").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("ONLINE_STATUS").InnerText
                    .Attributes("DATE_OFFLINE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("DATE_OFFLINE").InnerText
                    .Attributes("DATE_ONLINE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("DATE_ONLINE").InnerText
                    .Attributes("FILENO").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("FILENO").InnerText
                    .Attributes("INCLUDE_IN_CCR").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("INCLUDE_IN_CCR").InnerText
                    .Attributes("INCLUDE_IN_CCR_REASON").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("INCLUDE_IN_CCR_REASON").InnerText
                    .Attributes("PINCODE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("PINCODE").InnerText
                    .Attributes("CUSTOMER_CATEGORY_ID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("CUSTOMER_CATEGORY_ID").InnerText
                    .Attributes("EMPLOYEEID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("EMPLOYEEID").InnerText
                    .Attributes("IPAddress").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("IPAddress").InnerText

                End With

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("Agency")
                    'Agency Details
                    If ((.Attributes("LOCATION_CODE").InnerText).Trim = "") Then
                        intLocationCode = 0
                        strAction = "I"
                    Else
                        intLocationCode = ((.Attributes("LOCATION_CODE").InnerText).Trim)
                        strAction = "U"
                    End If
                    If ((.Attributes("Chain_Code").InnerText).Trim = "") Then
                        intChainCode = 0
                    Else
                        intChainCode = ((.Attributes("Chain_Code").InnerText).Trim)
                    End If

                    If ((.Attributes("IATA_TID").InnerText).Trim = "") Then
                        strIATAID = vbNullString
                    Else
                        strIATAID = ((.Attributes("IATA_TID").InnerText).Trim)
                    End If
                    If ((.Attributes("EMAIL").InnerText).Trim = "") Then
                        strEmail = vbNullString
                    Else
                        strEmail = ((.Attributes("EMAIL").InnerText).Trim)
                    End If
                    If ((.Attributes("FAX").InnerText).Trim = "") Then
                        strFax = vbNullString
                    Else
                        strFax = ((.Attributes("FAX").InnerText).Trim)
                    End If

                    If ((.Attributes("PHONE").InnerText).Trim = "") Then
                        strPhone = vbNullString
                    Else
                        strPhone = ((.Attributes("PHONE").InnerText).Trim)
                    End If

                    If ((.Attributes("COUNTRY").InnerText).Trim = "") Then
                        strCountry = vbNullString
                    Else
                        strCountry = ((.Attributes("COUNTRY").InnerText).Trim)
                    End If
                    If ((.Attributes("CITY").InnerText).Trim = "") Then
                        strCity = vbNullString
                    Else
                        strCity = ((.Attributes("CITY").InnerText).Trim)
                    End If
                    If ((.Attributes("ADDRESS1").InnerText).Trim = "") Then
                        strAddress1 = vbNullString
                    Else
                        strAddress1 = ((.Attributes("ADDRESS1").InnerText).Trim)
                    End If
                    If ((.Attributes("ADDRESS").InnerText).Trim = "") Then
                        strAddress = vbNullString
                    Else
                        strAddress = ((.Attributes("ADDRESS").InnerText).Trim)
                    End If

                    If ((.Attributes("NAME").InnerText).Trim = "") Then
                        strAgencyName = vbNullString
                    Else
                        strAgencyName = ((.Attributes("NAME").InnerText).Trim)
                    End If
                    If ((.Attributes("Aoffice").InnerText).Trim = "") Then
                        strAoffice = vbNullString
                    Else
                        strAoffice = ((.Attributes("Aoffice").InnerText).Trim)
                    End If

                    If ((.Attributes("CONTACT_PERSON_ID").InnerText).Trim = "") Then
                        intCONTACTPERSONID = 0
                    Else
                        intCONTACTPERSONID = ((.Attributes("CONTACT_PERSON_ID").InnerText).Trim)
                    End If

                    If ((.Attributes("PINCODE").InnerText).Trim = "") Then
                        strPIN = vbNullString
                    Else
                        strPIN = ((.Attributes("PINCODE").InnerText).Trim)
                    End If
                    If ((.Attributes("ORDERNUMBER_BACKUP").InnerText).Trim = "") Then
                        strOrdernumber_Backup = vbNullString
                    Else
                        strOrdernumber_Backup = ((.Attributes("ORDERNUMBER_BACKUP").InnerText).Trim)
                    End If

                    If ((.Attributes("ORDERNUMBER_PRIMARY").InnerText).Trim = "") Then
                        strOrdernumberPrimary = vbNullString
                    Else
                        strOrdernumberPrimary = ((.Attributes("ORDERNUMBER_PRIMARY").InnerText).Trim)
                    End If

                    If ((.Attributes("INSTALL_DATE_BACKUP").InnerText).Trim = "") Then
                        intInstallDateBackup = 0
                    Else
                        intInstallDateBackup = ((.Attributes("INSTALL_DATE_BACKUP").InnerText).Trim)
                    End If

                    If ((.Attributes("INSTALL_DATE_PRIMARY").InnerText).Trim = "") Then
                        intInstallDatePrimary = 0
                    Else
                        intInstallDatePrimary = ((.Attributes("INSTALL_DATE_PRIMARY").InnerText).Trim)
                    End If

                    If ((.Attributes("ONLINE_STATUS_BACKUP").InnerText).Trim = "") Then
                        strOnlineStatusBackup = vbNullString
                    Else
                        strOnlineStatusBackup = ((.Attributes("ONLINE_STATUS_BACKUP").InnerText).Trim)
                    End If
                    If ((.Attributes("AGENCYSTATUSID").InnerText).Trim = "") Then
                        intAgencystatusID = 0
                    Else
                        intAgencystatusID = ((.Attributes("AGENCYSTATUSID").InnerText).Trim)
                    End If

                    If ((.Attributes("AGENCYTYPEID").InnerText).Trim = "") Then
                        intAgencyTypeID = 0
                    Else
                        intAgencyTypeID = ((.Attributes("AGENCYTYPEID").InnerText).Trim)
                    End If

                    If ((.Attributes("LOCATION_SHORT_NAME").InnerText).Trim = "") Then
                        strLocationShortName = vbNullString
                    Else
                        strLocationShortName = ((.Attributes("LOCATION_SHORT_NAME").InnerText).Trim)
                    End If
                    If ((.Attributes("PRIORITYID").InnerText).Trim = "") Then
                        intPriorityID = 0
                    Else
                        intPriorityID = ((.Attributes("PRIORITYID").InnerText).Trim)
                    End If
                    If ((.Attributes("RESP_1A").InnerText).Trim = "") Then
                        int1AResponsiblity = 0
                    Else
                        int1AResponsiblity = ((.Attributes("RESP_1A").InnerText).Trim)
                    End If
                    If ((.Attributes("WWW_ADDRESS").InnerText).Trim = "") Then
                        strWebsite = vbNullString
                    Else
                        strWebsite = ((.Attributes("WWW_ADDRESS").InnerText).Trim)
                    End If

                    If ((.Attributes("ONLINE_STATUS").InnerText).Trim = "") Then
                        strOnlineSatus = vbNullString
                    Else
                        strOnlineSatus = ((.Attributes("ONLINE_STATUS").InnerText).Trim)
                    End If
                    If ((.Attributes("DATE_OFFLINE").InnerText).Trim = "") Then
                        intDateOffline = 0
                    Else
                        intDateOffline = ((.Attributes("DATE_OFFLINE").InnerText).Trim)
                    End If
                    If ((.Attributes("DATE_ONLINE").InnerText).Trim = "") Then
                        intDateOnline = 0
                    Else
                        intDateOnline = ((.Attributes("DATE_ONLINE").InnerText).Trim)
                    End If
                    If ((.Attributes("FILENO").InnerText).Trim = "") Then
                        intFileNo = 0
                    Else
                        intFileNo = ((.Attributes("FILENO").InnerText).Trim)
                    End If
                    If ((.Attributes("INCLUDE_IN_CCR").InnerText).Trim = "True") Then
                        'blnIncludeCCRoster = ((.Attributes("INCLUDE_IN_CCR").InnerText).Trim)
                        blnIncludeCCRoster = True
                    Else
                        blnIncludeCCRoster = False
                    End If

                    If ((.Attributes("INCLUDE_IN_CCR_REASON").InnerText).Trim = "") Then
                        strReason = vbNullString
                    Else
                        strReason = ((.Attributes("INCLUDE_IN_CCR_REASON").InnerText).Trim)
                    End If

                    If ((.Attributes("EMPLOYEEID").InnerText).Trim = "") Then
                        intEmployeeID = 0
                    Else
                        intEmployeeID = ((.Attributes("EMPLOYEEID").InnerText).Trim)
                    End If

                    If ((.Attributes("CUSTOMER_CATEGORY_ID").InnerText).Trim = "") Then
                        intCUSTOMER_CATEGORY_ID = 0
                    Else
                        intCUSTOMER_CATEGORY_ID = ((.Attributes("CUSTOMER_CATEGORY_ID").InnerText).Trim)
                    End If

                    If ((.Attributes("IPAddressID").InnerText).Trim = "") Then
                        strIPAddressID = ""
                    Else
                        strIPAddressID = ((.Attributes("IPAddressID").InnerText).Trim)
                    End If

                    If ((.Attributes("IPAddress").InnerText).Trim = "") Then
                        strIPAddress = ""
                    Else
                        strIPAddress = ((.Attributes("IPAddress").InnerText).Trim)
                    End If
                    If ((.Attributes("PANNO").InnerText).Trim <> "") Then
                        strPANNO = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("PANNO").InnerText
                    End If


                    If strAction = "I" Or strAction = "U" Then
                        If strAction = "U" Then
                            If intLocationCode = 0 Then
                                Throw (New AAMSException("Location code can't be blank."))
                            ElseIf strAgencyName = "" Then
                                Throw (New AAMSException("Agency Name can't be blank."))
                            ElseIf intChainCode = 0 Then
                                Throw (New AAMSException("Chain Code can't be blank."))
                            ElseIf strAddress = "" Then
                                Throw (New AAMSException("Address1 can't be blank."))
                            ElseIf strCity = "" Then
                                Throw (New AAMSException("City can't be blank."))
                            ElseIf strCountry = "" Then
                                Throw (New AAMSException("Country can't be blank."))
                            ElseIf strAoffice = "" Then
                                Throw (New AAMSException("Aoffice can't be blank."))
                            End If
                        End If
                        If strAction = "I" Then
                            If strAgencyName = "" Then
                                Throw (New AAMSException("Agency Name can't be blank."))
                            ElseIf intChainCode = 0 Then
                                Throw (New AAMSException("Chain Code can't be blank."))
                            ElseIf strAddress = "" Then
                                Throw (New AAMSException("Address1 can't be blank."))
                            ElseIf strCity = "" Then
                                Throw (New AAMSException("City can't be blank."))
                            ElseIf strCountry = "" Then
                                Throw (New AAMSException("Country can't be blank."))
                            ElseIf strAoffice = "" Then
                                Throw (New AAMSException("Aoffice can't be blank."))
                            End If
                        End If
                    Else
                        Throw (New AAMSException("Invalid Action Code."))
                    End If
                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_LOCATION_MASTER"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction
                    .Parameters.Add(New SqlParameter("@LOCATION_CODE", SqlDbType.Int))

                    If strAction = "I" Then
                        If intLocationCode = 0 Then
                            .Parameters("@LOCATION_CODE").Value = DBNull.Value
                        End If
                    Else
                        .Parameters("@LOCATION_CODE").Value = intLocationCode
                    End If


                    .Parameters.Add(New SqlParameter("@CHAINCODE", SqlDbType.Int))
                    .Parameters("@CHAINCODE").Value = intChainCode

                    .Parameters.Add(New SqlParameter("@Iata_tid", SqlDbType.VarChar, 20))
                    .Parameters("@Iata_tid").Value = strIATAID

                    .Parameters.Add(New SqlParameter("@Email", SqlDbType.VarChar, 100))
                    .Parameters("@Email").Value = strEmail

                    .Parameters.Add(New SqlParameter("@Fax", SqlDbType.VarChar, 30))
                    .Parameters("@Fax").Value = strFax

                    .Parameters.Add(New SqlParameter("@Phone", SqlDbType.VarChar, 30))
                    .Parameters("@Phone").Value = strPhone


                    .Parameters.Add(New SqlParameter("@Country", SqlDbType.VarChar, 20))
                    .Parameters("@Country").Value = strCountry

                    .Parameters.Add(New SqlParameter("@City", SqlDbType.VarChar, 30))
                    .Parameters("@City").Value = strCity

                    .Parameters.Add(New SqlParameter("@ADDRESS1", SqlDbType.VarChar, 100))
                    .Parameters("@ADDRESS1").Value = strAddress1

                    .Parameters.Add(New SqlParameter("@ADDRESS", SqlDbType.VarChar, 100))
                    .Parameters("@ADDRESS").Value = strAddress

                    .Parameters.Add(New SqlParameter("@NAME", SqlDbType.VarChar, 100))
                    .Parameters("@NAME").Value = strAgencyName

                    .Parameters.Add(New SqlParameter("@Aoffice", SqlDbType.Char, 3))
                    .Parameters("@Aoffice").Value = strAoffice

                    .Parameters.Add(New SqlParameter("@CONTACT_PERSON_ID", SqlDbType.Int))
                    If intCONTACTPERSONID = 0 Then
                        .Parameters("@CONTACT_PERSON_ID").Value = DBNull.Value
                    Else
                        .Parameters("@CONTACT_PERSON_ID").Value = intCONTACTPERSONID
                    End If
                    .Parameters.Add(New SqlParameter("@Ordernumber_Backup", SqlDbType.VarChar, 10))
                    .Parameters("@Ordernumber_Backup").Value = strOrdernumber_Backup


                    .Parameters.Add(New SqlParameter("@Ordernumber_Primary", SqlDbType.VarChar, 10))
                    .Parameters("@Ordernumber_Primary").Value = strOrdernumberPrimary


                    .Parameters.Add(New SqlParameter("@Install_Date_Backup", SqlDbType.Int))
                    If intInstallDateBackup = 0 Then
                        .Parameters("@Install_Date_Backup").Value = DBNull.Value
                    Else
                        .Parameters("@Install_Date_Backup").Value = intInstallDateBackup
                    End If

                    .Parameters.Add(New SqlParameter("@Install_Date_Primary", SqlDbType.Int))
                    If intInstallDatePrimary = 0 Then
                        .Parameters("@Install_Date_Primary").Value = DBNull.Value
                    Else
                        .Parameters("@Install_Date_Primary").Value = intInstallDatePrimary
                    End If

                    .Parameters.Add(New SqlParameter("@Online_Status_Backup", SqlDbType.VarChar, 6))
                    .Parameters("@Online_Status_Backup").Value = strOnlineStatusBackup

                    .Parameters.Add(New SqlParameter("@AgencystatusID", SqlDbType.Int))
                    If intAgencystatusID = 0 Then
                        .Parameters("@AgencystatusID").Value = DBNull.Value
                    Else
                        .Parameters("@AgencystatusID").Value = intAgencystatusID
                    End If
                    .Parameters.Add(New SqlParameter("@Agencytypeid", SqlDbType.Int))
                    If intAgencyTypeID = 0 Then
                        .Parameters("@Agencytypeid").Value = DBNull.Value
                    Else
                        .Parameters("@Agencytypeid").Value = intAgencyTypeID
                    End If
                    .Parameters.Add(New SqlParameter("@Location_Short_Name", SqlDbType.VarChar, 20))
                    .Parameters("@Location_Short_Name").Value = strLocationShortName

                    .Parameters.Add(New SqlParameter("@PriorityID", SqlDbType.Int))
                    If intPriorityID = 0 Then
                        .Parameters("@PriorityID").Value = DBNull.Value
                    Else
                        .Parameters("@PriorityID").Value = intPriorityID
                    End If
                    .Parameters.Add(New SqlParameter("@Resp_1A", SqlDbType.Int))
                    If int1AResponsiblity = 0 Then
                        .Parameters("@Resp_1A").Value = DBNull.Value
                    Else
                        .Parameters("@Resp_1A").Value = int1AResponsiblity
                    End If
                    .Parameters.Add(New SqlParameter("@Www_Address", SqlDbType.VarChar, 100))
                    .Parameters("@Www_Address").Value = strWebsite


                    .Parameters.Add(New SqlParameter("@Online_Status", SqlDbType.VarChar, 18))
                    .Parameters("@Online_Status").Value = strOnlineSatus

                    .Parameters.Add(New SqlParameter("@Date_Offline", SqlDbType.Int))
                    If intDateOffline = 0 Then
                        .Parameters("@Date_Offline").Value = DBNull.Value
                    Else
                        .Parameters("@Date_Offline").Value = intDateOffline
                    End If

                    .Parameters.Add(New SqlParameter("@Date_Online", SqlDbType.Int))
                    If intDateOnline = 0 Then
                        .Parameters("@Date_Online").Value = DBNull.Value
                    Else
                        .Parameters("@Date_Online").Value = intDateOnline
                    End If

                    .Parameters.Add(New SqlParameter("@Fileno", SqlDbType.Int))
                    If intFileNo = 0 Then
                        .Parameters("@Fileno").Value = DBNull.Value
                    Else
                        .Parameters("@Fileno").Value = intFileNo
                    End If

                    .Parameters.Add(New SqlParameter("@IncludeInCCRoster", SqlDbType.Bit))
                    If blnIncludeCCRoster = True Then
                        .Parameters("@IncludeInCCRoster").Value = 1
                    Else
                        .Parameters("@IncludeInCCRoster").Value = 0
                    End If

                    .Parameters.Add(New SqlParameter("@CC_Ex_Reason", SqlDbType.VarChar, 100))
                    .Parameters("@CC_Ex_Reason").Value = strReason

                    .Parameters.Add(New SqlParameter("@PinCode", SqlDbType.VarChar, 6))
                    .Parameters("@PinCode").Value = strPIN

                    .Parameters.Add(New SqlParameter("@EMPLOYEEID", SqlDbType.Int))
                    .Parameters("@EMPLOYEEID").Value = intEmployeeID

                    .Parameters.Add(New SqlParameter("@CUSTOMER_CATEGORY_ID", SqlDbType.Int))
                    .Parameters("@CUSTOMER_CATEGORY_ID").Value = intCUSTOMER_CATEGORY_ID

                    .Parameters.Add(New SqlParameter("@IPAddressID", SqlDbType.BigInt))
                    If strIPAddressID = "" Then
                        .Parameters("@IPAddressID").Value = DBNull.Value
                    Else
                        .Parameters("@IPAddressID").Value = CInt(strIPAddressID)
                    End If

                    .Parameters.Add(New SqlParameter("@IPAddress", SqlDbType.Char, 15))
                    If strIPAddress = "" Then
                        .Parameters("@IPAddress").Value = DBNull.Value
                    Else
                        .Parameters("@IPAddress").Value = strIPAddress
                    End If

                    .Parameters.Add(New SqlParameter("@PANNO", SqlDbType.VarChar, 20))
                    If strPANNO = "" Then
                        .Parameters("@PANNO").Value = DBNull.Value
                    Else
                        .Parameters("@PANNO").Value = strPANNO
                    End If

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0

                    If strAction = "U" Then
                        ''Update History Details
                        'Call View on the Locationcode
                        ''Compare ViewXmlOutput and Updated user Input
                        ''Insert the changed data in the log Table
                        Dim objInputXml, objHistoryOutputXml As New XmlDocument
                        Dim objXmlNode As XmlNode
                        Dim objXmlAttribute As XmlAttribute
                        Dim i As Integer
                        Dim strAttributeName As String = vbNullString
                        Dim strOnlineStatusAttributes As String
                        blnMaintainHistory = False
                        strOnlineStatusAttributes = "ONLINE_STATUS,ONLINE_STATUS_BACKUP,INSTALL_DATE_PRIMARY,INSTALL_DATE_BACKUP,ORDERNUMBER_PRIMARY,ORDERNUMBER_BACKUP,RESP_1A,AGENCYSTATUSID,AGENCYTYPEID,PRIORITYID,Chain_Code,CONTACT_PERSON_ID,CUSTOMER_CATEGORY_ID"
                        strChangeData = ""
                        objInputXml.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")
                        objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("Agency").Attributes("LOCATION_CODE").InnerText
                        objHistoryOutputXml = View(objInputXml)
                        If objHistoryOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            objXmlNode = UpdateDoc.DocumentElement.SelectSingleNode("Agency")
                            'objXmlNode = objHistoryOutputXml.DocumentElement.SelectSingleNode("Agency")
                            If objHistoryOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes("BRAGENCY") IsNot Nothing Then
                                If objHistoryOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes("BRAGENCY").InnerText.ToUpper = "TRUE" Then
                                    blnBRAgency = True
                                End If
                            End If
                            For Each objXmlAttribute In objXmlNode.Attributes
                                strAttributeName = objXmlAttribute.Name
                                If i = objXmlNode.Attributes.Count Then Exit For
                                If objHistoryOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes(strAttributeName) IsNot Nothing Then
                                    If objXmlAttribute.InnerText <> objHistoryOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes(strAttributeName).InnerText Then
                                        If InStr(strOnlineStatusAttributes, strAttributeName, CompareMethod.Text) > 0 Then
                                            If InStr(strAttributeName, "AGENCYSTATUSID", CompareMethod.Text) > 0 Then blnMaintainHistory = True
                                            If InStr(strAttributeName, "AGENCYTYPEID", CompareMethod.Text) > 0 Then blnMaintainHistory = True
                                            If InStr(strAttributeName, "PRIORITYID", CompareMethod.Text) > 0 Then blnMaintainHistory = True
                                            If InStr(strAttributeName, "CUSTOMER_CATEGORY_ID", CompareMethod.Text) > 0 Then blnMaintainHistory = True
                                        Else
                                            Select Case strAttributeName
                                                Case "Chain_Name"
                                                    strChangeData = strChangeData & ",Agency Group:-" & objHistoryOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes(strAttributeName).InnerText & " To " & objXmlAttribute.InnerText
                                                Case "ADDRESS"
                                                    strChangeData = strChangeData & ",Address1:-" & objHistoryOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes(strAttributeName).InnerText & " To " & objXmlAttribute.InnerText
                                                    If blnSendMailAddressChange = False Then blnSendMailAddressChange = True
                                                Case "ADDRESS1"
                                                    strChangeData = strChangeData & ",Address2:-" & objHistoryOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes(strAttributeName).InnerText & " To " & objXmlAttribute.InnerText
                                                    If blnSendMailAddressChange = False Then blnSendMailAddressChange = True
                                                Case "PINCODE"
                                                    strChangeData = strChangeData & ",Pin Code:-" & objHistoryOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes(strAttributeName).InnerText & " To " & objXmlAttribute.InnerText
                                                    If blnSendMailAddressChange = False Then blnSendMailAddressChange = True
                                                Case "IATA_TID"
                                                    strChangeData = strChangeData & ",IATA ID:-" & objHistoryOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes(strAttributeName).InnerText & " To " & objXmlAttribute.InnerText
                                                Case "LOCATION_SHORT_NAME"
                                                    strChangeData = strChangeData & ",Office Name:-" & objHistoryOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes(strAttributeName).InnerText & " To " & objXmlAttribute.InnerText
                                                Case "RESP_1A_NAME"
                                                    strChangeData = strChangeData & ",1A Responsibility:-" & objHistoryOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes(strAttributeName).InnerText & " To " & objXmlAttribute.InnerText
                                                Case "DATE_OFFLINE"
                                                    strChangeData = strChangeData & ",Date Offline:-" & ConvertDate(objHistoryOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes(strAttributeName).InnerText) & " To " & ConvertDate(objXmlAttribute.InnerText)
                                                Case "DATE_ONLINE"
                                                    strChangeData = strChangeData & ",Date Online:-" & ConvertDate(objHistoryOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes(strAttributeName).InnerText) & " To " & ConvertDate(objXmlAttribute.InnerText)
                                                Case "INCLUDE_IN_CCR"
                                                    strChangeData = strChangeData & ",Include in CC Roster:-" & objHistoryOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes(strAttributeName).InnerText & " To " & objXmlAttribute.InnerText
                                                Case "WWW_ADDRESS"
                                                    strChangeData = strChangeData & ",Web Site:-" & objHistoryOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes(strAttributeName).InnerText & " To " & objXmlAttribute.InnerText
                                                Case "INCLUDE_IN_CCR_REASON"
                                                    strChangeData = strChangeData & ",Reason:-" & objHistoryOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes(strAttributeName).InnerText & " To " & objXmlAttribute.InnerText
                                                Case "FILENO"
                                                    strChangeData = strChangeData & ",File Number:-" & objHistoryOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes(strAttributeName).InnerText & " To " & objXmlAttribute.InnerText
                                                Case "PANNO"
                                                    strChangeData = strChangeData & ",PAN No:-" & objHistoryOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes(strAttributeName).InnerText & " To " & objXmlAttribute.InnerText

                                                Case "IPAddressID"
                                                    strChangeData = strChangeData
                                                Case "IPAddress"
                                                    strChangeData = strChangeData & ",IPAddress:-" & objHistoryOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes(strAttributeName).InnerText & " To " & objXmlAttribute.InnerText
                                                Case Else
                                                    strChangeData = strChangeData & "," & StrConv(strAttributeName, VbStrConv.ProperCase) & ":-" & objHistoryOutputXml.DocumentElement.SelectSingleNode("Agency").Attributes(strAttributeName).InnerText & " To " & objXmlAttribute.InnerText
                                                    If strAttributeName = "CITY" Then
                                                        If blnSendMailAddressChange = False Then blnSendMailAddressChange = True
                                                    End If
                                            End Select
                                            blnMaintainHistory = True
                                        End If
                                    End If
                                End If
                                i = i + 1
                            Next
                        Else
                        End If
                        If strChangeData.Trim.Length > 1 Then strChangeData = Mid(strChangeData, 2, strChangeData.Trim.Length)
                        'objHisSqlCommand
                    End If
                    objSqlCommand.Connection.Open()
                    'MsgBox("ChangeData" & strChangeData, MsgBoxStyle.Information)
                    ''Update History Details--Ends here
                    If strAction = "U" Then ''Check for change in Group & if changed then AccountsManger id
                        ''Send Lcode & new Chaincode
                        Dim objSqlCmdAccMgr As New SqlCommand
                        Dim objSqlAccMgrReader As SqlDataReader
                        With objSqlCmdAccMgr
                            .Connection = objSqlConnection
                            .CommandText = "UP_GET_INC_AM_EMAILID"
                            .CommandType = CommandType.StoredProcedure
                            .Parameters.Add(New SqlParameter("@CHAIN_CODE", SqlDbType.Int))
                            .Parameters("@CHAIN_CODE").Value = intChainCode

                            .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                            .Parameters("@LCODE").Value = intLocationCode
                        End With
                        objSqlAccMgrReader = objSqlCmdAccMgr.ExecuteReader()
                        If objSqlAccMgrReader.HasRows Then
                            Do While objSqlAccMgrReader.Read
                                strCHAIN_CODE_OLD = Trim(objSqlAccMgrReader.GetValue(objSqlAccMgrReader.GetOrdinal("CHAIN_CODE_OLD")) & "")
                                strACCOUNTS_MGR_OLD = Trim(objSqlAccMgrReader.GetValue(objSqlAccMgrReader.GetOrdinal("ACCOUNTS_MGR_OLD")) & "")
                                strACCOUNTS_MGR_OLD_EMAIL = Trim(objSqlAccMgrReader.GetValue(objSqlAccMgrReader.GetOrdinal("ACCOUNTS_MGR_OLD_EMAIL")) & "")
                                strACCOUNTS_MGR_NEW = Trim(objSqlAccMgrReader.GetValue(objSqlAccMgrReader.GetOrdinal("ACCOUNTS_MGR_NEW")) & "")
                                strACCOUNTS_MGR_NEW_EMAIL = Trim(objSqlAccMgrReader.GetValue(objSqlAccMgrReader.GetOrdinal("ACCOUNTS_MGR_NEW_EMAIL")) & "")
                            Loop
                        End If
                        objSqlAccMgrReader.Close()
                        objSqlCmdAccMgr.Dispose()
                        If strACCOUNTS_MGR_OLD <> strACCOUNTS_MGR_NEW Then
                            'Get Mail Template
                            strSource = System.Configuration.ConfigurationSettings.AppSettings("MAIL_SOURCE").Trim
                            objInputSendMailXml.LoadXml(strSendMail_INPUT)

                            Dim strMailTemplate As String = ""
                            Dim objSqlCmdMTemplate As New SqlCommand
                            Dim objSqlTemplateReader As SqlDataReader
                            With objSqlCmdMTemplate
                                .Connection = objSqlConnection
                                .CommandText = "UP_SRO_EMAIL_TEMPLATES"
                                .CommandType = CommandType.StoredProcedure
                                .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.VarChar, 1))
                                .Parameters.Add(New SqlParameter("@MailTemplateName", SqlDbType.VarChar, 40))
                            End With
                            objSqlCmdMTemplate.Parameters("@ACTION").Value = "V"
                            objSqlCmdMTemplate.Parameters("@MailTemplateName").Value = "AGENCYADDITION_CHANGE"
                            objSqlTemplateReader = objSqlCmdMTemplate.ExecuteReader()
                            If objSqlTemplateReader.HasRows Then
                                Do While objSqlTemplateReader.Read()
                                    strMailTemplate = Trim(objSqlTemplateReader.GetValue(objSqlTemplateReader.GetOrdinal("MailTemplate")) & "")
                                Loop
                            End If
                            objSqlTemplateReader.Close()
                            objSqlCmdMTemplate.Dispose()
                            ''Get FeedbackMail Template Ends Here

                            'Replace EmailTemplate
                            strEmailBody = strMailTemplate
                            strEmailBody = strEmailBody.Replace("[[GROUPINCHARGE]]", strACCOUNTS_MGR_OLD)
                            strEmailBody = strEmailBody.Replace("[[LOCATION]]", intLocationCode & ", " & strAgencyName & "")
                            strEmailBody = strEmailBody.Replace("[[ACTION]]", "removed from")
                            strEmailBody = strEmailBody.Replace("[[CHAINCODE]]", strCHAIN_CODE_OLD)
                            strEmailBody = strEmailBody.Replace("[[DATE]]", DateTime.Today)

                            strEmailBodyNew = strMailTemplate
                            strEmailBodyNew = strEmailBodyNew.Replace("[[GROUPINCHARGE]]", strACCOUNTS_MGR_NEW)
                            strEmailBodyNew = strEmailBodyNew.Replace("[[LOCATION]]", intLocationCode & ", " & strAgencyName & "")
                            strEmailBodyNew = strEmailBodyNew.Replace("[[ACTION]]", "added to")
                            strEmailBodyNew = strEmailBodyNew.Replace("[[CHAINCODE]]", intChainCode)
                            strEmailBodyNew = strEmailBodyNew.Replace("[[DATE]]", DateTime.Today)

                        End If
                    End If
                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED
                    objTran = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)
                    objSqlCommand.Transaction = objTran
                    If strAction = "U" And blnMaintainHistory = True Then
                        Dim objHisSqlCommand As New SqlCommand
                        With objHisSqlCommand
                            .Connection = objSqlConnection
                            .CommandType = CommandType.StoredProcedure
                            .CommandText = "UP_SRO_TA_LOCATION_MASTER_LOG"

                            .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                            .Parameters("@LCODE").Value = intLocationCode
                            ''Added
                            .Parameters.Add(New SqlParameter("@AgencystatusID", SqlDbType.Int))
                            If intAgencystatusID = 0 Then
                                .Parameters("@AgencystatusID").Value = DBNull.Value
                            Else
                                .Parameters("@AgencystatusID").Value = intAgencystatusID
                            End If
                            .Parameters.Add(New SqlParameter("@Agencytypeid", SqlDbType.Int))
                            If intAgencyTypeID = 0 Then
                                .Parameters("@Agencytypeid").Value = DBNull.Value
                            Else
                                .Parameters("@Agencytypeid").Value = intAgencyTypeID
                            End If

                            .Parameters.Add(New SqlParameter("@PriorityID", SqlDbType.Int))
                            If intPriorityID = 0 Then
                                .Parameters("@PriorityID").Value = DBNull.Value
                            Else
                                .Parameters("@PriorityID").Value = intPriorityID
                            End If

                            .Parameters.Add(New SqlParameter("@CUSTOMER_CATEGORY_ID", SqlDbType.Int))
                            If intCUSTOMER_CATEGORY_ID = 0 Then
                                .Parameters("@CUSTOMER_CATEGORY_ID").Value = DBNull.Value
                            Else
                                .Parameters("@CUSTOMER_CATEGORY_ID").Value = intCUSTOMER_CATEGORY_ID
                            End If

                            .Parameters.Add(New SqlParameter("@EMPLOYEEID", SqlDbType.Int))
                            .Parameters("@EMPLOYEEID").Value = intEmployeeID

                            .Parameters.Add(New SqlParameter("@CHANGEDATA", SqlDbType.VarChar, 7000))
                            .Parameters("@CHANGEDATA").Value = strChangeData
                        End With
                        objHisSqlCommand.Transaction = objTran
                        intRecordsAffected = objHisSqlCommand.ExecuteNonQuery()
                    End If
                    intRecordsAffected = objSqlCommand.ExecuteNonQuery()
                    If UCase(strAction) = "I" Then
                        intRetId = .Parameters("@RETURNID").Value
                        If intRetId = -2 Then Throw (New AAMSException("Fileno already allocated!"))
                        If intRetId = -1 Then
                            Throw (New AAMSException("Agency already exist!"))
                        End If

                        If intRetId = 0 Then
                            Throw (New AAMSException("Unable to Insert!"))
                        End If
                        With objUpdateDocOutput.DocumentElement.SelectSingleNode("Agency")
                            .Attributes("LOCATION_CODE").InnerText = intRetId
                        End With
                    ElseIf UCase(strAction) = "U" Then
                        intRetId = .Parameters("@RETURNID").Value
                        If intRetId = -2 Then Throw (New AAMSException("Fileno already allocated!"))
                        If intRetId = -1 Then
                            Throw (New AAMSException("Agency alreay exist!"))
                        End If
                        If intRetId = 0 Then
                            Throw (New AAMSException("Unable to update!"))
                        End If
                    End If
                    objTran.Commit()
                End With
                ''Send Email to Account Manager Group Changed.
                If strACCOUNTS_MGR_OLD <> strACCOUNTS_MGR_NEW Then
                    Dim ObjSendMail As bizUtility.bzEmail
                    With objInputSendMailXml.SelectSingleNode("SENDMAILIMMEDIATE_INPUT/MAIL_DETAILS")
                        .Attributes("SOURCE").InnerText = strSource
                        .Attributes("DESTINATION_TO").InnerText = strACCOUNTS_MGR_OLD_EMAIL
                        .Attributes("SUBJECT").InnerText = "Group Changed"
                        .Attributes("MESSAGE").InnerText = strEmailBody
                    End With
                    ObjSendMail = New bizUtility.bzEmail
                    boolSendMailStatus = ObjSendMail.SendMail(objInputSendMailXml)


                    With objInputSendMailXml.SelectSingleNode("SENDMAILIMMEDIATE_INPUT/MAIL_DETAILS")
                        .Attributes("SOURCE").InnerText = strSource
                        .Attributes("DESTINATION_TO").InnerText = strACCOUNTS_MGR_NEW_EMAIL
                        .Attributes("SUBJECT").InnerText = "Group Changed"
                        .Attributes("MESSAGE").InnerText = strEmailBodyNew
                    End With
                    boolSendMailStatus = ObjSendMail.SendMail(objInputSendMailXml)
                End If
                If blnBRAgency = True And blnSendMailAddressChange = True Then
                    SendAddressDetailsMail(strAgencyName, strAddress, strAddress1, strCity, strPIN)
                End If
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                If Not objTran Is Nothing Then
                    objTran.Rollback()
                End If
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                If intRetId <= 0 Then
                    bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                End If
                Return objUpdateDocOutput
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                'Fill Error Status
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                Return objUpdateDocOutput
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Return objUpdateDocOutput
        End Function
        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.View
            'Purpose:This function gives details of Agency.
            'Input  :
            '<MS_VIEWAGENCY_INPUT>
            '	<LOCATION_CODE></LOCATION_CODE>
            '</MS_VIEWAGENCY_INPUT>

            'Output :
            '<MS_VIEWAGENCY_OUTPUT>
            '	<Agency LOCATION_CODE='' Chain_Code='' Chain_Name='' IATA_TID='' EMAIL='' FAX='' PHONE='' COUNTRY='' PINCODE=''
            '		CITY='' ADDRESS1='' ADDRESS='' NAME='' Aoffice='' CONTACT_PERSON_ID='' CONTACT_PERSON_NAME='' ORDERNUMBER_BACKUP=''
            '		ORDERNUMBER_PRIMARY='' INSTALL_DATE_BACKUP='' INSTALL_DATE_PRIMARY='' ONLINE_STATUS_BACKUP=''
            '		AGENCYSTATUSID='' AGENCYTYPEID='' LOCATION_SHORT_NAME='' PRIORITYID='' RESP_1A='' RESP_1A_NAME='' WWW_ADDRESS=''
            '		ONLINE_STATUS='' DATE_OFFLINE='' DATE_ONLINE='' FILENO='' INCLUDE_IN_CCR='' INCLUDE_IN_CCR_REASON='' OFFICEID='' IPAddressID='' IPAddress='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_UPDATEAGENCY_OUTPUT>

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim intLocationCode As Integer
            Const strMETHOD_NAME As String = "View"

            Try
                objOutputXml.LoadXml(strVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                intLocationCode = Integer.Parse(IndexDoc.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText.Trim)
                If intLocationCode = 0 Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_LOCATION_MASTER"
                    .Connection = objSqlConnection
                    .Parameters.Add("@LOCATION_CODE", SqlDbType.Int)
                    .Parameters("@LOCATION_CODE").Value = intLocationCode

                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    Dim blnRecordFound As Boolean
                    If blnRecordFound = False Then
                        With objOutputXml.DocumentElement.SelectSingleNode("Agency")
                            .Attributes("LOCATION_CODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOCATION_CODE")))
                            .Attributes("Chain_Code").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Chain_Code")))
                            .Attributes("Chain_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Chain_Name")))
                            .Attributes("IATA_TID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("IATA_TID")) & "")
                            .Attributes("EMAIL").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMAIL")) & "")

                            .Attributes("FAX").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FAX")) & "")
                            .Attributes("PHONE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PHONE")) & "")
                            .Attributes("COUNTRY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("COUNTRY")) & "")
                            .Attributes("PINCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PINCODE")) & "")

                            .Attributes("CITY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITY")) & "")
                            .Attributes("ADDRESS1").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ADDRESS1")) & "")
                            .Attributes("ADDRESS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ADDRESS")) & "")
                            .Attributes("NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NAME")) & "")
                            .Attributes("Aoffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Aoffice")) & "")

                            .Attributes("CONTACT_PERSON_ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CONTACT_PERSON_ID")) & "")
                            .Attributes("CONTACT_PERSON_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CONTACT_PERSON_NAME")) & "")
                            .Attributes("ORDERNUMBER_BACKUP").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDERNUMBER_BACKUP")) & "")
                            .Attributes("ORDERNUMBER_PRIMARY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDERNUMBER_PRIMARY")) & "")

                            .Attributes("INSTALL_DATE_BACKUP").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("INSTALL_DATE_BACKUP")) & "")
                            .Attributes("INSTALL_DATE_PRIMARY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("INSTALL_DATE_PRIMARY")) & "")

                            .Attributes("ONLINE_STATUS_BACKUP").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ONLINE_STATUS_BACKUP")) & "")
                            .Attributes("AGENCYSTATUSID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AGENCYSTATUSID")) & "")
                            .Attributes("AGENCYTYPEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AGENCYTYPEID")) & "")
                            .Attributes("LOCATION_SHORT_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOCATION_SHORT_NAME")) & "")

                            .Attributes("PRIORITYID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRIORITYID")) & "")
                            .Attributes("RESP_1A").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("RESP_1A")) & "")

                            .Attributes("RESP_1A_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("RESP_1A_NAME")) & "")
                            .Attributes("WWW_ADDRESS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("WWW_ADDRESS")) & "")
                            .Attributes("ONLINE_STATUS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ONLINE_STATUS")) & "")

                            .Attributes("DATE_OFFLINE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE_OFFLINE")) & "")
                            .Attributes("DATE_ONLINE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE_ONLINE")) & "")
                            .Attributes("FILENO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FILENO")) & "")

                            .Attributes("INCLUDE_IN_CCR").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("INCLUDE_IN_CCR")) & "")
                            .Attributes("INCLUDE_IN_CCR_REASON").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("INCLUDE_IN_CCR_REASON")) & "")
                            .Attributes("OFFICEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OFFICEID")) & "")
                            .Attributes("CUSTOMER_CATEGORY_ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CUSTOMER_CATEGORY_ID")) & "")

                            .Attributes("IPAddressID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("IPAddessID")) & "")
                            .Attributes("IPAddress").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("IPAddress")) & "")
                            .Attributes("Group_Classification_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Group_Classification_Name")) & "")
                            .Attributes("PANNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PANNO")) & "")
                            .Attributes("BRAGENCY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("BRAGENCY")) & "")

                            Dim strCOMP_VERTICAL As String = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("COMP_VERTICAL")) & "")
                            .Attributes("COMP_VERTICAL").InnerText = IIf(strCOMP_VERTICAL = "1", "Amadeus", IIf(strCOMP_VERTICAL = "2", "ResBird", (IIf(strCOMP_VERTICAL = "3", "Non1A", ""))))



                        End With
                    End If
                    blnRecordFound = True
                Loop
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
            objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Return objOutputXml
        End Function
        Public Function GetHistory(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function gives details of Agency.
            'Input  :
            '<MS_GETAGENCYHISTORY_INPUT>
            '	<LOCATION_CODE></LOCATION_CODE>
            '</MS_GETAGENCYHISTORY_INPUT>


            'Output :
            '<MS_GETAGENCYHISTORY_OUTPUT>
            '	<AGENCYHISTORY Employee_Name='' DateTime='' ChangeData='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_GETAGENCYHISTORY_OUTPUT>

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim intLocationCode As String
            Const strMETHOD_NAME As String = "GetHistory"
            Dim blnRecordFound As Boolean

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean


            Try
                objOutputXml.LoadXml(strAGENCYHISTORY_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                intLocationCode = SearchDoc.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText.Trim
                If intLocationCode = "" Then
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
                    .CommandText = "UP_SRO_TA_LOCATION_MASTER_LOG"
                    .Connection = objSqlConnection
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "S"

                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    .Parameters("@LCODE").Value = intLocationCode

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
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AGENCYHISTORY")

                If objSqlReader.HasRows Then
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.RemoveChild(objAptNode)
                End If

                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("Employee_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Employee_Name")) & "")
                    objAptNodeClone.Attributes("DateTime").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DateTime")) & "")
                    objAptNodeClone.Attributes("ChangeData").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ChangeData")) & "")

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
        Public Function GetAgencyStaffDetails(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the Agency Staff List, based on the given field value
            'Input  : 
            '<UP_GETAGENCYSTAFFDETAILS_INPUT>
            '<LCODE></LCODE>
            '</UP_GETAGENCYSTAFFDETAILS_INPUT>

            'Output :  
            '<UP_GETAGENCYSTAFFDETAILS_OUTPUT>
            '<AGENCYSTAFF LCODE='' AGENCYSTAFFID='' STAFFNAME='' DESIGNATION='' />
            '<Errors Status=''>
            '<Error Code='' Description='' />
            '</Errors>
            '</UP_GETAGENCYSTAFFDETAILS_OUTPUT>
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strLCODE As String
            Dim blnRecordFound As Boolean
            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Dim strMETHOD_NAME As String = "GetAgencyStaffDetails"

            objOutputXml.LoadXml(srtGETAGENCYSTAFFDETAILS_OUTPUT)

            Try

                If SearchDoc.DocumentElement.SelectSingleNode("LCODE").InnerText <> "" Then
                    strLCODE = SearchDoc.DocumentElement.SelectSingleNode("LCODE").InnerText
                Else
                    strLCODE = ""
                End If
                If strLCODE = "" Then
                    Throw (New AAMSException("Agency Location Code can't be blank."))
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
                    .CommandText = "UP_GET_TA_AGENCYSTAFF"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    .Parameters("@LCODE").Value = strLCODE

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

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF")
                If objSqlReader.HasRows Then
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.RemoveChild(objAptNode)
                End If

                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("LCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")))
                    objAptNodeClone.Attributes("AGENCYSTAFFID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AGENCYSTAFFID")))
                    objAptNodeClone.Attributes("STAFFNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("STAFFNAME")))
                    objAptNodeClone.Attributes("DESIGNATION").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DESIGNATION")))
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop
                objSqlReader.Close()
                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found")
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
            Catch exec As Exception
                'msgbox(exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try

            Return objOutputXml

        End Function
        Public Function GetAgencyCrsUseDetails(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the Agency CrsUse Detail List, based on the given field value
            'Input  : 
            '<UP_GETCRSDETAILS_INPUT>
            '   <LCODE></LCODE>
            '</UP_GETCRSDETAILS_INPUT>

            'Output :  
            '<UP_GETCRSDETAILS_OUTPUT>
            '<CRS RN='' LCODE='' DATE='' CURRENTID='' CRS='' OFFICEID='' Action='' />
            '   <Errors Status=''>
            '       <Error Code='' Description='' />
            '   </Errors>
            '</UP_GETCRSDETAILS_OUTPUT>
            ''************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strLCODE As String
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "GetAgencyCrsUseDetails"

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            objOutputXml.LoadXml(srtGETAGENCYCRSUSEDETAILS_OUTPUT)

            Try

                If SearchDoc.DocumentElement.SelectSingleNode("LCODE").InnerText <> "" Then
                    strLCODE = SearchDoc.DocumentElement.SelectSingleNode("LCODE").InnerText
                Else
                    strLCODE = ""
                End If
                If strLCODE = "" Then
                    Throw (New AAMSException("Agency Location Code can't be blank."))
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
                    .CommandText = "UP_GET_TA_AGENCYCRSUSEDETAILS"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    .Parameters("@LCODE").Value = strLCODE
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

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("CRS")
                If objSqlReader.HasRows Then
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.RemoveChild(objAptNode)
                End If
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("RN").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("RN")))
                    objAptNodeClone.Attributes("LCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")))
                    objAptNodeClone.Attributes("DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE")))
                    objAptNodeClone.Attributes("CURRENTID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CURRENTID")))
                    objAptNodeClone.Attributes("CRS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CRS")))
                    objAptNodeClone.Attributes("OFFICEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OFFICEID")))

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop
                objSqlReader.Close()
                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found")
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
            Catch exec As Exception
                'msgbox(exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try

            Return objOutputXml

        End Function
        Public Function GetAgencyCompetitionDetails(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the Agency Competition Detail List, based on the given field value
            'Input  : 
            '<UP_GETCOMPETITIONDETAILS_INPUT>
            '   <LOCATION_CODE></LOCATION_CODE>
            '</UP_GETCOMPETITIONDETAILS_INPUT>

            'Output :  
            '<UP_GETCOMPETITIONDETAILS_OUTPUT>
            '<COMPETITION_DETAILS CRSID='' LOCATION_CODE ='' DATE_END='' DATE_START='' SOLE_USER='' DIAL_BACKUP='' ONLINESTATUSID=''
            'PRINTER_COUNT='' PC_COUNT='' Action='' />
            '   <Errors Status=''>
            '       <Error Code='' Description='' />
            '   </Errors>
            '</UP_GETCOMPETITIONDETAILS_OUTPUT>
            ''************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strLCODE As String
            Dim blnRecordFound As Boolean

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Dim strMETHOD_NAME As String = "GetAgencyCompetitionDetails"


            objOutputXml.LoadXml(srtGETAGENCYCOMPETITIONDETAILS_OUTPUT)

            Try

                If SearchDoc.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText <> "" Then
                    strLCODE = SearchDoc.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText
                Else
                    strLCODE = ""
                End If
                If strLCODE = "" Then
                    Throw (New AAMSException("Agency Location Code can't be blank."))
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
                    .CommandText = "UP_GET_TA_AGENCYCOMPETITIONDETAILS"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    .Parameters("@LCODE").Value = strLCODE

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

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS")
                If objSqlReader.HasRows Then
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.RemoveChild(objAptNode)
                End If
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("CRSID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CRSID")) & "")
                    objAptNodeClone.Attributes("LOCATION_CODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOCATION_CODE")) & "")
                    objAptNodeClone.Attributes("DATE_END").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE_END")) & "")
                    objAptNodeClone.Attributes("DATE_START").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE_START")) & "")
                    objAptNodeClone.Attributes("SOLE_USER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SOLE_USER")) & "")
                    objAptNodeClone.Attributes("DIAL_BACKUP").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DIAL_BACKUP")) & "")
                    objAptNodeClone.Attributes("ONLINESTATUSID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ONLINESTATUSID")) & "")
                    objAptNodeClone.Attributes("PRINTER_COUNT").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRINTER_COUNT")) & "")
                    objAptNodeClone.Attributes("PC_COUNT").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PC_COUNT")) & "")
                    objAptNodeClone.Attributes("CommercialDetails").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CommercialDetails")) & "")

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop
                objSqlReader.Close()
                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found")
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
            Catch exec As Exception
                'msgbox(exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try

            Return objOutputXml

        End Function
        Public Function GetInstalledPC(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the Misc Installation record, based on the given field value
            'Input  : 
            '<MS_GETPCINSTALLATION_INPUT>
            '	<LCODE></LCODE>
            '</MS_GETPCINSTALLATION_INPUT>

            'Output :  
            '<MS_GETPCINSTALLATION_OUTPUT>
            '	<PCINSTALLATION ROWID='' LCODE='' DATE='' CPUTYPE='' CPUNO='' MONTYPE='' MONNO='' KBDTYPE='' KBDNO='' MSETYPE='' MSENO='' OrderNumber='' REMARKS='' CHALLANDATE='' CHALLANNUMBER='' LoggedBy='' Employee_Name='' LoggedDateTime='' CHALLANSTATUS='' CDRNO='' LastModifiedDate =''  />
            '   <TOTAL A1PC='' AGENCYPC='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_GETPCINSTALLATION_OUTPUT>

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strLCODE As String
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "GetInstalledPC"
            Dim intA1PC As Integer = 0
            Dim intAGENCYPC As Integer = 0

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean
            objOutputXml.LoadXml(srtGETPCINSTALLATION_OUTPUT)

            Try

                If SearchDoc.DocumentElement.SelectSingleNode("LCODE").InnerText <> "" Then
                    strLCODE = SearchDoc.DocumentElement.SelectSingleNode("LCODE").InnerText
                Else
                    strLCODE = ""
                End If
                If strLCODE = "" Then
                    Throw (New AAMSException("Agency Code can't be blank."))
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
                    .CommandText = "UP_GET_TA_PCINSTALLATION"
                    .Connection = objSqlConnection
                    .Parameters.Add(New SqlParameter("@Action", SqlDbType.Char))
                    .Parameters("@Action").Value = "S"

                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    .Parameters("@LCODE").Value = strLCODE

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

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = ""

                    .Parameters.Add(New SqlParameter("@AGENCYPC", SqlDbType.Int))
                    .Parameters("@AGENCYPC").Direction = ParameterDirection.Output
                    .Parameters("@AGENCYPC").Value = 0

                    .Parameters.Add(New SqlParameter("@A1PC", SqlDbType.Int))
                    .Parameters("@A1PC").Direction = ParameterDirection.Output
                    .Parameters("@A1PC").Value = 0

                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("PCINSTALLATION")
                objAptNodeClone = objAptNode.CloneNode(True)
                If objSqlReader.HasRows = True Then objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("ROWID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ROWID")))
                    objAptNodeClone.Attributes("LCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")) & "")
                    objAptNodeClone.Attributes("DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE")) & "")

                    objAptNodeClone.Attributes("CPUTYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CPUTYPE")) & "")
                    objAptNodeClone.Attributes("CPUNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CPUNO")) & "")

                    If Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CPUTYPE")) & "") = "CPP" Or Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MONTYPE")) & "") = "MMP" Then
                        intAGENCYPC = intAGENCYPC + 1
                    Else
                        intA1PC = intA1PC + 1
                    End If

                    objAptNodeClone.Attributes("MONTYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MONTYPE")) & "")
                    objAptNodeClone.Attributes("MONNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MONNO")) & "")

                    objAptNodeClone.Attributes("MSETYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MSETYPE")) & "")
                    objAptNodeClone.Attributes("MSENO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MSENO")) & "")

                    objAptNodeClone.Attributes("KBDTYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("KBDTYPE")) & "")
                    objAptNodeClone.Attributes("KBDNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("KBDNO")) & "")


                    objAptNodeClone.Attributes("OrderNumber").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OrderNumber")) & "")
                    objAptNodeClone.Attributes("REMARKS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("REMARKS")) & "")
                    objAptNodeClone.Attributes("CHALLANDATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHALLANDATE")) & "")
                    objAptNodeClone.Attributes("CHALLANNUMBER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHALLANNUMBER")) & "")
                    objAptNodeClone.Attributes("LoggedBy").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LoggedBy")) & "")
                    objAptNodeClone.Attributes("Employee_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Employee_Name")) & "")
                    objAptNodeClone.Attributes("LoggedDateTime").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LoggedDateTime")) & "")
                    objAptNodeClone.Attributes("CHALLANSTATUS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHALLANSTATUS")) & "")
                    objAptNodeClone.Attributes("CDRNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CDRNO")) & "")
                    objAptNodeClone.Attributes("LastModifiedDate").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LastModifiedDate")) & "")

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop

                objSqlReader.Close()
                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found")
                Else
                    'objOutputXml.DocumentElement("TOTAL").Attributes("A1PC").InnerText = intA1PC.ToString
                    'objOutputXml.DocumentElement("TOTAL").Attributes("AGENCYPC").InnerText = intAGENCYPC.ToString

                    objOutputXml.DocumentElement("TOTAL").Attributes("A1PC").InnerText = Val(objSqlCommand.Parameters("@A1PC").Value)
                    objOutputXml.DocumentElement("TOTAL").Attributes("AGENCYPC").InnerText = Val(objSqlCommand.Parameters("@AGENCYPC").Value)

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
            Catch exec As Exception
                'msgbox(exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try

            Return objOutputXml
        End Function
        Public Function GetInstalledMiscHW(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the Misc Installation record, based on the given field value
            'Input  : 
            '<MS_GETMISCINSTALLATION_INPUT>
            '	<LCode></LCode>
            '</MS_GETMISCINSTALLATION_INPUT>]
            'Output :  
            '<MS_GETMISCINSTALLATION_OUTPUT>
            '	<GETPCINSTALLATION ROWID='' LCODE='' DATE='' EQUIPMENT_TYPE='' EQUIPMENT_NO='' QTY='' OrderNUmber='' CHALLANDATE='' ChallanNumber='' LoggedBy='' Employee_Name='' LoggedDateTime='' CHALLANSTATUS='' />
            '   <TOTAL MISCPC='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_GETMISCINSTALLATION_OUTPUT>

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strLCODE As String
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "GetInstalledMiscHW"
            Dim intMISCPC As Integer = 0
            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean


            objOutputXml.LoadXml(srtGETMISCINSTALLATION_OUTPUT)

            Try

                If SearchDoc.DocumentElement.SelectSingleNode("LCode").InnerText <> "" Then
                    strLCODE = SearchDoc.DocumentElement.SelectSingleNode("LCode").InnerText
                Else
                    strLCODE = ""
                End If
                If strLCODE = "" Then
                    Throw (New AAMSException("Agency Code can't be blank."))
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
                    .CommandText = "UP_SRO_TA_MISCINSTALLATION"
                    .Connection = objSqlConnection
                    .Parameters.Add(New SqlParameter("@Action", SqlDbType.Char))
                    .Parameters("@Action").Value = "S"

                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    .Parameters("@LCODE").Value = strLCODE

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

                    .Parameters.Add(New SqlParameter("@RETUNID", SqlDbType.Int))
                    .Parameters("@RETUNID").Direction = ParameterDirection.Output
                    .Parameters("@RETUNID").Value = ""
                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("GETMISCINSTALLATION")
                objAptNodeClone = objAptNode.CloneNode(True)
                If objSqlReader.HasRows = True Then objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    intMISCPC = intMISCPC + 1
                    objAptNodeClone.Attributes("ROWID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ROWID")))
                    objAptNodeClone.Attributes("LCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")))
                    objAptNodeClone.Attributes("DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE")) & "")
                    objAptNodeClone.Attributes("EQUIPMENT_TYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EQUIPMENT_TYPE")) & "")
                    objAptNodeClone.Attributes("EQUIPMENT_NO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EQUIPMENT_NO")) & "")
                    objAptNodeClone.Attributes("QTY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("QTY")) & "")
                    objAptNodeClone.Attributes("OrderNUmber").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OrderNUmber")) & "")
                    objAptNodeClone.Attributes("CHALLANDATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHALLANDATE")) & "")
                    objAptNodeClone.Attributes("ChallanNumber").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ChallanNumber")) & "")

                    objAptNodeClone.Attributes("LoggedBy").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LoggedBy")) & "")
                    objAptNodeClone.Attributes("Employee_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Employee_Name")) & "")
                    objAptNodeClone.Attributes("LoggedDateTime").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LoggedDateTime")) & "")
                    objAptNodeClone.Attributes("CHALLANSTATUS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHALLANSTATUS")) & "")
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop
                objSqlReader.Close()
                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found")
                Else
                    objOutputXml.DocumentElement("TOTAL").Attributes("MISCPC").InnerText = intMISCPC.ToString

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
            Catch exec As Exception
                'msgbox(exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try

            Return objOutputXml
        End Function
        Public Function GetConnectivityHistory(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function gives History of Agency Connectivity.
            'Input  :
            '<MS_GETAGENCYCONNECTIVITYHISTORY_INPUT>
            '	<LOCATION_CODE></LOCATION_CODE>
            '</MS_GETAGENCYCONNECTIVITYHISTORY_INPUT>


            'Output :
            '<MS_GETAGENCYCONNECTIVITYHISTORY_OUTPUT>
            '	<AGENCYCONNECTIVITYHISTORY LCode='' Online_Status='' ConnectivityType= '' InstallDate='' EndDate ='' ChangeBy='' ChangeDate='' Reason='' IOrderNumber='' DOrderNumber='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_GETAGENCYCONNECTIVITYHISTORY_OUTPUT>

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim intLocationCode As String
            Const strMETHOD_NAME As String = "GetConnectivityHistory"
            Dim blnRecordFound As Boolean
            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean


            Try
                objOutputXml.LoadXml(strAGENCYCONNECTIVITYHISTORY_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                intLocationCode = SearchDoc.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText.Trim

                If intLocationCode = "" Then
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
                    .CommandText = "UP_GET_TA_CONNECTIVITYHISTORY"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    .Parameters("@LCODE").Value = intLocationCode
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
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AGENCYCONNECTIVITYHISTORY")

                If objSqlReader.HasRows Then
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.RemoveChild(objAptNode)
                End If

                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("LCode").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCode")) & "")
                    objAptNodeClone.Attributes("Online_Status").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Online_Status")) & "")
                    objAptNodeClone.Attributes("ConnectivityType").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ConnectivityType")) & "")
                    objAptNodeClone.Attributes("InstallDate").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("InstallDate")) & "")
                    objAptNodeClone.Attributes("EndDate").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EndDate")) & "")
                    objAptNodeClone.Attributes("ChangeBy").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ChangeBy")) & "")
                    objAptNodeClone.Attributes("ChangeDate").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ChangeDate")) & "")
                    objAptNodeClone.Attributes("Reason").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Reason")) & "")
                    objAptNodeClone.Attributes("IOrderNumber").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("IOrderNumber")) & "")
                    objAptNodeClone.Attributes("DOrderNumber").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DOrderNumber")) & "")
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
        Public Function GetFileNumber() As System.Xml.XmlDocument
            '***********************************************************************
            '#############################################################################
            ' Method Written by Ashish Srivastava on date 18-Dec-2007
            '############################################################################
            'Purpose: To list out the those  record having fileno status is zero, 
            'Input  : 

            'Output :  
            '<T_C_AGENCY_FILENO>
            '          <Status />
            '          <FileNo />
            '< /T_C_AGENCY_FILENO>

            '<T_C_AGENCY_FILENO>
            '<FILENUMBER Status='' FileNo='' />
            '<Errors Status=''><Error Code='' Description='' /></Errors>
            '</T_C_AGENCY_FILENO>"
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "GetFileNumber"

            objOutputXml.LoadXml(strGetFileNumber_OUTPUT)
            Try
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_GET_TA_FILENO"
                    .Connection = objSqlConnection
                End With

                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("FILENUMBER")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("FileNo").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FileNo")))
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop

                If (blnRecordFound = False) Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                End If
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch exec As Exception
                'msgbox(exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function
        Public Function ScannedFileStatus(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function gives History of Agency Connectivity.
            'Input  :
            '<UP_GETFINENOSTATUS_INPUT>
            '<FILENO/>
            '</UP_GETFINENOSTATUS_INPUT >

            'Output :
            '<UP_GETFINENOSTATUS_OUTPUT>
            '   <FILESTATUS ScannedFileStatus=''/>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</UP_GETFINENOSTATUS_OUTPUT >
            '************************************************************************
            Dim strScannedFileStatus_Output As String
            Dim objSqlConnection As New SqlConnection(bzShared.GetDOCConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim intFileno As String
            Const strMETHOD_NAME As String = "ScannedFileStatus"
            Dim blnRecordFound As Boolean
            strScannedFileStatus_Output = "<UP_GETFINENOSTATUS_OUTPUT><FILESTATUS ScannedFileStatus=''/><Errors Status=''><Error Code='' Description='' /></Errors></UP_GETFINENOSTATUS_OUTPUT>"


            Try
                objOutputXml.LoadXml(strScannedFileStatus_Output)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                intFileno = SearchDoc.DocumentElement.SelectSingleNode("FILENO").InnerText.Trim

                If intFileno = "" Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_GET_TA_SCANNEDFILESTATUS]"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@FILENO", SqlDbType.Int))
                    .Parameters("@FILENO").Value = intFileno
                End With

                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                If objSqlReader.HasRows Then
                    objOutputXml.DocumentElement.SelectSingleNode("FILESTATUS").Attributes("ScannedFileStatus").InnerText = "True"
                    blnRecordFound = True
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("FILESTATUS").Attributes("ScannedFileStatus").InnerText = "False"
                    blnRecordFound = False
                End If

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
        Public Function SaveAgencyFileno(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function gives History of Agency Connectivity.
            'Input  :

            '<UP_UPDATEFILENO_INPUT>
            '   <FILENO/>
            '   <LOCATION_CODE/>
            '   </STATUS>   'change
            '</UP_UPDATEFILENO_INPUT>

            'Output :
            '<UP_UPDATEFILENO_OUTPUT>
            '   <FILEDETAILS FILENO=''/>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</UP_UPDATEFILENO_OUTPUT>
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objSqlDocCommand As New SqlCommand

            Dim objUpdateDocOutput As New XmlDocument
            Dim objUpdateDoc_DocOutput As New XmlDocument

            Dim strLCODE As String
            Dim strFileno As String
            Dim intRetId As Integer
            Dim intRecordsAffected As Int32
            Dim strStatusID As String
            Const strMETHOD_NAME As String = "SaveAgencyFileno"
            Try
                objUpdateDocOutput.LoadXml(strUPDATEFileno_OUTPUT)

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("LOCATION_CODE")
                    strLCODE = .InnerText
                End With

                With UpdateDoc.DocumentElement.SelectSingleNode("FILENO")
                    strFileno = .InnerText
                End With

                With UpdateDoc.DocumentElement.SelectSingleNode("STATUS")
                    strStatusID = .InnerText
                End With


                If strLCODE = "" Then
                    Throw (New AAMSException("Agency Code can't be blank."))
                End If
                If strFileno = "" Then
                    Throw (New AAMSException("File Number can't be blank."))
                End If



                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_AGENCY_FILENO"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "U"

                    .Parameters.Add(New SqlParameter("@LOCATION_CODE", SqlDbType.Int))
                    .Parameters("@LOCATION_CODE").Value = strLCODE

                    .Parameters.Add(New SqlParameter("@FILENO", SqlDbType.Int))
                    .Parameters("@FILENO").Value = CType(strFileno, Integer)

                    If strStatusID = "" Then
                        .Parameters.Add(New SqlParameter("@AGENCYSTATUSID", SqlDbType.Int))
                        .Parameters("@AGENCYSTATUSID").Value = System.DBNull.Value
                    Else
                        .Parameters.Add(New SqlParameter("@AGENCYSTATUSID", SqlDbType.Int))
                        .Parameters("@AGENCYSTATUSID").Value = CType(strStatusID, Integer)

                    End If

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = ""

                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED
                    .Connection.Open()
                    intRecordsAffected = .ExecuteNonQuery()

                    intRetId = .Parameters("@RETURNID").Value
                    If intRetId = 0 Then
                        Throw (New AAMSException("Unable to Update!"))
                    Else
                        objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"

                        '' Code to Update FileNo into T_C_Fillings on AAMSDOCUMENT Databse
                        'START ##################################################################

                        Dim objSqlDocumentConnection As New SqlConnection(bzShared.GetDOCConnectionString())
                        objUpdateDoc_DocOutput.LoadXml("<UP_TA_UPDATE_DOC_FILING_OUTPUT><DETAILS FILENO='' LOCATION_CODE=''/><Errors Status=''>	<Error Code='' Description=''/></Errors></UP_TA_UPDATE_DOC_FILING_OUTPUT>")

                        With objSqlDocCommand


                            .Connection = objSqlDocumentConnection
                            .CommandType = CommandType.StoredProcedure
                            .CommandText = "UP_SRO_TA_TCFILING_FILENO"

                            .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                            .Parameters("@ACTION").Value = "U"

                            .Parameters.Add(New SqlParameter("@LOCATION_CODE", SqlDbType.Int))
                            .Parameters("@LOCATION_CODE").Value = strLCODE

                            .Parameters.Add(New SqlParameter("@FILENO", SqlDbType.Int))
                            .Parameters("@FILENO").Value = CType(strFileno, Integer)

                            .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                            .Parameters("@RETURNID").Direction = ParameterDirection.Output
                            .Parameters("@RETURNID").Value = ""

                            'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED
                            If .Connection.State = ConnectionState.Open Then .Connection.Close()
                            .Connection.Open()
                            intRecordsAffected = .ExecuteNonQuery()

                            intRetId = .Parameters("@RETURNID").Value
                            If intRetId = 0 Then
                                Throw (New AAMSException("Unable to Update!"))
                            Else
                                objUpdateDoc_DocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                            End If
                        End With
                        'END ##################################################################

                    End If
                End With
                'Checking whether record is deleted successfully or not
                objUpdateDocOutput.DocumentElement.SelectSingleNode("FILEDETAILS").Attributes("FILENO").InnerText = strFileno
                objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                Return objUpdateDocOutput
            Catch Exec As Exception
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                Return objUpdateDocOutput
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objUpdateDocOutput
        End Function
        Public Function GetFileNoDetails(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************            
            'Purpose: To list out the those  record having fileno status is zero, 
            'Input  : 
            '<TA_GETAGENCYFILENODETAILS_INPUT>
            '   <LCode/>
            '   <FILENO/>
            '</TA_GETAGENCYFILENODETAILS_INPUT>

            'Output :  
            '<TA_GETAGENCYFILENODETAILS_OUTPUT>
            '	<DETAILS ID='' DOCTYPE="" FILENO="" FILEORDER="" ORDER_NO="" ORDER_TYPE="" ContentType="" EmailFrom="" EmailTo="" EmailSubject="" EmailBody="" PDFDocFileName=""></DETAILS>
            '</TA_GETAGENCYFILENODETAILS_OUTPUT>

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strFineNo As Integer
            Dim intLCode As Integer
            Dim strMETHOD_NAME As String = "GetFileNoDetails"

            objOutputXml.LoadXml(strGETFilenoTransfer_OUTPUT)
            Try

                If UpdateDoc.DocumentElement.SelectSingleNode("LCode") IsNot Nothing Then
                    If UpdateDoc.DocumentElement.SelectSingleNode("LCode").InnerText <> "" Then
                        intLCode = UpdateDoc.DocumentElement.SelectSingleNode("LCode").InnerText
                    End If
                End If
                If intLCode = 0 Then
                    Throw (New AAMSException("Agency can't be blank."))
                End If
                If UpdateDoc.DocumentElement.SelectSingleNode("FILENO").InnerText <> "" Then
                    strFineNo = UpdateDoc.DocumentElement.SelectSingleNode("FILENO").InnerText
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_FILENO"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "V"

                    .Parameters.Add(New SqlParameter("@ID", SqlDbType.Int))
                    .Parameters("@ID").Value = DBNull.Value

                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.BigInt))
                    .Parameters("@LCODE").Value = intLCode

                    .Parameters.Add(New SqlParameter("@FILENO", SqlDbType.Int))
                    If strFineNo = 0 Then
                        .Parameters("@FILENO").Value = DBNull.Value
                    Else
                        .Parameters("@FILENO").Value = strFineNo
                    End If


                    .Parameters.Add(New SqlParameter("@ORDERNO", SqlDbType.Char, 40))
                    .Parameters("@ORDERNO").Value = DBNull.Value

                    .Parameters.Add(New SqlParameter("@DOCTYPE", SqlDbType.Int))
                    .Parameters("@DOCTYPE").Value = 0

                    .Parameters.Add(New SqlParameter("@SEQUENCE", SqlDbType.Int))
                    .Parameters("@SEQUENCE").Value = 0

                    .Parameters.Add(New SqlParameter("@ORDERTYPE", SqlDbType.Char, 200))
                    .Parameters("@ORDERTYPE").Value = DBNull.Value

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0

                End With

                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("DETAILS")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ID")))
                    objAptNodeClone.Attributes("LCode").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCode")))
                    objAptNodeClone.Attributes("FILENO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FILENO")))
                    objAptNodeClone.Attributes("DOCTYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DOCTYPE")))
                    objAptNodeClone.Attributes("ORDER_NO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDER_NO")))
                    objAptNodeClone.Attributes("FILEORDER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FILEORDER")))
                    objAptNodeClone.Attributes("ORDER_TYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDER_TYPE")))
                    objAptNodeClone.Attributes("ContentType").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ContentType")))

                    objAptNodeClone.Attributes("EmailFrom").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailFrom")))
                    objAptNodeClone.Attributes("EmailTo").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailTo")))
                    objAptNodeClone.Attributes("EmailSubject").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailSubject")))
                    objAptNodeClone.Attributes("EmailBody").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailBody")))
                    objAptNodeClone.Attributes("PDFDocFileName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PDFDocFileName")))

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop

                If (blnRecordFound = False) Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                End If
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch exec As Exception
                'msgbox(exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function
        Public Function GetFileNoDetailsForDoc(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************            
            'Purpose: To list out the those  record having fileno status is zero, 
            'Input  : 
            '<TA_GETAGENCYFILENODETAILS_INPUT>
            '   <FILENO/>
            '</TA_GETAGENCYFILENODETAILS_INPUT>

            'Output :  
            '<TA_GETAGENCYFILENODETAILS_OUTPUT>
            '	<DETAILS ID='' DOCTYPE="" FILENO="" FILEORDER="" ORDER_NO="" ORDER_TYPE="" ContentType="" EmailFrom="" EmailTo="" EmailSubject="" EmailBody="" PDFDocFileName=""></DETAILS>
            '</TA_GETAGENCYFILENODETAILS_OUTPUT>

            Dim objSqlConnection As New SqlConnection(bzShared.GetDOCConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strFineNo As Integer
            Dim strMETHOD_NAME As String = "GetFileNoDetails"

            objOutputXml.LoadXml(strGETFilenoTransfer_OUTPUT)
            Try

                If UpdateDoc.DocumentElement.SelectSingleNode("FILENO").InnerText <> "" Then
                    strFineNo = UpdateDoc.DocumentElement.SelectSingleNode("FILENO").InnerText
                End If
                If Len(strFineNo) = 0 Then
                    Throw (New AAMSException("File No can't be blank."))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_FILENO"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "V"

                    .Parameters.Add(New SqlParameter("@ID", SqlDbType.Int))
                    .Parameters("@ID").Value = DBNull.Value

                    .Parameters.Add(New SqlParameter("@FILENO", SqlDbType.Int))
                    .Parameters("@FILENO").Value = strFineNo

                    .Parameters.Add(New SqlParameter("@ORDERNO", SqlDbType.Char, 40))
                    .Parameters("@ORDERNO").Value = DBNull.Value

                    .Parameters.Add(New SqlParameter("@DOCTYPE", SqlDbType.Int))
                    .Parameters("@DOCTYPE").Value = 0

                    .Parameters.Add(New SqlParameter("@SEQUENCE", SqlDbType.Int))
                    .Parameters("@SEQUENCE").Value = 0

                    .Parameters.Add(New SqlParameter("@ORDERTYPE", SqlDbType.Char, 200))
                    .Parameters("@ORDERTYPE").Value = DBNull.Value

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0

                End With

                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("DETAILS")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ID")))
                    objAptNodeClone.Attributes("FILENO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FILENO")))
                    objAptNodeClone.Attributes("DOCTYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DOCTYPE")))
                    objAptNodeClone.Attributes("ORDER_NO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDER_NO")))
                    objAptNodeClone.Attributes("FILEORDER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FILEORDER")))
                    objAptNodeClone.Attributes("ORDER_TYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDER_TYPE")))
                    objAptNodeClone.Attributes("ContentType").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ContentType")))

                    objAptNodeClone.Attributes("EmailFrom").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailFrom")))
                    objAptNodeClone.Attributes("EmailTo").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailTo")))
                    objAptNodeClone.Attributes("EmailSubject").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailSubject")))
                    objAptNodeClone.Attributes("EmailBody").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EmailBody")))
                    objAptNodeClone.Attributes("PDFDocFileName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PDFDocFileName")))

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop

                If (blnRecordFound = False) Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                End If
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch exec As Exception
                'msgbox(exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function
        Public Function UpdateFile(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************            
            'Purpose: To list out the those  record having fileno status is zero, 
            'Input  : 
            '<TA_UPDATEAGENCYFILENODETAILS_INPUT>
            '   <AGENCYFILE ID='' LCode='' FILENO=''  ORDERNO ='' DOCTYPE ='' SEQUENCE='' ORDERTYPE='' DOCUMENT ='' ContentType='' EmailFrom=''	EmailTo	='' EmailSubject='' EmailBody='' PDFDocFileName=''/>
            '</TA_UPDATEAGENCYFILENODETAILS_INPUT>

            'Output :  
            '<TA_UPDATEAGENCYFILENODETAILS_OUTPUT>
            '<AGENCYFILE ID='' LCode='' FILENO=''  ORDERNO ='' DOCTYPE ='' SEQUENCE='' ORDERTYPE='' ContentType='' EmailFrom=''	EmailTo	='' EmailSubject=''	EmailBody='' NewPDFDocFileName=''/>
            '   <Errors Status=''>
            '       <Error Code='' Description='' />
            '   </Errors>
            '</TA_UPDATEAGENCYFILENODETAILS_OUTPUT>
            '***********************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetDOCConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim intLCode As Integer '--Added
            Dim IntfileNo As UInteger
            Dim strID As Integer = 0
            Dim strORDERNO As String = ""
            Dim strDOCTYPE As Integer = 0
            Dim strSEQUENCE As Integer = 0
            Dim strORDERTYPE As String = ""

            Dim strContentType As String = ""
            Dim strEmailFrom As String = ""
            Dim strEmailTo As String = ""
            Dim strEmailBody As String = ""
            Dim strPDFDocFileName As String = ""
            Dim strEmailSubject As String = ""

            Dim intRetId As Integer
            Dim intRecordsAffected As Integer

            Dim strMETHOD_NAME As String = "UpdateFile"

            objOutputXml.LoadXml(strUPDATEFilenoTransfer_OUTPUT)
            Try
                If UpdateDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("LCode") IsNot Nothing Then
                    If UpdateDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("LCode").InnerText <> "" Then
                        intLCode = CInt(UpdateDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("LCode").InnerText)
                    End If
                End If
                If Len(intLCode) = 0 Then
                    Throw (New AAMSException("Agency can't be blank."))
                End If
                If UpdateDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").InnerText <> "" Then
                    IntfileNo = CInt(UpdateDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").InnerText)
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ID").InnerText <> "" Then
                    strID = (UpdateDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ID").InnerText.Trim & "")
                Else
                    Throw (New AAMSException("File ID can't be blank."))
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERNO").InnerText <> "" Then
                    strORDERNO = (UpdateDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERNO").InnerText.Trim & "")
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("DOCTYPE").InnerText <> "" Then
                    strDOCTYPE = (UpdateDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("DOCTYPE").InnerText.Trim & "")
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("SEQUENCE").InnerText <> "" Then
                    strSEQUENCE = (UpdateDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("SEQUENCE").InnerText.Trim & "")
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERTYPE").InnerText <> "" Then
                    strORDERTYPE = (UpdateDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERTYPE").InnerText.Trim & "")
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_FILENO"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "U"

                    .Parameters.Add(New SqlParameter("@ID", SqlDbType.Int))
                    .Parameters("@ID").Value = strID

                    .Parameters.Add(New SqlParameter("@FILENO", SqlDbType.Int))
                    .Parameters("@FILENO").Value = IntfileNo

                    .Parameters.Add(New SqlParameter("@ORDERNO", SqlDbType.Char, 40))
                    .Parameters("@ORDERNO").Value = strORDERNO

                    .Parameters.Add(New SqlParameter("@DOCTYPE", SqlDbType.Int))
                    .Parameters("@DOCTYPE").Value = strDOCTYPE

                    .Parameters.Add(New SqlParameter("@SEQUENCE", SqlDbType.Int))
                    .Parameters("@SEQUENCE").Value = strSEQUENCE

                    .Parameters.Add(New SqlParameter("@ORDERTYPE", SqlDbType.Char, 200))
                    .Parameters("@ORDERTYPE").Value = strORDERTYPE

                    .Parameters.Add(New SqlParameter("@DOCUMENT", SqlDbType.Image))
                    .Parameters("@DOCUMENT").Value = DBNull.Value

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0

                    .Connection.Open()
                    intRecordsAffected = .ExecuteNonQuery()

                    intRetId = .Parameters("@RETURNID").Value

                    If intRetId = 0 Then
                        Throw (New AAMSException("Unable to Update!"))
                    Else
                        objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ID").InnerText = strID
                        objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").InnerText = IntfileNo
                        objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERNO").InnerText = strORDERNO
                        objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("DOCTYPE").InnerText = strDOCTYPE
                        objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("SEQUENCE").InnerText = strSEQUENCE
                        objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").InnerText = IntfileNo
                        objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ContentType").InnerText = strContentType
                        objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    End If
                End With

            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch exec As Exception
                'msgbox(exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function
        Public Function DeleteFile(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************            
            'Purpose: To Delete the Scanned File 
            'Input  : 
            '<TA_DELETEAGENCYFILENODETAILS_INPUT>
            '   <AGENCYFILE ID='' FILENO=''  ORDERNO ='' />
            '</TA_DELETEAGENCYFILENODETAILS_INPUT>

            'Output :  
            '<TA_DELETEAGENCYFILENODETAILS_OUTPUT>
            '   <AGENCYFILE ID='' FILENO=''  ORDERNO ='' />
            '<Errors Status=''>
            '<Error Code='' Description='' />
            '</Errors>
            '</TA_DELETEAGENCYFILENODETAILS_OUTPUT>
            '***********************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetDOCConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim IntfileNo As Integer
            Dim strID As Integer = 0
            Dim strORDERNO As String = ""

            Dim intRetId As Integer
            Dim intRecordsAffected As Integer

            Dim strMETHOD_NAME As String = "DeleteFile"

            objOutputXml.LoadXml(strUPDATEFilenoTransfer_OUTPUT)

            Try

                If UpdateDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ID").InnerText <> "" Then
                    strID = (UpdateDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ID").InnerText.Trim & "")
                Else
                    Throw (New AAMSException("File ID can't be blank."))
                End If


                If UpdateDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").InnerText <> "" Then
                    IntfileNo = CInt(UpdateDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").InnerText)
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERNO").InnerText <> "" Then
                    strORDERNO = (UpdateDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERNO").InnerText.Trim & "")
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_FILENO"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"

                    .Parameters.Add(New SqlParameter("@ID", SqlDbType.Int))
                    .Parameters("@ID").Value = strID

                    .Parameters.Add(New SqlParameter("@FILENO", SqlDbType.Int))
                    .Parameters("@FILENO").Value = IntfileNo

                    .Parameters.Add(New SqlParameter("@ORDERNO", SqlDbType.Char, 40))
                    .Parameters("@ORDERNO").Value = strORDERNO

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0


                    .Connection.Open()
                    intRecordsAffected = .ExecuteNonQuery()

                    intRetId = .Parameters("@RETURNID").Value

                    If intRetId = 0 Then
                        Throw (New AAMSException("Record has not been deleted!"))
                    Else
                        objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ID").InnerText = strID
                        objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").InnerText = IntfileNo
                        objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERNO").InnerText = strORDERNO
                        objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    End If
                End With

            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch exec As Exception
                'msgbox(exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function
        Public Function UpLoadFile(ByVal CmdfileObj As SqlCommand) As System.Xml.XmlDocument

            'Public Function UpLoadFile(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument

            '***********************************************************************            
            'Purpose: To Upload Documents
            'Input  : 
            '<TA_UPDATEAGENCYFILENODETAILS_INPUT>
            '   <AGENCYFILE ID ='' FILENO=''  ORDERNO ='' DOCTYPE ='' SEQUENCE='' ORDERTYPE='' DOCUMENT='' ContentType='' EmailFrom=''	EmailTo	=''	EmailSubject = '' EmailBody='' PDFDocFileName=''/>
            '</TA_UPDATEAGENCYFILENODETAILS_INPUT>

            'Output :  
            '<TA_UPDATEAGENCYFILENODETAILS_OUTPUT>
            '<AGENCYFILE ID='' FILENO='' ORDERNO ='' DOCTYPE ='' SEQUENCE='' ORDERTYPE='' ContentType='' EmailFrom=''	EmailTo	=''	EmailSubject = '' EmailBody='' NewPDFDocFileName=''/>
            '   <Errors Status=''>
            '       <Error Code='' Description='' />
            '   </Errors>
            '</TA_UPDATEAGENCYFILENODETAILS_OUTPUT>
            '***********************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetDOCConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim strFileNo As Integer
            Dim strID As Integer = 0
            Dim strORDERNO As String = ""
            Dim strDOCTYPE As Integer = 0
            Dim strSEQUENCE As Integer = 0
            Dim strORDERTYPE As String = ""

            Dim strContentType As Integer
            Dim strEmailFrom As String = ""
            Dim strEmailTo As String = ""
            Dim strEmailBody As String = ""
            Dim strEmailSubject As String = ""
            Dim strPDFDocFileName As String = ""
            Dim strNewPDFDocFileName As String = ""
            Dim strDOCUMENT() As Byte
            Dim intRetId As Integer
            Dim intRecordsAffected As Integer
            Dim intLCode As UInteger
            Dim strMETHOD_NAME As String = "UpLoadFile"

            objOutputXml.LoadXml(strUPDATEFilenoTransfer_OUTPUT)

            Try
                With CmdfileObj
                    'strID = Convert.ToString(.Parameters("@ID").Value)
                    intLCode = .Parameters("@LCODE").Value
                    If Not IsDBNull(.Parameters("@FILENO").Value) Then
                        strFileNo = .Parameters("@FILENO").Value
                    End If


                    strORDERNO = Convert.ToString(.Parameters("@ORDERNO").Value)
                    strDOCTYPE = Convert.ToString(.Parameters("@DOCTYPE").Value)
                    strSEQUENCE = Convert.ToInt16(.Parameters("@SEQUENCE").Value)

                    strContentType = Convert.ToInt16(.Parameters("@ContentType").Value)
                    strEmailFrom = Convert.ToString(.Parameters("@EmailFrom").Value)
                    strEmailTo = Convert.ToString(.Parameters("@EmailTo").Value)
                    strEmailTo = Convert.ToString(.Parameters("@EmailTo").Value)
                    strEmailSubject = Convert.ToString(.Parameters("@EmailSubject").Value)
                    strEmailBody = Convert.ToString(.Parameters("@EmailBody").Value)
                    strPDFDocFileName = Convert.ToString(.Parameters("@PDFDocFileName").Value)
                    strORDERTYPE = Convert.ToString(.Parameters("@ORDERTYPE").Value)

                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_FILENO"
                    .Connection = objSqlConnection
                    .Connection.Open()
                    intRecordsAffected = .ExecuteNonQuery()
                    intRetId = .Parameters("@RETURNID").Value

                End With

                If intRetId = 0 Then
                    Throw (New AAMSException("Unable to Insert!"))
                Else
                    If strContentType = 3 Then
                        strNewPDFDocFileName = CStr(intRetId) & ".pdf" ' Return NewPdfFineName AutoGenerated 
                    End If
                    If strContentType = 4 Then
                        strNewPDFDocFileName = CStr(intRetId) & ".jpg" ' Return NewPdfFineName AutoGenerated 
                    End If


                    objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ID").InnerText = intRetId
                    If strFileNo = 0 Then
                        objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").InnerText = ""
                    Else
                        objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").InnerText = strFileNo
                    End If

                    objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("LCode").InnerText = intLCode
                    objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERNO").InnerText = strORDERNO
                    objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("DOCTYPE").InnerText = strDOCTYPE
                    objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("SEQUENCE").InnerText = strSEQUENCE
                    objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERTYPE").InnerText = strORDERTYPE

                    objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ContentType").InnerText = strContentType
                    objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailFrom").InnerText = strEmailFrom
                    objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailTo").InnerText = strEmailTo
                    objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailSubject").InnerText = strEmailSubject
                    objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailBody").InnerText = strEmailBody
                    objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("NewPDFDocFileName").InnerText = strNewPDFDocFileName

                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                End If

            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch exec As Exception
                'msgbox(exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function
        Public Function UpdateStaffContactPerson(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************            
            'Purpose: To Update Staff Contact Person Into Location Master
            'Input  : 
            '<TA_UPDATEAGENCYSTAFFCONTACTPERSON_INPUT>
            '   <STAFF Location_code = '' Contact_Person_Id=''/>
            '</TA_UPDATEAGENCYSTAFFCONTACTPERSON_INPUT>

            'Output :  
            '<TA_UPDATEAGENCYSTAFFCONTACTPERSON_OUTPUT>
            '<STAFF Location_code = '' Contact_Person_Id='' />
            '   <Errors Status=''>
            '       <Error Code='' Description='' />
            '   </Errors>
            '</TA_UPDATEAGENCYSTAFFCONTACTPERSON_OUTPUT>
            '***********************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim IntLcode As Integer, IntStaffContactPersonId As Integer, intRetId As Integer, intRecordsAffected As Integer

            Dim strMETHOD_NAME As String = "UpdateStaffContactPerson"

            objOutputXml.LoadXml(strAgencyStaffContactPerson_OUTPUT)

            Try

                If UpdateDoc.DocumentElement.SelectSingleNode("STAFF").Attributes("Location_code").InnerText <> "" Then
                    IntLcode = CInt(UpdateDoc.DocumentElement.SelectSingleNode("STAFF").Attributes("Location_code").InnerText)
                End If

                If UpdateDoc.DocumentElement.SelectSingleNode("STAFF").Attributes("Contact_Person_Id").InnerText <> "" Then
                    IntStaffContactPersonId = CInt(UpdateDoc.DocumentElement.SelectSingleNode("STAFF").Attributes("Contact_Person_Id").InnerText)
                End If

                If Len(IntLcode) = 0 Then
                    Throw (New AAMSException("Location Code can't be blank."))
                End If


                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_STAFFCONTACTPERSON"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "U"

                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.Int))
                    .Parameters("@LCODE").Value = IntLcode

                    .Parameters.Add(New SqlParameter("@STAFFCONTACTPERSONID", SqlDbType.Int))
                    .Parameters("@STAFFCONTACTPERSONID").Value = IntStaffContactPersonId

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0


                    .Connection.Open()
                    intRecordsAffected = .ExecuteNonQuery()

                    intRetId = .Parameters("@RETURNID").Value

                    If intRetId = 0 Then
                        Throw (New AAMSException("Unable to Update!"))
                    Else
                        objOutputXml.DocumentElement.SelectSingleNode("STAFF").Attributes("Location_code").InnerText = IntLcode
                        objOutputXml.DocumentElement.SelectSingleNode("STAFF").Attributes("Contact_Person_Id").InnerText = IntStaffContactPersonId
                        objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    End If
                End With

            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch exec As Exception
                'msgbox(exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function
        Public Function AgencyGroup_Type(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the Agency Aoffice and its Type 
            'Input  : 
            '<UP_GETAGENCYGROUP_INPUT>
            '<LOCATION_CODE></LOCATION_CODE>
            '</UP_GETAGENCYGROUP_INPUT>

            'Output :  
            '<UP_GETAGENCYAOFFICE_OUTPUT>
            '<AGENCYGROUPTYPE LOCATION_CODE='' GroupTypeName='' AOffice='' CHAIN_CODE=''/>
            '<Errors Status=''>
            '<Error Code='' Description='' />
            '</Errors>
            '</UP_GETAGENCYAOFFICE_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strLCODE As String
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "AgencyGroup_Type"

            objOutputXml.LoadXml(strGETAGENCYAOFFICE_OUTPUT)

            Try

                If SearchDoc.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText <> "" Then
                    strLCODE = SearchDoc.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText
                Else
                    strLCODE = ""
                End If
                If strLCODE = "" Then
                    Throw (New AAMSException("Location Code can't be blank."))
                End If
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_GET_TA_AGENCYGROUP_TYPE"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@LOCATION_CODE", SqlDbType.Int))
                    .Parameters("@LOCATION_CODE").Value = strLCODE

                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AGENCYGROUPTYPE")
                If objSqlReader.HasRows Then
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.RemoveChild(objAptNode)
                End If

                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("LOCATION_CODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOCATION_CODE")))
                    objAptNodeClone.Attributes("GroupTypeID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("GroupTypeID")) & "")
                    objAptNodeClone.Attributes("AOffice").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AOffice")) & "")
                    objAptNodeClone.Attributes("CHAIN_CODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHAIN_CODE")) & "")

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop

                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                End If

            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch exec As Exception
                'msgbox(exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function
        Public Function ConvertDate(ByVal intDate As String) As String
            Try
                Dim dtDateFrom As String
                intDate = intDate.Trim()
                If intDate.Length = 8 Then
                    dtDateFrom = Right(CType(intDate, String), 2) & "/" & Mid(CType(intDate, String), 5, 2) & "/" & Left(CType(intDate, String), 4)
                End If
                Return dtDateFrom
            Catch ex As Exception
                Return CDate("1/1/1900")
            End Try
        End Function
        Public Function SendMail(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument

            'Purpose:  This function Sends the Mail Based on Criteria.
            'Input :
            '<SENDMAILIMMEDIATE_INPUT>
            '   <MAIL_DETAILS DESTINATION_TO='' SUBJECT='' MESSAGE='' SOURCE='' DESTINATION_CC='' DESTINATION_BCC=''/>
            '</SENDMAILIMMEDIATE_INPUT>

            'Input:      
            '<TA_SENDMAILAGENCY_INPUT>
            '   <AGENCYFILE ID ='' FILENO='' ORDERNO ='' DOCTYPE ='' ORDERTYPE='' ContentType='' EmailFrom='' EmailTo=''  EmailSubject ='' EmailBody='' />
            '</ TA_SENDMAILAGENCY_INPUT >

            'Output:     
            '<TA_SENDMAILAGENCY_OUTPUT>
            '<AGENCYFILE ID='' FILENO='' ORDERNO ='' DOCTYPE ='' SEQUENCE='' ORDERTYPE='' ContentType='' EmailFrom='' EmailTo  ='' EmailSubject =''  EmailBody=''  EmailStatus=''/>
            '   <Errors Status=''>
            '        <Error Code='' Description='' />
            '   </Errors>
            '</ TA_SENDMAILAGENCY_OUTPUT >
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim ObjSendMail As bizUtility.bzEmail
            Dim objbzAgency As bizTravelAgency.bzAgency
            Dim objSendMailOutputXml As New XmlDocument
            Dim objOutputXml As New XmlDocument
            Dim objInputSendMailXml As New XmlDocument
            Dim objSqlCommand As SqlCommand
            Dim objUpdateSentDateSqlCommand As SqlCommand
            Dim intRecordsAffected As Integer
            Dim intRetId As Integer

            Dim boolSendMailStatus As Boolean
            Dim strorderNumber As String = ""
            Dim strDOCTYPE As Integer
            Dim strORDERTYPE As String = ""
            Dim strEmailTo As String = ""
            Dim strEmailFrom As String = ""
            Dim strEmailSubject As String = ""
            Dim strEmailBody As String = ""
            Dim strFileNo As Integer = 0
            Dim intLCode As UInteger
            Dim strOrderid As String = ""
            Dim blnIsISPpage As Boolean
            Dim strMETHOD_NAME As String = "SendMail"

            objSendMailOutputXml.LoadXml(strAgencySendMail_OUTPUT)
            objInputSendMailXml.LoadXml(strSendMail_INPUT)

            Try
                If SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("LCode").InnerText <> "" Then
                    intLCode = SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("LCode").InnerText
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERID").InnerText <> "" Then
                    strOrderid = SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERID").InnerText
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ISISPORDER").InnerText.ToUpper = "TRUE" Then
                    blnIsISPpage = True
                Else
                    blnIsISPpage = False
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").InnerText <> "" Then
                    strFileNo = SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").InnerText
                End If
                strorderNumber = SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERNO").InnerText.Trim
                strDOCTYPE = SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("DOCTYPE").InnerText
                strORDERTYPE = SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERTYPE").InnerText

                strEmailFrom = SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailFrom").InnerText
                strEmailTo = SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailTo").InnerText
                strEmailSubject = SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailSubject").InnerText
                strEmailBody = SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailBody").InnerText

                With objInputSendMailXml.SelectSingleNode("SENDMAILIMMEDIATE_INPUT/MAIL_DETAILS")
                    .Attributes("SOURCE").InnerText = strEmailFrom
                    .Attributes("DESTINATION_TO").InnerText = strEmailTo
                    .Attributes("SUBJECT").InnerText = strEmailSubject
                    .Attributes("MESSAGE").InnerText = strEmailBody
                End With

                ObjSendMail = New bizUtility.bzEmail
                boolSendMailStatus = ObjSendMail.SendMail(objInputSendMailXml)

                If boolSendMailStatus = True Then
                    '* Update Mail Sent Date into Selected Order
                    objUpdateSentDateSqlCommand = New SqlCommand
                    With objUpdateSentDateSqlCommand
                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "UP_SRO_TA_UPDATEORDERSENTMAIL"
                        .Connection = objSqlConnection

                        .Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1))
                        .Parameters("@Action").Value = "U"

                        .Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.VarChar, 40))
                        .Parameters("@OrderNo").Value = strorderNumber

                        .Parameters.Add(New SqlParameter("@ORDERID", SqlDbType.BigInt))
                        If strOrderid = "" Then
                            .Parameters("@ORDERID").Value = DBNull.Value
                        Else
                            .Parameters("@ORDERID").Value = strOrderid
                        End If
                        .Parameters.Add(New SqlParameter("@ISISPORDER", SqlDbType.Bit))
                        If blnIsISPpage = True Then
                            .Parameters("@ISISPORDER").Value = 1
                        Else
                            .Parameters("@ISISPORDER").Value = 0
                        End If

                        .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                        .Parameters("@RETURNID").Direction = ParameterDirection.Output
                        .Parameters("@RETURNID").Value = 0

                        .Connection.Open()
                        intRecordsAffected = .ExecuteNonQuery()
                        intRetId = .Parameters("@RETURNID").Value

                    End With

                    If intRecordsAffected = 1 Or intRetId = 1 Then
                        objSqlCommand = New SqlCommand
                        With objSqlCommand

                            .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                            .Parameters("@ACTION").Value = "I"

                            .Parameters.Add(New SqlParameter("@ID", SqlDbType.Char, 1))
                            .Parameters("@ID").Value = ""

                            .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.BigInt))
                            .Parameters("@LCODE").Value = intLCode

                            .Parameters.Add(New SqlParameter("@FILENO", SqlDbType.Int))
                            If strFileNo = 0 Then
                                .Parameters("@FILENO").Value = DBNull.Value
                            Else
                                .Parameters("@FILENO").Value = strFileNo
                            End If


                            .Parameters.Add(New SqlParameter("@ORDERNO", SqlDbType.Char, 40))
                            .Parameters("@ORDERNO").Value = strorderNumber

                            .Parameters.Add(New SqlParameter("@DOCTYPE", SqlDbType.Int))
                            .Parameters("@DOCTYPE").Value = strDOCTYPE ' Always 2 Case Order

                            .Parameters.Add(New SqlParameter("@SEQUENCE", SqlDbType.Int))
                            .Parameters("@SEQUENCE").Value = 0

                            .Parameters.Add(New SqlParameter("@ORDERTYPE", SqlDbType.Char, 200))
                            .Parameters("@ORDERTYPE").Value = strORDERTYPE

                            .Parameters.Add(New SqlParameter("@DOCUMENT", SqlDbType.Image))
                            .Parameters("@DOCUMENT").Value = DBNull.Value

                            .Parameters.Add(New SqlParameter("@ContentType", SqlDbType.Int))
                            .Parameters("@ContentType").Value = 2

                            .Parameters.Add(New SqlParameter("@EmailFrom", SqlDbType.VarChar, 100))
                            .Parameters("@EmailFrom").Value = strEmailFrom

                            .Parameters.Add(New SqlParameter("@EmailTo", SqlDbType.VarChar, 3000))
                            .Parameters("@EmailTo").Value = strEmailTo

                            .Parameters.Add(New SqlParameter("@EmailSubject", SqlDbType.VarChar, 500))
                            .Parameters("@EmailSubject").Value = strEmailSubject

                            .Parameters.Add(New SqlParameter("@EmailBody", SqlDbType.VarChar, 8000))
                            .Parameters("@EmailBody").Value = strEmailBody

                            .Parameters.Add(New SqlParameter("@PDFDocFileName", SqlDbType.VarChar, 100))
                            .Parameters("@PDFDocFileName").Value = ""

                            .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                            .Parameters("@RETURNID").Direction = ParameterDirection.Output

                            .Parameters("@RETURNID").Value = 0

                        End With

                        objbzAgency = New bizTravelAgency.bzAgency
                        objOutputXml = objbzAgency.UpLoadFile(objSqlCommand)


                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ID").InnerText = objOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ID").InnerText
                            If strFileNo = 0 Then
                                objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").InnerText = ""
                            Else
                                objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").InnerText = strFileNo
                            End If

                            objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERNO").InnerText = strorderNumber
                            objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("DOCTYPE").InnerText = strDOCTYPE
                            objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERTYPE").InnerText = strORDERTYPE
                            objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ContentType").InnerText = "2"
                            objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailFrom").InnerText = strEmailFrom
                            objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailTo").InnerText = strEmailTo
                            objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailSubject").InnerText = strEmailSubject
                            objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailBody").InnerText = strEmailBody
                            objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailStatus").InnerText = "True"
                            objSendMailOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                        Else
                            objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ID").InnerText = ""
                            'objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").InnerText = strFileNo
                            If strFileNo = 0 Then
                                objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").InnerText = ""
                            Else
                                objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").InnerText = strFileNo
                            End If
                            objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERNO").InnerText = strorderNumber
                            objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("DOCTYPE").InnerText = strDOCTYPE
                            objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERTYPE").InnerText = strORDERTYPE
                            objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ContentType").InnerText = "2"
                            objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailFrom").InnerText = strEmailFrom
                            objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailTo").InnerText = strEmailTo
                            objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailSubject").InnerText = strEmailSubject
                            objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailBody").InnerText = strEmailBody
                            objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailStatus").InnerText = "False"
                            objSendMailOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "TRUE"
                            Throw (New AAMSException("Error While Sending Mail"))
                        End If
                    End If
                Else
                    objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ID").InnerText = ""
                    'objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").InnerText = strFileNo
                    If strFileNo = 0 Then
                        objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").InnerText = ""
                    Else
                        objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").InnerText = strFileNo
                    End If
                    objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERNO").InnerText = strorderNumber
                    objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("DOCTYPE").InnerText = strDOCTYPE
                    objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERTYPE").InnerText = strORDERTYPE
                    objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ContentType").InnerText = "2"
                    objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailFrom").InnerText = strEmailFrom
                    objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailTo").InnerText = strEmailTo
                    objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailSubject").InnerText = strEmailSubject
                    objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailBody").InnerText = strEmailBody
                    objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailStatus").InnerText = "False"
                    objSendMailOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "TRUE"
                    Throw (New AAMSException("Error While Sending Mail"))
                End If


            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objSendMailOutputXml, "101", Exec.Message)
                Return objSendMailOutputXml
            Catch exec As Exception
                'msgbox(exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bzShared.FillErrorStatus(objSendMailOutputXml, "103", exec.Message)
                Return objSendMailOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
            End Try
            Return objSendMailOutputXml

        End Function
        Public Function AgencyList(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            'Purpose:This function gives search results based on choosen search criterion.
            'Input  :- 
            '<RP_AGENCY_INPUT>
            '   <AGENCY NAME='' LOCATION_SHORT_NAME='' City_Name='' Country_Name='' StatusCode='' Aoffice='' OFFICEID='' Crs='' ADDRESS='' AgencyStatusId='' AgencyTypeId='' EMAIL='' DATE_ONLINE='' DATE_OFFLINE='' FAX='' FILENO=''
            '   IATA_TID='' EmployeeID='' Limited_To_Aoffice='' Limited_To_Region='' Limited_To_OwnAagency='' SecurityRegionID='' > 
            '</AGENCY></RP_AGENCY_INPUT>

            'Output:
            '<RP_AGENCY_OUTPUT>
            '   <AGENCY LCODE='' CHAIN_CODE='' CHAIN_NAME='' NAME='' ADDRESS='' CITY='' COUNTRY='' OFFICEID='' AOFFICE='' RESP_1A='' PRIMARY_CONECTIVITY='' INSTALLATIONDATE='' EMAIL='' PHONE='' FAX='' AGENCYSTATUS_NAME='' IATA_TID='' WWW_ADDRESS='' AGENCYTYPE='' PANNO='' STAFFNAME=''/>
            '   </AGENCY>
            '</RP_AGENCY_OUTPUT>
            '-----------------------------------------------------------------------------

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean

            Dim strAgencyName As String = vbNullString, strLocationShortName As String = vbNullString, strCity As String = vbNullString, strCountry As String = vbNullString, strOnlineStatus As String = vbNullString, strAoffice As String = vbNullString, strOfficeID As String = vbNullString, strCrs As String = vbNullString, strAddress As String = vbNullString, strEmail As String = vbNullString, strFax As String = vbNullString, strIATAID As String = vbNullString, strLimitedToAoffice As String = vbNullString
            Dim intFileNo, intAgencystatusID, intAgencyTypeID, intLimitedToRegion, intLimitedToOwnAgency As Integer, intRESP_1A As Integer
            Dim intDateOnline As Integer, intDateOffline As Integer
            Dim strEmployeeID As String = ""
            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strRP_AGENCY_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                With SearchDoc.DocumentElement.SelectSingleNode("AGENCY")
                    If .Attributes("NAME").InnerText.Trim() <> "" Then
                        strAgencyName = .Attributes("NAME").InnerText.Trim()
                    End If
                    If .Attributes("LOCATION_SHORT_NAME").InnerText.Trim() <> "" Then
                        strLocationShortName = .Attributes("LOCATION_SHORT_NAME").InnerText.Trim()
                    End If
                    If .Attributes("City_Name").InnerText.Trim() <> "" Then
                        strCity = .Attributes("City_Name").InnerText.Trim()
                    End If
                    If .Attributes("Country_Name").InnerText.Trim() <> "" Then
                        strCountry = .Attributes("Country_Name").InnerText.Trim()
                    End If
                    If .Attributes("StatusCode").InnerText.Trim() <> "" Then
                        strOnlineStatus = .Attributes("StatusCode").InnerText.Trim() ''Doubt
                    End If
                    If .Attributes("Aoffice").InnerText.Trim() <> "" Then
                        strAoffice = .Attributes("Aoffice").InnerText.Trim()
                    End If
                    If .Attributes("OFFICEID").InnerText.Trim() <> "" Then
                        strOfficeID = .Attributes("OFFICEID").InnerText.Trim()
                    End If
                    If .Attributes("Crs").InnerText.Trim() <> "" Then
                        strCrs = .Attributes("Crs").InnerText.Trim()
                    End If
                    If .Attributes("ADDRESS").InnerText.Trim() <> "" Then
                        strAddress = .Attributes("ADDRESS").InnerText.Trim()
                    End If
                    If .Attributes("AgencyStatusId").InnerText.Trim() = "" Then
                        intAgencystatusID = 0
                    Else
                        intAgencystatusID = .Attributes("AgencyStatusId").InnerText.Trim()
                    End If
                    If .Attributes("AgencyTypeId").InnerText.Trim() = "" Then
                        intAgencyTypeID = 0
                    Else
                        intAgencyTypeID = .Attributes("AgencyTypeId").InnerText.Trim()
                    End If
                    If .Attributes("EMAIL").InnerText.Trim() <> "" Then
                        strEmail = .Attributes("EMAIL").InnerText.Trim()
                    End If
                    If .Attributes("DATE_ONLINE").InnerText.Trim() <> "" Then
                        intDateOnline = .Attributes("DATE_ONLINE").InnerText.Trim()
                    End If
                    If .Attributes("DATE_OFFLINE").InnerText.Trim() <> "" Then
                        intDateOffline = .Attributes("DATE_OFFLINE").InnerText.Trim()
                    End If
                    If .Attributes("FAX").InnerText.Trim() <> "" Then
                        strFax = .Attributes("FAX").InnerText.Trim()
                    End If

                    If .Attributes("FILENO").InnerText.Trim() = "" Then
                        intFileNo = 0
                    Else
                        intFileNo = .Attributes("FILENO").InnerText.Trim()
                    End If
                    If .Attributes("IATA_TID").InnerText.Trim() <> "" Then
                        strIATAID = .Attributes("IATA_TID").InnerText.Trim()
                    End If

                    If .Attributes("EmployeeID").InnerText.Trim() <> "" Then
                        intRESP_1A = .Attributes("EmployeeID").InnerText.Trim()
                    End If
                    If .Attributes("Limited_To_Aoffice").InnerText.Trim() <> "" Then
                        strLimitedToAoffice = .Attributes("Limited_To_Aoffice").InnerText.Trim()
                    End If
                    If .Attributes("Limited_To_Region").InnerText.Trim() = "" Then
                        intLimitedToRegion = 0
                    Else
                        intLimitedToRegion = .Attributes("Limited_To_Region").InnerText.Trim()
                    End If
                    If .Attributes("Limited_To_OwnAagency").InnerText.Trim() = "" Then
                        intLimitedToOwnAgency = 0
                    Else
                        intLimitedToOwnAgency = .Attributes("Limited_To_OwnAagency").InnerText.Trim()
                    End If
                End With
                Dim strCOMP_VERTICAL As String = ""
                If SearchDoc.DocumentElement.SelectSingleNode("COMP_VERTICAL") IsNot Nothing Then
                    If SearchDoc.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText.Trim() <> "" Then
                        strCOMP_VERTICAL = SearchDoc.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText.Trim()
                    End If
                End If

                '--------Retrieving & Checking Details from Input XMLDocument ------End
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_RPT_AGENCYLIST"
                    .Connection = objSqlConnection
                    .Parameters.Add("@NAME", SqlDbType.VarChar, 200)
                    .Parameters("@NAME").Value = strAgencyName

                    .Parameters.Add("@LOCATION_SHORT_NAME", SqlDbType.VarChar, 50)
                    .Parameters("@LOCATION_SHORT_NAME").Value = strLocationShortName

                    .Parameters.Add("@CITY", SqlDbType.VarChar, 80)
                    .Parameters("@CITY").Value = strCity

                    .Parameters.Add("@COUNTRY", SqlDbType.VarChar, 50)
                    .Parameters("@COUNTRY").Value = strCountry

                    .Parameters.Add("@ONLINE_STATUS", SqlDbType.VarChar, 20)
                    .Parameters("@ONLINE_STATUS").Value = strOnlineStatus

                    .Parameters.Add("@AOFFICE", SqlDbType.VarChar, 3)
                    .Parameters("@AOFFICE").Value = strAoffice

                    .Parameters.Add("@OFFICEID", SqlDbType.VarChar, 10)
                    .Parameters("@OFFICEID").Value = strOfficeID

                    .Parameters.Add("@CRS", SqlDbType.VarChar, 5)
                    .Parameters("@CRS").Value = strCrs

                    .Parameters.Add("@ADDRESS", SqlDbType.VarChar, 200)
                    .Parameters("@ADDRESS").Value = strAddress

                    .Parameters.Add("@AGENCYSTATUSID", SqlDbType.Int)
                    If intAgencystatusID = 0 Then
                        .Parameters("@AGENCYSTATUSID").Value = DBNull.Value
                    Else
                        .Parameters("@AGENCYSTATUSID").Value = intAgencystatusID
                    End If

                    .Parameters.Add("@AGENCYTYPEID", SqlDbType.Int)
                    If intAgencyTypeID = 0 Then
                        .Parameters("@AGENCYTYPEID").Value = DBNull.Value
                    Else
                        .Parameters("@AGENCYTYPEID").Value = intAgencyTypeID
                    End If
                    .Parameters.Add("@EMAIL", SqlDbType.VarChar, 80)
                    .Parameters("@EMAIL").Value = strEmail

                    .Parameters.Add("@DATE_ONLINE", SqlDbType.Int)
                    If intDateOnline = 0 Then
                        .Parameters("@DATE_ONLINE").Value = DBNull.Value
                    Else
                        .Parameters("@DATE_ONLINE").Value = intDateOnline
                    End If

                    .Parameters.Add("@DATE_OFFLINE", SqlDbType.Int)
                    If intDateOffline = 0 Then
                        .Parameters("@DATE_OFFLINE").Value = DBNull.Value
                    Else
                        .Parameters("@DATE_OFFLINE").Value = intDateOffline
                    End If

                    .Parameters.Add("@FAX", SqlDbType.VarChar, 20)
                    .Parameters("@FAX").Value = strFax

                    .Parameters.Add("@FILENO", SqlDbType.Int)
                    If intFileNo = 0 Then
                        .Parameters("@FILENO").Value = DBNull.Value
                    Else
                        .Parameters("@FILENO").Value = intFileNo
                    End If

                    .Parameters.Add("@IATA_TID", SqlDbType.VarChar, 20)
                    .Parameters("@IATA_TID").Value = strIATAID

                    .Parameters.Add("@RESP_1A", SqlDbType.Int)
                    If intRESP_1A = 0 Then
                        .Parameters("@RESP_1A").Value = DBNull.Value
                    Else
                        .Parameters("@RESP_1A").Value = intRESP_1A
                    End If

                    .Parameters.Add("@LIMITED_TO_AOFFICE", SqlDbType.Char, 3)
                    .Parameters("@LIMITED_TO_AOFFICE").Value = strLimitedToAoffice

                    .Parameters.Add("@LIMITED_TO_REGION", SqlDbType.Int)
                    If intLimitedToRegion = 0 Then
                        .Parameters("@LIMITED_TO_REGION").Value = DBNull.Value
                    Else
                        .Parameters("@LIMITED_TO_REGION").Value = intLimitedToRegion
                    End If

                    .Parameters.Add("@LIMITED_TO_OWNAAGENCY", SqlDbType.Int) 'Location_code
                    If intLimitedToOwnAgency = 0 Then
                        .Parameters("@LIMITED_TO_OWNAAGENCY").Value = DBNull.Value
                    Else
                        .Parameters("@LIMITED_TO_OWNAAGENCY").Value = intLimitedToOwnAgency
                    End If

                    .Parameters.Add("@COMP_VERTICAL", SqlDbType.SmallInt)
                    If strCOMP_VERTICAL = "" Then
                        .Parameters("@COMP_VERTICAL").Value = DBNull.Value
                    Else
                        .Parameters("@COMP_VERTICAL").Value = Val(strCOMP_VERTICAL)
                    End If

                End With


                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument

                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AGENCY")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("LCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")) & "")
                    objAptNodeClone.Attributes("CHAIN_CODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHAIN_CODE")) & "")
                    objAptNodeClone.Attributes("CHAIN_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHAIN_NAME")) & "")
                    objAptNodeClone.Attributes("NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NAME")) & "")
                    objAptNodeClone.Attributes("ADDRESS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ADDRESS")) & "")
                    objAptNodeClone.Attributes("CITY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITY")) & "")
                    objAptNodeClone.Attributes("COUNTRY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("COUNTRY")) & "")
                    objAptNodeClone.Attributes("OFFICEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OFFICEID")) & "")
                    objAptNodeClone.Attributes("AOFFICE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AOFFICE")) & "")
                    objAptNodeClone.Attributes("RESP_1A").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("RESP_1A")) & "")
                    objAptNodeClone.Attributes("PRIMARY_CONECTIVITY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRIMARY_CONECTIVITY")) & "")
                    objAptNodeClone.Attributes("INSTALLATIONDATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("INSTALLATIONDATE")) & "")
                    objAptNodeClone.Attributes("EMAIL").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("EMAIL")) & "")
                    objAptNodeClone.Attributes("PHONE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PHONE")) & "")
                    objAptNodeClone.Attributes("FAX").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("FAX")) & "")
                    objAptNodeClone.Attributes("AGENCYSTATUS_NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AGENCY_STATUS_NAME")) & "")
                    objAptNodeClone.Attributes("IATA_TID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("IATA_TID")) & "")
                    objAptNodeClone.Attributes("WWW_ADDRESS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("WWW_ADDRESS")) & "")
                    objAptNodeClone.Attributes("AGENCYTYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AGENCY_TYPE_NAME")) & "")
                    objAptNodeClone.Attributes("PANNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PANNO")) & "")
                    objAptNodeClone.Attributes("STAFFNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("STAFFNAME")) & "")

                    strCOMP_VERTICAL = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("COMP_VERTICAL")) & "")
                    objAptNodeClone.Attributes("COMP_VERTICAL").InnerText = IIf(strCOMP_VERTICAL = "1", "Amadeus", IIf(strCOMP_VERTICAL = "2", "ResBird", (IIf(strCOMP_VERTICAL = "3", "Non1A", ""))))

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
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
                'msgbox(Exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function
        Public Function OrderFeasibility(ByVal IndexDoc As System.Xml.XmlDocument)
            'Purpose:This function gives details of Agency.
            'Input  :
            '<MS_VIEWAGENCYORDERFEASIBILITY_INPUT>
            '	<LOCATION_CODE></LOCATION_CODE>
            '   <USEORIGINAL></USEORIGINAL>
            '</MS_VIEWAGENCYORDERFEASIBILITY_INPUT>

            'Output :
            '<MS_VIEWAGENCYORDERFEASIBILITY_INPUT>
            '	<AGENCYORDERFEASIBILITY LOCATION_CODE=''  NAME='' ADDRESS='' CITY='' COUNTRY='' ONLINE_STATUS='' IATA_TID=''>
            '   <PRODUCTIVITYDETAILS MONTH='' PRODUCTIVITY='' />
            '   <CRSDETAILS CRS='' AVERAGE='' />
            '   <HARDWAREDETAILS PC1A='' AGENCYPC='' HCPRINTER='' TICKETPRINTER='' />
            '   <SUMMARY LAST12MONTH1A_AVERAGE='' BEFORE_LAST12MONTH1A_AVERAGE='' TOTALPOTENTIAL='' TOTALREQUIREMENT='' />
            '   </AGENCYORDERFEASIBILITY>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_VIEWAGENCYORDERFEASIBILITY_INPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim intLocationCode As Integer
            Dim blnUseOriginal As Boolean
            Dim objAptNode As XmlNode, objAptNodeClone As XmlNode
            Const strMETHOD_NAME As String = "OrderFeasibility"
            Try
                objOutputXml.LoadXml(strOrderFeasibility_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                intLocationCode = Integer.Parse(IndexDoc.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText.Trim)
                If IndexDoc.DocumentElement.SelectSingleNode("USEORIGINAL").InnerText.Trim.ToUpper = "TRUE" Then
                    blnUseOriginal = True
                Else
                    blnUseOriginal = False
                End If

                If intLocationCode = 0 Then
                    Throw (New AAMSException("Incomplete Parameters"))
                End If
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_RPT_C_L_OFR"
                    .Connection = objSqlConnection
                    .Parameters.Add("@LCODE", SqlDbType.Int)
                    .Parameters("@LCODE").Value = intLocationCode

                    .Parameters.Add("@USEORIGINAL", SqlDbType.Char, 1)
                    If blnUseOriginal = True Then
                        .Parameters("@USEORIGINAL").Value = "Y"
                    Else
                        .Parameters("@USEORIGINAL").Value = "N"
                    End If

                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                Dim blnRecordFound As Boolean

                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AGENCYORDERFEASIBILITY/CRSDETAILS")
                objAptNodeClone = objAptNode.CloneNode(True)
                If objSqlReader.HasRows = True Then objOutputXml.DocumentElement.SelectSingleNode("AGENCYORDERFEASIBILITY").RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("CRS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CRSCODETEXT")) & "")
                    objAptNodeClone.Attributes("AVERAGE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRODUCTIVITY")) & "")

                    objOutputXml.DocumentElement.SelectSingleNode("AGENCYORDERFEASIBILITY").AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop
                objSqlReader.NextResult()

                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AGENCYORDERFEASIBILITY/PRODUCTIVITYDETAILS")
                objAptNodeClone = objAptNode.CloneNode(True)
                If objSqlReader.HasRows = True Then objOutputXml.DocumentElement.SelectSingleNode("AGENCYORDERFEASIBILITY").RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("MONTH").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MONTH")) & "")
                    objAptNodeClone.Attributes("PRODUCTIVITY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRODUCTIVITY")) & "")

                    objOutputXml.DocumentElement.SelectSingleNode("AGENCYORDERFEASIBILITY").AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop

                objSqlReader.NextResult()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("AGENCYORDERFEASIBILITY/HARDWAREDETAILS")
                        blnRecordFound = True
                        .Attributes("PC1A").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PCCOUNT")) & "")
                        .Attributes("AGENCYPC").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("APCCOUNT")) & "")
                        .Attributes("HCPRINTER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DMPCOUNT")) & "")
                        .Attributes("TICKETPRINTER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("TKTCOUNT")) & "")
                    End With
                    With objOutputXml.DocumentElement.SelectSingleNode("AGENCYORDERFEASIBILITY/SUMMARY")
                        .Attributes("LAST12MONTH1A_AVERAGE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CAVGERAGE")) & "")
                        .Attributes("BEFORE_LAST12MONTH1A_AVERAGE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LAVGERAGE")) & "")
                        .Attributes("TOTALPOTENTIAL").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("TOTALPOTENTIAL")) & "")
                        .Attributes("TOTALREQUIREMENT").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("REQUREDSEGMENTS")) & "")
                    End With
                Loop

                objSqlReader.NextResult()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("AGENCYORDERFEASIBILITY")
                        blnRecordFound = True
                        .Attributes("LOCATION_CODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOCATION_CODE")) & "")
                        .Attributes("NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NAME")) & "")
                        .Attributes("ADDRESS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ADDRESS")) & "")
                        .Attributes("CITY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITY")) & "")
                        .Attributes("COUNTRY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("COUNTRY")) & "")
                        .Attributes("ONLINE_STATUS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ONLINE_STATUS")) & "")
                        .Attributes("IATA_TID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("IATA_TID")) & "")
                    End With
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
                'msgbox(Exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function
        Public Function SendMailWeb(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument

            'Purpose:  This function Sends the Mail Based on Criteria.
            'Input :
            '<SENDMAILIMMEDIATE_INPUT>
            '   <MAIL_DETAILS DESTINATION_TO='' SUBJECT='' MESSAGE='' SOURCE='' DESTINATION_CC='' DESTINATION_BCC=''/>
            '</SENDMAILIMMEDIATE_INPUT>

            'Input:      
            '<TA_SENDMAILAGENCY_INPUT>
            '   <AGENCYFILE ID ='' FILENO='' ORDERNO ='' DOCTYPE ='' ORDERTYPE='' ContentType='' EmailFrom='' EmailTo=''  EmailSubject ='' EmailBody='' />
            '</ TA_SENDMAILAGENCY_INPUT >

            'Output:     
            '<TA_SENDMAILAGENCY_OUTPUT>
            '<AGENCYFILE ID='' FILENO='' ORDERNO ='' DOCTYPE ='' SEQUENCE='' ORDERTYPE='' ContentType='' EmailFrom='' EmailTo  ='' EmailSubject =''  EmailBody=''  EmailStatus=''/>
            '   <Errors Status=''>
            '        <Error Code='' Description='' />
            '   </Errors>
            '</ TA_SENDMAILAGENCY_OUTPUT >
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim ObjSendMail As bizUtility.bzEmail
            Dim objbzAgency As bizTravelAgency.bzAgency
            Dim objSendMailOutputXml As New XmlDocument
            Dim objOutputXml As New XmlDocument
            Dim objInputSendMailXml As New XmlDocument
            Dim objSqlCommand As SqlCommand
            Dim objUpdateSentDateSqlCommand As SqlCommand
            Dim intRecordsAffected As Integer
            Dim intRetId As Integer

            Dim boolSendMailStatus As Boolean
            Dim strorderNumber As String = ""
            Dim strDOCTYPE As Integer
            Dim strORDERTYPE As String = ""
            Dim strEmailTo As String = ""
            Dim strEmailFrom As String = ""
            Dim strEmailSubject As String = ""
            Dim strEmailBody As String = ""
            Dim strFileNo As Integer = 0
            Dim intLCode As UInteger
            Dim strOrderid As String = ""
            Dim blnIsISPpage As Boolean
            Dim objTran As SqlTransaction
            Dim strMETHOD_NAME As String = "SendMail"

            objSendMailOutputXml.LoadXml(strAgencySendMail_OUTPUT)
            objInputSendMailXml.LoadXml(strSendMail_INPUT)

            Try
                If SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("LCode").InnerText <> "" Then
                    intLCode = SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("LCode").InnerText
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERID").InnerText <> "" Then
                    strOrderid = SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERID").InnerText
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ISISPORDER").InnerText.ToUpper = "TRUE" Then
                    blnIsISPpage = True
                Else
                    blnIsISPpage = False
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").InnerText <> "" Then
                    strFileNo = SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").InnerText
                End If
                strorderNumber = SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERNO").InnerText.Trim
                strDOCTYPE = SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("DOCTYPE").InnerText
                strORDERTYPE = SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERTYPE").InnerText

                strEmailFrom = SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailFrom").InnerText
                strEmailTo = SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailTo").InnerText
                strEmailSubject = SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailSubject").InnerText
                strEmailBody = SearchDoc.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailBody").InnerText

                objUpdateSentDateSqlCommand = New SqlCommand
                With objUpdateSentDateSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_TA_UPDATEORDERSENTMAILWEB"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1))
                    .Parameters("@Action").Value = "U"

                    .Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.VarChar, 40))
                    .Parameters("@OrderNo").Value = strorderNumber

                    .Parameters.Add(New SqlParameter("@ORDERID", SqlDbType.BigInt))
                    If strOrderid = "" Then
                        .Parameters("@ORDERID").Value = DBNull.Value
                    Else
                        .Parameters("@ORDERID").Value = strOrderid
                    End If
                    .Parameters.Add(New SqlParameter("@ISISPORDER", SqlDbType.Bit))
                    If blnIsISPpage = True Then
                        .Parameters("@ISISPORDER").Value = 1
                    Else
                        .Parameters("@ISISPORDER").Value = 0
                    End If

                    .Parameters.Add(New SqlParameter("@ID", SqlDbType.Char, 1))
                    .Parameters("@ID").Value = ""

                    .Parameters.Add(New SqlParameter("@LCODE", SqlDbType.BigInt))
                    .Parameters("@LCODE").Value = intLCode

                    .Parameters.Add(New SqlParameter("@FILENO", SqlDbType.Int))
                    If strFileNo = 0 Then
                        .Parameters("@FILENO").Value = DBNull.Value
                    Else
                        .Parameters("@FILENO").Value = strFileNo
                    End If

                    .Parameters.Add(New SqlParameter("@DOCTYPE", SqlDbType.Int))
                    .Parameters("@DOCTYPE").Value = strDOCTYPE ' Always 2 Case Order

                    .Parameters.Add(New SqlParameter("@SEQUENCE", SqlDbType.Int))
                    .Parameters("@SEQUENCE").Value = 0

                    .Parameters.Add(New SqlParameter("@ORDERTYPE", SqlDbType.Char, 200))
                    .Parameters("@ORDERTYPE").Value = strORDERTYPE

                    .Parameters.Add(New SqlParameter("@ContentType", SqlDbType.Int))
                    .Parameters("@ContentType").Value = 2

                    .Parameters.Add(New SqlParameter("@EmailFrom", SqlDbType.VarChar, 1000))
                    .Parameters("@EmailFrom").Value = strEmailFrom

                    .Parameters.Add(New SqlParameter("@EmailTo", SqlDbType.VarChar, 8000))
                    .Parameters("@EmailTo").Value = strEmailTo

                    .Parameters.Add(New SqlParameter("@EmailSubject", SqlDbType.VarChar, 500))
                    .Parameters("@EmailSubject").Value = strEmailSubject

                    .Parameters.Add(New SqlParameter("@EmailBody", SqlDbType.VarChar, 8000))
                    .Parameters("@EmailBody").Value = strEmailBody

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0

                    .Connection.Open()

                    objTran = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)
                    objUpdateSentDateSqlCommand.Transaction = objTran

                    intRecordsAffected = .ExecuteNonQuery()
                    intRetId = .Parameters("@RETURNID").Value
                End With
                'AFTER UPDATE
                If intRetId = 0 Then
                    Throw (New AAMSException("Error While Sending Mail"))
                End If
                objTran.Commit()
                objTran = Nothing

                With objInputSendMailXml.SelectSingleNode("SENDMAILIMMEDIATE_INPUT/MAIL_DETAILS")
                    .Attributes("SOURCE").InnerText = strEmailFrom
                    .Attributes("DESTINATION_TO").InnerText = strEmailTo
                    .Attributes("SUBJECT").InnerText = strEmailSubject
                    .Attributes("MESSAGE").InnerText = strEmailBody
                End With
                ObjSendMail = New bizUtility.bzEmail
                boolSendMailStatus = ObjSendMail.SendMail(objInputSendMailXml)
                If boolSendMailStatus = False Then
                    objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ID").InnerText = ""
                    'objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").InnerText = strFileNo
                    If strFileNo = 0 Then
                        objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").InnerText = ""
                    Else
                        objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("FILENO").InnerText = strFileNo
                    End If
                    objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERNO").InnerText = strorderNumber
                    objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("DOCTYPE").InnerText = strDOCTYPE
                    objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ORDERTYPE").InnerText = strORDERTYPE
                    objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("ContentType").InnerText = "2"
                    objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailFrom").InnerText = strEmailFrom
                    objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailTo").InnerText = strEmailTo
                    objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailSubject").InnerText = strEmailSubject
                    objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailBody").InnerText = strEmailBody
                    objSendMailOutputXml.DocumentElement.SelectSingleNode("AGENCYFILE").Attributes("EmailStatus").InnerText = "False"
                    objSendMailOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "TRUE"
                    Throw (New AAMSException("Error While Sending Mail"))
                End If

            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                If Not objTran Is Nothing Then
                    objTran.Rollback()
                End If
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objSendMailOutputXml, "101", Exec.Message)
                Return objSendMailOutputXml
            Catch exec As Exception
                'msgbox(exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bzShared.FillErrorStatus(objSendMailOutputXml, "103", exec.Message)
                Return objSendMailOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
            End Try
            objSendMailOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Return objSendMailOutputXml
        End Function
        Public Function GetAgencyGroupCompetitionDetails(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the Agency Competition Detail List, based on the given field value
            'Input  : 
            '<UP_GETCOMPETITIONDETAILS_INPUT>
            '   <CHAIN_CODE></CHAIN_CODE>
            '   <ResponsibleStaffID></@ResponsibleStaffID>    
            '</UP_GETCOMPETITIONDETAILS_INPUT>

            'Output :  
            '<UP_GETCOMPETITIONDETAILS_OUTPUT>
            '<COMPETITION_DETAILS  LCODE ='' AGENCY='' CRSID=''  DATE_END='' DATE_START='' SOLE_USER='' DIAL_BACKUP='' ONLINESTATUSID='' PRINTER_COUNT='' PC_COUNT='' Action='' />
            '   <Errors Status=''>
            '       <Error Code='' Description='' />
            '   </Errors>
            '</UP_GETCOMPETITIONDETAILS_OUTPUT>
            ''************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strCHAINCODE As String
            Dim strResponsibleStaffID As String
            Dim blnRecordFound As Boolean

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Dim strMETHOD_NAME As String = "GetAgencyGroupCompetitionDetails"


            objOutputXml.LoadXml(srtGETAGENCYGROUPCOMPETITIONDETAILS_OUTPUT)

            Try

                If SearchDoc.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText <> "" Then
                    strCHAINCODE = SearchDoc.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText
                Else
                    strCHAINCODE = ""
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("ResponsibleStaffID").InnerText <> "" Then
                    strResponsibleStaffID = SearchDoc.DocumentElement.SelectSingleNode("ResponsibleStaffID").InnerText
                Else
                    strResponsibleStaffID = ""
                End If

                If strCHAINCODE = "" Then
                    Throw (New AAMSException("Agency Location Code can't be blank."))
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
                    .CommandText = "UP_GET_TA_AGENCYGROUPCOMPETITIONDETAILS"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@CHAINCODE", SqlDbType.Int))
                    .Parameters("@CHAINCODE").Value = strCHAINCODE

                    .Parameters.Add(New SqlParameter("@ResponsibleStaffID", SqlDbType.Int))
                    .Parameters("@ResponsibleStaffID").Value = strResponsibleStaffID


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

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS")
                If objSqlReader.HasRows Then
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.RemoveChild(objAptNode)
                End If
                Do While objSqlReader.Read
                    blnRecordFound = True

                    objAptNodeClone.Attributes("LCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")) & "")
                    objAptNodeClone.Attributes("AGENCY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AGENCY")) & "")

                    objAptNodeClone.Attributes("CRSID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CRSID")) & "")
                    objAptNodeClone.Attributes("DATE_END").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE_END")) & "")
                    objAptNodeClone.Attributes("DATE_START").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE_START")) & "")
                    objAptNodeClone.Attributes("SOLE_USER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SOLE_USER")) & "")
                    objAptNodeClone.Attributes("DIAL_BACKUP").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DIAL_BACKUP")) & "")
                    objAptNodeClone.Attributes("ONLINESTATUSID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ONLINESTATUSID")) & "")
                    objAptNodeClone.Attributes("PRINTER_COUNT").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PRINTER_COUNT")) & "")
                    objAptNodeClone.Attributes("PC_COUNT").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PC_COUNT")) & "")


                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop
                objSqlReader.Close()
                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found")
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
            Catch exec As Exception
                'msgbox(exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try

            Return objOutputXml

        End Function
        Public Function GetAgencyGroupInstalledPC(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the Misc Installation record, based on the given field value
            'Input  : 
            '<MS_GETPCINSTALLATION_INPUT>
            '	<CHAIN_CODE></CHAIN_CODE>
            '   <ResponsibleStaffID></ResponsibleStaffID>
            '</MS_GETPCINSTALLATION_INPUT>

            'Output :  
            '<MS_GETPCINSTALLATION_OUTPUT>
            '	<PCINSTALLATION ROWID='' CHAIN_CODE='' AGENCY='' DATE='' CPUTYPE='' CPUNO='' MONTYPE='' MONNO='' KBDTYPE='' KBDNO='' MSETYPE='' MSENO='' OrderNumber='' REMARKS='' CHALLANDATE='' CHALLANNUMBER='' LoggedBy='' Employee_Name='' LoggedDateTime='' CHALLANSTATUS='' CDRNO='' LastModifiedDate =''  />
            '   <TOTAL A1PC='' AGENCYPC='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_GETPCINSTALLATION_OUTPUT>

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strCHAIN_CODE As String
            Dim strResponsibleStaffID As String

            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "GetInstalledPC"
            Dim intA1PC As Integer = 0
            Dim intAGENCYPC As Integer = 0

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean
            objOutputXml.LoadXml(srtGETAGENCYGROUPPCINSTALLATION_OUTPUT)

            Try

                If SearchDoc.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText <> "" Then
                    strCHAIN_CODE = SearchDoc.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText
                Else
                    strCHAIN_CODE = ""
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("ResponsibleStaffID").InnerText <> "" Then
                    strResponsibleStaffID = SearchDoc.DocumentElement.SelectSingleNode("ResponsibleStaffID").InnerText
                Else
                    strResponsibleStaffID = ""
                End If

                If strCHAIN_CODE = "" Then
                    Throw (New AAMSException("Agency ChainCode can't be blank."))
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
                    .CommandText = "UP_GET_TA_AGENCYGROUPPCINSTALLATION"
                    .Connection = objSqlConnection
                    .Parameters.Add(New SqlParameter("@Action", SqlDbType.Char))
                    .Parameters("@Action").Value = "S"

                    .Parameters.Add(New SqlParameter("@CHAIN_CODE", SqlDbType.Int))
                    .Parameters("@CHAIN_CODE").Value = strCHAIN_CODE

                    .Parameters.Add(New SqlParameter("@ResponsibleStaffID", SqlDbType.Int))
                    .Parameters("@ResponsibleStaffID").Value = strResponsibleStaffID


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

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = ""

                    .Parameters.Add(New SqlParameter("@AGENCYPC", SqlDbType.Int))
                    .Parameters("@AGENCYPC").Direction = ParameterDirection.Output
                    .Parameters("@AGENCYPC").Value = 0

                    .Parameters.Add(New SqlParameter("@A1PC", SqlDbType.Int))
                    .Parameters("@A1PC").Direction = ParameterDirection.Output
                    .Parameters("@A1PC").Value = 0

                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("PCINSTALLATION")
                objAptNodeClone = objAptNode.CloneNode(True)
                If objSqlReader.HasRows = True Then objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("ROWID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ROWID")))

                    objAptNodeClone.Attributes("LCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")) & "")
                    objAptNodeClone.Attributes("AGENCY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AGENCY")) & "")

                    objAptNodeClone.Attributes("DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE")) & "")

                    objAptNodeClone.Attributes("CPUTYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CPUTYPE")) & "")
                    objAptNodeClone.Attributes("CPUNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CPUNO")) & "")

                    If Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CPUTYPE")) & "") = "CPP" Or Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MONTYPE")) & "") = "MMP" Then
                        intAGENCYPC = intAGENCYPC + 1
                    Else
                        intA1PC = intA1PC + 1
                    End If

                    objAptNodeClone.Attributes("MONTYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MONTYPE")) & "")
                    objAptNodeClone.Attributes("MONNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MONNO")) & "")

                    objAptNodeClone.Attributes("MSETYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MSETYPE")) & "")
                    objAptNodeClone.Attributes("MSENO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MSENO")) & "")

                    objAptNodeClone.Attributes("KBDTYPE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("KBDTYPE")) & "")
                    objAptNodeClone.Attributes("KBDNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("KBDNO")) & "")


                    objAptNodeClone.Attributes("OrderNumber").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OrderNumber")) & "")
                    objAptNodeClone.Attributes("REMARKS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("REMARKS")) & "")
                    objAptNodeClone.Attributes("CHALLANDATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHALLANDATE")) & "")
                    objAptNodeClone.Attributes("CHALLANNUMBER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHALLANNUMBER")) & "")
                    objAptNodeClone.Attributes("LoggedBy").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LoggedBy")) & "")
                    objAptNodeClone.Attributes("Employee_Name").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Employee_Name")) & "")
                    objAptNodeClone.Attributes("LoggedDateTime").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LoggedDateTime")) & "")
                    objAptNodeClone.Attributes("CHALLANSTATUS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHALLANSTATUS")) & "")
                    objAptNodeClone.Attributes("CDRNO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CDRNO")) & "")
                    objAptNodeClone.Attributes("LastModifiedDate").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LastModifiedDate")) & "")

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop

                objSqlReader.Close()
                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found")
                Else
                    objOutputXml.DocumentElement("TOTAL").Attributes("A1PC").InnerText = Val(objSqlCommand.Parameters("@A1PC").Value)
                    objOutputXml.DocumentElement("TOTAL").Attributes("AGENCYPC").InnerText = Val(objSqlCommand.Parameters("@AGENCYPC").Value)

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
            Catch exec As Exception
                'msgbox(exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function

        Public Function GetAgencyGroupStaffDetails(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the Agency Staff List, based on the given field value
            'Input  : 
            'Input  : 
            '<UP_GETAGENCYSTAFFDETAILS_INPUT>
            '<CHAIN_CODE</CHAIN_CODE> 
            ' <ResponsibleStaffID></ResponsibleStaffID>
            '</UP_GETAGENCYSTAFFDETAILS_INPUT>


            'Output :  
            '<UP_GETAGENCYSTAFFDETAILS_OUTPUT>
            '<AGENCYSTAFF LCODE='' AGENCYSTAFFID='' STAFFNAME='' DESIGNATION='' />
            '<Errors Status=''>
            '<Error Code='' Description='' />
            '</Errors>
            '</UP_GETAGENCYSTAFFDETAILS_OUTPUT>
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strChainCode As String
            Dim blnRecordFound As Boolean
            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean
            Dim intResponsibleStaffID As Integer
            Dim strMETHOD_NAME As String = "GetAgencyStaffDetails"

            objOutputXml.LoadXml(srtGETAGENCYGROUPSTAFFDETAILS_OUTPUT)

            Try

                If SearchDoc.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText <> "" Then
                    strChainCode = SearchDoc.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText
                Else
                    strChainCode = ""
                End If


                If SearchDoc.DocumentElement.SelectSingleNode("ResponsibleStaffID").InnerText <> "" Then
                    intResponsibleStaffID = SearchDoc.DocumentElement.SelectSingleNode("ResponsibleStaffID").InnerText
                Else
                    intResponsibleStaffID = ""
                End If


                If strChainCode = "" Then
                    Throw (New AAMSException("chain Code can't be blank."))
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
                    .CommandText = "UP_GET_INC_TA_AGENCYGROUPSTAFF"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@CHAIN_CODE", SqlDbType.BigInt))
                    .Parameters("@CHAIN_CODE").Value = strChainCode

                    .Parameters.Add(New SqlParameter("@ResponsibleStaffID", SqlDbType.BigInt))
                    .Parameters("@ResponsibleStaffID").Value = intResponsibleStaffID

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

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF")
                If objSqlReader.HasRows Then
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.RemoveChild(objAptNode)
                End If

                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("LCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")))
                    objAptNodeClone.Attributes("AGENCY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AGENCY"))) & ""
                    objAptNodeClone.Attributes("AGENCYSTAFFID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AGENCYSTAFFID")))
                    objAptNodeClone.Attributes("STAFFNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("STAFFNAME")))
                    objAptNodeClone.Attributes("DESIGNATION").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DESIGNATION")))
                    objAptNodeClone.Attributes("CHAIN_CODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CHAIN_CODE")))

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop

                objSqlReader.Close()
                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found")
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
            Catch exec As Exception
                'msgbox(exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function

        Public Function GetAgencyGroupCrsUseDetails(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: To list out the Agency CrsUse Detail List, based on the given field value
            'Input  : 
            '<UP_GETCRSDETAILS_INPUT>
            '<CHAIN_CODE</CHAIN_CODE> 
            '<ResponsibleStaffID></ResponsibleStaffID>
            '</UP_GETCRSDETAILS_INPUT>

            'Output :  
            '<UP_GETCRSDETAILS_OUTPUT>
            '<CRS RN='' LCODE='' AGENCY ='' DATE='' CURRENTID='' CRS='' OFFICEID='' Action='' />
            '   <Errors Status=''>
            '       <Error Code='' Description='' />
            '   </Errors>
            '</UP_GETCRSDETAILS_OUTPUT>
            ''************************************************************************

            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim strCHAINCODE As String
            Dim strResponsibleStaffID As String
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "GetAgencyGroupCrsUseDetails"

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            objOutputXml.LoadXml(srtGETAGENCYGROUPCRSUSEDETAILS_OUTPUT)

            Try

                If SearchDoc.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText <> "" Then
                    strCHAINCODE = SearchDoc.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText
                Else
                    strCHAINCODE = ""
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("ResponsibleStaffID").InnerText <> "" Then
                    strResponsibleStaffID = SearchDoc.DocumentElement.SelectSingleNode("ResponsibleStaffID").InnerText
                Else
                    strResponsibleStaffID = ""
                End If




                If strCHAINCODE = "" Then
                    Throw (New AAMSException("Agency Location Code can't be blank."))
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
                    .CommandText = "UP_GET_TA_AGENCYGROUPCRSUSEDETAILS"
                    .Connection = objSqlConnection

                    .Parameters.Add(New SqlParameter("@CHAIN_CODE", SqlDbType.Int))
                    .Parameters("@CHAIN_CODE").Value = strCHAINCODE

                    .Parameters.Add(New SqlParameter("@ResponsibleStaffID", SqlDbType.Int))
                    .Parameters("@ResponsibleStaffID").Value = strResponsibleStaffID


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

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("CRS")
                If objSqlReader.HasRows Then
                    objAptNodeClone = objAptNode.CloneNode(True)
                    objOutputXml.DocumentElement.RemoveChild(objAptNode)
                End If
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("RN").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("RN")))
                    objAptNodeClone.Attributes("LCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LCODE")))
                    objAptNodeClone.Attributes("AGENCY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("AGENCY")))
                    objAptNodeClone.Attributes("DATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DATE")))
                    objAptNodeClone.Attributes("CURRENTID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CURRENTID")))
                    objAptNodeClone.Attributes("CRS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CRS")))
                    objAptNodeClone.Attributes("OFFICEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OFFICEID")))

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop
                objSqlReader.Close()
                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found")
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
            Catch exec As Exception
                'msgbox(exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try

            Return objOutputXml

        End Function

        Public Function GetAgencyAddressDetails(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            'Purpose:This function gives search results based on choosen search criterion.
            'Input  :- 
            '<TA_SEARCHAGENCY_INPUT>
            '	<OFFICEID></OFFICEID>
            '</TA_SEARCHAGENCY_INPUT>

            'Output:
            '<MS_SEARCHAGENCY_OUTPUT>
            '	<AGNECY LOCATION_CODE='' CHAIN_CODE='' OfficeID='' NAME=''  ADDRESS='' ADDRESS1='' CITY='' COUNTRY='' ONLINE_STATUS='' PINCODE='' Email='' CONTACT_PERSON='' CONTACT_PERSON_ID='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_SEARCHAGENCY_OUTPUT>
            Const strSEARCHAGENCYDETAILS_OUTPUT = "<TA_SEARCHAGENCY_OUTPUT><AGNECY NAME='' ADDRESS='' ADDRESS1='' CITY='' PINCODE='' CITYCODE=''/><Errors Status=''><Error Code='' Description='' /></Errors></TA_SEARCHAGENCY_OUTPUT>"
            '-----------------------------------------------------------------------------
            Dim objSqlConnection As New SqlConnection(GetAAMSConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strOfficeID As String = ""



            Const strMETHOD_NAME As String = "GetAgencyAddressDetails"
            objOutputXml.LoadXml(strSEARCHAGENCYDETAILS_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start

                If SearchDoc.DocumentElement.SelectSingleNode("OFFICEID").InnerText.Trim() <> "" Then
                    strOfficeID = SearchDoc.DocumentElement.SelectSingleNode("OFFICEID").InnerText.Trim()
                End If


                If strOfficeID = "" Then
                    Throw (New AAMSException("OfficeID can't be blank."))
                End If

                '--------Retrieving & Checking Details from Input XMLDocument ------End
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_SER_TA_LOCATION_MASTER_ON_OFFICEID]"
                    .Connection = objSqlConnection

                    .Parameters.Add("@OFFICEID", SqlDbType.VarChar, 10)
                    .Parameters("@OFFICEID").Value = strOfficeID



                End With

                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("AGNECY")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True
                    objAptNodeClone.Attributes("NAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("NAME")))
                    objAptNodeClone.Attributes("ADDRESS").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ADDRESS")) & "")
                    objAptNodeClone.Attributes("ADDRESS1").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ADDRESS1")) & "")
                    objAptNodeClone.Attributes("CITY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITY")) & "")
                    objAptNodeClone.Attributes("PINCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("PINCODE")) & "")
                    objAptNodeClone.Attributes("CITYCODE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("CITYCODE")) & "")


                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop
                objSqlReader.Close()
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
                'msgbox(Exec.ToString)
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function

        Public Shared Function GetAAMSConnectionString() As String
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



            lstrLoginName = System.Configuration.ConfigurationSettings.AppSettings("AAMSLoginName").Trim 'Trim(My.MySettings.Default.LoginName)
            lstrServerName = System.Configuration.ConfigurationSettings.AppSettings("AAMSServer").Trim  'Trim(My.MySettings.Default.Server)
            lstrDBName = System.Configuration.ConfigurationSettings.AppSettings("AAMSDatabase").Trim  'Trim(My.MySettings.Default.Database)
            lstrPassword = objBarcode.Decrypt(Trim(System.Configuration.ConfigurationSettings.AppSettings("AAMSPasskey")), Key) 'objBarcode.Decrypt(Trim(My.MySettings.Default.Passkey), Key)
            lstrMaxPoolSize = System.Configuration.ConfigurationSettings.AppSettings("MaxConnectionPool").Trim 'Trim(My.MySettings.Default.MaxConnectionPool)
            lstrNet = System.Configuration.ConfigurationSettings.AppSettings("CommProtocol").Trim 'Trim(My.MySettings.Default.CommProtocol)
            lstrTrusted = System.Configuration.ConfigurationSettings.AppSettings("TrustedConnection").Trim 'Trim(My.MySettings.Default.TrustedConnection)

            'If values are not present in app.config, set them to default values
            If lstrLoginName = "" Then lstrLoginName = "sa"
            If lstrPassword = "" Then lstrPassword = "Contr)l"
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
        Public Function SendAddressDetailsMail(ByVal strAgencyName As String, ByVal strAddress As String, ByVal strAddress1 As String, ByVal strCity As String, ByVal strPIN As String)
            'Send Mail To BirdResTeam

            'Get MailID 
            'Get Template
            'Send(Mail)
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim ObjSendMail As bizUtility.bzEmail
            Dim boolSendMailStatus As Boolean
            Dim objInputSendMailXml As New XmlDocument
            Dim strEmailBody As String
            Dim strEmailTo As String
            Dim strCC As String
            Dim strSource As String
            Dim objSqlCmdMTemplate As New SqlCommand
            objInputSendMailXml.LoadXml(strSendMail_INPUT)
            Try

                Dim strMailTemplate As String = ""
                Dim objSqlTemplateReader As SqlDataReader
                With objSqlCmdMTemplate
                    .Connection = objSqlConnection
                    .CommandText = "UP_SRO_EMAIL_TEMPLATES"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.VarChar, 1))
                    .Parameters.Add(New SqlParameter("@MailTemplateName", SqlDbType.VarChar, 40))
                End With
                objSqlCmdMTemplate.Parameters("@ACTION").Value = "V"
                objSqlCmdMTemplate.Parameters("@MailTemplateName").Value = "BRMAILALERT_ADDRESSCHANGE"
                objSqlCmdMTemplate.Connection.Open()
                objSqlTemplateReader = objSqlCmdMTemplate.ExecuteReader()
                If objSqlTemplateReader.HasRows Then
                    Do While objSqlTemplateReader.Read()
                        strMailTemplate = Trim(objSqlTemplateReader.GetValue(objSqlTemplateReader.GetOrdinal("MailTemplate")) & "")
                        strEmailTo = Trim(objSqlTemplateReader.GetValue(objSqlTemplateReader.GetOrdinal("FIELD_VALUE")) & "")
                    Loop
                End If
                objSqlTemplateReader.Close()
                objSqlCmdMTemplate.Dispose()
                strSource = System.Configuration.ConfigurationSettings.AppSettings("MAIL_SOURCE").Trim
                strEmailBody = strMailTemplate
                strEmailBody = strEmailBody.Replace("[[Name]]", strAgencyName)
                strEmailBody = strEmailBody.Replace("[[ADDRESS]]", strAddress)
                strEmailBody = strEmailBody.Replace("[[ADDRESS1]]", strAddress1)
                strEmailBody = strEmailBody.Replace("[[CITY]]", strCity)
                strEmailBody = strEmailBody.Replace("[[PIN]]", strPIN)


                With objInputSendMailXml.SelectSingleNode("SENDMAILIMMEDIATE_INPUT/MAIL_DETAILS")
                    .Attributes("SOURCE").InnerText = strSource
                    .Attributes("DESTINATION_TO").InnerText = strEmailTo
                    .Attributes("SUBJECT").InnerText = "Address Change : Agency Using BirdRes"
                    .Attributes("MESSAGE").InnerText = strEmailBody
                End With

                ObjSendMail = New bizUtility.bzEmail
                boolSendMailStatus = ObjSendMail.SendMail(objInputSendMailXml)

            Catch ex As Exception
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCmdMTemplate.Dispose()

            End Try
        End Function
        
    End Class
End Namespace


