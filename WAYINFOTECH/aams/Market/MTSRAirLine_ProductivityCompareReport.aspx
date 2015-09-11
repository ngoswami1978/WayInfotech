<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MTSRAirLine_ProductivityCompareReport.aspx.vb" Inherits="Market_MTSRAirLine_ProductivityCmpareReport" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Market::Airline Passive Report</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
   <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
    <script language="javascript" type="text/javascript"> 
//     function CheckValidation()
//     {  
//                  if (document.getElementById("drpAirline").value=='')  
//                   {
//                    document.getElementById("lblError").innerHTML='Airline is mandatory field.';          
//                    document.getElementById("drpAirline").focus();
//                    return false;  
//                  }       
//                      if(parseInt(document.getElementById("drpYearFrom").value) >parseInt(document.getElementById("drpYearTo").value))
//                    {                   
//                    document.getElementById("lblError").innerHTML='Date range is not valid.';          
//                    document.getElementById("drpYearFrom").focus();
//                    return false;
//                    } 
//                   
//                   if(parseInt(document.getElementById("drpYearTo").value)- parseInt(document.getElementById("drpYearFrom").value)>4)
//                    {                   
//                    document.getElementById("lblError").innerHTML='Maximun number of years should be 4 years.';          
//                    document.getElementById("drpYearFrom").focus();
//                    return false;
//                    } 
//              
//     }
//     
       function CheckValidation()
     {  
                  if (document.getElementById("drpAirline").value=='')  
                   {
                    document.getElementById("lblError").innerHTML='Airline is mandatory field.';          
                    document.getElementById("drpAirline").focus();
                    return false;  
                  }       
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
     
    </script>
</head>
<body >
    <form id="form1"  runat="server" defaultfocus="rdSummOpt" defaultbutton="btnSearch">    
     <table width="845px" align="left" class="border_rightred">
            <tr >
            
                <td valign="top"  style="width:845px;" colspan="2" >
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Market -&gt;</span><span class="sub_menu">Airline Productivity Comparision
                                Report</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                                Airline Productivity Comparision Report</td>
                        </tr>
                         <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" >
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                            <td style="width: 845px;" class="redborder" valign="top" >
                                                                 <table border="0" cellpadding="2" cellspacing="1" style="width: 845px;" class="left">
                                                    <tr>
                                                        <td class="center" colspan="8"  >
                                                          <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                    </tr> 
                                                    <tr>
                                                        <td style="width:3%; ">
                                                        </td>
                                                        <td class="textbold" style="width:15%;">
                                                            Airline<span class="Mandatory">*</span> </td>
                                                        <td style="width: 29%; ">
                                                            <asp:DropDownList ID="drpAirline" runat="server" CssClass="dropdownlist" TabIndex="1"
                                                                Width="242px">
                                                            </asp:DropDownList></td>
                                                         <td class="textbold" style="width: 2%;">
                                                            </td>
                                                             <td class="textbold" style="width: 10%; ">
                                                            City</td>    
                                                        <td class="textbold" style="width: 29%; ">    
                                                            <asp:DropDownList ID="drpCity" runat="server" CssClass="dropdownlist" Height="19px"
                                                                TabIndex="2" Width="242px">
                                                            </asp:DropDownList></td>                                                      
                                                        <td class="left" style="width: 2%"></td> 
                                                        <td class="center" style="width: 10%; ">
                                                            <asp:Button ID="btnSearch" runat="server" CssClass="button" TabIndex="18" Text="Search" AccessKey="A"
                                                                Visible="true" /></td>
                                                    </tr>                                                   
                                                     <tr>
                                                        <td  style="width:3%; ">
                                                        </td>
                                                        <td class="textbold" style="width:15%;">
                                                            Country</td>
                                                        <td style="width: 29%; ">
                                                            <asp:DropDownList ID="drpCountry" runat="server" CssClass="dropdownlist" Height="19px"
                                                                TabIndex="3" Width="242px">
                                                            </asp:DropDownList></td>
                                                         <td class="textbold" style="width: 2%;">
                                                            </td>
                                                             <td class="textbold" style="width: 10%; ">
                                                            Region</td>    
                                                        <td class="textbold" style="width: 29%; ">    
                                                            <asp:DropDownList ID="drpRegion" runat="server" CssClass="dropdownlist" Height="19px"
                                                                TabIndex="4" Width="242px">
                                                            </asp:DropDownList></td>                                                      
                                                        <td class="left" style="width: 2%"></td> 
                                                        <td class="center" style="width: 10%; ">
                                                            <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="20" Text="Export" AccessKey="E"
                                                                Visible="true" /></td>
                                                    </tr> 
                                                     <tr>
                                                        <td  style="width:3%; ">
                                                        </td>
                                                        <td class="textbold" style="width:15%;">
                                                            Aoffice</td>
                                                        <td style="width: 29%; ">
                                                            <asp:DropDownList ID="drpAoffice" runat="server" CssClass="dropdownlist" Height="19px"
                                                                TabIndex="5" Width="242px">
                                                            </asp:DropDownList></td>
                                                         <td class="textbold" style="width: 2%;">
                                                            </td>
                                                             <td class="textbold" style="width: 10%; ">
                                                            </td>    
                                                        <td class="textbold" style="width: 29%; ">    
                                                          </td>                                                      
                                                        <td class="left" style="width: 2%"></td> 
                                                        <td class="center" style="width: 10%; ">
                                                            <asp:Button ID="btnReset" runat="server" CssClass="button" TabIndex="19" Text="Reset" AccessKey="R"
                                                                Visible="true" /></td>
                                                    </tr>
                                                                     <tr>
                                                                         <td style="width: 3%">
                                                                         </td>
                                                                         <td class="textbold" style="width: 15%">
                                                                         Group Type
                                                                         </td>
                                                                         <td class="textbold" style="width: 20%;">
                                                                            <asp:DropDownList onkeyup="gotop(this.id)" CssClass="dropdownlist" TabIndex="17"   ID="drpLstGroupType" runat="server">
                                                                            </asp:DropDownList>
                                                                         </td>
                                                                         <td class="textbold" style="width: 2%">
                                                                         </td>
                                                                         <td class="textbold" style="width: 10%">
                                                                         </td>
                                                                         <td class="textbold" style="width: 29%">
                                                                         </td>
                                                                         <td class="left" style="width: 2%">
                                                                         </td>
                                                                         <td class="center" style="width: 10%">
                                                                         </td>
                                                                     </tr>
                                                    <tbody >
                                                       <tr>  
                                                        <td >      </td>                                                                      
                                                          <td colspan ="7"> </td>
                                                       </tr>
                                                    </tbody>
                                                    <tr>
                                                        <td  style="width:3%; ">
                                                        </td>
                                                        <td class="textbold" style="width:15%;">
                                                            </td>
                                                       
                                                         <td class="subheading"   align ="center" colspan ="4" style ="width:72%;"> Compare Between</td> 
                                                                                                            
                                                        <td class="left" style="width: 2%"></td> 
                                                        <td class="center" style="width: 10%; ">
                                                          </td>
                                                    </tr>                                             
                                                     <tr>
                                                     <td style="width:3%; "></td>
                                                     <td style="width:15%; "></td>
                                                        <td colspan ="4" style ="width:72%;">
                                                            <div  id="DivMonthYearRange" runat ="server"  >
                                                                 <table   border="0" cellspacing="0" cellpadding="0"  width ="100%" >                                                                   
                                                                    <tr> 
                                                                    
                                                                    </tr>
                                                                    <tr>                                                                  
                                                                      <td colspan ="6"  style ="height:2px;" ></td>
                                                                   </tr>  
                                                                           <td colspan ="6" style="width:87%;">
                                                                               <table width="100%" border="0" cellspacing="0" cellpadding="0" >                                                                                          
                                                                                     <tbody>
                                                                                       <tr>
                                                                                        <%-- <td style="width:3%; "></td>--%> 
                                                                                          <td  align ="left" colspan ="6" ><span  class="subheading"  >1'st Year Range</span></td>
                                                                                       </tr>
                                                                                    </tbody>
                                                                                    <tr>                                                                                      
                                                                                          <td colspan ="6"  style ="height:8px;" ></td>
                                                                                       </tr>
                                                                                    <tr>
                                                                                   <%-- <td style="width:3%; ">
                                                                                    </td>--%>
                                                                                    <td class="textbold" style="width:17%;" align ="center" >
                                                                                        Period From</td>
                                                                                    <td style="width: 33%; "><table cellpadding ="0" border ="0" cellspacing="0"  style ="width:100%;">
                                                                                        <tr>
                                                                                           <td style="width:40%; "><asp:DropDownList ID="drpMonthFrom" runat="server" CssClass="dropdownlist"
                                                                                          TabIndex="5" Width="102px">
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
                                                                                            <td align ="right" valign ="top" style="width:10%;">
                                                                                           </td>
                                                                                            <td align ="right" valign ="top" style="width:50%; "><asp:DropDownList id="drpYearFrom" tabIndex=6 runat="server" CssClass="dropdownlist" Width="71px"></asp:DropDownList>
                                                                                           </td>                                                             
                                                                                        </tr>
                                                                                     </table>
                                                                                       </td>
                                                                                     <td class="textbold" style="width: 2%; ">
                                                                                        </td>
                                                                                         <td class="textbold" align ="center" style="width: 13%;">
                                                                                             Period To</td>    
                                                                                    <td class="textbold" style="width: 33%;"><table cellpadding ="0" border ="0" cellspacing="0"  style ="width:100%;">
                                                                                        <tr>
                                                                                           <td style ="width:40%;"><asp:DropDownList ID="drpMonthTo" runat="server" CssClass="dropdownlist"
                                                                                          TabIndex="7" Width="102px">
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
                                                                                            <td align ="right" valign ="top" style ="width:10%;">
                                                                                           </td>
                                                                                            <td align ="right" valign ="top" style ="width:50%;"><asp:DropDownList id="drpYearTo" tabIndex=8 runat="server" CssClass="dropdownlist" Width="71px"></asp:DropDownList>
                                                                                           </td>                                                             
                                                                                        </tr>
                                                                                     </table>    </td>   
                                                                                       <td class="left" style="width: 2%; "></td> 
                                                                                </tr>
                                                                                <tr>
                                                                                  <td style =" height:10px;"></td>
                                                                                </tr>
                                                                                   <tbody>
                                                                                   <tr>
                                                                                     <%--<td style="width:3%; "></td> --%>
                                                                                      <td  align ="left" colspan ="6"> <span class="subheading" >2'nd Year Range</span></td>
                                                                                   </tr>
                                                                               </tbody>
                                                                               <tr>                                                                                     
                                                                                          <td colspan ="6"  style ="height:8px;" ></td>
                                                                                       </tr>
                                                                                   <tr>
                                                                                  <%--  <td style="width:3%; ">
                                                                                    </td>--%>
                                                                                    <td class="textbold" style="width:15%;" align ="center">
                                                                                        Period From</td>
                                                                                    <td style="width: 29%; "><table cellpadding ="0" border ="0" cellspacing="0"  style ="width:100%;">
                                                                                        <tr>
                                                                                           <td style ="width:40%;"><asp:DropDownList ID="drpMonthFrom2" runat="server" CssClass="dropdownlist"
                                                                                          TabIndex="9" Width="102px">
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
                                                                                            <td align ="right" valign ="top" style ="width:10%;">
                                                                                           </td>
                                                                                            <td align ="right" valign ="top" style ="width:50%;"><asp:DropDownList id="drpYearFrom2" tabIndex=10 runat="server" CssClass="dropdownlist" Width="71px"></asp:DropDownList>
                                                                                           </td>                                                             
                                                                                        </tr>
                                                                                     </table>
                                                                                       </td>
                                                                                     <td class="textbold" style="width: 2%;">
                                                                                        </td>
                                                                                         <td class="textbold" style="width: 10%; "  align ="center">
                                                                                             Period To</td>    
                                                                                    <td class="textbold" style="width: 29%; "><table cellpadding ="0" border ="0" cellspacing="0"  style ="width:100%;">
                                                                                        <tr>
                                                                                           <td style ="width:40%;"><asp:DropDownList ID="drpMonthTo2" runat="server" CssClass="dropdownlist"
                                                                                          TabIndex="11" Width="102px">
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
                                                                                            <td align ="right" valign ="top" style ="width:10%;">
                                                                                           </td>
                                                                                            <td align ="right" valign ="top" style ="width:50%;"><asp:DropDownList id="drpYearTo2" tabIndex=12 runat="server" CssClass="dropdownlist" Width="71px"></asp:DropDownList>
                                                                                           </td>                                                             
                                                                                        </tr>
                                                                                        
                                                                                     </table>    </td>   
                                                                                       <td class="left" style="width: 2%"></td>                                                    
                                                                                    <td class="center" style="width: 10%; ">
                                                                                        </td>
                                                                                </tr> 
                                                                                <tr>
                                                                                        <td  style="height: 10%" >&nbsp;</td>
                                                                                        
                                                                                        </tr>
                                                                                 
                                                                               </table>
                                                                           
                                                                           </td>
                                                                 </table>
                                                            
                                                            </div>
                                                             
                                                        </td>
                                                        <td  style="width:2%; "></td>
                                                       <td style="width:10%; "></td> 
                                                     </tr>
                                                   
                                                    <tr> 
                                                    <td></td>
                                                    <td></td>
                                                    <td class="subheading"   align ="center" colspan ="4" style ="width:72%;"> Summary option</td> 
                                                    <td></td>
                                                    <td></td>
                                                    </tr>  
                                                     <tr>
                                                        <td style="width:3%; ">
                                                        </td>
                                                        <td></td>
                                                        <td class="textbold" style="width:85%;" align ="center" valign ="top" colspan ="4" ><table   cellpadding ="0"  cellspacing ="0"> <tr><td >
                                                        <asp:RadioButtonList ID="rdSummOpt" runat="server" CssClass="textbox" RepeatDirection="Horizontal"
                                                                Width="422px" TabIndex="14">
                                                                <asp:ListItem Value="1">City</asp:ListItem>
                                                                <asp:ListItem Selected="True" Value="2">Country</asp:ListItem>
                                                                <asp:ListItem Value="4">Region</asp:ListItem>
                                                                <asp:ListItem Value="3">Amadeus Office</asp:ListItem>
                                                            </asp:RadioButtonList></td></tr></table>
                                                            </td>
                                                                                                            
                                                        <td class="left" style="width: 2%"></td> 
                                                        <td class="center" style="width: 10%; ">
                                                            </td>
                                                    </tr>                         
                                                      <tr>
                                                      <td style="width: 23px"></td>
                                                      <td ><%--<span  class="textbold">Total Records</span>--%></td>
                                                    <td colspan="6"><%-- <asp:TextBox ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="3" Width="100px" ReadOnly="True" Text ="0"></asp:TextBox>--%></td> 
                                                    </tr> 
                                                    
                                                     <tr>
                                                    <td colspan="8">
                                                      
                                                    </td> 
                                                    </tr> 
                                                      <tr>
                                                     <td colspan="8" class="textbold" > <asp:Label id="lblFound" runat ="server" Text ="No. of records found " Font-Bold="True" Width="142px" Visible="False" ></asp:Label>
                                                                <asp:TextBox ID="txtRecordCount3" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="3" Width="100px" ReadOnly="True" Text ="0" Visible="False"></asp:TextBox>
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
            <td style="width:3pt"></td>
                <td valign="top" style="padding-left:8px; height: 200px;" class ="redborder" >
                   <table  width="838px" border="0" align="left" cellpadding="0" cellspacing="0"> 
                   <tr valign="top" >
                     <td>
                     <asp:Button ID="btnFirstYearProductivity" runat="server" Text="1'st Year Productivity" CssClass="headingtab" Width="130px" TabIndex="15" />&nbsp;
                         <asp:Button ID="btnSecYearProductivity" runat="server" Text="2'nd Year Productivity" CssClass="headingtabactive" Enabled="True" Width="130px" TabIndex="15" />&nbsp;
                         <asp:Button id="btnComparision" runat="server" CssClass="headingtabactive" Width="130px" Text="Comparision" Enabled="True" TabIndex="17"></asp:Button></td>
                   </tr>
                   
                        <tr>  
                             <td  colspan ="2" valign ="top" ><%--<span  class ="lightblue">Airline Passive Report&nbsp; </span> --%>
<%--<PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_OUTPUT>

<DETAILS 

 TYPE = '' 

 VALUE = ''

 AMADEUS ='' 

 ABACUS ='' 

 GALILEO ='' 

 WORLDSPAN ='' 

 SABREDOMESTIC =''

 TOTAL ='' 

 TYPE_PER='' 

 AMADEUS_PER ='' 

 ABACUS_PER ='' 

 GALILEO_PER ='' 

 WORLDSPAN_PER ='' 

 SABREDOMESTIC_PER = '' />

<Errors Status="">

 <Error Code="" Description=""/>

</Errors>

</PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_OUTPUT>
--%>

                                    <asp:GridView  EnableViewState="false" ID="grdvAirlineProductivityCom" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor" Caption ="" ShowFooter ="true" 
                                                            AlternatingRowStyle-CssClass="lightblue" BackColor="White" Font-Bold="False" ForeColor="Black" TabIndex="13"  AllowPaging ="true" AllowSorting ="true"  HeaderStyle-ForeColor="White" PageSize="50" >
                                                              <Columns> 
                                                                   
                                                                     <asp:BoundField DataField="VALUE" HeaderText="VALUE"   SortExpression="VALUE"   />
                                                                       <asp:TemplateField HeaderText="1A" SortExpression="AMADEUS"   >
                                                                        <ItemTemplate   >
                                                                                <%#Eval("AMADEUS")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="Tot1A" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                         <ItemStyle HorizontalAlign="Right" Width="120px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                        
                                                                     </asp:TemplateField>     
                                                                        <asp:TemplateField HeaderText="1B"  SortExpression="ABACUS" >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("ABACUS")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="Tot1B" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                         <ItemStyle HorizontalAlign="Right" Width="120px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>     
                                                                       <asp:TemplateField HeaderText="1G" SortExpression="GALILEO"  >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("GALILEO")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="Tot1G" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                         <ItemStyle HorizontalAlign="Right" Width="120px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>  
                                                                   <asp:TemplateField HeaderText="1P" SortExpression="WORLDSPAN"   >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("WORLDSPAN")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="Tot1p" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                         <ItemStyle HorizontalAlign="Right" Width="120px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField> 
                                                                       <asp:TemplateField HeaderText="1W"  SortExpression="SABREDOMESTIC" >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("SABREDOMESTIC")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="Tot1w" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                         <ItemStyle HorizontalAlign="Right" Width="120px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>  
                                                                      <asp:TemplateField HeaderText="Total" SortExpression="TOTAL" >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("TOTAL")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="Total" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                         <ItemStyle HorizontalAlign="Right" Width="120px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>
                                                                     
                                                                
                                                                       <asp:TemplateField HeaderText="Type%" SortExpression="TYPE_PER" >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("TYPE_PER")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotType" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                         <ItemStyle HorizontalAlign="Right" Width="120px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>     
                                                                        <asp:TemplateField HeaderText="1A%" SortExpression="AMADEUS_PER"  >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("AMADEUS_PER")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="Tot1APer" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                         <ItemStyle HorizontalAlign="Right" Width="120px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>     
                                                                       <asp:TemplateField HeaderText="1B%" SortExpression="ABACUS_PER" >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("ABACUS_PER")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="Tot1BPer" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                         <ItemStyle HorizontalAlign="Right" Width="120px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>  
                                                                   <asp:TemplateField HeaderText="1G%" SortExpression="GALILEO_PER" >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("GALILEO_PER")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="Tot1GPer" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                         <ItemStyle HorizontalAlign="Right" Width="120px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField> 
                                                                       <asp:TemplateField HeaderText="1P%"  SortExpression="WORLDSPAN_PER" >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("WORLDSPAN_PER")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="Tot1PPer" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                         <ItemStyle HorizontalAlign="Right" Width="120px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>  
                                                                     <asp:TemplateField HeaderText="1W%"  SortExpression="SABREDOMESTIC_PER" >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("SABREDOMESTIC_PER")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="Tot1WPer" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                         <ItemStyle HorizontalAlign="Right" Width="120px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>             
                                                            </Columns>
                                                            
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" ForeColor="White"  />
                                                            <RowStyle CssClass="textbold" />
                                        <FooterStyle CssClass="Gridheading" />
                                     
                                        <PagerTemplate></PagerTemplate>
                                     <%--   <PagerSettings FirstPageText="" NextPageText="&gt;&gt; Next" LastPageText="" PreviousPageText="&lt;&lt;  Prev" Mode="NextPrevious" />--%>
                                        <PagerStyle HorizontalAlign="Center" />
                                   </asp:GridView>                             
                                                                   
                             </td>
                        </tr>
                        <tr>
                          <td  colspan ="2" style ="height:10px;"></td>
                        </tr> 
              <tr>                                                   
                                                    <td colspan="6" valign ="top"  >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr style="padding-top : 4pt; padding-bottom : 4pt">                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="3" Width="100px" ReadOnly="True" Text ="0" Visible="True"  ></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
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
