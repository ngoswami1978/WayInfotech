<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_ISPPlan.aspx.vb" Inherits="ISP_MSUP_ISPPlan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
      <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
      <script src="../JavaScript/AAMS.js" type="text/javascript"></script> 
   
            
    <script type="text/javascript" language ="javascript" >
      
    function ShowBillCycle()
    {
     if(document.getElementById("drpBillFreq").value=='0' || document.getElementById("drpBillFreq").value=='M')
        {
            document.getElementById("DivCycleDate").style.display ="none";
            document.getElementById("DivCycle").style.display="block";
            return false;
        } 
        else
        {            
             document.getElementById("DivCycleDate").style.display="block";
             document.getElementById("DivCycle").style.display="none";
            if(document.getElementById("drpBillFreq").value=='Q')
            {
            document.getElementById("Label1").innerHTML= "Bill Day Every Quarter";
            }
             if(document.getElementById("drpBillFreq").value=='H')
            {
            document.getElementById("Label1").innerHTML= "Bill Day Every half yearly";
            }
            if(document.getElementById("drpBillFreq").value=='Y')
            {
            document.getElementById("Label1").innerHTML= "Bill Day Every yearly";
            }
            
              return false;
        }
    }  
          
    function validateIspPlan()
    {
      if(document.getElementById("drpISPProvider").selectedIndex==0)
            {
                document.getElementById("lblError").innerHTML='Provider Name is Mandatory.';
                document.getElementById("drpISPProvider").focus();
                return false;
            }   
            if(document.getElementById("txtNPID").value.trim()=='')
                {
                    document.getElementById("lblError").innerHTML='NPID is Mandatory.';
                    document.getElementById("txtNPID").focus();
                    return false;
                }


        if(document.getElementById("txtBandWidth").value.trim()=='')
            {
                document.getElementById("lblError").innerHTML='Bandwidth is Mandatory.';
                document.getElementById("txtBandWidth").focus();
                return false;
            }
    
        if(document.getElementById("txtInstallationCharge").value.trim()=='')
        {
            document.getElementById("lblError").innerHTML='Installation Charge is Mandatory.';
            document.getElementById("txtInstallationCharge").focus();
            return false;
        }    
         if(document.getElementById("drpBillFreq").value=='0')
        {
            document.getElementById("lblError").innerHTML='Bill Frequency is Mandatory.';
            document.getElementById("drpBillFreq").focus();
            return false;
        }
        
        
            if(document.getElementById("drpBillFreq").value=='M')
         {
               if((document.getElementById("txtBillFrom").value.trim()=='') )
                   {
                    document.getElementById("lblError").innerHTML='Billing Cycle From is Mandatory.';
                    document.getElementById("txtBillFrom").focus();
                    return false;
                    } 
                if( (document.getElementById("txtBillTo").value.trim()=='') )
                   {
                    document.getElementById("lblError").innerHTML='Billing Cycle To is Mandatory.';
                    document.getElementById("txtBillTo").focus();
                    return false;
                    }      
                
                   if(parseInt(document.getElementById("txtBillFrom").value)>parseInt("31") || parseInt(document.getElementById("txtBillFrom").value)< parseInt("1"))
               {
                    document.getElementById("lblError").innerHTML='Billing Cycle From Range is not valid.';
                    document.getElementById("txtBillFrom").focus();
                    return false;     
                 }      
                    
                 if(parseInt(document.getElementById("txtBillTo").value)>parseInt("31") || parseInt(document.getElementById("txtBillTo").value)<parseInt("1"))
               {
                    document.getElementById("lblError").innerHTML='Billing Cycle To Range is not valid.';
                    document.getElementById("txtBillTo").focus();
                    return false;     
                 } 
                 
//                    if(parseInt(document.getElementById("txtBillTo").value)< parseInt(document.getElementById("txtBillFrom").value))
//               {
//                    document.getElementById("lblError").innerHTML='Bill To Can be less than Bill From';
//                    document.getElementById("txtBillTo").focus();
//                    return false;     
//                 }    


               if(parseInt(document.getElementById("txtBillTo").value)== parseInt(document.getElementById("txtBillFrom").value))
                  {
                    document.getElementById("lblError").innerHTML="Billing Cycle is not Valid.";
                    document.getElementById("txtBillTo").focus();
                    return false;     
                 }    
                 var StartDay= parseInt(document.getElementById("txtBillFrom").value);
                 var EndDay=   parseInt(document.getElementById("txtBillTo").value);
//                 var StartDate=new Date();                
//                  StartDate.setMonth(6);
//                  StartDate.setDate(StartDay);
//                  StartDate.setHours(0);
//                  var EndDate=  new Date();
//               
//                 EndDate.setMonth(7);
//                 EndDate.setDate(EndDay);
//                 EndDate.setHours(0);
                // alert(StartDate);
                // alert(EndDate);                 
                // alert(days_between(StartDate,EndDate)+1);
//                 if (StartDay >15 && StartDay < EndDay)
//                 { 
//                     if ((days_between(StartDate,EndDate)+1) > 31 )
//                    {                 
//                         window.document.getElementById('txtBillFrom').focus();
//                         window.document.getElementById('lblError').innerHTML="Billing Cycle is not Valid.";
//                         return false;
//                   }
                // }
                
                if(parseInt(document.getElementById("txtBillFrom").value)>1)
                  {
                   if  ( (parseInt(EndDay) + 1 ) != (parseInt(StartDay)))
                   {
                          window.document.getElementById('txtBillFrom').focus();
                          window.document.getElementById('lblError').innerHTML="Billing Cycle is not Valid.";
                          return false;
                   }
                  } 
                  
                  else
                  {                      
                      if (parseInt(EndDay) !=28 &&  parseInt(EndDay) !=29 &&  parseInt(EndDay) !=30 &&  parseInt(EndDay) !=31)
                      {
                         if  ( (parseInt(EndDay) + 1 ) != (parseInt(StartDay)))
                           {
                                  window.document.getElementById('txtBillFrom').focus();
                                  window.document.getElementById('lblError').innerHTML="Billing Cycle is not Valid.";
                                  return false;
                           }
                       }
                  }
           
        }
     
     }
  
    </script>
   
   <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
    <!-- import the calendar script -->
    <script type="text/javascript" src="../Calender/calendar.js"></script>
    <!-- import the language module -->
    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
    <!-- import the calendar setup module -->
    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

</head>
<body>
    <form id="form1" runat="server">
      <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top" style="height: 482px">
                    <table width="860px" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">ISP-&gt;</span><span class="sub_menu">ISP Plan Information</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage <span style="font-family: Microsoft Sans Serif">ISP&nbsp; Plan Information</span>
                                    </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="860px" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder" > 
                                          <table style="width: 860px" border="0" cellpadding="1" cellspacing="2" class="left textbold">
                                            <tr>
                                               
                                               <td style="width: 50px"></td> 
                                               <td colspan="5" align ="center" >
                                                    <asp:Label ID="lblError" runat="server"  CssClass="ErrorMsg"></asp:Label></td> 
                                                <td class="gap">
                                                </td>
                                              
                                               
                                                
                                            </tr>
                                    <tr>
                                    
                                    <td style="width: 50px"></td>
                                    
                                        <td style="width: 150px" >
                                            ISP Name<span class="Mandatory" >*</span></td>
                                        <td  class="textbold" style="width: 160px">
                                        <%--<asp:DropDownList ID="drpISPProvider" runat="server" CssClass="dropdownlist" TabIndex="1" Width="129px">                                        
                                        </asp:DropDownList>--%>
                                        <asp:DropDownList ID="drpISPName" runat="server" CssClass="dropdownlist" TabIndex="1" Width="129px">                                        
                                        </asp:DropDownList>                                       
                                        
                                        </td>
                                        
                                        <td style="width: 20px" >
                                                </td>
                                                
                                        <td style="width: 180px">
                                            Contention Ration</td>
                                        <td style="width: 150px">
                                         <asp:TextBox ID="txtContentionRation" runat="server" CssClass="textbox" MaxLength="10" Width="120px" TabIndex="2"></asp:TextBox></td>
                                        <td style="width: 120px">
                                       <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="19" AccessKey="S" />
                                       </td>
                                    </tr>
                                    
                                    <tr>
                                    <td style="width: 50px;"></td>
                                        <td style="height: 25px" >
                                            NPID<span class="Mandatory" >*</span></td>
                                        <td  class="textbold" >
                                                                   <asp:TextBox ID="txtNPID" runat ="server" CssClass ="textbox" MaxLength="20" TabIndex="3" Width="120px"></asp:TextBox></td>
                                                                   
                                                                   
                                                                   <td style="width: 20px;">
                                                </td>
                                                
                                                
                                        <td  class="textbold">
                                            Monthly Charge
                                        </td>
                                        <td>
                                         <asp:TextBox ID="txtMonthlyCharge" runat="server" CssClass="textbox" MaxLength="10" Width="120px" TabIndex="4"></asp:TextBox></td>
                                        <td>
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="20" AccessKey="N" /></td>
                                    </tr>
                                    <tr>
                                    <td style="width: 50px"></td>
                                        <td style="height: 24px;">
                                            Bandwidth <span class="Mandatory" >*</span></td>
                                        <td class="textbold">
                                            <asp:TextBox ID="txtBandWidth" runat="server" CssClass="textbox" MaxLength="20" TabIndex="5" Width="120px"></asp:TextBox></td>
                                        <td style="width: 20px">
                                                </td>
                                                
                                                
                                        <td class="textbold" >
                                           Equipment Monthly Rental</td>
                                        
                                        <td>
                                         <asp:TextBox ID="txtMonthlyRental" runat="server" CssClass="textbox" MaxLength="10" Width="120px" TabIndex="6"></asp:TextBox></td>
                                                                    <td> <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="21" AccessKey="R" /></td>
                                    </tr>
                                    <tr>
                                    <td style="width: 50px"></td>
                                        <td style="height: 21px;" nowrap="false">
                                            Installation Charge <span class="Mandatory" >*</span></td>
                                        <td class="textbold">
                                         <asp:TextBox ID="txtInstallationCharge" runat="server" CssClass="textbox" MaxLength="10" TabIndex="7" Width="120px"></asp:TextBox></td>
                                         
                                         
                                         <td style="width: 20px">
                                                </td>
                                                
                                                
                                        <td nowrap="nowrap" >
                                            Equip. One Time Charges</td>
                                        <td>
                                         <asp:TextBox ID="txtOnetimeCharges" runat="server" CssClass="textbox" MaxLength="10" Width="120px" TabIndex="8"></asp:TextBox></td>
                                        <td>
                                                                    </td>
                                    </tr>
                                    
                                        <tr>
                                        <td style="width: 50px"></td>
                                        <td nowrap="nowrap" style="width: 130px"  >
                                            Equipment Included<%--<span class="Mandatory" >*</span>--%></td>
                                        <td class="textbold" nowrap="nowrap" >
                                            <asp:CheckBox ID="chkEquiIncluded" runat="server" TabIndex="9" Width="88px" /></td>
                                            
                                            <td style="width: 20px" >
                                                </td>
                                                
                                        <td>
                                            VAT Percentage</td>
                                        <td>
                                         <asp:TextBox ID="txtVatPercent" runat="server" CssClass="textbox" MaxLength="10" Width="120px" TabIndex="10"></asp:TextBox></td>
                                        <td>
                                                                    </td>
                                    </tr>    
                                            
                                        <tr>   
                                        <td style="width: 50px"></td>                                     
                                        <td style="height: 21px;">
                                            Billing Frequency<span class="Mandatory">*</span></td>
                                        <td class="textbold">
                                            <asp:DropDownList ID="drpBillFreq" runat="server" CssClass="dropdownlist" TabIndex="11" Width="129px">
                                                <asp:ListItem Text="Select One" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Monthly" Value="M"></asp:ListItem>
                                                <asp:ListItem Text="Quaterly" Value="Q"></asp:ListItem>
                                                <asp:ListItem Text="Half Yearly" Value="H"></asp:ListItem>
                                                <asp:ListItem Text="Yearly" Value="Y"></asp:ListItem>                                                
                                            </asp:DropDownList></td>
                                            
                                            <td style="width: 20px">
                                                </td>
                                                
                                        <td>
                                            Delivery Time Line</td>
                                        <td>
                                         <asp:TextBox ID="txtDTimeLine" runat="server" CssClass="textbox" MaxLength="10" Width="120px" TabIndex="12"></asp:TextBox></td>
                                        <td>
                                                                    </td>
                                                                   
                                                                    
                                    </tr>       
                                           
                                    
                                        
                                       <tr id="DivCycle" runat ="server">  
                                        <td style="width: 50px"></td>                                      
                                        <td style="height: 21px;">
                                            Billing Cycle<span class="Mandatory" >*</span> </td>
                                        <td class="textbold">
                                            <asp:TextBox ID="txtBillFrom" runat="server" CssClass="textbox" MaxLength="2" Width="50px" TabIndex="13"></asp:TextBox>
                                        <span class="txtbold">-</span>
                                            <asp:TextBox ID="txtBillTo" runat="server" CssClass="textbox" MaxLength="2" Width="54px" TabIndex="14"></asp:TextBox></td>
                                        
                                        <td style="width: 20px">
                                                </td>
                                                
                                        <td>
                                            Bill Date Every Month</td>
                                        <td><asp:DropDownList ID="drpBillDateEveryMonth" runat="server" CssClass="dropdownlist" Width="126px" TabIndex="15">
                                            <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td>
                                                                    </td>
                                                                    
                                    </tr> 
                                 
                                         <tr  id="DivCycleDate" runat ="server">  
                                        <td style="width: 50px"></td>                                      
                                        <td style="height: 21px;">
                                             Bill Start Month<span class="Mandatory" >*</span> </td>
                                        <td class="textbold">
                                            <asp:DropDownList ID="drpMonthFrom" runat="server" CssClass="dropdownlist" TabIndex="16"
                                                Width="129px" Visible="False">
                                                <asp:ListItem Value="1">January</asp:ListItem>
                                                <asp:ListItem Value="2">February</asp:ListItem>
                                                <asp:ListItem Value="3">March</asp:ListItem>
                                                <asp:ListItem Value="4">April</asp:ListItem>
                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                <asp:ListItem Value="6">June</asp:ListItem>
                                                <asp:ListItem Value="7">July</asp:ListItem>
                                                <asp:ListItem Value="8">August</asp:ListItem>
                                                <asp:ListItem Value="9">september</asp:ListItem>
                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                <asp:ListItem Value="12">December</asp:ListItem>
                                            </asp:DropDownList></td>
                                        
                                        <td style="width: 20px">
                                                </td>
                                                
                                        <td>
                                           <asp:Label ID="Label1"  Text =""  runat ="server" ></asp:Label></td>
                                        <td><asp:DropDownList ID="drpBillDateEveryFrequency" runat="server" CssClass="dropdownlist" Width="125px" TabIndex="17">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList></td>
                                        <td>
                                                                    </td>
                                                                   
                                                                    
                                    </tr>         
                                            
                                           <tr>                                        
                                        <td style="width: 50px;">
                                        </td>
                                        <td style="height: 21px;" class="textbold">
                                            Remarks 
                                            </td>
                                        <td  style="height: 21px;" colspan="4" rowspan="3">
                                         <asp:TextBox ID="txtRemarks" runat="server" CssClass="textbox" MaxLength="300"  TextMode="MultiLine" Height="56px" Width="494px" TabIndex="18"></asp:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>      
                                           
                                               <tr>                                        
                                        <td style="width: 50px;">
                                        </td>
                                        <td style="height: 21px;" class="textbold">
                                            </td>
                                        <td>
                                         </td>
                                    </tr>   
                                          
                                           
                                           
                                           
                                            <tr>                                        
                                        <td style="width: 50px;">
                                        </td>
                                        <td style="height: 21px;" class="textbold">
                                            </td>
                                        <td>
                                         </td>
                                    </tr>   
                                           
                                           
                                             <tr>                                        
                                        <td style="width: 50px;">
                                        </td>
                                        <td style="height: 21px;" class="textbold">
                                            </td>
                                        <td>
                                         </td>
                                        <td style="width: 20px">
                                        </td>
                                        <td>
                                                                    </td>
                                                                     <td>
                                                                    </td> <td>
                                                                    </td>
                                    </tr>        
                                           
                                             <tr>                                        
                                        <td style="width: 50px;">
                                        </td>
                                        <td style="height: 21px;" class="textbold">
                                            </td>
                                        <td>
                                        <asp:DropDownList ID="drpBandWidth" runat="server" CssClass="dropdownlist" Visible="False"  >
                                        <asp:ListItem Text="Select One"  Value="0"></asp:ListItem>                                    
                                        <asp:ListItem Text="128k" Value="128k"></asp:ListItem>
                                        <asp:ListItem Text="256kbps" Value="128k" ></asp:ListItem>
                                        <asp:ListItem Text="32k" Value="32k"  ></asp:ListItem>
                                        <asp:ListItem Text="512kbps" Value="512kbps" ></asp:ListItem>
                                        <asp:ListItem Text="64k" Value="64k"  ></asp:ListItem>
                                        <asp:ListItem Text="64kbps" Value="64kbps" ></asp:ListItem>                                        
                                        </asp:DropDownList></td>
                                        <td style="width: 20px">
                                        </td>
                                        <td>
                                                                    </td>
                                                                     <td>
                                                                    </td> <td>
                                                                    </td>
                                    </tr>      
                                           
                                       
                                    <tr>
                                        <td style="width: 50px">
                                        </td>
                                        <td class="ErrorMsg" colspan="5">
                                            Field Marked * are Mandatory</td>
                                        <td>
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
<script type ="text/javascript" language ="javascript"  >
   ShowBillCycle();
</script>
</html>

