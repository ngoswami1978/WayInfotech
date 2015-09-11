<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ISPRPT_Order.aspx.vb" Inherits="ISP_ISPRPT_Order" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <title>Search ISP Plan</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script> 
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
   
</head>
<body>
    <form id="form1" runat="server" defaultbutton ="btnDisplay" defaultfocus="txtAgencyName">
    <table width="860px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" >
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">ISP-&gt;</span><span class="sub_menu">ISP Order Report</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                <span >ISP</span> <span >&nbsp;Order Report</span></td>
                        </tr>
                        <tr>
                            <td valign="top" >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder" >
                                            <table border="0" cellpadding="2" cellspacing="1" style="width: 100%">
                                                                               <tr>
                                                                                    <td colspan="8" align ="center" ><asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>                                                                        
                                                                                </tr>
                                                                                 <tr>
                                                                                    <td colspan="8" ></td> 
                                                                                </tr>
                                                                               <tr>
                                                                                    <td style="width:3%"  >
                                                                                        &nbsp;</td>
                                                                                    <td style="width:17%" class="textbold"><span class="textbold">Agency Name</span><input type="hidden" id="hdAgencyNameId" runat="server" value="" style="width: 5px" />
                                                                                        <input type="hidden" id="hdAgencyName" runat="server" value="" style="width: 5px" /></td>
                                                                                    <td colspan="4" style="width:66%"><asp:TextBox ID="txtAgencyName" runat="server" MaxLength="40" Width="517px" TabIndex="1" CssClass="textbox" Height="18px"></asp:TextBox>&nbsp;<img tabIndex="2" src="../Images/lookup.gif" onclick="javascript:return PopupAgencyPageISPOrderReport();" style="cursor:pointer;" /></td>
                                                                                  
                                                                                    <td style="width:13%">
                                                                                       <asp:Button ID="btnDisplay" CssClass="button" runat="server" Text="Print" TabIndex="10" AccessKey="P" /></td>
                                                                               </tr> 
                                                                                <tr>
                                                                                    <td style="width:3%"  >
                                                                                        &nbsp;</td>
                                                                                    <td style="width:15%" class="textbold"><span class="textbold">City</span>
                                                                                    </td>
                                                                                    <td style="width:19%">
                                                                                        <asp:DropDownList ID="drpCity" runat="server" Width="208px" TabIndex="3" CssClass="dropdownlist" >
                                                                                        </asp:DropDownList></td>
                                                                                     <td style="width:3%" class="textbold" >
                                                                                        &nbsp;</td>
                                                                                    <td style="width:15%" class="textbold"><span class="textbold">Country</span>
                                                                                    </td>
                                                                                    <td style="width:20%">
                                                                                        <asp:DropDownList ID="drpCountry" runat="server" Width="206px" TabIndex="4" CssClass="dropdownlist" >
                                                                                        </asp:DropDownList>
                                                                                        </td>    
                                                                                    <td  style="width:3%"><asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="11" AccessKey="R" /></td>                                                                          
                                                                                    <td style="width:13%">
                                                                                       </td>
                                                                                </tr>                                                                       
                                                                                 <tr>
                                                                                    <td style="width:3%" >
                                                                                        &nbsp;</td>
                                                                                    <td style="width:15%" class="textbold"><span class="textbold">ISP Name</span>
                                                                                    </td>
                                                                                    <td style="width:19%">
                                                                                        <asp:DropDownList ID="drpIspname" runat="server" Width="208px" TabIndex="5" CssClass="dropdownlist" >
                                                                                        </asp:DropDownList></td>
                                                                                     <td style="width:3%" class="textbold" >
                                                                                        &nbsp;</td>
                                                                                    <td style="width:15%" class="textbold"><span class="textbold"></span>
                                                                                    </td>
                                                                                    <td style="width:20%">                                                                              
                                                                                        </td>    
                                                                                    <td  style="width:3%"></td>                                                                          
                                                                                    <td style="width:16%">
                                                                                       </td>
                                                                                </tr>                                                                       
                                                                                                                                               
                                                                                  <tr>
                                                                                    <td style="width:3%; height: 23px;" >
                                                                                        &nbsp;</td>
                                                                                    <td style="width:15%; height: 23px;" class="textbold">
                                                                                        From<span class="Mandatory">*</span>
                                                                                    </td>
                                                                                    <td style="width:19%; height: 23px;">
                                                                                        <asp:DropDownList ID="drpMonthFrom" runat="server" Width="100px" TabIndex="6" CssClass="dropdownlist" ><asp:ListItem Value="1">January</asp:ListItem>
                                                                                                    <asp:ListItem Value="2">February</asp:ListItem>
                                                                                                    <asp:ListItem Value="3">March</asp:ListItem>
                                                                                                    <asp:ListItem Value="4">April</asp:ListItem>
                                                                                                    <asp:ListItem Value="5">May</asp:ListItem>
                                                                                                    <asp:ListItem Value="6">June</asp:ListItem>
                                                                                                    <asp:ListItem Value="7">July</asp:ListItem>
                                                                                                    <asp:ListItem Value="8">August</asp:ListItem>
                                                                                                    <asp:ListItem Value="9">September</asp:ListItem>
                                                                                                    <asp:ListItem Value="10">October</asp:ListItem>
                                                                                                    <asp:ListItem Value="11">November</asp:ListItem>
                                                                                                    <asp:ListItem Value="12">December</asp:ListItem>
                                                                                        </asp:DropDownList>&nbsp;&nbsp;<asp:DropDownList ID="drpYearFrom" runat="server" Width="100px" TabIndex="7" CssClass="dropdownlist" >
                                                                                        </asp:DropDownList></td>
                                                                                     <td style="width:3%; height: 23px;" class="textbold" >
                                                                                        &nbsp;</td>
                                                                                    <td style="width:15%; height: 23px;" class="textbold"><span class="textbold">To</span><span class="Mandatory">*</span>
                                                                                    </td>
                                                                                    <td style="width:20%; height: 23px;"><asp:DropDownList ID="drpMonthTo" runat="server" Width="100px" TabIndex="8" CssClass="dropdownlist" ><asp:ListItem Value="1">January</asp:ListItem>
                                                                                                    <asp:ListItem Value="2">February</asp:ListItem>
                                                                                                    <asp:ListItem Value="3">March</asp:ListItem>
                                                                                                    <asp:ListItem Value="4">April</asp:ListItem>
                                                                                                    <asp:ListItem Value="5">May</asp:ListItem>
                                                                                                    <asp:ListItem Value="6">June</asp:ListItem>
                                                                                                    <asp:ListItem Value="7">July</asp:ListItem>
                                                                                                    <asp:ListItem Value="8">August</asp:ListItem>
                                                                                                    <asp:ListItem Value="9">September</asp:ListItem>
                                                                                                    <asp:ListItem Value="10">October</asp:ListItem>
                                                                                                    <asp:ListItem Value="11">November</asp:ListItem>
                                                                                                    <asp:ListItem Value="12">December</asp:ListItem>
                                                                                        </asp:DropDownList>&nbsp;&nbsp;<asp:DropDownList ID="drpYearTo" runat="server" Width="100px" TabIndex="9" CssClass="dropdownlist" >
                                                                                        </asp:DropDownList>                                                                              
                                                                                        </td>    
                                                                                    <td  style="width:3%; height: 23px;"></td>                                                                          
                                                                                    <td style="width:16%; height: 23px;">
                                                                                       </td>
                                                                                </tr>  
                                                                                   <tr>
                                                                                    <td colspan="8" >&nbsp;</td> 
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
