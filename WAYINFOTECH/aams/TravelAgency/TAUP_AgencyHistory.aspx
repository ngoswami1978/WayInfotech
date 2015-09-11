<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TAUP_AgencyHistory.aspx.vb"
    Inherits="TravelAgency_MSUP_AgencyHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="width:100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="right">
                    <a href="#" class="LinkButtons" onclick="window.close();">Close</a>&nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td align="center" class="heading">
                    Agency History</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="grdAgencyHistory" runat="server" Width="100%" EnableViewState="false" HeaderStyle-CssClass="Gridheading"
                        AlternatingItemStyle-CssClass="lightblue" ItemStyle-CssClass="ItemColor" AutoGenerateColumns="False"
                        BorderColor="#d4d0c8" BorderWidth="1">
                        <Columns>
                            <asp:TemplateColumn HeaderText="Employee Name" >
                                <ItemTemplate>
                                    <%#Eval("Employee_Name")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="20%" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Date Time" >
                                <ItemTemplate>
                                    <%#Eval("DateTime")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="20%"/>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Change Data" >
                                <ItemTemplate>
                                    <%#Eval("ChangeData")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="60%"/>
                            </asp:TemplateColumn>                                                  
                        </Columns>
                        <AlternatingItemStyle CssClass="lightblue" />
                        <ItemStyle CssClass="textbold" />
                        <HeaderStyle CssClass="Gridheading" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
