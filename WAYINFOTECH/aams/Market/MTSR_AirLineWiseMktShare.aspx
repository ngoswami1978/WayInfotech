<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MTSR_AirLineWiseMktShare.aspx.vb" Inherits="Productivity_PDSR_AirLineWiseMktShare" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
   <title>AAMS: Productivity</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>
   <script type="text/javascript" language="javascript">
   
   
      function DetailFunction(FMonth,TMonth,FYear,TYear,LimAoff,LimReg,LimOwnOff,AirCode,BreakupType,City,Country,Region,Aoff,OnCarr,GType)
     {
         var parameter="Fmonth=" + FMonth  + "&TMonth=" + TMonth + "&FYear=" + FYear +  "&TYear=" + TYear +   "&LimAoff=" +  LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff + "&AirCode=" + AirCode + "&BreakupType=" + BreakupType + "&City=" + City + "&Country=" + Country  + "&Region=" + Region   +  "&Aoff=" + Aoff  + "&OnCarr=" + OnCarr + "&GType=" + GType  ;
         type="MTSR_AirLineWiseMktShareDetails.aspx?Popup=T&" + parameter;   
         window.open(type,"AirLineDetials","height=600,width=920,top=30,left=20,scrollbars=1,status=1");     
         return false;        
                                           
     }
          
         function GraphFunction(FMonth,TMonth,FYear,TYear,LimAoff,LimReg,LimOwnOff,AirCode,BreakupType,City,Country,Region,Aoff,OnCarr,GType)
     {
         var parameter="Case=AirlineWiseGraph&Fmonth=" + FMonth  + "&TMonth=" + TMonth + "&FYear=" + FYear +  "&TYear=" + TYear +   "&LimAoff=" +  LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff + "&AirCode=" + AirCode +  "&BreakupType=" + BreakupType + "&City=" + City + "&Country=" + Country  + "&Region=" + Region   +  "&Aoff=" + Aoff  +  "&OnCarr=" + OnCarr  + "&GType=" + GType +  "&Param=1"   ;
         type="../RPSR_ReportShow.aspx?Popup=T&" + parameter;   
         window.open(type,"AirLineDetials","height=600,width=920,top=30,left=20,scrollbars=1,status=1");     
         return false;        
                                           
     }
     
   
    function SearchValidate()
    {
        if(document.getElementById("drpAirLine").selectedIndex=='0')
        {
        document.getElementById("lblError").innerHTML="Airline Name is Mandatory";
        return false;
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

function chkShowBreakup()
{

if(document.getElementById("chkShowBr").checked==true)
{
  document.getElementById("rdShobrBookings").style.display ="block";  
  
        // document.getElementById("rdShobrBookings").style.display ="visible";   
}
else
        {
            document.getElementById("rdShobrBookings").style.display ="none";  
//    document.getElementById("rdShobrBookings").style.visibility='hidden';
        }
}
   </script>
</head>


<body >
    <form id="form1" runat="server">
      <table width="860px" align="left" class="border_rightred">
            <tr >
                <td valign="top"  style="width:860px; height: 245px;" >
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Market-></span><span class="sub_menu">Airline Wise Market Share</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Airline wise Market Share
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
                                                          <table width="100%" border="0"   align="left" cellpadding="0" cellspacing="0">
                                                          
                        
                                                <tr>
                                                    <td class="textbold" colspan="7" align="center" style="height: 15px">
                                                        &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                              <tr>
                                                                  <td class="textbold" height="25" width="6%">
                                                                  </td>
                                                                  <td class="textbold" style="width: 162px">
                                                        Country</td>
                                                                  <td style="width: 172px" ><asp:DropDownList ID="drpCountry" runat="server" CssClass="dropdownlist" Width="227px" TabIndex="1">
                                                                  </asp:DropDownList></td>
                                                                  <td class="textbold" style="width: 115px">
                                                                  </td>
                                                                  <td  class="textbold" style="width: 115px">
                                                        City</td>
                                                                  <td style="width: 236px" ><asp:DropDownList ID="drpCity" runat="server" CssClass="dropdownlist" Width="227px" TabIndex="2">
                                                                  </asp:DropDownList></td>
                                                                  <td style="width: 176px" align="center">
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search"  AccessKey="A"/></td>
                                                              </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold" style="width: 162px">
                                                                        Air Line</td>
                                                                    <td style="width: 172px"><asp:DropDownList ID="drpAirLine" runat="server" CssClass="dropdownlist" Width="227px" TabIndex="1">
                                                                    </asp:DropDownList></td>
                                                                    <td class="textbold" style="width: 115px">
                                                                    </td>
                                                                    <td class="textbold" style="width: 115px" >
                                                        Region</td>
                                                                    <td style="width: 236px">
                                                                         

                                                                        <asp:DropDownList ID="drpRegion" runat="server" CssClass="dropdownlist" Width="227px" TabIndex="18">
                                                                        </asp:DropDownList></td>
                                                                    <td align="center">
                                                                        <asp:Button ID="btnExport" runat="server" CssClass="button" Text="Export" AccessKey="E" /></td>
                                                                </tr>
                                                              <tr>
                                                                  <td class="textbold" style="height: 25px">
                                                                  </td>
                                                                  <td class="textbold" style="width: 162px">
                                                                      1a Office</td>
                                                                  <td style="width: 172px">
                                                                      <asp:DropDownList ID="drpOneAoffice" runat="server" CssClass="dropdownlist" Width="227px" TabIndex="1">
                                                                      </asp:DropDownList></td>
                                                                  <td class="textbold" style="width: 115px">
                                                                  </td>
                                                                  <td class="textbold" style="width: 115px">
                                                                      Carrier Type</td>
                                                                  <td style="width: 236px"><asp:DropDownList ID="drpCarrierType" runat="server" CssClass="dropdownlist" Width="227px" TabIndex="18">
                                                                  <asp:ListItem Text="--All--" Value="1" Selected=True ></asp:ListItem>
                                                                  <asp:ListItem Text="Online" Value="2" ></asp:ListItem>
                                                                  <asp:ListItem Text="Offline" Value="3" ></asp:ListItem>
                                                                  </asp:DropDownList></td>
                                                                  <td align="center">
                                                                  <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" AccessKey="R" /></td>
                                                              </tr>
                                                              <tr id="trgrouptype" runat="server"  >
                                                                  <td class="textbold" style="height: 25px">
                                                                  </td>
                                                                  <td class="textbold" style="width: 162px">
                                                                      Group Type</td>
                                                                  <td style="width: 172px">
                                                                      <asp:DropDownList ID="drpLstGroupType" runat="server" CssClass="dropdown" onkeyup="gotop(this.id)"
                                                                          TabIndex="4" Width="227px">
                                                                      </asp:DropDownList></td>
                                                                  <td class="textbold" style="width: 115px">
                                                                  </td>
                                                                  <td class="textbold" style="width: 115px">
                                                                  </td>
                                                                  <td style="width: 236px">
                                                                  </td>
                                                                  <td align="center">
                                                                  </td>
                                                              </tr>
                                                              <tr>
                                                                  <td class="textbold" style="height: 30px">
                                                                  </td>
                                                                  <td class="textbold" align="center" colspan="2" style="height: 30px">
                                                                      &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; 
                                                                      <asp:CheckBox ID="chkShowBr" runat="server" Checked=true Text="Show Breakup of Bookings" /></td>
                                                                  <td class="textbold" colspan="1" style="height: 30px">
                                                                  </td>
                                                                 
                                                                  <td class="textbold" colspan="2" style="height: 30px">
                                                                      <asp:RadioButtonList ID="rdShobrBookings" runat="server" RepeatDirection="Horizontal">
                                                                          <asp:ListItem Value="1" Selected=true >Show Total</asp:ListItem>
                                                                          <asp:ListItem Value="2">Show Average</asp:ListItem>
                                                                          </asp:RadioButtonList></td>
                                                                  <td style="height: 30px">
                                                                  </td>
                                                              </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 18px">
                                                                    </td>
                                                                    <td class="subheading" colspan="2" style="height: 18px" >
                                                                        Date From</td>
                                                                    <td class="subheading" colspan="1" style="height: 18px">
                                                                    </td>
                                                                        <td class="subheading" colspan="2" style="height: 18px" >
                                                                        Date To</td>
                                                                        
                                                                        <td style="height: 18px">
                                                                       
                                                                        
                                                                        </td>
                                                                </tr>
                                                              
                                                              <tr>
                                                                  <td class="textbold" style="height: 25px">
                                                                  </td>
                                                                  <td class="textbold" style="width: 162px; height: 25px">
                                                                      Month &nbsp;</td>
                                                                  <td style="width: 172px; height: 25px">
                                                                        <asp:DropDownList ID="drpMonthF" runat="server" CssClass="dropdownlist" Width="104px" TabIndex="19">
                                                                        </asp:DropDownList></td>
                                                                  <td class="textbold" style="width: 115px; height: 25px">
                                                                  </td>
                                                                  <td class="textbold" style="width: 115px; height: 25px">
                                                                      Month</td>
                                                                  <td style="width: 236px; height: 25px">
                                                                        <asp:DropDownList ID="drpMonthTo" runat="server" CssClass="dropdownlist" Width="104px" TabIndex="19">
                                                                        </asp:DropDownList></td>
                                                                  <td style="height: 25px">
                                                                  </td>
                                                              </tr>
                                                                
                                                                <tr>
                                                                    <td class="textbold" style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 25px; width: 162px;">
                                                                        Year</td>
                                                                    <td style="height: 25px; width: 172px;"><asp:DropDownList ID="drpYearF" runat="server" CssClass="dropdownlist" Width="104px" TabIndex="20">
                                                                    </asp:DropDownList></td>
                                                                    <td class="textbold" style="width: 115px; height: 25px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 25px; width: 115px;">
                                                                        Year</td>
                                                                    <td style="height: 25px; width: 236px;" >
                                                                        <asp:DropDownList ID="drpYearTo" runat="server" CssClass="dropdownlist" Width="104px" TabIndex="20">
                                                                        </asp:DropDownList>&nbsp;</td>
                                                                    <td style="height: 25px" >
                                                                    </td>
                                                                </tr>
                                                              <tr>
                                                                  <td class="textbold" style="height: 25px">
                                                                  </td>
                                                                  <td class="textbold" style="width: 162px; height: 25px">
                                                                  </td>
                                                                  <td style="width: 172px; height: 25px">
                                                                  </td>
                                                                  <td class="textbold" style="width: 115px; height: 25px">
                                                                  </td>
                                                                  <td class="textbold" style="width: 115px; height: 25px">
                                                                  </td>
                                                                  <td style="width: 236px; height: 25px">
                                                                  </td>
                                                                  <td style="height: 25px">
                                                                  </td>
                                                              </tr>
                                                                <tr>
                                                                
                                                                <td colspan="7">
                                                                
                                                                 <asp:GridView EnableViewState ="false" AllowSorting="true" HeaderStyle-ForeColor="white" ID="grdvMktShareDetails"  ShowFooter=true   runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="860px" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue">
                                                              <Columns>
                                                              
                                                              <asp:TemplateField SortExpression="AIRLINE_CODE" HeaderText="Airline Code">
                                                              <ItemTemplate>
                                                              <%# Eval("AIRLINE_CODE") %>
                                                              </ItemTemplate>
                                                              <FooterTemplate>
                                                             </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                              
                                                              <asp:TemplateField SortExpression="AirlineName" HeaderText="Airline Name">
                                                              <ItemTemplate>
                                                              <asp:Label ID="lblAirlineNameVal" runat="server" Text='<%# Eval("AirlineName") %>'></asp:Label>
                                                              </ItemTemplate>
                                                              <FooterTemplate>
                                                             
                                                              </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                                <asp:TemplateField SortExpression="AMADEUS" HeaderText="1A"   ItemStyle-HorizontalAlign="Right">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblAMADEUSVal" runat="server" Text='<%# Eval("AMADEUS") %>' CssClass ="right" ></asp:Label>
                                                              </ItemTemplate>
                                                              <FooterTemplate>
                                                             
                                                              </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                            <asp:TemplateField SortExpression="ABACUS" HeaderText="1B"  ItemStyle-HorizontalAlign="Right">
                                                              <ItemTemplate >
                                                               <asp:Label ID="lblABACUSVal" runat="server" Text='<%# Eval("ABACUS") %>'  CssClass ="right"  ></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                              
                                                              </FooterTemplate>
                                                              </asp:TemplateField>
                                                            <asp:TemplateField SortExpression="GALILEO" HeaderText="1G"  ItemStyle-HorizontalAlign="Right">
                                                              <ItemTemplate>
                                                             <asp:Label ID="lblGALILEOVal" runat="server" Text='<%# Eval("GALILEO") %>'  CssClass ="right" ></asp:Label>
                                                             </ItemTemplate>
                                                              <FooterTemplate>
                                                              
                                                              </FooterTemplate>
                                                              </asp:TemplateField>
                                                           
                                                            <asp:TemplateField SortExpression="WORLDSPAN" HeaderText="1P"  ItemStyle-HorizontalAlign="Right">
                                                              <ItemTemplate>
                                                             <asp:Label ID="lblWORLDSPANVal" runat="server" Text='<%# Eval("WORLDSPAN") %>'  CssClass ="right" ></asp:Label>
                                                             </ItemTemplate>
                                                              <FooterTemplate>
                                                              
                                                                </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                              <asp:TemplateField SortExpression="SABREDOMESTIC" HeaderText="1W"  ItemStyle-HorizontalAlign="Right">
                                                              <ItemTemplate>
                                                             <asp:Label ID="lblSABREDOMESTICVal" runat="server" Text='<%# Eval("SABREDOMESTIC") %>'  CssClass ="right" ></asp:Label>
                                                             </ItemTemplate>
                                                              <FooterTemplate>
                                                              
                                                                </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                              <asp:TemplateField SortExpression="NETBOOKINGS" HeaderText="Netbookings"  ItemStyle-HorizontalAlign="Right">
                                                              <ItemTemplate>
                                                             <asp:Label ID="lblNETBOOKINGSVal" runat="server" Text='<%# Eval("NETBOOKINGS") %>'  CssClass ="right" ></asp:Label>
                                                             </ItemTemplate>
                                                              <FooterTemplate>
                                                              
                                                                </FooterTemplate>
                                                              </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <a href="#" class="LinkButtons" id="lnkDetails" runat="server">Details</a>&nbsp;<a href="#"
                                                                            class="LinkButtons" id="LnkGraph" runat="server">Graph</a>
                                                                       
                                                                        <asp:HiddenField ID="hdACode" runat="server" Value='<%#Eval("AIRLINE_CODE")%>'  />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Wrap="False" Width="30px" />
                                                                </asp:TemplateField>
                                                              
                                               </Columns>
                                                            
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False"  />
                                                            <FooterStyle CssClass="Gridheading right" HorizontalAlign="Right" Wrap="False"  />
                                                            
                                                            <RowStyle CssClass="textbold" />
                                   </asp:GridView>
                                   
                                    <asp:GridView EnableViewState ="false" ID="grdvBreakUpOutPut" AllowSorting="true" HeaderStyle-ForeColor="white"  ShowFooter=true   runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="860px" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue">
                                                              <Columns>
                                                              
                                                               <asp:TemplateField SortExpression="AIRLINE_CODE" HeaderText="Airline Code">
                                                              <ItemTemplate>
                                                              <%# Eval("AIRLINE_CODE") %>
                                                              </ItemTemplate>
                                                              <FooterTemplate>
                                                             </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                              
                                                              
                                                              <asp:TemplateField SortExpression="AirlineName" HeaderText="Airline Name">
                                                              <ItemTemplate>
                                                              <asp:Label ID="lblAirlineNameVal" runat="server" Text='<%# Eval("AirlineName") %>'></asp:Label>
                                                              </ItemTemplate>
                                                              <FooterTemplate>
                                                              </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                                <asp:TemplateField SortExpression="CRS" HeaderText="CRS" ItemStyle-Width="50px">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblCRSVal" runat="server" Text='<%# Eval("CRS") %>'></asp:Label>
                                                              </ItemTemplate>
                                                              <FooterTemplate>
                                                             
                                                              </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                            <asp:TemplateField SortExpression="BOOKINGACTIVE" HeaderText="Booking Active"  ItemStyle-HorizontalAlign="Right">
                                                              <ItemTemplate>
                                                               <asp:Label ID="lblBOOKINGACTIVEVal" runat="server" Text='<%# Eval("BOOKINGACTIVE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                              
                                                              </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                            <asp:TemplateField SortExpression="CANCELACTIVE" HeaderText="Cancel Active"  ItemStyle-HorizontalAlign="Right">
                                                              <ItemTemplate>
                                                             <asp:Label ID="lblCANCELACTIVEVal" runat="server" Text='<%# Eval("CANCELACTIVE") %>'></asp:Label>
                                                             </ItemTemplate>
                                                              <FooterTemplate>
                                                             
                                                              </FooterTemplate>
                                                              </asp:TemplateField>
                                                           
                                                            <asp:TemplateField SortExpression="BOOKINGPASSIVE" HeaderText="Booking Passive"  ItemStyle-HorizontalAlign="Right">
                                                              <ItemTemplate>
                                                             <asp:Label ID="lblBOOKINGPASSIVEVal" runat="server" Text='<%# Eval("BOOKINGPASSIVE") %>'></asp:Label>
                                                             </ItemTemplate>
                                                              <FooterTemplate>
                                                             
                                                                </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                              <asp:TemplateField SortExpression="CANCELPASSIVE" HeaderText="Cancel Passive"  ItemStyle-HorizontalAlign="Right">
                                                              <ItemTemplate>
                                                             <asp:Label ID="lblCANCELPASSIVEVal" runat="server" Text='<%# Eval("CANCELPASSIVE") %>'></asp:Label>
                                                             </ItemTemplate>
                                                              <FooterTemplate>
                                                             
                                                                </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                              <asp:TemplateField SortExpression="LATE" HeaderText="Late"  ItemStyle-HorizontalAlign="Right">
                                                              <ItemTemplate>
                                                             <asp:Label ID="lblLATEVal" runat="server" Text='<%# Eval("LATE") %>'></asp:Label>
                                                             </ItemTemplate>
                                                              <FooterTemplate>
                                                              
                                                                </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                              
                                                              
                                                               <asp:TemplateField SortExpression="NULLACTIVE" HeaderText="Null Active"  ItemStyle-HorizontalAlign="Right">
                                                              <ItemTemplate>
                                                             <asp:Label ID="lblNULLACTIVEVal" runat="server" Text='<%# Eval("NULLACTIVE") %>'></asp:Label>
                                                             </ItemTemplate>
                                                              <FooterTemplate>
                                                              
                                                                </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                              
                                                               <asp:TemplateField SortExpression="NULLPASSIVE" HeaderText="Null Passive"  ItemStyle-HorizontalAlign="Right">
                                                              <ItemTemplate>
                                                             <asp:Label ID="lblNULLPASSIVEVal" runat="server" Text='<%# Eval("NULLPASSIVE") %>'></asp:Label>
                                                             </ItemTemplate>
                                                              <FooterTemplate>
                                                              
                                                                </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                              
                                                               <asp:TemplateField SortExpression="NETBOOKINGS" HeaderText="Netbookings"  ItemStyle-HorizontalAlign="Right">
                                                              <ItemTemplate>
                                                             <asp:Label ID="lblNETBOOKINGSVal" runat="server" Text='<%# Eval("NETBOOKINGS") %>'></asp:Label>
                                                             </ItemTemplate>
                                                              <FooterTemplate>
                                                             
                                                                </FooterTemplate>
                                                              </asp:TemplateField>
                                                              
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <a href="#" class="LinkButtons" id="lnkDetails" runat="server">Details</a>                                                                       
                                                                        <asp:HiddenField ID="hdACode" runat="server" Value='<%#Eval("AIRLINE_CODE")%>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Wrap="False" Width="30px" />
                                                                </asp:TemplateField>
                                               </Columns>
                                                            
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False"  />
                                                            <FooterStyle CssClass="Gridheading right" HorizontalAlign="Right" Wrap="False"  />
                                                            
                                                            <RowStyle CssClass="textbold" />
                                   </asp:GridView>
                                                                </td>
                                                                </tr>
                                                                  <!-- code for paging----->
                                            <tr>                                                   
                                                    <td colspan="7" valign ="top"  >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="800px">
                                                      <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
                                                                      <tr class="paddingtop paddingbottom">                                                                                                                                                
                                                                          <td style="width: 243px" class="left" nowrap="nowrap"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey"  ReadOnly="true"></asp:TextBox></td>
                                                                          <td style="width: 200px" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 356px" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 187px" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                    </td>
                                                </tr>
                                                
            <!-- code for paging----->
                                                                
                                               </table>
                                                        </td>
                                                </tr>
                                                           
                                        </table>
                                        
                                          
                                        </td>
                                    </tr>
                                </table>
                                 <asp:HiddenField ID="hdAMADEUS" runat="server" />
                                                                        <asp:HiddenField ID="hdABACUS" runat="server" />
                                                                        <asp:HiddenField ID="hdGALILEO" runat="server" />
                                                                        <asp:HiddenField ID="hdWORLDSPAN" runat="server" />
                                                                        <asp:HiddenField ID="hdSABREDOMESTIC" runat="server" />
                                                                        <asp:HiddenField ID="hdNETBOOKINGS" runat="server" />
                                                                        
                                <asp:HiddenField ID="hdBOOKINGACTIVE" runat="server" />
                                <asp:HiddenField ID="hdCANCELACTIVE" runat="server" />
                                <asp:HiddenField ID="hdBOOKINGPASSIVE" runat="server" />
                                <asp:HiddenField ID="hdCANCELPASSIVE" runat="server" />
                                <asp:HiddenField ID="hdLATE" runat="server" />
                                <asp:HiddenField ID="hdNULLACTIVE" runat="server" />
                                <asp:HiddenField ID="hdNULLPASSIVE" runat="server" />
                                <asp:HiddenField ID="hdNETBOOKINGSBr" runat="server" />
                                
                                     
                                                     
                                                        </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr valign="top" >
            <td valign="top" style="padding-left:4px; height: 21px;" >
            
            
             
              
                
           
           
           
           
           
                
                
               
                
                
              
              
              
              
                  
            </td>
            </tr>
            <tr>
            
            
            </tr>
        </table>
    </form>
</body>
<script type="text/javascript" language="javascript">
 chkShowBreakup();
</script>
</html>
