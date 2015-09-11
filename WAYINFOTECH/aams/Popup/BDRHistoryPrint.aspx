<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BDRHistoryPrint.aspx.vb" Inherits="Popup_BDRHistoryPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="width:100%" border="0" cellpadding="0" cellspacing="0">
            
            <tr>
                <td align="center" class="heading">
                    BDR History</td>
            </tr>  
             <tr>
                <td align="center" >
                    </td>
            </tr>           
            <tr>
                <td>
                    <asp:DataGrid ID="grdBDRHistory" runat="server" Width="100%" EnableViewState="false" HeaderStyle-CssClass="Gridheading"
                        AlternatingItemStyle-CssClass="lightblue" ItemStyle-CssClass="ItemColor" AutoGenerateColumns="False"
                        BorderColor="#d4d0c8" BorderWidth="1">
                        <Columns>
                            <asp:TemplateColumn HeaderText="Employee Name" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  >
                                <ItemTemplate>
                                    <%#Eval("EMPLOYEENAME")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left"  />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Date Time" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" >
                                <ItemTemplate>
                                    <%#Eval("DateTime")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Change Data" >
                                <ItemTemplate>
                                    <%#Eval("ChangeData")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
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
