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
<body bgcolor="ffffff" >
    <form id="frmCity" runat="server"  defaultfocus="ddlGroupName">
    
     <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">TravelAgency-&gt;</span><span class="sub_menu">Products</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage <span style="font-family: Microsoft Sans Serif">Product </span></td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">   <table style="width: 100%" border="0" cellpadding="1" cellspacing="2" class="left textbold">
                                            <tr>
                                                <td style="width: 10%">
                                                </td>
                                                <td class="textbold" style="width: 10%">
                                                </td>
                                                <td colspan="4" class="gap">
                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                <td style="width: 10%">
                                                </td>
                                                <td style="width: 34%">
                                                </td>
                                            </tr>
                                    <tr>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 20%" class="textbold">
                                            Group Name<span class="Mandatory" >*</span></td>
                                        <td colspan="4">
                                                                    <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlGroupName" runat="server" CssClass="dropdownlist" Width="100%" TabIndex="2">
                                                                    </asp:DropDownList></td>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 34%">
                                                                    <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" OnClientClick="return ProductPage()" TabIndex="3" AccessKey="S" /></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 10%" class="textbold">
                                            Name<span class="Mandatory" >*</span></td>
                                        <td colspan="4" class="textbold">
                                                                   <asp:TextBox ID="txtProductName" runat ="server" CssClass ="textbox" Width="98.75%" MaxLength="49" TabIndex="2"></asp:TextBox></td>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 34%">
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="3" AccessKey="N" /></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 10%" class="textbold">
                                            Version <span class="Mandatory" >*</span></td>
                                        <td style="width: 25%" class="textbold">
                                                                    <asp:TextBox ID="txtVersion" runat="server" CssClass="textbox" MaxLength="10" Width="90%" TabIndex="2"></asp:TextBox></td>
                                        <td class="textbold" style="width: 5%">
                                        </td>
                                        <td style="width: 15%" class="textbold">
                                                                    Edition <span class="Mandatory" >*</span></td>
                                        <td style="width: 15%">
                                                                    <asp:TextBox ID="txtEdition" runat="server" CssClass="textbox" MaxLength="30" Width="96.5%" TabIndex="2"></asp:TextBox></td>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 34%">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3" AccessKey="R" /></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 10%" class="textbold">
                                            CRS <span class="Mandatory" >*</span></td>
                                        <td colspan="4">
                                                                    <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlCrs" runat="server" CssClass="dropdownlist" Width="100%" TabIndex="2">
                                                                    </asp:DropDownList></td>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 34%">
                                                                    </td>
                                    </tr>
                                            <tr>
                                                <td class="gap" colspan="6">
                                                </td>
                                                <td style="width: 10%">
                                                </td>
                                                <td style="width: 34%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%">
                                                </td>
                                                <td class="subheading" colspan="6">
                                                    Pricing</td>
                                                <td style="width: 34%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="gap" colspan="6">
                                                </td>
                                                <td style="width: 34%">
                                                </td>
                                                <td style="width: 34%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%">
                                                </td>
                                                <td class="textbold" style="width: 10%">
                                                        List Price</td>
                                                <td>
                                                        <asp:TextBox ID="txtListPrice" runat="server" CssClass="textbox" MaxLength="5" Width="90%" onkeyup="checknumeric(this.id)" TabIndex="2"></asp:TextBox></td>
                                                <td class="textbold" style="width: 5%">
                                                </td>
                                                <td class="textbold" style="width: 10%">
                                                        Segment Req</td>
                                                <td style="width: 34%">
                                                        <asp:TextBox ID="txtSegmentReq" runat="server" CssClass="textbox" MaxLength="4" Width="96.5%" onkeyup="checknumeric(this.id)" TabIndex="2"></asp:TextBox></td>
                                                <td style="width: 34%">
                                                </td>
                                                <td style="width: 34%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%">
                                                </td>
                                                <td class="textbold" style="width: 10%">
                                                </td>
                                                <td colspan="4">
                                                        <asp:CheckBox ID="chbInsReq" runat="server" CssClass="textbold" Text="Segments  req per installation" Width="100%" TabIndex="2" /></td>
                                                <td style="width: 34%">
                                                </td>
                                                <td style="width: 34%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="gap" colspan="6">
                                                </td>
                                                <td style="width: 34%">
                                                </td>
                                                <td style="width: 34%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%">
                                                </td>
                                                <td class="subheading" colspan="6">
                                                    Requirements</td>
                                                <td style="width: 34%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="gap" colspan="6">
                                                </td>
                                                <td style="width: 34%">
                                                </td>
                                                <td style="width: 34%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%">
                                                </td>
                                                <td class="textbold" style="width: 10%">
                                                        O.S <span class="Mandatory" >*</span></td>
                                                <td>
                                                                    <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlOS" runat="server" CssClass="dropdownlist" Width="93%" TabIndex="2">
                                                                    </asp:DropDownList></td>
                                                <td style="width: 5%">
                                                </td>
                                                <td style="width: 10%">
                                                        Ram</td>
                                                <td style="width: 34%">
                                                        <asp:TextBox ID="txtRam" runat="server" CssClass="textbox" MaxLength="5" Width="80%" onkeyup="checknumeric(this.id)" TabIndex="2"></asp:TextBox>MB</td>
                                                <td style="width: 34%">
                                                </td>
                                                <td style="width: 34%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%">
                                                </td>
                                                <td class="textbold" style="width: 10%">
                                                        HDD</td>
                                                <td>
                                                        <asp:TextBox ID="txtHdd" runat="server" CssClass="textbox" MaxLength="5" Width="80%" onkeyup="checknumeric(this.id)" TabIndex="2"></asp:TextBox>MB</td>
                                                <td style="width: 5%">
                                                </td>
                                                <td style="width: 10%" class="textbold">
                                                        CPU</td>
                                                <td style="width: 34%">
                                                        <asp:TextBox ID="txtCpu" runat="server" CssClass="textbox" MaxLength="4" Width="80%" onkeyup="checknumeric(this.id)" TabIndex="2"></asp:TextBox>Mhz</td>
                                                <td style="width: 34%">
                                                </td>
                                                <td style="width: 34%">
                                                </td>
                                            </tr>
                                    <tr>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 10%" class="textbold">
                                            Remarks</td>
                                        <td colspan="4">
                                            <asp:TextBox ID="txtremarks" runat="server" CssClass="textbox" Rows="5" TextMode="MultiLine"
                                                Width="98%" Height="60px" MaxLength="99" TabIndex="2"></asp:TextBox></td>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 34%">
                                        </td>
                                    </tr>
                                            <tr>
                                                <td class="gap" colspan="6">
                                                </td>
                                                <td style="width: 10%">
                                                </td>
                                                <td style="width: 34%">
                                                </td>
                                            </tr>
                                    <tr>
                                        <td style="width: 10%">
                                        </td>
                                        <td class="ErrorMsg" colspan="5">
                                            Field Marked * are Mandatory</td>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 34%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 5%">
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
