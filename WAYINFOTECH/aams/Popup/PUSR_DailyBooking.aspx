<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PUSR_DailyBooking.aspx.vb" Inherits="Popup_PUSR_DailyBooking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>1a Daily Booking Details</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>
    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
<link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
</head>
<script type="text/javascript" language="javascript">

function DataValidatin()
{
            if(document.getElementById('<%=txtdtFrom.ClientId%>').value != '')
                {
                if (isDate(document.getElementById('<%=txtdtFrom.ClientId%>').value,"d/M/yyyy") == false)	
                {
                   document.getElementById('<%=lblError.ClientId%>').innerText = "Date From is not valid.";			
	               document.getElementById('<%=txtdtFrom.ClientId%>').focus();
	               return(false);  
                }
               }  
                if(document.getElementById('<%=txtdtTo.ClientId%>').value != '')
                {
                if (isDate(document.getElementById('<%=txtdtTo.ClientId%>').value,"d/M/yyyy") == false)	
                {
                   document.getElementById('<%=lblError.ClientId%>').innerText = "Date To is not valid.";			
	               document.getElementById('<%=txtdtTo.ClientId%>').focus();
	               return(false);  
                }
               }
                 if(document.getElementById('<%=txtdtTo.ClientId%>').value != '' && document.getElementById('<%=txtdtFrom.ClientId%>').value != '')
                {
                if (compareDates(document.getElementById('<%=txtdtFrom.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtdtTo.ClientId%>').value,"d/M/yyyy")=='1')	
                {
                   document.getElementById('<%=lblError.ClientId%>').innerText = "Date To is shorter than Date From.";			
	               document.getElementById('<%=txtdtTo.ClientId%>').focus();
	               return(false);  
                }
               }  
 }      



function validateAirBreakUp()
    {
   
                var stAir=document.getElementById("chkAir").checked;
                var stCar=document.getElementById("chkCar").checked;
                var stHotel=document.getElementById("chkHotel").checked;
                var stAirBreak=document.getElementById("chkAirBreakUp").checked;
            if(stAirBreak==true)
            {
            
            document.getElementById("chkCar").checked=false;
            document.getElementById("chkHotel").checked=false;
            //return false;
            } 
           
      }
   
   
   function ValidateCarHotel()
   {
   
                var stAir=document.getElementById("chkAir").checked;
                var stCar=document.getElementById("chkCar").checked;
                var stHotel=document.getElementById("chkHotel").checked;
                var stAirBreak=document.getElementById("chkAirBreakUp").checked; 
                
                if(stCar==true || stHotel==true)
                {
                document.getElementById("chkAirBreakUp").checked=false;
               // return false;
                } 
               
   }
</script>
<body>
    <form id="form1" runat="server" defaultbutton="btnRun">
    <table cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td>
     <table width="860px" align="left" class="border_rightred">
            <tr >
                <td valign="top"  style="width:860px;" >
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Productivity-></span><span class="sub_menu">Daily Bookings</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                &nbsp;Daily Bookings Details</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" >
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                        <td style="width: 860px; height: 245px;" class="redborder" valign="top" >
                                                          <table width="100%" border="0"   align="left" cellpadding="0" cellspacing="0">
                                                          
                        
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" style="height: 15px">
                                                        &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                                <tr>
                                                                        <td class="textbold" style="height: 25px">
                                                                        </td>
                                                                        <td class="subheading" colspan="4" style="height: 25px">
                                                                            Agency &nbsp;Details</td>
                                                                            <td></td>
                                                                </tr>
                                                                
                                                                
                                                <tr>
                                                    <td width="6%" class="textbold" height="25px">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="width: 220px">
                                                        Agency Name
                                                        </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtAgencyName" CssClass="textboxgrey" MaxLength="50" runat="server" Width="93%" ReadOnly="True" TabIndex="1"></asp:TextBox> </td>
                                                    <td style="width: 176px">
                                                        <asp:Button ID="btnRun" CssClass="button" runat="server" Text="Search" /></td>
                                                </tr>
                                                              <tr>
                                                                  <td class="textbold" height="25" width="6%">
                                                                  </td>
                                                                  <td class="textbold" style="width: 220px" valign="top" >
                                                                      Address</td>
                                                                  <td colspan="3">
                                                                      <asp:TextBox ID="txtAddress" runat="server" CssClass="textboxgrey" Height="48px"
                                                                          MaxLength="40" ReadOnly="True" Width="93%" TabIndex="2"></asp:TextBox></td>
                                                                  <td style="width: 176px" valign="top" >
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" /></td>
                                                              </tr>
                                                <tr>
                                                    <td class="textbold" width="6%" style="height: 25px">
                                                    </td>
                                                    <td class="textbold" style="height: 25px; width: 220px;">
                                                        City</td>
                                                    <td style="height: 25px; width: 240px;">
                                                        <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" MaxLength="20" ReadOnly="True"
                                                            Width="133px" TabIndex="3"></asp:TextBox></td>
                                                    <td class="textbold" style="height: 25px; width: 91px;">
                                                        Country</td>
                                                    <td style="height: 25px; width: 184px;">
                                                        <asp:TextBox ID="txtCountry" runat="server" CssClass="textboxgrey" MaxLength="20"
                                                            ReadOnly="True" Width="126px" TabIndex="4"></asp:TextBox></td>
                                                    <td style="height: 25px; width: 176px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px">
                                                    </td>
                                                    <td class="textbold" style="height: 25px; width: 220px;" >
                                                        Date From</td>
                                                    <td style="height: 25px; width: 240px;">
                                                        <asp:TextBox ID="txtdtFrom" runat="server" CssClass="textbox" MaxLength="20" Width="132px" TabIndex="5"></asp:TextBox>
                                                         <img id="f_trigger_d18" alt="" src="../Images/calender.gif" TabIndex="6" title="Date selector" style="cursor: pointer" />

                                                                                                 <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtdtFrom.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "f_trigger_d18",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                        
                                                        </td>
                                                    <td  class="textbold" style="height: 25px; width: 91px;">
                                                        Date To</td>
                                                    <td style="height: 25px; width: 184px;">
                                                        <asp:TextBox ID="txtdtTo" runat="server" CssClass="textbox" MaxLength="10" Width="124px" TabIndex="7"></asp:TextBox>
                                                        <img id="Img1" alt="" src="../Images/calender.gif" TabIndex="16" title="Date selector" style="cursor: pointer" />

                                                                                                 <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtdtTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "Img1",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                        </td>
                                                    <td style="width: 176px; height: 25px;">
                                                    </td>
                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 25px;" colspan="5" nowrap="nowrap" ><asp:CheckBox ID="chkBreakup" runat="server" CssClass="textbold" Text="Show Breakup" Width="120px" AutoPostBack="True" TabIndex="8"   />&nbsp;&nbsp;
                                                                        &nbsp;&nbsp; &nbsp;<asp:CheckBox ID="chkUseOriginalBooking" runat="server" CssClass="textbold" Text="NBS" Width="119px" Checked="false" TabIndex="9"   />
                                                                        &nbsp;&nbsp;
                                                                        <asp:CheckBox ID="chkWholGrp" runat="server" CssClass="textbold" Text="Show Productivity For Whole Group" Width="248px" TabIndex="10"   />
                                                                        &nbsp; &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 25px">
                                                                    </td>
                                                                    <td class="subheading" colspan="4" style="height: 25px">
                                                                        Daily Bookings Details</td>
                                                                        <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 25px; width: 220px;"><asp:CheckBox ID="chkAir" runat="server" CssClass="textbold" Text="Air" Width="120px" TabIndex="11"   /></td>
                                                                    <td style="height: 25px; width: 240px;"><asp:CheckBox ID="chkCar" runat="server" CssClass="textbold" Text="Car" Width="120px" TabIndex="12"   /></td>
                                                                    <td class="textbold" style="height: 25px; width: 91px;"><asp:CheckBox ID="chkHotel" runat="server" CssClass="textbold" Text="Hotel" Width="120px" TabIndex="13"   /></td>
                                                                    <td style="height: 25px; width: 184px;"><asp:CheckBox ID="chkAirBreakUp" runat="server" CssClass="textbold" Text="Air Breakup" Width="90px" TabIndex="14"   /></td>
                                                                    <td style="height: 25px">
                                                                    </td>
                                                                </tr>
                                               </table>
                                                        </td>
                                                </tr>
                                                           
                                        </table>
                                        
                                          
                                        </td>
                                    </tr>
                                </table>
                                                        <input type="hidden" id="hdAgencyNameId" runat="server" value="" style="width: 5px" />
                                                        <input type="hidden" id="hdAgencyName" runat="server" value="" style="width: 5px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            
            
            
            
            <!-- Code for Paging ---->
            <tr>                                                   
                                                    <td valign ="top"  >
                                                   
                                                    </td>
                                                </tr>
            <!-- Code for Paging ---->
        </table>
    </td>
     <td>
    
    </td>
    </tr>
    
    <tr>
    <td colspan="2">
     <table  width="100%" border="0" align="left" cellpadding="0" cellspacing="0"> 
              
             
                                                                            <tr>  
                                                                                    <td class="redborder" valign="top" align="left">
                                                                                    
                                                                                    
                                                                                    <table width="660px" border="0" align="left" cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td valign="top" align="left">
                                                                                        <asp:GridView ID="grdvMotivDetails" HeaderStyle-ForeColor="white" AllowSorting="true"  ShowFooter=true   runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="660px" FooterStyle-CssClass="Gridheading" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" EnableViewState="False">
                                                              <Columns>
                                                              
                                                              <asp:TemplateField SortExpression ="Date" HeaderText="Date">
                                                              <ItemTemplate>
                                                              <asp:Label ID="lblDateVal" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                                              </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblHTotal" runat="server" Text="Total"></asp:Label>
                                                              </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                                <asp:TemplateField SortExpression ="Netbookings" HeaderText="Total">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblNetbookingsVal" runat="server" Text='<%# Eval("Netbookings") %>'></asp:Label>
                                                              </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblHNetbookings" runat="server"></asp:Label>
                                                              </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                               <asp:TemplateField SortExpression ="Air_Netbookings" HeaderText="Air Bookings">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblAir_NetbookingsVal" runat="server" Text='<%# Eval("Air_Netbookings") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblHAir_Netbookings" runat="server"></asp:Label>
                                                              </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                            
                                                              
                                                                <asp:TemplateField SortExpression ="PASSIVE" HeaderText="NBS">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblPASSIVEVal" runat="server" Text='<%# Eval("PASSIVE") %>'></asp:Label>
                                                              </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblHPASSIVE" runat="server"></asp:Label>
                                                              </FooterTemplate>
                                                              <ItemStyle Wrap="false" Width="50px" />
                                                              </asp:TemplateField>
                                                              
                                                                <asp:TemplateField SortExpression ="WITHPASSIVE" HeaderText="Total">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblWITHPASSIVEVal" runat="server" Text='<%# Eval("WITHPASSIVE") %>'></asp:Label>
                                                              </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblHWITHPASSIVE" runat="server"></asp:Label>
                                                              </FooterTemplate>
                                                              </asp:TemplateField>
                                                              <asp:TemplateField SortExpression ="Car_Netbookings" HeaderText="Car NetBookings">
                                                              <ItemTemplate>
                                                             <asp:Label ID="lblCar_NetbookingsVal" runat="server" Text='<%# Eval("Car_Netbookings") %>'></asp:Label>
                                                             </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblHCar_Netbookings" runat="server"></asp:Label>
                                                              </FooterTemplate>
                                                              </asp:TemplateField>
                                                           
                                                            <asp:TemplateField SortExpression ="Hotel_Netbookings" HeaderText="Hotel NetBookings">
                                                              <ItemTemplate>
                                                             <asp:Label ID="lblHotel_NetbookingsVal" runat="server" Text='<%# Eval("Hotel_Netbookings") %>'></asp:Label>
                                                             </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblHHotel_Netbookings" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                           
                                                              
                                               </Columns>
                                                            
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False"  />
                                                            <RowStyle CssClass="textbold" />
                                                                                          <FooterStyle CssClass="Gridheading" />
                                   </asp:GridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                    
                                                                                    <tr>
                                                                                        <td valign="top" align="left">
                                                                                        <asp:GridView ID="grdvAirwithArBreak" runat="server" HeaderStyle-ForeColor="white" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="860px" ShowFooter="true" FooterStyle-CssClass="Gridheading" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" EnableViewState="False" AllowSorting="True">
                                                              <Columns>
                                                              
                                                               <asp:TemplateField SortExpression ="Date" HeaderText="Date">
                                                              <ItemTemplate>
                                                             <asp:Label ID="lblDateVal" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                                             </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblDate" runat="server" Text="Total"></asp:Label>
                                                                </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                               <asp:TemplateField SortExpression ="Air_Netbookings" HeaderText="Air NetBookings">
                                                              <ItemTemplate>
                                                             <asp:Label ID="lblAir_NetbookingsVal1" runat="server" Text='<%# Eval("Air_Netbookings") %>'></asp:Label>
                                                             </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblAir_Netbookings1" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                              
                                                               <asp:TemplateField SortExpression ="AirlineName" HeaderText="AirLine Name">
                                                              <ItemTemplate>
                                                             <asp:Label ID="lblAirlineNameVal" runat="server" Text='<%# Eval("AirlineName") %>'></asp:Label>
                                                             </ItemTemplate>
                                                              </asp:TemplateField>
                                                              
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False"  />
                                                            <RowStyle CssClass="textbold" />
                                       <FooterStyle CssClass="Gridheading" />
                                   </asp:GridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                    
                                                                                    <tr>
                                                                                        <td valign="top" align="left">
                                                                                        
                                                                                        <asp:GridView ID="grdvBreakup" runat="server" HeaderStyle-ForeColor="white" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="900px" ShowFooter="true" FooterStyle-CssClass="Gridheading"  HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" EnableViewState="False" AllowSorting="True">
                                                              <Columns>
                                                 
                                                  
                                                  
                                                            <asp:TemplateField SortExpression ="DATE" HeaderText="Date">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblDATEVal" runat="server" Text='<%# Eval("DATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblHDATE" runat="server" Text="Total"></asp:Label>
                                                              </FooterTemplate>
                                                              <ItemStyle Wrap="false" />
                                                              </asp:TemplateField>
                                                              
                                                              
                                                              
                                                               <asp:TemplateField SortExpression ="NETBOOKINGS" HeaderText="Total">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblNETBOOKINGS" runat="server" Text='<%# Eval("NETBOOKINGS") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblFNETBOOKINGS" runat="server"></asp:Label>
                                                              </FooterTemplate>
                                                              <ItemStyle Wrap="false" />
                                                              
                                                              </asp:TemplateField>
                                                              
                                                              
                                                              <asp:TemplateField SortExpression ="AirBookings" HeaderText="Air Bookings">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblAirBookings" runat="server" Text='<%# Eval("AirBookings") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblFAirBookings" runat="server"></asp:Label>
                                                              </FooterTemplate>
                                                              <ItemStyle Wrap="false" />
                                                              
                                                              </asp:TemplateField>
                                                              
                                                               <asp:TemplateField SortExpression ="Passive" HeaderText="NBS ">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblPASSIVEVal" runat="server" Text='<%# Eval("Passive") %>'></asp:Label>
                                                              </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblHPASSIVE" runat="server"></asp:Label>
                                                              </FooterTemplate>
                                                              <ItemStyle Wrap="false" Width="50px" />
                                                              
                                                              </asp:TemplateField>
                                                              
                                                                <asp:TemplateField SortExpression ="WithPassive" HeaderText="Total">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblWITHPASSIVEVal" runat="server" Text='<%# Eval("WithPassive") %>'></asp:Label>
                                                              </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblHWITHPASSIVE" runat="server"></asp:Label>
                                                              </FooterTemplate>
                                                              <ItemStyle Wrap="false" />
                                                              
                                                              </asp:TemplateField>
                                                              
                                                              
                                                              <asp:TemplateField SortExpression ="CarBookings" HeaderText="Car Bookings">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblCarBookings" runat="server" Text='<%# Eval("CarBookings") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblFCarBookings" runat="server"></asp:Label>
                                                              </FooterTemplate>
                                                              <ItemStyle Wrap="false" />
                                                              
                                                              </asp:TemplateField>
                                                              
                                                        
                                                        <asp:TemplateField SortExpression ="HotelBookings" HeaderText="Hotel Bookings">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblHotelBookings" runat="server" Text='<%# Eval("HotelBookings") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblFHotelBookings" runat="server"></asp:Label>
                                                              </FooterTemplate>
                                                              <ItemStyle Wrap="false" />
                                                              
                                                              </asp:TemplateField>
                                                              
                                                              
                                                              
                                                 <asp:TemplateField SortExpression ="AIR_ADD_BOOKINGS" HeaderText="Airline Add Bookings">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblAIR_ADD_BOOKINGSVal" runat="server" Text='<%# Eval("AIR_ADD_BOOKINGS") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblHAIR_ADD_BOOKINGS" runat="server"></asp:Label>
                                                              </FooterTemplate>
                                                              <ItemStyle Wrap="false" />
                                                              
                                                              </asp:TemplateField>
                                                              
                                                 
                                                            <asp:TemplateField SortExpression ="AIR_CANCEL_BOOKINGS" HeaderText="AirLine Cancel Bookings">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblAIR_CANCEL_BOOKINGSVal" runat="server" Text='<%# Eval("AIR_CANCEL_BOOKINGS") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblHAIR_CANCEL_BOOKINGS" runat="server"></asp:Label>
                                                              </FooterTemplate>
                                                              <ItemStyle Wrap="false" />
                                                              
                                                              </asp:TemplateField>
                                                              
                                                
                                                            <asp:TemplateField SortExpression ="AIR_MODIFY_BOOKINGS" HeaderText="AirLine Modify Bookings">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblAIR_MODIFY_BOOKINGSVal" runat="server" Text='<%# Eval("AIR_MODIFY_BOOKINGS") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblHAIR_MODIFY_BOOKINGS" runat="server"></asp:Label>
                                                              </FooterTemplate>
                                                              <ItemStyle Wrap="false" />
                                                              
                                                              </asp:TemplateField>
                                                              
                                                 
                                                            <asp:TemplateField SortExpression ="AIR_NET_BOOKINGS" HeaderText="AirLine Net Bookings">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblAIR_NET_BOOKINGSVal" runat="server" Text='<%# Eval("AIR_NET_BOOKINGS") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblHAIR_NET_BOOKINGS" runat="server"></asp:Label>
                                                              </FooterTemplate>
                                                              <ItemStyle Wrap="false" />
                                                              
                                                              </asp:TemplateField>
                                                              
                                                  
                                                            <asp:TemplateField SortExpression ="PASSIVE_ADD" HeaderText="Passive Add">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblPASSIVE_ADD" runat="server" Text='<%# Eval("PASSIVE_ADD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblFPASSIVE_ADD" runat="server"></asp:Label>
                                                              </FooterTemplate>
                                                              <ItemStyle Wrap="false" />
                                                              
                                                              </asp:TemplateField>
                                                              
                                                
                                                         <asp:TemplateField SortExpression ="PASSIVE_CANCEL" HeaderText="Passive Cancel">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblPASSIVE_CANCEL" runat="server" Text='<%# Eval("PASSIVE_CANCEL") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblFPASSIVE_CANCEL" runat="server"></asp:Label>
                                                              </FooterTemplate>
                                                              <ItemStyle Wrap="false" />
                                                              
                                                              </asp:TemplateField>
                                                              
                                                
                                                  <asp:TemplateField SortExpression ="PASSIVE_MODIFY" HeaderText="Passive Modify">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblePASSIVE_MODIFY" runat="server" Text='<%# Eval("PASSIVE_MODIFY") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblFPASSIVE_MODIFY" runat="server"></asp:Label>
                                                              </FooterTemplate>
                                                              <ItemStyle Wrap="false" />
                                                              
                                                              </asp:TemplateField>
                                                              
                                                  
                                                        <asp:TemplateField SortExpression ="PASSIVE_NET" HeaderText="Passive Net">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblPASSIVE_NET" runat="server" Text='<%# Eval("PASSIVE_NET") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblFPASSIVE_NET" runat="server"></asp:Label>
                                                              </FooterTemplate>
                                                              <ItemStyle Wrap="false" />
                                                              
                                                              </asp:TemplateField>
                                                              
                                                 
                                                  
                                                           
                                                              
                                                              
                                                              
                                                              
                                                              
                                                              
                                                              
                                                 
                                                              
                                                              
                                                    
                                                              
                                                               <asp:TemplateField SortExpression ="AirlineName" HeaderText="AirlineName">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblAirlineName" runat="server" Text='<%# Eval("AirlineName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblFAirlineName" runat="server"></asp:Label>
                                                              </FooterTemplate>
                                                              <ItemStyle Wrap="false" />
                                                              
                                                              </asp:TemplateField>
                                                            
  
                                                     </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="true"  />
                                                            <RowStyle CssClass="textbold" />
                                       <FooterStyle CssClass="Gridheading" />
                                   </asp:GridView>
                                   
                                                                                        </td>
                                                                                    </tr>
                                                                                    
                                                                                  
                                                                                    
                                                                                    
                                                                                    <tr>
                                                                                        <td valign="top" align="left">
                                                                                         <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="700px">
                                                      <table border="0" cellpadding="0" cellspacing="0">
                                                                      <tr style="padding-top : 4pt; padding-bottom : 4pt">                                                                                                                                                
                                                                          <td  nowrap="nowrap"  class="left" style="width: 235px; height: 30px"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="85px" CssClass="textboxgrey" ></asp:TextBox></td>
                                                                          <td  class="right" style="width: 107px; height: 30px">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td  nowrap="nowrap"  class="center" style="width: 213px; height: 30px">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td class="left" style="width: 159px; height: 30px">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                          <td class="left" style="width: 159px; height: 30px"><asp:Button ID="btnExport" CssClass="button" runat="server" Text="Export" /></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    
                                                                                    <tr>
                                                                                        <td valign="top" align="left">
                                                                                        <asp:GridView ID="grdvCarBreakUp" runat="server" HeaderStyle-ForeColor="white" AutoGenerateColumns="False" HorizontalAlign="Left" 
                                                            Width="800px" ShowFooter="true" FooterStyle-CssClass="Gridheading" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" EnableViewState="False" AllowSorting="True">
                                                              <Columns>
                                                              
                                                              <asp:TemplateField SortExpression ="Date" HeaderText="Date">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lblFDate" runat="server" Text="Total"></asp:Label>
                                                              </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                              
                                                              <asp:TemplateField SortExpression ="Car_Bookings" HeaderText="Car Bookings">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblCar_Bookings2" runat="server" Text='<%# Eval("Car_Bookings") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                             </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                               <asp:TemplateField SortExpression ="Hotel_Bookings" HeaderText="Hotel Bookings">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblHotel_Bookings2" runat="server" Text='<%# Eval("Hotel_Bookings") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                             </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                               <asp:TemplateField SortExpression ="Car_Voucher_Printed" HeaderText="Car Voucher Printed">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblCar_Voucher_Printed2" runat="server" Text='<%# Eval("Car_Voucher_Printed") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                             </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                               <asp:TemplateField SortExpression ="Car_Number_of_Days" HeaderText="Car Number of Days">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblCar_Number_of_Days2" runat="server" Text='<%# Eval("Car_Voucher_Printed") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                             </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                               <asp:TemplateField SortExpression ="Hotel_Nights" HeaderText="Hotel Nights">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblHotel_Nights2" runat="server" Text='<%# Eval("Hotel_Nights") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                             </FooterTemplate>
                                                              </asp:TemplateField>
                                                 
                                                        </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" ForeColor="White"  />
                                                            <RowStyle CssClass="textbold" />
                                       <FooterStyle CssClass="Gridheading" />
                                   </asp:GridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                    
                                                                                    
                                                                                    <tr>
                                                                                        <td valign="top" align="left">
                                                                                        <asp:Panel ID="Panel1" runat="server" Visible="false" Width="700px">
                                                      <table border="0" cellpadding="0" cellspacing="0">
                                                                      <tr style="padding-top : 4pt; padding-bottom : 4pt">                                                                                                                                                
                                                                          <td nowrap="nowrap"  class="left" style="width: 235px; height: 30px"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount1" runat ="server"  Width="85px" CssClass="textboxgrey" ></asp:TextBox></td>
                                                                          <td  class="right" style="width: 107px; height: 30px">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev1" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td  nowrap="nowrap"  class="center" style="width: 213px; height: 30px">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlPageNumber1" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td class="left" style="width: 159px; height: 30px">
                                                                              <asp:LinkButton ID="lnkNext1" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                          <td class="left" style="width: 159px; height: 30px"><asp:Button ID="btnExportBrk" CssClass="button" runat="server" Text="Export" /></td>
                                                                      </tr>
                                                                  </table>
                                                                  </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    
                                                                                    <tr>
                                                                                        <td valign="top" align="left">
                                                                                        
                                                                                        </td>
                                                                                    </tr>
                                                                                    
                                                                                    </table>
                                                                                    
                                                                                    
                                                                                      
                              
                                   
                                   
                                   
                                  <%-- <MotiveDetails DATE='' LCODE='' AIR_ADD_BOOKINGS='' AIR_CANCEL_BOOKINGS='' AIR_MODIFY_BOOKINGS='' AIR_NET_BOOKINGS='' PASSIVE_ADD='' PASSIVE_CANCEL='' PASSIVE_MODIFY=''
                                    PASSIVE_NET='' NETBOOKINGS='' AirBookings='' CarBookings='' HotelBookings=''/>--%>		
                                  
                                   
                                   
                                   
                                  <%-- <MotiveDetails LCODE='' Date='' Car_Bookings='' Hotel_Bookings='' Car_Voucher_Printed='' 
                                   Car_Number_of_Days='' Hotel_Nights='' />	--%>				  
                                  
                                   
                                     
                                  
                                                                                    </td>                                                                 
                                                                            </tr> 
                                                                    </table>
    </td>
    </tr>
    </table>
    
    
   
    </form>
</body>
</html>
