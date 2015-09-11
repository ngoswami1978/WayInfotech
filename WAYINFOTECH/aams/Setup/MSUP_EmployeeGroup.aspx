<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_EmployeeGroup.aspx.vb"
    Inherits="Setup_MSUP_EmployeeGroup" ValidateRequest="false" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Employee</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
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
/*function GroupGrantAll()
{
    for(intcnt=1;intcnt<=document.getElementById('<%=gvEmployeeGroup.ClientID%>').rows.length-1;intcnt++)
    {        
        document.getElementById('<%=gvEmployeeGroup.ClientID%>').rows[intcnt].cells[3].children[0].checked=true;   
    }
    return false;
}
function GroupRevokeAll()
{
    for(intcnt=1;intcnt<=document.getElementById('<%=gvEmployeeGroup.ClientID%>').rows.length-1;intcnt++)
    {        
        document.getElementById('<%=gvEmployeeGroup.ClientID%>').rows[intcnt].cells[3].children[0].checked=false;       
    }
    return false;
}
*/
function PageLoadMethod()
{
   //CheckRestrict();
}
function AGroupMandatory()
    {
        if (  document.getElementById("txtGroupChainCode").value!="")
         {
           if(IsDataValid(document.getElementById("txtGroupChainCode").value,3)==false)
            {
            document.getElementById("lblError").innerHTML="Chain code is not valid.";
            document.getElementById("txtGroupChainCode").focus();
            return false;
            } 
         }         
         return true;
     }
</script>

<body>
    <form id="form1" runat="server" defaultbutton="btnGroupSearch" defaultfocus="txtGroupName">
        <table width="860px" align="left" style="height: 486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">Setup-></span><span class="sub_menu">Employee Agency Group</span>
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
                                                        <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server" EnableViewState="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="100%" valign="top">
                                                        <asp:Panel ID="pnlGroup" runat="server" Width="100%">
                                                            <table width="100%" border="0" align="left" cellpadding="2" cellspacing="0">
                                                                <tr>
                                                                    <td colspan="6" height="9">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                        &nbsp;</td>
                                                                    <td class="textbold">
                                                                        Group Name</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtGroupName" CssClass="textbox" runat="server" TabIndex="1"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        Chain Code
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtGroupChainCode" CssClass="textbox" runat="server" TabIndex="2"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:Button ID="btnGroupSearch" runat="server" CssClass="button" Text="Search" TabIndex="7"
                                                                            AccessKey="A" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 5%">
                                                                        &nbsp;</td>
                                                                    <td class="textbold" width="17%">
                                                                        Agency Type</td>
                                                                    <td width="20%">
                                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpGroupAgencyType" runat="server"
                                                                            Width="137px" CssClass="textbold" TabIndex="3">
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold" width="22%">
                                                                        City
                                                                    </td>
                                                                    <td width="20%">
                                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpGroupCity" runat="server" Width="137px"
                                                                            CssClass="textbold" TabIndex="4">
                                                                        </asp:DropDownList></td>
                                                                    <td width="23%">
                                                                        <asp:Button ID="btnGroupSave" runat="server" CssClass="button" Text="Save" TabIndex="8"
                                                                            AccessKey="S" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                        &nbsp;</td>
                                                                    <td class="textbold">
                                                                        1a office</td>
                                                                    <td>
                                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpGroupAoffice" runat="server" Width="137px"
                                                                            CssClass="textbold" TabIndex="5">
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold">
                                                                        Security Region</td>
                                                                    <td>
                                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpRegionList" runat="server" TabIndex="1"
                                                                            Width="137px" CssClass="textbold">
                                                                        </asp:DropDownList></td>
                                                                    <td>
                                                                        <asp:Button ID="btnGroupReset" runat="server" CssClass="button" Text="Reset" TabIndex="9"
                                                                            AccessKey="R" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 26px">
                                                                    </td>
                                                                    
                                                                    <td class="textbold" style="height: 26px">
                                                                        Selected Only
                                                                    </td>
                                                                    <td style="height: 26px">
                                                                        <asp:CheckBox ID="chkSelected" runat="server" TabIndex="6" />
                                                                     </td>
                                                                     
                                                                    <td class="textbold" style="height: 26px">
                                                                        Main Group</td>
                                                                    <td width="20%" style="height: 26px">
                                                                        <asp:CheckBox ID="chkMainGroup" runat="server" TabIndex="6" /></td>
                                                                    
                                                                    <td style="height: 26px">
                                                                        <asp:Button ID="btnGrGrantAll" runat="server" CssClass="button" Text="Grant All"
                                                                            TabIndex="10" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                        &nbsp;</td>
                                                                    <td colspan="2" class="ErrorMsg">
                                                                        <input id="hdInputXml" runat="server" style="width: 4px" type="hidden" />
                                                                        <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        <asp:Button ID="btnGrRevokeAll" runat="server" CssClass="button" Text="Revoke All"
                                                                            TabIndex="11" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6">
                                                                        <asp:GridView ID="gvEmployeeGroup" TabIndex="6" runat="server" Width="98%" AutoGenerateColumns="False"
                                                                            AllowSorting="True">
                                                                            <Columns>
                                                                                <asp:BoundField HeaderText="Chain Code" DataField="Chain_Code" SortExpression="Chain_Code">
                                                                                </asp:BoundField>
                                                                                <asp:BoundField HeaderText="Aoffice" DataField="Aoffice" SortExpression="Aoffice"></asp:BoundField>
                                                                                <asp:BoundField HeaderText="Group Name" DataField="Chain_Name" SortExpression="Chain_Name">
                                                                                </asp:BoundField>
                                                                                <asp:TemplateField HeaderText="Select">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkChainCode" runat="server" Checked='<%#DataBinder.Eval(Container.DataItem, "ChainSelected")%>' />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="center" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                            <RowStyle CssClass="textbold" />
                                                                            <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6">
                                                                        <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                <tr class="paddingtop paddingbottom">
                                                                                    <td style="width: 30%" class="left">
                                                                                        <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                                            ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey"
                                                                                            ReadOnly="true"></asp:TextBox></td>
                                                                                    <td style="width: 25%" class="right">
                                                                                        <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                                    <td style="width: 20%" class="center">
                                                                                        <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber"
                                                                                            Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                                                        </asp:DropDownList></td>
                                                                                    <td style="width: 25%" class="left">
                                                                                        <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
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
