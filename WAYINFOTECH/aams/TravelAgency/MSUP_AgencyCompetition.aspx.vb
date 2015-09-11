
Partial Class TravelAgency_MSUP_AgencyCompetition
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
        If Not Page.IsPostBack Then
            objeAAMS.BindDropDown(drpCRSCode, "CRS", True)
            objeAAMS.BindDropDown(drpOnlineStatus, "OS", True)
            If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                AgencyCompetitionView()
            End If
            Session("AgencyComptition") = Nothing
        End If
    End Sub
    '*********************************************************************************************************
    '****************************Method for View Agency *****************************************************
    '*********************************************************************************************************
    Private Sub AgencyCompetitionView()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAgency As New AAMS.bizMaster.bzAgencyGroup
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            objInputXml.LoadXml("<UP_GETCOMPETITIONDETAILS_INPUT><LOCATION_CODE></LOCATION_CODE></UP_GETCOMPETITIONDETAILS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerXml = Session("Action").ToString().Split("|").GetValue(1)
            'Here Back end Method Call
            objOutputXml = objbzAgency.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                Dim objIAppendXml, objOAppendXml As New XmlDocument
                Dim objIXml As New XmlDocument
                Dim objxmldocfrag As XmlDocumentFragment
                Dim objNode As XmlNode
                Dim objNodeList As XmlNodeList

                objIAppendXml.LoadXml("<Document><COMPETITION_DETAILS ComptID='' CRSID='' DATE_END='' DATE_START='' SOLE_USER='' DIAL_BACKUP='' ONLINESTATUSID='' PRINTER_COUNT='' PC_COUNT='' Action='' /></Document>")
                If Session("Agency") Is Nothing Then
                    objOAppendXml.LoadXml("<UP_UPDATECOMPETITIONDETAILS_INPUT></UP_UPDATECOMPETITIONDETAILS_INPUT>")
                Else
                    objOAppendXml.LoadXml(Convert.ToString(Session("Agency")))
                End If
                objNodeList = objOutputXml.DocumentElement.SelectNodes("COMPETITION_DETAILS")
                For Each objNode In objNodeList
                    If objIAppendXml.DocumentElement.SelectNodes("COMPETITION_DETAILS").Count = 0 Then
                        objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("ComptID").Value = 1
                    Else
                        objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("ComptID").Value = objInputXml.DocumentElement.SelectNodes("COMPETITION_DETAILS").Item(objInputXml.DocumentElement.SelectNodes("COMPETITION_DETAILS").Count - 1).Attributes("ComptID").Value + 1
                    End If
                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("CRSID").Value = objNode.Attributes("CRSID").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("DATE_END").Value = objNode.Attributes("DATE_END").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("DATE_START").Value = objNode.Attributes("DATE_START").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("SOLE_USER").Value = objNode.Attributes("SOLE_USER").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("DIAL_BACKUP").Value = objNode.Attributes("DIAL_BACKUP").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("ONLINESTATUSID").Value = objNode.Attributes("ONLINESTATUSID").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("PRINTER_COUNT").Value = objNode.Attributes("PRINTER_COUNT").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("PC_COUNT").Value = objNode.Attributes("PC_COUNT").Value
                    objIAppendXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("Action").Value = "I"

                    objxmldocfrag = objOAppendXml.CreateDocumentFragment()
                    objxmldocfrag.InnerXml = objIAppendXml.DocumentElement.InnerXml
                    objOAppendXml.DocumentElement.AppendChild(objxmldocfrag)
                Next
                Session("Agency") = objOAppendXml.OuterXml

                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdComptitionAgency.DataSource = ds.Tables("COMPETITION_DETAILS")
                grdComptitionAgency.DataBind()
            Else
                grdComptitionAgency.DataSource = String.Empty
                grdComptitionAgency.DataBind()
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
            objIxml.LoadXml("<Document><COMPETITION_DETAILS ComptID='' CRSID='' DATE_END='' DATE_START='' SOLE_USER='' DIAL_BACKUP='' ONLINESTATUSID='' PRINTER_COUNT='' PC_COUNT='' Action='' /></Document>")
            If Session("Agency") Is Nothing Then
                objInputXml.LoadXml("<UP_UPDATECOMPETITIONDETAILS_INPUT></UP_UPDATECOMPETITIONDETAILS_INPUT>")
            Else
                objInputXml.LoadXml(Convert.ToString(Session("Agency")))
            End If
            If Not ViewState("ComptID") Is Nothing Then
                objXMLNode = objInputXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS[@ComptID='" & ViewState("ComptID") & "']")
                objInputXml.DocumentElement.RemoveChild(objXMLNode)
            End If


            If objInputXml.DocumentElement.SelectNodes("COMPETITION_DETAILS").Count = 0 Then
                objIxml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("ComptID").Value = 1
            Else
                objIxml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("ComptID").Value = objInputXml.DocumentElement.SelectNodes("COMPETITION_DETAILS").Item(objInputXml.DocumentElement.SelectNodes("COMPETITION_DETAILS").Count - 1).Attributes("ComptID").Value + 1
            End If
            objIxml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("CRSID").Value = drpCRSCode.SelectedValue
            objIxml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("DATE_END").Value = txtDateEnd.Text
            objIxml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("DATE_START").Value = txtDateStart.Text
            objIxml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("SOLE_USER").Value = drpSoleUser.SelectedValue

            objIxml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("DIAL_BACKUP").Value = drpDialBackup.SelectedValue
            objIxml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("ONLINESTATUSID").Value = drpOnlineStatus.SelectedValue
            objIxml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("PRINTER_COUNT").Value = txtPrinterCount.Text
            objIxml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("PC_COUNT").Value = txtPCCount.Text
            objIxml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS").Attributes("Action").Value = "I"


            If Not ViewState("ComptID") Is Nothing Then
                ViewState("ComptID") = Nothing
            End If
            objxmldocfrag = objInputXml.CreateDocumentFragment()
            objxmldocfrag.InnerXml = objIxml.DocumentElement.InnerXml
            objInputXml.DocumentElement.AppendChild(objxmldocfrag)
            Session("Agency") = objInputXml.OuterXml
           
            objXmlReader = New XmlNodeReader(objInputXml)
            ds.ReadXml(objXmlReader)
            grdComptitionAgency.DataSource = ds.Tables("COMPETITION_DETAILS")
            grdComptitionAgency.DataBind()
            AgencyComtitionReset()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub AgencyComtitionReset()
        drpCRSCode.SelectedIndex = 0
        drpOnlineStatus.SelectedIndex = 0
        txtDateStart.Text = String.Empty
        txtDateEnd.Text = String.Empty
        drpDialBackup.SelectedIndex = 0
        drpSoleUser.SelectedIndex = 0
        txtPCCount.Text = String.Empty
        txtPrinterCount.Text = String.Empty
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
        Try
            If Not Session("Agency") Is Nothing Then
                objInputXml.LoadXml(Session("Agency"))
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
                lblError.Text = "Please add atleast one Comptition."
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            objbzEmployee = Nothing
        End Try
    End Sub

    Protected Sub grdComptitionAgency_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdComptitionAgency.ItemCommand
        If e.CommandName = "EditX" Then
            AgencyEdit(e.CommandArgument)
        End If
        If e.CommandName = "DeleteX" Then
            AgencyDelete(e.CommandArgument)
        End If
    End Sub
    Private Sub AgencyEdit(ByVal strComption As String)
        Dim objInputXml As New XmlDocument
        Try
            If Not Session("Agency") Is Nothing Then
                objInputXml.LoadXml(Convert.ToString(Session("Agency")))
                With objInputXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS[@ComptID='" & strComption & "']")
                    drpCRSCode.SelectedValue = .Attributes("CRSID").Value
                    drpOnlineStatus.SelectedValue = .Attributes("ONLINESTATUSID").Value
                    txtDateStart.Text = .Attributes("DATE_START").Value
                    txtDateEnd.Text = .Attributes("DATE_END").Value
                    drpDialBackup.SelectedValue = .Attributes("DIAL_BACKUP").Value
                    drpSoleUser.SelectedValue = .Attributes("SOLE_USER").Value
                    txtPCCount.Text = .Attributes("PC_COUNT").Value
                    txtPrinterCount.Text = .Attributes("PRINTER_COUNT").Value
                    ViewState("RN") = .Attributes("RN").Value
                End With
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub AgencyDelete(ByVal strComption As String)
        Dim objInputXml As New XmlDocument
        Dim objXmlReader As XmlReader
        Dim ds As New DataSet
        Try
            If Not Session("Agency") Is Nothing Then
                objInputXml.LoadXml(Convert.ToString(Session("Agency")))
                objInputXml.DocumentElement.RemoveChild(objInputXml.DocumentElement.SelectSingleNode("COMPETITION_DETAILS[@ComptID='" & strComption & "']"))
                objXmlReader = New XmlNodeReader(objInputXml)
                ds.ReadXml(objXmlReader)
                grdComptitionAgency.DataSource = ds.Tables("COMPETITION_DETAILS")
                grdComptitionAgency.DataBind()
                Session("Agency") = objInputXml.OuterXml
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
End Class
