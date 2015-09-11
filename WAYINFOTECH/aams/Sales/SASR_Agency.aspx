<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SASR_Agency.aspx.vb" Inherits="Sales_SASR_Agency" EnableEventValidation="false" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>AAMS: Agency</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
</head>

<script type="text/javascript" src="../JavaScript/AAMS.js"></script>

<script type="text/javascript" src="../Calender/calendar.js"></script>

<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

<script type="text/javascript" src="../Calender/calendar-setup.js"></script>

<body onload="javascript:OnloadAdvanceSearchTravelAgency();">
    <form id="form1" runat="server" defaultfocus="txtAgencyName" defaultbutton="btnSearch">
        <table width="860px" class="border_rightred">
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td align="left">
                                <span class="menu">Sales -&gt;DSR -&gt;</span><span class="sub_menu"><asp:Label id="LblPlan" runat="server"   text  ="Unplanned Visit"></asp:Label></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center">
                                Search Agency
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center">
                                                        &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="false"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 6%">
                                                    </td>
                                                    <td style="width: 18%">
                                                    </td>
                                                    <td style="width: 20%">
                                                    </td>
                                                    <td style="width: 18%">
                                                    </td>
                                                    <td style="width: 20%">
                                                    </td>
                                                    <td style="width: 18%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold">
                                                        Agency Name</td>
                                                    <td colspan="3">
                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpSearchType" CssClass="dropdownlist"
                                                            Width="104px" runat="server" TabIndex="1">
                                                            <asp:ListItem>Contains</asp:ListItem>
                                                            <asp:ListItem>Starting With</asp:ListItem>
                                                            <asp:ListItem>Exactly</asp:ListItem>
                                                        </asp:DropDownList>&nbsp;&nbsp;<asp:TextBox ID="txtAgencyName" CssClass="textbox"
                                                            MaxLength="100" runat="server" Width="341px" TabIndex="1"></asp:TextBox><span class="textbold"></span></td>
                                                    <td>
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="2"
                                                            AccessKey="A" /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold">
                                                        Short Name</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShortName" runat="server" CssClass="textbox" MaxLength="40" TabIndex="1"></asp:TextBox></td>
                                                    <td class="textbold">
                                                        City</td>
                                                    <td>
                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpCity" CssClass="dropdownlist" Width="137px"
                                                            runat="server" TabIndex="1">
                                                        </asp:DropDownList></td>
                                                    <td>
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="2"
                                                            AccessKey="R" /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold">
                                                        Office ID</td>
                                                    <td>
                                                        <asp:TextBox ID="txtOfficeId" runat="server" CssClass="textbox" MaxLength="20" TabIndex="1"></asp:TextBox></td>
                                                    <td class="textbold">
                                                        Country</td>
                                                    <td>
                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpCountry" CssClass="dropdownlist"
                                                            Width="137px" runat="server" TabIndex="1">
                                                        </asp:DropDownList></td>
                                                    <td>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold">
                                                        CRS</td>
                                                    <td>
                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpCRS" CssClass="dropdownlist" Width="137px"
                                                            runat="server" TabIndex="1">
                                                        </asp:DropDownList></td>
                                                    <td class="textbold">
                                                        Aoffice</td>
                                                    <td>
                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpAoffice" CssClass="dropdownlist"
                                                            Width="137px" runat="server" TabIndex="1">
                                                        </asp:DropDownList></td>
                                                    <td>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold">
                                                        Lcode</td>
                                                    <td>
                                                        <asp:TextBox ID="txtLcode" runat="server" CssClass="textbox" MaxLength="9" TabIndex="1"></asp:TextBox></td>
                                                    <td class="textbold">
                                                        Chain Code</td>
                                                    <td>
                                                        <asp:TextBox ID="txtChainCode" runat="server" CssClass="textbox" MaxLength="9" TabIndex="1"></asp:TextBox></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold">
                                                        Whole Group</td>
                                                    <td>
                                                        <asp:CheckBox ID="chkWholeGroup" runat="server" CssClass="textbold" TabIndex="1"
                                                            TextAlign="Left" Width="144px" /></td>
                                                    <td class="textbold">
                                                        Company Vertical</td>
                                                    <td>
                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="DlstCompVertical" CssClass="dropdownlist"
                                                            Width="137px" runat="server" TabIndex="1">
                                                        </asp:DropDownList></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td colspan="4" class="subheading">
                                                        <img alt="" src="../Images/down.jpg" style="cursor: pointer" id="btnUp" runat="server"
                                                            tabindex="1" />&nbsp;&nbsp;<asp:LinkButton ID="lnkAdvance" CssClass="menu" Text="Advance Search"
                                                                runat="server" TabIndex="1"></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:Panel ID="pnlAdvanceSearch" runat="server" Width="100%">
                                                            <table style="width: 100%" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td style="width: 6%">
                                                                    </td>
                                                                    <td style="width: 18%" class="textbold">
                                                                        Address</td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtAddress" CssClass="textbox" MaxLength="200" runat="server" TabIndex="1"
                                                                            Width="452px"></asp:TextBox><span class="textbold"></span></td>
                                                                    <td style="width: 18%">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Online Status</td>
                                                                    <td style="width: 20%">
                                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpOnlineStatus" CssClass="dropdownlist"
                                                                            Width="137px" runat="server" TabIndex="1">
                                                                        </asp:DropDownList></td>
                                                                    <td style="width: 18%">
                                                                        <span style="font-size: 9pt; font-family: Arial">Agency Status</span></td>
                                                                    <td style="width: 20%">
                                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpAgencyStatus" CssClass="dropdownlist"
                                                                            Width="137px" runat="server" TabIndex="1">
                                                                        </asp:DropDownList></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Agency Type</td>
                                                                    <td>
                                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpAgencyType" CssClass="dropdownlist"
                                                                            Width="137px" runat="server" TabIndex="1">
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold">
                                                                        Email</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" MaxLength="40" TabIndex="1"></asp:TextBox></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Date Offline From</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDateOfflineF" runat="server" CssClass="textbox" MaxLength="40"
                                                                            TabIndex="1"></asp:TextBox>
                                                                        <img id="imgDateOfflineF" alt="" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateOfflineF.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateOfflineF",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                        </script>

                                                                    </td>
                                                                    <td class="textbold">
                                                                        Date Offline To</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDateOfflineT" runat="server" CssClass="textbox" MaxLength="40"
                                                                            TabIndex="1"></asp:TextBox>
                                                                        <img id="imgDateOfflineT" alt="" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateOfflineT.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateOfflineT",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                        </script>

                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Date Online From</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDateOnlineF" runat="server" CssClass="textbox" MaxLength="40"
                                                                            TabIndex="1"></asp:TextBox>
                                                                        <img id="imgDateOnlineF" alt="" src="../Images/calender.gif" tabindex="1" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateOnlineF.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateOnlineF",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                        </script>

                                                                    </td>
                                                                    <td class="textbold">
                                                                        Date Online To</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDateOnlineT" runat="server" CssClass="textbox" MaxLength="40"
                                                                            TabIndex="1"></asp:TextBox>
                                                                        <img id="imgDateOnlineT" alt="" src="../Images/calender.gif" tabindex="1" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateOnlineT.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateOnlineT",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                        </script>

                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Fax</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFax" runat="server" CssClass="textbox" MaxLength="40" TabIndex="1"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        File Number</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFielNumber" runat="server" CssClass="textbox" MaxLength="5" TabIndex="1"></asp:TextBox></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        IATA ID</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIATAId" runat="server" CssClass="textbox" MaxLength="20" TabIndex="1"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        IP Address</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIPAddress" runat="server" CssClass="textbox" MaxLength="16" TabIndex="1"></asp:TextBox></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Backup</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="drpBackupOnlineStatus" runat="server" CssClass="textbold" Height="25px"
                                                                            onkeyup="gotop(this.id)" TabIndex="1" Width="137px">
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold">
                                                                        Phone</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="textbox" MaxLength="30" TabIndex="1"></asp:TextBox></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Website</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtWeb" runat="server" CssClass="textbox" MaxLength="100" TabIndex="1"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        IATA Status</td>
                                                                    <td>
                                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpPriority" CssClass="dropdownlist"
                                                                            Width="137px" runat="server" TabIndex="1">
                                                                        </asp:DropDownList></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        1Aresponsibility</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAResponsibility" runat="server" CssClass="textboxgrey" MaxLength="40"
                                                                          ReadOnly ="true"    TabIndex="1" Width="131px"></asp:TextBox><img alt="" onclick="javascript:return EmployeePageTravelAgency();" style  ="display:none;"
                                                                                src="../Images/lookup.gif" tabindex="1"  /></td>
                                                                    <td class="textbold" align="left">
                                                                        Agency Using Birdres</td>
                                                                    <td class="textbold">
                                                                        <asp:CheckBox ID="chkAgencyUsingBirdres" runat="server" TabIndex="1" /></td>
                                                                    <td>
                                                                        <input type="hidden" id="hdEmployeePageName" style="width: 1px" runat='server' /></td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:HiddenField ID="hdAdvanceSearch" EnableViewState="true" runat="server" />
                                                        <asp:HiddenField ID="hdEmployeeName" EnableViewState="true" runat="server" />
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" class="gap">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:GridView ID="grdAgency" runat="server" AutoGenerateColumns="False" TabIndex="3"
                                                            Width="100%" EnableViewState="true" AllowSorting="true" HeaderStyle-ForeColor="white">
                                                            <Columns>
                                                                  <asp:TemplateField HeaderText="  "  HeaderImageUrl ="~/Images/empty-flg.gif"  SortExpression="COLORCODE">                                                                  
                                                                    <ItemTemplate>
                                                                        <asp:Image ImageUrl="" runat ="server"  ID ="ImgColorCode" />
                                                                        <asp:HiddenField ID="hdColorCode" runat="server" Value='<%#Eval("COLORCODE")%>' />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="30px" />
                                                                    <ItemStyle Width="30px" Wrap="false" HorizontalAlign ="center" />
                                                                 </asp:TemplateField>
                                                                <asp:BoundField DataField="CHAIN_CODE" HeaderText="Chain Code " SortExpression="CHAIN_CODE">
                                                                    <HeaderStyle Width="7%" Wrap="False" />
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="LOCATION_CODE" HeaderText="Lcode " SortExpression="LOCATION_CODE">
                                                                    <HeaderStyle Width="6%" Wrap="False" />
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OfficeID" HeaderText="Office ID " SortExpression="OfficeID">
                                                                    <HeaderStyle Width="9%" Wrap="False" />
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="NAME" HeaderText="Agency Name" SortExpression="NAME">
                                                                    <HeaderStyle Width="18%" Wrap="False" />
                                                                    <ItemStyle Wrap="True" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ADDRESS" HeaderText="Address " SortExpression="ADDRESS">
                                                                    <HeaderStyle Width="20%" Wrap="False" />
                                                                    <ItemStyle Wrap="True" />
                                                                </asp:BoundField>
                                                              
                                                                <asp:BoundField DataField="CITY" HeaderText="City " SortExpression="CITY">
                                                                    <HeaderStyle Width="9%" Wrap="False" />
                                                                    <ItemStyle Wrap="True" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="COUNTRY" HeaderText="Country " SortExpression="COUNTRY">
                                                                    <HeaderStyle Width="10%" Wrap="False" />
                                                                    <ItemStyle Wrap="True" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ONLINE_STATUS" HeaderText="Online Status " SortExpression="ONLINE_STATUS">
                                                                    <HeaderStyle Width="8%" Wrap="False" />
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                     <asp:LinkButton ID="lnkSelect"  runat="server" CssClass="LinkButtons" CommandName ="SelectData" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LOCATION_CODE") + "|" + DataBinder.Eval(Container.DataItem, "CHAIN_CODE")+ "|" + DataBinder.Eval(Container.DataItem, "OfficeID") + "|" + DataBinder.Eval(Container.DataItem, "NAME") + "|" + DataBinder.Eval(Container.DataItem, "CITY")+ "|" + DataBinder.Eval(Container.DataItem,"COUNTRY") %>'>Select</asp:LinkButton>
                                                                     <asp:HiddenField ID="HdSelected" runat="server" Value='<% #Container.DataItem("SELECTED") %>' />                                                                   
                                                                                
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="10%" />
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <RowStyle CssClass="textbold" />
                                                            <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" valign="top">
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
                                                                        <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList onkeyup="gotop(this.id)"
                                                                            ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="litAgency" runat="server"></asp:Literal><asp:TextBox ID="txtRecordOnCurrentPage"
                        runat="server" Width="73px" CssClass="textboxgrey" Visible="false"></asp:TextBox></td>
            </tr>
        </table>
    </form>
</body>
</html>
