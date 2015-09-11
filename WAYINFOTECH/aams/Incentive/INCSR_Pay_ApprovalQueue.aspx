<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INCSR_Pay_ApprovalQueue.aspx.vb"
    ValidateRequest="false" Inherits="Incentive_INCSR_Pay_ApprovalQueue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS::Incentive::Business Case Approval Queue</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>

    <!-- import the language module -->

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script type="text/javascript" language="javascript">
       
          function  Payment(objBCID,objChainCode,objMonth,objYear,ObjPayId,objPayTime,objCurPayNo,objPeriod,objPLB)
          {
          
          //alert(objPayTime);
            var Param="BCaseID=" + objBCID + "&Chain_Code=" + objChainCode  +"&Month=" + objMonth + "&Year=" + objYear + "&PayId=" + ObjPayId  + "&PayTime=" + objPayTime  + "&CurPayNo=" + objCurPayNo  +  "&PLB=" + objPLB + "&Period=" + objPeriod  ;
            var type;
           // type= "INCUP_PaymentProcess.aspx?" + Param;
            if (objPayTime=='U' )
            {
               type= "INCUP_PaymentProcessUpfront.aspx?" + Param;           
            }
            else
            {
              type= "INCUP_PaymentProcess.aspx?" + Param;
            }
            
               window.open(type,"IncPay","height=730,width=970,top=30,left=20,scrollbars=1,status=1,resizable=1");	        
            //window.location.href =type;
            return false;
          }
          
          
            
          function  PaymentSheet(objBCID,objChainCode,objMonth,objYear,objPayTime,objCurPayNo)
          {
            
            //ASHISH ON 01-11-2010 FOR THE DISPLAY PAYMENT SHEET REPORT 
            var Param="BCaseID=" + objBCID + "&Chain_Code=" + objChainCode  +"&Month=" + objMonth + "&Year=" + objYear   +  "&PayType=" + objPayTime  + "&CurPayNo=" + objCurPayNo + "&Case=PaymentSheet" ;
            alert (Param);
            var type;
          
            if (objPayTime=='U' )
            {
               type= "../RPSR_ReportShow.aspx?" + Param;           
            }
            else
            {
              type= "../RPSR_ReportShow.aspx?" + Param;
            }
                                
                 window.open(type,"parent",'height=600,width=880,top=30,left=20,scrollbars=1,resizable=1');       
                 return false;
          }
          
          
          
         
         function CheckValidation()
         {
         // window.document.getElementById ('UpDateProgress1').className ="displayNone";
           $get("<%=Btnexp.ClientID %>").click(); 
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
              
            
            
             return false;
			
    }
       
       
        function ExpandModels(trId, ChkClientId,ObjdpTotalInWord,ObjdpTotalInNumeric) 
       {   
      
            if  ( document.getElementById(ChkClientId).checked==true)
            {
                document.getElementById(ChkClientId).checked=true;
            }
            else
            {
                document.getElementById(ChkClientId).checked=false;
            }  
                 lastBehaviorInWord = $find(ObjdpTotalInWord);     
                  if (lastBehaviorInWord)
                  {    
                   lastBehaviorInWord.populate(trId); 
                  }
                  
                   lastBehaviorInNumeric = $find(ObjdpTotalInNumeric);     
                  if (lastBehaviorInNumeric)
                  {    
                   lastBehaviorInNumeric.populate(trId); 
                  }
                  
        }
        
        function CallReject()
    {
        document.getElementById('<%=lblReasonError.ClientId%>').innerHTML="";
        document.getElementById('<%=txtReason.ClientId%>').value='';
        $get("<%=btnConfirm.ClientID %>").click();
        $get("<%=btnYes.ClientID %>").focus();
        return false;
    }
   
        function ShowMandatory()
         {
         
                if(document.getElementById('<%=txtReason.ClientId%>').value =='')
            {
                document.getElementById('<%=lblReasonError.ClientId%>').innerHTML='Please provide the reason for the rejection.'
                document.getElementById('<%=txtReason.ClientId%>').focus();
                return false;
            }
               if (document.getElementById("txtReason").value.trim().length>500)
        {
             document.getElementById("lblReasonError").innerHTML="Reason can't be greater than 500 characters."
             document.getElementById("txtReason").focus();
             return false;
        }  
            
              document.getElementById('<%=lblReasonError.ClientId%>').innerHTML="";
             return true;
         }

    
       
    </script>

    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
      <style type="text/css">

   .confirmationBackground {
	background-color:#434040;
	filter:alpha(opacity=20);
	opacity:0.7;
}
.confirmationPopup 
{
	
	background-color:#ffffdd;
	border:3px solid #0457b7;
	padding:px;
	width:250px;
	background-color:#ffffff;
	border-top-width:3px;

}

.strip_bluelogin	{
	background-image:url(../Images/strip_bluelogin.jpg);
	background-repeat:repeat-x;
	height:36px;
	font-family:Verdana;
	font-size:16px;
	color:#FFFFFF;
	padding-left:10px;
}

</style>
</head>
<body>
    <form id="frmAgency" runat="server" defaultfocus="txtChainCode">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="660">
        </asp:ScriptManager>
        <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        </asp:ScriptManagerProxy>
        <table width="845px" class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%">
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td colspan ="2" valign="top" align="left">
                                            <span class="menu">Incentive-></span><span class="sub_menu">Payment Approval Queue</span>
                                        </td>
                                    </tr>
                                    <tr class="heading">
                                        <td class="heading" align="center" valign="top" style ="width:800px;" >Payment Approval Queue</td>
                                        <td>
                                         <asp:Button ID="Btnexp" CssClass="button" runat="server" Text="exp"
                                                          style="display:none;"  TabIndex="17" AccessKey="r" Width="115px" />
                                        
                                        </td>
                                           
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="top">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td valign="top" class="redborder">
                                                    <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td  align="left" colspan="6"  valign="TOP" style="width:200px;height:20px;">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False" Width ="800px"  style="text-align:center;" ></asp:Label>
                                                            </td>                                                           
                                                        </tr>
                                                        <tr>
                                                            <td  colspan="6">
                                                                <table style="width: 840px" border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td style="width: 15%;" class="textbold">
                                                                            &nbsp;</td>
                                                                        <td style="width: 6%;" class="textbold">
                                                                            <span class="textbold">Month</span></td>
                                                                        <td style="width: 22%;" class="textbold">
                                                                            <asp:DropDownList ID="drpMonths" runat="server" Width="148px" TabIndex="1" CssClass="dropdownlist">
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
                                                                            </asp:DropDownList>&nbsp;&nbsp;
                                                                        </td>
                                                                        <td style="width: 6%;">
                                                                            <span class="textbold">Year</span></td>
                                                                        <td style="width: 20%;" class="textbold">
                                                                            <asp:DropDownList ID="drpYears" runat="server" CssClass="dropdownlist" Width="158px"
                                                                                TabIndex="1">
                                                                            </asp:DropDownList></td>
                                                                        <td style="width: 5%;" class="textbold">
                                                                        </td>
                                                                        <td style="width: 20%;">
                                                                            <asp:Button ID="BtnSearch" CssClass="button" runat="server" Text="Seach" TabIndex="3"
                                                                                AccessKey="S" Width="115px" /></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 2px;" class="textbold" colspan="7" align="center" valign="TOP"> <asp:Button ID="BtnConfirm" Text="" runat="server" CssClass="displayNone" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 15%; height: 19px;" class="textbold">
                                                                            &nbsp;</td>
                                                                        <td style="width: 6%; height: 19px;" class="textbold" nowrap="nowrap">
                                                                            <span class="textbold">Status</span></td>
                                                                        <td style="width: 22%; height: 19px;" class="textbold">
                                                                            <asp:DropDownList ID="DlstStatus" runat="server" CssClass="dropdownlist" Width="148px"
                                                                                TabIndex="1">
                                                                            </asp:DropDownList></td>
                                                                        <td style="width: 6%; height: 19px;">
                                                                            <span class="textbold"></span>
                                                                        </td>
                                                                        <td style="width: 20%; height: 19px;" class="textbold">
                                                                        </td>
                                                                        <td style="width: 5%; height: 19px;" class="textbold">
                                                                        </td>
                                                                        <td style="width: 20%; height: 19px;">
                                                                            <asp:Button ID="BtnExport" CssClass="button" runat="server" Text="Export" TabIndex="17"
                                                                                AccessKey="E" Width="115px" /></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 2px;" class="textbold" colspan="7" align="center" valign="TOP">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="textbold" style="width: 15%; height: 19px">
                                                                        </td>
                                                                        <td class="textbold" nowrap="nowrap" style="width: 6%; height: 19px">
                                                                        </td>
                                                                        <td class="textbold" style="width: 22%; height: 19px">
                                                                        </td>
                                                                        <td style="width: 6%; height: 19px">
                                                                        </td>
                                                                        <td class="textbold" style="width: 20%; height: 19px">
                                                                        </td>
                                                                        <td class="textbold" style="width: 5%; height: 19px">
                                                                        </td>
                                                                        <td style="width: 20%; height: 19px">
                                                                            <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="17"
                                                                                AccessKey="r" Width="115px" /></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 2px;" class="textbold" colspan="7" align="center" valign="TOP">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="textbold" style="width: 15%; height: 19px">
                                                                        </td>
                                                                        <td class="textbold" nowrap="nowrap" style="width: 6%; height: 19px">
                                                                        </td>
                                                                        <td class="textbold" style="width: 22%; height: 19px">
                                                                        </td>
                                                                        <td style="width: 6%; height: 19px">
                                                                        </td>
                                                                        <td class="textbold" style="width: 20%; height: 19px">
                                                                        </td>
                                                                        <td class="textbold" style="width: 5%; height: 19px">
                                                                        </td>
                                                                        <td style="width: 20%; height: 19px">
                                                                            <asp:Button ID="BtnApproved" CssClass="button" runat="server" Text="Approved" TabIndex="17"
                                                                                Width="115px" /></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 2px;" class="textbold" colspan="7" align="center" valign="TOP">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="textbold" style="width: 15%; height: 18px">
                                                                        </td>
                                                                        <td class="textbold" colspan ="4" nowrap="nowrap" style="width: 6%; height: 18px">
                                                                          <div id="DivFAColor" runat="server" visible ="false"  >
                                                                        <b>Note: &nbsp;<asp:Label ID="lblFAColor" runat="server" BackColor="LightSeaGreen"
                                                                            Text="&nbsp;&nbsp;&nbsp;"></asp:Label>
                                                                            Denotes Finally Approved.</b></div>
                                                                        
                                                                        </td>
                                                                      
                                                                        <td class="textbold" style="width: 5%; height: 18px">
                                                                        </td>
                                                                        <td style="width: 20%; height: 18px">
                                                                            <asp:Button ID="BtnReject" CssClass="button" runat="server" Text="Reject" TabIndex="17" 
                                                                                Width="115px" /></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 2px;" class="textbold" colspan="7" align="center" valign="TOP">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="textbold" style="width: 15%; height: 19px">
                                                                        </td>
                                                                        <td class="textbold" nowrap="nowrap" style="width: 6%; height: 19px">
                                                                        </td>
                                                                        <td class="textbold" style="width: 22%; height: 19px">
                                                                        </td>
                                                                        <td style="width: 6%; height: 19px">
                                                                        </td>
                                                                        <td class="textbold" style="width: 20%; height: 19px">
                                                                        </td>
                                                                        <td class="textbold" style="width: 5%; height: 19px">
                                                                        </td>
                                                                        <td style="width: 20%; height: 19px">
                                                                            <asp:Button ID="BtnFinnallyApproved" CssClass="button" runat="server" Text="Finally Approved"
                                                                                TabIndex="17" AccessKey="r" Width="115px" /></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 2px;" class="textbold" colspan="7" align="center" valign="TOP">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="textbold">
                                                            </td>
                                                            <td colspan="2">
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        
                                                       
                                                        <tr>
                                                            <td colspan="6" align="Left">
                                                                <asp:Button ID="btnSelectAll" runat="server" Text="Select All" CssClass="button"
                                                                    Visible="False" Width="80px" />
                                                                <asp:Button ID="btnDeSelectAll" runat="server" Text="DeSelect All" CssClass="button"
                                                                    Visible="False" />&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6" align ="left"    >
                                                                <table style="width:830px;" border="0">
                                                                    <tr>
                                                                        <td align ="center" >
                                                                            <asp:UpdateProgress ID="UpDateProgress1" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="10"
                                                                                runat="server">
                                                                                <ProgressTemplate>
                                                                                    <img alt="Loading.." src="../Images/loading.gif" id="imgLoad" runat="server" />
                                                                                </ProgressTemplate>
                                                                            </asp:UpdateProgress>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                  </tr>
                                                                </table>
                                                               
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6" align="right">
                                                            </td>
                                                        </tr>
                                                          
                                                       
                                                        <tr>
                                                            <td colspan="6" height="4">
                                                                <asp:GridView ID="gvIspPaymentRec" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                    AllowPaging="true" Width="2500px" EnableViewState="true" AllowSorting="false"
                                                                    ShowFooter="true">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderStyle-Width="100px">
                                                                            <HeaderTemplate>
                                                                                <asp:Label ID="lblAction" runat="server" Width="73px" Text="Select"></asp:Label>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkPtId" runat="server" Checked="false" />
                                                                                <asp:HiddenField ID="hdPANumber" runat="server" Value='<%#Eval("PAYMENT_ID")%>' />
                                                                                <asp:HiddenField ID="hdMonths" runat="server" />
                                                                                <asp:HiddenField ID="hdYears" runat="server" />
                                                                                <asp:HiddenField ID="HdFinalApproved" runat="server" Value='<%#Eval("PA_FINAL_APPROVED")%>' />
                                                                                <asp:HiddenField ID="hdRowNo" runat="server" Value='<%# Eval("ROWNO")%>' />
                                                                                <asp:HiddenField ID="hdCheckUncheckStatus" runat="server" Value='<%#Eval("CheckUncheckStatus")%>' />
                                                                                <asp:HiddenField ID="HdTotal" runat="server" Value='<%#Eval("THISQUARTERPAYMENT")%>' />
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="ItemColor" />
                                                                            <HeaderStyle Wrap="False" CssClass="Gridheading" />
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Region" ItemStyle-Width="100px" DataField="REGION" SortExpression="REGION"
                                                                            HeaderStyle-Width="100px"></asp:BoundField>
                                                                        <asp:BoundField HeaderText="BCaseId " ItemStyle-Width="70px" DataField="BC_ID" SortExpression="BC_ID" 
                                                                            HeaderStyle-Width="40px"></asp:BoundField>
                                                                        <asp:BoundField DataField="CHAIN_CODE" HeaderText="Chain Code" HeaderStyle-Wrap="false"
                                                                            ItemStyle-Wrap="false"  SortExpression="CHAIN_CODE" />
                                                                        <asp:BoundField DataField="OFFICEID" HeaderText="Office ID" HeaderStyle-Wrap="false"
                                                                            ItemStyle-Wrap="false"  Visible ="true"  SortExpression="OFFICEID" />
                                                                        <asp:BoundField DataField="GROUPNAME" HeaderText="Group Name" HeaderStyle-Wrap="false" SortExpression="GROUPNAME"
                                                                            ItemStyle-Width="180px" ItemStyle-Wrap="false" />
                                                                        <asp:BoundField DataField="CITY" HeaderText="City" HeaderStyle-Wrap="false" SortExpression="CITY"
                                                                            ItemStyle-Wrap="false" />
                                                                        <asp:BoundField DataField="PERIOD" HeaderText="Period" HeaderStyle-Wrap="false" 
                                                                            ItemStyle-Wrap="false" ItemStyle-Width="180px" />
                                                                        <asp:BoundField DataField="NOOFMONTHS" HeaderText="No Of Month" HeaderStyle-Wrap="false" SortExpression="NOOFMONTHS"
                                                                             />
                                                                        <asp:BoundField DataField="AMOUNT" HeaderText="Amount" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" SortExpression="AMOUNT" ItemStyle-HorizontalAlign="right"  />
                                                                        <asp:BoundField DataField="ANYADJ" HeaderText="Any ADJ" HeaderStyle-Wrap="false"
                                                                            ItemStyle-Wrap="false" SortExpression="ANYADJ"  ItemStyle-HorizontalAlign="right"  />
                                                                        <asp:BoundField DataField="THISQUARTERPAYMENT" HeaderText="Final Payment" HeaderStyle-Wrap="false" SortExpression="THISQUARTERPAYMENT"
                                                                            ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="right"  />
                                                                        <asp:BoundField DataField="SEGMENTS_PAID_FOR" HeaderText="Segment Paid For" HeaderStyle-Wrap="false" SortExpression="SEGMENTS_PAID_FOR"
                                                                            ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="right"  />
                                                                        <%-- <asp:BoundField DataField="SEGMENT" HeaderText="Segment" HeaderStyle-Wrap="false"
                                                                                ItemStyle-Wrap="false" />--%>
                                                                        <asp:BoundField DataField="GROSSSEGMENTS" HeaderText="Gross Segment" HeaderStyle-Wrap="false"
                                                                            ItemStyle-Wrap="false" Visible ="true"  SortExpression="GROSSSEGMENTS"  ItemStyle-HorizontalAlign="right"  />
                                                                        <asp:BoundField DataField="GROSS_WOIC" HeaderText="Segment Gross WOIC" HeaderStyle-Wrap="false" 
                                                                            ItemStyle-Wrap="false" Visible ="true"  SortExpression="GROSS_WOIC"  ItemStyle-HorizontalAlign="right"   />
                                                                        <asp:BoundField DataField="SOLE" HeaderText="Sole" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" Visible ="true"  ItemStyle-HorizontalAlign="center"   SortExpression="SOLE" />
                                                                        <asp:BoundField DataField="REMARKS" HeaderText="Remarks" HeaderStyle-Wrap="false"
                                                                            ItemStyle-Wrap="true" />
                                                                             <asp:BoundField DataField="HW" HeaderText="HW" HeaderStyle-Wrap="false" SortExpression="HW" 
                                                                            ItemStyle-Wrap="true"  ItemStyle-HorizontalAlign="right"  />
                                                                             <asp:BoundField DataField="SARAL" HeaderText="SARAL" HeaderStyle-Wrap="false"
                                                                            ItemStyle-Wrap="true"  SortExpression="SARAL"  ItemStyle-HorizontalAlign="right"  />
                                                                             <asp:BoundField DataField="ILL" HeaderText="ILL" HeaderStyle-Wrap="false"
                                                                            ItemStyle-Wrap="true" SortExpression="ILL"  ItemStyle-HorizontalAlign="right"  />
                                                                             <asp:BoundField DataField="WLL" HeaderText="WLL" HeaderStyle-Wrap="false"
                                                                            ItemStyle-Wrap="true" SortExpression="WLL"  ItemStyle-HorizontalAlign="right"  />
                                                                        <asp:BoundField DataField="TOTALCOST" HeaderText="Total Cost" HeaderStyle-Wrap="false"
                                                                            ItemStyle-Wrap="false" Visible ="true"  SortExpression="TOTALCOST"  ItemStyle-HorizontalAlign="right"  />
                                                                        <asp:BoundField DataField="CPS_GROSS" HeaderText="CPS Gross " HeaderStyle-Wrap="false"
                                                                            ItemStyle-Wrap="false"  Visible ="true"  SortExpression="CPS_GROSS"   ItemStyle-HorizontalAlign="right"  />
                                                                        <asp:BoundField DataField="CPS_WOIC" HeaderText="CPS WOIC " HeaderStyle-Wrap="false"
                                                                            ItemStyle-Wrap="false" Visible ="true"  SortExpression="CPS_WOIC"  ItemStyle-HorizontalAlign="right"   />
                                                                        <asp:BoundField DataField="CPS_WOHW" HeaderText="CPS WOHW " HeaderStyle-Wrap="false"
                                                                            ItemStyle-Wrap="false"  Visible ="true" SortExpression="CPS_WOHW"  ItemStyle-HorizontalAlign="right"   /> 
                                                                        <asp:BoundField DataField="GUARUNTEEBY" HeaderText="Guaruntee By " 
                                                                            HeaderStyle-Wrap="false" ItemStyle-Wrap="false" Visible ="true" SortExpression="GUARUNTEEBY"  />
                                                                            <asp:BoundField DataField="BIRDRESUPDATE" HeaderText="Birdres Update "
                                                                            HeaderStyle-Wrap="false" ItemStyle-Wrap="false" Visible ="true" SortExpression="BIRDRESUPDATE"   />
                                                                        <asp:BoundField DataField="STATUS" HeaderText="Status" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" Visible ="false"  SortExpression="STATUS"/>
                                                                        <asp:BoundField DataField="CURRENTSTATUS" HeaderText="Current Status" HeaderStyle-Wrap="false"
                                                                           Visible ="false"   ItemStyle-Wrap="false" SortExpression="CURRENTSTATUS" />
                                                                        <asp:TemplateField HeaderText="Action">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkPayment" CssClass="LinkButtons" Text="Details" runat="server"
                                                                                    CommandName="SelectPay" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "BC_ID") + "|" + DataBinder.Eval(Container.DataItem, "CHAIN_CODE") %>'
                                                                                    Visible="true"></asp:LinkButton>&nbsp;
                                                                              
                                                                                <asp:LinkButton ID="lnkPaymentSheet" CssClass="LinkButtons" Text="Display" runat="server"
                                                                                    CommandName="SelectPay" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "BC_ID") + "|" + DataBinder.Eval(Container.DataItem, "CHAIN_CODE") %>'
                                                                                    Visible="false"></asp:LinkButton>
                                                                                    
                                                                                    
                                                                                <asp:HiddenField ID="hdInput" Value='<%# DataBinder.Eval(Container.DataItem, "BC_ID") + "|" + DataBinder.Eval(Container.DataItem, "CHAIN_CODE") %>'
                                                                                    runat="server" />
                                                                                     <asp:HiddenField ID="hdPayType" value='<%# DataBinder.Eval(Container.DataItem, "PAYMENTTYPE")  %>' runat ="server" />
                                                                                         <asp:HiddenField ID="HdCurPayNo" value='<%# DataBinder.Eval(Container.DataItem, "NO_OF_PAYMENT")  %>' runat ="server" />
                                                                                           <asp:HiddenField ID="HDPLBCYCLE" value='<%# DataBinder.Eval(Container.DataItem, "PLBCYCLE")  %>' runat ="server" />
                                                                                            <asp:HiddenField ID="HDPLBPERIODFROM" value='<%# DataBinder.Eval(Container.DataItem, "PLBPERIODFROM")  %>' runat ="server" />
                                                                                             <asp:HiddenField ID="HDPLBPERIODTO" value='<%# DataBinder.Eval(Container.DataItem, "PLBPERIODTO")  %>' runat ="server" />                
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <AlternatingRowStyle CssClass="lightblue" HorizontalAlign="left" />
                                                                    <RowStyle CssClass="textbold" HorizontalAlign="left" />
                                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White" HorizontalAlign="left" />
                                                                    <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                    <PagerTemplate>
                                                                    </PagerTemplate>
                                                                    <PagerStyle HorizontalAlign="Center" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6" height="12">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="LblToTalInNum" runat="server" Visible="false"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="LblTotal" runat="server"></asp:Label></td>
                                                                    </tr>
                                                                </table>
                                                                <cc1:DynamicPopulateExtender ID="dpTotalInWord" runat="server" CacheDynamicResults="false"
                                                                    ClearContentsDuringUpdate="true" EnableViewState="true" ServiceMethod="CalculateServiceForWord"
                                                                    UpdatingCssClass="dynamicPopulate_Updating" TargetControlID="LblTotal">
                                                                </cc1:DynamicPopulateExtender>
                                                                <cc1:DynamicPopulateExtender ID="dpTotalInNumeric" runat="server" CacheDynamicResults="false"
                                                                    ClearContentsDuringUpdate="true" EnableViewState="true" ServiceMethod="CalculateServiceForNumeric"
                                                                    UpdatingCssClass="dynamicPopulate_Updating" TargetControlID="LblToTalInNum">
                                                                </cc1:DynamicPopulateExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6" width="860px;">
                                                                <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr class="paddingtop paddingbottom">
                                                                            <td style="width: 30%" class="left">
                                                                                <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                                    ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey"
                                                                                    ReadOnly="True"></asp:TextBox></td>
                                                                            <td style="width: 25%" class="right">
                                                                                <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                            <td style="width: 20%" class="center">
                                                                                <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber"
                                                                                    Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                                                </asp:DropDownList></td>
                                                                            <td style="width: 25%" class="left">
                                                                                <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6" style="height: 44px">
                                                                <asp:HiddenField ID="hdID" runat="server" />
                                                                <asp:HiddenField ID="hdUpdateForSessionXml" runat="server" />
                                                                &nbsp;&nbsp;
                                                                <asp:HiddenField ID="hdChechedItem" runat="server" Value="0" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                              <tr>
                                                    <td colspan="6">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <cc1:ConfirmButtonExtender ID="cnfrmBtnExt" runat="server" DisplayModalPopupID="mdlPopUpExt"
                                                                        TargetControlID="BtnConfirm" BehaviorID="cnfrmBtnExt">
                                                                    </cc1:ConfirmButtonExtender>
                                                                </td>
                                                                <td>
                                                                    <cc1:ModalPopupExtender ID="mdlPopUpExt" runat="server" TargetControlID="BtnConfirm"
                                                                        BackgroundCssClass="confirmationBackground" PopupControlID="pnlPopup" OkControlID="BtnFakeYes"
                                                                        CancelControlID="BtnFakeNo">
                                                                    </cc1:ModalPopupExtender>
                                                                    <asp:Panel ID="pnlPopup" runat="server" Width="600px" Height="250px" Style="display: none"
                                                                        HorizontalAlign="Center" CssClass="confirmationPopup">
                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td class="strip_bluelogin" align="left" colspan="2">
                                                                                    Amadeus agent management system </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td height="30" colspan="2">
                                                                                    <asp:Label ID="lblReasonError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="textbold" colspan="2">
                                                                                    <%--<b>The payment needs further approval , if click yes the process will start from the
                                                                                        first level<br />
                                                                                        Do you want to proceed?</b>--%>
                                                                                      <b>The payment needs further payment advice generaion, if click yes<br/> the process will also remove
                                                                                        from the payment approval queue.<br />
                                                                                        Do you want to proceed?</b>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td height="30" colspan="2">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="textbold" valign="middle" align="center">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;Reason</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtReason" runat="server" Text="" TextMode="MultiLine" Width="490px"
                                                                                        Rows="4" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" align="center">
                                                                                    <table border="0" width="40%" cellspacing="10" cellpadding="1">
                                                                                        <tr>
                                                                                            <td align="center">
                                                                                                <asp:Button ID="btnYes" runat="server" CssClass="button" Text="Yes" Width="60px"
                                                                                                    OnClientClick=" return ShowMandatory();" /></td>
                                                                                            <td align="center">
                                                                                                <asp:Button ID="btnNo" CssClass="button" runat="server" Text="No" Width="60px" OnClientClick="$find('mdlPopUpExt').hide(); return false;" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center">
                                                                                                <asp:Button ID="BtnFakeYes" runat="server" Text="" Width="60px" CssClass="displayNone" /></td>
                                                                                            <td align="center">
                                                                                                <asp:Button ID="BtnFakeNo" runat="server" Text="" Width="60px" CssClass="displayNone" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
