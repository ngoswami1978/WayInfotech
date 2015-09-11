<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ISRPT_ISPPlan.aspx.vb" Inherits="ISP_ISRPT_ISPPlan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <title>Search ISP Plan</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
     
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" defaultbutton ="btnReportPrint" defaultfocus ="drpISPProvider">
    <table width="860px" align="left" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">ISP-&gt;</span><span class="sub_menu">ISP Plan Information Report</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                 ISP<span style="font-family: Microsoft Sans Serif">&nbsp;Plan Information Report</span></td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%">
                                                                        <tr>
                                                                            <td colspan="4" class="center gap">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 175px">
                                                                            </td>
                                                                            <td class="textbold" style="width: 103px">
                                                                                Provider Name</td>
                                                                            <td style="width: 298px" ><asp:DropDownList ID="drpISPProvider" runat="server" CssClass="dropdownlist" Width="214px" TabIndex="1">
                                                                            </asp:DropDownList>&nbsp;</td>
                                                                            <td>
                                                                    <asp:Button ID="btnReportPrint" CssClass="button" runat="server" Text="Print" TabIndex="4" AccessKey="P" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 175px; height: 26px;">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                City Name</td>
                                                                            <td style="width: 298px">
                                                                    <asp:DropDownList ID="drpCityName" runat="server" CssClass="dropdownlist" Width="214px" TabIndex="2">
                                                                    </asp:DropDownList></td>
                                                                            <td style="height: 26px">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="5" AccessKey="R" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 175px">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                NPID</td>
                                                                            <td style="width: 298px" >
                                                                                <asp:TextBox ID="txtNpid" runat="server" CssClass="textbold" Width="208px" MaxLength="20" TabIndex="3"></asp:TextBox></td>
                                                                            <td>
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
    </form>
</body>
</html>
