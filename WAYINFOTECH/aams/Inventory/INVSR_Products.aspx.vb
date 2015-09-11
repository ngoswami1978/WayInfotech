Imports System.Data
Imports System.Xml
Imports System.Data.SqlClient
Partial Class INVSR_Products
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Public strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Page.IsPostBack Then
                If Not Request.QueryString("prodID") Is Nothing Then
                    hdprodID.Value = Request.QueryString("prodID").ToString
                End If
                If Not Request.QueryString("godownID") Is Nothing Then
                    hdgodownID.Value = Request.QueryString("godownID").ToString
                End If
                If Not Request.QueryString("challanType") Is Nothing Then
                    hdchallanType.value = Request.QueryString("challanType").ToString
                End If
                If Not Request.QueryString("LCODE") Is Nothing Then
                    hdLCODE.Value = objED.Decrypt(Request.QueryString("LCODE").ToString)
                End If
                If Not Request.QueryString("IssChallanNo") Is Nothing Then
                    hdIssChallanNo.Value = Request.QueryString("IssChallanNo").ToString
                End If
                StockDetailsSearch()
            End If
            'Code of Security Check
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    
#Region "Method for search StockDetails"
    Private Sub StockDetailsSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbzChallan As New AAMS.bizInventory.bzChallan

            objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /><GODOWNID /><EQUIP_GROUP /><EQUIP_CODE /><SERIALNUMBER /><STOCKSTATUS /><VENDERSERIALNO /><CHALLANNUMBER /><LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("PRODUCTID").InnerText = hdprodID.Value
            objInputXml.DocumentElement.SelectSingleNode("GODOWNID").InnerText = hdgodownID.Value
            objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = hdchallanType.Value
            objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdLCode.Value
            objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = hdIssChallanNo.Value

            'Here Back end Method Call 
            objOutputXml = objbzChallan.SearchStockDetails(objInputXml)
            'objOutputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_OUTPUT><DETAILS PRODUCTID='1' AOFFICE='dsfds' EGROUP_CODE='d' EQUIPMENT_CODE='d' PRODUCTNAME='dsddsfds' SERIALNUMBER='' VENDORSR_NUMBER='' STATUS='' CHALLANNUMBER='' CREATIONDATE='' 	GODOWNID='' CHALLANCATEGORY='' SUPPLIERID='' LCODE='' OUTTO='' /><Errors Status='FALSE'>	<Error Code='' Description=''/>	</Errors></INV_SEARCHSTOCKDETAILS_OUTPUT>")
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvProducts.DataSource = ds.Tables("DETAILS")
                gvProducts.DataBind()
                btnSelect.Visible = True
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                btnSelect.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
   


  


    Protected Sub gvProducts_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvProducts.RowDeleting

    End Sub
End Class
