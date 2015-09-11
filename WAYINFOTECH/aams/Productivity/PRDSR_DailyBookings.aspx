<%@ Page Language="VB"  EnableEventValidation="false"   AutoEventWireup="false" CodeFile="PRDSR_DailyBookings.aspx.vb" Inherits="Productivity_PRDSR_DailyBookings" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
   <title>AAMS: Productivity</title>   
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>
    <script type="text/javascript" language="javascript" >
    
   
    function buttonClickDAILYB()
    {
    document.getElementById("hdButtonClick").value="1";
    }
    
    
       function ActDeAct()
     {
        {debugger;}
        IE4 = (document.all);
        NS4 = (document.layers);
        var whichASC;
        whichASC = (NS4) ? e.which : event.keyCode;
        if(whichASC!=9 &&  whichASC !=13 && whichASC!=18)
        {
        document.getElementById("hdAgencyName").value="";
        document.getElementById("chkGroupProductivity").disabled=true;
        document.getElementById("chkGroupProductivity").checked=false;
        }
    	
     }

</script>
</head>

<body  onload="return GroupProductivityChkDAILYB();" >
    <form id="form1"  defaultbutton="btnDisplay"  defaultfocus="txtAgencyName" runat="server">
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
                                Search Daily Bookings
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" >
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                        <td style="width: 860px;" class="redborder" valign="top" >
                                                          <table width="100%" border="0"   align="left" cellpadding="0" cellspacing="0" id="TABLE1" onclick="return TABLE1_onclick()">
                                                          
                        
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" style="height: 15px">
                                                        &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                   
                                                    </td>
                                                </tr>
                                                
                                                              <tr>
                                                                  <td  height="25" width="6%">
                                                                  </td>
                                                                  <td class="textbold" style="width: 288px">
                                                        Country</td>
                                                                  <td style="width: 257px" ><asp:DropDownList ID="drpCountrys" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="1">
                                                                  </asp:DropDownList></td>
                                                                  <td  class="textbold">
                                                        City</td>
                                                                  <td ><asp:DropDownList ID="drpCitys" runat="server" CssClass="dropdownlist" Width="144px" TabIndex="1">
                                                                  </asp:DropDownList></td>
                                                                  <td style="width: 176px" align="center">
                                                        <asp:Button ID="btnDisplay" CssClass="button" runat="server" Text="Search" TabIndex="3"  AccessKey="A"/></td>
                                                              </tr>
                                                <tr>
                                                    <td width="6%" style="height: 25px">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="width: 288px; height: 25px;">
                                                        Agency Name
                                                        </td>
                                                    <td colspan="3" style="height: 25px">
                                                        <asp:TextBox ID="txtAgencyName" CssClass="textbox" MaxLength="40" runat="server" Width="93%" TabIndex="1"></asp:TextBox> 
                                                        <img src="../Images/lookup.gif" alt="" onclick="javascript:return PopupAgencyPageDAILYB();" tabindex="1" style="cursor:pointer;"  /></td>
                                                    <td style="width: 176px; height: 25px;" align="center">
                                                        <asp:Button ID="btnExport" CssClass="button" runat="server" Text="Export" TabIndex="3" AccessKey="E" /></td>
                                                </tr>
                                                <tr>
                                                    <td width="6%" style="height: 25px">
                                                    
                                                    </td>
                                                    <td class="textbold" style="height: 25px; width: 288px;">
                                                        <asp:CheckBox ID="chkGroupProductivity" runat="server" CssClass="textbold" Text="Group Productivity" TextAlign="Left" Width="144px" TabIndex="1"   /></td>
                                                    <td style="height: 25px; width: 257px;">
                                                        <asp:CheckBox ID="chkOriginalBk" runat="server" CssClass="textbold" Text="NBS    " TextAlign="Left" Width="152px" Checked="false" TabIndex="1"   /></td>
                                                    <td width="18%" class="textbold" style="height: 25px">
                                                                      </td>
                                                    <td width="20%" style="height: 25px"></td>
                                                    <td style="height: 25px; width: 176px;" align="center">
                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" style="position: relative" TabIndex="3" AccessKey="R" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 25px">
                                                    </td>
                                                    <td class="textbold" style="height: 25px; width: 288px;" >
                                                        Agency Status</td>
                                                    <td style="height: 25px; width: 257px;"><asp:DropDownList ID="drpAgencyStatus" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="1">
                                                    </asp:DropDownList></td>
                                                    <td  class="textbold" style="height: 25px">
                                                        Responsible Staff</td>
                                                    <td style="height: 25px">
                                                        <asp:DropDownList ID="drpResponsibleStaff" runat="server" CssClass="dropdownlist" Width="144px" TabIndex="1">
                                                    </asp:DropDownList></td>
                                                    <td style="width: 176px; height: 25px;" align="center"><asp:Button ID="Button1" CssClass="button" runat="server" Text="Export New" Visible="False" TabIndex="3" /></td>
                                                </tr>
                                                              <tr>
                                                                  <td style="height: 25px" width="6%">
                                                                  </td>
                                                                  <td class="textbold" style="width: 288px; height: 25px">
                                                                      Lcode</td>
                                                                  <td style="width: 257px; height: 25px">
                                                                      <asp:TextBox ID="txtLcode" runat="server" CssClass="textbox" MaxLength="9" TabIndex="1"></asp:TextBox></td>
                                                                  <td class="textbold" style="height: 25px" width="18%">
                                                                      Chain Code</td>
                                                                  <td style="height: 25px" valign="middle">
                                                                      <asp:TextBox ID="txtChainCode" runat="server" CssClass="textbox" MaxLength="9" TabIndex="1" Width="136px"></asp:TextBox></td>
                                                                  <td style="height: 25px" width="18%">
                                                                  </td>
                                                              </tr>
                                                              <tr>
                                                                  <td style="height: 25px" width="6%">
                                                                  </td>
                                                                  <td class="textbold" style="width: 288px; height: 25px">
                                                                      Agency Group Category</td>
                                                                  <td style="width: 257px; height: 25px">
                                                        <asp:DropDownList ID="drpAgencyType" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="1">
                                                        </asp:DropDownList></td>
                                                                  <td class="textbold" style="height: 25px" width="18%">
                                                                      Agency Category</td>
                                                                  <td style="height: 25px" valign="middle">
                                                                      <asp:DropDownList ID="drpGroupAgencyType" runat="server" CssClass="dropdownlist" Width="147px" TabIndex="1">
                                                                      </asp:DropDownList></td>
                                                                  <td style="height: 25px" width="18%">
                                                                  </td>
                                                              </tr>
                                                 <tr>
                                                                    <td width="6%" style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold" style="width: 288px; height: 25px">
                                                                        1a Office</td>
                                                                    <td style="height: 25px; width: 257px;">
                                                                        <asp:DropDownList ID="drpOneAOffice" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="1">
                                                                                                  </asp:DropDownList></td>
                                                                    <td  class="textbold" width="18%" style="height: 25px">
                                                                      Group Type</td>
                                                                    <td style="height: 25px" valign="middle" >
                                                                    <table cellpadding="0" cellspacing="0" width="145px"  height="20px">
                                                                    <tr>
                                                                    <td  valign="middle"><asp:DropDownList ID="drpLstGroupType" runat="server" CssClass="dropdownlist" Width="144px" TabIndex="1">
                                                                    </asp:DropDownList></td>
                                                                    </tr>
                                                                    </table>
                                                                    
                                                                        
                                                                   </td>
                                                                    <td width="18%" style="height: 25px">
                                                                    </td>
                                                                </tr>
                                                    
                                                    
                                                                  <tr>
                                                                    <td style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold" style="width: 288px">
                                                        Region</td>
                                                                    <td style="width: 257px">
                                                                         

                                                                        <asp:DropDownList ID="drpRegion" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="1">
                                                                        </asp:DropDownList></td>
                                                                        
                                                                    <td>
                                                                   </td>
                                                                    <td rowspan="4" valign="bottom">  <span class="subheading"> Online Status </span> &nbsp; &nbsp;
                                                                    <div id="dvStatus"  runat="server" enableviewstate="true"    style="overflow:auto; height:95px;width:144px;  border:solid 1px silver;">
                                                                            <asp:CheckBoxList CssClass="textbox" EnableViewState="true"   ID="chkOnlineStatus"  runat="server" Height="104px" Width="120px" CellPadding="1" CellSpacing="1" TabIndex="2">
                                                                            </asp:CheckBoxList></div>
                                                                    </td>
                                                                </tr>
                                                                
                                                    
                                                    
                                                     <tr>
                                                                    <td style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold" style="width: 288px; height: 25px;" >
                                                                        Productivity</td>
                                                                    <td style="height: 25px; width: 257px;" >
                                                                        <asp:DropDownList ID="drpProductivity" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="1">
                                                                            <asp:ListItem>--All--</asp:ListItem>
                                                                            <asp:ListItem Value="1">Equal To</asp:ListItem>
                                                                            <asp:ListItem Value="2">Greater Than</asp:ListItem>
                                                                            <asp:ListItem Value="3">Greater Than Equal To</asp:ListItem>
                                                                            <asp:ListItem Value="4">Less Than</asp:ListItem>
                                                                            <asp:ListItem Value="5">Less Than Equal To</asp:ListItem>
                                                                            <asp:ListItem Value="6">Not Equal To</asp:ListItem>
                                                                            <asp:ListItem Value="7">Between</asp:ListItem>
                                                                                                  </asp:DropDownList></td>
                                                                    <td class="textbold" style="height: 25px">
                                                                        <asp:TextBox ID="txtProductivityFrm" runat="server" CssClass="textboxgrey" MaxLength="9" Width="64px" TabIndex="1" Enabled="False"></asp:TextBox>&nbsp;
                                                                        <asp:TextBox ID="txtProductivityTo" runat="server" CssClass="textboxgrey" MaxLength="9" Width="64px" TabIndex="1" Enabled="False"></asp:TextBox></td>
                                                                </tr>
                                                                
                                                                <tr>
                                                                    <td style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 25px; width: 288px;" >
                                                                        Performance</td>
                                                                    <td style="height: 25px; width: 257px;" >
                                                                    <asp:DropDownList ID="drpPerformence" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="1">
                                                                            <asp:ListItem>--All--</asp:ListItem>
                                                                            <asp:ListItem Value="1">Equal To</asp:ListItem>
                                                                            <asp:ListItem Value="2">Greater Than</asp:ListItem>
                                                                            <asp:ListItem Value="3">Greater Than Equal To</asp:ListItem>
                                                                            <asp:ListItem Value="4">Less Than</asp:ListItem>
                                                                            <asp:ListItem Value="5">Less Than Equal To</asp:ListItem>
                                                                            <asp:ListItem Value="6">Not Equal To</asp:ListItem>
                                                                            <asp:ListItem Value="7">Between</asp:ListItem>
                                                                    </asp:DropDownList>

                                                                                                 
                                                                        </td>
                                                                    <td  style="height: 25px">
                                                                        <asp:TextBox ID="txtPerformenceFrm" runat="server" CssClass="textboxgrey" MaxLength="9" Width="64px" TabIndex="1" Enabled="False"></asp:TextBox>&nbsp;
                                                                        <asp:TextBox ID="txtPerformenceTo" runat="server" CssClass="textboxgrey" MaxLength="9" Width="64px" TabIndex="1" Enabled="False"></asp:TextBox></td>
                                                                </tr>
                                                              
                                                                <tr>
                                                                    <td style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 25px; width: 288px;">
                                                                        Month &nbsp;&nbsp;
                                                                    </td>
                                                                    <td style="height: 25px; width: 257px;">
                                                                         <asp:DropDownList ID="drpMonth" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="1">
                                                                                                  </asp:DropDownList></td>
                                                                    <td class="textbold" style="height: 25px">
                                                                        Year &nbsp; &nbsp; &nbsp;
                                                                         <asp:DropDownList ID="drpYear" runat="server" CssClass="dropdownlist" Width="96px" TabIndex="1">
                                                                                                  </asp:DropDownList></td>
                                                                </tr>
                                                              <tr>
                                                                  <td style="height: 25px">
                                                                  </td>
                                                                  <td>
                                                                  <asp:CheckBox ID="chkShowAddress" runat="server" CssClass="textbold" Text="Show Address" Width="120px" TabIndex="1"   /></td>
                                                                  <td class="textbold" style="height: 25px">
                                                                      <asp:CheckBox ID="chkGroupClassification" runat="server" CssClass="textbold" TabIndex="1"
                                                                          Text="Show Agency Category" Width="148px" /></td>
                                                                  <td class="textbold" style="height: 25px">
                                                                  <asp:CheckBox ID="chkShowChaniCode" runat="server" CssClass="textbold" Text="Show ChainCode" Width="120px" TabIndex="1"   /></td>
                                                                  <td colspan="2" style="height: 25px">
                                                        </td>
                                                              </tr>
                                                              <tr>
                                                                  <td style="height: 25px">
                                                                  </td>
                                                                  <td class="textbold" style="width: 288px; height: 25px">
                                                                      Company Vertical</td>
                                                                  <td style="height: 25px; width: 257px;">
                                                                      <asp:DropDownList ID="DlstCompVertical" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                          TabIndex="1" Width="137px">
                                                                      </asp:DropDownList></td>
                                                                  <td class="textbold" style="height: 25px">
                                                                  <div id="dv1" runat="server" >
                                                                        Airline Name</div>
                                                                  </td>
                                                                  <td style="height: 25px" colspan ="2">
                                                                  <div id="dv2" runat="server" >
                                                                    <asp:DropDownList ID="drpAirLineName" runat="server" CssClass="dropdownlist" Width="232px" TabIndex="1">
                                                                    </asp:DropDownList>
                                                                    </div>
                                                                  </td>
                                                                
                                                              </tr>
                                                             
                                                                <tr>
                                                                    <td style="height: 25px">
                                                                    </td>
                                                                    <td class="subheading" colspan="4" style="height: 25px">
                                                                        Daily Bookings Details</td>
                                                                        <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 25px; width: 288px;"><asp:CheckBox ID="chkAir" runat="server" CssClass="textbold" Text="Air" Width="120px" Checked="True" TabIndex="1"   /></td>
                                                                    <td style="height: 25px; width: 257px;"><asp:CheckBox ID="chkCar" runat="server" CssClass="textbold" Text="Car" Width="120px" TabIndex="1"   /></td>
                                                                    <td class="textbold" style="height: 25px"><asp:CheckBox ID="chkHotel" runat="server" CssClass="textbold" Text="Hotel" Width="120px" TabIndex="1"   /></td>
                                                                    <td style="height: 25px"><asp:CheckBox ID="chkAirBreakUp" runat="server" CssClass="textbold" Text="Air Breakup" Width="120px" TabIndex="1"   /></td>
                                                                    <td style="height: 25px">
                                                                    </td>
                                                                </tr>
                                                              <tr height="20px">
                                                                  
                                                              </tr>
                                                             
                                               </table>
                                                        </td>
                                                </tr>
                                                           
                                        </table>
                                        
                                          
                                        </td>
                                    </tr>
                                </table>
                                                        <input type="hidden" id="hdAgencyNameId" runat="server" value="" style="width: 5px" />
                                                        <input type="hidden" id="hdAgency" runat="server" value="" style="width: 5px" />
                                                        <input type="hidden" id="hdAir" runat="server" value="" style="width: 5px" />
                                                        <input type="hidden" id="hdAirBr" runat="server" value="" style="width: 5px" />
                                                        <input type="hidden" id="hdCar" runat="server" value="" style="width: 5px" />
                                                        <input type="hidden" id="hdHotel" runat="server" value="" style="width: 5px" />
                                                        
                                                        <input type="hidden" id="hdAgencyName" runat="server" value="" style="width: 5px" />
                                                        
                                                         
                                                         <input type="hidden" id="hdChkGroupProductivity" runat="server" value="" style="width: 5px" />
                                                        <input type="hidden" id="hdButtonClick" runat="server" value="" style="width: 5px" />
                                                         
                                                         
                                                         </td>
                        </tr>
                    </table>
                </td>
                <td> </td>
            </tr>
           
            
            <!-- code for paging----->
                                          
                                          
                                          
                                          
                                          
                                           
                                                
            <!-- code for paging----->
                                                
            <tr>
            </tr>
        </table>
    </td>
    <td></td>
    
    </tr>
    <tr>
    <td colspan="2">
    <table width="100%" cellpadding="0">
         <tr align="left"  >
            <td valign="top" style="padding-left:4px;" colspan="2" >
            <%If chkAir.Checked = True And chkCar.Checked = True And chkHotel.Checked = True And chkAirBreakUp.Checked = False Then%>
                 <asp:Panel ID="PanelAll" runat="server" width="8560px" >
              <table  width="8500px" id="tlbgrdvDailyBookingsAll" runat="server"  border="0" align="left" cellpadding="0" cellspacing="0"> 
                                                                            <tr>  
                                                                                    <td class="redborder">
                                                                                   <asp:GridView AllowSorting="True" HeaderStyle-Wrap="false"  HeaderStyle-ForeColor="white"  ID="grdvDailyBookingsAll" FooterStyle-HorizontalAlign="right" ShowFooter="True"  runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="8560px" FooterStyle-CssClass="Gridheading"  HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" TabIndex="4">
                                                              <Columns>
                                                              
                                                              
                                                              
                                                         <asp:TemplateField SortExpression="LCODE" HeaderText="LCode" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblLcode" runat="server" Text='<%# Eval("LCODE") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>     
                                                           
                                                            <asp:TemplateField SortExpression ="AgencyName" HeaderText="Agency Name" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAgencyName" runat="server" Text='<%# Eval("AgencyName") %>'></asp:Label>
                                                         </ItemTemplate>
                                                                <HeaderStyle Width="150px" />
                                                         <ItemStyle Wrap="false" />
                                                                
                                                         </asp:TemplateField>    
                                                         
                                                            
                                                          <asp:TemplateField SortExpression="Chain_Code"  HeaderText="Chain Code">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblChainCode" runat="server" Text='<%# Eval("Chain_Code") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         <HeaderStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression="Group_Classification_Name" HeaderStyle-Width="50px"  HeaderText="Type">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblGroupCode" runat="server" Text='<%# Eval("Group_Classification_Name ") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         <HeaderStyle Wrap="true"  />
                                                         </asp:TemplateField>  
                                                         
                                                         
                                                          <asp:TemplateField  HeaderText="Employee Name" SortExpression="Employee_Name">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("Employee_Name") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <HeaderStyle Width="150px" />
                                                         <ItemStyle Wrap="false" />
                                                              
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField HeaderText="Address" SortExpression="ADDRESS">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("ADDRESS") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <HeaderStyle Width="330px" />
                                                         <ItemStyle Wrap="true" Width="350px" />
                                                              
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField  SortExpression ="ONLINE_STATUS" HeaderText="Online Status">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblOnlineStatus" runat="server" Text='<%# Eval("ONLINE_STATUS") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <HeaderStyle Width="60px" />
                                                         <ItemStyle Wrap="false" />
                                                         
                                                         </asp:TemplateField>    
                                                           <asp:BoundField  DataField="COMP_VERTICAL" HeaderText="Company Vertical " SortExpression ="COMP_VERTICAL"   >
                                                                         <ItemStyle Width="60px" Wrap="False" />
                                                                         <HeaderStyle Wrap="true" />
                                                                     </asp:BoundField>    
                                                         
                                                          <asp:TemplateField  SortExpression ="CITY" HeaderText="City">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblCity" runat="server" Text='<%# Eval("CITY") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField  SortExpression ="COUNTRY" HeaderText="Country">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("COUNTRY") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression="OFFICEID"  HeaderText="Office ID">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblOfficeID" runat="server" Text='<%# Eval("OFFICEID") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression="Aoffice"  HeaderText="Aoffice" FooterText="Total">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAoffice" runat="server" Text='<%# Eval("Aoffice") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <FooterStyle HorizontalAlign="Left" />
                                                         <ItemStyle Wrap="false" />
                                                              
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression="Airline_Name" HeaderText="Airline Name">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAirlineName" runat="server" Text='<%# Eval("Airline_Name") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="TARGET"  HeaderText="Target">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblTarget" runat="server" Text='<%# Eval("TARGET") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" />
                                                         <ItemStyle Wrap="false" />
                                                              
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="TARGETPERDAY" HeaderText="Target Per Day" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblTargetPerDay" runat="server" Text='<%# Eval("TARGETPERDAY") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                              <ItemStyle HorizontalAlign="Right" />
                                                              <HeaderStyle Width="50px" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="AverageBookings" HeaderText="Average Bookings" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAverageBookings" runat="server" Text='<%# Eval("AverageBookings") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                              <ItemStyle HorizontalAlign="Right" Wrap="false"  />
                                                              <HeaderStyle Width="70px" Wrap="true" />
                                                              
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Netbookings" HeaderText="Air Net" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblNetbookings" runat="server" Text='<%# Eval("Netbookings") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                              <ItemStyle HorizontalAlign="Right"/>
                                                              <HeaderStyle Width="45px" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                         
                                                        
                                                          <asp:TemplateField SortExpression="Passive" HeaderText="NBS">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblPassive" runat="server" Text='<%# Eval("Passive") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                              <ItemStyle HorizontalAlign="Right" />
                                                         </asp:TemplateField> 
                                                         
                                                         <asp:TemplateField SortExpression ="WithPassive" HeaderText="Total">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblWithPassive" runat="server" Text='<%# Eval("WithPassive") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" Wrap="false"  /> <HeaderStyle Wrap="true" />
                                                         </asp:TemplateField> 
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Car_Netbookings" HeaderText="Car Netbookings">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblCarNetbookings" runat="server" Text='<%# Eval("Car_Netbookings") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" />
                                                         </asp:TemplateField>    
                                                         
                                                          <asp:TemplateField SortExpression ="Hotel_Netbookings" HeaderText="Hotel Netbookings" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblHotelNetbookings" runat="server" Text='<%# Eval("Hotel_Netbookings") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" />
                                                         </asp:TemplateField>  
                                                         
                                                              
                                                             
                                                       
                                                       <asp:BoundField DataField="D1"  SortExpression="D1">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField   DataField="Car_D1" SortExpression="Car_D1">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField  DataField="Hotel_D1" SortExpression="Hotel_D1" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>

                                                       
                                                       <asp:BoundField  DataField="D2" SortExpression="D2" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField   DataField="Car_D2" SortExpression="Car_D2" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D2" SortExpression="Hotel_D2"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D3" SortExpression="D3" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D3" SortExpression="Car_D3" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D3" SortExpression="Hotel_D3" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D4" SortExpression="D4" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D4" SortExpression="Car_D4" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D4" SortExpression="Hotel_D4"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D5" SortExpression="D5" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D5" SortExpression="Car_D5">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D5" SortExpression="Hotel_D5"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D6" SortExpression="D6" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D6" SortExpression="Car_D6" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D6" SortExpression="Hotel_D6" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D7" SortExpression="D7" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D7" SortExpression="Car_D7" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D7" SortExpression="Hotel_D7" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D8" SortExpression="D8" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D8" SortExpression="Car_D8" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D8" SortExpression="Hotel_D8" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D9" SortExpression="D9" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D9" SortExpression="Car_D9" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D9" SortExpression="Hotel_D9" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                           <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D10" SortExpression="D10"  >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D10" SortExpression="Car_D10" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression="Hotel_D10" DataField="Hotel_D10" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D11" SortExpression="D11" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D11" SortExpression="Car_D11" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D11" SortExpression="Hotel_D11" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D12" SortExpression="D12" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField  SortExpression="Car_D12" DataField="Car_D12" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D12" SortExpression="Hotel_D12" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D13" SortExpression="D13" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D13" SortExpression="Car_D13" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D13" SortExpression="Hotel_D13" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D14" SortExpression="D14"  >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D14" SortExpression="Car_D14" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D14" SortExpression="Hotel_D14" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D15" SortExpression="D15">
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D15" SortExpression="Car_D15" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D15" SortExpression="Hotel_D15" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D16" SortExpression="D16" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D16" SortExpression="Car_D16" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D16" SortExpression="Hotel_D16" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D17" SortExpression="D17" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D17" SortExpression="Car_D17" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D17" SortExpression="Hotel_D17" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D18" SortExpression="D18"  >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D18" SortExpression="Car_D18" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D18" SortExpression="Hotel_D18" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D19" SortExpression="D19" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D19" SortExpression="Car_D19" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D19" SortExpression="Hotel_D19" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D20" SortExpression="D20" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D20" SortExpression="Car_D20" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D20" SortExpression="Hotel_D20" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D21" SortExpression="D21" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D21" SortExpression="Car_D21" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D21" SortExpression="Hotel_D21"  >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D22" SortExpression="D22"  >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D22" SortExpression="Car_D22" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D22" SortExpression="Hotel_D22" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D23" SortExpression="D23" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D23" SortExpression="Car_D23" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D23" SortExpression="Hotel_D23"  >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D24" SortExpression="D24" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D24" SortExpression="Car_D24" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D24" SortExpression="Hotel_D24"  >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D25" SortExpression="D25" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D25" SortExpression="Car_D25" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D25" SortExpression="Hotel_D25"   >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D26" SortExpression="D26" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D26" SortExpression="Car_D26"  >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D26" SortExpression="Hotel_D26"  >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D27" SortExpression="D27" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D27" SortExpression="Car_D27" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D27" SortExpression="Hotel_D27" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D28" SortExpression="D28"  >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D28" SortExpression="Car_D28"  >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D28" SortExpression="Hotel_D28"  >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D29" SortExpression="D29" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D29" SortExpression="Car_D29" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D29" SortExpression="Hotel_D29" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D30" SortExpression="D30" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D30" SortExpression="Car_D30" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D30" SortExpression="Hotel_D30" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="D31" SortExpression="D31" >
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Car_D31" SortExpression="D31">
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Hotel_D31" SortExpression="Hotel_D31">
                                                           <ItemStyle HorizontalAlign="Right" /> <HeaderStyle Wrap=true />
                                                       </asp:BoundField>
                                                       
                                                       
                                                       
                                                       <asp:TemplateField SortExpression ="Performance" HeaderText="Performance">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblPerformance" runat="server" Text='<%# Eval("Performance") %>'></asp:Label>
                                                         </ItemTemplate>
                                                           <ItemStyle HorizontalAlign="Right" />
                                                         </asp:TemplateField>  
                                                         
                                                         
                                                       <asp:TemplateField HeaderText="Action">
                                                       <ItemTemplate>
                                                       <a href="#" class="LinkButtons" id="lnkDetails" runat="server">Details</a>
                                                        </ItemTemplate>
                                                       </asp:TemplateField>
                                                       
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" ForeColor="White"  />
                                                            <RowStyle CssClass="textbold" />
                                                                                       <FooterStyle CssClass="Gridheading" HorizontalAlign="Right" />
                                   </asp:GridView>
                                                                                    </td>                                                                 
                                                                            </tr> 
                                                                    </table>
                                    </asp:Panel>    
                <%End If%>
           
            
              <%If (chkAir.Checked = True And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = True) Or (chkAir.Checked = True And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Then%>
                  <asp:Panel ID="Panel2" runat="server" width="3500px" >
              <table  width="3500px" id="tlbgrdvAirWithAirBr" runat="server"  border="0" align="left" cellpadding="0" cellspacing="0"> 
                                                                            <tr>  
                                                                                    <td class="redborder">
                                                                                      <asp:GridView   EnableViewState ="false" HeaderStyle-ForeColor="white" ShowFooter=true FooterStyle-HorizontalAlign="right"  ID="grdvAirWithAirBr" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="3500px"  FooterStyle-CssClass="Gridheading" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" AllowSorting="True" TabIndex="4">
                                                              <Columns>
                                                              
                                                              
                                                              
                                                         <asp:TemplateField SortExpression ="LCODE"  HeaderText="LCode">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblLcode" runat="server" Text='<%# Eval("LCODE") %>'></asp:Label>
                                                         </ItemTemplate>
                                                          <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>     
                                                           
                                                            <asp:TemplateField SortExpression ="AgencyName"  HeaderText="Agency Name" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAgencyName" runat="server" Text='<%# Eval("AgencyName") %>'></asp:Label>
                                                         </ItemTemplate>
                                                                <HeaderStyle Width="150px" />
                                                                 <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                            
                                                          <asp:TemplateField SortExpression ="Chain_Code"  HeaderText="Chain Code">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblChainCode" runat="server" Text='<%# Eval("Chain_Code") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         <HeaderStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         <asp:TemplateField SortExpression="Group_Classification_Name"  HeaderStyle-Width="50px" HeaderText="Type">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblGroupCode" runat="server" Text='<%# Eval("Group_Classification_Name ") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         <HeaderStyle Wrap="true"  />
                                                         </asp:TemplateField>  
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Employee_Name"  HeaderText="Employee Name">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("Employee_Name") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <HeaderStyle Width="150px" />
                                                               <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="ADDRESS"  HeaderText="Address">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("ADDRESS") %>'></asp:Label>
                                                         </ItemTemplate>
                                                          <HeaderStyle Width="350px" />
                                                         <ItemStyle Wrap="true" Width="350px" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="ONLINE_STATUS"  HeaderText="Online Status">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblOnlineStatus" runat="server" Text='<%# Eval("ONLINE_STATUS") %>'></asp:Label>
                                                         </ItemTemplate>
                                                             <HeaderStyle Width="60px" />
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>  
                                                          
                                                           <asp:BoundField  DataField="COMP_VERTICAL" HeaderText="Company Vertical " SortExpression ="COMP_VERTICAL"   >
                                                                         <ItemStyle Width="60px" Wrap="False" />
                                                                         <HeaderStyle Wrap="true" />
                                                                     </asp:BoundField>    
                                                         
                                                          <asp:TemplateField SortExpression ="CITY" HeaderText="City">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblCity" runat="server" Text='<%# Eval("CITY") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="COUNTRY" HeaderText="Country">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("COUNTRY") %>'></asp:Label>
                                                         </ItemTemplate>
                                                          <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="OFFICEID" HeaderText="Office ID">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblOfficeID" runat="server" Text='<%# Eval("OFFICEID") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Aoffice" HeaderText="Aoffice" FooterText="Total">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAoffice" runat="server" Text='<%# Eval("Aoffice") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <FooterStyle HorizontalAlign="Left" />
                                                               <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Airline_Name" HeaderText="Airline Name">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAirlineName" runat="server" Text='<%# Eval("Airline_Name") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="TARGET" HeaderText="Target" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblTarget" runat="server" Text='<%# Eval("TARGET") %>'></asp:Label>
                                                         </ItemTemplate>
                                                             <ItemStyle HorizontalAlign="Right" />
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="TARGETPERDAY" HeaderText="Target Per Day" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblTargetPerDay" runat="server" Text='<%# Eval("TARGETPERDAY") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" />
                                                                <HeaderStyle Width="50px" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="AverageBookings" HeaderText="Average Bookings" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAverageBookings" runat="server" Text='<%# Eval("AverageBookings") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" />
                                                              <HeaderStyle Width="70px" Wrap="true" />
                                                              
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Netbookings" HeaderText="Air Net" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblNetbookings" runat="server" Text='<%# Eval("Netbookings") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" />
                                                              <HeaderStyle Width="45px" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                         
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Passive" HeaderText="NBS">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblPassive" runat="server" Text='<%# Eval("Passive") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" Width="60px" />
                                                              <HeaderStyle Width="60px" Wrap="true" />
                                                         </asp:TemplateField> 
                                                         
                                                         <asp:TemplateField SortExpression ="WithPassive" HeaderText="Total">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblWithPassive" runat="server" Text='<%# Eval("WithPassive") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" />
                                                         </asp:TemplateField> 
                                                         
                                                         
                                                       
                                                       <asp:BoundField SortExpression ="D1"  DataField="D1">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D2"  DataField="D2" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D3"  DataField="D3" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D4"  DataField="D4" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D5"  DataField="D5" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                      
                                                       <asp:BoundField SortExpression ="D6"  DataField="D6" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                      
                                                       <asp:BoundField SortExpression ="D7"  DataField="D7">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                      
                                                       <asp:BoundField SortExpression ="D8"  DataField="D8">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                      
                                                       <asp:BoundField SortExpression ="D9"  DataField="D9">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                      
                                                       <asp:BoundField SortExpression ="D10"  DataField="D10">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                      
                                                       <asp:BoundField SortExpression ="D11"  DataField="D11" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                      
                                                       <asp:BoundField SortExpression ="D12"  DataField="D12">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                      
                                                       <asp:BoundField SortExpression ="D13"  DataField="D13">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                      
                                                       <asp:BoundField SortExpression ="D14"  DataField="D14">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                      
                                                       <asp:BoundField SortExpression ="D15"  DataField="D15">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                      
                                                       <asp:BoundField SortExpression ="D16"  DataField="D16">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                      
                                                       <asp:BoundField SortExpression ="D17"  DataField="D17">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                      
                                                       <asp:BoundField SortExpression ="D18"  DataField="D18">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                      
                                                       <asp:BoundField SortExpression ="D19"  DataField="D19">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                      
                                                       <asp:BoundField SortExpression ="D20"  DataField="D20">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                        
                                                       <asp:BoundField SortExpression ="D21"  DataField="D21">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                      
                                                       <asp:BoundField SortExpression ="D22"  DataField="D22" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                      
                                                       <asp:BoundField SortExpression ="D23"  DataField="D23">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                      
                                                       <asp:BoundField SortExpression ="D24"  DataField="D24">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                      
                                                       <asp:BoundField SortExpression ="D25"  DataField="D25">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                      
                                                       <asp:BoundField SortExpression ="D26"  DataField="D26">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                      
                                                       <asp:BoundField SortExpression ="D27"  DataField="D27" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                      
                                                       <asp:BoundField SortExpression ="D28"  DataField="D28">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                        
                                                       <asp:BoundField SortExpression ="D29"  DataField="D29">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                      
                                                       <asp:BoundField SortExpression ="D30"  DataField="D30">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                      
                                                       <asp:BoundField SortExpression ="D31"  DataField="D31">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:TemplateField SortExpression="Performance" HeaderText="Performance" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblPerformence" runat="server" Text='<%# Eval("Performance") %>'></asp:Label>
                                                         </ItemTemplate>
                                                           <ItemStyle HorizontalAlign="Right" />
                                                         </asp:TemplateField>    
                                                      
                                                       <asp:TemplateField HeaderText="Action">
                                                       <ItemTemplate>
                                                       <a href="#" class="LinkButtons" id="lnkDetails" runat="server">Details</a>
                                                        </ItemTemplate>
                                                       </asp:TemplateField>
                                                       
                                                  
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="true" ForeColor="White"  />
                                                            <RowStyle CssClass="textbold" />
                                                                                          <FooterStyle CssClass="Gridheading" HorizontalAlign="Right" />
                                   </asp:GridView>
                                                                                    </td>                                                                 
                                                                            </tr> 
                                                                    </table>
                                    </asp:Panel>   
                <%End If%>   
                
               <%If (chkAir.Checked = False And chkCar.Checked = True And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Then%>
                  <asp:Panel ID="Panel1" runat="server" width="3660px" >
              <table  width="3600px" id="tlbgrdvCar" runat="server"  border="0" align="left" cellpadding="0" cellspacing="0"> 
                                                                            <tr valign="top" >  
                                                                                    <td class="redborder">
                                                                                      <asp:GridView EnableViewState ="false"   HeaderStyle-ForeColor="white" ID="grdvCar" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="3600px" ShowFooter=true FooterStyle-CssClass="Gridheading" FooterStyle-HorizontalAlign="right"  HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" AllowSorting="True" TabIndex="4">
                                                              <Columns>
                                                              
                                                              
                                                              
                                                         <asp:TemplateField SortExpression ="LCODE" HeaderText="LCode">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblLcode" runat="server" Text='<%# Eval("LCODE") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>     
                                                           
                                                            <asp:TemplateField SortExpression ="AgencyName" ItemStyle-Wrap="false"  HeaderText="Agency Name" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAgencyName" runat="server" Text='<%# Eval("AgencyName") %>'></asp:Label>
                                                         </ItemTemplate>
                                                                <HeaderStyle Width="150px" />
                                                         </asp:TemplateField>    
                                                         
                                                            
                                                          <asp:TemplateField SortExpression ="Chain_Code" HeaderText="Chain Code">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblChainCode" runat="server" Text='<%# Eval("Chain_Code") %>'></asp:Label>
                                                         </ItemTemplate>
                                                          <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression="Group_Classification_Name"  HeaderStyle-Width="50px" HeaderText="Type">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblGroupCode" runat="server" Text='<%# Eval("Group_Classification_Name ") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         <HeaderStyle Wrap="true"  />
                                                         </asp:TemplateField>  
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Employee_Name" HeaderText="Employee Name">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("Employee_Name") %>'></asp:Label>
                                                         </ItemTemplate>
                                                               <HeaderStyle Width="150px" />
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="ADDRESS" HeaderText="Address">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("ADDRESS") %>'></asp:Label>
                                                         </ItemTemplate>
                                                          <HeaderStyle Width="200px" />
                                                         <ItemStyle Wrap="true" Width="350px" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="ONLINE_STATUS" HeaderText="Online Status">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblOnlineStatus" runat="server" Text='<%# Eval("ONLINE_STATUS") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <HeaderStyle Width="60px" />
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                          <asp:BoundField  DataField="COMP_VERTICAL" HeaderText="Company Vertical " SortExpression ="COMP_VERTICAL"   >
                                                                         <ItemStyle Width="60px" Wrap="False" />
                                                                         <HeaderStyle Wrap="true" />
                                                                     </asp:BoundField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="CITY" HeaderText="City">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblCity" runat="server" Text='<%# Eval("CITY") %>'></asp:Label>
                                                         </ItemTemplate>
                                                          <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="COUNTRY" HeaderText="Country">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("COUNTRY") %>'></asp:Label>
                                                         </ItemTemplate>
                                                          <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="OFFICEID" HeaderText="Office ID">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblOfficeID" runat="server" Text='<%# Eval("OFFICEID") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Aoffice" HeaderText="Aoffice" FooterText="Total">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAoffice" runat="server" Text='<%# Eval("Aoffice") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <FooterStyle HorizontalAlign="Left" />
                                                              <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          
                                                          <asp:TemplateField SortExpression ="TARGET" HeaderText="Target" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblTarget" runat="server" Text='<%# Eval("TARGET") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="TARGETPERDAY" HeaderText="Target Per Day" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblTargetPerDay" runat="server" Text='<%# Eval("TARGETPERDAY") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" Wrap="false" /><HeaderStyle Width="50px" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Car_Netbookings" HeaderText="Car Netbookings" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblCarNetbookings" runat="server" Text='<%# Eval("Car_Netbookings") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" Wrap="false"  />
                                                              
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                           
                                                         
                                                       
                                                       <asp:BoundField SortExpression ="Car_D1"  DataField="Car_D1" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D2"  DataField="Car_D2">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D3"  DataField="Car_D3">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D4"  DataField="Car_D4">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D5"  DataField="Car_D5">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D6"  DataField="Car_D6" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D7"  DataField="Car_D7">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D8"  DataField="Car_D8" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D9"  DataField="Car_D9">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D10"  DataField="Car_D10" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D11"  DataField="Car_D11">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D12"  DataField="Car_D12">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D13"  DataField="Car_D13" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D14"  DataField="Car_D14" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D15"  DataField="Car_D15">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D16"  DataField="Car_D16">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D17"  DataField="Car_D17">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D18"  DataField="Car_D18">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D19"  DataField="Car_D19">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D20"  DataField="Car_D20">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D21"  DataField="Car_D21">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D22"  DataField="Car_D22">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D23"  DataField="Car_D23" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D24"  DataField="Car_D24">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D25"  DataField="Car_D25">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D26"  DataField="Car_D26">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D27"  DataField="Car_D27">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D28"  DataField="Car_D28" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D29"  DataField="Car_D29">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D30"  DataField="Car_D30">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D31"  DataField="Car_D31">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       
                                                       <asp:TemplateField SortExpression="Performance" HeaderText="Performance">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblPerformance" runat="server" Text='<%# Eval("Performance") %>'></asp:Label>
                                                         </ItemTemplate>
                                                           <ItemStyle HorizontalAlign="Right" />
                                                         </asp:TemplateField>   
                                                         
                                                       <asp:TemplateField HeaderText="Action">
                                                       <ItemTemplate>
                                                       <a href="#" class="LinkButtons" id="lnkDetails" runat="server">Details</a>
                                                        </ItemTemplate>
                                                       </asp:TemplateField>
                                                       
                                                       
                                                  </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="true" ForeColor="White"  />
                                                            
                                                            <RowStyle CssClass="textbold" />
                                                                                          <FooterStyle CssClass="Gridheading" HorizontalAlign="Right" />
                                   </asp:GridView>
                                                                                    </td>                                                                 
                                                                            </tr> 
                                                                    </table>
                                    </asp:Panel>   
                <%End If%> 
                
           
           <%If (chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = True And chkAirBreakUp.Checked = False) Then%>
                  <asp:Panel ID="Panel3" runat="server" width="3200px" >
              <table  width="3260px" id="tlbgrdvHotel" runat="server"  border="0" align="left" cellpadding="0" cellspacing="0"> 
                                                                            <tr>  
                                                                                    <td class="redborder">
                                                                                      <asp:GridView EnableViewState ="false" ShowFooter="true" HeaderStyle-ForeColor="white"  ID="grdvHotel" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="3200px" FooterStyle-CssClass="Gridheading" FooterStyle-HorizontalAlign="right"  HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" AllowSorting="True" TabIndex="4">
                                                              <Columns>
                                                              
                                                              
                                                              
                                                         <asp:TemplateField SortExpression ="LCODE" HeaderText="LCode">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblLcode" runat="server" Text='<%# Eval("LCODE") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>     
                                                           
                                                            <asp:TemplateField SortExpression ="AgencyName" HeaderText="Agency Name" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAgencyName" runat="server" Text='<%# Eval("AgencyName") %>'></asp:Label>
                                                         </ItemTemplate>
                                                                <HeaderStyle Width="150px" />
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                            
                                                          <asp:TemplateField SortExpression ="Chain_Code" HeaderText="Chain Code">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblChainCode" runat="server" Text='<%# Eval("Chain_Code") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         <HeaderStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                         <asp:TemplateField SortExpression="Group_Classification_Name" HeaderStyle-Width="50px"  HeaderText="Type">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblGroupCode" runat="server" Text='<%# Eval("Group_Classification_Name ") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         <HeaderStyle Wrap="true"  />
                                                         </asp:TemplateField> 
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Employee_Name" HeaderText="Employee Name">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("Employee_Name") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <HeaderStyle Width="150px" />
                                                              <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="ADDRESS" HeaderText="Address">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("ADDRESS") %>'></asp:Label>
                                                         </ItemTemplate>
                                                          <HeaderStyle Width="200px" />
                                                         <ItemStyle Wrap="true"  Width="350px"/>
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="ONLINE_STATUS"  HeaderText="Online Status">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblOnlineStatus" runat="server" Text='<%# Eval("ONLINE_STATUS") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <HeaderStyle Width="60px" />
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                          <asp:BoundField  DataField="COMP_VERTICAL" HeaderText="Company Vertical " SortExpression ="COMP_VERTICAL"   >
                                                                         <ItemStyle Width="60px" Wrap="False" />
                                                                         <HeaderStyle Wrap="true" />
                                                                     </asp:BoundField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="CITY" HeaderText="City">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblCity" runat="server" Text='<%# Eval("CITY") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="COUNTRY"  HeaderText="Country">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("COUNTRY") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="OFFICEID" HeaderText="Office ID">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblOfficeID" runat="server" Text='<%# Eval("OFFICEID") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Aoffice" HeaderText="Aoffice" FooterText="Total" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAoffice" runat="server" Text='<%# Eval("Aoffice") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <FooterStyle HorizontalAlign="Left" />
                                                              <ItemStyle Wrap="false" />
                                                              <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                         <asp:TemplateField SortExpression ="TARGET" HeaderText="Target" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblTarget" runat="server" Text='<%# Eval("TARGET") %>'></asp:Label>
                                                         </ItemTemplate>
                                                             <ItemStyle HorizontalAlign="Right" />
                                                             <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="TARGETPERDAY" HeaderText="Target Per Day" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblTargetPerDay" runat="server" Text='<%# Eval("TARGETPERDAY") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" />
                                                              <HeaderStyle Width="50px" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Hotel_Netbookings" HeaderText="Hotel Netbookings" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblCarNetbookings" runat="server" Text='<%# Eval("Hotel_Netbookings") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" />
                                                              <HeaderStyle Width="50px" />
                                                        </asp:TemplateField>    
                                                         
                                                        
                                                       
                                                       
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D1"  DataField="Hotel_D1" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D2"  DataField="Hotel_D2" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D3"  DataField="Hotel_D3">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D4"  DataField="Hotel_D4" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D5"  DataField="Hotel_D5">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D6"  DataField="Hotel_D6">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D7"  DataField="Hotel_D7" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D8"  DataField="Hotel_D8">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D9"  DataField="Hotel_D9">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D10"  DataField="Hotel_D10" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D11"  DataField="Hotel_D11" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D12"  DataField="Hotel_D12" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D13"  DataField="Hotel_D13">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D14"  DataField="Hotel_D14">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D15"  DataField="Hotel_D15" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D16"  DataField="Hotel_D16" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D17"  DataField="Hotel_D17" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D18"  DataField="Hotel_D18" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D19"  DataField="Hotel_D19" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D20"  DataField="Hotel_D20" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D21"  DataField="Hotel_D21">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D22"  DataField="Hotel_D22"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D23"  DataField="Hotel_D23" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D24"  DataField="Hotel_D24"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D25"  DataField="Hotel_D25">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D26"  DataField="Hotel_D26" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D27"  DataField="Hotel_D27" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D28"  DataField="Hotel_D28">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D29"  DataField="Hotel_D29">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D30"  DataField="Hotel_D30">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Hotel_D31"  DataField="Hotel_D31" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                          <asp:TemplateField SortExpression="Performance" HeaderText="Performance">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblPerformance" runat="server" Text='<%# Eval("Performance") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" />
                                                         </asp:TemplateField>    
                                                          
                                                          
                                                          <asp:TemplateField HeaderText="Action">
                                                       <ItemTemplate>
                                                       <a href="#" class="LinkButtons" id="lnkDetails" runat="server">Details</a>
                                                        </ItemTemplate>
                                                       </asp:TemplateField>
                                                       
                                                       
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" ForeColor="White"  />
                                                            <RowStyle CssClass="textbold" />
                                                                                          <FooterStyle CssClass="Gridheading" HorizontalAlign="Right" />
                                   </asp:GridView>
                                                                                    </td>                                                                 
                                                                            </tr> 
                                                                    </table>
                                    </asp:Panel>   
                <%End If%> 
           
           
            <%If (chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Or (chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = True) Then%>
                  <asp:Panel ID="Panel4" runat="server" width="1000px" >
              <table  width="1060px" border="0" id="tlbgrdvNoChk" runat="server"  align="left" cellpadding="0" cellspacing="0"> 
                                                                            <tr>  
                                                                                    <td class="redborder">
                                                                                      <asp:GridView EnableViewState="false" HeaderStyle-ForeColor="white" ID="grdvNoChk" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="1000px" ShowFooter=true FooterStyle-CssClass="Gridheading" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" AllowSorting="True" TabIndex="4">
                                                              <Columns>
                                                              
                                                              
                                                              
                                                         <asp:TemplateField SortExpression ="LCODE" HeaderText="LCode">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblLcode" runat="server" Text='<%# Eval("LCODE") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>     
                                                           
                                                            <asp:TemplateField SortExpression ="AgencyName" HeaderText="Agency Name">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAgencyName" runat="server" Text='<%# Eval("AgencyName") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <HeaderStyle Width="150px" />
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                            
                                                          <asp:TemplateField SortExpression ="Chain_Code" HeaderText="Chain Code">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblChainCode" runat="server" Text='<%# Eval("Chain_Code") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>  
                                                         
                                                         
                                                          <asp:TemplateField SortExpression="Group_Classification_Name"  HeaderStyle-Width="50px" HeaderText="Type">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblGroupCode" runat="server" Text='<%# Eval("Group_Classification_Name ") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         <HeaderStyle Wrap="true"  />
                                                         </asp:TemplateField> 
                                                         
                                                           
                                                         
                                                          <asp:TemplateField SortExpression ="Employee_Name" HeaderText="Employee Name">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("Employee_Name") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <HeaderStyle Width="120px"  Wrap=true />
                                                               <ItemStyle Wrap="false" />
                                                              
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="ADDRESS" HeaderText="Address">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("ADDRESS") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Width="350px" Wrap="true" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="ONLINE_STATUS" HeaderText="Online Status">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblOnlineStatus" runat="server" Text='<%# Eval("ONLINE_STATUS") %>'></asp:Label>
                                                         </ItemTemplate>
                                                               <HeaderStyle Wrap=true Width="60px" />
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                          <asp:BoundField  DataField="COMP_VERTICAL" HeaderText="Company Vertical " SortExpression ="COMP_VERTICAL"   >
                                                                         <ItemStyle Width="60px" Wrap="False" />
                                                                         <HeaderStyle Wrap="true" />
                                                                     </asp:BoundField>    
                                                         
                                                          <asp:TemplateField SortExpression ="CITY" HeaderText="City">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblCity" runat="server" Text='<%# Eval("CITY") %>'></asp:Label>
                                                         </ItemTemplate>
                                                               <HeaderStyle Wrap=true />
                                                         
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="COUNTRY" HeaderText="Country">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("COUNTRY") %>'></asp:Label>
                                                         </ItemTemplate>
                                                               <HeaderStyle Wrap=true />
                                                         
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="OFFICEID" HeaderText="Office ID">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblOfficeID" runat="server" Text='<%# Eval("OFFICEID") %>'></asp:Label>
                                                         </ItemTemplate>
                                                               <HeaderStyle Wrap=true />
                                                         
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Aoffice" HeaderText="Aoffice" FooterText="Total">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAoffice" runat="server" Text='<%# Eval("Aoffice") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <FooterStyle HorizontalAlign="Left" />
                                                               <HeaderStyle Wrap=true />
                                                              
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Airline_Name" HeaderText="Airline Name" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAirlineName" runat="server" Text='<%# Eval("Airline_Name") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="TARGET" HeaderText="Target">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblTarget" runat="server" Text='<%# Eval("TARGET") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" />
                                                              <FooterStyle HorizontalAlign="Right" />
                                                               <HeaderStyle Wrap=true />
                                                              
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField ItemStyle-Width="105px" SortExpression ="TARGETPERDAY" HeaderText="Target Per Day">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblTargetPerDay" runat="server" Text='<%# Eval("TARGETPERDAY") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                              <FooterStyle HorizontalAlign="Right" />
                                                              <HeaderStyle Wrap=true />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField ItemStyle-Width="80px" SortExpression ="Performance" HeaderText="Performance">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblPerformance" runat="server" Text='<%# Eval("Performance") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" />
                                                              <FooterStyle HorizontalAlign="Right" />
                                                               <HeaderStyle Wrap=true />
                                                              
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                         <asp:TemplateField  HeaderText="Action">
                                                       <ItemTemplate>
                                                       <a href="#" class="LinkButtons" id="lnkDetails" runat="server">Details</a>
                                                        </ItemTemplate>
                                                       </asp:TemplateField>
                                                       
                                                       
                                                        </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" ForeColor="White"  />
                                                            <RowStyle CssClass="textbold" />
                                                                                          <FooterStyle CssClass="Gridheading" />
                                   </asp:GridView>
                                                                                    </td>                                                                 
                                                                            </tr> 
                                                                    </table>
                                    </asp:Panel>   
                <%End If%> 
                
                
                <%If (chkAir.Checked = False And chkCar.Checked = True And chkHotel.Checked = True And chkAirBreakUp.Checked = False) Then%>
                  <asp:Panel ID="Panel5" runat="server" width="5500px" >
              <table  width="5560px" id="tlbgrdvCarHotel" runat="server"  border="0" align="left" cellpadding="0" cellspacing="0"> 
                                                                            <tr>  
                                                                                    <td class="redborder">
                                                                                      <asp:GridView EnableViewState="false" HeaderStyle-ForeColor="white"  ID="grdvCarHotel" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="5500px" FooterStyle-CssClass="Gridheading" HeaderStyle-CssClass="Gridheading" ShowFooter="true" FooterStyle-HorizontalAlign="right"  RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" AllowSorting="True" TabIndex="4">
                                                              <Columns>
                                                              
                                                              
                                                              
                                                         <asp:TemplateField SortExpression ="LCODE" HeaderText="LCode">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblLcode" runat="server" Text='<%# Eval("LCODE") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>     
                                                           
                                                            <asp:TemplateField SortExpression ="AgencyName" HeaderText="Agency Name" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAgencyName" runat="server" Text='<%# Eval("AgencyName") %>'></asp:Label>
                                                         </ItemTemplate>
                                                                <HeaderStyle Width="150px" />
                                                                <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                            
                                                          <asp:TemplateField SortExpression ="Chain_Code" HeaderText="Chain Code">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblChainCode" runat="server" Text='<%# Eval("Chain_Code") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         <HeaderStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         <asp:TemplateField SortExpression="Group_Classification_Name"  HeaderStyle-Width="50px" HeaderText="Type">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblGroupCode" runat="server" Text='<%# Eval("Group_Classification_Name ") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         <HeaderStyle Wrap="true"  />
                                                         </asp:TemplateField> 
                                                         
                                                          <asp:TemplateField SortExpression ="Employee_Name" HeaderText="Employee Name">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("Employee_Name") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <HeaderStyle Width="150px" />
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="ADDRESS" HeaderText="Address">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("ADDRESS") %>'></asp:Label>
                                                         </ItemTemplate>
                                                          <HeaderStyle Width="200px" />
                                                         <ItemStyle Wrap="true" Width="350px" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="ONLINE_STATUS" HeaderText="Online Status">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblOnlineStatus" runat="server" Text='<%# Eval("ONLINE_STATUS") %>'></asp:Label>
                                                         </ItemTemplate>
                                                          <HeaderStyle Width="60px" />
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                          <asp:BoundField  DataField="COMP_VERTICAL" HeaderText="Company Vertical " SortExpression ="COMP_VERTICAL"   >
                                                                         <ItemStyle Width="60px" Wrap="False" />
                                                                         <HeaderStyle Wrap="true" />
                                                                     </asp:BoundField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="CITY" HeaderText="City">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblCity" runat="server" Text='<%# Eval("CITY") %>'></asp:Label>
                                                         </ItemTemplate>
                                                          <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="COUNTRY" HeaderText="Country">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("COUNTRY") %>'></asp:Label>
                                                         </ItemTemplate>
                                                          <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="OFFICEID" HeaderText="Office ID">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblOfficeID" runat="server" Text='<%# Eval("OFFICEID") %>'></asp:Label>
                                                         </ItemTemplate>
                                                          <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Aoffice" HeaderText="Aoffice" FooterText="Total">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAoffice" runat="server" Text='<%# Eval("Aoffice") %>'></asp:Label>
                                                         </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Left" />
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                        
                                                         
                                                          <asp:TemplateField SortExpression ="TARGET" HeaderText="Target" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblTarget" runat="server" Text='<%# Eval("TARGET") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" />
                                                              <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="TARGETPERDAY" HeaderText="Target Per Day">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblTargetPerDay" runat="server" Text='<%# Eval("TARGETPERDAY") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" />
                                                              
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                         
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Car_Netbookings" HeaderText="Car Netbookings">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblCar_Netbookings" runat="server" Text='<%# Eval("Car_Netbookings") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                                         </asp:TemplateField>  
                                                         
                                                          <asp:TemplateField SortExpression ="Hotel_Netbookings" HeaderText="Hotel Netbookings">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblHotel_Netbookings" runat="server" Text='<%# Eval("Hotel_Netbookings") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                                              
                                                         </asp:TemplateField> 
                                                         
                                                         
                                                         
                                                         <asp:BoundField SortExpression ="Car_D1"  DataField="Car_D1" >
                                                             <ItemStyle HorizontalAlign="Right" />
                                                         </asp:BoundField>
                                                         <asp:BoundField SortExpression ="Hotel_D1"  DataField="Hotel_D1" >
                                                             <ItemStyle HorizontalAlign="Right" />
                                                         </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D2"  DataField="Car_D2">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D2"  DataField="Hotel_D2" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D3"  DataField="Car_D3">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D3"  DataField="Hotel_D3">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D4"  DataField="Car_D4">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D4"  DataField="Hotel_D4" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D5"  DataField="Car_D5">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D5"  DataField="Hotel_D5">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D6"  DataField="Car_D6">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D6"  DataField="Hotel_D6" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D7"  DataField="Car_D7" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D7"  DataField="Hotel_D7" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       
                                                       <asp:BoundField SortExpression ="Car_D8"  DataField="Car_D8" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D8"  DataField="Hotel_D8" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D9"  DataField="Car_D9">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D9"  DataField="Hotel_D9">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D10"  DataField="Car_D10">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D10"  DataField="Hotel_D10">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D11"  DataField="Car_D11">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D11"    DataField="Hotel_D11" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       
                                                       <asp:BoundField SortExpression ="Car_D12"  DataField="Car_D12"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D12"  DataField="Hotel_D12">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D13"  DataField="Car_D13" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D13"  DataField="Hotel_D13">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       
                                                       
                                                       <asp:BoundField SortExpression ="Car_D14"  DataField="Car_D14" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D14"  DataField="Hotel_D14">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D15"  DataField="Car_D15">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D15"  DataField="Hotel_D15" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D16"  DataField="Car_D16">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D16"  DataField="Hotel_D16" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D17"  DataField="Car_D17" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D17"  DataField="Hotel_D17">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D18"  DataField="Car_D18">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D18"  DataField="Hotel_D18"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D19"  DataField="Car_D19" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D19"  DataField="Hotel_D19"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D20"  DataField="Car_D20">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D20"  DataField="Hotel_D20" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D21"  DataField="Car_D21">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D21"  DataField="Hotel_D21">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D22"  DataField="Car_D22" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D22"  DataField="Hotel_D22" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D23"  DataField="Car_D23" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D23"  DataField="Hotel_D23" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D24"  DataField="Car_D24" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D24"  DataField="Hotel_D24">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D25"  DataField="Car_D25">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D25"  DataField="Hotel_D25" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D26"  DataField="Car_D26">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D26"  DataField="Hotel_D26" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D27"  DataField="Car_D27" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D27"  DataField="Hotel_D27" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D28"  DataField="Car_D28" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D28"  DataField="Hotel_D28" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D29"  DataField="Car_D29" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D29"  DataField="Hotel_D29" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D30"  DataField="Car_D30" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D30"  DataField="Hotel_D30"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="Car_D31"  DataField="Car_D31">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D31"  DataField="Hotel_D31" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                         


                                                         
                                                         
                                                          
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Performance" HeaderText="Performance">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblPerformance" runat="server" Text='<%# Eval("Performance") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                        <asp:TemplateField  HeaderText="Action">
                                                       <ItemTemplate>
                                                       <a href="#" class="LinkButtons" id="lnkDetails" runat="server">Details</a>
                                                        </ItemTemplate>
                                                       </asp:TemplateField>
                                                       
                                                       
                                                        </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" ForeColor="White"  />
                                                            <RowStyle CssClass="textbold" />
                                                                                          <FooterStyle CssClass="Gridheading" HorizontalAlign="Right" />
                                   </asp:GridView>
                                                                                    </td>                                                                 
                                                                            </tr> 
                                                                    </table>
                                    </asp:Panel>   
                <%End If%>
                
                 <%If (chkAir.Checked = True And chkCar.Checked = True And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Then%>
                  <asp:Panel ID="Panel6" runat="server" width="5500px" >
              <table  width="5560px" border="0" id="tlbgrdvAirCar" runat="server"  align="left" cellpadding="0" cellspacing="0"> 
                                                                            <tr>  
                                                                                    <td class="redborder">
                                                                                      <asp:GridView ID="grdvAirCar" HeaderStyle-ForeColor="white" EnableViewState="false" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="5500px" FooterStyle-CssClass="Gridheading" ShowFooter="true" FooterStyle-HorizontalAlign="right" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" AllowSorting="True" TabIndex="4">
                                                              <Columns>
                                                              
                                                              
                                                              
                                                         <asp:TemplateField SortExpression ="LCODE" HeaderText="LCode">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblLcode" runat="server" Text='<%# Eval("LCODE") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>     
                                                           
                                                            <asp:TemplateField SortExpression ="AgencyName" HeaderText="Agency Name" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAgencyName" runat="server" Text='<%# Eval("AgencyName") %>'></asp:Label>
                                                         </ItemTemplate>
                                                               <HeaderStyle Width="150px" />
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                            
                                                          <asp:TemplateField SortExpression ="Chain_Code" HeaderText="Chain Code">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblChainCode" runat="server" Text='<%# Eval("Chain_Code") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression="Group_Classification_Name" HeaderStyle-Width="50px"  HeaderText="Type">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblGroupCode" runat="server" Text='<%# Eval("Group_Classification_Name ") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         <HeaderStyle Wrap="true"  />
                                                         </asp:TemplateField> 
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Employee_Name" HeaderText="Employee Name">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("Employee_Name") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <HeaderStyle Width="150px" />
                                                         <ItemStyle Wrap="false" />
                                                              
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="ADDRESS" HeaderText="Address">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("ADDRESS") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <HeaderStyle Width="200px" />
                                                         <ItemStyle Wrap="true" Width="350px" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="ONLINE_STATUS" HeaderText="Online Status">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblOnlineStatus" runat="server" Text='<%# Eval("ONLINE_STATUS") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <HeaderStyle Width="60px" />
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                          <asp:BoundField  DataField="COMP_VERTICAL" HeaderText="Company Vertical " SortExpression ="COMP_VERTICAL"   >
                                                                         <ItemStyle Width="60px" Wrap="False" />
                                                                         <HeaderStyle Wrap="true" />
                                                                     </asp:BoundField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="CITY" HeaderText="City">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblCity" runat="server" Text='<%# Eval("CITY") %>'></asp:Label>
                                                         </ItemTemplate>
                                                          <ItemStyle Wrap="false" />
                                                        
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="COUNTRY" HeaderText="Country">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("COUNTRY") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="OFFICEID" HeaderText="Office ID">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblOfficeID" runat="server" Text='<%# Eval("OFFICEID") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Aoffice" HeaderText="Aoffice"  FooterText="Total">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAoffice" runat="server" Text='<%# Eval("Aoffice") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <FooterStyle HorizontalAlign="Left" /><ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         <asp:TemplateField SortExpression ="TARGET" HeaderText="Target">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblTarget" runat="server" Text='<%# Eval("TARGET") %>'></asp:Label>
                                                         </ItemTemplate>
                                                             <ItemStyle HorizontalAlign="Right" /><ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="TARGETPERDAY" HeaderText="Target Per Day">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblTargetPerDay" runat="server" Text='<%# Eval("TARGETPERDAY") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" /><ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                         
                                                         
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="AverageBookings" HeaderText="Average Bookings">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAverageBookings" runat="server" Text='<%# Eval("AverageBookings") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" Wrap="false" />   
                                                              <HeaderStyle Width="70px" Wrap="true" />
                                                              </asp:TemplateField>  
                                                         
                                                          <asp:TemplateField SortExpression ="Netbookings" HeaderText="Air Net">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblNetbookings" runat="server" Text='<%# Eval("Netbookings") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" /> <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>  
                                                         
                                                         
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Passive" HeaderText="NBS">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblPassive" runat="server" Text='<%# Eval("Passive") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" /> <ItemStyle Wrap="false" />
                                                         </asp:TemplateField> 
                                                         
                                                         <asp:TemplateField SortExpression ="WithPassive" HeaderText="Total">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblWithPassive" runat="server" Text='<%# Eval("WithPassive") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" /> <ItemStyle Wrap="false" />
                                                         </asp:TemplateField> 
                                                         
                                                         
                                                         
                                                           <asp:TemplateField SortExpression ="Car_Netbookings" HeaderText="Car Netbookings">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblCar_Netbookings" runat="server" Text='<%# Eval("Car_Netbookings") %>'></asp:Label>
                                                         </ItemTemplate>
                                                               <ItemStyle HorizontalAlign="Right"  Wrap="false"/> 
                                                         </asp:TemplateField> 
                                                         
                                                         
                                                         
                                                         
                                                         
                                                         
                                                         
                                                         
                                                         
                                                         
                                                         
                                                         
                                                         
                                                         
                                                         
                                                         
                                                         
                                                         
                                                         <asp:BoundField SortExpression ="D1"  DataField="D1"  >
                                                             <ItemStyle HorizontalAlign="Right" />
                                                         </asp:BoundField>
                                                         <asp:BoundField SortExpression ="Car_D1"  DataField="Car_D1" >
                                                             <ItemStyle HorizontalAlign="Right" />
                                                         </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D2"  DataField="D2">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D2"  DataField="Car_D2" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D3"  DataField="D3" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D3"  DataField="Car_D3">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D4"  DataField="D4" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D4"  DataField="Car_D4" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D5"  DataField="D5" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D5"  DataField="Car_D5" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D6"  DataField="D6" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D6"  DataField="Car_D6" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D7"  DataField="D7" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D7"  DataField="Car_D7" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       
                                                       <asp:BoundField SortExpression ="D8"  DataField="D8">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D8"  DataField="Car_D8" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D9"  DataField="D9" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D9"  DataField="Car_D9">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D10"  DataField="D10">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D10"  DataField="Car_D10" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D11"  DataField="D11">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D11"  DataField="Car_D11">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       
                                                       <asp:BoundField SortExpression ="D12"  DataField="D12">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D12"  DataField="Car_D12" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D13"  DataField="D13" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D13"  DataField="Car_D13" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       
                                                       
                                                       <asp:BoundField SortExpression ="D14"  DataField="D14">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D14"  DataField="Car_D14" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D15"  DataField="D15" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D15"  DataField="Car_D15" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D16"  DataField="D16" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D16"  DataField="Car_D16">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D17"  DataField="D17" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D17"  DataField="Car_D17" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D18"  DataField="D18"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D18"  DataField="Car_D18" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D19"  DataField="D19" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D19"  DataField="Car_D19" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D20"  DataField="D20" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D20"  DataField="Car_D20"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D21"  DataField="D21" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D21"  DataField="Car_D21"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D22"  DataField="D22" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D22"  DataField="Car_D22"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D23"  DataField="D23" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D23"  DataField="Car_D23" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D24"  DataField="D24"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D24"  DataField="Car_D24" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D25"  DataField="D25"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D25"  DataField="Car_D25" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D26"  DataField="D26" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D26"  DataField="Car_D26"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D27"  DataField="D27" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D27"  DataField="Car_D27" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D28"  DataField="D28"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D28"  DataField="Car_D28" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D29"  DataField="D29" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D29"  DataField="Car_D29"   >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D30"  DataField="D30" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D30"  DataField="Car_D30" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField  SortExpression ="D31"  DataField="D31" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Car_D31"  DataField="Car_D31" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                         


                                                          <asp:TemplateField SortExpression ="Performance" HeaderText="Performance">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblPerformance" runat="server" Text='<%# Eval("Performance") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" />
                                                         </asp:TemplateField> 
                                                         
                                                         
                                                        <asp:TemplateField HeaderText="Action">
                                                       <ItemTemplate>
                                                       <a href="#" class="LinkButtons" id="lnkDetails" runat="server">Details</a>
                                                        </ItemTemplate>
                                                       </asp:TemplateField>
                                                       
                                                         
                                                         
                                                        </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" ForeColor="White"  />
                                                            <RowStyle CssClass="textbold" />
                                                                                          <FooterStyle CssClass="Gridheading" HorizontalAlign="Right" />
                                   </asp:GridView>
                                                                                    </td>                                                                 
                                                                            </tr> 
                                                                    </table>
                                    </asp:Panel>   
                <%End If%>
              
              
              
               <%If (chkAir.Checked = True And chkCar.Checked = False And chkHotel.Checked = True And chkAirBreakUp.Checked = False) Then%>
                  <asp:Panel ID="Panel7" runat="server" width="6000px" >
              <table  width="6060px" id="tlbgrdvAirHotel" runat="server"  border="0" align="left" cellpadding="0" cellspacing="0"> 
                                                                            <tr>  
                                                                                    <td class="redborder">
                                                                                      <asp:GridView EnableViewState ="false" ShowFooter="true" FooterStyle-HorizontalAlign="right" HeaderStyle-ForeColor="white" ID="grdvAirHotel" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="6000px" FooterStyle-CssClass="Gridheading" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue" AllowSorting="True" TabIndex="4">
                                                              <Columns>
                                                              
                                                              
                                                              
                                                         <asp:TemplateField SortExpression ="LCODE" HeaderText="LCode">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblLcode" runat="server" Text='<%# Eval("LCODE") %>'></asp:Label>
                                                         </ItemTemplate>
                                                          <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>     
                                                           
                                                            <asp:TemplateField SortExpression ="AgencyName" HeaderText="Agency Name" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAgencyName" runat="server" Text='<%# Eval("AgencyName") %>'></asp:Label>
                                                         </ItemTemplate>
                                                                <HeaderStyle Width="150px" /> <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                            
                                                          <asp:TemplateField SortExpression ="Chain_Code" HeaderText="Chain Code">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblChainCode" runat="server" Text='<%# Eval("Chain_Code") %>'></asp:Label>
                                                         </ItemTemplate> <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>   
                                                         
                                                         
                                                          <asp:TemplateField SortExpression="Group_Classification_Name"  HeaderStyle-Width="50px"  HeaderText="Type">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblGroupCode" runat="server" Text='<%# Eval("Group_Classification_Name ") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Wrap="false" />
                                                         <HeaderStyle Wrap="true"  />
                                                         </asp:TemplateField> 
                                                         
                                                          
                                                         
                                                          <asp:TemplateField SortExpression ="Employee_Name" HeaderText="Employee Name">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("Employee_Name") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <HeaderStyle Width="150px" /> <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="ADDRESS" HeaderText="Address">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("ADDRESS") %>'></asp:Label>
                                                         </ItemTemplate> <ItemStyle Wrap="true" Width="350px" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="ONLINE_STATUS" HeaderText="Online Status">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblOnlineStatus" runat="server" Text='<%# Eval("ONLINE_STATUS") %>'></asp:Label>
                                                         </ItemTemplate> <ItemStyle Wrap="false" Width="60px" />
                                                         </asp:TemplateField>    
                                                          <asp:BoundField  DataField="COMP_VERTICAL" HeaderText="Company Vertical " SortExpression ="COMP_VERTICAL"   >
                                                                         <ItemStyle Width="60px" Wrap="False" />
                                                                         <HeaderStyle Wrap="true" />
                                                                     </asp:BoundField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="CITY" HeaderText="City">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblCity" runat="server" Text='<%# Eval("CITY") %>'></asp:Label>
                                                         </ItemTemplate> <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="COUNTRY" HeaderText="Country">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("COUNTRY") %>'></asp:Label>
                                                         </ItemTemplate> <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="OFFICEID" HeaderText="Office ID">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblOfficeID" runat="server" Text='<%# Eval("OFFICEID") %>'></asp:Label>
                                                         </ItemTemplate> <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Aoffice" HeaderText="Aoffice"  FooterText="Total">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAoffice" runat="server" Text='<%# Eval("Aoffice") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <FooterStyle HorizontalAlign="Left" /> <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                        <asp:TemplateField SortExpression ="TARGET" HeaderText="Target" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblTarget" runat="server" Text='<%# Eval("TARGET") %>'></asp:Label>
                                                         </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" /> <ItemStyle Wrap="false" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                         
                                                         
                                                         
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="TARGETPERDAY" HeaderText="Target Per Day" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblTargetPerDay" runat="server" Text='<%# Eval("TARGETPERDAY") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right"  Wrap="false"  /> 
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="AverageBookings" HeaderText="Average Bookings" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAverageBookings" runat="server" Text='<%# Eval("AverageBookings") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right"    Wrap="false" />
                                                              <HeaderStyle Width="70px" Wrap="true" />
                                                              
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Netbookings" HeaderText="Air Net" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblNetbookings" runat="server" Text='<%# Eval("Netbookings") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right"   Wrap="false"  />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Passive" HeaderText="NBS">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblPassive" runat="server" Text='<%# Eval("Passive") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right"    Wrap="false" />
                                                         </asp:TemplateField> 
                                                         
                                                         <asp:TemplateField SortExpression ="WithPassive" HeaderText="Total">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblWithPassive" runat="server" Text='<%# Eval("WithPassive") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right"   Wrap="false"  />
                                                         </asp:TemplateField> 
                                                         
                                                         
                                                          <asp:TemplateField SortExpression ="Hotel_Netbookings" HeaderText="Hotel Netbookings" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblHotel_Netbookings" runat="server" Text='<%# Eval("Hotel_Netbookings") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right"   Wrap="false"  />
                                                         </asp:TemplateField>    
                                                         
                                                        
                                                         <asp:BoundField SortExpression ="D1"  DataField="D1"  >
                                                             <ItemStyle HorizontalAlign="Right" />
                                                         </asp:BoundField>
                                                         <asp:BoundField SortExpression ="Hotel_D1"  DataField="Hotel_D1" >
                                                             <ItemStyle HorizontalAlign="Right" />
                                                         </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D2"  DataField="D2">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D2"  DataField="Hotel_D2" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D3"  DataField="D3" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D3"  DataField="Hotel_D3"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D4"  DataField="D4" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D4"  DataField="Hotel_D4" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D5"  DataField="D5">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D5"  DataField="Hotel_D5" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D6"  DataField="D6" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D6"  DataField="Hotel_D6" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D7"  DataField="D7">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D7"  DataField="Hotel_D7"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       
                                                       <asp:BoundField SortExpression ="D8"  DataField="D8" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D8"  DataField="Hotel_D8" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D9"  DataField="D9" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D9"  DataField="Hotel_D9"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D10"  DataField="D10" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D10"  DataField="Hotel_D10"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D11"  DataField="D11"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D11"  DataField="Hotel_D11"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       
                                                       <asp:BoundField SortExpression ="D12"  DataField="D12" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D12"  DataField="Hotel_D12">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D13"  DataField="D13" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D13"  DataField="Hotel_D13" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       
                                                       
                                                       <asp:BoundField SortExpression ="D14"  DataField="D14" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D14"  DataField="Hotel_D14" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D15"  DataField="D15" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D15"  DataField="Hotel_D15" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D16"  DataField="D16" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D16"  DataField="Hotel_D16"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D17"  DataField="D17">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D17"  DataField="Hotel_D17" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D18"  DataField="D18" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D18"  DataField="Hotel_D18" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D19"  DataField="D19" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D19"  DataField="Hotel_D19"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D20"  DataField="D20" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D20"  DataField="Hotel_D20"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D21"  DataField="D21"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D21"  DataField="Hotel_D21" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D22"  DataField="D22" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D22"  DataField="Hotel_D22"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D23"  DataField="D23" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D23"  DataField="Hotel_D23" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D24"  DataField="D24" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D24"  DataField="Hotel_D24" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D25"  DataField="D25"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D25"  DataField="Hotel_D25" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D26"  DataField="D26" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D26"  DataField="Hotel_D26" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D27"  DataField="D27" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D27"  DataField="Hotel_D27" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D28"  DataField="D28"  >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D28"  DataField="Hotel_D28" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D29"  DataField="D29" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D29"  DataField="Hotel_D29">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D30"  DataField="D30" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D30"  DataField="Hotel_D30" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField SortExpression ="D31"  DataField="D31" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression ="Hotel_D31"  DataField="Hotel_D31" >
                                                           <ItemStyle HorizontalAlign="Right" />
                                                       </asp:BoundField>
                                                         

                                                          <asp:TemplateField SortExpression ="Performance" HeaderText="Performance">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblPerformance" runat="server" Text='<%# Eval("Performance") %>'></asp:Label>
                                                         </ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Right" />
                                                         </asp:TemplateField>    
                                                         
                                                         
                                                         
                                                         <asp:TemplateField HeaderText="Action">
                                                       <ItemTemplate>
                                                       <a href="#" class="LinkButtons" id="lnkDetails" runat="server">Details</a>
                                                        </ItemTemplate>
                                                       </asp:TemplateField>
                                                       
                                                       
                                                        </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" ForeColor="White"  />
                                                            <RowStyle CssClass="textbold" />
                                                                                          <FooterStyle CssClass="Gridheading" HorizontalAlign="Right" />
                                   </asp:GridView>
                                                                                    </td>                                                                 
                                                                            </tr> 
                                                                    </table>
                                    </asp:Panel>   
                <%End If%>
                  
            </td>
            </tr>
            
            <tr>
            
            <td valign ="top" align="left" >
            
            <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="800px">
                                                      <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
                                                                      <tr class="paddingtop paddingbottom">                                                                                                                                                
                                                                          <td style="width: 243px" class="left" nowrap="nowrap"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  ReadOnly="true" Width="105px" CssClass="textboxgrey" TabIndex="5" ></asp:TextBox></td>
                                                                          <td style="width: 200px" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev" TabIndex="5"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 356px" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" TabIndex="5" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 187px" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next" TabIndex="5">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table>
                                                                  </asp:Panel>
            </td>
            <td></td>
            </tr>
        
        </table>
    </td>
    </tr>
    </table>
      
       
        
    </form>
    
</body>
<script type="text/javascript" language="javascript">
if(document.getElementById("chkAirBreakUp")!=null)
        {
                        var stAirBreak=document.getElementById("chkAirBreakUp").checked;
                         if(stAirBreak==true)
            {
            
                 document.getElementById("dv1").style.display='block';
                 document.getElementById("dv2").style.display='block';
            
           
            //return false;
            } 
            else
            {
            
                 document.getElementById("dv1").style.display='none';
                 document.getElementById("dv2").style.display='none';
            
            }
        }
function TABLE1_onclick() {}
    
    if (document.getElementById("hdAgencyName").value=="")
      {
          document.getElementById("chkGroupProductivity").disabled=true;
          document.getElementById("chkGroupProductivity").checked==false;        
      }	 

</script>
</html>
