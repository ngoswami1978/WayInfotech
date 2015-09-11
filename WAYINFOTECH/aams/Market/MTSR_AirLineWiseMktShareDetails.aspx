<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MTSR_AirLineWiseMktShareDetails.aspx.vb" Inherits="Market_MTSR_AirLineWiseMktShareDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS:Airline wise Market Share Details</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <base target="_self"/>
     <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" language="javascript" >
   

</script>
       
  
     
</head>
<body  >
    <form id="frmAgencyOrderHistory" runat="server"  >
        <table width="860px" align="left" height="486px" >
            <tr>
                <td valign="top">
                    <table width="100%" align="left">  
                         <tr>
                            <td align="right">
                                <a href="#" class="LinkButtons" onclick="window.close();">Close</a>&nbsp;&nbsp;&nbsp;</td>
                        </tr>                      
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Airline wise Market Share Details</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                 
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" align="center" valign ="top" >
                                                                    <asp:GridView ID="gvAirlineDetails" runat="server" BorderWidth="1" BorderColor="#d4d0c8" AutoGenerateColumns="False" AllowSorting ="true"  HeaderStyle-ForeColor ="white" ShowFooter ="true" 
                                                                        Width="100%" TabIndex="9">
                                                                        <Columns>                                                 

                                                                        <asp:BoundField DataField="LCODE" HeaderText="Lcode"  SortExpression ="LCODE" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="left"   />
                                                                         <asp:BoundField DataField="Chain_code" HeaderText="Chain Code"  SortExpression ="Chain_code" HeaderStyle-HorizontalAlign="left" ItemStyle-Width="150px" />
                                                                        <asp:BoundField DataField="Airline" HeaderText="Airline"  SortExpression ="Airline" HeaderStyle-HorizontalAlign="left" ItemStyle-Width="150px" />
                                                                       <asp:BoundField DataField="AgencyName" HeaderText="Agency Name"  SortExpression ="AgencyName" HeaderStyle-HorizontalAlign="left" ItemStyle-Width="140px" />
                                                                        <asp:BoundField DataField="Address" HeaderText="Address"  SortExpression ="Address" HeaderStyle-HorizontalAlign="left" ItemStyle-Width="150px" />
                                                                        <asp:BoundField   DataField="city" HeaderText="City"  SortExpression ="city" ItemStyle-Width="100px" />   
                                                                        <asp:BoundField   DataField="A" HeaderText="1A"  SortExpression ="A"  ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="60px" />  
                                                                        <asp:BoundField   DataField="B" HeaderText="1B"  SortExpression ="B" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="60px"  />  
                                                                        <asp:BoundField   DataField="G" HeaderText="1G"  SortExpression ="G" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="60px"  />  
                                                                        <asp:BoundField   DataField="P" HeaderText="1P"  SortExpression ="P" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="60px"  />  
                                                                        <asp:BoundField   DataField="W" HeaderText="1W"  SortExpression ="W" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="60px"  />  
                                                                        <asp:BoundField   DataField="TOTAL" HeaderText="Total"  SortExpression ="TOTAL" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="70px"  />                                                                                                                                                     
                                                                        </Columns>                                                                                                                                    
                                                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign ="left"  />
                                                                    <RowStyle CssClass="ItemColor" HorizontalAlign="Left"/>
                                                                    <AlternatingRowStyle  CssClass ="lightblue" HorizontalAlign="Left" />
                                                                      <FooterStyle CssClass="Gridheading" />
                                     
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
                    </table>
                    
                                
               
            </tr>
        </table>
    </form>
</body>
</html>

  