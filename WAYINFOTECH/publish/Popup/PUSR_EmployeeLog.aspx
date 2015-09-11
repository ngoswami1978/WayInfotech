<%@ page language="VB" autoeventwireup="false" inherits="Popup_PUSR_EmployeeLog, App_Web_pusr_employeelog.aspx.8c51f6d0" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Agency</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <base target="_self"/>
</head>

<script type="text/javascript" src="../JavaScript/WAY.js"></script>

<!-- import the calendar script -->

<script type="text/javascript" src="../Calender/calendar.js"></script>

<!-- import the language module -->

<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

<!-- import the calendar setup module -->

<script type="text/javascript" src="../Calender/calendar-setup.js"></script>

<%--<script language="javascript" type="text/javascript">
    function AdvanceSearch()
    {           
        if(document.getElementById('<%=hdAdvanceSearch.ClientID%>').value=="1")
        {
            document.getElementById('btnUp').src='../images/up.jpg';
            document.getElementById('<%=pnlAdvanceSearch.ClientID%>').style.display ='none'
            document.getElementById('<%=hdAdvanceSearch.ClientID%>').value='0';
        }
        else
        {
            document.getElementById('btnUp').src="../images/down.jpg";
            document.getElementById('<%=pnlAdvanceSearch.ClientID%>').style.display ='block'
            document.getElementById('<%=hdAdvanceSearch.ClientID%>').value='1';
        }        
    }
    function OnloadAdvanceSearch()
    {            
       if(document.getElementById('<%=hdAdvanceSearch.ClientID%>').value=="1")
       {            
           document.getElementById('<%=pnlAdvanceSearch.ClientID%>').style.display ='block'            
       }
       else
       {
           document.getElementById('<%=pnlAdvanceSearch.ClientID%>').style.display ='none'
       }
     }
     function AgencyReset()
    {
        document.getElementById("txtAoffice").value="";       
        //return false;
    }
    function EditFunction(CheckBoxObj)
    {           
        window.location.href="MSUP_Agency.aspx?Action=U|"+CheckBoxObj;       
        return false;
    }
    function DeleteFunction(CheckBoxObj)
    {   
        if (confirm("Are you sure you want to delete?")==true)
        {          
            window.location.href="MSSR_Agency.aspx?Action=D|"+CheckBoxObj+"|"+document.getElementById("txtAoffice").value;       
            return false;
        }
    }
    function NewFunction()
    {   
        window.location.href="MSUP_Agency.aspx?Action=I";       
        return false;
    }   
    </script>--%>
<body>
    <form id="frmEmployeeLog" runat="server">
        <table width="860px" align="left" class="border_rightred">
            <tr>
                <td valign="top" style="width: 860px;">
                    <table width="100%" class="left">
                        <tr>
                            <td>
                                <%--<span class="menu">Setup -&gt;</span><span class="sub_menu"> Employee Details</span>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading center">
                                User Log</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 860px" valign="top">
                    <table cellpadding="2" width="100%">
                        <tr>
                            <td style="width: 429px" align="right" class="textbold">
                                Select Category
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="drpSelect" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                    TabIndex="1" Width="214px" AutoPostBack="True">
                                    <asp:ListItem Selected="True" Value="1">User</asp:ListItem>
                                    <asp:ListItem Value="2">Group</asp:ListItem>
                                    <asp:ListItem Value="3">IP</asp:ListItem>
                                    <asp:ListItem Value="4">Supervisory</asp:ListItem>
                                    <asp:ListItem Value="5">Helpdesk</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="center" class="textbold" colspan="6">
                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                           
                        </tr>
                        
                    </table>
                    </td>
            </tr>
            <tr>
                <td valign="top" style="padding-left: 4px; height: 304px;">
                    <asp:Panel ID="pnlEmployeeDetails" runat="server">
                        <table width="850" border="0" align="left" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="textbold" colspan="6" align="center">
                                    <asp:Label ID="lblEmployeeDetailsError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="redborder" colspan="2" valign="top">
                                  <span class="subheading">User Details </span>
                                  
                                  
                                    <asp:GridView ID="grdEmployeeDetails" runat="server" BorderWidth="1" BorderColor="#d4d0c8"
                                        AutoGenerateColumns="False" Width="99%" TabIndex="9"  HeaderStyle-ForeColor="white">
                                        <Columns>
                                            <asp:BoundField DataField="CHANGEDBY" HeaderText="User Name" HeaderStyle-HorizontalAlign="Left"
                                                 ItemStyle-Width="30%" HeaderStyle-Width="15%">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DATETIME" HeaderText="Date Time" HeaderStyle-HorizontalAlign="Left"
                                                 ItemStyle-Width="20%" HeaderStyle-Width="15%">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CHANGEDDATA" HeaderText="Change Data" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="50%" HeaderStyle-Width="40%">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                         
                                            <asp:BoundField DataField="ADDED" HeaderText="ADDED" HeaderStyle-HorizontalAlign="Left"
                                                 ItemStyle-Width="50%" HeaderStyle-Width="40%" Visible="false">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="REMOVED" HeaderText="REMOVED" HeaderStyle-HorizontalAlign="Left"
                                                 ItemStyle-Width="50%" HeaderStyle-Width="40%" Visible="false">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            
                                            
                                            
                                        </Columns>
                                        <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                        <RowStyle CssClass="ItemColor" HorizontalAlign="Left" />
                                        <AlternatingRowStyle CssClass="lightblue" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 10px;">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlGroup" runat="server">
                        <table width="850px" border="0" align="left" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="textbold" colspan="6" align="center">
                                    <asp:Label ID="lblGroupError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="redborder" colspan="2" valign="top">
                                    <span class="subheading">Group Details </span>
                                    <asp:GridView ID="grdEmployeeGroup" runat="server" BorderWidth="1" BorderColor="#d4d0c8"
                                        AutoGenerateColumns="False" Width="99%" TabIndex="9"  HeaderStyle-ForeColor="white">
                                        <Columns>
                                            <asp:BoundField DataField="CHANGEDBY" HeaderText="User Name" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="30%" HeaderStyle-Width="30%">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="DATETIME" HeaderText="Date Time" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="20%" HeaderStyle-Width="20%">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="CHANGEDDATA" HeaderText="Change Data" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="50%" HeaderStyle-Width="40%" Visible="false">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            
                                            
                                            <asp:BoundField DataField="ADDED" HeaderText="Add Group" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="25%" HeaderStyle-Width="25%">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="REMOVED" HeaderText="Remove Group" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="25%" HeaderStyle-Width="25%">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                        <RowStyle CssClass="ItemColor" HorizontalAlign="Left" />
                                        <AlternatingRowStyle CssClass="lightblue" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 10px;">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlIPAddress" runat="server">
                        <table width="850px" border="0" align="left" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="textbold" colspan="6" align="center">
                                    <asp:Label ID="lblIPError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="redborder" colspan="2" valign="top">
                                    <span class="subheading">IP Address</span>
                                    <asp:GridView ID="grdIPAddress" runat="server" BorderWidth="1" BorderColor="#d4d0c8"
                                        AutoGenerateColumns="False" Width="99%" TabIndex="9"  HeaderStyle-ForeColor="white">
                                        <Columns>
                                            <asp:BoundField DataField="CHANGEDBY" HeaderText="User Name" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="30%" HeaderStyle-Width="30%">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DATETIME" HeaderText="Date Time" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="20%" HeaderStyle-Width="20%">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CHANGEDDATA" HeaderText="Change Data" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="40%" HeaderStyle-Width="25%" Visible="false">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            
                                          <asp:BoundField DataField="ADDED" HeaderText="Add IP" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="25%" HeaderStyle-Width="25%">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="REMOVED" HeaderText="Remove IP" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="25%" HeaderStyle-Width="25%">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        
                                        </Columns>
                                        
                                        
                                        
                                        <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                        <RowStyle CssClass="ItemColor" HorizontalAlign="Left" />
                                        <AlternatingRowStyle CssClass="lightblue" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 10px;">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlSupervisor" runat="server">
                        <table width="850px" border="0" align="left" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="textbold" colspan="6" align="center">
                                    <asp:Label ID="lblSupervisorError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="redborder" colspan="2" valign="top">
                                    <span class="subheading">SuperVisor</span>
                                    <asp:GridView ID="grdSupervisor" runat="server" BorderWidth="1" BorderColor="#d4d0c8"
                                        AutoGenerateColumns="False" Width="99%" TabIndex="9"  HeaderStyle-ForeColor="white">
                                        <Columns>
                                            <asp:BoundField DataField="CHANGEDBY" HeaderText="User Name" HeaderStyle-HorizontalAlign="Left"
                                                SortExpression="CHANGEDBY" ItemStyle-Width="30%" HeaderStyle-Width="15%">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DATETIME" HeaderText="Date Time" HeaderStyle-HorizontalAlign="Left"
                                                SortExpression="LOGDATE" ItemStyle-Width="20%" HeaderStyle-Width="15%">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CHANGEDDATA" HeaderText="Change Data" HeaderStyle-HorizontalAlign="Left"
                                                SortExpression="CHANGEDDATA" ItemStyle-Width="50%" HeaderStyle-Width="40%" Visible="false">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            
                                             <asp:BoundField DataField="ADDED" HeaderText="Add Domain" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="25%" HeaderStyle-Width="25%" >
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="REMOVED" HeaderText="Remove Domain" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="25%" HeaderStyle-Width="25%">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            
                                        </Columns>
                                        <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                        <RowStyle CssClass="ItemColor" HorizontalAlign="Left" />
                                        <AlternatingRowStyle CssClass="lightblue" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 10px;">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlHelpDesk" runat="server">
                        <table width="850px" border="0" align="left" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="textbold" colspan="6" align="center">
                                    <asp:Label ID="lblHelpDeskError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="redborder" colspan="2" valign="top">
                                    <span class="subheading">HelpDesk</span>
                                    <asp:GridView ID="grdHelpDesk" runat="server" BorderWidth="1" BorderColor="#d4d0c8"
                                        AutoGenerateColumns="False" Width="99%" TabIndex="9"  HeaderStyle-ForeColor="white">
                                        <Columns>
                                            <asp:BoundField DataField="CHANGEDBY" HeaderText="User Name" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="30%" HeaderStyle-Width="30%">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DATETIME" HeaderText="Date Time" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="20%" HeaderStyle-Width="20%">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CHANGEDDATA" HeaderText="Change Data" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="40%" HeaderStyle-Width="50%" >
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            
                                               <asp:BoundField DataField="ADDED" HeaderText="Add IP" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="25%" HeaderStyle-Width="25%" Visible="false" >
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="REMOVED" HeaderText="Remove IP" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="25%" HeaderStyle-Width="25%" Visible="false" >
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                          
                                            
                                        </Columns>
                                        <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                        <RowStyle CssClass="ItemColor" HorizontalAlign="Left" />
                                        <AlternatingRowStyle CssClass="lightblue" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 10px;">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
      
        </table>
    </form>
</body>
</html>
