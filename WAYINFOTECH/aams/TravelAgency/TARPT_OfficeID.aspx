<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TARPT_OfficeID.aspx.vb" Inherits="TravelAgency_TARPT_OfficeID" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

 <html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title>AAMS: Office ID Summary</title>
     <base target="_self" />
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
  
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
  <script type="text/javascript" language="javascript">
  
</script>
<body>
    <form id="form1"  defaultbutton="btnDisplay" runat="server">
        <table width="860px" align="left" class="border_rightred" style="height: 486px;">
            <tr>
                <td valign="top" style="height: 482px">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Travel Agency-></span><span class="sub_menu">Office ID Summary</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                 Office Id Summary</td>
                        </tr>
                        <tr>
                            <td valign="top" style="height: 314px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" style="height: 15px">
                                                        &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                
                                               
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP" style="height: 7px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px; width: 227px;">
                                                    </td>
                                                    <td class="textbold" style="width: 305px; height: 25px;" align="left">
                                                        City</td>
                                                    <td class="textbold" style="width: 200px; height: 25px;">
                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpCity" runat="server" TabIndex="2" Width="165px" CssClass="textbold">
                                                        </asp:DropDownList></td>
                                                    <td class="textbold" style="width: 74px; height: 25px;">
                                                    </td>
                                                    <td style="height: 25px"><asp:Button ID="btnDisplay" CssClass="button" runat="server" TabIndex="4" Text="Display"  AccessKey="D" />
                                                    </td>
                                                    <td style="height :25px;width:50px">
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP" style="height: 7px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 227px;">
                                                    </td>
                                                    <td class="textbold" style="width: 305px ">
                                                        Summary Type</td>
                                                    <td class="textbold" style="width: 200px;">
                                                    <asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlSummaryType" runat="server" TabIndex="2" Width="165px" CssClass="textbold">
                                                        <asp:ListItem Selected="True" Value="2">--All--</asp:ListItem>
                                                        <asp:ListItem Value="1">Allocated OfficeID</asp:ListItem>
                                                        <asp:ListItem Value="0">UnAllocated OfficeID</asp:ListItem>
                                                    </asp:DropDownList>
                                                        </td>
                                                    <td class="textbold" style="width: 94px;">
                                                        </td>
                                                    <td > <asp:Button ID="btnReset" CssClass="button" runat="server" TabIndex="5" Text="Reset"  AccessKey="R" />
                                                    </td>
                                                    <td >
                                                       </td>
                                                </tr>
                                                 <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP" style="height: 7px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 227px; height: 25px">
                                                    </td>
                                                    <td style="width: 305px; height: 25px;">
                                                    </td>
                                                    <td class="textbold" style="width: 200px; height: 25px;">
                                                        <asp:CheckBox ID="chkallocatedid" class="textbold" runat="server" Text="Allocated OfficeId"
                                                            TabIndex="3" Width="168px" Checked="True" CssClass="displayNone" Visible="False" /></td>
                                                    <td class="textbold" style="width: 94px; height: 25px;">
                                                    </td>
                                                    <td style="height: 25px">
                                                    </td>
                                                    <td style="height: 25px">
                                                        &nbsp;</td>
                                                </tr>
                                             
                                                   <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP" style="height: 7px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 227px; height: 25px">
                                                    </td>
                                                    <td style="width: 305px; height: 25px">
                                                    </td>
                                                    <td class="textbold" style="width: 200px; height: 25px">
                                                    </td>
                                                    <td class="textbold" style="width: 94px; height: 25px">
                                                    </td>
                                                    <td style="height: 25px">
                                                    </td>
                                                    <td style="height: 25px">
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 227px; height: 25px">
                                                    </td>
                                                    <td style="width: 305px; height: 25px">
                                                    </td>
                                                    <td class="textbold" style="width: 200px; height: 25px">
                                                    </td>
                                                    <td class="textbold" style="width: 94px; height: 25px">
                                                    </td>
                                                    <td style="height: 25px">
                                                    </td>
                                                    <td style="height: 25px">
                                                    </td>
                                                </tr>
                                                   <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP" style="height: 7px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" class="textbold" colspan="6" valign="top">
                                                     <asp:HiddenField ID="hdCity" runat="server" />
                                                      <asp:HiddenField ID="hdCityText" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" style="height: 4px">
                                                    </td>
                                                </tr>
                                                 
                                               
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Literal ID="litOfficeId" runat="server"></asp:Literal></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
