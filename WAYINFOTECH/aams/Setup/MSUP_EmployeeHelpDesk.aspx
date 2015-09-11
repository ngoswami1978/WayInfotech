<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_EmployeeHelpDesk.aspx.vb" Inherits="Setup_MSUP_EmployeeHelpDesk" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Employee</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
</head>
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<script language="javascript" type="text/javascript">

function ValidateEmployeeHelpDesk()
{
   return true;
}
function ColorMethod(id,total)
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

<body  >
    <form id="form1" runat="server" defaultbutton="btnSave">
        <table width="860px" align="left"  class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">Setup-></span><span class="sub_menu">Employee HelpDesk</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  class="heading" align="center">
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
                                        <td valign="top">
                                            <asp:Repeater ID="theTabStrip" runat="server">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" CssClass="headingtabactive" runat="server" Text="<%# Container.DataItem %>" />&nbsp;
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" class="redborder">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" align="center"  valign="TOP" rowspan="0">
                                                    <asp:Label ID="lblError" cssclass="ErrorMsg" runat="server"></asp:Label>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td width="100%" valign="top">
                                                            <table width="100%" border="0" align="left" cellpadding="1" cellspacing="1">
                                                                <tr>
                                                                    
                                                                    <td class="subheading" colspan="4">
                                                                        Functional ( Default Settings )</td>
                                                                    
                                                                </tr>   
                                                                                                                           
                                                                <tr>
                                                                    <td >
                                                                    </td>
                                                                    <td class="textbold" style="width: 99px">
                                                                        &nbsp;</td>
                                                                    <td class="textbold">
                                                                        </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                       </td>
                                                                </tr>    
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold" style="width: 99px">
                                                                        Assigned To&nbsp;<span class="Mandatory"></span></td>
                                                                    <td class="textbold">
                                                                    <asp:DropDownList  onkeyup="gotop(this.id)"  ID="DlstFuncAssignTo" runat="server" TabIndex="1" Width="174px" CssClass="textbold">
                                                                        </asp:DropDownList>
                                                                       </td>
                                                                      <td align ="right" > <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" AccessKey="S" TabIndex="2" />
                                                                    </td>
                                                                    <td>
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td >
                                                                        &nbsp;</td>
                                                                    <td class="textbold" style="width: 99px">
                                                                        Contact Type&nbsp;<span class="Mandatory"></span></td>
                                                                    <td class="textbold">
                                                                    <asp:DropDownList  onkeyup="gotop(this.id)"  ID="DlstFuncConType" runat="server" TabIndex="1" Width="174px" CssClass="textbold">
                                                                    </asp:DropDownList>
                                                                       </td>
                                                                    <td align ="right" ><asp:Button ID="BtnReset" runat="server" CssClass="button" Text="Reset"  AccessKey="R" TabIndex="2"/>
                                                                        </td>
                                                                    <td>
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td  style="width: 20%">
                                                                    </td>
                                                                    <td class="textbold" style="width: 99px">
                                                                        &nbsp;</td>
                                                                    <td width="20%">
                                                                        </td>
                                                                    <td width="22%">
                                                                        </td>
                                                                    <td width="25%">
                                                                        </td>
                                                                </tr>
                                                                 <tr>
                                                                      <td class="subheading" colspan="4">
                                                                        Technical ( Default Settings )</td>
                                                                    
                                                                </tr>  
                                                                 <tr>
                                                                    <td  style="width: 20%">
                                                                    </td>
                                                                    <td class="textbold" style="width: 99px">
                                                                        </td>
                                                                    <td width="20%">
                                                                        </td>
                                                                    <td width="22%">
                                                                        </td>
                                                                    <td width="25%">
                                                                        </td>
                                                                </tr>                                                                
                                                                 
                                                                 <tr>
                                                                    <td  style="width: 20%">
                                                                    </td>
                                                                    <td class="textbold" style="width: 99px">
                                                                        Assigned To&nbsp;<span class="Mandatory"></span>
                                                                        &nbsp;</td>
                                                                    <td width="20%">
                                                                        <asp:DropDownList ID="DlstTechAssignTo" runat="server" Width="174px" CssClass="textbold" TabIndex="1">
                                                                        </asp:DropDownList></td>
                                                                    <td width="22%">
                                                                        </td>
                                                                    <td width="25%">
                                                                        </td>
                                                                </tr>   
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold" style="width: 99px">
                                                                        Contact Type&nbsp;</td>
                                                                    <td class="textbold"><asp:DropDownList  onkeyup="gotop(this.id)"  ID="DlstTechConType" runat="server" TabIndex="1" Width="174px" CssClass="textbold">
                                                                    </asp:DropDownList></td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        </td>
                                                                </tr>                                                         
                                                               <%-- <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="ErrorMsg" colspan="4">
                                                                        Field Marked * are Mandatory</td>
                                                                </tr>--%>
                                                                <tr>
                                                                    <td colspan="5" align="center">
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold" colspan="3">
                                                                    </td>
                                                                    <td>
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
                    <asp:HiddenField ID="hdTabType" runat="server" />
                </td>
            </tr>
        </table>
            

    </form>
</body>
</html>
