           <%@ Page Language="VB"  AutoEventWireup="false" CodeFile="MSUP_OnlineStatus.aspx.vb" Inherits="TravelAgency_MSUP_OnlineStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Add/Modify Online Status</title>
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script type="text/javascript" language ="javascript" >      
      function  NewMSUPOnlineStatus()
       {        
           window.location="MSUP_OnlineStatus.aspx?Action=I";
           return false;
       }  
   function OnlineStatusMandatory()
    {
      if (  document.getElementById("txtStatusCode").value.trim()=="")
         {
            document.getElementById("lblError").innerHTML="Status Code is mandatory.";
            document.getElementById("txtStatusCode").focus();
            return false;         
         }
        if (  document.getElementById("txtStatusCode").value!="")
         {
           if(IsDataValid(document.getElementById("txtStatusCode").value,7)==false)
            {
            document.getElementById("lblError").innerHTML="Status Code is not valid.";
            document.getElementById("txtStatusCode").focus();
            return false;
            } 
         }
         if (  document.getElementById("txtOnlineStatus").value.trim()=="")
         {
            document.getElementById("lblError").innerHTML="Online Status is mandatory.";
            document.getElementById("txtOnlineStatus").focus();
            return false;         
         }
          if (  document.getElementById("txtOnlineStatus").value!="")
         {
           if(IsDataValid(document.getElementById("txtOnlineStatus").value,7)==false)
            {
            document.getElementById("lblError").innerHTML="Onlinle Status is not valid.";
            document.getElementById("txtOnlineStatus").focus();
            return false;
            } 
         }
         
         return true;
     }
  
    </script>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body bgcolor="ffffff" >
    <form id="frmUpTravelAgency" runat="server"  defaultfocus="txtStatusCode">
    
     <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">TravelAgency-&gt;</span><span class="sub_menu">Online Status</span>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage Online Status</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                 
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="6%" class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 57px; height: 22px;">
                                                                    </td>
                                                                <td class="textbold" style="width: 90px; height: 22px;">
                                                                    Status Code<span class="Mandatory">*</span></td>
                                                                <td style="width: 192px; height: 22px" >
                                                                   <asp:TextBox ID="txtStatusCode" runat ="server" CssClass ="textbox" Width="208px" MaxLength="6" TabIndex="1"></asp:TextBox>
                                                                    </td>
                                                                <td width="21%" style="height: 22px">
                                                                    </td>
                                                                <td width="30%" style="height: 22px">
                                                                    <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="3" AccessKey="S" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 57px; height: 22px;">
                                                                    </td>
                                                                <td class="textbold" style="width: 90px; height: 22px;">
                                                                    Online Status<span class="Mandatory">*</span></td>
                                                                <td class="textbold" style="width: 192px; height: 22px;">
                                                                    <asp:TextBox ID="txtOnlineStatus" runat="server" CssClass="textbox" MaxLength="30" Width="208px" TabIndex="2"></asp:TextBox></td>
                                                                <td style="height: 22px">
                                                                </td>
                                                                <td style="height: 22px">
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="4" AccessKey="N" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 57px; height: 22px;">
                                                                </td>
                                                                <td colspan="2" style="height: 22px" class="ErrorMsg">
                                                                    Field Marked * are Mandatory</td>                                                                
                                                                <td style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td style="height: 22px">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="5" AccessKey="R" /></td>
                                                            </tr>                                                           
                                                            <tr>
                                                                <td class="textbold" style="height: 19px">
                                                                    &nbsp;</td>
                                                                <td colspan="2" style="height: 19px" >
                                                                    </td>
                                                                <td style="width: 192px; height: 19px;">
                                                                    &nbsp;</td>
                                                                <td style="height: 19px">
                                                                    &nbsp;</td>
                                                                <td style="height: 19px">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td colspan="6" height="4">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" >
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
        </table>
    <!-- Code by Rakesh -->
    
  
    </form>
</body>
</html>
