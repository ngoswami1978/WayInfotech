<%@ page language="VB" autoeventwireup="false" inherits="Main, App_Web_main.aspx.cdcab7d2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AAMS: Amadeus Agent Managment System</title>
    <link href="CSS/AAMS.css" rel="stylesheet" type="text/css" />    
</head>
<script >
    function UnloadMailPage()
        {   
          window.location.href="SupportPages/TimeOutSession.aspx?LogOut=Logout"  
        }
</script>

<frameset rows="44,*" border="0" framespacing="0" onunload ="UnloadMailPage();">
 <frame src="Way.aspx" name="lhnavframe" frameborder="0" marginheight="0" marginwidth="0" scrolling="no">
  <frameset cols="178,*" border="0" framespacing="0">
   <frame src="LeftBar.aspx" name="topframe" frameborder="0"  marginheight="0" marginwidth="0"  scrolling="auto" noresize>
   <frame  id="mainframe" src=<%=strMainFramePage %> name="mainframe"  frameborder="0" marginheight="0" marginwidth="0" scrolling="auto">
  </frameset>
  <noframes>
   A frames enabled browser is needed to view AAMS.   
  </noframes>
</frameset> 
 
</html>
