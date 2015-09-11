<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SASR_SCFollowupRem.aspx.vb"
    Inherits="Sales_SASR_SCFollowupRem" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS::Sales::DSR Details Followup Remarks </title>

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <base target="_self" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
             function MandatoryData()
             {
                if (document.getElementById ('txtFollowupRem').value.trim()=='' )
                {
                    document.getElementById ('lblError').innerHTML="Remark is mandatory."  
                    document.getElementById ('txtFollowupRem').focus();
                    return false  ; 
                }
                
                 if (document.getElementById("txtFollowupRem").value.trim().length>1000)
                {
                     document.getElementById("lblError").innerHTML="Remark can't be greater than 1000 characters."
                     document.getElementById("txtFollowupRem").focus();
                     return false;
                }  
                
             }
    </script>

</head>
<body>
    <form id="frmSalesRemarks" runat="server">
        <table width="100%">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td align="right">
                                <a href="#" class="LinkButtons" onclick="window.close();">Close</a>&nbsp;&nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                <asp:Label ID="lblHeading" runat="server" Text="Followup Remarks" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td colspan="6" align="center">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <table width="650px">
                                                         
                                                            <tr class="top">
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td class="textbox" style="width: 120px;">
                                                                    Department
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtDept" runat="server" CssClass="textboxgrey" Width="160px" ReadOnly="true"
                                                                        TabIndex="1"></asp:TextBox>
                                                                </td>
                                                                <td class="textbold" >
                                                                    Deptt Specific
                                                                </td>
                                                                <td colspan ="2">
                                                                    <asp:TextBox ID="txtDepttSpecific" runat="server" Width="160px" CssClass="textboxgrey"
                                                                        ReadOnly="true"></asp:TextBox>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr class="top">
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td class="textbold">
                                                                    Status
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtServiceStatus" runat="server" CssClass="textboxgrey" Width="160px"
                                                                        ReadOnly="true" TabIndex="4"></asp:TextBox>
                                                                </td>
                                                                <td nowrap="nowrap" class="textbold">
                                                                    Assigned to
                                                                </td>
                                                                <td colspan ="2">
                                                                    <asp:TextBox ID="TxtAssignedTo" runat="server" CssClass="textboxgrey" Width="160px"
                                                                        ReadOnly="true" TabIndex="4"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td nowrap="nowrap" class="textbold">
                                                                    Closer Date
                                                                </td>
                                                                <td nowrap="nowrap">
                                                                    <asp:TextBox ID="txtCloserDate" runat="server" CssClass="textboxgrey" Width="160px"
                                                                        TabIndex="4" ReadOnly="true"></asp:TextBox>
                                                                </td>
                                                                <td class="textbold">
                                                                    Target Closer Date
                                                                </td>
                                                                <td colspan ="2">
                                                                    <asp:TextBox ID="txtTargetCloserDate" runat="server" CssClass="textboxgrey" Width="160px"
                                                                        ReadOnly="true" TabIndex="1"></asp:TextBox>
                                                                </td>
                                                             
                                                            </tr>
                                                             <tr class="top">
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td class="textbox" style="width: 120px;">
                                                                   Detailed Disc /Issue Reported
                                                                </td>
                                                               
                                                                <td colspan ="4">
                                                                <asp:TextBox ID="txtDetailedDiscussion" runat="server" CssClass="textboxgrey" Width="95%"
                                                                                            TextMode="MultiLine" Rows="4" Height="40px" ReadOnly="True"></asp:TextBox>
                                                                   
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 140px;">
                                                                    Followup Remarks <span class="mandatory">*</span></td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="txtFollowupRem" runat="server" CssClass="textbox" TextMode="MultiLine"
                                                                        Rows="5" Width="99%" Height="60px"></asp:TextBox>
                                                                </td>                                                                
                                                                <td style ="width :100px">
                                                                    <asp:Button ID="BtnSave" runat="Server" Text="Save" CssClass="button" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" align="center">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" height="4" align="center">
                                                        <asp:GridView ID="gvRemarks" AllowSorting="true" HeaderStyle-ForeColor="white" runat="server"
                                                            BorderWidth="1" BorderColor="#d4d0c8" AutoGenerateColumns="False" Width="95%"
                                                            TabIndex="9">
                                                            <Columns>
                                                                <asp:BoundField DataField="RESP_1A_NAME" HeaderText="USER" HeaderStyle-HorizontalAlign="left"
                                                                    ItemStyle-Width="15%" SortExpression ="RESP_1A_NAME" />
                                                                <asp:BoundField DataField="DATETIME" HeaderText="DATE" HeaderStyle-HorizontalAlign="left"
                                                                    ItemStyle-Wrap="true" Visible="true" SortExpression ="DATETIME" />
                                                                <asp:BoundField DataField="FOLLOWUP_REMARKS" HeaderText="Followup Remark" SortExpression ="FOLLOWUP_REMARKS"  />
                                                            </Columns>
                                                            <HeaderStyle CssClass="Gridheading" />
                                                            <RowStyle CssClass="ItemColor" HorizontalAlign="Left" />
                                                            <AlternatingRowStyle CssClass="lightblue" HorizontalAlign="Left" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="650px">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr class="paddingtop paddingbottom">
                                                                    <td style="width: 30%" class="left">
                                                                        <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                            ID="txtTotalRecordCount" runat="server" Width="50px" CssClass="textboxgrey" ReadOnly="true"></asp:TextBox></td>
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="hidden" runat="server" id="hdDSR_VISIT_ID" style="width: 1px" />
                    <input type="hidden" runat="server" id="hdLCode" style="width: 1px" />
                    <input type="hidden" runat="server" id="hdVisitDATE" style="width: 1px" />
                    <input type="hidden" runat="server" id="HdVisitType" style="width: 1px" />
                    <input type="hidden" runat="server" id="hdDSR_SC_DETAIL_ID" style="width: 1px" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
