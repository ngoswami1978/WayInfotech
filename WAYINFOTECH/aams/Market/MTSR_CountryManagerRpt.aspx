<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MTSR_CountryManagerRpt.aspx.vb" Inherits="Market_MTSR_CountryManagerRpt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Country Manager Report</title>
     <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>
    <script type="text/javascript" src="../Calender/calendar.js"></script>
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
 <script type="text/javascript" src="../Calender/calendar-setup.js"></script>
 <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
 
 <script type="text/javascript" language="javascript" >
 
 function PopupAgencyPage()
 {
    var type;
    type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
    return false;
    
 }
 
 function ValidateCMR()
 { 
 
if(document.getElementById('<%=txtLcode.ClientID%>').value.trim()!="")
      {
        if(IsDataValid(document.getElementById("txtLcode").value,12)==false)
        {
            document.getElementById('lblError').innerHTML='Invalid Lcode.';             
            document.getElementById("txtLcode").focus();
            return false;
         }                  
      }
      
      if(document.getElementById('<%=txtChainCode.ClientID%>').value.trim()!="")
      {
        if(IsDataValid(document.getElementById("txtChainCode").value,12)==false)
        {
            document.getElementById('lblError').innerHTML='Invalid Chain code.';             
            document.getElementById("txtChainCode").focus();
            return false;
         }                  
      }
              
 if(window.document.getElementById("txtDate").value=='')
 {
 window.document.getElementById("lblError").innerHTML="Date From is Mandatory";
 return false;
 }
 if(window.document.getElementById("txtDate").value!='')
 {
     if (isDate(window.document.getElementById("txtDate").value.trim(),"dd/MM/yyyy")==false)
    {
    document.getElementById("lblError").innerText="Invalid date format";
    document.getElementById("txtDate").focus();
    return false;

    }
}
 
 }
 </script>
 
</head>
<body>
    <form id="form1" defaultfocus="btnSearch" runat="server">
    
    
    
    <table cellpadding="0" cellspacing="0" border="0">
    <tr>
    <td>
    <!-- Code for Search Criteria -->
    
    <table width="860px" align="left" class="border_rightred" id="TABLE1">
            <tr>
                <td valign="top" style="width: 850px;">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Market-></span><span class="sub_menu">Country Manager Report</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" style="height: 20px">
                                Country Manager Report</td>
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
                                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" height="25" width="6%">
                                                                </td>
                                                                <td class="textbold" style="width: 116px">
                                                                    Country</td>
                                                                <td style="width: 249px">
                                                                    <asp:DropDownList ID="drpCountry" TabIndex="1" runat="server"  onkeyup="gotop(this.id)" CssClass="dropdownlist"
                                                                        Width="184px">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 132px">
                                                                    City</td>
                                                                <td style="width: 223px">
                                                                    <asp:DropDownList ID="drpCity1" runat="server" TabIndex="1"  onkeyup="gotop(this.id)" CssClass="dropdownlist"
                                                                        Width="184px">
                                                                    </asp:DropDownList></td>
                                                                <td style="height: 25px; width: 176px;">
                                                                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td width="6%" class="textbold" height="25px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 116px">
                                                                    Agency Name
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="txtAgencyName" CssClass="textbox" MaxLength="40" runat="server"
                                                                        TabIndex="1" Width="94%"></asp:TextBox>&nbsp;
                                                                    <img src="../Images/lookup.gif" alt="" tabindex="1" onclick="javascript:return PopupAgencyPage();" style="cursor:pointer;"  /></td>
                                                                <td style="width: 176px">
                                                                    <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="21" Text="Export"  AccessKey="E"/></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px">
                                                                </td>
                                                                <td class="textbold" style="height: 25px; width: 116px;">
                                                                    1 AOffice</td>
                                                                <td style="height: 25px; width: 249px;">
                                                                    <asp:DropDownList TabIndex="1" ID="drpAOffice1" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                        Width="184px">
                                                                        
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="textbold" style="height: 25px; width: 132px;">
                                                                    Region</td>
                                                                <td style="height: 25px; width: 223px;">
                                                                    <asp:DropDownList ID="drpRegion" runat="server" onkeyup="gotop(this.id)" TabIndex="1" CssClass="dropdownlist"
                                                                        Width="184px" Style="position: relative">
                                                                    </asp:DropDownList></td>
                                                                <td style="height: 25px">
                                                                    <asp:Button ID="btnReset" TabIndex="20" CssClass="button" runat="server" Text="Reset"
                                                                         AccessKey="R" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 116px; height: 25px">
                                                                    Lcode</td>
                                                                <td style="width: 249px; height: 25px">
                                                                    <asp:TextBox ID="txtLcode" runat="server" CssClass="textbox" Width="176px" TabIndex="1"></asp:TextBox></td>
                                                                <td class="textbold" style="width: 132px; height: 25px">
                                                                    Chain Code</td>
                                                                <td style="width: 223px; height: 25px">
                                                                    <asp:TextBox ID="txtChaincode" runat="server" CssClass="textbox" Width="176px" TabIndex="1"></asp:TextBox></td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 116px; height: 25px">
                                                                    From&nbsp;
                                                                </td>
                                                                <td style="width: 249px; height: 25px">
                                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="textbox" Width="176px" TabIndex="1"></asp:TextBox><span class="Mandatory" >*</span>
                                                                     <img id="imgReceivedFrom" alt="" src="../Images/calender.gif" TabIndex="14" title="Date selector" style="cursor: pointer" />
                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDate.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgReceivedFrom",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                    </td>
                                                                <td class="textbold" style="width: 132px; height: 25px">
                                                                    </td>
                                                                <td style="height: 25px; width: 223px;">
                                                        <input type="hidden" id="hdAgencyNameId" runat="server" value="" style="width: 5px" />
                                                                   
                                                                </td>
                                                                <td style="height: 25px">
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 25px">
                                                                </td>
                                                                <td colspan="3" class="ErrorMsg" style="height: 25px">
                                                                Field Marked * are Mandatory
                                                                </td>
                                                                <td style="width: 223px; height: 25px">
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
                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            
           
        </table>
        
    </td>
    <td></td>
    
    </tr>
    
    <tr>
    <td colspan="2">
    <!-- Code for Search Result Gridview & Paging -->
    <table cellpadding="0" cellspacing="0" border="0">
     <tr id="rowData" runat="server" visible="false"  >
                                    <td class="redborder">
                                    <table  width="100%" border="0"  cellspacing="0" cellpadding="0">
                                    <tr >
                                    <td>
                                   <asp:GridView AllowSorting="True" HeaderStyle-Wrap="false"  HeaderStyle-ForeColor="white"  ID="grdCmr" FooterStyle-HorizontalAlign="right" ShowFooter="True"  runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                            Width="3000px" FooterStyle-CssClass="Gridheading" FooterStyle-VerticalAlign="top"    HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                            AlternatingRowStyle-CssClass="lightblue">
                                                              <Columns>
                                                         
                                                         <asp:TemplateField SortExpression="LCODE" HeaderText="LCode" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblLcode" runat="server" Text='<%# Eval("LCODE") %>'></asp:Label>
                                                         <asp:HiddenField ID="hdMNC" runat="server" Value='<%# Eval("MNC") %>'/>
                                                         </ItemTemplate>
                                                         <ItemStyle Width="70px"/>
                                                         </asp:TemplateField>     
                                                           
                                                            <asp:TemplateField SortExpression="CHAIN_CODE"  HeaderText="Chain Code">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblChainCode" runat="server" Text='<%# Eval("CHAIN_CODE") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Width="50px" />
                                                         </asp:TemplateField>  
                                                         
                                                            <asp:TemplateField SortExpression ="NAME" HeaderText="Agency Name" >
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblAgencyName" runat="server" Text='<%# Eval("NAME") %>'></asp:Label>
                                                         </ItemTemplate>
                                                                <HeaderStyle Width="150px" />
                                                         <ItemStyle Width="150px"  />
                                                                
                                                         </asp:TemplateField>    
                                                          
                                                          <asp:TemplateField  SortExpression ="CITY" HeaderText="City">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblCity" runat="server" Text='<%# Eval("CITY") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Width="50px"  />
                                                         </asp:TemplateField>    
                                                        
                                                          <asp:TemplateField SortExpression="OFFICEID"  HeaderText="Office ID">
                                                         <ItemTemplate>
                                                         <asp:Label ID="lblOfficeID" runat="server" Text='<%# Eval("OFFICEID") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle Width="50px"  />
                                                         </asp:TemplateField>    
                                                         
                                                       <asp:BoundField DataField="ONLINE_STATUS" HeaderText="Online Status"  SortExpression="ONLINE_STATUS">
                                                       <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                           </asp:BoundField>
                                                       
                                                       <asp:BoundField   DataField="MIDTTOTAL_PMONTH" SortExpression="MIDTTOTAL_PMONTH">
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                         
                                                       </asp:BoundField>
                                                       <asp:BoundField  DataField="MIDTTOTAL_P2MONTH" SortExpression="MIDTTOTAL_P2MONTH" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                          
                                                       </asp:BoundField>
                                                       <asp:BoundField  DataField="MIDTTOTAL_P3MONTH" SortExpression="MIDTTOTAL_P3MONTH" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                         </asp:BoundField>
                                                       <asp:BoundField   DataField="MIDTTOTAL_P4MONTH" SortExpression="MIDTTOTAL_P4MONTH" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                          
                                                       </asp:BoundField>
                                                       
                                                       
                                                       
                                                       <asp:BoundField DataField="PROJECTION_CUR_MONTH" SortExpression="PROJECTION_CUR_MONTH"  >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                          
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="NETBOOKINGS_CUR_MONTH" SortExpression="NETBOOKINGS_CUR_MONTH" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                         
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="NETBOOKINGS_PREV_MONTH" SortExpression="NETBOOKINGS_PREV_MONTH" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                         
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="BIDT2_NETBOOKINGS" SortExpression="BIDT2_NETBOOKINGS" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                         
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="SEGS_COMP_TO_HW" SortExpression="SEGS_COMP_TO_HW" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                         
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="SEGS_COMP_TO_LINES" SortExpression="SEGS_COMP_TO_LINES" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                         
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="AVG_SEGSHW_SEGS" SortExpression="AVG_SEGSHW_SEGS"  >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                         
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="AVG_SEGSLINE_SEGS" SortExpression="AVG_SEGSLINE_SEGS" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                         
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="AVE_SEGSBIDT_2" SortExpression="AVE_SEGSBIDT_2">
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                         
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="MKTSHDIFF_1_2" SortExpression="MKTSHDIFF_1_2"  >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                         
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="MKTSHDIFF_2_3" SortExpression="MKTSHDIFF_2_3" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                         
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="MKTSHDIFF_2_4" SortExpression="MKTSHDIFF_2_4" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                         
                                                       </asp:BoundField>
                                                       
                                                       
                                                       <asp:BoundField DataField="A_PREVPER" SortExpression="A_PREVPER" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                         
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="A_PREV2PER" SortExpression="A_PREV2PER" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                         
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="A_PREV3PER" SortExpression="A_PREV3PER" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                         
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="A_PREV4PER" SortExpression="A_PREV4PER" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                         
                                                       </asp:BoundField>
                                                       
                                                       
                                                       <asp:BoundField DataField="A_PREV" SortExpression="A_PREV" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                         
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="A_PREV2" SortExpression="A_PREV2" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                         
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="A_PREV3" SortExpression="A_PREV3" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                         
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="A_PREV4" SortExpression="A_PREV4" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" />
                                                         
                                                       </asp:BoundField>
                                                       
                                                       
                                                       
                                                       <asp:BoundField DataField="B_PREV" SortExpression="B_PREV"  >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" /> 
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="B_PREV2" SortExpression="B_PREV2" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" /> 
                                                       </asp:BoundField>
                                                       <asp:BoundField SortExpression="B_PREV3" DataField="B_PREV3" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" /> 
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="B_PREV4" SortExpression="B_PREV4" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" /> 
                                                       </asp:BoundField>
                                                       
                                                       
                                                       <asp:BoundField DataField="G_PREV" SortExpression="G_PREV" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" /> 
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="G_PREV2" SortExpression="G_PREV2" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" /> 
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="G_PREV3" SortExpression="G_PREV3" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" /> 
                                                       </asp:BoundField>
                                                       <asp:BoundField  SortExpression="G_PREV4" DataField="G_PREV4" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" /> 
                                                       </asp:BoundField>
                                                       
                                                       
                                                       <asp:BoundField DataField="P_PREV" SortExpression="P_PREV" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" /> 
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="P_PREV2" SortExpression="P_PREV2" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" /> 
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="P_PREV3" SortExpression="P_PREV3" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" /> 
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="P_PREV4" SortExpression="P_PREV4" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" /> 
                                                       </asp:BoundField>
                                                       
                                                       
                                                       <asp:BoundField DataField="W_PREV" SortExpression="W_PREV"  >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" /> 
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="W_PREV2" SortExpression="W_PREV2" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" /> 
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="W_PREV3" SortExpression="W_PREV3" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" /> 
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="W_PREV4" SortExpression="W_PREV4">
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" /> 
                                                       </asp:BoundField>
                                                       
                                                        <asp:BoundField DataField="TARGET" SortExpression="Target" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" /> 
                                                       </asp:BoundField>
                                                       
                                                       
                                                       <asp:BoundField DataField="NO_OF_1A_PIV_HW" SortExpression="NO_OF_1A_PIV_HW" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" /> 
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="NO_OF_1A_OLD_HW" SortExpression="NO_OF_1A_OLD_HW">
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" /> 
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="NO_OF_PTYPE_HW" SortExpression="NO_OF_PTYPE_HW"  >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" /> 
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="MISC" SortExpression="MISC" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Right" /> 
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="SALESEXECUTIVE" SortExpression="SALESEXECUTIVE" >
                                                           <ItemStyle Width="150px" HorizontalAlign="Left" /> 
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="STATION" SortExpression="STATION" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Left" /> 
                                                       </asp:BoundField>
                                                       
                                                       <asp:BoundField DataField="MNC" SortExpression="MNC" HeaderText="Agency Type" >
                                                           <ItemStyle Width="50px" HorizontalAlign="Left"  /> 
                                                       </asp:BoundField>
                                                       
                                                       </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left"  ForeColor="White"  />
                                                            <RowStyle CssClass="textbold" />
                                                                                       <FooterStyle CssClass="Gridheading" HorizontalAlign="Right" />
                                   </asp:GridView>
                                   
                                    </td>
                                    </tr>
                                    
                                    
                                    <tr>
            
            <td valign ="top" align="left" style="height: 69px" >
            
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
            </table >
                                    </td>
                                    </tr>
    </table> 
    </td>
    </tr>
    
    </table>


     
    </form>
</body>
</html>
