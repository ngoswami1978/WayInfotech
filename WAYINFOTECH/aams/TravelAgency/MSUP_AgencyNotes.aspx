<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_AgencyNotes.aspx.vb" Inherits="TravelAgency_MSUP_AgencyNotes" %>

<%@ Register Src="../UserControl/MenuControl.ascx" TagName="MenuControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Agency Notes</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
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
                                                    <td class="textbold" align="center" valign="TOP" rowspan="0" style="height: 25px">
                                                        <asp:Label ID="lblError" class="ErrorMsg" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="100%" valign="top">
                                                       
 <asp:Panel ID="pnlEmployee" runat="server" Width="100%">
                                                            <table width="100%" border="0" align="left" cellpadding="2" cellspacing="0">
                                                                
                                                                 <tr>
                                                                <td style="height: 52px; width: 58px;"></td>
                                                                <td style="height: 52px; width: 567px;">
                                                                <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Height="48px" Width="560px" CssClass="textbox" ></asp:TextBox>
                                                                     </td>
                                                                     <td style="height: 52px; width: 68px;">
                                                                         <br />
                                                                         <br />
                                                                     <asp:Button ID="txtSave" runat="server" TabIndex="21" CssClass="button" Text="Save" Width="72px" />
                                                                     </td>
                                                                <td style="height: 52px; width: 24px;"></td>
                                                                </tr>        
                                                               
                                                               
                                                               
                                                                <tr>
                                                                <td style="height: 118px; width: 58px;"></td>
                                                                <td colspan="4" style="height: 118px; width: 655px;">
                                                                <asp:GridView ID="gvMiscInstallation" runat="server"  EnableViewState ="false" AutoGenerateColumns="False" TabIndex="5" Width="98%" Height="96px">
                                                                     <Columns>                                               
                                                                         <asp:TemplateField HeaderText="User Name">
                                                                                 <ItemTemplate> 
                                                                                                                                                            
                                                                                 </ItemTemplate>
                                                                         </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="Date Time">
                                                                                 <ItemTemplate>                       
                                                                                 </ItemTemplate>
                                                                         </asp:TemplateField>    
                                                                          <asp:TemplateField HeaderText="Notes">
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
                                                                <td style="height: 118px"></td>
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
