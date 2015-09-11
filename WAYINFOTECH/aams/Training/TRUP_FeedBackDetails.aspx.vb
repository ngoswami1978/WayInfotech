
Partial Class Training_TRUP_FeedBackDetails
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objxmldocfrag As XmlDocumentFragment

#Region "Page Filter()"
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
#End Region
#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                SetData()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "SetData()"
    Protected Sub SetData()
        Dim objbzParticipantFeedBack As New AAMS.bizTraining.bzParticipantFeedBack
        Dim xDoc As New XmlDocument
        Dim objIdoc As New XmlDocument
        Dim objOdoc As New XmlDocument
        Dim strInput As String = "<T_Domain_Input><ParticipantId/><Feedback/></T_Domain_Input>"
        objIdoc.LoadXml(strInput)
        objIdoc.DocumentElement.SelectSingleNode("ParticipantId").InnerText = objED.Decrypt(Request.QueryString("TR_COURSEP_ID").ToString())
        'Call Dll Function Here For Calling Retrieving Xml For Grid
        objOdoc = objbzParticipantFeedBack.ViewFeedBackparticipant(objIdoc)
        'objOdoc.Load("c:\feedback.xml")
        If objOdoc.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            With objOdoc.DocumentElement.SelectSingleNode("PARITCIPANT")
                txtParticipantName.Text = .Attributes("ParticipantName").Value
                txtAgencyName.Text = .Attributes("AgencyName").Value
                txtAgencyAddress.Text = .Attributes("AgencyAddress").Value
                txtCourseDetails.Text = .Attributes("CourseName").Value
                txtCourseDuration.Text = .Attributes("CourseDuration").Value + " Days"
                txtCourseLevel.Text = .Attributes("CourseLevel").Value
                ' txtFromDate.Text = .Attributes("PeriodFrom").Value.Split(" ").GetValue(0)
                '    txtToDate.Text = .Attributes("PeriodTo").Value.Split(" ").GetValue(0)
                txtCourseSession.Text = "( " + .Attributes("PeriodFrom").Value.Split(" ").GetValue(0) + " - " + .Attributes("PeriodTo").Value.Split(" ").GetValue(0) + " )"
                txtLocation.Text = .Attributes("Location").Value
                txtTrainerName.Text = .Attributes("TrainerName").Value

            End With
            BindGrid(ReturnGridXml(objOdoc))
        Else

            lblError.Text = objOdoc.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

        End If

    End Sub
#End Region

#Region "ReturnGridXml(ByVal xDoc As XmlDocument) As XmlDocument"
    Protected Function ReturnGridXml(ByVal xDoc As XmlDocument) As XmlDocument
        Dim xDomainNode As XmlNode
        Dim xDomainNodeList As XmlNodeList
        Dim xTopicNode As XmlNode
        Dim xTopicNodeList As XmlNodeList
        Dim xIDoc As New XmlDocument
        Dim xMain As New XmlDocument
        Dim strInput As String = "<T_DOMAIN_OUTPUT><DOMAIN DOMAINID='' DOMAINNAME='' TOPICID='' TOPICNAME='' EVALMARKS='' COMMENTS='' EVALTYPE='' Status='' /></T_DOMAIN_OUTPUT>"
        Dim intDomain As Integer
        xMain.LoadXml("<T_DOMAIN_OUTPUT></T_DOMAIN_OUTPUT>")
        xDomainNodeList = xDoc.DocumentElement.SelectNodes("Domain")
        For Each xDomainNode In xDomainNodeList
            intDomain = 0
            xTopicNodeList = xDomainNode.SelectNodes("Topics")
            For Each xTopicNode In xTopicNodeList
                xIDoc.LoadXml(strInput)
                With xIDoc.DocumentElement.SelectSingleNode("DOMAIN")
                    .Attributes("DOMAINID").Value = xDomainNode.Attributes("Id").Value
                    If intDomain = 0 Then
                        .Attributes("DOMAINNAME").Value = xDomainNode.Attributes("Name").Value
                        intDomain = intDomain + 1
                    End If
                    .Attributes("TOPICID").Value = xTopicNode.Attributes("TopicId").Value
                    .Attributes("TOPICNAME").Value = xTopicNode.Attributes("TopicName").Value
                    .Attributes("EVALMARKS").Value = xTopicNode.Attributes("Evalmarks").Value
                    .Attributes("COMMENTS").Value = xTopicNode.Attributes("Comments").Value
                    .Attributes("EVALTYPE").Value = xTopicNode.Attributes("Eval_Type").Value
                    .Attributes("Status").Value = xTopicNode.Attributes("Status").Value
                End With
                objxmldocfrag = xMain.CreateDocumentFragment()
                objxmldocfrag.InnerXml = xIDoc.OuterXml
                xMain.DocumentElement.AppendChild(objxmldocfrag)
            Next
        Next
        Return xMain
    End Function
#End Region

#Region "BindGrid(ByVal objOutputXml As XmlDocument)"
    Protected Sub BindGrid(ByVal objOutputXml As XmlDocument)
        Dim objXmlReader As New XmlNodeReader(objOutputXml)
        Dim ds As New DataSet
        ds.ReadXml(objXmlReader)
        If ds.Tables("DOMAIN").Rows.Count <> 0 Then
            gvCourse.DataSource = ds.Tables("DOMAIN")
            gvCourse.DataBind()
        Else
            gvCourse.DataSource = Nothing
            gvCourse.DataBind()
        End If
    End Sub
#End Region



End Class
