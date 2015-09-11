<%@ Page Language="VB" AutoEventWireup="false"  MaintainScrollPositionOnPostback="true"  CodeFile="PUSRISPPaymentDetails.aspx.vb" Inherits="Popup_PUSRISPPaymentDetails" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Connectivity History</title>
        <base target="_self"/>
    <script type="text/javascript" language ="javascript" >
      function PrintHistory()
        {
          document.frames(0).focus();  
	      document.frames(0).print(); 
        }
     </script>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="width:100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="right" style ="width:860px;"><%--<a href="#" class="LinkButtons" onclick="PrintHistory();">Print</a>&nbsp;&nbsp;--%>&nbsp;
                    <a href="#" class="LinkButtons" onclick="window.close();">Close</a>&nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td align="center" class="heading">
                    ISP Payment Details</td>
            </tr>
            <tr>
                <td align="center" >
                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
            </tr>
            <tr>
                <td  style="padding-left:5px;" >
               <asp:GridView ID="GvDopaymentDetails" runat="server"  AutoGenerateColumns="False" TabIndex="4" Width="2024px"  AllowSorting ="true"  HeaderStyle-ForeColor="White" PageSize="25"  >
                                                     <Columns>                                                                                       
                                                                        <asp:TemplateField HeaderText="Account Id"  HeaderStyle-Wrap="false" SortExpression ="CAFNumber" >
                                                                                <itemtemplate>
                                                                                  <%--  <%#Eval("SlNo")%>--%>
                                                                                    <%#Eval("CAFNumber")%>  
                                                                                    <asp:HiddenField ID="hdPANumber" runat="server" Value='<%#Eval("PANumber")%>' />
                                                                                     <asp:HiddenField ID="hdNPID" runat="server" Value='<%#Eval("NPID")%>' />
                                                                                      <asp:HiddenField ID="hdISPOrderID" runat="server" Value='<%#Eval("ISPOrderID")%>' />
                                                                                        <asp:HiddenField ID="hdMonths" runat="server" Value='<%#Eval("Month")%>' />
                                                                                          <asp:HiddenField ID="hdYears" runat="server" Value='<%#Eval("Year")%>' />                                                                                                    
                                                                                    <asp:HiddenField ID="hdAmt" runat="server" Value='<%#Eval("Amount")%>' />
                                                                                </itemtemplate>
                                                                        </asp:TemplateField>     
                                                                                <%-- <asp:BoundField DataField="AccountNo" HeaderText="Account No" />--%>
                                                                               <%--  <asp:BoundField DataField="ISPOrderID"  HeaderText="ISP Order ID"  HeaderStyle-Wrap="false" />--%>
                                                                                <asp:BoundField DataField="UserName"  HeaderText="Login Name"  HeaderStyle-Wrap="false"  ItemStyle-Wrap="false" SortExpression ="UserName"   />  
                                                                                 <asp:BoundField DataField="NPID"  HeaderText="NPID"  HeaderStyle-Wrap="false"  ItemStyle-Wrap="false"   SortExpression ="NPID"  />
                                                                                                 <asp:BoundField DataField="Month" HeaderText="Cost Activity Month"  HeaderStyle-Wrap="false"  SortExpression ="Month"  />
                                                                                                 <asp:BoundField DataField="ISPName" HeaderText="ISP Name"  HeaderStyle-Wrap="false" ItemStyle-Wrap="true"  SortExpression ="ISPName"   />
                                                                                                 <asp:BoundField DataField="AgencyName"  HeaderText="Agency Name"  HeaderStyle-Wrap="false" ItemStyle-Wrap="true"  ItemStyle-Width="120px"  SortExpression ="AgencyName"  />
                                                                                                 <asp:BoundField DataField="Address" HeaderText="Address"  HeaderStyle-Wrap="false" ItemStyle-Wrap="true" ItemStyle-Width="180px"  SortExpression ="Address"   />
                                                                                                 <asp:BoundField DataField="City" HeaderText="City"  HeaderStyle-Wrap="false" ItemStyle-Wrap="False"  SortExpression ="City"  />
                                                                                                 <asp:BoundField DataField="OfficeID"  HeaderText="Office ID"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false"   SortExpression ="OfficeID"  />                                                                                                 
                                                                                                 <asp:BoundField DataField="OnlineDate" HeaderText="Online Date"   HeaderStyle-Wrap="false" ItemStyle-Wrap="false"  SortExpression ="OnlineDate"  />
                                                                                                 <asp:BoundField DataField="CancellationDate" HeaderText="Cancellation Date"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false"   SortExpression ="CancellationDate"  />
                                                                                                 <asp:BoundField DataField="Status" HeaderText="Status"  HeaderStyle-Wrap="false"  SortExpression ="UserName"  />
                                                                                                 <asp:BoundField DataField="StaticIP"  HeaderText="Static IP"  HeaderStyle-Wrap="false"  ItemStyle-Wrap="false"  SortExpression ="StaticIP"  />                                                                                                
                                                                                                 <asp:BoundField DataField="ISPRentalCharges"  HeaderText="Monthly Rental"   HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="right"  SortExpression ="ISPRentalCharges"  />
                                                                                                 <asp:BoundField DataField="VATAmount" HeaderText="VAT (12.24%)"  HeaderStyle-Wrap="false"   ItemStyle-HorizontalAlign="right"  SortExpression ="VATAmount"   />
                                                                                                 <asp:BoundField DataField="ISPInstallationCharges" HeaderText="Installation Charges"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="right"  SortExpression ="ISPInstallationCharges"  />
                                                                                                 <asp:BoundField DataField="I_VAT" HeaderText="VAT (12.24%)"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="right"  SortExpression ="I_VAT"    />
                                                                                                  <asp:BoundField DataField="StartDate"  HeaderText="From"  HeaderStyle-Wrap="false"  SortExpression ="StartDate"  />
                                                                                                 <asp:BoundField DataField="EndDate" HeaderText="To"  HeaderStyle-Wrap="false"  SortExpression ="EndDate"  />                                                                                                
                                                                                                  <asp:BoundField DataField="DaysUsed" HeaderText="Days Used"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="right"  SortExpression ="DaysUsed"  />
                                                                                                 <asp:BoundField DataField="ProRate" HeaderText="Pro Rate"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="right"  SortExpression ="ProRate"   />
                                                                                                 <asp:BoundField DataField="Amount" HeaderText="Amount"    HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="right"  SortExpression ="Amount"  />                                                                                                                                                                                                                                                                      
                                                                               <asp:TemplateField HeaderText=""  HeaderStyle-Wrap="false">
                                                                                <itemtemplate>                                                                                                
                                                                                </itemtemplate>
                                                                               </asp:TemplateField>     
                                                     </Columns>
                                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                                    <RowStyle CssClass="textbold" />
                                                                    <HeaderStyle CssClass="Gridheading"  HorizontalAlign="Left" />                                                    
                                         </asp:GridView>
                  
                </td>
            </tr>
            <tr>
                                                                <td  >
                                                                     <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="850px">
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="87px" CssClass="textboxgrey" ReadOnly ="true"  ></asp:TextBox></td>
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
            <tr>
            <td>
            
            <iframe id="ifrmPrnt" runat="server"  frameborder="0" src="BDRHistoryPrint.aspx" height="0" width="0"   ></iframe></td></tr>
             
        </table>
    </form>
</body>
</html>
