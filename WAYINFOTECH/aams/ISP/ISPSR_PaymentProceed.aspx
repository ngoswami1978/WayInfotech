<%@ Page Language="VB" MaintainScrollPositionOnPostback="true"   ValidateRequest="false"  AutoEventWireup="false" CodeFile="ISPSR_PaymentProceed.aspx.vb" Inherits="ISP_ISPSR_PaymentProceed" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <title>Search ISP Payment Proceed</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>     
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
    <script type ="text/javascript" language ="javascript">
    
    
//    function SelectAndUpdateSession()
//    {
//      
//    }


function DoPayment(s)
{
   
   
   if (document.getElementById('hdChechedItem').value=="0")
    {
        document.getElementById('lblError').innerHTML = "There is no item selected to do Payment."
        return false;
    }
   
//    var id;
//    id=s+'|'+id +'|'+ "" + '|'+ "" + '|'+ "Do"; 
//    CallServer(id,"This is context from client");
      
        var type;      
        type = "ISPUP_PaymentReceived.aspx?NewFirtTime=N" ;
        window.open(type,"IspPay","height=400px,width=830px,top=150,left=150,scrollbars=0,status=1");	   	                    
    return false;
}
      function SendAoffice(s,RNo)
   {
      var id
      st=s;    
     
      id=s+'|'+id +'|'+ RNo + '|'+ document.getElementById(s).checked + '|'+ "Check";       
      CallServer(id,"This is context from client");
      
   }
   
    function ReceiveServerData(args, context)
    {        
      var pos=args.split('!'); 
           
            
            
                if(pos[1]=="Check")
                 {
                   if  ( document.getElementById(pos[0]).checked==true)
                          {
                          document.getElementById(pos[0]).checked=true;
                                                   }
                       else
                         {
                       document.getElementById(pos[0]).checked=false;
                         }
                         if(pos[2]=="CheckItemExist")
                         {
                           document.getElementById('hdChechedItem').value=1;
                           }
                           else
                           {
                           document.getElementById('hdChechedItem').value="0";
                           } 
                }                   
                else
                {
                 
                   
                        var type;      
                        type = "ISPUP_PaymentReceived.aspx?TotalAmt=" + pos[2] + "&ISPOrderID=" + pos[3] + "&Month=" + pos[4] + "&Year=" + pos[5] ;
   	                    window.open(type,"IspPay","height=400px,width=830px,top=150,left=150,scrollbars=0,status=1");	
                      //  return false;
                
                }         
            
            
             return false;
			
    }
    function CheckValidation()
    
    {  
     
      if (document.getElementById('drpIspname').selectedIndex==0)
      {
           document.getElementById('lblError').innerHTML='ISP name is mandatory.';
           document.getElementById('drpIspname').focus();
          return false;
      }
    }
      function ActDeAct()
     {
        IE4 = (document.all);
        NS4 = (document.layers);
        var whichASC;
        whichASC = (NS4) ? e.which : event.keyCode;
        if(whichASC!=9)
        {
        document.getElementById("hdAgencyName").value="";
     
        }
    	
     }
     function PopupAgencyPage()
        {

         var type;
         // type = "../Popup/PUSR_Agency.aspx" ;
            type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;
        }
//          function SelectAll()
//        {
//            for(intcnt=1;intcnt<=document.getElementById('<%=gvIspPaymentRec.ClientID%>').rows.length-1;intcnt++)
//            {      
//              
//          
//                  if (  document.getElementById('<%=gvIspPaymentRec.ClientID%>').rows[intcnt].cells[1].children[0].value=='')
//                   {
//                   document.getElementById('<%=gvIspPaymentRec.ClientID%>').rows[intcnt].cells[0].children[0].checked=true;
//                   }
//              
//            }
//         
//               document.getElementById('btnSelectAll').className="displayNone";
//             //  document.getElementById('btnDeSelectAll').className="displayBlock";
//             return false;
//        }
//        function DeSelectAll()
//        {
//            for(intcnt=1;intcnt<=document.getElementById('<%=gvIspPaymentRec.ClientID%>').rows.length-1;intcnt++)
//            {        
//                if (  document.getElementById('<%=gvIspPaymentRec.ClientID%>').rows[intcnt].cells[1].children[0].value=='')
//                   {
//                   document.getElementById('<%=gvIspPaymentRec.ClientID%>').rows[intcnt].cells[0].children[0].checked=false;
//                   }
//               
//            }
//                document.getElementById('btnSelectAll').className="displayBlock";
//               document.getElementById('btnDeSelectAll').className="displayNone";
//             return false;
//        }
        
    </script>
</head>
<body >
    <form id="form1" runat="server" defaultbutton="btnSearch"  defaultfocus="txtAgencyName">
    <table width="850px" class="border_rightred" cellpadding ="0" cellspacing ="0">
            <tr>
                <td valign="top">
                    <table width="850px;" >
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">ISP-&gt;</span><span class="sub_menu">ISP Payment  <asp:Literal ID="ProRec1" runat ="server" ></asp:Literal> Details</span></td>
                        </tr>
                          <tr>
                            <td align="right" style ="width:840px;"><a href="#" id="lnkClose"  runat ="server" class="LinkButtons" onclick="window.close();">Close</a></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" style="width:860px;">
                                <span >ISP Payment <asp:Literal ID="ProRec2" runat ="server" ></asp:Literal></span> Details</td>
                        </tr>                        
                        <tr>
                            <td valign="top" >
                                <table width="850px" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder"  style="width:850px;">
                                        <asp:Panel ID="pnlSearch" runat ="server"   Width ="100%">
                                                <table border="0" cellpadding="2" cellspacing="1"  Width ="100%">
                                                                               <tr>
                                                                                    <td colspan="8" class="center gap"><asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>                                                                        
                                                                                </tr>
                                                                               
                                                                               <tr>
                                                                                    <td style="width:12%" class="textbold" >
                                                                                        &nbsp;</td>
                                                                                    <td style="width:15%" class="textbold"><span class="textbold">Agency Name</span><input type="hidden" id="hdAgencyNameId" runat="server" value="" style="width: 5px" />
                                                                                        <input type="hidden" id="hdAgencyName" runat="server" value="" style="width: 5px" /></td>
                                                                                    <td colspan="4" style="width:66%">
                                                                                        <asp:TextBox ID="txtAgencyName" runat="server" MaxLength="40" Width="383px" TabIndex="1" CssClass="textbox" Height="20px"></asp:TextBox>&nbsp;<img src="../Images/lookup.gif" onclick="javascript:return PopupAgencyPage();" style="cursor:pointer;" /></td>
                                                                                  
                                                                                    <td style="width:16%">
                                                                                       <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="8" AccessKey="A" /></td>
                                                                               </tr> 
                                                                                <tr>
                                                                                    <td style="width:3%" class="textbold" >
                                                                                        &nbsp;</td>
                                                                                    <td style="width:15%" class="textbold"><span class="textbold">City</span>
                                                                                    </td>
                                                                                    <td style="width:19%">
                                                                                        <asp:DropDownList ID="drpCity" runat="server" Width="150px" TabIndex="3" CssClass="dropdownlist" >
                                                                                        </asp:DropDownList></td>
                                                                                     <td style="width:3%" class="textbold" >
                                                                                        &nbsp;</td>
                                                                                    <td style="width:7%"  align="left" class="textbold"><span class="textbold">Country</span>
                                                                                    </td>
                                                                                    <td style="width:20%">
                                                                                        <asp:DropDownList ID="drpCountry" runat="server" Width="150px" TabIndex="4" CssClass="dropdownlist" >
                                                                                        </asp:DropDownList>
                                                                                        </td>    
                                                                                    <td  style="width:3%"><asp:Button ID="btnDisplay" CssClass="button" runat="server" Text="Print" TabIndex="9" AccessKey="P" /></td>                                                                          
                                                                                    <td style="width:33%">
                                                                                       </td>
                                                                                </tr>                                                                                 
                                                                                 <tr>
                                                                                    <td style="width:3%; " class="textbold" >
                                                                                        &nbsp;</td>
                                                                                    <td style="width:15%; " class="textbold"><span class="textbold">ISP Name</span><span class="Mandatory">*</span>
                                                                                    </td>
                                                                                    <td style="width:19%; " colspan="4">
                                                                                        <asp:DropDownList ID="drpIspname" runat="server" Width="384px" TabIndex="5" CssClass="dropdownlist" >
                                                                                        </asp:DropDownList></td>
                                                                                    <td  style="width:3%; "><asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="10" AccessKey="R" /></td>                                                                          
                                                                                    <td style="width:33%; ">
                                                                                       </td>
                                                                                </tr>                                                                               
                                                                                  <tr>
                                                                                    <td style="width:3%;" class="textbold" >
                                                                                        &nbsp;</td>
                                                                                    <td style="width:15%; " class="textbold"><span class ="textbold">Month</span><span class="Mandatory">*</span>
                                                                                      
                                                                                    </td>
                                                                                    <td style="width:19%;"><asp:DropDownList ID="drpMonthFrom" runat="server" Width="150px" TabIndex="6" CssClass="dropdownlist" >
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
                                                                                        </asp:DropDownList></td>
                                                                                     <td style="width:3%;" class="textbold" >
                                                                                        &nbsp;</td>
                                                                                    <td style="width:7%;" class="textbold"> Year<span class="Mandatory">*</span></td>
                                                                                    <td style="width:20%;"><asp:DropDownList ID="drpYearFrom" runat="server" Width="150px" TabIndex="7" CssClass="dropdownlist" >
                                                                                        </asp:DropDownList></td>    
                                                                                    <td  style="width:3%; "></td>                                                                          
                                                                                    <td style="width:33%;">
                                                                                       </td>
                                                                                </tr>                                                                                                                                                     
                                                                                  <tr>
                                                                                    <td style="width:3%" class="textbold" >
                                                                                        &nbsp;</td>
                                                                                    <td style="width:15%" class="textbold">
                                                                                       
                                                                                    </td>
                                                                                    <td style="width:19%">
                                                                                        </td>
                                                                                     <td style="width:3%" class="textbold" >
                                                                                        &nbsp;</td>
                                                                                    <td style="width:7%" align="center" class="textbold"><%--<span class="textbold">To</span><span class="Mandatory">*</span>--%>
                                                                                    </td>
                                                                                    <td style="width:20%"><%--<asp:DropDownList ID="drpMonthTo" runat="server" Width="100px" TabIndex="7" >
                                                                                      <asp:ListItem Value="1">January</asp:ListItem>
                                                                                            <asp:ListItem Value="2">February</asp:ListItem>
                                                                                            <asp:ListItem Value="3">March</asp:ListItem>
                                                                                            <asp:ListItem Value="4">April</asp:ListItem>
                                                                                            <asp:ListItem Value="5">May</asp:ListItem>
                                                                                            <asp:ListItem Value="6">June</asp:ListItem>
                                                                                            <asp:ListItem Value="7">July</asp:ListItem>
                                                                                            <asp:ListItem Value="8t">August</asp:ListItem>
                                                                                            <asp:ListItem Value="9">september</asp:ListItem>
                                                                                            <asp:ListItem Value="10">October</asp:ListItem>
                                                                                            <asp:ListItem Value="11">November</asp:ListItem>
                                                                                            <asp:ListItem Value="12">December</asp:ListItem>
                                                                                        </asp:DropDownList>&nbsp;&nbsp;<asp:DropDownList ID="drpYearTo" runat="server" Width="100px" TabIndex="8" >
                                                                                        </asp:DropDownList>   --%>                                                                           
                                                                                        </td>    
                                                                                    <td  style="width:3%"></td>                                                                          
                                                                                    <td style="width:33%">
                                                                                       </td>
                                                                                </tr> 
                                                                                <tr>
                                                                                    <td class="textbold" >
                                                                                        &nbsp;</td>
                                                                                   <td  >
                                                                                        </td>
                                                                                    <td  colspan="3" class="ErrorMsg" >
                                                                                        Field Marked * are Mandatory</td>
                                                                                   
                                                                                    <td  colspan ="3"><asp:HiddenField ID="hdMonth" runat ="server" /><asp:HiddenField ID="hdYear" runat ="server" />
                                                                                        &nbsp;</td>
                                                                                </tr>
                                                                            </table>
                                         
                                         </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                    </tr>
                                   
                                      <tr>
                                                    <td colspan ="8" align ="right">
                                                        </td>
                                     </tr>
                                    
                                </table>
                             
                            </td>
                        </tr>
                         <tr>
                                    <td colspan="8" valign ="top" >
                                       <asp:Panel ID="pnlDopaymentDetails" runat ="server"  Visible ="false">
                                            <asp:GridView ID="GvDopaymentDetails" runat="server"  AutoGenerateColumns="False" TabIndex="4" Width="2024px"  AllowPaging ="true" AllowSorting ="true"  HeaderStyle-ForeColor="White" PageSize="25"  >
                                                     <Columns>                                                                                       
                                                                        <asp:TemplateField HeaderText="Account Id"  HeaderStyle-Wrap="false" SortExpression ="CAFNumber" >
                                                                                <itemtemplate>
                                                                                  <%--  <%#Eval("SlNo")%>--%>
                                                                                    <%#Eval("CAFNumber")%>  
                                                                                    <asp:HiddenField ID="hdPANumber" runat="server" Value='<%#Eval("PANumber")%>' />
                                                                                     <asp:HiddenField ID="hdNPID" runat="server" Value='<%#Eval("NPID")%>' />
                                                                                      <asp:HiddenField ID="hdISPOrderID" runat="server" Value='<%#Eval("ISPOrderID")%>' />
                                                                                        <asp:HiddenField ID="hdMonths" runat="server" Value='<%#Eval("Month")%>' />
                                                                                          <asp:HiddenField ID="hdYears" runat="server" Value='<%#Eval("Year")%>' />                                                                                                    
                                                                                    <asp:HiddenField ID="hdAmt" runat="server" Value='<%#Eval("Amount")%>' />
                                                                                </itemtemplate>
                                                                        </asp:TemplateField>     
                                                                                <%-- <asp:BoundField DataField="AccountNo" HeaderText="Account No" />--%>
                                                                               <%--  <asp:BoundField DataField="ISPOrderID"  HeaderText="ISP Order ID"  HeaderStyle-Wrap="false" />--%>
                                                                                <asp:BoundField DataField="UserName"  HeaderText="Login Name"  HeaderStyle-Wrap="false"  ItemStyle-Wrap="false" SortExpression ="UserName"   />  
                                                                                 <asp:BoundField DataField="NPID"  HeaderText="NPID"  HeaderStyle-Wrap="false"  ItemStyle-Wrap="false"   SortExpression ="NPID"  />
                                                                                                 <asp:BoundField DataField="Month" HeaderText="Cost Activity Month"  HeaderStyle-Wrap="false"  SortExpression ="Month"  />
                                                                                                 <asp:BoundField DataField="ISPName" HeaderText="ISP Name"  HeaderStyle-Wrap="false" ItemStyle-Wrap="true"  SortExpression ="ISPName"   />
                                                                                                 <asp:BoundField DataField="AgencyName"  HeaderText="Agency Name"  HeaderStyle-Wrap="false" ItemStyle-Wrap="true"  ItemStyle-Width="120px"  SortExpression ="AgencyName"  />
                                                                                                 <asp:BoundField DataField="Address" HeaderText="Address"  HeaderStyle-Wrap="false" ItemStyle-Wrap="true" ItemStyle-Width="180px"  SortExpression ="Address"   />
                                                                                                 <asp:BoundField DataField="City" HeaderText="City"  HeaderStyle-Wrap="false" ItemStyle-Wrap="False"  SortExpression ="City"  />
                                                                                                 <asp:BoundField DataField="OfficeID"  HeaderText="Office ID"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false"   SortExpression ="OfficeID"  />                                                                                                 
                                                                                                 <asp:BoundField DataField="OnlineDate" HeaderText="Online Date"   HeaderStyle-Wrap="false" ItemStyle-Wrap="false"  SortExpression ="OnlineDate"  />
                                                                                                 <asp:BoundField DataField="CancellationDate" HeaderText="Cancellation Date"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false"   SortExpression ="CancellationDate"  />
                                                                                                 <asp:BoundField DataField="Status" HeaderText="Status"  HeaderStyle-Wrap="false"  SortExpression ="UserName"  />
                                                                                                 <asp:BoundField DataField="StaticIP"  HeaderText="Static IP"  HeaderStyle-Wrap="false"  ItemStyle-Wrap="false"  SortExpression ="StaticIP"  />                                                                                                
                                                                                                 <asp:BoundField DataField="ISPRentalCharges"  HeaderText="Monthly Rental"   HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="right"  SortExpression ="ISPRentalCharges"  />
                                                                                                 <asp:BoundField DataField="VATAmount" HeaderText="VAT (12.24%)"  HeaderStyle-Wrap="false"   ItemStyle-HorizontalAlign="right"  SortExpression ="VATAmount"   />
                                                                                                 <asp:BoundField DataField="ISPInstallationCharges" HeaderText="Installation Charges"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="right"  SortExpression ="ISPInstallationCharges"  />
                                                                                                 <asp:BoundField DataField="I_VAT" HeaderText="VAT (12.24%)"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="right"  SortExpression ="I_VAT"    />
                                                                                                  <asp:BoundField DataField="StartDate"  HeaderText="From"  HeaderStyle-Wrap="false"  SortExpression ="StartDate"  />
                                                                                                 <asp:BoundField DataField="EndDate" HeaderText="To"  HeaderStyle-Wrap="false"  SortExpression ="EndDate"  />                                                                                                
                                                                                                  <asp:BoundField DataField="DaysUsed" HeaderText="Days Used"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="right"  SortExpression ="DaysUsed"  />
                                                                                                 <asp:BoundField DataField="ProRate" HeaderText="Pro Rate"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="right"  SortExpression ="ProRate"   />
                                                                                                 <asp:BoundField DataField="Amount" HeaderText="Amount"    HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="right"  SortExpression ="Amount"  />                                                                                                                                                                                                                                                                      
                                                                               <asp:TemplateField HeaderText=""  HeaderStyle-Wrap="false">
                                                                                <itemtemplate>                                                                                                
                                                                                </itemtemplate>
                                                                               </asp:TemplateField>     
                                                     </Columns>
                                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                                    <RowStyle CssClass="textbold" />
                                                                    <HeaderStyle CssClass="Gridheading"  HorizontalAlign="Left" />                                                    
                                         </asp:GridView>  </asp:Panel></td>
                                </tr>  
                                
                    </table>
                       <table border="0" cellpadding="2" cellspacing="1" style="width:2024px">
                                     <tr>
                                                    <td colspan ="8" align ="Left">
                                                        <asp:Button ID="btnSelectAll" runat="server" Text="Select All" CssClass="button" Visible="False" Width="80px" />
                                                        <asp:Button ID="btnDeSelectAll" runat="server" Text="DeSelect All" CssClass="button" Visible="False" />&nbsp;<asp:Button id="btnPayment" runat="server" CssClass="button" Text="Do Payment" Visible="False"></asp:Button>
                                                        </td>
                                     </tr>
                                     <tr>
                                                    <td colspan ="8" align ="right">
                                                       </td>
                                     </tr>
                                    <tr>
                                                    <td colspan="8" style="width:2024px">
                                                       <asp:Panel ID="pnlGrid" runat ="server" style="width:2024px">
                                                            <asp:GridView ID="gvIspPaymentRec" runat="server"  AutoGenerateColumns="False" TabIndex="4"  style="width:2024px"   AllowPaging ="true" AllowSorting ="true"  HeaderStyle-ForeColor="White" PageSize="25"   >
                                                                     <Columns>
                                                                                           <asp:TemplateField  HeaderStyle-Width="73px" >
                                                                                        <HeaderTemplate>
                                                                                            <asp:Label ID="lblAction" runat="server" Width ="73px" Text="Select"></asp:Label>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                              <asp:CheckBox ID="chkPtId" runat ="server" Checked="false"  />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="ItemColor" />
                                                                                        <HeaderStyle Wrap="False" CssClass="Gridheading" />
                                                                                    </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Account Id"  HeaderStyle-Wrap="false" ItemStyle-Wrap="true" SortExpression ="CAFNumber" >
                                                                                                <itemtemplate>
                                                                                                  <%--  <%#Eval("SlNo")%>--%>
                                                                                                    <%#Eval("CAFNumber")%>  
                                                                                                    <asp:HiddenField ID="hdPANumber" runat="server" Value='<%#Eval("PANumber")%>' />
                                                                                                     <asp:HiddenField ID="hdNPID" runat="server" Value='<%#Eval("NPID")%>' />
                                                                                                      <asp:HiddenField ID="hdISPOrderID" runat="server" Value='<%#Eval("ISPOrderID")%>' />
                                                                                                        <asp:HiddenField ID="hdMonths" runat="server" Value='<%#Eval("Month")%>' />
                                                                                                          <asp:HiddenField ID="hdYears" runat="server" Value='<%#Eval("Year")%>' />
                                                                                                            <asp:HiddenField ID="hdVATPercentage" runat="server" Value='<%#Eval("VATPercentage")%>' />
                                                                                                              <asp:HiddenField ID="hdRowNo" runat="server" Value='<%#Eval("RowNo")%>' />
                                                                                                                <asp:HiddenField ID="hdCheckUncheckStatus" runat="server" Value='<%#Eval("CheckUncheckStatus")%>' />
                                                                                                     
                                                                                                    <asp:HiddenField ID="hdAmt" runat="server" Value='<%#Eval("Amount")%>' />
                                                                                                </itemtemplate>
                                                                                        </asp:TemplateField>   
                                                                                                 <asp:BoundField DataField="UserName"  HeaderText="Login Name"  HeaderStyle-Wrap="false"  ItemStyle-Wrap="false" SortExpression ="UserName" />  
                                                                                                <%-- <asp:BoundField DataField="AccountNo" HeaderText="Account No" />--%>
                                                                                                <%-- <asp:BoundField DataField="ISPOrderID"  HeaderText="ISP Order ID"  HeaderStyle-Wrap="false" />--%>
                                                                                                 <asp:BoundField DataField="NPID"  HeaderText="NPID"  HeaderStyle-Wrap="false"  ItemStyle-Wrap="false"   SortExpression ="NPID"  />
                                                                                                 <asp:BoundField DataField="Month" HeaderText="Cost Activity Month"  HeaderStyle-Wrap="false"  SortExpression ="Month"  />
                                                                                                 <asp:BoundField DataField="ISPName" HeaderText="ISP Name"  HeaderStyle-Wrap="false" ItemStyle-Wrap="true"  SortExpression ="ISPName"   />
                                                                                                 <asp:BoundField DataField="AgencyName"  HeaderText="Agency Name"  HeaderStyle-Wrap="false" ItemStyle-Wrap="true"  ItemStyle-Width="120px"  SortExpression ="AgencyName"  />
                                                                                                 <asp:BoundField DataField="Address" HeaderText="Address"  HeaderStyle-Wrap="false" ItemStyle-Wrap="true" ItemStyle-Width="180px"  SortExpression ="Address"   />
                                                                                                 <asp:BoundField DataField="City" HeaderText="City"  HeaderStyle-Wrap="false" ItemStyle-Wrap="False"  SortExpression ="City"  />
                                                                                                 <asp:BoundField DataField="OfficeID"  HeaderText="Office ID"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false"   SortExpression ="OfficeID"  />                                                                                                 
                                                                                                 <asp:BoundField DataField="OnlineDate" HeaderText="Online Date"   HeaderStyle-Wrap="false" ItemStyle-Wrap="false"  SortExpression ="OnlineDate"  />
                                                                                                 <asp:BoundField DataField="CancellationDate" HeaderText="Cancellation Date"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false"   SortExpression ="CancellationDate"  />
                                                                                                 <asp:BoundField DataField="Status" HeaderText="Status"  HeaderStyle-Wrap="false"  SortExpression ="UserName"  />
                                                                                                 <asp:BoundField DataField="StaticIP"  HeaderText="Static IP"  HeaderStyle-Wrap="false"  ItemStyle-Wrap="false"  SortExpression ="StaticIP"  />                                                                                                
                                                                                                 <asp:BoundField DataField="ISPRentalCharges"  HeaderText="Monthly Rental"   HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="right"  SortExpression ="ISPRentalCharges"  />
                                                                                                 <asp:BoundField DataField="VATAmount" HeaderText="VAT (12.24%)"  HeaderStyle-Wrap="false"   ItemStyle-HorizontalAlign="right"  SortExpression ="VATAmount"   />
                                                                                                 <asp:BoundField DataField="ISPInstallationCharges" HeaderText="Installation Charges"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="right"  SortExpression ="ISPInstallationCharges"  />
                                                                                                 <asp:BoundField DataField="I_VAT" HeaderText="VAT (12.24%)"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="right"  SortExpression ="I_VAT"    />
                                                                                                  <asp:BoundField DataField="StartDate"  HeaderText="From"  HeaderStyle-Wrap="false"  SortExpression ="StartDate"  />
                                                                                                 <asp:BoundField DataField="EndDate" HeaderText="To"  HeaderStyle-Wrap="false"  SortExpression ="EndDate"  />                                                                                                
                                                                                                  <asp:BoundField DataField="DaysUsed" HeaderText="Days Used"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="right"  SortExpression ="DaysUsed"  />
                                                                                                 <asp:BoundField DataField="ProRate" HeaderText="Pro Rate"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="right"  SortExpression ="ProRate"   />
                                                                                                 <asp:BoundField DataField="Amount" HeaderText="Amount"    HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="right"  SortExpression ="Amount"  />                                                                                                                                                                                  
                                                                                               <asp:TemplateField HeaderText=""  HeaderStyle-Wrap="false">
                                                                                                <itemtemplate>                                                                                                
                                                                                                </itemtemplate>
                                                                                               </asp:TemplateField>     
                                                                     </Columns>
                                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                                    <RowStyle CssClass="textbold" />
                                                                    <HeaderStyle CssClass="Gridheading"  HorizontalAlign="Left" />  
                                                                          <PagerTemplate></PagerTemplate>
                                     <%--   <PagerSettings FirstPageText="" NextPageText="&gt;&gt; Next" LastPageText="" PreviousPageText="&lt;&lt;  Prev" Mode="NextPrevious" />--%>
                                        <PagerStyle HorizontalAlign="Center" />                                                  
                                                         </asp:GridView>  </asp:Panel></td>
                                                </tr>
                                                
                                                   <tr>
                          <td  colspan ="2" style ="height:10px;"></td>
                            </tr> 
                              <tr>                                                   
                                <td colspan="6" valign ="top"  >
                                <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="850px">
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
                                <td colspan="6" style="height: 23px" ></td> 
                            </tr> 
                     </table>
                    <asp:HiddenField ID="hdUpdateForSessionXml" runat="server" />
                     <asp:HiddenField id="hdChechedItem" runat="server" Value ="0"></asp:HiddenField>
                </td>
            </tr>   
                                
        </table>
    </form>
</body>
</html>
