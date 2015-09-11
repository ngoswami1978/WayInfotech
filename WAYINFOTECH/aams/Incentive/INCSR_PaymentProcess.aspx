<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INCSR_PaymentProcess.aspx.vb"
    Inherits="Incentive_INCSR_PaymentProcess" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Search Process Payment</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>

    <script type="text/javascript" language="javascript">       
       function  History(objBCID,objChainCode,objMonth,objYear,objFirstTime,objPayTime,objCurPayNo,objPeriod,objPLB)
          {
            var Param="BCaseID=" + objBCID + "&Chain_Code=" + objChainCode  +"&Month=" + objMonth + "&Year=" + objYear   + "&FirstTime=" + objFirstTime   + "&PayType=" + objPayTime  + "&CurPayNo=" + objCurPayNo  +  "&PLB=" + objPLB + "&Period=" + objPeriod  ;
            var type;
            type= "INC_PaymentProcHistory.aspx?" + Param; 
            window.open(type,"IncPayProcHis","height=630,width=1025,top=30,left=20,scrollbars=1,status=1,resizable=1");	        
            return false;
          }
                    
       function PopupAgencyGroupForIncentive()
        {
                    var type;
                    type = "../Setup/MSSR_ManageAgencyGroup.aspx?Popup=T" ;
   	                window.open(type,"IncSBC","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
                    return false;

        }       
      function  Payment(objBCID,objChainCode,objMonth,objYear,objFirstTime,objPayTime,objCurPayNo,objPeriod,objPLB)
      {      
      //alert(objPeriod);    
        var Param="BCaseID=" + objBCID + "&Chain_Code=" + objChainCode  +"&Month=" + objMonth + "&Year=" + objYear   + "&FirstTime=" + objFirstTime   + "&PayType=" + objPayTime  + "&CurPayNo=" + objCurPayNo +  "&PLB=" + objPLB + "&Period=" + objPeriod  ;
        var type;
      
        if (objPayTime=='U' )
        {
           type= "INCUP_PaymentProcessUpfront.aspx?" + Param;           
        }
        else
        {
          type= "INCUP_PaymentProcess.aspx?" + Param;
        }
            window.open(type,"IncPay","height=630,width=1025,top=30,left=20,scrollbars=1,status=1,resizable=1");	        
                return false;
      }
         
         
      function  PaymentSheet(objBCID,objChainCode,objMonth,objYear,objFirstTime,objPayTime,objCurPayNo)
      {
        
        //ASHISH ON 01-11-2010 FOR THE DISPLAY PAYMENT SHEET REPORT 
        var Param="BCaseID=" + objBCID + "&Chain_Code=" + objChainCode  +"&Month=" + objMonth + "&Year=" + objYear   + "&FirstTime=" + objFirstTime   + "&PayType=" + objPayTime  + "&CurPayNo=" + objCurPayNo + "&Case=PaymentSheet" ;
        //alert (Param);
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
             if (  document.getElementById("txtChainCode").value!="")
         {
           if(IsDataValid(document.getElementById("txtChainCode").value,3)==false)
            {
            document.getElementById("lblError").innerHTML="Chain code is not valid.";
            document.getElementById("txtChainCode").focus();
            return false;
            } 
         }
     }
     
     function ShowPopupTabChange(id)
        {       
            try
            {            
            if (document.getElementById(id).disabled==false)
            { 
                var modal = $find('ModalPopupExtender2'); 
                document.getElementById('PnlPrrogress').style.height='310px';
                modal.show(); 
                }
            }    
             catch(err){}
         }         
      function MandatoryField()
      {
          try
          {           
              var chkbox =document.getElementById("chkLstNIDTField");
              var inputArr = chkbox.getElementsByTagName('input');         
              var Count =0;        
               for (var i=0; i<inputArr.length; i++)
              {
                 if (inputArr[i].checked == true)
                    {
                       Count=Count+1;
                    }
              }       
              if (Count ==0 )
              {
                document.getElementById("lblRemError").innerHTML="Airline data is mandatory.";
                document.getElementById("txtRem").focus();
                return false;
              }
              
              
                if ( document.getElementById("txtRem").value.trim().length == 0) 
                  {
                     if (Count>0)
                     {
                          document.getElementById("lblRemError").innerHTML = "Remark is mandatory.";  
                           document.getElementById("txtRem").focus();
                          return false;               
                          
                     }
                  
                 }
                   if (document.getElementById('HdSelectedCount').value != Count)
                    {
                         if (document.getElementById('HdRem').value.trim()  == document.getElementById('txtRem').value.trim() )
                         {  
                                  document.getElementById("lblRemError").innerHTML = "Please change the remark.";  
                                  document.getElementById("txtRem").focus();
                                  return false;               
                         }                     
                    }
                 
                   if ( document.getElementById("txtRem").value.trim().length>8000) 
                      {
                   
                               document.getElementById("lblRemError").innerHTML  = "Remark can't be greater than 8000."  ;
                               document.getElementById("txtRem").focus();
                               return false;
                     } 
          }
           catch(err){}
      
      }      
          
      function CheckOrUnckeckItemFromQlaification()
            {
                try
                {
               
                    var Counter=0;
                    var strProductivity="0";
                    var intLength=0;
                    var intRows=0;
                    var intTotalSelection=0;
                    var chkbox =document.getElementById("chkLstNIDTField");
                    if( chkbox !=null)
                    {
                        var inputArr = chkbox.getElementsByTagName('input');
                        var labelArr = chkbox.getElementsByTagName('label');
                        if( inputArr !=null)
                            {
                                 intLength=inputArr.length;   
                                 for(Counter=0;Counter<intLength;Counter++)
                                {
                                    var elementID='chkLstNIDTField_'+ Counter;
                                       
                                    if(document.getElementById(elementID).checked==true)
                                    {                                         
                                         intTotalSelection+=1;                                    
                                         if (labelArr[Counter].innerHTML.toUpperCase()=="PRODUCTIVITY")
                                            {
                                                  strProductivity="1" ;                                
                                            }
                                     }
                                 }   
                                         
                                if (strProductivity=="1")
                                {
                                    for(Counter=0;Counter<intLength;Counter++)
                                    {
                                           var elementID='chkLstNIDTField_'+ Counter;   
                                            if (labelArr[Counter].innerHTML.toUpperCase()!="PRODUCTIVITY")
                                            {
                                                  document.getElementById(elementID).disabled=true;
                                                  document.getElementById(elementID).checked=false;
                                            }
                                            else
                                            {
                                             document.getElementById(elementID).disabled=false;
                                            }                       
                                    }
                                }
                                else
                                {
                                    for(Counter=0;Counter<intLength;Counter++)
                                    {
                                        var elementID='chkLstNIDTField_'+ Counter;                                       
                                       
                                        if (labelArr[Counter].innerHTML.toUpperCase()=="PRODUCTIVITY")
                                            {
                                                  document.getElementById(elementID).disabled=true;
                                                  document.getElementById(elementID).checked=false;                                
                                            }
                                        else
                                            {
                                                  document.getElementById(elementID).disabled=false;   
                                            }
                                    }
                                } 
                                                                  
                                if(intTotalSelection==0)
                                {                      
                                    for(Counter=0;Counter<intLength;Counter++)
                                    {
                                           var elementID='chkLstNIDTField_'+ Counter;                               
                                           document.getElementById(elementID).disabled=false;
                                     }
                                }                
                        }                                                                           
                    }
               
                 }    
            catch(err){alert(err)}
         }
           
         function loadOnPage() 
            {
               Sys.WebForms.PageRequestManager.getInstance().add_endRequest(CheckOrUnckeckItemFromQlaification);
            }
            
            //Added by neeraj to open a popup for Action like Edit, Delete
            function findPos(obj) {
	            var curleft = curtop = 0;
	            if (obj.offsetParent) {
		            curleft = obj.offsetLeft
		            curtop = obj.offsetTop
		            while (obj = obj.offsetParent) {
			            curleft += obj.offsetLeft
			            curtop += obj.offsetTop
		            }
	            }
	            return [curleft,curtop];
            }
            
            function display_menu(parent,named)
            {
	            //get the named menu
	            var menu_element = document.getElementById(named);
	            //override the 'display:none;' style attribute
	            menu_element.style.display = "";
	            //get the placement of the element that invoked the menu...
	            var placement = findPos(parent);
	            //...and put the menu there
	            menu_element.style.left = placement[0] + "px";
	            menu_element.style.top = placement[1] + "px";
            }

            //Hide a named menu
            function hide_menu(named)
            {
	            //get the named menu
	            var menu_element = document.getElementById(named);
	            //hide it with a style attribute
	            menu_element.style.display = "none";
            }
            
            function Autohide_menu(named)
            {
               var grd=document.getElementById("GvProcessPayment"); 		       
               var divId;
               var menu_element;
               //alert(grd.rows.length);
               //alert(named);
		       if (grd !=null)
		       {
		            for(var i=1;i<grd.rows.length;i++)
                    {
//                        divId= 'div' + document.getElementById("GvProcessPayment").rows[i].cells[0].innerText;
                        divId= 'div' + i ;
                        if (divId!=null)
                        {
                            //alert(divId);
                            if (divId != named)
                            {
                                menu_element = document.getElementById(divId);
                                menu_element.style.display = "none";
                            }                            
                        }
		           }	
		       }    		   
            }
            
            //end Added ny neeraj to open a popup

    </script>

    <style type="text/css">
    .popup
    {
	    position:absolute;
	    border:solid 1px black;
	    background-color:white;
	    padding:4px;
    }
    
    .modalBackground {
	    background-color:#434040;
	    filter:alpha(opacity=40);
	    opacity:0.7;
    }

    .modalPopup {
	    background-color:#ffffdd;
	    border:8px solid #3a3434;
	    padding:3px;
	    width:250px;
	    background-color:#ffffff;

    }
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
    .modalCloseButton	{
	    background-image:url(../Images/strip_tab.jpg);
	    background-repeat:repeat-x;
	    background-color:#f9f9f9;	
	    font-family:Verdana;
	    font-size:13px;
	    color:#0457b7;
	    border-top:1px solid #0457b7;
	    border-bottom:2px solid #0457b7;
	    border-left:1px solid #0457b7;
	    border-right:1px solid #0457b7;
	    text-align:center;
	    vertical-align:middle;
	    text-decoration:none;
	    cursor:pointer ;
    }
    .msgcolor
    {
     color:#0457b7;
    }
    .DragableHeader	

    {
	    font-family:Verdana;
	    font-size:16px;
	    color:#0055b5;
	    white-space:nowrap;
	    font-weight:700;
    	
    }
    </style>
</head>
<body onload="loadOnPage();">
    <form id="form1" runat="server" defaultbutton="btnSearch" defaultfocus="txtChainCode">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="660">
        </asp:ScriptManager>
        <table width="860px" align="left" class="border_rightred" style="height: 486px;">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Incentive-&gt;</span><span class="sub_menu">Process&nbsp; Payment</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Process&nbsp; Payment</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <table width="900px" border="0" align="left" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="textbold" colspan="7" align="center">
                                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                    <asp:UpdatePanel ID="UpdQualPnl" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:Label ID="lblQualError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="6%" height="30px">
                                                                    &nbsp;</td>
                                                                <td width="15%" class="textbold">
                                                                    Group Name</td>
                                                                <td width="30%">
                                                                    <asp:TextBox ID="txtGroupName" CssClass="textbox" MaxLength="40" runat="server" TabIndex="1"
                                                                        Width="220px"></asp:TextBox><img src="../Images/lookup.gif" onclick="javascript:return PopupAgencyGroupForIncentive();"
                                                                            id="ImgAGroup" style="cursor: pointer;" alt="" runat="server" /></td>
                                                                <td style="width: 151px">
                                                                </td>
                                                                <td style="width: 109px">
                                                                    <span class="textbold">Chain Code</span></td>
                                                                <td width="21%">
                                                                    <asp:TextBox ID="txtChainCode" runat="server" CssClass="textbox" MaxLength="6" TabIndex="1"></asp:TextBox></td>
                                                                <td width="18%">
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3"
                                                                        AccessKey="S" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td height="25px">
                                                                    &nbsp;</td>
                                                                <td class="textbold">
                                                                    City</td>
                                                                <td>
                                                                    <asp:DropDownList ID="drpCity" CssClass="dropdownlist" Width="137px" runat="server"
                                                                        TabIndex="1">
                                                                    </asp:DropDownList></td>
                                                                <td style="width: 151px">
                                                                </td>
                                                                <td class="textbold" nowrap="nowrap" style="width: 109px">
                                                                    Country</td>
                                                                <td>
                                                                    <asp:DropDownList ID="drpCountry" CssClass="dropdownlist" Width="137px" runat="server"
                                                                        TabIndex="1">
                                                                    </asp:DropDownList></td>
                                                                <td>
                                                                    <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="3" Text="Export"
                                                                        AccessKey="E" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td height="30px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" nowrap="nowrap">
                                                                    Month</td>
                                                                <td class="textbold">
                                                                    <asp:DropDownList ID="drpMonthFrom" runat="server" CssClass="dropdownlist" TabIndex="1"
                                                                        Width="137px">
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
                                                                <td class="textbold" style="width: 151px">
                                                                </td>
                                                                <td nowrap="nowrap" class="textbold" style="width: 109px">
                                                                    Year&nbsp;</td>
                                                                <td>
                                                                    <asp:DropDownList ID="drpYearFrom" runat="server" CssClass="dropdownlist" TabIndex="1"
                                                                        Width="137px">
                                                                    </asp:DropDownList></td>
                                                                <td>
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3"
                                                                        AccessKey="R" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="7" style="height: 10px;">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td align="left" colspan="6" class="textbold" >
                                                                    <div id="DivFAColor" runat="server" visible="false">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    Note:</td>
                                                                                <td>
                                                                                    <asp:Label ID="LblRejected" runat="server" BackColor="Red" Text="&nbsp;&nbsp;&nbsp;"></asp:Label>
                                                                                    <b>Denotes Rejected payment.</b>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblFAColor" runat="server" BackColor="LightSeaGreen" Text="&nbsp;&nbsp;&nbsp;"></asp:Label>
                                                                                    <b>Denotes Payment advice has been generated and available in approval queue.</b></td>
                                                                            </tr>
                                                                             <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="LblSkipPay" runat="server" BackColor="Yellow" Text="&nbsp;&nbsp;&nbsp;"></asp:Label>
                                                                                    <b>Denotes Skip Payment</b></td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="7" style="height: 10px;">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td colspan="7">
                                                                                <asp:UpdatePanel ID="UpdPnl" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <table width="100%" cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td colspan="7">
                                                                                                    <asp:GridView ID="GvProcessPayment" runat="server" AutoGenerateColumns="False" TabIndex="2"
                                                                                                        Width="1200px" EnableViewState="true" AllowSorting="true" HeaderStyle-ForeColor="white">
                                                                                                        <Columns>
                                                                                                            <asp:BoundField HeaderText="BCaseId" ItemStyle-Width="45px" DataField="BC_ID" SortExpression="BC_ID"
                                                                                                                HeaderStyle-Width="45px"></asp:BoundField>
                                                                                                            <asp:BoundField HeaderText="Chain Code " DataField="CHAIN_CODE" SortExpression="CHAIN_CODE"
                                                                                                                HeaderStyle-Width="50px" ItemStyle-Width="50px" />
                                                                                                            <asp:BoundField HeaderText="Group Name " DataField="CHAIN_NAME" SortExpression="CHAIN_NAME"
                                                                                                                ItemStyle-Wrap="true" HeaderStyle-Width="130px" ItemStyle-Width="130px" />
                                                                                                            <asp:BoundField HeaderText="Billing Cycle " DataField="PAYMENT_CYCLE_NAME" SortExpression="PAYMENT_CYCLE_NAME"
                                                                                                                ItemStyle-Wrap="true" HeaderStyle-Width="90px" ItemStyle-Width="90px" />
                                                                                                            <asp:BoundField HeaderText="City " DataField="CITY" SortExpression="CITY" HeaderStyle-Width="70px"
                                                                                                                ItemStyle-Width="70px" />
                                                                                                            <asp:BoundField HeaderText="Aoffice " DataField="AOFFICE" SortExpression="AOFFICE"
                                                                                                                HeaderStyle-Width="45px" ItemStyle-Width="45px" />
                                                                                                            <asp:TemplateField HeaderText="Payment Period " HeaderStyle-Width="140px" ItemStyle-Width="140px">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblPaymentFromto" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PAYMENTPERIOD_FROM") + " - "  + DataBinder.Eval(Container.DataItem, "PAYMENTPERIOD_TO") %>' />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:BoundField HeaderText="Account Manager " DataField="ACCOUNTMANAGER_NAME" SortExpression="ACCOUNTMANAGER_NAME"
                                                                                                                HeaderStyle-Width="115px" ItemStyle-Width="115px" />
                                                                                                            <asp:BoundField HeaderText="Cheque Received" DataField="BR_CHK_RECEIVED" SortExpression="BR_CHK_RECEIVED"
                                                                                                                HeaderStyle-Width="100px" ItemStyle-Width="100px"></asp:BoundField>
                                                                                                            <asp:BoundField HeaderText="Qualification Average" DataField="QualiAvgNIDTFIELDS"
                                                                                                                ItemStyle-Wrap="true" ItemStyle-Width="140px" HeaderStyle-Width="140px"></asp:BoundField>
                                                                                                            <asp:TemplateField HeaderText="Action" ItemStyle-Wrap="false" ItemStyle-Width="104px"
                                                                                                                ItemStyle-VerticalAlign="Top">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:LinkButton ID="lnkPaymentMenu" CssClass="LinkButtons" Text="Payment..." runat="server"
                                                                                                                        CommandName="SelectPay"></asp:LinkButton>
                                                                                                                    <%--  <div class="popup" id="div<%# DataBinder.Eval(Container.DataItem, "BC_ID") %>"   style="display: none; width: 100px" onclick ="hide_menu('div<%# DataBinder.Eval(Container.DataItem, "BC_ID") %>');"  onmouseout="Autohide_menu('div<%# DataBinder.Eval(Container.DataItem, "BC_ID") %>');">--%>
                                                                                                                    <div class="popup" id="div<%# CType(Container, GridViewRow).RowIndex  +1 %>" style="display: none;
                                                                                                                        width: 96px" onclick="hide_menu('div<%# CType(Container, GridViewRow).RowIndex +1 %>');"
                                                                                                                        onmouseout="Autohide_menu('div<%# CType(Container, GridViewRow).RowIndex +1 %>');">
                                                                                                                        <asp:LinkButton ID="lnkPayment" CssClass="LinkButtons" Text="Payment" runat="server"
                                                                                                                            CommandName="SelectPay" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "BC_ID") + "|" + DataBinder.Eval(Container.DataItem, "CHAIN_CODE") %>'></asp:LinkButton>
                                                                                                                        <br />
                                                                                                                        <asp:LinkButton ID="LnkModQualification" CssClass="LinkButtons" Text="Modify Quali Avg."
                                                                                                                            OnClientClick="ShowPopupTabChange(this.id);" runat="server" CommandName="SelectModQual"
                                                                                                                            CommandArgument='<%# (CType(Container, GridViewRow).RowIndex).tostring() + "|" + DataBinder.Eval(Container.DataItem, "BC_ID") + "|" + DataBinder.Eval(Container.DataItem, "CHAIN_CODE") %>'></asp:LinkButton>
                                                                                                                        <br />
                                                                                                                        <asp:LinkButton ID="lnkHistory" CssClass="LinkButtons" Text="History" runat="server"
                                                                                                                            CommandName="SelectHistory" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "BC_ID") + "|" + DataBinder.Eval(Container.DataItem, "CHAIN_CODE") %>'></asp:LinkButton>
                                                                                                                        <asp:LinkButton ID="lnkPaymentSheet" Visible="false" CssClass="LinkButtons" Text="Display"
                                                                                                                            runat="server" CommandName="SelectPaymentSheet" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "BC_ID") + "|" + DataBinder.Eval(Container.DataItem, "CHAIN_CODE") %>'></asp:LinkButton>
                                                                                                                        <asp:HiddenField ID="hdInput" Value='<%# DataBinder.Eval(Container.DataItem, "BC_ID") + "|" + DataBinder.Eval(Container.DataItem, "CHAIN_CODE") %>'
                                                                                                                            runat="server" />
                                                                                                                        <asp:HiddenField ID="hdPayType" Value='<%# DataBinder.Eval(Container.DataItem, "PAYMENTTYPE")  %>'
                                                                                                                            runat="server" />
                                                                                                                        <asp:HiddenField ID="HdFirstTime" Value='<%# DataBinder.Eval(Container.DataItem, "UPFRONTFIRSTTIME")  %>'
                                                                                                                            runat="server" />
                                                                                                                        <asp:HiddenField ID="HdCurPayNo" Value='<%# DataBinder.Eval(Container.DataItem, "NO_OF_PAYMENT")  %>'
                                                                                                                            runat="server" />
                                                                                                                        <asp:HiddenField ID="HdMonth" Value='' runat="server" />
                                                                                                                        <asp:HiddenField ID="HdYear" Value='' runat="server" />
                                                                                                                        <asp:HiddenField ID="HdPLBCycle" Value='<%# Eval("PAYMENT_CYCLE_NAME") %>' runat="server" />
                                                                                                                        <asp:HiddenField ID="HdRejected" Value='<%# DataBinder.Eval(Container.DataItem, "REJECTED")  %>'
                                                                                                                            runat="server" />
                                                                                                                        <asp:HiddenField ID="HdPACreated" Value='<%# DataBinder.Eval(Container.DataItem, "PA_CREATED")  %>'
                                                                                                                            runat="server" />
                                                                                                                            
                                                                                                                      <asp:HiddenField ID="HdSkipPayment" Value='<%# DataBinder.Eval(Container.DataItem, "SKIPPAYMENT")  %>'
                                                                                                                            runat="server" />
                                                                                                                            
                                                                                                                        <%--<img  id="imgClose" src="../Images/sm_x.jpg" style="width:10px;height:10px;vertical-align:bottom;" runat="server" onclick="javascript:return hide_menu('div<%# DataBinder.Eval(Container.DataItem, "BC_ID") %>');" />--%>
                                                                                                                        <img src="../Images/X16.gif" id="ImgClose" alt="" runat="server" style="width: 10px;
                                                                                                                            height: 10px; vertical-align: bottom;" />
                                                                                                                    </div>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                                                        <RowStyle CssClass="textbold" />
                                                                                                        <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                        <PagerSettings PageButtonCount="5" />
                                                                                                    </asp:GridView>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="7" valign="top">
                                                                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                            <tr style="padding-top: 4pt; padding-bottom: 4pt">
                                                                                                                <td style="width: 30%" class="left">
                                                                                                                    <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                                                                        ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey"
                                                                                                                        ReadOnly="true"></asp:TextBox></td>
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
                                                                                                <td colspan="7">
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="7">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                                        <ContentTemplate>
                                                                                                            <table>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:Button ID="BtnConfirm" Text="" runat="server" CssClass="displayNone" />
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <cc1:ModalPopupExtender ID="mdlPopUpExt" runat="server" TargetControlID="BtnConfirm"
                                                                                                                            PopupDragHandleControlID="DivHeder" BackgroundCssClass="confirmationBackground"
                                                                                                                            PopupControlID="pnlPopup" CancelControlID="BtnCancel">
                                                                                                                        </cc1:ModalPopupExtender>
                                                                                                                        <asp:Panel ID="pnlPopup" runat="server" Width="600px" Height="310px" Style="display: none"
                                                                                                                            HorizontalAlign="Center" CssClass="confirmationPopup">
                                                                                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                                                <tr>
                                                                                                                                    <td class="strip_bluelogin" align="left" colspan="2">
                                                                                                                                        <div id="DivHeder">
                                                                                                                                            Amadeus agent management system
                                                                                                                                        </div>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td colspan="2" class="subheading">
                                                                                                                                    Qualification Average
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td height="20px" colspan="2">
                                                                                                                                        <asp:Label ID="lblRemError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td height="5px" colspan="2">
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td style="width: 100px;">
                                                                                                                                        &nbsp;&nbsp;&nbsp;</td>
                                                                                                                                    <td align="left">
                                                                                                                                        <asp:CheckBoxList ID="chkLstNIDTField" CssClass="textbold" RepeatDirection="Horizontal"
                                                                                                                                            RepeatColumns="4" runat="server" TabIndex="1" Width="400px" AutoPostBack="false">
                                                                                                                                        </asp:CheckBoxList>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td align="center" colspan="2" style="height: 25px;">
                                                                                                                                        <table style="width: 96%;" border="0">
                                                                                                                                            <tr>
                                                                                                                                                <td align="center">
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
                                                                                                                                    <td class="textbold">
                                                                                                                                        Remark</td>
                                                                                                                                    <td>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td colspan="2">
                                                                                                                                        <asp:TextBox ID="txtRem" runat="server" Text="" TextMode="MultiLine" Width="470px"
                                                                                                                                            TabIndex="1" Rows="4" />
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td height="2px" colspan="2">
                                                                                                                                        <asp:HiddenField ID="HdInputData" runat="server" />
                                                                                                                                        <asp:HiddenField ID="HdSelectedCount" runat="server" />
                                                                                                                                        <asp:HiddenField ID="HdRem" runat="server" Value="" />
                                                                                                                                        <asp:HiddenField ID="HdPLB" runat="server" Value="" />
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td colspan="2" align="center">
                                                                                                                                        <table border="0" width="25%" cellspacing="10" cellpadding="1">
                                                                                                                                            <tr>
                                                                                                                                                <td align="center">
                                                                                                                                                    <asp:Button ID="btnUpdate" runat="server" CssClass="button" Text="Update" Width="60px"
                                                                                                                                                        TabIndex="1" OnClientClick="return MandatoryField();" OnClick="btnUpdate_Click" /></td>
                                                                                                                                                <td align="center">
                                                                                                                                                    <asp:Button ID="BtnCancel" CssClass="button" runat="server" Text="Close" Width="60px"
                                                                                                                                                        TabIndex="1" OnClientClick="$find('mdlPopUpExt').hide(); return false;" />
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
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="7">
                                                                                                    <cc1:ModalPopupExtender BackgroundCssClass="confirmationBackground" DropShadow="false"
                                                                                                        PopupControlID="PnlPrrogress" TargetControlID="PnlPrrogress" RepositionMode="RepositionOnWindowResizeAndScroll"
                                                                                                        ID="ModalPopupExtender2" runat="server" CacheDynamicResults="false">
                                                                                                    </cc1:ModalPopupExtender>
                                                                                                    <asp:Panel ID="PnlPrrogress" runat="server" Width="600px" Height="280px" Style="display: none"
                                                                                                        HorizontalAlign="Center" CssClass="confirmationPopup">
                                                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                            <tr>
                                                                                                                <td class="strip_bluelogin" align="left">
                                                                                                                    Amadeus agent management system
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td align="center" valign="middle" style="height: 200px">
                                                                                                                    <img src="../Images/er.gif" id="img" runat="server" alt="" />
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
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
                <td>
                    <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" Width="73px" CssClass="textboxgrey"
                        Visible="false"></asp:TextBox>
                    <input type="hidden" id="hdCtrlId" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
