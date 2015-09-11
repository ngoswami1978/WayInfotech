<%@ page language="VB" autoeventwireup="false" inherits="SupportPages_TimeOutSession, App_Web_timeoutsession.aspx.268dd4bc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<script language="javascript" type="text/javascript">
function redirect () 
{ 
    setTimeout("go_now()",5000); 
}

function go_now ()
{     
    window.parent.location.href="../Login.aspx?Logout=True"; 
}
</script>

<body onload="redirect();"  bgcolor="#dee7f7" >
    <form id="form1" runat="server">
  <table width="80%" cellpadding="0" cellspacing="0" border="0" align="center">
    <tr>
        <td>&nbsp;
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
    </tr>
    <tr>
        <td class="TimedOut" align="center"> Your Session Has Timed Out.
        </td>
    </tr>
    <tr>
        <td class="TimedOut" align="center"> You will be shortly redirected to Login Page.
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:LinkButton ID="linkLogin" runat="server"  Text="Login"></asp:LinkButton>
         </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
    </tr>
   </table>
    </form>
</body>
</html>
