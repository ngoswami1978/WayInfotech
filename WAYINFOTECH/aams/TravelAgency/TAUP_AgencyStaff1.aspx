<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TAUP_AgencyStaff1.aspx.vb"
    Inherits="TravelAgency_TAUP_AgencyStaff1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Agency Staff</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>

    <!-- import the calendar script -->

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <!-- import the language module -->

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <!-- import the calendar setup module -->

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

</head>

<script language="javascript" type="text/javascript">  

  function FillAgencyDetails()
         {
        
           var officeId;
           officeId=  document.getElementById('txtOfficeId').value;
           var officeIdClassName=document.getElementById('txtOfficeId').className;
         
           if (officeId != ""  && officeIdClassName !="textboxgrey")
           {
                document.getElementById('txtAgencyName').value="Searching...";
                CallServerAgencyDetails(officeId,"This is context from client");
           }
           else
           {
        
           }
           return false;
        }

function ReceiveServerAgencyDetails(args, context)
        {
    
         document.getElementById('lblError').innerText ="";
         var obj = new ActiveXObject("MsXml2.DOMDocument");
                        
            if (args =="")
            {                            
                document.getElementById('hdAgencyNameId').value="";			  
			    document.getElementById('txtAgencyName').value="";
			    document.getElementById('txtAddress').value="";
			    document.getElementById('txtCity').value="";
			    document.getElementById('txtCountry').value="";
            }
            else
            {
                  var parts = args.split("$");
                  obj.loadXML(parts[0]);
                  var dsRoot=obj.documentElement; 
            
			    if (dsRoot !=null)
			    { 
			            document.getElementById('hdAgencyNameId').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("LOCATION_CODE") ;			           
			            document.getElementById('txtAgencyName').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("NAME")
			            document.getElementById('txtAddress').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("ADDRESS") + '\n ' + dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("ADDRESS1")
			            document.getElementById('txtCity').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("CITY")
			            document.getElementById('txtCountry').value=dsRoot.getElementsByTagName("AGNECY")[0].getAttribute("COUNTRY")
			     }     	       
		   }            
        
        }

      function  TAUPAgencyStaff()
       {              
           window.location="TAUP_AgencyStaff1.aspx?Action=I";
           return false;
       }
       
       function AgencyStaffMandatory()
        {
        
         if (document.getElementById("TxtSignInNum").value=="")
         {          
            document.getElementById("lblError").innerHTML="Sign In is mandatory.";
            document.getElementById("TxtSignInNum").focus();
            return false;
          
         }
          if (document.getElementById("TxtSignInNum").value.length<4)
          {
            document.getElementById("lblError").innerHTML="Sign In is not valid.";
            document.getElementById("TxtSignInNum").focus();
            return false;
          }
          if (document.getElementById("TxtSignInChar").value=="")
         {          
            document.getElementById("lblError").innerHTML="Sign In is mandatory.";
            document.getElementById("TxtSignInChar").focus();
            return false;
          
         }
          if (document.getElementById("TxtSignInChar").value.length<2)
          {
            document.getElementById("lblError").innerHTML="Sign In is not valid.";
            document.getElementById("TxtSignInChar").focus();
            return false;
          }
         
        if (document.getElementById("txtName").value=="")
         {          
            document.getElementById("lblError").innerHTML="First Name is mandatory.";
            document.getElementById("txtName").focus();
            return false;
          
         }
        if (document.getElementById("txtName").value!="")
         {
           if(IsDataValid(document.getElementById("txtName").value,7)==false)
            {
            document.getElementById("lblError").innerHTML="First Name is not valid.";
            document.getElementById("txtName").focus();
            return false;
            } 
         }
         if (document.getElementById("txtSurName").value=="")
         {          
            document.getElementById("lblError").innerHTML="Sur Name is mandatory.";
            document.getElementById("txtSurName").focus();
            return false;
          
         }
        if (document.getElementById("txtSurName").value!="")
         {
           if(IsDataValid(document.getElementById("txtSurName").value,7)==false)
            {
            document.getElementById("lblError").innerHTML="Sur Name is not valid.";
            document.getElementById("txtSurName").focus();
            return false;
            } 
         }
       
         
//          if (document.getElementById("txtDesig").value!="")
//         {
//           if(IsDataValid(document.getElementById("txtDesig").value,7)==false)
//            {
//            document.getElementById("lblError").innerHTML="Designation is not valid.";
//            document.getElementById("txtDesig").focus();
//            return false;
//            } 
//         }
//     
//        if(document.getElementById('<%=txtDob.ClientId%>').value != '')
//        {
//        if (isDate(document.getElementById('<%=txtDob.ClientId%>').value,"d/M/yyyy") == false)	
//        {
//           document.getElementById('<%=lblError.ClientId%>').innerText = "Date of birth is not valid.";			
//	       document.getElementById('<%=txtDob.ClientId%>').focus();
//	       return(false);  
//        }
//         }
 
//             if(document.getElementById('<%=txtDow.ClientId%>').value != '')
//        {
//        if (isDate(document.getElementById('<%=txtDow.ClientId%>').value,"d/M/yyyy") == false)	
//        {
//           document.getElementById('<%=lblError.ClientId%>').innerText = "Date of wedding is not valid.";			
//	       document.getElementById('<%=txtDow.ClientId%>').focus();
//	       return(false);  
//        }
//         } 
//        if (compareDates(document.getElementById('<%=txtDob.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtDow.ClientId%>').value,"d/M/yyyy")==1)
//        {
//           document.getElementById('<%=lblError.ClientId%>').innerText = "Date of birth can't be greater than wedding date.";			
//	       document.getElementById('<%=txtDob.ClientId%>').focus();
//	       return(false);  
//        }




           if (document.getElementById("txtEmail").value=="")
         {          
            document.getElementById("lblError").innerHTML="Email is mandatory.";
            document.getElementById("txtEmail").focus();
            return false;
          
         }
         if(document.getElementById("txtEmail").value!='')
         {              
        if(checkEmail(document.getElementById("txtEmail").value)==false)
        {
            document.getElementById("lblError").innerHTML='Email is not valid.';
            document.getElementById("txtEmail").focus();
            return false;
        }
        } 
           if (document.getElementById("TxtMob").value=="")
         {          
            document.getElementById("lblError").innerHTML="Mobile No. is mandatory.";
            document.getElementById("TxtMob").focus();
            return false;
          
         }
            if (document.getElementById("DlstDesg").value=="")
         {          
            document.getElementById("lblError").innerHTML="Designation is mandatory.";
            document.getElementById("DlstDesg").focus();
            return false;
          
         }
             
           if (document.getElementById("txtDob").value=="")
         {          
            document.getElementById("lblError").innerHTML="Date of birth is mandatory.";
            document.getElementById("txtDob").focus();
            return false;
          
         }
         
              if (document.getElementById("txtDob").value!="")
         {   
             reg = new RegExp("^[0-9/]+$"); 
            if(reg.test(document.getElementById("txtDob").value) == false) 
            {
                    document.getElementById("lblError").innerHTML="Date of birth is not valid.";
                    document.getElementById("txtDob").focus();
                    return false; 
             }   
         }
         
          var dob =document.getElementById("txtDob").value.split("/");
        //  alert(dob.length);
         if (dob.length<2)
         {
                document.getElementById("lblError").innerHTML="Date of birth is not valid.";
                document.getElementById("txtDob").focus();
                return false;   
         }
         if (dob.length==2)
         {
            if (dob[0].length!=2)
            {
                document.getElementById("lblError").innerHTML="Date of birth is not valid.";
                document.getElementById("txtDob").focus();
                return false;   
            }
             if (dob[1].length!=2)
            {
                document.getElementById("lblError").innerHTML="Date of birth is not valid.";
                document.getElementById("txtDob").focus();
                return false;  
            }
            
             if (parseInt(dob[1],10)== 2 )
             {  
                     if (parseInt(dob[0],10)>=29)
                     {
                        document.getElementById("lblError").innerHTML="Date of birth is not valid.";
                        document.getElementById("txtDob").focus();
                        return false;  
                     }
             }
             
              if (parseInt(dob[1],10)== 4 || parseInt(dob[1],10)== 6 || parseInt(dob[1],10)== 9 || parseInt(dob[1],10)== 11   )
             {  
                 
                     if (parseInt(dob[0],10)>=30)
                     {
                        document.getElementById("lblError").innerHTML="Date of birth is not valid.";
                        document.getElementById("txtDob").focus();
                        return false;  
                     }
             }
             
            
         
         }
         if (dob.length==3)
         {
            if (isDate(document.getElementById('<%=txtDob.ClientId%>').value,"d/M/yyyy") == false)	
            {
                document.getElementById("lblError").innerHTML="Date of birth is not valid.";
                document.getElementById("txtDob").focus();
                return false;  
            }
         }
         
         //##############  DOW #################
         
            if (document.getElementById("DlstMaritalStatus").value=="Yes")
         {          
                   if (document.getElementById("txtDow").value=="")
                 {          
                    document.getElementById("lblError").innerHTML="Date of wedding is mandatory.";
                    document.getElementById("txtDow").focus();
                    return false;              
                 }
                 
            }
         
             if(document.getElementById('<%=txtDow.ClientId%>').value != '')
        {
        
            reg = new RegExp("^[0-9/]+$"); 
            if(reg.test(document.getElementById("txtDow").value) == false) 
            {
                      document.getElementById("lblError").innerHTML="Date of wedding is not valid.";
                    document.getElementById("txtDow").focus();
                    return false; 
             }
             
          var doW =document.getElementById("txtDow").value.split("/");
                    //  alert(dob.length);
                     if (doW.length<2)
                     {
                            document.getElementById("lblError").innerHTML="Date of wedding is not valid.";
                            document.getElementById("txtDow").focus();
                            return false;   
                     }
                     if (doW.length==2)
                     {
                        if (doW[0].length!=2)
                        {
                            document.getElementById("lblError").innerHTML="Date of wedding is not valid.";
                            document.getElementById("txtDow").focus();
                            return false;   
                        }
                         if (doW[1].length!=2)
                        {
                            document.getElementById("lblError").innerHTML="Date of wedding is not valid.";
                            document.getElementById("txtDow").focus();
                            return false;  
                        }
                        
                         if (parseInt(doW[1],10)== 2 )
                         {  
                                 if (parseInt(doW[0],10)>=29)
                                 {
                                    document.getElementById("lblError").innerHTML="Date of wedding is not valid.";
                                    document.getElementById("txtDow").focus();
                                    return false;  
                                 }
                         }
                         
                          if (parseInt(doW[1],10)== 4 || parseInt(doW[1],10)== 6 || parseInt(doW[1],10)== 9 || parseInt(doW[1],10)== 11   )
                         {  
                             
                                 if (parseInt(doW[0],10)>=30)
                                 {
                                    document.getElementById("lblError").innerHTML="Date of wedding is not valid.";
                                    document.getElementById("txtDow").focus();
                                    return false;  
                                 }
                         }
                         
                        
                     
                     }
                     if (doW.length==3)
                     {
                        if (isDate(document.getElementById('<%=txtDow.ClientId%>').value,"d/M/yyyy") == false)	
                        {
                            document.getElementById("lblError").innerHTML="Date of wedding is not valid.";
                            document.getElementById("txtDow").focus();
                            return false;  
                        }
                     
                    }
        
            if (isDate(document.getElementById('<%=txtDow.ClientId%>').value,"d/M/yyyy") == true)	
            {
                if(document.getElementById('<%=txtDob.ClientId%>').value != '')
                {
                       if (isDate(document.getElementById('<%=txtDob.ClientId%>').value,"d/M/yyyy") == true)
                       {
                            if (compareDates(document.getElementById('<%=txtDob.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtDow.ClientId%>').value,"d/M/yyyy")==1)
                                {
                                   document.getElementById('<%=lblError.ClientId%>').innerText = "Date of birth can't be greater than wedding date.";			
	                               document.getElementById('<%=txtDob.ClientId%>').focus();
	                               return(false);  
                                }
                         }
                }
            }
        }    
         
         
//       if (document.getElementById("txtNotes").value.length>300)
//        {
//             document.getElementById("lblError").innerHTML="Notes can't be greater than 300 characters."
//             document.getElementById("txtNotes").focus();
//             return false;
//        }  
       if (document.getElementById("txtAgencyName").value=="")
       {          
            document.getElementById("lblError").innerHTML="Agency Name is mandatory.";            
            return false;
       }
            return true;         
       }
       function PopupAgencyPage()
       {
        

           var type;
            type = "TASR_Agency.aspx?Popup=T" ;
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;


 
       }
       
      function next(currentControl, maxLength, nextControl)
   {
         checknumeric(currentControl);
        if(document.getElementById(currentControl).value.length >= maxLength)
        {  
            document.getElementById(nextControl).focus();
        }
  } 

 function checkalphbets(objCtrlid)
   {
            var tempVal=document.getElementById(objCtrlid).value;
            var validVal="";
            for(var i=0;i<tempVal.length;i++)
            {
                 vAscii = tempVal.charCodeAt(i) ;
                 if((vAscii >= 65 && vAscii <= 90) || (vAscii >= 97 && vAscii <= 122))
                    {
                        validVal+=tempVal.substr(i,1);
                    }
                 else
                    {
                    }
            }
            document.getElementById(objCtrlid).value=validVal;
   }
    
</script>

<body>
    <form id="form1" runat="server" defaultfocus="TxtSignInNum" defaultbutton ="btnSave">
        <table width="860px" align="left" style="height: 486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">Travel Agency-></span><span class="sub_menu">Agency Staff</span></td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#1A61A9" class="heading" align="center">
                                            Manage Agency Staff&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" class="redborder">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" align="center" valign="TOP" rowspan="0">
                                                        <asp:Label ID="lblError" class="ErrorMsg" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td width="100%" valign="top">
                                                        <asp:Panel ID="pnlEmployee" runat="server" Width="100%">
                                                            <table width="100%" border="0" align="left" cellpadding="2" cellspacing="0">
                                                                <tr>
                                                                    <td colspan="5">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 50px">
                                                                    </td>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Sign In<span class="Mandatory">*</span></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TxtSignInNum" CssClass="textbox" runat="server" MaxLength="4" TabIndex="1"
                                                                            Width="50px"></asp:TextBox>&nbsp;
                                                                        <asp:TextBox ID="TxtSignInChar" CssClass="textbox" runat="server" MaxLength="2" TabIndex="1"
                                                                            onkeyup="checkalphbets(this.id)" Width="40px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td> <asp:Button ID="btnNew" runat="server" CssClass="button" TabIndex="2" Text="New"
                                                                            AccessKey="n" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Name </td>
                                                                    <td colspan="3">
                                                                        <table cellpadding="0" cellspacing="0" border="0">
                                                                            <tr>
                                                                                <td class="textbold">
                                                                                    Title&nbsp;</td>
                                                                                <td class="textbold">
                                                                                    First Name<span class="Mandatory">*</span>&nbsp;</td>
                                                                                <td class="textbold">
                                                                                    Second Name&nbsp;</td>
                                                                                <td class="textbold">
                                                                                    Sur Name<span class="Mandatory">*</span></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:DropDownList ID="DlstTitle" runat="server" CssClass ="dropdownlist" TabIndex="1">
                                                                                       <asp:ListItem Text="" Value="" ></asp:ListItem>
                                                                                        <asp:ListItem Text="Mr." Value="Mr."></asp:ListItem>
                                                                                          <asp:ListItem Text="Ms." Value="Ms."></asp:ListItem>
                                                                                        <asp:ListItem Text="Mrs." Value="Mrs."></asp:ListItem>
                                                                                    </asp:DropDownList>&nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtName" CssClass="textbox" runat="server" Width="140px" TabIndex="1" MaxLength="30"></asp:TextBox>&nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtSecName" CssClass="textbox" runat="server" Width="140px" TabIndex="1" MaxLength="30"></asp:TextBox>&nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtSurName" CssClass="textbox" runat="server" Width="140px" TabIndex="1" MaxLength="30"></asp:TextBox>&nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td> <asp:Button ID="btnSave" runat="server" TabIndex="2" CssClass="button" Text="Save"
                                                                            AccessKey="s" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Email<span class="Mandatory">*</span>
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtEmail" CssClass="textbox" runat="server" Width="498px" TabIndex="1"
                                                                            MaxLength="100"></asp:TextBox></td>
                                                                    <td><asp:Button ID="btnReset" runat="server" TabIndex="2" CssClass="button" Text="Reset"
                                                                            AccessKey="r" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Mobile No.<span class="Mandatory">*</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TxtMob" CssClass="textbox" runat="server" TabIndex="1" MaxLength="10"  Width="145px" onkeyup="checknumeric(this.id);"></asp:TextBox>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Phone No.</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPhone1" CssClass="textbox"   Width="145px" runat="server" TabIndex="1" MaxLength="30" onkeyup="checknumeric(this.id);"></asp:TextBox>
                                                                        <asp:TextBox ID="txtFax1" CssClass="textbox" runat="server" TabIndex="1" MaxLength="30"
                                                                            Visible="false" Width="145px"></asp:TextBox></td>
                                                                    <td>
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Designation<span class="Mandatory">*</span>
                                                                    </td>
                                                                    <td>
                                                                    <asp:DropDownList ID="DlstDesg" runat="server" Width="148px"  CssClass ="dropdownlist" TabIndex="1">
                                                                                       <asp:ListItem></asp:ListItem>
                                                                                      
                                                                                    </asp:DropDownList>
                                                                        <asp:TextBox ID="txtDesig" runat="server" CssClass="textbox" Width="145px" TabIndex="1"
                                                                         Visible ="false"     MaxLength="40"></asp:TextBox>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Date of Birth<span class="Mandatory">*</span></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDob" runat="server" CssClass="textbox" TabIndex="1" Width="113px"
                                                                            MaxLength="10"></asp:TextBox>&nbsp;&nbsp;<img id="f_trigger_dob" alt="" src="../Images/calender.gif"
                                                                               tabindex="1" title="Date selector" style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDob.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "f_trigger_dob",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                        </script>

                                                                    </td>
                                                                    <td>
                                                                       </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Marital Status
                                                                    </td>
                                                                    <td>
                                                                         <asp:DropDownList ID="DlstMaritalStatus" runat="server" Width="148px" CssClass ="dropdownlist" TabIndex="1">
                                                                                          <asp:ListItem Text="" Value="" ></asp:ListItem>
                                                                                        <asp:ListItem Text="No" Value="No" ></asp:ListItem>
                                                                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                                       
                                                                                    </asp:DropDownList>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Date of Wedding</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDow" runat="server" CssClass="textbox" TabIndex="1" Width="113px"
                                                                            MaxLength="10"></asp:TextBox>&nbsp;&nbsp;<img id="f_trigger_dow" alt="" src="../Images/calender.gif"
                                                                                tabindex="1" title="Date selector" style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDow.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "f_trigger_dow",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                        </script>

                                                                    </td>
                                                                    <td>
                                                                        </td>
                                                                </tr>
                                                                <tr style ="display:none;">
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Responsible</td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkRes" runat="server" TabIndex="1" /></td>
                                                                    <td class="textbold">
                                                                        Correspondence</td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkCor" runat="server" TabIndex="1" /></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr style ="display:none;">
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Notes</td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtNotes" runat="server" CssClass="textbox" Height="72px" TextMode="MultiLine"
                                                                            Width="498px" TabIndex="1" MaxLength="300"></asp:TextBox></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr style ="display:none;">
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Contact Person</td>
                                                                    <td colspan="3">
                                                                        <asp:CheckBox ID="chkContactPerson" runat="server" CssClass="textbox" /></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold" colspan="4">
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                        &nbsp;</td>
                                                                    <td class="subheading" colspan="4">
                                                                        Agency Details
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 5px">
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                        &nbsp;</td>
                                                                    <td class="textbold">
                                                                        Office Id
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtOfficeId" runat="server" CssClass="textbox" MaxLength="9" TabIndex="1"
                                                                            ReadOnly="false"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        </td>
                                                                    <td>
                                                                        </td>
                                                                    
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                        &nbsp;</td>
                                                                    <td class="textbold">
                                                                        Agency Name <span id="MrkStar" runat="server" class="Mandatory">*</span></td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textboxgrey" MaxLength="40"
                                                                            TabIndex="1" Width="498px" ReadOnly="True"></asp:TextBox>
                                                                        <img id="ImgAgency" runat="server" src="../Images/lookup.gif" onclick='javascript:return PopupAgencyPage();'
                                                                         tabindex="1"  alt=""  style="cursor: pointer;" />
                                                                          <asp:HiddenField ID="hdPageSource" runat ="server" />
                                                                         </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Address</td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="textboxgrey" Height="50px"
                                                                            MaxLength="40" TabIndex="1" Width="498px" ReadOnly="True"></asp:TextBox></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                        &nbsp;</td>
                                                                    <td class="textbold">
                                                                        City
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" MaxLength="30" TabIndex="1"
                                                                            ReadOnly="True"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        Country</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCountry" runat="server" CssClass="textboxgrey" MaxLength="30"
                                                                            TabIndex="1" Width="142px" ReadOnly="True"></asp:TextBox></td>
                                                                    
                                                                </tr>
                                                                <tr height="10">
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                        &nbsp;</td>
                                                                    <td colspan="4" class="ErrorMsg">
                                                                        <%--Field Marked * are Mandoatry--%>
                                                                        Field Marked * are Mandatory<asp:HiddenField ID="hdAgencyNameId" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
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
