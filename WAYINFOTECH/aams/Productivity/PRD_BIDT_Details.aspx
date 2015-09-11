<%@ Page Language="VB"  EnableEventValidation ="false"  AutoEventWireup="false" CodeFile="PRD_BIDT_Details.aspx.vb" Inherits="Productivity_PRD_BIDT_Details" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Productivity::1 A Productivity Details</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
   <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
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
              
     }
            function SelectBreakFunction(FMonth,TMonth,FYear,TYear,Lcode,Aoff,GrData,UseOrig,ResId,Air,Car,Hotel,Insurance,LimAoff,LimReg,LimOwnOff,Agency,Add,City,Country)
     {
//                 alert(FMonth);               
//                 alert(TMonth);
//                 alert(FYear);
//                 alert(TYear);
//                 alert(Lcode);
//                 alert(Aoff);
//                 alert(GrData);
//                 alert(UseOrig);
//                 alert(ResId);
//                 alert(Air);
//                 alert(Car);
//                 alert(Hotel);               
//                 alert(Insurance);
//                 alert(LimAoff);
//                 alert(LimReg);
//                 alert(LimOwnOff);
//                   alert(Agency);
//                     alert(Add);
//                     alert(City);
//                      alert(Country);
                 var parameter="Fmonth=" + FMonth  + "&TMonth=" + TMonth + "&FYear=" + FYear +  "&TYear=" + TYear +  "&Lcode=" + Lcode +  "&Aoff=" + Aoff +  "&GrData=" + GrData +  "&UseOrig=" + UseOrig  +  "&ResId=" + ResId  +  "&Air=" + Air  +  "&Car=" + Car +  "&Hotel=" + Hotel +  "&Insurance=" + Insurance + "&LimAoff=" + LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff + "&Agency=" + Agency + "&Add=" + Add + "&City=" + City + "&Country=" + Country ;
               
                type = "PDSR_BIDTBreakUp.aspx?Popup=T&" + parameter;
   	            window.open(type,"aa2","height=600,width=920,top=30,left=20,scrollbars=1,status=1");            
   	            return false;
     }
    </script>
</head>
<body >
    <form id="form1"  runat="server" defaultfocus="drpCountry" defaultbutton="btnSearch">    
     <table width="860px" align="left" class="border_rightred">
            <tr >
                <td valign="top"  style="width:860px;" >
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Productivity -&gt;</span><span class="sub_menu">1A Productivity Details</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                                1A Productivity Details</td>
                        </tr>
                         <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" >
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                            <td style="width: 860px;" class="redborder" valign="top" >
                                                                 <table border="0" cellpadding="2" cellspacing="1" style="width: 845px;" class="left">
                                                    <tr>
                                                        <td class="center" colspan="8"  >
                                                          <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                    </tr>
                                                     <tr>
                                                        <td style="width:3%">
                                                        </td>
                                                        <td class="subheading" colspan="5">
                                                            Agency Details</td>
                                                        <td  style="width:2%"></td> 
                                                        <td  style="width:10%">
                                                        </td>
                                                       </tr>
                                                     <tr>
                                                        <td style="width:3%">
                                                        <input id="Hidden1" runat="server" style="width: 1px" type="hidden" /></td>
                                                        <td class="textbold" style="width:15%">
                                                            Agency Name</td>
                                                         <td colspan="4" style="width:70%"><asp:TextBox ID="txtAgencyName" runat="server" CssClass="textboxgrey" MaxLength="50" TabIndex="3" Width="601px" ReadOnly="True"></asp:TextBox>
                                                            </td>                                                       
                                                        <td class="left" style="width: 2%">
                                                            </td> 
                                                        <td class="left" style="width: 10%">
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="24" AccessKey="A" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:3%">
                                                        </td>
                                                        <td class="textbold" style="width:15%"> Address</td>                                                                               
                                                        <td colspan="4" style="width:70%">
                                                            <asp:TextBox ID="txtAdd" runat="server" CssClass="textboxgrey" Height="41px" MaxLength="50"
                                                                ReadOnly="True" TabIndex="3" TextMode="MultiLine" Width="601px"></asp:TextBox></td>                                                      
                                                          <td class="left" style="width: 2%"></td> 
                                                        <td style="width: 10%;" class="left" valign ="top"> 
                                                            <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="24" Text="Export" AccessKey="E" /></td>
                                                    </tr>  
                                                     <tr>
                                                        <td style="width:3%; ">
                                                        </td>
                                                        <td class="textbold" style="width:15%;">
                                                            City</td>
                                                        <td style="width: 29%; ">
                                                            <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                                TabIndex="3" Width="241px"></asp:TextBox></td>
                                                         <td class="textbold" style="width: 2%;">
                                                            </td>
                                                             <td class="textbold" style="width: 10%; ">
                                                            Country</td>    
                                                        <td class="textbold" style="width: 29%; ">    
                                                            <asp:TextBox ID="txtCountry" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                                TabIndex="3" Width="241px"></asp:TextBox></td>                                                      
                                                        <td class="left" style="width: 2%"></td> 
                                                        <td class="center" style="width: 10%; ">
                                                            <asp:Button ID="btnPrint" CssClass="button" runat="server" Text="Print" TabIndex="24" Visible="False"  AccessKey="P"/></td>
                                                    </tr>  
                                                      <tr>
                                                        <td style="width:3%; ">
                                                        </td>
                                                        <td class="textbold" style="width:15%;">
                                                            Month</td>
                                                        <td style="width: 29%; "><table cellpadding ="0" border ="0" cellspacing="0"  style ="width:100%;">
                                                            <tr>
                                                               <td style ="width:40%;"><asp:DropDownList ID="drpMonthFrom" runat="server" CssClass="dropdownlist"
                                                              TabIndex="20" Width="96px">
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
                                                                <td align ="right" valign ="top" style ="width:20%;"><span  class="textbold" >Year</span>
                                                               </td>
                                                                <td align ="right" valign ="top" style ="width:40%;"><asp:DropDownList id="drpYearFrom" tabIndex="21" runat="server" CssClass="dropdownlist" Width="82px"></asp:DropDownList>
                                                               </td>                                                             
                                                            </tr>
                                                         </table>
                                                           </td>
                                                         <td class="textbold" style="width: 2%;">
                                                            </td>
                                                             <td class="textbold" style="width: 10%; ">
                                                                 Month</td>    
                                                        <td class="textbold" style="width: 29%; "><table cellpadding ="0" border ="0" cellspacing="0"  style ="width:100%;">
                                                            <tr>
                                                               <td style ="width:40%;"><asp:DropDownList ID="drpMonthTo" runat="server" CssClass="dropdownlist"
                                                              TabIndex="20" Width="96px">
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
                                                                <td align ="right" valign ="top" style ="width:20%;"><span  class="textbold" >Year</span>
                                                               </td>
                                                                <td align ="right" valign ="top" style ="width:40%;"><asp:DropDownList id="drpYearTo" tabIndex="21" runat="server" CssClass="dropdownlist" Width="82px"></asp:DropDownList>
                                                               </td>                                                             
                                                            </tr>
                                                         </table>    </td>   
                                                           <td class="left" style="width: 2%"></td>                                                    
                                                        <td class="center" style="width: 10%; ">
                                                            </td>
                                                    </tr>  
                                                     <tr>
                                                        <td style="width:3%; ">
                                                        </td>
                                                        <td class="textbold" style="width:15%;">
                                                            </td>
                                                        <td style="width: 29%; ">
                                                            <asp:CheckBox ID="ChkWholeGroup" runat="server" CssClass="textbox" TabIndex="17"
                                                                Text="Show Productivity for whole group" Width="243px" Height ="25px" /></td>
                                                         <td class="textbold" style="width: 2%;">
                                                            </td>
                                                             <td class="textbold" style="width: 10%; ">
                                                            </td>    
                                                        <td class="textbold" style="width: 29%; ">    
                                                            <asp:CheckBox ID="ChkOrignalBook" runat="server" CssClass="textbox" TabIndex="17" Text="NBS " /></td>                                                      
                                                        <td class="left" style="width: 2%"></td> 
                                                        <td class="center" style="width: 10%; ">
                                                            </td>
                                                    </tr>
                                                     <tr>
                                                      <td  style="width:15%; "><span class="subheading" ></span></td>
                                                      <td colspan ="6"  class="subheading">1A Booking Details</td>
                                                    </tr>  
                                                    <tr>
                                                    
                                                    <td style="width:3%; "></td>
                                                      <td  style="width:15%; " colspan ="5"><asp:CheckBoxList ID="ChkABooking" runat="server" CssClass="textbox" TabIndex="19" RepeatDirection="Horizontal" Width="678px">
                                                                    <asp:ListItem Selected="True">Air</asp:ListItem>
                                                                    <asp:ListItem>Car</asp:ListItem>
                                                                    <asp:ListItem>Hotel</asp:ListItem>
                                                                    <asp:ListItem>Insurance</asp:ListItem>
                                                                </asp:CheckBoxList></td>
                                                      <td ></td>
                                                    </tr> 
                                                       <tr>
                                                       <td></td>
                                                       <td colspan="7">
                                                                </td> 
                                                    </tr> 
                                                    
                                                     <tr>
                                                    <td colspan="8"></td> 
                                                    </tr> 
                                                      <tr>
                                                    <td colspan="8" style ="height:10px;"></td> 
                                                    </tr> 
                                                    <tr>
                                                      <td></td>
                                                      <td ><span  class="textbold"><b>No. of records found</b></span></td>
                                                     <td ><asp:TextBox ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="3" Width="100px" ReadOnly="True" Text ="0"></asp:TextBox></td>                                                      
                                                       <td colspan ="2" ><span  class="textbold"><b>Total Productivity </b></span></td>
                                                    <td colspan="3"> <asp:TextBox ID="txtTotlaProductivity" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="3" Width="100px" ReadOnly="True" Text ="0"></asp:TextBox></td> 
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
                <td valign="top" style="padding-left:4px;" >
                   <table  width="860px" border="0" align="left" cellpadding="0" cellspacing="0"> 
                        <tr>  
                             <td class="redborder">
                                    <asp:GridView EnableViewState="false" ID="grdvBidtDetails" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor" AllowSorting="true"  HeaderStyle-ForeColor ="white" 
                                                            AlternatingRowStyle-CssClass="lightblue" >
                                                              <Columns>                                                                   
                                                                     <asp:BoundField DataField="LOCATION_CODE" HeaderText="Lcode "   SortExpression ="LOCATION_CODE"  />                                                                    
                                                                     <asp:BoundField DataField="ADDRESS" HeaderText="Address "   SortExpression ="ADDRESS"  />                                                                    
                                                                     <asp:BoundField  DataField="CITY" HeaderText="City "   SortExpression ="CITY"   />
                                                                     <asp:BoundField DataField="COUNTRY" HeaderText="Country "   SortExpression ="COUNTRY"  />
                                                                     <asp:BoundField DataField="MONTH" HeaderText="Month "   SortExpression ="MONTH"   />
                                                                     <asp:BoundField DataField="YEAR" HeaderText="Year "   SortExpression ="YEAR"   />                                                                   
                                                                     <asp:BoundField DataField="AIR" HeaderText="Air "  SortExpression ="AIR"   />
                                                                     <asp:BoundField DataField="CAR" HeaderText="Car "   SortExpression ="CAR"  />
                                                                     <asp:BoundField DataField="HOTEL" HeaderText="Hotel "   SortExpression ="HOTEL"  />
                                                                     <asp:BoundField DataField="INSURANCE" HeaderText="Insurance "  SortExpression ="INSURANCE"   />
                                                                     <asp:BoundField DataField="TOTAL" HeaderText="Total "  SortExpression ="TOTAL"   />
                                                                     <asp:BoundField DataField="PASSIVE" HeaderText="NBS "  SortExpression ="PASSIVE"   />
                                                                     <asp:BoundField DataField="WITHPASSIVE" HeaderText="Total Air Booking "  SortExpression ="WITHPASSIVE"   />
                                                                     <asp:TemplateField HeaderText="Action">
                                                                         <ItemTemplate>  
                                                                         <a href="#" class="LinkButtons" id="linkABreakUp" runat="server">1 A BreakUp</a>                                                                                                                                      
                                                                         
                                                                              <asp:HiddenField ID="hdAdd" runat="server"  Value ='<%# Eval("ADDRESS") %>' /> 
                                                                             <asp:HiddenField ID="hdCountry" runat="server"   Value ='<%# Eval("COUNTRY") %>' />                                                                            
                                                                             <asp:HiddenField id="hdTot"  Value ='<%# Eval("TOTAL") %>'   runat ="server" />                                                                       
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>   
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False"  />
                                                            <RowStyle CssClass="textbold" />
                                   </asp:GridView>                             
                                                                   
                             </td>
                        </tr> 
                         <tr>                                                   
                                                    <td colspan="6" valign ="top"   >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr style="padding-top : 4pt; padding-bottom : 4pt">                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;</b></span>&nbsp;&nbsp;</td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                    </td>
                                                </tr>
                         <tr>
                            <td colspan="6" ></td> 
                        </tr> 
                     </table> 
                </td> 
            </tr> 
        </table>     
    </form>
</body>
</html>
