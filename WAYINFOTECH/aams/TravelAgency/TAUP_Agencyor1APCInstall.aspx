<%@ Page Language="VB"  ValidateRequest ="false"  EnableEventValidation="false" AutoEventWireup="false" CodeFile="TAUP_Agencyor1APCInstall.aspx.vb" Inherits="TravelAgency_TAUP_Agencyor1APCInstall" %>
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
  
  
   
   function AgencyPCMandatory()
      {

       try
       {        
         
          if(document.getElementById('<%=txtInstallDate.ClientId%>').value == '')
         {
               document.getElementById('<%=lblError.ClientId%>').innerText = document.getElementById('<%=dtIR.ClientId%>').innerText  + " is mandatory.";
               
	          // document.getElementById('<%=txtInstallDate.ClientId%>').focus();
	           return false;  
         }
            if(document.getElementById('<%=txtInstallDate.ClientId%>').value != '')
         {
            if (isDate(document.getElementById('<%=txtInstallDate.ClientId%>').value,"d/M/yyyy") == false)	
            {
               document.getElementById('<%=lblError.ClientId%>').innerText = "Enter valid date of Installation.";			
	           document.getElementById('<%=txtInstallDate.ClientId%>').focus();
	           return false ;  
            }
         } 

		   
         return true;
         }catch(ex)
         {
            document.getElementById("lblError").innerHTML=ex;
         }
     }
    function ResetSaveForChallan()
      {
          document.getElementById("hdAllowSaveForChallan").value="0";  
          return true;   
      }
      
      function ResetSaveForOrder()
      {
       document.getElementById("hdAllowSaveForOrder").value="0";  
       return true;    
      }
        function ResetSaveForCpuNo()
      {
       document.getElementById("hdAllowSaveForCpuNo").value="0";  
       return true;    
      }
           function ResetSaveForMonNo()
      {
       document.getElementById("hdAllowSaveForMonNo").value="0";  
       return true;    
      }
    </script>
    
  
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body  >
    <form id="form1" runat="server"   defaultfocus ="txtOrderNo"  >    
      <table width="800"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Agency -&gt;</span><span class="sub_menu"><asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                </span></td>
                        </tr>
                         <tr>
                                    <td align ="right" >   <a href="#" class="LinkButtons" onclick="window.close();  window.opener.document.forms['form1'].submit();">Close</a>&nbsp;&nbsp;&nbsp;</td>
                                  </tr>
                        <tr>
                            <td class="heading center"   >
                                <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                                &nbsp;</td>
                        </tr>
                        <tr >
                            <td >
                               <table width="100%" border="0" cellspacing="0" cellpadding="0">                                 
                                    <tr>
                                        <td class="redborder center">
                                            <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">                                                
                                                <tr>
                                                    <td class="center"   height="25px" >
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
                                                       <td> 
                                                        <asp:Panel  ID="pnlNo" runat="server"  width="100%" Visible ="false"  >
                                                               <table border ="0" cellpadding ="0" cellspacing ="0"  width="100%"  > 
                                                                <tr>
                                                                               <td class="Gridheading" colspan="2"><asp:Label ID="lblMonOrCpuNo"    Text ="" runat ="server" ></asp:Label></td>
                                                                                       
                                                                           </tr>
                                                                   <tr>
                                                                               <td class="right textbold" colspan="2" >
                                                                                <span class="button"  >Would you like to install it at this Location also?</span>
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
                                                        </asp:Panel> 
                                                        </td> 
                                                     </tr>                                                                              
                                                    <tr>
                                                      <td align ="right" ></td>
                                                    </tr>
                                                    <tr>
                                                       <td  > 
                                                          <asp:Panel  ID="PnlDetails" runat="server"   Width ="100%"  >
                                                                <table border ="0" cellpadding ="0" cellspacing ="0" Width ="100%" >                                                                       
                                                                    <tr>
                                                                       <td style ="width:3%" valign ="top" ></td>
                                                                       <td  align ="center" valign ="top" >
                                                                           <div id="divRead" runat ="server" >
                                                                                   <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                                        <tr>
                                                                                            <td class="textbold" style="width:35%">
                                                                                                Challan No.</td>
                                                                                            <td style="width: 65%" colspan ="3">
                                                                                                <asp:TextBox ID="txtRChallanNo" runat="server" CssClass="textboxgrey" MaxLength="17"
                                                                                                    TabIndex="1" Width="155px" ReadOnly="True"></asp:TextBox></td> 
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="textbold" style="width:35%; height :35px;" >Date of Installation.<span class="Mandatory" ></span></td>
                                                                                            <td  style="width: 65%" colspan ="3">
                                                                                                <asp:TextBox ID="txtRInstallDate" runat="server" CssClass="textboxgrey" MaxLength="10"
                                                                                                    ReadOnly="True" TabIndex="2" Width="130px"></asp:TextBox>&nbsp;<img id="f_RDateInstall" alt="" src="../Images/calender.gif" style="cursor: pointer"
                                                                                                      tabindex="2" title="Date selector" /></td>
                                                                                        </tr>
                                                                                        <tr>                                                                      
                                                                                            <td class="textbold" style="width:35%">
                                                                                                Order No.</td>
                                                                                            <td  style="width: 65%" colspan ="3">
                                                                                                <asp:TextBox ID="txtRorderNo" runat="server" CssClass="textboxgrey" MaxLength="12" TabIndex="4"
                                                                                                    Width="155px" ReadOnly="True"></asp:TextBox></td>   
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="textbold" style="width:35%">
                                                                                                Cpu Type</td>
                                                                                            <td style="width: 65%" colspan ="3">
                                                                                                <asp:DropDownList ID="drpRCpuType" runat="server" Width="160px" CssClass="dropdown" Enabled="False" TabIndex="5">
                                                                                                </asp:DropDownList></td>  
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="textbold" style="width:35%">
                                                                                                Cpu No.</td>
                                                                                            <td  style="width: 65%" colspan ="3">
                                                                                                <asp:TextBox ID="txtRCpuNo" runat="server" CssClass="textboxgrey" MaxLength="25"
                                                                                                    TabIndex="6" Width="155px" ReadOnly="True"></asp:TextBox></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="textbold" style="width:35%">
                                                                                                Monitor Type.</td>
                                                                                            <td style="width: 65%" colspan ="3">
                                                                                                <asp:DropDownList ID="drpRMonType" runat="server" Width="160px" CssClass="dropdown" Enabled="False" TabIndex="7">
                                                                                                </asp:DropDownList></td> 
                                                                                        </tr>
                                                                                        <tr>                                                                     
                                                                                            <td class="textbold" style="width:35%">
                                                                                                Monitor No.</td>
                                                                                            <td  style="width: 65%" colspan ="3">
                                                                                                <asp:TextBox ID="txtRMonNo" runat="server" CssClass="textboxgrey" MaxLength="25"
                                                                                                    TabIndex="8" Width="155px" ReadOnly="True"></asp:TextBox></td> 
                                                                                        </tr>
                                                                                        <tr>                                                                     
                                                                                            <td class="textbold" style="width:35%">
                                                                                                Keyboard Type.</td>
                                                                                            <td  style="width: 65%" colspan ="3">
                                                                                                <asp:DropDownList ID="drpRKeyType" runat="server" Width="160px" CssClass="dropdown" Enabled="False" TabIndex="9">
                                                                                                </asp:DropDownList></td> 
                                                                                        </tr>
                                                                                        <tr>                                                                     
                                                                                            <td class="textbold" style="width:35%">
                                                                                                Keyboard No.</td>
                                                                                            <td  style="width: 65%" colspan ="3">
                                                                                                <asp:TextBox ID="txtRKeyboardNo" runat="server" CssClass="textboxgrey" MaxLength="25"
                                                                                                    TabIndex="10" Width="155px" ReadOnly="True"></asp:TextBox></td> 
                                                                                        </tr>
                                                                                        <tr>                                                                      
                                                                                            <td class="textbold" style="width:35%;height:30px;">
                                                                                                Mouse Type</td>
                                                                                            <td  style="width: 65%;" colspan ="3"><asp:DropDownList ID="drpRMouseType" runat="server" Width="160px" CssClass="dropdown" Enabled="False" TabIndex="11">
                                                                                                </asp:DropDownList></td> 
                                                                                        </tr>
                                                                                        <tr>                                                                   
                                                                                            <td class="textbold" style="width:35%;height:2px;">
                                                                                                Mouse No.</td>
                                                                                            <td  style="width: 65%" colspan ="3">
                                                                                                <asp:TextBox ID="txtRMouseNo" runat="server" CssClass="textboxgrey" MaxLength="25"
                                                                                                    TabIndex="12" Width="155px" ReadOnly="True"></asp:TextBox></td>
                                                                                        </tr>
                                                                                        <tr>                                                                      
                                                                                            <td class="textbold" style="width:35%;height:2px;">
                                                                                                Cdr No.</td>
                                                                                            <td  style="width: 65%" colspan ="3">
                                                                                                <asp:TextBox ID="txtRCdrNo" runat="server" CssClass="textboxgrey" MaxLength="25"
                                                                                                    TabIndex="13" Width="155px" ReadOnly="True"></asp:TextBox></td> 
                                                                                        </tr>
                                                                                        <tr>                                                                      
                                                                                            <td class="textbold" style="width:35%;height:2px;">
                                                                                                Addl. Ram.</td>
                                                                                            <td  style="width: 65%" colspan ="3">
                                                                                                <asp:TextBox ID="txtRRam" runat="server" CssClass="textboxgrey" MaxLength="25"
                                                                                                    TabIndex="14" Width="155px" ReadOnly="True"></asp:TextBox></td> 
                                                                                        </tr>
                                                                                        <tr>                                                                     
                                                                                            <td class="textbold" style="width:35%;height:2px;">
                                                                                               Remarks</td>
                                                                                            <td  style="width: 65%" colspan ="3">
                                                                                                <asp:TextBox ID="txtRRem" runat="server" CssClass="textboxgrey"  ReadOnly="True" TextMode ="MultiLine" 
                                                                                                    TabIndex="15" Width="154px" Height="80px"></asp:TextBox></td> 
                                                                                        </tr>
                                                                                        <tr>                                                                      
                                                                                            <td class="textbold" style="width:35%" > 
                                                                                            </td>
                                                                                            <td style="width:35%"></td>
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
                                                                                                &nbsp;</td>
                                                                                          
                                                                                        </tr>   
                                                                                 </table>
                                                                           </div>
                                                                       </td>
                                                                        <td align="center" valign="top">
                                                                            <div id="divEdit" runat="server">
                                                                                <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                                    <tr>
                                                                                        <td class="textbold" style="width: 35%">
                                                                                            Challan No
                                                                                        </td>
                                                                                        <td style="width: 50%" colspan="2">
                                                                                            <asp:TextBox ID="txtChallanNo" runat="server" CssClass="textbox" MaxLength="17" TabIndex="16"
                                                                                                Width="155px"></asp:TextBox></td>
                                                                                        <td style="width: 15%">
                                                                                            </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="5" width="100%">
                                                                                            <asp:Panel ID="pnlEnableorDisable" runat="server" Width="100%">
                                                                                                <table width="100%">
                                                                                                    <tr>
                                                                                                        <td class="textbold" style="width: 35%;">
                                                                                                            <asp:Label ID="dtIR" runat="server"></asp:Label>.<span class="Mandatory">*</span></td>
                                                                                                        <td style="width: 50%" colspan="3">
                                                                                                            <asp:TextBox ID="txtInstallDate" runat="server" CssClass="textboxgrey" MaxLength="10"
                                                                                                                ReadOnly="True" TabIndex="18" Width="134px"></asp:TextBox>&nbsp;<img id="f_DateInstall"
                                                                                                                    alt="" src="../Images/calender.gif" style="cursor: pointer" runat="server" tabindex="19"
                                                                                                                    title="Date selector" /></td>
                                                                                                        <td style="width: 15%">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="textbold" style="width: 35%">
                                                                                                            Order No.<span class="Mandatory">*</span></td>
                                                                                                        <td style="width: 50%" colspan="4">
                                                                                                            <%-- <asp:DropDownList ID="drpOrderNo" runat="server" CssClass="dropdown" TabIndex="19"
                                                                                                    Visible="False" Width="162px">
                                                                                                </asp:DropDownList>--%>
                                                                                                            <asp:TextBox ID="txtorderNo" runat="server" CssClass="textbox" MaxLength="12" TabIndex="20"
                                                                                                                Width="155px"></asp:TextBox></td>
                                                                                                                <td style="width: 15%">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="textbold" style="width: 35%">
                                                                                                            Cpu Type<span class="Mandatory">*</span></td>
                                                                                                        <td style="width: 50%" colspan="3">
                                                                                                            <asp:DropDownList ID="drpCpuType" runat="server" Width="160px" CssClass="dropdown"
                                                                                                                TabIndex="21" AutoPostBack="True">
                                                                                                            </asp:DropDownList></td>
                                                                                                        <td style="width: 15%">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="textbold" style="width: 35%">
                                                                                                            Cpu No.<span class="Mandatory">*</span></td>
                                                                                                        <td style="width: 50%" colspan="4">
                                                                                                            <asp:DropDownList ID="drpCpuNo" runat="server" CssClass="dropdown" TabIndex="22"
                                                                                                                Width="160px">
                                                                                                            </asp:DropDownList>
                                                                                                            <asp:TextBox ID="txtCpuNo" runat="server" CssClass="textbox" MaxLength="25" TabIndex="23"
                                                                                                                Width="155px"></asp:TextBox></td>
                                                                                                                <td style="width: 15%">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="textbold" style="width: 35%">
                                                                                                            Monitor Type<span class="Mandatory">*</span></td>
                                                                                                        <td style="width: 50%" colspan="3">
                                                                                                            <asp:DropDownList ID="drpMonType" runat="server" Width="160px" CssClass="dropdown"
                                                                                                                TabIndex="24" AutoPostBack="True">
                                                                                                            </asp:DropDownList></td>
                                                                                                        <td style="width: 15%">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="textbold" style="width: 35%">
                                                                                                            Monitor No.<span class="Mandatory">*</span></td>
                                                                                                        <td style="width: 65%" colspan="4">
                                                                                                            <asp:DropDownList ID="drpMonNo" runat="server" CssClass="dropdown" TabIndex="25"
                                                                                                                Width="160px">
                                                                                                            </asp:DropDownList>
                                                                                                            <asp:TextBox ID="txtMonNo" runat="server" CssClass="textbox" MaxLength="25" TabIndex="26"
                                                                                                                Width="155px"></asp:TextBox></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="textbold" style="width: 35%">
                                                                                                            Keyboard Type<span class="Mandatory">*</span></td>
                                                                                                        <td style="width: 50%" colspan="3">
                                                                                                            <asp:DropDownList ID="drpKeyType" runat="server" Width="160px" CssClass="dropdown"
                                                                                                                TabIndex="27">
                                                                                                            </asp:DropDownList></td>
                                                                                                        <td style="width: 15%">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="textbold" style="width: 35%">
                                                                                                            Keyboard No.<span class="Mandatory">*</span></td>
                                                                                                        <td style="width: 50%" colspan="3">
                                                                                                            <asp:TextBox ID="txtKeyboardNo" runat="server" CssClass="textbox" MaxLength="25"
                                                                                                                TabIndex="28" Width="155px"></asp:TextBox></td>
                                                                                                        <td style="width: 15%">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="textbold" style="width:35%;height:30px;">
                                                                                                            Mouse Type<span class="Mandatory">*</span></td>
                                                                                                        <td style="width: 65%" colspan="4">
                                                                                                            <asp:DropDownList ID="drpMouseType" runat="server" Width="160px" CssClass="dropdown"
                                                                                                                TabIndex="29">
                                                                                                            </asp:DropDownList></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="textbold" style="width: 35%">
                                                                                                            Mouse No.<span class="Mandatory">*</span></td>
                                                                                                        <td style="width: 50%" colspan="3">
                                                                                                            <asp:TextBox ID="txtMouseNo" runat="server" CssClass="textbox" MaxLength="25" TabIndex="30"
                                                                                                                Width="155px"></asp:TextBox></td>
                                                                                                        <td style="width: 15%">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="textbold" style="width: 35%">
                                                                                                            Cdr No.</td>
                                                                                                        <td style="width: 50%" colspan="3">
                                                                                                            <asp:TextBox ID="txtCdrNo" runat="server" CssClass="textbox" MaxLength="25" TabIndex="31"
                                                                                                                Width="155px"></asp:TextBox></td>
                                                                                                        <td style="width: 15%">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="textbold" style="width: 35%">
                                                                                                            Addl. Ram.</td>
                                                                                                        <td style="width: 65%" colspan="3">
                                                                                                            <asp:TextBox ID="txtRam" runat="server" CssClass="textbox" MaxLength="25" TabIndex="32"
                                                                                                                Width="155px"></asp:TextBox></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="textbold" style="width: 35%; height: 80px;">
                                                                                                            Remarks</td>
                                                                                                        <td style="width: 50%;" colspan="3">
                                                                                                            <asp:TextBox ID="txtRem" runat="server" CssClass="textbox" ReadOnly="false" TextMode="MultiLine"
                                                                                                                Height="80px" TabIndex="33" Width="154px"></asp:TextBox></td>
                                                                                                        <td style="width: 15%">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="textbold" style="width: 35%">
                                                                                                            &nbsp;</td>
                                                                                                        <td style="width: 35%">
                                                                                                        </td>
                                                                                                        <td style="width: 5%">
                                                                                                            &nbsp;</td>
                                                                                                        <td style="width: 25%" class="center">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                        </td>
                                                                                                        <td colspan="4" class="ErrorMsg">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                        </td>
                                                                                                        <td colspan="4" class="ErrorMsg">
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </td>
                                                                        <td  align ="center" valign ="top" >
                                                                           <div id="div1" runat ="server" >
                                                                                   <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                                        <tr >
                                                                                            <td class="textbold" style="width:35%">
                                                                                              </td>
                                                                                            <td style="width: 65%" colspan ="3">
                                                                                               <asp:Button ID="btnValidate" runat="server" CssClass="button" TabIndex="35" AccessKey ="V"
                                                                                                           Text="Validate Challan No." Width="118px" /></td>                                           
                                                                                           
                                                                                        </tr>
                                                                                          <tr >
                                                                                            <td class="textbold" style="width:35%">
                                                                                              </td>
                                                                                            <td style="width: 65%" colspan ="3">
                                                                           
                                                                           <asp:Button ID="btnSave" runat="server" CssClass="button" TabIndex="36" Text="Save" Width="118px"  AccessKey ="S" /></td> 
                                                                                                
                                                                                        </tr>
                                                                                         <tr >
                                                                                            <td class="textbold" style="width:35%">
                                                                                              </td>
                                                                                            <td style="width: 65%" colspan ="3">
                                                                           <asp:Button accessKey="R" id="btnReset" tabIndex=37 runat="server" CssClass="button" Text="Reset" Width="118px"></asp:Button></td> 
                                                                                                
                                                                                        </tr>
                                                                                       
                                                                                       
                                                                                 </table>
                                                                           </div>
                                                                       </td>
                                                                       <td align ="center" valign ="top">
                                                                           &nbsp;</td>
                                                                       
                                                                    </tr>
                                                                     <tr>
                                                                            <td colspan="2" >
                                                                            </td>
                                                                            <td colspan="2" class="ErrorMsg">
                                                                                Field Marked * are Mandatory</td>
                                                                          
                                                                        </tr>    
                                                                        <td></td>
                                                                </table>                                                             
                                                            </asp:Panel> 
                                                       </td> 
                                                  </tr>                                                              
                                                
                                             
                                                
                                             </table>
                                        </td>
                                    </tr>
                                    <tr>
                                      <td> <asp:HiddenField ID="hdInputSavexml" runat= "server" Value="0" />
                                                                         <asp:HiddenField ID="hdOrderNoExist" runat= "server" Value="0" />
                                                                         <asp:HiddenField ID="hdblnOrderOverride" runat= "server" Value="0" />
                                                                         <asp:HiddenField ID="hdblnSNoOverride" runat= "server" Value="0" />
                                                                         <asp:HiddenField ID="hdblnChallanOverride" runat= "server" Value="0" />
                                                                         <asp:HiddenField ID="hdChallanNoExist" runat= "server" Value="0" />
                                                                         <asp:HiddenField ID="hdAllowSaveForOrder" runat= "server" Value="0" />
                                                                         <asp:HiddenField ID="hdAllowSaveForChallan" runat= "server" Value="0" />
                                                                         <asp:HiddenField ID="hdAllowSaveForCpuNo" runat= "server" Value="0" />
                                                                         <asp:HiddenField ID="hdAllowSaveForMonNo" runat= "server" Value="0" />
                                                                         <asp:HiddenField ID="hdAllowSaveForKeyNo" runat= "server" Value="0" />
                                                                         <asp:HiddenField ID="hdAllowSaveForMSENo" runat= "server" Value="0" />
                                                                         <asp:HiddenField ID="hdOverRideBackDate" runat= "server" Value="0" /> 
                                                                         <asp:HiddenField ID="hdEquipCode" runat= "server" Value="0" /> 
                                                                         <asp:HiddenField ID="hdComboData" runat= "server" Value="0" /> 
                                                                         <asp:HiddenField ID="hdComboData2" runat= "server" Value="0" /> 
                                                                         <asp:HiddenField ID="hdEquipCodexml" runat= "server" Value="0" /> 
                                                                         <asp:HiddenField ID="hdfocustemp" runat= "server" Value="0" /> 
                                                                         <asp:HiddenField ID="hdCpuNo" runat= "server" Value="0" /> 
                                                                         <asp:HiddenField ID="hdMonNo" runat= "server" Value="0" />
                                                                         <asp:HiddenField ID="hdRep" runat= "server" Value="0" /> 
                                                                         <asp:HiddenField ID="hdReplacementChallanExist" runat= "server" Value="0" />
                                          <asp:HiddenField ID="SaveStatus" runat="server" /><INPUT style="WIDTH: 1px" id="hdChallaDetails" type=hidden runat="server" />
                                          <asp:HiddenField ID="hdChallnType" runat="server" />
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
  <script type="text/javascript">
        Calendar.setup({
        inputField     :    '<%=txtInstallDate.clientId%>',
        ifFormat       :    "%d/%m/%Y",
        button         :    "f_DateInstall",
        //align          :    "Tl",
        singleClick    :    true
        });
    </script>
</body>
</html>
