Imports System.IO
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Data.SqlClient
Imports System.String
Imports System.IO.Path
Imports System.Drawing.Printing
Partial Class TravelAgency_TASR_ViewOrderDoc
    Inherits System.Web.UI.Page
    Public Shared imgWidth As Integer = 750
    Public Shared imgHeight As Integer = 950
    Public Shared imagZoomInOutClick As Integer = 0

    Protected Sub grdViewDocument_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdViewDocument.RowDataBound
        Try
            'If hdContentType.Value.Trim() = "1" Or hdContentType.Value.Trim() = "4" Then
            '    Dim img As System.Web.UI.WebControls.Image
            '    img = CType(grdViewDocument.Rows(0).Cells(0).FindControl("imgDocument"), System.Web.UI.WebControls.Image)
            '    img.Width = imgWidth + imagZoomInOutClick * 10
            '    img.Height = imgHeight + imagZoomInOutClick * 10
            'End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
       
    End Sub
    Private Function CreateImageToBinary(ByVal ID As String) As String
        Try
            Dim objtaOrderDoc As New AAMS.bizTravelAgency.bzOrder
            Dim xInupDoc, xOutputDoc As New XmlDocument
            Dim strPath As String = ""
            Dim dSet As New DataSet
            Dim strgImagePath As String = "~/DOC/" ' ConfigurationManager.AppSettings("gImagePath").ToString()
            'strPath = System.AppDomain.CurrentDomain.BaseDirectory.Trim() + "\DOC\" & ID.ToString() & ".gif"
            xInupDoc.LoadXml("<TA_GETSCANNEDIMAGE_INPUT><ID>" & ID & "</ID></TA_GETSCANNEDIMAGE_INPUT>")
            dSet = objtaOrderDoc.GetScannedImage(xInupDoc)
            If dSet IsNot Nothing Then

                If dSet.Tables(0).Rows.Count > 0 Then
                    Dim bytImageData() As Byte = _
                                    dSet.Tables(0).Rows(0)(1)
                    Dim stmImageData As New MemoryStream(bytImageData)


                    Dim img1 As Image = Image.FromStream(stmImageData)
                    '****************************Delete Existing Image************
                    Dim strFileName As String = ID.ToString() & ".gif"
                    If System.IO.File.Exists(strgImagePath & "/" & strFileName) Then
                        System.IO.File.Delete(strgImagePath & "/" & strFileName)
                    End If
                    '*************************************************************
                    strPath = Server.MapPath("~/DOC/") & ID.ToString() & ".gif"
                    img1.Save(strPath, ImageFormat.Gif)
                    Return strgImagePath & ID.ToString() & ".gif"
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim objeAAMSSession As New eAAMS
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl

            '  btnZoomIn.Attributes.Add("onclick", "return GetWidth()")
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMSSession.CheckSession())
                Exit Sub
            End If


            Dim strBuilder As New StringBuilder
            Dim objeAAMS As New eAAMS
            Dim objtaOrderDoc As New AAMS.bizTravelAgency.bzOrder
            Dim binStr As String = ""
            btnPrint.Enabled = True
            btnSaveAs.Enabled = True
            Dim objxmlReader As XmlNodeReader
            Dim dSet As New DataSet
            Dim objSecurityXml As New XmlDocument
            Dim xInupDoc, xOutputDoc As New XmlDocument

            If Request.QueryString("AgencyName") IsNot Nothing Then
                txtAgencyName.Text = (Request.QueryString("AgencyName").Trim())
            End If


            If Request.QueryString("TYPE") IsNot Nothing Then
                If Request.QueryString("TYPE").ToString().Trim() = "M" Then
                    spHeader.InnerText = "View Misc. Document"
                    'If (Request.QueryString("FileNo") Is Nothing) Then
                    '    lblError.Text = "Records Not Found"
                    '    Exit Sub
                    'ElseIf (Request.QueryString("FileNo") = "") Then
                    '    lblError.Text = "Records Not Found"
                    '    Exit Sub
                    'End If
                End If
            End If

            btnPrint.Attributes.Add("onclick", "return PrintImage();")

            If (Not IsPostBack) Then

                clearCache()
                grdViewDocument.DataSource = Nothing
                grdViewDocument.DataBind()

                ViewState("ViewDoc") = Nothing

                xInupDoc.LoadXml("<TA_GETSCANNEDDOCUMENT_INPUT><LCode></LCode>  <FileNo></FileNo><Order_No></Order_No><ORDERID></ORDERID></TA_GETSCANNEDDOCUMENT_INPUT>")


                If (Request.QueryString("LCode") IsNot Nothing) Then
                    If (Request.QueryString("LCode").Trim().Length <> 0) Then
                        xInupDoc.DocumentElement.SelectSingleNode("LCode").InnerText = Request.QueryString("LCode").Trim()
                    Else
                        lblError.Text = "Location Code cann't be blank."
                        Exit Sub
                    End If
                Else
                    lblError.Text = "Location Code Not Found"
                    Exit Sub
                End If

                If (Request.QueryString("FileNo") IsNot Nothing) Then
                    If (Request.QueryString("OrderNo") IsNot Nothing) Then
                        xInupDoc.DocumentElement.SelectSingleNode("Order_No").InnerText = Request.QueryString("OrderNo").Trim()
                    End If
                    If (Request.QueryString("FileNo") IsNot Nothing) Then
                        If (Request.QueryString("FileNo").Trim().Length <> 0) Then
                            xInupDoc.DocumentElement.SelectSingleNode("FileNo").InnerText = Request.QueryString("FileNo").Trim()
                        Else
                            '  lblError.Text = "File no can't be blank."
                            '  Exit Sub
                            xInupDoc.DocumentElement.SelectSingleNode("FileNo").InnerText = ""
                        End If
                    End If

                    If Request.QueryString("TYPE") IsNot Nothing Then
                        If Request.QueryString("TYPE").ToString().Trim() = "M" Then
                            xInupDoc.LoadXml("<TA_GETSCANNEDDOCUMENT_INPUT><LCode></LCode><FileNo></FileNo><ORDERID></ORDERID></TA_GETSCANNEDDOCUMENT_INPUT>")
                            'xInupDoc.DocumentElement.SelectSingleNode("FileNo").InnerText = Request.QueryString("FileNo").Trim()

                            If (Request.QueryString("LCode").Trim().Length <> 0) Then
                                xInupDoc.DocumentElement.SelectSingleNode("LCode").InnerText = Request.QueryString("LCode").Trim()
                            End If

                            If Request.QueryString("AOrderId") IsNot Nothing Then
                                If xInupDoc.DocumentElement.SelectSingleNode("ORDERID") IsNot Nothing Then
                                    xInupDoc.DocumentElement.SelectSingleNode("ORDERID").InnerText = Request.QueryString("AOrderId").ToString
                                End If
                            End If


                            xOutputDoc = objtaOrderDoc.GetMiscDocumentDetailWeb(xInupDoc)
                            ' xOutputDoc.LoadXml(" <TA_GETSCANNEDDOCUMENT_OUTPUT><Document ID='36111' FileNo='2109' Order_No='' Status='' FileOrder='1' DocType='' Order_Type='' ContentType='3' EmailFrom='' EmailTo='' EmailSubject='' EmailBody='' PDFDocFileName='1893' />  <Errors Status='FALSE'><Error Code='101' Description='' /> </Errors></TA_GETSCANNEDDOCUMENT_OUTPUT>")
                            If xOutputDoc.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                                ViewState("ViewDoc") = xOutputDoc.OuterXml
                                objxmlReader = New XmlNodeReader(xOutputDoc)
                                dSet.ReadXml(objxmlReader)
                                grdViewDocument.DataSource = dSet.Tables("Document").DefaultView
                                grdViewDocument.DataBind()
                            Else
                                ViewState("ViewDoc") = Nothing
                                lblError.Text = xOutputDoc.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                                'Code on 3rd March 2009
                                btnZoomIn.Enabled = False
                                btnZoomOut.Enabled = False
                                btnFitToPage.Enabled = False
                                btnSaveAs.Enabled = False
                                btnPrint.Enabled = False
                                'Code End on 3rd March 2009
                                Exit Sub
                                objxmlReader = New XmlNodeReader(xOutputDoc)
                                dSet.ReadXml(objxmlReader)
                                grdViewDocument.DataSource = dSet.Tables("Document").DefaultView
                                grdViewDocument.DataBind()


                               

                            End If
                        End If
                    Else
                        ' objtaOrderDoc.GetScannedDocumentWeb()
                        If Request.QueryString("AOrderId") IsNot Nothing Then
                            If xInupDoc.DocumentElement.SelectSingleNode("ORDERID") IsNot Nothing Then
                                xInupDoc.DocumentElement.SelectSingleNode("ORDERID").InnerText = Request.QueryString("AOrderId").ToString
                            End If
                        End If


                        xOutputDoc = objtaOrderDoc.GetScannedDocumentWeb(xInupDoc)
                        If xOutputDoc.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            ViewState("ViewDoc") = xOutputDoc.OuterXml
                            objxmlReader = New XmlNodeReader(xOutputDoc)
                            dSet.ReadXml(objxmlReader)
                            grdViewDocument.DataSource = dSet.Tables("Document").DefaultView
                            grdViewDocument.DataBind()
                        Else
                            ViewState("ViewDoc") = Nothing
                            lblError.Text = xOutputDoc.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

                            'Code on 3rd March 2009
                            btnZoomIn.Enabled = False
                            btnZoomOut.Enabled = False
                            btnFitToPage.Enabled = False
                            btnSaveAs.Enabled = False
                            btnPrint.Enabled = False
                            'Code End on 3rd March 2009

                            Exit Sub
                            objxmlReader = New XmlNodeReader(xOutputDoc)
                            dSet.ReadXml(objxmlReader)
                            grdViewDocument.DataSource = dSet.Tables("Document").DefaultView
                            grdViewDocument.DataBind()

                           
                        End If
                    End If
                Else
                    lblError.Text = "Invalid Parameter"
                    Exit Sub
                End If

            End If

            If Request.QueryString("TYPE") IsNot Nothing Then
                If Request.QueryString("TYPE").ToString().Trim() = "M" Then
                    spHeader.InnerText = "View Misc. Document"

                    objSecurityXml.LoadXml(Session("Security"))
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                        If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='View Misc Documents']").Count <> 0 Then
                            strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='View Misc Documents']").Attributes("Value").Value)
                        End If
                        If Not strBuilder Is Nothing Then
                            If strBuilder(4) = "0" Then
                                btnPrint.Enabled = False
                                btnSaveAs.Enabled = False
                            End If
                            If strBuilder(0) = "0" Then
                                Response.Redirect("~/NoRights.aspx")
                            End If

                        End If
                    Else
                        strBuilder = objeAAMS.SecurityCheck(31)
                    End If

                End If
            Else
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='View Order Documents']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='View Order Documents']").Attributes("Value").Value)
                    End If
                    If Not strBuilder Is Nothing Then
                        If strBuilder(4) = "0" Then
                            btnPrint.Enabled = False
                            btnSaveAs.Enabled = False
                        End If

                        If strBuilder(0) = "0" Then
                          Response.Redirect("~/NoRights.aspx")
                        End If

                    End If
                Else
                    strBuilder = objeAAMS.SecurityCheck(31)
                End If
            End If


            'Code Added on working Zoom In-Out

            If hdContentType.Value.Trim() = "1" Or hdContentType.Value.Trim() = "4" Then
                btnZoomIn.Enabled = True
                btnFitToPage.Enabled = True
                btnZoomOut.Enabled = True
            Else
                btnZoomIn.Enabled = False
                btnFitToPage.Enabled = False
                btnZoomOut.Enabled = False
            End If

            'If hdContentType.Value.Trim() = "2" Then
            '    btnSaveAs.Enabled = False
            'Else
            '    btnSaveAs.Enabled = True
            'End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub cmdSaveAs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveAs.Click
        Try
            If Session("ImagePath") IsNot Nothing Then
                Dim filePath As String = Session("ImagePath") ' Server.MapPath(specify the file path on server here) 
                ' Dim imgpath As System.IO.Path
                Dim fileName As String = System.IO.Path.GetFileName(filePath)

                Dim strPhysicalPath As String = Server.MapPath("../DOC/") + fileName

                Dim targetFile As System.IO.FileInfo = New System.IO.FileInfo(strPhysicalPath)

                If File.Exists(Server.MapPath("../DOC/") + fileName) Then
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName)
                    Response.AddHeader("Content-Length", targetFile.Length.ToString())
                    Response.ContentType = "image/gif/jpg"
                    Response.WriteFile(strPhysicalPath)
                End If
            End If


            If Session("ImagePath") IsNot Nothing Then
                If hdContentType.Value.Trim() = "2" Then
                    Dim StrChk As String = Session("ImagePath")
                    SetEmailValues(StrChk.Split("=").GetValue(1))
                End If
            End If


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Sub SetEmailValues(ByVal strID As String)
        Dim xInputDoc, xOutPutDoc As New XmlDocument
        Dim bzEmailDetails As New AAMS.bizTravelAgency.bzOrder
        xInputDoc.LoadXml("<UP_GETORDEREMAILDETAILS_INPUT><ID></ID></UP_GETORDEREMAILDETAILS_INPUT>")
        xInputDoc.DocumentElement.SelectSingleNode("ID").InnerText = strID
        xOutPutDoc = bzEmailDetails.GetEmailDetails(xInputDoc)
        Dim objExport As New ExportExcel
        ' hdMailTo.Value = xOutPutDoc.DocumentElement.SelectSingleNode("MailDetail").Attributes("EmailTo").Value
        'txtMailFrom.Text = xOutPutDoc.DocumentElement.SelectSingleNode("MailDetail").Attributes("EmailFrom").Value
        ' hdMailSubject.Value = xOutPutDoc.DocumentElement.SelectSingleNode("MailDetail").Attributes("EmailSubject").Value
        'txtMailBody.Text = xOutPutDoc.DocumentElement.SelectSingleNode("MailDetail").Attributes("EmailBody").Value
        ' hdMailBody.Value = xOutPutDoc.DocumentElement.SelectSingleNode("MailDetail").Attributes("EmailBody").Value

        Dim objNewXml As New XmlDocument
        objNewXml.LoadXml("<UP_GETORDEREMAILDETAILS_OUTPUT><OrderMail Mail=''/></UP_GETORDEREMAILDETAILS_OUTPUT>")
        objNewXml.DocumentElement.SelectSingleNode("OrderMail").Attributes("Mail").Value = "<CENTER> <TABLE style='BORDER-RIGHT: #000 1px solid; BORDER-TOP: #000 1px solid; BORDER-LEFT: #000 1px solid; BORDER-BOTTOM: #000 1px solid' cellSpacing=0 cellPadding=0 width=551px> <TBODY><TR> <TD align=left style='width:551px;word-wrap:break-word'> <TABLE cellSpacing=0 cellPadding=0 width='100%' border=0>"
        objNewXml.DocumentElement.SelectSingleNode("OrderMail").Attributes("Mail").Value &= "<b>Mail To: </b>"
        objNewXml.DocumentElement.SelectSingleNode("OrderMail").Attributes("Mail").Value &= xOutPutDoc.DocumentElement.SelectSingleNode("MailDetail").Attributes("EmailTo").Value.Replace(",", ", ")
        objNewXml.DocumentElement.SelectSingleNode("OrderMail").Attributes("Mail").Value &= "</TD></TR>"
        objNewXml.DocumentElement.SelectSingleNode("OrderMail").Attributes("Mail").Value &= "<TR><TD>"
        objNewXml.DocumentElement.SelectSingleNode("OrderMail").Attributes("Mail").Value &= "<b>Subject: </b>"
        objNewXml.DocumentElement.SelectSingleNode("OrderMail").Attributes("Mail").Value &= xOutPutDoc.DocumentElement.SelectSingleNode("MailDetail").Attributes("EmailSubject").Value
        objNewXml.DocumentElement.SelectSingleNode("OrderMail").Attributes("Mail").Value &= "</TD></TR>  </TBODY></TABLE></CENTER><TABLE align='centre'><TR><TD>"
        objNewXml.DocumentElement.SelectSingleNode("OrderMail").Attributes("Mail").Value &= xOutPutDoc.DocumentElement.SelectSingleNode("MailDetail").Attributes("EmailBody").Value
        objNewXml.DocumentElement.SelectSingleNode("OrderMail").Attributes("Mail").Value &= "</TD></TR></TABLE>"


        ' objExport.ExportDetails(objNewXml, "OrderMail", ExportExcel.ExportFormat.Excel, "mailTest.doc")


        Response.Clear()


        Dim strBytArr As New System.Text.ASCIIEncoding
        Dim byteArr As New Byte()
        Dim intArrLength As Int32 = strBytArr.GetByteCount(objNewXml.DocumentElement.SelectSingleNode("OrderMail").Attributes("Mail").Value.Trim())
        Dim bye(intArrLength) As Byte
        bye = strBytArr.GetBytes(objNewXml.DocumentElement.SelectSingleNode("OrderMail").Attributes("Mail").Value)
        Response.Clear()
        Response.ClearContent()
        Response.BufferOutput = True


        Response.AddHeader("Content-Disposition", "attachment; filename=OrderMail.html")
        Response.AddHeader("Content-Length", intArrLength.ToString())
        Response.ContentType = "application/octet-stream" '// "application/ms-word"

        Dim mStream As New MemoryStream(bye)
        mStream.WriteTo(Response.OutputStream)
        Response.Flush()
        'Response.ClearContent()
        ' Response.Clear()
        Response.Close()
        ' Response.Write(objNewXml.DocumentElement.SelectSingleNode("OrderMail").Attributes("Mail").Value)





    End Sub

    Protected Sub grdViewDocument_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdViewDocument.PageIndexChanged


    End Sub
    Protected Function GetImageDoc(ByVal ID As Object) As String
        Try
            lblError.Text = ""
            If (ViewState("ViewDoc") IsNot Nothing) Then
                Dim ds As New DataSet
                Dim xOutputDoc As New XmlDataDocument
                Dim strFileName As String
                Dim name, name1 As String
                name = ""
                name1 = ""
                Dim strimage As String = ""
                xOutputDoc.LoadXml(ViewState("ViewDoc").ToString())
                If xOutputDoc.DocumentElement.SelectNodes("Document[@ID='" & ID & "']").Count > 0 Then
                    With xOutputDoc.DocumentElement.SelectSingleNode("Document[@ID='" & ID & "']")
                        If Request.QueryString("AgencyName") IsNot Nothing Then
                            txtAgencyName.Text = (Request.QueryString("AgencyName").Trim())
                        End If
                        txtOrderName.Text = xOutputDoc.DocumentElement.SelectSingleNode("Document[@ID='" & ID & "']").Attributes("Order_No").Value 'xOutputDoc.DocumentElement.SelectSingleNode("Document").Attributes("Order_No").Value
                        txtOrderType.Text = xOutputDoc.DocumentElement.SelectSingleNode("Document[@ID='" & ID & "']").Attributes("Order_Type").Value ' xOutputDoc.DocumentElement.SelectSingleNode("Document").Attributes("Order_Type").Value
                        txtSqenceNumber.Text = xOutputDoc.DocumentElement.SelectSingleNode("Document[@ID='" & ID & "']").Attributes("FileOrder").Value() '= xOutputDoc.DocumentElement.SelectSingleNode("Document").Attributes("ID").Value
                        txtFileNo.Text = xOutputDoc.DocumentElement.SelectSingleNode("Document[@ID='" & ID & "']").Attributes("FileNo").Value 'xOutputDoc.DocumentElement.SelectSingleNode("Document").Attributes("FileNo").Value
                        hdContentType.Value = xOutputDoc.DocumentElement.SelectSingleNode("Document[@ID='" & ID & "']").Attributes("ContentType").Value
                    End With
                End If

                'strFileName = System.AppDomain.CurrentDomain.BaseDirectory() + "DOC"
                strFileName = Server.MapPath("~/DOC")
                'Dim str1 As String = Directory.GetLastAccessTime(strFileName).ToShortDateString()
                'Dim str2 As String = Today.ToShortDateString()
                'Dim str3() As String = System.IO.Directory.GetFiles(strFileName, "*.gif")

                '**************************************************



                '**************************************************

                If xOutputDoc.DocumentElement.SelectNodes("Document[@ID='" & ID & "']").Count > 0 Then
                    Dim strContentType As String = xOutputDoc.DocumentElement.SelectSingleNode("Document[@ID='" & ID & "']").Attributes("ContentType").Value.Trim()

                    If strContentType = "1" Or strContentType = "4" Then
                        'Do the work of Image
                        ' grdViewDocument.FindControl("pnlImage").Visible = True


                        If strContentType = "4" Then

                            Dim strFilePath As String = System.Configuration.ConfigurationManager.AppSettings("PDFDOC").Trim()

                            Dim strPdfName As String = xOutputDoc.DocumentElement.SelectSingleNode("Document[@ID='" & ID & "']").Attributes("PDFDocFileName").Value.Trim()

                            'strFileName = Server.MapPath("~/DOC")


                            If Directory.GetLastAccessTime(Server.MapPath("~/DOC")).ToShortDateString() <> Today.ToShortDateString() Then

                                For Each strFile As String In System.IO.Directory.GetFiles(Server.MapPath("../DOC"), "*.jpg")
                                    System.IO.File.Delete(strFile)
                                Next
                                '*********************************
                            End If




                            If System.IO.File.Exists(strFilePath & "\" & strPdfName & ".jpg") Then
                                Dim strSourceFile As String = strFilePath & "\" & strPdfName & ".jpg"
                                Dim strDestinationFile As String = Server.MapPath("~/DOC/" & strPdfName & ".jpg")

                                If File.Exists(strDestinationFile) Then
                                    File.Delete(strDestinationFile)
                                End If
                                File.Copy(strSourceFile, strDestinationFile)

                                strimage = "../DOC/" & strPdfName & ".jpg"
                                Session("ImagePath") = strimage
                                'btnPrint.Enabled = False
                                'btnSaveAs.Enabled = False
                                Return strimage

                            Else
                                lblError.Text = "File Doesn't Exist"
                            End If

                        End If

                        If strContentType = "1" Then



                            If Directory.GetLastAccessTime(strFileName).ToShortDateString() <> Today.ToShortDateString() Then

                                For Each strFile As String In System.IO.Directory.GetFiles(strFileName, "*.gif")
                                    System.IO.File.Delete(strFile)
                                Next
                                '*********************************
                            End If


                            If System.IO.File.Exists(strFileName & "\" & ID.ToString() & ".gif") Then
                                If (System.IO.File.GetCreationTime(strFileName & "\" & ID.ToString() & ".gif").ToShortDateString() = Today.ToShortDateString()) Then
                                    name = System.IO.File.GetLastAccessTime(strFileName & "\" & ID.ToString() & ".gif").ToShortDateString()
                                    Dim strgImagePath As String = "~/DOC/" 'ConfigurationManager.AppSettings("gImagePath").ToString()
                                    strimage = strgImagePath & ID.ToString() & ".gif" 'strFileName & "\" & ID.ToString() & ".gif"

                                Else
                                    strimage = CreateImageToBinary(ID)
                                End If
                            Else
                                strimage = CreateImageToBinary(ID)
                            End If
                            'End of Image Work
                            Session("ImagePath") = strFileName & "\" & ID.ToString() & ".gif"
                            Return strimage
                        End If

                    End If


                    If xOutputDoc.DocumentElement.SelectSingleNode("Document[@ID='" & ID & "']").Attributes("ContentType").Value.Trim() = "2" Then
                        'Do the work of Email
                        Dim strID As String = ID.ToString()
                        Session("ImagePath") = "ID=" & strID
                    End If

                    If xOutputDoc.DocumentElement.SelectSingleNode("Document[@ID='" & ID & "']").Attributes("ContentType").Value.Trim() = "3" Then
                        'Do the work of PDF Document
                        Dim strFilePath As String = System.Configuration.ConfigurationManager.AppSettings("PDFDOC").Trim()




                        Dim strPdfName As String = xOutputDoc.DocumentElement.SelectSingleNode("Document[@ID='" & ID & "']").Attributes("PDFDocFileName").Value.Trim()

                        'strFileName = Server.MapPath("~/PDFDOC")

                        If System.IO.File.Exists(strFilePath & "\" & strPdfName & ".pdf") Then

                            Dim strSourceFile As String = strFilePath & "\" & strPdfName & ".pdf"

                            Dim strDestinationFile As String = Server.MapPath("~/DOC/" & strPdfName & ".pdf")


                            'Code Added for Delete of All Temprory PDF Files
                            If Directory.GetLastAccessTime(Server.MapPath("~/DOC")).ToShortDateString() <> Today.ToShortDateString() Then

                                For Each strFile As String In System.IO.Directory.GetFiles(Server.MapPath("~/DOC"), "*.pdf")
                                    System.IO.File.Delete(strFile)
                                Next
                                '*********************************
                            End If


                            If File.Exists(strDestinationFile) Then
                                File.Delete(strDestinationFile)
                            End If

                            File.Copy(strSourceFile, strDestinationFile)


                            strimage = "../DOC/" & strPdfName & ".pdf"

                            Session("ImagePath") = Nothing
                            btnPrint.Enabled = False
                            btnSaveAs.Enabled = False
                            Return strimage
                        Else
                            lblError.Text = "File Doesn't Exist"
                        End If
                    Else
                        lblError.Text = xOutputDoc.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If



                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Function

    Protected Sub grdViewDocument_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdViewDocument.PageIndexChanging
        Try
            imgHeight = 950
            imgWidth = 750

            If (ViewState("ViewDoc") IsNot Nothing) Then
                grdViewDocument.PageIndex = e.NewPageIndex
                ViewState("PageIndex") = grdViewDocument.PageIndex
                Dim objxmlReader As XmlNodeReader
                Dim xOutputDoc As New XmlDataDocument
                Dim ds As New DataSet
                xOutputDoc.LoadXml(ViewState("ViewDoc").ToString())
                objxmlReader = New XmlNodeReader(xOutputDoc)
                ds.ReadXml(objxmlReader)
                grdViewDocument.DataSource = ds.Tables("Document").DefaultView
                grdViewDocument.DataBind()

                'Code Added on working Zoom In-Out

                If hdContentType.Value.Trim() = "1" Or hdContentType.Value.Trim() = "4" Then
                    btnZoomIn.Enabled = True
                    btnFitToPage.Enabled = True
                    btnZoomOut.Enabled = True
                Else
                    btnZoomIn.Enabled = False
                    btnFitToPage.Enabled = False
                    btnZoomOut.Enabled = False
                End If

                'If hdContentType.Value.Trim() = "2" Then
                '    btnSaveAs.Enabled = False
                'Else
                '    btnSaveAs.Enabled = True
                'End If

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub cmdViewAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnViewAll.Click
        Dim StrOrderId As String = ""

        Try
            ' ViewState("ViewAllChk") = "-111"
            Session("ImagePath") = Nothing
            grdViewDocument.DataSource = Nothing
            grdViewDocument.DataBind()
            Response.Clear()
            'Response.ContentType = "Text/html"
            Response.ContentType = String.Empty
            Response.ClearContent()
            Response.ClearHeaders()

            imgHeight = 950
            imgWidth = 750


            If Request.QueryString("AOrderId") IsNot Nothing Then
                StrOrderId = Request.QueryString("AOrderId").ToString
            End If




            If Request.QueryString("TYPE") IsNot Nothing Then
                If Request.QueryString("TYPE").ToString().Trim() = "M" Then
                    Response.Redirect("~/TravelAgency/TASR_ViewOrderDoc.aspx?LCode=" & Request.QueryString("LCode") & "&FileNo=" & Request.QueryString("FileNo") & "&AgencyName=" & txtAgencyName.Text.Trim() & "&TYPE=M" & "&AOrderId=" & StrOrderId, False)
                End If
            Else
                Response.Redirect("~/TravelAgency/TASR_ViewOrderDoc.aspx?LCode=" & Request.QueryString("LCode") & "&FileNo=" & Request.QueryString("FileNo") & "&AgencyName=" & txtAgencyName.Text.Trim().Trim & "&AOrderId=" & StrOrderId, False)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub clearCache()
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        HttpContext.Current.Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        HttpContext.Current.Response.Cache.SetLastModified(DateTime.Now)
        HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(False)
        imagZoomInOutClick = 0
        imgWidth = 750
        imgHeight = 950
    End Sub

    Protected Sub btnZoomIn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnZoomIn.Click
        Try
            If hdContentType.Value.Trim() = "1" Or hdContentType.Value.Trim() = "4" Then
                imagZoomInOutClick += 1
                imgWidth = 750 + imagZoomInOutClick * 100
                imgHeight = 950 + imagZoomInOutClick * 100
                bindimage()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnZoomOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnZoomOut.Click
        Try
            If hdContentType.Value.Trim() = "1" Or hdContentType.Value.Trim() = "4" Then
                imagZoomInOutClick -= 1
                imgWidth = 750 + imagZoomInOutClick * 100
                imgHeight = 950 + imagZoomInOutClick * 100

                If imgWidth <= 0 Or imgHeight <= 0 Then
                    imagZoomInOutClick += 1
                    imgWidth = 750 + imagZoomInOutClick * 100
                    imgHeight = 950 + imagZoomInOutClick * 100
                End If

                bindimage()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnFitToPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFitToPage.Click
        Try
            If hdContentType.Value.Trim() = "1" Or hdContentType.Value.Trim() = "4" Then
                imgWidth = 750
                imgHeight = 950
                imagZoomInOutClick = 0
                bindimage()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub bindimage()
        If (ViewState("ViewDoc") IsNot Nothing) Then
            If ViewState("PageIndex") IsNot Nothing Then
                grdViewDocument.PageIndex = Convert.ToInt32(ViewState("PageIndex"))
            Else
                grdViewDocument.PageIndex = 0
            End If
            Dim objxmlReader As XmlNodeReader
            Dim xOutputDoc As New XmlDataDocument
            Dim ds As New DataSet
            xOutputDoc.LoadXml(ViewState("ViewDoc").ToString())
            objxmlReader = New XmlNodeReader(xOutputDoc)
            ds.ReadXml(objxmlReader)
            grdViewDocument.DataSource = ds.Tables("Document").DefaultView
            grdViewDocument.DataBind()
        End If
    End Sub
End Class
