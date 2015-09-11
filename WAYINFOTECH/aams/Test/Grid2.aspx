<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Grid2.aspx.vb" Inherits="Test_Grid2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns ="false" >
         <Columns>
          <asp:BoundField DataField ="Id" HeaderText ="Id" />
          <asp:BoundField DataField ="Name" HeaderText ="Name" />
          <asp:TemplateField>
             <ItemTemplate >
                          <asp:LinkButton ID="lnkEdit" runat ="server" CommandName ="EditX" CommandArgument='<%# Eval("Id")%>'>Edit</asp:LinkButton>
                          <asp:LinkButton ID="LinkButton1" runat ="server" CommandName ="DeleteX" CommandArgument='<%# Eval("Id")%>'>Delete</asp:LinkButton>
             </ItemTemplate>
          </asp:TemplateField>
         
         </Columns>
        
        </asp:GridView>
        &nbsp;
        <asp:Button ID="Button1" runat="server" Text="Button" />
        <table>
            <tr>
                <td style="width: 100px">
                    Id</td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtId" runat="server"></asp:TextBox></td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    Name</td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
