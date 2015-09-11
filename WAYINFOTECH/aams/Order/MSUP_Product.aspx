<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_Product.aspx.vb" Inherits="Order_MSUP_Product" %>
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
    <form id="frmCity" runat="server"  defaultfocus="txtCtyName">
    
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
                                Manage <span style="font-family: Microsoft Sans Serif">Product </span></td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">   <table style="width: 100%" border="0" cellpadding="1" cellspacing="2">
                                    <tr>
                                        <td style="width: 13%">
                                        </td>
                                        <td style="width: 10%" class="textbold">
                                            Group Name</td>
                                        <td colspan="3">
                                                                    <asp:DropDownList ID="ddlGroupName" runat="server" CssClass="dropdownlist" Width="214px">
                                                                    </asp:DropDownList></td>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 34%">
                                                                    <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" AccessKey="S" /></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 13%">
                                        </td>
                                        <td style="width: 10%" class="textbold">
                                            Name</td>
                                        <td colspan="3" class="textbold">
                                                                   <asp:TextBox ID="txtProductName" runat ="server" CssClass ="textbox" Width="208px" MaxLength="30"></asp:TextBox></td>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 34%">
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New"  AccessKey="N"/></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 13%">
                                        </td>
                                        <td style="width: 10%" class="textbold">
                                            Version</td>
                                        <td style="width: 10%" class="textbold">
                                                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox" MaxLength="30" Width="50px"></asp:TextBox></td>
                                        <td style="width: 8%" class="textbold">
                                                                    Edition</td>
                                        <td style="width: 15%">
                                                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox" MaxLength="30" Width="55px"></asp:TextBox></td>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 34%">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" AccessKey="R" /></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 13%">
                                        </td>
                                        <td style="width: 10%" class="textbold">
                                            CRS</td>
                                        <td colspan="3">
                                                                    <asp:DropDownList ID="ddlCrs" runat="server" CssClass="dropdownlist" Width="214px">
                                                                    </asp:DropDownList></td>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 34%">
                                                                    </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 13%">
                                        </td>
                                        <td colspan="3" style="vertical-align: text-top">
                                        <fieldset style="width: 82%; height: 115px;">
                                        <legend class="textbold">Pricing</legend>
                                        
                                            <table style="width: 100%">
                                                <tr>
                                                    <td class="textbold">
                                                        List Price</td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox" MaxLength="30" Width="50px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold">
                                                        Segment Req</td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox" MaxLength="30" Width="50px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 10pt">
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:CheckBox ID="CheckBox1" runat="server" CssClass="textbold" Text="Segments  req per installation" Width="100%" /></td>
                                                </tr>
                                            </table>
                                            </fieldset>
                                        </td>
                                        <td colspan="2">
                                         <fieldset style="width: 82%; height: 110px;">
                                        <legend class="textbold">Requirements</legend>
                                        
                                            <table style="width: 100%">
                                                <tr>
                                                    <td class="textbold">
                                                        O.S</td>
                                                    <td>
                                                                    <asp:DropDownList ID="ddlOS" runat="server" CssClass="dropdownlist" Width="100px">
                                                                    </asp:DropDownList></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold">
                                                        Ram</td>
                                                    <td class="textbold">
                                                        <asp:TextBox ID="TextBox6" runat="server" CssClass="textbox" MaxLength="30" Width="75px"></asp:TextBox>MB</td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold">
                                                        HDD</td>
                                                    <td class="textbold">
                                                        <asp:TextBox ID="TextBox5" runat="server" CssClass="textbox" MaxLength="30" Width="75px"></asp:TextBox>MB</td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold">
                                                        CPU</td>
                                                    <td class="textbold">
                                                        <asp:TextBox ID="TextBox7" runat="server" CssClass="textbox" MaxLength="30" Width="75px"></asp:TextBox>Mhz</td>
                                                </tr>
                                            </table>
                                            </fieldset>
                                        </td>
                                        <td style="width: 34%">
                                                                    </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 13%">
                                        </td>
                                        <td style="width: 10%" class="textbold">
                                            Remarks</td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtremarks" runat="server" CssClass="textbox" Rows="5" TextMode="MultiLine"
                                                Width="208px" Height="60px"></asp:TextBox></td>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 34%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 13%">
                                        </td>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 8%">
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 34%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 13%">
                                        </td>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 8%">
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 34%">
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
