Imports System.Threading
Imports System.Xml
Imports System.Data
Imports System.Globalization
Imports System.Web.Services
Imports System.Web
Imports System.IO
Imports System.Collections.Generic

Imports System.Web.Script.Services
Imports System.Web.Services.Protocols
Imports AjaxControlToolkit
Imports System.Configuration
Partial Class Incentive_INCSR_Pay_ApprovalQueue
    Inherits System.Web.UI.Page
    Const strSendMail_INPUT = "<SENDMAILIMMEDIATE_INPUT><MAIL_DETAILS DESTINATION_TO='' SUBJECT='' MESSAGE='' SOURCE='' DESTINATION_CC='' DESTINATION_BCC='' ATTACHMENT_FILE=''/></SENDMAILIMMEDIATE_INPUT>"
    ' Implements System.Web.UI.ICallbackEventHandler
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim str As String
    WithEvents chkSelect As CheckBox
    Dim objED As New EncyrptDeCyrpt
    Dim strBuilder As New StringBuilder
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim strError As String = ""

        System.Threading.Thread.Sleep(1000)
        Session("PageName") = Request.Url.ToString()
        objeAAMS.ExpirePageCache()

        BtnExport.Attributes.Add("onclick", "return CheckValidation();")
        ' BtnSearch.Attributes.Add("onclick", "return CheckValidation();")


        lblError.Text = String.Empty
        LblTotal.Text = ""
        '***************************************************************************************
        'Code of Security Check
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            lblError.Text = "Session is expired."
            Exit Sub
        End If
        Try
            gvIspPaymentRec.PageSize = ConfigurationManager.AppSettings("PAGE_SIZE").ToString

            'Dim m As ClientScriptManager = Me.ClientScript
            'str = m.GetCallbackEventReference(Me, "args", "ReceiveServerData", "'this is context from server'")
            'Dim strCallback As String = "function CallServer(args,context){" + str + ";}"
            'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)
            ' @ Code added for Firing event by checkbox of Grid

            If Session("PaymentRecDataSource") IsNot Nothing Then

                For i As Integer = 0 To gvIspPaymentRec.Rows.Count - 1

                    Dim hdPANumber As HiddenField
                    Dim hdRowno As HiddenField
                    Dim chkPTID As CheckBox
                    Dim HdFinalApproved As HiddenField

                    hdPANumber = gvIspPaymentRec.Rows(i).FindControl("hdPANumber")
                    chkPTID = gvIspPaymentRec.Rows(i).FindControl("chkPTID")
                    hdRowno = gvIspPaymentRec.Rows(i).FindControl("hdRowno")
                    HdFinalApproved = gvIspPaymentRec.Rows(i).FindControl("HdFinalApproved")


                    If HdFinalApproved.Value = "" Or HdFinalApproved.Value.Trim.ToUpper() = "FALSE" Then
                        chkPTID.Visible = True
                        Dim id As String
                        id = chkPTID.ClientID
                        Dim hdCheckUncheckStatus As HiddenField
                        hdCheckUncheckStatus = gvIspPaymentRec.Rows(i).FindControl("hdCheckUncheckStatus")

                        'If hdCheckUncheckStatus.Value = "" Or hdCheckUncheckStatus.Value = "False" Then
                        '    chkPTID.Checked = False
                        'Else
                        '    chkPTID.Checked = True
                        'End If
                        '' chkPTID.Attributes.Add("onclick", "return SendAoffice('" + id + "','" + hdRowno.Value + "');")

                        'dpBehaviorModFare.BehaviorID = dpBehaviorModFare.ClientID
                        'chkPTID.Attributes.Add("onclick", "return  ExpandModels('" + hdRowno.Value + "','" + id + "','" + dpBehaviorModFare.BehaviorID + "');")
                    Else
                        chkPTID.Visible = False
                    End If
                Next
            Else
                gvIspPaymentRec.DataSource = Nothing
                gvIspPaymentRec.DataBind()
                pnlPaging.Visible = False
            End If




            '@ Code added for Firing event by checkbox of Grid



            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Payment Approval Queue']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Payment Approval Queue']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        BtnSearch.Enabled = False
                    End If
                    If strBuilder(4) = "0" Then
                        BtnExport.Enabled = False
                    End If
                    If strBuilder(1) = "0" Then
                        ' btnPayment.Enabled = False
                    End If

                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PaymentReject']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PaymentReject']").Attributes("Value").Value)
                    If strBuilder(1) = "0" And strBuilder(2) = "0" Then
                        BtnReject.Enabled = False
                    Else
                        BtnReject.Enabled = True
                    End If
                Else
                    BtnReject.Enabled = False
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                BtnReject.Enabled = True
            End If

            If Not Page.IsPostBack Then
                BindDropDowns()
                Session("TotalSum") = ""
                Session("Year") = Nothing
                Session("Month") = Nothing
                Session("Status") = Nothing
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            ' Session("CheckedItem") = HttpContext.Current.Session("CheckedItem")
        End Try
    End Sub


#Region "BindDropDowns()"
    Private Sub BindDropDowns()
        Try
            Dim i As Integer = 0

            Dim IntYearFrom As Integer = 3
            Dim IntYearTo As Integer = 3
            'If System.Configuration.ConfigurationManager.AppSettings("YearFrom") IsNot Nothing Then
            '    IntYearFrom = Val(System.Configuration.ConfigurationManager.AppSettings("YearFrom"))
            'End If
            'If System.Configuration.ConfigurationManager.AppSettings("YearTo") IsNot Nothing Then
            '    IntYearTo = Val(System.Configuration.ConfigurationManager.AppSettings("YearTo"))
            'End If

            For j As Integer = DateTime.Now.Year + IntYearFrom To DateTime.Now.Year - IntYearTo Step -1
                drpYears.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                i += 1
            Next
            drpYears.SelectedValue = DateTime.Now.Year
            drpMonths.SelectedValue = DateTime.Now.Month

            objeAAMS.BindDropDown(DlstStatus, "INCENTIVE_STATUS", True, 3)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        Try
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                lblError.Text = "Session is expired."
                Exit Sub
            End If
            Session("TotalSum") = Nothing
            binddata()
            CheckedItem()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Private Sub binddata()
        Session("TotalSum") = Nothing
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzPaymentApprovalQue As New AAMS.bizIncetive.bzPaymentApprovalQue
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Try

            objInputXml.LoadXml("<UP_SER_INC_PAYMENT_APPROVAL_QUE_INPUT> <CHAIN_CODE></CHAIN_CODE>  <GROUPNAME></GROUPNAME>   <AOFFICE></AOFFICE>   <MONTH></MONTH><YEAR></YEAR>    <STATUS></STATUS>  <EMPLOYEEID></EMPLOYEEID>    <REC_DATE></REC_DATE>      <PAGE_NO></PAGE_NO>   <PAGE_SIZE></PAGE_SIZE>    <SORT_BY></SORT_BY>    <DESC></DESC>     </UP_SER_INC_PAYMENT_APPROVAL_QUE_INPUT>")


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

            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If
            objInputXml.DocumentElement.SelectSingleNode("MONTH").InnerText = drpMonths.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("YEAR").InnerText = drpYears.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("GROUPNAME").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = ""
            If DlstStatus.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("STATUS").InnerText = DlstStatus.SelectedValue
            End If

            'objInputXml.DocumentElement.SelectSingleNode("PEN_PAY_APROVAL").InnerText = ChkPPA.Checked

            objInputXml.DocumentElement.SelectSingleNode("REC_DATE").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ""



            'Here Back end Method Call
            ' hdChechedItem.Value = "0"

            ' Session("PaymentRecDataSource") = Nothing
            '  ddlPageNumber.SelectedValue = 1

            '  objOutputXml = objbzISPOrder.PaymentProceedReport(objInputXml)

            '  <UP_SER_INC_PAYMENT_APPROVAL_QUE_OUTPUT>
            '      <DETAILS PAYMENT_ID='' BC_ID ='' CHAIN_CODE='' 
            'GROUPNAME ='' AOFFICE='' MONTH='' YEAR=''  REC_DATE=''
            'STATUS ='' CURRENT_STATUS='' PA_FINAL_APPROVED=''/>
            '      <Errors Status=''>
            '               <Error Code='' Description='' />    
            '      </Errors>  
            '</UP_SER_INC_PAYMENT_APPROVAL_QUE_OUTPUT>


            '<UP_SER_INC_PAYMENT_APPROVAL_QUE_OUTPUT>

            '<DETAILS ROWNO='' PAYMENT_ID='' STATUS =''
            ' CURRENTSTATUS='' PA_FINAL_APPROVED='' BC_ID ='' 
            'CHAIN_CODE='' CITY='' PERIOD='' NOOFMONTHS=''
            ' AMOUNT='' ANYADJ='' THISQUARTERPAYMENT=''
            ' SEGMENTS_PAID_FOR='' GROSSSEGMENTS='' 
            'SEGMENT='' GROSS_WOIC='' SOLE='' REMARKS=''
            ' TOTALCOST='' CPS_GROSS='' CPS_WOIC='' CPS_WOHW=''
            ' UPFRONTGUARANTEEBY=''/>

            '     <Errors Status=''>

            '              <Error Code='' Description='' />       

            '     </Errors>   

            '</UP_SER_INC_PAYMENT_APPROVAL_QUE_OUTPUT>
            hdChechedItem.Value = "0"
            Session("PaymentRecDataSource") = Nothing
            Session("CheckedItem") = ""
            ddlPageNumber.SelectedValue = 1
            hdUpdateForSessionXml.Value = ""

            'objOutputXml.LoadXml("<UP_SER_INC_PAYMENT_APPROVAL_QUE_OUTPUT><DETAILS    ROWNO = '1'   PAYMENT_ID = '28'  CheckUncheckStatus = ''    BC_ID = '127'  CHAIN_CODE = '19308'   GROUPNAME = 'T B N TRAVEL BUSINESS'    CITY = 'New Delhi'   PERIOD = 'Jul-09 - Aug-09'   NOOFMONTHS = '1'   AMOUNT = '150000'     ANYADJ = ''         THISQUARTERPAYMENT = '150000'            SEGMENTS_PAID_FOR = '0'     GROSSSEGMENTS = '0'    SEGMENT = ''   GROSS_WOIC = '0'  SOLE = 'False'     REMARKS = ''     TOTALCOST = '0'    CPS_GROSS = '0'   CPS_WOIC = '0'    CPS_WOHW = '0'  UPFRONTGUARANTEEBY = '0'  STATUS = 'Final Approved'   CURRENTSTATUS = '' PA_FINAL_APPROVED='' /><DETAILS    ROWNO = '2'   PAYMENT_ID = '28'  CheckUncheckStatus = ''    BC_ID = '127'  CHAIN_CODE = '19308'   GROUPNAME = 'T B N TRAVEL BUSINESS'    CITY = 'New Delhi'   PERIOD = 'Jul-09 - Aug-09'   NOOFMONTHS = '1'   AMOUNT = '150000'     ANYADJ = ''         THISQUARTERPAYMENT = '150000'            SEGMENTS_PAID_FOR = '0'     GROSSSEGMENTS = '0'    SEGMENT = ''   GROSS_WOIC = '0'  SOLE = 'False'     REMARKS = ''     TOTALCOST = '0'    CPS_GROSS = '0'   CPS_WOIC = '0'    CPS_WOHW = '0'  UPFRONTGUARANTEEBY = '0'  STATUS = 'Final Approved'   CURRENTSTATUS = 'Final Approved' PA_FINAL_APPROVED='True' /> <DETAILS    ROWNO = '3'   PAYMENT_ID = '28'  CheckUncheckStatus = ''    BC_ID = '127'  CHAIN_CODE = '19308'   GROUPNAME = 'T B N TRAVEL BUSINESS'    CITY = 'New Delhi'   PERIOD = 'Jul-09 - Aug-09'   NOOFMONTHS = '1'   AMOUNT = '150000'     ANYADJ = ''         THISQUARTERPAYMENT = '150000'            SEGMENTS_PAID_FOR = '0'     GROSSSEGMENTS = '0'    SEGMENT = ''   GROSS_WOIC = '0'  SOLE = 'False'     REMARKS = ''     TOTALCOST = '0'    CPS_GROSS = '0'   CPS_WOIC = '0'    CPS_WOHW = '0'  UPFRONTGUARANTEEBY = '0'  STATUS = 'Approved'   CURRENTSTATUS = 'Approved' PA_FINAL_APPROVED='' />  <PAGE PAGE_COUNT='1' TOTAL_ROWS='3'/>  <Errors Status='False'>          <Error Code='' Description='' />          </Errors>   </UP_SER_INC_PAYMENT_APPROVAL_QUE_OUTPUT>")

            objOutputXml = objbzPaymentApprovalQue.SearchPaymentApprovalQue(objInputXml)

            'objOutputXml.Save("c:\PaymentApprovalQue.xml")
            Try
                objOutputXml.Save("C:\admin\PaymentApprovalQue.xml")
            Catch ex As Exception
            End Try
            'objOutputXml.Load("C:\AAMSXml\PaymentApprovalQue.xml")

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                DivFAColor.visible = True
                ViewState("InputXml") = objInputXml.OuterXml

                hdUpdateForSessionXml.Value = objOutputXml.OuterXml

                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)

                '@ Added Code for Sorting and Paging
                'Dim dsets As New DataSet
                'dsets = ds
                'Session("PaymentRecDataSource") = ds
                'Dim Clmn As String = ""
                'If ViewState("Desc") Is Nothing Then
                '    ViewState("Desc") = "FALSE"
                'End If
                'If ViewState("SortName") Is Nothing Then
                '    ViewState("SortName") = "CHAIN_CODE"
                '    If ViewState("Desc") = "FALSE" Then
                '        '  bubbleSortAsc(ds, ViewState("SortName"), dsets)
                '    Else
                '        ' bubbleSortDesc(ds, ViewState("SortName"), dsets)
                '    End If
                'Else
                '    If ViewState("Desc") = "FALSE" Then
                '        ' bubbleSortAsc(ds, ViewState("SortName"), dsets)
                '    Else
                '        '  bubbleSortDesc(ds, ViewState("SortName"), dsets)
                '    End If
                'End If

                gvIspPaymentRec.PageIndex = 0
                gvIspPaymentRec.DataSource = Nothing
                gvIspPaymentRec.DataBind()

                Session("PaymentRecDataSource") = ds

                gvIspPaymentRec.DataSource = ds.Tables("DETAILS").DefaultView
                gvIspPaymentRec.DataBind()
                btnSelectAll.Visible = True
                btnDeSelectAll.Visible = False

                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                'SetImageForSorting(gvIspPaymentRec)
                pnlPaging.Visible = True
                BindControlsForNavigation(gvIspPaymentRec.PageCount)
                total()

            Else
                gvIspPaymentRec.DataSource = Nothing
                gvIspPaymentRec.DataBind()

                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
                ' hdUpdateForSessionXml.Value = ""
                btnSelectAll.Visible = False
                btnDeSelectAll.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message

        Finally
            objOutputXml = Nothing
            objInputXml = Nothing
            '  objbzISPOrder = Nothing

            ShowHideSelectAllButton()
           
        End Try
    End Sub

    Private Sub binddata(ByVal InputstrXml As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzISPOrder As New AAMS.bizISP.bzISPOrder
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim objbzPaymentApprovalQue As New AAMS.bizIncetive.bzPaymentApprovalQue
        Try

            objInputXml.LoadXml(InputstrXml)


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




            'Here Back end Method Call
            ' hdChechedItem.Value = "0"

            ' Session("PaymentRecDataSource") = Nothing
            ddlPageNumber.SelectedValue = 1

            '  objOutputXml = objbzISPOrder.PaymentProceedReport(objInputXml)

            '  <UP_SER_INC_PAYMENT_APPROVAL_QUE_OUTPUT>
            '      <DETAILS PAYMENT_ID='' BC_ID ='' CHAIN_CODE='' 
            'GROUPNAME ='' AOFFICE='' MONTH='' YEAR=''  REC_DATE=''
            'STATUS ='' CURRENT_STATUS='' PA_FINAL_APPROVED=''/>
            '      <Errors Status=''>
            '               <Error Code='' Description='' />    
            '      </Errors>  
            '</UP_SER_INC_PAYMENT_APPROVAL_QUE_OUTPUT>



            hdChechedItem.Value = "0"

            Session("PaymentRecDataSource") = Nothing
            Session("CheckedItem") = ""
            ddlPageNumber.SelectedValue = 1
            hdUpdateForSessionXml.Value = ""


            'objOutputXml.LoadXml("<UP_SER_INC_PAYMENT_APPROVAL_QUE_OUTPUT><DETAILS ROWNO='1' PAYMENT_ID='1' STATUS ='' GROUPNAME='' CURRENTSTATUS='' PA_FINAL_APPROVED='False' BC_ID ='2' CHAIN_CODE='' CITY='' PERIOD='' NOOFMONTHS='' AMOUNT='' ANYADJ='' THISQUARTERPAYMENT='' SEGMENTS_PAID_FOR='' GROSSSEGMENTS='' SEGMENT='' GROSS_WOIC='' SOLE='' REMARKS='' TOTALCOST='' CPS_GROSS='' CPS_WOIC='' CPS_WOHW='' UPFRONTGUARANTEEBY=''/>  <DETAILS ROWNO='2' PAYMENT_ID='2' STATUS ='' CURRENTSTATUS='' PA_FINAL_APPROVED='True' BC_ID ='2' CHAIN_CODE='' CITY='' PERIOD='' NOOFMONTHS='' AMOUNT='' ANYADJ='' THISQUARTERPAYMENT='' SEGMENTS_PAID_FOR='' GROSSSEGMENTS='' SEGMENT='' GROSS_WOIC='' SOLE='' REMARKS='' TOTALCOST='' CPS_GROSS='' CPS_WOIC='' CPS_WOHW='' UPFRONTGUARANTEEBY=''/> <DETAILS ROWNO='3' PAYMENT_ID='3' STATUS ='' CURRENTSTATUS='' PA_FINAL_APPROVED='False' BC_ID ='2' CHAIN_CODE='' CITY='' PERIOD='' NOOFMONTHS='' AMOUNT='' ANYADJ='' THISQUARTERPAYMENT='' SEGMENTS_PAID_FOR='' GROSSSEGMENTS='' SEGMENT='' GROSS_WOIC='' SOLE='' REMARKS='' TOTALCOST='' CPS_GROSS='' CPS_WOIC='' CPS_WOHW='' UPFRONTGUARANTEEBY=''/>  <PAGE PAGE_COUNT='1' TOTAL_ROWS='3'/> <Errors Status='False'>          <Error Code='' Description='' />          </Errors>   </UP_SER_INC_PAYMENT_APPROVAL_QUE_OUTPUT>")
            objOutputXml = objbzPaymentApprovalQue.SearchPaymentApprovalQue(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                DivFAColor.visible = True
                ViewState("InputXml") = objInputXml.OuterXml
                hdUpdateForSessionXml.Value = objOutputXml.OuterXml

                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)





                '@ Added Code for Sorting and Paging
                'Dim dsets As New DataSet
                'dsets = ds
                'Session("PaymentRecDataSource") = ds
                'Dim Clmn As String = ""
                'If ViewState("Desc") Is Nothing Then
                '    ViewState("Desc") = "FALSE"
                'End If
                'If ViewState("SortName") Is Nothing Then
                '    ViewState("SortName") = "CHAIN_CODE"
                '    If ViewState("Desc") = "FALSE" Then
                '        '  bubbleSortAsc(ds, ViewState("SortName"), dsets)
                '    Else
                '        ' bubbleSortDesc(ds, ViewState("SortName"), dsets)
                '    End If
                'Else
                '    If ViewState("Desc") = "FALSE" Then
                '        ' bubbleSortAsc(ds, ViewState("SortName"), dsets)
                '    Else
                '        '  bubbleSortDesc(ds, ViewState("SortName"), dsets)
                '    End If
                'End If

                gvIspPaymentRec.PageIndex = 0
                gvIspPaymentRec.DataSource = Nothing
                gvIspPaymentRec.DataBind()



                Session("PaymentRecDataSource") = ds
                gvIspPaymentRec.DataSource = ds.Tables("DETAILS").DefaultView
                gvIspPaymentRec.DataBind()


                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                'SetImageForSorting(gvIspPaymentRec)
                pnlPaging.Visible = True
                BindControlsForNavigation(gvIspPaymentRec.PageCount)

            Else
                gvIspPaymentRec.DataSource = Nothing
                gvIspPaymentRec.DataBind()

                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False


                ' hdUpdateForSessionXml.Value = ""
            End If
        Catch ex As Exception
            lblError.Text = ex.Message

        Finally
            objOutputXml = Nothing
            objInputXml = Nothing
            objbzISPOrder = Nothing
            ShowHideSelectAllButton()
        End Try
    End Sub


 

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
          
            Response.Redirect(Request.Url.ToString, False)
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub gvIspPaymentRec_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvIspPaymentRec.RowDataBound
        Try

            If (e.Row.RowIndex < 0) Then
                Exit Sub
            End If
            Dim hdPANumber As HiddenField

            Dim chkPTID As CheckBox
            hdPANumber = e.Row.FindControl("hdPANumber")
            chkPTID = e.Row.FindControl("chkPTID")

            Dim lnkPayment As LinkButton
            Dim hdRowno As HiddenField
            hdRowno = e.Row.FindControl("hdRowno")

            Dim hdInput As HiddenField
            hdInput = e.Row.FindControl("hdInput")
            Dim hdCheckUncheckStatus As HiddenField
            hdCheckUncheckStatus = e.Row.FindControl("hdCheckUncheckStatus")


           
            Dim HdFinalApproved As HiddenField

            HdFinalApproved = e.Row.FindControl("HdFinalApproved")

            Dim hdDecChainCode As String = hdInput.Value.ToString().Split("|")(1)
            Dim hdEnChainCode As String = objED.Encrypt(hdDecChainCode)

            lnkPayment = CType(e.Row.FindControl("lnkPayment"), LinkButton)

            Dim hdPayType As HiddenField

            hdPayType = e.Row.FindControl("hdPayType")

            Dim HdCurPayNo As HiddenField
            HdCurPayNo = CType(e.Row.FindControl("HdCurPayNo"), HiddenField)

            'ashish on 01-11-2010 Payment sheet Report
            Dim lnkPaymentSheet As LinkButton
            lnkPaymentSheet = CType(e.Row.FindControl("lnkPaymentSheet"), LinkButton)
            'end

            '@ Start of code  Added on 22 Feb 2011
            Dim StrPaymentPeriod As String = ""
            Dim HDPLBCYCLE As HiddenField
            Dim HDPLBPERIODFROM As HiddenField
            Dim HDPLBPERIODTO As HiddenField
            HDPLBCYCLE = CType(e.Row.FindControl("HDPLBCYCLE"), HiddenField)
            If HDPLBCYCLE.Value.Trim.ToUpper = "TRUE" Then
                HDPLBCYCLE.Value = "1"
            Else
                HDPLBCYCLE.Value = "0"
            End If
            HDPLBPERIODFROM = CType(e.Row.FindControl("HDPLBPERIODFROM"), HiddenField)
            HDPLBPERIODFROM.Value = objeAAMS.ConvertDate(HDPLBPERIODFROM.Value).ToString("dd/MM/yyyy")
            HDPLBPERIODTO = CType(e.Row.FindControl("HDPLBPERIODTO"), HiddenField)
            HDPLBPERIODTO.Value = objeAAMS.ConvertDate(HDPLBPERIODTO.Value).ToString("dd/MM/yyyy")
            StrPaymentPeriod = HDPLBPERIODFROM.Value.Trim + "-" + HDPLBPERIODTO.Value.Trim
            '@ End of code Added on 22 Feb 2011



            lnkPayment.Attributes.Add("onclick", "return Payment(" & "'" & hdInput.Value.ToString().Split("|")(0) & "'" + ",'" & hdEnChainCode & "'" + ",'" & drpMonths.SelectedValue & "'" + ",'" & drpYears.SelectedValue & "'" + ",'" & hdPANumber.Value.ToString & "','" & hdPayType.Value.ToString.ToUpper & "','" & HdCurPayNo.Value.ToString.ToUpper & "','" & StrPaymentPeriod.ToString.Trim & "','" & HDPLBCYCLE.Value.Trim & "'" & ");")
           

            If HdFinalApproved.Value = "" Or HdFinalApproved.Value.Trim.ToUpper() = "FALSE" Then
                chkPTID.Visible = True
                Dim id As String
                id = chkPTID.ClientID
                If hdCheckUncheckStatus.Value = "" Or hdCheckUncheckStatus.Value = "False" Then
                    chkPTID.Checked = False
                Else
                    chkPTID.Checked = True
                End If
                ' chkPTID.Attributes.Add("onclick", "return SendAoffice('" + id + "','" + hdRowno.Value + "');")

                dpTotalInWord.BehaviorID = dpTotalInWord.ClientID
                dpTotalInNumeric.BehaviorID = dpTotalInNumeric.ClientID


                chkPTID.Attributes.Add("onclick", "return  ExpandModels('" + hdRowno.Value + "','" + id + "','" + dpTotalInWord.BehaviorID + "','" + dpTotalInNumeric.ClientID + "');")
            Else
                chkPTID.Visible = False
                e.Row.BackColor = Drawing.Color.LightSeaGreen
                e.Row.ForeColor = Drawing.Color.Black

            End If



            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))

            'ASHISH ON 01-11-2010 FOR THE DISPLAY PAYMENT SHEET REPORT 
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PaymentSheet']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PaymentSheet']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        lnkPaymentSheet.Enabled = False
                    Else
                        lnkPaymentSheet.Attributes.Add("onclick", "return PaymentSheet(" & "'" & hdInput.Value.ToString().Split("|")(0) & "'" + ",'" & hdEnChainCode & "'" + ",'" & drpMonths.SelectedValue & "'" + ",'" & drpYears.SelectedValue & "','" & hdPayType.Value.ToString.Trim.ToUpper & "','" & HdCurPayNo.Value.ToString.Trim.ToUpper & "'" & ");")
                    End If

                Else
                    lnkPaymentSheet.Enabled = False
                End If
            Else
                lnkPaymentSheet.Attributes.Add("onclick", "return PaymentSheet(" & "'" & hdInput.Value.ToString().Split("|")(0) & "'" + ",'" & hdEnChainCode & "'" + ",'" & drpMonths.SelectedValue & "'" + ",'" & drpYears.SelectedValue & "','" & hdPayType.Value.ToString.Trim.ToUpper & "','" & HdCurPayNo.Value.ToString.Trim.ToUpper & "'" & ");")

            End If
            ''end ashish

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
        
    End Sub

    Protected Sub btnSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        Try
            Dim objXmlReader As XmlNodeReader
            Dim ds2 As New DataSet
            Dim objOutputIspOrderSearchXml As New XmlDocument

            Dim sum As Double = 0

            Dim chkPTID As CheckBox
            Dim hdPANumber As HiddenField
            Dim HdFinalApproved As HiddenField
            Dim i As Integer
            If (gvIspPaymentRec.Rows.Count <= 0) Then
                lblError.Text = "There is no No Row For Selection."
            End If
            For i = 0 To gvIspPaymentRec.Rows.Count - 1

                hdPANumber = gvIspPaymentRec.Rows(i).FindControl("hdPANumber")
                chkPTID = gvIspPaymentRec.Rows(i).FindControl("chkPTID")
                HdFinalApproved = gvIspPaymentRec.Rows(i).FindControl("HdFinalApproved")
                'If hdPANumber.Value = "" Then
                '    chkPTID.Checked = True
                'Else
                '    chkPTID.Checked = False
                'End If
                If chkPTID.Visible = True Then
                    chkPTID.Checked = True
                End If
               
            Next
            '  LblTotal.Text = String.Format("<span style='font-family:courier new;font-weight:bold;'>{0}</span>", " Total Sum =    " + sum.ToString) ' "Total Sum= " + sum.ToString

            btnSelectAll.Visible = False
            btnDeSelectAll.Visible = True

            '@Now Update the SessionDataource as well as  hdUpdateForSessionXml

            If Session("PaymentRecDataSource") IsNot Nothing Then
                Dim dset As DataSet = CType(Session("PaymentRecDataSource"), DataSet)

                hdUpdateForSessionXml.Value = dset.GetXml()

                If hdUpdateForSessionXml.Value <> "" Then
                    '  gvIspPaymentRec.PageIndex = CInt(ddlPageNumber.SelectedValue) - 1
                    objOutputIspOrderSearchXml.LoadXml(hdUpdateForSessionXml.Value)
                    For Each objxmlnode As XmlNode In objOutputIspOrderSearchXml.DocumentElement.SelectNodes("DETAILS")

                        If objxmlnode.Attributes("PA_FINAL_APPROVED").Value.Trim().ToUpper <> "TRUE" Then
                            objxmlnode.Attributes("CheckUncheckStatus").Value = "True"

                        End If

                    Next
                    hdUpdateForSessionXml.Value = objOutputIspOrderSearchXml.OuterXml
                    objOutputIspOrderSearchXml.LoadXml(hdUpdateForSessionXml.Value)
                    objXmlReader = New XmlNodeReader(objOutputIspOrderSearchXml)
                    ds2.ReadXml(objXmlReader)
                    Session("PaymentRecDataSource") = ds2

                    'If objOutputIspOrderSearchXml.DocumentElement.SelectNodes("DETAILS[@CheckUncheckStatus ='True']").Count > 0 Then
                    '    Session("Checked") = "TRUE"
                    '    hdChechedItem.Value = "1"
                    'Else
                    '    Session("Checked") = "FALSE"
                    '    hdChechedItem.Value = "0"
                    'End If


                End If

                '  SetImageForSorting(gvIspPaymentRec)

            End If


        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            total()
            CheckedItem()
        End Try
    End Sub

    Protected Sub btnDeSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeSelectAll.Click

        Dim objXmlReader As XmlNodeReader
        Dim ds2 As New DataSet
        Dim objOutputIspOrderSearchXml As New XmlDocument
        Dim chkPTID As CheckBox
        Dim hdPANumber As HiddenField
        Dim HdFinalApproved As HiddenField
        Dim i As Integer
        Try
            If (gvIspPaymentRec.Rows.Count <= 0) Then
                lblError.Text = "There is no No Row For DeSelection."
            End If
            For i = 0 To gvIspPaymentRec.Rows.Count - 1

                hdPANumber = gvIspPaymentRec.Rows(i).FindControl("hdPANumber")
                chkPTID = gvIspPaymentRec.Rows(i).FindControl("chkPTID")
                HdFinalApproved = gvIspPaymentRec.Rows(i).FindControl("HdFinalApproved")
                'If hdPANumber.Value = "" Then
                '    chkPTID.Visible = True
                '    chkPTID.Checked = False
                'Else
                '    chkPTID.Visible = False
                '    chkPTID.Checked = False
                'End If
                chkPTID.Checked = False
            Next

            btnDeSelectAll.Visible = False
            btnSelectAll.Visible = True


            '   LblTotal.Text = String.Format("<span style='font-family:courier new;font-weight:bold;'>{0}</span>", " Total Sum =  ( 0 )   " + "Rupees Zero Only.") ' "Total Sum = 0"

            LblToTalInNum.Text = String.Format("<span style='font-family:courier new;font-weight:bold;'>{0}</span>", " Total Sum =     " + 0.ToString) '"Total Sum= " + sum.ToString

            '@Now Update the SessionDataource as well as  hdUpdateForSessionXml

            If Session("PaymentRecDataSource") IsNot Nothing Then
                Session("CheckedItem") = "0"
                Dim dset As DataSet = CType(Session("PaymentRecDataSource"), DataSet)

                hdUpdateForSessionXml.Value = dset.GetXml()

                If hdUpdateForSessionXml.Value <> "" Then

                    objOutputIspOrderSearchXml.LoadXml(hdUpdateForSessionXml.Value)
                    For Each objxmlnode As XmlNode In objOutputIspOrderSearchXml.DocumentElement.SelectNodes("DETAILS")
                        objxmlnode.Attributes("CheckUncheckStatus").Value = ""
                    Next
                    hdUpdateForSessionXml.Value = objOutputIspOrderSearchXml.OuterXml
                    objOutputIspOrderSearchXml.LoadXml(hdUpdateForSessionXml.Value)
                    objXmlReader = New XmlNodeReader(objOutputIspOrderSearchXml)
                    ds2.ReadXml(objXmlReader)
                    Session("PaymentRecDataSource") = ds2


                    'Session("Checked") = "FALSE"
                    'hdChechedItem.Value = "0"
                End If
                ' SetImageForSorting(gvIspPaymentRec)
            End If
            ''@
           
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            CheckedItem()
        End Try
    End Sub

   
    
#Region "Code for Filter "
    'Code Added by Abhishek
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                ' Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    'Code Added by Abhishek
#End Region
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Try
            If Session("Security") Is Nothing Then
                Response.Redirect("~/SupportPages/TimeOutSession.aspx?Logout=True", False)
                'ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession(), True)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Rating_Script_" + Me.ClientID.ToString(), objeAAMS.CheckSession(), True)
                'Exit Sub
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub Btnexp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnexp.Click
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            lblError.Text = "Session is expired."
            Exit Sub
        End If
        'imgLoad.visible = False
        ' UpDateProgress1.Visible = False
        ' Server.Execute("ExportApprovalQueue.aspx", False)
        '  imgLoad.attributes.add("display", "none")
        'Dim imgLoad As System.Web.UI.HtmlControls.HtmlImage
        'imgLoad = CType(UpDateProgress1.FindControl("imgLoad"), System.Web.UI.HtmlControls.HtmlImage)
        'imgLoad.Attributes.Add("display", "none")

        Session("Year") = drpYears.SelectedValue

        Session("Month") = drpMonths.SelectedValue

        Session("Status") = DlstStatus.SelectedValue
        '  Session("PEN_PAY_APROVAL") = ChkPPA.Checked



        ' Server.Transfer("MSSR_PaymentExportApprovalQueue.aspx?Type=Export", False)

        Response.Redirect("MSSR_PaymentExportApprovalQueue.aspx?Type=Export", False)

        'Dim objInputXml, objOutputXml As New XmlDocument
        'Dim objbzPaymentApprovalQue As New AAMS.bizIncetive.bzPaymentApprovalQue
        'Dim ds As New DataSet
        'Dim objSecurityXml As New XmlDocument
        'Dim objXmlReader As XmlNodeReader
        'Try

        '    objInputXml.LoadXml("<UP_SER_INC_PAYMENT_APPROVAL_QUE_INPUT> <CHAIN_CODE></CHAIN_CODE>  <GROUPNAME></GROUPNAME>   <AOFFICE></AOFFICE>   <MONTH></MONTH><YEAR></YEAR>    <STATUS></STATUS>  <EMPLOYEEID></EMPLOYEEID>    <REC_DATE></REC_DATE>       <PAGE_NO></PAGE_NO>   <PAGE_SIZE></PAGE_SIZE>    <SORT_BY></SORT_BY>    <DESC></DESC>     </UP_SER_INC_PAYMENT_APPROVAL_QUE_INPUT>")


        '    '   <UP_SER_INC_PAYMENT_APPROVAL_QUE_INPUT>
        '    '      <CHAIN_CODE></CHAIN_CODE>
        '    '      <GROUPNAME></GROUPNAME>
        '    '      <AOFFICE></AOFFICE>
        '    '      <MONTH></MONTH>
        '    '      <YEAR></YEAR>
        '    '      <STATUS></STATUS>
        '    '      <EMPLOYEEID></EMPLOYEEID>
        '    '      <REC_DATE></REC_DATE>   
        '    '      <PAGE_NO></PAGE_NO>
        '    '      <PAGE_SIZE></PAGE_SIZE>
        '    '      <SORT_BY></SORT_BY>
        '    '      <DESC></DESC>  
        '    '</UP_SER_INC_PAYMENT_APPROVAL_QUE_INPUT>

        '    If Not Session("LoginSession") Is Nothing Then
        '        objInputXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = Session("LoginSession").ToString().Split("|")(0)
        '    End If
        '    objInputXml.DocumentElement.SelectSingleNode("MONTH").InnerText = drpMonths.SelectedValue
        '    objInputXml.DocumentElement.SelectSingleNode("YEAR").InnerText = drpYears.SelectedValue
        '    objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = ""
        '    objInputXml.DocumentElement.SelectSingleNode("GROUPNAME").InnerText = ""
        '    objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = ""
        '    objInputXml.DocumentElement.SelectSingleNode("STATUS").InnerText = DlstStatus.SelectedValue

        '    objInputXml.DocumentElement.SelectSingleNode("REC_DATE").InnerText = ""
        '    objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = ""
        '    objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ""
        '    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ""
        '    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ""





        '    'Here Back end Method Call
        '    ' hdChechedItem.Value = "0"

        '    ' Session("PaymentRecDataSource") = Nothing
        '    '  ddlPageNumber.SelectedValue = 1

        '    '  objOutputXml = objbzISPOrder.DETAILSReport(objInputXml)

        '    '<DETAILS 
        '    '            ROWNO = "1"
        '    '            PAYMENT_ID = "28"
        '    '            CheckUncheckStatus = ""
        '    '            BC_ID = "127"
        '    '            CHAIN_CODE = "19308"
        '    '            GROUPNAME = "T B N TRAVEL BUSINESS"
        '    '            CITY = "New Delhi"
        '    '            PERIOD = "Jul-09 - Aug-09"
        '    '            NOOFMONTHS = "1"
        '    '            AMOUNT = "150000"
        '    '            ANYADJ = ""
        '    '            THISQUARTERPAYMENT = "150000"
        '    '            SEGMENTS_PAID_FOR = "0"
        '    '            GROSSSEGMENTS = "0"
        '    '            SEGMENT = ""
        '    '            GROSS_WOIC = "0"
        '    '            SOLE = "False"
        '    '            REMARKS = ""
        '    '            TOTALCOST = "0"
        '    '            CPS_GROSS = "0"
        '    '            CPS_WOIC = "0"
        '    '            CPS_WOHW = "0"
        '    '            UPFRONTGUARANTEEBY = "0"
        '    '            STATUS = "Final Approved"
        '    '            CURRENTSTATUS = "Final Approved"
        '    'PA_FINAL_APPROVED="True" /> 

        '    '<DETAILS    ROWNO = "1"   PAYMENT_ID = "28"  CheckUncheckStatus = ""    BC_ID = "127"  CHAIN_CODE = "19308"   GROUPNAME = "T B N TRAVEL BUSINESS"    CITY = "New Delhi"   PERIOD = "Jul-09 - Aug-09"   NOOFMONTHS = "1"   AMOUNT = "150000"     ANYADJ = ""         THISQUARTERPAYMENT = "150000"            SEGMENTS_PAID_FOR = "0"     GROSSSEGMENTS = "0"    SEGMENT = ""   GROSS_WOIC = "0"  SOLE = "False"     REMARKS = ""     TOTALCOST = "0"    CPS_GROSS = "0"   CPS_WOIC = "0"    CPS_WOHW = "0"  UPFRONTGUARANTEEBY = "0"  STATUS = "Final Approved"   CURRENTSTATUS = "" PA_FINAL_APPROVED="" />  '<DETAILS    ROWNO = "1"   PAYMENT_ID = "28"  CheckUncheckStatus = ""    BC_ID = "127"  CHAIN_CODE = "19308"   GROUPNAME = "T B N TRAVEL BUSINESS"    CITY = "New Delhi"   PERIOD = "Jul-09 - Aug-09"   NOOFMONTHS = "1"   AMOUNT = "150000"     ANYADJ = ""         THISQUARTERPAYMENT = "150000"            SEGMENTS_PAID_FOR = "0"     GROSSSEGMENTS = "0"    SEGMENT = ""   GROSS_WOIC = "0"  SOLE = "False"     REMARKS = ""     TOTALCOST = "0"    CPS_GROSS = "0"   CPS_WOIC = "0"    CPS_WOHW = "0"  UPFRONTGUARANTEEBY = "0"  STATUS = "Final Approved"   CURRENTSTATUS = "Final Approved" PA_FINAL_APPROVED="True" />   '<DETAILS    ROWNO = "1"   PAYMENT_ID = "28"  CheckUncheckStatus = ""    BC_ID = "127"  CHAIN_CODE = "19308"   GROUPNAME = "T B N TRAVEL BUSINESS"    CITY = "New Delhi"   PERIOD = "Jul-09 - Aug-09"   NOOFMONTHS = "1"   AMOUNT = "150000"     ANYADJ = ""         THISQUARTERPAYMENT = "150000"            SEGMENTS_PAID_FOR = "0"     GROSSSEGMENTS = "0"    SEGMENT = ""   GROSS_WOIC = "0"  SOLE = "False"     REMARKS = ""     TOTALCOST = "0"    CPS_GROSS = "0"   CPS_WOIC = "0"    CPS_WOHW = "0"  UPFRONTGUARANTEEBY = "0"  STATUS = "Final Approved"   CURRENTSTATUS = "Final Approved" PA_FINAL_APPROVED="True" /> 

        '    '<DETAILS    ROWNO = '1'   PAYMENT_ID = '28'  CheckUncheckStatus = ''    BC_ID = '127'  CHAIN_CODE = '19308'   GROUPNAME = 'T B N TRAVEL BUSINESS'    CITY = 'New Delhi'   PERIOD = 'Jul-09 - Aug-09'   NOOFMONTHS = '1'   AMOUNT = '150000'     ANYADJ = ''         THISQUARTERPAYMENT = '150000'            SEGMENTS_PAID_FOR = '0'     GROSSSEGMENTS = '0'    SEGMENT = ''   GROSS_WOIC = '0'  SOLE = 'False'     REMARKS = ''     TOTALCOST = '0'    CPS_GROSS = '0'   CPS_WOIC = '0'    CPS_WOHW = '0'  UPFRONTGUARANTEEBY = '0'  STATUS = 'Final Approved'   CURRENTSTATUS = '' PA_FINAL_APPROVED='' />  '<DETAILS    ROWNO = '1'   PAYMENT_ID = '28'  CheckUncheckStatus = ''    BC_ID = '127'  CHAIN_CODE = '19308'   GROUPNAME = 'T B N TRAVEL BUSINESS'    CITY = 'New Delhi'   PERIOD = 'Jul-09 - Aug-09'   NOOFMONTHS = '1'   AMOUNT = '150000'     ANYADJ = ''         THISQUARTERPAYMENT = '150000'            SEGMENTS_PAID_FOR = '0'     GROSSSEGMENTS = '0'    SEGMENT = ''   GROSS_WOIC = '0'  SOLE = 'False'     REMARKS = ''     TOTALCOST = '0'    CPS_GROSS = '0'   CPS_WOIC = '0'    CPS_WOHW = '0'  UPFRONTGUARANTEEBY = '0'  STATUS = 'Final Approved'   CURRENTSTATUS = 'Final Approved' PA_FINAL_APPROVED='True' />   '<DETAILS    ROWNO = '1'   PAYMENT_ID = '28'  CheckUncheckStatus = ''    BC_ID = '127'  CHAIN_CODE = '19308'   GROUPNAME = 'T B N TRAVEL BUSINESS'    CITY = 'New Delhi'   PERIOD = 'Jul-09 - Aug-09'   NOOFMONTHS = '1'   AMOUNT = '150000'     ANYADJ = ''         THISQUARTERPAYMENT = '150000'            SEGMENTS_PAID_FOR = '0'     GROSSSEGMENTS = '0'    SEGMENT = ''   GROSS_WOIC = '0'  SOLE = 'False'     REMARKS = ''     TOTALCOST = '0'    CPS_GROSS = '0'   CPS_WOIC = '0'    CPS_WOHW = '0'  UPFRONTGUARANTEEBY = '0'  STATUS = 'Final Approved'   CURRENTSTATUS = 'Final Approved' PA_FINAL_APPROVED='True' /> 
        '    '  objOutputXml.LoadXml("<UP_SER_INC_PAYMENT_APPROVAL_QUE_OUTPUT><DETAILS ROWNO='1' CheckUncheckStatus ='' GROUPNAME='' PAYMENT_ID='1' STATUS ='' CURRENTSTATUS='' PA_FINAL_APPROVED='False' BC_ID ='2' CHAIN_CODE='' CITY='' PERIOD='' NOOFMONTHS='' AMOUNT='' ANYADJ='' THISQUARTERPAYMENT='' SEGMENTS_PAID_FOR='' GROSSSEGMENTS='' SEGMENT='' GROSS_WOIC='' SOLE='' REMARKS='' TOTALCOST='' CPS_GROSS='' CPS_WOIC='' CPS_WOHW='' UPFRONTGUARANTEEBY=''/>  <DETAILS ROWNO='2' PAYMENT_ID='2' STATUS ='' CURRENTSTATUS='' PA_FINAL_APPROVED='True' BC_ID ='2' CHAIN_CODE='' CITY='' PERIOD='' NOOFMONTHS='' AMOUNT='' ANYADJ='' THISQUARTERPAYMENT='' SEGMENTS_PAID_FOR='' GROSSSEGMENTS='' SEGMENT='' GROSS_WOIC='' SOLE='' REMARKS='' TOTALCOST='' CPS_GROSS='' CPS_WOIC='' CPS_WOHW='' UPFRONTGUARANTEEBY=''/> <DETAILS ROWNO='3' PAYMENT_ID='3' STATUS ='' CURRENTSTATUS='' PA_FINAL_APPROVED='False' BC_ID ='2' CHAIN_CODE='' CITY='' PERIOD='' NOOFMONTHS='' AMOUNT='' ANYADJ='' THISQUARTERPAYMENT='' SEGMENTS_PAID_FOR='' GROSSSEGMENTS='' SEGMENT='' GROSS_WOIC='' SOLE='' REMARKS='' TOTALCOST='' CPS_GROSS='' CPS_WOIC='' CPS_WOHW='' UPFRONTGUARANTEEBY=''/>   <Errors Status='False'>          <Error Code='' Description='' />          </Errors>   </UP_SER_INC_PAYMENT_APPROVAL_QUE_OUTPUT>")

        '    objOutputXml = objbzPaymentApprovalQue.SearchPaymentApprovalQue(objInputXml)

        '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

        '        ViewState("InputXml") = objInputXml.OuterXml


        '        Dim objExport As New ExportExcel
        '        Dim strArray() As String = {"Chain Code", "Agency Name", "City", "Period", "No. Of Month", "Amount", "Any ADJ", "Quarter Payment", "Segment Paid For", "Gross Segment", "Gross Segment WOIC", "Sole", "Remarks", "Total Cost", "CPS Gross", "CPS WOIC", "CPS WOHW", "Upfront Guranteed By", "Status", "Current Status"}
        '        Dim intArray() As Integer = {4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24}
        '        objExport.ExportDetails(objOutputXml, "DETAILS", intArray, strArray, ExportExcel.ExportFormat.Excel, "PayQue.xls")



        '    Else
        '        gvIspPaymentRec.DataSource = Nothing
        '        gvIspPaymentRec.DataBind()
        '        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        '        pnlPaging.Visible = False
        '        LblTotal.Text = ""
        '        LblToTalInNum.Text = ""
        '        btnSelectAll.Visible = False
        '        btnDeSelectAll.Visible = False

        '    End If
        'Catch ex As Exception
        '    lblError.Text = ex.Message

        'Finally
        '    objOutputXml = Nothing
        '    objInputXml = Nothing
        '    ' objbzISPOrder = Nothing
        'End Try
    End Sub

    Protected Sub BtnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExport.Click
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            lblError.Text = "Session is expired."
            Exit Sub
        End If
        total()

        'imgLoad.visible = False
        ' UpDateProgress1.Visible = False
        ' Server.Execute("ExportApprovalQueue.aspx", False)
        '  imgLoad.attributes.add("display", "none")
        'Dim imgLoad As System.Web.UI.HtmlControls.HtmlImage
        'imgLoad = CType(UpDateProgress1.FindControl("imgLoad"), System.Web.UI.HtmlControls.HtmlImage)
        'imgLoad.Attributes.Add("display", "none")

        'Session("Year") = drpYears.SelectedValue

        'Session("Month") = drpMonths.SelectedValue

        'Session("Status") = DlstStatus.SelectedValue

        ' Server.Transfer("MSSR_PaymentExportApprovalQueue.aspx?Type=Export", False)

        ' Response.Redirect("MSSR_PaymentExportApprovalQueue.aspx?Type=Export", False)

        'Dim objInputXml, objOutputXml As New XmlDocument
        'Dim objbzPaymentApprovalQue As New AAMS.bizIncetive.bzPaymentApprovalQue
        'Dim ds As New DataSet
        'Dim objSecurityXml As New XmlDocument
        'Dim objXmlReader As XmlNodeReader
        'Try

        '    objInputXml.LoadXml("<UP_SER_INC_PAYMENT_APPROVAL_QUE_INPUT> <CHAIN_CODE></CHAIN_CODE>  <GROUPNAME></GROUPNAME>   <AOFFICE></AOFFICE>   <MONTH></MONTH><YEAR></YEAR>    <STATUS></STATUS>  <EMPLOYEEID></EMPLOYEEID>    <REC_DATE></REC_DATE>       <PAGE_NO></PAGE_NO>   <PAGE_SIZE></PAGE_SIZE>    <SORT_BY></SORT_BY>    <DESC></DESC>     </UP_SER_INC_PAYMENT_APPROVAL_QUE_INPUT>")


        '    '   <UP_SER_INC_PAYMENT_APPROVAL_QUE_INPUT>
        '    '      <CHAIN_CODE></CHAIN_CODE>
        '    '      <GROUPNAME></GROUPNAME>
        '    '      <AOFFICE></AOFFICE>
        '    '      <MONTH></MONTH>
        '    '      <YEAR></YEAR>
        '    '      <STATUS></STATUS>
        '    '      <EMPLOYEEID></EMPLOYEEID>
        '    '      <REC_DATE></REC_DATE>   
        '    '      <PAGE_NO></PAGE_NO>
        '    '      <PAGE_SIZE></PAGE_SIZE>
        '    '      <SORT_BY></SORT_BY>
        '    '      <DESC></DESC>  
        '    '</UP_SER_INC_PAYMENT_APPROVAL_QUE_INPUT>

        '    If Not Session("LoginSession") Is Nothing Then
        '        objInputXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = Session("LoginSession").ToString().Split("|")(0)
        '    End If
        '    objInputXml.DocumentElement.SelectSingleNode("MONTH").InnerText = drpMonths.SelectedValue
        '    objInputXml.DocumentElement.SelectSingleNode("YEAR").InnerText = drpYears.SelectedValue
        '    objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = ""
        '    objInputXml.DocumentElement.SelectSingleNode("GROUPNAME").InnerText = ""
        '    objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = ""
        '    objInputXml.DocumentElement.SelectSingleNode("STATUS").InnerText = DlstStatus.SelectedValue

        '    objInputXml.DocumentElement.SelectSingleNode("REC_DATE").InnerText = ""
        '    objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = ""
        '    objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ""
        '    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ""
        '    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ""





        '    'Here Back end Method Call
        '    ' hdChechedItem.Value = "0"

        '    ' Session("PaymentRecDataSource") = Nothing
        '    '  ddlPageNumber.SelectedValue = 1

        '    '  objOutputXml = objbzISPOrder.DETAILSReport(objInputXml)

        '    '<DETAILS 
        '    '            ROWNO = "1"
        '    '            PAYMENT_ID = "28"
        '    '            CheckUncheckStatus = ""
        '    '            BC_ID = "127"
        '    '            CHAIN_CODE = "19308"
        '    '            GROUPNAME = "T B N TRAVEL BUSINESS"
        '    '            CITY = "New Delhi"
        '    '            PERIOD = "Jul-09 - Aug-09"
        '    '            NOOFMONTHS = "1"
        '    '            AMOUNT = "150000"
        '    '            ANYADJ = ""
        '    '            THISQUARTERPAYMENT = "150000"
        '    '            SEGMENTS_PAID_FOR = "0"
        '    '            GROSSSEGMENTS = "0"
        '    '            SEGMENT = ""
        '    '            GROSS_WOIC = "0"
        '    '            SOLE = "False"
        '    '            REMARKS = ""
        '    '            TOTALCOST = "0"
        '    '            CPS_GROSS = "0"
        '    '            CPS_WOIC = "0"
        '    '            CPS_WOHW = "0"
        '    '            UPFRONTGUARANTEEBY = "0"
        '    '            STATUS = "Final Approved"
        '    '            CURRENTSTATUS = "Final Approved"
        '    'PA_FINAL_APPROVED="True" /> 

        '    '<DETAILS    ROWNO = "1"   PAYMENT_ID = "28"  CheckUncheckStatus = ""    BC_ID = "127"  CHAIN_CODE = "19308"   GROUPNAME = "T B N TRAVEL BUSINESS"    CITY = "New Delhi"   PERIOD = "Jul-09 - Aug-09"   NOOFMONTHS = "1"   AMOUNT = "150000"     ANYADJ = ""         THISQUARTERPAYMENT = "150000"            SEGMENTS_PAID_FOR = "0"     GROSSSEGMENTS = "0"    SEGMENT = ""   GROSS_WOIC = "0"  SOLE = "False"     REMARKS = ""     TOTALCOST = "0"    CPS_GROSS = "0"   CPS_WOIC = "0"    CPS_WOHW = "0"  UPFRONTGUARANTEEBY = "0"  STATUS = "Final Approved"   CURRENTSTATUS = "" PA_FINAL_APPROVED="" />  '<DETAILS    ROWNO = "1"   PAYMENT_ID = "28"  CheckUncheckStatus = ""    BC_ID = "127"  CHAIN_CODE = "19308"   GROUPNAME = "T B N TRAVEL BUSINESS"    CITY = "New Delhi"   PERIOD = "Jul-09 - Aug-09"   NOOFMONTHS = "1"   AMOUNT = "150000"     ANYADJ = ""         THISQUARTERPAYMENT = "150000"            SEGMENTS_PAID_FOR = "0"     GROSSSEGMENTS = "0"    SEGMENT = ""   GROSS_WOIC = "0"  SOLE = "False"     REMARKS = ""     TOTALCOST = "0"    CPS_GROSS = "0"   CPS_WOIC = "0"    CPS_WOHW = "0"  UPFRONTGUARANTEEBY = "0"  STATUS = "Final Approved"   CURRENTSTATUS = "Final Approved" PA_FINAL_APPROVED="True" />   '<DETAILS    ROWNO = "1"   PAYMENT_ID = "28"  CheckUncheckStatus = ""    BC_ID = "127"  CHAIN_CODE = "19308"   GROUPNAME = "T B N TRAVEL BUSINESS"    CITY = "New Delhi"   PERIOD = "Jul-09 - Aug-09"   NOOFMONTHS = "1"   AMOUNT = "150000"     ANYADJ = ""         THISQUARTERPAYMENT = "150000"            SEGMENTS_PAID_FOR = "0"     GROSSSEGMENTS = "0"    SEGMENT = ""   GROSS_WOIC = "0"  SOLE = "False"     REMARKS = ""     TOTALCOST = "0"    CPS_GROSS = "0"   CPS_WOIC = "0"    CPS_WOHW = "0"  UPFRONTGUARANTEEBY = "0"  STATUS = "Final Approved"   CURRENTSTATUS = "Final Approved" PA_FINAL_APPROVED="True" /> 

        '    '<DETAILS    ROWNO = '1'   PAYMENT_ID = '28'  CheckUncheckStatus = ''    BC_ID = '127'  CHAIN_CODE = '19308'   GROUPNAME = 'T B N TRAVEL BUSINESS'    CITY = 'New Delhi'   PERIOD = 'Jul-09 - Aug-09'   NOOFMONTHS = '1'   AMOUNT = '150000'     ANYADJ = ''         THISQUARTERPAYMENT = '150000'            SEGMENTS_PAID_FOR = '0'     GROSSSEGMENTS = '0'    SEGMENT = ''   GROSS_WOIC = '0'  SOLE = 'False'     REMARKS = ''     TOTALCOST = '0'    CPS_GROSS = '0'   CPS_WOIC = '0'    CPS_WOHW = '0'  UPFRONTGUARANTEEBY = '0'  STATUS = 'Final Approved'   CURRENTSTATUS = '' PA_FINAL_APPROVED='' />  '<DETAILS    ROWNO = '1'   PAYMENT_ID = '28'  CheckUncheckStatus = ''    BC_ID = '127'  CHAIN_CODE = '19308'   GROUPNAME = 'T B N TRAVEL BUSINESS'    CITY = 'New Delhi'   PERIOD = 'Jul-09 - Aug-09'   NOOFMONTHS = '1'   AMOUNT = '150000'     ANYADJ = ''         THISQUARTERPAYMENT = '150000'            SEGMENTS_PAID_FOR = '0'     GROSSSEGMENTS = '0'    SEGMENT = ''   GROSS_WOIC = '0'  SOLE = 'False'     REMARKS = ''     TOTALCOST = '0'    CPS_GROSS = '0'   CPS_WOIC = '0'    CPS_WOHW = '0'  UPFRONTGUARANTEEBY = '0'  STATUS = 'Final Approved'   CURRENTSTATUS = 'Final Approved' PA_FINAL_APPROVED='True' />   '<DETAILS    ROWNO = '1'   PAYMENT_ID = '28'  CheckUncheckStatus = ''    BC_ID = '127'  CHAIN_CODE = '19308'   GROUPNAME = 'T B N TRAVEL BUSINESS'    CITY = 'New Delhi'   PERIOD = 'Jul-09 - Aug-09'   NOOFMONTHS = '1'   AMOUNT = '150000'     ANYADJ = ''         THISQUARTERPAYMENT = '150000'            SEGMENTS_PAID_FOR = '0'     GROSSSEGMENTS = '0'    SEGMENT = ''   GROSS_WOIC = '0'  SOLE = 'False'     REMARKS = ''     TOTALCOST = '0'    CPS_GROSS = '0'   CPS_WOIC = '0'    CPS_WOHW = '0'  UPFRONTGUARANTEEBY = '0'  STATUS = 'Final Approved'   CURRENTSTATUS = 'Final Approved' PA_FINAL_APPROVED='True' /> 
        '    '  objOutputXml.LoadXml("<UP_SER_INC_PAYMENT_APPROVAL_QUE_OUTPUT><DETAILS ROWNO='1' CheckUncheckStatus ='' GROUPNAME='' PAYMENT_ID='1' STATUS ='' CURRENTSTATUS='' PA_FINAL_APPROVED='False' BC_ID ='2' CHAIN_CODE='' CITY='' PERIOD='' NOOFMONTHS='' AMOUNT='' ANYADJ='' THISQUARTERPAYMENT='' SEGMENTS_PAID_FOR='' GROSSSEGMENTS='' SEGMENT='' GROSS_WOIC='' SOLE='' REMARKS='' TOTALCOST='' CPS_GROSS='' CPS_WOIC='' CPS_WOHW='' UPFRONTGUARANTEEBY=''/>  <DETAILS ROWNO='2' PAYMENT_ID='2' STATUS ='' CURRENTSTATUS='' PA_FINAL_APPROVED='True' BC_ID ='2' CHAIN_CODE='' CITY='' PERIOD='' NOOFMONTHS='' AMOUNT='' ANYADJ='' THISQUARTERPAYMENT='' SEGMENTS_PAID_FOR='' GROSSSEGMENTS='' SEGMENT='' GROSS_WOIC='' SOLE='' REMARKS='' TOTALCOST='' CPS_GROSS='' CPS_WOIC='' CPS_WOHW='' UPFRONTGUARANTEEBY=''/> <DETAILS ROWNO='3' PAYMENT_ID='3' STATUS ='' CURRENTSTATUS='' PA_FINAL_APPROVED='False' BC_ID ='2' CHAIN_CODE='' CITY='' PERIOD='' NOOFMONTHS='' AMOUNT='' ANYADJ='' THISQUARTERPAYMENT='' SEGMENTS_PAID_FOR='' GROSSSEGMENTS='' SEGMENT='' GROSS_WOIC='' SOLE='' REMARKS='' TOTALCOST='' CPS_GROSS='' CPS_WOIC='' CPS_WOHW='' UPFRONTGUARANTEEBY=''/>   <Errors Status='False'>          <Error Code='' Description='' />          </Errors>   </UP_SER_INC_PAYMENT_APPROVAL_QUE_OUTPUT>")

        '    objOutputXml = objbzPaymentApprovalQue.SearchPaymentApprovalQue(objInputXml)

        '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

        '        ViewState("InputXml") = objInputXml.OuterXml


        '        Dim objExport As New ExportExcel
        '        Dim strArray() As String = {"Chain Code", "Agency Name", "City", "Period", "No. Of Month", "Amount", "Any ADJ", "Quarter Payment", "Segment Paid For", "Gross Segment", "Gross Segment WOIC", "Sole", "Remarks", "Total Cost", "CPS Gross", "CPS WOIC", "CPS WOHW", "Upfront Guranteed By", "Status", "Current Status"}
        '        Dim intArray() As Integer = {4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24}
        '        objExport.ExportDetails(objOutputXml, "DETAILS", intArray, strArray, ExportExcel.ExportFormat.Excel, "PayQue.xls")



        '    Else
        '        gvIspPaymentRec.DataSource = Nothing
        '        gvIspPaymentRec.DataBind()
        '        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        '        pnlPaging.Visible = False
        '        LblTotal.Text = ""
        '        LblToTalInNum.Text = ""
        '        btnSelectAll.Visible = False
        '        btnDeSelectAll.Visible = False

        '    End If
        'Catch ex As Exception
        '    lblError.Text = ex.Message

        'Finally
        '    objOutputXml = Nothing
        '    objInputXml = Nothing
        '    ' objbzISPOrder = Nothing
        'End Try
    End Sub

    Protected Sub BtnApproved_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnApproved.Click

        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            lblError.Text = "Session is expired."
            Exit Sub
        End If
        Dim objInputXml, objOutputXml As New XmlDocument

        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument

        Dim objbzPaymentApprovalQue As New AAMS.bizIncetive.bzPaymentApprovalQue

        Dim objChildNode, objChildNodeClone As XmlNode
        ' Dim chkPTID As CheckBox
        ' Dim hdPANumber As HiddenField
        ' Dim HdFinalApproved As HiddenField
        Dim i As Integer
        Try

            objInputXml.LoadXml("<UP_UPDATE_PAYMENT_INC_APPROVAL_QUE_INPUT><DETAILS EMPLOYEEID='' PAYMENT_ID='' REC_DATE='' APPROVAL_STATUS_ID='1' /></UP_UPDATE_PAYMENT_INC_APPROVAL_QUE_INPUT >")

            'Reading and Appending records into the Input XMLDocument         

            objChildNode = objInputXml.DocumentElement.SelectSingleNode("DETAILS")
            objChildNodeClone = objChildNode.CloneNode(True)

            objInputXml.DocumentElement.RemoveChild(objChildNode)
            If (gvIspPaymentRec.Rows.Count <= 0) Then
                lblError.Text = "There is no No Row For Selection."
            End If
            'For i = 0 To gvIspPaymentRec.Rows.Count - 1
            '    hdPANumber = gvIspPaymentRec.Rows(i).FindControl("hdPANumber")
            '    chkPTID = gvIspPaymentRec.Rows(i).FindControl("chkPTID")
            '    HdFinalApproved = gvIspPaymentRec.Rows(i).FindControl("HdFinalApproved")
            '    If chkPTID.Checked = True Then
            '        With objChildNodeClone

            '            If Not Session("LoginSession") Is Nothing Then
            '                .Attributes("EMPLOYEEID").Value = Session("LoginSession").ToString().Split("|")(0)
            '            End If

            '            .Attributes("PAYMENT_ID").Value = hdPANumber.Value

            '            .Attributes("REC_DATE").Value = ""

            '            .Attributes("APPROVAL_STATUS_ID").Value = "2"
            '        End With

            '        objInputXml.DocumentElement.AppendChild(objChildNodeClone)
            '        objChildNodeClone = objChildNode.CloneNode(True)
            '    End If
            'Next

            If HttpContext.Current.Session("PaymentRecDataSource") IsNot Nothing Then
                Dim dset As DataSet = CType(HttpContext.Current.Session("PaymentRecDataSource"), DataSet)

                For i = 0 To dset.Tables("DETAILS").Rows.Count - 1

                    If dset.Tables("DETAILS").Rows(i)("CheckUncheckStatus").ToString().Trim().ToUpper() = "TRUE" Then

                        With objChildNodeClone
                            If Not Session("LoginSession") Is Nothing Then
                                .Attributes("EMPLOYEEID").Value = Session("LoginSession").ToString().Split("|")(0)
                            End If

                            .Attributes("PAYMENT_ID").Value = dset.Tables("DETAILS").Rows(i)("PAYMENT_ID").ToString()

                            .Attributes("REC_DATE").Value = ""

                            .Attributes("APPROVAL_STATUS_ID").Value = "2"
                        End With
                        objInputXml.DocumentElement.AppendChild(objChildNodeClone)
                        objChildNodeClone = objChildNode.CloneNode(True)
                    End If

                Next
            End If

            If objInputXml.DocumentElement.SelectSingleNode("DETAILS") Is Nothing Then
                lblError.Text = "There is no payment is selected to send for approval for next level."
                Exit Sub
            End If

            objOutputXml = objbzPaymentApprovalQue.UpdatePaymentApproval_Que(objInputXml)

            '   objOutputXml.LoadXml("<UP_SER_INC_PAYMENT_APPROVAL_QUE_OUTPUT>  <DETAILS PAYMENT_ID='1' BC_ID ='1' CHAIN_CODE='' GROUPNAME ='' AOFFICE='' MONTH='3' YEAR='2008'  REC_DATE='' STATUS ='' CURRENT_STATUS='' PA_FINAL_APPROVED='No'/>   <DETAILS PAYMENT_ID='2' BC_ID ='1' CHAIN_CODE='' GROUPNAME ='' AOFFICE='' MONTH='3' YEAR='2008'  REC_DATE='' STATUS ='' CURRENT_STATUS='' PA_FINAL_APPROVED='No'/> <DETAILS PAYMENT_ID='3' BC_ID ='1' CHAIN_CODE='' GROUPNAME ='' AOFFICE='' MONTH='3' YEAR='2008'  REC_DATE='' STATUS ='' CURRENT_STATUS='' PA_FINAL_APPROVED='No'/>  <Errors Status=''>          <Error Code='' Description='' />            </Errors>   </UP_SER_INC_PAYMENT_APPROVAL_QUE_OUTPUT>")

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If ViewState("InputXml") IsNot Nothing Then
                    binddata(ViewState("InputXml").ToString)
                End If
                lblError.Text = "Successfully sent approval for the next level."
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            If (gvIspPaymentRec.Rows.Count > 0) Then
                total()
                CheckedItem()
            End If

        End Try
    End Sub

    Protected Sub BtnReject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnReject.Click

        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            lblError.Text = "Session is expired."
            Exit Sub
        End If
        lblReasonError.Text = String.Empty
        txtReason.Text = String.Empty
        Dim objInputXml, objOutputXml As New XmlDocument

        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument

        Dim objbzPaymentApprovalQue As New AAMS.bizIncetive.bzPaymentApprovalQue

        Dim objChildNode, objChildNodeClone As XmlNode
        ' Dim chkPTID As CheckBox
        ' Dim hdPANumber As HiddenField
        ' Dim HdFinalApproved As HiddenField
        Dim i As Integer
        Try

            objInputXml.LoadXml("<UP_UPDATE_PAYMENT_INC_APPROVAL_QUE_INPUT><REJECTED_REASON REASON='' > </REJECTED_REASON><DETAILS EMPLOYEEID='' PAYMENT_ID='' REC_DATE='' APPROVAL_STATUS_ID='' /></UP_UPDATE_PAYMENT_INC_APPROVAL_QUE_INPUT >")

            'Reading and Appending records into the Input XMLDocument         

            objChildNode = objInputXml.DocumentElement.SelectSingleNode("DETAILS")
            objChildNodeClone = objChildNode.CloneNode(True)

            objInputXml.DocumentElement.RemoveChild(objChildNode)
            If (gvIspPaymentRec.Rows.Count <= 0) Then
                lblError.Text = "There is no  Row For Selection."
            End If
            'For i = 0 To gvIspPaymentRec.Rows.Count - 1
            '    hdPANumber = gvIspPaymentRec.Rows(i).FindControl("hdPANumber")
            '    chkPTID = gvIspPaymentRec.Rows(i).FindControl("chkPTID")
            '    HdFinalApproved = gvIspPaymentRec.Rows(i).FindControl("HdFinalApproved")
            '    If chkPTID.Checked = True Then
            '        With objChildNodeClone

            '            If Not Session("LoginSession") Is Nothing Then
            '                .Attributes("EMPLOYEEID").Value = Session("LoginSession").ToString().Split("|")(0)
            '            End If

            '            .Attributes("PAYMENT_ID").Value = hdPANumber.Value

            '            .Attributes("REC_DATE").Value = ""

            '            .Attributes("APPROVAL_STATUS_ID").Value = "4"
            '        End With

            '        objInputXml.DocumentElement.AppendChild(objChildNodeClone)
            '        objChildNodeClone = objChildNode.CloneNode(True)
            '    End If
            'Next

            If HttpContext.Current.Session("PaymentRecDataSource") IsNot Nothing Then
                Dim dset As DataSet = CType(HttpContext.Current.Session("PaymentRecDataSource"), DataSet)

                For i = 0 To dset.Tables("DETAILS").Rows.Count - 1

                    If dset.Tables("DETAILS").Rows(i)("CheckUncheckStatus").ToString().Trim().ToUpper() = "TRUE" Then

                        With objChildNodeClone
                            If Not Session("LoginSession") Is Nothing Then
                                .Attributes("EMPLOYEEID").Value = Session("LoginSession").ToString().Split("|")(0)
                            End If

                            .Attributes("PAYMENT_ID").Value = dset.Tables("DETAILS").Rows(i)("PAYMENT_ID").ToString()

                            .Attributes("REC_DATE").Value = ""

                            .Attributes("APPROVAL_STATUS_ID").Value = "4"
                        End With
                        objInputXml.DocumentElement.AppendChild(objChildNodeClone)
                        objChildNodeClone = objChildNode.CloneNode(True)
                    End If

                Next
            End If


            If objInputXml.DocumentElement.SelectSingleNode("DETAILS") Is Nothing Then
                lblError.Text = "There is no payment is selected  for rejection."
                Exit Sub
            End If
            mdlPopUpExt.show()
            Exit Sub
            objOutputXml = objbzPaymentApprovalQue.UpdatePaymentApproval_Que(objInputXml)

            '   objOutputXml.LoadXml("<UP_SER_INC_PAYMENT_APPROVAL_QUE_OUTPUT>  <DETAILS PAYMENT_ID='1' BC_ID ='1' CHAIN_CODE='' GROUPNAME ='' AOFFICE='' MONTH='3' YEAR='2008'  REC_DATE='' STATUS ='' CURRENT_STATUS='' PA_FINAL_APPROVED='No'/>   <DETAILS PAYMENT_ID='2' BC_ID ='1' CHAIN_CODE='' GROUPNAME ='' AOFFICE='' MONTH='3' YEAR='2008'  REC_DATE='' STATUS ='' CURRENT_STATUS='' PA_FINAL_APPROVED='No'/> <DETAILS PAYMENT_ID='3' BC_ID ='1' CHAIN_CODE='' GROUPNAME ='' AOFFICE='' MONTH='3' YEAR='2008'  REC_DATE='' STATUS ='' CURRENT_STATUS='' PA_FINAL_APPROVED='No'/>  <Errors Status=''>          <Error Code='' Description='' />            </Errors>   </UP_SER_INC_PAYMENT_APPROVAL_QUE_OUTPUT>")

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If ViewState("InputXml") IsNot Nothing Then
                    binddata(ViewState("InputXml").ToString)
                End If
                lblError.Text = "Successfully rejected and sent to first level for further approval."
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            If (gvIspPaymentRec.Rows.Count > 0) Then
                total()
            End If

        End Try
    End Sub

    Protected Sub BtnFinnallyApproved_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFinnallyApproved.Click
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            lblError.Text = "Session is expired."
            Exit Sub
        End If
        Dim objInputXml, objOutputXml As New XmlDocument

        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument

        Dim objbzPaymentApprovalQue As New AAMS.bizIncetive.bzPaymentApprovalQue

        Dim objChildNode, objChildNodeClone As XmlNode
        'Dim chkPTID As CheckBox
        ' Dim hdPANumber As HiddenField
        '  Dim HdFinalApproved As HiddenField
        Dim objDS As New DataSet

        Dim i As Integer
        Try

            objInputXml.LoadXml("<UP_UPDATE_PAYMENT_INC_APPROVAL_QUE_INPUT><DETAILS EMPLOYEEID='' PAYMENT_ID='1' REC_DATE='' APPROVAL_STATUS_ID='1' /></UP_UPDATE_PAYMENT_INC_APPROVAL_QUE_INPUT >")

            'Reading and Appending records into the Input XMLDocument         

            objChildNode = objInputXml.DocumentElement.SelectSingleNode("DETAILS")
            objChildNodeClone = objChildNode.CloneNode(True)

            objInputXml.DocumentElement.RemoveChild(objChildNode)
            If (gvIspPaymentRec.Rows.Count <= 0) Then
                lblError.Text = "There is no  Row For Selection."
            End If


            'For i = 0 To gvIspPaymentRec.Rows.Count - 1
            '    hdPANumber = gvIspPaymentRec.Rows(i).FindControl("hdPANumber")
            '    chkPTID = gvIspPaymentRec.Rows(i).FindControl("chkPTID")
            '    HdFinalApproved = gvIspPaymentRec.Rows(i).FindControl("HdFinalApproved")
            '    If chkPTID.Checked = True Then
            '        With objChildNodeClone

            '            If Not Session("LoginSession") Is Nothing Then
            '                .Attributes("EMPLOYEEID").Value = Session("LoginSession").ToString().Split("|")(0)
            '            End If

            '            .Attributes("PAYMENT_ID").Value = hdPANumber.Value

            '            .Attributes("REC_DATE").Value = ""

            '            .Attributes("APPROVAL_STATUS_ID").Value = "3"
            '        End With

            '        objInputXml.DocumentElement.AppendChild(objChildNodeClone)
            '        objChildNodeClone = objChildNode.CloneNode(True)
            '    End If
            'Next


            If HttpContext.Current.Session("PaymentRecDataSource") IsNot Nothing Then
                Dim dset As DataSet = CType(HttpContext.Current.Session("PaymentRecDataSource"), DataSet)

                For i = 0 To dset.Tables("DETAILS").Rows.Count - 1

                    If dset.Tables("DETAILS").Rows(i)("CheckUncheckStatus").ToString().Trim().ToUpper() = "TRUE" Then

                        With objChildNodeClone
                            If Not Session("LoginSession") Is Nothing Then
                                .Attributes("EMPLOYEEID").Value = Session("LoginSession").ToString().Split("|")(0)
                            End If

                            .Attributes("PAYMENT_ID").Value = dset.Tables("DETAILS").Rows(i)("PAYMENT_ID").ToString()

                            .Attributes("REC_DATE").Value = ""

                            .Attributes("APPROVAL_STATUS_ID").Value = "3"
                        End With
                        objInputXml.DocumentElement.AppendChild(objChildNodeClone)
                        objChildNodeClone = objChildNode.CloneNode(True)
                    End If

                Next
            End If

            If objInputXml.DocumentElement.SelectSingleNode("DETAILS") Is Nothing Then
                lblError.Text = "There is no payment is selected  to send for final approval."
                Exit Sub
            End If

            objOutputXml = objbzPaymentApprovalQue.UpdatePaymentApproval_Que(objInputXml)

            ' objOutputXml.LoadXml("<UP_SER_INC_PAYMENT_APPROVAL_QUE_OUTPUT>  <DETAILS PAYMENT_ID='1' BC_ID ='1' CHAIN_CODE='' GROUPNAME ='' AOFFICE='' MONTH='3' YEAR='2008'  REC_DATE='' STATUS ='' CURRENT_STATUS='' PA_FINAL_APPROVED='No'/>   <DETAILS PAYMENT_ID='2' BC_ID ='1' CHAIN_CODE='' GROUPNAME ='' AOFFICE='' MONTH='3' YEAR='2008'  REC_DATE='' STATUS ='' CURRENT_STATUS='' PA_FINAL_APPROVED='No'/> <DETAILS PAYMENT_ID='3' BC_ID ='1' CHAIN_CODE='' GROUPNAME ='' AOFFICE='' MONTH='3' YEAR='2008'  REC_DATE='' STATUS ='' CURRENT_STATUS='' PA_FINAL_APPROVED='No'/>  <Errors Status=''>          <Error Code='' Description='' />            </Errors>   </UP_SER_INC_PAYMENT_APPROVAL_QUE_OUTPUT>")

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = "Successfully finally approved."

                ''@@@@@@@@ call Method to Mail Payment Sheet to the Configured Users.
                Session("Year") = drpYears.SelectedValue
                Session("Month") = drpMonths.SelectedValue
                Session("Status") = DlstStatus.SelectedValue
                If Session("PaymentRecDataSource") IsNot Nothing Then
                    objDS = CType(HttpContext.Current.Session("PaymentRecDataSource"), DataSet)
                End If
                Session("PaymentRecDataSourceAutoGenerateForMail") = objDS
                ''@@@@@@@@ 

                If ViewState("InputXml") IsNot Nothing Then
                    binddata(ViewState("InputXml").ToString)
                End If
                'Response.Redirect("MSSR_PaymentExportApprovalQueue.aspx?Type=GeneratePaySheet", True)
                GenerateExcelSheet()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If


        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            If (gvIspPaymentRec.Rows.Count > 0) Then
                total()
            End If
            CheckedItem()
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
            Dim objCol As DataColumn

            objCol = New DataColumn("Region", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("BCaseId", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)
            objCol = New DataColumn("Chain Code", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)
            objCol = New DataColumn("Office ID", System.Type.GetType("System.String"))
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

            objCol = New DataColumn("Final Payment", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("Segment Paid For", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("Gross Segment", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("Segment Gross WOIC", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("Remarks", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            'HW=''                  14
            'SARAL=''               15
            'ILL=''                 16
            'WLL=''                 17
            objCol = New DataColumn("HW", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("SARAL", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("ILL", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("WLL", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)
         


            objCol = New DataColumn("Total Cost", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("CPS Gross", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("CPS WOIC", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("CPS WOHW", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            'OFFICEID=''            22
            'CHAIN_CODE=''          23


         

            

            'GUARUNTEEBY=''         24
            'BIRDRESUPDATE=''       25
            objCol = New DataColumn("Guaranted By", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)

            objCol = New DataColumn("BirdresUpdate", System.Type.GetType("System.String"))
            objDT.Columns.Add(objCol)



            'objCol = New DataColumn("UpfrontGuaranted By", System.Type.GetType("System.String"))
            'objDT.Columns.Add(objCol)

            'objCol = New DataColumn("Status", System.Type.GetType("System.String"))
            'objDT.Columns.Add(objCol)

            'objCol = New DataColumn("Current Status", System.Type.GetType("System.String"))
            'objDT.Columns.Add(objCol)

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
                    drNewRow("Region") = dset.Tables("DETAILS").Rows(i)("REGION").ToString()
                    drNewRow("BCaseId") = dset.Tables("DETAILS").Rows(i)("BC_ID").ToString()
                    drNewRow("Group Name") = dset.Tables("DETAILS").Rows(i)("GROUPNAME").ToString()
                    drNewRow("City") = dset.Tables("DETAILS").Rows(i)("CITY").ToString()
                    drNewRow("Period") = dset.Tables("DETAILS").Rows(i)("PERIOD").ToString()
                    drNewRow("No Of Month") = dset.Tables("DETAILS").Rows(i)("NOOFMONTHS").ToString()
                    drNewRow("Amount") = dset.Tables("DETAILS").Rows(i)("AMOUNT").ToString()
                    drNewRow("Any ADJ") = dset.Tables("DETAILS").Rows(i)("ANYADJ").ToString()
                    drNewRow("Final Payment") = dset.Tables("DETAILS").Rows(i)("THISQUARTERPAYMENT").ToString()
                    drNewRow("Segment Paid For") = dset.Tables("DETAILS").Rows(i)("SEGMENTS_PAID_FOR").ToString()
                    drNewRow("Gross Segment") = dset.Tables("DETAILS").Rows(i)("GROSSSEGMENTS").ToString()
                    drNewRow("Segment Gross WOIC") = dset.Tables("DETAILS").Rows(i)("GROSS_WOIC").ToString()
                    drNewRow("Remarks") = dset.Tables("DETAILS").Rows(i)("REMARKS").ToString()
                    drNewRow("Total Cost") = dset.Tables("DETAILS").Rows(i)("TOTALCOST").ToString()
                    drNewRow("CPS Gross") = dset.Tables("DETAILS").Rows(i)("CPS_GROSS").ToString()
                    drNewRow("CPS WOIC") = dset.Tables("DETAILS").Rows(i)("CPS_WOIC").ToString()
                    drNewRow("CPS WOHW") = dset.Tables("DETAILS").Rows(i)("CPS_WOHW").ToString()

                    'REGION                 0
                    'HW=''                  14
                    'SARAL=''               15
                    'ILL=''                 16
                    'WLL=''                 17
                    'OFFICEID=''            22
                    'CHAIN_CODE=''          23
                    'GUARUNTEEBY=''         24
                    'BIRDRESUPDATE=''       25
                    drNewRow("HW") = dset.Tables("DETAILS").Rows(i)("HW").ToString()
                    drNewRow("SARAL") = dset.Tables("DETAILS").Rows(i)("SARAL").ToString()
                    drNewRow("ILL") = dset.Tables("DETAILS").Rows(i)("ILL").ToString()
                    drNewRow("WLL") = dset.Tables("DETAILS").Rows(i)("WLL").ToString()
                    drNewRow("Office ID") = dset.Tables("DETAILS").Rows(i)("OFFICEID").ToString()
                    drNewRow("Guaranted By") = dset.Tables("DETAILS").Rows(i)("GUARUNTEEBY").ToString()
                    drNewRow("BirdresUpdate") = dset.Tables("DETAILS").Rows(i)("BIRDRESUPDATE").ToString()

                    ' drNewRow("UpfrontGuaranted By") = dset.Tables("DETAILS").Rows(i)("UPFRONTGUARANTEEBY").ToString()
                    ' drNewRow("Status") = dset.Tables("DETAILS").Rows(i)("STATUS").ToString()
                    ' drNewRow("Current Status") = dset.Tables("DETAILS").Rows(i)("CURRENTSTATUS").ToString()

                    ' drNewRow("Status") = dset.Tables("DETAILS").Rows(i)("STATUS").ToString()
                    ' drNewRow("Current Status") = dset.Tables("DETAILS").Rows(i)("CURRENTSTATUS").ToString()
                    '  drNewRow("Status") = dset.Tables("DETAILS").Rows(i)("STATUS").ToString()
                    ' drNewRow("Current Status") = dset.Tables("DETAILS").Rows(i)("CURRENTSTATUS").ToString()
                    ' drNewRow("Current Status") = dset.Tables("DETAILS").Rows(i)("CURRENTSTATUS").ToString()
                    ' drNewRow("Current Status") = dset.Tables("DETAILS").Rows(i)("CURRENTSTATUS").ToString()

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

                objExWorkSheet.Range("A" + (1).ToString(), "Y" + (rowCount + 10).ToString()).Columns.AutoFit()
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
                .Attributes("SUBJECT").InnerText = "Payment Summery Report" & drpMonths.Text & " " & drpYears.Text
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


    '#Region " GetCallbackResult Procedure is fired internally by ICallbackEventHandler Interface  "
    '    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
    '        Return str
    '    End Function
    '#End Region
    '#Region " RaiseCallbackEvent Procedure is fired internally by ICallbackEventHandler Interface  "
    '    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent

    '        Dim objXmlReader As XmlNodeReader
    '        Dim ds2 As New DataSet
    '        Dim objOutputIspOrderSearchXml As New XmlDocument
    '        Dim objInputXml, objOutputXml As New XmlDocument
    '        Dim strArr() As String
    '        strArr = eventArgument.Split("|")

    '        If strArr(4).ToString = "Check" Then
    '            Dim Rowno As String = strArr(2)
    '            Dim CheckStatus As String = strArr(3)
    '            str = strArr(0) + "!" + "Check"
    '            If Session("PaymentRecDataSource") IsNot Nothing Then
    '                Dim dset As DataSet = CType(Session("PaymentRecDataSource"), DataSet)
    '                hdUpdateForSessionXml.Value = dset.GetXml()
    '                If hdUpdateForSessionXml.Value <> "" Then
    '                    objOutputIspOrderSearchXml.LoadXml(hdUpdateForSessionXml.Value)
    '                    If objOutputIspOrderSearchXml.DocumentElement.SelectNodes("PAYMENTPROCEED[@RowNo='" + Rowno + "']").Count > 0 Then
    '                        If CheckStatus.ToString.ToUpper = "TRUE" Then
    '                            objOutputIspOrderSearchXml.DocumentElement.SelectSingleNode("PAYMENTPROCEED[@RowNo='" + Rowno + "']").Attributes("CheckUncheckStatus").Value = "True"
    '                        Else
    '                            objOutputIspOrderSearchXml.DocumentElement.SelectSingleNode("PAYMENTPROCEED[@RowNo='" + Rowno + "']").Attributes("CheckUncheckStatus").Value = ""
    '                        End If
    '                    End If
    '                    hdUpdateForSessionXml.Value = objOutputIspOrderSearchXml.OuterXml
    '                    objOutputIspOrderSearchXml.LoadXml(hdUpdateForSessionXml.Value)
    '                    objXmlReader = New XmlNodeReader(objOutputIspOrderSearchXml)
    '                    ds2.ReadXml(objXmlReader)
    '                    Session("PaymentRecDataSource") = ds2
    '                    If objOutputIspOrderSearchXml.DocumentElement.SelectNodes("PAYMENTPROCEED[@CheckUncheckStatus ='True']").Count > 0 Then
    '                        Session("Checked") = "TRUE"
    '                        hdChechedItem.Value = "1"
    '                        str = strArr(0) + "!" + "Check" + "!" + "CheckItemExist"
    '                    Else
    '                        Session("Checked") = "FALSE"
    '                        str = strArr(0) + "!" + "Check" + "!" + "CheckItemNotExist"
    '                    End If


    '                End If
    '            End If
    '        ElseIf strArr(4).ToString = "Do" Then
    '            '  str = strArr(0) + "|" + "Do"
    '            str = strArr(0) + "!" + "Do" + "!" + +"!" + +"!" + +"!" + ""

    '        End If

    '    End Sub
    '#End Region


#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        ' Dim objXmlReader As XmlNodeReader
        Dim ds2 As New DataSet
        Dim objOutputIspOrderSearchXml As New XmlDocument
        Try
            lblError.Text = ""
            'If hdUpdateForSessionXml.Value <> "" Then
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If

          
            gvIspPaymentRec.PageIndex = CInt(ddlPageNumber.SelectedValue) - 1

            ChangeGlobalDatasetonPageNavigation()

            If Session("PaymentRecDataSource") IsNot Nothing Then

                Dim ds As DataSet = CType(Session("PaymentRecDataSource"), DataSet)

                'Dim dsets As New DataSet
                'dsets = ds
                'If ViewState("Desc") Is Nothing Then
                '    ViewState("Desc") = "FALSE"
                'End If
                'If ViewState("SortName") Is Nothing Then
                '    ViewState("SortName") = "CHAIN_CODE" '"VALUE"
                '    If ViewState("Desc") = "FALSE" Then
                '        ' bubbleSortAsc(ds, ViewState("SortName"), dsets)
                '    Else
                '        ' bubbleSortDesc(ds, ViewState("SortName"), dsets)
                '    End If
                'Else
                '    If ViewState("Desc") = "FALSE" Then
                '        'bubbleSortAsc(ds, ViewState("SortName"), dsets)
                '    Else
                '        'bubbleSortDesc(ds, ViewState("SortName"), dsets)
                '    End If
                'End If

                gvIspPaymentRec.DataSource = ds.Tables("DETAILS") 'dv
                gvIspPaymentRec.DataBind()
                ' SetImageForSorting(gvIspPaymentRec)
               
                ' @ Code For Totals in Footer
                ' Dim intRow, IntColno As Integer

                ' @ End ofCode For Totals in Footer
            End If
            BindControlsForNavigation(gvIspPaymentRec.PageCount)
            total()
            ShowHideSelectAllButton()
            'End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        '   Dim objXmlReader As XmlNodeReader
        Dim ds2 As New DataSet
        Dim objOutputIspOrderSearchXml As New XmlDocument

        Try
            lblError.Text = ""
            '  If hdUpdateForSessionXml.Value <> "" Then
            ChangeGlobalDatasetonPageNavigation()

            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If

          
            gvIspPaymentRec.PageIndex = CInt(ddlPageNumber.SelectedValue) - 1

            
            If Session("PaymentRecDataSource") IsNot Nothing Then
                '  Dim dv As New DataView
                Dim ds As DataSet = CType(Session("PaymentRecDataSource"), DataSet)

                ' Dim dv As DataView
                'Dim dsets As New DataSet
                'dsets = ds
                'If ViewState("Desc") Is Nothing Then
                '    ViewState("Desc") = "FALSE"
                'End If
                'If ViewState("SortName") Is Nothing Then
                '    ViewState("SortName") = "CHAIN_CODE" '"VALUE"
                '    If ViewState("Desc") = "FALSE" Then
                '        ' bubbleSortAsc(ds, ViewState("SortName"), dsets)
                '    Else
                '        ' bubbleSortDesc(ds, ViewState("SortName"), dsets)
                '    End If
                'Else
                '    If ViewState("Desc") = "FALSE" Then
                '        ' bubbleSortAsc(ds, ViewState("SortName"), dsets)
                '    Else
                '        ' bubbleSortDesc(ds, ViewState("SortName"), dsets)
                '    End If
                'End If

                gvIspPaymentRec.DataSource = ds.Tables("DETAILS") 'dv
                gvIspPaymentRec.DataBind()
                ' SetImageForSorting(gvIspPaymentRec)
             

            End If
            BindControlsForNavigation(gvIspPaymentRec.PageCount)
            total()

            ShowHideSelectAllButton()
            ' End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        '   Dim objXmlReader As XmlNodeReader
        Dim ds2 As New DataSet
        Dim objOutputIspOrderSearchXml As New XmlDocument
        Try

            lblError.Text = ""

            gvIspPaymentRec.PageIndex = CInt(ddlPageNumber.SelectedValue) - 1

            ChangeGlobalDatasetonPageNavigation()

            If Session("PaymentRecDataSource") IsNot Nothing Then
                '  Dim dv As New DataView
                Dim ds As DataSet = CType(Session("PaymentRecDataSource"), DataSet)


                'Dim dsets As New DataSet
                'dsets = ds
                'If ViewState("Desc") Is Nothing Then
                '    ViewState("Desc") = "FALSE"
                'End If
                'If ViewState("SortName") Is Nothing Then
                '    ViewState("SortName") = "CHAIN_CODE" '"VALUE"

                '    If ViewState("Desc") = "FALSE" Then
                '        ' bubbleSortAsc(ds, ViewState("SortName"), dsets)
                '    Else
                '        '  bubbleSortDesc(ds, ViewState("SortName"), dsets)
                '    End If
                'Else
                '    If ViewState("Desc") = "FALSE" Then
                '        '   bubbleSortAsc(ds, ViewState("SortName"), dsets)
                '    Else
                '        ' bubbleSortDesc(ds, ViewState("SortName"), dsets)
                '    End If
                'End If


                gvIspPaymentRec.DataSource = ds.Tables("DETAILS") 'dv
                gvIspPaymentRec.DataBind()

                ' SetImageForSorting(gvIspPaymentRec)
             
            End If
            BindControlsForNavigation(gvIspPaymentRec.PageCount)
            total()
            ShowHideSelectAllButton()
            'End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub



    Private Sub BindControlsForNavigation(ByVal CrrentPageNo As String)
        ' ##################################################################
        '@ Code Added For Paging And Sorting
        ' ###################################################################
        pnlPaging.Visible = True
        '  Dim count As Integer = 0
        Dim count As Integer = CInt(CrrentPageNo)
        Dim selectedValue As String = IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue)
        If count <> ddlPageNumber.Items.Count Then
            ddlPageNumber.Items.Clear()
            For i As Integer = 1 To count
                ddlPageNumber.Items.Add(i.ToString)
            Next
        End If
        ddlPageNumber.SelectedValue = selectedValue
        'Code for hiding prev and next button based on count
        If count = 1 Then
            lnkNext.Visible = False
            lnkPrev.Visible = False
        Else

            lnkPrev.Visible = True
            lnkNext.Visible = True
        End If

        'Code for hiding next button when pagenumber is equal to page count
        If ddlPageNumber.SelectedValue = count.ToString Then
            lnkNext.Visible = False
        Else
            lnkNext.Visible = True
        End If

        'Code for hiding prev button when pagenumber is 1
        If ddlPageNumber.SelectedValue = "1" Then
            lnkPrev.Visible = False
        Else
            lnkPrev.Visible = True
        End If

        ' ###################################################################
        '@ End of Code Added For Paging And Sorting 
        ' ###################################################################
    End Sub


    Private Sub bubbleSortAsc(ByRef dset As DataSet, ByVal Clmn As String, ByVal dset2 As DataSet)
        '  If Clmn = "VALUE" Then
        If Clmn = "BC_ID" Or Clmn = "CHAIN_CODE" Or Clmn = "NOOFMONTHS" Or Clmn = "AMOUNT" Or Clmn = "THISQUARTERPAYMENT" Or Clmn = "GROSSSEGMENTS" Or Clmn = "SEGMENT" Or Clmn = "GROSS_WOIC=" Or Clmn = "SOLE" Or Clmn = "REMARKS=" Or Clmn = "TOTALCOST" Or Clmn = "CPS_GROSS" Or Clmn = "CPS_WOIC" Or Clmn = "CPS_WOHW" Then
            Dim i, j As Integer
            Dim SortedDataset As New DataSet
            For i = 0 To dset.Tables("DETAILS").Rows.Count - 2
                For j = 0 To dset.Tables("DETAILS").Rows.Count - 2 - i
                    If CType(dset.Tables("DETAILS").Rows(j)(Clmn).ToString, Decimal) > CType(dset.Tables("DETAILS").Rows(j + 1)(Clmn).ToString, Decimal) Then

                        Dim objOutputXml As New XmlDocument

                        objOutputXml.LoadXml("<DETAILS ROWNO='' PAYMENT_ID='' CheckUncheckStatus='' BC_ID='' CHAIN_CODE='' GROUPNAME='' CITY='' PERIOD='' NOOFMONTHS='' AMOUNT='' ANYADJ='' THISQUARTERPAYMENT='' SEGMENTS_PAID_FOR='' GROSSSEGMENTS='' SEGMENT='' GROSS_WOIC='' SOLE='' REMARKS='' TOTALCOST='' CPS_GROSS='' CPS_WOIC='' CPS_WOHW='' UPFRONTGUARANTEEBY='' STATUS='' CURRENTSTATUS='' PA_FINAL_APPROVED='' /> ")
                        With objOutputXml.DocumentElement("DETAILS")
                            ' <DETAILS 
                            ' ROWNO=''
                            ' PAYMENT_ID=''
                            ' CheckUncheckStatus='' 
                            ' BC_ID=''
                            ' CHAIN_CODE='' 
                            ' GROUPNAME=''
                            ' CITY='' 
                            ' PERIOD=''
                            ' NOOFMONTHS=''
                            ' AMOUNT=''
                            ' ANYADJ='' 
                            ' THISQUARTERPAYMENT=''
                            ' SEGMENTS_PAID_FOR=''
                            ' GROSSSEGMENTS=''
                            ' SEGMENT='' 
                            ' GROSS_WOIC=''
                            ' SOLE=''
                            ' REMARKS=''
                            ' TOTALCOST=''
                            ' CPS_GROSS=''
                            ' CPS_WOIC=''
                            ' CPS_WOHW='' 
                            ' PFRONTGUARANTEEBY=''
                            ' STATUS=''
                            ' CURRENTSTATUS='' 
                            ' PA_FINAL_APPROVED='' /> 

                            .Attributes("ROWNO").Value = dset.Tables("DETAILS").Rows(j)(0)
                            .Attributes("PAYMENT_ID").Value = dset.Tables("DETAILS").Rows(j)(1)
                            .Attributes("CheckUncheckStatus").Value = dset.Tables("DETAILS").Rows(j)(2)
                            .Attributes("BC_ID").Value = dset.Tables("DETAILS").Rows(j)(3)
                            .Attributes("CHAIN_CODE").Value = dset.Tables("DETAILS").Rows(j)(4)
                            .Attributes("GROUPNAME").Value = dset.Tables("DETAILS").Rows(j)(5)
                            .Attributes("CITY").Value = dset.Tables("DETAILS").Rows(j)(6)
                            .Attributes("PERIOD").Value = dset.Tables("DETAILS").Rows(j)(7)
                            .Attributes("NOOFMONTHS").Value = dset.Tables("DETAILS").Rows(j)(8)
                            .Attributes("AMOUNT").Value = dset.Tables("DETAILS").Rows(j)(9)
                            .Attributes("ANYADJ").Value = dset.Tables("DETAILS").Rows(j)(10)
                            .Attributes("THISQUARTERPAYMENT").Value = dset.Tables("DETAILS").Rows(j)(11)
                            .Attributes("SEGMENTS_PAID_FOR").Value = dset.Tables("DETAILS").Rows(j)(12)
                            .Attributes("GROSSSEGMENTS").Value = dset.Tables("DETAILS").Rows(j)(13)
                            .Attributes("SEGMENT").Value = dset.Tables("DETAILS").Rows(j)(14)


                            .Attributes("GROSS_WOIC").Value = dset.Tables("DETAILS").Rows(j)(15)
                            .Attributes("SOLE").Value = dset.Tables("DETAILS").Rows(j)(16)
                            .Attributes("REMARKS").Value = dset.Tables("DETAILS").Rows(j)(17)
                            .Attributes("TOTALCOST").Value = dset.Tables("DETAILS").Rows(j)(18)
                            .Attributes("CPS_GROSS").Value = dset.Tables("DETAILS").Rows(j)(19)
                            .Attributes("CPS_WOIC").Value = dset.Tables("DETAILS").Rows(j)(20)
                            .Attributes("CPS_WOHW").Value = dset.Tables("DETAILS").Rows(j)(21)
                            .Attributes("UPFRONTGUARANTEEBY").Value = dset.Tables("DETAILS").Rows(j)(22)
                            .Attributes("STATUS").Value = dset.Tables("DETAILS").Rows(j)(23)
                            .Attributes("CURRENTSTATUS").Value = dset.Tables("DETAILS").Rows(j)(24)

                            .Attributes("PA_FINAL_APPROVED").Value = dset.Tables("DETAILS").Rows(j)(25)


                        End With

                        dset.Tables("DETAILS").Rows(j)(0) = dset2.Tables("DETAILS").Rows(j + 1)(0)
                        dset.Tables("DETAILS").Rows(j)(1) = dset2.Tables("DETAILS").Rows(j + 1)(1)
                        dset.Tables("DETAILS").Rows(j)(2) = dset2.Tables("DETAILS").Rows(j + 1)(2)
                        dset.Tables("DETAILS").Rows(j)(3) = dset2.Tables("DETAILS").Rows(j + 1)(3)
                        dset.Tables("DETAILS").Rows(j)(4) = dset2.Tables("DETAILS").Rows(j + 1)(4)
                        dset.Tables("DETAILS").Rows(j)(5) = dset2.Tables("DETAILS").Rows(j + 1)(5)
                        dset.Tables("DETAILS").Rows(j)(6) = dset2.Tables("DETAILS").Rows(j + 1)(6)
                        dset.Tables("DETAILS").Rows(j)(7) = dset2.Tables("DETAILS").Rows(j + 1)(7)
                        dset.Tables("DETAILS").Rows(j)(8) = dset2.Tables("DETAILS").Rows(j + 1)(8)
                        dset.Tables("DETAILS").Rows(j)(9) = dset2.Tables("DETAILS").Rows(j + 1)(9)
                        dset.Tables("DETAILS").Rows(j)(10) = dset2.Tables("DETAILS").Rows(j + 1)(10)
                        dset.Tables("DETAILS").Rows(j)(11) = dset2.Tables("DETAILS").Rows(j + 1)(11)
                        dset.Tables("DETAILS").Rows(j)(12) = dset2.Tables("DETAILS").Rows(j + 1)(12)
                        dset.Tables("DETAILS").Rows(j)(13) = dset2.Tables("DETAILS").Rows(j + 1)(13)
                        dset.Tables("DETAILS").Rows(j)(14) = dset2.Tables("DETAILS").Rows(j + 1)(14)
                        dset.Tables("DETAILS").Rows(j)(15) = dset2.Tables("DETAILS").Rows(j + 1)(15)
                        dset.Tables("DETAILS").Rows(j)(16) = dset2.Tables("DETAILS").Rows(j + 1)(16)
                        dset.Tables("DETAILS").Rows(j)(17) = dset2.Tables("DETAILS").Rows(j + 1)(17)
                        dset.Tables("DETAILS").Rows(j)(18) = dset2.Tables("DETAILS").Rows(j + 1)(18)
                        dset.Tables("DETAILS").Rows(j)(19) = dset2.Tables("DETAILS").Rows(j + 1)(19)
                        dset.Tables("DETAILS").Rows(j)(20) = dset2.Tables("DETAILS").Rows(j + 1)(20)
                        dset.Tables("DETAILS").Rows(j)(21) = dset2.Tables("DETAILS").Rows(j + 1)(21)
                        dset.Tables("DETAILS").Rows(j)(22) = dset2.Tables("DETAILS").Rows(j + 1)(22)
                        dset.Tables("DETAILS").Rows(j)(23) = dset2.Tables("DETAILS").Rows(j + 1)(23)
                        dset.Tables("DETAILS").Rows(j)(24) = dset2.Tables("DETAILS").Rows(j + 1)(24)
                        dset.Tables("DETAILS").Rows(j)(25) = dset2.Tables("DETAILS").Rows(j + 1)(25)

                        ' <DETAILS 
                        ' ROWNO=''
                        ' PAYMENT_ID=''
                        ' CheckUncheckStatus='' 
                        ' BC_ID=''
                        ' CHAIN_CODE='' 
                        ' GROUPNAME=''
                        ' CITY='' 
                        ' PERIOD=''
                        ' NOOFMONTHS=''
                        ' AMOUNT=''
                        ' ANYADJ='' 
                        ' THISQUARTERPAYMENT=''
                        ' SEGMENTS_PAID_FOR=''
                        ' GROSSSEGMENTS=''
                        ' SEGMENT='' 
                        ' GROSS_WOIC=''
                        ' SOLE=''
                        ' REMARKS=''
                        ' TOTALCOST=''
                        ' CPS_GROSS=''
                        ' CPS_WOIC=''
                        ' CPS_WOHW='' 
                        ' PFRONTGUARANTEEBY=''
                        ' STATUS=''
                        ' CURRENTSTATUS='' 
                        ' PA_FINAL_APPROVED='' /> 

                        dset.Tables("DETAILS").Rows(j + 1)(0) = objOutputXml.DocumentElement("DETAILS").Attributes("ROWNO").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(1) = objOutputXml.DocumentElement("DETAILS").Attributes("PAYMENT_ID").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(2) = objOutputXml.DocumentElement("DETAILS").Attributes("CheckUncheckStatus").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(3) = objOutputXml.DocumentElement("DETAILS").Attributes("BC_ID").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(4) = objOutputXml.DocumentElement("DETAILS").Attributes("CHAIN_CODE").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(5) = objOutputXml.DocumentElement("DETAILS").Attributes("GROUPNAME").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(6) = objOutputXml.DocumentElement("DETAILS").Attributes("CITY").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(7) = objOutputXml.DocumentElement("DETAILS").Attributes("PERIOD").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(8) = objOutputXml.DocumentElement("DETAILS").Attributes("NOOFMONTHS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(9) = objOutputXml.DocumentElement("DETAILS").Attributes("AMOUNT").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(10) = objOutputXml.DocumentElement("DETAILS").Attributes("ANYADJ").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(11) = objOutputXml.DocumentElement("DETAILS").Attributes("THISQUARTERPAYMENT").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(12) = objOutputXml.DocumentElement("DETAILS").Attributes("SEGMENTS_PAID_FOR").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(13) = objOutputXml.DocumentElement("DETAILS").Attributes("GROSSSEGMENTS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(14) = objOutputXml.DocumentElement("DETAILS").Attributes("SEGMENT").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(15) = objOutputXml.DocumentElement("DETAILS").Attributes("GROSS_WOIC").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(16) = objOutputXml.DocumentElement("DETAILS").Attributes("SOLE").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(17) = objOutputXml.DocumentElement("DETAILS").Attributes("REMARKS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(18) = objOutputXml.DocumentElement("DETAILS").Attributes("TOTALCOST").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(19) = objOutputXml.DocumentElement("DETAILS").Attributes("CPS_GROSS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(20) = objOutputXml.DocumentElement("DETAILS").Attributes("CPS_WOIC").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(21) = objOutputXml.DocumentElement("DETAILS").Attributes("CPS_WOHW").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(22) = objOutputXml.DocumentElement("DETAILS").Attributes("PFRONTGUARANTEEBY").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(23) = objOutputXml.DocumentElement("DETAILS").Attributes("STATUS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(24) = objOutputXml.DocumentElement("DETAILS").Attributes("CURRENTSTATUS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(25) = objOutputXml.DocumentElement("DETAILS").Attributes("PA_FINAL_APPROVED").Value()


                        dset.AcceptChanges()
                    End If
                Next j
            Next i
        Else
            Dim i, j As Integer
            Dim SortedDataset As New DataSet
            For i = 0 To dset.Tables("DETAILS").Rows.Count - 2
                For j = 0 To dset.Tables("DETAILS").Rows.Count - 2 - i
                    If String.Compare(dset.Tables("DETAILS").Rows(j)(Clmn).ToString.Trim, dset.Tables("DETAILS").Rows(j + 1)(Clmn).ToString.Trim) > 0 Then
                        Dim objOutputXml As New XmlDocument
                        objOutputXml.LoadXml("<DETAILS ROWNO='' PAYMENT_ID='' CheckUncheckStatus='' BC_ID='' CHAIN_CODE='' GROUPNAME='' CITY='' PERIOD='' NOOFMONTHS='' AMOUNT='' ANYADJ='' THISQUARTERPAYMENT='' SEGMENTS_PAID_FOR='' GROSSSEGMENTS='' SEGMENT='' GROSS_WOIC='' SOLE='' REMARKS='' TOTALCOST='' CPS_GROSS='' CPS_WOIC='' CPS_WOHW='' UPFRONTGUARANTEEBY='' STATUS='' CURRENTSTATUS='' PA_FINAL_APPROVED='' /> ")
                        With objOutputXml.DocumentElement("DETAILS")

                            ' <DETAILS 
                            ' ROWNO=''
                            ' PAYMENT_ID=''
                            ' CheckUncheckStatus='' 
                            ' BC_ID=''
                            ' CHAIN_CODE='' 
                            ' GROUPNAME=''
                            ' CITY='' 
                            ' PERIOD=''
                            ' NOOFMONTHS=''
                            ' AMOUNT=''
                            ' ANYADJ='' 
                            ' THISQUARTERPAYMENT=''
                            ' SEGMENTS_PAID_FOR=''
                            ' GROSSSEGMENTS=''
                            ' SEGMENT='' 
                            ' GROSS_WOIC=''
                            ' SOLE=''
                            ' REMARKS=''
                            ' TOTALCOST=''
                            ' CPS_GROSS=''
                            ' CPS_WOIC=''
                            ' CPS_WOHW='' 
                            ' PFRONTGUARANTEEBY=''
                            ' STATUS=''
                            ' CURRENTSTATUS='' 
                            ' PA_FINAL_APPROVED='' /> 

                            .Attributes("ROWNO").Value = dset.Tables("DETAILS").Rows(j)(0)
                            .Attributes("PAYMENT_ID").Value = dset.Tables("DETAILS").Rows(j)(1)
                            .Attributes("CheckUncheckStatus").Value = dset.Tables("DETAILS").Rows(j)(2)
                            .Attributes("BC_ID").Value = dset.Tables("DETAILS").Rows(j)(3)
                            .Attributes("CHAIN_CODE").Value = dset.Tables("DETAILS").Rows(j)(4)
                            .Attributes("GROUPNAME").Value = dset.Tables("DETAILS").Rows(j)(5)
                            .Attributes("CITY").Value = dset.Tables("DETAILS").Rows(j)(6)
                            .Attributes("PERIOD").Value = dset.Tables("DETAILS").Rows(j)(7)
                            .Attributes("NOOFMONTHS").Value = dset.Tables("DETAILS").Rows(j)(8)
                            .Attributes("AMOUNT").Value = dset.Tables("DETAILS").Rows(j)(9)
                            .Attributes("ANYADJ").Value = dset.Tables("DETAILS").Rows(j)(10)
                            .Attributes("THISQUARTERPAYMENT").Value = dset.Tables("DETAILS").Rows(j)(11)
                            .Attributes("SEGMENTS_PAID_FOR").Value = dset.Tables("DETAILS").Rows(j)(12)
                            .Attributes("GROSSSEGMENTS").Value = dset.Tables("DETAILS").Rows(j)(13)
                            .Attributes("SEGMENT").Value = dset.Tables("DETAILS").Rows(j)(14)


                            .Attributes("GROSS_WOIC").Value = dset.Tables("DETAILS").Rows(j)(15)
                            .Attributes("SOLE").Value = dset.Tables("DETAILS").Rows(j)(16)
                            .Attributes("REMARKS").Value = dset.Tables("DETAILS").Rows(j)(17)
                            .Attributes("TOTALCOST").Value = dset.Tables("DETAILS").Rows(j)(18)
                            .Attributes("CPS_GROSS").Value = dset.Tables("DETAILS").Rows(j)(19)
                            .Attributes("CPS_WOIC").Value = dset.Tables("DETAILS").Rows(j)(20)
                            .Attributes("CPS_WOHW").Value = dset.Tables("DETAILS").Rows(j)(21)
                            .Attributes("PFRONTGUARANTEEBY").Value = dset.Tables("DETAILS").Rows(j)(22)
                            .Attributes("STATUS").Value = dset.Tables("DETAILS").Rows(j)(23)
                            .Attributes("CURRENTSTATUS").Value = dset.Tables("DETAILS").Rows(j)(24)
                            .Attributes("PA_FINAL_APPROVED").Value = dset.Tables("DETAILS").Rows(j)(25)


                        End With

                        dset.Tables("DETAILS").Rows(j)(0) = dset2.Tables("DETAILS").Rows(j + 1)(0)
                        dset.Tables("DETAILS").Rows(j)(1) = dset2.Tables("DETAILS").Rows(j + 1)(1)
                        dset.Tables("DETAILS").Rows(j)(2) = dset2.Tables("DETAILS").Rows(j + 1)(2)
                        dset.Tables("DETAILS").Rows(j)(3) = dset2.Tables("DETAILS").Rows(j + 1)(3)
                        dset.Tables("DETAILS").Rows(j)(4) = dset2.Tables("DETAILS").Rows(j + 1)(4)
                        dset.Tables("DETAILS").Rows(j)(5) = dset2.Tables("DETAILS").Rows(j + 1)(5)
                        dset.Tables("DETAILS").Rows(j)(6) = dset2.Tables("DETAILS").Rows(j + 1)(6)
                        dset.Tables("DETAILS").Rows(j)(7) = dset2.Tables("DETAILS").Rows(j + 1)(7)
                        dset.Tables("DETAILS").Rows(j)(8) = dset2.Tables("DETAILS").Rows(j + 1)(8)
                        dset.Tables("DETAILS").Rows(j)(9) = dset2.Tables("DETAILS").Rows(j + 1)(9)
                        dset.Tables("DETAILS").Rows(j)(10) = dset2.Tables("DETAILS").Rows(j + 1)(10)
                        dset.Tables("DETAILS").Rows(j)(11) = dset2.Tables("DETAILS").Rows(j + 1)(11)
                        dset.Tables("DETAILS").Rows(j)(12) = dset2.Tables("DETAILS").Rows(j + 1)(12)
                        dset.Tables("DETAILS").Rows(j)(13) = dset2.Tables("DETAILS").Rows(j + 1)(13)
                        dset.Tables("DETAILS").Rows(j)(14) = dset2.Tables("DETAILS").Rows(j + 1)(14)
                        dset.Tables("DETAILS").Rows(j)(15) = dset2.Tables("DETAILS").Rows(j + 1)(15)
                        dset.Tables("DETAILS").Rows(j)(16) = dset2.Tables("DETAILS").Rows(j + 1)(16)
                        dset.Tables("DETAILS").Rows(j)(17) = dset2.Tables("DETAILS").Rows(j + 1)(17)
                        dset.Tables("DETAILS").Rows(j)(18) = dset2.Tables("DETAILS").Rows(j + 1)(18)
                        dset.Tables("DETAILS").Rows(j)(19) = dset2.Tables("DETAILS").Rows(j + 1)(19)
                        dset.Tables("DETAILS").Rows(j)(20) = dset2.Tables("DETAILS").Rows(j + 1)(20)
                        dset.Tables("DETAILS").Rows(j)(21) = dset2.Tables("DETAILS").Rows(j + 1)(21)
                        dset.Tables("DETAILS").Rows(j)(22) = dset2.Tables("DETAILS").Rows(j + 1)(22)
                        dset.Tables("DETAILS").Rows(j)(23) = dset2.Tables("DETAILS").Rows(j + 1)(23)
                        dset.Tables("DETAILS").Rows(j)(24) = dset2.Tables("DETAILS").Rows(j + 1)(24)
                        dset.Tables("DETAILS").Rows(j)(25) = dset2.Tables("DETAILS").Rows(j + 1)(25)


                        ' <DETAILS 
                        ' ROWNO=''
                        ' PAYMENT_ID=''
                        ' CheckUncheckStatus='' 
                        ' BC_ID=''
                        ' CHAIN_CODE='' 
                        ' GROUPNAME=''
                        ' CITY='' 
                        ' PERIOD=''
                        ' NOOFMONTHS=''
                        ' AMOUNT=''
                        ' ANYADJ='' 
                        ' THISQUARTERPAYMENT=''
                        ' SEGMENTS_PAID_FOR=''
                        ' GROSSSEGMENTS=''
                        ' SEGMENT='' 
                        ' GROSS_WOIC=''
                        ' SOLE=''
                        ' REMARKS=''
                        ' TOTALCOST=''
                        ' CPS_GROSS=''
                        ' CPS_WOIC=''
                        ' CPS_WOHW='' 
                        ' PFRONTGUARANTEEBY=''
                        ' STATUS=''
                        ' CURRENTSTATUS='' 
                        ' PA_FINAL_APPROVED='' /> 

                        dset.Tables("DETAILS").Rows(j + 1)(0) = objOutputXml.DocumentElement("DETAILS").Attributes("ROWNO").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(1) = objOutputXml.DocumentElement("DETAILS").Attributes("PAYMENT_ID").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(2) = objOutputXml.DocumentElement("DETAILS").Attributes("CheckUncheckStatus").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(3) = objOutputXml.DocumentElement("DETAILS").Attributes("BC_ID").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(4) = objOutputXml.DocumentElement("DETAILS").Attributes("CHAIN_CODE").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(5) = objOutputXml.DocumentElement("DETAILS").Attributes("GROUPNAME").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(6) = objOutputXml.DocumentElement("DETAILS").Attributes("CITY").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(7) = objOutputXml.DocumentElement("DETAILS").Attributes("PERIOD").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(8) = objOutputXml.DocumentElement("DETAILS").Attributes("NOOFMONTHS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(9) = objOutputXml.DocumentElement("DETAILS").Attributes("AMOUNT").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(10) = objOutputXml.DocumentElement("DETAILS").Attributes("ANYADJ").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(11) = objOutputXml.DocumentElement("DETAILS").Attributes("THISQUARTERPAYMENT").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(12) = objOutputXml.DocumentElement("DETAILS").Attributes("SEGMENTS_PAID_FOR").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(13) = objOutputXml.DocumentElement("DETAILS").Attributes("GROSSSEGMENTS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(14) = objOutputXml.DocumentElement("DETAILS").Attributes("SEGMENT").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(15) = objOutputXml.DocumentElement("DETAILS").Attributes("GROSS_WOIC").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(16) = objOutputXml.DocumentElement("DETAILS").Attributes("SOLE").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(17) = objOutputXml.DocumentElement("DETAILS").Attributes("REMARKS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(18) = objOutputXml.DocumentElement("DETAILS").Attributes("TOTALCOST").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(19) = objOutputXml.DocumentElement("DETAILS").Attributes("CPS_GROSS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(20) = objOutputXml.DocumentElement("DETAILS").Attributes("CPS_WOIC").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(21) = objOutputXml.DocumentElement("DETAILS").Attributes("CPS_WOHW").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(22) = objOutputXml.DocumentElement("DETAILS").Attributes("PFRONTGUARANTEEBY").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(23) = objOutputXml.DocumentElement("DETAILS").Attributes("STATUS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(24) = objOutputXml.DocumentElement("DETAILS").Attributes("CURRENTSTATUS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(25) = objOutputXml.DocumentElement("DETAILS").Attributes("PA_FINAL_APPROVED").Value()

                        dset.AcceptChanges()
                    End If
                Next j
            Next i

        End If
    End Sub
    Private Sub bubbleSortDesc(ByRef dset As DataSet, ByVal Clmn As String, ByVal dset2 As DataSet)

        '  If Clmn = "VALUE" Then
        If Clmn = "BC_ID" Or Clmn = "CHAIN_CODE" Or Clmn = "NOOFMONTHS" Or Clmn = "AMOUNT" Or Clmn = "THISQUARTERPAYMENT" Or Clmn = "GROSSSEGMENTS" Or Clmn = "SEGMENT" Or Clmn = "GROSS_WOIC=" Or Clmn = "SOLE" Or Clmn = "REMARKS=" Or Clmn = "TOTALCOST" Or Clmn = "CPS_GROSS" Or Clmn = "CPS_WOIC" Or Clmn = "CPS_WOHW" Then
            Dim i, j As Integer
            Dim SortedDataset As New DataSet
            For i = 0 To dset.Tables("DETAILS").Rows.Count - 2
                For j = 0 To dset.Tables("DETAILS").Rows.Count - 2 - i
                    If CType(dset.Tables("DETAILS").Rows(j)(Clmn).ToString, Decimal) < CType(dset.Tables("DETAILS").Rows(j + 1)(Clmn).ToString, Decimal) Then

                        Dim objOutputXml As New XmlDocument

                        objOutputXml.LoadXml("<DETAILS ROWNO='' PAYMENT_ID='' CheckUncheckStatus='' BC_ID='' CHAIN_CODE='' GROUPNAME='' CITY='' PERIOD='' NOOFMONTHS='' AMOUNT='' ANYADJ='' THISQUARTERPAYMENT='' SEGMENTS_PAID_FOR='' GROSSSEGMENTS='' SEGMENT='' GROSS_WOIC='' SOLE='' REMARKS='' TOTALCOST='' CPS_GROSS='' CPS_WOIC='' CPS_WOHW='' UPFRONTGUARANTEEBY='' STATUS='' CURRENTSTATUS='' PA_FINAL_APPROVED='' /> ")
                        With objOutputXml.DocumentElement("DETAILS")
                            ' <DETAILS 
                            ' ROWNO=''
                            ' PAYMENT_ID=''
                            ' CheckUncheckStatus='' 
                            ' BC_ID=''
                            ' CHAIN_CODE='' 
                            ' GROUPNAME=''
                            ' CITY='' 
                            ' PERIOD=''
                            ' NOOFMONTHS=''
                            ' AMOUNT=''
                            ' ANYADJ='' 
                            ' THISQUARTERPAYMENT=''
                            ' SEGMENTS_PAID_FOR=''
                            ' GROSSSEGMENTS=''
                            ' SEGMENT='' 
                            ' GROSS_WOIC=''
                            ' SOLE=''
                            ' REMARKS=''
                            ' TOTALCOST=''
                            ' CPS_GROSS=''
                            ' CPS_WOIC=''
                            ' CPS_WOHW='' 
                            ' PFRONTGUARANTEEBY=''
                            ' STATUS=''
                            ' CURRENTSTATUS='' 
                            ' PA_FINAL_APPROVED='' /> 

                            .Attributes("ROWNO").Value = dset.Tables("DETAILS").Rows(j)(0)
                            .Attributes("PAYMENT_ID").Value = dset.Tables("DETAILS").Rows(j)(1)
                            .Attributes("CheckUncheckStatus").Value = dset.Tables("DETAILS").Rows(j)(2)
                            .Attributes("BC_ID").Value = dset.Tables("DETAILS").Rows(j)(3)
                            .Attributes("CHAIN_CODE").Value = dset.Tables("DETAILS").Rows(j)(4)
                            .Attributes("GROUPNAME").Value = dset.Tables("DETAILS").Rows(j)(5)
                            .Attributes("CITY").Value = dset.Tables("DETAILS").Rows(j)(6)
                            .Attributes("PERIOD").Value = dset.Tables("DETAILS").Rows(j)(7)
                            .Attributes("NOOFMONTHS").Value = dset.Tables("DETAILS").Rows(j)(8)
                            .Attributes("AMOUNT").Value = dset.Tables("DETAILS").Rows(j)(9)
                            .Attributes("ANYADJ").Value = dset.Tables("DETAILS").Rows(j)(10)
                            .Attributes("THISQUARTERPAYMENT").Value = dset.Tables("DETAILS").Rows(j)(11)
                            .Attributes("SEGMENTS_PAID_FOR").Value = dset.Tables("DETAILS").Rows(j)(12)
                            .Attributes("GROSSSEGMENTS").Value = dset.Tables("DETAILS").Rows(j)(13)
                            .Attributes("SEGMENT").Value = dset.Tables("DETAILS").Rows(j)(14)


                            .Attributes("GROSS_WOIC").Value = dset.Tables("DETAILS").Rows(j)(15)
                            .Attributes("SOLE").Value = dset.Tables("DETAILS").Rows(j)(16)
                            .Attributes("REMARKS").Value = dset.Tables("DETAILS").Rows(j)(17)
                            .Attributes("TOTALCOST").Value = dset.Tables("DETAILS").Rows(j)(18)
                            .Attributes("CPS_GROSS").Value = dset.Tables("DETAILS").Rows(j)(19)
                            .Attributes("CPS_WOIC").Value = dset.Tables("DETAILS").Rows(j)(20)
                            .Attributes("CPS_WOHW").Value = dset.Tables("DETAILS").Rows(j)(21)
                            .Attributes("UPFRONTGUARANTEEBY").Value = dset.Tables("DETAILS").Rows(j)(22)
                            .Attributes("STATUS").Value = dset.Tables("DETAILS").Rows(j)(23)
                            .Attributes("CURRENTSTATUS").Value = dset.Tables("DETAILS").Rows(j)(24)

                            .Attributes("PA_FINAL_APPROVED").Value = dset.Tables("DETAILS").Rows(j)(25)


                        End With

                        dset.Tables("DETAILS").Rows(j)(0) = dset2.Tables("DETAILS").Rows(j + 1)(0)
                        dset.Tables("DETAILS").Rows(j)(1) = dset2.Tables("DETAILS").Rows(j + 1)(1)
                        dset.Tables("DETAILS").Rows(j)(2) = dset2.Tables("DETAILS").Rows(j + 1)(2)
                        dset.Tables("DETAILS").Rows(j)(3) = dset2.Tables("DETAILS").Rows(j + 1)(3)
                        dset.Tables("DETAILS").Rows(j)(4) = dset2.Tables("DETAILS").Rows(j + 1)(4)
                        dset.Tables("DETAILS").Rows(j)(5) = dset2.Tables("DETAILS").Rows(j + 1)(5)
                        dset.Tables("DETAILS").Rows(j)(6) = dset2.Tables("DETAILS").Rows(j + 1)(6)
                        dset.Tables("DETAILS").Rows(j)(7) = dset2.Tables("DETAILS").Rows(j + 1)(7)
                        dset.Tables("DETAILS").Rows(j)(8) = dset2.Tables("DETAILS").Rows(j + 1)(8)
                        dset.Tables("DETAILS").Rows(j)(9) = dset2.Tables("DETAILS").Rows(j + 1)(9)
                        dset.Tables("DETAILS").Rows(j)(10) = dset2.Tables("DETAILS").Rows(j + 1)(10)
                        dset.Tables("DETAILS").Rows(j)(11) = dset2.Tables("DETAILS").Rows(j + 1)(11)
                        dset.Tables("DETAILS").Rows(j)(12) = dset2.Tables("DETAILS").Rows(j + 1)(12)
                        dset.Tables("DETAILS").Rows(j)(13) = dset2.Tables("DETAILS").Rows(j + 1)(13)
                        dset.Tables("DETAILS").Rows(j)(14) = dset2.Tables("DETAILS").Rows(j + 1)(14)
                        dset.Tables("DETAILS").Rows(j)(15) = dset2.Tables("DETAILS").Rows(j + 1)(15)
                        dset.Tables("DETAILS").Rows(j)(16) = dset2.Tables("DETAILS").Rows(j + 1)(16)
                        dset.Tables("DETAILS").Rows(j)(17) = dset2.Tables("DETAILS").Rows(j + 1)(17)
                        dset.Tables("DETAILS").Rows(j)(18) = dset2.Tables("DETAILS").Rows(j + 1)(18)
                        dset.Tables("DETAILS").Rows(j)(19) = dset2.Tables("DETAILS").Rows(j + 1)(19)
                        dset.Tables("DETAILS").Rows(j)(20) = dset2.Tables("DETAILS").Rows(j + 1)(20)
                        dset.Tables("DETAILS").Rows(j)(21) = dset2.Tables("DETAILS").Rows(j + 1)(21)
                        dset.Tables("DETAILS").Rows(j)(22) = dset2.Tables("DETAILS").Rows(j + 1)(22)
                        dset.Tables("DETAILS").Rows(j)(23) = dset2.Tables("DETAILS").Rows(j + 1)(23)
                        dset.Tables("DETAILS").Rows(j)(24) = dset2.Tables("DETAILS").Rows(j + 1)(24)
                        dset.Tables("DETAILS").Rows(j)(25) = dset2.Tables("DETAILS").Rows(j + 1)(25)

                        ' <DETAILS 
                        ' ROWNO=''
                        ' PAYMENT_ID=''
                        ' CheckUncheckStatus='' 
                        ' BC_ID=''
                        ' CHAIN_CODE='' 
                        ' GROUPNAME=''
                        ' CITY='' 
                        ' PERIOD=''
                        ' NOOFMONTHS=''
                        ' AMOUNT=''
                        ' ANYADJ='' 
                        ' THISQUARTERPAYMENT=''
                        ' SEGMENTS_PAID_FOR=''
                        ' GROSSSEGMENTS=''
                        ' SEGMENT='' 
                        ' GROSS_WOIC=''
                        ' SOLE=''
                        ' REMARKS=''
                        ' TOTALCOST=''
                        ' CPS_GROSS=''
                        ' CPS_WOIC=''
                        ' CPS_WOHW='' 
                        ' PFRONTGUARANTEEBY=''
                        ' STATUS=''
                        ' CURRENTSTATUS='' 
                        ' PA_FINAL_APPROVED='' /> 

                        dset.Tables("DETAILS").Rows(j + 1)(0) = objOutputXml.DocumentElement("DETAILS").Attributes("ROWNO").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(1) = objOutputXml.DocumentElement("DETAILS").Attributes("PAYMENT_ID").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(2) = objOutputXml.DocumentElement("DETAILS").Attributes("CheckUncheckStatus").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(3) = objOutputXml.DocumentElement("DETAILS").Attributes("BC_ID").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(4) = objOutputXml.DocumentElement("DETAILS").Attributes("CHAIN_CODE").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(5) = objOutputXml.DocumentElement("DETAILS").Attributes("GROUPNAME").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(6) = objOutputXml.DocumentElement("DETAILS").Attributes("CITY").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(7) = objOutputXml.DocumentElement("DETAILS").Attributes("PERIOD").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(8) = objOutputXml.DocumentElement("DETAILS").Attributes("NOOFMONTHS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(9) = objOutputXml.DocumentElement("DETAILS").Attributes("AMOUNT").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(10) = objOutputXml.DocumentElement("DETAILS").Attributes("ANYADJ").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(11) = objOutputXml.DocumentElement("DETAILS").Attributes("THISQUARTERPAYMENT").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(12) = objOutputXml.DocumentElement("DETAILS").Attributes("SEGMENTS_PAID_FOR").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(13) = objOutputXml.DocumentElement("DETAILS").Attributes("GROSSSEGMENTS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(14) = objOutputXml.DocumentElement("DETAILS").Attributes("SEGMENT").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(15) = objOutputXml.DocumentElement("DETAILS").Attributes("GROSS_WOIC").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(16) = objOutputXml.DocumentElement("DETAILS").Attributes("SOLE").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(17) = objOutputXml.DocumentElement("DETAILS").Attributes("REMARKS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(18) = objOutputXml.DocumentElement("DETAILS").Attributes("TOTALCOST").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(19) = objOutputXml.DocumentElement("DETAILS").Attributes("CPS_GROSS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(20) = objOutputXml.DocumentElement("DETAILS").Attributes("CPS_WOIC").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(21) = objOutputXml.DocumentElement("DETAILS").Attributes("CPS_WOHW").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(22) = objOutputXml.DocumentElement("DETAILS").Attributes("PFRONTGUARANTEEBY").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(23) = objOutputXml.DocumentElement("DETAILS").Attributes("STATUS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(24) = objOutputXml.DocumentElement("DETAILS").Attributes("CURRENTSTATUS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(25) = objOutputXml.DocumentElement("DETAILS").Attributes("PA_FINAL_APPROVED").Value()


                        dset.AcceptChanges()
                    End If
                Next j
            Next i
        Else
            Dim i, j As Integer
            Dim SortedDataset As New DataSet
            For i = 0 To dset.Tables("DETAILS").Rows.Count - 2
                For j = 0 To dset.Tables("DETAILS").Rows.Count - 2 - i
                    If String.Compare(dset.Tables("DETAILS").Rows(j)(Clmn).ToString.Trim, dset.Tables("DETAILS").Rows(j + 1)(Clmn).ToString.Trim) < 0 Then
                        Dim objOutputXml As New XmlDocument
                        objOutputXml.LoadXml("<DETAILS ROWNO='' PAYMENT_ID='' CheckUncheckStatus='' BC_ID='' CHAIN_CODE='' GROUPNAME='' CITY='' PERIOD='' NOOFMONTHS='' AMOUNT='' ANYADJ='' THISQUARTERPAYMENT='' SEGMENTS_PAID_FOR='' GROSSSEGMENTS='' SEGMENT='' GROSS_WOIC='' SOLE='' REMARKS='' TOTALCOST='' CPS_GROSS='' CPS_WOIC='' CPS_WOHW='' UPFRONTGUARANTEEBY='' STATUS='' CURRENTSTATUS='' PA_FINAL_APPROVED='' /> ")
                        With objOutputXml.DocumentElement("DETAILS")

                            ' <DETAILS 
                            ' ROWNO=''
                            ' PAYMENT_ID=''
                            ' CheckUncheckStatus='' 
                            ' BC_ID=''
                            ' CHAIN_CODE='' 
                            ' GROUPNAME=''
                            ' CITY='' 
                            ' PERIOD=''
                            ' NOOFMONTHS=''
                            ' AMOUNT=''
                            ' ANYADJ='' 
                            ' THISQUARTERPAYMENT=''
                            ' SEGMENTS_PAID_FOR=''
                            ' GROSSSEGMENTS=''
                            ' SEGMENT='' 
                            ' GROSS_WOIC=''
                            ' SOLE=''
                            ' REMARKS=''
                            ' TOTALCOST=''
                            ' CPS_GROSS=''
                            ' CPS_WOIC=''
                            ' CPS_WOHW='' 
                            ' PFRONTGUARANTEEBY=''
                            ' STATUS=''
                            ' CURRENTSTATUS='' 
                            ' PA_FINAL_APPROVED='' /> 

                            .Attributes("ROWNO").Value = dset.Tables("DETAILS").Rows(j)(0)
                            .Attributes("PAYMENT_ID").Value = dset.Tables("DETAILS").Rows(j)(1)
                            .Attributes("CheckUncheckStatus").Value = dset.Tables("DETAILS").Rows(j)(2)
                            .Attributes("BC_ID").Value = dset.Tables("DETAILS").Rows(j)(3)
                            .Attributes("CHAIN_CODE").Value = dset.Tables("DETAILS").Rows(j)(4)
                            .Attributes("GROUPNAME").Value = dset.Tables("DETAILS").Rows(j)(5)
                            .Attributes("CITY").Value = dset.Tables("DETAILS").Rows(j)(6)
                            .Attributes("PERIOD").Value = dset.Tables("DETAILS").Rows(j)(7)
                            .Attributes("NOOFMONTHS").Value = dset.Tables("DETAILS").Rows(j)(8)
                            .Attributes("AMOUNT").Value = dset.Tables("DETAILS").Rows(j)(9)
                            .Attributes("ANYADJ").Value = dset.Tables("DETAILS").Rows(j)(10)
                            .Attributes("THISQUARTERPAYMENT").Value = dset.Tables("DETAILS").Rows(j)(11)
                            .Attributes("SEGMENTS_PAID_FOR").Value = dset.Tables("DETAILS").Rows(j)(12)
                            .Attributes("GROSSSEGMENTS").Value = dset.Tables("DETAILS").Rows(j)(13)
                            .Attributes("SEGMENT").Value = dset.Tables("DETAILS").Rows(j)(14)


                            .Attributes("GROSS_WOIC").Value = dset.Tables("DETAILS").Rows(j)(15)
                            .Attributes("SOLE").Value = dset.Tables("DETAILS").Rows(j)(16)
                            .Attributes("REMARKS").Value = dset.Tables("DETAILS").Rows(j)(17)
                            .Attributes("TOTALCOST").Value = dset.Tables("DETAILS").Rows(j)(18)
                            .Attributes("CPS_GROSS").Value = dset.Tables("DETAILS").Rows(j)(19)
                            .Attributes("CPS_WOIC").Value = dset.Tables("DETAILS").Rows(j)(20)
                            .Attributes("CPS_WOHW").Value = dset.Tables("DETAILS").Rows(j)(21)
                            .Attributes("PFRONTGUARANTEEBY").Value = dset.Tables("DETAILS").Rows(j)(22)
                            .Attributes("STATUS").Value = dset.Tables("DETAILS").Rows(j)(23)
                            .Attributes("CURRENTSTATUS").Value = dset.Tables("DETAILS").Rows(j)(24)
                            .Attributes("PA_FINAL_APPROVED").Value = dset.Tables("DETAILS").Rows(j)(25)


                        End With

                        dset.Tables("DETAILS").Rows(j)(0) = dset2.Tables("DETAILS").Rows(j + 1)(0)
                        dset.Tables("DETAILS").Rows(j)(1) = dset2.Tables("DETAILS").Rows(j + 1)(1)
                        dset.Tables("DETAILS").Rows(j)(2) = dset2.Tables("DETAILS").Rows(j + 1)(2)
                        dset.Tables("DETAILS").Rows(j)(3) = dset2.Tables("DETAILS").Rows(j + 1)(3)
                        dset.Tables("DETAILS").Rows(j)(4) = dset2.Tables("DETAILS").Rows(j + 1)(4)
                        dset.Tables("DETAILS").Rows(j)(5) = dset2.Tables("DETAILS").Rows(j + 1)(5)
                        dset.Tables("DETAILS").Rows(j)(6) = dset2.Tables("DETAILS").Rows(j + 1)(6)
                        dset.Tables("DETAILS").Rows(j)(7) = dset2.Tables("DETAILS").Rows(j + 1)(7)
                        dset.Tables("DETAILS").Rows(j)(8) = dset2.Tables("DETAILS").Rows(j + 1)(8)
                        dset.Tables("DETAILS").Rows(j)(9) = dset2.Tables("DETAILS").Rows(j + 1)(9)
                        dset.Tables("DETAILS").Rows(j)(10) = dset2.Tables("DETAILS").Rows(j + 1)(10)
                        dset.Tables("DETAILS").Rows(j)(11) = dset2.Tables("DETAILS").Rows(j + 1)(11)
                        dset.Tables("DETAILS").Rows(j)(12) = dset2.Tables("DETAILS").Rows(j + 1)(12)
                        dset.Tables("DETAILS").Rows(j)(13) = dset2.Tables("DETAILS").Rows(j + 1)(13)
                        dset.Tables("DETAILS").Rows(j)(14) = dset2.Tables("DETAILS").Rows(j + 1)(14)
                        dset.Tables("DETAILS").Rows(j)(15) = dset2.Tables("DETAILS").Rows(j + 1)(15)
                        dset.Tables("DETAILS").Rows(j)(16) = dset2.Tables("DETAILS").Rows(j + 1)(16)
                        dset.Tables("DETAILS").Rows(j)(17) = dset2.Tables("DETAILS").Rows(j + 1)(17)
                        dset.Tables("DETAILS").Rows(j)(18) = dset2.Tables("DETAILS").Rows(j + 1)(18)
                        dset.Tables("DETAILS").Rows(j)(19) = dset2.Tables("DETAILS").Rows(j + 1)(19)
                        dset.Tables("DETAILS").Rows(j)(20) = dset2.Tables("DETAILS").Rows(j + 1)(20)
                        dset.Tables("DETAILS").Rows(j)(21) = dset2.Tables("DETAILS").Rows(j + 1)(21)
                        dset.Tables("DETAILS").Rows(j)(22) = dset2.Tables("DETAILS").Rows(j + 1)(22)
                        dset.Tables("DETAILS").Rows(j)(23) = dset2.Tables("DETAILS").Rows(j + 1)(23)
                        dset.Tables("DETAILS").Rows(j)(24) = dset2.Tables("DETAILS").Rows(j + 1)(24)
                        dset.Tables("DETAILS").Rows(j)(25) = dset2.Tables("DETAILS").Rows(j + 1)(25)


                        ' <DETAILS 
                        ' ROWNO=''
                        ' PAYMENT_ID=''
                        ' CheckUncheckStatus='' 
                        ' BC_ID=''
                        ' CHAIN_CODE='' 
                        ' GROUPNAME=''
                        ' CITY='' 
                        ' PERIOD=''
                        ' NOOFMONTHS=''
                        ' AMOUNT=''
                        ' ANYADJ='' 
                        ' THISQUARTERPAYMENT=''
                        ' SEGMENTS_PAID_FOR=''
                        ' GROSSSEGMENTS=''
                        ' SEGMENT='' 
                        ' GROSS_WOIC=''
                        ' SOLE=''
                        ' REMARKS=''
                        ' TOTALCOST=''
                        ' CPS_GROSS=''
                        ' CPS_WOIC=''
                        ' CPS_WOHW='' 
                        ' PFRONTGUARANTEEBY=''
                        ' STATUS=''
                        ' CURRENTSTATUS='' 
                        ' PA_FINAL_APPROVED='' /> 

                        dset.Tables("DETAILS").Rows(j + 1)(0) = objOutputXml.DocumentElement("DETAILS").Attributes("ROWNO").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(1) = objOutputXml.DocumentElement("DETAILS").Attributes("PAYMENT_ID").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(2) = objOutputXml.DocumentElement("DETAILS").Attributes("CheckUncheckStatus").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(3) = objOutputXml.DocumentElement("DETAILS").Attributes("BC_ID").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(4) = objOutputXml.DocumentElement("DETAILS").Attributes("CHAIN_CODE").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(5) = objOutputXml.DocumentElement("DETAILS").Attributes("GROUPNAME").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(6) = objOutputXml.DocumentElement("DETAILS").Attributes("CITY").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(7) = objOutputXml.DocumentElement("DETAILS").Attributes("PERIOD").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(8) = objOutputXml.DocumentElement("DETAILS").Attributes("NOOFMONTHS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(9) = objOutputXml.DocumentElement("DETAILS").Attributes("AMOUNT").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(10) = objOutputXml.DocumentElement("DETAILS").Attributes("ANYADJ").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(11) = objOutputXml.DocumentElement("DETAILS").Attributes("THISQUARTERPAYMENT").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(12) = objOutputXml.DocumentElement("DETAILS").Attributes("SEGMENTS_PAID_FOR").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(13) = objOutputXml.DocumentElement("DETAILS").Attributes("GROSSSEGMENTS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(14) = objOutputXml.DocumentElement("DETAILS").Attributes("SEGMENT").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(15) = objOutputXml.DocumentElement("DETAILS").Attributes("GROSS_WOIC").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(16) = objOutputXml.DocumentElement("DETAILS").Attributes("SOLE").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(17) = objOutputXml.DocumentElement("DETAILS").Attributes("REMARKS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(18) = objOutputXml.DocumentElement("DETAILS").Attributes("TOTALCOST").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(19) = objOutputXml.DocumentElement("DETAILS").Attributes("CPS_GROSS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(20) = objOutputXml.DocumentElement("DETAILS").Attributes("CPS_WOIC").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(21) = objOutputXml.DocumentElement("DETAILS").Attributes("CPS_WOHW").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(22) = objOutputXml.DocumentElement("DETAILS").Attributes("PFRONTGUARANTEEBY").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(23) = objOutputXml.DocumentElement("DETAILS").Attributes("STATUS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(24) = objOutputXml.DocumentElement("DETAILS").Attributes("CURRENTSTATUS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(25) = objOutputXml.DocumentElement("DETAILS").Attributes("PA_FINAL_APPROVED").Value()

                        dset.AcceptChanges()
                    End If
                Next j
            Next i

        End If
    End Sub

    Private Sub SetImageForSorting(ByVal grd As GridView)
        Dim imgUp As New Image
        imgUp.ImageUrl = "~/Images/Sortup.gif"
        Dim imgDown As New Image
        imgDown.ImageUrl = "~/Images/Sortdown.gif"
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Dim intcol As Integer = grd.Columns.IndexOf(field)
                If ViewState("Desc") = "FALSE" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "TRUE" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If
            End If
        Next
    End Sub


#End Region


    Private Sub ChangeGlobalDatasetonPageNavigation()
        Dim objOutputIspOrderSearchXml As New XmlDocument
        Dim objXmlReader As XmlReader
        Dim ds2 As New DataSet
        If Session("PaymentRecDataSource") IsNot Nothing Then
            Dim dset As DataSet = CType(Session("PaymentRecDataSource"), DataSet)
            hdUpdateForSessionXml.Value = dset.GetXml()

            If hdUpdateForSessionXml.Value <> "" Then
                objOutputIspOrderSearchXml.LoadXml(hdUpdateForSessionXml.Value)

                For i As Integer = 0 To gvIspPaymentRec.Rows.Count - 1
                    Dim hdRowNo As HiddenField
                    hdRowNo = gvIspPaymentRec.Rows(i).FindControl("hdRowNo")
                    Dim Rowno As Integer
                    Rowno = hdRowNo.Value

                    Dim chkPTID As CheckBox = gvIspPaymentRec.Rows(i).FindControl("chkPTID")
                    If chkPTID.Visible = True Then
                        If objOutputIspOrderSearchXml.DocumentElement.SelectNodes("DETAILS[@ROWNO='" + Rowno.ToString + "']").Count > 0 Then
                            If chkPTID.Checked Then
                                objOutputIspOrderSearchXml.DocumentElement.SelectSingleNode("DETAILS[@ROWNO='" + Rowno.ToString + "']").Attributes("CheckUncheckStatus").Value = "True"
                            Else
                                objOutputIspOrderSearchXml.DocumentElement.SelectSingleNode("DETAILS[@ROWNO='" + Rowno.ToString + "']").Attributes("CheckUncheckStatus").Value = ""
                            End If
                        End If
                    End If
                    hdUpdateForSessionXml.Value = objOutputIspOrderSearchXml.OuterXml
                    objOutputIspOrderSearchXml.LoadXml(hdUpdateForSessionXml.Value)

                Next

                objXmlReader = New XmlNodeReader(objOutputIspOrderSearchXml)
                ds2.ReadXml(objXmlReader)
                Session("PaymentRecDataSource") = ds2



            End If
        End If
    End Sub




    <System.Web.Script.Services.ScriptMethod()> _
 <WebMethod(EnableSession:=True)> _
 Public Shared Function CalculateServiceForWord(ByVal contextKey As String) As String

        System.Threading.Thread.Sleep(100)
        Dim sum As Double = 0
        Dim ds2 As New DataSet
        If HttpContext.Current.Session("PaymentRecDataSource") IsNot Nothing Then
            Dim dset As DataSet = CType(HttpContext.Current.Session("PaymentRecDataSource"), DataSet)
            For i As Integer = 0 To dset.Tables("DETAILS").Rows.Count - 1
                If dset.Tables("DETAILS").Rows(i)("ROWNO") = contextKey Then
                    If dset.Tables("DETAILS").Rows(i)("CheckUncheckStatus").ToString().Trim().ToUpper() = "TRUE" Then
                        dset.Tables("DETAILS").Rows(i)("CheckUncheckStatus") = ""
                    Else
                        dset.Tables("DETAILS").Rows(i)("CheckUncheckStatus") = "True"
                    End If
                End If
                If dset.Tables("DETAILS").Rows(i)("CheckUncheckStatus").ToString().Trim().ToUpper() = "TRUE" Then
                    sum = sum + Val(dset.Tables("DETAILS").Rows(i)("THISQUARTERPAYMENT").ToString().Trim())
                End If
            Next
            dset.AcceptChanges()
            HttpContext.Current.Session("PaymentRecDataSource") = dset            '
        End If

        Dim StrTotal As String = ""
        StrTotal = CurrencyToWord(sum.ToString("f2"))
        'Dim lbl As New System.Web.UI.WebControls.Literal()

        HttpContext.Current.Session("TotalSum") = sum.ToString
        '  Return String.Format("<span style='font-family:courier new;font-weight:bold;'>{0}</span>", " Total Sum =    " + StrTotal.ToString)
        CheckedItem()
        If Val(sum) > 0 Then
            Return String.Format("<span style='font-family:courier new;font-weight:bold;'>{0}</span>", " Total Sum =    " + " (  " + sum.ToString + "  )" + StrTotal.ToString) '"Total Sum= " + sum.ToString
        Else
            Return ""
        End If


    End Function

    <System.Web.Script.Services.ScriptMethod()> _
    <WebMethod(EnableSession:=True)> _
    Public Shared Function CalculateServiceForNumeric(ByVal contextKey As String) As String

        System.Threading.Thread.Sleep(100)
        Dim sum As Double = 0
        Dim ds2 As New DataSet
        If HttpContext.Current.Session("PaymentRecDataSource") IsNot Nothing Then
            Dim dset As DataSet = CType(HttpContext.Current.Session("PaymentRecDataSource"), DataSet)
            For i As Integer = 0 To dset.Tables("DETAILS").Rows.Count - 1
                If dset.Tables("DETAILS").Rows(i)("ROWNO") = contextKey Then
                    If dset.Tables("DETAILS").Rows(i)("CheckUncheckStatus").ToString().Trim().ToUpper() = "TRUE" Then
                        dset.Tables("DETAILS").Rows(i)("CheckUncheckStatus") = ""
                    Else
                        dset.Tables("DETAILS").Rows(i)("CheckUncheckStatus") = "True"
                    End If
                End If
                If dset.Tables("DETAILS").Rows(i)("CheckUncheckStatus").ToString().Trim().ToUpper() = "TRUE" Then
                    sum = sum + Val(dset.Tables("DETAILS").Rows(i)("THISQUARTERPAYMENT").ToString().Trim())
                End If
            Next
            dset.AcceptChanges()
            HttpContext.Current.Session("PaymentRecDataSource") = dset            '
        End If


        Return String.Format("<span style='font-family:courier new;font-weight:bold;'>{0}</span>", " Total Sum =    " + sum.ToString)


    End Function



    Private Sub total()


        Dim sum As Double = 0
        If HttpContext.Current.Session("PaymentRecDataSource") IsNot Nothing Then
            Dim dset As DataSet = CType(HttpContext.Current.Session("PaymentRecDataSource"), DataSet)

            For i As Integer = 0 To dset.Tables("DETAILS").Rows.Count - 1

                If dset.Tables("DETAILS").Rows(i)("CheckUncheckStatus").ToString().Trim().ToUpper() = "TRUE" Then
                    sum = sum + Val(dset.Tables("DETAILS").Rows(i)("THISQUARTERPAYMENT").ToString().Trim())
                End If
            Next
        End If

        Dim StrTotal As String = ""
        '  sum = 0.9
        StrTotal = CurrencyToWord(sum.ToString("f2"))

        ' LblTotal.Text = String.Format("<span style='font-family:courier new;font-weight:bold;'>{0}</span>", " Total Sum =    " + sum.ToString) '"Total Sum= " + sum.ToString

        If Val(sum) > 0 Then
            LblTotal.Text = String.Format("<span style='font-family:courier new;font-weight:bold;'>{0}</span>", " Total Sum =    " + " (  " + sum.ToString + "  )" + StrTotal.ToString) '"Total Sum= " + sum.ToString
        End If


        LblToTalInNum.Text = String.Format("<span style='font-family:courier new;font-weight:bold;'>{0}</span>", " Total Sum =    " + sum.ToString) '"Total Sum= " + sum.ToString

        'Session("TotalSum") = String.Format("<span style='font-family:courier new;font-weight:bold;'>{0}</span>", " Total Sum =    " + sum.ToString) '"Total Sum= " 
    End Sub
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


            ' ModiFied By Abhishek
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

        '@ ModiFied By Abhishek
        'If Paisa = "" Then
        '    CurrencyToWord = "Rupees " & Words & Hundred & " Only."
        'Else
        '    CurrencyToWord = "Rupees " & Words & Hundred & Paisa
        'End If

        If Amt >= 1 Then
            If Paisa = "" Or Paisa Is Nothing Then
                CurrencyToWord = "Rupees " & Words & Hundred & " Only."
            Else
                CurrencyToWord = "Rupees " & Words & Hundred & Paisa
            End If
        Else
            CurrencyToWord = "" & Words & Hundred & Paisa
            CurrencyToWord = CurrencyToWord.ToString.Replace("and", "")
        End If
        '=====================================================================
    End Function

    ' Conversion for Hundred
    '*****************************************
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

    ' Conversion for tens
    '*****************************************
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

    '@ start Code If All Payyment is finally approved then selectALL button is invisibile otherwise visisble
    Private Sub ShowHideSelectAllButton()
        Try
            Dim intFinalApprovedCount As Integer = 0
            If Session("PaymentRecDataSource") IsNot Nothing Then
                Dim dset As DataSet = CType(Session("PaymentRecDataSource"), DataSet)

                For j As Integer = 0 To dset.Tables("DETAILS").Rows.Count - 1
                    If dset.Tables("DETAILS").Rows(j)("PA_FINAL_APPROVED").ToString().Trim().ToUpper() <> "TRUE" Then
                        intFinalApprovedCount = intFinalApprovedCount + 1
                    End If
                Next
                If intFinalApprovedCount = 0 Then
                    btnSelectAll.Visible = False
                    btnDeSelectAll.Visible = False
                Else
                    btnSelectAll.Visible = True
                    btnDeSelectAll.Visible = False
                End If
            Else
                btnSelectAll.Visible = False
                btnDeSelectAll.Visible = False
            End If
        Catch ex As Exception
        End Try
    End Sub
    '@ End  Code If All Payment is finally approved then selectALL button is invisibile otherwise visisble
    Protected Sub btnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYes.Click


        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            lblError.Text = "Session is expired."
            Exit Sub
        End If
        Dim objInputXml, objOutputXml As New XmlDocument

        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument

        Dim objbzPaymentApprovalQue As New AAMS.bizIncetive.bzPaymentApprovalQue

        Dim objChildNode, objChildNodeClone As XmlNode
        ' Dim chkPTID As CheckBox
        ' Dim hdPANumber As HiddenField
        ' Dim HdFinalApproved As HiddenField
        Dim i As Integer
        Try

            objInputXml.LoadXml("<UP_UPDATE_PAYMENT_INC_APPROVAL_QUE_INPUT><REJECTED_REASON REASON='' > </REJECTED_REASON><DETAILS EMPLOYEEID='' PAYMENT_ID='' REC_DATE='' APPROVAL_STATUS_ID=''  /></UP_UPDATE_PAYMENT_INC_APPROVAL_QUE_INPUT >")

            'Reading and Appending records into the Input XMLDocument         

            objChildNode = objInputXml.DocumentElement.SelectSingleNode("DETAILS")
            objChildNodeClone = objChildNode.CloneNode(True)

            objInputXml.DocumentElement.RemoveChild(objChildNode)
            If (gvIspPaymentRec.Rows.Count <= 0) Then
                lblError.Text = "There is no  Row For Selection."
            End If
            'For i = 0 To gvIspPaymentRec.Rows.Count - 1
            '    hdPANumber = gvIspPaymentRec.Rows(i).FindControl("hdPANumber")
            '    chkPTID = gvIspPaymentRec.Rows(i).FindControl("chkPTID")
            '    HdFinalApproved = gvIspPaymentRec.Rows(i).FindControl("HdFinalApproved")
            '    If chkPTID.Checked = True Then
            '        With objChildNodeClone

            '            If Not Session("LoginSession") Is Nothing Then
            '                .Attributes("EMPLOYEEID").Value = Session("LoginSession").ToString().Split("|")(0)
            '            End If

            '            .Attributes("PAYMENT_ID").Value = hdPANumber.Value

            '            .Attributes("REC_DATE").Value = ""

            '            .Attributes("APPROVAL_STATUS_ID").Value = "4"
            '        End With

            '        objInputXml.DocumentElement.AppendChild(objChildNodeClone)
            '        objChildNodeClone = objChildNode.CloneNode(True)
            '    End If
            'Next

            If HttpContext.Current.Session("PaymentRecDataSource") IsNot Nothing Then
                Dim dset As DataSet = CType(HttpContext.Current.Session("PaymentRecDataSource"), DataSet)

                For i = 0 To dset.Tables("DETAILS").Rows.Count - 1

                    If dset.Tables("DETAILS").Rows(i)("CheckUncheckStatus").ToString().Trim().ToUpper() = "TRUE" Then

                        With objChildNodeClone
                            If Not Session("LoginSession") Is Nothing Then
                                .Attributes("EMPLOYEEID").Value = Session("LoginSession").ToString().Split("|")(0)
                            End If

                            .Attributes("PAYMENT_ID").Value = dset.Tables("DETAILS").Rows(i)("PAYMENT_ID").ToString()

                            .Attributes("REC_DATE").Value = ""

                            .Attributes("APPROVAL_STATUS_ID").Value = "4"
                        End With
                        objInputXml.DocumentElement.AppendChild(objChildNodeClone)
                        objChildNodeClone = objChildNode.CloneNode(True)
                    End If

                Next
                objInputXml.DocumentElement.SelectSingleNode("REJECTED_REASON").Attributes("REASON").Value = txtReason.Text
            End If


            If objInputXml.DocumentElement.SelectSingleNode("DETAILS") Is Nothing Then
                lblError.Text = "There is no payment is selected  for rejection."
                Exit Sub
            End If

            Try
                objInputXml.Save("C:\admin\PaymentApprovalQueRejected.xml")
            Catch ex As Exception
            End Try

            objOutputXml = objbzPaymentApprovalQue.UpdatePaymentApproval_Que(objInputXml)


            '   objOutputXml.LoadXml("<UP_SER_INC_PAYMENT_APPROVAL_QUE_OUTPUT>  <DETAILS PAYMENT_ID='1' BC_ID ='1' CHAIN_CODE='' GROUPNAME ='' AOFFICE='' MONTH='3' YEAR='2008'  REC_DATE='' STATUS ='' CURRENT_STATUS='' PA_FINAL_APPROVED='No'/>   <DETAILS PAYMENT_ID='2' BC_ID ='1' CHAIN_CODE='' GROUPNAME ='' AOFFICE='' MONTH='3' YEAR='2008'  REC_DATE='' STATUS ='' CURRENT_STATUS='' PA_FINAL_APPROVED='No'/> <DETAILS PAYMENT_ID='3' BC_ID ='1' CHAIN_CODE='' GROUPNAME ='' AOFFICE='' MONTH='3' YEAR='2008'  REC_DATE='' STATUS ='' CURRENT_STATUS='' PA_FINAL_APPROVED='No'/>  <Errors Status=''>          <Error Code='' Description='' />            </Errors>   </UP_SER_INC_PAYMENT_APPROVAL_QUE_OUTPUT>")

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If ViewState("InputXml") IsNot Nothing Then
                    binddata(ViewState("InputXml").ToString)
                End If
                lblError.Text = "Successfully rejected and also remove from queue." ' sent to first level for further approval."
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            If (gvIspPaymentRec.Rows.Count > 0) Then
                total()
            End If
            ShowHideSelectAllButton()
            CheckedItem()
        End Try

    End Sub
    Private Shared Sub CheckedItem()
        Dim counter As Integer = 0
        If HttpContext.Current.Session("PaymentRecDataSource") IsNot Nothing Then
            Dim dset As DataSet = CType(HttpContext.Current.Session("PaymentRecDataSource"), DataSet)
            For i As Integer = 0 To dset.Tables("DETAILS").Rows.Count - 1
                If dset.Tables("DETAILS").Rows(i)("CheckUncheckStatus").ToString().Trim().ToUpper() = "TRUE" Then
                    counter = counter + 1
                End If
            Next
        End If
        If counter > 0 Then
            HttpContext.Current.Session("CheckedItem") = "1"
        Else
            HttpContext.Current.Session("CheckedItem") = "0"
        End If

    End Sub

End Class

