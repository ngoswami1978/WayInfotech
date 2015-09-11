<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INC_PaymentProcHistory.aspx.vb"
    Inherits="Incentive_INC_PaymentProcHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AAMS::Incentive::Process Payment History</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table width="990px" align="left" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left" colspan="2">
                                <span class="menu">Incentive-&gt;</span><span class="sub_menu">Process&nbsp; Payment
                                    History</span></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 840px;">
                            </td>
                            <td>
                                <a href="#" class="LinkButtons" onclick="window.close();">Close</a></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" colspan="2">
                                Process Payment History</td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="2">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="textbold" align="center" style="width: 850px;">
                                            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="TrAvgQual" runat="server">
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td colspan="6" width="100%" align="center">
                                                        <asp:Panel ID="PnlQualAvg" runat="server" Visible="False" Style="width: 100%;">
                                                            <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td style="width: 100%;" align="center" class="subheading">
                                                                        Average Qualification History
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" style="width: 100%">
                                                                        <asp:GridView ID="GvQualAvg" runat="server" AutoGenerateColumns="False" TabIndex="1"
                                                                            Width="100%" EnableViewState="true" AllowSorting="false" HeaderStyle-ForeColor="white"
                                                                            ShowFooter="false">
                                                                            <Columns>
                                                                                <asp:BoundField HeaderText="Qualification" DataField="QualiAvgNIDTFIELDS" ItemStyle-Wrap="true"
                                                                                    ItemStyle-Width="360px" HeaderStyle-Width="360px"></asp:BoundField>
                                                                                <asp:TemplateField HeaderText="Qualification Remark">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtAvgQualRem" runat="server" TextMode="MultiLine" CssClass="textboxgrey left"
                                                                                            ReadOnly="true" TabIndex="1" Text='<%# Eval("REMARK") %>' Width="300px"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                                                    <FooterStyle HorizontalAlign="Left" Width="300px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Employee Name" SortExpression="EMPLOYEENAME">
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("EMPLOYEENAME")%>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Left" Width="180px" />
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="180px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Date Time" SortExpression="DateTime">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblDate" Text='<%#Eval("DateTime")%>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Left" Width="145px" />
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="145px" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                            <RowStyle CssClass="textbold" VerticalAlign="Top" />
                                                                            <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                            <FooterStyle CssClass="Gridheading" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 2px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr id="TrPayProc" runat="server">
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td colspan="6" align="center" class="subheading">
                                                        Process Payment History
                                                    </td>
                                                    <td colspan="1">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="7">
                                                        &nbsp;
                                                    </td>
                                                    <td colspan="1" style="width: 5px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" width="100%" valign="top">
                                                        <asp:Repeater ID="RptPaymentHistory" runat="server">
                                                            <HeaderTemplate>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr class="lightblue">
                                                                        <td colspan="2" style="width: 100%; text-align: center;">
                                                                            <span style="background-color: Yellow; font-weight: bold;"><<--------------------------------------
                                                                                History
                                                                                <%# (CType(Container, RepeaterItem).ItemIndex+1).ToString() %>
                                                                                -------------------------------------->></span>
                                                                            <%--  <span style ="background-color:Yellow;font-weight:bold;" ><<-------------------------------------- History  <%#  (Val(Ctype(RptPaymentHistory.DataSource,DataTable ).Rows.Count)- Val(CType(Container, RepeaterItem).ItemIndex)).ToString    %>  -------------------------------------->></span>                                                                                                                                                         --%>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="lightblue">
                                                                        <td align="center">
                                                                            <asp:Label CssClass="textbold" ID="lblDate" runat="server" Text='<%# Eval("LOGDATE") %>'></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label CssClass="textbold" ID="LblEmpName" runat="server" Text='<%# Eval("EMPLOYEE_NAME") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td class="textbold" style="width: 150px;" valign="top">
                                                                                        Qualification
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtQualAvgSelected" runat="server" CssClass="textboxgrey" TextMode="MultiLine"
                                                                                            ReadOnly="True" TabIndex="1" Width="618px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="textbold" style="width: 150px;" valign="top">
                                                                                        Qualification Avg.
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtQualAvgData" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                            TabIndex="1" Width="100px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <input id="hdEnChainCode" runat="server" style="width: 21px" type="hidden" /><input
                                                                                id="hdChainCode" runat="server" style="width: 21px" type="hidden" />
                                                                            <input id="HdPaymentId" runat="server" style="width: 21px" type="hidden" />
                                                                            <input id="HdPACreated" runat="server" style="width: 21px" type="hidden" />
                                                                            <input id="hdFixedOrRate" runat="server" style="width: 21px" type="hidden" />
                                                                            <input id="hdIsPLB" runat="server" style="width: 21px" type="hidden" />
                                                                            <input id="hdFirstTime" runat="server" style="width: 21px" type="hidden" />
                                                                            <input id="hdPaymentType" runat="server" style="width: 21px" type="hidden" />
                                                                            <input id="hdUpFronttType" runat="server" style="width: 21px" type="hidden" />
                                                                            <input id="HdpREVPaymentId" runat="server" style="width: 21px" type="hidden" />
                                                                            <input id="HdYearAndSettleMent" runat="server" style="width: 21px" type="hidden" />
                                                                            <input id="hdFinallyApproved" runat="server" style="width: 21px" type="hidden" />
                                                                            <input id="hdEndSettlement" runat="server" style="width: 21px" type="hidden" />
                                                                            <input id="HdUpNoPay" runat="server" style="width: 21px" type="hidden" />
                                                                            <input id="HdUpNoPayDone" runat="server" style="width: 21px" type="hidden" />
                                                                            <input id="HdOneTimeUpNoOfPay" runat="server" style="width: 21px" type="hidden" />
                                                                            <input id="HdOneTimeUpNoPayDone" runat="server" style="width: 21px" type="hidden" />
                                                                            <input id="HdCurPayNo" runat="server" style="width: 21px" type="hidden" />
                                                                            <input id="HdAdjustable" runat="server" style="width: 21px" type="hidden" />
                                                                            <input id="hdShowPopup" runat="server" style="width: 21px" type="hidden" />
                                                                            <input id="HdPaidUpFrontAtthisLevel" runat="server" style="width: 21px" type="hidden" />
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="100%" colspan="2">
                                                                            <asp:GridView ID="GvProcessPayment" runat="server" AutoGenerateColumns="False" TabIndex="1"
                                                                                Width="100%" EnableViewState="true" AllowSorting="false" HeaderStyle-ForeColor="white"
                                                                                ShowFooter="true">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Productivity Type">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label class="textbold" ID="lblProductivity" runat="server" Text='<%# Eval("PRODUCTIVITYTYPE") %>'></asp:Label>
                                                                                            <asp:HiddenField ID="hdProductivityId" runat="server" Value='<%# Eval("PRODUCTIVITYTYPEID") %>' />
                                                                                            <asp:HiddenField ID="hdPayMentId" runat="server" Value='<%# Eval("PAYMENT_ID") %>' />
                                                                                            <asp:HiddenField ID="hdNotInBCase" runat="server" Value='<%# Eval("ISCHECKED") %>' />
                                                                                            <asp:HiddenField ID="hdRowID" runat="server" Value='<%# Eval("ROWID") %>' />
                                                                                            <asp:HiddenField ID="hdHL" runat="server" Value='<%# Eval("HL") %>' />
                                                                                            <asp:HiddenField ID="hdROI" runat="server" Value='<%# Eval("ROI") %>' />
                                                                                            <asp:HiddenField ID="hdNidtFields" runat="server" Value='<%# Eval("NIDTFIELDS") %>' />
                                                                                            <asp:HiddenField ID="hdStandardSeg" runat="server" Value='<%# Eval("STANDARSEGMENT") %>' />
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                                                        <ItemStyle HorizontalAlign="Left" Width="120px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Standard Segment">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtStandardSegment" runat="server" MaxLength="5" CssClass="textboxgrey right"
                                                                                                TabIndex="1" ReadOnly="true" Text='<%# Eval("STANDARSEGMENT") %>' Width="96%"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="center" Width="90px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Exemption %">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtExumption" runat="server" MaxLength="5" CssClass="textboxgrey right"
                                                                                                ReadOnly="true" TabIndex="1" Text='<%# Eval("EXCEMPTION") %>' Width="96%"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                                                        <FooterStyle HorizontalAlign="Right" Width="90px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Exempted Segment">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtActulaSegment" runat="server" MaxLength="5" CssClass="textboxgrey right"
                                                                                                TabIndex="1" ReadOnly="true" Text='<%# Eval("SEGMENT") %>' Width="96%"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                                                        <FooterStyle HorizontalAlign="Right" Width="90px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Calculated Segment">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtCalCulatedSegment" ReadOnly="true" runat="server" MaxLength="12"
                                                                                                TabIndex="1" CssClass="textboxgrey right" Text='<%# Eval("SEGMENT") %>' Width="96%"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:TextBox ID="txtTotalCalCulatedSegment" ReadOnly="true" runat="server" MaxLength="12"
                                                                                                TabIndex="1" CssClass="textboxgrey right" Text='<%# Eval("SEGMENT") %>' Width="96%"></asp:TextBox>
                                                                                        </FooterTemplate>
                                                                                        <FooterStyle HorizontalAlign="Right" Width="90px" />
                                                                                        <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Standard Rate / Revised Rate">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtStdRate" runat="server" MaxLength="5" CssClass="textboxgrey right"
                                                                                                TabIndex="1" ReadOnly="true" Text='<%# Eval("STANDARDRATE") %>' Width="48%"></asp:TextBox>&nbsp;<asp:TextBox
                                                                                                    ID="txtRate" runat="server" MaxLength="5" TabIndex="1" Text='<%# Eval("RATE") %>'
                                                                                                    Width="46%" CssClass="textboxgrey right" ReadOnly="true"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                        <FooterStyle HorizontalAlign="Right" Width="210px" />
                                                                                        <ItemStyle HorizontalAlign="Right" Width="210px" Wrap="true" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Amount After Exemption">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtFinalAmount" ReadOnly="true" runat="server" MaxLength="12" CssClass="textboxgrey right"
                                                                                                TabIndex="1" Text='<%# Eval("SEGMENT") %>' Width="96%"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:TextBox ID="txtGrandFinalAmount" ReadOnly="true" runat="server" MaxLength="12"
                                                                                                TabIndex="1" CssClass="textboxgrey right" Text='<%# Eval("SEGMENT") %>' Width="96%"></asp:TextBox>
                                                                                        </FooterTemplate>
                                                                                        <FooterStyle HorizontalAlign="Right" Width="100px" />
                                                                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Markets Remarks">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtRemByChangeInRate" Width="195px" Rows="3" Height="30px" TextMode="MultiLine"
                                                                                                CssClass="textboxgrey left" TabIndex="1" runat="server" Text='<%# Eval("RATEREM") %>' />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Left" Width="200px" VerticalAlign="top" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <AlternatingRowStyle CssClass="lightblue" />
                                                                                <RowStyle CssClass="textbold" VerticalAlign="Top" />
                                                                                <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                <FooterStyle CssClass="Gridheading" />
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="100%" colspan="2">
                                                                            <table  width="990px" cellpadding="0" cellspacing="0" border="0">
                                                                                <tr>
                                                                                    <td width="120px" style="height: 5px;">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td width="300px" colspan="3">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td width="200px">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trUpFrontAmount" runat="server">
                                                                                    <td width="120px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="300px" colspan="3" class="Gridheading">
                                                                                        <asp:Label ID="lblUpFrontAmount" Text="Upfront Amount" runat="server" Visible="False"></asp:Label>
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                        <asp:TextBox ID="txtUpFrontAmount" ReadOnly="true" runat="server" MaxLength="12"
                                                                                            CssClass="textboxgrey right" Width="94%" TabIndex="1" Visible="False"></asp:TextBox>
                                                                                    </td>
                                                                                    <td width="200px">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td width="120px" style="height: 5px;">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="300px" colspan="3">
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                    </td>
                                                                                    <td width="200px">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trSignUpAmt" runat="server" visible="false">
                                                                                    <td width="120px">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td width="300px" colspan="3" class="Gridheading">
                                                                                        <asp:Label ID="LblSignUpAmt" Text="SignUp Amount" runat="server" Visible="False"></asp:Label>
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                        <asp:TextBox ID="txtSignUpAmt" ReadOnly="true" runat="server" MaxLength="12" CssClass="textboxgrey right"
                                                                                            Width="96%" TabIndex="1" Visible="False"></asp:TextBox>
                                                                                    </td>
                                                                                    <td width="200px">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td width="120px" style="height: 5px;">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="300px" colspan="3">
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                    </td>
                                                                                    <td width="200px">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trPrevUpfrontAmount" runat="server">
                                                                                    <td width="120px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="300px" colspan="3" class="Gridheading">
                                                                                        <asp:Label ID="lblPrevUpfrontAmount" Text="Previous Upfront Amount" runat="server"
                                                                                            Visible="False"></asp:Label>
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                        <asp:TextBox ID="txtPrevUpfrontAmount" ReadOnly="true" runat="server" MaxLength="12"
                                                                                            CssClass="textboxgrey right" Width="94%" TabIndex="1" Visible="False"></asp:TextBox>
                                                                                    </td>
                                                                                    <td width="200px">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td width="120px" style="height: 5px;">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="300px" colspan="3">
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                    </td>
                                                                                    <td width="200px">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr runat="server" id="trCBFAmount">
                                                                                    <td width="120px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="300px" colspan="3" class="Gridheading">
                                                                                        <asp:Label ID="LblCBFAmount" runat="server" Text="Carries Balance Upfront Amount"
                                                                                            Visible="False"></asp:Label></td>
                                                                                    <td width="100px">
                                                                                        <asp:TextBox ID="txtCBForwardAmount" runat="server" CssClass="textboxgrey right"
                                                                                            MaxLength="12" ReadOnly="true" TabIndex="1" Visible="False" Width="94%"></asp:TextBox></td>
                                                                                    <td width="200px">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td width="120px" style="height: 5px;">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="300px" colspan="3">
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                    </td>
                                                                                    <td width="200px">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trLPlb" runat="server" visible="false">
                                                                                    <td width="120px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="300px" colspan="3" class="Gridheading">
                                                                                        <asp:Label ID="LblPlb" Text="PLB Amount" runat="server" Visible="False"></asp:Label>
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                        <asp:TextBox ID="txtPLBAmt" ReadOnly="false" runat="server" MaxLength="12" CssClass="textboxbold right"
                                                                                            Width="94%" TabIndex="1" Visible="False"></asp:TextBox>
                                                                                    </td>
                                                                                    <td width="200px">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td width="120px" style="height: 5px;">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="300px" colspan="3">
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                    </td>
                                                                                    <td width="200px">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trBalanceUpfrontAmount" runat="server">
                                                                                    <td width="120px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="300px" colspan="3" class="Gridheading">
                                                                                        <asp:Label ID="lblBalanceUpfrontAmount" Text="Balance Upfront Amount" runat="server"
                                                                                            Visible="False"></asp:Label>
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                        <asp:TextBox ID="txtBalanceUpfrontAmount" ReadOnly="true" runat="server" MaxLength="12"
                                                                                            CssClass="textboxgrey right" Width="94%" TabIndex="1" Visible="False"></asp:TextBox>
                                                                                    </td>
                                                                                    <td width="200px">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td width="120px" style="height: 5px;">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="300px" colspan="3">
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                    </td>
                                                                                    <td width="200px">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trLatestUpfontAmount" runat="server">
                                                                                    <td width="120px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="300px" colspan="3" class="Gridheading">
                                                                                        <asp:Label ID="lblLatestUpfontAmount" Text="Payment Upfront Now" runat="server" Visible="False"></asp:Label>
                                                                                    </td>
                                                                                    <td width="100px" class="textbold">
                                                                                        <asp:TextBox ID="txtLatestUpfontAmount" ReadOnly="true" runat="server" MaxLength="12"
                                                                                            CssClass="textboxgrey right" Width="94%" TabIndex="1" Visible="False"></asp:TextBox>
                                                                                    </td>
                                                                                    <td width="200px">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td width="140px" style="height: 5px;">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="90px">
                                                                                    </td>
                                                                                    <td width="285px" colspan="3">
                                                                                    </td>
                                                                                    <td width="100px">
                                                                                    </td>
                                                                                    <td width="200px">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="9" style="padding-top: 10px;">
                                                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <div class="subheading" style="width: 100%; height: 18px;">
                                                                                                        &nbsp;&nbsp;Business Development Remarks</div>
                                                                                                </td>
                                                                                                <td>
                                                                                                    &nbsp;&nbsp;</td>
                                                                                                <td>
                                                                                                    <div class="subheading" style="width: 100%; height: 18px;">
                                                                                                        &nbsp;&nbsp;Payment Approval Remarks</div>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="padding-top: 10px; padding-left: 10px;">
                                                                                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="textboxgrey" TextMode="MultiLine"
                                                                                                        ReadOnly="true" Height="100px" Width="440px" TabIndex="1"></asp:TextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                                <td style="padding-top: 10px; padding-left: 10px;">
                                                                                                    <asp:TextBox ID="txtPayAppRemarks" runat="server" CssClass="textboxgrey" TextMode="MultiLine"
                                                                                                        ReadOnly="true" Height="100px" Width="440px" TabIndex="1"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="150" style="height: 5px;" colspan="2">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                    <td colspan="1" style="width: 2px;">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="80%">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr class="paddingtop paddingbottom">
                                            <td style="width: 35%" class="left">
                                                <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                    ID="txtTotalRecordCount" runat="server" Width="70px" CssClass="textboxgrey" ReadOnly="True"></asp:TextBox></td>
                                            <td style="width: 20%" class="right">
                                                <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"
                                                    TabIndex="7"><< Prev</asp:LinkButton></td>
                                            <td style="width: 25%" class="center">
                                                <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber"
                                                    Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" TabIndex="7">
                                                </asp:DropDownList></td>
                                            <td style="width: 25%" class="left">
                                                <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next"
                                                    TabIndex="7">Next >></asp:LinkButton></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
