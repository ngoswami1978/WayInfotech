<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ISPUP_Order.aspx.vb" Inherits="ISP_ISPUP_Order" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Agency</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />       
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
   
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>

<script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
<script language="javascript" type="text/javascript">

  function ValidationISPOrder()
    {
    
                  if(document.getElementById('txtAgencyName').value == '')
                {            
                    document.getElementById('lblError').innerHTML='Agency name is mandatory.';
                    document.getElementById('txtAgencyName').focus();
                    return false;
                }
                    if(document.getElementById('drpIspName').value == '')
                {            
                    document.getElementById('lblError').innerHTML='ISP name is mandatory.';
                    document.getElementById('drpIspName').focus();
                    return false;
                }
                  if(document.getElementById('drpPlainId').value == '')
                {            
                    document.getElementById('lblError').innerHTML='NPID is mandatory.';
                    document.getElementById('drpPlainId').focus();
                    return false;
                }
              if(document.getElementById('drpIspOrderStatus').value=='')
                {            
                    document.getElementById('lblError').innerHTML='ISP Order Status is mandatory.';
                    document.getElementById('drpIspOrderStatus').focus();
                    return false;
                }
//                   if(document.getElementById('<%=txtOrderNumber.ClientId%>').value == '')
//                    {               
//                       document.getElementById('<%=lblError.ClientId%>').innerText = "Order No. is Mandatory.";			
//	                   document.getElementById('<%=txtOrderNumber.ClientId%>').focus();
//	                   return(false);  
//                   
//                     } 
//                   if(document.getElementById('<%=txtOrderDate.ClientId%>').value == '')
//                    {               
//                       document.getElementById('<%=lblError.ClientId%>').innerText = "Order Date is Mandatory.";			
//	                   document.getElementById('<%=txtOrderDate.ClientId%>').focus();
//	                   return(false);  
//                   
//                     }   
//                
                    
                       if(document.getElementById('<%=txtOnlineDate.ClientId%>').value != '')
                {
                if (isDate(document.getElementById('<%=txtOnlineDate.ClientId%>').value,"d/M/yyyy") == false)	
                {
                   document.getElementById('<%=lblError.ClientId%>').innerText = "Online Date is not valid.";			
	               document.getElementById('<%=txtOnlineDate.ClientId%>').focus();
	               return(false);  
                }
                 }
    
                  if(document.getElementById('<%=txtOrderDate.ClientId%>').value != '')
                    {
                    if (isDate(document.getElementById('<%=txtOrderDate.ClientId%>').value,"d/M/yyyy") == false)	
                    {
                       document.getElementById('<%=lblError.ClientId%>').innerText = "Order Date is not valid.";			
	                   document.getElementById('<%=txtOrderDate.ClientId%>').focus();
	                   return(false);  
                    }
                     }    
           
                     if(document.getElementById('<%=txtApprovalDate.ClientId%>').value != '')
                {
                if (isDate(document.getElementById('<%=txtApprovalDate.ClientId%>').value,"d/M/yyyy") == false)	
                {
                   document.getElementById('<%=lblError.ClientId%>').innerText = "Approval Date is not valid.";			
	               document.getElementById('<%=txtApprovalDate.ClientId%>').focus();
	               return(false);  
                }
                 }  
                   if(document.getElementById('<%=txtExpOnlineDate.ClientId%>').value != '')
                {
                if (isDate(document.getElementById('<%=txtExpOnlineDate.ClientId%>').value,"d/M/yyyy") == false)	
                {
                   document.getElementById('<%=lblError.ClientId%>').innerText = "Expected Online Date is not valid.";			
	               document.getElementById('<%=txtExpOnlineDate.ClientId%>').focus();
	               return(false);  
                }
                 }   
                 
                  if(document.getElementById('<%=txtCanDate.ClientId%>').value != '')
                {
                if (isDate(document.getElementById('<%=txtCanDate.ClientId%>').value,"d/M/yyyy") == false)	
                {
                   document.getElementById('<%=lblError.ClientId%>').innerText = "Cancellation Date is not valid.";			
	               document.getElementById('<%=txtCanDate.ClientId%>').focus();
	               return(false);  
                }
                 }  
                 
               // alert(document.getElementById('drpIspOrderStatus').options[document.getElementById('drpIspOrderStatus').selectedIndex].text);
              if(document.getElementById('drpIspOrderStatus').value=='6')
                {     
                if(document.getElementById('<%=txtOnlineDate.ClientId%>').value =='')
                   {
                   document.getElementById('lblError').innerHTML='Online Date is mandatory.';
                    document.getElementById('txtOnlineDate').focus();
                    return false;
                   }
                 }  
                     
                if(document.getElementById('drpIspOrderStatus').value=='2')
                 {    
                      if(document.getElementById('<%=txtCanDate.ClientId%>').value =='')
                        {
                        document.getElementById('lblError').innerHTML='Cancellation date is mandatory.';
                         document.getElementById('txtCanDate').focus();
                        return false;
                         }    
                     if(document.getElementById('<%=txtCanReason.ClientId%>').value =='')
                    {
                    document.getElementById('lblError').innerHTML='Cancellation Reason is mandatory.';
                     document.getElementById('txtCanReason').focus();
                    return false;
                     }  
                 }  
                 
                     if(document.getElementById('txtStaticIP').value != '')
                {
                if (isValidIPAddress(document.getElementById('txtStaticIP').value)==false)	
                {
                   document.getElementById('lblError').innerText = "Static IP is not valid.";			
                   document.getElementById('txtStaticIP').focus();
                   return(false);  
                }
               } 
               
               
                 
                        if (document.getElementById("txtCanReason").value.trim().length>300)
                    {
                         document.getElementById("lblError").innerHTML="Cancellation Reason  cann't be more than 300 characters."
                         document.getElementById("txtCanReason").focus();
                         return false;
                    } 
                    
                          if (document.getElementById("txtRem").value.trim().length>300)
                    {
                         document.getElementById("lblError").innerHTML="Remarks  cann't be more than 300 characters."
                         document.getElementById("txtRem").focus();
                         return false;
                    }       
                         
               
               
}
 
     //Added by Mukund on 12-Feb 2008
       
  function PopupAgencyPage()
{


          var type;
          // type = "../Popup/PUSR_Agency.aspx" ;
            type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;
                    
                             
}


        function PopupISPDetails()
        {

     var type;
    var CityVal=document.getElementById('<%=txtCity.ClientID%>').value

    if(CityVal=='' || CityVal==null) 
    {
    document.getElementById('<%=lblError.ClientID%>').innerHTML ='First Select Agency'
        return false;
    }


     
    type = "../ISP/MSSR_ISP.aspx?Popup=T&CityNmae="+ CityVal ;
   	window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
    return false;

 }
 
 
 
 
  function  PopupISPPlanId()
        {
          var Lcode= document.getElementById('<%= hdLcode.ClientID%>').value;
           document.getElementById('lblError').innerHTML=''
           
         if(document.getElementById('<%=drpIspName.ClientID%>').value=="")
              {
                  document.getElementById('lblError').innerHTML='First Select ISP.';             
                  document.getElementById("drpIspName").focus();
                  return false;
              }
         var IspName = document.getElementById('<%=drpIspName.ClientID%>').value;

          var IspId=   document.getElementById('<%=hdISPId.ClientID%>').value;

           var varProviderID=  document.getElementById('<%=hdIspProviderID.ClientID%>').value;

   	      
            var type;      
            type = "../ISP/MSSR_ISPPlan.aspx?Popup=T&IspName=" + IspName + "&IspId=" + IspId + "&Lcode=" + Lcode + "&IspProviderID=" + varProviderID ;
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;
        }
        


    function NewFunction()
    {       
         window.location.href="ISPUP_Order.aspx?Action=New";
            return false;    
     
    }
    function PopupEmployee()
{


           var type;     
            var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
            if (strEmployeePageName!="")
            {
                type = "../Setup/" + strEmployeePageName+ "?Popup=T" ; 
                //type = "../Setup/MSSR_Employee.aspx?Popup=T" ;
   	            window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
                return false;
            }
}

</script>
<body >
    <form id="form1" runat="server" defaultfocus="txtOnlineDate">
        <table width="860px" align="left" style="height: 486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="860px">
                        <tr>
                            <td valign="top">
                                <table width="860px" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left" style="width:860px;">
                                            <span class="menu">ISP-&gt;</span><span class="sub_menu">ISP Order</span>
                                        </td>
                                      
                                    </tr>
                                    <tr>
                                        <td bgcolor="#1A61A9" class="heading" align="center" style="width:860px;">
                                            Manage ISP Order&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="width:860px;">
                                <table width="860px" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td valign="top">
                                        </td>
                                    </tr>
                                      <tr>
                                        <td valign="top"></td>
                                    </tr>
                                   <tr>
                                        <td valign="top">
                                            <asp:Panel ID="Panel1" runat="server"  style="width:600px;"  >
                                              <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                  <tr>
                                                     <td valign="top"><asp:Button ID="btnIspOrder" runat="server" Text="ISP Order Details" CssClass="headingtab" Width="120px" />&nbsp;<asp:Button ID="btnPrevIspDetails" runat="server" Text="Previous ISP Order  Details" CssClass="headingtab" Enabled="true" Width="160px" />&nbsp;<asp:Button ID="BtnSendingMail" runat="server" Text="Mailing List" CssClass="headingtab" Enabled="True" Width="160px" /></td>
                                                  </tr>
                                              </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" class="redborder" >
                                           <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width:100%" valign="top">
                                                        <asp:Panel ID="pnlIspOrder" runat="server" Width="100%">
                                                           <table width="800" border="0" align="left" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                <td  colspan ="6" class="textbold" align="center" valign="TOP" rowspan="0">
                                                                    <asp:Label ID="lblError"  CssClass="ErrorMsg" runat="server"></asp:Label>
                                                                </td>
                                                                </tr>
                                                                 <tr>
                                                                     <td  class="textbold" colspan="6" align="center" valign="TOP" style ="height :5px"></td>
                                                                </tr>
                                                                   <tr>
                                                                    <td class="textbold" style="width: 4px;" >
                                                                    </td>
                                                                    <td class="subheading" colspan="4" style="height: 19px" >
                                                                        Agency Detail</td>
                                                                    <td >
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                     <td  style ="height :5px"  class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width:2%">
                                                                    </td>
                                                                    <td class="textbold" style ="width:23%" >Name&nbsp;<span class="Mandatory">*</span></td>
                                                                    <td colspan="3" >
                                                                        <asp:TextBox ID="txtAgencyName" TabIndex="1"
                                                                            CssClass="textboxgrey" runat="server" Width="514px" ReadOnly="True"></asp:TextBox>
                                                                             <img alt="" src="../Images/lookup.gif"  onclick="javascript:return PopupAgencyPage();" visible="false"  id="imgAgency" runat="server" style="cursor:pointer;" />
                                                                        <asp:HiddenField ID="hdAgencyNameId" runat="server"  />
                                                                            </td>
                                                                           
                                                                    <td style ="width:10%">
                                                                        <asp:Button ID="btnNew" runat="server" CssClass="button" TabIndex="37" Text="New" AccessKey="N" /></td>
                                                                </tr>
                                                                <tr>
                                                                     <td style ="height :5px" class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 4px;">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Address</td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtAgencyAddress" runat="server" CssClass="textboxgrey" TabIndex="2"
                                                                            TextMode="MultiLine" Width="514px" Height="50px" ReadOnly="True" Rows="5"></asp:TextBox></td>
                                                                   
                                                                    <td valign ="top" >
                                                                        <asp:Button ID="btnSave" runat="server" TabIndex="38" CssClass="button" Text="Save" AccessKey="S" /><br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                     <td style ="height :5px" class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="textbold" style="width: 4px; height: 22px;">
                                                                    </td>
                                                                    <td class="textbold" style="height: 22px">City</td>
                                                                    <td style="width:30%; height: 22px;" >
                                                                        <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey"
                                                                            TabIndex="3" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    <td class="textbold" style="width:25%; height: 22px;">Country</td>
                                                                    <td style="width: 185px; height: 22px;">
                                                                        <asp:TextBox ID="txtCountry" CssClass="textboxgrey" TabIndex="4" runat="server" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    <td style="height: 22px"><asp:Button ID="btnExport" runat="server" TabIndex="39" CssClass="button" Text="Export" AccessKey="E" /></td>
                                                                </tr>
                                                                  <tr>
                                                                     <td style ="height :5px" class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="textbold" style="width: 4px; height: 21px;">
                                                                    </td>
                                                                    <td class="textbold" style="height: 21px">Phone</td>
                                                                    <td style="height: 21px">
                                                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="textboxgrey"
                                                                            TabIndex="5" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    <td class="textbold" style="height: 21px">Fax</td>
                                                                    <td style="width: 185px; height: 21px;">
                                                                        <asp:TextBox ID="txtFax" CssClass="textboxgrey" TabIndex="6" runat="server" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    <td style="height: 21px"><asp:Button ID="btnReset" runat="server" TabIndex="40" CssClass="button" Text="Reset" AccessKey="R" /></td>
                                                                </tr>
                                                                <tr>
                                                                     <td style="height: 5px" class="textbold" colspan="6" align="center" valign="TOP">
<input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' /></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="textbold" style="width: 4px;">
                                                                    </td>
                                                                    <td class="textbold">Office Id</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtOfficeId" runat="server" CssClass="textboxgrey"
                                                                            TabIndex="7" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    <td class="textbold"></td>
                                                                    <td style="width: 185px">         
                                                                        </td>
                                                                    <td>
                                                                        <asp:HiddenField ID="hdLcode" runat="server" />
                                                                    </td>
                                                                </tr>  
                                                                <tr>
                                                                    <td class="textbold" >
                                                                    </td>
                                                                    <td class="textbold">
                                                                        <input id="hdIspNameId" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdPendingWithId" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdProcessedById" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdOrderTypeNew" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdOrderTypeCancel" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdISPId" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdISPIdName" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdNPID" runat="server" style="width: 1px" type="hidden" />
                                                                         <input id="hdIspPlanId" runat="server" style="width: 1px" type="hidden" /></td>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td style="width: 185px">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                 <tr>
                                                                     <td style="height: 2px" class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                  <tr>
                                                                    <td class="textbold" style="width: 4px;" >
                                                                    </td>
                                                                    <td class="subheading" colspan="4" style="height: 19px">
                                                                        ISP Details</td>
                                                                    <td style="height: 19px">
                                                                    </td>
                                                                </tr>
                                                                   <tr>
                                                                     <td  style ="height :5px"  class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="textbold" style="height:25px; width: 4px;">
                                                                    </td>
                                                                    <td class="textbold">ISP Name </td>
                                                                    <td>
                                                                        <asp:DropDownList id="drpIspName" tabIndex="8" onkeyup="gotop(this.id)" runat="server" Width="136px" CssClass="dropdownlist" AutoPostBack="True">
                                                                                                                </asp:DropDownList>
                                                                       <%-- <asp:TextBox ID="drpIspName" runat="server" CssClass="textboxgrey"
                                                                            TabIndex="8" ReadOnly="True"></asp:TextBox>
                                                                             <img src="../Images/lookup.gif"  onclick="javascript:return PopupISPDetails();" id="imgISP" runat="server" visible="false" style="cursor:pointer;"  />--%>
                                                                        </td>
                                                                    <td class="textbold">ISP City Name</td>
                                                                    <td style="width: 185px">
                                                                        <asp:TextBox ID="txtISPCityName" CssClass="textboxgrey" TabIndex="9" runat="server" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    <td>
                                                                        <asp:HiddenField ID="hdLoggedByID" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                  <tr>
                                                                     <td  style ="height :5px"  class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="textbold" style="width: 4px;" >
                                                                    </td>
                                                                    <td class="subheading" colspan="4" style="height: 19px"> NPID (ISP Plan)</td>
                                                                    <td style="height: 19px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                     <td style ="height :5px"  class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="textbold" style="width: 4px;">
                                                                    </td>
                                                                    <td class="textbold">NPID&nbsp;<span class="Mandatory">*</span></td>
                                                                    <td>
                                                                    <asp:DropDownList id="drpPlainId" tabIndex="10" onkeyup="gotop(this.id)" runat="server" Width="136px" CssClass="dropdownlist">
                                                                                                                </asp:DropDownList>
                                                                       <%-- <asp:TextBox ID="drpPlainId" runat="server" CssClass="textboxgrey"
                                                                            TabIndex="10" ReadOnly="True"></asp:TextBox>
                                                                            <img src="../Images/lookup.gif"  onclick="javascript:return PopupISPPlanId();" id="imgNpid"  visible ="false" runat="server" style="cursor:pointer;" />--%>
                                                                        
                                                                        </td>
                                                                    <td class="textbold">Band Width </td>
                                                                    <td style="width: 185px">
                                                                        <asp:TextBox ID="txtBandWidth" CssClass="textboxgrey" TabIndex="11" runat="server" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                     <td style ="height :5px"  class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="textbold" style="width: 4px;">
                                                                    </td>
                                                                    <td class="textbold">Installation Charges  </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtInsCharges" runat="server" CssClass="textboxgrey"
                                                                            TabIndex="12" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    <td class="textbold">Monthly Charges</td>
                                                                    <td style="width: 185px">
                                                                        <asp:TextBox ID="txtMonCharges" CssClass="textboxgrey" TabIndex="13" runat="server" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    <td></td>
                                                                </tr>
                                                                  <tr>
                                                                     <td style ="height :5px"  class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="textbold" style="width: 4px;">
                                                                    </td>
                                                                    <td class="textbold">Equipment Included</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEquipIncluded" runat="server" CssClass="textboxgrey"
                                                                            TabIndex="14" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    <td class="textbold">Equipment One Time Charges </td>
                                                                    <td style="width: 185px">
                                                                        <asp:TextBox ID="txtEqipOneTimeCharges" CssClass="textboxgrey" TabIndex="15" runat="server" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    <td></td>
                                                                </tr>
                                                                   <tr>
                                                                     <td style ="height :5px"  class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="textbold" style="width: 4px;">
                                                                    </td>
                                                                    <td class="textbold" style ="width:500px" valign ="top"   >
                                                                        Equipment Monthly Rented</td>
                                                                    <td  valign ="top">
                                                                        <asp:TextBox ID="txtEqipMonRented" runat="server" CssClass="textboxgrey"
                                                                            TabIndex="16" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    <td class="textbold">
                                                                       Delivery TimeLine</td>
                                                                    <td style="width: 185px"  valign ="top"><asp:TextBox ID="txtDelTimeLine" CssClass="textboxgrey" TabIndex="17" runat="server" ReadOnly="True"></asp:TextBox></td>
                                                                    <td></td>
                                                                </tr>
                                                               <tr>
                                                                   <td class="textbold" style="width: 4px">
                                                                   </td>
                                                                   <td class="textbold" style="width: 500px">
                                                                   </td>
                                                                   <td>
                                                                   </td>
                                                                   <td class="textbold">
                                                                   </td>
                                                                   <td style="width: 185px">
                                                                   </td>
                                                                   <td>
                                                                   </td>
                                                               </tr>
                                                                    <tr>
                                                                     <td style ="height :5px"  class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                             
                                                                    <tr>
                                                                    <td class="textbold" style="width: 4px" >
                                                                    </td>
                                                                    <td class="subheading" colspan="4" style="height: 19px">Order Details</td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                     <td  class="textbold" colspan="6" align="center" valign="TOP" style ="height :5px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width:5%">
                                                                        </td>
                                                                    <td class="textbold" style="width:20%">
                                                                        </td>
                                                                    <td colspan="2" style="width:30%"><asp:RadioButtonList ID="rdlNewCancel" runat="server" CssClass="dropdown" RepeatDirection="Horizontal"
                                                                            Width="300px" CellPadding="0" CellSpacing="0" onclick="FillOrderType()" TabIndex="1" Visible="False">
                                                                            <asp:ListItem Selected="True" Value="t">New Order</asp:ListItem>
                                                                            <asp:ListItem Value="f">Cancellation</asp:ListItem>
                                                                        </asp:RadioButtonList></td> 
                                                                      <td style="width:185px"></td>
                                                                        <td style="width:25%" ></td>                                                                    
                                                                </tr>
                                                                 <tr>
                                                                     <td  class="textbold" colspan="6" align="center" valign="TOP" style ="height :5px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold"   style="width:5%; height: 21px;"></td>
                                                                    <td class="textbold" style="width:20%; height: 21px;">Order Number&nbsp;</td>
                                                                    <td style="width:20%; height: 21px;"><asp:TextBox ID="txtOrderNumber" runat="server" CssClass="textboxgrey" TabIndex="18" ReadOnly="True"></asp:TextBox></td>
                                                                    <td class="textbold" style="width:15%; height: 21px;">
                                                                        ISP Order Status<span class="Mandatory">*</span></td>
                                                                    <td style="width:185px; height: 21px;"><asp:DropDownList ID="drpIspOrderStatus" runat="server" TabIndex="19" Width="137px" CssClass="textbold">
                                                                      </asp:DropDownList></td>
                                                                    <td style="width:20%; height: 21px;">
                                                                        </td>
                                                                </tr>
                                                              <%--  <tr>
                                                                     <td style ="height :5px" class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>--%>
                                                              <%--  <tr>
                                                                    <td class="textbold" style="width: 4px"></td>
                                                                    <td class="textbold" style="width: 15%">Order Type<span class="Mandatory">*</span></td>
                                                                    <td colspan="3" style="width:30%"><asp:DropDownList ID="ddlOrderType" runat="server" TabIndex="4" Width="437px" CssClass="textbold">
                                                                    </asp:DropDownList>&nbsp;</td>                                                                    
                                                                    <td style="width:35%"></td>
                                                                </tr>--%>
                                                                <tr>
                                                                     <td style ="height :5px" class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                  <tr>
                                                                   <td class="textbold" style="width:4px"></td>
                                                                    <td class="textbold" style="width:15%">Order Date&nbsp;</td>
                                                                    <td style="width:15%">
                                                                        <asp:TextBox ID="txtOrderDate" runat="server" CssClass="textboxgrey" MaxLength="15" TabIndex="20" ReadOnly="True"></asp:TextBox>
                                                                        &nbsp; </td>
                                                                      <td class="textbold" style="width:15%">
                                                                          Online Date</td>
                                                                      <td style="width:185px">
                                                                          <asp:TextBox ID="txtOnlineDate" runat="server" CssClass="textbox" MaxLength="12" TabIndex="21"></asp:TextBox>
                                                                          <img
                                                                              id="ImgOnlineDate" alt="" src="../Images/calender.gif" style="cursor: pointer" tabindex="6"
                                                                              title="Date selector" /><script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtOnlineDate.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "ImgOnlineDate",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    }); </script></td>
                                                                    <td style="width:35%"></td> 
                                                                </tr>
                                                                  <tr>
                                                                     <td style ="height :5px" class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                <tr>
                                                                   <td class="textbold" style="width:4px"></td>
                                                                    <td class="textbold" style="width:15%">
                                                                        <span >Expected Online Date</span></td>
                                                                    <td style="width:15%">
                                                                        <asp:TextBox ID="txtExpOnlineDate" runat="server" CssClass="textbox" MaxLength="15" TabIndex="23"></asp:TextBox>
                                                                        <img id="ImgExpOnlineDate" alt="" src="../Images/calender.gif" style="cursor: pointer" tabindex="24"
                                                                            title="Date selector" />&nbsp;<script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtExpOnlineDate.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "ImgExpOnlineDate",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    }); </script></td>
                                                                      <td class="textbold" style="width:15%">Approved by</td>
                                                                      <td style="width:185px">
                                                                          <asp:TextBox ID="txtApprovedBy" runat="server" CssClass="textboxgrey" MaxLength="15" ReadOnly="True"
                                                                              TabIndex="25"></asp:TextBox><img tabIndex="26" id="img1A" src="../Images/lookup.gif" onclick="javascript:return PopupEmployee();" runat="server" style="cursor:pointer;"   />
                                                                        <asp:HiddenField ID="hdApprovedBy" runat="server" />
                                                                       <%--  <asp:DropDownList ID="drpApprovedBy" runat="server" TabIndex="25" Width="137px" CssClass="textbold" Visible="False">
                                                                      </asp:DropDownList>--%></td>
                                                                    <td style="width:35%"></td>
                                                                </tr>
                                                                  <tr>
                                                                     <td  style ="height :5px" class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width:4px; height: 21px;"></td>
                                                                    <td class="textbold" style="width:15%; height: 21px;">Logged by&nbsp;</td>
                                                                    <td style="width:15%; height: 21px;">
                                                                        <asp:TextBox ID="txtLoggedby" runat="server" CssClass="textboxgrey"  ReadOnly="True"
                                                                            TabIndex="27"></asp:TextBox></td>
                                                                    <td class="textbold" style="width:15%; height: 21px;">
                                                                        &nbsp;Account ID</td>
                                                                    <td style="width:185px; height: 21px;">
                                                                        <asp:TextBox ID="txtCafAcId" CssClass="textbox" TabIndex="28" runat="server" MaxLength="25"></asp:TextBox></td>
                                                                    <td style="width:35%; height: 21px;"></td>
                                                                </tr>
                                                                <tr>
                                                                     <td   style="height :1px" class="textbold" colspan="6" align="center" valign="TOP" ></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 4px; height: 21px;">
                                                                    </td>
                                                                    <td class="textbold" style="height: 21px">Approval Date</td>
                                                                    <td style="height: 21px">
                                                                        <asp:TextBox ID="txtApprovalDate" runat="server" CssClass="textbox"
                                                                            TabIndex="29" MaxLength="12"></asp:TextBox>
                                                                        <img id="ImgApprovalDate" alt="" src="../Images/calender.gif" style="cursor: pointer" tabindex="30"
                                                                            title="Date selector" /><script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtApprovalDate.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "ImgApprovalDate",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    }); </script></td>
                                                                    <td class="textbold" style="height: 21px">
                                                                        WLL Number </td>
                                                                    <td style="height: 21px; width: 185px;">
                                                                        <asp:TextBox ID="txtMDNNo" CssClass="textbox" TabIndex="31" runat="server" MaxLength="25"></asp:TextBox></td>
                                                                    <td style="height: 21px"></td>
                                                                </tr>
                                                                <tr>
                                                                     <td  style ="height :5px"  class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="textbold" style="width: 4px; height: 21px;">
                                                                    </td>
                                                                    <td class="textbold" style="height: 21px">Cancellation Date</td>
                                                                    <td style="height: 21px">
                                                                        <asp:TextBox ID="txtCanDate" runat="server" CssClass="textbox"
                                                                            TabIndex="32" MaxLength="12"></asp:TextBox>
                                                                        <img id="ImgCanDate" alt="" src="../Images/calender.gif" style="cursor: pointer" tabindex="33"
                                                                            title="Date selector" /><script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtCanDate.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "ImgCanDate",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    }); </script></td>
                                                                    <td class="textbold" style="height: 21px">Login Name</td>
                                                                    <td style="height: 21px; width: 185px;">
                                                                        <asp:TextBox ID="txtLoginName" runat="server" CssClass="textbox"
                                                                            TabIndex="34" MaxLength="25"></asp:TextBox></td>
                                                                    <td style="height: 21px"></td>
                                                                </tr>
                                                                <tr>
                                                                     <td    class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>                                                              
                                                               <tr>
                                                                   <td class="textbold" style="width: 4px">
                                                                   </td>
                                                                   <td class="textbold" valign="top">
                                                                       Static IP</td>
                                                                   <td colspan="3" rowspan="1" style="">
                                                                       <asp:TextBox ID="txtStaticIP" runat="server" CssClass="textbox" MaxLength="15" TabIndex="32"></asp:TextBox></td>
                                                                   <td>
                                                                   </td>
                                                               </tr>
                                                               <tr>
                                                                   <td class="textbold" style="width: 4px">
                                                                   </td>
                                                                   <td class="textbold" valign="top">
                                                                   </td>
                                                                   <td colspan="3" rowspan="1" style="">
                                                                   </td>
                                                                   <td>
                                                                   </td>
                                                               </tr>
                                                                 <tr>
                                                                    <td class="textbold" style="width: 4px;">
                                                                    </td>
                                                                    <td class="textbold" valign="top" >Cancellation Reason</td>
                                                                    <td colspan ="3" rowspan="2" style="height: 47px">
                                                                        <asp:TextBox ID="txtCanReason" CssClass="textbox" TabIndex="35" runat="server" Width="507px" MaxLength="300" TextMode="MultiLine" Height="56px"></asp:TextBox></td>
                                                                    
                                                                    <td></td>
                                                                </tr>
                                                                
                                                                <tr>
                                                                
                                                                </tr>
                                                                
                                                                  <tr>
                                                                     <td   style ="height :5px" class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="textbold" style="width: 4px;">
                                                                    </td>
                                                                    <td class="textbold" valign="top" >Remarks</td>
                                                                    <td colspan ="3">
                                                                        <asp:TextBox ID="txtRem" runat="server" CssClass="textbox"
                                                                            TabIndex="36" Width="507px" MaxLength="300" TextMode="MultiLine" Height="64px"></asp:TextBox>
                                                                        </td>
                                                                    
                                                                </tr>
                                                                <tr>
                                                                     <td  style="height :2px"  class="textbold" colspan="6" align="center" valign="TOP">
                                                                         <asp:HiddenField ID="HiddenField1" runat="server" />
                                                                         <asp:HiddenField ID="hdIspProviderID" runat="server" />
                                                                     </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 4px">
                                                                        &nbsp;</td>
                                                                    <td colspan="4" class="ErrorMsg">
                                                                        Field Marked * are Mandatory</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>                                                           
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                   <tr>
                                                    <td style="width:100%" valign="top">
                                                        <asp:Panel ID="pnlPrevIspOrder" runat="server" Width="100%">
                                                         <table border ="0" cellpadding ="0" cellspacing="0"  width ="100%">
                                                            <tr>
                                                              <td  width ="100%">
                                                               <asp:GridView  ID="grdPrevISpOrder" AllowSorting="true" HeaderStyle-ForeColor="white" runat="server" BorderWidth="1" BorderColor="#d4d0c8" AutoGenerateColumns="False"
                                                                        Width="100%" >
                                                                           <Columns>
                                                                          
                                                                               <asp:TemplateField HeaderText="Order Number" SortExpression="OrderNumber">
                                                                                   <ItemTemplate>
                                                                                       <asp:Label ID="lblOrderNo" runat="server" Text='<%#Eval("OrderNumber")%>'></asp:Label>
                                                                                   </ItemTemplate>
                                                                                   <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                                   <HeaderStyle HorizontalAlign="Left" Width="5%" Wrap="False" />
                                                                               </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="ISP Name" SortExpression="ISPName">
                                                                                   <ItemTemplate>
                                                                                       <%#Eval("ISPName")%>
                                                                                   </ItemTemplate>
                                                                                   <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                                   <HeaderStyle HorizontalAlign="Left" Width="5%" Wrap="False" />
                                                                               </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="Agency Name" SortExpression="AgencyName">
                                                                                   <ItemTemplate>
                                                                                        <%#Eval("AgencyName")%>
                                                                                   </ItemTemplate>
                                                                                   <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                                   <HeaderStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                                                                               </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="Address" SortExpression="Address">
                                                                                   <ItemTemplate>
                                                                                         <%#Eval("Address")%>
                                                                                   </ItemTemplate>
                                                                                   <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                                   <HeaderStyle HorizontalAlign="Left" Width="15%" Wrap="False" />
                                                                               </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="Office Id" SortExpression="OfficeId">
                                                                                   <ItemTemplate>
                                                                                         <%#Eval("OfficeId")%>
                                                                                   </ItemTemplate>
                                                                                   <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                                   <HeaderStyle HorizontalAlign="Left" Width="15%" Wrap="False" />
                                                                               </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="City" SortExpression="City">
                                                                                   <ItemTemplate>
                                                                                        <%#Eval("City")%>
                                                                                   </ItemTemplate>
                                                                                   <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                                   <HeaderStyle HorizontalAlign="Left" Width="5%" Wrap="False" />
                                                                               </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="Approved Date" SortExpression="ApprovedDate">
                                                                                   <ItemTemplate>
                                                                                        <%#Eval("ApprovedDate")%>
                                                                                   </ItemTemplate>
                                                                                   <ItemStyle HorizontalAlign="Left" Width="5%" Wrap="False" />
                                                                                   <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                                                                               </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="Order Date" SortExpression="OrderDate">
                                                                                   <ItemTemplate>
                                                                                        <%#Eval("OrderDate")%>
                                                                                   </ItemTemplate>
                                                                                   <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                                   <HeaderStyle HorizontalAlign="Left" Width="5%" Wrap="False" />
                                                                               </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="Online Date" SortExpression="OnlineDate">
                                                                                   <ItemTemplate>
                                                                                       <%#Eval("OnlineDate")%>
                                                                                   </ItemTemplate>
                                                                                   <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                                   <HeaderStyle HorizontalAlign="Left" Width="5%" Wrap="False" />
                                                                               </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="Status Name" SortExpression="StatusName">
                                                                                   <ItemTemplate>
                                                                                         <%#Eval("StatusName")%>
                                                                                   </ItemTemplate>
                                                                                   <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                                   <HeaderStyle HorizontalAlign="Left" Width="5%" Wrap="False" />
                                                                               </asp:TemplateField>                                                                               
                                                                           </Columns>
                                                                           <HeaderStyle CssClass="Gridheading"/>
                                                                    <RowStyle CssClass="ItemColor" HorizontalAlign="Left"/>
                                                                    <AlternatingRowStyle  CssClass ="lightblue" HorizontalAlign="Left" />
                                                                       </asp:GridView>
                                                              </td>
                                                            </tr>
                                                             <!-- code for paging----->
                                            <tr>                                                   
                                                    <td valign ="top">
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
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
                                                                  </table></asp:Panel>
                                                    </td>
                                                </tr>
                                                
            <!-- code for paging----->
                                                         </table>
                                                        </asp:Panel>
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
