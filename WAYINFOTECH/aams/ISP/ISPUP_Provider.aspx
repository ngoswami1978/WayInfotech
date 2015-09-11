<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ISPUP_Provider.aspx.vb" Inherits="ISP_ISPUP_Provider" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AAMS::ISP::Manage Contact Type</title>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
      <script language="javascript" type="text/javascript">      
           function ProviderMandatory()
              {
               // alert( document.getElementById("txtISPProvider").value);
                if (  document.getElementById("txtISPProvider").value.trim()=='')
                 {         
                    document.getElementById("lblError").innerHTML="Provider name is mandatory.";
                    document.getElementById("txtISPProvider").focus();
                    return false;          
                 }  
                 
//                   if (  document.getElementById("txtISPProvider").value!="")
//                 {
//                   if(IsDataValid(document.getElementById("txtISPProvider").value,7)==false)
//                    {
//                    document.getElementById("lblError").innerHTML="Provider name is not valid.";
//                    document.getElementById("txtISPProvider").focus();
//                    return false;
//                    } 
//                 }                 
                        
                 return true;
             }
             
               function NewMSUPProvider()
                {
                window.location.href="ISPUP_Provider.aspx?Action=I|";
                return false;
                }
        
        
       </script>
</head>
<body  >
   <form id="frmManageContactType" runat="server" defaultfocus="txtISPProvider">
    
         <table width="860px" align="left"  class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">ISP-&gt;</span><span class="sub_menu">Manage ISP Provider</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage ISP Provider</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                        <tr>
                                            <td style="width: 176px">
                                            </td>
                                            <td class="gap" colspan="2" style="text-align: center">
                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                            <td>
                                            </td>
                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 196px">
                                                                            </td>
                                                                            <td class="textbold" style="width: 100px">
                                                                                Provider Name<span class="Mandatory" >*</span></td>
                                                                            <td style="width: 308px">
                                                                                <asp:TextBox ID="txtISPProvider" runat="server" CssClass="textbold" MaxLength="20"
                                                                                    TabIndex="1" Width="208px"></asp:TextBox></td>
                                                                            <td>
                                                                    <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="2" AccessKey="S" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Reserved</td>
                                                                            <td style="width: 308px">
                                                                            <asp:CheckBox ID="chkReserved" runat="server" TabIndex="1" />
                                                                    </td>
                                                                            <td>
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="2" AccessKey="N" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px; height: 26px;">
                                                                            </td>
                                                                            <td class="textbold" style="height: 26px">
                                                             </td>
                                                                            <td style="width: 308px; height: 26px;">
                                                                    </td>
                                                                            <td style="height: 26px">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="2" AccessKey="R" /></td>
                                                                        </tr>
                                        <tr>
                                            <td style="width: 176px; height: 26px">
                                            </td>
                                            <td style="height: 26px" colspan ="2" class="ErrorMsg">
                                                &nbsp;Field Marked * are Mandatory</td>
                                            
                                            <td style="height: 26px">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 176px; height: 26px">
                                            </td>
                                            <td colspan="2" style="height: 26px" >
                                               </td>
                                            <td style="height: 26px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 176px; height: 26px">
                                        <asp:HiddenField ID="hdID" runat ="server" />
                                            </td>
                                            <td class="ErrorMsg" colspan="2">
                                            </td>
                                            <td >
                                            </td>
                                        </tr>
                                                                          <tr>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4" style="text-align: center">
                                            &nbsp;</td>
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
      
    </form>
</body>
</html>
