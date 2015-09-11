<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TASR_Training.aspx.vb" Inherits="TravelAgency_TASR_Training" %>

<%@ Register Src="../UserControl/MenuControl.ascx" TagName="MenuControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS::Agency::Manage User Name</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" defaultfocus="txtUserName" defaultbutton="btnSave">
        <!-- Code by Rakesh -->
        <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Agency-&gt;</span><span class="sub_menu">Training Search</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Training</td>
                        </tr>
                        <tr>
                            <td valign="top" style="height: 277px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <uc1:MenuControl ID="MenuControl1" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="redborder center">
                                            <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                <tr>
                                                    <td style="width: 176px">
                                                    </td>
                                                    <td class="gap" colspan="2" style="text-align: center">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 176px">
                                                    </td>
                                                    <td class="textbold">
                                                        User Name <span class="Mandatory">*</span></td>
                                                    <td style="width: 308px">
                                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="textbold" Width="208px" MaxLength="25"
                                                            TabIndex="2"></asp:TextBox></td>
                                                    <td>
                                                        <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="4"
                                                            OnClientClick="return ValidateAgencyTrainingPage()" AccessKey="S" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 176px">
                                                        <input id="hdID" runat="server" style="width: 1px" type="hidden" /></td>
                                                    <td class="textbold">
                                                        Password <span class="Mandatory">*</span></td>
                                                    <td style="width: 308px">
                                                        <asp:TextBox ID="txtPassword" runat="server" CssClass="textbold" MaxLength="25" TabIndex="2"
                                                            Width="208px"></asp:TextBox></td>
                                                    <td>
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="5"
                                                            AccessKey="R" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 176px; height: 26px;">
                                                        <input id="hdLCode" runat="server" style="width: 1px" type="hidden" />&nbsp;
                                                    </td>
                                                    <td class="textbold">
                                                    Training Status
                                                    </td>
                                                    <td style="width: 308px; height: 26px;">                                                            
                                                            <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlTrnStatus" runat="server"  CssClass="dropdownlist" Width="50%" TabIndex="3">
                                                                    </asp:DropDownList>
                                                    </td>
                                                    <td style="height: 26px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 176px;">
                                                    </td>
                                                    <td class="ErrorMsg" colspan="3">
                                                        Field Marked * are Mandatory</td>
                                                </tr>
                                                <tr>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="text-align: center; height: 21px;">
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
    </form>
</body>
</html>
