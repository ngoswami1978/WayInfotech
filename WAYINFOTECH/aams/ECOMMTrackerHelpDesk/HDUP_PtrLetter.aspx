<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDUP_PtrLetter.aspx.vb" Inherits="ETHelpDesk_HDUP_PtrLetter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
 <title>HelpDesk: PTR Letter</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
   </head>
<script type="text/javascript" src="../JavaScript/ETracker.js"></script>
<!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
<body style="font-size: 12pt; font-family: Times New Roman">
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
                                            <span class="menu">ETrackers HelpDesk-&gt;</span><span class="sub_menu">Order</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#1A61A9" class="heading" align="center">
                                            Manage Order
                                        </td>
                                    </tr>
                                </table>                              
                            </td>
                        </tr>                       
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                         
                                      <tr>
                                        <td valign="top" class="redborder">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">                                              
                                                <tr>
                                                    <td class="textbold" align="center" valign="TOP" rowspan="0"><asp:Label ID="lblError" class="ErrorMsg" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td width="100%" valign="top">
                                                        <asp:Panel ID="pnlEmployee" runat="server" Width="100%">
                                                            <table width="100%" border="0" align="left" cellpadding="2" cellspacing="0">
                                                               
                                                               
                                                                
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Logged Date</td>
                                                                    <td >
                                                                     <asp:TextBox ID="txtLoggedDt" runat="server" ></asp:TextBox>
                                                                     <img id="imgReceivedFrom" alt="" src="../Images/calender.gif" TabIndex="16" title="Date selector" style="cursor: pointer" />
                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtLoggedDt.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgApprovalFrom",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                       </td>
                                                                       <td >
                                                                     Logged By
                                                                       </td>
                                                                       <td >
                                                                     <asp:TextBox ID="txtLoggedBy" runat="server" ></asp:TextBox>
                                                                       </td>
                                                                       
                                                                    <td style="width: 187px">
                                                                        <asp:Button ID="btnSave" runat="server" TabIndex="20" CssClass="button" Text="Save" OnClientClick="return OrderPage()" /></td>
                                                                </tr>
                                                               
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Entry1</td>
                                                                    <td colspan="3">
                                                                     <asp:TextBox ID="txtEntry1" runat="server" Width="472px" CssClass="textbox" Height="16px" TextMode="MultiLine" ></asp:TextBox>
                                                                       </td>
                                                                       
                                                                       
                                                                    <td style="width: 187px">
                                                                        <asp:Button ID="Button1" runat="server" TabIndex="20" CssClass="button" Text="Print" /></td>
                                                                </tr>
                                                                
                                                                
                                                                 <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Entry2</td>
                                                                    <td colspan="3">
                                                                     <asp:TextBox ID="txtEntry2" runat="server" Width="472px" CssClass="textbox" TextMode="MultiLine" ></asp:TextBox>
                                                                       </td>
                                                                       <td style="width: 187px">
                                                                        <asp:Button ID="Button2" runat="server" TabIndex="20" CssClass="button" Text="Close" /></td>
                                                                </tr>
                                                                
                                                                 <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Entry3</td>
                                                                    <td colspan="3">
                                                                     <asp:TextBox ID="txtEntry3" runat="server" Width="472px" CssClass="textbox" TextMode="MultiLine" ></asp:TextBox>
                                                                       </td>
                                                                      <td style="width: 187px">
                                                                        </td>
                                                                </tr>
                                                                
                                                                 <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold" valign="top" >
                                                                        Letter</td>
                                                                    <td colspan="3">
                                                                     <asp:TextBox ID="txtLetter" runat="server" Width="472px" CssClass="textbox" Height="304px" TextMode="MultiLine" ></asp:TextBox>
                                                                       </td>
                                                                      <td style="width: 187px">
                                                                       </td>
                                                                </tr>
                                                                
                                                                
                                                                
                                                                <tr>
                                                                    <td class="textbold" style="height: 11px">
                                                                        &nbsp;</td>
                                                                    <td colspan="4" class="ErrorMsg" style="height: 11px">
                                                                        Field Marked * are Mandatory</td>
                                                                    <td style="height: 11px; width: 187px;">
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
