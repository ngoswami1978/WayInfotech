<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRDSR_AirlineWise_Dailybooking.aspx.vb"
    Inherits="Productivity_PRDSR_AirlineWise_Dailybooking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Productivity</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>

</head>
<body>
    <form id="form1" defaultbutton="btnDisplay" runat="server">
        <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <!-- Code for Search Criteria -->
                    <table width="860px" align="left" class="border_rightred">
                        <tr>
                            <td valign="top" style="width: 860px;">
                                <table width="100%" align="left">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">Productivity-></span><span class="sub_menu">DailyBookings Airlinewise
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="heading" align="center" valign="top">
                                            DailyBookings Airlinewise
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
                                                                            <td height="25" width="6%" style="width: 100px">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Airline Name</td>
                                                                            <td colspan="4">
                                                                                <asp:DropDownList ID="drpAirLineName" runat="server" CssClass="dropdownlist" Width="480px"
                                                                                    TabIndex="1">
                                                                                </asp:DropDownList></td>
                                                                            <td align="left">
                                                                                <asp:Button ID="btnDisplay" CssClass="button" runat="server" Text="Search" TabIndex="3"
                                                                                    AccessKey="A" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="25" width="6%">
                                                                            </td>
                                                                            <td class="textbold" style="width: 150px">
                                                                                Country</td>
                                                                            <td style="width: 150px">
                                                                                <asp:DropDownList ID="drpCountrys" runat="server" CssClass="dropdownlist" Width="137px"
                                                                                    TabIndex="1">
                                                                                </asp:DropDownList></td>
                                                                                <td style="width:50px"></td>
                                                                            <td class="textbold" align="left" style="width: 200px">
                                                                                City</td>
                                                                            <td style="width: 250px">
                                                                                <asp:DropDownList ID="drpCitys" runat="server" CssClass="dropdownlist" Width="143px"
                                                                                    TabIndex="1">
                                                                                </asp:DropDownList></td>
                                                                            <td style="width: 150px" align="left">
                                                                                <asp:Button ID="btnExport" runat="server" CssClass="button" Text="Export" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="6%" style="height: 25px">
                                                                            </td>
                                                                            <td class="textbold" style="height: 25px;">
                                                                                1a Office</td>
                                                                            <td style="height: 25px;">
                                                                                <asp:DropDownList ID="drpOneAOffice" runat="server" CssClass="dropdownlist" Width="137px"
                                                                                    TabIndex="1">
                                                                                </asp:DropDownList></td>
                                                                                 <td style="width:50px">&nbsp;</td>
                                                                            <td width="18%" class="textbold" align="left">
                                                                                Region</td>
                                                                            <td>
                                                                                <asp:DropDownList ID="drpRegion" runat="server" CssClass="dropdownlist" Width="144px"
                                                                                    TabIndex="1">
                                                                                </asp:DropDownList></td>
                                                                            <td align="left">
                                                                                <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" Style="position: relative;
                                                                                    left: 0px; top: 0px;" TabIndex="3" AccessKey="R" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 25px" width="6%">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Group Type</td>
                                                                            <td>
                                                                                <asp:DropDownList ID="drpLstGroupType" runat="server" CssClass="dropdownlist" TabIndex="1"
                                                                                    Width="137px">
                                                                                </asp:DropDownList></td>
                                                                                 <td style="width:50px"></td>
                                                                            <td align="left" class="textbold" width="18%">
                                                                                Company Vertical</td>
                                                                            <td>
                                                                                <asp:DropDownList ID="DlstCompVertical" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                                    TabIndex="1" Width="144px">
                                                                                </asp:DropDownList></td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 25px" width="6%">
                                                                            </td>
                                                                            <td class="textbold" style="width: 288px; height: 25px">
                                                                                Month
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="drpMonth" runat="server" CssClass="dropdownlist" Width="137px"
                                                                                    TabIndex="1">
                                                                                </asp:DropDownList></td>
                                                                                 <td style="width:50px"></td>
                                                                            <td align="left" class="textbold" width="18%">
                                                                                &nbsp;Year&nbsp;</td>
                                                                            <td>
                                                                                <asp:DropDownList ID="drpYear" runat="server" CssClass="dropdownlist" Width="144px"
                                                                                    TabIndex="1">
                                                                                </asp:DropDownList></td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr height="20px">
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <input type="hidden" id="hdAgencyNameId" runat="server" value="" style="width: 5px" />
                                            <input type="hidden" id="hdAgency" runat="server" value="" style="width: 5px" />
                                            <input type="hidden" id="hdAir" runat="server" value="" style="width: 5px" />
                                            <input type="hidden" id="hdAirBr" runat="server" value="" style="width: 5px" />
                                            <input type="hidden" id="hdCar" runat="server" value="" style="width: 5px" />
                                            <input type="hidden" id="hdHotel" runat="server" value="" style="width: 5px" />
                                            <input type="hidden" id="hdChkGroupProductivity" runat="server" value="" style="width: 5px" />
                                            <input type="hidden" id="hdButtonClick" runat="server" value="" style="width: 5px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <!-- code for paging----->
                        <!-- code for paging----->
                        <tr>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <!-- Code for Search Result Gridview & Paging -->
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr valign="top">
                            <td valign="top" style="padding-left: 4px;" colspan="2">
                                <asp:Panel ID="Panel2" runat="server">
                                    <table id="tlbgrdvAirWithAirBr" runat="server" border="0" align="left" cellpadding="0"
                                        cellspacing="0">
                                        <tr>
                                            <td class="redborder">
                                                <asp:GridView EnableViewState="false" HeaderStyle-ForeColor="white" ShowFooter="true"
                                                    FooterStyle-HorizontalAlign="right" ID="grdvAirWithAirBr" runat="server" AutoGenerateColumns="False"
                                                    HorizontalAlign="Center" FooterStyle-CssClass="Gridheading" HeaderStyle-CssClass="Gridheading"
                                                    RowStyle-CssClass="ItemColor" AlternatingRowStyle-CssClass="lightblue" AllowSorting="True"
                                                    TabIndex="4">
                                                    <Columns>
                                                        <asp:TemplateField SortExpression="Airline_Name" HeaderText="Airline Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAirlineName" runat="server" Text='<%# Eval("Airline_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="false" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="AverageBookings" HeaderText="Average Bookings">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAverageBookings" runat="server" Text='<%# Eval("AverageBookings") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="70px" Wrap="true" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Netbookings" HeaderText="Air Net">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNetbookings" runat="server" Text='<%# Eval("Netbookings") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="45px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField SortExpression="D1" DataField="D1">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D2" DataField="D2">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D3" DataField="D3">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D4" DataField="D4">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D5" DataField="D5">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D6" DataField="D6">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D7" DataField="D7">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D8" DataField="D8">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D9" DataField="D9">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D10" DataField="D10">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D11" DataField="D11">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D12" DataField="D12">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D13" DataField="D13">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D14" DataField="D14">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D15" DataField="D15">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D16" DataField="D16">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D17" DataField="D17">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D18" DataField="D18">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D19" DataField="D19">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D20" DataField="D20">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D21" DataField="D21">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D22" DataField="D22">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D23" DataField="D23">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D24" DataField="D24">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D25" DataField="D25">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D26" DataField="D26">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D27" DataField="D27">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D28" DataField="D28">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D29" DataField="D29">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D30" DataField="D30">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField SortExpression="D31" DataField="D31">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="true" ForeColor="White" />
                                                    <RowStyle CssClass="textbold" />
                                                    <FooterStyle CssClass="Gridheading" HorizontalAlign="Right" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="left">
                                <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="800px">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
                                        <tr class="paddingtop paddingbottom">
                                            <td style="width: 243px" class="left" nowrap="nowrap">
                                                <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                    ID="txtTotalRecordCount" runat="server" ReadOnly="true" Width="105px" CssClass="textboxgrey"
                                                    TabIndex="5"></asp:TextBox></td>
                                            <td style="width: 200px" class="right">
                                                <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"
                                                    TabIndex="5"><< Prev</asp:LinkButton></td>
                                            <td style="width: 356px" class="center">
                                                <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber"
                                                    Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" TabIndex="5">
                                                </asp:DropDownList></td>
                                            <td style="width: 187px" class="left">
                                                <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next"
                                                    TabIndex="5">Next >></asp:LinkButton></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
