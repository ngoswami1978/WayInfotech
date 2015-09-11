<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_Product.aspx.vb" Inherits="Order_MSSR_Product" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Product</title>
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script type="text/javascript" language ="javascript" >      
    
  
    </script>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body bgcolor="ffffff"  >
    <form id="form1" runat="server"  >
    
     <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">Order-&gt;</span><span class="sub_menu">Products</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search <span style="font-family: Microsoft Sans Serif">Product</span></td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                 
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP" style="height: 25px">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="6%" class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 22px;" colspan="2">
                                                                    <span style="font-family: Microsoft Sans Serif">Product Group Name</span></td>
                                                                <td style="width: 192px;" >
                                                                    <asp:DropDownList ID="ddlGroupName" runat="server" CssClass="dropdownlist" Width="214px">
                                                                    </asp:DropDownList></td>
                                                                <td width="21%">
                                                                    </td>
                                                                <td width="30%" style="height: 22px">
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" AccessKey="A" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" colspan="2" style="height: 22px">
                                                                    Product Name</td>
                                                                <td class="textbold" style="width: 192px; height: 22px;">
                                                                   <asp:TextBox ID="txtProductName" runat ="server" CssClass ="textbox" Width="208px" MaxLength="30"></asp:TextBox></td>
                                                                <td style="height: 22px">
                                                                </td>
                                                                <td style="height: 22px">
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" AccessKey="N" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td colspan="2" class="textbold">
                                                             Provider CRS</td>
                                                                <td style="width: 192px">
                                                                    <asp:DropDownList ID="ddlCrs" runat="server" CssClass="dropdownlist" Width="214px">
                                                                    </asp:DropDownList></td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" AccessKey="R" /></td>
                                                            </tr>                                                           
                                                            <tr>
                                                                <td class="textbold">
                                                                    </td>
                                                                <td colspan="2" >
                                                                    </td>
                                                                <td style="width: 192px">
                                                                    </td>
                                                                <td>
                                                                    </td>
                                                                <td>
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                </td>
                                                                <td colspan="2" class="textbold">
                                                                    O.S</td>
                                                                <td style="width: 192px">
                                                                    <asp:DropDownList ID="ddlOS" runat="server" CssClass="dropdownlist" Width="214px">
                                                                    </asp:DropDownList></td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td colspan="6" height="4">
                                                                    <asp:GridView ID="gvProduct" runat="server"  AutoGenerateColumns="False" TabIndex="4" Width="100%"    >
                                                 <Columns>
                                               
                                                 <asp:BoundField DataField="GroupName" HeaderText="Group Name"   />
                                                  <asp:BoundField DataField="ProductName" HeaderText="Product Name"   />
                                                 <asp:BoundField DataField="Version" HeaderText="Version"   />
                                                            <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblAction" runat="server" Text="Action"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                           <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="EditX" CommandArgument='<%#Eval("ID")%>'
                                                                            CssClass="LinkButtons">
                                                                            Edit 
                                                                        </asp:LinkButton>
                                                                        &nbsp;
                                                                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName="DeleteX" CommandArgument='<%#Eval("ID")%>' 
                                                                            CssClass="LinkButtons">
                                                                            Delete
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="20%" CssClass="ItemColor" />
                                                                    <HeaderStyle Wrap="False" CssClass="Gridheading" />
                                                                </asp:TemplateField>
                                                 
                                                 
                                                 </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" />
                                                    
                                                 </asp:GridView>
                                                                    
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" >
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
        </table>
    <!-- Code by Rakesh -->
    
  
    </form>
</body>
</html>
