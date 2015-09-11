Imports System.Xml
Imports System.Data.SqlClient
Imports AAMS.bizShared
Namespace bizMaster
    Public Class bzEmailTemplates
        Implements bizInterface.BizLayerI
        Const strClass_NAME = "bzEmailTemplates"
        Const srtVIEW_OUTPUT = "<MS_VIEWEMAILTEMPLATES_OUTPUT><TEMPLATES MailTemplateName='' MailTemplate=''/><Errors Status=''><Error Code='' Description=''/></Errors></MS_VIEWEMAILTEMPLATES_OUTPUT>"
        Public Function Add() As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Add

        End Function

        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Delete

        End Function

        Public Function Search(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Search
            
        End Function

        Public Function Update(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.Update
           
        End Function

        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument Implements bizInterface.BizLayerI.View
            '***********************************************************************
            'Purpose:This function gives details of Email Group.
            'Input  :
            '<MS_VIEWEMAILTEMPLATES_INPUT>
            '<TEMPLATES MailTemplateName='' />
            '</MS_VIEWEMAILTEMPLATES_INPUT>

            'Output :
            '<MS_VIEWEMAILGROUP_OUTPUT>
            '<MS_VIEWEMAILTEMPLATES_OUTPUT>
            '<TEMPLATES MailTemplateName='' MailTemplate=''/>
            '<Errors Status="">
            '<Error Code="" Description=""/>
            '</Errors>
            '</MS_VIEWEMAILTEMPLATES_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim STRMailTemplateName As String


            Const strMETHOD_NAME As String = "View"
            Dim blnRecordFound As Boolean
            Try
                objOutputXml.LoadXml(srtVIEW_OUTPUT)
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                STRMailTemplateName = IndexDoc.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("MailTemplateName").InnerText
                
                If STRMailTemplateName = "" Then
                    Throw New Exception("Enter Template Name!")
                End If
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_EMAIL_TEMPLATES"
                    .Connection = objSqlConnection
                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters.Add("@MailTemplateName", SqlDbType.VarChar, 45)
                    .Parameters("@ACTION").Value = "V"
                    .Parameters("@MailTemplateName").Value = STRMailTemplateName
                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    With objOutputXml.DocumentElement.SelectSingleNode("TEMPLATES")
                        .Attributes("MailTemplateName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MailTemplateName")) & "")
                        .Attributes("MailTemplate").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MailTemplate")) & "")
                    End With
                    blnRecordFound = True
                Loop
                If blnRecordFound = False Then
                    bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                End If
            Catch Exec As AAMSException
                'CATCHING AAMS EXCEPTIONS
                bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bzShared.FillErrorStatus(objOutputXml, "103", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objOutputXml
        End Function

    End Class
End Namespace
