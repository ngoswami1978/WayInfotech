<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SASR_AgencyTargetHistory.aspx.vb"
    Inherits="Sales_SASR_AgencyTargetHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Agency Target History</title>
    <base target="_self" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="right">
                    &nbsp; <a href="#" class="LinkButtons" onclick="window.close();">Close</a>&nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td align="center" class="heading">
                    Agency Target History</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="grdATargetHistory" BorderWidth="1" BorderColor="#d4d0c8" runat="server"
                        AutoGenerateColumns="False" Width="100%" EnableViewState="true" AllowSorting="True"
                        HeaderStyle-ForeColor="white">
                        <Columns>                            
                              <asp:TemplateField HeaderText="Month" SortExpression="MONTH">
                                <ItemTemplate>
                                    <%#Eval("MONTH")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Year" SortExpression="YEAR">
                                <ItemTemplate>
                                    <%#Eval("YEAR")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EmployeeName" SortExpression="EMPNAME">
                                <ItemTemplate>
                                    <%#Eval("EMPNAME")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="DateTime" SortExpression="DATE"
                                ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <%#Eval("DATE")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Change Data" SortExpression="CHANGEDDATA">
                                <ItemTemplate>
                                    <%#Eval("CHANGEDDATA")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle CssClass="lightblue" />
                        <HeaderStyle CssClass="Gridheading" HorizontalAlign="left" />
                        <RowStyle CssClass="textbold" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr style="padding-top: 4pt; padding-bottom: 4pt">
                                <td style="width: 30%" class="left">
                                    <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                        ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey"></asp:TextBox></td>
                                <td style="width: 25%" class="right">
                                    <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                <td style="width: 20%" class="center">
                                    <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList onkeyup="gotop(this.id)"
                                        ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                    </asp:DropDownList></td>
                                <td style="width: 25%" class="left">
                                    <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" Width="73px" CssClass="textboxgrey"
                        Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                  
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
