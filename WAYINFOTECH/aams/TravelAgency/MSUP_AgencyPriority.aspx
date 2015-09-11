<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_AgencyPriority.aspx.vb" Inherits="TravelAgency_MSUP_AgencyPriority" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Add/Modify Agency Priority</title>
      <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script type="text/javascript" language ="javascript" >      
     
      function  NewMSUPAgencyPriority()
       {        
           window.location="MSUP_AgencyPriority.aspx?Action=I";
           return false;
       }  
   
   function AgencyPriorityMandatory()
    {
        if (document.getElementById("txtPriority").value.trim()=="")
         {          
            document.getElementById("lblError").innerHTML="Agency Priority is mandatory.";
            document.getElementById("txtPriority").focus();
            return false;
           
         }
        if (document.getElementById("txtPriority").value!="")
         {
           if(IsDataValid(document.getElementById("txtPriority").value,7)==false)
            {
            document.getElementById("lblError").innerHTML="Agency Priority is not valid.";
            document.getElementById("txtPriority").focus();
            return false;
            } 
         }
       
         return true;
     }
  
    </script>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
    
</head>
<body bgcolor="ffffff" >
    <form id="frmAgencyPriority" runat="server"  defaultfocus="txtPriority">
    
     <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">TravelAgency-&gt;</span><span class="sub_menu">Agency Priority</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage Agency Priority</td>
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
                                                                    Priority Name<span class="Mandatory">*</span></td>
                                                                <td style="width: 192px; height: 22px" >
                                                                   <asp:TextBox ID="txtPriority" runat ="server" CssClass ="textbox" Width="208px" MaxLength="40" TabIndex="1"></asp:TextBox>
                                                                    </td>
                                                                <td width="21%" style="height: 22px">
                                                                    </td>
                                                                <td width="30%" style="height: 22px">
                                                                    <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="2" /></td>
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
                                                                    </td>
                                                                <td class="textbold" style="width: 192px; height: 22px;">
                                                                    </td>
                                                                <td style="height: 22px">
                                                                </td>
                                                                <td style="height: 22px">
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="3" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 57px">
                                                                </td>
                                                                <td colspan="2" style="height: 14px" class="ErrorMsg">
                                                                    Field Marked * are Mandatory</td> 
                                                               
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="4" /></td>
                                                            </tr>                                                           
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td colspan="2" >
                                                                    </td>
                                                                <td style="width: 192px">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
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
