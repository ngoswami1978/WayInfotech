<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INCUP_PaymentProcessUpfront.aspx.vb"
    Inherits="Incentive_INCUP_PaymentProcessUpfront" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Process Payment</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <style type="text/css">

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

    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>

    <script type="text/javascript" language="javascript">
         
            function PaymentSheetReport(objBCID,objChainCode,objMonth,objYear,objPayTime,objCurPayNo,ObjPayId,ObjPeriodF,ObjPeriodT,ObjPLB)
          {
            
          
           //var Param="BCaseID=" + objBCID + "&Chain_Code=" + objChainCode  +"&Month=" + objMonth + "&Year=" + objYear   +  "&PayType=" + objPayTime  + "&CurPayNo=" + objCurPayNo +   "&PayId=" + ObjPayId + "&Case=PaymentSheet" ;
             var Param="BCaseID=" + objBCID + "&Chain_Code=" + objChainCode  +"&Month=" + objMonth + "&Year=" + objYear   +  "&PayType=" + objPayTime  + "&CurPayNo=" + objCurPayNo +   "&PayId=" + ObjPayId  +   "&PeriodFrom=" + ObjPeriodF +   "&PeriodTo=" + ObjPeriodT +   "&PLB=" + ObjPLB + "&Case=PaymentSheet" ;
          
           // alert (Param);
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
         
          function  Payment(objBCID,objChainCode,objMonth,objYear)
          {
            var Param="BCId=" + objBCID + "&ChainCode=" + objChainCode  +"&Month=" + objMonth + "&Year=" + objYear ;
            var type;
            type= "INCUP_PaymentProcess.aspx?" + Param;
            window.location.href =type;
            return false;
          }
          
             
                     function PaymentHistory(BCaseID, ChainCode,objMonth,objYear)
        {          
              var type;       
             type = "../Incentive/INCSR_PaymentHistoryDetails.aspx?Action=Payment&Chain_Code=" + ChainCode + "&BCaseID=" + BCaseID +"&Month=" + objMonth + "&Year=" + objYear ;
   	            window.open(type,"IncDetails","height=400,width=600,top=30,left=20,scrollbars=1,status=1,resizable=1");	        
             // window.location ="MSSR_BcaseDetails.aspx?Chain_Code=" + ChainCode + "&BCaseID=" + BCaseID; 
              return false;
        }   
          
         
         function CheckValidation()
         {
         }
         
         function ShowPlan()
         {
           if ( window.document.getElementById ('pnlPlan').className=='displayNone')
             {
              window.document.getElementById ('BtnShowPlan').value="Hide Plan";
              window.document.getElementById ('pnlPlan').className ="displayBlock";
             }
             else
             {
                 window.document.getElementById ('BtnShowPlan').value ="Show Plan";
                 window.document.getElementById ('pnlPlan').className="displayNone";
             }
             return false;
            
         }
                 function DetailsFunction(BCaseID, ChainCode)
        {          
              var type;       
             type = "../Incentive/INCUP_BacseDetails.aspx?Action=Payment&Chain_Code=" + ChainCode + "&BCaseID=" + BCaseID; 
   	            window.open(type,"IncDetails","height=630,width=1000,top=30,left=20,scrollbars=1,status=1,resizable=1");	        
             // window.location ="MSSR_BcaseDetails.aspx?Chain_Code=" + ChainCode + "&BCaseID=" + BCaseID; 
              return false;
        }   
         
         
function checknumericForInc(objCtrlid)
{
var tempVal=document.getElementById(objCtrlid).value;
var validVal="";
for(var i=0;i<tempVal.length;i++)
{
if((parseInt(tempVal.substr(i,1)))>= 0 || (parseInt(tempVal.substr(i,1))<= 9))
{validVal+=tempVal.substr(i,1);}else{ }
}
document.getElementById(objCtrlid).value=validVal;
}

function checknumericGreaterZeroForInc(objCtrlid)
{
var tempVal=document.getElementById(objCtrlid).value;
var validVal="";
    for(var i=0;i<tempVal.length;i++)
    {
        if((parseInt(tempVal.substr(i,1)))>= 0 || (parseInt(tempVal.substr(i,1))<= 9))
        {validVal+=tempVal.substr(i,1);}else{ }
    }

    if (validVal=="0")
    {
        validVal=""
        document.getElementById(objCtrlid).value=validVal;
        document.getElementById("lblError").innerText="Quantity must be greater than zero";
        return false;
    }
    else
    {
        document.getElementById(objCtrlid).value=validVal;
    }

}

function checknumericWithDotForInc(objCtrlid)
{
//{debugger;}
var countDec=0;
var tempVal=document.getElementById(objCtrlid).value;

var validVal="";
for(var i=0;i<tempVal.length;i++)
{

if (tempVal.substr(i,1)=='.')
{
  countDec=countDec+1;
}
//alert(countDec);
if(((parseInt(tempVal.substr(i,1)))>= 0 || (parseInt(tempVal.substr(i,1))<= 9) || tempVal.substr(i,1)=='.') && (countDec <= 1 ) )
{validVal+=tempVal.substr(i,1);}else{ }
}
document.getElementById(objCtrlid).value=validVal;

}

function CheckNumericWithoutZero(objCtrlid)
{
var tempVal=document.getElementById(objCtrlid).value;
var validVal="";
for(var i=0;i<tempVal.length;i++)
{
if((parseInt(tempVal.substr(i,1)))>= 1 || (parseInt(tempVal.substr(i,1))<= 9))
{validVal+=tempVal.substr(i,1);}else{ }
}
document.getElementById(objCtrlid).value=validVal;
return true;
}





function ChangeAmountPrevious( objExumption,objAmount,objAmountAfterExumption,GvProcessPayment)
{
//{debugger;}
try
{
    var Amount=document.getElementById(objAmount).value;
    var Exumption=document.getElementById(objExumption).value;
    var AmountAfterExumption;
    AmountAfterExumption = Amount - (((Amount)* (Exumption))/100);  
    AmountAfterExumption =AmountAfterExumption.toFixed(2);  
    document.getElementById(objAmountAfterExumption).value=AmountAfterExumption;       
        
        var grd=document.getElementById(GvProcessPayment);
        var footerrow=grd.rows.length-1;  
        var GrandSumAfterExumption=0;  
        
                      for(var i=1;i<grd.rows.length-1;i++)
                        {
                          var amt =parseFloat(grd.rows[i].cells[5].children[0].value);
                         // alert(amount);
                          GrandSumAfterExumption= GrandSumAfterExumption + amt;
                          GrandSumAfterExumption=GrandSumAfterExumption;
                         // alert(GrandSumAfterExumption);
                        }
      
        grd.rows[footerrow].cells[5].children[0].value=GrandSumAfterExumption.toFixed(2);
      //  alert(grd.rows[footerrow].cells[5].children[0].value);

} catch(err){}
         
       
  
     
}




function ChangeAmount(objActualSegment, objExumption,objSegmentAfterExumption,objRate,objFinalAmount,GvProcessPayment,objtxtGrandFinalAmt,objExtraSum)
{
//{debugger;}
try
{
    var ActualSegment=document.getElementById(objActualSegment).value;
    var Exumption=document.getElementById(objExumption).value;
    var SegmentAfterExumption;
    SegmentAfterExumption = ActualSegment - (((ActualSegment)* (Exumption))/100);  
    SegmentAfterExumption =SegmentAfterExumption.toFixed(2);
    
    var Rate=document.getElementById(objRate).value;
    var FinalAmount;
    FinalAmount=SegmentAfterExumption*Rate;
    FinalAmount =FinalAmount.toFixed(2);
     
    document.getElementById(objSegmentAfterExumption).value=SegmentAfterExumption;   
    document.getElementById(objFinalAmount).value=FinalAmount;   
    
    
        
        
        var grd=document.getElementById(GvProcessPayment);
        var footerrow=grd.rows.length-1;  
       
        var GrandSumSegmentAfterExumption=0;          
                      for(var i=1;i<grd.rows.length-1;i++)
                        {
                          var amt =parseFloat(grd.rows[i].cells[3].children[0].value);
                         // alert(amount);
                          GrandSumSegmentAfterExumption= GrandSumSegmentAfterExumption + amt;
                          GrandSumSegmentAfterExumption=GrandSumSegmentAfterExumption;
                         // alert(GrandSumAfterExumption);
                        }
      
        grd.rows[footerrow].cells[3].children[0].value=GrandSumSegmentAfterExumption.toFixed(2);
        
        
        
           var GrandSumAfterExumption=0;          
                      for(var i=1;i<grd.rows.length-1;i++)
                        {
                          var amt =parseFloat(grd.rows[i].cells[5].children[0].value);
                         // alert(amount);
                          GrandSumAfterExumption= GrandSumAfterExumption + amt;
                          GrandSumAfterExumption=GrandSumAfterExumption;
                         // alert(GrandSumAfterExumption);
                        }
      
        grd.rows[footerrow].cells[5].children[0].value=GrandSumAfterExumption.toFixed(2);
        
        var TotalGrandAmt;
        var TotalGroundAmt =parseFloat(GrandSumAfterExumption) + parseFloat(objExtraSum);
        
         document.getElementById(objtxtGrandFinalAmt).value=TotalGroundAmt.toFixed(2);;   
    
    
        
      //  alert(grd.rows[footerrow].cells[5].children[0].value);

} catch(err){}
         
       
  
     
}


function ChangeAmountForFixedPayment(objActualSegment, objExumption,objSegmentAfterExumption,objRate,objFinalAmount,GvProcessPayment,objtxtGrandFinalAmt,objExtraSum)
{
//{debugger;}
try
{
    var ActualSegment=document.getElementById(objActualSegment).value;
    var Exumption=document.getElementById(objExumption).value;
    var SegmentAfterExumption;
    SegmentAfterExumption = ActualSegment - (((ActualSegment)* (Exumption))/100);  
    SegmentAfterExumption =SegmentAfterExumption.toFixed(2);
    
    var Rate=document.getElementById(objRate).value;
    var FinalAmount;
    FinalAmount=parseFloat(SegmentAfterExumption) + parseFloat(Rate);
    FinalAmount =FinalAmount.toFixed(2);
     
    document.getElementById(objSegmentAfterExumption).value=SegmentAfterExumption;   
    document.getElementById(objFinalAmount).value=FinalAmount;         
        
        var grd=document.getElementById(GvProcessPayment);
        var footerrow=grd.rows.length-1;  
       
        var GrandSumSegmentAfterExumption=0;          
                      for(var i=1;i<grd.rows.length-1;i++)
                        {
                          var amt =parseFloat(grd.rows[i].cells[3].children[0].value);
                         // alert(amount);
                          GrandSumSegmentAfterExumption= GrandSumSegmentAfterExumption + amt;
                          GrandSumSegmentAfterExumption=GrandSumSegmentAfterExumption;
                         // alert(GrandSumAfterExumption);
                        }
      
        grd.rows[footerrow].cells[3].children[0].value=GrandSumSegmentAfterExumption.toFixed(2);
        
        
        
           var GrandSumAfterExumption=0;          
                      for(var i=1;i<grd.rows.length-1;i++)
                        {
                          var amt =parseFloat(grd.rows[i].cells[5].children[0].value);
                         // alert(amount);
                          GrandSumAfterExumption= GrandSumAfterExumption + amt;
                          GrandSumAfterExumption=GrandSumAfterExumption;
                         // alert(GrandSumAfterExumption);
                        }
      
        grd.rows[footerrow].cells[5].children[0].value=GrandSumAfterExumption.toFixed(2);
        
        var TotalGrandAmt;
        var TotalGroundAmt =parseFloat(GrandSumAfterExumption) + parseFloat(objExtraSum);
        
         document.getElementById(objtxtGrandFinalAmt).value=TotalGroundAmt.toFixed(2);;   
    
    
        
      //  alert(grd.rows[footerrow].cells[5].children[0].value);

} catch(err){}
         
       
  
     
}



  function validateDecimalValue(id)
{

       var  dotcount=0; 
       if(document.getElementById(id).value =='')
        {
            
        }
        else
        {
        var strValue = document.getElementById(id).value;
        
            reg = new RegExp("^[0-9.]+$"); 
            if(reg.test(strValue) == false) 
            {
                document.getElementById('<%=lblError.ClientId%>').innerHTML ='Only numeric with decimal is allowed.';
                document.getElementById(id).focus();
                return false;
             }
             
               for (var i=0; i < strValue.length; i++) 
	                    {
		                    if (strValue.charAt(i)=='.')
		                     { 
		                     dotcount=dotcount+1;
		                      }
		               }
                        if(dotcount>1)
                        {
                             document.getElementById(id).focus();
                             document.getElementById('<%=lblError.ClientId%>').innerHTML ='Only numeric with decimal is allowed.';
                             return false;
                        }
                         dotcount=0; 
            
        
        }
          document.getElementById('<%=lblError.ClientId%>').innerHTML ="";
        
}


function ValidateRegisterPage()
{
try
{

     //debugger;
     if (document.getElementById("txtRemarks").value.trim().length>300)
        {
             document.getElementById("lblError").innerHTML="Business Remarks can't be greater than 300 characters."
             document.getElementById("txtRemarks").focus();
             return false;
        }  
        if (document.getElementById("txtPayAppRemarks").value.trim().length>300)
        {
             document.getElementById("lblError").innerHTML="Payment Approval Remarks can't be greater than 300 characters."
             document.getElementById("txtPayAppRemarks").focus();
             return false;
        }  
        
       if(document.getElementById('GvProcessPayment')!=null)
       {
                             var dotcount=0;
                             for(intcnt=1;intcnt<=document.getElementById('GvProcessPayment').rows.length-1;intcnt++)
                            {   
                                    if (document.getElementById('GvProcessPayment').rows[intcnt].cells[2].children[0] !=null )
                                    {
                                              if (document.getElementById('GvProcessPayment').rows[intcnt].cells[2].children[0].type=="text")
                                        { 
                                        var strValue = document.getElementById('GvProcessPayment').rows[intcnt].cells[2].children[0].value.trim();
	                                    if (strValue != '')
                                              {
                                                reg = new RegExp("^[0-9.]+$"); 
                                                if(reg.test(strValue) == false) 
                                                {
                                                  document.getElementById('<%=GvProcessPayment.ClientID%>').rows[intcnt].cells[2].children[0].focus();
                                                  document.getElementById('lblError').innerHTML ='Only numeric with decimal is allowed.'; 
                                                  return false;
                                                 }
                                              }
                                                for (var i=0; i < strValue.length; i++) 
	                                            {
		                                            if (strValue.charAt(i)=='.')
		                                             { 
		                                             dotcount=dotcount+1;
		                                              }
		                                       }
                                                if(dotcount>1)
                                                {
                                                    document.getElementById('<%=GvProcessPayment.ClientID%>').rows[intcnt].cells[2].children[0].focus();
                                                    document.getElementById("lblError").innerHTML='Only numeric with decimal is allowed.';
                                                    return false;
                                                }
                                                
                                         dotcount=0; 
                                            if (strValue != '')
                                              {
                                                    //  alert(strValue);
                                                  if(parseFloat(strValue)>100)
                                                    {
                                                       document.getElementById('<%=GvProcessPayment.ClientID%>').rows[intcnt].cells[2].children[0].focus();
                                                       document.getElementById("lblError").innerHTML="Exemption can't be greater than 100.";
                                                       return false;
                                                    }
                                              }
                                         
                                         }
                                    
                                    }
                            //}
                          }
  
                             for(intcnt=1;intcnt<=document.getElementById('GvProcessPayment').rows.length-1;intcnt++)
                            {   
                                    if (document.getElementById('GvProcessPayment').rows[intcnt].cells[5].children[1] !=null )
                                    {
                                              if (document.getElementById('GvProcessPayment').rows[intcnt].cells[5].children[1].type=="text")
                                        { 
                                        var strValue = document.getElementById('GvProcessPayment').rows[intcnt].cells[5].children[1].value.trim();
	                                    if (strValue != '')
                                              {
                                                reg = new RegExp("^[0-9.]+$"); 
                                                if(reg.test(strValue) == false) 
                                                {
                                                  document.getElementById('<%=GvProcessPayment.ClientID%>').rows[intcnt].cells[5].children[1].focus();
                                                  document.getElementById('lblError').innerHTML ='Only numeric with decimal is allowed.'; 
                                                  return false;
                                                 }
                                              }
                                                for (var i=0; i < strValue.length; i++) 
	                                            {
		                                            if (strValue.charAt(i)=='.')
		                                             { 
		                                             dotcount=dotcount+1;
		                                              }
		                                       }
                                                if(dotcount>1)
                                                {
                                                    document.getElementById('<%=GvProcessPayment.ClientID%>').rows[intcnt].cells[5].children[1].focus();
                                                    document.getElementById("lblError").innerHTML='Only numeric with decimal is allowed.';
                                                    return false;
                                                }
                                                
                                         dotcount=0; 
                                         }
                                    
                                    }
                            //}
                          }
                          
                           //------------------This code for Rate rem--------------------
                            for(intcnt=1;intcnt<=document.getElementById('GvProcessPayment').rows.length-1;intcnt++)
                            {   
                                    if (document.getElementById('GvProcessPayment').rows[intcnt].cells[5].children[0] !=null )
                                    {
                                       if (document.getElementById('GvProcessPayment').rows[intcnt].cells[5].children[1] !=null )
                                       {     
                                           if (document.getElementById('GvProcessPayment').rows[intcnt].cells[5].children[0].type=="text")
                                            { 
                                                   if (document.getElementById('GvProcessPayment').rows[intcnt].cells[5].children[1].type=="text")
                                                    {      
                                                       if (document.getElementById('GvProcessPayment').rows[intcnt].cells[7].children[0] !=null )
                                                         {  
                                                           if (document.getElementById('GvProcessPayment').rows[intcnt].cells[7].children[0].type=="textarea")
                                                             {                                                                   
                                                                 if(document.getElementById('GvProcessPayment').rows[intcnt].cells[5].children[1].value.trim()!='')
                                                                  {     
                                                                      if ( parseInt(document.getElementById('GvProcessPayment').rows[intcnt].cells[5].children[0].value)!=parseInt(document.getElementById('GvProcessPayment').rows[intcnt].cells[5].children[1].value))
                                                                          {
                                                                                if (document.getElementById('GvProcessPayment').rows[intcnt].cells[7].children[0].value.trim()=='')
                                                                                {
                                                                                        document.getElementById('<%=GvProcessPayment.ClientID%>').rows[intcnt].cells[7].children[0].focus();
                                                                                        document.getElementById("lblError").innerHTML='Remark is mandatory.';
                                                                                        return false;
                                                                                }
                                                                          }                                                                
                                                                   } 
                                                                 if (document.getElementById('GvProcessPayment').rows[intcnt].cells[7].children[0].value.trim().length>8000)
                                                                    {
                                                                         document.getElementById("lblError").innerHTML="Remarks can't be greater than 8000 characters."
                                                                         document.getElementById('GvProcessPayment').rows[intcnt].cells[7].children[0].focus();
                                                                         return false;
                                                                    }                                                         
                                                              }   
                                                         }
                                                    
                                                    }
                                            }
                                       }                                     
                                    }
                            }
                          //------------------This code for Rate rem-------------------- 
        }               
  
        ShowPopupTabChange(1);    
  
  }catch(err){}

}

    </script>

</head>
<body>
    <form id="form1" runat="server" defaultfocus="GvProcessPayment">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="660">
        </asp:ScriptManager>
        <table width="990px" align="left" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Incentive-&gt;</span><span class="sub_menu">Process&nbsp; Payment</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Process&nbsp; Payment</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="1" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td class="textbold" colspan="6" align="center">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                            </td>
                                                            <td align="center" class="textbold" colspan="1">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6" style="height: 5px;">
                                                                <asp:Panel ID="pnlDetails" runat="server" CssClass="displayBlock" Width="100%">
                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Panel ID="PnlGroupDetails" runat="server" Width="860px" Visible="true">
                                                                                    <table width="100%" border="0" cellspacing="1" cellpadding="1">
                                                                                        <tr>
                                                                                            <td class="subheading" colspan="6" width="840">
                                                                                                Group Details<span id="Spn" runat="server" style="padding-left: 300px;"><asp:Image
                                                                                                    Visible="false" ID="ImgEndyearSettelement" runat="server" ImageUrl="~/Images/img_info.jpg"
                                                                                                    ToolTip="Year End Settlement" /></span></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="textbold" style="width: 15%">
                                                                                                Group Name</td>
                                                                                            <td colspan="4" style="width: 64%">
                                                                                                <asp:TextBox ID="txtGroupName" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                    TabIndex="1" Width="533px" ReadOnly="True"></asp:TextBox>
                                                                                            </td>
                                                                                            <td align="center" colspan="1" style="width: 21%">
                                                                                                <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="2"
                                                                                                    AccessKey="S" Width="130px" /></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="textbold" style="width: 15%; height: 23px;">
                                                                                                Chain Code</td>
                                                                                            <td style="width: 23%; height: 23px;">
                                                                                                <asp:TextBox ID="txtChainCode" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                    ReadOnly="True" TabIndex="1" Width="181px"></asp:TextBox></td>
                                                                                            <td class="textbold" style="width: 3%; height: 23px;">
                                                                                            </td>
                                                                                            <td class="textbold" style="width: 15%; height: 23px;">
                                                                                                Account Manager</td>
                                                                                            <td class="textbold" style="width: 23%; height: 23px;">
                                                                                                <asp:TextBox ID="txtActManager" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                    ReadOnly="True" TabIndex="1" Width="181px"></asp:TextBox></td>
                                                                                            <td align="center" class="textbold" style="width: 21%; height: 23px">
                                                                                                <asp:Button ID="BtnReset" CssClass="button" runat="server" Text="Reset" TabIndex="2"
                                                                                                    AccessKey="R" Width="130px" /></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="textbold" style="width: 15%;">
                                                                                                City</td>
                                                                                            <td style="width: 23%;">
                                                                                                <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                                                                    TabIndex="1" Width="181px"></asp:TextBox></td>
                                                                                            <td class="textbold" style="width: 3%;">
                                                                                            </td>
                                                                                            <td class="textbold" style="width: 15%;">
                                                                                                Billing Cycle</td>
                                                                                            <td class="textbold" style="width: 23%;">
                                                                                                <asp:TextBox ID="txtBillCycle" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                    ReadOnly="True" TabIndex="1" Width="181px"></asp:TextBox></td>
                                                                                            <td class="textbold" style="width: 21%" align="center">
                                                                                                <asp:Button ID="BtnBCase" CssClass="button" runat="server" Text="Business Case" TabIndex="2"
                                                                                                    AccessKey="B" Width="130px" /></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="textbold" style="width: 15%;" valign="top">
                                                                                                Aoffice</td>
                                                                                            <td style="width: 23%;" valign="top">
                                                                                                <asp:TextBox ID="txtAoffice" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                    ReadOnly="True" TabIndex="1" Width="181px"></asp:TextBox></td>
                                                                                            <td class="textbold" style="width: 3%;">
                                                                                            </td>
                                                                                            <td class="textbold" style="width: 15%;" valign="top">
                                                                                                Payment Period</td>
                                                                                            <td class="textbold" style="width: 23%;" valign="top">
                                                                                                <asp:TextBox ID="TxtPayPeriod" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                    ReadOnly="True" TabIndex="1" Width="181px"></asp:TextBox></td>
                                                                                            <td class="textbold" style="width: 21%" valign="top" align="center">
                                                                                                <asp:Button ID="BtnPaymentHistory" CssClass="button" runat="server" Text="Past Payment History"
                                                                                                    TabIndex="2" AccessKey="B" Width="130px" /></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="textbold" style="width: 15%" valign="top">
                                                                                                Payment Type</td>
                                                                                            <td style="width: 23%" valign="top">
                                                                                                <asp:TextBox ID="txtPaymentType" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                    ReadOnly="True" TabIndex="1" Width="181px"></asp:TextBox></td>
                                                                                            <td class="textbold" style="width: 3%">
                                                                                            </td>
                                                                                            <td class="textbold" style="width: 15%" valign="top">
                                                                                                Adjustment</td>
                                                                                            <td class="textbold" style="width: 23%" valign="top">
                                                                                                <asp:TextBox ID="txtAdjustment" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                    ReadOnly="True" TabIndex="1" Width="181px"></asp:TextBox></td>
                                                                                            <td align="center" class="textbold" style="width: 21%" valign="top">
                                                                                                <asp:Button ID="BtnPaymentSheetReport" runat="server" AccessKey="B" CssClass="button"
                                                                                                    TabIndex="2" Text="Payment Detail Sheet" Width="164px" /></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="textbold" style="width: 15%; height: 23px" valign="top">
                                                                                                Payment Term</td>
                                                                                            <td style="width: 23%;" valign="top">
                                                                                                <asp:TextBox ID="txtPaymentTerm" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                    ReadOnly="True" TabIndex="1" Width="181px"></asp:TextBox></td>
                                                                                            <td class="textbold" style="width: 3%; height: 23px">
                                                                                            </td>
                                                                                            <td class="textbold" style="width: 15%; height: 23px" valign="top">
                                                                                                Signup Adjustable
                                                                                            </td>
                                                                                            <td class="textbold" style="width: 23%; height: 23px" valign="top">
                                                                                                <asp:TextBox ID="txtSignupAdjustable" TabIndex="1" runat="server" CssClass="textboxgrey"
                                                                                                    Width="181px" MaxLength="50" ReadOnly="True"></asp:TextBox>
                                                                                            </td>
                                                                                            <td class="textbold" style="width: 21%; height: 23px" valign="top" align="center">
                                                                                                <asp:Button ID="BtnSendForApproval" CssClass="button" runat="server" Text="Generate  Payment Advice"
                                                                                                    TabIndex="2" AccessKey="B" Width="164px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="textbold" style="width: 15%; height: 23px" valign="top">
                                                                                                Commitment</td>
                                                                                            <td style="width: 23%;" valign="top">
                                                                                                <asp:TextBox ID="TxtConPercentage" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                    ReadOnly="True" TabIndex="1" Width="181px"></asp:TextBox></td>
                                                                                            <td class="textbold" style="width: 3%; height: 23px">
                                                                                            </td>
                                                                                            <td class="textbold" style="width: 15%" valign="top">
                                                                                                <span id="SpnSignAmt" runat="server" visible="false">Signup Amount</span>
                                                                                            </td>
                                                                                            <td class="textbold" style="width: 23%" valign="top">
                                                                                                <asp:TextBox ID="TxtSpnSignAmt" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                    Visible="false" ReadOnly="True" TabIndex="1" Width="181px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td align="center" class="textbold" style="width: 21%" valign="top"><asp:Button accessKey="B" id="BtnSkipPayment" tabIndex="2" runat="server" CssClass="button" Width="164px" Text="Skip Payment"></asp:Button>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="displayNone">
                                                                                            <td class="textbold" style="width: 15%" valign="top">
                                                                                                Qualification Avg.
                                                                                            </td>
                                                                                            <td colspan="4" style="width: 64%">
                                                                                                <asp:TextBox ID="txtQualAvg" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                                    TabIndex="1" Width="525px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td class="textbold" style="width: 21%" valign="top">
                                                                                                <asp:TextBox ID="TxtMinSegment" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                                    Visible="false" TabIndex="1" Width="125px"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="6" align="center" style="width: 830px; height: 30px">
                                                                                                <asp:UpdateProgress ID="UpDateProgress1" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="10"
                                                                                                    runat="server">
                                                                                                    <ProgressTemplate>
                                                                                                        <img alt="Loading.." src="../Images/loading.gif" id="imgLoad" runat="server" />
                                                                                                    </ProgressTemplate>
                                                                                                </asp:UpdateProgress>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                        <!--
                                                                                                           End of Inccentive Plan
                                                                                                     -->
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                            <td colspan="1" style="height: 5px">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6" width="100%">
                                                                <asp:GridView ID="GvProcessPayment" runat="server" AutoGenerateColumns="False" TabIndex="1"
                                                                    Width="100%" EnableViewState="true" AllowSorting="false" HeaderStyle-ForeColor="white"
                                                                    ShowFooter="true">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Productivity Type">
                                                                            <ItemTemplate>
                                                                                <asp:Label class="textbold" ID="lblProductivity" runat="server" Text='<%# Eval("PRODUCTIVITYTYPE") %>'></asp:Label>
                                                                                <asp:HiddenField ID="hdProductivityId" runat="server" Value='<%# Eval("PRODUCTIVITYTYPEID") %>' />
                                                                                <asp:HiddenField ID="hdPayMentId" runat="server" Value='<%# Eval("PAYMENT_ID") %>' />
                                                                                <asp:HiddenField ID="hdNotInBCase" runat="server" Value='<%# Eval("ISCHECKED") %>' />
                                                                                <asp:HiddenField ID="hdRowID" runat="server" Value='<%# Eval("ROWID") %>' />
                                                                                <asp:HiddenField ID="hdHL" runat="server" Value='<%# Eval("HL") %>' />
                                                                                <asp:HiddenField ID="hdROI" runat="server" Value='<%# Eval("ROI") %>' />
                                                                                <asp:HiddenField ID="hdNidtFields" runat="server" Value='<%# Eval("NIDTFIELDS") %>' />
                                                                                <asp:HiddenField ID="hdStandardSeg" runat="server" Value='<%# Eval("STANDARSEGMENT") %>' />
                                                                                <asp:HiddenField ID="HdRate" runat="server" Value='<%# Eval("RATE") %>' />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Standard Segment">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtStandardSegment" runat="server" MaxLength="5" CssClass="textboxgrey right"
                                                                                    TabIndex="1" ReadOnly="true" Text='<%# Eval("STANDARSEGMENT") %>' Width="96%"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="center" Width="90px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Exemption %">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtExumption" runat="server" MaxLength="5" CssClass="textbox right"
                                                                                    TabIndex="1" OnTextChanged="txtExumption_TextChanged" AutoPostBack="true" Text='<%# Eval("EXCEMPTION") %>'
                                                                                    Width="96%"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="90px" VerticalAlign="top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Exempted Segment">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtActulaSegment" runat="server" MaxLength="5" CssClass="textboxgrey right"
                                                                                    TabIndex="1" ReadOnly="true" Text='<%# Eval("SEGMENT") %>' Width="96%"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="90px" VerticalAlign="top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Calculated Segment">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtCalCulatedSegment" ReadOnly="true" runat="server" MaxLength="12"
                                                                                    TabIndex="1" CssClass="textboxgrey right" Text='<%# Eval("SEGMENT") %>' Width="96%"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:TextBox ID="txtTotalCalCulatedSegment" ReadOnly="true" runat="server" MaxLength="12"
                                                                                    TabIndex="1" CssClass="textboxgrey right" Text='<%# Eval("SEGMENT") %>' Width="96%"></asp:TextBox>
                                                                            </FooterTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" Width="90px" />
                                                                            <ItemStyle HorizontalAlign="Right" Width="90px" VerticalAlign="top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Standard Rate / Revised Rate">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtStdRate" runat="server" MaxLength="5" CssClass="textboxgrey right"
                                                                                    TabIndex="1" ReadOnly="true" Text='<%# Eval("STANDARDRATE") %>' Width="47%"></asp:TextBox>&nbsp;<asp:TextBox
                                                                                        ID="txtRate" runat="server" MaxLength="5" CssClass="textbox right" TabIndex="1"
                                                                                        Text='<%# Eval("RATE") %>' Width="45%" OnTextChanged="txtRate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" Width="210px" />
                                                                            <ItemStyle HorizontalAlign="Right" Width="210px" VerticalAlign="top" Wrap="true" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Amount After Exemption">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtFinalAmount" ReadOnly="true" runat="server" MaxLength="12" CssClass="textboxgrey right"
                                                                                    TabIndex="1" Text='<%# Eval("SEGMENT") %>' Width="96%"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:TextBox ID="txtGrandFinalAmount" ReadOnly="true" runat="server" MaxLength="12"
                                                                                    TabIndex="1" CssClass="textboxgrey right" Text='<%# Eval("SEGMENT") %>' Width="96%"></asp:TextBox>
                                                                            </FooterTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" Width="100px" />
                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" VerticalAlign="top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Markets Remarks">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtRemByChangeInRate" Width="195px" Rows="3" Height="30px" TextMode="MultiLine"
                                                                                    TabIndex="1" runat="server" Text='<%# Eval("RATEREM") %>' CssClass="textbox" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="200px" VerticalAlign="top" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                                    <RowStyle CssClass="textbold" VerticalAlign="Top" />
                                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                    <FooterStyle CssClass="Gridheading" />
                                                                </asp:GridView>
                                                            </td>
                                                            <td colspan="1" style="width: 5px;">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6" align="right" style="height: 5px;">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6">
                                                                <table width="990px" cellpadding="0" cellspacing="0" border="0">
                                                                    <tr id="trUpFrontAmount" runat="server">
                                                                        <td width="120">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="90px">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="90px">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="90px">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="300px" colspan="3" class="Gridheading">
                                                                            <asp:Label ID="lblUpFrontAmount" Text="Upfront Amount" runat="server" Visible="False"></asp:Label>
                                                                        </td>
                                                                        <td width="100px">
                                                                            <asp:TextBox ID="txtUpFrontAmount" ReadOnly="true" runat="server" MaxLength="12"
                                                                                CssClass="textboxgrey right" Width="95%" TabIndex="1" Visible="False"></asp:TextBox>
                                                                        </td>
                                                                        <td width="200px">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="120" style="height: 5px;">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="300px" colspan="3">
                                                                        </td>
                                                                        <td width="100px">
                                                                        </td>
                                                                        <td width="200px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="trSignUpAmt" runat="server" visible="false">
                                                                        <td width="120">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="300px" colspan="3" class="Gridheading">
                                                                            <asp:Label ID="LblSignUpAmt" Text="SignUp Amount" runat="server" Visible="False"></asp:Label>
                                                                        </td>
                                                                        <td width="100px">
                                                                            <asp:TextBox ID="txtSignUpAmt" ReadOnly="true" runat="server" MaxLength="12" CssClass="textboxgrey right"
                                                                                Width="95%" TabIndex="1" Visible="False"></asp:TextBox>
                                                                        </td>
                                                                        <td width="200px">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="120" style="height: 5px;">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="300px" colspan="3">
                                                                        </td>
                                                                        <td width="100px">
                                                                        </td>
                                                                        <td width="200px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="trPrevUpfrontAmount" runat="server">
                                                                        <td width="120">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="300px" colspan="3" class="Gridheading">
                                                                            <asp:Label ID="lblPrevUpfrontAmount" Text="Previous Upfront Amount" runat="server"
                                                                                Visible="False"></asp:Label>
                                                                        </td>
                                                                        <td width="100px">
                                                                            <asp:TextBox ID="txtPrevUpfrontAmount" ReadOnly="true" runat="server" MaxLength="12"
                                                                                CssClass="textboxgrey right" Width="95%" TabIndex="1" Visible="False"></asp:TextBox>
                                                                        </td>
                                                                        <td width="130px">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="120" style="height: 5px;">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="300px" colspan="3">
                                                                        </td>
                                                                        <td width="100px">
                                                                        </td>
                                                                        <td width="130px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="trCBFAmount">
                                                                        <td width="120">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td colspan="3" width="300px" class="Gridheading">
                                                                            <asp:Label ID="LblCBFAmount" runat="server" Text="Carries Balance Upfront Amount"
                                                                                Visible="False"></asp:Label></td>
                                                                        <td width="100px">
                                                                            <asp:TextBox ID="txtCBForwardAmount" runat="server" CssClass="textboxgrey right"
                                                                                MaxLength="12" ReadOnly="true" TabIndex="1" Visible="False" Width="95%"></asp:TextBox></td>
                                                                        <td width="130px">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="120" style="height: 5px;">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="300px" colspan="3">
                                                                        </td>
                                                                        <td width="100px">
                                                                        </td>
                                                                        <td width="130px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="trLPlb" runat="server" visible="false">
                                                                        <td width="120">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="300px" colspan="3" class="Gridheading">
                                                                            <asp:Label ID="LblPlb" Text="PLB Amount" runat="server" Visible="False"></asp:Label>
                                                                        </td>
                                                                        <td width="100px">
                                                                            <asp:TextBox ID="txtPLBAmt" ReadOnly="false" runat="server" MaxLength="12" CssClass="textboxbold right"
                                                                                AutoPostBack="false" Width="95%" TabIndex="1" Visible="False" OnTextChanged="txtPLBAmt_TextChanged"></asp:TextBox>
                                                                        </td>
                                                                        <td width="130px">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="120" style="height: 5px;">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="300px" colspan="3">
                                                                        </td>
                                                                        <td width="100px">
                                                                        </td>
                                                                        <td width="130px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="trBalanceUpfrontAmount" runat="server">
                                                                        <td width="120">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="300px" colspan="3" class="Gridheading">
                                                                            <asp:Label ID="lblBalanceUpfrontAmount" Text="Balance Upfront Amount" runat="server"
                                                                                Visible="False"></asp:Label>
                                                                        </td>
                                                                        <td width="100px">
                                                                            <asp:TextBox ID="txtBalanceUpfrontAmount" ReadOnly="true" runat="server" MaxLength="12"
                                                                                CssClass="textboxgrey right" Width="95%" TabIndex="1" Visible="False"></asp:TextBox>
                                                                        </td>
                                                                        <td width="130px">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="120" style="height: 5px;">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="300px" colspan="3">
                                                                        </td>
                                                                        <td width="100px">
                                                                        </td>
                                                                        <td width="130px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="trLatestUpfontAmount" runat="server">
                                                                        <td width="120">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="300px" colspan="3" class="Gridheading">
                                                                            <asp:Label ID="lblLatestUpfontAmount" Text="Payment Upfront Now" runat="server" Visible="False"></asp:Label>
                                                                        </td>
                                                                        <td width="100px" class="textbold">
                                                                            <asp:TextBox ID="txtLatestUpfontAmount" ReadOnly="true" runat="server" MaxLength="12"
                                                                                CssClass="textboxgrey right" Width="95%" TabIndex="1" Visible="False" AutoPostBack="True"
                                                                                OnTextChanged="txtLatestUpfontAmount_TextChanged"></asp:TextBox>
                                                                        </td>
                                                                        <td width="130px">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="120" style="height: 5px;">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="90px">
                                                                        </td>
                                                                        <td width="300px" colspan="3">
                                                                        </td>
                                                                        <td width="100px">
                                                                        </td>
                                                                        <td width="130px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="9" style="padding-top: 10px;">
                                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td>
                                                                                        <div class="subheading" style="width: 100%; height: 18px;">
                                                                                            &nbsp;&nbsp;Business Development Remarks</div>
                                                                                    </td>
                                                                                    <td>
                                                                                        &nbsp;&nbsp;</td>
                                                                                    <td>
                                                                                        <div class="subheading" style="width: 100%; height: 18px;">
                                                                                            &nbsp;&nbsp;Payment Approval Remarks</div>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="padding-top: 10px; padding-left: 10px;">
                                                                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="textbox" TextMode="MultiLine"
                                                                                            Height="100px" Width="440px" TabIndex="1"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td style="padding-top: 10px; padding-left: 10px;">
                                                                                        <asp:TextBox ID="txtPayAppRemarks" runat="server" CssClass="textbox" TextMode="MultiLine"
                                                                                            Height="100px" Width="440px" TabIndex="1"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="9" valign="top" style="padding-top: 15px;">
                                                                            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                                                <tr>
                                                                                    <td id="tdMIDtInverstment" runat="server" valign="top" colspan="2">
                                                                                        <asp:UpdatePanel ID="UpdpnlMidtDetails" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                                                                    <tr>
                                                                                                        <td colspan="2" class="subheading" valign="middle" style="height: 18px;">
                                                                                                            MIDT / Slab / Qualification Average Details</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="2" style="cursor: pointer; padding-top: 5px;">
                                                                                                            <asp:Image ImageUrl="~/Images/MidTInverstMentHide.jpg" ID="imgMidt_Panel" runat="server"
                                                                                                                Width="100%" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="2" valign="top">
                                                                                                            <asp:Panel ID="pnlMIDT" runat="server" Visible="true">
                                                                                                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                                                                                    <tr valign="top">
                                                                                                                        <td class="subheading" align="center" width="50%" valign="middle">
                                                                                                                            MIDT FOR LAST 6 MONTHS</td>
                                                                                                                        <td>
                                                                                                                            &nbsp;</td>
                                                                                                                        <td class="subheading" align="center" width="50%" valign="middle">
                                                                                                                            <asp:Label ID="LblQS" runat="server" Text="QUALIFICATION SLABS"></asp:Label></td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td valign="top" width="50%">
                                                                                                                            <asp:GridView ID="grdvMIDT" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                                Width="100%" EnableViewState="true" AllowSorting="True" ShowFooter="false">
                                                                                                                                <Columns>
                                                                                                                                    <asp:BoundField DataField="MIDT" HeaderText="MIDT" ItemStyle-Wrap="false" />
                                                                                                                                    <asp:BoundField DataField="A1" HeaderText="1A" ItemStyle-CssClass="right" />
                                                                                                                                    <asp:BoundField DataField="B1" HeaderText="1B" ItemStyle-CssClass="right" />
                                                                                                                                    <asp:BoundField DataField="G1" HeaderText="1G" ItemStyle-CssClass="right" />
                                                                                                                                    <asp:BoundField DataField="P1" HeaderText="1P" ItemStyle-CssClass="right" />
                                                                                                                                    <asp:BoundField DataField="W1" HeaderText="1W" ItemStyle-CssClass="right" />
                                                                                                                                    <asp:BoundField DataField="TOTAL" HeaderText="TOTAL" ItemStyle-CssClass="right" />
                                                                                                                                    <asp:BoundField DataField="A1PER" HeaderText="1A%" ItemStyle-CssClass="right" />
                                                                                                                                </Columns>
                                                                                                                                <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                                <RowStyle CssClass="textbold" />
                                                                                                                                <HeaderStyle CssClass="Gridheading" ForeColor="White" HorizontalAlign="Left" />
                                                                                                                                <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                            </asp:GridView>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            &nbsp;</td>
                                                                                                                        <td valign="top" width="50%">
                                                                                                                            <asp:GridView ID="GvSlabQualification" runat="server" AutoGenerateColumns="False"
                                                                                                                                TabIndex="6" Width="100%" EnableViewState="true" AllowSorting="True" ShowFooter="false">
                                                                                                                                <Columns>
                                                                                                                                    <asp:BoundField DataField="SLAB" HeaderText="SLABS" ItemStyle-Width="150px" />
                                                                                                                                    <asp:BoundField DataField="RATES" HeaderText="RATES" />
                                                                                                                                </Columns>
                                                                                                                                <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                                <RowStyle CssClass="textbold" />
                                                                                                                                <HeaderStyle CssClass="Gridheading" ForeColor="White" HorizontalAlign="Left" />
                                                                                                                                <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                            </asp:GridView>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </asp:Panel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                                <cc1:CollapsiblePanelExtender ID="collapsePnlMIDT" CollapsedImage="../Images/MidTInverstMentShow.JPG"
                                                                                                    ExpandedImage="../Images/MidTInverstMentHide.JPG" runat="Server" TargetControlID="pnlMIDT"
                                                                                                    ImageControlID="imgMidt_Panel" ExpandControlID="imgMidt_Panel" SuppressPostBack="true"
                                                                                                    CollapseControlID="imgMidt_Panel" Collapsed="true" />
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="9">
                                                                               <asp:Panel ID="pnlPreviousDataSave" Width="640px" Height="450px" runat="server" Style="display: none"  ScrollBars ="Auto" 
                                                                                CssClass="modalPopup">
                                                                                <table style="width: 640px;" border="0" cellspacing="5" cellpadding="0">                                                                                  
                                                                                    <tr>
                                                                                        <td colspan="6" align="center" style="height: 20px;">
                                                                                              <asp:label ID="lblPaspaymentError" runat ="server"  CssClass ="ErrorMsg" ></asp:label>
                                                                                        </td>
                                                                                    </tr>
                                                                                      <tr>
                                                                                        <td colspan="6" align="center" style="height: 20px;">
                                                                                         <asp:Label ID="lblMsgDetails" CssClass ="msgcolor" runat="server" Text="To Continue enter past payment for the follwing periods:"></asp:Label>                                                                                           
                                                                                        </td>
                                                                                       
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="6" align="center" class="msgcolor" valign ="top" >                                                                                           
                                                                                            <asp:GridView ID="GvPasPaymentData" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                ShowFooter="false" ShowHeader="true" Width="600px" EnableViewState="true" AllowSorting="true">
                                                                                                <Columns>
                                                                                                     <asp:TemplateField HeaderText="Payment Period">
                                                                                                        <ItemTemplate>
                                                                                                         <asp:HiddenField ID="hdMisMonth" runat="server" Value='<%# Eval("Month") %>' />
                                                                                                          <asp:HiddenField ID="HdMisYear" runat="server" Value='<%# Eval("Year") %>' />
                                                                                                           <asp:HiddenField ID="HdNoOfPay" runat="server" Value='<%# Eval("NofPay") %>' />
                                                                                                            <asp:Label ID="LblPaymentPeriod" Text ='<%# Eval("PaymentPeriod")  %>' runat="server" CssClass="textbox right"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="100px" CssClass="ItemColor" HorizontalAlign="center" />
                                                                                                        <HeaderStyle Wrap="False" CssClass="Gridheading" Width="100px" />
                                                                                                    </asp:TemplateField>                                                                                                   
                                                                                                    <asp:TemplateField HeaderText="Payment Amount" >
                                                                                                        <ItemTemplate>
                                                                                                            <asp:TextBox ID="TxtPaymentAmt" runat="server" CssClass="textbox right" MaxLength ="9" Width ="100px"></asp:TextBox>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="100px" CssClass="ItemColor" HorizontalAlign="center" />
                                                                                                        <HeaderStyle Wrap="False" CssClass="Gridheading" Width="100px" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Productivity Avg">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:TextBox ID="TxtProdAvg" runat="server" CssClass="textbox right" MaxLength ="9" Width ="100px"></asp:TextBox>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="100px" CssClass="ItemColor" HorizontalAlign="center" />
                                                                                                        <HeaderStyle Wrap="False" CssClass="Gridheading" Width="100px" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Remark">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:TextBox ID="TxtRem" runat="server" CssClass="textbox" TextMode ="MultiLine" Wrap ="true" Height ="30px"  Width ="198px" ></asp:TextBox>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="200px" CssClass="ItemColor" HorizontalAlign="center" />
                                                                                                        <HeaderStyle Wrap="False" CssClass="Gridheading" Width="200px" />
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                                <AlternatingRowStyle CssClass="lightblue" />
                                                                                                <RowStyle CssClass="textbold" />
                                                                                                <HeaderStyle CssClass="Gridheading" ForeColor="White" HorizontalAlign="Left" />
                                                                                                <FooterStyle CssClass="Gridheading" />
                                                                                            </asp:GridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                     <tr>
                                                                                        <td colspan="6" align="center">
                                                                                            <asp:Button ID="btnColose" CssClass="modalCloseButton" runat="server" Text="Close"
                                                                                                Width="80px" Height="20px" />&nbsp;&nbsp; <asp:Button ID="BtnPastpaySave" CssClass="modalCloseButton" runat="server" Text="save"
                                                                                                Width="80px" Height="20px" /></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="6">
                                                                                            <asp:Button ID="ButtonInvisible" runat="server" Text="Button" Style="display: none;" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                            <!--End Panel For Route Map  -->
                                                                            <cc1:ModalPopupExtender BackgroundCssClass="modalBackground" DropShadow="false" PopupControlID="pnlPreviousDataSave"
                                                                                TargetControlID="ButtonInvisible" RepositionMode="RepositionOnWindowResizeAndScroll"
                                                                                ID="ModalPopupExtenderPreviousDataSave" runat="server" CancelControlID="btnColose">
                                                                            </cc1:ModalPopupExtender>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="5" style="height: 20px;">
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="5">
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6">
                                                                <input id="hdEnChainCode" runat="server" style="width: 21px" type="hidden" /><input
                                                                    id="hdChainCode" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="HdPaymentId" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="HdPACreated" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="hdFixedOrRate" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="hdIsPLB" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="hdFirstTime" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="hdPaymentType" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="hdUpFronttType" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="HdpREVPaymentId" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="HdYearAndSettleMent" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="hdFinallyApproved" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="hdEndSettlement" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="HdUpNoPay" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="HdUpNoPayDone" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="HdOneTimeUpNoOfPay" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="HdOneTimeUpNoPayDone" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="HdCurPayNo" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="HdAdjustable" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="hdShowPopup" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="HdPaidUpFrontAtthisLevel" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="HdPlbTypeId" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="HdQualAgv" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="HdPLBCycle" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="hdPLBFROM" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="hdPLBTO" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="HdPmtFormat" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="HdSkipPayment" runat="server" style="width: 21px" type="hidden" />
                                                                 <input id="HdMisNoOfPay" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="HdMisMonth" runat="server" style="width: 21px" type="hidden" />
                                                                <input id="HdMisYear" runat="server" style="width: 21px" type="hidden" />
                                                            </td>
                                                            <td colspan="1">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="7">
                                                                <asp:Panel ID="PnlBrakupDetails" Width="780px" Height="500px" runat="server" Style="display: none"
                                                                    CssClass="modalPopup">
                                                                    <table style="width: 775px;" border="0" cellspacing="5" cellpadding="0" id="popupdraghandlecontrolid">
                                                                        <tr>
                                                                            <td align="left" class="DragableHeader" style="width: 200px;">
                                                                                Breakup Data
                                                                            </td>
                                                                            <td align="right">
                                                                                <asp:Button ID="btnColoseMe" CssClass="modalCloseButton" runat="server" Text="Close Me"
                                                                                    Width="80px" Height="20px" />&nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td class="textbold" style="width: 110px;">
                                                                                            Airline Qualification
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtQualAvgSelected" runat="server" CssClass="textboxgrey" TextMode="MultiLine"
                                                                                                ReadOnly="True" TabIndex="1" Width="616px"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="textbold" style="width: 110px;" valign="middle">
                                                                                            Total Qualification
                                                                                        </td>
                                                                                        <td valign="middle">
                                                                                            <table width="620px" border="0" cellpadding="0" cellspacing="0">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txtTotalQualification" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                                            TabIndex="1" Width="80px"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td class="textbold" style="width: 300px;" align="center">
                                                                                                        Total Qualification with HL/ROI benefits</td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txtTotalQualificationAfterExem" runat="server" CssClass="textboxgrey"
                                                                                                            ReadOnly="True" TabIndex="1" Width="80px"></asp:TextBox></td>
                                                                                                    <td class="textbold" style="width: 110px;" align="center">
                                                                                                        Qualification Avg.
                                                                                                    </td>
                                                                                                    <td align="right" class="textbold">
                                                                                                        <asp:TextBox ID="txtQualAvgData" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                                            TabIndex="1" Width="80px"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <div id="DivHeader" runat="server">
                                                                        <table border='0' cellpadding='0' cellspacing='0'>
                                                                            <tr class="Gridheading">
                                                                                <td style="width: 200px;">
                                                                                    Productivity Type</td>
                                                                                <td style="width: 120px; text-align: center;">
                                                                                    Standard Segment</td>
                                                                                <td style="width: 120px; text-align: center;">
                                                                                    Actual Segment
                                                                                </td>
                                                                                <td style="width: 120px; text-align: center;">
                                                                                    <asp:Label ID="LblStdRateorFixed" runat="server" Text="Standard Rate"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 120px; text-align: center;">
                                                                                    <asp:Label ID="LblRateorFixed" runat="server" Text="Revised Rate"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 100px; text-align: center;">
                                                                                    Selected</td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                    <div id="divBreakupDetails" runat="server" style="height: 410px; overflow: auto;
                                                                        overflow-x: hidden;">
                                                                        <asp:GridView ID="GvBreakup" runat="server" AutoGenerateColumns="False" TabIndex="1"
                                                                            Width="100%" EnableViewState="true" AllowSorting="false" HeaderStyle-ForeColor="white"
                                                                            ShowHeader="false" ShowFooter="false">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Productivity Type">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblProductivity" runat="server" Text='<%# Eval("PRODUCTIVITYTYPE") %>'></asp:Label>
                                                                                        <asp:HiddenField ID="hdProductivityId" runat="server" Value='<%# Eval("PRODUCTIVITYTYPEID") %>' />
                                                                                        <asp:HiddenField ID="hdPayMentId" runat="server" Value='<%# Eval("PAYMENT_ID") %>' />
                                                                                        <asp:HiddenField ID="hdNotInBCase" runat="server" Value='<%# Eval("ISCHECKED") %>' />
                                                                                        <asp:HiddenField ID="hdRowID" runat="server" Value='<%# Eval("ROWID") %>' />
                                                                                        <asp:HiddenField ID="hdHL" runat="server" Value='<%# Eval("HL") %>' />
                                                                                        <asp:HiddenField ID="hdROI" runat="server" Value='<%# Eval("ROI") %>' />
                                                                                        <asp:HiddenField ID="hdNidtFields" runat="server" Value='<%# Eval("NIDTFIELDS") %>' />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Standard Segment">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="LblStdSeg" runat="server" Text='<%# Eval("STANDARSEGMENT") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="center" Width="120px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Revised Segment">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="LblSeg" runat="server" Text='<%# Eval("SEGMENT") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="center" Width="120px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Standard Rate">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="LblStdRate" runat="server" Text='<%# Eval("STANDARDRATE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="center" Width="120px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Revised Rate">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="LblRate" runat="server" Text='<%# Eval("RATE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="center" Width="120px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Selected">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="LblChecked" runat="server" Text='<%# Eval("ISCHECKED") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="center" Width="100px" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                            <RowStyle CssClass="textbold" VerticalAlign="Top" />
                                                                            <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                            <FooterStyle CssClass="Gridheading" />
                                                                        </asp:GridView>
                                                                    </div>
                                                                    <asp:Button ID="BtnFake" runat="server" Text="Button" Style="display: none;" />
                                                                </asp:Panel>
                                                                <!--End Panel For Route Map  -->
                                                                <cc1:ModalPopupExtender BackgroundCssClass="modalBackground" DropShadow="false" PopupControlID="PnlBrakupDetails"
                                                                    TargetControlID="BtnFake" RepositionMode="RepositionOnWindowResizeAndScroll"
                                                                    ID="ModalPopupBreakup" runat="server" CancelControlID="btnColoseMe" PopupDragHandleControlID="popupdraghandlecontrolid"
                                                                    Drag="true">
                                                                </cc1:ModalPopupExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="7">
                                                                <asp:Button ID="BtnOpenBreakup" runat="server" Text="" AccessKey="M" BorderWidth="0" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="7">
                                                                <cc1:ModalPopupExtender BackgroundCssClass="modalBackground" DropShadow="true" PopupControlID="PnlPrrogress"
                                                                    TargetControlID="PnlPrrogress" RepositionMode="RepositionOnWindowResizeAndScroll"
                                                                    ID="ModalPopupExtender2" runat="server">
                                                                </cc1:ModalPopupExtender>
                                                                <asp:Panel ID="PnlPrrogress" runat="server" CssClass="overPanel_Test" Height="0px"
                                                                    Width="150px" BackColor="white">
                                                                    <table style="width: 150px; height: 150px;">
                                                                        <tr>
                                                                            <td valign="middle" align="center">
                                                                                <img src="../Images/er.gif" id="img" runat="server" alt="" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="7">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Button ID="BtnConfirm" Text="" runat="server" CssClass="displayNone" />
                                                                        </td>
                                                                        <td>
                                                                            <cc1:ModalPopupExtender ID="mdlPopUpExt" runat="server" TargetControlID="BtnConfirm"
                                                                                BackgroundCssClass="confirmationBackground" PopupControlID="pnlPopup" CancelControlID="btnNo">
                                                                            </cc1:ModalPopupExtender>
                                                                            <asp:Panel ID="pnlPopup" runat="server" Width="600px" Height="250px" Style="display: none"
                                                                                HorizontalAlign="Center" CssClass="confirmationPopup">
                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                    <tr>
                                                                                        <td class="strip_bluelogin" align="left" colspan="2">
                                                                                            Amadeus agent management system
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td height="30" colspan="2">
                                                                                            <asp:Label ID="lblReasonError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="textbold" colspan="2">
                                                                                            <b>Data has been changed<br />
                                                                                                Do you want to generate the payment advice without saving ?</b>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td height="30" colspan="2">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="2" align="center">
                                                                                            <table border="0" width="40%" cellspacing="10" cellpadding="1">
                                                                                                <tr>
                                                                                                    <td align="center">
                                                                                                        <asp:Button ID="btnYes" runat="server" CssClass="button" Text="Yes" Width="60px"
                                                                                                            OnClick="btnYes_Click" /></td>
                                                                                                    <td align="center">
                                                                                                        <asp:Button ID="btnNo" CssClass="button" runat="server" Text="No" Width="60px" OnClientClick="$find('mdlPopUpExt').hide(); return false;" />
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
                </td>
            </tr>
        </table>
    </form>
</body>

<script type="text/javascript">
        // The following snippet works around a problem where FloatingBehavior
        // doesn't allow drops outside the "content area" of the page - where "content
        // area" is a little unusual for our sample web pages due to their use of CSS
        // for layout.
//        function setBodyHeightToContentHeight() {
//            document.body.style.height = Math.max(document.documentElement.scrollHeight, document.body.scrollHeight)+"px";
//            
//        }
//        setBodyHeightToContentHeight();
//        $addHandler(window, "resize", setBodyHeightToContentHeight);    
  function ShowPopupTabChange(id)
        {
            try
            {
            var modal = $find('ModalPopupExtender2'); 
            document.getElementById('PnlPrrogress').style.height='150px';
            if (document.getElementById ('imgLoad') !=null)
             {
                document.getElementById ('imgLoad').style.display ="none";
             }
            modal.show(); 
            }
             catch(err){}
         
        } 
        
          function ValidatePastPayment()
        {
             try
            {
              //  alert("abhi");
                 if(document.getElementById('GvPasPaymentData')!=null)
                       {
                           var dotcount=0;
                           
                              for(intcnt=1;intcnt<=document.getElementById('GvPasPaymentData').rows.length-1;intcnt++)
                            {   
                                    if (document.getElementById('GvPasPaymentData').rows[intcnt].cells[1].children[0] !=null )
                                    {
                                              if (document.getElementById('GvPasPaymentData').rows[intcnt].cells[1].children[0].type=="text")
                                          {                                           
                                                 var strValue = document.getElementById('GvPasPaymentData').rows[intcnt].cells[1].children[0].value.trim();
	                                            
	                                              if (strValue == '')
                                                  {                                                     
                                                           document.getElementById('<%=GvPasPaymentData.ClientID%>').rows[intcnt].cells[1].children[0].focus();
                                                           document.getElementById("lblPaspaymentError").innerHTML="Payment amount is mandatory.";
                                                           return false;                                                       
                                                  }  
	                                            
	                                            if (strValue != '')
                                                  {
                                                    reg = new RegExp("^[0-9.]+$"); 
                                                    if(reg.test(strValue) == false) 
                                                    {
                                                      document.getElementById('<%=GvPasPaymentData.ClientID%>').rows[intcnt].cells[1].children[0].focus();
                                                      document.getElementById('lblPaspaymentError').innerHTML ='Only numeric with decimal is allowed.'; 
                                                      return false;
                                                     }
                                                  }
                                              
                                                  for (var i=0; i < strValue.length; i++) 
	                                                {
		                                                if (strValue.charAt(i)=='.')
		                                                 { 
		                                                     dotcount=dotcount+1;
		                                                  }
		                                           }
                                                    if(dotcount>1)
                                                    {
                                                        document.getElementById('<%=GvPasPaymentData.ClientID%>').rows[intcnt].cells[1].children[0].focus();
                                                        document.getElementById("lblPaspaymentError").innerHTML='Only numeric with decimal is allowed.';
                                                        return false;
                                                    }
                                                
                                            dotcount=0;  
                                                                      
                                          }                                    
                                    }                           
                            }
                            
                            
                               for(intcnt=1;intcnt<=document.getElementById('GvPasPaymentData').rows.length-1;intcnt++)
                            {   
                                    if (document.getElementById('GvPasPaymentData').rows[intcnt].cells[2].children[0] !=null )
                                    {
                                              if (document.getElementById('GvPasPaymentData').rows[intcnt].cells[2].children[0].type=="text")
                                          {                                           
                                                 var strValue = document.getElementById('GvPasPaymentData').rows[intcnt].cells[2].children[0].value.trim();
	                                            
	                                              if (strValue == '')
                                                  {                                                     
                                                           document.getElementById('<%=GvPasPaymentData.ClientID%>').rows[intcnt].cells[2].children[0].focus();
                                                           document.getElementById("lblPaspaymentError").innerHTML="Productivity Avg is mandatory.";
                                                           return false;                                                       
                                                  }  
	                                            
	                                            if (strValue != '')
                                                  {
                                                    reg = new RegExp("^[0-9.]+$"); 
                                                    if(reg.test(strValue) == false) 
                                                    {
                                                      document.getElementById('<%=GvPasPaymentData.ClientID%>').rows[intcnt].cells[2].children[0].focus();
                                                      document.getElementById('lblPaspaymentError').innerHTML ='Only numeric with decimal is allowed.'; 
                                                      return false;
                                                     }
                                                  }
                                              
                                                  for (var i=0; i < strValue.length; i++) 
	                                                {
		                                                if (strValue.charAt(i)=='.')
		                                                 { 
		                                                     dotcount=dotcount+1;
		                                                  }
		                                           }
                                                    if(dotcount>1)
                                                    {
                                                        document.getElementById('<%=GvPasPaymentData.ClientID%>').rows[intcnt].cells[2].children[0].focus();
                                                        document.getElementById("lblPaspaymentError").innerHTML='Only numeric with decimal is allowed.';
                                                        return false;
                                                    }
                                                
                                            dotcount=0;  
                                                                      
                                          }                                    
                                    }                           
                            }
                            
                               for(intcnt=1;intcnt<=document.getElementById('GvPasPaymentData').rows.length-1;intcnt++)
                            {   
                                    if (document.getElementById('GvPasPaymentData').rows[intcnt].cells[3].children[0] !=null )
                                    {
                                              if (document.getElementById('GvPasPaymentData').rows[intcnt].cells[3].children[0].type=="textarea")
                                          {                                           
                                                 var strValue = document.getElementById('GvPasPaymentData').rows[intcnt].cells[3].children[0].value.trim();
	                                            
	                                              if (strValue == '')
                                                  {                                                     
//                                                           document.getElementById('<%=GvPasPaymentData.ClientID%>').rows[intcnt].cells[3].children[0].focus();
//                                                           document.getElementById("lblPaspaymentError").innerHTML="Remark is mandatory.";
//                                                           return false;                                                       
                                                  }  
                                                  if (strValue != '')
                                                  {
                                                       if (strValue.length>8000)
                                                        {
                                                              document.getElementById('<%=GvPasPaymentData.ClientID%>').rows[intcnt].cells[3].children[0].focus();
                                                               document.getElementById("lblPaspaymentError").innerHTML="Remarks can't be greater than 8000 characters."
                                                               
                                                             return false;
                                                        }   
                                                  }
	                                             
                                                                      
                                          }                                    
                                    }                           
                            }
                           
                       }     
            }     
             catch(err){}
        }
        
</script>

</html>
