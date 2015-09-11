Imports System.Threading
Imports System.IO
Imports System.Xml
Imports System.Data
Imports System.Collections.Generic

Partial Class Incentive_MSSR_PaymentExportApprovalQueue
    Inherits System.Web.UI.Page
    Const strSendMail_INPUT = "<SENDMAILIMMEDIATE_INPUT><MAIL_DETAILS DESTINATION_TO='' SUBJECT='' MESSAGE='' SOURCE='' DESTINATION_CC='' DESTINATION_BCC='' ATTACHMENT_FILE=''/></SENDMAILIMMEDIATE_INPUT>"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strExportType As String
        strExportType = Request.QueryString("Type").ToString

        If strExportType.ToString.Trim() = "GeneratePaySheet" Then
            GenerateExcelSheet()
        Else
            'GenerateExcelSheet()
            BtnExport()
        End If
    End Sub

    Private Sub BtnExport()

        Dim dset As DataSet = CType(HttpContext.Current.Session("PaymentRecDataSource"), DataSet)

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzPaymentApprovalQue As New AAMS.bizIncetive.bzPaymentApprovalQue
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        '        Dim objXmlReader As XmlNodeReader
        Try

            objInputXml.LoadXml("<UP_SER_INC_PAYMENT_APPROVAL_QUE_INPUT> <CHAIN_CODE></CHAIN_CODE>  <GROUPNAME></GROUPNAME>   <AOFFICE></AOFFICE>   <MONTH></MONTH><YEAR></YEAR>    <STATUS></STATUS>  <EMPLOYEEID></EMPLOYEEID>    <REC_DATE></REC_DATE>       <PAGE_NO></PAGE_NO>   <PAGE_SIZE></PAGE_SIZE>    <SORT_BY></SORT_BY>    <DESC></DESC>     </UP_SER_INC_PAYMENT_APPROVAL_QUE_INPUT>")


            '   <UP_SER_INC_PAYMENT_APPROVAL_QUE_INPUT>
            '      <CHAIN_CODE></CHAIN_CODE>
            '      <GROUPNAME></GROUPNAME>
            '      <AOFFICE></AOFFICE>
            '      <MONTH></MONTH>
            '      <YEAR></YEAR>
            '      <STATUS></STATUS>
            '      <EMPLOYEEID></EMPLOYEEID>
            '      <REC_DATE></REC_DATE>   
            '      <PAGE_NO></PAGE_NO>
            '      <PAGE_SIZE></PAGE_SIZE>
            '      <SORT_BY></SORT_BY>
            '      <DESC></DESC>  
            '</UP_SER_INC_PAYMENT_APPROVAL_QUE_INPUT>

            'Session("Year") = drpYears.SelectedValue

            'Session("Month") = drpYears.SelectedValue

            'Session("Status") = DlstStatus.SelectedValue

            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If
            If Not Session("Year") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("YEAR").InnerText = Session("Year").ToString  'Request.QueryString("Year")

            End If
            If Not Session("Month") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("MONTH").InnerText = Session("Month").ToString  'Request.QueryString("Month")
            End If
            If Not Session("Status") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("STATUS").InnerText = Session("Status").ToString  ' Request.QueryString("Status")

            End If

            'objInputXml.DocumentElement.SelectSingleNode("MONTH").InnerText = "11"

            objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("GROUPNAME").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = ""

            objInputXml.DocumentElement.SelectSingleNode("REC_DATE").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ""

            'Here Back end Method Call
            ' hdChechedItem.Value = "0"
            ' Session("PaymentRecDataSource") = Nothing
            '  ddlPageNumber.SelectedValue = 1
            '  objOutputXml = objbzISPOrder.DETAILSReport(objInputXml)

            '<DETAILS 
            'REGION=''              0
            'BC_ID=''               1
            'GROUPNAME=''           2
            'CITY=''                3
            'PERIOD=''              4
            'NOOFMONTHS=''          5
            'AMOUNT=''              6
            'ANYADJ=''              7
            'THISQUARTERPAYMENT=''  8
            'SEGMENTS_PAID_FOR=''   9
            'GROSSSEGMENTS=''       10 
            'GROSS_WOIC=''          11
            'SOLE=''                12
            'REMARKS=''             13
            'HW=''                  14
            'SARAL=''               15
            'ILL=''                 16
            'WLL=''                 17
            'TOTALCOST=''           18 
            'CPS_GROSS=''           19 
            'CPS_WOIC=''            20
            'CPS_WOHW=''            21
            'OFFICEID=''            22
            'CHAIN_CODE=''          23
            'GUARUNTEEBY=''         24
            'BIRDRESUPDATE=''       25
            'ROWNO=''               26
            'PAYMENT_ID=''          27
            'CheckUncheckStatus=''  28
            'SEGMENT=''             29
            'UPFRONTGUARANTEEBY=''  30 
            'STATUS=''              31
            'CURRENTSTATUS=''       32
            'PA_FINAL_APPROVED=''   33
            'PAYMENTTYPE=''         34
            'NO_OF_PAYMENT=''       35
            'MONTH=''               36
            'YEAR=''                37
            'PLBCYCLE=''            38
            'PLBPERIODFROM=''       39 
            'PLBPERIODTO=''         40
            ' />


            '<DETAILS    ROWNO = "1"   PAYMENT_ID = "28"  CheckUncheckStatus = ""    BC_ID = "127"  CHAIN_CODE = "19308"   GROUPNAME = "T B N TRAVEL BUSINESS"    CITY = "New Delhi"   PERIOD = "Jul-09 - Aug-09"   NOOFMONTHS = "1"   AMOUNT = "150000"     ANYADJ = ""         THISQUARTERPAYMENT = "150000"            SEGMENTS_PAID_FOR = "0"     GROSSSEGMENTS = "0"    SEGMENT = ""   GROSS_WOIC = "0"  SOLE = "False"     REMARKS = ""     TOTALCOST = "0"    CPS_GROSS = "0"   CPS_WOIC = "0"    CPS_WOHW = "0"  UPFRONTGUARANTEEBY = "0"  STATUS = "Final Approved"   CURRENTSTATUS = "" PA_FINAL_APPROVED="" />  '<DETAILS    ROWNO = "1"   PAYMENT_ID = "28"  CheckUncheckStatus = ""    BC_ID = "127"  CHAIN_CODE = "19308"   GROUPNAME = "T B N TRAVEL BUSINESS"    CITY = "New Delhi"   PERIOD = "Jul-09 - Aug-09"   NOOFMONTHS = "1"   AMOUNT = "150000"     ANYADJ = ""         THISQUARTERPAYMENT = "150000"            SEGMENTS_PAID_FOR = "0"     GROSSSEGMENTS = "0"    SEGMENT = ""   GROSS_WOIC = "0"  SOLE = "False"     REMARKS = ""     TOTALCOST = "0"    CPS_GROSS = "0"   CPS_WOIC = "0"    CPS_WOHW = "0"  UPFRONTGUARANTEEBY = "0"  STATUS = "Final Approved"   CURRENTSTATUS = "Final Approved" PA_FINAL_APPROVED="True" />   '<DETAILS    ROWNO = "1"   PAYMENT_ID = "28"  CheckUncheckStatus = ""    BC_ID = "127"  CHAIN_CODE = "19308"   GROUPNAME = "T B N TRAVEL BUSINESS"    CITY = "New Delhi"   PERIOD = "Jul-09 - Aug-09"   NOOFMONTHS = "1"   AMOUNT = "150000"     ANYADJ = ""         THISQUARTERPAYMENT = "150000"            SEGMENTS_PAID_FOR = "0"     GROSSSEGMENTS = "0"    SEGMENT = ""   GROSS_WOIC = "0"  SOLE = "False"     REMARKS = ""     TOTALCOST = "0"    CPS_GROSS = "0"   CPS_WOIC = "0"    CPS_WOHW = "0"  UPFRONTGUARANTEEBY = "0"  STATUS = "Final Approved"   CURRENTSTATUS = "Final Approved" PA_FINAL_APPROVED="True" /> 

            '<DETAILS    ROWNO = '1'   PAYMENT_ID = '28'  CheckUncheckStatus = ''    BC_ID = '127'  CHAIN_CODE = '19308'   GROUPNAME = 'T B N TRAVEL BUSINESS'    CITY = 'New Delhi'   PERIOD = 'Jul-09 - Aug-09'   NOOFMONTHS = '1'   AMOUNT = '150000'     ANYADJ = ''         THISQUARTERPAYMENT = '150000'            SEGMENTS_PAID_FOR = '0'     GROSSSEGMENTS = '0'    SEGMENT = ''   GROSS_WOIC = '0'  SOLE = 'False'     REMARKS = ''     TOTALCOST = '0'    CPS_GROSS = '0'   CPS_WOIC = '0'    CPS_WOHW = '0'  UPFRONTGUARANTEEBY = '0'  STATUS = 'Final Approved'   CURRENTSTATUS = '' PA_FINAL_APPROVED='' />  '<DETAILS    ROWNO = '1'   PAYMENT_ID = '28'  CheckUncheckStatus = ''    BC_ID = '127'  CHAIN_CODE = '19308'   GROUPNAME = 'T B N TRAVEL BUSINESS'    CITY = 'New Delhi'   PERIOD = 'Jul-09 - Aug-09'   NOOFMONTHS = '1'   AMOUNT = '150000'     ANYADJ = ''         THISQUARTERPAYMENT = '150000'            SEGMENTS_PAID_FOR = '0'     GROSSSEGMENTS = '0'    SEGMENT = ''   GROSS_WOIC = '0'  SOLE = 'False'     REMARKS = ''     TOTALCOST = '0'    CPS_GROSS = '0'   CPS_WOIC = '0'    CPS_WOHW = '0'  UPFRONTGUARANTEEBY = '0'  STATUS = 'Final Approved'   CURRENTSTATUS = 'Final Approved' PA_FINAL_APPROVED='True' />   '<DETAILS    ROWNO = '1'   PAYMENT_ID = '28'  CheckUncheckStatus = ''    BC_ID = '127'  CHAIN_CODE = '19308'   GROUPNAME = 'T B N TRAVEL BUSINESS'    CITY = 'New Delhi'   PERIOD = 'Jul-09 - Aug-09'   NOOFMONTHS = '1'   AMOUNT = '150000'     ANYADJ = ''         THISQUARTERPAYMENT = '150000'            SEGMENTS_PAID_FOR = '0'     GROSSSEGMENTS = '0'    SEGMENT = ''   GROSS_WOIC = '0'  SOLE = 'False'     REMARKS = ''     TOTALCOST = '0'    CPS_GROSS = '0'   CPS_WOIC = '0'    CPS_WOHW = '0'  UPFRONTGUARANTEEBY = '0'  STATUS = 'Final Approved'   CURRENTSTATUS = 'Final Approved' PA_FINAL_APPROVED='True' /> 
            '  objOutputXml.LoadXml("<UP_SER_INC_PAYMENT_APPROVAL_QUE_OUTPUT><DETAILS ROWNO='1' CheckUncheckStatus ='' GROUPNAME='' PAYMENT_ID='1' STATUS ='' CURRENTSTATUS='' PA_FINAL_APPROVED='False' BC_ID ='2' CHAIN_CODE='' CITY='' PERIOD='' NOOFMONTHS='' AMOUNT='' ANYADJ='' THISQUARTERPAYMENT='' SEGMENTS_PAID_FOR='' GROSSSEGMENTS='' SEGMENT='' GROSS_WOIC='' SOLE='' REMARKS='' TOTALCOST='' CPS_GROSS='' CPS_WOIC='' CPS_WOHW='' UPFRONTGUARANTEEBY=''/>  <DETAILS ROWNO='2' PAYMENT_ID='2' STATUS ='' CURRENTSTATUS='' PA_FINAL_APPROVED='True' BC_ID ='2' CHAIN_CODE='' CITY='' PERIOD='' NOOFMONTHS='' AMOUNT='' ANYADJ='' THISQUARTERPAYMENT='' SEGMENTS_PAID_FOR='' GROSSSEGMENTS='' SEGMENT='' GROSS_WOIC='' SOLE='' REMARKS='' TOTALCOST='' CPS_GROSS='' CPS_WOIC='' CPS_WOHW='' UPFRONTGUARANTEEBY=''/> <DETAILS ROWNO='3' PAYMENT_ID='3' STATUS ='' CURRENTSTATUS='' PA_FINAL_APPROVED='False' BC_ID ='2' CHAIN_CODE='' CITY='' PERIOD='' NOOFMONTHS='' AMOUNT='' ANYADJ='' THISQUARTERPAYMENT='' SEGMENTS_PAID_FOR='' GROSSSEGMENTS='' SEGMENT='' GROSS_WOIC='' SOLE='' REMARKS='' TOTALCOST='' CPS_GROSS='' CPS_WOIC='' CPS_WOHW='' UPFRONTGUARANTEEBY=''/>   <Errors Status='False'>          <Error Code='' Description='' />          </Errors>   </UP_SER_INC_PAYMENT_APPROVAL_QUE_OUTPUT>")

            objOutputXml = objbzPaymentApprovalQue.SearchPaymentApprovalQue(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                'ViewState("InputXml") = objInputXml.OuterXml
                'Dim objExport As New ExportExcel
                'Dim strArray() As String = {"Chain Code", "Agency Name", "City", "Period", "No. Of Month", "Amount", "Any ADJ", "Quarter Payment", "Segment Paid For", "Gross Segment", "Gross Segment WOIC", "Sole", "Remarks", "Total Cost", "CPS Gross", "CPS WOIC", "CPS WOHW", "Upfront Guranteed By", "Status", "Current Status"}
                'Dim intArray() As Integer = {4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24}
                'objExport.ExportDetails(objOutputXml, "DETAILS", intArray, strArray, ExportExcel.ExportFormat.Excel, "PayQue.xls")
                Dim objExport As New ExportExcel
                Dim strArray() As String = {"Region", "BCaseId", "Chain Code", "Office ID", "Group Name", "City", "Period", "No. Of Month", "Amount", "Any AJD", "Final Payment", "Segment Paid For", "Gross Segment", "Gross Segment WOIC", "Sole", "Remarks", "HW", "SARAL", "ILL", "WLL", "Total Cost", "CPS Gross", "CPS WOIC", "CPS WOHW", "Guaruntee By", "Birdres Update"}
                Dim intArray() As Integer = {0, 1, 23, 22, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 24, 25}
                objExport.ExportDetails(objOutputXml, "DETAILS", intArray, strArray, ExportExcel.ExportFormat.Excel, "PayQue.xls")

            Else
                Dim objOutputXmlExport As New XmlDocument
                Dim objXmlNode As XmlNode

                'objOutputXmlExport.LoadXml("<PP><DETAILS    ROWNO = ''    PAYMENT_ID = ''   CheckUncheckStatus = ''   BC_ID = ''   CHAIN_CODE = '' GROUPNAME = '' CITY = ''   PERIOD = ''    NOOFMONTHS = ''    AMOUNT = ''   ANYADJ = '' THISQUARTERPAYMENT = '' SEGMENTS_PAID_FOR = ''  GROSSSEGMENTS = ''  SEGMENT = '' GROSS_WOIC = '' SOLE = ''    REMARKS = ''  TOTALCOST = '' CPS_GROSS = '' CPS_WOIC = ''   CPS_WOHW = ''  UPFRONTGUARANTEEBY = ''      STATUS = ''   CURRENTSTATUS = ''  PA_FINAL_APPROVED='' /> ></PP>")

                objOutputXmlExport.LoadXml("<PP><DETAILS REGION='' BC_ID='' GROUPNAME='' CITY='' PERIOD='' NOOFMONTHS='' AMOUNT='' ANYADJ='' THISQUARTERPAYMENT='' SEGMENTS_PAID_FOR='' GROSSSEGMENTS='' GROSS_WOIC='' SOLE='' REMARKS='' HW='' SARAL='' ILL='' WLL='' TOTALCOST='' CPS_GROSS='' CPS_WOIC='' CPS_WOHW='' OFFICEID='' CHAIN_CODE='' GUARUNTEEBY='' BIRDRESUPDATE='' ROWNO='' PAYMENT_ID='' CheckUncheckStatus='' SEGMENT='' UPFRONTGUARANTEEBY='' STATUS='' CURRENTSTATUS='' PA_FINAL_APPROVED='' PAYMENTTYPE='' NO_OF_PAYMENT=''  MONTH='' YEAR='' PLBCYCLE='' PLBPERIODFROM='' PLBPERIODTO='' /></PP>")

                objXmlNode = objOutputXmlExport.DocumentElement.SelectSingleNode("DETAILS")

                For Each XmlAttr As XmlAttribute In objXmlNode.Attributes
                    XmlAttr.Value = ""
                Next


                objOutputXml.DocumentElement.AppendChild(objOutputXml.ImportNode(objXmlNode, True))
                Dim objExport As New ExportExcel
                Dim strArray() As String = {"Region", "BCaseId", "Chain Code", "Office ID", "Group Name", "City", "Period", "No. Of Month", "Amount", "Any AJD", "Final Payment", "Segment Paid For", "Gross Segment", "Gross Segment WOIC", "Sole", "Remarks", "HW", "SARAL", "ILL", "WLL", "Total Cost", "CPS Gross", "CPS WOIC", "CPS WOHW", "Guaruntee By", "Birdres Update"}
                Dim intArray() As Integer = {0, 1, 23, 22, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 24, 25}
                objExport.ExportDetails(objOutputXml, "DETAILS", intArray, strArray, ExportExcel.ExportFormat.Excel, "PayQue.xls")



            End If
        Catch ex As Exception

        Finally
            objOutputXml = Nothing
            objInputXml = Nothing
            ' objbzISPOrder = Nothing
        End Try
    End Sub

    Private Sub GenerateExcelSheet()
        'Dim dset As DataSet = CType(HttpContext.Current.Session("PaymentRecDataSourceAutoGenerateForMail"), DataSet)

        Dim blnOpen As Boolean = False
        Dim strUniqueFn As String = ""
        Dim objDT As New DataTable
        Dim drNewRow As DataRow
        Dim x As Integer
        Dim columnCount As Integer
        Dim rowCount As Int16
        Dim boolRec As Boolean

        Dim ObjSendMail As AAMS.bizUtility.bzEmail
        Dim boolSendMailStatus As Boolean
        Dim objInputSendMailXml As New XmlDocument
        Dim strEmailBody As String = ""
        Dim strSource As String = ""
        Dim strDestination As String = ""

        Try
            boolRec = False

            ' Get the user id.
            Dim strUser As String = _
                Thread.CurrentPrincipal.Identity.Name.Substring( _
                  Thread.CurrentPrincipal.Identity.Name.IndexOf("\") + 1).ToUpper()

            ' Get the folder to store files in.
            Dim strFolder As String = Server.MapPath("~") + "\MailAttachment\"

            ' Create a reference to the folder.
            Dim di As New IO.DirectoryInfo(strFolder)

            ' Create a list of files in the directory.
            Dim fi As IO.FileInfo() = di.GetFiles(strUser & "*.*")
            Dim i As Integer

            For i = 0 To fi.Length - 1
                IO.File.Delete(strFolder & "\" & fi(i).Name)
            Next

            ' Get a unique file name.
            strUniqueFn = strUser & "Payment" & _
            IO.Path.GetFileNameWithoutExtension(IO.Path.GetTempFileName()) & ".xls"

            ' Get the full path to the file.
            Dim strPath As String = strFolder & strUniqueFn
            Dim strNewPath As String = strFolder & "Payment"

            ' Get the data for the report.
            'Dim dset As DataSet = CType(HttpContext.Current.Session("PaymentRecDataSource"), DataSet)
            Dim dset As DataSet = CType(HttpContext.Current.Session("PaymentRecDataSourceAutoGenerateForMail"), DataSet)

            ' Tweak the dataset so it displays meaningful DataSet and Table Names.
            'dset.DataSetName = "My_Report"
            'dset.Tables(4).TableName = "Payment"

            strDestination = dset.Tables("CONFIG").Rows(0)("EMAILIDS")
            strEmailBody = dset.Tables("CONFIG").Rows(0)("EMAIL_TEMPLATE")


            Dim objCol As DataColumn

            objCol = New DataColumn("BCaseId", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("Chain Code", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("Group Name", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("City", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("Period", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("No Of Month", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("Amount", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("Any ADJ", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("Quarter Payment", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("Segment Paid For", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("Gross Segment", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("Segment Gross WOIC", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("Remarks", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("Total Cost", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("CPS Gross", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("CPS WOIC", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("CPS WOHW", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("UpfrontGuaranted By", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("Status", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("Current Status", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            'drNewRow = objDT.NewRow
            'drNewRow("BCaseId") = "BCaseId"
            'drNewRow("Chain Code") = "Chain Code"
            'drNewRow("Group Name") = "Group Name"
            'drNewRow("City") = "City"
            'drNewRow("Period") = "Period"
            'drNewRow("No Of Month") = "No Of Month"
            'drNewRow("Amount") = "Amount"
            'drNewRow("Any ADJ") = "Any ADJ"
            'drNewRow("Quarter Payment") = "Quarter Payment"
            'drNewRow("Segment Paid For") = "Segment Paid For"
            'drNewRow("Gross Segment") = "Gross Segment"
            'drNewRow("Segment Gross WOIC") = "Segment Gross WOIC"
            'drNewRow("Remarks") = "Remarks"
            'drNewRow("Total Cost") = "Total Cost"
            'drNewRow("CPS Gross") = "CPS Gross"
            'drNewRow("CPS WOIC") = "CPS WOIC"
            'drNewRow("CPS WOHW") = "CPS WOHW"
            'drNewRow("UpfrontGuaranted By") = "UpfrontGuaranted By"
            'drNewRow("Status") = "Status"
            'drNewRow("Current Status") = "Current Status"
            'objDT.Rows.Add(drNewRow)

            For i = 0 To dset.Tables("DETAILS").Rows.Count - 1
                If dset.Tables("DETAILS").Rows(i)("CheckUncheckStatus").ToString().Trim().ToUpper() = "TRUE" Then
                    drNewRow = objDT.NewRow
                    drNewRow("BCaseId") = dset.Tables("DETAILS").Rows(i)("BC_ID").ToString()
                    drNewRow("Chain Code") = dset.Tables("DETAILS").Rows(i)("CHAIN_CODE").ToString()
                    drNewRow("Group Name") = dset.Tables("DETAILS").Rows(i)("GROUPNAME").ToString()
                    drNewRow("City") = dset.Tables("DETAILS").Rows(i)("CITY").ToString()
                    drNewRow("Period") = dset.Tables("DETAILS").Rows(i)("PERIOD").ToString()
                    drNewRow("No Of Month") = dset.Tables("DETAILS").Rows(i)("NOOFMONTHS").ToString()
                    drNewRow("Amount") = dset.Tables("DETAILS").Rows(i)("AMOUNT").ToString()
                    drNewRow("Any ADJ") = dset.Tables("DETAILS").Rows(i)("ANYADJ").ToString()
                    drNewRow("Quarter Payment") = dset.Tables("DETAILS").Rows(i)("THISQUARTERPAYMENT").ToString()
                    drNewRow("Segment Paid For") = dset.Tables("DETAILS").Rows(i)("SEGMENTS_PAID_FOR").ToString()
                    drNewRow("Gross Segment") = dset.Tables("DETAILS").Rows(i)("GROSSSEGMENTS").ToString()
                    drNewRow("Segment Gross WOIC") = dset.Tables("DETAILS").Rows(i)("GROSS_WOIC").ToString()
                    drNewRow("Remarks") = dset.Tables("DETAILS").Rows(i)("REMARKS").ToString()
                    drNewRow("Total Cost") = dset.Tables("DETAILS").Rows(i)("TOTALCOST").ToString()
                    drNewRow("CPS Gross") = dset.Tables("DETAILS").Rows(i)("CPS_GROSS").ToString()
                    drNewRow("CPS WOIC") = dset.Tables("DETAILS").Rows(i)("CPS_WOIC").ToString()
                    drNewRow("CPS WOHW") = dset.Tables("DETAILS").Rows(i)("CPS_WOHW").ToString()
                    drNewRow("UpfrontGuaranted By") = dset.Tables("DETAILS").Rows(i)("UPFRONTGUARANTEEBY").ToString()
                    drNewRow("Status") = dset.Tables("DETAILS").Rows(i)("STATUS").ToString()
                    drNewRow("Current Status") = dset.Tables("DETAILS").Rows(i)("CURRENTSTATUS").ToString()
                    objDT.Rows.Add(drNewRow)
                End If
            Next

            objDT.TableName = "Payment"

            '[BCaseId]
            '[Chain Code] [Group Name] [City] [Period] [No Of Month] [Amount] [Any ADJ] [Quarter Payment] 
            '[Segment Paid For] [Gross Segment] [Segment Gross WOIC] [Remarks] [Total Cost] [CPS Gross]  [CPS WOIC]
            '[CPS WOHW]  [UpfrontGuaranted By]  [Status] [Current Status]

            ' Write the data out as XML with an Excel extension.
            'objDT.WriteXml(strPath, System.Data.XmlWriteMode.IgnoreSchema)

            Dim objExApplication As New Excel.Application
            Dim objExWorkBooks As Excel.Workbooks
            Dim objExWorkBook As Excel.Workbook
            Dim objExWorkSheet As Excel.Worksheet
            Dim objTemplatePath As String
            Dim stringArray As Object(,) = New Object(objDT.Rows.Count, objDT.Columns.Count - 1) {}

            'Add Column Header
            For x = 0 To objDT.Columns.Count - 1
                stringArray(0, x) = objDT.Columns(x).ColumnName.ToString()
            Next

            ' Add Column data
            For row As Integer = 0 To objDT.Rows.Count - 1
                For col As Integer = 0 To objDT.Columns.Count - 1
                    stringArray(row + 1, col) = objDT.Rows(row)(col).ToString()
                Next
            Next

            objTemplatePath = Server.MapPath("~\Template\PaymentSummery.xls")

            objExWorkBooks = objExApplication.Workbooks
            objExWorkBook = objExWorkBooks.Open(objTemplatePath)
            objExWorkSheet = objExApplication.ActiveSheet

            columnCount = objDT.Columns.Count
            rowCount = objDT.Rows.Count
            rowCount = rowCount + 1

            If rowCount <> 0 Then
                boolRec = True
                Dim lastColumn As String = GetLastColumnName(columnCount)
                Dim usedRange As String = "A1:" + lastColumn + rowCount.ToString()

                objExWorkSheet.Range(usedRange).Value2 = stringArray
                objExWorkSheet.Range(usedRange.ToString()).Borders.LineStyle = True

                objExWorkSheet.Range("A" + (1).ToString(), "T" + (rowCount + 10).ToString()).Columns.AutoFit()
                'objExWorkSheet.Range("A" + (1).ToString(), "T" + (rowCount + 10).ToString()).WrapText = True
                objTemplatePath = Server.MapPath("~\MailAttachment\" + strUniqueFn)
                strUniqueFn = objTemplatePath
                objExWorkBook.SaveAs(strUniqueFn)
                objExWorkBook.Close()
                objExWorkBooks.Close()
                objExApplication.Quit()
            End If

            'Send Mail Code
            strSource = System.Configuration.ConfigurationSettings.AppSettings("MAIL_SOURCE").Trim
            objInputSendMailXml.LoadXml(strSendMail_INPUT)

            With objInputSendMailXml.SelectSingleNode("SENDMAILIMMEDIATE_INPUT/MAIL_DETAILS")
                .Attributes("SOURCE").InnerText = strSource
                .Attributes("DESTINATION_TO").InnerText = strDestination
                .Attributes("SUBJECT").InnerText = "Payment Summery Report"
                .Attributes("MESSAGE").InnerText = strEmailBody
                .Attributes("ATTACHMENT_FILE").InnerText = objTemplatePath
            End With
            ObjSendMail = New AAMS.bizUtility.bzEmail
            boolSendMailStatus = ObjSendMail.SendMail(objInputSendMailXml)
            If boolSendMailStatus = True Then

            End If

        Catch ex As Exception

        Finally
            ObjSendMail = Nothing
            objInputSendMailXml = Nothing
        End Try
    End Sub

    Private Function GetLastColumnName(ByVal lastColumnIndex As Integer) As String
        Dim lastColumn As String = ""
        ' check whether the column count is > 26
        If lastColumnIndex > 26 Then
            ' If the column count is > 26, the the last column index will be something 
            ' like "AA", "DE", "BC" etc

            ' Get the first letter
            ' ASCII index 65 represent char. 'A'. So, we use 64 in this calculation as a starting point
            Dim first As Char = Convert.ToChar(64 + ((lastColumnIndex - 1) \ 26))

            ' Get the second letter
            Dim second As Char = Convert.ToChar(64 + (IIf(lastColumnIndex Mod 26 = 0, 26, lastColumnIndex Mod 26)))

            ' Concat. them
            lastColumn = first.ToString() & second.ToString()
        Else
            ' ASCII index 65 represent char. 'A'. So, we use 64 in this calculation as a starting point
            lastColumn = Convert.ToChar(64 + lastColumnIndex).ToString()
        End If
        Return lastColumn
    End Function


End Class
