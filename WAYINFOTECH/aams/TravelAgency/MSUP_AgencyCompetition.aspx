<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_AgencyCompetition.aspx.vb" Inherits="TravelAgency_MSUP_AgencyCompetition" %>
<%@ Register Src="../UserControl/MenuControl.ascx" TagName="MenuControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Agency</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />        
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />      
</head>
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
<body>
    <form id="form1" runat="server">
        <table width="860px" align="left" style="height: 486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">Setup-></span><span class="sub_menu">Agency</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#1A61A9" class="heading" align="center">
                                            Manage Agency Competition
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td valign="top">
                                            <uc1:MenuControl ID="MenuControl1" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" class="redborder">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" align="center" valign="TOP" rowspan="0">
                                                        <asp:Label ID="lblError" class="ErrorMsg" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="100%" valign="top">
                                                        <asp:Panel ID="pnlEmployee" runat="server" Width="100%">
                                                            <table width="100%" border="0" align="left" cellpadding="2" cellspacing="0">
                                                            <tr>
                                                            <td colspan="5" style="height:25px"></td>
                                                            </tr>
                                                             <tr>
                                                                    <td class="textbold" style="width: 5%">
                                                                    </td>
                                                                    <td class="textbold" style="width: 19%">
                                                                        CRS Code<span class="Mandatory">*</span></td>
                                                                    <td style="width: 20%"><asp:DropDownList ID="drpCRSCode" CssClass="dropdown" runat="server">
                                                                    </asp:DropDownList></td>
                                                                    <td class="textbold" style="width: 20%">
                                                                        Online Status</td>
                                                                    <td style="width: 20%"><asp:DropDownList ID="drpOnlineStatus" CssClass="dropdown" runat="server">
                                                                    </asp:DropDownList></td>
                                                                    <td style="width: 23%"><asp:Button ID="btnAdd" runat="server" TabIndex="22" CssClass="button" Text="Add" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Date Start</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDateStart" CssClass="textbox" runat="server"></asp:TextBox>
                                                                        <img id="imgDateStart" alt="" src="../Images/calender.gif" TabIndex="16" title="Date selector" style="cursor: pointer" />

                                                                                                 <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateStart.ClientID%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateStart",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                                                  </td>
                                                                    <td class="textbold">
                                                                        Date End</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDateEnd" CssClass="textbox" runat="server"></asp:TextBox>
                                                                        <img id="imgtxtDateEnd" alt="" src="../Images/calender.gif" TabIndex="16" title="Date selector" style="cursor: pointer" />

                                                                                                 <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateEnd.ClientID%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgtxtDateEnd",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                        </td>
                                                                    <td>
                                                                        <asp:Button ID="btnSave" runat="server" TabIndex="20" CssClass="button" Text="Save" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Dial Backup</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="drpDialBackup" CssClass="dropdown" runat="server">
                                                                        <asp:ListItem>False</asp:ListItem>
                                                                            <asp:ListItem>True</asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold">
                                                                        Sole User</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="drpSoleUser" CssClass="dropdown" runat="server">
                                                                        <asp:ListItem>False</asp:ListItem>
                                                                            <asp:ListItem>True</asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                                    <td>
                                                                        <asp:Button ID="btnReset" runat="server" TabIndex="22" CssClass="button" Text="Reset" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        PC Count</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPCCount" CssClass="textbox" runat="server"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        Printer Count</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPrinterCount" CssClass="textbox" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:Button ID="btnHistory" runat="server" TabIndex="21" CssClass="button" Text="History" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                     <td colspan="4">
                                                                        &nbsp;&nbsp;&nbsp;<asp:DataGrid ID="grdComptitionAgency" runat="server" AlternatingItemStyle-CssClass="lightblue"
                                                                             AutoGenerateColumns="False" BorderColor="#d4d0c8" BorderWidth="1" HeaderStyle-CssClass="Gridheading"
                                                                             ItemStyle-CssClass="ItemColor" Width="100%">
                                                                             <Columns>
                                                                                 <asp:TemplateColumn HeaderText="CRS Code">
                                                                                     <ItemTemplate>
                                                                                         <asp:Label ID="lblLocationCode" runat="server" Text='<%#Eval("CRSID")%>'></asp:Label>
                                                                                         <asp:HiddenField ID="hdComptID" runat="server" Value='<%#Eval("ComptID")%>' />
                                                                                     </ItemTemplate>
                                                                                     <ItemStyle HorizontalAlign="Left" />
                                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                                 </asp:TemplateColumn>
                                                                                 <asp:TemplateColumn HeaderText="online Status">
                                                                                     <ItemTemplate>
                                                                                         <%#Eval("ONLINESTATUSID")%>
                                                                                     </ItemTemplate>
                                                                                     <ItemStyle HorizontalAlign="Left" />
                                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                                 </asp:TemplateColumn>
                                                                                 <asp:TemplateColumn HeaderText="Date Start">
                                                                                     <ItemTemplate>
                                                                                         <%#Eval("DATE_START")%>
                                                                                     </ItemTemplate>
                                                                                     <ItemStyle HorizontalAlign="Left" />
                                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                                 </asp:TemplateColumn>
                                                                                  <asp:TemplateColumn HeaderText="Date End">
                                                                                     <ItemTemplate>
                                                                                         <%#Eval("DATE_END")%>
                                                                                     </ItemTemplate>
                                                                                     <ItemStyle HorizontalAlign="Left" />
                                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                                 </asp:TemplateColumn>
                                                                                 <asp:TemplateColumn HeaderText="Dial Backup">
                                                                                     <ItemTemplate>
                                                                                         <%#Eval("DIAL_BACKUP")%>
                                                                                     </ItemTemplate>
                                                                                     <ItemStyle HorizontalAlign="Left" />
                                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                                 </asp:TemplateColumn>
                                                                                  <asp:TemplateColumn HeaderText="Sole User">
                                                                                     <ItemTemplate>
                                                                                         <%#Eval("SOLE_USER")%>
                                                                                     </ItemTemplate>
                                                                                     <ItemStyle HorizontalAlign="Left" />
                                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                                 </asp:TemplateColumn>
                                                                                 <asp:TemplateColumn HeaderText="PC Count">
                                                                                     <ItemTemplate>
                                                                                         <%#Eval("PC_COUNT")%>
                                                                                     </ItemTemplate>
                                                                                     <ItemStyle HorizontalAlign="Left" />
                                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                                 </asp:TemplateColumn>
                                                                                  <asp:TemplateColumn HeaderText="Printer Count">
                                                                                     <ItemTemplate>
                                                                                         <%#Eval("PRINTER_COUNT")%>
                                                                                     </ItemTemplate>
                                                                                     <ItemStyle HorizontalAlign="Left" />
                                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                                 </asp:TemplateColumn>
                                                                                 <asp:TemplateColumn HeaderText="Action">
                                                                                     <ItemTemplate>
                                                                                         <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("ComptID")%>' CommandName="EditX"
                                                                                             CssClass="LinkButtons" Text="Edit"></asp:LinkButton>
                                                                                         <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("ComptID")%>' CommandName="DeleteX"
                                                                                             CssClass="LinkButtons" Text="Delete"></asp:LinkButton>
                                                                                     </ItemTemplate>
                                                                                 </asp:TemplateColumn>
                                                                             </Columns>
                                                                             <AlternatingItemStyle CssClass="lightblue" />
                                                                             <ItemStyle CssClass="textbold" />
                                                                             <HeaderStyle CssClass="Gridheading" />
                                                                         </asp:DataGrid>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                        &nbsp;</td>
                                                                    <td class="textbold">
                                                                        </td>
                                                                    <td>
                                                                        </td>
                                                                    <td class="textbold">
                                                                        </td>
                                                                    <td>
                                                                        </td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                        &nbsp;</td>
                                                                    <td colspan="4" class="ErrorMsg">
                                                                        Field Marked * are Mandatory</td>
                                                                    <td>
                                                                        &nbsp;</td>
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
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
