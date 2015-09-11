<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PUSR_MonNo.aspx.vb" Inherits="Popup_PUSR_MonNo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Misc. Hardware History</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
  
    function CloseMiscPage(id)
    {
    if (id=="1")
    {
    if (window.opener.document.forms['form1']['hdAllowSaveForMonNo']!=null)
     { 
        window.opener.document.forms['form1']['hdAllowSaveForMonNo'].value="1";
        if (window.opener.document.forms['form1']["hdblnSNoOverride"].value == "0" || window.opener.document.forms['form1']["hdblnSNoOverride"].value == "")
        {
         document.getElementById("lblError").innerHTML="You don't have enough rights to Install H/W without a Valid Mon No.";
        //window.opener.document.forms['form1']["hdfocustemp"].value="0";
        window.close();
        return false;
        }
     }
    }
    else
    {
        if (window.opener.document.forms['form1']['hdAllowSaveForMonNo']!=null)
     { 
        window.opener.document.forms['form1']['hdAllowSaveForMonNo'].value="0";
        window.close();
         return false;
      }
    }
    window.close();
         return false;
    }
    function fnEquipID()
    {   
    if (window.opener.document.forms['form1']['hdAllowSaveForMonNo']!=null)
     { 
        window.opener.document.forms['form1']['hdAllowSaveForMonNo'].value="";
        window.close();
         return false;
      }
       window.close();
         return false;
    }
    
   
    </script>
</head>
<body>
    <form id="form1" runat="server">
       <table style="width:600px" border="0" cellpadding="0" cellspacing="0" class="border_rightred ">
            <tr>
                <td align="right" colspan="2">
                    <asp:LinkButton ID="lnkClose"  CssClass="LinkButtons" runat="server" OnClientClick="return fnEquipID()" >Close</asp:LinkButton>&nbsp;&nbsp;
                    &nbsp; &nbsp; &nbsp;

                 </td>
                
            </tr>
            <tr>
                <td align="center" class="heading" colspan="2">
                    Hardware Installtion</td>
                    
            </tr>
                  <tr>
                            <td valign="top">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                   <tr><td class="gap"></td></tr>
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
            <tr>
                <td class="center gap" colspan="2">
                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                    &nbsp;
                </td>
            </tr>
           <tr>
               <td class="right textbold" colspan="2">
                Would you like to install it at this Location also?
                <asp:Button ID="Button1" runat="server" Text="Yes" CssClass="button topMargin" OnClientClick="return CloseMiscPage('1')" />
            <asp:Button ID="Button2" runat="server" Text="No" CssClass="button topMargin" OnClientClick="return CloseMiscPage('2')"/>&nbsp;</td>
                       
           </tr>
            <tr>
                <td colspan="2"> 
             <asp:GridView  ID="gvInstall" runat="server"  AutoGenerateColumns="False"  TabIndex="6" Width="100%" >
                                                                                <Columns>
                                                                            
                                                                   <asp:BoundField DataField="AGENCYNAME" HeaderText="Agency Name" />
                                                                   <asp:BoundField DataField="OFFICEID" HeaderText="Office ID"  />
                                                                   <asp:BoundField DataField="DATEINSTALLED" HeaderText="Date" />
                                                   
                                                                
                                                     
                                                 
                                                 </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" />                                                    
                                                 </asp:GridView>
                </td>
                
             
            </tr>
           </table>
           </td>
                                    </tr>
                                </table></td>
           </tr>
             
        </table>
    </form>
</body>
</html>
