Imports System.Xml
Imports System.Xml.XPath
Imports System.Xml.Xsl
Imports System.IO
Imports System.Text
Partial Class GetServerResponse
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            '   objeAAMS.ExpirePageCache()
            GenerateXML()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GenerateXML()
        Try
            Response.Clear()
            Dim statesString As String

            statesString = GetStatesXMLString(Request.QueryString("ID").ToString())

            If statesString = "" Then
                statesString = GetStatesXMLString(Request.QueryString("ID").ToString())
            End If

            Response.Clear()
            Response.ContentType = "text/xml"
            Response.Write(statesString)
            Response.End()
        Catch ex As Exception

        End Try
      
    End Sub

    Public Function GetStatesXMLString(ByVal strID As String) As String
        Try
            Dim objXmlDoc As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim xPathDoc As XPathDocument
            Dim objnode As XmlNode
            Dim strPageName As String = ""
            objXmlDoc.LoadXml(Session("LeftBar"))

            If Session("EmployeePageName") Is Nothing OrElse Session("EmployeePageName") = "" Then
                strPageName = "MSSR_Employee.aspx"
            Else
                strPageName = Session("EmployeePageName")
            End If

            objnode = objXmlDoc.DocumentElement.SelectSingleNode("HEADING[@ID='2']/Item[@ID='8']")
            If objnode IsNot Nothing Then
                objnode.Attributes("LINK").Value = "Setup/" & strPageName
            End If

            objXmlReader = New XmlNodeReader(objXmlDoc)
            xPathDoc = New XPathDocument(objXmlReader)
            Dim transformToNode As XslTransform = New XslTransform
            transformToNode.Load(New XPathDocument(Server.MapPath("~/App_Data/Menu.xslt")).CreateNavigator(), New XmlUrlResolver)
            Dim xslArgs As XsltArgumentList = New XsltArgumentList
            xslArgs.AddParam("ID", "", strID)
            Dim NodeMemoryStream As MemoryStream = New MemoryStream
            Dim NodeXmlTextWriter As XmlTextWriter = New XmlTextWriter(NodeMemoryStream, Encoding.UTF8)
            NodeXmlTextWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'")
            transformToNode.Transform(xPathDoc, xslArgs, NodeXmlTextWriter)
            NodeXmlTextWriter.Flush()
            NodeMemoryStream.Position = 0
            Dim NodeStreamReader As StreamReader = New StreamReader(NodeMemoryStream)
            Return NodeStreamReader.ReadToEnd()
        Catch ex As Exception
            Return ""
        End Try
    End Function

End Class
