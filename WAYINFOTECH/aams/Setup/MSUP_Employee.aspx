<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_Employee.aspx.vb" Inherits="Setup_MSUP_Employee"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AAMS: Employee</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
</head>
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
<script type="text/javascript" language ="javascript" >
  
    //ashish added 
    function ShowPTRHistory()
      {  
     var type="../Popup/PUSR_EmployeeLog.aspx";
     var strReturn;
     if (window.showModalDialog)
     {
     strReturn=window.showModalDialog(type,null,'dialogWidth:880px;dialogHeight:600px;help:no;');       
     }
    else
    {  
       strReturn=window.open(type,null,'height=600,width=880,top=30,left=20,scrollbars=1');   
     }
       
    }

    //end ashish
  
  
    /*Added by Tapan Nath Activate Login if Login Required is True*/
    function ActivateLogin()
        {
           // debugger;
          if (document.getElementById("chkLoginRequired").checked==false)
            {                
                document.getElementById("txtLogin").className ="textboxgrey";
                document.getElementById("txtPassword").className ="textboxgrey";
                document.getElementById("txtRetypePassword").className ="textboxgrey";
                document.getElementById("txtIPAddress").className ="textboxgrey";
                
                document.getElementById("txtLogin").readOnly =true;
                document.getElementById("txtPassword").readOnly=true;
                document.getElementById("txtRetypePassword").readOnly=true;
                document.getElementById("txtIPAddress").readOnly=true;
                document.getElementById("chkAgmntSigned").checked=false;
                document.getElementById("chkAgmntSigned").disabled=true;
                
                document.getElementById("txtLogin").value=''
                document.getElementById("txtPassword").value=''
                document.getElementById("txtRetypePassword").value=''
                document.getElementById("txtIPAddress").value=''
                return false;
            }
          else
            {
                document.getElementById("txtLogin").className ="textbox";
                document.getElementById("txtPassword").className ="textbox";
                document.getElementById("txtRetypePassword").className ="textbox";
                document.getElementById("txtIPAddress").className ="textbox";
                
                document.getElementById("txtLogin").readOnly=false;
                document.getElementById("txtPassword").readOnly=false;
                document.getElementById("txtRetypePassword").readOnly=false;                                
                document.getElementById("chkAgmntSigned").disabled=false;
                //document.getElementById("txtIPAddress").readOnly=false;                
                 if (document.getElementById("txtIPAddress").value != '')
                    {
                    document.getElementById("txtIPAddress").readOnly=true;
                     document.getElementById("txtIPAddress").className ="textboxgrey";
                    }
                else
                    {
                    document.getElementById("txtIPAddress").readOnly=false;
                    }
                return false;
            }  
        }
        /*Added by Tapan Nath Activate Login if Login Required is True*/
     function ColorMethodEmployeeNavigation(id,total)
            {   
           
                    document.getElementById("lblError").innerHTML='';
                    var ctextFront;
                    var ctextBack;
                    var Hcontrol;
                    var HFlush;
                    
                    ctextFront = id.substring(0,15);        
                    ctextBack = id.substring(17,26);   
                   
                    for(var i=0;i<total;i++)
                    {
                        HFlush = "0" + i;
                        Hcontrol = ctextFront +  HFlush + ctextBack;
                        if (document.getElementById(Hcontrol).className != "displayNone")
                        {
                            document.getElementById(Hcontrol).className="headingtabactive";
                        }
                    }
                    
                   document.getElementById(id).className="headingtab";     
                  
                   document.getElementById('lblPanelClick').value =id;                  
                  
                  if('<%=Session("Action").ToString().Split("|").GetValue(0)%>'=='U')
                 //  if( document.getElementById('hdSessionValue').value.split('|')[0]=='U')
                  
                   {      
                   if (id == (ctextFront +  "00" + ctextBack))
                   {   
                       document.getElementById('hdTabType').value='0';
                       window.location.href="MSUP_Employee.aspx"
                       return false;
                   }       
                   else if (id == (ctextFront +  "01" + ctextBack))
                   {   
                       document.getElementById('hdTabType').value='1';
                       window.location.href="MSUP_EmployeePermission.aspx" 
                       return false;         
                   }
                   else if (id == (ctextFront +  "02" + ctextBack))
                   {
                        document.getElementById('hdTabType').value='2';
                       window.location.href="MSUP_EmployeeGroup.aspx"
                       return false;
                   }
                   else if (id == (ctextFront +  "03" + ctextBack))
                   {
                       document.getElementById('hdTabType').value='3';
                       window.location.href="MSUP_EmployeeIP.aspx"
                       return false;
                      
                   }
                   else if (id == (ctextFront +  "04" + ctextBack))
                   {
                        document.getElementById('hdTabType').value='4';
                        window.location.href="MSUP_EmployeeSupervisory.aspx"
                        return false;
                       
                   }
                   
                    else if (id == (ctextFront +  "05" + ctextBack))
                   {
                       
                        window.location.href="MSUP_EmployeeHelpDesk.aspx"
                        return false;
                       
                   }
       
                   }                               
                   
            }
</script>

<body onload="PageLoadMethodForEmployee();">
    <form id="form1" runat="server" defaultbutton="btnSave" defaultfocus="txtEmployeeName">
        <table width="860px" align="left"  class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">Setup-></span><span class="sub_menu">Employee</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#1A61A9" class="heading" align="center">
                                            Manage User
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td valign="top">
                                            <asp:HiddenField ID="lblPanelClick" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="height: 22px">
                                            <asp:Repeater ID="theTabStrip" runat="server">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" CssClass="headingtabactive" runat="server" Text="<%# Container.DataItem %>" />&nbsp;
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" class="redborder" style="height: 354px">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" align="center" height="25px" valign="TOP" rowspan="0">
                                                    <asp:Label ID="lblError" class="ErrorMsg" runat="server"></asp:Label>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td width="100%" valign="top" >
                                                        <asp:Panel ID="pnlEmployee" runat="server" Width="100%">
                                                            <table width="100%" border="0" align="left" cellpadding="2" cellspacing="0">
                                                                <tr>
                                                                    <td >
                                                                        &nbsp;</td>
                                                                    <td class="textbold">
                                                                        Employee Name <span class="Mandatory">*</span></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEmployeeName" MaxLength="40" TabIndex="1" CssClass="textbox" runat="server"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        Department <span class="Mandatory">*</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpDepartment" runat="server" TabIndex="1" Width="137px" CssClass="textbold">
                                                                        </asp:DropDownList></td>
                                                                    <td>
                                                                        <asp:Button ID="btnSave" runat="server" TabIndex="2" CssClass="button" Text="Save"  AccessKey="S"/>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td  >
                                                                    </td>
                                                                    <td class="textbold" >
                                                                        Designation <span class="Mandatory">*</span> </td>
                                                                    <td>
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"   ID="drpDesignation" TabIndex="1" runat="server" Width="136px" CssClass="textbold"></asp:DropDownList></td>
                                                                    <td class="textbold">
                                                                        1a office <span class="Mandatory">*</span></td>
                                                                    <td>
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpAoffice" runat="server" TabIndex="1" Width="137px" CssClass="textbold">
                                                                        </asp:DropDownList></td>
                                                                    <td>
                                                                        <asp:Button ID="btnNew" runat="server" TabIndex="2" CssClass="button" Text="New"  AccessKey="N"/></td>
                                                                </tr>
                                                                <tr>
                                                                    <td >
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Mobile Number <span class="Mandatory">*</span> </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtMobileNumber" MaxLength="30" TabIndex="1" CssClass="textbox" runat="server"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        City<span class="Mandatory"><span style="font-size: 9pt; color: #000000; font-family: Arial">
                                                                            </span><span class="Mandatory">*</span></span></td>
                                                                    <td>

                                                                        
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpCity" runat="server" TabIndex="1" Width="137px" CssClass="textbold">
                                                                    </asp:DropDownList></td>
                                                                    <td><asp:Button ID="btnHistory" runat="server" TabIndex="2" CssClass="button" Text="History"  AccessKey="N"/></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 26px" >
                                                                    </td>
                                                                    <td class="textbold" style="height: 26px">
                                                                        Email <span class="Mandatory">*</span> </td>
                                                                    <td style="height: 26px">
                                                                        <asp:TextBox ID="txtEmail" CssClass="textbox" MaxLength="100" TabIndex="1" runat="server"></asp:TextBox></td>
                                                                    <td class="textbold" style="height: 26px">
                                                                        Head of Department <span class="Mandatory">*</span> </td>
                                                                    <td style="height: 26px">
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpHOD" runat="server" TabIndex="1" Width="137px" CssClass="textbold">
                                                                        </asp:DropDownList></td>
                                                                    <td style="height: 26px">
                                                                        <asp:Button ID="btnReset" runat="server" TabIndex="2" CssClass="button" Text="Reset"  AccessKey="R"/></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">Login Required</td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkLoginRequired" runat="server" onclick="ActivateLogin()" Checked="True"/></td>
                                                                    <td class="textbold">
                                                                        Immediate Supervisor <span class="Mandatory">*</span></td>
                                                                    <td>
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpImmediateSuperVisor" runat="server" TabIndex="1" Width="137px" CssClass="textbold">
                                                                        </asp:DropDownList></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td >
                                                                        &nbsp;</td>
                                                                    <td class="textbold">Login<span class="Mandatory"><span style="font-size: 9pt; color: #000000; font-family: Arial">
                                                                             </span><span class="Mandatory">*</span></span></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtLogin" MaxLength="20" CssClass="textbox" TabIndex="1" runat="server"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        <%--<span class="Mandatory">*</span>--%> 
                                                                        First form to be displayed</td>
                                                                    <td>
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpFirstForm" runat="server" Width="137px" TabIndex="1" CssClass="textbold">
                                                                        
                                                                        </asp:DropDownList></td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td >
                                                                    </td>
                                                                    <td class="textbold">Password<span class="Mandatory"><span style="color: #000000"></span><span class="Mandatory">
                                                                            <span style="font-size: 9pt; color: #000000; font-family: Arial"> </span>
                                                                            <span class="Mandatory">*</span></span></span></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPassword" MaxLength="15" CssClass="textbox" TextMode="Password" TabIndex="1"  runat="server"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        Restrict </td>
                                                                    <td>
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpRestrict" runat="server" TabIndex="1" Width="137px" CssClass="textbold">
                                                                        <asp:ListItem></asp:ListItem>
                                                                        <asp:ListItem Text="Own Agency" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Amadeus Office" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Security Region" Value="4"></asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td >
                                                                    </td>
                                                                    <td class="textbold">Retype Password<span class="Mandatory"><span style="font-size: 9pt; color: #000000; font-family: Arial">
                                                                         </span></span></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtRetypePassword" MaxLength="15" TabIndex="1" TextMode="Password" CssClass="textbox" runat="server"></asp:TextBox></td>
                                                                        
                                                                    <td class="textbold">
                                                                        Region List <span class="Mandatory">*</span></td>
                                                                    <td class="displayNone">
                                                                        <asp:TextBox ID="txtDOJ" runat="server" MaxLength="10" TabIndex="1" CssClass="textbox"></asp:TextBox>
                                                                                                                    <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDOJ.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "f_trigger_d18",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                         <img id="f_trigger_d18" alt="" src="../Images/calender.gif" TabIndex="16" title="Date selector" style="cursor: pointer" /></td>
                                                                    <td>
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpRegionList"  runat="server" TabIndex="1" Width="137px" CssClass="textbold">
                                                                        </asp:DropDownList></td>
                                                                </tr>
                                                                <tr>
                                                                    <td >
                                                                    </td>
                                                                    <td class="textbold">IP Address<span class="Mandatory"><span class="Mandatory"></span></span></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIPAddress" runat="server" CssClass="textbox" MaxLength="20" TabIndex="1"></asp:TextBox></td>
                                                                    <td class="textbold" id="tdPasswordExpire" runat="server">
                                                                        Password Never Expire</td>
                                                                    <td><asp:CheckBox ID="chkPassExpire" runat="server" TabIndex="1" /></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold"  id="tdChangePassword" runat="server">
                                                                        Change Password on First Login</td>
                                                                    <td valign="top" >
                                                                    <asp:CheckBox ID="chkChangePass" runat="server" Checked="True" TabIndex="1" />
                                                                    </td>
                                                                    
                                                                    <td class="textbold" valign="top" >
                                                                    Agreement Signed
                                                                    </td>
                                                                    
                                                                    <td valign="top"  >
                                                                     <asp:CheckBox ID="chkAgmntSigned" runat="server" TabIndex="1" />
                                                                    </td>
                                                                    <td >
                                                                    </td>
                                                                </tr>
                                                                <tr id="trActive" runat="server">
                                                                    <td class="textbold" >
                                                                    </td>
                                                                    <td class="textbold" >
                                                                        Active</td>
                                                                    <td  valign="top"><asp:CheckBox ID="chkActive" runat="server" TabIndex="1" />
                                                                    <input type="hidden" runat="server" id="hdRequest" style="width: 6px" /></td>
                                                                    <td class="textbold" >
                                                                        Agency Type</td>
                                                                    <td class="textbold"><asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlAgencyType" runat="server" TabIndex="1" Width="137px" CssClass="textbold">
                                                                    </asp:DropDownList></td>
                                                                    <td >
                                                                    </td>
                                                                </tr>
                                                                <tr id="trActive1" runat="server">
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Show my agency to supervisor</td>
                                                                    <td valign="top"><asp:CheckBox ID="chkShowToSupervisor" runat="server" TabIndex="1" /></td>
                                                                    <td class="textbold">
                                                                        Date of Leaving</td>
                                                                    <td class="textbold">
                                                                        <asp:TextBox ID="txtDOL" runat="server" MaxLength="10" TabIndex="1" CssClass="textbox"></asp:TextBox><img id="Img1" alt="" src="../Images/calender.gif" TabIndex="1" title="Date selector" style="cursor: pointer" />
                                                                                                                                                                         <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDOL.clientId%>',
                                                                                                    ifFormat       :     "%d/%m/%Y",
                                                                                                    button         :    "Img1",
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
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="hdTabType" runat="server" />
                    <asp:HiddenField ID="hdSessionValue" runat="server" />
                </td>
            </tr>
        </table>
            

    </form>
    <script language ="javascript"  type ="text/javascript" >
     ActivateLogin();
    </script>
</body>
</html>
