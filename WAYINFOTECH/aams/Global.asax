<%@ Application Language="VB" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
        Dim inputXML As New XmlDocument
        Dim inputXMLReader As XmlReader
        Dim Gbldataset As New DataSet
        ' Dim dRowLoginLog As DataRow
        
        inputXML.LoadXml("<LOGIN LoginName='' ActiveFlag='' LoginIp='' ></LOGIN>")
        inputXMLReader = New XmlNodeReader(inputXML)
        Gbldataset.ReadXml(inputXMLReader)
        
        Gbldataset.Tables(0).TableName = "UserLog"
        
        Dim PrimaryKeys(1) As DataColumn
        Dim PK As DataColumn = Gbldataset.Tables("UserLog").Columns("LoginName")
        PrimaryKeys(0) = PK
        Dim PK1 As DataColumn = Gbldataset.Tables("UserLog").Columns("LoginIp")
        PrimaryKeys(1) = PK1
        Gbldataset.Tables("UserLog").PrimaryKey = PrimaryKeys
        
        Gbldataset.Tables("UserLog").Rows.Clear()
        
        'dRowLoginLog = Gbldataset.Tables("UserLog").NewRow
        'dRowLoginLog("LoginName") = "Neeraj"
        'dRowLoginLog("ActiveFlag") = 1
        'dRowLoginLog("LoginIp") = "127.0.0.0"
        'Gbldataset.Tables("UserLog").Rows.Add(dRowLoginLog)
        
        Application("LoginLog") = Gbldataset
        
    End Sub
    
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
       
        
    End Sub
     Sub Application_PreRequestHandlerExecute(ByVal sender As Object, ByVal e As EventArgs)
        
        'If Response.ContentType = "text/html" Then
        '    Response.Filter = New TrimStream(Response.Filter)
        'End If
    End Sub
    Protected Sub Application_BeginRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        'If Request.Url.PathAndQuery.ToLower.IndexOf("makethumbnail") = -1 Then
        'Response.Filter = New TrimStream(Response.Filter)
        'End If
        'If Response.ContentType = "text/html" Then
        '    Response.Filter = New TrimStream(Response.Filter)
        'End If
    End Sub
       
</script>