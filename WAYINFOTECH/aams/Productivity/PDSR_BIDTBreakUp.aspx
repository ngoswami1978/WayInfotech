<%@ Page Language="VB" EnableEventValidation="false" AutoEventWireup="false" CodeFile="PDSR_BIDTBreakUp.aspx.vb"
    Inherits="Productivity_PDSR_BIDTBreakUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Productivity::1 A Breakup</title>

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <!-- import the language module -->

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <!-- import the calendar setup module -->

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script language="javascript" type="text/javascript"> 
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
                    document.getElementById("lblError").innerHTML='Maximun number of years should be 4 years.';          
                    document.getElementById("drpYearFrom").focus();
                    return false;
                    } 
              
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
                                <span class="menu">Productivity -&gt;</span><span class="sub_menu">1A Breakup Details</span></td>
                        </tr>
                        <tr>
                            <td class="heading center">
                                1A Breakup</td>
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
                                                                        TabIndex="3" Width="601px" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                                <td class="left" style="width: 2%">
                                                                </td>
                                                                <td class="left" style="width: 10%">
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="24"
                                                                        AccessKey="A" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 3%">
                                                                </td>
                                                                <td class="textbold" style="width: 15%">
                                                                    Address</td>
                                                                <td colspan="4" style="width: 70%">
                                                                    <asp:TextBox ID="txtAdd" runat="server" CssClass="textboxgrey" Height="41px" MaxLength="50"
                                                                        ReadOnly="True" TabIndex="3" TextMode="MultiLine" Width="601px"></asp:TextBox></td>
                                                                <td class="left" style="width: 2%">
                                                                </td>
                                                                <td style="width: 10%;" class="left" valign="top">
                                                                    <asp:Button ID="btnExport" CssClass="button" runat="server" Text="Export" TabIndex="24"
                                                                        AccessKey="E" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 3%;">
                                                                </td>
                                                                <td class="textbold" style="width: 15%;">
                                                                    City</td>
                                                                <td style="width: 29%;">
                                                                    <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                                        TabIndex="3" Width="241px"></asp:TextBox></td>
                                                                <td class="textbold" style="width: 2%;">
                                                                </td>
                                                                <td class="textbold" style="width: 10%;">
                                                                    Country</td>
                                                                <td class="textbold" style="width: 29%;">
                                                                    <asp:TextBox ID="txtCountry" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                        ReadOnly="True" TabIndex="3" Width="241px"></asp:TextBox></td>
                                                                <td class="left" style="width: 2%">
                                                                </td>
                                                                <td class="center" style="width: 10%;">
                                                                    <asp:Button ID="btnPrint" runat="server" CssClass="button" TabIndex="24" Text="Print"
                                                                        Visible="False" AccessKey="P" /></td>
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
                                                                                <asp:DropDownList ID="drpMonthFrom" runat="server" CssClass="dropdownlist" TabIndex="20"
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
                                                                                <asp:DropDownList ID="drpYearFrom" TabIndex="21" runat="server" CssClass="dropdownlist"
                                                                                    Width="82px">
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
                                                                                <asp:DropDownList ID="drpMonthTo" runat="server" CssClass="dropdownlist" TabIndex="20"
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
                                                                                <asp:DropDownList ID="drpYearTo" TabIndex="21" runat="server" CssClass="dropdownlist"
                                                                                    Width="82px">
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
                                                                    <asp:CheckBox ID="ChkWholeGroup" runat="server" CssClass="textbox" TabIndex="17"
                                                                        Text="Show Productivity for whole group" Width="243px" Height="25px" /></td>
                                                                <td class="textbold" style="width: 2%;">
                                                                </td>
                                                                <td class="textbold" style="width: 10%;">
                                                                </td>
                                                                <td class="textbold" style="width: 29%;">
                                                                    <asp:CheckBox ID="ChkShowNBS" runat="server" CssClass="textbox" TabIndex="17" Text="NBS"
                                                                        Width="243px" Height="25px" /></td>
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
                    <table width="850px" border="0" align="left" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="redborder" colspan="2" valign="top">
                                <span class="lightblue">Air Bookings Breakup </span>
                                <asp:GridView EnableViewState="false" ID="grdvAirBreakup" runat="server" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                    Caption="" ShowFooter="true" AlternatingRowStyle-CssClass="lightblue" BackColor="White"
                                    Font-Bold="False" ForeColor="Black">
                                    <Columns>
                                        <asp:BoundField DataField="AGENCYNAME" HeaderText="Agency Name" HeaderStyle-Wrap="false" />
                                        <asp:BoundField DataField="ADDRESS" HeaderText="Address" HeaderStyle-Wrap="false" />
                                        <asp:BoundField DataField="CITY" HeaderText="City" HeaderStyle-Wrap="false" />
                                        <asp:BoundField DataField="MONTHYEAR" HeaderText="Month'Year" HeaderStyle-Wrap="false" />
                                        <asp:BoundField DataField="OFFICEID" HeaderText="Office Id" HeaderStyle-Wrap="false" />
                                        <asp:TemplateField HeaderText="Additions">
                                            <ItemTemplate>
                                                <%#Eval("ADDITIONS")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="TotAdd" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="80px" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cancel">
                                            <ItemTemplate>
                                                <%#Eval("CANCEL")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="TotCan" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="80px" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Auto Credit">
                                            <ItemTemplate>
                                                <%#Eval("AUTOCREDIT")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="TotCred" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="80px" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NBS Additions">
                                            <ItemTemplate>
                                                <%#Eval("NBSADDITIONS")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="TotNBSAddtion" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="80px" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NBS Cancel">
                                            <ItemTemplate>
                                                <%#Eval("NBSCANCEL")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="TotNBSCancellation" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="80px" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NBSAUTOCREDIT" HeaderText="NBS Auto Credit" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign ="Right" FooterStyle-HorizontalAlign ="Right"  />
                                        <asp:TemplateField HeaderText="Net Bookings">
                                            <ItemTemplate>
                                                <%#Eval("NETBOOKINGS")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="TotNet" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="80px" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="CE">
                                            <ItemTemplate>
                                                <%#Eval("CE")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LblCE" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="80px" Wrap="False" />
                                            <HeaderStyle Wrap="False"  HorizontalAlign ="center"  />
                                            <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle CssClass="lightblue" />
                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" />
                                    <RowStyle CssClass="textbold" />
                                    <FooterStyle CssClass="Gridheading" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px;">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <span class="lightblue">Car Bookings Breakup </span>
                                <asp:GridView EnableViewState="false" ID="grdvCarBreakup" runat="server" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" Caption="" Width="100%" HeaderStyle-CssClass="Gridheading"
                                    RowStyle-CssClass="ItemColor" ShowFooter="true" HeaderStyle-Wrap="false" AlternatingRowStyle-CssClass="lightblue">
                                    <Columns>
                                        <asp:BoundField DataField="AGENCYNAME" HeaderText="Agency Name" HeaderStyle-Wrap="false" />
                                        <asp:BoundField DataField="ADDRESS" HeaderText="Address" HeaderStyle-Wrap="false" />
                                        <asp:BoundField DataField="CITY" HeaderText="City" HeaderStyle-Wrap="false" />
                                        <asp:BoundField DataField="MONTHYEAR" HeaderText="Month'Year" HeaderStyle-Wrap="false" />
                                        <asp:BoundField DataField="OFFICEID" HeaderText="Office Id" HeaderStyle-Wrap="false" />
                                        <asp:TemplateField HeaderText="Net Bookings">
                                            <ItemTemplate>
                                                <%#Eval("NETBOOKINGS")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="TotNet" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="80px" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle CssClass="lightblue" />
                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" />
                                    <RowStyle CssClass="textbold" />
                                    <FooterStyle CssClass="Gridheading" />
                                </asp:GridView>
                            </td>
                            <td valign="top">
                                <span class="lightblue">Hotel Bookings Breakup</span><asp:GridView EnableViewState="false"
                                    ID="grdvHotelBreakup" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                    Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                    Caption="" ShowFooter="true" AlternatingRowStyle-CssClass="lightblue">
                                    <Columns>
                                        <asp:BoundField DataField="AGENCYNAME" HeaderText="Agency Name" HeaderStyle-Wrap="false" />
                                        <asp:BoundField DataField="ADDRESS" HeaderText="Address" HeaderStyle-Wrap="false" />
                                        <asp:BoundField DataField="CITY" HeaderText="City" HeaderStyle-Wrap="false" />
                                        <asp:BoundField DataField="MONTHYEAR" HeaderText="Month'Year" HeaderStyle-Wrap="false" />
                                        <asp:BoundField DataField="OFFICEID" HeaderText="Office Id" HeaderStyle-Wrap="false" />
                                        <asp:TemplateField HeaderText="Net Bookings">
                                            <ItemTemplate>
                                                <%#Eval("NETBOOKINGS")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="TotNet" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="80px" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle CssClass="lightblue" />
                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" />
                                    <RowStyle CssClass="textbold" />
                                    <FooterStyle CssClass="Gridheading" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
