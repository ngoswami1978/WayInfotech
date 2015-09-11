<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PUSR_Employee.aspx.vb" Inherits="Popup_PUSR_Employee" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>WAY: User</title>
    <base target="_self"/>
    
    <link href="../CSS/WAY.css" rel="stylesheet" type="text/css" />
</head>
<body >
    <form id="form1" runat="server"  defaultbutton ="btnSearch">
        <table width="860px" align="left" class="border_rightred" style="height:486px;">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left">
                            <span class="menu">Setup-></span><span class="sub_menu">User</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search User
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
                                                                    User Name</td>
                                                                <td width="30%" >
                                                                    <asp:TextBox ID="txtEmployeeName" CssClass="textbox" MaxLength="40" runat="server"></asp:TextBox></td>
                                                                <td style="width: 103px" >
                                                                    <span class="textbold">Department</span></td>
                                                                <td width="21%">
                                                                    <asp:DropDownList ID="drpDepartment" CssClass="dropdownlist" Width="137px" runat="server">
                                                                    </asp:DropDownList></td>
                                                                <td width="18%">
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" /></td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td class="textbold" style="height: 25px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 25px">
                                                                    Aoffice</td>
                                                                <td style="height: 25px">
                                                                    <asp:DropDownList ID="drpAoffice" CssClass="dropdownlist" Width="137px" runat="server">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" nowrap="nowrap"  >
                                                                    Permission</td>
                                                                <td style="height: 25px">
                                                                    <asp:DropDownList ID="drplstPermission" runat="server" Width="136px">
                                                                    </asp:DropDownList></td>
                                                                <td style="height: 25px">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" /></td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td class="textbold" height="30px">
                                                                    &nbsp;</td>
                                                                <td class="textbold">
                                                                    Designation</td>
                                                                <td>
                                                                    <asp:DropDownList ID="drplstDeig" runat="server" Width="136px">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" nowrap="nowrap"  >
                                                                    Agreement Signed
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkAgmntSinged" runat="server" /></td>
                                                                <td>
                                                                    </td>
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
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td colspan="2" >
                                                                    </td>
                                                                <td style="width: 103px">
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
                                                            <tr>
                                                                <td colspan="6" height="4">
                                                                    <asp:DataGrid ID="grdEmployee" BorderWidth="1" BorderColor="#d4d0c8" runat="server"  AutoGenerateColumns="False" ItemStyle-CssClass="ItemColor" AlternatingItemStyle-CssClass="lightblue" HeaderStyle-CssClass="Gridheading" Width="100%">
                                                                        <Columns>
                                                                        
                                                                            <asp:TemplateColumn HeaderText="User Name">
                                                                            <ItemTemplate>                                                                            
                                                                            <%#Eval("Employee_Name")%>
                                                                            </ItemTemplate>                                                                            
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="Department">
                                                                            <ItemTemplate>
                                                                            <%#Eval("Department_Name")%>
                                                                            </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="Aoffice">
                                                                            <ItemTemplate>
                                                                            <%#Eval("Aoffice")%>
                                                                            </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="DOJ">
                                                                            <ItemTemplate>
                                                                            <asp:Label ID="lblDOJ" runat="server" Text='<%# objeAAMS.ConvertDate(Eval("DateStart")).ToString("dd-MMM-yy")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="Date of Leaving">
                                                                            <ItemTemplate>
                                                                            <asp:Label ID="lblDOL" runat="server" Text='<%# objeAAMS.ConvertDate(Eval("DateEnd")).ToString("dd-MMM-yy")%>'></asp:Label>
                                                                            
                                                                            </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="City">
                                                                            <ItemTemplate>
                                                                            <%#Eval("City_Name")%>
                                                                            </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                             <asp:TemplateColumn HeaderText="Action">
                                                                            <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkEdit" CssClass="LinkButtons" Text="Select" runat="server" CommandName="SelectX" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EmployeeID") + "|" + DataBinder.Eval(Container.DataItem, "Employee_Name") %>'></asp:LinkButton>&nbsp;                                                                            
                                                                            </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                        </Columns>
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
