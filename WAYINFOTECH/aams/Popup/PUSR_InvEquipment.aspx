<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PUSR_InvEquipment.aspx.vb" Inherits="Popup_PUSR_InvEquipment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title>AAMS:Inventory-Equipment</title>
    <base target="_self"/>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
    function ResetEquipment()
    {
    document.getElementById("txtEquipmentCode").value='';
    document.getElementById("txtEquipGroup").value='';
    document.getElementById("txtEquipmentDesc").value='';
    document.getElementById("txtEquipConfig").value='';
    document.getElementById("grdEquipment").style.display='none';
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="860px" align="left" class="border_rightred" style="height:486px;">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left">
                            <span class="menu">Inventory-></span><span class="sub_menu">Equipment</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Equipment
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                 
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="6" align="center" >
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" ></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="6%" class="textbold" height="30px">
                                                                    &nbsp;</td>
                                                                <td width="15%" class="textbold">
                                                                    Equipment Type</td>
                                                                <td style="width: 207px" >
                                                                    <asp:TextBox ID="txtEquipmentCode" CssClass="textbox" MaxLength="40" runat="server"></asp:TextBox></td>
                                                                <td style="width: 103px" >
                                                                    <span class="textbold">Equipment Description</span></td>
                                                                <td width="21%">
                                                                    <asp:TextBox ID="txtEquipmentDesc" runat="server" CssClass="textbox" MaxLength="40"></asp:TextBox></td>
                                                                <td width="18%">
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" /></td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td class="textbold" style="height: 25px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 25px">
                                                                    Equipment Group</td>
                                                                <td style="height: 25px; width: 207px;">
                                                                    <asp:TextBox ID="txtEquipGroup" runat="server" CssClass="textbox" MaxLength="40"></asp:TextBox></td>
                                                                <td class="textbold" nowrap="nowrap"  >
                                                                    Equipment Configuration</td>
                                                                <td style="height: 25px">
                                                                    <asp:TextBox ID="txtEquipConfig" runat="server" CssClass="textbox" MaxLength="40"></asp:TextBox></td>
                                                                <td style="height: 25px">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 23px">
                                                                    &nbsp;</td>
                                                               <td colspan="2" class="ErrorMsg">
                                                                    </td>
                                                                <td style="height: 23px; width: 103px;">
                                                                    &nbsp;</td>
                                                                <td style="height: 23px">
                                                                    &nbsp;</td>
                                                                <td style="height: 23px">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="4">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="4">
                                                                    <asp:DataGrid ID="grdEquipment" BorderWidth="1" BorderColor="#d4d0c8" runat="server"  AutoGenerateColumns="False" ItemStyle-CssClass="ItemColor" AlternatingItemStyle-CssClass="lightblue" HeaderStyle-CssClass="Gridheading" Width="100%">
                                                                        <Columns>
                                                                        
                                                                            <asp:TemplateColumn HeaderText="Equipment Code">
                                                                            <ItemTemplate>                                                                            
                                                                            <%#Eval("EQUIPMENT_CODE")%>
                                                                            </ItemTemplate>                                                                            
                                                                            </asp:TemplateColumn>
                                                                            
                                                                            <asp:TemplateColumn HeaderText="Equipment Group">
                                                                            <ItemTemplate>
                                                                            <%#Eval("EGROUP_CODE")%>
                                                                            </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            
                                                                            <asp:TemplateColumn HeaderText="Configuration">
                                                                            <ItemTemplate>
                                                                            <%#Eval("CONFIG")%>
                                                                            </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            
                                                                            <asp:TemplateColumn HeaderText="Description">
                                                                            <ItemTemplate>
                                                                            <%#Eval("DESCRIPTION")%>
                                                                            </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="Action">
                                                                            <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkSelect" CssClass="LinkButtons" Text="Select" runat="server" CommandName="SelectX" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EQUIPMENT_CODE")%>'></asp:LinkButton>&nbsp;                                                                            
                                                                            </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                        </Columns>
                                                                        <AlternatingItemStyle CssClass="lightblue" />
                                                                        <ItemStyle CssClass="ItemColor" />
                                                                        <HeaderStyle CssClass="Gridheading" />
                                                                    </asp:DataGrid></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="12">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                  
                                        </td>
                                    </tr>
                                </table>
                                <asp:Literal ID="litEmployee" runat="server"></asp:Literal></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
