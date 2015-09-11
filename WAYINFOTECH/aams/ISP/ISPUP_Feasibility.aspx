<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ISPUP_Feasibility.aspx.vb" ValidateRequest ="false" Inherits="ISP_ISPUP_Feasibility" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>

    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
    <title>AAMS:ISP Feasibility</title>
</head>

<script type="text/javascript" src="../JavaScript/AAMS.js"></script>

<script language="javascript" type="text/javascript">

 function NewFunction()
    {   
        window.location.href="ISPUP_Feasibility.aspx?Action=I"+""; 
        //document.getElementById("rdlNew_1").style="hidden"
        return false;
    }
 function SendEmail(obj1,obj2)
 {  
  var type;
  type = "../Popup/PUUP_ProductMailSend.aspx?RequestId=" + obj1 + "&ISPID=" + obj2 ;

//  var strReturn;   
//  if (window.showModalDialog)
//  {
//      strReturn=window.showModalDialog(type,null,'dialogWidth:785px;dialogHeight:680px;status:yes;help:no;');       
//  }
//  else
//  {
//      strReturn=window.open(type,null,'height=680px,width="785px",top=100,left=100,status=1,scrollbars=0');       
  //}	
  
  window.open(type,"aa","height=680,width=785px,top=30,left=20,scrollbars=1,status=1");
  return false;  
  }
  
  function SendBTEmail(obj1,obj2)
  {
    var type;
    type ="../Popup/PUUP_ProductMailSend.aspx?RequestId=" + obj1 + "&ISPID=" + obj2 + "&MailType=BT" ; 
    window.open(type,"bb","height=680,width=785px,top=30,left=20,scrollbar=1,status=1");
    return false;
  }
     
   
function PopupAgencyPage()
{


          var type;
          // type = "../Popup/PUSR_Agency.aspx" ;
            type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");
   	        return false;
     }
     
      function EnableDisableImg()
    {   
     var rdoExistingAgency = document.getElementById('<%=rbtExistingAgency.ClientID%>'); 
        if (rdoExistingAgency.disabled)
        {
         document.getElementById("img1").style.visibility='hidden'; 
        }
        else
        {
         document.getElementById("img1").style.visibility='visible'; 
        }
       
        return true;
    }

function radionewexisting(id) 
{   

    var rdoExistingAgency = document.getElementById('<%=rbtExistingAgency.ClientID%>'); 
    var rdoNewAgency = document.getElementById("rbtNewAgency"); 
    
      // Clear Controls Values.
         if ("1" == "1")
         {
        
             document.getElementById('<%=txtAgencyName.ClientID%>').value=''
             document.getElementById('<%=txtAddress.ClientID%>').value=''
             document.getElementById('<%=txtFax.ClientID%>').value=''
             document.getElementById('<%=txtPhone.ClientID%>').value=''
             document.getElementById('<%=txtPinCode.ClientID%>').value=''
             document.getElementById('<%=txtConcernPerson.ClientID%>').value=''
             document.getElementById('<%=txtOfficeId.ClientID%>').value=''
             document.getElementById('<%=txtCity.ClientID%>').value=''
             document.getElementById('<%=txtCountry.ClientID%>').value=''
             
            document.getElementById('<%=hdAgency.ClientID%>').value = ''
            document.getElementById('<%=hdAddress.ClientID%>').value=''
            document.getElementById('<%=hdCountry.ClientID%>').value=''
            document.getElementById('<%=hdCity.ClientID%>').value=''
            document.getElementById('<%=hdOffice.ClientID%>').value=''
            document.getElementById('<%=hdPhone.ClientID%>').value=''
            document.getElementById('<%=hdFax.ClientID%>').value = ''
             
             
         }
    
      
    
     if (rdoExistingAgency.checked)     
    { 
    
 
document.getElementById("txtCountry").style.display='block'; 
document.getElementById("txtCity").style.display='block'; 
document.getElementById("drpCity").style.display='none';
document.getElementById("drpCountry").style.display='none';
 

    document.getElementById("img1").style.visibility='visible'; 
    document.getElementById('<%=txtAgencyName.ClientID%>').readOnly = true;
    document.getElementById('<%=txtAgencyName.ClientID%>').className ="textboxgrey";
    document.getElementById('<%=txtAddress.ClientID%>').readOnly = true;
    document.getElementById('<%=txtAddress.ClientID%>').className ="textboxgrey";
    document.getElementById('<%=txtPinCode.ClientID%>').readOnly = true;
    document.getElementById('<%=txtPinCode.ClientID%>').className ="textboxgrey";
    
   // document.getElementById('<%=drpCountry.ClientID%>').disabled = true;
   // document.getElementById('<%=drpCity.ClientID%>').disabled = true;
    document.getElementById('<%=txtPhone.ClientID%>').readOnly = true;
    document.getElementById('<%=txtPhone.ClientID%>').className ="textboxgrey";
    document.getElementById('<%=txtFax.ClientID%>').readOnly = true;
     document.getElementById('<%=txtFax.ClientID%>').className ="textboxgrey";
    document.getElementById('<%=txtConcernPerson.ClientID%>').readOnly = true;
    document.getElementById('<%=txtConcernPerson.ClientID%>').className ="textboxgrey";
    
    if (rdoExistingAgency.disabled)
    {
     document.getElementById("img1").style.visibility='hidden'; 
    }
    else
    {
     document.getElementById("img1").style.visibility='visible'; 
       
    
    }
        
        return;
    }
    
    if (rdoNewAgency.checked )
    {  
    
    
   document.getElementById("img1").style.visibility='hidden'; 
   document.getElementById("txtCountry").style.display="none"
document.getElementById("txtCity").style.display="none"
document.getElementById("drpCity").style.display='block';
document.getElementById("drpCountry").style.display='block';
    
   // document.getElementById('<%=txtOfficeId.ClientID%>').readOnly = true;
    
    document.getElementById('<%=txtAgencyName.ClientID%>').readOnly = false;
    document.getElementById('<%=txtAgencyName.ClientID%>').className ="textbox";
    document.getElementById('<%=txtAddress.ClientID%>').readOnly = false;
    document.getElementById('<%=txtAddress.ClientID%>').className ="textbox";
    document.getElementById('<%=txtPinCode.ClientID%>').readOnly = false;
    document.getElementById('<%=txtPinCode.ClientID%>').className ="textbox";
    document.getElementById('<%=drpCountry.ClientID%>').disabled = false;
    document.getElementById('<%=drpCity.ClientID%>').disabled = false;
    document.getElementById('<%=txtPhone.ClientID%>').readOnly = false;
    document.getElementById('<%=txtPhone.ClientID%>').className ="textbox";
    document.getElementById('<%=txtFax.ClientID%>').readOnly = false;
     document.getElementById('<%=txtFax.ClientID%>').className ="textbox";
    document.getElementById('<%=txtConcernPerson.ClientID%>').readOnly = false;
    document.getElementById('<%=txtConcernPerson.ClientID%>').className ="textbox";
   
   
   
    /*
        // Commented by Pankaj on 25th july
        document.getElementById('<%=txtAgencyName.ClientID%>').className = "textboxgrey"; 
      //  document.getElementById('<%=txtAgencyName.ClientID%>').readonly = true;        
        document.getElementById("txtAgencyName").value ="Dummy Location";  
        
        document.getElementById("txtAddress").value ="";
        document.getElementById("txtPhone").value="";
       // P document.getElementById("txtCountry").value="";
        document.getElementById("txtFax").value="";
        document.getElementById("txtOfficeId").value="";
        document.getElementById("img1").style.visibility='hidden'; 
        document.getElementById('<%=drpCity.ClientID%>').disabled=false;
        
        // disabling controls
        
      //  document.getElementById('<%=txtAgencyName.ClientID%>').disabled = true;
        document.getElementById('<%=txtPhone.ClientID%>').disabled = true;
        document.getElementById('<%=txtFax.ClientID%>').disabled = true;
     // P   document.getElementById('<%=txtCountry.ClientID%>').disabled = true;
        document.getElementById('<%=txtOfficeId.ClientID%>').disabled = true;
        document.getElementById('<%=txtAddress.ClientID%>').disabled = true;
        
       // document.getElementById("drpCity").options(0).selected = true;
       
       */
return;
    }
    
   
}
function ValidateISP()
{
    var cboCityId=document.getElementById('<%=drpCity.ClientId%>');
    var cboISP=document.getElementById('<%=drpIspName.ClientId%>');
        
   //*********** Validating Currency Code *****************************
   /*
   if(cboCityId.selectedIndex ==0)
        {
        
        document.getElementById('<%= lblError.ClientId %>').innerText="City is mandatory."
        return false;
            
        }
   */
   if(cboISP.selectedIndex ==0)
        {
        
        document.getElementById('<%= lblError.ClientId %>').innerText="ISP is mandatory."
        return false;
            
        }
     
    
}
function ValidateISPUpdate()
{
    var cboFesibilityId=document.getElementById('<%=ddlFeasibleStatus.ClientId%>');
    var txtFesibilityDate=document.getElementById('<%=txtFeasibilityDate.ClientId%>');
    var cboFesibilityIdIndex=document.getElementById('<%=ddlFeasibleStatus.ClientId%>').selectedIndex;
    //*********** Validating Currency Code *****************************
   if(cboFesibilityId.selectedIndex ==0)
        {
            document.getElementById('<%= lblError.ClientId %>').innerText="Feasibility status is mandatory."
            return false;
        }
        else
        {
            if (document.getElementById("ddlFeasibleStatus").options[cboFesibilityIdIndex].text.toUpperCase()=="FEASIBLE")
            {
                if(txtFesibilityDate.value =='')
                {
                    document.getElementById('<%= lblError.ClientId %>').innerText="Feasibility date is mandatory."
                    return false;
                }
                else
                {
                    if (isDate(txtFesibilityDate.value,"d/M/yyyy") == false)	
                        {
                           document.getElementById('<%=lblError.ClientId%>').innerText = "Fesibility date is not valid.";			
	                      txtFesibilityDate.focus();
	                       return(false);  
                        }
                
                }
            }
        }    
}

function ValidateForm()
{

  var rdoNewAgency = document.getElementById("rbtNewAgency"); 
    var cboCityId=document.getElementById('<%=drpCity.ClientId%>');
    var cboCountry=document.getElementById('<%=drpCountry.ClientId%>');
        document.getElementById('<%= lblError.ClientId %>').innerText="";
   
   if(document.getElementById('<%=txtAgencyName.ClientId%>').value == '')
   {
        document.getElementById('<%= lblError.ClientId %>').innerText="Agency is mandatory."
        document.getElementById('<%= txtAgencyName.ClientId %>').focus();
        return false;
   
   }
   
   if (rdoNewAgency.checked==true)
    { 
    
     if(document.getElementById('<%=txtAddress.ClientId%>').value == '')
           {
                document.getElementById('<%= lblError.ClientId %>').innerText="Address is mandatory."
                document.getElementById('<%= txtAddress.ClientId %>').focus();
                return false;
            }
        if(cboCountry.selectedIndex ==0)
                    {
                        document.getElementById('<%= lblError.ClientId %>').innerText="Country is mandatory."
                        cboCountry.focus();
                        return false;
                    }
                        
               if(cboCityId.selectedIndex ==0)
                    {
                     document.getElementById('<%= lblError.ClientId %>').innerText="City is mandatory."
                     cboCityId.focus();
                    return false;
                        
                    }
                    
                     if(document.getElementById('<%=txtPhone.ClientId%>').value == '')
               {
               document.getElementById('<%= lblError.ClientId %>').innerText="Phone is mandatory."
               document.getElementById('<%= txtPhone.ClientId %>').focus();
                    return false;
               
               }
               else
               {
                var strValue = document.getElementById('<%=txtPhone.ClientId%>').value
             reg = new RegExp("^[0-9]+$"); 
            if(reg.test(strValue) == false) 
            {

                document.getElementById('<%=lblError.ClientId%>').innerText ='Phone number should contain only digits.'
                document.getElementById('<%= txtPhone.ClientId %>').focus();
                return false;

             }
               
               }
               if(document.getElementById('<%=txtConcernPerson.ClientId%>').value == '')
               {
               document.getElementById('<%= lblError.ClientId %>').innerText="Contact Person is mandatory."
               document.getElementById('<%= txtConcernPerson.ClientId %>').focus();
                    return false;
               
               }
               if(document.getElementById('<%=txtPinCode.ClientId%>').value == '')
               {
               document.getElementById('<%= lblError.ClientId %>').innerText="Pin Code is mandatory."
               document.getElementById('<%= txtPinCode.ClientId %>').focus();
                    return false;
               
               }
               else
               {
                var strValue = document.getElementById('<%=txtPinCode.ClientId%>').value
             reg = new RegExp("^[0-9]+$"); 
            if(reg.test(strValue) == false) 
            {

                document.getElementById('<%=lblError.ClientId%>').innerText ='Pin Code should contain only digits.'
                document.getElementById('<%=txtPinCode.ClientId%>').focus();
                return false;

             }
               
               }
                    
                    
    
    }
   
   
          
             
           
                
               
    //  }
   return true;
   
     
    
}
  

function ConfirmDelete()
	{
		 			 
		 if (confirm("Are you sure you want to delete?")==true)
            {    
              
               return true;        
            }
            return false;
	}
</script>

<script type="text/javascript" src="../JavaScript/AAMS.js"></script>

<!-- import the calendar script -->

<script type="text/javascript" src="../Calender/calendar.js"></script>

<!-- import the language module -->

<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

<!-- import the calendar setup module -->

<script type="text/javascript" src="../Calender/calendar-setup.js"></script>

<script type="text/javascript" src="../Calender/calendar-setup.js"></script>

<body  onload ="return EnableDisableImg();">
    <form id="form1" runat="server">
        <table width="860px" align="left" class="border_rightred" style="height: 486px;">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">ISP-></span><span class="sub_menu">ISP Feasibility Request</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                ISP Feasibility Request
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" style="height: 15px">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 5%; height: 19px;">
                                                    </td>
                                                    <td class="textbold" style="width: 20%; height: 19px;">
                                                    </td>
                                                    <td colspan="2" style="width: 30%; height: 19px;">
                                                        &nbsp;<asp:RadioButton ID="rbtExistingAgency" runat="server" Checked="True" CssClass="dropdown"
                                                            GroupName="AgencyType" Text="Existing Agency" Width="103px" TabIndex="2" />
                                                        <asp:RadioButton ID="rbtNewAgency" runat="server" CssClass="dropdown" GroupName="AgencyType"
                                                            Text="New Agency" Width="85px" TabIndex="2" /></td>
                                                    <td style="width: 30%; height: 19px;">
                                                        <asp:HiddenField ID="hdlcode" runat="server" />
                                                    </td>
                                                    <td style="width: 25%; height: 19px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="6%" class="textbold" style="height: 25px">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 25px; width: 127px;">
                                                        Agency Name<span class="Mandatory">*</span></td>
                                                    <td colspan="3" style="height: 25px">
                                                        <asp:TextBox ID="txtAgencyName" CssClass="textboxgrey" MaxLength="100" runat="server"
                                                               Width="432px" TabIndex="2" ReadOnly="True"></asp:TextBox><span class="textbold">&nbsp;<img
                                                                id="img1" alt="Select & Add Agency Name" onclick="javascript:return PopupAgencyPage();"
                                                                src="../Images/lookup.gif" runat="server" /></span></td>
                                                    <td width="18%" style="height: 25px">
                                                        <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="3" AccessKey="S" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 52px" width="6%">
                                                    </td>
                                                    <td class="textbold" style="width: 127px; height: 52px">
                                                        Address<span class="Mandatory">*</span></td>
                                                    <td colspan="3" style="height: 52px">
                                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="textboxgrey" Height="40px"
                                                            MaxLength="300" TabIndex="2" TextMode="MultiLine" Width="432px" ReadOnly="True"></asp:TextBox>
                                                        
                                                    </td>
                                                    <td valign="top" width="18%" style="height: 52px">
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="3" AccessKey="N" /><br />
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3" AccessKey="R" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" >
                                                    </td>
                                                    <td class="textbold" style="width: 127px;">
                                                        Office ID</td>
                                                    <td class="textbold" style="width: 314px;">
                                                        <asp:TextBox ID="txtOfficeId" runat="server" CssClass="textboxgrey" MaxLength="10" TabIndex="7" ReadOnly="True"></asp:TextBox></td>
                                                    <td class="textbold" style="width: 180px;" >
                                                        Country<span class="Mandatory">*</span></td>
                                                     <td class="textbold" ><asp:DropDownList ID="drpCountry" runat="server" TabIndex="2" CssClass="dropdownlist"
                                                            Width="137px" AutoPostBack="True">
                                                     </asp:DropDownList>
                                                         <asp:TextBox ID="txtCountry" runat="server" CssClass="textboxgrey" MaxLength="40"
                                                             ReadOnly="True" TabIndex="7"></asp:TextBox></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td class="textbold" style="height: 26px">
                                                    </td>
                                                    <td class="textbold" style="width: 127px; height: 26px">
                                                        Fax</td>
                                                    <td style="width: 314px; height: 26px">
                                                        <asp:TextBox ID="txtFax" runat="server" CssClass="textboxgrey" MaxLength="20"
                                                            TabIndex="2" ReadOnly="True"></asp:TextBox></td>
                                                    <td  id="txtcityu2" class="textbold" style="height: 26px; width: 180px;">
                                                        City<span class="Mandatory">*</span></td>
                                                    <td id="txtcityl2" style="height: 26px">
                                                        <asp:DropDownList ID="drpCity" runat="server" TabIndex="2" CssClass="dropdownlist"
                                                            Width="137px">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" MaxLength="40" ReadOnly="True"
                                                            TabIndex="7"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 26px">
                                                    </td>
                                                    <td class="textbold" style="width: 127px; height: 26px">
                                                        Phone<span class="Mandatory">*</span></td>
                                                    <td style="width: 314px; height: 26px">
                                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="textboxgrey" MaxLength="20" TabIndex="2" ReadOnly="True"></asp:TextBox></td>
                                                    <td class="textbold" style="height: 26px; width: 180px;">
                                                        Pin Code<span class="Mandatory">*</span></td>
                                                    <td  style="height: 26px">
                                                        <asp:TextBox ID="txtPinCode" runat="server" CssClass="textboxgrey" MaxLength="12" ReadOnly="True"
                                                            TabIndex="2"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 26px">
                                                    </td>
                                                    <td class="textbold" style="width: 127px; height: 26px">
                                                        Concern Person<span class="Mandatory">*</span></td>
                                                    <td style="width: 314px; height: 26px">
                                                        <asp:TextBox ID="txtConcernPerson" runat="server" CssClass="textboxgrey" MaxLength="25"
                                                            ReadOnly="True" TabIndex="2"></asp:TextBox></td>
                                                    <td class="textbold" style="width: 180px; height: 26px">
                                                    </td>
                                                    <td style="height: 26px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 26px">
                                                    </td>
                                                    <td  id="txtcityu1" class="textbold" style="width: 127px; height: 26px">
                                                        </td>
                                                    <td  id="txtcityl1" style="width: 314px; height: 26px">
                                                        </td>
                                                    <td class="textbold" style="width: 180px; height: 26px">
                                                    </td>
                                                    <td style="height: 26px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="subheading" colspan="6" align="center" style="height: 15px">
                                                        Feasibility Details
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 25px; width: 127px;">
                                                        Request ID</td>
                                                    <td style="width: 314px; height: 25px;">
                                                        <asp:TextBox ID="txtRequestId" runat="server" TabIndex="9" CssClass="textboxgrey"
                                                            ReadOnly="true" MaxLength="40"></asp:TextBox></td>
                                                    <td class="textbold" style="height: 25px; width: 180px;">
                                                        Logged Date</td>
                                                    <td style="height: 25px; width: 209px;">
                                                        <asp:TextBox ID="txtLoggedDate" TabIndex="10" runat="server" CssClass="textboxgrey"
                                                            MaxLength="40" ReadOnly="True"></asp:TextBox>

                                                        

                                                    </td>
                                                    <td style="height: 25px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 30px">
                                                    </td>
                                                    <td class="textbold" style="height: 30px; width: 127px;">
                                                        Logged By</td>
                                                    <td style="height: 30px; width: 314px;">
                                                        <asp:TextBox ID="txtLoggedBy" runat="server" TabIndex="11" CssClass="textboxgrey"
                                                            ReadOnly="true" MaxLength="40"></asp:TextBox></td>
                                                    <td class="textbold" style="height: 30px; width: 180px;">
                                                    </td>
                                                    <td style="height: 30px; width: 209px;">
                                                        </td>
                                                    <td style="height: 30px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 72px">
                                                    </td>
                                                    <td class="textbold" style="width: 127px; height: 72px">
                                                        Remarks</td>
                                                    <td style="height: 72px" colspan="4">
                                                        <asp:TextBox ID="txtRemarks" TabIndex="2" runat="server" CssClass="textbox" MaxLength="300"
                                                            Height="72px" TextMode="MultiLine" Width="432px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="subheading" colspan="6" align="center" style="height: 15px">
                                                        ISP Details
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px">
                                                    </td>
                                                    <td class="textbold" style="height: 25px; width: 127px;">
                                                        ISP Name</td>
                                                    <td class="textbold" colspan="3" style="height: 25px">
                                                        <asp:DropDownList ID="drpIspName" TabIndex="2" runat="server" CssClass="dropdownlist"
                                                            Width="436px">
                                                        </asp:DropDownList></td>
                                                    <td style="height: 25px">
                                                        <asp:Button ID="btnAdd" CssClass="button" runat="server" Text="Add" TabIndex="2" /></td>
                                                </tr>
                                               <asp:Panel ID="pnlFStatus" runat="server" Visible ="false"> 
                                                <tr>
                                                    <td class="textbold" height="30" style="height: 25px">
                                                    </td>
                                                    <td class="textbold" style="height: 25px; width: 127px;">
                                                        Feasibility Status</td>
                                                    <td style="width: 314px; height: 25px;">
                                                        <asp:DropDownList ID="ddlFeasibleStatus" TabIndex="14" runat="server" CssClass="dropdownlist"
                                                            Width="137px">
                                                        </asp:DropDownList>

                                                        

                                                    </td>
                                                    <td class="textbold" style="height: 25px; width: 190px;">
                                                        Feasible Date</td>
                                                    <td style="height: 25px; width: 209px;">
                                                        <asp:TextBox ID="txtFeasibilityDate" runat="server" CssClass="textbox" TabIndex="15"
                                                            MaxLength="40"></asp:TextBox>
                                                        <img id="Img3" alt="" src="../Images/calender.gif" style="cursor: pointer" title="Date selector" />
                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField    :    '<%=txtFeasibilityDate.clientId%>',
                                                                                                    ifFormat      :    "%d/%m/%Y",
                                                                                                    button        :    "Img3",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                        </script>
                                                        </td>
                                                    <td>
                                                    <asp:Button ID="btnUpdate" CssClass="button" runat="server" Text="Update" visible="false" />
                                                    <!-- code by pankaj -->
                                                        </td>
                                                </tr>
                                                 </asp:Panel> 
                                                <tr>
                                                    <td colspan="6" style="height: 4px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" style="height: 4px" class="ErrorMsg">Field Marked * are Mandatory
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ErrorMsg" colspan="6" style="height: 4px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" style="height: 4px">
                                                        <asp:GridView ID="gvISPUpdate" runat="server" AutoGenerateColumns="False"
                                                            TabIndex="7" Width="100%" EnableViewState="True" AllowSorting ="true">
                                                            <Columns>
                                                                <asp:BoundField DataField="ISPName" HeaderText="ISP Name" SortExpression="ISPName" />
                                                                <asp:BoundField DataField="FeasibleStatusName" HeaderText="Feasibility Status" SortExpression="FeasibleStatusName" />
                                                             
                                                                 <asp:TemplateField HeaderText="Feasibility Date" SortExpression="FeasibleDate">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                           <%#ConvertDate(Eval("FeasibleDate"))%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>  
                                                                <asp:TemplateField HeaderText="Mail Send Date" SortExpression="MailSendDatetime">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                           <%#ConvertDate(Eval("MailSendDatetime"))%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>                                                                                                                                                                                         
                                                                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign ="Center">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                           <asp:LinkButton CssClass="LinkButtons" ID="btnEdit" CausesValidation="false" Text="Update Status"
                                                                            CommandArgument='<% #Container.DataItem("ISPID") + "|" + Container.DataItem("FeasibleStatusID") + "|" + Container.DataItem("FeasibleDate")%>' CommandName="UpdateX" runat="server" ></asp:LinkButton>&nbsp;
                                                                            <asp:LinkButton CssClass="LinkButtons" ID="lnkDelete" CausesValidation="false" Text="Delete" CommandArgument='<% #Container.DataItem("ISPID")%>' CommandName="DeleteX" runat="server" OnClientClick="return ConfirmDelete()"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Send Mail" HeaderStyle-HorizontalAlign ="Center">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                         <asp:LinkButton CssClass="LinkButtons" ID="btnMail" CausesValidation="false" Text="Send Mail" CommandArgument='<%# Eval("ISPID") %>' CommandName="SendMailX" runat="server" ></asp:LinkButton>&nbsp;
                                                                         <asp:LinkButton CssClass="LinkButtons" ID="btnBTMail" CausesValidation="false" Text="Send BTMail" CommandArgument='<%# Eval("ISPID") %>' CommandName="SendBTMailX" runat="server" ></asp:LinkButton>
                                                                         <asp:HiddenField ID="hdISPID" Value=' <%#Eval("ISPID")%>' runat="server" />  
                                                                         <asp:HiddenField ID="hdFeasibleStatusID" Value=' <%#Eval("FeasibleStatusID")%>' runat="server" />
                                                                         <asp:HiddenField ID="hdFeasibleStatusDate" Value=' <%#Eval("FeasibleDate")%>' runat="server" />
                                                                         <asp:HiddenField ID="hdMailSendDatetime" Value=' <%#Eval("MailSendDatetime")%>' runat="server" />                                                                                                                                                                                                                                                                                                            
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                                                                        
                                                               
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <RowStyle CssClass="textbold" />
                                                            <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                                        </asp:GridView>

                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td colspan="6" height="12">
                                                    <asp:HiddenField ID="hdCity" runat="server" />
                                                        <asp:HiddenField ID="hdRequest" runat="server" />
                                                        <asp:HiddenField ID="hdAgency" runat="server" />
                                                        <asp:HiddenField ID="hdIspId" runat="server" />
                                                         <asp:HiddenField ID="hdAddress" runat="server" />
                                                         <asp:HiddenField ID="hdCountry" runat="server" />
                                                         <asp:HiddenField ID="hdPin" runat="server" />
                                                         <asp:HiddenField ID="hdOffice" runat="server" />
                                                         <asp:HiddenField ID="hdPhone" runat="server" />
                                                         <asp:HiddenField ID="hdFax" runat="server" />
                                                         <asp:HiddenField ID="hdISPRequestPage" runat="server" />
                                                         <asp:HiddenField ID="hdConcernPerson" runat="server" />
                                                         
                                                          <asp:HiddenField ID="hdData" runat="server" />
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" height="12">
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
