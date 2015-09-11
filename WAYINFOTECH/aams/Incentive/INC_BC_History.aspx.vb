Imports System.Data
Imports System.Xml
Partial Class Incentive_INC_BC_History
    Inherits System.Web.UI.Page
    '<INC_HISTORY_BUSINESSCASE_OUTPUT>
    '<BUSINESSCASE BC_ID='' CHAIN_CODE='' GROUP_NAME=''/>
    '<BC_HISTORY EMPLOYEE_NAME='' DATETIME='' >
    '<HEADER CHANGEDDATA=''/>
    '<CONNECTIVITY CHANGEDDATA=''/>
    '<HARDWARE CHANGEDDATA=''/>
    '<BREAKUP CHANGEDDATA=''/>
    '<BC_PLAN>
    '<INC_TYPE INC_TYPE_ID='' INC_TYPE_NAME='' INC_SLAB_REQUIRED=''/>
    '<CASE INC_PLAN_ID='' INC_PLAN_NAME=''>
    '<PLAN_DETAILS SLABS_START='' SLABS_END='' SLABS_RATE=''/>
    '<NIDT_FIELDS_ID ID=''/>
    '</CASE >
    '</BC_PLAN>
    '<PLB BC_ID='' PLBTYPEID='' PLBAMOUNT=''>
    '<PLB_DETAILS SLABS_START='' SLABS_END='' SLABS_RATE='' />
    '</PLB >
    '</BC_HISTORY>
    '<Errors Status=''>
    '<Error Code='' Description='' />
    '</Errors>
    '</INC_HISTORY_BUSINESSCASE_OUTPUT>

    '<INC_HISTORY_BUSINESSCASE_INPUT>
    '<BC_ID></BC_ID>
    '</INC_HISTORY_BUSINESSCASE_INPUT >

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Request.QueryString("BC_ID") & "" <> "" Then
                FillHistory()
            Else
                trGroupName.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Sub FillHistory()
        Dim objInputXml As New XmlDocument
        Dim objOutputXml As New XmlDocument
        Dim objBCHistory As New AAMS.bizIncetive.bzBusinessCase
        Dim ds As New DataSet
        Dim objReader As XmlNodeReader
        Try
            objInputXml.LoadXml("<INC_HISTORY_BUSINESSCASE_INPUT><BC_ID></BC_ID></INC_HISTORY_BUSINESSCASE_INPUT >")
            objInputXml.DocumentElement.SelectSingleNode("BC_ID").InnerText = Request.QueryString("BC_ID").ToString
            objOutputXml = objBCHistory.ViewHistory(objInputXml)
            'objOutputXml.Load("D:\XML\XML\AAMS\Incentive\INC_HISTORY_BUSINESSCASE_OUTPUT.xml")
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblGroupName.Text = objOutputXml.DocumentElement.SelectSingleNode("BUSINESSCASE").Attributes("GROUP_NAME").InnerText
                Session("BC_HISTORY") = objOutputXml.OuterXml
                objReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objReader)
                gvBCHistory.DataSource = ds.Tables("BC_HISTORY")
                gvBCHistory.DataBind()

            Else
                trGroupName.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            ds = Nothing
            objInputXml = Nothing
            objOutputXml = Nothing
            objBCHistory = Nothing
        End Try
    End Sub
  

    Protected Sub gvBCHistory_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvBCHistory.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim objXmlNode As XmlNode
            Dim objXmlDoc As New XmlDocument
            Try


                If Session("BC_HISTORY") IsNot Nothing Then
                    objXmlDoc.LoadXml(Session("BC_HISTORY"))
                    objXmlNode = objXmlDoc.DocumentElement.SelectNodes("BC_HISTORY").Item(e.Row.RowIndex)
                    ViewState("RowIndex") = e.Row.RowIndex.ToString


                    CType(e.Row.FindControl("lblEmployeeName"), Label).Text = objXmlNode.Attributes("EMPLOYEE_NAME").InnerText
                    CType(e.Row.FindControl("lblChangedDateTime"), Label).Text = objXmlNode.Attributes("DATETIME").InnerText
                    If objXmlNode.SelectSingleNode("HEADER") IsNot Nothing Then
                        If objXmlNode.SelectSingleNode("HEADER").Attributes("CHANGEDDATA") IsNot Nothing Then
                            CType(e.Row.FindControl("lblHeader"), Label).Text = objXmlNode.SelectSingleNode("HEADER").Attributes("CHANGEDDATA").InnerText
                        Else
                            CType(e.Row.FindControl("trHeader"), HtmlTableRow).Visible = False
                        End If
                    Else
                        CType(e.Row.FindControl("trHeader"), HtmlTableRow).Visible = False
                    End If

                    If objXmlNode.SelectSingleNode("CONNECTIVITY") IsNot Nothing Then
                        If objXmlNode.SelectSingleNode("CONNECTIVITY").Attributes("CHANGEDDATA") IsNot Nothing Then
                            CType(e.Row.FindControl("lblConnectivity"), Label).Text = objXmlNode.SelectSingleNode("CONNECTIVITY").Attributes("CHANGEDDATA").InnerText
                        Else
                            CType(e.Row.FindControl("trConnectivity"), HtmlTableRow).Visible = False
                        End If
                    Else
                        CType(e.Row.FindControl("trConnectivity"), HtmlTableRow).Visible = False
                    End If

                    If objXmlNode.SelectSingleNode("HARDWARE") IsNot Nothing Then
                        If objXmlNode.SelectSingleNode("HARDWARE").Attributes("CHANGEDDATA") IsNot Nothing Then
                            CType(e.Row.FindControl("lblHardware"), Label).Text = objXmlNode.SelectSingleNode("HARDWARE").Attributes("CHANGEDDATA").InnerText
                        Else
                            CType(e.Row.FindControl("trHardware"), HtmlTableRow).Visible = False
                        End If
                    Else
                        CType(e.Row.FindControl("trHardware"), HtmlTableRow).Visible = False
                    End If

                    If objXmlNode.SelectSingleNode("BREAKUP") IsNot Nothing Then
                        If objXmlNode.SelectSingleNode("BREAKUP").Attributes("CHANGEDDATA") IsNot Nothing Then
                            CType(e.Row.FindControl("lblBreakup"), Label).Text = objXmlNode.SelectSingleNode("BREAKUP").Attributes("CHANGEDDATA").InnerText
                        Else
                            CType(e.Row.FindControl("trBreakup"), HtmlTableRow).Visible = False
                        End If
                    Else
                        CType(e.Row.FindControl("trBreakup"), HtmlTableRow).Visible = False
                    End If
                    If objXmlNode.SelectSingleNode("SLAB_QUALIFICATION") IsNot Nothing Then
                        If objXmlNode.SelectSingleNode("SLAB_QUALIFICATION").Attributes("ID") IsNot Nothing Then
                            CType(e.Row.FindControl("lblSlabQualification"), Label).Text = objXmlNode.SelectSingleNode("SLAB_QUALIFICATION").Attributes("ID").InnerText
                        Else
                            CType(e.Row.FindControl("trSlabQualification"), HtmlTableRow).Visible = False
                        End If
                    Else
                        CType(e.Row.FindControl("trSlabQualification"), HtmlTableRow).Visible = False
                    End If
                    'Slab Qualification

                    '@Start of  Minimum Segment Criteria
                    If objXmlNode.SelectSingleNode("MIN_SEG_CRITERIA") IsNot Nothing Then
                        If objXmlNode.SelectSingleNode("MIN_SEG_CRITERIA").Attributes("ID") IsNot Nothing Then
                            CType(e.Row.FindControl("lblMinSegCriteria"), Label).Text = objXmlNode.SelectSingleNode("MIN_SEG_CRITERIA").Attributes("ID").InnerText
                        Else
                            CType(e.Row.FindControl("trMinSegCriteria"), HtmlTableRow).Visible = False
                        End If
                    Else
                        CType(e.Row.FindControl("trMinSegCriteria"), HtmlTableRow).Visible = False
                    End If
                    '@End of  Minimum Segment Criteria


                    If objXmlNode.SelectSingleNode("BC_PLAN") IsNot Nothing Then
                        If objXmlNode.SelectSingleNode("BC_PLAN/INC_TYPE") IsNot Nothing Then
                            If objXmlNode.SelectSingleNode("BC_PLAN/INC_TYPE").Attributes("INC_TYPE_NAME") IsNot Nothing Then
                                CType(e.Row.FindControl("lblPaymentType"), Label).Text = objXmlNode.SelectSingleNode("BC_PLAN/INC_TYPE").Attributes("INC_TYPE_NAME").InnerText
                            Else
                                CType(e.Row.FindControl("trPaymentType"), HtmlTableRow).Visible = False
                            End If
                        Else
                            CType(e.Row.FindControl("trPaymentType"), HtmlTableRow).Visible = False
                        End If

                        If objXmlNode.SelectSingleNode("BC_PLAN/CASE") IsNot Nothing Then
                            Dim gvBCHistoryPlan As GridView = CType(e.Row.FindControl("gvBCHistoryPlan"), GridView)
                            AddHandler gvBCHistoryPlan.RowDataBound, AddressOf gvBCHistoryPlan_RowDataBound
                            gvBCHistoryPlan.DataSource = GetDataTable(objXmlNode.SelectSingleNode("BC_PLAN"), "CASE")
                            gvBCHistoryPlan.DataBind()
                        End If
                    Else
                        CType(e.Row.FindControl("trPaymentType"), HtmlTableRow).Visible = False
                    End If

                    If objXmlNode.SelectSingleNode("PLB") IsNot Nothing Then


                        If objXmlNode.SelectSingleNode("PLB/PLB_DETAILS") IsNot Nothing Then
                            Dim gvPLBDetail As GridView = CType(e.Row.FindControl("gvPLBDetail"), GridView)
                            AddHandler gvPLBDetail.RowDataBound, AddressOf gvPLBDetail_RowDataBound
                            gvPLBDetail.DataSource = GetDataTable(objXmlNode.SelectSingleNode("PLB"), "")
                            gvPLBDetail.DataBind()
                        End If
                    End If

                    '
                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            Finally
                objXmlNode = Nothing
                objXmlDoc = Nothing
            End Try
        End If
        
    End Sub

    Protected Sub gvBCHistoryPlan_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim objXmlNode As XmlNode
            Dim objXmlDoc As New XmlDocument
            Dim dvDataRow As DataRowView

            Try

                If Session("BC_HISTORY") IsNot Nothing Then

                    objXmlDoc.LoadXml(Session("BC_HISTORY"))
                    objXmlNode = objXmlDoc.DocumentElement.SelectNodes("BC_HISTORY").Item(Val(ViewState("RowIndex")))
                    CType(e.Row.FindControl("lblCaseName"), Label).Text = DataBinder.Eval(e.Row.DataItem, "INC_PLAN_NAME")
                    If objXmlNode.SelectSingleNode("BC_PLAN/CASE/PLAN_DETAILS") IsNot Nothing Then
                        Dim GvIncSlabsNested As GridView = CType(e.Row.FindControl("GvIncSlabsNested"), GridView)
                        'AddHandler GvIncSlabsNested.RowDataBound, AddressOf GvIncSlabsNested_RowDataBound
                        GvIncSlabsNested.DataSource = GetDataTable(objXmlNode.SelectNodes("BC_PLAN/CASE").Item(e.Row.RowIndex), "PLAN_DETAILS")
                        GvIncSlabsNested.DataBind()

                        dvDataRow = e.Row.DataItem
                        If (dvDataRow.DataView.Table.Columns("PAYMENTTYPENAME")) IsNot Nothing Then
                            GvIncSlabsNested.HeaderRow.Cells(2).Text = DataBinder.Eval(e.Row.DataItem, "PAYMENTTYPENAME")
                        End If
                    End If
                    If objXmlNode.SelectNodes("BC_PLAN/CASE").Item(e.Row.RowIndex).SelectSingleNode("NIDT_FIELDS_ID") IsNot Nothing Then
                        Dim objOutputCriteriaXmlXml As New XmlDocument
                        'Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase
                        ' Dim ds As New DataSet
                        ' Dim objXmlReader As XmlNodeReader

                        'objOutputCriteriaXmlXml = objbzBusinessCase.List_NIDTFields
                        'If objOutputCriteriaXmlXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        '    Dim objTempMidtXml As New XmlDocument
                        '    objTempMidtXml.LoadXml("<INC_LISTNIDTFIELDS_TYPE_OUTPUT></INC_LISTNIDTFIELDS_TYPE_OUTPUT >")
                        Dim objArrayList As New ArrayList(objXmlNode.SelectNodes("BC_PLAN/CASE").Item(e.Row.RowIndex).SelectSingleNode("NIDT_FIELDS_ID").Attributes("ID").InnerText.ToString().Split(","))
                        Dim chkSelectedCriteria As CheckBoxList = CType(e.Row.FindControl("chkSelectedCriteria"), CheckBoxList)

                        '    For ii As Integer = 0 To objArrayList.Count - 1
                        '        Dim objN As XmlNode = objOutputCriteriaXmlXml.DocumentElement.SelectSingleNode("NIDTFIELDS[@NIDT_FIELDS_ID='" + objArrayList(ii).ToString().Trim() + "']")
                        '        If objN IsNot Nothing Then
                        '            objTempMidtXml.DocumentElement.AppendChild(objTempMidtXml.ImportNode(objN, True))
                        '        End If
                        '    Next

                        'ds = New DataSet()
                        'objXmlReader = New XmlNodeReader(objTempMidtXml)
                        'ds.ReadXml(objXmlReader)
                        chkSelectedCriteria.DataSource = objArrayList ' ds.Tables("NIDTFIELDS").DefaultView
                        'chkSelectedCriteria.DataValueField = "NIDT_FIELDS_ID"
                        'chkSelectedCriteria.DataTextField = "NIDT_FIELDS_NAME"
                        chkSelectedCriteria.DataBind()

                        For i As Integer = 0 To chkSelectedCriteria.Items.Count - 1
                            chkSelectedCriteria.Items(i).Selected = True
                        Next
                        'End If
                    End If

                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            Finally
                objXmlNode = Nothing
                objXmlDoc = Nothing
            End Try
        End If
        
    End Sub

    Protected Sub gvPLBDetail_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim objXmlNode As XmlNode
            Dim objXmlDoc As New XmlDocument
            Try
                If Session("BC_HISTORY") IsNot Nothing Then

                    objXmlDoc.LoadXml(Session("BC_HISTORY"))
                    objXmlNode = objXmlDoc.DocumentElement.SelectNodes("BC_HISTORY").Item(Val(ViewState("RowIndex")))

                    If objXmlNode.SelectSingleNode("PLB").Attributes("PLBTYPEID").InnerText <> "" Then
                        CType(e.Row.FindControl("lblPLBType"), Label).Text = IIf(objXmlNode.SelectSingleNode("PLB").Attributes("PLBTYPEID").InnerText = "1", "Fixed", "Slab Based")
                        If objXmlNode.SelectSingleNode("PLB").Attributes("PLBAMOUNT").InnerText <> "" Then
                            CType(e.Row.FindControl("lblPLBBonusAmount"), Label).Text = objXmlNode.SelectSingleNode("PLB").Attributes("PLBAMOUNT").InnerText
                        Else
                            CType(e.Row.FindControl("trPLBBonusAmount"), HtmlTableRow).Visible = False
                        End If
                    End If

                    If objXmlNode.SelectSingleNode("PLB/PLB_DETAILS") IsNot Nothing Then
                        Dim GvIncPLBNested As GridView = CType(e.Row.FindControl("GvIncPLBNested"), GridView)
                        GvIncPLBNested.DataSource = GetDataTable(objXmlNode.SelectNodes("PLB").Item(e.Row.RowIndex), "PLB_DETAILS")
                        GvIncPLBNested.DataBind()
                    End If


                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            Finally
                objXmlNode = Nothing
                objXmlDoc = Nothing
            End Try
        End If
        
    End Sub


    Function GetDataTable(ByVal objXmlNode As XmlNode, ByVal strTableName As String) As DataTable
        Dim ds As New DataSet
        Dim objReader As XmlNodeReader
        Try
            objReader = New XmlNodeReader(objXmlNode)
            ds.ReadXml(objReader)
            If strTableName <> "" Then
                Return ds.Tables(strTableName)
            Else
                Return ds.Tables(0)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Function
End Class
