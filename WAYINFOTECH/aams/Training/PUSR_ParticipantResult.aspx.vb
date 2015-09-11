
Partial Class Training_PUSR_ParticipantResult
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim strBuilder As StringBuilder
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
            Session("PageName") = Request.Url.ToString()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ParticipantQuestionAnswer']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ParticipantQuestionAnswer']").Attributes("Value").Value)
                    If strBuilder(4) = "0" Then
                        btnExport.Enabled = False
                    End If
                End If
            Else
                objeAAMS.SecurityCheck(31)
            End If

            ViewRecords()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
#End Region


    Private Sub ViewRecords()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objTraining As New AAMS.bizTraining.bzTraining


            objInputXml.LoadXml("<TR_PARTICIPANT_TEST_RESULT_INPUT><STAFF_ID/><DAYS/> </TR_PARTICIPANT_TEST_RESULT_INPUT>")

            If Request.QueryString("TR_COURSEP_ID") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("STAFF_ID").InnerText = objED.Decrypt(Request.QueryString("TR_COURSEP_ID").Trim())
            End If
            If Request.QueryString("DAYS") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("DAYS").InnerText = Request.QueryString("DAYS").Trim()
            End If
            'Here Back end Method Call
            objOutputXml = objTraining.TestResult(objInputXml)
            ' objOutputXml.Load("c:/test1.xml")
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Dim objNode As XmlNode
                '<COURSEDETAILS COURSENAME ='aaaaaaaa' COURSELEVEL='ccccccc' DAYS='2' TOTALMARKS='10' MARKSSCORED='5'/>

                objNode = objOutputXml.DocumentElement.SelectSingleNode("COURSEDETAILS")
                txtCourseTitle.Text = objNode.Attributes("COURSENAME").Value
                txtCourseLevel.Text = objNode.Attributes("COURSELEVEL").Value
                txtTestDay.Text = objNode.Attributes("DAYS").Value
                txtTotalMarks.Text = objNode.Attributes("TOTALMARKS").Value
                txtMarksObtained.Text = objNode.Attributes("MARKSSCORED").Value
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvParticipantResult.DataSource = ds.Tables("STAFF_TEST")
                gvParticipantResult.DataBind()
            Else
                gvParticipantResult.DataSource = Nothing
                gvParticipantResult.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Protected Sub gvParticipantResult_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvParticipantResult.RowDataBound

        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            e.Row.Cells(0).Text = (e.Row.RowIndex + 1).ToString

            ' e.Row.Cells(1).Attributes.Add("style", "white-space: normal; word-wrap: break-word; word-break: break-all;")
            Dim strRightAns As String = e.Row.Cells(6).Text
            Dim strGivenAns As String = e.Row.Cells(7).Text
            e.Row.Cells(8).Text = IIf(strRightAns.Equals(strGivenAns, StringComparison.OrdinalIgnoreCase), "Yes", "No")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub gvParticipantResult_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvParticipantResult.SelectedIndexChanged

    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objTraining As New AAMS.bizTraining.bzTraining


            objInputXml.LoadXml("<TR_PARTICIPANT_TEST_RESULT_INPUT><STAFF_ID/><DAYS/> </TR_PARTICIPANT_TEST_RESULT_INPUT>")

            If Request.QueryString("TR_COURSEP_ID") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("STAFF_ID").InnerText = objED.Decrypt(Request.QueryString("TR_COURSEP_ID").Trim())
            End If
            If Request.QueryString("DAYS") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("DAYS").InnerText = Request.QueryString("DAYS").Trim()
            End If
            'Here Back end Method Call
            objOutputXml = objTraining.TestResult(objInputXml)
            ' objOutputXml.Load("c:/test1.xml")
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ' Dim objNode As XmlNode
                '<COURSEDETAILS COURSENAME ='aaaaaaaa' COURSELEVEL='ccccccc' DAYS='2' TOTALMARKS='10' MARKSSCORED='5'/>

                'objNode = objOutputXml.DocumentElement.SelectSingleNode("COURSEDETAILS")
                'txtCourseTitle.Text = objNode.Attributes("COURSENAME").Value
                'txtCourseLevel.Text = objNode.Attributes("COURSELEVEL").Value
                'txtTestDay.Text = objNode.Attributes("DAYS").Value
                'txtTotalMarks.Text = objNode.Attributes("TOTALMARKS").Value
                'txtMarksObtained.Text = objNode.Attributes("MARKSSCORED").Value
                ' objXmlReader = New XmlNodeReader(objOutputXml)
                'ds.ReadXml(objXmlReader)
                ' gvParticipantResult.DataSource = ds.Tables("STAFF_TEST")

                'Dim Rowno As New DataColumn
                'Rowno.DataType = GetType(String)
                'Rowno.AllowDBNull = True
                '' Rowno.AutoIncrement = True

                'Dim Correct As New DataColumn
                'Correct.DataType = GetType(String)
                'Correct.AllowDBNull = True

                'ds.Tables("STAFF_TEST").Columns.Add("Rowno")
                'ds.Tables("STAFF_TEST").Columns.Add("Correct")
                'ds.AcceptChanges()

                ' gvParticipantResult.DataBind()

                '<STAFF_TEST 
                '               QS_ID = "1029"
                '               QS_TEXT = "The Global indictor PN(" is used to describe,")
                '               QS_OPTION1 = "Travel across the Pacific Ocean"
                '               QS_OPTION2 = "Travel between South west Pacific & South America via. North America"
                '               QS_OPTION3 = "Travel between TC 3 & South America Via North America"
                '               QS_OPTION4 = "Travel between tc 3 & North America via South America"
                '               QS_RIGHT_OPTION = "2"
                '               ANSWER_GIVEN = "1"
                '/> 

                Dim objNewxmlDoc As New XmlDocument
                objNewxmlDoc.LoadXml("<TR_PARTICIPANT_TEST_RESULT_OUTPUT><COURSEDETAILS COURSENAME='' COURSELEVEL='' DAYS='' TOTALMARKS='' MARKSSCORED='' /> <STAFF_TEST QS_ID='' QS_TEXT='' QS_OPTION1='' QS_OPTION2='' QS_OPTION3='' QS_OPTION4='' QS_RIGHT_OPTION='' ANSWER_GIVEN='' ROWNO='' CORRECT='' /> </TR_PARTICIPANT_TEST_RESULT_OUTPUT> ")

                Dim i As Integer = 0
                Dim objxmlnode As XmlNode = objNewxmlDoc.DocumentElement.SelectSingleNode("STAFF_TEST")
                Dim objxmlClonenode As XmlNode = objxmlnode.CloneNode(True)
                objNewxmlDoc.DocumentElement.RemoveChild(objxmlnode)
                For Each objnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("STAFF_TEST")
                    objxmlClonenode.Attributes("QS_ID").Value = objnode.Attributes("QS_ID").Value.ToString
                    objxmlClonenode.Attributes("QS_TEXT").Value = objnode.Attributes("QS_TEXT").Value.ToString
                    objxmlClonenode.Attributes("QS_OPTION1").Value = objnode.Attributes("QS_OPTION1").Value.ToString
                    objxmlClonenode.Attributes("QS_OPTION2").Value = objnode.Attributes("QS_OPTION2").Value.ToString
                    objxmlClonenode.Attributes("QS_OPTION3").Value = objnode.Attributes("QS_OPTION3").Value.ToString
                    objxmlClonenode.Attributes("QS_OPTION4").Value = objnode.Attributes("QS_OPTION4").Value.ToString
                    objxmlClonenode.Attributes("QS_RIGHT_OPTION").Value = objnode.Attributes("QS_RIGHT_OPTION").Value.ToString
                    objxmlClonenode.Attributes("ANSWER_GIVEN").Value = objnode.Attributes("ANSWER_GIVEN").Value.ToString
                    objxmlClonenode.Attributes("ROWNO").Value = (i + 1).ToString
                    Dim strRightAns As String = objnode.Attributes("QS_RIGHT_OPTION").Value.ToString
                    Dim strGivenAns As String = objnode.Attributes("ANSWER_GIVEN").Value.ToString
                    objxmlClonenode.Attributes("CORRECT").Value = IIf(strRightAns.Equals(strGivenAns, StringComparison.OrdinalIgnoreCase), "Yes", "No")

                    objNewxmlDoc.DocumentElement.AppendChild(objxmlClonenode)
                    objxmlClonenode = objxmlnode.CloneNode(True)
                    i = i + 1
                Next

                'For i As Integer = 0 To ds.Tables("STAFF_TEST").Rows.Count - 1

                '    ds.Tables("STAFF_TEST").Rows(i)("Rowno") = (i + 1).ToString
                '    Dim strRightAns As String = ds.Tables("STAFF_TEST").Rows(i)("QS_RIGHT_OPTION").ToString
                '    Dim strGivenAns As String = ds.Tables("STAFF_TEST").Rows(i)("ANSWER_GIVEN").ToString
                '    ds.Tables("STAFF_TEST").Rows(i)("Correct") = IIf(strRightAns.Equals(strGivenAns, StringComparison.OrdinalIgnoreCase), "Yes", "No")
                'Next             
                'objNewxmlDoc.LoadXml(ds.GetXml)
            
                Dim objExport As New ExportExcel
                Dim strArray() As String = {"S.No.", "Question", "Ans1", "Ans2", "Ans3", "Ans4", "Right Ans", "Ans Given", "Correct"}
                Dim intArray() As Integer = {8, 1, 2, 3, 4, 5, 6, 7, 9}
                objExport.ExportDetails(objNewxmlDoc, "STAFF_TEST", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportParticipant.xls")
            Else

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
End Class
