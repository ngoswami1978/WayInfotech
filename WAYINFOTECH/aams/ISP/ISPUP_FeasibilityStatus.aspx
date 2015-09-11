<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ISPUP_FeasibilityStatus.aspx.vb"
    Inherits="ISP_ISPUP_FeasibilityStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript"></script>

</head>
<script language="javascript" type="text/javascript">
 function NewFunction()
    {   
        window.location.href="ISPUP_FeasibilityStatus.aspx?Action=I";       
        return false;
    }
</script>
<body>
    <form id="form1" defaultbutton="btnSave"  defaultfocus="txtFeasibilityStatus" runat="server">
        <table width="860px" align="left" style="height: 486px;" class="border_rightred">
            <tr>
                <td valign="top" style="height: 191px">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">ISP-></span><span class="sub_menu">Feasibility Status</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Update Feasibility Status</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%" class="textbold" style="height: 25px;">
                                                        &nbsp;</td>
                                                    <td width="10%" nowrap="nowrap" class="textbold" style="height: 25px">
                                                        Feasibility Status</td>
                                                    <td width="30%" style="height: 25px">
                                                        <asp:TextBox ID="txtFeasibilityStatus" MaxLength="15" CssClass="textbox" runat="server"
                                                            TabIndex="1" Width="256px"></asp:TextBox></td>
                                                    <td width="20%" style="height: 25px">
                                                        <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="2" AccessKey="S" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 25px;">
                                                    </td>
                                                    <td class="textbold" style="height: 25px">
                                                    </td>
                                                    <td style="height: 25px">
                                                    </td>
                                                    <td style="height: 25px">
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" OnClientClick="javascript:NewFunction();"
                                                            TabIndex="3" AccessKey="N" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 25px;">
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="4" AccessKey="R" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 19px;">
                                                    </td>
                                                    <td style="height: 19px">
                                                    </td>
                                                    <td style="height: 19px">
                                                    </td>
                                                    <td style="height: 19px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" height="12">
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
