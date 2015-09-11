<%@ Page Language="VB"  EnableViewState="true" ValidateRequest ="false"  EnableEventValidation="false" AutoEventWireup="false" CodeFile="TAUP_New1APCInstall.aspx.vb" Inherits="TravelAgency_TAUP_New1APCInstall" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Agency PC Installation</title>
      <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
   <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
    <script type="text/javascript" src="../Calender/calendar.js"></script>
    <!-- import the language module -->
    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
    <!-- import the calendar setup module -->
    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>
    <script type="text/javascript" language ="javascript" > 


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
  
</head>
<body  >
    <form id="form1" runat="server"   defaultfocus ="txtChallanNo" >    
      <table width="600px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Agency -&gt;</span><span class="sub_menu">1A &nbsp;PC Installation
                                </span></td>
                        </tr>
                         <tr>
                                    <td align ="right" > <a href="#" class="LinkButtons" onclick="window.close();  window.opener.document.forms['form1'].submit(); ">Close</a>&nbsp;&nbsp;&nbsp;</td>
                                  </tr>
                        <tr>
                            <td class="heading center" >
                                &nbsp; Manage 1A PC Installation</td>
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
                                                                   <td align ="center" valign ="top"  ><asp:Label id="lblConfirm" runat="server" CssClass="Gridheading"></asp:Label> &nbsp;&nbsp;<asp:Button ID="btnYes"  runat ="server" Text ="Yes" CssClass="button" />&nbsp;&nbsp;<asp:Button ID="btnNo"  runat ="server" Text ="No" CssClass="button" /></td>                                                                                                                        
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
                                                       <td valign ="TOP"> 
                                                        <asp:Panel  ID="pnlNo" runat="server"  width="100%" Visible ="false"  >
                                                               <table border ="0" cellpadding ="0" cellspacing ="0"  width="100%"  > 
                                                                <tr>
                                                                               <td class="Gridheading" colspan="2"><asp:Label ID="lblMonOrCpuNo"    Text ="" runat ="server" ></asp:Label></td>
                                                                                       
                                                                           </tr>
                                                                   <tr>
                                                                               <td class="right textbold" colspan="2" >
                                                                                <span class="button"  >Would you like to install it at this Location also?</span>
                                                                                <asp:Button ID="Button1" runat="server" Text="Yes" CssClass="button" OnClientClick="return CloseMiscPage('1')" />
                                                                            <asp:Button ID="Button2" runat="server" Text="No" CssClass="button" OnClientClick="return CloseMiscPage('2')"/>&nbsp;</td>                                                                                       
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
                                                       <td valign ="TOP"> 
                                                        <asp:Panel  ID="PnlDetails" runat="server"   >
                                                            <table border ="0" cellpadding ="2" cellspacing ="2" > 
                                                                     <tr style ="height:1px;">
                                                                        <td  style="width:15%"></td>
                                                                        <td class="textbold" style="width:25%" ><asp:HiddenField id="hdMonNo" runat="server"></asp:HiddenField></td>                                                                           
                                                                        <td style="width:35%"><asp:DropDownList ID="DropDownList3" runat="server" Width="194px" CssClass="dropdown" TabIndex="7" Visible="False">
                                                                        </asp:DropDownList><asp:TextBox ID="TextBox4" runat="server" CssClass="textbox" MaxLength="25"
                                                                                TabIndex="8" Width="188px" Visible="False"></asp:TextBox></td>
                                                                        <td style="width:5%">
                                                                            </td>
                                                                        <td style="width:30%" class="center"> </td>
                                                                    </tr>                                                                                                                                 
                                                                    <tr>
                                                                        <td style="width:15%">
                                                                        </td>
                                                                        <td class="textbold" style="width:20%">
                                                                            Challan No<span class="Mandatory" >*</span></td>
                                                                        <td style="width: 35%"><asp:TextBox ID="txtChallanNo" runat="server" CssClass="textbox" MaxLength="17"
                                                                                TabIndex="1" Width="117px"></asp:TextBox>
                                                                           <asp:Button id="btnValidate" tabIndex=19 runat="server" CssClass="button" Text="Validate" Width="65px" AccessKey="V"></asp:Button></td>                                           
                                                                        <td style="width: 5%"></td>
                                                                        <td class="left" style="width: 30%">
                                                                           <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="21" Width="118px" AccessKey="R" /></td>
                                                                    </tr>  
                                                            </table>  
                                                       </asp:Panel>
                                                       </td>                                                    
                                                    </tr>  
                                                    <tr>
                                                       <td  valign ="top" > 
                                                        <asp:Panel  ID="pnlEnableorDisable" runat="server"   >
                                                            <table border ="0" cellpadding ="2" cellspacing ="2" >                                                                  
                                                                                                               
                                                                    <tr>
                                                                        <td style="width:15%">
                                                                        </td>
                                                                        <td class="textbold" style="width:20%">
                                                                            Date of Installation .<span class="Mandatory" >*</span></td>
                                                                        <td style="width: 35%">

                                                                            
                                                                            <asp:TextBox ID="txtInstallDate" runat="server" CssClass="textboxgrey" MaxLength="10"
                                                                                ReadOnly="True" TabIndex="2" Width="158px"></asp:TextBox>
                                                                            <img id="f_DateInstall" alt="" src="../Images/calender.gif" style="cursor: pointer" runat="server" 
                                                                                tabindex="3" title="Date selector" /><script type="text/javascript">
                                                                                                                                Calendar.setup({
                                                                                                                                inputField     :    '<%=txtInstallDate.clientId%>',
                                                                                                                                ifFormat       :    "%d/%m/%Y",
                                                                                                                                button         :    "f_DateInstall",
                                                                                                                                //align          :    "Tl",
                                                                                                                                singleClick    :    true
                                                                                                                                });
                                                                            </script></td>
                                                                       
                                                                        <td style="width: 5%">
                                                                            </td>
                                                                        <td class="left" style="width: 25%">
                                                                           <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="20" Width="118px" AccessKey="S" /></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:15%">
                                                                        </td>
                                                                        <td class="textbold" style="width:20%">
                                                                            Order No.</td>
                                                                        <td style="width: 35%"><asp:DropDownList ID="drpOrderNo" runat="server" Width="194px" CssClass="dropdown" TabIndex="4" Visible="False">
                                                                        </asp:DropDownList><asp:TextBox ID="txtOrderNo" runat="server" CssClass="textbox" MaxLength="12" TabIndex="5"
                                                                                Width="188px"></asp:TextBox></td>                                           
                                                                        <td style="width: 5%">
                                                                            &nbsp;</td>
                                                                        <td class="center" style="width: 30%">
                                                                           </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:15%">
                                                                        </td>
                                                                        <td class="textbold" style="width:20%">
                                                                            CPU Type<span class="Mandatory" >*</span></td>
                                                                        <td style="width: 35%">
                                                                            <asp:DropDownList ID="drpCpuType" runat="server" Width="194px" CssClass="dropdown" TabIndex="6" AutoPostBack="True">
                                                                            </asp:DropDownList></td>                                           
                                                                        <td style="width: 5%">
                                                                            &nbsp;</td>
                                                                        <td class="center" style="width: 30%">
                                                                           </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:15%">
                                                                        </td>
                                                                        <td class="textbold" style="width:20%">
                                                                            CPU No.<span class="Mandatory" >*</span></td>
                                                                        <td style="width: 35%"><asp:DropDownList ID="drpCpuNo" runat="server" Width="194px" CssClass="dropdown" TabIndex="7" Visible="False">
                                                                        </asp:DropDownList><asp:TextBox ID="txtCpuNo" runat="server" CssClass="textbox" MaxLength="25"
                                                                                TabIndex="8" Width="188px"></asp:TextBox></td>                                           
                                                                        <td style="width: 5%">
                                                                            &nbsp;</td>
                                                                        <td class="center" style="width: 30%">
                                                                           </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:15%">
                                                                        </td>
                                                                        <td class="textbold" style="width:20%">
                                                                            Monitor Type<span class="Mandatory" >*</span></td>
                                                                        <td style="width: 35%">
                                                                            <asp:DropDownList ID="drpMonType" runat="server" Width="194px" CssClass="dropdown" TabIndex="9" AutoPostBack="True">
                                                                            </asp:DropDownList></td>                                           
                                                                        <td style="width: 5%">
                                                                            &nbsp;</td>
                                                                        <td class="center" style="width: 30%">
                                                                           </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:15%">
                                                                        </td>
                                                                        <td class="textbold" style="width:20%">
                                                                            Monitor No.<span class="Mandatory" >*</span></td>
                                                                        <td style="width: 35%"><asp:DropDownList ID="drpMonNo" runat="server" Width="194px" CssClass="dropdown" TabIndex="10" Visible="False">
                                                                        </asp:DropDownList><asp:TextBox ID="txtMonNo" runat="server" CssClass="textbox" MaxLength="25"
                                                                                TabIndex="11" Width="188px"></asp:TextBox></td>                                           
                                                                        <td style="width: 5%">
                                                                            &nbsp;</td>
                                                                        <td class="center" style="width: 30%">
                                                                           </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:15%">
                                                                        </td>
                                                                        <td class="textbold" style="width:20%">
                                                                            Keyboard Type<span class="Mandatory" >*</span></td>
                                                                        <td style="width: 35%"><asp:DropDownList ID="drpKeyType" runat="server" Width="194px" CssClass="dropdown" TabIndex="12">
                                                                            </asp:DropDownList></td>                                           
                                                                        <td style="width: 5%">
                                                                            &nbsp;</td>
                                                                        <td class="center" style="width: 30%">
                                                                           </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:15%">
                                                                        </td>
                                                                        <td class="textbold" style="width:20%">
                                                                            Keyboard No.<span class="Mandatory" >*</span></td>
                                                                        <td style="width: 35%">
                                                                            <asp:TextBox ID="txtKeyboardNo" runat="server" CssClass="textbox" MaxLength="25"
                                                                                TabIndex="13" Width="188px"></asp:TextBox></td>                                           
                                                                        <td style="width: 5%">
                                                                            &nbsp;</td>
                                                                        <td class="center" style="width: 30%">
                                                                           </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:15%">
                                                                        </td>
                                                                        <td class="textbold" style="width:20%">
                                                                            Mouse Type<span class="Mandatory" >*</span></td>
                                                                        <td style="width: 35%">
                                                                            <asp:DropDownList ID="drpMouseType" runat="server" Width="194px" CssClass="dropdown" TabIndex="14">
                                                                            </asp:DropDownList></td>                                           
                                                                        <td style="width: 5%">
                                                                            &nbsp;</td>
                                                                        <td class="center" style="width: 30%">
                                                                           </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:15%">
                                                                        </td>
                                                                        <td class="textbold" style="width:20%">
                                                                            Mouse No.<span class="Mandatory" >*</span></td>
                                                                        <td style="width: 35%">
                                                                            <asp:TextBox ID="txtMouseNo" runat="server" CssClass="textbox" MaxLength="25"
                                                                                TabIndex="15" Width="188px"></asp:TextBox></td>                                           
                                                                        <td style="width: 5%">
                                                                            &nbsp;</td>
                                                                        <td class="center" style="width: 30%">
                                                                           </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:15%">
                                                                        </td>
                                                                        <td class="textbold" style="width:20%">
                                                                            Cdr No.</td>
                                                                        <td style="width: 35%">
                                                                            <asp:TextBox ID="txtCdrNo" runat="server" CssClass="textbox" MaxLength="25"
                                                                                TabIndex="16" Width="188px"></asp:TextBox></td>                                           
                                                                        <td style="width: 5%">
                                                                            &nbsp;</td>
                                                                        <td class="center" style="width: 30%">
                                                                           </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:15%">
                                                                        </td>
                                                                        <td class="textbold" style="width:20%">
                                                                            Addl. Ram.</td>
                                                                        <td style="width: 35%">
                                                                            <asp:TextBox ID="txtRam" runat="server" CssClass="textbox" MaxLength="25"
                                                                                TabIndex="17" Width="188px"></asp:TextBox></td>                                           
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
                                                                                TabIndex="18" Width="188px" Height="80px"></asp:TextBox></td>                                           
                                                                        <td style="width: 5%">
                                                                            &nbsp;</td>
                                                                        <td class="center" style="width: 25%">
                                                                           </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td  style="width:15%"></td>
                                                                        <td class="textbold" style="width:25%" >  
                                                                                                      <asp:HiddenField ID="hdkkuno" runat ="server" />
                                                                            &nbsp; &nbsp;
                                                                                                   
                                                                            </td>
                                                                            
                                                                        <td style="width:35%">
                                                                        <%-- <asp:HiddenField ID="SaveStatus" runat ="server" />--%>
                                                                           <%-- --%>
                                                                        </td>
                                                                        <td style="width:5%">
                                                                            &nbsp;</td>
                                                                        <td style="width:25%" class="center"> </td>
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
                                                      <td>  <asp:HiddenField ID="SaveStatus" runat ="server" />
                                                      <input id="hdLcode" runat="server" style="width: 5px" type="hidden" /> <input id="hdCpunNo" runat="server" style="width: 5px" value ="0" type="hidden" /><input id="hdBlankChallanNo" runat="server" style="width: 5px" value ="0" type="hidden" />
                                                                            <input id="hdInputSavexml" runat="server" style="width: 5px"  value ="0" type="hidden" />
                                                                            <input id="hdOrderNoExist" runat="server" style="width: 5px"  value ="0" type="hidden" />
                                                                             <input id="hdblnOrderOverride" runat="server" style="width: 5px" value ="0" type="hidden" />
                                                                               <input id="hdblnSNoOverride" runat="server" style="width: 5px" value ="0" type="hidden" />
                                                                              <input id="hdblnChallanOverride" runat="server" style="width: 5px" value ="0" type="hidden" />
                                                                              <input id="hdChallanNoExist" runat="server" style="width: 5px"  value ="0" type="hidden" />
                                                                                   <input id="hdAllowSaveForOrder" runat="server" style="width: 5px" value ="0" type="hidden" />
                                                                                        <input id="hdAllowSaveForChallan" runat="server" style="width: 5px" value ="0" type="hidden" />
                                                                                             <input id="hdAllowSaveForCpuNo" runat="server" style="width: 5px" value ="0" type="hidden" />
                                                                                                  <input id="hdAllowSaveForMonNo" runat="server" style="width: 5px" value ="0" type="hidden" />
                                                                                                    <input id="hdAllowSaveForKeyNo" runat="server" style="width: 5px" value ="0" type="hidden" />
                                                                                                      <input id="hdAllowSaveForMSENo" runat="server" style="width: 5px" value ="0" type="hidden" />
                                                                                                   <input id="hdOverRideBackDate" runat="server" style="width: 1px" type="hidden" />
                                                                                                    <input id="hdEquipCode" runat="server" style="width: 1px" type="hidden" />
                                                                                                      <input id="hdComboData" runat="server" style="width: 1px" type="hidden" />
                                                                                                      <input id="hdComboData2" runat="server" style="width: 1px" type="hidden" />
                                                                                                      <input id="hdEquipCodexml" runat="server" style="width: 1px" type="hidden" />
                                                                                                       <input id="hdfocustemp" runat="server" style="width: 1px" type="hidden" />
                                                                                                       <asp:HiddenField ID="hdCpuNo" runat ="server" />
                                                                                                        <input id="hdChallaDetails" runat="server" style="width: 1px" type="hidden" />
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
