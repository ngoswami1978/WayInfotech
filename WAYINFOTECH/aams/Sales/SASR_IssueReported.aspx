<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SASR_IssueReported.aspx.vb"
    Inherits="Sales_SASR_IssueReported" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS::Sales::DSR Details Issue Reported </title>

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <base target="_self" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
             function MandatoryData()
             {
              if (document.getElementById ('hdDSR_VISIT_ID').value.trim()=='' )
                {
//                    document.getElementById ('lblError').innerHTML="Please file the DSR for the date : "  + '<%=strDSRDate %>'                
//                    return false  ; 
                }
             
                if (document.getElementById ('txtDetailedDiscussion').value.trim()=='' )
                {
                    document.getElementById ('lblError').innerHTML="Detailed Disc /Issue Reported is mandatory."  
                    document.getElementById ('txtDetailedDiscussion').focus();
                    return false  ; 
                }
                
                 if (document.getElementById("txtDetailedDiscussion").value.trim().length>1000)
                {
                     document.getElementById("lblError").innerHTML=" Detailed Disc /Issue Reported greater than 1000 characters."
                     document.getElementById("txtDetailedDiscussion").focus();
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
                                <asp:Label ID="lblHeading" runat="server" Text="Service Call Comp Info /Mkt info Remarks" />
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
                                                    <td colspan="6" align="left">
                                                        <table cellpadding="0" cellspacing="0" width="650px">
                                                            <tr>
                                                                <td class="textbold" style="width: 210px;">
                                                                    &nbsp;Detailed Disc / Issue Reported<span class="Mandatory">*</span></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDetailedDiscussion" runat="server" CssClass="textbox" Width="340px"
                                                                        TextMode="MultiLine" Rows="5" Height="60px"></asp:TextBox>
                                                                </td>
                                                                <td>
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
                                                                <asp:BoundField DataField="RESP_1A_NAME" HeaderText="USER" HeaderStyle-HorizontalAlign="left" SortExpression ="RESP_1A_NAME"
                                                                    ItemStyle-Width="15%" />
                                                                <asp:BoundField DataField="DATETIME" HeaderText="DATE" HeaderStyle-HorizontalAlign="left" SortExpression ="DATETIME"
                                                                    ItemStyle-Wrap="true" Visible="true" />
                                                                <asp:BoundField DataField="ISSUEREPORT_REMARKS" HeaderText="Detailed Disc /Issue Reported " SortExpression ="ISSUEREPORT_REMARKS"
                                                                    HeaderStyle-HorizontalAlign="left" ItemStyle-Wrap="true" />
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
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
