<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TAUP_DeinstallMiscInstall.aspx.vb" Inherits="TravelAgency_TAUP_DeinstallMiscInstall" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Deinstall Misc. Hardware </title>
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
     <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
      <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
        <script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
<script type="text/javascript" language="javascript">
    
    function RefreshMisc()
     {
  
   
   if(document.getElementById("hdMsg").value=="1")
   {
        window.opener.document.getElementById("hdDeinstalled").value="1"
        window.opener.document.forms['form1'].submit();
    }
    
    }
    
    
     function closeWindow()
     {
       
    window.close();
    return false;
     }
     
     
     
  function validateChallanNo()
   { 
   
      var strChallanNo= document.getElementById("txtChallanNo").value;
     strChallanNo=strChallanNo.trim();
         if( strChallanNo=="")
     {
         if (confirm('Challan No. is blank. Want to continue ?'))
         {
            if (document.getElementById("hdOverRide").value == "0" || document.getElementById("hdOverRide").value == "")
             {
                 document.getElementById("lblError").innerHTML="You don't have enough rights to DeInstall H/W without a ChallanNo";
                 return false;
             }
         }
         else
         {
           try
                {
                document.getElementById("txtChallanNo").focus();
                 return false;
                }
           catch(err)
                {
                
                }     
         }
     }
     else
     {
      //CallServer(strChallanNo,"This is context from client");
      
      CallServer(strChallanNo + "|1" ,"This is context from client");
      return false;
     }    
     
   }
   
   
   
   
    function ReceiveServerData(args, context)
    {      
            
      
            var result=args.split("|");
            if(result[0]=="0")
            {
                    if(confirm('Given Challan No. doesnot exist do you want to continue'))
                    {
                             if (document.getElementById("hdOverRide").value == "0" || document.getElementById("hdOverRide").value == "")
                             {
                                 document.getElementById("lblError").innerHTML="You don't have enough rights to DeInstall H/W without a Valid Challan No.";
                                 document.getElementById("txtChallanNo").focus();
                                 return false;
                             }
                 
                    }
                    else
                    {
                                    document.getElementById("txtChallanNo").focus();
                                    return false;
                    }
            }
            
           if (result[0]=="1")
            {
            
            } 
            
            
            if (result[0]=="-1")
            {
              
                document.getElementById("lblError").innerHTML=result[1];
                document.getElementById("txtChallanNo").focus();
                return false;
            }
            
			
    }
    
    
     
</script>

</head>
<body >
    <form id="form1" runat="server">
     <table width="560px" align="left" height="200px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">TravelAgency-&gt;</span><span class="sub_menu">Deinstall Misc. Hardware </span></td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                            <asp:LinkButton  ID="btnClose" CssClass="LinkButtons" runat="server" Text="Close"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                <span style="font-family: Microsoft Sans Serif">Deinstall Misc. Hardware </span></td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    
                                     <tr>
                                     
                                     <td colspan="3">
                                      <asp:Panel ID="pnlErroMsg" Visible="false"  runat="server" Width="100%"  >
                                 <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                 <tr>
                                     <td valign="top" colspan="2" style="width: 683px;" >
                                     <asp:Label ID="lblValidationMsg" CssClass="button" runat="server" Width="384px" EnableViewState="False" Height="20px" ></asp:Label>
                                     </td>
                                     <td colspan="2" valign="top">
                                     <table cellpadding="0" cellspacing="0">
                                     <tr valign="top" >
                                     <td>
                                      <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="button" />
                                     </td>
                                     <td>
                                     <asp:Button ID="btnNo" runat="server" Text="No" CssClass="button" />
                                     
                                     </td>
                                     
                                     </tr>
                                     </table>
                                    
                                     
                                     </td>
                                 </tr>
                                 </table>
                                 </asp:Panel>
                                     
                                     </td>
                                     
                                     
                                     </tr>
                                     
                                    <tr>
                                        <td class="redborder center">
                                        <asp:Panel ID="pnlDeInstall" runat="server" >
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                        <tr>
                                                                            <td colspan="4" class="center gap">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td class="textbold" style="width: 232px">
                                                                                Date of De-Installation</td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtDtInstall" runat="server" CssClass="textboxgrey" MaxLength="10" Width="160px" TabIndex="1"></asp:TextBox>
                                                                                <img id="imgDateInstall" runat="server"  alt="" src="../Images/calender.gif" title="Date selector"
                                                                                                                       TabIndex="2" style="cursor: pointer" />
                                                                                                                       <script type="text/javascript">
                                                                                                                                                Calendar.setup({
                                                                                                                                                inputField     :    '<%=txtDtInstall.clientId%>',
                                                                                                                                                ifFormat       :    "%d/%m/%Y",
                                                                                                                                                button         :    "imgDateInstall",
                                                                                                                                                //align          :    "Tl",
                                                                                                                                                singleClick    :    true
                                                                                                                                                });
                                                                                                                    </script>
                                                                                </td>
                                                                            <td>
                                                                    <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Ok" TabIndex="4" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td class="textbold" style="width: 232px">
                                                                                Challan No.</td>
                                                                            <td>
                                                                   <asp:TextBox ID="txtChallanNo" runat ="server" CssClass ="textbox" Width="184px" MaxLength="17" TabIndex="3"></asp:TextBox></td>
                                                                            <td>
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="5" /></td>
                                                                        </tr>
                                    
                                                                      
                                                                    </table>
                                       </asp:Panel>
                                        </td>
                                        
                                    </tr>
                                    
                                    
                                     
                                </table>
                                
                                                <input id="hdOverRide" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdEquipCode" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdFlagMiscInstall" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdOverRideBackDate" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdOverRideSerialNo" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdfocustemp" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdComboData" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdEuipText" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdAction" runat="server" style="width:1px" type="hidden" />
                                                <input id="hdMsg" runat="server" style="width:1px" type="hidden" />
                                                <input id="hdYesNo" runat="server" style="width:1px" type="hidden" />
                                                <input id="hdValidChallan" runat="server" style="width:1px" type="hidden" />
                                                
                                                
                                                
                                                   
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
