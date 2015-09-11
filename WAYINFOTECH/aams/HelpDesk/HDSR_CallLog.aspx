<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDSR_CallLog.aspx.vb" Inherits="HelpDesk_HDSR_CallLog"
    ValidateRequest="false" EnableEventValidation="false" MaintainScrollPositionOnPostback ="true"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::HelpDesk::Search Call Log</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"      title="win2k-cold-1" />
    <script type="text/javascript" src="../Calender/calendar.js"></script>
    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>
    </head>
<body>
    <form id="form1" runat="server" defaultfocus="txtAgencyName" defaultbutton="btnSearch">
        <div>
            <table>
                <tr>
                    <td>
                        <table width="860px" class="border_rightred left">
                            <tr>
                                <td class="top">
                                    <table width="100%" class="left">
                                        <tr>
                                            <td>
                                                <span class="menu">HelpDesk -&gt;</span><span class="sub_menu">Call Log Search</span></td>
                                        </tr>
                                        <tr>
                                            <td class="heading center">Search Call Log</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td class="redborder center">
                                                            <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                <tr>
                                                                    <td class="center" colspan="6">
                                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><input id="hdAgencyName" runat="server" style="width: 1px" type="hidden" /></td>
                                                                    <td class="textbold">Agency Name</td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textbox TextTitleCase" MaxLength="50"
                                                                            TabIndex="2" Width="528px"></asp:TextBox>
                                                                        <img id="Img2" runat="server" alt="Select & Add Agency Name" onclick="PopupPageCallLog(2)"
                                                                            src="../Images/lookup.gif" style="cursor: pointer;" /></td>
                                                                    <td style="width: 12%;" class="center">
                                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3"
                                                                            AccessKey="a" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 3%"></td>
                                                                    <td class="textbold" style="width: 15%">OfficeID</td>
                                                                    <td style="width: 27%"><asp:TextBox ID="txtOfficeID" runat="server" CssClass="textbox" MaxLength="25" TabIndex="2"
                                                                            Width="170px"></asp:TextBox></td>
                                                                    <td class="textbold" style="width: 15%">
                                                                        Customer Category</td>
                                                                    <td style="width: 26%">
                                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlCustomerCategory" runat="server"
                                                                            CssClass="dropdownlist" Width="176px" TabIndex="2">
                                                                        </asp:DropDownList></td>
                                                                    <td class="center" style="width: 13%">
                                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="3"
                                                                            AccessKey="n" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <input id="hdLoggedBy" runat="server" style="width: 1px" type="hidden" /></td>
                                                                    <td class="textbold">Logged By</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtLoggedBy" runat="server" CssClass="textbox" MaxLength="50" TabIndex="2"
                                                                            Width="170px"></asp:TextBox>
                                                                        <img id="img1" runat="server" alt="Select & Add Logged By" onclick="PopupPageCallLog(1)"
                                                                            src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                                                    <td class="textbold">Caller Name</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCallerName" runat="server" CssClass="textbox TextTitleCase" MaxLength="50"
                                                                            TabIndex="2" Width="170px" onkeypress="allTextWithSpace();"></asp:TextBox>
                                                                        <img id="Img3" runat="server" alt="Select & Add Caller Name" onclick="PopupPageCallLog(3);"
                                                                            src="../Images/lookup.gif" style="cursor: pointer" /></td>
                                                                    <td class="center"><asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3"
                                                                            AccessKey="r" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td class="textbold">Query Group</td>
                                                                    <td><asp:DropDownList onkeyup="gotop(this.id)" ID="ddlQueryGroup" runat="server" CssClass="dropdownlist"
                                                                            Width="176px" AutoPostBack="True" TabIndex="2">
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold">Query Sub Group</td>
                                                                    <td><asp:DropDownList onkeyup="gotop(this.id)" ID="ddlQuerySubGroup" runat="server" CssClass="dropdownlist"
                                                                            Width="176px" AutoPostBack="True" TabIndex="2">
                                                                            <asp:ListItem Selected="True" Value="">All</asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                                    <td class="center"><asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="3" Text="Export"
                                                                            AccessKey="e" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><input id="hdCallCallerName" runat="server" style="width: 1px" type="hidden" /></td>
                                                                    <td class="textbold">Query Category</td>
                                                                    <td><asp:DropDownList ID="ddlQueryCategory" runat="server" CssClass="dropdownlist" Width="176px"
                                                                            AutoPostBack="True" TabIndex="2" onkeyup="gotop(this.id)">
                                                                            <asp:ListItem Selected="True" Value="">All</asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold">Query Sub Category</td>
                                                                    <td><asp:DropDownList onkeyup="gotop(this.id)" ID="ddlQuerySubCategory" runat="server"
                                                                            CssClass="dropdownlist" Width="176px" TabIndex="2">
                                                                            <asp:ListItem Selected="True" Value="">All</asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td class="textbold">Query Status</td>
                                                                    <td><asp:DropDownList onkeyup="gotop(this.id)" ID="ddlQueryStatus" runat="server" CssClass="dropdownlist"
                                                                            Width="176px" TabIndex="2">
                                                                            <asp:ListItem Selected="True" Value="">All</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="textbold">Query Priority</td>
                                                                    <td><asp:DropDownList onkeyup="gotop(this.id)" ID="ddlQueryPriority" runat="server" CssClass="dropdownlist"
                                                                            Width="176px" TabIndex="2">
                                                                            <asp:ListItem Selected="True" Value="">All</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td class="textbold">Coordinator1</td>
                                                                    <td><asp:DropDownList onkeyup="gotop(this.id)" ID="ddlCoordinator1" runat="server" CssClass="dropdownlist"
                                                                            Width="176px" TabIndex="2">
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold">Coordinator2</td>
                                                                    <td><asp:DropDownList onkeyup="gotop(this.id)" ID="ddlCoordinator2" runat="server" CssClass="dropdownlist"
                                                                            Width="176px" TabIndex="2">
                                                                        </asp:DropDownList></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td class="textbold">Date Assigned</td>
                                                                    <td><asp:TextBox ID="txtDateAssigned" runat="server" CssClass="textbox" Width="170px"
                                                                            TabIndex="2" ></asp:TextBox>
                                                                        <img id="imgDateAssigned" alt="" tabindex="2" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateAssigned.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateAssigned",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                                                                                                                      });
                                                                        </script>

                                                                    </td>
                                                                    <td class="textbold">Disposition</td>
                                                                    <td><asp:DropDownList onkeyup="gotop(this.id)" ID="ddlDisposition" runat="server" CssClass="dropdownlist"
                                                                            Width="176px" TabIndex="2">
                                                                        </asp:DropDownList></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td class="textbold">
                                                                        Opened Date From
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtQueryOpenedDateFrom" runat="server" CssClass="textbox" Width="170px"
                                                                             TabIndex="2"></asp:TextBox>
                                                                        <img id="imgOpenedDateFrom" alt="" tabindex="2" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtQueryOpenedDateFrom.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y %H:%M",
                                                  button         :    "imgOpenedDateFrom",
                                                 //align          :    "Tl",
                                                  singleClick    :    true,
                                                  showsTime      : true
                                                               });
                                                                        </script>

                                                                    </td>
                                                                    <td class="textbold">
                                                                        Opened Date To
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtQueryOpenedDateTo" runat="server" CssClass="textbox" Width="170px"
                                                                            TabIndex="2"></asp:TextBox>
                                                                        <img id="imgOpenedDateTo" alt="" tabindex="2" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtQueryOpenedDateTo.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y %H:%M",
                                                  button         :    "imgOpenedDateTo",
                                                 //align          :    "Tl",
                                                  singleClick    :    true,
                                                  showsTime      : true
                                                               });
                                                                        </script>

                                                                    </td>
                                                                    <td>
                                                                        <input id="hdQueryCategory" runat="server" style="width: 1px" type="hidden" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td class="textbold">Close Date From</td>
                                                                    <td><asp:TextBox ID="txtCloseDateFrom1" runat="server" CssClass="textbox" Width="170px"
                                                                             TabIndex="2"></asp:TextBox>
                                                                        <img id="imgCloseDateFrom" alt="" tabindex="2" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtCloseDateFrom1.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y %H:%M",
                                                  button         :    "imgCloseDateFrom",
                                                 //align          :    "Tl",
                                                  singleClick    :    true,
                                                  showsTime      : true
                                                               });
                                                                        </script>

                                                                    </td>
                                                                    <td class="textbold">Close Date To</td>
                                                                    <td><asp:TextBox ID="txtCloseDateTo1" runat="server" CssClass="textbox" Width="170px"
                                                                            TabIndex="2"></asp:TextBox>
                                                                        <img id="imgCloseDateTo" alt="" tabindex="2" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtCloseDateTo1.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y %H:%M",
                                                  button         :    "imgCloseDateTo",
                                                 //align          :    "Tl",
                                                  singleClick    :    true,
                                                  showsTime      : true
                                                               });
                                                                        </script>

                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td class="textbold">1A Office</td>
                                                                    <td><asp:DropDownList onkeyup="gotop(this.id)" ID="ddlAOffice" runat="server" CssClass="dropdownlist"
                                                                            Width="176px" TabIndex="2">
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold">Agency 1A Office</td>
                                                                    <td><asp:DropDownList onkeyup="gotop(this.id)" ID="ddlAgencyAOffice" runat="server" CssClass="dropdownlist"
                                                                            Width="176px" TabIndex="2">
                                                                        </asp:DropDownList></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td class="textbold">PTR No.</td>
                                                                    <td><asp:TextBox ID="txtPTRNo" runat="server" CssClass="textbox" Width="170px" MaxLength="9"
                                                                            onkeyup="checknumeric(this.id)" TabIndex="2"></asp:TextBox></td>
                                                                    <td class="textbold">Work Order No.</td>
                                                                    <td><asp:TextBox ID="txtWorkOrderNo" runat="server" CssClass="textbox" Width="170px"
                                                                            MaxLength="9" onkeyup="checknumeric(this.id)" TabIndex="2"></asp:TextBox></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td class="textbold">LTR No.</td>
                                                                    <td><asp:TextBox ID="txtLTRNo" runat="server" CssClass="textbox" Width="170px" MaxLength="9"
                                                                            onkeyup="checknumeric(this.id)" TabIndex="2"></asp:TextBox></td>
                                                                    <td class="textbold">Region</td>
                                                                    <td><asp:DropDownList onkeyup="gotop(this.id)" ID="ddlRegion" runat="server" CssClass="dropdownlist"
                                                                            Width="176px" TabIndex="2">
                                                                        </asp:DropDownList></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td class="textbold">State</td>
                                                                    <td><asp:DropDownList onkeyup="gotop(this.id)" ID="ddlState" runat="server" CssClass="dropdownlist"
                                                                            Width="176px" TabIndex="2">
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold">City</td>
                                                                    <td><asp:DropDownList onkeyup="gotop(this.id)" ID="ddlCity" runat="server" CssClass="dropdownlist"
                                                                            Width="176px" TabIndex="2">
                                                                        </asp:DropDownList></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td class="textbold">IATA ID</td>
                                                                    <td><asp:TextBox ID="txtIATA" runat="server" CssClass="textbox" MaxLength="20" TabIndex="2"
                                                                            Width="170px"></asp:TextBox></td>
                                                                    <td class="textbold">Call Assigned To</td>
                                                                    <td><asp:TextBox ID="txtAssignedTo" runat="server" CssClass="textbox" MaxLength="20"
                                                                            TabIndex="2" Width="170px"></asp:TextBox></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td class="textbold">
                                                                        IATA</td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkISIATA" runat="server" CssClass="textbold" Checked="True" TabIndex="2" /></td>
                                                                    <td class="textbold">
                                                                        Contact Type</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlContactType" runat="server" CssClass="textbold" onkeyup="gotop(this.id)"
                                                                            TabIndex="2" Width="174px">
                                                                        </asp:DropDownList></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td class="textbold">
                                                                        Display Last Call</td>
                                                                    <td><asp:CheckBox ID="chkDisplayLastCall" runat="server" CssClass="textbold" onClick="fillDateCallLog(1)"
                                                                            TabIndex="2" /></td>
                                                                    <td class="textbold">
                                                                        Pending</td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkISPending" runat="server" CssClass="textbold" onClick="fillDateCallLog(2)"
                                                                            TabIndex="2" /></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Title</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTitle" runat="server" CssClass="textbox" MaxLength="100" 
                                                                            TabIndex="2" Width="170px"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        Airline Name</td>
                                                                    <td><asp:DropDownList ID="drpAirLineName" runat="server" CssClass="textbold" onkeyup="gotop(this.id)"
                                                                            TabIndex="2" Width="174px">
                                                                    </asp:DropDownList></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        IR Number</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtHD_IR_REF" runat="server" CssClass="textbox" MaxLength="9" onkeyup="checknumeric(this.id)"
                                                                            TabIndex="2" Width="170px"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        Company Vertical</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DlstCompVertical" runat="server" CssClass="dropdownlist" TabIndex="2"
                                                                            Width="174px">
                                                                        </asp:DropDownList></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <input id="hdRowID" runat="server" style="width: 1px" type="hidden" /></td>
                                                                    <td>
                                                                    <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" />
                                                                    <input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' />
                                                                    </td>
                                                                    <td colspan="4">
                                                                        <input id="hdFromTime" runat="server" style="width: 1px" type="hidden" enableviewstate="true" />
                                                                        <input id="hdToTime" runat="server" style="width: 1px" type="hidden" enableviewstate="true" />
                                                                        <input id="hdPendingTime" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdDeleteId" runat="server" style="width: 1px" type="hidden" enableviewstate="true" />
                                                                        <input id="hdData" runat="server" style="width: 1px" type="hidden" />
                                                                        <asp:HiddenField ID="hdLcodeMuk" runat="server" />
                                                                        <asp:HiddenField ID="hdReIDMuk" runat="server" />
                                                                        <asp:HiddenField ID="hdStatusMuk" runat="server" />
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
                </tr>
                <tr>
                    <td class="redborder">
                        <asp:Button ID="btnEdit" runat="server" CssClass="button bottomMargin"
                            Text="Edit" OnClientClick="return GetCallLogData()" Visible="False" />
                        <asp:Button ID="btnChangeStatus" runat="server" CssClass="button bottomMargin"
                            Text="Change Status" OnClientClick="return StatusChangeDataCallLog()" Visible="False" Width="110px" />
                        <asp:Button ID="btnSelect" runat="server" CssClass="button bottomMargin displayNone"
                            Text="Select" OnClientClick="return ReturnDataCallLog()" Visible="False" />
                        <asp:GridView ID="gvCallLog" runat="server" AutoGenerateColumns="False" TabIndex="6"
                            Width="2300px" EnableViewState="False" AllowSorting="True">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <input type="checkbox" id="chkAllSelect" name="chkAllSelect" onclick="SelectAllCallLog();" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <input type="checkbox" id="chkSelect" name="chkSelect" runat="server" />
                                        <asp:HiddenField ID="hdDataID" runat="server" Value='<% #Container.DataItem("HD_RE_ID")%> ' />
                                        <asp:HiddenField ID="hdLCode" runat="server" Value='<% #Container.DataItem("LCODE")%> ' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="LTR No" DataField="HD_RE_ID" SortExpression="HD_RE_ID" HeaderStyle-Width="3%">
                                </asp:BoundField>
                                <asp:BoundField HeaderText="OfficeID" DataField="OfficeID" SortExpression="OfficeID" HeaderStyle-Width="4%">
                                         </asp:BoundField>
                                <asp:BoundField HeaderText="Agency Name" DataField="AgencyName" SortExpression="AgencyName" HeaderStyle-Width="8%" ItemStyle-Wrap="true">                                                                    
                                </asp:BoundField>
                                 <asp:BoundField HeaderText="Company Vertical" DataField="COMP_VERTICAL" SortExpression="COMP_VERTICAL" HeaderStyle-Width="3%" HeaderStyle-Wrap ="true" >
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Opened Date" DataField="HD_RE_OPEN_DATE" SortExpression="HD_RE_OPEN_DATE" HeaderStyle-Width="5%">
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Closed Date" DataField="HD_RE_CLOSED_DATE" SortExpression="HD_RE_CLOSED_DATE" HeaderStyle-Width="5%">
                                
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Call Logged By" DataField="LoggedBy" SortExpression="LoggedBy" HeaderStyle-Width="5%" ItemStyle-Wrap="true">
                                
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Call Closed By" DataField="AssignedTo" SortExpression="AssignedTo" HeaderStyle-Width="5%" ItemStyle-Wrap="true">
                                
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Query Group" DataField="HD_QUERY_GROUP_NAME" SortExpression="HD_QUERY_GROUP_NAME" HeaderStyle-Width="4%" ItemStyle-Wrap="true">
                                  
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Query Sub Group" DataField="CALL_SUB_GROUP_NAME" SortExpression="CALL_SUB_GROUP_NAME" HeaderStyle-Width="4%" ItemStyle-Wrap="true">
                                  
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Query Category" DataField="CALL_CATEGORY_NAME" SortExpression="CALL_CATEGORY_NAME" HeaderStyle-Width="4%" ItemStyle-Wrap="true">
                                  
                                </asp:BoundField>
                                
                                <asp:BoundField HeaderText="Assigned To" DataField="LAST_ASSIGNEDTO" SortExpression="LAST_ASSIGNEDTO" HeaderStyle-Width="4%" ItemStyle-Wrap="true">
                                  
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Left Date Time" DataField="LEFTDATETIME" SortExpression="LEFTDATETIME" HeaderStyle-Width="5%" ItemStyle-Wrap="true">
                                  
                                </asp:BoundField>
                                
                                <asp:BoundField HeaderText="Status" DataField="HD_STATUS_NAME" SortExpression="HD_STATUS_NAME" HeaderStyle-Width="6%" ItemStyle-Wrap="true">
                                
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Rept Calls" DataField="RepeatCalls" SortExpression="RepeatCalls" HeaderStyle-Width="2%" />
                                <asp:BoundField HeaderText="Linked LTR" DataField="HD_RE_ID_CHILD" SortExpression="HD_RE_ID_CHILD" HeaderStyle-Width="6%" />
                                 <asp:BoundField HeaderText="AOffice" DataField="AOffice" SortExpression="AOffice" HeaderStyle-Width="3%" />
                                 
                                 <asp:BoundField HeaderText="Title" DataField="Title" SortExpression="Title" HeaderStyle-Width="6%" />
                                 <asp:BoundField HeaderText="Coordinator1" DataField="Coordinator1" SortExpression="Coordinator1" HeaderStyle-Width="5%" />
                                 <asp:BoundField HeaderText="Coordinator2" DataField="Coordinator2" SortExpression="Coordinator2" HeaderStyle-Width="5%" />
                                 <asp:BoundField HeaderText="AirlineName" DataField="AIRLINE_NAME" SortExpression="AIRLINE_NAME" HeaderStyle-Width="5%" />
                                
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="200px" ItemStyle-Wrap="false" >
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditX" Text="Edit" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="lnkRelog" runat="server" CommandName="RelogX" Text="Relog" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteX" Text="Delete"  CssClass="LinkButtons"></asp:LinkButton>                                       
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle CssClass="lightblue" />
                            <RowStyle CssClass="textbold" />
                            <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                        </asp:GridView>
                        <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr class="paddingtop paddingbottom">
                                    <td style="width: 30%" class="left">
                                        <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                            ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey"
                                            ReadOnly="True"></asp:TextBox></td>
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
        </div>
    </form>
</body>
</html>
