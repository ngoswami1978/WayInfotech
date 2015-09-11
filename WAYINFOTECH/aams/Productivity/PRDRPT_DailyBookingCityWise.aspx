<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRDRPT_DailyBookingCityWise.aspx.vb" Inherits="Productivity_PRDSR_DailyBookingCityWise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
   <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
  <script type="text/javascript" src="../Calender/calendar.js"></script>
  <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
  <script type="text/javascript" src="../Calender/calendar-setup.js"></script>
  <link rel="stylesheet" href="../CSS/AAMS.css" type="text/css" />
  <script type="text/javascript" language="javascript" >
  function ValidateReportInput()
  {
                if(document.getElementById('drpMonths').selectedIndex=='0')
                    {  
                     document.getElementById("lblError").innerHTML='Month  is Mandatory.';
                     document.getElementById("drpMonths").focus();
                    return false;
                    }
                    
                    if(document.getElementById('drpYears').selectedIndex=='0')
                    {  
                     document.getElementById("lblError").innerHTML='Year is Mandatory.';
                     document.getElementById("drpYears").focus();
                    return false;
                    }
                    
  }
  </script>
</head>
<body >
    <form id="form1" runat="server" defaultbutton ="btnDisplayReport">
    <table style="width:845px" align="left" class="border_rightred" >
            <tr>
                <td valign="top"  style="width:860px;">
                    <table width="100%" >
                        <tr>
                            <td valign="top"  align="left" ><span class="menu">Productivity-></span><span class="sub_menu">Daily Booking City Wise Report</span></td>
                        </tr>
                        <tr>
                            <td  class="heading" align="center" valign="top" >
                                Daily Booking City Wise</td>
                        </tr>
                        <tr>
                            <td valign="top" align="LEFT">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td  align ="left" class="redborder" style="width:860px" >                                 
                                                        <table  style="width:100%" border="0"  cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="9" align="center" style="height:25px;" valign="TOP"><asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr>
                                                                                                                
                                                             
                                                              <tr>
                                                                <td style="width:15%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:6%;" class="textbold" ><span class ="textbold">Month</span></td>
                                                                <td style="width:22%;" class="textbold" >
                                                                <asp:DropDownList id="drpMonths" runat="server" CssClass="dropdownlist" Width="158px" TabIndex="1"></asp:DropDownList></td>
                                                                <td style="width:1%;" class="textbold"></td>
                                                                <td style="width:2%;" class="textbold" ></td>
                                                                <td style="width:6%;"><span class="textbold">Year</span></td> 
                                                                <td style="width:20%;" class="textbold" ><asp:DropDownList id="drpYears" runat="server" CssClass="dropdownlist" Width="158px" TabIndex="1">
                                                                </asp:DropDownList></td>
                                                                <td style="width:5%;" class="textbold" ></td>
                                                                <td style="width:20%;" >
                                                                <asp:Button ID="btnDisplayReport" CssClass="button" runat="server" Text="Export" TabIndex="3" AccessKey="D" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 15%; height: 19px;">
                                                                </td>
                                                                <td class="textbold" style="width: 6%; height: 19px;">
                                                                </td>
                                                                <td class="textbold" colspan="5" style="height: 19px">
                                                                    <asp:RadioButtonList ID="rdCity" runat="server" RepeatDirection="Horizontal" Width="432px" TabIndex="1">
                                                                        <asp:ListItem Value="1" Selected=True >City</asp:ListItem>
                                                                        <asp:ListItem Value="2">Graph</asp:ListItem>
                                                                        <asp:ListItem Value="3">OfficeID</asp:ListItem>
                                                                        <asp:ListItem Value="4">Region</asp:ListItem>
                                                                        <asp:ListItem Value="5">State</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                               </td>
                                                                <td class="textbold" style="width: 5%; height: 19px;">
                                                                </td>
                                                                <td style="width: 20%; height: 19px; padding-top:3px;" valign="top" >
                                                                <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3" AccessKey="R" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td  style="height:2px;" class="textbold" colspan="9" align="center" valign="TOP" >                                                                
                                                                </td>
                                                            </tr>
                                                           <tr>
                                                                <td style="width:15%; height: 19px;" class="textbold" >&nbsp;</td>
                                                                <td style="width:6%; height: 19px;" class="textbold" ><span class="textbold"> </span></td>
                                                                <td style="width:22%; height: 19px;" class="textbold" >
                                                                 </td>
                                                                <td style="width:1%; height: 19px;" class="textbold">
                                                                </td>
                                                                <td style="width:2%; height: 19px;" class="textbold" ></td>
                                                                <td style="width:6%; height: 19px;" ><span class="textbold"> </span></td> 
                                                                <td style="width:20%; height: 19px;" class="textbold" >
                                                               </td>
                                                                <td style="width:5%; height: 19px;" class="textbold" >
                                                                </td>
                                                                <td style="width:20%; height: 19px;" ></td>
                                                            </tr> 
                                                           <tr height="3px"></tr>
                                                             <tr>
                                                                <td  style="height: 15px" class="textbold" colspan="9" align="center" valign="TOP" > 
                                                                                                                               
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td  class="textbold" colspan="9" align="center" valign="TOP" style="height:10px;">                                                                &nbsp;</td>
                                                            </tr>
                                                        </table>  
                                        </td>
                                    </tr>
                                       <tr>
                                            <td  class="textbold"  align="center" valign="TOP" style="height:10px;">                                                                
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
