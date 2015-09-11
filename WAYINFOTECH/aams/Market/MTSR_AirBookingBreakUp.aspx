<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MTSR_AirBookingBreakUp.aspx.vb" Inherits="Market_MTSR_AirBookingBreakUp" %>

 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Market</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>

    <script type="text/javascript" language="javascript">
   function PopupAgencyPage()
 {
    var type;
    type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1");	
    return false;
 }
 function CheckValidation()
     {
         if(parseInt(document.getElementById("drpYearFrom").value) >parseInt(document.getElementById("drpYearTo").value))
                    {     
                                                  
                    document.getElementById("lblError").innerHTML='Date range is not valid.';          
                    document.getElementById("drpYearFrom").focus();
                    return false;
                    } 
                               
      }
      
       
   

    </script>

</head>
<body>
    <form id="form1" defaultbutton="btnSearch" defaultfocus="drpCountry" runat="server">
        <table width="860px" align="left" class="border_rightred" id="TABLE1" language="javascript">
            <tr>
                <td valign="top" style="width: 850px;">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Market-&gt;</span><span class="sub_menu"></span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" style="height: 20px">
                                Air Bookings BreakUp</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 851px;" class="redborder" valign="top">
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="textbold" colspan="6" align="center" style="height: 15px">
                                                                    &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" width="6%" style="height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 116px; height: 25px;">
                                                                    Country</td>
                                                                <td style="width: 295px; height: 25px;">
                                                                    <asp:DropDownList ID="drpCountry" TabIndex="1" runat="server" CssClass="dropdownlist"
                                                                        Width="184px">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 132px; height: 25px;">
                                                                    City</td>
                                                                <td style="width: 306px; height: 25px;">
                                                                    <asp:DropDownList ID="drpCity" runat="server" TabIndex="2" CssClass="dropdownlist"
                                                                        Width="184px">
                                                                    </asp:DropDownList></td>
                                                                <td style="width: 176px; height: 25px;">
                                                                    <asp:Button ID="btnSearch" TabIndex="16" CssClass="button" runat="server" Text="Search"  AccessKey="S"/></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 116px; height: 25px">
                                                                    Agency</td>
                                                                <td colspan="3" style="height: 25px">
                                                                    <asp:TextBox ID="txtAgencyName" TabIndex="3" runat="server" CssClass="textbox" MaxLength="40"
                                                                        Style="position: relative" Width="86%"></asp:TextBox>
                                                                    <img alt="Select & Add Agency Name" tabindex="4" onclick="javascript:return PopupAgencyPage();"
                                                                        src="../Images/lookup.gif" style="position: relative" /></td>
                                                                <td style="height: 25px">
                                                                    <asp:Button ID="btnReset" runat="server" CssClass="button" Style="position: relative"
                                                                        TabIndex="18" Text="Reset"  AccessKey="R"/></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 116px; height: 25px">
                                                                    AirLine</td>
                                                                <td style="width: 295px; height: 25px">
                                                                    <asp:DropDownList ID="drpAirline" runat="server" CssClass="dropdownlist" Style="position: relative"
                                                                        TabIndex="1" Width="184px">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 132px; height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 306px; height: 25px">
                                                                    <asp:CheckBox TabIndex="5" Text="Show BreakUp Of Bookings" ID="chkGroup" runat="server" Style="position: relative"
                                                                        Width="200px" /></td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 116px; height: 25px">
                                                                    From&nbsp;
                                                                </td>
                                                                <td style="width: 295px; height: 25px">
                                                                    <asp:DropDownList ID="drpMonthFrom" TabIndex="11" runat="server" CssClass="dropdownlist"
                                                                        Width="104px" Style="position: relative">
                                                                        <asp:ListItem Value="1">January</asp:ListItem>
                                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                                        <asp:ListItem Value="9">september</asp:ListItem>
                                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                                    </asp:DropDownList>&nbsp;
                                                                    <asp:DropDownList TabIndex="12" ID="drpYearFrom" runat="server" CssClass="dropdownlist"
                                                                        Width="64px" Style="position: relative; left: 8px; top: 0px;">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 132px; height: 25px">
                                                                    To</td>
                                                                <td style="height: 25px; width: 306px;">
                                                                    <asp:DropDownList ID="drpMonthTo" runat="server" TabIndex="13" CssClass="dropdownlist"
                                                                        Width="104px" Style="position: relative">
                                                                        <asp:ListItem Value="1">January</asp:ListItem>
                                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                                        <asp:ListItem Value="9">september</asp:ListItem>
                                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                                    </asp:DropDownList>&nbsp;
                                                                    <asp:DropDownList ID="drpYearTo" runat="server" TabIndex="14" CssClass="dropdownlist"
                                                                        Width="72px" Style="position: relative; left: 0px; top: 0px;">
                                                                    </asp:DropDownList></td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px">
                                                                </td>
                                                                <td class="textbold" style="height: 25px; width: 116px;">
                                                                </td>
                                                                <td style="height: 25px; width: 295px;">
                                                                </td>
                                                                <td class="textbold" style="height: 25px; width: 132px;">
                                                                </td>
                                                                <td style="height: 25px; width: 306px;">
                                                                </td>
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
            <tr>
                <td valign="top" class="top border_rightred">
                    <table width="1200px" border="0" align="left" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="redborder" style="height: 194px">
                                <asp:GridView TabIndex="20" ID="grdTravelAssistance" runat="server" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                    AlternatingRowStyle-CssClass="lightblue">
                                    <Columns>
                                        <asp:BoundField DataField="Name" HeaderText="Name">
                                             
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Address" HeaderText="Address">
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="1A" HeaderText="1A">
                                             
                                        </asp:BoundField>
                                        <asp:BoundField DataField="1B" HeaderText="1B">
                                             
                                        </asp:BoundField>
                                        <asp:BoundField DataField="1G" HeaderText="1G">
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="1P" HeaderText="1P">
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="1W" HeaderText="1W">
                                           
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Total" HeaderText="Total">
                                            
                                        </asp:BoundField>
                                         <asp:TemplateField>
                                            <ItemTemplate>
                                         <asp:HiddenField ID="hdAdd" runat="server" Value='<%# Eval("ADDRESS") %>' />
                                <asp:HiddenField ID="hdCountry" runat="server" Value='<%# Eval("COUNTRY") %>' />
                                <asp:HiddenField ID="hdLcode" runat="server" Value='<%#Eval("LCODE")%>' />
                                </ItemTemplate>
                                 </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle CssClass="lightblue" />
                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" />
                                    <RowStyle CssClass="textbold" />
                                </asp:GridView>
                                
                         
                        
                          <asp:GridView TabIndex="20" ID="GridView1" runat="server" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                    AlternatingRowStyle-CssClass="lightblue">
                                    <Columns>
                                        <asp:BoundField DataField="Name" HeaderText="Name">
                                             
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Address" HeaderText="Address">
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CRS Code" HeaderText="CRS Code">
                                             
                                        </asp:BoundField>
                                        <asp:BoundField DataField="1B" HeaderText="Booking Active">
                                             
                                        </asp:BoundField>
                                        <asp:BoundField DataField="1G" HeaderText="Cancel Active">
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="1P" HeaderText="Booking Passive">
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="1W" HeaderText="Cancel Passive">
                                           
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Total" HeaderText="Late">
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Total" HeaderText="Null Active">
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Total" HeaderText="Null Passive">
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Total" HeaderText="Net Bookings">
                                            
                                        </asp:BoundField>
                                         <asp:TemplateField>
                                            <ItemTemplate>
                                         <asp:HiddenField ID="hdAdd" runat="server" Value='<%# Eval("ADDRESS") %>' />
                                <asp:HiddenField ID="hdCountry" runat="server" Value='<%# Eval("COUNTRY") %>' />
                                <asp:HiddenField ID="hdLcode" runat="server" Value='<%#Eval("LCODE")%>' />
                                </ItemTemplate>
                                 </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle CssClass="lightblue" />
                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" />
                                    <RowStyle CssClass="textbold" />
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
