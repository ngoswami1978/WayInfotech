<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_Country.aspx.vb" Inherits="Setup_MSSR_Country" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Country Details</title>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body bgcolor="ffffff" >
    <form id="frmCRS" runat="server">
     <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Setup-></span><span class="sub_menu">Country</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                               Country List</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center"  valign="TOP" style="height: 4px">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" ></asp:Label>
                                                    </td>
                                                </tr>
                                               
                                                <tr>
                                                    <td colspan="4" style="height: 4px" align="center">
                                                    
                                                        <asp:GridView ID="gvCountryDetail" runat="server" AutoGenerateColumns="False" TabIndex="4" Width="50%"  AllowSorting="true" HeaderStyle-ForeColor="white"   >
                                                   
                                                    <Columns>
                                                        <asp:BoundField DataField="Country_Name" HeaderText="CountryName" SortExpression="Country_Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"  />
                                                                                  
                                                 
                                                 </Columns>
                                                 
                                                  <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" />
                                                    
                                                 
                                                 </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr height="12">
                                                    
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
        </table>
    </form>
</body>
</html>
