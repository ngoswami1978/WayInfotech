<%@ Page Language="vb" AutoEventWireup="false" CodeFile="INCUP_PaymentProcess2.aspx.vb"
    Inherits="Incentive_INCUP_PaymentProcess2" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Employee</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>

    <script type="text/javascript" language="javascript">
       




          function pp()
          {
                  IE4 = (document.all);
                NS4 = (document.layers);
                var whichASC;
                whichASC = (NS4) ? e.which : event.keyCode;
               // alert(whichASC);
                  if(whichASC==9 )
                {
               // alert(whichASC)
                 return false;
             
                }
    	
          }
          function  Payment(objBCID,objChainCode,objMonth,objYear)
          {
            var Param="BCId=" + objBCID + "&ChainCode=" + objChainCode  +"&Month=" + objMonth + "&Year=" + objYear ;
            var type;
            type= "INCUP_PaymentProcess.aspx?" + Param;
            window.location.href =type;
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
                 }
            
            }
              
     
    //}
  }
  }catch(err){}

}

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table width="845px" align="left" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Incentive-&gt;</span><span class="sub_menu">Process&nbsp; Payment</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage &nbsp;Process&nbsp; Payment</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
                                            <ContentTemplate >
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
                                                        <asp:Panel ID="pnlDetails" runat="server" CssClass="displayBlock">
                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Panel ID="PnlGroupDetails" runat="server" Width="840px" Visible="true">
                                                                            <table width="100%" border="0" cellspacing="2" cellpadding="2">
                                                                                <tr>
                                                                                    <td class="subheading" colspan="6" width="840">
                                                                                        Group Details</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="textbold" style="width: 42%">
                                                                                        Group Name</td>
                                                                                    <td colspan="4" style="width: 70%">
                                                                                        <asp:TextBox ID="txtGroupName" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                            TabIndex="1" Width="533px" ReadOnly="True"></asp:TextBox>
                                                                                    </td>
                                                                                    <td align="center" colspan="1" style="width: 70%">
                                                                                        <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="2"
                                                                                            AccessKey="R" Width="120px" /></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="textbold" style="width: 42%; height: 23px;">
                                                                                        Chain Code</td>
                                                                                    <td style="width: 23%; height: 23px;">
                                                                                        <asp:TextBox ID="txtChainCode" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                            ReadOnly="True" TabIndex="1" Width="181px"></asp:TextBox></td>
                                                                                    <td class="textbold" style="width: 6%; height: 23px;">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 15%; height: 23px;">
                                                                                        Account Manager</td>
                                                                                    <td class="textbold" style="width: 21%; height: 23px;">
                                                                                        <asp:TextBox ID="txtActManager" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                            ReadOnly="True" TabIndex="1" Width="181px"></asp:TextBox></td>
                                                                                    <td align="center" class="textbold" style="width: 21%; height: 23px">
                                                                                        <asp:Button ID="BtnReset" CssClass="button" runat="server" Text="Reset" TabIndex="2"
                                                                                            AccessKey="R" Width="120px" /></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="textbold" style="width: 42%;">
                                                                                        City</td>
                                                                                    <td style="width: 23%;">
                                                                                        <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                                                            TabIndex="1" Width="181px"></asp:TextBox></td>
                                                                                    <td class="textbold" style="width: 6%;">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 15%;">
                                                                                        Billing Cycle</td>
                                                                                    <td class="textbold" style="width: 21%;">
                                                                                        <asp:TextBox ID="txtBillCycle" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                            ReadOnly="True" TabIndex="1" Width="181px"></asp:TextBox></td>
                                                                                    <td class="textbold" style="width: 21%" align="center"><asp:Button ID="BtnBCase" CssClass="button" runat="server" Text="Business Case" TabIndex="2"
                                                                                            AccessKey="B" Width="120px" /></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="textbold" style="width: 42%;" valign="top">
                                                                                        Aoffice</td>
                                                                                    <td style="width: 23%;" valign="top">
                                                                                        <asp:TextBox ID="txtAoffice" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                            ReadOnly="True" TabIndex="1" Width="181px"></asp:TextBox></td>
                                                                                    <td class="textbold" style="width: 6%;">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 15%;" valign="top">
                                                                                        Payment Period</td>
                                                                                    <td class="textbold" style="width: 21%;" valign="top">
                                                                                        <asp:TextBox ID="TxtPayPeriod" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                                                            TabIndex="1" Width="181px"></asp:TextBox></td>
                                                                                    <td class="textbold" style="width: 21%" valign="top" align="center"><asp:Button ID="BtnSendForApproval" CssClass="button" runat="server" Text="Generate  Payment Advice" TabIndex="2"
                                                                                            AccessKey="B" Width="164px" /></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="textbold" style="width: 42%" valign="top">Incentive Type
                                                                                    </td>
                                                                                    <td colspan="4" style="width: 70%"><asp:TextBox ID="txtIncetiveType" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                                                            TabIndex="1" Width="533px"></asp:TextBox>
                                                                                    </td>
                                                                                   
                                                                                    <td class="textbold" style="width: 21%" valign="top">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="textbold" style="width: 42%; height: 23px" valign="top">
                                                                                    </td>
                                                                                    <td style="width: 23%; " valign="top">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 6%; height: 23px">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 15%; height: 23px" valign="top">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 21%; height: 23px" valign="top">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 21%; height: 23px" valign="top">
                                                                                    </td>
                                                                                </tr>                                                                             
                                                                                  <tr>
                                                                                <td colspan="6" align ="center" style="width:830px; height :30px"  >
                                                                                    <asp:UpdateProgress ID="UpDateProgress1" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="10"
                                                                                        runat="server">
                                                                                        <ProgressTemplate>
                                                                                            <img alt="Loading.." src="../Images/loading.gif" id="imgLoad" runat="server"  />
                                                                                        </ProgressTemplate>
                                                                                    </asp:UpdateProgress>
                                                                                </td>
                                                                            </tr>
                                                                               <tr>
                                                                                    <td class="textbold" style="width: 42%" valign="top">
                                                                                    </td>
                                                                                    <td style="width: 23%" valign="top">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 6%">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 15%" valign="top">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 21%" valign="top">
                                                                                    </td>
                                                                                    <td class="textbold" style="width: 21%" valign="top">
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
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" width="100%">
                                                        <asp:GridView ID="GvProcessPayment" runat="server" AutoGenerateColumns="False" TabIndex="1"
                                                            Width="100%" EnableViewState="true" AllowSorting="false" HeaderStyle-ForeColor="white"
                                                            ShowFooter="true" >
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Productivity Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductivity" runat="server" Text='<%# Eval("PRODUCTIVITYTYPE") %>'></asp:Label>
                                                                        <asp:HiddenField ID="hdProductivityId" runat="server" Value='<%# Eval("PRODUCTIVITYTYPEID") %>' />
                                                                        <asp:HiddenField ID="hdPayMentId" runat="server" Value='<%# Eval("PAYMENT_ID") %>' />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Actual Segment"  >
                                                                    <ItemTemplate >
                                                                        <asp:Label ID="txtActulaSegment" runat="server" MaxLength="5" CssClass=" right" TabIndex ="1"
                                                                             ReadOnly="true" Text='<%# Eval("SEGMENT") %>' Width="100%"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Exemption %">
                                                                    <ItemTemplate>
                                                                        <asp:Textbox ID="txtExumption" runat="server" MaxLength="5" CssClass="textboxbold right" TabIndex ="2" 
                                                                            OnTextChanged="txtExumption_TextChanged"    AutoPostBack ="true"      Text='<%# Eval("EXCEMPTION") %>' Width="100%"></asp:Textbox>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Calculated Segment">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtCalCulatedSegment" ReadOnly="true"  runat="server" MaxLength="12" TabIndex ="3"
                                                                             CssClass="textboxgrey right" Text='<%# Eval("SEGMENT") %>' Width="100%"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="txtTotalCalCulatedSegment" ReadOnly="true"  runat="server" MaxLength="12" TabIndex ="6"
                                                                            CssClass="textboxgrey right" Text='<%# Eval("SEGMENT") %>' Width="100%"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" Width="120px" />
                                                                    <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Rate">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtRate" runat="server" MaxLength="5" CssClass="textboxgrey right" TabIndex ="4"
                                                                            ReadOnly="true"  Text='<%# Eval("RATE") %>' Width="100%"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Amount After Exemption">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtFinalAmount" ReadOnly="true"  runat="server" MaxLength="12" CssClass="textboxgrey right" TabIndex ="5"
                                                                            Text='<%# Eval("SEGMENT") %>' Width="100%"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="txtGrandFinalAmount" ReadOnly="true" runat="server" MaxLength="12" TabIndex ="7"
                                                                            CssClass="textboxgrey right" Text='<%# Eval("SEGMENT") %>' Width="100%"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" Width="120px" />
                                                                    <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <RowStyle CssClass="textbold" VerticalAlign="Top" />
                                                            <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                            <FooterStyle CssClass="Gridheading" />
                                                        </asp:GridView>
                                                    </td>
                                                    <td colspan="1" width="90%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" align="right">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <table width="100%">
                                                            <tr>
                                                                <td width="150">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                    <asp:label ID="LblSoleAmount" Text="Sole Amount" runat="server" Visible ="False"  ></asp:label> </td>
                                                                <td width="120">
                                                                    <asp:Label ID="txtSoleAmt" ReadOnly="true"  runat="server" MaxLength="12" CssClass="textboxgrey right"
                                                                        Width="100%" TabIndex="1" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td width="150">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                    </td>
                                                                <td width="120">                                                                   
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td width="150">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                    <asp:label ID="LabelFixPayAmt" Text=" Fixed Amount" runat="server" Visible ="False"  ></asp:label> </td>
                                                                <td width="120">
                                                                    <asp:Label ID="txtFixedPayAmt" ReadOnly="true"  runat="server" MaxLength="12" CssClass="textboxgrey right"
                                                                        Width="100%" TabIndex="1" Visible="False" ></asp:Label>
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td width="150">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                    </td>
                                                                <td width="120">
                                                                    </td>
                                                            </tr>
                                                             <tr>
                                                                <td width="150">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                     <asp:label ID="LblUpFrontAmt" Text="Upfront Amount" runat="server" Visible ="False"  ></asp:label></td>
                                                                <td width="120">
                                                                    <asp:Label ID="txtUpfrontAmt" ReadOnly="true"  runat="server" MaxLength="12" CssClass="textboxgrey right"
                                                                        Width="100%" TabIndex="1" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td width="150">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                    </td>
                                                                <td width="120">
                                                                   </td>
                                                            </tr>
                                                             <tr>
                                                                <td width="150">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                    <asp:label ID="LblFixUpFrontAmt" Text="Fix Upfront" runat="server" Visible ="False"  ></asp:label></td> 
                                                                <td width="120">
                                                                    <asp:Label ID="txtFixUpFrontAmt" ReadOnly="true"  runat="server" MaxLength="12" CssClass="textboxgrey right"
                                                                        Width="100%" TabIndex="1" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td width="150">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                    </td>
                                                                <td width="120">
                                                                   </td>
                                                            </tr>
                                                             <tr>
                                                                <td width="150">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                     <asp:label ID="LblBonus" Text="Bonus" runat="server" Visible ="False"  ></asp:label></td>
                                                                <td width="120">
                                                                    <asp:Label ID="txtBonusAmt" ReadOnly="true"  runat="server" MaxLength="12" CssClass="textboxgrey right"
                                                                        Width="100%" TabIndex="1" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td width="150">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                    </td>
                                                                <td width="120">
                                                                    </td>
                                                            </tr>
                                                             <tr class ="Gridheading">
                                                                <td width="150">
                                                                </td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                    Grand Total</td>
                                                                <td width="120">
                                                                </td>
                                                                <td width="120">
                                                                   </td>
                                                                <td width="120">
                                                                    <asp:Label ID="txtGrandFinalAmt" ReadOnly="true"  runat="server" MaxLength="12" CssClass="textboxgrey right"
                                                                        Width="100%" TabIndex="1"></asp:Label>
                                                                </td>
                                                            </tr>                                                            
                                                        </table>
                                                    </td>
                                                    <td>
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
                                                            </td>
                                                    <td colspan="1">
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
</html>
