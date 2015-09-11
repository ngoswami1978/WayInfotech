<%@ page language="VB" autoeventwireup="false" inherits="Setup_MSRPT_Employee, App_Web_msrpt_employee.aspx.4cd3357d" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AAMS: Employee List</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />    
     <script src="../JavaScript/WAY.js" type="text/javascript"></script>
</head>
<body >
    <form id="form1" runat="server"  defaultbutton ="btnSearch" defaultfocus ="txtEmployeeName" >
        <table width="860px" align="left" class="border_rightred" style="height:486px;">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left">
                            <span class="menu">Setup-></span><span class="sub_menu">Employee List</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Employee List</td>
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
                                                                    Employee Name</td>
                                                                <td width="30%" >
                                                                    <asp:TextBox ID="txtEmployeeName" CssClass="textbox" MaxLength="40" runat="server" TabIndex="1"></asp:TextBox></td>
                                                                <td width="12%" >
                                                                    <span class="textbold">Department</span></td>
                                                                <td width="21%">
                                                                    <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpDepartment" CssClass="dropdownlist" Width="137px" runat="server" TabIndex="2">
                                                                    </asp:DropDownList></td>
                                                                <td width="18%">
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Display" TabIndex="7" AccessKey="A" /></td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td class="textbold" height="25px">
                                                                    &nbsp;</td>
                                                                <td class="textbold">
                                                                    Aoffice</td>
                                                                <td>
                                                                    <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpAoffice" CssClass="dropdownlist" Width="137px" runat="server" TabIndex="3">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" nowrap="nowrap" >
                                                                    Permission</td>
                                                                <td>
                                                                    <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drplstPermission" CssClass="dropdownlist" runat="server" Width="136px" TabIndex="4" ></asp:DropDownList>
                                                                    </td>
                                                                <td>
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="9"  AccessKey="R"/></td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td class="textbold" height="30px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" nowrap="nowrap">
                                                                    Designation</td>
                                                                <td class="textbold">
                                                                    <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drplstDeig" CssClass="dropdownlist" runat="server" Width="136px" TabIndex="5" ></asp:DropDownList>
                                                                    </td>
                                                                <td nowrap="nowrap" class="textbold">
                                                                    Agreement Signed &nbsp;&nbsp;
                                                                </td>
                                                                <td>
                                                                <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpAgreementSigned" CssClass="dropdownlist" runat="server" Width="136px" TabIndex="6" >
                                                               <asp:ListItem Text="Agreement Signed" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Agreement Not Signed" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="--All--" Value="3" Selected="True" ></asp:ListItem>
                                                                </asp:DropDownList>
                                                                
                                                                </td>
                                                                <td>
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 23px">
                                                                    &nbsp;</td>
                                                               <td colspan="2" class="ErrorMsg">
                                                                    </td>
                                                                <td style="height: 23px">
                                                                    &nbsp;</td>
                                                                <td style="height: 23px">
                                                                    &nbsp;</td>
                                                                <td style="height: 23px">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td colspan="2" >
                                                                    </td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="4">
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
