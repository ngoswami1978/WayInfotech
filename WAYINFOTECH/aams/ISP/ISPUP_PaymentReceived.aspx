<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ISPUP_PaymentReceived.aspx.vb" Inherits="ISP_ISPUP_PaymentReceived" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ISP Payment Received</title>
     <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet"/>
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>   
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
    <script type="text/javascript" src="../Calender/calendar.js"></script>
    <!-- import the language module -->
    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
    <!-- import the calendar setup module -->
    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>
     <script language="javascript" type="text/javascript">
        function CheckValidation()
        {
          if (document.getElementById('<%=drpSentToAc.ClientId%>').selectedIndex==0)
          {
          
              document.getElementById('lblError').innerHTML='PA Sent to A/c is mandatory.';
             document.getElementById('<%=drpSentToAc.ClientId%>').focus();
             return false;
          }
           if (document.getElementById('<%=drpRecToAc.ClientId%>').selectedIndex==0)
          {
               document.getElementById('lblError').innerHTML='PA Received to A/c is mandatory.';
             document.getElementById('<%=drpRecToAc.ClientId%>').focus();
             return false;
          }
          
          if   (document.getElementById('<%=txtChqNo.ClientId%>').value.trim()!='')
                 {  
                    if (document.getElementById('<%=txtChqDate.ClientId%>').value.trim()=='')
                  {
                       document.getElementById('lblError').innerHTML='Cheque date is mandatory.';
                     document.getElementById('<%=txtChqDate.ClientId%>').focus();
                     return false;
                  }
                              if(document.getElementById('<%=txtChqDate.ClientId%>').value != '')
                 {
                    if (isDate(document.getElementById('<%=txtChqDate.ClientId%>').value,"d/M/yyyy") == false)	
                    {
                         document.getElementById('lblError').innerHTML = "Cheque date is not valid.";			
	                   document.getElementById('<%=txtChqDate.ClientId%>').focus();
	                   return(false);  
                    }
                 } 
                  
                               if (document.getElementById('<%=txtEmployeeName.ClientId%>').value=='')
                  {
                       document.getElementById('lblError').innerHTML='Cheque sent to is mandatory.';
                    // document.getElementById('<%=txtEmployeeName.ClientId%>').focus();
                     return false;
                  }
                
                }
         
          
//            if (document.getElementById('<%=txtChqNo.ClientId%>').value.trim()=='')
//          {
//             document.getElementById('<%=lblError.ClientId%>').innerText='Cheque No. is mandatory.'
//             document.getElementById('<%=txtChqNo.ClientId%>').focus();
//             return false;
//          }
//           if (document.getElementById('<%=txtChqNo.ClientId%>').value.trim()=='')
//          {
//             document.getElementById('<%=lblError.ClientId%>').innerText='Cheque No. is mandatory.'
//             document.getElementById('<%=txtChqNo.ClientId%>').focus();
//             return false;
//          }
        
           if(document.getElementById('<%=txtChqDate.ClientId%>').value != '')
         {
            if (isDate(document.getElementById('<%=txtChqDate.ClientId%>').value,"d/M/yyyy") == false)	
            {
               document.getElementById('lblError').innerHTML= "Cheque date is not valid.";			
	           document.getElementById('<%=txtChqDate.ClientId%>').focus();
	           return(false);  
            }
         } 
          if (document.getElementById('<%=txtAmt.ClientId%>').value.trim()=='')
          {
               document.getElementById('lblError').innerHTML='Amount is mandatory.';
             document.getElementById('<%=txtAmt.ClientId%>').focus();
             return false;
          }
          
           if (document.getElementById('<%=txtAmt.ClientId%>').value!='')
          {
            if(IsDataValid(document.getElementById("txtAmt").value,5)==false)
              {
                  document.getElementById('lblError').innerHTML='Amount is not valid.';
                 document.getElementById('<%=txtAmt.ClientId%>').focus();
                 return false;
               }
          }
          // alert('' + parseInt(document.getElementById('<%=txtAmt.ClientId%>').value);
          
       
         
        }
        
        
         function PopupEmployee()
        {
                       var type;    
                                           
                     var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
                    if (strEmployeePageName!="")
                    {
                        type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;  
                        //type = "../Setup/MSSR_Employee.aspx?Popup=T" ;
   	                    window.open(type,"POPEmp","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
                        return false;
                    }
                            
                                     
        }

    
    </script>
</head>
<body onload="window.focus();" >
    <form id="form1" runat="server" >
      <table width="810px"  class="border_rightred" id="tbl" runat="server" >
            <tr>
                <td valign="top" style="height: 520px">
                    <table width="100%" >
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">ISP-></span><span class="sub_menu">Payment Received</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" >
                                ISP Payment Received&nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top" >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                               <table width="100%" border="0" cellpadding="0" cellspacing="0" >                                                          
                                                            <tr> 
                                                                <td  class="textbold" colspan="6" align="center" valign="middle" style="height: 22px">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr> 
                                                             <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>                                                            
                                                            <tr>
                                                                <td  class="textbold" style="height:22px;width:6%">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height:22px;width:20%">
                                                                    </td>
                                                                <td class="textbold" style="height:22px;width:15%">
                                                                    PA No.</td>
                                                                <td  style="height:22px;width:30%" >                                                                    
                                                                   <asp:TextBox ID="txtPANO" runat ="server" CssClass ="textboxgrey" Width="235px" TabIndex="1" MaxLength="3" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                                <td  style="height:22px;width:5%">
                                                                 </td>
                                                                <td style="height:22px;width:45%">
                                                                    <asp:Button ID="btnSave" runat="server" CssClass="button" TabIndex="6" Text="Save" AccessKey="S" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                               <tr>
                                                                <td  class="textbold" style="height:22px;width:6%">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height:22px;width:9%">
                                                                    </td>
                                                                <td class="textbold" style="height:22px;width:15%">
                                                                    PA Sent To A/c<span class ="Mandatory">*</span> </td>
                                                                <td  style="height:22px;width:30%" >                                                                    
                                                                    <asp:DropDownList ID="drpSentToAc" runat="server" Width="235px">
                                                                    </asp:DropDownList></td>
                                                                <td  style="height:22px;width:5%">
                                                                 </td>
                                                                <td style="height:22px;width:45%">
                                                                    <asp:Button ID="btnReset" runat="server" CssClass="button" TabIndex="6" Text="Reset" AccessKey="R" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td   style="height:2px;" class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                               <tr>
                                                                <td  class="textbold" style="height:22px;width:6%">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height:22px;width:9%">
                                                                    </td>
                                                                <td class="textbold" style="height:22px;width:15%">
                                                                    PA Received In A/c<span class ="Mandatory">*</span> </td>
                                                                <td  style="height:22px;width:30%" >                                                                    
                                                                    <asp:DropDownList ID="drpRecToAc" runat="server" Width="235px">
                                                                    </asp:DropDownList>&nbsp;</td>
                                                                <td  style="height:22px;width:10%">
                                                                 </td>
                                                                <td style="height:22px;width:45%">
                                                                    </td>
                                                            </tr>
                                                              <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                               <tr>
                                                                <td  class="textbold" style="height:22px;width:6%">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height:22px;width:9%">
                                                                    </td>
                                                                <td class="textbold" style="height:22px;width:15%">
                                                                    Cheque No</td>
                                                                <td  style="height:22px;width:30%" >                                                                    
                                                                   <asp:TextBox ID="txtChqNo" runat ="server" CssClass ="textbox" Width="140px" TabIndex="1" MaxLength="8"></asp:TextBox>
                                                                </td>
                                                                <td  style="height:22px;width:10%">
                                                                 </td>
                                                                <td style="height:22px;width:45%">
                                                                    </td>
                                                            </tr>
                                                        <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                               <tr>
                                                                <td  class="textbold" style="height:22px;width:6%">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height:22px;width:9%">
                                                                    </td>
                                                                <td class="textbold" style="height:22px;width:15%">
                                                                    Cheque Date </td>
                                                                <td  style="height:22px;width:30%" >                                                                    
                                                                   <asp:TextBox ID="txtChqDate" runat ="server" CssClass ="textbox" Width="140px" TabIndex="1" MaxLength="10"></asp:TextBox>&nbsp; <img id="imgChqDate" alt="" src="../Images/calender.gif" title="Date selector"
                                                                                                                         style="cursor: pointer"  runat ="server" />  
                                                                                                                         <script type="text/javascript">
                                                                                                                                                Calendar.setup({
                                                                                                                                                inputField     :    '<%=txtChqDate.clientId%>',
                                                                                                                                                ifFormat       :    "%d/%m/%Y",
                                                                                                                                                button         :    "imgChqDate",
                                                                                                                                                //align          :    "Tl",
                                                                                                                                                singleClick    :    true
                                                                                                                                                });
                                                                                                                                                 </script>
                                                                </td>
                                                                <td  style="height:22px;width:10%">
                                                                 </td>
                                                                <td style="height:22px;width:45%">
                                                                    </td>
                                                            </tr>
                                                        <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                               <tr>
                                                                <td  class="textbold" style="height:22px;width:6%">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height:22px;width:9%">
                                                                    </td>
                                                                <td class="textbold" style="height:22px;width:15%">
                                                                    Amount <span class ="Mandatory">*</span> </td>
                                                                <td  style="height:22px;width:30%" >                                                                    
                                                                   <asp:TextBox ID="txtAmt" runat ="server" CssClass ="textbox" Width="140px" TabIndex="1" MaxLength="12"></asp:TextBox>
                                                                </td>
                                                                <td  style="height:22px;width:10%">
                                                                 </td>
                                                                <td style="height:22px;width:45%">
                                                                    </td>
                                                            </tr>
                                                             <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td  class="textbold" style="height:22px;width:6%">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height:22px;width:9%">
                                                                    </td>
                                                                <td class="textbold" style="height:22px;width:15%">Cheque Sent To </td>
                                                                <td  style="height:22px;width:30%" >                                                                    
                                                                    <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="textboxgrey" MaxLength="3"
                                                                        ReadOnly="True" TabIndex="1" Width="235px"></asp:TextBox>
                                                                    &nbsp;</td>
                                                                <td  style="height:22px;width:10%" valign ="top"  ><img tabIndex="26" id="img1A" src="../Images/lookup.gif" onclick="javascript:return PopupEmployee();" runat="server" style="cursor:pointer;"   />&nbsp;
                                                                 </td>
                                                                <td style="height:22px;width:45%">
                                                                    </td>
                                                            </tr>
                                                              <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                               <tr>
                                                                <td  class="textbold" style="height:22px;width:6%">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height:22px;width:9%">
                                                                    </td>
                                                                <td class="textbold" style="height:22px;width:15%">
                                                                    Remark&nbsp;
                                                                </td>
                                                                <td  colspan="2" style="height:22px;width:25%" >                                                                    
                                                                   <asp:TextBox ID="txtRemark" runat ="server" CssClass ="textbox" Width="314px" TabIndex="1" MaxLength="3" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                                </td>                                                               
                                                                <td style="height:22px;width:45%"><asp:HiddenField ID="hdMonth" runat ="server" /><asp:HiddenField ID="hdYear" runat ="server" />
                                                                        <asp:HiddenField ID="hdEmployeeID" runat="server" />                                      
                                                                        <input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' />
                                                                    </td>
                                                            </tr>
                                                      <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 23px">
                                                                    &nbsp;</td>
                                                               <td  >
                                                                    </td>
                                                                <td  colspan="2" class="ErrorMsg" style="height: 23px">
                                                                    Field Marked * are Mandatory</td>
                                                                <td style="height: 23px">
                                                                    &nbsp;</td>
                                                                <td style="height: 23px">
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