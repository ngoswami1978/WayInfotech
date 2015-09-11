<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PUSR_OrderFeasibility.aspx.vb"
    Inherits="TravelAgency_TASR_OrderFeasibility" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::TravelAgency::Order Feasibility</title>
 <base target="_self" />
    <script src="../java-script/AAMS.js" type="text/java-script"></script>

    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script type="text/java-script" src="../Calender/calendar.js"></script>

    <!-- import the language module -->

    <script type="text/java-script" src="../Calender/lang/calendar-en.js"></script>

    <!-- import the calendar setup module -->

    <script type="text/java-script" src="../Calender/calendar-setup.js"></script>

    <script language="java-script" type="text/java-script"> 
    
    </script>

</head>
<body oncontextmenu="return false">
    <form id="form1" runat="server" >
        <table width="860px" align="left" class="border_rightred">
            <tr>
                <td valign="top" style="width: 860px;">
                    <table width="100%" class="left">
                        <tr>
                            <td>
                                <span class="menu">Travel Agency -&gt;</span><span class="sub_menu">Order
                Feasibility Report<span style="font-size: 12pt; font-family: Times New Roman"> &nbsp; &nbsp; &nbsp;
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</span><a class="LinkButtons"
                                            href="#" onclick="window.close();">Close</a></span></td>
                        </tr>
                        <tr>
                            <td class="heading center">
                                Order Feasibility Report</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 845px;" class="redborder" valign="top">
                                                        <table border="0" cellpadding="2" cellspacing="1" style="width: 845px;" class="left">
                                                            <tr>
                                                                <td class="center" colspan="8">
                                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 12px">
                                                                </td>
                                                                <td class="subheading" colspan="5">
                                                                    Agency Details</td>
                                                                <td style="width: 12px">
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 12px">
                                                                    <input id="Hidden1" runat="server" style="width: 1px" type="hidden" /></td>
                                                                <td class="textbold">
                                                                    Agency Name</td>
                                                                <td colspan="4">
                                                                    <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                        TabIndex="1" Width="489px" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                                <td class="left" style="width: 12px">
                                                                </td>
                                                                <td class="left">
                                                                    <asp:Button ID="btnPrint" runat="server" CssClass="button" TabIndex="17" Text="Print"
                                                                         AccessKey="P" style="position: relative; left: 0px;" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 12px">
                                                                </td>
                                                                <td class="textbold">
                                                                    Address</td>
                                                                <td colspan="4">
                                                                    <asp:TextBox ID="txtAdd" runat="server" CssClass="textboxgrey" Height="41px" MaxLength="50"
                                                                        ReadOnly="True" TabIndex="2" TextMode="MultiLine" Width="490px"></asp:TextBox></td>
                                                                <td class="left" style="width: 12px">
                                                                </td>
                                                                <td class="left" valign="top">
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 12px;">
                                                                </td>
                                                                <td class="textbold" style="width: 120px;">
                                                                    City</td>
                                                                <td style="width: 200px;">
                                                                    <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                                        TabIndex="3" Width="200px"></asp:TextBox></td>
                                                                <td class="textbold" style="width: 50px;">
                                                                </td>
                                                                <td class="textbold" style="width: 100px;">
                                                                    Country</td>
                                                                <td class="textbold" style="width: 200px;">
                                                                    <asp:TextBox ID="txtCountry" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                        ReadOnly="True" TabIndex="4" Width="139px"></asp:TextBox></td>
                                                                <td class="left" style="width: 12px">
                                                                </td>
                                                                <td class="center" style="width: 150px;">
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 12px">
                                                                </td>
                                                                <td class="textbold">
                                                                    IATA</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtIATA" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                                        TabIndex="5" Width="200px"></asp:TextBox></td>
                                                                <td class="textbold">
                                                                </td>
                                                                <td class="textbold" style="width: 10%">
                                                                    Online Status</td>
                                                                <td class="textbold">
                                                                    <asp:TextBox ID="txtOnlineStatus" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                                        TabIndex="6" Width="138px"></asp:TextBox></td>
                                                                <td class="left" style="width: 12px">
                                                                </td>
                                                                <td class="center">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 12px">
                                                                </td>
                                                                <td colspan="7">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 12px">
                                                                </td>
                                                                <td colspan="7">
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
                <td valign="top" style="height: 302px" >
                    <table width="850px" border="0" align="left" cellpadding="0" cellspacing="0">
                        <tr>
                            <td  valign="top" style="width: 430px ">
                                <span class="lightblue"></span><span style="font-size: 12px; font-family: Arial, Verdana;
                                    background-color: #dde7f2">1A Actual for Last 12 Months</span>
                              
                <asp:Panel ID="pnl1Aactual" runat ="server"  ScrollBars="Vertical"  Width="95%" Height ="100px">

                                    <asp:GridView EnableViewState="false"
                                    ID="grdv1Aactual" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                    Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                    Caption=""  AlternatingRowStyle-CssClass="lightblue" BackColor="White"
                                    Font-Bold="False" TabIndex="7" ForeColor="Black">
                                    <Columns>
                                        <asp:BoundField DataField="MONTH" HeaderText="Month" >
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PRODUCTIVITY" HeaderText="Productivity" >
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                    </Columns>
                                    <AlternatingRowStyle CssClass="lightblue" />
                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" />
                                    <RowStyle CssClass="textbold" />
                                    <FooterStyle CssClass="Gridheading" />
                                </asp:GridView>
                            </asp:Panel>    
                            <span class="lightblue">Other CRS average of last 12 months&nbsp;</span>
                       <asp:Panel ID="pnlCRS" runat ="server"  ScrollBars="Vertical"  Width="95%" Height ="100px">
                            <asp:GridView
                                EnableViewState="false" ID="grdvCRS" runat="server" AutoGenerateColumns="False"
                                HorizontalAlign="Center" Caption="" Width="100%" HeaderStyle-CssClass="Gridheading"
                                RowStyle-CssClass="ItemColor"   TabIndex="8" HeaderStyle-Wrap="false"  AlternatingRowStyle-CssClass="lightblue">
                                <Columns>
                                    <asp:BoundField DataField="CRS" HeaderText="CRS" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="AVERAGE" HeaderText="Average" HeaderStyle-Wrap="false" />
                                </Columns>
                                <AlternatingRowStyle CssClass="lightblue" />
                                <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" />
                                <RowStyle CssClass="textbold" />
                               
                            </asp:GridView>
                          </asp:Panel>
                         
                            </td>
                            
                            <td valign="top">
                            
                                <span class="lightblue">Hardware Details</span>
                                <table class="redborder">
                                    <tr>
                                        <td class="textbold" style="width: 389px; height: 28px;">
                                            1a PC</td>
                                        <td style="width: 86px; height: 28px;">
                                            <asp:TextBox ID="txt1aPC" runat="server"  TabIndex="9" CssClass="textboxgrey" ReadOnly="True"  Width="100px"></asp:TextBox></td>
                                            <td class="textbold" style="width: 277px; height: 28px;">
                                            Agency PC</td>
                                        <td style="width: 146px; height: 28px;">
                                            <asp:TextBox TabIndex="10" ID="txtAgencyPC" Width="100px" CssClass="textboxgrey" ReadOnly="True" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textbold" style="width: 389px; height: 26px;">
                                            HC Printers</td>
                                        <td style="width: 86px; height: 26px;" >
                                            <asp:TextBox ID="txtHCPrinter" runat="server" CssClass="textboxgrey" ReadOnly="True"  TabIndex="11" Width="100px"></asp:TextBox></td>
                                            <td class="textbold" style="width: 277px; height: 26px;">
                                                Ticket Printers</td>
                                        <td style="width: 146px; height: 26px;">
                                            <asp:TextBox ID="txtPrinters" TabIndex="12" Width="100px" CssClass="textboxgrey" ReadOnly="True" runat="server"></asp:TextBox>
                                        </td>
                                  </tr>
                                  <tr>
                                 </tr> 
                                  
                    </table>
                 
                      <span class="lightblue">Summary</span>
                    <table class="redborder">
                                  
                                    <tr>
                                        <td class="textbold" style="width: 1028px; height: 22px;">
                                            1a Average for last 12 months</td>
                                        <td style="width: 240px; height: 22px;" >
                                            <asp:TextBox TabIndex="13" ID="txt1aAvgLast12months" CssClass="textboxgrey" ReadOnly="True" runat="server" Width="200px"></asp:TextBox></td>
                                        
                                        
                                    </tr>
                                    <tr>
                                        <td class="textbold" style="width: 1028px; height: 30px;">
                                            1a Average for the period before the last 12 months</td>
                                        <td style="width: 240px; height: 30px;" >
                                            <asp:TextBox ID="txt1aAveragebefore12Months" CssClass="textboxgrey" ReadOnly="True" TabIndex="14" runat="server" Width="200px"></asp:TextBox></td>
                                            
                                    </tr>
                                    <tr>
                                        <td class="textbold" style="width: 1028px; height: 22px;">
                                            Total Potential</td>
                                        <td style="width: 240px; height: 22px;" >
                                            <asp:TextBox ID="txtTotalPotential" CssClass="textboxgrey" ReadOnly="True" runat="server"  TabIndex="15" Width="200px"></asp:TextBox></td>
                                            
                                    </tr>
                                    <tr>
                                        <td class="textbold" style="width: 1028px; height: 22px;">
                                            Total Requirement</td>
                                        <td style="width: 240px; height: 22px;" >
                                            <asp:TextBox ID="txtTotalReq" CssClass="textboxgrey" ReadOnly="True" runat="server" Width="200px" TabIndex="16"></asp:TextBox></td>
                                          
                                    </tr>
                                  
                    </table> 
                    </td>
                    <tr>
                    </tr>
                    <tr>
                        <td colspan="2" >
                        </td>
                    </tr>
                    <tr>
                       <%-- <td valign="top">
                            <span class="lightblue">Other CRS average of last 12 months&nbsp;</span><asp:GridView
                                EnableViewState="false" ID="grdvCRS" runat="server" AutoGenerateColumns="False"
                                HorizontalAlign="Center" Caption="" Width="100%" HeaderStyle-CssClass="Gridheading"
                                RowStyle-CssClass="ItemColor"   HeaderStyle-Wrap="false" AlternatingRowStyle-CssClass="lightblue">
                                <Columns>
                                    <asp:BoundField DataField="CRS" HeaderText="CRS" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="AVERAGE" HeaderText="Average" HeaderStyle-Wrap="false" />
                                </Columns>
                                <AlternatingRowStyle CssClass="lightblue" />
                                <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" />
                                <RowStyle CssClass="textbold" />
                               
                            </asp:GridView>
                        </td>--%>
                     <%-- <td valign="top">
                            <span class="lightblue">Summary</span>
                            <table>
                                    <tr>
                                        <td class="textbold" style="width: 1028px; height: 26px;">
                                            1a Average for last 12 months</td>
                                        <td style="width: 240px; height: 26px;" >
                                            <asp:TextBox ID="TextBox4" runat="server" Width="199px"></asp:TextBox></td>
                                        
                                        
                                    </tr>
                                    <tr>
                                        <td class="textbold" style="width: 1028px">
                                            1a Average for the period before the last 12 months</td>
                                        <td style="width: 240px" >
                                            <asp:TextBox ID="TextBox6" runat="server" Width="197px"></asp:TextBox></td>
                                            
                                    </tr>
                                    <tr>
                                        <td class="textbold" style="width: 1028px">
                                            Total Potential</td>
                                        <td style="width: 240px" >
                                            <asp:TextBox ID="TextBox8" runat="server" Width="196px"></asp:TextBox></td>
                                            
                                    </tr>
                                    <tr>
                                        <td class="textbold" style="width: 1028px">
                                            Total Requirement</td>
                                        <td style="width: 240px" >
                                            <asp:TextBox ID="TextBox10" runat="server" Width="198px"></asp:TextBox></td>
                                          
                                    </tr>
                    </table>
                        </td> --%>
                    </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>



