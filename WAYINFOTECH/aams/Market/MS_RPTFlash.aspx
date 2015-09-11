<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MS_RPTFlash.aspx.vb" Inherits="Market_MS_RPTFlash" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AAMS::Inventory::Search Flash Report</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnFlash">
    <div>
    <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">Inventory-&gt;</span><span class="sub_menu">Flash Report Search</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Flash Report </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                        <tr>
                                            <td style="width: 150px">
                                            </td>
                                            <td class="gap" colspan="2" style="text-align: center">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False" Width="100%"></asp:Label></td>
                                            <td>
                                            </td>
                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 150px">
                                                                            </td>
                                                                            <td class="textbold" style="width: 15%">
                                                                                Month
                                                                            </td>
                                                                            <td style="width: 308px">
                                                                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="dropdownlist" Width="125px">
                                                                                <asp:ListItem Text="January" Value="1" ></asp:ListItem>
                                                                                <asp:ListItem Text="February" Value="2" ></asp:ListItem>
                                                                                <asp:ListItem Text="March" Value="3" ></asp:ListItem>
                                                                                <asp:ListItem Text="April" Value="4" ></asp:ListItem>
                                                                                <asp:ListItem Text="May" Value="5" ></asp:ListItem>
                                                                                <asp:ListItem Text="June" Value="6" ></asp:ListItem>
                                                                                <asp:ListItem Text="July" Value="7" ></asp:ListItem>
                                                                                <asp:ListItem Text="August" Value="8" ></asp:ListItem>
                                                                                <asp:ListItem Text="September" Value="9" ></asp:ListItem>
                                                                                <asp:ListItem Text="October" Value="10" ></asp:ListItem>
                                                                                <asp:ListItem Text="November" Value="11" ></asp:ListItem>
                                                                                <asp:ListItem Text="December" Value="12" ></asp:ListItem>
                                                                                 </asp:DropDownList></td>
                                                                            <td>
                                                                    <asp:Button ID="btnFlash" CssClass="button" runat="server" Text="Display Flash " TabIndex="3" Width="144px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 150px">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Year</td>
                                                                            <td style="width: 308px">
                                                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="dropdownlist" Width="125px">
                                                                                </asp:DropDownList></td>
                                                                            <td>
                                                                    <asp:Button ID="btnFlashSummary" CssClass="button" runat="server" Text="Display Flash Summary" TabIndex="4" Width="144px" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 150px;">
                                                                            </td>
                                                                            <td class="textbold" style="height: 26px">
                                                             </td>
                                                                            <td style="width: 308px; height: 26px;">
                                                                    </td>
                                                                            <td style="height: 26px">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="5" Width="144px"  AccessKey="R"/></td>
                                                                        </tr>
                                        <tr>
                                            <td style="width: 150px;">
                                            </td>
                                            <td class="textbold" style="height: 26px">
                                            </td>
                                            <td style="width: 308px; height: 26px">
                                            </td>
                                            <td style="height: 26px">
                                            </td>
                                        </tr>
                                       
                                                                          <tr>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4" style="text-align: center">
                                                                                &nbsp;</td>
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
    </div>
    </form>
     
</body>
</html>
