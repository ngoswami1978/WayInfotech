<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_AgencyPcInstallation.aspx.vb" Inherits="TravelAgency_MSUP_AgencyPcInstallation" %>

<%@ Register Src="../UserControl/MenuControl.ascx" TagName="MenuControl" TagPrefix="uc1" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Agency PC Installation</title>
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
                                                    <td class="textbold" align="center" valign="TOP" rowspan="0" style="height: 22px">
                                                        <asp:Label ID="lblError" class="ErrorMsg" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="100%" valign="top">
                                                       <asp:Panel ID="pnlEmployee" runat="server" Width="100%">
                                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="textbold" style="width: 46px; height: 21px;">
                                                                        </td>
                                                                    <td class="textbold" style="width: 138px; height: 21px;">
                                                                        Date of Installation
                                                                    </td>
                                                                    <td style="width: 214px; height: 21px;">
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
                                                                    <td class="textbold" style="height: 21px; width: 141px;" nowrap="nowrap">
                                                                        Logged Date & Time</td>
                                                                    <td style="height: 21px; width: 175px;">
                                                                        <asp:TextBox ID="txtLoggedDtTime" runat="server" CssClass="textbox" Width="136px"></asp:TextBox>
                                                                     </td>                                                                                                                                       
                                                                    <td style="width: 171px; height: 21px;">
                                                                        <asp:Button ID="btnNew" runat="server" TabIndex="21" CssClass="button" Text="New" /></td>
                                                                    <td style="width: 13px; height: 21px;"></td>
                                                                    </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 46px">
                                                                    </td>
                                                                    <td class="textbold" style="width: 138px">
                                                                        CPU Number
                                                                    </td>
                                                                    <td style="height: 25px; width: 214px;">
                                                                        <asp:TextBox ID="txtCPUno" runat="server" CssClass="textbox"  TabIndex="3" Width="136px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="textbold" style="width: 141px">
                                                                        CPU Type
                                                                    </td>
                                                                    
                                                                    <td style="height: 25px; width: 175px;">
                                                                    <asp:DropDownList ID="DropDownList2" runat="server" Width="142px" CssClass="textbold" >
                                                                    <asp:ListItem Text="--Select CPU Type --" Selected="True" ></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    </td>                                                                                                                                       
                                                                    <td style="width: 171px">
                                                                        <asp:Button ID="btnDeinstall" runat="server" TabIndex="20" CssClass="button" Text="Deinstall" /><td style="width: 13px">
                                                                    </td>
                                                                   
                                                                
                                                                <tr>
                                                                    <td class="textbold" style="width: 46px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 25px; width: 138px;">
                                                                       Monitor Number</td>
                                                                    <td style="height: 25px; width: 214px;">
                                                                    <asp:TextBox ID="txtMontype" runat="server" Width="136px" CssClass="textbox" ></asp:TextBox>    
                                                                    </td>
                                                                    <td class="textbold" style="width: 141px">
                                                                       Monitor Type
                                                                    </td>
                                                                    <td style="height: 25px; width: 175px;">
                                                                        <asp:DropDownList ID="drpMonType" runat="server" Width="142px" CssClass="textbold" >
                                                                        <asp:ListItem Selected="True" Text="--Select Monitor Type--"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td> 
                                                                    <td style="width: 171px">
                                                                        <asp:Button ID="btnReplace" runat="server"  CssClass="button" Text="Replace" />
                                                                    </td>
                                                                    <td style="width: 13px"></td>
                                                                </tr>
                                                             
                                                              
                                                                <tr>
                                                                    <td class="textbold" style="width: 46px; height: 25px;">
                                                                    </td>
                                                                      <td class="textbold" style="height: 25px; width: 138px;">
                                                                        KeyBord Number</td>
                                                                      <td style="height: 25px; width: 214px;">
                                                                        <asp:TextBox ID="txtKBNo" runat="server" CssClass="textbox" Width="136px"></asp:TextBox>
                                                                      </td> 
                                                                        <td class="textbold" style="height: 25px; width: 141px;">
                                                                        KeyBord Type</td>
                                                                    <td style="height: 25px; width: 175px;">
                                                                        <asp:DropDownList ID='drplstKBType' runat="server" Width="142px" CssClass="textbold" >
                                                                        <asp:ListItem Text="-- KeyBord Type--" Selected="True"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                     </td>   
                                                                                                                                                                                                           
                                                                    <td style="width: 171px; height: 25px;"><asp:Button ID="btnHistory" runat="server" TabIndex="20" CssClass="button" Text="History" />
                                                                    </td>
                                                                    <td style="height: 25px"></td>
                                                                    
                                                                </tr>
                                                                
                                                                
                                                                 
                                                                <tr>
                                                                    <td class="textbold" style="height: 25px; width: 46px;">
                                                                    </td>
                                                                      <td class="textbold" style="height: 25px; width: 138px;">
                                                                        MSC Number
                                                                    </td>
                                                                    <td style="height: 25px; width: 214px;">
                                                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox" Width="136px"></asp:TextBox>
                                                                    </td> 
                                                                      <td class="textbold" style="height: 25px; width: 141px;">
                                                                        MSC Type</td>
                                                                    <td style="height: 25px; width: 175px;"><asp:DropDownList ID='DropDownList1' runat="server" CssClass="textbold" Width="142px" >
                                                                        <asp:ListItem Text="--Select MSC Type--"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    </td>                                                                                                                              
                                                                    <td style="height: 25px; width: 171px;">
                                                                    </td>
                                                                     <td style="height: 25px">
                                                                    </td>
                                                                </tr>
                                                                
                                                                                                                                  
                                                                <tr>
                                                                    <td class="textbold" style="height: 25px; width: 46px;">
                                                                    </td>
                                                                    <td class="textbold" style="height: 25px; width: 138px;">
                                                                        CDR Number
                                                                    </td>
                                                                    <td style="height: 25px; width: 214px;">
                                                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox" Width="136px"></asp:TextBox>
                                                                    </td>
                                                                      <td class="textbold" style="height: 25px; width: 141px;">
                                                                        Order Number</td>
                                                                    <td style="height: 25px; width: 175px;">
                                                                        <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox" Width="136px"></asp:TextBox>
                                                                     </td>                                                                                                                       
                                                                    <td style="height: 25px; width: 171px;">
                                                                    </td>
                                                                     <td style="height: 25px">
                                                                    </td>
                                                                </tr>
                                                                 
                                                                
                                                                 <tr>
                                                                    <td class="textbold" style="height: 25px; width: 46px;">
                                                                    </td>
                                                                       <td class="textbold" style="height: 25px; width: 138px;">
                                                                        Challen Number
                                                                    </td>
                                                                    <td style="height: 25px; width: 214px;">
                                                                        <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox" Width="136px"></asp:TextBox>
                                                                    </td>
                                                                      <td class="textbold" style="height: 25px; width: 141px;">
                                                                        Logged By.
                                                                    </td>
                                                                    <td style="height: 25px; width: 175px;">
                                                                        <asp:TextBox ID="txtLoggedBy" runat="server" CssClass="textbox" Width="136px"></asp:TextBox>
                                                                    </td>
                                                                                                                                                                                          
                                                                    <td style="height: 25px; width: 171px;">
                                                                    </td>
                                                                     <td style="height: 25px">
                                                                    </td>
                                                                </tr>
                                                                  <tr></tr>  
                                                                                                                                
                                                                 <tr>
                                                                    <td class="textbold" style="height: 37px; width: 46px;">
                                                                    </td>
                                                                    <td class="textbold" style="height: 37px; width: 138px;">
                                                                        Remarks</td>
                                                                    <td style="height: 27px; width: 149px;" colspan="3" rowspan="2">
                                                                        <asp:TextBox ID="TextBox5" runat="server" CssClass="textbox" TextMode="MultiLine" Height="24px" Width="464px"></asp:TextBox></td>                                                                                                                         
                                                                                                                                                                                            
                                                                    
                                                                    
                                                                    <td style="height: 37px; width: 171px;">
                                                                    </td>
                                                                    <td style="height: 37px">
                                                                    </td>                                                                   
                                                                </tr>
                                                                
                                                                        <tr>  
                                                                <td style="height: 4px; width: 46px;"></td>
                                                                <td style="height: 4px; width: 138px;"></td>
                                                                <td style="height: 4px"></td>
                                                                <td style="height: 4px"></td>
                                                                <td style="height: 4px"></td>
                                                                
                                                                
                                                                        </tr>
                                                                
                                                                
                                                                
                                                                <tr>
                                                                <td style="width: 46px"></td>
                                                                <td colspan="4">
                                                                <asp:GridView ID="gvOrderStatus" runat="server"  EnableViewState ="false" AutoGenerateColumns="False" TabIndex="5" Width="100%">
                                                                         <Columns>                                               
                                                                         <asp:TemplateField HeaderText="Installation Date">
                                                                                 <ItemTemplate> 
                                                                                                                                                            
                                                                                 </ItemTemplate>
                                                                         </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="CPU Type">
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
