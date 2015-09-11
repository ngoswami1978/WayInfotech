Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Web.Services.Protocols
Partial Class Sales_SASR_VisitDetails_JQ
    Inherits System.Web.UI.Page
#Region "Global Variable Declaration"
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim strurl As String = Request.Url.ToString()
                Session("PageName") = strurl
                '  btnSave.Attributes.Add("onClick", "return ValidateForm();")
                ' Checking Query String for update .
                ' If Request.QueryString("Action") IsNot Nothing And Request.QueryString("SVR_REASONID") IsNot Nothing Then
                'hdID.Value = objED.Decrypt(Request.QueryString("SVR_REASONID").ToString().Trim())
                ViewDetails()
                ' End If


            End If
            ' Checking security.
            ' CheckSecurity()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    <System.Web.Script.Services.ScriptMethod()> _
<WebMethod()> _
     Public Shared Function UpdateVisitDetails(ByVal sManagerID As String, ByVal sManagerName As String, ByVal sPersonMet As String, ByVal sInTime As String, ByVal sReportingManagerID As String, ByVal sReportingManagerName As String, ByVal sDesignation As String, ByVal sOutTime As String, ByVal sAction As String, ByVal sSEQUENCENO As String) As String
        Dim objAddXmlDoc As New XmlDataDocument
        Dim objBlnkVisitNode, objVisitNode, objVisitCloneNode As XmlNode
        Dim strResult As String = ""
        Try

            '<DETAIL SEQUENCENO='1' MANAGERID='1' MANAGER_NAME='VAISHALI' IMMEDIATE_MANAGERID='2' 
            'IMMEDIATE_MANAGERNAME='ANKUSH' CONTACT_NAME='PANKAJ' DESIGNATION='ML' INTIME='1000' 
            'OUTTIME='1200' />
            objAddXmlDoc.LoadXml(HttpContext.Current.Session("VisitDetails"))

            If sAction.Trim = "U" Then
                If Val(sSEQUENCENO) > 0 Then
                    objVisitNode = objAddXmlDoc.DocumentElement.SelectSingleNode("VISITDETAIL/DETAIL[@SEQUENCENO=" + sSEQUENCENO + "]")
                    If Not objVisitNode Is Nothing Then
                        objVisitNode.Attributes("MANAGERID").Value = sManagerID
                        objVisitNode.Attributes("MANAGER_NAME").Value = sManagerName
                        objVisitNode.Attributes("IMMEDIATE_MANAGERID").Value = sReportingManagerID
                        objVisitNode.Attributes("IMMEDIATE_MANAGERNAME").Value = sReportingManagerName
                        objVisitNode.Attributes("CONTACT_NAME").Value = sPersonMet
                        objVisitNode.Attributes("DESIGNATION").Value = sDesignation
                        objVisitNode.Attributes("INTIME").Value = sInTime
                        objVisitNode.Attributes("OUTTIME").Value = sOutTime
                    Else
                        strResult = "Invalid Sequence number"
                    End If
                Else
                    strResult = "Invalid Sequence number"
                End If
            ElseIf sAction.Trim = "I" Then
                objVisitNode = objAddXmlDoc.DocumentElement.SelectSingleNode("VISITDETAIL/DETAIL")
                objVisitCloneNode = objVisitNode.CloneNode(True)
                'Deleteing Blank Nodes
                objBlnkVisitNode = objAddXmlDoc.DocumentElement.SelectSingleNode("VISITDETAIL/DETAIL[@SEQUENCENO='']")
                If Not objBlnkVisitNode Is Nothing Then
                    objAddXmlDoc.DocumentElement.SelectSingleNode("VISITDETAIL").RemoveChild(objBlnkVisitNode)
                End If

                objVisitCloneNode.Attributes("SEQUENCENO").Value = objAddXmlDoc.DocumentElement.SelectNodes("VISITDETAIL/DETAIL").Count + 1
                objVisitCloneNode.Attributes("MANAGERID").Value = sManagerID
                objVisitCloneNode.Attributes("MANAGER_NAME").Value = sManagerName
                objVisitCloneNode.Attributes("IMMEDIATE_MANAGERID").Value = sReportingManagerID
                objVisitCloneNode.Attributes("IMMEDIATE_MANAGERNAME").Value = sReportingManagerName
                objVisitCloneNode.Attributes("CONTACT_NAME").Value = sPersonMet
                objVisitCloneNode.Attributes("DESIGNATION").Value = sDesignation
                objVisitCloneNode.Attributes("INTIME").Value = sInTime
                objVisitCloneNode.Attributes("OUTTIME").Value = sOutTime
                objAddXmlDoc.DocumentElement.SelectSingleNode("VISITDETAIL").AppendChild(objVisitCloneNode)
                objVisitCloneNode = objVisitNode.CloneNode(True)
            End If
            HttpContext.Current.Session("VisitDetails") = objAddXmlDoc.OuterXml
            strResult = objAddXmlDoc.DocumentElement.SelectSingleNode("VISITDETAIL").OuterXml
        Catch ex As Exception
            strResult = ex.Message
        End Try
        Return strResult.ToString()
    End Function

#Region "ViewDetails()"
    Private Sub ViewDetails()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objSales As New AAMS.bizSales.bzDSR
        Try

            objInputXml.LoadXml("<SL_VIEW_DSR_VISITDETAILS_INPUT><DSR_DETAIL_ID/><PREDATE/><LCODE/><LOGINID/></SL_VIEW_DSR_VISITDETAILS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("DSR_DETAIL_ID").InnerText = "10"
            objInputXml.DocumentElement.SelectSingleNode("PREDATE").InnerText = "20081001"
            objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = "43"
            objInputXml.DocumentElement.SelectSingleNode("LOGINID").InnerText = "24"

            objOutputXml = objSales.View(objInputXml)
            '  objOutputXml.Load("C:\Sales.xml")
            Session("VisitDetails") = objOutputXml.OuterXml
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                'Binding Agency Details
                With objOutputXml.DocumentElement.SelectSingleNode("AGENCY")
                    'LCODE="43" CHAIN_CODE="300" NAME="Varun Travels Pvt Ltd" ADDRESS="First Floor, Landmark Complex"
                    'CITY="Ahmedabad" COUNTRY="India" DATEFROM="" DATETO="" OFFICEID="AMDWI2105"
                    txtAgencyName.Text = .Attributes("NAME").InnerText
                    txtAddress.Text = .Attributes("ADDRESS").InnerText
                    txtCity.Text = .Attributes("CITY").InnerText
                    txtCountry.Text = .Attributes("COUNTRY").InnerText
                    txtLcode.Text = .Attributes("LCODE").InnerText
                    txtChainCode.Text = .Attributes("CHAIN_CODE").InnerText
                    txtOfficeID.Text = .Attributes("OFFICEID").InnerText
                    txtDateFrom.Text = .Attributes("DATEFROM").InnerText
                    txtDateTo.Text = .Attributes("DATETO").InnerText
                End With

                With objOutputXml.DocumentElement.SelectSingleNode("PRODUCTIVITY")
                    '<PRODUCTIVITY>
                    '		<MIDT A1="" B1="" G1="" P1="" W1="" TOTAL="" />
                    '		<BC ONCONTRACT="Y" BCOMMIT="100" MINSEGMENT="" LASTMONTHA1MIDT="" />
                    '		<BIDT MONTH1="157" MONTH2="188" MONTH3="118" />
                    '		<DAILYMOTIVE PAST_MONTH="563" CURRENT_MONTH="354" />
                    '	</PRODUCTIVITY>
                    With .SelectSingleNode("MIDT")
                        lit1A.Text = .Attributes("A1").InnerText
                        lit1B.Text = .Attributes("B1").InnerText
                        lit1G.Text = .Attributes("G1").InnerText
                        lit1P.Text = .Attributes("P1").InnerText
                        lit1W.Text = .Attributes("W1").InnerText
                        litTotal.Text = .Attributes("TOTAL").InnerText
                    End With
                    With .SelectSingleNode("BC")
                        txtOnContract.Text = .Attributes("ONCONTRACT").InnerText
                        txtBusinessCommit.Text = .Attributes("BCOMMIT").InnerText
                        'txtmin.Text = .Attributes("MINSEGMENT").InnerText
                        txtLatestMonth1A.Text = .Attributes("LASTMONTHA1MIDT").InnerText
                    End With

                    With .SelectSingleNode("BIDT")
                        litMonth1.Text = .Attributes("MONTH1").InnerText
                        litMonth2.Text = .Attributes("MONTH2").InnerText
                        litMonth3.Text = .Attributes("MONTH3").InnerText
                    End With
                    With .SelectSingleNode("DAILYMOTIVE")
                        txtPastMonthDailyMotive.Text = .Attributes("PAST_MONTH").InnerText
                        txtCurrentMonthDailyMotive.Text = .Attributes("CURRENT_MONTH").InnerText
                    End With
                End With



                'Binding Managers Drop Down

                BindManagers(objOutputXml)
                BindGrid(objOutputXml, "VISITDETAIL")
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
#End Region


    Private Sub BindManagers(ByVal objViewXml As XmlDocument)
        Dim objXmlReader As XmlNodeReader
        Dim objDS As New DataSet
        Try
            objXmlReader = New XmlNodeReader(objViewXml)
            objDS.ReadXml(objXmlReader)

            ddlManager.DataSource = objDS.Tables("EMPLOYEE")
            ddlManager.DataTextField = "MANAGER_NAME"
            ddlManager.DataValueField = "MANAGERID"
            ddlManager.DataBind()

            ddlReportingManager.DataSource = objDS.Tables("EMPLOYEE")
            ddlReportingManager.DataTextField = "IMMEDIATE_MANAGERNAME"
            ddlReportingManager.DataValueField = "IMMEDIATE_MANAGERID"
            ddlReportingManager.DataBind()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Private Sub BindGrid(ByVal objViewXml As XmlDocument, ByVal strGridType As String)
        Dim objXmlReader As XmlNodeReader
        Dim objDS As New DataSet
        Try
            objXmlReader = New XmlNodeReader(objViewXml)
            objDS.ReadXml(objXmlReader)
            Select Case strGridType
                Case "VISITDETAIL"
                    If objDS.Tables("DETAIL").Rows.Count > 0 Then
                        If objDS.Tables("DETAIL").Rows.Count = 1 And objDS.Tables("DETAIL").Rows(0)("SEQUENCENO").ToString.Trim.Length = 0 Then
                            gvVisitDetails.DataSource = Nothing
                        Else
                            gvVisitDetails.DataSource = objDS.Tables("DETAIL")
                        End If
                    Else
                        gvVisitDetails.DataSource = Nothing
                    End If
                    gvVisitDetails.DataBind()
            End Select
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Protected Sub btnAddVisitDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddVisitDetails.Click
        Dim objAddXmlDoc As New XmlDataDocument
        Dim objBlnkVisitNode, objVisitNode, objVisitCloneNode As XmlNode
        Try

            '<DETAIL SEQUENCENO='1' MANAGERID='1' MANAGER_NAME='VAISHALI' IMMEDIATE_MANAGERID='2' 
            'IMMEDIATE_MANAGERNAME='ANKUSH' CONTACT_NAME='PANKAJ' DESIGNATION='ML' INTIME='1000' 
            'OUTTIME='1200' />
            objAddXmlDoc.LoadXml(Session("VisitDetails"))

            If btnAddVisitDetails.Text.Trim = "Update" Then
                If hdVisitDetails.Value.Trim.Length > 0 Then
                    objVisitNode = objAddXmlDoc.DocumentElement.SelectSingleNode("VISITDETAIL/DETAIL[@SEQUENCENO=" + hdVisitDetails.Value.Trim + "]")
                    objVisitNode.Attributes("MANAGERID").Value = ddlManager.SelectedValue
                    objVisitNode.Attributes("MANAGER_NAME").Value = ddlManager.SelectedItem.Text
                    objVisitNode.Attributes("IMMEDIATE_MANAGERID").Value = ddlReportingManager.SelectedValue
                    objVisitNode.Attributes("IMMEDIATE_MANAGERNAME").Value = ddlReportingManager.SelectedItem.Text
                    objVisitNode.Attributes("CONTACT_NAME").Value = txtPersonMet.Text.Trim
                    objVisitNode.Attributes("DESIGNATION").Value = txtDesignation.Text.Trim
                    objVisitNode.Attributes("INTIME").Value = txtInTime.Text.Trim
                    objVisitNode.Attributes("OUTTIME").Value = txtOutTime.Text.Trim
                    hdVisitDetails.Value = ""
                End If
            Else
                objVisitNode = objAddXmlDoc.DocumentElement.SelectSingleNode("VISITDETAIL/DETAIL")
                objVisitCloneNode = objVisitNode.CloneNode(True)
                'Deleteing Blank Nodes
                objBlnkVisitNode = objAddXmlDoc.DocumentElement.SelectSingleNode("VISITDETAIL/DETAIL[@SEQUENCENO='']")
                If Not objBlnkVisitNode Is Nothing Then
                    objAddXmlDoc.DocumentElement.SelectSingleNode("VISITDETAIL").RemoveChild(objBlnkVisitNode)
                End If
                objVisitCloneNode.Attributes("SEQUENCENO").Value = gvVisitDetails.Rows.Count + 1
                objVisitCloneNode.Attributes("MANAGERID").Value = ddlManager.SelectedValue
                objVisitCloneNode.Attributes("MANAGER_NAME").Value = ddlManager.SelectedItem.Text
                objVisitCloneNode.Attributes("IMMEDIATE_MANAGERID").Value = ddlReportingManager.SelectedValue
                objVisitCloneNode.Attributes("IMMEDIATE_MANAGERNAME").Value = ddlReportingManager.SelectedItem.Text
                objVisitCloneNode.Attributes("CONTACT_NAME").Value = txtPersonMet.Text.Trim
                objVisitCloneNode.Attributes("DESIGNATION").Value = txtDesignation.Text.Trim
                objVisitCloneNode.Attributes("INTIME").Value = txtInTime.Text.Trim
                objVisitCloneNode.Attributes("OUTTIME").Value = txtOutTime.Text.Trim
                objAddXmlDoc.DocumentElement.SelectSingleNode("VISITDETAIL").AppendChild(objVisitCloneNode)
                objVisitCloneNode = objVisitNode.CloneNode(True)
            End If
            Session("VisitDetails") = objAddXmlDoc.OuterXml
            'Binding Visit Details Data Grid
            BindGrid(objAddXmlDoc, "VISITDETAIL")
            'Clear Controls 
            btnCancelVisitDetails_Click(sender, e)

        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrorVisitDetails.Text = ex.Message
        End Try
    End Sub


    Protected Sub gvVisitDetails_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvVisitDetails.RowCommand
        Dim strCommandArgs As String = ""
        Try
            If e.CommandName = "EditX" Then
                strCommandArgs = e.CommandArgument
                btnAddVisitDetails.Text = "Update"
                hdVisitDetails.Value = strCommandArgs.Split("|").GetValue(0)
                ddlManager.SelectedValue = strCommandArgs.Split("|").GetValue(1)
                ddlReportingManager.SelectedValue = strCommandArgs.Split("|").GetValue(3)
                txtPersonMet.Text = strCommandArgs.Split("|").GetValue(5)
                txtDesignation.Text = strCommandArgs.Split("|").GetValue(6)
                txtInTime.Text = strCommandArgs.Split("|").GetValue(7)
                txtOutTime.Text = strCommandArgs.Split("|").GetValue(8)
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrorVisitDetails.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnCancelVisitDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelVisitDetails.Click
        Try
            If (btnAddVisitDetails.Text.Trim = "Update") Then
                btnAddVisitDetails.Text = "Add"
            End If
            ddlManager.SelectedIndex = 0
            ddlReportingManager.SelectedIndex = 0
            txtPersonMet.Text = ""
            txtDesignation.Text = ""
            txtInTime.Text = ""
            txtOutTime.Text = ""
            hdVisitDetails.Value = ""

        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrorVisitDetails.Text = ex.Message
        End Try
    End Sub
End Class
