<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TAUP_NewAgencyPCInstall.aspx.vb" Inherits="TravelAgency_TAUP_NewAgencyPCInstall" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Agency PC Installation</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
   <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
    <script type="text/javascript" src="../Calender/calendar.js"></script>
    <!-- import the language module -->
    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
    <!-- import the calendar setup module -->
    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>
    <script type="text/javascript" language ="javascript" >  
//   // var st;
//   

//        function ValidateOrderNo()
//   {     
//    
//     // document.getElementById("lblError").innerHTML=""; 
//      var strOrderNo= document.getElementById("txtOrderNo").value;
//      strOrderNo=strOrderNo.trim();  
//      if(strOrderNo=="")
//      {
//               if (confirm('Order Number is blank. Want to continue ?')==true)
//                 { 
//                    //document.getElementById("hdAllowSaveForOrder").value="1";
//                    if  (document.getElementById("hdblnOrderOverride").value == "0" || document.getElementById("hdblnOrderOverride").value == "")                    
//                    {        
//                        document.getElementById("hdAllowSaveForOrder").value="0";              
//                        document.getElementById("lblError").innerHTML="You don't have enough rights to Install H/W without a Order No.";
//                        document.getElementById("txtOrderNo").focus();                        
//                        return false;
//                        
//                    }
//                   else
//                    {                  
//                        document.getElementById("hdAllowSaveForOrder").value="1";
//                        document.getElementById("txtChallanNo").focus();
//                        return false;
//                     }
//                 } 
//                 else
//                 { 
//                     document.getElementById("hdAllowSaveForOrder").value="0";
//                     document.getElementById("txtOrderNo").focus();
//                     return false;
//                 }       
//        
//      }
//      else if( strOrderNo=="0")
//      {
//               if  (document.getElementById("hdblnOrderOverride").value == "1") 
//               {
//                        document.getElementById("hdAllowSaveForOrder").value="1";
//                        document.getElementById("txtChallanNo").focus();
//                        return false;
//               } 
//               else
//               {
//                 document.getElementById("hdAllowSaveForOrder").value="0";
//                  CallServer(strOrderNo + "|O","This is context from client");
//                  return false;
//               }                  
//      
//      }
//       else if( strOrderNo!="")
//      {          document.getElementById("hdAllowSaveForOrder").value="0" ;
//                 CallServer(strOrderNo + "|O","This is context from client");
//                 return false;              
//      }
//      
//   } 
//   
//    function ReceiveServerData(args, context)
//    {      
//            var result=args.split("|");
//            if (result[0]=="0")
//            {
//                if(confirm('Given Order number does not exist .Do you want to Continue?')==true)
//                {
//                     document.getElementById("hdAllowSaveForOrder").value ="1";
//                      if (document.getElementById("hdblnOrderOverride").value == "0" || document.getElementById("hdblnOrderOverride").value == "")
//                     {
//                         document.getElementById("lblError").innerHTML="You don't have enough rights to Install H/W without a Valid Order No.";
//                         document.getElementById("txtOrderNo").focus();
//                          document.getElementById("hdAllowSaveForOrder").value ="0";
//                         return false;
//                     } 
//                   //  document.getElementById("btnSave").focus();
//                    // return true; 
//                    
//                }
//                else
//                {
//                    document.getElementById("hdAllowSaveForOrder").value ="0"
//                    document.getElementById("txtOrderNo").focus();
//                     return false;
//                }
//            }
//            if (result[0]=="1")
//            {
//                document.getElementById("hdAllowSaveForOrder").value ="1";
//                 return true;
//            }
//             if (result[0]=="-1")
//            {
//                 document.getElementById("hdAllowSaveForOrder").value ="0";
//                 document.getElementById("lblError").innerHTML=result[1];
//                 document.getElementById("txtOrderNo").focus();
//                 return false;
//            }           
//            
//          

//    }
   function AgencyPCMandatory()
      {

 
         if (  document.getElementById("txtQty").value!="")
         {
           if(IsDataValid(document.getElementById("txtQty").value,3)==false)
            {
            document.getElementById("lblError").innerHTML="Qty is not valid.";
            document.getElementById("txtQty").focus();
            return false;
            } 
         } 
          if (  document.getElementById("txtQty").value!="")
          { 
             if(parseInt(document.getElementById("txtQty").value)<1)
            {
            document.getElementById("lblError").innerHTML="Qty must be greater than 0.";
            document.getElementById("txtQty").focus();
            return false;
            }    
        }
        if (document.getElementById("txtRem").value.trim().length>100)
         {
            document.getElementById("lblError").innerHTML="Remark Can't be greater than 100 characters.";
            document.getElementById("txtRem").focus();
            return false;
         } 
         return true;
     }  
     
      function ResetSaveForOrder()
      {
          document.getElementById("hdAllowSaveForOrder").value="0";  
          return true;   
      }
      
//      function ResetSaveForChallan()
//      {
//       document.getElementById("hdAllowSaveForChallan").value="0";  
//       return false;    
//      }
    </script>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body   >
    <form id="form1" runat="server"   defaultfocus ="txtOrderNo"  >    
      <table width="600px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Agency -&gt;</span><span class="sub_menu">Agency PC Installation
                                </span></td>
                        </tr>
                         <tr>
                                    <td align ="right" > <a href="#" class="LinkButtons" onclick="window.close();  window.opener.document.forms['form1'].submit();">Close</a>&nbsp;&nbsp;&nbsp;</td>
                                  </tr>
                        <tr>
                            <td class="heading center" >
                                &nbsp; Manage Agency PC Installation</td>
                        </tr>
                        <tr>
                            <td >
                               <table width="100%" border="0" cellspacing="0" cellpadding="0">                                 
                                    <tr>
                                        <td class="redborder center">
                                                <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                    
                                                    <tr>
                                                        <td class="center" colspan="5"  height="25px" >
                                                          <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                    </tr>
                                                       <tr>
                                                       <td valign ="top" > 
                                                        <asp:Panel  ID="pnlMsg" runat="server"  width="100%" Visible ="false"  >
                                                            <table border ="0" cellpadding ="0" cellspacing ="0"  width="100%"  > 
                                                               <tr>
                                                                   <td align ="center" valign ="top"  ><asp:Label id="lblConfirm" runat="server" CssClass="Gridheading"></asp:Label> &nbsp;&nbsp;<asp:Button ID="btnYes"  runat ="server" Text ="Yes" CssClass="button topMargin" />&nbsp;&nbsp;<asp:Button ID="btnNo"  runat ="server" Text ="No" CssClass="button topMargin" /></td>                                                                                                                        
                                                               </tr>
                                                                <tr>
                                                                    <td class="center" colspan="5" style="height: 25px" >
                                                                      </td>
                                                                </tr>
                                                               </table> 
                                                        </asp:Panel> 
                                                        </td> 
                                                     </tr> 
                                                      <tr>
                                                       <td valign ="top"  colspan ="5"> 
                                                        <asp:Panel  ID="PnlDetails" runat="server"  width="100%" >
                                                            <table border ="0" cellpadding ="0" cellspacing ="0"  width="100%"  > 
                                                                         <tr>
                                                        <td style="width:15%">
                                                        </td>
                                                        <td class="textbold" style="width:20%">
                                                            Order No.<span class="Mandatory" >*</span></td>
                                                        <td style="width: 35%">
                                                            <asp:TextBox ID="txtOrderNo" runat="server" CssClass="textbox" MaxLength="12" ReadOnly="false"
                                                                TabIndex="1" Width="156px"></asp:TextBox></td>                                           
                                                        <td style="width: 5%">
                                                            &nbsp;</td>
                                                        <td class="left" style="width: 25%">
                                                           <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="6" AccessKey="S" /></td>
                                                    </tr>
                                                   
                                                    <tr>
                                                        <td style="width:15%">
                                                        </td>
                                                        <td class="textbold" style="width:20%">
                                                            Installation Date<span class="Mandatory" >*</span></td>
                                                        <td style="width: 35%">
                                                            <asp:TextBox ID="txtInstallDate" runat="server" CssClass="textboxgrey" MaxLength="10"
                                                                ReadOnly="True" TabIndex="2" Width="158px"></asp:TextBox>
                                                            <img id="f_DateInstall" alt="" src="../Images/calender.gif" style="cursor: pointer" runat="server" 
                                                                tabindex="3" title="Date selector" />

                                                            <script type="text/javascript">
                                                                                                                Calendar.setup({
                                                                                                                inputField     :    '<%=txtInstallDate.clientId%>',
                                                                                                                ifFormat       :    "%d/%m/%Y",
                                                                                                                button         :    "f_DateInstall",
                                                                                                                //align          :    "Tl",
                                                                                                                singleClick    :    true
                                                                                                                });
                                                            </script>

                                                        </td>
                                                       
                                                        <td style="width: 5%">
                                                            </td>
                                                        <td class="left" style="width: 25%"><asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="7" AccessKey="R" /></td>
                                                    </tr>
                                                     <tr>
                                                        <td style="width:15%">
                                                        </td>
                                                        <td class="textbold" style="width:20%">
                                                            Quantity</td>
                                                        <td style="width: 35%">
                                                            <asp:TextBox ID="txtQty" runat="server" CssClass="textbox" MaxLength="5"
                                                                TabIndex="4" Width="155px">1</asp:TextBox></td>                                           
                                                        <td style="width: 5%">
                                                            &nbsp;</td>
                                                        <td class="center" style="width: 30%">
                                                           </td>
                                                    </tr>
                                                     <tr>
                                                        <td style="width:10%">
                                                        </td>
                                                        <td class="textbold" style="width:20%">
                                                           Remarks</td>
                                                        <td style="width: 35%">
                                                            <asp:TextBox ID="txtRem" runat="server" CssClass="textbox"  ReadOnly="false" TextMode ="MultiLine" 
                                                                TabIndex="5" Width="154px" Height="80px"></asp:TextBox></td>                                           
                                                        <td style="width: 5%">
                                                            &nbsp;</td>
                                                        <td class="center" style="width: 25%">
                                                           </td>
                                                    </tr>
                                                     <tr>
                                                        <td  style="width:15%"></td>
                                                        <td class="textbold" style="width:25%" >
                                                          </td>
                                                        <td style="width:35%">             </td>
                                                        <td style="width:5%">
                                                            &nbsp;</td>
                                                        <td style="width:25%" class="center"> </td>
                                                    </tr>
                                                     <tr>
                                                          <td >
                                                        </td>
                                                        <td colspan="4" class="ErrorMsg">
                                                           
                                                            
                                                      </td>
                                                      
                                                    </tr>    
                                                    <tr>
                                                          <td >
                                                        </td>
                                                        <td colspan="4" class="ErrorMsg">
                                                            Field Marked * are Mandatory</td>
                                                      
                                                    </tr>       
                                                            
                                                            </table> 
                                                        </asp:Panel> 
                                                        </td> 
                                                     </tr> 
                                                     <tr>
                                                       <td> <input id="hdLcode" runat="server" style="width: 5px" type="hidden" />
                                                            <input id="hdOrderNoExist" runat="server" style="width: 5px" type="hidden" value ="0" />
                                                            <input id="hdblnOrderOverride" runat="server" style="width: 5px" type="hidden"  value ="0"  />
                                                             <input id="hdAllowSaveForOrder" runat="server" style="width: 5px" value ="0" type="hidden" />
                                                            <asp:HiddenField ID="SaveStatus" runat="server" /></td>
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
