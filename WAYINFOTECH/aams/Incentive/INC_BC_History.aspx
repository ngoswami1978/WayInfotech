<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INC_BC_History.aspx.vb"
    Inherits="Incentive_INC_BC_History" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Business Case History</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="100%">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td align="right">
                                <a href="#" class="LinkButtons" onclick="window.close();">Close</a>&nbsp;&nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Business Case History</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td style="width: 2%">
                                                    </td>
                                                    <td style="width: 10%">
                                                    </td>
                                                    <td style="width: 20%" class="textbold">
                                                        Search by
                                                    </td>
                                                    <td class="textbold" style="width: 10%">
                                                        <asp:DropDownList ID="ddlSearch" runat="server" CssClass="textbold" onkeyup="gotop(this.id)"
                                                            TabIndex="1" Width="201px">
                                                            <asp:ListItem Selected="true" Text="All" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="CONNECTIVITY" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="HARDWARE" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="HEADER" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="BREAKUP" Value="4"></asp:ListItem>
                                                            <asp:ListItem Text="PLAN" Value="5"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 20%">
                                                    </td>
                                                    <td style="width: 38%">
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td style="width: 2%">
                                                    </td>
                                                    <td style="width: 20%">
                                                    </td>
                                                    <td class="textbold" style="width: 20%">
                                                    </td>
                                                    <td class="textbold" style="width: 10%">
                                                    </td>
                                                    <td style="width: 20%">
                                                    </td>
                                                    <td style="width: 28%">
                                                    </td>
                                                </tr>
                                                <tr id="trGroupName" runat="server">
                                                    <td style="width: 2%">
                                                    </td>
                                                    <td  class="textbold bold">
                                                        Group Name
                                                    </td>
                                                    <td class="textbold" style="width: 30%" colspan="2">
                                                        <asp:Label ID="lblGroupName" runat="server" CssClass="textbold"></asp:Label>
                                                    </td>
                                                    
                                                    <td style="width: 20%">
                                                    </td>
                                                    <td >
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="6">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" style="width: 100%">
                                                        <asp:GridView BorderWidth="0px" ID="gvBCHistory" runat="server" AutoGenerateColumns="false"
                                                            Width="100%" ShowHeader="false" ShowFooter="false">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <table cellpadding="1" cellspacing="0" border="0" width="100%">
                                                                            <tr>
                                                                                <td colspan="5" class="smallgap">
                                                                                    <hr style="color: Black; height: 1pt" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 2%">
                                                                                </td>
                                                                                <td class="textbold bold" style="width:20%">
                                                                                    Employee Name
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblEmployeeName" runat="server" CssClass="textbold"></asp:Label>
                                                                                </td>
                                                                                <td class="textbold bold"  >Changed DateTime
                                                                                </td>
                                                                                   
                                                                                <td> <asp:Label ID="lblChangedDateTime" runat="server" CssClass="textbold"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <%--<tr><td></td><td colspan="4" style="width:98%" class="bcHeading">
                                                                            Header
                                                                            </td></tr>--%>
                                                                            <tr id="trHeader" runat="server">
                                                                                <td style="width: 2%">
                                                                                </td>
                                                                                <td class="textbold  bold">
                                                                                    Header
                                                                                </td>
                                                                                <td colspan="3" style="width: 80%">
                                                                                    <asp:Label ID="lblHeader" runat="server" CssClass="textbold"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                           <tr id="trConnectivity" runat="server">
                                                                                <td style="width: 2%">
                                                                                </td>
                                                                                <td class="textbold  bold">
                                                                                    Connectivity
                                                                                </td>
                                                                                <td colspan="3" style="width: 80%">
                                                                                    <asp:Label ID="lblConnectivity" runat="server" CssClass="textbold"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                           <tr id="trHardware" runat="server">
                                                                                <td style="width: 2%">
                                                                                </td>
                                                                                <td class="textbold  bold">
                                                                                    Hardware
                                                                                </td>
                                                                                <td colspan="3" style="width: 80%">
                                                                                    <asp:Label ID="lblHardware" runat="server" CssClass="textbold"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="trBreakup" runat="server">
                                                                                <td style="width: 2%">
                                                                                </td>
                                                                                <td class="textbold  bold">
                                                                                    Breakup
                                                                                </td>
                                                                                <td colspan="3" style="width: 80%">
                                                                                    <asp:Label ID="lblBreakup" runat="server" CssClass="textbold"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="trSlabQualification" runat="server">
                                                                                <td style="width: 2%">
                                                                                </td>
                                                                                <td class="textbold  bold">
                                                                                    Slab Qualification
                                                                                </td>
                                                                                <td colspan="3" style="width: 80%">
                                                                                    <asp:Label ID="lblSlabQualification" runat="server" CssClass="textbold"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                             <tr id="trMinSegCriteria" runat="server">
                                                                                <td style="width: 2%">
                                                                                </td>
                                                                                <td class="textbold  bold">
                                                                                    Minimum Segment Criteria
                                                                                </td>
                                                                                <td colspan="3" style="width: 80%">
                                                                                    <asp:Label ID="lblMinSegCriteria" runat="server" CssClass="textbold"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="trPaymentType" runat="server">
                                                                                <td style="width: 2%">
                                                                                </td>
                                                                                <td class="textbold  bold">
                                                                                    Payment Type
                                                                                </td>
                                                                                <td colspan="3" style="width: 80%">
                                                                                    <asp:Label ID="lblPaymentType" runat="server" CssClass="textbold"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="5">
                                                                                    <asp:GridView BorderWidth="0px" ID="gvBCHistoryPlan" runat="server" AutoGenerateColumns="false"
                                                                                        Width="100%" ShowFooter="false">
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderStyle-ForeColor="white">
                                                                                                <HeaderTemplate>
                                                                                                    <table cellpadding="1" cellspacing="0" border="0" width="100%" class="Gridheading GridheadingText"
                                                                                                        style="color: white">
                                                                                                        <tr>
                                                                                                            <td class="GridheadingText  bold" style="width: 100%">
                                                                                                                Plan Details
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <table cellpadding="1" cellspacing="0" border="0" width="100%">
                                                                                                        <tr>
                                                                                                            <td class="textbold  bold" style="width: 26%">
                                                                                                                Case Name
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblCaseName" runat="server" CssClass="textbold"></asp:Label>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td colspan="2" style="width: 100%">
                                                                                                                <asp:CheckBoxList ID="chkSelectedCriteria" RepeatDirection="Horizontal" Width="600px"
                                                                                                                    RepeatColumns="4" runat="server">
                                                                                                                </asp:CheckBoxList>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td colspan="2" style="width: 100%">
                                                                                                                <asp:GridView ID="GvIncSlabsNested" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                    ShowHeader="true" Width="100%" EnableViewState="true" AllowSorting="false" ShowFooter="false">
                                                                                                                    <Columns>
                                                                                                                        <asp:BoundField HeaderText="Start" DataField="SLABS_START">
                                                                                                                            <HeaderStyle Width="35%" />
                                                                                                                            <ItemStyle Width="35%" CssClass="right" />
                                                                                                                        </asp:BoundField>
                                                                                                                        <asp:BoundField HeaderText="End" DataField="SLABS_END">
                                                                                                                            <HeaderStyle Width="35%" />
                                                                                                                            <ItemStyle Width="35%" Wrap="True" CssClass="right" />
                                                                                                                        </asp:BoundField>
                                                                                                                        <asp:BoundField HeaderText="Amount" DataField="SLABS_RATE">
                                                                                                                            <HeaderStyle Width="30%" />
                                                                                                                            <ItemStyle Width="30%" Wrap="True" CssClass="right" />
                                                                                                                        </asp:BoundField>
                                                                                                                    </Columns>
                                                                                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                    <RowStyle CssClass="textbold" />
                                                                                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                    <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                </asp:GridView>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            
                                                                              <tr>
                                                                                <td colspan="5">
                                                                                    <asp:GridView BorderWidth="0px" ID="gvPLBDetail" runat="server" AutoGenerateColumns="false"
                                                                                        Width="100%" ShowFooter="false">
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderStyle-ForeColor="white">
                                                                                                <HeaderTemplate>
                                                                                                    <table cellpadding="1" cellspacing="0" border="0" width="100%" class="Gridheading GridheadingText"
                                                                                                        style="color: white">
                                                                                                        <tr>
                                                                                                            <td class="GridheadingText  bold" style="width: 100%">
                                                                                                                PLB Details
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <table cellpadding="1" cellspacing="0" border="0" width="100%">
                                                                                                        <tr>
                                                                                                            <td class="textbold  bold" style="width: 26%">
                                                                                                               PLB Type
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblPLBType" runat="server" CssClass="textbold"></asp:Label>
                                                                                                            </td>
                                                                                                        </tr>                                                                                                      
                                                                                                        <tr id="trPLBBonusAmount" runat="server">
                                                                                                            <td class="textbold  bold" style="width: 26%">
                                                                                                               PLB Bonus Amount
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblPLBBonusAmount" runat="server" CssClass="textbold"></asp:Label>
                                                                                                            </td>
                                                                                                        </tr>                                                                                                      
                                                                                                        <tr>
                                                                                                            <td colspan="2" style="width: 100%">
                                                                                                                <asp:GridView ID="GvIncPLBNested" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                    ShowHeader="true" Width="100%" EnableViewState="true" AllowSorting="false" ShowFooter="false">
                                                                                                                    <Columns>
                                                                                                                        <asp:BoundField HeaderText="Start" DataField="SLABS_START">
                                                                                                                            <HeaderStyle Width="35%" />
                                                                                                                            <ItemStyle Width="35%" CssClass="right" />
                                                                                                                        </asp:BoundField>
                                                                                                                        <asp:BoundField HeaderText="End" DataField="SLABS_END">
                                                                                                                            <HeaderStyle Width="35%" />
                                                                                                                            <ItemStyle Width="35%" Wrap="True" CssClass="right" />
                                                                                                                        </asp:BoundField>
                                                                                                                        <asp:BoundField HeaderText="Amount" DataField="SLABS_RATE">
                                                                                                                            <HeaderStyle Width="30%" />
                                                                                                                            <ItemStyle Width="30%" Wrap="True" CssClass="right" />
                                                                                                                        </asp:BoundField>
                                                                                                                    </Columns>
                                                                                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                    <RowStyle CssClass="textbold" />
                                                                                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                    <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                </asp:GridView>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="5" class="smallgap">
                                                                                    <hr style="color: Black; height: 1pt" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <RowStyle CssClass="textbold" />
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                            <HeaderStyle CssClass="Gridheading" ForeColor="White" />
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
        </table>
    </form>
</body>
</html>
