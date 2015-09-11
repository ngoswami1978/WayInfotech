<%@ Page Language="VB"  EnableEventValidation="false"   AutoEventWireup="false" CodeFile="MTSRAirLine_PassiveReport.aspx.vb" Inherits="Market_MTSRAirLine_PassiveReport" %>
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
              
     }
    </script>
</head>
<body >
    <form id="form1"  runat="server" defaultfocus="drpAirline" defaultbutton="btnSearch">    
     <table width="860px" align="left" class="border_rightred">
            <tr >
                <td valign="top"  style="width:860px;" >
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Market -&gt;</span><span class="sub_menu">Airline Passive Report</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                                Airline Passive Report</td>
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
                                                            <asp:Button ID="btnSearch" runat="server" CssClass="button" TabIndex="11" Text="Search"
                                                                Visible="true" AccessKey="A" /></td>
                                                    </tr>                                                   
                                                     <tr>
                                                        <td style="width:3%; ">
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
                                                            <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="12" Text="Export"
                                                                Visible="true" AccessKey="E" /></td>
                                                    </tr> 
                                                     <tr>
                                                        <td style="width:3%; ">
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
                                                                 Group Type</td>    
                                                        <td class="textbold" style="width: 29%; "> 
                                                        <asp:DropDownList ID="drpLstGroupType" runat="server" CssClass="dropdownlist" Height="19px" TabIndex="4" Width="242px"></asp:DropDownList></td>                                                      
                                                        <td class="left" style="width: 2%"></td> 
                                                        <td class="center" style="width: 10%; ">
                                                            <asp:Button ID="btnReset" runat="server" CssClass="button" TabIndex="13" Text="Reset"
                                                                Visible="true"  AccessKey="R"/></td>
                                                    </tr>   
                                                      <tr>
                                                        <td style="width:3%; height: 42px;">
                                                        </td>
                                                        <td class="textbold" style="width:15%; height: 42px;">
                                                            Period From</td>
                                                        <td style="width: 29%; height: 42px;"><table cellpadding ="0" border ="0" cellspacing="0"  style ="width:100%;">
                                                            <tr>
                                                               <td style ="width:40%;"><asp:DropDownList ID="drpMonthFrom" runat="server" CssClass="dropdownlist"
                                                              TabIndex="6" Width="96px">
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
                                                                <td align ="right" valign ="top" style ="width:40%;"><asp:DropDownList id="drpYearFrom" tabIndex=7 runat="server" CssClass="dropdownlist" Width="82px"></asp:DropDownList>
                                                               </td>                                                             
                                                            </tr>
                                                         </table>
                                                           </td>
                                                         <td class="textbold" style="width: 2%;">
                                                            </td>
                                                             <td class="textbold" style="width: 10%; ">
                                                                 Period To</td>    
                                                        <td class="textbold" style="width: 29%;"><table cellpadding ="0" border ="0" cellspacing="0"  style ="width:100%;">
                                                            <tr>
                                                               <td style ="width:40%;"><asp:DropDownList ID="drpMonthTo" runat="server" CssClass="dropdownlist"
                                                              TabIndex="8" Width="96px">
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
                                                                <td align ="right" valign ="top" style ="width:40%;"><asp:DropDownList id="drpYearTo" tabIndex=9 runat="server" CssClass="dropdownlist" Width="82px"></asp:DropDownList>
                                                               </td>                                                             
                                                            </tr>
                                                         </table>    </td>   
                                                           <td class="left" style="width: 2%; height: 42px;"></td>                                                    
                                                        <td class="center" style="width: 10%; height: 42px;">
                                                            </td>
                                                    </tr>
                                                     <tr> 
                                                    <td></td>
                                                    <td></td>
                                                    <td    align ="center" colspan ="4" style ="width:72%;"></td> 
                                                    <td></td>
                                                    <td></td>
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
                                                         <td>
                                                        </td>
                                                        <td class="textbold" style="width:85%;" valign ="top" colspan ="4" align ="center" ><table  cellpadding ="0"  cellspacing ="0"> <tr><td>
                                                        <asp:RadioButtonList ID="rdSummOpt" runat="server" CssClass="textbox" RepeatDirection="Horizontal"
                                                                Width="422px" TabIndex="10">
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
                                                       <td></td>
                                                       <td colspan="7">
                                                                </td> 
                                                    </tr> 
                                                      <tr>
                                                      <td></td>
                                                      <td ><%--<span  class="textbold">Total Records</span>--%></td>
                                                    <td colspan="6"> <%--<asp:TextBox ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="3" Width="100px" ReadOnly="True" Text ="0"></asp:TextBox>--%></td> 
                                                    </tr> 
                                                    
                                                     <tr>
                                                    <td colspan="8"  class="textbold" ><asp:Label id="lblFound" runat ="server" Text ="No. of records found " Font-Bold="True" Width="142px" Visible="False" ></asp:Label>
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
                <td valign="top" style="padding-left:4px;" >
                   <table  width="860px" border="0" align="left" cellpadding="0" cellspacing="0"> 
                        <tr>  
                             <td class="redborder" colspan ="2" valign ="top" ><%--<span  class ="lightblue">Airline Passive Report&nbsp; </span> --%>

                                    <asp:GridView EnableViewState ="false" ID="grdvAirPassive" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor" Caption ="" ShowFooter ="true" 
                                                            AlternatingRowStyle-CssClass="lightblue" BackColor="White" Font-Bold="False" ForeColor="Black" TabIndex="14"  AllowSorting ="true" HeaderStyle-ForeColor="white"  >
                                                              <Columns> 
                                                                     <asp:BoundField DataField="SUMMARYTYPE" HeaderText="SUMMARYTYPE"  SortExpression ="SUMMARYTYPE"   />
                                                                     <asp:BoundField DataField="CRSCODETEXT" HeaderText="CRS"   SortExpression ="CRSCODETEXT"   />
                                                                       <asp:TemplateField HeaderText="Passive Segments"  SortExpression ="PASSIVESEGMENTS"  >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("PASSIVESEGMENTS")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotPasSeg" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                         <ItemStyle HorizontalAlign="Right" Width="120px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>     
                                                                        <asp:TemplateField HeaderText="Active Segments"   SortExpression ="ACTIVESEGMENTS" >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("ACTIVESEGMENTS")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotActSeg" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                         <ItemStyle HorizontalAlign="Right" Width="120px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>     
                                                                       <asp:TemplateField HeaderText="Total Segments" SortExpression ="TOTALSEGMENTS"  >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("TOTALSEGMENTS")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotSeg" runat="server"  ></asp:label>
                                                                        </FooterTemplate>                                                                     
                                                                         <ItemStyle HorizontalAlign="Right" Width="120px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>  
                                                                   <asp:TemplateField HeaderText="Passive %" SortExpression ="PASSIVE_PER" >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("PASSIVE_PER")%>
                                                                        </ItemTemplate>                                                                        
                                                                        <FooterTemplate  >  
                                                                             <asp:label ID="TotPassPercent" runat="server"  ></asp:label>
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
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="3" Width="100px" ReadOnly="True" Text ="0" Visible="True"></asp:TextBox></td>
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
