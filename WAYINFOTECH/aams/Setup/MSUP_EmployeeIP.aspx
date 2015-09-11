<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_EmployeeIP.aspx.vb" Inherits="Setup_MSUP_EmployeeIP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Employee</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
</head>
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<script language="javascript" type="text/javascript">


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
    <form id="form1" runat="server" defaultbutton="btnIpSave">
        <table width="860px" align="left"  class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">Setup-></span><span class="sub_menu">Employee IP</span>
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
                                                            <table width="100%" border="0" align="left" cellpadding="2" cellspacing="0">
                                                                <tr>
                                                                    <td colspan="7" >
                                                                    </td>
                                                                </tr>                                                                
                                                                <tr>
                                                                    <td >
                                                                    </td>
                                                                    <td class="textbold">
                                                                        <asp:CheckBox ID="chkIPRestriction" runat="server" AutoPostBack="True" />
                                                                        </td>
                                                                    <td class="textbold">
                                                                        IP Restriction</td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>    
                                                                <tr>
                                                                    <td >
                                                                        &nbsp;</td>
                                                                    <td class="textbold">
                                                                        IP Pool</td>
                                                                    <td class="textbold">
                                                                        <asp:DropDownList ID="drpIpPool" runat="server" Width="137px" CssClass="textbold">
                                                                        </asp:DropDownList></td>
                                                                    <td>
                                                                        <asp:Button ID="btnIpApply" runat="server" CssClass="button" Text="Apply" /></td>
                                                                    <td>
                                                                        <asp:Button ID="btnIpSave" runat="server" CssClass="button" Text="Save" AccessKey="S" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td  style="width: 20%">
                                                                    </td>
                                                                    <td class="textbold" width="10%">
                                                                        IP <span class="Mandatory">*</span>
                                                                    </td>
                                                                    <td width="20%">
                                                                        <asp:TextBox ID="txtIP" runat="server" MaxLength="15" CssClass="textbox">
                                                                        </asp:TextBox></td>
                                                                    <td width="22%">
                                                                        <asp:Button ID="btnIpAdd" runat="server" CssClass="button" Text="Add" /></td>
                                                                    <td width="25%">
                                                                        <asp:Button ID="Button14" runat="server" CssClass="button" Text="Reset"  AccessKey="R"/></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                        &nbsp;</td>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>
                                                                    </td>
                                                                </tr>                                                              
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="ErrorMsg" colspan="4">
                                                                        Field Marked * are Mandatory</td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="5" align="center">
                                                                        <asp:GridView ID="gvIPPool" runat="server" 
                                                                            AutoGenerateColumns="False" Width="60%"  BorderWidth ="1"
                                                                           >
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="IP Pool">
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("IP")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="Action">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkDelete" runat="server" CssClass="LinkButtons" CommandName="DeleteX" CommandArgument='<%#Eval("IP")%>' Text="Delete"></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                            <RowStyle CssClass="textbold" />
                                                                            <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                        </asp:GridView></td>
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
