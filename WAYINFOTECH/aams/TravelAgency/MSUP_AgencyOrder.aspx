<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_AgencyOrder.aspx.vb" Inherits="TravelAgency_MSUP_AgencyOrder" %>
<%@ Register Src="../UserControl/MenuControl.ascx" TagName="MenuControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Agency</title>
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
<script language="javascript" type="text/javascript">
function PopupAgencyGroup()
{
    var type;
  type = "../Popup/PUSR_AgencyGroup.aspx" 
  var strReturn;   
  if (window.showModalDialog)
  {
      strReturn=window.showModalDialog(type,null,'dialogWidth:880px;dialogHeight:600px;help:no;');       
  }
  else
  {
      strReturn=window.open(type,null,'height=600,width=880,top=30,left=20,scrollbars=1');       
  }	   
  if (strReturn != null)
  {
      var sPos = strReturn.split('|'); 
      
      document.getElementById('<%=hdChainId.ClientID%>').value=sPos[0];
      document.getElementById('<%=txtAgencyGroup.ClientID%>').value=sPos[1];
  }
}

</script>
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
                                            Manage Agency Order&nbsp;</td>
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
                                                                    <td class="textbold">
                                                                        </td>
                                                                    <td class="textbold">
                                                                        </td>
                                                                    <td colspan="3">
                                                                        
                                                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" CssClass="dropdown" RepeatDirection="Horizontal"
                                                                            Width="500px">
                                                                            <asp:ListItem>New Order</asp:ListItem>
                                                                            <asp:ListItem>Cancellation</asp:ListItem>
                                                                        </asp:RadioButtonList></td>
                                                                    <td>
                                                                        <asp:Button ID="btnSave" runat="server" TabIndex="20" CssClass="button" Text="Save" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 5%">
                                                                    </td>
                                                                    <td class="textbold" style="width: 19%">
                                                                        Order Number</td>
                                                                    <td style="width: 20%">
                                                                        <asp:TextBox ID="TextBox18" runat="server" CssClass="textbox" MaxLength="30" TabIndex="5"></asp:TextBox></td>
                                                                    <td class="textbold" style="width: 20%">
                                                                        Order Status</td>
                                                                    <td style="width: 20%"><asp:DropDownList ID="DropDownList2" runat="server" TabIndex="4" Width="137px" CssClass="textbold">
                                                                    </asp:DropDownList></td>
                                                                    <td style="width: 23%">
                                                                        <asp:Button ID="btnNew" runat="server" TabIndex="21" CssClass="button" Text="New" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Order
                                                                        Type</td>
                                                                    <td><asp:DropDownList ID="DropDownList3" runat="server" TabIndex="4" Width="137px" CssClass="textbold">
                                                                    </asp:DropDownList></td>
                                                                    <td class="textbold">
                                                                        ISP Name</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox5" runat="server" CssClass="textbox" MaxLength="30" TabIndex="5"></asp:TextBox>
                                                                        <img src="../Images/lookup.gif" alt="Select & Add Agency Group." onclick="javascript:return PopupAgencyGroup();" /></td>
                                                                    <td>
                                                                        <asp:Button ID="btnReset" runat="server" TabIndex="22" CssClass="button" Text="Reset" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Plan ID</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEmail" CssClass="textbox" MaxLength="100" TabIndex="7" runat="server"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        Pending With</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox" MaxLength="30" ReadOnly="True"
                                                                            TabIndex="5"></asp:TextBox>
                                                                        <img src="../Images/lookup.gif" alt="Select & Add Agency Group." onclick="javascript:return PopupAgencyGroup();" /></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Processed By</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtLogin" MaxLength="20" CssClass="textbox" TabIndex="9" runat="server"></asp:TextBox></td>
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
                                                                    <td class="subheading" colspan="4">
                                                                        Agency Detail</td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Name&nbsp;</td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtAgencyGroup" MaxLength="15" TabIndex="13"
                                                                            CssClass="textbox" runat="server" Width="468px"></asp:TextBox>
                                                                            <img src="../Images/lookup.gif" alt="Select & Add Agency Group." onclick="javascript:return PopupAgencyGroup();" />
                                                                        <asp:HiddenField ID="hdChainId" runat="server" />
                                                                            </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Address</td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox" MaxLength="15" TabIndex="13"
                                                                            TextMode="Password" Width="468px"></asp:TextBox></td>
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
                                                                    <td class="subheading" colspan="4">
                                                                        Dates</td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Processed</td>
                                                                    <td class="textbold">
                                                                        <asp:TextBox ID="TextBox13" runat="server" CssClass="textbox" MaxLength="15" TabIndex="11"> </asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        </td>
                                                                    <td class="textbold">
                                                                        </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Approval</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPrimaryDate" runat="server" CssClass="textbox" MaxLength="15"
                                                                            TabIndex="11"></asp:TextBox>
                                                                        <img id="imgPrimaryDate" alt="" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" /></td>
                                                                    <td class="textbold">

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtPrimaryDate.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgPrimaryDate",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                        </script>

                                                                        Applied</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox" MaxLength="15" TabIndex="11"></asp:TextBox>
                                                                        <img id="Img1" alt="" src="../Images/calender.gif" tabindex="16" title="Date selector"
                                                                            style="cursor: pointer" /></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Message Sent</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtBackupDate" runat="server" CssClass="textbox" MaxLength="15"
                                                                            TabIndex="11"></asp:TextBox>
                                                                        <img id="Img2" alt="" src="../Images/calender.gif" tabindex="16" title="Date selector"
                                                                            style="cursor: pointer" /></td>
                                                                    <td class="textbold">

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtBackupDate.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgBackupDate",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                        </script>

                                                                        Received</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox14" runat="server" CssClass="textbox" MaxLength="15" TabIndex="11"></asp:TextBox>
                                                                        <img id="Img3" alt="" src="../Images/calender.gif" tabindex="16" title="Date selector"
                                                                            style="cursor: pointer" /></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Exp.
                                                                        Installation</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox" MaxLength="15" TabIndex="11"></asp:TextBox>
                                                                        <img id="Img4" alt="" src="../Images/calender.gif" tabindex="16" title="Date selector"
                                                                            style="cursor: pointer" /></td>
                                                                    <td class="textbold">
                                                                        Sent Back</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox6" runat="server" CssClass="textbox" MaxLength="15" TabIndex="11"></asp:TextBox>
                                                                        <img id="Img5" alt="" src="../Images/calender.gif" tabindex="16" title="Date selector"
                                                                            style="cursor: pointer" /></td>
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
                                                                    <td class="subheading" colspan="4">
                                                                        For Marketing Department</td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Receiving Date</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox7" runat="server" CssClass="textbox" MaxLength="20" TabIndex="9"></asp:TextBox>
                                                                        <img id="Img6" alt="" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" /></td>
                                                                    <td class="textbold">
                                                                        Resending Date</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox8" runat="server" CssClass="textbox" MaxLength="20" TabIndex="9"></asp:TextBox>
                                                                        <img id="Img7" alt="" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" /></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Office ID 1</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox9" runat="server" CssClass="textbox" MaxLength="20" TabIndex="9"></asp:TextBox>
                                                                        <img src="../Images/lookup.gif" alt="Select & Add Agency Group." onclick="javascript:return PopupAgencyGroup();" /></td>
                                                                    <td class="textbold">
                                                                        Office ID 2</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox10" runat="server" CssClass="textbox" MaxLength="20" TabIndex="9"></asp:TextBox>
                                                                        <img src="../Images/lookup.gif" alt="Select & Add Agency Group." onclick="javascript:return PopupAgencyGroup();" /></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Receiving Office ID</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox11" runat="server" CssClass="textbox" MaxLength="20" TabIndex="9"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        Agency PC Req.</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox12" runat="server" CssClass="textbox" MaxLength="20" TabIndex="9"></asp:TextBox></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Amadeus PC Req</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox15" runat="server" CssClass="textbox" MaxLength="20" TabIndex="9"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        Amadeus Printer Req.</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox16" runat="server" CssClass="textbox" MaxLength="20" TabIndex="9"></asp:TextBox></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Remarks</td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="TextBox17" runat="server" CssClass="textbox" MaxLength="20" TabIndex="9"></asp:TextBox></td>
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
