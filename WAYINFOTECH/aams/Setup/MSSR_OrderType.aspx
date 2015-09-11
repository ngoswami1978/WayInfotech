<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_OrderType.aspx.vb" Inherits="Setup_MSSR_OrderType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Order Type</title>
     <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">  
    </script>
    </head>
<body>
    <form id="FrmOrderType" runat="server">
       <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Setup-></span><span class="sub_menu">Order Type</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Order Type 
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="4" align="center" height="25px" valign="TOP">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 21px; width: 131px;">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 21px; width: 121px;">
                                                        Order Type Category</td>
                                                    <td  style="height: 21px; width: 380px;">
                                                        <asp:TextBox ID="txtOrderTypeCat" CssClass="textbox" runat="server" MaxLength="100" Width="224px" Wrap="False"></asp:TextBox></td>
                                                    <td  style="height: 21px">
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search"  AccessKey="A"/></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 131px; height: 22px;">
                                                    </td>
                                                    <td class="textbold" style="width: 121px; height: 22px;">
                                                        Order Type</td>
                                                    <td style="width: 380px; height: 22px;">
                                                    <asp:TextBox ID="txtOrderType" CssClass="textbox" runat="server" MaxLength="100" Width="224px" Wrap="False"></asp:TextBox>
                                                    </td>
                                                    <td style="height: 22px">
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" AccessKey="N" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 131px">
                                                        &nbsp;</td>
                                                    <td style="width: 121px">
                                                        &nbsp;</td>
                                                    <td style="width: 380px">
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset"  AccessKey="R"/></td>
                                                </tr>
                                                <tr>
                                                    <td height="20px" style="width: 131px">
                                                    </td>
                                                    <td style="width: 121px">
                                                    </td>
                                                    <td style="width: 380px">
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <asp:GridView ID="grdOrderType" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="50%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Order Type Category">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                    <%--   <%#Eval("OrdTypeCat")%>--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Order Type">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                    <%--   <%#Eval("OrdTypeCat")%>--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Time Required">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                    <%--   <%#Eval("OrdTypeCat")%>--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton CssClass="LinkButtons" ID="btnEdit" CausesValidation="false" runat="server"
                                                                            CommandName="EditX">Edit
                                                                        </asp:LinkButton>
                                                                        &nbsp;&nbsp;
                                                                        <asp:LinkButton CssClass="LinkButtons" ID="btnDelete" CausesValidation="false" runat="server"
                                                                            CommandName="DeleteX">
                                                                                        Delete
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="left" />
                                                            <RowStyle CssClass="textbold" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" height="12">
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
