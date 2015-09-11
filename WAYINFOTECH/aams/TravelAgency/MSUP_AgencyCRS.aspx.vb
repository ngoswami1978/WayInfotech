
Partial Class TravelAgency_MSUP_AgencyCRS
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage

#Region "Code for Filter "
    'Code Added by M. K
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    'Code Added by M. K
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblError.Text = String.Empty
        If Not Page.IsPostBack Then
            objeAAMS.BindDropDown(drpCRS, "CRS", True)
            If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                AgencyCRSView()
            End If
            Session("AgencyCRS") = Nothing
        End If
    End Sub
    '*********************************************************************************************************
    '****************************Method for View Agency *****************************************************
    '*********************************************************************************************************
    Private Sub AgencyCRSView()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAgency As New AAMS.bizMaster.bzAgencyGroup
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            objInputXml.LoadXml("<UP_GETCRSDETAILS_INPUT><LCODE></LCODE></UP_GETCRSDETAILS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerXml = Session("Action").ToString().Split("|").GetValue(1)
            'Here Back end Method Call
            objOutputXml = objbzAgency.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                Dim objIAppendXml, objOAppendXml As New XmlDocument
                Dim objIXml As New XmlDocument
                Dim objxmldocfrag As XmlDocumentFragment
                Dim objNode As XmlNode
                Dim objNodeList As XmlNodeList

                objIAppendXml.LoadXml("<Document><CRS RN='' LCODE='' DATE='' CURRENTID='' CRS='' OFFICEID='' Action='' /></Document>")
                If Session("EmployeeIP") Is Nothing Then
                    objOAppendXml.LoadXml("<UP_UPDATECRSDETAILS_INPUT></UP_UPDATECRSDETAILS_INPUT>")
                Else
                    objOAppendXml.LoadXml(Convert.ToString(Session("AgencyCRS")))
                End If
                objNodeList = objOutputXml.DocumentElement.SelectNodes("CRS")
                For Each objNode In objNodeList
                    objIAppendXml.DocumentElement.SelectSingleNode("IPAddress").Attributes("IP").Value = objNode.Attributes("IP").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("CRS").Attributes("RN").Value = objNode.Attributes("RN").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("CRS").Attributes("LCODE").Value = objNode.Attributes("RN").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("CRS").Attributes("CURRENTID").Value = objNode.Attributes("RN").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("CRS").Attributes("CRS").Value = objNode.Attributes("RN").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("CRS").Attributes("OFFICEID").Value = objNode.Attributes("RN").Value
                    objxmldocfrag = objOAppendXml.CreateDocumentFragment()
                    objxmldocfrag.InnerXml = objIAppendXml.DocumentElement.InnerXml
                    objOAppendXml.DocumentElement.AppendChild(objxmldocfrag)
                Next
                Session("AgencyCRS") = objOAppendXml.OuterXml

                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdCRSAgency.DataSource = ds.Tables("CRS")
                grdCRSAgency.DataBind()
            Else
                grdCRSAgency.DataSource = String.Empty
                grdCRSAgency.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            objbzAgency = Nothing
            ds = Nothing
        End Try
    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim objInputXml As New XmlDocument
        Dim objIxml As New XmlDocument
        Dim objxmldocfrag As XmlDocumentFragment
        Dim objXmlReader As XmlReader
        Dim ds As New DataSet
        Dim objXMLNode As XmlNode
        Try
            objIxml.LoadXml("<Document><CRS RN='' LCODE='' DATE='' CURRENTID='' CRS='' OFFICEID='' Action='' /></Document>")
            If Session("AgencyCRS") Is Nothing Then
                objInputXml.LoadXml("<UP_UPDATECRSDETAILS_INPUT></UP_UPDATECRSDETAILS_INPUT>")
            Else
                objInputXml.LoadXml(Convert.ToString(Session("AgencyCRS")))
            End If
            If Not ViewState("RN") Is Nothing Then
                objXMLNode = objInputXml.DocumentElement.SelectSingleNode("CRS[@RN='" & ViewState("RN") & "']")
                objInputXml.DocumentElement.RemoveChild(objXMLNode)
            End If
            If drpCurrentID.SelectedValue.ToUpper = "TRUE" Then
                If objInputXml.DocumentElement.SelectNodes("CRS[@CURRENTID='True']").Count = 0 Then

                    If objInputXml.DocumentElement.SelectNodes("CRS").Count = 0 Then
                        objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("RN").Value = 1
                    Else
                        objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("RN").Value = objInputXml.DocumentElement.SelectNodes("CRS").Item(objInputXml.DocumentElement.SelectNodes("CRS").Count - 1).Attributes("RN").Value + 1
                    End If
                    objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("LCODE").Value = "1212" 'Session("Action").ToString().Split("|").GetValue(1)
                    objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("CURRENTID").Value = drpCurrentID.SelectedValue
                    objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("CRS").Value = drpCRS.SelectedValue
                    objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("OFFICEID").Value = txtOfficeId.Text


                    If Not ViewState("RN") Is Nothing Then
                        ViewState("RN") = Nothing
                    End If
                    objxmldocfrag = objInputXml.CreateDocumentFragment()
                    objxmldocfrag.InnerXml = objIxml.DocumentElement.InnerXml
                    objInputXml.DocumentElement.AppendChild(objxmldocfrag)
                    Session("AgencyCRS") = objInputXml.OuterXml
                Else
                    lblError.Text = "You have not right to add more than one CurrentID 'True'"
                End If
            Else
                If objInputXml.DocumentElement.SelectNodes("CRS").Count = 0 Then
                    objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("RN").Value = 1
                Else
                    objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("RN").Value = objInputXml.DocumentElement.SelectNodes("CRS").Item(objInputXml.DocumentElement.SelectNodes("CRS").Count - 1).Attributes("RN").Value + 1
                End If
                objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("LCODE").Value = "1212" 'Session("Action").ToString().Split("|").GetValue(1)
                objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("CURRENTID").Value = drpCurrentID.SelectedValue
                objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("CRS").Value = drpCRS.SelectedValue
                objIxml.DocumentElement.SelectSingleNode("CRS").Attributes("OFFICEID").Value = txtOfficeId.Text
                If Not ViewState("RN") Is Nothing Then
                    ViewState("RN") = Nothing
                End If
                objxmldocfrag = objInputXml.CreateDocumentFragment()
                objxmldocfrag.InnerXml = objIxml.DocumentElement.InnerXml
                objInputXml.DocumentElement.AppendChild(objxmldocfrag)
                Session("AgencyCRS") = objInputXml.OuterXml
            End If
            objXmlReader = New XmlNodeReader(objInputXml)
            ds.ReadXml(objXmlReader)
            grdCRSAgency.DataSource = ds.Tables("CRS")
            grdCRSAgency.DataBind()
            AgencyCRSReset()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objInputXml, objOutputXml As New XmlDocument

        Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
        Try
            If Not Session("AgencyCRS") Is Nothing Then
                objInputXml.LoadXml(Session("AgencyCRS"))
                'Here Back end Method Call
                objOutputXml = objbzEmployee.AssignIP(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                        lblError.Text = objeAAMSMessage.messUpdate
                    Else
                        lblError.Text = objeAAMSMessage.messInsert
                    End If
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            Else
                lblError.Text = "Please add atleast one CRS."
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub grdCRSAgency_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdCRSAgency.ItemCommand
        If e.CommandName = "EditX" Then
            AgencyEdit(e.CommandArgument)
        End If
        If e.CommandName = "DeleteX" Then
            AgencyDelete(e.CommandArgument)
        End If
    End Sub
    Private Sub AgencyEdit(ByVal strRN As String)
        Dim objInputXml As New XmlDocument
        Try
            If Not Session("AgencyCRS") Is Nothing Then
                objInputXml.LoadXml(Convert.ToString(Session("AgencyCRS")))
                With objInputXml.DocumentElement.SelectSingleNode("CRS[@RN='" & strRN & "']")
                    drpCRS.SelectedValue = .Attributes("CRS").Value
                    drpCurrentID.SelectedValue = .Attributes("CURRENTID").Value
                    txtOfficeId.Text = .Attributes("OFFICEID").Value
                    ViewState("RN") = .Attributes("RN").Value
                End With
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub AgencyDelete(ByVal strRN As String)
        Dim objInputXml As New XmlDocument
        Dim objXmlReader As XmlReader
        Dim ds As New DataSet
        Try
            If Not Session("AgencyCRS") Is Nothing Then
                objInputXml.LoadXml(Convert.ToString(Session("AgencyCRS")))
                objInputXml.DocumentElement.RemoveChild(objInputXml.DocumentElement.SelectSingleNode("CRS[@RN='" & strRN & "']"))
                objXmlReader = New XmlNodeReader(objInputXml)
                ds.ReadXml(objXmlReader)
                grdCRSAgency.DataSource = ds.Tables("CRS")
                grdCRSAgency.DataBind()
                Session("AgencyCRS") = objInputXml.OuterXml
            Else
                grdCRSAgency.DataSource = String.Empty
                grdCRSAgency.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub AgencyCRSReset()
        txtOfficeId.Text = String.Empty
        drpCRS.SelectedIndex = 0
        drpCurrentID.SelectedIndex = 0
    End Sub
End Class
