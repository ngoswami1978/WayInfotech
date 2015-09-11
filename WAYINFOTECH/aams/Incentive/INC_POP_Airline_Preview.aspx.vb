Imports System.Data
Imports System.Xml

Partial Class Incentive_INC_POP_Airline_Preview
    Inherits System.Web.UI.Page
    Dim objXmlAirlineName As New XmlDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objXml As New XmlDocument
        Dim objNodeR As XmlNodeReader
        Dim ds As New DataSet
        Try
            If Session("AIRLINE_DATA_PREVIEW") IsNot Nothing And Session("AIRLINE_RAW_DATA") IsNot Nothing Then
                objXmlAirlineName.LoadXml(Session("AIRLINE_RAW_DATA").ToString())
                objXml.LoadXml(Session("AIRLINE_DATA_PREVIEW").ToString())
                objNodeR = New XmlNodeReader(objXml)
                ds.ReadXml(objNodeR)
                If ds.Tables.Count >= 1 Then


                    ds.Tables(0).Columns.Remove("Start")
                    ds.Tables(0).Columns.Remove("End")
                    grdvAirlinePrv.DataSource = ds.Tables(0).DefaultView
                    grdvAirlinePrv.DataBind()


                    For i As Integer = 1 To ds.Tables(0).Columns.Count - 1
                        grdvAirlinePrv.HeaderRow.Cells(i).Text = ReturnAirlineName(ds.Tables(0).Columns(i).ColumnName.Substring(1))
                    Next
                    'Added
                    If objXml.DocumentElement.SelectSingleNode("PREVIEW").Attributes("PAYMENTTYPEID").InnerText = "1" Then
                        adjType.Text = "Rate"
                    Else
                        adjType.Text = "Amount"
                    End If
                    If objXml.DocumentElement.SelectSingleNode("PREVIEW").Attributes("INC_TYPE_ID").InnerText = "2" Then
                        trUpfront.Visible = False
                        ' LblUpfront.Text = objXml.DocumentElement.SelectSingleNode("PREVIEW").Attributes("UPFRONT_AMOUNT").InnerText
                    Else
                        trUpfront.Visible = True
                        txtUpfront.Text = objXml.DocumentElement.SelectSingleNode("PREVIEW").Attributes("UPFRONT_AMOUNT").InnerText
                        If objXml.DocumentElement.SelectSingleNode("PREVIEW").Attributes("UPFRONTTYPEID").InnerText = "1" Then
                            txtUpfrotntType.Text = "One time"
                            lblUpfrontPeriod.Text = "For the Period of"
                            TexUpfrontPeriod.Text = objXml.DocumentElement.SelectSingleNode("PREVIEW").Attributes("FOR_PERIOD_OF").InnerText
                        ElseIf objXml.DocumentElement.SelectSingleNode("PREVIEW").Attributes("UPFRONTTYPEID").InnerText = "2" Then
                            txtUpfrotntType.Text = "Replinishable"
                            lblUpfrontPeriod.Visible = False
                            TexUpfrontPeriod.Visible = False
                        Else
                            txtUpfrotntType.Text = "Fixed"
                            lblUpfrontPeriod.Text = "No.of Payments"
                            TexUpfrontPeriod.Text = objXml.DocumentElement.SelectSingleNode("PREVIEW").Attributes("NO_OF_PAYMENTS").InnerText
                        End If
                    End If

                    txtSignup.Text = objXml.DocumentElement.SelectSingleNode("PREVIEW").Attributes("SIGNUPAMOUNT").InnerText
                    If objXml.DocumentElement.SelectSingleNode("PREVIEW").Attributes("ADJUSTABLE").InnerText.Trim.ToUpper = "TRUE" Then
                        txtSignupAdjustable.Text = "Yes"
                    Else
                        If objXml.DocumentElement.SelectSingleNode("PREVIEW").Attributes("SIGNUPAMOUNT").InnerText <> "" Then
                            txtSignupAdjustable.Text = "No"
                        End If
                    End If



                Else
                    lblError.Text = "No Record found."
                End If

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

   
    Protected Sub grdvAirlinePrv_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvAirlinePrv.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If

        e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
    End Sub


    Function ReturnAirlineName(ByVal strValue As String) As String
        Dim strName As String = ""
        ' <NIDTFIELDS NIDT_FIELDS_ID="1" NIDT_FIELDS_NAME="Productivity" /> 

        If objXmlAirlineName.DocumentElement.SelectSingleNode("NIDTFIELDS[@NIDT_FIELDS_ID='" + strValue + "']") IsNot Nothing Then
            strName = objXmlAirlineName.DocumentElement.SelectSingleNode("NIDTFIELDS[@NIDT_FIELDS_ID='" + strValue + "']").Attributes("NIDT_FIELDS_NAME").Value
        End If
        Return (strName)
    End Function
    Public Function ListAdjustType() As System.Xml.XmlDocument
        Dim objOutputXml As New XmlDocument
        Dim strOutput As String
        strOutput = "<INC_LIST_PAYMENT_TYPE><PAYMENT_TYPE PaymentTypeID='1' PaymentTypeName='Ratebased'/><PAYMENT_TYPE PaymentTypeID='2' PaymentTypeName='Amount'/></INC_LIST_PAYMENT_TYPE>"
        objOutputXml.LoadXml(strOutput)
        Return objOutputXml
    End Function

    Public Function ListIncentiveType() As System.Xml.XmlDocument
        Dim objOutputXml As New XmlDocument
        Dim strOutput As String
        strOutput = "<INC_LIST_INCENTIVE_TYPE><INCENTIVE_TYPE INC_TYPE_ID='1' INC_TYPE_NAME='Upfront Payment'/><INCENTIVE_TYPE INC_TYPE_ID='2' INC_TYPE_NAME='Post Payment'/></INC_LIST_INCENTIVE_TYPE>"
        objOutputXml.LoadXml(strOutput)
        Return objOutputXml
    End Function

    Public Function ListUpfrontType() As System.Xml.XmlDocument
        Dim objOutputXml As New XmlDocument
        Dim strOutput As String
        strOutput = "<INC_LIST_UPFRONT_TYPE><UPFRONT_TYPE INC_TYPE_ID='1' INC_TYPE_NAME='One time'/><UPFRONT_TYPE INC_TYPE_ID='2' INC_TYPE_NAME='Replinishable'/><UPFRONT_TYPE INC_TYPE_ID='3' INC_TYPE_NAME='Fixed'/></INC_LIST_UPFRONT_TYPE>"
        objOutputXml.LoadXml(strOutput)
        Return objOutputXml
    End Function
End Class
