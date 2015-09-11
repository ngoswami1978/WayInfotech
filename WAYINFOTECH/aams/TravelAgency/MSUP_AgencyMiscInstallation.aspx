<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_AgencyMiscInstallation.aspx.vb" Inherits="TravelAgency_MSUP_AgencyMiscInstallation" %>

<%@ Register Src="../UserControl/MenuControl.ascx" TagName="MenuControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
       <title>Agency Miscellineous Installation</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
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
                                            Manage Agency
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
                                                    <td class="textbold" align="center" valign="TOP" rowspan="0" style="height: 23px">
                                                        <asp:Label ID="lblError" class="ErrorMsg" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="100%" valign="top">
                                                       <asp:Panel ID="pnlEmployee" runat="server" Width="100%">
                                                            <table width="100%" border="0" align="left" cellpadding="2" cellspacing="0">
                                                                <tr>
                                                                    <td class="textbold" style="width: 51px; height: 24px;">
                                                                        </td>
                                                                    <td class="textbold" style="width: 108px; height: 24px;">
                                                                        Date of Installation
                                                                    </td>
                                                                    <td style="width: 225px; height: 24px;">
                                                                        <asp:TextBox ID="txtInstallDate"  TabIndex="1" CssClass="textbox" runat="server" Width="136px"></asp:TextBox>
                                                                            <img id="imgDateOnline" alt="" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtInstallDate.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateOnline",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                        </script>
                                                                    </td>
                                                                    <td class="textbold" style="height: 24px; width: 108px;" nowrap="nowrap">
                                                                        Logged Date & Time</td>
                                                                    <td style="height: 24px; width: 177px;">
                                                                        <asp:TextBox ID="txtLoggedDtTime" runat="server" CssClass="textbox" Width="136px"></asp:TextBox>
                                                                     </td>                                                                                                                                       
                                                                    <td style="width: 171px; height: 24px;">
                                                                        <asp:Button ID="btnNew" runat="server" TabIndex="21" CssClass="button" Text="New" /></td>
                                                                    <td style="width: 13px; height: 24px;"></td>
                                                                    </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 51px; height: 19px;">
                                                                    </td>
                                                                    <td class="textbold" style="width: 108px; height: 19px;">
                                                                        Equipment No.
                                                                    </td>
                                                                    <td style="height: 19px; width: 225px;">
                                                                        <asp:TextBox ID="txtEquipNo" runat="server" CssClass="textbox"  TabIndex="3" Width="136px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="textbold" style="width: 108px; height: 19px;">
                                                                        Challen Number
                                                                    </td>
                                                                    
                                                                    <td style="height: 19px; width: 177px;">
                                                                        <asp:TextBox ID="txtChallNo" runat="server" CssClass="textbox" Width="136px"></asp:TextBox></td>                                                                                                                                       
                                                                    <td style="width: 171px; height: 19px;">
                                                                        <asp:Button ID="btnDeinstall" runat="server" TabIndex="20" CssClass="button" Text="Deinstall" /><td style="width: 13px; height: 19px;">
                                                                    </td>
                                                                   
                                                                
                                                                <tr>
                                                                    <td class="textbold" style="width: 51px; height: 17px;">
                                                                    </td>
                                                                    <td class="textbold" style="height: 17px; width: 108px;">
                                                                        Equipment Qty.</td>
                                                                    <td style="height: 17px; width: 225px;">
                                                                    <asp:TextBox ID="txtEquipQty" runat="server" Width="136px" CssClass="textbox" ></asp:TextBox>    
                                                                    </td>
                                                                    <td class="textbold" style="width: 108px; height: 17px;">
                                                                        Logged By.
                                                                    </td>
                                                                    <td style="height: 17px; width: 177px;">
                                                                        <asp:TextBox ID="txtLoggedBy" runat="server" CssClass="textbox" Width="136px"></asp:TextBox></td> 
                                                                    <td style="width: 171px; height: 17px;">
                                                                        <asp:Button ID="btnReplace" runat="server"  CssClass="button" Text="Replace" />
                                                                    </td>
                                                                    <td style="width: 13px; height: 17px;"></td>
                                                                </tr>
                                                                
                                                                
                                                                
                                                                 <tr>
                                                                    <td class="textbold" style="width: 51px; height: 14px;">
                                                                    </td>
                                                                    <td class="textbold" style="height: 14px; width: 108px;">
                                                                        Equipment Type</td>
                                                                    <td style="height: 14px; width: 225px;">
                                                                    <asp:DropDownList ID="drpEquipType" runat="server" Width="144px" CssClass="textbold">
                                                                    <asp:ListItem Text="--Select Equipment--" Selected=True ></asp:ListItem>
                                                                    </asp:DropDownList>    
                                                                    </td>
                                                                    <td class="textbold" style="width: 108px; height: 14px;">
                                                                       
                                                                    </td>
                                                                    <td style="height: 14px; width: 177px;">
                                                                        </td> 
                                                                    <td style="width: 171px; height: 14px;">
                                                                       
                                                                    </td>
                                                                    <td style="width: 13px; height: 14px;"></td>
                                                                </tr>
                                                                
                                                                <tr>
                                                                <td style="width: 51px"></td>
                                                                <td colspan="4">
                                                                <asp:GridView ID="gvMiscInstallation" runat="server"  EnableViewState ="false" AutoGenerateColumns="False" TabIndex="5" Width="100%">
                                                                         <Columns>                                               
                                                                         <asp:TemplateField HeaderText="Installation Date">
                                                                                 <ItemTemplate> 
                                                                                                                                                            
                                                                                 </ItemTemplate>
                                                                         </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="Equipment Date">
                                                                                 <ItemTemplate>                       
                                                                                 </ItemTemplate>
                                                                         </asp:TemplateField>                                                        
                                                                                    <asp:TemplateField HeaderText="Action">
                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                        <ItemTemplate>
                                                                                            <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a> 
                                                                                            <a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>
                                                                                                                                                                                                                                                                  
                                                                                        
                                                                                 
                                                                         </Columns>
                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                            <RowStyle CssClass="textbold" />
                                                                            <HeaderStyle CssClass="Gridheading" />
                                                                         </asp:GridView>
                                                                </td>
                                                                <td></td>
                                                                <td></td>
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
