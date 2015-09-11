<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Way.aspx.vb" Inherits="AAMSHEAD"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml"  >
<head runat="server">
    <title>WAY</title>
    <link href="CSS/WAY.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
    function Logout()
    {
    window.location.href="SupportPages/TimeOutSession.aspx?LogOut=Logout";
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
<table width="100%" cellpadding="0" border="0" cellspacing="0" ><tr><td background="images/header_01BG.gif">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td width="100%" colspan="2" style="background:url(images/header_01.gif) no-repeat top left;height:44px" id="tdHeader" runat="server" >
   <div class="block" ><strong><asp:Label ID="lblUserName"  runat="server" Width="300px" ></asp:Label></strong>
<span onclick="Logout()" style="cursor:pointer;text-align:right" >Logout</span> </div>
    </td>
  </tr>
</table>
</td></tr></table>
    </form>
</body>
</html>
