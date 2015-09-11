<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TAUP_AgencyPCDeInstall.aspx.vb" Inherits="TravelAgency_TAUP_AgencyPCDeInstall" %>
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
//  
//        function ValidateOrderNo()
//   {     
//   //  
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
//                       
//                        document.getElementById("txtOrderNo").focus();                        
//                        return false;
//                        
//                    }
//                   else
//                    {                  
//                        document.getElementById("hdAllowSaveForOrder").value="1";
//                          if (document.getElementById("txtChallanNo").disabled==false )
//                          {
//                            document.getElementById("txtChallanNo").focus();
//                            }
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
//                         if (document.getElementById("txtChallanNo").disabled==false )
//                          {
//                          document.getElementById("txtChallanNo").focus();
//                          }
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
//   
//    function validateChallanNo()
//   { 
//   //
//  //  document.getElementById("lblError").innerHTML="";
//    if  (document.getElementById("hdAllowSaveForOrder").value!="1")     
//    {    
//    return false;
//    }
//     // document.getElementById("lblError").innerHTML=""; 
//      var strChallanNo= document.getElementById("txtChallanNo").value;
//      strChallanNo=strChallanNo.trim();
//      if( strChallanNo=="")
//      {
//               if (confirm('Challan Number is blank. Want to continue ?')==true)
//                 {
//                        if  (document.getElementById("hdblnChallanOverride").value == "0" || document.getElementById("hdblnChallanOverride").value == "")                    
//                        {       
//                            document.getElementById("hdAllowSaveForChallan").value="0";
//                            document.getElementById("lblError").innerHTML="You don't have enough rights to Install H/W without a Challan No.";
//                            document.getElementById("txtChallanNo").focus();
//                            return false;
//                        }
//                        else
//                        {
//                            document.getElementById("hdAllowSaveForChallan").value="1";
//                            document.getElementById("txtRem").focus();
//                             return false;
//                       }
//                 } 
//                 else  
//                 {                      
//                     document.getElementById("hdAllowSaveForChallan").value="0";
//                     document.getElementById("txtChallanNo").focus();
//                     return false;
//                 }       
//        
//      }
//      else if( strChallanNo=="0")
//      {
//               if  (document.getElementById("hdblnChallanOverride").value == "1") 
//               {     
//                        document.getElementById("hdAllowSaveForChallan").value="1";
//                        document.getElementById("txtRem").focus();
//                        return false;
//               } 
//               else
//               {                         
//                  document.getElementById("hdAllowSaveForChallan").value="0";
//                  CallServer(strChallanNo + "|C","This is context from client");
//                  return false;
//               }                  
//      
//      }
//       else if( strChallanNo!="")
//      {          
//                 document.getElementById("hdAllowSaveForChallan").value="0"; 
//                 CallServer(strChallanNo + "|C","This is context from client");
//                 return false;              
//      }
//     
//   }
//   
//    function ReceiveServerData(args, context)
//    {      
//        //   
//        document.getElementById("lblError").innerHTML="";  
//          
//          /*  Code for Order No   */
//            var result=args.split("|");
//            if (result[0]=="0")
//            {
//            
//           //document.getElementById("txtChallanNo").detachEvent('onfocusout',validateChallanNo);
//           //document.getElementById("txtChallanNo").attachEvent('onfocusout',validateChallanNo);
//               
//	
//                if(confirm('Given Order number does not exist .Do you want to Continue?')==true)
//                {
//                
//                      if (document.getElementById("hdblnOrderOverride").value == "0" || document.getElementById("hdblnOrderOverride").value == "")
//                     {
//                         document.getElementById("hdAllowSaveForOrder").value="0";
//                         document.getElementById("lblError").innerHTML="You don't have enough rights to Install H/W without a Valid Order No.";
//                         if (document.getElementById("txtChallanNo").disabled==false )
//                         {
//                          document.getElementById("txtChallanNo").focus();
//                          }
//                         return false;
//                     } 
//                     else
//                     {
//                       document.getElementById("hdAllowSaveForOrder").value="1";
//                         if (document.getElementById("txtChallanNo").disabled==false )
//                         {
//                           document.getElementById("txtChallanNo").focus();
//                         }
//                       return false;
//                     }
//                    // return true; 
//                    
//                }
//                else
//                {   
//                     document.getElementById("hdAllowSaveForOrder").value="0";
//                     document.getElementById("txtOrderNo").focus();
//                     return false;
//                }
//            }
//            if (result[0]=="O1")
//            {
//                   document.getElementById("hdAllowSaveForOrder").value="1";
//                   //document.getElementById("txtChallanNo").focus();
//                  return false;
//            }
//             if (result[0]=="-1")
//            {
//                 document.getElementById("hdAllowSaveForOrder").value="0";
//                 document.getElementById("lblError").innerHTML=result[1];
//                 document.getElementById("txtOrderNo").focus();
//                 return false;
//            }           
//            
//            
//            
//            
//           
//            
//       /*  Code for Order No   */
//       
//       /*  Code for Challan No   */
//                /*  Code for Challan No   */
//           if (result[0]=="C0")
//            {
//                if(confirm('Given Challan number does not exist .Do you want to Continue?')==true)
//                {
//                
//                      if (document.getElementById("hdblnChallanOverride").value == "0" || document.getElementById("hdblnChallanOverride").value == "")
//                     {                      
//                         document.getElementById("hdAllowSaveForChallan").value="0";
//                         document.getElementById("lblError").innerHTML="You don't have enough rights to Install H/W without a Valid Challan No.";
//                         document.getElementById("txtChallanNo").focus();
//                         return false;
//                     } 
//                 
//                       else
//                     {   
//                        document.getElementById("hdAllowSaveForChallan").value="1";                       
//                        return false;
//                     }
//                     
//                }
//                else
//                {  
//                     document.getElementById("hdAllowSaveForChallan").value="0";
//                     document.getElementById("txtChallanNo").focus();
//                     return false;
//                }
//            }
//            if (result[0]=="C1")
//            { 
//              document.getElementById("hdAllowSaveForChallan").value="1";
//                if (result[2]!='')
//                 {     
//                               
//                      if(confirm('Given challan No. ' + result[1] + ' is for ' + result[2] + ' OfficeID ' + result[3]  +  ' Want to reuse it for this Agency also ?')==true)
//                    {
//                              if (document.getElementById("hdblnChallanOverride").value == "0" || document.getElementById("hdblnChallanOverride").value == "")
//                             {
//                                 document.getElementById("lblError").innerHTML="You don't have enough rights to Install H/W without a Challan No.";
//                                 //document.getElementById("txtChallanNo").focus();
//                                 document.getElementById("hdAllowSaveForChallan").value="0";
//                                 return false;
//                             }  
//                             else
//                             {
//                            //  document.getElementById("drpCpuType").focus();
//                               return false;
//                            }
//                    }
//                    else
//                    {   
//                        document.getElementById("hdAllowSaveForChallan").value="0";                 
//                        document.getElementById("txtChallanNo").focus();
//                        return false;
//                    }  
//                }
//               
//                
//                  return false;
//              
//            
//            }
//               if (result[0]=="-1")
//            { 
//                 document.getElementById("hdAllowSaveForChallan").value="0";
//                 document.getElementById("lblError").innerHTML=result[1];
//                 document.getElementById("txtChallanNo").focus();
//                 return false;
//            } 
//          

//    }
   function PCDeInstallMandatory()
      {
      
      
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
      
      function ResetSaveForChallan()
      {
       document.getElementById("hdAllowSaveForChallan").value="0";  
       return true;    
      }
  
    </script>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body >
    <form id="form1" runat="server"   defaultfocus ="txtOrderNo"  >    
      <table width="600px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Agency -&gt;</span><span class="sub_menu"> PC DeInstallation
                                </span></td>
                        </tr>
                         <tr>
                                    <td align ="right" > <%--  <a href="#" class="LinkButtons" onclick="window.close();  window.opener.document.forms['form1'].submit();">Close</a>&nbsp;&nbsp;&nbsp;--%></td>
                                  </tr>
                        <tr>
                            <td class="heading center" >
                                &nbsp; PC DeInstallation</td>
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
                                                                   <td align ="center" valign ="top" style="height: 26px"  ><asp:Label id="lblConfirm" runat="server" CssClass="Gridheading"></asp:Label> &nbsp;&nbsp;<asp:Button ID="btnYes"  runat ="server" Text ="Yes" CssClass="button topMargin" />&nbsp;&nbsp;<asp:Button ID="btnNo"  runat ="server" Text ="No" CssClass="button topMargin" /></td>                                                                                                                        
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
                                                                    <td style="width:15%; height: 22px;">
                                                                    </td>
                                                                    <td class="textbold" style="width:20%; height: 22px;">
                                                                         Challan No.</td>
                                                                    <td style="width: 35%; height: 22px;">
                                                                        <asp:TextBox ID="txtChallanNo" runat="server" CssClass="textbox" MaxLength="17"
                                                                            TabIndex="1" Width="155px"></asp:TextBox></td>                                           
                                                                    <td style="width: 5%; height: 22px;">
                                                                        &nbsp;</td>
                                                                    <td class="left" style="width: 25%; height: 22px;">
                                                                       <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="6" AccessKey="S"  /></td>
                                                                </tr>
                                                               
                                                                <tr>
                                                                    <td style="width:15%">
                                                                    </td>
                                                                    <td class="textbold" style="width:20%">
                                                                        Order No.<span class="Mandatory" >*</span></td>
                                                                   <td style="width: 35%">
                                                                        <asp:TextBox ID="txtOrderNo" runat="server" CssClass="textbox" MaxLength="12" ReadOnly="false"
                                                                            TabIndex="2" Width="156px"></asp:TextBox></td>                                           
                                                                   
                                                                    <td style="width: 5%">
                                                                        </td>
                                                                    <td class="left" style="width: 25%"><asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="7" Visible="False" AccessKey="R" /></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td style="width:15%">
                                                                    </td>
                                                                    <td class="textbold" style="width:20%">
                                                                       DeInstallation Date</td>
                                                                        <td style="width: 35%">
                                                                        <asp:TextBox ID="txtDeInstallDate" runat="server" CssClass="textboxgrey" MaxLength="10"
                                                                            ReadOnly="True" TabIndex="3" Width="158px"></asp:TextBox>
                                                                            <img id="f_DateDeInstall" alt="" src="../Images/calender.gif" style="cursor: pointer" runat="server" 
                                                                            tabindex="4" title="Date selector" />

                                                                        <script type="text/javascript">
                                                                                                                            Calendar.setup({
                                                                                                                            inputField     :    '<%=txtDeInstallDate.clientId%>',
                                                                                                                            ifFormat       :    "%d/%m/%Y",
                                                                                                                            button         :    "f_DateDeInstall",
                                                                                                                            //align          :    "Tl",
                                                                                                                            singleClick    :    true
                                                                                                                            });
                                                                        </script>
                                                                        </td>
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
                                                          <td>  <input id="hdLcode" runat="server" style="width: 5px" type="hidden" />
                                                                        <asp:HiddenField ID="SaveStatus" runat="server" />
                                                                        <asp:HiddenField ID="hdblnOrderOverride" runat="server" Value ="0" />
                                                                        <asp:HiddenField ID="hdAllowSaveForOrder" runat="server" Value ="0" />
                                                                        <asp:HiddenField ID="hdOrderNoExist" runat="server" Value ="0" />
                                                                         <asp:HiddenField ID="hdblnChallanOverride" runat="server" Value ="0" />
                                                                          <asp:HiddenField ID="hdChallanNoExist" runat="server" Value ="0" />
                                                                          <asp:HiddenField ID="hdAllowSaveForChallan" runat="server" Value ="0" />
                                                                        
                                                                        
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
  </form>                   
</body>
</html>
