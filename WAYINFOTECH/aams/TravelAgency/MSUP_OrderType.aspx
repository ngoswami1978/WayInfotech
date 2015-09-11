<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_OrderType.aspx.vb"
    Inherits="Setup_MSUP_OrderType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Order Type</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">  
    
 
 
    function OrderTypePage()
{

if(document.getElementById('<%=ddlOrderTypeCat.ClientID%>').value=="")
{
            document.getElementById("lblError").innerText="Order Type Category is Mandatory";
		    document.getElementById('ddlOrderTypeCat').focus();
		    return false;
}
if(document.getElementById('<%=txtOrderType.ClientID%>').value=="") 
{
document.getElementById("lblError").innerText="Order Type is Mandatory";
		    document.getElementById('txtOrderType').focus();
		    return false;
}
 if(IsDataValid(document.getElementById("txtDaysReq").value,3)==false)
{
  document.getElementById("lblError").innerText="Number of Days is Numeric";
  return false;
}

 if(document.getElementById('<%=ddlOrderTypeChallan.ClientID%>').value=="None")
{
            document.getElementById("lblError").innerText="Please Select Order Type for Challan";
		    document.getElementById('ddlOrderTypeChallan').focus();
		    return false;
}



if(document.getElementById('<%=ddlOrderTypeTraning.ClientID%>').value=="None")
{
            document.getElementById("lblError").innerText="Please Select Order Type for Traning";
		    document.getElementById('ddlOrderTypeTraning').focus();
		    return false;
}


if(document.getElementById('<%=ddlOrderTypeHardware.ClientID%>').value=="None")
{
            document.getElementById("lblError").innerText="Please Select Order Type for Hardware";
		    document.getElementById('ddlOrderTypeHardware').focus();
		    return false;
}


if(document.getElementById('<%=ddlOrderTypeISP.ClientID%>').value=="None")
{
            document.getElementById("lblError").innerText="Please Select Order Type for ISP";
		    document.getElementById('ddlOrderTypeISP').focus();
		    return false;
}





return true;         
}




    </script>

</head>
<body style="font-size: 12pt; font-family: Times New Roman">
    <form id="frmOrderType" runat="server" defaultfocus="ddlOrderTypeCat">
        <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top" style="height: 519px">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">TravelAgency-&gt;</span><span class="sub_menu">Order Type</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" style="height: 10px">
                                Manage Order Type
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                            <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                <tr>
                                                    <td colspan="4" class="center gap">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 10%">
                                                    </td>
                                                    <td class="textbold" colspan="2" valign="top" align="center">
                                                        <asp:RadioButtonList ID="RdlNewCancel" runat="server" BorderWidth="0px" CellPadding="0"
                                                            CellSpacing="0" CssClass="textbold" RepeatDirection="Horizontal" Width="50%"
                                                            TabIndex="2">
                                                            <asp:ListItem Selected="True" Value="0">New Order</asp:ListItem>
                                                            <asp:ListItem Value="1">Cancellation</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 10%">
                                                    </td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 10%">
                                                    </td>
                                                    <td class="textbold">
                                                        Order Type Category <span class="Mandatory">*</span></td>
                                                    <td>
                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlOrderTypeCat" runat="server" CssClass="dropdownlist"
                                                            Width="100%" TabIndex="2">
                                                        </asp:DropDownList></td>
                                                    <td>
                                                        <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" OnClientClick="return OrderTypePage()"
                                                            TabIndex="16" AccessKey="S" /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold">
                                                        Order Type <span class="Mandatory">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtOrderType" CssClass="textbox" runat="server" MaxLength="40" Width="98%"
                                                            TabIndex="3"></asp:TextBox></td>
                                                    <td>
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="17"
                                                            AccessKey="N" /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold">
                                                        Descriptions
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDescription" CssClass="textbox" runat="server" MaxLength="300"
                                                            Width="98%" Height="50px" Rows="5" TextMode="MultiLine" TabIndex="4"></asp:TextBox></td>
                                                    <td class="top">
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="18"
                                                            AccessKey="R" /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold">
                                                        No.of Days Required
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDaysReq" CssClass="textbox" runat="server" Width="98%" onkeyup="checknumeric(this.id)"
                                                            MaxLength="4" TabIndex="5"></asp:TextBox></td>
                                                    <td class="top">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold">
                                                        <asp:CheckBox ID="chkOrderTrackingRequired" runat="server" Text="Order Tracking Required "
                                                            CssClass="textbold" TabIndex="6" />
                                                    </td>
                                                    <td style="width: 50%">
                                                        <asp:CheckBox ID="chkOrderEnabled" runat="server" Text="Disabled" CssClass="textbold"
                                                            TabIndex="7" />
                                                        <asp:CheckBox ID="chkIspOrdr" runat="server" Text="ISP Order " CssClass="textbold"
                                                            Visible="false" TabIndex="2" />
                                                    </td>
                                                    <td class="top">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold">
                                                        &nbsp;PC TYPE</td>
                                                    <td>
                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlPcType" runat="server" CssClass="dropdownlist"
                                                            Width="25%" TabIndex="8">
                                                            <asp:ListItem Value="Amadeus">Amadeus</asp:ListItem>
                                                            <asp:ListItem Value="Agency">Agency</asp:ListItem>
                                                            <asp:ListItem Value="None" Selected="True">None</asp:ListItem>
                                                        </asp:DropDownList></td>
                                                    <td class="top">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="subheading" colspan="2">
                                                        Include Order Type For</td>
                                                    <td class="top">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold" colspan="2">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 35%">
                                                                    Challan <span class="Mandatory">*</span>
                                                                </td>
                                                                <td style="width: 17%">
                                                                    <asp:DropDownList ID="ddlOrderTypeChallan" runat="server" CssClass="dropdownlist"
                                                                        Width="100%" TabIndex="9">
                                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                                        <asp:ListItem Value="None" Selected="True">--Select One--</asp:ListItem>
                                                                    </asp:DropDownList></td>
                                                                <td style="width: 1%">
                                                                    &nbsp;</td>
                                                                <td style="width: 30%">
                                                                    Training <span class="Mandatory">*</span></td>
                                                                <td style="width: 17%">
                                                                    <asp:DropDownList ID="ddlOrderTypeTraning" runat="server" CssClass="dropdownlist"
                                                                        Width="100%" TabIndex="10">
                                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                                        <asp:ListItem Value="None" Selected="True">--Select One--</asp:ListItem>
                                                                    </asp:DropDownList></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 35%">
                                                                    Hardware <span class="Mandatory">*</span></td>
                                                                <td style="width: 17%">
                                                                    <asp:DropDownList ID="ddlOrderTypeHardware" runat="server" CssClass="dropdownlist"
                                                                        Width="100%" TabIndex="11">
                                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                                        <asp:ListItem Value="None" Selected="True">--Select One--</asp:ListItem>
                                                                    </asp:DropDownList></td>
                                                                <td style="width: 1%">
                                                                    &nbsp;</td>
                                                                <td style="width: 30%">
                                                                    ISP <span class="Mandatory">*</span>&nbsp;</td>
                                                                <td style="width: 17%">
                                                                    <asp:DropDownList ID="ddlOrderTypeISP" runat="server" CssClass="dropdownlist" Width="100%"
                                                                        TabIndex="12">
                                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                                        <asp:ListItem Value="None" Selected="True">--Select One--</asp:ListItem>
                                                                    </asp:DropDownList></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td class="top">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="subheading" colspan="2" style="height: 10pt">
                                                        Connectivity</td>
                                                    <td class="top">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 23px">
                                                    </td>
                                                    <td class="textbold" style="height: 23px">
                                                        Old</td>
                                                    <td style="height: 23px">
                                                        <%-- <asp:DropDownList onkeyup="gotop(this.id)" ID="drplstOldConn" runat="server" CssClass="dropdownlist" Width="224px" TabIndex="2"  ></asp:DropDownList> --%>
                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drplstOldConn" runat="server" CssClass="dropdownlist"
                                                            Width="100%" TabIndex="13">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="top" style="height: 23px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 23px">
                                                    </td>
                                                    <td class="textbold" style="height: 23px">
                                                        New</td>
                                                    <td style="height: 23px">
                                                        <%--<asp:DropDownList onkeyup="gotop(this.id)" ID="drpLstNewConn" runat="server" Width="224px" CssClass="dropdownlist" TabIndex="2"></asp:DropDownList></td>--%>
                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpLstNewConn" runat="server" Width="100%"
                                                            CssClass="dropdownlist" TabIndex="14">
                                                        </asp:DropDownList></td>
                                                    <td class="top" style="height: 23px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="subheading" colspan="2">
                                                        Corporate Code Qualifier</td>
                                                    <td class="top">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold">
                                                        Corporate Codes</td>
                                                    <td>
                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlCorporateCode" runat="server" CssClass="dropdownlist"
                                                            Width="25%" TabIndex="15">
                                                        </asp:DropDownList></td>
                                                    <td>
                                                        <asp:Button ID="btnAdd" CssClass="button" runat="server" Text="Add" TabIndex="19" /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="1">
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:GridView ID="gvCorporateCode" runat="server" AutoGenerateColumns="False" TabIndex="4"
                                                            Width="100%" CellPadding="0">
                                                            <Columns>
                                                                <asp:BoundField DataField="Code" HeaderText="Code" />
                                                                <asp:BoundField DataField="Qualifier" HeaderText="Qualifier" />
                                                                <asp:BoundField DataField="Description" HeaderText="Description" />
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblAction" runat="server" Text="Action"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName="DeleteX"
                                                                            CommandArgument='<%#Eval("RowID")%>' CssClass="LinkButtons">
                                                                            Delete
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="20%" CssClass="ItemColor" />
                                                                    <HeaderStyle Wrap="False" CssClass="Gridheading" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <RowStyle CssClass="textbold" />
                                                            <HeaderStyle CssClass="Gridheading" />
                                                        </asp:GridView>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="ErrorMsg" colspan="2">
                                                        Field Marked * are Mandatory</td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td colspan="3">
                                                        &nbsp;</td>
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
