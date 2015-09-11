<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_EmployeeSupervisory.aspx.vb" Inherits="Setup_MSUP_EmployeeSupervisory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <form id="form1" runat="server" defaultbutton="btnSuperReset" >
        <table width="860px" align="left" style="height:486px"  class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">Setup-></span><span class="sub_menu">Employee Supervisory Rights</span>
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
                                                    <td class="textbold" align="center" height="25px" valign="TOP" rowspan="0">
                                                    <asp:Label ID="lblError" class="ErrorMsg" runat="server"></asp:Label>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td width="100%" valign="top">                                                       
                                                        <asp:Panel ID="pnlSupervisor" runat="server" Width="100%">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td style="width: 10%">
                                                                    </td>
                                                                    <td style="width: 70%" align="right">
                                                                        <asp:DataGrid AlternatingItemStyle-CssClass="lightblue" BorderWidth="1" BorderColor="#d4d0c8"
                                                                            AutoGenerateColumns="False" HeaderStyle-CssClass="Gridheading" ID="grdSuperVisor"
                                                                            ItemStyle-CssClass="ItemColor" runat="server" Width="87%">
                                                                            <Columns>
                                                                                <asp:TemplateColumn HeaderText="Domain" HeaderStyle-HorizontalAlign="left">
                                                                                    <ItemTemplate>
                                                                                       <asp:Label ID="lblDomainName" runat="server" Text='<%#Eval("DomainName")%>'></asp:Label>
                                                                                       <asp:HiddenField ID="hdDomainId" runat="server" Value='<%#Eval("DomainID")%>' />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="left" />
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn HeaderText="Supervisor" HeaderStyle-HorizontalAlign="left">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkSuperVisor" runat="server"></asp:CheckBox>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="left" />
                                                                                </asp:TemplateColumn>
                                                                            </Columns>
                                                                        </asp:DataGrid>
                                                                    </td>
                                                                    <td style="width: 20%" valign="top">
                                                                        <table>
                                                                            <tr>
                                                                                <td style="width: 3px">
                                                                                    <asp:Button ID="btnSuperSave" runat="server" CssClass="button" Text="Save"  AccessKey="S"/></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Button ID="btnSuperReset" runat="server" CssClass="button" Text="Reset" AccessKey="R" /></td>
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
