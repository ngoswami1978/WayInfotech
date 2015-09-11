<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TASR_GroupCaseChangeView.aspx.vb" Inherits="TravelAgency_TASR_GroupCaseChangeView" %>
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
                    document.getElementById("lblError").innerHTML='Maximun number of years should be 4 years.';          
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
<body  >
    <form id="form1"  runat="server" >    
     <table width="860px" align="left" class="border_rightred">
            <tr >
                <td valign="top"  style="width:860px;" >
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Agency -&gt;</span><span class="sub_menu">Group Case</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" style ="width:900px;" >
                                Group Case</td>
                        </tr>
                         <tr>
                            <td align="right" style ="width:860px;"><%--<a href="#" class="LinkButtons" onclick="PrintHistory();">Print</a>&nbsp;&nbsp;--%>&nbsp;
                                <a href="#" class="LinkButtons" onclick="window.close();">Close</a>&nbsp;&nbsp;&nbsp;</td>
                        </tr>
                         <tr>
                            <td valign="top" style="height: 420px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT"  valign="top" >
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                            <td style="width: 900px;" class="redborder" valign="top" >
                                                                 <table border="0" cellpadding="2" cellspacing="1" style="width: 845px;" class="left">
                                                    <tr>
                                                        <td class="center" colspan="8"  style ="width:860px;" >
                                                          <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                    
                                                      <td  align ="right"  colspan="8" style="width: 860px;" >   <asp:Button ID="btnShowSummary" runat="server" CssClass="button" TabIndex="24" Text="Show Summary" Width="97px"  AccessKey ="S" />
                                                         <asp:Button
                                                            ID="btnExport" runat="server" CssClass="button" TabIndex="24" Text="Export" Width="97px"  AccessKey ="E" /></td>
                                                   
                                                    </tr> 
                                                    <tr>
                                                      <td></td>
                                                      <td ><span  class="textbold"><b>No. of records found&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="3" Width="100px" ReadOnly="True" Text ="0"></asp:TextBox></b></span></td>
                                                    <td> </td>
                                                     <td >  &nbsp;</td>                                                      
                                                       <td colspan ="2"  align ="right">  &nbsp;&nbsp;</td>
                                                    <td colspan="2"> 
                                                      </td> 
                                                    </tr> 
                                                       <tr>
                                                        <td  valign="top"></td>  
                                                                 <td  colspan ="7"  valign="top">
                                                                  <asp:Panel ID="PnlDetails" runat ="server" Width ="100%" >
                                                                        <asp:GridView ID="GvDetails" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                                                                Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"  ShowFooter ="true" 
                                                                                                AlternatingRowStyle-CssClass="lightblue" >
                                                                                                  <Columns>                                                                   
                                                                                                         <asp:BoundField DataField="AGENCYNAME" HeaderText="Agency Name"  ItemStyle-Wrap="True"  HeaderStyle-Wrap="false"  ItemStyle-Width="200px" >
                                                                                                             <ItemStyle Width="200px" Wrap="True" />
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>                                                                    
                                                                                                         <asp:BoundField DataField="ADDRESS" HeaderText="Address" ItemStyle-Wrap="True"  HeaderStyle-Wrap="false"  ItemStyle-Width="250px" >
                                                                                                             <ItemStyle Width="250px" Wrap="True" />
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>                                                                    
                                                                                                         <asp:BoundField  DataField="ONLINE" HeaderText="Online"   ItemStyle-Wrap="True"  >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                         <asp:BoundField DataField="ONLINEDATE" HeaderText="Online Date"  ItemStyle-Wrap="True"  HeaderStyle-Wrap="false"  >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                         <asp:BoundField DataField="OFFICEID" HeaderText="OfiiceId"  ItemStyle-Wrap="True"   >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                         <asp:BoundField DataField="AGENCYPPC" HeaderText="Agency PC" ItemStyle-Wrap="True"    >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>                                                                   
                                                                                                         <asp:BoundField DataField="APC" HeaderText="1A PC "  ItemStyle-Wrap="True"   >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                         <asp:BoundField DataField="TKTPRINTER" HeaderText="TKT Printer"  ItemStyle-Wrap="True"   >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                         <asp:BoundField DataField="PRINTER" HeaderText="Printer"  ItemStyle-Wrap="True"  >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                         <asp:BoundField DataField="MINREQ" HeaderText="Min Required" ItemStyle-Wrap="True"  >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                          <asp:BoundField DataField="APREV" HeaderText="1A Prev" ItemStyle-Wrap="True"   >
                                                                                                              <HeaderStyle Wrap="False" />
                                                                                                          </asp:BoundField>
                                                                                                          <asp:BoundField DataField="BPREV" HeaderText="1B Prev" ItemStyle-Wrap="True"   >
                                                                                                              <HeaderStyle Wrap="False" />
                                                                                                          </asp:BoundField>
                                                                                                          <asp:BoundField DataField="GPREV" HeaderText="1G Prev"  ItemStyle-Wrap="True"  >
                                                                                                              <HeaderStyle Wrap="False" />
                                                                                                          </asp:BoundField>
                                                                                                          <asp:BoundField DataField="PPREV" HeaderText="1P Prev"  ItemStyle-Wrap="True"   >
                                                                                                              <HeaderStyle Wrap="False" />
                                                                                                          </asp:BoundField>
                                                                                                          <asp:BoundField DataField="WPREV" HeaderText="1W Prev" ItemStyle-Wrap="True"   >
                                                                                                              <HeaderStyle Wrap="False" />
                                                                                                          </asp:BoundField>
                                                                                                         <asp:BoundField DataField="POTENPCURR" HeaderText="Potential Prev"  ItemStyle-Wrap="True"  >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                          <asp:BoundField DataField="ACURR" HeaderText="1A Curr" ItemStyle-Wrap="True"  >
                                                                                                              <HeaderStyle Wrap="False" />
                                                                                                          </asp:BoundField>
                                                                                                          <asp:BoundField DataField="BCURR" HeaderText="1B Curr" ItemStyle-Wrap="True"  >
                                                                                                              <HeaderStyle Wrap="False" />
                                                                                                          </asp:BoundField>
                                                                                                          <asp:BoundField DataField="GCURR" HeaderText="1G Curr"  ItemStyle-Wrap="True"  >
                                                                                                              <HeaderStyle Wrap="False" />
                                                                                                          </asp:BoundField>
                                                                                                          <asp:BoundField DataField="PCURR" HeaderText="1P Curr"   ItemStyle-Wrap="True"  >
                                                                                                              <HeaderStyle Wrap="False" />
                                                                                                          </asp:BoundField>
                                                                                                          <asp:BoundField DataField="WCURR" HeaderText="1W Curr"  ItemStyle-Wrap="True"   >
                                                                                                              <HeaderStyle Wrap="False" />
                                                                                                          </asp:BoundField>
                                                                                                          <asp:BoundField DataField="POTENCURR" HeaderText="Potential Curr"  ItemStyle-Wrap="True"  >
                                                                                                              <HeaderStyle Wrap="False" />
                                                                                                          </asp:BoundField>
                                                                                                          
                                                                                                         <asp:BoundField DataField="JAN_PROD" HeaderText="JAN_PROD" ItemStyle-Wrap="True"  >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                         <asp:BoundField DataField="FEB_PROD" HeaderText="FEB_PROD" ItemStyle-Wrap="True"  >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                         <asp:BoundField DataField="MAR_PROD" HeaderText="MAR_PROD"  ItemStyle-Wrap="True" >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                         <asp:BoundField DataField="APR_PROD" HeaderText="APR_PROD"  ItemStyle-Wrap="True" >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                         <asp:BoundField DataField="MAY_PROD" HeaderText="MAY_PROD" ItemStyle-Wrap="True"   >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                         <asp:BoundField DataField="JUN_PROD" HeaderText="JUN_PROD" ItemStyle-Wrap="True"  >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                         <asp:BoundField DataField="JUL_PROD" HeaderText="JUL_PROD" ItemStyle-Wrap="True"  >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                         <asp:BoundField DataField="AUG_PROD" HeaderText="AUG_PROD" ItemStyle-Wrap="True"  >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                         <asp:BoundField DataField="SEP_PROD" HeaderText="SEP_PROD"  ItemStyle-Wrap="True"  >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                         <asp:BoundField DataField="OCT_PROD" HeaderText="OCT_PROD" ItemStyle-Wrap="True"  >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                         <asp:BoundField DataField="NOV_PROD" HeaderText="NOV_PROD" ItemStyle-Wrap="True"  >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                         <asp:BoundField DataField="DEC_PROD" HeaderText="DEC_PROD" ItemStyle-Wrap="True"   >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                         
                                                                                                         <asp:BoundField DataField="AVGCURR" HeaderText="1A Avg Curr"  ItemStyle-Wrap="True"  >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                         <asp:BoundField DataField="AVGPREV" HeaderText="1A Avg Prev" ItemStyle-Wrap="True"   >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                         <asp:BoundField DataField="EFFICIENCY" HeaderText="Efficiency" ItemStyle-Wrap="True"  >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                         <asp:BoundField DataField="DEFICIT" HeaderText="Deficit"  ItemStyle-Wrap="True"  >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                         <asp:BoundField DataField="DEFICITPER" HeaderText="Deficit %" ItemStyle-Wrap="True"   >
                                                                                                             <HeaderStyle Wrap="False" />
                                                                                                         </asp:BoundField>
                                                                                                         
                                                                                                </Columns>
                                                                                                <AlternatingRowStyle CssClass="lightblue" />
                                                                                                <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False"  />
                                                                                                <FooterStyle CssClass="Gridheading" HorizontalAlign="Right" Wrap="False" />
                                                                                                <RowStyle CssClass="textbold" />
                                                                       </asp:GridView>                             
                                                                   </asp:Panel>                                    
                                                                 </td>
                                                                 <td></td>
                                                            </tr> 
                                                               <tr>
                                                        <td></td>  
                                                                 <td  colspan ="7">
                                                                  <asp:Panel ID="PnlSummary" runat ="server" Width ="100%"  Visible ="false"  >
                                                                        <asp:GridView ID="GvSummary" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                                                                Width="400px" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                                                                AlternatingRowStyle-CssClass="lightblue" Caption="Group Summary" >
                                                                                                  <Columns>                                                                   
                                                                                                       <asp:BoundField DataField="Name"   ItemStyle-Wrap="True" >
                                                                                                           <HeaderStyle Wrap="False" />
                                                                                                       </asp:BoundField>
                                                                                                          <asp:BoundField DataField="Data" ItemStyle-Wrap="True"   >
                                                                                                              <HeaderStyle Wrap="False" />
                                                                                                          </asp:BoundField>
                                                                                                </Columns>
                                                                                                <AlternatingRowStyle CssClass="lightblue" />
                                                                                                <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False"  />
                                                                                                <RowStyle CssClass="textbold" />
                                                                       </asp:GridView>                             
                                                                   </asp:Panel>                                    
                                                                 </td>
                                                                 <td></td>
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

