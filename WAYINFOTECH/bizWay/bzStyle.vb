
Imports System.Xml
Imports System.Data.SqlClient
Namespace bizMaster
    Public Class bzStyle
        Const strClass_NAME = "bzMaster"
        Const srtVIEW_OUTPUT = "<MS_VIEWSTYLE_OUTPUT><STYLE W_StyleId='' BarcodeNo='' StyleName='' DesignNo='' ShadeNo='' MRP='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_VIEWSTYLE_OUTPUT>"
        Const strSEARCH_OUTPUT = "<MS_SEARCHSTYLE_OUTPUT><STYLE W_StyleId='' BarcodeNo='' StyleName='' DesignNo='' ShadeNo='' MRP='' /><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCHSTYLE_OUTPUT>"
        Const strDELETE_OUTPUT = "<MS_DELETESTYLE_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></MS_DELETESTYLE_OUTPUT>"
        Const strUPDATE_OUTPUT = "<MS_UPDATESTYLE_OUTPUT><STYLE Action='' W_StyleId='' BarcodeNo='' StyleName='' DesignNo='' ShadeNo='' MRP='' /><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATESTYLE_OUTPUT>"
        Const strUPDATE_STYLE_ORDER_OUTPUT = "<MS_UPDATESTYLE_ORDER_OUTPUT><STYLE Action='' W_StyleOrderID='' W_StyleId='' BarcodeNo='' StyleName='' DesignNo='' ShadeNo='' MRP='' Qty=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_UPDATESTYLE_ORDER_OUTPUT>"
        Const srtLIST_OUTPUT = "<UP_LIST_STYLE_OUTPUT><STYLE ID='' VALUE='' /><Errors Status=''><Error Code='' Description='' /></Errors></UP_LIST_STYLE_OUTPUT>"
        Const strOrderView_OUTPUT = "<INV_VIEW_W_STYLEORDER_OUTPUT><ORDERHEADER W_StyleOrderID='' Totqty='' Remarks='' OrderDate='' LoggedBy='' LoggedByName='' OrderNumber=''><ORDERDETAILS W_StyleOrderID='' qty='' W_StyleId='' StyleName='' DesignNo='' ShadeNo='' MRP='' /></ORDERHEADER><Errors Status=''><Error Code='' Description='' /></Errors></INV_VIEW_W_STYLEORDER_OUTPUT>"
        Const strUPDATEORDERBOOKING_OUTPUT = "<INV_UPDATE_W_ORDER_OUTPUT><ORDERHEADER W_StyleOrderID='' Totqty='' Remarks='' OrderDate='' LoggedBy='' LoggedByName='' OrderNumber=''><ORDERDETAILS W_StyleOrderID='' qty='' W_StyleId='' StyleName='' DesignNo='' ShadeNo='' MRP='' /></ORDERHEADER><Errors Status=''><Error Code='' Description='' /></Errors></INV_UPDATE_W_ORDER_OUTPUT>"
        Const strSearchStyleOrder_OUTPUT = "<MS_SEARCH_QUALITY_ORDER_OUTPUT><STYLE W_STYLEID='' W_StyleOrderID='' ORDERNUMBER='' ORDERDATE='' LOGGEDBY='' LOGGERNAME='' QUALITY='' DESIGN='' SHADENO='' MRP='' QTY=''/><PAGE PAGE_COUNT='' TOTAL_ROWS=''/><Errors Status=''><Error Code='' Description='' /></Errors></MS_SEARCH_QUALITY_ORDER_OUTPUT>"
        Const strOrderDELETE_OUTPUT = "<INV_DELETE_W_STYLEORDER_OUTPUT><Errors Status=''><Error Code='' Description='' /></Errors></INV_DELETE_W_STYLEORDER_OUTPUT>"


        Public Function Add() As System.Xml.XmlDocument

        End Function

        Public Function Delete(ByVal DeleteDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function deletes a AirlineOffice.
            'Input:XmlDocument
            '<MS_DELETESTYLE_INPUT>
            '      <W_StyleId></W_StyleId>
            '</MS_DELETESTYLE_INPUT>
            'Output :
            '<MS_DELETESTYLE_OUTPUT>
            '    <Errors Status=''>
            '        <Error Code='' Description='' />
            '    </Errors>
            '</MS_DELETESTYLE_OUTPUT>"            
            '************************************************

            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Delete"
            Dim strW_StyleId As String
            Dim objDeleteDocOutput As New XmlDocument

            objDeleteDocOutput.LoadXml(strDELETE_OUTPUT)

            Try
                intRecordsAffected = 0
                strW_StyleId = DeleteDoc.DocumentElement.SelectSingleNode("W_StyleId").InnerText.Trim
                If strW_StyleId = "" Then
                    Throw (New bizShared.AAMSException("Incomplete Parameters"))
                End If
                'Deleting the specific record
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_SRO_MS_STYLE]"
                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = "D"

                    .Parameters.Add(New SqlParameter("@W_StyleId", SqlDbType.Int))
                    .Parameters("@W_StyleId").Value = strW_StyleId

                    objSqlCommand.Connection.Open()
                    intRecordsAffected = .ExecuteNonQuery()
                    objSqlCommand.Connection.Close()
                End With

                'Checking whether record is deleted successfully or not
                If intRecordsAffected > 0 Then
                    objDeleteDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    Return (objDeleteDocOutput)
                Else
                    objDeleteDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "TRUE"
                    Call bizShared.bzShared.FillErrorStatus(objDeleteDocOutput, "101", "Record has not been deleted!")
                    Return (objDeleteDocOutput)
                End If
            Catch Exec As bizShared.AAMSException
                'CATCHING AAMS EXCEPTIONS
                bizShared.bzShared.FillErrorStatus(objDeleteDocOutput, "101", Exec.Message)
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                'Fill Error Status
                bizShared.bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bizShared.bzShared.FillErrorStatus(objDeleteDocOutput, "101", Exec.Message)
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objDeleteDocOutput

        End Function

        Public Function Search(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function gives search results based on choosen search criterion.
            'Input  :- 
            '<MS_SEARCHSTYLE_INPUT>
            '	<BarcodeNo />
            '	<StyleName />
            '	<DesignNo />
            '	<ShadeNo />
            '	<MRP />
            '	<PAGE_NO />
            '	<PAGE_SIZE />
            '	<SORT_BY />
            '	<DESC />
            '</MS_SEARCHSTYLE_INPUT>

            'Output :
            '<MS_SEARCHSTYLE_OUTPUT>
            '	<STYLE W_StyleId='' BarcodeNo='' StyleName='' DesignNo='' ShadeNo='' MRP='' />
            '   <PAGE PAGE_COUNT='' TOTAL_ROWS=''/> 
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_SEARCHSTYLE_OUTPUT>
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean

            Dim strBarcodeNo As String = String.Empty
            Dim strStyleName As String = String.Empty
            Dim strDesignNo As String = String.Empty
            Dim strShadeNo As String = String.Empty
            Dim strMRP As String = String.Empty

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSEARCH_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strBarcodeNo = SearchDoc.DocumentElement.SelectSingleNode("BarcodeNo").InnerText.Trim()
                strStyleName = SearchDoc.DocumentElement.SelectSingleNode("StyleName").InnerText.Trim()
                strDesignNo = SearchDoc.DocumentElement.SelectSingleNode("DesignNo").InnerText.Trim()
                strShadeNo = SearchDoc.DocumentElement.SelectSingleNode("ShadeNo").InnerText.Trim()
                strMRP = SearchDoc.DocumentElement.SelectSingleNode("MRP").InnerText.Trim()

                'Paging Section    
                If SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim() <> "" Then
                    intPageNo = SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim()
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim() <> "" Then
                    intPageSize = SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim() <> "" Then
                    strSortBy = SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("DESC").InnerText.Trim().ToUpper = "TRUE" Then
                    blnDesc = True
                Else
                    blnDesc = False
                End If

                '--------Retrieving & Checking Details from Input XMLDocument ------End
                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_SER_MS_STYLE]"
                    .Connection = objSqlConnection

                    .Parameters.Add("@BarcodeNo", SqlDbType.VarChar, 200)
                    .Parameters("@BarcodeNo").Value = strBarcodeNo

                    .Parameters.Add("@StyleName", SqlDbType.VarChar, 50)
                    .Parameters("@StyleName").Value = strStyleName

                    .Parameters.Add("@DesignNo", SqlDbType.VarChar, 50)
                    .Parameters("@DesignNo").Value = strDesignNo

                    .Parameters.Add("@ShadeNo", SqlDbType.VarChar, 25)
                    .Parameters("@ShadeNo").Value = strShadeNo

                    .Parameters.Add("@MRP", SqlDbType.Int)
                    If strMRP = "" Then
                        .Parameters("@MRP").Value = DBNull.Value
                    Else
                        .Parameters("@MRP").Value = Val(strMRP & "")
                    End If

                    .Parameters.Add("@PAGE_NO", SqlDbType.Int)
                    If intPageNo = 0 Then
                        .Parameters("@PAGE_NO").Value = DBNull.Value
                    Else
                        .Parameters("@PAGE_NO").Value = intPageNo
                    End If

                    .Parameters.Add("@PAGE_SIZE", SqlDbType.Int)
                    If intPageSize = 0 Then
                        .Parameters("@PAGE_SIZE").Value = DBNull.Value
                    Else
                        .Parameters("@PAGE_SIZE").Value = intPageSize
                    End If

                    .Parameters.Add("@SORT_BY", SqlDbType.VarChar, 100)
                    If strSortBy = "" Then
                        .Parameters("@SORT_BY").Value = DBNull.Value
                    Else
                        .Parameters("@SORT_BY").Value = strSortBy
                    End If

                    .Parameters.Add("@DESC", SqlDbType.Bit)
                    If blnDesc = True Then
                        .Parameters("@DESC").Value = 1
                    Else
                        .Parameters("@DESC").Value = 0
                    End If

                    .Parameters.Add(New SqlParameter("@TOTALROWS", SqlDbType.BigInt))
                    .Parameters("@TOTALROWS").Direction = ParameterDirection.Output
                    .Parameters("@TOTALROWS").Value = 0

                End With

                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument

                Do While objSqlReader.Read()
                    If blnRecordFound = False Then
                        With objOutputXml.DocumentElement.SelectSingleNode("STYLE")
                            .Attributes("W_StyleId").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("W_StyleId")))
                            .Attributes("BarcodeNo").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("BarcodeNo")))
                            .Attributes("StyleName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("StyleName")))
                            .Attributes("DesignNo").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DesignNo")))
                            .Attributes("ShadeNo").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ShadeNo")))
                            .Attributes("MRP").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MRP")))
                        End With
                        blnRecordFound = True
                    Else
                        objAptNode = objOutputXml.DocumentElement.SelectSingleNode("STYLE")
                        objAptNodeClone = objAptNode.CloneNode(True)
                        objAptNodeClone.Attributes("W_StyleId").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("W_StyleId")))
                        objAptNodeClone.Attributes("BarcodeNo").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("BarcodeNo")))
                        objAptNodeClone.Attributes("StyleName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("StyleName")))
                        objAptNodeClone.Attributes("DesignNo").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DesignNo")))
                        objAptNodeClone.Attributes("ShadeNo").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ShadeNo")))
                        objAptNodeClone.Attributes("MRP").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MRP")))
                        objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    End If
                Loop
                objSqlReader.Close()
                If blnRecordFound = False Then
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "TRUE"
                    bizShared.bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").InnerText = Val(objSqlCommand.Parameters("@TOTALROWS").Value)
                    If intPageSize = 0 Then
                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = 0
                    Else
                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = Math.Ceiling(objSqlCommand.Parameters("@TOTALROWS").Value / intPageSize)
                    End If

                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                End If

            Catch Exec As bizShared.AAMSException
                'CATCHING AAMS EXCEPTIONS
                bizShared.bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                'msgbox(Exec.ToString)
                bizShared.bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bizShared.bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try

            Return objOutputXml
        End Function

        Public Function Update(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***************************************************************************************************************
            'Purpose:This function Inserts/Updates Airline Office.
            'Input  :
            '<MS_UPDATESTYLE_INPUT>
            '	<STYLE Action='' W_StyleId='' BarcodeNo='' StyleName='' DesignNo='' ShadeNo='' MRP='' />
            '</MS_UPDATESTYLE_INPUT>

            'Output :
            '<MS_UPDATESTYLE_OUTPUT>
            '	<STYLE Action='' W_StyleId='' BarcodeNo='' StyleName='' DesignNo='' ShadeNo='' MRP='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</MS_UPDATESTYLE_OUTPUT>
            '***************************************************************************************************************

            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objUpdateDocOutput As New XmlDocument

            Dim strW_StyleId As String
            Dim intMRP As Integer
            Dim strBarcodeNo As String = String.Empty
            Dim strStyleName As String = String.Empty
            Dim strDesignNo As String = String.Empty
            Dim strShadeNo As String = String.Empty

            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_OUTPUT)

                With objUpdateDocOutput.DocumentElement.SelectSingleNode("STYLE")
                    .Attributes("Action").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("STYLE").Attributes("Action").InnerText
                    .Attributes("W_StyleId").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("STYLE").Attributes("W_StyleId").InnerText
                    .Attributes("BarcodeNo").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("STYLE").Attributes("BarcodeNo").InnerText
                    .Attributes("StyleName").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("STYLE").Attributes("StyleName").InnerText
                    .Attributes("DesignNo").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("STYLE").Attributes("DesignNo").InnerText
                    .Attributes("ShadeNo").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("STYLE").Attributes("ShadeNo").InnerText
                    .Attributes("MRP").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("STYLE").Attributes("MRP").InnerText
                End With

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("STYLE")
                    strAction = ((.Attributes("Action").InnerText).Trim).ToString
                    strW_StyleId = ((.Attributes("W_StyleId").InnerText).Trim).ToString
                    strBarcodeNo = ((.Attributes("BarcodeNo").InnerText).Trim).ToString
                    strStyleName = ((.Attributes("StyleName").InnerText).Trim).ToString
                    strDesignNo = ((.Attributes("DesignNo").InnerText).Trim).ToString
                    strShadeNo = ((.Attributes("ShadeNo").InnerText).Trim).ToString
                    intMRP = Val(((.Attributes("MRP").InnerText).Trim).ToString & "")

                    If strAction = "I" Then
                        If strBarcodeNo = "" Then
                            Throw (New bizShared.AAMSException("BarcodeNo can't be blank."))
                        End If
                    End If
                    If strAction = "U" Then
                        If strBarcodeNo = "" Then
                            Throw (New bizShared.AAMSException("BarcodeNo can't be blank."))
                        End If
                        If strW_StyleId = "" Then
                            Throw (New bizShared.AAMSException("Style id can't be Empty."))
                        End If
                    End If
                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_STYLE"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction

                    '@W_StyleId INT= NULL,
                    '@BarcodeNo VARCHAR(150) = NULL,
                    '@StyleName VARCHAR(50) = NULL,
                    '@DesignNo VARCHAR(50) = NULL,
                    '@ShadeNo VARCHAR(25) = NULL,
                    '@MRP INT = NULL,

                    .Parameters.Add(New SqlParameter("@W_StyleId", SqlDbType.Int))
                    .Parameters("@W_StyleId").Value = Val(strW_StyleId & "")

                    .Parameters.Add(New SqlParameter("@BarcodeNo", SqlDbType.VarChar, 200))
                    .Parameters("@BarcodeNo").Value = strBarcodeNo

                    .Parameters.Add(New SqlParameter("@StyleName", SqlDbType.VarChar, 50))
                    .Parameters("@StyleName").Value = strStyleName

                    .Parameters.Add(New SqlParameter("@DesignNo", SqlDbType.VarChar, 50))
                    .Parameters("@DesignNo").Value = strDesignNo

                    .Parameters.Add(New SqlParameter("@ShadeNo", SqlDbType.VarChar, 50))
                    .Parameters("@ShadeNo").Value = strShadeNo

                    .Parameters.Add(New SqlParameter("@MRP", SqlDbType.Int))
                    .Parameters("@MRP").Value = intMRP

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output

                    .Parameters("@RETURNID").Value = 0

                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED
                    .Connection.Open()

                    intRecordsAffected = .ExecuteNonQuery()

                    If UCase(strAction) = "I" Then
                        intRetId = .Parameters("@RETURNID").Value
                        If intRetId = 0 Then
                            Throw (New bizShared.AAMSException("Barcode No Already Exists!"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("STYLE").Attributes("W_StyleId").InnerText = intRetId
                        End If
                    ElseIf UCase(strAction) = "U" Then
                        intRetId = .Parameters("@RETURNID").Value
                        If intRetId = 0 Then
                            Throw (New bizShared.AAMSException("Barcode no Not Updated!"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                        End If
                    End If

                End With
            Catch Exec As bizShared.AAMSException
                'CATCHING AAMS EXCEPTIONS
                bizShared.bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                Return objUpdateDocOutput

            Catch Exec As Exception
                bizShared.bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bizShared.bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                Return objUpdateDocOutput

            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objUpdateDocOutput

        End Function

        Public Function View(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose: This function gives details of Airline.
            'Input  :
            '<MS_VIEWSTYLE_INPUT>
            '	<W_StyleId></W_StyleId>
            '</MS_VIEWSTYLE_INPUT>

            'Output :
            '<MS_VIEWSTYLE_OUTPUT>
            ' <STYLE W_StyleId='' BarcodeNo='' StyleName='' DesignNo='' ShadeNo='' MRP='' />
            '  <Errors Status=''>
            '       <Error Code='' Description='' />
            '  </Errors>
            '</MS_VIEWSTYLE_OUTPUT>
            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objSqlReader As SqlDataReader
            Dim strW_StyleId As String
            Const strMETHOD_NAME As String = "View"

            Try
                objOutputXml.LoadXml(srtVIEW_OUTPUT)

                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strW_StyleId = IndexDoc.DocumentElement.SelectSingleNode("W_StyleId").InnerText.Trim
                If strW_StyleId = "" Then
                    Throw (New bizShared.AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_SRO_MS_STYLE]"
                    .Connection = objSqlConnection
                    .Parameters.Add("W_StyleId", SqlDbType.Int)
                    .Parameters("W_StyleId").Value = strW_StyleId

                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"

                    .Parameters.Add("@RETURNID", SqlDbType.Int)
                    .Parameters("@RETURNID").Value = 0

                End With
                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    Dim blnRecordFound As Boolean
                    If blnRecordFound = False Then
                        With objOutputXml.DocumentElement.SelectSingleNode("STYLE")

                            .Attributes("W_StyleId").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("W_StyleId")))
                            .Attributes("BarcodeNo").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("BarcodeNo")))
                            .Attributes("StyleName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("StyleName")))
                            .Attributes("DesignNo").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DesignNo")))
                            .Attributes("ShadeNo").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ShadeNo")))
                            .Attributes("MRP").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MRP")))
                        End With
                    End If
                    blnRecordFound = True
                Loop
            Catch Exec As bizShared.AAMSException
                'CATCHING AAMS EXCEPTIONS

                bizShared.bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                bizShared.bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bizShared.bzShared.FillErrorStatus(objOutputXml, "103", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Return objOutputXml

        End Function

        Public Function UpdateStyleOrder(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***************************************************************************************************************
            'Purpose:This function Inserts/Updates Airline Office.
            'Input  :
            '<MS_UPDATESTYLE_ORDER_INPUT>
            '	<STYLE Action='' W_StyleOrderID='' W_StyleId='' BarcodeNo='' StyleName='' DesignNo='' ShadeNo='' MRP='' Qty='' Remarks='' />
            '</MS_UPDATESTYLE_ORDER_INPUT>

            'Output :
            '<MS_UPDATESTYLE_ORDER_OUTPUT>
            '	<STYLE Action='' W_StyleOrderID='' W_StyleId='' BarcodeNo='' StyleName='' DesignNo='' ShadeNo='' MRP='' Qty=''/>
            '<Errors Status=''>
            '    <Error Code='' Description='' />
            '</Errors>
            '</MS_UPDATESTYLE_ORDER_OUTPUT>
            '***************************************************************************************************************

            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString)
            Dim objSqlCommand As New SqlCommand
            Dim objUpdateDocOutput As New XmlDocument

            Dim W_StyleOrderID As String = String.Empty
            Dim strW_StyleId As String = String.Empty
            Dim strBarcodeNo As String = String.Empty
            Dim strStyleName As String = String.Empty
            Dim strDesignNo As String = String.Empty
            Dim strShadeNo As String = String.Empty
            Dim strRemarks As String = String.Empty
            Dim intMRP As Integer = 0
            Dim intQty As Integer = 0

            Dim intRecordsAffected As Integer
            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATE_STYLE_ORDER_OUTPUT)

                With objUpdateDocOutput.DocumentElement.SelectSingleNode("STYLE")
                    .Attributes("Action").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("STYLE").Attributes("Action").InnerText
                    .Attributes("W_StyleOrderID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("STYLE").Attributes("W_StyleOrderID").InnerText
                    .Attributes("W_StyleId").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("STYLE").Attributes("W_StyleId").InnerText
                    ''********** These are not required but if in cae user want's enter manually than it helpful
                    .Attributes("BarcodeNo").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("STYLE").Attributes("BarcodeNo").InnerText
                    .Attributes("StyleName").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("STYLE").Attributes("StyleName").InnerText
                    .Attributes("DesignNo").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("STYLE").Attributes("DesignNo").InnerText
                    .Attributes("ShadeNo").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("STYLE").Attributes("ShadeNo").InnerText
                    .Attributes("MRP").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("STYLE").Attributes("MRP").InnerText
                    ''********** These are not required but if in cae user want's enter manually than it helpful
                    .Attributes("Qty").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("STYLE").Attributes("Qty").InnerText
                End With

                'Retrieving & Checking Details from Input XMLDocument
                With UpdateDoc.DocumentElement.SelectSingleNode("STYLE")

                    If ((.Attributes("W_StyleOrderID").InnerText).Trim).ToString = "" Then
                        strAction = "I"
                    Else
                        strAction = "U"
                    End If

                    strW_StyleId = ((.Attributes("W_StyleId").InnerText).Trim).ToString
                    strBarcodeNo = ((.Attributes("BarcodeNo").InnerText).Trim).ToString
                    strStyleName = ((.Attributes("StyleName").InnerText).Trim).ToString
                    strDesignNo = ((.Attributes("DesignNo").InnerText).Trim).ToString
                    strShadeNo = ((.Attributes("ShadeNo").InnerText).Trim).ToString
                    intMRP = Val(((.Attributes("MRP").InnerText).Trim).ToString & "")
                    intQty = Val(((.Attributes("Qty").InnerText).Trim).ToString & "")
                    strRemarks = ((.Attributes("Remarks").InnerText).Trim).ToString

                    If strAction = "I" Then
                        If strW_StyleId = "" Then
                            Throw (New bizShared.AAMSException("Please select Style."))
                        End If
                    End If
                    If strAction = "U" Then
                        If strW_StyleId = "" Then
                            Throw (New bizShared.AAMSException("Please select Style."))
                        End If
                    End If
                End With

                'ADDING PARAMETERS IN STORED PROCEDURE
                With objSqlCommand
                    .Connection = objSqlConnection
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_STYLE_ORDER"

                    .Parameters.Add(New SqlParameter("@ACTION", SqlDbType.Char, 1))
                    .Parameters("@ACTION").Value = strAction

                    '@W_StyleId INT= NULL,
                    '@BarcodeNo VARCHAR(150) = NULL,
                    '@StyleName VARCHAR(50) = NULL,
                    '@DesignNo VARCHAR(50) = NULL,
                    '@ShadeNo VARCHAR(25) = NULL,
                    '@MRP INT = NULL,

                    .Parameters.Add(New SqlParameter("@W_StyleId", SqlDbType.Int))
                    .Parameters("@W_StyleId").Value = Val(strW_StyleId & "")

                    .Parameters.Add(New SqlParameter("@BarcodeNo", SqlDbType.VarChar, 200))
                    .Parameters("@BarcodeNo").Value = strBarcodeNo

                    .Parameters.Add(New SqlParameter("@StyleName", SqlDbType.VarChar, 50))
                    .Parameters("@StyleName").Value = strStyleName

                    .Parameters.Add(New SqlParameter("@DesignNo", SqlDbType.VarChar, 50))
                    .Parameters("@DesignNo").Value = strDesignNo

                    .Parameters.Add(New SqlParameter("@ShadeNo", SqlDbType.VarChar, 50))
                    .Parameters("@ShadeNo").Value = strShadeNo

                    .Parameters.Add(New SqlParameter("@MRP", SqlDbType.Int))
                    .Parameters("@MRP").Value = intMRP

                    .Parameters.Add(New SqlParameter("@QTY", SqlDbType.Int))
                    .Parameters("@QTY").Value = intQty

                    .Parameters.Add(New SqlParameter("@Remarks", SqlDbType.VarChar, 1000))
                    .Parameters("@Remarks").Value = strRemarks

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output

                    .Parameters("@RETURNID").Value = 0

                    'EXECUTING STORED PROCEDURE AND CHECKING IF RECORD IS INSERTED/UPDATED
                    .Connection.Open()

                    intRecordsAffected = .ExecuteNonQuery()

                    If UCase(strAction) = "I" Then
                        intRetId = .Parameters("@RETURNID").Value
                        If intRetId = 0 Then
                            Throw (New bizShared.AAMSException("Error while updating order!"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("STYLE").Attributes("W_StyleOrderID").InnerText = intRetId
                        End If
                    ElseIf UCase(strAction) = "U" Then
                        intRetId = .Parameters("@RETURNID").Value
                        If intRetId = 0 Then
                            Throw (New bizShared.AAMSException("Error while updating order!"))
                        Else
                            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                        End If
                    End If
                End With
            Catch Exec As bizShared.AAMSException
                'CATCHING AAMS EXCEPTIONS
                bizShared.bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                Return objUpdateDocOutput

            Catch Exec As Exception
                bizShared.bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bizShared.bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                Return objUpdateDocOutput

            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            Return objUpdateDocOutput
        End Function

        Public Function List(ByVal ListDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '---------INPUT XML-----------------------------------------------------------------
            '<UP_LIST_STYLE_INPUT>
            '   <TYPE/>
            '</UP_LIST_STYLE_INPUT>

            '---------OUTPUT XML-----------------------------------------------------------------
            '<UP_LIST_STYLE_OUTPUT>
            '	<STYLE ID='' VALUE='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</UP_LIST_STYLE_OUTPUT>
            '------------------------------------------------------------------------------------

            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean
            Dim strMETHOD_NAME As String = "List"
            Dim strType As String = String.Empty
            objOutputXml.LoadXml(srtLIST_OUTPUT)

            Try
                strType = ListDoc.DocumentElement.SelectSingleNode("TYPE").InnerText.Trim
                If strType = "" Then
                    Throw (New bizShared.AAMSException("Incomplete Parameters Type"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_LST_W_STYLE_LIST"
                    .Connection = objSqlConnection
                    .Parameters.Add(New SqlParameter("@TYPE", SqlDbType.Int))
                    .Parameters("@TYPE").Value = Val(strType & "")

                End With

                'retrieving the records according to the List Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("STYLE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read
                    blnRecordFound = True
                    objAptNodeClone.Attributes("ID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ID")) & "")
                    objAptNodeClone.Attributes("VALUE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("VALUE")) & "")
                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop

                If blnRecordFound = False Then
                    bizShared.bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                End If

            Catch Exec As bizShared.AAMSException
                'CATCHING AAMS EXCEPTIONS
                bizShared.bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch exec As Exception
                'msgbox(exec.ToString)
                bizShared.bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, exec)
                bizShared.bzShared.FillErrorStatus(objOutputXml, "103", exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try

            Return objOutputXml
        End Function

        Public Function OrderView(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '************************************************************************
            'Input
            '<INV_VIEW_W_STYLEORDER_INPUT>
            '	<W_StyleOrderID></W_StyleOrderID>
            '</INV_VIEW_W_STYLEORDER_INPUT>

            'Output
            '<INV_VIEW_W_STYLEORDER_OUTPUT>
            '	<ORDERHEADER W_StyleOrderID='' Totqty='' Remarks='' OrderDate='' LoggedBy='' LoggedByName=''
            '		OrderNumber=''>
            '		<ORDERDETAILS W_StyleOrderID='' qty='' W_StyleId='' StyleName='' DesignNo='' ShadeNo='' MRP='' />
            '	</ORDERHEADER>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</INV_VIEW_W_STYLEORDER_OUTPUT>

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objApndNode As XmlNode
            Dim objApndNodeClone As XmlNode
            Dim objApndNodeTemp As XmlNode
            Dim objApndNodeCloneTemp As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim intW_StyleOrderID As Integer
            Dim blnRecordFound As Boolean
            Dim blnHeadRecordFound As Boolean

            Const strMETHOD_NAME As String = "View"

            Try
                objOutputXml.LoadXml(strOrderView_OUTPUT)

                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                intW_StyleOrderID = IndexDoc.DocumentElement.SelectSingleNode("W_StyleOrderID").InnerText.Trim
                If intW_StyleOrderID = 0 Then
                    Throw (New bizShared.AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_STYLE_ORDER"
                    .Connection = objSqlConnection

                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "S"

                    .Parameters.Add("@W_StyleOrderID", SqlDbType.Int)
                    .Parameters("@W_StyleOrderID").Value = intW_StyleOrderID

                    .Parameters.Add("@RETURNID", SqlDbType.Int)
                    .Parameters("@RETURNID").Value = 0
                End With

                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()
                Do While objSqlReader.Read()
                    If blnHeadRecordFound = False Then
                        With objOutputXml.DocumentElement.SelectSingleNode("ORDERHEADER")
                            .Attributes("W_StyleOrderID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("W_StyleOrderID")) & "")
                            .Attributes("Totqty").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Totqty")) & "")
                            .Attributes("Remarks").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("Remarks")) & "")
                            .Attributes("OrderDate").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("OrderDate")) & "")
                            .Attributes("LoggedBy").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LoggedBy")) & "")
                            .Attributes("LoggedByName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LoggedByName")) & "")
                            blnHeadRecordFound = True
                        End With
                        objApndNode = objOutputXml.DocumentElement.SelectSingleNode("ORDERHEADER/ORDERDETAILS")
                        objApndNodeClone = objApndNode.CloneNode(True)
                        objOutputXml.DocumentElement.SelectSingleNode("ORDERHEADER").RemoveChild(objApndNode)
                    End If
                    objApndNodeClone.Attributes("W_StyleOrderID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("W_StyleOrderID")) & "")
                    objApndNodeClone.Attributes("qty").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("qty")) & "")
                    objApndNodeClone.Attributes("W_StyleId").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("W_StyleId")) & "")
                    objApndNodeClone.Attributes("StyleName").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("StyleName")) & "")
                    objApndNodeClone.Attributes("DesignNo").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DesignNo")) & "")
                    objApndNodeClone.Attributes("ShadeNo").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ShadeNo")) & "")
                    objApndNodeClone.Attributes("MRP").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MRP")) & "")
                    objOutputXml.DocumentElement.SelectSingleNode("ORDERHEADER").AppendChild(objApndNodeClone)

                    objApndNodeClone = objApndNode.CloneNode(True)
                Loop

            Catch Exec As bizShared.AAMSException
                'CATCHING AAMS EXCEPTIONS
                bizShared.bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                bizShared.bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bizShared.bzShared.FillErrorStatus(objOutputXml, "103", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Return objOutputXml
        End Function

        Public Function UpdateOrderBooking(ByVal UpdateDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '************************************************************************
            'Input
            '<INV_UPDATE_W_ORDER_INPUT>
            '	<ORDERHEADER W_StyleOrderID='' Totqty='' Remarks='' OrderDate='' LoggedBy='' LoggedByName='' OrderNumber=''>
            '		<ORDERDETAILS W_StyleOrderID='' qty='' W_StyleId='' StyleName='' DesignNo='' ShadeNo='' MRP='' />
            '	</ORDERHEADER>
            '</INV_UPDATE_W_ORDER_INPUT>

            'Output
            '<INV_UPDATE_W_ORDER_OUTPUT>
            '	<ORDERHEADER W_StyleOrderID='' Totqty='' Remarks='' OrderDate='' LoggedBy='' LoggedByName=''
            '		OrderNumber=''>
            '		<ORDERDETAILS W_StyleOrderID='' qty='' W_StyleId='' StyleName='' DesignNo='' ShadeNo='' MRP='' />
            '	</ORDERHEADER>
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</INV_UPDATE_W_ORDER_OUTPUT>

            '************************************************************************
            Dim intRetId As Integer
            Dim strAction As String = ""
            Dim strErrorMsg As String
            Dim intRecordsAffected As Int32
            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objSqlCommandNew As New SqlCommand
            Dim objTransaction As SqlTransaction
            Dim objUpdateDocOutput As New XmlDocument

            Dim intW_StyleOrderID As Integer
            Dim intTotqty As Integer
            Dim strRemarks As String
            Dim intOrderDate As Integer
            Dim strLoggedBy As String
            Dim strOrderNumber As String
            Dim intQty As Integer
            Dim intW_StyleId As Integer

            Dim objXmlNodeList As XmlNodeList
            Dim objXmlNode As XmlNode
            Dim objDetailXMLNode As XmlNode, objDetailXMLNodeClone As XmlNode

            Const strMETHOD_NAME As String = "Update"
            Try
                objUpdateDocOutput.LoadXml(strUPDATEORDERBOOKING_OUTPUT)
                With objUpdateDocOutput.DocumentElement.SelectSingleNode("ORDERHEADER")
                    .Attributes("W_StyleOrderID").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERHEADER").Attributes("W_StyleOrderID").InnerText
                    .Attributes("Totqty").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERHEADER").Attributes("Totqty").InnerText
                    .Attributes("Remarks").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERHEADER").Attributes("Remarks").InnerText
                    .Attributes("OrderDate").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERHEADER").Attributes("OrderDate").InnerText
                    .Attributes("LoggedBy").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERHEADER").Attributes("LoggedBy").InnerText
                    .Attributes("LoggedByName").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERHEADER").Attributes("LoggedByName").InnerText
                    .Attributes("OrderNumber").InnerText = UpdateDoc.DocumentElement.SelectSingleNode("ORDERHEADER").Attributes("OrderNumber").InnerText
                End With
                objDetailXMLNode = objUpdateDocOutput.DocumentElement.SelectSingleNode("ORDERHEADER/ORDERDETAILS")
                objDetailXMLNodeClone = objDetailXMLNode.CloneNode(True)
                objUpdateDocOutput.DocumentElement.SelectSingleNode("ORDERHEADER").RemoveChild(objDetailXMLNode)

                objXmlNodeList = UpdateDoc.DocumentElement.SelectNodes("ORDERHEADER/ORDERDETAILS")
                For Each objXmlNode In objXmlNodeList
                    objDetailXMLNodeClone.Attributes("W_StyleOrderID").InnerText = objXmlNode.Attributes("W_StyleOrderID").InnerText
                    objDetailXMLNodeClone.Attributes("qty").InnerText = objXmlNode.Attributes("qty").InnerText
                    objDetailXMLNodeClone.Attributes("W_StyleId").InnerText = objXmlNode.Attributes("W_StyleId").InnerText
                    objDetailXMLNodeClone.Attributes("StyleName").InnerText = objXmlNode.Attributes("StyleName").InnerText
                    objDetailXMLNodeClone.Attributes("DesignNo").InnerText = objXmlNode.Attributes("DesignNo").InnerText
                    objDetailXMLNodeClone.Attributes("ShadeNo").InnerText = objXmlNode.Attributes("ShadeNo").InnerText
                    objDetailXMLNodeClone.Attributes("MRP").InnerText = objXmlNode.Attributes("MRP").InnerText

                    objUpdateDocOutput.DocumentElement.SelectSingleNode("ORDERHEADER").AppendChild(objDetailXMLNodeClone)
                    objDetailXMLNodeClone = objDetailXMLNode.CloneNode(True)
                Next

                With UpdateDoc.DocumentElement.SelectSingleNode("ORDERHEADER")
                    If ((.Attributes("W_StyleOrderID").InnerText).Trim).ToString <> "" Then
                        intW_StyleOrderID = ((.Attributes("W_StyleOrderID").InnerText).Trim).ToString
                        strAction = "U"
                    Else
                        intW_StyleOrderID = 0
                        strAction = "I"
                    End If

                    If ((.Attributes("Totqty").InnerText).Trim).ToString <> "" Then
                        intTotqty = ((.Attributes("Totqty").InnerText).Trim).ToString
                    Else
                        intTotqty = 0
                    End If

                    If ((.Attributes("Remarks").InnerText).Trim).ToString <> "" Then
                        strRemarks = ((.Attributes("Remarks").InnerText).Trim).ToString
                    Else
                        strRemarks = ""
                    End If

                    If ((.Attributes("OrderDate").InnerText).Trim).ToString <> "" Then
                        intOrderDate = ((.Attributes("OrderDate").InnerText).Trim).ToString
                    Else
                        intOrderDate = 0
                    End If

                    If ((.Attributes("LoggedBy").InnerText).Trim).ToString <> "" Then
                        strLoggedBy = ((.Attributes("LoggedBy").InnerText).Trim).ToString
                    Else
                        strLoggedBy = vbNullString
                    End If

                    If ((.Attributes("OrderNumber").InnerText).Trim).ToString <> "" Then
                        strOrderNumber = ((.Attributes("OrderNumber").InnerText).Trim).ToString
                    Else
                        strOrderNumber = ""
                    End If

                    If strAction = "I" Or strAction = "U" Then
                        If intW_StyleOrderID = 0 Then
                            If strAction = "U" Then
                                If intW_StyleOrderID = 0 Then Throw (New bizShared.AAMSException("Order ID can't be blank."))
                            End If
                        End If
                        ''Check for Mandatory fields
                        If intTotqty = 0 Then Throw (New bizShared.AAMSException("Total Quantity can't be blank."))
                    Else
                        Throw (New bizShared.AAMSException("Invalid Action Code."))
                    End If
                End With

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_SRO_MS_STYLE_ORDER]"
                    .Connection = objSqlConnection

                    .Parameters.Add("@ACTION", SqlDbType.Char, 1)
                    .Parameters("@ACTION").Value = strAction

                    '@W_StyleOrderID INT,
                    '@Totqty INT = 0,
                    '@Remarks VARCHAR(1000),
                    '@LoggedBy INT=NULL,
                    '@OrderNumber VARCHAR(10)=NULL,


                    .Parameters.Add("@W_StyleOrderID", SqlDbType.Int)
                    If intW_StyleOrderID = 0 Then
                        .Parameters("@W_StyleOrderID").Value = DBNull.Value
                    Else
                        .Parameters("@W_StyleOrderID").Value = intW_StyleOrderID
                    End If

                    .Parameters.Add("@Totqty", SqlDbType.Int)
                    If intTotqty = 0 Then
                        .Parameters("@Totqty").Value = DBNull.Value
                    Else
                        .Parameters("@Totqty").Value = intTotqty
                    End If

                    .Parameters.Add("@Remarks", SqlDbType.VarChar, 1000)
                    If strRemarks = "" Then
                        .Parameters("@Remarks").Value = DBNull.Value
                    Else
                        .Parameters("@Remarks").Value = strRemarks
                    End If

                    .Parameters.Add("@LoggedBy", SqlDbType.Int)
                    If strLoggedBy = "" Then
                        .Parameters("@LoggedBy").Value = DBNull.Value
                    Else
                        .Parameters("@LoggedBy").Value = Val(strLoggedBy & "")
                    End If

                    .Parameters.Add("@OrderNumber", SqlDbType.VarChar, 10)
                    If strOrderNumber = "" Then
                        .Parameters("@OrderNumber").Value = DBNull.Value
                    Else
                        .Parameters("@OrderNumber").Value = strOrderNumber
                    End If

                    .Parameters.Add(New SqlParameter("@ISDETAIL", SqlDbType.VarChar, 3))
                    .Parameters("@ISDETAIL").Value = "No"

                    .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                    .Parameters("@RETURNID").Direction = ParameterDirection.Output
                    .Parameters("@RETURNID").Value = 0

                    objSqlConnection.Open()
                    objTransaction = objSqlConnection.BeginTransaction(IsolationLevel.RepeatableRead)

                    .Transaction = objTransaction
                    intRecordsAffected = .ExecuteNonQuery()
                    '.Connection.Close()
                    intRetId = .Parameters("@RETURNID").Value
                    intW_StyleOrderID = .Parameters("@RETURNID").Value
                    If UCase(strAction) = "I" Then
                        If intRetId = 0 Then
                            Throw (New bizShared.AAMSException("Unable to Insert!"))
                        Else
                            intW_StyleOrderID = intRetId
                        End If

                        With objUpdateDocOutput.DocumentElement.SelectSingleNode("ORDERHEADER")
                            .Attributes("W_StyleOrderID").InnerText = intRetId
                        End With
                    ElseIf UCase(strAction) = "U" Then
                        If intRetId = 0 Then
                            Throw (New bizShared.AAMSException("Unable to update!"))
                        End If
                    End If
                End With
                For Each objXmlNode In objXmlNodeList

                    intQty = 0
                    intW_StyleId = 0

                    If ((objXmlNode.Attributes("qty").InnerText).Trim).ToString <> "" Then
                        intQty = ((objXmlNode.Attributes("qty").InnerText).Trim).ToString
                    Else
                        intQty = 0
                    End If

                    If ((objXmlNode.Attributes("W_StyleId").InnerText).Trim).ToString <> "" Then
                        intW_StyleId = ((objXmlNode.Attributes("W_StyleId").InnerText).Trim).ToString
                    Else
                        intW_StyleId = 0
                    End If

                    Dim objSqlCommandTran As New SqlCommand
                    With objSqlCommandTran
                        .Connection = objSqlConnection
                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "[UP_SRO_MS_STYLE_ORDER]"

                        .Parameters.Add("@ACTION", SqlDbType.Char, 1)
                        .Parameters("@ACTION").Value = strAction

                        .Parameters.Add("@W_StyleOrderID", SqlDbType.Int)
                        If intW_StyleOrderID = 0 Then
                            .Parameters("@W_StyleOrderID").Value = DBNull.Value
                        Else
                            .Parameters("@W_StyleOrderID").Value = intW_StyleOrderID
                        End If

                        .Parameters.Add("@qty", SqlDbType.Int)
                        If intQty = 0 Then
                            .Parameters("@qty").Value = 0
                        Else
                            .Parameters("@qty").Value = Val(intQty & "")
                        End If

                        .Parameters.Add("@W_StyleId", SqlDbType.Int)
                        If intW_StyleId = 0 Then
                            .Parameters("@W_StyleId").Value = DBNull.Value
                        Else
                            .Parameters("@W_StyleId").Value = Val(intW_StyleId & "")
                        End If

                        .Parameters.Add(New SqlParameter("@ISDETAIL", SqlDbType.VarChar, 3))
                        .Parameters("@ISDETAIL").Value = "Yes"

                        .Parameters.Add(New SqlParameter("@RETURNID", SqlDbType.Int))
                        .Parameters("@RETURNID").Direction = ParameterDirection.Output
                        .Parameters("@RETURNID").Value = 0
                        .Transaction = objTransaction
                        intRecordsAffected = .ExecuteNonQuery()
                        intRetId = .Parameters("@RETURNID").Value
                    End With
                    If UCase(strAction) = "I" Then
                        If intRetId = 0 Then
                            Throw (New bizShared.AAMSException("Unable to Insert!"))
                        End If
                        With objUpdateDocOutput.DocumentElement.SelectSingleNode("ORDERHEADER")
                            .Attributes("W_StyleOrderID").InnerText = intRetId
                        End With
                    ElseIf UCase(strAction) = "U" Then
                        If intRetId = 0 Then
                            Throw (New bizShared.AAMSException("Unable to update!"))
                        End If
                    End If
                    objSqlCommandTran.Dispose()
                Next
                objTransaction.Commit()
            Catch Exec As bizShared.AAMSException
                'CATCHING AAMS EXCEPTIONS
                bizShared.bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bizShared.bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                Return objUpdateDocOutput
            Catch Exec As Exception
                If objSqlConnection.State <> ConnectionState.Closed Then
                    If Not objTransaction Is Nothing Then
                        objTransaction.Rollback()
                    End If
                    objSqlConnection.Close()
                End If
                bizShared.bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bizShared.bzShared.FillErrorStatus(objUpdateDocOutput, "101", Exec.Message)
                Return objUpdateDocOutput
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            objUpdateDocOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Return objUpdateDocOutput
        End Function

        Public Function SearchOrderBooking(ByVal SearchDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '***********************************************************************
            'Purpose:This function gives search results based on choosen search criterion.
            'Input :-
            '<MS_SEARCH_QUALITY_ORDER_INPUT>
            ' <ORDERNUMBER />
            ' <LOGGEDBY />
            ' <ORDER_DATE_FROM />
            ' <ORDER_DATE_TO />
            ' <W_StyleId />
            ' <PAGE_NO />
            ' <PAGE_SIZE />
            ' <SORT_BY />
            ' <DESC />
            '</MS_SEARCH_QUALITY_ORDER_INPUT>

            'Output :
            '<MS_SEARCH_QUALITY_ORDER_OUTPUT>
            ' <STYLE W_STYLEID='' W_StyleOrderID='' ORDERNUMBER='' ORDERDATE='' LOGGEDBY='' LOGGERNAME='' QUALITY='' DESIGN='' SHADENO='' MRP='' />
            ' <PAGE PAGE_COUNT='' TOTAL_ROWS=''/>
            ' <Errors Status=''>
            ' <Error Code='' Description='' />
            ' </Errors>
            '</MS_SEARCH_QUALITY_ORDER_OUTPUT>
            '************************************************************************

            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim objAptNode, objAptNodeClone As XmlNode
            Dim objSqlReader As SqlDataReader
            Dim blnRecordFound As Boolean


            Dim strORDERNUMBER As String = String.Empty
            Dim strLOGGEDBY As String = String.Empty
            Dim strORDER_DATE_FROM As String = String.Empty
            Dim strORDER_DATE_TO As String = String.Empty
            Dim strW_StyleId As String = String.Empty

            Dim intPageNo, intPageSize As Integer
            Dim strSortBy As String
            Dim blnDesc As Boolean

            Const strMETHOD_NAME As String = "Search"
            objOutputXml.LoadXml(strSearchStyleOrder_OUTPUT)

            Try
                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                strORDERNUMBER = SearchDoc.DocumentElement.SelectSingleNode("ORDERNUMBER").InnerText.Trim()
                strLOGGEDBY = SearchDoc.DocumentElement.SelectSingleNode("LOGGEDBY").InnerText.Trim()
                strORDER_DATE_FROM = SearchDoc.DocumentElement.SelectSingleNode("ORDER_DATE_FROM").InnerText.Trim()
                strORDER_DATE_TO = SearchDoc.DocumentElement.SelectSingleNode("ORDER_DATE_TO").InnerText.Trim()
                strW_StyleId = SearchDoc.DocumentElement.SelectSingleNode("W_StyleId").InnerText.Trim()

                'Paging Section
                If SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim() <> "" Then
                    intPageNo = SearchDoc.DocumentElement.SelectSingleNode("PAGE_NO").InnerText.Trim()
                End If
                If SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim() <> "" Then
                    intPageSize = SearchDoc.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim() <> "" Then
                    strSortBy = SearchDoc.DocumentElement.SelectSingleNode("SORT_BY").InnerText.Trim()
                End If

                If SearchDoc.DocumentElement.SelectSingleNode("DESC").InnerText.Trim().ToUpper = "TRUE" Then
                    blnDesc = True
                Else
                    blnDesc = False
                End If

                '--------Retrieving & Checking Details from Input XMLDocument ------End

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "[UP_SER_W_STYLE_ORDER]"
                    .Connection = objSqlConnection

                    .Parameters.Add("@ORDERNUMBER", SqlDbType.VarChar, 15)
                    If strORDERNUMBER = "" Then
                        .Parameters("@ORDERNUMBER").Value = DBNull.Value
                    Else
                        .Parameters("@ORDERNUMBER").Value = strORDERNUMBER
                    End If
                    .Parameters.Add("@LOGGEDBY", SqlDbType.Int)
                    If strLOGGEDBY = "" Then
                        .Parameters("@LOGGEDBY").Value = DBNull.Value
                    Else
                        .Parameters("@LOGGEDBY").Value = CInt(strLOGGEDBY)
                    End If

                    .Parameters.Add("@ORDER_DATE_FROM", SqlDbType.Int)
                    If strORDER_DATE_FROM = "" Then
                        .Parameters("@ORDER_DATE_FROM").Value = DBNull.Value
                    Else
                        .Parameters("@ORDER_DATE_FROM").Value = CInt(strORDER_DATE_FROM)
                    End If

                    .Parameters.Add("@ORDER_DATE_TO", SqlDbType.Int)
                    If strORDER_DATE_TO = "" Then
                        .Parameters("@ORDER_DATE_TO").Value = DBNull.Value
                    Else
                        .Parameters("@ORDER_DATE_TO").Value = CInt(strORDER_DATE_TO)
                    End If

                    .Parameters.Add("@W_StyleId", SqlDbType.Int)
                    If strW_StyleId = "" Then
                        .Parameters("@W_StyleId").Value = DBNull.Value
                    Else
                        .Parameters("@W_StyleId").Value = Val(strW_StyleId & "")
                    End If

                    .Parameters.Add("@PAGE_NO", SqlDbType.Int)
                    If intPageNo = 0 Then
                        .Parameters("@PAGE_NO").Value = DBNull.Value
                    Else
                        .Parameters("@PAGE_NO").Value = intPageNo
                    End If

                    .Parameters.Add("@PAGE_SIZE", SqlDbType.Int)
                    If intPageSize = 0 Then
                        .Parameters("@PAGE_SIZE").Value = DBNull.Value
                    Else
                        .Parameters("@PAGE_SIZE").Value = intPageSize
                    End If

                    .Parameters.Add("@SORT_BY", SqlDbType.VarChar, 100)
                    If strSortBy = "" Then
                        .Parameters("@SORT_BY").Value = DBNull.Value
                    Else
                        .Parameters("@SORT_BY").Value = strSortBy
                    End If

                    .Parameters.Add("@DESC", SqlDbType.Bit)
                    If blnDesc = True Then
                        .Parameters("@DESC").Value = 1
                    Else
                        .Parameters("@DESC").Value = 0
                    End If

                    .Parameters.Add(New SqlParameter("@TOTALROWS", SqlDbType.BigInt))
                    .Parameters("@TOTALROWS").Direction = ParameterDirection.Output
                    .Parameters("@TOTALROWS").Value = 0

                End With

                'retrieving the records according to the Search Criteria
                objSqlCommand.Connection.Open()
                objSqlReader = objSqlCommand.ExecuteReader()

                'Reading and Appending records into the Output XMLDocument
                objAptNode = objOutputXml.DocumentElement.SelectSingleNode("STYLE")
                objAptNodeClone = objAptNode.CloneNode(True)
                objOutputXml.DocumentElement.RemoveChild(objAptNode)
                Do While objSqlReader.Read()
                    blnRecordFound = True

                    objAptNodeClone.Attributes("W_STYLEID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("W_STYLEID")))
                    objAptNodeClone.Attributes("W_StyleOrderID").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("W_StyleOrderID")))
                    objAptNodeClone.Attributes("ORDERNUMBER").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDERNUMBER")))
                    objAptNodeClone.Attributes("ORDERDATE").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("ORDERDATE")))
                    If objAptNodeClone.Attributes("ORDERDATE").InnerText = "01/01/1900" Then
                        objAptNodeClone.Attributes("ORDERDATE").InnerText = ""
                    End If
                    objAptNodeClone.Attributes("LOGGEDBY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOGGEDBY")))
                    objAptNodeClone.Attributes("LOGGERNAME").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("LOGGERNAME")))
                    objAptNodeClone.Attributes("QUALITY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("QUALITY")))
                    objAptNodeClone.Attributes("DESIGN").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("DESIGN")))
                    objAptNodeClone.Attributes("SHADENO").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("SHADENO")))
                    objAptNodeClone.Attributes("MRP").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("MRP")))
                    objAptNodeClone.Attributes("QTY").InnerText = Trim(objSqlReader.GetValue(objSqlReader.GetOrdinal("QTY")))

                    objOutputXml.DocumentElement.AppendChild(objAptNodeClone)
                    objAptNodeClone = objAptNode.CloneNode(True)
                Loop
                objSqlReader.Close()
                If blnRecordFound = False Then
                    bizShared.bzShared.FillErrorStatus(objOutputXml, "101", "No Record Found!")
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").InnerText = Val(objSqlCommand.Parameters("@TOTALROWS").Value)
                    If intPageSize = 0 Then
                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = 0
                    Else
                        objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").InnerText = Math.Ceiling(Val(objSqlCommand.Parameters("@TOTALROWS").Value) / intPageSize)
                    End If
                End If
            Catch Exec As bizShared.AAMSException
                'CATCHING AAMS EXCEPTIONS
                bizShared.bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                'msgbox(Exec.ToString)
                bizShared.bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bizShared.bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try

            Return objOutputXml
        End Function

        Public Function OrderDELETE(ByVal IndexDoc As System.Xml.XmlDocument) As System.Xml.XmlDocument
            '************************************************************************
            'Input
            '<INV_DELETE_W_STYLEORDER_INPUT>
            '	<W_StyleOrderID></W_StyleOrderID>
            '</INV_DELETE_W_STYLEORDER_INPUT>

            'Output
            '<INV_DELETE_W_STYLEORDER_OUTPUT>            
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</INV_DELETE_W_STYLEORDER_OUTPUT>

            '************************************************************************
            Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString())
            Dim objSqlCommand As New SqlCommand
            Dim objOutputXml As New XmlDocument
            Dim intW_StyleOrderID As Integer
            Dim intRecordsAffected As Integer

            Const strMETHOD_NAME As String = "DELETE"
            Try
                objOutputXml.LoadXml(strOrderDELETE_OUTPUT)

                '--------Retrieving & Checking Details from Input XMLDocument ------Start
                intW_StyleOrderID = IndexDoc.DocumentElement.SelectSingleNode("W_StyleOrderID").InnerText.Trim
                If intW_StyleOrderID = 0 Then
                    Throw (New bizShared.AAMSException("Incomplete Parameters"))
                End If

                With objSqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "UP_SRO_MS_STYLE_ORDER"
                    .Connection = objSqlConnection

                    .Parameters.Add("@ACTION", SqlDbType.VarChar, 1)
                    .Parameters("@ACTION").Value = "D"

                    .Parameters.Add("@W_StyleOrderID", SqlDbType.Int)
                    .Parameters("@W_StyleOrderID").Value = intW_StyleOrderID

                    .Parameters.Add("@RETURNID", SqlDbType.Int)
                    .Parameters("@RETURNID").Value = 0
                End With

                ' RETRIVEING RECORD THROU STORED PROCEDURE
                objSqlCommand.Connection.Open()
                intRecordsAffected = objSqlCommand.ExecuteNonQuery()
                objSqlCommand.Connection.Close()

                'Checking whether record is deleted successfully or not
                If intRecordsAffected > 0 Then
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
                    Return (objOutputXml)
                Else
                    objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "TRUE"
                    Call bizShared.bzShared.FillErrorStatus(objOutputXml, "101", "Record has not been deleted!")
                    Return (objOutputXml)
                End If

            Catch Exec As bizShared.AAMSException
                'CATCHING AAMS EXCEPTIONS
                bizShared.bzShared.FillErrorStatus(objOutputXml, "101", Exec.Message)
                Return objOutputXml
            Catch Exec As Exception
                'CATCHING OTHER EXCEPTIONS
                bizShared.bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, Exec)
                bizShared.bzShared.FillErrorStatus(objOutputXml, "103", Exec.Message)
                Return objOutputXml
            Finally
                If objSqlConnection.State = ConnectionState.Open Then objSqlConnection.Close()
                objSqlCommand.Dispose()
            End Try
            objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").InnerText = "FALSE"
            Return objOutputXml
        End Function
    End Class
End Namespace