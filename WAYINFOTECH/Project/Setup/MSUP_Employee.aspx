<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_Employee.aspx.vb" Inherits="Setup_MSUP_Employee"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WAY: Employee</title>
    <link href="../CSS/WAY.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
</head>

<script type="text/javascript" src="../JavaScript/WAY.js"></script>

<!-- import the calendar script -->

<script type="text/javascript" src="../Calender/calendar.js"></script>

<!-- import the language module -->

<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

<!-- import the calendar setup module -->

<script type="text/javascript" src="../Calender/calendar-setup.js"></script>

<script type="text/javascript" language="javascript">
  
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
                   }                   
            }
</script>

<body>
    <form id="form1" runat="server" defaultbutton="btnSave" defaultfocus="txtEmployeeName">
        <table width="860px" align="left" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">Setup-></span><span class="sub_menu">User Profile</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#1A61A9" class="heading" align="center" style="height: 20px">
                                            Manage User Profile</td>
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
                                                    <td width="100%" valign="top">
                                                        <asp:Panel ID="pnlEmployee" runat="server" Width="100%">
                                                            <table width="100%" border="0" align="left" cellpadding="2" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td class="textbold">
                                                                        Employee Name <span class="Mandatory">*</span></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEmployeeName" MaxLength="40" TabIndex="1" CssClass="textbox"
                                                                            runat="server" Width="269px"></asp:TextBox></td>
                                                                    <td class="textbold" style="width: 10px">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnSave" runat="server" TabIndex="9" CssClass="button" Text="Save"
                                                                            AccessKey="S" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Contact Person&nbsp; <span class="Mandatory">*</span></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtContactPerson" runat="server" CssClass="textbox" MaxLength="40"
                                                                            TabIndex="2" Width="269px"></asp:TextBox></td>
                                                                    <td class="textbold" style="width: 10px">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnNew" runat="server" TabIndex="10" CssClass="button" Text="New"
                                                                            AccessKey="N" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Mobile Number <span class="Mandatory">*</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtMobileNumber" MaxLength="30" TabIndex="3" CssClass="textbox"
                                                                            runat="server" Width="269px"></asp:TextBox></td>
                                                                    <td class="textbold" style="width: 10px">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnReset" runat="server" TabIndex="11" CssClass="button" Text="Reset"
                                                                            AccessKey="R" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Email <span class="Mandatory">*</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEmail" CssClass="textbox" MaxLength="100" TabIndex="4" runat="server" Width="269px"></asp:TextBox></td>
                                                                    <td class="textbold" style="width: 10px">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnHistory" runat="server" TabIndex="12" CssClass="button" Text="History"
                                                                            AccessKey="N" Visible="False" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 26px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 26px">
                                                                        Login<span class="Mandatory"><span style="font-size: 9pt; color: #000000; font-family: Arial">
                                                                        </span><span class="Mandatory">*</span></span></td>
                                                                    <td style="height: 26px">
                                                                        <asp:TextBox ID="txtLogin" MaxLength="20" CssClass="textbox" TabIndex="5" runat="server" Width="269px"></asp:TextBox></td>
                                                                    <td class="textbold" style="height: 26px; width: 10px;">
                                                                    </td>
                                                                    <td style="height: 26px">
                                                                    </td>
                                                                    <td style="height: 26px">
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Password<span class="Mandatory"><span style="color: #000000"></span><span class="Mandatory">
                                                                            <span style="font-size: 9pt; color: #000000; font-family: Arial"></span><span class="Mandatory">
                                                                                *</span></span></span></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPassword" MaxLength="15" CssClass="textbox" TextMode="Password"
                                                                            TabIndex="6" runat="server" Width="269px"></asp:TextBox></td>
                                                                    <td class="textbold" style="width: 10px">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td class="textbold">
                                                                        Retype Password<span class="Mandatory"><span style="font-size: 9pt; color: #000000;
                                                                            font-family: Arial"> </span></span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtRetypePassword" MaxLength="15" TabIndex="7" TextMode="Password"
                                                                            CssClass="textbox" runat="server" Width="269px"></asp:TextBox></td>
                                                                    <td class="textbold" style="width: 10px">
                                                                        <%--<span class="Mandatory">*</span>--%>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold" >                                                                       </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkChangePass" runat="server" Checked="True" TabIndex="8"  Visible="false"/></td>
                                                                    <td class="textbold" style="width: 10px">
                                                                    
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold" style="width: 10px">
                                                                    </td>
                                                                    <td class="displayNone">
                                                                        </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr id="trActive1" runat="server">
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td valign="top">
                                                                    </td>
                                                                    <td class="textbold" style="width: 10px">
                                                                    </td>
                                                                    <td class="textbold">
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

    <script language="javascript" type="text/javascript">
     ActivateLogin();
    </script>

</body>
</html>
