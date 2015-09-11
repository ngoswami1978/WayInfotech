<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRSR_MailStatus.aspx.vb"
    Inherits="Training_TRSR_MailStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AAMS::Training::Manage Mail Status</title>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
    
    


    function Edit(StatusId)
    {
        window.location.href = "TRUP_MailStatus.aspx?Action=U&StatusId=" + StatusId;
        return false;
    }
    function Delete(StatusId)
    {
        if (confirm("Are you sure you want to delete?")== true)
        {
            document.getElementById('<%=hdID.ClientId%>').value  = StatusId;
            return true;
        }    
        return false;
    }
    </script>

</head>
<body>
    <form id="form1" runat="server" defaultfocus ="txtStatus" defaultbutton ="btnSearch">
        <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Training-&gt </span><span class="sub_menu">Mail Status</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Mail Status
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table style="width: 100%" border="0" cellpadding="1" cellspacing="2" class="left textbold">
                                                <tr>
                                                    <td colspan="4" class="gap" valign="top" align="center">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 16%">
                                                    </td>
                                                    <td style="width: 18%" class="textbold">
                                                        Mail Status Name<span class="Mandatory">*</span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="textbox" MaxLength="25" TabIndex="1"
                                                            Width="97%"></asp:TextBox></td>
                                                    <td style="width: 34%">
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="9"
                                                            AccessKey="s" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 16%">
                                                    </td>
                                                    <td style="width: 18%" class="textbold">
                                                    </td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td style="width: 34%">
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="10"
                                                            AccessKey="n" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 16%">
                                                    </td>
                                                    <td style="width: 18%" class="textbold">
                                                    </td>
                                                    <td style="width: 25%" class="textbold">
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="11"
                                                            AccessKey="r" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 16%; height: 23px;">
                                                    </td>
                                                    <td style="width: 18%; height: 23px;" class="textbold">
                                                    </td>
                                                    <td style="height: 23px">
                                                    </td>
                                                    <td style="width: 34%; height: 23px;">
                                                        <asp:Button ID="btnExport" CssClass="button" runat="server" Text="Export" TabIndex="11"
                                                            AccessKey="r" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 16%; height: 21px;">
                                                    </td>
                                                    <td style="width: 18%; height: 21px;" class="textbold">
                                                        <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" />
                                                        <asp:HiddenField ID="hdID" runat="server" />
                                                    </td>
                                                    <td style="height: 21px">
                                                    </td>
                                                    <td style="width: 34%; height: 21px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 16%; height: 23px;">
                                                    </td>
                                                    <td colspan="3" style="height: 23px">
                                                        <asp:GridView ID="gvStatus" runat="server" EnableViewState="False" AutoGenerateColumns="False"
                                                            TabIndex="6" Width="70%" AllowSorting="True">
                                                            <Columns>
                                                                <asp:BoundField DataField="TR_STATUS_NAME" HeaderText="Status" SortExpression="TR_STATUS_NAME" />
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="EditX" CssClass="LinkButtons">
                                                                        </asp:LinkButton>&nbsp;
                                                                        <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandName="DeleteX"
                                                                            CssClass="LinkButtons">
                                                                        </asp:LinkButton>
                                                                        <asp:HiddenField ID="hdStatusId" runat="server" Value='<%#Eval("TR_STATUS_ID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <RowStyle CssClass="textbold" />
                                                            <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="80%">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr class="paddingtop paddingbottom">
                                                                    <td style="width: 35%; height: 30px;" class="left">
                                                                        <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;<asp:TextBox
                                                                            ID="txtTotalRecordCount" runat="server" Width="70px" CssClass="textboxgrey" ReadOnly="True"></asp:TextBox></td>
                                                                    <td style="width: 20%; height: 30px;" class="right">
                                                                        <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                    <td style="width: 25%; height: 30px;" class="center">
                                                                        <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber"
                                                                            Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                                        </asp:DropDownList></td>
                                                                    <td style="width: 25%; height: 30px;" class="left">
                                                                        <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                </tr>
                                                <tr>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="text-align: center">
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
