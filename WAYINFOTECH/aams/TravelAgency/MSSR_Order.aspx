<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_Order.aspx.vb" Inherits="TravelAgency_MSSR_Order"
    MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Agency</title>
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

<script type="text/javascript" language="javascript">
     function OrderWholeGroup()
 {

try
    {
            if(document.getElementById("txtAgencyName").value=='')
            {
                document.getElementById("chbWholeGroup").checked=false;
                document.getElementById("chbWholeGroup").disabled =true;
            }
            else
            {
                 if(document.getElementById("hdtxtAgencyName").value.trim()==document.getElementById("txtAgencyName").value.trim())
                {
                document.getElementById("chbWholeGroup").disabled =false;
                }
                else
                {
                
                document.getElementById("hdAgencyNameId").value='';
                document.getElementById("hdtxtAgencyName").value='';
                document.getElementById("chbWholeGroup").checked=false;
                document.getElementById("chbWholeGroup").disabled =true;
                }
            
            }
            
        }
      catch(err){}
      
	    
 }
</script>

<body>
    <form id="form1" runat="server" defaultfocus="txtAgencyName" defaultbutton="btnSearch">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <table width="860px" align="left" class="border_rightred">
                        <tr>
                            <td valign="top" style="width: 860px;">
                                <table width="100%" align="left">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">Travel Agency-></span><span class="sub_menu">Order</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="heading" align="center" valign="top">
                                            Search Order
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td align="LEFT">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 860px;" class="redborder" valign="top">
                                                                    <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td class="textbold" colspan="6" align="center" style="height: 15px">
                                                                                &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="6%" height="25px">
                                                                                &nbsp;</td>
                                                                            <td width="18%" class="textbold">
                                                                                Agency Name
                                                                                <input type="hidden" id="hdAgencyNameId" runat="server" value="" style="width: 5px" />
                                                                                <input type="hidden" id="hdAgencyName" runat="server" value="" style="width: 5px" />
                                                                            </td>
                                                                            <td colspan="3">
                                                                                <asp:TextBox ID="txtAgencyName" CssClass="textbox" MaxLength="40" runat="server"
                                                                                    Width="94%" TabIndex="1"></asp:TextBox>
                                                                                <img src="../Images/lookup.gif" alt="" onclick="javascript:return PopupAgencyPageOrderMain();"
                                                                                    style="cursor: pointer;" tabindex="1" /></td>
                                                                            <td style="width: 176px">
                                                                                <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" OnClientClick="return OrderSearchPage()"
                                                                                    TabIndex="2" AccessKey="A" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="6%" style="height: 25px">
                                                                            </td>
                                                                            <td class="textbold" width="18%" style="height: 25px">
                                                                                Whole Group</td>
                                                                            <td width="20%" style="height: 25px">
                                                                                <asp:CheckBox ID="chbWholeGroup" runat="server" CssClass="textbold" TabIndex="1" /></td>
                                                                            <td width="18%" class="textbold" style="height: 25px">
                                                                                Country</td>
                                                                            <td width="20%" style="height: 25px">
                                                                                <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlCountry" runat="server" CssClass="dropdownlist"
                                                                                    Width="144px" TabIndex="1">
                                                                                </asp:DropDownList></td>
                                                                            <td style="height: 25px; width: 176px;">
                                                                                <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="2"
                                                                                    AccessKey="N" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="25px">
                                                                                &nbsp;</td>
                                                                            <td class="textbold">
                                                                                City</td>
                                                                            <td>
                                                                                <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlCity" runat="server" CssClass="dropdownlist"
                                                                                    Width="137px" TabIndex="1">
                                                                                </asp:DropDownList></td>
                                                                            <td class="textbold">
                                                                                Region</td>
                                                                            <td>
                                                                                <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlRegion" runat="server" CssClass="dropdownlist"
                                                                                    Width="144px" TabIndex="1">
                                                                                </asp:DropDownList></td>
                                                                            <td style="width: 176px">
                                                                                <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="2" Text="Export"
                                                                                    AccessKey="E" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="25px">
                                                                                &nbsp;</td>
                                                                            <td class="textbold">
                                                                                Order Type</td>
                                                                            <td colspan="3">
                                                                                <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlOrderType" runat="server" CssClass="dropdownlist"
                                                                                    Width="95%" TabIndex="1">
                                                                                </asp:DropDownList></td>
                                                                            <td style="width: 176px">
                                                                                <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="2"
                                                                                    AccessKey="R" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 25px">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Order Number</td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtOrderNumber" runat="server" CssClass="textbox" MaxLength="20"
                                                                                    Wrap="False" TabIndex="1"></asp:TextBox></td>
                                                                            <td class="textbold">
                                                                                Order Status</td>
                                                                            <td>
                                                                                <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlOrderStatus" runat="server" CssClass="dropdownlist"
                                                                                    Width="144px" TabIndex="1">
                                                                                </asp:DropDownList></td>
                                                                            <td style="width: 176px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 25px">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Company Vertical</td>
                                                                            <td>
                                                                                <asp:DropDownList ID="DlstCompVertical" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                                    TabIndex="1" Width="137px">
                                                                                </asp:DropDownList></td>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td style="width: 176px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;</td>
                                                                            <td colspan="4" class="subheading">
                                                                                <img src="../Images/down.jpg" tabindex="1" id="btnUp" onclick="Javascript:return AdvanceSearchOrderMain();" />&nbsp;&nbsp;Advance
                                                                                Search<asp:HiddenField ID="hdAdvanceSearch" runat="server" Value="" />
                                                                            </td>
                                                                            <td style="width: 176px">
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6">
                                                                                <asp:Panel ID="pnlAdvanceSearch" Style="display: none" runat="server" Width="100%">
                                                                                    <table style="width: 100%" cellpadding="0" cellspacing="0" border="0">
                                                                                        <tr>
                                                                                            <td width="6%" style="height: 25px">
                                                                                            </td>
                                                                                            <td width="18%" class="textbold">
                                                                                                Processed From</td>
                                                                                            <td width="20%">
                                                                                                <asp:TextBox ID="txtProcessedFrom" CssClass="textbox" MaxLength="10" runat="server"
                                                                                                    TabIndex="1"></asp:TextBox>
                                                                                                <img id="imgProcessedFrom" alt="" src="../Images/calender.gif" tabindex="1" title="Date selector"
                                                                                                    style="cursor: pointer" />

                                                                                                <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtProcessedFrom.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgProcessedFrom",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                </script>

                                                                                            </td>
                                                                                            <td width="18%">
                                                                                                <span class="textbold">Processed To</span></td>
                                                                                            <td width="20%">
                                                                                                <asp:TextBox ID="txtProcessedTo" runat="server" CssClass="textbox" MaxLength="10"
                                                                                                    TabIndex="1"></asp:TextBox>
                                                                                                <img id="imgProcessedTo" alt="" src="../Images/calender.gif" tabindex="1" title="Date selector"
                                                                                                    style="cursor: pointer" />

                                                                                                <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtProcessedTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgProcessedTo",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                </script>

                                                                                            </td>
                                                                                            <td width="18%">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="height: 25px">
                                                                                            </td>
                                                                                            <td class="textbold">
                                                                                                Received From</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtReceivedFrom" runat="server" CssClass="textbox" MaxLength="10"
                                                                                                    TabIndex="1"></asp:TextBox>
                                                                                                <img id="imgReceivedFrom" alt="" src="../Images/calender.gif" tabindex="1" title="Date selector"
                                                                                                    style="cursor: pointer" />

                                                                                                <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtReceivedFrom.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgReceivedFrom",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                </script>

                                                                                            </td>
                                                                                            <td class="textbold">
                                                                                                Received To</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtReceivedTo" runat="server" CssClass="textbox" MaxLength="10"
                                                                                                    TabIndex="1"></asp:TextBox>
                                                                                                <img id="imgReceivedTo" alt="" src="../Images/calender.gif" tabindex="1" title="Date selector"
                                                                                                    style="cursor: pointer" />

                                                                                                <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtReceivedTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgReceivedTo",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                </script>

                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="height: 25px">
                                                                                            </td>
                                                                                            <td class="textbold" style="height: 25px">
                                                                                                Message Sent From</td>
                                                                                            <td style="height: 25px">
                                                                                                <asp:TextBox ID="txtMessageFrom" runat="server" CssClass="textbox" MaxLength="10"
                                                                                                    TabIndex="1"></asp:TextBox>
                                                                                                <img id="imgMessageFrom" alt="" src="../Images/calender.gif" tabindex="1" title="Date selector"
                                                                                                    style="cursor: pointer" />

                                                                                                <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtMessageFrom.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgMessageFrom",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                </script>

                                                                                            </td>
                                                                                            <td class="textbold" style="height: 25px">
                                                                                                Message Sent To</td>
                                                                                            <td style="height: 25px">
                                                                                                <asp:TextBox ID="txtMessageTo" runat="server" CssClass="textbox" MaxLength="10" TabIndex="1"></asp:TextBox>
                                                                                                <img id="imgMessageTo" alt="" src="../Images/calender.gif" tabindex="1" title="Date selector"
                                                                                                    style="cursor: pointer" />

                                                                                                <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtMessageTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgMessageTo",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                </script>

                                                                                            </td>
                                                                                            <td style="height: 25px">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="height: 25px">
                                                                                            </td>
                                                                                            <td class="textbold">
                                                                                                Approval From</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtApprovalFrom" runat="server" CssClass="textbox" MaxLength="10"
                                                                                                    TabIndex="1"></asp:TextBox>
                                                                                                <img id="imgApprovalFrom" alt="" src="../Images/calender.gif" tabindex="1" title="Date selector"
                                                                                                    style="cursor: pointer" />

                                                                                                <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtApprovalFrom.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgApprovalFrom",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                </script>

                                                                                            </td>
                                                                                            <td class="textbold">
                                                                                                Approval To</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtApprovalTo" runat="server" CssClass="textbox" MaxLength="10"
                                                                                                    TabIndex="1"></asp:TextBox>
                                                                                                <img id="imgApprovalTo" alt="" src="../Images/calender.gif" tabindex="1" title="Date selector"
                                                                                                    style="cursor: pointer" />

                                                                                                <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtApprovalTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgApprovalTo",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                </script>

                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="height: 25px">
                                                                                            </td>
                                                                                            <td class="textbold" style="height: 25px">
                                                                                                Sent Back From</td>
                                                                                            <td style="height: 25px">
                                                                                                <asp:TextBox ID="txtSentFrom" runat="server" CssClass="textbox" MaxLength="10" TabIndex="1"></asp:TextBox>
                                                                                                <img id="imgSentFrom" alt="" src="../Images/calender.gif" tabindex="1" title="Date selector"
                                                                                                    style="cursor: pointer" />

                                                                                                <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtSentFrom.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgSentFrom",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                </script>

                                                                                            </td>
                                                                                            <td class="textbold" style="height: 25px">
                                                                                                Sent Back To</td>
                                                                                            <td style="height: 25px">
                                                                                                <asp:TextBox ID="txtSentTo" runat="server" CssClass="textbox" MaxLength="10" TabIndex="1"></asp:TextBox>
                                                                                                <img id="imgSentTo" alt="" src="../Images/calender.gif" tabindex="1" title="Date selector"
                                                                                                    style="cursor: pointer" />

                                                                                                <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtSentTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgSentTo",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                </script>

                                                                                            </td>
                                                                                            <td style="height: 25px">
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></asp:Panel>
                                                                                <asp:HiddenField ID="hdOrderID" runat="server" />
                                                                                <asp:HiddenField ID="hdtxtAgencyName" runat="server" />
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
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td valign="top" style="padding-left: 4px;" colspan="2">
                    <table width="1600px" border="0" align="left" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="redborder">
                                <asp:GridView ID="gvOrder" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                    Width="1600px" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                    AlternatingRowStyle-CssClass="lightblue" EnableViewState="False" AllowSorting="True"
                                    TabIndex="3">
                                    <Columns>
                                        <asp:BoundField DataField="AGENCYNAME" HeaderText="Agency Name" SortExpression="AGENCYNAME">
                                            <ItemStyle Width="250px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ADDRESS" HeaderText="Address" SortExpression="ADDRESS">
                                            <ItemStyle Width="290px" Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Region" HeaderText="Region" SortExpression="Region">
                                            <ItemStyle Width="110px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="COMP_VERTICAL" HeaderText="Company Vertical" SortExpression="COMP_VERTICAL">
                                            <ItemStyle Width="50px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CITY" HeaderText="City" SortExpression="CITY">
                                            <ItemStyle Width="70px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="COUNTRY" HeaderText="Country" SortExpression="COUNTRY">
                                            <ItemStyle Width="70px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ORDER_NUMBER" HeaderText="Order Number" SortExpression="ORDER_NUMBER">
                                            <ItemStyle Width="70px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ORDER_TYPE_NAME" HeaderText="Order Type" SortExpression="ORDER_TYPE_NAME">
                                            <ItemStyle Width="190px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ORDER_STATUS_NAME" HeaderText="Order Status" SortExpression="ORDER_STATUS_NAME">
                                            <ItemStyle Width="70px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="APPROVAL_DATE" HeaderText="Approval Date" SortExpression="APPROVAL_DATE">
                                            <ItemStyle Width="90px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RECEIVED_DATE" HeaderText="Received" SortExpression="RECEIVED_DATE">
                                            <ItemStyle Width="90px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ODDICEID" HeaderText="Office ID" SortExpression="ODDICEID">
                                            <ItemStyle Width="70px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MSG_SEND_DATE" HeaderText="Message Send Date" SortExpression="MSG_SEND_DATE">
                                            <ItemStyle Width="100px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PENDINGWITHNAME" HeaderText="Pending With Employees" SortExpression="PENDINGWITHNAME">
                                            <ItemStyle Width="100px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="INSTALLATION_DUE_DATE" HeaderText="Installation Due Date"
                                            SortExpression="INSTALLATION_DUE_DATE">
                                            <ItemStyle Width="90px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RECEIVED_DATE_MKT" HeaderText="Receiving" SortExpression="RECEIVED_DATE_MKT">
                                            <ItemStyle Width="90px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="REMARKS" HeaderText="Remarks" SortExpression="REMARKS">
                                            <ItemStyle Width="190px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a> <a href="#"
                                                    class="LinkButtons" id="linkDelete" runat="server">Delete</a>
                                                <asp:HiddenField ID="hdORDERID" runat="server" Value='<%#Eval("ORDERID")%>' />
                                                <asp:HiddenField ID="hdCITY" runat="server" Value='<%#Eval("City")%>' />
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle CssClass="lightblue" />
                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" ForeColor="White" />
                                    <RowStyle CssClass="textbold" />
                                </asp:GridView>
                                <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="55%">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr class="paddingtop paddingbottom">
                                            <td style="width: 30%" class="left">
                                                <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                    ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey"></asp:TextBox></td>
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
                </td>
            </tr>
        </table>

        <script>
        document.getElementById("chbWholeGroup").disabled=true;
        </script>

    </form>
</body>
</html>
