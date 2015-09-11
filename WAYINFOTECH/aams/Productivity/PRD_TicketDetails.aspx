<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRD_TicketDetails.aspx.vb"
    Inherits="Productivity_PRD_TicketDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Productivity::Ticket Booking Details</title>

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <!-- import the language module -->

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <!-- import the calendar setup module -->

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script language="javascript" type="text/javascript"> 
      function CallPrint( strid )
       {
        var prtContent = document.getElementById( strid ); 
        prtContent.border = 0; //set no border here 
        var strOldOne=prtContent.innerHTML;
        var WinPrint = window.open('','','letf=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');
        WinPrint.document.write(prtContent.outerHTML);
        WinPrint.document.close();
        WinPrint.focus();
        WinPrint.print();
        WinPrint.close();

       }      
    function CheckValidation()
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
                     var objChkAirBreakup =document.getElementById('ChkAirBreackup');
                       
                   if (objChkAirBreakup.checked==true) 
                   {
                       if(document.getElementById('<%=DlstAirline.ClientID%>').value.trim()=="")
                       {
                         // document.getElementById("lblError").innerHTML='Airline is mandatory.';          
                         // document.getElementById('<%=DlstAirline.ClientID%>').focus();                          
                          //return false;
                      }                    
                   } 
                    document.getElementById("lblError").innerHTML="";
              
     }
            function CheckedUnchecked1AProd()
         {
                   var objChkProductivity =document.getElementById('Chk1AProd');
                   var objchkNIDT=document.getElementById('chkNIDT');
                   var objChkAirBreakup =document.getElementById('ChkAirBreackup');
                       
                   if (objChkAirBreakup.checked==true) 
                   {
                      objChkProductivity.disabled=true;
                      objChkProductivity.checked=false;
                      
                    objchkNIDT.disabled=true;
                    objchkNIDT.checked=false;
                        
                      document.getElementById('Div1').className ="displayBlock";
                      document.getElementById('Div2').className ="displayBlock";                  
                      
                   } 
                   else
                   {
                        objchkNIDT.disabled=false;
                      document.getElementById('Div1').className ="displayNone";
                      document.getElementById('Div2').className ="displayNone";
                      if (document.getElementById('Hd1AprodRight').value=="1" )
                      {
                        objChkProductivity.disabled=false;
                      //  objChkProductivity.checked=false;                        
                      }
                      else
                      {
                      objChkProductivity.disabled=true;
                      objChkProductivity.checked=false;  
                      
                      }
                   }
              
   	          //  return false;
       }
    </script>

</head>
<body>
    <form id="form1" runat="server" defaultfocus="drpCountry" defaultbutton="btnSearch">
        <table width="860px" align="left" class="border_rightred">
            <tr>
                <td valign="top" style="width: 860px;">
                    <table width="100%" class="left">
                        <tr>
                            <td>
                                <span class="menu">Productivity -&gt;</span><span class="sub_menu">Ticket Booking Details</span></td>
                        </tr>
                        <tr>
                            <td class="heading center">
                                Ticket Booking Details</td>
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
                                                                <td style="width: 3%">
                                                                </td>
                                                                <td class="subheading" colspan="5">
                                                                    Agency Details</td>
                                                                <td style="width: 2%">
                                                                </td>
                                                                <td style="width: 10%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 3%">
                                                                    <input id="Hidden1" runat="server" style="width: 1px" type="hidden" /></td>
                                                                <td class="textbold" style="width: 15%">
                                                                    Agency Name</td>
                                                                <td colspan="4" style="width: 70%">
                                                                    <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                        TabIndex="1" Width="601px" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                                <td class="left" style="width: 2%">
                                                                </td>
                                                                <td class="left" style="width: 10%">
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="2"
                                                                        AccessKey="A" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 3%">  <input id="Hd1AprodRight" runat="server" style="width: 1px" type="hidden" value ="1" />
                                                                </td>
                                                                <td class="textbold" style="width: 15%">
                                                                    Address</td>
                                                                <td colspan="4" style="width: 70%">
                                                                    <asp:TextBox ID="txtAdd" runat="server" CssClass="textboxgrey" Height="41px" MaxLength="50"
                                                                        ReadOnly="True" TabIndex="1" TextMode="MultiLine" Width="601px"></asp:TextBox></td>
                                                                <td class="left" style="width: 2%">
                                                                </td>
                                                                <td style="width: 10%;" class="left" valign="top">
                                                                    <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="2" Text="Export"
                                                                        AccessKey="E" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 3%;">
                                                                </td>
                                                                <td class="textbold" style="width: 15%;">
                                                                    City</td>
                                                                <td style="width: 29%;">
                                                                    <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                                        TabIndex="1" Width="245px"></asp:TextBox></td>
                                                                <td class="textbold" style="width: 2%;">
                                                                </td>
                                                                <td class="textbold" style="width: 10%;">
                                                                    Country</td>
                                                                <td class="textbold" style="width: 29%;">
                                                                    <asp:TextBox ID="txtCountry" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                        ReadOnly="True" TabIndex="1" Width="245px"></asp:TextBox></td>
                                                                <td class="left" style="width: 2%">
                                                                </td>
                                                                <td class="center" style="width: 10%;">
                                                                    <asp:Button ID="btnReset" runat="server" AccessKey="R" CssClass="button" TabIndex="2"
                                                                        Text="Reset" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 3%;">
                                                                </td>
                                                                <td class="textbold" style="width: 15%;">
                                                                    Month</td>
                                                                <td style="width: 29%;">
                                                                    <table cellpadding="0" border="0" cellspacing="0" style="width: 100%;">
                                                                        <tr>
                                                                            <td style="width: 40%;">
                                                                                <asp:DropDownList ID="drpMonthFrom" runat="server" CssClass="dropdownlist" TabIndex="1"
                                                                                    Width="96px">
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
                                                                            <td align="right" valign="top" style="width: 20%;">
                                                                                <span class="textbold">Year</span>
                                                                            </td>
                                                                            <td align="right" valign="top" style="width: 40%;">
                                                                                <asp:DropDownList ID="drpYearFrom" TabIndex="1" runat="server" CssClass="dropdownlist"
                                                                                    Width="66px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td class="textbold" style="width: 2%;">
                                                                </td>
                                                                <td class="textbold" style="width: 10%;">
                                                                    Month</td>
                                                                <td class="textbold" style="width: 29%;">
                                                                    <table cellpadding="0" border="0" cellspacing="0" style="width: 100%;">
                                                                        <tr>
                                                                            <td style="width: 40%;">
                                                                                <asp:DropDownList ID="drpMonthTo" runat="server" CssClass="dropdownlist" TabIndex="1"
                                                                                    Width="96px">
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
                                                                            <td align="right" valign="top" style="width: 20%;">
                                                                                <span class="textbold">Year</span>
                                                                            </td>
                                                                            <td align="right" valign="top" style="width: 40%;">
                                                                                <asp:DropDownList ID="drpYearTo" TabIndex="1" runat="server" CssClass="dropdownlist"
                                                                                    Width="66px">
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
                                                                <td style="width: 3%;">
                                                                </td>
                                                                <td class="textbold" style="width: 15%;">
                                                                </td>
                                                                <td style="width: 29%;">
                                                                    <asp:CheckBox ID="ChkWholeGroup" runat="server" CssClass="textbox" TabIndex="1"
                                                                        Text="Show Productivity for whole group" Width="243px" Height="25px" /></td>
                                                                <td class="textbold" style="width: 2%;">
                                                                </td>
                                                                <td class="textbold" style="width: 10%;" valign ="top" ><div id="Div1" runat ="server" >Airline&nbsp;
                                                                </div>
                                                                </td>
                                                                <td class="textbold" style="width: 29%;" valign ="top" ><div id="Div2" runat="server"><asp:DropDownList ID="DlstAirline" runat="server" CssClass="dropdownlist" Width="250px"
                                                                            TabIndex="1"></asp:DropDownList></div>&nbsp;
                                                                </td>
                                                                <td class="left" style="width: 2%">
                                                                </td>
                                                                <td class="center" style="width: 10%;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 15%;">
                                                                    <span class="subheading"></span>
                                                                </td>
                                                                <td colspan="6" class="subheading">
                                                                    <span style="font-family: Arial">Ticket Details</span></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 3%;">
                                                                </td>
                                                                <td colspan="5" valign="top" style="width: 85%;">
                                                                    <table style="width:100%;">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:CheckBox ID="ChkTicket" TabIndex="1" runat="server" Text="Ticket" CssClass="textbox">
                                                                                </asp:CheckBox></td>
                                                                            <td>
                                                                                <asp:CheckBox ID="ChkDailyBook" TabIndex="1" runat="server" Text="Daily Booking"
                                                                                    CssClass="textbox"></asp:CheckBox></td>
                                                                            <td>
                                                                                <asp:CheckBox ID="Chk1AProd" TabIndex="1" runat="server" Text="1A Productivity" CssClass="textbox">
                                                                                </asp:CheckBox></td>
                                                                            <td>
                                                                                <asp:CheckBox ID="ChkAllCRS" TabIndex="1" runat="server" Text="All CRS MIDT" CssClass="textbox">
                                                                                </asp:CheckBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:CheckBox ID="chkNIDT" TabIndex="1" runat="server" Text="NIDT" CssClass="textbox">
                                                                                </asp:CheckBox>
                                                                            </td>
                                                                            
                                                                            <td>
                                                                                <asp:CheckBox ID="ChkAirBreackup" TabIndex="1" runat="server" Text="Air Breakup"
                                                                                    CssClass="textbox"></asp:CheckBox>
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
                                                                <td>
                                                                </td>
                                                                <td colspan="7">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="8">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="8" style="height: 10px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td colspan ="2" class="textbold">No. of records found&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3"
                                                                        TabIndex="1" Width="100px" ReadOnly="True" Text="0"></asp:TextBox></td>
                                                                
                                                                <td colspan="2">
                                                                    <span class="textbold"><b> </b></span>
                                                                </td>
                                                                <td colspan="3">
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
                <td valign="top" style="padding-left: 4px;">
                    <table width="860px" border="0" align="left" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="redborder">
                                <asp:GridView EnableViewState="false" ID="grdvTicketDetails" runat="server" AutoGenerateColumns="False" ShowFooter ="true" 
                                    HorizontalAlign="Center" Width="1010px" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                    AllowSorting="true" HeaderStyle-ForeColor="white" AlternatingRowStyle-CssClass="lightblue">
                                    <Columns>
                                        <asp:BoundField DataField="LCODE" HeaderText="Lcode " SortExpression="LCODE" >
                                            <ItemStyle Width="50px" Wrap="False" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="AGENCYNAME" HeaderText="Agency " SortExpression="AGENCYNAME">
                                            <ItemStyle Width="150px" Wrap="true"/>
                                            <HeaderStyle Wrap="False" />
                                          </asp:BoundField>
                                        <asp:BoundField DataField="ADDRESS" HeaderText="Address " SortExpression="ADDRESS">
                                             <ItemStyle Width="150px" Wrap="true"/>
                                            <HeaderStyle Wrap="False" />
                                          </asp:BoundField>
                                       <asp:BoundField DataField="CITY" HeaderText="City " SortExpression="CITY" >
                                        <ItemStyle Width="50px" Wrap="False" />
                                       </asp:BoundField>
                                       
                                        <asp:BoundField DataField="COUNTRY" HeaderText="Country " SortExpression="COUNTRY">
                                            <ItemStyle Width="50px" Wrap="False" />
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="AIRLINE_NAME" HeaderText="Airline " SortExpression="AIRLINE_NAME">
                                            <ItemStyle Width="50px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MONTH" HeaderText="Month " SortExpression="MONTH">
                                            <ItemStyle Width="35px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="YEAR" HeaderText="Year " SortExpression="YEAR">
                                            <ItemStyle Width="35px" Wrap="False" />
                                        </asp:BoundField>
                                        
                                          <asp:BoundField DataField="TKT_NETBOOKINGS" HeaderText="Tickets " SortExpression="TKT_NETBOOKINGS">
                                            <ItemStyle Width="50px" Wrap="False"  HorizontalAlign ="Right" />
                                            <HeaderStyle Wrap="False" />
                                             <FooterStyle HorizontalAlign ="Right" />
                                        </asp:BoundField>
                                          <asp:BoundField DataField="DB_NETBOOKINGS" HeaderText="Daily Productivity" SortExpression="DB_NETBOOKINGS">
                                            <ItemStyle Width="100px" Wrap="False" HorizontalAlign ="Right"  />
                                            <HeaderStyle Wrap="False" />
                                            <FooterStyle HorizontalAlign ="Right" />
                                        </asp:BoundField>
                                          <asp:BoundField DataField="A_NETBOOKINGS" HeaderText="1A Productivity " SortExpression="A_NETBOOKINGS">
                                            <ItemStyle Width="100px" Wrap="False"   HorizontalAlign ="Right"/>
                                            <HeaderStyle Wrap="False" />
                                             <FooterStyle HorizontalAlign ="Right" />
                                        </asp:BoundField>
                                          <asp:BoundField DataField="MT_NETBOOKINGS" HeaderText="ALL CRS MIDT " SortExpression="MT_NETBOOKINGS">
                                            <ItemStyle Width="90px" Wrap="False"  HorizontalAlign ="Right" />
                                            <HeaderStyle Wrap="False" />
                                             <FooterStyle HorizontalAlign ="Right" />
                                        </asp:BoundField>
                                        
                                         <asp:BoundField DataField="CODD" HeaderText="CODD" SortExpression="CODD">
                                            <ItemStyle Width="50px" Wrap="False" HorizontalAlign="Right" />
                                            <HeaderStyle Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="HX_BOOKINGS" HeaderText="HX BOOKINGS" SortExpression="HX_BOOKINGS">
                                            <ItemStyle Width="50px" Wrap="False" HorizontalAlign="Right" />
                                            <HeaderStyle Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:BoundField>

                                      
                                      
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
                                                <span class="textbold"><b>&nbsp;</b></span>&nbsp;&nbsp;</td>
                                            <td style="width: 25%" class="right">
                                                <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                            <td style="width: 20%" class="center">
                                                <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber"
                                                    Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
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
                    </table>
                </td>
            </tr>
        </table>
    </form>
   
</body>
 <script type ="text/javascript" >
    CheckedUnchecked1AProd();    
    </script>
</html>
