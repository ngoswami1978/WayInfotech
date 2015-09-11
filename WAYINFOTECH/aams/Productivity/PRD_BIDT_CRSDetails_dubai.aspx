<%@ Page Language="VB" EnableEventValidation ="false"  AutoEventWireup="false" CodeFile="PRD_BIDT_CRSDetails.aspx.vb" Inherits="Productivity_PRD_BIDT_CRSDetails" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Productivity::1 A Productivity CRS Details</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
   <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
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
                    document.getElementById("lblError").innerHTML='Maximum number of years should be 4 years.';          
                    document.getElementById("drpYearFrom").focus();
                    return false;
                    } 
              
     }
         function EnableCarrierType2()
         {
         
            if (document.getElementById('ChkAirBreak').checked==true )
            {
                if (document.getElementById('drpAirLine').selectedIndex==0 )
                 {
                     document.getElementById('RdCarryType').disabled=false;
                    // document.getElementById('ChkAirBreak').disabled=false;     
                 }
                 else
                 {
                     document.getElementById('RdCarryType').disabled=true;
                    // document.getElementById('ChkAirBreak').disabled=true;
                 }
            }
            else
            {
              document.getElementById('RdCarryType').disabled=true;
              // document.getElementById('ChkAirBreak').disabled=false;
            }
         }

         function EnableCarrierType()
         {
            if (document.getElementById('drpAirLine').selectedIndex==0 )
            {
             // document.getElementById('ChkAirBreak').checked=false;
               document.getElementById('RdCarryType').disabled=true;  
            }
            else
            {
              document.getElementById('RdCarryType').disabled=false;
              //document.getElementById('ChkAirBreak').checked=true;
            }
         }
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
         function gotops(ddlname)
     {
    
     if (event.keyCode==46 )
     {
        document.getElementById(ddlname).selectedIndex=0;
        setTimeout('__doPostBack(\'drpAirLine\',\'\')', 0)
        //EnableCarrierType3();
     }
     }
     
     
      function EnableCarrierType3()
         {
       
            if (document.getElementById('drpAirLine').selectedIndex==0 )
            {
               document.getElementById('ChkAirBreak').checked=false;
               document.getElementById('RdCarryType').disabled=true;  
               document.getElementById('ChkAirBreak').disabled=false;
            }
            else
            {
              document.getElementById('RdCarryType').disabled=true;  
              document.getElementById('ChkAirBreak').checked=true;
             // document.getElementById('ChkAirBreak').disabled=true;
            }           
         }
         
    </script>
</head>
<body   >
    <form id="form1"  runat="server" defaultfocus="drpCountry" defaultbutton="btnSearch">    
     <table width="860px" align="left" class="border_rightred">
            <tr >
                <td valign="top"  style="width:860px; height: 379px;" >
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Productivity -&gt;</span><span class="sub_menu">1 A Productivity CRS Details</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                                1 A Productivity CRS Details</td>
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
                                                        <td style="width:2%">
                                                        </td>
                                                        <td class="subheading" colspan="5" style="width:70%">
                                                            Agency Details</td>
                                                        <td  style="width:16%" rowspan ="3"></td> 
                                                        <td  style="width:12%">
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="25" AccessKey="A" /></td>
                                                       </tr>
                                                     <tr>
                                                        <td style="width:2%">
                                                        </td>
                                                        <td class="textbold" style="width:12%">
                                                            Agency Name</td>
                                                         <td colspan="4" style="width:58%"><asp:TextBox ID="txtAgencyName" runat="server" CssClass="textboxgrey" MaxLength="50" TabIndex="3" Width="513px" ReadOnly="True"></asp:TextBox></td>                                                       
                                                        <td class="left" style="width: 16%">
                                                              <asp:Button ID="btnExport" CssClass="button" runat="server" Text="Export" TabIndex="24" AccessKey="E" /></td> 
                                                        <td class="left" style="width: 12%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:2%; height: 53px;">
                                                        </td>
                                                        <td class="textbold" style="width:12%; height: 53px;"> Address</td>                                                                               
                                                        <td colspan="4" style="width:58%; height: 53px;"><asp:TextBox ID="txtAdd" runat="server" CssClass="textboxgrey" Height="41px" MaxLength="50"
                                                                ReadOnly="True" TabIndex="3" TextMode="MultiLine" Width="514px"></asp:TextBox></td>                                                      
                                                          <td class="left" style="width: 16%; height: 53px;" valign ="top"><asp:Button ID="btnGraph" CssClass="button" runat="server" Text="Graph" TabIndex="26" /></td> 
                                                        <td style="width: 12%; height: 53px;" class="left"></td>
                                                    </tr> 
                                                      <tr>
                                                        <td style="width:2%; height: 25px;">
                                                        </td>
                                                        <td class="textbold" style="width:12%; height: 25px;">
                                                            City</td>
                                                        <td style="width:27%; height: 25px;">
                                                            <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                                TabIndex="3" Width="225px"></asp:TextBox></td>
                                                         <td class="textbold" style="width: 1%; height: 25px;">
                                                            </td>
                                                             <td class="textbold" style="width: 5%; height: 25px;">
                                                            Country</td>    
                                                        <td class="textbold" style="width: 26%; height: 25px;">    
                                                            <asp:TextBox ID="txtCountry" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                                TabIndex="3" Width="217px"></asp:TextBox></td>                                                      
                                                        <td class="left" style="width: 16%; height: 25px;"></td> 
                                                        <td class="left" style="width: 12%; height: 25px;"></td>
                                                    </tr> 
                                                          <tr>
                                                        <td style="width:2%; ">
                                                        </td>
                                                        <td class="textbold" style="width:12%;">
                                                            Month</td>
                                                        <td style="width:27%;"><table cellpadding ="0" border ="0" cellspacing="0"  style ="width:100%;">
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
                                                              <asp:ListItem Value="9">september</asp:ListItem>
                                                              <asp:ListItem Value="10">October</asp:ListItem>
                                                              <asp:ListItem Value="11">November</asp:ListItem>
                                                              <asp:ListItem Value="12">December</asp:ListItem>
                                                          </asp:DropDownList>
                                                               </td>
                                                                <td align ="right" valign ="top" style ="width:20%;"><span  class="textbold" >Year</span>
                                                               </td>
                                                                <td align ="right" valign ="top" style ="width:40%;"><asp:DropDownList id="drpYearFrom" tabIndex=21 runat="server" CssClass="dropdownlist" Width="74px"></asp:DropDownList>
                                                               </td>                                                             
                                                            </tr>
                                                         </table>
                                                           </td>
                                                         <td class="textbold" style="width: 1%;">
                                                            </td>
                                                             <td class="textbold" style="width: 5%; ">
                                                                 Month</td>    
                                                        <td class="textbold" style="width: 26%; "><table cellpadding ="0" border ="0" cellspacing="0"  style ="width:100%;">
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
                                                              <asp:ListItem Value="9">september</asp:ListItem>
                                                              <asp:ListItem Value="10">October</asp:ListItem>
                                                              <asp:ListItem Value="11">November</asp:ListItem>
                                                              <asp:ListItem Value="12">December</asp:ListItem>
                                                          </asp:DropDownList>
                                                               </td>
                                                                <td align ="right" valign ="top" style ="width:20%;"><span  class="textbold" >Year</span>
                                                               </td>
                                                                <td align ="right" valign ="top" style ="width:40%;"><asp:DropDownList id="drpYearTo" tabIndex=21 runat="server" CssClass="dropdownlist" Width="78px"></asp:DropDownList>
                                                               </td>                                                             
                                                            </tr>
                                                         </table>    </td>   
                                                           <td class="left" style="width: 16%"></td>                                                    
                                                        <td class="center" style="width: 12%; ">
                                                            <asp:Button ID="btnPrint" CssClass="button" runat="server" Text="Print" TabIndex="24" Visible="False" AccessKey="P" /></td>
                                                    </tr>  
                                                         <tr>
                                                             <td style="width: 2%; height: 24px">
                                                             </td>
                                                             <td class="textbold" style="width: 12%">
                                                             </td>
                                                             <td style="width: 27%">
                                                            <asp:CheckBox ID="ChkWholeGroup" runat="server" CssClass="textbox" TabIndex="17" Height ="25px"
                                                                Text="Show Productivity for whole group" Width="222px" AutoPostBack="True" /></td>
                                                             <td class="textbold" style="width: 1%">
                                                             </td>
                                                             <td class="textbold" colspan="2" style="width: 31%">
                                                             </td>
                                                             <td class="left" colspan="2" style="width: 28%; height: 24px">
                                                             </td>
                                                         </tr>
                                                     <tr class="displayNone">
                                                        <td style="width:2%; height: 24px;">
                                                        </td>
                                                        <td class="textbold" style="width:12%; "> Airline Name
                                                            </td>
                                                        <td style="width:27%;"  ><asp:DropDownList id="drpAirLine" tabIndex=21 runat="server" CssClass="dropdownlist" Width="232px" AutoPostBack="True">
                                                        </asp:DropDownList></td>
                                                          <td class="textbold" style="width: 1%;">
                                                            </td>
                                                       
                                                             <td class="textbold" style="width: 31%; " colspan ="2">
                                                            </td>    
                                                                                                           
                                                        <td class="left" style="width: 28%; height: 24px;" colspan ="2"><asp:CheckBox ID="ChkAirBreak" runat="server" CssClass="textbox" TabIndex="17"  Enabled ="true"  Checked ="false" 
                                                                Text="Show Airline Breakup" Width="145px" AutoPostBack="True" Visible="False" /></td> 
                                                      
                                                    </tr>  
                                                   
                                                      
                                                    <tr>
                                                    <td style ="width:2%;"></td>                                                     
                                                      <td colspan ="7">
                                                        <table cellpadding ="0"  border ="0" cellspacing="0" style="width: 218px" class="displayNone" >
                                                        <thead class ="textbold" ><tr ><td class ="subheading"> Carrier Type</td></tr> </thead>
                                                        <tr>
                                                            <td style="height: 45px"> 
                                                                <asp:RadioButtonList ID="RdCarryType"    runat="server" CssClass ="textbox" RepeatDirection="Horizontal" Width="462px" Enabled ="false"  AutoPostBack ="true" >
                                                                    <asp:ListItem Value="1">Online</asp:ListItem>
                                                                    <asp:ListItem Value="0">Offline</asp:ListItem>
                                                                    <asp:ListItem Selected="True" Value ="">All</asp:ListItem>
                                                                </asp:RadioButtonList></td>
                                                            </tr>
                                                        
                                                        </table>
                                                      </td>
                                                    </tr>  
                                                   <tr>
                                                      <td></td>
                                                      <td colspan ="2"><span  class="textbold"><strong>No. of records found &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="3" Width="100px" ReadOnly="True" Text ="0"></asp:TextBox></strong></span></td>
                                                     <td ></td>                                                      
                                                       <td colspan ="2" ><span  class="textbold"><strong>Total Productivity &nbsp; </strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                           &nbsp; &nbsp; &nbsp;
                                                           <asp:TextBox ID="txtTotlaProductivity" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="3" Width="100px" ReadOnly="True" Text ="0"></asp:TextBox></span></td>
                                                    <td colspan="2"> </td> 
                                                    </tr> 
                                                 
                                                      </table>
                                                        <input id="Hidden1" runat="server" style="width: 1px" type="hidden" /></td>
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
                                     <%--                       <PR_SEARCHMIDTDETAILS_OUTPUT>
                                      <MIDTDETAILS LCODE="" ADDRESS="" CITY="" 
                                      COUNTRY="" AIRLINENAME="" MONTHYEAR=""
                                       A="" B="" G="" P="" W="" TOTAL="" /> 
                                    - <Errors Status="">
                                      <Error Code="" Description="" /> 
                                      </Errors>
                                      </PR_SEARCHMIDTDETAILS_OUTPUT>  --%>
                             <td class="redborder">
                                    <asp:GridView EnableViewState ="false" ID="grdvBidtCrsDetails" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" HeaderStyle-ForeColor ="white"  AllowSorting ="true" 
                                                            Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue">
                                                              <Columns>  
                                                                     <asp:BoundField DataField="ADDRESS" HeaderText="Address "  SortExpression ="ADDRESS"  />                                                                    
                                                                     <asp:BoundField  DataField="CITY" HeaderText="City "   SortExpression ="CITY"   />
                                                                     <asp:BoundField DataField="COUNTRY" HeaderText="Country "  SortExpression ="COUNTRY"   />
                                                                     <asp:BoundField DataField="MONTHYEAR" HeaderText="Month'Year "     SortExpression ="MONTHYEAR"  />
                                                                      <asp:BoundField DataField="AIRLINENAME" HeaderText="Airline name "   SortExpression ="AIRLINENAME"   />
                                                                     <asp:BoundField DataField="A" HeaderText="1A "  ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="right"   SortExpression ="A"  />                                                                   
                                                                     <asp:BoundField DataField="B" HeaderText="1B "  ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="right"  SortExpression ="B"  />
                                                                     <asp:BoundField DataField="G" HeaderText="1G " ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="right"  SortExpression ="G"   />
                                                                     <asp:BoundField DataField="P" HeaderText="1P "  ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="right"  SortExpression ="P"  />
                                                                      <asp:BoundField DataField="W" HeaderText="1W "  ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="right"  SortExpression ="W"  />                                                                  
                                                                     <asp:BoundField DataField="TOTAL" HeaderText="Total " ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="right"   SortExpression ="TOTAL"  />  
                                                                                                                                                                                                         
                                                                  <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="right" >
                                                                         <ItemTemplate>
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
