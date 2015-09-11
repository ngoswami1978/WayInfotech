<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TAUP_AgencyStaff.aspx.vb" Inherits="TravelAgency_TAUP_AgencyStaff" %>
<%@ Register Src="../UserControl/MenuControl.ascx" TagName="MenuControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Agency</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" /> 
     <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />   
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>  
       <script type="text/javascript" language ="javascript" >      
     </script>    

   
       
</head>
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
<script type ="text/javascript" >
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
    
 function AgencyStaffMandatory()
        {
        
         if (document.getElementById("TxtSignInNum").value=="")
         {          
//            document.getElementById("lblError").innerHTML="Sign In is mandatory.";
//            document.getElementById("TxtSignInNum").focus();
//            return false;
          
         }
          if (document.getElementById("TxtSignInChar").value!="")
          {
              if (document.getElementById("TxtSignInNum").value.length<4)
              {
                document.getElementById("lblError").innerHTML="Sign In is not valid.";
                document.getElementById("TxtSignInNum").focus();
                return false;
              }
          }
          if (document.getElementById("TxtSignInChar").value=="")
         {          
//            document.getElementById("lblError").innerHTML="Sign In is mandatory.";
//            document.getElementById("TxtSignInChar").focus();
//            return false;
          
         }
         
          if (document.getElementById("TxtSignInNum").value!="")
          {
              if (document.getElementById("TxtSignInChar").value.length<2)
              {
                document.getElementById("lblError").innerHTML="Sign In is not valid.";
                document.getElementById("TxtSignInChar").focus();
                return false;
              }
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
                      document.getElementById("lblError").innerHTML="Date of wedding is mandatory.";
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
         

     
            return true;         
       }
</script>
<body>
    <form id="form1" runat="server" defaultfocus="txtName" defaultbutton ="btnSave">
        <table width="860px" align="left" style="height: 486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">Travel Agency-&gt;</span><span class="sub_menu">Agency</span>
                                        </td>
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
                                            <uc1:MenuControl ID="MenuControl1" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" class="redborder">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" align="center" valign="TOP" rowspan="0"><asp:Label ID="lblError" class="ErrorMsg" runat="server"></asp:Label></td>
                                                </tr>                                                
                                                <tr>
                                                    <td width="100%" valign="top" >
                                                        <asp:Panel ID="pnlEmployee" runat="server" Width="100%">
                                                            <table width="100%" border="0" align="left" cellpadding="2" cellspacing="0">
                                                            <tr>
                                                            <td colspan="5" ></td>
                                                            </tr>
                                                            <tr>
                                                                    <td  style="width: 50px">
                                                                    </td>
                                                                    <td style="width: 120px">
                                                                        </td>
                                                                    <td style="width: 210px">
                                                                        </td>
                                                                    <td style="width: 145px">
                                                                        </td>
                                                                    <td style="width: 255px">
                                                                        </td>
                                                                    <td style="width: 90px"></td>
                                                                </tr>
                                                                
                                                                  <tr>
                                                                    <td  >
                                                                    </td>
                                                                    <td class="textbold"> Sign In<span class="Mandatory"></span>
                                                                        </td>
                                                                    <td > <asp:TextBox ID="TxtSignInNum" CssClass="textbox" runat="server" MaxLength="4" TabIndex="1"
                                                                            Width="50px"></asp:TextBox>&nbsp;
                                                                        <asp:TextBox ID="TxtSignInChar" CssClass="textbox" runat="server" MaxLength="2" TabIndex="1"
                                                                            onkeyup="checkalphbets(this.id)" Width="40px"></asp:TextBox>
                                                                        </td>
                                                                    <td >
                                                                        </td>
                                                                    <td >
                                                                        </td>
                                                                    <td ><asp:Button ID="btnNew" runat="server" CssClass="button" TabIndex="2" Text="New" AccessKey="N" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td  >
                                                                    </td>
                                                                    <td class="textbold" >
                                                                        Name <span class="Mandatory">*</span></td>
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
                                                                                       <asp:ListItem></asp:ListItem>
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
                                                                    <td><asp:Button ID="btnSave" runat="server" TabIndex="2" CssClass="button" Text="Save" AccessKey="S" />
                                                                        </td>
                                                                </tr>
                                                                 <tr>
                                                                    <td >
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Email<span class="Mandatory">*</span></td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtEmail" CssClass="textbox" runat="server" Width="495px" TabIndex="1" MaxLength="100"></asp:TextBox></td>
                                                                    <td><asp:Button ID="btnReset" runat="server" TabIndex="2" CssClass="button" Text="Reset" AccessKey="R" />
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td >
                                                                    </td>
                                                                    <td class="textbold">Mobile No.<span class="Mandatory">*</span>
                                                                        </td>
                                                                    <td>
                                                                     <asp:TextBox ID="TxtMob" CssClass="textbox" runat="server" onkeyup="checknumeric(this.id);" TabIndex="1" MaxLength="10" Width="140px"></asp:TextBox>
                                                                        </td>
                                                                    <td class="textbold">
                                                                        Phone No.</td>
                                                                    <td>
                                                                    <asp:TextBox ID="txtPhone" CssClass="textbox" runat="server"  onkeyup="checknumeric(this.id);"  TabIndex="1" MaxLength="30" Width="140px"></asp:TextBox>
                                                                        <asp:TextBox ID="txtFax" CssClass="textbox" runat="server" TabIndex="1" MaxLength="30" Width="140px" Visible ="false" ></asp:TextBox></td>
                                                                    <td>
                                                                        </td>
                                                                </tr>
                                                                 <tr>
                                                                    <td >
                                                                    </td>
                                                                    <td class="textbold">Designation<span class="Mandatory">*</span>
                                                                        </td>
                                                                    <td>
                                                                     <asp:DropDownList ID="DlstDesg" runat="server" Width="148px"  CssClass ="dropdownlist" TabIndex="1">
                                                                                       <asp:ListItem></asp:ListItem>
                                                                                      
                                                                                    </asp:DropDownList>
                                                                      <asp:TextBox ID="txtDesig"  Visible="false"  runat="server" CssClass="textbox" Width="466px" TabIndex="2" MaxLength="40"></asp:TextBox>
                                                                        </td>
                                                                    <td class="textbold">
                                                                         Date of Birth<span class="Mandatory">*</span></td>
                                                                    <td>
                                                                    <asp:TextBox ID="txtDob" runat="server" CssClass="textbox" TabIndex="1" Width="140px" MaxLength="10"></asp:TextBox><img id="f_trigger_dob" alt="" src="../Images/calender.gif" TabIndex="16" title="Date selector" style="cursor: pointer" runat="server"  /> <img id="f_trigger_dob2" alt="" src="../Images/calender.gif" TabIndex="19" title="Date selector" style="cursor: pointer" runat="server" visible ="false"  />                                                                                                <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDob.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "f_trigger_dob",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script></td>
                                                                    <td>
                                                                       </td>
                                                                </tr>
                                                                    <tr>
                                                                    <td >
                                                                    </td>
                                                                    <td class="textbold"> Marital Status
                                                                        </td>
                                                                    <td>
                                                                      <asp:DropDownList ID="DlstMaritalStatus" runat="server" Width="148px" CssClass ="dropdownlist" TabIndex="1">
                                                                                          <asp:ListItem ></asp:ListItem>
                                                                                        <asp:ListItem Text="No" Value="No" ></asp:ListItem>
                                                                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                                       
                                                                                    </asp:DropDownList>
                                                                      <asp:TextBox ID="TextBox1"  Visible="false"  runat="server" CssClass="textbox" Width="466px" TabIndex="2" MaxLength="40"></asp:TextBox>
                                                                        </td>
                                                                    <td class="textbold">
                                                                         Date of Wedding</td>
                                                                    <td>
                                                                     <asp:TextBox ID="txtDow" runat="server" CssClass="textbox" TabIndex="1" Width="140px" MaxLength="10"></asp:TextBox><img id="f_trigger_dow" alt="" src="../Images/calender.gif" TabIndex="16" title="Date selector" style="cursor: pointer" runat="server"  /> <img id="f_trigger_dow2" alt="" src="../Images/calender.gif" TabIndex="26" title="Date selector" style="cursor: pointer" runat="server" visible ="false"  />                                                                                                <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDow.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "f_trigger_dow",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script></td>
                                                                    <td>
                                                                        </td>
                                                                </tr>
                                                               
                                                              
                                                                <tr style ="display:none;">
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Responsible</td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkRes" runat="server" TabIndex="1" /></td>
                                                                    <td class="textbold">
                                                                        Correspondence</td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkCor" runat="server" TabIndex="1" Width="80px" /></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr  style ="display:none;">
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Notes</td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtNotes" runat="server" CssClass="textbox" Height="72px" TextMode="MultiLine"
                                                                            Width="466px" TabIndex="1" MaxLength="300"></asp:TextBox></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Contact Person</td>
                                                                    <td colspan="3">
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drplstConPer" runat="server" CssClass="dropdown" TabIndex="1"  Width="394px">
                                                                        </asp:DropDownList>
                                                                    <asp:Button ID="btnUpdateStaff" runat="server" Text="Update" CssClass="button" TabIndex="1" /></td>
                                                                    <td>
                                                                        &nbsp;</td> 
                                                                </tr>
                                                                 <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        </td>
                                                                    <td>
                                                                        </td>
                                                                    <td>
                                                                        </td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td colspan="4" class="ErrorMsg">
                                                                        Field Marked * are Mandatory</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td >
                                                                    </td>
                                                                     <td colspan="4">
                                                                        &nbsp;&nbsp;                                                                                                                                         
                                                                                    <asp:GridView ID="gvAgencyStaff" HeaderStyle-ForeColor="white" runat="server" AllowSorting="true"  AutoGenerateColumns="False" TabIndex="15" Width="700px" EnableViewState="true"   ShowHeader ="true" EmptyDataText="No Row Exist" Visible="False"  >                                                                                 
                                                                                         <Columns>                                                                                                                                                                  
                                                                                        <asp:TemplateField HeaderText="Name" SortExpression="STAFFNAME">                                                                                
                                                                                            <itemtemplate>
                                                                                                <%#Eval("STAFFNAME")%>
                                                                                                <asp:HiddenField ID="hdAGENCYSTAFFID" runat="server" Value='<%#Eval("AGENCYSTAFFID")%>' />
                                                                                            </itemtemplate>
                                                                                        </asp:TemplateField>     
                                                                                            <asp:BoundField DataField="DESIGNATION" SortExpression="DESIGNATION" HeaderText="Designation"   />                                                                                                                                                                     
                                                                                             <asp:TemplateField  >
                                                                                                    <HeaderTemplate   >
                                                                                                        <asp:Label ID="lblAction" runat="server" Text="Action"></asp:Label>
                                                                                                    </HeaderTemplate>
                                                                                                    <ItemTemplate>
                                                                                                           <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp;&nbsp;<a href="#" class="LinkButtons" id="linkDelete" runat="server">
                                                                                                                        Delete</a>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="20%" CssClass="ItemColor" />
                                                                                                    <HeaderStyle Wrap="False" CssClass="Gridheading" HorizontalAlign="Left" />
                                                                                              </asp:TemplateField>
                                                                                         </Columns>
                                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                                        <RowStyle CssClass="textbold" />
                                                                                        <HeaderStyle CssClass="Gridheading" ForeColor="White" />                                                                                
                                                                                        </asp:GridView>                                                                                                                                          
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        </td>
                                                                    <td>
                                                                        </td>
                                                                    <td>
                                                                        </td>
                                                                    <td>
                                                                        </td>
                                                                    <%--<td>
                                                                        &nbsp;</td>--%>
                                                                </tr>
                                                                <!-- code for paging----->
                                            <tr>                                                   
                                                    <td valign ="top" colspan ="5"  >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="760px">
                                                      <table border="0" cellpadding="0" cellspacing="0" style="width: 760px">
                                                                      <tr class="paddingtop paddingbottom">                                                                                                                                                
                                                                          <td style="width: 180px" class="left" nowrap="nowrap"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  ReadOnly="true" Width="77px" CssClass="textboxgrey" TabIndex="5" ></asp:TextBox></td>
                                                                          <td style="width: 100px" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev" TabIndex="5"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 256px" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" TabIndex="5" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 87px" class="left">
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
