<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MTSRNewMarketShare.aspx.vb" Inherits="Market_MTSRNewMarketShare" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Market::Market Share </title>
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
                   
              
              
     }
    </script>
</head>
<body  >
     <form id="form1"  runat="server" defaultfocus="drpCountry" defaultbutton="btnSearch">
     <table width="845px" align="left" class="border_rightred">
            <tr >
                <td valign="top"  style="width:845px;" >
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Market -&gt;</span><span class="sub_menu">Market Share
                                </span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                                Market Share </td>
                        </tr>
                         <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT"  >
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
                                                            Country</td>
                                                        <td style="width: 29%; ">
                                                            <asp:DropDownList ID="drpCountry" runat="server" CssClass="dropdownlist" Height="19px"
                                                                TabIndex="1" Width="242px">
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
                                                            <asp:Button ID="btnSearch" runat="server" CssClass="button" TabIndex="10" Text="Search"
                                                                Visible="true" AccessKey="A" /></td>
                                                    </tr>                                                   
                                                     <tr>
                                                        <td style="width:3%; ">
                                                        </td>
                                                        <td class="textbold" style="width:15%;">
                                                            Aoffice</td>
                                                        <td style="width: 29%; ">
                                                            <asp:DropDownList ID="drpAoffice" runat="server" CssClass="dropdownlist" Height="19px"
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
                                                            <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="11" Text="Export"
                                                                Visible="true" AccessKey="E" /></td>
                                                    </tr> 
                                                      <tr>
                                                        <td style="width:3%; ">
                                                        </td>
                                                        <td class="textbold" style="width:15%;">Period From</td>
                                                        <td style="width: 29%; "><table cellpadding ="0" border ="0" cellspacing="0"  style ="width:100%;">
                                                            <tr>
                                                               <td style ="width:40%;"><asp:DropDownList ID="drpMonthFrom" runat="server" CssClass="dropdownlist"
                                                              TabIndex="5" Width="96px">
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
                                                                <td align ="right" valign ="top" style ="width:40%;"><asp:DropDownList id="drpYearFrom" tabIndex=6 runat="server" CssClass="dropdownlist" Width="82px"></asp:DropDownList>
                                                               </td>                                                             
                                                            </tr>
                                                         </table>
                                                           </td>
                                                         <td class="textbold" style="width: 2%;">
                                                            </td>
                                                             <td class="textbold" style="width: 10%; ">Period To</td>    
                                                        <td class="textbold" style="width: 29%;"><table cellpadding ="0" border ="0" cellspacing="0"  style ="width:100%;">
                                                            <tr>
                                                               <td style ="width:40%;"><asp:DropDownList ID="drpMonthTo" runat="server" CssClass="dropdownlist"
                                                              TabIndex="7" Width="96px">
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
                                                                <td align ="right" valign ="top" style ="width:40%;"><asp:DropDownList id="drpYearTo" tabIndex=8 runat="server" CssClass="dropdownlist" Width="82px"></asp:DropDownList>
                                                               </td>                                                             
                                                            </tr>
                                                         </table>    </td>   
                                                           <td class="left" style="width: 2%; "></td>                                                    
                                                        <td class="center" style="width: 10%;" valign ="top" >
                                                        <asp:Button ID="BtnGraph" runat="server" CssClass="button" TabIndex="12" Text="Graph"
                                                                Visible="true" AccessKey="G" /></td>
                                                    </tr>
                                                                     <tr>
                                                                         <td style="width: 3%">
                                                                         </td>
                                                                         <td class="textbold" style="width: 15%">Group Type
                                                                         </td>
                                                                         <td class="textbold" style="width: 20%;">
                                                                            <asp:DropDownList onkeyup="gotop(this.id)" CssClass="dropdownlist" TabIndex="9"   ID="drpLstGroupType" runat="server" Width="242px">
                                                                            </asp:DropDownList>
                                                                         </td>
                                                                         <td class="textbold" style="width: 2%">
                                                                         </td>
                                                                         <td class="textbold" style="width: 14%">
                                                                             Company Vertical</td>
                                                                         <td class="textbold" style="width: 29%">
                                                                             <asp:DropDownList ID="DlstCompVertical" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                                 TabIndex="1" Width="96px">
                                                                             </asp:DropDownList></td>
                                                                         <td class="left" style="width: 2%">
                                                                         </td>
                                                                         <td class="center" style="width: 10%" valign="top"><asp:Button ID="BtnReset" runat="server" CssClass="button" TabIndex="13" Text="Reset"
                                                                Visible="true" AccessKey="R" />
                                                                         </td>
                                                                     </tr>
                                                     <tr> 
                                                    <td></td>
                                                    <td></td>
                                                    <td    align ="center" colspan ="4" style ="width:72%;"></td> 
                                                    <td></td>
                                                    <td class="center" style="width: 10%; ">
                                                    </td>
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
                                                                Width="422px" TabIndex="9">
                                                                <asp:ListItem Value="1">City</asp:ListItem>
                                                                <asp:ListItem Selected="True" Value="2">Country</asp:ListItem>                                                              
                                                                <asp:ListItem Value="3">Amadeus Office</asp:ListItem>
                                                                <asp:ListItem Value="4">Region</asp:ListItem>
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
                                                      <td ></td>
                                                    <td colspan="6"> </td> 
                                                    </tr> 
                                                    
                                                    <tr>
                                                      <td colspan ="9">
                                                              <asp:GridView  EnableViewState="false" ID="grdvMarketShare" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"  
                                                            Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor" Caption ="" ShowFooter ="true" 
                                                            AlternatingRowStyle-CssClass="lightblue" BackColor="White" Font-Bold="False" ForeColor="Black" TabIndex="15"   AllowSorting ="true"  HeaderStyle-ForeColor="White"  >
                                                              <Columns> 
                                                                   
                                                                     <asp:BoundField DataField="SELECTBY" HeaderText="SELECTBY"   SortExpression="SELECTBY"   />
                                                                       <asp:TemplateField HeaderText="1A" SortExpression="A"   >
                                                                        <ItemTemplate   >
                                                                                <%#Eval("A")%>
                                                                        </ItemTemplate>                                                  
                                                                         <ItemStyle HorizontalAlign="Right" Width="120px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                        
                                                                     </asp:TemplateField>     
                                                                        <asp:TemplateField HeaderText="1B"  SortExpression="B" >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("B")%>
                                                                        </ItemTemplate>                                               
                                                                         <ItemStyle HorizontalAlign="Right" Width="120px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>     
                                                                       <asp:TemplateField HeaderText="1G" SortExpression="G"  >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("G")%>
                                                                        </ItemTemplate>                                                  
                                                                         <ItemStyle HorizontalAlign="Right" Width="120px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>  
                                                                   <asp:TemplateField HeaderText="1P" SortExpression="P"   >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("P")%>
                                                                        </ItemTemplate>                                                
                                                                         <ItemStyle HorizontalAlign="Right" Width="120px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField> 
                                                                       <asp:TemplateField HeaderText="1W"  SortExpression="W" >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("W")%>
                                                                        </ItemTemplate>                                               
                                                                         <ItemStyle HorizontalAlign="Right" Width="120px" Wrap="False" />
                                                                         <HeaderStyle Wrap="False" />
                                                                         <FooterStyle HorizontalAlign="Right" Wrap="False" />
                                                                     </asp:TemplateField>  
                                                                      <asp:TemplateField HeaderText="Total" SortExpression="TOTAL" >
                                                                        <ItemTemplate  >
                                                                                <%#Eval("TOTAL")%>
                                                                        </ItemTemplate>                                               
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
                                                    <td colspan="9" valign ="top"  >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr style="padding-top : 4pt; padding-bottom : 4pt">                                                                                                                                                
                                                                          <td style="width: 30%; height: 34px;" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="3" Width="100px" ReadOnly="True" Text ="0" Visible="True"></asp:TextBox></td>
                                                                          <td style="width: 25%; height: 34px;" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%; height: 34px;" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%; height: 34px;" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
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

            
        </table>     
    </form>
    
</body>
</html>