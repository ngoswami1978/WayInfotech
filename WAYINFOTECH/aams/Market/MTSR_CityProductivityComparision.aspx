<%@ Page Language="VB" EnableEventValidation="false" AutoEventWireup="false" CodeFile="MTSR_CityProductivityComparision.aspx.vb"
    Inherits="Market_MTSR_CityProductivityComparision" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Market::Airline Passive Report</title>

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <!-- import the language module -->

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <!-- import the calendar setup module -->

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script language="javascript" type="text/javascript"> 
    
      function SelectDateRange()
      {
      if (document.getElementById('ChkUseDailyBookData').checked==true)
      {
        if ( document.getElementById('DivMonthYearRange')!=null)
        {
         document.getElementById('DivMonthYearRange').style.display ="none";
         }
          if ( document.getElementById('DivDateRange')!=null)
        {
         document.getElementById('DivDateRange').style.display ="block";
         }
      }
      else
      {
      if ( document.getElementById('DivMonthYearRange')!=null)
        {
         document.getElementById('DivMonthYearRange').style.display ="block";
         }
          if ( document.getElementById('DivDateRange')!=null)
        {
         document.getElementById('DivDateRange').style.display ="none";
         }
      
      }
       
      //   alert(document.getElementById('ChkUseDailyBookData').checked);
      }
       function CheckValidation()
     {  
                   if (document.getElementById('ChkUseDailyBookData').checked==false)
            {
                      if(parseInt(document.getElementById("drpYearFrom").value) >parseInt(document.getElementById("drpYearTo").value))
                    {                   
                    document.getElementById("lblError").innerHTML='Date range is not valid.';          
                    document.getElementById("drpYearFrom").focus();
                    return false;
                    } 
                   
                   if(parseInt(document.getElementById("drpYearTo").value)- parseInt(document.getElementById("drpYearFrom").value)>4)
                    {                   
                    document.getElementById("lblError").innerHTML='Maximum number of years should be 4 years.';          
                    document.getElementById("drpYearFrom").focus();
                    return false;
                    } 
                    
           
                      if(parseInt(document.getElementById("drpYearFrom2").value) >parseInt(document.getElementById("drpYearTo2").value))
                    {                   
                    document.getElementById("lblError").innerHTML='Date range is not valid.';          
                    document.getElementById("drpYearFrom2").focus();
                    return false;
                    } 
                   
                   if(parseInt(document.getElementById("drpYearTo2").value)- parseInt(document.getElementById("drpYearFrom2").value)>4)
                    {                   
                    document.getElementById("lblError").innerHTML='Maximum number of years should be 4 years.';          
                    document.getElementById("drpYearFrom2").focus();
                    return false;
                    } 
                    
          }   
          
                    if (document.getElementById('ChkUseDailyBookData').checked==true)
            {
            
                if(document.getElementById('<%=txtDateFrom1.ClientId%>').value != '')
                 {
                 
                   if (compareDates(document.getElementById('<%=txtDateFrom1.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtDateTo1.ClientId%>').value,"d/M/yyyy")==1)
                    {
                       document.getElementById('<%=lblError.ClientId%>').innerText = "Date range is not valid.";			
	                   document.getElementById('<%=txtDateFrom1.ClientId%>').focus();
	                   return(false);  
                    }
                 } 
                   if(document.getElementById('<%=txtDateFrom2.ClientId%>').value != '')
                 {
                 
                   if (compareDates(document.getElementById('<%=txtDateFrom2.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtDateTo2.ClientId%>').value,"d/M/yyyy")==1)
                    {
                       document.getElementById('<%=lblError.ClientId%>').innerText = "Date range is not valid.";			
	                   document.getElementById('<%=txtDateFrom2.ClientId%>').focus();
	                   return(false);  
                    }
                 } 
//                    
          }   
          
           
     }
      
      
    </script>

</head>
<body>
    <form id="form1" runat="server" defaultfocus="drpAirline" defaultbutton="btnSearch">
        <table width="860px" align="left" class="border_rightred">
            <tr>
                <td valign="top" style="width: 860px;">
                    <table width="100%" class="left">
                        <tr>
                            <td>
                                <span class="menu">Market -&gt;</span><span class="sub_menu">City Productivity Comparision
                                    Report</span></td>
                        </tr>
                        <tr>
                            <td class="heading center">
                                City Productivity Comparision Report</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 860px;" class="redborder" valign="top">
                                                        <table border="0" cellpadding="2" cellspacing="1" style="width: 845px;" class="left">
                                                            <tr>
                                                                <td class="center" colspan="8">
                                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 3%;">
                                                                </td>
                                                                <td class="textbold" style="width: 15%;" align="left">
                                                                    City</td>
                                                                <td style="width: 29%;">
                                                                    <asp:DropDownList ID="drpCity" runat="server" CssClass="dropdownlist" Height="19px"
                                                                        TabIndex="1" Width="206px">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 2%;">
                                                                </td>
                                                                <td class="textbold" style="width: 10%;">
                                                                    Country</td>
                                                                <td class="textbold" style="width: 29%;">
                                                                    <asp:DropDownList ID="drpCountry" runat="server" CssClass="dropdownlist" Height="19px"
                                                                        TabIndex="2" Width="206px">
                                                                    </asp:DropDownList></td>
                                                                <td class="left" style="width: 2%">
                                                                </td>
                                                                <td class="center" style="width: 10%;">
                                                                    <asp:Button ID="btnSearch" runat="server" CssClass="button" TabIndex="23" Text="Search"
                                                                        Visible="true" AccessKey="A" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 3%;">
                                                                </td>
                                                                <td class="textbold" style="width: 15%;" align="left">
                                                                    Aoffice</td>
                                                                <td style="width: 29%;">
                                                                    <asp:DropDownList ID="drpAoffice" runat="server" CssClass="dropdownlist" Height="19px"
                                                                        TabIndex="3" Width="206px">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 2%;">
                                                                </td>
                                                                <td class="textbold" style="width: 10%;">
                                                                    Region</td>
                                                                <td class="textbold" style="width: 29%;">
                                                                    <asp:DropDownList ID="drpRegion" runat="server" CssClass="dropdownlist" Height="19px"
                                                                        TabIndex="4" Width="206px">
                                                                    </asp:DropDownList></td>
                                                                <td class="left" style="width: 2%">
                                                                </td>
                                                                <td class="center" style="width: 10%;">
                                                                    <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="24" Text="Export"
                                                                        Visible="true" AccessKey="E" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 3%;">
                                                                </td>
                                                                <td class="textbold">
                                                                    Group Type</td>
                                                                <td class="textbold" style="width: 20%;">
                                                                    <asp:DropDownList onkeyup="gotop(this.id)" CssClass="dropdownlist" TabIndex="17"
                                                                        ID="drpLstGroupType" runat="server">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="textbold" style="width: 10%;">
                                                                </td>
                                                                <td class="textbold" style="width: 29%;">
                                                                </td>
                                                                <td class="left" style="width: 2%">
                                                                </td>
                                                                <td class="center" style="width: 10%;">
                                                                    </td>
                                                                    <td class="center" style="width: 10%;">
                                                                    <asp:Button ID="btnReset" runat="server" CssClass="button" TabIndex="25" Text="Reset"
                                                                        Visible="true" AccessKey="R" />
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 3%;">
                                                                </td>
                                                                <td class="textbold" style="width: 15%;">
                                                                </td>
                                                                <td class="subheading" align="center" colspan="4" style="width: 72%;">
                                                                    Compare Between</td>
                                                                <td class="left" style="width: 2%">
                                                                </td>
                                                                <td class="center" style="width: 10%;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 3%;">
                                                                </td>
                                                                <td style="width: 15%;">
                                                                </td>
                                                                <td colspan="4" style="width: 72%;">
                                                                    <div id="DivMonthYearRange" runat="server">
                                                                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                            <tr>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="6" style="height: 2px;">
                                                                                </td>
                                                                            </tr>
                                                                            <td colspan="6" style="width: 87%;">
                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <%-- <td style="width:3%; "></td>--%>
                                                                                            <td align="left" colspan="6">
                                                                                                <span class="subheading">1'st Year Range</span></td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                    <tr>
                                                                                        <td colspan="6" style="height: 8px;">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <%-- <td style="width:3%; ">
                                                                                    </td>--%>
                                                                                        <td class="textbold" style="width: 17%;" align="center">
                                                                                            Period From</td>
                                                                                        <td style="width: 33%;">
                                                                                            <table cellpadding="0" border="0" cellspacing="0" style="width: 100%;">
                                                                                                <tr>
                                                                                                    <td style="width: 40%;">
                                                                                                        <asp:DropDownList ID="drpMonthFrom" runat="server" CssClass="dropdownlist" TabIndex="5"
                                                                                                            Width="102px">
                                                                                                            <asp:ListItem Value="1">January</asp:ListItem>
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
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td align="right" valign="top" style="width: 10%;">
                                                                                                    </td>
                                                                                                    <td align="right" valign="top" style="width: 50%;">
                                                                                                        <asp:DropDownList ID="drpYearFrom" TabIndex="6" runat="server" CssClass="dropdownlist"
                                                                                                            Width="71px">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td class="textbold" style="width: 2%;">
                                                                                        </td>
                                                                                        <td class="textbold" align="center" style="width: 13%;">
                                                                                            Period To</td>
                                                                                        <td class="textbold" style="width: 33%;">
                                                                                            <table cellpadding="0" border="0" cellspacing="0" style="width: 100%;">
                                                                                                <tr>
                                                                                                    <td style="width: 40%;">
                                                                                                        <asp:DropDownList ID="drpMonthTo" runat="server" CssClass="dropdownlist" TabIndex="7"
                                                                                                            Width="102px">
                                                                                                            <asp:ListItem Value="1">January</asp:ListItem>
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
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td align="right" valign="top" style="width: 10%;">
                                                                                                    </td>
                                                                                                    <td align="right" valign="top" style="width: 50%;">
                                                                                                        <asp:DropDownList ID="drpYearTo" TabIndex="8" runat="server" CssClass="dropdownlist"
                                                                                                            Width="71px">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td class="left" style="width: 2%;">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="height: 10px;">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <%--<td style="width:3%; "></td> --%>
                                                                                            <td align="left" colspan="6">
                                                                                                <span class="subheading">2'nd Year Range</span></td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                    <tr>
                                                                                        <td colspan="6" style="height: 8px;">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <%--  <td style="width:3%; ">
                                                                                    </td>--%>
                                                                                        <td class="textbold" style="width: 15%;" align="center">
                                                                                            Period From</td>
                                                                                        <td style="width: 29%;">
                                                                                            <table cellpadding="0" border="0" cellspacing="0" style="width: 100%;">
                                                                                                <tr>
                                                                                                    <td style="width: 40%;">
                                                                                                        <asp:DropDownList ID="drpMonthFrom2" runat="server" CssClass="dropdownlist" TabIndex="9"
                                                                                                            Width="102px">
                                                                                                            <asp:ListItem Value="1">January</asp:ListItem>
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
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td align="right" valign="top" style="width: 10%;">
                                                                                                    </td>
                                                                                                    <td align="right" valign="top" style="width: 50%;">
                                                                                                        <asp:DropDownList ID="drpYearFrom2" TabIndex="10" runat="server" CssClass="dropdownlist"
                                                                                                            Width="71px">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td class="textbold" style="width: 2%;">
                                                                                        </td>
                                                                                        <td class="textbold" style="width: 10%;" align="center">
                                                                                            Period To</td>
                                                                                        <td class="textbold" style="width: 29%;">
                                                                                            <table cellpadding="0" border="0" cellspacing="0" style="width: 100%;">
                                                                                                <tr>
                                                                                                    <td style="width: 40%;">
                                                                                                        <asp:DropDownList ID="drpMonthTo2" runat="server" CssClass="dropdownlist" TabIndex="11"
                                                                                                            Width="102px">
                                                                                                            <asp:ListItem Value="1">January</asp:ListItem>
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
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td align="right" valign="top" style="width: 10%;">
                                                                                                    </td>
                                                                                                    <td align="right" valign="top" style="width: 50%;">
                                                                                                        <asp:DropDownList ID="drpYearTo2" TabIndex="12" runat="server" CssClass="dropdownlist"
                                                                                                            Width="71px">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td class="left" style="width: 2%">
                                                                                        </td>
                                                                                        <td class="center" style="width: 10%;">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="height: 10%">
                                                                                            &nbsp;</td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </table>
                                                                    </div>
                                                                    <div id="DivDateRange" style="display: none" runat="server">
                                                                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                            <tr>
                                                                                <tr>
                                                                                    <td colspan="6" style="height: 2px;">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="6" style="width: 87%;">
                                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <%-- <td style="width:3%; "></td>--%>
                                                                                                    <td align="left" colspan="6">
                                                                                                        <span class="subheading">1'st Year Range</span></td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                            <tr>
                                                                                                <%-- <td style="width:3%; ">
                                                                                    </td>--%>
                                                                                                <td class="textbold" style="width: 17%;" align="center">
                                                                                                </td>
                                                                                                <td style="width: 33%;">
                                                                                                    <table cellpadding="0" border="0" cellspacing="0" style="width: 100%;">
                                                                                                        <tr>
                                                                                                            <td style="width: 40%;" class="textbold">
                                                                                                                &nbsp;Period From</td>
                                                                                                            <td align="right" valign="top" style="width: 60%;" colspan="2">
                                                                                                                <asp:TextBox ID="txtDateFrom1" runat="server" CssClass="textboxgrey" MaxLength="10"
                                                                                                                    TabIndex="13" Width="80px" ReadOnly="True"></asp:TextBox>
                                                                                                                <img id="imgDateFrom1" tabindex="14" alt="" src="../Images/calender.gif" title="Date selector"
                                                                                                                    runat="server" style="cursor: pointer" />&nbsp;

                                                                                                                <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateFrom1.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateFrom1",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                                </script>

                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                                <td class="textbold" style="width: 2%;">
                                                                                                </td>
                                                                                                <td class="textbold" style="width: 13%;" align="center">
                                                                                                    Period To</td>
                                                                                                <td class="textbold" style="width: 33%;">
                                                                                                    <table cellpadding="0" border="0" cellspacing="0" style="width: 100%;">
                                                                                                        <tr>
                                                                                                            <td style="width: 100%;" colspan="3">
                                                                                                                <asp:TextBox ID="txtDateTo1" runat="server" CssClass="textboxgrey" MaxLength="10"
                                                                                                                    TabIndex="15" Width="80px" ReadOnly="True"></asp:TextBox>
                                                                                                                <img id="imgDateTo1" tabindex="16" alt="" src="../Images/calender.gif" title="Date selector"
                                                                                                                    runat="server" style="cursor: pointer" />&nbsp;

                                                                                                                <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateTo1.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateTo1",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                                </script>

                                                                                                                &nbsp;</td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                                <td class="left" style="width: 2%;">
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="left" colspan="6">
                                                                                                    <span class="subheading">2'nd Year Range</span></td>
                                                                                            </tr>
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <%--<td style="width:3%; "></td> --%>
                                                                                                    <td colspan="7">
                                                                                                        <span class="textbold"></span>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                            <tr>
                                                                                                <%--  <td style="width:3%; ">
                                                                                    </td>--%>
                                                                                                <td class="textbold" style="width: 15%; height: 39px;" align="center">
                                                                                                </td>
                                                                                                <td style="width: 29%; height: 39px;">
                                                                                                    <table cellpadding="0" border="0" cellspacing="0" style="width: 100%;">
                                                                                                        <tr>
                                                                                                            <td style="width: 40%;" class="textbold">
                                                                                                                &nbsp;Period From</td>
                                                                                                            <td align="right" valign="top" style="width: 60%;" colspan="2">
                                                                                                                <asp:TextBox ID="txtDateFrom2" runat="server" CssClass="textboxgrey" MaxLength="10"
                                                                                                                    TabIndex="17" Width="80px" ReadOnly="True"></asp:TextBox>
                                                                                                                <img id="imgDateFrom2" tabindex="18" alt="" src="../Images/calender.gif" title="Date selector"
                                                                                                                    runat="server" style="cursor: pointer" />&nbsp;

                                                                                                                <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateFrom2.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateFrom2",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                                </script>

                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                                <td class="textbold" style="width: 2%; height: 39px;">
                                                                                                </td>
                                                                                                <td class="textbold" style="width: 10%; height: 39px;" align="center">
                                                                                                    Period To</td>
                                                                                                <td class="textbold" style="width: 29%; height: 39px;">
                                                                                                    <table cellpadding="0" border="0" cellspacing="0" style="width: 100%;">
                                                                                                        <tr>
                                                                                                            <td style="width: 100%;" colspan="3">
                                                                                                                <asp:TextBox ID="txtDateTo2" runat="server" CssClass="textboxgrey" MaxLength="10"
                                                                                                                    TabIndex="19" Width="80px" ReadOnly="True"></asp:TextBox>
                                                                                                                <img id="imgDateTo2" tabindex="20" alt="" src="../Images/calender.gif" title="Date selector"
                                                                                                                    runat="server" style="cursor: pointer" />&nbsp;

                                                                                                                <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateTo2.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateTo2",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                                </script>

                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                                <td class="left" style="width: 2%; height: 39px;">
                                                                                                </td>
                                                                                                <td class="center" style="width: 10%; height: 39px;">
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="height: 10%">
                                                                                                    &nbsp;</td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                        </table>
                                                                    </div>
                                                                </td>
                                                                <td style="width: 2%;">
                                                                </td>
                                                                <td style="width: 10%;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 3%;">
                                                                </td>
                                                                <td style="width: 15%;">
                                                                </td>
                                                                <td class="textbold" colspan="2" style="width: 44%;">
                                                                    <asp:CheckBox ID="ChkUseDailyBookData" runat="server" Width="244px" CssClass="textbox"
                                                                        Text="Use Daily Booking Data" TabIndex="21" Height="25px" /></td>
                                                                <td class="textbold" colspan="2" style="width: 2%;">
                                                                    <asp:CheckBox ID="ChkUseOrignalBookData" runat="server" Width="244px" CssClass="textbox"
                                                                        Text="NBS" TabIndex="22" Height="25px" />
                                                                </td>
                                                                <td class="left" style="width: 2%">
                                                                </td>
                                                                <td class="center" style="width: 10%;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 23px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="8">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="8" class="textbold">
                                                                    <asp:Label ID="lblFound" runat="server" Text="No. of records found " Font-Bold="True"
                                                                        Width="139px" Visible="False"></asp:Label>
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
                </td>
            </tr>
            <tr>
                <td valign="top" style="padding-left: 4px; height: 304px;">
                    <table width="860px" border="0" align="left" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="redborder" colspan="2" valign="top">
                                <%--<span  class ="lightblue">Airline Passive Report&nbsp; </span> --%>
                                <asp:GridView EnableViewState="false" ID="grdvCityProductivity" runat="server" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" AllowSorting="true" HeaderStyle-ForeColor="white" Width="100%"
                                    HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor" Caption="" ShowFooter="False"
                                    AlternatingRowStyle-CssClass="lightblue" BackColor="White" Font-Bold="False"
                                    ForeColor="Black" TabIndex="26">
                                    <Columns>
                                        <asp:BoundField DataField="CITY" HeaderText="City" SortExpression="CITY" />
                                        <asp:BoundField DataField="COMPDATE1_TOTAL" HeaderText="COMPDATE1_TOTAL" SortExpression="COMPDATE1_TOTAL" />
                                        <asp:BoundField DataField="COMPDATE2_TOTAL" HeaderText="COMPDATE2_TOTAL" SortExpression="COMPDATE2_TOTAL" />
                                        <asp:BoundField DataField="DIFFERENCE" HeaderText="Difference" SortExpression="DIFFERENCE" />
                                        <asp:BoundField DataField="DIFFERENCE_PER" HeaderText="Difference %" SortExpression="DIFFERENCE_PER" />
                                    </Columns>
                                    <AlternatingRowStyle CssClass="lightblue" />
                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" />
                                    <RowStyle CssClass="textbold" />
                                    <FooterStyle CssClass="Gridheading" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" valign="top">
                                <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr style="padding-top: 4pt; padding-bottom: 4pt">
                                            <td style="width: 30%" class="left">
                                                <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                    ID="txtRecordCount" ReadOnly="true" runat="server" CssClass="textboxgrey" MaxLength="3"
                                                    TabIndex="3" Width="100px" Text="0" Visible="True"></asp:TextBox></td>
                                            <td style="width: 25%" class="right">
                                                <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                            <td style="width: 20%" class="center">
                                                <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList onkeyup="gotop(this.id)"
                                                    ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                </asp:DropDownList></td>
                                            <td style="width: 25%" class="left">
                                                <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>

<script type="text/javascript" language="javascript">
         SelectDateRange();
</script>

</html>
