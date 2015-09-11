<%@ page language="VB" autoeventwireup="false" inherits="LeftBar, App_Web_leftbar.aspx.cdcab7d2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Menu</title>
    <link href="CSS/AAMS.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript" src="JavaScript/LeftBar.js"></script>
</head>
<script language="javascript" type="text/javascript">
function CallOnload()
{
    CountRow = document.getElementById('<%=lblRowCount.ClientId%>').innerHTML;
}
</script>

<body onload="CallOnload();" bgcolor="#dee7f7" >
    <form id="form1" runat="server" >    
   
       <asp:Literal ID="ltLeftbar" runat="server"></asp:Literal>
       
             <div style="visibility:hidden" >
             
            <asp:Label ID="lblRowCount" runat="server"></asp:Label>    
          
                
        </div>
        
    </form>
</body>

</html>


