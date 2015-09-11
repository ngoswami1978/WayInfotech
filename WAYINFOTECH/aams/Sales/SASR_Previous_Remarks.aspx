<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SASR_Previous_Remarks.aspx.vb" Inherits="Sales_SASR_Previous_Remarks" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS::Sales::DSR Details</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <base target="_self"/>
     <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
     </head>
<body>
    <form id="frmSalesRemarks" runat="server" >
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
                               
                                <asp:Label ID="lblHeading" runat="server" Text=" Previous Remarks" />
                                </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                 
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  colspan="6" align="center" >
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="4" align="center">
                                                                    <asp:GridView ID="gvRemarks" AllowSorting="true" HeaderStyle-ForeColor="white" runat="server" BorderWidth="1" BorderColor="#d4d0c8" AutoGenerateColumns="False"
                                                                        Width="95%" TabIndex="9">
                                                                        <Columns>
                                                                        <asp:BoundField DataField="DATE"  HeaderText="Date"  HeaderStyle-HorizontalAlign="left" ItemStyle-Width ="15%"/>
                                                                        <asp:BoundField   DataField="CHANGE_DATA"  HeaderText="Remarks" HeaderStyle-HorizontalAlign="left" ItemStyle-Wrap="true" Visible="false" />
                                                                        <asp:BoundField   DataField="COMPETITION"  HeaderText="Competition Info/Mkt info Remarks " HeaderStyle-HorizontalAlign="left" ItemStyle-Wrap="true"  />
                                                                        <asp:BoundField   DataField="DISCUSSIONISSUE"  HeaderText="Detailed Discussion/Issue Reported " HeaderStyle-HorizontalAlign="left" ItemStyle-Wrap="true"  />
                                                                        
                                                                        </Columns>                                                                                                                                    
                                                                    <HeaderStyle CssClass="Gridheading"/>
                                                                    <RowStyle CssClass="ItemColor" HorizontalAlign="Left"/>
                                                                    <AlternatingRowStyle  CssClass ="lightblue" HorizontalAlign="Left" />
                                                                    </asp:GridView>
                                                                    </td>
                                                            </tr>
                                                        </table>
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
                     <input type="hidden" runat="server" id="hdLCode" style="width: 1px" />
                    <input type="hidden" runat="server" id="hdVisitDATE" style="width: 1px" />
             </td>
            </tr>
        </table>
    </form>
</body>
</html>
