<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INCRPT_VolumeBased.aspx.vb"
   MaintainScrollPositionOnPostback="true"   Inherits="Incentive_INCRPT_VolumeBased" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Incentive::Volume Based Report</title>
</head>
<body>
    <form id="form1" runat="server" defaultbutton ="btnDisplayReport">
        <table style="width: 845px" align="left" class="border_rightred">
            <tr>
                <td valign="top" style="width: 860px;">
                    <table width="100%">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Incentive-&gt;</span><span class="sub_menu"> Volume Based Report</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" style="height: 20px; width:850px;">
                                &nbsp;Volume Based Report</td>
                        </tr>
                        <tr>
                            <td valign="top" align="LEFT">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="left" class="redborder" style="width: 860px">
                                            <table style="width: 850px" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="7" align="center" style="height: 25px; width:840px;" valign="TOP">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                       <td colspan ="6" style="width: 840px">
                                                           <table style="width: 840px" border="0" cellpadding="0" cellspacing="0">
                                                                       <tr>
                                                    <td style="width: 15%;" class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 6%;" class="textbold">
                                                        <span class="textbold">Month</span></td>
                                                    <td style="width: 22%;" class="textbold">
                                                        <asp:DropDownList ID="drpMonths" runat="server" CssClass="dropdownlist" Width="158px"
                                                            TabIndex="1">
                                                        </asp:DropDownList></td>
                                                    <td style="width: 6%;">
                                                        <span class="textbold">Year</span></td>
                                                    <td style="width: 20%;" class="textbold">
                                                        <asp:DropDownList ID="drpYears" runat="server" CssClass="dropdownlist" Width="158px"
                                                            TabIndex="1">
                                                        </asp:DropDownList></td>
                                                    <td style="width: 5%;" class="textbold">
                                                    </td>
                                                    <td style="width: 20%;">
                                                        <asp:Button ID="btnDisplayReport" CssClass="button" runat="server" Text="Seach" TabIndex="3"
                                                            AccessKey="S" /></td>
                                                            
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="7" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%; height: 19px;" class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 6%; height: 19px;" class="textbold" nowrap="nowrap">
                                                        <span class="textbold"></span>
                                                    </td>
                                                    <td style="width: 22%; height: 19px;" class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 6%; height: 19px;">
                                                        <span class="textbold"></span>
                                                    </td>
                                                    <td style="width: 20%; height: 19px;" class="textbold">
                                                    </td>
                                                    <td style="width: 5%; height: 19px;" class="textbold">
                                                    </td>
                                                    <td style="width: 20%; height: 19px;">
                                                        <asp:Button ID="BtnExport" CssClass="button" runat="server" Text="Export" TabIndex="17"
                                                            AccessKey="E" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="7" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 15%; height: 19px">
                                                    </td>
                                                    <td class="textbold" nowrap="nowrap" style="width: 6%; height: 19px">
                                                    </td>
                                                    <td class="textbold" style="width: 22%; height: 19px">
                                                    </td>
                                                    <td style="width: 6%; height: 19px">
                                                    </td>
                                                    <td class="textbold" style="width: 20%; height: 19px">
                                                    </td>
                                                    <td class="textbold" style="width: 5%; height: 19px">
                                                    </td>
                                                    <td style="width: 20%; height: 19px">
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="17"
                                                            AccessKey="r" /></td>
                                                </tr>
                                                <tr height="3px">
                                                </tr>
                                                <tr>
                                                    <td style="height: 15px" class="textbold" colspan="7" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="7" align="center" valign="TOP" style="height: 10px;">
                                                        &nbsp;</td>
                                                        
                                                </tr>
                                                           </table>
                                                       </td>
                                                       <td></td>
                                                 </tr> 
                                                <tr>
                                                    <td align="center" valign="top" style="width: 1000px;" colspan="7">
                                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 1000px">
                                                            <tr>
                                                                <td style="width: 1000px;" valign="top">
                                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 1000px">
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold" colspan="6" style="width: 1060px; " valign="top">
                                                                                <asp:GridView ID="GvIncVol" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                    Width="1060px" EnableViewState="true" AllowSorting="True" ShowFooter="true">
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="Type " DataField="Type" SortExpression="Type" HeaderStyle-Width="50px">
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="A" DataField="A" SortExpression="A" HeaderStyle-Width="80px">
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="B" DataField="B" SortExpression="B" HeaderStyle-Width="80px">
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="G" DataField="G" SortExpression="G" HeaderStyle-Width="80px">
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="P" DataField="P" SortExpression="P" HeaderStyle-Width="80px">
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="W" DataField="W" SortExpression="W" HeaderStyle-Width="90px"
                                                                                            ItemStyle-Wrap="true"></asp:BoundField>
                                                                                        <asp:BoundField HeaderText="Total" DataField="Total" SortExpression="Total" HeaderStyle-Width="100px"
                                                                                            ItemStyle-Wrap="true"></asp:BoundField>
                                                                                      <asp:BoundField HeaderText="Percentage (%) " DataField="Per" SortExpression="Per" HeaderStyle-Width="110px"
                                                                                            ItemStyle-Wrap="true"></asp:BoundField>
                                                                                        <asp:BoundField HeaderText="No. of Location" DataField="No_of_Locations" SortExpression="No_of_Locations"
                                                                                            HeaderStyle-Width="110px" ItemStyle-Wrap="true"></asp:BoundField>
                                                                                        <asp:BoundField HeaderText="MNC" DataField="MNC" SortExpression="MNC" HeaderStyle-Width="70px"
                                                                                            ItemStyle-Wrap="true"></asp:BoundField>
                                                                                        <asp:BoundField HeaderText="Sole" DataField="Sole" SortExpression="Sole" HeaderStyle-Width="70px"
                                                                                            ItemStyle-Wrap="true"></asp:BoundField>
                                                                                        <asp:BoundField HeaderText="Multi" DataField="Multi" SortExpression="Multi" HeaderStyle-Width="70px"
                                                                                            ItemStyle-Wrap="true"></asp:BoundField>
                                                                                        <asp:BoundField HeaderText="Non 1A" DataField="Non1A" SortExpression="Non1A" HeaderStyle-Width="70px"
                                                                                            ItemStyle-Wrap="true"></asp:BoundField>
                                                                                    </Columns>
                                                                                    <AlternatingRowStyle CssClass="lightblue" HorizontalAlign="left" />
                                                                                    <RowStyle CssClass="textbold" HorizontalAlign="left" />
                                                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White" HorizontalAlign="left" />
                                                                                    <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" width="860px;">
                                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                            <tr class="paddingtop paddingbottom">
                                                                                <td style="width: 30%" class="left">
                                                                                    <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                                        ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey"
                                                                                        ReadOnly="True"></asp:TextBox></td>
                                                                                <td style="width: 25%" class="right">
                                                                                    <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                                <td style="width: 20%" class="center">
                                                                                    <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber"
                                                                                        Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                                                    </asp:DropDownList></td>
                                                                                <td style="width: 25%" class="left">
                                                                                    <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textbold" align="center" valign="TOP" style="height: 10px;">
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
