<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INVRPT_StockTransferTracking.aspx.vb" Inherits="Inventory_INVRPT_StockTransferTracking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Inventory::Stock Transfer Tracking Feature</title>
    <script type="text/javascript" src="../JavaScript/DisableRightClick.js"></script>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
   <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
  <script language="javascript" type="text/javascript">
       function ValidateStockTransferTrackingSearch()
       {
        if(document.getElementById('<%=txtTransferDateFrom.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtTransferDateFrom.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date Of Transfer-From Date Format is Invalid.";			
	       document.getElementById('<%=txtTransferDateFrom.ClientId%>').focus();
	       return(false);  
        }
        }
         
        if(document.getElementById('<%=txtTransferDateTo.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtTransferDateTo.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date Of Transfer-To Date Format is Invalid.";			
	       document.getElementById('<%=txtTransferDateTo.ClientId%>').focus();
	       return(false);  
        }
        }
         if(document.getElementById('<%=txtTransferDateTo.ClientId%>').value != '' && document.getElementById('<%=txtTransferDateTo.ClientId%>').value != '')
        {
        if (compareDates(document.getElementById('<%=txtTransferDateFrom.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtTransferDateTo.ClientId%>').value,"d/M/yyyy")=='1')	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date Of Transfer From Can't be Greater than Date Of Transfer To.";			
	       document.getElementById('<%=txtTransferDateTo.ClientId%>').focus();
	       return(false);  
        }
        }  
     }
                 
    
        function ResetAll()
        {
         document.getElementById("txtAmadeusSerialNo").value=""; 
         document.getElementById("txtVendorSerialNo").value=""; 
         document.getElementById("txtEquipCode").value=""; 
         document.getElementById("lblError").innerHTML=""; 
         document.getElementById("txtTransferDateFrom").value="";  
         document.getElementById("txtTransferDateTo").value=""; 
         document.getElementById("drpGodownFrom").selectedIndex=0;  
         document.getElementById("drpGodownTo").selectedIndex=0;  
         document.getElementById("txtAmadeusSerialNo").focus();
         document.getElementById("INV_Report_0").checked=true;
         return false;
        }
    
  
  </script>
</head>
<body>
    <form id="form1" runat="server" defaultfocus="txtAmadeusSerialNo" defaultbutton="btnDisplay">
   
     <div>
    <table width="840px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Inventory -&gt;</span><span class="sub_menu">Stock Transfer Tracking&nbsp;
                            </span></td>
                        </tr>
                        <tr>
                            <td class="heading center" style="height: 20px" >
                                Stock Transfer Tracking </td>
                        </tr>
                        <tr>
                            <td >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                        <tr>
                                            <td class="center" colspan="7" style="height: 25px" >
                                              <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2%; height: 18px;">
                                            </td>
                                            <td class="textbold" style="width: 17%; height: 18px;">
                                                Amadeus Serial No.</td>
                                            <td style="width: 26%; height: 18px;">
                                                <asp:TextBox ID="txtAmadeusSerialNo" runat="server" CssClass="textbox" MaxLength="40"
                                                    Style="left: 0px; position: relative; top: 0px" TabIndex="1" Width="170px"></asp:TextBox></td>
                                            <td class="textbold" style="width: 1%; height: 18px;">
                                            </td>
                                            <td class="textbold" style="width: 14%; height: 18px;">
                                                Vendor Serial No.</td>
                                            <td style="width: 26%; height: 18px;">
                                                <asp:TextBox ID="txtVendorSerialNo" runat="server" CssClass="textbox" MaxLength="40"
                                                    Style="left: 0px; position: relative; top: 0px" TabIndex="2" Width="170px"></asp:TextBox></td>
                                            <td class="center" style="width: 16%; height: 18px;">
                                               <asp:Button ID="btnDisplay" CssClass="button" runat="server" Text="Display" TabIndex="10" Width="85px" style="position: relative; left: -6px; top: 0px;" AccessKey="d" /></td>
                                        </tr>
                                       
                                        <tr>
                                            <td style="width:2%; height: 18px;">
                                            </td>
                                            <td class="textbold" style="width:17%; height: 18px;">
                                                Date Of Transfer From</td>
                                            <td style="width: 26%; height: 18px;">
                                                <asp:TextBox ID="txtTransferDateFrom" runat="server" CssClass="textbox" MaxLength="10"
                                                    Style="left: 0px; position: relative; top: 0px;" TabIndex="3" Width="170px"></asp:TextBox>
                                                <img id="Img1" alt="" src="../Images/calender.gif" style="cursor: pointer; position: relative"
                                                    tabindex="4" title="Date selector" />
                                                     <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtTransferDateFrom.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "Img1",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script></td>
                                            <td class="textbold" style="width:1%; height: 18px;">
                                               </td>
                                            <td class="textbold" style="width: 14%; height: 18px;">
                                                Date Of Transfer To</td>
                                            <td style="width: 26%; height: 18px;">
                                                <asp:TextBox ID="txtTransferDateTo" runat="server" CssClass="textbox" MaxLength="10"
                                                    Style="left: 0px; position: relative; top: 0px" TabIndex="5" Width="170px"></asp:TextBox>
                                                <img id="Img2" alt=""  height="15" src="../Images/calender.gif" style="cursor: pointer;
                                                    position: relative" tabindex="6" title="Date selector" />
                                                    <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtTransferDateTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "Img2",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script> </td>
                                            <td class="center" style="width: 16%; height: 18px;">
                                                <asp:Button ID="btnReset" runat="server" CssClass="button" Style="left: -6px; position: relative; top: 0px;"
                                                    TabIndex="11" Text="Reset" Width="85px" AccessKey="r" /></td>
                                        </tr>
                                        <tr>
                                            <td  style="width:2%; height: 18px;"></td>
                                            <td class="textbold" style="width:17%; height: 18px;" >
                                                Godown From
                                                </td>
                                            <td style="width:26%; height: 18px;"><asp:DropDownList ID="drpGodownFrom" runat="server" CssClass="dropdownlist" Width="176px" TabIndex="7" style="position: relative" onkeyup="gotop(this.id)">
                                            </asp:DropDownList></td>
                                             <td class="textbold" style="width:1%; height: 18px;">
                                               </td>
                                            <td class="textbold" style="width:14%; height: 18px;">     Godown To</td>
                                            <td style="width:26%; height: 18px;">
                                                <asp:DropDownList ID="drpGodownTo" runat="server" CssClass="dropdownlist" Width="176px" TabIndex="8" style="left: 0px; position: relative; top: 0px" onkeyup="gotop(this.id)">
                                                </asp:DropDownList></td>
                                            <td style="width:16%; height: 18px;" class="center"> </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2%; height: 18px">
                                            </td>
                                            <td class="textbold" style="width: 17%; height: 18px">
                                                Equipment Code</td>
                                            <td style="width: 26%; height: 18px">
                                                <asp:TextBox ID="txtEquipCode" runat="server" CssClass="textbox" MaxLength="3" TabIndex="9"
                                                    Width="170px" style="position: relative"></asp:TextBox></td>
                                            <td class="textbold" style="width: 1%; height: 18px">
                                            </td>
                                            <td class="textbold" style="width: 14%; height: 18px">
                                            </td>
                                            <td style="width: 26%; height: 18px">
                                            </td>
                                            <td class="center" style="width: 16%; height: 18px">
                                            </td>
                                        </tr>
                                         <tr>
                                                    <td class="subheading" colspan="8" align="center" style="height: 15px">
                                                       Reports
                                                    </td>
                                                </tr>
                                                  <tr>
                                                    <td style="height: 18px;" class="textbold" colspan="7" width="100%">
                                                    
                                                    <asp:RadioButtonList ID="INV_Report" RepeatDirection="Horizontal" runat="server"   Width="100%" TabIndex="15"   >
                                                        <asp:ListItem Selected="True">Detail Report</asp:ListItem>
                                                        <asp:ListItem>Summary Report</asp:ListItem>
                                                        
                                                        </asp:RadioButtonList>     
                                                        
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
    </div>
   
    </form>
   
</body>
</html>
