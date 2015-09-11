<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_AgencyCRS.aspx.vb" Inherits="TravelAgency_MSUP_AgencyCRS" EnableEventValidation="false" %>

<%@ Register Src="../UserControl/MenuControl.ascx" TagName="MenuControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Agency</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />   
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
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
                                            Manage Agency CRS</td>
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
                                                            <td colspan="6" style="height:25px"></td>
                                                            </tr>
                                                            <tr>
                                                                    <td class="textbold" style="width: 5%">
                                                                    </td>
                                                                    <td class="textbold" style="width: 17%">
                                                                        <span class="Mandatory"></span></td>
                                                                    <td style="width: 18%" class="textbold">
                                                                        CRS Code</td>
                                                                    <td class="textbold" style="width: 27%">
                                                                        <asp:DropDownList ID="drpCRS" CssClass="dropdown" runat="server">
                                                                        </asp:DropDownList></td>
                                                                    <td style="width: 20%"><asp:Button ID="btnAdd" runat="server" TabIndex="20" CssClass="button" Text="Add" /></td>
                                                                    <td style="width: 20%">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" >
                                                                    </td>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td  class="textbold">
                                                                        Office Id</td>
                                                                    <td class="textbold" >
                                                                        <asp:TextBox ID="txtOfficeId" CssClass="textbox" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:Button ID="btnSave" runat="server" TabIndex="20" CssClass="button" Text="Save" /></td>
                                                                    <td >
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Current Id</td>
                                                                    <td class="textbold">
                                                                        <asp:DropDownList ID="drpCurrentID" CssClass="dropdown" runat="server">
                                                                            <asp:ListItem>False</asp:ListItem>
                                                                            <asp:ListItem>True</asp:ListItem>                                                                            
                                                                        </asp:DropDownList></td>
                                                                    <td>
                                                                        <asp:Button ID="btnReset" runat="server" TabIndex="22" CssClass="button" Text="Reset" /></td>
                                                                    <td>
                                                                        &nbsp;</td>
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
                                                                        <asp:Button ID="btnHistory" runat="server" TabIndex="22" CssClass="button" Text="History" /></td>
                                                                    <td>
                                                                    </td>
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
                                                                    <td class="textbold" colspan="4">
                                                                        <asp:DataGrid ID="grdCRSAgency" runat="server" AlternatingItemStyle-CssClass="lightblue"
                                                                            AutoGenerateColumns="False" BorderColor="#d4d0c8" BorderWidth="1" HeaderStyle-CssClass="Gridheading"
                                                                            ItemStyle-CssClass="ItemColor" Width="100%">
                                                                            <Columns>
                                                                                <asp:TemplateColumn HeaderText="CRS Code">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblLocationCode" runat="server" Text='<%#Eval("CRS")%>'></asp:Label>
                                                                                        <asp:HiddenField ID="hdRN" runat="server" value='<%#Eval("RN")%>' />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="Office ID">
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("OFFICEID")%>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="Current ID">
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("CURRENTID")%>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateColumn>                                                                             
                                                                               
                                                                                <asp:TemplateColumn HeaderText="Action">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkEdit" CssClass="LinkButtons" runat="server" CommandName="EditX" CommandArgument='<%#Eval("RN")%>' Text="Edit"></asp:LinkButton>
                                                                                        <asp:LinkButton ID="lnkDelete" CssClass="LinkButtons" runat="server" CommandName="DeleteX" CommandArgument='<%#Eval("RN")%>' Text="Delete"></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                            </Columns>
                                                                            <AlternatingItemStyle CssClass="lightblue" />
                                                                            <ItemStyle CssClass="textbold" />
                                                                            <HeaderStyle CssClass="Gridheading" />
                                                                        </asp:DataGrid></td>
                                                                    <td >
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td colspan="4">
                                                                    </td>
                                                                    <td>
                                                                    </td>
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
