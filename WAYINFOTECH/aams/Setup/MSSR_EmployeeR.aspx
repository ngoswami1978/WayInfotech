<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_EmployeeR.aspx.vb"
    Inherits="Setup_MSSR_EmployeeR" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Employee</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>

</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSearch" defaultfocus="txtEmployeeName">
        <table width="860px" align="left" class="border_rightred" style="height: 486px;">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Setup-></span><span class="sub_menu">Employee</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Employee
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="6%" height="30px">
                                                        &nbsp;</td>
                                                    <td width="15%" class="textbold">
                                                        Employee Name</td>
                                                    <td width="30%">
                                                        <asp:TextBox ID="txtEmployeeName" CssClass="textbox" MaxLength="40" runat="server"
                                                            TabIndex="2"></asp:TextBox></td>
                                                    <td style="width: 109px">
                                                        <span class="textbold">Department</span></td>
                                                    <td width="21%">
                                                        <asp:DropDownList ID="drpDepartment" CssClass="dropdownlist" Width="137px" runat="server"
                                                            TabIndex="2">
                                                        </asp:DropDownList></td>
                                                    <td width="18%">
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3"
                                                            AccessKey="A" /></td>
                                                </tr>
                                                <tr>
                                                    <td height="25px">
                                                        &nbsp;</td>
                                                    <td class="textbold">
                                                        Aoffice</td>
                                                    <td>
                                                        <asp:DropDownList ID="drpAoffice" CssClass="dropdownlist" Width="137px" runat="server"
                                                            TabIndex="2">
                                                        </asp:DropDownList></td>
                                                    <td class="textbold" nowrap="nowrap" style="width: 109px">
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="3" Text="Export"
                                                            AccessKey="E" /></td>
                                                </tr>
                                                <tr>
                                                    <td height="30px">
                                                        &nbsp;</td>
                                                    <td class="textbold" nowrap="nowrap">
                                                    </td>
                                                    <td class="textbold">
                                                        &nbsp;</td>
                                                    <td nowrap="nowrap" class="textbold" style="width: 109px">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3"
                                                            AccessKey="R" /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" style="height: 5px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:GridView ID="grdEmployee" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                            Width="100%" EnableViewState="true" AllowSorting="true" HeaderStyle-ForeColor="white">
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Employee Name " DataField="Employee_Name" SortExpression="Employee_Name" />
                                                                <asp:BoundField HeaderText="Department " DataField="Department_Name" SortExpression="Department_Name" />
                                                                <asp:BoundField HeaderText="Aoffice" DataField="Aoffice" SortExpression="Aoffice" />
                                                                <asp:BoundField HeaderText="City " DataField="City_Name" SortExpression="City_Name" />
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkSelect" CssClass="LinkButtons" Text="Select" runat="server"
                                                                            CommandName="SelectX" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EmployeeID") + "|" + DataBinder.Eval(Container.DataItem, "Employee_Name") + "|" + DataBinder.Eval(Container.DataItem, "Aoffice") + "|" + DataBinder.Eval(Container.DataItem, "BR_ID") %>'></asp:LinkButton>&nbsp;
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <RowStyle CssClass="textbold" />
                                                            <HeaderStyle CssClass="Gridheading" />
                                                            <PagerSettings Mode="Numeric" Position="Bottom" PageButtonCount="5" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" valign="top">
                                                        <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr style="padding-top: 4pt; padding-bottom: 4pt">
                                                                    <td style="width: 30%" class="left">
                                                                        <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                            ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey"
                                                                            ReadOnly="true"></asp:TextBox></td>
                                                                    <td style="width: 25%" class="right">
                                                                        <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                    <td style="width: 20%" class="center">
                                                                        <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber"
                                                                            Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                                        </asp:DropDownList></td>
                                                                    <td style="width: 25%" class="left">
                                                                        <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
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
            <tr>
                <td>
                    <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" Width="73px" CssClass="textboxgrey"
                        Visible="false"></asp:TextBox>
                    <input type="hidden" id="hdCtrlId" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
