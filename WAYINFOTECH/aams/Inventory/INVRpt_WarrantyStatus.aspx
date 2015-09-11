<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INVRpt_WarrantyStatus.aspx.vb" Inherits="Inventory_INVRpt_WarrantyStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Inventory Warranty Status Report </title>
   <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />   
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/DisableRightClick.js"></script>
    <script type="text/javascript" language="javascript">
    //txtOrderFromDt
    //txtOrderDateTo
     function ValidateOrderSearch()
       {
        if(document.getElementById('<%=txtOrderFromDt.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtOrderFromDt.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Purchase Order Date Format is Invalid.";			
	       document.getElementById('<%=txtOrderFromDt.ClientId%>').focus();
	       return(false);  
        }
       }
         
        if(document.getElementById('<%=txtOrderDateTo.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtOrderDateTo.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Purchase Order Date-To Format is Invalid.";			
	       document.getElementById('<%=txtOrderDateTo.ClientId%>').focus();
	       return(false);  
        }
       }
         if(document.getElementById('<%=txtOrderDateTo.ClientId%>').value != '' && document.getElementById('<%=txtOrderFromDt.ClientId%>').value != '')
        {
        if (compareDates(document.getElementById('<%=txtOrderFromDt.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtOrderDateTo.ClientId%>').value,"d/M/yyyy")=='1')	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Purchase Order From Date Cann't be Greater than Date-To.";			
	       document.getElementById('<%=txtOrderDateTo.ClientId%>').focus();
	       return(false);  
        }
       }  
       }
    
    function ResetWarrantyStatus()
    {
    document.getElementById("txtPurchaseOrderNo").value='';
    document.getElementById("txtAmadeusSerialNo").value='';
    document.getElementById("txtOrderFromDt").value='';
    document.getElementById("txtOrderDateTo").value='';
    document.getElementById("txtVendorSerialNo").value='';
    document.getElementById("drpProduct").selectedIndex='0';
    return false;
    }
    
    </script>
    
     </head>
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>

<body>
    <form id="form1" runat="server" defaultbutton="btnDisplay" defaultfocus="txtPurchaseOrderNo" >
     <table width="860px"  align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Inventory-&gt;</span><span class="sub_menu"> Warranty Status Reports</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" >
                                <span> Warrranty Status Report</span></td>
                        </tr>
                        <tr>
                            <td >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center" >
                                            <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                <tr>
                                                    <td colspan="8" align="center" height="25px">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 5%; height: 18px;" class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 23%; height: 18px;" class="textbold">
                                                        <span class="textbold">Purchase Order No.</span></td>
                                                    <td style="width: 5%; height: 18px;">
                                                        <asp:TextBox ID="txtPurchaseOrderNo" runat="server" CssClass="textbox" MaxLength="40" Style="left: 0px;
                                                            position: relative; top: 0px" TabIndex="1" Width="176px"></asp:TextBox></td>
                                                    <td style="width: 8%; height: 18px;" class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 21%; height: 18px;" class="textbold">
                                                        <span class="textbold">Product</span>
                                                    </td>
                                                    <td style="width: 33%; height: 18px;">
                                                        <asp:DropDownList ID="drpProduct" runat="server" Width="184px" TabIndex="2" CssClass="dropdownlist">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 3%; height: 18px;">
                                                        <asp:Button ID="btnDisplay" CssClass="button" runat="server" Text="Print" TabIndex="9" AccessKey="p" /></td>
                                                    <td style="width: 13%; height: 18px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 5%; height: 18px;">
                                                    </td>
                                                    <td class="textbold" style="width: 23%; height: 18px;">
                                                        Amadeus Serial No.</td>
                                                    <td style="width: 5%; height: 18px;">
                                                        <asp:TextBox ID="txtAmadeusSerialNo" runat="server" CssClass="textbox" MaxLength="40" Style="left: 0px;
                                                            position: relative; top: 0px" TabIndex="3" Width="176px"></asp:TextBox></td>
                                                    <td class="textbold" style="width: 8%; height: 18px;">
                                                    </td>
                                                    <td class="textbold" style="width: 21%; height: 18px;">
                                                        Vendor Serial No.</td>
                                                    <td style="width: 33%; height: 18px;">
                                                        <asp:TextBox ID="txtVendorSerialNo" runat="server" CssClass="textbox" MaxLength="40" Style="left: 0px;
                                                            position: relative; top: 0px" TabIndex="4" Width="176px"></asp:TextBox></td>
                                                    <td style="width: 3%; height: 18px;">
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="10" AccessKey="r" /></td>
                                                    <td style="width: 13%; height: 18px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 5%; height: 18px;" class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 23%; height: 18px;" class="textbold">
                                                        Purchase Order Date
                                                        From</td>
                                                    <td style="width: 5%; height: 18px;">
                                                        <asp:TextBox ID="txtOrderFromDt" runat="server" CssClass="textbox" MaxLength="10" Style="left: 0px;
                                                            position: relative" TabIndex="5" Width="150px"></asp:TextBox>
                                                        <img id="Img1" alt="" src="../Images/calender.gif" style="cursor: pointer;
                                                            position: relative" tabindex="6" title="Date selector" />
                                                             <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtOrderFromDt.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "Img1",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script></td>
                                                    <td style="width: 8%; height: 18px;" class="textbold">
                                                        &nbsp;</td>
                                                    <td style="width: 21%; height: 18px;" class="textbold">
                                                        <span class="textbold">Purchase Order DateTo</span></td>
                                                    <td style="width: 33%; height: 18px;">
                                                        <asp:TextBox ID="txtOrderDateTo" runat="server" CssClass="textbox" MaxLength="10"
                                                            Style="left: 0px; position: relative; top: 0px;" TabIndex="7" Width="152px"></asp:TextBox>
                                                        <img id="Img2" alt="" src="../Images/calender.gif" style="cursor: pointer;
                                                            position: relative" tabindex="15" title="Date selector" height="15" />
                                                             <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtOrderDateTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "Img2",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script></td>
                                                    <td style="width: 3%; height: 18px;">
                                                    </td>
                                                    <td style="width: 16%; height: 18px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 5%; height: 18px">
                                                    </td>
                                                    <td class="textbold" style="width: 23%; height: 18px">
                                                        Equipment Code</td>
                                                    <td style="width: 5%; height: 18px">
                                                        <asp:TextBox ID="txtEquipCode" runat="server" CssClass="textbox" MaxLength="40" Style="left: 0px;
                                                            position: relative; top: 0px" TabIndex="3" Width="176px"></asp:TextBox></td>
                                                    <td class="textbold" style="width: 8%; height: 18px">
                                                    </td>
                                                    <td class="textbold" style="width: 21%; height: 18px">
                                                    </td>
                                                    <td style="width: 33%; height: 18px">
                                                    </td>
                                                    <td style="width: 3%; height: 18px">
                                                    </td>
                                                    <td style="width: 16%; height: 18px">
                                                    </td>
                                                </tr>
                                                  <tr>
                                                    <td style="height: 8px;" class="textbold" colspan="8" width="100%">
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
