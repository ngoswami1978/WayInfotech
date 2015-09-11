<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRPR_PrintLetter.aspx.vb" Inherits="Training_TRPR_PrintLetter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<meta content="no" visible="false" />
    <title>AAMS::Training::Print Letter</title>
   
         <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body >
    <form id="form1" runat="server">
    <div>
          <table style="width:100%">
          <tr><td style="width:100%"> <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server" EnableViewState="False"></asp:Label></td>
          </tr>
          <tr><td style="width:100%">   
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" BorderStyle="none" BorderWidth="0px" CellPadding="0" GridLines="None">
        <Columns>
        <asp:TemplateField>
        <ItemTemplate>

         <asp:TextBox ID="txtLetter"  runat="server" CssClass="textbox" Height="650px" BorderStyle="none"  TextMode="MultiLine" Wrap="true" Width="750px" ReadOnly="True"  BorderColor="white" BorderWidth="0px" Text='<%#Eval("TR_CLETTER") %>' ></asp:TextBox>
                    
        </ItemTemplate>
        </asp:TemplateField>
        </Columns>
        </asp:GridView>
       </td>
          </tr>
          
       
        
        <tr><td >
        <input type="hidden" runat="server" id="hdLetterType" style="width: 8px" />
        </td>
        <td></td>
        </tr>
        </table>
         
    </div>
    </form>
</body>

<script type="text/javascript" language="javascript">window.focus();window.print();</script>
</html>
