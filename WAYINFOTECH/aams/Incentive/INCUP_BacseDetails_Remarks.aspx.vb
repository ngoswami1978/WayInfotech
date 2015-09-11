
Partial Class INCUP_BacseDetails_Remarks
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Try

            Session("PageName") = strurl
            hdTabID.Style.Add("display", "none")

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            If Not Page.IsPostBack Then
                Bindata()
                If Request.QueryString("Chain_Code") IsNot Nothing Then
                    hdEnChainCode.Value = Request.QueryString("Chain_Code")
                    hdChainCode.Value = objED.Decrypt(hdEnChainCode.Value)
                    If Request.QueryString("BCaseID") IsNot Nothing Then
                        GetAllRemrks(Request.QueryString("BCaseID").Trim())
                        hdBcID.Value = Request.QueryString("BCaseID").Trim()
                    End If

                    bindApprovars(hdBcID.Value)
                End If
                If Request.QueryString("TabID") IsNot Nothing Then
                    hdTabID.Text = Request.QueryString("TabID")
                End If
            End If
            If Request.QueryString("Action") IsNot Nothing Then
                hdAction.Value = Request.QueryString("Action")
            End If
            If Request.QueryString("RefreshAction") IsNot Nothing Then
                hdRefreshAction.Value = Request.QueryString("RefreshAction")
            End If


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Private Sub bindApprovars(ByVal strBCaseID As String)
        Try
            '*********Code for Binding List of Approvars**********
            Dim objOutputXml, objInputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet()
            Dim objOnlineConn As New AAMS.bizIncetive.bzBusinessCase
            objOutputXml = New XmlDocument
            objInputXml.LoadXml("<INC_GETBUSINESSAPPROVERS_INPUT><BC_ID></BC_ID></INC_GETBUSINESSAPPROVERS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("BC_ID").InnerText = strBCaseID
            objOutputXml = objOnlineConn.Get_BCApprovers(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdApprovars.DataSource = ds.Tables("APPROVERS").DefaultView
                grdApprovars.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

  


    Sub Bindata()
        Dim TabText As New ArrayList()
        Try
            TabText.Add("Business Case")
            TabText.Add("Details")
            TabText.Add("Location")
            TabText.Add("Region Remarks")
            TabText.Add("Market Remarks")
            TabText.Add("Approvers")
            theTabSubStrip.DataSource = TabText
            theTabSubStrip.DataBind()



        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub



    Protected Sub theTabSubStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabSubStrip.ItemDataBound
        Dim Button2 As Button
        Button2 = e.Item.FindControl("Button2")
        If e.Item.ItemIndex = 0 Then
            Button2.CssClass = "headingtab"
        End If
        Button2 = e.Item.FindControl("Button2")

        Button2.Attributes.Add("onclick", "return TabMethodAgencySubGroup('" & Button2.ClientID.ToString() & "',6);")


    End Sub
   

  
    Protected Sub btnSaveRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveRemarks.Click
        Try
            hdTabID.Text = "0"

            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objRmrks As New AAMS.bizIncetive.bzBusinessCase
            objInputXml.LoadXml("<INC_UPDATE_BUSINESSCASE_INPUT><BUSINESSCASE_REMARKS BC_ID='' BC_REMARKS_TYPE='' BC_REMARKS='' EmployeeID=''/></INC_UPDATE_BUSINESSCASE_INPUT>")

            If txtRemarks.Text.Trim().Length = 0 Then
                lblError.Text = "Please fill Remarks"
                txtRemarks.Focus()
                Return
            End If
            If Session("Security") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("BUSINESSCASE_REMARKS").Attributes("EmployeeID").Value = objeAAMS.EmployeeID(Session("Security"))
            End If
            objInputXml.DocumentElement.SelectSingleNode("BUSINESSCASE_REMARKS").Attributes("BC_REMARKS_TYPE").Value = "1" ' drpRemarksType.SelectedValue.Trim()
            objInputXml.DocumentElement.SelectSingleNode("BUSINESSCASE_REMARKS").Attributes("BC_REMARKS").Value = txtRemarks.Text
            If hdBcID.Value.Trim() = "" Then
                lblError.Text = "Please Save a Business Case"
                Return
            End If
            objInputXml.DocumentElement.SelectSingleNode("BUSINESSCASE_REMARKS").Attributes("BC_ID").Value = hdBcID.Value


            objOutputXml = objRmrks.UpdateRemarks(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = "Remarks Added successfuly"
                txtRemarks.Text = ""
                hdBcID.Value = objOutputXml.DocumentElement.SelectSingleNode("BUSINESSCASE_REMARKS").Attributes("BC_ID").Value
                GetAllRemrks(hdBcID.Value.Trim)
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub GetAllRemrks(ByVal strBcID As String)
        Try
            Dim objInputXml, objOutputXml, objOutputXmlMarket, objOutputXmlRegion As New XmlDocument
            Dim objRdrNode As XmlNodeReader
            Dim ds As New DataSet
            Dim objRmrks As New AAMS.bizIncetive.bzBusinessCase
            objInputXml.LoadXml("<INC_GETBUSINESSREMARK_INPUT><BC_ID></BC_ID></INC_GETBUSINESSREMARK_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("BC_ID").InnerText = strBcID
            objOutputXml = objRmrks.GetRemarks(objInputXml)
            'objOutputXml.DocumentElement.SelectNodes("BUSINESSREMARK[@BC_REMARKS_TYPE='Market Remark']")
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objRdrNode = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objRdrNode)
                Dim dvR As DataView = ds.Tables("BUSINESSREMARK").DefaultView
                dvR.RowFilter = "BC_REMARKS_TYPE='Region Remark'"
                grdvRemarks.DataSource = dvR 'ds.Tables("BUSINESSREMARK").DefaultView
                grdvRemarks.DataBind()

                Dim dvM As DataView = ds.Tables("BUSINESSREMARK").DefaultView
                dvM.RowFilter = "BC_REMARKS_TYPE='Market Remark'"
                grdvMarketRemarks.DataSource = dvM 'ds.Tables("BUSINESSREMARK").DefaultView
                grdvMarketRemarks.DataBind()

            Else
                '  lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

  

    Protected Sub grdApprovars_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdApprovars.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lblLevel As Label = CType(e.Row.FindControl("lblLevel"), Label)

                Dim lblEmpName As Label = CType(e.Row.FindControl("lblApprovars"), Label)
                If Not String.IsNullOrEmpty(lblEmpName.Text) Then
                    lblLevel.Text = "Level " + (e.Row.RowIndex + 1).ToString()
                End If

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

   

    Protected Sub btnMktRemarksSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMktRemarksSave.Click
        Try
            hdTabID.Text = "1"
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objRmrks As New AAMS.bizIncetive.bzBusinessCase
            objInputXml.LoadXml("<INC_UPDATE_BUSINESSCASE_INPUT><BUSINESSCASE_REMARKS BC_ID='' BC_REMARKS_TYPE='' BC_REMARKS='' EmployeeID=''/></INC_UPDATE_BUSINESSCASE_INPUT>")

            If txtMktRemarks.Text.Trim().Length = 0 Then
                lblError.Text = "Please fill Remarks"
                txtMktRemarks.Focus()
                Return
            End If
            If Session("Security") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("BUSINESSCASE_REMARKS").Attributes("EmployeeID").Value = objeAAMS.EmployeeID(Session("Security"))
            End If
            objInputXml.DocumentElement.SelectSingleNode("BUSINESSCASE_REMARKS").Attributes("BC_REMARKS_TYPE").Value = "2" ' drpRemarksType.SelectedValue.Trim()
            objInputXml.DocumentElement.SelectSingleNode("BUSINESSCASE_REMARKS").Attributes("BC_REMARKS").Value = txtMktRemarks.Text
            If hdBcID.Value.Trim() = "" Then
                lblError.Text = "Please Save a Business Case"
                Return
            End If
            objInputXml.DocumentElement.SelectSingleNode("BUSINESSCASE_REMARKS").Attributes("BC_ID").Value = hdBcID.Value


            objOutputXml = objRmrks.UpdateRemarks(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = "Remarks Added successfuly"
                txtMktRemarks.Text = ""
                hdBcID.Value = objOutputXml.DocumentElement.SelectSingleNode("BUSINESSCASE_REMARKS").Attributes("BC_ID").Value
                GetAllRemrks(hdBcID.Value.Trim)
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnResetRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResetRemarks.Click
        txtRemarks.Text = ""
    End Sub

    Protected Sub btnResetMarketRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResetMarketRemarks.Click
        txtMktRemarks.Text = ""
    End Sub
End Class