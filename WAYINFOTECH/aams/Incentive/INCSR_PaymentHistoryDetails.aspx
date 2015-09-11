<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INCSR_PaymentHistoryDetails.aspx.vb" Inherits="Incentive_INCSR_PaymentHistoryDetails" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS:Past Payment History</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <base target="_self"/>
     <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" language="javascript" >
   
    
//        function GetOederId()
//        {   
//           var oMyObject = window.dialogArguments;
//           window.document.getElementById("hdOrderId").value=oMyObject;
//           alert(oMyObject);
//        }
</script>
       
  
     
</head>
<body  >
    <form id="frmAgencyOrderHistory" runat="server"  >
        <table width="100%" align="left" height="486px" >
            <tr>
                <td valign="top">
                    <table width="100%" align="left">  
                         <tr>
                            <td align="right">
                                <a href="#" class="LinkButtons" onclick="window.close();">Close</a>&nbsp;&nbsp;&nbsp;</td>
                        </tr>                      
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Past Payment History</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder" valign="top">
                                 
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="6" align="center" height="5px" valign="TOP">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="4" align="center">
                                                                    <asp:GridView ID="gvPaymentHistory" runat="server" BorderWidth="1" BorderColor="#d4d0c8" AutoGenerateColumns="False" AllowSorting ="false"  HeaderStyle-ForeColor ="white"
                                                                      ShowFooter ="true"    Width="400px" TabIndex="9">
                                                                        <Columns>                                                                        
                                                                       <asp:BoundField DataField="PAYMENT_MONTH_FROM" HeaderText="From Month" ItemStyle-HorizontalAlign="left"  HeaderStyle-HorizontalAlign="left"  Visible ="false"   />
                                                                       <asp:BoundField DataField="PAYMENT_YEAR_FROM" HeaderText="From Year"   HeaderStyle-HorizontalAlign="left"  Visible ="false"  />
                                                                        <asp:BoundField DataField="PAYMENT_MONTH_TO" HeaderText="To Month"   ItemStyle-HorizontalAlign="left"  HeaderStyle-HorizontalAlign="left"  Visible ="false"  />
                                                                       
                                                                       <asp:BoundField DataField="PAYMENT_YEAR_TO" HeaderText="To Year"   HeaderStyle-HorizontalAlign="left"  Visible ="false"  />
                                                                        <asp:BoundField DataField="PAYMENT_CYCLE_NAME" HeaderText="Payment Cycle"   HeaderStyle-HorizontalAlign="left"  Visible ="false"  />
                                                                       
                                                                        <asp:BoundField DataField="UPFRONT_AMOUNT" HeaderText="Upfront Amount"   HeaderStyle-HorizontalAlign="left"    ItemStyle-HorizontalAlign="right"  Visible ="false"  />
                                                                        <asp:BoundField DataField="NEXTUPFRONTAMOUNT" HeaderText="Next Upfront Amount"   ItemStyle-HorizontalAlign="right"  HeaderStyle-HorizontalAlign="left"  Visible ="false"  />
                                                                        <asp:BoundField DataField="TOTAL_AMOUNT" HeaderText="Total Amount" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="right"   Visible ="false"  />
                                                                        <asp:BoundField DataField="CHQ_DATE" HeaderText="Cheque Date"   ItemStyle-HorizontalAlign="left"  HeaderStyle-HorizontalAlign="left"  ItemStyle-Wrap="false"  />
                                                                        <asp:BoundField DataField="CHQ_AMOUNT" HeaderText="Cheque Amount"  HeaderStyle-HorizontalAlign="left"  ItemStyle-HorizontalAlign="right"   />
                                                                       
                                                                        
                                                                        <asp:BoundField DataField="CHQ_NO" HeaderText="Cheque No."  HeaderStyle-HorizontalAlign="left"   Visible ="false" />
                                                                        <asp:BoundField DataField="CHQ_BNAME" HeaderText="Bank Name" ItemStyle-HorizontalAlign="left"  HeaderStyle-HorizontalAlign="left"   Visible ="false"/>
                                                                        <asp:BoundField DataField="CREATED_DTTI" HeaderText="Created Date" ItemStyle-HorizontalAlign="left"  HeaderStyle-HorizontalAlign="left" ItemStyle-Wrap="false"  Visible ="false" />
                                                                        <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="Employee Name"   ItemStyle-HorizontalAlign="left"  HeaderStyle-HorizontalAlign="left"   Visible ="false" />
                                                                      
                                                                      <%--  <asp:BoundField DataField="INC_TYPE_NAME" HeaderText="Payment Type"   HeaderStyle-HorizontalAlign="left"   />
                                                                        <asp:BoundField DataField="PAYMENTTYPENAME" HeaderText="Payment Term"   ItemStyle-HorizontalAlign="right"  HeaderStyle-HorizontalAlign="left"  />
                                                                      --%>
                                                                        <asp:BoundField DataField="CHQ_ADJ_AMOUNT" HeaderText="Adjustment Amont"  HeaderStyle-HorizontalAlign="left"  ItemStyle-HorizontalAlign="right"   />
                                                                        </Columns>                                                                                                                                    
                                                                    <HeaderStyle CssClass="Gridheading"/>
                                                                    <RowStyle CssClass="ItemColor" HorizontalAlign="Left"/>
                                                                    <AlternatingRowStyle  CssClass ="lightblue" HorizontalAlign="Left" />
                                                                     <FooterStyle   CssClass="Gridheading"  />
                                                                    </asp:GridView>
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="12">
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td colspan="6" >
                                                                     <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ReadOnly ="true"  ></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                         </tr>
                                                                       </table></asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                  
                                        </td>
                                    </tr>
                                </table>
                                
                                </td>
                        </tr>
                        <tr>
                          <td>
                             <input type="hidden" runat="server" id="hdEnChainCode" style="width: 21px" />
                                                                                        <input type="hidden" runat="server" id="hdChainCode" style="width: 21px" />
                          </td>
                        </tr>
                    </table>
                  </td>   
                                
               
            </tr>
        </table>
    </form>
</body>
</html>