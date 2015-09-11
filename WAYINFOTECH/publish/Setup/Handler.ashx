<%@ WebHandler Language="VB" Class="Handler" %>

Imports System
Imports System.Web

Public Class Handler : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim bzImageControl As New WAY.bizMaster.bzPermission
        
        Dim ds As New DataSet
        objInputXml.LoadXml("<TA_GETCONTROLIMAGE_INPUT><SecurityOptionID></SecurityOptionID></TA_GETCONTROLIMAGE_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("SecurityOptionID").InnerText = context.Request.QueryString("ID")
        ds = bzImageControl.GetControlsImagesforPermission(objInputXml)
        
        If ds.Tables(0).Rows.Count <> 0 Then
            If ds.Tables(0).Rows(0)("Image").ToString <> "" Then
                context.Response.ContentType = "image/jpg"
                context.Response.BinaryWrite((DirectCast(ds.Tables(0).Rows(0)("Image"), Byte())))
            End If
        End If
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class